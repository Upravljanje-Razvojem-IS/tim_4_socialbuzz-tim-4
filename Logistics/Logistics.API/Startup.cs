using FluentValidation;
using FluentValidation.AspNetCore;
using Logistics.API.Interfaces;
using Logistics.API.Services;
using Logistics.Core.Entities;
using Logistics.Infrastructure;
using Logistics.Infrastructure.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;

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

            services.AddControllers();
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

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
