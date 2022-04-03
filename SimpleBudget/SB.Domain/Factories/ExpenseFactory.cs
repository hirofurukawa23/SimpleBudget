using SB.Domain.Entities;
using SB.Domain.ValueObjects;

namespace SB.Domain.Factories
{
    public class ExpenseFactory
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExpenseFactory()
        {
        }

        /// <summary>
        /// 支出オブジェクトを新規構築する
        /// ※IDの採番前オブジェクトを生成する
        /// </summary>
        /// <param name="date"></param>
        /// <param name="memo"></param>
        /// <param name="yen"></param>
        /// <returns></returns>
        public Expense Create(Date date, Memo memo, Yen yen, DateTime? created = null, DateTime? updated = null)
        {
            return new Expense(date, memo, yen, created: created, updated: updated);
        }

        /// <summary>
        /// 支出オブジェクトを再構築する
        /// ※IDを持っているオブジェクトを生成する
        /// </summary>
        /// <param name="date"></param>
        /// <param name="memo"></param>
        /// <param name="yen"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public Expense Recreate(Date date, Memo memo, Yen yen, int id, DateTime? created = null, DateTime? updated = null)
        {
            return new Expense(date, memo, yen, id, created, updated);
        }
    }
}
