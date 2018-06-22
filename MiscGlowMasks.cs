
using System;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
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
			Array.Resize(ref Main.glowMaskTexture, Main.glowMaskTexture.Length + Count);
			
			short i = (short)(Main.glowMaskTexture.Length - Count);
			Main.glowMaskTexture[i] = ModLoader.GetTexture("GoldensMisc/Items/Weapons/UndyingSpear_Glow");
			UndyingSpear = i;
			i++;
			Main.glowMaskTexture[i] = ModLoader.GetTexture("GoldensMisc/Projectiles/UndyingSpear_Glow");
			UndyingSpearProjectile = i;
			i++;
			Main.glowMaskTexture[i] = ModLoader.GetTexture("GoldensMisc/Items/Tools/RodofWarping_Glow");
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
				Main.glowMaskTexture[UndyingSpear] = null;
				Main.glowMaskTexture[UndyingSpearProjectile] = null;
				Main.glowMaskTexture[RodofWarping] = null;

				//Shrink array back to original if possible
				if(Main.glowMaskTexture.Length == End)
				{
					Array.Resize(ref Main.glowMaskTexture, Main.glowMaskTexture.Length - Count);
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
