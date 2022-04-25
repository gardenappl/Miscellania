
using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using GoldensMisc.Dusts;

namespace GoldensMisc.Projectiles
{
	public class RedCrystal : Minion
	{
		float Rotation
		{
			get { return Projectile.ai[0]; }
			set { Projectile.ai[0] = value; }
		}
		int ShootDelay
		{
			get { return (int)Projectile.ai[1]; }
			set { Projectile.ai[1] = value; }
		}
		int DustDelay
		{
			get { return (int)Projectile.localAI[0]; }
			set { Projectile.localAI[0] = value; }
		}
		
		const float TargetDist = 450f;

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().DemonCrown;
		}

		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 2;
		}
		
		public override void SetDefaults()
		{
//			Projectile.scale = 0.9f;
			Projectile.width = 16;
			Projectile.height = 25;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.ignoreWater = true;
			Projectile.timeLeft = 5;
			Projectile.tileCollide = false;
			Projectile.DamageType = DamageClass.Magic;
		}
		
		public override void CheckActive()
		{
			var player = Main.player[Projectile.owner];
			var modPlayer = player.GetModPlayer<MiscPlayer>();
			if(modPlayer.DemonCrown)
			{
				Projectile.timeLeft = 2;
			}
		}
		
		public override void Behaviour()
		{
			var player = Main.player[Projectile.owner];
			Rotation = MathHelper.WrapAngle(Rotation + 0.025f);
			float distance = (float)Math.Sin(Main.time / 18f + (double)Projectile.whoAmI / 50) * 64;
			Projectile.Center = player.MountedCenter + new Vector2(0, distance).RotatedBy(Rotation);
			
			Lighting.AddLight(Projectile.Center, Color.Red.ToVector3() * 0.5f);
			
			ShootDelay--;
			
			if(ShootDelay <= 0)
			{
				int target = -1;
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					var npc = Main.npc[k];
					if (npc.active && npc.CanBeChasedBy(this))
					{
						float distanceToNPC = Vector2.Distance(npc.Center, Projectile.Center);
						if (distanceToNPC < TargetDist && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
						{
							target = k;
						}
					}
				}
				
				if(target != -1)
				{
					var shootVel = Main.npc[target].Center - Projectile.Center;
					shootVel.Normalize();
					shootVel *= 6f;
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, shootVel, ModContent.ProjectileType<RedBullet>(), Projectile.damage, Projectile.knockBack * 0.5f, Projectile.owner);
					ShootDelay = 50;
				}
			}
			
			DustDelay--;
			if(DustDelay <= 0)
			{
				Dust.NewDustPerfect(Projectile.Center, ModContent.DustType<RedDustStatic>(), Vector2.Zero, Alpha: 80, Scale: 1.3f);
				DustDelay = 20;
			}
		}
		
		public override void Animate()
		{
			Projectile.frameCounter++;
			if(Projectile.frameCounter >= 40)
			{
				Projectile.frame++;
				if(Projectile.frame > 1)
				{
					Projectile.frame = 0;
				}
			}
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White * 0.85f;
		}
		
		public override bool? CanCutTiles()
		{
			return false;
		}
	}
}
