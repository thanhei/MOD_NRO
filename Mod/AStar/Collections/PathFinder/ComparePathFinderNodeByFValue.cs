using System;
using System.Collections.Generic;

namespace Mod.AStar.Collections.PathFinder
{
	// Token: 0x020001A9 RID: 425
	internal class ComparePathFinderNodeByFValue : IComparer<PathFinderNode>
	{
		// Token: 0x06001248 RID: 4680 RVA: 0x000C33A4 File Offset: 0x000C15A4
		public int Compare(PathFinderNode a, PathFinderNode b)
		{
			if (a.F > b.F)
			{
				return 1;
			}
			if (a.F < b.F)
			{
				return -1;
			}
			return 0;
		}
	}
}
