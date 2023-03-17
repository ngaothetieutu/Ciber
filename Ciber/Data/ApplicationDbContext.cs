using Ciber.Data.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.X509Certificates;
using System.Xml;

namespace Ciber.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Category> Categorys { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {

            base.OnModelCreating(builder);
            // Bỏ tiền tố AspNet của các bảng: mặc định
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                var tableName = entityType.GetTableName();
                if (tableName.StartsWith("AspNet"))
                {
                    entityType.SetTableName(tableName.Substring(6));
                }
            }
            builder.Entity<ApplicationUser>(b =>
            {
                b.ToTable("Users");
            });
            builder.Entity<Order>(b =>
            {
                b.ToTable("Order");
                b.HasKey("Id");
                b.Property(c => c.OrderName).HasMaxLength(250).IsRequired();
                b.HasOne(c => c.Product)
                    .WithMany(c => c.Orders)
                    .HasForeignKey(c => c.ProductId)
                    .OnDelete(DeleteBehavior.Restrict);
                b.HasOne(c => c.User)
                .WithMany(c => c.Orders)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            });
            builder.Entity<Category>(b =>
            {
                b.ToTable("Category");
                b.HasKey("Id");
                b.Property(u => u.Name).HasMaxLength(250);

                b.HasData(
                    new {Id = 1, Name = "Ti Vi", Description = "Các sản phẩm TV"},
                    new {Id =2, Name = "Tủ lạnh", Description = "Các sản phẩm Tủ lạnh" },
                    new {Id = 3, Name = "Máy tính", Description = "Các sản phẩm máy tính" }
                    );
            });
            builder.Entity<Product>(b => 
            { 
                b.ToTable("Product");
                b.HasKey("Id");
                b.Property(u => u.Name).HasMaxLength(250);
                b.HasOne(s => s.Category)
                .WithMany(ad => ad.Products)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);
                b.HasData(
                   new { Id = 1, Name = "Ti Vi SamSung",CategoryId = 1,Price = 10000000, Quantity = 5, Description = "TV SamSung SS1" },
                   new { Id = 2, Name = "Ti Vi LG", CategoryId = 1, Price = 9000000, Quantity = 4, Description = "TV LG" },
                   new { Id = 3, Name = "Ti Vi Sony", CategoryId = 1, Price = 11000000, Quantity = 3, Description = "TV Sony" },
                   new { Id = 4, Name = "Tủ lạnh Panasonic", CategoryId = 2, Price = 15000000, Quantity = 3, Description = "Tủ lạnh Panasonic L110" },
                   new { Id = 5, Name = "Tủ lạnh Panasonic", CategoryId = 2, Price = 18000000, Quantity = 3, Description = "Tủ lạnh Panasonic L150" },
                   new { Id = 6, Name = "Tủ lạnh LG", CategoryId = 2, Price = 11000000, Quantity = 6, Description = "Tủ lạnh LG L110" },
                   new { Id = 7, Name = "Laptop Gaming Assus", CategoryId = 3, Price = 11000000, Quantity = 5, Description = "Laptop gaming assus" },
                   new { Id = 8, Name = "Laptop Dell v12", CategoryId = 3, Price = 14000000, Quantity = 2, Description = "Laptop Dell" },
                   new { Id = 9, Name = "Laptop Lenovo", CategoryId = 3, Price = 15000000, Quantity = 6, Description = "Laptop Lenovo" }
                   );
            });
        }
    }
}