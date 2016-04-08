
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GoldensMisc.Projectiles;

namespace GoldensMisc
{
	public class GoldensMisc : Mod
	{
		public GoldensMisc()
		{
			Properties = new ModProperties
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}
		
		public override void Load()
		{
			AddProjectile("MagicSpearMiniAlt", new MagicSpearMini(), "GoldensMisc/Projectiles/MagicSpearMiniAlt");
		}
		
		public override void AddRecipes()
		{
			for(int i = 0; i < Main.recipe.Length; i++) //For each recipe in the game
			{
				if(Main.recipe[i].createItem.type == ItemID.MasterNinjaGear) //Check if the result is Master Ninja Gear
				{
					var recipe = new ModRecipe(this);
					recipe.AddIngredient(ItemID.TigerClimbingGear);
					recipe.AddIngredient(this, "NinjaGear");
					recipe.AddTile(TileID.TinkerersWorkbench);
					recipe.SetResult(ItemID.MasterNinjaGear);
					Main.recipe[i] = recipe; //Override the existing Master Ninja Gear recipe with our own
					break;
				}
			}
		}
		
		public override void Unload() //Change the recipe back
		{
			for(int i = 0; i < Main.recipe.Length; i++)
			{
				if(Main.recipe[i].createItem.type == ItemID.MasterNinjaGear)
				{
					var recipe = new ModRecipe(this);
					recipe.AddIngredient(ItemID.TigerClimbingGear);
					recipe.AddIngredient(ItemID.Tabi);
					recipe.AddIngredient(ItemID.BlackBelt);
					recipe.AddTile(TileID.TinkerersWorkbench);
					recipe.SetResult(ItemID.MasterNinjaGear);
					Main.recipe[i] = recipe;
					break;
				}
			}
		}
	}
}
