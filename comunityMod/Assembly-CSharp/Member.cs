using System;

// Token: 0x02000069 RID: 105
public class Member
{
	// Token: 0x06000537 RID: 1335 RVA: 0x00052330 File Offset: 0x00050530
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

	// Token: 0x04000B03 RID: 2819
	public int ID;

	// Token: 0x04000B04 RID: 2820
	public short head;

	// Token: 0x04000B05 RID: 2821
	public short headICON = -1;

	// Token: 0x04000B06 RID: 2822
	public short leg;

	// Token: 0x04000B07 RID: 2823
	public short body;

	// Token: 0x04000B08 RID: 2824
	public string name;

	// Token: 0x04000B09 RID: 2825
	public sbyte role;

	// Token: 0x04000B0A RID: 2826
	public string powerPoint;

	// Token: 0x04000B0B RID: 2827
	public int donate;

	// Token: 0x04000B0C RID: 2828
	public int receive_donate;

	// Token: 0x04000B0D RID: 2829
	public int curClanPoint;

	// Token: 0x04000B0E RID: 2830
	public int clanPoint;

	// Token: 0x04000B0F RID: 2831
	public int lastRequest;

	// Token: 0x04000B10 RID: 2832
	public string joinTime;
}
