using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPPractice
{
    /// <summary>
    /// 這邊介紹如何用Func來改善功能的使用
    /// </summary>
    public class PartialFunctionApplication
    {
        // 一般Function的寫法會寫成這樣，但這樣的方式就只能普通的呼叫，而不能泛用
        public static int Add(int x, int y) => x + y;

        // 透過Func的寫法，我們可以用Linq的呼叫方式來使用
        public static Func<int, int> Add(int x) => y => x + y;

        public static IEnumerable<int> ListAdd(params int[] ints)
        {
            // 一般呼叫Func的方式，這樣看起來會很怪，所以不太會這樣用
            //var result = Add(5)(10);
            
            // 如果用一般的寫法，我們這邊需要額外再去寫表達式
            var normalFunctionSelect = ints.Select(x => Add(x, 5));
            // 用Func的方式就不需要
            var results = ints.Select(Add(5));

            return results;
        }
    }
}
