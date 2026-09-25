using System;

// Token: 0x0200003F RID: 63
public class GamePad
{
	// Token: 0x06000341 RID: 833 RVA: 0x0003A290 File Offset: 0x00038490
	public GamePad()
	{
		this.R = 28;
		if (GameCanvas.w < 300)
		{
			this.isSmallGamePad = true;
			this.isMediumGamePad = false;
			this.isLargeGamePad = false;
		}
		if (GameCanvas.w >= 300 && GameCanvas.w <= 380)
		{
			this.isSmallGamePad = false;
			this.isMediumGamePad = true;
			this.isLargeGamePad = false;
		}
		if (GameCanvas.w > 380)
		{
			this.isSmallGamePad = false;
			this.isMediumGamePad = false;
			this.isLargeGamePad = true;
		}
		if (!this.isLargeGamePad)
		{
			this.xZone = 0;
			this.wZone = GameCanvas.hw;
			this.yZone = GameCanvas.hh >> 1;
			this.hZone = GameCanvas.h - 80;
			return;
		}
		this.xZone = 0;
		this.wZone = GameCanvas.hw / 4 * 3 - 20;
		this.yZone = GameCanvas.hh >> 1;
		this.hZone = GameCanvas.h;
		if (mSystem.clientType == 2)
		{
			this.xZone = 0;
			this.yZone = (GameCanvas.h >> 1) + 40;
			this.wZone = GameCanvas.hw / 4 * 3 - 40;
			this.hZone = GameCanvas.h;
		}
	}

