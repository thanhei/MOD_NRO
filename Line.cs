using System;

// Token: 0x0200006B RID: 107
public class Line
{
	// Token: 0x060003C9 RID: 969 RVA: 0x00005F27 File Offset: 0x00004127
	public void setLine(int x0, int y0, int x1, int y1, int vx, int vy, bool is2Line)
	{
		this.x0 = x0;
		this.y0 = y0;
		this.x1 = x1;
		this.y1 = y1;
		this.vx = vx;
		this.vy = vy;
		this.is2Line = is2Line;
	}

	// Token: 0x060003CA RID: 970 RVA: 0x00024678 File Offset: 0x00022878
	public void update()
	{
		this.x0 += this.vx;
		this.x1 += this.vx;
		this.y0 += this.vy;
		this.y1 += this.vy;
		this.f++;
	}

	// Token: 0x060003CB RID: 971 RVA: 0x000246E0 File Offset: 0x000228E0
	public void update_not_F()
	{
		this.x0 += this.vx;
		this.x1 += this.vx;
		this.y0 += this.vy;
		this.y1 += this.vy;
	}

	// Token: 0x04000670 RID: 1648
	public int x0;

	// Token: 0x04000671 RID: 1649
	public int y0;

	// Token: 0x04000672 RID: 1650
	public int x1;

	// Token: 0x04000673 RID: 1651
	public int y1;

	// Token: 0x04000674 RID: 1652
	public int vx;

	// Token: 0x04000675 RID: 1653
	public int vy;

	// Token: 0x04000676 RID: 1654
	public int f;

	// Token: 0x04000677 RID: 1655
	public int fRe;

	// Token: 0x04000678 RID: 1656
	public int idColor;

	// Token: 0x04000679 RID: 1657
	public int type;

	// Token: 0x0400067A RID: 1658
	public bool is2Line;

	// Token: 0x0400067B RID: 1659
	public FrameImage fraImgEff;

	// Token: 0x0400067C RID: 1660
	public int[] frame;
}
