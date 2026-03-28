using System;

// Token: 0x0200009E RID: 158
public class BallInfo
{
	// Token: 0x060005CA RID: 1482 RVA: 0x0004AF54 File Offset: 0x00049154
	public void SetChar()
	{
		this.cFocus = new global::Char();
		this.cFocus.charID = Res.random(-999, -800);
		this.cFocus.head = -1;
		this.cFocus.body = -1;
		this.cFocus.leg = -1;
		this.cFocus.bag = -1;
		this.cFocus.cName = string.Empty;
		this.cFocus.cHP = (this.cFocus.cHPFull = 20L);
	}

	// Token: 0x060005CB RID: 1483 RVA: 0x00006E89 File Offset: 0x00005089
	public void UpdChar()
	{
		this.cFocus.cx = this.x;
		this.cFocus.cy = this.y;
	}

	// Token: 0x04000A74 RID: 2676
	public int x;

	// Token: 0x04000A75 RID: 2677
	public int y;

	// Token: 0x04000A76 RID: 2678
	public int xTo = -999;

	// Token: 0x04000A77 RID: 2679
	public int yTo = -999;

	// Token: 0x04000A78 RID: 2680
	public int count;

	// Token: 0x04000A79 RID: 2681
	public int vy;

	// Token: 0x04000A7A RID: 2682
	public int vx;

	// Token: 0x04000A7B RID: 2683
	public int dir;

	// Token: 0x04000A7C RID: 2684
	public int idImg;

	// Token: 0x04000A7D RID: 2685
	public bool isPaint = true;

	// Token: 0x04000A7E RID: 2686
	public bool isDone;

	// Token: 0x04000A7F RID: 2687
	public bool isSetImg;

	// Token: 0x04000A80 RID: 2688
	public global::Char cFocus;
}
