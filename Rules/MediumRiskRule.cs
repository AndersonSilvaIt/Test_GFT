using Console_GFT.Enums;
using Console_GFT.Interfaces;
using System;

namespace Console_GFT.Rules
{
    public class MediumRiskRule : ICategoryRule
    {
        public string GetCategory(ITrade trade, DateTime referenceDate)
        {
            return trade.Value > 1000000 && trade.ClientSector.Equals("Public", StringComparison.InvariantCultureIgnoreCase) ? RuleEnum.MEDIUMRISK.GetDescription() : null;
        }
    }
}
