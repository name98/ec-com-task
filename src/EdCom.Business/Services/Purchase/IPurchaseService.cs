using EdCom.Business.Dtos;

namespace EdCom.Business.Services;

public interface IPurchaseService
{
    Task<PurchasesGetDto> Get(
        PurchasesGetRequest request,
        CancellationToken cancellationToken = default);

    Task<Guid> Create(
        PurchaseDto dto,
        CancellationToken cancellationToken = default);

    Task Update(
        Guid id,
        PurchaseDto dto,
        CancellationToken cancellationToken = default);

    Task Delete(
        Guid id,
        CancellationToken cancellationToken = default);
}

