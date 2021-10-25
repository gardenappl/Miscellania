using GoldensMisc.Tiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Terraria;
using Terraria.GameContent;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;

namespace GoldensMisc.Projectiles
{
	public class AutofisherBobber : ModProjectile
	{
		static readonly Color FishingLineColor = new Color(250, 90, 70, 100);
		
		public override string Texture => "Terraria/Images/Projectile_" + ProjectileID.BobberMechanics;

		public override bool IsLoadingEnabled (Mod mod)
		{
			return ModContent.GetInstance<ServerConfig>().Autofisher;
		}

		public override void SetDefaults()
		{
			Projectile.width = 14;
			Projectile.height = 14;
			Projectile.penetrate = -1;
			DrawOriginOffsetY = -8; 
			//Projectile.bobber = true;
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
			if(!TileEntity.ByID.ContainsKey((int)Projectile.ai[1]) || !(TileEntity.ByID[(int)Projectile.ai[1]] is AutofisherTE))
			{
				Projectile.Kill();
				return;
			}
			var te = (AutofisherTE)TileEntity.ByID[(int)Projectile.ai[1]];
			if(te.GetCurrentBait() == null)
			{
				Projectile.Kill();
				return;
			}
			//Projectile.timeLeft = 60;
			//if(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].fishingPole == 0 || Main.player[Projectile.owner].CCed || Main.player[Projectile.owner].noItems)
			//	Projectile.Kill();
			//else if(Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].shoot != Projectile.type)
			//	Projectile.Kill();
			//else if(Main.player[Projectile.owner].pulley)
			//	Projectile.Kill();
			//else if(Main.player[Projectile.owner].dead)
			//	Projectile.Kill();

			//if((double)Projectile.ai[1] > 0.0 && (double)Projectile.localAI[1] >= 0.0)
			//{
			//	Projectile.localAI[1] = -1f;
			//	if(!Projectile.lavaWet && !Projectile.honeyWet)
			//	{
			//		for(int index1 = 0; index1 < 100; ++index1)
			//		{
			//			int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X - 6.0), (float)(Projectile.position.Y - 10.0)), Projectile.width + 12, 24, Dust.dustWater(), 0.0f, 0.0f, 0);
			//			Main.dust[index2].velocity.Y -= 4.0f;
			//			Main.dust[index2].velocity.X *= 2.5f;
			//			Main.dust[index2].scale = 0.8f;
			//			Main.dust[index2].alpha = 100;
			//			Main.dust[index2].noGravity = true;
			//		}
			//		Main.PlaySound(19, (int)Projectile.position.X, (int)Projectile.position.Y, 0, 1f, 0.0f);
			//	}
			//}

			if((double)Projectile.ai[0] >= 1.0)
			{
				//don't know what this is
				//if((double)Projectile.ai[0] == 2.0)
				//{
				//	++Projectile.ai[0];
				//	Main.PlaySound(SoundID.Item17, Projectile.position);
				//	if(!Projectile.lavaWet && !Projectile.honeyWet)
				//	{
				//		for(int index1 = 0; index1 < 100; ++index1)
				//		{
				//			int index2 = Dust.NewDust(new Vector2((float)(Projectile.position.X - 6.0), (float)(Projectile.position.Y - 10.0)), Projectile.width + 12, 24, Dust.dustWater(), 0.0f, 0.0f, 0);
				//			Main.dust[index2].velocity.Y -= 4.0f;
				//			Main.dust[index2].velocity.X *= 2.5f;
				//			Main.dust[index2].scale = 0.8f;
				//			Main.dust[index2].alpha = 100;
				//			Main.dust[index2].noGravity = true;
				//		}
				//		Main.PlaySound(19, (int)Projectile.position.X, (int)Projectile.position.Y, 0, 1f, 0.0f);
				//	}
				//}
				//if((double)Projectile.localAI[0] < 100.0)
				//	++Projectile.localAI[0];
				Projectile.tileCollide = false;
				const double productDivisor = 15.8999996185303;
				const double ten = 10;
				Vector2 vector2 = new Vector2((float)(Projectile.position.X + (double)Projectile.width * 0.5), (float)(Projectile.position.Y + (double)Projectile.height * 0.5));
				//float num5 = (float)(Main.player[Projectile.owner].position.X + (double)(Main.player[Projectile.owner].width / 2) - vector2.X);
				//float num6 = (float)(Main.player[Projectile.owner].position.Y + (double)(Main.player[Projectile.owner].height / 2) - vector2.Y);
				float num5 = (te.Position.X + 1.5f) * 16f - vector2.X;
				float num6 = (te.Position.Y + 1) * 16f - vector2.Y; 
				float num7 = (float)Math.Sqrt((double)num5 * (double)num5 + (double)num6 * (double)num6);
				if((double)num7 > 3000.0)
					Projectile.Kill();
				double num8 = (double)num7;
				float num9 = (float)(productDivisor / num8);
				float num10 = num5 * num9;
				float num11 = num6 * num9;
				Projectile.velocity.X = (float)((Projectile.velocity.X * (double)(ten - 1) + (double)num10) / ten);
				Projectile.velocity.Y = (float)((Projectile.velocity.Y * (double)(ten - 1) + (double)num11) / ten);
				//if(Main.myPlayer == Projectile.owner)
				if(Main.netMode != NetmodeID.MultiplayerClient)
				{
					Rectangle rectangle1 = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
					Rectangle rectangle2 = new Rectangle(te.Position.X * 16, te.Position.Y * 16, 48, 32);
					if(((Rectangle)rectangle1).Intersects(rectangle2))
					{
						if((double)Projectile.ai[0] > 0.0)
						{
							int Type = (int)Projectile.ai[0];
							Item newItem = new Item();
							newItem.SetDefaults(Type, true);
							if(Type == 3196)
							{
								int FinalFishingLevel = te.GetFishingLevel(te.GetCurrentBait());
								int minValue = (FinalFishingLevel / 20 + 3) / 2;
								int maxValue = (FinalFishingLevel / 10 + 6) / 2;
								if(Main.rand.Next(50) < FinalFishingLevel)
									++maxValue;
								if(Main.rand.Next(100) < FinalFishingLevel)
									++maxValue;
								if(Main.rand.Next(150) < FinalFishingLevel)
									++maxValue;
								if(Main.rand.Next(200) < FinalFishingLevel)
									++maxValue;
								int count = Main.rand.Next(minValue, maxValue + 1);
								newItem.stack = count;
							}
							if(Type == 3197)
							{
								int FinalFishingLevel = te.GetFishingLevel(te.GetCurrentBait());
								int minValue = (FinalFishingLevel / 4 + 15) / 2;
								int maxValue = (FinalFishingLevel / 2 + 30) / 2;
								if(Main.rand.Next(50) < FinalFishingLevel)
									maxValue += 4;
								if(Main.rand.Next(100) < FinalFishingLevel)
									maxValue += 4;
								if(Main.rand.Next(150) < FinalFishingLevel)
									maxValue += 4;
								if(Main.rand.Next(200) < FinalFishingLevel)
									maxValue += 4;
								int count = Main.rand.Next(minValue, maxValue + 1);
								newItem.stack = count;
							}
							ItemLoader.CaughtFishStack(newItem);
							newItem.newAndShiny = true;
							Item.NewItem(te.Position.ToWorldCoordinates(0f, 0f), 48, 32, newItem.type, newItem.stack);
						}
						Projectile.Kill();
					}
				}
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			}
			else
			{
				//bool flag = false;
				//Vector2 vector2 = new Vector2((float)(Projectile.position.X + (double)Projectile.width * 0.5), (float)(Projectile.position.Y + (double)Projectile.height * 0.5));
				//float num1 = (float)(Main.player[Projectile.owner].position.X + (double)(Main.player[Projectile.owner].width / 2) - vector2.X);
				//float num2 = (float)(Main.player[Projectile.owner].position.Y + (double)(Main.player[Projectile.owner].height / 2) - vector2.Y);
				//Projectile.rotation = (float)Math.Atan2((double)num2, (double)num1) + 1.57f;
				//if(Math.Sqrt((double)num1 * (double)num1 + (double)num2 * (double)num2) > 900.0)
				//	Projectile.ai[0] = 1f;
				if(Projectile.wet)
				{
					Projectile.rotation = 0.0f;
					Projectile.velocity.X *= 0.9f;
					int index1 = (int)(Projectile.Center.X + (double)((Projectile.width / 2 + 8) * Projectile.direction)) / 16;
					int index2 = (int)(Projectile.Center.Y / 16.0);
					double num4 = Projectile.position.Y / 16.0;
					int index3 = (int)((Projectile.position.Y + (double)Projectile.height) / 16.0);
					if(Main.tile[index1, index2] == null)
						Main.tile[index1, index2] = new Tile();
					if(Main.tile[index1, index3] == null)
						Main.tile[index1, index3] = new Tile();
					if(Projectile.velocity.Y > 0.0)
					{
						Projectile.velocity.Y *= 0.5f;
					}
					int index4 = (int)(Projectile.Center.X / 16.0);
					int index5 = (int)(Projectile.Center.Y / 16.0);
					float num6 = (float)Projectile.position.Y + (float)Projectile.height;
					if(Main.tile[index4, index5 - 1] == null)
						Main.tile[index4, index5 - 1] = new Tile();
					if(Main.tile[index4, index5] == null)
						Main.tile[index4, index5] = new Tile();
					if(Main.tile[index4, index5 + 1] == null)
						Main.tile[index4, index5 + 1] = new Tile();
					if((int)Main.tile[index4, index5 - 1].LiquidAmount > 0)
						num6 = (float)(index5 * 16) - (float)(Main.tile[index4, index5 - 1].LiquidAmount / 16);
					else if((int)Main.tile[index4, index5].LiquidAmount > 0)
						num6 = (float)((index5 + 1) * 16) - (float)(Main.tile[index4, index5].LiquidAmount / 16);
					else if((int)Main.tile[index4, index5 + 1].LiquidAmount > 0)
						num6 = (float)((index5 + 2) * 16) - (float)(Main.tile[index4, index5 + 1].LiquidAmount / 16);
					if(Projectile.Center.Y > (double)num6)
					{
						Projectile.velocity.Y -= 0.1f;
						if(Projectile.velocity.Y < -8.0f)
							Projectile.velocity.Y = -8.0f;
						if(Projectile.Center.Y + Projectile.velocity.Y < (double)(num6 - 8))
							Projectile.velocity.Y = num6 - Projectile.Center.Y;
					}
					else
						Projectile.velocity.Y = (float)((double)num6 - Projectile.Center.Y);
					//if((double)Projectile.velocity.Y >= -0.01f && (double)Projectile.velocity.Y <= 0.01f)
					//	flag = true;
				}
				else
				{
					if(Projectile.velocity.Y == 0.0)
					{
						Projectile.velocity.X *= 0.95f;
					}
					Projectile.velocity.X *= 0.98f;
					Projectile.velocity.Y += 0.2f;
					if(Projectile.velocity.Y > 15.9f)
						Projectile.velocity.Y = 15.9f;
				}
				//if(Main.netMode != NetmodeID.MultiplayerClient)
				//{
				//	int num3 = te.GetFishingLevel(te.GetCurrentBait());
				//	if(num3 < 0 && num3 == -1)
				//		te.displayedFishingInfo = Language.GetTextValue("GameUI.FishingWarning");
				//}

				//if((double)Projectile.ai[0] != 0.0)
				//	flag = true;
				//if(!flag)
				//	return;
				//if((double)Projectile.ai[1] == 0.0 && Main.netMode != NetmodeID.MultiplayerClient)
				//{
				//	int num3 = Main.player[Projectile.owner].FishingLevel();
				//	if(num3 == -9000)
				//	{
				//		Projectile.localAI[1] += 5f;
				//		Projectile.localAI[1] += (float)Main.rand.Next(1, 3);
				//		if((double)Projectile.localAI[1] <= 660.0)
				//			return;
				//		Projectile.localAI[1] = 0.0f;
				//		Projectile.FishingCheck();
				//	}
				//	else
				//	{
				//		if(Main.rand.Next(300) < num3)
				//			Projectile.localAI[1] += (float)Main.rand.Next(1, 3);
				//		Projectile.localAI[1] += (float)(num3 / 30);
				//		Projectile.localAI[1] += (float)Main.rand.Next(1, 3);
				//		if(Main.rand.Next(60) == 0)
				//			Projectile.localAI[1] += 60f;
				//		if((double)Projectile.localAI[1] <= 660.0)
				//			return;
				//		Projectile.localAI[1] = 0.0f;
				//		Projectile.FishingCheck();
				//	}
				//}
				//else
				//{
				//	if((double)Projectile.ai[1] >= 0.0)
				//		return;
				//	if(Projectile.velocity.Y == 0.0 || Projectile.honeyWet && (double)Projectile.velocity.Y >= -0.01 && (double)Projectile.velocity.Y <= 0.01)
				//	{
				//		Projectile.velocity.Y = (float)((double)Main.rand.Next(100, 500) * 0.0149999996647239);
				//		Projectile.velocity.X = (float)((double)Main.rand.Next(-100, 101) * 0.0149999996647239);
				//		Projectile.wet = false;
				//		Projectile.lavaWet = false;
				//		Projectile.honeyWet = false;
				//	}
				//	Projectile.ai[1] += (float)Main.rand.Next(1, 5);
				//	if((double)Projectile.ai[1] < 0.0)
				//		return;
				//	Projectile.ai[1] = 0.0f;
				//	Projectile.localAI[1] = 0.0f;
				//	Projectile.netUpdate = true;
				//}
			}
		}

