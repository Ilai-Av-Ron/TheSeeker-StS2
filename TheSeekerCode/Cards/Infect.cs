using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;
using TheSeeker.TheSeekerCode.Utils;

namespace TheSeeker.TheSeekerCode.Cards;

public class Infect() : TheSeekerCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TheSeekerDisintegrationPower>(5)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        ArgumentNullException.ThrowIfNull(play.Target);
        if (!play.Target.HasPower<TheSeekerDisintegrationPower>()) return;
        
        foreach (var enemy in Owner.Creature.CombatState.HittableEnemies)
        {
            await TheSeekerDisintegrationPower.Apply(
                choiceContext,
                enemy,
                DynamicVars.Power<TheSeekerDisintegrationPower>().IntValue,
                Owner.Creature,
                this
            );
        }
    }

    protected override void OnUpgrade() => DynamicVars.Power<TheSeekerDisintegrationPower>().UpgradeValueBy(2);
}