using System;

// Token: 0x02000048 RID: 72
public abstract class IPaint
{
	// Token: 0x06000413 RID: 1043
	public abstract void paintDefaultBg(mGraphics g);

	// Token: 0x06000414 RID: 1044
	public abstract void paintfillDefaultBg(mGraphics g);

	// Token: 0x06000415 RID: 1045
	public abstract void repaintCircleBg();

	// Token: 0x06000416 RID: 1046
	public abstract void paintSolidBg(mGraphics g);

	// Token: 0x06000417 RID: 1047
	public abstract void paintDefaultPopup(mGraphics g, int x, int y, int w, int h);

	// Token: 0x06000418 RID: 1048
	public abstract void paintWhitePopup(mGraphics g, int y, int x, int width, int height);

	// Token: 0x06000419 RID: 1049
	public abstract void paintDefaultPopupH(mGraphics g, int h);

	// Token: 0x0600041A RID: 1050
	public abstract void paintCmdBar(mGraphics g, Command left, Command center, Command right);

	// Token: 0x0600041B RID: 1051
	public abstract void paintSelect(mGraphics g, int x, int y, int w, int h);

	// Token: 0x0600041C RID: 1052
	public abstract void paintLogo(mGraphics g, int x, int y);

	// Token: 0x0600041D RID: 1053
	public abstract void paintHotline(mGraphics g, string num);

	// Token: 0x0600041E RID: 1054
	public abstract void paintInputTf(mGraphics g, bool iss, int x, int y, int w, int h, int xText, int yText, string text);

	// Token: 0x0600041F RID: 1055
	public abstract void paintTabSoft(mGraphics g);

	// Token: 0x06000420 RID: 1056
	public abstract void paintBackMenu(mGraphics g, int x, int y, int w, int h, bool iss);

	// Token: 0x06000421 RID: 1057
	public abstract void paintMsgBG(mGraphics g, int x, int y, int w, int h, string title, string subTitle, string check);

	// Token: 0x06000422 RID: 1058
	public abstract void paintDefaultScrLisst(mGraphics g, string title, string subTitle, string check);

	// Token: 0x06000423 RID: 1059
	public abstract void paintCheck(mGraphics g, int x, int y, int index);

	// Token: 0x06000424 RID: 1060
	public abstract void paintImgMsg(mGraphics g, int x, int y, int index);

	// Token: 0x06000425 RID: 1061
	public abstract void paintTitleBoard(mGraphics g, int roomID);

	// Token: 0x06000426 RID: 1062
	public abstract void paintCheckPass(mGraphics g, int x, int y, bool check, bool focus);

	// Token: 0x06000427 RID: 1063
	public abstract void paintInputDlg(mGraphics g, int x, int y, int w, int h, string[] str);

	// Token: 0x06000428 RID: 1064
	public abstract void paintIconMainMenu(mGraphics g, int x, int y, bool iss, bool issSe, int i, int wStr);

	// Token: 0x06000429 RID: 1065
	public abstract void paintLineRoom(mGraphics g, int x, int y, int xTo, int yTo);

	// Token: 0x0600042A RID: 1066
	public abstract void paintCellContaint(mGraphics g, int x, int y, int w, int h, bool iss);

	// Token: 0x0600042B RID: 1067
	public abstract void paintScroll(mGraphics g, int x, int y, int h);

	// Token: 0x0600042C RID: 1068
	public abstract int[] getColorMsg();

	// Token: 0x0600042D RID: 1069
	public abstract void paintLogo(mGraphics g);

	// Token: 0x0600042E RID: 1070
	public abstract void paintTextLogin(mGraphics g, bool issRes);

	// Token: 0x0600042F RID: 1071
	public abstract void paintSellectBoard(mGraphics g, int x, int y, int w, int h);

	// Token: 0x06000430 RID: 1072
	public abstract int issRegissterUsingWAP();

	// Token: 0x06000431 RID: 1073
	public abstract string getCard();

	// Token: 0x06000432 RID: 1074
	public abstract void paintSellectedShop(mGraphics g, int x, int y, int w, int h);

	// Token: 0x06000433 RID: 1075
	public abstract string getUrlUpdateGame();

	// Token: 0x06000434 RID: 1076 RVA: 0x0004AA50 File Offset: 0x00048C50
	public string getFAQLink()
	{
		return "http://wap.teamobi.com/faqs.php?provider=";
	}

	// Token: 0x06000435 RID: 1077
	public abstract void doSelect(int focus);
}
