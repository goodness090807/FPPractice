using Dapper;
using FPPPractice.API.Repositories.Customer.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FPPPractice.API.Repositories.Customer
{
    public class CustomerRepository : RepositoryBase, ICustomerRepository
    {
        public async Task<IEnumerable<CustomerDto>> GetCustomerListAsync()
        {
            var strSQL = @"
                SELECT * FROM dbo.Customers
                ORDER BY Id
                OFFSET 0 ROWS
                FETCH NEXT 10 ROWS ONLY";

            return await Connection.QueryAsync<CustomerDto>(strSQL, transaction: Transaction);
        }

        public async Task InsertCustomerAsync(InsertCustomerDto customer)
        {
            var strSQL = @"
                INSERT INTO dbo.Customers
                    (Name)
                VALUES
                    (@Name)";

            await Connection.ExecuteAsync(strSQL, customer, transaction: Transaction);
        }
    }
}
