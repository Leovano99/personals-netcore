using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using VDI.Demo.Configuration;
using VDI.Demo.Web;

namespace VDI.Demo.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class DemoDbContextFactory : IDesignTimeDbContextFactory<DemoDbContext>
    {
        public DemoDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<DemoDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            DemoDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DemoConsts.ConnectionStringName));

            return new DemoDbContext(builder.Options);
        }
    }

}