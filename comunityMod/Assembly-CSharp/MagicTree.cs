using System;

// Token: 0x02000064 RID: 100
public class MagicTree : Npc, IActionListener
{
	// Token: 0x06000516 RID: 1302 RVA: 0x0005154C File Offset: 0x0004F74C
	public MagicTree(int npcId, int status, int cx, int cy, int templateId, int iconId)
		: base(npcId, status, cx, cy, templateId, iconId)
	{
		this.p = new PopUp(string.Empty, 0, 0);
		this.p.command = new Command(null, this, 1, null);
		PopUp.addPopUp(this.p);
	}

	// Token: 0x06000517 RID: 1303 RVA: 0x0005159C File Offset: 0x0004F79C
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
		catch (Exception)
		{
		}
		if (this.indexEffTask < 0 || this.effTask == null || this.cTypePk != 0)
		{
			return;
		}
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

	// Token: 0x06000518 RID: 1304 RVA: 0x000517D4 File Offset: 0x0004F9D4
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
		this.num = ((this.peaPostionX != null) ? (this.peaPostionX.Length * this.currPeas / this.maxPeas) : 0);
		if (this.isUpdateTree)
		{
			this.isUpdateTree = false;
			if ((this.seconds >= 0 && this.currPeas < this.maxPeas) || (this.seconds >= 0 && this.isUpdate) || this.isPeasEffect)
			{
				this.p.updateXYWH(new string[]
				{
					this.isUpdate ? mResources.UPGRADING : (this.currPeas.ToString() + "/" + this.maxPeas.ToString()),
					NinjaUtil.getTime(this.seconds)
				}, this.cx, this.cy - 20 - SmallImage.smallImg[this.id][4]);
			}
			else if (this.currPeas == this.maxPeas && !this.isUpdate)
			{
				this.p.updateXYWH(new string[]
				{
					mResources.can_harvest,
					this.currPeas.ToString() + "/" + this.maxPeas.ToString()
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

	// Token: 0x06000519 RID: 1305 RVA: 0x00051B03 File Offset: 0x0004FD03
	public void perform(int idAction, object p)
	{
		if (idAction == 1)
		{
			Service.gI().magicTree(1);
		}
	}

	// Token: 0x04000AB2 RID: 2738
	public static Image imgMagicTree;

	// Token: 0x04000AB3 RID: 2739
	public static Image pea = GameCanvas.loadImage("/mainImage/myTexture2dhatdau.png");

	// Token: 0x04000AB4 RID: 2740
	public int id;

	// Token: 0x04000AB5 RID: 2741
	public int level;

	// Token: 0x04000AB6 RID: 2742
	public int x;

	// Token: 0x04000AB7 RID: 2743
	public int y;

	// Token: 0x04000AB8 RID: 2744
	public int currPeas;

	// Token: 0x04000AB9 RID: 2745
	public int remainPeas;

	// Token: 0x04000ABA RID: 2746
	public int maxPeas;

	// Token: 0x04000ABB RID: 2747
	public new string strInfo;

	// Token: 0x04000ABC RID: 2748
	public string name;

	// Token: 0x04000ABD RID: 2749
	public int timeToRecieve;

	// Token: 0x04000ABE RID: 2750
	public bool isUpdate;

	// Token: 0x04000ABF RID: 2751
	public int[] peaPostionX;

	// Token: 0x04000AC0 RID: 2752
	public int[] peaPostionY;

	// Token: 0x04000AC1 RID: 2753
	internal int num;

	// Token: 0x04000AC2 RID: 2754
	public PopUp p;

	// Token: 0x04000AC3 RID: 2755
	public bool isUpdateTree;

	// Token: 0x04000AC4 RID: 2756
	public new static bool isPaint = true;

	// Token: 0x04000AC5 RID: 2757
	public bool isPeasEffect;

	// Token: 0x04000AC6 RID: 2758
	public new int seconds;

	// Token: 0x04000AC7 RID: 2759
	public new long last;

	// Token: 0x04000AC8 RID: 2760
	public new long cur;

	// Token: 0x04000AC9 RID: 2761
	internal int wPopUp;

	// Token: 0x04000ACA RID: 2762
	internal bool waitToUpdate;

	// Token: 0x04000ACB RID: 2763
	internal int delay;
}
