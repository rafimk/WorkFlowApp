using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Work_Flow_App.Data;
using Work_Flow_App.Domain;
using Work_Flow_App.Services.Interfaces;

namespace Work_Flow_App.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void ApplyMigrations(this IApplicationBuilder app)
        {
            using var services = app.ApplicationServices.CreateScope();

            var dbContext = services.ServiceProvider.GetService<ApplicationDbContext>();
            var userManager = services.ServiceProvider.GetService<UserManager<User>>();

            dbContext.Database.Migrate();
        }
    }
}
