using System;

// Token: 0x0200008E RID: 142
public class Position
{
	// Token: 0x06000765 RID: 1893 RVA: 0x000752C7 File Offset: 0x000734C7
	public Position()
	{
		this.x = 0;
		this.y = 0;
	}

	// Token: 0x06000766 RID: 1894 RVA: 0x000752DD File Offset: 0x000734DD
	public Position(int x, int y, int anchor)
	{
		this.x = x;
		this.y = y;
		this.anchor = anchor;
	}

	// Token: 0x06000767 RID: 1895 RVA: 0x000752FA File Offset: 0x000734FA
	public Position(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000768 RID: 1896 RVA: 0x00075310 File Offset: 0x00073510
	public void setPosTo(int xT, int yT)
	{
		this.xTo = (short)xT;
		this.yTo = (short)yT;
		this.distant = (short)Res.distance(this.x, this.y, (int)this.xTo, (int)this.yTo);
	}

	// Token: 0x06000769 RID: 1897 RVA: 0x00075348 File Offset: 0x00073548
	public int translate()
	{
		if (this.x == (int)this.xTo && this.y == (int)this.yTo)
		{
			return -1;
		}
		if (Math2.abs(((int)this.xTo - this.x) / 2) <= 1 && Math2.abs(((int)this.yTo - this.y) / 2) <= 1)
		{
			this.x = (int)this.xTo;
			this.y = (int)this.yTo;
			return 0;
		}
		if (this.x != (int)this.xTo)
		{
			this.x += ((int)this.xTo - this.x) / 2;
		}
		if (this.y != (int)this.yTo)
		{
			this.y += ((int)this.yTo - this.y) / 2;
		}
		if (Res.distance(this.x, this.y, (int)this.xTo, (int)this.yTo) <= (int)(this.distant / 5))
		{
			return 2;
		}
		return 1;
	}

	// Token: 0x0600076A RID: 1898 RVA: 0x00075439 File Offset: 0x00073639
	public void update()
	{
		this.layer.update();
	}

	// Token: 0x0600076B RID: 1899 RVA: 0x00075446 File Offset: 0x00073646
	public void paint(mGraphics g)
	{
		this.layer.paint(g, this.x, this.y);
	}

	// Token: 0x04000E42 RID: 3650
	public int x;

	// Token: 0x04000E43 RID: 3651
	public int y;

	// Token: 0x04000E44 RID: 3652
	public int anchor;

	// Token: 0x04000E45 RID: 3653
	public int g;

	// Token: 0x04000E46 RID: 3654
	public int v;

	// Token: 0x04000E47 RID: 3655
	public int w;

	// Token: 0x04000E48 RID: 3656
	public int h;

	// Token: 0x04000E49 RID: 3657
	public int color;

	// Token: 0x04000E4A RID: 3658
	public int limitY;

	// Token: 0x04000E4B RID: 3659
	public Layer layer;

	// Token: 0x04000E4C RID: 3660
	public short yTo;

	// Token: 0x04000E4D RID: 3661
	public short xTo;

	// Token: 0x04000E4E RID: 3662
	public short distant;
}
