using System;

namespace Mod.AStar
{
	// Token: 0x0200019A RID: 410
	public static class PositionExtensions
	{
		// Token: 0x0600121B RID: 4635 RVA: 0x000C2DD1 File Offset: 0x000C0FD1
		public static Point ToPoint(this Position position)
		{
			return new Point(position.Column, position.Row);
		}

		// Token: 0x0600121C RID: 4636 RVA: 0x000C2DE6 File Offset: 0x000C0FE6
		public static Position ToPosition(this Point point)
		{
			return new Position(point.Y, point.X);
		}
	}
}
