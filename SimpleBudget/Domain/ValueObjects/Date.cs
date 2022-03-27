using System.Diagnostics.CodeAnalysis;

namespace SB.Domain.ValueObjects
{
    public class Date : IEquatable<Date>
    {
        public Date(int year, int month, int date)
        {
            try
            {
                var dateTime = new DateTime(year, month, date);
                this.DateTime = dateTime;
                Year = dateTime.Year;
                Month = dateTime.Month;
                Day = dateTime.Day;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("日付を正しく入力して下さい。");
            }
        }

        /// <summary>
        /// DateTime型のデータ
        /// </summary>
        public DateTime DateTime { get; }

        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; }

        /// <summary>
        /// 月
        /// </summary>
        public int Month { get; }

        /// <summary>
        /// 日付
        /// </summary>
        public int Day { get; }

        /// <summary>
        /// スラッシュ付きの日付
        /// 例：2022/03/22
        /// </summary>
        public string DateWithSlash
        {
            get => $"{Year}/{Month.ToString("00")}/{Day.ToString("00")}";
        }

        /// <summary>
        /// DayOfWeekを取得する
        /// </summary>
        public DayOfWeek DayOfWeek
            => this.DateTime.DayOfWeek;

        private const string youbi = "曜日";

        /// <summary>
        /// 日本語訳したDayOfWeelを取得する
        /// </summary>
        public string DayOfWeekInJapanese
        {
            get
            {
                switch(this.DayOfWeek)
                {
                    case DayOfWeek.Sunday:
                        return $"日{youbi}";
                    case DayOfWeek.Monday:
                        return $"月{youbi}";
                    case DayOfWeek.Tuesday:
                        return $"火{youbi}";
                    case DayOfWeek.Wednesday:
                        return $"水{youbi}";
                    case DayOfWeek.Thursday:
                        return $"木{youbi}";
                    case DayOfWeek.Friday:
                        return $"金{youbi}";
                    case DayOfWeek.Saturday:
                        return $"土{youbi}";
                    default:
                        return "";
                }
            }
        }

        /// <summary>
        /// 等価比較をする
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Date? other)
        {
            if (other is null) { return false; }
            return this.Year.Equals(other.Year)
                && this.Month.Equals(other.Month)
                && this.Day.Equals(other.Day);
        }

        /// <summary>
        /// 等価比較をする
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool operator ==(Date d1, Date d2)
        {
            if (d1 is null) { return false; }
            return d1.Equals(d2);
        }

        /// <summary>
        /// 等価でないかを検証する
        /// </summary>
        /// <param name="d1"></param>
        /// <param name="d2"></param>
        /// <returns></returns>
        public static bool operator !=(Date d1, Date d2)
        {
            if (d1 is null)
            {
                if (d2 is null) { return false; }
                if (d2 is object) { return true; }
            }
            return !d1.Equals(d2);
        }

        /// <summary>
        /// System.ObjectのEquals
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool Equals(object? value)
        {
            var other = value as Date;
            return this.Equals(other);
        }

        /// <summary>
        /// System.ObjectのGetHashCode
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
