
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Consumable
{
	public class InertStone : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			return Config.MagicStones;
		}
		
		public override void SetDefaults()
		{
			item.name = "Inert Stone";
			item.width = 26;
			item.height = 26;
			AddTooltip2("'Has the potential to store magical energy'");
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
