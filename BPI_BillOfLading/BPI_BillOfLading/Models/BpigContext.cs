using System;
using System.Collections.Generic;
using BPI_BillOfLading.Models.Data;
using BPI_BillOfLading.Models.UserSetting;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models;

public partial class BpigContext : DbContext
{
    public BpigContext()
    {
    }

    public BpigContext(DbContextOptions<BpigContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BolUsersPolicy> BolUsersPolicies { get; set; }
    public virtual DbSet<Dep> Deps { get; set; }
    public virtual DbSet<BolDocHead> BolDocHeads { get; set; }
    public virtual DbSet<BolDocDetail> BolDocDetails { get; set; }
    public virtual DbSet<Holiday> Holidays { get; set; }
    public virtual DbSet<ReportsModel> ReportsModels { get; set; }
    public virtual DbSet<UserRight> UserRights { get; set; }
    public virtual DbSet<ReasonModel> ReasonModels { get; set; }
    public virtual DbSet<WhModel> WhModels { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:SetUsersConnection");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BolUsersPolicy>(entity =>
        {
            entity.HasKey(e => e.RowId);

            entity.ToTable("BOL_UsersPolicy");

            entity.Property(e => e.RowId)
                .ValueGeneratedOnAdd()
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

        modelBuilder.Entity<BolDocHead>(entity =>
        {
            entity.HasKey(e => e.DocId);

            entity.ToTable("BOL_DocHead", tb => tb.HasTrigger("TR_BOL_DocHead_UpdateStatus"));

            entity.HasIndex(e => e.DocId, "IX_BOL_DocHead").IsUnique();

            entity.Property(e => e.DocId)
                .ValueGeneratedNever()
                .HasColumnName("DocID");
            entity.Property(e => e.Company)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.Dep)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DocDate).HasColumnType("datetime");
            entity.Property(e => e.Plant)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Reason)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.Remark)
                .HasMaxLength(200)
                .IsUnicode(false);
            entity.Property(e => e.ReqDate).HasColumnType("datetime");
            entity.Property(e => e.Status)
                .HasMaxLength(1)
                .IsUnicode(false);
            entity.Property(e => e.UpdateDate).HasColumnType("datetime");
        });

        modelBuilder.Entity<BolDocDetail>(entity =>
        {
            entity.HasKey(e => e.DetailId);

            entity.ToTable("BOL_DocDetail");

            entity.HasIndex(e => e.DetailId, "IX_BOL_DocDetail");

            entity.HasIndex(e => new { e.WareHouse, e.Bin }, "IX_BOL_DocDetail_1");

            entity.HasIndex(e => e.PartNum, "IX_BOL_DocDetail_2");

            entity.Property(e => e.DetailId).HasColumnName("DetailID");
            entity.Property(e => e.Bin)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.DocId).HasColumnName("DocID");
            entity.Property(e => e.PartNum)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Qty).HasColumnType("decimal(15, 2)");
            entity.Property(e => e.Status)
               .HasMaxLength(1)
               .IsUnicode(false);
            entity.Property(e => e.Unit)
                .HasMaxLength(10)
                .IsUnicode(false);
            entity.Property(e => e.WareHouse)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Holiday>(entity =>
        {
            entity.HasKey(e => e.Holiday1); // Specify primary key

            entity.ToTable("_Holiday");

            entity.HasIndex(e => new { e.Company, e.Holiday1 }, "IX__Holiday").IsUnique();

            entity.Property(e => e.Company)
                .HasMaxLength(5)
                .IsUnicode(false);
            entity.Property(e => e.CreateDate).HasColumnType("datetime");
            entity.Property(e => e.Detail)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Holiday1).HasColumnName("Holiday");
        });

        modelBuilder.Entity<ReportsModel>().HasNoKey().ToView("BPI_BillOfLoading_PickUp");

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

        modelBuilder.Entity<ReasonModel>().HasNoKey().ToView("BPI_BillOfLoading_User");

        modelBuilder.Entity<WhModel>().HasNoKey().ToView("BPI_BillOfLoading_Wh");

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
