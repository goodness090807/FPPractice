using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice
{
    /// <summary>
    /// 這邊介紹，為什麼用Interface比較好
    /// 因為使用Delegate的速度比Interface慢4倍，又比直接調用慢8倍
    /// </summary>
    public class ReplaceDelegateInterface
    {
        public interface IMathOperation
        {
            decimal Compute(decimal left, decimal right);
        }

        public class AddOperation : IMathOperation
        {
            public decimal Compute(decimal left, decimal right)
            {
                return left + right;
            }
        }

        public class SubtractOperation : IMathOperation
        {
            public decimal Compute(decimal left, decimal right)
            {
                return left - right;
            }
        }

        private static IMathOperation GetMathOperation(char oper)
        {
            switch (oper)
            {
                case '+': return new AddOperation();
                case '-': return new SubtractOperation();
            }

            throw new NotSupportedException("沒有支援的方法");
        }

        public static decimal Eval(decimal left, char oper, decimal right)
        {
            return GetMathOperation(oper).Compute(left, right);
        }
    }
}
