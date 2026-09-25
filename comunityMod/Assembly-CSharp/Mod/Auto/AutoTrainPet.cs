using System;
using System.Collections.Generic;
using Mod.Constants;
using Mod.ModHelper;
using Mod.Xmap;
using UnityEngine;

namespace Mod.Auto
{
	// Token: 0x02000186 RID: 390
	internal class AutoTrainPet
	{
		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x06001183 RID: 4483 RVA: 0x000BD55A File Offset: 0x000BB75A
		// (set) Token: 0x06001184 RID: 4484 RVA: 0x000BD561 File Offset: 0x000BB761
		internal static AutoTrainPetMode Mode { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06001185 RID: 4485 RVA: 0x000BD569 File Offset: 0x000BB769
		// (set) Token: 0x06001186 RID: 4486 RVA: 0x000BD570 File Offset: 0x000BB770
		internal static AutoTrainPetAttackMode ModeAttackWhenNeeded { get; set; }

		// Token: 0x06001187 RID: 4487 RVA: 0x000BD578 File Offset: 0x000BB778
		internal static void SetState(int value)
		{
			AutoTrainPet.Mode = (AutoTrainPetMode)value;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000BD580 File Offset: 0x000BB780
		internal static void SetAttackState(int value)
		{
			AutoTrainPet.ModeAttackWhenNeeded = (AutoTrainPetAttackMode)value;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000BD588 File Offset: 0x000BB788
		internal static void Update()
		{
			if (AutoTrainPet.Mode == AutoTrainPetMode.Disabled)
			{
				return;
			}
			if (AutoTrainPet.isFirstTimeCheckPet)
			{
				AutoTrainPet.delayCheckPet = mSystem.currentTimeMillis();
				AutoTrainPet.isFirstTimeCheckPet = false;
			}
			if (mSystem.currentTimeMillis() - AutoTrainPet.delayCheckPet < 3000L)
			{
				return;
			}
			if ((float)GameCanvas.gameTick % (30f * Time.timeScale) != 0f || ThreadAction<XmapController>.gI.IsActing)
			{
				return;
			}
			AutoTrainPet.AutoPick();
			if (global::Char.myPetz().cStamina < 5 && GameScr.hpPotion > 0 && global::Char.myPetz().cHP > 0 && !global::Char.myPetz().isDie)
			{
				GameScr.gI().doUseHP();
			}
			if (AutoTrainPet.isMagicTreeOutOfPean && mSystem.currentTimeMillis() - AutoTrainPet.lastTimeCheckMagicTree >= 600000L)
			{
				AutoTrainPet.isMagicTreeOutOfPean = false;
				AutoTrainPet.lastTimeCheckMagicTree = mSystem.currentTimeMillis();
			}
			if (!AutoTrainPet.isMagicTreeUpgrading && GameScr.hpPotion == 0 && !AutoTrainPet.isGoHomeGetMorePean && !AutoTrainPet.isMagicTreeOutOfPean)
			{
				AutoTrainPet.isGoHomeGetMorePean = true;
				AutoTrainPet.mapZoneGoBack = new KeyValuePair<int, int>(TileMap.mapID, TileMap.zoneID);
			}
			if (AutoTrainPet.isGoHomeGetMorePean && !AutoTrainPet.isMagicTreeOutOfPean && (float)GameCanvas.gameTick % (60f * Time.timeScale) == 0f)
			{
				if (TileMap.mapID != global::Char.myCharz().cgender + 21)
				{
					if (!ThreadAction<XmapController>.gI.IsActing && GameScr.hpPotion == 0)
					{
						XmapController.start(global::Char.myCharz().cgender + 21);
					}
				}
				else
				{
					if (GameCanvas.menu.showMenu)
					{
						GameCanvas.menu.doCloseMenu();
					}
					if (GameScr.gI().magicTree.currPeas > 0)
					{
						Service.gI().openMenu(4);
						Service.gI().confirmMenu(4, 0);
						return;
					}
					AutoTrainPet.isMagicTreeOutOfPean = true;
					if (GameScr.gI().magicTree.isUpdateTree)
					{
						AutoTrainPet.lastTimeCheckMagicTree = mSystem.currentTimeMillis();
						AutoTrainPet.isMagicTreeUpgrading = true;
					}
					if ((GameScr.gI().magicTree.isPeasEffect || GameScr.gI().magicTree.isUpdateTree || GameScr.gI().magicTree.currPeas == 0) && !ThreadAction<XmapController>.gI.IsActing)
					{
						XmapController.start(AutoTrainPet.mapZoneGoBack.Key);
					}
				}
				if (TileMap.mapID == AutoTrainPet.mapZoneGoBack.Key && !ThreadAction<XmapController>.gI.IsActing)
				{
					if (TileMap.zoneID == AutoTrainPet.mapZoneGoBack.Value)
					{
						if (global::Char.myCharz().cx == AutoTrainPet.lastX)
						{
							AutoTrainPet.isGoHomeGetMorePean = false;
						}
					}
					else if ((float)GameCanvas.gameTick % (120f * Time.timeScale) == 0f)
					{
						Service.gI().requestChangeZone(AutoTrainPet.mapZoneGoBack.Value, 0);
					}
				}
			}
			AutoTrainPet.TeleToSafePos();
			AutoTrainPet.AutoSkill();
			if (AutoTrainPet.isMyPetDied)
			{
				return;
			}
			if (AutoTrainPet.isAssignedLastXPet)
			{
				AutoTrainPet.isAssignedLastXPet = false;
				Utils.TeleportMyChar(AutoTrainPet.lastXPet);
			}
			if (mSystem.currentTimeMillis() - AutoTrainPet.lastTimePetFollow > 600000L || (GameScr.findCharInMap(-global::Char.myCharz().charID) != null && Utils.Distance(global::Char.myCharz(), GameScr.findCharInMap(-global::Char.myCharz().charID)) > 400))
			{
				AutoTrainPet.lastTimePetFollow = mSystem.currentTimeMillis();
				Service.gI().petStatus(0);
				Utils.TeleportMyChar(global::Char.myCharz().cx);
				return;
			}
			if (AutoTrainPet.Mode == AutoTrainPetMode.Normal)
			{
				if (global::Char.myPetz().petStatus != 2)
				{
					Service.gI().petStatus(2);
					return;
				}
			}
			else
			{
				if (AutoTrainPet.isTTNL)
				{
					return;
				}
				if (AutoTrainPet.Mode == AutoTrainPetMode.AvoidSuperMob)
				{
					if (global::Char.myPetz().petStatus != 1)
					{
						Service.gI().petStatus(1);
					}
					Mob mob = AutoTrainPet.ClosestMob();
					if (mob != null && mob.x > 50 && mob.y > 50)
					{
						global::Char.myCharz().currentMovePoint = new MovePoint(mob.x + Res.random(-5, 5), mob.y);
						return;
					}
				}
				else if (AutoTrainPet.Mode == AutoTrainPetMode.Kaioken)
				{
					if (global::Char.myPetz().petStatus != 2)
					{
						Service.gI().petStatus(2);
					}
					if ((float)GameCanvas.gameTick % (60f * Time.timeScale) == 0f)
					{
						global::Char.myCharz().cy--;
						Service.gI().charMove();
					}
					if ((float)GameCanvas.gameTick % (60f * Time.timeScale) == 30f * Time.timeScale)
					{
						global::Char.myCharz().cy++;
						Service.gI().charMove();
					}
				}
			}
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000BD9E8 File Offset: 0x000BBBE8
		private static void TeleToSafePos()
		{
			if (!AutoTrainPet.isPicking && (global::Char.myCharz().arrItemBody[5] == null || global::Char.myCharz().arrItemBody[5].template.id != 449))
			{
				global::Char @char = GameScr.findCharInMap(-global::Char.myCharz().charID);
				if (@char == null)
				{
					@char = global::Char.myPetz();
				}
				if (@char != null)
				{
					if (@char.cHP <= 0 || @char.isDie)
					{
						if (!AutoTrainPet.isMyPetDied)
						{
							AutoTrainPet.isMyPetDied = true;
							if (!AutoTrainPet.isAssignedLastXPet)
							{
								AutoTrainPet.isAssignedLastXPet = true;
								AutoTrainPet.lastXPet = global::Char.myCharz().cx;
							}
							Utils.TeleportMyChar(50);
							return;
						}
					}
					else if (AutoTrainPet.isMyPetDied)
					{
						AutoTrainPet.isMyPetDied = false;
						return;
					}
				}
				else if (AutoTrainPet.isMyPetDied)
				{
					AutoTrainPet.isMyPetDied = false;
				}
			}
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x000BDAA4 File Offset: 0x000BBCA4
		private static void AutoSkill()
		{
			Skill skill = global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[2]);
			Skill skill2 = global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[0]);
			if (skill != null)
			{
				if (skill.point > 0 && mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill > (long)skill.coolDown)
				{
					if ((global::Char.myPetz().cHP * 100 / global::Char.myPetz().cHPFull < 10 || global::Char.myPetz().cMP * 100 / global::Char.myPetz().cMPFull < 10 || global::Char.myCharz().cHP * 100 / global::Char.myCharz().cHPFull < 10 || global::Char.myCharz().cMP * 100 / global::Char.myCharz().cMPFull < 10) && global::Char.myCharz().cgender == 1 && skill.manaUse < global::Char.myCharz().cMP)
					{
						if (skill.point > 1)
						{
							Utils.buffMe();
						}
						else
						{
							MyVector myVector = new MyVector();
							myVector.addElement(GameScr.findCharInMap(-global::Char.myCharz().charID));
							Service.gI().selectSkill((int)skill.template.id);
							Service.gI().sendPlayerAttack(new MyVector(), myVector, 2);
							Service.gI().selectSkill((int)skill2.template.id);
						}
						skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						return;
					}
					if ((global::Char.myCharz().cHP * 100 / global::Char.myCharz().cHPFull < 10 || global::Char.myCharz().cMP * 100 / global::Char.myCharz().cMPFull < 10) && global::Char.myCharz().cgender == 2)
					{
						GameScr.gI().doUseSkillNotFocus(skill);
						AutoTrainPet.isTTNL = true;
						skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						return;
					}
				}
				if (global::Char.myCharz().cgender == 2 && AutoTrainPet.isTTNL && mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill > 10000L)
				{
					global::Char.myCharz().myskill = skill2;
					AutoTrainPet.isTTNL = false;
				}
			}
			Skill skill3 = null;
			if (global::Char.myCharz().cgender == 0)
			{
				skill3 = global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[2]);
			}
			else if (global::Char.myCharz().cgender == 1)
			{
				skill3 = global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[6]);
			}
			else if (global::Char.myCharz().cgender == 2)
			{
				skill3 = global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[6]);
			}
			if (skill3 != null)
			{
				for (int i = 0; i < GameScr.vMob.size(); i++)
				{
					Mob mob = (Mob)GameScr.vMob.elementAt(i);
					if (mob.levelBoss != 0 && !mob.isMobMe && mob.hp > 0)
					{
						global::Char.myCharz().mobFocus = mob;
						if (skill3.point > 0 && skill3.manaUse < global::Char.myCharz().cMP && mSystem.currentTimeMillis() - skill3.lastTimeUseThisSkill > (long)skill3.coolDown)
						{
							MyVector myVector2 = new MyVector();
							myVector2.addElement(mob);
							Service.gI().selectSkill((int)skill3.template.id);
							Service.gI().sendPlayerAttack(myVector2, new MyVector(), 1);
							Service.gI().selectSkill((int)skill2.template.id);
							skill3.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						}
						return;
					}
				}
			}
			if (AutoTrainPet.saoMayLuoiThe)
			{
				AutoTrainPet.saoMayLuoiThe = false;
				Service.gI().selectSkill((int)skill2.template.id);
				switch (AutoTrainPet.ModeAttackWhenNeeded)
				{
				case AutoTrainPetAttackMode.AttackClosestMob:
				{
					MyVector myVector3 = new MyVector();
					myVector3.addElement(AutoTrainPet.ClosestMob());
					Service.gI().sendPlayerAttack(myVector3, new MyVector(), 1);
					return;
				}
				case AutoTrainPetAttackMode.AttackMyPet:
				{
					if (global::Char.myCharz().cFlag != 8)
					{
						Service.gI().getFlag(1, 8);
					}
					MyVector myVector4 = new MyVector();
					myVector4.addElement(GameScr.findCharInMap(-global::Char.myCharz().charID));
					Service.gI().sendPlayerAttack(new MyVector(), myVector4, 2);
					return;
				}
				case AutoTrainPetAttackMode.AttackMyself:
					if (global::Char.myCharz().cFlag != 8)
					{
						Service.gI().getFlag(1, 8);
					}
					Service.gI().sendPlayerAttack(new MyVector(), Utils.getMyVectorMe(), 2);
					break;
				default:
					return;
				}
			}
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x000BDF08 File Offset: 0x000BC108
		private static Mob ClosestMob()
		{
			Mob mob = null;
			int num = int.MaxValue;
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(i);
				if (mob2.status != 0 && mob2.status != 1 && mob2.hp > 0 && !mob2.isMobMe && mob2.levelBoss == 0 && mob2.getTemplate().type != MonsterType.Fly && ((AutoTrainPet.mobTemplateIdList.Count > 0 && AutoTrainPet.mobTemplateIdList.Contains(mob2.templateId)) || (AutoTrainPet.mobIdList.Count > 0 && AutoTrainPet.mobIdList.Contains(mob2.mobId)) || AutoTrainPet.mobTemplateIdList.Count == 0 || AutoTrainPet.mobIdList.Count == 0))
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

		// Token: 0x0600118D RID: 4493 RVA: 0x000BE020 File Offset: 0x000BC220
		private static void AutoPick()
		{
			if (mSystem.currentTimeMillis() - AutoTrainPet.lastTimePick > 550L)
			{
				bool flag = false;
				if (GameScr.vItemMap.size() == 0)
				{
					AutoTrainPet.isPicking = false;
				}
				for (int i = GameScr.vItemMap.size() - 1; i >= 0; i--)
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
					if ((itemMap.template.id >= 828 && itemMap.template.id <= 842) || itemMap.template.id == 859 || itemMap.template.id == 362 || (itemMap.template.id >= 353 && itemMap.template.id <= 360))
					{
						GameScr.vItemMap.removeElementAt(i);
					}
					else
					{
						if (itemMap.template.id == 74)
						{
							Service.gI().pickItem(itemMap.itemMapID);
							AutoTrainPet.lastTimePick = mSystem.currentTimeMillis();
							return;
						}
						int num = Utils.Distance(itemMap, global::Char.myCharz());
						if (itemMap.playerId == global::Char.myCharz().charID || itemMap.playerId == -1)
						{
							flag = true;
							if (!AutoTrainPet.isAssignedLastX)
							{
								AutoTrainPet.isAssignedLastX = true;
								AutoTrainPet.lastX = global::Char.myCharz().cx;
							}
							if (num > 60)
							{
								Utils.TeleportMyChar(itemMap.x);
							}
							else
							{
								global::Char.myCharz().currentMovePoint = new MovePoint(itemMap.x, itemMap.y);
							}
							Service.gI().pickItem(itemMap.itemMapID);
							AutoTrainPet.isPicking = true;
							AutoTrainPet.lastTimePick = mSystem.currentTimeMillis();
							break;
						}
					}
				}
				if (AutoTrainPet.isAssignedLastX && !flag)
				{
					if (AutoTrainPet.lastX <= 50 && AutoTrainPet.lastXPet > 50)
					{
						AutoTrainPet.lastX = AutoTrainPet.lastXPet;
					}
					AutoTrainPet.isPicking = false;
					if (Res.distance(global::Char.myCharz().cx, global::Char.myCharz().cy, AutoTrainPet.lastX, Utils.GetYGround(AutoTrainPet.lastX)) > 60)
					{
						Utils.TeleportMyChar(AutoTrainPet.lastX);
					}
					else
					{
						global::Char.myCharz().currentMovePoint = new MovePoint(AutoTrainPet.lastX, Utils.GetYGround(AutoTrainPet.lastX));
					}
					AutoTrainPet.isAssignedLastX = false;
				}
			}
		}

		// Token: 0x040018D4 RID: 6356
		internal static List<int> mobTemplateIdList = new List<int>();

		// Token: 0x040018D5 RID: 6357
		internal static List<int> mobIdList = new List<int>();

		// Token: 0x040018D6 RID: 6358
		private static long lastTimePick;

		// Token: 0x040018D7 RID: 6359
		private static int lastX;

		// Token: 0x040018D8 RID: 6360
		private static bool isAssignedLastX;

		// Token: 0x040018D9 RID: 6361
		private static int lastXPet;

		// Token: 0x040018DA RID: 6362
		private static bool isAssignedLastXPet;

		// Token: 0x040018DB RID: 6363
		private static bool isMyPetDied;

		// Token: 0x040018DC RID: 6364
		internal static bool isPicking;

		// Token: 0x040018DD RID: 6365
		private static long lastTimePetFollow = mSystem.currentTimeMillis();

		// Token: 0x040018DE RID: 6366
		private static bool isGoHomeGetMorePean;

		// Token: 0x040018DF RID: 6367
		private static bool isMagicTreeUpgrading;

		// Token: 0x040018E0 RID: 6368
		private static bool isMagicTreeOutOfPean;

		// Token: 0x040018E1 RID: 6369
		private static long lastTimeCheckMagicTree;

		// Token: 0x040018E2 RID: 6370
		private static KeyValuePair<int, int> mapZoneGoBack = default(KeyValuePair<int, int>);

		// Token: 0x040018E3 RID: 6371
		private static long delayCheckPet;

		// Token: 0x040018E4 RID: 6372
		internal static bool isFirstTimeCheckPet = true;

		// Token: 0x040018E5 RID: 6373
		private static bool isTTNL;

		// Token: 0x040018E6 RID: 6374
		internal static bool saoMayLuoiThe;
	}
}
