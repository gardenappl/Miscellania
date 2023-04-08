﻿
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.GameContent.Creative;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace GoldensMisc.Items.Tools
{
	public class RodofWarping : ModItem
	{

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().RodofWarping;
		}
		
		public override void SetStaticDefaults()
		{
			Item.staff[Item.type] = true;
			CreativeItemSacrificesCatalog.Instance.SacrificeCountNeededByItemId[Type] = 1;
		}
		
		public override void SetDefaults()
		{
			Item.autoReuse = false;
			Item.useStyle = ItemUseStyleID.Swing;
			Item.useAnimation = 18;
			Item.useTime = 18;
//			Item.reuseDelay = 60;
			Item.width = 38;
			Item.height = 38;
			Item.UseSound = SoundID.Item8;
			Item.rare = ItemRarityID.Red;
			Item.value = Item.sellPrice(0, 20);
		}
		
		public override bool? UseItem(Player player)
		{
			var teleportPos = new Vector2();
			teleportPos.X = Main.mouseX + Main.screenPosition.X - player.width / 2;
			teleportPos.Y = player.gravDir != 1 ? (Main.screenPosition.Y + Main.screenHeight - Main.mouseY) : (Main.mouseY + Main.screenPosition.Y - player.height);
			
			if (teleportPos.X > 50 && teleportPos.X < (double) (Main.maxTilesX * 16 - 50) && (teleportPos.Y > 50 && teleportPos.Y < (double) (Main.maxTilesY * 16 - 50)))
			{
				if (!Collision.SolidCollision(teleportPos, player.width, player.height)) //removed the Jungle Temple check because it's a post-Moon Lord tool and we don't give a damn
				{
					player.Teleport(teleportPos, 1);
					NetMessage.SendData(MessageID.Teleport, -1, -1, null, 0, (float) player.whoAmI, teleportPos.X, teleportPos.Y, 1);
					if(ModContent.GetInstance<ServerConfig>().RodofWarpingChaosState > 0f)
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
						player.AddBuff(BuffID.ChaosState, (int)Math.Floor(ModContent.GetInstance<ServerConfig>().RodofWarpingChaosState * 60));
					}
				}
			}
			return true;
		}

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Texture2D glowTexture = MiscGlowMasks.RodofWarping;
			Color color = Color.White;
			color.A = 255;
			spriteBatch.Draw(glowTexture, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D glowTexture = MiscGlowMasks.RodofWarping;
			Color color = Color.White;
			color.A = 255;
			Rectangle glowTextureSize = new(0, 0, glowTexture.Width, glowTexture.Height);
			Vector2 glowTextureOrigin = glowTexture.Size() * .5f;
			Vector2 glowOffset = new((float)(Item.width / 2) - glowTextureOrigin.X, (float)(Item.height - glowTexture.Height));
			Vector2 glowPosition = Item.position - Main.screenPosition + glowTextureOrigin + glowOffset;
			spriteBatch.Draw(glowTexture, glowPosition, glowTextureSize, color, rotation, glowTextureOrigin, scale, SpriteEffects.None, 0f);
		}

		public override void AddRecipes()
		{
			CreateRecipe()
				.AddIngredient(ItemID.RodofDiscord)
				.AddIngredient(ItemID.LunarBar, 15)
				.AddIngredient(ItemID.FragmentSolar, 10)
				.AddIngredient(ItemID.FragmentVortex, 10)
				.AddIngredient(ItemID.FragmentNebula, 10)
				.AddIngredient(ItemID.FragmentStardust, 10)
				.AddTile(TileID.LunarCraftingStation)
				.Register();
		}
	}
}
