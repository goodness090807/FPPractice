using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.FunctionalWayPractice
{
    public interface ICustomerRepository
    {
        Customer GetCustomerById(int customerId);
        Result SaveChange(Customer customer);
        /// <summary>
        /// 為了示範用，做了一個Rollback機制
        /// </summary>
        /// <returns></returns>
        Result RollbackTransaction();
    }
}
