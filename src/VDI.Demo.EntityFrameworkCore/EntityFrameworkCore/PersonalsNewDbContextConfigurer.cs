using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace VDI.Demo.EntityFrameworkCore
{
    public class PersonalsNewDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<PersonalsNewDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<PersonalsNewDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
