using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.AvoidingPrimitiveObsessionPrcatice
{
    /// <summary>
    /// 這邊是自己實作了ValueObject的基底類別
    /// 可以讓外部物件繼承來使用
    /// </summary>
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;

            // 如果ValueObject是null的話，就回傳false
            if (ReferenceEquals(valueObject, null))
                return false;

            // 否則就去做外部實作的比對
            return EqualsCore(valueObject);
        }

        /// <summary>
        /// 這邊沒實作，所以給外部實作就好了
        /// </summary>
        protected abstract bool EqualsCore(T other);

        /// <summary>
        /// GetHashCode也是一樣
        /// </summary>
        public override int GetHashCode()
        {
            return GetHashCodeCore();
        }

        protected abstract int GetHashCodeCore();

        /// <summary>
        /// VO記得要實作兩個Operator，等於和不等於
        /// </summary>
        public static bool operator ==(ValueObject<T> firstVO, ValueObject<T> secondVO)
        {
            if (ReferenceEquals(firstVO, null) && ReferenceEquals(secondVO, null))
                return true;
            if (ReferenceEquals(firstVO, null) || ReferenceEquals(secondVO, null))
                return false;

            return firstVO.Equals(secondVO);
        }

        public static bool operator !=(ValueObject<T> firstVO, ValueObject<T> secondVO)
        {
            return !(firstVO == secondVO);
        }
    }
}
