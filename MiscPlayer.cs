
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace GoldensMisc
{
	public class MiscPlayer : ModPlayer
	{
		public bool ExplosionResistant;
		
		public override void ResetEffects()
		{
			ExplosionResistant = false;
		}
		
		bool shake;
		Vector2 prevOffset;
		Vector2 targetOffset;
		int tick;
		int nextShake = 5;
		float intensity = 5;
		
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
		
//		public override bool CanHitPvpWithProj(Projectile proj, Player target)
//		{
//			Main.NewText("expl resist: " + ExplosionResistant);
//			Main.NewText("ai: " + proj.aiStyle);
//			Main.NewText("proj owner: " + proj.owner);
//			Main.NewText("who am i: " + player.whoAmI);
//			return false;
//		}
//		
//		public override void OnHitPvpWithProj(Projectile proj, Player target, int damage, bool crit)
//		{
//			Main.NewText("expl resist: " + ExplosionResistant);
//			Main.NewText("ai: " + proj.aiStyle);
//			Main.NewText("proj owner: " + proj.owner);
//			Main.NewText("who am i: " + player.whoAmI);
//		}
		
//		public override bool CanBeHitByProjectile(Projectile proj)
//		{
//			Main.NewText("expl resist: " + ExplosionResistant);
//			Main.NewText("ai: " + proj.aiStyle);
//			Main.NewText("proj owner: " + proj.owner);
//			Main.NewText("who am i: " + player.whoAmI);
//			return !(ExplosionResistant && proj.aiStyle == 16 && proj.owner == player.whoAmI);
//		}
	}
}
