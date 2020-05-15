using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Project.Service.Models
{
   public static class ModelBuilderExtension
    {
        public static void SeedData(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleMake>().HasData(
                new VehicleMake() { Id = 1, Name = "BMW Group", Abrv = "bmw"},
                new VehicleMake() { Id = 2, Name = "Mercedes-Benz company", Abrv = "Mercedes" },
                new VehicleMake() { Id = 3, Name = "Toyota motor", Abrv = "toyota" },
                new VehicleMake() { Id = 4, Name = "Mazda motor", Abrv = "mazda" },
                new VehicleMake() { Id = 5, Name = "Lexus - Rekusasu", Abrv = "lexus" }
                );

                 modelBuilder.Entity<VehicleModel>().HasData(
                 new VehicleModel() { Id = 1, Name = "New redisegnet Corolla 2 gen model b", Abrv = "Corolla", MakeId = 3 },
                 new VehicleModel() { Id = 2, Name = "Mazda cabrio MX-6", Abrv = "MX-6", MakeId = 4 }

             );
        }

    }
}
