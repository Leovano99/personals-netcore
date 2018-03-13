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
    public class PropertySystemDbContextFactory : IDesignTimeDbContextFactory<PropertySystemDbContext>
    {
        public PropertySystemDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PropertySystemDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            PropertySystemDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DemoConsts.ConnectionStringPropertySystemDbContext));

            return new PropertySystemDbContext(builder.Options);
        }
    }
}
