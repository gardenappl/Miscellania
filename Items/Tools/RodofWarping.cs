
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Items.Materials;

namespace GoldensMisc.Items.Tools
{
	public class RodofWarping : ModItem
	{
		public override bool Autoload(ref string name)
		{
			return Config.RodofWarping;
		}
		
		public override void SetStaticDefaults()
		{
			Item.staff[item.type] = true;
		}
		
		public override void SetDefaults()
		{
			item.autoReuse = false;
			item.useStyle = 1;
			item.useAnimation = 18;
			item.useTime = 18;
//			item.reuseDelay = 60;
			item.width = 38;
			item.height = 38;
			item.UseSound = SoundID.Item8;
			item.rare = 10;
			item.value = Item.sellPrice(0, 20);
			item.glowMask = MiscGlowMasks.RodofWarping;
		}
		
		public override bool UseItem(Player player)
		{
//			player.itemTime = item.useTime;
			var teleportPos = new Vector2();
			teleportPos.X = Main.mouseX + Main.screenPosition.X - player.width / 2;
			teleportPos.Y = player.gravDir != 1 ? (Main.screenPosition.Y + Main.screenHeight - Main.mouseY) : (Main.mouseY + Main.screenPosition.Y - player.height);
			
			if (teleportPos.X > 50 && teleportPos.X < (double) (Main.maxTilesX * 16 - 50) && (teleportPos.Y > 50 && teleportPos.Y < (double) (Main.maxTilesY * 16 - 50)))
			{
				int tileX = (int) (teleportPos.X / 16f);
				int tileY = (int) (teleportPos.Y / 16f);
				if (!Collision.SolidCollision(teleportPos, player.width, player.height)) //removed the Jungle Temple check because it's a post-Moon Lord tool and we don't give a damn
				{
					player.Teleport(teleportPos, 1);
					NetMessage.SendData(65, -1, -1, null, 0, (float) player.whoAmI, teleportPos.X, teleportPos.Y, 1);
					if(Config.RodofWarpingChaosState > 0f)
					{
						if(player.chaosState)
						{
							player.statLife -= player.statLifeMax2 / 20;
							if(player.statLife <= 0)
							{
								var damageSource = PlayerDeathReason.ByOther(13);
								if(Main.rand.NextBool())
									damageSource = PlayerDeathReason.ByOther(player.Male ? 14 : 15);
								player.KillMe(damageSource, 1.0, 0);
							}
							player.lifeRegenCount = 0;
							player.lifeRegenTime = 0;
						}
						player.AddBuff(BuffID.ChaosState, (int)Math.Floor(Config.RodofWarpingChaosState * 60));
					}
				}
			}
			return true;
		}
		
		public override void AddRecipes()
		{
			var recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.RodofDiscord);
			recipe.AddIngredient(ItemID.LunarBar, 15);
			recipe.AddIngredient(ItemID.FragmentSolar, 10);
			recipe.AddIngredient(ItemID.FragmentVortex, 10);
			recipe.AddIngredient(ItemID.FragmentNebula, 10);
			recipe.AddIngredient(ItemID.FragmentStardust, 10);
			recipe.AddTile(TileID.LunarCraftingStation);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
	}
}
