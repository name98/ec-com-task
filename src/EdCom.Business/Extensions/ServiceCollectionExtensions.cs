using EdCom.Business.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EdCom.Business.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddBusiness(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddScoped<ICategoryService, OrderService>();
        serviceCollection.AddScoped<IPurchaseService, PurchaseService>();
        serviceCollection.AddScoped<IReportService, ReportService>();
    }
}