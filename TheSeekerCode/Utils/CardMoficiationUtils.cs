using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Models;
using TheSeeker.TheSeekerCode.CardModifiers;

namespace TheSeeker.TheSeekerCode.Utils;

public static class CardModificationUtils
{
    public static void AddBlock(CardModel card, int amount)
    {
        CardModifier.AddModifier<BonusBlockModifier>(card, amount);
        CardCmd.Preview(card);
    }
    
    public static void AddDamage(CardModel card, int amount)
    {
        card.DynamicVars.RecalculateForUpgradeOrEnchant();
        CardCmd.Preview(card);
    }
}