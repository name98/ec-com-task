using System.Text.Json.Serialization;

namespace EdCom.Business.Dtos;

public record PurchaseDto(
    decimal Price,
    Guid CategoryId,
    DateTime DateOfPurchase,
    [property:JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    string? Comment = null);