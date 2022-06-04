using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.ExceptionPractice
{
    public class UseResult
    {
        private List<Data> datas = new List<Data>();

        public void CreateData(string name)
        {
            var data = new Data(datas.Count + 1, name);

            var result = SaveData(data);

            if (result.IsFailure)
                Console.WriteLine(result.ErrorMessage);
        }

        public ResultHandler SaveData(Data data)
        {
            if(string.IsNullOrEmpty(data.Name))
            {
                return ResultHandler.Fail(ErrorType.CustomError, "姓名不可為空");
            }

            try
            {
                // 可以把這個想像成EF Core新增資料
                datas.Add(data);
            }
            catch(Exception ex)
            {
                // 這邊想像成EF Core新增資料發生錯誤了，像是主鍵重複的話，這部份我們就可以自己定義回傳訊息了
                if (ex.Message.Contains("IX_Unique_key"))
                    ResultHandler.Fail(ErrorType.SystemError, "主鍵重複了，可能需要重試");
            }            

            return ResultHandler.Ok();
        }

        public ResultHandler<Data> GetData(int id)
        {
            try
            {
                return ResultHandler.Ok(datas.FirstOrDefault(x => x.Id == id));
            }
            catch(Exception ex)
            {
                if (ex.Message.Contains(""))
                {
                    ResultHandler.Fail<Data>(ErrorType.SystemError, "找不到資料");
                }

                throw;
            }
        }

        public class Data
        {
            public Data(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; }
            public string Name { get; }
        }
    }
}
