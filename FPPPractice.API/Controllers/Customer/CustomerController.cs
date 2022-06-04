using FPPPractice.API.Controllers.Customer.Dtos;
using FPPPractice.API.Repositories.Customer;
using FPPPractice.API.Utils.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FPPPractice.API.Controllers.Customer
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public CustomerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomerListAsync()
        {
            var result = await _unitOfWork.Repository<ICustomerRepository>().GetCustomerListAsync();

            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> InsertCustomerAsync([FromBody] InsertCustomerDto customer)
        {
            _unitOfWork.BeginTransaction();

            await _unitOfWork.Repository<ICustomerRepository>().InsertCustomerAsync(
                new Repositories.Customer.Models.Dtos.InsertCustomerDto(customer.Name));

            _unitOfWork.Commit();

            return Ok();
        }
    }
}
