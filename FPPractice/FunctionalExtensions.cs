using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace FPPractice
{
    public static class FunctionalExtensions
    {
        /// <summary>
        /// Map通常是將資料做傳入傳出，傳入要處理的資料，透過Func來做完事情後並傳出
        /// </summary>
        public static TResult Map<TSource, TResult>(
            this TSource source,
            Func<TSource, TResult> fn) => fn(source);

        /// <summary>
        /// Tee通常是針對void的方法去做處理
        /// </summary>
        public static T Tee<T>(
            this T source,
            Action<T> act)
        {
            act(source);
            return source;
        }

        public static Company UsingMapResult()
        {
            var company = DisposableFactory.Using(
                () => new FileStream("MapText.txt", FileMode.Open, FileAccess.Read),
                stream => new byte[stream.Length].Tee(buff =>
                DisposableFactory.Using(
                    () => new StreamReader(stream, Encoding.UTF8),
                    // 這邊一定要使用類似ref結構的方式操作，把資料寫入buff，否則值會無法被寫入
                    sr => Encoding.UTF8.GetBytes(sr.ReadToEnd(), buff)))
                )
                // 下面的方式會看起來很攏長，所以上面是用Tee來做處理
                //stream =>
                //{
                //    using (var sr = new StreamReader(stream, Encoding.UTF8))
                //    {
                //        var str = sr.ReadToEnd();
                //        // 故意轉成byte來做傳回
                //        byte[] bytes = Encoding.UTF8.GetBytes(str);

                //        return bytes;
                //    }
                //})
                // 這邊可以直接調用Encoding的方式來Map資料，將byte轉回String
                .Map(Encoding.UTF8.GetString)
                // 調用Tee來做直接Print資料
                .Tee(Console.WriteLine)
                // 這邊的話，就是想要把字串轉成物件，也可以透過map的方式，這樣就不用用一個參數來接回傳的結果了
                .Map(str => JsonConvert.DeserializeObject<Company>(str));

            return company;
        }
    }

    public class Company
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
