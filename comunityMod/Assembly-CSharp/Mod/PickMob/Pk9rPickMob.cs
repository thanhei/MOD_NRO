using System;
using System.Collections.Generic;
using System.Linq;
using Mod.ModHelper.CommandMod.Chat;
using Mod.ModHelper.Menu;
using Mod.R;

namespace Mod.PickMob
{
	// Token: 0x02000128 RID: 296
	internal class Pk9rPickMob
	{
		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000E7B RID: 3707 RVA: 0x000AB657 File Offset: 0x000A9857
		// (set) Token: 0x06000E7C RID: 3708 RVA: 0x000AB65E File Offset: 0x000A985E
		internal static bool IsTanSat { get; set; }

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000E7D RID: 3709 RVA: 0x000AB666 File Offset: 0x000A9866
		// (set) Token: 0x06000E7E RID: 3710 RVA: 0x000AB66D File Offset: 0x000A986D
		internal static bool IsNeSieuQuai { get; set; } = true;

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000E7F RID: 3711 RVA: 0x000AB675 File Offset: 0x000A9875
		// (set) Token: 0x06000E80 RID: 3712 RVA: 0x000AB67C File Offset: 0x000A987C
		internal static bool IsVuotDiaHinh { get; set; } = true;

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000E81 RID: 3713 RVA: 0x000AB684 File Offset: 0x000A9884
		// (set) Token: 0x06000E82 RID: 3714 RVA: 0x000AB68B File Offset: 0x000A988B
		internal static bool IsAutoPickItems { get; set; } = true;

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000E83 RID: 3715 RVA: 0x000AB693 File Offset: 0x000A9893
		// (set) Token: 0x06000E84 RID: 3716 RVA: 0x000AB69A File Offset: 0x000A989A
		internal static bool IsItemMe { get; set; } = true;

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000E85 RID: 3717 RVA: 0x000AB6A2 File Offset: 0x000A98A2
		// (set) Token: 0x06000E86 RID: 3718 RVA: 0x000AB6A9 File Offset: 0x000A98A9
		internal static bool IsLimitTimesPickItem { get; set; } = true;

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x06000E87 RID: 3719 RVA: 0x000AB6B1 File Offset: 0x000A98B1
		// (set) Token: 0x06000E88 RID: 3720 RVA: 0x000AB6B8 File Offset: 0x000A98B8
		internal static bool IsAttackMonsterBySendCommand { get; set; }

