using System;

// Token: 0x0200005C RID: 92
public class Friend
{
	// Token: 0x06000343 RID: 835 RVA: 0x00005AF9 File Offset: 0x00003CF9
	public Friend(string friendName, sbyte type)
	{
		this.friendName = friendName;
		this.type = type;
	}

	// Token: 0x06000344 RID: 836 RVA: 0x00005B0F File Offset: 0x00003D0F
	public Friend(string friendName)
	{
		this.friendName = friendName;
		this.type = 2;
	}

	// Token: 0x0400056E RID: 1390
	public string friendName;

	// Token: 0x0400056F RID: 1391
	public sbyte type;
}
