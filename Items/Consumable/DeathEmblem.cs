
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Buffs;

namespace GoldensMisc.Items.Consumable
{
	public class DeathEmblem : ModItem
	{
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Emblem of Death");
			Tooltip.SetDefault("Stare into the eyes to return to the place of your last death\n" +
			                   "Will decrease maximum HP for a short period of time");

			DisplayName.AddTranslation(GameCulture.Russian, "Эмблема смерти");
			Tooltip.AddTranslation(GameCulture.Russian, "Посмотри в глаза, чтобы вернуться в место своей последней смерти\n" +
			                       "Снизит максимальное количество здоровья на некоторое время");

			DisplayName.AddTranslation(GameCulture.Chinese, "死亡纹章");
			Tooltip.AddTranslation(GameCulture.Chinese, "凝视它的眼睛,回到你最后的死亡之地\n" +
								   "将在短时间内减少最大生命值");
//			Main.RegisterItemAnimation(item.type, new DrawAnimationVertical(10, 8));
		}
		
		public override void SetDefaults()
		{
			item.useTurn = true;
			item.width = 26;
			item.height = 18;
			item.useStyle = 4;
			item.useTime = 90;
			item.UseSound = new LegacySoundStyle(SoundID.MoonLord, 0);
			item.useAnimation = 90;
			item.rare = 4;
			item.value = Item.sellPrice(0, 0, 50);
			item.consumable = true;
			item.maxStack = 99;
		}
		
		public override bool CanUseItem(Player player)
		{
			return player.showLastDeath;
		}
		
		public override bool UseItem(Player player)
		{
			if (Main.rand.Next(2) == 0)
				Dust.NewDust(player.position, player.width, player.height, 15, 0.0f, 0.0f, 150, Color.Red, 1.1f);
			if (player.itemAnimation == item.useAnimation / 2)
			{
				for (int index = 0; index < 70; ++index)
					Dust.NewDust(player.position, player.width, player.height, 15, (float) (player.velocity.X * 0.5), (float) (player.velocity.Y * 0.5), 150, Color.Red, 1.5f);
//				player.grappling[0] = -1;
//				player.grapCount = 0;
//				for (int index = 0; index < 1000; ++index)
//				{
//					if (Main.projectile[index].active && Main.projectile[index].owner == player.whoAmI && Main.projectile[index].aiStyle == 7)
//						Main.projectile[index].Kill();
//				}
				player.Teleport(player.lastDeathPostion, -69);
				player.Center = player.lastDeathPostion;
				if(Main.netMode == NetmodeID.SinglePlayer)
					NetMessage.SendData(65, -1, -1, null, 0, (float) player.whoAmI, player.lastDeathPostion.X, player.lastDeathPostion.Y, 3);
				
				player.AddBuff(mod.BuffType<CursedMemory>(), 120 * 60);
				for (int index = 0; index < 70; ++index)
					Dust.NewDust(player.position, player.width, player.height, 15, 0.0f, 0.0f, 150, Color.Red, 1.5f);
				return true;
			}
			return false;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Bone, 15);
			recipe.AddIngredient(ItemID.SoulofNight, 3);
			recipe.AddTile(TileID.MythrilAnvil);
			recipe.SetResult(this, 1);
			recipe.AddRecipe();
		}
	}
}
