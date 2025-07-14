using Oracle.ManagedDataAccess.Client;
using System.Data;
using IAMNC0000S03.Business.Interfaces;

namespace IAMNC0000S03.Infrastructure
{
    public class OracleDbContext : IOracleDbContext
    {
        private readonly string _connectionString;

        public OracleDbContext(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("OracleDb") ?? throw new ArgumentNullException(nameof(config), "OracleDb connection string is missing in configyration.");
        }
        public IDbConnection CreateConnection()
        {
            return new OracleConnection(_connectionString);
        }
    }
}