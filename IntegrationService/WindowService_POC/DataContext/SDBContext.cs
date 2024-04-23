using Microsoft.EntityFrameworkCore;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowService_POC.Model;

namespace WindowService_POC.DataContext
{
    public class SDBContext : DbContext
    {
        private const string connectionString = @"Data Source=LAPTOP-2LB1Q1KD\SQLEXPRESS; Initial Catalog=WFMDB;User Id=sa;Password=sql123;TrustServerCertificate=True";

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
        }
        public DbSet<Schedule> Schedules { get; set; }
        public DbSet<Projection> Projections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Projection>()
            .HasOne(p => p.Schedule)
            .WithMany(s => s.Projection)
            .HasForeignKey(p => p.SId);
        }
    }
}
