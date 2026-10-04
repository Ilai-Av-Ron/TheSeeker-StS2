    using MegaCrit.Sts2.Core.Entities.Cards;
    using BaseLib.Utils;
    using MegaCrit.Sts2.Core.Commands;
    using MegaCrit.Sts2.Core.GameActions.Multiplayer;
    using MegaCrit.Sts2.Core.Localization.DynamicVars;
    using MegaCrit.Sts2.Core.ValueProps;

    namespace TheSeeker.TheSeekerCode.Cards;

    public class ConjureUp() : TheSeekerCard(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
    {
        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [
            new BlockVar(1, ValueProp.Move)
        ];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
        {
            await CardPileCmd.ShuffleIfNecessary(choiceContext, Owner);
            
            
            var attacks = PileType.Draw
                .GetPile(Owner)
                .Cards
                .Where(card => card.Type == CardType.Attack)
                .ToList();

            if (attacks.Count > 0)
            {
                var attack = Owner.RunState.Rng.CombatCardSelection.NextItem(attacks);
                
                if (attack != null)
                {
                    await CardPileCmd.Add(attack, PileType.Play);
                    await CardCmd.AutoPlay(choiceContext, attack, null);
                }
            }

            await CommonActions.CardBlock(this, play);
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(3m);
        }
    }