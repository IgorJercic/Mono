using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
   public interface IVehicleModelRepository
    {
        //Vehicle model
        IEnumerable<VehicleModel> GetAllVehicleModels();
        Task<VehicleModel> GetVehicleModel(int id);
        Task<VehicleModel> UpdateVehicleModel(VehicleModel vehicleModelChanges);
        Task<VehicleModel> AddNewVehicleModel(VehicleModel vehicle);
        Task<VehicleModel> DeleteVehicleModel(int id);
  
        IEnumerable<VehicleMake> GetVehicleMakes();

        IQueryable<VehicleModel> GetVehicleModelsIQueryable(IEnumerable<VehicleModel> vehicleModels);
    }
}
