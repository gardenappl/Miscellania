
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
	public class MiscRecipes
	{
		public static void AddRecipeGroups(GoldensMisc mod)
		{
			var recipeGroup = new RecipeGroup(() => Lang.misc[37] + " " + Lang.GetItemNameValue(ItemID.SilverBar), new int[] {
				ItemID.SilverBar,
				ItemID.TungstenBar
			});
			RecipeGroup.RegisterGroup("GoldensMisc:Silver", recipeGroup);
			if(Config.AncientMuramasa)
			{
				recipeGroup = new RecipeGroup(() => Lang.misc[37] + " " + Lang.GetItemNameValue(ItemID.Muramasa), new int[] {
					ItemID.Muramasa,
					mod.ItemType<AncientMuramasa>()
				});
				RecipeGroup.RegisterGroup("GoldensMisc:Muramasa", recipeGroup);
			}
			if(Config.AncientForges)
			{
				recipeGroup = new RecipeGroup(() => Lang.misc[37] + " " + Lang.GetItemNameValue(ItemID.Hellforge), new int[] {
					ItemID.Hellforge,
					mod.ItemType<AncientHellforge>()
				});
				RecipeGroup.RegisterGroup("GoldensMisc:Hellforge", recipeGroup);
			}
		}
		
		public static void AddRecipes(GoldensMisc mod)
		{
//			var finder = new RecipeFinder();
//			finder.SetResult(ItemID.CellPhone);
//			var foundRecipes = finder.SearchRecipes();
//			foreach(var foundRecipe in foundRecipes)
//			{
//				var editor = new RecipeEditor(foundRecipe);
//				editor.DeleteRecipe();
//			}
//			
//			ModRecipe recipe = new PhoneUpgradeRecipe(mod);
//			recipe.AddIngredient(ItemID.CellPhone);
//			recipe.AddIngredient(mod.ItemType<WormholeMirror>());
//			recipe.SetResult(ItemID.CellPhone);
//			recipe.AddRecipe();
//			
//			recipe = new WormholePhoneRecipe(mod);
//			recipe.AddIngredient(ItemID.PDA);
//			recipe.AddIngredient(ItemID.MagicMirror);
//			recipe.AddIngredient(mod.ItemType<WormholeMirror>());
//			recipe.AddTile(TileID.TinkerersWorkbench);
//			recipe.SetResult(ItemID.CellPhone);
//			recipe.AddRecipe();
//			
//			recipe = new WormholePhoneRecipe(mod);
//			recipe.AddIngredient(ItemID.PDA);
//			recipe.AddIngredient(ItemID.IceMirror);
//			recipe.AddIngredient(mod.ItemType<WormholeMirror>());
//			recipe.AddTile(TileID.TinkerersWorkbench);
//			recipe.SetResult(ItemID.CellPhone);
//			recipe.AddRecipe();
		}
		
		public static void PostAddRecipes(GoldensMisc mod)
		{
			if(Config.AncientMuramasa)
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
			if(Config.AncientForges)
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
			if(Config.NinjaGear)
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
					editor.AddIngredient(mod.ItemType<NinjaGear>());
				}
			}
		}
		
//		class WormholePhoneRecipe : ModRecipe
//		{
//			public WormholePhoneRecipe(Mod mod) : base(mod) {}
//			
//			public override void OnCraft(Item item)
//			{
//				item.GetGlobalItem<CellPhoneData>().CanWormhole = true;
//			}
//		}
//		
//		class PhoneUpgradeRecipe : WormholePhoneRecipe
//		{
//			public PhoneUpgradeRecipe(Mod mod) : base(mod) {}
//			
//			public override bool RecipeAvailable()
//			{
//				for(int i = 0; i < 58; i++)
//				{
//					var item = Main.LocalPlayer.inventory[i];
//					if(item.type == ItemID.CellPhone && !item.GetGlobalItem<CellPhoneData>().CanWormhole)
//						return true;
//				}
//				return false;
//			}
//		}
	}
}
