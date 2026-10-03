using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheSeeker.TheSeekerCode.CardModifiers;

public class BonusDamageModifier :
    CardModifier,
    ICustomModel
{
    public override decimal ModifyBaseDamageAdditive(
        decimal originalDamage,
        ValueProp props)
    {
        return Amount;
    }

    public override bool ApplyStacked(CardModifier newApplied)
    {
        Amount += newApplied.Amount;
        return true;
    }
}