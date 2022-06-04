using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.ExceptionPractice
{
    /// <summary>
    /// 這邊介紹Exception的使用情境
    /// 基本上就是在內層如果遇到不合理的行為我們就會throw Exception
    /// 如果在外層驗證的話，通常是會去做輸入輸出，確保有回傳資料
    /// </summary>
    public class ExceptionUseCase
    {
        public void UpdateData(string name)
        {
            var validateResult = ValidateName(name);

            if (!string.IsNullOrEmpty(validateResult))
                Console.WriteLine(validateResult);

            var data = new Data();
            data.UpdateName(name);
        }

        private string ValidateName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return "name不能是空的";

            return string.Empty;
        }

        /// <summary>
        /// 這裡實現的不變性
        /// 底部的驗證，因為我們需要讓name有值，所以沒值的話是直接拋出錯誤的
        /// 就不需要像上面去回傳string，上面是外部的自行驗證
        /// </summary>
        public class Data
        {
            private readonly string _name;

            public Data()
            {

            }

            public Data(string name)
            {
                _name = name;
            }

            public Data UpdateName(string name)
            {
                if (string.IsNullOrEmpty(name))
                    throw new ArgumentNullException(nameof(name), "不要傳null或空值進來好嗎");

                return new Data(name);
            }
        }
    }
}
