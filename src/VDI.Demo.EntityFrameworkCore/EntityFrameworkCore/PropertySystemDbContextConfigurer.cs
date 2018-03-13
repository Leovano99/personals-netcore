using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace VDI.Demo.EntityFrameworkCore
{
    public static class PropertySystemDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<PropertySystemDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<PropertySystemDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
