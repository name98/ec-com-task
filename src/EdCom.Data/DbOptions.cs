namespace EdCom.Data;

public record DbOptions
{
    public const string KEY = "DbSettings";

    public required string ConnectionString { get; init; } = null!;
}