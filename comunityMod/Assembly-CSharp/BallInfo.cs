using System;

// Token: 0x02000011 RID: 17
public class BallInfo
{
	// Token: 0x060000AF RID: 175 RVA: 0x00006EBC File Offset: 0x000050BC
	public void SetChar()
	{
		this.cFocus = new global::Char();
		this.cFocus.charID = Res.random(-999, -800);
		this.cFocus.head = -1;
		this.cFocus.body = -1;
		this.cFocus.leg = -1;
		this.cFocus.bag = -1;
		this.cFocus.cName = string.Empty;
		this.cFocus.cHP = (this.cFocus.cHPFull = 20);
	}

	// Token: 0x060000B0 RID: 176 RVA: 0x00006F49 File Offset: 0x00005149
	public void UpdChar()
	{
		this.cFocus.cx = this.x;
		this.cFocus.cy = this.y;
	}

	// Token: 0x040000CF RID: 207
	public int x;

	// Token: 0x040000D0 RID: 208
	public int y;

	// Token: 0x040000D1 RID: 209
	public int xTo = -999;

	// Token: 0x040000D2 RID: 210
	public int yTo = -999;

	// Token: 0x040000D3 RID: 211
	public int count;

	// Token: 0x040000D4 RID: 212
	public int vy;

	// Token: 0x040000D5 RID: 213
	public int vx;

	// Token: 0x040000D6 RID: 214
	public int dir;

	// Token: 0x040000D7 RID: 215
	public int idImg;

	// Token: 0x040000D8 RID: 216
	public bool isPaint = true;

	// Token: 0x040000D9 RID: 217
	public bool isDone;

	// Token: 0x040000DA RID: 218
	public bool isSetImg;

	// Token: 0x040000DB RID: 219
	public global::Char cFocus;
}
