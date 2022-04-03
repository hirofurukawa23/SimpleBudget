using SB.Domain.FirstClassCollections;
using SB.Domain.ValueObjects;

namespace SB.Domain.Entities
{
    /// <summary>
    /// 集計の集約
    /// </summary>
    public class Aggregate
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="expenses"></param>
        public Aggregate(Expenses expenses)
        {
            if (expenses != null && expenses.Datas.Any())
            {
                Expenses = expenses;

                //日付でソートして開始日と終了日を設定する
                var ordered = expenses.Datas.OrderBy(x => x.Date.DateTime);
                FirstDate = ordered.First().Date;
                EndDate = ordered.Last().Date;

                //支出をSumしてバリューオブジェクトにする
                var sums = ordered.Sum(x => x.Yen.RelativeValue);
                TotalExpenseAmount = new Yen(sums);
            }
        }

        /// <summary>
        /// 集計開始日
        /// </summary>
        public Date FirstDate { get; private set; }

        /// <summary>
        /// 集計終了日
        /// </summary>
        public Date EndDate { get; private set; }

        /// <summary>
        /// 支出データリスト
        /// </summary>
        public Expenses Expenses { get; private set; }

        /// <summary>
        /// 期間内の合計支出金額
        /// </summary>
        public Yen TotalExpenseAmount { get; private set; } = new Yen(0);
    }
}
