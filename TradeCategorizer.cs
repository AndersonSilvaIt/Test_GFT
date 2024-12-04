using Console_GFT.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Console_GFT.Interfaces
{
    public class TradeCategorizer
    {
        private readonly List<ICategoryRule> _rules;

        public TradeCategorizer(IEnumerable<ICategoryRule> rules)
        {
            _rules = rules.ToList();
        }
        public TradeCategorizer() { }

        public string Categorize(ITrade trade, DateTime referenceDate)
        {
            foreach (var rule in _rules)
            {
                var category = rule.GetCategory(trade, referenceDate);
                if (category != null) return category;
            }

            return RuleEnum.UNCATEGORIZED.GetDescription();
        }

        public string CategorizeWithoutInterface(ITrade trade, DateTime referenceDate)
        {
            if ((referenceDate - trade.NextPaymentDate).Days > 30)
                return RuleEnum.EXPIRED.GetDescription();

            if (trade.Value > 1000000 && trade.ClientSector.Equals("Private", StringComparison.InvariantCultureIgnoreCase))
                return RuleEnum.HIGHRISK.GetDescription();

            if (trade.Value > 1000000 && trade.ClientSector.Equals("Public", StringComparison.InvariantCultureIgnoreCase))
                return RuleEnum.MEDIUMRISK.GetDescription();

            return RuleEnum.UNCATEGORIZED.GetDescription();
        }
    }
}