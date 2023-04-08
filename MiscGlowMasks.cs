
using System;
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
}
