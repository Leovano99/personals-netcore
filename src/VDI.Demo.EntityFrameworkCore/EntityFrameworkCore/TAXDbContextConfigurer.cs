using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace VDI.Demo.EntityFrameworkCore
{
    public static class TAXDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<TAXDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<TAXDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
