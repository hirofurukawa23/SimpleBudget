using SB.Domain.Entities;
using SB.Domain.Factories;
using SB.Domain.FirstClassCollections;
using static SB.Domain.FirstClassCollections.Aggregates;

namespace SB.Application.UseCases
{
    /// <summary>
    /// 集計一覧を取得するユースケース
    /// </summary>
    public class GetAggregatesUseCase
    {
        AggregateType _type;
        GetExpensesUseCase _expensesUseCase;
        AggregatesFactory _factory;
        AggregateFactory _aggregateFactory;
        ExpensesFactory _expensesFactory;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="type"></param>
        public GetAggregatesUseCase(AggregateType type)
        {
            _type = type;
            _expensesUseCase = new GetExpensesUseCase();
            _factory = new AggregatesFactory();
            _aggregateFactory = new AggregateFactory();
            _expensesFactory = new ExpensesFactory();
        }

        /// <summary>
        /// ユースケースを実行する
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Aggregates Execute()
        {
            var expenses = _expensesUseCase.Execute(null, null);
            if (expenses is null) { return _factory.CreateAggregates(new List<Aggregate>()); }

            var groupedAggregates = GroupByAggregates(expenses);
            return _factory.CreateAggregates(groupedAggregates.ToList());
        }

        /// <summary>
        /// グループ分けした集約を取得する
        /// </summary>
        /// <param name="expenses"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        private IEnumerable<Aggregate> GroupByAggregates(Expenses expenses)
        {
            if (_type == AggregateType.Yearly)
            {
                var r = new List<Aggregate>();
                var sorted = expenses.Datas.GroupBy(x => x.Date.Year);
                foreach (var group in sorted)
                {
                    var exps = _expensesFactory.Create(group.Select(x => x).ToList());
                    var agg = _aggregateFactory.CreateAggregate(exps);
                    r.Add(agg);
                }
                return r;
            }
            else if (_type == AggregateType.Monthly)
            {
                var r = new List<Aggregate>();
                var sorted = expenses.Datas.GroupBy(x => x.Date.Year);
                foreach(var group in sorted)
                {
                    var thenGroup = group.Select(x => x).GroupBy(x => x.Date.Month);
                    foreach(var g in thenGroup)
                    {
                        var exps = _expensesFactory.Create(g.Select(x => x).ToList());
                        var agg = _aggregateFactory.CreateAggregate(exps);
                        r.Add(agg);
                    }
                }
                return r;
            }
            return new List<Aggregate>();
        }
    }
}
