using Npgsql;
using System.Threading.Tasks;

namespace NPGSQLConnectionSetTimeout
{
    public static class QueryHelper
    {
        private static async Task<int> Execute(string sql, string connString, bool useTX, bool closeConn)
        {
            using (var conn = new NpgsqlConnection(connString))
            {
                try
                {
                    conn.Open();
                    if (useTX)
                    {
                        using (var tx = conn.BeginTransaction())
                        {
                            System.Diagnostics.Trace.WriteLine($"{sql} - Started");
                            //return await conn.QueryAsync(sql);
                            var cmd = new NpgsqlCommand(sql, conn, tx);
                            var result = await cmd.ExecuteReaderAsync();
                            return result.RecordsAffected;
                        }
                    }
                    else
                    {
                        System.Diagnostics.Trace.WriteLine($"{sql} - Started");
                        var cmd = new NpgsqlCommand(sql, conn);
                        var result = await cmd.ExecuteReaderAsync();
                        return result.RecordsAffected;
                    }
                }
                finally
                {
                    if(closeConn)
                    {
                        if (conn.State == System.Data.ConnectionState.Open)
                        {
                            conn.Close();
                        }
                    }
                }
            }
        }

        public static async Task ExecuteTestSQL(this string connString, bool useCommit, bool useTX, bool closeConn, int count)
        {
            var sql = "set statement_timeout = 120000; ";
            if (useCommit)
            {
                sql = sql + "commit; ";
            }
            for (int i = 0; i < count; i++)
            {
                System.Diagnostics.Trace.WriteLine($"Test {i} - Started");
                await Execute(sql, connString, useTX, closeConn);
                System.Diagnostics.Trace.WriteLine($"Test {i} - Completed");
            }
        }

    }
}
