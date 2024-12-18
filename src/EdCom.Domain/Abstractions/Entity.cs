namespace EdCom.Domain.Abstractions;

public abstract class Entity : IEntity
{
    public Guid Id { get; init; } = Guid.NewGuid();

    public DateTime DateCreated { get; init; } = DateTime.UtcNow;
}