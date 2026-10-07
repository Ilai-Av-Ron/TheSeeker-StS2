using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Powers;

public class SacrificeUtils
{
    public static Task Sacrifice(PlayerChoiceContext choiceContext, CardModel source,int amount)
    {
        return Sacrifice(choiceContext, source.Owner.Creature, amount, source);
    }

    public static async Task Sacrifice( PlayerChoiceContext choiceContext, Creature creature, int amount, CardModel? source = null)
    {
        if (amount <= 0)
            return;

        decimal hpBefore = creature.CurrentHp;

        await CreatureCmd.Damage(
            choiceContext,
            creature,
            amount,
            ValueProp.Unblockable | ValueProp.Unpowered,
            creature,
            source
        );

        int hpLost = (int)(hpBefore - creature.CurrentHp);
        if (hpLost <= 0) return;

        await PowerCmd.Apply<SacrificePower>(
            choiceContext,
            creature,
            hpLost,
            creature,
            source,
            false
        );
    }
}