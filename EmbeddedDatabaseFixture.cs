using MysticMind.PostgresEmbed;
using System;
using System.Collections.Generic;

namespace NPGSQLConnectionSetTimeout
{
    public class EmbeddedDatabaseFixture : IDisposable
    {
        public PgServer PgServer { get; set; }
        public bool IsInstalled { get; set; } = false;
        
        private const string PostgreSQLVersion = "10.5.1.0";
        private const int Port = 6002;

        public EmbeddedDatabaseFixture()
        {
            var serverParams = new Dictionary<string, string>();
            PgServer = new PgServer(PostgreSQLVersion, 
                port: Port,
                clearInstanceDirOnStop: true,
                addLocalUserAccessPermission: true,
                instanceId: Guid.Empty,                
                pgServerParams: serverParams);
            PgServer.Start();
        }

        public void Dispose()
        {
            try
            {
                if (PgServer != null)
                {
                    PgServer.Stop();
                }
            }
            catch (Exception)
            { }
        }
    }
}
