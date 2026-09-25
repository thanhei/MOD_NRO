using System;
using System.Collections.Generic;

namespace Mod
{
	// Token: 0x020000DB RID: 219
	internal class ClanUtils
	{
		// Token: 0x06000B5C RID: 2908 RVA: 0x00097394 File Offset: 0x00095594
		internal static bool CanAskForPeans()
		{
			return (DateTime.Now - ClanUtils.lastRequestedPean).TotalMinutes >= 5.0;
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x000973C8 File Offset: 0x000955C8
		internal static bool CanDonatePeans()
		{
			for (int i = 0; i < ClanMessage.vMessage.size(); i++)
			{
				ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
				if (clanMessage.type == 1 && clanMessage.recieve < clanMessage.maxCap && clanMessage.playerId != global::Char.myCharz().charID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x00097427 File Offset: 0x00095627
		internal static void RequestPeans()
		{
			Service.gI().clanMessage(1, null, -1);
			ClanUtils.lastRequestedPean = DateTime.Now;
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x00097440 File Offset: 0x00095640
		internal static void DonatePeans()
		{
			foreach (ClanMessage clanMessage in ClanUtils.GetDonationMsgs())
			{
				Service.gI().clanDonate(clanMessage.id);
			}
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x0009749C File Offset: 0x0009569C
		private static List<ClanMessage> GetDonationMsgs()
		{
			List<ClanMessage> list = new List<ClanMessage>();
			for (int i = 0; i < ClanMessage.vMessage.size(); i++)
			{
				ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(i);
				if (clanMessage.type == 1 && clanMessage.recieve < clanMessage.maxCap && clanMessage.playerId != global::Char.myCharz().charID)
				{
					list.Add(clanMessage);
				}
			}
			return list;
		}

		// Token: 0x04001489 RID: 5257
		private static DateTime lastRequestedPean = DateTime.MinValue;

		// Token: 0x0400148A RID: 5258
		private const sbyte PeanRequestInterval = 5;
	}
}
