using System;
using Newtonsoft.Json;

namespace Mod.AccountManager
{
	// Token: 0x0200018D RID: 397
	internal class CharacterInfo
	{
		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060011B7 RID: 4535 RVA: 0x000BE9B9 File Offset: 0x000BCBB9
		// (set) Token: 0x060011B8 RID: 4536 RVA: 0x000BE9C1 File Offset: 0x000BCBC1
		[JsonProperty("name")]
		internal string Name { get; set; } = "unknown";

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060011B9 RID: 4537 RVA: 0x000BE9CA File Offset: 0x000BCBCA
		// (set) Token: 0x060011BA RID: 4538 RVA: 0x000BE9D2 File Offset: 0x000BCBD2
		[JsonProperty("max_hp")]
		internal long MaxHP { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060011BB RID: 4539 RVA: 0x000BE9DB File Offset: 0x000BCBDB
		// (set) Token: 0x060011BC RID: 4540 RVA: 0x000BE9E3 File Offset: 0x000BCBE3
		[JsonProperty("max_mp")]
		internal long MaxMP { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060011BD RID: 4541 RVA: 0x000BE9EC File Offset: 0x000BCBEC
		// (set) Token: 0x060011BE RID: 4542 RVA: 0x000BE9F4 File Offset: 0x000BCBF4
		[JsonProperty("exp")]
		internal long EXP { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060011BF RID: 4543 RVA: 0x000BE9FD File Offset: 0x000BCBFD
		// (set) Token: 0x060011C0 RID: 4544 RVA: 0x000BEA05 File Offset: 0x000BCC05
		[JsonProperty("gender")]
		internal sbyte Gender { get; set; } = -1;

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060011C1 RID: 4545 RVA: 0x000BEA0E File Offset: 0x000BCC0E
		// (set) Token: 0x060011C2 RID: 4546 RVA: 0x000BEA16 File Offset: 0x000BCC16
		[JsonProperty("icon")]
		internal int Icon { get; set; } = -1;

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060011C3 RID: 4547 RVA: 0x000BEA1F File Offset: 0x000BCC1F
		// (set) Token: 0x060011C4 RID: 4548 RVA: 0x000BEA27 File Offset: 0x000BCC27
		[JsonProperty("char_id")]
		internal int CharID { get; set; }

		// Token: 0x060011C5 RID: 4549 RVA: 0x000BEA30 File Offset: 0x000BCC30
		internal string GetGender()
		{
			if (this.Gender < 3 && this.Gender >= 0)
			{
				return mResources.MENUGENDER[(int)this.Gender];
			}
			return "Unknown";
		}
	}
}
