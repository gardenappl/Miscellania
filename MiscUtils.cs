
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

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
	}
}
