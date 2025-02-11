using System;
using System.Collections.Generic;
using BPI_UserSettings.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace BPI_UserSettings.Models;

public partial class BpiDevContext : DbContext
{
    public BpiDevContext()
    {
    }

    public BpiDevContext(DbContextOptions<BpiDevContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Reason> Reasons { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=ConnectionStrings:SetUsersConnectionERP");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Reason>(entity =>
        {
            entity.HasKey(e => new { e.Company, e.ReasonType, e.ReasonCode });

            entity.ToTable("Reason", "Erp", tb => tb.HasTrigger("TR_Reason_ChangeCapture"));

            entity.HasIndex(e => e.SysRowId, "IX_Reason_SysIndex").IsUnique();

            entity.HasIndex(e => new { e.Company, e.ReasonType, e.Description }, "IX_Reason_TypeDescription").IsUnique();

            entity.Property(e => e.Company)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.ReasonType)
                .HasMaxLength(2)
                .HasDefaultValue("");
            entity.Property(e => e.ReasonCode)
                .HasMaxLength(8)
                .HasDefaultValue("");
            entity.Property(e => e.Description)
                .HasMaxLength(30)
                .HasDefaultValue("");
            entity.Property(e => e.DmracceptInv)
                .HasDefaultValue(true)
                .HasColumnName("DMRAcceptInv");
            entity.Property(e => e.DmracceptMtl)
                .HasDefaultValue(true)
                .HasColumnName("DMRAcceptMtl");
            entity.Property(e => e.DmracceptOpr)
                .HasDefaultValue(true)
                .HasColumnName("DMRAcceptOpr");
            entity.Property(e => e.DmracceptSub)
                .HasDefaultValue(true)
                .HasColumnName("DMRAcceptSub");
            entity.Property(e => e.DmrrejInv)
                .HasDefaultValue(true)
                .HasColumnName("DMRRejInv");
            entity.Property(e => e.DmrrejMtl)
                .HasDefaultValue(true)
                .HasColumnName("DMRRejMtl");
            entity.Property(e => e.DmrrejOpr)
                .HasDefaultValue(true)
                .HasColumnName("DMRRejOpr");
            entity.Property(e => e.DmrrejSub)
                .HasDefaultValue(true)
                .HasColumnName("DMRRejSub");
            entity.Property(e => e.ExternalMeslastSync)
                .HasColumnType("datetime")
                .HasColumnName("ExternalMESLastSync");
            entity.Property(e => e.ExternalMessyncRequired).HasColumnName("ExternalMESSyncRequired");
            entity.Property(e => e.InspFailInv).HasDefaultValue(true);
            entity.Property(e => e.InspFailMtl).HasDefaultValue(true);
            entity.Property(e => e.InspFailOpr).HasDefaultValue(true);
            entity.Property(e => e.InspFailSub).HasDefaultValue(true);
            entity.Property(e => e.JdfworkType)
                .HasMaxLength(50)
                .HasDefaultValue("")
                .HasColumnName("JDFWorkType");
            entity.Property(e => e.NonConfInv).HasDefaultValue(true);
            entity.Property(e => e.NonConfMtl).HasDefaultValue(true);
            entity.Property(e => e.NonConfOpr).HasDefaultValue(true);
            entity.Property(e => e.NonConfOther).HasDefaultValue(true);
            entity.Property(e => e.NonConfSub).HasDefaultValue(true);
            entity.Property(e => e.Qacause)
                .HasDefaultValue(true)
                .HasColumnName("QACause");
            entity.Property(e => e.QacorrectiveAct)
                .HasDefaultValue(true)
                .HasColumnName("QACorrectiveAct");
            entity.Property(e => e.Scrap).HasDefaultValue(true);
            entity.Property(e => e.SysRevId)
                .IsRowVersion()
                .IsConcurrencyToken()
                .HasColumnName("SysRevID");
            entity.Property(e => e.SysRowId)
                .HasDefaultValueSql("(CONVERT([uniqueidentifier],CONVERT([binary](10),newid())+CONVERT([binary](6),getutcdate())))")
                .HasColumnName("SysRowID");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
