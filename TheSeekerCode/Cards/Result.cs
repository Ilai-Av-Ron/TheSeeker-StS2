using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Cards;

public class Result() : TheSeekerCard(9, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(22, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(7);

    public override Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, Decimal amount, Creature? applier, CardModel? cardSource)
    {
        if (amount <= 0) return Task.CompletedTask;
        if (power.Owner != Owner.Creature) return Task.CompletedTask;
        if (power is not DexterityPower) return Task.CompletedTask;
        
        EnergyCost.AddThisCombat(-1);
        return Task.CompletedTask;
    }
    
}