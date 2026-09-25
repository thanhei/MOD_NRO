using System;

namespace Mod.ModMenu
{
	// Token: 0x02000133 RID: 307
	internal class ModMenuItemValuesConfig : ModMenuItemConfig
	{
		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x06000EFA RID: 3834 RVA: 0x000AD699 File Offset: 0x000AB899
		// (set) Token: 0x06000EFB RID: 3835 RVA: 0x000AD6A1 File Offset: 0x000AB8A1
		internal string[] Values { get; set; }

		// Token: 0x170000A2 RID: 162
		// (get) Token: 0x06000EFC RID: 3836 RVA: 0x000AD6AA File Offset: 0x000AB8AA
		// (set) Token: 0x06000EFD RID: 3837 RVA: 0x000AD6B2 File Offset: 0x000AB8B2
		internal string RMSName { get; set; }

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x000AD6BB File Offset: 0x000AB8BB
		// (set) Token: 0x06000EFF RID: 3839 RVA: 0x000AD6C3 File Offset: 0x000AB8C3
		internal Func<double> GetValueFunc { get; set; }

		// Token: 0x170000A4 RID: 164
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x000AD6CC File Offset: 0x000AB8CC
		// (set) Token: 0x06000F01 RID: 3841 RVA: 0x000AD6D4 File Offset: 0x000AB8D4
		internal Action<double> SetValueAction { get; set; }

		// Token: 0x170000A5 RID: 165
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x000AD6DD File Offset: 0x000AB8DD
		// (set) Token: 0x06000F03 RID: 3843 RVA: 0x000AD6E5 File Offset: 0x000AB8E5
		internal string TextFieldTitle { get; set; }

		// Token: 0x170000A6 RID: 166
		// (get) Token: 0x06000F04 RID: 3844 RVA: 0x000AD6EE File Offset: 0x000AB8EE
		// (set) Token: 0x06000F05 RID: 3845 RVA: 0x000AD6F6 File Offset: 0x000AB8F6
		internal string TextFieldHint { get; set; }

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x000AD6FF File Offset: 0x000AB8FF
		// (set) Token: 0x06000F07 RID: 3847 RVA: 0x000AD707 File Offset: 0x000AB907
		internal double MinValue { get; set; } = -2147483648.0;

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x06000F08 RID: 3848 RVA: 0x000AD710 File Offset: 0x000AB910
		// (set) Token: 0x06000F09 RID: 3849 RVA: 0x000AD718 File Offset: 0x000AB918
		internal double MaxValue { get; set; } = 2147483647.0;

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x06000F0A RID: 3850 RVA: 0x000AD721 File Offset: 0x000AB921
		// (set) Token: 0x06000F0B RID: 3851 RVA: 0x000AD729 File Offset: 0x000AB929
		internal bool IsFloatingPoint { get; set; }
	}
}
