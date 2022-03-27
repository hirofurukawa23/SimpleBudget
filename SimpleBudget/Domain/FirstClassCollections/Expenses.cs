using SB.Domain.Entities;

namespace SB.Domain.FirstClassCollections
{
    public class Expenses
    {
        public Expenses(IList<Expense> list)
        {
            Datas = new List<Expense>(list);
        }

        /// <summary>
        /// 支出リストのデータ
        /// </summary>
        public List<Expense> Datas { get; }
    }
}
