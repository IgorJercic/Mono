
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



        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
          
            services.AddDbContextPool<Project.Service.Models.ProjectDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("MyConnection1"), b => b.MigrationsAssembly("Project.Service")));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //services.AddScoped<IVehicleMakeRepository, VMakeRepository>();
            //services.AddScoped<IVehicleModelRepository, VModelRepository>();
            services.AddAutoMapper(typeof(Startup));
            services.AddPaging();
            services.AddOptions();
            services.AddAutofac();

            var builder = new ContainerBuilder();
            builder.Populate(services);
            builder.RegisterModule(new AutofacModule());
            AutofacContainer = builder.Build();
            return new AutofacServiceProvider(AutofacContainer);
        }


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
