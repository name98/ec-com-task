using EdCom.Business.Dtos;
using EdCom.Data;
using Microsoft.EntityFrameworkCore;

namespace EdCom.Business.Services;

public class ReportService(EdComDbContext dbContext) : IReportService
{
    private readonly EdComDbContext _dbContext = dbContext;

    public async Task<IReadOnlyCollection<ReportDto>> Get(
        uint? year,
        CancellationToken cancellationToken = default)
    {
        var query = _dbContext.Purchases.AsQueryable();
        if (year.HasValue)
        {
            query = query.Where(p => p.DateOfPurchase.Year == year.Value);
        }

        var result = await query
            .GroupBy(p => p.DateOfPurchase.Year)
            .OrderBy(p => p.Key)
            .Select(p => new ReportDto(
                p.Key,
                p.Sum(x => x.Price),
                p.GroupBy(m => m.DateOfPurchase.Month)
                    .OrderBy(m => m.Key)
                    .Select(m => new StatisticByMonthDto(
                        m.Key,
                        m.Sum(x => x.Price),
                        m.GroupBy(g => g.Category)
                            .Select(c => new StatisticByCategoryDto(
                                c.Key.Id,
                                c.Key.Title,
                                c.Sum(x => x.Price),
                                c.Count()))
                            .ToArray()))
                    .ToArray()))
            .ToArrayAsync(cancellationToken);

        return result;
    }
}