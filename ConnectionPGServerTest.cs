using System.Threading.Tasks;
using Xunit;

namespace NPGSQLConnectionSetTimeout
{
    public class ConnectionPGServerTest 
    {
        string connString = $"Server=localhost;Port=5432;Database=postgres;User Id=postgres;Password=1234;";
        string connSettings = $"Max Auto Prepare=20;Maximum Pool Size=100;Timeout=60;Command Timeout=120;";

        public ConnectionPGServerTest()
        {
            connString += connSettings;
        }

        [Fact]
        public async Task TestConnectionSetTimeoutServer()
        {
            await connString.ExecuteTestSQL(false, false, true, 1000);
        }

        [Fact]
        public async Task TestConnectionSetTimeoutServerCommit()
        {
            await connString.ExecuteTestSQL(true, false, false, 1000);
        }

        [Fact]
        public async Task TestConnectionSetTimeoutServerCommitConnClose()
        {
            await connString.ExecuteTestSQL(true, false, true, 1000);
        }

        [Fact]
        public async Task TestConnectionSetTimeoutServerCommitTransaction()
        {
            await connString.ExecuteTestSQL(true, true, false, 1000);
        }
    }
}
