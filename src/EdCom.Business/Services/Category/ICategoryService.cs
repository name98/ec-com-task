using EdCom.Business.Dtos;

namespace EdCom.Business.Services;

public interface ICategoryService
{
    Task<IReadOnlyCollection<CategoryGetDto>> GetList(
        CancellationToken cancellationToken = default);

    Task<CategoryGetDto> GetById(
        Guid id,
        CancellationToken cancellationToken = default);

    Task<Guid> Create(
        CategoryDto dto,
        CancellationToken cancellationToken = default);

    Task Update(
        Guid id,
        CategoryDto dto, CancellationToken cancellationToken = default);

    Task Delete(
        Guid id,
        CancellationToken cancellationToken = default);
}
