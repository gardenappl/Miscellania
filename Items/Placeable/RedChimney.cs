
using System;
using System.Collections.Generic;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Placeable
{
	public class RedChimney : ModItem
	{
		public override bool Autoload(ref string name, ref string texture, IList<EquipType> equips)
		{
			return Config.RedBrickFurniture;
		}
		
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.Chimney);
			item.name = "Red Chimney";
			item.createTile = mod.TileType(GetType().Name);
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RedBrick, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
