using System;

// Token: 0x020000C4 RID: 196
public class mLine
{
	// Token: 0x06000A32 RID: 2610 RVA: 0x00090CD9 File Offset: 0x0008EED9
	public mLine(int x1, int y1, int x2, int y2, int cl)
	{
		this.x1 = x1;
		this.y1 = y1;
		this.x2 = x2;
		this.y2 = y2;
		this.setColor(cl);
	}

	// Token: 0x06000A33 RID: 2611 RVA: 0x00090D08 File Offset: 0x0008EF08
	public void setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = (rgb >> 8) & 255;
		int num3 = (rgb >> 16) & 255;
		this.b = (float)num / 256f;
		this.g = (float)num2 / 256f;
		this.r = (float)num3 / 256f;
		this.a = 255f;
	}

	// Token: 0x0400120B RID: 4619
	public int x1;

	// Token: 0x0400120C RID: 4620
	public int x2;

	// Token: 0x0400120D RID: 4621
	public int y1;

	// Token: 0x0400120E RID: 4622
	public int y2;

	// Token: 0x0400120F RID: 4623
	public float r;

	// Token: 0x04001210 RID: 4624
	public float b;

	// Token: 0x04001211 RID: 4625
	public float g;

	// Token: 0x04001212 RID: 4626
	public float a;
}
