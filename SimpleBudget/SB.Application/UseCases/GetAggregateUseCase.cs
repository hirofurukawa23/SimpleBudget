using SB.Domain.Entities;
using SB.Domain.Factories;
using SB.Domain.ValueObjects;

namespace SB.Application.UseCases
{
    public class GetAggregateUseCase
    {
        GetExpensesUseCase _useCase;
        AggregateFactory _factory;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public GetAggregateUseCase()
        {
            _useCase = new GetExpensesUseCase();
            _factory = new AggregateFactory();
        }

        /// <summary>
        /// 集計集約を作成する
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public Aggregate Execute(Date? from, Date? to)
        {
            var expenses = _useCase.Execute(from, to);
            return _factory.CreateAggregate(expenses);
        }

        /// <summary>
        /// 集計集約を作成する
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public Aggregate ExecuteByYearMonth(int year, int month)
        {
            var from = new Date(year, month, 1);
            var toDate = from.DateTime.AddMonths(1).AddDays(-1);
            var to = new Date(toDate.Year, toDate.Month, toDate.Day); 
            var expenses = _useCase.Execute(from, to);
            return _factory.CreateAggregate(expenses);
        }
    }
}
