using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VoucherCreation.DAL.Entities;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace VoucherCreation.DAL
{
    public partial class PiVMSContext : DbContext
    {
        public PiVMSContext()
        {
        }

        public PiVMSContext(DbContextOptions<PiVMSContext> options)
            : base(options)
        {
        }


        public virtual DbSet<PiJob> PiJobs { get; set; }

        public virtual DbSet<PiRedemptionHistory> PiRedemptionHistories { get; set; }

        public virtual DbSet<PiUser> PiUsers { get; set; }

        public virtual DbSet<PiVoucher> PiVouchers { get; set; }

        public virtual DbSet<PiVoucherAssignment> PiVoucherAssignments { get; set; }

        public virtual DbSet<PiVoucherLimit> PiVoucherLimits { get; set; }

        public virtual DbSet<PiVoucherLimitDetail> PiVoucherLimitDetails { get; set; }

        public virtual DbSet<PiVoucherRedemption> PiVoucherRedemptions { get; set; }

        public virtual DbSet<PiLogs> PiLogs { get; set; }

        public virtual DbSet<PiCountry> PiCountrys { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PiJob>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PI_JOBS__3214EC078D39682D");

                entity.ToTable("PI_JOBS");

                entity.Property(e => e.Bgtype).HasColumnName("BGType");
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getutcdate())")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<PiRedemptionHistory>(entity =>
            {
                entity.HasKey(e => e.RedemId).HasName("PK__PI_REDEM__03A6BB9BF3DD1059");

                entity.ToTable("PI_REDEMPTION_HISTORY");

                entity.Property(e => e.RedemId).ValueGeneratedNever();
                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getutcdate())")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<PiUser>(entity =>
            {
                entity
                    .HasNoKey()
                    .ToTable("PI_USERS");

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getutcdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Email).HasMaxLength(200);
            });

            modelBuilder.Entity<PiVoucher>(entity =>
            {
                entity.HasKey(e => e.VoucherId).HasName("PK__PI_VOUCH__3AEE79212CBCBFEB");

                entity.ToTable("PI_VOUCHERS");

                entity.HasIndex(e => e.Code, "UQ__PI_VOUCH__A25C5AA74EA3FB59").IsUnique();

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.Code).HasMaxLength(200);
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getutcdate())")
                    .HasColumnType("datetime");

                entity.Property(e => e.ExpiryDate).HasColumnType("datetime");
            });

            modelBuilder.Entity<PiVoucherAssignment>(entity =>
            {
                entity.HasKey(e => e.VoucherId).HasName("PK__PI_VOUCH__3AEE792107CAF7A1");

                entity.ToTable("PI_VOUCHER_ASSIGNMENT");

                entity.Property(e => e.VoucherId).ValueGeneratedNever();
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getutcdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.Email).HasMaxLength(200);
                entity.Property(e => e.PhoneNumber).HasMaxLength(20);
                entity.Property(e => e.Pin).HasColumnName("PIN");
            });

            modelBuilder.Entity<PiVoucherLimit>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PI_VOUCH__3214EC07844F9F02");

                entity.ToTable("PI_VOUCHER_LIMITS");

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getutcdate())")
                    .HasColumnType("datetime");
                entity.Property(e => e.UpdatedDate)
                    .HasDefaultValueSql("(getutcdate())")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<PiVoucherLimitDetail>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK__PI_VOUCH__3214EC0773711897");

                entity.ToTable("PI_VOUCHER_LIMIT_DETAILS");

                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getutcdate())")
                    .HasColumnType("datetime");
            });

            modelBuilder.Entity<PiVoucherRedemption>(entity =>
            {
                entity.HasKey(e => e.RedemId).HasName("PK__PI_VOUCH__03A6BB9BB6A2F008");

                entity.ToTable("PI_VOUCHER_REDEMPTION");

                entity.Property(e => e.Amount).HasColumnType("decimal(18, 2)");
                entity.Property(e => e.CreatedDate)
                    .HasDefaultValueSql("(getutcdate())")
                    .HasColumnType("datetime");

            });


            OnModelCreatingCustom(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                if (IsConnectionString)
                {
                    optionsBuilder.UseSqlServer(ConnectionString);
                }
                else
                {
                    var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                    // define the database to use
                    optionsBuilder.UseSqlServer(config.GetConnectionString("dbConnection"));
                }
            }
        }
    }
}
