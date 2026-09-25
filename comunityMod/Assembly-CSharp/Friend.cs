using System;

// Token: 0x0200003C RID: 60
public class Friend
{
	// Token: 0x060002E1 RID: 737 RVA: 0x0003467C File Offset: 0x0003287C
	public Friend(string friendName, sbyte type)
	{
		this.friendName = friendName;
		this.type = type;
	}

	// Token: 0x060002E2 RID: 738 RVA: 0x00034692 File Offset: 0x00032892
	public Friend(string friendName)
	{
		this.friendName = friendName;
		this.type = 2;
	}

	// Token: 0x0400060E RID: 1550
	public string friendName;

	// Token: 0x0400060F RID: 1551
	public sbyte type;
}
