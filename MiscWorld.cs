
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using GoldensMisc.Tiles;

namespace GoldensMisc
{
	public class MiscWorld : ModWorld
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			if(Config.AncientForges)
			{
				int index = tasks.FindIndex(genpass => genpass.Name.Equals("Hellforge"));
				if(index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("[Miscellania] Ancient Hellforge", AddHellforges));
				}
				
				index = tasks.FindIndex(genpass => genpass.Name.Equals("Buried Chests"));
				if(index != -1)
				{
					tasks.Insert(index + 1, new PassLegacy("[Miscellania] Ancient Forge", AddForges));
				}
			}
		}
		
		public void AddHellforges(GenerationProgress progress = null)
		{
			AddFurniture(progress, "ancient hellforges", mod.TileType<AncientHellforge>(), 1500, Main.maxTilesY - 250, Main.maxTilesY - 5, WallID.ObsidianBrickUnsafe, WallID.HellstoneBrickUnsafe);
		}
		
		public void AddForges(GenerationProgress progress = null)
		{
			AddFurniture(progress, "ancient forges", mod.TileType<AncientForge>(), 300, (int)Main.worldSurface, Main.maxTilesY - 300, WallID.Planked, WallID.BorealWood);
		}
		
		static void AddFurniture(GenerationProgress progress, string name, int type, int rarity, int minY, int maxY, params int[] wallIDs)
		{
			try
			{
				if (progress != null)
				{
					progress.Message = "Adding " + name;
				}
				else
				{
					Main.NewText("Adding " + name);
				}
				int generated = 0;
				float toGenerate = Main.maxTilesX / rarity;
				
//				var watch = Stopwatch.StartNew();
				for (int i = 0; i < toGenerate; i++)
				{
					progress.Set(i / toGenerate);
					bool success = false;
					int attempts = 0;
					while (!success)
					{
//						if(watch.Elapsed.Seconds >= 10)
//						{
//							GoldensMisc.Log("SUCC FOR TOO LONG IMMA NUT");
//							GoldensMisc.Log("Generated {0}/{1} {2}", generated, toGenerate, name);
//							watch.Stop();
//							return;
//						}
						int x = WorldGen.genRand.Next(1, Main.maxTilesX);
						int y = WorldGen.genRand.Next(minY, maxY);
						foreach(int wallID in wallIDs)
						{
							if(Main.tile[x, y].wall == wallID)
							{
//								GoldensMisc.Log("READY FOR THE SUCC {0} {1}", num, attempts);
								while (!Main.tile[x, y].active())
								{
									y++;
								}
								y--;
								WorldGen.PlaceObject(x, y, (ushort)type, true);
								if (Main.tile[x, y].type == (ushort)type)
								{
									generated++;
									success = true;
//									GoldensMisc.Log("SUCCESS");
								}
								else
								{
									attempts++;
									if (attempts >= 1000)
									{
//										GoldensMisc.Log("OH NO");
										success = true;
									}
								}
							}
						}
					}
				}
				if(progress != null)
				{
					GoldensMisc.Log("Generated {0}/{1} {2}", generated, toGenerate, name);
				}
				else
				{
					Main.NewText(String.Format("Generated {0}/{1} {2}", generated, toGenerate, name));
				}
			}
			//Vanilla Terraria does this, so I guess this is OK...?
			catch(Exception e)
			{
				ErrorLogger.Log(e.ToString());
			}
		}
	}
}
