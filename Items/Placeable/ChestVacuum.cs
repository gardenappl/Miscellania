using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Placeable
{
	public class ChestVacuum : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<ServerConfig>().ChestVacuum;
		}

		public override void SetDefaults()
		{
			item.width = 28;
			item.height = 22;
			item.maxStack = 99;
			item.rare = 2;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.value = Item.buyPrice(gold: 15);
			item.createTile = mod.TileType(this.GetType().Name);
		}
	}
}
