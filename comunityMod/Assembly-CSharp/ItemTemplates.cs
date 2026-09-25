using System;

// Token: 0x0200005B RID: 91
public class ItemTemplates
{
	// Token: 0x060004CD RID: 1229 RVA: 0x0004E7DE File Offset: 0x0004C9DE
	public static void add(ItemTemplate it)
	{
		ItemTemplates.itemTemplates.put(it.id, it);
	}

	// Token: 0x060004CE RID: 1230 RVA: 0x0004E7F6 File Offset: 0x0004C9F6
	public static ItemTemplate get(short id)
	{
		return (ItemTemplate)ItemTemplates.itemTemplates.get(id);
	}

	// Token: 0x060004CF RID: 1231 RVA: 0x0004E80D File Offset: 0x0004CA0D
	public static short getPart(short itemTemplateID)
	{
		return ItemTemplates.get(itemTemplateID).part;
	}

	// Token: 0x060004D0 RID: 1232 RVA: 0x0004E81A File Offset: 0x0004CA1A
	public static short getIcon(short itemTemplateID)
	{
		return ItemTemplates.get(itemTemplateID).iconID;
	}

	// Token: 0x04000A14 RID: 2580
	public static MyHashTable itemTemplates = new MyHashTable();
}
