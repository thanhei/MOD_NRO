using System;

// Token: 0x0200006E RID: 110
public class MobCapcha
{
	// Token: 0x060003D4 RID: 980 RVA: 0x00005F89 File Offset: 0x00004189
	public static void init()
	{
		MobCapcha.imgMob = GameCanvas.loadImage("/mainImage/myTexture2dmobCapcha.png");
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x00024DD8 File Offset: 0x00022FD8
	public static void paint(mGraphics g, int x, int y)
	{
		if (!MobCapcha.isAttack)
		{
			if (GameCanvas.gameTick % 3 == 0)
			{
				if (global::Char.myCharz().cdir == 1)
				{
					MobCapcha.cmtoX = x - 20 - GameScr.cmx;
				}
				if (global::Char.myCharz().cdir == -1)
				{
					MobCapcha.cmtoX = x + 20 - GameScr.cmx;
				}
			}
			MobCapcha.cmtoY = global::Char.myCharz().cy - 40 - GameScr.cmy;
		}
		else
		{
			MobCapcha.delay++;
			if (MobCapcha.delay == 5)
			{
				MobCapcha.isAttack = false;
				MobCapcha.delay = 0;
			}
			MobCapcha.cmtoX = x - GameScr.cmx;
			MobCapcha.cmtoY = y - GameScr.cmy;
		}
		if (MobCapcha.cmx > x - GameScr.cmx)
		{
			MobCapcha.dir = -1;
		}
		else
		{
			MobCapcha.dir = 1;
		}
		g.drawImage(GameScr.imgCapcha, MobCapcha.cmx, MobCapcha.cmy - 40, 3);
		PopUp.paintPopUp(g, MobCapcha.cmx - 25, MobCapcha.cmy - 70, 50, 20, 16777215, false);
		mFont.tahoma_7b_dark.drawString(g, GameScr.gI().keyInput, MobCapcha.cmx, MobCapcha.cmy - 65, 2);
		if (MobCapcha.isCreateMob)
		{
			MobCapcha.isCreateMob = false;
			EffecMn.addEff(new Effect(18, MobCapcha.cmx + GameScr.cmx, MobCapcha.cmy + GameScr.cmy, 2, 10, -1));
		}
		if (MobCapcha.explode)
		{
			MobCapcha.explode = false;
			EffecMn.addEff(new Effect(18, MobCapcha.cmx + GameScr.cmx, MobCapcha.cmy + GameScr.cmy, 2, 10, -1));
			GameScr.gI().mobCapcha = null;
			MobCapcha.cmtoX = -GameScr.cmx;
			MobCapcha.cmtoY = -GameScr.cmy;
		}
		g.drawRegion(MobCapcha.imgMob, 0, MobCapcha.f * 40, 40, 40, (MobCapcha.dir != 1) ? 2 : 0, MobCapcha.cmx, MobCapcha.cmy + 3 + ((GameCanvas.gameTick % 10 <= 5) ? 0 : 1), 3);
		MobCapcha.moveCamera();
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00024FF0 File Offset: 0x000231F0
	public static void moveCamera()
	{
		if (MobCapcha.cmy != MobCapcha.cmtoY)
		{
			MobCapcha.cmvy = MobCapcha.cmtoY - MobCapcha.cmy << 2;
			MobCapcha.cmdy += MobCapcha.cmvy;
			MobCapcha.cmy += MobCapcha.cmdy >> 4;
			MobCapcha.cmdy &= 15;
		}
		if (MobCapcha.cmx != MobCapcha.cmtoX)
		{
			MobCapcha.cmvx = MobCapcha.cmtoX - MobCapcha.cmx << 2;
			MobCapcha.cmdx += MobCapcha.cmvx;
			MobCapcha.cmx += MobCapcha.cmdx >> 4;
			MobCapcha.cmdx &= 15;
		}
		MobCapcha.tF++;
		if (MobCapcha.tF == 5)
		{
			MobCapcha.tF = 0;
			MobCapcha.f++;
			if (MobCapcha.f > 2)
			{
				MobCapcha.f = 0;
			}
		}
	}

	// Token: 0x0400069F RID: 1695
	public static Image imgMob;

	// Token: 0x040006A0 RID: 1696
	public static int cmtoY;

	// Token: 0x040006A1 RID: 1697
	public static int cmy;

	// Token: 0x040006A2 RID: 1698
	public static int cmdy;

	// Token: 0x040006A3 RID: 1699
	public static int cmvy;

	// Token: 0x040006A4 RID: 1700
	public static int cmyLim;

	// Token: 0x040006A5 RID: 1701
	public static int cmtoX;

	// Token: 0x040006A6 RID: 1702
	public static int cmx;

	// Token: 0x040006A7 RID: 1703
	public static int cmdx;

	// Token: 0x040006A8 RID: 1704
	public static int cmvx;

	// Token: 0x040006A9 RID: 1705
	public static int cmxLim;

	// Token: 0x040006AA RID: 1706
	public static bool explode;

	// Token: 0x040006AB RID: 1707
	public static int delay;

	// Token: 0x040006AC RID: 1708
	public static bool isCreateMob;

	// Token: 0x040006AD RID: 1709
	public static int tF;

	// Token: 0x040006AE RID: 1710
	public static int f;

	// Token: 0x040006AF RID: 1711
	public static int dir;

	// Token: 0x040006B0 RID: 1712
	public static bool isAttack;
}
