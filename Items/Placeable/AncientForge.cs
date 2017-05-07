
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Placeable
{
	public class AncientForge : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			return Config.AncientForges;
		}
		
		public override void SetDefaults()
		{
			item.name = "Ancient Forge";
			item.width = 30;
			item.height = 26;
			item.maxStack = 99;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = Item.sellPrice(silver: 6);
			item.createTile = mod.TileType<Tiles.AncientForge>();
		}
	}
}
