using BlockUsersService.AuthHelper;
using BlockUsersService.Data.Blocking;
using BlockUsersService.Data.FollowingMock;
using BlockUsersService.Data.UserMock;
using BlockUsersService.Entities;
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
using System.Linq;
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
                //options.UseLazyLoadingProxies();
                options.UseSqlServer(Configuration.GetConnectionString("BlockUserDBContextConnectonString"));
            });

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.AddScoped<IBlockingRepository, BlockingRepository>();
            services.AddScoped<IBlockingService, BlockingService>();
            services.AddScoped<IUserMockRepository, UserMockRepository>();
            services.AddScoped<IAuthHelper, AuthHelperr>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
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
