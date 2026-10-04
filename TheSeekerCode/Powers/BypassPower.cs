using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Powers;

public class BypassPower() : TheSeekerPower, IDisintegrationHook
{
    public override PowerType Type => PowerType.Buff;

    public override PowerStackType StackType => PowerStackType.Single;

    public void ModifyDisintegration(TheSeekerDisintegrationPower power, ref decimal damage, ref ValueProp props)
    {
        props |= ValueProp.Unblockable;
    }
}