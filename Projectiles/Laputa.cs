using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class Laputa : ModProjectile
	{
		float startPosition
		{
			get { return Projectile.ai[0]; }
			set { Projectile.ai[0] = value; }
		}
		
		float endPosition
		{
			get { return Projectile.ai[1]; }
			set { Projectile.ai[1] = value; }
		}
		
		public override void SetDefaults()
		{
			Projectile.width = 52;
			Projectile.height = 14;
			Projectile.friendly = true;
			Projectile.scale = 2f;
//			Projectile.aiStyle = 1;
			Projectile.tileCollide = false;
			Projectile.ignoreWater = true;
		}
		
		public override void AI()
		{
			if(!Main.dedServ)
			{
				Main.NewText("lel " + startPosition + " " + endPosition);
				float intensity = (Projectile.position.Y - startPosition) / (endPosition - startPosition);
				Main.NewText("lol " + intensity);
				((LaputaSky)SkyManager.Instance["GoldensMisc:Laputa"]).Intensity = intensity + 0.2f;
			}
			if(Projectile.position.Y < endPosition)
			{
				Main.NewText("KILL");
				if(!Main.dedServ)
					SkyManager.Instance.Deactivate("GoldensMisc:Laputa");
				Projectile.Kill();
			}
		}
		
//		public override void DrawBehind(int index, List<int> drawCacheProjsBehindNPCsAndTiles, List<int> drawCacheProjsBehindNPCs,
//		                                List<int> drawCacheProjsBehindProjectiles, List<int> drawCacheProjsOverWiresUI)
//		{
//			drawCacheProjsBehindNPCsAndTiles.Add(index);
//		}
	}
}
