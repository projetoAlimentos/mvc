using Microsoft.AspNetCore.Identity;
using projeto.Models;
using System;
using System.Configuration;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using projeto.Data;
using projeto.Services;
using Microsoft.Extensions.Logging;


namespace projeto.Data
{
    public static class ApplicationDbInitializer
    {
        public static void Initialize(ApplicationDbContext context, IServiceProvider serviceProvider)
        {
            //context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.SaveChanges();
            CreateRoles(serviceProvider).Wait();
        }

        private async static Task CreateRoles(IServiceProvider serviceProvider)
        {
            //adding custom roles
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            string[] roleNames = { "Administrador", "Professor", "Assistente", "Aluno" };
            IdentityResult roleResult;
            foreach (var roleName in roleNames)
            {
                //creating the roles and seeding them to the database
                var roleExist = await RoleManager.RoleExistsAsync(roleName);
                if (!roleExist)
                {
                    roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
                }
            }
            //creating a super user who could maintain the web app
            var poweruser = new ApplicationUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com"
            };
            string UserPassword = "P@ssw0rd";
            var _user = await UserManager.FindByEmailAsync("admin@admin.com");
            if (_user == null)
            {
                var createPowerUser = await UserManager.CreateAsync(poweruser, UserPassword);
                if (createPowerUser.Succeeded)
                {
                    //here we tie the new user to the "Admin" role
                    await UserManager.AddToRoleAsync(poweruser, "Administrador");
                }
            }
        }
    }
}