using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.StockOutModel;
public class RTableBillContextStockOut : DbContext
{
    public RTableBillContextStockOut() { }

    public RTableBillContextStockOut(DbContextOptions<RTableBillContextStockOut> options) : base(options) { }

    public virtual DbSet<RTableBillModelStockOut> RTableBillModelStockOuts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // แมปโมเดลกับ stored procedure
        modelBuilder.Entity<RTableBillModelStockOut>().HasNoKey().ToView("ReturnGetTabelBill");
    }
}

