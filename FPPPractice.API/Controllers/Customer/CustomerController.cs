using FPPPractice.API.Controllers.Customer.Dtos;
using FPPPractice.API.Exceptions;
using FPPPractice.API.Repositories.Customer;
using FPPPractice.API.Utils.UnitOfWork;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace FPPPractice.API.Controllers.Customer
{
    public class CustomerController : BaseController
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


            await _unitOfWork.Repository<ICustomerRepository>().InsertCustomerAsync(
                new Repositories.Customer.Models.Dtos.InsertCustomerDto(customer.Name + "2"));

            throw new BadRequestException("測試錯誤");
            // 因為錯誤了，所以沒有Commit，會自動被Rollback
            _unitOfWork.Commit();
            return Ok();
        }
    }
}
