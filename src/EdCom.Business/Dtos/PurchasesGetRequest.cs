namespace EdCom.Business.Dtos;

public class PurchasesGetRequest: PaginatedRequest
{
    public Guid? CategoryId { get; init; } = null;

    public string? OrderBy { get; init; } = null;

    public string? Order { get; init; } = null;
}