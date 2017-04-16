
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class LaputaSky : CustomSky
	{
		bool isActive;
		public float Intensity;

		public override void Update(GameTime gameTime)
		{
			if(Intensity < 0f)
			{
				Intensity = 0f;
			}
			if (!isActive && Intensity > 0f)
			{
				Intensity -= 0.01f;
			}
		}

		public override void Draw(SpriteBatch spriteBatch, float minDepth, float maxDepth)
		{
			if (maxDepth >= 0 && minDepth < 0)
			{
				spriteBatch.Draw(Main.blackTileTexture, new Rectangle(0, 0, Main.screenWidth, Main.screenHeight), (isActive ? Color.White : Color.LightPink) * Intensity);
			}
		}

		public override float GetCloudAlpha()
		{
			return 0f;
		}

		public override void Activate(Vector2 position, params object[] args)
		{
			isActive = true;
		}

		public override void Deactivate(params object[] args)
		{
			isActive = false;
		}

		public override void Reset()
		{
			isActive = false;
		}

		public override bool IsActive()
		{
			return isActive || Intensity > 0f;
		}
	}
}
