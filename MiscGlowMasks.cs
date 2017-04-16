
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
		const short MiscGlowMasksCount = 2;
		static short MiscGlowMasksEnd;
		public static bool Loaded;
		
		public static void Load()
		{
			Array.Resize(ref Main.glowMaskTexture, Main.glowMaskTexture.Length + MiscGlowMasksCount);
			
			short i = (short)(Main.glowMaskTexture.Length - MiscGlowMasksCount);
			Main.glowMaskTexture[i] = ModLoader.GetTexture("GoldensMisc/Items/Weapons/UndyingSpear_Glow");
			UndyingSpear = i;
			
			i++;
			Main.glowMaskTexture[i] = ModLoader.GetTexture("GoldensMisc/Projectiles/UndyingSpear_Glow");
			UndyingSpearProjectile = i;
			
			i++;
			MiscGlowMasksEnd = i;
			
			Loaded = true;
		}
		
		public static void Unload()
		{
			Loaded = false;
			UndyingSpear = 0;
			UndyingSpearProjectile = 0;
			MiscGlowMasksEnd = 0;
			//If Miscellania was the last mod to add glow masks
			if(Main.glowMaskTexture.Length == MiscGlowMasksEnd)
			{
				//Remove our glow masks
				Array.Resize(ref Main.glowMaskTexture, Main.glowMaskTexture.Length - MiscGlowMasksCount);
			}
			//Otherwise just pray that everything will be fine
		}
	}
}
