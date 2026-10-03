using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Cards;

public class Meditate() : TheSeekerCard(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new BlockVar(10, ValueProp.Move),
            new PowerVar<StrengthPower>(3)
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardBlock(this, play);
        await PowerCmd.Apply<MeditatePower>(
            choiceContext,
            Owner.Creature,
            DynamicVars.Strength.IntValue,
            Owner.Creature,
            this,
            false
        );
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
        DynamicVars.Strength.UpgradeValueBy(1m);
    }
}