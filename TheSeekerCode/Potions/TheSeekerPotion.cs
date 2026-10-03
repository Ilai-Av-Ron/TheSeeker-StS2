using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using TheSeeker.TheSeekerCode.Character;
using TheSeeker.TheSeekerCode.Extensions;

namespace TheSeeker.TheSeekerCode.Potions;

[Pool(typeof(TheSeekerPotionPool))]
public abstract class TheSeekerPotion : CustomPotionModel
{
	public override string? CustomPackedImagePath =>
		$"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();
	public override string? CustomPackedOutlinePath =>
		$"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionOutlineImagePath();
}