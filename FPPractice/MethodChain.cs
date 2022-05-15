using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPPractice
{
    /// <summary>
    /// 為了讓同一層可以使用，所以透過static撰寫
    /// </summary>
    public static class StringBuilderExtension
    {
        // 因為這邊有this，所以需要一個static class來做封裝
        public static StringBuilder AppendFormattedLine(this StringBuilder stringBuilder,
            string format,
            params object[] args) => stringBuilder.AppendFormat(format, args).AppendLine();

        // 這邊是透過Func的方式來做到Delegate的判斷
        // 下面的意思是，如果Func中回傳了true就做處理，false就不做事
        // 但是這種方法有缺點，只能適用一種處理
        public static StringBuilder AppendWhenLine(this StringBuilder stringBuilder,
            Func<bool> predicate,
            string value) => predicate() ? stringBuilder.AppendLine(value) : stringBuilder;

        // 這邊的作法相對較好，因為我們可以針對判斷式，去做需要的處理
        public static StringBuilder AppendWhen(this StringBuilder stringBuilder,
             Func<bool> predicate,
             Func<StringBuilder, StringBuilder> fn)
                // 這邊的意思是，如果確認是true，那我就做我外面給的處理，false的話就不做
                => predicate() ? fn(stringBuilder) : stringBuilder;

        // 這邊透過Func，將IEnumerable資料做處理
        public static StringBuilder AppendSequence<T>(this StringBuilder stringBuilder,
            IEnumerable<T> seq,
            Func<StringBuilder, T, StringBuilder> fn) => seq.Aggregate(stringBuilder, fn);
    }

    /// <summary>
    /// 方法通常是可以串聯下去的
    /// 像是StringBuilder就是寫成這樣
    /// 我們也能自己去擴展內容達到我們想要的效果
    /// </summary>
    public class MethodChain
    {
        public static string GetStringBuilder(params string[] strings)
        {
            var sb = new StringBuilder();
            foreach(string str in strings)
            {
                // 這邊就可以直接使用我們的擴充方法了
                sb.AppendFormattedLine(str)
                  // 因為我們有做擴充，所以我們可以直接連在後面
                  .AppendWhen(() => 
                  {
                      // 這邊的話如果有星號就會再列印一遍
                      if (str.Contains("*"))
                          return true;

                      return false;
                  }, sb => sb.AppendLine(str));
            }

            return sb.ToString();
        }

        public static string GetFuncString(bool print, string value)
        {
            var sb = new StringBuilder();

            // 可以這樣呼叫
            sb.AppendWhenLine(() => print, value);

            // 也可以像是function一樣回傳我們要做的參數
            sb.AppendWhenLine(() =>
            {
                if (value.Contains("test"))
                    return true;

                return false;
            }, value);

            return sb.ToString();
        }

        public static string GetSequence(params string[] strings)
        {
            var sb = new StringBuilder();

            sb.AppendSequence(
                strings,
                (sb, str) => sb.AppendFormattedLine(str + "迴圈使用"));

            return sb.ToString();
        }
    }
}
