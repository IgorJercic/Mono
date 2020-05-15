
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project.Service.Models;
using ReflectionIT.Mvc.Paging;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;
using Autofac.Core;
using Project.Service;
using ProjectMono.Models;

namespace ProjectMono
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // public IContainer ApplicationContainer { get; private set; } 

        public ILifetimeScope AutofacContainer { get; private set; }
        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new AutofacModule());
        }


        // This method gets called by the runtime. Use this method to add services to the container. --OVO JE ORGINAL
        //public void ConfigureServices(IServiceCollection services)
        //{
        //    services.AddDbContextPool<Project.Service.Models.ProjectDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection1"), b => b.MigrationsAssembly("ProjectMono")));
        //    services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        //    services.AddScoped<IVehicleMakeRepository, VMakeRepository>();
        //    services.AddScoped<IVehicleModelRepository, VModelRepository>();
        //    services.AddAutoMapper(typeof(Startup));
        //    services.AddPaging();
        //   // services.AddAutofac();

        //    ////autofac
        //    //var builder = new Autofac.ContainerBuilder();
        //    //builder.Populate(services);
        //    //var cointener = builder.Build();
        //}

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContextPool<Project.Service.Models.ProjectDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection1"), b => b.MigrationsAssembly("ProjectMono")));
            services.AddDbContextPool<Project.Service.Models.ProjectDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection1"), b => b.MigrationsAssembly("Project.Service")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddScoped<IVehicleMakeRepository, VMakeRepository>();
            services.AddScoped<IVehicleModelRepository, VModelRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddPaging();
            // Add services to the collection
            services.AddOptions();
            services.AddAutofac();

            // Create a container-builder and register dependencies
            var builder = new ContainerBuilder();

            // Populate the service-descriptors added to `IServiceCollection`
            // BEFORE you add things to Autofac so that the Autofac
            // registrations can override stuff in the `IServiceCollection`
            // as needed
            builder.Populate(services);

            // Register your own things directly with Autofac
            builder.RegisterModule(new AutofacModule());

            AutofacContainer = builder.Build();

            // this will be used as the service-provider for the application!
            return new AutofacServiceProvider(AutofacContainer);
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
