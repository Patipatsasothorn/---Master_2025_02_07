using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BPI_BillOfLading.Models.StockOutModel;

    public class WHbyColumnContext : DbContext
    {
        public WHbyColumnContext() { }

        public WHbyColumnContext(DbContextOptions<WHbyColumnContext> options) : base(options) { }

        public virtual DbSet<WHbyColumnModel> WHbyColumnModels { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // แมปโมเดลกับ stored procedure
            modelBuilder.Entity<WHbyColumnModel>().HasNoKey().ToView("WHCode");
        }
    }

