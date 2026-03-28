using System;

// Token: 0x02000046 RID: 70
public class MainImage
{
	// Token: 0x060002AD RID: 685 RVA: 0x00005782 File Offset: 0x00003982
	public MainImage()
	{
	}

	// Token: 0x060002AE RID: 686 RVA: 0x000057A4 File Offset: 0x000039A4
	public MainImage(Image im, sbyte nFrame)
	{
		this.img = im;
		this.count = 0L;
		this.nFrame = nFrame;
	}

	// Token: 0x04000350 RID: 848
	public Image img;

	// Token: 0x04000351 RID: 849
	public long count = -1L;

	// Token: 0x04000352 RID: 850
	public int timeImageNull;

	// Token: 0x04000353 RID: 851
	public int idImage;

	// Token: 0x04000354 RID: 852
	public long timerequest;

	// Token: 0x04000355 RID: 853
	public sbyte nFrame = 1;

	// Token: 0x04000356 RID: 854
	public long timeUse = mSystem.currentTimeMillis();
}
