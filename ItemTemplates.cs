using System;

// Token: 0x02000068 RID: 104
public class ItemTemplates
{
	// Token: 0x060003B4 RID: 948 RVA: 0x00005E99 File Offset: 0x00004099
	public static void add(ItemTemplate it)
	{
		ItemTemplates.itemTemplates.put(it.id, it);
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00005EB1 File Offset: 0x000040B1
	public static ItemTemplate get(short id)
	{
		return (ItemTemplate)ItemTemplates.itemTemplates.get(id);
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00005EC8 File Offset: 0x000040C8
	public static short getPart(short itemTemplateID)
	{
		return ItemTemplates.get(itemTemplateID).part;
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00005ED5 File Offset: 0x000040D5
	public static short getIcon(short itemTemplateID)
	{
		return ItemTemplates.get(itemTemplateID).iconID;
	}

	// Token: 0x0400064E RID: 1614
	public static MyHashTable itemTemplates = new MyHashTable();
}
