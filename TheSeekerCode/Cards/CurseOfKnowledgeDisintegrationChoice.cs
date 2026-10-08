using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Character;
using TheSeeker.TheSeekerCode.Powers;
using BaseDisintegration = MegaCrit.Sts2.Core.Models.Cards.Disintegration;

namespace TheSeeker.TheSeekerCode.Cards;

public class CurseOfKnowledgeDisintegrationChoice() : TheSeekerCard(0, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy, false, false)
{
    public override CardPoolModel Pool => ModelDb.CardPool<TheSeekerCardPool>();
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TheSeekerDisintegrationPower>(10)];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        return Task.CompletedTask;
    }
    
    protected override void OnUpgrade() => DynamicVars.Power<TheSeekerDisintegrationPower>().UpgradeValueBy(3);
    
    public override string PortraitPath => ModelDb.Card<BaseDisintegration>().PortraitPath;

}