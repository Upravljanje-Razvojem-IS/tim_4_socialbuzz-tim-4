using BlockUsersService.AuthHelper;
using BlockUsersService.Data.Blocking;
using BlockUsersService.Data.FollowingMock;
using BlockUsersService.Data.UserMock;
using BlockUsersService.Entities;
using BlockUsersService.LoggerHelper;
using BlockUsersService.Services;
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

namespace BlockUsersService
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
            services.AddDbContext<BlockUserDBContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("BlockUserDBContextConnectonString"));
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IBlockingRepository, BlockingRepository>();
            services.AddScoped<IBlockingService, BlockingService>();
            services.AddScoped<IUserMockRepository, UserMockRepository>();
            services.AddScoped<IAuthHelper, AuthHelperr>();
            services.AddScoped<IFollowingMockRepository, FollowingMockRepository>();
            services.AddSingleton<ILogger, Logger>();

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("BlockUsersApiSpecification",
                     new Microsoft.OpenApi.Models.OpenApiInfo()
                     {
                         Title = "Blocking users API",
                         Version = "1",
                         Description = "Servis koji omogucava pregled i pretragu blokiranja korisnika, nova blokiranja, izmenu, " +
                         "brisanje postojecih blokiranja i prikaz liste blokiranih korisnika i korisnika koji blokiraju.",
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
                setupAction.SwaggerEndpoint("/swagger/BlockUsersApiSpecification/swagger.json", "Blocking users API");
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
