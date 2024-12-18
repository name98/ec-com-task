using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace EdCom.Data;

public class EdComDbContextFactory : IDbContextFactory<EdComDbContext>, IDesignTimeDbContextFactory<EdComDbContext>
{
    private readonly DbOptions _dbOptions;
    public EdComDbContextFactory(IOptions<DbOptions> options)
    {
        _dbOptions = options.Value;
    }

    public EdComDbContextFactory()
    {
    }

    public EdComDbContext CreateDbContext()
    {
        var optionsBuilder = new DbContextOptionsBuilder<EdComDbContext>();
        optionsBuilder.UseNpgsql(_dbOptions.ConnectionString);
        optionsBuilder.UseSnakeCaseNamingConvention();

        return new EdComDbContext(optionsBuilder.Options);
    }

    EdComDbContext IDesignTimeDbContextFactory<EdComDbContext>.CreateDbContext(string[] args)
    {
        var directory = new DirectoryInfo(Directory.GetCurrentDirectory());
        while (directory != null && directory.GetFiles("*.sln").Length is 0)
        {
            directory = directory.Parent;
        }

        if (directory is null)
        {
            throw new NullReferenceException("Solution directory not found");
        }

        const string appSettings = "appsettings.json";
        var settingsPath = Path.Combine(directory.FullName, "src", "EdCom.WebApi", appSettings);
        Console.WriteLine("Settings path: {0}", settingsPath);
        if (File.Exists(settingsPath) is false)
        {
            throw new Exception($"{settingsPath} not found.");
        }

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(settingsPath, false)
            .Build();

        var connectionString = configuration.GetRequiredSection(DbOptions.KEY).GetSection("ConnectionString").Value
            ?? throw new Exception("Invalid connection string");

        var dbOptions = Options.Create(new DbOptions
        {
            ConnectionString = connectionString,
        });

        return new EdComDbContextFactory(dbOptions).CreateDbContext();
    }
}