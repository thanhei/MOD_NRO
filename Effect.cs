using System;

// Token: 0x0200003A RID: 58
public class Effect
{
	// Token: 0x06000263 RID: 611 RVA: 0x000179C0 File Offset: 0x00015BC0
	public Effect()
	{
	}

	// Token: 0x06000264 RID: 612 RVA: 0x00017A54 File Offset: 0x00015C54
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
			EffectData effectData = new EffectData();
			effectData.ID = id;
			if (id >= 42 && id <= 46)
			{
				id = 106;
			}
			string text = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				id,
				"/data"
			});
			DataInputStream dataInputStream = MyStream.readFile(text);
			if (dataInputStream != null)
			{
				if (id > 100 && id < 200)
				{
					effectData.readData2(text);
				}
				else
				{
					effectData.readData(text);
				}
				effectData.img = GameCanvas.loadImage("/effectdata/" + id + "/img.png");
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

	// Token: 0x06000265 RID: 613 RVA: 0x00017C10 File Offset: 0x00015E10
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
			EffectData effectData = new EffectData();
			effectData.ID = id;
			if (id >= 42 && id <= 46)
			{
				id = 106;
			}
			string text = string.Concat(new object[]
			{
				"/x",
				mGraphics.zoomLevel,
				"/effectdata/",
				id,
				"/data"
			});
			DataInputStream dataInputStream = MyStream.readFile(text);
			if (dataInputStream != null)
			{
				if (id > 100 && id < 200)
				{
					effectData.readData2(text);
				}
				else
				{
					effectData.readData(text);
				}
				effectData.img = GameCanvas.loadImage("/effectdata/" + id + "/img.png");
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
			Effect.lastEff.addElement(this.effId + string.Empty);
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
		if (!Effect.isExistNewEff(this.effId + string.Empty))
		{
			Effect.newEff.addElement(this.effId + string.Empty);
		}
	}

	// Token: 0x06000266 RID: 614 RVA: 0x00017E5C File Offset: 0x0001605C
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

	// Token: 0x06000267 RID: 615 RVA: 0x000055A9 File Offset: 0x000037A9
	public static void addEffData(EffectData eff)
	{
		Effect.vEffData.addElement(eff);
	}

	// Token: 0x06000268 RID: 616 RVA: 0x00017EB0 File Offset: 0x000160B0
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

	// Token: 0x06000269 RID: 617 RVA: 0x00017EF8 File Offset: 0x000160F8
	public static bool isExistNewEff(string id)
	{
		for (int i = 0; i < Effect.newEff.size(); i++)
		{
			string text = (string)Effect.newEff.elementAt(i);
			if (text.Equals(id))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600026A RID: 618 RVA: 0x000055B6 File Offset: 0x000037B6
	public bool isPaintz()
	{
		return this.isPaint;
	}

	// Token: 0x0600026B RID: 619 RVA: 0x00017F40 File Offset: 0x00016140
	public void paintUnderBackground(mGraphics g, int xLayer, int yLayer)
	{
		if (!this.isPaintz())
		{
			return;
		}
		if (Effect.getEffDataById(this.effId).img != null)
		{
			Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x + xLayer, this.y + yLayer, this.trans, this.layer);
		}
	}

	// Token: 0x0600026C RID: 620 RVA: 0x00017FA4 File Offset: 0x000161A4
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

	// Token: 0x0600026D RID: 621 RVA: 0x0001807C File Offset: 0x0001627C
	public void paint(mGraphics g)
	{
		if (!this.isPaint)
		{
			return;
		}
		if (Effect.getEffDataById(this.effId) == null)
		{
			return;
		}
		if (Effect.getEffDataById(this.effId).img != null)
		{
			try
			{
				Effect.getEffDataById(this.effId).paintFrame(g, this.currFrame, this.x, this.y, this.trans, this.layer);
			}
			catch (Exception ex)
			{
			}
		}
	}

	// Token: 0x0600026E RID: 622 RVA: 0x00018108 File Offset: 0x00016308
	public void update()
	{
		try
		{
			if (this.effId >= 42 && this.effId <= 46)
			{
				this.getFrameKhangia();
			}
			else if (Effect.getEffDataById(this.effId) != null)
			{
				if (Effect.getEffDataById(this.effId).img != null)
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
							if ((int)this.c.isMonkey == 0)
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
		}
		catch (Exception ex)
		{
			EffecMn.vEff.removeElement(this);
		}
	}

	// Token: 0x0600026F RID: 623 RVA: 0x000055C6 File Offset: 0x000037C6
	public int getnFrame()
	{
		return this.data.Length;
	}

	// Token: 0x040002A1 RID: 673
	public int effId;

	// Token: 0x040002A2 RID: 674
	public int typeEff;

	// Token: 0x040002A3 RID: 675
	public int indexFrom;

	// Token: 0x040002A4 RID: 676
	public int indexTo;

	// Token: 0x040002A5 RID: 677
	public bool isNearPlayer;

	// Token: 0x040002A6 RID: 678
	public const int NEAR_PLAYER = 0;

	// Token: 0x040002A7 RID: 679
	public const int LOOP_NORMAL = 1;

	// Token: 0x040002A8 RID: 680
	public const int LOOP_TRANS = 2;

	// Token: 0x040002A9 RID: 681
	public const int BACKGROUND = 3;

	// Token: 0x040002AA RID: 682
	public const int CHAR = 4;

	// Token: 0x040002AB RID: 683
	public const int CHAR_PET_EFF = 5;

	// Token: 0x040002AC RID: 684
	public const int FIRE_TD = 0;

	// Token: 0x040002AD RID: 685
	public const int BIRD = 1;

	// Token: 0x040002AE RID: 686
	public const int FIRE_NAMEK = 2;

	// Token: 0x040002AF RID: 687
	public const int FIRE_SAYAI = 3;

	// Token: 0x040002B0 RID: 688
	public const int FROG = 5;

	// Token: 0x040002B1 RID: 689
	public const int CA = 4;

	// Token: 0x040002B2 RID: 690
	public const int ECH = 6;

	// Token: 0x040002B3 RID: 691
	public const int TACKE = 7;

	// Token: 0x040002B4 RID: 692
	public const int RAN = 8;

	// Token: 0x040002B5 RID: 693
	public const int KHI = 9;

	// Token: 0x040002B6 RID: 694
	public const int GACON = 10;

	// Token: 0x040002B7 RID: 695
	public const int DANONG = 11;

	// Token: 0x040002B8 RID: 696
	public const int DANBUOM = 12;

	// Token: 0x040002B9 RID: 697
	public const int QUA = 13;

	// Token: 0x040002BA RID: 698
	public const int THIENTHACH = 14;

	// Token: 0x040002BB RID: 699
	public const int CAVOI = 15;

	// Token: 0x040002BC RID: 700
	public const int NAM = 16;

	// Token: 0x040002BD RID: 701
	public const int RONGTHAN = 17;

	// Token: 0x040002BE RID: 702
	public const int BUOMBAY = 26;

	// Token: 0x040002BF RID: 703
	public const int KHUCGO = 27;

	// Token: 0x040002C0 RID: 704
	public const int DOIBAY = 28;

	// Token: 0x040002C1 RID: 705
	public const int CONMEO = 29;

	// Token: 0x040002C2 RID: 706
	public const int LUATAT = 30;

	// Token: 0x040002C3 RID: 707
	public const int ONGCONG = 31;

	// Token: 0x040002C4 RID: 708
	public const int KHANGIA1 = 42;

	// Token: 0x040002C5 RID: 709
	public const int KHANGIA2 = 43;

	// Token: 0x040002C6 RID: 710
	public const int KHANGIA3 = 44;

	// Token: 0x040002C7 RID: 711
	public const int KHANGIA4 = 45;

	// Token: 0x040002C8 RID: 712
	public const int KHANGIA5 = 46;

	// Token: 0x040002C9 RID: 713
	public global::Char c;

	// Token: 0x040002CA RID: 714
	public int t;

	// Token: 0x040002CB RID: 715
	public int currFrame;

	// Token: 0x040002CC RID: 716
	public int x;

	// Token: 0x040002CD RID: 717
	public int y;

	// Token: 0x040002CE RID: 718
	public int loop;

	// Token: 0x040002CF RID: 719
	public int tLoop;

	// Token: 0x040002D0 RID: 720
	public int tLoopCount;

	// Token: 0x040002D1 RID: 721
	private bool isPaint = true;

	// Token: 0x040002D2 RID: 722
	public int layer;

	// Token: 0x040002D3 RID: 723
	public int isStand;

	// Token: 0x040002D4 RID: 724
	public static MyVector vEffData = new MyVector();

	// Token: 0x040002D5 RID: 725
	public int trans;

	// Token: 0x040002D6 RID: 726
	public long timeExist;

	// Token: 0x040002D7 RID: 727
	public static MyVector lastEff = new MyVector();

	// Token: 0x040002D8 RID: 728
	public static MyVector newEff = new MyVector();

	// Token: 0x040002D9 RID: 729
	public static MyVector dowloadEff = new MyVector();

	// Token: 0x040002DA RID: 730
	private int[] khangia1 = new int[]
	{
		0,
		0,
		0,
		0,
		0,
		1,
		1,
		1,
		1,
		1
	};

	// Token: 0x040002DB RID: 731
	private int[] khangia2 = new int[]
	{
		2,
		2,
		2,
		2,
		2,
		3,
		3,
		3,
		3,
		3
	};

	// Token: 0x040002DC RID: 732
	private int[] khangia3 = new int[]
	{
		4,
		4,
		4,
		4,
		4,
		5,
		5,
		5,
		5,
		5
	};

	// Token: 0x040002DD RID: 733
	private int[] khangia4 = new int[]
	{
		6,
		6,
		6,
		6,
		6,
		7,
		7,
		7,
		7,
		7
	};

	// Token: 0x040002DE RID: 734
	private int[] khangia5 = new int[]
	{
		8,
		8,
		8,
		8,
		8,
		9,
		9,
		9,
		9,
		9
	};

	// Token: 0x040002DF RID: 735
	private bool isGetTime;

	// Token: 0x040002E0 RID: 736
	private short[] data;

	// Token: 0x040002E1 RID: 737
	public int cLastStatusMe;

	// Token: 0x040002E2 RID: 738
	public long cur_time_cLastStatusMe;
}
