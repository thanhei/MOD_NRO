using System;

// Token: 0x0200002B RID: 43
public class Effect
{
	// Token: 0x0600025B RID: 603 RVA: 0x0002E78C File Offset: 0x0002C98C
	public Effect()
	{
	}

	// Token: 0x0600025C RID: 604 RVA: 0x0002E820 File Offset: 0x0002CA20
	public Effect(int id, global::Char c, int layer, int loop, int loopCount, sbyte isStand)
	{
		this.c = c;
		this.effId = id;
		this.layer = layer;
		this.loop = loop;
		this.tLoop = loopCount;
		this.isStand = (int)isStand;
		if (Effect.getEffDataById(id) == null)
		{
			EffectData effectData = new EffectData
			{
				ID = id
			};
			if (id >= 42 && id <= 46)
			{
				id = 106;
			}
			string text = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				id.ToString(),
				"/data"
			});
			if (MyStream.readFile(text) != null)
			{
				if (id > 100 && id < 200)
				{
					effectData.readData2(text);
				}
				else
				{
					effectData.readData(text);
				}
				effectData.img = GameCanvas.loadImage("/effectdata/" + id.ToString() + "/img.png");
			}
			else
			{
				Service.gI().getEffData((short)id);
			}
			Effect.addEffData(effectData);
		}
		this.indexFrom = -1;
		this.indexTo = -1;
		this.trans = -1;
		this.typeEff = 4;
		if (id == 78)
		{
			this.typeEff = 5;
		}
	}

	// Token: 0x0600025D RID: 605 RVA: 0x0002E9C4 File Offset: 0x0002CBC4
	public Effect(int id, int x, int y, int layer, int loop, int loopCount)
	{
		this.x = x;
		this.y = y;
		this.effId = id;
		this.layer = layer;
		this.loop = loop;
		this.tLoop = loopCount;
		if (Effect.getEffDataById(id) == null)
		{
			EffectData effectData = new EffectData
			{
				ID = id
			};
			if (id >= 42 && id <= 46)
			{
				id = 106;
			}
			string text = string.Concat(new string[]
			{
				"/x",
				mGraphics.zoomLevel.ToString(),
				"/effectdata/",
				id.ToString(),
				"/data"
			});
			if (MyStream.readFile(text) != null)
			{
				if (id > 100 && id < 200)
				{
					effectData.readData2(text);
				}
				else
				{
					effectData.readData(text);
				}
				effectData.img = GameCanvas.loadImage("/effectdata/" + id.ToString() + "/img.png");
			}
			else
			{
				Service.gI().getEffData((short)id);
			}
			Effect.addEffData(effectData);
			if (Effect.lastEff.size() > 20)
			{
				Effect.removeEffData(int.Parse((string)Effect.lastEff.elementAt(0)));
				Effect.lastEff.removeElementAt(0);
			}
			Effect.lastEff.addElement(this.effId.ToString() + string.Empty);
		}
		this.indexFrom = -1;
		this.indexTo = -1;
		if (id == 78)
		{
			this.typeEff = 5;
		}
		else
		{
			this.typeEff = 1;
		}
		if (!Effect.isExistNewEff(this.effId.ToString() + string.Empty))
		{
			Effect.newEff.addElement(this.effId.ToString() + string.Empty);
		}
	}

	// Token: 0x0600025E RID: 606 RVA: 0x0002EBF0 File Offset: 0x0002CDF0
	public static void removeEffData(int id)
	{
		for (int i = 0; i < Effect.vEffData.size(); i++)
		{
			EffectData effectData = (EffectData)Effect.vEffData.elementAt(i);
			if (effectData.ID == id)
			{
				Effect.vEffData.removeElement(effectData);
				return;
			}
		}
	}

	// Token: 0x0600025F RID: 607 RVA: 0x0002EC38 File Offset: 0x0002CE38
	public static void addEffData(EffectData eff)
	{
		Effect.vEffData.addElement(eff);
	}

	// Token: 0x06000260 RID: 608 RVA: 0x0002EC48 File Offset: 0x0002CE48
	public static EffectData getEffDataById(int id)
	{
		for (int i = 0; i < Effect.vEffData.size(); i++)
		{
			EffectData effectData = (EffectData)Effect.vEffData.elementAt(i);
			if (effectData.ID == id)
			{
				return effectData;
			}
		}
		return null;
	}

	// Token: 0x06000261 RID: 609 RVA: 0x0002EC88 File Offset: 0x0002CE88
	public static bool isExistNewEff(string id)
	{
		for (int i = 0; i < Effect.newEff.size(); i++)
		{
			if (((string)Effect.newEff.elementAt(i)).Equals(id))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000262 RID: 610 RVA: 0x0002ECC5 File Offset: 0x0002CEC5
	public bool isPaintz()
	{
		return this.isPaint;
	}

	// Token: 0x06000263 RID: 611 RVA: 0x0002ECD4 File Offset: 0x0002CED4
	public void paintUnderBackground(mGraphics g, int xLayer, int yLayer)
	{
		if (this.isPaintz() && Effect.getEffDataById(this.effId).img != null)
		{
			Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x + xLayer, this.y + yLayer, this.trans, this.layer);
		}
	}

	// Token: 0x06000264 RID: 612 RVA: 0x0002ED30 File Offset: 0x0002CF30
	public void getFrameKhangia()
	{
		if (this.effId == 42)
		{
			this.currFrame = this.khangia1[this.t];
		}
		if (this.effId == 43)
		{
			this.currFrame = this.khangia2[this.t];
		}
		if (this.effId == 44)
		{
			this.currFrame = this.khangia3[this.t];
		}
		if (this.effId == 45)
		{
			this.currFrame = this.khangia4[this.t];
		}
		if (this.effId == 46)
		{
			this.currFrame = this.khangia5[this.t];
		}
		this.t++;
		if (this.t > this.khangia1.Length - 1)
		{
			this.t = 0;
		}
	}

	// Token: 0x06000265 RID: 613 RVA: 0x0002EDF8 File Offset: 0x0002CFF8
	public void paint(mGraphics g)
	{
		if (!this.isPaint || Effect.getEffDataById(this.effId) == null || Effect.getEffDataById(this.effId).img == null)
		{
			return;
		}
		try
		{
			Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x, this.y, this.trans, this.layer);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000266 RID: 614 RVA: 0x0002EE74 File Offset: 0x0002D074
	public void update()
	{
		try
		{
			if (this.effId >= 42 && this.effId <= 46)
			{
				this.getFrameKhangia();
			}
			else if (Effect.getEffDataById(this.effId) != null && Effect.getEffDataById(this.effId).img != null)
			{
				if (this.typeEff == 5)
				{
					this.data = Effect.getEffDataById(this.effId).get(this.c.statusMe);
				}
				else
				{
					this.data = Effect.getEffDataById(this.effId).get();
				}
				if (this.data != null)
				{
					if (!this.isGetTime)
					{
						this.isGetTime = true;
						int num = this.data.Length - 1;
						if (num > 0 && this.typeEff != 1)
						{
							this.t = Res.random(0, num);
						}
						if (this.typeEff == 0)
						{
							this.t = Res.random(this.indexFrom, this.indexTo);
						}
					}
					switch (this.typeEff)
					{
					case 0:
						if (Res.inRect(this.x - 50, this.y - 50, 100, 100, global::Char.myCharz().cx, global::Char.myCharz().cy) && this.t > this.indexFrom && this.t < this.indexTo)
						{
							if (this.t < this.indexTo)
							{
								this.t = this.indexTo;
							}
							this.isNearPlayer = true;
						}
						if (!this.isNearPlayer)
						{
							this.t++;
							if (this.t == this.indexTo)
							{
								this.t = this.indexFrom;
							}
						}
						else if (this.t < this.data.Length)
						{
							this.t++;
						}
						break;
					case 1:
					case 3:
						if (this.t < this.data.Length)
						{
							this.t++;
						}
						break;
					case 2:
						if (this.t < this.data.Length)
						{
							this.t++;
						}
						this.tLoopCount++;
						if (this.tLoopCount == this.tLoop)
						{
							this.tLoopCount = 0;
							this.trans = Res.random(0, 2);
						}
						break;
					case 4:
						this.x = this.c.cx;
						this.y = this.c.cy;
						if (this.t < this.data.Length)
						{
							this.t++;
						}
						break;
					case 5:
						this.trans = ((this.c.cdir != 1) ? 1 : 0);
						if (this.c.cdir == 1)
						{
							this.x = this.c.cx - 15;
						}
						else
						{
							this.x = this.c.cx + 15;
						}
						if (this.c.isMonkey == 0)
						{
							this.y = this.c.cy - 25;
						}
						else
						{
							this.y = this.c.cy - 35;
						}
						if (this.t < this.data.Length)
						{
							this.t++;
						}
						break;
					}
					if (this.t == this.data.Length / 2 && (this.effId == 62 || this.effId == 63 || this.effId == 64 || this.effId == 65))
					{
						SoundMn.playSound(this.x, this.y, SoundMn.FIREWORK, SoundMn.volume);
					}
					if (this.t <= this.data.Length - 1)
					{
						this.currFrame = (int)this.data[this.t];
					}
				}
				if (this.t >= this.data.Length - 1)
				{
					if (this.typeEff == 0 || this.typeEff == 3)
					{
						this.isPaint = false;
					}
					if (this.tLoop == -1)
					{
						EffecMn.vEff.removeElement(this);
					}
					if (this.typeEff == 2)
					{
						this.t = 0;
					}
					else
					{
						if (this.typeEff == 1 && this.loop == 1)
						{
							this.isPaint = false;
						}
						if (this.typeEff == 4 || this.typeEff == 5)
						{
							if (this.loop == -1)
							{
								this.t = 0;
							}
							else
							{
								this.tLoopCount++;
								if (this.tLoopCount == this.tLoop)
								{
									this.tLoopCount = 0;
									this.loop--;
									this.t = 0;
									if (this.loop == 0)
									{
										this.c.removeEffChar(0, this.effId);
									}
								}
							}
						}
						else
						{
							this.isNearPlayer = false;
							if (this.loop == -1)
							{
								this.tLoopCount++;
								this.t = 0;
								if (this.tLoopCount == this.tLoop)
								{
									this.tLoopCount = 0;
									if (this.tLoop > 1)
									{
										this.trans = Res.random(0, 2);
									}
								}
							}
							else
							{
								this.tLoopCount++;
								this.t = 0;
								if (this.tLoopCount == this.tLoop)
								{
									this.tLoopCount = 0;
									this.loop--;
									if (this.loop == 0)
									{
										EffecMn.vEff.removeElement(this);
									}
								}
							}
						}
					}
				}
				else
				{
					this.isPaint = true;
				}
			}
		}
		catch (Exception)
		{
			EffecMn.vEff.removeElement(this);
		}
	}

	// Token: 0x06000267 RID: 615 RVA: 0x0002F3F0 File Offset: 0x0002D5F0
	public int getnFrame()
	{
		return this.data.Length;
	}

	// Token: 0x04000505 RID: 1285
	public int effId;

	// Token: 0x04000506 RID: 1286
	public int typeEff;

	// Token: 0x04000507 RID: 1287
	public int indexFrom;

	// Token: 0x04000508 RID: 1288
	public int indexTo;

	// Token: 0x04000509 RID: 1289
	public bool isNearPlayer;

	// Token: 0x0400050A RID: 1290
	public const int NEAR_PLAYER = 0;

	// Token: 0x0400050B RID: 1291
	public const int LOOP_NORMAL = 1;

	// Token: 0x0400050C RID: 1292
	public const int LOOP_TRANS = 2;

	// Token: 0x0400050D RID: 1293
	public const int BACKGROUND = 3;

	// Token: 0x0400050E RID: 1294
	public const int CHAR = 4;

	// Token: 0x0400050F RID: 1295
	public const int CHAR_PET_EFF = 5;

	// Token: 0x04000510 RID: 1296
	public const int FIRE_TD = 0;

	// Token: 0x04000511 RID: 1297
	public const int BIRD = 1;

	// Token: 0x04000512 RID: 1298
	public const int FIRE_NAMEK = 2;

	// Token: 0x04000513 RID: 1299
	public const int FIRE_SAYAI = 3;

	// Token: 0x04000514 RID: 1300
	public const int FROG = 5;

	// Token: 0x04000515 RID: 1301
	public const int CA = 4;

	// Token: 0x04000516 RID: 1302
	public const int ECH = 6;

	// Token: 0x04000517 RID: 1303
	public const int TACKE = 7;

	// Token: 0x04000518 RID: 1304
	public const int RAN = 8;

	// Token: 0x04000519 RID: 1305
	public const int KHI = 9;

	// Token: 0x0400051A RID: 1306
	public const int GACON = 10;

	// Token: 0x0400051B RID: 1307
	public const int DANONG = 11;

	// Token: 0x0400051C RID: 1308
	public const int DANBUOM = 12;

	// Token: 0x0400051D RID: 1309
	public const int QUA = 13;

	// Token: 0x0400051E RID: 1310
	public const int THIENTHACH = 14;

	// Token: 0x0400051F RID: 1311
	public const int CAVOI = 15;

	// Token: 0x04000520 RID: 1312
	public const int NAM = 16;

	// Token: 0x04000521 RID: 1313
	public const int RONGTHAN = 17;

	// Token: 0x04000522 RID: 1314
	public const int BUOMBAY = 26;

	// Token: 0x04000523 RID: 1315
	public const int KHUCGO = 27;

	// Token: 0x04000524 RID: 1316
	public const int DOIBAY = 28;

	// Token: 0x04000525 RID: 1317
	public const int CONMEO = 29;

	// Token: 0x04000526 RID: 1318
	public const int LUATAT = 30;

	// Token: 0x04000527 RID: 1319
	public const int ONGCONG = 31;

	// Token: 0x04000528 RID: 1320
	public const int KHANGIA1 = 42;

	// Token: 0x04000529 RID: 1321
	public const int KHANGIA2 = 43;

	// Token: 0x0400052A RID: 1322
	public const int KHANGIA3 = 44;

	// Token: 0x0400052B RID: 1323
	public const int KHANGIA4 = 45;

	// Token: 0x0400052C RID: 1324
	public const int KHANGIA5 = 46;

	// Token: 0x0400052D RID: 1325
	public global::Char c;

	// Token: 0x0400052E RID: 1326
	public int t;

	// Token: 0x0400052F RID: 1327
	public int currFrame;

	// Token: 0x04000530 RID: 1328
	public int x;

	// Token: 0x04000531 RID: 1329
	public int y;

	// Token: 0x04000532 RID: 1330
	public int loop;

	// Token: 0x04000533 RID: 1331
	public int tLoop;

	// Token: 0x04000534 RID: 1332
	public int tLoopCount;

	// Token: 0x04000535 RID: 1333
	internal bool isPaint = true;

	// Token: 0x04000536 RID: 1334
	public int layer;

	// Token: 0x04000537 RID: 1335
	public int isStand;

	// Token: 0x04000538 RID: 1336
	public static MyVector vEffData = new MyVector();

	// Token: 0x04000539 RID: 1337
	public int trans;

	// Token: 0x0400053A RID: 1338
	public long timeExist;

	// Token: 0x0400053B RID: 1339
	public static MyVector lastEff = new MyVector();

	// Token: 0x0400053C RID: 1340
	public static MyVector newEff = new MyVector();

	// Token: 0x0400053D RID: 1341
	public static MyVector dowloadEff = new MyVector();

	// Token: 0x0400053E RID: 1342
	internal int[] khangia1 = new int[] { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 };

	// Token: 0x0400053F RID: 1343
	internal int[] khangia2 = new int[] { 2, 2, 2, 2, 2, 3, 3, 3, 3, 3 };

	// Token: 0x04000540 RID: 1344
	internal int[] khangia3 = new int[] { 4, 4, 4, 4, 4, 5, 5, 5, 5, 5 };

	// Token: 0x04000541 RID: 1345
	internal int[] khangia4 = new int[] { 6, 6, 6, 6, 6, 7, 7, 7, 7, 7 };

	// Token: 0x04000542 RID: 1346
	internal int[] khangia5 = new int[] { 8, 8, 8, 8, 8, 9, 9, 9, 9, 9 };

	// Token: 0x04000543 RID: 1347
	internal bool isGetTime;

	// Token: 0x04000544 RID: 1348
	internal short[] data;

	// Token: 0x04000545 RID: 1349
	public int cLastStatusMe;

	// Token: 0x04000546 RID: 1350
	public long cur_time_cLastStatusMe;
}
