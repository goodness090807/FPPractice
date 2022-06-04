using FPPPractice.API.Repositories.Customer.Models.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FPPPractice.API.Repositories.Customer
{
    public interface ICustomerRepository : IRepositoryBase
    {
        Task<IEnumerable<CustomerDto>> GetCustomerListAsync();
        Task InsertCustomerAsync(InsertCustomerDto customer);
    }
}