	// Token: 0x06000342 RID: 834 RVA: 0x0003A3C4 File Offset: 0x000385C4
	public void update()
	{
		try
		{
			if (GameScr.isAnalog != 0)
			{
				if (GameCanvas.isPointerDown && !GameCanvas.isPointerJustRelease)
				{
					this.isResetKeyHold = true;
					this.xTemp = GameCanvas.pxFirst;
					this.yTemp = GameCanvas.pyFirst;
					if (this.xTemp >= this.xZone && this.xTemp <= this.wZone && this.yTemp >= this.yZone && this.yTemp <= this.hZone)
					{
						if (!this.isGamePad)
						{
							this.xC = (this.xM = this.xTemp);
							this.yC = (this.yM = this.yTemp);
						}
						this.isGamePad = true;
						this.deltaX = GameCanvas.px - this.xC;
						this.deltaY = GameCanvas.py - this.yC;
						this.delta = Math2.pow(this.deltaX, 2) + Math2.pow(this.deltaY, 2);
						this.d = Res.sqrt(this.delta);
						if (Math2.abs(this.deltaX) > 4 || Math2.abs(this.deltaY) > 4)
						{
							this.angle = Res.angle(this.deltaX, this.deltaY);
							if (!GameCanvas.isPointerHoldIn(this.xC - this.R, this.yC - this.R, 2 * this.R, 2 * this.R))
							{
								if (this.d != 0)
								{
									this.yM = this.deltaY * this.R / this.d;
									this.xM = this.deltaX * this.R / this.d;
									this.xM += this.xC;
									this.yM += this.yC;
									if (!Res.inRect(this.xC - this.R, this.yC - this.R, 2 * this.R, 2 * this.R, this.xM, this.yM))
									{
										this.xM = this.xMLast;
										this.yM = this.yMLast;
									}
									else
									{
										this.xMLast = this.xM;
										this.yMLast = this.yM;
									}
								}
								else
								{
									this.xM = this.xMLast;
									this.yM = this.yMLast;
								}
							}
							else
							{
								this.xM = GameCanvas.px;
								this.yM = GameCanvas.py;
							}
							this.resetHold();
							if (this.checkPointerMove(2))
							{
								if ((this.angle <= 360 && this.angle >= 340) || (this.angle >= 0 && this.angle <= 20))
								{
									GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
								}
								else if (this.angle > 40 && this.angle < 70)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
								}
								else if (this.angle >= 70 && this.angle <= 110)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 8 : 22] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = true;
								}
								else if (this.angle > 110 && this.angle < 120)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
								}
								else if (this.angle >= 120 && this.angle <= 200)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
								}
								else if (this.angle > 200 && this.angle < 250)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
									GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = true;
								}
								else if (this.angle >= 250 && this.angle <= 290)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
								}
								else if (this.angle > 290 && this.angle < 340)
								{
									GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] = true;
									GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = true;
									GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = true;
								}
							}
							else
							{
								this.resetHold();
							}
						}
					}
				}
				else
				{
					this.xM = (this.xC = 45);
					if (!this.isLargeGamePad)
					{
						this.yM = (this.yC = GameCanvas.h - 90);
					}
					else
					{
						this.yM = (this.yC = GameCanvas.h - 45);
					}
					this.isGamePad = false;
					if (this.isResetKeyHold)
					{
						this.resetHold();
						this.isResetKeyHold = false;
					}
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000343 RID: 835 RVA: 0x0003A960 File Offset: 0x00038B60
	internal bool checkPointerMove(int distance)
	{
		if (GameScr.isAnalog == 0)
		{
			return false;
		}
		if (global::Char.myCharz().statusMe == 3)
		{
			return true;
		}
		try
		{
			for (int i = 2; i > 0; i--)
			{
				int num = GameCanvas.arrPos[i].x - GameCanvas.arrPos[i - 1].x;
				int num2 = GameCanvas.arrPos[i].y - GameCanvas.arrPos[i - 1].y;
				if (Res.abs(num) > distance && Res.abs(num2) > distance)
				{
					return false;
				}
			}
		}
		catch (Exception)
		{
		}
		return true;
	}

	// Token: 0x06000344 RID: 836 RVA: 0x0003A9F8 File Offset: 0x00038BF8
	internal void resetHold()
	{
		GameCanvas.clearKeyHold();
	}

	// Token: 0x06000345 RID: 837 RVA: 0x0003AA00 File Offset: 0x00038C00
	public void paint(mGraphics g)
	{
		if (GameScr.isAnalog != 0)
		{
			this.xZone = 0;
			this.yZone = (GameCanvas.h >> 1) + 40;
			this.wZone = GameCanvas.hw / 4 * 3 - 40;
			this.hZone = GameCanvas.h;
			g.drawImage(GameScr.imgAnalog1, this.xC, this.yC, mGraphics.HCENTER | mGraphics.VCENTER);
			g.drawImage(GameScr.imgAnalog2, this.xM, this.yM, mGraphics.HCENTER | mGraphics.VCENTER);
		}
	}

	// Token: 0x06000346 RID: 838 RVA: 0x0003AA8C File Offset: 0x00038C8C
	public bool disableCheckDrag()
	{
		return GameScr.isAnalog != 0 && this.isGamePad;
	}

	// Token: 0x06000347 RID: 839 RVA: 0x0003AAA0 File Offset: 0x00038CA0
	public bool disableClickMove()
	{
		bool flag;
		try
		{
			if (GameScr.isAnalog == 0)
			{
				flag = false;
			}
			else
			{
				flag = (GameCanvas.px >= this.xZone && GameCanvas.px <= this.xZone + this.wZone && GameCanvas.py >= this.yZone && GameCanvas.py <= this.yZone + this.hZone) || (GameCanvas.px >= this.xZone && GameCanvas.px <= GameCanvas.w && GameCanvas.py >= this.yZone && GameCanvas.py <= this.yZone + this.hZone) || GameCanvas.px >= GameCanvas.w - 50;
			}
		}
		catch (Exception)
		{
			flag = false;
		}
		return flag;
	}

	// Token: 0x040006D9 RID: 1753
	private bool isResetKeyHold = true;

	// Token: 0x040006DA RID: 1754
	internal int xC;

	// Token: 0x040006DB RID: 1755
	internal int yC;

	// Token: 0x040006DC RID: 1756
	internal int xM;

	// Token: 0x040006DD RID: 1757
	internal int yM;

	// Token: 0x040006DE RID: 1758
	internal int xMLast;

	// Token: 0x040006DF RID: 1759
	internal int yMLast;

	// Token: 0x040006E0 RID: 1760
	internal int R;

	// Token: 0x040006E1 RID: 1761
	internal int r;

	// Token: 0x040006E2 RID: 1762
	internal int d;

	// Token: 0x040006E3 RID: 1763
	internal int xTemp;

	// Token: 0x040006E4 RID: 1764
	internal int yTemp;

	// Token: 0x040006E5 RID: 1765
	internal int deltaX;

	// Token: 0x040006E6 RID: 1766
	internal int deltaY;

	// Token: 0x040006E7 RID: 1767
	internal int delta;

	// Token: 0x040006E8 RID: 1768
	internal int angle;

	// Token: 0x040006E9 RID: 1769
	public int xZone;

	// Token: 0x040006EA RID: 1770
	public int yZone;

	// Token: 0x040006EB RID: 1771
	public int wZone;

	// Token: 0x040006EC RID: 1772
	public int hZone;

	// Token: 0x040006ED RID: 1773
	internal bool isGamePad;

	// Token: 0x040006EE RID: 1774
	public bool isSmallGamePad;

	// Token: 0x040006EF RID: 1775
	public bool isMediumGamePad;

	// Token: 0x040006F0 RID: 1776
	public bool isLargeGamePad;
}
