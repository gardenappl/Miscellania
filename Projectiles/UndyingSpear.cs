
using Terraria;
using Terraria.ID;
using Terraria.Audio;
using Terraria.ModLoader;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace GoldensMisc.Projectiles
{
	public class UndyingSpear : ModProjectile
	{
		public override bool IsLoadingEnabled(Mod mod)
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
			Projectile.penetrate = 3;
			Projectile.DamageType = DamageClass.Magic;
			Projectile.ignoreWater = true;
			AIType = ProjectileID.JavelinFriendly;
		}
		
		public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
		{
			int type = Main.rand.NextBool(2) ? ModContent.ProjectileType<MagicSpearMini>() : ModContent.ProjectileType<MagicSpearMiniAlt>();
			
			switch(Main.rand.Next(4))
			{
				case 0: //Shoot right
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Left.X - 100, target.Left.Y, 5f, 0f, type, Projectile.damage / 2, 1f, Projectile.owner, 1, 1);
					return;
				case 1: //Shoot down
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Top.X, target.Top.Y - 100, 0f, 5f, type, Projectile.damage / 2, 1f, Projectile.owner, 1, 1);
					return;
				case 2: //Shoot left
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Right.X + 100, target.Right.Y, -5f, 0f, type, Projectile.damage / 2, 1f, Projectile.owner, 1, 1);
					return;
				case 3: //Shoot up
					Projectile.NewProjectile(Projectile.GetSource_FromThis(), target.Bottom.X, target.Bottom.Y + 100, 0f, -5f, type, Projectile.damage / 2, 1f, Projectile.owner, 1, 1);
					return;
			}
		}
		
		public override void Kill(int timeLeft)
		{
			SoundEngine.PlaySound(SoundID.Item10, Projectile.position);
			for (int i = 0; i < 5; i++)
			{
				int dust = Dust.NewDust(Projectile.position, Projectile.width, Projectile.height, DustID.BlueTorch, Projectile.velocity.X, Projectile.velocity.Y);
				Main.dust[dust].scale = 1.5f;
			}
		}

        public override void PostDraw(Color lightColor)
        {
			Texture2D glowTexture = MiscGlowMasks.UndyingSpearProjectile;
			lightColor = Color.White;
			lightColor.A = 255;
			Vector2 glowPosition = Projectile.position;
			Rectangle glowTextureSize = new(0, 0, glowTexture.Width, glowTexture.Height);
			Vector2 glowTextureOrigin = glowTexture.Size() * .5f;
			Main.EntitySpriteDraw(glowTexture, glowPosition, glowTextureSize, lightColor, Projectile.rotation, glowTextureOrigin, Projectile.scale, SpriteEffects.None, 0);
        }
    }
}
