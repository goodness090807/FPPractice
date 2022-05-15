using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice
{
    /// <summary>
    /// 這個是透過Func來做成的Delegate，最容易看懂，也最好閱讀
    /// </summary>
    public class AnonymousFuncDelegate
    {
        public static Func<decimal, decimal, decimal> GetMathOperation(char oper)
        {

            switch (oper)
            {
                case '+': return (l, r) => l + r;
                case '-': return (l, r) => l - r;
            }

            throw new NotSupportedException("沒有支援的方法");
        }

        public static decimal Eval(decimal left, char oper, decimal right)
        {
            return GetMathOperation(oper)(left, right);
        }
    }
}
