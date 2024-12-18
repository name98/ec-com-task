using EdCom.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdCom.Data.Configurations;

internal class CategoryConfiguration: EntityConfiguration<Category>
{
    private static DateTime DateCreated = new (2024, 12, 18, 0, 0, 0, DateTimeKind.Utc);

    public override void Configure(EntityTypeBuilder<Category> builder)
    {
        base.Configure(builder);

        builder.Property(p => p.Title).HasMaxLength(200);
        builder.HasIndex(p => p.Title).IsUnique();

        builder.HasData(
            new Category("Food", 100)
            {
                Id = new("D14E7C25-5351-47D6-A614-96EFA7497DC2"),
                DateCreated = DateCreated,
            },
            new Category("Transport", 200)
            {
                Id = new("94EFE18C-E82C-48A1-B4EE-96921B3F2EA0"),
                DateCreated = DateCreated,
            },
            new Category("Mobile Network", 300)
            {
                Id = new("E2A18BF7-01E3-47CF-BAF7-944E8B759198"),
                DateCreated = DateCreated,
            },
            new Category("Internet", 400)
            {
                Id = new("D5D150A6-0B9B-4D44-AED9-AE42CCA848B5"),
                DateCreated = DateCreated,
            },
            new Category("Entertainment", 500)
            {
                Id = new("6548BC25-0CB2-4FEC-BE40-D296AB261EC9"),
                DateCreated = DateCreated,
            });
    }
}