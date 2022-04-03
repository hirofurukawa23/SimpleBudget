using SB.Domain.ValueObjects;

namespace SB.Domain.Entities
{
    public class BudgetEvaluate
    {
        Aggregate _aggregate;
        Yen _budget;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="aggregate"></param>
        /// <param name="budget"></param>
        public BudgetEvaluate(Aggregate aggregate, Yen budget = null)
        {
            if (budget is null) { budget = new Yen(80000); }
            _budget = budget;
            _aggregate = aggregate;
        }

        /// <summary>
        /// 年月
        /// </summary>
        public string YearMonth
        {
            get
            {
               return _aggregate.FirstDate.MonthWithSlash;
            }
        }

        /// <summary>
        /// 予算額
        /// 例：8,000
        /// </summary>
        public string Budget
        {
            get
            {
                return _budget.WithComma;
            }
        }

        /// <summary>
        /// 支出額合計
        /// 例：12,000
        /// </summary>
        public string TotalExpenseAmount
        {
            get
            {
                return _aggregate.TotalExpenseAmount.WithComma;
            }
        }

        /// <summary>
        /// 合計金額
        /// </summary>
        public Yen BalanceInInt
        {
            get
            {
                return _budget.Subtract(_aggregate.TotalExpenseAmount);
            }
        }

        /// <summary>
        /// 3桁区切りの合計金額
        /// </summary>
        public string Balance
        {
            get
            {
                return BalanceInInt.WithCommaRelative;
            }
        }

        /// <summary>
        /// 予算オーバーかどうか
        /// </summary>
        public bool IsOverBudget
        {
            get
            {
                return BalanceInInt.IsNegative;
            }
        }
    }
}
