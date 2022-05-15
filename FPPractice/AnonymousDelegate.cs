using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice
{
    /// <summary>
    /// 匿名型別的delegate，據說跟Interface一樣快
    /// </summary>
    public class AnonymousDelegate
    {
        // 要宣告delegate的方法
        public delegate decimal MathOperation(decimal left, decimal right);

        private static MathOperation GetMathOperation(char oper)
        {
            switch (oper)
            {
                case '+': return delegate (decimal left, decimal right) { return left + right; };
                case '-': return delegate (decimal left, decimal right) { return left - right; };
            }

            throw new NotSupportedException("沒有支援的方法");
        }

        public static decimal Eval(decimal left, char oper, decimal right)
        {
            return GetMathOperation(oper)(left, right);
        }
    }
}
