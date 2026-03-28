using System;

// Token: 0x02000070 RID: 112
public class MovePoint
{
	// Token: 0x060003D9 RID: 985 RVA: 0x00005F9A File Offset: 0x0000419A
	public MovePoint(int xEnd, int yEnd, int act, int dir)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.dir = dir;
		this.status = act;
	}

	// Token: 0x060003DA RID: 986 RVA: 0x00005FBF File Offset: 0x000041BF
	public MovePoint(int xEnd, int yEnd)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
	}

	// Token: 0x040006B9 RID: 1721
	public int xEnd;

	// Token: 0x040006BA RID: 1722
	public int yEnd;

	// Token: 0x040006BB RID: 1723
	public int dir;

	// Token: 0x040006BC RID: 1724
	public int cvx;

	// Token: 0x040006BD RID: 1725
	public int cvy;

	// Token: 0x040006BE RID: 1726
	public int status;
}
