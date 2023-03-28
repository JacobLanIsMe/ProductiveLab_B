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
        public virtual DbSet<MediumType> MediumTypes { get; set; } = null!;
        public virtual DbSet<ObservationNote> ObservationNotes { get; set; } = null!;
        public virtual DbSet<OvumPickup> OvumPickups { get; set; } = null!;
        public virtual DbSet<OvumPickupDetail> OvumPickupDetails { get; set; } = null!;
        public virtual DbSet<OvumPickupDetailStatus> OvumPickupDetailStatuses { get; set; } = null!;
        public virtual DbSet<SpermFreeze> SpermFreezes { get; set; } = null!;
        public virtual DbSet<SpermFreezeOperationMethod> SpermFreezeOperationMethods { get; set; } = null!;
        public virtual DbSet<SpermRetrievalMethod> SpermRetrievalMethods { get; set; } = null!;
        public virtual DbSet<SpermScore> SpermScores { get; set; } = null!;
        public virtual DbSet<SpermScoreTimePoint> SpermScoreTimePoints { get; set; } = null!;
        public virtual DbSet<StorageCaneBox> StorageCaneBoxes { get; set; } = null!;
        public virtual DbSet<StorageShelf> StorageShelves { get; set; } = null!;
        public virtual DbSet<StorageTank> StorageTanks { get; set; } = null!;
        public virtual DbSet<StorageTankType> StorageTankTypes { get; set; } = null!;
        public virtual DbSet<StorageUnit> StorageUnits { get; set; } = null!;
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

                entity.HasOne(d => d.SpermRetrievalMethod)
                    .WithMany(p => p.CourseOfTreatments)
                    .HasForeignKey(d => d.SpermRetrievalMethodId)
                    .HasConstraintName("FK_CourseOfTreatment_SpermRetrievalMethod");

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

                entity.HasOne(d => d.MediumType)
                    .WithMany(p => p.MediumInUses)
                    .HasForeignKey(d => d.MediumTypeId)
                    .HasConstraintName("FK_MediumInUse_MediumType");
            });

            modelBuilder.Entity<MediumType>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("MediumType");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
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

                entity.Property(e => e.FreezeTime).HasColumnType("datetime");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CourseOfTreatment)
                    .WithMany(p => p.SpermFreezes)
                    .HasForeignKey(d => d.CourseOfTreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreeze_CourseOfTreatment");

                entity.HasOne(d => d.EmbryologistNavigation)
                    .WithMany(p => p.SpermFreezes)
                    .HasForeignKey(d => d.Embryologist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreeze_Employee");

                entity.HasOne(d => d.FreezeMediumInUse)
                    .WithMany(p => p.SpermFreezeFreezeMediumInUses)
                    .HasForeignKey(d => d.FreezeMediumInUseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreeze_MediumInUse2");

                entity.HasOne(d => d.MediumInUse)
                    .WithMany(p => p.SpermFreezeMediumInUses)
                    .HasForeignKey(d => d.MediumInUseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreeze_MediumInUse1");

                entity.HasOne(d => d.SpermFreezeOperationMethod)
                    .WithMany(p => p.SpermFreezes)
                    .HasForeignKey(d => d.SpermFreezeOperationMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreeze_SpermFreezeOperationMethod");

                entity.HasOne(d => d.StorageUnit)
                    .WithMany(p => p.SpermFreezes)
                    .HasForeignKey(d => d.StorageUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreeze_StorageUnit");
            });

            modelBuilder.Entity<SpermFreezeOperationMethod>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("SpermFreezeOperationMethod");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SpermRetrievalMethod>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("SpermRetrievalMethod");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SpermScore>(entity =>
            {
                entity.ToTable("SpermScore");

                entity.Property(e => e.SpermScoreId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActivityA).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ActivityB).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ActivityC).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.ActivityD).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Concentration).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.Morphology).HasColumnType("decimal(18, 0)");

                entity.Property(e => e.RecordTime).HasColumnType("datetime");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.Property(e => e.Volume).HasColumnType("decimal(18, 0)");

                entity.HasOne(d => d.CourseOfTreatment)
                    .WithMany(p => p.SpermScores)
                    .HasForeignKey(d => d.CourseOfTreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermScore_CourseOfTreatment");

                entity.HasOne(d => d.EmbryologistNavigation)
                    .WithMany(p => p.SpermScores)
                    .HasForeignKey(d => d.Embryologist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermScore_Employee");

                entity.HasOne(d => d.SpermScoreTimePoint)
                    .WithMany(p => p.SpermScores)
                    .HasForeignKey(d => d.SpermScoreTimePointId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermScore_SpermScoreTimePoint");
            });

            modelBuilder.Entity<SpermScoreTimePoint>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("SpermScoreTimePoint");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<StorageCaneBox>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("StorageCaneBox");

                entity.HasOne(d => d.StorageShelf)
                    .WithMany(p => p.StorageCaneBoxes)
                    .HasForeignKey(d => d.StorageShelfId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageCaneBox_StorageShelf");
            });

            modelBuilder.Entity<StorageShelf>(entity =>
            {
                entity.HasKey(e => e.SqlId)
                    .HasName("PK_StorageStrip");

                entity.ToTable("StorageShelf");

                entity.HasOne(d => d.StorageTank)
                    .WithMany(p => p.StorageShelves)
                    .HasForeignKey(d => d.StorageTankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageStrip_StorageTank");
            });

            modelBuilder.Entity<StorageTank>(entity =>
            {
                entity.HasKey(e => e.SqlId)
                    .HasName("PK_Storage");

                entity.ToTable("StorageTank");

                entity.HasOne(d => d.StorageTankType)
                    .WithMany(p => p.StorageTanks)
                    .HasForeignKey(d => d.StorageTankTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageTank_StorageTankType");
            });

            modelBuilder.Entity<StorageTankType>(entity =>
            {
                entity.HasKey(e => e.SqlId)
                    .HasName("PK_StorageType");

                entity.ToTable("StorageTankType");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<StorageUnit>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("StorageUnit");

                entity.HasOne(d => d.StorageCaneBox)
                    .WithMany(p => p.StorageUnits)
                    .HasForeignKey(d => d.StorageCaneBoxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageUnit_StorageCaneBox");
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
