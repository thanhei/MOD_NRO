using System;
using System.Collections.Generic;
using System.Linq;

namespace Mod.R
{
	// Token: 0x0200011D RID: 285
	internal class LocalizedString
	{
		// Token: 0x06000E43 RID: 3651 RVA: 0x000A7E2C File Offset: 0x000A602C
		private LocalizedString(string[] strings)
		{
			this.strings = strings;
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000A7E3C File Offset: 0x000A603C
		internal string Replace(string original, string newValue)
		{
			foreach (string text in this.strings)
			{
				original = original.Replace(text, newValue);
			}
			return original;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000A7E70 File Offset: 0x000A6070
		internal bool Contains(string str)
		{
			return this.strings.Any<string>((string s) => !string.IsNullOrEmpty(s) && s.Contains(str));
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x000A7EA4 File Offset: 0x000A60A4
		internal bool ContainsReversed(string str)
		{
			return this.strings.Any<string>((string s) => !string.IsNullOrEmpty(s) && str.Contains(s));
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000A7ED8 File Offset: 0x000A60D8
		internal bool StartsWith(string str)
		{
			return this.strings.Any<string>((string s) => !string.IsNullOrEmpty(s) && s.StartsWith(str));
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x000A7F0C File Offset: 0x000A610C
		internal bool StartsWithReversed(string str)
		{
			return this.strings.Any<string>((string s) => !string.IsNullOrEmpty(s) && str.StartsWith(s));
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000A7F40 File Offset: 0x000A6140
		internal bool EndsWith(string str)
		{
			return this.strings.Any<string>((string s) => !string.IsNullOrEmpty(s) && s.EndsWith(str));
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x000A7F74 File Offset: 0x000A6174
		internal bool EndsWithReversed(string str)
		{
			return this.strings.Any<string>((string s) => !string.IsNullOrEmpty(s) && str.EndsWith(s));
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000A7FA8 File Offset: 0x000A61A8
		internal bool IsEqual(string str)
		{
			return this.strings.Any<string>((string s) => !string.IsNullOrEmpty(s) && s == str);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x000A7FD9 File Offset: 0x000A61D9
		public static implicit operator LocalizedString(string[] str)
		{
			return new LocalizedString(str);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000A7FE1 File Offset: 0x000A61E1
		public static implicit operator string(LocalizedString localized)
		{
			return localized.strings[0];
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000A7FEB File Offset: 0x000A61EB
		public static bool operator ==(LocalizedString localized, string str)
		{
			return localized.IsEqual(str);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x000A7FF4 File Offset: 0x000A61F4
		public static bool operator !=(LocalizedString localized, string str)
		{
			return !localized.IsEqual(str);
		}

		// Token: 0x06000E50 RID: 3664 RVA: 0x000A8000 File Offset: 0x000A6200
		public static bool operator ==(string str, LocalizedString localized)
		{
			return localized.IsEqual(str);
		}

		// Token: 0x06000E51 RID: 3665 RVA: 0x000A8009 File Offset: 0x000A6209
		public static bool operator !=(string str, LocalizedString localized)
		{
			return !localized.IsEqual(str);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000A8018 File Offset: 0x000A6218
		public override bool Equals(object obj)
		{
			LocalizedString localizedString = obj as LocalizedString;
			return localizedString != null && EqualityComparer<string[]>.Default.Equals(this.strings, localizedString.strings);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x000A8047 File Offset: 0x000A6247
		public override int GetHashCode()
		{
			return HashCode.Combine<string[]>(this.strings);
		}

		// Token: 0x04001570 RID: 5488
		internal static LocalizedString[] xmapCantGoHereKeywords = new LocalizedString[]
		{
			new string[] { "Bạn chưa thể đến khu vực này", "", "" },
			new string[] { "Bang hội phải có từ 5 thành viên mới được tham gia", "", "" },
			new string[] { "Chỉ tiếp các bang hội, miễn tiếp khách vãng lai", "", "" },
			new string[] { "Gia nhập bang hội trên 2 ngày mới được tham gia", "", "" }
		};

		// Token: 0x04001571 RID: 5489
		internal static LocalizedString free1hCharm = new string[] { "thưởng bùa 1h ngẫu nhiên", "random 1 hour charm reward", "hadiah 1 jam charm" };

		// Token: 0x04001572 RID: 5490
		internal static LocalizedString challengeKarin = new string[] { "thách đấu thần mèo", "challenge with karin", "tantang karin" };

		// Token: 0x04001573 RID: 5491
		internal static LocalizedString acceptChallenge = new string[] { "đồng ý giao đấu", "accept fight", "accept fight" };

		// Token: 0x04001574 RID: 5492
		internal static LocalizedString mercenaryTao = new string[] { "Tàu Pảy Pảy", "Taopaipai", "Taopaipai" };

		// Token: 0x04001575 RID: 5493
		internal static LocalizedString saoMayLuoiThe = new string[] { "sao sư phụ không đánh đi", "", "" };

		// Token: 0x04001576 RID: 5494
		internal static LocalizedString errorOccurred = new string[] { "Có lỗi xảy ra vui lòng thử lại sau.", "", "" };

		// Token: 0x04001577 RID: 5495
		internal static LocalizedString goHome = new string[] { "Về nhà", "Go home", "Pulang" };

		// Token: 0x04001578 RID: 5496
		internal static LocalizedString spaceshipStation = new string[] { "Trạm tàu vũ trụ", "Spaceship station", "Spaceship station" };

		// Token: 0x04001579 RID: 5497
		internal static LocalizedString backTo = new string[] { "Về chỗ cũ", "Back to", "Kembali ke" };

		// Token: 0x0400157A RID: 5498
		internal static LocalizedString stoneForest = new string[] { "Rừng đá", "Stone forest", "Stone forest" };

		// Token: 0x0400157B RID: 5499
		internal static LocalizedString arbitration = new string[] { "Trọng tài", "Arbitration", "Arbitration" };

		// Token: 0x0400157C RID: 5500
		internal static LocalizedString cantChangeZoneInThisMap = new string[] { "Không thể đổi khu vực trong map này", "Can not change zone in this map", "Tidak bisa mengganti zone di map ini" };

		// Token: 0x0400157D RID: 5501
		internal static LocalizedString senzuTreeUpgrading = new string[] { "Đang nâng cấp", "Upgrading", "Sedang mengupgrade" };

		// Token: 0x0400157E RID: 5502
		internal static LocalizedString senzuBeanHarvested = new string[] { "Bạn vừa thu hoạch được", "You just harvested", "Kamu baru saja memanen" };

		// Token: 0x0400157F RID: 5503
		internal static LocalizedString free1hCharmReceived = new string[] { "Bạn vừa nhận thưởng bùa", "You receive award", "Kamu menerima hadiah" };

		// Token: 0x04001580 RID: 5504
		internal static LocalizedString getGift = new string[] { "Nhận quà", "Get Gift", "Menerima Gift" };

		// Token: 0x04001581 RID: 5505
		internal static LocalizedString rejectGift = new string[] { "Từ chối", "Reject", "Tolak" };

		// Token: 0x04001582 RID: 5506
		internal static LocalizedString talk = new string[] { "Nói chuyện", "Talk", "Bicara" };

		// Token: 0x04001583 RID: 5507
		internal static LocalizedString mission = new string[] { "Nhiệm vụ", "Quest", "Misi" };

		// Token: 0x04001584 RID: 5508
		private string[] strings;
	}
}
