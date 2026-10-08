using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using TheSeeker.TheSeekerCode.Powers;
using TheSeeker.TheSeekerCode.Relics;

namespace TheSeeker.TheSeekerCode.Relics;

public class MalevolentMantra() : TheSeekerRelic
{
    public override RelicRarity Rarity => RelicRarity.Rare;
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner)
            return;

        Flash();

        foreach (var enemy in Owner.Creature.CombatState.HittableEnemies)
        {
            await TheSeekerDisintegrationPower.Apply(
                choiceContext,
                enemy,
                1m,
                Owner.Creature,
                null
            );
        }
    }

    
}