
using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace GoldensMisc.Tiles
{
	public class RedChimneyTE : ModTileEntity
	{
		public override bool Autoload(ref string name)
		{
			return Config.RedBrickFurniture;
		}
		
		public enum State : byte
		{
			Deactivated,
			Active,
			Smoke
		}
		
		public State CurrentState = State.Active;
		
		public override void Update()
		{
			if(CurrentState == State.Smoke && Main.rand.Next(9) == 0)
			{
				var smokePos = new Vector2(Position.X * 16 + 16, Position.Y * 16 + 8);
				var smokeVel = Vector2.Zero;
				if (Main.windSpeed < 0f)
				{
					smokeVel.X = -Main.windSpeed;
				}
				int smokeType = Main.rand.Next(825, 828);
				if (Main.rand.Next(4) == 0)
				{
					Gore.NewGore(smokePos, smokeVel, smokeType, Main.rand.NextFloat() * 0.4f + 0.4f);
				}
				else if (Main.rand.Next(2) == 0)
				{
					Gore.NewGore(smokePos, smokeVel, smokeType, Main.rand.NextFloat() * 0.3f + 0.3f);
				}
				else
				{
					Gore.NewGore(smokePos, smokeVel, smokeType, Main.rand.NextFloat() * 0.2f + 0.2f);
				}
			}
		}

		public override bool ValidTile(int i, int j)
		{
			var tile = Main.tile[i, j];
			return tile.active() && tile.type == mod.TileType<RedChimney>() && tile.frameX == 0 && tile.frameY == 0;
		}

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendTileSquare(Main.myPlayer, i, j - 1, 3);
				NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i - 1, j - 2, Type);
				return -1;
			}
			return Place(i - 1, j - 2);
		}
		
		public override TagCompound Save()
		{
			return new TagCompound
			{
				{"s", (byte)CurrentState}
			};
		}
		
		public override void Load(TagCompound tag)
		{
			CurrentState = (State)tag.GetByte("s");
		}
		
		public override void NetSend(BinaryWriter writer, bool lightSend)
		{
			writer.Write((byte)CurrentState);
		}
		
		public override void NetReceive(BinaryReader reader, bool lightReceive)
		{
			CurrentState = (State)reader.ReadByte();
		}
	}
}
