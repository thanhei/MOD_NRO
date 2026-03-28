using System;

// Token: 0x02000033 RID: 51
public class ClanMessage : IActionListener
{
	// Token: 0x06000225 RID: 549 RVA: 0x00013DF8 File Offset: 0x00011FF8
	public static void addMessage(ClanMessage cm, int index, bool upToTop)
	{
		for (int i = 0; i < ClanMessage.vMessage.size(); i++)
		{
			ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
			if (clanMessage.id == cm.id)
			{
				ClanMessage.vMessage.removeElement(clanMessage);
				if (!upToTop)
				{
					ClanMessage.vMessage.insertElementAt(cm, i);
				}
				else
				{
					ClanMessage.vMessage.insertElementAt(cm, 0);
				}
				return;
			}
			if (clanMessage.maxCap != 0 && clanMessage.recieve == clanMessage.maxCap)
			{
				ClanMessage.vMessage.removeElement(clanMessage);
			}
		}
		if (index == -1)
		{
			ClanMessage.vMessage.addElement(cm);
		}
		else
		{
			ClanMessage.vMessage.insertElementAt(cm, 0);
		}
		if (ClanMessage.vMessage.size() > 20)
		{
			ClanMessage.vMessage.removeElementAt(ClanMessage.vMessage.size() - 1);
		}
	}

	// Token: 0x06000226 RID: 550 RVA: 0x00013EE4 File Offset: 0x000120E4
	public void paint(mGraphics g, int x, int y)
	{
		mFont mFont = mFont.tahoma_7b_dark;
		if ((int)this.role == 0)
		{
			mFont = mFont.tahoma_7b_red;
		}
		else if ((int)this.role == 1)
		{
			mFont = mFont.tahoma_7b_green;
		}
		else if ((int)this.role == 2)
		{
			mFont = mFont.tahoma_7b_green2;
		}
		if (this.type == 0)
		{
			mFont.drawString(g, this.playerName, x + 3, y + 1, 0);
			if ((int)this.color == 0)
			{
				mFont.tahoma_7_grey.drawString(g, this.chat[0] + ((this.chat.Length <= 1) ? string.Empty : "..."), x + 3, y + 11, 0);
			}
			else
			{
				mFont.tahoma_7_red.drawString(g, this.chat[0] + ((this.chat.Length <= 1) ? string.Empty : "..."), x + 3, y + 11, 0);
			}
			mFont.tahoma_7_grey.drawString(g, NinjaUtil.getTimeAgo(this.timeAgo) + " " + mResources.ago, x + GameCanvas.panel.wScroll - 3, y + 1, mFont.RIGHT);
		}
		if (this.type == 1)
		{
			mFont.drawString(g, string.Concat(new object[]
			{
				this.playerName,
				" (",
				this.recieve,
				"/",
				this.maxCap,
				")"
			}), x + 3, y + 1, 0);
			mFont.tahoma_7_blue.drawString(g, string.Concat(new string[]
			{
				mResources.request_pea,
				" ",
				NinjaUtil.getTimeAgo(this.timeAgo),
				" ",
				mResources.ago
			}), x + 3, y + 11, 0);
		}
		if (this.type == 2)
		{
			mFont.drawString(g, this.playerName, x + 3, y + 1, 0);
			mFont.tahoma_7_blue.drawString(g, mResources.request_join_clan, x + 3, y + 11, 0);
		}
	}

	// Token: 0x06000227 RID: 551 RVA: 0x000045ED File Offset: 0x000027ED
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x06000228 RID: 552 RVA: 0x000053A1 File Offset: 0x000035A1
	public void update()
	{
		if (this.time != 0L)
		{
			this.timeAgo = (int)(mSystem.currentTimeMillis() / 1000L - this.time);
		}
	}

	// Token: 0x04000204 RID: 516
	public int id;

	// Token: 0x04000205 RID: 517
	public int type;

	// Token: 0x04000206 RID: 518
	public int playerId;

	// Token: 0x04000207 RID: 519
	public string playerName;

	// Token: 0x04000208 RID: 520
	public long time;

	// Token: 0x04000209 RID: 521
	public int headId;

	// Token: 0x0400020A RID: 522
	public string[] chat;

	// Token: 0x0400020B RID: 523
	public sbyte color;

	// Token: 0x0400020C RID: 524
	public sbyte role;

	// Token: 0x0400020D RID: 525
	private int timeAgo;

	// Token: 0x0400020E RID: 526
	public int recieve;

	// Token: 0x0400020F RID: 527
	public int maxCap;

	// Token: 0x04000210 RID: 528
	public string[] option;

	// Token: 0x04000211 RID: 529
	public static MyVector vMessage = new MyVector();
}
