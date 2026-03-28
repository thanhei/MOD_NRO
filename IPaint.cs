using System;

// Token: 0x0200005F RID: 95
public abstract class IPaint
{
	// Token: 0x06000352 RID: 850
	public abstract void paintDefaultBg(mGraphics g);

	// Token: 0x06000353 RID: 851
	public abstract void paintfillDefaultBg(mGraphics g);

	// Token: 0x06000354 RID: 852
	public abstract void repaintCircleBg();

	// Token: 0x06000355 RID: 853
	public abstract void paintSolidBg(mGraphics g);

	// Token: 0x06000356 RID: 854
	public abstract void paintDefaultPopup(mGraphics g, int x, int y, int w, int h);

	// Token: 0x06000357 RID: 855
	public abstract void paintWhitePopup(mGraphics g, int y, int x, int width, int height);

	// Token: 0x06000358 RID: 856
	public abstract void paintDefaultPopupH(mGraphics g, int h);

	// Token: 0x06000359 RID: 857
	public abstract void paintCmdBar(mGraphics g, Command left, Command center, Command right);

	// Token: 0x0600035A RID: 858
	public abstract void paintSelect(mGraphics g, int x, int y, int w, int h);

	// Token: 0x0600035B RID: 859
	public abstract void paintLogo(mGraphics g, int x, int y);

	// Token: 0x0600035C RID: 860
	public abstract void paintHotline(mGraphics g, string num);

	// Token: 0x0600035D RID: 861
	public abstract void paintInputTf(mGraphics g, bool iss, int x, int y, int w, int h, int xText, int yText, string text);

	// Token: 0x0600035E RID: 862
	public abstract void paintTabSoft(mGraphics g);

	// Token: 0x0600035F RID: 863
	public abstract void paintBackMenu(mGraphics g, int x, int y, int w, int h, bool iss);

	// Token: 0x06000360 RID: 864
	public abstract void paintMsgBG(mGraphics g, int x, int y, int w, int h, string title, string subTitle, string check);

	// Token: 0x06000361 RID: 865
	public abstract void paintDefaultScrLisst(mGraphics g, string title, string subTitle, string check);

	// Token: 0x06000362 RID: 866
	public abstract void paintCheck(mGraphics g, int x, int y, int index);

	// Token: 0x06000363 RID: 867
	public abstract void paintImgMsg(mGraphics g, int x, int y, int index);

	// Token: 0x06000364 RID: 868
	public abstract void paintTitleBoard(mGraphics g, int roomID);

	// Token: 0x06000365 RID: 869
	public abstract void paintCheckPass(mGraphics g, int x, int y, bool check, bool focus);

	// Token: 0x06000366 RID: 870
	public abstract void paintInputDlg(mGraphics g, int x, int y, int w, int h, string[] str);

	// Token: 0x06000367 RID: 871
	public abstract void paintIconMainMenu(mGraphics g, int x, int y, bool iss, bool issSe, int i, int wStr);

	// Token: 0x06000368 RID: 872
	public abstract void paintLineRoom(mGraphics g, int x, int y, int xTo, int yTo);

	// Token: 0x06000369 RID: 873
	public abstract void paintCellContaint(mGraphics g, int x, int y, int w, int h, bool iss);

	// Token: 0x0600036A RID: 874
	public abstract void paintScroll(mGraphics g, int x, int y, int h);

	// Token: 0x0600036B RID: 875
	public abstract int[] getColorMsg();

	// Token: 0x0600036C RID: 876
	public abstract void paintLogo(mGraphics g);

	// Token: 0x0600036D RID: 877
	public abstract void paintTextLogin(mGraphics g, bool issRes);

	// Token: 0x0600036E RID: 878
	public abstract void paintSellectBoard(mGraphics g, int x, int y, int w, int h);

	// Token: 0x0600036F RID: 879
	public abstract int issRegissterUsingWAP();

	// Token: 0x06000370 RID: 880
	public abstract string getCard();

	// Token: 0x06000371 RID: 881
	public abstract void paintSellectedShop(mGraphics g, int x, int y, int w, int h);

	// Token: 0x06000372 RID: 882
	public abstract string getUrlUpdateGame();

	// Token: 0x06000373 RID: 883 RVA: 0x00005B72 File Offset: 0x00003D72
	public string getFAQLink()
	{
		return "http://wap.teamobi.com/faqs.php?provider=";
	}

	// Token: 0x06000374 RID: 884
	public abstract void doSelect(int focus);
}
