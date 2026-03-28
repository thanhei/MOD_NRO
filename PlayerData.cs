using System;

// Token: 0x020000C0 RID: 192
public class PlayerData
{
	// Token: 0x0600099D RID: 2461 RVA: 0x0000856A File Offset: 0x0000676A
	public PlayerData(int playerID, string name, short head, short body, short leg, long ppoint)
	{
		this.playerID = playerID;
		this.name = name;
		this.head = head;
		this.body = body;
		this.leg = leg;
		this.powpoint = ppoint;
	}

	// Token: 0x0600099E RID: 2462 RVA: 0x0000859F File Offset: 0x0000679F
	public string getInfo()
	{
		return string.Concat(new object[]
		{
			this.name,
			"\n",
			mResources.power_point,
			" ",
			this.powpoint
		});
	}

	// Token: 0x040011ED RID: 4589
	public int playerID;

	// Token: 0x040011EE RID: 4590
	public string name;

	// Token: 0x040011EF RID: 4591
	public short head;

	// Token: 0x040011F0 RID: 4592
	public short body;

	// Token: 0x040011F1 RID: 4593
	public short leg;

	// Token: 0x040011F2 RID: 4594
	public long powpoint;
}
