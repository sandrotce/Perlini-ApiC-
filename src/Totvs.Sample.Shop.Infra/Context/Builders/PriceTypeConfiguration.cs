using Totvs.Sample.Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Totvs.Sample.Shop.Infra.Context.Builders
{
    public class PriceTypeConfiguration : IEntityTypeConfiguration<Price>
    {
        public void Configure(EntityTypeBuilder<Price> builder)
        {
            builder.ToTable("Price");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.StartDate).IsRequired();
            builder.Property(p => p.EndDate).IsRequired();

            builder.HasOne(p => p.Product)
               .WithMany(p => p.Prices)
               .HasForeignKey(x => x.ProductId);
        }
    }
}
