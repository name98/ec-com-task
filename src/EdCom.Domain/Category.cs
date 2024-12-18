using EdCom.Domain.Abstractions;

namespace EdCom.Domain;

public class Category : Entity
{
    public Category(string title, int order): base()
    {
        Title = title;
        Order = order;
    }

    public string Title { get; set; }

    public int Order { get; set; }

    public List<Purchase> Purchases { get; set; } = [];
}

