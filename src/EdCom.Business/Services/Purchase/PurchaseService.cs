using System.Linq.Expressions;
using EdCom.Business.Dtos;
using EdCom.Business.Exceptions;
using EdCom.Data;
using EdCom.Domain;
using Microsoft.EntityFrameworkCore;

namespace EdCom.Business.Services;

public class PurchaseService(EdComDbContext dbContext) : IPurchaseService
{
    private readonly EdComDbContext _dbContext = dbContext;

    public async Task<PurchasesGetDto> Get(
        PurchasesGetRequest request,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Purchases.AsQueryable();

        if (request.CategoryId.HasValue)
        {
            query = query.Where(p => p.CategoryId == request.CategoryId.Value);
        }

        IQueryable<Purchase> Order<TKey>(Expression<Func<Purchase, TKey>> keySelector)
            => request.Order is "asc"
                ? query.OrderBy(keySelector)
                : query.OrderByDescending(keySelector);

        query = request.OrderBy switch
        {
            "price" => Order(p => p.Price),
            "category" => Order(p => p.Category.Title),
            _ => Order(p => p.DateOfPurchase),
        };

        var count = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(request.Page * request.PageSize)
            .Take(request.PageSize)
            .Select(p => new PurchaseGetDto(
                p.Id,
                p.Price,
                p.CategoryId,
                p.Category.Title,
                p.DateOfPurchase,
                p.Comment))
            .ToArrayAsync(cancellationToken);

        return new PurchasesGetDto(items, count);
    }

    public async Task<Guid> Create(
        PurchaseDto dto,
        CancellationToken cancellationToken = default)
    {
        EnsureValidDto(dto);

        var category = await _dbContext.Categories
            .GetById(dto.CategoryId, cancellationToken);

        var entity = new Purchase()
        {
            Price = dto.Price,
            Category = category,
            DateOfPurchase = dto.DateOfPurchase,
            Comment = string.IsNullOrWhiteSpace(dto.Comment)
                ? null
                : dto.Comment,
        };

        await _dbContext.AddAsync(entity, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    public async Task Delete(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var entity = await _dbContext.Purchases
            .GetById(id, cancellationToken);

        _dbContext.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task Update(
        Guid id,
        PurchaseDto dto,
        CancellationToken cancellationToken = default)
    {
        EnsureValidDto(dto);

        var purchase = await _dbContext.Purchases
            .GetById(id, cancellationToken);

        if (dto.CategoryId != purchase.CategoryId)
        {
            var category = await _dbContext.Categories
                .GetById(id, cancellationToken);

            purchase.Category = category;
        }

        purchase.Price = dto.Price;
        purchase.DateOfPurchase = dto.DateOfPurchase;
        purchase.Comment = string.IsNullOrWhiteSpace(dto.Comment)
            ? null
            : dto.Comment;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static void EnsureValidDto(PurchaseDto dto)
    {
        if (dto.Price <= 0)
        {
            throw new BusinessValidationException("Price must be positive");
        }
    }
}