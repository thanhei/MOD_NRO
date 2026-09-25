using System;
using UnityEngine;

// Token: 0x0200005C RID: 92
public class ItemTime
{
	// Token: 0x060004D3 RID: 1235 RVA: 0x0004E833 File Offset: 0x0004CA33
	public ItemTime()
	{
	}

	// Token: 0x060004D4 RID: 1236 RVA: 0x0004E843 File Offset: 0x0004CA43
	internal ItemTime(short idIcon, int time, bool isEquivalence)
		: this(idIcon, time)
	{
		this.isEquivalence = isEquivalence;
	}

	// Token: 0x060004D5 RID: 1237 RVA: 0x0004E854 File Offset: 0x0004CA54
	internal ItemTime(short idIcon, bool isInfinity)
	{
		this.idIcon = idIcon;
		this.isInfinity = isInfinity;
	}

	// Token: 0x060004D6 RID: 1238 RVA: 0x0004E874 File Offset: 0x0004CA74
	public ItemTime(short idIcon, int s)
	{
		this.idIcon = idIcon;
		this.minute = s / 60;
		this.second = s % 60;
		this.time = s;
		this.coutTime = s;
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.isPaint_coolDownBar = idIcon == 14;
	}

	// Token: 0x060004D7 RID: 1239 RVA: 0x0004E8D8 File Offset: 0x0004CAD8
	public void initTimeText(sbyte id, string text, int time)
	{
		if (time == -1)
		{
			this.dontClear = true;
		}
		else
		{
			this.dontClear = false;
		}
		this.isText = true;
		this.minute = time / 60;
		this.second = time % 60;
		this.idIcon = (short)id;
		this.time = time;
		this.coutTime = time;
		this.text = text;
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.isPaint_coolDownBar = this.idIcon == 14;
	}

	// Token: 0x060004D8 RID: 1240 RVA: 0x0004E954 File Offset: 0x0004CB54
	public void initTime(int time, bool isText)
	{
		this.minute = time / 60;
		this.second = time % 60;
		this.time = time;
		this.coutTime = time;
		this.isText = isText;
		this.curr = (this.last = mSystem.currentTimeMillis());
	}

