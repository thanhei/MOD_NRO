using System;

namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A1 RID: 417
	public class EuclideanNoSQR : ICalculateHeuristic
	{
		// Token: 0x06001230 RID: 4656 RVA: 0x000C3018 File Offset: 0x000C1218
		public int Calculate(Position source, Position destination)
		{
			return (int)((double)2 * (Math.Pow((double)(source.Row - destination.Row), 2.0) + Math.Pow((double)(source.Column - destination.Column), 2.0)));
		}
	}
}
