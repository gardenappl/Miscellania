﻿
using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;

namespace GoldensMisc.Tiles
{
	public class RedChimneyTE : ModTileEntity
	{
		public override bool IsLoadingEnabled(Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().RedBrickFurniture;
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
			if(CurrentState == State.Smoke && Main.rand.NextBool(9))
			{
				Vector2 smokePos = new (Position.X * 16 + 16, Position.Y * 16 + 8);
				Vector2 smokeVel = Vector2.Zero;
				int smokeType = Main.rand.Next(GoreID.ChimneySmoke1, GoreID.ChimneySmoke3 + 1);
				int rand = Main.rand.Next(4);
				if (rand == 3)
				{
					Gore.NewGore(new EntitySource_TileEntity(this), smokePos, smokeVel, smokeType, Main.rand.NextFloat() * 0.4f + 0.4f);
				}
				else if (rand == 2)
				{
					Gore.NewGore(new EntitySource_TileEntity(this), smokePos, smokeVel, smokeType, Main.rand.NextFloat() * 0.3f + 0.3f);
				}
				else
				{
					Gore.NewGore(new EntitySource_TileEntity(this), smokePos, smokeVel, smokeType, Main.rand.NextFloat() * 0.2f + 0.2f);
				}
			}
		}

		public override bool IsTileValidForEntity(int i, int j)
		{
			var tile = Main.tile[i, j];
			return tile.HasTile && tile.TileType == ModContent.TileType<RedChimney>() && tile.TileFrameX == 0 && tile.TileFrameY == 0;
		}

		public override int Hook_AfterPlacement(int i, int j, int type, int style, int direction, int alternate)
		{
			if (Main.netMode == NetmodeID.MultiplayerClient)
			{
				NetMessage.SendTileSquare(Main.myPlayer, i, j - 1, 3);
				NetMessage.SendData(MessageID.TileEntityPlacement, -1, -1, null, i - 1, j - 2, Type);
				return -1;
			}
			return Place(i - 1, j - 2);
		}
		
		public override void SaveData(TagCompound tag)
		{
			tag["s"] = (byte)CurrentState;
		}
		
		public override void LoadData(TagCompound tag)
		{
			if (tag.ContainsKey("s"))
				CurrentState = (State)tag.GetByte("s");
		}
		
		public override void NetSend(BinaryWriter writer)
		{
			writer.Write((byte)CurrentState);
		}
		
		public override void NetReceive(BinaryReader reader)
		{
			CurrentState = (State)reader.ReadByte();
		}
	}
}
