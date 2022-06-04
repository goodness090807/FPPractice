using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.FunctionalWayPractice
{
    public class Customer
    {
        public int Id { get; private set; }
        public decimal Balance { get; private set; }

        public void AddBalance(MoneyToCharge moneyToCharge)
        {
            // 這邊透過operator的方式，所以可以將物件轉成decimal
            Balance += moneyToCharge;
        }
    }
}
