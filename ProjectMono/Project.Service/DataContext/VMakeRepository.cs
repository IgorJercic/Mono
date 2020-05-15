using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Service.Models
{
   public class VMakeRepository : IVehicleMakeRepository
    {
 
        private readonly ProjectDbContext context;
        private ILogger<VMakeRepository> logger;

        //private ILogger<IVehicleMakeRepository> logger;

        public VMakeRepository(ProjectDbContext context)
        {
            this.context = context;
        }

        public VMakeRepository(ILogger<VMakeRepository> logger)
        {
            this.logger = logger;
        }

        public IEnumerable<VehicleMake> GetAllVehicleMakes(string sortOrder, string searchString, string currentFilter, int? page)
        {
           var NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
           var AbrvSortParm  = String.IsNullOrEmpty(sortOrder) ? "abrv_desc" : "";
           var CurrentFilter = searchString;
           var CurrentSort = sortOrder;

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            CurrentFilter = searchString;

            //no matter what, this code is fill a container
            var vehicleMake = from v in context.vehicleMakes
                              select v;

            if (!string.IsNullOrEmpty(searchString))
            {
                vehicleMake = vehicleMake.Where(x => x.Name.Contains(searchString) || x.Abrv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    vehicleMake = vehicleMake.OrderBy(x => x.Name);
                    break;
                case "abrv_desc":
                    vehicleMake = vehicleMake.OrderBy(x => x.Abrv);
                    break;
            }

            int pageSize = 5;
            // var mappetList = Mapper.Map<List<VehicleMakeDTO>>(vehicleMake);
            // return View(await Models.PaginatedList<VehicleMake>.CreateAsync(vehicleMake.AsNoTracking(), pageNumber ?? 1, pageSize));
            return vehicleMake;




            return context.vehicleMakes;
        }

        public IEnumerable<VehicleMake> GetAllVehicleMakes()
        {
            return  context.vehicleMakes;
        }

        public async Task<VehicleMake> GetVehicleMake(int id)
        {
            return await context.vehicleMakes.FindAsync(id);
        }

        public async Task<VehicleMake> UpdateVehicleMake(VehicleMake vehicleMakeChanges)
        {
            var vehicle = context.vehicleMakes.Attach(vehicleMakeChanges);
            vehicle.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            await context.SaveChangesAsync();
            return vehicleMakeChanges;
        }
        
        public async Task<VehicleMake> AddNewVehicleMake(VehicleMake vehicle)
        {
            context.Add(vehicle);
            await context.SaveChangesAsync();
            return  vehicle;
        }

        public async Task<VehicleMake> DeleteVehicleMake(int id)
        {
            VehicleMake vehicle = await context.vehicleMakes.FindAsync(id);

            if (vehicle != null)
            {
                //context.Remove(vehicle_fk);
                var commandText = "delete from VehicleModel where MakeId = @id";
                var vehicle_model = new SqlParameter("@id", id);
                context.Database.ExecuteSqlCommand(commandText, vehicle_model);

                context.vehicleMakes.Remove(vehicle);
                await context.SaveChangesAsync();
            }
            return vehicle;
        }

    }
}
