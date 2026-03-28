using System;

// Token: 0x020000B1 RID: 177
public class Info_RadaScr
{
	// Token: 0x060007F3 RID: 2035 RVA: 0x000721F4 File Offset: 0x000703F4
	public void SetInfo(int id, int no, int idIcon, sbyte rank, sbyte typeMonster, short templateId, string name, string info, global::Char charInfo, ItemOption[] itemOption)
	{
		this.id = id;
		this.no = no;
		this.idIcon = idIcon;
		this.rank = rank;
		this.typeMonster = typeMonster;
		if (templateId != -1)
		{
			this.mobInfo = new Mob();
			this.mobInfo.templateId = (int)templateId;
		}
		this.name = name;
		this.info = info;
		this.charInfo = charInfo;
		this.itemOption = itemOption;
		this.addItemDetail();
	}

	// Token: 0x060007F4 RID: 2036 RVA: 0x00007BEC File Offset: 0x00005DEC
	public void SetAmount(sbyte amount, sbyte max_amount)
	{
		this.amount = amount;
		this.max_amount = max_amount;
	}

	// Token: 0x060007F5 RID: 2037 RVA: 0x00007BFC File Offset: 0x00005DFC
	public void SetLevel(sbyte level)
	{
		this.level = level;
		this.addItemDetail();
	}

	// Token: 0x060007F6 RID: 2038 RVA: 0x00007C0B File Offset: 0x00005E0B
	public void SetUse(sbyte isUse)
	{
		this.isUse = isUse;
		this.addItemDetail();
	}

	// Token: 0x060007F7 RID: 2039 RVA: 0x0007226C File Offset: 0x0007046C
	public static global::Char SetCharInfo(int head, int body, int leg, int bag)
	{
		return new global::Char
		{
			head = head,
			body = body,
			leg = leg,
			bag = bag
		};
	}

	// Token: 0x060007F8 RID: 2040 RVA: 0x0007229C File Offset: 0x0007049C
	public static Info_RadaScr GetInfo(MyVector vec, int id)
	{
		if (vec != null)
		{
			for (int i = 0; i < vec.size(); i++)
			{
				Info_RadaScr info_RadaScr = (Info_RadaScr)vec.elementAt(i);
				if (info_RadaScr != null && info_RadaScr.id == id)
				{
					return info_RadaScr;
				}
			}
		}
		return null;
	}

	// Token: 0x060007F9 RID: 2041 RVA: 0x000722E8 File Offset: 0x000704E8
	public void paintInfo(mGraphics g, int x, int y)
	{
		this.count++;
		if (this.count > this.f.Length - 1)
		{
			this.count = 0;
		}
		if ((int)this.typeMonster == 0)
		{
			if (Mob.arrMobTemplate[this.mobInfo.templateId] != null)
			{
				if (Mob.arrMobTemplate[this.mobInfo.templateId].data != null)
				{
					Mob.arrMobTemplate[this.mobInfo.templateId].data.paintFrame(g, this.f[this.count], x, y, 0, 0);
				}
				else if (this.timeRequest - GameCanvas.timeNow < 0L)
				{
					this.timeRequest = GameCanvas.timeNow + 1500L;
					this.mobInfo.getData();
				}
			}
		}
		else if (this.charInfo != null)
		{
			this.charInfo.paintCharBody(g, x, y, 1, this.f[this.count], true);
		}
	}

	// Token: 0x060007FA RID: 2042 RVA: 0x000723EC File Offset: 0x000705EC
	public void addItemDetail()
	{
		this.cp = new ChatPopup();
		string text = string.Empty;
		string text2 = string.Empty;
		text2 = text2 + "\n|6|" + this.info;
		text2 += "\n--";
		if (this.itemOption != null)
		{
			int num = 0;
			bool flag = true;
			while (flag)
			{
				int num2 = 0;
				for (int i = 0; i < this.itemOption.Length; i++)
				{
					text = this.itemOption[i].getOptionString();
					if (!text.Equals(string.Empty) && num == (int)this.itemOption[i].activeCard)
					{
						num2++;
						break;
					}
				}
				if (num2 == 0)
				{
					break;
				}
				if (num == 0)
				{
					text2 = text2 + "\n|6|2|--" + mResources.unlock + "--";
				}
				else
				{
					string text3 = text2;
					text2 = string.Concat(new object[]
					{
						text3,
						"\n|6|2|--",
						mResources.equip,
						" Lv.",
						num,
						"--"
					});
				}
				for (int j = 0; j < this.itemOption.Length; j++)
				{
					text = this.itemOption[j].getOptionString();
					if (!text.Equals(string.Empty) && num == (int)this.itemOption[j].activeCard)
					{
						string text4 = "1";
						if ((int)this.level == 0)
						{
							text4 = "2";
						}
						else if ((int)this.itemOption[j].activeCard != 0)
						{
							if ((int)this.isUse == 0)
							{
								text4 = "2";
							}
							else if ((int)this.level < (int)this.itemOption[j].activeCard)
							{
								text4 = "2";
							}
						}
						string text3 = text2;
						text2 = string.Concat(new string[]
						{
							text3,
							"\n|",
							text4,
							"|1|",
							text
						});
					}
				}
				if (num2 != 0)
				{
					num++;
				}
			}
		}
		this.popUpDetailInit(this.cp, text2);
	}

