using System.Data;

namespace IAMNC0000S03.Business.Interfaces
{
    public interface IOracleDbContext
    {
        IDbConnection CreateConnection();
    }
}