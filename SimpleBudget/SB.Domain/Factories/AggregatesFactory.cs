using SB.Domain.Entities;
using SB.Domain.FirstClassCollections;

namespace SB.Domain.Factories
{
    public class AggregatesFactory
    {
        /// <summary>
        /// 集計を構築する
        /// </summary>
        /// <param name="expenses"></param>
        /// <returns></returns>
        public Aggregates CreateAggregates(IList<Aggregate> aggregates)
        {
            return new Aggregates(aggregates);
        }
    }
}
