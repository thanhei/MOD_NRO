using System;
using System.Collections.Generic;
using System.Linq;
using Mod.Constants;

namespace Mod.PickMob
{
	// Token: 0x02000126 RID: 294
	public class PickMobController
	{
		// Token: 0x06000E68 RID: 3688 RVA: 0x000AAA34 File Offset: 0x000A8C34
		public static void Update()
		{
			if (PickMobController.IsWaiting())
			{
				return;
			}
			global::Char @char = global::Char.myCharz();
			if (@char.statusMe == 14 || @char.cHP <= 0)
			{
				return;
			}
			bool flag = ItemTime.isExistItem(4387);
			bool flag2 = Pk9rPickMob.IsTanSat && flag;
			if (Pk9rPickMob.IsAutoPickItems && !flag2)
			{
				if (TileMap.mapID == global::Char.myCharz().cgender + 21 && GameScr.vItemMap.size() > 0)
				{
					Service.gI().pickItem(((ItemMap)GameScr.vItemMap.elementAt(0)).itemMapID);
					return;
				}
				if (PickMobController.IsPickingItems)
				{
					if (PickMobController.IndexItemPick >= PickMobController.ItemPicks.Count)
					{
						PickMobController.IsPickingItems = false;
						return;
					}
					ItemMap itemMap = PickMobController.ItemPicks[PickMobController.IndexItemPick];
					switch (PickMobController.GetTpyePickItem(itemMap))
					{
					case PickMobController.TypePickItem.CanNotPickItem:
						PickMobController.IndexItemPick++;
						return;
					case PickMobController.TypePickItem.PickItemNormal:
						Service.gI().charMove();
						Service.gI().pickItem(itemMap.itemMapID);
						itemMap.countAutoPick++;
						PickMobController.IndexItemPick++;
						PickMobController.Wait(500);
						return;
					case PickMobController.TypePickItem.PickItemTDLT:
						@char.cx = itemMap.xEnd;
						@char.cy = itemMap.yEnd;
						Service.gI().charMove();
						Service.gI().pickItem(itemMap.itemMapID);
						itemMap.countAutoPick++;
						PickMobController.IndexItemPick++;
						PickMobController.Wait(500);
						return;
					case PickMobController.TypePickItem.PickItemTanSat:
						PickMobController.Move(itemMap.xEnd, itemMap.yEnd);
						@char.mobFocus = null;
						PickMobController.Wait(500);
						return;
					}
				}
				PickMobController.ItemPicks.Clear();
				PickMobController.IndexItemPick = 0;
				for (int i = 0; i < GameScr.vItemMap.size(); i++)
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(i);
					if (PickMobController.GetTpyePickItem(itemMap2) != PickMobController.TypePickItem.CanNotPickItem)
					{
						PickMobController.ItemPicks.Add(itemMap2);
					}
				}
				if (PickMobController.ItemPicks.Count > 0)
				{
					PickMobController.IsPickingItems = true;
					return;
				}
			}
			if (Pk9rPickMob.IsTanSat)
			{
				if (@char.isCharge)
				{
					PickMobController.Wait(500);
					return;
				}
				@char.clearFocus(0);
				if (@char.mobFocus != null && !PickMobController.IsMobTanSat(@char.mobFocus))
				{
					@char.mobFocus = null;
				}
				if (@char.mobFocus == null)
				{
					@char.mobFocus = PickMobController.GetMobTanSat();
					if (flag && @char.mobFocus != null)
					{
						@char.cx = @char.mobFocus.xFirst - 24;
						@char.cy = @char.mobFocus.yFirst;
						Service.gI().charMove();
					}
				}
				if (@char.mobFocus != null)
				{
					if (@char.skillInfoPaint() == null)
					{
						Skill skillAttack = PickMobController.GetSkillAttack();
						if (skillAttack != null && !skillAttack.paintCanNotUseSkill)
						{
							Mob mobFocus = @char.mobFocus;
							mobFocus.x = mobFocus.xFirst;
							mobFocus.y = mobFocus.yFirst;
							if (Pk9rPickMob.IsAttackMonsterBySendCommand)
							{
								if (global::Char.myCharz().myskill != skillAttack)
								{
									Service.gI().selectSkill((int)skillAttack.template.id);
									global::Char.myCharz().myskill = skillAttack;
								}
								if (mobFocus.getTemplate().type == MonsterType.Fly)
								{
									if (Math.Abs(global::Char.myCharz().cx - mobFocus.x) > 70)
									{
										PickMobController.Move(mobFocus.x, Utils.GetYGround(mobFocus.x));
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
									PickMobController.Move(mobFocus.xFirst, mobFocus.yFirst);
								}
								if ((Utils.Distance(global::Char.myCharz(), mobFocus) <= 50 || (mobFocus.getTemplate().type == MonsterType.Fly && Math.Abs(global::Char.myCharz().cx - mobFocus.x) <= 70)) && mSystem.currentTimeMillis() - skillAttack.lastTimeUseThisSkill > (long)skillAttack.coolDown + 100L)
								{
									global::Char.myCharz().mobFocus = mobFocus;
									skillAttack.lastTimeUseThisSkill = mSystem.currentTimeMillis();
									MyVector myVector = new MyVector();
									myVector.addElement(mobFocus);
									Service.gI().sendPlayerAttack(myVector, new MyVector(), -1);
								}
							}
							else
							{
								GameScr.gI().doSelectSkill(skillAttack, true);
								if (Res.distance(mobFocus.xFirst, mobFocus.yFirst, @char.cx, @char.cy) <= 48)
								{
									@char.focusManualTo(mobFocus);
									Utils.DoDoubleClickToObj(mobFocus);
								}
								else
								{
									PickMobController.Move(mobFocus.xFirst, mobFocus.yFirst);
								}
							}
						}
					}
				}
				else if (!flag)
				{
					Mob mobNext = PickMobController.GetMobNext();
					if (mobNext != null)
					{
						PickMobController.Move(mobNext.xFirst - 24, mobNext.yFirst);
					}
				}
				PickMobController.Wait(500);
			}
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x000AAF30 File Offset: 0x000A9130
		private static void Move(int x, int y)
		{
			global::Char @char = global::Char.myCharz();
			if (!Pk9rPickMob.IsVuotDiaHinh)
			{
				@char.currentMovePoint = new MovePoint(x, y);
				return;
			}
			int[] pointYsdMax = PickMobController.GetPointYsdMax(@char.cx, x);
			if (pointYsdMax[1] >= y || (pointYsdMax[1] >= @char.cy && (@char.statusMe == 2 || @char.statusMe == 1)))
			{
				pointYsdMax[0] = x;
				pointYsdMax[1] = y;
			}
			@char.currentMovePoint = new MovePoint(pointYsdMax[0], pointYsdMax[1]);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000AAFA4 File Offset: 0x000A91A4
		private static PickMobController.TypePickItem GetTpyePickItem(ItemMap itemMap)
		{
			global::Char @char = global::Char.myCharz();
			bool flag = itemMap.playerId == @char.charID || itemMap.playerId == -1;
			if (Pk9rPickMob.IsItemMe && !flag)
			{
				return PickMobController.TypePickItem.CanNotPickItem;
			}
			if (Pk9rPickMob.IsLimitTimesPickItem && itemMap.countAutoPick > Pk9rPickMob.TimesAutoPickItemMax)
			{
				return PickMobController.TypePickItem.CanNotPickItem;
			}
			if (!PickMobController.FilterItemPick(itemMap))
			{
				return PickMobController.TypePickItem.CanNotPickItem;
			}
			if (Res.abs(@char.cx - itemMap.xEnd) < 60 && Res.abs(@char.cy - itemMap.yEnd) < 60)
			{
				return PickMobController.TypePickItem.PickItemNormal;
			}
			if (ItemTime.isExistItem(4387))
			{
				return PickMobController.TypePickItem.PickItemTDLT;
			}
			if (Pk9rPickMob.IsTanSat)
			{
				return PickMobController.TypePickItem.PickItemTanSat;
			}
			return PickMobController.TypePickItem.CanNotPickItem;
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x000AB044 File Offset: 0x000A9244
		private static bool FilterItemPick(ItemMap itemMap)
		{
			return (Pk9rPickMob.IdItemPicks.Count == 0 || Pk9rPickMob.IdItemPicks.Contains(itemMap.template.id)) && (Pk9rPickMob.IdItemBlocks.Count == 0 || !Pk9rPickMob.IdItemBlocks.Contains(itemMap.template.id)) && (Pk9rPickMob.TypeItemPicks.Count == 0 || Pk9rPickMob.TypeItemPicks.Contains(itemMap.template.type)) && (Pk9rPickMob.TypeItemBlocks.Count == 0 || !Pk9rPickMob.TypeItemBlocks.Contains(itemMap.template.type));
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000AB0E8 File Offset: 0x000A92E8
		private static Mob GetMobTanSat()
		{
			Mob mob = null;
			int num = int.MaxValue;
			global::Char @char = global::Char.myCharz();
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(i);
				int num2 = (mob2.xFirst - @char.cx) * (mob2.xFirst - @char.cx) + (mob2.yFirst - @char.cy) * (mob2.yFirst - @char.cy);
				if (PickMobController.IsMobTanSat(mob2) && num2 < num)
				{
					mob = mob2;
					num = num2;
				}
			}
			return mob;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x000AB180 File Offset: 0x000A9380
		private static Mob GetMobNext()
		{
			Mob mob = null;
			long num = mSystem.currentTimeMillis();
			for (int i = 0; i < GameScr.vMob.size(); i++)
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(i);
				if (PickMobController.IsMobNext(mob2) && mob2.lastTimeDie < num)
				{
					mob = mob2;
					num = mob2.lastTimeDie;
				}
			}
			return mob;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x000AB1D8 File Offset: 0x000A93D8
		private static bool IsMobTanSat(Mob mob)
		{
			if (mob.status == 0 || mob.status == 1 || mob.hp <= 0 || mob.isMobMe)
			{
				return false;
			}
			bool flag = Pk9rPickMob.IsNeSieuQuai && !ItemTime.isExistItem(4387);
			return (mob.levelBoss == 0 || !flag) && PickMobController.FilterMobTanSat(mob);
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x000AB23C File Offset: 0x000A943C
		private static bool IsMobNext(Mob mob)
		{
			if (mob.isMobMe)
			{
				return false;
			}
			if (!PickMobController.FilterMobTanSat(mob))
			{
				return false;
			}
			if (Pk9rPickMob.IsNeSieuQuai && !ItemTime.isExistItem(4387) && mob.getTemplate().hp >= 3000)
			{
				if (mob.levelBoss != 0)
				{
					Mob mob2 = null;
					bool flag = false;
					for (int i = 0; i < GameScr.vMob.size(); i++)
					{
						mob2 = (Mob)GameScr.vMob.elementAt(i);
						if (mob2.countDie == 10 && (mob2.status == 0 || mob2.status == 1))
						{
							flag = true;
							break;
						}
					}
					if (!flag)
					{
						return false;
					}
					mob.lastTimeDie = mob2.lastTimeDie;
				}
				else if (mob.countDie == 10 && (mob.status == 0 || mob.status == 1))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x000AB310 File Offset: 0x000A9510
		private static bool FilterMobTanSat(Mob mob)
		{
			return (Pk9rPickMob.IdMobsTanSat.Count == 0 || Pk9rPickMob.IdMobsTanSat.Contains(mob.mobId)) && (Pk9rPickMob.TypeMobsTanSat.Count == 0 || Pk9rPickMob.TypeMobsTanSat.Contains((int)mob.getTemplate().mobTemplateId));
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000AB364 File Offset: 0x000A9564
		private static Skill GetSkillAttack()
		{
			Skill skill = null;
			SkillTemplate skillTemplate = new SkillTemplate();
			foreach (sbyte b in Pk9rPickMob.IdSkillsTanSat)
			{
				skillTemplate.id = b;
				Skill skill2 = global::Char.myCharz().getSkill(skillTemplate);
				if (PickMobController.IsSkillBetter(skill2, skill))
				{
					skill = skill2;
				}
			}
			return skill;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x000AB3D8 File Offset: 0x000A95D8
		private static bool IsSkillBetter(Skill SkillBetter, Skill skill)
		{
			if (SkillBetter == null)
			{
				return false;
			}
			if (!PickMobController.CanUseSkill(SkillBetter))
			{
				return false;
			}
			bool flag = (SkillBetter.template.id == 17 && skill.template.id == 2) || (SkillBetter.template.id == 9 && skill.template.id == 0);
			return skill == null || skill.coolDown < SkillBetter.coolDown || flag;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x000AB44C File Offset: 0x000A964C
		private static bool CanUseSkill(Skill skill)
		{
			if (mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill > (long)skill.coolDown)
			{
				skill.paintCanNotUseSkill = false;
			}
			return (!skill.paintCanNotUseSkill || PickMobController.IdSkillsMelee.Contains(skill.template.id)) && !PickMobController.IdSkillsCanNotAttack.Contains(skill.template.id) && global::Char.myCharz().cMP >= PickMobController.GetManaUseSkill(skill);
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000AB4C4 File Offset: 0x000A96C4
		private static int GetManaUseSkill(Skill skill)
		{
			if (skill.template.manaUseType == 2)
			{
				return 1;
			}
			if (skill.template.manaUseType == 1)
			{
				return skill.manaUse * global::Char.myCharz().cMPFull / 100;
			}
			return skill.manaUse;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x000AB500 File Offset: 0x000A9700
		private static int GetYsd(int xsd)
		{
			global::Char @char = global::Char.myCharz();
			int num = TileMap.pxh;
			int num2 = -1;
			for (int i = 24; i < TileMap.pxh; i += 24)
			{
				if (TileMap.tileTypeAt(xsd, i, 2))
				{
					int num3 = Res.abs(i - @char.cy);
					if (num3 < num)
					{
						num = num3;
						num2 = i;
					}
				}
			}
			return num2;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x000AB558 File Offset: 0x000A9758
		private static int[] GetPointYsdMax(int xStart, int xEnd)
		{
			int num = TileMap.pxh;
			int num2 = -1;
			if (xStart > xEnd)
			{
				for (int i = xEnd; i < xStart; i += 24)
				{
					int ysd = PickMobController.GetYsd(i);
					if (ysd < num)
					{
						num = ysd;
						num2 = i;
					}
				}
			}
			else
			{
				for (int j = xEnd; j > xStart; j -= 24)
				{
					int ysd2 = PickMobController.GetYsd(j);
					if (ysd2 < num)
					{
						num = ysd2;
						num2 = j;
					}
				}
			}
			return new int[] { num2, num };
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x000AB5C2 File Offset: 0x000A97C2
		private static void Wait(int time)
		{
			PickMobController.IsWait = true;
			PickMobController.TimeStartWait = mSystem.currentTimeMillis();
			PickMobController.TimeWait = (long)time;
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x000AB5DB File Offset: 0x000A97DB
		private static bool IsWaiting()
		{
			if (PickMobController.IsWait && mSystem.currentTimeMillis() - PickMobController.TimeStartWait >= PickMobController.TimeWait)
			{
				PickMobController.IsWait = false;
			}
			return PickMobController.IsWait;
		}

		// Token: 0x040016B3 RID: 5811
		private const int TIME_REPICKITEM = 500;

		// Token: 0x040016B4 RID: 5812
		private const int TIME_DELAY_TANSAT = 500;

		// Token: 0x040016B5 RID: 5813
		private const int ID_ICON_ITEM_TDLT = 4387;

		// Token: 0x040016B6 RID: 5814
		private static readonly sbyte[] IdSkillsMelee = new sbyte[] { 0, 9, 2, 17, 4 };

		// Token: 0x040016B7 RID: 5815
		private static readonly sbyte[] IdSkillsCanNotAttack = new sbyte[] { 10, 11, 14, 23, 7 };

		// Token: 0x040016B8 RID: 5816
		private static readonly PickMobController _Instance = new PickMobController();

		// Token: 0x040016B9 RID: 5817
		public static bool IsPickingItems;

		// Token: 0x040016BA RID: 5818
		private static bool IsWait;

		// Token: 0x040016BB RID: 5819
		private static long TimeStartWait;

		// Token: 0x040016BC RID: 5820
		private static long TimeWait;

		// Token: 0x040016BD RID: 5821
		public static List<ItemMap> ItemPicks = new List<ItemMap>();

		// Token: 0x040016BE RID: 5822
		private static int IndexItemPick = 0;

		// Token: 0x02000127 RID: 295
		private enum TypePickItem
		{
			// Token: 0x040016C0 RID: 5824
			CanNotPickItem,
			// Token: 0x040016C1 RID: 5825
			PickItemNormal,
			// Token: 0x040016C2 RID: 5826
			PickItemTDLT,
			// Token: 0x040016C3 RID: 5827
			PickItemTanSat
		}
	}
}
