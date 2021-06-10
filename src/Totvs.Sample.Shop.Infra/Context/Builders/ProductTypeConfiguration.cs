using Totvs.Sample.Shop.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Totvs.Sample.Shop.Infra.Context.Builders
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Product");

            builder.HasKey(k => k.Id);
            builder.Property(p => p.Code).IsRequired();
            builder.Property(p => p.Name).IsRequired();
        }
    }
}
