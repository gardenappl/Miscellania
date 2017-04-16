

using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using GoldensMisc.Items;
using GoldensMisc.Items.Equipable;
using GoldensMisc.Items.Weapons;
using GoldensMisc.Projectiles;

namespace GoldensMisc
{
	public class GoldensMisc : Mod
	{
		public override void Load()
		{
			SkyManager.Instance["GoldensMisc:Laputa"] = new LaputaSky();
			AddProjectile("MagicSpearMiniAlt", new MagicSpearMini(), "GoldensMisc/Projectiles/MagicSpearMiniAlt");
		}
		
		public override void Unload()
		{
			MiscGlowMasks.Unload();
		}
		
		public override void AddRecipeGroups()
		{
			var recipeGroup = new RecipeGroup(() => Lang.misc[37] + " " + Main.itemName[ItemID.Muramasa], new int[]
			{
				ItemID.Muramasa,
				ItemType<AncientMuramasa>()
			});
			RecipeGroup.RegisterGroup("GoldensMisc:Muramasa", recipeGroup);
		}
		
		public override void AddRecipes()
		{
			var finder = new RecipeFinder();
			finder.AddIngredient(ItemID.TigerClimbingGear);
			finder.AddIngredient(ItemID.Tabi);
			finder.AddIngredient(ItemID.BlackBelt);
			finder.SetResult(ItemID.MasterNinjaGear);
			
			var foundRecipes = finder.SearchRecipes();
			foreach(var foundRecipe in foundRecipes)
			{
				var editor = new RecipeEditor(foundRecipe);
				editor.DeleteIngredient(ItemID.Tabi);
				editor.DeleteIngredient(ItemID.BlackBelt);
				editor.AddIngredient(ItemType<NinjaGear>());
			}
			
			finder = new RecipeFinder();
			finder.AddIngredient(ItemID.Muramasa);
			foundRecipes = finder.SearchRecipes();
			Log("Found {0} recipes using the Muramasa", foundRecipes.Count);
			foreach(var foundRecipe in foundRecipes)
			{
				var editor = new RecipeEditor(foundRecipe);
				editor.AcceptRecipeGroup("GoldensMisc:Muramasa");
			}
		}
		
		public static void Log(object message)
		{
			ErrorLogger.Log("[Miscellania] " + message);
		}
		
		public static void Log(string message, params object[] formatData)
		{
			ErrorLogger.Log("[Miscellania] " + String.Format(message, formatData));
		}
	}
}
