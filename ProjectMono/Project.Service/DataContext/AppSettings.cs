using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Project.Service.DataContext
{
   public class AppSettings
    {
        public AppSettings()
        {
            var configBuilder = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            configBuilder.AddJsonFile(path, false);
            var root = configBuilder.Build();
            var appSettings = root.GetSection("ConnectionStrings:MyConnection1");
            connectionSqlString = appSettings.Value;
        }
        public string connectionSqlString { get; set; }
    }
}
