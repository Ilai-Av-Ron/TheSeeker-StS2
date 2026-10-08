using TheSeeker.TheSeekerCode.Relics;

namespace TheSeeker.TheSeekerCode.Relics;

public class Revitalize() : TheSeekerRelic
{
    private int _playedLastTurn = 0; 
    public override RelicRarity Rarity => RelicRarity.Uncommon;
    
    public async Task AfterHarmony(bla bla bla)
    {
        if (_playedLastTurn != 0) return;
        
        Flash();
        await CreatureCmd.GainEnergyOrSomething(blabla, 1);
        _playedLastTurn = 2;
    }
    
    public override Task AfterPlayerTurnEnd(PlayerChoiceContext choiceContext, Player player)
    {
        if (player != Owner) return Task.CompletedTask;

        _playedLastTurn -= 1;
        return Task.CompletedTask;
    }
    
    
    
    
}