
using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class OrbofLight : ModProjectile
	{
		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().AncientOrb;
		}

		public override void SetStaticDefaults()
		{
			Main.projFrames[Projectile.type] = 1;
			Main.projPet[Projectile.type] = true;
			ProjectileID.Sets.TrailingMode[Projectile.type] = 2;
			ProjectileID.Sets.LightPet[Projectile.type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 32;
			Projectile.height = 32;
			Projectile.penetrate = -1;
			Projectile.netImportant = true;
			Projectile.timeLeft *= 5;
			Projectile.friendly = true;
			Projectile.ignoreWater = true;
			Projectile.tileCollide = false;
			Projectile.aiStyle = 11;
			AIType = ProjectileID.ShadowOrb;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White * 0.5f;
		}
		
		public override void AI()
		{
			var player = Main.player[Projectile.owner];
			var modPlayer = player.GetModPlayer<MiscPlayer>();
			if (!player.active)
			{
				Projectile.active = false;
				return;
			}
			if (player.dead)
			{
				modPlayer.OrbofLight = false;
			}
			if (modPlayer.OrbofLight)
			{
				Projectile.timeLeft = 2;
			}
			else
			{
				Projectile.timeLeft = 0;
			}
			Lighting.AddLight(Projectile.Center, 1f, 0.9f, 0.5f);
		}
	}
}
