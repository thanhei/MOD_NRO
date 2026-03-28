using System;

// Token: 0x02000063 RID: 99
public class ItemMap : IMapObject
{
	// Token: 0x0600039A RID: 922 RVA: 0x000236FC File Offset: 0x000218FC
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
		Res.outz(string.Concat(new object[]
		{
			"playerid=  ",
			this.playerId,
			" myid= ",
			global::Char.myCharz().charID
		}));
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00023798 File Offset: 0x00021998
	public ItemMap(int playerId, short itemMapID, short itemTemplateID, int x, int y, short r)
	{
		Res.outz(string.Concat(new object[]
		{
			"item map item= ",
			itemMapID,
			" template= ",
			itemTemplateID,
			" x= ",
			x,
			" y= ",
			y
		}));
		this.itemMapID = (int)itemMapID;
		this.template = ItemTemplates.get(itemTemplateID);
		Res.outz(string.Concat(new object[]
		{
			"playerid=  ",
			playerId,
			" myid= ",
			global::Char.myCharz().charID
		}));
		this.xEnd = x;
		this.x = x;
		this.yEnd = y;
		this.y = y;
		this.status = 1;
		this.playerId = playerId;
		if (this.isAuraItem())
		{
			this.rO = (int)r;
			this.setAuraItem();
		}
	}

	// Token: 0x0600039C RID: 924 RVA: 0x00005DAC File Offset: 0x00003FAC
	public void setPoint(int xEnd, int yEnd)
	{
		this.xEnd = xEnd;
		this.yEnd = yEnd;
		this.vx = xEnd - this.x >> 2;
		this.vy = yEnd - this.y >> 2;
		this.status = 2;
	}

	// Token: 0x0600039D RID: 925 RVA: 0x00023898 File Offset: 0x00021A98
	public void update()
	{
		if ((int)this.status == 2 && this.x == this.xEnd && this.y == this.yEnd)
		{
			GameScr.vItemMap.removeElement(this);
			if (global::Char.myCharz().itemFocus != null && global::Char.myCharz().itemFocus.Equals(this))
			{
				global::Char.myCharz().itemFocus = null;
			}
			return;
		}
		if ((int)this.status > 0)
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
			this.status = (sbyte)((int)this.status - 4);
			if ((int)this.status < -12)
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

	// Token: 0x0600039E RID: 926 RVA: 0x00023A68 File Offset: 0x00021C68
	public void paint(mGraphics g)
	{
		if (this.isAuraItem())
		{
			g.drawImage(TileMap.bong, this.x + 3, this.y, mGraphics.VCENTER | mGraphics.HCENTER);
			if ((int)this.status <= 0)
			{
				if (this.countAura < 10)
				{
					g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
				else
				{
					g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + (int)this.status + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
			else if (this.countAura < 10)
			{
				g.drawImage(ItemMap.imageAuraItem1, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			else
			{
				g.drawImage(ItemMap.imageAuraItem2, this.x, this.y + 3, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
		}
		else if (!this.isAuraItem())
		{
			if (GameCanvas.gameTick % 4 == 0)
			{
				g.drawImage(ItemMap.imageFlare, this.x, this.y + (int)this.status + 13, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			if ((int)this.status <= 0)
			{
				SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + (int)this.status + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.template.iconID, this.x, this.y + 3, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			if (global::Char.myCharz().itemFocus != null && global::Char.myCharz().itemFocus.Equals(this) && (int)this.status != 2)
			{
				g.drawRegion(Mob.imgHP, 0, 24, 9, 6, 0, this.x, this.y - 17, 3);
			}
		}
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00023C88 File Offset: 0x00021E88
	private bool isAuraItem()
	{
		return (int)this.template.type == 22;
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x00023CB4 File Offset: 0x00021EB4
	private void setAuraItem()
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

	// Token: 0x060003A1 RID: 929 RVA: 0x00023D50 File Offset: 0x00021F50
	private void updateAuraItemEff()
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

	// Token: 0x060003A2 RID: 930 RVA: 0x00023DD8 File Offset: 0x00021FD8
	public void paintAuraItemEff(mGraphics g)
	{
		if (!GameCanvas.lowGraphic && this.isAuraItem())
		{
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
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00023E88 File Offset: 0x00022088
	private void setDotPosition()
	{
		if (!GameCanvas.lowGraphic)
		{
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
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00005DE3 File Offset: 0x00003FE3
	public int getX()
	{
		return this.x;
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00005DEB File Offset: 0x00003FEB
	public int getY()
	{
		return this.y;
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00005DF3 File Offset: 0x00003FF3
	public int getH()
	{
		return 20;
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00005DF3 File Offset: 0x00003FF3
	public int getW()
	{
		return 20;
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x000045ED File Offset: 0x000027ED
	public void stopMoving()
	{
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x00004381 File Offset: 0x00002581
	public bool isInvisible()
	{
		return false;
	}

	// Token: 0x04000616 RID: 1558
	public int x;

	// Token: 0x04000617 RID: 1559
	public int y;

	// Token: 0x04000618 RID: 1560
	public int xEnd;

	// Token: 0x04000619 RID: 1561
	public int yEnd;

	// Token: 0x0400061A RID: 1562
	public int f;

	// Token: 0x0400061B RID: 1563
	public int vx;

	// Token: 0x0400061C RID: 1564
	public int vy;

	// Token: 0x0400061D RID: 1565
	public int playerId;

	// Token: 0x0400061E RID: 1566
	public int itemMapID;

	// Token: 0x0400061F RID: 1567
	public int IdCharMove;

	// Token: 0x04000620 RID: 1568
	public ItemTemplate template;

	// Token: 0x04000621 RID: 1569
	public sbyte status;

	// Token: 0x04000622 RID: 1570
	public bool isHintFocus;

	// Token: 0x04000623 RID: 1571
	public int rO;

	// Token: 0x04000624 RID: 1572
	public int xO;

	// Token: 0x04000625 RID: 1573
	public int yO;

	// Token: 0x04000626 RID: 1574
	public int angle;

	// Token: 0x04000627 RID: 1575
	public int iAngle;

	// Token: 0x04000628 RID: 1576
	public int iDot;

	// Token: 0x04000629 RID: 1577
	public int[] xArg;

	// Token: 0x0400062A RID: 1578
	public int[] yArg;

	// Token: 0x0400062B RID: 1579
	public int[] xDot;

	// Token: 0x0400062C RID: 1580
	public int[] yDot;

	// Token: 0x0400062D RID: 1581
	public int count;

	// Token: 0x0400062E RID: 1582
	public int countAura;

	// Token: 0x0400062F RID: 1583
	public static Image imageFlare = GameCanvas.loadImage("/mainImage/myTexture2dflare.png");

	// Token: 0x04000630 RID: 1584
	public static Image imageAuraItem1 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura1.png");

	// Token: 0x04000631 RID: 1585
	public static Image imageAuraItem2 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura2.png");

	// Token: 0x04000632 RID: 1586
	public static Image imageAuraItem3 = GameCanvas.loadImage("/mainImage/myTexture2ditemaura3.png");
}
