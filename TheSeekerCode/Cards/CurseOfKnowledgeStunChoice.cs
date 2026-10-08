using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Character;
using TheSeeker.TheSeekerCode.Powers;
using BaseWasteAway = MegaCrit.Sts2.Core.Models.Cards.WasteAway;


namespace TheSeeker.TheSeekerCode.Cards;

public class CurseOfKnowledgeStunChoice() : TheSeekerCard(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy, false, false)
{
    public override CardPoolModel Pool => ModelDb.CardPool<TheSeekerCardPool>();
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(10, ValueProp.Move)];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        return Task.CompletedTask;
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(8);
    
    public override string PortraitPath => ModelDb.Card<BaseWasteAway>().PortraitPath;

}