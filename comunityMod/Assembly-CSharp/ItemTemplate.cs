using System;

// Token: 0x0200005A RID: 90
public class ItemTemplate
{
	// Token: 0x060004CC RID: 1228 RVA: 0x0004E75C File Offset: 0x0004C95C
	public ItemTemplate(short templateID, sbyte type, sbyte gender, string name, string description, sbyte level, int strRequire, short iconID, short part, bool isUpToUp)
	{
		this.id = templateID;
		this.type = type;
		this.gender = gender;
		this.name = name;
		this.name = Res.changeString(this.name);
		this.description = description;
		this.description = Res.changeString(this.description);
		this.level = level;
		this.strRequire = strRequire;
		this.iconID = iconID;
		this.part = part;
		this.isUpToUp = isUpToUp;
	}

	// Token: 0x04000A07 RID: 2567
	public short id;

	// Token: 0x04000A08 RID: 2568
	public sbyte type;

	// Token: 0x04000A09 RID: 2569
	public sbyte gender;

	// Token: 0x04000A0A RID: 2570
	public string name;

	// Token: 0x04000A0B RID: 2571
	public string[] subName;

	// Token: 0x04000A0C RID: 2572
	public string description;

	// Token: 0x04000A0D RID: 2573
	public sbyte level;

	// Token: 0x04000A0E RID: 2574
	public short iconID;

	// Token: 0x04000A0F RID: 2575
	public short part;

	// Token: 0x04000A10 RID: 2576
	public bool isUpToUp;

	// Token: 0x04000A11 RID: 2577
	public int w;

	// Token: 0x04000A12 RID: 2578
	public int h;

	// Token: 0x04000A13 RID: 2579
	public int strRequire;
}
