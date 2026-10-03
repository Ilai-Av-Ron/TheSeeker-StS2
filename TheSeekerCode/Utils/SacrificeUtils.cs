using System.Dynamic;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Utils;

public class SacrificeUtils
{
    public static async Task Sacrifice(PlayerChoiceContext choiceContext, CardModel? source, int amount)
    {
        if (amount < 0) return;
        var creature = source.Owner.Creature;
        decimal hpBefore = creature.CurrentHp;

        await CreatureCmd.Damage(
            choiceContext,
            creature,
            amount,
            ValueProp.Unblockable | ValueProp.Unpowered,
            creature,
            source);

        int hpLost = (int)(hpBefore - creature.CurrentHp);
        if (hpLost <= 0) return;

        await PowerCmd.Apply<SacrificePower>(
            choiceContext,
            creature,
            hpLost,
            creature,
            source,
            false);


    }
}