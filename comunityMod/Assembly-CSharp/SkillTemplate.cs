using System;

// Token: 0x020000A5 RID: 165
public class SkillTemplate
{
	// Token: 0x060008DD RID: 2269 RVA: 0x00081AEF File Offset: 0x0007FCEF
	public bool isBuffToPlayer()
	{
		return this.type == 2;
	}

	// Token: 0x060008DE RID: 2270 RVA: 0x00081AFD File Offset: 0x0007FCFD
	public bool isUseAlone()
	{
		return this.type == 3;
	}

	// Token: 0x060008DF RID: 2271 RVA: 0x00081B0B File Offset: 0x0007FD0B
	public bool isAttackSkill()
	{
		return this.type == 1;
	}

	// Token: 0x060008E0 RID: 2272 RVA: 0x00081B19 File Offset: 0x0007FD19
	public bool isSkillSpec()
	{
		return this.type == 4;
	}

	// Token: 0x04000F9E RID: 3998
	public sbyte id;

	// Token: 0x04000F9F RID: 3999
	public int classId;

	// Token: 0x04000FA0 RID: 4000
	public string name;

	// Token: 0x04000FA1 RID: 4001
	public int maxPoint;

	// Token: 0x04000FA2 RID: 4002
	public int manaUseType;

	// Token: 0x04000FA3 RID: 4003
	public int type;

	// Token: 0x04000FA4 RID: 4004
	public int iconId;

	// Token: 0x04000FA5 RID: 4005
	public string[] description;

	// Token: 0x04000FA6 RID: 4006
	public Skill[] skills;

	// Token: 0x04000FA7 RID: 4007
	public string damInfo;
}
