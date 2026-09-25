using System;

// Token: 0x02000093 RID: 147
public class Scroll
{
	// Token: 0x060007BE RID: 1982 RVA: 0x000775D0 File Offset: 0x000757D0
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

	// Token: 0x060007BF RID: 1983 RVA: 0x00077631 File Offset: 0x00075831
	public ScrollResult updateKey()
	{
		if (this.styleUPDOWN)
		{
			return this.updateKeyScrollUpDown(false);
		}
		return this.updateKeyScrollLeftRight();
	}

	// Token: 0x060007C0 RID: 1984 RVA: 0x00077649 File Offset: 0x00075849
	public ScrollResult updateKey(bool isGetSelectNow)
	{
		if (this.styleUPDOWN)
		{
			return this.updateKeyScrollUpDown(isGetSelectNow);
		}
		return this.updateKeyScrollLeftRight();
	}

	// Token: 0x060007C1 RID: 1985 RVA: 0x00077664 File Offset: 0x00075864
	internal ScrollResult updateKeyScrollUpDown(bool isGetNow)
	{
		int num = this.xPos;
		int num2 = this.yPos;
		int num3 = this.width;
		int num4 = this.height;
		if (GameCanvas.isPointerDown)
		{
			if (!this.pointerIsDowning && GameCanvas.isPointer(num, num2, num3, num4))
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
				this.isDownWhenRunning = this.cmRun != 0;
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
						int num5 = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
						int num6 = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
						this.selectedItem = num5 * this.ITEM_PER_LINE + num6;
					}
					else
					{
						this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
					}
				}
				int num7 = GameCanvas.py - this.pointerDownLastX[0];
				if (!isGetNow)
				{
					if (num7 != 0 && this.selectedItem != -1)
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
				this.cmtoY -= num7;
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
					num7 /= 2;
				}
				this.cmy -= num7;
			}
		}
		bool flag = false;
		if (GameCanvas.isPointerJustRelease && this.pointerIsDowning)
		{
			int num8 = GameCanvas.py - this.pointerDownLastX[0];
			GameCanvas.isPointerJustRelease = false;
			if (Res.abs(num8) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
			{
				this.cmRun = 0;
				this.cmtoY = this.cmy;
				this.pointerDownFirstX = -1000;
				if (this.ITEM_PER_LINE > 1)
				{
					int num9 = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
					int num10 = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
					this.selectedItem = num9 * this.ITEM_PER_LINE + num10;
				}
				else
				{
					this.selectedItem = (this.cmtoY + GameCanvas.py - num2) / this.ITEM_SIZE;
				}
				this.pointerDownTime = 0;
				flag = true;
			}
			else if (this.selectedItem != -1 && this.pointerDownTime > 5)
			{
				this.pointerDownTime = 0;
				flag = true;
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
					int num11 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
					this.cmRun = -((num11 > 10) ? 10 : ((num11 < -10) ? (-10) : 0)) * 100;
				}
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerJustRelease = false;
		}
		return new ScrollResult
		{
			selected = this.selectedItem,
			isFinish = flag,
			isDowning = this.pointerIsDowning
		};
	}

	// Token: 0x060007C2 RID: 1986 RVA: 0x00077A8C File Offset: 0x00075C8C
	internal ScrollResult updateKeyScrollLeftRight()
	{
		int num = this.xPos;
		int num2 = this.yPos;
		int num3 = this.width;
		int num4 = this.height;
		if (GameCanvas.isPointerDown)
		{
			if (!this.pointerIsDowning && GameCanvas.isPointer(num, num2, num3, num4))
			{
				for (int i = 0; i < this.pointerDownLastX.Length; i++)
				{
					this.pointerDownLastX[0] = GameCanvas.px;
				}
				this.pointerDownFirstX = GameCanvas.px;
				this.pointerIsDowning = true;
				this.selectedItem = -1;
				this.isDownWhenRunning = this.cmRun != 0;
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
				int num5 = GameCanvas.px - this.pointerDownLastX[0];
				if (num5 != 0 && this.selectedItem != -1)
				{
					this.selectedItem = -1;
				}
				for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
				{
					this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
				}
				this.pointerDownLastX[0] = GameCanvas.px;
				this.cmtoX -= num5;
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
					num5 /= 2;
				}
				this.cmx -= num5;
			}
		}
		bool flag = false;
		if (GameCanvas.isPointerJustRelease && this.pointerIsDowning)
		{
			int num6 = GameCanvas.px - this.pointerDownLastX[0];
			GameCanvas.isPointerJustRelease = false;
			if (Res.abs(num6) < 20 && Res.abs(GameCanvas.px - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
			{
				this.cmRun = 0;
				this.cmtoX = this.cmx;
				this.pointerDownFirstX = -1000;
				this.selectedItem = (this.cmtoX + GameCanvas.px - num) / this.ITEM_SIZE;
				this.pointerDownTime = 0;
				flag = true;
			}
			else if (this.selectedItem != -1 && this.pointerDownTime > 5)
			{
				this.pointerDownTime = 0;
				flag = true;
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
					int num7 = GameCanvas.px - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
					this.cmRun = -((num7 > 10) ? 10 : ((num7 < -10) ? (-10) : 0)) * 100;
				}
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerJustRelease = false;
		}
		return new ScrollResult
		{
			selected = this.selectedItem,
			isFinish = flag,
			isDowning = this.pointerIsDowning
		};
	}

	// Token: 0x060007C3 RID: 1987 RVA: 0x00077DD8 File Offset: 0x00075FD8
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

	// Token: 0x060007C4 RID: 1988 RVA: 0x00077F98 File Offset: 0x00076198
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
		Res.outz(string.Concat(new string[]
		{
			"nItem= ",
			nItem.ToString(),
			" ITEMSIZE= ",
			ITEM_SIZE.ToString(),
			" heghit= ",
			height.ToString()
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

	// Token: 0x060007C5 RID: 1989 RVA: 0x00078078 File Offset: 0x00076278
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
				return;
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

	// Token: 0x060007C6 RID: 1990 RVA: 0x00078116 File Offset: 0x00076316
	public static Scroll gIz()
	{
		if (Scroll.gI == null)
		{
			Scroll.gI = new Scroll();
		}
		return Scroll.gI;
	}

	// Token: 0x04000E9B RID: 3739
	public int cmtoX;

	// Token: 0x04000E9C RID: 3740
	public int cmtoY;

	// Token: 0x04000E9D RID: 3741
	public int cmx;

	// Token: 0x04000E9E RID: 3742
	public int cmy;

	// Token: 0x04000E9F RID: 3743
	public int cmvx;

	// Token: 0x04000EA0 RID: 3744
	public int cmvy;

	// Token: 0x04000EA1 RID: 3745
	public int cmdx;

	// Token: 0x04000EA2 RID: 3746
	public int cmdy;

	// Token: 0x04000EA3 RID: 3747
	public int xPos;

	// Token: 0x04000EA4 RID: 3748
	public int yPos;

	// Token: 0x04000EA5 RID: 3749
	public int width;

	// Token: 0x04000EA6 RID: 3750
	public int height;

	// Token: 0x04000EA7 RID: 3751
	public int cmxLim;

	// Token: 0x04000EA8 RID: 3752
	public int cmyLim;

	// Token: 0x04000EA9 RID: 3753
	public static Scroll gI;

	// Token: 0x04000EAA RID: 3754
	internal int pointerDownTime;

	// Token: 0x04000EAB RID: 3755
	internal int pointerDownFirstX;

	// Token: 0x04000EAC RID: 3756
	internal int[] pointerDownLastX = new int[3];

	// Token: 0x04000EAD RID: 3757
	public bool pointerIsDowning;

	// Token: 0x04000EAE RID: 3758
	public bool isDownWhenRunning;

	// Token: 0x04000EAF RID: 3759
	internal int cmRun;

	// Token: 0x04000EB0 RID: 3760
	public int selectedItem;

	// Token: 0x04000EB1 RID: 3761
	public int ITEM_SIZE;

	// Token: 0x04000EB2 RID: 3762
	public int nITEM;

	// Token: 0x04000EB3 RID: 3763
	public int ITEM_PER_LINE;

	// Token: 0x04000EB4 RID: 3764
	public bool styleUPDOWN = true;
}
