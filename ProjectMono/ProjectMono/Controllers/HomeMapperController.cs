using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using ProjectMono.Models;
using AutoMapper;
using Project.Service;
using System.Collections.Generic;
using X.PagedList;
using System.Diagnostics;
using Ninject;

namespace ProjectMono.Controllers
{
    public class HomeMapperController : Controller
    {
       
        // Constructor injection
        private readonly IVehicleMakeRepository DbContext;
        private readonly IMapper Mapper;

        public HomeMapperController(IVehicleMakeRepository context, IMapper mapper)
        {
            this.DbContext = context;
            this.Mapper = mapper;
        }

        


        public  IActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {
            var pageNumber = page ?? 1;
            //no matter what, this code is fill a container
            //var model = DbContext.GetAllVehicleMakes(sortOrder, searchString, currentFilter, page);
            var model = DbContext.GetAllVehicleMakes();
            var mapper_model = Mapper.Map<List<VehicleMakeDTO>>(model);
            var mapperForView = Mapper.Map<List<VehicleMakeDTO>>(model);
            return View(mapperForView.ToList().ToPagedList(pageNumber, 5));
            
        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            int ID = id.GetValueOrDefault();
            if (id == null)
            {
                return NotFound();
            }
            var vehicleMake = await DbContext.GetVehicleMake(ID);
            if (vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            int ID = id.GetValueOrDefault();
            if (id == null)
            {
                return NotFound();
            }
            var vehicleMake = await DbContext.GetVehicleMake(ID);
            if(vehicleMake == null)
            {
                return NotFound();
            }
            return View(vehicleMake);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? id, [Bind("Id,Name,Abrv")] VehicleMake vehicle)
        {
        
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   await DbContext.UpdateVehicleMake(vehicle);
                }
                catch (DbUpdateConcurrencyException)
                {
                        return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }

            return View(vehicle);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                await DbContext.AddNewVehicleMake(vehicleMake);
                return RedirectToAction(nameof(Index));
            }
            return View(vehicleMake);
        }

        public async Task<IActionResult> Delete(int id)
        {
            await DbContext.DeleteVehicleMake(id);
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}