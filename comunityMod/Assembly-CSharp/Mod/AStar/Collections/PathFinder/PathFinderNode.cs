using System;
using System.Runtime.InteropServices;

namespace Mod.AStar.Collections.PathFinder
{
	// Token: 0x020001AC RID: 428
	[StructLayout(LayoutKind.Sequential, Pack = 1)]
	internal readonly struct PathFinderNode
	{
		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06001257 RID: 4695 RVA: 0x000C34F4 File Offset: 0x000C16F4
		public Position Position { get; }

		// Token: 0x17000111 RID: 273
		// (get) Token: 0x06001258 RID: 4696 RVA: 0x000C34FC File Offset: 0x000C16FC
		public int G { get; }

		// Token: 0x17000112 RID: 274
		// (get) Token: 0x06001259 RID: 4697 RVA: 0x000C3504 File Offset: 0x000C1704
		public int H { get; }

		// Token: 0x17000113 RID: 275
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x000C350C File Offset: 0x000C170C
		public Position ParentNodePosition { get; }

		// Token: 0x17000114 RID: 276
		// (get) Token: 0x0600125B RID: 4699 RVA: 0x000C3514 File Offset: 0x000C1714
		public int F { get; }

		// Token: 0x17000115 RID: 277
		// (get) Token: 0x0600125C RID: 4700 RVA: 0x000C351C File Offset: 0x000C171C
		public bool HasBeenVisited
		{
			get
			{
				return this.F > 0;
			}
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x000C3527 File Offset: 0x000C1727
		public PathFinderNode(Position position, int g, int h, Position parentNodePosition)
		{
			this.Position = position;
			this.G = g;
			this.H = h;
			this.ParentNodePosition = parentNodePosition;
			this.F = g + h;
		}
	}
}
