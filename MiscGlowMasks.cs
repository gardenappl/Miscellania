
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
		const short Count = 2;
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
			End = i;
			
			Loaded = true;
		}
		
		public static void Unload()
		{
			//Remove our glow masks
			if(Main.glowMaskTexture.Length == End)
			{
				Array.Resize(ref Main.glowMaskTexture, Main.glowMaskTexture.Length - Count);
			}
			else if(Main.glowMaskTexture.Length > End)
			{
				for(int i = End - Count; i < End; i++)
				{
					Main.glowMaskTexture[i] = ModLoader.GetTexture("Terraria/Item_0");
				}
			}
			Loaded = false;
			UndyingSpear = 0;
			UndyingSpearProjectile = 0;
			End = 0;
		}
	}
}
