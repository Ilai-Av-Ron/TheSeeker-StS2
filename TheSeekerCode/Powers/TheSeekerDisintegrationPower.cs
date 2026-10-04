using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;

namespace TheSeeker.TheSeekerCode.Powers;

// Sadly the original DisintegrationPower is sealed (un-inharitable).
public class TheSeekerDisintegrationPower() : TheSeekerPower
{
    public override PowerType Type => PowerType.Debuff;

    public override PowerStackType StackType => PowerStackType.Counter;

    public static async Task Apply(
        PlayerChoiceContext choiceContext,
        Creature target,
        decimal amount,
        Creature applier,
        CardModel? source)
    {
        if (amount <= 0) return;

        await PowerCmd.Apply<TheSeekerDisintegrationPower>(
            choiceContext,
            target,
            amount,
            applier,
            source,
            false
        );

        foreach (var hook in target.CombatState.IterateHookListeners().OfType<IDisintegrationHook>())
        {
            await hook.OnDisintegrationApplied(
                choiceContext,
                applier,
                target,
                amount
            );
        }
    }

    public static async Task<DamageResult> Trigger(PlayerChoiceContext choiceContext, TheSeekerDisintegrationPower power)
    {
        var combatState = power.Owner.CombatState;

        decimal damage = power.Amount;
        ValueProp props = ValueProp.Unpowered;

        // Allow anything to modify this Disintegration hit.
        foreach (var hook in combatState.IterateHookListeners().OfType<IDisintegrationHook>())
        {
            hook.ModifyDisintegration(
                power,
                ref damage,
                ref props
            );
        }

        var result = (
            await CreatureCmd.Damage(
                choiceContext,
                power.Owner,
                damage,
                props,
                power.Owner,
                null
            )
        ).Single();

        VfxCmd.PlayOnCreatureCenter(power.Owner, "vfx/vfx_attack_blunt");

        // Tell listeners what actually happened.
        foreach (var hook in combatState.IterateHookListeners().OfType<IDisintegrationHook>())
        {
            await hook.AfterDisintegrationDamage(
                choiceContext,
                power,
                result
            );
        }

        return result;
    }

    public override async Task AfterSideTurnEndLate(
        PlayerChoiceContext choiceContext,
        CombatSide side,
        IEnumerable<Creature> participants)
    {
        if (!participants.Contains(Owner))
            return;

        await Trigger(choiceContext, this);
    }
}