using System;

// Token: 0x0200001B RID: 27
public class ClanImage
{
	// Token: 0x060001C1 RID: 449 RVA: 0x0001A278 File Offset: 0x00018478
	public static void addClanImage(ClanImage cm)
	{
		Service.gI().clanImage((sbyte)cm.ID);
		ClanImage.vClanImage.addElement(cm);
	}

	// Token: 0x060001C2 RID: 450 RVA: 0x0001A298 File Offset: 0x00018498
	public static ClanImage getClanImage(short ID)
	{
		for (int i = 0; i < ClanImage.vClanImage.size(); i++)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(i);
			if (clanImage.ID == (int)ID)
			{
				return clanImage;
			}
		}
		return null;
	}

	// Token: 0x060001C3 RID: 451 RVA: 0x0001A2D8 File Offset: 0x000184D8
	public static bool isExistClanImage(int ID)
	{
		for (int i = 0; i < ClanImage.vClanImage.size(); i++)
		{
			if (((ClanImage)ClanImage.vClanImage.elementAt(i)).ID == ID)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0400033D RID: 829
	public int ID;

	// Token: 0x0400033E RID: 830
	public string name;

	// Token: 0x0400033F RID: 831
	public short[] idImage;

	// Token: 0x04000340 RID: 832
	public int xu;

	// Token: 0x04000341 RID: 833
	public int luong;

	// Token: 0x04000342 RID: 834
	public static MyVector vClanImage = new MyVector();

	// Token: 0x04000343 RID: 835
	public static MyHashTable idImages = new MyHashTable();
}
