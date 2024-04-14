using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using tparf.api.Entities;

namespace tparf.api.Data
{
    public class TparfDbContext : IdentityDbContext<ApplicationUser, IdentityRole<long>, long>
    {
        public TparfDbContext(DbContextOptions<TparfDbContext> options) : base(options)
        {
            Database.Migrate();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<ProductDescription> Descriptions { get; set; }
        public DbSet<Manufacturer> Manufacturers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<TokenInfo> TokenInfo { get; set; }
        public DbSet<Characteristic> Characteristics { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderStatus> OrderStatuses { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<Сurrencies> Сurrencies { get; set; }
    }
}
