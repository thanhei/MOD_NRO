using System;

namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x0200010F RID: 271
	public class Boss
	{
		// Token: 0x06000B6C RID: 2924 RVA: 0x00003FF8 File Offset: 0x000021F8
		public Boss()
		{
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x000A6E1C File Offset: 0x000A501C
		public Boss(string chatVip)
		{
			chatVip = chatVip.Replace("BOSS ", "").Replace(" vừa xuất hiện tại ", "|").Replace(" appear at ", "|");
			string[] array = chatVip.Split(new char[]
			{
				'|'
			});
			this.NameBoss = array[0].Trim();
			this.MapName = array[1].Trim();
			this.MapId = this.GetMapID(this.MapName);
			this.AppearTime = DateTime.Now;
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x000A6EAC File Offset: 0x000A50AC
		public int GetMapID(string mapName)
		{
			for (int i = 0; i < TileMap.mapNames.Length; i++)
			{
				if (TileMap.mapNames[i].Equals(mapName))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000A6EE0 File Offset: 0x000A50E0
		public void Paint(mGraphics g, int x, int y, int align)
		{
			TimeSpan timeSpan = DateTime.Now.Subtract(this.AppearTime);
			int num = (int)timeSpan.TotalSeconds;
			mFont mFont = mFont.tahoma_7_yellow;
			if (TileMap.mapID == this.MapId)
			{
				mFont = mFont.tahoma_7_red;
				for (int i = 0; i < GameScr.vCharInMap.size(); i++)
				{
					if (((global::Char)GameScr.vCharInMap.elementAt(i)).cName.Equals(this.NameBoss))
					{
						mFont = mFont.tahoma_7b_red;
						break;
					}
				}
			}
			mFont.drawString(g, string.Concat(new string[]
			{
				this.NameBoss,
				" - ",
				this.MapName,
				" - ",
				(num < 60) ? (num.ToString() + "s") : (timeSpan.Minutes.ToString() + "ph"),
				" trước"
			}), x, y, align);
		}

		// Token: 0x0400160E RID: 5646
		public string NameBoss;

		// Token: 0x0400160F RID: 5647
		public string MapName;

		// Token: 0x04001610 RID: 5648
		public int MapId;

		// Token: 0x04001611 RID: 5649
		public DateTime AppearTime;
	}
}
