using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Powers;

public class SlowBurnPower() : TheSeekerPower
{
    public override MegaCrit.PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Single;
    
    public async Task AfterDisintegrationDamage(PlayerChoiceContext choiceContext, TheSeekerDisintegrationPower power, DamageResult result)
    {
        if (result.UnblockedDamage <= 0) return;
        if (result.WasTargetKilled) return;
        // Cases where a non opponent takes disintegration damage.
        if (!Owner.CombatState.GetOpponentsOf(Owner).Contains(power.Owner)) return;

        await TheSeekerDisintegrationPower.Apply(
            choiceContext,
            power.Owner,
            1m,
            Owner,
            null
        );
    }

    
}