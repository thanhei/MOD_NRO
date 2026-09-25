using System;

// Token: 0x02000052 RID: 82
public class Info_RadaScr
{
	// Token: 0x06000489 RID: 1161 RVA: 0x0004CB7C File Offset: 0x0004AD7C
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

	// Token: 0x0600048A RID: 1162 RVA: 0x0004CBF1 File Offset: 0x0004ADF1
	public void SetAmount(sbyte amount, sbyte max_amount)
	{
		this.amount = amount;
		this.max_amount = max_amount;
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x0004CC01 File Offset: 0x0004AE01
	public void SetLevel(sbyte level)
	{
		this.level = level;
		this.addItemDetail();
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x0004CC10 File Offset: 0x0004AE10
	public void SetUse(sbyte isUse)
	{
		this.isUse = isUse;
		this.addItemDetail();
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0004CC1F File Offset: 0x0004AE1F
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

	// Token: 0x0600048E RID: 1166 RVA: 0x0004CC44 File Offset: 0x0004AE44
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

	// Token: 0x0600048F RID: 1167 RVA: 0x0004CC84 File Offset: 0x0004AE84
	public void paintInfo(mGraphics g, int x, int y)
	{
		this.count++;
		if (this.count > this.f.Length - 1)
		{
			this.count = 0;
		}
		if (this.typeMonster == 0)
		{
			if (Mob.arrMobTemplate[this.mobInfo.templateId] != null)
			{
				if (Mob.arrMobTemplate[this.mobInfo.templateId].data != null)
				{
					Mob.arrMobTemplate[this.mobInfo.templateId].data.paintFrame(g, this.f[this.count], x, y, 0, 0);
					return;
				}
				if (this.timeRequest - GameCanvas.timeNow < 0L)
				{
					this.timeRequest = GameCanvas.timeNow + 1500L;
					this.mobInfo.getData();
					return;
				}
			}
		}
		else if (this.charInfo != null)
		{
			this.charInfo.paintCharBody(g, x, y, 1, this.f[this.count], true);
		}
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0004CD74 File Offset: 0x0004AF74
	public void addItemDetail()
	{
		this.cp = new ChatPopup();
		string text = string.Empty;
		string text2 = string.Empty + "\n|6|" + this.info + "\n--";
		if (this.itemOption != null)
		{
			int num = 0;
			bool flag = true;
			while (flag)
			{
				int num2 = 0;
				for (int i = 0; i < this.itemOption.Length; i++)
				{
					if (!this.itemOption[i].getOptionString().Equals(string.Empty) && num == (int)this.itemOption[i].activeCard)
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
					text2 = string.Concat(new string[]
					{
						text3,
						"\n|6|2|--",
						mResources.equip,
						" Lv.",
						num.ToString(),
						"--"
					});
				}
				for (int j = 0; j < this.itemOption.Length; j++)
				{
					text = this.itemOption[j].getOptionString();
					if (!text.Equals(string.Empty) && num == (int)this.itemOption[j].activeCard)
					{
						string text4 = "1";
						if (this.level == 0)
						{
							text4 = "2";
						}
						else if (this.itemOption[j].activeCard != 0)
						{
							if (this.isUse == 0)
							{
								text4 = "2";
							}
							else if (this.level < this.itemOption[j].activeCard)
							{
								text4 = "2";
							}
						}
						string text5 = text2;
						text2 = string.Concat(new string[] { text5, "\n|", text4, "|1|", text });
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

	// Token: 0x06000491 RID: 1169 RVA: 0x0004CF68 File Offset: 0x0004B168
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

	// Token: 0x06000492 RID: 1170 RVA: 0x0004CFFC File Offset: 0x0004B1FC
	public void SetEff()
	{
		if (this.amount == this.max_amount && this.eff.size() == 0)
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

	// Token: 0x06000493 RID: 1171 RVA: 0x0004D084 File Offset: 0x0004B284
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

	// Token: 0x0400095E RID: 2398
	public const sbyte TYPE_MONSTER = 0;

	// Token: 0x0400095F RID: 2399
	public const sbyte TYPE_CHARPART = 1;

	// Token: 0x04000960 RID: 2400
	public sbyte rank;

	// Token: 0x04000961 RID: 2401
	public sbyte amount;

	// Token: 0x04000962 RID: 2402
	public sbyte max_amount;

	// Token: 0x04000963 RID: 2403
	public sbyte typeMonster;

	// Token: 0x04000964 RID: 2404
	public int id;

	// Token: 0x04000965 RID: 2405
	public int no;

	// Token: 0x04000966 RID: 2406
	public int idIcon;

	// Token: 0x04000967 RID: 2407
	public string name;

	// Token: 0x04000968 RID: 2408
	public string info;

	// Token: 0x04000969 RID: 2409
	public sbyte level;

	// Token: 0x0400096A RID: 2410
	public sbyte isUse;

	// Token: 0x0400096B RID: 2411
	public global::Char charInfo;

	// Token: 0x0400096C RID: 2412
	public Mob mobInfo;

	// Token: 0x0400096D RID: 2413
	public ItemOption[] itemOption;

	// Token: 0x0400096E RID: 2414
	internal int[] f = new int[] { 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 };

	// Token: 0x0400096F RID: 2415
	internal int count;

	// Token: 0x04000970 RID: 2416
	internal long timeRequest;

	// Token: 0x04000971 RID: 2417
	public ChatPopup cp;

	// Token: 0x04000972 RID: 2418
	public MyVector eff = new MyVector(string.Empty);
}
