using EdCom.Business.Extensions;
using EdCom.Data;
using EdCom.WebApi.Middlewares;

namespace EdCom.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.Configure<DbOptions>(builder.Configuration.GetRequiredSection(DbOptions.KEY));

        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddAppDbContext();
        builder.Services.AddBusiness();

        builder.Services.AddTransient<ExceptionHandlingMiddleware>();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}
