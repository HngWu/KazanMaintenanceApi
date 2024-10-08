using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KazanMaintenanceApi.Models;

public partial class Wsc2019Session3FinalContext : DbContext
{
    public Wsc2019Session3FinalContext()
    {
    }

    public Wsc2019Session3FinalContext(DbContextOptions<Wsc2019Session3FinalContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Asset> Assets { get; set; }

    public virtual DbSet<AssetOdometer> AssetOdometers { get; set; }

    public virtual DbSet<PmscheduleModel> PmscheduleModels { get; set; }

    public virtual DbSet<PmscheduleType> PmscheduleTypes { get; set; }

    public virtual DbSet<Pmtask> Pmtasks { get; set; }

    public virtual DbSet<Task> Tasks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=wsc2019Session3Final;Trusted_Connection=true;TrustServerCertificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Asset>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AssetGroupId).HasColumnName("AssetGroupID");
            entity.Property(e => e.AssetName).HasMaxLength(150);
            entity.Property(e => e.AssetSn)
                .HasMaxLength(20)
                .HasColumnName("AssetSN");
            entity.Property(e => e.DepartmentLocationId).HasColumnName("DepartmentLocationID");
            entity.Property(e => e.Description).HasMaxLength(2000);
            entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");
        });

        modelBuilder.Entity<AssetOdometer>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AssetId).HasColumnName("AssetID");

            entity.HasOne(d => d.Asset).WithMany(p => p.AssetOdometers)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_AssetOdometers_Assets");
        });

        modelBuilder.Entity<PmscheduleModel>(entity =>
        {
            entity.ToTable("PMScheduleModels");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
            entity.Property(e => e.PmscheduleTypeId).HasColumnName("PMScheduleTypeID");

            entity.HasOne(d => d.PmscheduleType).WithMany(p => p.PmscheduleModels)
                .HasForeignKey(d => d.PmscheduleTypeId)
                .HasConstraintName("FK_PMScheduleModels_PMScheduleTypes");
        });

        modelBuilder.Entity<PmscheduleType>(entity =>
        {
            entity.ToTable("PMScheduleTypes");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        modelBuilder.Entity<Pmtask>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PMTask");

            entity.ToTable("PMTasks");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.AssetId).HasColumnName("AssetID");
            entity.Property(e => e.PmscheduleTypeId).HasColumnName("PMScheduleTypeID");
            entity.Property(e => e.TaskId).HasColumnName("TaskID");

            entity.HasOne(d => d.Asset).WithMany(p => p.Pmtasks)
                .HasForeignKey(d => d.AssetId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PMTasks_Assets");

            entity.HasOne(d => d.PmscheduleType).WithMany(p => p.Pmtasks)
                .HasForeignKey(d => d.PmscheduleTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PMTasks_PMScheduleTypes");

            entity.HasOne(d => d.Task).WithMany(p => p.Pmtasks)
                .HasForeignKey(d => d.TaskId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PMTasks_Tasks");
        });

        modelBuilder.Entity<Task>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Name).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
