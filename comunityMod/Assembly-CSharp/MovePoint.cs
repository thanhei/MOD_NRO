using System;

// Token: 0x02000073 RID: 115
public class MovePoint
{
	// Token: 0x060005A6 RID: 1446 RVA: 0x000570DF File Offset: 0x000552DF
	public MovePoint(int xEnd, int yEnd, int act, int dir)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.dir = dir;
		this.status = act;
	}

	// Token: 0x060005A7 RID: 1447 RVA: 0x00057104 File Offset: 0x00055304
	public MovePoint(int xEnd, int yEnd)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
	}

	// Token: 0x04000BF6 RID: 3062
	public int xEnd;

	// Token: 0x04000BF7 RID: 3063
	public int yEnd;

	// Token: 0x04000BF8 RID: 3064
	public int dir;

	// Token: 0x04000BF9 RID: 3065
	public int cvx;

	// Token: 0x04000BFA RID: 3066
	public int cvy;

	// Token: 0x04000BFB RID: 3067
	public int status;
}
