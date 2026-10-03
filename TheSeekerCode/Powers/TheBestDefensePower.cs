using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Powers;

public class TheBestDefensePower() : TheSeekerPower
{
    public override PowerType Type =>
        PowerType.Buff;

    public override PowerStackType StackType =>
        PowerStackType.Counter;

    public override async Task AfterCardPlayed(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner.Creature != Owner) return;
        if (cardPlay.Card.Type != CardType.Attack) return;
        
        await CreatureCmd.GainBlock(Owner, 1m, ValueProp.Move, (CardPlay) null, true);
        
    }
    
}