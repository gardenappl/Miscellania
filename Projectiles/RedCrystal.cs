
using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using GoldensMisc.Dusts;

namespace GoldensMisc.Projectiles
{
	public class RedCrystal : Minion
	{
		float Rotation
		{
			get { return projectile.ai[0]; }
			set { projectile.ai[0] = value; }
		}
		int ShootDelay
		{
			get { return (int)projectile.ai[1]; }
			set { projectile.ai[1] = value; }
		}
		int DustDelay
		{
			get { return (int)projectile.localAI[0]; }
			set { projectile.localAI[0] = value; }
		}
		
		const float TargetDist = 450f;
		
		public override void SetDefaults()
		{
			projectile.name = "Red Crystal";
//			projectile.scale = 0.9f;
			projectile.width = 16;
			projectile.height = 25;
			Main.projFrames[projectile.type] = 2;
			projectile.friendly = true;
			projectile.penetrate = -1;
			projectile.ignoreWater = true;
			projectile.timeLeft = 5;
			projectile.tileCollide = false;
			projectile.magic = true;
		}
		
		public override void CheckActive()
		{
			var player = Main.player[projectile.owner];
			var modPlayer = player.GetModPlayer<MiscPlayer>(mod);
			if(modPlayer.DemonCrown)
			{
				projectile.timeLeft = 2;
			}
		}
		
		public override void Behaviour()
		{
			var player = Main.player[projectile.owner];
			Rotation = MathHelper.WrapAngle(Rotation + 0.025f);
			float distance = (float)Math.Sin(Main.time / 18 + (double)projectile.whoAmI / 50) * 64;
			projectile.Center = (player.MountedCenter + new Vector2(0, distance)).RotatedBy(Rotation, player.MountedCenter);
			
			Lighting.AddLight(projectile.Center, Color.Red.ToVector3() * 0.5f);
			
			ShootDelay--;
			
			if(ShootDelay <= 0)
			{
				int target = -1;
				for (int k = 0; k < Main.maxNPCs; k++)
				{
					var npc = Main.npc[k];
					if (npc.active && npc.CanBeChasedBy(this))
					{
						float distanceToNPC = Vector2.Distance(npc.Center, projectile.Center);
						if (distanceToNPC < TargetDist && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
						{
							target = k;
						}
					}
				}
				
				if(target != -1)
				{
					var shootVel = Main.npc[target].Center - projectile.Center;
					shootVel.Normalize();
					shootVel *= 6f;
					Projectile.NewProjectile(projectile.Center, shootVel, mod.ProjectileType<RedBullet>(), projectile.damage, projectile.knockBack * 0.5f, projectile.owner);
					ShootDelay = 50;
				}
			}
			
			DustDelay--;
			if(DustDelay <= 0)
			{
				Dust.NewDustPerfect(projectile.Center, mod.DustType<RedDustStatic>(), Vector2.Zero, Alpha: 80, Scale: 1.3f);
				DustDelay = 20;
			}
		}
		
		public override void Animate()
		{
			projectile.frameCounter++;
			if(projectile.frameCounter >= 40)
			{
				projectile.frame++;
				if(projectile.frame > 1)
				{
					projectile.frame = 0;
				}
			}
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White * 0.7f;
		}
		
		public override bool? CanCutTiles()
		{
			return false;
		}
	}
}
