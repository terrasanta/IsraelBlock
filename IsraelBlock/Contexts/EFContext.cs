using System.Data.Entity;
using IsraelBlock.Migrations;
using IsraelBlock.Models;

namespace IsraelBlock.Contexts
{
    public class EFContextDbContext : DbContext
    {
        public EFContextDbContext() : base("Asp_Net_MVC_CS")
        {
            Database.SetInitializer<EFContextDbContext>(new MigrateDatabaseToLatestVersion<EFContextDbContext, Configuration>());
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<Item> Items { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
          /*  modelBuilder.HasDefaultSchema("AULA_ASP");

            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnType("varchar2");
            modelBuilder.Entity<Supplier>().Property(x => x.Name).HasColumnType("varchar2");
            modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnType("varchar2");
            modelBuilder.Entity<Sale>().Property(x => x.ClientName).HasColumnType("varchar2");
            modelBuilder.Entity<Sale>().Property(x => x.ClientEmail).HasColumnType("varchar2");
            modelBuilder.Entity<Sale>().Property(x => x.ClientPhone).HasColumnType("varchar2");
            modelBuilder.Entity<Sale>().Property(x => x.ClientAddress).HasColumnType("varchar2");
            modelBuilder.Entity<PaymentType>().Property(x => x.NamePaymenttype).HasColumnType("varchar2");

            base.OnModelCreating(modelBuilder);*/
        }
    }
}