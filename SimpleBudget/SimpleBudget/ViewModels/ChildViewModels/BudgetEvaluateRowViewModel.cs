using Prism.Mvvm;
using SB.Domain.Entities;
using SB.Presentation.ViewModels.Dialogs;
using System.Windows.Media;

namespace SB.Presentation.ViewModels.ChildViewModels
{
    public class BudgetEvaluateRowViewModel
    {
        BudgetEvaluationListViewModel _parent;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent"></param>
        public BudgetEvaluateRowViewModel(BindableBase parent, BudgetEvaluate budgetEvaluate)
        {
            _parent = parent as BudgetEvaluationListViewModel;

            AggregateDate = budgetEvaluate.YearMonth;
            Budget = budgetEvaluate.Budget;
            Amount = budgetEvaluate.TotalExpenseAmount;
            Balance = budgetEvaluate.Balance;
            IsOverBudget = budgetEvaluate.IsOverBudget;
            ColorOfBalance = IsOverBudget ? Brushes.Red : Brushes.Black;
        }

        /// <summary>
        /// 集計月
        /// </summary>
        public string AggregateDate { get; set; }

        /// <summary>
        /// 予算額
        /// </summary>
        public string Budget { get; set; } 

        /// <summary>
        /// 支出合計
        /// </summary>
        public string Amount { get; set; }

        /// <summary>
        /// 収支
        /// </summary>
        public string Balance { get; set; }

        /// <summary>
        /// 予算をオーバーしているか
        /// </summary>
        public bool IsOverBudget { get; set; }

        /// <summary>
        /// 赤字or黒字
        /// </summary>
        public Brush ColorOfBalance { get; set; }
    }
}
