using System;

namespace Mod.AStar
{
	// Token: 0x02000195 RID: 405
	public interface IFindAPath
	{
		// Token: 0x06001203 RID: 4611
		Position[] FindPath(Position start, Position end);

		// Token: 0x06001204 RID: 4612
		Point[] FindPath(Point start, Point end);
	}
}
