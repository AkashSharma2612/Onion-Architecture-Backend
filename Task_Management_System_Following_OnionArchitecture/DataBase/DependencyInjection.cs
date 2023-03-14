using Application.Interface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence
{
    public static class DependencyInjection
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
            {

            services.AddEntityFrameworkSqlServer().AddDbContext<ApplicationDbContext>
                           (option => option.UseSqlServer(configuration.GetConnectionString("constr"), b => b.MigrationsAssembly
                            ("Task_Management_System")));
            services.AddScoped<IApplicationDbContext>(provider => provider.GetService<ApplicationDbContext>());
            }
        }
}
