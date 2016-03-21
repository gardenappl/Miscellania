
using System;
using Terraria;
using Terraria.ModLoader;

namespace GoldensMisc.Items
{
	public class UniversalMagnet : ModItem
	{
		public override void SetDefaults()
		{
			item.name = "Universal Magnet";
			item.width = 30;
			item.height = 32;
			AddTooltip("Increased pickup range for items");
			item.value = Item.buyPrice(0, 30);
			item.rare = 4;
			item.accessory = true;
		}
	}
}
