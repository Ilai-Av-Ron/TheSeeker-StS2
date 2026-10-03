using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using TheSeeker.TheSeekerCode.Powers;

namespace TheSeeker.TheSeekerCode.Relics;

public class Harmony() : TheSeekerRelic
{
    private CardType? _previousCardType;

    private readonly Dictionary<CardPlay, HarmonyTrigger> _pendingTriggers = new();

    private enum HarmonyTrigger
    {
        Strength,
        Dexterity
    }

    public override RelicRarity Rarity => RelicRarity.Starter;

    public override Task BeforeCardPlayed(CardPlay cardPlay)
    {
        if (cardPlay.Card.Owner != Owner)
            return Task.CompletedTask;

        var currentType = cardPlay.Card.Type;

        if (currentType == CardType.Power)
        {
            _previousCardType = null;
            return Task.CompletedTask;
        }

        if (currentType is not (CardType.Attack or CardType.Skill))
            return Task.CompletedTask;

        var trigger = GetHarmonyTrigger(currentType);

        if (trigger.HasValue)
            _pendingTriggers[cardPlay] = trigger.Value;

        _previousCardType = currentType;

        return Task.CompletedTask;
    }

    public override async Task AfterCardPlayed(
        PlayerChoiceContext choiceContext,
        CardPlay cardPlay)
    {
        if (!_pendingTriggers.Remove(cardPlay, out var trigger))
            return;

        Flash();

        await ApplyHarmonyTrigger(choiceContext, trigger);
    }

    public override Task AfterPlayerTurnStart(
        PlayerChoiceContext choiceContext,
        Player player)
    {
        if (player != Owner)
            return Task.CompletedTask;

        _previousCardType = null;
        _pendingTriggers.Clear();

        return Task.CompletedTask;
    }
    
    // Switched to using triggers to make sure that cards that played other cards behaved correctly.
    private HarmonyTrigger? GetHarmonyTrigger(CardType currentType)
    {
        return (_previousCardType, currentType) switch
        {
            (CardType.Skill, CardType.Attack) => HarmonyTrigger.Dexterity,
            (CardType.Attack, CardType.Skill) => HarmonyTrigger.Strength,
            _ => null
        };
    }

    private async Task ApplyHarmonyTrigger(
        PlayerChoiceContext choiceContext,
        HarmonyTrigger trigger)
    {
        switch (trigger)
        {
            case HarmonyTrigger.Dexterity:
                await PowerCmd.Apply<HarmonyDexterityPower>(
                    choiceContext,
                    Owner.Creature,
                    1m,
                    Owner.Creature,
                    null,
                    false
                );
                break;

            case HarmonyTrigger.Strength:
                await PowerCmd.Apply<HarmonyStrengthPower>(
                    choiceContext,
                    Owner.Creature,
                    1m,
                    Owner.Creature,
                    null,
                    false
                );
                break;
        }
    }
}