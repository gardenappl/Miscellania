
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Materials
{
	public class WormholeCrystal : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().WormholeMirror;
		}
		
		public override void SetStaticDefaults()
		{
			ItemID.Sets.ItemNoGravity[Item.type] = true;
		}
		
		public override void SetDefaults()
		{
			Item.width = 20;
			Item.height = 20;
			//Item.rare = 8;
			Item.rare = -1;
			Item.value = Item.sellPrice(0, 1);
			Item.maxStack = 99;
			Item.alpha = 50;
		}
	}
}
