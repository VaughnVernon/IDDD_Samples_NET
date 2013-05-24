using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SaaSOvation.Common.Domain.Model;

namespace SaaSOvation.AgilePM.Domain.Model.Products.BacklogItems
{
    public class BusinessPriorityTotals : ValueObject
    {
        public BusinessPriorityTotals(int totalBenefit, int totalPenalty, int totalValue, int totalCost, int totalRisk)
        {
            this.TotalBenefit = totalBenefit;
            this.TotalPenalty = totalPenalty;
            this.TotalValue = totalValue;
            this.TotalCost = totalCost;
            this.TotalRisk = totalRisk;
        }

        public int TotalBenefit { get; private set; }
        public int TotalPenalty { get; private set; }
        public int TotalValue { get; private set; }
        public int TotalCost { get; private set; }
        public int TotalRisk { get; private set; }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return this.TotalBenefit;
            yield return this.TotalPenalty;            
            yield return this.TotalValue;
            yield return this.TotalCost;
            yield return this.TotalRisk;
        }
    }
}
