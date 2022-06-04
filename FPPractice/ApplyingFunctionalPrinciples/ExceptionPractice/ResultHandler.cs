using System;

namespace FPPractice.ApplyingFunctionalPrinciples.ExceptionPractice
{
    public class ResultHandler
    {
        public bool IsSuccess { get; }
        public ErrorType? ErrorType { get; private set; }
        public string ErrorMessage { get; private set; }
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// 使用protected是代表不能直接生成，我們要透過下面的static方法來做生成
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="errorMessage"></param>
        protected ResultHandler(bool isSuccess, ErrorType? errorType, string errorMessage)
        {
            if (isSuccess && (errorType != null || !string.IsNullOrEmpty(errorMessage)))
                throw new InvalidOperationException("成功操作不能有錯誤訊息");
            if (!isSuccess && (errorType == null || string.IsNullOrEmpty(errorMessage)))
                throw new InvalidOperationException("失敗操作要有錯誤訊息");

            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static ResultHandler Ok()
        {
            return new ResultHandler(true, null, string.Empty);
        }
        public static ResultHandler<T> Ok<T>(T value)
        {
            return new ResultHandler<T>(value, true, null, string.Empty);
        }

        public static ResultHandler Fail(ErrorType errorType, string message)
        {
            return new ResultHandler(false, errorType, message);
        }

        public static ResultHandler<T> Fail<T>(ErrorType errorType, string message)
        {
            return new ResultHandler<T>(default(T), false, errorType, message);
        }
    }

    public class ResultHandler<T> : ResultHandler
    {
        private readonly T _value;
        public T value
        {
            get
            {
                if (!IsSuccess)
                    throw new InvalidOperationException();

                return _value;
            }
        }

        protected internal ResultHandler(T value, bool isSuccess, ErrorType? errorType, string errorMessage) 
            : base(isSuccess, errorType, errorMessage)
        {
            _value = value;
        }
    }

    public enum ErrorType
    {
        CustomError,
        SystemError
    }
}
