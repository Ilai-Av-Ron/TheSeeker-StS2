using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;
using BaseDisintegration = MegaCrit.Sts2.Core.Models.Cards.Disintegration;


namespace TheSeeker.TheSeekerCode.Cards;

public class BurdenOfKnowledgeDisintegrationChoice() : TheSeekerCard(0, CardType.Skill, CardRarity.Uncommon, TargetType.AllEnemies, false, false)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<TheSeekerDisintegrationPower>(4)];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        return Task.CompletedTask;
    }

    protected override void OnUpgrade() => DynamicVars.Power<TheSeekerDisintegrationPower>().UpgradeValueBy(2);
    
    public override string PortraitPath => ModelDb.Card<BaseDisintegration>().PortraitPath;

}