	// Token: 0x060007FB RID: 2043 RVA: 0x00072618 File Offset: 0x00070818
	public void popUpDetailInit(ChatPopup cp, string chat)
	{
		cp.sayWidth = RadarScr.wText;
		cp.cx = RadarScr.xText;
		cp.says = mFont.tahoma_7.splitFontArray(chat, cp.sayWidth - 8);
		cp.delay = 10000000;
		cp.c = null;
		cp.ch = cp.says.Length * 12;
		cp.cy = RadarScr.yText;
		cp.strY = 10;
		cp.lim = cp.ch - RadarScr.hText;
		if (cp.lim < 0)
		{
			cp.lim = 0;
		}
	}

	// Token: 0x060007FC RID: 2044 RVA: 0x000726B0 File Offset: 0x000708B0
	public void SetEff()
	{
		if ((int)this.amount == (int)this.max_amount && this.eff.size() == 0)
		{
			int num = Res.random(1, 5);
			for (int i = 0; i < num; i++)
			{
				Position position = new Position();
				position.x = Res.random(5, 25);
				position.y = Res.random(5, 25);
				position.v = i * Res.random(0, 8);
				position.w = 0;
				position.anchor = -1;
				this.eff.addElement(position);
			}
		}
	}

	// Token: 0x060007FD RID: 2045 RVA: 0x00072748 File Offset: 0x00070948
	public void paintEff(mGraphics g, int x, int y)
	{
		this.SetEff();
		for (int i = 0; i < this.eff.size(); i++)
		{
			Position position = (Position)this.eff.elementAt(i);
			if (position != null)
			{
				if (position.w < position.v)
				{
					position.w++;
				}
				if (position.w >= position.v)
				{
					position.anchor = GameCanvas.gameTick / 3 % (RadarScr.fraEff.nFrame + 1);
					if (position.anchor >= RadarScr.fraEff.nFrame)
					{
						this.eff.removeElementAt(i);
						i--;
					}
					else
					{
						RadarScr.fraEff.drawFrame(position.anchor, x + position.x, y + position.y, 0, 3, g);
					}
				}
			}
		}
	}

	// Token: 0x04000F32 RID: 3890
	public const sbyte TYPE_MONSTER = 0;

	// Token: 0x04000F33 RID: 3891
	public const sbyte TYPE_CHARPART = 1;

	// Token: 0x04000F34 RID: 3892
	public sbyte rank;

	// Token: 0x04000F35 RID: 3893
	public sbyte amount;

	// Token: 0x04000F36 RID: 3894
	public sbyte max_amount;

	// Token: 0x04000F37 RID: 3895
	public sbyte typeMonster;

	// Token: 0x04000F38 RID: 3896
	public int id;

	// Token: 0x04000F39 RID: 3897
	public int no;

	// Token: 0x04000F3A RID: 3898
	public int idIcon;

	// Token: 0x04000F3B RID: 3899
	public string name;

	// Token: 0x04000F3C RID: 3900
	public string info;

	// Token: 0x04000F3D RID: 3901
	public sbyte level;

	// Token: 0x04000F3E RID: 3902
	public sbyte isUse;

	// Token: 0x04000F3F RID: 3903
	public global::Char charInfo;

	// Token: 0x04000F40 RID: 3904
	public Mob mobInfo;

	// Token: 0x04000F41 RID: 3905
	public ItemOption[] itemOption;

	// Token: 0x04000F42 RID: 3906
	private int[] f = new int[]
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

	// Token: 0x04000F43 RID: 3907
	private int count;

	// Token: 0x04000F44 RID: 3908
	private long timeRequest;

	// Token: 0x04000F45 RID: 3909
	public ChatPopup cp;

	// Token: 0x04000F46 RID: 3910
	public MyVector eff = new MyVector(string.Empty);
}
