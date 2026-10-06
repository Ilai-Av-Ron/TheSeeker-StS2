using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Powers;

public class SacrificeNextTurnPower() : TheSeekerPower
{
    public override PowerType Type => PowerType.Debuff;
    public override PowerStackType StackType => PowerStackType.Counter;
    
    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player)
            return;

        await SacrificeUtils.Sacrifice(choiceContext, Owner, Amount);
        await PowerCmd.Remove(this);
    }

    
}