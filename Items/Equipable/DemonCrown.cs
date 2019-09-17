
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable
{
	[AutoloadEquip(EquipType.Face)]
	public class DemonCrown : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.DemonCrown;
		}
		
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 30;
			item.value = Item.sellPrice(0, 8);
			item.rare = 5;
			item.accessory = true;
		}
		
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.magicDamage += 0.1f;
			player.magicCrit += 7;
			player.manaCost -= 0.1f;
			player.statManaMax2 += 40;
			player.GetModPlayer<MiscPlayer>(mod).DemonCrown = true;
		}
	}
}
