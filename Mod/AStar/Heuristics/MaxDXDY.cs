using System;

namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A6 RID: 422
	public class MaxDXDY : ICalculateHeuristic
	{
		// Token: 0x06001236 RID: 4662 RVA: 0x000C30F9 File Offset: 0x000C12F9
		public int Calculate(Position source, Position destination)
		{
			return 2 * Math.Max(Math.Abs(source.Row - destination.Row), Math.Abs(source.Column - destination.Column));
		}
	}
}
