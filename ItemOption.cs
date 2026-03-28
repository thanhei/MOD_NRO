using System;

// Token: 0x02000065 RID: 101
public class ItemOption
{
	// Token: 0x060003AC RID: 940 RVA: 0x00003FF8 File Offset: 0x000021F8
	public ItemOption()
	{
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00024028 File Offset: 0x00022228
	public ItemOption(int optionTemplateId, int param)
	{
		if (optionTemplateId == 22)
		{
			optionTemplateId = 6;
			param *= 1000;
		}
		if (optionTemplateId == 23)
		{
			optionTemplateId = 7;
			param *= 1000;
		}
		this.param = param;
		this.optionTemplate = GameScr.gI().iOptionTemplates[optionTemplateId];
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00005E35 File Offset: 0x00004035
	public string getOptionString()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "#", this.param + string.Empty);
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00005E61 File Offset: 0x00004061
	public string getOptionName()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "+#", string.Empty);
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00005E7D File Offset: 0x0000407D
	public string getOptiongColor()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "$", string.Empty);
	}

	// Token: 0x04000639 RID: 1593
	public int param;

	// Token: 0x0400063A RID: 1594
	public sbyte active;

	// Token: 0x0400063B RID: 1595
	public sbyte activeCard;

	// Token: 0x0400063C RID: 1596
	public ItemOptionTemplate optionTemplate;
}
