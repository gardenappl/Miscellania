
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Equipable.Vanity
{
	public class CobaltDye : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().ExtraDyes;
		}

		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			Item.maxStack = 99;
			Item.value = Item.sellPrice(0, 1, 50);
			Item.rare = ItemRarityID.Orange;
		}
	}
}
