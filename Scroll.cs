using System;

// Token: 0x0200007E RID: 126
public class Scroll
{
	// Token: 0x06000445 RID: 1093 RVA: 0x00028A30 File Offset: 0x00026C30
	public void clear()
	{
		this.cmtoX = 0;
		this.cmtoY = 0;
		this.cmx = 0;
		this.cmy = 0;
		this.cmvx = 0;
		this.cmvy = 0;
		this.cmdx = 0;
		this.cmdy = 0;
		this.cmxLim = 0;
		this.cmyLim = 0;
		this.width = 0;
		this.height = 0;
	}

	// Token: 0x06000446 RID: 1094 RVA: 0x0000650F File Offset: 0x0000470F
	public ScrollResult updateKey()
	{
		if (this.styleUPDOWN)
		{
			return this.updateKeyScrollUpDown(false);
		}
		return this.updateKeyScrollLeftRight();
	}

	// Token: 0x06000447 RID: 1095 RVA: 0x0000652A File Offset: 0x0000472A
	public ScrollResult updateKey(bool isGetSelectNow)
	{
		if (this.styleUPDOWN)
		{
			return this.updateKeyScrollUpDown(isGetSelectNow);
		}
		return this.updateKeyScrollLeftRight();
	}

	// Token: 0x06000448 RID: 1096 RVA: 0x00028A94 File Offset: 0x00026C94
	private ScrollResult updateKeyScrollUpDown(bool isGetNow)
	{
		int num = this.xPos;
		int num2 = this.yPos;
		int w = this.width;
		int h = this.height;
		if (GameCanvas.isPointerDown)
		{
			if (!this.pointerIsDowning && GameCanvas.isPointer(num, num2, w, h))
			{
				for (int i = 0; i < this.pointerDownLastX.Length; i++)
				{
					this.pointerDownLastX[0] = GameCanvas.py;
				}
				this.pointerDownFirstX = GameCanvas.py;
				this.pointerIsDowning = true;
				if (!isGetNow)
				{
					this.selectedItem = -1;
				}
				this.isDownWhenRunning = (this.cmRun != 0);
				this.cmRun = 0;
			}
			else if (this.pointerIsDowning)
			{
				this.pointerDownTime++;
				if (this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.py && !this.isDownWhenRunning)
				{
					this.pointerDownFirstX = -1000;
					if (this.ITEM_PER_LINE > 1)
					{
						int num3 = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
						int num4 = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
						this.selectedItem = num3 * this.ITEM_PER_LINE + num4;
					}
					else
					{
						this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
					}
				}
				int num5 = GameCanvas.py - this.pointerDownLastX[0];
				if (!isGetNow)
				{
					if (num5 != 0 && this.selectedItem != -1)
					{
						this.selectedItem = -1;
					}
				}
				else
				{
					this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
				}
				for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
				{
					this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
				}
				this.pointerDownLastX[0] = GameCanvas.py;
				this.cmtoY -= num5;
				if (this.cmtoY < 0)
				{
					this.cmtoY = 0;
				}
				if (this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				if (this.cmy < 0 || this.cmy > this.cmyLim)
				{
					num5 /= 2;
				}
				this.cmy -= num5;
			}
		}
		bool isFinish = false;
		if (GameCanvas.isPointerJustRelease && this.pointerIsDowning)
		{
			int i2 = GameCanvas.py - this.pointerDownLastX[0];
			GameCanvas.isPointerJustRelease = false;
			if (Res.abs(i2) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
			{
				this.cmRun = 0;
				this.cmtoY = this.cmy;
				this.pointerDownFirstX = -1000;
				if (this.ITEM_PER_LINE > 1)
				{
					int num6 = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
					int num7 = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
					this.selectedItem = num6 * this.ITEM_PER_LINE + num7;
				}
				else
				{
					this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
				}
				this.pointerDownTime = 0;
				isFinish = true;
			}
			else if (this.selectedItem != -1 && this.pointerDownTime > 5)
			{
				this.pointerDownTime = 0;
				isFinish = true;
			}
			else if ((this.selectedItem == -1 && !this.isDownWhenRunning) || (isGetNow && this.selectedItem != -1 && !this.isDownWhenRunning))
			{
				if (this.cmy < 0)
				{
					this.cmtoY = 0;
				}
				else if (this.cmy > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				else
				{
					int num8 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
					if (num8 > 10)
					{
						num8 = 10;
					}
					else if (num8 < -10)
					{
						num8 = -10;
					}
					else
					{
						num8 = 0;
					}
					this.cmRun = -num8 * 100;
				}
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerJustRelease = false;
		}
		return new ScrollResult
		{
			selected = this.selectedItem,
			isFinish = isFinish,
			isDowning = this.pointerIsDowning
		};
	}

	// Token: 0x06000449 RID: 1097 RVA: 0x00028F34 File Offset: 0x00027134
	private ScrollResult updateKeyScrollLeftRight()
	{
		int num = this.xPos;
		int y = this.yPos;
		int w = this.width;
		int h = this.height;
		if (GameCanvas.isPointerDown)
		{
			if (!this.pointerIsDowning && GameCanvas.isPointer(num, y, w, h))
			{
				for (int i = 0; i < this.pointerDownLastX.Length; i++)
				{
					this.pointerDownLastX[0] = GameCanvas.px;
				}
				this.pointerDownFirstX = GameCanvas.px;
				this.pointerIsDowning = true;
				this.selectedItem = -1;
				this.isDownWhenRunning = (this.cmRun != 0);
				this.cmRun = 0;
			}
			else if (this.pointerIsDowning)
			{
				this.pointerDownTime++;
				if (this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.px && !this.isDownWhenRunning)
				{
					this.pointerDownFirstX = -1000;
					this.selectedItem = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
				}
				int num2 = GameCanvas.px - this.pointerDownLastX[0];
				if (num2 != 0 && this.selectedItem != -1)
				{
					this.selectedItem = -1;
				}
				for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
				{
					this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
				}
				this.pointerDownLastX[0] = GameCanvas.px;
				this.cmtoX -= num2;
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
					num2 /= 2;
				}
				this.cmx -= num2;
			}
		}
		bool isFinish = false;
		if (GameCanvas.isPointerJustRelease && this.pointerIsDowning)
		{
			int i2 = GameCanvas.px - this.pointerDownLastX[0];
			GameCanvas.isPointerJustRelease = false;
			if (Res.abs(i2) < 20 && Res.abs(GameCanvas.px - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
			{
				this.cmRun = 0;
				this.cmtoX = this.cmx;
				this.pointerDownFirstX = -1000;
				this.selectedItem = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
				this.pointerDownTime = 0;
				isFinish = true;
			}
			else if (this.selectedItem != -1 && this.pointerDownTime > 5)
			{
				this.pointerDownTime = 0;
				isFinish = true;
			}
			else if (this.selectedItem == -1 && !this.isDownWhenRunning)
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
					if (num3 > 10)
					{
						num3 = 10;
					}
					else if (num3 < -10)
					{
						num3 = -10;
					}
					else
					{
						num3 = 0;
					}
					this.cmRun = -num3 * 100;
				}
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerJustRelease = false;
		}
		return new ScrollResult
		{
			selected = this.selectedItem,
			isFinish = isFinish,
			isDowning = this.pointerIsDowning
		};
	}

	// Token: 0x0600044A RID: 1098 RVA: 0x000292E8 File Offset: 0x000274E8
	public void updatecm()
	{
		if (this.cmRun != 0 && !this.pointerIsDowning)
		{
			if (this.styleUPDOWN)
			{
				this.cmtoY += this.cmRun / 100;
				if (this.cmtoY < 0)
				{
					this.cmtoY = 0;
				}
				else if (this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				else
				{
					this.cmy = this.cmtoY;
				}
			}
			else
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
		if (this.cmy != this.cmtoY && !this.pointerIsDowning)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
	}

	// Token: 0x0600044B RID: 1099 RVA: 0x000294D8 File Offset: 0x000276D8
	public void setStyle(int nItem, int ITEM_SIZE, int xPos, int yPos, int width, int height, bool styleUPDOWN, int ITEM_PER_LINE)
	{
		this.xPos = xPos;
		this.yPos = yPos;
		this.ITEM_SIZE = ITEM_SIZE;
		this.nITEM = nItem;
		this.width = width;
		this.height = height;
		this.styleUPDOWN = styleUPDOWN;
		this.ITEM_PER_LINE = ITEM_PER_LINE;
		Res.outz(string.Concat(new object[]
		{
			"nItem= ",
			nItem,
			" ITEMSIZE= ",
			ITEM_SIZE,
			" heghit= ",
			height
		}));
		if (styleUPDOWN)
		{
			int num = nItem / ITEM_PER_LINE;
			if (nItem % ITEM_PER_LINE != 0)
			{
				num++;
			}
			this.cmyLim = num * ITEM_SIZE - height;
		}
		else
		{
			this.cmxLim = ITEM_PER_LINE * ITEM_SIZE - width;
		}
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmxLim < 0)
		{
			this.cmxLim = 0;
		}
	}

	// Token: 0x0600044C RID: 1100 RVA: 0x000295C4 File Offset: 0x000277C4
	public void moveTo(int to)
	{
		if (this.styleUPDOWN)
		{
			to -= (this.height - this.ITEM_SIZE) / 2;
			this.cmtoY = to;
			if (this.cmtoY < 0)
			{
				this.cmtoY = 0;
			}
			if (this.cmtoY > this.cmyLim)
			{
				this.cmtoY = this.cmyLim;
			}
		}
		else
		{
			to -= (this.width - this.ITEM_SIZE) / 2;
			this.cmtoX = to;
			if (this.cmtoX < 0)
			{
				this.cmtoX = 0;
			}
			if (this.cmtoX > this.cmxLim)
			{
				this.cmtoX = this.cmxLim;
			}
		}
	}

	// Token: 0x0600044D RID: 1101 RVA: 0x00006545 File Offset: 0x00004745
	public static Scroll gIz()
	{
		if (Scroll.gI == null)
		{
			Scroll.gI = new Scroll();
		}
		return Scroll.gI;
	}

	// Token: 0x04000784 RID: 1924
	public int cmtoX;

	// Token: 0x04000785 RID: 1925
	public int cmtoY;

	// Token: 0x04000786 RID: 1926
	public int cmx;

	// Token: 0x04000787 RID: 1927
	public int cmy;

	// Token: 0x04000788 RID: 1928
	public int cmvx;

	// Token: 0x04000789 RID: 1929
	public int cmvy;

	// Token: 0x0400078A RID: 1930
	public int cmdx;

	// Token: 0x0400078B RID: 1931
	public int cmdy;

	// Token: 0x0400078C RID: 1932
	public int xPos;

	// Token: 0x0400078D RID: 1933
	public int yPos;

	// Token: 0x0400078E RID: 1934
	public int width;

	// Token: 0x0400078F RID: 1935
	public int height;

	// Token: 0x04000790 RID: 1936
	public int cmxLim;

	// Token: 0x04000791 RID: 1937
	public int cmyLim;

	// Token: 0x04000792 RID: 1938
	public static Scroll gI;

	// Token: 0x04000793 RID: 1939
	private int pointerDownTime;

	// Token: 0x04000794 RID: 1940
	private int pointerDownFirstX;

	// Token: 0x04000795 RID: 1941
	private int[] pointerDownLastX = new int[3];

	// Token: 0x04000796 RID: 1942
	public bool pointerIsDowning;

	// Token: 0x04000797 RID: 1943
	public bool isDownWhenRunning;

	// Token: 0x04000798 RID: 1944
	private int cmRun;

	// Token: 0x04000799 RID: 1945
	public int selectedItem;

	// Token: 0x0400079A RID: 1946
	public int ITEM_SIZE;

	// Token: 0x0400079B RID: 1947
	public int nITEM;

	// Token: 0x0400079C RID: 1948
	public int ITEM_PER_LINE;

	// Token: 0x0400079D RID: 1949
	public bool styleUPDOWN = true;
}
