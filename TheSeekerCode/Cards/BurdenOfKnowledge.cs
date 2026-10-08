using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.CommonUi;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Cards;

public class BurdenOfKnowledge() : TheSeekerCard(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self)
{
    protected override IEnumerable<DynamicVar> CanonicalVars =>
    [
        new PowerVar<TheSeekerDisintegrationPower>(4),
        new PowerVar<WeakPower>(3),
        new PowerVar<VulnerablePower>(3)
    ];

    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Exhaust];

    protected override IEnumerable<IHoverTip> ExtraHoverTips =>
    [
        HoverTipFactory.FromCard<BurdenOfKnowledgeDisintegrationChoice>(IsUpgraded),
        HoverTipFactory.FromCard<BurdenOfKnowledgeStatusChoice>(IsUpgraded)
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        var choices = new List<CardModel>
        {
            Owner.Creature.CombatState.CreateCard(ModelDb.Card<BurdenOfKnowledgeDisintegrationChoice>(), Owner),
            Owner.Creature.CombatState.CreateCard(ModelDb.Card<BurdenOfKnowledgeStatusChoice>(),Owner)
        };

        if (IsUpgraded) CardCmd.Upgrade(choices, CardPreviewStyle.None);
        var chosenCard = await CardSelectCmd.FromChooseACardScreen(choiceContext,choices, Owner);

        switch (chosenCard)
        {
            case BurdenOfKnowledgeDisintegrationChoice:
                foreach (var enemy in Owner.Creature.CombatState.HittableEnemies)
                {
                    await TheSeekerDisintegrationPower.Apply(
                        choiceContext,
                        enemy,
                        DynamicVars.Power<TheSeekerDisintegrationPower>().IntValue,
                        Owner.Creature,
                        this
                    );
                }
                break;

            case BurdenOfKnowledgeStatusChoice:
                var target = await ChooseEnemy();
                if (target is null) return;
                
                await PowerCmd.Apply<WeakPower>(
                    choiceContext,
                    target,
                    DynamicVars.Power<WeakPower>().IntValue,
                    Owner.Creature,
                    this
                );
                await PowerCmd.Apply<VulnerablePower>(
                    choiceContext,
                    target,
                    DynamicVars.Power<VulnerablePower>().IntValue,
                    Owner.Creature,
                    this
                );
                break;
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<WeakPower>().UpgradeValueBy(2);
        DynamicVars.Power<VulnerablePower>().UpgradeValueBy(2);
        DynamicVars.Power<TheSeekerDisintegrationPower>().UpgradeValueBy(2);
    }
    
    private async Task<Creature?> ChooseEnemy()
    {
        var targetManager = NTargetManager.Instance;
        var ownerNode = NCombatRoom.Instance.CreatureNodes.First(node => node.Entity == Owner.Creature);

        targetManager.StartTargeting(
            TargetType.AnyEnemy,
            ownerNode,
            TargetMode.ClickMouseToTarget,
            null,
            null
        );

        var selectedNode = await targetManager.SelectionFinished();
        return selectedNode is NCreature creatureNode
            ? creatureNode.Entity
            : null;
    }
}