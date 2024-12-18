namespace EdCom.Business.Dtos;

public record CategoryGetDto(
    Guid Id,
    string Title,
    int Order): CategoryDto(
        Title,
        Order);