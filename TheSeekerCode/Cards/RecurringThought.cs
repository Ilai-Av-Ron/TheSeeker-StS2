using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using TheSeeker.TheSeekerCode.Cards;
using TheSeeker.TheSeekerCode.Utils;

namespace TheSeeker.TheSeekerCode.Cards;

public class RecurringThought() : TheSeekerCard(3, CardType.Attack, CardRarity.Rare, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => 
        [
            new HpLossVar(1),
            new DamageVar(4, ValueProp.Move)
        ];
    public override IEnumerable<CardKeyword> CanonicalKeywords => [CardKeyword.Ethereal];


    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await SacrificeUtils.Sacrifice(choiceContext, this, DynamicVars.HpLoss.IntValue);
        await CommonActions.CardAttack(this, play, hitCount: 2).Execute(choiceContext);
    }
    
    public override async Task AfterAutoPostPlayPhaseEntered(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner || Pile?.Type != PileType.Discard)
            return;
        
        await CardCmd.AutoPlay(choiceContext, this, null);
    }

    protected override void OnUpgrade() => EnergyCost.UpgradeBy(-1);
}