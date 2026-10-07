using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Cards;

public class GiveAndTake() : TheSeekerCard(0, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVar> CanonicalVars => 
        [
            new HpLossVar(3),
            new PowerVar<TheSeekerDisintegrationPower>(4m)
        ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async System.Threading.Tasks.Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardplay)
    {
        await SacrificeUtils.Sacrifice(choiceContext, this, DynamicVars.HpLoss.IntValue);
        foreach (var enemy in Owner.Creature.CombatState.HittableEnemies)
        {
            await TheSeekerDisintegrationPower.Apply(
                choiceContext,
                enemy,
                DynamicVars
                    .Power<TheSeekerDisintegrationPower>()
                    .IntValue,
                Owner.Creature,
                this
            );
        }
    }

    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Exhaust);
}