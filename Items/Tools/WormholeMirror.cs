
using System;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Items.Materials;

namespace GoldensMisc.Items.Tools
{
	public class WormholeMirror : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().WormholeMirror;
		}
		
		public override void SetDefaults()
		{
			Item.width = 22;
			Item.height = 28;
			Item.useStyle = ItemUseStyleID.HoldUp;
			Item.useTime = 90;
			Item.useAnimation = 90;
			Item.rare = ItemRarityID.Pink;
			Item.value = Item.sellPrice(0, 10);
		}

        public override bool CanUseItem(Player player)
        {
            //if (player.altFunctionUse == 2)
            //{
            //    if (UIWormhole.Visible)
            //        UIWormhole.Close();
            //    else
            //        UIWormhole.Open(item, false);
            //    return true;
            //}
            return false;
        }

        public override void AddRecipes()
		{
			CreateRecipe()
				.AddRecipeGroup("GoldensMisc:Silver", 10)
				.AddIngredient(ItemID.Glass, 12)
				.AddIngredient(ItemID.SoulofSight, 8)
				.AddIngredient(ItemID.WormholePotion, 5)
				.AddTile(TileID.MythrilAnvil)
				.Register();
		}
	}
}
