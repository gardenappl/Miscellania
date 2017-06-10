

using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Localization;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using GoldensMisc.Items;
using GoldensMisc.Items.Equipable;
using GoldensMisc.Items.Placeable;
using GoldensMisc.Items.Weapons;
using GoldensMisc.Projectiles;

namespace GoldensMisc
{
	public class GoldensMisc : Mod
	{
		public GoldensMisc()
		{
			Config.Load();
		}
		
		public override void Load()
		{
			if(!Main.dedServ)
			{
				MiscGlowMasks.Load();
				SkyManager.Instance["GoldensMisc:Laputa"] = new LaputaSky();
			}
			AddProjectile("MagicSpearMiniAlt", new MagicSpearMini());
			LanguageManager.Instance.OnLanguageChanged += SetNames;
			SetNames(LanguageManager.Instance);
		}
		
		void SetNames(LanguageManager manager)
		{
			GetItem<AmethystStaff>().DisplayName.AddTranslation(manager.ActiveCulture, Lang.GetItemNameValue(ItemID.AmethystStaff));
			GetItem<TopazStaff>().DisplayName.AddTranslation(manager.ActiveCulture, Lang.GetItemNameValue(ItemID.TopazStaff));
			GetItem<SapphireStaff>().DisplayName.AddTranslation(manager.ActiveCulture, Lang.GetItemNameValue(ItemID.SapphireStaff));
			GetItem<EmeraldStaff>().DisplayName.AddTranslation(manager.ActiveCulture, Lang.GetItemNameValue(ItemID.EmeraldStaff));
			GetItem<RubyStaff>().DisplayName.AddTranslation(manager.ActiveCulture, Lang.GetItemNameValue(ItemID.RubyStaff));
			GetItem<DiamondStaff>().DisplayName.AddTranslation(manager.ActiveCulture, Lang.GetItemNameValue(ItemID.DiamondStaff));
		}
		
		public override void Unload()
		{
			MiscGlowMasks.Unload();
		}
		
		public override void AddRecipeGroups()
		{
			if(Config.AncientMuramasa)
			{
				var recipeGroup = new RecipeGroup(() => Lang.misc[37] + " " + Lang.GetItemNameValue(ItemID.Muramasa), new int[]
				                                  {
				                                  	ItemID.Muramasa,
				                                  	ItemType<AncientMuramasa>()
				                                  });
				RecipeGroup.RegisterGroup("GoldensMisc:Muramasa", recipeGroup);
			}
			if(Config.AncientForges)
			{
				var recipeGroup = new RecipeGroup(() => Lang.misc[37] + " " + Lang.GetItemNameValue(ItemID.Hellforge), new int[]
				                                  {
				                                  	ItemID.Hellforge,
				                                  	ItemType<AncientHellforge>()
				                                  });
				RecipeGroup.RegisterGroup("GoldensMisc:Hellforge", recipeGroup);
			}
		}
		
		public override void PostAddRecipes()
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
					editor.AddIngredient(ItemType<NinjaGear>());
				}
			}
		}
		
		public static void Log(object message)
		{
			ErrorLogger.Log(String.Format("[Miscellania][{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), message));
		}
		
		public static void Log(string message, params object[] formatData)
		{
			ErrorLogger.Log(String.Format("[Miscellania][{0}] {1}", DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"), String.Format(message, formatData)));
		}
	}
}
