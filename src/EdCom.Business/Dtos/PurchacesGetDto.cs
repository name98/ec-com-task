namespace EdCom.Business.Dtos;

public record PurchasesGetDto(
    IReadOnlyCollection<PurchaseGetDto> Items,
    int TotalCount);

public record PurchaseGetDto(
    Guid Id,
    decimal Price,
    Guid CategoryId,
    string CategoryTitle,
    DateTime DateOfPurchase,
    string? Comment = null): PurchaseDto(
        Price,
        CategoryId,
        DateOfPurchase,
        Comment);