using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice.ApplyingFunctionalPrinciples.FunctionalWayPractice
{
    public static class ResultExtensions
    {
        public static Result<T> IfNull<T>(this T obj, string errorMessage) where T : class
        {
            if (obj == null)
                Result.Fail<T>(errorMessage);

            return Result.Ok(obj);
        }

        public static Result OnSuccess(this Result result, Action action)
        {
            if (result.IsFailure)
                return result;

            action();

            return Result.Ok();
        }

        /// <summary>
        /// 這個是有回傳值的調用
        /// </summary>
        /// <param name="result"></param>
        /// <param name="func"></param>
        /// <returns></returns>
        public static Result OnSuccess(this Result result, Func<Result> func)
        {
            if (result.IsFailure)
                return result;            

            return func();
        }

        /// <summary>
        /// 如果失敗就要做什麼事
        /// </summary>
        public static Result OnFailure(this Result result, Action action)
        {
            if (result.IsFailure)
                action();

            return result;
        }

        public static Result OnBoth(this Result result, Action<Result> action)
        {
            action(result);

            return result;
        }

        public static T OnBoth<T>(this Result result, Func<Result, T> func)
        {
            return func(result);
        }
    }
}
