using eCommerceStarterCode.Configuration;
using eCommerceStarterCode.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerceStarterCode.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions options)
            :base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<ShoppingCart> ShoppingCarts { get; set; }
        public DbSet<Vendor> Vendors { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Category> Categories { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Composite Key Declaration
            modelBuilder.Entity<ShoppingCart>().HasKey(SC => new { SC.CustomerId, SC.ProductId });

            //end of keys
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RolesConfiguration());
        }

    }
}
