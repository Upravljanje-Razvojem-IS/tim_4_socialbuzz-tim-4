using FluentValidation;
using FluentValidation.AspNetCore;
using Logistics.API.Interfaces;
using Logistics.API.Services;
using Logistics.Core.Entities;
using Logistics.Infrastructure;
using Logistics.Infrastructure.Configurations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Net.Mime;
using System.Reflection;
using System.Text;

namespace Logistics.API
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
            /*
             custom exceptioni za svaki return null
            refactor
            logger
            Validation entiteta,
            global exception,
            logger
             */

            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            });
            services.AddMvc().AddFluentValidation();

            services.AddTransient<IValidator<Address>, AddressConfiguration>();
            services.AddTransient<IValidator<City>, CityConfiguration>();
            services.AddTransient<IValidator<DistancePrice>, DistanceServiceConfiguration>();
            services.AddTransient<IValidator<Purchase>, PurchaseConfiguration>();
            services.AddTransient<IValidator<WeightRange>, WeightRangeConfiguration>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API", Version = "v1" });

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name }.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);

                c.IncludeXmlComments(xmlCommentsPath);
            });

            services.AddDbContext<LogisticsDbContext>(options =>
               options.UseSqlServer(Configuration.GetConnectionString("DatabaseString"))
            );

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = Configuration["Jwt:Issuer"],
                    ValidAudience = Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Secret"]))
                };
              
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddScoped<ICityService, CityService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IDistancePrice, DistancePriceService>();
            services.AddScoped<IWeightRangeService, WeightRangeService>();
            services.AddScoped<IPurchaseService, PurchaseService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Logistics.API v1"));
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }



            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
