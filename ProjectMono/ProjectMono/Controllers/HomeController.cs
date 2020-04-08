﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Service.Models;
using ProjectMono.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Project.Service;
using System.Collections.Generic;
using System.Diagnostics;

namespace ProjectMono.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Constructor injection
        /// </summary>
        private readonly AppDbContext _context;
        // private readonly IMonoRepositry _context;
        private readonly IMapper _mapper;

        public HomeController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<IActionResult> Index(string sortOrder, string searchString, string currentFilter, int? pageNumber)
        {
            #region
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["AbrvSortParm"] = String.IsNullOrEmpty(sortOrder) ? "abrv_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            ViewData["CurrentSort"] = sortOrder;
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewData["CurrentFilter"] = searchString;

            //no matter what, this code is fill a container
            var _vehicleMake = from v in _context.vehicleMakes
                               select v;

            if (!string.IsNullOrEmpty(searchString))
            {
                _vehicleMake = _vehicleMake.Where(x => x.Name.Contains(searchString) || x.Abrv.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "name_desc":
                    _vehicleMake = _vehicleMake.OrderBy(x => x.Name);
                    break;
                case "abrv_desc":
                    _vehicleMake = _vehicleMake.OrderBy(x => x.Abrv);
                    break;
            }

            int pageSize = 5;
            var mappetList = _mapper.Map<List<VehicleMakeDTO>>(_vehicleMake);

            return View(await Models.PaginatedList<VehicleMake>.CreateAsync(_vehicleMake.AsNoTracking(), pageNumber ?? 1, pageSize));
            #endregion


        }

        [HttpGet]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicleMake = await _context.vehicleMakes.FindAsync(id);
            if (vehicleMake == null)
            {
                return NotFound();
            }

            return View(vehicleMake);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var model = await _context.vehicleMakes.FindAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Abrv")] VehicleMake vehicle)
        {
            if (id != vehicle.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(vehicle);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleMakeExists(vehicle.Id))
                    {
                        return NotFound();
                    }

                }
                return RedirectToAction(nameof(Index));
            }

            return View(vehicle);

        }

        [HttpGet]
        public IActionResult Create()
        {
            return  View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Abrv")] VehicleMake vehicleMake)
        {
            if (ModelState.IsValid)
            {
                _context.Add(vehicleMake);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));

            }
            return View(vehicleMake);
        }

        public IActionResult Delete(int id)
        {
            VehicleMake vehicle = _context.vehicleMakes.Find(id);
            if (vehicle != null)
            {
                _context.vehicleMakes.Remove(vehicle);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        #region ne upotrebljava se
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }


        #endregion


        /// <summary>
        /// check if exist, koristim za update
        /// </summary>
        /// <returns></returns>
        /// 
        private bool VehicleMakeExists(int id)
        {
            return _context.vehicleMakes.Any(e => e.Id == id);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
