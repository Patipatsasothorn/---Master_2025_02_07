using System;
using System.Collections.Generic;
using BPI_UserSettings.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace BPI_UserSettings.Models;

public partial class BpigContext : DbContext
{
    public BpigContext()
    {
    }

    public BpigContext(DbContextOptions<BpigContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Dep> Deps { get; set; }
    public virtual DbSet<UserRight> UserRights { get; set; }
    public virtual DbSet<BolUsersPolicy> BolUsersPolicies { get; set; }
    public virtual DbSet<ReasonModel> ReasonModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:SetUsersConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Dep>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("_Dep");

            entity.Property(e => e.DepCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DepName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<UserRight>(entity =>
        {
            entity
                .HasNoKey()
                .ToTable("UserRight");

            entity.HasIndex(e => e.UserId, "IX_UserRight");

            entity.HasIndex(e => e.UserName, "IX_UserRight_1").IsUnique();

            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Remark)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.UserId)
                .ValueGeneratedOnAdd()
                .HasColumnName("UserID");
            entity.Property(e => e.UserName)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.UserPassword)
                .HasMaxLength(64)
                .IsFixedLength();
        });

        modelBuilder.Entity<BolUsersPolicy>(entity =>
        {
            entity.HasKey(e => e.RowId);

            entity.ToTable("BOL_UsersPolicy");

            entity.Property(e => e.RowId)
                .ValueGeneratedNever()
                .HasColumnName("RowID");
            entity.Property(e => e.CredateDate).HasColumnType("datetime");
            entity.Property(e => e.DataCode)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DataType)
                .HasMaxLength(3)
                .IsUnicode(false);
            entity.Property(e => e.UserId).HasColumnName("UserID");
        });

        modelBuilder.Entity<ReasonModel>().HasNoKey().ToView("BPI_BillOfLoading_User");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
