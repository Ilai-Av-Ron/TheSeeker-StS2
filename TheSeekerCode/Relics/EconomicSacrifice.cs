using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Runs;
using TheSeeker.TheSeekerCode.Relics;

namespace TheSeeker.TheSeekerCode.Relics;

public class EconomicSacrifice() : TheSeekerRelic
{
    public override RelicRarity Rarity => RelicRarity.Common;

    public async Task AfterCurrentHpChanged(
        IRunState runState,
        ICombatState? combatState,
        Creature creature,
        Decimal delta)
    {
        if (creature != Owner.Creature) return;
        if (delta >= 0) return;

        await PlayerCmd.GainGold(delta, creature.Player, false);
    }

    
}