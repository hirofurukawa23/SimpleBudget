using SB.Domain.Entities;
using SB.Domain.Factories;
using SB.Domain.FirstClassCollections;
using SB.Domain.Repositories;
using SB.Domain.ValueObjects;
using SB.Infrastructures;
using SB.Infrastructures.Tables;
using SQLite;

namespace SB.Application.Infrastructures.Repositories
{
    public class ExpenseRepository : IExpenseRepository
    {
        ExpenseFactory _expenseFactory;
        ExpensesFactory _expensesFactory;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public ExpenseRepository()
        {
            _expenseFactory = new ExpenseFactory();
            _expensesFactory = new ExpensesFactory();
        }

        /// <summary>
        /// 支出データを新規作成する
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        public Expense Create(Expense expense)
        {
            if (expense.Id != null)
            {
                throw new ArgumentException("新規の場合のみ有効です。");
            }

            int newId = GenerateNewId();
            var updateExpense = MappingToTable(expense, newId);
            updateExpense.CreateDate = DateTime.Now; //作成日付を更新する
            updateExpense.UpdateDate = DateTime.Now; //更新日付を更新する

            ExpenseTable newItem;
            using (var con = new SQLiteConnection(SqliteCore.GetDbPath()))
            {
                con.CreateTable<ExpenseTable>();
                con.Insert(updateExpense);
                newItem = con.Table<ExpenseTable>().FirstOrDefault(x => x.Id.Equals(newId));
            }
            //再構築して返却する
            return _expenseFactory.Recreate
                (
                    new Date(newItem.DateTime.Year, newItem.DateTime.Month, newItem.DateTime.Day),
                    new Memo(newItem.Memo),
                    new Yen(newItem.Yen), 
                    newItem.Id,
                    newItem.CreateDate,
                    newItem.UpdateDate
                );
        }

        /// <summary>
        /// 支出データを更新する
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        public Expense Update(Expense expense)
        {
            if (expense.Id is null)
            {
                throw new ArgumentException("更新の場合のみ有効です。"); 
            }

            var updateExpense = MappingToTable(expense, (int)expense.Id);
            updateExpense.UpdateDate = DateTime.Now; //更新日付を更新する

            ExpenseTable updateItem;
            using (var con = new SQLiteConnection(SqliteCore.GetDbPath()))
            {
                con.CreateTable<ExpenseTable>();
                con.Update(updateExpense);
                updateItem = con.Table<ExpenseTable>().FirstOrDefault(x => x.Id.Equals((int)expense.Id));
            }
            //再構築して返却する
            return _expenseFactory.Recreate
                (
                    new Date(updateItem.DateTime.Year, updateItem.DateTime.Month, updateItem.DateTime.Day),
                    new Memo(updateItem.Memo),
                    new Yen(updateItem.Yen),
                    updateItem.Id,
                    updateItem.CreateDate,
                    updateItem.UpdateDate
                );
        }

        /// <summary>
        /// ID指定で取得する
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Expense FindById(int id)
        {
            using (var con = new SQLiteConnection(SqliteCore.GetDbPath()))
            {
                con.CreateTable<ExpenseTable>();
                var rows = con.Query<ExpenseTable>(String.Format(
@"SELECT
    *
FROM
    ExpenseTable
WHERE
    ID = {0}
", id));
                if (rows.Any())
                {
                    return _expenseFactory.Recreate
                    (
                        new Date(rows[0].DateTime.Year, rows[0].DateTime.Month, rows[0].DateTime.Day),
                        new Memo(rows[0].Memo),
                        new Yen(rows[0].Yen),
                        id,
                        rows[0].CreateDate,
                        rows[0].UpdateDate
                    );
                }
                return null;
            }
        }

        /// <summary>
        /// 指定期間で検索する
        /// </summary>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public Expenses FindByPeriod(Date? from, Date? to)
        {
            using (var con = new SQLiteConnection(SqliteCore.GetDbPath()))
            {
                con.CreateTable<ExpenseTable>();
                var rows = con.Query<ExpenseTable>(
@"SELECT
    *
FROM
    EXPENSETABLE
ORDER BY
    DATETIME
");
                if (rows.Any())
                {
                    var expenses = new List<Expense>();
                    foreach (var row in rows)
                    {
                        if (from != null && row.DateTime < from.DateTime)
                        {
                            continue;
                        }
                        if (to != null && row.DateTime > to.DateTime)
                        {
                            continue;
                        }
                        expenses.Add(_expenseFactory.Recreate
                        (
                            new Date(row.DateTime.Year, row.DateTime.Month, row.DateTime.Day),
                            new Memo(row.Memo),
                            new Yen(row.Yen),
                            row.Id,
                            row.CreateDate,
                            row.UpdateDate
                        ));
                    }
                    return _expensesFactory.Create(expenses);
                }
                return null;
            }
        }

        /// <summary>
        /// 削除する
        /// </summary>
        /// <param name="expense"></param>
        /// <returns></returns>
        public bool DeleteById(int id)
        {
            using (var con = new SQLiteConnection(SqliteCore.GetDbPath()))
            {
                con.CreateTable<ExpenseTable>();
                var rows = con.Query<ExpenseTable>(String.Format(
@"DELETE
FROM
    ExpenseTable
WHERE
    ID = {0}
", id));
                return rows.Count.Equals(0) ? true : false;
            }
        }

        #region Private Method

        /// <summary>
        /// ExpenseTableにマッピングする
        /// </summary>
        /// <param name="expense"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        private ExpenseTable MappingToTable(Expense expense, int id)
        {
            return new ExpenseTable()
            {
                DateTime = new DateTime(expense.Date.Year, expense.Date.Month, expense.Date.Day),
                Memo = expense.Memo.Value,
                Yen = expense.Yen.RelativeValue,
                Id = id,
                CreateDate = expense.CreatedAt.HasValue ? expense.CreatedAt.Value : DateTime.Now,
                UpdateDate = expense.UpdatedAt.HasValue ? expense.UpdatedAt.Value : DateTime.Now,
            };
        }

        /// <summary>
        /// 新規IDを採番する
        /// </summary>
        /// <returns></returns>
        private int GenerateNewId()
        {
            int maxId = 0;
            using (var con = new SQLiteConnection(SqliteCore.GetDbPath()))
            {
                con.CreateTable<ExpenseTable>();
                var rows = con.Query<ExpenseTable>(
@"SELECT
    *
FROM
    EXPENSETABLE
WHERE
    ID = (SELECT MAX(ID) FROM EXPENSETABLE)
");
                if (rows.Any())
                {
                    maxId = rows[0].Id;
                }
            }
            return maxId + 1;
        }

        #endregion
    }
}
