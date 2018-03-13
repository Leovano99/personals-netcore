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
    public class PersonalsNewDbContextFactory : IDesignTimeDbContextFactory<PersonalsNewDbContext>
    {
        public PersonalsNewDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<PersonalsNewDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            PersonalsNewDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DemoConsts.ConnectionStringPersonalsNewDbContext));

            return new PersonalsNewDbContext(builder.Options);
        }
    }
}
