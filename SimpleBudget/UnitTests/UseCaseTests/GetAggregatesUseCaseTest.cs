using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Application.UseCases;
using static SB.Domain.FirstClassCollections.Aggregates;

namespace UnitTests.UseCaseTests
{
    /// <summary>
    /// 集約一覧を取得するユースケース
    /// </summary>
    [TestClass]
    public class GetAggregatesUseCaseTest
    {
        /// <summary>
        /// 年次の集計を取得する
        /// </summary>
        [TestMethod]
        public void GetAggregatesUseCaseTest1()
        {
            var useCase = new GetAggregatesUseCase(AggregateType.Yearly);
            var aggregates = useCase.Execute();
            Assert.IsNotNull(aggregates);
        }

        /// <summary>
        /// 月次の集計を取得する
        /// </summary>
        [TestMethod]
        public void GetAggregatesUseCaseTest2()
        {
            var useCase = new GetAggregatesUseCase(AggregateType.Monthly);
            var aggregates = useCase.Execute();
            Assert.IsNotNull(aggregates);
        }
    }
}
