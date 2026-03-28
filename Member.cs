using System;

// Token: 0x02000035 RID: 53
public class Member
{
	// Token: 0x0600022C RID: 556 RVA: 0x000053E5 File Offset: 0x000035E5
	public static string getRole(int r)
	{
		if (r == 0)
		{
			return mResources.clan_leader;
		}
		if (r == 1)
		{
			return mResources.clan_coleader;
		}
		if (r == 2)
		{
			return mResources.member;
		}
		return string.Empty;
	}

	// Token: 0x04000214 RID: 532
	public int ID;

	// Token: 0x04000215 RID: 533
	public short head;

	// Token: 0x04000216 RID: 534
	public short headICON = -1;

	// Token: 0x04000217 RID: 535
	public short leg;

	// Token: 0x04000218 RID: 536
	public short body;

	// Token: 0x04000219 RID: 537
	public string name;

	// Token: 0x0400021A RID: 538
	public sbyte role;

	// Token: 0x0400021B RID: 539
	public string powerPoint;

	// Token: 0x0400021C RID: 540
	public int donate;

	// Token: 0x0400021D RID: 541
	public int receive_donate;

	// Token: 0x0400021E RID: 542
	public int curClanPoint;

	// Token: 0x0400021F RID: 543
	public int clanPoint;

	// Token: 0x04000220 RID: 544
	public int lastRequest;

	// Token: 0x04000221 RID: 545
	public string joinTime;
}
