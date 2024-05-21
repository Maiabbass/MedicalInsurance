using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using api.Data;
using api.Entities;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
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


            services.AddIdentity<User,IdentityRole>(

                opt=>
                {
                      opt.Password.RequiredLength=3;
                    opt.Password.RequireDigit=false;
                    opt.Password.RequiredUniqueChars=0;
                    opt.Password.RequireUppercase=false;
                    opt.Password.RequireNonAlphanumeric=false;
                }
            )
            .AddEntityFrameworkStores<DataContext>().AddDefaultTokenProviders();
             


             // Authentication
            services.AddAuthentication(opt=>
            {
                    opt.DefaultAuthenticateScheme= JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    opt.DefaultScheme=JwtBearerDefaults.AuthenticationScheme;
            })
            // Adding Jwt Bearer
              .AddJwtBearer(options =>
                {
                 options.SaveToken = true;
                 options.RequireHttpsMetadata = false;
                  options.TokenValidationParameters = new TokenValidationParameters()
                {
                   ValidateIssuer = true,
                   ValidateAudience = true,
                  ValidAudience = _config["JWT:ValidAudience"],
                  ValidIssuer = _config["JWT:ValidIssuer"],
                  IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Secret"]))
                 };
                });
            
            
              services.AddControllers();
              services.AddCors();
              services.AddScoped<IUnitOfWork,UnitOfWork>();
              services.AddScoped<IEngineerService,EngineerService>();
              services.AddScoped<IPersonService,PersonService>();

              services.AddScoped<IAnnualDataService,AnnualDataService>();
              services.AddScoped<ICityService,CityService>();
              services.AddScoped<IHospitalService,HospitalService>();
              services.AddScoped<IWorkplaceService,WorkPlaceService>();
              services.AddScoped<IEngineeringUnitsService,EngineeringUnitsService>();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPIv5", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
           // app.Build();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPIv5 v1"));
            }

            app.UseHttpsRedirection();

            app.UseCors(x=>x.AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials()
            .WithOrigins("https://localhost:4200"));

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseRouting();

            app.UseAuthorization();

            
             
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
              });






            
        }
    }
}
