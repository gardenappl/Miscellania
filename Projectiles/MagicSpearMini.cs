using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class MagicSpearMini : ModProjectile
	{
		int timesSpawned
		{
			get { return (int)Projectile.ai[1]; }
			set { Projectile.ai[1] = value; }
		}
		
		bool undying
		{
			get { return Projectile.ai[0] > 0; }
			set { Projectile.ai[0] = value ? 1 : 0; }
		}

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().SpearofJustice;
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 18;
			Projectile.scale = 1.3f;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.maxPenetrate = -1;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.light = 0.1f;
			Projectile.tileCollide = false;
			Projectile.timeLeft = 60;
			Projectile.ignoreWater = true;
			AIType = ProjectileID.Bullet;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			if(timesSpawned > 2 || timesSpawned == -1)
			{
				return;
			}
			int type = Main.rand.Next(2) == 0 ? ModContent.ProjectileType<MagicSpearMini>() : ModContent.ProjectileType<MagicSpearMiniAlt>();

			if (undying)
			{
				switch(Main.rand.Next(4))
				{
					case 0: //Shoot right
						Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), target.Left.X - 100, target.Left.Y, 5f, 0f, type, Projectile.damage, 1f, Projectile.owner, 1, timesSpawned + 1);
						return;
					case 1: //Shoot down
						Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), target.Top.X, target.Top.Y - 100, 0f, 5f, type, Projectile.damage, 1f, Projectile.owner, 1, timesSpawned + 1);
						return;
					case 2: //Shoot left
						Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), target.Right.X + 100, target.Right.Y, -5f, 0f, type, Projectile.damage, 1f, Projectile.owner, 1, timesSpawned + 1);
						return;
					case 3: //Shoot up
						Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), target.Bottom.X, target.Bottom.Y + 100, 0f, -5f, type, Projectile.damage, 1f, Projectile.owner, 1, timesSpawned + 1);
						return;
				}
			}
			else
			{
				switch(Main.rand.Next(4))
				{
					case 0: //Shoot right
						Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), target.Left.X - 80, target.Left.Y, 4f, 0f, type, Projectile.damage, 0.5f, Projectile.owner, 0, timesSpawned + 1);
						return;
					case 1: //Shoot down
						Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), target.Top.X, target.Top.Y - 80, 0f, 4f, type, Projectile.damage, 0.5f, Projectile.owner, 0, timesSpawned + 1);
						return;
					case 2: //Shoot left
						Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), target.Right.X + 80, target.Right.Y, -4f, 0f, type, Projectile.damage, 0.5f, Projectile.owner, 0, timesSpawned + 1);
						return;
					case 3: //Shoot up
						Projectile.NewProjectile(Projectile.GetProjectileSource_FromThis(), target.Bottom.X, target.Bottom.Y + 80, 0f, -4f, type, Projectile.damage, 0.5f, Projectile.owner, 0, timesSpawned + 1);
						return;
				}
			}
			timesSpawned = -1; //A projectile should only spawn once
		}
	}
}
