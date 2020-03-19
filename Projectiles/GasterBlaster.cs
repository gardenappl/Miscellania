
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class GasterBlaster : ModProjectile
	{
		const float Scale = 1.25f;
		const float TargetDist = 700f;
		
		const int ShootDelay = 30;
		const int ShootTired = 90;
		const int ShootStop = 150;
		
		const float BeamLength = 1000f;
		const float BeamWidth = 18f;
		
		int BeamProj
		{
			get { return (int)projectile.ai[0]; }
			set { projectile.ai[0] = value; }
		}
		int ShootCounter
		{
			get { return (int)projectile.ai[1]; }
			set { projectile.ai[1] = value; }
		}
		Vector2 TargetPos;
		int SoundCounter
		{
			get { return (int)projectile.localAI[0]; }
			set { projectile.localAI[0] = value; }
		}
		bool FirstSpawn = true;

		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.GasterBlaster;
		}

		public override void SetStaticDefaults()
        {
            Main.projFrames[projectile.type] = 2;
            //Main.projPet[projectile.type] = true;
			//ProjectileID.Sets.Homing[projectile.type] = true;
			//ProjectileID.Sets.TurretFeature[projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[projectile.type] = true;
		}
		
		public override void SetDefaults()
		{
			projectile.netImportant = true;
			projectile.width = 44;
			projectile.height = 48;
			projectile.friendly = true;
//			projectile.scale = 1.5f;
			//projectile.minion = true;
			projectile.sentry = true;
			projectile.penetrate = -1;
			projectile.timeLeft = Projectile.SentryLifeTime;
			projectile.ignoreWater = true;
		}
		
		public override void AI()
		{
            if (FirstSpawn)
            {
                Main.player[projectile.owner].UpdateMaxTurrets();
                FirstSpawn = false;
            }
            projectile.velocity *= 0.9f;
			var player = Main.player[projectile.owner];
			var targetNPCHitbox = new Rectangle();
			float targetDist = TargetDist;
			if(ShootCounter == 0)
			{
				#region Choose target
				if(player.HasMinionAttackTargetNPC)
				{
					var npc = Main.npc[player.MinionAttackTargetNPC];
					if(Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
					{
						targetDist = Vector2.Distance(projectile.Center, TargetPos);
						TargetPos = Main.npc[player.MinionAttackTargetNPC].Center;
						targetNPCHitbox = Main.npc[player.MinionAttackTargetNPC].Hitbox;
						ShootCounter++;
					}
				}
				else for (int k = 0; k < 200; k++)
				{
					var npc = Main.npc[k];
					if (npc.CanBeChasedBy(this))
					{
						float distance = Vector2.Distance(npc.Center, projectile.Center);
						if (distance < targetDist && Collision.CanHitLine(projectile.position, projectile.width, projectile.height, npc.position, npc.width, npc.height))
						{
							targetDist = distance;
							TargetPos = npc.Center;
							targetNPCHitbox = npc.Hitbox;
							ShootCounter++;
						}
					}
				}
				#endregion
			}
			if(ShootCounter > 0)
			{
				ShootCounter++;
				if(ShootCounter < ShootDelay)
				{
					#region Prepare to shoot
					projectile.frame = 1;
					
					//Rotate
					var direction = TargetPos - projectile.Center;
					projectile.rotation = projectile.rotation.AngleLerp(direction.ToRotation() + (float)Math.PI * 1.5f, 0.2f);
					#endregion
				}
				else if(ShootCounter == ShootDelay)
				{
					#region Shoot
					//Push the Blaster back
					var angle = (projectile.rotation + MathHelper.PiOver2).ToRotationVector2();
					angle *= -3f;
					projectile.velocity = angle;
                    SoundCounter = 50;
				}
				else if(ShootCounter < ShootTired)
				{
					Projectile laser;
					if(BeamProj == -1 || Main.projectile[BeamProj] == null || Main.projectile[BeamProj].type != ModContent.ProjectileType<GasterLaser>())
					{
						int i = Projectile.NewProjectile(projectile.Center, Vector2.Zero, ModContent.ProjectileType<GasterLaser>(), projectile.damage, projectile.knockBack, projectile.owner);
						laser = Main.projectile[i];
						BeamProj = i;
					}
					else
					{
						laser = Main.projectile[BeamProj];
					}
					laser.rotation = projectile.rotation + MathHelper.PiOver2;
					laser.Center = projectile.Center;
					laser.timeLeft = 2;
					
					SoundCounter++;
					if (SoundCounter > 22)
					{
						SoundCounter = 0;
						Main.PlaySound(SoundID.Item34, projectile.position); //flamethrower sound
					}
					#endregion
				}
				else if(ShootCounter == ShootTired)
				{
					SoundCounter = 0;
					#region Tired of shooting
					projectile.frame = 0;
					BeamProj = -1;
					
					//Push the Blaster forward with same velocity
					var angle = (projectile.rotation + MathHelper.PiOver2).ToRotationVector2();
					angle *= 3f;
					projectile.velocity = angle;
					#endregion
				}
				else if(ShootCounter > ShootStop)
				{
					ShootCounter = 0;
				}
			}
		}
		
		public override void Kill(int timeLeft)
		{
			Main.PlaySound(SoundID.Item14, projectile.Center);
			for(int i = 0; i < 10; i++)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.SparksMech);
			}
			for(int i = 0; i < 15; i++)
			{
				Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Smoke, Scale: 2f);
				Dust.NewDust(projectile.position, projectile.width, projectile.height, DustID.Fire, Scale: 2f);
			}
		}
		
		//Had to do this otherwise the Laser has a weird offset :/
		//I guess the way Terraria handles a scaled projectile's Center is really weird
		
		public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
		{
			var texture = Main.projectileTexture[projectile.type];
			var source = new Rectangle(0, texture.Height / Main.projFrames[projectile.type] * projectile.frame, texture.Width, texture.Height / Main.projFrames[projectile.type]);
			var origin = new Vector2(texture.Width / 2, (texture.Height / Main.projFrames[projectile.type]) / 2);
			spriteBatch.Draw(Main.projectileTexture[projectile.type], projectile.Center - Main.screenPosition, source, lightColor,
			                 projectile.rotation, origin, projectile.scale * Scale, 0, 0);
			return false;
		}
	}
}
