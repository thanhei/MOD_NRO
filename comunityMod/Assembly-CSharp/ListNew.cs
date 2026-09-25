using System;

// Token: 0x02000061 RID: 97
public class ListNew
{
	// Token: 0x060004ED RID: 1261 RVA: 0x0004EF51 File Offset: 0x0004D151
	public ListNew()
	{
	}

	// Token: 0x060004EE RID: 1262 RVA: 0x0004EF68 File Offset: 0x0004D168
	public ListNew(int x, int y, int maxW, int maxH, int itemH, int maxSize, int limX, bool isLim0)
	{
		this.x = x;
		this.y = y;
		this.maxW = maxW;
		this.maxH = maxH;
		this.itemH = itemH;
		this.maxSize = maxSize;
		this.cmxLim = limX;
		if (isLim0 && this.cmxLim < 0)
		{
			this.cmxLim = 0;
		}
	}

	// Token: 0x060004EF RID: 1263 RVA: 0x0004EFD0 File Offset: 0x0004D1D0
	public void setMaxCamera(int limX)
	{
		if (limX < 0)
		{
			limX = 0;
		}
		this.cmxLim = limX;
		if (this.cmx > this.cmxLim)
		{
			this.cmx = this.cmxLim;
		}
		if (this.cmtoX > this.cmxLim)
		{
			this.cmtoX = this.cmxLim;
		}
	}

	// Token: 0x060004F0 RID: 1264 RVA: 0x0004F01F File Offset: 0x0004D21F
	public void updatePos(int x, int y, int maxW, int maxH)
	{
		this.x = x;
		this.y = y;
		this.maxW = maxW;
		this.maxH = maxH;
	}

	// Token: 0x060004F1 RID: 1265 RVA: 0x0004F040 File Offset: 0x0004D240
	public void updateMenuKey()
	{
		bool flag = false;
		if (GameCanvas.keyPressed[2])
		{
			flag = true;
			this.value--;
			if (this.value < 0)
			{
				this.value = this.maxSize - 1;
			}
			GameCanvas.clearKeyPressed();
		}
		else if (GameCanvas.keyPressed[8])
		{
			flag = true;
			this.value++;
			if (this.value > this.maxSize - 1)
			{
				this.value = this.maxSize - 1;
			}
			GameCanvas.clearKeyPressed();
		}
		if (flag)
		{
			this.cmtoX = (this.value + 1) * this.itemH - this.maxH / 2;
			if (this.cmtoX > this.cmxLim)
			{
				this.cmtoX = this.cmxLim;
			}
			if (this.cmtoX < 0)
			{
				this.cmtoX = 0;
			}
			if (this.value == this.maxSize - 1 || this.value == 0)
			{
				this.cmx = this.cmtoX;
			}
		}
		this.update_Pos_UP_DOWN();
	}

