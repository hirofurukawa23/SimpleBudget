namespace SB.Domain.ValueObjects
{
    public class Month : IEquatable<Month>
    {
        public Month(int year, int month)
        {
            try
            {
                var dateTime = new DateTime(year, month, 1);

                this.Year = dateTime.Year;
                this.Months = dateTime.Month;
                this.StartDate = new Date(year, month, 1);

                var lastOfMonth = dateTime.AddMonths(1).AddDays(-1); //1カ月足しこんで1日引けば当月の最終日付
                this.EndDate = new Date(lastOfMonth.Year, lastOfMonth.Month, lastOfMonth.Day);

                this.DaysCount = this.EndDate.Day; //最終日までの日数が月の日付数
            }
            catch (Exception ex)
            {
                throw new ArgumentException("年月を正しく入力してください。");
            }
        }

        /// <summary>
        /// 年
        /// </summary>
        public int Year { get; set; }

        /// <summary>
        /// 月
        /// </summary>
        public int Months { get; set; }

        /// <summary>
        /// 年月の開始日付
        /// </summary>
        public Date StartDate { get; set; }

        /// <summary>
        /// 年月の最終日付
        /// </summary>
        public Date EndDate { get; set; }

        /// <summary>
        /// 月が持つ日数
        /// </summary>
        public int DaysCount { get; set; }

        /// <summary>
        /// 等価比較をする
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public bool Equals(Month? other)
        {
            if (other is null) { return false; }
            return this.Year.Equals(other.Year)
                && this.Months.Equals(other.Months);
        }

        /// <summary>
        /// 等価であるかを検証する
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static bool operator ==(Month m1, Month m2)
        {
            return m1.Equals(m2);
        }

        /// <summary>
        /// 等価でないことを検証する
        /// </summary>
        /// <param name="m1"></param>
        /// <param name="m2"></param>
        /// <returns></returns>
        public static bool operator !=(Month m1, Month m2)
        {
            return !m1.Equals(m2);
        }

        /// <summary>
        /// System.ObjectのEquals
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public override bool Equals(object? value)
        {
            var other = value as Month;
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
