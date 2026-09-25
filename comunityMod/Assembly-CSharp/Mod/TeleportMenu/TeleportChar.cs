using System;

namespace Mod.TeleportMenu
{
	// Token: 0x0200010A RID: 266
	internal class TeleportChar
	{
		// Token: 0x17000076 RID: 118
		// (get) Token: 0x06000DC2 RID: 3522 RVA: 0x000A4612 File Offset: 0x000A2812
		// (set) Token: 0x06000DC3 RID: 3523 RVA: 0x000A461A File Offset: 0x000A281A
		internal int ID { get; set; }

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x000A4623 File Offset: 0x000A2823
		// (set) Token: 0x06000DC5 RID: 3525 RVA: 0x000A462B File Offset: 0x000A282B
		internal string Name { get; set; }

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000DC6 RID: 3526 RVA: 0x000A4634 File Offset: 0x000A2834
		// (set) Token: 0x06000DC7 RID: 3527 RVA: 0x000A463C File Offset: 0x000A283C
		internal long LastTimeTeleportTo { get; set; }

		// Token: 0x06000DC8 RID: 3528 RVA: 0x000A4645 File Offset: 0x000A2845
		internal TeleportChar(int charId)
		{
			this.Name = "no name";
			this.ID = charId;
			this.LastTimeTeleportTo = mSystem.currentTimeMillis();
		}

		// Token: 0x06000DC9 RID: 3529 RVA: 0x000A466A File Offset: 0x000A286A
		internal TeleportChar(string name, int charId)
		{
			this.ID = charId;
			this.Name = name;
			this.LastTimeTeleportTo = mSystem.currentTimeMillis();
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x000A468B File Offset: 0x000A288B
		internal TeleportChar(global::Char ch)
		{
			this.Name = ch.GetNameWithoutClanTag(false);
			this.ID = ch.charID;
			this.LastTimeTeleportTo = mSystem.currentTimeMillis();
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x000A46B7 File Offset: 0x000A28B7
		internal TeleportChar(string name, int charID, long lastTimeTeleportTo)
		{
			this.Name = name;
			this.ID = charID;
			this.LastTimeTeleportTo = lastTimeTeleportTo;
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x000A46D4 File Offset: 0x000A28D4
		public override string ToString()
		{
			return string.Format("{0} [{1}]", this.Name, this.ID);
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x000A46F4 File Offset: 0x000A28F4
		public override bool Equals(object obj)
		{
			TeleportChar teleportChar = obj as TeleportChar;
			return teleportChar != null && teleportChar.Name == this.Name && teleportChar.ID == this.ID;
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x000A4730 File Offset: 0x000A2930
		public override int GetHashCode()
		{
			return HashCode.Combine<int, string>(this.ID, this.Name);
		}
	}
}
