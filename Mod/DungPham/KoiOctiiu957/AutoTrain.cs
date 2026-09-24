using System;
using System.Collections.Generic;

namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x020000FE RID: 254
	public class AutoTrain : IActionListener, IChatable
	{
		// Token: 0x06000B30 RID: 2864 RVA: 0x000092A6 File Offset: 0x000074A6
		public static AutoTrain getInstance()
		{
			if (AutoTrain._Instance == null)
			{
				AutoTrain._Instance = new AutoTrain();
			}
			return AutoTrain._Instance;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x000A4C2C File Offset: 0x000A2E2C
		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().tfChat.getText() != null && !ChatTextField.gI().tfChat.getText().Equals(string.Empty) && !text.Equals(string.Empty) && text != null)
			{
				if (ChatTextField.gI().strChat.Equals(AutoTrain.inputMPPercentGoHome[0]))
				{
					try
					{
						int num = AutoTrain.minimumMPGoHome = int.Parse(ChatTextField.gI().tfChat.getText());
						GameScr.info1.addInfo("Về Nhà Khi MP Dưới\n[" + num.ToString() + "%]", 0);
					}
					catch
					{
						GameScr.info1.addInfo("%MP Không Hợp Lệ, Vui Lòng Nhập Lại", 0);
					}
					AutoTrain.ResetChatTextField();
					return;
				}
			}
			else
			{
				ChatTextField.gI().isShow = false;
			}
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000045ED File Offset: 0x000027ED
		public void onCancelChat()
		{
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000A4D04 File Offset: 0x000A2F04
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 1:
			{
				int num = (int)p;
				AutoTrain.listMobIds.Clear();
				for (int i = 0; i < GameScr.vMob.size(); i++)
				{
					Mob mob = (Mob)GameScr.vMob.elementAt(i);
					if (!mob.isMobMe && mob.templateId == num)
					{
						AutoTrain.listMobIds.Add(mob.mobId);
					}
				}
				AutoTrain.TurnOnAutoTrain();
				return;
			}
			case 2:
				AutoTrain.listMobIds.Clear();
				for (int j = 0; j < GameScr.vMob.size(); j++)
				{
					Mob mob2 = (Mob)GameScr.vMob.elementAt(j);
					if (!mob2.isMobMe)
					{
						AutoTrain.listMobIds.Add(mob2.mobId);
					}
				}
				AutoTrain.TurnOnAutoTrain();
				return;
			case 3:
				AutoTrain.TurnOnAutoTrain();
				return;
			case 4:
				AutoTrain.isAvoidSuperMob = !AutoTrain.isAvoidSuperMob;
				GameScr.info1.addInfo("Né Siêu Quái\n" + (AutoTrain.isAvoidSuperMob ? "[STATUS: OFF]" : "[STATUS: ON]"), 0);
				return;
			case 13:
				AutoTrain.isAttackBySendCommand = !AutoTrain.isAttackBySendCommand;
				GameScr.info1.addInfo("Đánh Ảo\n" + (AutoTrain.isAttackBySendCommand ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				return;
			case 5:
				AutoTrain.ShowMenuGoback();
				return;
			case 6:
				AutoTrain.listMobIds.Clear();
				AutoTrain.isAutoTrain = false;
				GameScr.info1.addInfo("Đã Clear Danh Sách Train!", 0);
				return;
			case 7:
				if (global::Char.myCharz().mobFocus == null)
				{
					GameScr.info1.addInfo("Vui Lòng Chọn Quái!", 0);
				}
				if (global::Char.myCharz().mobFocus != null)
				{
					AutoTrain.listMobIds.Add(global::Char.myCharz().mobFocus.mobId);
					GameScr.info1.addInfo("Đã Thêm Quái: " + global::Char.myCharz().mobFocus.mobId.ToString(), 0);
					return;
				}
				break;
			case 8:
				AutoTrain.isAutoTrain = false;
				GameScr.isAutoPlay = false;
				GameScr.canAutoPlay = false;
				global::Char.myCharz().mobFocus = null;
				GameScr.info1.addInfo("Đã Tắt Auto Train!", 0);
				return;
			case 9:
				if (AutoTrain.isGoBack)
				{
					AutoTrain.isGoBack = false;
					GameScr.info1.addInfo("Goback\n[STATUS: OFF]", 0);
					return;
				}
				if (!AutoTrain.isGoBack)
				{
					AutoTrain.isGobackCoordinate = false;
					AutoTrain.isGoBack = true;
					AutoTrain.gobackMapID = TileMap.mapID;
					AutoTrain.gobackZoneID = TileMap.zoneID;
					GameScr.info1.addInfo(string.Concat(new string[]
					{
						"Goback\n[",
						TileMap.mapNames[AutoTrain.gobackMapID],
						"]\n[",
						AutoTrain.gobackZoneID.ToString(),
						"]"
					}), 0);
					return;
				}
				break;
			case 10:
				if (AutoTrain.isGoBack)
				{
					AutoTrain.isGoBack = false;
					GameScr.info1.addInfo("Goback\n[STATUS: OFF]", 0);
					return;
				}
				if (!AutoTrain.isGoBack)
				{
					AutoTrain.isGobackCoordinate = true;
					AutoTrain.isGoBack = true;
					AutoTrain.gobackMapID = TileMap.mapID;
					AutoTrain.gobackZoneID = TileMap.zoneID;
					AutoTrain.gobackX = global::Char.myCharz().cx;
					AutoTrain.gobackY = global::Char.myCharz().cy;
					GameScr.info1.addInfo(string.Concat(new string[]
					{
						"Goback Tọa Độ\n[",
						AutoTrain.gobackX.ToString(),
						"-",
						AutoTrain.gobackY.ToString(),
						"]"
					}), 0);
					return;
				}
				break;
			case 11:
				ChatTextField.gI().strChat = AutoTrain.inputMPPercentGoHome[0];
				ChatTextField.gI().tfChat.name = AutoTrain.inputMPPercentGoHome[1];
				ChatTextField.gI().startChat2(AutoTrain.getInstance(), string.Empty);
				return;
			case 12:
				AutoTrain.ShowMenuSkill();
				return;
			default:
				return;
			}
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000A5068 File Offset: 0x000A3268
		public static void ShowMenu()
		{
			MyVector myVector = new MyVector();
			List<Mob> list = new List<Mob>();
			if (AutoTrain.isAutoTrain || GameScr.canAutoPlay)
			{
				myVector.addElement(new Command("Tắt Auto Train", AutoTrain.getInstance(), 8, null));
			}
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(i);
				if (!mob.isMobMe)
				{
					bool flag = false;
					for (int j = 0; j < list.Count; j++)
					{
						if (mob.templateId == list[j].templateId)
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						list.Add(mob);
						myVector.addElement(new Command(string.Concat(new string[]
						{
							"Tàn Sát\n",
							mob.getTemplate().name,
							"\n[",
							NinjaUtil.getMoneys(mob.maxHp),
							"HP]"
						}), AutoTrain.getInstance(), 1, mob.templateId));
					}
				}
			}
			myVector.addElement(new Command("Tàn Sát Tất Cả", AutoTrain.getInstance(), 2, null));
			myVector.addElement(new Command("Tàn Sát Theo Vị Trí", AutoTrain.getInstance(), 3, null));
						myVector.addElement(new Command("Cài Đặt Kỹ Năng", AutoTrain.getInstance(), 12, null));
			myVector.addElement(new Command("Né Siêu Quái\n" + (AutoTrain.isAvoidSuperMob ? "[STATUS: OFF]" : "[STATUS: ON]"), AutoTrain.getInstance(), 4, null));
			myVector.addElement(new Command("Đánh Ảo\n" + (AutoTrain.isAttackBySendCommand ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoTrain.getInstance(), 13, null));
			myVector.addElement(new Command("Goback", AutoTrain.getInstance(), 5, null));
			myVector.addElement(new Command("Clear Danh Sách Train", AutoTrain.getInstance(), 6, null));
			if (global::Char.myCharz().mobFocus != null)
			{
				myVector.addElement(new Command(string.Concat(new string[]
				{
					"Thêm\n[",
					global::Char.myCharz().mobFocus.getTemplate().name,
					"]\n[",
					global::Char.myCharz().mobFocus.mobId.ToString(),
					"]"
				}), AutoTrain.getInstance(), 7, null));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		public static void ShowMenuSkill()
		{
			Mod.Menu.SkillSelectionPanel.gI().Show();
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000A527C File Offset: 0x000A347C
		private static void ShowMenuGoback()
		{
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Goback\n" + (AutoTrain.isGoBack ? string.Concat(new string[]
			{
				"[",
				TileMap.mapNames[AutoTrain.gobackMapID],
				"]\n[",
				AutoTrain.gobackZoneID.ToString(),
				"]"
			}) : "[STATUS: OFF]"), AutoTrain.getInstance(), 9, null));
			myVector.addElement(new Command("Goback Tọa Độ\n" + ((!AutoTrain.isGoBack || !AutoTrain.isGobackCoordinate) ? "[STATUS: OFF]" : string.Concat(new string[]
			{
				"[",
				AutoTrain.gobackX.ToString(),
				"-",
				AutoTrain.gobackY.ToString(),
				"]"
			})), AutoTrain.getInstance(), 10, null));
			myVector.addElement(new Command("Về Nhà Khi MP Dưới\n[" + AutoTrain.minimumMPGoHome.ToString() + "%]", AutoTrain.getInstance(), 11, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x00009276 File Offset: 0x00007476
		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000A53A0 File Offset: 0x000A35A0
		private static void TeleportTo(int x, int y)
		{
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y + 1;
			Service.gI().charMove();
			global::Char.myCharz().cx = x;
			global::Char.myCharz().cy = y;
			Service.gI().charMove();
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000092BE File Offset: 0x000074BE
		private static bool isMeCanAttack(Mob mob)
		{
			return GameScr.canAutoPlay || !mob.checkIsBoss() || (mob.checkIsBoss() && AutoTrain.isAvoidSuperMob);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000092E0 File Offset: 0x000074E0
		private static bool isMeOutOfMP()
		{
			return global::Char.myCharz().cMP < global::Char.myCharz().cMPFull * (long)AutoTrain.minimumMPGoHome / 100L;
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x00009303 File Offset: 0x00007503
		private static void TurnOnAutoTrain()
		{
			if (AutoTrain.listMobIds.Count == 0)
			{
				GameScr.info1.addInfo("Danh Sách Tàn Sát Trống!", 0);
				AutoTrain.isAutoTrain = false;
				return;
			}
			if (!GameScr.canAutoPlay)
			{
				AutoTrain.isAutoTrain = true;
			}
			else
			{
				AutoTrain.isAutoTrain = false;
			}
			GameScr.isAutoPlay = true;
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x00009343 File Offset: 0x00007543
		public static void LoadAutoTrainSkills()
		{
			string text = Rms.loadRMSString("AutoTrainSkills");
			if (text != null)
			{
				try
				{
					AutoTrain.selectedAutoTrainSkills.Clear();
					string[] array = text.Split(new char[] { ',' });
					for (int i = 0; i < array.Length; i++)
					{
						if (array[i] != "")
						{
							AutoTrain.selectedAutoTrainSkills.Add(int.Parse(array[i]));
						}
					}
				}
				catch (Exception ex)
				{
				}
			}
		}

		public static void SaveAutoTrainSkills()
		{
			string text = "";
			for (int i = 0; i < AutoTrain.selectedAutoTrainSkills.Count; i++)
			{
				text = text + AutoTrain.selectedAutoTrainSkills[i].ToString() + ",";
			}
			Rms.saveRMSString("AutoTrainSkills", text);
		}

		static AutoTrain()
		{
			AutoTrain.minimumMPGoHome = 5;
			AutoTrain.inputMPPercentGoHome = new string[]
			{
				"Nhập %MP",
				"%MP"
			};
			AutoTrain.LoadAutoTrainSkills();
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000A5410 File Offset: 0x000A3610
		public static void UseGrape()
		{
			for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
			{
				Item item = global::Char.myCharz().arrItemBag[i];
				if (item != null && item.template.id == 212)
				{
					Service.gI().useItem(0, 1, (sbyte)item.indexUI, -1);
					return;
				}
			}
			for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
			{
				Item item2 = global::Char.myCharz().arrItemBag[j];
				if (item2 != null && item2.template.id == 211)
				{
					Service.gI().useItem(0, 1, (sbyte)item2.indexUI, -1);
					return;
				}
			}
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000A54BC File Offset: 0x000A36BC
		public static void Update()
		{
			if (GameScr.isAutoPlay && (GameScr.canAutoPlay || AutoTrain.isAutoTrain) && GameCanvas.gameTick % 20 == 0)
			{
				AutoTrain.DoIt();
			}
			if (global::Char.myCharz().cStamina <= 5 && GameCanvas.gameTick % 100 == 0)
			{
				AutoTrain.UseGrape();
			}
			if (!AutoTrain.isGoBack)
			{
				return;
			}
			if (global::Char.myCharz().meDead && GameCanvas.gameTick % 100 == 0)
			{
				Service.gI().returnTownFromDead();
			}
			if (AutoTrain.isMeOutOfMP())
			{
				int num = 21 + global::Char.myCharz().cgender;
				if (TileMap.mapID != num)
				{
					GameScr.isAutoPlay = false;
					global::Char.myCharz().mobFocus = null;
					if (GameCanvas.gameTick % 50 == 0)
					{
						AutoMap.StartRunToMapId(num);
						return;
					}
				}
			}
			else
			{
				if (AutoTrain.isMeOutOfMP())
				{
					return;
				}
				if (TileMap.mapID != AutoTrain.gobackMapID)
				{
					GameScr.isAutoPlay = false;
					AutoMap.StartRunToMapId(AutoTrain.gobackMapID);
				}
				if (TileMap.mapID == AutoTrain.gobackMapID)
				{
					if (!AutoTrain.isGobackCoordinate && GameCanvas.gameTick % 100 == 0)
					{
						GameScr.isAutoPlay = true;
					}
					if (TileMap.zoneID != AutoTrain.gobackZoneID && !global::Char.ischangingMap && !Controller.isStopReadMessage && GameCanvas.gameTick % 100 == 0)
					{
						Service.gI().requestChangeZone(AutoTrain.gobackZoneID, -1);
					}
					if (AutoTrain.isGobackCoordinate && (global::Char.myCharz().cx != AutoTrain.gobackX || global::Char.myCharz().cy != AutoTrain.gobackY) && GameCanvas.gameTick % 100 == 0)
					{
						AutoTrain.TeleportTo(AutoTrain.gobackX, AutoTrain.gobackY);
					}
				}
			}
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000A5638 File Offset: 0x000A3838
		private static Mob GetNextMob(int type)
		{
			if (type == 1)
			{
				long num = mSystem.currentTimeMillis();
				Mob result = null;
				for (int i = 0; i < AutoTrain.listMobIds.Count; i++)
				{
					Mob mob = (Mob)GameScr.vMob.elementAt(AutoTrain.listMobIds[i]);
					long cTimeDie = mob.cTimeDie;
					if (!mob.isMobMe && cTimeDie < num)
					{
						result = mob;
						num = cTimeDie;
					}
				}
				return result;
			}
			Mob result2 = null;
			int num2 = 9999;
			for (int j = 0; j < AutoTrain.listMobIds.Count; j++)
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(AutoTrain.listMobIds[j]);
				if (mob2.status != 0 && mob2.status != 1 && mob2.hp > 0L && !mob2.isMobMe && AutoTrain.isMeCanAttack(mob2))
				{
					int num3 = global::Math.abs(global::Char.myCharz().cx - mob2.x);
					if (num2 > num3)
					{
						result2 = mob2;
						num2 = num3;
					}
				}
			}
			return result2;
		}

		private static long lastTimeWait;
		public static void Wait(int time)
		{
			AutoTrain.lastTimeWait = mSystem.currentTimeMillis() + (long)time;
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x000A573C File Offset: 0x000A393C
		private static void DoIt()
		{
			if (mSystem.currentTimeMillis() < AutoTrain.lastTimeWait) return;

			if ((!AutoTrain.isAutoTrain && !GameScr.canAutoPlay) || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5)
			{
				return;
			}
			if (AutoTrain.listMobIds.Count == 0)
			{
				if (mSystem.currentTimeMillis() - AutoTrain.lastTimeAddNewMob > 5000L)
				{
					AutoTrain.lastTimeAddNewMob = mSystem.currentTimeMillis();
					GameScr.info1.addInfo("Danh Sách Tàn Sát Trống!", 0);
				}
				AutoTrain.isAutoTrain = false;
				return;
			}
			if (global::Char.myCharz().mobFocus != null && (global::Char.myCharz().mobFocus == null || !global::Char.myCharz().mobFocus.isMobMe))
			{
				if (global::Char.myCharz().mobFocus.hp <= 0L || global::Char.myCharz().mobFocus.status == 1 || global::Char.myCharz().mobFocus.status == 0 || !AutoTrain.isMeCanAttack(global::Char.myCharz().mobFocus))
				{
					global::Char.myCharz().mobFocus = null;
				}
			}
			else
			{
				bool hasMayDo = ItemTime.isExistItem(4387);
				if (!GameScr.canAutoPlay && AutoPick.isAutoPick && !hasMayDo)
				{
					AutoPick.FocusToNearestItem();
					if (global::Char.myCharz().itemFocus != null)
					{
						AutoPick.PickIt();
						AutoPick.FocusToNearestItem();
						AutoTrain.Wait(500); // Wait after pick
						return;
					}
				}
				else
				{
					global::Char.myCharz().itemFocus = null;
				}
				if (global::Char.myCharz().itemFocus == null)
				{
					Mob nextMob = AutoTrain.GetNextMob(0);
					if (nextMob == null)
					{
						nextMob = AutoTrain.GetNextMob(1);
						if (!GameScr.canAutoPlay)
						{
							global::Char.myCharz().currentMovePoint = new MovePoint(nextMob.xFirst, nextMob.yFirst);
							global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
						}
					}
					else
					{
						global::Char.myCharz().mobFocus = nextMob;
						if (GameScr.canAutoPlay)
						{
							global::Char.myCharz().cx = nextMob.x;
							global::Char.myCharz().cy = nextMob.y;
							Service.gI().charMove();
						}
					}
				}
			}
			if (global::Char.myCharz().mobFocus == null || (global::Char.myCharz().skillInfoPaint() != null && global::Char.myCharz().indexSkill < global::Char.myCharz().skillInfoPaint().Length && global::Char.myCharz().dart != null && global::Char.myCharz().arr != null))
			{
				return;
			}
			if (global::Char.myCharz().mobFocus != null && GameScr.canAutoPlay && (global::Math.abs(global::Char.myCharz().mobFocus.x - global::Char.myCharz().cx) > 100 || global::Math.abs(global::Char.myCharz().mobFocus.y - global::Char.myCharz().cy) > 100) && mSystem.currentTimeMillis() - AutoTrain.lastTimeTeleportToMob > 100L)
			{
				AutoTrain.lastTimeTeleportToMob = mSystem.currentTimeMillis();
				global::Char.myCharz().cx = global::Char.myCharz().mobFocus.x;
				global::Char.myCharz().cy = global::Char.myCharz().mobFocus.y;
				Service.gI().charMove();
			}
			Skill skill = null;
			if (AutoTrain.selectedAutoTrainSkills.Count > 0)
			{
				for (int i = 0; i < global::Char.myCharz().vSkillFight.size(); i++)
				{
					Skill s = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
					if (s != null && s.template != null && AutoTrain.selectedAutoTrainSkills.Contains((int)s.template.id) && !s.paintCanNotUseSkill && global::Char.myCharz().skillInfoPaint() == null)
					{
						int num = (int)((s.template.manaUseType == 2) ? 1L : ((s.template.manaUseType == 1) ? ((long)s.manaUse * global::Char.myCharz().cMPFull / 100L) : ((long)s.manaUse)));
						if (global::Char.myCharz().cMP >= (long)num)
						{
							if (skill == null || skill.coolDown < s.coolDown)
							{
								skill = s;
							}
						}
					}
				}
			}
			else
			{
				for (int i = 0; i < GameScr.keySkill.Length; i++)
				{
					if (GameScr.keySkill[i] != null && !GameScr.keySkill[i].paintCanNotUseSkill && GameScr.keySkill[i].template.id != 10 && GameScr.keySkill[i].template.id != 11 && GameScr.keySkill[i].template.id != 14 && GameScr.keySkill[i].template.id != 23 && GameScr.keySkill[i].template.id != 7 && GameScr.keySkill[i].template.id != 3 && GameScr.keySkill[i].template.id != 1 && GameScr.keySkill[i].template.id != 5 && GameScr.keySkill[i].template.id != 20 && GameScr.keySkill[i].template.id != 22 && GameScr.keySkill[i].template.id != 18 && GameScr.keySkill[i].template.id != 24 && GameScr.keySkill[i].template.id != 25 && GameScr.keySkill[i].template.id != 26 && ((global::Char.myCharz().cgender == 1 && (global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[5]) == null || GameScr.keySkill[i].template.id != 2)) || (global::Char.myCharz().cgender == 0 && (global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[3]) == null || GameScr.keySkill[i].template.id != 0))) && global::Char.myCharz().skillInfoPaint() == null)
					{
						int num = (int)((GameScr.keySkill[i].template.manaUseType == 2) ? 1L : ((GameScr.keySkill[i].template.manaUseType == 1) ? ((long)GameScr.keySkill[i].manaUse * global::Char.myCharz().cMPFull / 100L) : ((long)GameScr.keySkill[i].manaUse)));
						if (global::Char.myCharz().cMP >= (long)num)
						{
							if (skill == null)
							{
								skill = GameScr.keySkill[i];
							}
							else if (skill.coolDown < GameScr.keySkill[i].coolDown)
							{
								skill = GameScr.keySkill[i];
							}
						}
					}
				}
			}
			if (skill != null)
			{
				bool isAttackSkill = (skill.template.id == 0 || skill.template.id == 1 || skill.template.id == 2 || skill.template.id == 3 || skill.template.id == 4 || skill.template.id == 5 || skill.template.id == 9 || skill.template.id == 15 || skill.template.id == 17 || skill.template.id == 18);
				if (AutoTrain.isAttackBySendCommand && isAttackSkill)
				{
					Mob mobFocus = global::Char.myCharz().mobFocus;
					if (global::Char.myCharz().myskill != skill)
					{
						Service.gI().selectSkill((int)skill.template.id);
						global::Char.myCharz().myskill = skill;
					}
					bool isFlyMob = (mobFocus.getTemplate().type == 4 || mobFocus.getTemplate().type == 5);
					if (isFlyMob)
					{
						if (global::Math.abs(global::Char.myCharz().cx - mobFocus.x) > 70)
						{
							if (ItemTime.isExistItem(4387)) {
								global::Char.myCharz().cx = mobFocus.x;
								global::Char.myCharz().cy = mobFocus.y;
								Service.gI().charMove();
							} else {
								global::Char.myCharz().currentMovePoint = new MovePoint(mobFocus.x, MainMod.GetYGround(mobFocus.x));
							}
						}
						else
						{
							global::Char.myCharz().currentMovePoint = null;
							global::Char.myCharz().cx = mobFocus.x + Res.random(-5, 5);
							global::Char.myCharz().cy = mobFocus.y + Res.random(-5, 5);
							Service.gI().charMove();
						}
					}
					else
					{
						if (global::Math.abs(global::Char.myCharz().cx - mobFocus.xFirst) > 50) {
							if (ItemTime.isExistItem(4387)) {
								global::Char.myCharz().cx = mobFocus.xFirst;
								global::Char.myCharz().cy = mobFocus.yFirst;
								Service.gI().charMove();
							} else {
								global::Char.myCharz().currentMovePoint = new MovePoint(mobFocus.xFirst, mobFocus.yFirst);
							}
						}
					}

					long timeDist = mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill;
					if ((global::Math.abs(global::Char.myCharz().cx - mobFocus.x) <= 50 || (isFlyMob && global::Math.abs(global::Char.myCharz().cx - mobFocus.x) <= 70)) && timeDist > (long)skill.coolDown + 10L)
					{
						global::Char.myCharz().mobFocus = mobFocus;
						skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
						MyVector vMob = new MyVector();
						vMob.addElement(mobFocus);
						Service.gI().sendPlayerAttack(vMob, new MyVector(), 1);
					}
				}
				else
				{
					GameScr.gI().doSelectSkill(skill, true);
					GameScr.gI().doDoubleClickToObj(global::Char.myCharz().mobFocus);
				}
			}
		}

		// Token: 0x040015D2 RID: 5586
		private static AutoTrain _Instance;

		// Token: 0x040015D3 RID: 5587
		public static bool isAvoidSuperMob;
		public static bool isAttackBySendCommand = true;
		public static System.Collections.Generic.List<int> selectedAutoTrainSkills = new System.Collections.Generic.List<int>(new int[] { 0, 2, 4 });

		// Token: 0x040015D4 RID: 5588
		public static bool isGoBack;

		// Token: 0x040015D5 RID: 5589
		public static bool isGobackCoordinate;

		// Token: 0x040015D6 RID: 5590
		private static int gobackX;

		// Token: 0x040015D7 RID: 5591
		private static int gobackY;

		// Token: 0x040015D8 RID: 5592
		private static int gobackMapID;

		// Token: 0x040015D9 RID: 5593
		private static int gobackZoneID;

		// Token: 0x040015DA RID: 5594
		public static bool isAutoTrain;

		// Token: 0x040015DB RID: 5595
		public static int minimumMPGoHome;

		// Token: 0x040015DC RID: 5596
		private static string[] inputMPPercentGoHome;

		// Token: 0x040015DD RID: 5597
		public static List<int> listMobIds = new List<int>();

		// Token: 0x040015DE RID: 5598
		public static long lastTimeAddNewMob;

		// Token: 0x040015DF RID: 5599
		private static long lastTimeTeleportToMob;
	}
}
