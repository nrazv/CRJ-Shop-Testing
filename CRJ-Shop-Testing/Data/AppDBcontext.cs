using CRJ_Shop.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CRJ_Shop.Data
{
    public class AppDbContext : IdentityDbContext<IdentityUser>
    {

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<UserOrder> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);


            string customerRoleId = Guid.NewGuid().ToString();
            string adminUserId = Guid.NewGuid().ToString();

            // Seed roles
            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole { Id = adminUserId, Name = "Admin", NormalizedName = "ADMIN" },
                new IdentityRole { Id = customerRoleId, Name = "Customer", NormalizedName = "CUSTOMER" }
            );

            modelBuilder
               .Entity<Category>()
               .Property(e => e.ProductCategory)
               .HasConversion(
                v => v.ToString(),
                v => (AvailableCategories)Enum.Parse(typeof(AvailableCategories), v)
           );

            modelBuilder.Entity<ProductCategory>().HasKey(pc => new
            {
                pc.ProductId,
                pc.CategoryId,
            });

            modelBuilder.Entity<ProductCategory>()
             .HasOne(pc => pc.Product)
             .WithMany(p => p.ProductCategories)
             .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(c => c.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

        }

    }
}