using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace FPPractice
{
    /// <summary>
    /// 教學如何包裝Using方法
    /// </summary>
    public class UsingStatement
    {
        public async Task<string> GetByWeb()
        {
            var data = await GetData();

            // 假設這邊還會再對data做處理，這樣使用using會比較好看

            return data;
        }

        private async Task<string> GetData()
        {
            using (var client = new HttpClient())
            {
                return await client.GetAsync("https://docs.microsoft.com/zh-tw/dotnet/csharp/toc.json").Result.Content.ReadAsStringAsync();
            }
        }
    }
}
