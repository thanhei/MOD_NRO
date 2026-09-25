using System;

// Token: 0x02000046 RID: 70
public interface IMapObject
{
	// Token: 0x06000409 RID: 1033
	int getX();

	// Token: 0x0600040A RID: 1034
	int getY();

	// Token: 0x0600040B RID: 1035
	int getW();

	// Token: 0x0600040C RID: 1036
	int getH();

	// Token: 0x0600040D RID: 1037
	void stopMoving();

	// Token: 0x0600040E RID: 1038
	bool isInvisible();
}
