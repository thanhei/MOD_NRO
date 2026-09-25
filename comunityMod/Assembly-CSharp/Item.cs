using System;

// Token: 0x02000055 RID: 85
public class Item
{
	// Token: 0x0600049F RID: 1183 RVA: 0x0004D359 File Offset: 0x0004B559
	public void getCompare()
	{
		this.compare = GameCanvas.panel.getCompare(this);
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x0004D36C File Offset: 0x0004B56C
	public string getPrice()
	{
		string text = string.Empty;
		if (this.buyCoin <= 0 && this.buyGold <= 0)
		{
			return null;
		}
		if (this.buyCoin > 0 && this.buyGold <= 0)
		{
			text = this.buyCoin.ToString() + mResources.XU;
		}
		else if (this.buyGold > 0 && this.buyCoin <= 0)
		{
			text = this.buyGold.ToString() + mResources.LUONG;
		}
		else if (this.buyCoin > 0 && this.buyGold > 0)
		{
			text = string.Concat(new string[]
			{
				this.buyCoin.ToString(),
				mResources.XU,
				"/",
				this.buyGold.ToString(),
				mResources.LUONG
			});
		}
		return text;
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x0004D43C File Offset: 0x0004B63C
	public void paintUpgradeEffect(int x, int y, int upgrade, mGraphics g)
	{
		int num = GameScr.indexSize - 2;
		int num2 = 0;
		int num3 = ((upgrade >= 4) ? ((upgrade < 8) ? 1 : ((upgrade < 12) ? 2 : ((upgrade > 14) ? 4 : 3))) : 0);
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

	// Token: 0x060004A2 RID: 1186 RVA: 0x0004D810 File Offset: 0x0004BA10
	internal int upgradeEffectY(int tick)
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

	// Token: 0x060004A3 RID: 1187 RVA: 0x0004D858 File Offset: 0x0004BA58
	internal int upgradeEffectX(int tick)
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

	// Token: 0x060004A4 RID: 1188 RVA: 0x0004D8A0 File Offset: 0x0004BAA0
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

	// Token: 0x060004A5 RID: 1189 RVA: 0x0004D8E0 File Offset: 0x0004BAE0
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

	// Token: 0x060004A6 RID: 1190 RVA: 0x0004DA1C File Offset: 0x0004BC1C
	public bool isTypeBody()
	{
		return (0 <= this.template.type && this.template.type < 6) || this.template.type == 32 || this.template.type == 35 || this.template.type == 11 || this.template.type == 23;
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x0004DA84 File Offset: 0x0004BC84
	public string getLockstring()
	{
		if (this.isLock)
		{
			return mResources.LOCKED;
		}
		return mResources.NOLOCK;
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x0004DA99 File Offset: 0x0004BC99
	public string getUpgradestring()
	{
		if (this.template.level < 10 || this.template.type >= 10)
		{
			return mResources.NOTUPGRADE;
		}
		if (this.upgrade == 0)
		{
			return mResources.NOUPGRADE;
		}
		return null;
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x0004DACE File Offset: 0x0004BCCE
	public bool isTypeUIMe()
	{
		return this.typeUI == 5 || this.typeUI == 3 || this.typeUI == 4;
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x0004DAEE File Offset: 0x0004BCEE
	public bool isTypeUIShopView()
	{
		return this.isTypeUIShop() || (this.isTypeUIStore() || this.isTypeUIBook() || this.isTypeUIFashion());
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x0004DB18 File Offset: 0x0004BD18
	public bool isTypeUIShop()
	{
		return this.typeUI == 20 || this.typeUI == 21 || this.typeUI == 22 || this.typeUI == 23 || this.typeUI == 24 || this.typeUI == 25 || this.typeUI == 26 || this.typeUI == 27 || this.typeUI == 28 || this.typeUI == 29 || this.typeUI == 16 || this.typeUI == 17 || this.typeUI == 18 || this.typeUI == 19 || this.typeUI == 2 || this.typeUI == 6 || this.typeUI == 8;
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x0004DBD8 File Offset: 0x0004BDD8
	public bool isTypeUIShopLock()
	{
		return this.typeUI == 7 || this.typeUI == 9;
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x0004DBF0 File Offset: 0x0004BDF0
	public bool isTypeUIStore()
	{
		return this.typeUI == 14;
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x0004DBFF File Offset: 0x0004BDFF
	public bool isTypeUIBook()
	{
		return this.typeUI == 15;
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x0004DC0E File Offset: 0x0004BE0E
	public bool isTypeUIFashion()
	{
		return this.typeUI == 32;
	}

	// Token: 0x060004B0 RID: 1200 RVA: 0x0004DC1D File Offset: 0x0004BE1D
	public bool isUpMax()
	{
		return this.getUpMax() == this.upgrade;
	}

	// Token: 0x060004B1 RID: 1201 RVA: 0x0004DC30 File Offset: 0x0004BE30
	public int getUpMax()
	{
		if (this.template.level >= 1 && this.template.level < 20)
		{
			return 4;
		}
		if (this.template.level >= 20 && this.template.level < 40)
		{
			return 8;
		}
		if (this.template.level >= 40 && this.template.level < 50)
		{
			return 12;
		}
		if (this.template.level >= 50 && this.template.level < 60)
		{
			return 14;
		}
		return 16;
	}

	// Token: 0x060004B2 RID: 1202 RVA: 0x0004DCC0 File Offset: 0x0004BEC0
	public void setPartTemp(int headTemp, int bodyTemp, int legTemp, int bagTemp)
	{
		this.headTemp = headTemp;
		this.bodyTemp = bodyTemp;
		this.legTemp = legTemp;
		this.bagTemp = bagTemp;
	}

	// Token: 0x04000976 RID: 2422
	public const int OPT_STAR = 34;

	// Token: 0x04000977 RID: 2423
	public const int OPT_MOON = 35;

	// Token: 0x04000978 RID: 2424
	public const int OPT_SUN = 36;

	// Token: 0x04000979 RID: 2425
	public const int OPT_COLORNAME = 41;

	// Token: 0x0400097A RID: 2426
	public const int OPT_LVITEM = 72;

	// Token: 0x0400097B RID: 2427
	public const int OPT_STARSLOT = 102;

	// Token: 0x0400097C RID: 2428
	public const int OPT_MAXSTARSLOT = 107;

	// Token: 0x0400097D RID: 2429
	public const int TYPE_BODY_MIN = 0;

	// Token: 0x0400097E RID: 2430
	public const int TYPE_BODY_MAX = 6;

	// Token: 0x0400097F RID: 2431
	public const int TYPE_AO = 0;

	// Token: 0x04000980 RID: 2432
	public const int TYPE_QUAN = 1;

	// Token: 0x04000981 RID: 2433
	public const int TYPE_GANGTAY = 2;

	// Token: 0x04000982 RID: 2434
	public const int TYPE_GIAY = 3;

	// Token: 0x04000983 RID: 2435
	public const int TYPE_RADA = 4;

	// Token: 0x04000984 RID: 2436
	public const int TYPE_HAIR = 5;

	// Token: 0x04000985 RID: 2437
	public const int TYPE_DAUTHAN = 6;

	// Token: 0x04000986 RID: 2438
	public const int TYPE_NGOCRONG = 12;

	// Token: 0x04000987 RID: 2439
	public const int TYPE_SACH = 7;

	// Token: 0x04000988 RID: 2440
	public const int TYPE_NHIEMVU = 8;

	// Token: 0x04000989 RID: 2441
	public const int TYPE_GOLD = 9;

	// Token: 0x0400098A RID: 2442
	public const int TYPE_DIAMOND = 10;

	// Token: 0x0400098B RID: 2443
	public const int TYPE_BALO = 11;

	// Token: 0x0400098C RID: 2444
	public const int TYPE_MOUNT = 23;

	// Token: 0x0400098D RID: 2445
	public const int TYPE_MOUNT_VIP = 24;

	// Token: 0x0400098E RID: 2446
	public const int TYPE_DIAMOND_LOCK = 34;

	// Token: 0x0400098F RID: 2447
	public const int TYPE_TRAINSUIT = 32;

	// Token: 0x04000990 RID: 2448
	public const int TYPE_HAT = 35;

	// Token: 0x04000991 RID: 2449
	public const sbyte UI_WEAPON = 2;

	// Token: 0x04000992 RID: 2450
	public const sbyte UI_BAG = 3;

	// Token: 0x04000993 RID: 2451
	public const sbyte UI_BOX = 4;

	// Token: 0x04000994 RID: 2452
	public const sbyte UI_BODY = 5;

	// Token: 0x04000995 RID: 2453
	public const sbyte UI_STACK = 6;

	// Token: 0x04000996 RID: 2454
	public const sbyte UI_STACK_LOCK = 7;

	// Token: 0x04000997 RID: 2455
	public const sbyte UI_GROCERY = 8;

	// Token: 0x04000998 RID: 2456
	public const sbyte UI_GROCERY_LOCK = 9;

	// Token: 0x04000999 RID: 2457
	public const sbyte UI_UPGRADE = 10;

	// Token: 0x0400099A RID: 2458
	public const sbyte UI_UPPEARL = 11;

	// Token: 0x0400099B RID: 2459
	public const sbyte UI_UPPEARL_LOCK = 12;

	// Token: 0x0400099C RID: 2460
	public const sbyte UI_SPLIT = 13;

	// Token: 0x0400099D RID: 2461
	public const sbyte UI_STORE = 14;

	// Token: 0x0400099E RID: 2462
	public const sbyte UI_BOOK = 15;

	// Token: 0x0400099F RID: 2463
	public const sbyte UI_LIEN = 16;

	// Token: 0x040009A0 RID: 2464
	public const sbyte UI_NHAN = 17;

	// Token: 0x040009A1 RID: 2465
	public const sbyte UI_NGOCBOI = 18;

	// Token: 0x040009A2 RID: 2466
	public const sbyte UI_PHU = 19;

	// Token: 0x040009A3 RID: 2467
	public const sbyte UI_NONNAM = 20;

	// Token: 0x040009A4 RID: 2468
	public const sbyte UI_NONNU = 21;

	// Token: 0x040009A5 RID: 2469
	public const sbyte UI_AONAM = 22;

	// Token: 0x040009A6 RID: 2470
	public const sbyte UI_AONU = 23;

	// Token: 0x040009A7 RID: 2471
	public const sbyte UI_GANGTAYNAM = 24;

	// Token: 0x040009A8 RID: 2472
	public const sbyte UI_GANGTAYNU = 25;

	// Token: 0x040009A9 RID: 2473
	public const sbyte UI_QUANNAM = 26;

	// Token: 0x040009AA RID: 2474
	public const sbyte UI_QUANNU = 27;

	// Token: 0x040009AB RID: 2475
	public const sbyte UI_GIAYNAM = 28;

	// Token: 0x040009AC RID: 2476
	public const sbyte UI_GIAYNU = 29;

	// Token: 0x040009AD RID: 2477
	public const sbyte UI_TRADE = 30;

	// Token: 0x040009AE RID: 2478
	public const sbyte UI_UPGRADE_GOLD = 31;

	// Token: 0x040009AF RID: 2479
	public const sbyte UI_FASHION = 32;

	// Token: 0x040009B0 RID: 2480
	public const sbyte UI_CONVERT = 33;

	// Token: 0x040009B1 RID: 2481
	public ItemOption[] itemOption;

	// Token: 0x040009B2 RID: 2482
	public ItemTemplate template;

	// Token: 0x040009B3 RID: 2483
	public MyVector options;

	// Token: 0x040009B4 RID: 2484
	public int itemId;

	// Token: 0x040009B5 RID: 2485
	public int playerId;

	// Token: 0x040009B6 RID: 2486
	public bool isSelect;

	// Token: 0x040009B7 RID: 2487
	public int indexUI;

	// Token: 0x040009B8 RID: 2488
	public int quantity;

	// Token: 0x040009B9 RID: 2489
	public int quantilyToBuy;

	// Token: 0x040009BA RID: 2490
	public long powerRequire;

	// Token: 0x040009BB RID: 2491
	public bool isLock;

	// Token: 0x040009BC RID: 2492
	public int sys;

	// Token: 0x040009BD RID: 2493
	public int upgrade;

	// Token: 0x040009BE RID: 2494
	public int buyCoin;

	// Token: 0x040009BF RID: 2495
	public int buyCoinLock;

	// Token: 0x040009C0 RID: 2496
	public int buyGold;

	// Token: 0x040009C1 RID: 2497
	public int buyGoldLock;

	// Token: 0x040009C2 RID: 2498
	public int saleCoinLock;

	// Token: 0x040009C3 RID: 2499
	public int buySpec;

	// Token: 0x040009C4 RID: 2500
	public int buyRuby;

	// Token: 0x040009C5 RID: 2501
	public short iconSpec = -1;

	// Token: 0x040009C6 RID: 2502
	public sbyte buyType = -1;

	// Token: 0x040009C7 RID: 2503
	public int typeUI;

	// Token: 0x040009C8 RID: 2504
	public bool isExpires;

	// Token: 0x040009C9 RID: 2505
	public bool isBuySpec;

	// Token: 0x040009CA RID: 2506
	public EffectCharPaint eff;

	// Token: 0x040009CB RID: 2507
	public int indexEff;

	// Token: 0x040009CC RID: 2508
	public Image img;

	// Token: 0x040009CD RID: 2509
	public string info;

	// Token: 0x040009CE RID: 2510
	public string content;

	// Token: 0x040009CF RID: 2511
	public string reason = string.Empty;

	// Token: 0x040009D0 RID: 2512
	public int compare;

	// Token: 0x040009D1 RID: 2513
	public sbyte isMe;

	// Token: 0x040009D2 RID: 2514
	public bool newItem;

	// Token: 0x040009D3 RID: 2515
	public int headTemp = -1;

	// Token: 0x040009D4 RID: 2516
	public int bodyTemp = -1;

	// Token: 0x040009D5 RID: 2517
	public int legTemp = -1;

	// Token: 0x040009D6 RID: 2518
	public int bagTemp = -1;

	// Token: 0x040009D7 RID: 2519
	public int wpTemp = -1;

	// Token: 0x040009D8 RID: 2520
	public string nameNguoiKyGui = string.Empty;

	// Token: 0x040009D9 RID: 2521
	internal int[] color = new int[]
	{
		0, 0, 0, 0, 600841, 600841, 667658, 667658, 3346944, 3346688,
		4199680, 5052928, 3276851, 3932211, 4587571, 5046280, 6684682, 3359744
	};

	// Token: 0x040009DA RID: 2522
	internal int[][] colorBorder = new int[][]
	{
		new int[] { 18687, 16869, 15052, 13235, 11161, 9344 },
		new int[] { 45824, 39168, 32768, 26112, 19712, 13056 },
		new int[] { 16744192, 15037184, 13395456, 11753728, 10046464, 8404992 },
		new int[] { 13500671, 12058853, 10682572, 9371827, 7995545, 6684800 },
		new int[] { 16711705, 15007767, 13369364, 11730962, 10027023, 8388621 }
	};

	// Token: 0x040009DB RID: 2523
	internal int[] size = new int[] { 2, 1, 1, 1, 1, 1 };
}
