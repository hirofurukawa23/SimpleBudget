using Microsoft.VisualStudio.TestTools.UnitTesting;
using SB.Application.UseCases;
using System.Linq;
using static SB.Domain.FirstClassCollections.Aggregates;

namespace UnitTests.DomainTests
{
    [TestClass]
    public class AggregatesTest
    {
        [TestMethod]
        public void GetAggregatesTest1()
        {
            var useCase = new GetAggregatesUseCase(AggregateType.Yearly);
            var aggregates = useCase.Execute();
            var yearAggregate = aggregates.GetByYear(2021);
            Assert.IsTrue(yearAggregate.Expenses.Datas.All(x => x.Date.Year.Equals(2021)));
        }

        [TestMethod]
        public void GetAggregatesTest2()
        {
            var useCase = new GetAggregatesUseCase(AggregateType.Monthly);
            var aggregates = useCase.Execute();
            var yearAggregate = aggregates.GetByYearMonth(2022, 1);
            Assert.IsTrue(yearAggregate.Expenses.Datas.All(x => x.Date.Year.Equals(2022) && x.Date.Month.Equals(1)));
        }
    }
}
