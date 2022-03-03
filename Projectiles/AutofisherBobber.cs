using GoldensMisc.Tiles;
using static GoldensMisc.MiscUtils;
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
using Terraria.Chat;

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
			Projectile.timeLeft = 60;
			if((double)Projectile.ai[0] != 0.0)
			{
				
				if (Projectile.frameCounter == 0)
				{
					Projectile.frameCounter = 1;
					AutofisherReduceRemainingChumsInPool();
				}
				Projectile.tileCollide = false;
				const double productDivisor = 15.8999996185303;
				const double ten = 10;
				Vector2 vector2 = new Vector2((float)(Projectile.position.X + (double)Projectile.width * 0.5), (float)(Projectile.position.Y + (double)Projectile.height * 0.5));
				float num5 = (te.Position.X + 1.5f) * 16f - vector2.X;
				float num6 = (te.Position.Y + 1) * 16f - vector2.Y; 
				float num7 = (float)Math.Sqrt((double)num5 * (double)num5 + (double)num6 * (double)num6);
				if((double)num7 > 3000.0)
                {
					Projectile.Kill();
				}
					
				double num8 = (double)num7;
				float num9 = (float)(productDivisor / num8);
				float num10 = num5 * num9;
				float num11 = num6 * num9;
				Projectile.velocity.X = (float)((Projectile.velocity.X * (double)(ten - 1) + (double)num10) / ten);
				Projectile.velocity.Y = (float)((Projectile.velocity.Y * (double)(ten - 1) + (double)num11) / ten);
				if(Main.netMode != NetmodeID.MultiplayerClient)
				{
					Rectangle rectangle1 = new Rectangle((int)Projectile.position.X, (int)Projectile.position.Y, Projectile.width, Projectile.height);
					Rectangle rectangle2 = new Rectangle(te.Position.X * 16, te.Position.Y * 16, 48, 32);
					if(((Rectangle)rectangle1).Intersects(rectangle2))
					{
						if ((double)Projectile.ai[0] > 0.0)
						{
							int Type = (int)Projectile.ai[0];
							Item newItem = new Item();
							newItem.SetDefaults(Type, true);
							if (Type == 3196)
							{
								int FinalFishingLevel = te.GetFishingLevel(te.GetCurrentBait());
								int minValue = (FinalFishingLevel / 20 + 3) / 2;
								int maxValue = (FinalFishingLevel / 10 + 6) / 2;
								if (Main.rand.Next(50) < FinalFishingLevel)
									++maxValue;
								if (Main.rand.Next(100) < FinalFishingLevel)
									++maxValue;
								if (Main.rand.Next(150) < FinalFishingLevel)
									++maxValue;
								if (Main.rand.Next(200) < FinalFishingLevel)
									++maxValue;
								int count = Main.rand.Next(minValue, maxValue + 1);
								newItem.stack = count;
							}
							if (Type == 3197)
							{
								int FinalFishingLevel = te.GetFishingLevel(te.GetCurrentBait());
								int minValue = (FinalFishingLevel / 4 + 15) / 2;
								int maxValue = (FinalFishingLevel / 2 + 30) / 2;
								if (Main.rand.Next(50) < FinalFishingLevel)
									maxValue += 4;
								if (Main.rand.Next(100) < FinalFishingLevel)
									maxValue += 4;
								if (Main.rand.Next(150) < FinalFishingLevel)
									maxValue += 4;
								if (Main.rand.Next(200) < FinalFishingLevel)
									maxValue += 4;
								int count = Main.rand.Next(minValue, maxValue + 1);
								newItem.stack = count;
							}
							ItemLoader.CaughtFishStack(newItem);
							newItem.newAndShiny = true;
							Item.NewItem(new EntitySource_TileEntity(te), te.Position.ToWorldCoordinates(0f, 0f), 48, 32, newItem.type, newItem.stack);
						}
						else
						{ 
							if (Projectile.ai[0] < 0)
                            {
								int type = (int)(Projectile.ai[0] * -1);
								Point p = new((int)Projectile.position.X, (int)Projectile.position.Y);
								if (Main.netMode == NetmodeID.MultiplayerClient)
								{
									NetMessage.SendData(MessageID.FishOutNPC, -1, -1, null, p.X / 16, (float)(p.Y / 16), (float)type, 0f, 0, 0, 0);
								}
								else
								{
									int nPCi = NPC.NewNPC(new EntitySource_TileEntity(te),p.X, p.Y, type);
									if (Main.npc[nPCi].boss)
									{
										if (Main.netMode == NetmodeID.SinglePlayer)
										{
											Main.NewText(Language.GetTextValue("Announcement.HasAwoken", Main.npc[nPCi].GetTypeNetName()), 175, 75, byte.MaxValue);
										}
										else
										{
											if (Main.netMode == NetmodeID.Server)
											{
												ChatHelper.BroadcastChatMessage(NetworkText.FromKey("Announcement.HasAwoken", new object[] {
												Main.npc[nPCi].GetTypeNetName()}), new Color(175, 75, 255), -1);
											}
										}
									}
								}
                            }
								
						}
						Projectile.Kill();
					}
				}
				Projectile.rotation = (float)Math.Atan2((double)Projectile.velocity.Y, (double)Projectile.velocity.X) + 1.57f;
			}
			else
			{

				if(Projectile.wet)
				{
					Projectile.rotation = 0.0f;
					Projectile.velocity.X *= 0.9f;
					int index1 = (int)(Projectile.Center.X + ((Projectile.width / 2 + 8) * Projectile.direction)) / 16;
					int index2 = (int)(Projectile.Center.Y / 16f);
					int index3 = (int)((Projectile.position.Y + Projectile.height) / 16f);
					if(Projectile.velocity.Y > 0.0f)
					{
						Projectile.velocity.Y *= 0.5f;
					}
					int BobberCenterTileX = (int)(Projectile.Center.X / 16f);
					int BobberCenterTileY = (int)(Projectile.Center.Y / 16f);
					float waterLine = AutoFisherBobber_GetWaterLine(BobberCenterTileX, BobberCenterTileY);
					if(Projectile.Center.Y > waterLine)
					{
						//	GoldensMisc.Log((Projectile.Center.X / 16f));
						Projectile.velocity.Y -= 0.1f;
						if(Projectile.velocity.Y < -8.0f)
							Projectile.velocity.Y = -8.0f;
						if(Projectile.Center.Y + Projectile.velocity.Y < waterLine)
							Projectile.velocity.Y = waterLine - Projectile.Center.Y;
					}
					else
						Projectile.velocity.Y = waterLine - Projectile.Center.Y;
				}
				else
				{
					if (Projectile.velocity.Y == 0f)
					{
						Projectile.velocity.X *= 0.95f;
					}
					Projectile.velocity.X *= 0.98f;
					Projectile.velocity.Y += 0.2f;
					if (Projectile.velocity.Y > 15.9f)
						Projectile.velocity.Y = 15.9f;
				}
			}
		}

		public int FishingCheck(bool catchRealFish)
		{
			var te = (AutofisherTE)TileEntity.ByID[(int)Projectile.ai[1]];
			AutoFishingAttempt autoFisher = new();
			autoFisher.baitItem = te.GetCurrentBait();
			int bobberTileX = (int)(Projectile.Center.X / 16f);
			int bobberTileY = (int)(Projectile.Center.Y / 16f);
			autoFisher.position.X = bobberTileX;
			autoFisher.position.Y = bobberTileY;
			//GoldensMisc.Log(" X:" + bobberTileX + " Y:" + bobberTileY );
			GetAutoFishingPondState(autoFisher.position.X, autoFisher.position.Y, out autoFisher.inLava, out autoFisher.inHoney, out autoFisher.poolSize, out autoFisher.chumsInWater);
			if(autoFisher.poolSize < 75)
			{
				if(Main.netMode != NetmodeID.MultiplayerClient)
					te.DisplayedFishingInfo = Language.GetTextValue("GameUI.NotEnoughWater");
			}
			else
			{
				autoFisher.fishingPower = te.GetFishingLevel(autoFisher.baitItem);
				if(autoFisher.fishingPower == 0)
					return -1;
				if (autoFisher.chumsInWater == 0)
				{ 
					//do nothing
				}
				else if(autoFisher.chumsInWater == 1 )
				{
					autoFisher.fishingPower += 11;
				}
				else if (autoFisher.chumsInWater == 2)
				{
					autoFisher.fishingPower += 17;
				}
				else if (autoFisher.chumsInWater > 2)
				{
					autoFisher.fishingPower += 20;
				}
				if (Main.netMode != NetmodeID.MultiplayerClient)
					te.DisplayedFishingInfo = Language.GetTextValue("GameUI.FishingPower", (object)autoFisher.fishingPower);
				
				if (autoFisher.baitItem.type == ItemID.TruffleWorm)
				{
					te.DisplayedFishingInfo = Language.GetTextValue("GameUI.FishingWarning");
					if (!((autoFisher.position.X < 380 || autoFisher.position.X > Main.maxTilesX - 380) && autoFisher.poolSize > 1000 && !NPC.AnyNPCs(NPCID.DukeFishron)))
                    {
						return -1;
					}
						
				}
				const double maxPoolSize = 300;
				float worldSize = (float)(Main.maxTilesX / 4200);
				autoFisher.atmosphere = (float)((Projectile.position.Y / 16.0 - (60.0 + 10.0 * (double)(worldSize * worldSize))) / (Main.worldSurface / 6.0));
				if((double)autoFisher.atmosphere < 0.25)
					autoFisher.atmosphere = 0.25f;
				if((double)autoFisher.atmosphere > 1.0)
					autoFisher.atmosphere = 1f;
				autoFisher.waterNeededToFish = (int)(maxPoolSize * (double)autoFisher.atmosphere);
				autoFisher.waterQuality = (float)autoFisher.poolSize / (float)autoFisher.waterNeededToFish;
				if((double)autoFisher.waterQuality < 1.0)
					autoFisher.fishingPower = (int)((double)autoFisher.fishingPower * (double)autoFisher.waterQuality);
				double invertedWaterQuality = 1f - autoFisher.waterQuality;
				if(autoFisher.poolSize < autoFisher.waterNeededToFish && Main.netMode != NetmodeID.MultiplayerClient)
					te.DisplayedFishingInfo = Language.GetTextValue("GameUI.FullFishingPower", (object)autoFisher.fishingPower, (object)-Math.Round(invertedWaterQuality * 100.0));
				int num9 = (autoFisher.fishingPower + 75) / 2;

				if(!catchRealFish)
					return -1;

				if(Main.rand.Next(3) > num9)
					return -1;
				bool isSky = autoFisher.position.Y < Main.worldSurface * 0.5;
				if (isSky)
				{
					autoFisher.heightLevel = 0;
				}
				else
				{
					bool isSurface = autoFisher.position.Y < Main.worldSurface;
					if (isSurface)
					{
						autoFisher.heightLevel = 1;
					}
					else
					{
						bool isUnderground = autoFisher.position.Y < Main.rockLayer;
						if (isUnderground)
						{
							autoFisher.heightLevel = 2;
						}
						else
						{
							bool isCavern = autoFisher.position.Y < Main.maxTilesY - 300;
							if (isCavern)
							{
								autoFisher.heightLevel = 3;
							}
							else
							{
								//Welcome to Hell
								autoFisher.heightLevel = 4;
							}
						}
					}
				}
				autoFisher.zone = MiscUtils.GetZoneInLocation(te.Position.X, te.Position.Y);
				autoFisher.isJunk = false;
				// Vanilla Fishing Algorithm
				AutofisherCheck_RollDropLevels(ref autoFisher);
				AutofisherCheck_ProbeForQuestFish(ref autoFisher);
				AutofisherCheck_RollEnemySpawns(ref autoFisher);
				AutofisherCheck_RollItemDrop(ref autoFisher);
				AutofisherHooks.CatchFish(te, autoFisher.zone, autoFisher.baitItem, autoFisher.fishingPower, autoFisher.inLava ? 1 : (autoFisher.inHoney ? 2 : 0), autoFisher.poolSize, autoFisher.heightLevel, ref autoFisher.caughtItem, ref autoFisher.isJunk);
				AutofisherHooks.CatchEnemy(te, autoFisher.zone, autoFisher.baitItem, autoFisher.fishingPower, autoFisher.inLava ? 1 : (autoFisher.inHoney ? 2 : 0), autoFisher.poolSize, autoFisher.heightLevel, ref autoFisher.caughtEnemy);
				//GoldensMisc.Log(autoFisher.caughtItem);
				if (autoFisher.caughtItem <= 0 && autoFisher.caughtEnemy <= 0)
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
				GoldensMisc.Log(autoFisher.caughtItem + " " + autoFisher.caughtEnemy);
				if (autoFisher.caughtEnemy == 0)
				{
					Projectile.ai[0] = autoFisher.caughtItem;
					Projectile.netUpdate = true;
					return autoFisher.isJunk ? -2 : autoFisher.caughtItem;
				}
				else
                {
					Projectile.ai[0] = (autoFisher.caughtEnemy * -1);
					Projectile.netUpdate = true;
					return autoFisher.caughtEnemy;
				}
				
			}
			return -1;
		}

		static void AutofisherCheck_RollDropLevels(ref AutoFishingAttempt autoFisher)
		{
			int commonChance = 150 / autoFisher.fishingPower;
			int uncommonChance = 300 / autoFisher.fishingPower;
			int rareChance = 1050 / autoFisher.fishingPower;
			int veryRareChance = 2250 / autoFisher.fishingPower;
			int legendaryChance = 4500 / autoFisher.fishingPower;
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
			autoFisher.common = false;
			autoFisher.uncommon = false;
			autoFisher.rare = false;
			autoFisher.veryrare = false;
			autoFisher.legendary = false;
			autoFisher.crate = false;
			bool flag6 = Main.rand.Next(commonChance) == 0;
			if (flag6)
			{
				autoFisher.common = true;
			}
			bool flag7 = Main.rand.Next(uncommonChance) == 0;
			if (flag7)
			{
				autoFisher.uncommon = true;
			}
			bool flag8 = Main.rand.Next(rareChance) == 0;
			if (flag8)
			{
				autoFisher.rare = true;
			}
			bool flag9 = Main.rand.Next(veryRareChance) == 0;
			if (flag9)
			{
				autoFisher.veryrare = true;
			}
			bool flag10 = Main.rand.Next(legendaryChance) == 0;
			if (flag10)
			{
				autoFisher.legendary = true;
			}
			bool flag11 = Main.rand.Next(100) < crateChance;
			if (flag11)
			{
				autoFisher.crate = true;
			}
		}

		static void AutofisherCheck_ProbeForQuestFish(ref AutoFishingAttempt autoFisher)
		{
			autoFisher.questFish = Main.anglerQuestItemNetIDs[Main.anglerQuest];
			bool isAnglerActive = !NPC.AnyNPCs(369);
			if (isAnglerActive)
			{
				autoFisher.questFish = -1;
			}
			bool anglerQuestFinished = Main.anglerQuestFinished;
			if (anglerQuestFinished)
			{
				autoFisher.questFish = -1;
			}
		}

		static void AutofisherCheck_RollEnemySpawns(ref AutoFishingAttempt autoFisher)
		{
			if (!(autoFisher.inLava || autoFisher.inHoney))
			{
				if (Main.bloodMoon && !Main.dayTime)
				{
					int maxValue = 6;
					if (autoFisher.bobberType == 760)
					{
						maxValue = 3;
					}
					if (Main.rand.Next(maxValue) == 0)
					{
						if (Main.hardMode)
						{
							autoFisher.caughtEnemy = (int)Utils.SelectRandom<short>(Main.rand, new short[]
							{
							NPCID.GoblinShark,
							NPCID.BloodEelHead,
							NPCID.ZombieMerman,
							NPCID.EyeballFlyingFish
							});
							if (Main.rand.Next(10) == 0)
							{
								autoFisher.caughtEnemy = NPCID.BloodNautilus;
							}
						}
						else
						{
							autoFisher.caughtEnemy = (int)Utils.SelectRandom<short>(Main.rand, new short[]
							{
							NPCID.EyeballFlyingFish,
							NPCID.ZombieMerman
							});
						}
					}
				}
				if (autoFisher.baitItem.type == ItemID.TruffleWorm && (autoFisher.position.X < 380 || autoFisher.position.X > Main.maxTilesX - 380) && autoFisher.poolSize > 1000 && !NPC.AnyNPCs(NPCID.DukeFishron))
				{
					autoFisher.caughtEnemy = NPCID.DukeFishron;
				}
			}
			
		}
		static void AutofisherCheck_RollItemDrop(ref AutoFishingAttempt autoFisher)
		{
			if (!(autoFisher.caughtEnemy > 0))
			{
				if (autoFisher.inLava)
				{
					bool flag5 = autoFisher.crate && Main.rand.Next(5) == 0;
					if (flag5)
					{
						autoFisher.caughtItem = (Main.hardMode ? 4878 : 4877);
					}
					else
					{
						bool flag6 = autoFisher.legendary && Main.hardMode && Main.rand.Next(3) == 0;
						if (flag6)
						{
							autoFisher.caughtItem = (int)Main.rand.NextFromList(new short[]
								{
									4819,
									4820,
									4872,
									2331
								});
						}
						else
						{
							bool flag7 = autoFisher.legendary && !Main.hardMode && Main.rand.Next(3) == 0;
							if (flag7)
							{
								autoFisher.caughtItem = (int)Main.rand.NextFromList(new short[]
								{
										4819,
										4820,
										4872
								});
							}
							else
							{
								bool veryrare = autoFisher.veryrare;
								if (veryrare)
								{
									autoFisher.caughtItem = 2312;
								}
								else
								{
									bool rare = autoFisher.rare;
									if (rare)
									{
										autoFisher.caughtItem = 2315;
									}
								}
							}
						}
					}
				}
				else
				{
					if (autoFisher.inHoney)
					{
						bool flag8 = autoFisher.rare || (autoFisher.uncommon && Main.rand.Next(2) == 0);
						if (flag8)
						{
							autoFisher.caughtItem = 2314;
						}
						else
						{
							bool flag9 = autoFisher.uncommon && autoFisher.questFish == 2451;
							if (flag9)
							{
								autoFisher.caughtItem = 2451;
							}
						}
					}
					else
					{
						bool flag10 = Main.rand.Next(50) > autoFisher.fishingPower && Main.rand.Next(50) > autoFisher.fishingPower && autoFisher.poolSize < autoFisher.waterNeededToFish;
						if (flag10)
						{
							autoFisher.isJunk = true;
							autoFisher.caughtItem = Main.rand.Next(2337, 2340);
						}
						else
						{
							if (autoFisher.crate)
							{
								bool hardMode = Main.hardMode;
								bool flag11 = autoFisher.veryrare || autoFisher.legendary;
								if (flag11)
								{
									autoFisher.caughtItem = (hardMode ? 3981 : 2336);
								}
								else
								{
									bool flag12 = autoFisher.rare && autoFisher.zone.HasFlag(Zone.Dungeon);
									if (flag12)
									{
										autoFisher.caughtItem = (hardMode ? 3984 : 3205);
									}
									else
									{
										bool flag13 = autoFisher.rare && autoFisher.zone.HasFlag(Zone.Beach);
										if (flag13)
										{
											autoFisher.caughtItem = (hardMode ? 5003 : 5002);
										}
										else
										{
											bool flag14 = autoFisher.rare && autoFisher.zone.HasFlag(Zone.Corrupt);
											if (flag14)
											{
												autoFisher.caughtItem = (hardMode ? 3982 : 3203);
											}
											else
											{
												bool flag15 = autoFisher.rare && autoFisher.zone.HasFlag(Zone.Crimson);
												if (flag15)
												{
													autoFisher.caughtItem = (hardMode ? 3983 : 3204);
												}
												else
												{
													bool flag16 = autoFisher.rare && autoFisher.zone.HasFlag(Zone.Hallow);
													if (flag16)
													{
														autoFisher.caughtItem = (hardMode ? 3986 : 3207);
													}
													else
													{
														bool flag17 = autoFisher.rare && autoFisher.zone.HasFlag(Zone.Jungle);
														if (flag17)
														{
															autoFisher.caughtItem = (hardMode ? 3987 : 3208);
														}
														else
														{
															bool flag18 = autoFisher.rare && autoFisher.zone.HasFlag(Zone.Snow);
															if (flag18)
															{
																autoFisher.caughtItem = (hardMode ? 4406 : 4405);
															}
															else
															{
																bool flag19 = autoFisher.rare && autoFisher.zone.HasFlag(Zone.Desert);
																if (flag19)
																{
																	autoFisher.caughtItem = (hardMode ? 4408 : 4407);
																}
																else
																{
																	bool flag20 = autoFisher.rare && autoFisher.heightLevel == 0;
																	if (flag20)
																	{
																		autoFisher.caughtItem = (hardMode ? 3985 : 3206);
																	}
																	else
																	{
																		if (autoFisher.uncommon)
																		{
																			autoFisher.caughtItem = (hardMode ? 3980 : 2335);
																		}
																		else
																		{
																			autoFisher.caughtItem = (hardMode ? 3979 : 2334);
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
								bool flag21 = !NPC.combatBookWasUsed && Main.bloodMoon && autoFisher.legendary && Main.rand.Next(3) == 0;
								if (flag21)
								{
									autoFisher.caughtItem = 4382;
								}
								else
								{
									bool flag22 = autoFisher.legendary && Main.rand.Next(5) == 0;
									if (flag22)
									{
										autoFisher.caughtItem = 2423;
									}
									else
									{
										bool flag23 = autoFisher.legendary && Main.rand.Next(5) == 0;
										if (flag23)
										{
											autoFisher.caughtItem = 3225;
										}
										else
										{
											bool flag24 = autoFisher.legendary && Main.rand.Next(10) == 0;
											if (flag24)
											{
												autoFisher.caughtItem = 2420;
											}
											else
											{
												bool flag25 = !autoFisher.legendary && !autoFisher.veryrare && autoFisher.uncommon && Main.rand.Next(5) == 0;
												if (flag25)
												{
													autoFisher.caughtItem = 3196;
												}
												else
												{
													bool flag = autoFisher.zone.HasFlag(Zone.Desert);
													bool zoneDungeon = autoFisher.zone.HasFlag(Zone.Dungeon);
													if (zoneDungeon)
													{
														flag = false;
														bool flag26 = autoFisher.caughtItem == 0 && autoFisher.veryrare && Main.rand.Next(7) == 0;
														if (flag26)
														{
															autoFisher.caughtItem = 3000;
														}
													}
													else
													{
														bool flag2 = autoFisher.zone.HasFlag(Zone.Corrupt);
														bool flag3 = autoFisher.zone.HasFlag(Zone.Crimson);
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
															bool flag30 = autoFisher.legendary && Main.hardMode && autoFisher.zone.HasFlag(Zone.Snow) && autoFisher.heightLevel == 3 && Main.rand.Next(3) != 0;
															if (flag30)
															{
																autoFisher.caughtItem = 2429;
															}
															else
															{
																bool flag31 = autoFisher.legendary && Main.hardMode && Main.rand.Next(2) == 0;
																if (flag31)
																{
																	autoFisher.caughtItem = 3210;
																}
																else
																{
																	bool rare2 = autoFisher.rare;
																	if (rare2)
																	{
																		autoFisher.caughtItem = 2330;
																	}
																	else
																	{
																		bool flag32 = autoFisher.uncommon && autoFisher.questFish == 2454;
																		if (flag32)
																		{
																			autoFisher.caughtItem = 2454;
																		}
																		else
																		{
																			bool flag33 = autoFisher.uncommon && autoFisher.questFish == 2485;
																			if (flag33)
																			{
																				autoFisher.caughtItem = 2485;
																			}
																			else
																			{
																				bool flag34 = autoFisher.uncommon && autoFisher.questFish == 2457;
																				if (flag34)
																				{
																					autoFisher.caughtItem = 2457;
																				}
																				else
																				{
																					bool uncommon2 = autoFisher.uncommon;
																					if (uncommon2)
																					{
																						autoFisher.caughtItem = 2318;
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
																bool flag36 = autoFisher.legendary && Main.hardMode && autoFisher.zone.HasFlag(Zone.Snow) && autoFisher.heightLevel == 3 && Main.rand.Next(3) != 0;
																if (flag36)
																{
																	autoFisher.caughtItem = 2429;
																}
																else
																{
																	bool flag37 = autoFisher.legendary && Main.hardMode && Main.rand.Next(2) == 0;
																	if (flag37)
																	{
																		autoFisher.caughtItem = 3211;
																	}
																	else
																	{
																		bool flag38 = autoFisher.uncommon && autoFisher.questFish == 2477;
																		if (flag38)
																		{
																			autoFisher.caughtItem = 2477;
																		}
																		else
																		{
																			bool flag39 = autoFisher.uncommon && autoFisher.questFish == 2463;
																			if (flag39)
																			{
																				autoFisher.caughtItem = 2463;
																			}
																			else
																			{
																				if (autoFisher.uncommon)
																				{
																					autoFisher.caughtItem = 2319;
																				}
																				else
																				{
																					if (autoFisher.common)
																					{
																						autoFisher.caughtItem = 2305;
																					}
																				}
																			}
																		}
																	}
																}
															}
															else
															{
																bool zoneHallow = autoFisher.zone.HasFlag(Zone.Hallow);
																if (zoneHallow)
																{
																	bool flag40 = autoFisher.legendary && Main.hardMode && autoFisher.zone.HasFlag(Zone.Snow) && autoFisher.heightLevel == 3 && Main.rand.Next(3) != 0;
																	if (flag40)
																	{
																		autoFisher.caughtItem = 2429;
																	}
																	else
																	{
																		bool flag41 = autoFisher.legendary && Main.hardMode && Main.rand.Next(2) == 0;
																		if (flag41)
																		{
																			autoFisher.caughtItem = 3209;
																		}
																		else
																		{
																			bool flag42 = autoFisher.heightLevel > 1 && autoFisher.veryrare;
																			if (flag42)
																			{
																				autoFisher.caughtItem = 2317;
																			}
																			else
																			{
																				bool flag43 = autoFisher.heightLevel > 1 && autoFisher.uncommon && autoFisher.questFish == 2465;
																				if (flag43)
																				{
																					autoFisher.caughtItem = 2465;
																				}
																				else
																				{
																					bool flag44 = autoFisher.heightLevel < 2 && autoFisher.uncommon && autoFisher.questFish == 2468;
																					if (flag44)
																					{
																						autoFisher.caughtItem = 2468;
																					}
																					else
																					{
																						bool rare3 = autoFisher.rare;
																						if (rare3)
																						{
																							autoFisher.caughtItem = 2310;
																						}
																						else
																						{
																							bool flag45 = autoFisher.uncommon && autoFisher.questFish == 2471;
																							if (flag45)
																							{
																								autoFisher.caughtItem = 2471;
																							}
																							else
																							{
																								bool uncommon4 = autoFisher.uncommon;
																								if (uncommon4)
																								{
																									autoFisher.caughtItem = 2307;
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
														bool flag46 = autoFisher.caughtItem == 0 && autoFisher.zone.HasFlag(Zone.Snow);
														if (flag46)
														{
															bool flag47 = autoFisher.heightLevel < 2 && autoFisher.uncommon && autoFisher.questFish == 2467;
															if (flag47)
															{
																autoFisher.caughtItem = 2467;
															}
															else
															{
																bool flag48 = autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == 2470;
																if (flag48)
																{
																	autoFisher.caughtItem = 2470;
																}
																else
																{
																	bool flag49 = autoFisher.heightLevel >= 2 && autoFisher.uncommon && autoFisher.questFish == 2484;
																	if (flag49)
																	{
																		autoFisher.caughtItem = 2484;
																	}
																	else
																	{
																		bool flag50 = autoFisher.heightLevel > 1 && autoFisher.uncommon && autoFisher.questFish == 2466;
																		if (flag50)
																		{
																			autoFisher.caughtItem = 2466;
																		}
																		else
																		{
																			bool flag51 = (autoFisher.common && Main.rand.Next(12) == 0) || (autoFisher.uncommon && Main.rand.Next(6) == 0);
																			if (flag51)
																			{
																				autoFisher.caughtItem = 3197;
																			}
																			else
																			{
																				if (autoFisher.uncommon)
																				{
																					autoFisher.caughtItem = 2306;
																				}
																				else
																				{
																					if (autoFisher.common)
																					{
																						autoFisher.caughtItem = 2299;
																					}
																					else
																					{
																						bool flag52 = autoFisher.heightLevel > 1 && Main.rand.Next(3) == 0;
																						if (flag52)
																						{
																							autoFisher.caughtItem = 2309;
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
														bool flag53 = autoFisher.caughtItem == 0 && autoFisher.zone.HasFlag(Zone.Jungle);
														if (flag53)
														{
															bool flag54 = autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == 2452;
															if (flag54)
															{
																autoFisher.caughtItem = 2452;
															}
															else
															{
																bool flag55 = autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == 2483;
																if (flag55)
																{
																	autoFisher.caughtItem = 2483;
																}
																else
																{
																	bool flag56 = autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == 2488;
																	if (flag56)
																	{
																		autoFisher.caughtItem = 2488;
																	}
																	else
																	{
																		bool flag57 = autoFisher.heightLevel >= 1 && autoFisher.uncommon && autoFisher.questFish == 2486;
																		if (flag57)
																		{
																			autoFisher.caughtItem = 2486;
																		}
																		else
																		{
																			bool flag58 = autoFisher.heightLevel > 1 && autoFisher.uncommon;
																			if (flag58)
																			{
																				autoFisher.caughtItem = 2311;
																			}
																			else
																			{
																				if (autoFisher.uncommon)
																				{
																					autoFisher.caughtItem = 2313;
																				}
																				else
																				{
																					if (autoFisher.common)
																					{
																						autoFisher.caughtItem = 2302;
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
														bool flag59 = autoFisher.caughtItem == 0 && autoFisher.zone.HasFlag(Zone.Shroom) && autoFisher.uncommon && autoFisher.questFish == 2475;
														if (flag59)
														{
															autoFisher.caughtItem = 2475;
														}
													}
													bool flag60 = autoFisher.caughtItem == 0;
													if (flag60)
													{
														bool flag61 = autoFisher.heightLevel <= 1 && (autoFisher.position.X < 380 || autoFisher.position.X > Main.maxTilesX - 380) && autoFisher.poolSize > 1000;
														if (flag61)
														{
															bool flag62 = autoFisher.veryrare && Main.rand.Next(2) == 0;
															if (flag62)
															{
																autoFisher.caughtItem = 2341;
															}
															else
															{
																if (autoFisher.veryrare)
																{
																	autoFisher.caughtItem = 2342;
																}
																else
																{
																	bool flag63 = autoFisher.rare && Main.rand.Next(5) == 0;
																	if (flag63)
																	{
																		autoFisher.caughtItem = 2438;
																	}
																	else
																	{
																		bool flag64 = autoFisher.rare && Main.rand.Next(3) == 0;
																		if (flag64)
																		{
																			autoFisher.caughtItem = 2332;
																		}
																		else
																		{
																			bool flag65 = autoFisher.uncommon && autoFisher.questFish == 2480;
																			if (flag65)
																			{
																				autoFisher.caughtItem = 2480;
																			}
																			else
																			{
																				bool flag66 = autoFisher.uncommon && autoFisher.questFish == 2481;
																				if (flag66)
																				{
																					autoFisher.caughtItem = 2481;
																				}
																				else
																				{
																					if (autoFisher.uncommon)
																					{
																						autoFisher.caughtItem = 2316;
																					}
																					else
																					{
																						bool flag67 = autoFisher.common && Main.rand.Next(2) == 0;
																						if (flag67)
																						{
																							autoFisher.caughtItem = 2301;
																						}
																						else
																						{
																							if (autoFisher.common)
																							{
																								autoFisher.caughtItem = 2300;
																							}
																							else
																							{
																								autoFisher.caughtItem = 2297;
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
																bool flag69 = autoFisher.uncommon && autoFisher.questFish == 4393;
																if (flag69)
																{
																	autoFisher.caughtItem = 4393;
																}
																else
																{
																	bool flag70 = autoFisher.uncommon && autoFisher.questFish == 4394;
																	if (flag70)
																	{
																		autoFisher.caughtItem = 4394;
																	}
																	else
																	{
																		if (autoFisher.uncommon)
																		{
																			autoFisher.caughtItem = 4410;
																		}
																		else
																		{
																			bool flag71 = Main.rand.Next(3) == 0;
																			if (flag71)
																			{
																				autoFisher.caughtItem = 4402;
																			}
																			else
																			{
																				autoFisher.caughtItem = 4401;
																			}
																		}
																	}
																}
															}
														}
													}
													bool flag72 = autoFisher.caughtItem != 0;
													if (!flag72)
													{
														bool flag73 = autoFisher.heightLevel < 2 && autoFisher.uncommon && autoFisher.questFish == 2461;
														if (flag73)
														{
															autoFisher.caughtItem = 2461;
														}
														else
														{
															bool flag74 = autoFisher.heightLevel == 0 && autoFisher.uncommon && autoFisher.questFish == 2453;
															if (flag74)
															{
																autoFisher.caughtItem = 2453;
															}
															else
															{
																bool flag75 = autoFisher.heightLevel == 0 && autoFisher.uncommon && autoFisher.questFish == 2473;
																if (flag75)
																{
																	autoFisher.caughtItem = 2473;
																}
																else
																{
																	bool flag76 = autoFisher.heightLevel == 0 && autoFisher.uncommon && autoFisher.questFish == 2476;
																	if (flag76)
																	{
																		autoFisher.caughtItem = 2476;
																	}
																	else
																	{
																		bool flag77 = autoFisher.heightLevel < 2 && autoFisher.uncommon && autoFisher.questFish == 2458;
																		if (flag77)
																		{
																			autoFisher.caughtItem = 2458;
																		}
																		else
																		{
																			bool flag78 = autoFisher.heightLevel < 2 && autoFisher.uncommon && autoFisher.questFish == 2459;
																			if (flag78)
																			{
																				autoFisher.caughtItem = 2459;
																			}
																			else
																			{
																				bool flag79 = autoFisher.heightLevel == 0 && autoFisher.uncommon;
																				if (flag79)
																				{
																					autoFisher.caughtItem = 2304;
																				}
																				else
																				{
																					bool flag80 = autoFisher.heightLevel > 0 && autoFisher.heightLevel < 3 && autoFisher.uncommon && autoFisher.questFish == 2455;
																					if (flag80)
																					{
																						autoFisher.caughtItem = 2455;
																					}
																					else
																					{
																						bool flag81 = autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == 2479;
																						if (flag81)
																						{
																							autoFisher.caughtItem = 2479;
																						}
																						else
																						{
																							bool flag82 = autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == 2456;
																							if (flag82)
																							{
																								autoFisher.caughtItem = 2456;
																							}
																							else
																							{
																								bool flag83 = autoFisher.heightLevel == 1 && autoFisher.uncommon && autoFisher.questFish == 2474;
																								if (flag83)
																								{
																									autoFisher.caughtItem = 2474;
																								}
																								else
																								{
																									bool flag84 = autoFisher.heightLevel > 1 && autoFisher.rare && Main.rand.Next(5) == 0;
																									if (flag84)
																									{
																										bool flag85 = Main.hardMode && Main.rand.Next(2) == 0;
																										if (flag85)
																										{
																											autoFisher.caughtItem = 2437;
																										}
																										else
																										{
																											autoFisher.caughtItem = 2436;
																										}
																									}
																									else
																									{
																										bool flag86 = autoFisher.heightLevel > 1 && autoFisher.legendary && Main.rand.Next(3) != 0;
																										if (flag86)
																										{
																											autoFisher.caughtItem = 2308;
																										}
																										else
																										{
																											bool flag87 = autoFisher.heightLevel > 1 && autoFisher.veryrare && Main.rand.Next(2) == 0;
																											if (flag87)
																											{
																												autoFisher.caughtItem = 2320;
																											}
																											else
																											{
																												bool flag88 = autoFisher.heightLevel > 1 && autoFisher.rare;
																												if (flag88)
																												{
																													autoFisher.caughtItem = 2321;
																												}
																												else
																												{
																													bool flag89 = autoFisher.heightLevel > 1 && autoFisher.uncommon && autoFisher.questFish == 2478;
																													if (flag89)
																													{
																														autoFisher.caughtItem = 2478;
																													}
																													else
																													{
																														bool flag90 = autoFisher.heightLevel > 1 && autoFisher.uncommon && autoFisher.questFish == 2450;
																														if (flag90)
																														{
																															autoFisher.caughtItem = 2450;
																														}
																														else
																														{
																															bool flag91 = autoFisher.heightLevel > 1 && autoFisher.uncommon && autoFisher.questFish == 2464;
																															if (flag91)
																															{
																																autoFisher.caughtItem = 2464;
																															}
																															else
																															{
																																bool flag92 = autoFisher.heightLevel > 1 && autoFisher.uncommon && autoFisher.questFish == 2469;
																																if (flag92)
																																{
																																	autoFisher.caughtItem = 2469;
																																}
																																else
																																{
																																	bool flag93 = autoFisher.heightLevel > 2 && autoFisher.uncommon && autoFisher.questFish == 2462;
																																	if (flag93)
																																	{
																																		autoFisher.caughtItem = 2462;
																																	}
																																	else
																																	{
																																		bool flag94 = autoFisher.heightLevel > 2 && autoFisher.uncommon && autoFisher.questFish == 2482;
																																		if (flag94)
																																		{
																																			autoFisher.caughtItem = 2482;
																																		}
																																		else
																																		{
																																			bool flag95 = autoFisher.heightLevel > 2 && autoFisher.uncommon && autoFisher.questFish == 2472;
																																			if (flag95)
																																			{
																																				autoFisher.caughtItem = 2472;
																																			}
																																			else
																																			{
																																				bool flag96 = autoFisher.heightLevel > 2 && autoFisher.uncommon && autoFisher.questFish == 2460;
																																				if (flag96)
																																				{
																																					autoFisher.caughtItem = 2460;
																																				}
																																				else
																																				{
																																					bool flag97 = autoFisher.heightLevel > 1 && autoFisher.uncommon && Main.rand.Next(4) != 0;
																																					if (flag97)
																																					{
																																						autoFisher.caughtItem = 2303;
																																					}
																																					else
																																					{
																																						bool flag98 = autoFisher.heightLevel > 1 && (autoFisher.uncommon || autoFisher.common || Main.rand.Next(4) == 0);
																																						if (flag98)
																																						{
																																							bool flag99 = Main.rand.Next(4) == 0;
																																							if (flag99)
																																							{
																																								autoFisher.caughtItem = 2303;
																																							}
																																							else
																																							{
																																								autoFisher.caughtItem = 2309;
																																							}
																																						}
																																						else
																																						{
																																							bool flag100 = autoFisher.uncommon && autoFisher.questFish == 2487;
																																							if (flag100)
																																							{
																																								autoFisher.caughtItem = 2487;
																																							}
																																							else
																																							{
																																								bool flag101 = autoFisher.poolSize > 1000 && autoFisher.common;
																																								if (flag101)
																																								{
																																									autoFisher.caughtItem = 2298;
																																								}
																																								else
																																								{
																																									autoFisher.caughtItem = 2290;
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
			}
		}

		private static void GetAutoFishingPondWidth(int x, int y, out int minX, out int maxX)
		{
			minX = x;
			maxX = x;
			while (minX > 9 && Main.tile[(minX - 1), y].LiquidAmount > 0 && !WorldGen.SolidTile((minX -1), y, false))
			{
				minX--;
			}
			while (maxX < Main.maxTilesX - 9 && Main.tile[(maxX + 1), y].LiquidAmount > 0 && !WorldGen.SolidTile((maxX + 1), y, false))
			{
				maxX++;
			}
		}


		private static void GetAutoFishingPondState(int x, int y, out bool lava, out bool honey, out int numWaters, out int chumCount)
		{
			lava = false;
			honey = false;
			numWaters = 0;
			chumCount = 0;
			Point tileCoords = new(0, 0);
			GetAutoFishingPondWidth(x, y, out int minX, out int maxX);
			for (int i = minX; i <= maxX; i++)
			{
				int num = y;
				while (Main.tile[i, num].LiquidAmount > 0 && !WorldGen.SolidTile(i, num, false) && num < Main.maxTilesY - 10)
				{
					numWaters++;
					num++;
					if (HasLava(Main.tile[i, num]))
					{
						lava = true;
					}
					else
					{
						if (HasHoney(Main.tile[i, num]))
						{
							honey = true;
						}
					}
					tileCoords.X = i;
					tileCoords.Y = num;
					
					chumCount += Main.instance.ChumBucketProjectileHelper.GetChumsInLocation(tileCoords);
				}
			}
			if (honey)// boost tile count in honey
			{
				numWaters = (int)((double)numWaters * 1.5);
			}
		}

		private float AutoFisherBobber_GetWaterLine(int X, int Y)
		{
			float result = Projectile.position.Y + (float)Projectile.height;
			Tile tile1 = Main.tile[X, Y - 1];
			Tile tile2 = Main.tile[X, Y];
			Tile tile3 = Main.tile[X, Y + 1];
			if (tile1 == null)
			{
				tile1 = new Tile();
			}
			if (tile2 == null)
			{
				tile2 = new Tile();
			}
			if (tile3 == null)
			{
				tile3 = new Tile();
			}
			if (tile1.LiquidAmount > 0)
			{
				result = (Y * 16);
				result -= Main.tile[X, Y - 1].LiquidAmount / 16;
			}
			else
			{
				if (tile2.LiquidAmount > 0)
				{
					result = ((Y + 1) * 16);
					result -= (Main.tile[X, Y].LiquidAmount / 16);
				}
				else
				{
					if (tile3.LiquidAmount > 0)
					{
						result = ((Y + 2) * 16);
						result -= (Main.tile[X, Y + 1].LiquidAmount / 16);
					}
				}
			}
			return result;
		}

		private void AutofisherReduceRemainingChumsInPool()
		{
			
			int bobberTileX = (int)((Projectile.Center.X / 16f) + 1);
			int bobberTileY = (int)((Projectile.Center.Y / 16f) + 1);
			List<Tuple<int, Point>> list = new List<Tuple<int, Point>>();
			List<int> list2 = new List<int>();
			for (int i = 0; i < 1000; i++)
			{
				Projectile projectile = Main.projectile[i];
				if (projectile.active && projectile.owner == Main.myPlayer && projectile.timeLeft > 60 && projectile.type == ProjectileID.ChumBucket)
				{
					//GoldensMisc.Log("Bobber Y:" + bobberTileY + " Chum Y:" + (int)(projectile.Center.Y / 16f));
					Tuple<int, Point> tple = new(i, (projectile.Center + Vector2.UnitY * 16f).ToTileCoordinates());
					//GoldensMisc.Log(tple);
					list.Add(tple);
				}
			}
			if (!(list.Count == 0))
			{
				GetAutoFishingPondWidth(bobberTileX, bobberTileY, out int minX, out int maxX);
				//GoldensMisc.Log(minX + " " + maxX);
				Point p = default;
				for (int j = minX; j <= maxX; j++)
				{
					p.X = j;
					int index = bobberTileY - 1;
					/*GoldensMisc.Log(Main.tile[j, index]);
					if (Main.tile[j, index].LiquidAmount > 0)
						GoldensMisc.Log("Has liquid");
					if (!WorldGen.SolidTile(j, index, false))
						GoldensMisc.Log("Is not solid");
					if (index < Main.maxTilesY - 10)
						GoldensMisc.Log("Above world edge");
					int explosion = Projectile.NewProjectile(null, new Vector2(((float)j * 16f), ((float)index * 16f)), new Vector2(0f, 0f), ProjectileID.Grenade, 0, 0);*/
					while (Main.tile[j, index].LiquidAmount > 0 && !WorldGen.SolidTile(j, index, false) && index < Main.maxTilesY - 10)
					{
						
						index++;
						p.Y = index;
						for (int l = list.Count - 1; l >= 0; l--)
						{
							//GoldensMisc.Log("Point X:" + p.X + "Point Y:" + p.Y);
							//GoldensMisc.Log("Tuple X:" + list[l].Item2.X + "Tuple Y:" + list[l].Item2.Y);
							if (list[l].Item2 == p)
							{
								//GoldensMisc.Log("Ran code 4");
								list2.Add(list[l].Item1);
								list.RemoveAt(l);
							}
						}
						if (list.Count == 0)
						{
							break;
						}
					}
					if (list.Count == 0)
					{
						break;
					}
				}
				for (int k = 0; k < list2.Count; k++)
				{
					Projectile obj = Main.projectile[list2[k]];
					//GoldensMisc.Log(obj);
					obj.ai[0] += 1f;
					obj.netUpdate = true;
				}
			}
		}

		public struct AutoFishingAttempt
		{
			public Point position;
			public bool common;
			public bool uncommon;
			public bool rare;
			public bool veryrare;
			public bool legendary;
			public bool crate;
			public bool isJunk;
			public bool inLava;
			public bool inHoney;
			public int poolSize;
			public int waterNeededToFish;
			public float waterQuality;
			public int chumsInWater;
			public int fishingPower;
			public Item baitItem;
			public Zone zone;
			public int bobberType;
			public float atmosphere;
			public int questFish;
			public int heightLevel;
			public int caughtItem;
			public int caughtEnemy;
		}
		public override bool PreDrawExtras()
		{
			if(!TileEntity.ByID.ContainsKey((int)Projectile.ai[1]))
				return false;

            AutofisherTE te = TileEntity.ByID[(int)Projectile.ai[1]] as AutofisherTE;
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
				bool facingRight = Main.tile[te.Position.X, te.Position.Y].TileFrameX == 0;
				Vector2 value = te.Position.ToWorldCoordinates(2, 0);
				value -= new Vector2(6f, 4f); //brute-force fix for weird offset
				if(facingRight)
					value.X += 46f;


				//value = Main.player[Projectile.owner].RotatedRelativePoint(value + new Vector2(8f), true) - new Vector2(8f);
				float projPosX = Projectile.position.X + (float)Projectile.width * 0.5f - value.X;
				float projPosY = Projectile.position.Y + (float)Projectile.height * 0.5f - value.Y;
				Math.Sqrt((double)(projPosX * projPosX + projPosY * projPosY));
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
						float rotation2 = (float)Math.Atan2((double)projPosY, (double)projPosX) - 1.57f;
						Color color2 = Lighting.GetColor((int)value.X / 16, (int)(value.Y / 16f), FishingLineColor);
						Main.spriteBatch.Draw(TextureAssets.FishingLine.Value, new Vector2(value.X - Main.screenPosition.X + (float)TextureAssets.FishingLine.Width() * 0.5f, value.Y - Main.screenPosition.Y + (float)TextureAssets.FishingLine.Height() * 0.5f), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, 0, TextureAssets.FishingLine.Width(), (int)num)), color2, rotation2, new Vector2((float)TextureAssets.FishingLine.Width() * 0.5f, 0f), 1f, SpriteEffects.None, 0f);
					}
				}
			}
			return false;
		}
	}
}
