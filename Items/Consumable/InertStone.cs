
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Consumable
{
	public class InertStone : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().MagicStones;
		}

		public override void SetDefaults()
		{
			Item.width = 26;
			Item.height = 26;
			Item.consumable = false;
			Item.rare = ItemRarityID.Orange;
			Item.value = Item.buyPrice(0, 50);
		}
	}
}
