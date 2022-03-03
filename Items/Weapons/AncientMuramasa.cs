
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.Creative;

namespace GoldensMisc.Items.Weapons
{
	public class AncientMuramasa : ModItem
	{
		public override void SetStaticDefaults()
		{
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().AncientMuramasa;
		}
		
		public override void SetDefaults()
		{
			Item.CloneDefaults(ItemID.Muramasa);
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), (int)(hitbox.Width * 1.25F), (int)(hitbox.Height * 1.25F), DustID.DungeonWater);
			Main.dust[dust].noGravity = true;
		}
		
		public override void AddRecipes()
		{
//			var recipe = new ModRecipe(mod);
//			recipe.SetResult(ItemID.NightsEdge);
//			recipe.AddIngredient(ItemID.LightsBane);
//			recipe.AddIngredient(this);
//			recipe.AddIngredient(ItemID.BladeofGrass);
//			recipe.AddIngredient(ItemID.FieryGreatsword);
//			recipe.AddTile(TileID.DemonAltar);
//			recipe.AddRecipe();
//			
//			recipe = new ModRecipe(mod);
//			recipe.SetResult(ItemID.NightsEdge);
//			recipe.AddIngredient(ItemID.BloodButcherer);
//			recipe.AddIngredient(this);
//			recipe.AddIngredient(ItemID.BladeofGrass);
//			recipe.AddIngredient(ItemID.FieryGreatsword);
//			recipe.AddTile(TileID.DemonAltar);
//			recipe.AddRecipe();
		}
	}
}
