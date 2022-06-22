
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Enums;
using Terraria.ID;
using Terraria.GameContent;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class GasterLaser : ModProjectile
	{
		const float BeamWidth = 18f;
		float BeamLength
		{
			get { return Projectile.ai[0]; }
			set { Projectile.ai[0] = value; }
		}
		Vector2 endPoint;

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().GasterBlaster;
		}

		public override void SetDefaults()
		{
			Projectile.netImportant = true;
			Projectile.width = 1;
			Projectile.height = 1;
			Projectile.friendly = true;
			Projectile.penetrate = -1;
			Projectile.timeLeft = 2;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
			Projectile.DamageType = DamageClass.Summon;
			Projectile.scale = 1.5f;
			ProjectileID.Sets.SentryShot[Projectile.type] = true;
		}
		
		public override bool? Colliding(Rectangle projHitbox, Rectangle targetHitbox)
		{
			return MiscUtils.DoesBeamCollide(targetHitbox, projHitbox.Center(), Projectile.rotation, BeamWidth);
		}
		
		public override void AI()
		{
			BeamLength = MiscUtils.GetBeamLength(Projectile.Center, Projectile.rotation);
			endPoint = Projectile.Center + Projectile.rotation.ToRotationVector2() * BeamLength;
            var unit = endPoint - Projectile.Center;
            unit.Normalize();

            int dustAmount = Main.rand.Next(5, 10);
			for(int i = 0; i < dustAmount; i++)
            {
                int dust = Dust.NewDust(endPoint - unit * 10f - new Vector2(25, 25), 50, 50, DustID.Smoke, Main.rand.NextFloat(), Main.rand.NextFloat());
				Main.dust[dust].noGravity = true;
			}
			DelegateMethods.v3_1 = new Vector3(0.8f, 0.8f, 1f);
			Utils.PlotTileLine(Projectile.Center, endPoint, BeamWidth, new Utils.TileActionAttempt(DelegateMethods.CastLight));
		}
		
		public override void CutTiles()
		{
			DelegateMethods.tilecut_0 = TileCuttingContext.AttackProjectile;
			var unit = Projectile.velocity;
			Utils.PlotTileLine(Projectile.Center, endPoint, BeamWidth, new Utils.TileActionAttempt(DelegateMethods.CutTiles));
		}

		public override bool PreDraw(ref Color lightColor)
		{
			var unit = endPoint - Projectile.Center;
			unit.Normalize();
//			DrawLaser(spriteBatch, Main.projectileTexture[Projectile.type], startPoint, unit, 5, -1.57f, Projectile.scale);
//			return false;
//		}
//
//		/// <summary>
//		/// The core function of drawing a laser
//		/// </summary>
//		public void DrawLaser(SpriteBatch spriteBatch, Texture2D texture, Vector2 start, Vector2 unit, float step, float rotation = 0f, float scale = 1f, Color color = default(Color))
//		{
			var position = Projectile.Center;
			float r = unit.ToRotation() - MathHelper.PiOver2;
			const int transDist = 50;
			const int step = 14;
			var texture = TextureAssets.Projectile[Projectile.type].Value;

			#region Draw laser body
			for (float i = transDist; i < BeamLength - 20; i += step)
			{
				position = Projectile.Center + i * unit;
				Main.EntitySpriteDraw(texture, position - Main.screenPosition,
				                 new Rectangle(0, 24, 38, 30), i < transDist ? Color.Transparent : Color.White, r,
				                 new Vector2(38 / 2, 30 / 2), Projectile.scale, SpriteEffects.None, 0);
			}
			#endregion

			#region Draw laser tail
			Main.EntitySpriteDraw(texture, Projectile.Center + unit * (transDist - step) - Main.screenPosition,
			                 new Rectangle(0, 0, 38, 22), Color.White, r, new Vector2(38 / 2, 22 / 2), Projectile.scale, SpriteEffects.None, 0);
			#endregion

			#region Draw laser head
			Main.EntitySpriteDraw(texture, Projectile.Center + unit * BeamLength - Main.screenPosition,
			                 new Rectangle(0, 56, 38, 22), Color.White, r, new Vector2(38 / 2, 22), Projectile.scale, SpriteEffects.None, 0);
			#endregion
			return false;
		}
		
		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs, List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverPlayers, List<int> drawCacheProjsOverWiresUI)
		{
			drawCacheProjsBehindProjectiles.Add(index);
		}
	}
}
