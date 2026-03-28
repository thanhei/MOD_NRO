using System;

// Token: 0x0200006A RID: 106
public class Key
{
	// Token: 0x060003C6 RID: 966 RVA: 0x00005EFE File Offset: 0x000040FE
	public static void mapKeyPC()
	{
		if (!Main.isPC)
		{
			return;
		}
		Key.UP = 15;
		Key.DOWN = 16;
		Key.LEFT = 17;
		Key.RIGHT = 18;
	}

	// Token: 0x0400065B RID: 1627
	public static int NUM0;

	// Token: 0x0400065C RID: 1628
	public static int NUM1 = 1;

	// Token: 0x0400065D RID: 1629
	public static int NUM2 = 2;

	// Token: 0x0400065E RID: 1630
	public static int NUM3 = 3;

	// Token: 0x0400065F RID: 1631
	public static int NUM4 = 4;

	// Token: 0x04000660 RID: 1632
	public static int NUM5 = 5;

	// Token: 0x04000661 RID: 1633
	public static int NUM6 = 6;

	// Token: 0x04000662 RID: 1634
	public static int NUM7 = 7;

	// Token: 0x04000663 RID: 1635
	public static int NUM8 = 8;

	// Token: 0x04000664 RID: 1636
	public static int NUM9 = 9;

	// Token: 0x04000665 RID: 1637
	public static int STAR = 10;

	// Token: 0x04000666 RID: 1638
	public static int BOUND = 11;

	// Token: 0x04000667 RID: 1639
	public static int UP = 12;

	// Token: 0x04000668 RID: 1640
	public static int DOWN = 13;

	// Token: 0x04000669 RID: 1641
	public static int LEFT = 14;

	// Token: 0x0400066A RID: 1642
	public static int RIGHT = 15;

	// Token: 0x0400066B RID: 1643
	public static int FIRE = 16;

	// Token: 0x0400066C RID: 1644
	public static int LEFT_SOFTKEY = 17;

	// Token: 0x0400066D RID: 1645
	public static int RIGHT_SOFTKEY = 18;

	// Token: 0x0400066E RID: 1646
	public static int CLEAR = 19;

	// Token: 0x0400066F RID: 1647
	public static int BACK = 20;
}
