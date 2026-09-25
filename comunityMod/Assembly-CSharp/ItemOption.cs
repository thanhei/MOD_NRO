using System;

// Token: 0x02000058 RID: 88
public class ItemOption
{
	// Token: 0x060004C6 RID: 1222 RVA: 0x00003A00 File Offset: 0x00001C00
	public ItemOption()
	{
	}

	// Token: 0x060004C7 RID: 1223 RVA: 0x0004E6A8 File Offset: 0x0004C8A8
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

	// Token: 0x060004C8 RID: 1224 RVA: 0x0004E6F6 File Offset: 0x0004C8F6
	public string getOptionString()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "#", this.param.ToString() + string.Empty);
	}

	// Token: 0x060004C9 RID: 1225 RVA: 0x0004E722 File Offset: 0x0004C922
	public string getOptionName()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "+#", string.Empty);
	}

	// Token: 0x060004CA RID: 1226 RVA: 0x0004E73E File Offset: 0x0004C93E
	public string getOptiongColor()
	{
		return NinjaUtil.replace(this.optionTemplate.name, "$", string.Empty);
	}

	// Token: 0x04000A00 RID: 2560
	public int param;

	// Token: 0x04000A01 RID: 2561
	public sbyte active;

	// Token: 0x04000A02 RID: 2562
	public sbyte activeCard;

	// Token: 0x04000A03 RID: 2563
	public ItemOptionTemplate optionTemplate;
}
