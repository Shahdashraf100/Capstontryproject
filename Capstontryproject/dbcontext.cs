using Capstontryproject.Models;
using Microsoft.EntityFrameworkCore;

namespace Capstontryproject
{
    public class dbcontext : DbContext
    {
        public dbcontext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // إعدادات User
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(u => u.Id);
                entity.Property(u => u.Username)
                      .IsRequired()
                      .HasMaxLength(100);

                entity.Property(u => u.Email)
                      .IsRequired()
                      .HasMaxLength(150);

                entity.HasOne(u => u.Cart)
                      .WithOne(c => c.User)
                      .HasForeignKey<Cart>(c => c.UserId);

                entity.HasMany(u => u.Orders)
                      .WithOne(o => o.User)
                      .HasForeignKey(o => o.UserId);
            });

            // إعدادات Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(p => p.Id);
                entity.Property(p => p.Name)
                      .IsRequired()
                      .HasMaxLength(200);

                entity.Property(p => p.Description)
                      .HasMaxLength(500);

                entity.Property(p => p.Price)
                      .HasColumnType("decimal(18, 2)");

                entity.Property(p => p.ImageUrl)
                      .HasMaxLength(300);
            });

            // إعدادات Cart
            modelBuilder.Entity<Cart>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.CreatedAt)
                      .HasDefaultValueSql("GETDATE()");

                entity.HasOne(c => c.User)
                      .WithOne(u => u.Cart)
                      .HasForeignKey<Cart>(c => c.UserId);

                entity.HasMany(c => c.CartItems)
                      .WithOne(ci => ci.Cart)
                      .HasForeignKey(ci => ci.CartId);
            });

            // إعدادات CartItem
            modelBuilder.Entity<CartItem>(entity =>
            {
                entity.HasKey(ci => ci.Id);

                entity.Property(ci => ci.Price)
                      .HasColumnType("decimal(18, 2)");

                entity.HasOne(ci => ci.Cart)
                      .WithMany(c => c.CartItems)
                      .HasForeignKey(ci => ci.CartId);

                entity.HasOne(ci => ci.Product)
                      .WithMany(p => p.CartItems)
                      .HasForeignKey(ci => ci.ProductId);
            });

            // إعدادات Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(o => o.Id);
                entity.Property(o => o.OrderDate)
                      .HasDefaultValueSql("GETDATE()");

                entity.Property(o => o.TotalAmount)
                      .HasColumnType("decimal(18, 2)");

                entity.HasOne(o => o.User)
                      .WithMany(u => u.Orders)
                      .HasForeignKey(o => o.UserId);

                entity.HasMany(o => o.OrderItems)
                      .WithOne(oi => oi.Order)
                      .HasForeignKey(oi => oi.OrderId);
            });

            // إعدادات OrderItem
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(oi => oi.Id);

                entity.Property(oi => oi.Price)
                      .HasColumnType("decimal(18, 2)");

                entity.HasOne(oi => oi.Order)
                      .WithMany(o => o.OrderItems)
                      .HasForeignKey(oi => oi.OrderId);

                entity.HasOne(oi => oi.Product)
                      .WithMany(p => p.OrderItems)
                      .HasForeignKey(oi => oi.ProductId);
            });
        }

        public DbSet<User> users { get; set; }
        public DbSet<Cart>Carts { get; set; }
        public DbSet<Order> orders { get; set; }
        public DbSet<Product> products { get;set; }
        public DbSet<OrderItem> orderItems { get; set; }
        public DbSet<CartItem> cartItems { get; set; }

    }
}
