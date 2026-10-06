using TheSeeker.TheSeekerCode.Cards;

namespace TheSeeker.TheSeekerCode.Cards;

public class SlowBurn() : TheSeekerCard(2, CardType.Power, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await PowerCmd.Apply<SlowBurnPower>(
            choiceContext,
            Owner.Creature,
            1m,
            Owner.Creature,
            this,
            false
        );
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}