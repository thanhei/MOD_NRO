using System;

namespace Mod.Graphics
{
	// Token: 0x0200015B RID: 347
	internal static class HideGameUI
	{
		// Token: 0x06001062 RID: 4194 RVA: 0x000B4B6C File Offset: 0x000B2D6C
		internal static void SetState(bool newState)
		{
			HideGameUI.isEnabled = newState;
		}

		// Token: 0x06001063 RID: 4195 RVA: 0x000B4B74 File Offset: 0x000B2D74
		internal static bool ShouldDrawImage(Image image)
		{
			return !HideGameUI.isEnabled || (image != GameScr.imgChat && image != GameScr.imgChat2 && image != GameScr.imgChatPC && image != GameScr.imgChatsPC2 && image != GameScr.imgFire0 && image != GameScr.imgFire1 && image != GameScr.imgHP1 && image != GameScr.imgHP2 && image != GameScr.imgHP3 && image != GameScr.imgHP4 && image != GameScr.imgNR1 && image != GameScr.imgNR2 && image != GameScr.imgNR3 && image != GameScr.imgNR4 && image != GameScr.imgLbtn && image != GameScr.imgLbtn2 && image != GameScr.imgLbtnFocus && image != GameScr.imgLbtnFocus2 && image != GameScr.imgSkill && image != GameScr.imgSkill2 && image != GameScr.imgFocus && image != GameScr.imgFocus2 && image != GameScr.imgAnalog1 && image != GameScr.imgAnalog2 && image != GameScr.imgNut && image != GameScr.imgNutF && image != GameScr.imgPanel && image != GameScr.imgPanel2 && image != GameScr.imgHP && image != GameScr.imgHPLost && image != GameScr.imgMP && image != GameScr.imgMPLost && image != GameScr.imgArrow && image != GameScr.imgArrow2 && image != GameScr.arrow && image != GameScr.imgMenu && image != GameScr.imgKhung && image != GameScr.imgSP && image != Menu.imgMenu1 && image != Menu.imgMenu2 && image != Command.btn0left && image != Command.btn0mid && image != Command.btn0right && image != Command.btn1left && image != Command.btn1mid && image != Command.btn1right);
		}

		// Token: 0x040017C2 RID: 6082
		internal static bool isEnabled;
	}
}
