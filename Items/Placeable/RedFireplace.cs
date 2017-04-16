
using System;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Placeable
{
	public class RedFireplace : ModItem
	{
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.Fireplace);
			item.name = "Red Fireplace";
			item.createTile = mod.TileType(GetType().Name);
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RedBrick, 10);
			recipe.AddRecipeGroup("Wood", 4);
			recipe.AddIngredient(ItemID.Torch, 2);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
