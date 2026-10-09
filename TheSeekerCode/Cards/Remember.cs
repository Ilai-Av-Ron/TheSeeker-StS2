using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;

namespace TheSeeker.TheSeekerCode.Cards;

public class Remember() : TheSeekerCard(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy)
{
    private bool _gainEnergy;
    
    protected override bool ShouldGlowGoldInternal => _gainEnergy;
    
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new DamageVar(7, ValueProp.Move),
            new EnergyVar(1)
        ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        if (_gainEnergy)
        {
            _gainEnergy = false;
            await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner);
        }
    }

    protected override void OnUpgrade() => DynamicVars.Damage.UpgradeValueBy(3);
    
    public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner) return Task.CompletedTask;

        _gainEnergy = cardPlay.Card.Type == CardType.Skill;
        return Task.CompletedTask;
    }

    public override Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
    {
        if (player == Owner) _gainEnergy = false;
        return Task.CompletedTask;
    }
}