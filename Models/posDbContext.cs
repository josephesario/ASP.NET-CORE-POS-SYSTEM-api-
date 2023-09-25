using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace POS.Models;

public partial class posDbContext : DbContext
{
    public posDbContext()
    {
    }

    public posDbContext(DbContextOptions<posDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TAccount> TAccounts { get; set; }

    public virtual DbSet<TAudit> TAudits { get; set; }

    public virtual DbSet<TClient> TClients { get; set; }

    public virtual DbSet<TProductCategory> TProductCategories { get; set; }

    public virtual DbSet<TSale> TSales { get; set; }

    public virtual DbSet<TStock> TStocks { get; set; }

    public virtual DbSet<TStockStatus> TStockStatuses { get; set; }

    public virtual DbSet<TStore> TStores { get; set; }

    public virtual DbSet<TSupplier> TSuppliers { get; set; }

    public virtual DbSet<TSystemNotification> TSystemNotifications { get; set; }

    public virtual DbSet<TUserDetail> TUserDetails { get; set; }

    public virtual DbSet<TUserDetailsStatus> TUserDetailsStatuses { get; set; }

    public virtual DbSet<TUserRegistrationDetail> TUserRegistrationDetails { get; set; }

    public virtual DbSet<TUserType> TUserTypes { get; set; }

    public virtual DbSet<TUserTypeStatus> TUserTypeStatuses { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {

            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                .AddJsonFile("appsettings.json")
                .Build();

            string connectionString = configuration.GetConnectionString("posDbContext");

            optionsBuilder.UseSqlServer(connectionString);

        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TAccount>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PK__t_Accoun__F267253E17CBC4A2");

            entity.HasOne(d => d.Client).WithMany(p => p.TAccounts).HasConstraintName("fk_t_Client_and_t_Account");

            entity.HasOne(d => d.UserDetails).WithMany(p => p.TAccounts)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserDetails_and_t_Account");
        });

        modelBuilder.Entity<TAudit>(entity =>
        {
            entity.Property(e => e.AuditD).ValueGeneratedOnAdd();

            entity.HasOne(d => d.UserDetails).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserDetails_and_t_Audit");
        });

        modelBuilder.Entity<TClient>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PK__t_Client__81A2CB81508598AD");

            entity.Property(e => e.ClientId).ValueGeneratedNever();
        });

        modelBuilder.Entity<TProductCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__t_Produc__19093A2B30F1F49C");

            entity.HasOne(d => d.UserDetails).WithMany(p => p.TProductCategories)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserDetails_and_t_Product_Category");
        });

        modelBuilder.Entity<TSale>(entity =>
        {
            entity.HasOne(d => d.Product).WithOne()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_Stock_and_t_Sales");

            entity.HasOne(d => d.UserDetails).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserDetails_and_t_Sales");
        });

        modelBuilder.Entity<TStock>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__t_Stock__2D10D14A1BBE3D9D");

            entity.HasOne(d => d.Category).WithMany(p => p.TStocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_Product_Category_and_t_Stock");

            entity.HasOne(d => d.Client).WithMany(p => p.TStocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_Client_and_t_Stock");

            entity.HasOne(d => d.Supplier).WithMany(p => p.TStocks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_Supplier_and_t_Stock");
        });

        modelBuilder.Entity<TStockStatus>(entity =>
        {
            entity.HasKey(e => e.StockStatusId).HasName("PK__t_Stock___EC5FDC55B662756A");

            entity.HasOne(d => d.Product).WithMany(p => p.TStockStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_Stock_and_t_Stock_Status");

            entity.HasOne(d => d.UserDetails).WithMany(p => p.TStockStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserDetails_and_t_Stock_Status");
        });

        modelBuilder.Entity<TStore>(entity =>
        {
            entity.HasOne(d => d.Product).WithOne()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_Stock_and_t_Store");

            entity.HasOne(d => d.StockStatus).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_Stock_Status_and_t_Store");

            entity.HasOne(d => d.UserDetails).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserDetails_and_t_Store");
        });

        modelBuilder.Entity<TSupplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PK__t_Suppli__DB8E62CD10F930D6");

            entity.HasOne(d => d.UserDetails).WithMany(p => p.TSuppliers)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserDetails_and_t_Supplier");
        });

        modelBuilder.Entity<TSystemNotification>(entity =>
        {
            entity.Property(e => e.MessageId).ValueGeneratedOnAdd();

            entity.HasOne(d => d.UserDetails).WithMany()
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserDetails_and_t_SystemNotifications");
        });

        modelBuilder.Entity<TUserDetail>(entity =>
        {
            entity.HasKey(e => e.UserDetailsId).HasName("PK__t_UserDe__7286828D19EDC22B");

            entity.HasOne(d => d.Registration).WithMany(p => p.TUserDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserRegistrationDetails_and_t_UserDetails");

            entity.HasOne(d => d.UserDetailsStatus).WithMany(p => p.TUserDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserDetails_Status_and_t_UserDetails");

            entity.HasOne(d => d.UserType).WithMany(p => p.TUserDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_UserDetails_UserType");
        });

        modelBuilder.Entity<TUserDetailsStatus>(entity =>
        {
            entity.HasKey(e => e.UserDetailsStatusId).HasName("PK__t_UserDe__C33B7C3ABDCD9E6E");

            entity.HasOne(d => d.Client).WithMany(p => p.TUserDetailsStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserDetails_Status_and_t_Client");
        });

        modelBuilder.Entity<TUserRegistrationDetail>(entity =>
        {
            entity.HasKey(e => e.RegistrationId).HasName("PK__t_UserRe__A3DB1415AB8DE89F");

            entity.HasOne(d => d.Client).WithMany(p => p.TUserRegistrationDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_Client_and_t_UserRegistrationDetails");
        });

        modelBuilder.Entity<TUserType>(entity =>
        {
            entity.HasKey(e => e.UserTypeId).HasName("PK__t_UserTy__9D31025EADAE4FE8");

            entity.HasOne(d => d.Client).WithMany(p => p.TUserTypes)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserType_and_t_Client");
        });

        modelBuilder.Entity<TUserTypeStatus>(entity =>
        {
            entity.HasKey(e => e.UserTypeStatusId).HasName("PK__t_UserTy__F710B24DD70EAF16");

            entity.HasOne(d => d.Client).WithMany(p => p.TUserTypeStatuses)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_t_UserType_Status_and_t_UserType");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
