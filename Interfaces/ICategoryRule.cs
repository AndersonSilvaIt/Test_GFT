using System;

namespace Console_GFT.Interfaces
{
    public interface ICategoryRule
    {
        string GetCategory(ITrade trade, DateTime referenceDate);
    }
}
