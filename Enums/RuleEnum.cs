using System.ComponentModel;

namespace Console_GFT.Enums
{
    public enum RuleEnum
    {
        [Description("EXPIRED")]
        EXPIRED,

        [Description("HIGHRISK")]
        HIGHRISK,

        [Description("MEDIUMRISK")]
        MEDIUMRISK,

        [Description("UNCATEGORIZED")]
        UNCATEGORIZED
    }
}