using EdCom.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EdCom.Data.Configurations;

internal class PurchaseConfiguration: EntityConfiguration<Purchase>
{
    public override void Configure(EntityTypeBuilder<Purchase> builder)
    {
        base.Configure(builder);

        builder.HasOne(p => p.Category)
            .WithMany(o => o.Purchases)
            .HasForeignKey(p => p.CategoryId);
    }
}