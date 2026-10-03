using MegaCrit.Sts2.Core.Entities.Cards;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.Core.Localization;
using TheSeeker.TheSeekerCode.Utils;


namespace TheSeeker.TheSeekerCode.Cards;

    public class Endow() : TheSeekerCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
    {
        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new DamageVar(3, ValueProp.Move),
            new BlockVar(1, ValueProp.Move)
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
        {
            await CommonActions.CardAttack(this, play, hitCount: 2).Execute(choiceContext);
            
            var attack = await CommonActions.SelectSingleCard(
                this,
                SelectionScreenPrompt,
                choiceContext,
                PileType.Discard,
                card => card.Type == CardType.Attack
            );

            if (attack != null)
            {
                CardModificationUtils.AddBlock(attack, DynamicVars.Block.IntValue);
            }
            
        }
        
        private static readonly LocString SelectionScreenPrompt =
            new("card_selection", "THESEEKER-ENDOW.prompt");

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(1m);
            DynamicVars.Block.UpgradeValueBy(1m);
        }
    }