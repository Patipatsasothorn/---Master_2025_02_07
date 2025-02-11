using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.StockOutModel;
public class WHContext : DbContext
{

    public WHContext() { }

    public WHContext(DbContextOptions<WHContext> options) : base(options) { }

    public virtual DbSet<WHModel> WHModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // แมปโมเดลกับ stored procedure
        modelBuilder.Entity<WHModel>().HasNoKey().ToView("WHDescription");
    }
}

