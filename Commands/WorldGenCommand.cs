
using System;
using System.Threading.Tasks;
using Terraria;
using Terraria.ModLoader;
using Terraria.World.Generation;

namespace GoldensMisc.Commands
{
	public class WorldGenCommand : ModCommand
	{
		public override CommandType Type
		{
			get { return CommandType.World; }
		}
		
		public override string Command
		{
			get { return "miscWorldGen"; }
		}
		
		public override string Usage
		{
			get { return "/miscWorldGen <forge|hellforge>"; }
		}
		
		public override string Description
		{
			get { return "Generates Miscellania world features in pre-existing worlds" + Environment.NewLine +
					"WARNING: May cause lag, as well as damage previously built/generated structures!"; }
		}
		
		public override void Action(CommandCaller caller, string input, string[] args)
		{
			if(args.Length != 1)
			{
				throw new UsageException("Usage: " + Usage);
			}
			if(args[0].Equals("hellforge", StringComparison.OrdinalIgnoreCase))
			{
				Task.Run(() => mod.GetModWorld<MiscWorld>().AddHellforges());
			}
			if(args[0].Equals("forge", StringComparison.OrdinalIgnoreCase))
			{
				Task.Run(() => mod.GetModWorld<MiscWorld>().AddForges());
			}
			else
			{
				throw new UsageException("Usage: " + Usage);
			}
			return;
		}
	}
}
