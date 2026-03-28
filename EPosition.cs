using System;

// Token: 0x02000038 RID: 56
public class EPosition
{
	// Token: 0x06000255 RID: 597 RVA: 0x0000550D File Offset: 0x0000370D
	public EPosition(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000256 RID: 598 RVA: 0x00005531 File Offset: 0x00003731
	public EPosition(int x, int y, int fol)
	{
		this.x = x;
		this.y = y;
		this.follow = (sbyte)fol;
	}

	// Token: 0x06000257 RID: 599 RVA: 0x0000555D File Offset: 0x0000375D
	public EPosition()
	{
	}

	// Token: 0x04000299 RID: 665
	public int x;

	// Token: 0x0400029A RID: 666
	public int y;

	// Token: 0x0400029B RID: 667
	public int anchor;

	// Token: 0x0400029C RID: 668
	public sbyte follow;

	// Token: 0x0400029D RID: 669
	public sbyte count;

	// Token: 0x0400029E RID: 670
	public sbyte dir = 1;

	// Token: 0x0400029F RID: 671
	public short index = -1;
}
