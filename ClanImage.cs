using System;

// Token: 0x02000031 RID: 49
public class ClanImage
{
	// Token: 0x0600021F RID: 543 RVA: 0x0000536D File Offset: 0x0000356D
	public static void addClanImage(ClanImage cm)
	{
		Service.gI().clanImage((sbyte)cm.ID);
		ClanImage.vClanImage.addElement(cm);
	}

	// Token: 0x06000220 RID: 544 RVA: 0x00013D68 File Offset: 0x00011F68
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

	// Token: 0x06000221 RID: 545 RVA: 0x00013DB0 File Offset: 0x00011FB0
	public static bool isExistClanImage(int ID)
	{
		for (int i = 0; i < ClanImage.vClanImage.size(); i++)
		{
			ClanImage clanImage = (ClanImage)ClanImage.vClanImage.elementAt(i);
			if (clanImage.ID == ID)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x040001FC RID: 508
	public int ID;

	// Token: 0x040001FD RID: 509
	public string name;

	// Token: 0x040001FE RID: 510
	public short[] idImage;

	// Token: 0x040001FF RID: 511
	public int xu;

	// Token: 0x04000200 RID: 512
	public int luong;

	// Token: 0x04000201 RID: 513
	public static MyVector vClanImage = new MyVector();

	// Token: 0x04000202 RID: 514
	public static MyHashTable idImages = new MyHashTable();
}
