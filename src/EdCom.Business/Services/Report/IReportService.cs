using EdCom.Business.Dtos;

namespace EdCom.Business.Services;

public interface IReportService
{
    Task<IReadOnlyCollection<ReportDto>> Get(
        uint? year = null,
        CancellationToken cancellationToken = default);
}