using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using VDI.Demo.Configuration;
using VDI.Demo.Web;

namespace VDI.Demo.EntityFrameworkCore
{
    public class NewCommDbContextFactory : IDesignTimeDbContextFactory<NewCommDbContext>
    {
        public NewCommDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<NewCommDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            NewCommDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DemoConsts.ConnectionStringNewCommDbContext));

            return new NewCommDbContext(builder.Options);
        }
    }
}
