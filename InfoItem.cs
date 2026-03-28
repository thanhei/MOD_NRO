using System;

// Token: 0x020000AF RID: 175
public class InfoItem
{
	// Token: 0x060007E6 RID: 2022 RVA: 0x00007B18 File Offset: 0x00005D18
	public InfoItem(string s)
	{
		this.f = mFont.tahoma_7_green2;
		this.s = s;
		this.speed = 20;
	}

	// Token: 0x060007E7 RID: 2023 RVA: 0x00007B42 File Offset: 0x00005D42
	public InfoItem(string s, mFont f, int speed)
	{
		this.f = f;
		this.s = s;
		this.speed = speed;
	}

	// Token: 0x04000F11 RID: 3857
	public string s;

	// Token: 0x04000F12 RID: 3858
	private mFont f;

	// Token: 0x04000F13 RID: 3859
	public int speed = 70;

	// Token: 0x04000F14 RID: 3860
	public global::Char charInfo;

	// Token: 0x04000F15 RID: 3861
	public bool isChatServer;

	// Token: 0x04000F16 RID: 3862
	public bool isOnline;

	// Token: 0x04000F17 RID: 3863
	public int timeCount;

	// Token: 0x04000F18 RID: 3864
	public int maxTime;

	// Token: 0x04000F19 RID: 3865
	public long last;

	// Token: 0x04000F1A RID: 3866
	public long curr;
}
