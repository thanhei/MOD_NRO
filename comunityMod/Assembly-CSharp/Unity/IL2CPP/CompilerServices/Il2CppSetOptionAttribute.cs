using System;

namespace Unity.IL2CPP.CompilerServices
{
	// Token: 0x020000D6 RID: 214
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Delegate, Inherited = false, AllowMultiple = true)]
	public class Il2CppSetOptionAttribute : Attribute
	{
		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000B3C RID: 2876 RVA: 0x00095B5E File Offset: 0x00093D5E
		// (set) Token: 0x06000B3D RID: 2877 RVA: 0x00095B66 File Offset: 0x00093D66
		public Option Option { get; private set; }

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x06000B3E RID: 2878 RVA: 0x00095B6F File Offset: 0x00093D6F
		// (set) Token: 0x06000B3F RID: 2879 RVA: 0x00095B77 File Offset: 0x00093D77
		public object Value { get; private set; }

		// Token: 0x06000B40 RID: 2880 RVA: 0x00095B80 File Offset: 0x00093D80
		public Il2CppSetOptionAttribute(Option option, object value)
		{
			this.Option = option;
			this.Value = value;
		}
	}
}
