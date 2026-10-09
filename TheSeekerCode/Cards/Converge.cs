using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Cards;

public class Converge() : TheSeekerCard(3, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy), IDisintegrationHook
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(9, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }

    public Task AfterDisintegrationDamage(PlayerChoiceContext choiceContext,
        TheSeekerDisintegrationPower power,
        DamageResult result)
    {
        if (result.UnblockedDamage <= 0) return Task.CompletedTask;
        if (result.WasTargetKilled) return Task.CompletedTask;
        // Cases where a non opponent takes disintegration damage.
        if (!Owner.Creature.CombatState.GetOpponentsOf(Owner.Creature).Contains(power.Owner)) return Task.CompletedTask;
        
        EnergyCost.AddThisCombat( -1);
        return Task.CompletedTask;
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}