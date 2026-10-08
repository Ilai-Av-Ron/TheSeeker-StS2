using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;

namespace TheSeeker.TheSeekerCode.Cards;

public class Praxis() : TheSeekerCard(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy)
{
    private bool _wasPlayed;
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(7m, ValueProp.Move)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        _wasPlayed = true;
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
    }
    
    public override Task AfterCombatEnd(CombatRoom room)
    {
        Refine(_wasPlayed ? 1m : -1m);
        _wasPlayed = false;

        return Task.CompletedTask;
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(4m);
    
    private void Refine(decimal amount)
    {
        var card = DeckVersion ?? this;

        card.DynamicVars.Damage.BaseValue += amount;
        CardCmd.Preview(card);
    }
}