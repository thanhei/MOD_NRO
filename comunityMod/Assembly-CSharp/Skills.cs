using System;

// Token: 0x020000A6 RID: 166
public class Skills
{
	// Token: 0x060008E2 RID: 2274 RVA: 0x00081B27 File Offset: 0x0007FD27
	public static void add(Skill skill)
	{
		Skills.skills.put(skill.skillId, skill);
	}

	// Token: 0x060008E3 RID: 2275 RVA: 0x00081B3F File Offset: 0x0007FD3F
	public static Skill get(short skillId)
	{
		return (Skill)Skills.skills.get(skillId);
	}

	// Token: 0x04000FA8 RID: 4008
	public static MyHashTable skills = new MyHashTable();
}
