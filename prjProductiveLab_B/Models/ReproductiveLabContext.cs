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
        public virtual DbSet<FertilizationStatus> FertilizationStatuses { get; set; } = null!;
        public virtual DbSet<Function> Functions { get; set; } = null!;
        public virtual DbSet<FunctionType> FunctionTypes { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<IdentityServer> IdentityServers { get; set; } = null!;
        public virtual DbSet<Incubator> Incubators { get; set; } = null!;
        public virtual DbSet<JobTitle> JobTitles { get; set; } = null!;
        public virtual DbSet<MediumInUse> MediumInUses { get; set; } = null!;
        public virtual DbSet<ObservationNote> ObservationNotes { get; set; } = null!;
        public virtual DbSet<OvumPickup> OvumPickups { get; set; } = null!;
        public virtual DbSet<OvumPickupDetail> OvumPickupDetails { get; set; } = null!;
        public virtual DbSet<OvumPickupDetailStatus> OvumPickupDetailStatuses { get; set; } = null!;
        public virtual DbSet<SpermFreeze> SpermFreezes { get; set; } = null!;
        public virtual DbSet<SpermPickup> SpermPickups { get; set; } = null!;
        public virtual DbSet<SpermRetrievalMethod> SpermRetrievalMethods { get; set; } = null!;
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

            modelBuilder.Entity<FertilizationStatus>(entity =>
            {
                entity.HasKey(e => e.SqlId)
                    .HasName("PK_FertilizationStatus_1");

                entity.ToTable("FertilizationStatus");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
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

            modelBuilder.Entity<MediumInUse>(entity =>
            {
                entity.ToTable("MediumInUse");

                entity.Property(e => e.MediumInUseId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ExpirationDate).HasColumnType("date");

                entity.Property(e => e.OpenDate).HasColumnType("date");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<ObservationNote>(entity =>
            {
                entity.ToTable("ObservationNote");

                entity.Property(e => e.ObservationNoteId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.OvumPickupDetail)
                    .WithMany(p => p.ObservationNotes)
                    .HasForeignKey(d => d.OvumPickupDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationNote_OvumPickupDetail");
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

                entity.Property(e => e.UpdateTime).HasColumnType("datetime");

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

            modelBuilder.Entity<OvumPickupDetail>(entity =>
            {
                entity.ToTable("OvumPickupDetail");

                entity.Property(e => e.OvumPickupDetailId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.FertilizationStatus)
                    .WithMany(p => p.OvumPickupDetails)
                    .HasForeignKey(d => d.FertilizationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumPickupDetail_FertilizationStatus");

                entity.HasOne(d => d.Incubator)
                    .WithMany(p => p.OvumPickupDetails)
                    .HasForeignKey(d => d.IncubatorId)
                    .HasConstraintName("FK_OvumPickupDetail_Incubator");

                entity.HasOne(d => d.MediumInUse)
                    .WithMany(p => p.OvumPickupDetails)
                    .HasForeignKey(d => d.MediumInUseId)
                    .HasConstraintName("FK_OvumPickupDetail_MediumInUse");

                entity.HasOne(d => d.OvumPickupDetailStatus)
                    .WithMany(p => p.OvumPickupDetails)
                    .HasForeignKey(d => d.OvumPickupDetailStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumPickupDetail_OvumPickupDetailStatus");

                entity.HasOne(d => d.OvumPickup)
                    .WithMany(p => p.OvumPickupDetails)
                    .HasForeignKey(d => d.OvumPickupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumPickupDetail_OvumPickup");
            });

            modelBuilder.Entity<OvumPickupDetailStatus>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("OvumPickupDetailStatus");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SpermFreeze>(entity =>
            {
                entity.ToTable("SpermFreeze");

                entity.Property(e => e.SpermFreezeId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.SpermPickup)
                    .WithMany(p => p.SpermFreezes)
                    .HasForeignKey(d => d.SpermPickupId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreeze_SpermPickup");
            });

            modelBuilder.Entity<SpermPickup>(entity =>
            {
                entity.ToTable("SpermPickup");

                entity.Property(e => e.SpermPickupId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CourseOfTreatment)
                    .WithMany(p => p.SpermPickups)
                    .HasForeignKey(d => d.CourseOfTreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermPickup_CourseOfTreatment");

                entity.HasOne(d => d.SpermRetrievalMethod)
                    .WithMany(p => p.SpermPickups)
                    .HasForeignKey(d => d.SpermRetrievalMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermPickup_SpermRetrievalMethod");
            });

            modelBuilder.Entity<SpermRetrievalMethod>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("SpermRetrievalMethod");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
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
