

using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
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
			SkyManager.Instance["GoldensMisc:Laputa"] = new LaputaSky();
			AddProjectile("MagicSpearMiniAlt", new MagicSpearMini(), "GoldensMisc/Projectiles/MagicSpearMiniAlt");
		}
		
		public override void ChatInput(string text, ref bool broadcast)
		{
			//argument split code from ExampleMod
			if (text[0] != '/')
				return;
			int index = text.IndexOf(' ');
			string command;
			string[] args;
			if (index < 0)
			{
				command = text.Substring(1);
				args = new string[0];
			}
			else
			{
				command = text.Substring(1, index - 1);
				args = text.Substring(index).Split(new []{' '}, StringSplitOptions.RemoveEmptyEntries);
			}
			
			switch(command)
			{
				case "miscworldgen":
					if(args.Length == 1)
					{
						if(args[0].Equals("hellforge", StringComparison.OrdinalIgnoreCase))
						{
							Task.Run(() => GetModWorld<MiscWorld>().AddHellforges(new GenerationProgress(), true));
						}
						if(args[0].Equals("forge", StringComparison.OrdinalIgnoreCase))
						{
							Task.Run(() => GetModWorld<MiscWorld>().AddForges(new GenerationProgress(), true));
						}
					}
					else
					{
						Main.NewText("Usage: /miscworldgen <forge|hellforge>");
						Main.NewText("WARNING: May cause lag, as well as damage previously built/generated structures!", 255, 50, 50);
					}
					broadcast = false;
					return;
			}
		}
		
		public override void AddRecipes()
		{
			var finder = new RecipeFinder(); //Try to find Master Ninja Gear recipe
			finder.AddIngredient(ItemID.TigerClimbingGear);
			finder.AddIngredient(ItemID.Tabi);
			finder.AddIngredient(ItemID.BlackBelt);
			finder.SetResult(ItemID.MasterNinjaGear);
			
			var foundRecipes = finder.SearchRecipes();
			foreach(var foundRecipe in foundRecipes) //If a recipe was found
			{
				var editor = new RecipeEditor(foundRecipe);
				editor.DeleteIngredient(ItemID.Tabi);
				editor.DeleteIngredient(ItemID.BlackBelt);
				editor.AddIngredient(ItemType<Items.NinjaGear>());
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
