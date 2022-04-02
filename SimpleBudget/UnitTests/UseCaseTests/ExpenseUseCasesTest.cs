using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Application.UseCases;
using SB.Domain.Factories;
using SB.Domain.ValueObjects;
using System;
using System.Linq;

namespace UnitTests.UseCaseTests
{
    /// <summary>
    /// 支出入力画面のユースケーステスト
    /// </summary>
    [TestClass]
    public class ExpenseUseCasesTest
    {
        #region CreateOrUpdateExpenseUseCase

        [TestMethod]
        public void CreateNewExpenseTest()
        {
            var date = new Date(2022, 3, 3);
            var memo = new Memo("Just a memo for test3.");
            var yen = new Yen(10000);

            var entity = new ExpenseFactory().Create(date, memo, yen);

            //ExpenseEntityを作成する
            var useCase = new CreateOrUpdateExpenseUseCase();
            var newExpense = useCase.Execute(entity);

            Assert.IsNotNull(newExpense);
        }

        [TestMethod]
        public void UpdateExpenseTest()
        {
            var useCase1 = new GetExpensesUseCase();
            var expenses = useCase1.Execute(null, null);
            var obj = expenses.Datas.First();

            var entity = new ExpenseFactory().Recreate(obj.Date, obj.Memo, obj.Yen, (int)obj.Id);

            //ExpenseEntityを作成する
            var useCase = new CreateOrUpdateExpenseUseCase();
            var newExpense = useCase.Execute(entity);

            Assert.IsNotNull(newExpense);
        }

        #endregion

        #region GetExpensesUseCase

        [TestMethod]
        public void GetAllExpensesTest()
        {
            var useCase = new GetExpensesUseCase();
            //期間内すべての支出を取得する
            var expenses = useCase.Execute(null, null);
            Assert.IsTrue(expenses.Datas.Any());
        }

        [TestMethod]
        public void GetFromAndToExpensesTest()
        {
            var useCase = new GetExpensesUseCase();

            //期間内すべての支出を取得する
            var from = new Date(2022, 3, 1);
            var to = new Date(2022, 3, 31);

            var expenses = useCase.Execute(from, to);
            Assert.IsTrue(expenses.Datas.All(x => new DateTime(2022, 3, 1) <= x.Date.DateTime && x.Date.DateTime <= new DateTime(2022, 3, 31)));
        }

        [TestMethod]
        public void GetFromExpensesTest()
        {
            var useCase = new GetExpensesUseCase();

            //指定した期間以降の支出を取得する
            var from = new Date(2022, 3, 1);

            var expenses = useCase.Execute(from, null);
            Assert.IsTrue(expenses.Datas.All(x => new DateTime(2022, 3, 1) <= x.Date.DateTime));
        }

        [TestMethod]
        public void GetToExpensesTest()
        {
            var useCase = new GetExpensesUseCase();

            //指定した期間以下の支出を取得する
            var to = new Date(2022, 3, 31);

            var expenses = useCase.Execute(null, to);
            Assert.IsTrue(expenses.Datas.All(x => x.Date.DateTime <= new DateTime(2022, 3, 31)));
        }

        #endregion

        #region GetExpenseUseCase

        [TestMethod]
        public void GetExpenseUseCaseTest()
        {
            var useCase1 = new GetExpensesUseCase();
            //期間内すべての支出を取得する
            var expenses = useCase1.Execute(null, null);

            var useCase = new GetExpenseUseCase();
            var expense = useCase.Execute((int)expenses.Datas.First().Id);
            Assert.IsNotNull(expense);
        }

        #endregion

        #region DeleteExpenseUseCase

        [TestMethod]
        public void DeleteExpenseUseCaseTest()
        {
            var useCase = new DeleteExpenseUseCase();
            var result = useCase.Execute(1);

            var useCase2 = new GetExpensesUseCase();
            var list = useCase2.Execute(null, null);
            var data = list?.Datas?.FirstOrDefault(x => x.Id == 1);
            Assert.IsNull(data);
        }

        #endregion
    }
}
