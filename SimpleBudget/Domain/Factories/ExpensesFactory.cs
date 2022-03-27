using SB.Domain.Entities;
using SB.Domain.FirstClassCollections;

namespace SB.Domain.Factories
{
    /// <summary>
    /// 支出一覧データ作成ファクトリ
    /// </summary>
    public class ExpensesFactory
    {
        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExpensesFactory()
        {
        }

        /// <summary>
        /// 支出一覧データオブジェクトを構築する
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public Expenses Create(IList<Expense> list)
        {
            return new Expenses(list);
        }
    }
}
