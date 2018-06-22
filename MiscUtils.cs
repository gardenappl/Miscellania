
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.GameContent.UI.Elements;
using Terraria.ID;
using Terraria.UI;

namespace GoldensMisc
{
	public class MiscUtils
	{
		//Thank you Iriazul for this!
		public static bool DoesBeamCollide(Rectangle targetHitbox, Vector2 beamStart, float beamAngle, float beamWidth)
		{
			float length = GetBeamLength(beamStart, beamAngle);
			var endPoint = beamStart + beamAngle.ToRotationVector2() * length;
			float temp = 0f;
			return Collision.CheckAABBvLineCollision(targetHitbox.TopLeft(), targetHitbox.Size(), beamStart, endPoint, beamWidth, ref temp);
		}

		public static float GetBeamLength(Vector2 beamStart, float beamAngle, float maxLength = 10000f)
		{
			var startTile = beamStart.ToTileCoordinates();
			var endTile = (beamStart + beamAngle.ToRotationVector2() * maxLength).ToTileCoordinates();
			Tuple<int, int> collisionTile;
			if(!Collision.TupleHitLine(startTile.X, startTile.Y, endTile.X, endTile.Y,
									   0, 0, new List<Tuple<int, int>>(), out collisionTile))
			{
				return maxLength;
			}
			var beamEnd = new Vector2(collisionTile.Item1 * 16 + 8, collisionTile.Item2 * 16 + 8);
			return (beamStart - beamEnd).Length();
		}

		public static class UI
		{
			public static readonly Color defaultUIBlue = new Color(73, 94, 171);
			public static readonly Color defaultUIBlueMouseOver = new Color(63, 82, 151) * 0.7f;

			public static void FadedMouseOver(UIMouseEvent evt, UIElement listeningElement)
			{
				Main.PlaySound(SoundID.MenuTick);
				((UIPanel)evt.Target).BackgroundColor = defaultUIBlue;
			}

			public static void FadedMouseOut(UIMouseEvent evt, UIElement listeningElement)
			{
				((UIPanel)evt.Target).BackgroundColor = defaultUIBlueMouseOver;
			}

			public static void CustomFadedMouseOver(Color customColor, UIMouseEvent evt, UIElement listeningElement)
			{
				Main.PlaySound(SoundID.MenuTick);
				((UIPanel)evt.Target).BackgroundColor = customColor;
			}

			public static void CustomFadedMouseOut(Color customColor, UIMouseEvent evt, UIElement listeningElement)
			{
				((UIPanel)evt.Target).BackgroundColor = customColor;
			}
		}
	}
}
