using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.StockOutModel;
public class allWHContext : DbContext
{

    public allWHContext() { }

    public allWHContext(DbContextOptions<allWHContext> options) : base(options) { }

    public virtual DbSet<allWHModel> allWHModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // แมปโมเดลกับ stored procedure
        modelBuilder.Entity<allWHModel>().HasNoKey().ToView("allWH");
    }
}
