using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Entity;
using System.Threading.Tasks;
using Brainstorm.Model.Model;

namespace Brainstorm.DatabaseContext.DatabaseContext
{
   public class ProjectDbContext:DbContext
    {
        public ProjectDbContext()
        {
            Configuration.LazyLoadingEnabled = false; // Disable Lazy Loading
        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Purchase> Purchases { get; set; }
        public DbSet<PurchaseProduct> PurchaseProducts { get; set; }
        public DbSet<Sale> Sales { get; set; }
        public DbSet<SaleProduct> SaleProducts { get; set; }
    }
}
