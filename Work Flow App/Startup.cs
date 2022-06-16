using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Data;
using Work_Flow_App.Domain;
using Work_Flow_App.Extensions;
using Work_Flow_App.Infrastructure;
using Work_Flow_App.Infrastructure.Interfaces;
using Work_Flow_App.Services;
using Work_Flow_App.Services.Interfaces;

namespace Work_Flow_App
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
            services.AddDbContext<ApplicationDbContext>
            (
                options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services
               .AddIdentity<User, Role>(options =>
               {
                   options.Password.RequiredLength = 4;
                   options.Password.RequireDigit = false;
                   options.Password.RequireLowercase = false;
                   options.Password.RequireNonAlphanumeric = false;
                   options.Password.RequireUppercase = false;
               })
               .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddHttpContextAccessor();

            // Dependency Injection 
            services.AddAutoMapper(typeof(Startup).Assembly);
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IAttachmentService, AttachmentService>();
            services.AddScoped<IIdentityService, IdentityService>();
            services.AddScoped<IRequestService, RequestService>();
            services.AddScoped<IRequestWorkFlowService, RequestWorkFlowService>();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.ApplyMigrations();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
