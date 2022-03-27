using SB.Application.Infrastructures.Repositories;
using SB.Domain.FirstClassCollections;
using SB.Domain.Repositories;
using SB.Domain.ValueObjects;

namespace SB.Application.UseCases
{
    public class GetExpensesUseCase
    {
        IExpenseRepository _repository;

        public GetExpensesUseCase()
        {
            _repository = new ExpenseRepository();
        }

        /// <summary>
        /// 支出リストを取得するユースケースを実行する
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public Expenses Execute(Date? from, Date? to)
        {
            return _repository.FindByPeriod(from, to);
        }
    }
}
