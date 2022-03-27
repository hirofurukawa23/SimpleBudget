using Prism.Commands;
using Prism.Mvvm;
using SB.Domain.Entities;
using static SB.Presentation.ViewModels.ExpensesListViewModel;

namespace SB.Presentation.ViewModels.ChildViewModels
{
    /// <summary>
    /// 支出一覧内の行データ用のViewModel
    /// </summary>
    public class ExpenseRowViewModel : BindableBase
    {
        ExpensesListViewModel _parent;
        Expense _expsense;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="parent"></param>
        public ExpenseRowViewModel(BindableBase parent, Expense expense)
        {
            _parent = parent as ExpensesListViewModel;
            _expsense = expense;

            //コマンド処理
            CheckedCommand = new DelegateCommand(CheckedAction);
            UnCheckedCommand = new DelegateCommand(UnCheckedAction);

            //各データプロパティをバインドする
            Checked = false;
            Id = _expsense.Id;
            ExpenseDate = _expsense.Date.DateWithSlash;
            Amount = _expsense.Yen.WithComma;
            RegisterDate = _expsense.CreatedAtWithSlash;
        }

        #region Properties

        /// <summary>
        /// チェックボックス
        /// </summary>
        public bool Checked
        {
            get => _checked;
            set => SetProperty(ref _checked, value);
        }
        private bool _checked;

        /// <summary>
        /// 支出ID
        /// </summary>
        public int? Id
        {
            get => _id;
            set => SetProperty(ref _id, value);
        }
        private int? _id;

        /// <summary>
        /// 支出日付
        /// </summary>
        public string ExpenseDate
        {
            get => _expenseDate;
            set => SetProperty(ref _expenseDate, value);
        }
        private string _expenseDate;

        /// <summary>
        /// 支出額
        /// </summary>
        public string Amount
        {
            get => _amount;
            set => SetProperty(ref _amount, value);
        }
        private string _amount;

        /// <summary>
        /// 登録日
        /// </summary>
        public string RegisterDate
        {
            get => _registerDate;
            set => SetProperty(ref _registerDate, value);
        }
        private string _registerDate;

        #endregion

        #region Commands

        /// <summary>
        /// チェックボックスのチェック
        /// </summary>
        public DelegateCommand CheckedCommand { get; private set; }
        private void CheckedAction()
        {
            _parent.EnableHandler(Event.RowCheckChanged);
        }

        /// <summary>
        /// チェックボックスのアンチェック
        /// </summary>
        public DelegateCommand UnCheckedCommand { get; private set; }
        private void UnCheckedAction()
        {
            _parent.EnableHandler(Event.RowCheckChanged);
        }

        #endregion
    }
}
