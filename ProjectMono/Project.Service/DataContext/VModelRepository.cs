using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
   public class VModelRepository : IVehicleModelRepository
    {
        private readonly ProjectDbContext context;
        private ILogger<IVehicleModelRepository> logger;

        public VModelRepository(ProjectDbContext context)
        {
            this.context = context;
        }

        public VModelRepository(ILogger<IVehicleModelRepository> logger)
        {
            this.logger = logger;
        }
  
        public IEnumerable<VehicleModel> GetAllVehicleModels()
        {
            return context.vehicleModels;
        }

        public async Task<VehicleModel> GetVehicleModel(int id)
        {
            return await context.vehicleModels.FindAsync(id);
        }

        public async Task<VehicleModel> UpdateVehicleModel(VehicleModel vehicleModelChanges)
        {
            var vehicle = context.vehicleModels.Attach(vehicleModelChanges); //context.vehicleMakes.Attach(vehicleMakeChanges);
            vehicle.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return vehicleModelChanges;
        }

        public async Task<VehicleModel> DeleteVehicleModel(int id)
        {
            VehicleModel vehicleModel = await context.vehicleModels.FindAsync(id);

            if (vehicleModel != null)
            {
                context.vehicleModels.Remove(vehicleModel);
                await context.SaveChangesAsync();
            }
            return vehicleModel;
        }

        public async Task<VehicleModel> AddNewVehicleModel(VehicleModel vehicle)
        {
            context.Add(vehicle);
            await context.SaveChangesAsync();
            return vehicle;
        }

        public IQueryable<VehicleModel> GetVehicleModelsIQueryable(IEnumerable<VehicleModel> vehicleModels)
        {
            var VehicleModel = from v in context.vehicleModels
                               select v;
            return VehicleModel;
        }

        public IEnumerable<VehicleMake> GetVehicleMakes()
        {
            return context.vehicleMakes;
        }
    }
}
