using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.StockOutModel;
public class TableBillContextStockOut : DbContext
{
    public TableBillContextStockOut() { }

    public TableBillContextStockOut(DbContextOptions<TableBillContextStockOut> options) : base(options) { }

    public virtual DbSet<TableBillModelStockOut> TableBillModelStockOuts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // แมปโมเดลกับ stored procedure
        modelBuilder.Entity<TableBillModelStockOut>().HasNoKey().ToView("GetTabelBill");
    }
}

