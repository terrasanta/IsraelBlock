namespace IsraelBlock.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<IsraelBlock.Contexts.EFContextDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(IsraelBlock.Contexts.EFContextDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            //  to avoid creating duplicate seed data. E.g.
            //
            /*   context.PaymentType.AddOrUpdate(
                  p => p.NamePaymenttype,
                  new Models.PaymentType { NamePaymenttype = "Dinheiro" },
                  new Models.PaymentType { NamePaymenttype = "Cart�o Cr�dito" },
                  new Models.PaymentType { NamePaymenttype = "Cart�o D�bito" },
                  new Models.PaymentType { NamePaymenttype = "Vale Alimenta��o" },
                  new Models.PaymentType { NamePaymenttype = "Vale Refei��o" }
                );
            */
        }
    }
}