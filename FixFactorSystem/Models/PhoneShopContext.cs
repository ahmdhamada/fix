using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace FixFactorSystem.Models
{
    public partial class PhoneShopContext : DbContext
    {
        public PhoneShopContext()
        {
        }

        public PhoneShopContext(DbContextOptions<PhoneShopContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Adapters> Adapters { get; set; }
        public virtual DbSet<AspNetRoleClaims> AspNetRoleClaims { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUserTokens> AspNetUserTokens { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Covers> Covers { get; set; }
        public virtual DbSet<Maintenence> Maintenence { get; set; }
        public virtual DbSet<NewPhone> NewPhone { get; set; }
        public virtual DbSet<NewPhoneDetails> NewPhoneDetails { get; set; }
        public virtual DbSet<Offers> Offers { get; set; }
        public virtual DbSet<Products> Products { get; set; }
        public virtual DbSet<UsedPhone> UsedPhone { get; set; }
        public virtual DbSet<UsedPhoneDetails> UsedPhoneDetails { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=Hamada\\SQLEXPRESS; Database=PhoneShop; Trusted_Connection=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adapters>(entity =>
            {
                entity.HasKey(e => e.AdapterId);

                entity.Property(e => e.AdapterColor).HasMaxLength(50);

                entity.Property(e => e.AdapterName).IsRequired();
            });

            modelBuilder.Entity<AspNetRoleClaims>(entity =>
            {
                entity.HasIndex(e => e.RoleId);

                entity.Property(e => e.RoleId).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetRoleClaims)
                    .HasForeignKey(d => d.RoleId);
            });

            modelBuilder.Entity<AspNetRoles>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName)
                    .HasName("RoleNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedName] IS NOT NULL)");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);
            });

            modelBuilder.Entity<AspNetUserClaims>(entity =>
            {
                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserClaims)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserLogins>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.HasIndex(e => e.UserId);

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.ProviderKey).HasMaxLength(128);

                entity.Property(e => e.UserId).IsRequired();

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserLogins)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserRoles>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId , e.Note });

                entity.HasIndex(e => e.RoleId);

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.RoleId);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserRoles)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUserTokens>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

                entity.Property(e => e.LoginProvider).HasMaxLength(128);

                entity.Property(e => e.Name).HasMaxLength(128);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.AspNetUserTokens)
                    .HasForeignKey(d => d.UserId);
            });

            modelBuilder.Entity<AspNetUsers>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName)
                    .HasName("UserNameIndex")
                    .IsUnique()
                    .HasFilter("([NormalizedUserName] IS NOT NULL)");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
            });

            modelBuilder.Entity<Covers>(entity =>
            {
                entity.HasKey(e => e.CoverId);

                entity.Property(e => e.CoverColor).HasMaxLength(50);
            });

            modelBuilder.Entity<Maintenence>(entity =>
            {
                entity.HasKey(e => e.DamageId);

                entity.Property(e => e.DamMobName).IsRequired();

                entity.Property(e => e.DamageDescription).IsRequired();

                entity.Property(e => e.PhoneOwner).IsRequired();
            });

            modelBuilder.Entity<NewPhone>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Image).HasMaxLength(50);

                entity.Property(e => e.MobName)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Ram)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Storage)
                    .IsRequired()
                    .HasColumnName("storage")
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<NewPhoneDetails>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Battary).HasMaxLength(50);

                entity.Property(e => e.Color).HasMaxLength(50);

                entity.Property(e => e.MobName).HasMaxLength(50);

                entity.Property(e => e.Ram).HasMaxLength(50);

                entity.Property(e => e.Storage).HasMaxLength(50);
            });

            modelBuilder.Entity<Offers>(entity =>
            {
                entity.HasKey(e => e.OfferId);

                entity.Property(e => e.Details).IsRequired();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Products>(entity =>
            {
                entity.HasKey(e => e.ProductId);

                entity.Property(e => e.ProductId)
                    .HasColumnName("productId")
                    .ValueGeneratedOnAdd();

                entity.Property(e => e.Details).HasColumnName("details");

                entity.Property(e => e.Img).HasColumnName("img");

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .ValueGeneratedNever();

                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasColumnName("productName");
            });

            modelBuilder.Entity<UsedPhone>(entity =>
            {
                entity.HasKey(e => e.Idu);

                entity.Property(e => e.Idu).HasColumnName("idu");

                entity.Property(e => e.Imgu).HasColumnName("imgu");

                entity.Property(e => e.MobNameu).IsRequired();

                entity.Property(e => e.Ramu)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Storageu)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<UsedPhoneDetails>(entity =>
            {
                entity.HasKey(e => e.Idu);

                entity.Property(e => e.Idu).HasColumnName("idu");

                entity.Property(e => e.AddDetails).IsRequired();

                entity.Property(e => e.Battary).HasMaxLength(50);

                entity.Property(e => e.Camerau).IsRequired();

                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Imgu).IsRequired();

                entity.Property(e => e.MobNameu).IsRequired();

                entity.Property(e => e.Ramu)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Storageu)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
