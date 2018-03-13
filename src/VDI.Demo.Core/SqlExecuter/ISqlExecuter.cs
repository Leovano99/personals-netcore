using System;
using System.Collections.Generic;
using System.Data;

namespace VDI.Demo.SqlExecuter
{
    public interface ISqlExecuter
    {
        int Execute(string sql, params object[] parameters);

        IReadOnlyList<T> GetFromPersonals<T>(string sql, object parameters = null, CommandType? commandType = null);
        IReadOnlyList<T> GetFromEngin3<T>(string sql, object parameters = null, CommandType? commandType = null);
    }
}
