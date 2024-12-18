namespace EdCom.Business.Dtos;

public record ReportDto(
    int Year,
    decimal TotalAmountSpent,
    IReadOnlyCollection<StatisticByMonthDto> StatisticByMonths);

public record StatisticByMonthDto(
    int Month,
    decimal TotalAmountSpent,
    IReadOnlyCollection<StatisticByCategoryDto> StatisticByCategories);

public record StatisticByCategoryDto(
    Guid Id,
    string Title,
    decimal TotalAmountSpent,
    int NumberOfPurchases);
