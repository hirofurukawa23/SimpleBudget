using SB.Domain.Entities;

namespace SB.Domain.FirstClassCollections
{
    public class Aggregates
    {
        /// <summary>
        /// 集計タイプ
        /// </summary>
        public enum AggregateType
        {
            /// <summary>
            /// 年毎集計
            /// </summary>
            Yearly,
            /// <summary>
            /// 月毎集計
            /// </summary>
            Monthly,
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="aggregates"></param>
        public Aggregates(IList<Aggregate> aggregates)
        {
            Datas = new List<Aggregate>(aggregates);
            AggregatesType = AggregateType.Monthly;

            foreach(var aggr in Datas)
            {
                var firstAggregates = aggr.Expenses.Datas;
                var firstExpData = aggr.Expenses.Datas.First();
                if (firstAggregates.Any(x => !x.Date.Month.Equals(firstExpData.Date.Month)))
                { //集約内に別の月データがまぎれていたら年毎集約になる
                    AggregatesType = AggregateType.Yearly;
                    break;
                }
            }
        }

        /// <summary>
        /// 集計の一覧データ
        /// </summary>
        public List<Aggregate> Datas { get; private set; }

        /// <summary>
        /// 集約単位
        /// </summary>
        public AggregateType AggregatesType { get; private set; }

        /// <summary>
        /// 指定年の集約を取得する
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public Aggregate GetByYear(int year)
        {
            if (AggregatesType != AggregateType.Yearly) { return new Aggregate(new Expenses(new List<Expense>())); }
            var r = Datas.FirstOrDefault(x => x.FirstDate.Year.Equals(year));
            if (r is null) { return new Aggregate(new Expenses(new List<Expense>())); }
            return r;
        }

        /// <summary>
        /// 指定年の集約を取得する
        /// </summary>
        /// <param name="year"></param>
        /// <returns></returns>
        public Aggregate GetByYearMonth(int year, int month)
        {
            if (AggregatesType != AggregateType.Monthly) { return new Aggregate(new Expenses(new List<Expense>())); }
            var r = Datas.FirstOrDefault(x => x.FirstDate.Year.Equals(year) && x.FirstDate.Month.Equals(month));
            if (r is null) { return new Aggregate(new Expenses(new List<Expense>())); }
            return r;
        }
    }
}
