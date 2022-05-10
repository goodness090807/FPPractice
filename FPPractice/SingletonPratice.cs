using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice
{
    /// <summary>
    /// Singleton實踐，通常會加sealed，不讓物件被繼承
    /// 參考文章：https://blog.davy.tw/posts/singleton-basic-in-c-sharp/
    /// 微軟文章：https://docs.microsoft.com/en-us/previous-versions/msp-n-p/ff650316(v=pandp.10)?redirectedfrom=MSDN
    /// </summary>
    public sealed class SingletonPratice
    {
        private static SingletonPratice _signleton;

        // 基本的短寫法
        //public static SingletonPratice Instance
        //{
        //    get
        //    {
        //        return _signleton ?? (_signleton = new SingletonPratice());
        //    }
        //}

        // 更短的寫法
        public static SingletonPratice Instance => _signleton ?? (_signleton = new SingletonPratice());
    }
}
