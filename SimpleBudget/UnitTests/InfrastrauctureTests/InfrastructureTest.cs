using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Application.Infrastructures.Repositories;
using SB.Domain.Factories;
using SB.Domain.Repositories;
using SB.Domain.ValueObjects;

namespace UnitTests.InfrastrauctureTests
{
    [TestClass]
    public class InfrastructureTest
    {
        [TestMethod]
        public void CreateExpenseTest() 
        {
            var date = new Date(2022, 3, 20);
            var memo = new Memo("test");
            var yen = new Yen(20000);

            var factory = new ExpenseFactory();
            var expense = factory.Create(date, memo, yen);

            IExpenseRepository repository = new ExpenseRepository();
            var created = repository.Create(expense);

        }
    }
}
