
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ModLoader;
using GoldensMisc.Projectiles;

namespace GoldensMisc
{
	public class MiscPlayer : ModPlayer
	{
		public bool ExplosionResistant;
		public bool DemonCrown;
		public bool Magnet;
		public bool OrbofLight;
		
		public override void ResetEffects()
		{
			ExplosionResistant = false;
			DemonCrown = false;
			Magnet = false;
			OrbofLight = false;
		}
		
		bool shake;
		Vector2 prevOffset;
		Vector2 targetOffset;
		int tick;
		int nextShake = 5;
		float intensity = 5f;
		
		public override void ModifyScreenPosition()
		{
			float offsetX = MathHelper.Lerp(prevOffset.X, targetOffset.X, 0.2f);
			float offsetY = MathHelper.Lerp(prevOffset.Y, targetOffset.Y, 0.2f);
			var offset = new Vector2(offsetX, offsetY);
			prevOffset = offset;
			
			Main.screenPosition += offset;
			
			if(shake)
			{
				tick++;
				if(tick >= nextShake)
				{
					tick = 0;
					nextShake = Main.rand.Next(3, 8);
					intensity += 0.1f;
					targetOffset = Main.rand.NextVector2Unit() * Main.rand.Next((int)intensity, (int)intensity * 2);
				}
			}
			else
				targetOffset = new Vector2();
		}
		
		public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
		{
			if(ExplosionResistant)
			{
				int projType = damageSource.SourceProjectileType;
				var proj = new Projectile();
				proj.SetDefaults(projType);
				if(proj.aiStyle == 16 && damageSource.SourcePlayerIndex == player.whoAmI)
					return false;
			}
			return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
		}
		
		public override void PostUpdateEquips()
		{
			if(DemonCrown && player.ownedProjectileCounts[mod.ProjectileType<RedCrystal>()] == 0)
				Projectile.NewProjectile(player.Center, Vector2.Zero, mod.ProjectileType<RedCrystal>(), 60, 8, player.whoAmI);
		}
	}
}
