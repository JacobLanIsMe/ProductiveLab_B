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
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Function> Functions { get; set; } = null!;
        public virtual DbSet<FunctionType> FunctionTypes { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<IdentityServer> IdentityServers { get; set; } = null!;
        public virtual DbSet<Incubator> Incubators { get; set; } = null!;
        public virtual DbSet<JobTitle> JobTitles { get; set; } = null!;
        public virtual DbSet<Medium> Media { get; set; } = null!;
        public virtual DbSet<OvumPickup> OvumPickups { get; set; } = null!;
        public virtual DbSet<OvumPickupIncubator> OvumPickupIncubators { get; set; } = null!;
        public virtual DbSet<OvumPickupMedium> OvumPickupMedia { get; set; } = null!;
        public virtual DbSet<SubFunction> SubFunctions { get; set; } = null!;
        public virtual DbSet<Treatment> Treatments { get; set; } = null!;
        public virtual DbSet<TreatmentStatus> TreatmentStatuses { get; set; } = null!;

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
                entity.ToTable("CourseOfTreatment");

                entity.Property(e => e.CourseOfTreatmentId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.Property(e => e.SurgicalTime).HasColumnType("datetime");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.CourseOfTreatments)
                    .HasForeignKey(d => d.CustomerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseOfTreatment_Customer");

                entity.HasOne(d => d.DoctorNavigation)
                    .WithMany(p => p.CourseOfTreatments)
                    .HasForeignKey(d => d.Doctor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseOfTreatment_Employee");

                entity.HasOne(d => d.Treatment)
                    .WithMany(p => p.CourseOfTreatments)
                    .HasForeignKey(d => d.TreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseOfTreatment_Treatment");

                entity.HasOne(d => d.TreatmentStatus)
                    .WithMany(p => p.CourseOfTreatments)
                    .HasForeignKey(d => d.TreatmentStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_CourseOfTreatment_TreatmentStatus");
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
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Customer_Customer");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.IdentityServer)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.IdentityServerId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_IdentityServer");

                entity.HasOne(d => d.JobTitle)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.JobTitleId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Staff_Identity");
            });

            modelBuilder.Entity<Function>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("Function");

                entity.Property(e => e.SqlId).ValueGeneratedNever();

                entity.HasOne(d => d.FunctionType)
                    .WithMany(p => p.Functions)
                    .HasForeignKey(d => d.FunctionTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Function_FunctionType");
            });

            modelBuilder.Entity<FunctionType>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("FunctionType");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
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

            modelBuilder.Entity<Incubator>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("Incubator");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<JobTitle>(entity =>
            {
                entity.HasKey(e => e.SqlId)
                    .HasName("PK_Identity");

                entity.ToTable("JobTitle");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Medium>(entity =>
            {
                entity.ToTable("Medium");

                entity.Property(e => e.MediumId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.OpenDate).HasColumnType("date");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<OvumPickup>(entity =>
            {
                entity.ToTable("OvumPickup");

                entity.Property(e => e.OvumPickupId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.CocGrade1).HasColumnName("COC_Grade1");

                entity.Property(e => e.CocGrade2).HasColumnName("COC_Grade2");

                entity.Property(e => e.CocGrade3).HasColumnName("COC_Grade3");

                entity.Property(e => e.CocGrade4).HasColumnName("COC_Grade4");

                entity.Property(e => e.CocGrade5).HasColumnName("COC_Grade5");

                entity.Property(e => e.EndTime).HasColumnType("datetime");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.Property(e => e.StartTime).HasColumnType("datetime");

                entity.Property(e => e.TriggerTime).HasColumnType("datetime");

                entity.HasOne(d => d.CourseOfTreatment)
                    .WithMany(p => p.OvumPickups)
                    .HasForeignKey(d => d.CourseOfTreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumPickup_CourseOfTreatment");

                entity.HasOne(d => d.EmbryologistNavigation)
                    .WithMany(p => p.OvumPickups)
                    .HasForeignKey(d => d.Embryologist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumPickup_Employee");
            });

            modelBuilder.Entity<OvumPickupIncubator>(entity =>
            {
                entity.ToTable("OvumPickupIncubator");

                entity.Property(e => e.OvumPickupIncubatorId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.OvumPickup)
                    .WithMany(p => p.OvumPickupIncubators)
                    .HasForeignKey(d => d.OvumPickupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumPickupIncubator_OvumPickup");
            });

            modelBuilder.Entity<OvumPickupMedium>(entity =>
            {
                entity.ToTable("OvumPickupMedium");

                entity.Property(e => e.OvumPickupMediumId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.Medium)
                    .WithMany(p => p.OvumPickupMedia)
                    .HasForeignKey(d => d.MediumId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumPickupMedium_Medium");

                entity.HasOne(d => d.OvumPickup)
                    .WithMany(p => p.OvumPickupMedia)
                    .HasForeignKey(d => d.OvumPickupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumPickupMedium_OvumPickup");
            });

            modelBuilder.Entity<SubFunction>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("SubFunction");

                entity.Property(e => e.SqlId).ValueGeneratedNever();

                entity.HasOne(d => d.Function)
                    .WithMany(p => p.SubFunctions)
                    .HasForeignKey(d => d.FunctionId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SubFunction_Function");
            });

            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("Treatment");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<TreatmentStatus>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("TreatmentStatus");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
