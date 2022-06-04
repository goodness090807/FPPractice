using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.AvoidingPrimitiveObsessionPrcatice
{
    public class Customer
    {
        public CustomerName Name { get; private set; }
        public Email Email { get; private set; }

        public Customer(CustomerName name, Email email)
        {
            // 這邊可以做一些基礎的驗證
            if (name == null)
                throw new ArgumentNullException(nameof(name));
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            Name = name;
            Email = email;
        }

        public void ChangeName(CustomerName name)
        {
            // 基本上不用這麼多驗證，可能之後會改掉
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            Name = name;
        }

        public void ChangeEmail(Email email)
        {
            // 基本上不用這麼多驗證，可能之後會改掉
            if (email == null)
                throw new ArgumentNullException(nameof(email));

            Email = email;
        }
    }
}
