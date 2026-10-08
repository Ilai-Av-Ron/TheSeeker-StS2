using TheSeeker.TheSeekerCode.Cards;

namespace TheSeeker.TheSeekerCode.Cards;

public class MethodicalLearner() : TheSeekerCard(1, CardType.Attack, CardRarity.Basic, TargetType.AnyEnemy)
{
    protected override IEnumerable<DynamicVar> CanonicalVars => [new DamageVar(8, ValueProp.Move)];

    protected override async System.Threading.Tasks.Task OnPlay(PlayerChoiceContext choiceContext, CardPlay play)
    {
        await CommonActions.CardAttack(this, play).Execute(choiceContext);
        CardModificationUtils.AddBlock(this, 3);
    }

    protected override void OnUpgrade() => DynamicVar.Damage.UpgradeValueBy(3m);
}