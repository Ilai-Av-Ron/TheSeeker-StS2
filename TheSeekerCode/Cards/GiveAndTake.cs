using TheSeeker.TheSeekerCode.Cards;

namespace TheSeeker.TheSeekerCode.Cards;

public class GiveAndTake() : TheSeekerCard(0, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<MegaCrit.Sts2.Core.Localization.DynamicVars.DynamicVar> CanonicalVars => 
        [
            new HpLossVar(3),
            new PowerVar<TheSeekerDisintegrationPower>(4m)
        ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override async System.Threading.Tasks.Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await SacrificeUtils.Sacrifice(choiceContext, this, DynamicVars.HpLoss.IntValue);
        await TheSeekerDisintegrationPower.Apply(
            choiceContext,
            cardPlay.Target,
            DynamicVars
                .Power<TheSeekerDisintegrationPower>()
                .IntValue,
            Owner.Creature,
            this
        );
    }

    protected override void OnUpgrade() => RemoveKeyword(CardKeyword.Exhaust);
}