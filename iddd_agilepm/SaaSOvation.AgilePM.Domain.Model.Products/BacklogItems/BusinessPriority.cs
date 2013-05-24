using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    public class BusinessPriority : ValueObject
    {
        public BusinessPriority(BusinessPriorityRatings ratings)
        {
            AssertionConcern.AssertArgumentNotNull(ratings, "The ratings must be provided.");
            this.Ratings = ratings;
        }

        public BusinessPriorityRatings Ratings { get; private set; }

        public float CostPercentage(BusinessPriorityTotals totals)
        {
            return (float)100 * this.Ratings.Cost / totals.TotalCost;
        }

        public float Priority(BusinessPriorityTotals totals)
        {
            var costAndRisk = CostPercentage(totals) + RiskPercentage(totals);
            return ValuePercentage(totals) / costAndRisk;
        }

        public float RiskPercentage(BusinessPriorityTotals totals)
        {
            return (float)100 * this.Ratings.Risk / totals.TotalRisk;
        }

        public float ValuePercentage(BusinessPriorityTotals totals)
        {
            return (float)100 * this.TotalValue / totals.TotalValue;
        }

        public float TotalValue
        {
            get
            {
                return this.Ratings.Benefit + this.Ratings.Penalty;
            }
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.Ratings;
        }
    }
}
