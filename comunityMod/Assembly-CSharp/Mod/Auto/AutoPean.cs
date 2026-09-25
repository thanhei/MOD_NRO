using System;
using UnityEngine;

namespace Mod.Auto
{
	// Token: 0x0200017E RID: 382
	internal class AutoPean
	{
		// Token: 0x170000E1 RID: 225
		// (get) Token: 0x06001143 RID: 4419 RVA: 0x000BA75C File Offset: 0x000B895C
		// (set) Token: 0x06001144 RID: 4420 RVA: 0x000BA763 File Offset: 0x000B8963
		internal static bool isAutoRequest
		{
			get
			{
				return AutoPean._isAutoRequest;
			}
			set
			{
				AutoPean._isAutoRequest = value;
				if (value)
				{
					AutoPean.HandleSenzuBeans();
				}
			}
		}

		// Token: 0x170000E2 RID: 226
		// (get) Token: 0x06001145 RID: 4421 RVA: 0x000BA773 File Offset: 0x000B8973
		// (set) Token: 0x06001146 RID: 4422 RVA: 0x000BA77A File Offset: 0x000B897A
		internal static bool isAutoDonate
		{
			get
			{
				return AutoPean._isAutoDonate;
			}
			set
			{
				AutoPean._isAutoDonate = value;
				if (value)
				{
					AutoPean.HandleSenzuBeans();
				}
			}
		}

		// Token: 0x170000E3 RID: 227
		// (get) Token: 0x06001147 RID: 4423 RVA: 0x000BA78A File Offset: 0x000B898A
		// (set) Token: 0x06001148 RID: 4424 RVA: 0x000BA791 File Offset: 0x000B8991
		internal static bool isAutoHarvest
		{
			get
			{
				return AutoPean._isAutoHarvest;
			}
			set
			{
				AutoPean._isAutoHarvest = value;
				if (value)
				{
					AutoPean.HandleSenzuBeans();
				}
			}
		}

		// Token: 0x06001149 RID: 4425 RVA: 0x000BA7A1 File Offset: 0x000B89A1
		internal static void Update()
		{
			if ((float)GameCanvas.gameTick % (60f * Time.timeScale) != 0f)
			{
				return;
			}
			AutoPean.HandleSenzuBeans();
		}

		// Token: 0x0600114A RID: 4426 RVA: 0x000BA7C2 File Offset: 0x000B89C2
		private static void HandleSenzuBeans()
		{
			if (AutoPean.isAutoRequest && ClanUtils.CanAskForPeans())
			{
				ClanUtils.RequestPeans();
			}
			if (AutoPean.isAutoDonate && ClanUtils.CanDonatePeans())
			{
				ClanUtils.DonatePeans();
			}
			if (AutoPean.isAutoHarvest)
			{
				AutoPean.HarvestMagicTree();
			}
		}

		// Token: 0x0600114B RID: 4427 RVA: 0x000BA7F8 File Offset: 0x000B89F8
		internal static void HarvestMagicTree()
		{
			MagicTree magicTree = GameScr.gI().magicTree;
			if (!Utils.IsMyCharHome() || magicTree.isUpdate || magicTree.isPeasEffect || magicTree.currPeas == 0)
			{
				return;
			}
			Service.gI().openMenu(4);
			Service.gI().confirmMenu(4, 0);
		}

		// Token: 0x040018A4 RID: 6308
		internal static bool _isAutoRequest;

		// Token: 0x040018A5 RID: 6309
		internal static bool _isAutoDonate;

		// Token: 0x040018A6 RID: 6310
		internal static bool _isAutoHarvest;
	}
}
