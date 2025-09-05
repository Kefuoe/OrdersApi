using Microsoft.EntityFrameworkCore;
using Orders.Api.Models;

namespace Orders.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<User> Users => Set<User>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Users
        modelBuilder.Entity<User>(b =>
        {
            b.HasKey(u => u.UserId);
            b.Property(u => u.Name).HasMaxLength(200);
            b.Property(u => u.Surname).HasMaxLength(200);
            b.Property(u => u.Email).HasMaxLength(256);
        });

        //Orders
        modelBuilder.Entity<Order>(b =>
        {
            b.HasKey(o => o.Id);
            b.Property(o => o.OrderNumber).IsRequired();
            b.HasIndex(o => o.OrderNumber).IsUnique();
            b.Property(o => o.CreatedAt).HasColumnType("datetime2");
            b.Property(o => o.Status).HasDefaultValue(OrderStatus.Draft);

            b.HasOne(o => o.User)
            .WithMany(u => u.Orders)
            .HasForeignKey(o => o.UserId)
            .OnDelete(DeleteBehavior.Restrict);

            b.HasMany(o => o.Items)
            .WithOne(i => i.Order!)
            .HasForeignKey(i => i.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
        });

        //OrderItems
        modelBuilder.Entity<OrderItem>(b =>
        {
            b.HasKey(i => i.Id);
            b.Property(i => i.Quantity).IsRequired();
            b.Property(i => i.UnitPrice).HasColumnType("decimal(18,2)").IsRequired();
        });

        //Optional: DB SEQUENCE for safe concurrent OrderNumber incrementing
        modelBuilder.HasSequence<long>("OrderNumbers", schema: "dbo")
                    .StartsAt(1)
                    .IncrementsBy(1);

        modelBuilder.Entity<Order>()
            .Property(o => o.OrderNumber)
            .HasDefaultValueSql("NEXT VALUE FOR dbo.OrderNumbers");

    }


}