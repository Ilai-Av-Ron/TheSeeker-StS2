using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheSeeker.TheSeekerCode.CardModifiers;

public class BonusBlockModifier : CardModifier, ICustomModel, ILocalizationProvider
{
    public string? LocTable => "card_modifiers";

    public List<(string, string)>? Localization =>
        new CardModifierLoc(
            "Granted Block",
            "Gain {Amount} Block when this card is played.",
            "Gain {Amount} Block."
        );

    public override bool ApplyStacked(CardModifier newApplied)
    {
        Amount += newApplied.Amount;
        return true;
    }

    public override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        if (Owner?.Owner == null)
            return;

        await CreatureCmd.GainBlock(
            Owner.Owner.Creature,
            Amount,
            ValueProp.Move,
            cardPlay
        );
    }

    public override void ModifyDescriptionPost(
        Creature? target,
        ref string description)
    {
        description += "\n" + GetLoc("extraCardText").GetFormattedText();
    }
}