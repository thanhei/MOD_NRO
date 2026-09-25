using System;
using Assets.src.g;
using Mod.Constants;
using Mod.ModHelper;
using Mod.R;
using Mod.Xmap;
using UnityEngine;

namespace Mod.Auto
{
	// Token: 0x02000183 RID: 387
	internal class AutoTrainNewAccount
	{
		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x000BB30B File Offset: 0x000B950B
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x000BB312 File Offset: 0x000B9512
		internal static bool IsTanSat
		{
			get
			{
				return AutoTrainNewAccount._isTanSatInternal;
			}
			set
			{
				if (value == AutoTrainNewAccount._isTanSatInternal)
				{
					return;
				}
				AutoTrainNewAccount.lastTimeCheckTN = mSystem.currentTimeMillis();
				AutoTrainNewAccount._isTanSatInternal = value;
			}
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000BB330 File Offset: 0x000B9530
		internal static void Update()
		{
			if (!AutoTrainNewAccount.isEnabled)
			{
				return;
			}
			try
			{
				if (global::Char.myCharz().taskMaint.taskId > 11)
				{
					GameScr.info1.addInfo(Strings.completed + "!", 0);
					AutoTrainNewAccount.isEnabled = false;
				}
				else
				{
					if (global::Char.myCharz().taskMaint.taskId < 9)
					{
						for (int i = GameScr.vNpc.size() - 1; i >= 0; i--)
						{
							if (string.IsNullOrEmpty(((Npc)GameScr.vNpc.elementAt(i)).template.name))
							{
								GameScr.vNpc.removeElementAt(i);
							}
						}
						GameScr.vCharInMap.removeAllElements();
					}
					if (AutoTrainNewAccount.isNhapCodeTanThu && (float)GameCanvas.gameTick % (60f * Time.timeScale) == 0f)
					{
						TField tfield = new TField();
						tfield.setText("tan thu nro");
						Service.gI().sendClientInput(new TField[] { tfield });
						GameScr.gI().switchToMe();
						ClientInput.instance = null;
						global::Char.chatPopup = null;
						AutoTrainNewAccount.isNhapCodeTanThu = false;
					}
					if (GameScr.hpPotion <= 0 && (global::Char.myCharz().cMP < 15 || global::Char.myCharz().cHP < 15))
					{
						if (!AutoTrainNewAccount.isHarvestingPean)
						{
							AutoTrainNewAccount.isHarvestingPean = true;
						}
						AutoTrainNewAccount.IsTanSat = (AutoTrainNewAccount.isPKKarinSama = (AutoTrainNewAccount.isPKT77 = false));
						if (TileMap.mapID != global::Char.myCharz().cgender + 21 && !ThreadAction<XmapController>.gI.IsActing)
						{
							XmapController.start(global::Char.myCharz().cgender + 21);
						}
					}
					if ((global::Char.myCharz().cMP < AutoTrainNewAccount.myMinMP || global::Char.myCharz().cHP < AutoTrainNewAccount.myMinHP) && !AutoTrainNewAccount.isHarvestingPean && (TileMap.mapID != global::Char.myCharz().cgender + 21 || global::Char.myCharz().taskMaint.taskId < 3) && mSystem.currentTimeMillis() - AutoTrainNewAccount.lastTimeEatPean > 2000L && (AutoTrainNewAccount.minPeans <= 0 || GameScr.hpPotion >= AutoTrainNewAccount.minPeans))
					{
						AutoTrainNewAccount.lastTimeEatPean = mSystem.currentTimeMillis();
						GameScr.gI().doUseHP();
					}
					if ((global::Char.myCharz().isDie || global::Char.myCharz().cHP <= 0) && (float)GameCanvas.gameTick % (30f * Time.timeScale) == 0f)
					{
						Service.gI().returnTownFromDead();
					}
					if (TileMap.mapID == global::Char.myCharz().cgender + 21)
					{
						if (!AutoTrainNewAccount.isHarvestingPean && AutoTrainNewAccount.minPeans > 0 && GameScr.hpPotion < AutoTrainNewAccount.minPeans)
						{
							AutoTrainNewAccount.isHarvestingPean = true;
						}
						if (GameScr.vItemMap.size() > 0)
						{
							ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(0);
							if (mSystem.currentTimeMillis() - AutoTrainNewAccount.lastTimePickedItem >= 550L)
							{
								AutoTrainNewAccount.lastTimePickedItem = mSystem.currentTimeMillis();
								Service.gI().pickItem(itemMap.itemMapID);
							}
						}
						if (AutoTrainNewAccount.minPeans <= 0)
						{
							if (GameScr.gI().magicTree.currPeas == 0 || (global::Char.myCharz().cgender == 1 && GameScr.hpPotion >= 30) || (global::Char.myCharz().cgender != 1 && GameScr.hpPotion >= 20))
							{
								AutoTrainNewAccount.isHarvestingPean = false;
							}
						}
						else if (GameScr.hpPotion >= AutoTrainNewAccount.minPeans)
						{
							AutoTrainNewAccount.isHarvestingPean = false;
						}
						if (global::Char.myCharz().taskMaint.taskId >= 2 && GameScr.gI().magicTree.currPeas > 0 && ((global::Char.myCharz().cgender == 1 && GameScr.hpPotion < 30) || (global::Char.myCharz().cgender != 1 && GameScr.hpPotion < 20)) && (float)GameCanvas.gameTick % (30f * Time.timeScale) == 0f)
						{
							Service.gI().openMenu(4);
							Service.gI().confirmMenu(4, 0);
						}
						if (global::Char.myCharz().xu >= 5000L && GameScr.gI().magicTree.level == 1 && (GameScr.gI().magicTree.strInfo != LocalizedString.senzuTreeUpgrading || !GameScr.gI().magicTree.isUpdate) && (float)GameCanvas.gameTick % (60f * Time.timeScale) == 0f)
						{
							Service.gI().openMenu(4);
							Service.gI().confirmMenu(4, 1);
							Service.gI().confirmMenu(5, 0);
							GameScr.gI().magicTree.strInfo = LocalizedString.senzuTreeUpgrading;
							GameScr.gI().magicTree.isUpdate = true;
							AutoTrainNewAccount.isHarvestingPean = false;
						}
						if (GameCanvas.menu.showMenu)
						{
							GameCanvas.menu.doCloseMenu();
						}
					}
					if (!AutoTrainNewAccount.isNhapCodeTanThu && !AutoTrainNewAccount.isHarvestingPean && !AutoTrainNewAccount.isPicking && GameCanvas.gameTick % (30 * (int)Time.timeScale) == 0 && global::Char.myCharz().cHP > 1 && !GameScr.gI().isBagFull() && global::Char.myCharz().taskMaint.taskId <= 11)
					{
						AutoTrainNewAccount.AutoNV();
					}
					if (global::Char.myCharz().taskMaint.taskId > 3 && global::Char.myCharz().taskMaint.taskId <= 11)
					{
						AutoTrainNewAccount.AutoPoint();
					}
					if (!ThreadAction<XmapController>.gI.IsActing && !AutoTrainNewAccount.isNhapCodeTanThu && !AutoTrainNewAccount.isHarvestingPean && global::Char.myCharz().cHP > 1 && !GameScr.gI().isBagFull() && global::Char.myCharz().taskMaint.taskId <= 11 && (AutoTrainNewAccount.minPeans <= 0 || GameScr.hpPotion >= AutoTrainNewAccount.minPeans))
					{
						if (AutoTrainNewAccount.IsTanSat && !AutoTrainNewAccount.AutoPick())
						{
							AutoTrainNewAccount.TanSat();
						}
						if (AutoTrainNewAccount.isPKKarinSama)
						{
							AutoTrainNewAccount.PKThanMeo();
						}
						else if (AutoTrainNewAccount.isPKT77)
						{
							AutoTrainNewAccount.PKT77();
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000BB8E0 File Offset: 0x000B9AE0
		private static void AutoPoint()
		{
			if (mSystem.currentTimeMillis() - AutoTrainNewAccount.lastTimeAutoPoint >= 1000L)
			{
				AutoTrainNewAccount.lastTimeAutoPoint = mSystem.currentTimeMillis();
				if ((global::Char.myCharz().cHPGoc < 400 || (global::Char.myCharz().cDamGoc >= 40 && global::Char.myCharz().cHPGoc < 500)) && global::Char.myCharz().cTiemNang > (long)(global::Char.myCharz().cHPGoc + 1000))
				{
					Service.gI().upPotential(0, 1);
					return;
				}
				if (global::Char.myCharz().cMPGoc < 300 && global::Char.myCharz().cDamGoc >= 25 && global::Char.myCharz().cTiemNang > (long)(global::Char.myCharz().cMPGoc + 1000))
				{
					Service.gI().upPotential(1, 1);
					return;
				}
				if (global::Char.myCharz().cDamGoc < 70 && global::Char.myCharz().cTiemNang > (long)(global::Char.myCharz().cDamGoc * 100))
				{
					Service.gI().upPotential(2, 1);
				}
			}
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000BB9E4 File Offset: 0x000B9BE4
		private static bool AutoPick()
		{
			if (GameScr.vItemMap.size() == 0)
			{
				AutoTrainNewAccount.isPicking = false;
				return false;
			}
			bool flag = false;
			for (int i = GameScr.vItemMap.size() - 1; i >= 0; i--)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
				if (itemMap != null)
				{
					int num = Res.distance(global::Char.myCharz().cx, global::Char.myCharz().cy, itemMap.x, itemMap.y);
					if (itemMap.playerId == global::Char.myCharz().charID || (itemMap.playerId == -1 && num <= 60) || itemMap.template.id == 74)
					{
						flag = true;
					}
					if ((itemMap.template.id >= 828 && itemMap.template.id <= 842) || itemMap.template.id == 859 || itemMap.template.id == 362 || (itemMap.template.id >= 353 && itemMap.template.id <= 360))
					{
						GameScr.vItemMap.removeElementAt(i);
					}
					else if (mSystem.currentTimeMillis() - AutoTrainNewAccount.lastTimePickedItem > 550L)
					{
						if (itemMap.playerId == global::Char.myCharz().charID)
						{
							AutoTrainNewAccount.isPicking = true;
							global::Char.myCharz().mobFocus = null;
							if (num > 60 && num < 100)
							{
								global::Char.myCharz().currentMovePoint = new MovePoint(itemMap.x, itemMap.y);
							}
							if (num >= 100)
							{
								Utils.TeleportMyChar(itemMap.x, itemMap.y);
							}
							Service.gI().pickItem(itemMap.itemMapID);
							AutoTrainNewAccount.lastTimePickedItem = mSystem.currentTimeMillis();
						}
						else if ((itemMap.playerId == -1 && num <= 60) || itemMap.template.id == 74)
						{
							AutoTrainNewAccount.isPicking = true;
							global::Char.myCharz().mobFocus = null;
							Service.gI().pickItem(itemMap.itemMapID);
							AutoTrainNewAccount.lastTimePickedItem = mSystem.currentTimeMillis();
						}
					}
				}
			}
			if (!flag)
			{
				AutoTrainNewAccount.isPicking = false;
			}
			return flag;
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000BBBF0 File Offset: 0x000B9DF0
		private static void TanSat()
		{
			Skill myskill = global::Char.myCharz().myskill;
			if (AutoTrainNewAccount.isPicking || mSystem.currentTimeMillis() - myskill.lastTimeUseThisSkill <= (long)myskill.coolDown + 100L)
			{
				return;
			}
			Mob mob = AutoTrainNewAccount.ClosestMob();
			if (mob == null)
			{
				return;
			}
			if (mSystem.currentTimeMillis() - AutoTrainNewAccount.lastTimeCheckTN > 3000L)
			{
				AutoTrainNewAccount.lastTimeCheckTN = mSystem.currentTimeMillis();
				if (global::Char.myCharz().cTiemNang == AutoTrainNewAccount.lastTN)
				{
					Utils.TeleportMyChar(global::Char.myCharz());
				}
				AutoTrainNewAccount.lastTN = global::Char.myCharz().cTiemNang;
			}
			if (mob.getTemplate().type == MonsterType.Fly)
			{
				if (Math.Abs(global::Char.myCharz().cx - mob.x) > 70)
				{
					Utils.TeleportMyChar(mob.x);
				}
				else
				{
					global::Char.myCharz().cx = mob.x + Res.random(-5, 5);
					global::Char.myCharz().cy = mob.y + Res.random(-5, 5);
					Service.gI().charMove();
				}
			}
			else
			{
				global::Char.myCharz().currentMovePoint = new MovePoint(mob.x, mob.y);
			}
			if (Utils.Distance(global::Char.myCharz(), mob) <= 50 || (mob.getTemplate().type == MonsterType.Fly && Math.Abs(global::Char.myCharz().cx - mob.x) <= 70))
			{
				myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
				global::Char.myCharz().mobFocus = mob;
				MyVector myVector = new MyVector();
				myVector.addElement(mob);
				Service.gI().sendPlayerAttack(myVector, new MyVector(), -1);
			}
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x000BBD7C File Offset: 0x000B9F7C
		private static void TrainUntilMeStrongEnough(int maxHPmob)
		{
			if (AutoTrainNewAccount.myMinHP != 15)
			{
				AutoTrainNewAccount.myMinHP = 15;
			}
			if (AutoTrainNewAccount.myMinMP != 15)
			{
				AutoTrainNewAccount.myMinMP = 15;
			}
			if (AutoTrainNewAccount.isNhanBua && GameCanvas.menu.showMenu && TileMap.mapID == global::Char.myCharz().cgender + 42)
			{
				GameCanvas.menu.doCloseMenu();
			}
			if (!AutoTrainNewAccount.isNhanBua)
			{
				if (TileMap.mapID != global::Char.myCharz().cgender + 42)
				{
					if (!ThreadAction<XmapController>.gI.IsActing)
					{
						AutoTrainNewAccount.IsTanSat = false;
						XmapController.start(global::Char.myCharz().cgender + 42);
						return;
					}
				}
				else
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(0);
					Utils.TeleportMyChar(npc.cx);
					if (Res.distance(global::Char.myCharz().cx, global::Char.myCharz().cy, npc.cx, npc.cy) <= 50)
					{
						if (!GameCanvas.menu.showMenu)
						{
							Service.gI().openMenu(21);
							return;
						}
						if (((Command)GameCanvas.menu.menuItems.elementAt(0)).caption.Replace('\n', ' ').ToLower() == LocalizedString.free1hCharm)
						{
							Service.gI().confirmMenu(21, 0);
						}
						AutoTrainNewAccount.isNhanBua = true;
						return;
					}
				}
			}
			else if (TileMap.mapID == 9 || TileMap.mapID == 3 || TileMap.mapID == 17)
			{
				if (AutoTrainNewAccount.maxHPMob != maxHPmob)
				{
					AutoTrainNewAccount.maxHPMob = maxHPmob;
				}
				if (AutoTrainNewAccount.minHPMob != 0)
				{
					AutoTrainNewAccount.minHPMob = 0;
				}
				if (!AutoTrainNewAccount.IsTanSat)
				{
					AutoTrainNewAccount.IsTanSat = true;
					return;
				}
			}
			else if (!ThreadAction<XmapController>.gI.IsActing)
			{
				AutoTrainNewAccount.IsTanSat = false;
				if (global::Char.myCharz().cgender == 0)
				{
					XmapController.start(3);
				}
				if (global::Char.myCharz().cgender == 1)
				{
					XmapController.start(9);
				}
				if (global::Char.myCharz().cgender == 2)
				{
					XmapController.start(17);
				}
			}
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000BBF58 File Offset: 0x000BA158
		private static Mob ClosestMob()
		{
			Mob mob = null;
			int num = int.MaxValue;
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(i);
				if (mob2.status != 0 && mob2.status != 1 && mob2.hp > 0 && !mob2.isMobMe && mob2.maxHp <= AutoTrainNewAccount.maxHPMob && mob2.maxHp >= AutoTrainNewAccount.minHPMob)
				{
					int num2 = Res.distance(mob2.x, mob2.y, global::Char.myCharz().cx, global::Char.myCharz().cy);
					if (num > num2)
					{
						num = num2;
						mob = mob2;
					}
				}
			}
			return mob;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000BC008 File Offset: 0x000BA208
		private static void AutoNV()
		{
			switch (global::Char.myCharz().taskMaint.taskId)
			{
			case 0:
				AutoTrainNewAccount.AutoNV0();
				return;
			case 1:
				AutoTrainNewAccount.AutoNV1();
				return;
			case 2:
				AutoTrainNewAccount.AutoNV2();
				return;
			case 3:
				AutoTrainNewAccount.AutoNV3();
				return;
			case 4:
			case 5:
			case 6:
				AutoTrainNewAccount.AutoNV4to6();
				return;
			case 7:
				AutoTrainNewAccount.AutoNV7();
				return;
			case 8:
				AutoTrainNewAccount.AutoNV8();
				return;
			case 9:
				AutoTrainNewAccount.AutoNV9();
				return;
			case 10:
				AutoTrainNewAccount.AutoNV10();
				return;
			case 11:
				AutoTrainNewAccount.AutoNV11();
				return;
			default:
				return;
			}
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000BC098 File Offset: 0x000BA298
		private static void AutoNV0()
		{
			if (TileMap.mapID >= 39 && TileMap.mapID <= 41)
			{
				Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(0);
				Utils.TeleportMyChar((int)(waypoint.maxX - 20), (int)waypoint.maxY);
				return;
			}
			if (TileMap.mapID >= 21 && TileMap.mapID <= 23)
			{
				if (global::Char.myCharz().taskMaint.index == 2)
				{
					if (global::Char.myCharz().cgender == 0)
					{
						Service.gI().openMenu(0);
					}
					if (global::Char.myCharz().cgender == 1)
					{
						Service.gI().openMenu(2);
					}
					if (global::Char.myCharz().cgender == 2)
					{
						Service.gI().openMenu(1);
						return;
					}
				}
				else if (global::Char.myCharz().taskMaint.index == 3)
				{
					if (global::Char.myCharz().cgender == 0)
					{
						if (Math.Abs(global::Char.myCharz().cx - 85) <= 10 && Math.Abs(global::Char.myCharz().cy - 336) <= 10)
						{
							Service.gI().getItem(0, 0);
						}
						else
						{
							Utils.TeleportMyChar(85, 336);
						}
					}
					if (global::Char.myCharz().cgender == 2)
					{
						if (Math.Abs(global::Char.myCharz().cx - 94) <= 10 && Math.Abs(global::Char.myCharz().cy - 336) <= 10)
						{
							Service.gI().getItem(0, 0);
						}
						else
						{
							Utils.TeleportMyChar(94, 336);
						}
					}
					if (global::Char.myCharz().cgender == 1)
					{
						if (Math.Abs(global::Char.myCharz().cx - 638) <= 10 && Math.Abs(global::Char.myCharz().cy - 336) <= 10)
						{
							Service.gI().getItem(0, 0);
							return;
						}
						Utils.TeleportMyChar(638, 336);
						return;
					}
				}
				else
				{
					if (global::Char.myCharz().taskMaint.index == 4)
					{
						Service.gI().openMenu(4);
						Service.gI().confirmMenu(4, 0);
						return;
					}
					if (global::Char.myCharz().taskMaint.index == 5)
					{
						if (GameCanvas.menu.showMenu)
						{
							GameCanvas.menu.doCloseMenu();
						}
						if (global::Char.myCharz().cgender == 0)
						{
							Service.gI().openMenu(0);
						}
						if (global::Char.myCharz().cgender == 1)
						{
							Service.gI().openMenu(2);
						}
						if (global::Char.myCharz().cgender == 2)
						{
							Service.gI().openMenu(1);
						}
					}
				}
			}
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000BC30C File Offset: 0x000BA50C
		private static void AutoNV1()
		{
			if (global::Char.myCharz().taskMaint.index == 0)
			{
				global::Char.myCharz().npcFocus = null;
				if (TileMap.mapID >= 21 && TileMap.mapID <= 23)
				{
					if (!ThreadAction<XmapController>.gI.IsActing)
					{
						XmapController.start(global::Char.myCharz().cgender * 7);
						return;
					}
				}
				else if (TileMap.mapID == global::Char.myCharz().cgender * 7)
				{
					AutoTrainNewAccount.IsTanSat = true;
					return;
				}
			}
			else if (global::Char.myCharz().taskMaint.index == 1)
			{
				AutoTrainNewAccount.IsTanSat = false;
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().itemFocus = null;
				global::Char.myCharz().charFocus = null;
				if (!ThreadAction<XmapController>.gI.IsActing && TileMap.mapID != global::Char.myCharz().cgender + 21)
				{
					XmapController.start(global::Char.myCharz().cgender + 21);
					return;
				}
				if (TileMap.mapID == global::Char.myCharz().cgender + 21)
				{
					if (global::Char.myCharz().cgender == 0)
					{
						Service.gI().openMenu(0);
					}
					if (global::Char.myCharz().cgender == 1)
					{
						Service.gI().openMenu(2);
					}
					if (global::Char.myCharz().cgender == 2)
					{
						Service.gI().openMenu(1);
					}
				}
			}
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000BC450 File Offset: 0x000BA650
		private static void AutoNV2()
		{
			if (global::Char.myCharz().taskMaint.index == 0)
			{
				if (TileMap.mapID != global::Char.myCharz().cgender * 7 + 1)
				{
					if (!ThreadAction<XmapController>.gI.IsActing)
					{
						AutoTrainNewAccount.IsTanSat = false;
						XmapController.start(global::Char.myCharz().cgender * 7 + 1);
						return;
					}
				}
				else if (!AutoTrainNewAccount.IsTanSat)
				{
					AutoTrainNewAccount.IsTanSat = true;
					return;
				}
			}
			else if (global::Char.myCharz().taskMaint.index == 1)
			{
				global::Char.myCharz().mobFocus = null;
				AutoTrainNewAccount.IsTanSat = false;
				if (TileMap.mapID != global::Char.myCharz().cgender + 21)
				{
					if (!ThreadAction<XmapController>.gI.IsActing)
					{
						XmapController.start(global::Char.myCharz().cgender + 21);
						return;
					}
				}
				else
				{
					if (global::Char.myCharz().cgender == 0)
					{
						Service.gI().openMenu(0);
					}
					if (global::Char.myCharz().cgender == 1)
					{
						Service.gI().openMenu(2);
					}
					if (global::Char.myCharz().cgender == 2)
					{
						Service.gI().openMenu(1);
					}
				}
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000BC560 File Offset: 0x000BA760
		private static void AutoNV3()
		{
			if (global::Char.myCharz().taskMaint.index == 0)
			{
				Service.gI().upPotential(2, 1);
			}
			if (global::Char.myCharz().taskMaint.index == 1)
			{
				if (TileMap.mapID == global::Char.myCharz().cgender + 42)
				{
					if (global::Char.myCharz().cgender == 0)
					{
						Utils.TeleportMyChar(149, 288);
					}
					if (global::Char.myCharz().cgender == 1)
					{
						Utils.TeleportMyChar(126, 264);
					}
					if (global::Char.myCharz().cgender == 2)
					{
						Utils.TeleportMyChar(156, 288);
					}
					AutoTrainNewAccount.AutoPick();
					return;
				}
				if (!ThreadAction<XmapController>.gI.IsActing)
				{
					XmapController.start(global::Char.myCharz().cgender + 42);
					return;
				}
			}
			else if (global::Char.myCharz().taskMaint.index == 2)
			{
				if (TileMap.mapID != global::Char.myCharz().cgender + 21)
				{
					if (!ThreadAction<XmapController>.gI.IsActing)
					{
						XmapController.start(global::Char.myCharz().cgender + 21);
						return;
					}
				}
				else
				{
					if (global::Char.myCharz().cgender == 0)
					{
						Service.gI().openMenu(0);
					}
					if (global::Char.myCharz().cgender == 1)
					{
						Service.gI().openMenu(2);
					}
					if (global::Char.myCharz().cgender == 2)
					{
						Service.gI().openMenu(1);
					}
				}
			}
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x000BC6B8 File Offset: 0x000BA8B8
		private static void AutoNV4to6()
		{
			if (AutoTrainNewAccount.myMinHP != 30)
			{
				AutoTrainNewAccount.myMinHP = 30;
			}
			if (AutoTrainNewAccount.myMinMP != 15)
			{
				AutoTrainNewAccount.myMinHP = 15;
			}
			if (AutoTrainNewAccount.maxHPMob != 500)
			{
				AutoTrainNewAccount.maxHPMob = 500;
			}
			if (AutoTrainNewAccount.minHPMob != 499)
			{
				AutoTrainNewAccount.minHPMob = 499;
			}
			if (global::Char.myCharz().taskMaint.index < 3)
			{
				int num = 2 + global::Char.myCharz().cgender * 7;
				if (global::Char.myCharz().taskMaint.index == 1)
				{
					if (global::Char.myCharz().cgender == 0)
					{
						num = 9;
					}
					else
					{
						num = 2;
					}
				}
				if (global::Char.myCharz().taskMaint.index == 2)
				{
					if (global::Char.myCharz().cgender == 2)
					{
						num = 9;
					}
					else
					{
						num = 16;
					}
				}
				if (TileMap.mapID == num)
				{
					AutoTrainNewAccount.IsTanSat = true;
					return;
				}
				AutoTrainNewAccount.IsTanSat = false;
				if (!ThreadAction<XmapController>.gI.IsActing)
				{
					XmapController.start(num);
					return;
				}
			}
			else
			{
				AutoTrainNewAccount.IsTanSat = false;
				AutoTrainNewAccount.maxHPMob = int.MaxValue;
				AutoTrainNewAccount.minHPMob = 0;
				if (TileMap.mapID != global::Char.myCharz().cgender + 21)
				{
					if (!ThreadAction<XmapController>.gI.IsActing)
					{
						XmapController.start(global::Char.myCharz().cgender + 21);
						return;
					}
				}
				else
				{
					if (global::Char.myCharz().cgender == 0)
					{
						Service.gI().openMenu(0);
					}
					if (global::Char.myCharz().cgender == 1)
					{
						Service.gI().openMenu(2);
					}
					if (global::Char.myCharz().cgender == 2)
					{
						Service.gI().openMenu(1);
					}
				}
			}
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x000BC83C File Offset: 0x000BAA3C
		private static void AutoNV7()
		{
			if (global::Char.myCharz().cPower <= 78000L || global::Char.myCharz().taskMaint.index == 0)
			{
				AutoTrainNewAccount.TrainUntilMeStrongEnough(200);
				return;
			}
			if (global::Char.myCharz().taskMaint.index == 1)
			{
				if (TileMap.mapID == 3 || TileMap.mapID == 11 || TileMap.mapID == 17)
				{
					if (AutoTrainNewAccount.myMinHP != 45)
					{
						AutoTrainNewAccount.myMinHP = 45;
					}
					if (AutoTrainNewAccount.myMinMP != 15)
					{
						AutoTrainNewAccount.myMinHP = 15;
					}
					if (AutoTrainNewAccount.maxHPMob != 600)
					{
						AutoTrainNewAccount.maxHPMob = 600;
					}
					if (AutoTrainNewAccount.minHPMob != 599)
					{
						AutoTrainNewAccount.minHPMob = 599;
					}
					AutoTrainNewAccount.IsTanSat = true;
					return;
				}
				if (!ThreadAction<XmapController>.gI.IsActing)
				{
					AutoTrainNewAccount.IsTanSat = false;
					if (global::Char.myCharz().cgender == 0)
					{
						XmapController.start(3);
					}
					if (global::Char.myCharz().cgender == 1)
					{
						XmapController.start(11);
					}
					if (global::Char.myCharz().cgender == 2)
					{
						XmapController.start(17);
						return;
					}
				}
			}
			else if (global::Char.myCharz().taskMaint.index == 2)
			{
				AutoTrainNewAccount.IsTanSat = false;
				AutoTrainNewAccount.maxHPMob = int.MaxValue;
				AutoTrainNewAccount.minHPMob = 0;
				if (TileMap.mapID == global::Char.myCharz().cgender * 7)
				{
					Service.gI().openMenu(global::Char.myCharz().cgender + 7);
					return;
				}
				if (!ThreadAction<XmapController>.gI.IsActing)
				{
					XmapController.start(global::Char.myCharz().cgender * 7);
					return;
				}
			}
			else if (global::Char.myCharz().taskMaint.index == 3)
			{
				if (TileMap.mapID != global::Char.myCharz().cgender + 21)
				{
					if (!ThreadAction<XmapController>.gI.IsActing)
					{
						XmapController.start(global::Char.myCharz().cgender + 21);
						return;
					}
				}
				else
				{
					if (global::Char.myCharz().cgender == 0)
					{
						Service.gI().openMenu(0);
					}
					if (global::Char.myCharz().cgender == 1)
					{
						Service.gI().openMenu(2);
					}
					if (global::Char.myCharz().cgender == 2)
					{
						Service.gI().openMenu(1);
					}
				}
			}
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000BCA4C File Offset: 0x000BAC4C
		private static void AutoNV8()
		{
			if (global::Char.myCharz().cPower <= 140000L || global::Char.myCharz().taskMaint.index == 0)
			{
				AutoTrainNewAccount.TrainUntilMeStrongEnough(500);
				return;
			}
			if (global::Char.myCharz().taskMaint.index == 1)
			{
				if (AutoTrainNewAccount.myMinHP != 60)
				{
					AutoTrainNewAccount.myMinHP = 60;
				}
				if (AutoTrainNewAccount.myMinMP != 15)
				{
					AutoTrainNewAccount.myMinMP = 15;
				}
				if (!ThreadAction<XmapController>.gI.IsActing)
				{
					if (global::Char.myCharz().cgender == 0 && TileMap.mapID != 12)
					{
						XmapController.start(12);
					}
					else if (global::Char.myCharz().cgender == 1 && TileMap.mapID != 18)
					{
						XmapController.start(18);
					}
					else if (global::Char.myCharz().cgender == 2 && TileMap.mapID != 4)
					{
						XmapController.start(4);
					}
				}
				if (TileMap.mapID == 12 || TileMap.mapID == 18 || TileMap.mapID == 4)
				{
					AutoTrainNewAccount.maxHPMob = 1000;
					AutoTrainNewAccount.minHPMob = 999;
					if (!AutoTrainNewAccount.IsTanSat)
					{
						AutoTrainNewAccount.IsTanSat = true;
						return;
					}
				}
			}
			else if (global::Char.myCharz().taskMaint.index == 2)
			{
				AutoTrainNewAccount.IsTanSat = false;
				if (TileMap.mapID != global::Char.myCharz().cgender + 21)
				{
					if (!ThreadAction<XmapController>.gI.IsActing)
					{
						XmapController.start(global::Char.myCharz().cgender + 21);
						return;
					}
				}
				else
				{
					if (global::Char.myCharz().cgender == 0)
					{
						Service.gI().openMenu(0);
					}
					if (global::Char.myCharz().cgender == 1)
					{
						Service.gI().openMenu(2);
					}
					if (global::Char.myCharz().cgender == 2)
					{
						Service.gI().openMenu(1);
						return;
					}
				}
			}
			else if (global::Char.myCharz().taskMaint.index == 3 && TileMap.mapID != 47 && !ThreadAction<XmapController>.gI.IsActing)
			{
				XmapController.start(47);
			}
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000BCC2C File Offset: 0x000BAE2C
		private static void AutoNV9()
		{
			if (global::Char.myCharz().taskMaint.index <= 1)
			{
				if (TileMap.mapID == 47)
				{
					Service.gI().openMenu(17);
					return;
				}
				if (!ThreadAction<XmapController>.gI.IsActing)
				{
					XmapController.start(47);
					return;
				}
			}
			else if (global::Char.myCharz().taskMaint.index == 2)
			{
				if (TileMap.mapID != 47)
				{
					if (!ThreadAction<XmapController>.gI.IsActing)
					{
						XmapController.start(47);
						return;
					}
				}
				else
				{
					if (Math.Abs(global::Char.myCharz().cx - 600) >= 20)
					{
						Utils.TeleportMyChar(600, 336);
						return;
					}
					if (global::Char.myCharz().currentMovePoint == null || (global::Char.myCharz().currentMovePoint.xEnd != 600 && global::Char.myCharz().currentMovePoint.yEnd != 10))
					{
						global::Char.myCharz().currentMovePoint = new MovePoint(600, 10);
						return;
					}
				}
			}
			else if (global::Char.myCharz().taskMaint.index == 3)
			{
				if (TileMap.mapID == 46)
				{
					if (GameCanvas.menu.showMenu && GameCanvas.menu.menuItems.size() == 1)
					{
						Service.gI().confirmMenu(18, 0);
						GameCanvas.menu.doCloseMenu();
						return;
					}
					Service.gI().openMenu(18);
					return;
				}
				else if (TileMap.mapID == 47)
				{
					if (Math.Abs(global::Char.myCharz().cx - 600) >= 20)
					{
						Utils.TeleportMyChar(600, 336);
						return;
					}
					if (global::Char.myCharz().currentMovePoint == null || (global::Char.myCharz().currentMovePoint.xEnd != 600 && global::Char.myCharz().currentMovePoint.yEnd != 10))
					{
						global::Char.myCharz().currentMovePoint = new MovePoint(600, 10);
						return;
					}
				}
				else if (!ThreadAction<XmapController>.gI.IsActing)
				{
					XmapController.start(47);
				}
			}
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000BCE1C File Offset: 0x000BB01C
		private static void AutoNV10()
		{
			AutoTrainNewAccount.isPKKarinSama = false;
			AutoTrainNewAccount.isPKT77 = false;
			AutoTrainNewAccount.minPeans = ((global::Char.myCharz().taskMaint.index > 1) ? 0 : 7);
			if (global::Char.myCharz().taskMaint.index == 0)
			{
				if (TileMap.mapID == 46)
				{
					Npc npc = GameScr.findNPCInMap(18);
					if (npc == null || npc.isHide)
					{
						AutoTrainNewAccount.isPKKarinSama = true;
						return;
					}
					if (global::Char.myCharz().cx != 421 || global::Char.myCharz().cy != 408)
					{
						Utils.TeleportMyChar(421, 408);
						return;
					}
					if (!GameCanvas.menu.showMenu)
					{
						Service.gI().openMenu(18);
						return;
					}
					if (GameCanvas.menu.menuItems.size() == 4 && ((Command)GameCanvas.menu.menuItems.elementAt(3)).caption.Replace('\n', ' ').ToLower() == LocalizedString.challengeKarin)
					{
						Service.gI().confirmMenu(18, 3);
					}
					else if (GameCanvas.menu.menuItems.size() == 2 && ((Command)GameCanvas.menu.menuItems.elementAt(0)).caption.Replace('\n', ' ').ToLower() == LocalizedString.acceptChallenge)
					{
						Service.gI().confirmMenu(18, 0);
					}
					GameCanvas.menu.doCloseMenu();
					global::Char.chatPopup = null;
					return;
				}
				else if (TileMap.mapID == 47)
				{
					if (Math.Abs(global::Char.myCharz().cx - 600) >= 20)
					{
						Utils.TeleportMyChar(600, 336);
						return;
					}
					if (global::Char.myCharz().currentMovePoint == null || (global::Char.myCharz().currentMovePoint.xEnd != 600 && global::Char.myCharz().currentMovePoint.yEnd != 10))
					{
						global::Char.myCharz().currentMovePoint = new MovePoint(600, 10);
						return;
					}
				}
				else if (!ThreadAction<XmapController>.gI.IsActing)
				{
					XmapController.start(47);
					return;
				}
			}
			else if (global::Char.myCharz().taskMaint.index == 1)
			{
				if (TileMap.mapID == 46)
				{
					if (global::Char.myCharz().currentMovePoint == null || (global::Char.myCharz().currentMovePoint.xEnd != 576 && global::Char.myCharz().currentMovePoint.yEnd != 552))
					{
						global::Char.myCharz().currentMovePoint = new MovePoint(576, 552);
					}
					AutoTrainNewAccount.isTeleT77 = true;
					return;
				}
				if (TileMap.mapID == 47)
				{
					if (AutoTrainNewAccount.isTeleT77 && (global::Char.myCharz().cx != 371 || global::Char.myCharz().cy != 336))
					{
						AutoTrainNewAccount.isTeleT77 = false;
						Utils.TeleportMyChar(371, 336);
						return;
					}
					AutoTrainNewAccount.isPKT77 = true;
					return;
				}
				else
				{
					AutoTrainNewAccount.isTeleT77 = true;
					if (!ThreadAction<XmapController>.gI.IsActing)
					{
						XmapController.start(47);
						return;
					}
				}
			}
			else if (global::Char.myCharz().taskMaint.index == 2)
			{
				if (TileMap.mapID == 47)
				{
					Service.gI().openMenu(17);
					return;
				}
				if (!ThreadAction<XmapController>.gI.IsActing)
				{
					XmapController.start(47);
					return;
				}
			}
			else if (global::Char.myCharz().taskMaint.index == 3)
			{
				if (TileMap.mapID != global::Char.myCharz().cgender + 21)
				{
					if (!ThreadAction<XmapController>.gI.IsActing)
					{
						XmapController.start(global::Char.myCharz().cgender + 21);
						return;
					}
				}
				else
				{
					if (global::Char.myCharz().cgender == 0)
					{
						Service.gI().openMenu(0);
					}
					if (global::Char.myCharz().cgender == 1)
					{
						Service.gI().openMenu(2);
					}
					if (global::Char.myCharz().cgender == 2)
					{
						Service.gI().openMenu(1);
					}
				}
			}
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000BD1D8 File Offset: 0x000BB3D8
		private static void AutoNV11()
		{
			if (ThreadAction<XmapController>.gI.IsActing)
			{
				return;
			}
			if (global::Char.myCharz().cgender == 0 && TileMap.mapID != 5)
			{
				XmapController.start(5);
				return;
			}
			if (global::Char.myCharz().cgender == 1 && TileMap.mapID != 13)
			{
				XmapController.start(13);
				return;
			}
			if (global::Char.myCharz().cgender == 2 && TileMap.mapID != 20)
			{
				XmapController.start(20);
				return;
			}
			if (TileMap.mapID == 5 || TileMap.mapID == 13 || TileMap.mapID == 20)
			{
				if (!GameCanvas.menu.showMenu)
				{
					Service.gI().openMenu(13 + global::Char.myCharz().cgender);
					return;
				}
				if (GameCanvas.menu.menuItems.size() > 0)
				{
					Command command = (Command)GameCanvas.menu.menuItems.elementAt(0);
					if (command.caption.Replace('\n', ' ') == LocalizedString.talk || command.caption.Replace('\n', ' ') == LocalizedString.mission)
					{
						command.performAction();
					}
				}
				GameCanvas.menu.doCloseMenu();
				global::Char.chatPopup = null;
			}
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000BD300 File Offset: 0x000BB500
		private static void PKThanMeo()
		{
			if (AutoTrainNewAccount.myMinHP != 60)
			{
				AutoTrainNewAccount.myMinHP = 60;
			}
			if (AutoTrainNewAccount.myMinMP != 20)
			{
				AutoTrainNewAccount.myMinMP = 20;
			}
			Skill myskill = global::Char.myCharz().myskill;
			if (AutoTrainNewAccount.isPicking || mSystem.currentTimeMillis() - myskill.lastTimeUseThisSkill <= (long)myskill.coolDown + 100L)
			{
				return;
			}
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (!(@char.cName != "Karin") && @char.cTypePk == 3)
				{
					global::Char.myCharz().cx = @char.cx + Res.random(-5, 5);
					global::Char.myCharz().cy = @char.cy;
					Service.gI().charMove();
					if (Utils.Distance(global::Char.myCharz(), @char) <= 50)
					{
						myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						global::Char.myCharz().charFocus = @char;
						MyVector myVector = new MyVector();
						myVector.addElement(@char);
						Service.gI().sendPlayerAttack(new MyVector(), myVector, -1);
					}
				}
			}
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000BD418 File Offset: 0x000BB618
		private static void PKT77()
		{
			if (AutoTrainNewAccount.myMinHP != 100)
			{
				AutoTrainNewAccount.myMinHP = 100;
			}
			if (AutoTrainNewAccount.myMinMP != 20)
			{
				AutoTrainNewAccount.myMinMP = 20;
			}
			Skill myskill = global::Char.myCharz().myskill;
			if (AutoTrainNewAccount.isPicking || mSystem.currentTimeMillis() - myskill.lastTimeUseThisSkill <= (long)myskill.coolDown + 100L)
			{
				return;
			}
			int i = 0;
			while (i < GameScr.vCharInMap.size())
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (!(@char.cName != LocalizedString.mercenaryTao) && @char.cTypePk == 3)
				{
					global::Char.myCharz().cx = @char.cx + Res.random(-5, 5);
					global::Char.myCharz().cy = @char.cy;
					Service.gI().charMove();
					if (Utils.Distance(global::Char.myCharz(), @char) <= 50)
					{
						myskill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						global::Char.myCharz().charFocus = @char;
						MyVector myVector = new MyVector();
						myVector.addElement(@char);
						Service.gI().sendPlayerAttack(new MyVector(), myVector, -1);
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x000BD532 File Offset: 0x000BB732
		internal static void SetState(bool value)
		{
			AutoTrainNewAccount.isEnabled = value;
		}

		// Token: 0x040018B8 RID: 6328
		internal static bool isEnabled;

		// Token: 0x040018B9 RID: 6329
		internal static bool isPicking;

		// Token: 0x040018BA RID: 6330
		private static long lastTimePickedItem;

		// Token: 0x040018BB RID: 6331
		internal static bool isNhapCodeTanThu;

		// Token: 0x040018BC RID: 6332
		internal static bool isHarvestingPean;

		// Token: 0x040018BD RID: 6333
		private static int myMinMP = 15;

		// Token: 0x040018BE RID: 6334
		private static long lastTimeEatPean;

		// Token: 0x040018BF RID: 6335
		private static int myMinHP = 15;

		// Token: 0x040018C0 RID: 6336
		private static long lastTimeAutoPoint;

		// Token: 0x040018C1 RID: 6337
		private static int minHPMob;

		// Token: 0x040018C2 RID: 6338
		private static int maxHPMob = int.MaxValue;

		// Token: 0x040018C3 RID: 6339
		internal static bool isNhanBua;

		// Token: 0x040018C4 RID: 6340
		private static bool isTeleT77 = true;

		// Token: 0x040018C5 RID: 6341
		private static long lastTN;

		// Token: 0x040018C6 RID: 6342
		private static long lastTimeCheckTN;

		// Token: 0x040018C7 RID: 6343
		private static bool _isTanSatInternal;

		// Token: 0x040018C8 RID: 6344
		private static bool isPKKarinSama;

		// Token: 0x040018C9 RID: 6345
		private static bool isPKT77;

		// Token: 0x040018CA RID: 6346
		private static int minPeans;
	}
}
