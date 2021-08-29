using LoggerService.AuthHelper;
using LoggerService.Data;
using LoggerService.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LoggerService
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
            services.AddDbContext<LoggerDBContext>(options =>
            {

                options.UseSqlServer(Configuration.GetConnectionString("LoggerDBContextConnectonString"));

            });
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<ILogRepository, LogRepository>();
            services.AddScoped<IAuthHelperr, AuthHelperr>();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("LoggerApiSpecification",
                     new Microsoft.OpenApi.Models.OpenApiInfo()
                     {
                         Title = "Logger API",
                         Version = "1",
                         Description = "Servis koji omogucava logovanje akcija(endpoint-a). Informacije dobija od drugih servisa, putem http zahteva, " +
                         "sa kojim je povezan, upisuje ih u bazu i ispisuje ih na konzoli. " +
                         "Postoji mogucnost iscitavanja sadrzaja iz baze ukoliko je korisnik autentifikovan.",
                         Contact = new Microsoft.OpenApi.Models.OpenApiContact
                         {
                             Name = "Mina Topalovic",
                             Email = "minatopalovic98@gmail.com"
                         },
                         License = new Microsoft.OpenApi.Models.OpenApiLicense
                         {
                             Name = "FTN licenca"
                         }
                     });

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name }.xml";
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
                setupAction.SwaggerEndpoint("/swagger/LoggerApiSpecification/swagger.json", "Logger API");
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
