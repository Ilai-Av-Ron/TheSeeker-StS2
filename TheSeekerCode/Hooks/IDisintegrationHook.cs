using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Powers;

public interface IDisintegrationHook
{
    Task OnDisintegrationApplied(
        PlayerChoiceContext choiceContext,
        Creature applier,
        Creature target,
        decimal amount)
    {
        return Task.CompletedTask;
    }

    void ModifyDisintegration(
        TheSeekerDisintegrationPower power,
        ref decimal damage,
        ref ValueProp props)
    {
    }

    Task AfterDisintegrationDamage(
        PlayerChoiceContext choiceContext,
        TheSeekerDisintegrationPower power,
        DamageResult result)
    {
        return Task.CompletedTask;
    }
}