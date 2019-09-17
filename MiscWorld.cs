
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
using Terraria.Localization;

namespace GoldensMisc
{
	public class MiscWorld : ModWorld
	{
		public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
		{
			if(ServerConfig.Instance.AncientForges)
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
			AddFurniture(progress, Language.GetTextValue("Mods.GoldensMisc.WorldGen.AncientHellforge"), mod.TileType<AncientHellforge>(), 1500, Main.maxTilesY - 250, Main.maxTilesY - 5, WallID.ObsidianBrickUnsafe, WallID.HellstoneBrickUnsafe);
		}
		
		public void AddForges(GenerationProgress progress = null)
		{
			AddFurniture(progress, Language.GetTextValue("Mods.GoldensMisc.WorldGen.AncientForge"), mod.TileType<AncientForge>(), 300, (int)Main.worldSurface, Main.maxTilesY - 300, WallID.Planked, WallID.BorealWood);
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
					int attempts = 0;
					while (!success)
					{
						int x = WorldGen.genRand.Next(1, Main.maxTilesX);
						int y = WorldGen.genRand.Next(minY, maxY);
						foreach(int wallID in wallIDs)
						{
							if(Main.tile[x, y].wall == wallID)
							{
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
								}
								else
								{
									attempts++;
									if (attempts >= 1000)
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
				GoldensMisc.Instance.Logger.Error(e.ToString());
			}
		}
	}
}
