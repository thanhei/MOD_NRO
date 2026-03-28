using System;

// Token: 0x02000067 RID: 103
public class ItemTemplate
{
	// Token: 0x060003B2 RID: 946 RVA: 0x0002407C File Offset: 0x0002227C
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

	// Token: 0x04000641 RID: 1601
	public short id;

	// Token: 0x04000642 RID: 1602
	public sbyte type;

	// Token: 0x04000643 RID: 1603
	public sbyte gender;

	// Token: 0x04000644 RID: 1604
	public string name;

	// Token: 0x04000645 RID: 1605
	public string[] subName;

	// Token: 0x04000646 RID: 1606
	public string description;

	// Token: 0x04000647 RID: 1607
	public sbyte level;

	// Token: 0x04000648 RID: 1608
	public short iconID;

	// Token: 0x04000649 RID: 1609
	public short part;

	// Token: 0x0400064A RID: 1610
	public bool isUpToUp;

	// Token: 0x0400064B RID: 1611
	public int w;

	// Token: 0x0400064C RID: 1612
	public int h;

	// Token: 0x0400064D RID: 1613
	public int strRequire;
}
