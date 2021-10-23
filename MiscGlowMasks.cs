
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace GoldensMisc
{
	public static class MiscGlowMasks
	{
		public static short UndyingSpear;
		public static short UndyingSpearProjectile;
		public static short RodofWarping;
		const short Count = 3;
		static short End;
		public static bool Loaded;
		
		public static void Load()
		{
			Array.Resize(ref TextureAssets.GlowMask, TextureAssets.GlowMask.Length + Count);
			
			short i = (short)(TextureAssets.GlowMask.Length - Count);
			TextureAssets.GlowMask[i] = ModContent.Request<Texture2D>("GoldensMisc/Items/Weapons/UndyingSpear_Glow");
			UndyingSpear = i;
			i++;
			TextureAssets.GlowMask[i] = ModContent.Request<Texture2D>("GoldensMisc/Projectiles/UndyingSpear_Glow");
			UndyingSpearProjectile = i;
			i++;
			TextureAssets.GlowMask[i] = ModContent.Request<Texture2D>("GoldensMisc/Items/Tools/RodofWarping_Glow");
			RodofWarping = i;
			i++;
			End = i;
			
			Loaded = true;
		}
		
		public static void Unload()
		{
			if(Loaded)
			{
				//Remove our glow masks so the mod can unload properly(?)
				TextureAssets.GlowMask[UndyingSpear] = null;
				TextureAssets.GlowMask[UndyingSpearProjectile] = null;
				TextureAssets.GlowMask[RodofWarping] = null;

				//Shrink array back to original if possible
				if(TextureAssets.GlowMask.Length == End)
				{
					Array.Resize(ref TextureAssets.GlowMask, TextureAssets.GlowMask.Length - Count);
				}

				Loaded = false;
				UndyingSpear = 0;
				UndyingSpearProjectile = 0;
				RodofWarping = 0;
				End = 0;
			}
		}
	}
}
