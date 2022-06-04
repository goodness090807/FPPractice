using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.AvoidingPrimitiveObsessionPrcatice
{
    public class CustomerController
    {
        /// <summary>
        /// 假設這邊用Post新增資料
        /// </summary>
        public void CreateCustomer(CustomerViewModel bodyParam)
        {
            var customerNameResult = CustomerName.Create(bodyParam.Name);
            var emailResult = Email.Create(bodyParam.Email);

            // 這個自己亂寫的，主要目的是為了模擬錯誤回傳
            ModelState modelState = new ModelState();

            //下面模擬API驗證失敗的回傳
            if (customerNameResult.IsFailure)
                modelState.AddModelError(customerNameResult.ErrorMessage);
            if (emailResult.IsFailure)
                modelState.AddModelError(emailResult.ErrorMessage);
            // 有錯誤就結束了，不要再往下做
            if (modelState.Errors.Count > 0)
                return;

            var customer = new Customer(customerNameResult.Value, emailResult.Value);

            // 接下來再做一些處理，例如儲存之類的
            // _database.SaveChange(customer);

            Console.WriteLine($"顧客名稱：{customerNameResult.Value}，Email：{emailResult.Value}");
        }

        public class CustomerViewModel
        {
            /// <summary>
            /// 這邊就不用再驗證了，因為內部有做驗證
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 這邊就不用再驗證了，因為內部有做驗證
            /// </summary>
            public string Email { get; set; }
        }

        public class ModelState
        {
            public List<string> Errors { get; private set; }

            public void AddModelError(string error)
            {
                Errors.Add(error);
            }
        }
    }
}
