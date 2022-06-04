using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.AvoidingPrimitiveObsessionPrcatice
{
    /// <summary>
    /// 這邊教學用類似ValueObject的寫法來達到封裝
    /// </summary>
    public class Email : ValueObject<Email>
    {
        private readonly string _value;

        /// <summary>
        /// 建構子，這邊通常不做任何式，包含驗證，由Create去做才對
        /// </summary>
        public Email(string value)
        {
            _value = value;
        }

        /// <summary>
        /// 創建一個Mail，並在這邊做各項的驗證
        /// </summary>
        public static ResultHandler<Email> Create(string email)
        {
            if (string.IsNullOrEmpty(email))
                return ResultHandler.Fail<Email>("email不能是空的");

            email = email.Trim();

            if (email.Length > 256)
                return ResultHandler.Fail<Email>("email長度過長");

            if (!email.Contains("@"))
                return ResultHandler.Fail<Email>("不正確的email格式");

            return ResultHandler.Ok(new Email(email));
        }

        protected override bool EqualsCore(Email other)
        {
            return _value == other._value;
        }

        protected override int GetHashCodeCore()
        {
            return _value.GetHashCode();
        }

        public static explicit operator Email(string email)
        {
            return Create(email).Value;
        }

        public static implicit operator string(Email email)
        {
            return email._value;
        }
    }
}
