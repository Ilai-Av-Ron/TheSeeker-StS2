using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Character;
using TheSeeker.TheSeekerCode.Powers;
using BaseDisintegration = MegaCrit.Sts2.Core.Models.Cards.Disintegration;

namespace TheSeeker.TheSeekerCode.Cards;

public class GlimpseOfKnowledgeDisintegrationChoice() : TheSeekerCard(0, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy, false, false)
{
    public override CardPoolModel Pool => ModelDb.CardPool<TheSeekerCardPool>();
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TheSeekerDisintegrationPower>(6)];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        return Task.CompletedTask;
    }

    protected override void OnUpgrade() => DynamicVars.Power<TheSeekerDisintegrationPower>().UpgradeValueBy(2);
    
    public override string CustomPortraitPath => ModelDb.Card<BaseDisintegration>().PortraitPath;

}