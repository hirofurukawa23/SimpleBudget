using SB.Application.Infrastructures.Repositories;
using SB.Domain.Entities;
using SB.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SB.Application.UseCases
{
    public class GetExpenseUseCase
    {
        IExpenseRepository _repository;

        public GetExpenseUseCase()
        {
            _repository = new ExpenseRepository();
        }

        /// <summary>
        /// 支出リストを取得するユースケースを実行する
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        public Expense Execute(int id)
        {
            return _repository.FindById(id);
        }
    }
}
