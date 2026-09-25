using System;
using System.Collections.Generic;
using Assets.src.g;
using UnityEngine;

// Token: 0x02000084 RID: 132
public class Panel : IActionListener, IChatable
{
	// Token: 0x06000648 RID: 1608 RVA: 0x0005B0AC File Offset: 0x000592AC
	public Panel()
	{
		this.init();
		this.cmdClose = new Command(string.Empty, this, 1003, null);
		this.cmdClose.img = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
		this.cmdClose.cmdClosePanel = true;
		this.currItem = null;
	}

	// Token: 0x06000649 RID: 1609 RVA: 0x0005B4B4 File Offset: 0x000596B4
	public static void loadBg()
	{
		Panel.imgMap = GameCanvas.loadImage("/img/map" + TileMap.planetID.ToString() + ".png");
		Panel.imgBantay = GameCanvas.loadImage("/mainImage/myTexture2dbantay.png");
		Panel.imgX = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png");
		Panel.imgXu = GameCanvas.loadImage("/mainImage/myTexture2dimgMoney.png");
		Panel.imgLuong = GameCanvas.loadImage("/mainImage/myTexture2dimgDiamond.png");
		Panel.imgLuongKhoa = GameCanvas.loadImage("/mainImage/luongkhoa.png");
		Panel.imgUp = GameCanvas.loadImage("/mainImage/myTexture2dup.png");
		Panel.imgDown = GameCanvas.loadImage("/mainImage/myTexture2ddown.png");
		Panel.imgStar = GameCanvas.loadImage("/mainImage/star.png");
		Panel.imgMaxStar = GameCanvas.loadImage("/mainImage/starE.png");
		Panel.imgStar8 = GameCanvas.loadImage("/mainImage/star8.png");
		Panel.imgStar9 = mSystem.loadImage("/mainImage/star9.png");
		Panel.imgStarCuongHoa = mSystem.loadImage("/mainImage/starCH.png");
		Panel.imgNew = GameCanvas.loadImage("/mainImage/new.png");
		Panel.imgTicket = GameCanvas.loadImage("/mainImage/ticket12.png");
	}

	// Token: 0x0600064A RID: 1610 RVA: 0x0005B5B8 File Offset: 0x000597B8
	public void init()
	{
		this.pX = GameCanvas.pxLast + this.cmxMap;
		this.pY = GameCanvas.pyLast + this.cmyMap;
		this.lastTabIndex = new int[this.tabName.Length];
		for (int i = 0; i < this.lastTabIndex.Length; i++)
		{
			this.lastTabIndex[i] = -1;
		}
	}

	// Token: 0x0600064B RID: 1611 RVA: 0x0005B618 File Offset: 0x00059818
	public int getXMap()
	{
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			if (TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i])
			{
				return Panel.mapX[(int)TileMap.planetID][i];
			}
		}
		return -1;
	}

	// Token: 0x0600064C RID: 1612 RVA: 0x0005B660 File Offset: 0x00059860
	public int getYMap()
	{
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			if (TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i])
			{
				return Panel.mapY[(int)TileMap.planetID][i];
			}
		}
		return -1;
	}

	// Token: 0x0600064D RID: 1613 RVA: 0x0005B6A8 File Offset: 0x000598A8
	public int getXMapTask()
	{
		if (global::Char.myCharz().taskMaint == null)
		{
			return -1;
		}
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			if (GameScr.mapTasks[global::Char.myCharz().taskMaint.index] == Panel.mapId[(int)TileMap.planetID][i])
			{
				return Panel.mapX[(int)TileMap.planetID][i];
			}
		}
		return -1;
	}

	// Token: 0x0600064E RID: 1614 RVA: 0x0005B710 File Offset: 0x00059910
	public int getYMapTask()
	{
		if (global::Char.myCharz().taskMaint == null)
		{
			return -1;
		}
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			if (GameScr.mapTasks[global::Char.myCharz().taskMaint.index] == Panel.mapId[(int)TileMap.planetID][i])
			{
				return Panel.mapY[(int)TileMap.planetID][i];
			}
		}
		return -1;
	}

	// Token: 0x0600064F RID: 1615 RVA: 0x0005B778 File Offset: 0x00059978
	internal void setType(int position)
	{
		this.typeShop = -1;
		this.W = Panel.WIDTH_PANEL;
		this.H = GameCanvas.h;
		this.X = 0;
		this.Y = 0;
		this.ITEM_HEIGHT = 24;
		this.position = position;
		if (position == 0)
		{
			this.xScroll = 2;
			this.yScroll = 80;
			this.wScroll = this.W - 4;
			this.hScroll = this.H - 96;
			this.cmx = this.wScroll;
			this.cmtoX = 0;
			this.X = 0;
		}
		else if (position == 1)
		{
			this.wScroll = this.W - 4;
			this.xScroll = GameCanvas.w - this.wScroll;
			this.yScroll = 80;
			this.hScroll = this.H - 96;
			this.X = this.xScroll - 2;
			this.cmx = -(GameCanvas.w + this.W);
			this.cmtoX = GameCanvas.w - this.W;
		}
		this.TAB_W = this.W / 5 - 1;
		this.currentTabIndex = 0;
		this.currentTabName = this.tabName[this.type];
		if (this.currentTabName.Length < 5)
		{
			this.TAB_W += 5;
		}
		this.startTabPos = this.xScroll + this.wScroll / 2 - this.currentTabName.Length * this.TAB_W / 2;
		this.lastSelect = new int[this.currentTabName.Length];
		this.cmyLast = new int[this.currentTabName.Length];
		for (int i = 0; i < this.currentTabName.Length; i++)
		{
			this.lastSelect[i] = (GameCanvas.isTouch ? (-1) : 0);
		}
		if (this.lastTabIndex[this.type] != -1)
		{
			this.currentTabIndex = this.lastTabIndex[this.type];
		}
		if (this.currentTabIndex < 0)
		{
			this.currentTabIndex = 0;
		}
		if (this.currentTabIndex > this.currentTabName.Length - 1)
		{
			this.currentTabIndex = this.currentTabName.Length - 1;
		}
		this.scroll = null;
	}

	// Token: 0x06000650 RID: 1616 RVA: 0x0005B98C File Offset: 0x00059B8C
	public void setTypeMapTrans()
	{
		this.type = 14;
		this.setType(0);
		this.setTabMapTrans();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000651 RID: 1617 RVA: 0x0005B9BE File Offset: 0x00059BBE
	public void setTypeInfomatioin()
	{
		this.type = 6;
		this.cmx = this.wScroll;
		this.cmtoX = 0;
	}

	// Token: 0x06000652 RID: 1618 RVA: 0x0005B9DC File Offset: 0x00059BDC
	public void setTypeMap()
	{
		if (!GameScr.gI().isMapFize() && Panel.isPaintMap)
		{
			if (Hint.isOnTask(2, 0))
			{
				Hint.isViewMap = true;
				GameScr.info1.addInfo(mResources.go_to_quest, 0);
			}
			if (Hint.isOnTask(3, 0))
			{
				Hint.isViewPotential = true;
			}
			this.type = 4;
			this.currentTabName = this.tabName[this.type];
			this.startTabPos = this.xScroll + this.wScroll / 2 - this.currentTabName.Length * this.TAB_W / 2;
			this.cmx = (this.cmtoX = 0);
			this.setTabMap();
		}
	}

	// Token: 0x06000653 RID: 1619 RVA: 0x0005BA88 File Offset: 0x00059C88
	public void setTypeArchivement()
	{
		this.currentListLength = global::Char.myCharz().arrArchive.Length;
		this.setType(0);
		this.type = 9;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = 0);
		}
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
	}

	// Token: 0x06000654 RID: 1620 RVA: 0x0005BB44 File Offset: 0x00059D44
	public void setTypeKiGuiOnly()
	{
		this.type = 17;
		this.setType(1);
		this.setTabKiGui();
		this.typeShop = 2;
		this.currentTabIndex = 0;
	}

	// Token: 0x06000655 RID: 1621 RVA: 0x0005BB6C File Offset: 0x00059D6C
	public void setTabChatManager()
	{
		this.currentListLength = this.chats.Count;
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000656 RID: 1622 RVA: 0x00004887 File Offset: 0x00002A87
	public void setTabChatPlayer()
	{
	}

	// Token: 0x06000657 RID: 1623 RVA: 0x00004887 File Offset: 0x00002A87
	public void setTypeChatPlayer()
	{
	}

	// Token: 0x06000658 RID: 1624 RVA: 0x0005BC0C File Offset: 0x00059E0C
	public void setTabKiGui()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = global::Char.myCharz().arrItemShop[4].Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
	}

	// Token: 0x06000659 RID: 1625 RVA: 0x0005BCC8 File Offset: 0x00059EC8
	public void setTypeBodyOnly()
	{
		this.type = 7;
		this.setType(1);
		this.setTabInventory(true);
		this.currentTabIndex = 0;
	}

	// Token: 0x0600065A RID: 1626 RVA: 0x0005BCE6 File Offset: 0x00059EE6
	public void addChatMessage(InfoItem info)
	{
		this.logChat.insertElementAt(info, 0);
		if (this.logChat.size() > 20)
		{
			this.logChat.removeElementAt(this.logChat.size() - 1);
		}
	}

	// Token: 0x0600065B RID: 1627 RVA: 0x000151BF File Offset: 0x000133BF
	internal bool IsNewMessage(string name)
	{
		return false;
	}

	// Token: 0x0600065C RID: 1628 RVA: 0x000151BF File Offset: 0x000133BF
	public bool IsHaveNewMessage()
	{
		return false;
	}

	// Token: 0x0600065D RID: 1629 RVA: 0x00004887 File Offset: 0x00002A87
	internal void ClearNewMessage(string name)
	{
	}

	// Token: 0x0600065E RID: 1630 RVA: 0x0005BD1C File Offset: 0x00059F1C
	public void addPlayerMenu(Command pm)
	{
		this.vPlayerMenu.addElement(pm);
	}

	// Token: 0x0600065F RID: 1631 RVA: 0x0005BD2C File Offset: 0x00059F2C
	public void setTabPlayerMenu()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = this.vPlayerMenu.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
	}

	// Token: 0x06000660 RID: 1632 RVA: 0x0005BDE5 File Offset: 0x00059FE5
	public void setTypeFlag()
	{
		this.type = 18;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.setTabFlag();
	}

	// Token: 0x06000661 RID: 1633 RVA: 0x0005BE18 File Offset: 0x0005A018
	public void setTabFlag()
	{
		this.currentListLength = this.vFlag.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		if (this.selected > this.currentListLength - 1)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000662 RID: 1634 RVA: 0x0005BEE6 File Offset: 0x0005A0E6
	public void setTypePlayerMenu(global::Char c)
	{
		this.type = 10;
		this.setType(0);
		this.setTabPlayerMenu();
		this.charMenu = c;
	}

	// Token: 0x06000663 RID: 1635 RVA: 0x0005BF04 File Offset: 0x0005A104
	public void setTypeFriend()
	{
		this.type = 11;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.setTabFriend();
	}

	// Token: 0x06000664 RID: 1636 RVA: 0x0005BF34 File Offset: 0x0005A134
	public void setTypeEnemy()
	{
		this.type = 16;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.setTabEnemy();
	}

	// Token: 0x06000665 RID: 1637 RVA: 0x0005BF64 File Offset: 0x0005A164
	public void setTypeTop(sbyte t)
	{
		this.type = 15;
		this.setType(0);
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.setTabTop();
		this.isThachDau = t != 0;
	}

	// Token: 0x06000666 RID: 1638 RVA: 0x0005BFA4 File Offset: 0x0005A1A4
	public void setTabTop()
	{
		this.currentListLength = this.vTop.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		if (this.selected > this.currentListLength - 1)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000667 RID: 1639 RVA: 0x0005C074 File Offset: 0x0005A274
	public void setTabFriend()
	{
		this.currentListLength = this.vFriend.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		if (this.selected > this.currentListLength - 1)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000668 RID: 1640 RVA: 0x0005C144 File Offset: 0x0005A344
	public void setTabEnemy()
	{
		this.currentListLength = this.vEnemy.size();
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		if (this.selected > this.currentListLength - 1)
		{
			this.selected = this.currentListLength - 1;
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000669 RID: 1641 RVA: 0x0005C212 File Offset: 0x0005A412
	public void setTypeMessage()
	{
		this.type = 8;
		this.setType(0);
		this.setTabMessage();
		this.currentTabIndex = 0;
	}

	// Token: 0x0600066A RID: 1642 RVA: 0x0005C212 File Offset: 0x0005A412
	public void setTypeLockInventory()
	{
		this.type = 8;
		this.setType(0);
		this.setTabMessage();
		this.currentTabIndex = 0;
	}

	// Token: 0x0600066B RID: 1643 RVA: 0x0005C22F File Offset: 0x0005A42F
	public void setTypeShop(int typeShop)
	{
		this.type = 1;
		this.setType(0);
		this.setTabShop();
		this.currentTabIndex = 0;
		this.typeShop = typeShop;
	}

	// Token: 0x0600066C RID: 1644 RVA: 0x0005C254 File Offset: 0x0005A454
	public void setTypeBox()
	{
		this.type = 2;
		if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
		{
			Panel.boxTabName = new string[][] { mResources.chestt };
		}
		else
		{
			Panel.boxTabName = new string[][]
			{
				mResources.chestt,
				mResources.inventory
			};
		}
		this.tabName[2] = Panel.boxTabName;
		this.setType(0);
		if (this.currentTabIndex == 0)
		{
			this.setTabBox();
		}
		if (this.currentTabIndex == 1)
		{
			this.setTabInventory(true);
		}
		if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
		{
			GameCanvas.panel2 = new Panel();
			GameCanvas.panel2.tabName[7] = new string[][] { new string[] { string.Empty } };
			GameCanvas.panel2.setTypeBodyOnly();
			GameCanvas.panel2.show();
		}
	}

	// Token: 0x0600066D RID: 1645 RVA: 0x0005C328 File Offset: 0x0005A528
	public void setTypeCombine()
	{
		this.type = 12;
		if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
		{
			Panel.boxCombine = new string[][] { mResources.combine };
		}
		else
		{
			Panel.boxCombine = new string[][]
			{
				mResources.combine,
				mResources.inventory
			};
		}
		this.tabName[this.type] = Panel.boxCombine;
		this.setType(0);
		if (this.currentTabIndex == 0)
		{
			this.setTabCombine();
		}
		if (this.currentTabIndex == 1)
		{
			this.setTabInventory(true);
		}
		if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
		{
			GameCanvas.panel2 = new Panel();
			GameCanvas.panel2.tabName[7] = new string[][] { new string[] { string.Empty } };
			GameCanvas.panel2.setTypeBodyOnly();
			GameCanvas.panel2.show();
		}
		this.combineSuccess = -1;
		this.isDoneCombine = true;
	}

	// Token: 0x0600066E RID: 1646 RVA: 0x0005C410 File Offset: 0x0005A610
	public void setTabCombine()
	{
		this.currentListLength = this.vItemCombine.size() + 1;
		this.ITEM_HEIGHT = 24;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 9;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
	}

	// Token: 0x0600066F RID: 1647 RVA: 0x0005C4CC File Offset: 0x0005A6CC
	public void setTypeAuto()
	{
		this.type = 22;
		this.setType(0);
		this.setTabAuto();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000670 RID: 1648 RVA: 0x0005C500 File Offset: 0x0005A700
	internal void setTabAuto()
	{
		this.currentListLength = Panel.strAuto.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000671 RID: 1649 RVA: 0x0005C5B8 File Offset: 0x0005A7B8
	public void setTypePetMain()
	{
		this.type = 21;
		if (GameCanvas.panel2 != null)
		{
			Panel.boxPet = mResources.petMainTab2;
		}
		else
		{
			Panel.boxPet = mResources.petMainTab;
		}
		this.tabName[21] = Panel.boxPet;
		if (global::Char.myCharz().cgender == 1)
		{
			this.strStatus = new string[]
			{
				mResources.follow,
				mResources.defend,
				mResources.attack,
				mResources.gohome,
				mResources.fusion,
				mResources.fusionForever
			};
		}
		else
		{
			this.strStatus = new string[]
			{
				mResources.follow,
				mResources.defend,
				mResources.attack,
				mResources.gohome,
				mResources.fusion
			};
		}
		this.setType(2);
		if (this.currentTabIndex == 0)
		{
			this.setTabPetInventory();
		}
		if (this.currentTabIndex == 1)
		{
			this.setTabPetStatus();
		}
		if (this.currentTabIndex == 2)
		{
			this.setTabInventory(true);
		}
	}

	// Token: 0x06000672 RID: 1650 RVA: 0x0005C6AC File Offset: 0x0005A8AC
	public void setTypeMain()
	{
		this.type = 0;
		this.setType(0);
		if (this.currentTabIndex == 1)
		{
			this.setTabInventory(true);
		}
		if (this.currentTabIndex == 2)
		{
			this.setTabSkill();
		}
		if (this.currentTabIndex == 3)
		{
			if (this.mainTabName.Length == 4)
			{
				this.setTabTool();
			}
			else
			{
				this.setTabClans();
			}
		}
		if (this.currentTabIndex == 4)
		{
			this.setTabTool();
		}
	}

	// Token: 0x06000673 RID: 1651 RVA: 0x0005C718 File Offset: 0x0005A918
	public void setTypeZone()
	{
		this.type = 3;
		this.setType(0);
		this.setTabZone();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000674 RID: 1652 RVA: 0x0005C74C File Offset: 0x0005A94C
	public void addItemDetail(Item item)
	{
		try
		{
			this.cp = new ChatPopup();
			string text = string.Empty;
			string text2 = string.Empty;
			if ((int)item.template.gender != global::Char.myCharz().cgender)
			{
				if (item.template.gender == 0)
				{
					text2 = text2 + "\n|7|1|" + mResources.from_earth;
				}
				else if (item.template.gender == 1)
				{
					text2 = text2 + "\n|7|1|" + mResources.from_namec;
				}
				else if (item.template.gender == 2)
				{
					text2 = text2 + "\n|7|1|" + mResources.from_sayda;
				}
			}
			string text3 = string.Empty;
			if (item.itemOption != null)
			{
				for (int i = 0; i < item.itemOption.Length; i++)
				{
					if (item.itemOption[i].optionTemplate.id == 72)
					{
						text3 = " [+" + item.itemOption[i].param.ToString() + "]";
					}
				}
			}
			bool flag = false;
			if (item.itemOption != null)
			{
				for (int j = 0; j < item.itemOption.Length; j++)
				{
					if (item.itemOption[j].optionTemplate.id == 41)
					{
						flag = true;
						if (item.itemOption[j].param == 1)
						{
							text2 = text2 + "|0|1|" + item.template.name + text3;
						}
						if (item.itemOption[j].param == 2)
						{
							text2 = text2 + "|2|1|" + item.template.name + text3;
						}
						if (item.itemOption[j].param == 3)
						{
							text2 = text2 + "|8|1|" + item.template.name + text3;
						}
						if (item.itemOption[j].param == 4)
						{
							text2 = text2 + "|7|1|" + item.template.name + text3;
						}
					}
				}
			}
			if (!flag)
			{
				text2 = text2 + "|0|1|" + item.template.name + text3;
			}
			if (item.itemOption != null)
			{
				int k = 0;
				while (k < item.itemOption.Length)
				{
					if (item.itemOption[k].optionTemplate.name.StartsWith("$"))
					{
						text = item.itemOption[k].getOptiongColor();
						if (item.itemOption[k].param == 1)
						{
							text2 = text2 + "\n|1|1|" + text;
						}
						if (item.itemOption[k].param == 0)
						{
							text2 = text2 + "\n|0|1|" + text;
							goto IL_0328;
						}
						goto IL_0328;
					}
					else
					{
						text = item.itemOption[k].getOptionString();
						if (text.Equals(string.Empty))
						{
							goto IL_0328;
						}
						if (item.itemOption[k].optionTemplate.id != 72)
						{
							if (item.itemOption[k].optionTemplate.id == 102)
							{
								this.cp.starSlot = (sbyte)item.itemOption[k].param;
								goto IL_0328;
							}
							if (item.itemOption[k].optionTemplate.id == 107)
							{
								this.cp.maxStarSlot = (sbyte)item.itemOption[k].param;
								goto IL_0328;
							}
							text2 = text2 + "\n|1|1|" + text;
							goto IL_0328;
						}
					}
					IL_03BD:
					k++;
					continue;
					IL_0328:
					if (item.itemOption[k].optionTemplate.id != 228)
					{
						goto IL_03BD;
					}
					Res.outz("========>>> " + item.itemOption[k].optionTemplate.name + "_" + item.itemOption[k].param.ToString());
					if (item.itemOption[k].param > 7)
					{
						for (int l = 0; l < item.itemOption[k].param - 7; l++)
						{
							this.cp.starCuongHoa[l + 7] = true;
						}
						goto IL_03BD;
					}
					goto IL_03BD;
				}
			}
			if (this.currItem.template.strRequire > 1)
			{
				string text4 = mResources.pow_request + ": " + this.currItem.template.strRequire.ToString();
				if ((long)this.currItem.template.strRequire > global::Char.myCharz().cPower)
				{
					string text5 = text2 + "\n|3|1|" + text4;
					text2 = string.Concat(new string[]
					{
						text5,
						"\n|3|1|",
						mResources.your_pow,
						": ",
						global::Char.myCharz().cPower.ToString()
					});
				}
				else
				{
					text2 = text2 + "\n|6|1|" + text4;
				}
			}
			else
			{
				text2 += "\n|6|1|";
			}
			this.currItem.compare = this.getCompare(this.currItem);
			text2 = text2 + "\n--" + "\n|6|" + item.template.description;
			if (!item.reason.Equals(string.Empty))
			{
				if (!item.template.description.Equals(string.Empty))
				{
					text2 += "\n--";
				}
				text2 = text2 + "\n|2|" + item.reason;
			}
			if (this.cp.maxStarSlot > 0)
			{
				text2 += "\n\n";
			}
			this.popUpDetailInit(this.cp, text2);
			this.idIcon = (int)item.template.iconID;
			this.partID = null;
			this.charInfo = null;
		}
		catch (Exception ex)
		{
			Res.outz("ex " + ex.StackTrace);
		}
	}

	// Token: 0x06000675 RID: 1653 RVA: 0x0005CCEC File Offset: 0x0005AEEC
	public void popUpDetailInit(ChatPopup cp, string chat)
	{
		cp.isClip = false;
		cp.sayWidth = 180;
		cp.cx = 3 + this.X - ((this.X != 0) ? (Res.abs(cp.sayWidth - this.W) + 8) : 0);
		cp.says = mFont.tahoma_7_red.splitFontArray(chat, cp.sayWidth - 10);
		cp.delay = 10000000;
		cp.c = null;
		cp.sayRun = 7;
		cp.ch = 15 - cp.sayRun + cp.says.Length * 12 + 10;
		if (cp.ch > GameCanvas.h - 80)
		{
			cp.ch = GameCanvas.h - 80;
			cp.lim = cp.says.Length * 12 - cp.ch + 17;
			if (cp.lim < 0)
			{
				cp.lim = 0;
			}
			ChatPopup.cmyText = 0;
			cp.isClip = true;
		}
		cp.cy = GameCanvas.menu.menuY - cp.ch;
		while (cp.cy < 10)
		{
			cp.cy++;
			GameCanvas.menu.menuY++;
		}
		cp.mH = 0;
		cp.strY = 10;
	}

	// Token: 0x06000676 RID: 1654 RVA: 0x0005CE34 File Offset: 0x0005B034
	public void popUpDetailInitArray(ChatPopup cp, string[] chat)
	{
		cp.sayWidth = 160;
		cp.cx = 3 + this.X;
		cp.says = chat;
		cp.delay = 10000000;
		cp.c = null;
		cp.sayRun = 7;
		cp.ch = 15 - cp.sayRun + cp.says.Length * 12 + 10;
		cp.cy = GameCanvas.menu.menuY - cp.ch;
		cp.mH = 0;
		cp.strY = 10;
	}

	// Token: 0x06000677 RID: 1655 RVA: 0x0005CEC0 File Offset: 0x0005B0C0
	public void addMessageDetail(ClanMessage cm)
	{
		this.cp = new ChatPopup();
		string text = "|0|" + cm.playerName + "\n|1|" + Member.getRole((int)cm.role);
		for (int i = 0; i < this.myMember.size(); i++)
		{
			Member member = (Member)this.myMember.elementAt(i);
			if (cm.playerId == member.ID)
			{
				string text2 = text;
				text2 = string.Concat(new string[]
				{
					text2,
					"\n|5|",
					mResources.clan_capsuledonate,
					": ",
					member.clanPoint.ToString()
				});
				text2 = string.Concat(new string[]
				{
					text2,
					"\n|5|",
					mResources.clan_capsuleself,
					": ",
					member.curClanPoint.ToString()
				});
				text2 = string.Concat(new string[]
				{
					text2,
					"\n|4|",
					mResources.give_pea,
					": ",
					member.donate.ToString(),
					mResources.time
				});
				text = string.Concat(new string[]
				{
					text2,
					"\n|4|",
					mResources.receive_pea,
					": ",
					member.receive_donate.ToString(),
					mResources.time
				});
				this.partID = new int[]
				{
					(int)member.head,
					(int)member.leg,
					(int)member.body
				};
				break;
			}
		}
		text += "\n--";
		for (int j = 0; j < cm.chat.Length; j++)
		{
			text = text + "\n" + cm.chat[j];
		}
		if (cm.type == 1)
		{
			string text3 = text;
			text = string.Concat(new string[]
			{
				text3,
				"\n|6|",
				mResources.received,
				" ",
				cm.recieve.ToString(),
				"/",
				cm.maxCap.ToString()
			});
		}
		this.popUpDetailInit(this.cp, text);
		this.charInfo = null;
	}

	// Token: 0x06000678 RID: 1656 RVA: 0x0005D0F8 File Offset: 0x0005B2F8
	public void addThachDauDetail(TopInfo t)
	{
		string text = "|0|1|" + t.name + "\n|1|Top " + t.rank + "\n|1|" + t.info + "\n|2|" + t.info2;
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.partID = new int[]
		{
			t.headID,
			(int)t.leg,
			(int)t.body
		};
		this.currItem = null;
		this.charInfo = null;
	}

	// Token: 0x06000679 RID: 1657 RVA: 0x0005D198 File Offset: 0x0005B398
	public void addClanMemberDetail(Member m)
	{
		string text = "|0|1|" + m.name;
		string text2 = "\n|2|1|";
		if (m.role == 0)
		{
			text2 = "\n|7|1|";
		}
		if (m.role == 1)
		{
			text2 = "\n|1|1|";
		}
		if (m.role == 2)
		{
			text2 = "\n|0|1|";
		}
		string text3 = text + text2 + Member.getRole((int)m.role);
		text3 = string.Concat(new string[]
		{
			text3,
			"\n|2|1|",
			mResources.power,
			": ",
			m.powerPoint
		}) + "\n--";
		text3 = string.Concat(new string[]
		{
			text3,
			"\n|5|",
			mResources.clan_capsuledonate,
			": ",
			m.clanPoint.ToString()
		});
		text3 = string.Concat(new string[]
		{
			text3,
			"\n|5|",
			mResources.clan_capsuleself,
			": ",
			m.curClanPoint.ToString()
		});
		text3 = string.Concat(new string[]
		{
			text3,
			"\n|4|",
			mResources.give_pea,
			": ",
			m.donate.ToString(),
			mResources.time
		});
		text3 = string.Concat(new string[]
		{
			text3,
			"\n|4|",
			mResources.receive_pea,
			": ",
			m.receive_donate.ToString(),
			mResources.time
		});
		text = string.Concat(new string[]
		{
			text3,
			"\n|6|",
			mResources.join_date,
			": ",
			m.joinTime
		});
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.partID = new int[]
		{
			(int)m.head,
			(int)m.leg,
			(int)m.body
		};
		this.currItem = null;
		this.charInfo = null;
	}

	// Token: 0x0600067A RID: 1658 RVA: 0x0005D39C File Offset: 0x0005B59C
	public void addClanDetail(Clan cl)
	{
		try
		{
			string text = "|0|" + cl.name;
			string[] array = mFont.tahoma_7_green.splitFontArray(cl.slogan, this.wScroll - 60);
			for (int i = 0; i < array.Length; i++)
			{
				text = text + "\n|2|" + array[i];
			}
			string text2 = text + "\n--";
			text2 = string.Concat(new string[]
			{
				text2,
				"\n|7|",
				mResources.clan_leader,
				": ",
				cl.leaderName
			});
			text2 = string.Concat(new string[]
			{
				text2,
				"\n|1|",
				mResources.power_point,
				": ",
				cl.powerPoint
			});
			text2 = string.Concat(new string[]
			{
				text2,
				"\n|4|",
				mResources.member,
				": ",
				cl.currMember.ToString(),
				"/",
				cl.maxMember.ToString()
			});
			text2 = string.Concat(new string[]
			{
				text2,
				"\n|4|",
				mResources.level,
				": ",
				cl.level.ToString()
			});
			text = string.Concat(new string[]
			{
				text2,
				"\n|4|",
				mResources.clan_birthday,
				": ",
				NinjaUtil.getDate(cl.date)
			});
			this.cp = new ChatPopup();
			this.popUpDetailInit(this.cp, text);
			this.idIcon = (int)ClanImage.getClanImage((short)cl.imgID).idImage[0];
			this.currItem = null;
		}
		catch (Exception ex)
		{
			Res.outz("Throw  exception " + ex.StackTrace);
		}
	}

	// Token: 0x0600067B RID: 1659 RVA: 0x0005D588 File Offset: 0x0005B788
	public void addSkillDetail(SkillTemplate tp, Skill skill, Skill nextSkill)
	{
		string text = "|0|" + tp.name;
		for (int i = 0; i < tp.description.Length; i++)
		{
			text = text + "\n|4|" + tp.description[i];
		}
		text += "\n--";
		if (skill != null)
		{
			string text2 = text;
			text2 = string.Concat(new string[]
			{
				text2,
				"\n|2|",
				mResources.cap_do,
				": ",
				skill.point.ToString()
			}) + "\n|5|" + NinjaUtil.replace(tp.damInfo, "#", skill.damage.ToString() + string.Empty);
			text2 = string.Concat(new string[]
			{
				text2,
				"\n|5|",
				mResources.KI_consume,
				skill.manaUse.ToString(),
				(tp.manaUseType != 1) ? string.Empty : "%"
			});
			text = string.Concat(new string[]
			{
				text2,
				"\n|5|",
				mResources.cooldown,
				": ",
				skill.strTimeReplay(),
				"s"
			}) + "\n--";
			if (skill.point == tp.maxPoint)
			{
				text = text + "\n|0|" + mResources.max_level_reach;
			}
			else
			{
				if (!skill.template.isSkillSpec())
				{
					text2 = text;
					text = string.Concat(new string[]
					{
						text2,
						"\n|1|",
						mResources.next_level_require,
						Res.formatNumber(nextSkill.powRequire),
						" ",
						mResources.potential
					});
				}
				text = text + "\n|4|" + NinjaUtil.replace(tp.damInfo, "#", nextSkill.damage.ToString() + string.Empty);
			}
		}
		else
		{
			string text3 = text + "\n|2|" + mResources.not_learn;
			text3 = string.Concat(new string[]
			{
				text3,
				"\n|1|",
				mResources.learn_require,
				Res.formatNumber(nextSkill.powRequire),
				" ",
				mResources.potential
			}) + "\n|4|" + NinjaUtil.replace(tp.damInfo, "#", nextSkill.damage.ToString() + string.Empty);
			text3 = string.Concat(new string[]
			{
				text3,
				"\n|4|",
				mResources.KI_consume,
				nextSkill.manaUse.ToString(),
				(tp.manaUseType != 1) ? string.Empty : "%"
			});
			text = string.Concat(new string[]
			{
				text3,
				"\n|4|",
				mResources.cooldown,
				": ",
				nextSkill.strTimeReplay(),
				"s"
			});
		}
		this.currItem = null;
		this.partID = null;
		this.charInfo = null;
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.idIcon = 0;
	}

	// Token: 0x0600067C RID: 1660 RVA: 0x0005D8A8 File Offset: 0x0005BAA8
	public void show()
	{
		if (GameCanvas.isTouch)
		{
			this.cmdClose.x = 156;
			this.cmdClose.y = 3;
		}
		else
		{
			this.cmdClose.x = GameCanvas.w - 19;
			this.cmdClose.y = GameCanvas.h - 19;
		}
		this.cmdClose.isPlaySoundButton = false;
		ChatPopup.currChatPopup = null;
		InfoDlg.hide();
		this.timeShow = 20;
		this.isShow = true;
		this.isClose = false;
		SoundMn.gI().panelOpen();
		if (this.isTypeShop())
		{
			global::Char.myCharz().setPartOld();
		}
	}

	// Token: 0x0600067D RID: 1661 RVA: 0x0005D94C File Offset: 0x0005BB4C
	public void chatTFUpdateKey()
	{
		if (this.chatTField != null && this.chatTField.isShow)
		{
			if (this.chatTField.left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(this.chatTField.left)) && this.chatTField.left != null)
			{
				this.chatTField.left.performAction();
			}
			if (this.chatTField.right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(this.chatTField.right)) && this.chatTField.right != null)
			{
				this.chatTField.right.performAction();
			}
			if (this.chatTField.center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(this.chatTField.center)) && this.chatTField.center != null)
			{
				this.chatTField.center.performAction();
			}
			if (this.chatTField.isShow && GameCanvas.keyAsciiPress != 0)
			{
				this.chatTField.keyPressed(GameCanvas.keyAsciiPress);
				GameCanvas.keyAsciiPress = 0;
			}
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
		}
	}

	// Token: 0x0600067E RID: 1662 RVA: 0x0005DA84 File Offset: 0x0005BC84
	public void updateKey()
	{
		if ((this.chatTField != null && this.chatTField.isShow) || !GameCanvas.panel.isDoneCombine || InfoDlg.isShow)
		{
			return;
		}
		if (this.tabIcon != null && this.tabIcon.isShow)
		{
			this.tabIcon.updateKey();
			return;
		}
		if (this.isClose || !this.isShow)
		{
			return;
		}
		if (this.cmdClose.isPointerPressInside())
		{
			this.cmdClose.performAction();
			return;
		}
		if (GameCanvas.keyPressed[13])
		{
			if (this.type != 4)
			{
				this.hide();
				return;
			}
			this.setTypeMain();
			this.cmx = (this.cmtoX = 0);
		}
		if (GameCanvas.keyPressed[12] || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
		{
			if (this.left.idAction > 0)
			{
				this.perform(this.left.idAction, this.left.p);
			}
			else
			{
				this.waitToPerform = 2;
			}
		}
		if (this.Equals(GameCanvas.panel) && GameCanvas.panel2 == null && GameCanvas.isPointerJustRelease && !GameCanvas.isPointer(this.X, this.Y, this.W, this.H) && !this.pointerIsDowning)
		{
			this.hide();
			return;
		}
		if (!this.isClanOption)
		{
			this.updateKeyInTabBar();
		}
		switch (this.type)
		{
		case 0:
			if (this.currentTabIndex == 0)
			{
				this.updateKeyQuest();
				GameCanvas.clearKeyPressed();
				return;
			}
			if (this.currentTabIndex == 1)
			{
				this.updateKeyInventory();
			}
			if (this.currentTabIndex == 2)
			{
				this.updateKeySkill();
			}
			if (this.currentTabIndex == 3)
			{
				if (this.mainTabName.Length == 4)
				{
					this.updateKeyTool();
				}
				else
				{
					this.updateKeyClans();
				}
			}
			if (this.currentTabIndex == 4)
			{
				this.updateKeyTool();
			}
			break;
		case 1:
		case 17:
		case 25:
			if (this.currentTabIndex < this.currentTabName.Length - ((GameCanvas.panel2 == null) ? 1 : 0) && this.type != 17)
			{
				this.updateKeyScrollView();
			}
			else if (this.typeShop == 0)
			{
				this.updateKeyInventory();
			}
			else
			{
				this.updateKeyScrollView();
			}
			break;
		case 2:
			this.updateKeyInventory();
			break;
		case 3:
			this.updateKeyScrollView();
			break;
		case 4:
			this.updateKeyMap();
			GameCanvas.clearKeyPressed();
			return;
		case 7:
			this.updateKeyInventory();
			break;
		case 8:
			this.updateKeyScrollView();
			break;
		case 9:
			this.updateKeyScrollView();
			break;
		case 10:
			this.updateKeyScrollView();
			break;
		case 11:
		case 16:
			this.updateKeyScrollView();
			break;
		case 12:
			this.updateKeyCombine();
			break;
		case 13:
			this.updateKeyGiaoDich();
			break;
		case 14:
			this.updateKeyScrollView();
			break;
		case 15:
			this.updateKeyScrollView();
			break;
		case 18:
			this.updateKeyScrollView();
			break;
		case 19:
			this.updateKeyOption();
			break;
		case 20:
			this.updateKeyOption();
			break;
		case 21:
			if (this.currentTabIndex == 0)
			{
				this.updateKeyScrollView();
			}
			if (this.currentTabIndex == 1)
			{
				this.updateKeyPetStatus();
			}
			if (this.currentTabIndex == 2)
			{
				this.updateKeyScrollView();
			}
			break;
		case 22:
			this.updateKeyAuto();
			break;
		case 23:
		case 24:
			this.updateKeyScrollView();
			break;
		}
		GameCanvas.clearKeyHold();
		for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
		{
			GameCanvas.keyPressed[i] = false;
		}
	}

	// Token: 0x0600067F RID: 1663 RVA: 0x00004887 File Offset: 0x00002A87
	internal void updateKeyAuto()
	{
	}

	// Token: 0x06000680 RID: 1664 RVA: 0x0005DDF4 File Offset: 0x0005BFF4
	internal void updateKeyPetStatus()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x06000681 RID: 1665 RVA: 0x00004887 File Offset: 0x00002A87
	internal void updateKeyPetSkill()
	{
	}

	// Token: 0x06000682 RID: 1666 RVA: 0x0005DDF4 File Offset: 0x0005BFF4
	internal void keyGiaodich()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x06000683 RID: 1667 RVA: 0x0005DDFC File Offset: 0x0005BFFC
	internal void updateKeyGiaoDich()
	{
		if (this.currentTabIndex == 0)
		{
			if (this.Equals(GameCanvas.panel))
			{
				this.updateKeyInventory();
			}
			if (this.Equals(GameCanvas.panel2))
			{
				this.keyGiaodich();
			}
		}
		if (this.currentTabIndex == 1 || this.currentTabIndex == 2)
		{
			this.keyGiaodich();
		}
	}

	// Token: 0x06000684 RID: 1668 RVA: 0x0005DDF4 File Offset: 0x0005BFF4
	internal void updateKeyTool()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x06000685 RID: 1669 RVA: 0x0005DDF4 File Offset: 0x0005BFF4
	internal void updateKeySkill()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x06000686 RID: 1670 RVA: 0x0005DDF4 File Offset: 0x0005BFF4
	internal void updateKeyClanIcon()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x06000687 RID: 1671 RVA: 0x0005DE50 File Offset: 0x0005C050
	public void setTabGiaoDich(bool isMe)
	{
		this.currentListLength = ((!isMe) ? (this.vFriendGD.size() + 3) : (this.vMyGD.size() + 3));
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000688 RID: 1672 RVA: 0x0005DF20 File Offset: 0x0005C120
	public void setTypeGiaoDich(global::Char cGD)
	{
		this.type = 13;
		this.tabName[this.type] = Panel.boxGD;
		this.isAccept = false;
		this.isLock = false;
		this.isFriendLock = false;
		this.vMyGD.removeAllElements();
		this.vFriendGD.removeAllElements();
		this.moneyGD = 0;
		this.friendMoneyGD = 0;
		if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
		{
			GameCanvas.panel2 = new Panel();
			GameCanvas.panel2.type = 13;
			GameCanvas.panel2.tabName[this.type] = new string[][] { mResources.item_receive };
			GameCanvas.panel2.setType(1);
			GameCanvas.panel2.setTabGiaoDich(false);
			GameCanvas.panel.tabName[this.type] = new string[][]
			{
				mResources.inventory,
				mResources.item_give
			};
			GameCanvas.panel2.show();
			GameCanvas.panel2.charMenu = cGD;
		}
		if (this.Equals(GameCanvas.panel))
		{
			this.setType(0);
		}
		if (this.currentTabIndex == 0)
		{
			this.setTabInventory(true);
		}
		if (this.currentTabIndex == 1)
		{
			this.setTabGiaoDich(true);
		}
		if (this.currentTabIndex == 2)
		{
			this.setTabGiaoDich(false);
		}
		this.charMenu = cGD;
	}

	// Token: 0x06000689 RID: 1673 RVA: 0x0005E064 File Offset: 0x0005C264
	internal void paintGiaoDich(mGraphics g, bool isMe)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		MyVector myVector = ((!isMe) ? this.vFriendGD : this.vMyGD);
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int num4 = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = 34;
			int num8 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				if (i == this.currentListLength - 1)
				{
					if (isMe)
					{
						g.setColor(15196114);
						g.fillRect(num5, num2, this.wScroll, num4);
						if (!this.isLock)
						{
							if (!this.isFriendLock)
							{
								mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.not_lock_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
							else
							{
								mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.locked_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
							}
						}
						else if (this.isFriendLock)
						{
							g.setColor(15196114);
							g.fillRect(num5, num2, this.wScroll, num4);
							g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
							((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.done, this.xScroll + this.wScroll - 22, num2 + 7, 2);
							mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.locked_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
						}
						else
						{
							mFont.tahoma_7_grey.drawString(g, mResources.opponent + mResources.not_lock_trade, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
						}
					}
				}
				else if (i == this.currentListLength - 2)
				{
					if (isMe)
					{
						g.setColor(15196114);
						g.fillRect(num5, num2, this.wScroll, num4);
						if (!this.isAccept)
						{
							if (!this.isLock)
							{
								g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
								((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.mlock, this.xScroll + this.wScroll - 22, num2 + 7, 2);
								mFont.tahoma_7_grey.drawString(g, mResources.you + mResources.not_lock_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
							}
							else
							{
								g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num2 + 2, StaticObj.TOP_RIGHT);
								((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, mResources.CANCEL, this.xScroll + this.wScroll - 22, num2 + 7, 2);
								mFont.tahoma_7_grey.drawString(g, mResources.you + mResources.locked_trade, this.xScroll + 5, num2 + num4 / 2 - 4, mFont.LEFT);
							}
						}
					}
					else if (!this.isFriendLock)
					{
						mFont.tahoma_7b_dark.drawString(g, mResources.not_lock_trade_upper, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
					}
					else
					{
						mFont.tahoma_7b_dark.drawString(g, mResources.locked_trade_upper, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
					}
				}
				else if (i == this.currentListLength - 3)
				{
					if (this.isLock)
					{
						g.setColor(13748667);
					}
					else
					{
						g.setColor((i != this.selected) ? 15196114 : 16383818);
					}
					g.fillRect(num, num2, num3, num4);
					if (this.isLock)
					{
						g.setColor(13748667);
					}
					else
					{
						g.setColor((i != this.selected) ? 9993045 : 7300181);
					}
					g.fillRect(num5, num6, num7, num8);
					g.drawImage(Panel.imgXu, num5 + num7 / 2, num6 + num8 / 2, 3);
					mFont.tahoma_7_green2.drawString(g, NinjaUtil.getMoneys((long)((!isMe) ? this.friendMoneyGD : this.moneyGD)) + " " + mResources.XU, num + 5, num2 + 11, 0);
					mFont.tahoma_7_green.drawString(g, mResources.money_trade, num + 5, num2, 0);
				}
				else
				{
					if (myVector.size() == 0)
					{
						return;
					}
					if (this.isLock)
					{
						g.setColor(13748667);
					}
					else
					{
						g.setColor((i != this.selected) ? 15196114 : 16383818);
					}
					g.fillRect(num, num2, num3, num4);
					if (this.isLock)
					{
						g.setColor(13748667);
					}
					else
					{
						g.setColor((i != this.selected) ? 9993045 : 9541120);
					}
					Item item = (Item)myVector.elementAt(i);
					if (item != null)
					{
						for (int j = 0; j < item.itemOption.Length; j++)
						{
							if (item.itemOption[j].optionTemplate.id == 72 && item.itemOption[j].param > 0)
							{
								sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[j].param);
								if (Panel.GetColor_ItemBg((int)color_Item_Upgrade) != -1)
								{
									if (this.isLock)
									{
										g.setColor(13748667);
									}
									else
									{
										g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
									}
								}
							}
						}
					}
					g.fillRect(num5, num6, num7, num8);
					if (item != null)
					{
						string text = string.Empty;
						mFont mFont = mFont.tahoma_7_green2;
						if (item.itemOption != null)
						{
							for (int k = 0; k < item.itemOption.Length; k++)
							{
								if (item.itemOption[k].optionTemplate.id == 72)
								{
									text = " [+" + item.itemOption[k].param.ToString() + "]";
								}
								if (item.itemOption[k].optionTemplate.id == 41)
								{
									if (item.itemOption[k].param == 1)
									{
										mFont = Panel.GetFont(0);
									}
									else if (item.itemOption[k].param == 2)
									{
										mFont = Panel.GetFont(2);
									}
									else if (item.itemOption[k].param == 3)
									{
										mFont = Panel.GetFont(8);
									}
									else if (item.itemOption[k].param == 4)
									{
										mFont = Panel.GetFont(7);
									}
								}
							}
						}
						mFont.drawString(g, item.template.name + text, num + 5, num2 + 1, 0);
						string text2 = string.Empty;
						if (item.itemOption != null)
						{
							if (item.itemOption.Length != 0 && item.itemOption[0] != null)
							{
								text2 += item.itemOption[0].getOptionString();
							}
							mFont mFont2 = mFont.tahoma_7_blue;
							if (item.compare < 0 && item.template.type != 5)
							{
								mFont2 = mFont.tahoma_7_red;
							}
							if (item.itemOption.Length > 1)
							{
								for (int l = 1; l < item.itemOption.Length; l++)
								{
									if (item.itemOption[l] != null && item.itemOption[l].optionTemplate.id != 102 && item.itemOption[l].optionTemplate.id != 107)
									{
										text2 = text2 + "," + item.itemOption[l].getOptionString();
									}
								}
							}
							mFont2.drawString(g, text2, num + 5, num2 + 11, mFont.LEFT);
						}
						SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
						if (item.itemOption != null)
						{
							for (int m = 0; m < item.itemOption.Length; m++)
							{
								this.paintOptItem(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num5, num6, num7, num8);
							}
							for (int n = 0; n < item.itemOption.Length; n++)
							{
								this.paintOptSlotItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
							}
						}
						if (item.quantity > 1)
						{
							mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x0600068A RID: 1674 RVA: 0x0005EA44 File Offset: 0x0005CC44
	internal void updateKeyMap()
	{
		if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
		{
			this.yMove -= 5;
			this.cmyMap = this.yMove - (this.yScroll + this.hScroll / 2);
			if (this.yMove < this.yScroll)
			{
				this.yMove = this.yScroll;
			}
		}
		if (GameCanvas.keyHold[(!Main.isPC) ? 8 : 22])
		{
			this.yMove += 5;
			this.cmyMap = this.yMove - (this.yScroll + this.hScroll / 2);
			if (this.yMove > this.yScroll + 200)
			{
				this.yMove = this.yScroll + 200;
			}
		}
		if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
		{
			this.xMove -= 5;
			this.cmxMap = this.xMove - this.wScroll / 2;
			if (this.xMove < 16)
			{
				this.xMove = 16;
			}
		}
		if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
		{
			this.xMove += 5;
			this.cmxMap = this.xMove - this.wScroll / 2;
			if (this.xMove > 250)
			{
				this.xMove = 250;
			}
		}
		if (GameCanvas.isPointerDown)
		{
			this.pointerIsDowning = true;
			if (!this.trans)
			{
				this.pa1 = this.cmxMap;
				this.pa2 = this.cmyMap;
				this.trans = true;
			}
			this.cmxMap = this.pa1 + (GameCanvas.pxLast - GameCanvas.px);
			this.cmyMap = this.pa2 + (GameCanvas.pyLast - GameCanvas.py);
		}
		if (GameCanvas.isPointerJustRelease)
		{
			this.trans = false;
			GameCanvas.pxLast = GameCanvas.px;
			GameCanvas.pyLast = GameCanvas.py;
			this.pX = GameCanvas.pxLast + this.cmxMap;
			this.pY = GameCanvas.pyLast + this.cmyMap;
		}
		if (GameCanvas.isPointerClick)
		{
			this.pointerIsDowning = false;
		}
		if (this.cmxMap < 0)
		{
			this.cmxMap = 0;
		}
		if (this.cmxMap > this.cmxMapLim)
		{
			this.cmxMap = this.cmxMapLim;
		}
		if (this.cmyMap < 0)
		{
			this.cmyMap = 0;
		}
		if (this.cmyMap > this.cmyMapLim)
		{
			this.cmyMap = this.cmyMapLim;
		}
	}

	// Token: 0x0600068B RID: 1675 RVA: 0x0005ECB4 File Offset: 0x0005CEB4
	internal void updateKeyCombine()
	{
		if (this.currentTabIndex == 0)
		{
			this.updateKeyScrollView();
			this.keyTouchCombine = -1;
			if (this.selected == this.vItemCombine.size() && GameCanvas.isPointerClick)
			{
				GameCanvas.isPointerClick = false;
				this.keyTouchCombine = 1;
			}
		}
		if (this.currentTabIndex == 1)
		{
			this.updateKeyScrollView();
		}
	}

	// Token: 0x0600068C RID: 1676 RVA: 0x0005ED0C File Offset: 0x0005CF0C
	internal void updateKeyQuest()
	{
		if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
		{
			this.cmyQuest -= 5;
		}
		if (GameCanvas.keyHold[(!Main.isPC) ? 8 : 22])
		{
			this.cmyQuest += 5;
		}
		if (this.cmyQuest < 0)
		{
			this.cmyQuest = 0;
		}
		int num = this.indexRowMax * 12 - (this.hScroll - 60);
		if (num < 0)
		{
			num = 0;
		}
		if (this.cmyQuest > num)
		{
			this.cmyQuest = num;
		}
		if (this.scroll != null)
		{
			if (!GameCanvas.isTouch)
			{
				this.scroll.cmy = this.cmyQuest;
			}
			this.scroll.updateKey();
		}
		int num2 = this.xScroll + this.wScroll / 2 - 35;
		int num3 = ((GameCanvas.h <= 300) ? 15 : 20);
		int num4 = this.yScroll + this.hScroll - num3 - 15;
		int px = GameCanvas.px;
		int py = GameCanvas.py;
		this.keyTouchMapButton = -1;
		if (Panel.isPaintMap && !GameScr.gI().isMapDocNhan() && px >= num2 && px <= num2 + 70 && py >= num4 && py <= num4 + 30 && (this.scroll == null || !this.scroll.pointerIsDowning))
		{
			this.keyTouchMapButton = 1;
			if (GameCanvas.isPointerJustRelease)
			{
				SoundMn.gI().buttonClick();
				this.waitToPerform = 2;
				GameCanvas.clearAllPointerEvent();
			}
		}
	}

	// Token: 0x0600068D RID: 1677 RVA: 0x0005EE78 File Offset: 0x0005D078
	internal void getCurrClanOtion()
	{
		this.isClanOption = false;
		if (this.type != 0 || this.mainTabName.Length != 5 || this.currentTabIndex != 3)
		{
			return;
		}
		this.isClanOption = false;
		if (this.selected == 0)
		{
			this.currClanOption = new int[this.clansOption.Length];
			for (int i = 0; i < this.currClanOption.Length; i++)
			{
				this.currClanOption[i] = i;
			}
			if (!this.isViewMember)
			{
				this.isClanOption = true;
				return;
			}
		}
		else if (this.selected != 1 && !this.isSearchClan && this.selected > 0)
		{
			this.currClanOption = new int[1];
			for (int j = 0; j < this.currClanOption.Length; j++)
			{
				this.currClanOption[j] = j;
			}
			this.isClanOption = true;
		}
	}

	// Token: 0x0600068E RID: 1678 RVA: 0x0005EF40 File Offset: 0x0005D140
	internal void updateKeyClansOption()
	{
		if (this.currClanOption == null)
		{
			return;
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
		{
			this.currMess = this.getCurrMessage();
			this.cSelected--;
			if (this.selected == 0 && this.cSelected < 0)
			{
				this.cSelected = this.currClanOption.Length - 1;
			}
			if (this.selected > 1 && this.isMessage && this.currMess.option != null && this.cSelected < 0)
			{
				this.cSelected = this.currMess.option.Length - 1;
				return;
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
		{
			this.currMess = this.getCurrMessage();
			this.cSelected++;
			if (this.selected == 0 && this.cSelected > this.currClanOption.Length - 1)
			{
				this.cSelected = 0;
			}
			if (this.selected > 1 && this.isMessage && this.currMess.option != null && this.cSelected > this.currMess.option.Length - 1)
			{
				this.cSelected = 0;
			}
		}
	}

	// Token: 0x0600068F RID: 1679 RVA: 0x0005F07C File Offset: 0x0005D27C
	internal void updateKeyClans()
	{
		this.updateKeyScrollView();
		this.updateKeyClansOption();
	}

	// Token: 0x06000690 RID: 1680 RVA: 0x0005F08C File Offset: 0x0005D28C
	internal void checkOptionSelect()
	{
		try
		{
			if (this.type == 0 && this.currentTabIndex == 3 && this.mainTabName.Length == 5 && this.selected != -1)
			{
				int num = 0;
				if (this.selected == 0)
				{
					num = this.xScroll + this.wScroll / 2 - this.clansOption.Length * this.TAB_W / 2;
					this.cSelected = (GameCanvas.px - num) / this.TAB_W;
				}
				else
				{
					this.currMess = this.getCurrMessage();
					if (this.currMess != null && this.currMess.option != null)
					{
						num = this.xScroll + this.wScroll - 2 - this.currMess.option.Length * 40;
						this.cSelected = (GameCanvas.px - num) / 40;
					}
				}
				if (GameCanvas.px < num)
				{
					this.cSelected = -1;
				}
			}
		}
		catch (Exception ex)
		{
			Res.outz("Throw err " + ex.StackTrace);
		}
	}

	// Token: 0x06000691 RID: 1681 RVA: 0x0005F190 File Offset: 0x0005D390
	public void updateScroolMouse(int a)
	{
		bool flag = false;
		if (GameCanvas.pxMouse > this.X + this.wScroll || GameCanvas.pxMouse < this.X)
		{
			return;
		}
		if (this.indexMouse == -1)
		{
			this.indexMouse = this.selected;
		}
		if (a > 0)
		{
			this.indexMouse -= a;
			flag = true;
		}
		else if (a < 0)
		{
			this.indexMouse += -a;
			flag = true;
		}
		if (this.indexMouse < 0)
		{
			this.indexMouse = 0;
		}
		if (flag)
		{
			this.cmtoY = this.indexMouse * 12;
			if (this.cmtoY > this.cmyLim)
			{
				this.cmtoY = this.cmyLim;
			}
			if (this.cmtoY < 0)
			{
				this.cmtoY = 0;
			}
		}
	}

	// Token: 0x06000692 RID: 1682 RVA: 0x0005F250 File Offset: 0x0005D450
	internal void updateKeyScrollView()
	{
		if (this.currentListLength <= 0)
		{
			return;
		}
		bool flag = false;
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21])
		{
			flag = true;
			if (this.isTabInven() && this.isnewInventory)
			{
				if (this.selected > 0 && this.sellectInventory == 0)
				{
					this.selected--;
				}
			}
			else
			{
				this.selected--;
				if (this.type == 24)
				{
					this.selected -= 2;
					if (this.selected < 0)
					{
						this.selected = 0;
					}
				}
				else if (this.selected < 0)
				{
					if (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.currentTabIndex <= 3 && this.maxPageShop[this.currentTabIndex] > 1)
					{
						InfoDlg.showWait();
						if (this.currPageShop[this.currentTabIndex] <= 0)
						{
							Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.maxPageShop[this.currentTabIndex] - 1, -1);
							return;
						}
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] - 1, -1);
						return;
					}
					else
					{
						this.selected = this.currentListLength - 1;
						if (this.isClanOption)
						{
							this.selected = -1;
						}
						if (this.size_tab > 0)
						{
							this.selected = -1;
						}
					}
				}
				this.lastSelect[this.currentTabIndex] = this.selected;
				this.cSelected = 0;
				this.getCurrClanOtion();
			}
		}
		else if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22])
		{
			flag = true;
			if (this.isTabInven() && this.isnewInventory)
			{
				if (this.selected < 1 && this.sellectInventory == 0)
				{
					this.selected++;
				}
			}
			else
			{
				this.selected++;
				if (this.type == 24)
				{
					this.selected += 2;
					if (this.selected > this.currentListLength - 1)
					{
						this.selected = this.currentListLength - 1;
					}
				}
				else if (this.selected > this.currentListLength - 1)
				{
					if (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.currentTabIndex <= 3 && this.maxPageShop[this.currentTabIndex] > 1)
					{
						InfoDlg.showWait();
						if (this.currPageShop[this.currentTabIndex] >= this.maxPageShop[this.currentTabIndex] - 1)
						{
							Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, 0, -1);
							return;
						}
						Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] + 1, -1);
						return;
					}
					else
					{
						this.selected = 0;
					}
				}
				this.lastSelect[this.currentTabIndex] = this.selected;
				this.cSelected = 0;
				this.getCurrClanOtion();
			}
		}
		if (this.isnewInventory && GameCanvas.keyPressed[5] && this.itemInvenNew != null)
		{
			this.pointerDownTime = 0;
			this.waitToPerform = 2;
		}
		if (flag)
		{
			this.cmtoY = this.selected * this.ITEM_HEIGHT - this.hScroll / 2;
			if (this.cmtoY > this.cmyLim)
			{
				this.cmtoY = this.cmyLim;
			}
			if (this.cmtoY < 0)
			{
				this.cmtoY = 0;
			}
			this.cmy = this.cmtoY;
		}
		if (GameCanvas.isPointerDown)
		{
			this.justRelease = false;
			if (!this.pointerIsDowning && GameCanvas.isPointer(this.xScroll, this.yScroll, this.wScroll, this.hScroll))
			{
				for (int i = 0; i < this.pointerDownLastX.Length; i++)
				{
					this.pointerDownLastX[0] = GameCanvas.py;
				}
				this.pointerDownFirstX = GameCanvas.py;
				this.pointerIsDowning = true;
				this.isDownWhenRunning = this.cmRun != 0;
				this.cmRun = 0;
			}
			else if (this.pointerIsDowning)
			{
				this.pointerDownTime++;
				if (this.pointerDownTime > 5 && this.pointerDownFirstX == GameCanvas.py && !this.isDownWhenRunning)
				{
					this.pointerDownFirstX = -1000;
					this.selected = (this.cmtoY + GameCanvas.py - this.yScroll) / this.ITEM_HEIGHT;
					if (this.selected >= this.currentListLength)
					{
						this.selected = -1;
					}
					this.checkOptionSelect();
				}
				else
				{
					this.indexMouse = -1;
				}
				int num = GameCanvas.py - this.pointerDownLastX[0];
				if (num != 0 && this.selected != -1)
				{
					this.selected = -1;
					this.cSelected = -1;
				}
				for (int j = this.pointerDownLastX.Length - 1; j > 0; j--)
				{
					this.pointerDownLastX[j] = this.pointerDownLastX[j - 1];
				}
				this.pointerDownLastX[0] = GameCanvas.py;
				this.cmtoY -= num;
				if (this.cmtoY < 0)
				{
					this.cmtoY = 0;
				}
				if (this.cmtoY > this.cmyLim)
				{
					this.cmtoY = this.cmyLim;
				}
				if (this.cmy < 0 || this.cmy > this.cmyLim)
				{
					num /= 2;
				}
				this.cmy -= num;
				if (this.cmy < -(GameCanvas.h / 3))
				{
					this.wantUpdateList = true;
				}
				else
				{
					this.wantUpdateList = false;
				}
				if (this.isnewInventory)
				{
					int num2 = GameCanvas.px - this.xScroll;
					this.sellectInventory = (GameCanvas.py - this.yScroll) / 34 * 5 + num2 / 34;
				}
			}
		}
		if (!GameCanvas.isPointerJustRelease || !this.pointerIsDowning)
		{
			return;
		}
		this.justRelease = true;
		int num3 = GameCanvas.py - this.pointerDownLastX[0];
		GameCanvas.isPointerJustRelease = false;
		if (Res.abs(num3) < 20 && Res.abs(GameCanvas.py - this.pointerDownFirstX) < 20 && !this.isDownWhenRunning)
		{
			this.cmRun = 0;
			this.cmtoY = this.cmy;
			this.pointerDownFirstX = -1000;
			this.selected = (this.cmtoY + GameCanvas.py - this.yScroll) / this.ITEM_HEIGHT;
			if (this.selected >= this.currentListLength)
			{
				this.selected = -1;
			}
			this.checkOptionSelect();
			this.pointerDownTime = 0;
			this.waitToPerform = 10;
			if (this.isnewInventory)
			{
				this.waitToPerform = -1;
			}
			SoundMn.gI().panelClick();
		}
		else if (this.selected != -1 && this.pointerDownTime > 5)
		{
			this.pointerDownTime = 0;
			this.waitToPerform = 1;
		}
		else if (this.selected == -1 && !this.isDownWhenRunning)
		{
			if (this.cmy < 0)
			{
				this.cmtoY = 0;
			}
			else if (this.cmy > this.cmyLim)
			{
				this.cmtoY = this.cmyLim;
			}
			else
			{
				int num4 = GameCanvas.py - this.pointerDownLastX[0] + (this.pointerDownLastX[0] - this.pointerDownLastX[1]) + (this.pointerDownLastX[1] - this.pointerDownLastX[2]);
				this.cmRun = -((num4 > 10) ? 10 : ((num4 < -10) ? (-10) : 0)) * 100;
			}
		}
		if ((this.isTabInven() || this.type == 13) && GameCanvas.py < this.yScroll + 21)
		{
			this.selected = 0;
			this.updateKeyInvenTab();
		}
		this.pointerIsDowning = false;
		this.pointerDownTime = 0;
		GameCanvas.isPointerJustRelease = false;
	}

	// Token: 0x06000693 RID: 1683 RVA: 0x0002DFE2 File Offset: 0x0002C1E2
	public string subArray(string[] str)
	{
		return null;
	}

	// Token: 0x06000694 RID: 1684 RVA: 0x0005F9C4 File Offset: 0x0005DBC4
	internal void updateKeyInTabBar()
	{
		if ((this.scroll != null && this.scroll.pointerIsDowning) || this.pointerIsDowning)
		{
			return;
		}
		int num = this.currentTabIndex;
		if (this.isTabInven() && this.isnewInventory)
		{
			if (this.selected == -1)
			{
				if (GameCanvas.keyPressed[6])
				{
					this.currentTabIndex++;
					if (this.currentTabIndex >= this.currentTabName.Length)
					{
						if (GameCanvas.panel2 != null)
						{
							this.currentTabIndex = this.currentTabName.Length - 1;
							GameCanvas.isFocusPanel2 = true;
						}
						else
						{
							this.currentTabIndex = 0;
						}
					}
					this.selected = this.lastSelect[this.currentTabIndex];
					this.lastTabIndex[this.type] = this.currentTabIndex;
				}
				if (GameCanvas.keyPressed[4])
				{
					this.currentTabIndex--;
					if (this.currentTabIndex < 0)
					{
						this.currentTabIndex = this.currentTabName.Length - 1;
					}
					if (GameCanvas.isFocusPanel2)
					{
						GameCanvas.isFocusPanel2 = false;
					}
					this.selected = this.lastSelect[this.currentTabIndex];
					this.lastTabIndex[this.type] = this.currentTabIndex;
				}
			}
			else if (this.selected > 0)
			{
				if (GameCanvas.keyPressed[8])
				{
					if (this.newSelected == 0)
					{
						this.sellectInventory++;
					}
					else
					{
						this.sellectInventory += 5;
					}
				}
				else if (GameCanvas.keyPressed[2])
				{
					if (this.newSelected == 0)
					{
						this.sellectInventory--;
					}
					else
					{
						this.sellectInventory -= 5;
					}
				}
				else if (GameCanvas.keyPressed[4])
				{
					if (this.newSelected == 0)
					{
						this.sellectInventory -= 5;
					}
					else
					{
						this.sellectInventory--;
					}
				}
				else if (GameCanvas.keyPressed[6])
				{
					if (this.newSelected == 0)
					{
						this.sellectInventory += 5;
					}
					else
					{
						this.sellectInventory++;
					}
				}
			}
			int num2 = this.sellectInventory;
			if (this.sellectInventory == this.nTableItem)
			{
				this.sellectInventory = 0;
			}
		}
		else if (!this.IsTabOption())
		{
			if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
			{
				if (this.isTabInven())
				{
					if (this.selected >= 0)
					{
						this.updateKeyInvenTab();
					}
					else
					{
						this.currentTabIndex++;
						if (this.currentTabIndex >= this.currentTabName.Length)
						{
							if (GameCanvas.panel2 != null)
							{
								this.currentTabIndex = this.currentTabName.Length - 1;
								GameCanvas.isFocusPanel2 = true;
							}
							else
							{
								this.currentTabIndex = 0;
							}
						}
						this.selected = this.lastSelect[this.currentTabIndex];
						this.lastTabIndex[this.type] = this.currentTabIndex;
					}
				}
				else
				{
					this.currentTabIndex++;
					if (this.currentTabIndex >= this.currentTabName.Length)
					{
						if (GameCanvas.panel2 != null)
						{
							this.currentTabIndex = this.currentTabName.Length - 1;
							GameCanvas.isFocusPanel2 = true;
						}
						else
						{
							this.currentTabIndex = 0;
						}
					}
					this.selected = this.lastSelect[this.currentTabIndex];
					this.lastTabIndex[this.type] = this.currentTabIndex;
				}
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
			{
				this.currentTabIndex--;
				if (this.currentTabIndex < 0)
				{
					this.currentTabIndex = this.currentTabName.Length - 1;
				}
				if (GameCanvas.isFocusPanel2)
				{
					GameCanvas.isFocusPanel2 = false;
				}
				this.selected = this.lastSelect[this.currentTabIndex];
				this.lastTabIndex[this.type] = this.currentTabIndex;
			}
		}
		this.keyTouchTab = -1;
		for (int i = 0; i < this.currentTabName.Length; i++)
		{
			if (GameCanvas.isPointer(this.startTabPos + i * this.TAB_W, 52, this.TAB_W - 1, 25))
			{
				this.keyTouchTab = i;
				if (GameCanvas.isPointerJustRelease)
				{
					this.currentTabIndex = i;
					this.lastTabIndex[this.type] = i;
					GameCanvas.isPointerJustRelease = false;
					this.selected = this.lastSelect[this.currentTabIndex];
					if (num == this.currentTabIndex && this.cmRun == 0)
					{
						this.cmtoY = 0;
						this.selected = (GameCanvas.isTouch ? (-1) : 0);
						break;
					}
					break;
				}
			}
		}
		if (num == this.currentTabIndex)
		{
			return;
		}
		this.size_tab = 0;
		SoundMn.gI().panelClick();
		int num3 = this.type;
		switch (num3)
		{
		case 0:
			if (this.currentTabIndex == 0)
			{
				this.setTabTask();
			}
			if (this.currentTabIndex == 1)
			{
				this.setTabInventory(true);
			}
			if (this.currentTabIndex == 2)
			{
				this.setTabSkill();
			}
			if (this.currentTabIndex == 3)
			{
				if (this.mainTabName.Length > 4)
				{
					this.setTabClans();
				}
				else
				{
					this.setTabTool();
				}
			}
			if (this.currentTabIndex == 4)
			{
				this.setTabTool();
			}
			break;
		case 1:
			this.setTabShop();
			break;
		case 2:
			if (this.currentTabIndex == 0)
			{
				this.setTabBox();
			}
			if (this.currentTabIndex == 1)
			{
				this.setTabInventory(true);
			}
			break;
		case 3:
			this.setTabZone();
			break;
		default:
			if (num3 != 12)
			{
				if (num3 != 13)
				{
					if (num3 != 21)
					{
						if (num3 == 25)
						{
							this.setTabSpeacialSkill();
						}
					}
					else
					{
						if (this.currentTabIndex == 0)
						{
							this.setTabPetInventory();
						}
						if (this.currentTabIndex == 1)
						{
							this.setTabPetStatus();
						}
						if (this.currentTabIndex == 2)
						{
							this.setTabInventory(true);
						}
					}
				}
				else
				{
					if (this.currentTabIndex == 0)
					{
						if (this.Equals(GameCanvas.panel))
						{
							this.setTabInventory(true);
						}
						else if (this.Equals(GameCanvas.panel2))
						{
							this.setTabGiaoDich(false);
						}
					}
					if (this.currentTabIndex == 1)
					{
						this.setTabGiaoDich(true);
					}
					if (this.currentTabIndex == 2)
					{
						this.setTabGiaoDich(false);
					}
				}
			}
			else
			{
				if (this.currentTabIndex == 0)
				{
					this.setTabCombine();
				}
				if (this.currentTabIndex == 1)
				{
					this.setTabInventory(true);
				}
			}
			break;
		}
		this.selected = this.lastSelect[this.currentTabIndex];
	}

	// Token: 0x06000695 RID: 1685 RVA: 0x0005FFD4 File Offset: 0x0005E1D4
	internal void setTabPetStatus()
	{
		this.currentListLength = this.strStatus.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000696 RID: 1686 RVA: 0x00004887 File Offset: 0x00002A87
	internal void setTabPetSkill()
	{
	}

	// Token: 0x06000697 RID: 1687 RVA: 0x0006008C File Offset: 0x0005E28C
	internal void setTabTool()
	{
		SoundMn.gI().getSoundOption();
		this.currentListLength = Panel.strTool.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000698 RID: 1688 RVA: 0x0006014C File Offset: 0x0005E34C
	public void initTabClans()
	{
		if (this.isSearchClan)
		{
			this.currentListLength = ((this.clans != null) ? (this.clans.Length + 2) : 2);
			this.clanInfo = mResources.clan_list;
		}
		else if (this.isViewMember)
		{
			this.clanReport = string.Empty;
			this.currentListLength = ((this.member != null) ? this.member.size() : this.myMember.size()) + 2;
			this.clanInfo = mResources.member + " " + ((this.currClan == null) ? global::Char.myCharz().clan.name : this.currClan.name);
		}
		else if (this.isMessage)
		{
			this.currentListLength = ClanMessage.vMessage.size() + 2;
			this.clanInfo = mResources.msg;
			this.clanReport = string.Empty;
		}
		if (global::Char.myCharz().clan == null)
		{
			this.clansOption = new string[][]
			{
				mResources.findClan,
				mResources.createClan
			};
		}
		else if (!this.isViewMember)
		{
			if (this.myMember.size() > 1)
			{
				this.clansOption = new string[][]
				{
					mResources.chatClan,
					mResources.request_pea2,
					mResources.memberr
				};
			}
			else
			{
				this.clansOption = new string[][] { mResources.memberr };
			}
		}
		else if (global::Char.myCharz().role > 0)
		{
			this.clansOption = new string[][]
			{
				mResources.msgg,
				mResources.leaveClan
			};
		}
		else if (this.myMember.size() > 1)
		{
			this.clansOption = new string[][]
			{
				mResources.msgg,
				mResources.leaveClan,
				mResources.khau_hieuu,
				mResources.bieu_tuongg
			};
		}
		else
		{
			this.clansOption = new string[][]
			{
				mResources.msgg,
				mResources.khau_hieuu,
				mResources.bieu_tuongg
			};
		}
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000699 RID: 1689 RVA: 0x000603C4 File Offset: 0x0005E5C4
	public void setTabClans()
	{
		GameScr.isNewClanMessage = false;
		this.ITEM_HEIGHT = 24;
		if (this.lastSelect != null && this.lastSelect[3] == 0)
		{
			this.lastSelect[3] = -1;
		}
		this.currentListLength = 2;
		if (global::Char.myCharz().clan != null)
		{
			this.isMessage = true;
			this.isViewMember = false;
			this.isSearchClan = false;
		}
		else
		{
			this.isMessage = false;
			this.isViewMember = false;
			this.isSearchClan = true;
		}
		if (global::Char.myCharz().clan != null)
		{
			this.currentListLength = ClanMessage.vMessage.size() + 2;
		}
		this.initTabClans();
		this.cSelected = -1;
		if (this.chatTField == null)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		if (global::Char.myCharz().clan == null)
		{
			this.clanReport = mResources.findingClan;
			Service.gI().searchClan(string.Empty);
		}
		this.selected = this.lastSelect[this.currentTabIndex];
		if (GameCanvas.isTouch)
		{
			this.selected = -1;
		}
	}

	// Token: 0x0600069A RID: 1690 RVA: 0x00060504 File Offset: 0x0005E704
	public void initLogMessage()
	{
		this.currentListLength = this.logChat.size() + 1;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x0600069B RID: 1691 RVA: 0x000605B6 File Offset: 0x0005E7B6
	internal void setTabMessage()
	{
		this.ITEM_HEIGHT = 24;
		this.initLogMessage();
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
	}

	// Token: 0x0600069C RID: 1692 RVA: 0x000605D8 File Offset: 0x0005E7D8
	public void setTabShop()
	{
		this.ITEM_HEIGHT = 24;
		if (this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null && this.typeShop != 2)
		{
			this.currentListLength = this.checkCurrentListLength(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length);
		}
		else
		{
			this.currentListLength = global::Char.myCharz().arrItemShop[this.currentTabIndex].Length;
		}
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
	}

	// Token: 0x0600069D RID: 1693 RVA: 0x000606E4 File Offset: 0x0005E8E4
	internal void setTabSkill()
	{
		this.ITEM_HEIGHT = 30;
		this.currentListLength = global::Char.myCharz().nClass.skillTemplates.Length + 6;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = this.cmyLim;
		}
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
	}

	// Token: 0x0600069E RID: 1694 RVA: 0x0006079C File Offset: 0x0005E99C
	internal void setTabMapTrans()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = this.mapNames.Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = 0);
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
	}

	// Token: 0x0600069F RID: 1695 RVA: 0x000607FC File Offset: 0x0005E9FC
	internal void setTabZone()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = GameScr.gI().zones.Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = 0);
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
	}

	// Token: 0x060006A0 RID: 1696 RVA: 0x00060860 File Offset: 0x0005EA60
	internal void setTabBox()
	{
		this.currentListLength = this.checkCurrentListLength(global::Char.myCharz().arrItemBox.Length);
		this.ITEM_HEIGHT = 24;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 9;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
	}

	// Token: 0x060006A1 RID: 1697 RVA: 0x00060924 File Offset: 0x0005EB24
	internal void setTabPetInventory()
	{
		this.ITEM_HEIGHT = 30;
		Item[] arrItemBody = global::Char.myPetz().arrItemBody;
		Skill[] arrPetSkill = global::Char.myPetz().arrPetSkill;
		this.currentListLength = arrItemBody.Length + arrPetSkill.Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = 0);
		}
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
	}

	// Token: 0x060006A2 RID: 1698 RVA: 0x000609EC File Offset: 0x0005EBEC
	internal void setTabInventory(bool resetSelect)
	{
		if (this.isnewInventory)
		{
			int num = global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length;
			this.currentListLength = this.checkCurrentListLength(num);
			this.currentListLength = 3;
			this.newSelected = 0;
			this.size_tab = (sbyte)(num / 20 + ((num % 20 > 0) ? 1 : 0));
			Res.outz("sizeTab = " + this.size_tab.ToString());
			return;
		}
		this.currentListLength = this.checkCurrentListLength(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length);
		this.ITEM_HEIGHT = 24;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (resetSelect)
		{
			this.selected = (GameCanvas.isTouch ? (-1) : 0);
		}
	}

	// Token: 0x060006A3 RID: 1699 RVA: 0x00060B28 File Offset: 0x0005ED28
	internal void setTabMap()
	{
		if (!Panel.isPaintMap)
		{
			return;
		}
		if (TileMap.lastPlanetId != TileMap.planetID)
		{
			Res.outz("LOAD TAM HINH");
			Panel.imgMap = GameCanvas.loadImageRMS("/img/map" + TileMap.planetID.ToString() + ".png");
			TileMap.lastPlanetId = TileMap.planetID;
		}
		this.cmxMap = this.getXMap() - this.wScroll / 2;
		this.cmyMap = this.getYMap() + this.yScroll - (this.yScroll + this.hScroll / 2);
		this.pa1 = this.cmxMap;
		this.pa2 = this.cmyMap;
		this.cmxMapLim = 250 - this.wScroll;
		this.cmyMapLim = 220 - this.hScroll;
		if (this.cmxMapLim < 0)
		{
			this.cmxMapLim = 0;
		}
		if (this.cmyMapLim < 0)
		{
			this.cmyMapLim = 0;
		}
		for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
		{
			if (TileMap.mapID == Panel.mapId[(int)TileMap.planetID][i])
			{
				this.xMove = Panel.mapX[(int)TileMap.planetID][i] + this.xScroll;
				this.yMove = Panel.mapY[(int)TileMap.planetID][i] + this.yScroll + 5;
				break;
			}
		}
		this.xMap = this.getXMap() + this.xScroll;
		this.yMap = this.getYMap() + this.yScroll;
		this.xMapTask = this.getXMapTask() + this.xScroll;
		this.yMapTask = this.getYMapTask() + this.yScroll;
		Resources.UnloadUnusedAssets();
		GC.Collect();
	}

	// Token: 0x060006A4 RID: 1700 RVA: 0x00060CCF File Offset: 0x0005EECF
	internal void setTabTask()
	{
		this.cmyQuest = 0;
	}

	// Token: 0x060006A5 RID: 1701 RVA: 0x00060CD8 File Offset: 0x0005EED8
	public void moveCamera()
	{
		if (this.timeShow > 0)
		{
			this.timeShow--;
		}
		if (this.justRelease && this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1)
		{
			if (this.cmy < -50)
			{
				InfoDlg.showWait();
				this.justRelease = false;
				if (this.currPageShop[this.currentTabIndex] <= 0)
				{
					Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.maxPageShop[this.currentTabIndex] - 1, -1);
				}
				else
				{
					Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] - 1, -1);
				}
			}
			else if (this.cmy > this.cmyLim + 50)
			{
				this.justRelease = false;
				InfoDlg.showWait();
				if (this.currPageShop[this.currentTabIndex] >= this.maxPageShop[this.currentTabIndex] - 1)
				{
					Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, 0, -1);
				}
				else
				{
					Service.gI().kigui(4, -1, (sbyte)this.currentTabIndex, this.currPageShop[this.currentTabIndex] + 1, -1);
				}
			}
		}
		if (this.cmx != this.cmtoX && !this.pointerIsDowning)
		{
			this.cmvx = this.cmtoX - this.cmx << 2;
			this.cmdx += this.cmvx;
			this.cmx += this.cmdx >> 3;
			this.cmdx &= 15;
		}
		if (Math2.abs(this.cmtoX - this.cmx) < 10)
		{
			this.cmx = this.cmtoX;
		}
		if (this.isClose)
		{
			this.isClose = false;
			this.cmtoX = this.wScroll;
		}
		if (this.cmtoX >= this.wScroll - 10 && this.cmx >= this.wScroll - 10 && this.position == 0)
		{
			this.isShow = false;
			this.cleanCombine();
			if (this.isChangeZone)
			{
				this.isChangeZone = false;
				if (global::Char.myCharz().cHP > 0 && global::Char.myCharz().statusMe != 14)
				{
					InfoDlg.showWait();
					if (this.type == 3)
					{
						Service.gI().requestChangeZone(this.selected, -1);
					}
					else if (this.type == 14)
					{
						Service.gI().requestMapSelect(this.selected);
					}
				}
			}
			if (this.isSelectPlayerMenu)
			{
				this.isSelectPlayerMenu = false;
				int num = this.vPlayerMenu.size() - this.vPlayerMenu_id.size();
				if (global::Char.myCharz().charFocus != null)
				{
					if (this.selected - num < 0)
					{
						global::Char.myCharz().charFocus.menuSelect = this.selected;
					}
					else
					{
						global::Char.myCharz().charFocus.menuSelect = (int)short.Parse((string)this.vPlayerMenu_id.elementAt(this.selected - num));
					}
				}
				((Command)this.vPlayerMenu.elementAt(this.selected)).performAction();
			}
			this.vPlayerMenu.removeAllElements();
			this.vPlayerMenu_id.removeAllElements();
			this.charMenu = null;
		}
		if (this.cmRun != 0 && !this.pointerIsDowning)
		{
			this.cmtoY += this.cmRun / 100;
			if (this.cmtoY < 0)
			{
				this.cmtoY = 0;
			}
			else if (this.cmtoY > this.cmyLim)
			{
				this.cmtoY = this.cmyLim;
			}
			else
			{
				this.cmy = this.cmtoY;
			}
			this.cmRun = this.cmRun * 9 / 10;
			if (this.cmRun < 100 && this.cmRun > -100)
			{
				this.cmRun = 0;
			}
		}
		if (this.cmy != this.cmtoY && !this.pointerIsDowning)
		{
			this.cmvy = this.cmtoY - this.cmy << 2;
			this.cmdy += this.cmvy;
			this.cmy += this.cmdy >> 4;
			this.cmdy &= 15;
		}
		this.cmyLast[this.currentTabIndex] = this.cmy;
	}

	// Token: 0x060006A6 RID: 1702 RVA: 0x00061124 File Offset: 0x0005F324
	public void paintDetail(mGraphics g)
	{
		if (this.cp == null || this.cp.says == null)
		{
			return;
		}
		this.cp.paint(g);
		int num = this.cp.cx + 13;
		int num2 = this.cp.cy + 11;
		if (this.type == 15)
		{
			num += 5;
			num2 += 26;
		}
		if (this.type == 0 && this.currentTabIndex == 3)
		{
			if (this.isSearchClan)
			{
				num -= 5;
			}
			else if (this.partID != null || this.charInfo != null)
			{
				num = this.cp.cx + 21;
				num2 = this.cp.cy + 40;
			}
		}
		if (this.partID != null)
		{
			Part part = GameScr.parts[this.partID[0]];
			Part part2 = GameScr.parts[this.partID[1]];
			Part part3 = GameScr.parts[this.partID[2]];
			SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 - global::Char.CharInfo[0][0][2] + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
			SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[0][1][0]].id, num + global::Char.CharInfo[0][1][1] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dx, num2 - global::Char.CharInfo[0][1][2] + (int)part2.pi[global::Char.CharInfo[0][1][0]].dy, 0, 0);
			SmallImage.drawSmallImage(g, (int)part3.pi[global::Char.CharInfo[0][2][0]].id, num + global::Char.CharInfo[0][2][1] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dx, num2 - global::Char.CharInfo[0][2][2] + (int)part3.pi[global::Char.CharInfo[0][2][0]].dy, 0, 0);
		}
		else if (this.charInfo != null)
		{
			this.charInfo.paintCharBody(g, num + 5, num2 + 25, 1, 0, true);
		}
		else if (this.idIcon != -1)
		{
			SmallImage.drawSmallImage(g, this.idIcon, this.cp.cx + 8, this.cp.cy + 2, 0, mGraphics.TOP | mGraphics.LEFT);
		}
		if (this.currItem != null && this.currItem.template.type != 5)
		{
			if (this.currItem.compare > 0)
			{
				g.drawImage(Panel.imgUp, num - 7, num2 + 13, 3);
				mFont.tahoma_7b_green.drawString(g, Res.abs(this.currItem.compare).ToString() + string.Empty, num + 1, num2 + 8, 0);
				return;
			}
			if (this.currItem.compare < 0 && this.currItem.compare != -1)
			{
				g.drawImage(Panel.imgDown, num - 7, num2 + 13, 3);
				mFont.tahoma_7b_red.drawString(g, Res.abs(this.currItem.compare).ToString() + string.Empty, num + 1, num2 + 8, 0);
			}
		}
	}

	// Token: 0x060006A7 RID: 1703 RVA: 0x00061484 File Offset: 0x0005F684
	public void paintTop(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		if (this.currentListLength == 0)
		{
			return;
		}
		int num = (this.cmy + this.hScroll) / 24 + 1;
		if (num < this.hScroll / 24 + 1)
		{
			num = this.hScroll / 24 + 1;
		}
		if (num > this.currentListLength)
		{
			num = this.currentListLength;
		}
		int num2 = this.cmy / 24;
		if (num2 >= num)
		{
			num2 = num - 1;
		}
		if (num2 < 0)
		{
			num2 = 0;
		}
		for (int i = num2; i < num; i++)
		{
			int num3 = this.xScroll;
			int num4 = this.yScroll + i * this.ITEM_HEIGHT;
			int num5 = 24;
			int num6 = this.ITEM_HEIGHT - 1;
			int num7 = this.xScroll + num5;
			int num8 = this.yScroll + i * this.ITEM_HEIGHT;
			int num9 = this.wScroll - num5;
			int num10 = this.ITEM_HEIGHT - 1;
			g.setColor((i != this.selected) ? 15196114 : 16383818);
			g.fillRect(num7, num8, num9, num10);
			g.setColor((i != this.selected) ? 9993045 : 9541120);
			g.fillRect(num3, num4, num5, num6);
			TopInfo topInfo = (TopInfo)this.vTop.elementAt(i);
			if (topInfo.headICON != -1)
			{
				SmallImage.drawSmallImage(g, topInfo.headICON, num3, num4, 0, 0);
			}
			else
			{
				Part part = GameScr.parts[topInfo.headID];
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num4 + num10 - 1, 0, mGraphics.BOTTOM | mGraphics.LEFT);
			}
			g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
			if (topInfo.pId != global::Char.myCharz().charID)
			{
				mFont.tahoma_7b_green.drawString(g, topInfo.name, num7 + 5, num8, 0);
			}
			else
			{
				mFont.tahoma_7b_red.drawString(g, topInfo.name, num7 + 5, num8, 0);
			}
			mFont.tahoma_7_blue.drawString(g, topInfo.info, num7 + num9 - 5, num8 + 11, 1);
			mFont.tahoma_7_green2.drawString(g, mResources.rank + ": " + topInfo.rank.ToString() + string.Empty, num7 + 5, num8 + 11, 0);
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006A8 RID: 1704 RVA: 0x00061730 File Offset: 0x0005F930
	public void paint(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY() + mGraphics.addYWhenOpenKeyBoard);
		g.translate(-this.cmx, 0);
		g.translate(this.X, this.Y);
		if (GameCanvas.panel.combineSuccess != -1)
		{
			if (this.Equals(GameCanvas.panel))
			{
				this.paintCombineEff(g);
			}
			return;
		}
		GameCanvas.paintz.paintFrameSimple(this.X, this.Y, this.W, this.H, g);
		try
		{
			this.paintTopInfo(g);
		}
		catch (Exception)
		{
		}
		this.paintBottomMoneyInfo(g);
		this.paintTab(g);
		switch (this.type)
		{
		case 0:
			if (this.currentTabIndex == 0)
			{
				this.paintTask(g);
			}
			if (this.currentTabIndex == 1)
			{
				this.paintInventory(g);
			}
			if (this.currentTabIndex == 2)
			{
				this.paintSkill(g);
			}
			if (this.currentTabIndex == 3)
			{
				if (this.mainTabName.Length == 4)
				{
					this.paintTools(g);
				}
				else
				{
					this.paintClans(g);
				}
			}
			if (this.currentTabIndex == 4)
			{
				this.paintTools(g);
				return;
			}
			break;
		case 1:
			this.paintShop(g);
			return;
		case 2:
			if (this.currentTabIndex == 0)
			{
				this.paintBox(g);
			}
			if (this.currentTabIndex == 1)
			{
				this.paintInventory(g);
				return;
			}
			break;
		case 3:
			this.paintZone(g);
			return;
		case 4:
			this.paintMap(g);
			return;
		case 5:
		case 6:
			break;
		case 7:
			this.paintInventory(g);
			return;
		case 8:
			this.paintLogChat(g);
			return;
		case 9:
			this.paintArchivement(g);
			return;
		case 10:
			this.paintPlayerMenu(g);
			return;
		case 11:
			this.paintFriend(g);
			return;
		case 12:
			if (this.currentTabIndex == 0)
			{
				this.paintCombine(g);
			}
			if (this.currentTabIndex == 1)
			{
				this.paintInventory(g);
				return;
			}
			break;
		case 13:
			if (this.currentTabIndex == 0)
			{
				if (this.Equals(GameCanvas.panel))
				{
					this.paintInventory(g);
				}
				else
				{
					this.paintGiaoDich(g, false);
				}
			}
			if (this.currentTabIndex == 1)
			{
				this.paintGiaoDich(g, true);
			}
			if (this.currentTabIndex == 2)
			{
				this.paintGiaoDich(g, false);
				return;
			}
			break;
		case 14:
			this.paintMapTrans(g);
			return;
		case 15:
			this.paintTop(g);
			return;
		case 16:
			this.paintEnemy(g);
			return;
		case 17:
			this.paintShop(g);
			return;
		case 18:
			this.paintFlagChange(g);
			return;
		case 19:
			this.paintOption(g);
			return;
		case 20:
			this.paintAccount(g);
			return;
		case 21:
			if (this.currentTabIndex == 0)
			{
				this.paintPetInventory(g);
			}
			if (this.currentTabIndex == 1)
			{
				this.paintPetStatus(g);
			}
			if (this.currentTabIndex == 2)
			{
				this.paintInventory(g);
				return;
			}
			break;
		case 22:
			this.paintAuto(g);
			break;
		case 23:
			this.paintGameInfo(g);
			return;
		case 24:
			this.paintGameSubInfo(g);
			return;
		case 25:
			this.paintSpeacialSkill(g);
			return;
		default:
			return;
		}
	}

	// Token: 0x060006A9 RID: 1705 RVA: 0x00061A1C File Offset: 0x0005FC1C
	internal void paintShop(mGraphics g)
	{
		try
		{
			if (this.type == 1 && this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null && this.typeShop != 2)
			{
				this.paintInventory(g);
			}
			else
			{
				g.setColor(16711680);
				g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
				if (this.typeShop == 2 && this.Equals(GameCanvas.panel))
				{
					if (this.currentTabIndex <= 3 && GameCanvas.isTouch)
					{
						if (this.cmy < -50)
						{
							GameCanvas.paintShukiren(this.xScroll + this.wScroll / 2, this.yScroll + 30, g);
						}
						else if (this.cmy < 0)
						{
							mFont.tahoma_7_grey.drawString(g, mResources.getDown, this.xScroll + this.wScroll / 2, this.yScroll + 15, 2);
						}
						else if (this.cmyLim >= 0)
						{
							if (this.cmy > this.cmyLim + 50)
							{
								GameCanvas.paintShukiren(this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 30, g);
							}
							else if (this.cmy > this.cmyLim)
							{
								mFont.tahoma_7_grey.drawString(g, mResources.getUp, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 25, 2);
							}
						}
					}
					if (global::Char.myCharz().arrItemShop[this.currentTabIndex].Length == 0 && this.type != 17)
					{
						mFont.tahoma_7_grey.drawString(g, mResources.notYetSell, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - 10, 2);
						return;
					}
				}
				g.translate(0, -this.cmy);
				Item[] array = global::Char.myCharz().arrItemShop[this.currentTabIndex];
				if (this.typeShop == 2 && (this.currentTabIndex == 4 || this.type == 17))
				{
					array = global::Char.myCharz().arrItemShop[4];
					if (array.Length == 0)
					{
						mFont.tahoma_7_grey.drawString(g, mResources.notYetSell, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - 10, 2);
						return;
					}
				}
				int num = array.Length;
				for (int i = 0; i < num; i++)
				{
					int num2 = this.xScroll + 26;
					int num3 = this.yScroll + i * this.ITEM_HEIGHT;
					int num4 = this.wScroll - 26;
					int num5 = this.ITEM_HEIGHT - 1;
					int num6 = this.xScroll;
					int num7 = this.yScroll + i * this.ITEM_HEIGHT;
					int num8 = 24;
					int num9 = this.ITEM_HEIGHT - 1;
					if (num3 - this.cmy <= this.yScroll + this.hScroll && num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
					{
						g.setColor((i != this.selected) ? 15196114 : 16383818);
						g.fillRect(num2, num3, num4, num5);
						g.setColor((i != this.selected) ? 9993045 : 9541120);
						g.fillRect(num6, num7, num8, num9);
						Item item = array[i];
						if (item != null)
						{
							string text = string.Empty;
							mFont mFont = mFont.tahoma_7_green2;
							if (item.isMe != 0 && this.typeShop == 2 && this.currentTabIndex <= 3 && !this.Equals(GameCanvas.panel2) && item.template.name.Length < 20)
							{
								mFont = mFont.tahoma_7b_green;
							}
							if (item.itemOption != null)
							{
								for (int j = 0; j < item.itemOption.Length; j++)
								{
									if (item.itemOption[j].optionTemplate.id == 72)
									{
										text = " [+" + item.itemOption[j].param.ToString() + "]";
									}
									if (item.itemOption[j].optionTemplate.id == 41)
									{
										if (item.itemOption[j].param == 1)
										{
											mFont = Panel.GetFont(0);
										}
										else if (item.itemOption[j].param == 2)
										{
											mFont = Panel.GetFont(2);
										}
										else if (item.itemOption[j].param == 3)
										{
											mFont = Panel.GetFont(8);
										}
										else if (item.itemOption[j].param == 4)
										{
											mFont = Panel.GetFont(7);
										}
									}
								}
							}
							mFont.drawString(g, item.template.name + text, num2 + 5, num3 + 1, 0);
							string text2 = string.Empty;
							if (item.itemOption != null && item.itemOption.Length >= 1)
							{
								if (item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
								{
									text2 += item.itemOption[0].getOptionString();
								}
								mFont mFont2 = mFont.tahoma_7_blue;
								if (item.compare < 0 && item.template.type != 5)
								{
									mFont2 = mFont.tahoma_7_red;
								}
								if (this.typeShop == 2 && item.itemOption.Length > 1 && item.buyType != -1)
								{
									text2 += string.Empty;
								}
								if (this.typeShop != 2 || (this.typeShop == 2 && item.buyType <= 1))
								{
									mFont2.drawString(g, text2, num2 + 5, num3 + 11, 0);
								}
							}
							if (item.buySpec > 0)
							{
								SmallImage.drawSmallImage(g, (int)item.iconSpec, num2 + num4 - 7, num3 + 9, 0, 3);
								mFont.tahoma_7b_blue.drawString(g, Res.formatNumber((long)item.buySpec), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
							}
							if (item.buyCoin != 0 || item.buyGold != 0)
							{
								if (this.typeShop != 2 && item.powerRequire == 0L)
								{
									if (item.buyCoin > 0 && item.buyGold > 0)
									{
										if (item.buyCoin > 0)
										{
											g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
											mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
										}
										if (item.buyGold > 0)
										{
											g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7 + 11, 3);
											mFont.tahoma_7b_green.drawString(g, Res.formatNumber((long)item.buyGold), num2 + num4 - 15, num3 + 12, mFont.RIGHT);
										}
									}
									else
									{
										if (item.buyCoin > 0)
										{
											g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
											mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
										}
										if (item.buyGold > 0)
										{
											g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7, 3);
											mFont.tahoma_7b_green.drawString(g, Res.formatNumber((long)item.buyGold), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
										}
									}
								}
								if (this.typeShop == 2 && this.currentTabIndex <= 3 && !this.Equals(GameCanvas.panel2))
								{
									if (item.buyCoin > 0 && item.buyGold > 0)
									{
										if (item.buyCoin > 0)
										{
											g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
											((global::Char.myCharz().xu >= (long)item.buyCoin) ? mFont.tahoma_7b_yellow : mFont.tahoma_7b_red).drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
										}
										if (item.buyGold > 0)
										{
											g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7 + 11, 3);
											((global::Char.myCharz().luong >= item.buyGold) ? mFont.tahoma_7b_green : mFont.tahoma_7b_red).drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 12, mFont.RIGHT);
										}
									}
									else
									{
										if (item.buyCoin > 0)
										{
											g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 7, 3);
											((global::Char.myCharz().xu >= (long)item.buyCoin) ? mFont.tahoma_7b_yellow : mFont.tahoma_7b_red).drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
										}
										if (item.buyGold > 0)
										{
											g.drawImage(Panel.imgLuong, num2 + num4 - 7, num3 + 7, 3);
											((global::Char.myCharz().luong >= item.buyGold) ? mFont.tahoma_7b_green : mFont.tahoma_7b_red).drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 1, mFont.RIGHT);
										}
										try
										{
											mFont = mFont.tahoma_7b_green;
											if (!global::Char.myCharz().cName.Equals(item.nameNguoiKyGui))
											{
												mFont = mFont.tahoma_7b_green;
											}
											mFont.drawString(g, item.nameNguoiKyGui, num2 + num4, num3 + 1 + mFont.tahoma_7b_red.getHeight(), mFont.RIGHT);
										}
										catch (Exception)
										{
										}
									}
								}
							}
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
							if (item.quantity > 1)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num6 + num8, num7 + num9 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
							if (item.newItem && GameCanvas.gameTick % 10 > 5)
							{
								g.drawImage(Panel.imgNew, num6 + num8 / 2, num3 + 19, 3);
							}
						}
						if (this.typeShop == 2 && (this.Equals(GameCanvas.panel2) || this.currentTabIndex == 4) && item.buyType != 0)
						{
							if (item.buyType == 1)
							{
								mFont.tahoma_7_green.drawString(g, mResources.dangban, num2 + num4 - 5, num3 + 1, mFont.RIGHT);
								if (item.buyCoin != -1)
								{
									g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 19, 3);
									mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 15, num3 + 13, mFont.RIGHT);
								}
								else if (item.buyGold != -1)
								{
									g.drawImage(Panel.imgLuongKhoa, num2 + num4 - 7, num3 + 17, 3);
									mFont.tahoma_7b_red.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 11, mFont.RIGHT);
								}
							}
							else if (item.buyType == 2)
							{
								mFont.tahoma_7b_blue.drawString(g, mResources.daban, num2 + num4 - 5, num3 + 1, mFont.RIGHT);
								if (item.buyCoin != -1)
								{
									g.drawImage(Panel.imgXu, num2 + num4 - 7, num3 + 17, 3);
									mFont.tahoma_7b_yellow.drawString(g, Res.formatNumber2((long)item.buyCoin), num2 + num4 - 15, num3 + 11, mFont.RIGHT);
								}
								else if (item.buyGold != -1)
								{
									g.drawImage(Panel.imgLuongKhoa, num2 + num4 - 7, num3 + 17, 3);
									mFont.tahoma_7b_red.drawString(g, Res.formatNumber2((long)item.buyGold), num2 + num4 - 15, num3 + 11, mFont.RIGHT);
								}
							}
						}
					}
				}
				this.paintScrollArrow(g);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060006AA RID: 1706 RVA: 0x00004887 File Offset: 0x00002A87
	internal void paintAuto(mGraphics g)
	{
	}

	// Token: 0x060006AB RID: 1707 RVA: 0x00062644 File Offset: 0x00060844
	internal void paintPetStatus(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < this.strStatus.Length; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 1;
			int num4 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, num4);
				mFont.tahoma_7b_dark.drawString(g, this.strStatus[i], this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006AC RID: 1708 RVA: 0x00004887 File Offset: 0x00002A87
	internal void paintPetSkill()
	{
	}

	// Token: 0x060006AD RID: 1709 RVA: 0x00062748 File Offset: 0x00060948
	internal void paintPetInventory(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		Item[] arrItemBody = global::Char.myPetz().arrItemBody;
		Skill[] arrPetSkill = global::Char.myPetz().arrPetSkill;
		for (int i = 0; i < arrItemBody.Length + arrPetSkill.Length; i++)
		{
			bool flag = i < arrItemBody.Length;
			int num = i;
			int num2 = i - arrItemBody.Length;
			int num3 = this.xScroll + 36;
			int num4 = this.yScroll + i * this.ITEM_HEIGHT;
			int num5 = this.wScroll - 36;
			int num6 = this.ITEM_HEIGHT - 1;
			int num7 = this.xScroll;
			int num8 = this.yScroll + i * this.ITEM_HEIGHT;
			int num9 = 34;
			int num10 = this.ITEM_HEIGHT - 1;
			if (num4 - this.cmy <= this.yScroll + this.hScroll && num4 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				Item item = ((!flag) ? null : arrItemBody[num]);
				g.setColor((i == this.selected) ? 16383818 : ((!flag) ? 15723751 : 15196114));
				g.fillRect(num3, num4, num5, num6);
				g.setColor((i == this.selected) ? 9541120 : ((!flag) ? 11837316 : 9993045));
				if (item != null)
				{
					for (int j = 0; j < item.itemOption.Length; j++)
					{
						if (item.itemOption[j].optionTemplate.id == 72 && item.itemOption[j].param > 0)
						{
							sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[j].param);
							if (Panel.GetColor_ItemBg((int)color_Item_Upgrade) != -1)
							{
								g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
							}
						}
					}
				}
				g.fillRect(num7, num8, num9, num10);
				if (item != null && item.isSelect && GameCanvas.panel.type == 12)
				{
					g.setColor((i != this.selected) ? 6047789 : 7040779);
					g.fillRect(num7, num8, num9, num10);
				}
				if (item != null)
				{
					string text = string.Empty;
					mFont mFont = mFont.tahoma_7_green2;
					if (item.itemOption != null)
					{
						for (int k = 0; k < item.itemOption.Length; k++)
						{
							if (item.itemOption[k].optionTemplate.id == 72)
							{
								text = " [+" + item.itemOption[k].param.ToString() + "]";
							}
							if (item.itemOption[k].optionTemplate.id == 41)
							{
								if (item.itemOption[k].param == 1)
								{
									mFont = Panel.GetFont(0);
								}
								else if (item.itemOption[k].param == 2)
								{
									mFont = Panel.GetFont(2);
								}
								else if (item.itemOption[k].param == 3)
								{
									mFont = Panel.GetFont(8);
								}
								else if (item.itemOption[k].param == 4)
								{
									mFont = Panel.GetFont(7);
								}
							}
						}
					}
					mFont.drawString(g, item.template.name + text, num3 + 5, num4 + 1, 0);
					string text2 = string.Empty;
					if (item.itemOption != null)
					{
						if (item.itemOption.Length != 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
						{
							text2 += item.itemOption[0].getOptionString();
						}
						mFont mFont2 = mFont.tahoma_7_blue;
						if (item.compare < 0 && item.template.type != 5)
						{
							mFont2 = mFont.tahoma_7_red;
						}
						if (item.itemOption.Length > 1)
						{
							for (int l = 1; l < 2; l++)
							{
								if (item.itemOption[l] != null && item.itemOption[l].optionTemplate.id != 102 && item.itemOption[l].optionTemplate.id != 107)
								{
									text2 = text2 + "," + item.itemOption[l].getOptionString();
								}
							}
						}
						mFont2.drawString(g, text2, num3 + 5, num4 + 11, mFont.LEFT);
					}
					SmallImage.drawSmallImage(g, (int)item.template.iconID, num7 + num9 / 2, num8 + num10 / 2, 0, 3);
					if (item.itemOption != null)
					{
						for (int m = 0; m < item.itemOption.Length; m++)
						{
							this.paintOptItem(g, item.itemOption[m].optionTemplate.id, item.itemOption[m].param, num7, num8, num9, num10);
						}
						for (int n = 0; n < item.itemOption.Length; n++)
						{
							this.paintOptSlotItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num7, num8, num9, num10);
						}
					}
					if (item.quantity > 1)
					{
						mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num7 + num9, num8 + num10 - mFont.tahoma_7_yellow.getHeight(), 1);
					}
				}
				else if (!flag)
				{
					Skill skill = arrPetSkill[num2];
					g.drawImage(GameScr.imgSkill, num7 + num9 / 2, num8 + num10 / 2, 3);
					if (skill.template != null)
					{
						mFont.tahoma_7_blue.drawString(g, skill.template.name, num3 + 5, num4 + 1, 0);
						mFont.tahoma_7_green2.drawString(g, mResources.level + ": " + skill.point.ToString() + string.Empty, num3 + 5, num4 + 11, 0);
						SmallImage.drawSmallImage(g, skill.template.iconId, num7 + num9 / 2, num8 + num10 / 2, 0, 3);
					}
					else
					{
						mFont.tahoma_7_green2.drawString(g, skill.moreInfo, num3 + 5, num4 + 5, 0);
						SmallImage.drawSmallImage(g, GameScr.efs[98].arrEfInfo[0].idImg, num7 + num9 / 2, num8 + num10 / 2, 0, 3);
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006AE RID: 1710 RVA: 0x00062DE0 File Offset: 0x00060FE0
	internal void paintScrollArrow(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if ((this.cmy > 24 && this.currentListLength > 0) || (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1))
		{
			g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll - 12, this.yScroll + 3, 0);
		}
		if ((this.cmy < this.cmyLim && this.currentListLength > 0) || (this.Equals(GameCanvas.panel) && this.typeShop == 2 && this.maxPageShop[this.currentTabIndex] > 1))
		{
			g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll - 12, this.yScroll + this.hScroll - 8, 0);
		}
	}

	// Token: 0x060006AF RID: 1711 RVA: 0x00062ED4 File Offset: 0x000610D4
	internal void paintTools(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.strTool.Length; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 1;
			int num4 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, num4);
				mFont.tahoma_7b_dark.drawString(g, Panel.strTool[i], this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
				if (Panel.strTool[i].Equals(mResources.gameInfo))
				{
					int j = 0;
					while (j < Panel.vGameInfo.size())
					{
						if (!((GameInfo)Panel.vGameInfo.elementAt(j)).hasRead)
						{
							if (GameCanvas.gameTick % 20 > 10)
							{
								g.drawImage(Panel.imgNew, num + 10, num2 + 10, 3);
								break;
							}
							break;
						}
						else
						{
							j++;
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006B0 RID: 1712 RVA: 0x00063040 File Offset: 0x00061240
	internal void paintGameSubInfo(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.contenInfo.Length; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * 15;
			int num3 = this.wScroll;
			int item_HEIGHT = this.ITEM_HEIGHT;
			if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				mFont.tahoma_7b_dark.drawString(g, Panel.contenInfo[i], this.xScroll + 5, num2 + 6, mFont.LEFT);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006B1 RID: 1713 RVA: 0x00063104 File Offset: 0x00061304
	internal void paintGameInfo(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.vGameInfo.size(); i++)
		{
			GameInfo gameInfo = (GameInfo)Panel.vGameInfo.elementAt(i);
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 1;
			int num4 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, num4);
				mFont.tahoma_7b_dark.drawString(g, gameInfo.main, this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
				if (!gameInfo.hasRead && GameCanvas.gameTick % 20 > 10)
				{
					g.drawImage(Panel.imgNew, num + 10, num2 + 10, 3);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006B2 RID: 1714 RVA: 0x00063244 File Offset: 0x00061444
	internal void paintSkill(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		int num = global::Char.myCharz().nClass.skillTemplates.Length;
		for (int i = 0; i < num + 6; i++)
		{
			int num2 = this.xScroll + 30;
			int num3 = this.yScroll + i * this.ITEM_HEIGHT;
			int num4 = this.wScroll - 30;
			int num5 = this.ITEM_HEIGHT - 1;
			int num6 = this.xScroll;
			int num7 = this.yScroll + i * this.ITEM_HEIGHT;
			int item_HEIGHT = this.ITEM_HEIGHT;
			if (num3 - this.cmy <= this.yScroll + this.hScroll && num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				if (i == 5)
				{
					g.setColor((i != this.selected) ? 16765060 : 16776068);
				}
				g.fillRect(num2, num3, num4, num5);
				g.drawImage(GameScr.imgSkill, num6, num7, 0);
				if (i == 0)
				{
					SmallImage.drawSmallImage(g, 567, num6 + 4, num7 + 4, 0, 0);
					string text = string.Concat(new string[]
					{
						mResources.HP,
						" ",
						mResources.root,
						": ",
						NinjaUtil.getMoneys((long)global::Char.myCharz().cHPGoc)
					});
					mFont.tahoma_7b_blue.drawString(g, text, num2 + 5, num3 + 3, 0);
					mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
					{
						NinjaUtil.getMoneys((long)(global::Char.myCharz().cHPGoc + 1000)),
						" ",
						mResources.potential,
						": ",
						mResources.increase,
						" ",
						global::Char.myCharz().hpFrom1000TiemNang.ToString()
					}), num2 + 5, num3 + 15, 0);
				}
				if (i == 1)
				{
					SmallImage.drawSmallImage(g, 569, num6 + 4, num7 + 4, 0, 0);
					string text2 = string.Concat(new string[]
					{
						mResources.KI,
						" ",
						mResources.root,
						": ",
						NinjaUtil.getMoneys((long)global::Char.myCharz().cMPGoc)
					});
					mFont.tahoma_7b_blue.drawString(g, text2, num2 + 5, num3 + 3, 0);
					mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
					{
						NinjaUtil.getMoneys((long)(global::Char.myCharz().cMPGoc + 1000)),
						" ",
						mResources.potential,
						": ",
						mResources.increase,
						" ",
						global::Char.myCharz().mpFrom1000TiemNang.ToString()
					}), num2 + 5, num3 + 15, 0);
				}
				if (i == 2)
				{
					SmallImage.drawSmallImage(g, 568, num6 + 4, num7 + 4, 0, 0);
					string text3 = string.Concat(new string[]
					{
						mResources.hit_point,
						" ",
						mResources.root,
						": ",
						NinjaUtil.getMoneys((long)global::Char.myCharz().cDamGoc)
					});
					mFont.tahoma_7b_blue.drawString(g, text3, num2 + 5, num3 + 3, 0);
					mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
					{
						NinjaUtil.getMoneys((long)(global::Char.myCharz().cDamGoc * 100)),
						" ",
						mResources.potential,
						": ",
						mResources.increase,
						" ",
						global::Char.myCharz().damFrom1000TiemNang.ToString()
					}), num2 + 5, num3 + 15, 0);
				}
				if (i == 3)
				{
					SmallImage.drawSmallImage(g, 721, num6 + 4, num7 + 4, 0, 0);
					string text4 = string.Concat(new string[]
					{
						mResources.armor,
						" ",
						mResources.root,
						": ",
						NinjaUtil.getMoneys((long)global::Char.myCharz().cDefGoc)
					});
					mFont.tahoma_7b_blue.drawString(g, text4, num2 + 5, num3 + 3, 0);
					mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
					{
						NinjaUtil.getMoneys((long)(500000 + global::Char.myCharz().cDefGoc * 100000)),
						" ",
						mResources.potential,
						": ",
						mResources.increase,
						" ",
						global::Char.myCharz().defFrom1000TiemNang.ToString()
					}), num2 + 5, num3 + 15, 0);
				}
				if (i == 4)
				{
					SmallImage.drawSmallImage(g, 719, num6 + 4, num7 + 4, 0, 0);
					string text5 = string.Concat(new string[]
					{
						mResources.critical,
						" ",
						mResources.root,
						": ",
						global::Char.myCharz().cCriticalGoc.ToString(),
						"%"
					});
					int num8 = global::Char.myCharz().cCriticalGoc;
					if (num8 > Panel.t_tiemnang.Length - 1)
					{
						num8 = Panel.t_tiemnang.Length - 1;
					}
					long num9 = Panel.t_tiemnang[num8];
					mFont.tahoma_7b_blue.drawString(g, text5, num2 + 5, num3 + 3, 0);
					long num10 = num9;
					mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
					{
						Res.formatNumber2(num10),
						" ",
						mResources.potential,
						": ",
						mResources.increase,
						" ",
						global::Char.myCharz().criticalFrom1000Tiemnang.ToString()
					}), num2 + 5, num3 + 15, 0);
				}
				if (i == 5)
				{
					if (Panel.specialInfo != null)
					{
						SmallImage.drawSmallImage(g, (int)Panel.spearcialImage, num6 + 4, num7 + 4, 0, 0);
						string[] array = mFont.tahoma_7.splitFontArray(Panel.specialInfo, 120);
						for (int j = 0; j < array.Length; j++)
						{
							mFont.tahoma_7_green2.drawString(g, array[j], num2 + 5, num3 + 3 + j * 12, 0);
						}
					}
					else
					{
						mFont.tahoma_7_green2.drawString(g, string.Empty, num2 + 5, num3 + 9, 0);
					}
				}
				if (i >= 6)
				{
					SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[i - 6];
					SmallImage.drawSmallImage(g, skillTemplate.iconId, num6 + 4, num7 + 4, 0, 0);
					Skill skill = global::Char.myCharz().getSkill(skillTemplate);
					if (skill != null)
					{
						mFont.tahoma_7b_blue.drawString(g, skillTemplate.name, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_blue.drawString(g, mResources.level + ": " + skill.point.ToString(), num2 + num4 - 5, num3 + 3, mFont.RIGHT);
						if (skill.point == skillTemplate.maxPoint)
						{
							mFont.tahoma_7_green2.drawString(g, mResources.max_level_reach, num2 + 5, num3 + 15, 0);
						}
						else if (skill.template.isSkillSpec())
						{
							string text6 = mResources.proficiency + ": ";
							int num11 = mFont.tahoma_7_green2.getWidthExactOf(text6) + num2 + 5;
							int num12 = num3 + 15;
							mFont.tahoma_7_green2.drawString(g, text6, num2 + 5, num12, 0);
							mFont.tahoma_7_green2.drawString(g, "(" + skill.strCurExp() + ")", num2 + num4 - 5, num12, mFont.RIGHT);
							num12 += 4;
							g.setColor(7169134);
							g.fillRect(num11, num12, 50, 5);
							int num13 = (int)(skill.curExp * 50 / 1000);
							g.setColor(11992374);
							g.fillRect(num11, num12, num13, 5);
							if (skill.curExp < 1000)
							{
							}
						}
						else
						{
							Skill skill2 = skillTemplate.skills[skill.point];
							mFont.tahoma_7_green2.drawString(g, string.Concat(new string[]
							{
								mResources.level,
								" ",
								(skill.point + 1).ToString(),
								" ",
								mResources.need,
								" ",
								Res.formatNumber2(skill2.powRequire),
								" ",
								mResources.potential
							}), num2 + 5, num3 + 15, 0);
						}
					}
					else
					{
						Skill skill3 = skillTemplate.skills[0];
						string text7 = string.Concat(new string[]
						{
							mResources.need_upper,
							" ",
							Res.formatNumber2(skill3.powRequire),
							" ",
							mResources.potential_to_learn
						});
						if (skill3.template.id == 24 || skill3.template.id == 25 || skill3.template.id == 26)
						{
							text7 = string.Concat(new string[]
							{
								mResources.need_upper,
								" ",
								Res.formatNumber2(skill3.powRequire),
								" ",
								mResources.potential_to_learn_tuyetKi
							});
						}
						mFont.tahoma_7b_green.drawString(g, skillTemplate.name, num2 + 5, num3 + 3, 0);
						mFont.tahoma_7_green2.drawString(g, text7, num2 + 5, num3 + 15, 0);
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006B3 RID: 1715 RVA: 0x00063BA8 File Offset: 0x00061DA8
	internal void paintMapTrans(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < this.mapNames.Length; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll;
			int num4 = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll;
			int item_HEIGHT = this.ITEM_HEIGHT;
			int item_HEIGHT2 = this.ITEM_HEIGHT;
			if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(this.xScroll, num2, this.wScroll, num4);
				mFont.tahoma_7b_blue.drawString(g, this.mapNames[i], 5, num2 + 1, 0);
				mFont.tahoma_7_grey.drawString(g, this.planetNames[i], 5, num2 + 11, 0);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006B4 RID: 1716 RVA: 0x00063CE0 File Offset: 0x00061EE0
	internal void paintZone(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		int[] zones = GameScr.gI().zones;
		int[] pts = GameScr.gI().pts;
		for (int i = 0; i < pts.Length; i++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int num4 = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = 34;
			int num8 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, num4);
				g.setColor(this.zoneColor[pts[i]]);
				g.fillRect(num5, num6, num7, num8);
				if (zones[i] != -1)
				{
					if (pts[i] != 1)
					{
						mFont.tahoma_7_yellow.drawString(g, zones[i].ToString() + string.Empty, num5 + num7 / 2, num2 + 6, mFont.CENTER);
					}
					else
					{
						mFont.tahoma_7_grey.drawString(g, zones[i].ToString() + string.Empty, num5 + num7 / 2, num2 + 6, mFont.CENTER);
					}
					mFont.tahoma_7_green2.drawString(g, GameScr.gI().numPlayer[i].ToString() + "/" + GameScr.gI().maxPlayer[i].ToString(), num + 5, num2 + 6, 0);
				}
				if (GameScr.gI().rankName1[i] != null)
				{
					mFont.tahoma_7_grey.drawString(g, GameScr.gI().rankName1[i] + "(Top " + GameScr.gI().rank1[i].ToString() + ")", num + num3 - 2, num2 + 1, mFont.RIGHT);
					mFont.tahoma_7_grey.drawString(g, GameScr.gI().rankName2[i] + "(Top " + GameScr.gI().rank2[i].ToString() + ")", num + num3 - 2, num2 + 11, mFont.RIGHT);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006B5 RID: 1717 RVA: 0x00063F88 File Offset: 0x00062188
	internal void paintSpeacialSkill(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		if (this.currentListLength == 0)
		{
			return;
		}
		int num = (this.cmy + this.hScroll) / 24 + 1;
		if (num < this.hScroll / 24 + 1)
		{
			num = this.hScroll / 24 + 1;
		}
		if (num > this.currentListLength)
		{
			num = this.currentListLength;
		}
		int num2 = this.cmy / 24;
		if (num2 >= num)
		{
			num2 = num - 1;
		}
		if (num2 < 0)
		{
			num2 = 0;
		}
		for (int i = num2; i < num; i++)
		{
			int num3 = this.xScroll;
			int num4 = this.yScroll + i * this.ITEM_HEIGHT;
			int num5 = 24;
			int num6 = this.ITEM_HEIGHT - 1;
			int num7 = this.xScroll + num5;
			int num8 = this.yScroll + i * this.ITEM_HEIGHT;
			int num9 = this.wScroll - num5;
			int num10 = this.ITEM_HEIGHT - 1;
			g.setColor((i != this.selected) ? 15196114 : 16383818);
			g.fillRect(num7, num8, num9, num10);
			g.setColor((i != this.selected) ? 9993045 : 9541120);
			g.fillRect(num3, num4, num5, num6);
			SmallImage.drawSmallImage(g, (int)global::Char.myCharz().imgSpeacialSkill[this.currentTabIndex][i], num3 + num5 / 2, num4 + num6 / 2, 0, 3);
			string[] array = mFont.tahoma_7_grey.splitFontArray(global::Char.myCharz().infoSpeacialSkill[this.currentTabIndex][i], 140);
			for (int j = 0; j < array.Length; j++)
			{
				mFont.tahoma_7_grey.drawString(g, array[j], num7 + 5, num8 + 1 + j * 11, 0);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006B6 RID: 1718 RVA: 0x00064168 File Offset: 0x00062368
	internal void paintBox(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		try
		{
			Item[] arrItemBox = global::Char.myCharz().arrItemBox;
			this.currentListLength = this.checkCurrentListLength(arrItemBox.Length);
			int num = arrItemBox.Length / 20 + ((arrItemBox.Length % 20 > 0) ? 1 : 0);
			this.TAB_W_NEW = this.wScroll / num;
			for (int i = 0; i < this.currentListLength; i++)
			{
				int num2 = this.xScroll + 36;
				int num3 = this.yScroll + i * this.ITEM_HEIGHT;
				int num4 = this.wScroll - 36;
				int num5 = this.ITEM_HEIGHT - 1;
				int num6 = this.xScroll;
				int num7 = this.yScroll + i * this.ITEM_HEIGHT;
				int num8 = 34;
				int num9 = this.ITEM_HEIGHT - 1;
				if (num3 - this.cmy <= this.yScroll + this.hScroll && num3 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					if (i == 0)
					{
						for (int j = 0; j < num; j++)
						{
							int num10 = ((j == this.newSelected && this.selected == 0) ? ((GameCanvas.gameTick % 10 < 7) ? (-1) : 0) : 0);
							g.setColor((j != this.newSelected) ? 15723751 : 16383818);
							g.fillRect(this.xScroll + j * this.TAB_W_NEW, num3 + 9 + num10, this.TAB_W_NEW - 1, 14);
							mFont.tahoma_7_grey.drawString(g, string.Empty + j.ToString(), this.xScroll + j * this.TAB_W_NEW + this.TAB_W_NEW / 2, this.yScroll + 11 + num10, mFont.CENTER);
						}
					}
					else
					{
						g.setColor((i != this.selected) ? 15196114 : 16383818);
						g.fillRect(num2, num3, num4, num5);
						g.setColor((i != this.selected) ? 9993045 : 9541120);
						Item item = arrItemBox[this.GetInventorySelect_body(i, this.newSelected)];
						if (item != null)
						{
							for (int k = 0; k < item.itemOption.Length; k++)
							{
								if (item.itemOption[k].optionTemplate.id == 72 && item.itemOption[k].param > 0)
								{
									sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[k].param);
									if (Panel.GetColor_ItemBg((int)color_Item_Upgrade) != -1)
									{
										g.setColor((i != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
									}
								}
							}
						}
						g.fillRect(num6, num7, num8, num9);
						if (item != null)
						{
							string text = string.Empty;
							mFont mFont = mFont.tahoma_7_green2;
							if (item.itemOption != null)
							{
								for (int l = 0; l < item.itemOption.Length; l++)
								{
									if (item.itemOption[l].optionTemplate.id == 72)
									{
										text = " [+" + item.itemOption[l].getOptionString() + "]";
									}
									if (item.itemOption[l].optionTemplate.id == 41)
									{
										if (item.itemOption[l].param == 1)
										{
											mFont = Panel.GetFont(0);
										}
										else if (item.itemOption[l].param == 2)
										{
											mFont = Panel.GetFont(2);
										}
										else if (item.itemOption[l].param == 3)
										{
											mFont = Panel.GetFont(8);
										}
										else if (item.itemOption[l].param == 4)
										{
											mFont = Panel.GetFont(7);
										}
									}
								}
							}
							mFont.drawString(g, item.template.name + text, num2 + 5, num3 + 1, 0);
							string text2 = string.Empty;
							if (item.itemOption != null)
							{
								if (item.itemOption.Length != 0 && item.itemOption[0] != null)
								{
									text2 += item.itemOption[0].getOptionString();
								}
								mFont mFont2 = mFont.tahoma_7_blue;
								if (item.compare < 0 && item.template.type != 5)
								{
									mFont2 = mFont.tahoma_7_red;
								}
								if (item.itemOption.Length > 1)
								{
									for (int m = 1; m < item.itemOption.Length; m++)
									{
										if (item.itemOption[m] != null && item.itemOption[m].optionTemplate.id != 102 && item.itemOption[m].optionTemplate.id != 107)
										{
											text2 = text2 + "," + item.itemOption[m].getOptionString();
										}
									}
								}
								mFont2.drawString(g, text2, num2 + 5, num3 + 11, mFont.LEFT);
							}
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num6 + num8 / 2, num7 + num9 / 2, 0, 3);
							if (item.itemOption != null)
							{
								for (int n = 0; n < item.itemOption.Length; n++)
								{
									this.paintOptItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num6, num7, num8, num9);
								}
								for (int num11 = 0; num11 < item.itemOption.Length; num11++)
								{
									this.paintOptSlotItem(g, item.itemOption[num11].optionTemplate.id, item.itemOption[num11].param, num6, num7, num8, num9);
								}
							}
							if (item.quantity > 1)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num6 + num8, num7 + num9 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
						}
					}
				}
			}
		}
		catch (Exception)
		{
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006B7 RID: 1719 RVA: 0x00064790 File Offset: 0x00062990
	public Member getCurrMember()
	{
		if (this.selected < 2)
		{
			return null;
		}
		if (this.selected > ((this.member == null) ? this.myMember.size() : this.member.size()) + 1)
		{
			return null;
		}
		if (this.member != null)
		{
			return (Member)this.member.elementAt(this.selected - 2);
		}
		return (Member)this.myMember.elementAt(this.selected - 2);
	}

	// Token: 0x060006B8 RID: 1720 RVA: 0x0006480D File Offset: 0x00062A0D
	public ClanMessage getCurrMessage()
	{
		if (this.selected < 2)
		{
			return null;
		}
		if (this.selected > ClanMessage.vMessage.size() + 1)
		{
			return null;
		}
		return (ClanMessage)ClanMessage.vMessage.elementAt(this.selected - 2);
	}

	// Token: 0x060006B9 RID: 1721 RVA: 0x00064847 File Offset: 0x00062A47
	public Clan getCurrClan()
	{
		if (this.selected < 2)
		{
			return null;
		}
		if (this.selected > this.clans.Length + 1)
		{
			return null;
		}
		return this.clans[this.selected - 2];
	}

	// Token: 0x060006BA RID: 1722 RVA: 0x00064878 File Offset: 0x00062A78
	internal void paintLogChat(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		if (this.logChat.size() == 0)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_msg, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2 + 24, 2);
		}
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = 24;
			int num4 = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll + num3;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = this.wScroll - num3;
			int num8 = this.ITEM_HEIGHT - 1;
			if (i == 0)
			{
				g.setColor(15196114);
				g.fillRect(num, num6, this.wScroll, num8);
				g.drawImage((i != this.selected) ? GameScr.imgLbtn2 : GameScr.imgLbtnFocus2, this.xScroll + this.wScroll - 5, num6 + 2, StaticObj.TOP_RIGHT);
				((i != this.selected) ? mFont.tahoma_7b_dark : mFont.tahoma_7b_green2).drawString(g, (!this.isViewChatServer) ? mResources.on : mResources.off, this.xScroll + this.wScroll - 22, num6 + 7, 2);
				mFont.tahoma_7_grey.drawString(g, (!this.isViewChatServer) ? mResources.onPlease : mResources.offPlease, this.xScroll + 5, num6 + num8 / 2 - 4, mFont.LEFT);
			}
			else
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num5, num6, num7, num8);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num, num2, num3, num4);
				InfoItem infoItem = (InfoItem)this.logChat.elementAt(i - 1);
				if (infoItem.charInfo.headICON != -1)
				{
					SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
				}
				else
				{
					Part part = GameScr.parts[infoItem.charInfo.head];
					SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
				}
				g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
				mFont tahoma_7b_dark = mFont.tahoma_7b_dark;
				mFont.tahoma_7b_green2.drawString(g, infoItem.charInfo.cName, num5 + 5, num6, 0);
				if (!infoItem.isChatServer)
				{
					mFont.tahoma_7_blue.drawString(g, Res.split(infoItem.s, "|", 0)[2], num5 + 5, num6 + 11, 0);
				}
				else
				{
					mFont.tahoma_7_red.drawString(g, Res.split(infoItem.s, "|", 0)[2], num5 + 5, num6 + 11, 0);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006BB RID: 1723 RVA: 0x00064BE4 File Offset: 0x00062DE4
	internal void paintFlagChange(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll + 26;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 26;
			int num4 = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = 24;
			int num8 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, num4);
				g.setColor((i != this.selected) ? 9993045 : 9541120);
				g.fillRect(num5, num6, num7, num8);
				Item item = (Item)this.vFlag.elementAt(i);
				if (item != null)
				{
					mFont.tahoma_7_green2.drawString(g, item.template.name, num + 5, num2 + 1, 0);
					string text = string.Empty;
					if (item.itemOption != null && item.itemOption.Length >= 1)
					{
						if (item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
						{
							text += item.itemOption[0].getOptionString();
						}
						mFont.tahoma_7_blue.drawString(g, text, num + 5, num2 + 11, 0);
						SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006BC RID: 1724 RVA: 0x00064DFC File Offset: 0x00062FFC
	internal void paintEnemy(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		if (this.currentListLength == 0)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_enemy, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
			return;
		}
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = 24;
			int num4 = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll + num3;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = this.wScroll - num3;
			int num8 = this.ITEM_HEIGHT - 1;
			g.setColor((i != this.selected) ? 15196114 : 16383818);
			g.fillRect(num5, num6, num7, num8);
			g.setColor((i != this.selected) ? 9993045 : 9541120);
			g.fillRect(num, num2, num3, num4);
			InfoItem infoItem = (InfoItem)this.vEnemy.elementAt(i);
			if (infoItem.charInfo.headICON != -1)
			{
				SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
			}
			else
			{
				Part part = GameScr.parts[infoItem.charInfo.head];
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 + 3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
			}
			g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
			if (infoItem.isOnline)
			{
				mFont.tahoma_7b_green.drawString(g, infoItem.charInfo.cName, num5 + 5, num6, 0);
				mFont.tahoma_7_blue.drawString(g, infoItem.s, num5 + 5, num6 + 11, 0);
			}
			else
			{
				mFont.tahoma_7_grey.drawString(g, infoItem.charInfo.cName, num5 + 5, num6, 0);
				mFont.tahoma_7_grey.drawString(g, infoItem.s, num5 + 5, num6 + 11, 0);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006BD RID: 1725 RVA: 0x0006508C File Offset: 0x0006328C
	internal void paintFriend(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		if (this.currentListLength == 0)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_friend, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
			return;
		}
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = 24;
			int num4 = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll + num3;
			int num6 = this.yScroll + i * this.ITEM_HEIGHT;
			int num7 = this.wScroll - num3;
			int num8 = this.ITEM_HEIGHT - 1;
			g.setColor((i != this.selected) ? 15196114 : 16383818);
			g.fillRect(num5, num6, num7, num8);
			g.setColor((i != this.selected) ? 9993045 : 9541120);
			g.fillRect(num, num2, num3, num4);
			InfoItem infoItem = (InfoItem)this.vFriend.elementAt(i);
			if (infoItem.charInfo.headICON != -1)
			{
				SmallImage.drawSmallImage(g, infoItem.charInfo.headICON, num, num2, 0, 0);
			}
			else
			{
				Part part = GameScr.parts[infoItem.charInfo.head];
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num2 + 3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
			}
			g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
			if (infoItem.isOnline)
			{
				mFont.tahoma_7b_green.drawString(g, infoItem.charInfo.cName, num5 + 5, num6, 0);
				mFont.tahoma_7_blue.drawString(g, infoItem.s, num5 + 5, num6 + 11, 0);
			}
			else
			{
				mFont.tahoma_7_grey.drawString(g, infoItem.charInfo.cName, num5 + 5, num6, 0);
				mFont.tahoma_7_grey.drawString(g, infoItem.s, num5 + 5, num6 + 11, 0);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006BE RID: 1726 RVA: 0x0006531C File Offset: 0x0006351C
	public void paintPlayerMenu(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < this.vPlayerMenu.size(); i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 1;
			int num4 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				Command command = (Command)this.vPlayerMenu.elementAt(i);
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, num4);
				if (command.caption2.Equals(string.Empty))
				{
					mFont.tahoma_7b_dark.drawString(g, command.caption, this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
				}
				else
				{
					mFont.tahoma_7b_dark.drawString(g, command.caption, this.xScroll + this.wScroll / 2, num2 + 1, mFont.CENTER);
					mFont.tahoma_7b_dark.drawString(g, command.caption2, this.xScroll + this.wScroll / 2, num2 + 11, mFont.CENTER);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006BF RID: 1727 RVA: 0x000654A0 File Offset: 0x000636A0
	internal void paintClans(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(-this.cmx, -this.cmy);
		g.setColor(0);
		int num = this.xScroll + this.wScroll / 2 - this.clansOption.Length * this.TAB_W / 2;
		if (this.currentListLength == 2)
		{
			mFont.tahoma_7_green2.drawString(g, this.clanReport, this.xScroll + this.wScroll / 2, this.yScroll + 24 + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
			if (this.isMessage && this.myMember.size() == 1)
			{
				for (int i = 0; i < mResources.clanEmpty.Length; i++)
				{
					mFont.tahoma_7b_dark.drawString(g, mResources.clanEmpty[i], this.xScroll + this.wScroll / 2, this.yScroll + 24 + this.hScroll / 2 - mResources.clanEmpty.Length * 12 / 2 + i * 12, mFont.CENTER);
				}
			}
		}
		if (this.isMessage)
		{
			this.currentListLength = ClanMessage.vMessage.size() + 2;
		}
		for (int j = 0; j < this.currentListLength; j++)
		{
			int num2 = this.xScroll;
			int num3 = this.yScroll + j * this.ITEM_HEIGHT;
			int num4 = 24;
			int num5 = this.ITEM_HEIGHT - 1;
			int num6 = this.xScroll + num4;
			int num7 = this.yScroll + j * this.ITEM_HEIGHT;
			int num8 = this.wScroll - num4;
			int num9 = this.ITEM_HEIGHT - 1;
			if (num7 - this.cmy <= this.yScroll + this.hScroll && num7 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				if (j == 0)
				{
					for (int k = 0; k < this.clansOption.Length; k++)
					{
						g.setColor((k != this.cSelected || j != this.selected) ? 15723751 : 16383818);
						g.fillRect(num + k * this.TAB_W, num7, this.TAB_W - 1, 23);
						for (int l = 0; l < this.clansOption[k].Length; l++)
						{
							mFont.tahoma_7_grey.drawString(g, this.clansOption[k][l], num + k * this.TAB_W + this.TAB_W / 2, this.yScroll + l * 11, mFont.CENTER);
						}
					}
				}
				else if (j == 1)
				{
					g.setColor((j != this.selected) ? 15196114 : 16383818);
					g.fillRect(this.xScroll, num7, this.wScroll, num9);
					if (this.clanInfo != null)
					{
						mFont.tahoma_7b_dark.drawString(g, this.clanInfo, this.xScroll + this.wScroll / 2, num7 + 6, mFont.CENTER);
					}
				}
				else if (this.isSearchClan)
				{
					if (this.clans != null && this.clans.Length != 0)
					{
						g.setColor((j != this.selected) ? 15196114 : 16383818);
						g.fillRect(num6, num7, num8, num9);
						g.setColor((j != this.selected) ? 9993045 : 9541120);
						g.fillRect(num2, num3, num4, num5);
						if (ClanImage.isExistClanImage(this.clans[j - 2].imgID))
						{
							if (ClanImage.getClanImage((short)this.clans[j - 2].imgID).idImage != null)
							{
								SmallImage.drawSmallImage(g, (int)ClanImage.getClanImage((short)this.clans[j - 2].imgID).idImage[0], num2 + num4 / 2, num3 + num5 / 2, 0, StaticObj.VCENTER_HCENTER);
							}
						}
						else
						{
							ClanImage clanImage = new ClanImage();
							clanImage.ID = this.clans[j - 2].imgID;
							if (!ClanImage.isExistClanImage(clanImage.ID))
							{
								ClanImage.addClanImage(clanImage);
							}
						}
						string text = ((this.clans[j - 2].name.Length <= 23) ? this.clans[j - 2].name : (this.clans[j - 2].name.Substring(0, 23) + "..."));
						mFont.tahoma_7b_green2.drawString(g, text, num6 + 5, num7, 0);
						g.setClip(num6, num7, num8 - 10, num9);
						mFont.tahoma_7_blue.drawString(g, this.clans[j - 2].slogan, num6 + 5, num7 + 11, 0);
						g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
						mFont.tahoma_7_green2.drawString(g, this.clans[j - 2].currMember.ToString() + "/" + this.clans[j - 2].maxMember.ToString(), num6 + num8 - 5, num7, mFont.RIGHT);
					}
				}
				else if (this.isViewMember)
				{
					g.setColor((j != this.selected) ? 15196114 : 16383818);
					g.fillRect(num6, num7, num8, num9);
					g.setColor((j != this.selected) ? 9993045 : 9541120);
					g.fillRect(num2, num3, num4, num5);
					Member member = ((this.member == null) ? ((Member)this.myMember.elementAt(j - 2)) : ((Member)this.member.elementAt(j - 2)));
					if (member.headICON != -1)
					{
						SmallImage.drawSmallImage(g, (int)member.headICON, num2, num3, 0, 0);
					}
					else
					{
						Part part = GameScr.parts[(int)member.head];
						SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, num2 + (int)part.pi[global::Char.CharInfo[0][0][0]].dx, num3 + 3 + (int)part.pi[global::Char.CharInfo[0][0][0]].dy, 0, 0);
					}
					g.setClip(this.xScroll, this.yScroll + this.cmy, this.wScroll, this.hScroll);
					mFont mFont = mFont.tahoma_7b_dark;
					if (member.role == 0)
					{
						mFont = mFont.tahoma_7b_red;
					}
					else if (member.role == 1)
					{
						mFont = mFont.tahoma_7b_green;
					}
					else if (member.role == 2)
					{
						mFont = mFont.tahoma_7b_green2;
					}
					mFont.drawString(g, member.name, num6 + 5, num7, 0);
					mFont.tahoma_7_blue.drawString(g, mResources.power + ": " + member.powerPoint, num6 + 5, num7 + 11, 0);
					SmallImage.drawSmallImage(g, 7223, num6 + num8 - 7, num7 + 12, 0, 3);
					mFont.tahoma_7_blue.drawString(g, string.Empty + member.clanPoint.ToString(), num6 + num8 - 15, num7 + 6, mFont.RIGHT);
				}
				else if (this.isMessage && ClanMessage.vMessage.size() != 0)
				{
					ClanMessage clanMessage = (ClanMessage)ClanMessage.vMessage.elementAt(j - 2);
					g.setColor((j != this.selected || clanMessage.option != null) ? 15196114 : 16383818);
					g.fillRect(num2, num3, num8 + num4, num9);
					clanMessage.paint(g, num2, num3);
					if (clanMessage.option != null)
					{
						int num10 = this.xScroll + this.wScroll - 2 - clanMessage.option.Length * 40;
						for (int m = 0; m < clanMessage.option.Length; m++)
						{
							if (m == this.cSelected && j == this.selected)
							{
								g.drawImage(GameScr.imgLbtnFocus2, num10 + m * 40 + 20, num7 + num9 / 2, StaticObj.VCENTER_HCENTER);
								mFont.tahoma_7b_green2.drawString(g, clanMessage.option[m], num10 + m * 40 + 20, num7 + 6, mFont.CENTER);
							}
							else
							{
								g.drawImage(GameScr.imgLbtn2, num10 + m * 40 + 20, num7 + num9 / 2, StaticObj.VCENTER_HCENTER);
								mFont.tahoma_7b_dark.drawString(g, clanMessage.option[m], num10 + m * 40 + 20, num7 + 6, mFont.CENTER);
							}
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006C0 RID: 1728 RVA: 0x00065D38 File Offset: 0x00063F38
	internal void paintArchivement(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		g.setColor(0);
		if (this.currentListLength == 0)
		{
			mFont.tahoma_7_green2.drawString(g, mResources.no_mission, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - mFont.tahoma_7.getHeight() / 2, 2);
			return;
		}
		if (global::Char.myCharz().arrArchive == null || global::Char.myCharz().arrArchive.Length != this.currentListLength)
		{
			return;
		}
		for (int i = 0; i < this.currentListLength; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll;
			int num4 = this.ITEM_HEIGHT - 1;
			Archivement archivement = global::Char.myCharz().arrArchive[i];
			g.setColor((i != this.selected || ((archivement.isRecieve || archivement.isFinish) && (!archivement.isRecieve || !archivement.isFinish))) ? 15196114 : 16383818);
			g.fillRect(num, num2, num3, num4);
			if (archivement != null)
			{
				if (!archivement.isFinish)
				{
					mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
					mFont.tahoma_7_green.drawString(g, archivement.money.ToString() + " " + mResources.RUBY, num + num3 - 5, num2, mFont.RIGHT);
					mFont.tahoma_7_red.drawString(g, archivement.info2, num + 5, num2 + 11, 0);
				}
				else if (archivement.isFinish && !archivement.isRecieve)
				{
					mFont.tahoma_7.drawString(g, archivement.info1, num + 5, num2, 0);
					mFont.tahoma_7_blue.drawString(g, mResources.reward_mission + archivement.money.ToString() + " " + mResources.RUBY, num + 5, num2 + 11, 0);
					if (i == this.selected)
					{
						mFont.tahoma_7b_green2.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
						mFont.tahoma_7b_dark.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
					}
					else
					{
						g.drawImage(GameScr.imgLbtn2, num + num3 - 20, num2 + num4 / 2, StaticObj.VCENTER_HCENTER);
						mFont.tahoma_7b_dark.drawString(g, mResources.receive_upper, num + num3 - 20, num2 + 6, mFont.CENTER);
					}
				}
				else if (archivement.isFinish && archivement.isRecieve)
				{
					mFont.tahoma_7_green.drawString(g, archivement.info1, num + 5, num2, 0);
					mFont.tahoma_7_green.drawString(g, archivement.info2, num + 5, num2 + 11, 0);
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006C1 RID: 1729 RVA: 0x0006601C File Offset: 0x0006421C
	internal void paintCombine(mGraphics g)
	{
		g.setColor(16711680);
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		if (this.vItemCombine.size() == 0)
		{
			if (this.combineInfo != null)
			{
				for (int i = 0; i < this.combineInfo.Length; i++)
				{
					mFont.tahoma_7b_dark.drawString(g, this.combineInfo[i], this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll / 2 - this.combineInfo.Length * 14 / 2 + i * 14 + 5, 2);
				}
			}
			return;
		}
		for (int j = 0; j < this.vItemCombine.size() + 1; j++)
		{
			int num = this.xScroll + 36;
			int num2 = this.yScroll + j * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 36;
			int num4 = this.ITEM_HEIGHT - 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + j * this.ITEM_HEIGHT;
			int num7 = 34;
			int num8 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				if (j == this.vItemCombine.size())
				{
					if (this.vItemCombine.size() > 0)
					{
						if (!GameCanvas.isTouch && j == this.selected)
						{
							g.setColor(16383818);
							g.fillRect(num5, num2, this.wScroll, num4 + 2);
						}
						if ((j == this.selected && this.keyTouchCombine == 1) || (!GameCanvas.isTouch && j == this.selected))
						{
							g.drawImage(GameScr.imgLbtnFocus, this.xScroll + this.wScroll / 2, num2 + num4 / 2 + 1, StaticObj.VCENTER_HCENTER);
							mFont.tahoma_7b_green2.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
						}
						else
						{
							g.drawImage(GameScr.imgLbtn, this.xScroll + this.wScroll / 2, num2 + num4 / 2 + 1, StaticObj.VCENTER_HCENTER);
							mFont.tahoma_7b_dark.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, num2 + num4 / 2 - 4, mFont.CENTER);
						}
					}
				}
				else
				{
					g.setColor((j != this.selected) ? 15196114 : 16383818);
					g.fillRect(num, num2, num3, num4);
					g.setColor((j != this.selected) ? 9993045 : 9541120);
					Item item = (Item)this.vItemCombine.elementAt(j);
					if (item != null)
					{
						for (int k = 0; k < item.itemOption.Length; k++)
						{
							if (item.itemOption[k].optionTemplate.id == 72 && item.itemOption[k].param > 0)
							{
								sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(item.itemOption[k].param);
								if (Panel.GetColor_ItemBg((int)color_Item_Upgrade) != -1)
								{
									g.setColor((j != this.selected) ? Panel.GetColor_ItemBg((int)color_Item_Upgrade) : Panel.GetColor_ItemBg((int)color_Item_Upgrade));
								}
							}
						}
					}
					g.fillRect(num5, num6, num7, num8);
					if (item != null)
					{
						string text = string.Empty;
						mFont mFont = mFont.tahoma_7_green2;
						if (item.itemOption != null)
						{
							for (int l = 0; l < item.itemOption.Length; l++)
							{
								if (item.itemOption[l].optionTemplate.id == 72)
								{
									text = " [+" + item.itemOption[l].param.ToString() + "]";
								}
								if (item.itemOption[l].optionTemplate.id == 41)
								{
									if (item.itemOption[l].param == 1)
									{
										mFont = Panel.GetFont(0);
									}
									else if (item.itemOption[l].param == 2)
									{
										mFont = Panel.GetFont(2);
									}
									else if (item.itemOption[l].param == 3)
									{
										mFont = Panel.GetFont(8);
									}
									else if (item.itemOption[l].param == 4)
									{
										mFont = Panel.GetFont(7);
									}
								}
							}
						}
						mFont.drawString(g, item.template.name + text, num + 5, num2 + 1, 0);
						string text2 = string.Empty;
						if (item.itemOption != null)
						{
							if (item.itemOption.Length != 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
							{
								text2 += item.itemOption[0].getOptionString();
							}
							mFont mFont2 = mFont.tahoma_7_blue;
							if (item.compare < 0 && item.template.type != 5)
							{
								mFont2 = mFont.tahoma_7_red;
							}
							if (item.itemOption.Length > 1)
							{
								for (int m = 1; m < item.itemOption.Length; m++)
								{
									if (item.itemOption[m] != null && item.itemOption[m].optionTemplate.id != 102 && item.itemOption[m].optionTemplate.id != 107)
									{
										text2 = text2 + "," + item.itemOption[m].getOptionString();
									}
								}
							}
							mFont2.drawString(g, text2, num + 5, num2 + 11, mFont.LEFT);
						}
						SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
						if (item.itemOption != null)
						{
							for (int n = 0; n < item.itemOption.Length; n++)
							{
								this.paintOptItem(g, item.itemOption[n].optionTemplate.id, item.itemOption[n].param, num5, num6, num7, num8);
							}
							for (int num9 = 0; num9 < item.itemOption.Length; num9++)
							{
								this.paintOptSlotItem(g, item.itemOption[num9].optionTemplate.id, item.itemOption[num9].param, num5, num6, num7, num8);
							}
						}
						if (item.quantity > 1)
						{
							mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num5 + num7, num6 + num8 - mFont.tahoma_7_yellow.getHeight(), 1);
						}
					}
				}
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006C2 RID: 1730 RVA: 0x000666DC File Offset: 0x000648DC
	internal void paintInventory(mGraphics g)
	{
		bool flag = true;
		if (flag && this.isnewInventory)
		{
			Item[] arrItemBody = global::Char.myCharz().arrItemBody;
			Item[] arrItemBag = global::Char.myCharz().arrItemBag;
			g.setColor(16711680);
			int num = arrItemBody.Length + arrItemBag.Length;
			int num2 = num / 20 + ((num % 20 > 0) ? 1 : 0) + 1;
			int num3 = 0;
			this.TAB_W_NEW = this.wScroll / num2;
			for (int i = num3; i < num2; i++)
			{
				int num4 = ((i == this.newSelected && this.selected == 0) ? ((GameCanvas.gameTick % 10 < 7) ? (-1) : 0) : 0);
				g.setColor((i != this.newSelected) ? 15723751 : 16383818);
				g.fillRect(this.xScroll + i * this.TAB_W_NEW, 89 + num4 - 10, this.TAB_W_NEW - 1, 21);
				if (i == this.newSelected)
				{
					g.setColor(13524492);
					g.fillRect(this.xScroll + i * this.TAB_W_NEW, 89 + num4 - 10 + 21 - 3, this.TAB_W_NEW - 1, 3);
				}
				mFont.tahoma_7_grey.drawString(g, string.Empty + (i + 1).ToString(), this.xScroll + i * this.TAB_W_NEW + this.TAB_W_NEW / 2, 91 + num4 - 10, mFont.CENTER);
			}
			num3 = 1;
			int num5 = this.xScroll;
			int num6 = this.yScroll + num3 * this.ITEM_HEIGHT;
			int num7 = 34;
			int num8 = this.ITEM_HEIGHT - 1;
			for (int j = 0; j < 4; j++)
			{
				num5 = this.xScroll;
				num6 = this.yScroll + (j + num3) * this.ITEM_HEIGHT;
				bool flag2 = true;
				int k = 0;
				while (k < 5)
				{
					if (this.newSelected > 0)
					{
						int num9 = (this.newSelected - 1) * 20;
						if (j * 5 + k + num9 >= arrItemBag.Length)
						{
							break;
						}
						Item item = arrItemBag[j * 5 + k + num9];
						num5 = this.xScroll + num7 * k;
						int num10 = this.sellectInventory % 5;
						int num11 = this.sellectInventory / 5;
						if (this.newSelected > 0)
						{
							g.setColor(15196114);
						}
						else
						{
							g.setColor(9993045);
						}
						g.drawRect(num5, num6, num7, num8);
						if (j == num11 && k == num10 && this.selected > 0)
						{
							g.setColor(16383818);
							this.itemInvenNew = item;
						}
						g.fillRect(num5 + 2, num6 + 2, num7 - 3, num8 - 3);
						if (item != null)
						{
							int num12 = num5 + Panel.imgNew.getWidth() / 2;
							int num13 = num6;
							int num14 = 34;
							int num15 = this.ITEM_HEIGHT - 1;
							SmallImage.drawSmallImage(g, (int)item.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
							if (item.quantity > 1)
							{
								mFont.tahoma_7_yellow.drawString(g, string.Empty + item.quantity.ToString(), num5, num6 - mFont.tahoma_7_yellow.getHeight(), 1);
							}
							if (item.newItem && GameCanvas.gameTick % 10 > 5)
							{
								g.drawImage(Panel.imgNew, num12, num13, 3);
							}
							for (int l = 0; l < item.itemOption.Length; l++)
							{
								this.paintOptSlotItem(g, item.itemOption[l].optionTemplate.id, item.itemOption[l].param, num12, num13, num14, num15);
							}
						}
						if (!flag2)
						{
							break;
						}
						k++;
					}
					else
					{
						if (j * 5 + k < arrItemBody.Length)
						{
							Item item = arrItemBody[j * 5 + k];
							break;
						}
						break;
					}
				}
			}
			num3 = ((this.newSelected != 0) ? 5 : 3);
			int num16 = this.yScroll + num3 * this.ITEM_HEIGHT + 5;
			if (this.newSelected == 0)
			{
			}
			num5 = this.xScroll;
			num6 = this.yScroll + num3 * this.ITEM_HEIGHT;
			num7 = 34;
			num8 = this.ITEM_HEIGHT - 1;
			if (this.newSelected == 0)
			{
				g.setColor(15196114);
				num3 = 1;
				this.nTableItem = 10;
				if (this.eBanner != null)
				{
					this.eBanner.paint(g);
					this.eBanner.x = num5 + 34 + 34;
					this.eBanner.y = num6 + num8 - 25;
				}
				for (int m = 0; m < 10; m++)
				{
					Item item2 = arrItemBody[m];
					if (m < 5)
					{
						num5 = this.xScroll;
						num6 = this.yScroll + (m + num3) * this.ITEM_HEIGHT;
					}
					else
					{
						int num17 = 5;
						num5 = this.xScroll + 4 * num7;
						num6 = this.yScroll + (m - num17 + num3) * this.ITEM_HEIGHT;
					}
					g.setColor(15196114);
					g.drawRect(num5, num6, num7, num8);
					if (this.sellectInventory == m)
					{
						this.itemInvenNew = item2;
						g.setColor(16383818);
					}
					else
					{
						g.setColor(9993045);
					}
					g.fillRect(num5 + 2, num6 + 2, num7 - 3, num8 - 3);
					if (item2 == null)
					{
						Panel.screenTab6.drawFrame(m, num5 + num7 / 2 - 8, num6 + num8 / 2 - 8, 0, mGraphics.TOP | mGraphics.LEFT, g);
					}
					if (item2 != null)
					{
						SmallImage.drawSmallImage(g, (int)item2.template.iconID, num5 + num7 / 2, num6 + num8 / 2, 0, 3);
						if (item2.quantity > 1)
						{
							mFont.tahoma_7_yellow.drawString(g, string.Empty + item2.quantity.ToString(), num5 + 4 * num7, num6 - mFont.tahoma_7_yellow.getHeight(), 1);
						}
					}
				}
				num3 = 1;
				num5 = this.xScroll + 34;
				num6 = this.yScroll + num3 * this.ITEM_HEIGHT;
				num8 = 4 * (this.ITEM_HEIGHT - 1);
				global::Char.myCharz().paintCharBody(g, num5 + 34 + 17, num6 + num8 - 25, 1, 0, true);
				num3 = 3;
				int num18 = 2;
				num5 = this.xScroll + 34;
				num6 = this.yScroll + (1 + num3) * this.ITEM_HEIGHT - 1;
				num7 = 102;
				num8 = this.ITEM_HEIGHT * num18;
				g.setColor(15196114);
				g.drawRect(num5, num6, num7, num8);
				g.setColor(9993045);
				g.fillRect(num5 + 1, num6 + 1, num7 - 2, num8 - 2);
				this.paintItemBodyBagInfo(g, num5 + 3, num6 - 2);
				int num19 = ((this.newSelected != 0) ? 5 : 6);
				num16 = this.yScroll + num19 * this.ITEM_HEIGHT;
				g.setColor(15196114);
				if (this.newSelected == 0)
				{
					num18 = 1;
				}
				g.drawRect(this.xScroll, num16, this.wScroll, this.ITEM_HEIGHT * num18);
				g.setColor(16777215);
				g.fillRect(this.xScroll + 1, num16 + 1, this.wScroll - 2, this.ITEM_HEIGHT * num18 - 2);
			}
			if (this.itemInvenNew != null && this.itemInvenNew.itemOption != null)
			{
				string text = string.Empty;
				mFont mFont = mFont.tahoma_7_green2;
				if (this.itemInvenNew.itemOption != null)
				{
					for (int n = 0; n < this.itemInvenNew.itemOption.Length; n++)
					{
						if (this.itemInvenNew.itemOption[n].optionTemplate.id == 72)
						{
							text = " [+" + this.itemInvenNew.itemOption[n].param.ToString() + "]";
						}
						if (this.itemInvenNew.itemOption[n].optionTemplate.id == 41)
						{
							if (this.itemInvenNew.itemOption[n].param == 1)
							{
								mFont = Panel.GetFont(0);
							}
							else if (this.itemInvenNew.itemOption[n].param == 2)
							{
								mFont = Panel.GetFont(2);
							}
							else if (this.itemInvenNew.itemOption[n].param == 3)
							{
								mFont = Panel.GetFont(8);
							}
							else if (this.itemInvenNew.itemOption[n].param == 4)
							{
								mFont = Panel.GetFont(7);
							}
						}
					}
				}
				mFont.drawString(g, this.itemInvenNew.template.name + text, this.xScroll + 5, num16 + 1, 0);
				string text2 = string.Empty;
				if (this.itemInvenNew.itemOption != null)
				{
					if (this.itemInvenNew.itemOption.Length != 0 && this.itemInvenNew.itemOption[0] != null && this.itemInvenNew.itemOption[0].optionTemplate.id != 102 && this.itemInvenNew.itemOption[0].optionTemplate.id != 107)
					{
						text2 += this.itemInvenNew.itemOption[0].getOptionString();
					}
					mFont mFont2 = mFont.tahoma_7_blue;
					if (this.itemInvenNew.compare < 0 && this.itemInvenNew.template.type != 5)
					{
						mFont2 = mFont.tahoma_7_red;
					}
					if (this.itemInvenNew.itemOption.Length > 1)
					{
						for (int num20 = 1; num20 < 2; num20++)
						{
							if (this.itemInvenNew.itemOption[num20] != null && this.itemInvenNew.itemOption[num20].optionTemplate.id != 102 && this.itemInvenNew.itemOption[num20].optionTemplate.id != 107)
							{
								text2 = text2 + "," + this.itemInvenNew.itemOption[num20].getOptionString();
							}
						}
					}
					try
					{
						if (mFont2.getWidth(text2) > this.wScroll)
						{
							text2 = mFont2.splitFontArray(text2, this.wScroll)[0];
						}
					}
					catch (Exception)
					{
					}
					mFont2.drawString(g, text2, this.xScroll + 5, num16 + 11, mFont.LEFT);
				}
			}
		}
		if (flag && this.isnewInventory)
		{
			return;
		}
		g.setColor(16711680);
		Item[] arrItemBody2 = global::Char.myCharz().arrItemBody;
		Item[] arrItemBag2 = global::Char.myCharz().arrItemBag;
		this.currentListLength = this.checkCurrentListLength(arrItemBody2.Length + arrItemBag2.Length);
		int num21 = (arrItemBody2.Length + arrItemBag2.Length) / 20 + (((arrItemBody2.Length + arrItemBag2.Length) % 20 > 0) ? 1 : 0);
		this.TAB_W_NEW = this.wScroll / num21;
		for (int num22 = 0; num22 < num21; num22++)
		{
			int num23 = ((num22 == this.newSelected && this.selected == 0) ? ((GameCanvas.gameTick % 10 < 7) ? (-1) : 0) : 0);
			g.setColor((num22 != this.newSelected) ? 15723751 : 16383818);
			g.fillRect(this.xScroll + num22 * this.TAB_W_NEW, 89 + num23 - 10, this.TAB_W_NEW - 1, 21);
			if (num22 == this.newSelected)
			{
				g.setColor(13524492);
				g.fillRect(this.xScroll + num22 * this.TAB_W_NEW, 89 + num23 - 10 + 21 - 3, this.TAB_W_NEW - 1, 3);
			}
			mFont.tahoma_7_grey.drawString(g, string.Empty + (num22 + 1).ToString(), this.xScroll + num22 * this.TAB_W_NEW + this.TAB_W_NEW / 2, 91 + num23 - 10, mFont.CENTER);
		}
		g.setClip(this.xScroll, this.yScroll + 21, this.wScroll, this.hScroll - 21);
		g.translate(0, -this.cmy);
		try
		{
			for (int num24 = 1; num24 < this.currentListLength; num24++)
			{
				int num25 = this.xScroll + 36;
				int num26 = this.yScroll + num24 * this.ITEM_HEIGHT;
				int num27 = this.wScroll - 36;
				int num28 = this.ITEM_HEIGHT - 1;
				int num29 = this.xScroll;
				int num30 = this.yScroll + num24 * this.ITEM_HEIGHT;
				int num31 = 34;
				int num32 = this.ITEM_HEIGHT - 1;
				if (num26 - this.cmy <= this.yScroll + this.hScroll && num26 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
				{
					bool inventorySelect_isbody = this.GetInventorySelect_isbody(num24, this.newSelected, global::Char.myCharz().arrItemBody);
					int inventorySelect_body = this.GetInventorySelect_body(num24, this.newSelected);
					int inventorySelect_bag = this.GetInventorySelect_bag(num24, this.newSelected, global::Char.myCharz().arrItemBody);
					g.setColor((num24 == this.selected) ? 16383818 : ((!inventorySelect_isbody) ? 15723751 : 15196114));
					g.fillRect(num25, num26, num27, num28);
					g.setColor((num24 == this.selected) ? 9541120 : ((!inventorySelect_isbody) ? 11837316 : 9993045));
					Item item3 = ((!inventorySelect_isbody) ? arrItemBag2[inventorySelect_bag] : arrItemBody2[inventorySelect_body]);
					if (item3 != null)
					{
						for (int num33 = 0; num33 < item3.itemOption.Length; num33++)
						{
							if (item3.itemOption[num33].optionTemplate.id == 72 && item3.itemOption[num33].param > 0)
							{
								byte b = (byte)Panel.GetColor_Item_Upgrade(item3.itemOption[num33].param);
								if (Panel.GetColor_ItemBg((int)b) != -1)
								{
									g.setColor((num24 != this.selected) ? Panel.GetColor_ItemBg((int)b) : Panel.GetColor_ItemBg((int)b));
								}
							}
						}
					}
					g.fillRect(num29, num30, num31, num32);
					if (item3 != null && item3.isSelect && GameCanvas.panel.type == 12)
					{
						g.setColor((num24 != this.selected) ? 6047789 : 7040779);
						g.fillRect(num29, num30, num31, num32);
					}
					if (item3 != null)
					{
						string text3 = string.Empty;
						mFont mFont3 = mFont.tahoma_7_green2;
						if (item3.itemOption != null)
						{
							for (int num34 = 0; num34 < item3.itemOption.Length; num34++)
							{
								if (item3.itemOption[num34].optionTemplate.id == 72)
								{
									text3 = " [+" + item3.itemOption[num34].param.ToString() + "]";
								}
								if (item3.itemOption[num34].optionTemplate.id == 41)
								{
									if (item3.itemOption[num34].param == 1)
									{
										mFont3 = Panel.GetFont(0);
									}
									else if (item3.itemOption[num34].param == 2)
									{
										mFont3 = Panel.GetFont(2);
									}
									else if (item3.itemOption[num34].param == 3)
									{
										mFont3 = Panel.GetFont(8);
									}
									else if (item3.itemOption[num34].param == 4)
									{
										mFont3 = Panel.GetFont(7);
									}
								}
							}
						}
						mFont3.drawString(g, item3.template.name + text3, num25 + 5, num26 + 1, 0);
						string text4 = string.Empty;
						if (item3.itemOption != null)
						{
							if (item3.itemOption.Length != 0 && item3.itemOption[0] != null && item3.itemOption[0].optionTemplate.id != 102 && item3.itemOption[0].optionTemplate.id != 107)
							{
								text4 += item3.itemOption[0].getOptionString();
							}
							mFont mFont4 = mFont.tahoma_7_blue;
							if (item3.compare < 0 && item3.template.type != 5)
							{
								mFont4 = mFont.tahoma_7_red;
							}
							if (item3.itemOption.Length > 1)
							{
								for (int num35 = 1; num35 < 2; num35++)
								{
									if (item3.itemOption[num35] != null && item3.itemOption[num35].optionTemplate.id != 102 && item3.itemOption[num35].optionTemplate.id != 107)
									{
										text4 = text4 + "," + item3.itemOption[num35].getOptionString();
									}
								}
							}
							mFont4.drawString(g, text4, num25 + 5, num26 + 11, mFont.LEFT);
						}
						SmallImage.drawSmallImage(g, (int)item3.template.iconID, num29 + num31 / 2, num30 + num32 / 2, 0, 3);
						if (item3.itemOption != null)
						{
							for (int num36 = 0; num36 < item3.itemOption.Length; num36++)
							{
								this.paintOptItem(g, item3.itemOption[num36].optionTemplate.id, item3.itemOption[num36].param, num29, num30, num31, num32);
							}
							for (int num37 = 0; num37 < item3.itemOption.Length; num37++)
							{
								this.paintOptSlotItem(g, item3.itemOption[num37].optionTemplate.id, item3.itemOption[num37].param, num29, num30, num31, num32);
							}
						}
						if (item3.quantity > 1)
						{
							mFont.tahoma_7_yellow.drawString(g, string.Empty + item3.quantity.ToString(), num29 + num31, num30 + num32 - mFont.tahoma_7_yellow.getHeight(), 1);
						}
					}
				}
			}
		}
		catch (Exception)
		{
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x060006C3 RID: 1731 RVA: 0x0006787C File Offset: 0x00065A7C
	internal void paintTab(mGraphics g)
	{
		if (this.type == 23 || this.type == 24)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.gameInfo, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 20)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.account, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 22)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.autoFunction, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 19)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.option, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 18)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.change_flag, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 13 && this.Equals(GameCanvas.panel2))
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.item_receive2, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 12 && GameCanvas.panel2 != null)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.UPGRADE, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 11)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.friend, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 16)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.enemy, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 15)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, this.topName, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 2 && GameCanvas.panel2 != null)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.chest, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 9)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.achievement_mission, this.xScroll + this.wScroll / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 3)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.select_zone, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 14)
		{
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			mFont.tahoma_7b_dark.drawString(g, mResources.select_map, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
			return;
		}
		if (this.type == 4)
		{
			mFont.tahoma_7b_dark.drawString(g, mResources.map, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			return;
		}
		if (this.type == 7)
		{
			mFont.tahoma_7b_dark.drawString(g, mResources.trangbi, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			return;
		}
		if (this.type == 17)
		{
			mFont.tahoma_7b_dark.drawString(g, mResources.kigui, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			return;
		}
		if (this.type == 8)
		{
			mFont.tahoma_7b_dark.drawString(g, mResources.msg, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			return;
		}
		if (this.type == 10)
		{
			mFont.tahoma_7b_dark.drawString(g, mResources.wat_do_u_want, this.startTabPos + this.TAB_W / 2, 59, mFont.CENTER);
			g.setColor(13524492);
			g.fillRect(this.X + 1, 78, this.W - 2, 1);
			return;
		}
		if (this.currentTabIndex == 3 && this.mainTabName.Length != 4)
		{
			g.translate(-this.cmx, 0);
		}
		for (int i = 0; i < this.currentTabName.Length; i++)
		{
			g.setColor((i != this.currentTabIndex) ? 16773296 : 6805896);
			PopUp.paintPopUp(g, this.startTabPos + i * this.TAB_W, 52, this.TAB_W - 1, 25, (i == this.currentTabIndex) ? 1 : 0, true);
			if (i == this.keyTouchTab)
			{
				g.drawImage(ItemMap.imageFlare, this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 62, 3);
			}
			mFont mFont = ((i != this.currentTabIndex) ? mFont.tahoma_7_grey : mFont.tahoma_7_green2);
			if (!this.currentTabName[i][1].Equals(string.Empty))
			{
				mFont.drawString(g, this.currentTabName[i][0], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 53, mFont.CENTER);
				mFont.drawString(g, this.currentTabName[i][1], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 64, mFont.CENTER);
			}
			else
			{
				mFont.drawString(g, this.currentTabName[i][0], this.startTabPos + i * this.TAB_W + this.TAB_W / 2, 59, mFont.CENTER);
			}
			if (this.type == 0 && this.currentTabName.Length == 5 && GameScr.isNewClanMessage && GameCanvas.gameTick % 4 == 0)
			{
				g.drawImage(ItemMap.imageFlare, this.startTabPos + 3 * this.TAB_W + this.TAB_W / 2, 77, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
		}
		g.setColor(13524492);
		g.fillRect(this.X + 1, 78, this.W - 2, 1);
	}

	// Token: 0x060006C4 RID: 1732 RVA: 0x000680E4 File Offset: 0x000662E4
	internal void paintBottomMoneyInfo(mGraphics g)
	{
		if (this.type != 13 || (this.currentTabIndex != 2 && !this.Equals(GameCanvas.panel2)))
		{
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			g.setColor(11837316);
			g.fillRect(this.X + 1, this.H - 15, this.W - 2, 14);
			g.setColor(13524492);
			g.fillRect(this.X + 1, this.H - 15, this.W - 2, 1);
			g.drawImage(Panel.imgXu, this.X + 11, this.H - 7, 3);
			g.drawImage(Panel.imgLuong, this.X + 75, this.H - 8, 3);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().xuStr + string.Empty, this.X + 24, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().luongStr + string.Empty, this.X + 85, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(Panel.imgLuongKhoa, this.X + 130, this.H - 8, 3);
			mFont.tahoma_7_yellow.drawString(g, global::Char.myCharz().luongKhoaStr + string.Empty, this.X + 140, this.H - 13, mFont.LEFT, mFont.tahoma_7_grey);
		}
	}

	// Token: 0x060006C5 RID: 1733 RVA: 0x00068290 File Offset: 0x00066490
	internal void paintClanInfo(mGraphics g)
	{
		if (global::Char.myCharz().clan == null)
		{
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), 25, 50, 0, 33);
			mFont.tahoma_7b_white.drawString(g, mResources.not_join_clan, (this.wScroll - 50) / 2 + 50, 20, mFont.CENTER);
			return;
		}
		if (!this.isViewMember)
		{
			Clan clan = global::Char.myCharz().clan;
			if (clan != null)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), 25, 50, 0, 33);
				mFont.tahoma_7b_white.drawString(g, clan.name, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
				mFont.tahoma_7_yellow.drawString(g, mResources.achievement_point + ": " + clan.powerPoint, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_yellow.drawString(g, mResources.clan_point + ": " + clan.clanPoint.ToString(), 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
				mFont.tahoma_7_yellow.drawString(g, mResources.level + ": " + clan.level.ToString(), 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
				TextInfo.paint(g, clan.slogan, 60, 38, this.wScroll - 70, this.ITEM_HEIGHT, mFont.tahoma_7_yellow);
				return;
			}
		}
		else
		{
			Clan clan2 = ((this.currClan == null) ? global::Char.myCharz().clan : this.currClan);
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), 25, 50, 0, 33);
			mFont.tahoma_7b_white.drawString(g, clan2.name, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
			mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
			{
				mResources.member,
				": ",
				clan2.currMember.ToString(),
				"/",
				clan2.maxMember.ToString()
			}), 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, mResources.clan_leader + ": " + clan2.leaderName, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
			TextInfo.paint(g, clan2.slogan, 60, 38, this.wScroll - 70, this.ITEM_HEIGHT, mFont.tahoma_7_yellow);
		}
	}

	// Token: 0x060006C6 RID: 1734 RVA: 0x000684EC File Offset: 0x000666EC
	internal void paintToolInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, mResources.dragon_ball + " " + GameMidlet.VERSION, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7_yellow.drawString(g, mResources.character + ": " + global::Char.myCharz().cName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		string text = ((!GameCanvas.loginScr.tfUser.getText().Equals(string.Empty)) ? GameCanvas.loginScr.tfUser.getText() : mResources.not_register_yet);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
		{
			mResources.account_server,
			" ",
			ServerListScreen.nameServer[ServerListScreen.ipSelect],
			": ",
			text
		}), 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060006C7 RID: 1735 RVA: 0x000685D8 File Offset: 0x000667D8
	internal void paintGiaoDichInfo(mGraphics g)
	{
		mFont.tahoma_7_yellow.drawString(g, mResources.select_item, 60, 4, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.lock_trade, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.wait_opp_lock_trade, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.press_done, 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060006C8 RID: 1736 RVA: 0x0006865C File Offset: 0x0006685C
	internal void paintMyInfo(mGraphics g)
	{
		this.paintCharInfo(g, global::Char.myCharz());
	}

	// Token: 0x060006C9 RID: 1737 RVA: 0x0006866C File Offset: 0x0006686C
	internal void paintPetInfo(mGraphics g)
	{
		mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(global::Char.myPetz().cPower), this.X + 60, 4, mFont.LEFT, mFont.tahoma_7_grey);
		if (global::Char.myPetz().cPower > 0L)
		{
			mFont.tahoma_7_yellow.drawString(g, (!global::Char.myPetz().me) ? global::Char.myPetz().currStrLevel : global::Char.myPetz().getStrLevel(), this.X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		}
		if (global::Char.myPetz().cDamFull > 0)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + " :" + global::Char.myPetz().cDamFull.ToString(), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		}
		if (global::Char.myPetz().cMaxStamina > 0)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.vitality, this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(GameScr.imgMPLost, this.X + 100, 41, 0);
			int num = global::Char.myPetz().cStamina * mGraphics.getImageWidth(GameScr.imgMP) / (int)global::Char.myPetz().cMaxStamina;
			g.setClip(100, this.X + 41, num, 20);
			g.drawImage(GameScr.imgMP, this.X + 100, 41, 0);
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x060006CA RID: 1738 RVA: 0x000687FC File Offset: 0x000669FC
	internal void paintCharInfo(mGraphics g, global::Char c)
	{
		mFont.tahoma_7b_white.drawString(g, ((GameScr.isNewMember == 1) ? "       " : string.Empty) + c.cName, this.X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		if (GameScr.isNewMember == 1)
		{
			SmallImage.drawSmallImage(g, 5427, this.X + 55, 4, 0, 0);
		}
		if (c.cMaxStamina > 0)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.vitality, this.X + 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(GameScr.imgMPLost, this.X + 95, 19, 0);
			int num = c.cStamina * mGraphics.getImageWidth(GameScr.imgMP) / (int)c.cMaxStamina;
			g.setClip(95, this.X + 19, num, 20);
			g.drawImage(GameScr.imgMP, this.X + 95, 19, 0);
		}
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		if (c.cPower > 0L)
		{
			mFont.tahoma_7_yellow.drawString(g, (!c.me) ? c.currStrLevel : c.getStrLevel(), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		}
		mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(c.cPower), this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060006CB RID: 1739 RVA: 0x00068980 File Offset: 0x00066B80
	internal void paintCharInfo(mGraphics g, global::Char c, int x, int y)
	{
		mFont.tahoma_7b_white.drawString(g, ((GameScr.isNewMember == 1) ? "       " : string.Empty) + c.cName, x + 60, y + 4, mFont.LEFT, mFont.tahoma_7b_dark);
		if (GameScr.isNewMember == 1)
		{
			SmallImage.drawSmallImage(g, 5427, x + 55, y + 4, 0, 0);
		}
		if (c.cMaxStamina > 0)
		{
			mFont.tahoma_7_yellow.drawString(g, mResources.vitality, x + 60, y + 16, mFont.LEFT, mFont.tahoma_7_grey);
			g.drawImage(GameScr.imgMPLost, x + 95, y + 19, 0);
			int num = c.cStamina * mGraphics.getImageWidth(GameScr.imgMP) / (int)c.cMaxStamina;
			g.drawImage(GameScr.imgMP, x + 95, y + 19, 0);
		}
		if (c.cPower > 0L)
		{
			mFont.tahoma_7_yellow.drawString(g, (!c.me) ? c.currStrLevel : c.getStrLevel(), x + 60, y + 27, mFont.LEFT, mFont.tahoma_7_grey);
		}
		mFont.tahoma_7_yellow.drawString(g, mResources.power + ": " + NinjaUtil.getMoneys(c.cPower), x + 60, y + 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060006CC RID: 1740 RVA: 0x00068ACC File Offset: 0x00066CCC
	internal void paintZoneInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, mResources.zone + " " + TileMap.zoneID.ToString(), 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7_yellow.drawString(g, TileMap.mapName, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7b_white.drawString(g, TileMap.zoneID.ToString() + string.Empty, 25, 27, mFont.CENTER);
	}

	// Token: 0x060006CD RID: 1741 RVA: 0x00068B50 File Offset: 0x00066D50
	public int getCompare(Item item)
	{
		if (item == null)
		{
			return -1;
		}
		if (!item.isTypeBody())
		{
			return 0;
		}
		if (item.itemOption == null)
		{
			return -1;
		}
		ItemOption itemOption = item.itemOption[0];
		if (itemOption.optionTemplate.id == 22)
		{
			itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
			itemOption.param *= 1000;
		}
		if (itemOption.optionTemplate.id == 23)
		{
			itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
			itemOption.param *= 1000;
		}
		Item item2 = null;
		for (int i = 0; i < global::Char.myCharz().arrItemBody.Length; i++)
		{
			Item item3 = global::Char.myCharz().arrItemBody[i];
			if (itemOption.optionTemplate.id == 22)
			{
				itemOption.optionTemplate = GameScr.gI().iOptionTemplates[6];
				itemOption.param *= 1000;
			}
			if (itemOption.optionTemplate.id == 23)
			{
				itemOption.optionTemplate = GameScr.gI().iOptionTemplates[7];
				itemOption.param *= 1000;
			}
			if (item3 != null && item3.itemOption != null && item3.template.type == item.template.type)
			{
				item2 = item3;
				break;
			}
		}
		if (item2 == null)
		{
			this.isUp = true;
			return itemOption.param;
		}
		int num = ((item2 == null || item2.itemOption == null) ? itemOption.param : (itemOption.param - item2.itemOption[0].param));
		if (num < 0)
		{
			this.isUp = false;
			return num;
		}
		this.isUp = true;
		return num;
	}

	// Token: 0x060006CE RID: 1742 RVA: 0x00068CEC File Offset: 0x00066EEC
	internal void paintMapInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, mResources.MENUGENDER[(int)TileMap.planetID], 60, 4, mFont.LEFT);
		string text = string.Empty;
		if (TileMap.mapID >= 135 && TileMap.mapID <= 138)
		{
			text = " " + mResources.tang + TileMap.zoneID.ToString();
		}
		mFont.tahoma_7_yellow.drawString(g, TileMap.mapName + text, 60, 16, mFont.LEFT);
		mFont.tahoma_7b_white.drawString(g, mResources.quest_place + ": ", 60, 27, mFont.LEFT);
		if (GameScr.getTaskMapId() >= 0 && GameScr.getTaskMapId() <= TileMap.mapNames.Length - 1)
		{
			mFont.tahoma_7_yellow.drawString(g, TileMap.mapNames[GameScr.getTaskMapId()], 60, 38, mFont.LEFT);
			return;
		}
		mFont.tahoma_7_yellow.drawString(g, mResources.random, 60, 38, mFont.LEFT);
	}

	// Token: 0x060006CF RID: 1743 RVA: 0x00068DE4 File Offset: 0x00066FE4
	internal void paintShopInfo(mGraphics g)
	{
		if (this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null)
		{
			this.paintMyInfo(g);
			return;
		}
		if (this.selected < 0)
		{
			if (this.typeShop != 2)
			{
				mFont.tahoma_7_white.drawString(g, mResources.say_hello, this.X + 60, 14, 0);
				mFont.tahoma_7_white.drawString(g, Panel.strWantToBuy, this.X + 60, 26, 0);
				return;
			}
			mFont.tahoma_7_white.drawString(g, mResources.say_hello, this.X + 60, 5, 0);
			mFont.tahoma_7_white.drawString(g, Panel.strWantToBuy, this.X + 60, 17, 0);
			mFont.tahoma_7_white.drawString(g, string.Concat(new string[]
			{
				mResources.page,
				" ",
				(this.currPageShop[this.currentTabIndex] + 1).ToString(),
				"/",
				this.maxPageShop[this.currentTabIndex].ToString()
			}), this.X + 60, 29, 0);
			return;
		}
		else
		{
			if (this.currentTabIndex < 0 || this.currentTabIndex > global::Char.myCharz().arrItemShop.Length - 1 || this.selected < 0 || this.selected > global::Char.myCharz().arrItemShop[this.currentTabIndex].Length - 1)
			{
				return;
			}
			Item item = global::Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
			if (item != null)
			{
				if (this.Equals(GameCanvas.panel) && this.currentTabIndex <= 3 && this.typeShop == 2)
				{
					mFont.tahoma_7b_white.drawString(g, string.Concat(new string[]
					{
						mResources.page,
						" ",
						(this.currPageShop[this.currentTabIndex] + 1).ToString(),
						"/",
						this.maxPageShop[this.currentTabIndex].ToString()
					}), this.X + 55, 4, 0);
				}
				mFont.tahoma_7b_white.drawString(g, item.template.name, this.X + 55, 24, 0);
				string text = mResources.pow_request + " " + Res.formatNumber((long)item.template.strRequire);
				if ((long)item.template.strRequire > global::Char.myCharz().cPower)
				{
					mFont.tahoma_7_yellow.drawString(g, text, this.X + 55, 35, 0);
					return;
				}
				mFont.tahoma_7_green.drawString(g, text, this.X + 55, 35, 0);
			}
			return;
		}
	}

	// Token: 0x060006D0 RID: 1744 RVA: 0x00069084 File Offset: 0x00067284
	internal void paintItemBoxInfo(mGraphics g)
	{
		string text = string.Concat(new string[]
		{
			mResources.used,
			": ",
			this.hasUse.ToString(),
			"/",
			global::Char.myCharz().arrItemBox.Length.ToString(),
			" ",
			mResources.place
		});
		mFont.tahoma_7b_white.drawString(g, mResources.chest, 60, 4, 0);
		mFont.tahoma_7_yellow.drawString(g, text, 60, 16, 0);
	}

	// Token: 0x060006D1 RID: 1745 RVA: 0x00069110 File Offset: 0x00067310
	internal void paintSkillInfo(mGraphics g)
	{
		mFont.tahoma_7_white.drawString(g, "Top " + global::Char.myCharz().rank.ToString(), this.X + 45 + (this.W - 50) / 2, 2, mFont.CENTER);
		mFont.tahoma_7_yellow.drawString(g, mResources.potential_point, this.X + 45 + (this.W - 50) / 2, 14, mFont.CENTER);
		mFont.tahoma_7_white.drawString(g, string.Empty + NinjaUtil.getMoneys(global::Char.myCharz().cTiemNang), this.X + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0) + 45 + (this.W - 50) / 2, 26, mFont.CENTER);
		mFont.tahoma_7_yellow.drawString(g, mResources.active_point + ": " + NinjaUtil.getMoneys(global::Char.myCharz().cNangdong), this.X + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0) + 45 + (this.W - 50) / 2, 38, mFont.CENTER);
	}

	// Token: 0x060006D2 RID: 1746 RVA: 0x00069240 File Offset: 0x00067440
	internal void paintItemBodyBagInfo(mGraphics g)
	{
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
		{
			mResources.HP,
			": ",
			global::Char.myCharz().cHP.ToString(),
			" / ",
			global::Char.myCharz().cHPFull.ToString()
		}), this.X + 60, 2, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
		{
			mResources.KI,
			": ",
			global::Char.myCharz().cMP.ToString(),
			" / ",
			global::Char.myCharz().cMPFull.ToString()
		}), this.X + 60, 14, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + global::Char.myCharz().cDamFull.ToString(), this.X + 60, 26, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
		{
			mResources.armor,
			": ",
			global::Char.myCharz().cDefull.ToString(),
			", ",
			mResources.critical,
			": ",
			global::Char.myCharz().cCriticalFull.ToString(),
			"%"
		}), this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060006D3 RID: 1747 RVA: 0x000693D8 File Offset: 0x000675D8
	internal void paintItemBodyBagInfo(mGraphics g, int x, int y)
	{
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
		{
			mResources.HP,
			": ",
			global::Char.myCharz().cHP.ToString(),
			" / ",
			global::Char.myCharz().cHPFull.ToString()
		}), x, y + 2, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
		{
			mResources.KI,
			": ",
			global::Char.myCharz().cMP.ToString(),
			" / ",
			global::Char.myCharz().cMPFull.ToString()
		}), x, y + 14, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.hit_point + ": " + global::Char.myCharz().cDamFull.ToString(), x, y + 26, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
		{
			mResources.armor,
			": ",
			global::Char.myCharz().cDefull.ToString(),
			", ",
			mResources.critical,
			": ",
			global::Char.myCharz().cCriticalFull.ToString(),
			"%"
		}), x, y + 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060006D4 RID: 1748 RVA: 0x00069558 File Offset: 0x00067758
	internal void paintTopInfo(mGraphics g)
	{
		g.setClip(this.X + 1, this.Y, this.W - 2, this.yScroll - 2);
		g.setColor(9993045);
		g.fillRect(this.X, this.Y, this.W - 2, 50);
		switch (this.type)
		{
		case 0:
			if (this.currentTabIndex == 0)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
			}
			if (this.currentTabIndex == 1)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				if (this.isnewInventory)
				{
					this.paintCharInfo(g, global::Char.myCharz());
				}
				else
				{
					this.paintItemBodyBagInfo(g);
				}
			}
			if (this.currentTabIndex == 2)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintSkillInfo(g);
			}
			if (this.currentTabIndex == 3)
			{
				if (this.mainTabName.Length == 5)
				{
					this.paintClanInfo(g);
				}
				else
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintToolInfo(g);
				}
			}
			if (this.currentTabIndex == 4)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintToolInfo(g);
				return;
			}
			break;
		case 1:
			if (this.currentTabIndex == this.currentTabName.Length - 1 && GameCanvas.panel2 == null)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			}
			else if (global::Char.myCharz().npcFocus != null)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().npcFocus.avatar, this.X + 25, 50, 0, 33);
			}
			this.paintShopInfo(g);
			return;
		case 2:
			if (this.currentTabIndex == 0)
			{
				SmallImage.drawSmallImage(g, 526, this.X + 25, 50, 0, 33);
				this.paintItemBoxInfo(g);
			}
			if (this.currentTabIndex == 1)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintItemBodyBagInfo(g);
				return;
			}
			break;
		case 3:
			SmallImage.drawSmallImage(g, 561, this.X + 25, 50, 0, 33);
			this.paintZoneInfo(g);
			return;
		case 4:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMapInfo(g);
			return;
		case 5:
		case 6:
			break;
		case 7:
		case 17:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			return;
		case 8:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			return;
		case 9:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			return;
		case 10:
			if (this.charMenu != null)
			{
				SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
				this.paintCharInfo(g, this.charMenu);
				return;
			}
			break;
		case 11:
		case 16:
		case 23:
		case 24:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			return;
		case 12:
			if (this.currentTabIndex == 0)
			{
				int num = 1410;
				for (int i = 0; i < GameScr.vNpc.size(); i++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(i);
					if (npc.template.npcTemplateId == this.idNPC)
					{
						num = npc.avatar;
					}
				}
				SmallImage.drawSmallImage(g, num, this.X + 25, 50, 0, 33);
				this.paintCombineInfo(g);
			}
			if (this.currentTabIndex == 1)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintMyInfo(g);
				return;
			}
			break;
		case 13:
			if (this.currentTabIndex == 0 || this.currentTabIndex == 1)
			{
				if (this.Equals(GameCanvas.panel))
				{
					SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
					this.paintGiaoDichInfo(g);
				}
				if (this.Equals(GameCanvas.panel2) && this.charMenu != null)
				{
					SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
					this.paintCharInfo(g, this.charMenu);
				}
			}
			if (this.currentTabIndex == 2 && this.charMenu != null)
			{
				SmallImage.drawSmallImage(g, this.charMenu.avatarz(), this.X + 25, 50, 0, 33);
				this.paintCharInfo(g, this.charMenu);
				return;
			}
			break;
		case 14:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMapInfo(g);
			return;
		case 15:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			return;
		case 18:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			return;
		case 19:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintToolInfo(g);
			return;
		case 20:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintToolInfo(g);
			return;
		case 21:
			if (this.currentTabIndex == 0)
			{
				SmallImage.drawSmallImage(g, global::Char.myPetz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintPetInfo(g);
			}
			if (this.currentTabIndex == 1)
			{
				SmallImage.drawSmallImage(g, global::Char.myPetz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintPetStatusInfo(g);
			}
			if (this.currentTabIndex == 2)
			{
				SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
				this.paintItemBodyBagInfo(g);
				return;
			}
			break;
		case 22:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintToolInfo(g);
			break;
		case 25:
			SmallImage.drawSmallImage(g, global::Char.myCharz().avatarz(), this.X + 25, 50, 0, 33);
			this.paintMyInfo(g);
			return;
		default:
			return;
		}
	}

	// Token: 0x060006D5 RID: 1749 RVA: 0x00004887 File Offset: 0x00002A87
	internal void paintChatManager(mGraphics g)
	{
	}

	// Token: 0x060006D6 RID: 1750 RVA: 0x00004887 File Offset: 0x00002A87
	internal void paintChatPlayer(mGraphics g)
	{
	}

	// Token: 0x060006D7 RID: 1751 RVA: 0x00069C14 File Offset: 0x00067E14
	internal string getStatus(int status)
	{
		string text;
		switch (status)
		{
		case 0:
			text = mResources.follow;
			break;
		case 1:
			text = mResources.defend;
			break;
		case 2:
			text = mResources.attack;
			break;
		case 3:
			text = mResources.gohome;
			break;
		default:
			text = "aaa";
			break;
		}
		return text;
	}

	// Token: 0x060006D8 RID: 1752 RVA: 0x00069C60 File Offset: 0x00067E60
	internal void paintPetStatusInfo(mGraphics g)
	{
		mFont.tahoma_7b_white.drawString(g, "HP: " + global::Char.myPetz().cHP.ToString() + "/" + global::Char.myPetz().cHPFull.ToString(), this.X + 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7b_white.drawString(g, "MP: " + global::Char.myPetz().cMP.ToString() + "/" + global::Char.myPetz().cMPFull.ToString(), this.X + 60, 16, mFont.LEFT, mFont.tahoma_7b_dark);
		mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
		{
			mResources.critical,
			": ",
			global::Char.myPetz().cCriticalFull.ToString(),
			"   ",
			mResources.armor,
			": ",
			global::Char.myPetz().cDefull.ToString()
		}), this.X + 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
		mFont.tahoma_7_yellow.drawString(g, mResources.status + ": " + this.strStatus[(int)global::Char.myPetz().petStatus], this.X + 60, 38, mFont.LEFT, mFont.tahoma_7_grey);
	}

	// Token: 0x060006D9 RID: 1753 RVA: 0x00069DC0 File Offset: 0x00067FC0
	internal void paintCombineInfo(mGraphics g)
	{
		if (this.combineTopInfo != null)
		{
			for (int i = 0; i < this.combineTopInfo.Length; i++)
			{
				mFont.tahoma_7_white.drawString(g, this.combineTopInfo[i], this.X + 45 + (this.W - 50) / 2, 5 + i * 14, mFont.CENTER);
			}
		}
	}

	// Token: 0x060006DA RID: 1754 RVA: 0x00004887 File Offset: 0x00002A87
	internal void paintInfomation(mGraphics g)
	{
	}

	// Token: 0x060006DB RID: 1755 RVA: 0x00069E1C File Offset: 0x0006801C
	public void paintMap(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(-this.cmxMap, -this.cmyMap);
		g.drawImage(Panel.imgMap, this.xScroll, this.yScroll, 0);
		int head = global::Char.myCharz().head;
		SmallImage.drawSmallImage(g, (int)GameScr.parts[head].pi[global::Char.CharInfo[0][0][0]].id, this.xMap, this.yMap + 5, 0, 3);
		int num = mFont.CENTER;
		if (this.xMap <= 40)
		{
			num = mFont.LEFT;
		}
		if (this.xMap >= 220)
		{
			num = mFont.RIGHT;
		}
		mFont.tahoma_7b_yellow.drawString(g, TileMap.mapName, this.xMap, this.yMap - 12, num, mFont.tahoma_7_grey);
		int num2 = -1;
		if (GameScr.getTaskMapId() != -1)
		{
			for (int i = 0; i < Panel.mapId[(int)TileMap.planetID].Length; i++)
			{
				if (Panel.mapId[(int)TileMap.planetID][i] == GameScr.getTaskMapId())
				{
					num2 = i;
					break;
				}
				num2 = 4;
			}
			if (GameCanvas.gameTick % 4 > 0)
			{
				g.drawImage(ItemMap.imageFlare, this.xScroll + Panel.mapX[(int)TileMap.planetID][num2], this.yScroll + Panel.mapY[(int)TileMap.planetID][num2], 3);
			}
		}
		if (!GameCanvas.isTouch)
		{
			g.drawImage(Panel.imgBantay, this.xMove, this.yMove, StaticObj.TOP_RIGHT);
			for (int j = 0; j < Panel.mapX[(int)TileMap.planetID].Length; j++)
			{
				int num3 = Panel.mapX[(int)TileMap.planetID][j] + this.xScroll;
				int num4 = Panel.mapY[(int)TileMap.planetID][j] + this.yScroll;
				if (Res.inRect(num3 - 15, num4 - 15, 30, 30, this.xMove, this.yMove))
				{
					num = mFont.CENTER;
					if (num3 <= 20)
					{
						num = mFont.LEFT;
					}
					if (num3 >= 220)
					{
						num = mFont.RIGHT;
					}
					mFont.tahoma_7b_yellow.drawString(g, TileMap.mapNames[Panel.mapId[(int)TileMap.planetID][j]], num3, num4 - 12, num, mFont.tahoma_7_grey);
					break;
				}
			}
		}
		else if (!this.trans)
		{
			for (int k = 0; k < Panel.mapX[(int)TileMap.planetID].Length; k++)
			{
				int num5 = Panel.mapX[(int)TileMap.planetID][k] + this.xScroll;
				int num6 = Panel.mapY[(int)TileMap.planetID][k] + this.yScroll;
				if (Res.inRect(num5 - 15, num6 - 15, 30, 30, this.pX, this.pY))
				{
					num = mFont.CENTER;
					if (num5 <= 30)
					{
						num = mFont.LEFT;
					}
					if (num5 >= 220)
					{
						num = mFont.RIGHT;
					}
					g.drawImage(Panel.imgBantay, num5, num6, StaticObj.TOP_RIGHT);
					mFont.tahoma_7b_yellow.drawString(g, TileMap.mapNames[Panel.mapId[(int)TileMap.planetID][k]], num5, num6 - 12, num, mFont.tahoma_7_grey);
					break;
				}
			}
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if (num2 != -1)
		{
			if (Panel.mapX[(int)TileMap.planetID][num2] + this.xScroll < this.cmxMap)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 5, this.xScroll + 5, this.yScroll + this.hScroll / 2 - 4, 0);
			}
			if (this.cmxMap + this.wScroll < Panel.mapX[(int)TileMap.planetID][num2] + this.xScroll)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 6, this.xScroll + this.wScroll - 5, this.yScroll + this.hScroll / 2 - 4, StaticObj.TOP_RIGHT);
			}
			if (Panel.mapY[(int)TileMap.planetID][num2] < this.cmyMap)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll / 2, this.yScroll + 5, StaticObj.TOP_CENTER);
			}
			if (Panel.mapY[(int)TileMap.planetID][num2] > this.cmyMap + this.hScroll)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - 5, StaticObj.BOTTOM_HCENTER);
			}
		}
	}

	// Token: 0x060006DC RID: 1756 RVA: 0x0006A294 File Offset: 0x00068494
	public void paintTask(mGraphics g)
	{
		int num = ((GameCanvas.h <= 300) ? 15 : 20);
		if (Panel.isPaintMap && !GameScr.gI().isMapDocNhan() && !GameScr.gI().isMapFize())
		{
			g.drawImage((this.keyTouchMapButton != 1) ? GameScr.imgLbtn : GameScr.imgLbtnFocus, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - num, 3);
			mFont.tahoma_7b_dark.drawString(g, mResources.map, this.xScroll + this.wScroll / 2, this.yScroll + this.hScroll - (num + 5), mFont.CENTER);
		}
		this.xstart = this.xScroll + 5;
		this.ystart = this.yScroll + 14;
		this.yPaint = this.ystart;
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll - 35);
		if (this.scroll != null)
		{
			if (this.scroll.cmy > 0)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 1, this.xScroll + this.wScroll - 12, this.yScroll + 3, 0);
			}
			if (this.scroll.cmy < this.scroll.cmyLim)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 0, this.xScroll + this.wScroll - 12, this.yScroll + this.hScroll - 45, 0);
			}
			g.translate(0, -this.scroll.cmy);
		}
		this.indexRowMax = 0;
		if (this.indexMenu == 0)
		{
			bool flag = false;
			if (global::Char.myCharz().taskMaint != null)
			{
				for (int i = 0; i < global::Char.myCharz().taskMaint.names.Length; i++)
				{
					mFont.tahoma_7_grey.drawString(g, global::Char.myCharz().taskMaint.names[i], this.xScroll + this.wScroll / 2, this.yPaint - 5 + i * 12, mFont.CENTER);
					this.indexRowMax++;
				}
				this.yPaint += (global::Char.myCharz().taskMaint.names.Length - 1) * 12;
				int num2 = 0;
				string text = string.Empty;
				for (int j = 0; j < global::Char.myCharz().taskMaint.subNames.Length; j++)
				{
					if (global::Char.myCharz().taskMaint.subNames[j] != null)
					{
						num2 = j;
						text = "- " + global::Char.myCharz().taskMaint.subNames[j];
						if (global::Char.myCharz().taskMaint.counts[j] != -1)
						{
							if (global::Char.myCharz().taskMaint.index == j)
							{
								if (global::Char.myCharz().taskMaint.counts[j] != 1)
								{
									string text2 = text;
									text = string.Concat(new string[]
									{
										text2,
										" (",
										global::Char.myCharz().taskMaint.count.ToString(),
										"/",
										global::Char.myCharz().taskMaint.counts[j].ToString(),
										")"
									});
								}
								if (global::Char.myCharz().taskMaint.count == global::Char.myCharz().taskMaint.counts[j])
								{
									mFont.tahoma_7.drawString(g, text, this.xstart + 5, this.yPaint += 12, 0);
								}
								else
								{
									mFont mFont = mFont.tahoma_7_grey;
									if (!flag)
									{
										flag = true;
										mFont = mFont.tahoma_7_blue;
										mFont.drawString(g, text, this.xstart + 5 + ((mFont == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
									}
									else
									{
										mFont.drawString(g, "- ...", this.xstart + 5 + ((mFont == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
									}
								}
							}
							else if (global::Char.myCharz().taskMaint.index > j)
							{
								if (global::Char.myCharz().taskMaint.counts[j] != 1)
								{
									string text3 = text;
									text = string.Concat(new string[]
									{
										text3,
										" (",
										global::Char.myCharz().taskMaint.counts[j].ToString(),
										"/",
										global::Char.myCharz().taskMaint.counts[j].ToString(),
										")"
									});
								}
								mFont.tahoma_7_white.drawString(g, text, this.xstart + 5, this.yPaint += 12, 0);
							}
							else
							{
								if (global::Char.myCharz().taskMaint.counts[j] != 1)
								{
									text = text + " 0/" + global::Char.myCharz().taskMaint.counts[j].ToString();
								}
								mFont mFont2 = mFont.tahoma_7_grey;
								if (!flag)
								{
									flag = true;
									mFont2 = mFont.tahoma_7_blue;
									mFont2.drawString(g, text, this.xstart + 5 + ((mFont2 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
								}
								else
								{
									mFont2.drawString(g, "- ...", this.xstart + 5 + ((mFont2 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
								}
							}
						}
						else if (global::Char.myCharz().taskMaint.index > j)
						{
							mFont.tahoma_7_white.drawString(g, text, this.xstart + 5, this.yPaint += 12, 0);
						}
						else
						{
							mFont mFont3 = mFont.tahoma_7_grey;
							if (!flag)
							{
								flag = true;
								mFont3 = mFont.tahoma_7_blue;
								mFont3.drawString(g, text, this.xstart + 5 + ((mFont3 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
							}
							else
							{
								mFont3.drawString(g, "- ...", this.xstart + 5 + ((mFont3 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
							}
						}
						this.indexRowMax++;
					}
					else if (global::Char.myCharz().taskMaint.index <= j)
					{
						text = "- " + global::Char.myCharz().taskMaint.subNames[num2];
						mFont mFont4 = mFont.tahoma_7_grey;
						if (!flag)
						{
							flag = true;
							mFont4 = mFont.tahoma_7_blue;
						}
						mFont4.drawString(g, text, this.xstart + 5 + ((mFont4 == mFont.tahoma_7_blue && GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), this.yPaint += 12, 0);
					}
				}
				this.yPaint += 5;
				for (int k = 0; k < global::Char.myCharz().taskMaint.details.Length; k++)
				{
					mFont.tahoma_7_green2.drawString(g, global::Char.myCharz().taskMaint.details[k], this.xstart + 5, this.yPaint += 12, 0);
					this.indexRowMax++;
				}
			}
			else
			{
				int taskMapId = GameScr.getTaskMapId();
				sbyte taskNpcId = GameScr.getTaskNpcId();
				string text4 = string.Empty;
				if (taskMapId == -3 || taskNpcId == -3)
				{
					text4 = mResources.DES_TASK[3];
				}
				else if (global::Char.myCharz().taskMaint == null && global::Char.myCharz().ctaskId == 9 && global::Char.myCharz().nClass.classId == 0)
				{
					text4 = mResources.TASK_INPUT_CLASS;
				}
				else
				{
					if (taskNpcId < 0 || taskMapId < 0)
					{
						return;
					}
					text4 = string.Concat(new string[]
					{
						mResources.DES_TASK[0],
						Npc.arrNpcTemplate[(int)taskNpcId].name,
						mResources.DES_TASK[1],
						TileMap.mapNames[taskMapId],
						mResources.DES_TASK[2]
					});
				}
				string[] array = mFont.tahoma_7_white.splitFontArray(text4, 150);
				for (int l = 0; l < array.Length; l++)
				{
					if (l == 0)
					{
						mFont.tahoma_7_white.drawString(g, array[l], this.xstart + 5, this.yPaint = this.ystart, 0);
					}
					else
					{
						mFont.tahoma_7_white.drawString(g, array[l], this.xstart + 5, this.yPaint += 12, 0);
					}
				}
			}
		}
		else if (this.indexMenu == 1)
		{
			this.yPaint = this.ystart - 12;
			for (int m = 0; m < global::Char.myCharz().taskOrders.size(); m++)
			{
				TaskOrder taskOrder = (TaskOrder)global::Char.myCharz().taskOrders.elementAt(m);
				mFont.tahoma_7_white.drawString(g, taskOrder.name, this.xstart + 5, this.yPaint += 12, 0);
				if (taskOrder.count == (int)taskOrder.maxCount)
				{
					mFont.tahoma_7_white.drawString(g, string.Concat(new string[]
					{
						(taskOrder.taskId != 0) ? mResources.KILLBOSS : mResources.KILL,
						" ",
						Mob.arrMobTemplate[taskOrder.killId].name,
						" (",
						taskOrder.count.ToString(),
						"/",
						taskOrder.maxCount.ToString(),
						")"
					}), this.xstart + 5, this.yPaint += 12, 0);
				}
				else
				{
					mFont.tahoma_7_blue.drawString(g, string.Concat(new string[]
					{
						(taskOrder.taskId != 0) ? mResources.KILLBOSS : mResources.KILL,
						" ",
						Mob.arrMobTemplate[taskOrder.killId].name,
						" (",
						taskOrder.count.ToString(),
						"/",
						taskOrder.maxCount.ToString(),
						")"
					}), this.xstart + 5, this.yPaint += 12, 0);
				}
				this.indexRowMax += 3;
				this.inforW = this.popupW - 25;
				this.paintMultiLine(g, mFont.tahoma_7_grey, taskOrder.description, this.xstart + 5, this.yPaint += 12, 0);
				this.yPaint += 12;
			}
		}
		if (this.scroll == null)
		{
			this.scroll = new Scroll();
			this.scroll.setStyle(this.indexRowMax, 12, this.xScroll, this.yScroll, this.wScroll, this.hScroll - num - 40, true, 1);
		}
	}

	// Token: 0x060006DD RID: 1757 RVA: 0x0006AE54 File Offset: 0x00069054
	public void paintMultiLine(mGraphics g, mFont f, string[] arr, string str, int x, int y, int align)
	{
		for (int i = 0; i < arr.Length; i++)
		{
			string text = arr[i];
			if (text.StartsWith("c"))
			{
				if (text.StartsWith("c0"))
				{
					text = text.Substring(2);
					f = mFont.tahoma_7b_dark;
				}
				else if (text.StartsWith("c1"))
				{
					text = text.Substring(2);
					f = mFont.tahoma_7b_yellow;
				}
				else if (text.StartsWith("c2"))
				{
					text = text.Substring(2);
					f = mFont.tahoma_7b_green;
				}
			}
			if (i == 0)
			{
				f.drawString(g, text, x, y, align);
			}
			else
			{
				if (i < this.indexRow + 30 && i > this.indexRow - 30)
				{
					f.drawString(g, text, x, y += 12, align);
				}
				else
				{
					y += 12;
				}
				this.yPaint += 12;
				this.indexRowMax++;
			}
		}
	}

	// Token: 0x060006DE RID: 1758 RVA: 0x0006AF44 File Offset: 0x00069144
	public void paintMultiLine(mGraphics g, mFont f, string str, int x, int y, int align)
	{
		int num = ((!GameCanvas.isTouch || GameCanvas.w < 320) ? 10 : 20);
		string[] array = f.splitFontArray(str, this.inforW - num);
		for (int i = 0; i < array.Length; i++)
		{
			if (i == 0)
			{
				f.drawString(g, array[i], x, y, align);
			}
			else
			{
				if (i < this.indexRow + 15 && i > this.indexRow - 15)
				{
					f.drawString(g, array[i], x, y += 12, align);
				}
				else
				{
					y += 12;
				}
				this.yPaint += 12;
				this.indexRowMax++;
			}
		}
	}

	// Token: 0x060006DF RID: 1759 RVA: 0x0006AFF4 File Offset: 0x000691F4
	public void cleanCombine()
	{
		for (int i = 0; i < this.vItemCombine.size(); i++)
		{
			((Item)this.vItemCombine.elementAt(i)).isSelect = false;
		}
		this.vItemCombine.removeAllElements();
	}

	// Token: 0x060006E0 RID: 1760 RVA: 0x0006B03C File Offset: 0x0006923C
	public void hideNow()
	{
		if (this.timeShow > 0)
		{
			this.isClose = false;
			return;
		}
		this.cp = null;
		if (this.isTypeShop() || TileMap.mapID == 45)
		{
			global::Char.myCharz().resetPartTemp();
		}
		if (this.chatTField != null && this.type == 13 && this.chatTField.isShow)
		{
			this.chatTField = null;
		}
		if (this.type == 13 && !this.isAccept)
		{
			Service.gI().giaodich(3, -1, -1, -1);
		}
		Res.outz("HIDE PANELLLLLLLLLLLLLLLLLLLLLL");
		SoundMn.gI().buttonClose();
		GameScr.isPaint = true;
		TileMap.lastPlanetId = -1;
		Panel.imgMap = null;
		mSystem.gcc();
		this.isClanOption = false;
		this.isClose = true;
		this.cleanCombine();
		Hint.clickNpc();
		GameCanvas.panel2 = null;
		GameCanvas.clearAllPointerEvent();
		GameCanvas.clearKeyPressed();
		this.pointerDownTime = (this.pointerDownFirstX = 0);
		this.pointerIsDowning = false;
		this.isShow = false;
		if ((global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5) && global::Char.myCharz().meDead)
		{
			Command command = new Command(mResources.DIES[0], 11038, GameScr.gI());
			GameScr.gI().center = command;
			global::Char.myCharz().cHP = 0;
		}
	}

	// Token: 0x060006E1 RID: 1761 RVA: 0x0006B198 File Offset: 0x00069398
	public void hide()
	{
		if (this.timeShow > 0)
		{
			this.isClose = false;
			return;
		}
		this.cp = null;
		if (this.isTypeShop() || TileMap.mapID == 45)
		{
			global::Char.myCharz().resetPartTemp();
		}
		if (this.chatTField != null && this.type == 13 && this.chatTField.isShow)
		{
			this.chatTField = null;
		}
		if (this.type == 13 && !this.isAccept)
		{
			Service.gI().giaodich(3, -1, -1, -1);
		}
		if (this.type == 15)
		{
			Service.gI().sendThachDau(-1);
		}
		SoundMn.gI().buttonClose();
		GameScr.isPaint = true;
		TileMap.lastPlanetId = -1;
		if (Panel.imgMap != null)
		{
			Panel.imgMap.texture = null;
			Panel.imgMap = null;
		}
		mSystem.gcc();
		this.isClanOption = false;
		if (this.type != 4)
		{
			if (this.type == 24)
			{
				this.setTypeGameInfo();
			}
			else if (this.type == 23)
			{
				this.setTypeMain();
			}
			else if (this.type == 3 || this.type == 14)
			{
				if (this.isChangeZone)
				{
					this.isClose = true;
				}
				else
				{
					this.setTypeMain();
					this.cmx = (this.cmtoX = 0);
				}
			}
			else if (this.type == 18 || this.type == 19 || this.type == 20 || this.type == 21)
			{
				this.setTypeMain();
				this.cmx = (this.cmtoX = 0);
			}
			else if (this.type == 8 || this.type == 11 || this.type == 16)
			{
				this.setTypeAccount();
				this.cmx = (this.cmtoX = 0);
			}
			else
			{
				this.isClose = true;
			}
		}
		else
		{
			this.setTypeMain();
			this.cmx = (this.cmtoX = 0);
		}
		Hint.clickNpc();
		GameCanvas.panel2 = null;
		GameCanvas.clearAllPointerEvent();
		GameCanvas.clearKeyPressed();
		GameCanvas.isFocusPanel2 = false;
		this.pointerDownTime = (this.pointerDownFirstX = 0);
		this.pointerIsDowning = false;
		if ((global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5) && global::Char.myCharz().meDead)
		{
			Command command = new Command(mResources.DIES[0], 11038, GameScr.gI());
			GameScr.gI().center = command;
			global::Char.myCharz().cHP = 0;
		}
	}

	// Token: 0x060006E2 RID: 1762 RVA: 0x0006B40C File Offset: 0x0006960C
	public void update()
	{
		if (this.chatTField != null && this.chatTField.isShow)
		{
			this.chatTField.update();
			return;
		}
		if (this.isKiguiXu)
		{
			this.delayKigui++;
			if (this.delayKigui == 10)
			{
				this.delayKigui = 0;
				this.isKiguiXu = false;
				this.chatTField.tfChat.setText(string.Empty);
				this.chatTField.strChat = mResources.kiguiXuchat + " ";
				this.chatTField.tfChat.name = mResources.input_money;
				this.chatTField.to = string.Empty;
				this.chatTField.isShow = true;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				this.chatTField.tfChat.setMaxTextLenght(10);
				if (GameCanvas.isTouch)
				{
					this.chatTField.tfChat.doChangeToTextBox();
				}
				if (Main.isWindowsPhone)
				{
					this.chatTField.tfChat.strInfo = this.chatTField.strChat;
				}
				if (!Main.isPC)
				{
					this.chatTField.startChat2(this, string.Empty);
				}
			}
			return;
		}
		if (this.isKiguiLuong)
		{
			this.delayKigui++;
			if (this.delayKigui == 10)
			{
				this.delayKigui = 0;
				this.isKiguiLuong = false;
				this.chatTField.tfChat.setText(string.Empty);
				this.chatTField.strChat = mResources.kiguiLuongchat + "  ";
				this.chatTField.tfChat.name = mResources.input_money;
				this.chatTField.to = string.Empty;
				this.chatTField.isShow = true;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				this.chatTField.tfChat.setMaxTextLenght(10);
				if (GameCanvas.isTouch)
				{
					this.chatTField.tfChat.doChangeToTextBox();
				}
				if (Main.isWindowsPhone)
				{
					this.chatTField.tfChat.strInfo = this.chatTField.strChat;
				}
				if (!Main.isPC)
				{
					this.chatTField.startChat2(this, string.Empty);
				}
			}
			return;
		}
		if (this.scroll != null)
		{
			this.scroll.updatecm();
		}
		if (this.tabIcon != null && this.tabIcon.isShow)
		{
			this.tabIcon.update();
			return;
		}
		this.moveCamera();
		if (this.isTabInven() && this.isnewInventory)
		{
			if (this.eBanner == null)
			{
				this.eBanner = new Effect(205, 0, 0, 3, 10, -1);
				this.eBanner.typeEff = 2;
			}
			if (this.eBanner != null)
			{
				this.eBanner.update();
			}
		}
		if (this.waitToPerform > 0)
		{
			this.waitToPerform--;
			if (this.waitToPerform == 0)
			{
				this.lastSelect[this.currentTabIndex] = this.selected;
				switch (this.type)
				{
				case 0:
					this.doFireMain();
					break;
				case 1:
				case 17:
					this.doFireShop();
					break;
				case 2:
					this.doFireBox();
					break;
				case 3:
					this.doFireZone();
					break;
				case 4:
					this.doFireMap();
					break;
				case 7:
					if (this.Equals(GameCanvas.panel2) && GameCanvas.panel.type == 2)
					{
						this.doFireBox();
						return;
					}
					this.doFireInventory();
					break;
				case 8:
					this.doFireLogMessage();
					break;
				case 9:
					this.doFireArchivement();
					break;
				case 10:
					this.doFirePlayerMenu();
					break;
				case 11:
					this.doFireFriend();
					break;
				case 12:
					this.doFireCombine();
					break;
				case 13:
					this.doFireGiaoDich();
					break;
				case 14:
					this.doFireMapTrans();
					break;
				case 15:
					this.doFireTop();
					break;
				case 16:
					this.doFireEnemy();
					break;
				case 18:
					this.doFireChangeFlag();
					break;
				case 19:
					this.doFireOption();
					break;
				case 20:
					this.doFireAccount();
					break;
				case 21:
					this.doFirePetMain();
					break;
				case 22:
					this.doFireAuto();
					break;
				case 23:
					this.doFireGameInfo();
					break;
				case 25:
					this.doSpeacialSkill();
					break;
				}
			}
		}
		for (int i = 0; i < ClanMessage.vMessage.size(); i++)
		{
			((ClanMessage)ClanMessage.vMessage.elementAt(i)).update();
		}
		this.updateCombineEff();
	}

	// Token: 0x060006E3 RID: 1763 RVA: 0x00004887 File Offset: 0x00002A87
	internal void doSpeacialSkill()
	{
	}

	// Token: 0x060006E4 RID: 1764 RVA: 0x0006B8A8 File Offset: 0x00069AA8
	internal void doFireGameInfo()
	{
		if (this.selected != -1)
		{
			this.infoSelect = this.selected;
			((GameInfo)Panel.vGameInfo.elementAt(this.infoSelect)).hasRead = true;
			Rms.saveRMSInt(((GameInfo)Panel.vGameInfo.elementAt(this.infoSelect)).id.ToString() + string.Empty, 1);
			this.setTypeGameSubInfo();
		}
	}

	// Token: 0x060006E5 RID: 1765 RVA: 0x00004887 File Offset: 0x00002A87
	internal void doFireAuto()
	{
	}

	// Token: 0x060006E6 RID: 1766 RVA: 0x0006B91C File Offset: 0x00069B1C
	internal void doFirePetMain()
	{
		if (this.currentTabIndex == 0)
		{
			if (this.selected == -1 || this.selected > global::Char.myPetz().arrItemBody.Length - 1)
			{
				return;
			}
			MyVector myVector = new MyVector(string.Empty);
			this.currItem = global::Char.myPetz().arrItemBody[this.selected];
			if (this.currItem != null)
			{
				myVector.addElement(new Command(mResources.MOVEOUT, this, 2006, this.currItem));
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
		if (this.currentTabIndex == 1)
		{
			this.doFirePetStatus();
		}
		if (this.currentTabIndex == 2)
		{
			this.doFireInventory();
		}
	}

	// Token: 0x060006E7 RID: 1767 RVA: 0x0006B9FC File Offset: 0x00069BFC
	internal void doFirePetStatus()
	{
		if (this.selected == -1)
		{
			return;
		}
		if (this.selected == 5)
		{
			GameCanvas.startYesNoDlg(mResources.sure_fusion, new Command(mResources.YES, 888351), new Command(mResources.NO, 2001));
			return;
		}
		Service.gI().petStatus((sbyte)this.selected);
		if (this.selected < 4)
		{
			global::Char.myPetz().petStatus = (sbyte)this.selected;
		}
	}

	// Token: 0x060006E8 RID: 1768 RVA: 0x0006BA70 File Offset: 0x00069C70
	internal void doFireTop()
	{
		if (this.selected >= -1)
		{
			if (this.isThachDau)
			{
				Service.gI().sendTop(this.topName, (sbyte)this.selected);
				return;
			}
			MyVector myVector = new MyVector(string.Empty);
			myVector.addElement(new Command(mResources.CHAR_ORDER[0], this, 9999, (TopInfo)this.vTop.elementAt(this.selected)));
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addThachDauDetail((TopInfo)this.vTop.elementAt(this.selected));
		}
	}

	// Token: 0x060006E9 RID: 1769 RVA: 0x0006BB2C File Offset: 0x00069D2C
	internal void doFireMapTrans()
	{
		this.doFireZone();
	}

	// Token: 0x060006EA RID: 1770 RVA: 0x0006BB34 File Offset: 0x00069D34
	internal void doFireGiaoDich()
	{
		if (this.currentTabIndex == 0 && this.Equals(GameCanvas.panel))
		{
			this.doFireInventory();
			return;
		}
		if ((this.currentTabIndex == 0 && this.Equals(GameCanvas.panel2)) || this.currentTabIndex == 2)
		{
			if (this.Equals(GameCanvas.panel2))
			{
				this.currItem = (Item)GameCanvas.panel2.vFriendGD.elementAt(this.selected);
			}
			else
			{
				this.currItem = (Item)GameCanvas.panel.vFriendGD.elementAt(this.selected);
			}
			Res.outz2("toi day select= " + this.selected.ToString());
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(mResources.CLOSE, this, 8000, this.currItem));
			if (this.currItem != null)
			{
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
		if (this.currentTabIndex == 1)
		{
			if (this.selected == this.currentListLength - 3)
			{
				if (this.isLock)
				{
					return;
				}
				this.putMoney();
			}
			else if (this.selected == this.currentListLength - 2)
			{
				if (!this.isAccept)
				{
					this.isLock = !this.isLock;
					if (this.isLock)
					{
						Service.gI().giaodich(5, -1, -1, -1);
					}
					else
					{
						this.hide();
						InfoDlg.showWait();
						Service.gI().giaodich(3, -1, -1, -1);
					}
				}
				else
				{
					this.isAccept = false;
				}
			}
			else if (this.selected == this.currentListLength - 1)
			{
				if (this.isLock && !this.isAccept && this.isFriendLock)
				{
					GameCanvas.startYesNoDlg(mResources.do_u_sure_to_trade, new Command(mResources.YES, this, 7002, null), new Command(mResources.NO, this, 4005, null));
				}
			}
			else
			{
				if (this.isLock)
				{
					return;
				}
				this.currItem = (Item)GameCanvas.panel.vMyGD.elementAt(this.selected);
				MyVector myVector2 = new MyVector();
				myVector2.addElement(new Command(mResources.CLOSE, this, 8000, this.currItem));
				if (this.currItem != null)
				{
					GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
					this.addItemDetail(this.currItem);
				}
				else
				{
					this.cp = null;
				}
			}
		}
		if (GameCanvas.isTouch)
		{
			this.selected = -1;
		}
	}

	// Token: 0x060006EB RID: 1771 RVA: 0x0006BDF8 File Offset: 0x00069FF8
	internal void doFireCombine()
	{
		if (this.currentTabIndex == 0)
		{
			if (this.selected == -1 || this.vItemCombine.size() == 0)
			{
				return;
			}
			if (this.selected == this.vItemCombine.size())
			{
				this.keyTouchCombine = -1;
				this.selected = (GameCanvas.isTouch ? (-1) : 0);
				InfoDlg.showWait();
				Service.gI().combine(1, this.vItemCombine);
				return;
			}
			if (this.selected > this.vItemCombine.size() - 1)
			{
				return;
			}
			this.currItem = (Item)GameCanvas.panel.vItemCombine.elementAt(this.selected);
			MyVector myVector = new MyVector();
			myVector.addElement(new Command(mResources.GETOUT, this, 6001, this.currItem));
			if (this.currItem != null)
			{
				GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addItemDetail(this.currItem);
			}
			else
			{
				this.cp = null;
			}
		}
		if (this.currentTabIndex == 1)
		{
			this.doFireInventory();
		}
	}

	// Token: 0x060006EC RID: 1772 RVA: 0x0006BF1B File Offset: 0x0006A11B
	internal void doFirePlayerMenu()
	{
		if (this.selected != -1)
		{
			this.isSelectPlayerMenu = true;
			this.hide();
		}
	}

	// Token: 0x060006ED RID: 1773 RVA: 0x0006BF34 File Offset: 0x0006A134
	internal void doFireShop()
	{
		this.currItem = null;
		if (this.selected < 0)
		{
			return;
		}
		MyVector myVector = new MyVector();
		if (this.currentTabIndex < this.currentTabName.Length - ((GameCanvas.panel2 == null) ? 1 : 0) && this.type != 17)
		{
			this.currItem = global::Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
			if (this.currItem != null)
			{
				if (this.currItem.isBuySpec)
				{
					if (this.currItem.buySpec > 0)
					{
						myVector.addElement(new Command(mResources.buy_with + "\n" + Res.formatNumber2((long)this.currItem.buySpec), this, 3005, this.currItem));
					}
				}
				else if (this.typeShop == 4)
				{
					myVector.addElement(new Command(mResources.receive_upper, this, 30001, this.currItem));
					myVector.addElement(new Command(mResources.DELETE, this, 30002, this.currItem));
					myVector.addElement(new Command(mResources.receive_all, this, 30003, this.currItem));
				}
				else if (this.currItem.buyCoin == 0 && this.currItem.buyGold == 0)
				{
					if (this.currItem.powerRequire != 0L)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.learn_with,
							"\n",
							Res.formatNumber(this.currItem.powerRequire),
							" \n",
							mResources.potential
						}), this, 3004, this.currItem));
					}
					else
					{
						myVector.addElement(new Command(mResources.receive_upper + "\n" + mResources.free, this, 3000, this.currItem));
					}
				}
				else if (this.typeShop == 8)
				{
					if (this.currItem.buyCoin > 0)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.buy_with,
							"\n",
							Res.formatNumber2((long)this.currItem.buyCoin),
							"\n",
							mResources.XU
						}), this, 30001, this.currItem));
					}
					if (this.currItem.buyGold > 0)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.buy_with,
							"\n",
							Res.formatNumber2((long)this.currItem.buyGold),
							"\n",
							mResources.LUONG
						}), this, 30002, this.currItem));
					}
				}
				else if (this.typeShop != 2)
				{
					if (this.currItem.buyCoin > 0)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.buy_with,
							"\n",
							Res.formatNumber2((long)this.currItem.buyCoin),
							"\n",
							mResources.XU
						}), this, 3000, this.currItem));
					}
					if (this.currItem.buyGold > 0)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.buy_with,
							"\n",
							Res.formatNumber2((long)this.currItem.buyGold),
							"\n",
							mResources.LUONG
						}), this, 3001, this.currItem));
					}
				}
				else
				{
					if (this.currItem.buyCoin != -1)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.buy_with,
							"\n",
							Res.formatNumber2((long)this.currItem.buyCoin),
							"\n",
							mResources.XU
						}), this, 10016, this.currItem));
					}
					if (this.currItem.buyGold != -1)
					{
						myVector.addElement(new Command(string.Concat(new string[]
						{
							mResources.buy_with,
							"\n",
							Res.formatNumber2((long)this.currItem.buyGold),
							"\n",
							mResources.LUONG
						}), this, 10017, this.currItem));
					}
				}
			}
		}
		else if (this.typeShop == 0)
		{
			if (this.selected == 0)
			{
				this.setNewSelected(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length, false);
			}
			else
			{
				this.currItem = null;
				if (!this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody))
				{
					Item item = global::Char.myCharz().arrItemBag[this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody)];
					if (item != null)
					{
						this.currItem = item;
					}
				}
				else
				{
					Item item2 = global::Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
					if (item2 != null)
					{
						this.currItem = item2;
					}
				}
				if (this.currItem != null)
				{
					myVector.addElement(new Command(mResources.SALE, this, 3002, this.currItem));
				}
			}
		}
		else
		{
			if (this.type == 17)
			{
				this.currItem = global::Char.myCharz().arrItemShop[4][this.selected];
			}
			else
			{
				this.currItem = global::Char.myCharz().arrItemShop[this.currentTabIndex][this.selected];
			}
			if (this.currItem.buyType == 0)
			{
				if (this.currItem.isHaveOption(87))
				{
					myVector.addElement(new Command(mResources.kiguiLuong, this, 10013, this.currItem));
				}
				else
				{
					myVector.addElement(new Command(mResources.kiguiXu, this, 10012, this.currItem));
				}
			}
			else if (this.currItem.buyType == 1)
			{
				myVector.addElement(new Command(mResources.huykigui, this, 10014, this.currItem));
				myVector.addElement(new Command(mResources.upTop, this, 10018, this.currItem));
			}
			else if (this.currItem.buyType == 2)
			{
				myVector.addElement(new Command(mResources.nhantien, this, 10015, this.currItem));
			}
		}
		if (this.currItem != null)
		{
			global::Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addItemDetail(this.currItem);
			return;
		}
		this.cp = null;
	}

	// Token: 0x060006EE RID: 1774 RVA: 0x0006C620 File Offset: 0x0006A820
	internal void doFireArchivement()
	{
		if (this.selected >= 0 && global::Char.myCharz().arrArchive[this.selected].isFinish && !global::Char.myCharz().arrArchive[this.selected].isRecieve)
		{
			if (!GameCanvas.isTouch)
			{
				Service.gI().getArchivemnt(this.selected);
				return;
			}
			if (GameCanvas.px > this.xScroll + this.wScroll - 40)
			{
				Service.gI().getArchivemnt(this.selected);
			}
		}
	}

	// Token: 0x060006EF RID: 1775 RVA: 0x0006C6A8 File Offset: 0x0006A8A8
	internal void doFireInventory()
	{
		Res.outz("fire inventory");
		if (global::Char.myCharz().statusMe == 14)
		{
			GameCanvas.startOKDlg(mResources.can_not_do_when_die);
			return;
		}
		if (this.selected == -1)
		{
			return;
		}
		if (this.selected == 0)
		{
			this.setNewSelected(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length, false);
			return;
		}
		this.currItem = null;
		MyVector myVector = new MyVector();
		if (this.isnewInventory)
		{
			this.currItem = this.itemInvenNew;
			if (this.newSelected == 0)
			{
				myVector.addElement(new Command(mResources.GETOUT, this, 2002, this.currItem));
			}
			else if (GameCanvas.panel.type == 12)
			{
				myVector.addElement(new Command(mResources.use_for_combine, this, 6000, this.currItem));
			}
			else if (GameCanvas.panel.type == 13)
			{
				myVector.addElement(new Command(mResources.use_for_trade, this, 7000, this.currItem));
			}
			else if (this.currItem.isTypeBody())
			{
				myVector.addElement(new Command(mResources.USE, this, 2000, this.currItem));
				if (global::Char.myCharz().havePet)
				{
					myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, this.currItem));
				}
			}
			else
			{
				myVector.addElement(new Command(mResources.USE, this, 2001, this.currItem));
			}
		}
		else if (!this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody))
		{
			Item item = global::Char.myCharz().arrItemBag[this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody)];
			if (item != null)
			{
				this.currItem = item;
				if (GameCanvas.panel.type == 12)
				{
					myVector.addElement(new Command(mResources.use_for_combine, this, 6000, this.currItem));
				}
				else if (GameCanvas.panel.type == 13)
				{
					myVector.addElement(new Command(mResources.use_for_trade, this, 7000, this.currItem));
				}
				else if (item.isTypeBody())
				{
					myVector.addElement(new Command(mResources.USE, this, 2000, this.currItem));
					if (global::Char.myCharz().havePet)
					{
						myVector.addElement(new Command(mResources.MOVEFORPET, this, 2005, this.currItem));
					}
				}
				else
				{
					myVector.addElement(new Command(mResources.USE, this, 2001, this.currItem));
				}
			}
		}
		else
		{
			Item item2 = global::Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
			if (item2 != null)
			{
				this.currItem = item2;
				myVector.addElement(new Command(mResources.GETOUT, this, 2002, this.currItem));
			}
		}
		if (this.currItem != null)
		{
			global::Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
			if (GameCanvas.panel.type != 12 && GameCanvas.panel.type != 13)
			{
				if (this.position == 0)
				{
					myVector.addElement(new Command(mResources.MOVEOUT, this, 2003, this.currItem));
				}
				if (this.position == 1)
				{
					myVector.addElement(new Command(mResources.SALE, this, 3002, this.currItem));
				}
			}
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addItemDetail(this.currItem);
			return;
		}
		this.cp = null;
	}

	// Token: 0x060006F0 RID: 1776 RVA: 0x0006CA7F File Offset: 0x0006AC7F
	internal void doRada()
	{
		this.hide();
		if (RadarScr.list == null || RadarScr.list.size() == 0)
		{
			Service.gI().SendRada(0, -1);
			RadarScr.gI().switchToMe();
			return;
		}
		RadarScr.gI().switchToMe();
	}

	// Token: 0x060006F1 RID: 1777 RVA: 0x0006CABC File Offset: 0x0006ACBC
	internal void doFireTool()
	{
		if (this.selected < 0)
		{
			return;
		}
		if (SoundMn.IsDelAcc && this.selected == Panel.strTool.Length - 1)
		{
			Service.gI().sendDelAcc();
			return;
		}
		if (!global::Char.myCharz().havePet)
		{
			switch (this.selected)
			{
			case 0:
				this.hide();
				this.doRada();
				return;
			case 1:
				Service.gI().openMenu(54);
				return;
			case 2:
				this.setTypeGameInfo();
				return;
			case 3:
				Service.gI().getFlag(0, -1);
				InfoDlg.showWait();
				return;
			case 4:
				if (global::Char.myCharz().statusMe == 14)
				{
					GameCanvas.startOKDlg(mResources.can_not_do_when_die);
					return;
				}
				Service.gI().openUIZone();
				return;
			case 5:
				GameCanvas.endDlg();
				if (global::Char.myCharz().checkLuong() < 5)
				{
					GameCanvas.startOKDlg(mResources.not_enough_luong_world_channel);
					return;
				}
				if (this.chatTField == null)
				{
					this.chatTField = new ChatTextField();
					this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
					this.chatTField.initChatTextField();
					this.chatTField.parentScreen = GameCanvas.panel;
				}
				this.chatTField.strChat = mResources.world_channel_5_luong;
				this.chatTField.tfChat.name = mResources.CHAT;
				this.chatTField.to = string.Empty;
				this.chatTField.isShow = true;
				this.chatTField.tfChat.isFocus = true;
				this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
				if (Main.isWindowsPhone)
				{
					this.chatTField.tfChat.strInfo = this.chatTField.strChat;
				}
				if (!Main.isPC)
				{
					this.chatTField.startChat2(this, string.Empty);
					return;
				}
				if (GameCanvas.isTouch)
				{
					this.chatTField.tfChat.doChangeToTextBox();
					return;
				}
				break;
			case 6:
				this.setTypeAccount();
				return;
			case 7:
				this.setTypeOption();
				return;
			case 8:
				GameCanvas.loginScr.backToRegister();
				return;
			case 9:
				if (GameCanvas.loginScr.isLogin2)
				{
					SoundMn.gI().backToRegister();
				}
				break;
			default:
				return;
			}
			return;
		}
		switch (this.selected)
		{
		case 0:
			this.hide();
			this.doRada();
			return;
		case 1:
			Service.gI().openMenu(54);
			return;
		case 2:
			this.setTypeGameInfo();
			return;
		case 3:
			this.doFirePet();
			return;
		case 4:
			Service.gI().getFlag(0, -1);
			InfoDlg.showWait();
			return;
		case 5:
			if (global::Char.myCharz().statusMe == 14)
			{
				GameCanvas.startOKDlg(mResources.can_not_do_when_die);
				return;
			}
			Service.gI().openUIZone();
			return;
		case 6:
			GameCanvas.endDlg();
			if (global::Char.myCharz().checkLuong() < 5)
			{
				GameCanvas.startOKDlg(mResources.not_enough_luong_world_channel);
				return;
			}
			if (this.chatTField == null)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = GameCanvas.panel;
			}
			this.chatTField.strChat = mResources.world_channel_5_luong;
			this.chatTField.tfChat.name = mResources.CHAT;
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.isFocus = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat2(this, string.Empty);
				return;
			}
			if (GameCanvas.isTouch)
			{
				this.chatTField.tfChat.doChangeToTextBox();
				return;
			}
			break;
		case 7:
			this.setTypeAccount();
			return;
		case 8:
			this.setTypeOption();
			return;
		case 9:
			GameCanvas.loginScr.backToRegister();
			return;
		case 10:
			if (GameCanvas.loginScr.isLogin2)
			{
				SoundMn.gI().backToRegister();
			}
			break;
		default:
			return;
		}
	}

	// Token: 0x060006F2 RID: 1778 RVA: 0x0006CEE4 File Offset: 0x0006B0E4
	internal void setTypeGameSubInfo()
	{
		string content = ((GameInfo)Panel.vGameInfo.elementAt(this.infoSelect)).content;
		Panel.contenInfo = mFont.tahoma_7_grey.splitFontArray(content, this.wScroll - 40);
		this.currentListLength = Panel.contenInfo.Length;
		this.ITEM_HEIGHT = 16;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.type = 24;
		this.setType(0);
	}

	// Token: 0x060006F3 RID: 1779 RVA: 0x0006CFC0 File Offset: 0x0006B1C0
	internal void setTypeGameInfo()
	{
		this.currentListLength = Panel.vGameInfo.size();
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.type = 23;
		this.setType(0);
	}

	// Token: 0x060006F4 RID: 1780 RVA: 0x0006D06B File Offset: 0x0006B26B
	internal void doFirePet()
	{
		InfoDlg.showWait();
		Service.gI().petInfo();
		this.timeShow = 20;
	}

	// Token: 0x060006F5 RID: 1781 RVA: 0x0006D084 File Offset: 0x0006B284
	internal void searchClan()
	{
		this.chatTField.strChat = mResources.input_clan_name;
		this.chatTField.tfChat.name = mResources.clan_name;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.isFocus = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x060006F6 RID: 1782 RVA: 0x0006D134 File Offset: 0x0006B334
	internal void chatClan()
	{
		this.chatTField.strChat = mResources.chat_clan;
		this.chatTField.tfChat.name = mResources.CHAT;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.isFocus = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x060006F7 RID: 1783 RVA: 0x0006D1E4 File Offset: 0x0006B3E4
	public void creatClan()
	{
		this.chatTField.strChat = mResources.input_clan_name_to_create;
		this.chatTField.tfChat.name = mResources.input_clan_name;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x060006F8 RID: 1784 RVA: 0x0006D284 File Offset: 0x0006B484
	public void putMoney()
	{
		if (this.chatTField == null)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		this.chatTField.strChat = mResources.input_money_to_trade;
		this.chatTField.tfChat.name = mResources.input_money;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
		this.chatTField.tfChat.setMaxTextLenght(10);
		if (GameCanvas.isTouch)
		{
			this.chatTField.tfChat.doChangeToTextBox();
		}
		if (Main.isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x060006F9 RID: 1785 RVA: 0x0006D3A0 File Offset: 0x0006B5A0
	public void putQuantily()
	{
		if (this.chatTField == null)
		{
			this.chatTField = new ChatTextField();
			this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
			this.chatTField.initChatTextField();
			this.chatTField.parentScreen = GameCanvas.panel;
		}
		this.chatTField.strChat = mResources.input_quantity_to_trade;
		this.chatTField.tfChat.name = mResources.input_quantity;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
		if (GameCanvas.isTouch)
		{
			this.chatTField.tfChat.doChangeToTextBox();
		}
		if (Main.isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x060006FA RID: 1786 RVA: 0x0006D4AC File Offset: 0x0006B6AC
	public void chagenSlogan()
	{
		this.chatTField.strChat = mResources.input_clan_slogan;
		this.chatTField.tfChat.name = mResources.input_clan_slogan;
		this.chatTField.to = string.Empty;
		this.chatTField.isShow = true;
		this.chatTField.tfChat.isFocus = true;
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
		if (Main.isWindowsPhone)
		{
			this.chatTField.tfChat.strInfo = this.chatTField.strChat;
		}
		if (!Main.isPC)
		{
			this.chatTField.startChat2(this, string.Empty);
		}
	}

	// Token: 0x060006FB RID: 1787 RVA: 0x0006D55C File Offset: 0x0006B75C
	public void changeIcon()
	{
		if (this.tabIcon == null)
		{
			this.tabIcon = new TabClanIcon();
		}
		this.tabIcon.text = this.chatTField.tfChat.getText();
		this.tabIcon.show(false);
		this.chatTField.isShow = false;
	}

	// Token: 0x060006FC RID: 1788 RVA: 0x0006D5B0 File Offset: 0x0006B7B0
	internal void addFriend(InfoItem info)
	{
		string text = "|0|1|" + info.charInfo.cName + "\n";
		string text2 = ((!info.isOnline) ? (text + "|3|1|" + mResources.is_offline) : (text + "|4|1|" + mResources.is_online)) + "\n--";
		text = string.Concat(new string[]
		{
			text2,
			"\n|5|",
			mResources.power,
			": ",
			info.s
		});
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.charInfo = info.charInfo;
		this.currItem = null;
	}

	// Token: 0x060006FD RID: 1789 RVA: 0x0006D670 File Offset: 0x0006B870
	internal void doFireEnemy()
	{
		if (this.selected >= 0 && this.vEnemy.size() != 0)
		{
			MyVector myVector = new MyVector();
			this.currInfoItem = this.selected;
			myVector.addElement(new Command(mResources.REVENGE, this, 10000, (InfoItem)this.vEnemy.elementAt(this.currInfoItem)));
			myVector.addElement(new Command(mResources.DELETE, this, 10001, (InfoItem)this.vEnemy.elementAt(this.currInfoItem)));
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addFriend((InfoItem)this.vEnemy.elementAt(this.selected));
		}
	}

	// Token: 0x060006FE RID: 1790 RVA: 0x0006D750 File Offset: 0x0006B950
	internal void doFireFriend()
	{
		if (this.selected >= 0 && this.vFriend.size() != 0)
		{
			MyVector myVector = new MyVector();
			this.currInfoItem = this.selected;
			myVector.addElement(new Command(mResources.CHAT, this, 8001, (InfoItem)this.vFriend.elementAt(this.currInfoItem)));
			myVector.addElement(new Command(mResources.DELETE, this, 8002, (InfoItem)this.vFriend.elementAt(this.currInfoItem)));
			myVector.addElement(new Command(mResources.den, this, 8004, (InfoItem)this.vFriend.elementAt(this.currInfoItem)));
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addFriend((InfoItem)this.vFriend.elementAt(this.selected));
		}
	}

	// Token: 0x060006FF RID: 1791 RVA: 0x0006D85C File Offset: 0x0006BA5C
	internal void doFireChangeFlag()
	{
		if (this.selected >= 0)
		{
			MyVector myVector = new MyVector();
			this.currInfoItem = this.selected;
			myVector.addElement(new Command(mResources.change_flag, this, 10030, null));
			myVector.addElement(new Command(mResources.BACK, this, 10031, null));
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
		}
	}

	// Token: 0x06000700 RID: 1792 RVA: 0x0006D8E0 File Offset: 0x0006BAE0
	internal void doFireLogMessage()
	{
		if (this.selected == 0)
		{
			this.isViewChatServer = !this.isViewChatServer;
			Rms.saveRMSInt("viewchat", this.isViewChatServer ? 1 : 0);
			if (GameCanvas.isTouch)
			{
				this.selected = -1;
				return;
			}
		}
		else if (this.selected >= 0 && this.logChat.size() != 0)
		{
			MyVector myVector = new MyVector();
			this.currInfoItem = this.selected - 1;
			myVector.addElement(new Command(mResources.CHAT, this, 8001, (InfoItem)this.logChat.elementAt(this.currInfoItem)));
			myVector.addElement(new Command(mResources.make_friend, this, 8003, (InfoItem)this.logChat.elementAt(this.currInfoItem)));
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addLogMessage((InfoItem)this.logChat.elementAt(this.selected - 1));
		}
	}

	// Token: 0x06000701 RID: 1793 RVA: 0x0006DA00 File Offset: 0x0006BC00
	internal void doFireClanOption()
	{
		try
		{
			this.partID = null;
			this.charInfo = null;
			Res.outz("cSelect= " + this.cSelected.ToString());
			if (this.selected < 0)
			{
				this.cSelected = -1;
			}
			else
			{
				if (global::Char.myCharz().clan == null)
				{
					if (this.selected == 0)
					{
						if (this.cSelected == 0)
						{
							this.searchClan();
						}
						else if (this.cSelected == 1)
						{
							InfoDlg.showWait();
							this.creatClan();
							Service.gI().getClan(1, -1, null);
						}
					}
					else if (this.selected != -1)
					{
						if (this.selected == 1)
						{
							if (this.isSearchClan)
							{
								Service.gI().searchClan(string.Empty);
							}
							else if (this.isViewMember && this.currClan != null)
							{
								GameCanvas.startYesNoDlg(mResources.do_u_want_join_clan + this.currClan.name, new Command(mResources.YES, this, 4000, this.currClan), new Command(mResources.NO, this, 4005, this.currClan));
							}
						}
						else if (this.isSearchClan)
						{
							this.currClan = this.getCurrClan();
							if (this.currClan != null)
							{
								MyVector myVector = new MyVector();
								myVector.addElement(new Command(mResources.request_join_clan, this, 4000, this.currClan));
								myVector.addElement(new Command(mResources.view_clan_member, this, 4001, this.currClan));
								GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
								this.addClanDetail(this.getCurrClan());
							}
						}
						else if (this.isViewMember)
						{
							this.currMem = this.getCurrMember();
							if (this.currMem != null)
							{
								MyVector myVector2 = new MyVector();
								myVector2.addElement(new Command(mResources.CLOSE, this, 8000, this.currClan));
								GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
								GameCanvas.menu.startAt(myVector2, 0, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
								this.addClanMemberDetail(this.currMem);
							}
						}
					}
				}
				else if (this.selected == 0)
				{
					if (this.isMessage)
					{
						if (this.cSelected == 0)
						{
							if (this.myMember.size() > 1)
							{
								this.chatClan();
							}
							else
							{
								this.member = null;
								this.isSearchClan = false;
								this.isViewMember = true;
								this.isMessage = false;
								this.currentListLength = this.myMember.size() + 2;
								this.initTabClans();
							}
						}
						if (this.cSelected == 1)
						{
							Service.gI().clanMessage(1, null, -1);
						}
						if (this.cSelected == 2)
						{
							this.member = null;
							this.isSearchClan = false;
							this.isViewMember = true;
							this.isMessage = false;
							this.currentListLength = this.myMember.size() + 2;
							this.initTabClans();
							this.getCurrClanOtion();
						}
					}
					else if (this.isViewMember)
					{
						if (this.cSelected == 0)
						{
							this.isSearchClan = false;
							this.isViewMember = false;
							this.isMessage = true;
							this.currentListLength = ClanMessage.vMessage.size() + 2;
							this.initTabClans();
						}
						if (this.cSelected == 1)
						{
							if (this.myMember.size() > 1)
							{
								Service.gI().leaveClan();
							}
							else
							{
								this.chagenSlogan();
							}
						}
						if (this.cSelected == 2)
						{
							if (this.myMember.size() > 1)
							{
								this.chagenSlogan();
							}
							else
							{
								Service.gI().getClan(3, -1, null);
							}
						}
						if (this.cSelected == 3)
						{
							Service.gI().getClan(3, -1, null);
						}
					}
				}
				else if (this.selected == 1)
				{
					if (this.isSearchClan)
					{
						Service.gI().searchClan(string.Empty);
					}
				}
				else if (this.isSearchClan)
				{
					this.currClan = this.getCurrClan();
					if (this.currClan != null)
					{
						MyVector myVector3 = new MyVector();
						myVector3.addElement(new Command(mResources.view_clan_member, this, 4001, this.currClan));
						GameCanvas.menu.startAt(myVector3, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
						this.addClanDetail(this.getCurrClan());
					}
				}
				else if (this.isViewMember)
				{
					Res.outz("TOI DAY 1");
					this.currMem = this.getCurrMember();
					if (this.currMem != null)
					{
						MyVector myVector4 = new MyVector();
						Res.outz("TOI DAY 2");
						if (this.member != null)
						{
							myVector4.addElement(new Command(mResources.CLOSE, this, 8000, null));
							Res.outz("TOI DAY 3");
						}
						else if (this.myMember != null)
						{
							Res.outz("TOI DAY 4");
							Res.outz("my role= " + global::Char.myCharz().role.ToString());
							if (global::Char.myCharz().charID == this.currMem.ID || global::Char.myCharz().role == 2)
							{
								myVector4.addElement(new Command(mResources.CLOSE, this, 8000, this.currMem));
							}
							if (global::Char.myCharz().role < 2 && global::Char.myCharz().charID != this.currMem.ID)
							{
								Res.outz("TOI DAY");
								if (this.currMem.role == 0 || this.currMem.role == 1)
								{
									myVector4.addElement(new Command(mResources.CLOSE, this, 8000, this.currMem));
								}
								if (this.currMem.role == 2)
								{
									myVector4.addElement(new Command(mResources.create_clan_co_leader, this, 5002, this.currMem));
								}
								if (global::Char.myCharz().role == 0)
								{
									myVector4.addElement(new Command(mResources.create_clan_leader, this, 5001, this.currMem));
									if (this.currMem.role == 1)
									{
										myVector4.addElement(new Command(mResources.disable_clan_mastership, this, 5003, this.currMem));
									}
								}
							}
							if (global::Char.myCharz().role < this.currMem.role)
							{
								myVector4.addElement(new Command(mResources.kick_clan_mem, this, 5004, this.currMem));
							}
						}
						GameCanvas.menu.startAt(myVector4, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
						this.addClanMemberDetail(this.currMem);
					}
				}
				else if (this.isMessage)
				{
					this.currMess = this.getCurrMessage();
					if (this.currMess != null)
					{
						if (this.currMess.type == 0)
						{
							MyVector myVector5 = new MyVector();
							myVector5.addElement(new Command(mResources.CLOSE, this, 8000, this.currMess));
							GameCanvas.menu.startAt(myVector5, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
							this.addMessageDetail(this.currMess);
						}
						else if (this.currMess.type == 1)
						{
							if (this.currMess.playerId != global::Char.myCharz().charID && this.cSelected != -1)
							{
								Service.gI().clanDonate(this.currMess.id);
							}
						}
						else if (this.currMess.type == 2 && this.currMess.option != null)
						{
							if (this.cSelected == 0)
							{
								Service.gI().joinClan(this.currMess.id, 1);
							}
							else if (this.cSelected == 1)
							{
								Service.gI().joinClan(this.currMess.id, 0);
							}
						}
					}
				}
				if (GameCanvas.isTouch)
				{
					this.cSelected = -1;
					this.selected = -1;
				}
			}
		}
		catch (Exception)
		{
			throw;
		}
	}

	// Token: 0x06000702 RID: 1794 RVA: 0x0006E240 File Offset: 0x0006C440
	internal void doFireMain()
	{
		try
		{
			if (this.currentTabIndex == 0)
			{
				this.setTypeMap();
			}
			if (this.currentTabIndex == 1)
			{
				this.doFireInventory();
			}
			if (this.currentTabIndex == 2)
			{
				this.doFireSkill();
			}
			if (this.currentTabIndex == 3)
			{
				if (this.mainTabName.Length == 4)
				{
					this.doFireTool();
				}
				else
				{
					this.doFireClanOption();
				}
			}
			if (this.currentTabIndex == 4)
			{
				this.doFireTool();
			}
		}
		catch (Exception ex)
		{
			Res.outz("Throw ex " + ex.StackTrace);
		}
	}

	// Token: 0x06000703 RID: 1795 RVA: 0x0006E2D4 File Offset: 0x0006C4D4
	internal void doFireSkill()
	{
		if (this.selected < 0)
		{
			return;
		}
		if (global::Char.myCharz().statusMe == 14)
		{
			GameCanvas.startOKDlg(mResources.can_not_do_when_die);
			return;
		}
		if (this.selected != 0 && this.selected != 1 && this.selected != 2 && this.selected != 3 && this.selected != 4 && this.selected != 5)
		{
			int num = this.selected - 6;
			SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[num];
			Skill skill = global::Char.myCharz().getSkill(skillTemplate);
			Skill skill2 = null;
			MyVector myVector = new MyVector(string.Empty);
			if (skill != null)
			{
				if (skill.point == skillTemplate.maxPoint)
				{
					myVector.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
					myVector.addElement(new Command(mResources.CLOSE, 2));
				}
				else
				{
					skill2 = skillTemplate.skills[skill.point];
					myVector.addElement(new Command(mResources.UPGRADE, this, 9002, skill2));
					myVector.addElement(new Command(mResources.make_shortcut, this, 9003, skill.template));
				}
			}
			else
			{
				skill2 = skillTemplate.skills[0];
				myVector.addElement(new Command(mResources.learn, this, 9004, skill2));
			}
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addSkillDetail(skillTemplate, skill, skill2);
			return;
		}
		long cTiemNang = global::Char.myCharz().cTiemNang;
		int cHPGoc = global::Char.myCharz().cHPGoc;
		int cMPGoc = global::Char.myCharz().cMPGoc;
		int cDamGoc = global::Char.myCharz().cDamGoc;
		int cDefGoc = global::Char.myCharz().cDefGoc;
		int cCriticalGoc = global::Char.myCharz().cCriticalGoc;
		int num2 = 1000;
		if (this.selected == 0)
		{
			if (cTiemNang < (long)(global::Char.myCharz().cHPGoc + num2))
			{
				GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + global::Char.myCharz().cTiemNang.ToString() + mResources.not_enough_potential_point2 + (global::Char.myCharz().cHPGoc + num2).ToString(), false);
				return;
			}
			if (cTiemNang > (long)cHPGoc && cTiemNang < (long)(10 * (2 * (cHPGoc + num2) + 180) / 2))
			{
				GameCanvas.startYesNoDlg(string.Concat(new string[]
				{
					mResources.use_potential_point_for1,
					(cHPGoc + num2).ToString(),
					mResources.use_potential_point_for2,
					global::Char.myCharz().hpFrom1000TiemNang.ToString(),
					mResources.for_HP
				}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
				return;
			}
			if (cTiemNang >= (long)(10 * (2 * (cHPGoc + num2) + 180) / 2) && cTiemNang < (long)(100 * (2 * (cHPGoc + num2) + 1980) / 2))
			{
				MyVector myVector2 = new MyVector(string.Empty);
				myVector2.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					global::Char.myCharz().hpFrom1000TiemNang.ToString(),
					mResources.HP,
					"\n-",
					Res.formatNumber2((long)(cHPGoc + num2))
				}), this, 9000, null));
				myVector2.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					((int)(10 * global::Char.myCharz().hpFrom1000TiemNang)).ToString(),
					mResources.HP,
					"\n-",
					Res.formatNumber2((long)(10 * (2 * (cHPGoc + num2) + 180) / 2))
				}), this, 9006, null));
				GameCanvas.menu.startAt(myVector2, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected);
			}
			if (cTiemNang >= (long)(100 * (2 * (cHPGoc + num2) + 1980) / 2))
			{
				MyVector myVector3 = new MyVector(string.Empty);
				myVector3.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					global::Char.myCharz().hpFrom1000TiemNang.ToString(),
					mResources.HP,
					"\n-",
					Res.formatNumber2((long)(cHPGoc + num2))
				}), this, 9000, null));
				myVector3.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					((int)(10 * global::Char.myCharz().hpFrom1000TiemNang)).ToString(),
					mResources.HP,
					"\n-",
					Res.formatNumber2((long)(10 * (2 * (cHPGoc + num2) + 180) / 2))
				}), this, 9006, null));
				myVector3.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					((int)(100 * global::Char.myCharz().hpFrom1000TiemNang)).ToString(),
					mResources.HP,
					"\n-",
					Res.formatNumber2((long)(100 * (2 * (cHPGoc + num2) + 1980) / 2))
				}), this, 9007, null));
				GameCanvas.menu.startAt(myVector3, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected);
			}
		}
		if (this.selected == 1)
		{
			if (global::Char.myCharz().cTiemNang < (long)(global::Char.myCharz().cMPGoc + num2))
			{
				GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + global::Char.myCharz().cTiemNang.ToString() + mResources.not_enough_potential_point2 + (global::Char.myCharz().cMPGoc + num2).ToString());
				return;
			}
			if (cTiemNang > (long)cMPGoc && cTiemNang < (long)(10 * (2 * (cMPGoc + num2) + 180) / 2))
			{
				GameCanvas.startYesNoDlg(string.Concat(new string[]
				{
					mResources.use_potential_point_for1,
					(cMPGoc + num2).ToString(),
					mResources.use_potential_point_for2,
					global::Char.myCharz().mpFrom1000TiemNang.ToString(),
					mResources.for_KI
				}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
				return;
			}
			if (cTiemNang >= (long)(10 * (2 * (cMPGoc + num2) + 180) / 2) && cTiemNang < (long)(100 * (2 * (cMPGoc + num2) + 1980) / 2))
			{
				MyVector myVector4 = new MyVector(string.Empty);
				myVector4.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					global::Char.myCharz().mpFrom1000TiemNang.ToString(),
					mResources.KI,
					"\n-",
					Res.formatNumber2((long)(cHPGoc + num2))
				}), this, 9000, null));
				myVector4.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					((int)(10 * global::Char.myCharz().mpFrom1000TiemNang)).ToString(),
					mResources.KI,
					"\n-",
					Res.formatNumber2((long)(10 * (2 * (cHPGoc + num2) + 180) / 2))
				}), this, 9006, null));
				GameCanvas.menu.startAt(myVector4, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected);
			}
			if (cTiemNang >= (long)(100 * (2 * (cMPGoc + num2) + 1980) / 2))
			{
				MyVector myVector5 = new MyVector(string.Empty);
				myVector5.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					global::Char.myCharz().mpFrom1000TiemNang.ToString(),
					mResources.KI,
					"\n-",
					Res.formatNumber2((long)(cMPGoc + num2))
				}), this, 9000, null));
				myVector5.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					((int)(10 * global::Char.myCharz().mpFrom1000TiemNang)).ToString(),
					mResources.KI,
					"\n-",
					Res.formatNumber2((long)(10 * (2 * (cMPGoc + num2) + 180) / 2))
				}), this, 9006, null));
				myVector5.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					((int)(100 * global::Char.myCharz().mpFrom1000TiemNang)).ToString(),
					mResources.KI,
					"\n-",
					Res.formatNumber2((long)(100 * (2 * (cMPGoc + num2) + 1980) / 2))
				}), this, 9007, null));
				GameCanvas.menu.startAt(myVector5, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected);
			}
		}
		if (this.selected == 2)
		{
			if (global::Char.myCharz().cTiemNang < (long)(global::Char.myCharz().cDamGoc * (int)global::Char.myCharz().expForOneAdd))
			{
				GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + global::Char.myCharz().cTiemNang.ToString() + mResources.not_enough_potential_point2 + (cDamGoc * 100).ToString());
				return;
			}
			if (cTiemNang > (long)cDamGoc && cTiemNang < (long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd))
			{
				GameCanvas.startYesNoDlg(string.Concat(new string[]
				{
					mResources.use_potential_point_for1,
					(cDamGoc * 100).ToString(),
					mResources.use_potential_point_for2,
					global::Char.myCharz().damFrom1000TiemNang.ToString(),
					mResources.for_hit_point
				}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
				return;
			}
			if (cTiemNang >= (long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd) && cTiemNang < (long)(100 * (2 * cDamGoc + 99) / 2 * (int)global::Char.myCharz().expForOneAdd))
			{
				MyVector myVector6 = new MyVector(string.Empty);
				myVector6.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					global::Char.myCharz().damFrom1000TiemNang.ToString(),
					"\n",
					mResources.hit_point,
					"\n-",
					Res.formatNumber2((long)(cDamGoc * 100))
				}), this, 9000, null));
				myVector6.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					((int)(10 * global::Char.myCharz().damFrom1000TiemNang)).ToString(),
					"\n",
					mResources.hit_point,
					"\n-",
					Res.formatNumber2((long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd))
				}), this, 9006, null));
				GameCanvas.menu.startAt(myVector6, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected);
			}
			if (cTiemNang >= (long)(100 * (2 * cDamGoc + 99) / 2 * (int)global::Char.myCharz().expForOneAdd))
			{
				MyVector myVector7 = new MyVector(string.Empty);
				myVector7.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					global::Char.myCharz().damFrom1000TiemNang.ToString(),
					"\n",
					mResources.hit_point,
					"\n-",
					Res.formatNumber2((long)(cDamGoc * 100))
				}), this, 9000, null));
				myVector7.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					((int)(10 * global::Char.myCharz().damFrom1000TiemNang)).ToString(),
					"\n",
					mResources.hit_point,
					"\n-",
					Res.formatNumber2((long)(10 * (2 * cDamGoc + 9) / 2 * (int)global::Char.myCharz().expForOneAdd))
				}), this, 9006, null));
				myVector7.addElement(new Command(string.Concat(new string[]
				{
					mResources.increase_upper,
					"\n",
					((int)(100 * global::Char.myCharz().damFrom1000TiemNang)).ToString(),
					"\n",
					mResources.hit_point,
					"\n-",
					Res.formatNumber2((long)(100 * (2 * cDamGoc + 99) / 2 * (int)global::Char.myCharz().expForOneAdd))
				}), this, 9007, null));
				GameCanvas.menu.startAt(myVector7, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
				this.addSkillDetail2(this.selected);
			}
		}
		if (this.selected == 3)
		{
			if (global::Char.myCharz().cTiemNang < (long)(50000 + global::Char.myCharz().cDefGoc * 1000))
			{
				GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + NinjaUtil.getMoneys(global::Char.myCharz().cTiemNang) + mResources.not_enough_potential_point2 + NinjaUtil.getMoneys((long)(50000 + global::Char.myCharz().cDefGoc * 1000)));
				return;
			}
			long num3 = (long)(2 * (cDefGoc + 5)) / 2L * 100000L;
			long num4 = 10L * (long)(2 * (cDefGoc + 5) + 9) / 2L * 100000L;
			long num5 = 100L * (long)(2 * (cDefGoc + 5) + 99) / 2L * 100000L;
			mResources.use_potential_point_for1 = mResources.increase_upper;
			MyVector myVector8 = new MyVector(string.Empty);
			myVector8.addElement(new Command(string.Concat(new string[]
			{
				mResources.use_potential_point_for1,
				"\n1 ",
				mResources.armor,
				"\n",
				Res.formatNumber2(num3)
			}), this, 9000, null));
			myVector8.addElement(new Command(string.Concat(new string[]
			{
				mResources.use_potential_point_for1,
				"\n10 ",
				mResources.armor,
				"\n",
				Res.formatNumber2(num4)
			}), this, 9006, null));
			myVector8.addElement(new Command(string.Concat(new string[]
			{
				mResources.use_potential_point_for1,
				"\n100 ",
				mResources.armor,
				"\n",
				Res.formatNumber2(num5)
			}), this, 9007, null));
			GameCanvas.menu.startAt(myVector8, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addSkillDetail2(this.selected);
			return;
		}
		else
		{
			if (this.selected != 4)
			{
				if (this.selected == 5)
				{
					Service.gI().speacialSkill(0);
				}
				return;
			}
			int num6 = global::Char.myCharz().cCriticalGoc;
			if (num6 > Panel.t_tiemnang.Length - 1)
			{
				num6 = Panel.t_tiemnang.Length - 1;
			}
			long num7 = Panel.t_tiemnang[num6];
			if (global::Char.myCharz().cTiemNang < num7)
			{
				GameCanvas.startOKDlg(mResources.not_enough_potential_point1 + Res.formatNumber2(global::Char.myCharz().cTiemNang) + mResources.not_enough_potential_point2 + Res.formatNumber2(num7));
				return;
			}
			GameCanvas.startYesNoDlg(string.Concat(new string[]
			{
				mResources.use_potential_point_for1,
				Res.formatNumber(num7),
				mResources.use_potential_point_for2,
				global::Char.myCharz().criticalFrom1000Tiemnang.ToString(),
				mResources.for_crit
			}), new Command(mResources.increase_upper, this, 9000, null), new Command(mResources.CANCEL, this, 4007, null));
			return;
		}
	}

	// Token: 0x06000704 RID: 1796 RVA: 0x0006F354 File Offset: 0x0006D554
	internal void addLogMessage(InfoItem info)
	{
		string text = "|0|1|" + info.charInfo.cName + "\n" + "\n--" + "\n|5|" + Res.split(info.s, "|", 0)[2];
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
		this.charInfo = info.charInfo;
		this.currItem = null;
	}

	// Token: 0x06000705 RID: 1797 RVA: 0x0006F3D4 File Offset: 0x0006D5D4
	internal void addSkillDetail2(int type)
	{
		string text = string.Empty;
		int num = 0;
		if (this.selected == 0)
		{
			num = global::Char.myCharz().cHPGoc + 1000;
		}
		if (this.selected == 1)
		{
			num = global::Char.myCharz().cMPGoc + 1000;
		}
		if (this.selected == 2)
		{
			num = global::Char.myCharz().cDamGoc * (int)global::Char.myCharz().expForOneAdd;
		}
		if (this.selected == 3)
		{
			num = 500000 + global::Char.myCharz().cDefGoc * 100000;
		}
		string text2 = text;
		text = string.Concat(new string[]
		{
			text2,
			"|5|2|",
			mResources.USE,
			" ",
			num.ToString(),
			" ",
			mResources.potential
		});
		if (type == 0)
		{
			text = text + "\n|5|2|" + mResources.to_gain_20hp;
		}
		if (type == 1)
		{
			text = text + "\n|5|2|" + mResources.to_gain_20mp;
		}
		if (type == 2)
		{
			text = text + "\n|5|2|" + mResources.to_gain_1pow;
		}
		if (type == 3)
		{
			text = text + "\n|5|2|" + mResources.to_gain_1pow;
		}
		this.currItem = null;
		this.partID = null;
		this.charInfo = null;
		this.idIcon = -1;
		this.cp = new ChatPopup();
		this.popUpDetailInit(this.cp, text);
	}

	// Token: 0x06000706 RID: 1798 RVA: 0x00004887 File Offset: 0x00002A87
	internal void doFireClanIcon()
	{
	}

	// Token: 0x06000707 RID: 1799 RVA: 0x0006F528 File Offset: 0x0006D728
	internal void doFireMap()
	{
		if (Panel.imgMap != null)
		{
			Panel.imgMap.texture = null;
			Panel.imgMap = null;
		}
		TileMap.lastPlanetId = -1;
		mSystem.gcc();
		SmallImage.loadBigRMS();
		this.setTypeMain();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000708 RID: 1800 RVA: 0x0006F573 File Offset: 0x0006D773
	internal void doFireZone()
	{
		if (this.selected != -1)
		{
			Res.outz("FIRE ZONE");
			this.isChangeZone = true;
			GameCanvas.panel.hide();
		}
	}

	// Token: 0x06000709 RID: 1801 RVA: 0x0006F59C File Offset: 0x0006D79C
	public void updateRequest(int recieve, int maxCap)
	{
		this.cp.says[this.cp.says.Length - 1] = string.Concat(new string[]
		{
			mResources.received,
			" ",
			recieve.ToString(),
			"/",
			maxCap.ToString()
		});
	}

	// Token: 0x0600070A RID: 1802 RVA: 0x0006F5FC File Offset: 0x0006D7FC
	internal void doFireBox()
	{
		if (this.selected < 0)
		{
			return;
		}
		this.currItem = null;
		MyVector myVector = new MyVector();
		if (this.currentTabIndex == 0 && !this.Equals(GameCanvas.panel2))
		{
			if (this.selected == 0)
			{
				this.setNewSelected(global::Char.myCharz().arrItemBox.Length, false);
			}
			else
			{
				sbyte b = (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected);
				Item item = global::Char.myCharz().arrItemBox[(int)b];
				if (item != null)
				{
					if (this.isBoxClan)
					{
						myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
						myVector.addElement(new Command(mResources.USE, this, 2010, item));
					}
					else if (item.isTypeBody())
					{
						myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
					}
					else
					{
						myVector.addElement(new Command(mResources.GETOUT, this, 1000, item));
					}
					this.currItem = item;
				}
			}
		}
		if (this.currentTabIndex == 1 || this.Equals(GameCanvas.panel2))
		{
			if (this.selected == 0)
			{
				this.setNewSelected(global::Char.myCharz().arrItemBody.Length + global::Char.myCharz().arrItemBag.Length, true);
			}
			else
			{
				Item[] arrItemBody = global::Char.myCharz().arrItemBody;
				if (!this.GetInventorySelect_isbody(this.selected, this.newSelected, arrItemBody))
				{
					sbyte b2 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, arrItemBody);
					Item item2 = global::Char.myCharz().arrItemBag[(int)b2];
					if (item2 != null)
					{
						myVector.addElement(new Command(mResources.move_to_chest, this, 1001, item2));
						if (item2.isTypeBody())
						{
							myVector.addElement(new Command(mResources.USE, this, 2000, item2));
						}
						else
						{
							myVector.addElement(new Command(mResources.USE, this, 2001, item2));
						}
						this.currItem = item2;
					}
				}
				else
				{
					Item item3 = global::Char.myCharz().arrItemBody[this.GetInventorySelect_body(this.selected, this.newSelected)];
					if (item3 != null)
					{
						myVector.addElement(new Command(mResources.move_to_chest2, this, 1002, item3));
						this.currItem = item3;
					}
				}
			}
		}
		if (this.currItem != null)
		{
			global::Char.myCharz().setPartTemp(this.currItem.headTemp, this.currItem.bodyTemp, this.currItem.legTemp, this.currItem.bagTemp);
			if (this.isBoxClan)
			{
				myVector.addElement(new Command(mResources.MOVEOUT, this, 2011, this.currItem));
			}
			GameCanvas.menu.startAt(myVector, this.X, (this.selected + 1) * this.ITEM_HEIGHT - this.cmy + this.yScroll);
			this.addItemDetail(this.currItem);
		}
		else
		{
			this.cp = null;
		}
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
	}

	// Token: 0x0600070B RID: 1803 RVA: 0x0006F8F0 File Offset: 0x0006DAF0
	public void itemRequest(sbyte itemAction, string info, sbyte where, sbyte index)
	{
		GameCanvas.endDlg();
		ItemObject itemObject = new ItemObject();
		itemObject.type = (int)itemAction;
		itemObject.id = (int)index;
		itemObject.where = (int)where;
		GameCanvas.startYesNoDlg(info, new Command(mResources.YES, this, 2004, itemObject), new Command(mResources.NO, this, 4005, null));
	}

	// Token: 0x0600070C RID: 1804 RVA: 0x0006F948 File Offset: 0x0006DB48
	public void saleRequest(sbyte type, string info, short id)
	{
		ItemObject itemObject = new ItemObject();
		itemObject.type = (int)type;
		itemObject.id = (int)id;
		GameCanvas.startYesNoDlg(info, new Command(mResources.YES, this, 3003, itemObject), new Command(mResources.NO, this, 4005, null));
	}

	// Token: 0x0600070D RID: 1805 RVA: 0x0006F994 File Offset: 0x0006DB94
	public void perform(int idAction, object p)
	{
		if (idAction == 9999)
		{
			TopInfo topInfo = (TopInfo)p;
			Service.gI().sendThachDau(topInfo.pId);
		}
		if (idAction == 170391)
		{
			Rms.clearAll();
			if (mGraphics.zoomLevel > 1)
			{
				Rms.saveRMSInt("levelScreenKN", 1);
			}
			else
			{
				Rms.saveRMSInt("levelScreenKN", 0);
			}
			GameMidlet.instance.exit();
		}
		if (idAction == 6001)
		{
			Item item = (Item)p;
			item.isSelect = false;
			GameCanvas.panel.vItemCombine.removeElement(item);
			if (GameCanvas.panel.currentTabIndex == 0)
			{
				GameCanvas.panel.setTabCombine();
			}
		}
		if (idAction == 6000)
		{
			Item item2 = (Item)p;
			for (int i = 0; i < GameCanvas.panel.vItemCombine.size(); i++)
			{
				if (((Item)GameCanvas.panel.vItemCombine.elementAt(i)).template.id == item2.template.id)
				{
					GameCanvas.startOKDlg(mResources.already_has_item);
					return;
				}
			}
			item2.isSelect = true;
			GameCanvas.panel.vItemCombine.addElement(item2);
			if (GameCanvas.panel.currentTabIndex == 0)
			{
				GameCanvas.panel.setTabCombine();
			}
		}
		if (idAction == 7000)
		{
			if (this.isLock)
			{
				GameCanvas.startOKDlg(mResources.unlock_item_to_trade);
				return;
			}
			Item item3 = (Item)p;
			for (int j = 0; j < GameCanvas.panel.vMyGD.size(); j++)
			{
				if (((Item)GameCanvas.panel.vMyGD.elementAt(j)).indexUI == item3.indexUI)
				{
					GameCanvas.startOKDlg(mResources.already_has_item);
					return;
				}
			}
			if (item3.quantity > 1)
			{
				this.putQuantily();
				return;
			}
			item3.isSelect = true;
			Item item4 = new Item();
			item4.template = item3.template;
			item4.itemOption = item3.itemOption;
			item4.indexUI = item3.indexUI;
			GameCanvas.panel.vMyGD.addElement(item4);
			Service.gI().giaodich(2, -1, (sbyte)item4.indexUI, item4.quantity);
		}
		if (idAction == 7001)
		{
			Item item5 = (Item)p;
			item5.isSelect = false;
			GameCanvas.panel.vMyGD.removeElement(item5);
			if (GameCanvas.panel.currentTabIndex == 1)
			{
				GameCanvas.panel.setTabGiaoDich(true);
			}
			Service.gI().giaodich(4, -1, (sbyte)item5.indexUI, -1);
		}
		if (idAction == 7002)
		{
			this.isAccept = true;
			GameCanvas.endDlg();
			Service.gI().giaodich(7, -1, -1, -1);
			this.hide();
		}
		if (idAction == 8003)
		{
			InfoItem infoItem = (InfoItem)p;
			Service.gI().friend(1, infoItem.charInfo.charID);
			int num = this.type;
		}
		if (idAction == 8002)
		{
			InfoItem infoItem2 = (InfoItem)p;
			Service.gI().friend(2, infoItem2.charInfo.charID);
		}
		if (idAction == 8004)
		{
			InfoItem infoItem3 = (InfoItem)p;
			Service.gI().gotoPlayer(infoItem3.charInfo.charID);
		}
		if (idAction == 8001)
		{
			Res.outz("chat player");
			InfoItem infoItem4 = (InfoItem)p;
			if (this.chatTField == null)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = GameCanvas.panel;
			}
			this.chatTField.strChat = mResources.chat_player;
			this.chatTField.tfChat.name = mResources.chat_with + " " + infoItem4.charInfo.cName;
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.isFocus = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
		}
		if (idAction == 1000)
		{
			Service.gI().getItem(Panel.BOX_BAG, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
		}
		if (idAction == 1001)
		{
			sbyte b = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			Service.gI().getItem(Panel.BAG_BOX, b);
		}
		if (idAction == 1003)
		{
			this.hide();
		}
		if (idAction == 1002)
		{
			Service.gI().getItem(Panel.BODY_BOX, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
		}
		if (idAction == 2011)
		{
			Service.gI().useItem(1, 2, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected), -1);
		}
		if (idAction == 2010)
		{
			Service.gI().useItem(0, 2, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected), -1);
			Item item6 = (Item)p;
			if (item6 != null && (item6.template.id == 193 || item6.template.id == 194))
			{
				GameCanvas.panel.hide();
			}
		}
		if (idAction == 2000)
		{
			Item[] arrItemBody = global::Char.myCharz().arrItemBody;
			sbyte b2 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, arrItemBody);
			if (this.isnewInventory)
			{
				b2 = (sbyte)this.currItem.indexUI;
			}
			Service.gI().getItem(Panel.BAG_BODY, b2);
		}
		if (idAction == 2001)
		{
			Res.outz("use item");
			Item item7 = (Item)p;
			bool inventorySelect_isbody = this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			sbyte b3 = (inventorySelect_isbody ? ((sbyte)this.GetInventorySelect_body(this.selected, this.newSelected)) : ((sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody)));
			if (this.isnewInventory)
			{
				b3 = (sbyte)this.currItem.indexUI;
				sbyte b4 = 0;
				if (this.newSelected != 0)
				{
					b4 = 1;
				}
				Service.gI().useItem(0, b4, b3, -1);
			}
			else
			{
				Service.gI().useItem(0, (!inventorySelect_isbody) ? 1 : 0, b3, -1);
			}
			if (item7.template.id == 193 || item7.template.id == 194)
			{
				GameCanvas.panel.hide();
			}
		}
		if (idAction == 2002)
		{
			if (this.isnewInventory)
			{
				Service.gI().getItem(Panel.BODY_BAG, (sbyte)this.sellectInventory);
			}
			else
			{
				Service.gI().getItem(Panel.BODY_BAG, (sbyte)this.GetInventorySelect_body(this.selected, this.newSelected));
			}
		}
		if (idAction == 2003)
		{
			Res.outz("remove item");
			bool inventorySelect_isbody2 = this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			sbyte b5 = (inventorySelect_isbody2 ? ((sbyte)this.GetInventorySelect_body(this.selected, this.newSelected)) : ((sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody)));
			Service.gI().useItem(1, (!inventorySelect_isbody2) ? 1 : 0, b5, -1);
		}
		if (idAction == 2004)
		{
			GameCanvas.endDlg();
			ItemObject itemObject = (ItemObject)p;
			sbyte b6 = (sbyte)itemObject.where;
			sbyte b7 = (sbyte)itemObject.id;
			Service.gI().useItem((itemObject.type != 0) ? 2 : 3, b6, b7, -1);
		}
		if (idAction == 2005)
		{
			sbyte b8 = (sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			Service.gI().getItem(Panel.BAG_PET, b8);
		}
		if (idAction == 2006)
		{
			Item[] arrItemBody2 = global::Char.myPetz().arrItemBody;
			sbyte b9 = (sbyte)this.selected;
			Service.gI().getItem(Panel.PET_BAG, b9);
		}
		if (idAction == 30001)
		{
			Res.outz("nhan do");
			Service.gI().buyItem(0, this.selected, 0);
		}
		if (idAction == 30002)
		{
			Res.outz("xoa do");
			Service.gI().buyItem(1, this.selected, 0);
		}
		if (idAction == 30003)
		{
			Res.outz("nhan tat");
			Service.gI().buyItem(2, this.selected, 0);
		}
		if (idAction == 3000)
		{
			Res.outz("mua do");
			Item item8 = (Item)p;
			Service.gI().buyItem(0, (int)item8.template.id, 0);
		}
		if (idAction == 3001)
		{
			Item item9 = (Item)p;
			GameCanvas.msgdlg.pleasewait();
			Service.gI().buyItem(1, (int)item9.template.id, 0);
		}
		if (idAction == 3002)
		{
			GameCanvas.endDlg();
			bool inventorySelect_isbody3 = this.GetInventorySelect_isbody(this.selected, this.newSelected, global::Char.myCharz().arrItemBody);
			sbyte b10 = (inventorySelect_isbody3 ? ((sbyte)this.GetInventorySelect_body(this.selected, this.newSelected)) : ((sbyte)this.GetInventorySelect_bag(this.selected, this.newSelected, global::Char.myCharz().arrItemBody)));
			Service.gI().saleItem(0, (!inventorySelect_isbody3) ? 1 : 0, (short)b10);
		}
		if (idAction == 3003)
		{
			GameCanvas.endDlg();
			ItemObject itemObject2 = (ItemObject)p;
			Service.gI().saleItem(1, (sbyte)itemObject2.type, (short)itemObject2.id);
		}
		if (idAction == 3004)
		{
			Item item10 = (Item)p;
			Service.gI().buyItem(3, (int)item10.template.id, 0);
		}
		if (idAction == 3005)
		{
			Res.outz("mua do");
			Item item11 = (Item)p;
			Service.gI().buyItem(3, (int)item11.template.id, 0);
		}
		if (idAction == 4000)
		{
			Clan clan = (Clan)p;
			if (clan != null)
			{
				GameCanvas.endDlg();
				Service.gI().clanMessage(2, null, clan.ID);
			}
		}
		if (idAction == 4001)
		{
			Clan clan2 = (Clan)p;
			if (clan2 != null)
			{
				InfoDlg.showWait();
				this.clanReport = mResources.PLEASEWAIT;
				Service.gI().clanMember(clan2.ID);
			}
		}
		if (idAction == 4005)
		{
			GameCanvas.endDlg();
		}
		if (idAction == 4007)
		{
			GameCanvas.endDlg();
		}
		if (idAction == 4006)
		{
			ClanMessage clanMessage = (ClanMessage)p;
			Service.gI().clanDonate(clanMessage.id);
		}
		if (idAction == 5001)
		{
			Member member = (Member)p;
			Service.gI().clanRemote(member.ID, 0);
		}
		if (idAction == 5002)
		{
			Member member2 = (Member)p;
			Service.gI().clanRemote(member2.ID, 1);
		}
		if (idAction == 5003)
		{
			Member member3 = (Member)p;
			Service.gI().clanRemote(member3.ID, 2);
		}
		if (idAction == 5004)
		{
			Member member4 = (Member)p;
			Service.gI().clanRemote(member4.ID, -1);
		}
		if (idAction == 9000)
		{
			Service.gI().upPotential(this.selected, 1);
			GameCanvas.endDlg();
			InfoDlg.showWait();
		}
		if (idAction == 9006)
		{
			Service.gI().upPotential(this.selected, 10);
			GameCanvas.endDlg();
			InfoDlg.showWait();
		}
		if (idAction == 9007)
		{
			Service.gI().upPotential(this.selected, 100);
			GameCanvas.endDlg();
			InfoDlg.showWait();
		}
		if (idAction == 9002)
		{
			Skill skill = (Skill)p;
			if (skill.template.isSkillSpec())
			{
				GameCanvas.startOKDlg(mResources.updSkill);
			}
			else
			{
				GameCanvas.startOKDlg(string.Concat(new string[]
				{
					mResources.can_buy_from_Uron1,
					skill.powRequire.ToString(),
					mResources.can_buy_from_Uron2,
					skill.moreInfo,
					mResources.can_buy_from_Uron3
				}));
			}
		}
		if (idAction == 9003)
		{
			if (GameCanvas.isTouch && !Main.isPC)
			{
				GameScr.gI().doSetOnScreenSkill((SkillTemplate)p);
			}
			else
			{
				GameScr.gI().doSetKeySkill((SkillTemplate)p);
			}
		}
		if (idAction == 9004)
		{
			Skill skill2 = (Skill)p;
			if (skill2.template.isSkillSpec())
			{
				GameCanvas.startOKDlg(mResources.learnSkill);
			}
			else
			{
				GameCanvas.startOKDlg(string.Concat(new string[]
				{
					mResources.can_buy_from_Uron1,
					skill2.powRequire.ToString(),
					mResources.can_buy_from_Uron2,
					skill2.moreInfo,
					mResources.can_buy_from_Uron3
				}));
			}
		}
		if (idAction == 10000)
		{
			InfoItem infoItem5 = (InfoItem)p;
			Service.gI().enemy(1, infoItem5.charInfo.charID);
			GameCanvas.panel.hideNow();
		}
		if (idAction == 10001)
		{
			InfoItem infoItem6 = (InfoItem)p;
			Service.gI().enemy(2, infoItem6.charInfo.charID);
			InfoDlg.showWait();
		}
		if (idAction == 10012)
		{
			if (this.chatTField == null)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = ((GameCanvas.panel2 != null) ? GameCanvas.panel2 : GameCanvas.panel);
			}
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.chatTField.tfChat.setText(string.Empty);
			if (this.currItem.quantity == 1)
			{
				this.chatTField.strChat = mResources.kiguiXuchat;
				this.chatTField.tfChat.name = mResources.input_money;
			}
			else
			{
				this.chatTField.strChat = mResources.input_quantity + " ";
				this.chatTField.tfChat.name = mResources.input_quantity;
			}
			this.chatTField.tfChat.setMaxTextLenght(10);
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			if (GameCanvas.isTouch)
			{
				this.chatTField.tfChat.doChangeToTextBox();
			}
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
		}
		if (idAction == 10013)
		{
			if (this.chatTField == null)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = ((GameCanvas.panel2 != null) ? GameCanvas.panel2 : GameCanvas.panel);
			}
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			this.chatTField.tfChat.setText(string.Empty);
			if (this.currItem.quantity == 1)
			{
				this.chatTField.strChat = mResources.kiguiLuongchat;
				this.chatTField.tfChat.name = mResources.input_money;
			}
			else
			{
				this.chatTField.strChat = mResources.input_quantity + "  ";
				this.chatTField.tfChat.name = mResources.input_quantity;
			}
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			if (GameCanvas.isTouch)
			{
				this.chatTField.tfChat.doChangeToTextBox();
			}
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
		}
		if (idAction == 10014)
		{
			Item item12 = (Item)p;
			Service.gI().kigui(1, item12.itemId, -1, -1, -1);
			InfoDlg.showWait();
		}
		if (idAction == 10015)
		{
			Item item13 = (Item)p;
			Service.gI().kigui(2, item13.itemId, -1, -1, -1);
			InfoDlg.showWait();
		}
		if (idAction == 10016)
		{
			Item item14 = (Item)p;
			Service.gI().kigui(3, item14.itemId, 0, item14.buyCoin, -1);
			InfoDlg.showWait();
		}
		if (idAction == 10017)
		{
			Item item15 = (Item)p;
			Service.gI().kigui(3, item15.itemId, 1, item15.buyGold, -1);
			InfoDlg.showWait();
		}
		if (idAction == 10018)
		{
			Item item16 = (Item)p;
			Service.gI().kigui(5, item16.itemId, -1, -1, -1);
			InfoDlg.showWait();
		}
		if (idAction == 10019)
		{
			Session_ME.gI().close();
			Rms.saveRMSString("acc", string.Empty);
			Rms.saveRMSString("pass", string.Empty);
			GameCanvas.loginScr.tfPass.setText(string.Empty);
			GameCanvas.loginScr.tfUser.setText(string.Empty);
			GameCanvas.loginScr.isLogin2 = false;
			GameCanvas.loginScr.switchToMe();
			GameCanvas.endDlg();
			this.hide();
		}
		if (idAction == 10020)
		{
			GameCanvas.endDlg();
		}
		if (idAction == 10030)
		{
			Service.gI().getFlag(1, (sbyte)this.selected);
			GameCanvas.panel.hideNow();
		}
		if (idAction == 10031)
		{
			Session_ME.gI().close();
		}
		if (idAction == 11000)
		{
			Service.gI().kigui(0, this.currItem.itemId, 1, this.currItem.buyRuby, 1);
			GameCanvas.endDlg();
		}
		if (idAction == 11001)
		{
			Service.gI().kigui(0, this.currItem.itemId, 1, this.currItem.buyRuby, this.currItem.quantilyToBuy);
			GameCanvas.endDlg();
		}
		if (idAction == 11002)
		{
			this.chatTField.isShow = false;
			GameCanvas.endDlg();
		}
	}

	// Token: 0x0600070E RID: 1806 RVA: 0x00070BB4 File Offset: 0x0006EDB4
	public void onChatFromMe(string text, string to)
	{
		if (this.chatTField.tfChat.getText() == null || this.chatTField.tfChat.getText().Equals(string.Empty) || text.Equals(string.Empty) || text == null)
		{
			this.chatTField.isShow = false;
			return;
		}
		if (this.chatTField.strChat.Equals(mResources.input_clan_name))
		{
			InfoDlg.showWait();
			this.chatTField.isShow = false;
			Service.gI().searchClan(text);
			return;
		}
		if (this.chatTField.strChat.Equals(mResources.chat_clan))
		{
			InfoDlg.showWait();
			this.chatTField.isShow = false;
			Service.gI().clanMessage(0, text, -1);
			return;
		}
		if (this.chatTField.strChat.Equals(mResources.input_clan_name_to_create))
		{
			if (this.chatTField.tfChat.getText() == string.Empty)
			{
				GameScr.info1.addInfo(mResources.clan_name_blank, 0);
				return;
			}
			if (this.tabIcon == null)
			{
				this.tabIcon = new TabClanIcon();
			}
			this.tabIcon.text = this.chatTField.tfChat.getText();
			this.tabIcon.show(false);
			this.chatTField.isShow = false;
			return;
		}
		else
		{
			if (!this.chatTField.strChat.Equals(mResources.input_clan_slogan))
			{
				if (this.chatTField.strChat.Equals(mResources.input_Inventory_Pass))
				{
					try
					{
						int num = int.Parse(this.chatTField.tfChat.getText());
						this.chatTField.isShow = false;
						this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
						this.hide();
						if (this.chatTField.tfChat.getText().Length != 6 || this.chatTField.tfChat.getText().Equals(string.Empty))
						{
							GameCanvas.startOKDlg(mResources.input_Inventory_Pass_wrong);
						}
						else
						{
							Service.gI().setLockInventory(num);
							this.chatTField.isShow = false;
							this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
							this.hide();
						}
						return;
					}
					catch (Exception)
					{
						GameCanvas.startOKDlg(mResources.ALERT_PRIVATE_PASS_2);
						return;
					}
				}
				if (this.chatTField.strChat.Equals(mResources.world_channel_5_luong))
				{
					if (!this.chatTField.tfChat.getText().Equals(string.Empty))
					{
						Service.gI().chatGlobal(this.chatTField.tfChat.getText());
						this.chatTField.isShow = false;
						this.hide();
						return;
					}
				}
				else if (this.chatTField.strChat.Equals(mResources.chat_player))
				{
					this.chatTField.isShow = false;
					InfoItem infoItem = null;
					if (this.type == 8)
					{
						infoItem = (InfoItem)this.logChat.elementAt(this.currInfoItem);
					}
					else if (this.type == 11)
					{
						infoItem = (InfoItem)this.vFriend.elementAt(this.currInfoItem);
					}
					if (infoItem.charInfo.charID != global::Char.myCharz().charID)
					{
						Service.gI().chatPlayer(text, infoItem.charInfo.charID);
						return;
					}
				}
				else if (this.chatTField.strChat.Equals(mResources.input_quantity_to_trade))
				{
					int num2 = 0;
					try
					{
						num2 = int.Parse(this.chatTField.tfChat.getText());
					}
					catch (Exception)
					{
						GameCanvas.startOKDlg(mResources.input_quantity_wrong);
						this.chatTField.isShow = false;
						this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
						return;
					}
					if (num2 <= 0 || num2 > this.currItem.quantity)
					{
						GameCanvas.startOKDlg(mResources.input_quantity_wrong);
						this.chatTField.isShow = false;
						this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
						return;
					}
					this.currItem.isSelect = true;
					Item item = new Item();
					item.template = this.currItem.template;
					item.quantity = num2;
					item.indexUI = this.currItem.indexUI;
					item.itemOption = this.currItem.itemOption;
					GameCanvas.panel.vMyGD.addElement(item);
					Service.gI().giaodich(2, -1, (sbyte)item.indexUI, item.quantity);
					this.chatTField.isShow = false;
					this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
					return;
				}
				else if (this.chatTField.strChat == mResources.input_money_to_trade)
				{
					int num3 = 0;
					try
					{
						num3 = int.Parse(this.chatTField.tfChat.getText());
					}
					catch (Exception)
					{
						GameCanvas.startOKDlg(mResources.input_money_wrong);
						this.chatTField.isShow = false;
						this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
						return;
					}
					if ((long)num3 > global::Char.myCharz().xu)
					{
						GameCanvas.startOKDlg(mResources.not_enough_money);
						this.chatTField.isShow = false;
						this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
						return;
					}
					this.moneyGD = num3;
					Service.gI().giaodich(2, -1, -1, num3);
					this.chatTField.isShow = false;
					this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
					return;
				}
				else
				{
					if (this.chatTField.strChat.Equals(mResources.kiguiXuchat))
					{
						try
						{
							Service.gI().kigui(0, this.currItem.itemId, 0, int.Parse(this.chatTField.tfChat.getText()), 1);
						}
						catch (Exception)
						{
							GameCanvas.startOKDlg(mResources.input_money_wrong);
						}
						this.chatTField.isShow = false;
						return;
					}
					if (this.chatTField.strChat.Equals(mResources.kiguiXuchat + " "))
					{
						try
						{
							Service.gI().kigui(0, this.currItem.itemId, 0, int.Parse(this.chatTField.tfChat.getText()), this.currItem.quantilyToBuy);
						}
						catch (Exception)
						{
							GameCanvas.startOKDlg(mResources.input_money_wrong);
						}
						this.chatTField.isShow = false;
						return;
					}
					if (this.chatTField.strChat.Equals(mResources.kiguiLuongchat))
					{
						this.doNotiRuby(0);
						this.chatTField.isShow = false;
						return;
					}
					if (this.chatTField.strChat.Equals(mResources.kiguiLuongchat + "  "))
					{
						this.doNotiRuby(1);
						this.chatTField.isShow = false;
						return;
					}
					if (this.chatTField.strChat.Equals(mResources.input_quantity + " "))
					{
						this.currItem.quantilyToBuy = int.Parse(this.chatTField.tfChat.getText());
						if (this.currItem.quantilyToBuy > this.currItem.quantity)
						{
							GameCanvas.startOKDlg(mResources.input_quantity_wrong);
							return;
						}
						this.isKiguiXu = true;
						this.chatTField.isShow = false;
						return;
					}
					else if (this.chatTField.strChat.Equals(mResources.input_quantity + "  "))
					{
						this.currItem.quantilyToBuy = int.Parse(this.chatTField.tfChat.getText());
						if (this.currItem.quantilyToBuy > this.currItem.quantity)
						{
							GameCanvas.startOKDlg(mResources.input_quantity_wrong);
							return;
						}
						this.isKiguiLuong = true;
						this.chatTField.isShow = false;
					}
				}
				return;
			}
			if (this.chatTField.tfChat.getText() == string.Empty)
			{
				GameScr.info1.addInfo(mResources.clan_slogan_blank, 0);
				return;
			}
			Service.gI().getClan(4, (sbyte)global::Char.myCharz().clan.imgID, this.chatTField.tfChat.getText());
			this.chatTField.isShow = false;
			return;
		}
	}

	// Token: 0x0600070F RID: 1807 RVA: 0x000713D4 File Offset: 0x0006F5D4
	public void onCancelChat()
	{
		this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
	}

	// Token: 0x06000710 RID: 1808 RVA: 0x000713EC File Offset: 0x0006F5EC
	public void setCombineEff(int type)
	{
		this.typeCombine = type;
		this.rS = 90;
		if (this.typeCombine == 0)
		{
			this.iDotS = 5;
			this.angleS = (this.angleO = 90);
			this.time = 2;
			for (int i = 0; i < this.vItemCombine.size(); i++)
			{
				Item item = (Item)this.vItemCombine.elementAt(i);
				if (item != null)
				{
					if (item.template.type == 14)
					{
						this.iconID2 = item.template.iconID;
					}
					else
					{
						this.iconID1 = item.template.iconID;
					}
				}
			}
		}
		else if (this.typeCombine == 1)
		{
			this.iDotS = 2;
			this.angleS = (this.angleO = 0);
			this.time = 1;
			for (int j = 0; j < this.vItemCombine.size(); j++)
			{
				Item item2 = (Item)this.vItemCombine.elementAt(j);
				if (item2 != null)
				{
					if (j == 0)
					{
						this.iconID1 = item2.template.iconID;
					}
					else
					{
						this.iconID2 = item2.template.iconID;
					}
				}
			}
		}
		else if (this.typeCombine == 2)
		{
			this.iDotS = 7;
			this.angleS = (this.angleO = 25);
			this.time = 1;
			for (int k = 0; k < this.vItemCombine.size(); k++)
			{
				Item item3 = (Item)this.vItemCombine.elementAt(k);
				if (item3 != null)
				{
					this.iconID1 = item3.template.iconID;
				}
			}
		}
		else if (this.typeCombine == 3)
		{
			this.xS = GameCanvas.hw;
			this.yS = GameCanvas.hh;
			this.iDotS = 1;
			this.angleS = (this.angleO = 1);
			this.time = 4;
			for (int l = 0; l < this.vItemCombine.size(); l++)
			{
				Item item4 = (Item)this.vItemCombine.elementAt(l);
				if (item4 != null)
				{
					this.iconID1 = item4.template.iconID;
				}
			}
		}
		else if (this.typeCombine == 4)
		{
			this.iDotS = this.vItemCombine.size();
			this.iconID = new short[this.iDotS];
			this.angleS = (this.angleO = 25);
			this.time = 1;
			for (int m = 0; m < this.vItemCombine.size(); m++)
			{
				Item item5 = (Item)this.vItemCombine.elementAt(m);
				if (item5 != null)
				{
					this.iconID[m] = item5.template.iconID;
				}
			}
		}
		this.speed = 1;
		this.isSpeedCombine = true;
		this.isDoneCombine = false;
		this.isCompleteEffCombine = false;
		this.iAngleS = 360 / this.iDotS;
		this.xArgS = new int[this.iDotS];
		this.yArgS = new int[this.iDotS];
		this.xDotS = new int[this.iDotS];
		this.yDotS = new int[this.iDotS];
		this.setDotStar();
		this.isPaintCombine = true;
		this.countUpdate = 10;
		this.countR = 30;
		this.countWait = 10;
		this.addTextCombineNPC(this.idNPC, mResources.combineSpell);
	}

	// Token: 0x06000711 RID: 1809 RVA: 0x00071744 File Offset: 0x0006F944
	internal void updateCombineEff()
	{
		this.countUpdate--;
		if (this.countUpdate < 0)
		{
			this.countUpdate = 0;
		}
		this.countR--;
		if (this.countR < 0)
		{
			this.countR = 0;
		}
		if (this.countUpdate != 0)
		{
			return;
		}
		if (!this.isCompleteEffCombine)
		{
			if (this.time > 0)
			{
				if (this.combineSuccess != -1)
				{
					if (this.typeCombine == 3)
					{
						if (GameCanvas.gameTick % 10 == 0)
						{
							EffecMn.addEff(new Effect(21, this.xS - 10, this.yS + 25, 4, 1, 1));
							this.time--;
						}
					}
					else
					{
						if (GameCanvas.gameTick % 2 == 0)
						{
							if (this.isSpeedCombine)
							{
								if (this.speed < 40)
								{
									this.speed += 2;
								}
							}
							else if (this.speed > 10)
							{
								this.speed -= 2;
							}
						}
						if (this.countR == 0)
						{
							if (this.isSpeedCombine)
							{
								if (this.rS > 0)
								{
									this.rS -= 5;
								}
								else if (GameCanvas.gameTick % 10 == 0)
								{
									this.isSpeedCombine = false;
									this.time--;
									this.countR = 5;
									this.countWait = 10;
								}
							}
							else if (this.rS < 90)
							{
								this.rS += 5;
							}
							else if (GameCanvas.gameTick % 10 == 0)
							{
								this.isSpeedCombine = true;
								this.countR = 10;
							}
						}
						this.angleS = this.angleO;
						this.angleS -= this.speed;
						if (this.angleS >= 360)
						{
							this.angleS -= 360;
						}
						if (this.angleS < 0)
						{
							this.angleS = 360 + this.angleS;
						}
						this.angleO = this.angleS;
						this.setDotStar();
					}
				}
			}
			else if (GameCanvas.gameTick % 20 == 0)
			{
				this.isCompleteEffCombine = true;
			}
			if (GameCanvas.gameTick % 20 == 0)
			{
				if (this.typeCombine != 3)
				{
					EffectPanel.addServerEffect(132, this.xS, this.yS, 2);
				}
				EffectPanel.addServerEffect(114, this.xS, this.yS + 20, 2);
				return;
			}
		}
		else
		{
			if (!this.isCompleteEffCombine)
			{
				return;
			}
			if (this.combineSuccess == 1)
			{
				if (this.countWait == 10)
				{
					EffecMn.addEff(new Effect(22, this.xS - 3, this.yS + 25, 4, 1, 1));
				}
				this.countWait--;
				if (this.countWait < 0)
				{
					this.countWait = 0;
				}
				if (this.rS < 300)
				{
					this.rS = Res.abs(this.rS + 10);
					if (this.rS == 20)
					{
						this.addTextCombineNPC(this.idNPC, mResources.combineFail);
					}
				}
				else if (GameCanvas.gameTick % 20 == 0)
				{
					if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.tabName[7] = new string[][] { new string[] { string.Empty } };
						GameCanvas.panel2.setTypeBodyOnly();
						GameCanvas.panel2.show();
					}
					this.combineSuccess = -1;
					this.isDoneCombine = true;
					if (this.typeCombine == 4)
					{
						GameCanvas.panel.hideNow();
					}
				}
				this.setDotStar();
				return;
			}
			if (this.combineSuccess != 0)
			{
				return;
			}
			if (this.countWait == 10)
			{
				if (this.typeCombine == 2)
				{
					EffecMn.addEff(new Effect(20, this.xS - 3, this.yS + 15, 4, 2, 1));
				}
				else
				{
					EffecMn.addEff(new Effect(21, this.xS - 10, this.yS + 25, 4, 1, 1));
				}
				this.addTextCombineNPC(this.idNPC, mResources.combineSuccess);
				this.isPaintCombine = false;
			}
			if (this.isPaintCombine)
			{
				return;
			}
			this.countWait--;
			if (this.countWait < -50)
			{
				this.countWait = -50;
				if (this.typeCombine < 3 && GameCanvas.w > 2 * Panel.WIDTH_PANEL)
				{
					GameCanvas.panel2 = new Panel();
					GameCanvas.panel2.tabName[7] = new string[][] { new string[] { string.Empty } };
					GameCanvas.panel2.setTypeBodyOnly();
					GameCanvas.panel2.show();
				}
				this.combineSuccess = -1;
				this.isDoneCombine = true;
				if (this.typeCombine == 4)
				{
					GameCanvas.panel.hideNow();
				}
			}
		}
	}

	// Token: 0x06000712 RID: 1810 RVA: 0x00071BD4 File Offset: 0x0006FDD4
	public void paintCombineEff(mGraphics g)
	{
		GameScr.gI().paintBlackSky(g);
		this.paintCombineNPC(g);
		if (GameCanvas.gameTick % 4 == 0)
		{
			g.drawImage(ItemMap.imageFlare, this.xS, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
		}
		if (this.typeCombine == 0)
		{
			for (int i = 0; i < this.yArgS.Length; i++)
			{
				SmallImage.drawSmallImage(g, (int)this.iconID1, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				if (this.isPaintCombine)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID2, this.xDotS[i], this.yDotS[i], 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
			return;
		}
		if (this.typeCombine == 1)
		{
			if (!this.isPaintCombine)
			{
				SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				return;
			}
			for (int j = 0; j < this.yArgS.Length; j++)
			{
				SmallImage.drawSmallImage(g, (int)this.iconID1, this.xDotS[0], this.yDotS[0], 0, mGraphics.VCENTER | mGraphics.HCENTER);
				SmallImage.drawSmallImage(g, (int)this.iconID2, this.xDotS[1], this.yDotS[1], 0, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			return;
		}
		else if (this.typeCombine == 2)
		{
			if (!this.isPaintCombine)
			{
				SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				return;
			}
			for (int k = 0; k < this.yArgS.Length; k++)
			{
				SmallImage.drawSmallImage(g, (int)this.iconID1, this.xDotS[k], this.yDotS[k], 0, mGraphics.VCENTER | mGraphics.HCENTER);
			}
			return;
		}
		else if (this.typeCombine == 3)
		{
			if (!this.isPaintCombine)
			{
				SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				return;
			}
			SmallImage.drawSmallImage(g, (int)this.iconID1, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
			return;
		}
		else
		{
			if (this.typeCombine != 4)
			{
				return;
			}
			if (!this.isPaintCombine)
			{
				if (this.iconID3 != -1)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID3, this.xS, this.yS, 0, mGraphics.VCENTER | mGraphics.HCENTER);
					return;
				}
			}
			else
			{
				for (int l = 0; l < this.iconID.Length; l++)
				{
					SmallImage.drawSmallImage(g, (int)this.iconID[l], this.xDotS[l], this.yDotS[l], 0, mGraphics.VCENTER | mGraphics.HCENTER);
				}
			}
			return;
		}
	}

	// Token: 0x06000713 RID: 1811 RVA: 0x00071E78 File Offset: 0x00070078
	internal void setDotStar()
	{
		for (int i = 0; i < this.yArgS.Length; i++)
		{
			if (this.angleS >= 360)
			{
				this.angleS -= 360;
			}
			if (this.angleS < 0)
			{
				this.angleS = 360 + this.angleS;
			}
			this.yArgS[i] = Res.abs(this.rS * Res.sin(this.angleS) / 1024);
			this.xArgS[i] = Res.abs(this.rS * Res.cos(this.angleS) / 1024);
			if (this.angleS < 90)
			{
				this.xDotS[i] = this.xS + this.xArgS[i];
				this.yDotS[i] = this.yS - this.yArgS[i];
			}
			else if (this.angleS >= 90 && this.angleS < 180)
			{
				this.xDotS[i] = this.xS - this.xArgS[i];
				this.yDotS[i] = this.yS - this.yArgS[i];
			}
			else if (this.angleS >= 180 && this.angleS < 270)
			{
				this.xDotS[i] = this.xS - this.xArgS[i];
				this.yDotS[i] = this.yS + this.yArgS[i];
			}
			else
			{
				this.xDotS[i] = this.xS + this.xArgS[i];
				this.yDotS[i] = this.yS + this.yArgS[i];
			}
			this.angleS -= this.iAngleS;
		}
	}

	// Token: 0x06000714 RID: 1812 RVA: 0x00072034 File Offset: 0x00070234
	public void paintCombineNPC(mGraphics g)
	{
		g.translate(-GameScr.cmx, -GameScr.cmy);
		if (this.typeCombine < 3)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId == this.idNPC)
				{
					npc.paint(g);
					if (npc.chatInfo != null)
					{
						npc.chatInfo.paint(g, npc.cx, npc.cy - npc.ch - GameCanvas.transY, npc.cdir);
					}
				}
			}
		}
		GameCanvas.resetTrans(g);
		if (GameCanvas.gameTick % 4 == 0)
		{
			g.drawImage(ItemMap.imageFlare, this.xS - 5, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
			g.drawImage(ItemMap.imageFlare, this.xS + 5, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
			g.drawImage(ItemMap.imageFlare, this.xS, this.yS + 15, mGraphics.BOTTOM | mGraphics.HCENTER);
		}
		for (int j = 0; j < Effect2.vEffect3.size(); j++)
		{
			((Effect2)Effect2.vEffect3.elementAt(j)).paint(g);
		}
	}

	// Token: 0x06000715 RID: 1813 RVA: 0x0007217C File Offset: 0x0007037C
	public void addTextCombineNPC(int idNPC, string text)
	{
		if (this.typeCombine >= 3)
		{
			return;
		}
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			if (npc.template.npcTemplateId == idNPC)
			{
				npc.addInfo(text);
			}
		}
	}

	// Token: 0x06000716 RID: 1814 RVA: 0x000721D0 File Offset: 0x000703D0
	public void setTypeOption()
	{
		this.type = 19;
		this.setType(0);
		this.setTabOption();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x06000717 RID: 1815 RVA: 0x00072204 File Offset: 0x00070404
	internal void setTabOption()
	{
		SoundMn.gI().getStrOption();
		this.currentListLength = Panel.strCauhinh.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x06000718 RID: 1816 RVA: 0x000722C4 File Offset: 0x000704C4
	internal void paintOption(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.strCauhinh.Length; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 1;
			int num4 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, num4);
				mFont.tahoma_7b_dark.drawString(g, Panel.strCauhinh[i], this.xScroll + 25, num2 + 6, mFont.LEFT);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x06000719 RID: 1817 RVA: 0x000723C0 File Offset: 0x000705C0
	internal void doFireOption()
	{
		if (this.selected < 0)
		{
			return;
		}
		switch (this.selected)
		{
		case 0:
			SoundMn.gI().AuraToolOption();
			return;
		case 1:
			SoundMn.gI().AuraToolOption2();
			return;
		case 2:
			SoundMn.gI().soundToolOption();
			return;
		case 3:
			if (Main.isPC)
			{
				GameCanvas.startYesNoDlg(mResources.changeSizeScreen, new Command(mResources.YES, this, 170391, null), new Command(mResources.NO, this, 4005, null));
				return;
			}
			SoundMn.gI().CaseSizeScr();
			return;
		case 4:
			if (Main.isPC)
			{
				GameCanvas.startYesNoDlg(mResources.changeSizeScreen, new Command(mResources.YES, this, 170391, null), new Command(mResources.NO, this, 4005, null));
				return;
			}
			SoundMn.gI().CaseAnalog();
			return;
		case 5:
			SoundMn.gI().CaseAnalog();
			return;
		default:
			return;
		}
	}

	// Token: 0x0600071A RID: 1818 RVA: 0x000724A8 File Offset: 0x000706A8
	public void setTypeAccount()
	{
		this.type = 20;
		this.setType(0);
		this.setTabAccount();
		this.cmx = (this.cmtoX = 0);
	}

	// Token: 0x0600071B RID: 1819 RVA: 0x000724DC File Offset: 0x000706DC
	internal void setTabAccount()
	{
		if (Main.IphoneVersionApp)
		{
			Panel.strAccount = new string[]
			{
				mResources.inventory_Pass,
				mResources.friend,
				mResources.enemy,
				mResources.msg
			};
			if (GameScr.canAutoPlay)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.autoFunction
				};
			}
		}
		else
		{
			Panel.strAccount = new string[]
			{
				mResources.inventory_Pass,
				mResources.friend,
				mResources.enemy,
				mResources.msg,
				mResources.charger
			};
			if (GameScr.canAutoPlay)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.charger,
					mResources.autoFunction
				};
			}
			if ((mSystem.clientType == 2 || mSystem.clientType == 7) && mResources.language != 2)
			{
				Panel.strAccount = new string[]
				{
					mResources.inventory_Pass,
					mResources.friend,
					mResources.enemy,
					mResources.msg,
					mResources.charger
				};
				if (GameScr.canAutoPlay)
				{
					Panel.strAccount = new string[]
					{
						mResources.inventory_Pass,
						mResources.friend,
						mResources.enemy,
						mResources.msg,
						mResources.charger,
						mResources.autoFunction
					};
				}
			}
		}
		this.currentListLength = Panel.strAccount.Length;
		this.ITEM_HEIGHT = 24;
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
	}

	// Token: 0x0600071C RID: 1820 RVA: 0x00072708 File Offset: 0x00070908
	internal void paintAccount(mGraphics g)
	{
		g.setClip(this.xScroll, this.yScroll, this.wScroll, this.hScroll);
		g.translate(0, -this.cmy);
		for (int i = 0; i < Panel.strAccount.Length; i++)
		{
			int num = this.xScroll;
			int num2 = this.yScroll + i * this.ITEM_HEIGHT;
			int num3 = this.wScroll - 1;
			int num4 = this.ITEM_HEIGHT - 1;
			if (num2 - this.cmy <= this.yScroll + this.hScroll && num2 - this.cmy >= this.yScroll - this.ITEM_HEIGHT)
			{
				g.setColor((i != this.selected) ? 15196114 : 16383818);
				g.fillRect(num, num2, num3, num4);
				mFont.tahoma_7b_dark.drawString(g, Panel.strAccount[i], this.xScroll + this.wScroll / 2, num2 + 6, mFont.CENTER);
			}
		}
		this.paintScrollArrow(g);
	}

	// Token: 0x0600071D RID: 1821 RVA: 0x00072808 File Offset: 0x00070A08
	internal void doFireAccount()
	{
		if (this.selected < 0)
		{
			return;
		}
		switch (this.selected)
		{
		case 0:
			GameCanvas.endDlg();
			if (this.chatTField == null)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = GameCanvas.panel;
			}
			this.chatTField.tfChat.setText(string.Empty);
			this.chatTField.strChat = mResources.input_Inventory_Pass;
			this.chatTField.tfChat.name = mResources.input_Inventory_Pass;
			this.chatTField.to = string.Empty;
			this.chatTField.isShow = true;
			this.chatTField.tfChat.isFocus = true;
			this.chatTField.tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
			if (GameCanvas.isTouch)
			{
				this.chatTField.tfChat.doChangeToTextBox();
			}
			if (!Main.isPC)
			{
				this.chatTField.startChat2(this, string.Empty);
			}
			if (Main.isWindowsPhone)
			{
				this.chatTField.tfChat.strInfo = this.chatTField.strChat;
				return;
			}
			break;
		case 1:
			Service.gI().friend(0, -1);
			InfoDlg.showWait();
			return;
		case 2:
			Service.gI().enemy(0, -1);
			InfoDlg.showWait();
			return;
		case 3:
			this.setTypeMessage();
			if (this.chatTField == null)
			{
				this.chatTField = new ChatTextField();
				this.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
				this.chatTField.initChatTextField();
				this.chatTField.parentScreen = GameCanvas.panel;
				return;
			}
			break;
		case 4:
			if (mResources.language == 2)
			{
				string text = "http://dragonball.indonaga.com/coda/?username=" + GameCanvas.loginScr.tfUser.getText();
				this.hideNow();
				try
				{
					GameMidlet.instance.platformRequest(text);
					break;
				}
				catch (Exception ex)
				{
					ex.StackTrace.ToString();
					break;
				}
			}
			this.hideNow();
			if (global::Char.myCharz().taskMaint.taskId <= 10)
			{
				GameCanvas.startOKDlg(mResources.finishBomong);
				return;
			}
			MoneyCharge.gI().switchToMe();
			return;
		case 5:
			this.setTypeAuto();
			break;
		default:
			return;
		}
	}

	// Token: 0x0600071E RID: 1822 RVA: 0x0005DDF4 File Offset: 0x0005BFF4
	internal void updateKeyOption()
	{
		this.updateKeyScrollView();
	}

	// Token: 0x0600071F RID: 1823 RVA: 0x00072A7C File Offset: 0x00070C7C
	public void setTypeSpeacialSkill()
	{
		this.type = 25;
		this.setType(0);
		this.setTabSpeacialSkill();
		this.currentTabIndex = 0;
	}

	// Token: 0x06000720 RID: 1824 RVA: 0x00072A9C File Offset: 0x00070C9C
	internal void setTabSpeacialSkill()
	{
		this.ITEM_HEIGHT = 24;
		this.currentListLength = global::Char.myCharz().infoSpeacialSkill[this.currentTabIndex].Length;
		this.cmyLim = this.currentListLength * this.ITEM_HEIGHT - this.hScroll;
		if (this.cmyLim < 0)
		{
			this.cmyLim = 0;
		}
		this.cmy = (this.cmtoY = this.cmyLast[this.currentTabIndex]);
		if (this.cmy < 0)
		{
			this.cmy = (this.cmtoY = 0);
		}
		if (this.cmy > this.cmyLim)
		{
			this.cmy = (this.cmtoY = this.cmyLim);
		}
		this.selected = (GameCanvas.isTouch ? (-1) : 0);
	}

	// Token: 0x06000721 RID: 1825 RVA: 0x00072B5D File Offset: 0x00070D5D
	public bool isTypeShop()
	{
		return this.type == 1;
	}

	// Token: 0x06000722 RID: 1826 RVA: 0x00072B6C File Offset: 0x00070D6C
	internal void doNotiRuby(int type)
	{
		try
		{
			this.currItem.buyRuby = int.Parse(this.chatTField.tfChat.getText());
		}
		catch (Exception)
		{
			GameCanvas.startOKDlg(mResources.input_money_wrong);
			this.chatTField.isShow = false;
			return;
		}
		Command command = new Command(mResources.YES, this, (type != 0) ? 11001 : 11000, null);
		Command command2 = new Command(mResources.NO, this, 11002, null);
		GameCanvas.startYesNoDlg(mResources.notiRuby, command, command2);
	}

	// Token: 0x06000723 RID: 1827 RVA: 0x00072C00 File Offset: 0x00070E00
	public static void paintUpgradeEffect(int x, int y, int wItem, int hItem, int nline, int cl, mGraphics g)
	{
		try
		{
			int num = ((wItem << 1) + (hItem << 1)) / nline;
			Panel.nsize = Panel.sizeUpgradeEff.Length;
			if (nline > 4)
			{
				Panel.nsize = 2;
			}
			for (int i = 0; i < nline; i++)
			{
				for (int j = 0; j < Panel.nsize; j++)
				{
					int num2 = ((Panel.sizeUpgradeEff[j] <= 1) ? 1 : ((Panel.sizeUpgradeEff[j] >> 1) + 1));
					int num3 = x + Panel.upgradeEffectX(num * i, GameCanvas.gameTick - j * 4, wItem, hItem, num2);
					int num4 = y + Panel.upgradeEffectY(num * i, GameCanvas.gameTick - j * 4, wItem, hItem, num2);
					g.setColor(Panel.colorUpgradeEffect[cl][j]);
					g.fillRect(num3, num4, Panel.sizeUpgradeEff[j], Panel.sizeUpgradeEff[j]);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000724 RID: 1828 RVA: 0x00072CDC File Offset: 0x00070EDC
	internal static int upgradeEffectX(int dk, int tick, int wItem, int hitem, int wSize)
	{
		int num = (tick + dk) % ((wItem << 1) + (hitem << 1));
		if (0 <= num && num < wItem)
		{
			return num % wItem;
		}
		if (wItem <= num && num < wItem + hitem)
		{
			return wItem - wSize;
		}
		if (wItem + hitem <= num && num < (wItem << 1) + hitem)
		{
			return wItem - (num - hitem) % wItem - wSize;
		}
		return 0;
	}

	// Token: 0x06000725 RID: 1829 RVA: 0x00072D2C File Offset: 0x00070F2C
	internal static int upgradeEffectY(int dk, int tick, int wItem, int hitem, int wSize)
	{
		int num = (tick + dk) % ((wItem << 1) + (hitem << 1));
		if (0 <= num && num < wItem)
		{
			return 0;
		}
		if (wItem <= num && num < wItem + hitem)
		{
			return num % wItem;
		}
		if (wItem + hitem <= num && num < (wItem << 1) + hitem)
		{
			return hitem - wSize;
		}
		return hitem - (num - (wItem << 1)) % hitem - wSize;
	}

	// Token: 0x06000726 RID: 1830 RVA: 0x00072D7C File Offset: 0x00070F7C
	public static int GetColor_ItemBg(int id)
	{
		switch (id)
		{
		case 1:
			return 2786816;
		case 2:
			return 7078041;
		case 3:
			return 12537346;
		case 4:
			return 1269146;
		case 5:
			return 13279744;
		case 6:
			return 11599872;
		default:
			return -1;
		}
	}

	// Token: 0x06000727 RID: 1831 RVA: 0x00072DD0 File Offset: 0x00070FD0
	public static sbyte GetColor_Item_Upgrade(int lv)
	{
		if (lv < 0)
		{
			return 0;
		}
		switch (lv)
		{
		case 0:
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
		case 6:
		case 7:
		case 8:
			return 0;
		case 9:
			return 4;
		case 10:
			return 1;
		case 11:
			return 5;
		case 12:
			return 3;
		case 13:
			return 2;
		default:
			return 6;
		}
	}

	// Token: 0x06000728 RID: 1832 RVA: 0x00072E30 File Offset: 0x00071030
	public static mFont GetFont(int color)
	{
		mFont mFont = mFont.tahoma_7;
		switch (color)
		{
		case -1:
			mFont = mFont.tahoma_7;
			break;
		case 0:
			mFont = mFont.tahoma_7b_dark;
			break;
		case 1:
			mFont = mFont.tahoma_7b_green;
			break;
		case 2:
			mFont = mFont.tahoma_7b_blue;
			break;
		case 3:
			mFont = mFont.tahoma_7_red;
			break;
		case 4:
			mFont = mFont.tahoma_7_green;
			break;
		case 5:
			mFont = mFont.tahoma_7_blue;
			break;
		case 7:
			mFont = mFont.tahoma_7b_red;
			break;
		case 8:
			mFont = mFont.tahoma_7b_yellow;
			break;
		}
		return mFont;
	}

	// Token: 0x06000729 RID: 1833 RVA: 0x00072EBC File Offset: 0x000710BC
	public void paintOptItem(mGraphics g, int idOpt, int param, int x, int y, int w, int h)
	{
		if (idOpt == 34)
		{
			if (this.imgo_0 != null)
			{
				g.drawImage(this.imgo_0, x, y + h - this.imgo_0.getHeight());
			}
			else
			{
				this.imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
			}
			if (this.imgo_1 != null)
			{
				g.drawImage(this.imgo_1, x, y + h - this.imgo_1.getHeight());
				return;
			}
			this.imgo_1 = mSystem.loadImage("/mainImage/o_1.png");
			return;
		}
		else
		{
			if (idOpt != 35)
			{
				if (idOpt == 36)
				{
					if (this.imgo_0 != null)
					{
						g.drawImage(this.imgo_0, x, y + h - this.imgo_0.getHeight());
					}
					else
					{
						this.imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
					}
					if (this.imgo_3 != null)
					{
						g.drawImage(this.imgo_3, x, y + h - this.imgo_3.getHeight());
						return;
					}
					this.imgo_3 = mSystem.loadImage("/mainImage/o_3.png");
				}
				return;
			}
			if (this.imgo_0 != null)
			{
				g.drawImage(this.imgo_0, x, y + h - this.imgo_0.getHeight());
			}
			else
			{
				this.imgo_0 = mSystem.loadImage("/mainImage/o_0.png");
			}
			if (this.imgo_2 != null)
			{
				g.drawImage(this.imgo_2, x, y + h - this.imgo_2.getHeight());
				return;
			}
			this.imgo_2 = mSystem.loadImage("/mainImage/o_2.png");
			return;
		}
	}

	// Token: 0x0600072A RID: 1834 RVA: 0x00073030 File Offset: 0x00071230
	public void paintOptSlotItem(mGraphics g, int idOpt, int param, int x, int y, int w, int h)
	{
		if (idOpt == 102 && param > ChatPopup.numSlot)
		{
			sbyte color_Item_Upgrade = Panel.GetColor_Item_Upgrade(param);
			Panel.paintUpgradeEffect(x, y, w, h, param - ChatPopup.numSlot, (int)color_Item_Upgrade, g);
		}
	}

	// Token: 0x0600072B RID: 1835 RVA: 0x00073068 File Offset: 0x00071268
	public static mFont setTextColor(int id, int type)
	{
		if (type == 0)
		{
			switch (id)
			{
			case 0:
				return mFont.bigNumber_While;
			case 1:
				return mFont.bigNumber_green;
			case 3:
				return mFont.bigNumber_orange;
			case 4:
				return mFont.bigNumber_blue;
			case 5:
				return mFont.bigNumber_yellow;
			case 6:
				return mFont.bigNumber_red;
			}
			return mFont.bigNumber_While;
		}
		switch (id)
		{
		case 0:
			return mFont.tahoma_7b_white;
		case 1:
			return mFont.tahoma_7b_green;
		case 3:
			return mFont.tahoma_7b_yellowSmall2;
		case 4:
			return mFont.tahoma_7b_blue;
		case 5:
			return mFont.tahoma_7b_yellow;
		case 6:
			return mFont.tahoma_7b_red;
		case 7:
			return mFont.tahoma_7b_dark;
		}
		return mFont.tahoma_7b_white;
	}

	// Token: 0x0600072C RID: 1836 RVA: 0x00073120 File Offset: 0x00071320
	internal bool GetInventorySelect_isbody(int select, int subSelect, Item[] arrItem)
	{
		int num = select - 1 + subSelect * 20;
		return subSelect == 0 && num < arrItem.Length;
	}

	// Token: 0x0600072D RID: 1837 RVA: 0x00073141 File Offset: 0x00071341
	internal int GetInventorySelect_body(int select, int subSelect)
	{
		return select - 1 + subSelect * 20;
	}

	// Token: 0x0600072E RID: 1838 RVA: 0x0007314B File Offset: 0x0007134B
	internal int GetInventorySelect_bag(int select, int subSelect, Item[] arrItem)
	{
		return select - 1 + subSelect * 20 - arrItem.Length;
	}

	// Token: 0x0600072F RID: 1839 RVA: 0x00073159 File Offset: 0x00071359
	internal bool isTabInven()
	{
		return (this.type == 0 && this.currentTabIndex == 1) || (this.type == 7 && this.currentTabIndex == 0);
	}

	// Token: 0x06000730 RID: 1840 RVA: 0x00073180 File Offset: 0x00071380
	internal void updateKeyInvenTab()
	{
		if (this.selected < 0)
		{
			return;
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
		{
			this.newSelected--;
			if (this.isnewInventory)
			{
				this.currentListLength = 5;
			}
			if (this.newSelected < 0)
			{
				this.newSelected = 0;
				if (GameCanvas.isFocusPanel2)
				{
					GameCanvas.isFocusPanel2 = false;
					GameCanvas.panel.selected = 0;
					return;
				}
			}
		}
		else
		{
			if (!GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
			{
				return;
			}
			this.newSelected++;
			if (this.isnewInventory)
			{
				this.currentListLength = 5;
			}
			if (this.newSelected > (int)(this.size_tab - 1))
			{
				this.newSelected = (int)(this.size_tab - 1);
				if (GameCanvas.panel2 != null)
				{
					GameCanvas.isFocusPanel2 = true;
					GameCanvas.panel2.selected = 0;
				}
			}
		}
	}

	// Token: 0x06000731 RID: 1841 RVA: 0x0007325C File Offset: 0x0007145C
	internal void updateKeyInventory()
	{
		this.updateKeyScrollView();
		if (this.selected == 0)
		{
			this.updateKeyInvenTab();
		}
	}

	// Token: 0x06000732 RID: 1842 RVA: 0x00073272 File Offset: 0x00071472
	internal bool IsTabOption()
	{
		if (this.size_tab > 0)
		{
			if (this.currentTabName.Length > 1)
			{
				if (this.selected == 0)
				{
					return true;
				}
			}
			else if (this.selected >= 0)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000733 RID: 1843 RVA: 0x000732A0 File Offset: 0x000714A0
	internal int checkCurrentListLength(int arrLength)
	{
		int num = 20;
		int num2 = arrLength / 20 + ((arrLength % 20 > 0) ? 1 : 0);
		this.size_tab = (sbyte)num2;
		if (this.newSelected > num2 - 1)
		{
			this.newSelected = num2 - 1;
		}
		if (arrLength % 20 > 0 && this.newSelected == num2 - 1)
		{
			num = arrLength % 20;
		}
		return num + 1;
	}

	// Token: 0x06000734 RID: 1844 RVA: 0x000732F8 File Offset: 0x000714F8
	internal void setNewSelected(int arrLength, bool resetSelect)
	{
		int num = arrLength / 20 + ((arrLength % 20 > 0) ? 1 : 0);
		int num2 = this.xScroll;
		this.newSelected = (GameCanvas.px - num2) / this.TAB_W_NEW;
		if (this.newSelected > num - 1)
		{
			this.newSelected = num - 1;
		}
		if (GameCanvas.px < num2)
		{
			this.newSelected = 0;
		}
		this.setTabInventory(resetSelect);
	}

	// Token: 0x04000C7F RID: 3199
	public bool isShow;

	// Token: 0x04000C80 RID: 3200
	public int X;

	// Token: 0x04000C81 RID: 3201
	public int Y;

	// Token: 0x04000C82 RID: 3202
	public int W;

	// Token: 0x04000C83 RID: 3203
	public int H;

	// Token: 0x04000C84 RID: 3204
	public int ITEM_HEIGHT;

	// Token: 0x04000C85 RID: 3205
	public int TAB_W;

	// Token: 0x04000C86 RID: 3206
	public int TAB_W_NEW;

	// Token: 0x04000C87 RID: 3207
	public int cmtoY;

	// Token: 0x04000C88 RID: 3208
	public int cmy;

	// Token: 0x04000C89 RID: 3209
	public int cmdy;

	// Token: 0x04000C8A RID: 3210
	public int cmvy;

	// Token: 0x04000C8B RID: 3211
	public int cmyLim;

	// Token: 0x04000C8C RID: 3212
	public int xc;

	// Token: 0x04000C8D RID: 3213
	public int[] cmyLast;

	// Token: 0x04000C8E RID: 3214
	public int cmtoX;

	// Token: 0x04000C8F RID: 3215
	public int cmx;

	// Token: 0x04000C90 RID: 3216
	public int cmxLim;

	// Token: 0x04000C91 RID: 3217
	public int cmxMap;

	// Token: 0x04000C92 RID: 3218
	public int cmyMap;

	// Token: 0x04000C93 RID: 3219
	public int cmxMapLim;

	// Token: 0x04000C94 RID: 3220
	public int cmyMapLim;

	// Token: 0x04000C95 RID: 3221
	public int cmyQuest;

	// Token: 0x04000C96 RID: 3222
	public static Image imgBantay;

	// Token: 0x04000C97 RID: 3223
	public static Image imgX;

	// Token: 0x04000C98 RID: 3224
	public static Image imgMap;

	// Token: 0x04000C99 RID: 3225
	public TabClanIcon tabIcon;

	// Token: 0x04000C9A RID: 3226
	public MyVector vItemCombine = new MyVector();

	// Token: 0x04000C9B RID: 3227
	public int moneyGD;

	// Token: 0x04000C9C RID: 3228
	public int friendMoneyGD;

	// Token: 0x04000C9D RID: 3229
	public bool isLock;

	// Token: 0x04000C9E RID: 3230
	public bool isFriendLock;

	// Token: 0x04000C9F RID: 3231
	public bool isAccept;

	// Token: 0x04000CA0 RID: 3232
	public bool isFriendAccep;

	// Token: 0x04000CA1 RID: 3233
	public string topName;

	// Token: 0x04000CA2 RID: 3234
	public ChatTextField chatTField;

	// Token: 0x04000CA3 RID: 3235
	public static string specialInfo;

	// Token: 0x04000CA4 RID: 3236
	public static short spearcialImage;

	// Token: 0x04000CA5 RID: 3237
	public static Image imgStar;

	// Token: 0x04000CA6 RID: 3238
	public static Image imgMaxStar;

	// Token: 0x04000CA7 RID: 3239
	public static Image imgStar8;

	// Token: 0x04000CA8 RID: 3240
	public static Image imgStar9;

	// Token: 0x04000CA9 RID: 3241
	public static Image imgStarCuongHoa;

	// Token: 0x04000CAA RID: 3242
	public static Image imgNew;

	// Token: 0x04000CAB RID: 3243
	public static Image imgXu;

	// Token: 0x04000CAC RID: 3244
	public static Image imgTicket;

	// Token: 0x04000CAD RID: 3245
	public static Image imgLuong;

	// Token: 0x04000CAE RID: 3246
	public static Image imgLuongKhoa;

	// Token: 0x04000CAF RID: 3247
	internal static Image imgUp;

	// Token: 0x04000CB0 RID: 3248
	internal static Image imgDown;

	// Token: 0x04000CB1 RID: 3249
	internal int pa1;

	// Token: 0x04000CB2 RID: 3250
	internal int pa2;

	// Token: 0x04000CB3 RID: 3251
	internal bool trans;

	// Token: 0x04000CB4 RID: 3252
	internal int pX;

	// Token: 0x04000CB5 RID: 3253
	internal int pY;

	// Token: 0x04000CB6 RID: 3254
	internal Command left = new Command(mResources.SELECT, 0);

	// Token: 0x04000CB7 RID: 3255
	public int type;

	// Token: 0x04000CB8 RID: 3256
	public int currentTabIndex;

	// Token: 0x04000CB9 RID: 3257
	public int startTabPos;

	// Token: 0x04000CBA RID: 3258
	public int[] lastTabIndex;

	// Token: 0x04000CBB RID: 3259
	public string[][] currentTabName;

	// Token: 0x04000CBC RID: 3260
	internal int[] currClanOption;

	// Token: 0x04000CBD RID: 3261
	public int mainTabPos = 4;

	// Token: 0x04000CBE RID: 3262
	public int shopTabPos = 50;

	// Token: 0x04000CBF RID: 3263
	public int boxTabPos = 50;

	// Token: 0x04000CC0 RID: 3264
	public string[][] mainTabName;

	// Token: 0x04000CC1 RID: 3265
	public string[] mapNames;

	// Token: 0x04000CC2 RID: 3266
	public string[] planetNames;

	// Token: 0x04000CC3 RID: 3267
	public static string[] strTool = new string[]
	{
		mResources.gameInfo,
		mResources.change_flag,
		mResources.change_zone,
		mResources.chat_world,
		mResources.account,
		mResources.option,
		mResources.change_account
	};

	// Token: 0x04000CC4 RID: 3268
	public static string[] strCauhinh = new string[]
	{
		(!GameCanvas.isPlaySound) ? mResources.turnOnSound : mResources.turnOffSound,
		mResources.increase_vga,
		mResources.analog,
		(mGraphics.zoomLevel <= 1) ? mResources.x2Screen : mResources.x1Screen
	};

	// Token: 0x04000CC5 RID: 3269
	public static string[] strAccount = new string[]
	{
		mResources.inventory_Pass,
		mResources.friend,
		mResources.enemy,
		mResources.msg,
		mResources.charger
	};

	// Token: 0x04000CC6 RID: 3270
	public static string[] strAuto = new string[] { mResources.useGem };

	// Token: 0x04000CC7 RID: 3271
	public static int graphics = 0;

	// Token: 0x04000CC8 RID: 3272
	public string[][] shopTabName;

	// Token: 0x04000CC9 RID: 3273
	public int[] maxPageShop;

	// Token: 0x04000CCA RID: 3274
	public int[] currPageShop;

	// Token: 0x04000CCB RID: 3275
	internal static string[][] boxTabName = new string[][]
	{
		mResources.chestt,
		mResources.inventory
	};

	// Token: 0x04000CCC RID: 3276
	internal static string[][] boxCombine = new string[][]
	{
		mResources.combine,
		mResources.inventory
	};

	// Token: 0x04000CCD RID: 3277
	internal static string[][] boxZone = new string[][] { mResources.zonee };

	// Token: 0x04000CCE RID: 3278
	internal static string[][] boxMap = new string[][] { mResources.mapp };

	// Token: 0x04000CCF RID: 3279
	internal static string[][] boxGD = new string[][]
	{
		mResources.inventory,
		mResources.item_give,
		mResources.item_receive
	};

	// Token: 0x04000CD0 RID: 3280
	internal static string[][] boxPet = mResources.petMainTab;

	// Token: 0x04000CD1 RID: 3281
	public string[][][] tabName = new string[][][]
	{
		null,
		null,
		Panel.boxTabName,
		Panel.boxZone,
		Panel.boxMap,
		null,
		null,
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		Panel.boxCombine,
		Panel.boxGD,
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		Panel.boxPet,
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } },
		new string[][] { new string[] { string.Empty } }
	};

	// Token: 0x04000CD2 RID: 3282
	internal static sbyte BOX_BAG = 0;

	// Token: 0x04000CD3 RID: 3283
	internal static sbyte BAG_BOX = 1;

	// Token: 0x04000CD4 RID: 3284
	internal static sbyte BOX_BODY = 2;

	// Token: 0x04000CD5 RID: 3285
	internal static sbyte BODY_BOX = 3;

	// Token: 0x04000CD6 RID: 3286
	internal static sbyte BAG_BODY = 4;

	// Token: 0x04000CD7 RID: 3287
	internal static sbyte BODY_BAG = 5;

	// Token: 0x04000CD8 RID: 3288
	internal static sbyte BAG_PET = 6;

	// Token: 0x04000CD9 RID: 3289
	internal static sbyte PET_BAG = 7;

	// Token: 0x04000CDA RID: 3290
	public int hasUse;

	// Token: 0x04000CDB RID: 3291
	public int hasUseBag;

	// Token: 0x04000CDC RID: 3292
	public int currentListLength;

	// Token: 0x04000CDD RID: 3293
	internal int[] lastSelect;

	// Token: 0x04000CDE RID: 3294
	public static int[] mapIdTraidat = new int[]
	{
		21, 0, 1, 2, 24, 3, 4, 5, 6, 27,
		28, 29, 30, 42, 47, 46
	};

	// Token: 0x04000CDF RID: 3295
	public static int[] mapXTraidat = new int[]
	{
		39, 42, 105, 93, 61, 93, 142, 165, 210, 100,
		165, 220, 233, 10, 125, 125
	};

	// Token: 0x04000CE0 RID: 3296
	public static int[] mapYTraidat = new int[]
	{
		28, 60, 48, 96, 88, 131, 136, 95, 32, 200,
		189, 167, 120, 110, 20, 20
	};

	// Token: 0x04000CE1 RID: 3297
	public static int[] mapIdNamek = new int[]
	{
		22, 7, 8, 9, 25, 11, 12, 13, 10, 31,
		32, 33, 34, 43
	};

	// Token: 0x04000CE2 RID: 3298
	public static int[] mapXNamek = new int[]
	{
		55, 30, 93, 80, 24, 149, 219, 220, 233, 170,
		148, 195, 148, 10
	};

	// Token: 0x04000CE3 RID: 3299
	public static int[] mapYNamek = new int[]
	{
		136, 84, 69, 34, 25, 42, 32, 110, 192, 70,
		106, 156, 210, 57
	};

	// Token: 0x04000CE4 RID: 3300
	public static int[] mapIdSaya = new int[]
	{
		23, 14, 15, 16, 26, 17, 18, 20, 19, 35,
		36, 37, 38, 44
	};

	// Token: 0x04000CE5 RID: 3301
	public static int[] mapXSaya = new int[]
	{
		90, 95, 144, 234, 231, 122, 176, 158, 205, 54,
		105, 159, 231, 27
	};

	// Token: 0x04000CE6 RID: 3302
	public static int[] mapYSaya = new int[]
	{
		10, 43, 20, 36, 69, 87, 112, 167, 160, 151,
		173, 207, 194, 29
	};

	// Token: 0x04000CE7 RID: 3303
	public static int[][] mapId = new int[][]
	{
		Panel.mapIdTraidat,
		Panel.mapIdNamek,
		Panel.mapIdSaya
	};

	// Token: 0x04000CE8 RID: 3304
	public static int[][] mapX = new int[][]
	{
		Panel.mapXTraidat,
		Panel.mapXNamek,
		Panel.mapXSaya
	};

	// Token: 0x04000CE9 RID: 3305
	public static int[][] mapY = new int[][]
	{
		Panel.mapYTraidat,
		Panel.mapYNamek,
		Panel.mapYSaya
	};

	// Token: 0x04000CEA RID: 3306
	public Item currItem;

	// Token: 0x04000CEB RID: 3307
	public Clan currClan;

	// Token: 0x04000CEC RID: 3308
	public ClanMessage currMess;

	// Token: 0x04000CED RID: 3309
	public Member currMem;

	// Token: 0x04000CEE RID: 3310
	public Clan[] clans;

	// Token: 0x04000CEF RID: 3311
	public MyVector member;

	// Token: 0x04000CF0 RID: 3312
	public MyVector myMember;

	// Token: 0x04000CF1 RID: 3313
	public MyVector logChat = new MyVector();

	// Token: 0x04000CF2 RID: 3314
	public MyVector vPlayerMenu = new MyVector();

	// Token: 0x04000CF3 RID: 3315
	public MyVector vFriend = new MyVector();

	// Token: 0x04000CF4 RID: 3316
	public MyVector vMyGD = new MyVector();

	// Token: 0x04000CF5 RID: 3317
	public MyVector vFriendGD = new MyVector();

	// Token: 0x04000CF6 RID: 3318
	public MyVector vTop = new MyVector();

	// Token: 0x04000CF7 RID: 3319
	public MyVector vEnemy = new MyVector();

	// Token: 0x04000CF8 RID: 3320
	public MyVector vFlag = new MyVector();

	// Token: 0x04000CF9 RID: 3321
	public MyVector vPlayerMenu_id = new MyVector();

	// Token: 0x04000CFA RID: 3322
	public Command cmdClose;

	// Token: 0x04000CFB RID: 3323
	public static bool CanNapTien = false;

	// Token: 0x04000CFC RID: 3324
	public static int WIDTH_PANEL = 240;

	// Token: 0x04000CFD RID: 3325
	internal int position;

	// Token: 0x04000CFE RID: 3326
	public string playerChat;

	// Token: 0x04000CFF RID: 3327
	public Dictionary<string, Panel.PlayerChat> chats = new Dictionary<string, Panel.PlayerChat>();

	// Token: 0x04000D00 RID: 3328
	public global::Char charMenu;

	// Token: 0x04000D01 RID: 3329
	internal bool isThachDau;

	// Token: 0x04000D02 RID: 3330
	public int typeShop = -1;

	// Token: 0x04000D03 RID: 3331
	public int xScroll;

	// Token: 0x04000D04 RID: 3332
	public int yScroll;

	// Token: 0x04000D05 RID: 3333
	public int wScroll;

	// Token: 0x04000D06 RID: 3334
	public int hScroll;

	// Token: 0x04000D07 RID: 3335
	public ChatPopup cp;

	// Token: 0x04000D08 RID: 3336
	public int idIcon;

	// Token: 0x04000D09 RID: 3337
	public int[] partID;

	// Token: 0x04000D0A RID: 3338
	internal int timeShow;

	// Token: 0x04000D0B RID: 3339
	public bool isBoxClan;

	// Token: 0x04000D0C RID: 3340
	public int w;

	// Token: 0x04000D0D RID: 3341
	internal int pa;

	// Token: 0x04000D0E RID: 3342
	public int selected;

	// Token: 0x04000D0F RID: 3343
	internal int cSelected;

	// Token: 0x04000D10 RID: 3344
	internal int newSelected;

	// Token: 0x04000D11 RID: 3345
	internal bool isClanOption;

	// Token: 0x04000D12 RID: 3346
	public bool isSearchClan;

	// Token: 0x04000D13 RID: 3347
	public bool isMessage;

	// Token: 0x04000D14 RID: 3348
	public bool isViewMember;

	// Token: 0x04000D15 RID: 3349
	public const int TYPE_MAIN = 0;

	// Token: 0x04000D16 RID: 3350
	public const int TYPE_SHOP = 1;

	// Token: 0x04000D17 RID: 3351
	public const int TYPE_BOX = 2;

	// Token: 0x04000D18 RID: 3352
	public const int TYPE_ZONE = 3;

	// Token: 0x04000D19 RID: 3353
	public const int TYPE_MAP = 4;

	// Token: 0x04000D1A RID: 3354
	public const int TYPE_CLANS = 5;

	// Token: 0x04000D1B RID: 3355
	public const int TYPE_INFOMATION = 6;

	// Token: 0x04000D1C RID: 3356
	public const int TYPE_BODY = 7;

	// Token: 0x04000D1D RID: 3357
	public const int TYPE_MESS = 8;

	// Token: 0x04000D1E RID: 3358
	public const int TYPE_ARCHIVEMENT = 9;

	// Token: 0x04000D1F RID: 3359
	public const int PLAYER_MENU = 10;

	// Token: 0x04000D20 RID: 3360
	public const int TYPE_FRIEND = 11;

	// Token: 0x04000D21 RID: 3361
	public const int TYPE_COMBINE = 12;

	// Token: 0x04000D22 RID: 3362
	public const int TYPE_GIAODICH = 13;

	// Token: 0x04000D23 RID: 3363
	public const int TYPE_MAPTRANS = 14;

	// Token: 0x04000D24 RID: 3364
	public const int TYPE_TOP = 15;

	// Token: 0x04000D25 RID: 3365
	public const int TYPE_ENEMY = 16;

	// Token: 0x04000D26 RID: 3366
	public const int TYPE_KIGUI = 17;

	// Token: 0x04000D27 RID: 3367
	public const int TYPE_FLAG = 18;

	// Token: 0x04000D28 RID: 3368
	public const int TYPE_OPTION = 19;

	// Token: 0x04000D29 RID: 3369
	public const int TYPE_ACCOUNT = 20;

	// Token: 0x04000D2A RID: 3370
	public const int TYPE_PET_MAIN = 21;

	// Token: 0x04000D2B RID: 3371
	public const int TYPE_AUTO = 22;

	// Token: 0x04000D2C RID: 3372
	public const int TYPE_GAMEINFO = 23;

	// Token: 0x04000D2D RID: 3373
	public const int TYPE_GAMEINFOSUB = 24;

	// Token: 0x04000D2E RID: 3374
	public const int TYPE_SPEACIALSKILL = 25;

	// Token: 0x04000D2F RID: 3375
	internal int pointerDownTime;

	// Token: 0x04000D30 RID: 3376
	internal int pointerDownFirstX;

	// Token: 0x04000D31 RID: 3377
	internal int[] pointerDownLastX = new int[3];

	// Token: 0x04000D32 RID: 3378
	internal bool pointerIsDowning;

	// Token: 0x04000D33 RID: 3379
	internal bool isDownWhenRunning;

	// Token: 0x04000D34 RID: 3380
	internal bool wantUpdateList;

	// Token: 0x04000D35 RID: 3381
	internal int waitToPerform;

	// Token: 0x04000D36 RID: 3382
	internal int cmRun;

	// Token: 0x04000D37 RID: 3383
	internal int keyTouchLock = -1;

	// Token: 0x04000D38 RID: 3384
	internal int keyToundGD = -1;

	// Token: 0x04000D39 RID: 3385
	internal int keyTouchCombine = -1;

	// Token: 0x04000D3A RID: 3386
	internal int keyTouchMapButton = -1;

	// Token: 0x04000D3B RID: 3387
	public int indexMouse = -1;

	// Token: 0x04000D3C RID: 3388
	internal bool justRelease;

	// Token: 0x04000D3D RID: 3389
	internal int keyTouchTab = -1;

	// Token: 0x04000D3E RID: 3390
	internal int nTableItem;

	// Token: 0x04000D3F RID: 3391
	public string[][] clansOption = new string[][]
	{
		mResources.findClan,
		mResources.createClan
	};

	// Token: 0x04000D40 RID: 3392
	public string clanInfo = string.Empty;

	// Token: 0x04000D41 RID: 3393
	public string clanReport = string.Empty;

	// Token: 0x04000D42 RID: 3394
	internal bool isHaveClan;

	// Token: 0x04000D43 RID: 3395
	internal Scroll scroll;

	// Token: 0x04000D44 RID: 3396
	internal int cmvx;

	// Token: 0x04000D45 RID: 3397
	internal int cmdx;

	// Token: 0x04000D46 RID: 3398
	internal bool isSelectPlayerMenu;

	// Token: 0x04000D47 RID: 3399
	internal string[] strStatus = new string[]
	{
		mResources.follow,
		mResources.defend,
		mResources.attack,
		mResources.gohome,
		mResources.fusion,
		mResources.fusionForever
	};

	// Token: 0x04000D48 RID: 3400
	internal static string log;

	// Token: 0x04000D49 RID: 3401
	internal int tt;

	// Token: 0x04000D4A RID: 3402
	internal int currentButtonPress;

	// Token: 0x04000D4B RID: 3403
	public static long[] t_tiemnang = new long[]
	{
		50000000L, 250000000L, 1250000000L, 5000000000L, 15000000000L, 30000000000L, 45000000000L, 60000000000L, 75000000000L, 90000000000L,
		110000000000L, 130000000000L, 150000000000L, 170000000000L
	};

	// Token: 0x04000D4C RID: 3404
	internal int[] zoneColor = new int[] { 43520, 14743570, 14155776 };

	// Token: 0x04000D4D RID: 3405
	public string[] combineInfo;

	// Token: 0x04000D4E RID: 3406
	public string[] combineTopInfo;

	// Token: 0x04000D4F RID: 3407
	public static int[] color1 = new int[] { 2327248, 8982199, 16713222 };

	// Token: 0x04000D50 RID: 3408
	public static int[] color2 = new int[] { 4583423, 16719103, 16714764 };

	// Token: 0x04000D51 RID: 3409
	internal int sellectInventory;

	// Token: 0x04000D52 RID: 3410
	internal Item itemInvenNew;

	// Token: 0x04000D53 RID: 3411
	internal Effect eBanner;

	// Token: 0x04000D54 RID: 3412
	internal static FrameImage screenTab6;

	// Token: 0x04000D55 RID: 3413
	internal bool isUp;

	// Token: 0x04000D56 RID: 3414
	internal int compare;

	// Token: 0x04000D57 RID: 3415
	public static string strWantToBuy = string.Empty;

	// Token: 0x04000D58 RID: 3416
	public int xstart;

	// Token: 0x04000D59 RID: 3417
	public int ystart;

	// Token: 0x04000D5A RID: 3418
	public int popupW = 140;

	// Token: 0x04000D5B RID: 3419
	public int popupH = 160;

	// Token: 0x04000D5C RID: 3420
	public int cmySK;

	// Token: 0x04000D5D RID: 3421
	public int cmtoYSK;

	// Token: 0x04000D5E RID: 3422
	public int cmdySK;

	// Token: 0x04000D5F RID: 3423
	public int cmvySK;

	// Token: 0x04000D60 RID: 3424
	public int cmyLimSK;

	// Token: 0x04000D61 RID: 3425
	public int popupY;

	// Token: 0x04000D62 RID: 3426
	public int popupX;

	// Token: 0x04000D63 RID: 3427
	public int isborderIndex;

	// Token: 0x04000D64 RID: 3428
	public int isselectedRow;

	// Token: 0x04000D65 RID: 3429
	public int indexSize = 28;

	// Token: 0x04000D66 RID: 3430
	public int indexTitle;

	// Token: 0x04000D67 RID: 3431
	public int indexSelect;

	// Token: 0x04000D68 RID: 3432
	public int indexRow = -1;

	// Token: 0x04000D69 RID: 3433
	public int indexRowMax;

	// Token: 0x04000D6A RID: 3434
	public int indexMenu;

	// Token: 0x04000D6B RID: 3435
	public int columns = 6;

	// Token: 0x04000D6C RID: 3436
	public int rows;

	// Token: 0x04000D6D RID: 3437
	public int inforX;

	// Token: 0x04000D6E RID: 3438
	public int inforY;

	// Token: 0x04000D6F RID: 3439
	public int inforW;

	// Token: 0x04000D70 RID: 3440
	public int inforH;

	// Token: 0x04000D71 RID: 3441
	internal int yPaint;

	// Token: 0x04000D72 RID: 3442
	internal int xMap;

	// Token: 0x04000D73 RID: 3443
	internal int yMap;

	// Token: 0x04000D74 RID: 3444
	internal int xMapTask;

	// Token: 0x04000D75 RID: 3445
	internal int yMapTask;

	// Token: 0x04000D76 RID: 3446
	internal int xMove;

	// Token: 0x04000D77 RID: 3447
	internal int yMove;

	// Token: 0x04000D78 RID: 3448
	public static bool isPaintMap = true;

	// Token: 0x04000D79 RID: 3449
	public bool isClose;

	// Token: 0x04000D7A RID: 3450
	internal int infoSelect;

	// Token: 0x04000D7B RID: 3451
	public static MyVector vGameInfo = new MyVector(string.Empty);

	// Token: 0x04000D7C RID: 3452
	public static string[] contenInfo;

	// Token: 0x04000D7D RID: 3453
	public bool isViewChatServer;

	// Token: 0x04000D7E RID: 3454
	internal int currInfoItem;

	// Token: 0x04000D7F RID: 3455
	public global::Char charInfo;

	// Token: 0x04000D80 RID: 3456
	internal bool isChangeZone;

	// Token: 0x04000D81 RID: 3457
	internal bool isKiguiXu;

	// Token: 0x04000D82 RID: 3458
	internal bool isKiguiLuong;

	// Token: 0x04000D83 RID: 3459
	internal int delayKigui;

	// Token: 0x04000D84 RID: 3460
	public sbyte combineSuccess = -1;

	// Token: 0x04000D85 RID: 3461
	public int idNPC;

	// Token: 0x04000D86 RID: 3462
	public int xS;

	// Token: 0x04000D87 RID: 3463
	public int yS;

	// Token: 0x04000D88 RID: 3464
	internal int rS;

	// Token: 0x04000D89 RID: 3465
	internal int angleS;

	// Token: 0x04000D8A RID: 3466
	internal int angleO;

	// Token: 0x04000D8B RID: 3467
	internal int iAngleS;

	// Token: 0x04000D8C RID: 3468
	internal int iDotS;

	// Token: 0x04000D8D RID: 3469
	internal int speed;

	// Token: 0x04000D8E RID: 3470
	internal int[] xArgS;

	// Token: 0x04000D8F RID: 3471
	internal int[] yArgS;

	// Token: 0x04000D90 RID: 3472
	internal int[] xDotS;

	// Token: 0x04000D91 RID: 3473
	internal int[] yDotS;

	// Token: 0x04000D92 RID: 3474
	internal int time;

	// Token: 0x04000D93 RID: 3475
	internal int typeCombine;

	// Token: 0x04000D94 RID: 3476
	internal int countUpdate;

	// Token: 0x04000D95 RID: 3477
	internal int countR;

	// Token: 0x04000D96 RID: 3478
	internal int countWait;

	// Token: 0x04000D97 RID: 3479
	internal bool isSpeedCombine;

	// Token: 0x04000D98 RID: 3480
	internal bool isCompleteEffCombine = true;

	// Token: 0x04000D99 RID: 3481
	internal bool isPaintCombine;

	// Token: 0x04000D9A RID: 3482
	public bool isDoneCombine = true;

	// Token: 0x04000D9B RID: 3483
	public short iconID1;

	// Token: 0x04000D9C RID: 3484
	public short iconID2;

	// Token: 0x04000D9D RID: 3485
	public short iconID3;

	// Token: 0x04000D9E RID: 3486
	public short[] iconID;

	// Token: 0x04000D9F RID: 3487
	public string[][] speacialTabName;

	// Token: 0x04000DA0 RID: 3488
	public static int[] sizeUpgradeEff = new int[] { 2, 1, 1 };

	// Token: 0x04000DA1 RID: 3489
	public static int nsize = 1;

	// Token: 0x04000DA2 RID: 3490
	public const sbyte COLOR_WHITE = 0;

	// Token: 0x04000DA3 RID: 3491
	public const sbyte COLOR_GREEN = 1;

	// Token: 0x04000DA4 RID: 3492
	public const sbyte COLOR_PURPLE = 2;

	// Token: 0x04000DA5 RID: 3493
	public const sbyte COLOR_ORANGE = 3;

	// Token: 0x04000DA6 RID: 3494
	public const sbyte COLOR_BLUE = 4;

	// Token: 0x04000DA7 RID: 3495
	public const sbyte COLOR_YELLOW = 5;

	// Token: 0x04000DA8 RID: 3496
	public const sbyte COLOR_RED = 6;

	// Token: 0x04000DA9 RID: 3497
	public const sbyte COLOR_BLACK = 7;

	// Token: 0x04000DAA RID: 3498
	public static int[][] colorUpgradeEffect = new int[][]
	{
		new int[] { 16777215, 15000805, 13487823, 11711155, 9671828, 7895160 },
		new int[] { 61952, 58624, 52224, 45824, 39168, 32768 },
		new int[] { 13500671, 12058853, 10682572, 9371827, 7995545, 6684800 },
		new int[] { 16744192, 15037184, 13395456, 11753728, 10046464, 8404992 },
		new int[] { 37119, 33509, 28108, 24499, 21145, 17536 },
		new int[] { 16776192, 15063040, 12635136, 11776256, 10063872, 8290304 },
		new int[] { 16711680, 15007744, 13369344, 11730944, 10027008, 8388608 }
	};

	// Token: 0x04000DAB RID: 3499
	public const int color_item_white = 15987701;

	// Token: 0x04000DAC RID: 3500
	public const int color_item_green = 2786816;

	// Token: 0x04000DAD RID: 3501
	public const int color_item_purple = 7078041;

	// Token: 0x04000DAE RID: 3502
	public const int color_item_orange = 12537346;

	// Token: 0x04000DAF RID: 3503
	public const int color_item_blue = 1269146;

	// Token: 0x04000DB0 RID: 3504
	public const int color_item_yellow = 13279744;

	// Token: 0x04000DB1 RID: 3505
	public const int color_item_red = 11599872;

	// Token: 0x04000DB2 RID: 3506
	public const int color_item_black = 2039326;

	// Token: 0x04000DB3 RID: 3507
	internal Image imgo_0;

	// Token: 0x04000DB4 RID: 3508
	internal Image imgo_1;

	// Token: 0x04000DB5 RID: 3509
	internal Image imgo_2;

	// Token: 0x04000DB6 RID: 3510
	internal Image imgo_3;

	// Token: 0x04000DB7 RID: 3511
	public const int numItem = 20;

	// Token: 0x04000DB8 RID: 3512
	public const sbyte INVENTORY_TAB = 1;

	// Token: 0x04000DB9 RID: 3513
	public sbyte size_tab;

	// Token: 0x04000DBA RID: 3514
	internal bool isnewInventory;

	// Token: 0x02000085 RID: 133
	public class PlayerChat
	{
		// Token: 0x06000736 RID: 1846 RVA: 0x00073756 File Offset: 0x00071956
		public PlayerChat(string name, int charId)
		{
			this.name = name;
			this.charID = charId;
			this.isNewMessage = true;
		}

		// Token: 0x04000DBB RID: 3515
		public string name;

		// Token: 0x04000DBC RID: 3516
		public int charID;

		// Token: 0x04000DBD RID: 3517
		public bool isNewMessage;

		// Token: 0x04000DBE RID: 3518
		public List<InfoItem> chats = new List<InfoItem>();
	}
}
