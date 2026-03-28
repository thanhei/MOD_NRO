using System;

// Token: 0x0200006C RID: 108
public class MagicTree : Npc, IActionListener
{
	// Token: 0x060003CC RID: 972 RVA: 0x0002473C File Offset: 0x0002293C
	public MagicTree(int npcId, int status, int cx, int cy, int templateId, int iconId) : base(npcId, status, cx, cy, templateId, iconId)
	{
		this.p = new PopUp(string.Empty, 0, 0);
		this.p.command = new Command(null, this, 1, null);
		PopUp.addPopUp(this.p);
	}

	// Token: 0x060003CD RID: 973 RVA: 0x0002478C File Offset: 0x0002298C
	public override void paint(mGraphics g)
	{
		if (this.id == 0)
		{
			return;
		}
		SmallImage.drawSmallImage(g, this.id, this.cx, this.cy, 0, StaticObj.BOTTOM_HCENTER);
		if (global::Char.myCharz().npcFocus != null && global::Char.myCharz().npcFocus.Equals(this))
		{
			g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 1, mGraphics.BOTTOM | mGraphics.HCENTER);
			if (this.name != null)
			{
				mFont.tahoma_7b_white.drawString(g, this.name, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 20, mFont.CENTER, mFont.tahoma_7_grey);
			}
		}
		else if (this.name != null)
		{
			mFont.tahoma_7b_white.drawString(g, this.name, this.cx, this.cy - SmallImage.smallImg[this.id][4] - 17, mFont.CENTER, mFont.tahoma_7_grey);
		}
		try
		{
			for (int i = 0; i < this.currPeas; i++)
			{
				g.drawImage(MagicTree.pea, this.cx + this.peaPostionX[i] - SmallImage.smallImg[this.id][3] / 2, this.cy + this.peaPostionY[i] - SmallImage.smallImg[this.id][4], 0);
			}
		}
		catch (Exception ex)
		{
		}
		if (this.indexEffTask >= 0 && this.effTask != null && (int)this.cTypePk == 0)
		{
			SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx, this.cy - 15 + this.effTask.arrEfInfo[this.indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			if (GameCanvas.gameTick % 2 == 0)
			{
				this.indexEffTask++;
				if (this.indexEffTask >= this.effTask.arrEfInfo.Length)
				{
					this.indexEffTask = 0;
				}
			}
		}
	}

	// Token: 0x060003CE RID: 974 RVA: 0x000249F0 File Offset: 0x00022BF0
	public override void update()
	{
		this.p.isPaint = MagicTree.isPaint;
		this.cur = mSystem.currentTimeMillis();
		if (this.cur - this.last >= 1000L)
		{
			this.seconds--;
			this.last = this.cur;
			if (this.seconds < 0)
			{
				this.seconds = 0;
			}
		}
		if (!this.isUpdate)
		{
			if (this.currPeas < this.maxPeas && this.seconds == 0)
			{
				this.waitToUpdate = true;
			}
		}
		else if (this.seconds == 0)
		{
			this.isUpdate = false;
			this.waitToUpdate = true;
		}
		if (this.waitToUpdate)
		{
			this.delay++;
			if (this.delay == 20)
			{
				this.delay = 0;
				this.waitToUpdate = false;
				Service.gI().getMagicTree(2);
			}
		}
		this.num = ((this.peaPostionX == null) ? 0 : (this.peaPostionX.Length * this.currPeas / this.maxPeas));
		if (this.isUpdateTree)
		{
			this.isUpdateTree = false;
			if ((this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate) || this.isPeasEffect)
			{
				this.p.updateXYWH(new string[]
				{
					this.isUpdate ? mResources.UPGRADING : (this.currPeas + "/" + this.maxPeas),
					NinjaUtil.getTime(this.seconds)
				}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
			}
			else if (this.currPeas == this.maxPeas && !this.isUpdate)
			{
				this.p.updateXYWH(new string[]
				{
					mResources.can_harvest,
					this.currPeas + "/" + this.maxPeas
				}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
			}
		}
		if ((this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate))
		{
			this.p.says[this.p.says.Length - 1] = NinjaUtil.getTime(this.seconds);
		}
		if (this.isPeasEffect)
		{
			this.p.isPaint = false;
			ServerEffect.addServerEffect(98, this.cx + this.peaPostionX[this.currPeas - 1] - SmallImage.smallImg[this.id][3] / 2, this.cy + this.peaPostionY[this.currPeas - 1] - SmallImage.smallImg[this.id][4], 1);
			this.currPeas--;
			if (GameCanvas.gameTick % 2 == 0)
			{
				SoundMn.gI().HP_MPup();
			}
			if (this.currPeas == this.remainPeas)
			{
				this.p.isPaint = true;
				this.isUpdateTree = true;
				this.isPeasEffect = false;
			}
		}
		base.update();
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00005F5E File Offset: 0x0000415E
	public void perform(int idAction, object p)
	{
		if (idAction == 1)
		{
			Service.gI().magicTree(1);
		}
	}

	// Token: 0x0400067D RID: 1661
	public static Image imgMagicTree;

	// Token: 0x0400067E RID: 1662
	public static Image pea = GameCanvas.loadImage("/mainImage/myTexture2dhatdau.png");

	// Token: 0x0400067F RID: 1663
	public int id;

	// Token: 0x04000680 RID: 1664
	public int level;

	// Token: 0x04000681 RID: 1665
	public int x;

	// Token: 0x04000682 RID: 1666
	public int y;

	// Token: 0x04000683 RID: 1667
	public int currPeas;

	// Token: 0x04000684 RID: 1668
	public int remainPeas;

	// Token: 0x04000685 RID: 1669
	public int maxPeas;

	// Token: 0x04000686 RID: 1670
	public new string strInfo;

	// Token: 0x04000687 RID: 1671
	public string name;

	// Token: 0x04000688 RID: 1672
	public int timeToRecieve;

	// Token: 0x04000689 RID: 1673
	public bool isUpdate;

	// Token: 0x0400068A RID: 1674
	public int[] peaPostionX;

	// Token: 0x0400068B RID: 1675
	public int[] peaPostionY;

	// Token: 0x0400068C RID: 1676
	private int num;

	// Token: 0x0400068D RID: 1677
	public PopUp p;

	// Token: 0x0400068E RID: 1678
	public bool isUpdateTree;

	// Token: 0x0400068F RID: 1679
	public new static bool isPaint = true;

	// Token: 0x04000690 RID: 1680
	public bool isPeasEffect;

	// Token: 0x04000691 RID: 1681
	public new int seconds;

	// Token: 0x04000692 RID: 1682
	public new long last;

	// Token: 0x04000693 RID: 1683
	public new long cur;

	// Token: 0x04000694 RID: 1684
	private int wPopUp;

	// Token: 0x04000695 RID: 1685
	private bool waitToUpdate;

	// Token: 0x04000696 RID: 1686
	private int delay;
}
