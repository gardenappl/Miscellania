
using System;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	[AutoloadEquip(EquipType.Head)]
	public class BaseballCap : ModItem
	{
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("'How do you do, fellow kids?'");
			DisplayName.AddTranslation(GameCulture.Russian, "Бейсбольная кепка");
			Tooltip.AddTranslation(GameCulture.Russian, null);
		}
		
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 10;
			item.rare = 1;
			item.vanity = true;
			item.value = Item.sellPrice(0, 0, 50);
		}
	}
}
