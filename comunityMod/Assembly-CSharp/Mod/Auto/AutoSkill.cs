using System;
using System.Collections;
using Mod.Constants;
using Mod.R;
using UnityEngine;

namespace Mod.Auto
{
	// Token: 0x02000181 RID: 385
	internal class AutoSkill
	{
		// Token: 0x170000E6 RID: 230
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x000BB103 File Offset: 0x000B9303
		// (set) Token: 0x06001162 RID: 4450 RVA: 0x000BB10A File Offset: 0x000B930A
		internal static AutoSkill.TargetMode targetMode { get; set; }

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x000BB112 File Offset: 0x000B9312
		internal static bool shouldReviveDeadChars
		{
			get
			{
				return AutoSkill.targetMode > AutoSkill.TargetMode.None;
			}
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000BB11C File Offset: 0x000B931C
		internal static void setReviveTargetMode(int target)
		{
			AutoSkill.targetMode = (AutoSkill.TargetMode)target;
			if (AutoSkill.shouldReviveDeadChars && global::Char.myCharz().cgender != (int)CharGender.Namekian)
			{
				AutoSkill.targetMode = AutoSkill.TargetMode.None;
				GameScr.info1.addInfo(Strings.youAreNotNamekian + "!", 0);
			}
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x000BB15C File Offset: 0x000B935C
		internal static void Update()
		{
			if (!AutoSkill.shouldReviveDeadChars || (float)GameCanvas.gameTick % (30f * Time.timeScale) != 0f)
			{
				return;
			}
			global::Char deadCharInMap = AutoSkill.getDeadCharInMap();
			Skill skill = global::Char.myCharz().getSkill(global::Char.myCharz().nClass.skillTemplates[2]);
			if (deadCharInMap == null || !skill.CanUse())
			{
				return;
			}
			if (AutoSkill.canHealChar(deadCharInMap) && skill.point <= 1)
			{
				AutoSkill.useSkillOn(deadCharInMap, skill);
				return;
			}
			Utils.buffMe();
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x000BB1D8 File Offset: 0x000B93D8
		private static bool isValidTarget(global::Char target)
		{
			switch (AutoSkill.targetMode)
			{
			case AutoSkill.TargetMode.Everyone:
				return true;
			case AutoSkill.TargetMode.OnlyClanMembers:
				return target.IsFromMyClan();
			case AutoSkill.TargetMode.OnlyPet:
				return target.IsPet();
			case AutoSkill.TargetMode.OnlyMyPet:
				return target.IsPet() && global::Char.myCharz().GetPetId() == target.charID;
			default:
				return false;
			}
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x000BB234 File Offset: 0x000B9434
		private static global::Char getDeadCharInMap()
		{
			int i;
			for (i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
				if (AutoSkill.isValidTarget(@char) && @char.IsCharDead())
				{
					return @char;
				}
			}
			if (i == GameScr.vCharInMap.size() && AutoSkill.isValidTarget(global::Char.myCharz()) && global::Char.myCharz().IsCharDead())
			{
				return global::Char.myCharz();
			}
			return null;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x000BB2A5 File Offset: 0x000B94A5
		private static bool canHealChar(global::Char ch)
		{
			return ch.cFlag == global::Char.myCharz().cFlag;
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x000BB2BC File Offset: 0x000B94BC
		private static void useSkillOn(global::Char c, Skill skill)
		{
			Service.gI().selectSkill((int)skill.template.id);
			Service.gI().sendPlayerAttack(new MyVector(), new MyVector(new ArrayList { c }), -1);
			skill.lastTimeUseThisSkill = mSystem.currentTimeMillis();
		}

		// Token: 0x02000182 RID: 386
		internal enum TargetMode
		{
			// Token: 0x040018B3 RID: 6323
			None,
			// Token: 0x040018B4 RID: 6324
			Everyone,
			// Token: 0x040018B5 RID: 6325
			OnlyClanMembers,
			// Token: 0x040018B6 RID: 6326
			OnlyPet,
			// Token: 0x040018B7 RID: 6327
			OnlyMyPet
		}
	}
}
