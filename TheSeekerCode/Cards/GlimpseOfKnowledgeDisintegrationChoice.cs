using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Cards;

public class GlimpseOfKnowledgeDisintegrationChoice() : TheSeekerCard(0, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TheSeekerDisintegrationPower>(6)];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        return Task.CompletedTask;
    }

    protected override void OnUpgrade() => DynamicVars.Power<TheSeekerDisintegrationPower>().UpgradeValueBy(2);
}