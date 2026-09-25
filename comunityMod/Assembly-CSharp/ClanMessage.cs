using System;

// Token: 0x0200001D RID: 29
public class ClanMessage : IActionListener
{
	// Token: 0x060001C7 RID: 455 RVA: 0x0001A32C File Offset: 0x0001852C
	public static void addMessage(ClanMessage cm, int index, bool upToTop)
	{
		int i = 0;
		while (i < ClanMessage.vMessage.size())
		{
			ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
			if (clanMessage.id == cm.id)
			{
				ClanMessage.vMessage.removeElement(clanMessage);
				if (!upToTop)
				{
					ClanMessage.vMessage.insertElementAt(cm, i);
					return;
				}
				ClanMessage.vMessage.insertElementAt(cm, 0);
				return;
			}
			else
			{
				if (clanMessage.maxCap != 0 && clanMessage.recieve == clanMessage.maxCap)
				{
					ClanMessage.vMessage.removeElement(clanMessage);
				}
				i++;
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

	// Token: 0x060001C8 RID: 456 RVA: 0x0001A3F8 File Offset: 0x000185F8
	public void paint(mGraphics g, int x, int y)
	{
		mFont mFont = mFont.tahoma_7b_dark;
		if (this.role == 0)
		{
			mFont = mFont.tahoma_7b_red;
		}
		else if (this.role == 1)
		{
			mFont = mFont.tahoma_7b_green;
		}
		else if (this.role == 2)
		{
			mFont = mFont.tahoma_7b_green2;
		}
		if (this.type == 0)
		{
			mFont.drawString(g, this.playerName, x + 3, y + 1, 0);
			if (this.color == 0)
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
			mFont.drawString(g, string.Concat(new string[]
			{
				this.playerName,
				" (",
				this.recieve.ToString(),
				"/",
				this.maxCap.ToString(),
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

	// Token: 0x060001C9 RID: 457 RVA: 0x00004887 File Offset: 0x00002A87
	public void perform(int idAction, object p)
	{
	}

	// Token: 0x060001CA RID: 458 RVA: 0x0001A5F0 File Offset: 0x000187F0
	public void update()
	{
		if (this.time != 0L)
		{
			this.timeAgo = (int)(mSystem.currentTimeMillis() / 1000L - this.time);
		}
	}

	// Token: 0x04000345 RID: 837
	public int id;

	// Token: 0x04000346 RID: 838
	public int type;

	// Token: 0x04000347 RID: 839
	public int playerId;

	// Token: 0x04000348 RID: 840
	public string playerName;

	// Token: 0x04000349 RID: 841
	public long time;

	// Token: 0x0400034A RID: 842
	public int headId;

	// Token: 0x0400034B RID: 843
	public string[] chat;

	// Token: 0x0400034C RID: 844
	public sbyte color;

	// Token: 0x0400034D RID: 845
	public sbyte role;

	// Token: 0x0400034E RID: 846
	internal int timeAgo;

	// Token: 0x0400034F RID: 847
	public int recieve;

	// Token: 0x04000350 RID: 848
	public int maxCap;

	// Token: 0x04000351 RID: 849
	public string[] option;

	// Token: 0x04000352 RID: 850
	public static MyVector vMessage = new MyVector();
}
