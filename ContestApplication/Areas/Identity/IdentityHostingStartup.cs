using System;
using ContestApplication.Areas.Identity.Data;
using ContestApplication.Data;
using ContestApplication.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ContestApplication.Areas.Identity.IdentityHostingStartup))]
namespace ContestApplication.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ContestApplicationContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ContestApplicationContextConnection")));

                services.AddDefaultIdentity<ContestApplicationUser>(options => {
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                    options.SignIn.RequireConfirmedAccount = true;
                    options.SignIn.RequireConfirmedEmail = true;
                    options.User.RequireUniqueEmail = true;
                })
                .AddEntityFrameworkStores<ContestApplicationContext>();

            });
        }
    }
}