
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class Laputa : ModProjectile
	{
		float startPosition
		{
			get { return projectile.ai[0]; }
			set { projectile.ai[0] = value; }
		}
		
		float endPosition
		{
			get { return projectile.ai[1]; }
			set { projectile.ai[1] = value; }
		}
		
		public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Laputa Light");

			DisplayName.AddTranslation(GameCulture.Russian, "Свет Лапуты");
			DisplayName.AddTranslation(GameCulture.Chinese, "空岛之光");
		}
		
		public override void SetDefaults()
		{
			projectile.width = 52;
			projectile.height = 14;
			projectile.friendly = true;
			projectile.scale = 2f;
//			projectile.aiStyle = 1;
			projectile.tileCollide = false;
			projectile.ignoreWater = true;
		}
		
		public override void AI()
		{
			if(!Main.dedServ)
			{
				Main.NewText("lel " + startPosition + " " + endPosition);
				float intensity = (projectile.position.Y - startPosition) / (endPosition - startPosition);
				Main.NewText("lol " + intensity);
				((LaputaSky)SkyManager.Instance["GoldensMisc:Laputa"]).Intensity = intensity + 0.2f;
			}
			if(projectile.position.Y < endPosition)
			{
				Main.NewText("KILL");
				if(!Main.dedServ)
					SkyManager.Instance.Deactivate("GoldensMisc:Laputa");
				projectile.Kill();
			}
		}
		
//		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs,
//		                                List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
//		{
//			drawCacheProjsBehindNPCsAndTiles.Add(index);
//		}
	}
}
