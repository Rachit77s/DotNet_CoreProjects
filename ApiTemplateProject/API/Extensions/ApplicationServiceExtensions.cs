using API.Data;
using API.Interfaces;
using API.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Extensions
{
    //Rachit
    public static class ApplicationServiceExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddSingleton<IUser, UserService>();

            // Download the Microsoft.EntityFrameworkCore from the Nuget Package Manager
            // DataContext is the class that is inheriting the DbContext class of EF Core
            services.AddDbContextPool<DataContext>(options =>
            {
                // Download the Microsoft.EntityFrameworkCore.SqlServer from the Nuget Package Manager
                options.UseSqlServer(config.GetConnectionString("SampleAppDBConnection"));
            });

            return services;
        }
    }
}
