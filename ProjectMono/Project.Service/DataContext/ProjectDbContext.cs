using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Project.Service.DataContext;

namespace Project.Service.Models
{
   public class ProjectDbContext: DbContext
    {
        public class BuildOptions
        {
            public BuildOptions()
            {
                settings = new AppSettings();
                opsBuilder = new DbContextOptionsBuilder<ProjectDbContext>();
                opsBuilder.UseSqlServer(settings.connectionSqlString);
                dbOptions = opsBuilder.Options;
            }

            public DbContextOptionsBuilder<ProjectDbContext> opsBuilder { get; set; }
            public DbContextOptions<ProjectDbContext> dbOptions { get; set; }
            AppSettings settings { get; set; }
        }

        public static BuildOptions ops = new BuildOptions();

        public ProjectDbContext(DbContextOptions<ProjectDbContext> options): base(options){}
        public virtual DbSet<VehicleMake> vehicleMakes { get; set; }
        public virtual DbSet<VehicleModel> vehicleModels { get; set; }

        /// <summary>
        /// Kreiramo bazu i podatke koji su nam potrebni s time da overide virtual OnModelCreating
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.SeedData();   
        }
    }
}
