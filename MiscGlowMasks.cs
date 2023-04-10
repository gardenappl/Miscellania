
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace GoldensMisc
{
	public static class MiscGlowMasks
	{
		public static Texture2D UndyingSpear;
		public static Texture2D UndyingSpearProjectile;
		public static Texture2D RodofWarping;
		public static bool Loaded;
		
		public static void Load()
		{
			if (!Loaded)
            {
				UndyingSpear = ModContent.Request<Texture2D>("GoldensMisc/Items/Weapons/UndyingSpear_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
				UndyingSpearProjectile = ModContent.Request<Texture2D>("GoldensMisc/Projectiles/UndyingSpear_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
				RodofWarping = ModContent.Request<Texture2D>("GoldensMisc/Items/Tools/RodofWarping_Glow", ReLogic.Content.AssetRequestMode.ImmediateLoad).Value;
				Loaded = true;
			}
			
		}
		
		public static void Unload()
		{
			if(Loaded)
			{
				UndyingSpear = null;
				UndyingSpearProjectile = null;
				RodofWarping = null;
				Loaded = false;
			}
		}

	}

	public abstract class GlowingModItem : ModItem
    {
		public Texture2D GlowMask;

		public override void PostDrawInInventory(SpriteBatch spriteBatch, Vector2 position, Rectangle frame, Color drawColor, Color itemColor, Vector2 origin, float scale)
		{
			Texture2D glowTexture = GlowMask;
			Color color = Color.White;
			color.A = 255;
			spriteBatch.Draw(glowTexture, position, frame, color, 0f, origin, scale, SpriteEffects.None, 0f);
		}

		public override void PostDrawInWorld(SpriteBatch spriteBatch, Color lightColor, Color alphaColor, float rotation, float scale, int whoAmI)
		{
			Texture2D glowTexture = GlowMask;
			Color color = Color.White;
			color.A = 255;
			Rectangle glowTextureSize = new(0, 0, glowTexture.Width, glowTexture.Height);
			Vector2 glowTextureOrigin = glowTexture.Size() * .5f;
			Vector2 glowOffset = new((float)(Item.width / 2) - glowTextureOrigin.X, (float)(Item.height - glowTexture.Height));
			Vector2 glowPosition = Item.position - Main.screenPosition + glowTextureOrigin + glowOffset;
			spriteBatch.Draw(glowTexture, glowPosition, glowTextureSize, color, rotation, glowTextureOrigin, scale, SpriteEffects.None, 0f);
		}
	}
}
