using System;

// Token: 0x02000066 RID: 102
public class MainImage
{
	// Token: 0x0600052E RID: 1326 RVA: 0x0005223B File Offset: 0x0005043B
	public MainImage()
	{
	}

	// Token: 0x0600052F RID: 1327 RVA: 0x0005225D File Offset: 0x0005045D
	public MainImage(Image im, sbyte nFrame)
	{
		this.img = im;
		this.count = 0L;
		this.nFrame = nFrame;
	}

	// Token: 0x04000AF3 RID: 2803
	public Image img;

	// Token: 0x04000AF4 RID: 2804
	public long count = -1L;

	// Token: 0x04000AF5 RID: 2805
	public int timeImageNull;

	// Token: 0x04000AF6 RID: 2806
	public int idImage;

	// Token: 0x04000AF7 RID: 2807
	public long timerequest;

	// Token: 0x04000AF8 RID: 2808
	public sbyte nFrame = 1;

	// Token: 0x04000AF9 RID: 2809
	public long timeUse = mSystem.currentTimeMillis();
}
