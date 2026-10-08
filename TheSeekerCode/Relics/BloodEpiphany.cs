using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Rooms;
using TheSeeker.TheSeekerCode.Relics;

namespace TheSeeker.TheSeekerCode.Relics;

public class BloodEpiphany() : TheSeekerRelic, ISacrificeHook
{
    private bool _wasPlayed;
    
    public override RelicRarity Rarity => RelicRarity.Common;
    
    public async Task AfterSacrifice(PlayerChoiceContext choiceContext, Creature creature, int amount)
    {
        if (_wasPlayed) return;
        if (creature != Owner.Creature) return;
        
        Flash();
        await CreatureCmd.TriggerAnim(Owner.Creature, "Cast", Owner.Character.CastAnimDelay);
        await CreatureCmd.Heal(Owner.Creature, 3);
        _wasPlayed = true;
    }
    
    public override Task AfterCombatEnd(CombatRoom room)
    {
        _wasPlayed = false;
        return Task.CompletedTask;
    }
    
}