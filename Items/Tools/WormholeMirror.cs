
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
		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.WormholeMirror;
		}
		
		public override void SetDefaults()
		{
			item.width = 22;
			item.height = 28;
			item.useStyle = 4;
			item.useTime = 90;
			item.useAnimation = 90;
			item.rare = 5;
			item.value = Item.sellPrice(0, 10);
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
			var recipe = new ModRecipe(mod);
			recipe.AddRecipeGroup("GoldensMisc:Silver", 10);
			recipe.AddIngredient(ItemID.Glass, 12);
			//recipe.AddIngredient(mod.ItemType<WormholeCrystal>(), 2);
			recipe.AddIngredient(ItemID.SoulofSight, 8);
			recipe.AddIngredient(ItemID.WormholePotion, 5);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
