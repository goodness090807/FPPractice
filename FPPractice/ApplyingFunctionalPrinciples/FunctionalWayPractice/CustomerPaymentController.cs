using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.FunctionalWayPractice
{
    public class CustomerPaymentController
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IPaymentService _paymentService;

        public CustomerPaymentController(ICustomerRepository customerRepository, IPaymentService paymentService)
        {
            _customerRepository = customerRepository;
            _paymentService = paymentService;
        }

        /// <summary>
        /// 付款
        /// </summary>
        /// <param name="customerId">這個可能是從驗證帶進來的</param>
        /// <param name="moneyAmount">付款金額</param>
        public void Pay(int customerId, decimal moneyAmount)
        {
            var moneyToCharge = MoneyToCharge.Create(moneyAmount);
            if (moneyToCharge.IsFailure)
            {
                Console.WriteLine(moneyToCharge.ErrorMessage);
                return;
            }

            // 我們這邊可以自己擴充判斷是否Null，這樣就可以做到透過Result來接了
            var customer = _customerRepository.GetCustomerById(customerId).IfNull("找不到資源");
            if(customer.IsFailure)
            {
                Console.WriteLine(customer.ErrorMessage);
                return;
            }

            // 透過封裝來做到改變屬性，而不是直接改變屬性
            customer.Value.AddBalance(moneyToCharge.Value);

            // 盡量用Result封裝，不要用try catch
            var result = _paymentService.ChargePayment(customer.Value.Id, moneyToCharge.Value);

            if (result.IsFailure)
            {
                Console.WriteLine(result.ErrorMessage);
                return;
            }

            result = _customerRepository.SaveChange(customer.Value);
            if (result.IsFailure)
            {
                // 假裝有要做Rollback的機制
                _customerRepository.RollbackTransaction();
                Console.WriteLine(result.ErrorMessage);
                return;
            }

            Console.WriteLine("付款成功");
        }

        /// <summary>
        /// 透過鐵軌式做到連結的方式
        /// </summary>
        /// <param name="customerId">這個可能是從驗證帶進來的</param>
        /// <param name="moneyAmount">付款金額</param>
        public void PayTheRailWay(int customerId, decimal moneyAmount)
        {
            // 可以先把一些功能寫出來，我們可以透過Result來Combine，就可以快速確認正確性
            var moneyToCharge = MoneyToCharge.Create(moneyAmount);
            var customer = _customerRepository.GetCustomerById(customerId).IfNull("找不到資源");

            var result = Result.Combine(moneyToCharge, customer)
                .OnSuccess(() => customer.Value.AddBalance(moneyToCharge.Value))
                .OnSuccess(() => _paymentService.ChargePayment(customer.Value.Id, moneyToCharge.Value))
                .OnSuccess(() => _customerRepository.SaveChange(customer.Value)
                    .OnFailure(() => _customerRepository.RollbackTransaction()))
                .OnBoth(result => Log(result))
                .OnBoth(result => result.IsSuccess ? "付款成功" : result.ErrorMessage);

            Console.WriteLine(result);
        }

        private void Log(Result result)
        {
            if (result.IsFailure)
                Console.WriteLine(result.ErrorMessage);
            else
                Console.WriteLine("執行成功");
        }
    }
}