	// Token: 0x060004D9 RID: 1241 RVA: 0x0004E9A0 File Offset: 0x0004CBA0
	public static bool isExistItem(int id)
	{
		for (int i = 0; i < global::Char.vItemTime.size(); i++)
		{
			if ((int)((ItemTime)global::Char.vItemTime.elementAt(i)).idIcon == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004DA RID: 1242 RVA: 0x0004E9E0 File Offset: 0x0004CBE0
	public static ItemTime getMessageById(int id)
	{
		for (int i = 0; i < GameScr.textTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)GameScr.textTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return itemTime;
			}
		}
		return null;
	}

	// Token: 0x060004DB RID: 1243 RVA: 0x0004EA20 File Offset: 0x0004CC20
	public static bool isExistMessage(int id)
	{
		for (int i = 0; i < GameScr.textTime.size(); i++)
		{
			if ((int)((ItemTime)GameScr.textTime.elementAt(i)).idIcon == id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060004DC RID: 1244 RVA: 0x0004EA60 File Offset: 0x0004CC60
	public static ItemTime getItemById(int id)
	{
		for (int i = 0; i < global::Char.vItemTime.size(); i++)
		{
			ItemTime itemTime = (ItemTime)global::Char.vItemTime.elementAt(i);
			if ((int)itemTime.idIcon == id)
			{
				return itemTime;
			}
		}
		return null;
	}

	// Token: 0x060004DD RID: 1245 RVA: 0x0004EAA0 File Offset: 0x0004CCA0
	public void initTime(int time)
	{
		this.minute = time / 60;
		this.second = time % 60;
		this.coutTime = time;
		this.curr = (this.last = mSystem.currentTimeMillis());
	}

	// Token: 0x060004DE RID: 1246 RVA: 0x0004EADC File Offset: 0x0004CCDC
	public void paint(mGraphics g, int x, int y)
	{
		SmallImage.drawSmallImage(g, (int)this.idIcon, x, y, 0, 3);
		string text;
		if (!this.isInfinity)
		{
			text = this.minute.ToString() + "'" + this.second.ToString() + "s";
			if (this.minute == 0)
			{
				text = this.second.ToString() + "s";
				if (this.second < 10)
				{
					text = this.second.ToString() + "." + this.tenthSecond.ToString() + "s";
				}
			}
			if (this.isEquivalence)
			{
				text = "~" + text;
			}
		}
		else
		{
			text = "∞";
		}
		mFont.tahoma_7b_white.drawString(g, text, x, y + 15, 2, mFont.tahoma_7b_dark);
	}

	// Token: 0x060004DF RID: 1247 RVA: 0x0004EBAC File Offset: 0x0004CDAC
	public void paintText(mGraphics g, int x, int y)
	{
		if (this.isPaint_coolDownBar)
		{
			if (global::Char.myCharz() != null)
			{
				int num = 80;
				int num2 = GameCanvas.w / 2 - num / 2;
				int num3 = GameCanvas.h - 80;
				g.setColor(8421504);
				g.fillRect(num2, num3, num, 2);
				g.setColor(16777215);
				if (this.per > 0)
				{
					g.fillRect(num2, num3, num * this.per / 100, 2);
				}
			}
			return;
		}
		string text = this.minute.ToString() + "'" + this.second.ToString() + "s";
		if (this.minute < 1)
		{
			text = this.second.ToString() + "s";
		}
		if (this.minute < 0)
		{
			text = string.Empty;
		}
		if (this.dontClear)
		{
			text = string.Empty;
		}
		mFont.tahoma_7b_white.drawString(g, this.text + " " + text, x, y, 0, mFont.tahoma_7b_dark);
	}

	// Token: 0x060004E0 RID: 1248 RVA: 0x0004ECA4 File Offset: 0x0004CEA4
	public void update()
	{
		if (this.isInfinity)
		{
			return;
		}
		this.curr = mSystem.currentTimeMillis();
		this.tenthSecond = Mathf.Clamp((int)(9L - (this.curr - this.last) / 100L), 0, 9);
		if (this.curr - this.last >= 1000L)
		{
			this.last = mSystem.currentTimeMillis();
			this.second--;
			this.coutTime--;
			if (this.second == -1)
			{
				this.second = 59;
				this.minute--;
			}
			if (this.time > 0)
			{
				this.per = this.coutTime * 100 / this.time;
			}
		}
		if (this.minute < 0 && !this.isText)
		{
			global::Char.vItemTime.removeElement(this);
		}
		if (this.minute < 0 && this.isText && !this.dontClear)
		{
			GameScr.textTime.removeElement(this);
		}
	}

	// Token: 0x04000A15 RID: 2581
	public short idIcon;

	// Token: 0x04000A16 RID: 2582
	public int second;

	// Token: 0x04000A17 RID: 2583
	public int minute;

	// Token: 0x04000A18 RID: 2584
	internal long curr;

	// Token: 0x04000A19 RID: 2585
	internal long last;

	// Token: 0x04000A1A RID: 2586
	internal bool isText;

	// Token: 0x04000A1B RID: 2587
	internal bool dontClear;

	// Token: 0x04000A1C RID: 2588
	internal string text;

	// Token: 0x04000A1D RID: 2589
	internal bool isPaint_coolDownBar;

	// Token: 0x04000A1E RID: 2590
	public int time;

	// Token: 0x04000A1F RID: 2591
	public int coutTime;

	// Token: 0x04000A20 RID: 2592
	internal int per = 100;

	// Token: 0x04000A21 RID: 2593
	internal bool isEquivalence;

	// Token: 0x04000A22 RID: 2594
	internal bool isInfinity;

	// Token: 0x04000A23 RID: 2595
	internal int tenthSecond;
}
