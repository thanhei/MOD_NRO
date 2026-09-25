using System;

// Token: 0x02000029 RID: 41
public class EPosition
{
	// Token: 0x0600024D RID: 589 RVA: 0x0002E4E4 File Offset: 0x0002C6E4
	public EPosition(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x0600024E RID: 590 RVA: 0x0002E508 File Offset: 0x0002C708
	public EPosition(int x, int y, int fol)
	{
		this.x = x;
		this.y = y;
		this.follow = (sbyte)fol;
	}

	// Token: 0x0600024F RID: 591 RVA: 0x0002E534 File Offset: 0x0002C734
	public EPosition()
	{
	}

	// Token: 0x040004FD RID: 1277
	public int x;

	// Token: 0x040004FE RID: 1278
	public int y;

	// Token: 0x040004FF RID: 1279
	public int anchor;

	// Token: 0x04000500 RID: 1280
	public sbyte follow;

	// Token: 0x04000501 RID: 1281
	public sbyte count;

	// Token: 0x04000502 RID: 1282
	public sbyte dir = 1;

	// Token: 0x04000503 RID: 1283
	public short index = -1;
}
