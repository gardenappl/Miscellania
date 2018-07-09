
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Consumable
{
	public class InertStone : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.MagicStones;
		}

		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.rare = 3;
			item.value = Item.buyPrice(0, 50);
		}
		
		//This is necessary to prevent Life and Mana Stones from dissapearing when using Quick Heal/Mana
		public override bool ConsumeItem(Player player)
		{
			return false;
		}
	}
}
