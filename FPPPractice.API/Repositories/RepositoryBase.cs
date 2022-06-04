using System.Data;

namespace FPPPractice.API.Repositories
{
    public class RepositoryBase : IRepositoryBase
    {
        public IDbTransaction Transaction { get; set; }

        public IDbConnection Connection { get; set; }
    }
}
