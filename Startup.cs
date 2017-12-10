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
using projeto.Data;
using projeto.Models;
using projeto.Services;
using Microsoft.Extensions.Logging;

namespace projeto
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
          options.UseMySQL(Configuration.GetConnectionString("SampleyConnection")));

      services.AddIdentity<ApplicationUser, IdentityRole>()
          .AddEntityFrameworkStores<ApplicationDbContext>()
          .AddDefaultTokenProviders();

      services.Configure<IdentityOptions>(options =>
      {
              // Password settings
              options.Password.RequireDigit = false;
        options.Password.RequiredLength = 8;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequireLowercase = false;
        options.Password.RequiredUniqueChars = 4;

              // Lockout settings
              options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);
        options.Lockout.MaxFailedAccessAttempts = 10;
        options.Lockout.AllowedForNewUsers = true;

              // User settings
              options.User.RequireUniqueEmail = true;
      });

      services.ConfigureApplicationCookie(options =>
      {
              // Cookie settings
              options.Cookie.HttpOnly = true;
        options.Cookie.Expiration = TimeSpan.FromDays(150);
        options.LoginPath = "/Account/Login"; // If the LoginPath is not set here, ASP.NET Core will default to /Account/Login
              options.LogoutPath = "/Account/Logout"; // If the LogoutPath is not set here, ASP.NET Core will default to /Account/Logout
              options.AccessDeniedPath = "/Account/AccessDenied"; // If the AccessDeniedPath is not set here, ASP.NET Core will default to /Account/AccessDenied
              options.SlidingExpiration = true;
      });


      // Add application services.
      services.AddTransient<IEmailSender, EmailSender>();

      services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IHostingEnvironment env, 
        ILoggerFactory loggerFactory, IServiceProvider serviceProvider)
    {
      if (env.IsDevelopment())
      {
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
                  template: "{controller=Home}/{action=Index}/{id?}");
      });

      CreateRoles(serviceProvider).Wait();
    }

    private async Task CreateRoles(IServiceProvider serviceProvider)
    {
      //adding custom roles
      var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
      var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
      string[] roleNames = { "Admin", "Manager", "Member" };
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
        UserName = Configuration.GetSection("UserSettings")["UserEmail"],
        Email = Configuration.GetSection("UserSettings")["UserEmail"]
      };
      string UserPassword = Configuration.GetSection("UserSettings")["UserPassword"];
      var _user = await UserManager.FindByEmailAsync(Configuration.GetSection("UserSettings")["UserEmail"]);
      if (_user == null)
      {
        var createPowerUser = await UserManager.CreateAsync(poweruser, UserPassword);
        if (createPowerUser.Succeeded)
        {
          //here we tie the new user to the "Admin" role 
          await UserManager.AddToRoleAsync(poweruser, "Admin");
        }
      }
    }
  }
}
