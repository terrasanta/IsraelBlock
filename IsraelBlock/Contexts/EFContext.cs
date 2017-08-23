using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using IsraelBlock.Models;
using System.Data.Entity;

namespace IsraelBlock.Contexts
{
    public class EFContextDbContext : DbContext
    {
        public EFContextDbContext() : base("Asp_Net_MVC_CS") { }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Maker> Makers{ get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
    }
}