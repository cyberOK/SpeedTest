using Microsoft.EntityFrameworkCore;
using SpeedTestModel.IPerf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpeedTestModel.ServerIPerfProvider
{
    public class ServerIPerfContext : DbContext
    {
        public DbSet<ServerIPerf> ServersIPerf { get; set; }

        public ServerIPerfContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Filename=ServersIPerf.db");
        }
    }
}
