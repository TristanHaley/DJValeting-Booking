using System;
using System.Reflection;
using Application.Handlers.Bookings.Commands;
using Application.Infrastructure;
using Application.Infrastructure.AutoMapper;
using Application.Interfaces;
using DjValetingApi;
using FluentValidation.AspNetCore;
using Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using NSwag;
using Persistence;
using Persistence.Seeding;
using Presentation.Filters.Filters;

namespace Presentation
{
    public class Startup
    {
        #region Constructors

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        #endregion

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                logger.LogInformation("In 'Development' environment");
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            // NSwag / Swagger Setup
            const string specificationPath = "/api/specification.json";
            app.UseOpenApi(settings =>
            {
                settings.Path         = specificationPath;
                settings.DocumentName = "DjValeting Swagger";
            });
            app.UseSwaggerUi3(settings =>
            {
                settings.Path         = "/api";
                settings.DocumentPath = specificationPath;
            });
            app.UseReDoc(settings =>
            {
                settings.Path         = "/redoc";
                settings.DocumentPath = specificationPath;
            });

            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
                endpoints.MapControllers();
            });
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            // Blazor
            services.AddRazorPages();
            services.AddServerSideBlazor()
                    .AddCircuitOptions(options => options.DetailedErrors = true);

            // Database seeder(s)
            services.AddTransient<ContextSeeder, DatabaseSeeder>();
            
            // GUI Framework
            services.AddMudServices();
            
            // Application Transients
            services.AddTransient<IServerDateTime, ServerDateTime>();

            // AddTransient password hashing service

            // API Clients
            services.AddHttpClient<IBookingClient, BookingClient>(client => client.BaseAddress = new Uri(Configuration["BaseUrl"]));

            // Add MediatR
            services.AddMediatR(typeof(CreateBookingCommandHandler).GetTypeInfo().Assembly);
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

            // Cookies
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded    = _ => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            // AutoMapper
            services.AddAutoMapper(mapperConfig => mapperConfig.AddMaps(typeof(AutoMapperProfile)));

            // Add DbContext using SQLite Provider
            services.AddDbContext<IDjValetingContext, DjValetingContext>(options =>
            {
                options.UseSqlite(Configuration.GetConnectionString("DjValetingConnection"), options => options.CommandTimeout(60));
#if DEBUG
                options.EnableSensitiveDataLogging();
                options.EnableDetailedErrors();
#endif
            });

            // Controllers
            services.AddControllersWithViews();

            // MVC
            services.AddMvc(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)))
                    .SetCompatibilityVersion(CompatibilityVersion.Latest)
                    .AddViewLocalization()
                    .AddFluentValidation(fluentValidate => fluentValidate.RegisterValidatorsFromAssemblyContaining<CreateBookingCommandValidator>());

            // Swagger
            services.AddOpenApiDocument(document =>
            {
                document.DocumentName = "DJ Valeting";
                document.PostProcess = post =>
                {
                    post.Info.Title = "DJ Valeting API";
                    post.Info.Contact = new OpenApiContact
                    {
                        Name  = "Tristan Haley",
                        Email = "TristanHaley071@gmail.com"
                    };

                    post.Info.Version     = "v1";
                    post.Info.Description = "RESTful API definitions for DJ Valeting";
                };
            });
        }
    }
}