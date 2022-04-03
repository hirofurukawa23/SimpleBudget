using Prism.Commands;
using Prism.Services.Dialogs;
using SB.Application.UseCases;
using SB.Domain.Factories;
using SB.Domain.ValueObjects;
using SB.Presentation.ViewModels.ChildViewModels;
using System.Collections.ObjectModel;
using System.Linq;
using static SB.Domain.FirstClassCollections.Aggregates;

namespace SB.Presentation.ViewModels.Dialogs
{
    public class BudgetEvaluationListViewModel : DialogBase
    {
        BudgetEvaluateFactory _budgetEvaluateItemFactory;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public BudgetEvaluationListViewModel() : base("支出評価一覧")
        {
            //ファクトリ
            _budgetEvaluateItemFactory = new BudgetEvaluateFactory();

            //コマンド
            CloseDialogCommand = new DelegateCommand<string>(CloseDialog);

            //月次集計を取得する
            Rows = GetEvaluateList();
        }

        #region Properties

        /// <summary>
        /// 行データ
        /// </summary>
        public ObservableCollection<BudgetEvaluateRowViewModel> Rows { get; private set; }

        #endregion

        #region Commands

        /// <summary>
        /// 閉じるボタン
        /// </summary>
        public DelegateCommand<string> CloseDialogCommand { get; private set; }
        private void CloseDialog(string parameter)
        {
            RaiseRequestClose(new DialogResult(ButtonResult.OK));
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// 月次集計の各アイテムを作成する
        /// </summary>
        /// <returns></returns>
        private ObservableCollection<BudgetEvaluateRowViewModel> GetEvaluateList()
        {
            var r = new ObservableCollection<BudgetEvaluateRowViewModel>();
            var budget = new Yen(80000);    //予算は固定で80,000円（仮）
            var aggregates = new GetAggregatesUseCase(AggregateType.Monthly).Execute();
            if (aggregates != null && aggregates.Datas.Any())
            {
                //日付逆順にして表示する
                foreach (var aggregate in aggregates.Datas.OrderByDescending(x => x.FirstDate.DateTime))
                {
                    var evaluateItem = _budgetEvaluateItemFactory.Create(aggregate, budget);
                    r.Add(new BudgetEvaluateRowViewModel(this, evaluateItem));
                }
            }
            return r;
        }

        #endregion
    }
}
