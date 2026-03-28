using System;

// Token: 0x02000085 RID: 133
public class SkillTemplate
{
	// Token: 0x06000459 RID: 1113 RVA: 0x0000656F File Offset: 0x0000476F
	public bool isBuffToPlayer()
	{
		return this.type == 2;
	}

	// Token: 0x0600045A RID: 1114 RVA: 0x00006580 File Offset: 0x00004780
	public bool isUseAlone()
	{
		return this.type == 3;
	}

	// Token: 0x0600045B RID: 1115 RVA: 0x00006591 File Offset: 0x00004791
	public bool isAttackSkill()
	{
		return this.type == 1;
	}

	// Token: 0x0600045C RID: 1116 RVA: 0x000065A2 File Offset: 0x000047A2
	public bool isSkillSpec()
	{
		return this.type == 4;
	}

	// Token: 0x040007CF RID: 1999
	public sbyte id;

	// Token: 0x040007D0 RID: 2000
	public int classId;

	// Token: 0x040007D1 RID: 2001
	public string name;

	// Token: 0x040007D2 RID: 2002
	public int maxPoint;

	// Token: 0x040007D3 RID: 2003
	public int manaUseType;

	// Token: 0x040007D4 RID: 2004
	public int type;

	// Token: 0x040007D5 RID: 2005
	public int iconId;

	// Token: 0x040007D6 RID: 2006
	public string[] description;

	// Token: 0x040007D7 RID: 2007
	public Skill[] skills;

	// Token: 0x040007D8 RID: 2008
	public string damInfo;
}
