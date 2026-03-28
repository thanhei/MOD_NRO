using System;

// Token: 0x0200007D RID: 125
public class Position
{
	// Token: 0x0600043D RID: 1085 RVA: 0x0000644E File Offset: 0x0000464E
	public Position()
	{
		this.x = 0;
		this.y = 0;
	}

	// Token: 0x0600043E RID: 1086 RVA: 0x00006464 File Offset: 0x00004664
	public Position(int x, int y, int anchor)
	{
		this.x = x;
		this.y = y;
		this.anchor = anchor;
	}

	// Token: 0x0600043F RID: 1087 RVA: 0x00006481 File Offset: 0x00004681
	public Position(int x, int y)
	{
		this.x = x;
		this.y = y;
	}

	// Token: 0x06000440 RID: 1088 RVA: 0x00006497 File Offset: 0x00004697
	public void setPosTo(int xT, int yT)
	{
		this.xTo = (short)xT;
		this.yTo = (short)yT;
		this.distant = (short)Res.distance(this.x, this.y, (int)this.xTo, (int)this.yTo);
	}

	// Token: 0x06000441 RID: 1089 RVA: 0x00028928 File Offset: 0x00026B28
	public int translate()
	{
		if (this.x == (int)this.xTo && this.y == (int)this.yTo)
		{
			return -1;
		}
		if (global::Math.abs(((int)this.xTo - this.x) / 2) <= 1 && global::Math.abs(((int)this.yTo - this.y) / 2) <= 1)
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

	// Token: 0x06000442 RID: 1090 RVA: 0x000064CD File Offset: 0x000046CD
	public void update()
	{
		this.layer.update();
	}

	// Token: 0x06000443 RID: 1091 RVA: 0x000064DA File Offset: 0x000046DA
	public void paint(mGraphics g)
	{
		this.layer.paint(g, this.x, this.y);
	}

	// Token: 0x04000777 RID: 1911
	public int x;

	// Token: 0x04000778 RID: 1912
	public int y;

	// Token: 0x04000779 RID: 1913
	public int anchor;

	// Token: 0x0400077A RID: 1914
	public int g;

	// Token: 0x0400077B RID: 1915
	public int v;

	// Token: 0x0400077C RID: 1916
	public int w;

	// Token: 0x0400077D RID: 1917
	public int h;

	// Token: 0x0400077E RID: 1918
	public int color;

	// Token: 0x0400077F RID: 1919
	public int limitY;

	// Token: 0x04000780 RID: 1920
	public Layer layer;

	// Token: 0x04000781 RID: 1921
	public short yTo;

	// Token: 0x04000782 RID: 1922
	public short xTo;

	// Token: 0x04000783 RID: 1923
	public short distant;
}
