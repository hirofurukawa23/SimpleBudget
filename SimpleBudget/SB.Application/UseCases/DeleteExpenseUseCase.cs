using SB.Application.Infrastructures.Repositories;
using SB.Domain.Repositories;

namespace SB.Application.UseCases
{
    public class DeleteExpenseUseCase
    {
        IExpenseRepository _repository;

        /// <summary>
        /// 支出データを削除するユースケース
        /// </summary>
        public DeleteExpenseUseCase()
        {
            _repository = new ExpenseRepository();
        }

        /// <summary>
        /// ユースケースを実行する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool Execute(int id)
        {
            return _repository.DeleteById(id);
        }
    }
}
