using Microsoft.EntityFrameworkCore;
using tparf.Models;
namespace tparf.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        //public DbSet<CategoryProduct> CategoryProducts { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        //public DbSet<ManufacturerProduct> ManufacturerProducts { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductProperty> ProductProperties { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<User> Users { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<CategoryProduct>()
        //        .HasKey(pc => new { pc.ProductId, pc.CategoryId });
        //    modelBuilder.Entity<CategoryProduct>()
        //        .HasOne(p => p.Product)
        //        .WithMany(pc => pc.CategoryProducts)
        //        .HasForeignKey(p => p.ProductId);
        //    modelBuilder.Entity<CategoryProduct>()
        //        .HasOne(p => p.Category).
        //        WithMany(pc => pc.CategoryProducts)
        //        .HasForeignKey(c => c.CategoryId);

        //    modelBuilder.Entity<ManufacturerProduct>()
        //        .HasKey(po => new { po.ProductId, po.ManufacturerId });
        //    modelBuilder.Entity<ManufacturerProduct>()
        //        .HasOne(p => p.Product)
        //        .WithMany(pc => pc.ManufacturerProducts)
        //        .HasForeignKey(p => p.ProductId);
        //    modelBuilder.Entity<ManufacturerProduct>()
        //        .HasOne(p => p.Manufacturer).
        //        WithMany(pc => pc.ManufacturerProducts)
        //        .HasForeignKey(c => c.ManufacturerId);
        //}
    }
}
