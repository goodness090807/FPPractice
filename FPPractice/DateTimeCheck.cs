using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice
{
    /// <summary>
    /// 封裝
    /// </summary>
    public class DateTimeCheck
    {
        ///// <summary>
        ///// 用private set的原因，是不能讓外面改，但內部可以改
        ///// </summary>
        //public DateTime BeginDate { get; private set; }
        //public DateTime EndDate { get; private set; }

        // 如果不是常變更的值，我們不用這樣寫
        //private readonly DateTime _beginDate;
        //private readonly DateTime _endDate;

        //public DateTime BeginDate { get { return _beginDate; } }
        //public DateTime EndDate { get { return _endDate; } }

        // 以下的作法算是最好的
        public DateTime BeginDate { get; }
        public DateTime EndDate { get; }

        public DateTimeCheck(DateTime beginDate, DateTime endDate)
        {
            if (beginDate.CompareTo(endDate) > 0) throw new Exception("資料錯誤");

            BeginDate = beginDate;
            EndDate = endDate;
        }

        /// <summary>
        /// 架設要更新內部屬性，可以透過這樣的方式來做更新，這樣就不會有直接異動到變數的問題
        /// </summary>
        public DateTimeCheck Slide(int days)
        {
            return new DateTimeCheck(BeginDate.AddDays(days), EndDate.AddDays(days));
        }

        public bool IsInRange(DateTime checkDate)
        {
            return BeginDate.CompareTo(checkDate) <= 0 && EndDate.CompareTo(checkDate) > 0;
        }
    }
}
