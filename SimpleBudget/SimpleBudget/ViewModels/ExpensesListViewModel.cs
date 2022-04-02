using Prism.Commands;
using Prism.Mvvm;
using Prism.Services.Dialogs;
using SB.Application.UseCases;
using SB.Domain.ValueObjects;
using SB.Presentation.Helpers;
using SB.Presentation.ViewModels.ChildViewModels;
using SB.Presentation.Views.Dialogs;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace SB.Presentation.ViewModels
{
    public class ExpensesListViewModel : BindableBase
    {
        IDialogService _dialogService;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExpensesListViewModel(IDialogService dialogService)
        {
            _dialogService = dialogService;

            //コマンド
            SearchCommand = new DelegateCommand(SearchAction);
            CreateCommand = new DelegateCommand(CreateAction);
            EditCommand = new DelegateCommand(EditAction);
            CloseCommand = new DelegateCommand(CloseAction);
            DeleteCommand = new DelegateCommand(DeleteAction);
            MonthlyAggregateCommand = new DelegateCommand(MonthlyAggregateAction);

            //最新の支出一覧を生成する
            Rows = GetExpensesList(DateFrom, DateTo);
        }

        #region Properties

        /// <summary>
        /// 画面タイトル
        /// </summary>
        public string Title => "支出一覧";

        /// <summary>
        /// 検索対象の開始日付
        /// </summary>
        public DateTime? DateFrom
        {
            get => _dateFrom;
            set => SetProperty(ref _dateFrom, value);
        }
        private DateTime? _dateFrom;

        /// <summary>
        /// 検索対象の終了日付
        /// </summary>
        public DateTime? DateTo
        {
            get => _dateTo;
            set => SetProperty(ref _dateTo, value);
        }
        private DateTime? _dateTo;

        /// <summary>
        /// 支出一覧リスト
        /// </summary>
        public ObservableCollection<ExpenseRowViewModel> Rows
        {
            get => _rows;
            set => SetProperty(ref _rows, value);
        }
        public ObservableCollection<ExpenseRowViewModel> _rows = new ObservableCollection<ExpenseRowViewModel>();

        #endregion

        #region Enable Status

        /// <summary>
        /// 編集ボタンの活性状態
        /// </summary>
        public bool EnabledEdit
        {
            get => _enabledEdit;
            set => SetProperty(ref _enabledEdit, value);
        }
        private bool _enabledEdit;

        /// <summary>
        /// 削除ボタンの活性状態
        /// </summary>
        public bool EnabledDelete
        {
            get => _enabledDelete;
            set => SetProperty(ref _enabledDelete, value);
        }
        private bool _enabledDelete;

        #endregion

        #region Commands

        /// <summary>
        /// 表示ボタン
        /// </summary>
        public DelegateCommand SearchCommand { get; private set; }
        private void SearchAction()
        {
            Rows = GetExpensesList(DateFrom, DateTo);
        }

        /// <summary>
        /// 新規作成ボタン
        /// </summary>
        public DelegateCommand CreateCommand { get; private set; }
        private void CreateAction()
        {
            ShowDialog();
        }

        /// <summary>
        /// 編集ボタン
        /// </summary>
        public DelegateCommand  EditCommand { get; private set; }
        private void EditAction()
        {
            ShowDialog();
        }

        /// <summary>
        /// 削除ボタン
        /// </summary>
        public DelegateCommand DeleteCommand { get; private set; }
        private void DeleteAction()
        {
            var targetRows = Rows.Where(x => x.Checked);
            if (!targetRows.Any()) { return; }

            bool resultFlg = true;
            var useCase = new DeleteExpenseUseCase();
            foreach(var row in targetRows)
            {
                if (row.Id is null) { continue; }
                var result = useCase.Execute(Convert.ToInt32(row.Id));
                if (!result)
                {
                    resultFlg = false;
                    break;
                }
            }
            ShowMessageBox(resultFlg);
            Rows = GetExpensesList(DateFrom, DateTo);
            EnableHandler(Event.RenewList);
        }

        /// <summary>
        /// 月次集計ボタン
        /// </summary>
        public DelegateCommand MonthlyAggregateCommand { get; private set; }
        private void MonthlyAggregateAction()
        {
            var param = new DialogParameters();
            _dialogService.ShowDialog(nameof(BudgetEvaluationListView), param, r => { });
        }

        /// <summary>
        /// 閉じるボタン
        /// </summary>
        public DelegateCommand CloseCommand { get; private set; }
        private void CloseAction()
        {
            System.Windows.Application.Current.Shutdown();
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 画面のイベント定義
        /// </summary>
        public enum Event
        {
            /// <summary>
            /// 定義なし
            /// </summary>
            NotSet = 0,
            /// <summary>
            /// 行チェック・アンチェック
            /// </summary>
            RowCheckChanged,
            /// <summary>
            /// 一覧の再描画
            /// </summary>
            RenewList,
        }

        /// <summary>
        /// 活性・非活性を制御する
        /// </summary>
        public void EnableHandler(Event e)
        {
            switch (e)
            {
                case Event.RowCheckChanged:
                case Event.RenewList:
                    var checkedRowCount = Rows.Count(x => x.Checked);
                    EnabledEdit = checkedRowCount.Equals(1) ? true : false;
                    EnabledDelete = checkedRowCount > 0 ? true : false;
                    break;

                case Event.NotSet:
                default:
                    break;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// ダイアログを表示する
        /// </summary>
        private void ShowDialog()
        {
            var param = new DialogParameters();
            var targetId = Rows.FirstOrDefault(x => x.Checked);

            //IDを持っているデータならパラメータとして追加
            if (targetId != null) { param.Add("Id", targetId.Id); }
            _dialogService.ShowDialog(nameof(ExpenseInputDialogView), param, r => { });

            //ダイアログが閉じた後は最新一覧を取得しなおす
            Rows = GetExpensesList(DateFrom, DateTo);
            EnableHandler(Event.RenewList);
        }

        /// <summary>
        /// データグリッドにバインドするデータを構築する
        /// </summary>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        private ObservableCollection<ExpenseRowViewModel> GetExpensesList(DateTime? dateFrom, DateTime? dateTo)
        {
            //ユースケースから支出データ一覧を取得する
            var useCase = new GetExpensesUseCase();
            var expenses = useCase.Execute(CreateDateValue(dateFrom), CreateDateValue(dateTo));

            //行に該当するViewModelを取得する
            var helper = new ConvertToExpenseRowHelper(this, expenses);
            return helper.GetCollection();
        }

        /// <summary>
        /// メッセージを表示する
        /// </summary>
        /// <param name="result"></param>
        private void ShowMessageBox(bool result)
        {
            var message = "削除が完了しました。";
            if (!result) { message = "削除に失敗した行があります。"; }
            MessageBox.Show(message);
        }

        /// <summary>
        /// DateTimeの値を構築する
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        private Date CreateDateValue(DateTime? date)
            => date is null ? null : new Date(date.Value.Year, date.Value.Month, date.Value.Day);

        #endregion
    }
}
