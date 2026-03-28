using System;

// Token: 0x02000045 RID: 69
public class ListNew
{
	// Token: 0x060002A2 RID: 674 RVA: 0x00005711 File Offset: 0x00003911
	public ListNew()
	{
	}

	// Token: 0x060002A3 RID: 675 RVA: 0x0001A36C File Offset: 0x0001856C
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

	// Token: 0x060002A4 RID: 676 RVA: 0x0001A3DC File Offset: 0x000185DC
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

	// Token: 0x060002A5 RID: 677 RVA: 0x00005725 File Offset: 0x00003925
	public void updatePos(int x, int y, int maxW, int maxH)
	{
		this.x = x;
		this.y = y;
		this.maxW = maxW;
		this.maxH = maxH;
	}

	// Token: 0x060002A6 RID: 678 RVA: 0x0001A434 File Offset: 0x00018634
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

	// Token: 0x060002A7 RID: 679 RVA: 0x0001A54C File Offset: 0x0001874C
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
				this.isDownWhenRunning = (this.cmRun != 0);
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
			int i2 = GameCanvas.py - this.pointerDownLastX[0];
			GameCanvas.isPointerClick = false;
			if (Res.abs(i2) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning && GameCanvas.isPointerSelect)
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
					int num2 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
					if (num2 > 10)
					{
						num2 = 10;
					}
					else if (num2 < -10)
					{
						num2 = -10;
					}
					else
					{
						num2 = 0;
					}
					this.cmRun = -num2 * 100;
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

	// Token: 0x060002A8 RID: 680 RVA: 0x0001A8BC File Offset: 0x00018ABC
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
				this.isDownWhenRunning = (this.cmRun != 0);
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
		if (GameCanvas.isPointerClick && this.pointerIsDowning)
		{
			int i2 = GameCanvas.px - this.pointerDownLastX[0];
			GameCanvas.isPointerClick = false;
			if (Res.abs(i2) < 20 && Res.abs(GameCanvas.px - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning && GameCanvas.isPointerSelect)
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
					int num2 = GameCanvas.px - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
					if (num2 > 10)
					{
						num2 = 10;
					}
					else if (num2 < -10)
					{
						num2 = -10;
					}
					else
					{
						num2 = 0;
					}
					this.cmRun = -num2 * 100;
				}
			}
			this.pointerIsDowning = false;
			this.pointerDownTime = 0;
			GameCanvas.isPointerClick = false;
		}
	}

	// Token: 0x060002A9 RID: 681 RVA: 0x0001ABF0 File Offset: 0x00018DF0
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

	// Token: 0x060002AA RID: 682 RVA: 0x00005744 File Offset: 0x00003944
	public void updateMenu()
	{
		this.moveCamera();
		this.updateMenuKey();
	}

	// Token: 0x060002AB RID: 683 RVA: 0x00005752 File Offset: 0x00003952
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

	// Token: 0x060002AC RID: 684 RVA: 0x00005779 File Offset: 0x00003979
	public void resetList()
	{
		this.cmtoX = 0;
	}

	// Token: 0x0400033A RID: 826
	public int maxW;

	// Token: 0x0400033B RID: 827
	public int itemH;

	// Token: 0x0400033C RID: 828
	public int maxH;

	// Token: 0x0400033D RID: 829
	public int maxSize;

	// Token: 0x0400033E RID: 830
	public int x;

	// Token: 0x0400033F RID: 831
	public int y;

	// Token: 0x04000340 RID: 832
	public int value;

	// Token: 0x04000341 RID: 833
	public int cmtoX;

	// Token: 0x04000342 RID: 834
	public int cmx;

	// Token: 0x04000343 RID: 835
	public int cmdy;

	// Token: 0x04000344 RID: 836
	public int cmvy;

	// Token: 0x04000345 RID: 837
	public int cmxLim;

	// Token: 0x04000346 RID: 838
	private int pointerDownTime;

	// Token: 0x04000347 RID: 839
	private int pointerDownFirstX;

	// Token: 0x04000348 RID: 840
	private int[] pointerDownLastX = new int[3];

	// Token: 0x04000349 RID: 841
	public bool pointerIsDowning;

	// Token: 0x0400034A RID: 842
	public bool isDownWhenRunning;

	// Token: 0x0400034B RID: 843
	private int cmRun;

	// Token: 0x0400034C RID: 844
	private MyVector vecCmd;

	// Token: 0x0400034D RID: 845
	public int w;

	// Token: 0x0400034E RID: 846
	private int cmvx;

	// Token: 0x0400034F RID: 847
	private int cmdx;
}
