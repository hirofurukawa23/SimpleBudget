using SB.Domain.Entities;
using SB.Domain.FirstClassCollections;
using SB.Domain.ValueObjects;

namespace SB.Domain.Repositories
{
    public interface IExpenseRepository
    {
        /// <summary>
        /// 新規作成する
        /// </summary>
        Expense Create(Expense expense);

        /// <summary>
        /// 更新する
        /// </summary>
        Expense Update(Expense expense);

        /// <summary>
        /// 特定の支出を取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Expense FindById(int id);

        /// <summary>
        /// 特定期間の支出を取得する
        /// </summary>
        /// <param name="from">検索期間の開始日（未設定なら期間指定なし）</param>
        /// <param name="to">検索期間の終了日（未設定なら期間指定なし）</param>
        /// <returns></returns>
        Expenses FindByPeriod(Date? from, Date? to);

        /// <summary>
        /// 削除する
        /// </summary>
        bool DeleteById(int id);
    }
}
