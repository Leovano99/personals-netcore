using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace VDI.Demo.EntityFrameworkCore
{
    public static class AccountingDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AccountingDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AccountingDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
