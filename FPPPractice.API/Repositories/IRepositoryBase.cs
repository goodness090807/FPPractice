using System.Data;

namespace FPPPractice.API.Repositories
{
    public interface IRepositoryBase
    {
        IDbTransaction Transaction { get; set; }
        IDbConnection Connection { get; set; }
    }
}
