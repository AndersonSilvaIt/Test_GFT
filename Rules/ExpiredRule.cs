using Console_GFT.Enums;
using Console_GFT.Interfaces;
using System;

namespace Console_GFT.Rules
{
    public class ExpiredRule : ICategoryRule
    {
        public string GetCategory(ITrade trade, DateTime referenceDate)
        {
            return (referenceDate - trade.NextPaymentDate).Days > 30 ? RuleEnum.EXPIRED.GetDescription() : null;
        }
    }
}