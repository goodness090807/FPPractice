using System;

namespace FPPractice.ApplyingFunctionalPrinciples.FunctionalWayPractice
{
    public class Result
    {
        public bool IsSuccess { get; }
        public string ErrorMessage { get; private set; }
        public bool IsFailure => !IsSuccess;

        /// <summary>
        /// 使用protected是代表不能直接生成，我們要透過下面的static方法來做生成
        /// </summary>
        /// <param name="isSuccess"></param>
        /// <param name="errorMessage"></param>
        protected Result(bool isSuccess, string errorMessage)
        {
            if (isSuccess && !string.IsNullOrEmpty(errorMessage))
                throw new InvalidOperationException("成功操作不能有錯誤訊息");
            if (!isSuccess && string.IsNullOrEmpty(errorMessage))
                throw new InvalidOperationException("失敗操作要有錯誤訊息");

            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result Ok()
        {
            return new Result(true, string.Empty);
        }
        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, string.Empty);
        }

        public static Result Fail(string message)
        {
            return new Result(false, message);
        }

        public static Result<T> Fail<T>(string message)
        {
            return new Result<T>(default(T), false, message);
        }

        /// <summary>
        /// 這邊的作法，是指我們可以把Result都丟進來
        /// 代表一定要都成功，如果有一個失敗的話就會回傳錯誤
        /// </summary>
        /// <param name="results"></param>
        /// <returns></returns>
        public static Result Combine(params Result[] results)
        {
            foreach(var result in results)
            {
                if (result.IsFailure)
                    return result;
            }

            return Ok();
        }
    }

    public class Result<T> : Result
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

        protected internal Result(T value, bool isSuccess, string errorMessage)
            : base(isSuccess, errorMessage)
        {
            _value = value;
        }
    }
}
