﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Service.Models;
using ReflectionIT.Mvc.Paging;

namespace ProjectMono
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContextPool<Project.Service.Models.DbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection"), b => b.MigrationsAssembly("ProjectMono")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            #region conn strings 
            //services.AddDbContext<MonoContext>(options =>
            //{
            //    options.UseSqlServer(Configuration.GetConnectionString("MyConnection"));
            //});

            //  services.AddDbContext<MonoContext>(opts => opts.UseSqlServer(Configuration["ConnectionString:MyConnection"]));
            // services.AddControllers();
            #endregion
            services.AddScoped<IVehicleMakeRepository, VMakeRepository>();
            services.AddScoped<IVehicleModelRepository, VModelRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddPaging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }
            else
            {
                app.UseExceptionHandler("Home/Error/");
            }

            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
