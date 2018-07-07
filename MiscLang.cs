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
			text.AddTranslation(GameCulture.Chinese, "取消");
			mod.AddTranslation(text);

			text = mod.CreateTranslation("HomeButton");
			text.SetDefault("Home");
			text.AddTranslation(GameCulture.Russian, "Домой");
			text.AddTranslation(GameCulture.Chinese, "回家");
			mod.AddTranslation(text);

			//text = mod.CreateTranslation("TeleportButton");
			//text.SetDefault("Teleport");
			//text.AddTranslation(GameCulture.Russian, "Телепортироваться");
			//text.AddTranslation(GameCulture.Chinese, "传送");
			//mod.AddTranslation(text);

			//text = mod.CreateTranslation("TeleportButtonNoSelect");
			//text.SetDefault("You must first select a player to teleport to.");
			//text.AddTranslation(GameCulture.Russian, "Сперва выберите игрока, к которому нужно телепортироваться.");
			//text.AddTranslation(GameCulture.Chinese, "你必须先选择一个玩家进行传送");
			//mod.AddTranslation(text);
		}
	}
}
