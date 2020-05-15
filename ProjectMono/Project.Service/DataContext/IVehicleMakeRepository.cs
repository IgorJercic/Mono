using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
   public interface IVehicleMakeRepository
    {
        //Vehicle make
      //  IEnumerable<VehicleMake> GetAllVehicleMakes(string sortOrder, string searchString, string currentFilter, int? page);
        IEnumerable<VehicleMake> GetAllVehicleMakes();
        Task<VehicleMake> GetVehicleMake(int id);
        Task<VehicleMake> UpdateVehicleMake(VehicleMake vehicleMakeChanges);
        Task<VehicleMake> AddNewVehicleMake(VehicleMake vehicle);
        Task<VehicleMake> DeleteVehicleMake(int id);
    }
}
