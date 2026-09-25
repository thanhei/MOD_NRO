using System;

// Token: 0x02000060 RID: 96
public class Line
{
	// Token: 0x060004E9 RID: 1257 RVA: 0x0004EE59 File Offset: 0x0004D059
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

	// Token: 0x060004EA RID: 1258 RVA: 0x0004EE90 File Offset: 0x0004D090
	public void update()
	{
		this.x0 += this.vx;
		this.x1 += this.vx;
		this.y0 += this.vy;
		this.y1 += this.vy;
		this.f++;
	}

	// Token: 0x060004EB RID: 1259 RVA: 0x0004EEF8 File Offset: 0x0004D0F8
	public void update_not_F()
	{
		this.x0 += this.vx;
		this.x1 += this.vx;
		this.y0 += this.vy;
		this.y1 += this.vy;
	}

	// Token: 0x04000A4E RID: 2638
	public int x0;

	// Token: 0x04000A4F RID: 2639
	public int y0;

	// Token: 0x04000A50 RID: 2640
	public int x1;

	// Token: 0x04000A51 RID: 2641
	public int y1;

	// Token: 0x04000A52 RID: 2642
	public int vx;

	// Token: 0x04000A53 RID: 2643
	public int vy;

	// Token: 0x04000A54 RID: 2644
	public int f;

	// Token: 0x04000A55 RID: 2645
	public int fRe;

	// Token: 0x04000A56 RID: 2646
	public int idColor;

	// Token: 0x04000A57 RID: 2647
	public int type;

	// Token: 0x04000A58 RID: 2648
	public bool is2Line;

	// Token: 0x04000A59 RID: 2649
	public FrameImage fraImgEff;

	// Token: 0x04000A5A RID: 2650
	public int[] frame;
}
