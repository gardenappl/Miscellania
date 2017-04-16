
using System;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public abstract class Minion : ModProjectile
	{
		public override void AI()
		{
			CheckActive();
			Behaviour();
			Animate();
		}
		
		public abstract void CheckActive();
		
		public abstract void Behaviour();
		
		public virtual void Animate() {}
	}
}
