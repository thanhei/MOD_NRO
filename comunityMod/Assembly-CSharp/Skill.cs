using System;

// Token: 0x020000A0 RID: 160
public class Skill
{
	// Token: 0x060008D4 RID: 2260 RVA: 0x00081918 File Offset: 0x0007FB18
	public string strCurExp()
	{
		if (this.curExp / 10 >= 100)
		{
			return "MAX";
		}
		if (this.curExp % 10 == 0)
		{
			return ((int)(this.curExp / 10)).ToString() + "%";
		}
		int num = (int)(this.curExp % 10);
		return ((int)(this.curExp / 10)).ToString() + "." + (num % 10).ToString() + "%";
	}

	// Token: 0x060008D5 RID: 2261 RVA: 0x00081998 File Offset: 0x0007FB98
	public string strTimeReplay()
	{
		if (this.coolDown % 1000 == 0)
		{
			return (this.coolDown / 1000).ToString() + string.Empty;
		}
		int num = this.coolDown % 1000;
		return (this.coolDown / 1000).ToString() + "." + ((num % 100 != 0) ? (num / 10) : (num / 100)).ToString();
	}

	// Token: 0x060008D6 RID: 2262 RVA: 0x00081A18 File Offset: 0x0007FC18
	public void paint(int x, int y, mGraphics g)
	{
		SmallImage.drawSmallImage(g, this.template.iconId, x, y, 0, StaticObj.VCENTER_HCENTER);
		long num = mSystem.currentTimeMillis() - this.lastTimeUseThisSkill;
		if (num < (long)this.coolDown)
		{
			g.setColor(2721889, 0.7f);
			if (this.paintCanNotUseSkill && GameCanvas.gameTick % 6 > 2)
			{
				g.setColor(876862);
			}
			int num2 = (int)(num * 20L / (long)this.coolDown);
			g.fillRect(x - 10, y - 10 + num2, 20, 20 - num2);
			return;
		}
		this.paintCanNotUseSkill = false;
	}

	// Token: 0x04000F70 RID: 3952
	public const sbyte ATT_STAND = 0;

	// Token: 0x04000F71 RID: 3953
	public const sbyte ATT_FLY = 1;

	// Token: 0x04000F72 RID: 3954
	public const sbyte SKILL_AUTO_USE = 0;

	// Token: 0x04000F73 RID: 3955
	public const sbyte SKILL_CLICK_USE_ATTACK = 1;

	// Token: 0x04000F74 RID: 3956
	public const sbyte SKILL_CLICK_USE_BUFF = 2;

	// Token: 0x04000F75 RID: 3957
	public const sbyte SKILL_CLICK_NPC = 3;

	// Token: 0x04000F76 RID: 3958
	public const sbyte SKILL_CLICK_LIVE = 4;

	// Token: 0x04000F77 RID: 3959
	public SkillTemplate template;

	// Token: 0x04000F78 RID: 3960
	public short skillId;

	// Token: 0x04000F79 RID: 3961
	public int point;

	// Token: 0x04000F7A RID: 3962
	public long powRequire;

	// Token: 0x04000F7B RID: 3963
	public int coolDown;

	// Token: 0x04000F7C RID: 3964
	public long lastTimeUseThisSkill;

	// Token: 0x04000F7D RID: 3965
	public int dx;

	// Token: 0x04000F7E RID: 3966
	public int dy;

	// Token: 0x04000F7F RID: 3967
	public int maxFight;

	// Token: 0x04000F80 RID: 3968
	public int manaUse;

	// Token: 0x04000F81 RID: 3969
	public SkillOption[] options;

	// Token: 0x04000F82 RID: 3970
	public bool paintCanNotUseSkill;

	// Token: 0x04000F83 RID: 3971
	public short damage;

	// Token: 0x04000F84 RID: 3972
	public string moreInfo;

	// Token: 0x04000F85 RID: 3973
	public short price;

	// Token: 0x04000F86 RID: 3974
	public short curExp;
}
