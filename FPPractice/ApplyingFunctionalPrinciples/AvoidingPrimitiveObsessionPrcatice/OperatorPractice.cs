using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.AvoidingPrimitiveObsessionPrcatice
{
    /// <summary>
    /// 這邊介紹如何使用隱式或顯示轉換
    /// </summary>
    public class OperatorPractice
    {
        private readonly string _value;

        public OperatorPractice(string value)
        {
            _value = value;
        }

        public static OperatorPractice Create(string name)
        {
            // 通常驗證會寫在這邊，而不是寫在建構子
            // 這邊其實可以寫成ResultHandler的寫法，但這邊不實現
            if (string.IsNullOrEmpty(name))
                throw new ArgumentNullException(name, "值不能是空的");

            return new OperatorPractice(name);
        }

        /// <summary>
        /// 隱含轉換的用法說明
        /// </summary>
        public static string GetOperatorPracticeString(string name)
        {
            // 這邊拿到了我們的物件
            OperatorPractice operatorPrcatice = new OperatorPractice(name);
            // 但因為我們有隱含轉換，所以可以直接把物件轉成string
            string operatorPrcaticeString = operatorPrcatice;
            // 也可以像這樣return回去
            return operatorPrcatice;
        }

        /// <summary>
        /// 明確轉換的用法說明
        /// </summary>
        public static OperatorPractice GetOperatorPracticeByString(string name)
        {
            // 明確轉換用法，這樣能轉換成功是因為有定義
            var operatorPractice = (OperatorPractice)name;

            // 這邊我把它變成null
            name = null;
            // 因為我們在Create的時候有做到驗證值是不是empty或是null，所以驗證失敗就會報錯
            var errorOperatorPractice = (OperatorPractice)name;

            return operatorPractice;
        }

        /// <summary>
        /// 隱含轉換的設定
        /// </summary>
        public static implicit operator string(OperatorPractice operatorPractice)
        {
            return operatorPractice._value;
        }

        /// <summary>
        /// 明確轉換的設定
        /// </summary>
        public static explicit operator OperatorPractice(string value)
        {
            return Create(value);
        }
    }
}
