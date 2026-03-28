using System;

// Token: 0x020000D0 RID: 208
public class GamePad
{
	// Token: 0x06000AD6 RID: 2774 RVA: 0x000A1700 File Offset: 0x0009F900
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
		}
		else
		{
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
	}

	// Token: 0x06000AD7 RID: 2775 RVA: 0x000A1844 File Offset: 0x0009FA44
	public void update()
	{
		try
		{
			if (GameScr.isAnalog != 0)
			{
				if (GameCanvas.isPointerDown && !GameCanvas.isPointerJustRelease)
				{
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
						this.delta = global::Math.pow(this.deltaX, 2) + global::Math.pow(this.deltaY, 2);
						this.d = Res.sqrt(this.delta);
						if (global::Math.abs(this.deltaX) > 4 || global::Math.abs(this.deltaY) > 4)
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
					this.resetHold();
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x06000AD8 RID: 2776 RVA: 0x000A1EA0 File Offset: 0x000A00A0
	private bool checkPointerMove(int distance)
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
				int i2 = GameCanvas.arrPos[i].x - GameCanvas.arrPos[i - 1].x;
				int i3 = GameCanvas.arrPos[i].y - GameCanvas.arrPos[i - 1].y;
				if (Res.abs(i2) > distance && Res.abs(i3) > distance)
				{
					return false;
				}
			}
		}
		catch (Exception ex)
		{
		}
		return true;
	}

	// Token: 0x06000AD9 RID: 2777 RVA: 0x0000905B File Offset: 0x0000725B
	private void resetHold()
	{
		GameCanvas.clearKeyHold();
	}

	// Token: 0x06000ADA RID: 2778 RVA: 0x000A1F54 File Offset: 0x000A0154
	public void paint(mGraphics g)
	{
		if (GameScr.isAnalog == 0)
		{
			return;
		}
		this.xZone = 0;
		this.yZone = (GameCanvas.h >> 1) + 40;
		this.wZone = GameCanvas.hw / 4 * 3 - 40;
		this.hZone = GameCanvas.h;
		g.drawImage(GameScr.imgAnalog1, this.xC, this.yC, mGraphics.HCENTER | mGraphics.VCENTER);
		g.drawImage(GameScr.imgAnalog2, this.xM, this.yM, mGraphics.HCENTER | mGraphics.VCENTER);
	}

	// Token: 0x06000ADB RID: 2779 RVA: 0x00009062 File Offset: 0x00007262
	public bool disableCheckDrag()
	{
		return GameScr.isAnalog != 0 && this.isGamePad;
	}

	// Token: 0x06000ADC RID: 2780 RVA: 0x000A1FE4 File Offset: 0x000A01E4
	public bool disableClickMove()
	{
		bool result;
		try
		{
			if (GameScr.isAnalog == 0)
			{
				result = false;
			}
			else
			{
				bool flag = (GameCanvas.px >= this.xZone && GameCanvas.px <= this.xZone + this.wZone && GameCanvas.py >= this.yZone && GameCanvas.py <= this.yZone + this.hZone) || (GameCanvas.px >= this.xZone && GameCanvas.px <= GameCanvas.w && GameCanvas.py >= this.yZone && GameCanvas.py <= this.yZone + this.hZone) || GameCanvas.px >= GameCanvas.w - 50;
				result = flag;
			}
		}
		catch (Exception ex)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x0400148F RID: 5263
	private int xC;

	// Token: 0x04001490 RID: 5264
	private int yC;

	// Token: 0x04001491 RID: 5265
	private int xM;

	// Token: 0x04001492 RID: 5266
	private int yM;

	// Token: 0x04001493 RID: 5267
	private int xMLast;

	// Token: 0x04001494 RID: 5268
	private int yMLast;

	// Token: 0x04001495 RID: 5269
	private int R;

	// Token: 0x04001496 RID: 5270
	private int r;

	// Token: 0x04001497 RID: 5271
	private int d;

	// Token: 0x04001498 RID: 5272
	private int xTemp;

	// Token: 0x04001499 RID: 5273
	private int yTemp;

	// Token: 0x0400149A RID: 5274
	private int deltaX;

	// Token: 0x0400149B RID: 5275
	private int deltaY;

	// Token: 0x0400149C RID: 5276
	private int delta;

	// Token: 0x0400149D RID: 5277
	private int angle;

	// Token: 0x0400149E RID: 5278
	public int xZone;

	// Token: 0x0400149F RID: 5279
	public int yZone;

	// Token: 0x040014A0 RID: 5280
	public int wZone;

	// Token: 0x040014A1 RID: 5281
	public int hZone;

	// Token: 0x040014A2 RID: 5282
	private bool isGamePad;

	// Token: 0x040014A3 RID: 5283
	public bool isSmallGamePad;

	// Token: 0x040014A4 RID: 5284
	public bool isMediumGamePad;

	// Token: 0x040014A5 RID: 5285
	public bool isLargeGamePad;
}
