using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.FunctionalWayPractice
{
    public interface IPaymentService
    {
        Result ChargePayment(int customerId, MoneyToCharge moneyToCharge);
    }
}
