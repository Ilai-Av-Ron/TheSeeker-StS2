using BaseLib.Abstracts;
using TheSeeker.TheSeekerCode.Extensions;
using Godot;

namespace TheSeeker.TheSeekerCode.Character;

public class TheSeekerPotionPool : CustomPotionPoolModel
{
    public override Color LabOutlineColor => TheSeeker.Color;
    

    public override string BigEnergyIconPath => "charui/big_energy.png".ImagePath();
    public override string TextEnergyIconPath => "charui/text_energy.png".ImagePath();
}