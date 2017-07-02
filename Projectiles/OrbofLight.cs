
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
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Ancient Orb of Light");
			DisplayName.AddTranslation(GameCulture.Russian, "Древняя сфера света");
			Main.projFrames[projectile.type] = 1;
			Main.projPet[projectile.type] = true;
			ProjectileID.Sets.TrailingMode[projectile.type] = 2;
			ProjectileID.Sets.LightPet[projectile.type] = true;
		}

		public override void SetDefaults()
		{
			projectile.width = 32;
			projectile.height = 32;
			projectile.penetrate = -1;
			projectile.netImportant = true;
			projectile.timeLeft *= 5;
			projectile.friendly = true;
			projectile.ignoreWater = true;
			projectile.tileCollide = false;
			projectile.aiStyle = 11;
			aiType = ProjectileID.ShadowOrb;
		}
		
		public override Color? GetAlpha(Color lightColor)
		{
			return Color.White * 0.7f;
		}
		
		public override void AI()
		{
			var player = Main.player[projectile.owner];
			var modPlayer = player.GetModPlayer<MiscPlayer>(mod);
			if (!player.active)
			{
				projectile.active = false;
				return;
			}
			if (player.dead)
			{
				modPlayer.OrbofLight = false;
			}
			if (modPlayer.OrbofLight)
			{
				projectile.timeLeft = 2;
			}
			else
			{
				projectile.timeLeft = 0;
			}
			Lighting.AddLight(projectile.Center, 1f, 0.9f, 0.5f);
		}
	}
}
