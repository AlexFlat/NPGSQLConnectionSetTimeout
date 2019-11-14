using System.Threading.Tasks;
using Xunit;

namespace NPGSQLConnectionSetTimeout
{
    public class ConnectionEmbeddedTest : EmbeddedDatabaseTestBase
    {
        string connString = "";
        string connSettings = $"Max Auto Prepare=20;Maximum Pool Size=100;Timeout=60;Command Timeout=120;";

        public ConnectionEmbeddedTest(EmbeddedDatabaseFixture fixtureData) : base(fixtureData)
        {
            connString = $"Server=localhost;Port={EmbeddedDatabase.PgServer.PgPort};User Id=postgres;Password=test;Database=postgres;";
            connString += connSettings;
        }

        [Fact]
        public async Task TestConnectionSetTimeoutEmbedded()
        {
            await Install();
            await connString.ExecuteTestSQL(false, false, true, 1000);
        }

        [Fact]
        public async Task TestConnectionSetTimeoutEmbeddedCommit()
        {
            await Install();
            await connString.ExecuteTestSQL(true, false, false, 1000);
        }

        [Fact]
        public async Task TestConnectionSetTimeoutEmbeddedCommitCloseConn()
        {
            await Install();
            await connString.ExecuteTestSQL(true, false, true, 1000);
        }

        [Fact]
        public async Task TestConnectionSetTimeoutEmbeddedCommitTransaction()
        {
            await Install();
            await connString.ExecuteTestSQL(true, true, false, 1000);
        }

    }
}
