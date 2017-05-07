
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	public class UniversalMagnet : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			return Config.Magnets;
		}
		
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
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetModPlayer<MiscPlayer>(mod).Magnet = true;
		}
	}
}
