using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Terraria.Localization;

namespace GoldensMisc
{
	public static class MiscLang
	{
		public static void AddText()
		{
			var mod = GoldensMisc.Instance;

			var text = mod.CreateTranslation("CloseButton");
			text.SetDefault("Close");
			text.AddTranslation(GameCulture.Russian, "Закрыть");
			mod.AddTranslation(text);

			text = mod.CreateTranslation("HomeButton");
			text.SetDefault("Home");
			text.AddTranslation(GameCulture.Russian, "Домой");
			mod.AddTranslation(text);
			//text = mod.CreateTranslation("TeleportButton");
			//text.SetDefault("Teleport");
			//text.AddTranslation(GameCulture.Russian, "Телепортироваться");
			//mod.AddTranslation(text);

			//text = mod.CreateTranslation("TeleportButtonNoSelect");
			//text.SetDefault("You must first select a player to teleport to.");
			//text.AddTranslation(GameCulture.Russian, "Сперва выберите игрока, к которому нужно телепортироваться.");
			//mod.AddTranslation(text);
		}
	}
}
