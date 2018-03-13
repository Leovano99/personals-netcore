using System.Data.SqlClient;
using Shouldly;
using Xunit;

namespace VDI.Demo.Tests.General
{
    public class ConnectionString_Tests
    {
        [Fact]
        public void SqlConnectionStringBuilder_Test()
        {
            var csb = new SqlConnectionStringBuilder("Server=localhost; Database=Demo; Trusted_Connection=True;");
            csb["Database"].ShouldBe("Demo");
        }
    }
}
