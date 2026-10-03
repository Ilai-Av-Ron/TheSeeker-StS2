using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using TheSeeker.TheSeekerCode.Cards;

namespace TheSeeker.TheSeekerCode.Powers;

public class MeditateStrengthPower : TemporaryStrengthPower
{
    public override AbstractModel OriginModel => ModelDb.Card<Meditate>();
}

public class MeditatePower : TheSeekerPower
{
    public override PowerType Type => PowerType.Buff;
    public override PowerStackType StackType => PowerStackType.Counter;

    public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner.Player)
            return;

        await PowerCmd.Apply<MeditateStrengthPower>(
            choiceContext,
            Owner,
            Amount,
            Owner,
            null,
            false
        );

        await PowerCmd.Remove(this);
    }
}