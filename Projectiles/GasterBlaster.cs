
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.GameContent;
using Terraria.Audio;
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
			get { return (int)Projectile.ai[0]; }
			set { Projectile.ai[0] = value; }
		}
		int ShootCounter
		{
			get { return (int)Projectile.ai[1]; }
			set { Projectile.ai[1] = value; }
		}
		Vector2 TargetPos;
		int SoundCounter
		{
			get { return (int)Projectile.localAI[0]; }
			set { Projectile.localAI[0] = value; }
		}
		bool FirstSpawn = true;

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().GasterBlaster;
		}

		public override void SetStaticDefaults()
        {
            Main.projFrames[Projectile.type] = 2;
            //Main.projPet[Projectile.type] = true;
			//ProjectileID.Sets.Homing[Projectile.type] = true;
			//ProjectileID.Sets.TurretFeature[Projectile.type] = true;
			ProjectileID.Sets.MinionTargettingFeature[Projectile.type] = true;
		}
		
		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 44;
			Projectile.height = 48;
			Projectile.friendly = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.sentry = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = Projectile.SentryLifeTime;
			Projectile.ignoreWater = true;
		}
		
		public override void AI()
		{
            if (FirstSpawn)
            {
                Main.player[Projectile.owner].UpdateMaxTurrets();
                FirstSpawn = false;
            }
            Projectile.velocity *= 0.9f;
			var player = Main.player[Projectile.owner];
			var targetNPCHitbox = new Rectangle();
			float targetDist = TargetDist;
			if(ShootCounter == 0)
			{
				#region Choose target
				if(player.HasMinionAttackTargetNPC)
				{
					var npc = Main.npc[player.MinionAttackTargetNPC];
					if(Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
					{
						targetDist = Vector2.Distance(Projectile.Center, TargetPos);
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
						float distance = Vector2.Distance(npc.Center, Projectile.Center);
						if (distance < targetDist && Collision.CanHitLine(Projectile.position, Projectile.width, Projectile.height, npc.position, npc.width, npc.height))
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
					Projectile.frame = 1;
					
					//Rotate
					var direction = TargetPos - Projectile.Center;
					Projectile.rotation = Projectile.rotation.AngleLerp(direction.ToRotation() + (float)Math.PI * 1.5f, 0.2f);
					#endregion
				}
				else if(ShootCounter == ShootDelay)
				{
					#region Shoot
					//Push the Blaster back
					var angle = (Projectile.rotation + MathHelper.PiOver2).ToRotationVector2();
					angle *= -3f;
					Projectile.velocity = angle;
                    SoundCounter = 50;
				}
				else if(ShootCounter < ShootTired)
				{
					Projectile laser;
					if(BeamProj == -1 || Main.projectile[BeamProj] == null || Main.projectile[BeamProj].type != ModContent.ProjectileType<GasterLaser>())
					{
						int i = Projectile.NewProjectile(Projectile.GetSource_FromThis(), Projectile.Center, Vector2.Zero, ModContent.ProjectileType<GasterLaser>(), Projectile.damage, Projectile.knockBack, Projectile.owner);
						laser = Main.projectile[i];
						BeamProj = i;
					}
					else
					{
						laser = Main.projectile[BeamProj];
					}
					laser.rotation = Projectile.rotation + MathHelper.PiOver2;
					laser.Center = Projectile.Center;
					laser.timeLeft = 2;
					
					SoundCounter++;
					if (SoundCounter > 22)
					{
						SoundCounter = 0;
						SoundEngine.PlaySound(SoundID.Item34, Projectile.position); //flamethrower sound
					}
					#endregion
				}
				else if(ShootCounter == ShootTired)
				{
					SoundCounter = 0;
					#region Tired of shooting
					Projectile.frame = 0;
					BeamProj = -1;
					
					//Push the Blaster forward with same velocity
					var angle = (Projectile.rotation + MathHelper.PiOver2).ToRotationVector2();
					angle *= 3f;
					Projectile.velocity = angle;
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
			SoundEngine.PlaySound(SoundID.Item14, Projectile.Center);
			for(int i = 0; i < 10; i++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.SparksMech);
			}
			for(int i = 0; i < 15; i++)
			{
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Smoke, Scale: 2f);
				Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.Torch, Scale: 2f);
			}
		}
		
		//Had to do this otherwise the Laser has a weird offset :/
		//I guess the way Terraria handles a scaled projectile's Center is really weird
		
		public override bool PreDraw(ref Color lightColor)
		{
			var texture = TextureAssets.Projectile[Projectile.type].Value;
			var source = new Rectangle(0, texture.Height / Main.projFrames[Projectile.type] * Projectile.frame, texture.Width, texture.Height / Main.projFrames[Projectile.type]);
			var origin = new Vector2(texture.Width / 2, (texture.Height / Main.projFrames[Projectile.type]) / 2);
			Main.EntitySpriteDraw(TextureAssets.Projectile[Projectile.type].Value, Projectile.Center - Main.screenPosition, source, lightColor,
			                 Projectile.rotation, origin, Projectile.scale * Scale, 0, 0);
			return false;
		}
	}
}
