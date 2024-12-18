using EdCom.Business.Dtos;
using EdCom.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdCom.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController(IPurchaseService service) : ControllerBase
{
    private readonly IPurchaseService _service = service;

    [HttpGet]
    public async Task<PurchasesGetDto> Get(
        [FromQuery] PurchasesGetRequest request,
        CancellationToken cancellationToken)
    {
        var result = await _service.Get(request, cancellationToken);

        return result;
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<Guid> Create(
        [FromBody] PurchaseDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _service.Create(dto, cancellationToken);

        return result;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] PurchaseDto dto,
        CancellationToken cancellationToken)
    {
        await _service.Update(
            id,
            dto,
            cancellationToken);

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(
        Guid id,
        CancellationToken cancellationToken)
    {
        await _service.Delete(id, cancellationToken);

        return NoContent();
    }
}
