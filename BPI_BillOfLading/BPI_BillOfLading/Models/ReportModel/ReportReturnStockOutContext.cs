using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.ReportModel
{
    public class ReportReturnStockOutContext : DbContext
    {
        public ReportReturnStockOutContext(DbContextOptions<ReportReturnStockOutContext> options) : base(options) { }

        public virtual DbSet<ReportReturnStockOutModel> ReportReturnStockOutModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ReportReturnStockOutModel>().HasNoKey().ToView("reportruturn");
        }
    }
}
