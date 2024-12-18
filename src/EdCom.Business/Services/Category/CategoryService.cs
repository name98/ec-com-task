using EdCom.Business.Dtos;
using EdCom.Business.Exceptions;
using EdCom.Data;
using EdCom.Domain;
using Microsoft.EntityFrameworkCore;

namespace EdCom.Business.Services;

public class OrderService(EdComDbContext dbContext) : ICategoryService
{
    private readonly EdComDbContext _dbContext = dbContext;

    public async Task<CategoryGetDto> GetById(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Categories
            .GetById(
                id,
                p => new CategoryGetDto(
                    p.Id,
                    p.Title,
                    p.Order),
                cancellationToken);

        return result;
    }

    public async Task<IReadOnlyCollection<CategoryGetDto>> GetList(
        CancellationToken cancellationToken = default)
    {
        var result = await _dbContext.Categories
            .OrderBy(p => p.Order)
            .Select(o => new CategoryGetDto(
                o.Id,
                o.Title,
                o.Order))
            .ToArrayAsync(cancellationToken);

        return result;
    }

    public async Task Delete(
        Guid id,
        CancellationToken cancellationToken = default)
    {
        var hasSomePurchase = await _dbContext.Purchases
            .AnyAsync(p => p.CategoryId == id, cancellationToken);

        if (hasSomePurchase)
        {
            throw new BusinessValidationException("Category refer to some purchases");
        }

        var entity = await _dbContext.Categories
            .Include(p => p.Purchases)
            .GetById(id, cancellationToken);

        _dbContext.Remove(entity);

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task<Guid> Create(
        CategoryDto dto,
        CancellationToken cancellationToken = default)
    {
        EnsureValidDto(dto);

        await EnsureTitleUnique(dto.Title, cancellationToken);

        var entity = new Category(dto.Title, dto.Order);

        await _dbContext.AddAsync(entity, cancellationToken);

        await _dbContext.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

    public async Task Update(
        Guid id,
        CategoryDto dto,
        CancellationToken cancellationToken = default)
    {
        EnsureValidDto(dto);

        var entity = await _dbContext.Categories
            .GetById(id, cancellationToken);

        if (string.Equals(entity.Title, dto.Title, StringComparison.OrdinalIgnoreCase) is false)
        {
            await EnsureTitleUnique(dto.Title, cancellationToken);
        }

        entity.Title = dto.Title;
        entity.Order = dto.Order;

        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    private static void EnsureValidDto(CategoryDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Title))
        {
            throw new BusinessValidationException("Title is empty");
        }
    }

    private async Task EnsureTitleUnique(
        string title,
        CancellationToken cancellationToken)
    {
        var titleExists = await _dbContext.Categories
            .AnyAsync(p => p.Title.ToLower() == title.ToLower(), cancellationToken);

        if (titleExists)
            throw new BusinessValidationException($"Category with title '{title}' already exists");
    }
}