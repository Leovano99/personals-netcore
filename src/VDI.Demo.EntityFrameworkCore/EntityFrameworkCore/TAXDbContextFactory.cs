using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using VDI.Demo.Configuration;
using VDI.Demo.Web;

namespace VDI.Demo.EntityFrameworkCore
{
    public class TAXDbContextFactory : IDesignTimeDbContextFactory<TAXDbContext>
    {
        public TAXDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TAXDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            TAXDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DemoConsts.ConnectionStringTAXDbContext));

            return new TAXDbContext(builder.Options);
        }
    }
}
