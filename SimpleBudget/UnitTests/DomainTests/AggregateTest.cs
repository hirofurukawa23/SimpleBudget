using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Application.UseCases;
using SB.Domain.Entities;

namespace UnitTests.DomainTests
{
    /// <summary>
    /// 集計集約のテストクラス
    /// </summary>
    [TestClass]
    public class AggregateTest
    {
        [TestMethod]
        public void AgregateTest1()
        {
            var useCase = new GetExpensesUseCase();
            var expenses = useCase.Execute(null, null);

            //集計集約を構築する
            var aggreaget = new Aggregate(expenses);
            Assert.IsNotNull(aggreaget);
        }
    }
}
