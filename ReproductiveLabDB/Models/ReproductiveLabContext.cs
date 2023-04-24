using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace ReproductiveLabDB.Models
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

        public virtual DbSet<BlastocystScoreExpansion> BlastocystScoreExpansions { get; set; } = null!;
        public virtual DbSet<BlastocystScoreIce> BlastocystScoreIces { get; set; } = null!;
        public virtual DbSet<BlastocystScoreTe> BlastocystScoreTes { get; set; } = null!;
        public virtual DbSet<BlastomereScoreC> BlastomereScoreCs { get; set; } = null!;
        public virtual DbSet<BlastomereScoreF> BlastomereScoreFs { get; set; } = null!;
        public virtual DbSet<BlastomereScoreG> BlastomereScoreGs { get; set; } = null!;
        public virtual DbSet<CourseOfTreatment> CourseOfTreatments { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<EmbryoStatus> EmbryoStatuses { get; set; } = null!;
        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<FertilisationResult> FertilisationResults { get; set; } = null!;
        public virtual DbSet<FertilisationStatus> FertilisationStatuses { get; set; } = null!;
        public virtual DbSet<FrequentlyUsedMedium> FrequentlyUsedMedia { get; set; } = null!;
        public virtual DbSet<Function> Functions { get; set; } = null!;
        public virtual DbSet<FunctionType> FunctionTypes { get; set; } = null!;
        public virtual DbSet<Gender> Genders { get; set; } = null!;
        public virtual DbSet<GermCellOperation> GermCellOperations { get; set; } = null!;
        public virtual DbSet<GermCellSituation> GermCellSituations { get; set; } = null!;
        public virtual DbSet<GermCellSource> GermCellSources { get; set; } = null!;
        public virtual DbSet<IdentityServer> IdentityServers { get; set; } = null!;
        public virtual DbSet<Incubator> Incubators { get; set; } = null!;
        public virtual DbSet<JobTitle> JobTitles { get; set; } = null!;
        public virtual DbSet<MediumInUse> MediumInUses { get; set; } = null!;
        public virtual DbSet<MediumType> MediumTypes { get; set; } = null!;
        public virtual DbSet<ObservationNote> ObservationNotes { get; set; } = null!;
        public virtual DbSet<ObservationNoteEmbryoStatus> ObservationNoteEmbryoStatuses { get; set; } = null!;
        public virtual DbSet<ObservationNoteOperation> ObservationNoteOperations { get; set; } = null!;
        public virtual DbSet<ObservationNoteOvumAbnormality> ObservationNoteOvumAbnormalities { get; set; } = null!;
        public virtual DbSet<ObservationNotePhoto> ObservationNotePhotos { get; set; } = null!;
        public virtual DbSet<ObservationType> ObservationTypes { get; set; } = null!;
        public virtual DbSet<OperationType> OperationTypes { get; set; } = null!;
        public virtual DbSet<OvumAbnormality> OvumAbnormalities { get; set; } = null!;
        public virtual DbSet<OvumFreeze> OvumFreezes { get; set; } = null!;
        public virtual DbSet<OvumMaturation> OvumMaturations { get; set; } = null!;
        public virtual DbSet<OvumPickup> OvumPickups { get; set; } = null!;
        public virtual DbSet<OvumPickupDetail> OvumPickupDetails { get; set; } = null!;
        public virtual DbSet<OvumPickupDetailStatus> OvumPickupDetailStatuses { get; set; } = null!;
        public virtual DbSet<OvumThaw> OvumThaws { get; set; } = null!;
        public virtual DbSet<OvumThawFreezePair> OvumThawFreezePairs { get; set; } = null!;
        public virtual DbSet<SpermFreeze> SpermFreezes { get; set; } = null!;
        public virtual DbSet<SpermFreezeOperationMethod> SpermFreezeOperationMethods { get; set; } = null!;
        public virtual DbSet<SpermFreezeSituation> SpermFreezeSituations { get; set; } = null!;
        public virtual DbSet<SpermRetrievalMethod> SpermRetrievalMethods { get; set; } = null!;
        public virtual DbSet<SpermScore> SpermScores { get; set; } = null!;
        public virtual DbSet<SpermScoreTimePoint> SpermScoreTimePoints { get; set; } = null!;
        public virtual DbSet<SpermThaw> SpermThaws { get; set; } = null!;
        public virtual DbSet<StorageCanist> StorageCanists { get; set; } = null!;
        public virtual DbSet<StorageStripBox> StorageStripBoxes { get; set; } = null!;
        public virtual DbSet<StorageTank> StorageTanks { get; set; } = null!;
        public virtual DbSet<StorageTankType> StorageTankTypes { get; set; } = null!;
        public virtual DbSet<StorageUnit> StorageUnits { get; set; } = null!;
        public virtual DbSet<TopColor> TopColors { get; set; } = null!;
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
            modelBuilder.Entity<BlastocystScoreExpansion>(entity =>
            {
                entity.HasKey(e => e.SqlId)
                    .HasName("PK_BlastocystScore_Expantion");

                entity.ToTable("BlastocystScore_Expansion");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<BlastocystScoreIce>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("BlastocystScore_ICE");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<BlastocystScoreTe>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("BlastocystScore_TE");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<BlastomereScoreC>(entity =>
            {
                entity.HasKey(e => e.SlqId);

                entity.ToTable("BlastomereScore_C");

                entity.Property(e => e.SlqId).ValueGeneratedNever();
            });

            modelBuilder.Entity<BlastomereScoreF>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("BlastomereScore_F");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<BlastomereScoreG>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("BlastomereScore_G");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<CourseOfTreatment>(entity =>
            {
                entity.HasKey(e => e.CourseOfTreatmentId)
                    .IsClustered(false);

                entity.ToTable("CourseOfTreatment");

                entity.HasIndex(e => e.SqlId, "IX_CourseOfTreatment")
                    .IsClustered();

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

                entity.HasOne(d => d.OvumFromCourseOfTreatment)
                    .WithMany(p => p.InverseOvumFromCourseOfTreatment)
                    .HasForeignKey(d => d.OvumFromCourseOfTreatmentId)
                    .HasConstraintName("FK_CourseOfTreatment_CourseOfTreatment");

                entity.HasOne(d => d.SpermFromCourseOfTreatment)
                    .WithMany(p => p.InverseSpermFromCourseOfTreatment)
                    .HasForeignKey(d => d.SpermFromCourseOfTreatmentId)
                    .HasConstraintName("FK_CourseOfTreatment_CourseOfTreatment1");

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
                entity.HasKey(e => e.CustomerId)
                    .IsClustered(false);

                entity.ToTable("Customer");

                entity.HasIndex(e => e.SqlId, "IX_Customer")
                    .IsClustered();

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

            modelBuilder.Entity<EmbryoStatus>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("EmbryoStatus");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId)
                    .HasName("PK_Staff")
                    .IsClustered(false);

                entity.ToTable("Employee");

                entity.HasIndex(e => e.SqlId, "IX_Employee")
                    .IsClustered();

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

            modelBuilder.Entity<FertilisationResult>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("FertilisationResult");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<FertilisationStatus>(entity =>
            {
                entity.HasKey(e => e.SqlId)
                    .HasName("PK_FertilizationStatus_1");

                entity.ToTable("FertilisationStatus");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<FrequentlyUsedMedium>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("FrequentlyUsedMedium");

                entity.HasOne(d => d.MediumType)
                    .WithMany(p => p.FrequentlyUsedMedia)
                    .HasForeignKey(d => d.MediumTypeId)
                    .HasConstraintName("FK_FrequentlyUsedMedium_MediumType");
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

            modelBuilder.Entity<GermCellOperation>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("GermCellOperation");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<GermCellSituation>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("GermCellSituation");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<GermCellSource>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("GermCellSource");

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
                entity.HasKey(e => e.MediumInUseId)
                    .HasName("PK_Medium")
                    .IsClustered(false);

                entity.ToTable("MediumInUse");

                entity.HasIndex(e => e.SqlId, "IX_MediumInUse")
                    .IsClustered();

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
                entity.HasKey(e => e.ObservationNoteId)
                    .IsClustered(false);

                entity.ToTable("ObservationNote");

                entity.HasIndex(e => e.SqlId, "IX_ObservationNote")
                    .IsClustered();

                entity.Property(e => e.ObservationNoteId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.BlastocystScoreExpansionId).HasColumnName("BlastocystScore_Expansion_Id");

                entity.Property(e => e.BlastocystScoreIceId).HasColumnName("BlastocystScore_ICE_Id");

                entity.Property(e => e.BlastocystScoreTeId).HasColumnName("BlastocystScore_TE_Id");

                entity.Property(e => e.BlastomereScoreCId).HasColumnName("BlastomereScore_C_Id");

                entity.Property(e => e.BlastomereScoreFId).HasColumnName("BlastomereScore_F_Id");

                entity.Property(e => e.BlastomereScoreGId).HasColumnName("BlastomereScore_G_Id");

                entity.Property(e => e.Kidscore)
                    .HasColumnType("decimal(18, 1)")
                    .HasColumnName("KIDScore");

                entity.Property(e => e.ObservationTime).HasColumnType("datetime");

                entity.Property(e => e.Pgtanumber).HasColumnName("PGTANumber");

                entity.Property(e => e.Pgtaresult).HasColumnName("PGTAResult");

                entity.Property(e => e.Pgtmresult).HasColumnName("PGTMResult");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.BlastocystScoreExpansion)
                    .WithMany(p => p.ObservationNotes)
                    .HasForeignKey(d => d.BlastocystScoreExpansionId)
                    .HasConstraintName("FK_ObservationNote_BlastocystScore_Expantion");

                entity.HasOne(d => d.BlastocystScoreIce)
                    .WithMany(p => p.ObservationNotes)
                    .HasForeignKey(d => d.BlastocystScoreIceId)
                    .HasConstraintName("FK_ObservationNote_BlastocystScore_ICE");

                entity.HasOne(d => d.BlastocystScoreTe)
                    .WithMany(p => p.ObservationNotes)
                    .HasForeignKey(d => d.BlastocystScoreTeId)
                    .HasConstraintName("FK_ObservationNote_BlastocystScore_TE");

                entity.HasOne(d => d.BlastomereScoreC)
                    .WithMany(p => p.ObservationNotes)
                    .HasForeignKey(d => d.BlastomereScoreCId)
                    .HasConstraintName("FK_ObservationNote_BlastomereScore_C");

                entity.HasOne(d => d.BlastomereScoreF)
                    .WithMany(p => p.ObservationNotes)
                    .HasForeignKey(d => d.BlastomereScoreFId)
                    .HasConstraintName("FK_ObservationNote_BlastomereScore_F");

                entity.HasOne(d => d.BlastomereScoreG)
                    .WithMany(p => p.ObservationNotes)
                    .HasForeignKey(d => d.BlastomereScoreGId)
                    .HasConstraintName("FK_ObservationNote_BlastomereScore_G");

                entity.HasOne(d => d.EmbryologistNavigation)
                    .WithMany(p => p.ObservationNotes)
                    .HasForeignKey(d => d.Embryologist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationNote_Employee");

                entity.HasOne(d => d.FertilisationResult)
                    .WithMany(p => p.ObservationNotes)
                    .HasForeignKey(d => d.FertilisationResultId)
                    .HasConstraintName("FK_ObservationNote_FertilisationResult");

                entity.HasOne(d => d.ObservationType)
                    .WithMany(p => p.ObservationNotes)
                    .HasForeignKey(d => d.ObservationTypeId)
                    .HasConstraintName("FK_ObservationNote_ObservationType");

                entity.HasOne(d => d.OvumMaturation)
                    .WithMany(p => p.ObservationNotes)
                    .HasForeignKey(d => d.OvumMaturationId)
                    .HasConstraintName("FK_ObservationNote_OvumMaturation");

                entity.HasOne(d => d.OvumPickupDetail)
                    .WithMany(p => p.ObservationNotes)
                    .HasForeignKey(d => d.OvumPickupDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationNote_OvumPickupDetail");
            });

            modelBuilder.Entity<ObservationNoteEmbryoStatus>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("ObservationNoteEmbryoStatus");

                entity.HasIndex(e => e.SqlId, "IX_ObservationNoteEmbryoStatus")
                    .IsClustered();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.ForeignKey)
                    .WithMany(p => p.ObservationNoteEmbryoStatuses)
                    .HasForeignKey(d => d.ForeignKeyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationNoteEmbryoStatus_EmbryoStatus");

                entity.HasOne(d => d.ObservationNote)
                    .WithMany(p => p.ObservationNoteEmbryoStatuses)
                    .HasForeignKey(d => d.ObservationNoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationNoteEmbryoStatus_ObservationNote");
            });

            modelBuilder.Entity<ObservationNoteOperation>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("ObservationNoteOperation");

                entity.HasIndex(e => e.SqlId, "IX_ObservationNoteOperation")
                    .IsClustered();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.ForeignKey)
                    .WithMany(p => p.ObservationNoteOperations)
                    .HasForeignKey(d => d.ForeignKeyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationNoteOperation_OperationType");

                entity.HasOne(d => d.ObservationNote)
                    .WithMany(p => p.ObservationNoteOperations)
                    .HasForeignKey(d => d.ObservationNoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationNoteOperation_ObservationNote");
            });

            modelBuilder.Entity<ObservationNoteOvumAbnormality>(entity =>
            {
                entity.HasKey(e => e.Id)
                    .IsClustered(false);

                entity.ToTable("ObservationNoteOvumAbnormality");

                entity.HasIndex(e => e.SqlId, "IX_ObservationNoteOvumAbnormality")
                    .IsClustered();

                entity.Property(e => e.Id).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.ForeignKey)
                    .WithMany(p => p.ObservationNoteOvumAbnormalities)
                    .HasForeignKey(d => d.ForeignKeyId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationNoteOvumAbnormality_OvumAbnormality");

                entity.HasOne(d => d.ObservationNote)
                    .WithMany(p => p.ObservationNoteOvumAbnormalities)
                    .HasForeignKey(d => d.ObservationNoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationNoteOvumAbnormality_ObservationNote");
            });

            modelBuilder.Entity<ObservationNotePhoto>(entity =>
            {
                entity.HasKey(e => e.ObservationNotePhotoId)
                    .IsClustered(false);

                entity.ToTable("ObservationNotePhoto");

                entity.HasIndex(e => e.SqlId, "IX_ObservationNotePhoto")
                    .IsClustered();

                entity.Property(e => e.ObservationNotePhotoId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.ObservationNote)
                    .WithMany(p => p.ObservationNotePhotos)
                    .HasForeignKey(d => d.ObservationNoteId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_ObservationNotePhoto_ObservationNote");
            });

            modelBuilder.Entity<ObservationType>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("ObservationType");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<OperationType>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("OperationType");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<OvumAbnormality>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("OvumAbnormality");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<OvumFreeze>(entity =>
            {
                entity.HasKey(e => e.OvumFreezeId)
                    .IsClustered(false);

                entity.ToTable("OvumFreeze");

                entity.HasIndex(e => e.SqlId, "IX_OvumFreeze")
                    .IsClustered();

                entity.Property(e => e.OvumFreezeId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FreezeTime).HasColumnType("datetime");

                entity.Property(e => e.OvumMorphologyA).HasColumnName("OvumMorphology_A");

                entity.Property(e => e.OvumMorphologyB).HasColumnName("OvumMorphology_B");

                entity.Property(e => e.OvumMorphologyC).HasColumnName("OvumMorphology_C");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.EmbryologistNavigation)
                    .WithMany(p => p.OvumFreezes)
                    .HasForeignKey(d => d.Embryologist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumFreeze_Employee");

                entity.HasOne(d => d.MediumInUse)
                    .WithMany(p => p.OvumFreezes)
                    .HasForeignKey(d => d.MediumInUseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumFreeze_MediumInUse");

                entity.HasOne(d => d.StorageUnit)
                    .WithMany(p => p.OvumFreezes)
                    .HasForeignKey(d => d.StorageUnitId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumFreeze_StorageUnit");

                entity.HasOne(d => d.TopColor)
                    .WithMany(p => p.OvumFreezes)
                    .HasForeignKey(d => d.TopColorId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumFreeze_TopColor");
            });

            modelBuilder.Entity<OvumMaturation>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("OvumMaturation");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<OvumPickup>(entity =>
            {
                entity.HasKey(e => e.OvumPickupId)
                    .IsClustered(false);

                entity.ToTable("OvumPickup");

                entity.HasIndex(e => e.SqlId, "IX_OvumPickup")
                    .IsClustered();

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
                entity.HasKey(e => e.OvumPickupDetailId)
                    .IsClustered(false);

                entity.ToTable("OvumPickupDetail");

                entity.HasIndex(e => e.SqlId, "IX_OvumPickupDetail")
                    .IsClustered();

                entity.Property(e => e.OvumPickupDetailId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.FertilisationStatus)
                    .WithMany(p => p.OvumPickupDetails)
                    .HasForeignKey(d => d.FertilisationStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumPickupDetail_FertilisationStatus");

                entity.HasOne(d => d.Incubator)
                    .WithMany(p => p.OvumPickupDetails)
                    .HasForeignKey(d => d.IncubatorId)
                    .HasConstraintName("FK_OvumPickupDetail_Incubator");

                entity.HasOne(d => d.MediumInUse)
                    .WithMany(p => p.OvumPickupDetails)
                    .HasForeignKey(d => d.MediumInUseId)
                    .HasConstraintName("FK_OvumPickupDetail_MediumInUse");

                entity.HasOne(d => d.OvumFreeze)
                    .WithMany(p => p.OvumPickupDetails)
                    .HasForeignKey(d => d.OvumFreezeId)
                    .HasConstraintName("FK_OvumPickupDetail_OvumFreeze");

                entity.HasOne(d => d.OvumPickupDetailStatus)
                    .WithMany(p => p.OvumPickupDetails)
                    .HasForeignKey(d => d.OvumPickupDetailStatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumPickupDetail_OvumPickupDetailStatus");

                entity.HasOne(d => d.OvumPickup)
                    .WithMany(p => p.OvumPickupDetails)
                    .HasForeignKey(d => d.OvumPickupId)
                    .HasConstraintName("FK_OvumPickupDetail_OvumPickup");

                entity.HasOne(d => d.OvumThaw)
                    .WithMany(p => p.OvumPickupDetails)
                    .HasForeignKey(d => d.OvumThawId)
                    .HasConstraintName("FK_OvumPickupDetail_OvumThaw");
            });

            modelBuilder.Entity<OvumPickupDetailStatus>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("OvumPickupDetailStatus");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<OvumThaw>(entity =>
            {
                entity.HasKey(e => e.OvumThawId)
                    .IsClustered(false);

                entity.ToTable("OvumThaw");

                entity.HasIndex(e => e.SqlId, "IX_OvumThaw")
                    .IsClustered();

                entity.Property(e => e.OvumThawId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CourseOfTreatment)
                    .WithMany(p => p.OvumThaws)
                    .HasForeignKey(d => d.CourseOfTreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumThaw_CourseOfTreatment");
            });

            modelBuilder.Entity<OvumThawFreezePair>(entity =>
            {
                entity.HasKey(e => e.OvumThawFreezePairId)
                    .IsClustered(false);

                entity.ToTable("OvumThawFreezePair");

                entity.HasIndex(e => e.SqlId, "IX_OvumThawFreezePair")
                    .IsClustered();

                entity.Property(e => e.OvumThawFreezePairId).ValueGeneratedNever();

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.FreezeOvumPickupDetail)
                    .WithMany(p => p.OvumThawFreezePairFreezeOvumPickupDetails)
                    .HasForeignKey(d => d.FreezeOvumPickupDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumThawFreezePair_OvumPickupDetail");

                entity.HasOne(d => d.OvumThaw)
                    .WithMany(p => p.OvumThawFreezePairs)
                    .HasForeignKey(d => d.OvumThawId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumThawFreezePair_OvumThaw");

                entity.HasOne(d => d.ThawOvumPickupDetail)
                    .WithMany(p => p.OvumThawFreezePairThawOvumPickupDetails)
                    .HasForeignKey(d => d.ThawOvumPickupDetailId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_OvumThawFreezePair_OvumPickupDetail1");
            });

            modelBuilder.Entity<SpermFreeze>(entity =>
            {
                entity.HasKey(e => e.SpermFreezeId)
                    .IsClustered(false);

                entity.ToTable("SpermFreeze");

                entity.HasIndex(e => e.SqlId, "IX_SpermFreeze")
                    .IsClustered();

                entity.Property(e => e.SpermFreezeId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CourseOfTreatment)
                    .WithMany(p => p.SpermFreezes)
                    .HasForeignKey(d => d.CourseOfTreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreeze_CourseOfTreatment");

                entity.HasOne(d => d.SpermFreezeSituation)
                    .WithMany(p => p.SpermFreezes)
                    .HasForeignKey(d => d.SpermFreezeSituationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreeze_SpermFreezeSituation");

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

            modelBuilder.Entity<SpermFreezeSituation>(entity =>
            {
                entity.ToTable("SpermFreezeSituation");

                entity.Property(e => e.SpermFreezeSituationId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.FreezeTime).HasColumnType("datetime");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.EmbryologistNavigation)
                    .WithMany(p => p.SpermFreezeSituations)
                    .HasForeignKey(d => d.Embryologist)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreezeSituation_Employee");

                entity.HasOne(d => d.FreezeMediumInUse)
                    .WithMany(p => p.SpermFreezeSituationFreezeMediumInUses)
                    .HasForeignKey(d => d.FreezeMediumInUseId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreezeSituation_MediumInUse");

                entity.HasOne(d => d.MediumInUseId1Navigation)
                    .WithMany(p => p.SpermFreezeSituationMediumInUseId1Navigations)
                    .HasForeignKey(d => d.MediumInUseId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreezeSituation_MediumInUse1");

                entity.HasOne(d => d.MediumInUseId2Navigation)
                    .WithMany(p => p.SpermFreezeSituationMediumInUseId2Navigations)
                    .HasForeignKey(d => d.MediumInUseId2)
                    .HasConstraintName("FK_SpermFreezeSituation_MediumInUse2");

                entity.HasOne(d => d.MediumInUseId3Navigation)
                    .WithMany(p => p.SpermFreezeSituationMediumInUseId3Navigations)
                    .HasForeignKey(d => d.MediumInUseId3)
                    .HasConstraintName("FK_SpermFreezeSituation_MediumInUse3");

                entity.HasOne(d => d.SpermFreezeOperationMethod)
                    .WithMany(p => p.SpermFreezeSituations)
                    .HasForeignKey(d => d.SpermFreezeOperationMethodId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermFreezeSituation_SpermFreezeOperationMethod");
            });

            modelBuilder.Entity<SpermRetrievalMethod>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("SpermRetrievalMethod");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<SpermScore>(entity =>
            {
                entity.HasKey(e => e.SpermScoreId)
                    .IsClustered(false);

                entity.ToTable("SpermScore");

                entity.HasIndex(e => e.SqlId, "IX_SpermScore")
                    .IsClustered();

                entity.Property(e => e.SpermScoreId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.ActivityA).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ActivityB).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ActivityC).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.ActivityD).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Concentration).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.Morphology).HasColumnType("decimal(18, 2)");

                entity.Property(e => e.RecordTime).HasColumnType("datetime");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.Property(e => e.Volume).HasColumnType("decimal(18, 2)");

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

            modelBuilder.Entity<SpermThaw>(entity =>
            {
                entity.HasKey(e => e.SpermThawId)
                    .IsClustered(false);

                entity.ToTable("SpermThaw");

                entity.HasIndex(e => e.SqlId, "IX_SpermThaw")
                    .IsClustered();

                entity.Property(e => e.SpermThawId).HasDefaultValueSql("(newid())");

                entity.Property(e => e.SqlId).ValueGeneratedOnAdd();

                entity.HasOne(d => d.CourseOfTreatment)
                    .WithMany(p => p.SpermThaws)
                    .HasForeignKey(d => d.CourseOfTreatmentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_SpermThaw_CourseOfTreatment");
            });

            modelBuilder.Entity<StorageCanist>(entity =>
            {
                entity.HasKey(e => e.SqlId)
                    .HasName("PK_StorageStrip");

                entity.ToTable("StorageCanist");

                entity.HasOne(d => d.StorageTank)
                    .WithMany(p => p.StorageCanists)
                    .HasForeignKey(d => d.StorageTankId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageStrip_StorageTank");
            });

            modelBuilder.Entity<StorageStripBox>(entity =>
            {
                entity.HasKey(e => e.SqlId)
                    .HasName("PK_StorageCaneBox");

                entity.ToTable("StorageStripBox");

                entity.HasOne(d => d.StorageCanist)
                    .WithMany(p => p.StorageStripBoxes)
                    .HasForeignKey(d => d.StorageCanistId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageCaneBox_StorageShelf");
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

                entity.HasOne(d => d.StorageStripBox)
                    .WithMany(p => p.StorageUnits)
                    .HasForeignKey(d => d.StorageStripBoxId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_StorageUnit_StorageCaneBox");
            });

            modelBuilder.Entity<TopColor>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("TopColor");

                entity.Property(e => e.SqlId).ValueGeneratedNever();
            });

            modelBuilder.Entity<Treatment>(entity =>
            {
                entity.HasKey(e => e.SqlId);

                entity.ToTable("Treatment");

                entity.Property(e => e.SqlId).ValueGeneratedNever();

                entity.HasOne(d => d.EmbryoOperation)
                    .WithMany(p => p.TreatmentEmbryoOperations)
                    .HasForeignKey(d => d.EmbryoOperationId)
                    .HasConstraintName("FK_Treatment_GermCellOperation3");

                entity.HasOne(d => d.EmbryoSituation)
                    .WithMany(p => p.TreatmentEmbryoSituations)
                    .HasForeignKey(d => d.EmbryoSituationId)
                    .HasConstraintName("FK_Treatment_GermCellSituation3");

                entity.HasOne(d => d.OvumOperation)
                    .WithMany(p => p.TreatmentOvumOperations)
                    .HasForeignKey(d => d.OvumOperationId)
                    .HasConstraintName("FK_Treatment_GermCellOperation1");

                entity.HasOne(d => d.OvumSituation)
                    .WithMany(p => p.TreatmentOvumSituations)
                    .HasForeignKey(d => d.OvumSituationId)
                    .HasConstraintName("FK_Treatment_GermCellSituation1");

                entity.HasOne(d => d.OvumSource)
                    .WithMany(p => p.TreatmentOvumSources)
                    .HasForeignKey(d => d.OvumSourceId)
                    .HasConstraintName("FK_Treatment_GermCellSource1");

                entity.HasOne(d => d.SpermOperation)
                    .WithMany(p => p.TreatmentSpermOperations)
                    .HasForeignKey(d => d.SpermOperationId)
                    .HasConstraintName("FK_Treatment_GermCellOperation2");

                entity.HasOne(d => d.SpermRetrievalMethod)
                    .WithMany(p => p.Treatments)
                    .HasForeignKey(d => d.SpermRetrievalMethodId)
                    .HasConstraintName("FK_Treatment_SpermRetrievalMethod");

                entity.HasOne(d => d.SpermSituation)
                    .WithMany(p => p.TreatmentSpermSituations)
                    .HasForeignKey(d => d.SpermSituationId)
                    .HasConstraintName("FK_Treatment_GermCellSituation2");

                entity.HasOne(d => d.SpermSource)
                    .WithMany(p => p.TreatmentSpermSources)
                    .HasForeignKey(d => d.SpermSourceId)
                    .HasConstraintName("FK_Treatment_GermCellSource2");
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
