using BaseLib.Abstracts;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheSeeker.TheSeekerCode.CardModifiers;

public class BonusBlockModifier : CardModifier, ICustomModel, ILocalizationProvider
{
    private const string BlockKey = "BonusBlock";
    public string? LocTable => "card_modifiers";

    public List<(string, string)>? Localization =>
        new CardModifierLoc(
            "Granted Block",
            "Gain {BonusBlock:diff()} Block when this card is played.",
            "Gain {BonusBlock:diff()} Block."
        );

    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new BlockVar(BlockKey, Amount, ValueProp.Move)
    ];
    
    public override void OnInitialApplication()
    {
        DynamicVars[BlockKey].BaseValue = Amount;
    }

    public override bool ApplyStacked(CardModifier newApplied)
    {
        Amount += newApplied.Amount;

        DynamicVars[BlockKey].BaseValue = Amount;

        return true;
    }

    public override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        if (Owner == null)
            return;

        await CommonActions.CardBlock(
            Owner,
            DynamicVars[BlockKey],
            cardPlay
        );
    }

    public override void ModifyDescriptionPost(
        Creature? target,
        ref string description)
    {
        if (Owner != null)
        {
            bool runGlobalHooks =
                Owner.CombatState != null &&
                Owner.Pile?.Type is PileType.Hand or PileType.Play;

            DynamicVars[BlockKey].UpdateCardPreview(
                Owner,
                CardPreviewMode.Normal,
                target,
                runGlobalHooks
            );
        }

        description += "\n" +
                       GetLoc("extraCardText").GetFormattedText();
    }
}