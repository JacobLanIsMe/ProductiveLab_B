using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace prjProductiveLab_B.Models
{
    public partial class ReproductiveLabContext : DbContext
    {
        public ReproductiveLabContext()
        {
        }

        public ReproductiveLabContext(DbContextOptions<ReproductiveLabContext> options)
            : base(options)
        {
        }

        public virtual DbSet<CourseOfTreatment> CourseOfTreatments { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<IdentityServer> IdentityServers { get; set; } = null!;
        public virtual DbSet<JobTitle> JobTitles { get; set; } = null!;
        public virtual DbSet<Treatment> Treatments { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=ReproductiveLab;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CourseOfTreatment>(entity =>
            {
                entity.HasKey(e => e.CourseOfTreatment1);

                entity.ToTable("CourseOfTreatment");

                entity.Property(e => e.CourseOfTreatment1)
                    .HasColumnName("CourseOfTreatment")
                    .HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CourseOfTreatments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseOfTreatment_Customer");

                entity.HasOne(d => d.DoctorNavigation)
                    .WithMany(p => p.CourseOfTreatmentDoctorNavigations)
                    .HasForeignKey(d => d.Doctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseOfTreatment_Staff");

                entity.HasOne(d => d.EmbryologistNavigation)
                    .WithMany(p => p.CourseOfTreatmentEmbryologistNavigations)
                    .HasForeignKey(d => d.Embryologist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseOfTreatment_Staff1");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.CourseOfTreatments)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseOfTreatment_Treatment");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.ToTable("Customer");

                entity.Property(e => e.CustomerId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.Birthday).HasColumnType("date");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Gender)
                    .WithMany(p => p.Customers)
                    .HasForeignKey(d => d.GenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Gender");

                entity.HasOne(d => d.SpouseNavigation)
                    .WithMany(p => p.InverseSpouseNavigation)
                    .HasForeignKey(d => d.Spouse)
                    .HasConstraintName("FK_Customer_Customer");
            });

            modelBuilder.Entity<Gender>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("Gender");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<IdentityServer>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("IdentityServer");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.HasKey(e => e.SqlId)
                    .HasName("PK_Identity");

                entity.ToTable("JobTitle");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("Treatment");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.ToTable("Staff");

                entity.Property(e => e.StaffId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdentityServer)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.IdentityServerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_IdentityServer");

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.staff)
                    .HasForeignKey(d => d.JobTitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_Identity");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
