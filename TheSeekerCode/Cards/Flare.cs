using BaseLib.Utils;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Utils;

namespace TheSeeker.TheSeekerCode.Cards;

public class Flare() : TheSeekerCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new BlockVar(7, ValueProp.Move)];

    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var selectedAttack = await CommonActions.SelectSingleCard(
            this,
            SelectionScreenPrompt,
            choiceContext,
            PileType.Hand,
            card => card.Type == CardType.Attack
        );

        if (selectedAttack == null)
            return;
        
        CardModificationUtils.AddBlock(selectedAttack, DynamicVars.Block.IntValue);
        CardModificationUtils.addKeyword(selectedAttack, CardKeyword.Exhaust);
        CardModificationUtils.addKeyword(selectedAttack, CardKeyword.Retain);
    }
    
    private static readonly LocString SelectionScreenPrompt =
        new("card_selection", "THESEEKER-FLARE.prompt");

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3);
    }
}