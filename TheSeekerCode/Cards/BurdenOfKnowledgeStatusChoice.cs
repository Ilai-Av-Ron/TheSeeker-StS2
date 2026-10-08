using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Character;
using TheSeeker.TheSeekerCode.Powers;
using BaseMindRot = MegaCrit.Sts2.Core.Models.Cards.Sloth;

namespace TheSeeker.TheSeekerCode.Cards;

public class BurdenOfKnowledgeStatusChoice() : TheSeekerCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy, false, false)
{
    public override CardPoolModel Pool => ModelDb.CardPool<TheSeekerCardPool>();
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
    
    public override string PortraitPath => ModelDb.Card<BaseMindRot>().PortraitPath;

}