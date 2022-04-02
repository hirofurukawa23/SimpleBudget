using SB.Domain.Entities;
using SB.Domain.FirstClassCollections;

namespace SB.Domain.Factories
{
    /// <summary>
    /// 集計の集約を構築するファクトリクラス
    /// </summary>
    public class AggregateFactory
    {
        public AggregateFactory()
        {
        }

        /// <summary>
        /// 集計を構築する
        /// </summary>
        /// <param name="expenses"></param>
        /// <returns></returns>
        public Aggregate CreateAggregate(Expenses expenses)
        {
            return new Aggregate(expenses);
        }
    }
}
