
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Weapons
{
	public class AncientMuramasa : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.AncientMuramasa;
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Muramasa");
			DisplayName.AddTranslation(GameCulture.Russian, "Древняя Мурамаса");
		}
		
		public override void SetDefaults()
		{
			item.CloneDefaults(ItemID.Muramasa);
		}
		
		public override void MeleeEffects(Player player, Rectangle hitbox)
		{
			int dust = Dust.NewDust(new Vector2(hitbox.X, hitbox.Y), (int)(hitbox.Width * 1.25F), (int)(hitbox.Height * 1.25F), 29);
			Main.dust[dust].noGravity = true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.SetResult(ItemID.NightsEdge);
			recipe.AddIngredient(ItemID.LightsBane);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.BladeofGrass);
			recipe.AddIngredient(ItemID.FieryGreatsword);
			recipe.AddTile(TileID.DemonAltar);
			recipe.AddRecipe();
			
			recipe = new ModRecipe(mod);
			recipe.SetResult(ItemID.NightsEdge);
			recipe.AddIngredient(ItemID.BloodButcherer);
			recipe.AddIngredient(this);
			recipe.AddIngredient(ItemID.BladeofGrass);
			recipe.AddIngredient(ItemID.FieryGreatsword);
			recipe.AddTile(TileID.DemonAltar);
			recipe.AddRecipe();
		}
	}
}
