using System.Threading.Tasks;
using Xunit;

namespace NPGSQLConnectionSetTimeout
{
    [Collection(EmbeddedDatabaseCollection.CollectionName)]
    public abstract class EmbeddedDatabaseTestBase
    {
        protected string TABLENAME = "test";

        protected EmbeddedDatabaseFixture EmbeddedDatabase { get; private set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="fixtureData"></param>
        public EmbeddedDatabaseTestBase(EmbeddedDatabaseFixture fixtureData)
        {
            EmbeddedDatabase = fixtureData;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        protected async Task Install()
        {
            //await Uninstall();
            // Won't be called in parallel - The base class has a Collection attribute that will prevent 
            // any parallel run, and tests in the same class cannot run in parallel, so no need to lock here. 
            // The database fixture is shared between a class, so check whether or not it's been installed yet
            // before installing, so that we don't install multiple times. 
            // Make sure any tests in the same class don't interfere with each other's test data!
            if (!EmbeddedDatabase.IsInstalled)
            {
                //System.Diagnostics.Trace.WriteLine($"{nameof(Install)}");
                //var connStringBuilder = new NpgsqlConnectionStringBuilder(GetConnectionString());
                //try
                //{
                //    var sql = $"create database \"{connStringBuilder.Database}\";";
                //    await Execute(sql, connString: GetConnectionStringPostgres());
                //}
                //catch (Exception ex)
                //{
                //    System.Diagnostics.Trace.WriteLine(ex.Message);
                //}
                //await Execute($"create table if not exists {TABLENAME} (col1 text, col2 text);");
                EmbeddedDatabase.IsInstalled = true;
            }
        }

    }
}
