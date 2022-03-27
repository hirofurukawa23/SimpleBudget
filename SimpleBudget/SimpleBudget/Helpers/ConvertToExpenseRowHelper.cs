using Prism.Mvvm;
using SB.Domain.FirstClassCollections;
using SB.Presentation.ViewModels;
using SB.Presentation.ViewModels.ChildViewModels;
using System.Collections.ObjectModel;
using System.Linq;

namespace SB.Presentation.Helpers
{
    internal class ConvertToExpenseRowHelper
    {
        Expenses _expenses;
        ExpensesListViewModel _parent;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent"></param>
        /// <param name="expneses"></param>
        public ConvertToExpenseRowHelper(BindableBase parent, Expenses expneses)
        {
            _expenses = expneses;
            _parent = parent as ExpensesListViewModel;
        }

        /// <summary>
        /// 支出データの行ViewModelコレクションを取得する
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<ExpenseRowViewModel> GetCollection()
        {
            var r = new ObservableCollection<ExpenseRowViewModel>();
            if (_expenses is null) { return r; }

            //支出一覧の行を構築する
            foreach (var row in _expenses.Datas.OrderByDescending(x => x.Date.DateTime))
            {
                r.Add(new ExpenseRowViewModel(_parent, row));
            }
            return r;
        }
    }
}
