using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Project.Service.Models;

namespace Project.Service.DataContext 
{
    public class DbFactory : IDesignTimeDbContextFactory<ProjectDbContext>
    {
        public ProjectDbContext CreateDbContext(string[] args)
        {
            //string connectionSqlString = "server=DESKTOP-1JOC7A2\\SQLEXPRESS;database=Mono;Trusted_Connection=true";
            AppSettings appSettings = new AppSettings();
            var opsBuild = new DbContextOptionsBuilder<ProjectDbContext>();
             opsBuild.UseSqlServer(appSettings.connectionSqlString);
            //opsBuild.UseSqlServer(connectionSqlString);
            return new ProjectDbContext(opsBuild.Options);
        }
    }
}
