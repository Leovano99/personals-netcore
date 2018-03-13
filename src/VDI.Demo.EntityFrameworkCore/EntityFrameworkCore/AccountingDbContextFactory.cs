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
    public class AccountingDbContextFactory : IDesignTimeDbContextFactory<AccountingDbContext>
    {
        public AccountingDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<AccountingDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            AccountingDbContextConfigurer.Configure(builder, configuration.GetConnectionString(DemoConsts.ConnectionStringAccountingDbContext));

            return new AccountingDbContext(builder.Options);
        }
    }
}
