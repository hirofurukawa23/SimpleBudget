using Prism.Commands;
using Prism.Services.Dialogs;
using SB.Application.UseCases;
using SB.Domain.Entities;
using SB.Domain.Factories;
using SB.Domain.ValueObjects;
using System;
using System.Windows;

namespace SB.Presentation.ViewModels.Dialogs
{
    public class ExpenseInputDialogViewModel : DialogBase
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExpenseInputDialogViewModel() : base("支出入力")
        {
            UpdateCommand = new DelegateCommand(UpdateAction);
            ClearCommand = new DelegateCommand(ClearAction);
            CloseDialogCommand = new DelegateCommand<string>(CloseDialog);
        }

        #region Properties

        /// <summary>
        /// 支出データのId
        /// </summary>
        public int? Id { get; private set; }
        /// <summary>
        /// 作成日
        /// </summary>
        public DateTime? CreatedAt { get; private set; }
        /// <summary>
        /// 更新日
        /// </summary>
        public DateTime? UpdatedAt { get; private set; }

        /// <summary>
        /// 入力日付
        /// </summary>
        public DateTime? InputDate
        {
            get => _inputDate;
            set => SetProperty(ref _inputDate, value);
        }
        private DateTime? _inputDate;

        /// <summary>
        /// 入力金額
        /// </summary>
        public string InputAmount
        {
            get => _inputAmount;
            set => SetProperty(ref _inputAmount, value);
        }
        private string _inputAmount = "";

        /// <summary>
        /// 入力備考
        /// </summary>
        public string InputMemo
        {
            get => _inputMemo;
            set => SetProperty(ref _inputMemo, value);
        }
        private string _inputMemo = "";

        #endregion

        #region Commands

        /// <summary>
        /// 更新ボタン
        /// </summary>
        public DelegateCommand UpdateCommand { get; private set; }
        private void UpdateAction()
        {
            var expenseData = GetExpsenseObject();
            if (expenseData is null) { return; }

            //作成した支出データを更新する
            var useCase = new CreateOrUpdateExpenseUseCase();
            useCase.Execute(expenseData);

            //メッセージを表示して画面を閉じる
            MessageBox.Show("更新が完了しました");
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }

        /// <summary>
        /// クリアボタン
        /// </summary>
        public DelegateCommand ClearCommand { get; private set; }
        private void ClearAction()
        {
            ClearControl();
        }

        /// <summary>
        /// 閉じるボタン
        /// </summary>
        public DelegateCommand<string> CloseDialogCommand { get; private set; }
        private void CloseDialog(string parameter)
        {
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// 画面起動時
        /// </summary>
        /// <param name="parameters"></param>
        public override void OnDialogOpened(IDialogParameters parameters)
        {
            var id = parameters.GetValue<string>("Id");
            if (!string.IsNullOrEmpty(id) && int.TryParse(id, out var convertedId))
            { //IDを持っている場合は、IDに紐づく支出データを画面上に表示する
                var useCase = new GetExpenseUseCase();
                var expense = useCase.Execute(convertedId);
                MappingToControl(expense);
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 支出データのバリューオブジェクトを生成する
        /// </summary>
        /// <returns></returns>
        private Expense GetExpsenseObject()
        {
            if (this.InputDate is null) { return null; }

            var date = new Date(this.InputDate.Value.Year, this.InputDate.Value.Month, this.InputDate.Value.Day);
            var memo = new Memo(this.InputMemo);
            var yen = new Yen(Convert.ToInt32(this.InputAmount));
            var factory = new ExpenseFactory();
            if (this.Id is null)
            {
                return factory.Create(date, memo, yen);
            }
            else
            {
                return factory.Recreate(date, memo, yen, Convert.ToInt32(Id), CreatedAt, DateTime.Now);
            }
        }

        /// <summary>
        /// バリューオブジェクトを画面コントロールに反映する
        /// </summary>
        /// <param name="expense"></param>
        private void MappingToControl(Expense expense)
        {
            if (expense.Id != null)
            {
                Id = Convert.ToInt32(expense.Id);
            }
            InputDate = expense.Date.DateTime;
            InputAmount = expense.Yen.RelativeValue.ToString();
            InputMemo = expense.Memo.Value;
            CreatedAt = expense.CreatedAt.HasValue ? expense.CreatedAt.Value : DateTime.Now;
            UpdatedAt = expense.UpdatedAt.HasValue ? expense.UpdatedAt.Value : DateTime.Now;
        }

        /// <summary>
        /// コントロールをクリアする
        /// </summary>
        private void ClearControl()
        {
            InputDate = null;
            InputAmount = "";
            InputMemo = "";
        }

        #endregion
    }
}
