using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;
using BaseMindRot = MegaCrit.Sts2.Core.Models.Cards.MindRot;

namespace TheSeeker.TheSeekerCode.Cards;

public class GlimpseOfKnowledgeDamageChoice() : TheSeekerCard(0, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy, false, false)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(13, ValueProp.Move)];

    protected override Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        return Task.CompletedTask;
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(4);
    
    public override string PortraitPath => ModelDb.Card<BaseMindRot>().PortraitPath;
}