	// Token: 0x060004F2 RID: 1266 RVA: 0x0004F138 File Offset: 0x0004D338
	public void update_Pos_UP_DOWN()
	{
		if (this.cmxLim <= 0)
		{
			return;
		}
		if (GameCanvas.isPointerDown)
		{
			if (!this.pointerIsDowning && GameCanvas.isPointer(this.x, this.y, this.maxW, this.maxH))
			{
				for (int i = 0; i < this.pointerDownLastX.Length; i++)
				{
					this.pointerDownLastX[i] = GameCanvas.py;
				}
				this.pointerDownFirstX = GameCanvas.py;
				this.pointerIsDowning = true;
				this.isDownWhenRunning = this.cmRun != 0;
				this.cmRun = 0;
			}
			else if (this.pointerIsDowning)
			{
				this.pointerDownTime++;
				if (this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.py && !this.isDownWhenRunning)
				{
					this.pointerDownFirstX = -1000;
				}
				int num = GameCanvas.py - this.pointerDownLastX[0];
				if (num != 0 && this.value != -1)
				{
					this.value = -1;
				}
				for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
				{
					this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
				}
				this.pointerDownLastX[0] = GameCanvas.py;
				this.cmtoX -= num;
				if (this.cmtoX < 0)
				{
					this.cmtoX = 0;
				}
				if (this.cmtoX > this.cmxLim)
				{
					this.cmtoX = this.cmxLim;
				}
				if (this.cmx < 0 || this.cmx > this.cmxLim)
				{
					num /= 2;
				}
				this.cmx -= num;
			}
		}
		if (GameCanvas.isPointerClick && this.pointerIsDowning)
		{
			int num2 = GameCanvas.py - this.pointerDownLastX[0];
			GameCanvas.isPointerClick = false;
			if (Res.abs(num2) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning && GameCanvas.isPointerSelect)
			{
				this.cmRun = 0;
				this.cmtoX = this.cmx;
				this.pointerDownFirstX = -1000;
				this.pointerDownTime = 0;
			}
			else if (this.value != -1 && this.pointerDownTime > 5)
			{
				this.pointerDownTime = 0;
			}
			else if (this.value == -1 && !this.isDownWhenRunning)
			{
				if (this.cmx < 0)
				{
					this.cmtoX = 0;
				}
				else if (this.cmx > this.cmxLim)
				{
					this.cmtoX = this.cmxLim;
				}
				else
				{
					int num3 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
					this.cmRun = -((num3 > 10) ? 10 : ((num3 < -10) ? (-10) : 0)) * 100;
				}
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerClick = false;
		}
		if (this.cmx <= 0)
		{
			this.cmx = 0;
		}
		if (this.cmx >= this.cmxLim)
		{
			this.cmx = this.cmxLim;
		}
	}

	// Token: 0x060004F3 RID: 1267 RVA: 0x0004F434 File Offset: 0x0004D634
	public void updatePos_LEFT_RIGHT()
	{
		if (GameCanvas.isPointerDown)
		{
			if (!this.pointerIsDowning && GameCanvas.isPointer(this.x, this.y, this.maxW, this.maxH))
			{
				for (int i = 0; i < this.pointerDownLastX.Length; i++)
				{
					this.pointerDownLastX[0] = GameCanvas.px;
				}
				this.pointerDownFirstX = GameCanvas.px;
				this.pointerIsDowning = true;
				this.isDownWhenRunning = this.cmRun != 0;
				this.cmRun = 0;
			}
			else if (this.pointerIsDowning)
			{
				this.pointerDownTime++;
				if (this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.px && !this.isDownWhenRunning)
				{
					this.pointerDownFirstX = -1000;
				}
				int num = GameCanvas.px - this.pointerDownLastX[0];
				if (num != 0 && this.value != -1)
				{
					this.value = -1;
				}
				for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
				{
					this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
				}
				this.pointerDownLastX[0] = GameCanvas.px;
				this.cmtoX -= num;
				if (this.cmtoX < 0)
				{
					this.cmtoX = 0;
				}
				if (this.cmtoX > this.cmxLim)
				{
					this.cmtoX = this.cmxLim;
				}
				if (this.cmx < 0 || this.cmx > this.cmxLim)
				{
					num /= 2;
				}
				this.cmx -= num;
			}
		}
		if (!GameCanvas.isPointerClick || !this.pointerIsDowning)
		{
			return;
		}
		int num2 = GameCanvas.px - this.pointerDownLastX[0];
		GameCanvas.isPointerClick = false;
		if (Res.abs(num2) < 20 && Res.abs(GameCanvas.px - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning && GameCanvas.isPointerSelect)
		{
			this.cmRun = 0;
			this.cmtoX = this.cmx;
			this.pointerDownFirstX = -1000;
			this.pointerDownTime = 0;
		}
		else if (this.value != -1 && this.pointerDownTime > 5)
		{
			this.pointerDownTime = 0;
		}
		else if (this.value == -1 && !this.isDownWhenRunning)
		{
			if (this.cmx < 0)
			{
				this.cmtoX = 0;
			}
			else if (this.cmx > this.cmxLim)
			{
				this.cmtoX = this.cmxLim;
			}
			else
			{
				int num3 = GameCanvas.px - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
				this.cmRun = -((num3 > 10) ? 10 : ((num3 < -10) ? (-10) : 0)) * 100;
			}
		}
		this.pointerIsDowning = false;
		this.pointerDownTime = 0;
		GameCanvas.isPointerClick = false;
	}

	// Token: 0x060004F4 RID: 1268 RVA: 0x0004F6F8 File Offset: 0x0004D8F8
	public void moveCamera()
	{
		if (this.cmRun != 0 && !this.pointerIsDowning)
		{
			this.cmtoX += this.cmRun / 100;
			if (this.cmtoX < 0)
			{
				this.cmtoX = 0;
			}
			else if (this.cmtoX > this.cmxLim)
			{
				this.cmtoX = this.cmxLim;
			}
			else
			{
				this.cmx = this.cmtoX;
			}
			this.cmRun = this.cmRun * 9 / 10;
			if (this.cmRun < 100 && this.cmRun > -100)
			{
				this.cmRun = 0;
			}
		}
		if (this.cmx != this.cmtoX && !this.pointerIsDowning)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 4;
			this.cmdx &= 15;
		}
	}

	// Token: 0x060004F5 RID: 1269 RVA: 0x0004F7F7 File Offset: 0x0004D9F7
	public void updateMenu()
	{
		this.moveCamera();
		this.updateMenuKey();
	}

	// Token: 0x060004F6 RID: 1270 RVA: 0x0004F805 File Offset: 0x0004DA05
	public void setToX(int value)
	{
		if (value < 0)
		{
			value = 0;
		}
		if (value > this.cmxLim)
		{
			value = this.cmxLim;
		}
		this.cmtoX = value;
	}

	// Token: 0x060004F7 RID: 1271 RVA: 0x0004F826 File Offset: 0x0004DA26
	public void resetList()
	{
		this.cmtoX = 0;
	}

	// Token: 0x04000A5B RID: 2651
	public int maxW;

	// Token: 0x04000A5C RID: 2652
	public int itemH;

	// Token: 0x04000A5D RID: 2653
	public int maxH;

	// Token: 0x04000A5E RID: 2654
	public int maxSize;

	// Token: 0x04000A5F RID: 2655
	public int x;

	// Token: 0x04000A60 RID: 2656
	public int y;

	// Token: 0x04000A61 RID: 2657
	public int value;

	// Token: 0x04000A62 RID: 2658
	public int cmtoX;

	// Token: 0x04000A63 RID: 2659
	public int cmx;

	// Token: 0x04000A64 RID: 2660
	public int cmdy;

	// Token: 0x04000A65 RID: 2661
	public int cmvy;

	// Token: 0x04000A66 RID: 2662
	public int cmxLim;

	// Token: 0x04000A67 RID: 2663
	internal int pointerDownTime;

	// Token: 0x04000A68 RID: 2664
	internal int pointerDownFirstX;

	// Token: 0x04000A69 RID: 2665
	internal int[] pointerDownLastX = new int[3];

	// Token: 0x04000A6A RID: 2666
	public bool pointerIsDowning;

	// Token: 0x04000A6B RID: 2667
	public bool isDownWhenRunning;

	// Token: 0x04000A6C RID: 2668
	internal int cmRun;

	// Token: 0x04000A6D RID: 2669
	internal MyVector vecCmd;

	// Token: 0x04000A6E RID: 2670
	public int w;

	// Token: 0x04000A6F RID: 2671
	internal int cmvx;

	// Token: 0x04000A70 RID: 2672
	internal int cmdx;
}
