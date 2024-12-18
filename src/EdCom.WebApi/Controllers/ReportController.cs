using EdCom.Business.Dtos;
using EdCom.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdCom.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController(IReportService service) : ControllerBase
{
    private readonly IReportService _service = service;

    [HttpGet]
    public async Task<IReadOnlyCollection<ReportDto>> Get(
        [FromQuery] uint? year,
        CancellationToken cancellationToken)
    {
        var result = await _service.Get(year, cancellationToken);

        return result;
    }
}
