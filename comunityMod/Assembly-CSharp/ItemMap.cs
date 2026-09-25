using System;
using System.Runtime.CompilerServices;

// Token: 0x02000056 RID: 86
public class ItemMap : IMapObject
{
	// Token: 0x060004B4 RID: 1204 RVA: 0x0004DDDC File Offset: 0x0004BFDC
	public ItemMap(short itemMapID, short itemTemplateID, int x, int y, int xEnd, int yEnd)
	{
		this.itemMapID = (int)itemMapID;
		this.template = ItemTemplates.get(itemTemplateID);
		this.x = xEnd;
		this.y = y;
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.vx = xEnd - x >> 2;
		this.vy = 5;
		Res.outz("playerid=  " + this.playerId.ToString() + " myid= " + global::Char.myCharz().charID.ToString());
	}

	// Token: 0x060004B5 RID: 1205 RVA: 0x0004DE64 File Offset: 0x0004C064
	public ItemMap(int playerId, short itemMapID, short itemTemplateID, int x, int y, short r)
	{
		Res.outz(string.Concat(new string[]
		{
			"item map item= ",
			itemMapID.ToString(),
			" template= ",
			itemTemplateID.ToString(),
			" x= ",
			x.ToString(),
			" y= ",
			y.ToString()
		}));
		this.itemMapID = (int)itemMapID;
		this.template = ItemTemplates.get(itemTemplateID);
		Res.outz("playerid=  " + playerId.ToString() + " myid= " + global::Char.myCharz().charID.ToString());
		this.x = (this.xEnd = x);
		this.y = (this.yEnd = y);
		this.status = 1;
		this.playerId = playerId;
		if (this.isAuraItem())
		{
			this.rO = (int)r;
			this.setAuraItem();
		}
	}

