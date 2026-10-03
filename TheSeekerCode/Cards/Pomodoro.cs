using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Utils;

namespace TheSeeker.TheSeekerCode.Cards;

public class Pomodoro() : TheSeekerCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new HpLossVar(3),
        new EnergyVar(2)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await SacrificeUtils.Sacrifice(choiceContext, this, DynamicVars.HpLoss.IntValue);
        await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext,
            Owner.Creature,
            DynamicVars.Energy.IntValue,
            Owner.Creature,
            this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.HpLoss.UpgradeValueBy(-2);
    }
}