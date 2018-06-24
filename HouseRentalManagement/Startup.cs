using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using HouseRentalManagement.Data;
using HouseRentalManagement.Models;
using HouseRentalManagement.Services;
using HouseRentalManagement.Services.Interfaces;
using HouseRentalManagement.Data.Interface;
using HouseRentalManagement.Config;

namespace HouseRentalManagement
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<ApplicationUser, IdentityRole>(o => {
                // lockout settings
                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
                o.Lockout.MaxFailedAccessAttempts = 3;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            // Add application services.
            AddServices(services);

            services.AddMvc();

            // configuration
            SystemSettings(services);
        }

        public void AddServices(IServiceCollection services)
        {
            // services
            services.AddScoped<ILoginService, LoginService>();
            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IHouseService, HouseService>();
            services.AddScoped<IFacilityService, FacilityService>();
            services.AddScoped<ITenantService, TenantService>();
            services.AddScoped<IFeaturedPhotoService, FeaturedPhotoService>();
            services.AddScoped<IFrontendService, FrontendService>();

            // business
            services.AddScoped<IAccessCodeRepository, AccessCodeRepository>();
            services.AddScoped<IHouseRepository, HouseRepository>();
            services.AddScoped<IFacilityRepository, FacilityRepository>();
            services.AddScoped<IAmenityRepository, AmenityRepository>();
            services.AddScoped<IGettingAroundRepository, GettingAroundRepository>();
            services.AddScoped<IHouseImageRepository, HouseImageRepository>();
            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IFeaturedPhotoRepository, FeaturedPhotoRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=House}/{action=Index}/{id?}");
            });
        }

        public void SystemSettings(IServiceCollection services)
        {
            services.Configure<ImageOptions>(Configuration.GetSection(key: "Image"));
        }
    }
}
