using BPI_BillOfLading.Models.StockOutModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.ReturnStockModel;

    public class convertfcontext : DbContext
{
    public convertfcontext() { }

    public convertfcontext(DbContextOptions<convertfcontext> options) : base(options) { }

    public virtual DbSet<convertfmodel> convertfmodels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // แมปโมเดลกับ stored procedure
        modelBuilder.Entity<convertfmodel>().HasNoKey().ToView("convertf");
    }
}

