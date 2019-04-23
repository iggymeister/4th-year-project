using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace web.Models
{
    public partial class project_dbContext : DbContext
    {
        public project_dbContext()
        {
        }

        public project_dbContext(DbContextOptions<project_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Drivers> Drivers { get; set; }
        public virtual DbSet<JobCreators> JobCreators { get; set; }
        public virtual DbSet<JobOffers> JobOffers { get; set; }
        public virtual DbSet<Jobs> Jobs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=tcp:yearproject.database.windows.net,1433;Initial Catalog=project_db;Persist Security Info=False;User ID=X00094287;Password=Wasup123;MultipleActiveResultSets=True;Trusted_Connection=False;Encrypt=True;Integrated Security=False;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Drivers>(entity =>
            {
                entity.HasKey(e => e.DriverId);

                entity.HasIndex(e => e.DriverId)
                    .HasName("UQ__Drivers__F1B1CD0505B6F322")
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__Drivers__A9D105346583B131")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNo)
                    .HasName("UQ__Drivers__F3EE33E28251A288")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__Drivers__536C85E406960911")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.FrequentDrivingLocations)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobCreators>(entity =>
            {
                entity.HasKey(e => e.CreatorId);

                entity.HasIndex(e => e.CreatorId)
                    .HasName("UQ__JobCreat__6C75483010EEF0F9")
                    .IsUnique();

                entity.HasIndex(e => e.Email)
                    .HasName("UQ__JobCreat__A9D10534D87E415A")
                    .IsUnique();

                entity.HasIndex(e => e.PhoneNo)
                    .HasName("UQ__JobCreat__F3EE33E21CAFD7D6")
                    .IsUnique();

                entity.HasIndex(e => e.Username)
                    .HasName("UQ__JobCreat__536C85E47F6FAC76")
                    .IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .IsRequired()
                    .HasMaxLength(15)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<JobOffers>(entity =>
            {
                entity.HasKey(e => e.JobOfferId);

                entity.HasIndex(e => e.JobOfferId)
                    .HasName("UQ__JobOffer__5B32794C61566D07")
                    .IsUnique();

                entity.Property(e => e.DriverId)
                    .IsRequired();

                entity.Property(e => e.JobId).HasColumnName("JobID");

                entity.Property(e => e.ApproxDeliveryDate)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.ProposedPickupDate)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.JobOfferConfirmed);

                entity.HasOne(d => d.DriverUsernameNavigation)
                    .WithMany(p => p.JobOffers)
                    .HasPrincipalKey(p => p.DriverId)
                    .HasForeignKey(d => d.DriverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JobOffers__Drive__5CD6CB2B");

                entity.HasOne(d => d.Job)
                    .WithMany(p => p.JobOffers)
                    .HasForeignKey(d => d.JobId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__JobOffers__JobID__5BE2A6F2");
            });

            modelBuilder.Entity<Jobs>(entity =>
            {
                entity.HasKey(e => e.JobId);

                entity.HasIndex(e => e.CreatorId)
                    .HasName("UQ__Jobs__6C7548102DE6A699")
                    .IsUnique();

                entity.HasIndex(e => e.JobId)
                    .HasName("UQ__Jobs__056690C3D64FDD88")
                    .IsUnique();

                entity.Property(e => e.AdditionalInfo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.CreatorId).HasColumnName("CreatorID");

                entity.Property(e => e.DeliveryArea)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.DeliveryDate)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.DriverUsername)
                    .HasMaxLength(15)
                    .IsUnicode(false);

                entity.Property(e => e.PackageSizeInDimensions)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.PickUpArea)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.Creator)
                    .WithOne(p => p.Jobs)
                    .HasForeignKey<Jobs>(d => d.CreatorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__Jobs__CreatorID__571DF1D5");

                entity.HasOne(d => d.DriverUsernameNavigation)
                    .WithMany(p => p.Jobs)
                    .HasPrincipalKey(p => p.Username)
                    .HasForeignKey(d => d.DriverUsername)
                    .HasConstraintName("FK__Jobs__DriverUser__5812160E");
            });
        }
    }
}
