using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;
using TheSeeker.TheSeekerCode.Utils;

namespace TheSeeker.TheSeekerCode.Cards;

public class Overreach() : TheSeekerCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new CardsVar(3)];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];


    protected override async Task OnPlay(
        PlayerChoiceContext choiceContext,
        CardPlay play)
    {
        var drawnCards = await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        await SacrificeUtils.Sacrifice(
            choiceContext,
            this,
            drawnCards.Count()
        );
        var attacksDrawnCount = drawnCards.Count(c => c.Type == CardType.Attack);
        var skillsDrawnCount = drawnCards.Count(c => c.Type == CardType.Skill);
        
        await PowerCmd.Apply<HarmonyDexterityPower>(
            choiceContext,
            Owner.Creature,
            skillsDrawnCount,
            Owner.Creature,
            null,
            false
        );
        
        await PowerCmd.Apply<HarmonyStrengthPower>(
            choiceContext,
            Owner.Creature,
            attacksDrawnCount,
            Owner.Creature,
            null,
            false
        );
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Cards.UpgradeValueBy(1);
    }
}