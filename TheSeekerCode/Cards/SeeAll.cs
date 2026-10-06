using TheSeeker.TheSeekerCode.Cards;

namespace TheSeeker.TheSeekerCode.Cards;

public class SeeAll() : TheSeekerCard(1, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(5, ValueProp.Move)];

    protected override async System.Threading.Tasks.Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        int enemyCount = Owner.Creature.CombatState.HittableEnemies.Count();
        
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        await PowerCmd.Apply<HarmonyDexterityPower>(
            choiceContext,
            Owner.Creature,
            enemyCount,
            Owner.Creature,
            this,
            false
        );
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(3m);
    }
}