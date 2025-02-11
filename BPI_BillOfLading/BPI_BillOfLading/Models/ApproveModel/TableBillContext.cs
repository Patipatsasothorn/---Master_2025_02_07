
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.context;
public class TableBillContext : DbContext
{
    public TableBillContext() { }

    public TableBillContext(DbContextOptions<TableBillContext> options) : base(options) { }

    public virtual DbSet<TableBillModel> TableBillModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // แมปโมเดลกับ stored procedure
        modelBuilder.Entity<TableBillModel>().HasNoKey().ToView("TableBill");
    }
}

