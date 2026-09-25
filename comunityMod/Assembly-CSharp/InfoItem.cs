using System;

// Token: 0x0200004F RID: 79
public class InfoItem
{
	// Token: 0x06000478 RID: 1144 RVA: 0x0004BF4D File Offset: 0x0004A14D
	public InfoItem(string s)
	{
		this.f = mFont.tahoma_7_green2;
		this.s = s;
		this.speed = 20;
	}

	// Token: 0x06000479 RID: 1145 RVA: 0x0004BF77 File Offset: 0x0004A177
	public InfoItem(string s, mFont f, int speed)
	{
		this.f = f;
		this.s = s;
		this.speed = speed;
	}

	// Token: 0x0400092B RID: 2347
	public string s;

	// Token: 0x0400092C RID: 2348
	internal mFont f;

	// Token: 0x0400092D RID: 2349
	public int speed = 70;

	// Token: 0x0400092E RID: 2350
	public global::Char charInfo;

	// Token: 0x0400092F RID: 2351
	public bool isChatServer;

	// Token: 0x04000930 RID: 2352
	public bool isOnline;

	// Token: 0x04000931 RID: 2353
	public int timeCount;

	// Token: 0x04000932 RID: 2354
	public int maxTime;

	// Token: 0x04000933 RID: 2355
	public long last;

	// Token: 0x04000934 RID: 2356
	public long curr;
}
