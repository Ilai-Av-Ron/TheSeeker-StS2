using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;

namespace TheSeeker.TheSeekerCode.Utils;

public static class ChooseEnemyUtil
{
    public static async Task<Creature?> ChooseEnemy(this Player owner)
    {
        var targetManager = NTargetManager.Instance;

        var ownerNode = NCombatRoom.Instance.CreatureNodes
            .First(node => node.Entity == owner.Creature);

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