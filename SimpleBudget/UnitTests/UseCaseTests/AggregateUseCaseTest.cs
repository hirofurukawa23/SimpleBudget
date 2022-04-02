using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Application.UseCases;
using SB.Domain.ValueObjects;

namespace UnitTests.UseCaseTests
{
    /// <summary>
    /// 集計を取得するユースケースのテストクラス
    /// </summary>
    [TestClass]
    public class AggregateUseCaseTest
    {
        [TestMethod]
        public void GetAgregateUseCase1()
        {
            var useCaes = new GetAggregateUseCase();
            var aggregate = useCaes.Execute(null, null);
            Assert.IsNotNull(aggregate);
        }

        [TestMethod]
        public void GetAgregateUseCase2()
        {
            var from = new Date(2022, 2, 1);
            var to = new Date(2022, 2, 28);

            var useCaes = new GetAggregateUseCase();
            var aggregate = useCaes.Execute(from, to);
            Assert.IsNotNull(aggregate);
        }

        [TestMethod]
        public void GetAgregateUseCase3()
        {
            var useCaes = new GetAggregateUseCase();
            var aggregate = useCaes.ExecuteByYearMonth(2022, 2);
            Assert.IsNotNull(aggregate);
        }
    }
}
