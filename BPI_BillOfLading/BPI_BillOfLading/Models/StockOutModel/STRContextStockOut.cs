using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.StockOutModel;
public class STRContextStockOut : DbContext
{
    public STRContextStockOut() { }

    public STRContextStockOut(DbContextOptions<STRContextStockOut> options) : base(options) { }

    public virtual DbSet<STRModelStockOut> STRModelStockOuts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // แมปโมเดลกับ stored procedure
        modelBuilder.Entity<STRModelStockOut>().HasNoKey().ToView("recordpayments");
    }
}

