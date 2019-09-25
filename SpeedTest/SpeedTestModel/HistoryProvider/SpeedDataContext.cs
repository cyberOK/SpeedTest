using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SpeedTestModel.HistoryProvider
{
    public class SpeedDataContext : DbContext
    {
        public DbSet<SpeedData> SpeedDatas { get; set; }

        public SpeedDataContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=History.db");
        }
    }
}
