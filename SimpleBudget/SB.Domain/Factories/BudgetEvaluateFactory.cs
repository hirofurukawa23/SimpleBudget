using SB.Domain.Entities;
using SB.Domain.ValueObjects;

namespace SB.Domain.Factories
{
    public class BudgetEvaluateFactory
    {
        public BudgetEvaluateFactory()
        {
        }

        /// <summary>
        /// 作成する
        /// </summary>
        /// <param name="aggregate"></param>
        /// <param name="budget"></param>
        /// <returns></returns>
        public BudgetEvaluate Create(Aggregate aggregate, Yen budget = null)
        {
            return new BudgetEvaluate(aggregate, budget);
        }
    }
}
