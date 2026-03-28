using System;

// Token: 0x0200005D RID: 93
public class Hint
{
	// Token: 0x06000346 RID: 838 RVA: 0x00005B34 File Offset: 0x00003D34
	public static bool isOnTask(int tastId, int index)
	{
		return global::Char.myCharz().taskMaint != null && (int)global::Char.myCharz().taskMaint.taskId == tastId && global::Char.myCharz().taskMaint.index == index;
	}

	// Token: 0x06000347 RID: 839 RVA: 0x0002115C File Offset: 0x0001F35C
	public static bool isPaintz()
	{
		return (!Hint.isOnTask(0, 3) || GameCanvas.panel.currentTabIndex != 0 || (GameCanvas.panel.cmy >= 0 && GameCanvas.panel.cmy <= 30)) && (!Hint.isOnTask(2, 0) || !GameCanvas.panel.isShow || GameCanvas.panel.currentTabIndex == 0);
	}

	// Token: 0x06000348 RID: 840 RVA: 0x000211D4 File Offset: 0x0001F3D4
	public static void clickNpc()
	{
		if (GameCanvas.panel.isShow)
		{
			Hint.isPaint = false;
		}
		if (GameScr.getNpcTask() != null)
		{
			Hint.x = GameScr.getNpcTask().cx;
			Hint.y = GameScr.getNpcTask().cy;
			Hint.trans = 0;
			Hint.isCamera = true;
			Hint.type = ((!GameCanvas.isTouch) ? 0 : 1);
		}
	}

	// Token: 0x06000349 RID: 841 RVA: 0x00021240 File Offset: 0x0001F440
	public static void nextMap(int index)
	{
		if (GameCanvas.panel.isShow)
		{
			return;
		}
		if (PopUp.vPopups.size() - 1 < index)
		{
			return;
		}
		PopUp popUp = (PopUp)PopUp.vPopups.elementAt(index);
		Hint.x = popUp.cx + popUp.sayWidth / 2;
		Hint.y = popUp.cy + 30;
		if (popUp.isHide || !popUp.isPaint)
		{
			Hint.isPaint = false;
		}
		else
		{
			Hint.isPaint = true;
		}
		Hint.type = 0;
		Hint.isCamera = true;
		Hint.trans = 0;
		if (!GameCanvas.isTouch)
		{
			Hint.isPaint = false;
		}
	}

