using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Cards;

public class Commitment() : TheSeekerCard(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new DamageVar(23, ValueProp.Move),
            new HpLossVar(5)
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await SacrificeUtils.Sacrifice(choiceContext, this, DynamicVars.HpLoss.IntValue);
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }

    protected override void OnUpgrade() => DynamicVars.HpLoss.UpgradeValueBy(-3);
}