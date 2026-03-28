using System;

// Token: 0x02000080 RID: 128
public class Skill
{
	// Token: 0x06000450 RID: 1104 RVA: 0x00029678 File Offset: 0x00027878
	public string strCurExp()
	{
		if (this.curExp / 10 >= 100)
		{
			return "MAX";
		}
		if (this.curExp % 10 == 0)
		{
			return (int)(this.curExp / 10) + "%";
		}
		int num = (int)(this.curExp % 10);
		return string.Concat(new object[]
		{
			(int)(this.curExp / 10),
			".",
			num % 10,
			"%"
		});
	}

	// Token: 0x06000451 RID: 1105 RVA: 0x00029704 File Offset: 0x00027904
	public string strTimeReplay()
	{
		if (this.coolDown % 1000 == 0)
		{
			return this.coolDown / 1000 + string.Empty;
		}
		int num = this.coolDown % 1000;
		return this.coolDown / 1000 + "." + ((num % 100 != 0) ? (num / 10) : (num / 100));
	}

	// Token: 0x06000452 RID: 1106 RVA: 0x00029784 File Offset: 0x00027984
	public void paint(int x, int y, mGraphics g)
	{
		SmallImage.drawSmallImage(g, this.template.iconId, x, y, 0, StaticObj.VCENTER_HCENTER);
		long num = mSystem.currentTimeMillis();
		long num2 = num - this.lastTimeUseThisSkill;
		if (num2 < (long)this.coolDown)
		{
			g.setColor(2721889, 0.7f);
			if (this.paintCanNotUseSkill && GameCanvas.gameTick % 6 > 2)
			{
				g.setColor(876862);
			}
			int num3 = (int)(num2 * 20L / (long)this.coolDown);
			g.fillRect(x - 10, y - 10 + num3, 20, 20 - num3);
		}
		else
		{
			this.paintCanNotUseSkill = false;
		}
	}

	// Token: 0x040007A1 RID: 1953
	public const sbyte ATT_STAND = 0;

	// Token: 0x040007A2 RID: 1954
	public const sbyte ATT_FLY = 1;

	// Token: 0x040007A3 RID: 1955
	public const sbyte SKILL_AUTO_USE = 0;

	// Token: 0x040007A4 RID: 1956
	public const sbyte SKILL_CLICK_USE_ATTACK = 1;

	// Token: 0x040007A5 RID: 1957
	public const sbyte SKILL_CLICK_USE_BUFF = 2;

	// Token: 0x040007A6 RID: 1958
	public const sbyte SKILL_CLICK_NPC = 3;

	// Token: 0x040007A7 RID: 1959
	public const sbyte SKILL_CLICK_LIVE = 4;

	// Token: 0x040007A8 RID: 1960
	public SkillTemplate template;

	// Token: 0x040007A9 RID: 1961
	public short skillId;

	// Token: 0x040007AA RID: 1962
	public int point;

	// Token: 0x040007AB RID: 1963
	public long powRequire;

	// Token: 0x040007AC RID: 1964
	public int coolDown;

	// Token: 0x040007AD RID: 1965
	public long lastTimeUseThisSkill;

	// Token: 0x040007AE RID: 1966
	public int dx;

	// Token: 0x040007AF RID: 1967
	public int dy;

	// Token: 0x040007B0 RID: 1968
	public int maxFight;

	// Token: 0x040007B1 RID: 1969
	public int manaUse;

	// Token: 0x040007B2 RID: 1970
	public SkillOption[] options;

	// Token: 0x040007B3 RID: 1971
	public bool paintCanNotUseSkill;

	// Token: 0x040007B4 RID: 1972
	public short damage;

	// Token: 0x040007B5 RID: 1973
	public string moreInfo;

	// Token: 0x040007B6 RID: 1974
	public short price;

	// Token: 0x040007B7 RID: 1975
	public short curExp;
}
