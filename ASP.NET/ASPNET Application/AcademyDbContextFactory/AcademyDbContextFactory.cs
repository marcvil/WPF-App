using A4.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ASPNET_Application.DbContextFactory
{

    public class AcademyDbContextFactory : IDesignTimeDbContextFactory<AcademyDbContext>
    {
        public AcademyDbContext CreateDbContext(string[] args)
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);


            var configuration = builder.Build();
            var dbConnection = configuration.GetConnectionString("AcademyDbContext");

            var optionsBuilder = new DbContextOptionsBuilder<AcademyDbContext>();
            optionsBuilder.UseSqlServer(dbConnection, x => x.MigrationsAssembly("ASPNET Application"));

            return new AcademyDbContext(optionsBuilder.Options);
        }
    }

}
