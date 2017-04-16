
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;

namespace GoldensMisc
{
	public static class MiscUtils
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
			return (beamStart - collisionTile.ToWorldCoordinates()).Length();
		}
		
		public static Point ToTileCoordinates(this Tuple<int, int> tuple)
		{
			return new Point(tuple.Item1, tuple.Item2);
		}

		public static Point16 ToTileCoordinates16(this Tuple<int, int> tuple)
		{
			return new Point16(tuple.Item1, tuple.Item2);
		}

		public static Vector2 ToWorldCoordinates(this Tuple<int, int> tuple, float autoAddX = 8f, float autoAddY = 8f)
		{
			return new Vector2(tuple.Item1 * 16 + autoAddX, tuple.Item2 * 16 + autoAddY);
		}
	}
}
