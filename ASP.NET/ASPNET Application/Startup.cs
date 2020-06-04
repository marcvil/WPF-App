using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.DAL;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using WpfApp1.AppBootsraper;

namespace ASPNET_Application
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
        
            services.AddDbContext<AcademyDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("AcademyDbContext"),b => b.MigrationsAssembly("AcademyASPNET")));
           
            var bootstrapper = new Bootstraper();
            bootstrapper.Init();

            services.AddControllers();
        }

        /*private static Func<AcademyDbContext> GetDbConstructor(string dbConnection)
        {
            var optionsBuilder = new DbContextOptionsBuilder<AcademyDbContext>();
            optionsBuilder.UseSqlServer(dbConnection, b => b.MigrationsAssembly("AcademyASPNET"));

            var dbContextConst = new Func<AcademyDbContext>(() =>
            {
                return new AcademyDbContext(optionsBuilder.Options);
            });
            return dbContextConst;
        }
        */

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                     name: "default.html",
                    pattern: "{controller=Home}/{action=Main}/{id?}");
            });
        }
    }
}
