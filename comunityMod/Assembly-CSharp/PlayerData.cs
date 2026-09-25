using System;

// Token: 0x02000089 RID: 137
public class PlayerData
{
	// Token: 0x0600073E RID: 1854 RVA: 0x00073F79 File Offset: 0x00072179
	public PlayerData(int playerID, string name, short head, short body, short leg, long ppoint)
	{
		this.playerID = playerID;
		this.name = name;
		this.head = head;
		this.body = body;
		this.leg = leg;
		this.powpoint = ppoint;
	}

	// Token: 0x0600073F RID: 1855 RVA: 0x00073FAE File Offset: 0x000721AE
	public string getInfo()
	{
		return string.Concat(new string[]
		{
			this.name,
			"\n",
			mResources.power_point,
			" ",
			this.powpoint.ToString()
		});
	}

	// Token: 0x04000DD4 RID: 3540
	public int playerID;

	// Token: 0x04000DD5 RID: 3541
	public string name;

	// Token: 0x04000DD6 RID: 3542
	public short head;

	// Token: 0x04000DD7 RID: 3543
	public short body;

	// Token: 0x04000DD8 RID: 3544
	public short leg;

	// Token: 0x04000DD9 RID: 3545
	public long powpoint;
}
