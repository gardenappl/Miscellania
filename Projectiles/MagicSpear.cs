
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class MagicSpear : ModProjectile
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().SpearofJustice;
		}

		public override void SetDefaults()
		{
			Projectile.scale = 1.3f;
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.aiStyle = 1;
			Projectile.friendly = true;
			Projectile.penetrate = 2;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.ignoreWater = true;
			AIType = ProjectileID.JavelinFriendly;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			int type = Main.rand.Next(2) == 0 ? ModContent.ProjectileType<MagicSpearMini>() : ModContent.ProjectileType<MagicSpearMiniAlt>();

			switch (Main.rand.Next(4))
			{
				case 0: //Shoot right
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Left.X - 80, target.Left.Y, 4f, 0f, type, Projectile.damage / 3, 0.5f, Projectile.owner, 0, 1);
					return;
				case 1: //Shoot down
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Top.X, target.Top.Y - 80, 0f, 4f, type, Projectile.damage / 3, 0.5f, Projectile.owner, 0, 1);
					return;
				case 2: //Shoot left
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Right.X + 80, target.Right.Y, -4f, 0f, type, Projectile.damage / 3, 0.5f, Projectile.owner, 0, 1);
					return;
				case 3: //Shoot up
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Bottom.X, target.Bottom.Y + 80, 0f, -4f, type, Projectile.damage / 3, 0.5f, Projectile.owner, 0, 1);
					return;
			}
		}
		
		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item, Projectile.position, 10);
			for (int i = 0; i < 4; i++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch, Projectile.velocity.X, Projectile.velocity.Y);
				Main.dust[dust].scale = 1.4f;
			}
		}
	}
}
