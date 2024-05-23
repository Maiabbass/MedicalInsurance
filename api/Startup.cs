using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

namespace API
{
    
    public class Startup
    {
        private readonly IConfiguration _config ;
        public Startup(IConfiguration config)
        {
            _config= config;
           
        }

        

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            
           
            services.AddDbContext<DataContext>( Options => {
                Options.UseSqlServer(_config.GetConnectionString("DefaultConnection")) ;
            });
            
            services.AddControllers();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
             services.AddScoped<IEngineerService,EngineerService>();
              services.AddScoped<IPersonService,PersonService>();

              services.AddScoped<IAnnualDataService,AnnualDataService>();
              services.AddScoped<ICityService,CityService>();
              services.AddScoped<IHospitalService,HospitalService>();
              services.AddScoped<IWorkplaceService,WorkPlaceService>();
              services.AddScoped<IEngineeringUnitsService,EngineeringUnitsService>();
              services.AddScoped<IEngineeringeDeparServices,EngineeringeDeparServices>();
              services.AddScoped<ISurgicalProceduresServices,SurgicalProceduresServices>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
              });

            
        }
    }
}
