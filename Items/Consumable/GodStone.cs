using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Items.Consumable
{
	public class GodStone : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.GodStone;
		}
		
		public override void SetStaticDefaults()
		{
			Tooltip.SetDefault("Can be used infintely many times\n" +
			                   "Does not cause potion sickness\n" +
			                   "This is a cheater item");
			DisplayName.AddTranslation(GameCulture.Russian, "Божественный камень");
			Tooltip.AddTranslation(GameCulture.Russian, "Не тратится при использовании\n" +
			                       "Не призводит к послезельевой болезни\n" +
			                       "Предмет для читеров");
		}
		
		public override void SetDefaults()
		{
			item.width = 26;
			item.height = 26;
			item.healLife = 1000;
			item.healMana = 500;
			item.potion = true;
			item.useStyle = 4;
			item.useTime = 30;
			item.useAnimation = 30;
			item.UseSound = SoundID.Item29;
			item.rare = 11;
		}
		
		public override bool ConsumeItem(Player player)
		{
			return false;
		}
		
		public override bool UseItem(Player player)
		{
			player.ClearBuff(BuffID.PotionSickness);
			player.potionDelay = 0;
			return true;
		}
		
		public override void HoldStyle(Player player)
		{
			player.itemLocation.X -= 10 * player.direction;
			player.itemLocation.Y += 10 * player.gravDir;
		}
	}
}
