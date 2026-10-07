using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Cards;

public class PayThePrice() : TheSeekerCard(1, CardType.Skill, CardRarity.Common, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new BlockVar(11, ValueProp.Move),
            new HpLossVar(4)
        ];

    protected override async Task OnPlay( PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await PowerCmd.Apply<SacrificeNextTurnPower>(
            choiceContext,
            Owner.Creature,
            DynamicVars.HpLoss.IntValue,
            Owner.Creature,
            this,
            false
        );
    }

   
    

    protected override void OnUpgrade()
    {
        DynamicVars.HpLoss.UpgradeValueBy(-2);
    }
}