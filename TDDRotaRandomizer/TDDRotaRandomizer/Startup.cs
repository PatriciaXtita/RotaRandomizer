using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using RotaRandomizer.Domain.Repositories;
using RotaRandomizer.Domain.Services;
using RotaRandomizer.Persistence.Contexts;
using RotaRandomizer.Persistence.Repositories;
using RotaRandomizer.Services;

namespace RotaRandomizer
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
            services.AddControllers();
            var connection = Configuration["MySQLConnection:MySqlConnectionString"];
            services.AddDbContext<RotaDbContext>(options =>
                options.UseMySql(connection)
            );
            services.AddMvc();

            services.AddScoped<IRotaRepository, RotaRepository>();
            services.AddScoped<IRotaService, RotaService>();

            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IEmployeeService, EmployeeService>();

            services.AddScoped<IShiftRepository, ShiftRepository>();
            services.AddScoped<IShiftService, ShiftService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