		public int FishingCheck(bool catchRealFish)
		{
			var te = (AutofisherTE)TileEntity.ByID[(int)Projectile.ai[1]];

			var baitItem = te.GetCurrentBait();
			int bobberTileX = (int)(Projectile.Center.X / 16.0);
			int bobberTileY = (int)(Projectile.Center.Y / 16.0);
			if((int)Main.tile[bobberTileX, bobberTileY].LiquidAmount < 0)
				++bobberTileY;
			bool lava = false;
			bool honey = false;
			int i1 = bobberTileX;
			int i2 = bobberTileX;
			while(i1 > 10 && (int)Main.tile[i1, bobberTileY].LiquidAmount > 0 && !WorldGen.SolidTile(i1, bobberTileY))
				--i1;
			while(i2 < Main.maxTilesX - 10 && (int)Main.tile[i2, bobberTileY].LiquidAmount > 0 && !WorldGen.SolidTile(i2, bobberTileY))
				++i2;
			int poolSize = 0;
			for(int i3 = i1; i3 <= i2; ++i3)
			{
				int j2 = bobberTileY;
				while((int)Main.tile[i3, j2].LiquidAmount > 0 && !WorldGen.SolidTile(i3, j2) && j2 < Main.maxTilesY - 10)
				{
					++poolSize;
					++j2;
					if(MiscUtils.hasLava(Main.tile[i3, j2]))
						lava = true;
					else if(MiscUtils.hasHoney(Main.tile[i3, j2]))
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

				//	Projectile.ai[1] = (float)(Main.rand.Next(-180, -60) - 100);

				//	Projectile.localAI[1] = (float)num2;

				//	Projectile.netUpdate = true;

				//}
				//else
				{
					const double maxPoolSize = 300;
					float worldSize = (float)(Main.maxTilesX / 4200);
					float Atmosphere = (float)((Projectile.position.Y / 16.0 - (60.0 + 10.0 * (double)(worldSize * worldSize))) / (Main.worldSurface / 6.0));
					if((double)Atmosphere < 0.25)
						Atmosphere = 0.25f;
					if((double)Atmosphere > 1.0)
						Atmosphere = 1f;
					int waterNeededToFish = (int)(maxPoolSize * (double)Atmosphere);
					float waterQuality = (float)poolSize / (float)waterNeededToFish;
					if((double)waterQuality < 1.0)
						fishingPower = (int)((double)fishingPower * (double)waterQuality);
					float invertedEffectivePoolSize = 1f - waterQuality;
					if(poolSize < waterNeededToFish && Main.netMode != NetmodeID.MultiplayerClient)
						te.DisplayedFishingInfo = Language.GetTextValue("GameUI.FullFishingPower", (object)fishingPower, (object)-Math.Round((double)invertedEffectivePoolSize * 100.0));
					int num9 = (fishingPower + 75) / 2;

					if(!catchRealFish)
						return -1;

					if(/*Main.player[Projectile.owner].wet || */Main.rand.Next(100) > num9)
						return -1;
					int heightLevel;
					bool isSurface = (double)Projectile.position.Y < Main.worldSurface * 0.5;
					if (isSurface)
					{
						heightLevel = 0;
					}
					else
					{
						bool isUnderground = (double)Projectile.position.Y < Main.worldSurface;
						if (isUnderground)
						{
							heightLevel = 1;
						}
						else
						{
							bool isCavern = (double)Projectile.position.Y < Main.rockLayer;
							if (isCavern)
							{
								heightLevel = 2;
							}
							else
							{
								bool isMolten = Projectile.position.Y < Main.maxTilesY - 300;
								if (isMolten)
								{
									//Lava Layer
									heightLevel = 3;
								}
								else
								{
									//Welcome to Hell
									heightLevel = 4;
								}
							}
						}
					}
					var zone = MiscUtils.GetZoneInLocation(te.Position.X, te.Position.Y);
					bool junk = false;
					// Vanilla Fishing Algorithm
					AutofisherCheck_RollDropLevels(fishingPower, out bool common, out bool uncommon, out bool rare, out bool veryrare, out bool legendary, out bool crate);
					int questFish = AutofisherCheck_ProbeForQuestFish();
					int caughtType = AutofisherCheck_RollItemDrop(fishingPower, poolSize, waterNeededToFish, zone, heightLevel, Projectile.position, questFish, common, uncommon, rare, veryrare, legendary, crate, honey, lava);
					AutofisherHooks.CatchFish(te, zone, baitItem, fishingPower, lava ? 1 : (honey ? 2 : 0), poolSize, heightLevel, ref caughtType, ref junk);

					if(caughtType <= 0 /*|| Crates.Contains(num10) */)
						return -1;

					//          if (Main.player[Projectile.owner].sonarPotion)
					//          {
					//            Item newItem = new Item();
					//int Type = num10;
					//int num19 = 0;
					//newItem.SetDefaults(Type, num19 != 0);
					//            Vector2 position = Projectile.position;
					//newItem.position = position;
					//            int stack = 1;
					//int num20 = 1;
					//int num21 = 0;
					//ItemText.NewText(newItem, stack, num20 != 0, num21 != 0);
					//          }

					//float num22 = (float)num2;

					//Projectile.ai[1] = (float)Main.rand.Next(-240, -90) - num22;

					//Projectile.localAI[1] = (float)num10;

					//Projectile.netUpdate = true;
					Projectile.ai[0] = caughtType;
					Projectile.netUpdate = true;
					return junk? -2 : caughtType;
				}
			}
			return -1;
		}

		static void AutofisherCheck_RollDropLevels(int fishingLevel, out bool common, out bool uncommon, out bool rare, out bool veryrare, out bool legendary, out bool crate)
		{
			int commonChance = 150 / fishingLevel;
			int uncommonChance = 300 / fishingLevel;
			int rareChance = 1050 / fishingLevel;
			int veryRareChance = 2250 / fishingLevel;
			int legendaryChance = 4500 / fishingLevel;
			const int crateChance = 10;
			bool flag = commonChance < 2;
			if (flag)
			{
				commonChance = 2;
			}
			bool flag2 = uncommonChance < 3;
			if (flag2)
			{
				uncommonChance = 3;
			}
			bool flag3 = rareChance < 4;
			if (flag3)
			{
				rareChance = 4;
			}
			bool flag4 = veryRareChance < 5;
			if (flag4)
			{
				veryRareChance = 5;
			}
			bool flag5 = legendaryChance < 6;
			if (flag5)
			{
				legendaryChance = 6;
			}
			common = false;
			uncommon = false;
			rare = false;
			veryrare = false;
			legendary = false;
			crate = false;
			bool flag6 = Main.rand.Next(commonChance) == 0;
			if (flag6)
			{
				common = true;
			}
			bool flag7 = Main.rand.Next(uncommonChance) == 0;
			if (flag7)
			{
				uncommon = true;
			}
			bool flag8 = Main.rand.Next(rareChance) == 0;
			if (flag8)
			{
				rare = true;
			}
			bool flag9 = Main.rand.Next(veryRareChance) == 0;
			if (flag9)
			{
				veryrare = true;
			}
			bool flag10 = Main.rand.Next(legendaryChance) == 0;
			if (flag10)
			{
				legendary = true;
			}
			bool flag11 = Main.rand.Next(100) < crateChance;
			if (flag11)
			{
				crate = true;
			}
		}

		static int AutofisherCheck_ProbeForQuestFish()
		{
			int questFish = Main.anglerQuestItemNetIDs[Main.anglerQuest];
			bool isAnglerActive = !NPC.AnyNPCs(369);
			if (isAnglerActive)
			{
				questFish = -1;
			}
			bool anglerQuestFinished = Main.anglerQuestFinished;
			if (anglerQuestFinished)
			{
				questFish = -1;
			}
			return questFish;
		}

		static int AutofisherCheck_RollItemDrop(int fishingLevel , int poolSize, int waterNeededToFish, Zone zone, int heightLevel, Vector2 position, int questFish, bool common, bool uncommon, bool rare, bool veryrare, bool legendary, bool crate, bool inLava, bool inHoney)
		{
			int caughtItem = -1;
			if (inLava)
			{
				bool flag5 = crate && Main.rand.Next(5) == 0;
				if (flag5)
				{
					caughtItem = (Main.hardMode ? 4878 : 4877);
				}
				else
				{
					bool flag6 = legendary && Main.hardMode && Main.rand.Next(3) == 0;
					if (flag6)
					{
						caughtItem = (int)Main.rand.NextFromList(new short[]
						{
							4819,
							4820,
							4872,
							2331
						});
					}
					else
					{
						bool flag7 = legendary && !Main.hardMode && Main.rand.Next(3) == 0;
						if (flag7)
						{
							caughtItem = (int)Main.rand.NextFromList(new short[]
							{
								4819,
								4820,
								4872
							});
						}
						else
						{
							if (veryrare)
							{
								caughtItem = 2312;
							}
							else
							{
								if (rare)
								{
									caughtItem = 2315;
								}
							}
						}
					}
				}
			}
			else
			{
				if (inHoney)
				{
					bool flag8 = rare || (uncommon && Main.rand.Next(2) == 0);
					if (flag8)
					{
						caughtItem = 2314;
					}
					else
					{
						bool flag9 = uncommon && questFish == 2451;
						if (flag9)
						{
							caughtItem = 2451;
						}
					}
				}
				else
				{
					bool flag10 = Main.rand.Next(50) > fishingLevel && Main.rand.Next(50) > fishingLevel && poolSize < waterNeededToFish;
					if (flag10)
					{
						caughtItem = Main.rand.Next(2337, 2340);
					}
					else
					{
						if (crate)
						{
							bool hardMode = Main.hardMode;
							bool flag11 = veryrare || legendary;
							if (flag11)
							{
								caughtItem = (hardMode ? 3981 : 2336);
							}
							else
							{
								bool flag12 = rare && zone.HasFlag(Zone.Dungeon);
								if (flag12)
								{
									caughtItem = (hardMode ? 3984 : 3205);
								}
								else
								{
									bool flag13 = rare && zone.HasFlag(Zone.Beach);
									if (flag13)
									{
										caughtItem = (hardMode ? 5003 : 5002);
									}
									else
									{
										bool flag14 = rare && zone.HasFlag(Zone.Corrupt);
										if (flag14)
										{
											caughtItem = (hardMode ? 3982 : 3203);
										}
										else
										{
											bool flag15 = rare && zone.HasFlag(Zone.Crimson);
											if (flag15)
											{
												caughtItem = (hardMode ? 3983 : 3204);
											}
											else
											{
												bool flag16 = rare && zone.HasFlag(Zone.Hallow);
												if (flag16)
												{
													caughtItem = (hardMode ? 3986 : 3207);
												}
												else
												{
													bool flag17 = rare && zone.HasFlag(Zone.Jungle);
													if (flag17)
													{
														caughtItem = (hardMode ? 3987 : 3208);
													}
													else
													{
														bool flag18 = rare && zone.HasFlag(Zone.Snow);
														if (flag18)
														{
															caughtItem = (hardMode ? 4406 : 4405);
														}
														else
														{
															bool flag19 = rare && zone.HasFlag(Zone.Desert);
															if (flag19)
															{
																caughtItem = (hardMode ? 4408 : 4407);
															}
															else
															{
																bool flag20 = rare && heightLevel == 0;
																if (flag20)
																{
																	caughtItem = (hardMode ? 3985 : 3206);
																}
																else
																{
																	if (uncommon)
																	{
																		caughtItem = (hardMode ? 3980 : 2335);
																	}
																	else
																	{
																		caughtItem = (hardMode ? 3979 : 2334);
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
						else
						{
							bool flag21 = !NPC.combatBookWasUsed && Main.bloodMoon && legendary && Main.rand.Next(3) == 0;
							if (flag21)
							{
								caughtItem = 4382;
							}
							else
							{
								bool flag22 = legendary && Main.rand.Next(5) == 0;
								if (flag22)
								{
									caughtItem = 2423;
								}
								else
								{
									bool flag23 = legendary && Main.rand.Next(5) == 0;
									if (flag23)
									{
										caughtItem = 3225;
									}
									else
									{
										bool flag24 = legendary && Main.rand.Next(10) == 0;
										if (flag24)
										{
											caughtItem = 2420;
										}
										else
										{
											bool flag25 = !legendary && !veryrare && uncommon && Main.rand.Next(5) == 0;
											if (flag25)
											{
												caughtItem = 3196;
											}
											else
											{
												bool flag = zone.HasFlag(Zone.Desert);
												bool zoneDungeon = zone.HasFlag(Zone.Dungeon);
												if (zoneDungeon)
												{
													flag = false;
													bool flag26 = caughtItem == 0 && veryrare && Main.rand.Next(7) == 0;
													if (flag26)
													{
														caughtItem = 3000;
													}
												}
												else
												{
													bool flag2 = zone.HasFlag(Zone.Corrupt);
													bool flag3 = zone.HasFlag(Zone.Crimson);
													bool flag27 = flag2 && flag3;
													if (flag27)
													{
														bool flag28 = Main.rand.Next(2) == 0;
														if (flag28)
														{
															flag3 = false;
														}
														else
														{
															flag2 = false;
														}
													}
													bool flag29 = flag2;
													if (flag29)
													{
														bool flag30 = legendary && Main.hardMode && zone.HasFlag(Zone.Snow) && heightLevel == 3 && Main.rand.Next(3) != 0;
														if (flag30)
														{
															caughtItem = 2429;
														}
														else
														{
															bool flag31 = legendary && Main.hardMode && Main.rand.Next(2) == 0;
															if (flag31)
															{
																caughtItem = 3210;
															}
															else
															{
																bool rare2 = rare;
																if (rare2)
																{
																	caughtItem = 2330;
																}
																else
																{
																	bool flag32 = uncommon && questFish == 2454;
																	if (flag32)
																	{
																		caughtItem = 2454;
																	}
																	else
																	{
																		bool flag33 = uncommon && questFish == 2485;
																		if (flag33)
																		{
																			caughtItem = 2485;
																		}
																		else
																		{
																			bool flag34 = uncommon && questFish == 2457;
																			if (flag34)
																			{
																				caughtItem = 2457;
																			}
																			else
																			{
																				bool uncommon2 = uncommon;
																				if (uncommon2)
																				{
																					caughtItem = 2318;
																				}
																			}
																		}
																	}
																}
															}
														}
													}
													else
													{
														bool flag35 = flag3;
														if (flag35)
														{
															bool flag36 = legendary && Main.hardMode && zone.HasFlag(Zone.Snow) && heightLevel == 3 && Main.rand.Next(3) != 0;
															if (flag36)
															{
																caughtItem = 2429;
															}
															else
															{
																bool flag37 = legendary && Main.hardMode && Main.rand.Next(2) == 0;
																if (flag37)
																{
																	caughtItem = 3211;
																}
																else
																{
																	bool flag38 = uncommon && questFish == 2477;
																	if (flag38)
																	{
																		caughtItem = 2477;
																	}
																	else
																	{
																		bool flag39 = uncommon && questFish == 2463;
																		if (flag39)
																		{
																			caughtItem = 2463;
																		}
																		else
																		{
																			if (uncommon)
																			{
																				caughtItem = 2319;
																			}
																			else
																			{
																				if (common)
																				{
																					caughtItem = 2305;
																				}
																			}
																		}
																	}
																}
															}
														}
														else
														{
															bool zoneHallow = zone.HasFlag(Zone.Hallow);
															if (zoneHallow)
															{
																bool flag40 = legendary && Main.hardMode && zone.HasFlag(Zone.Snow) && heightLevel == 3 && Main.rand.Next(3) != 0;
																if (flag40)
																{
																	caughtItem = 2429;
																}
																else
																{
																	bool flag41 = legendary && Main.hardMode && Main.rand.Next(2) == 0;
																	if (flag41)
																	{
																		caughtItem = 3209;
																	}
																	else
																	{
																		bool flag42 = heightLevel > 1 && veryrare;
																		if (flag42)
																		{
																			caughtItem = 2317;
																		}
																		else
																		{
																			bool flag43 = heightLevel > 1 && uncommon && questFish == 2465;
																			if (flag43)
																			{
																				caughtItem = 2465;
																			}
																			else
																			{
																				bool flag44 = heightLevel < 2 && uncommon && questFish == 2468;
																				if (flag44)
																				{
																					caughtItem = 2468;
																				}
																				else
																				{
																					bool rare3 = rare;
																					if (rare3)
																					{
																						caughtItem = 2310;
																					}
																					else
																					{
																						bool flag45 = uncommon && questFish == 2471;
																						if (flag45)
																						{
																							caughtItem = 2471;
																						}
																						else
																						{
																							bool uncommon4 = uncommon;
																							if (uncommon4)
																							{
																								caughtItem = 2307;
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
													bool flag46 = caughtItem == 0 && zone.HasFlag(Zone.Snow);
													if (flag46)
													{
														bool flag47 = heightLevel < 2 && uncommon && questFish == 2467;
														if (flag47)
														{
															caughtItem = 2467;
														}
														else
														{
															bool flag48 = heightLevel == 1 && uncommon && questFish == 2470;
															if (flag48)
															{
																caughtItem = 2470;
															}
															else
															{
																bool flag49 = heightLevel >= 2 && uncommon && questFish == 2484;
																if (flag49)
																{
																	caughtItem = 2484;
																}
																else
																{
																	bool flag50 = heightLevel > 1 && uncommon && questFish == 2466;
																	if (flag50)
																	{
																		caughtItem = 2466;
																	}
																	else
																	{
																		bool flag51 = (common && Main.rand.Next(12) == 0) || (uncommon && Main.rand.Next(6) == 0);
																		if (flag51)
																		{
																			caughtItem = 3197;
																		}
																		else
																		{
																			if (uncommon)
																			{
																				caughtItem = 2306;
																			}
																			else
																			{
																				if (common)
																				{
																					caughtItem = 2299;
																				}
																				else
																				{
																					bool flag52 = heightLevel > 1 && Main.rand.Next(3) == 0;
																					if (flag52)
																					{
																						caughtItem = 2309;
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
													bool flag53 = caughtItem == 0 && zone.HasFlag(Zone.Jungle);
													if (flag53)
													{
														bool flag54 = heightLevel == 1 && uncommon && questFish == 2452;
														if (flag54)
														{
															caughtItem = 2452;
														}
														else
														{
															bool flag55 = heightLevel == 1 && uncommon && questFish == 2483;
															if (flag55)
															{
																caughtItem = 2483;
															}
															else
															{
																bool flag56 = heightLevel == 1 && uncommon && questFish == 2488;
																if (flag56)
																{
																	caughtItem = 2488;
																}
																else
																{
																	bool flag57 = heightLevel >= 1 && uncommon && questFish == 2486;
																	if (flag57)
																	{
																		caughtItem = 2486;
																	}
																	else
																	{
																		bool flag58 = heightLevel > 1 && uncommon;
																		if (flag58)
																		{
																			caughtItem = 2311;
																		}
																		else
																		{
																			if (uncommon)
																			{
																				caughtItem = 2313;
																			}
																			else
																			{
																				if (common)
																				{
																					caughtItem = 2302;
																				}
																			}
																		}
																	}
																}
															}
														}
													}
													bool flag59 = caughtItem == 0 && zone.HasFlag(Zone.Shroom) && uncommon && questFish == 2475;
													if (flag59)
													{
														caughtItem = 2475;
													}
												}
												bool flag60 = caughtItem == 0;
												if (flag60)
												{
													bool flag61 = heightLevel <= 1 && (position.X < 380 || position.X > Main.maxTilesX - 380) && poolSize > 1000;
													if (flag61)
													{
														bool flag62 = veryrare && Main.rand.Next(2) == 0;
														if (flag62)
														{
															caughtItem = 2341;
														}
														else
														{
															if (veryrare)
															{
																caughtItem = 2342;
															}
															else
															{
																bool flag63 = rare && Main.rand.Next(5) == 0;
																if (flag63)
																{
																	caughtItem = 2438;
																}
																else
																{
																	bool flag64 = rare && Main.rand.Next(3) == 0;
																	if (flag64)
																	{
																		caughtItem = 2332;
																	}
																	else
																	{
																		bool flag65 = uncommon && questFish == 2480;
																		if (flag65)
																		{
																			caughtItem = 2480;
																		}
																		else
																		{
																			bool flag66 = uncommon && questFish == 2481;
																			if (flag66)
																			{
																				caughtItem = 2481;
																			}
																			else
																			{
																				if (uncommon)
																				{
																					caughtItem = 2316;
																				}
																				else
																				{
																					bool flag67 = common && Main.rand.Next(2) == 0;
																					if (flag67)
																					{
																						caughtItem = 2301;
																					}
																					else
																					{
																						if (common)
																						{
																							caughtItem = 2300;
																						}
																						else
																						{
																							caughtItem = 2297;
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
													else
													{
														bool flag68 = flag;
														if (flag68)
														{
															bool flag69 = uncommon && questFish == 4393;
															if (flag69)
															{
																caughtItem = 4393;
															}
															else
															{
																bool flag70 = uncommon && questFish == 4394;
																if (flag70)
																{
																	caughtItem = 4394;
																}
																else
																{
																	if (uncommon)
																	{
																		caughtItem = 4410;
																	}
																	else
																	{
																		bool flag71 = Main.rand.Next(3) == 0;
																		if (flag71)
																		{
																			caughtItem = 4402;
																		}
																		else
																		{
																			caughtItem = 4401;
																		}
																	}
																}
															}
														}
													}
												}
												bool flag72 = caughtItem != 0;
												if (!flag72)
												{
													bool flag73 = heightLevel < 2 && uncommon && questFish == 2461;
													if (flag73)
													{
														caughtItem = 2461;
													}
													else
													{
														bool flag74 = heightLevel == 0 && uncommon && questFish == 2453;
														if (flag74)
														{
															caughtItem = 2453;
														}
														else
														{
															bool flag75 = heightLevel == 0 && uncommon && questFish == 2473;
															if (flag75)
															{
																caughtItem = 2473;
															}
															else
															{
																bool flag76 = heightLevel == 0 && uncommon && questFish == 2476;
																if (flag76)
																{
																	caughtItem = 2476;
																}
																else
																{
																	bool flag77 = heightLevel < 2 && uncommon && questFish == 2458;
																	if (flag77)
																	{
																		caughtItem = 2458;
																	}
																	else
																	{
																		bool flag78 = heightLevel < 2 && uncommon && questFish == 2459;
																		if (flag78)
																		{
																			caughtItem = 2459;
																		}
																		else
																		{
																			bool flag79 = heightLevel == 0 && uncommon;
																			if (flag79)
																			{
																				caughtItem = 2304;
																			}
																			else
																			{
																				bool flag80 = heightLevel > 0 && heightLevel < 3 && uncommon && questFish == 2455;
																				if (flag80)
																				{
																					caughtItem = 2455;
																				}
																				else
																				{
																					bool flag81 = heightLevel == 1 && uncommon && questFish == 2479;
																					if (flag81)
																					{
																						caughtItem = 2479;
																					}
																					else
																					{
																						bool flag82 = heightLevel == 1 && uncommon && questFish == 2456;
																						if (flag82)
																						{
																							caughtItem = 2456;
																						}
																						else
																						{
																							bool flag83 = heightLevel == 1 && uncommon && questFish == 2474;
																							if (flag83)
																							{
																								caughtItem = 2474;
																							}
																							else
																							{
																								bool flag84 = heightLevel > 1 && rare && Main.rand.Next(5) == 0;
																								if (flag84)
																								{
																									bool flag85 = Main.hardMode && Main.rand.Next(2) == 0;
																									if (flag85)
																									{
																										caughtItem = 2437;
																									}
																									else
																									{
																										caughtItem = 2436;
																									}
																								}
																								else
																								{
																									bool flag86 = heightLevel > 1 && legendary && Main.rand.Next(3) != 0;
																									if (flag86)
																									{
																										caughtItem = 2308;
																									}
																									else
																									{
																										bool flag87 = heightLevel > 1 && veryrare && Main.rand.Next(2) == 0;
																										if (flag87)
																										{
																											caughtItem = 2320;
																										}
																										else
																										{
																											bool flag88 = heightLevel > 1 && rare;
																											if (flag88)
																											{
																												caughtItem = 2321;
																											}
																											else
																											{
																												bool flag89 = heightLevel > 1 && uncommon && questFish == 2478;
																												if (flag89)
																												{
																													caughtItem = 2478;
																												}
																												else
																												{
																													bool flag90 = heightLevel > 1 && uncommon && questFish == 2450;
																													if (flag90)
																													{
																														caughtItem = 2450;
																													}
																													else
																													{
																														bool flag91 = heightLevel > 1 && uncommon && questFish == 2464;
																														if (flag91)
																														{
																															caughtItem = 2464;
																														}
																														else
																														{
																															bool flag92 = heightLevel > 1 && uncommon && questFish == 2469;
																															if (flag92)
																															{
																																caughtItem = 2469;
																															}
																															else
																															{
																																bool flag93 = heightLevel > 2 && uncommon && questFish == 2462;
																																if (flag93)
																																{
																																	caughtItem = 2462;
																																}
																																else
																																{
																																	bool flag94 = heightLevel > 2 && uncommon && questFish == 2482;
																																	if (flag94)
																																	{
																																		caughtItem = 2482;
																																	}
																																	else
																																	{
																																		bool flag95 = heightLevel > 2 && uncommon && questFish == 2472;
																																		if (flag95)
																																		{
																																			caughtItem = 2472;
																																		}
																																		else
																																		{
																																			bool flag96 = heightLevel > 2 && uncommon && questFish == 2460;
																																			if (flag96)
																																			{
																																				caughtItem = 2460;
																																			}
																																			else
																																			{
																																				bool flag97 = heightLevel > 1 && uncommon && Main.rand.Next(4) != 0;
																																				if (flag97)
																																				{
																																					caughtItem = 2303;
																																				}
																																				else
																																				{
																																					bool flag98 = heightLevel > 1 && (uncommon || common || Main.rand.Next(4) == 0);
																																					if (flag98)
																																					{
																																						bool flag99 = Main.rand.Next(4) == 0;
																																						if (flag99)
																																						{
																																							caughtItem = 2303;
																																						}
																																						else
																																						{
																																							caughtItem = 2309;
																																						}
																																					}
																																					else
																																					{
																																						bool flag100 = uncommon && questFish == 2487;
																																						if (flag100)
																																						{
																																							caughtItem = 2487;
																																						}
																																						else
																																						{
																																							bool flag101 = poolSize > 1000 && common;
																																							if (flag101)
																																							{
																																								caughtItem = 2298;
																																							}
																																							else
																																							{
																																								caughtItem = 2290;
																																							}
																																						}
																																					}
																																				}
																																			}
																																		}
																																	}
																																}
																															}
																														}
																													}
																												}
																											}
																										}
																									}
																								}
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return caughtItem;	
		}

		public override bool PreDrawExtras()
		{
			if(!TileEntity.ByID.ContainsKey((int)Projectile.ai[1]))
				return false;

			var te = TileEntity.ByID[(int)Projectile.ai[1]] as AutofisherTE;
			if(te == null)
				return false;
			//Lighting.AddLight(Projectile.Center, 0.7f, 0.9f, 0.6f);
			//if(Projectile.bobber && Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].holdStyle > 0)
			{
				//float pPosX = player.MountedCenter.X;
				//float pPosY = player.MountedCenter.Y;
				//pPosY += Main.player[Projectile.owner].gfxOffY;
				//int type = Main.player[Projectile.owner].inventory[Main.player[Projectile.owner].selectedItem].type;
				//float gravDir = Main.player[Projectile.owner].gravDir;

				//if(type == Mod.ItemType("GooFishingPole"))
				//{
				//	pPosX += (float)(50 * Main.player[Projectile.owner].direction);
				//	if(Main.player[Projectile.owner].direction < 0)
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


				//value = Main.player[Projectile.owner].RotatedRelativePoint(value + new Vector2(8f), true) - new Vector2(8f);
				float projPosX = Projectile.position.X + (float)Projectile.width * 0.5f - value.X;
				float projPosY = Projectile.position.Y + (float)Projectile.height * 0.5f - value.Y;
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
					projPosX = Projectile.position.X + (float)Projectile.width * 0.5f - value.X;
					projPosY = Projectile.position.Y + (float)Projectile.height * 0.5f - value.Y;
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
						projPosX = Projectile.position.X + (float)Projectile.width * 0.5f - value.X;
						projPosY = Projectile.position.Y + (float)Projectile.height * 0.1f - value.Y;
						if(num3 > 12f)
						{
							float num4 = 0.3f;
							float num5 = Math.Abs(Projectile.velocity.X) + Math.Abs(Projectile.velocity.Y);
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
							num5 = 1f - Projectile.localAI[0] / 100f;
							num4 *= num5;
							if(projPosY > 0f)
							{
								projPosY *= 1f + num4;
								projPosX *= 1f - num4;
							}
							else
							{
								num5 = Math.Abs(Projectile.velocity.X) / 3f;
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
						Main.spriteBatch.Draw(TextureAssets.FishingLine.Value, new Vector2(value.X - Main.screenPosition.X + (float)TextureAssets.FishingLine.Width() * 0.5f, value.Y - Main.screenPosition.Y + (float)TextureAssets.FishingLine.Height() * 0.5f), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.FishingLine.Width(), (int)num)), color2, rotation2, new Vector2((float)TextureAssets.FishingLine.Width() * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			return false;
		}
	}
}
