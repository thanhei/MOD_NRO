using System;
using System.Collections.Generic;

namespace Mod.AStar.Collections.MultiDimensional
{
	// Token: 0x020001B2 RID: 434
	public interface IModelAGrid<T>
	{
		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06001287 RID: 4743
		int Height { get; }

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06001288 RID: 4744
		int Width { get; }

		// Token: 0x17000125 RID: 293
		T this[int row, int column] { get; set; }

		// Token: 0x17000126 RID: 294
		T this[Position position] { get; set; }

		// Token: 0x0600128D RID: 4749
		IEnumerable<Position> GetSuccessorPositions(Position node, bool optionsUseDiagonals = false);
	}
}
