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
		public override string Texture
		{
			get { return "Terraria/Projectile_" + ProjectileID.BobberMechanics; }
		}

		public override bool Autoload(ref string name)
		{
			return Config.Autofisher;
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
			//Main.NewText(projectile.ai[1]);
			if(!TileEntity.ByID.ContainsKey((int)projectile.ai[1]) || !(TileEntity.ByID[(int)projectile.ai[1]] is AutofisherTE))
			{
				projectile.Kill();
				//Main.NewText("kill me");
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
						if((double)projectile.ai[0] > 0.0 && (double)projectile.ai[0] < 3930.0)
						{
							int Type = (int)projectile.ai[0];
							Item newItem = new Item();
							newItem.SetDefaults(Type, true);
							if(Type == 3196)
							{
								int num1 = te.GetFishingLevel();
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
								int num1 = te.GetFishingLevel();
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
							Item.NewItem(te.Position.ToWorldCoordinates(0f, 0f), 48, 32, newItem.type, newItem.stack);
						}
						projectile.Kill();
					}
				}
				projectile.rotation = (float)Math.Atan2((double)projectile.velocity.Y, (double)projectile.velocity.X) + 1.57f;
			}
			else
			{
				//Main.NewText("just fishin");
				//bool flag = false;
				//Vector2 vector2 = new Vector2((float)(projectile.position.X + (double)projectile.width * 0.5), (float)(projectile.position.Y + (double)projectile.height * 0.5));
				//float num1 = (float)(Main.player[projectile.owner].position.X + (double)(Main.player[projectile.owner].width / 2) - vector2.X);
				//float num2 = (float)(Main.player[projectile.owner].position.Y + (double)(Main.player[projectile.owner].height / 2) - vector2.Y);
				//projectile.rotation = (float)Math.Atan2((double)num2, (double)num1) + 1.57f;
				//if(Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2) > 900.0)
				//	projectile.ai[0] = 1f;
				if(projectile.wet)
				{
					//Main.NewText("wet");
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
					//Main.NewText("not wet");
					if(projectile.velocity.Y == 0.0)
					{
						projectile.velocity.X *= 0.95f;
					}
					projectile.velocity.X *= 0.98f;
					projectile.velocity.Y += 0.2f;
					if(projectile.velocity.Y > 15.9f)
						projectile.velocity.Y = 15.9f;
				}
				//if(Main.netMode != NetmodeID.Server)
				//{
				//	int num3 = Main.player[projectile.owner].FishingLevel();
				//	if(num3 < 0 && num3 == -1)
				//		Main.player[projectile.owner].displayedFishingInfo = Language.GetTextValue("GameUI.FishingWarning");
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

		public int FishingCheck()
		{
			var te = (AutofisherTE)TileEntity.ByID[(int)projectile.ai[1]];

			int index = (int)(projectile.Center.X / 16.0);
			int j1 = (int)(projectile.Center.Y / 16.0);
			if((int)Main.tile[index, j1].liquid < 0)
				++j1;
			bool flag1 = false;
			bool flag2 = false;
			int i1 = index;
			int i2 = index;
			while(i1 > 10 && (int)Main.tile[i1, j1].liquid > 0 && !WorldGen.SolidTile(i1, j1))
				--i1;
			while(i2 < Main.maxTilesX - 10 && (int)Main.tile[i2, j1].liquid > 0 && !WorldGen.SolidTile(i2, j1))
				++i2;
			int num1 = 0;
			for(int i3 = i1; i3 <= i2; ++i3)
			{
				int j2 = j1;
				while((int)Main.tile[i3, j2].liquid > 0 && !WorldGen.SolidTile(i3, j2) && j2 < Main.maxTilesY - 10)
				{
					++num1;
					++j2;
					if(Main.tile[i3, j2].lava())
						flag1 = true;
					else if(Main.tile[i3, j2].honey())
						flag2 = true;
				}
			}
			if(flag2)
				num1 = (int)((double)num1 * 1.5);
			if(num1 < 75)
			{
				//if(Main.netMode != NetmodeID.SinglePlayer)
				//	Main.NewText(Language.GetTextValue("GameUI.NotEnoughWater"));
					//te.DisplayedFishingInfo = Language.GetTextValue("GameUI.NotEnoughWater");
			}
			else
			{
				int num2 = te.GetFishingLevel();
				if(num2 == 0)
					return -1;
				//Main.NewText(Language.GetTextValue("GameUI.FishingPower", (object)num2));
				//te.DisplayedFishingInfo = Language.GetTextValue("GameUI.FishingPower", (object)num2);
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
					float num7 = (float)num1 / (float)num6;
					if((double)num7 < 1.0)
						num2 = (int)((double)num2 * (double)num7);
					float num8 = 1f - num7;
					//if(num1 < num6)
					//	Main.NewText(Language.GetTextValue("GameUI.FullFishingPower", (object)num2, (object)-Math.Round((double)num8 * 100.0)));
						//te.DisplayedFishingInfo = Language.GetTextValue("GameUI.FullFishingPower", (object)num2, (object)-Math.Round((double)num8 * 100.0));
					int num9 = (num2 + 75) / 2;
					if(/*Main.player[projectile.owner].wet || */Main.rand.Next(100) > num9)
						return -1;
					int num10 = 0;
					int num11 = (double)j1 >= Main.worldSurface * 0.5 ? ((double)j1 >= Main.worldSurface ? ((double)j1 >= Main.rockLayer ? (j1 >= Main.maxTilesY - 300 ? 4 : 3) : 2) : 1) : 0;
					int num12 = 150;
					int num13 = num2;
					int maxValue1 = num12 / num13;
					int num14 = 2;
					int maxValue2 = num12 * num14 / num2;
					int num15 = 7;
					int maxValue3 = num12 * num15 / num2;
					int num16 = 15;
					int maxValue4 = num12 * num16 / num2;
					int num17 = 30;
					int maxValue5 = num12 * num17 / num2;
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
					//Main.NewText(zone);
					if(flag1)
					{
						//if (!ItemID.Sets.CanFishInLava[Main.player[projectile.owner].HeldItem.type])
						//  return;
						if(flag7)
							num10 = 2331;
						else if(flag6)
							num10 = 2312;
						else if(flag5)
							num10 = 2315;
					}
					else if(flag2)
					{
						if(flag5 || flag4 && Main.rand.Next(2) == 0)
							num10 = 2314;
						else if(flag4 && type == 2451)
							num10 = 2451;
					}
					else if(Main.rand.Next(50) > num2 && Main.rand.Next(50) > num2 && num1 < num6)

						num10 = Main.rand.Next(2337, 2340);
					else if(Main.rand.Next(100) < num18)
						num10 = !(flag6 | flag7) ? (!flag5 || !zone.HasFlag(Zone.Corrupt) ? (!flag5 || !zone.HasFlag(Zone.Crimson) ? (!flag5 || !zone.HasFlag(Zone.Holy) ? (!flag5 || !zone.HasFlag(Zone.Dungeon) ? (!flag5 || !zone.HasFlag(Zone.Jungle) ? (!flag5 || num11 != 0 ? (!flag4 ? 2334 : 2335) : 3206) : 3208) : 3205) : 3207) : 3204) : 3203) : 2336;
					else if(flag7 && Main.rand.Next(5) == 0)
						num10 = 2423;
					else if(flag7 && Main.rand.Next(5) == 0)
						num10 = 3225;
					else if(flag7 && Main.rand.Next(10) == 0)
						num10 = 2420;
					else if(((flag7 ? 0 : (!flag6 ? 1 : 0)) & (flag4 ? 1 : 0)) != 0 && Main.rand.Next(5) == 0)
					{
						num10 = 3196;
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
							if(flag7 && Main.hardMode && (zone.HasFlag(Zone.Snow) && num11 == 3) && Main.rand.Next(3) != 0)
								num10 = 2429;
							else if(flag7 && Main.hardMode && Main.rand.Next(2) == 0)
								num10 = 3210;
							else if(flag5)
								num10 = 2330;
							else if(flag4 && type == 2454)
								num10 = 2454;
							else if(flag4 && type == 2485)
								num10 = 2485;
							else if(flag4 && type == 2457)
								num10 = 2457;
							else if(flag4)
								num10 = 2318;
						}
						else if(flag9)
						{
							if(flag7 && Main.hardMode && (zone.HasFlag(Zone.Snow) && num11 == 3) && Main.rand.Next(3) != 0)
								num10 = 2429;
							else if(flag7 && Main.hardMode && Main.rand.Next(2) == 0)
								num10 = 3211;
							else if(flag4 && type == 2477)
								num10 = 2477;
							else if(flag4 && type == 2463)
								num10 = 2463;
							else if(flag4)
								num10 = 2319;
							else if(flag3)
								num10 = 2305;
						}
						else if(zone.HasFlag(Zone.Holy))
						{
							if(flag7 && Main.hardMode && (zone.HasFlag(Zone.Snow) && num11 == 3) && Main.rand.Next(3) != 0)
								num10 = 2429;
							else if(flag7 && Main.hardMode && Main.rand.Next(2) == 0)
								num10 = 3209;
							else if(num11 > 1 & flag6)
								num10 = 2317;
							else if(num11 > 1 & flag5 && type == 2465)
								num10 = 2465;
							else if(num11 < 2 & flag5 && type == 2468)

								num10 = 2468;
							else if(flag5)
								num10 = 2310;
							else if(flag4 && type == 2471)
								num10 = 2471;
							else if(flag4)
								num10 = 2307;
						}
						if(num10 == 0 && zone.HasFlag(Zone.Snow))
						{
							if(num11 < 2 & flag4 && type == 2467)

								num10 = 2467;
							else if(num11 == 1 & flag4 && type == 2470)
								num10 = 2470;
							else if(num11 >= 2 & flag4 && type == 2484)
								num10 = 2484;
							else if(num11 > 1 & flag4 && type == 2466)
								num10 = 2466;
							else if(flag3 && Main.rand.Next(12) == 0 || flag4 && Main.rand.Next(6) == 0)
								num10 = 3197;
							else if(flag4)
								num10 = 2306;
							else if(flag3)
								num10 = 2299;
							else if(num11 > 1 && Main.rand.Next(3) == 0)
								num10 = 2309;
						}
						if(num10 == 0 && zone.HasFlag(Zone.Jungle))
						{
							if(num11 == 1 & flag4 && type == 2452)
								num10 = 2452;
							else if(num11 == 1 & flag4 && type == 2483)
								num10 = 2483;
							else if(num11 == 1 & flag4 && type == 2488)
								num10 = 2488;
							else if(num11 >= 1 & flag4 && type == 2486)
								num10 = 2486;
							else if(num11 > 1 & flag4)
								num10 = 2311;
							else if(flag4)
								num10 = 2313;
							else if(flag3)
								num10 = 2302;
						}
						if(num10 == 0 && zone.HasFlag(Zone.Shroom) && (flag4 && type == 2475))
							num10 = 2475;
						if(num10 == 0)
						{
							if(num11 <= 1 && (index < 380 || index > Main.maxTilesX - 380) && num1 > 1000)
							{
								num10 = !flag6 || Main.rand.Next(2) != 0 ? (!flag6 ? (!flag5 || Main.rand.Next(5) != 0 ? (!flag5 || Main.rand.Next(2) != 0 ? (!flag4 || type != 2480 ? (!flag4 || type != 2481 ? (!flag4 ? (!flag3 || Main.rand.Next(2) != 0 ? (!flag3 ? 2297 : 2300) : 2301) : 2316) : 2481) : 2480) : 2332) : 2438) : 2342) : 2341;
							}
							else
							{
								//int sandTiles = Main.sandTiles; //doesn't seem to do anything?
							}
						}
						if(num10 == 0)
							num10 = !(num11 < 2 & flag4) || type != 2461 ? (!(num11 == 0 & flag4) || type != 2453 ? (!(num11 == 0 & flag4) || type != 2473 ? (!(num11 == 0 & flag4) || type != 2476 ? (!(num11 < 2 & flag4) || type != 2458 ? (!(num11 < 2 & flag4) || type != 2459 ? (!(num11 == 0 & flag4) ? (((num11 <= 0 ? 0 : (num11 < 3 ? 1 : 0)) & (flag4 ? 1 : 0)) == 0 || type != 2455 ? (!(num11 == 1 & flag4) || type != 2479 ? (!(num11 == 1 & flag4) || type != 2456 ? (!(num11 == 1 & flag4) || type != 2474 ? (!(num11 > 1 & flag5) || Main.rand.Next(5) != 0 ? (!(num11 > 1 & flag7) ? (!(num11 > 1 & flag6) || Main.rand.Next(2) != 0 ? (!(num11 > 1 & flag5) ? (!(num11 > 1 & flag4) || type != 2478 ? (!(num11 > 1 & flag4) || type != 2450 ? (!(num11 > 1 & flag4) || type != 2464 ? (!(num11 > 1 & flag4) || type != 2469 ? (!(num11 > 2 & flag4) || type != 2462 ? (!(num11 > 2 & flag4) || type != 2482 ? (!(num11 > 2 & flag4) || type != 2472 ? (!(num11 > 2 & flag4) || type != 2460 ? (!(num11 > 1 & flag4) || Main.rand.Next(4) == 0 ? (num11 <= 1 || !(flag4 | flag3) && Main.rand.Next(4) != 0 ? (!flag4 || type != 2487 ? (!(num1 > 1000 & flag3) ? 2290 : 2298) : 2487) : (Main.rand.Next(4) != 0 ? 2309 : 2303)) : 2303) : 2460) : 2472) : 2482) : 2462) : 2469) : 2464) : 2450) : 2478) : 2321) : 2320) : 2308) : (!Main.hardMode || Main.rand.Next(2) != 0 ? 2436 : 2437)) : 2474) : 2456) : 2479) : 2455) : 2304) : 2459) : 2458) : 2476) : 2473) : 2453) : 2461;
					}
					if(num10 <= 0 /*|| Crates.Contains(num10) */)
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
					projectile.ai[0] = num10;
					projectile.netUpdate = true;
					return num10;
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
						//Color color2 = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), new Color(207, 205, 192));    //fishing line color
						var color2 = new Color(200, 80, 80);
						Main.spriteBatch.Draw(Main.fishingLineTexture, new Vector2(value.X - Main.screenPosition.X + (float)Main.fishingLineTexture.Width * 0.5f, value.Y - Main.screenPosition.Y + (float)Main.fishingLineTexture.Height * 0.5f), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, Main.fishingLineTexture.Width, (int)num)), color2, rotation2, new Vector2((float)Main.fishingLineTexture.Width * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			return false;
		}
	}
}
