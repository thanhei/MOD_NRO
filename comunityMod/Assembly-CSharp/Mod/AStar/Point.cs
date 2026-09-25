using System;

namespace Mod.AStar
{
	// Token: 0x02000198 RID: 408
	public class Point
	{
		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600120F RID: 4623 RVA: 0x000C2CA1 File Offset: 0x000C0EA1
		public int X
		{
			get
			{
				return this.x;
			}
		}

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x06001210 RID: 4624 RVA: 0x000C2CA9 File Offset: 0x000C0EA9
		public int Y
		{
			get
			{
				return this.y;
			}
		}

		// Token: 0x06001211 RID: 4625 RVA: 0x000C2CB1 File Offset: 0x000C0EB1
		public Point(int x, int y)
		{
			this.x = x;
			this.y = y;
		}

		// Token: 0x0400196F RID: 6511
		private int x;

		// Token: 0x04001970 RID: 6512
		private int y;
	}
}
