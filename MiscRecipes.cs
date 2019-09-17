
using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GoldensMisc.Items;
using GoldensMisc.Items.Equipable;
using GoldensMisc.Items.Placeable;
using GoldensMisc.Items.Tools;
using GoldensMisc.Items.Weapons;

namespace GoldensMisc
{
	public static class MiscRecipes
	{
		public static void AddRecipeGroups()
		{
			var recipeGroup = new RecipeGroup(() => Lang.misc[37] + " " + Lang.GetItemNameValue(ItemID.SilverBar), 
				ItemID.SilverBar,
				ItemID.TungstenBar
			);
			RecipeGroup.RegisterGroup("GoldensMisc:Silver", recipeGroup);
			if(ServerConfig.Instance.AncientMuramasa)
			{
				recipeGroup = new RecipeGroup(() => Lang.misc[37] + " " + Lang.GetItemNameValue(ItemID.Muramasa), 
					ItemID.Muramasa,
					GoldensMisc.Instance.ItemType<AncientMuramasa>()
				);
				RecipeGroup.RegisterGroup("GoldensMisc:Muramasa", recipeGroup);
			}
			if(ServerConfig.Instance.AncientForges)
			{
				recipeGroup = new RecipeGroup(() => Lang.misc[37] + " " + Lang.GetItemNameValue(ItemID.Hellforge),
					ItemID.Hellforge,
					GoldensMisc.Instance.ItemType<AncientHellforge>()
				);
				RecipeGroup.RegisterGroup("GoldensMisc:Hellforge", recipeGroup);
			}
		}
		
		public static void PostAddRecipes()
		{
			if(ServerConfig.Instance.AncientMuramasa)
			{
				var finder = new RecipeFinder();
				finder.AddIngredient(ItemID.Muramasa);
				var foundRecipes = finder.SearchRecipes();
				foreach(var foundRecipe in foundRecipes)
				{
					var editor = new RecipeEditor(foundRecipe);
					editor.AcceptRecipeGroup("GoldensMisc:Muramasa");
				}
			}
			if(ServerConfig.Instance.AncientForges)
			{
				var finder = new RecipeFinder();
				finder.AddIngredient(ItemID.Hellforge);
				var foundRecipes = finder.SearchRecipes();
				foreach(var foundRecipe in foundRecipes)
				{
					var editor = new RecipeEditor(foundRecipe);
					editor.AcceptRecipeGroup("GoldensMisc:Hellforge");
				}
			}
			if(ServerConfig.Instance.NinjaGear)
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
					editor.AddIngredient(GoldensMisc.Instance.ItemType<NinjaGear>());
				}
			}
		}
	}
}