	// Token: 0x060004B6 RID: 1206 RVA: 0x0004DF52 File Offset: 0x0004C152
	[MethodImpl(MethodImplOptions.NoOptimization)]
	public void setPoint(int xEnd, int yEnd)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.vx = xEnd - this.x >> 2;
		this.vy = yEnd - this.y >> 2;
		this.status = 2;
	}

	// Token: 0x060004B7 RID: 1207 RVA: 0x0004DF8C File Offset: 0x0004C18C
	public void update()
	{
		if (this.status == 2 && this.x == this.xEnd && this.y == this.yEnd)
		{
			GameScr.vItemMap.removeElement(this);
			if (global::Char.myCharz().itemFocus != null && global::Char.myCharz().itemFocus.Equals(this))
			{
				global::Char.myCharz().itemFocus = null;
			}
			return;
		}
		if (this.status > 0)
		{
			if (this.vx == 0)
			{
				this.x = this.xEnd;
			}
			if (this.vy == 0)
			{
				this.y = this.yEnd;
			}
			if (this.x != this.xEnd)
			{
				this.x += this.vx;
				if ((this.vx > 0 && this.x > this.xEnd) || (this.vx < 0 && this.x < this.xEnd))
				{
					this.x = this.xEnd;
				}
			}
			if (this.y != this.yEnd)
			{
				this.y += this.vy;
				if ((this.vy > 0 && this.y > this.yEnd) || (this.vy < 0 && this.y < this.yEnd))
				{
					this.y = this.yEnd;
				}
			}
		}
		else
		{
			this.status -= 4;
			if (this.status < -12)
			{
				this.y -= 12;
				this.status = 1;
			}
		}
		if (this.isAuraItem())
		{
			this.updateAuraItemEff();
		}
	}

	// Token: 0x060004B8 RID: 1208 RVA: 0x0004E11C File Offset: 0x0004C31C
	public void paint(mGraphics g)
	{
		if (!this.isAuraItem())
		{
			if (!this.isAuraItem())
			{
				if (GameCanvas.gameTick % 4 == 0)
				{
					g.drawImage(ItemMap.imageFlare, this.x, this.y + (int)this.status + 13, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				if (this.status <= 0)
				{
					SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + (int)this.status + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				if (global::Char.myCharz().itemFocus != null && global::Char.myCharz().itemFocus.Equals(this) && this.status != 2)
				{
					g.drawRegion(Mob.imgHP, 0, 24, 9, 6, 0, this.x, this.y - 17, 3);
				}
			}
			return;
		}
		g.drawImage(TileMap.bong, this.x + 3, this.y, mGraphics.VCENTER | mGraphics.HCENTER);
		if (this.status <= 0)
		{
			if (this.countAura < 10)
			{
				g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				return;
			}
			g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
			return;
		}
		else
		{
			if (this.countAura < 10)
			{
				g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				return;
			}
			g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
			return;
		}
	}

	// Token: 0x060004B9 RID: 1209 RVA: 0x0004E308 File Offset: 0x0004C508
	internal bool isAuraItem()
	{
		return this.template.type == 22;
	}

	// Token: 0x060004BA RID: 1210 RVA: 0x0004E31C File Offset: 0x0004C51C
	internal void setAuraItem()
	{
		this.xO = this.x;
		this.yO = this.y;
		this.iDot = 120;
		this.angle = 0;
		if (!GameCanvas.lowGraphic)
		{
			this.iAngle = 360 / this.iDot;
			this.xArg = new int[this.iDot];
			this.yArg = new int[this.iDot];
			this.xDot = new int[this.iDot];
			this.yDot = new int[this.iDot];
			this.setDotPosition();
		}
	}

	// Token: 0x060004BB RID: 1211 RVA: 0x0004E3B4 File Offset: 0x0004C5B4
	internal void updateAuraItemEff()
	{
		this.count++;
		this.countAura++;
		if (this.countAura >= 40)
		{
			this.countAura = 0;
		}
		if (this.count >= this.iDot)
		{
			this.count = 0;
		}
		if (this.count % 10 == 0 && !GameCanvas.lowGraphic)
		{
			ServerEffect.addServerEffect(114, this.x - 5, this.y - 30, 1);
		}
	}

	// Token: 0x060004BC RID: 1212 RVA: 0x0004E430 File Offset: 0x0004C630
	public void paintAuraItemEff(mGraphics g)
	{
		if (GameCanvas.lowGraphic || !this.isAuraItem())
		{
			return;
		}
		for (int i = 0; i < this.yArg.Length; i++)
		{
			if (this.count == i)
			{
				if (this.countAura <= 20)
				{
					g.drawImage(ItemMap.imageAuraItem3, this.xDot[i], this.yDot[i] + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					SmallImage.drawSmallImage(g, (int)this.template.iconID, this.xDot[i], this.yDot[i] + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
		}
	}

	// Token: 0x060004BD RID: 1213 RVA: 0x0004E4CC File Offset: 0x0004C6CC
	internal void setDotPosition()
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		for (int i = 0; i < this.yArg.Length; i++)
		{
			this.yArg[i] = Res.abs(this.rO * Res.sin(this.angle) / 1024);
			this.xArg[i] = Res.abs(this.rO * Res.cos(this.angle) / 1024);
			if (this.angle < 90)
			{
				this.xDot[i] = this.xO + this.xArg[i];
				this.yDot[i] = this.yO - this.yArg[i];
			}
			else if (this.angle >= 90 && this.angle < 180)
			{
				this.xDot[i] = this.xO - this.xArg[i];
				this.yDot[i] = this.yO - this.yArg[i];
			}
			else if (this.angle >= 180 && this.angle < 270)
			{
				this.xDot[i] = this.xO - this.xArg[i];
				this.yDot[i] = this.yO + this.yArg[i];
			}
			else
			{
				this.xDot[i] = this.xO + this.xArg[i];
				this.yDot[i] = this.yO + this.yArg[i];
			}
			this.angle += this.iAngle;
		}
	}

	// Token: 0x060004BE RID: 1214 RVA: 0x0004E653 File Offset: 0x0004C853
	public int getX()
	{
		return this.x;
	}

	// Token: 0x060004BF RID: 1215 RVA: 0x0004E65B File Offset: 0x0004C85B
	public int getY()
	{
		return this.y;
	}

	// Token: 0x060004C0 RID: 1216 RVA: 0x0004E663 File Offset: 0x0004C863
	public int getH()
	{
		return 20;
	}

	// Token: 0x060004C1 RID: 1217 RVA: 0x0004E663 File Offset: 0x0004C863
	public int getW()
	{
		return 20;
	}

	// Token: 0x060004C2 RID: 1218 RVA: 0x00004887 File Offset: 0x00002A87
	public void stopMoving()
	{
	}

	// Token: 0x060004C3 RID: 1219 RVA: 0x000151BF File Offset: 0x000133BF
	public bool isInvisible()
	{
		return false;
	}

	// Token: 0x040009DC RID: 2524
	public int countAutoPick;

	// Token: 0x040009DD RID: 2525
	public int x;

	// Token: 0x040009DE RID: 2526
	public int y;

	// Token: 0x040009DF RID: 2527
	public int xEnd;

	// Token: 0x040009E0 RID: 2528
	public int yEnd;

	// Token: 0x040009E1 RID: 2529
	public int f;

	// Token: 0x040009E2 RID: 2530
	public int vx;

	// Token: 0x040009E3 RID: 2531
	public int vy;

	// Token: 0x040009E4 RID: 2532
	public int playerId;

	// Token: 0x040009E5 RID: 2533
	public int itemMapID;

	// Token: 0x040009E6 RID: 2534
	public int IdCharMove;

	// Token: 0x040009E7 RID: 2535
	public ItemTemplate template;

	// Token: 0x040009E8 RID: 2536
	public sbyte status;

	// Token: 0x040009E9 RID: 2537
	public bool isHintFocus;

	// Token: 0x040009EA RID: 2538
	public int rO;

	// Token: 0x040009EB RID: 2539
	public int xO;

	// Token: 0x040009EC RID: 2540
	public int yO;

	// Token: 0x040009ED RID: 2541
	public int angle;

	// Token: 0x040009EE RID: 2542
	public int iAngle;

	// Token: 0x040009EF RID: 2543
	public int iDot;

	// Token: 0x040009F0 RID: 2544
	public int[] xArg;

	// Token: 0x040009F1 RID: 2545
	public int[] yArg;

	// Token: 0x040009F2 RID: 2546
	public int[] xDot;

	// Token: 0x040009F3 RID: 2547
	public int[] yDot;

	// Token: 0x040009F4 RID: 2548
	public int count;

	// Token: 0x040009F5 RID: 2549
	public int countAura;

	// Token: 0x040009F6 RID: 2550
	public static Image imageFlare = GameCanvas.loadImage("/mainImage/myTexture2dflare.png");

	// Token: 0x040009F7 RID: 2551
	public static Image imageAuraItem1 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura1.png");

	// Token: 0x040009F8 RID: 2552
	public static Image imageAuraItem2 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura2.png");

	// Token: 0x040009F9 RID: 2553
	public static Image imageAuraItem3 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura3.png");
}
