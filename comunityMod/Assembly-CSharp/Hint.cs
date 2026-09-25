using System;

// Token: 0x02000042 RID: 66
public class Hint
{
	// Token: 0x060003FA RID: 1018 RVA: 0x00049BEA File Offset: 0x00047DEA
	public static bool isOnTask(int tastId, int index)
	{
		return global::Char.myCharz().taskMaint != null && (int)global::Char.myCharz().taskMaint.taskId == tastId && global::Char.myCharz().taskMaint.index == index;
	}

	// Token: 0x060003FB RID: 1019 RVA: 0x00049C20 File Offset: 0x00047E20
	public static bool isPaintz()
	{
		return (!Hint.isOnTask(0, 3) || GameCanvas.panel.currentTabIndex != 0 || (GameCanvas.panel.cmy >= 0 && GameCanvas.panel.cmy <= 30)) && (!Hint.isOnTask(2, 0) || !GameCanvas.panel.isShow || GameCanvas.panel.currentTabIndex == 0);
	}

	// Token: 0x060003FC RID: 1020 RVA: 0x00049C84 File Offset: 0x00047E84
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
			Hint.type = (GameCanvas.isTouch ? 1 : 0);
		}
	}

	// Token: 0x060003FD RID: 1021 RVA: 0x00049CE4 File Offset: 0x00047EE4
	public static void nextMap(int index)
	{
		if (!GameCanvas.panel.isShow && PopUp.vPopups.size() - 1 >= index)
		{
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
	}

	// Token: 0x060003FE RID: 1022 RVA: 0x00049D7C File Offset: 0x00047F7C
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
			if (((Mob)GameScr.vMob.elementAt(i)).isHintFocus)
			{
				flag = true;
				break;
			}
		}
		int j = 0;
		while (j < GameScr.vMob.size())
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(j);
			if (mob.isHintFocus)
			{
				Hint.x = mob.x;
				Hint.y = mob.y + 5;
				Hint.isCamera = true;
				if (mob.status == 0)
				{
					mob.isHintFocus = false;
					return;
				}
				break;
			}
			else
			{
				if (!flag)
				{
					if (mob.status != 0)
					{
						mob.isHintFocus = true;
						return;
					}
					mob.isHintFocus = false;
				}
				j++;
			}
		}
	}

	// Token: 0x060003FF RID: 1023 RVA: 0x00049E4C File Offset: 0x0004804C
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

	// Token: 0x06000400 RID: 1024 RVA: 0x00049EDC File Offset: 0x000480DC
	public static void paintArrowPointToHint(mGraphics g)
	{
		try
		{
			if (Hint.isPaintArrow && (Hint.x <= GameScr.cmx || Hint.x >= GameScr.cmx + GameScr.gW || Hint.y <= GameScr.cmy || Hint.y >= GameScr.cmy + GameScr.gH) && GameCanvas.gameTick % 10 >= 5 && ChatPopup.currChatPopup == null && ChatPopup.serverChatPopUp == null && !GameCanvas.panel.isShow && Hint.isCamera)
			{
				int num = Hint.x - global::Char.myCharz().cx;
				int num2 = Hint.y - global::Char.myCharz().cy;
				int num3 = 0;
				int num4 = 0;
				int num5 = 0;
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
						num5 = 0;
					}
					else
					{
						num3 = GameScr.gW / 2;
						num4 = GameScr.gH - 10;
						num5 = 5;
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
						num5 = 0;
					}
					else
					{
						num3 = GameScr.gW / 2;
						num4 = 10;
						num5 = 6;
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
						num5 = 3;
					}
					else
					{
						num3 = GameScr.gW / 2;
						num4 = GameScr.gH - 10;
						num5 = 5;
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
						num5 = 3;
					}
					else
					{
						num3 = GameScr.gW / 2;
						num4 = 10;
						num5 = 6;
					}
				}
				GameScr.resetTranslate(g);
				g.drawRegion(GameScr.arrow, 0, 0, 13, 16, num5, num3, num4, StaticObj.VCENTER_HCENTER);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000401 RID: 1025 RVA: 0x0004A108 File Offset: 0x00048308
	public static void paint(mGraphics g)
	{
		if (ChatPopup.serverChatPopUp != null || global::Char.myCharz().isUsePlane || global::Char.myCharz().isTeleport)
		{
			return;
		}
		Hint.paintArrowPointToHint(g);
		if (GameCanvas.menu.tDelay == 0 && Hint.isPaint && ChatPopup.scr == null && !global::Char.ischangingMap && GameCanvas.currentScreen == GameScr.gI() && (!GameCanvas.panel.isShow || GameCanvas.panel.cmx == 0))
		{
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
	}

	// Token: 0x06000402 RID: 1026 RVA: 0x0004A214 File Offset: 0x00048414
	public static void hint()
	{
		if (global::Char.myCharz().taskMaint == null || GameCanvas.currentScreen != GameScr.instance)
		{
			Hint.isPaint = false;
			Hint.isPaintArrow = false;
			return;
		}
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
				return;
			}
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
			return;
		case 1:
			if (ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14)
			{
				Hint.x = GameCanvas.w / 2;
				Hint.y = GameCanvas.h - 15;
				return;
			}
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
					return;
				}
				Hint.clickNpc();
			}
			return;
		case 2:
			if (ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14)
			{
				Hint.x = GameCanvas.w / 2;
				Hint.y = GameCanvas.h - 15;
				return;
			}
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
						int num = ((GameCanvas.h <= 300) ? 10 : 15);
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
				if (global::Char.myCharz().cMP <= 0)
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
			return;
		case 3:
			if (ChatPopup.currChatPopup != null || global::Char.myCharz().statusMe == 14)
			{
				Hint.x = GameCanvas.w / 2;
				Hint.y = GameCanvas.h - 15;
				return;
			}
			if (index == 0)
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
					int h = GameCanvas.h;
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
				if (global::Char.myCharz().cMP <= 0)
				{
					Hint.x = GameScr.xHP + 5;
					Hint.y = GameScr.yHP + 13;
					Hint.isCamera = false;
					return;
				}
			}
			else
			{
				Hint.isPaint = false;
				Hint.isPaintArrow = false;
			}
			return;
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
			return;
		}
	}

	// Token: 0x06000403 RID: 1027 RVA: 0x0004A880 File Offset: 0x00048A80
	public static void update()
	{
		Hint.hint();
		int num = ((Hint.trans != 0) ? (-2) : 2);
		if (!Hint.activeClick)
		{
			Hint.paintFlare = false;
			Hint.t++;
			if (Hint.t == 50)
			{
				Hint.t = 0;
				Hint.activeClick = true;
			}
			return;
		}
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
		if (Hint.type != 1)
		{
			return;
		}
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

	// Token: 0x040008E0 RID: 2272
	public static int x;

	// Token: 0x040008E1 RID: 2273
	public static int y;

	// Token: 0x040008E2 RID: 2274
	public static int type;

	// Token: 0x040008E3 RID: 2275
	public static int t;

	// Token: 0x040008E4 RID: 2276
	public static int xF;

	// Token: 0x040008E5 RID: 2277
	public static int yF;

	// Token: 0x040008E6 RID: 2278
	public static bool isShow;

	// Token: 0x040008E7 RID: 2279
	public static bool activeClick;

	// Token: 0x040008E8 RID: 2280
	public static bool isViewMap;

	// Token: 0x040008E9 RID: 2281
	public static bool isCloseMap;

	// Token: 0x040008EA RID: 2282
	public static bool isViewPotential;

	// Token: 0x040008EB RID: 2283
	public static bool isPaint;

	// Token: 0x040008EC RID: 2284
	public static bool isCamera;

	// Token: 0x040008ED RID: 2285
	public static int trans;

	// Token: 0x040008EE RID: 2286
	public static bool paintFlare;

	// Token: 0x040008EF RID: 2287
	public static bool isPaintArrow;

	// Token: 0x040008F0 RID: 2288
	internal int s = 2;
}
