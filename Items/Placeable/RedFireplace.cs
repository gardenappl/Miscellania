
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Placeable
{
	public class RedFireplace : ModItem
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().RedBrickFurniture;
		}
		
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Fireplace);
			Item.createTile = ModContent.TileType<Tiles.RedFireplace>();
		}
		
		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.RedBrick, 10)
				.AddRecipeGroup("Wood", 4)
				.AddIngredient(ItemID.Torch, 2)
				.AddTile(TileID.WorkBenches)
				.Register();
		}
	}
}
