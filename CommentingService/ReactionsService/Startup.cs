using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ReactionsService.Data.BlockingMock;
using ReactionsService.Data.FollowingMock;
using ReactionsService.Data.PostMock;
using ReactionsService.Data.Reactions;
using ReactionsService.Data.Type_of_reaction;
using ReactionsService.Models;
using System;
using Microsoft.AspNetCore.Http;
using System.Reflection;
using System.IO;
using ReactionsService.AuthorizationHelper;
using ReactionsService.LoggerHelper;

namespace ReactionsService
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
            }
            ).AddXmlDataContractSerializerFormatters(); //podrska za application/xml

            services.AddScoped<IReactionRepository, ReactionRepository>();//svaki put kada dodje req, napravi novu instancu ovoga
            services.AddScoped<ITypeOfReactionRepository, TypeOfReactionRepository>();
            services.AddScoped<IBlockingMockRepository, BlockingMockRepository>();
            services.AddScoped<IFollowingMockRepository, FollowingMockRepository>();
            services.AddScoped<IPostMockRepository, PostMockRepository>();
            services.AddScoped<IAuthorization, Authorization>();
            services.AddSingleton(typeof(ILoggingMockRepository<>), typeof(LoggingMockRepository<>));

            services.AddSwaggerGen(setupAction =>
            {
                setupAction.SwaggerDoc("ReactionsApiSpecification",
                     new Microsoft.OpenApi.Models.OpenApiInfo() //definise kako se kreira swagger dokument
                     {
                         Title = "Reacting to users' posts API",
                         Version = "1",
                         Description = "API koji omogucava pregled reakcija na objavama korisnika, dodavanje novih reakcija, izmenu, kao i brisanje postojecih reakcija.",
                         Contact = new Microsoft.OpenApi.Models.OpenApiContact
                         {
                             Name = "Nina Kozma",
                             Email = "nina.kozma@gmail.com"
                         },
                         License = new Microsoft.OpenApi.Models.OpenApiLicense
                         {
                             Name = "FTN licenca"
                         }
                     });

                var xmlComments = $"{Assembly.GetExecutingAssembly().GetName().Name }.xml"; //refleksija
                var xmlCommentsPath = Path.Combine(AppContext.BaseDirectory, xmlComments); //spaja vise stringova

                setupAction.IncludeXmlComments(xmlCommentsPath); //da bi swagger mogao citati xml komenatare

            });


            services.AddDbContext<ContextDB>(o => o.UseSqlServer(Configuration.GetConnectionString("Database")));
        
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else //Ukoliko se nalazimo u Production modu postavljamo default poruku za greške koje nastaju na servisu
            {
                app.UseExceptionHandler(appBuilder =>
                {
                    appBuilder.Run(async context =>
                    {
                        context.Response.StatusCode = 500;
                        await context.Response.WriteAsync("Unexpected error, please try again later.");
                    });
                });
            }

            app.UseSwagger();
            app.UseSwaggerUI(setupAction => {
                setupAction.SwaggerEndpoint("/swagger/ReactionsApiSpecification/swagger.json", "Reacting to users' posts API");
                setupAction.RoutePrefix = ""; //odmah mi otvori swagger dokumentaciju kada pokrenem servis u browseru
            });

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
