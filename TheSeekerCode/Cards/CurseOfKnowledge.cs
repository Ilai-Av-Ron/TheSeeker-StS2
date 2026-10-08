using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;
using TheSeeker.TheSeekerCode.Utils;

namespace TheSeeker.TheSeekerCode.Cards;

public class CurseOfKnowledge() : TheSeekerCard(3, CardType.Skill, CardRarity.Rare, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new DamageVar(10, ValueProp.Move),
        new PowerVar<TheSeekerDisintegrationPower>(10)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<CurseOfKnowledgeDisintegrationChoice>(IsUpgraded),
        HoverTipFactory.FromCard<CurseOfKnowledgeStunChoice>(IsUpgraded)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var choices = new List<CardModel>
        {
            Owner.Creature.CombatState.CreateCard(ModelDb.Card<CurseOfKnowledgeDisintegrationChoice>(), Owner),
            Owner.Creature.CombatState.CreateCard(ModelDb.Card<CurseOfKnowledgeStunChoice>(),Owner)
        };

        if (IsUpgraded) CardCmd.Upgrade(choices, CardPreviewStyle.None);
        var chosenCard = await CardSelectCmd.FromChooseACardScreen(choiceContext,choices, Owner);
        
        var target = await Owner.ChooseEnemy();
        if (target is null) return;
        
        switch (chosenCard)
        {
            case CurseOfKnowledgeDisintegrationChoice:
                await TheSeekerDisintegrationPower.Apply(
                    choiceContext,
                    target,
                    DynamicVars.Power<TheSeekerDisintegrationPower>().IntValue,
                    Owner.Creature,
                    this
                );
                break;

            case CurseOfKnowledgeStunChoice:
                await CommonActions.CardAttack(this, cardPlay).Execute(choiceContext);
                await CreatureCmd.Stun(target);
                break;
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(8);
        DynamicVars.Power<TheSeekerDisintegrationPower>().UpgradeValueBy(3);
    }
}