using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.AvoidingPrimitiveObsessionPrcatice
{
    public class ResultHandler
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; private set; }
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// 使用protected是代表不能直接生成，我們要透過下面的static方法來做生成
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="errorMessage"></param>
        protected ResultHandler(bool isSuccess, string errorMessage)
        {
            if (isSuccess && !string.IsNullOrEmpty(errorMessage))
                throw new InvalidOperationException("成功操作不能有錯誤訊息");
            if (!isSuccess && string.IsNullOrEmpty(errorMessage))
                throw new InvalidOperationException("失敗操作要有錯誤訊息");

            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static ResultHandler Ok()
        {
            return new ResultHandler(true, string.Empty);
        }
        public static ResultHandler<T> Ok<T>(T value)
        {
            return new ResultHandler<T>(value, true, string.Empty);
        }

        public static ResultHandler Fail(string message)
        {
            return new ResultHandler(false, message);
        }

        public static ResultHandler<T> Fail<T>(string message)
        {
            return new ResultHandler<T>(default(T), false, message);
        }
    }

    public class ResultHandler<T> : ResultHandler
    {
        private readonly T _value;
        public T Value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException();

                return _value;
            }
        }

        protected internal ResultHandler(T value, bool isSuccess, string errorMessage)
            : base(isSuccess, errorMessage)
        {
            _value = value;
        }
    }
}
