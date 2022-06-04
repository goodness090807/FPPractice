using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.ExceptionPractice
{
    /// <summary>
    /// 這裡介紹了盡量不要使用的方式，因為會讓維護較困難
    /// </summary>
    public class NotWriteThis
    {
        public void InsertDataNotWriteThis(Data data)
        {
            // 把它移出來也不一定好，因為也不知道裡面實作了什麼，到底有沒有用try catch的方式來實作
            Validation(data);

            try
            {
                // 這樣的驗證會讓程式碼可讀性降低
                Validation(data);
            }
            catch(ValidationException ex)
            {
                // 也不知道這邊回傳的東西到底是什麼
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 相對好的做法，這樣能將方法變成數學的函式一樣，有輸入輸出
        /// </summary>
        public void InsertDataBetterWay(Data data)
        {
            var validateResult = ValidateData(data);
            
            if (validateResult != string.Empty)
            {
                Console.WriteLine(validateResult);
                return;
            }

            // do something
            Console.WriteLine("新增成功");
        }

        /// <summary>
        /// 這種做法通常會讓我們不知道裡面實作了什麼，需要進來查看才知道
        /// 會影響我們的推理能力，
        /// </summary>
        private void Validation(Data data)
        {
            if (data.Id == null)
                throw new ValidationException("Id不能為Null");

            if (string.IsNullOrEmpty(data.Name))
                throw new ValidationException("Name不能為空");
        }

        /// <summary>
        /// 這個是簡單的讓功能數學化，這樣才會有輸入和輸出
        /// </summary>
        /// <param name="dataName"></param>
        /// <returns></returns>
        private string ValidateData(Data data)
        {
            if (data.Id == null)
                return "Id不能為Null";

            if (string.IsNullOrEmpty(data.Name))
                return "Name不能為空";

            return string.Empty;
        }

        public class Data
        {
            public int? Id { get; set; }
            public string Name { get; set; }
        }
    }
}
