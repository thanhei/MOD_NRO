using System;

// Token: 0x020000BE RID: 190
public class TransportScr : mScreen, IActionListener
{
	// Token: 0x060009CF RID: 2511 RVA: 0x0008CF44 File Offset: 0x0008B144
	public TransportScr()
	{
		this.posX = new int[this.n];
		this.posY = new int[this.n];
		for (int i = 0; i < this.n; i++)
		{
			this.posX[i] = Res.random(0, GameCanvas.w);
			this.posY[i] = i * (GameCanvas.h / this.n);
		}
		this.posX2 = new int[this.n];
		this.posY2 = new int[this.n];
		for (int j = 0; j < this.n; j++)
		{
			this.posX2[j] = Res.random(0, GameCanvas.w);
			this.posY2[j] = j * (GameCanvas.h / this.n);
		}
	}

	// Token: 0x060009D0 RID: 2512 RVA: 0x0008D01E File Offset: 0x0008B21E
	public static TransportScr gI()
	{
		if (TransportScr.instance == null)
		{
			TransportScr.instance = new TransportScr();
		}
		return TransportScr.instance;
	}

	// Token: 0x060009D1 RID: 2513 RVA: 0x0008D038 File Offset: 0x0008B238
	public override void switchToMe()
	{
		if (TransportScr.ship == null)
		{
			TransportScr.ship = GameCanvas.loadImage("/mainImage/myTexture2dfutherShip.png");
		}
		if (TransportScr.taungam == null)
		{
			TransportScr.taungam = GameCanvas.loadImage("/mainImage/taungam.png");
		}
		this.isSpeed = false;
		this.transNow = false;
		if (global::Char.myCharz().checkLuong() > 0 && this.type == 0)
		{
			this.center = new Command(mResources.faster, this, 1, null);
		}
		else
		{
			this.center = null;
		}
		this.currSpeed = 0;
		base.switchToMe();
	}

	// Token: 0x060009D2 RID: 2514 RVA: 0x0008D0C0 File Offset: 0x0008B2C0
	public override void paint(mGraphics g)
	{
		g.setColor((this.type != 0) ? 3056895 : 0);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		for (int i = 0; i < this.n; i++)
		{
			g.setColor((this.type != 0) ? 11140863 : 14802654);
			g.fillRect(this.posX[i], this.posY[i], 10, 2);
		}
		if (this.type == 0)
		{
			g.drawRegion(TransportScr.ship, 0, 0, 72, 95, 7, this.cmx + this.currSpeed, GameCanvas.h / 2, 3);
		}
		if (this.type == 1)
		{
			g.drawRegion(TransportScr.taungam, 0, 0, 144, 79, 2, this.cmx + this.currSpeed, GameCanvas.h / 2, 3);
		}
		for (int j = 0; j < this.n; j++)
		{
			g.setColor((this.type != 0) ? 7536127 : 14935011);
			g.fillRect(this.posX2[j], this.posY2[j], 18, 3);
		}
		base.paint(g);
	}

	// Token: 0x060009D3 RID: 2515 RVA: 0x0008D1E8 File Offset: 0x0008B3E8
	public override void update()
	{
		if (this.type == 0)
		{
			if (!this.isSpeed)
			{
				this.currSpeed = GameCanvas.w / 2 * (int)this.time / (int)this.maxTime;
			}
		}
		else
		{
			this.currSpeed += 2;
		}
		Controller.isStopReadMessage = false;
		this.cmx = (((GameCanvas.w / 2 + this.cmx) / 2 + this.cmx) / 2 + this.cmx) / 2;
		if (this.type == 1)
		{
			this.cmx = 0;
		}
		for (int i = 0; i < this.n; i++)
		{
			this.posX[i] -= this.speed / 2;
			if (this.posX[i] < -20)
			{
				this.posX[i] = GameCanvas.w;
			}
		}
		for (int j = 0; j < this.n; j++)
		{
			this.posX2[j] -= this.speed;
			if (this.posX2[j] < -20)
			{
				this.posX2[j] = GameCanvas.w;
			}
		}
		if (GameCanvas.gameTick % 3 == 0)
		{
			this.speed += ((!this.isSpeed) ? 1 : 2);
		}
		if (this.speed > ((!this.isSpeed) ? 25 : 80))
		{
			this.speed = ((!this.isSpeed) ? 25 : 80);
		}
		this.curr = mSystem.currentTimeMillis();
		if (this.curr - this.last >= 1000L)
		{
			this.time += 1;
			this.last = this.curr;
		}
		if (this.isSpeed)
		{
			this.currSpeed += 3;
		}
		if (this.currSpeed >= GameCanvas.w / 2 + 30 && !this.transNow)
		{
			this.transNow = true;
			Service.gI().transportNow();
		}
		base.update();
	}

	// Token: 0x060009D4 RID: 2516 RVA: 0x0008D3BE File Offset: 0x0008B5BE
	public override void updateKey()
	{
		base.updateKey();
	}

	// Token: 0x060009D5 RID: 2517 RVA: 0x0008D3C8 File Offset: 0x0008B5C8
	public void perform(int idAction, object p)
	{
		if (idAction == 1)
		{
			GameCanvas.startYesNoDlg(mResources.fasterQuestion, new Command(mResources.YES, this, 2, null), new Command(mResources.NO, this, 3, null));
		}
		if (idAction == 2 && global::Char.myCharz().checkLuong() > 0)
		{
			this.isSpeed = true;
			GameCanvas.endDlg();
			this.center = null;
		}
		if (idAction == 3)
		{
			GameCanvas.endDlg();
		}
	}

	// Token: 0x04001161 RID: 4449
	public static TransportScr instance;

	// Token: 0x04001162 RID: 4450
	public static Image ship;

	// Token: 0x04001163 RID: 4451
	public static Image taungam;

	// Token: 0x04001164 RID: 4452
	public sbyte type;

	// Token: 0x04001165 RID: 4453
	public int speed = 5;

	// Token: 0x04001166 RID: 4454
	public int[] posX;

	// Token: 0x04001167 RID: 4455
	public int[] posY;

	// Token: 0x04001168 RID: 4456
	public int[] posX2;

	// Token: 0x04001169 RID: 4457
	public int[] posY2;

	// Token: 0x0400116A RID: 4458
	internal int cmx;

	// Token: 0x0400116B RID: 4459
	internal int n = 20;

	// Token: 0x0400116C RID: 4460
	public short time;

	// Token: 0x0400116D RID: 4461
	public short maxTime;

	// Token: 0x0400116E RID: 4462
	public long last;

	// Token: 0x0400116F RID: 4463
	public long curr;

	// Token: 0x04001170 RID: 4464
	internal bool isSpeed;

	// Token: 0x04001171 RID: 4465
	internal bool transNow;

	// Token: 0x04001172 RID: 4466
	internal int currSpeed;
}
