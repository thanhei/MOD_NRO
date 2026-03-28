using System;

// Token: 0x02000082 RID: 130
public class SkillOption
{
	// Token: 0x06000455 RID: 1109 RVA: 0x0002982C File Offset: 0x00027A2C
	public string getOptionString()
	{
		if (this.optionString == null)
		{
			this.optionString = NinjaUtil.replace(this.optionTemplate.name, "#", string.Empty + this.param);
		}
		return this.optionString;
	}

	// Token: 0x040007C5 RID: 1989
	public int param;

	// Token: 0x040007C6 RID: 1990
	public SkillOptionTemplate optionTemplate;

	// Token: 0x040007C7 RID: 1991
	public string optionString;
}
