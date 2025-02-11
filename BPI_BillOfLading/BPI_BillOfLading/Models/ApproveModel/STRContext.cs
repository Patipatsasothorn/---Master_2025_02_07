using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.context;

public class STRContext : DbContext
{
    public STRContext() { }

    public STRContext(DbContextOptions<STRContext> options) : base(options) { }

    public virtual DbSet<STRModel> STRModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // แมปโมเดลกับ stored procedure
        modelBuilder.Entity<STRModel>().HasNoKey().ToView("BillOfLading");
    }
}

