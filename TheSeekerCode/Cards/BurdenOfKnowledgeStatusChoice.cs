using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Cards;

public class BurdenOfKnowledgeStatusChoice() : TheSeekerCard(0, CardType.Skill, CardRarity.Token, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<WeakPower>(3),
        new PowerVar<VulnerablePower>(3)
    ];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        return Task.CompletedTask;
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<WeakPower>().UpgradeValueBy(2);
        DynamicVars.Power<VulnerablePower>().UpgradeValueBy(2);
    }
}