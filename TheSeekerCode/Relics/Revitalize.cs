using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using TheSeeker.TheSeekerCode.Relics;

namespace TheSeeker.TheSeekerCode.Relics;

public class Revitalize() : TheSeekerRelic, Harmony.IHarmonyHook
{
    private int _cooldown = 0; 
    public override RelicRarity Rarity => RelicRarity.Uncommon;
    
    public async Task AfterHarmonyTriggered(PlayerChoiceContext choiceContext, Player player)
    {
        if (_cooldown != 0) return;
        if (player != Owner) return;
        
        Flash();
        await PlayerCmd.GainEnergy(1m, Owner
        );
        _cooldown = 2;
    }
    
    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner) return Task.CompletedTask;
        if (_cooldown > 0) _cooldown -= 1;
        return Task.CompletedTask;
    }
}