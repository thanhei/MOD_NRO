using System;

namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A2 RID: 418
	public static class HeuristicFactory
	{
		// Token: 0x06001232 RID: 4658 RVA: 0x000C3068 File Offset: 0x000C1268
		public static ICalculateHeuristic Create(HeuristicFormula heuristicFormula)
		{
			switch (heuristicFormula)
			{
			case HeuristicFormula.Manhattan:
				return new Manhattan();
			case HeuristicFormula.MaxDXDY:
				return new MaxDXDY();
			case HeuristicFormula.DiagonalShortCut:
				return new DiagonalShortcut();
			case HeuristicFormula.Euclidean:
				return new Euclidean();
			case HeuristicFormula.EuclideanNoSQR:
				return new EuclideanNoSQR();
			case HeuristicFormula.Custom1:
				return new Custom1();
			default:
				throw new ArgumentOutOfRangeException("heuristicFormula", heuristicFormula, null);
			}
		}
	}
}
