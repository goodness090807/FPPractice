using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice
{
    /// <summary>
    /// 解說經典的Delegate的用法
    /// </summary>
    public class ClassicDelegation
    {
        // 要宣告delegate的方法
        public delegate decimal MathOperation(decimal left, decimal right);

        public static decimal Add(decimal left, decimal right)
        {
            return left + right;
        }

        public static decimal Subtract(decimal left, decimal right)
        {
            return left - right;
        }

        private static MathOperation GetMathOperation(char oper)
        {
            switch(oper)
            {
                case '+': return Add;
                case '-': return Subtract;
            }

            throw new NotSupportedException("沒有支援的方法");
        }

        public static decimal Eval(decimal left, char oper, decimal right)
        {
            return GetMathOperation(oper)(left, right);
        }
    }
}
