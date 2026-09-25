using System;

namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A3 RID: 419
	public enum HeuristicFormula
	{
		// Token: 0x0400197D RID: 6525
		Manhattan = 1,
		// Token: 0x0400197E RID: 6526
		MaxDXDY,
		// Token: 0x0400197F RID: 6527
		DiagonalShortCut,
		// Token: 0x04001980 RID: 6528
		Euclidean,
		// Token: 0x04001981 RID: 6529
		EuclideanNoSQR,
		// Token: 0x04001982 RID: 6530
		Custom1
	}
}
