using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Service;
using Project.Service.Models;
using ProjectMono.Models;
using X.PagedList;

namespace ProjectMono.Controllers
{
    public class VehicleModelsController : Controller
    {
        private readonly Project.Service.Models.IVehicleModelRepository DbContext;
        private readonly IMapper Mapper;
        public VehicleModelsController(IVehicleModelRepository context, IMapper mapper)
        {
            this.DbContext = context;
            this.Mapper = mapper;

        }

        // GET: VehicleModels
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
         
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = String.IsNullOrEmpty(sortOrder) ? "abrv_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var pageNumber = page ?? 1;
            ViewData["CurrentFilter"] = searchString;

            var vehicleModel = DbContext.GetAllVehicleModels();
            var mapperModel = Mapper.Map<List<VehicleModelDTO>>(vehicleModel);
            var mapperForView = Mapper.Map<List<VehicleModelDTO>>(vehicleModel);


            if (!String.IsNullOrEmpty(searchString))
            {
                var mapperForViewSearch = mapperModel.Where(x => x.Name.Contains(searchString) || x.Abrv.Contains(searchString));
                return View(mapperForViewSearch.ToList().ToPagedList(pageNumber, 5));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    mapperForView = mapperForView.OrderBy(x => x.Name).ToList();
                    break;
                case "abrv_desc":
                    mapperForView = mapperForView.OrderBy(x => x.Abrv).ToList();
                    break;
            }
            ViewBag.VehicleMakes = DbContext.GetVehicleMakes();
            return View(await mapperForView.ToList().ToPagedListAsync(pageNumber, 5));
          
        }

        // GET: VehicleModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            int ID = id.GetValueOrDefault();
            var vehicleModel = await DbContext.GetVehicleModel(ID);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            ViewBag.VehicleMakes = DbContext.GetVehicleMakes();
            return View(vehicleModel);
        }

        // GET: VehicleModels/Create
        public IActionResult Create()
        {
            var CarMakeModel = DbContext.GetVehicleMakes().ToList();
            CarMakeModel.Insert(0, new VehicleMake { Id = 0, Name = "Select" });
            ViewBag.List = CarMakeModel;
            return View();
        }

        //other way of create not the same as others just to show!
        [HttpGet()]
        public async Task<ActionResult> CreateModel(string name, string abrv, int MakeId)
        {
            VehicleModel vehicleModel = new VehicleModel();
            vehicleModel.Name = name;
            vehicleModel.Abrv = abrv;
            vehicleModel.MakeId = MakeId;
           await DbContext.AddNewVehicleModel(vehicleModel);
           return RedirectToAction(nameof(Index));
        }

        // GET: VehicleModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
         
            if (id == null)
            {
                return NotFound();
            }
            int ID = id.GetValueOrDefault();
            var vehicleModel = await DbContext.GetVehicleModel(ID);
            if (vehicleModel == null)
            {
                return NotFound();
            }

            //needs for listing car makes
            var CarMakeModel = DbContext.GetVehicleMakes().ToList();
            CarMakeModel.Insert(0, new VehicleMake { Id = 0, Name = "Select" });
            ViewBag.List = CarMakeModel;
            return View(vehicleModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, string name, string abrv, int makeId, VehicleModel vehicleModel)
        {
            if (id != vehicleModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await DbContext.UpdateVehicleModel(vehicleModel);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleModelExists(vehicleModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        return Error();
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleModel);
        }

        // GET: VehicleModels/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return NotFound();
            }
            var vehicleModel = await DbContext.GetVehicleModel(id);
            if (vehicleModel == null)
            {
                return NotFound();
            }
            return View(vehicleModel);
        }

        // POST: VehicleModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {

            var vehicleModel = await DbContext.GetVehicleModel(id);
            await DbContext.DeleteVehicleModel(id);
            return RedirectToAction(nameof(Index));
        }

        private bool VehicleModelExists(int id)
        {
            if(DbContext.GetVehicleModel(id) != null)
            {
                return true;
            }
            return false;
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
 
}

