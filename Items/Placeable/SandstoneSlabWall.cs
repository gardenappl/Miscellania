
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Placeable
{
	public class SandstoneSlabWall : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.BuildingMaterials;
		}
		
		public override void SetDefaults()
		{
			item.width = 24;
			item.height = 24;
			item.maxStack = 999;
			item.useTurn = true;
			item.autoReuse = true;
			item.useAnimation = 15;
			item.useTime = 10;
			item.useStyle = 1;
			item.consumable = true;
			item.createWall = mod.WallType(GetType().Name);
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.SandstoneSlab, 1);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this, 4);
			recipe.AddRecipe();
			
			recipe = new ModRecipe(mod);
			recipe.AddIngredient(this, 4);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(ItemID.SandstoneSlab, 1);
			recipe.AddRecipe();
		}
		
//		public override void ModifyTooltips(List<TooltipLine> tooltips)
//		{
//			if(GoldensMisc.VanillaTweaksLoaded)
//			{
//				var modVanillaTweaks = ModLoader.GetMod("VanillaTweaks");
//				object rename = modVanillaTweaks.Call("GetConfigValue", "SandstoneRename");
//				if(rename is bool && (bool)rename)
//				{
//					int index = tooltips.FindIndex(line => line.mod == "Terraria" && line.Name == "ItemName");
//					if(index != -1)
//					{
//						if(Language.ActiveCulture == GameCulture.English)
//							tooltips[index].text = "Sand Slab Wall";
//					}
//				}
//			}
//		}
	}
}
