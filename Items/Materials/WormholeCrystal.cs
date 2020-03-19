
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Materials
{
	public class WormholeCrystal : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ModContent.GetInstance<ServerConfig>().WormholeMirror;
		}
		
		public override void SetStaticDefaults()
		{
			ItemID.Sets.ItemNoGravity[item.type] = true;
		}
		
		public override void SetDefaults()
		{
			item.width = 20;
			item.height = 20;
			//item.rare = 8;
			item.rare = -1;
			item.value = Item.sellPrice(0, 1);
			item.maxStack = 99;
			item.alpha = 50;
		}
	}
}
