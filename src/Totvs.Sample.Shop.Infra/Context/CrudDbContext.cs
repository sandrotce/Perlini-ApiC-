using Microsoft.EntityFrameworkCore;
using Tnf.EntityFrameworkCore;
using Tnf.Runtime.Session;
using Totvs.Sample.Shop.Domain.Entities;
using Totvs.Sample.Shop.Infra.Context.Builders;

namespace Totvs.Sample.Shop.Infra.Context {
    public abstract class CrudDbContext : TnfDbContext {
        
        public DbSet<Product> Products { get; set; }
        public DbSet<Price> Prices { get; set; }        

        public CrudDbContext (DbContextOptions<CrudDbContext> options, ITnfSession session) : base (options, session) { }

        protected override void OnModelCreating (ModelBuilder modelBuilder) {
            base.OnModelCreating (modelBuilder);
            
            modelBuilder.ApplyConfiguration (new ProductTypeConfiguration ());
            modelBuilder.ApplyConfiguration (new PriceTypeConfiguration ());
        }
    }
}