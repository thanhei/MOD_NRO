using System;

// Token: 0x0200002D RID: 45
public class mLine
{
	// Token: 0x060001E7 RID: 487 RVA: 0x000051E0 File Offset: 0x000033E0
	public mLine(int x1, int y1, int x2, int y2, int cl)
	{
		this.x1 = x1;
		this.y1 = y1;
		this.x2 = x2;
		this.y2 = y2;
		this.setColor(cl);
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x000136BC File Offset: 0x000118BC
	public void setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = rgb >> 8 & 255;
		int num3 = rgb >> 16 & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = 255f;
	}

	// Token: 0x040001E0 RID: 480
	public int x1;

	// Token: 0x040001E1 RID: 481
	public int x2;

	// Token: 0x040001E2 RID: 482
	public int y1;

	// Token: 0x040001E3 RID: 483
	public int y2;

	// Token: 0x040001E4 RID: 484
	public float r;

	// Token: 0x040001E5 RID: 485
	public float b;

	// Token: 0x040001E6 RID: 486
	public float g;

	// Token: 0x040001E7 RID: 487
	public float a;
}
