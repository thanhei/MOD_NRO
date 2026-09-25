using System;

namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A0 RID: 416
	public class Euclidean : ICalculateHeuristic
	{
		// Token: 0x0600122E RID: 4654 RVA: 0x000C2FC4 File Offset: 0x000C11C4
		public int Calculate(Position source, Position destination)
		{
			return (int)((double)2 * Math.Sqrt(Math.Pow((double)(source.Row - destination.Row), 2.0) + Math.Pow((double)(source.Column - destination.Column), 2.0)));
		}
	}
}
