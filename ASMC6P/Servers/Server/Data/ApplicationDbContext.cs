using ASMC6P.Shared.Entities;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

using System.Diagnostics.CodeAnalysis;

namespace ASMC6P.Server.Data;

public class ApplicationDbContext : IdentityDbContext<UserEntity, RoleEntity, Guid>
{
    private readonly UserManager<UserEntity> _userManager;

    protected ApplicationDbContext(UserManager<UserEntity> userManager)
    {
        _userManager = userManager;
    }
    public ApplicationDbContext([NotNull] DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.NoTracking;
        ChangeTracker.LazyLoadingEnabled = false;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
        optionsBuilder.LogTo(Console.WriteLine);
    }
    public virtual DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.Entity<RefreshTokenEntity>(entity =>
        {
            entity.ToTable("Refreshtokens");
            entity.HasKey("Id");
        });

        #region Data

        builder.Entity<RoleEntity>().HasData(new RoleEntity()
        {
            Id = Guid.Parse("ab251560-a455-40fd-adfd-54f9e150f874"),
            Name = "Administrator",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            NormalizedName = "Administrator"
        }, new RoleEntity()
        {
            Id = Guid.Parse("8d4b836e-d9fa-4fa9-88c0-9a875d2b7d5c"),
            Name = "Employee",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            NormalizedName = "Employee"
        }, new RoleEntity()
        {
            Id = Guid.Parse("f91ec0e5-d768-42e2-8926-de7d3162430f"),
            Name = "Customer",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            NormalizedName = "Customer"
        });



        builder.Entity<UserEntity>().HasData(new UserEntity()
        {
            Id = Guid.Parse("fb1eab16-920e-4480-b3ee-01f6e9c15ab5"),
            UserName = "vankieu0409@gmail.com",
            NormalizedUserName = "vankieu0409@gmail.com",
            SecurityStamp = Guid.NewGuid().ToString(),
            Email = "vankieu0409@gmail.com",
            PasswordHash = "AQAAAAEAACcQAAAAEMfrd51YGMSLzKs7NWUztJV/CKxRqpABxKVBI7+iwpeD82bZA8aBCnr7kKusapiDQw==",
            EmailConfirmed = true,
            NormalizedEmail = "vankieu0409@gmail.com",
            PhoneNumber = "",
            PhoneNumberConfirmed = false,
            LockoutEnabled = false,
            LockoutEnd = DateTimeOffset.MinValue,
            AccessFailedCount = 0,
            IsDeleted = false,
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            TwoFactorEnabled = false,
            Image = "https://media-cdn-v2.laodong.vn/Storage/NewsPortal/2022/8/18/1082204/Leesuk.jpg",
            Decriptions = "",
            DisplayName = "Bậu"
        }, new UserEntity()
        {
            Id = Guid.Parse("6aa93c41-f21f-44e3-8f46-7d76b03574c5"),
            UserName = "kieunvph14806@fpt.edu.vn",
            NormalizedUserName = "kieunvph14806@fpt.edu.vn",
            SecurityStamp = Guid.NewGuid().ToString(),
            Email = "kieunvph14806@fpt.edu.vn",
            PasswordHash = "AQAAAAEAACcQAAAAEMfrd51YGMSLzKs7NWUztJV/CKxRqpABxKVBI7+iwpeD82bZA8aBCnr7kKusapiDQw==",
            EmailConfirmed = true,
            NormalizedEmail = "kieunvph14806@fpt.edu.vn",
            PhoneNumber = "",
            PhoneNumberConfirmed = false,
            LockoutEnabled = false,
            LockoutEnd = DateTimeOffset.MinValue,
            AccessFailedCount = 0,
            IsDeleted = false,
            Image = "https://media-cdn-v2.laodong.vn/Storage/NewsPortal/2022/8/18/1082204/Leesuk.jpg",
            Decriptions = " Chị rất nóng tính",
            DisplayName = "Chị Nhà Cục Súc",
            ConcurrencyStamp = Guid.NewGuid().ToString(),
            TwoFactorEnabled = false
        });
        builder.Entity<IdentityUserRole<Guid>>().HasData(new IdentityUserRole<Guid>()
        {
            UserId = Guid.Parse("fb1eab16-920e-4480-b3ee-01f6e9c15ab5"),
            RoleId = Guid.Parse("ab251560-a455-40fd-adfd-54f9e150f874")
        }, new IdentityUserRole<Guid>()
        {
            UserId = Guid.Parse("fb1eab16-920e-4480-b3ee-01f6e9c15ab5"),
            RoleId = Guid.Parse("8d4b836e-d9fa-4fa9-88c0-9a875d2b7d5c")
        }, new IdentityUserRole<Guid>()
        {
            UserId = Guid.Parse("6aa93c41-f21f-44e3-8f46-7d76b03574c5"),
            RoleId = Guid.Parse("f91ec0e5-d768-42e2-8926-de7d3162430f"),
        });

        #endregion
    }
}