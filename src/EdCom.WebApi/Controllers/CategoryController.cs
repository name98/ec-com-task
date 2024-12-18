using EdCom.Business.Dtos;
using EdCom.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace EdCom.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CategoryController(ICategoryService service) : ControllerBase
{
    private readonly ICategoryService _service = service;

    [HttpGet]
    public async Task<IReadOnlyCollection<CategoryGetDto>> GetList(
        CancellationToken cancellationToken)
    {
        var result = await _service.GetList(cancellationToken);

        return result;
    }

    [HttpGet("{id}")]
    public async Task<CategoryGetDto> GetById(
        Guid id,
        CancellationToken cancellationToken)
    {
        var result = await _service.GetById(id, cancellationToken);

        return result;
    }

    [HttpPost]
    [ProducesResponseType(201)]
    public async Task<Guid> Create(
        [FromBody] CategoryDto dto,
        CancellationToken cancellationToken)
    {
        var result = await _service.Create(dto, cancellationToken);

        return result;
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        Guid id,
        [FromBody] CategoryDto dto,
        CancellationToken cancellationToken)
    {
        await _service.Update(id, dto, cancellationToken);

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
