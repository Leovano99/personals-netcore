using Abp.Dependency;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VDI.Demo.SqlExecuter;
using System.Collections.Generic;
using System.Data.SqlClient;
using Dapper;
using System.Linq;
using System.Data;

namespace VDI.Demo.EntityFrameworkCore
{
    public class SqlExecuter : ISqlExecuter, ITransientDependency
    {
        private readonly IDbContextProvider<PersonalsNewDbContext> _dbContextPersonals;
        private readonly IDbContextProvider<DemoDbContext> _dbContextDemo;

        public SqlExecuter(
            IDbContextProvider<PersonalsNewDbContext> dbContextPersonals,
            IDbContextProvider<DemoDbContext> dbContextDemo
            )
        {
            _dbContextPersonals = dbContextPersonals;
            _dbContextDemo = dbContextDemo;

        }

        public int Execute(string sql, params object[] parameters)
        {
            return _dbContextPersonals.GetDbContext().Database.ExecuteSqlCommand(sql, parameters);
        }

        public IReadOnlyList<T> GetFromEngin3<T>(string sql, object parameters = null, CommandType? commandType = null)
        {
            var tempDbConn = _dbContextDemo.GetDbContext();
            string tempConnStr = tempDbConn.Database.GetDbConnection().ConnectionString;

            using (var conn = new SqlConnection(tempConnStr))
            {
                return conn.Query<T>(sql, parameters).ToList();
            }
        }

        public IReadOnlyList<T> GetFromPersonals<T>(string sql, object parameters = null, CommandType? commandType = null)
        {
            var tempDbConn = _dbContextPersonals.GetDbContext();
            string tempConnStr = tempDbConn.Database.GetDbConnection().ConnectionString;

            using (var conn = new SqlConnection(tempConnStr))
            {
                return conn.Query<T>(sql, parameters).ToList();
            }
        }
    }
}
