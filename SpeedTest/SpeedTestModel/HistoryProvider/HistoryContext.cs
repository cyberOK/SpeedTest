using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SpeedTestModel.Iperf;

namespace SpeedTestModel.HistoryManager
{
    public class HistoryContext : DbContext
    {
        public DbSet<SpeedData> History { get; set; }

        public HistoryContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=History.db");
        }
    }
}
