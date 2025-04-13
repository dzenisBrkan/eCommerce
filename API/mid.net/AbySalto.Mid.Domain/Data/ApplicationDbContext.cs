using AbySalto.Mid.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AbySalto.Mid.Domain.Data;

public class ApplicationDbContext : IdentityDbContext<User, Role, int, IdentityUserClaim<int>, UserRole, IdentityUserLogin<int>, IdentityRoleClaim<int>, IdentityUserToken<int>>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Bucket> Buckets { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductImage> ProductImages { get; set; }
    public DbSet<ProductTag> ProductTags { get; set; }
    public DbSet<Review> Review { get; set; }
    public DbSet<BucketItem> BucketItems { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<UserRole> UserRole { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>()
            .OwnsOne(p => p.Dimensions);

        modelBuilder.Entity<Product>()
            .OwnsOne(p => p.Meta);
  

        modelBuilder.Entity<User>()
            .HasMany(e => e.UserRoles)
            .WithOne(e => e.User)
            .HasForeignKey(e => e.UserId)
            .IsRequired();

        modelBuilder.Entity<Role>()
            .HasMany(e => e.UserRoles)
            .WithOne(e => e.Role)
            .HasForeignKey(e => e.RoleId)
            .IsRequired();

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Title)
            .HasDatabaseName("Idx_Title");

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Price)
            .HasDatabaseName("Idx_Price");

        modelBuilder.Entity<Product>()
            .HasIndex(p => p.Rating)
            .HasDatabaseName("Idx_Rating");
    }
}