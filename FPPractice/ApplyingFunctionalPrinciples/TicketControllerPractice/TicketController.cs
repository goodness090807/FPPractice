using FPPractice.ApplyingFunctionalPrinciples.ExceptionPractice;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.TicketControllerPractice
{
    /// <summary>
    /// 這邊介紹如何將Excepction來做應用
    /// </summary>
    public class TicketController
    {
        private readonly TheaterGateway _theaterGateway;
        private readonly TicketRepository _ticketRepository;

        public TicketController(TheaterGateway theaterGateway, TicketRepository ticketRepository)
        {
            _theaterGateway = theaterGateway;
            _ticketRepository = ticketRepository;
        }

        /// <summary>
        /// 假裝是Action，要做個購買票的動作
        /// </summary>
        public void BuyTicket(DateTime dateTime, string userName)
        {
            var validationResult = Validation(dateTime, userName);
            // 錯誤就強制結束
            if (validationResult.IsFailure)
            {
                Console.WriteLine(validationResult.ErrorMessage);
                return;
            }

            // 使用外部資源來訂票
            var apiResult = _theaterGateway.Reserve(dateTime, userName);
            // 錯誤一樣強制結束
            if (apiResult.IsFailure)
            {
                Console.WriteLine(validationResult.ErrorMessage);
                return;
            }

            _ticketRepository.InsertTicket(dateTime, userName);
            Console.WriteLine("購買成功");
        }

        /// <summary>
        /// 假設要做驗證，要有一個回傳
        /// </summary>
        private ResultHandler Validation(DateTime dateTime, string userName)
        {
            if (dateTime < DateTime.UtcNow.AddHours(8))
                return ResultHandler.Fail(ErrorType.CustomError, "訂購日期不能小於今天");
            if (string.IsNullOrEmpty(userName))
                return ResultHandler.Fail(ErrorType.CustomError, "沒有名字");

            return ResultHandler.Ok();
        }

        /// <summary>
        /// 這邊一樣，如果打外部的Api的話，可以透過ResultHandler來做回傳
        /// 但try catch的部分，我們只針對知道的部分來做回傳，不對通用的來回傳
        /// </summary>
        public class TheaterGateway
        {
            // 假設打了外部的API
            public ResultHandler Reserve(DateTime date, string userName)
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        httpClient.BaseAddress = new Uri("http://exmaple.com.tw");

                        var jsonStr = JsonConvert.SerializeObject(new
                        {
                            Date = date,
                            UserName = userName
                        });

                        var buffer = Encoding.UTF8.GetBytes(jsonStr);
                        var byteContent = new ByteArrayContent(buffer);

                        httpClient.PostAsync("/api/test", byteContent);

                        return ResultHandler.Ok();
                    }
                }
                catch(InvalidOperationException)
                {
                    return ResultHandler.Fail(ErrorType.SystemError, "網址有誤");
                }
                catch(HttpRequestException)
                {
                    return ResultHandler.Fail(ErrorType.SystemError, "連不上外部資源");
                }
            }
        }

        public class TicketRepository
        {
            public void InsertTicket(DateTime dateTime, string userName)
            {
                // 這邊做儲存的動作，這邊就不實現了
            }
        }
    }
}
