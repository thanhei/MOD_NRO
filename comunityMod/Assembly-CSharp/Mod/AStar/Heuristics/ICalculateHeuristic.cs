using System;

namespace Mod.AStar.Heuristics
{
	// Token: 0x020001A4 RID: 420
	public interface ICalculateHeuristic
	{
		// Token: 0x06001233 RID: 4659
		int Calculate(Position source, Position destination);
	}
}