		// Token: 0x06000E89 RID: 3721 RVA: 0x000AB6C0 File Offset: 0x000A98C0
		internal static Pk9rPickMob getInstance()
		{
			if (Pk9rPickMob._Instance == null)
			{
				Pk9rPickMob._Instance = new Pk9rPickMob();
			}
			return Pk9rPickMob._Instance;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000AB6D8 File Offset: 0x000A98D8
		internal static void SetSlaughter(bool newState)
		{
			Pk9rPickMob.IsTanSat = newState;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x000AB6E0 File Offset: 0x000A98E0
		internal static void SetAvoidSuperMonster(bool newState)
		{
			Pk9rPickMob.IsNeSieuQuai = newState;
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000AB6E8 File Offset: 0x000A98E8
		internal static void SetCrossTerrain(bool newState)
		{
			Pk9rPickMob.IsVuotDiaHinh = newState;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x000AB6F0 File Offset: 0x000A98F0
		internal static void SetAutoPickItems(bool newState)
		{
			Pk9rPickMob.IsAutoPickItems = newState;
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x000AB6F8 File Offset: 0x000A98F8
		internal static void SetAutoPickItemsFromOthers(bool newState)
		{
			Pk9rPickMob.IsItemMe = newState;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000AB700 File Offset: 0x000A9900
		internal static void SetPickUpLimited(bool newState)
		{
			Pk9rPickMob.IsLimitTimesPickItem = newState;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x000AB708 File Offset: 0x000A9908
		internal static void SetAttackMonsterBySendCommand(bool newState)
		{
			Pk9rPickMob.IsAttackMonsterBySendCommand = newState;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000AB710 File Offset: 0x000A9910
		[ChatCommand("ts")]
		internal static void ToggleSlaughter()
		{
			Pk9rPickMob.SetSlaughter(!Pk9rPickMob.IsTanSat);
			GameScr.info1.addInfo(Strings.pickMobTitle + ": " + Strings.OnOffStatus(Pk9rPickMob.IsTanSat), 0);
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000AB744 File Offset: 0x000A9944
		[ChatCommand("add")]
		internal static void ToggleSelectedMob()
		{
			Mob mobFocus = global::Char.myCharz().mobFocus;
			ItemMap itemFocus = global::Char.myCharz().itemFocus;
			if (mobFocus != null)
			{
				Pk9rPickMob.AddOrRemoveMonsterAutoAttack(mobFocus.mobId);
				return;
			}
			if (itemFocus != null)
			{
				Pk9rPickMob.AddOrRemoveItemAutoPick(itemFocus.template.id);
				return;
			}
			GameScr.info1.addInfo(Strings.pickMobPlsFocusOnMonsterOrItem + "!", 0);
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000AB7A4 File Offset: 0x000A99A4
		[ChatCommand("addm")]
		internal static void AddOrRemoveMonsterAutoAttack(int mobId)
		{
			if (Pk9rPickMob.IdMobsTanSat.Contains(mobId))
			{
				Pk9rPickMob.IdMobsTanSat.Remove(mobId);
				GameScr.info1.addInfo(string.Format(Strings.pickMobMonsterRemoved, mobId) + "!", 0);
				return;
			}
			Pk9rPickMob.IdMobsTanSat.Add(mobId);
			GameScr.info1.addInfo(string.Format(Strings.pickMobMonsterAdded, mobId) + "!", 0);
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000AB820 File Offset: 0x000A9A20
		[ChatCommand("addi")]
		internal static void AddOrRemoveItemAutoPick(short id)
		{
			if (Pk9rPickMob.IdItemPicks.Contains(id))
			{
				Pk9rPickMob.IdItemPicks.Remove(id);
				GameScr.info1.addInfo(string.Format(Strings.pickMobAutoPickItemListRemoved, ItemTemplates.get(id).name, id) + "!", 0);
				return;
			}
			Pk9rPickMob.IdItemPicks.Add(id);
			GameScr.info1.addInfo(string.Format(Strings.pickMobAutoPickItemListAdded, ItemTemplates.get(id).name, id) + "!", 0);
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000AB8B4 File Offset: 0x000A9AB4
		[ChatCommand("addt")]
		internal static void ToggleItemOrMobType()
		{
			Mob mobFocus = global::Char.myCharz().mobFocus;
			ItemMap itemFocus = global::Char.myCharz().itemFocus;
			if (mobFocus != null)
			{
				Pk9rPickMob.AddOrRemoveMonsterTypeAutoAttack(mobFocus.getTemplate().mobTemplateId);
				return;
			}
			if (itemFocus != null)
			{
				Pk9rPickMob.AddOrRemoveItemTypeAutoPick(itemFocus.template.type);
				return;
			}
			GameScr.info1.addInfo(Strings.pickMobPlsFocusOnMonsterOrItem + "!", 0);
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x000AB91C File Offset: 0x000A9B1C
		[ChatCommand("addtm")]
		internal static void AddOrRemoveMonsterTypeAutoAttack(sbyte type)
		{
			if (Pk9rPickMob.TypeMobsTanSat.Contains((int)type))
			{
				Pk9rPickMob.TypeMobsTanSat.Remove((int)type);
				GameScr.info1.addInfo(string.Format(Strings.pickMobMonsterTypeRemoved, Mob.arrMobTemplate[(int)type].name, Mob.arrMobTemplate[(int)type].mobTemplateId) + "!", 0);
				return;
			}
			Pk9rPickMob.TypeMobsTanSat.Add((int)type);
			GameScr.info1.addInfo(string.Format(Strings.pickMobMonsterTypeAdded, Mob.arrMobTemplate[(int)type].name, Mob.arrMobTemplate[(int)type].mobTemplateId) + "!", 0);
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x000AB9C8 File Offset: 0x000A9BC8
		[ChatCommand("addti")]
		internal static void AddOrRemoveItemTypeAutoPick(sbyte type)
		{
			if (Pk9rPickMob.TypeItemPicks.Contains(type))
			{
				Pk9rPickMob.TypeItemPicks.Remove(type);
				GameScr.info1.addInfo(string.Format(Strings.pickMobAutoPickItemTypesListRemoved, type) + "!", 0);
				return;
			}
			Pk9rPickMob.TypeItemPicks.Add(type);
			GameScr.info1.addInfo(string.Format(Strings.pickMobAutoPickItemTypesListAdded, type) + "!", 0);
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x000ABA44 File Offset: 0x000A9C44
		[ChatCommand("clrm")]
		internal static void ClearMonsterToFightList()
		{
			Pk9rPickMob.IdMobsTanSat.Clear();
			Pk9rPickMob.TypeMobsTanSat.Clear();
			GameScr.info1.addInfo(Strings.pickMobMonsterListCleared + "!", 0);
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x000ABA74 File Offset: 0x000A9C74
		[ChatCommand("vdh")]
		internal static void ToggleCrossTerrain()
		{
			Pk9rPickMob.SetCrossTerrain(!Pk9rPickMob.IsVuotDiaHinh);
			GameScr.info1.addInfo(Strings.pickMobVDHTitle + ": " + Strings.OnOffStatus(Pk9rPickMob.IsVuotDiaHinh), 0);
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x000ABAA7 File Offset: 0x000A9CA7
		[ChatCommand("nsq")]
		internal static void ToggleAvoidSuperMonsters()
		{
			Pk9rPickMob.SetAvoidSuperMonster(!Pk9rPickMob.IsNeSieuQuai);
			GameScr.info1.addInfo(Strings.pickMobAvoidSuperMobTitle + ": " + Strings.OnOffStatus(Pk9rPickMob.IsNeSieuQuai), 0);
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x000ABADA File Offset: 0x000A9CDA
		[ChatCommand("anhat")]
		internal static void ToggleAutoPickUpItems()
		{
			Pk9rPickMob.SetAutoPickItems(Pk9rPickMob.IsAutoPickItems);
			GameScr.info1.addInfo(Strings.autoPickItemTitle + ": " + Strings.OnOffStatus(Pk9rPickMob.IsAutoPickItems), 0);
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000ABB0A File Offset: 0x000A9D0A
		[ChatCommand("itm")]
		internal static void ToggleFilterOtherCharItems()
		{
			Pk9rPickMob.SetAutoPickItemsFromOthers(!Pk9rPickMob.IsItemMe);
			GameScr.info1.addInfo(Strings.pickMobPickMyItemOnlyTitle + ": " + Strings.OnOffStatus(Pk9rPickMob.IsItemMe), 0);
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x000ABB40 File Offset: 0x000A9D40
		[ChatCommand("sln")]
		internal static void TogglePickUpLimit()
		{
			Pk9rPickMob.SetPickUpLimited(!Pk9rPickMob.IsLimitTimesPickItem);
			GameScr.info1.addInfo(Strings.pickMobLimitPickTimesTitle + ": " + Strings.OnOffStatus(Pk9rPickMob.IsLimitTimesPickItem) + (Pk9rPickMob.IsLimitTimesPickItem ? (", " + Pk9rPickMob.TimesAutoPickItemMax.ToString()) : ""), 0);
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x000ABBA0 File Offset: 0x000A9DA0
		[ChatCommand("sln")]
		internal static void SetPickUpLimit(int limit)
		{
			Pk9rPickMob.TimesAutoPickItemMax = limit;
			GameScr.info1.addInfo(Strings.pickMobLimitPickTimesTitle + ": " + Pk9rPickMob.TimesAutoPickItemMax.ToString(), 0);
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x000ABBCC File Offset: 0x000A9DCC
		[ChatCommand("clri")]
		internal static void ResetItemFilterToDefault()
		{
			Pk9rPickMob.IdItemPicks.Clear();
			Pk9rPickMob.TypeItemPicks.Clear();
			Pk9rPickMob.TypeItemBlocks.Clear();
			Pk9rPickMob.IdItemBlocks.Clear();
			Pk9rPickMob.IdItemBlocks.AddRange(Pk9rPickMob.IdItemBlockBase);
			GameScr.info1.addInfo(Strings.pickMobItemListResetToDefault + "!", 0);
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x000ABC2C File Offset: 0x000A9E2C
		[ChatCommand("cnn")]
		internal static void SetToPickOnlyGems()
		{
			Pk9rPickMob.IdItemPicks.Clear();
			Pk9rPickMob.TypeItemPicks.Clear();
			Pk9rPickMob.TypeItemBlocks.Clear();
			Pk9rPickMob.IdItemBlocks.Clear();
			Pk9rPickMob.IdItemBlocks.AddRange(Pk9rPickMob.IdItemBlockBase);
			Pk9rPickMob.IdItemPicks.Add(77);
			Pk9rPickMob.IdItemPicks.Add(861);
			GameScr.info1.addInfo(Strings.pickMobConfiguredPickGemsOnly + "!", 0);
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x000ABCA5 File Offset: 0x000A9EA5
		[ChatCommand("skill")]
		internal static void ToggleSelectedSkillForSlaughter()
		{
			Pk9rPickMob.AddOrRemoveSkillAutoAttack(global::Char.myCharz().myskill.template.id);
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x000ABCC0 File Offset: 0x000A9EC0
		[ChatCommand("skill")]
		internal static void AddOrRemoveSkillAutoAttack(int index)
		{
			SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[index - 1];
			if (Pk9rPickMob.IdSkillsTanSat.Contains(skillTemplate.id))
			{
				Pk9rPickMob.IdSkillsTanSat.Remove(skillTemplate.id);
				GameScr.info1.addInfo(string.Format(Strings.pickMobSkillListRemoved, skillTemplate.name, skillTemplate.id) + "!", 0);
				return;
			}
			Pk9rPickMob.IdSkillsTanSat.Add(skillTemplate.id);
			GameScr.info1.addInfo(string.Format(Strings.pickMobSkillListAdded, skillTemplate.name, skillTemplate.id) + "!", 0);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000ABD78 File Offset: 0x000A9F78
		[ChatCommand("skillid")]
		internal static void AddOrRemoveSkillAutoAttack(sbyte id)
		{
			SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates.FirstOrDefault<SkillTemplate>((SkillTemplate t) => t.id == id);
			if (skillTemplate == null)
			{
				return;
			}
			if (Pk9rPickMob.IdSkillsTanSat.Contains(id))
			{
				Pk9rPickMob.IdSkillsTanSat.Remove(id);
				GameScr.info1.addInfo(string.Format(Strings.pickMobSkillListRemoved, skillTemplate.name, skillTemplate.id) + "!", 0);
				return;
			}
			Pk9rPickMob.IdSkillsTanSat.Add(id);
			GameScr.info1.addInfo(string.Format(Strings.pickMobSkillListAdded, skillTemplate.name, skillTemplate.id) + "!", 0);
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000ABE4B File Offset: 0x000AA04B
		[ChatCommand("clrs")]
		internal static void SaveCurrentSkillListAsDefault()
		{
			Pk9rPickMob.IdSkillsTanSat.Clear();
			Pk9rPickMob.IdSkillsTanSat.AddRange(Pk9rPickMob.IdSkillsBase);
			GameScr.info1.addInfo(Strings.pickMobSkillListResetToDefault + "!", 0);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x000ABE80 File Offset: 0x000AA080
		[ChatCommand("blocki")]
		internal static void BlockFocusedItem()
		{
			ItemMap itemFocus = global::Char.myCharz().itemFocus;
			if (itemFocus != null)
			{
				Pk9rPickMob.BlockItem(itemFocus.template.id);
				return;
			}
			GameScr.info1.addInfo(Strings.pickMobPlsFocusOnItem + "!", 0);
		}

		// Token: 0x06000EA6 RID: 3750 RVA: 0x000ABEC8 File Offset: 0x000AA0C8
		[ChatCommand("blockti")]
		internal static void BlockFocusedItemType()
		{
			ItemMap itemFocus = global::Char.myCharz().itemFocus;
			if (itemFocus != null)
			{
				Pk9rPickMob.BlockItemType(itemFocus.template.type);
				return;
			}
			GameScr.info1.addInfo(Strings.pickMobPlsFocusOnItem + "!", 0);
		}

		// Token: 0x06000EA7 RID: 3751 RVA: 0x000ABF10 File Offset: 0x000AA110
		[ChatCommand("blocki")]
		internal static void BlockItem(short id)
		{
			if (Pk9rPickMob.IdItemBlocks.Contains(id))
			{
				Pk9rPickMob.IdItemBlocks.Remove(id);
				GameScr.info1.addInfo(string.Format(Strings.pickMobDontPickItemListAdded, ItemTemplates.get(id).name, id) + "!", 0);
				return;
			}
			Pk9rPickMob.IdItemBlocks.Add(id);
			GameScr.info1.addInfo(string.Format(Strings.pickMobDontPickItemListRemoved, ItemTemplates.get(id).name, id) + "!", 0);
		}

		// Token: 0x06000EA8 RID: 3752 RVA: 0x000ABFA4 File Offset: 0x000AA1A4
		[ChatCommand("blockti")]
		internal static void BlockItemType(sbyte type)
		{
			if (Pk9rPickMob.TypeItemBlocks.Contains(type))
			{
				Pk9rPickMob.TypeItemBlocks.Remove(type);
				GameScr.info1.addInfo(string.Format(Strings.pickMobDontPickItemTypeListRemoved, type) + "!", 0);
				return;
			}
			Pk9rPickMob.TypeItemBlocks.Add(type);
			GameScr.info1.addInfo(string.Format(Strings.pickMobDontPickItemTypeListAdded, type) + "!", 0);
		}

		// Token: 0x06000EA9 RID: 3753 RVA: 0x000AC020 File Offset: 0x000AA220
		[Obsolete("Không cần dùng")]
		internal static bool Chat(string text)
		{
			if (Pk9rPickMob.IsGetInfoChat<int>(text, "sln"))
			{
				Pk9rPickMob.TimesAutoPickItemMax = Pk9rPickMob.GetInfoChat<int>(text, "sln");
				GameScr.info1.addInfo("Số lần nhặt giới hạn là: " + Pk9rPickMob.TimesAutoPickItemMax.ToString(), 0);
			}
			else if (Pk9rPickMob.IsGetInfoChat<short>(text, "addi"))
			{
				short infoChat = Pk9rPickMob.GetInfoChat<short>(text, "addi");
				if (Pk9rPickMob.IdItemPicks.Contains(infoChat))
				{
					Pk9rPickMob.IdItemPicks.Remove(infoChat);
					GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách chỉ tự động nhặt item: {0}[{1}]", ItemTemplates.get(infoChat).name, infoChat), 0);
				}
				else
				{
					Pk9rPickMob.IdItemPicks.Add(infoChat);
					GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách chỉ tự động nhặt item: {0}[{1}]", ItemTemplates.get(infoChat).name, infoChat), 0);
				}
			}
			else if (Pk9rPickMob.IsGetInfoChat<short>(text, "blocki"))
			{
				short infoChat2 = Pk9rPickMob.GetInfoChat<short>(text, "blocki");
				if (Pk9rPickMob.IdItemBlocks.Contains(infoChat2))
				{
					Pk9rPickMob.IdItemBlocks.Remove(infoChat2);
					GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách không tự động nhặt item: {0}[{1}]", ItemTemplates.get(infoChat2).name, infoChat2), 0);
				}
				else
				{
					Pk9rPickMob.IdItemBlocks.Add(infoChat2);
					GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách không tự động nhặt item: {0}[{1}]", ItemTemplates.get(infoChat2).name, infoChat2), 0);
				}
			}
			else if (Pk9rPickMob.IsGetInfoChat<sbyte>(text, "addti"))
			{
				sbyte infoChat3 = Pk9rPickMob.GetInfoChat<sbyte>(text, "addti");
				if (Pk9rPickMob.TypeItemPicks.Contains(infoChat3))
				{
					Pk9rPickMob.TypeItemPicks.Remove(infoChat3);
					GameScr.info1.addInfo("Đã xoá khỏi danh sách chỉ tự động nhặt loại item: " + infoChat3.ToString(), 0);
				}
				else
				{
					Pk9rPickMob.TypeItemPicks.Add(infoChat3);
					GameScr.info1.addInfo("Đã thêm vào danh sách chỉ tự động nhặt loại item: " + infoChat3.ToString(), 0);
				}
			}
			else if (Pk9rPickMob.IsGetInfoChat<sbyte>(text, "blockti"))
			{
				sbyte infoChat4 = Pk9rPickMob.GetInfoChat<sbyte>(text, "blockti");
				if (Pk9rPickMob.TypeItemBlocks.Contains(infoChat4))
				{
					Pk9rPickMob.TypeItemBlocks.Remove(infoChat4);
					GameScr.info1.addInfo("Đã xoá khỏi danh sách không tự động nhặt loại item: " + infoChat4.ToString(), 0);
				}
				else
				{
					Pk9rPickMob.TypeItemBlocks.Add(infoChat4);
					GameScr.info1.addInfo("Đã thêm vào danh sách không tự động nhặt loại item: " + infoChat4.ToString(), 0);
				}
			}
			else if (Pk9rPickMob.IsGetInfoChat<int>(text, "addm"))
			{
				int infoChat5 = Pk9rPickMob.GetInfoChat<int>(text, "addm");
				if (Pk9rPickMob.IdMobsTanSat.Contains(infoChat5))
				{
					Pk9rPickMob.IdMobsTanSat.Remove(infoChat5);
					GameScr.info1.addInfo("Đã xoá mob: " + infoChat5.ToString(), 0);
				}
				else
				{
					Pk9rPickMob.IdMobsTanSat.Add(infoChat5);
					GameScr.info1.addInfo("Đã thêm mob: " + infoChat5.ToString(), 0);
				}
			}
			else if (Pk9rPickMob.IsGetInfoChat<int>(text, "addtm"))
			{
				int infoChat6 = Pk9rPickMob.GetInfoChat<int>(text, "addtm");
				if (Pk9rPickMob.TypeMobsTanSat.Contains(infoChat6))
				{
					Pk9rPickMob.TypeMobsTanSat.Remove(infoChat6);
					GameScr.info1.addInfo(string.Format("Đã xoá loại mob: {0}[{1}]", Mob.arrMobTemplate[infoChat6].name, infoChat6), 0);
				}
				else
				{
					Pk9rPickMob.TypeMobsTanSat.Add(infoChat6);
					GameScr.info1.addInfo(string.Format("Đã thêm loại mob: {0}[{1}]", Mob.arrMobTemplate[infoChat6].name, infoChat6), 0);
				}
			}
			else if (Pk9rPickMob.IsGetInfoChat<int>(text, "skill"))
			{
				int num = Pk9rPickMob.GetInfoChat<int>(text, "skill") - 1;
				SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[num];
				if (Pk9rPickMob.IdSkillsTanSat.Contains(skillTemplate.id))
				{
					Pk9rPickMob.IdSkillsTanSat.Remove(skillTemplate.id);
					GameScr.info1.addInfo(string.Format("Đã xoá khỏi danh sách skill sử dụng tự động đánh quái skill: {0}[{1}]", skillTemplate.name, skillTemplate.id), 0);
				}
				else
				{
					Pk9rPickMob.IdSkillsTanSat.Add(skillTemplate.id);
					GameScr.info1.addInfo(string.Format("Đã thêm vào danh sách skill sử dụng tự động đánh quái skill: {0}[{1}]", skillTemplate.name, skillTemplate.id), 0);
				}
			}
			else
			{
				if (!Pk9rPickMob.IsGetInfoChat<sbyte>(text, "skillid"))
				{
					return false;
				}
				sbyte infoChat7 = Pk9rPickMob.GetInfoChat<sbyte>(text, "skillid");
				if (Pk9rPickMob.IdSkillsTanSat.Contains(infoChat7))
				{
					Pk9rPickMob.IdSkillsTanSat.Remove(infoChat7);
					GameScr.info1.addInfo("Đã xoá khỏi danh sách skill sử dụng tự động đánh quái skill: " + infoChat7.ToString(), 0);
				}
				else
				{
					Pk9rPickMob.IdSkillsTanSat.Add(infoChat7);
					GameScr.info1.addInfo("Đã thêm vào danh sách skill sử dụng tự động đánh quái skill: " + infoChat7.ToString(), 0);
				}
			}
			return true;
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x000AC4FC File Offset: 0x000AA6FC
		[Obsolete("Không cần dùng")]
		internal static bool HotKeys()
		{
			int keyAsciiPress = GameCanvas.keyAsciiPress;
			if (keyAsciiPress <= 98)
			{
				if (keyAsciiPress == 97)
				{
					Pk9rPickMob.ToggleSelectedMob();
					return true;
				}
				if (keyAsciiPress == 98)
				{
					Pk9rPickMob.Chat("abf");
					return true;
				}
			}
			else
			{
				if (keyAsciiPress == 110)
				{
					Pk9rPickMob.ToggleAutoPickUpItems();
					return true;
				}
				if (keyAsciiPress == 116)
				{
					Pk9rPickMob.ToggleSlaughter();
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000AC54F File Offset: 0x000AA74F
		internal static void Update()
		{
			PickMobController.Update();
		}

		// Token: 0x06000EAC RID: 3756 RVA: 0x000AC558 File Offset: 0x000AA758
		internal static void MobStartDie(object obj)
		{
			Mob mob = (Mob)obj;
			if (mob.status != 1 && mob.status != 0)
			{
				mob.lastTimeDie = mSystem.currentTimeMillis();
				mob.countDie++;
				if (mob.countDie > 10)
				{
					mob.countDie = 0;
				}
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x000AC5A7 File Offset: 0x000AA7A7
		internal static void UpdateCountDieMob(Mob mob)
		{
			if (mob.levelBoss != 0)
			{
				mob.countDie = 0;
			}
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x000AC5B8 File Offset: 0x000AA7B8
		internal static void ShowMenu()
		{
			string text = string.Empty;
			Mob mobFocus = global::Char.myCharz().mobFocus;
			ItemMap itemFocus = global::Char.myCharz().itemFocus;
			string text2 = "PickMobNRO by Phucprotein\n";
			if (mobFocus != null || itemFocus != null)
			{
				if (mobFocus != null)
				{
					text2 += string.Format(Strings.pickMobFocusedMob, mobFocus.getTemplate().name, mobFocus.mobId, mobFocus.getTemplate().mobTemplateId);
				}
				else
				{
					text2 += string.Format(Strings.pickMobFocusedItem, itemFocus.template.name, itemFocus.template.id, itemFocus.template.type);
				}
			}
			MenuBuilder menuBuilder = new MenuBuilder().setChatPopup(text2);
			if (mobFocus != null || itemFocus != null)
			{
				if (mobFocus != null)
				{
					if (Pk9rPickMob.IdMobsTanSat.Contains(mobFocus.mobId))
					{
						text = string.Format(Strings.pickMobRemoveMobIdFromList, mobFocus.mobId);
					}
					else
					{
						text = string.Format(Strings.pickMobAddMobIdToList, mobFocus.mobId);
					}
				}
				else if (itemFocus != null)
				{
					if (Pk9rPickMob.IdItemPicks.Contains(itemFocus.template.id))
					{
						text = string.Format(Strings.pickMobRemoveFromList, itemFocus.template.name);
					}
					else
					{
						text = string.Format(Strings.pickMobAddToList, itemFocus.template.name);
					}
				}
				menuBuilder.addItem(text, new MenuAction(new Action(Pk9rPickMob.ToggleSelectedMob)));
				if (mobFocus != null)
				{
					if (Pk9rPickMob.TypeMobsTanSat.Contains((int)mobFocus.getTemplate().mobTemplateId))
					{
						text = string.Format(Strings.pickMobRemoveFromList, mobFocus.getTemplate().name);
					}
					else
					{
						text = string.Format(Strings.pickMobAddToList, mobFocus.getTemplate().name);
					}
				}
				else if (itemFocus != null)
				{
					if (Pk9rPickMob.TypeItemPicks.Contains(itemFocus.template.type))
					{
						text = string.Format(Strings.pickMobRemoveItemTypeFromList, itemFocus.template.type);
					}
					else
					{
						text = string.Format(Strings.pickMobAddItemTypeToList, itemFocus.template.type);
					}
				}
				menuBuilder.addItem(text, new MenuAction(new Action(Pk9rPickMob.ToggleItemOrMobType)));
				if (itemFocus != null)
				{
					if (Pk9rPickMob.IdItemBlocks.Contains(itemFocus.template.id))
					{
						text = string.Format(Strings.pickMobRemoveFromDontPickList, itemFocus.template.name);
					}
					else
					{
						text = string.Format(Strings.pickMobAddToDontPickList, itemFocus.template.name);
					}
					menuBuilder.addItem(text, new MenuAction(delegate
					{
						Pk9rPickMob.BlockItem(itemFocus.template.id);
					}));
					if (Pk9rPickMob.TypeItemBlocks.Contains(itemFocus.template.type))
					{
						text = string.Format(Strings.pickMobRemoveItemTypeFromDontPickList, itemFocus.template.type);
					}
					else
					{
						text = string.Format(Strings.pickMobAddItemTypeToDontPickList, itemFocus.template.type);
					}
					menuBuilder.addItem(text, new MenuAction(delegate
					{
						Pk9rPickMob.BlockItemType(itemFocus.template.type);
					}));
				}
			}
			if (Pk9rPickMob.IdMobsTanSat.Count + Pk9rPickMob.TypeMobsTanSat.Count > 0)
			{
				menuBuilder.addItem(Strings.pickMobClearMonsterList, new MenuAction(delegate
				{
					Pk9rPickMob.ClearMonsterToFightList();
				}));
			}
			SkillTemplate template = global::Char.myCharz().myskill.template;
			if (Pk9rPickMob.IdSkillsTanSat.Contains(template.id))
			{
				text = string.Format(Strings.pickMobRemoveFromSkillList, template.name);
			}
			else
			{
				text = string.Format(Strings.pickMobAddToSkillList, template.name);
			}
			menuBuilder.addItem(text, new MenuAction(delegate
			{
				Pk9rPickMob.ToggleSelectedSkillForSlaughter();
			}));
			menuBuilder.addItem(Strings.pickMobResetSkillListToDefault, new MenuAction(delegate
			{
				Pk9rPickMob.SaveCurrentSkillListAsDefault();
			}));
			menuBuilder.addItem(Strings.pickMobResetItemListToDefault, new MenuAction(delegate
			{
				Pk9rPickMob.ResetItemFilterToDefault();
			}));
			if (Pk9rPickMob.IdMobsTanSat.Count + Pk9rPickMob.TypeMobsTanSat.Count > 0)
			{
				menuBuilder.addItem(Strings.pickMobViewMonsterList, new MenuAction(delegate
				{
					string text3 = Strings.pickMobMonsterIdList + ": ";
					if (Pk9rPickMob.IdMobsTanSat.Count > 0)
					{
						foreach (int num in Pk9rPickMob.IdMobsTanSat)
						{
							text3 = text3 + num.ToString() + ", ";
						}
						text3 = text3.Remove(text3.LastIndexOf(','), 2);
					}
					else
					{
						text3 += Strings.empty;
					}
					text3 = text3 + "\n" + Strings.pickMobMonsterTypeList + ": ";
					if (Pk9rPickMob.TypeMobsTanSat.Count > 0)
					{
						foreach (int num2 in Pk9rPickMob.TypeMobsTanSat)
						{
							text3 += string.Format("{0} [{1}], ", Mob.arrMobTemplate[num2].name, num2);
						}
						text3 = text3.Remove(text3.LastIndexOf(','), 2);
					}
					else
					{
						text3 += Strings.empty;
					}
					GameCanvas.startOKDlg(text3);
				}));
			}
			if (Pk9rPickMob.IdItemPicks.Count + Pk9rPickMob.TypeItemPicks.Count + Pk9rPickMob.IdItemBlocks.Count + Pk9rPickMob.TypeItemBlocks.Count > 0)
			{
				menuBuilder.addItem(Strings.pickMobViewItemList, new MenuAction(delegate
				{
					string text4 = Strings.pickMobAutoPickItemList + ": ";
					if (Pk9rPickMob.IdItemPicks.Count > 0)
					{
						foreach (short num3 in Pk9rPickMob.IdItemPicks)
						{
							text4 += string.Format("{0} [{1}], ", ItemTemplates.get(num3).name, num3);
						}
						text4 = text4.Remove(text4.LastIndexOf(','), 2);
					}
					else
					{
						text4 += Strings.empty;
					}
					text4 += string.Format("\n{0}: ", Strings.pickMobAutoPickItemTypeList);
					if (Pk9rPickMob.TypeItemPicks.Count > 0)
					{
						foreach (sbyte b in Pk9rPickMob.TypeItemPicks)
						{
							text4 = text4 + b.ToString() + ", ";
						}
						text4 = text4.Remove(text4.LastIndexOf(','), 2);
					}
					else
					{
						text4 += Strings.empty;
					}
					text4 += string.Format("\n{0}: ", Strings.pickMobDontPickItemList);
					if (Pk9rPickMob.IdItemBlocks.Count > 0)
					{
						foreach (short num4 in Pk9rPickMob.IdItemBlocks)
						{
							text4 += string.Format("{0} [{1}], ", ItemTemplates.get(num4).name, num4);
						}
						text4 = text4.Remove(text4.LastIndexOf(','), 2);
					}
					else
					{
						text4 += Strings.empty;
					}
					text4 += string.Format("\n{0}: ", Strings.pickMobDontPickItemTypeList);
					if (Pk9rPickMob.TypeItemBlocks.Count > 0)
					{
						foreach (sbyte b2 in Pk9rPickMob.TypeItemBlocks)
						{
							text4 = text4 + b2.ToString() + ", ";
						}
						text4 = text4.Remove(text4.LastIndexOf(','), 2);
					}
					else
					{
						text4 += Strings.empty;
					}
					GameCanvas.startOKDlg(text4);
				}));
			}
			menuBuilder.addItem(Strings.pickMobViewSkillList, new MenuAction(delegate
			{
				string text5 = Strings.pickMobSkillList + ": ";
				foreach (sbyte b3 in Pk9rPickMob.IdSkillsTanSat)
				{
					for (int i = 0; i < 8; i++)
					{
						SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[i];
						if (skillTemplate.id == b3)
						{
							text5 += string.Format("{0} [{1}]", skillTemplate.name, b3);
							goto IL_0087;
						}
					}
				}
				IL_0087:
				GameCanvas.startOKDlg(text5);
			}));
			menuBuilder.start();
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x000ACB1C File Offset: 0x000AAD1C
		private static bool IsGetInfoChat<T>(string text, string s)
		{
			if (!text.StartsWith(s))
			{
				return false;
			}
			try
			{
				Convert.ChangeType(text.Substring(s.Length), typeof(T));
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000ACB6C File Offset: 0x000AAD6C
		private static T GetInfoChat<T>(string text, string s)
		{
			return (T)((object)Convert.ChangeType(text.Substring(s.Length), typeof(T)));
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x000ACB90 File Offset: 0x000AAD90
		private static bool IsGetInfoChat<T>(string text, string s, int n)
		{
			if (!text.StartsWith(s))
			{
				return false;
			}
			try
			{
				string[] array = text.Substring(s.Length).Split(' ', StringSplitOptions.None);
				for (int i = 0; i < n; i++)
				{
					Convert.ChangeType(array[i], typeof(T));
				}
			}
			catch
			{
				return false;
			}
			return true;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x000ACBF8 File Offset: 0x000AADF8
		private static T[] GetInfoChat<T>(string text, string s, int n)
		{
			T[] array = new T[n];
			string[] array2 = text.Substring(s.Length).Split(' ', StringSplitOptions.None);
			for (int i = 0; i < n; i++)
			{
				array[i] = (T)((object)Convert.ChangeType(array2[i], typeof(T)));
			}
			return array;
		}

		// Token: 0x040016C4 RID: 5828
		private const int ID_ITEM_GEM = 77;

		// Token: 0x040016C5 RID: 5829
		private const int ID_ITEM_GEM_LOCK = 861;

		// Token: 0x040016C6 RID: 5830
		private const int DEFAULT_HP_BUFF = 20;

		// Token: 0x040016C7 RID: 5831
		private const int DEFAULT_MP_BUFF = 20;

		// Token: 0x040016C8 RID: 5832
		private static readonly sbyte[] IdSkillsBase = new sbyte[] { 0, 2, 17, 4 };

		// Token: 0x040016C9 RID: 5833
		private static readonly short[] IdItemBlockBase = new short[] { 225, 353, 354, 355, 356, 357, 358, 359, 360, 362 };

		// Token: 0x040016D1 RID: 5841
		internal static List<int> IdMobsTanSat = new List<int>();

		// Token: 0x040016D2 RID: 5842
		internal static List<int> TypeMobsTanSat = new List<int>();

		// Token: 0x040016D3 RID: 5843
		internal static List<sbyte> IdSkillsTanSat = new List<sbyte>(Pk9rPickMob.IdSkillsBase);

		// Token: 0x040016D4 RID: 5844
		internal static int TimesAutoPickItemMax = 7;

		// Token: 0x040016D5 RID: 5845
		internal static List<short> IdItemPicks = new List<short>();

		// Token: 0x040016D6 RID: 5846
		internal static List<short> IdItemBlocks = new List<short>(Pk9rPickMob.IdItemBlockBase);

		// Token: 0x040016D7 RID: 5847
		internal static List<sbyte> TypeItemPicks = new List<sbyte>();

		// Token: 0x040016D8 RID: 5848
		internal static List<sbyte> TypeItemBlocks = new List<sbyte>();

		// Token: 0x040016D9 RID: 5849
		private static Pk9rPickMob _Instance;
	}
}
