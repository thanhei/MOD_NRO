using System;

// Token: 0x020000AC RID: 172
public interface IMapObject
{
	// Token: 0x060007D2 RID: 2002
	int getX();

	// Token: 0x060007D3 RID: 2003
	int getY();

	// Token: 0x060007D4 RID: 2004
	int getW();

	// Token: 0x060007D5 RID: 2005
	int getH();

	// Token: 0x060007D6 RID: 2006
	void stopMoving();

	// Token: 0x060007D7 RID: 2007
	bool isInvisible();
}
