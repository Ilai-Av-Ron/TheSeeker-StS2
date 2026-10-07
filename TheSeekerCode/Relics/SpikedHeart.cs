using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Relics;

namespace TheSeeker.TheSeekerCode.Relics;

public class SpikedHeart() : TheSeekerRelic, ISacrificeHook
{
    public override RelicRarity Rarity => RelicRarity.Uncommon;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(4m, ValueProp.Unpowered)]; 
    
    public async Task AfterSacrifice(PlayerChoiceContext context, Creature creature, int amount)
    {
        if (creature != Owner.Creature) return;
        
        Flash();
        DamageVar damage = DynamicVars.Damage;
        await CreatureCmd.Damage(context, Owner.Creature.CombatState.HittableEnemies, damage.BaseValue, damage.Props, Owner.Creature, null);
    }
    
}