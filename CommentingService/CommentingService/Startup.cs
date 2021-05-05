namespace CommentingService
{
    using CommentingService.AuthorizationHelper;
    using CommentingService.Data;
    using CommentingService.Data.BlockingMock;
    using CommentingService.Data.Commenting;
    using CommentingService.Data.FolllowingMock;
    using CommentingService.Data.PostMock;
    using CommentingService.LoggerHelper;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using System.Text;

    public class Startup
    {
       public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(setup =>
            {
                setup.ReturnHttpNotAcceptable = true;
            }
            ).AddXmlDataContractSerializerFormatters(); //Podrska za XML (application/json + application/xml)
            
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); //pretrazi ceo domen servisa u potrazi za konfiguracijama automapera, koje se nazivaju PROFILI

            services.AddScoped<IAuthorization, Authorization>();
            services.AddScoped<ICommentingRepository, CommentingRepository>(); //svaki put kada dodje req, napravi novu instancu ovoga
            services.AddScoped<IBlockingMockRepository, BlockingMockRepository>(); 
            services.AddScoped<IFollowingMockRepository, FollowingMockRepository>(); 
            services.AddScoped<IPostMockRepository, PostMockRepository>();
            services.AddSingleton(typeof(ILoggingMockRepository<>), typeof(LoggingMockRepository<>));

            services.AddDbContext<ContextDB>(o => o.UseSqlServer(Configuration.GetConnectionString("Database")));

            services.AddSwaggerGen(setupAction =>
            {            
                setupAction.SwaggerDoc("CommentingApiSpecification",
                     new Microsoft.OpenApi.Models.OpenApiInfo() //definise kako se kreira swagger dokument
                     {
                         Title = "Commenting users' posts API",
                         Version = "1",
                         Description = "API koji omogucava pregled komentara na objavama korisnika, dodavanje novih komenatara, izmenu, kao i brisanje postojecih komenatara.",
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

                /*setupAction.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme()
                {
                    Type = SecuritySchemeType.ApiKey,
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "API KeyJWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                    Scheme = "ApiKeyScheme"
                });
                var key = new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Authorization"
                    },
                    In = ParameterLocation.Header
                };

                var requirement = new OpenApiSecurityRequirement
                {
                    {key, new List<string>() }
                };
                setupAction.AddSecurityRequirement(requirement);*/
            });

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

            
            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(setupAction => {
                setupAction.SwaggerEndpoint("/swagger/CommentingApiSpecification/swagger.json", "Commenting users' posts API");
                setupAction.RoutePrefix = ""; //odmah mi otvori swagger dokumentaciju kada pokrenem servis u browseru
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
