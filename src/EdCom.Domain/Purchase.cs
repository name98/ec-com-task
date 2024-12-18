using EdCom.Domain.Abstractions;

namespace EdCom.Domain;

public class Purchase : Entity
{
    public decimal Price { get; set; }

    public Guid CategoryId { get; set; }

    public Category Category { get; set; } = null!;

    public DateTime DateOfPurchase { get; set; }

    public string? Comment { get; set; } = null;
}