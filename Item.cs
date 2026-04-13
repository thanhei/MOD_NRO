using System;

// Token: 0x02000062 RID: 98
public class Item
{
	// Token: 0x06000385 RID: 901 RVA: 0x00005C58 File Offset: 0x00003E58
	public void getCompare()
	{
		this.compare = GameCanvas.panel.getCompare(this);
	}

	// Token: 0x06000386 RID: 902 RVA: 0x00022C2C File Offset: 0x00020E2C
	public string getPrice()
	{
		string result = string.Empty;
		if (this.buyCoin <= 0 && this.buyGold <= 0)
		{
			return null;
		}
		if (this.buyCoin > 0 && this.buyGold <= 0)
		{
			result = this.buyCoin + mResources.XU;
		}
		else if (this.buyGold > 0 && this.buyCoin <= 0)
		{
			result = this.buyGold + mResources.LUONG;
		}
		else if (this.buyCoin > 0 && this.buyGold > 0)
		{
			result = string.Concat(new object[]
			{
				this.buyCoin,
				mResources.XU,
				"/",
				this.buyGold,
				mResources.LUONG
			});
		}
		return result;
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00022CFC File Offset: 0x00020EFC
	public void paintUpgradeEffect(int x, int y, int upgrade, mGraphics g)
	{
		int num = GameScr.indexSize - 2;
		int num2 = 0;
		int num3 = (upgrade >= 4) ? ((upgrade >= 8) ? ((upgrade >= 12) ? ((upgrade > 14) ? 4 : 3) : 2) : 1) : 0;
		for (int i = num2; i < this.size.Length; i++)
		{
			int num4 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - i * 4);
			int num5 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - i * 4);
			g.setColor(this.colorBorder[num3][i]);
			g.fillRect(num4 - this.size[i] / 2, num5 - this.size[i] / 2, this.size[i], this.size[i]);
		}
		if (upgrade == 4 || upgrade == 8)
		{
			for (int j = num2; j < this.size.Length; j++)
			{
				int num6 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - num * 2 - j * 4);
				int num7 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - num * 2 - j * 4);
				g.setColor(this.colorBorder[num3 - 1][j]);
				g.fillRect(num6 - this.size[j] / 2, num7 - this.size[j] / 2, this.size[j], this.size[j]);
			}
		}
		if (upgrade != 1 && upgrade != 4 && upgrade != 8)
		{
			for (int k = num2; k < this.size.Length; k++)
			{
				int num8 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - num * 2 - k * 4);
				int num9 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - num * 2 - k * 4);
				g.setColor(this.colorBorder[num3][k]);
				g.fillRect(num8 - this.size[k] / 2, num9 - this.size[k] / 2, this.size[k], this.size[k]);
			}
		}
		if (upgrade != 1 && upgrade != 4 && upgrade != 8 && upgrade != 12 && upgrade != 2 && upgrade != 5 && upgrade != 9)
		{
			for (int l = num2; l < this.size.Length; l++)
			{
				int num10 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - num - l * 4);
				int num11 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - num - l * 4);
				g.setColor(this.colorBorder[num3][l]);
				g.fillRect(num10 - this.size[l] / 2, num11 - this.size[l] / 2, this.size[l], this.size[l]);
			}
		}
		if (upgrade != 1 && upgrade != 4 && upgrade != 8 && upgrade != 12 && upgrade != 2 && upgrade != 5 && upgrade != 9 && upgrade != 13 && upgrade != 3 && upgrade != 6 && upgrade != 10 && upgrade != 15)
		{
			for (int m = num2; m < this.size.Length; m++)
			{
				int num12 = x - num / 2 + this.upgradeEffectX(GameCanvas.gameTick - num * 3 - m * 4);
				int num13 = y - num / 2 + this.upgradeEffectY(GameCanvas.gameTick - num * 3 - m * 4);
				g.setColor(this.colorBorder[num3][m]);
				g.fillRect(num12 - this.size[m] / 2, num13 - this.size[m] / 2, this.size[m], this.size[m]);
			}
		}
	}



	// Token: 0x06000F41 RID: 3905
	public void paintItemEffect(mGraphics g, int x, int y, int index)
	{
		if (index > 8)
		{
			index = 8;
		}
		if (index < 0)
		{
			index = 0;
		}
		int[] array = new int[]
		{
			0,
			0,
			1,
			1,
			2,
			3,
			4,
			5
		};
		int[] array2 = new int[]
		{
			0,
			0,
			0,
			0,
			600841,
			3346944,
			3932211,
			6684682,
			39423
		};
		int[] array4 = new int[4];
		array4[1] = 68;
		array4[2] = 68;
		int[] array3 = array4;
		int baseW = 34;
		int baseH = 23;
		if (index >= 4)
		{
			g.setColor(array2[index]);
			g.fillRect(x - baseW / 2, y - baseH / 2, baseW, baseH);
		}
		if (index < 4)
		{
			if (index == 1)
			{
				for (int i = 0; i < 2; i++)
				{
					for (int j = 0; j < this.size.Length; j++)
					{
						int t = GameCanvas.gameTick + array3[i] - j * 4;
						int rawX = this.method_0(t) - baseW / 2;
						int rawY = this.method_1(t) - baseH / 2;
						int px = x + rawX;
						int py = y + rawY;
						g.setColor(this.colorBorder[array[index - 1]][j]);
						g.fillRect(px - this.size[j] / 2, py - this.size[j] / 2, this.size[j], this.size[j]);
					}
				}
				return;
			}
			if (index != 2)
			{
				for (int n2 = 0; n2 < 2; n2++)
				{
					for (int l2 = 0; l2 < this.size.Length; l2++)
					{
						int t2 = GameCanvas.gameTick + array3[n2] - l2 * 4;
						int rawX2 = this.method_0(t2) - baseW / 2;
						int rawY2 = this.method_1(t2) - baseH / 2;
						int px2 = x + rawX2;
						int py2 = y + rawY2;
						g.setColor(this.colorBorder[0][l2]);
						g.fillRect(px2 - this.size[l2] / 2, py2 - this.size[l2] / 2, this.size[l2], this.size[l2]);
					}
				}
				for (int m2 = 2; m2 < 4; m2++)
				{
					for (int n3 = 0; n3 < this.size.Length; n3++)
					{
						int t3 = GameCanvas.gameTick + array3[m2] - n3 * 4;
						int rawX3 = this.method_0(t3) - baseW / 2;
						int rawY3 = this.method_1(t3) - baseH / 2;
						int px3 = x + rawX3;
						int py3 = y + rawY3;
						g.setColor(this.colorBorder[1][n3]);
						g.fillRect(px3 - this.size[n3] / 2, py3 - this.size[n3] / 2, this.size[n3], this.size[n3]);
					}
				}
				return;
			}
		}
		for (int k = 0; k < 4; k++)
		{
			for (int j2 = 0; j2 < this.size.Length; j2++)
			{
				int t4 = GameCanvas.gameTick + array3[k] - j2 * 4;
				int rawX4 = this.method_0(t4) - baseW / 2;
				int rawY4 = this.method_1(t4) - baseH / 2;
				int px4 = x + rawX4;
				int py4 = y + rawY4;
				int color;
				if (index == 8)
				{
					int c = 180 + (GameCanvas.gameTick / 2 + j2 * 10) % 50;
					color = (c << 16 | c << 8 | c);
				}
				else
				{
					color = this.colorBorder[array[index - 1]][j2];
				}
				g.setColor(color);
				g.fillRect(px4 - this.size[j2] / 2, py4 - this.size[j2] / 2, this.size[j2], this.size[j2]);
			}
		}
	}

	// Token: 0x06000C1F RID: 3103
	private int method_0(int t)
	{
		int w = 34;
		int max = w - 1;
		int p = t % (w * 4);
		if (p < w)
		{
			return p;
		}
		if (p < w * 2)
		{
			return max;
		}
		if (p < w * 3)
		{
			return max - p % w;
		}
		return 0;
	}

	// Token: 0x06000C20 RID: 3104
	private int method_1(int t)
	{
		int h = 23;
		int max = h - 1;
		int p = t % (h * 4);
		if (p < h)
		{
			return 0;
		}
		if (p < h * 2)
		{
			return p % h;
		}
		if (p < h * 3)
		{
			return max;
		}
		return max - p % h;
	}


	// Token: 0x06000388 RID: 904 RVA: 0x000230F4 File Offset: 0x000212F4
	private int upgradeEffectY(int tick)
	{
		int num = GameScr.indexSize - 2;
		int num2 = tick % (4 * num);
		if (0 <= num2 && num2 < num)
		{
			return 0;
		}
		if (num <= num2 && num2 < num * 2)
		{
			return num2 % num;
		}
		if (num * 2 <= num2 && num2 < num * 3)
		{
			return num;
		}
		return num - num2 % num;
	}

	// Token: 0x06000389 RID: 905 RVA: 0x0002314C File Offset: 0x0002134C
	private int upgradeEffectX(int tick)
	{
		int num = GameScr.indexSize - 2;
		int num2 = tick % (4 * num);
		if (0 <= num2 && num2 < num)
		{
			return num2 % num;
		}
		if (num <= num2 && num2 < num * 2)
		{
			return num;
		}
		if (num * 2 <= num2 && num2 < num * 3)
		{
			return num - num2 % num;
		}
		return 0;
	}

	// Token: 0x0600038A RID: 906 RVA: 0x000231A4 File Offset: 0x000213A4
	public bool isHaveOption(int id)
	{
		for (int i = 0; i < this.itemOption.Length; i++)
		{
			ItemOption itemOption = this.itemOption[i];
			if (itemOption != null && itemOption.optionTemplate.id == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600038B RID: 907 RVA: 0x000231F0 File Offset: 0x000213F0
	public Item clone()
	{
		Item item = new Item();
		item.template = this.template;
		if (this.options != null)
		{
			item.options = new MyVector();
			for (int i = 0; i < this.options.size(); i++)
			{
				ItemOption itemOption = new ItemOption();
				itemOption.optionTemplate = ((ItemOption)this.options.elementAt(i)).optionTemplate;
				itemOption.param = ((ItemOption)this.options.elementAt(i)).param;
				item.options.addElement(itemOption);
			}
		}
		item.itemId = this.itemId;
		item.playerId = this.playerId;
		item.indexUI = this.indexUI;
		item.quantity = this.quantity;
		item.isLock = this.isLock;
		item.sys = this.sys;
		item.upgrade = this.upgrade;
		item.buyCoin = this.buyCoin;
		item.buyCoinLock = this.buyCoinLock;
		item.buyGold = this.buyGold;
		item.buyGoldLock = this.buyGoldLock;
		item.saleCoinLock = this.saleCoinLock;
		item.typeUI = this.typeUI;
		item.isExpires = this.isExpires;
		return item;
	}

	// Token: 0x0600038C RID: 908 RVA: 0x00023334 File Offset: 0x00021534
	public bool isTypeBody()
	{
		return (0 <= (int)this.template.type && (int)this.template.type < 6) || (int)this.template.type == 32 || (int)this.template.type == 35 || (int)this.template.type == 11 || (int)this.template.type == 23;
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00005C6B File Offset: 0x00003E6B
	public string getLockstring()
	{
		return (!this.isLock) ? mResources.NOLOCK : mResources.LOCKED;
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00005C87 File Offset: 0x00003E87
	public string getUpgradestring()
	{
		if ((int)this.template.level < 10 || (int)this.template.type >= 10)
		{
			return mResources.NOTUPGRADE;
		}
		if (this.upgrade == 0)
		{
			return mResources.NOUPGRADE;
		}
		return null;
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00005CC7 File Offset: 0x00003EC7
	public bool isTypeUIMe()
	{
		return this.typeUI == 5 || this.typeUI == 3 || this.typeUI == 4;
	}

	// Token: 0x06000390 RID: 912 RVA: 0x00005CF0 File Offset: 0x00003EF0
	public bool isTypeUIShopView()
	{
		return this.isTypeUIShop() || (this.isTypeUIStore() || this.isTypeUIBook() || this.isTypeUIFashion());
	}

	// Token: 0x06000391 RID: 913 RVA: 0x000233B4 File Offset: 0x000215B4
	public bool isTypeUIShop()
	{
		return this.typeUI == 20 || this.typeUI == 21 || this.typeUI == 22 || this.typeUI == 23 || this.typeUI == 24 || this.typeUI == 25 || this.typeUI == 26 || this.typeUI == 27 || this.typeUI == 28 || this.typeUI == 29 || this.typeUI == 16 || this.typeUI == 17 || this.typeUI == 18 || this.typeUI == 19 || this.typeUI == 2 || this.typeUI == 6 || this.typeUI == 8;
	}

	// Token: 0x06000392 RID: 914 RVA: 0x00005D23 File Offset: 0x00003F23
	public bool isTypeUIShopLock()
	{
		return this.typeUI == 7 || this.typeUI == 9;
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00005D41 File Offset: 0x00003F41
	public bool isTypeUIStore()
	{
		return this.typeUI == 14;
	}

	// Token: 0x06000394 RID: 916 RVA: 0x00005D53 File Offset: 0x00003F53
	public bool isTypeUIBook()
	{
		return this.typeUI == 15;
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00005D65 File Offset: 0x00003F65
	public bool isTypeUIFashion()
	{
		return this.typeUI == 32;
	}

	// Token: 0x06000396 RID: 918 RVA: 0x00005D77 File Offset: 0x00003F77
	public bool isUpMax()
	{
		return this.getUpMax() == this.upgrade;
	}

	// Token: 0x06000397 RID: 919 RVA: 0x000234A0 File Offset: 0x000216A0
	public int getUpMax()
	{
		if ((int)this.template.level >= 1 && (int)this.template.level < 20)
		{
			return 4;
		}
		if ((int)this.template.level >= 20 && (int)this.template.level < 40)
		{
			return 8;
		}
		if ((int)this.template.level >= 40 && (int)this.template.level < 50)
		{
			return 12;
		}
		if ((int)this.template.level >= 50 && (int)this.template.level < 60)
		{
			return 14;
		}
		return 16;
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00005D8D File Offset: 0x00003F8D
	public void setPartTemp(int headTemp, int bodyTemp, int legTemp, int bagTemp)
	{
		this.headTemp = headTemp;
		this.bodyTemp = bodyTemp;
		this.legTemp = legTemp;
		this.bagTemp = bagTemp;
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00023550 File Offset: 0x00021750
	public string getFullName()
	{
		string text = this.template.name;
		if (this.itemOption != null)
		{
			for (int i = 0; i < this.itemOption.Length; i++)
			{
				if (this.itemOption[i].optionTemplate.id == 72)
				{
					text = string.Concat(new object[]
					{
						text,
						" [+",
						this.itemOption[i].param,
						"]"
					});
					break;
				}
			}
		}
		if (this.itemOption != null)
		{
			for (int j = 0; j < this.itemOption.Length; j++)
			{
				if (this.itemOption[j].optionTemplate.name.StartsWith("$"))
				{
					string optiongColor = this.itemOption[j].getOptiongColor();
					if (this.itemOption[j].param == 1)
					{
						text = text + "\n" + optiongColor;
					}
					if (this.itemOption[j].param == 0)
					{
						text = text + "\n" + optiongColor;
					}
				}
				else
				{
					string optionString = this.itemOption[j].getOptionString();
					if (!optionString.Equals(string.Empty) && this.itemOption[j].optionTemplate.id != 72)
					{
						text = text + "\n" + optionString;
					}
				}
			}
		}
		if (this.template.strRequire > 1)
		{
			text = string.Concat(new object[]
			{
				text,
				"\n",
				mResources.pow_request,
				": ",
				this.template.strRequire
			});
		}
		return text + "\n" + this.template.description;
	}

	// Token: 0x040005B0 RID: 1456
	public const int OPT_STAR = 34;

	// Token: 0x040005B1 RID: 1457
	public const int OPT_MOON = 35;

	// Token: 0x040005B2 RID: 1458
	public const int OPT_SUN = 36;

	// Token: 0x040005B3 RID: 1459
	public const int OPT_COLORNAME = 41;

	// Token: 0x040005B4 RID: 1460
	public const int OPT_LVITEM = 72;

	// Token: 0x040005B5 RID: 1461
	public const int OPT_STARSLOT = 102;

	// Token: 0x040005B6 RID: 1462
	public const int OPT_MAXSTARSLOT = 107;

	// Token: 0x040005B7 RID: 1463
	public const int TYPE_BODY_MIN = 0;

	// Token: 0x040005B8 RID: 1464
	public const int TYPE_BODY_MAX = 6;

	// Token: 0x040005B9 RID: 1465
	public const int TYPE_AO = 0;

	// Token: 0x040005BA RID: 1466
	public const int TYPE_QUAN = 1;

	// Token: 0x040005BB RID: 1467
	public const int TYPE_GANGTAY = 2;

	// Token: 0x040005BC RID: 1468
	public const int TYPE_GIAY = 3;

	// Token: 0x040005BD RID: 1469
	public const int TYPE_RADA = 4;

	// Token: 0x040005BE RID: 1470
	public const int TYPE_HAIR = 5;

	// Token: 0x040005BF RID: 1471
	public const int TYPE_DAUTHAN = 6;

	// Token: 0x040005C0 RID: 1472
	public const int TYPE_NGOCRONG = 12;

	// Token: 0x040005C1 RID: 1473
	public const int TYPE_SACH = 7;

	// Token: 0x040005C2 RID: 1474
	public const int TYPE_NHIEMVU = 8;

	// Token: 0x040005C3 RID: 1475
	public const int TYPE_GOLD = 9;

	// Token: 0x040005C4 RID: 1476
	public const int TYPE_DIAMOND = 10;

	// Token: 0x040005C5 RID: 1477
	public const int TYPE_BALO = 11;

	// Token: 0x040005C6 RID: 1478
	public const int TYPE_MOUNT = 23;

	// Token: 0x040005C7 RID: 1479
	public const int TYPE_MOUNT_VIP = 24;

	// Token: 0x040005C8 RID: 1480
	public const int TYPE_DIAMOND_LOCK = 34;

	// Token: 0x040005C9 RID: 1481
	public const int TYPE_TRAINSUIT = 32;

	// Token: 0x040005CA RID: 1482
	public const int TYPE_HAT = 35;

	// Token: 0x040005CB RID: 1483
	public const sbyte UI_WEAPON = 2;

	// Token: 0x040005CC RID: 1484
	public const sbyte UI_BAG = 3;

	// Token: 0x040005CD RID: 1485
	public const sbyte UI_BOX = 4;

	// Token: 0x040005CE RID: 1486
	public const sbyte UI_BODY = 5;

	// Token: 0x040005CF RID: 1487
	public const sbyte UI_STACK = 6;

	// Token: 0x040005D0 RID: 1488
	public const sbyte UI_STACK_LOCK = 7;

	// Token: 0x040005D1 RID: 1489
	public const sbyte UI_GROCERY = 8;

	// Token: 0x040005D2 RID: 1490
	public const sbyte UI_GROCERY_LOCK = 9;

	// Token: 0x040005D3 RID: 1491
	public const sbyte UI_UPGRADE = 10;

	// Token: 0x040005D4 RID: 1492
	public const sbyte UI_UPPEARL = 11;

	// Token: 0x040005D5 RID: 1493
	public const sbyte UI_UPPEARL_LOCK = 12;

	// Token: 0x040005D6 RID: 1494
	public const sbyte UI_SPLIT = 13;

	// Token: 0x040005D7 RID: 1495
	public const sbyte UI_STORE = 14;

	// Token: 0x040005D8 RID: 1496
	public const sbyte UI_BOOK = 15;

	// Token: 0x040005D9 RID: 1497
	public const sbyte UI_LIEN = 16;

	// Token: 0x040005DA RID: 1498
	public const sbyte UI_NHAN = 17;

	// Token: 0x040005DB RID: 1499
	public const sbyte UI_NGOCBOI = 18;

	// Token: 0x040005DC RID: 1500
	public const sbyte UI_PHU = 19;

	// Token: 0x040005DD RID: 1501
	public const sbyte UI_NONNAM = 20;

	// Token: 0x040005DE RID: 1502
	public const sbyte UI_NONNU = 21;

	// Token: 0x040005DF RID: 1503
	public const sbyte UI_AONAM = 22;

	// Token: 0x040005E0 RID: 1504
	public const sbyte UI_AONU = 23;

	// Token: 0x040005E1 RID: 1505
	public const sbyte UI_GANGTAYNAM = 24;

	// Token: 0x040005E2 RID: 1506
	public const sbyte UI_GANGTAYNU = 25;

	// Token: 0x040005E3 RID: 1507
	public const sbyte UI_QUANNAM = 26;

	// Token: 0x040005E4 RID: 1508
	public const sbyte UI_QUANNU = 27;

	// Token: 0x040005E5 RID: 1509
	public const sbyte UI_GIAYNAM = 28;

	// Token: 0x040005E6 RID: 1510
	public const sbyte UI_GIAYNU = 29;

	// Token: 0x040005E7 RID: 1511
	public const sbyte UI_TRADE = 30;

	// Token: 0x040005E8 RID: 1512
	public const sbyte UI_UPGRADE_GOLD = 31;

	// Token: 0x040005E9 RID: 1513
	public const sbyte UI_FASHION = 32;

	// Token: 0x040005EA RID: 1514
	public const sbyte UI_CONVERT = 33;

	// Token: 0x040005EB RID: 1515
	public ItemOption[] itemOption;

	// Token: 0x040005EC RID: 1516
	public ItemTemplate template;

	// Token: 0x040005ED RID: 1517
	public MyVector options;

	// Token: 0x040005EE RID: 1518
	public int itemId;

	// Token: 0x040005EF RID: 1519
	public int playerId;

	// Token: 0x040005F0 RID: 1520
	public bool isSelect;

	// Token: 0x040005F1 RID: 1521
	public int indexUI;

	// Token: 0x040005F2 RID: 1522
	public int quantity;

	// Token: 0x040005F3 RID: 1523
	public int quantilyToBuy;

	// Token: 0x040005F4 RID: 1524
	public long powerRequire;

	// Token: 0x040005F5 RID: 1525
	public bool isLock;

	// Token: 0x040005F6 RID: 1526
	public int sys;

	// Token: 0x040005F7 RID: 1527
	public int upgrade;

	// Token: 0x040005F8 RID: 1528
	public int buyCoin;

	// Token: 0x040005F9 RID: 1529
	public int buyCoinLock;

	// Token: 0x040005FA RID: 1530
	public int buyGold;

	// Token: 0x040005FB RID: 1531
	public int buyGoldLock;

	// Token: 0x040005FC RID: 1532
	public int saleCoinLock;

	// Token: 0x040005FD RID: 1533
	public int buySpec;

	// Token: 0x040005FE RID: 1534
	public int buyRuby;

	// Token: 0x040005FF RID: 1535
	public short iconSpec = -1;

	// Token: 0x04000600 RID: 1536
	public sbyte buyType = -1;

	// Token: 0x04000601 RID: 1537
	public int typeUI;

	// Token: 0x04000602 RID: 1538
	public bool isExpires;

	// Token: 0x04000603 RID: 1539
	public bool isBuySpec;

	// Token: 0x04000604 RID: 1540
	public EffectCharPaint eff;

	// Token: 0x04000605 RID: 1541
	public int indexEff;

	// Token: 0x04000606 RID: 1542
	public Image img;

	// Token: 0x04000607 RID: 1543
	public string info;

	// Token: 0x04000608 RID: 1544
	public string content;

	// Token: 0x04000609 RID: 1545
	public string reason = string.Empty;

	// Token: 0x0400060A RID: 1546
	public int compare;

	// Token: 0x0400060B RID: 1547
	public sbyte isMe;

	// Token: 0x0400060C RID: 1548
	public bool newItem;

	// Token: 0x0400060D RID: 1549
	public int headTemp = -1;

	// Token: 0x0400060E RID: 1550
	public int bodyTemp = -1;

	// Token: 0x0400060F RID: 1551
	public int legTemp = -1;

	// Token: 0x04000610 RID: 1552
	public int bagTemp = -1;

	// Token: 0x04000611 RID: 1553
	public int wpTemp = -1;

	// Token: 0x04000612 RID: 1554
	public string nameNguoiKyGui = string.Empty;

	// Token: 0x04000613 RID: 1555
	private int[] color = new int[]
	{
		0,
		0,
		0,
		0,
		600841,
		600841,
		667658,
		667658,
		3346944,
		3346688,
		4199680,
		5052928,
		3276851,
		3932211,
		4587571,
		5046280,
		6684682,
		3359744
	};

	// Token: 0x04000614 RID: 1556
	private int[][] colorBorder = new int[][]
	{
		new int[]
		{
			18687,
			16869,
			15052,
			13235,
			11161,
			9344
		},
		new int[]
		{
			45824,
			39168,
			32768,
			26112,
			19712,
			13056
		},
		new int[]
		{
			16744192,
			15037184,
			13395456,
			11753728,
			10046464,
			8404992
		},
		new int[]
		{
			13500671,
			12058853,
			10682572,
			9371827,
			7995545,
			6684800
		},
		new int[]
		{
			16711705,
			15007767,
			13369364,
			11730962,
			10027023,
			8388621
		}
	};

	// Token: 0x04000615 RID: 1557
	private int[] size = new int[]
	{
		2,
		1,
		1,
		1,
		1,
		1
	};
}
