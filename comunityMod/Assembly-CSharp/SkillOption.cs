using System;

// Token: 0x020000A2 RID: 162
public class SkillOption
{
	// Token: 0x060008D9 RID: 2265 RVA: 0x00081AAF File Offset: 0x0007FCAF
	public string getOptionString()
	{
		if (this.optionString == null)
		{
			this.optionString = NinjaUtil.replace(this.optionTemplate.name, "#", string.Empty + this.param.ToString());
		}
		return this.optionString;
	}

	// Token: 0x04000F94 RID: 3988
	public int param;

	// Token: 0x04000F95 RID: 3989
	public SkillOptionTemplate optionTemplate;

	// Token: 0x04000F96 RID: 3990
	public string optionString;
}
