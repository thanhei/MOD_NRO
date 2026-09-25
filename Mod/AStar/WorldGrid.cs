using System;
using Mod.AStar.Collections.MultiDimensional;

namespace Mod.AStar
{
	// Token: 0x0200019B RID: 411
	public class WorldGrid : Grid<short>
	{
		// Token: 0x0600121D RID: 4637 RVA: 0x000C2DF9 File Offset: 0x000C0FF9
		public WorldGrid(int height, int width)
			: base(height, width)
		{
		}

		// Token: 0x0600121E RID: 4638 RVA: 0x000C2E04 File Offset: 0x000C1004
		public WorldGrid(short[,] worldArray)
			: base(worldArray.GetLength(0), worldArray.GetLength(1))
		{
			for (int i = 0; i < worldArray.GetLength(0); i++)
			{
				for (int j = 0; j < worldArray.GetLength(1); j++)
				{
					base[i, j] = worldArray[i, j];
				}
			}
		}
	}
}
