using BPI_BillOfLading.context;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.ApproveModel
{
    public class ApproveContext : DbContext
    {
        public ApproveContext() { }

        public ApproveContext(DbContextOptions<ApproveContext> options) : base(options) { }

        public virtual DbSet<ApproveModel> ApproveModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // แมปโมเดลกับ stored procedure
            modelBuilder.Entity<ApproveModel>().HasNoKey().ToView("Approve");
        }
    }
}
