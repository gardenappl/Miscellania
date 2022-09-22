
using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.GameContent.Generation;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.WorldBuilding;
using GoldensMisc.Tiles;
using Terraria.Localization;
using Terraria.IO;

namespace GoldensMisc
{
	public class MiscSystem : ModSystem
	{

		public override void AddRecipeGroups()
		{
			MiscRecipes.AddRecipeGroups();
		}

		public override void PostAddRecipes()
		{
			MiscRecipes.PostAddRecipes();
		}

		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			if(ModContent.GetInstance<ServerConfig>().AncientForges)
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

        public static void AddHellforges(GenerationProgress progress = null, GameConfiguration configuration = null)
		{
			AddFurniture(progress, Language.GetTextValue("Mods.GoldensMisc.WorldGen.AncientHellforge"), ModContent.TileType<AncientHellforge>(), 1500, Main.maxTilesY - 250, Main.maxTilesY - 5, WallID.ObsidianBrickUnsafe, WallID.HellstoneBrickUnsafe);
		}
		
		public static void AddForges(GenerationProgress progress = null, GameConfiguration configuration = null)
		{
			AddFurniture(progress, Language.GetTextValue("Mods.GoldensMisc.WorldGen.AncientForge"), ModContent.TileType<AncientForge>(), 300, (int)Main.worldSurface, Main.maxTilesY - 300, WallID.Planked, WallID.BorealWood);
		}
		
		static void AddFurniture(GenerationProgress progress, string name, int type, int rarity, int minY, int maxY, params int[] wallIDs)
		{
			try
			{
				if (progress != null)
				{
					progress.Message = name;
				}
				else
				{
					Main.NewText(name);
				}
				int generated = 0;
				int toGenerate = Main.maxTilesX / rarity;
				
				for (int i = 0; i < toGenerate; i++)
				{
					if(progress != null)
						progress.Set(i / toGenerate);
					bool success = false;
					byte attempts = 0;
					ushort successcount = 0;
					while (!success)
					{
						

						if (successcount < 65533)
						{
							successcount++;
						}
						else
						{
							successcount = ushort.MaxValue;
							break;
						}
						
						int x = WorldGen.genRand.Next(1, Main.maxTilesX);
						int y = WorldGen.genRand.Next(minY, maxY);
						foreach(int wallID in wallIDs)
						{
							if(Main.tile[x, y].WallType == wallID)
							{
								byte hasTileCount = 0;
								while (!Main.tile[x, y].HasTile)
								{
									y++;
									if (hasTileCount < 80)
									{
										hasTileCount++;
									}
									else
									{
										hasTileCount = byte.MaxValue;
										break;
									}
								}
								y--;
								WorldGen.PlaceObject(x, y, (ushort)type, true);
								if (Main.tile[x, y].TileType == (ushort)type)
								{
									generated++;
									success = true;
								}
								else
								{
									attempts++;
									if (attempts > 254)
									{
										success = true;
									}
								}
							}
						}
					}
				}
				if(progress != null)
				{
					GoldensMisc.Log(name + ' ' + Language.GetTextValue("Mods.GoldensMisc.WorldGen.FurnitureGeneratorResults"), generated, toGenerate);
				}
				else
				{
					Main.NewText(Language.GetTextValue("Mods.GoldensMisc.WorldGen.FurnitureGeneratorResults", generated, toGenerate));
				}
			}
			//Vanilla Terraria does this, so I guess this is OK...?
			catch(Exception e)
			{
				GoldensMisc.Error("Error during worldgen", e);
			}
		}
	}
}