	// Token: 0x0600034A RID: 842 RVA: 0x000212EC File Offset: 0x0001F4EC
	public static void clickMob()
	{
		Hint.type = 1;
		if (GameCanvas.panel.isShow)
		{
			Hint.isPaint = false;
		}
		bool flag = false;
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob.isHintFocus)
			{
				flag = true;
				break;
			}
		}
		for (int j = 0; j < GameScr.vMob.size(); j++)
		{
			Mob mob2 = (Mob)GameScr.vMob.elementAt(j);
			if (mob2.isHintFocus)
			{
				Hint.x = mob2.x;
				Hint.y = mob2.y + 5;
				Hint.isCamera = true;
				if (mob2.status == 0)
				{
					mob2.isHintFocus = false;
				}
				break;
			}
			if (!flag)
			{
				if (mob2.status != 0)
				{
					mob2.isHintFocus = true;
					break;
				}
				mob2.isHintFocus = false;
			}
		}
	}

	// Token: 0x0600034B RID: 843 RVA: 0x000213F4 File Offset: 0x0001F5F4
	public static bool isHaveItem()
	{
		if (GameCanvas.panel.isShow)
		{
			Hint.isPaint = false;
		}
		for (int i = 0; i < GameScr.vItemMap.size(); i++)
		{
			ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
			if (itemMap.playerId == global::Char.myCharz().charID && itemMap.template.id == 73)
			{
				Hint.type = 1;
				Hint.x = itemMap.x;
				Hint.y = itemMap.y + 5;
				Hint.isCamera = true;
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600034C RID: 844 RVA: 0x00021490 File Offset: 0x0001F690
	public static void paintArrowPointToHint(mGraphics g)
	{
		try
		{
			if (Hint.isPaintArrow)
			{
				if (Hint.x <= GameScr.cmx || Hint.x >= GameScr.cmx + GameScr.gW || Hint.y <= GameScr.cmy || Hint.y >= GameScr.cmy + GameScr.gH)
				{
					if (GameCanvas.gameTick % 10 >= 5)
					{
						if (ChatPopup.currChatPopup == null)
						{
							if (ChatPopup.serverChatPopUp == null)
							{
								if (!GameCanvas.panel.isShow)
								{
									if (Hint.isCamera)
									{
										int num = Hint.x - global::Char.myCharz().cx;
										int num2 = Hint.y - global::Char.myCharz().cy;
										int num3 = 0;
										int num4 = 0;
										int arg = 0;
										if (num > 0 && num2 >= 0)
										{
											if (Res.abs(num) >= Res.abs(num2))
											{
												num3 = GameScr.gW - 10;
												num4 = GameScr.gH / 2 + 30;
												if (GameCanvas.isTouch)
												{
													num4 = GameScr.gH / 2 + 10;
												}
												arg = 0;
											}
											else
											{
												num3 = GameScr.gW / 2;
												num4 = GameScr.gH - 10;
												arg = 5;
											}
										}
										else if (num >= 0 && num2 < 0)
										{
											if (Res.abs(num) >= Res.abs(num2))
											{
												num3 = GameScr.gW - 10;
												num4 = GameScr.gH / 2 + 30;
												if (GameCanvas.isTouch)
												{
													num4 = GameScr.gH / 2 + 10;
												}
												arg = 0;
											}
											else
											{
												num3 = GameScr.gW / 2;
												num4 = 10;
												arg = 6;
											}
										}
										if (num < 0 && num2 >= 0)
										{
											if (Res.abs(num) >= Res.abs(num2))
											{
												num3 = 10;
												num4 = GameScr.gH / 2 + 30;
												if (GameCanvas.isTouch)
												{
													num4 = GameScr.gH / 2 + 10;
												}
												arg = 3;
											}
											else
											{
												num3 = GameScr.gW / 2;
												num4 = GameScr.gH - 10;
												arg = 5;
											}
										}
										else if (num <= 0 && num2 < 0)
										{
											if (Res.abs(num) >= Res.abs(num2))
											{
												num3 = 10;
												num4 = GameScr.gH / 2 + 30;
												if (GameCanvas.isTouch)
												{
													num4 = GameScr.gH / 2 + 10;
												}
												arg = 3;
											}
											else
											{
												num3 = GameScr.gW / 2;
												num4 = 10;
												arg = 6;
											}
										}
										GameScr.resetTranslate(g);
										g.drawRegion(GameScr.arrow, 0, 0, 13, 16, arg, num3, num4, StaticObj.VCENTER_HCENTER);
									}
								}
							}
						}
					}
				}
			}
		}
		catch (Exception ex)
		{
		}
	}

	// Token: 0x0600034D RID: 845 RVA: 0x00021740 File Offset: 0x0001F940
	public static void paint(mGraphics g)
	{
		if (ChatPopup.serverChatPopUp != null)
		{
			return;
		}
		if (global::Char.myCharz().isUsePlane || global::Char.myCharz().isTeleport)
		{
			return;
		}
		Hint.paintArrowPointToHint(g);
		if (GameCanvas.menu.tDelay != 0)
		{
			return;
		}
		if (!Hint.isPaint)
		{
			return;
		}
		if (ChatPopup.scr != null)
		{
			return;
		}
		if (global::Char.ischangingMap)
		{
			return;
		}
		if (GameCanvas.currentScreen != GameScr.gI())
		{
			return;
		}
		if (GameCanvas.panel.isShow && GameCanvas.panel.cmx != 0)
		{
			return;
		}
		if (Hint.isCamera)
		{
			g.translate(-GameScr.cmx, -GameScr.cmy);
		}
		if (Hint.trans == 0)
		{
			g.drawImage(Panel.imgBantay, Hint.x - 15, Hint.y, 0);
		}
		if (Hint.trans == 1)
		{
			g.drawRegion(Panel.imgBantay, 0, 0, 14, 16, 2, Hint.x + 15, Hint.y, StaticObj.TOP_RIGHT);
		}
		if (Hint.paintFlare)
		{
			g.drawImage(ItemMap.imageFlare, Hint.x, Hint.y, 3);
		}
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0002186C File Offset: 0x0001FA6C
	public static void hint()
	{
		if (global::Char.myCharz().taskMaint != null && GameCanvas.currentScreen == GameScr.instance)
		{
			int taskId = (int)global::Char.myCharz().taskMaint.taskId;
			int index = global::Char.myCharz().taskMaint.index;
			Hint.isCamera = false;
			Hint.trans = 0;
			Hint.type = 0;
			Hint.isPaint = true;
			Hint.isPaintArrow = true;
			if (GameCanvas.menu.showMenu && taskId > 0)
			{
				Hint.isPaint = false;
			}
			switch (taskId)
			{
			case 0:
				if (ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					if (index == 0 && TileMap.vGo.size() != 0)
					{
						Hint.x = (int)(((Waypoint)TileMap.vGo.elementAt(0)).minX - 100);
						Hint.y = (int)(((Waypoint)TileMap.vGo.elementAt(0)).minY + 40);
						Hint.isCamera = true;
					}
					if (index == 1)
					{
						Hint.nextMap(0);
					}
					if (index == 2)
					{
						Hint.clickNpc();
					}
					if (index == 3)
					{
						if (!GameCanvas.panel.isShow)
						{
							Hint.clickNpc();
						}
						else if (GameCanvas.panel.currentTabIndex == 0)
						{
							if (GameCanvas.panel.cp == null)
							{
								Hint.x = GameCanvas.panel.xScroll + GameCanvas.panel.wScroll / 2;
								Hint.y = GameCanvas.panel.yScroll + 20;
							}
							else if (GameCanvas.menu.tDelay != 0)
							{
								Hint.x = GameCanvas.panel.xScroll + 25;
								Hint.y = GameCanvas.panel.yScroll + 60;
							}
						}
						else if (GameCanvas.panel.currentTabIndex == 1)
						{
							Hint.x = GameCanvas.panel.startTabPos + 10;
							Hint.y = 65;
						}
					}
					if (index == 4)
					{
						if (GameCanvas.panel.isShow)
						{
							Hint.x = GameCanvas.panel.cmdClose.x + 5;
							Hint.y = GameCanvas.panel.cmdClose.y + 5;
						}
						else if (GameCanvas.menu.showMenu)
						{
							Hint.x = GameCanvas.w / 2;
							Hint.y = GameCanvas.h - 20;
						}
						else
						{
							Hint.clickNpc();
						}
					}
					if (index == 5)
					{
						Hint.clickNpc();
					}
				}
				break;
			case 1:
				if (ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					if (index == 0)
					{
						if (TileMap.isOfflineMap())
						{
							Hint.nextMap(0);
						}
						else
						{
							Hint.clickMob();
						}
					}
					if (index == 1)
					{
						if (!TileMap.isOfflineMap())
						{
							Hint.nextMap(1);
						}
						else
						{
							Hint.clickNpc();
						}
					}
				}
				break;
			case 2:
				if (ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else
				{
					if (index == 0)
					{
						if (!TileMap.isOfflineMap())
						{
							Hint.isViewMap = true;
						}
						if (!GameCanvas.panel.isShow)
						{
							if (!Hint.isViewMap)
							{
								Hint.x = GameScr.gI().cmdMenu.x;
								Hint.y = GameScr.gI().cmdMenu.y + 13;
								Hint.trans = 1;
							}
							else
							{
								if (GameScr.getTaskMapId() == TileMap.mapID)
								{
									if (!Hint.isHaveItem())
									{
										Hint.clickMob();
									}
								}
								else
								{
									Hint.nextMap(0);
								}
								if (Hint.isViewMap)
								{
									Hint.isCloseMap = true;
								}
							}
						}
						else if (!Hint.isViewMap)
						{
							if (GameCanvas.panel.currentTabIndex == 0)
							{
								int num = (GameCanvas.h <= 300) ? 10 : 15;
								Hint.x = GameCanvas.panel.xScroll + GameCanvas.panel.wScroll / 2;
								Hint.y = GameCanvas.panel.yScroll + GameCanvas.panel.hScroll - num;
							}
							else
							{
								Hint.x = GameCanvas.panel.startTabPos + 10;
								Hint.y = 65;
							}
						}
						else if (!Hint.isCloseMap)
						{
							Hint.x = GameCanvas.panel.cmdClose.x + 5;
							Hint.y = GameCanvas.panel.cmdClose.y + 5;
						}
						else
						{
							Hint.isPaint = false;
						}
						if (global::Char.myCharz().cMP <= 0L)
						{
							Hint.x = GameScr.xHP + 5;
							Hint.y = GameScr.yHP + 13;
							Hint.isCamera = false;
						}
					}
					if (index == 1)
					{
						Hint.isPaint = false;
						Hint.isPaintArrow = false;
					}
				}
				break;
			case 3:
				if (ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14)
				{
					Hint.x = GameCanvas.w / 2;
					Hint.y = GameCanvas.h - 15;
				}
				else if (index == 0)
				{
					if (!GameCanvas.panel.isShow)
					{
						if (!Hint.isViewPotential)
						{
							Hint.x = GameScr.gI().cmdMenu.x;
							Hint.y = GameScr.gI().cmdMenu.y + 13;
							Hint.trans = 1;
						}
						else
						{
							if (GameScr.getTaskMapId() == TileMap.mapID)
							{
								if (!Hint.isHaveItem())
								{
									Hint.clickMob();
								}
							}
							else
							{
								Hint.nextMap(0);
							}
							if (Hint.isViewMap)
							{
								Hint.isCloseMap = true;
							}
						}
					}
					else if (!Hint.isViewPotential)
					{
						int num2 = (GameCanvas.h <= 300) ? 10 : 15;
						Hint.x = GameCanvas.panel.xScroll + 10 + 108 - 18;
						Hint.y = 65;
					}
					else if (!Hint.isCloseMap)
					{
						Hint.x = GameCanvas.panel.cmdClose.x + 5;
						Hint.y = GameCanvas.panel.cmdClose.y + 5;
					}
					else
					{
						Hint.isPaint = false;
					}
					if (global::Char.myCharz().cMP <= 0L)
					{
						Hint.x = GameScr.xHP + 5;
						Hint.y = GameScr.yHP + 13;
						Hint.isCamera = false;
					}
				}
				else
				{
					Hint.isPaint = false;
					Hint.isPaintArrow = false;
				}
				break;
			default:
				if (global::Char.myCharz().taskMaint.taskId == 9 && global::Char.myCharz().taskMaint.index == 2)
				{
					for (int i = 0; i < PopUp.vPopups.size(); i++)
					{
						PopUp popUp = (PopUp)PopUp.vPopups.elementAt(i);
						if (popUp.cy <= 24)
						{
							Hint.x = popUp.cx + popUp.sayWidth / 2;
							Hint.y = popUp.cy + 30;
							Hint.isCamera = true;
							Hint.isPaint = false;
							Hint.isPaintArrow = true;
							return;
						}
					}
				}
				Hint.isPaint = false;
				Hint.isPaintArrow = false;
				break;
			}
		}
		else
		{
			Hint.isPaint = false;
			Hint.isPaintArrow = false;
		}
	}

	// Token: 0x0600034F RID: 847 RVA: 0x00021FE0 File Offset: 0x000201E0
	public static void update()
	{
		Hint.hint();
		int num = (Hint.trans != 0) ? -2 : 2;
		if (!Hint.activeClick)
		{
			Hint.paintFlare = false;
			Hint.t++;
			if (Hint.t == 50)
			{
				Hint.t = 0;
				Hint.activeClick = true;
			}
		}
		else
		{
			Hint.t++;
			if (Hint.type == 0)
			{
				if (Hint.t == 2)
				{
					Hint.x += 2 * num;
					Hint.y -= 4;
					Hint.paintFlare = true;
				}
				if (Hint.t == 4)
				{
					Hint.x -= 2 * num;
					Hint.y += 4;
					Hint.activeClick = false;
					Hint.paintFlare = false;
					Hint.t = 0;
				}
				if (Hint.t > 4)
				{
					Hint.activeClick = false;
				}
			}
			if (Hint.type == 1)
			{
				if (Hint.t == 2)
				{
					if (GameCanvas.isTouch)
					{
						GameScr.startFlyText(mResources.press_twice, Hint.x, Hint.y + 10, 0, 20, mFont.MISS_ME);
					}
					Hint.paintFlare = true;
					Hint.x += 2 * num;
					Hint.y -= 4;
				}
				if (Hint.t == 4)
				{
					Hint.paintFlare = false;
					Hint.x -= num;
					Hint.y += 2;
				}
				if (Hint.t == 6)
				{
					Hint.paintFlare = true;
					Hint.x += num;
					Hint.y -= 2;
				}
				if (Hint.t == 8)
				{
					Hint.paintFlare = false;
					Hint.x -= num;
					Hint.y += 2;
				}
				if (Hint.t == 10)
				{
					Hint.x -= num;
					Hint.y += 2;
					Hint.activeClick = false;
					Hint.t = 0;
				}
			}
		}
	}

	// Token: 0x04000570 RID: 1392
	public static int x;

	// Token: 0x04000571 RID: 1393
	public static int y;

	// Token: 0x04000572 RID: 1394
	public static int type;

	// Token: 0x04000573 RID: 1395
	public static int t;

	// Token: 0x04000574 RID: 1396
	public static int xF;

	// Token: 0x04000575 RID: 1397
	public static int yF;

	// Token: 0x04000576 RID: 1398
	public static bool isShow;

	// Token: 0x04000577 RID: 1399
	public static bool activeClick;

	// Token: 0x04000578 RID: 1400
	public static bool isViewMap;

	// Token: 0x04000579 RID: 1401
	public static bool isCloseMap;

	// Token: 0x0400057A RID: 1402
	public static bool isViewPotential;

	// Token: 0x0400057B RID: 1403
	public static bool isPaint;

	// Token: 0x0400057C RID: 1404
	public static bool isCamera;

	// Token: 0x0400057D RID: 1405
	public static int trans;

	// Token: 0x0400057E RID: 1406
	public static bool paintFlare;

	// Token: 0x0400057F RID: 1407
	public static bool isPaintArrow;

	// Token: 0x04000580 RID: 1408
	private int s = 2;
}
