using System;

// Token: 0x0200006D RID: 109
public class MobCapcha
{
	// Token: 0x06000580 RID: 1408 RVA: 0x00055D3C File Offset: 0x00053F3C
	public static void init()
	{
		MobCapcha.imgMob = GameCanvas.loadImage("/mainImage/myTexture2dmobCapcha.png");
	}

	// Token: 0x06000581 RID: 1409 RVA: 0x00055D50 File Offset: 0x00053F50
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
		g.drawRegion(MobCapcha.imgMob, 0, MobCapcha.f * 40, 40, 40, (MobCapcha.dir != 1) ? 2 : 0, MobCapcha.cmx, MobCapcha.cmy + 3 + ((GameCanvas.gameTick % 10 > 5) ? 1 : 0), 3);
		MobCapcha.moveCamera();
	}

	// Token: 0x06000582 RID: 1410 RVA: 0x00055F3C File Offset: 0x0005413C
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

	// Token: 0x04000BAC RID: 2988
	public static Image imgMob;

	// Token: 0x04000BAD RID: 2989
	public static int cmtoY;

	// Token: 0x04000BAE RID: 2990
	public static int cmy;

	// Token: 0x04000BAF RID: 2991
	public static int cmdy;

	// Token: 0x04000BB0 RID: 2992
	public static int cmvy;

	// Token: 0x04000BB1 RID: 2993
	public static int cmyLim;

	// Token: 0x04000BB2 RID: 2994
	public static int cmtoX;

	// Token: 0x04000BB3 RID: 2995
	public static int cmx;

	// Token: 0x04000BB4 RID: 2996
	public static int cmdx;

	// Token: 0x04000BB5 RID: 2997
	public static int cmvx;

	// Token: 0x04000BB6 RID: 2998
	public static int cmxLim;

	// Token: 0x04000BB7 RID: 2999
	public static bool explode;

	// Token: 0x04000BB8 RID: 3000
	public static int delay;

	// Token: 0x04000BB9 RID: 3001
	public static bool isCreateMob;

	// Token: 0x04000BBA RID: 3002
	public static int tF;

	// Token: 0x04000BBB RID: 3003
	public static int f;

	// Token: 0x04000BBC RID: 3004
	public static int dir;

	// Token: 0x04000BBD RID: 3005
	public static bool isAttack;
}
