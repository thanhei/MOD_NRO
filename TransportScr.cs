using System;

// Token: 0x020000CD RID: 205
public class TransportScr : mScreen, IActionListener
{
	// Token: 0x06000A71 RID: 2673 RVA: 0x0009C2F4 File Offset: 0x0009A4F4
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

	// Token: 0x06000A72 RID: 2674 RVA: 0x00008B7F File Offset: 0x00006D7F
	public static TransportScr gI()
	{
		if (TransportScr.instance == null)
		{
			TransportScr.instance = new TransportScr();
		}
		return TransportScr.instance;
	}

	// Token: 0x06000A73 RID: 2675 RVA: 0x0009C3DC File Offset: 0x0009A5DC
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
		if (global::Char.myCharz().checkLuong() > 0 && (int)this.type == 0)
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

	// Token: 0x06000A74 RID: 2676 RVA: 0x0009C474 File Offset: 0x0009A674
	public override void paint(mGraphics g)
	{
		g.setColor(((int)this.type != 0) ? 3056895 : 0);
		g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
		for (int i = 0; i < this.n; i++)
		{
			g.setColor(((int)this.type != 0) ? 11140863 : 14802654);
			g.fillRect(this.posX[i], this.posY[i], 10, 2);
		}
		if ((int)this.type == 0)
		{
			g.drawRegion(TransportScr.ship, 0, 0, 72, 95, 7, this.cmx + this.currSpeed, GameCanvas.h / 2, 3);
		}
		if ((int)this.type == 1)
		{
			g.drawRegion(TransportScr.taungam, 0, 0, 144, 79, 2, this.cmx + this.currSpeed, GameCanvas.h / 2, 3);
		}
		for (int j = 0; j < this.n; j++)
		{
			g.setColor(((int)this.type != 0) ? 7536127 : 14935011);
			g.fillRect(this.posX2[j], this.posY2[j], 18, 3);
		}
		base.paint(g);
	}

	// Token: 0x06000A75 RID: 2677 RVA: 0x0009C5C4 File Offset: 0x0009A7C4
	public override void update()
	{
		if ((int)this.type == 0)
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
		if ((int)this.type == 1)
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

	// Token: 0x06000A76 RID: 2678 RVA: 0x00008B9A File Offset: 0x00006D9A
	public override void updateKey()
	{
		base.updateKey();
	}

	// Token: 0x06000A77 RID: 2679 RVA: 0x0009C7E0 File Offset: 0x0009A9E0
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

	// Token: 0x040013B3 RID: 5043
	public static TransportScr instance;

	// Token: 0x040013B4 RID: 5044
	public static Image ship;

	// Token: 0x040013B5 RID: 5045
	public static Image taungam;

	// Token: 0x040013B6 RID: 5046
	public sbyte type;

	// Token: 0x040013B7 RID: 5047
	public int speed = 5;

	// Token: 0x040013B8 RID: 5048
	public int[] posX;

	// Token: 0x040013B9 RID: 5049
	public int[] posY;

	// Token: 0x040013BA RID: 5050
	public int[] posX2;

	// Token: 0x040013BB RID: 5051
	public int[] posY2;

	// Token: 0x040013BC RID: 5052
	private int cmx;

	// Token: 0x040013BD RID: 5053
	private int n = 20;

	// Token: 0x040013BE RID: 5054
	public short time;

	// Token: 0x040013BF RID: 5055
	public short maxTime;

	// Token: 0x040013C0 RID: 5056
	public long last;

	// Token: 0x040013C1 RID: 5057
	public long curr;

	// Token: 0x040013C2 RID: 5058
	private bool isSpeed;

	// Token: 0x040013C3 RID: 5059
	private bool transNow;

	// Token: 0x040013C4 RID: 5060
	private int currSpeed;
}
