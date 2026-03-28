using System;

// Token: 0x02000086 RID: 134
public class Skills
{
	// Token: 0x0600045E RID: 1118 RVA: 0x000065B3 File Offset: 0x000047B3
	public static void add(Skill skill)
	{
		Skills.skills.put(skill.skillId, skill);
	}

	// Token: 0x0600045F RID: 1119 RVA: 0x000065CB File Offset: 0x000047CB
	public static Skill get(short skillId)
	{
		return (Skill)Skills.skills.get(skillId);
	}

	// Token: 0x040007D9 RID: 2009
	public static MyHashTable skills = new MyHashTable();
}
