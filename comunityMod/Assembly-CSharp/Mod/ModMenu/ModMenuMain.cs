using System;
using System.Collections;
using System.Linq;
using Mod.AccountManager;
using Mod.Auto;
using Mod.Background;
using Mod.CharEffect;
using Mod.CustomPanel;
using Mod.DeveloperFunctions;
using Mod.Graphics;
using Mod.ModHelper;
using Mod.PickMob;
using Mod.R;
using Mod.Set;
using Mod.TeleportMenu;
using Mod.Xmap;
using UnityEngine;

namespace Mod.ModMenu
{
	// Token: 0x02000134 RID: 308
	internal static class ModMenuMain
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000F0D RID: 3853 RVA: 0x000AD758 File Offset: 0x000AB958
		// (set) Token: 0x06000F0E RID: 3854 RVA: 0x000AD75F File Offset: 0x000AB95F
		internal static Panel currentPanel
		{
			get
			{
				return GameCanvas.panel2;
			}
			set
			{
				GameCanvas.panel2 = value;
			}
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x000AD768 File Offset: 0x000AB968
		internal static void Initialize()
		{
			if (ModMenuMain.cmdOpenModMenu != null)
			{
				return;
			}
			ModMenuMain.cmdOpenModMenu = new Command("", ModMenuMain.actionListener, 1, null);
			ModMenuMain.cmdOpenModMenu.img = new Image();
			ModMenuMain.imgMenu = CustomGraphics.FlipTextureHorizontally(GameScr.imgMenu.texture);
			ModMenuMain.cmdOpenModMenu.img.texture = ModMenuMain.imgMenu;
			ModMenuMain.cmdOpenModMenu.img.w = ModMenuMain.cmdOpenModMenu.img.texture.width;
			ModMenuMain.cmdOpenModMenu.img.h = ModMenuMain.cmdOpenModMenu.img.texture.height;
			ModMenuMain.cmdOpenModMenu.isPlaySoundButton = false;
			ModMenuMain.cmdOpenModMenu.w = ModMenuMain.cmdOpenModMenu.img.w / mGraphics.zoomLevel;
			ModMenuMain.cmdOpenModMenu.h = ModMenuMain.cmdOpenModMenu.img.h / mGraphics.zoomLevel;
			ModMenuMain.UpdatePosition();
			ModMenuMain.LoadModMenuItems();
			ModMenuMain.LoadData();
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000AD868 File Offset: 0x000ABA68
		internal static void UpdatePosition()
		{
			if (ModMenuMain.cmdOpenModMenu == null)
			{
				return;
			}
			ModMenuMain.cmdOpenModMenu.x = GameCanvas.w - ModMenuMain.cmdOpenModMenu.w;
			ModMenuMain.cmdOpenModMenu.y = (int)((float)mGraphics.getImageHeight(GameScr.imgChat) * 1.5f);
			if (ModMenuMain.currentPanel != null && ModMenuMain.currentPanel == GameCanvas.panel2 && ModMenuMain.currentPanel.type == CustomPanelMenu.TYPE_CUSTOM_PANEL_MENU)
			{
				ModMenuMain.currentPanel.cmdClose.x = GameCanvas.w - ModMenuMain.currentPanel.cmdClose.img.getWidth() - 1;
				ModMenuMain.currentPanel.cmdClose.y = 1;
			}
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x000AD912 File Offset: 0x000ABB12
		internal static void UpdateLanguage(sbyte newLanguage)
		{
			if (newLanguage == ModMenuMain.lastLanguage)
			{
				return;
			}
			ModMenuMain.lastLanguage = newLanguage;
			ModMenuMain.LoadModMenuItems();
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000AD928 File Offset: 0x000ABB28
		private static void LoadModMenuItems()
		{
			ModMenuItemBoolean[] array = new ModMenuItemBoolean[26];
			int num = 0;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig.ID = "VSync_Toggle";
			modMenuItemBooleanConfig.Title = "VSync";
			modMenuItemBooleanConfig.Description = Strings.vSyncDescription;
			modMenuItemBooleanConfig.GetValueFunc = () => QualitySettings.vSyncCount == 1;
			modMenuItemBooleanConfig.SetValueAction = delegate(bool value)
			{
				QualitySettings.vSyncCount = (value ? 1 : 0);
			};
			modMenuItemBooleanConfig.RMSName = "enable_vsync";
			array[num] = new ModMenuItemBoolean(modMenuItemBooleanConfig);
			int num2 = 1;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig2 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig2.ID = "PickMob_Toggle";
			modMenuItemBooleanConfig2.Title = Strings.pickMobTitle;
			modMenuItemBooleanConfig2.Description = Strings.pickMobDescription;
			modMenuItemBooleanConfig2.GetValueFunc = () => Pk9rPickMob.IsTanSat;
			modMenuItemBooleanConfig2.SetValueAction = new Action<bool>(Pk9rPickMob.SetSlaughter);
			modMenuItemBooleanConfig2.GetIsDisabled = () => AutoTrainNewAccount.isEnabled;
			modMenuItemBooleanConfig2.GetDisabledReason = () => string.Format(Strings.functionShouldBeDisabled, Strings.autoTrainForNewbieTitle);
			array[num2] = new ModMenuItemBoolean(modMenuItemBooleanConfig2);
			int num3 = 2;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig3 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig3.ID = "PickMob_AutoPickItem_Toggle";
			modMenuItemBooleanConfig3.Title = Strings.autoPickItemTitle;
			modMenuItemBooleanConfig3.Description = Strings.autoPickItemDescription;
			modMenuItemBooleanConfig3.GetValueFunc = () => Pk9rPickMob.IsAutoPickItems;
			modMenuItemBooleanConfig3.SetValueAction = new Action<bool>(Pk9rPickMob.SetAutoPickItems);
			modMenuItemBooleanConfig3.RMSName = "pickmob_auto_pick";
			modMenuItemBooleanConfig3.GetIsDisabled = () => AutoTrainNewAccount.isEnabled;
			modMenuItemBooleanConfig3.GetDisabledReason = () => string.Format(Strings.functionShouldBeDisabled, Strings.autoTrainForNewbieTitle);
			array[num3] = new ModMenuItemBoolean(modMenuItemBooleanConfig3);
			int num4 = 3;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig4 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig4.ID = "AutoTrainForNewbie_Toggle";
			modMenuItemBooleanConfig4.Title = Strings.autoTrainForNewbieTitle;
			modMenuItemBooleanConfig4.Description = Strings.autoTrainForNewbieDescription;
			modMenuItemBooleanConfig4.GetValueFunc = () => AutoTrainNewAccount.isEnabled;
			modMenuItemBooleanConfig4.SetValueAction = new Action<bool>(AutoTrainNewAccount.SetState);
			modMenuItemBooleanConfig4.GetIsDisabled = () => global::Char.myCharz().taskMaint == null || global::Char.myCharz().taskMaint.taskId > 11;
			modMenuItemBooleanConfig4.GetDisabledReason = () => Strings.noLongerNewAccount + "!";
			array[num4] = new ModMenuItemBoolean(modMenuItemBooleanConfig4);
			int num5 = 4;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig5 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig5.ID = "AutoSellTrashItems_Toggle";
			modMenuItemBooleanConfig5.Title = Strings.autoSellTrashItemsTitle;
			modMenuItemBooleanConfig5.Description = Strings.autoSellTrashItemsDescription;
			modMenuItemBooleanConfig5.GetValueFunc = () => AutoSellTrashItems.isEnabled;
			modMenuItemBooleanConfig5.SetValueAction = new Action<bool>(AutoSellTrashItems.SetState);
			array[num5] = new ModMenuItemBoolean(modMenuItemBooleanConfig5);
			int num6 = 5;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig6 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig6.ID = "AutoSendAttack_Toggle";
			modMenuItemBooleanConfig6.Title = Strings.autoAttack;
			modMenuItemBooleanConfig6.Description = Strings.autoSendAttackDescription;
			modMenuItemBooleanConfig6.GetValueFunc = () => ThreadAction<AutoSendAttack>.gI.IsActing;
			modMenuItemBooleanConfig6.SetValueAction = new Action<bool>(ThreadActionUpdate<AutoSendAttack>.toggle);
			modMenuItemBooleanConfig6.GetIsDisabled = () => Pk9rPickMob.IsTanSat;
			modMenuItemBooleanConfig6.GetDisabledReason = () => string.Format(Strings.functionShouldBeDisabled, Strings.pickMobTitle) + "!";
			array[num6] = new ModMenuItemBoolean(modMenuItemBooleanConfig6);
			int num7 = 6;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig7 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig7.ID = "AutoLogin_Toggle";
			modMenuItemBooleanConfig7.Title = Strings.autoLoginTitle;
			modMenuItemBooleanConfig7.Description = Strings.autoLoginDescription;
			modMenuItemBooleanConfig7.GetValueFunc = () => AutoLogin.isEnabled;
			modMenuItemBooleanConfig7.SetValueAction = new Action<bool>(AutoLogin.SetState);
			array[num7] = new ModMenuItemBoolean(modMenuItemBooleanConfig7);
			int num8 = 7;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig8 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig8.ID = "ShowTargetInfo_Toggle";
			modMenuItemBooleanConfig8.Title = Strings.showTargetInfoTitle;
			modMenuItemBooleanConfig8.Description = Strings.showTargetInfoDescription;
			modMenuItemBooleanConfig8.GetValueFunc = () => CharEffectMain.isEnabled;
			modMenuItemBooleanConfig8.SetValueAction = new Action<bool>(CharEffectMain.setState);
			modMenuItemBooleanConfig8.RMSName = "show_target_info";
			array[num8] = new ModMenuItemBoolean(modMenuItemBooleanConfig8);
			int num9 = 8;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig9 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig9.ID = "SkipSpaceship_Toggle";
			modMenuItemBooleanConfig9.Title = Strings.skipSpaceshipTitle;
			modMenuItemBooleanConfig9.Description = Strings.skipSpaceshipDescription;
			modMenuItemBooleanConfig9.GetValueFunc = () => SpaceshipSkip.isEnabled;
			modMenuItemBooleanConfig9.SetValueAction = delegate(bool value)
			{
				SpaceshipSkip.isEnabled = value;
			};
			modMenuItemBooleanConfig9.RMSName = "skip_spaceship";
			array[num9] = new ModMenuItemBoolean(modMenuItemBooleanConfig9);
			int num10 = 9;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig10 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig10.ID = "AutoAskForPeans_Toggle";
			modMenuItemBooleanConfig10.Title = Strings.autoAskForPeansTitle;
			modMenuItemBooleanConfig10.Description = Strings.autoAskForPeansDescription;
			modMenuItemBooleanConfig10.GetValueFunc = () => AutoPean.isAutoRequest;
			modMenuItemBooleanConfig10.SetValueAction = delegate(bool value)
			{
				AutoPean.isAutoRequest = value;
			};
			modMenuItemBooleanConfig10.RMSName = "auto_ask_for_peans";
			modMenuItemBooleanConfig10.GetIsDisabled = () => global::Char.myCharz().clan == null;
			modMenuItemBooleanConfig10.GetDisabledReason = () => Strings.youAreNotInAClan + "!";
			array[num10] = new ModMenuItemBoolean(modMenuItemBooleanConfig10);
			int num11 = 10;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig11 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig11.ID = "AutoDonatePeans_Toggle";
			modMenuItemBooleanConfig11.Title = Strings.autoDonatePeansTitle;
			modMenuItemBooleanConfig11.Description = Strings.autoDonatePeansDescription;
			modMenuItemBooleanConfig11.GetValueFunc = () => AutoPean.isAutoDonate;
			modMenuItemBooleanConfig11.SetValueAction = delegate(bool value)
			{
				AutoPean.isAutoDonate = value;
			};
			modMenuItemBooleanConfig11.RMSName = "auto_donate_peans";
			modMenuItemBooleanConfig11.GetIsDisabled = () => global::Char.myCharz().clan == null;
			modMenuItemBooleanConfig11.GetDisabledReason = () => Strings.youAreNotInAClan + "!";
			array[num11] = new ModMenuItemBoolean(modMenuItemBooleanConfig11);
			int num12 = 11;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig12 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig12.ID = "AutoHarvestPeans_Toggle";
			modMenuItemBooleanConfig12.Title = Strings.autoHarvestPeansTitle;
			modMenuItemBooleanConfig12.Description = Strings.autoHarvestPeansDescription;
			modMenuItemBooleanConfig12.GetValueFunc = () => AutoPean.isAutoHarvest;
			modMenuItemBooleanConfig12.SetValueAction = delegate(bool value)
			{
				AutoPean.isAutoHarvest = value;
			};
			modMenuItemBooleanConfig12.RMSName = "auto_harvest_peans";
			array[num12] = new ModMenuItemBoolean(modMenuItemBooleanConfig12);
			int num13 = 12;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig13 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig13.ID = "CustomBg_Toggle";
			modMenuItemBooleanConfig13.Title = Strings.customBackgroundTitle;
			modMenuItemBooleanConfig13.Description = Strings.customBackgroundDescription;
			modMenuItemBooleanConfig13.GetValueFunc = () => CustomBackground.isEnabled;
			modMenuItemBooleanConfig13.SetValueAction = new Action<bool>(CustomBackground.SetState);
			modMenuItemBooleanConfig13.RMSName = "custom_bg";
			array[num13] = new ModMenuItemBoolean(modMenuItemBooleanConfig13);
			int num14 = 13;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig14 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig14.ID = "HideGameUI_Toggle";
			modMenuItemBooleanConfig14.Title = Strings.hideGameUITitle;
			modMenuItemBooleanConfig14.Description = Strings.hideGameUIDescription;
			modMenuItemBooleanConfig14.GetValueFunc = () => HideGameUI.isEnabled;
			modMenuItemBooleanConfig14.SetValueAction = new Action<bool>(HideGameUI.SetState);
			modMenuItemBooleanConfig14.RMSName = "hide_game_ui";
			array[num14] = new ModMenuItemBoolean(modMenuItemBooleanConfig14);
			int num15 = 14;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig15 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig15.ID = "Intro_Toggle";
			modMenuItemBooleanConfig15.Title = Strings.introTitle;
			modMenuItemBooleanConfig15.Description = Strings.introDescription;
			modMenuItemBooleanConfig15.GetValueFunc = () => IntroPlayer.isEnabled;
			modMenuItemBooleanConfig15.SetValueAction = delegate(bool value)
			{
				IntroPlayer.isEnabled = value;
			};
			modMenuItemBooleanConfig15.RMSName = "intro_enabled";
			array[num15] = new ModMenuItemBoolean(modMenuItemBooleanConfig15);
			int num16 = 15;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig16 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig16.ID = "Xmap_UseNormalCapsule_Toggle";
			modMenuItemBooleanConfig16.Title = Strings.xmapUseNormalCapsule;
			modMenuItemBooleanConfig16.Description = Strings.xmapUseNormalCapsuleDescription;
			modMenuItemBooleanConfig16.GetValueFunc = () => Pk9rXmap.isUseCapsuleNormal;
			modMenuItemBooleanConfig16.SetValueAction = delegate(bool value)
			{
				Pk9rXmap.isUseCapsuleNormal = value;
			};
			modMenuItemBooleanConfig16.RMSName = "xmap_use_normal_capsule";
			array[num16] = new ModMenuItemBoolean(modMenuItemBooleanConfig16);
			int num17 = 16;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig17 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig17.ID = "Xmap_UseCapsuleVIP_Toggle";
			modMenuItemBooleanConfig17.Title = Strings.xmapUseSpecialCapsule;
			modMenuItemBooleanConfig17.Description = Strings.xmapUseSpecialCapsuleDescription;
			modMenuItemBooleanConfig17.GetValueFunc = () => Pk9rXmap.isUseCapsuleVip;
			modMenuItemBooleanConfig17.SetValueAction = delegate(bool value)
			{
				Pk9rXmap.isUseCapsuleVip = value;
			};
			modMenuItemBooleanConfig17.RMSName = "xmap_use_capsule_vip";
			array[num17] = new ModMenuItemBoolean(modMenuItemBooleanConfig17);
			int num18 = 17;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig18 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig18.ID = "Xmap_UseAStar_Toggle";
			modMenuItemBooleanConfig18.Title = Strings.xmapUseAStar;
			modMenuItemBooleanConfig18.Description = Strings.xmapUseAStarDescription;
			modMenuItemBooleanConfig18.GetValueFunc = () => Pk9rXmap.isXmapAStar;
			modMenuItemBooleanConfig18.SetValueAction = delegate(bool value)
			{
				Pk9rXmap.isXmapAStar = value;
			};
			modMenuItemBooleanConfig18.RMSName = "xmap_use_astar";
			array[num18] = new ModMenuItemBoolean(modMenuItemBooleanConfig18);
			int num19 = 18;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig19 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig19.ID = "PickMob_AvoidSuperMob_Toggle";
			modMenuItemBooleanConfig19.Title = Strings.pickMobAvoidSuperMobTitle;
			modMenuItemBooleanConfig19.Description = Strings.avoidSuperMobDescription;
			modMenuItemBooleanConfig19.GetValueFunc = () => Pk9rPickMob.IsNeSieuQuai;
			modMenuItemBooleanConfig19.SetValueAction = new Action<bool>(Pk9rPickMob.SetAvoidSuperMonster);
			modMenuItemBooleanConfig19.RMSName = "pickmob_avoid_super_mob";
			array[num19] = new ModMenuItemBoolean(modMenuItemBooleanConfig19);
			int num20 = 19;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig20 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig20.ID = "PickMob_VDH_Toggle";
			modMenuItemBooleanConfig20.Title = Strings.pickMobVDHTitle;
			modMenuItemBooleanConfig20.Description = Strings.pickMobVDHDescription;
			modMenuItemBooleanConfig20.GetValueFunc = () => Pk9rPickMob.IsVuotDiaHinh;
			modMenuItemBooleanConfig20.SetValueAction = new Action<bool>(Pk9rPickMob.SetCrossTerrain);
			modMenuItemBooleanConfig20.RMSName = "pickmob_cross_terrain";
			array[num20] = new ModMenuItemBoolean(modMenuItemBooleanConfig20);
			int num21 = 20;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig21 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig21.ID = "PickMob_AttackMonsterBySendCommand_Toggle";
			modMenuItemBooleanConfig21.Title = Strings.pickMobAttackMonsterBySendCommandTitle;
			modMenuItemBooleanConfig21.Description = Strings.pickMobAttackMonsterBySendCommandDescription;
			modMenuItemBooleanConfig21.GetValueFunc = () => Pk9rPickMob.IsAttackMonsterBySendCommand;
			modMenuItemBooleanConfig21.SetValueAction = new Action<bool>(Pk9rPickMob.SetAttackMonsterBySendCommand);
			modMenuItemBooleanConfig21.RMSName = "pickmob_attack_monster_by_send_command";
			array[num21] = new ModMenuItemBoolean(modMenuItemBooleanConfig21);
			int num22 = 21;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig22 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig22.ID = "PickMob_PickMyItemOnly_Toggle";
			modMenuItemBooleanConfig22.Title = Strings.pickMobPickMyItemOnlyTitle;
			modMenuItemBooleanConfig22.Description = Strings.pickMobPickMyItemOnlyDescription;
			modMenuItemBooleanConfig22.GetValueFunc = () => Pk9rPickMob.IsItemMe;
			modMenuItemBooleanConfig22.SetValueAction = new Action<bool>(Pk9rPickMob.SetAutoPickItemsFromOthers);
			modMenuItemBooleanConfig22.RMSName = "pickmob_pick_my_item_only";
			array[num22] = new ModMenuItemBoolean(modMenuItemBooleanConfig22);
			int num23 = 22;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig23 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig23.ID = "PickMob_LimitPickTimes_Toggle";
			modMenuItemBooleanConfig23.Title = Strings.pickMobLimitPickTimesTitle;
			modMenuItemBooleanConfig23.Description = Strings.pickMobLimitPickTimesDescription;
			modMenuItemBooleanConfig23.GetValueFunc = () => Pk9rPickMob.IsLimitTimesPickItem;
			modMenuItemBooleanConfig23.SetValueAction = new Action<bool>(Pk9rPickMob.SetPickUpLimited);
			modMenuItemBooleanConfig23.RMSName = "pickmob_limit_pick_item_times";
			array[num23] = new ModMenuItemBoolean(modMenuItemBooleanConfig23);
			int num24 = 23;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig24 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig24.ID = "NotifyBoss_Toggle";
			modMenuItemBooleanConfig24.Title = Strings.notifyBossTitle;
			modMenuItemBooleanConfig24.Description = Strings.notifyBossDescription;
			modMenuItemBooleanConfig24.GetValueFunc = () => Boss.isEnabled;
			modMenuItemBooleanConfig24.SetValueAction = new Action<bool>(Boss.setState);
			modMenuItemBooleanConfig24.RMSName = "notify_boss";
			modMenuItemBooleanConfig24.GetIsDisabled = () => true;
			modMenuItemBooleanConfig24.GetDisabledReason = () => "This feature is currently in development state";
			array[num24] = new ModMenuItemBoolean(modMenuItemBooleanConfig24);
			int num25 = 24;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig25 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig25.ID = "ShowCharList_Toggle";
			modMenuItemBooleanConfig25.Title = Strings.showCharListTitle;
			modMenuItemBooleanConfig25.Description = Strings.showCharListDescription;
			modMenuItemBooleanConfig25.GetValueFunc = () => ListCharsInMap.isEnabled;
			modMenuItemBooleanConfig25.SetValueAction = new Action<bool>(ListCharsInMap.setState);
			modMenuItemBooleanConfig25.RMSName = "show_char_list";
			modMenuItemBooleanConfig25.GetIsDisabled = () => true;
			modMenuItemBooleanConfig25.GetDisabledReason = () => "This feature is currently in development state";
			array[num25] = new ModMenuItemBoolean(modMenuItemBooleanConfig25);
			int num26 = 25;
			ModMenuItemBooleanConfig modMenuItemBooleanConfig26 = new ModMenuItemBooleanConfig();
			modMenuItemBooleanConfig26.ID = "ShowPetInCharList_Toggle";
			modMenuItemBooleanConfig26.Title = Strings.showPetInCharListTitle;
			modMenuItemBooleanConfig26.Description = Strings.showPetInCharListDescription;
			modMenuItemBooleanConfig26.GetValueFunc = () => ListCharsInMap.isShowPet;
			modMenuItemBooleanConfig26.SetValueAction = new Action<bool>(ListCharsInMap.setStatePet);
			modMenuItemBooleanConfig26.RMSName = "show_pets_in_char_list";
			modMenuItemBooleanConfig26.GetIsDisabled = () => !ListCharsInMap.isEnabled;
			modMenuItemBooleanConfig26.GetDisabledReason = () => string.Format(Strings.functionShouldBeEnabled, Strings.showCharListTitle);
			array[num26] = new ModMenuItemBoolean(modMenuItemBooleanConfig26);
			ModMenuMain.modMenuItemBools = array;
			ModMenuItemValues[] array2 = new ModMenuItemValues[13];
			int num27 = 0;
			ModMenuItemValuesConfig modMenuItemValuesConfig = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig.ID = "Set_FPS";
			modMenuItemValuesConfig.Title = "FPS";
			modMenuItemValuesConfig.Description = Strings.setFPSDescription;
			modMenuItemValuesConfig.GetValueFunc = () => (double)Application.targetFrameRate;
			modMenuItemValuesConfig.SetValueAction = delegate(double value)
			{
				if (value >= 5.0 && value <= Screen.currentResolution.refreshRateRatio.value)
				{
					Application.targetFrameRate = (int)value;
				}
			};
			modMenuItemValuesConfig.MinValue = 5.0;
			modMenuItemValuesConfig.MaxValue = Screen.currentResolution.refreshRateRatio.value;
			modMenuItemValuesConfig.RMSName = "target_fps";
			modMenuItemValuesConfig.GetIsDisabled = () => QualitySettings.vSyncCount == 1;
			modMenuItemValuesConfig.GetDisabledReason = () => string.Format(Strings.functionShouldBeDisabled, "VSync");
			modMenuItemValuesConfig.TextFieldTitle = Strings.inputFPS;
			modMenuItemValuesConfig.TextFieldHint = "FPS";
			array2[num27] = new ModMenuItemValues(modMenuItemValuesConfig);
			int num28 = 1;
			ModMenuItemValuesConfig modMenuItemValuesConfig2 = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig2.ID = "Set_GameSpeed";
			modMenuItemValuesConfig2.Title = Strings.setGameSpeedTitle;
			modMenuItemValuesConfig2.Description = Strings.setGameSpeedDescription;
			modMenuItemValuesConfig2.IsFloatingPoint = true;
			modMenuItemValuesConfig2.GetValueFunc = () => (double)Time.timeScale;
			modMenuItemValuesConfig2.SetValueAction = delegate(double value)
			{
				if (value >= 0.25 && value <= 20.0)
				{
					Time.timeScale = (float)value;
				}
			};
			modMenuItemValuesConfig2.MinValue = 0.25;
			modMenuItemValuesConfig2.MaxValue = 20.0;
			modMenuItemValuesConfig2.RMSName = "game_speed";
			modMenuItemValuesConfig2.TextFieldTitle = Strings.inputGameSpeed;
			modMenuItemValuesConfig2.TextFieldHint = Strings.inputGameSpeedHint;
			array2[num28] = new ModMenuItemValues(modMenuItemValuesConfig2);
			int num29 = 2;
			ModMenuItemValuesConfig modMenuItemValuesConfig3 = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig3.ID = "Set_GameDelay";
			modMenuItemValuesConfig3.Title = Strings.setGameDelayTitle;
			modMenuItemValuesConfig3.Description = Strings.setGameDelayDescription;
			modMenuItemValuesConfig3.IsFloatingPoint = true;
			modMenuItemValuesConfig3.GetValueFunc = () => Math.Round((double)(Time.fixedDeltaTime * 100f), 3);
			modMenuItemValuesConfig3.SetValueAction = delegate(double value)
			{
				if (value >= 1.0 && value <= 5.0)
				{
					Time.fixedDeltaTime = (float)value / 100f;
				}
			};
			modMenuItemValuesConfig3.MinValue = 1.0;
			modMenuItemValuesConfig3.MaxValue = 5.0;
			modMenuItemValuesConfig3.RMSName = "game_delay";
			modMenuItemValuesConfig3.TextFieldTitle = Strings.inputGameDelay;
			modMenuItemValuesConfig3.TextFieldHint = Strings.inputGameDelayHint;
			array2[num29] = new ModMenuItemValues(modMenuItemValuesConfig3);
			int num30 = 3;
			ModMenuItemValuesConfig modMenuItemValuesConfig4 = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig4.ID = "Set_ReduceGraphics";
			modMenuItemValuesConfig4.Title = Strings.setReduceGraphicsTitle;
			modMenuItemValuesConfig4.Values = Strings.setReduceGraphicsChoices;
			modMenuItemValuesConfig4.GetValueFunc = () => (double)GraphicsReducer.Level;
			modMenuItemValuesConfig4.SetValueAction = delegate(double level)
			{
				GraphicsReducer.Level = (ReduceGraphicsLevel)level;
			};
			modMenuItemValuesConfig4.RMSName = "reduce_graphics";
			array2[num30] = new ModMenuItemValues(modMenuItemValuesConfig4);
			int num31 = 4;
			ModMenuItemValuesConfig modMenuItemValuesConfig5 = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig5.ID = "Set_MyCharSpeed";
			modMenuItemValuesConfig5.Title = Strings.setMyCharSpeedTitle;
			modMenuItemValuesConfig5.Description = Strings.setMyCharSpeedDescription;
			modMenuItemValuesConfig5.GetValueFunc = () => (double)Utils.myCharSpeed;
			modMenuItemValuesConfig5.SetValueAction = delegate(double value)
			{
				Utils.myCharSpeed = (int)value;
			};
			modMenuItemValuesConfig5.RMSName = "my_char_speed";
			modMenuItemValuesConfig5.MinValue = 0.0;
			modMenuItemValuesConfig5.MaxValue = 25.0;
			modMenuItemValuesConfig5.TextFieldTitle = Strings.inputMyCharSpeed;
			modMenuItemValuesConfig5.TextFieldHint = Strings.inputMyCharSpeedHint;
			array2[num31] = new ModMenuItemValues(modMenuItemValuesConfig5);
			int num32 = 5;
			ModMenuItemValuesConfig modMenuItemValuesConfig6 = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig6.ID = "Set_GoBack";
			modMenuItemValuesConfig6.Title = "GoBack";
			modMenuItemValuesConfig6.Values = Strings.setGoBackChoices;
			modMenuItemValuesConfig6.GetValueFunc = () => (double)AutoGoback.mode;
			modMenuItemValuesConfig6.SetValueAction = delegate(double value)
			{
				AutoGoback.setState((int)value);
			};
			modMenuItemValuesConfig6.GetIsDisabled = () => AutoTrainNewAccount.isEnabled;
			modMenuItemValuesConfig6.GetDisabledReason = () => string.Format(Strings.functionShouldBeDisabled, Strings.autoTrainForNewbieTitle);
			array2[num32] = new ModMenuItemValues(modMenuItemValuesConfig6);
			int num33 = 6;
			ModMenuItemValuesConfig modMenuItemValuesConfig7 = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig7.ID = "Set_AutoTrainPet";
			modMenuItemValuesConfig7.Title = Strings.setAutoTrainPetTitle;
			modMenuItemValuesConfig7.Values = Strings.setAutoTrainPetChoices;
			modMenuItemValuesConfig7.GetValueFunc = () => (double)AutoTrainPet.Mode;
			modMenuItemValuesConfig7.SetValueAction = delegate(double value)
			{
				AutoTrainPet.SetState((int)value);
			};
			modMenuItemValuesConfig7.GetIsDisabled = () => !global::Char.myCharz().havePet || AutoTrainNewAccount.isEnabled;
			modMenuItemValuesConfig7.GetDisabledReason = delegate
			{
				if (!global::Char.myCharz().havePet)
				{
					return Strings.youDontHaveDisciple + "!";
				}
				if (AutoTrainNewAccount.isEnabled)
				{
					return string.Format(Strings.functionShouldBeDisabled, Strings.autoTrainForNewbieTitle);
				}
				return string.Empty;
			};
			array2[num33] = new ModMenuItemValues(modMenuItemValuesConfig7);
			int num34 = 7;
			ModMenuItemValuesConfig modMenuItemValuesConfig8 = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig8.ID = "Set_AutoAttackWhenDiscipleNeed";
			modMenuItemValuesConfig8.Title = Strings.setAutoAttackWhenDiscipleNeededTitle;
			modMenuItemValuesConfig8.Values = Strings.setAutoAttackWhenDiscipleNeededChoices;
			modMenuItemValuesConfig8.GetValueFunc = () => (double)AutoTrainPet.ModeAttackWhenNeeded;
			modMenuItemValuesConfig8.SetValueAction = delegate(double value)
			{
				AutoTrainPet.SetAttackState((int)value);
			};
			modMenuItemValuesConfig8.RMSName = "auto_pet_mode";
			modMenuItemValuesConfig8.GetIsDisabled = () => AutoTrainPet.Mode <= AutoTrainPetMode.Disabled;
			modMenuItemValuesConfig8.GetDisabledReason = () => string.Format(Strings.functionShouldBeEnabled, Strings.setAutoTrainPetTitle);
			array2[num34] = new ModMenuItemValues(modMenuItemValuesConfig8);
			int num35 = 8;
			ModMenuItemValuesConfig modMenuItemValuesConfig9 = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig9.ID = "Set_AutoRescue";
			modMenuItemValuesConfig9.Title = Strings.setAutoRescueTitle;
			modMenuItemValuesConfig9.Values = Strings.setAutoRescueChoices;
			modMenuItemValuesConfig9.GetValueFunc = () => (double)AutoSkill.targetMode;
			modMenuItemValuesConfig9.SetValueAction = delegate(double value)
			{
				AutoSkill.setReviveTargetMode((int)value);
			};
			modMenuItemValuesConfig9.GetIsDisabled = delegate
			{
				if (global::Char.myCharz().cgender != 1)
				{
					return true;
				}
				Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(2);
				return skill == null || !skill.template.isBuffToPlayer();
			};
			modMenuItemValuesConfig9.GetDisabledReason = delegate
			{
				if (global::Char.myCharz().cgender != 1)
				{
					return Strings.youAreNotNamekian + "!";
				}
				Skill skill2 = (Skill)global::Char.myCharz().vSkillFight.elementAt(2);
				if (skill2 == null)
				{
					return Strings.setAutoRescueSkill3Null + "!";
				}
				if (!skill2.template.isBuffToPlayer())
				{
					return Strings.setAutoRescueSkill3BuffInvalid + "!";
				}
				return "";
			};
			array2[num35] = new ModMenuItemValues(modMenuItemValuesConfig9);
			int num36 = 9;
			ModMenuItemValuesConfig modMenuItemValuesConfig10 = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig10.ID = "Set_XmapTimeout";
			modMenuItemValuesConfig10.Title = Strings.xmapTimeout;
			modMenuItemValuesConfig10.Description = Strings.setXmapTimeoutDescription;
			modMenuItemValuesConfig10.GetValueFunc = () => (double)Pk9rXmap.aStarTimeout;
			modMenuItemValuesConfig10.SetValueAction = delegate(double value)
			{
				Pk9rXmap.aStarTimeout = (int)value;
			};
			modMenuItemValuesConfig10.MinValue = 10.0;
			modMenuItemValuesConfig10.MaxValue = 300.0;
			modMenuItemValuesConfig10.RMSName = "xmap_astar_timeout";
			modMenuItemValuesConfig10.TextFieldTitle = Strings.xmapTimeout;
			modMenuItemValuesConfig10.TextFieldHint = Strings.xmapEditTimeout + " (10-300s)";
			array2[num36] = new ModMenuItemValues(modMenuItemValuesConfig10);
			int num37 = 10;
			ModMenuItemValuesConfig modMenuItemValuesConfig11 = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig11.ID = "Set_BgScaleMode";
			modMenuItemValuesConfig11.Title = Strings.customBgDefaultScaleModeTitle;
			modMenuItemValuesConfig11.Values = new string[]
			{
				ScaleMode.StretchToFill.GetName(),
				ScaleMode.ScaleAndCrop.GetName(),
				ScaleMode.ScaleToFit.GetName()
			};
			modMenuItemValuesConfig11.GetValueFunc = () => (double)CustomBackground.DefaultScaleMode;
			modMenuItemValuesConfig11.SetValueAction = delegate(double value)
			{
				CustomBackground.DefaultScaleMode = (ScaleMode)value;
			};
			array2[num37] = new ModMenuItemValues(modMenuItemValuesConfig11);
			int num38 = 11;
			ModMenuItemValuesConfig modMenuItemValuesConfig12 = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig12.ID = "Set_TimeChangeBg";
			modMenuItemValuesConfig12.Title = Strings.setTimeChangeCustomBgTitle;
			modMenuItemValuesConfig12.Description = Strings.setTimeChangeCustomBgDescription;
			modMenuItemValuesConfig12.GetValueFunc = () => (double)(CustomBackground.intervalChangeBg / 1000);
			modMenuItemValuesConfig12.SetValueAction = delegate(double value)
			{
				CustomBackground.intervalChangeBg = (int)value * 1000;
			};
			modMenuItemValuesConfig12.TextFieldTitle = Strings.inputTimeChangeBg;
			modMenuItemValuesConfig12.TextFieldHint = Strings.inputTimeChangeBgHint;
			array2[num38] = new ModMenuItemValues(modMenuItemValuesConfig12);
			int num39 = 12;
			ModMenuItemValuesConfig modMenuItemValuesConfig13 = new ModMenuItemValuesConfig();
			modMenuItemValuesConfig13.ID = "Set_IntroVolume";
			modMenuItemValuesConfig13.Title = Strings.setIntroVolumeTitle;
			modMenuItemValuesConfig13.Description = Strings.setIntroVolumeDescription;
			modMenuItemValuesConfig13.GetValueFunc = () => (double)((int)(IntroPlayer.volume * 100f));
			modMenuItemValuesConfig13.SetValueAction = delegate(double value)
			{
				IntroPlayer.volume = (float)value / 100f;
			};
			modMenuItemValuesConfig13.RMSName = "intro_volume";
			modMenuItemValuesConfig13.TextFieldTitle = Strings.introInputVolume;
			modMenuItemValuesConfig13.TextFieldHint = Strings.introInputVolumeHint;
			modMenuItemValuesConfig13.MinValue = 0.0;
			modMenuItemValuesConfig13.MaxValue = 100.0;
			array2[num39] = new ModMenuItemValues(modMenuItemValuesConfig13);
			ModMenuMain.modMenuItemValues = array2;
			ModMenuItemFunction[] array3 = new ModMenuItemFunction[8];
			array3[0] = new ModMenuItemFunction(new ModMenuItemFunctionConfig
			{
				ID = "OpenXmapMenu",
				Title = Strings.openXmapMenuTitle,
				Description = Strings.openXmapMenuDescription,
				Action = new Action(Pk9rXmap.ShowXmapMenu)
			});
			array3[1] = new ModMenuItemFunction(new ModMenuItemFunctionConfig
			{
				ID = "OpenPickMobMenu",
				Title = Strings.openPickMobMenuTitle,
				Description = Strings.openPickMobMenuDescription,
				Action = new Action(Pk9rPickMob.ShowMenu)
			});
			array3[2] = new ModMenuItemFunction(new ModMenuItemFunctionConfig
			{
				ID = "OpenTeleportMenu",
				Title = Strings.openTeleportMenuTitle,
				Description = Strings.openTeleportMenuDescription,
				Action = new Action(TeleportMenuMain.ShowMenu)
			});
			array3[3] = new ModMenuItemFunction(new ModMenuItemFunctionConfig
			{
				ID = "OpenCustomBackgroundMenu",
				Title = Strings.openCustomBackgroundMenuTitle,
				Description = Strings.openCustomBackgroundMenuDescription,
				Action = new Action(CustomBackground.ShowMenu)
			});
			array3[4] = new ModMenuItemFunction(new ModMenuItemFunctionConfig
			{
				ID = "OpenIntroMenu",
				Title = Strings.openIntroMenuTitle,
				Description = Strings.openIntroMenuDescription,
				Action = new Action(IntroPlayer.ShowMenu)
			});
			array3[5] = new ModMenuItemFunction(new ModMenuItemFunctionConfig
			{
				ID = "OpenVietnameseInputMenu",
				Title = Strings.openVietnameseInputMenuTitle,
				Description = Strings.openVietnameseInputMenuDescription,
				Action = new Action(VietnameseInput.ShowMenu)
			});
			int num40 = 6;
			ModMenuItemFunctionConfig modMenuItemFunctionConfig = new ModMenuItemFunctionConfig();
			modMenuItemFunctionConfig.ID = "OpenSetsMenu";
			modMenuItemFunctionConfig.Title = Strings.openSetsMenuTitle;
			modMenuItemFunctionConfig.Description = Strings.openSetsMenuDescription;
			modMenuItemFunctionConfig.Action = new Action(SetDo.ShowMenu);
			modMenuItemFunctionConfig.GetIsDisabled = () => true;
			modMenuItemFunctionConfig.GetDisabledReason = () => "This feature is currently in development state";
			array3[num40] = new ModMenuItemFunction(modMenuItemFunctionConfig);
			int num41 = 7;
			ModMenuItemFunctionConfig modMenuItemFunctionConfig2 = new ModMenuItemFunctionConfig();
			modMenuItemFunctionConfig2.ID = "AddUserAoToAccountManager";
			modMenuItemFunctionConfig2.Title = Strings.addUserAoToAccountManagerTitle;
			modMenuItemFunctionConfig2.Description = Strings.addUserAoToAccountManagerDescription;
			modMenuItemFunctionConfig2.Action = new Action(InGameAccountManager.AddUserAoToAccountManager);
			modMenuItemFunctionConfig2.GetIsDisabled = () => Utils.IsOpenedByExternalAccountManager || InGameAccountManager.SelectedAccount != null || string.IsNullOrEmpty(Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()));
			modMenuItemFunctionConfig2.GetDisabledReason = delegate
			{
				if (Utils.IsOpenedByExternalAccountManager)
				{
					return Strings.openedByExternalAccountManager + "!";
				}
				if (InGameAccountManager.SelectedAccount != null)
				{
					return Strings.inGameAccountManagerUnregisteredAccountAlreadyAdded + "!";
				}
				return Strings.accountAlreadyRegistered + "!";
			};
			array3[num41] = new ModMenuItemFunction(modMenuItemFunctionConfig2);
			ModMenuMain.modMenuItemFunctions = array3;
			ModMenuMain.modMenuItemDeveloperFunctions = new ModMenuItemFunction[]
			{
				new ModMenuItemFunction(new ModMenuItemFunctionConfig
				{
					ID = "GetGameData",
					Title = "Extract data from the game",
					Description = "Extract information from the game (maps, monsters, NPCs, items, etc.) as JSON data",
					Action = new Action(GameData.ShowMenu)
				})
			};
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x000AF42C File Offset: 0x000AD62C
		internal static void ShowPanel()
		{
			if (ModMenuMain.currentPanel == null)
			{
				ModMenuMain.currentPanel = new Panel();
			}
			CustomPanelMenu.Show(new CustomPanelMenuConfig
			{
				SetTabAction = new Action<Panel>(ModMenuMain.SetTabModMenu),
				DoFireItemAction = new Action<Panel>(ModMenuMain.DoFireModMenu),
				PaintAction = new Action<Panel, mGraphics>(ModMenuMain.PaintModMenu)
			}, ModMenuMain.currentPanel);
			ModMenuMain.currentPanel.cmdClose.x = GameCanvas.w - ModMenuMain.currentPanel.cmdClose.img.getWidth() - 1;
			ModMenuMain.currentPanel.cmdClose.y = 1;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x000AF4CC File Offset: 0x000AD6CC
		internal static void Paint(mGraphics g)
		{
			if (global::Char.isLoadingMap)
			{
				return;
			}
			if (ChatTextField.gI().isShow)
			{
				return;
			}
			if (GameCanvas.menu.showMenu)
			{
				return;
			}
			if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
			{
				return;
			}
			Command command = ModMenuMain.cmdOpenModMenu;
			if (command != null)
			{
				command.paint(g);
			}
			if (ModMenuMain.cmdOpenModMenu != null && (GameCanvas.isMouseFocus(ModMenuMain.cmdOpenModMenu.x, ModMenuMain.cmdOpenModMenu.y, ModMenuMain.cmdOpenModMenu.w, ModMenuMain.cmdOpenModMenu.h) || (GameCanvas.isMouseFocus((int)((double)ModMenuMain.cmdOpenModMenu.x - (double)ModMenuMain.cmdOpenModMenu.w * 1.5), ModMenuMain.cmdOpenModMenu.y, (int)((double)ModMenuMain.cmdOpenModMenu.w * 2.5), ModMenuMain.cmdOpenModMenu.h) && GameCanvas.isPointerDown)))
			{
				g.drawImage(ItemMap.imageFlare, ModMenuMain.cmdOpenModMenu.x + 4, ModMenuMain.cmdOpenModMenu.y + 15, mGraphics.VCENTER | mGraphics.HCENTER);
			}
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x000AF5E4 File Offset: 0x000AD7E4
		internal static void UpdateTouch()
		{
			if (ModMenuMain.cmdOpenModMenu == null)
			{
				return;
			}
			if (global::Char.isLoadingMap)
			{
				return;
			}
			if (ChatTextField.gI().isShow)
			{
				return;
			}
			if (GameCanvas.menu.showMenu)
			{
				return;
			}
			if (GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
			{
				return;
			}
			if (GameCanvas.isPointerHoldIn((int)((double)ModMenuMain.cmdOpenModMenu.x - (double)ModMenuMain.cmdOpenModMenu.w * 1.5), ModMenuMain.cmdOpenModMenu.y, (int)((double)ModMenuMain.cmdOpenModMenu.w * 2.5), ModMenuMain.cmdOpenModMenu.h) && GameCanvas.isPointerClick)
			{
				GameCanvas.isPointerJustDown = false;
				GameScr.gI().isPointerDowning = false;
				ModMenuMain.cmdOpenModMenu.performAction();
				global::Char.myCharz().currentMovePoint = null;
				GameCanvas.clearAllPointerEvent();
				return;
			}
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000AF6B5 File Offset: 0x000AD8B5
		internal static void SetTabModMenu(Panel panel)
		{
			SetTabPanelTemplates.setTabListTemplate(panel, new ICollection[]
			{
				ModMenuMain.modMenuItemBools,
				ModMenuMain.modMenuItemValues,
				ModMenuMain.modMenuItemFunctions,
				ModMenuMain.modMenuItemDeveloperFunctions
			});
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000AF6E4 File Offset: 0x000AD8E4
		internal static void DoFireModMenu(Panel panel)
		{
			if (panel.currentTabIndex == 0)
			{
				ModMenuMain.DoFireModMenuBools(panel);
			}
			else if (panel.currentTabIndex == 1)
			{
				ModMenuMain.DoFireModMenuValues(panel);
			}
			else if (panel.currentTabIndex == 2)
			{
				ModMenuMain.DoFireModMenuFunctions(panel);
			}
			else if (panel.currentTabIndex == 3)
			{
				ModMenuMain.DoFireModMenuDeveloperFunctions(panel);
			}
			ModMenuMain.NotifySelectDisabledItem(panel);
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x000AF738 File Offset: 0x000AD938
		private static void DoFireModMenuFunctions(Panel panel)
		{
			if (ModMenuMain.modMenuItemFunctions[panel.selected].IsDisabled)
			{
				return;
			}
			panel.hide();
			Action action = ModMenuMain.modMenuItemFunctions[panel.selected].Action;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x000AF770 File Offset: 0x000AD970
		private static void DoFireModMenuDeveloperFunctions(Panel panel)
		{
			if (panel.selected < 0)
			{
				return;
			}
			if (ModMenuMain.modMenuItemDeveloperFunctions[panel.selected].IsDisabled)
			{
				return;
			}
			panel.hide();
			Action action = ModMenuMain.modMenuItemDeveloperFunctions[panel.selected].Action;
			if (action == null)
			{
				return;
			}
			action();
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x000AF7BC File Offset: 0x000AD9BC
		private static void DoFireModMenuBools(Panel panel)
		{
			if (panel.selected < 0)
			{
				return;
			}
			if (ModMenuMain.modMenuItemBools[panel.selected].IsDisabled)
			{
				return;
			}
			ModMenuMain.modMenuItemBools[panel.selected].SwitchSelection();
			GameScr.info1.addInfo(ModMenuMain.modMenuItemBools[panel.selected].Title + ": " + Strings.OnOffStatus(ModMenuMain.modMenuItemBools[panel.selected].Value), 0);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x000AF834 File Offset: 0x000ADA34
		private static void DoFireModMenuValues(Panel panel)
		{
			if (panel.selected < 0)
			{
				return;
			}
			int selected = panel.selected;
			if (ModMenuMain.modMenuItemValues[selected].IsDisabled)
			{
				return;
			}
			if (ModMenuMain.modMenuItemValues[selected].Values != null)
			{
				ModMenuMain.modMenuItemValues[selected].SwitchSelection();
				return;
			}
			ModMenuMain.modMenuItemValues[selected].StartChat(ModMenuMain.currentPanel.chatTField = new ChatTextField());
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x000AF89C File Offset: 0x000ADA9C
		private static void NotifySelectDisabledItem(Panel panel)
		{
			int selected = panel.selected;
			if (selected == -1)
			{
				return;
			}
			if (panel.currentTabIndex == 0)
			{
				if (!ModMenuMain.modMenuItemBools[selected].IsDisabled)
				{
					return;
				}
				GameScr.info1.addInfo(ModMenuMain.modMenuItemBools[selected].DisabledReason, 0);
				return;
			}
			else if (panel.currentTabIndex == 1)
			{
				if (!ModMenuMain.modMenuItemValues[selected].IsDisabled)
				{
					return;
				}
				GameScr.info1.addInfo(ModMenuMain.modMenuItemValues[selected].DisabledReason, 0);
				return;
			}
			else
			{
				if (panel.currentTabIndex != 2)
				{
					if (panel.currentTabIndex == 3)
					{
						if (!ModMenuMain.modMenuItemDeveloperFunctions[selected].IsDisabled)
						{
							return;
						}
						GameScr.info1.addInfo(ModMenuMain.modMenuItemDeveloperFunctions[selected].DisabledReason, 0);
					}
					return;
				}
				if (!ModMenuMain.modMenuItemFunctions[selected].IsDisabled)
				{
					return;
				}
				GameScr.info1.addInfo(ModMenuMain.modMenuItemFunctions[selected].DisabledReason, 0);
				return;
			}
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x000AF974 File Offset: 0x000ADB74
		internal static void PaintModMenu(Panel panel, mGraphics g)
		{
			g.setClip(panel.xScroll, panel.yScroll, panel.wScroll, panel.hScroll);
			g.translate(0, -panel.cmy);
			if (panel.currentTabIndex == 0)
			{
				ModMenuMain.PaintModMenuBools(panel, g);
				return;
			}
			if (panel.currentTabIndex == 1)
			{
				ModMenuMain.PaintModMenuValues(panel, g);
				return;
			}
			if (panel.currentTabIndex == 2)
			{
				ModMenuMain.PaintModMenuFunctions(panel, g);
				return;
			}
			if (panel.currentTabIndex == 3)
			{
				ModMenuMain.PaintModMenuDeveloperFunctions(panel, g);
			}
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x000AF9F0 File Offset: 0x000ADBF0
		private static void PaintModMenuBools(Panel panel, mGraphics g)
		{
			if (ModMenuMain.modMenuItemBools == null || ModMenuMain.modMenuItemBools.Length != panel.currentListLength)
			{
				return;
			}
			int num = Math.Max(panel.cmy / panel.ITEM_HEIGHT, 0);
			bool flag = true;
			string text = string.Empty;
			int num2 = 0;
			int num3 = 0;
			for (int i = num; i < Mathf.Clamp(num + panel.hScroll / panel.ITEM_HEIGHT + 2, 0, panel.currentListLength); i++)
			{
				int xScroll = panel.xScroll;
				int num4 = panel.yScroll + i * panel.ITEM_HEIGHT;
				int wScroll = panel.wScroll;
				int num5 = panel.ITEM_HEIGHT - 1;
				ModMenuItemBoolean modMenuItemBoolean = ModMenuMain.modMenuItemBools[i];
				if (!modMenuItemBoolean.IsDisabled)
				{
					g.setColor((i != panel.selected) ? 15196114 : 16383818);
				}
				else
				{
					g.setColor((i != panel.selected) ? 12038050 : 13686587);
				}
				g.fillRect(xScroll, num4, wScroll, num5);
				mFont.tahoma_7_green2.drawString(g, (i + 1).ToString() + ". " + modMenuItemBoolean.Title, xScroll + 7, num4, 0);
				if (i == panel.selected && mFont.tahoma_7_blue.getWidth(modMenuItemBoolean.Description) > panel.wScroll - 20 && !panel.isClose)
				{
					flag = false;
					text = modMenuItemBoolean.Description;
					num2 = xScroll + 7;
					num3 = num4 + 11;
				}
				else
				{
					mFont.tahoma_7_blue.drawString(g, Utils.TrimUntilFit(modMenuItemBoolean.Description, ModMenuMain.style, panel.wScroll - 5), xScroll + 7, num4 + 11, 0);
				}
				if (modMenuItemBoolean.Value)
				{
					g.setColor(45056);
				}
				else
				{
					g.setColor(14680064);
				}
				g.fillRect(xScroll, num4, 2, num5);
			}
			if (flag)
			{
				TextInfo.reset();
			}
			else
			{
				TextInfo.paint(g, text, num2, num3, panel.wScroll - 5, 15, mFont.tahoma_7_blue);
				g.setClip(panel.xScroll, panel.yScroll, panel.wScroll, panel.hScroll);
				g.translate(0, -panel.cmy);
			}
			panel.paintScrollArrow(g);
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x000AFC18 File Offset: 0x000ADE18
		private static void PaintModMenuValues(Panel panel, mGraphics g)
		{
			if (ModMenuMain.modMenuItemValues == null || ModMenuMain.modMenuItemValues.Length != panel.currentListLength)
			{
				return;
			}
			int num = Math.Max(panel.cmy / panel.ITEM_HEIGHT, 0);
			bool flag = true;
			string text = string.Empty;
			int num2 = 0;
			int num3 = 0;
			double num4 = 0.0;
			for (int i = num; i < Mathf.Clamp(num + panel.hScroll / panel.ITEM_HEIGHT + 2, 0, panel.currentListLength); i++)
			{
				int xScroll = panel.xScroll;
				int num5 = panel.yScroll + i * panel.ITEM_HEIGHT;
				int wScroll = panel.wScroll;
				int num6 = panel.ITEM_HEIGHT - 1;
				ModMenuItemValues modMenuItemValues = ModMenuMain.modMenuItemValues[i];
				if (!modMenuItemValues.IsDisabled)
				{
					g.setColor((i != panel.selected) ? 15196114 : 16383818);
				}
				else
				{
					g.setColor((i != panel.selected) ? 12038050 : 13686587);
				}
				g.fillRect(xScroll, num5, wScroll, num6);
				mFont.tahoma_7_green2.drawString(g, (i + 1).ToString() + ". " + modMenuItemValues.Title, xScroll + 5, num5, 0);
				string text2;
				int num7;
				if (modMenuItemValues.Values != null)
				{
					text2 = modMenuItemValues.getSelectedValue();
					num7 = panel.wScroll - 5;
				}
				else
				{
					text2 = modMenuItemValues.Description;
					num7 = panel.wScroll - 5 - mFont.tahoma_7_blue.getWidth(modMenuItemValues.SelectedValue.ToString());
					mFont.tahoma_7b_red.drawString(g, modMenuItemValues.SelectedValue.ToString(), xScroll + wScroll - 2, num5 + panel.ITEM_HEIGHT - 14, mFont.RIGHT);
				}
				if (i == panel.selected && mFont.tahoma_7_blue.getWidth(text2) > num7 && !panel.isClose)
				{
					flag = false;
					text = text2;
					num4 = modMenuItemValues.SelectedValue;
					num2 = xScroll + 5;
					num3 = num5 + 11;
				}
				else
				{
					mFont.tahoma_7_blue.drawString(g, Utils.TrimUntilFit(text2, ModMenuMain.style, num7), xScroll + 5, num5 + 11, 0);
				}
			}
			if (flag)
			{
				TextInfo.reset();
			}
			else
			{
				TextInfo.paint(g, text, num2, num3, panel.wScroll - 10 - mFont.tahoma_7b_red.getWidth(num4.ToString()), 15, mFont.tahoma_7_blue);
				g.setClip(panel.xScroll, panel.yScroll, panel.wScroll, panel.hScroll);
				g.translate(0, -panel.cmy);
			}
			panel.paintScrollArrow(g);
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x000AFE9C File Offset: 0x000AE09C
		private static void PaintModMenuFunctions(Panel panel, mGraphics g)
		{
			if (ModMenuMain.modMenuItemFunctions == null || ModMenuMain.modMenuItemFunctions.Length != panel.currentListLength)
			{
				return;
			}
			int num = Math.Max(panel.cmy / panel.ITEM_HEIGHT, 0);
			bool flag = true;
			string text = string.Empty;
			int num2 = 0;
			int num3 = 0;
			for (int i = num; i < Mathf.Clamp(num + panel.hScroll / panel.ITEM_HEIGHT + 2, 0, panel.currentListLength); i++)
			{
				int xScroll = panel.xScroll;
				int num4 = panel.yScroll + i * panel.ITEM_HEIGHT;
				int wScroll = panel.wScroll;
				int num5 = panel.ITEM_HEIGHT - 1;
				ModMenuItemFunction modMenuItemFunction = ModMenuMain.modMenuItemFunctions[i];
				if (!modMenuItemFunction.IsDisabled)
				{
					g.setColor((i != panel.selected) ? 15196114 : 16383818);
				}
				else
				{
					g.setColor((i != panel.selected) ? 12038050 : 13686587);
				}
				g.fillRect(xScroll, num4, wScroll, num5);
				mFont.tahoma_7_green2.drawString(g, (i + 1).ToString() + ". " + modMenuItemFunction.Title, xScroll + 5, num4, 0);
				if (i == panel.selected && mFont.tahoma_7_blue.getWidth(modMenuItemFunction.Description) > panel.wScroll - 5 && !panel.isClose)
				{
					flag = false;
					text = modMenuItemFunction.Description;
					num2 = xScroll + 5;
					num3 = num4 + 11;
				}
				else
				{
					mFont.tahoma_7_blue.drawString(g, Utils.TrimUntilFit(modMenuItemFunction.Description, ModMenuMain.style, panel.wScroll - 5), xScroll + 5, num4 + 11, 0);
				}
			}
			if (flag)
			{
				TextInfo.reset();
			}
			else
			{
				TextInfo.paint(g, text, num2, num3, panel.wScroll - 5, 15, mFont.tahoma_7_blue);
				g.setClip(panel.xScroll, panel.yScroll, panel.wScroll, panel.hScroll);
				g.translate(0, -panel.cmy);
			}
			panel.paintScrollArrow(g);
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x000B0094 File Offset: 0x000AE294
		private static void PaintModMenuDeveloperFunctions(Panel panel, mGraphics g)
		{
			if (ModMenuMain.modMenuItemDeveloperFunctions == null || ModMenuMain.modMenuItemDeveloperFunctions.Length != panel.currentListLength)
			{
				return;
			}
			int num = Math.Max(panel.cmy / panel.ITEM_HEIGHT, 0);
			bool flag = true;
			string text = string.Empty;
			int num2 = 0;
			int num3 = 0;
			for (int i = num; i < Mathf.Clamp(num + panel.hScroll / panel.ITEM_HEIGHT + 2, 0, panel.currentListLength); i++)
			{
				int xScroll = panel.xScroll;
				int num4 = panel.yScroll + i * panel.ITEM_HEIGHT;
				int wScroll = panel.wScroll;
				int num5 = panel.ITEM_HEIGHT - 1;
				ModMenuItemFunction modMenuItemFunction = ModMenuMain.modMenuItemDeveloperFunctions[i];
				if (!modMenuItemFunction.IsDisabled)
				{
					g.setColor((i != panel.selected) ? 15196114 : 16383818);
				}
				else
				{
					g.setColor((i != panel.selected) ? 12038050 : 13686587);
				}
				g.fillRect(xScroll, num4, wScroll, num5);
				mFont.tahoma_7_green2.drawString(g, (i + 1).ToString() + ". " + modMenuItemFunction.Title, xScroll + 5, num4, 0);
				if (i == panel.selected && mFont.tahoma_7_blue.getWidth(modMenuItemFunction.Description) > panel.wScroll - 5 && !panel.isClose)
				{
					flag = false;
					text = modMenuItemFunction.Description;
					num2 = xScroll + 5;
					num3 = num4 + 11;
				}
				else
				{
					mFont.tahoma_7_blue.drawString(g, Utils.TrimUntilFit(modMenuItemFunction.Description, ModMenuMain.style, panel.wScroll - 5), xScroll + 5, num4 + 11, 0);
				}
			}
			if (flag)
			{
				TextInfo.reset();
			}
			else
			{
				TextInfo.paint(g, text, num2, num3, panel.wScroll - 5, 15, mFont.tahoma_7_blue);
				g.setClip(panel.xScroll, panel.yScroll, panel.wScroll, panel.hScroll);
				g.translate(0, -panel.cmy);
			}
			panel.paintScrollArrow(g);
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x000B028C File Offset: 0x000AE48C
		internal static void SaveData()
		{
			foreach (ModMenuItemBoolean modMenuItemBoolean in ModMenuMain.modMenuItemBools)
			{
				if (!string.IsNullOrEmpty(modMenuItemBoolean.RMSName))
				{
					Utils.SaveData(modMenuItemBoolean.RMSName, modMenuItemBoolean.Value, true);
				}
			}
			foreach (ModMenuItemValues modMenuItemValues in ModMenuMain.modMenuItemValues)
			{
				if (!string.IsNullOrEmpty(modMenuItemValues.RMSName))
				{
					if (modMenuItemValues.IsFloatingPoint)
					{
						Utils.SaveData(modMenuItemValues.RMSName, modMenuItemValues.SelectedValue, true);
					}
					else
					{
						Utils.SaveData(modMenuItemValues.RMSName, (long)modMenuItemValues.SelectedValue, true);
					}
				}
			}
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x000B032C File Offset: 0x000AE52C
		internal static void LoadData()
		{
			foreach (ModMenuItemBoolean modMenuItemBoolean in ModMenuMain.modMenuItemBools)
			{
				bool flag;
				if (!string.IsNullOrEmpty(modMenuItemBoolean.RMSName) && Utils.TryLoadDataBool(modMenuItemBoolean.RMSName, out flag, true))
				{
					modMenuItemBoolean.Value = flag;
				}
			}
			foreach (ModMenuItemValues modMenuItemValues in ModMenuMain.modMenuItemValues)
			{
				if (!string.IsNullOrEmpty(modMenuItemValues.RMSName))
				{
					double num;
					long num2;
					if (modMenuItemValues.IsFloatingPoint && Utils.TryLoadDataDouble(modMenuItemValues.RMSName, out num, true))
					{
						modMenuItemValues.SelectedValue = num;
					}
					else if (Utils.TryLoadDataLong(modMenuItemValues.RMSName, out num2, true))
					{
						modMenuItemValues.SelectedValue = (double)num2;
					}
				}
			}
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x000B03E0 File Offset: 0x000AE5E0
		internal static ModMenuItem GetModMenuItem(string id)
		{
			return ModMenuMain.modMenuItemBools.Concat<ModMenuItem>(ModMenuMain.modMenuItemValues).Concat<ModMenuItem>(ModMenuMain.modMenuItemValues).Concat<ModMenuItem>(ModMenuMain.modMenuItemFunctions)
				.Concat<ModMenuItem>(ModMenuMain.modMenuItemDeveloperFunctions)
				.FirstOrDefault<ModMenuItem>((ModMenuItem item) => item.ID == id);
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x000B0438 File Offset: 0x000AE638
		internal static T GetModMenuItem<T>(string id) where T : ModMenuItem
		{
			if (typeof(T) == typeof(ModMenuItemBoolean))
			{
				return ModMenuMain.modMenuItemBools.FirstOrDefault<ModMenuItemBoolean>((ModMenuItemBoolean item) => item.ID == id) as T;
			}
			if (typeof(T) == typeof(ModMenuItemValues))
			{
				return ModMenuMain.modMenuItemValues.FirstOrDefault<ModMenuItemValues>((ModMenuItemValues item) => item.ID == id) as T;
			}
			if (typeof(T) == typeof(ModMenuItemFunction))
			{
				return ModMenuMain.modMenuItemFunctions.FirstOrDefault<ModMenuItemFunction>((ModMenuItemFunction item) => item.ID == id) as T;
			}
			if (typeof(T) == typeof(ModMenuItemFunction))
			{
				return ModMenuMain.modMenuItemDeveloperFunctions.FirstOrDefault<ModMenuItemFunction>((ModMenuItemFunction item) => item.ID == id) as T;
			}
			return default(T);
		}

		// Token: 0x040016FB RID: 5883
		internal static ModMenuItemBoolean[] modMenuItemBools;

		// Token: 0x040016FC RID: 5884
		internal static ModMenuItemValues[] modMenuItemValues;

		// Token: 0x040016FD RID: 5885
		internal static ModMenuItemFunction[] modMenuItemFunctions;

		// Token: 0x040016FE RID: 5886
		internal static ModMenuItemFunction[] modMenuItemDeveloperFunctions;

		// Token: 0x040016FF RID: 5887
		internal static Texture2D imgMenu;

		// Token: 0x04001700 RID: 5888
		private static ModMenuMain.ModMenuMainActionListener actionListener = new ModMenuMain.ModMenuMainActionListener();

		// Token: 0x04001701 RID: 5889
		private static sbyte lastLanguage = -1;

		// Token: 0x04001702 RID: 5890
		private static GUIStyle style = new GUIStyle
		{
			font = Resources.Load<Font>(string.Format("FontSys/x{0}/chelthm", mGraphics.zoomLevel))
		};

		// Token: 0x04001703 RID: 5891
		internal static Command cmdOpenModMenu;

		// Token: 0x02000135 RID: 309
		private class ModMenuMainActionListener : IActionListener
		{
			// Token: 0x06000F27 RID: 3879 RVA: 0x000B0586 File Offset: 0x000AE786
			public void perform(int idAction, object p)
			{
				if (idAction == 1)
				{
					ModMenuMain.ShowPanel();
				}
			}
		}
	}
}
