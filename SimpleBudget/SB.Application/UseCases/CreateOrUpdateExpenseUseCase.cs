using SB.Application.Infrastructures.Repositories;
using SB.Domain.Entities;
using SB.Domain.Repositories;

namespace SB.Application.UseCases
{
    public class CreateOrUpdateExpenseUseCase
    {
        IExpenseRepository _repository;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public CreateOrUpdateExpenseUseCase()
        {
            _repository = new ExpenseRepository();
        }

        /// <summary>
        /// 新規の支出エンティティを作成する
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        public Expense Execute(Expense expense)
        {
            if (expense.Id is null)
            { //新規作成
                var newEntity = _repository.Create(expense);
                return newEntity;
            }
            if (expense.Id != null)
            { //更新
                var updateEntity = _repository.Update(expense);
                return updateEntity;
            }
            return expense; //そのまま返却
        }
    }
}
