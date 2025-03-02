using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RegisterLogin.Application.Services.Implementation;
using RegisterLogin.Application.Services.Interfaces;
using RegisterLogin.Infrastructure.Repositories.Implementation;
using RegisterLogin.Infrastructure.Repositories.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
//in package bayad download koni chon omadi ye laye dg
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegisterLogin.IOC.Configurations
{
    public static class DiContainer
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginService, LoginService>();
            services.AddScoped<ILoginRepository, LoginRepository>();
        }
        //baraye in joda kardam va configure ra dadam ke momkene bazi az tanzimat
        //az file appsetting.json khande beshe injoori tamiz tare
        public static void RegisterAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
            .AddCookie(options =>
            {
                options.LoginPath = "/Login/Login";
                options.LogoutPath = "/Login/Login";
                options.ExpireTimeSpan = TimeSpan.FromSeconds(43200);
            });
        }
    }
}
