using GoldensMisc.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class AutofisherBobber : ModProjectile
	{
		static readonly Color FishingLineColor = new Color(250, 90, 70, 100);

		public override string Texture => "Terraria/Projectile_" + ProjectileID.BobberMechanics;

		public override bool Autoload(ref string name)
		{
			return ServerConfig.Instance.Autofisher;
		}

		public override void SetDefaults()
		{
			projectile.width = 14;
			projectile.height = 14;
			projectile.penetrate = -1;
			//projectile.bobber = true;
		}

		//private static int[] Crates = new int[]
		//{
		//	ItemID.WoodenCrate,
		//	ItemID.IronCrate,
		//	ItemID.GoldenCrate,
		//	ItemID.CorruptFishingCrate,
		//	ItemID.CrimsonFishingCrate,
		//	ItemID.DungeonFishingCrate,
		//	ItemID.FloatingIslandFishingCrate,
		//	ItemID.HallowedFishingCrate,
		//	ItemID.JungleFishingCrate
		//};
		
		//ai[0] == 0 - fishing
		//ai[0] > 0 - reeling, ai[0] - fish ItemID
		//ai[1] - tile entity ID
		public override void AI()
		{
			if(!TileEntity.ByID.ContainsKey((int)projectile.ai[1]) || !(TileEntity.ByID[(int)projectile.ai[1]] is AutofisherTE))
			{
				projectile.Kill();
				return;
			}
			var te = (AutofisherTE)TileEntity.ByID[(int)projectile.ai[1]];
			if(te.GetCurrentBait() == null)
			{
				projectile.Kill();
				return;
			}
			//projectile.timeLeft = 60;
			//if(Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].fishingPole == 0 || Main.player[projectile.owner].CCed || Main.player[projectile.owner].noItems)
			//	projectile.Kill();
			//else if(Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].shoot != projectile.type)
			//	projectile.Kill();
			//else if(Main.player[projectile.owner].pulley)
			//	projectile.Kill();
			//else if(Main.player[projectile.owner].dead)
			//	projectile.Kill();

			//if((double)projectile.ai[1] > 0.0 && (double)projectile.localAI[1] >= 0.0)
			//{
			//	projectile.localAI[1] = -1f;
			//	if(!projectile.lavaWet && !projectile.honeyWet)
			//	{
			//		for(int index1 = 0; index1 < 100; ++index1)
			//		{
			//			int index2 = Dust.NewDust(new Vector2((float)(projectile.position.X - 6.0), (float)(projectile.position.Y - 10.0)), projectile.width + 12, 24, Dust.dustWater(), 0.0f, 0.0f, 0);
			//			Main.dust[index2].velocity.Y -= 4.0f;
			//			Main.dust[index2].velocity.X *= 2.5f;
			//			Main.dust[index2].scale = 0.8f;
			//			Main.dust[index2].alpha = 100;
			//			Main.dust[index2].noGravity = true;
			//		}
			//		Main.PlaySound(19, (int)projectile.position.X, (int)projectile.position.Y, 0, 1f, 0.0f);
			//	}
			//}

			if((double)projectile.ai[0] >= 1.0)
			{
				//don't know what this is
				//if((double)projectile.ai[0] == 2.0)
				//{
				//	++projectile.ai[0];
				//	Main.PlaySound(SoundID.Item17, projectile.position);
				//	if(!projectile.lavaWet && !projectile.honeyWet)
				//	{
				//		for(int index1 = 0; index1 < 100; ++index1)
				//		{
				//			int index2 = Dust.NewDust(new Vector2((float)(projectile.position.X - 6.0), (float)(projectile.position.Y - 10.0)), projectile.width + 12, 24, Dust.dustWater(), 0.0f, 0.0f, 0);
				//			Main.dust[index2].velocity.Y -= 4.0f;
				//			Main.dust[index2].velocity.X *= 2.5f;
				//			Main.dust[index2].scale = 0.8f;
				//			Main.dust[index2].alpha = 100;
				//			Main.dust[index2].noGravity = true;
				//		}
				//		Main.PlaySound(19, (int)projectile.position.X, (int)projectile.position.Y, 0, 1f, 0.0f);
				//	}
				//}
				//if((double)projectile.localAI[0] < 100.0)
				//	++projectile.localAI[0];
				projectile.tileCollide = false;
				double num3 = 15.8999996185303;
				int num4 = 10;
				Vector2 vector2 = new Vector2((float)(projectile.position.X + (double)projectile.width * 0.5), (float)(projectile.position.Y + (double)projectile.height * 0.5));
				//float num5 = (float)(Main.player[projectile.owner].position.X + (double)(Main.player[projectile.owner].width / 2) - vector2.X);
				//float num6 = (float)(Main.player[projectile.owner].position.Y + (double)(Main.player[projectile.owner].height / 2) - vector2.Y);
				float num5 = (te.Position.X + 1.5f) * 16f - vector2.X;
				float num6 = (te.Position.Y + 1) * 16f - vector2.Y; 
				float num7 = (float)Math.Sqrt((double)num5 * (double)num5 + (double)num6 * (double)num6);
				if((double)num7 > 3000.0)
					projectile.Kill();
				double num8 = (double)num7;
				float num9 = (float)(num3 / num8);
				float num10 = num5 * num9;
				float num11 = num6 * num9;
				projectile.velocity.X = (float)((projectile.velocity.X * (double)(num4 - 1) + (double)num10) / (double)num4);
				projectile.velocity.Y = (float)((projectile.velocity.Y * (double)(num4 - 1) + (double)num11) / (double)num4);
				//if(Main.myPlayer == projectile.owner)
				if(Main.netMode != NetmodeID.MultiplayerClient)
				{
					Rectangle rectangle1 = new Rectangle((int)projectile.position.X, (int)projectile.position.Y, projectile.width, projectile.height);
					Rectangle rectangle2 = new Rectangle(te.Position.X * 16, te.Position.Y * 16, 48, 32);
					if(((Rectangle)rectangle1).Intersects(rectangle2))
					{
						if((double)projectile.ai[0] > 0.0)
						{
							int Type = (int)projectile.ai[0];
							Item newItem = new Item();
							newItem.SetDefaults(Type, true);
							if(Type == 3196)
							{
								int num1 = te.GetFishingLevel(te.GetCurrentBait());
								int minValue = (num1 / 20 + 3) / 2;
								int num2 = (num1 / 10 + 6) / 2;
								if(Main.rand.Next(50) < num1)
									++num2;
								if(Main.rand.Next(100) < num1)
									++num2;
								if(Main.rand.Next(150) < num1)
									++num2;
								if(Main.rand.Next(200) < num1)
									++num2;
								int num12 = Main.rand.Next(minValue, num2 + 1);
								newItem.stack = num12;
							}
							if(Type == 3197)
							{
								int num1 = te.GetFishingLevel(te.GetCurrentBait());
								int minValue = (num1 / 4 + 15) / 2;
								int num2 = (num1 / 2 + 30) / 2;
								if(Main.rand.Next(50) < num1)
									num2 += 4;
								if(Main.rand.Next(100) < num1)
									num2 += 4;
								if(Main.rand.Next(150) < num1)
									num2 += 4;
								if(Main.rand.Next(200) < num1)
									num2 += 4;
								int num12 = Main.rand.Next(minValue, num2 + 1);
								newItem.stack = num12;
							}
							ItemLoader.CaughtFishStack(newItem);
							Item.NewItem(te.Position.ToWorldCoordinates(0f, 0f), 48, 32, newItem.type, newItem.stack);
						}
						projectile.Kill();
					}
				}
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			}
			else
			{
				//bool flag = false;
				//Vector2 vector2 = new Vector2((float)(projectile.position.X + (double)projectile.width * 0.5), (float)(projectile.position.Y + (double)projectile.height * 0.5));
				//float num1 = (float)(Main.player[projectile.owner].position.X + (double)(Main.player[projectile.owner].width / 2) - vector2.X);
				//float num2 = (float)(Main.player[projectile.owner].position.Y + (double)(Main.player[projectile.owner].height / 2) - vector2.Y);
				//projectile.rotation = (float)Math.Atan2((double)num2, (double)num1) + 1.57f;
				//if(Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2) > 900.0)
				//	projectile.ai[0] = 1f;
				if(projectile.wet)
				{
					projectile.rotation = 0.0f;
					projectile.velocity.X *= 0.9f;
					int index1 = (int)(projectile.Center.X + (double)((projectile.width / 2 + 8) * projectile.direction)) / 16;
					int index2 = (int)(projectile.Center.Y / 16.0);
					double num4 = projectile.position.Y / 16.0;
					int index3 = (int)((projectile.position.Y + (double)projectile.height) / 16.0);
					if(Main.tile[index1, index2] == null)
						Main.tile[index1, index2] = new Tile();
					if(Main.tile[index1, index3] == null)
						Main.tile[index1, index3] = new Tile();
					if(projectile.velocity.Y > 0.0)
					{
						projectile.velocity.Y *= 0.5f;
					}
					int index4 = (int)(projectile.Center.X / 16.0);
					int index5 = (int)(projectile.Center.Y / 16.0);
					float num6 = (float)projectile.position.Y + (float)projectile.height;
					if(Main.tile[index4, index5 - 1] == null)
						Main.tile[index4, index5 - 1] = new Tile();
					if(Main.tile[index4, index5] == null)
						Main.tile[index4, index5] = new Tile();
					if(Main.tile[index4, index5 + 1] == null)
						Main.tile[index4, index5 + 1] = new Tile();
					if((int)Main.tile[index4, index5 - 1].liquid > 0)
						num6 = (float)(index5 * 16) - (float)((int)Main.tile[index4, index5 - 1].liquid / 16);
					else if((int)Main.tile[index4, index5].liquid > 0)
						num6 = (float)((index5 + 1) * 16) - (float)((int)Main.tile[index4, index5].liquid / 16);
					else if((int)Main.tile[index4, index5 + 1].liquid > 0)
						num6 = (float)((index5 + 2) * 16) - (float)((int)Main.tile[index4, index5 + 1].liquid / 16);
					if(projectile.Center.Y > (double)num6)
					{
						projectile.velocity.Y -= 0.1f;
						if(projectile.velocity.Y < -8.0f)
							projectile.velocity.Y = -8.0f;
						if(projectile.Center.Y + projectile.velocity.Y < (double)num6)
							projectile.velocity.Y = num6 - projectile.Center.Y;
					}
					else
						projectile.velocity.Y = (float)((double)num6 - projectile.Center.Y);
					//if((double)projectile.velocity.Y >= -0.01f && (double)projectile.velocity.Y <= 0.01f)
					//	flag = true;
				}
				else
				{
					if(projectile.velocity.Y == 0.0)
					{
						projectile.velocity.X *= 0.95f;
					}
					projectile.velocity.X *= 0.98f;
					projectile.velocity.Y += 0.2f;
					if(projectile.velocity.Y > 15.9f)
						projectile.velocity.Y = 15.9f;
				}
				//if(Main.netMode != NetmodeID.MultiplayerClient)
				//{
				//	int num3 = te.GetFishingLevel(te.GetCurrentBait());
				//	if(num3 < 0 && num3 == -1)
				//		te.displayedFishingInfo = Language.GetTextValue("GameUI.FishingWarning");
				//}

				//if((double)projectile.ai[0] != 0.0)
				//	flag = true;
				//if(!flag)
				//	return;
				//if((double)projectile.ai[1] == 0.0 && Main.netMode != NetmodeID.MultiplayerClient)
				//{
				//	int num3 = Main.player[projectile.owner].FishingLevel();
				//	if(num3 == -9000)
				//	{
				//		projectile.localAI[1] += 5f;
				//		projectile.localAI[1] += (float)Main.rand.Next(1, 3);
				//		if((double)projectile.localAI[1] <= 660.0)
				//			return;
				//		projectile.localAI[1] = 0.0f;
				//		projectile.FishingCheck();
				//	}
				//	else
				//	{
				//		if(Main.rand.Next(300) < num3)
				//			projectile.localAI[1] += (float)Main.rand.Next(1, 3);
				//		projectile.localAI[1] += (float)(num3 / 30);
				//		projectile.localAI[1] += (float)Main.rand.Next(1, 3);
				//		if(Main.rand.Next(60) == 0)
				//			projectile.localAI[1] += 60f;
				//		if((double)projectile.localAI[1] <= 660.0)
				//			return;
				//		projectile.localAI[1] = 0.0f;
				//		projectile.FishingCheck();
				//	}
				//}
				//else
				//{
				//	if((double)projectile.ai[1] >= 0.0)
				//		return;
				//	if(projectile.velocity.Y == 0.0 || projectile.honeyWet && (double)projectile.velocity.Y >= -0.01 && (double)projectile.velocity.Y <= 0.01)
				//	{
				//		projectile.velocity.Y = (float)((double)Main.rand.Next(100, 500) * 0.0149999996647239);
				//		projectile.velocity.X = (float)((double)Main.rand.Next(-100, 101) * 0.0149999996647239);
				//		projectile.wet = false;
				//		projectile.lavaWet = false;
				//		projectile.honeyWet = false;
				//	}
				//	projectile.ai[1] += (float)Main.rand.Next(1, 5);
				//	if((double)projectile.ai[1] < 0.0)
				//		return;
				//	projectile.ai[1] = 0.0f;
				//	projectile.localAI[1] = 0.0f;
				//	projectile.netUpdate = true;
				//}
			}
		}

		public int FishingCheck(bool catchRealFish)
		{
			var te = (AutofisherTE)TileEntity.ByID[(int)projectile.ai[1]];

			var baitItem = te.GetCurrentBait();

			int bobberTileX = (int)(projectile.Center.X / 16.0);
			int bobberTileY = (int)(projectile.Center.Y / 16.0);
			if((int)Main.tile[bobberTileX, bobberTileY].liquid < 0)
				++bobberTileY;
			bool lava = false;
			bool honey = false;
			int i1 = bobberTileX;
			int i2 = bobberTileX;
			while(i1 > 10 && (int)Main.tile[i1, bobberTileY].liquid > 0 && !WorldGen.SolidTile(i1, bobberTileY))
				--i1;
			while(i2 < Main.maxTilesX - 10 && (int)Main.tile[i2, bobberTileY].liquid > 0 && !WorldGen.SolidTile(i2, bobberTileY))
				++i2;
			int poolSize = 0;
			for(int i3 = i1; i3 <= i2; ++i3)
			{
				int j2 = bobberTileY;
				while((int)Main.tile[i3, j2].liquid > 0 && !WorldGen.SolidTile(i3, j2) && j2 < Main.maxTilesY - 10)
				{
					++poolSize;
					++j2;
					if(Main.tile[i3, j2].lava())
						lava = true;
					else if(Main.tile[i3, j2].honey())
						honey = true;
				}
			}
			if(honey)
				poolSize = (int)((double)poolSize * 1.5);
			if(poolSize < 75)
			{
				if(Main.netMode != NetmodeID.MultiplayerClient)
					te.DisplayedFishingInfo = Language.GetTextValue("GameUI.NotEnoughWater");
			}
			else
			{
				int fishingPower = te.GetFishingLevel(baitItem);
				if(fishingPower == 0)
					return -1;
				if(Main.netMode != NetmodeID.MultiplayerClient)
					te.DisplayedFishingInfo = Language.GetTextValue("GameUI.FishingPower", (object)fishingPower);
				//if(num2 < 0)
				//{
				//	if(num2 != -1)
				//		return;
				//	te.DisplayedFishingInfo = Language.GetTextValue("GameUI.FishingWarning");
				//	if(index >= 380 && index <= Main.maxTilesX - 380 || (num1 <= 1000 || NPC.AnyNPCs(370)))
				//		return;

				//	projectile.ai[1] = (float)(Main.rand.Next(-180, -60) - 100);

				//	projectile.localAI[1] = (float)num2;

				//	projectile.netUpdate = true;

				//}
				//else
				{
					int num3 = 300;
					float num4 = (float)(Main.maxTilesX / 4200);
					float num5 = (float)((projectile.position.Y / 16.0 - (60.0 + 10.0 * (double)(num4 * num4))) / (Main.worldSurface / 6.0));
					if((double)num5 < 0.25)
						num5 = 0.25f;
					if((double)num5 > 1.0)
						num5 = 1f;
					int num6 = (int)((double)num3 * (double)num5);
					float num7 = (float)poolSize / (float)num6;
					if((double)num7 < 1.0)
						fishingPower = (int)((double)fishingPower * (double)num7);
					float num8 = 1f - num7;
					if(poolSize < num6 && Main.netMode != NetmodeID.MultiplayerClient)
						te.DisplayedFishingInfo = Language.GetTextValue("GameUI.FullFishingPower", (object)fishingPower, (object)-Math.Round((double)num8 * 100.0));
					int num9 = (fishingPower + 75) / 2;

					if(!catchRealFish)
						return -1;

					if(/*Main.player[projectile.owner].wet || */Main.rand.Next(100) > num9)
						return -1;
					int caughtType = 0;
					int worldLayer = (double)bobberTileY >= Main.worldSurface * 0.5 ? ((double)bobberTileY >= Main.worldSurface ? ((double)bobberTileY >= Main.rockLayer ? (bobberTileY >= Main.maxTilesY - 300 ? 4 : 3) : 2) : 1) : 0;
					int num12 = 150;
					int num13 = fishingPower;
					int maxValue1 = num12 / num13;
					int num14 = 2;
					int maxValue2 = num12 * num14 / fishingPower;
					int num15 = 7;
					int maxValue3 = num12 * num15 / fishingPower;
					int num16 = 15;
					int maxValue4 = num12 * num16 / fishingPower;
					int num17 = 30;
					int maxValue5 = num12 * num17 / fishingPower;
					if(maxValue1 < 2)

						maxValue1 = 2;
					if(maxValue2 < 3)

						maxValue2 = 3;
					if(maxValue3 < 4)

						maxValue3 = 4;
					if(maxValue4 < 5)

						maxValue4 = 5;
					if(maxValue5 < 6)

						maxValue5 = 6;
					bool flag3 = false;
					bool flag4 = false;
					bool flag5 = false;
					bool flag6 = false;
					bool flag7 = false;
					if(Main.rand.Next(maxValue1) == 0)
						flag3 = true;
					if(Main.rand.Next(maxValue2) == 0)
						flag4 = true;
					if(Main.rand.Next(maxValue3) == 0)
						flag5 = true;
					if(Main.rand.Next(maxValue4) == 0)
						flag6 = true;
					if(Main.rand.Next(maxValue5) == 0)
						flag7 = true;
					int num18 = 10;
					//if(Main.player[projectile.owner].cratePotion)
					//	num18 += 10;
					int type = -1;
					var zone = MiscUtils.GetZoneInLocation(te.Position.X, te.Position.Y);

					bool junk = false;

					#region Vanilla Fishing Algorithm

					if(lava)
					{
						//if (!ItemID.Sets.CanFishInLava[Main.player[projectile.owner].HeldItem.type])
						//  return;
						if(flag7)
							caughtType = 2331;
						else if(flag6)
							caughtType = 2312;
						else if(flag5)
							caughtType = 2315;
					}
					else if(honey)
					{
						if(flag5 || flag4 && Main.rand.Next(2) == 0)
							caughtType = 2314;
						else if(flag4 && type == 2451)
							caughtType = 2451;
					}
					else if(Main.rand.Next(50) > fishingPower && Main.rand.Next(50) > fishingPower && poolSize < num6)
					{
						junk = true;
						caughtType = Main.rand.Next(2337, 2340);
					}
					else if(Main.rand.Next(100) < num18)
						caughtType = !(flag6 | flag7) ? (!flag5 || !zone.HasFlag(Zone.Corrupt) ? (!flag5 || !zone.HasFlag(Zone.Crimson) ? (!flag5 || !zone.HasFlag(Zone.Holy) ? (!flag5 || !zone.HasFlag(Zone.Dungeon) ? (!flag5 || !zone.HasFlag(Zone.Jungle) ? (!flag5 || worldLayer != 0 ? (!flag4 ? 2334 : 2335) : 3206) : 3208) : 3205) : 3207) : 3204) : 3203) : 2336;
					else if(flag7 && Main.rand.Next(5) == 0)
						caughtType = 2423;
					else if(flag7 && Main.rand.Next(5) == 0)
						caughtType = 3225;
					else if(flag7 && Main.rand.Next(10) == 0)
						caughtType = 2420;
					else if(((flag7 ? 0 : (!flag6 ? 1 : 0)) & (flag4 ? 1 : 0)) != 0 && Main.rand.Next(5) == 0)
					{
						caughtType = 3196;
					}
					else
					{
						bool flag8 = zone.HasFlag(Zone.Corrupt);
						bool flag9 = zone.HasFlag(Zone.Crimson);
						if(flag8 && flag9)
						{
							if(Main.rand.Next(2) == 0)
								flag9 = false;
							else
								flag8 = false;
						}
						if(flag8)
						{
							if(flag7 && Main.hardMode && (zone.HasFlag(Zone.Snow) && worldLayer == 3) && Main.rand.Next(3) != 0)
								caughtType = 2429;
							else if(flag7 && Main.hardMode && Main.rand.Next(2) == 0)
								caughtType = 3210;
							else if(flag5)
								caughtType = 2330;
							else if(flag4 && type == 2454)
								caughtType = 2454;
							else if(flag4 && type == 2485)
								caughtType = 2485;
							else if(flag4 && type == 2457)
								caughtType = 2457;
							else if(flag4)
								caughtType = 2318;
						}
						else if(flag9)
						{
							if(flag7 && Main.hardMode && (zone.HasFlag(Zone.Snow) && worldLayer == 3) && Main.rand.Next(3) != 0)
								caughtType = 2429;
							else if(flag7 && Main.hardMode && Main.rand.Next(2) == 0)
								caughtType = 3211;
							else if(flag4 && type == 2477)
								caughtType = 2477;
							else if(flag4 && type == 2463)
								caughtType = 2463;
							else if(flag4)
								caughtType = 2319;
							else if(flag3)
								caughtType = 2305;
						}
						else if(zone.HasFlag(Zone.Holy))
						{
							if(flag7 && Main.hardMode && (zone.HasFlag(Zone.Snow) && worldLayer == 3) && Main.rand.Next(3) != 0)
								caughtType = 2429;
							else if(flag7 && Main.hardMode && Main.rand.Next(2) == 0)
								caughtType = 3209;
							else if(worldLayer > 1 & flag6)
								caughtType = 2317;
							else if(worldLayer > 1 & flag5 && type == 2465)
								caughtType = 2465;
							else if(worldLayer < 2 & flag5 && type == 2468)

								caughtType = 2468;
							else if(flag5)
								caughtType = 2310;
							else if(flag4 && type == 2471)
								caughtType = 2471;
							else if(flag4)
								caughtType = 2307;
						}
						if(caughtType == 0 && zone.HasFlag(Zone.Snow))
						{
							if(worldLayer < 2 & flag4 && type == 2467)

								caughtType = 2467;
							else if(worldLayer == 1 & flag4 && type == 2470)
								caughtType = 2470;
							else if(worldLayer >= 2 & flag4 && type == 2484)
								caughtType = 2484;
							else if(worldLayer > 1 & flag4 && type == 2466)
								caughtType = 2466;
							else if(flag3 && Main.rand.Next(12) == 0 || flag4 && Main.rand.Next(6) == 0)
								caughtType = 3197;
							else if(flag4)
								caughtType = 2306;
							else if(flag3)
								caughtType = 2299;
							else if(worldLayer > 1 && Main.rand.Next(3) == 0)
								caughtType = 2309;
						}
						if(caughtType == 0 && zone.HasFlag(Zone.Jungle))
						{
							if(worldLayer == 1 & flag4 && type == 2452)
								caughtType = 2452;
							else if(worldLayer == 1 & flag4 && type == 2483)
								caughtType = 2483;
							else if(worldLayer == 1 & flag4 && type == 2488)
								caughtType = 2488;
							else if(worldLayer >= 1 & flag4 && type == 2486)
								caughtType = 2486;
							else if(worldLayer > 1 & flag4)
								caughtType = 2311;
							else if(flag4)
								caughtType = 2313;
							else if(flag3)
								caughtType = 2302;
						}
						if(caughtType == 0 && zone.HasFlag(Zone.Shroom) && (flag4 && type == 2475))
							caughtType = 2475;
						if(caughtType == 0)
						{
							if(worldLayer <= 1 && (bobberTileX < 380 || bobberTileX > Main.maxTilesX - 380) && poolSize > 1000)
							{
								caughtType = !flag6 || Main.rand.Next(2) != 0 ? (!flag6 ? (!flag5 || Main.rand.Next(5) != 0 ? (!flag5 || Main.rand.Next(2) != 0 ? (!flag4 || type != 2480 ? (!flag4 || type != 2481 ? (!flag4 ? (!flag3 || Main.rand.Next(2) != 0 ? (!flag3 ? 2297 : 2300) : 2301) : 2316) : 2481) : 2480) : 2332) : 2438) : 2342) : 2341;
							}
							else
							{
								//int sandTiles = Main.sandTiles; //doesn't seem to do anything?
							}
						}
						if(caughtType == 0)
							caughtType = !(worldLayer < 2 & flag4) || type != 2461 ? (!(worldLayer == 0 & flag4) || type != 2453 ? (!(worldLayer == 0 & flag4) || type != 2473 ? (!(worldLayer == 0 & flag4) || type != 2476 ? (!(worldLayer < 2 & flag4) || type != 2458 ? (!(worldLayer < 2 & flag4) || type != 2459 ? (!(worldLayer == 0 & flag4) ? (((worldLayer <= 0 ? 0 : (worldLayer < 3 ? 1 : 0)) & (flag4 ? 1 : 0)) == 0 || type != 2455 ? (!(worldLayer == 1 & flag4) || type != 2479 ? (!(worldLayer == 1 & flag4) || type != 2456 ? (!(worldLayer == 1 & flag4) || type != 2474 ? (!(worldLayer > 1 & flag5) || Main.rand.Next(5) != 0 ? (!(worldLayer > 1 & flag7) ? (!(worldLayer > 1 & flag6) || Main.rand.Next(2) != 0 ? (!(worldLayer > 1 & flag5) ? (!(worldLayer > 1 & flag4) || type != 2478 ? (!(worldLayer > 1 & flag4) || type != 2450 ? (!(worldLayer > 1 & flag4) || type != 2464 ? (!(worldLayer > 1 & flag4) || type != 2469 ? (!(worldLayer > 2 & flag4) || type != 2462 ? (!(worldLayer > 2 & flag4) || type != 2482 ? (!(worldLayer > 2 & flag4) || type != 2472 ? (!(worldLayer > 2 & flag4) || type != 2460 ? (!(worldLayer > 1 & flag4) || Main.rand.Next(4) == 0 ? (worldLayer <= 1 || !(flag4 | flag3) && Main.rand.Next(4) != 0 ? (!flag4 || type != 2487 ? (!(poolSize > 1000 & flag3) ? 2290 : 2298) : 2487) : (Main.rand.Next(4) != 0 ? 2309 : 2303)) : 2303) : 2460) : 2472) : 2482) : 2462) : 2469) : 2464) : 2450) : 2478) : 2321) : 2320) : 2308) : (!Main.hardMode || Main.rand.Next(2) != 0 ? 2436 : 2437)) : 2474) : 2456) : 2479) : 2455) : 2304) : 2459) : 2458) : 2476) : 2473) : 2453) : 2461;
					}

					#endregion

					AutofisherHooks.CatchFish(te, zone, baitItem, fishingPower, lava ? 1 : (honey ? 2 : 0), poolSize, worldLayer, ref caughtType, ref junk);

					if(caughtType <= 0 /*|| Crates.Contains(num10) */)
						return -1;

					//          if (Main.player[projectile.owner].sonarPotion)
					//          {
					//            Item newItem = new Item();
					//int Type = num10;
					//int num19 = 0;
					//newItem.SetDefaults(Type, num19 != 0);
					//            Vector2 position = projectile.position;
					//newItem.position = position;
					//            int stack = 1;
					//int num20 = 1;
					//int num21 = 0;
					//ItemText.NewText(newItem, stack, num20 != 0, num21 != 0);
					//          }

					//float num22 = (float)num2;

					//projectile.ai[1] = (float)Main.rand.Next(-240, -90) - num22;

					//projectile.localAI[1] = (float)num10;

					//projectile.netUpdate = true;
					projectile.ai[0] = caughtType;
					projectile.netUpdate = true;
					return junk? -2 : caughtType;
				}
			}
			return -1;
		}
		
		public override bool PreDrawExtras(SpriteBatch spriteBatch)
		{
			if(!TileEntity.ByID.ContainsKey((int)projectile.ai[1]))
				return false;

			var te = TileEntity.ByID[(int)projectile.ai[1]] as AutofisherTE;
			if(te == null)
				return false;
			//Lighting.AddLight(projectile.Center, 0.7f, 0.9f, 0.6f);
			//if(projectile.bobber && Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].holdStyle > 0)
			{
				//float pPosX = player.MountedCenter.X;
				//float pPosY = player.MountedCenter.Y;
				//pPosY += Main.player[projectile.owner].gfxOffY;
				//int type = Main.player[projectile.owner].inventory[Main.player[projectile.owner].selectedItem].type;
				//float gravDir = Main.player[projectile.owner].gravDir;

				//if(type == mod.ItemType("GooFishingPole"))
				//{
				//	pPosX += (float)(50 * Main.player[projectile.owner].direction);
				//	if(Main.player[projectile.owner].direction < 0)
				//	{
				//		pPosX -= 13f;
				//	}
				//	pPosY -= 30f * gravDir;
				//}

				//if(gravDir == -1f)
				//{
				//	pPosY -= 12f;
				//}
				bool facingRight = Main.tile[te.Position.X, te.Position.Y].frameX == 0;
				Vector2 value = te.Position.ToWorldCoordinates(2, 0);
				value -= new Vector2(6f, 4f); //brute-force fix for weird offset
				if(facingRight)
					value.X += 46f;


				//value = Main.player[projectile.owner].RotatedRelativePoint(value + new Vector2(8f), true) - new Vector2(8f);
				float projPosX = projectile.position.X + (float)projectile.width * 0.5f - value.X;
				float projPosY = projectile.position.Y + (float)projectile.height * 0.5f - value.Y;
				Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
				float rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX) - 1.57f;
				bool flag2 = true;
				if(projPosX == 0f && projPosY == 0f)
				{
					flag2 = false;
				}
				else
				{
					float projPosXY = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
					projPosXY = 12f / projPosXY;
					projPosX *= projPosXY;
					projPosY *= projPosXY;
					value.X -= projPosX;
					value.Y -= projPosY;
					projPosX = projectile.position.X + (float)projectile.width * 0.5f - value.X;
					projPosY = projectile.position.Y + (float)projectile.height * 0.5f - value.Y;
				}
				while(flag2)
				{
					float num = 12f;
					float num2 = (float)Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
					float num3 = num2;
					if(float.IsNaN(num2) || float.IsNaN(num3))
					{
						flag2 = false;
					}
					else
					{
						if(num2 < 20f)
						{
							num = num2 - 8f;
							flag2 = false;
						}
						num2 = 12f / num2;
						projPosX *= num2;
						projPosY *= num2;
						value.X += projPosX;
						value.Y += projPosY;
						projPosX = projectile.position.X + (float)projectile.width * 0.5f - value.X;
						projPosY = projectile.position.Y + (float)projectile.height * 0.1f - value.Y;
						if(num3 > 12f)
						{
							float num4 = 0.3f;
							float num5 = Math.Abs(projectile.velocity.X) + Math.Abs(projectile.velocity.Y);
							if(num5 > 16f)
							{
								num5 = 16f;
							}
							num5 = 1f - num5 / 16f;
							num4 *= num5;
							num5 = num3 / 80f;
							if(num5 > 1f)
							{
								num5 = 1f;
							}
							num4 *= num5;
							if(num4 < 0f)
							{
								num4 = 0f;
							}
							num5 = 1f - projectile.localAI[0] / 100f;
							num4 *= num5;
							if(projPosY > 0f)
							{
								projPosY *= 1f + num4;
								projPosX *= 1f - num4;
							}
							else
							{
								num5 = Math.Abs(projectile.velocity.X) / 3f;
								if(num5 > 1f)
								{
									num5 = 1f;
								}
								num5 -= 0.5f;
								num4 *= num5;
								if(num4 > 0f)
								{
									num4 *= 2f;
								}
								projPosY *= 1f + num4;
								projPosX *= 1f - num4;
							}
						}
						rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX) - 1.57f;
						Color color2 = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), FishingLineColor);
						Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(value.X - Main.screenPosition.X + (float)Main.fishingLineTexture.Width * 0.5f, value.Y - Main.screenPosition.Y + (float)Main.fishingLineTexture.Height * 0.5f), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num)), color2, rotation2, new Vector2((float)Main.fishingLineTexture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			return false;
		}
	}
}
