using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.AvoidingPrimitiveObsessionPrcatice
{
    /// <summary>
    /// ValueObject的寫法，將每一個屬性作驗證
    /// 這樣就能將外部的驗證做來這邊，就不需要去做額外的驗證了
    /// </summary>
    public class CustomerName : ValueObject<CustomerName>
    {
        private readonly string _value;

        public CustomerName(string value)
        {
            _value = value;
        }

        public static ResultHandler<CustomerName> Create(string customerName)
        {
            if (string.IsNullOrWhiteSpace(customerName))
                return ResultHandler.Fail<CustomerName>("名字不能是空值");

            customerName = customerName.Trim();

            if (customerName.Length > 20)
                return ResultHandler.Fail<CustomerName>("名字不能超過20個字");

            return ResultHandler.Ok(new CustomerName(customerName));
        }

        protected override bool EqualsCore(CustomerName other)
        {
            return _value == other._value;
        }

        protected override int GetHashCodeCore()
        {
            return _value.GetHashCode();
        }

        public static explicit operator CustomerName(string customerName)
        {
            return Create(customerName).Value;
        }

        public static implicit operator string(CustomerName customerName)
        {
            return customerName._value;
        }
    }
}
