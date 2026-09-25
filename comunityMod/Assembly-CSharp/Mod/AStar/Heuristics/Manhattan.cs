using System;

namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A5 RID: 421
	public class Manhattan : ICalculateHeuristic
	{
		// Token: 0x06001234 RID: 4660 RVA: 0x000C30CC File Offset: 0x000C12CC
		public int Calculate(Position source, Position destination)
		{
			return 2 * (Math.Abs(source.Row - destination.Row) + Math.Abs(source.Column - destination.Column));
		}
	}
}
