using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PASMicroservice.DBContexts;
using PASMicroservice.Mocks;
using PASMicroservice.Repositories;

namespace PASMicroservice
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
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }).AddXmlDataContractSerializerFormatters();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddDbContext<PASContext>(o => o.UseSqlServer(Configuration.GetConnectionString("ProductsAndServicesDB")));

            services.AddTransient<IListingRepository, ListingRepository>();
            services.AddTransient<IListingTypeRepository, ListingTypeRepository>();
            services.AddTransient<ICategoryRepository, CategoryRepository>();

            services.AddTransient<IUserMockRepository, UserMockRepository>();
            services.AddSingleton(typeof(ILoggerMockRepository<>), typeof(LoggerMockRepository<>));

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("ProductsAndServicesOpenApiSpecification",
                    new Microsoft.OpenApi.Models.OpenApiInfo()
                    {
                        Title = "Products and services API",
                        Version = "1",
                        Description = "Pomoću ovog API-ja može da se vrši pregled listinga, dodavanje novih listinga i njihova modifikacija.",
                        Contact = new Microsoft.OpenApi.Models.OpenApiContact
                        {
                            Name = "Stefan Katić",
                            Email = "katicstefan.iis@gmail.com",
                            Url = new Uri("http://www.github.com/katicstefan")
                        },
                        License = new Microsoft.OpenApi.Models.OpenApiLicense
                        {
                            Name = "FTN license",
                            Url = new Uri("http://www.ftn.uns.ac.rs/")
                        },
                        TermsOfService = new Uri("http://www.ftn.uns.ac.rs/productsAndServicesTermsOfService")
                    });
                var xmlComments = $"{ Assembly.GetExecutingAssembly().GetName().Name }.xml";
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments);
                setupAction.IncludeXmlComments(xmlCommentsPath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(setupAction =>
            {
                setupAction.SwaggerEndpoint("/swagger/ProductsAndServicesOpenApiSpecification/swagger.json", "Products and services API");
                setupAction.RoutePrefix = "";
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
