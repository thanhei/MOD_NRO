using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Mod.Graphics;
using Mod.R;
using MonoHook;
using UnityEngine;

namespace Mod
{
	// Token: 0x020000DD RID: 221
	internal static class GameEventHook
	{
		// Token: 0x06000B89 RID: 2953 RVA: 0x0009860C File Offset: 0x0009680C
		private static void InstallAll()
		{
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(MotherCanvas.checkZoomLevel(int, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.MotherCanvas_checkZoomLevel_hook(MotherCanvas, int, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.MotherCanvas_checkZoomLevel_original(MotherCanvas, int, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Image.createImage(string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Image_createImage_hook(string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Image_createImage_original(string)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Rms.GetiPhoneDocumentsPath()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Rms_GetiPhoneDocumentsPath_hook()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Rms_GetiPhoneDocumentsPath_original()).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Rms.saveRMSString(string, string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Rms_saveRMSString_hook(string, string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Rms_saveRMSString_original(string, string)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Rms.saveRMS(string, sbyte[])).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Rms_saveRMS_hook(string, sbyte[])).MethodHandle), null);
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Rms.loadRMS(string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Rms_loadRMS_hook(string)).MethodHandle), null);
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ServerListScreen.saveIP()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ServerListScreen_saveIP_hook()).MethodHandle), null);
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ServerListScreen.loadIP()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ServerListScreen_loadIP_hook()).MethodHandle), null);
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ServerListScreen.getServerList(string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ServerListScreen_getServerList_hook(string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ServerListScreen_getServerList_original(string)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Panel..ctor()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_ctor_hook(Panel)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel__ctor_original(Panel)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameScr.updateKey()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_updateKey_hook(GameScr)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_updateKey_original(GameScr)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ChatTextField.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ChatTextField_paint_hook(ChatTextField, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ChatTextField_paint_original(ChatTextField, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ChatTextField.startChat(int, IChatable, string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ChatTextField_startChat_hook_1(ChatTextField, int, IChatable, string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ChatTextField_startChat_original_1(ChatTextField, int, IChatable, string)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ChatTextField.startChat(IChatable, string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ChatTextField_startChat_hook_2(ChatTextField, IChatable, string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ChatTextField_startChat_original_2(ChatTextField, IChatable, string)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Teleport.update()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Teleport_update_hook(Teleport)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Teleport_update_original(Teleport)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ChatTextField.update()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ChatTextField_update_hook(ChatTextField)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ChatTextField_update_original(ChatTextField)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Rms.clearAll()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Rms_clearAll_hook()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Rms_clearAll_original()).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameScr.update()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_update_hook(GameScr)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_update_original(GameScr)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Service.login(string, string, string, sbyte)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Service_login_hook(Service, string, string, string, sbyte)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Service_login_original(Service, string, string, string, sbyte)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ServerListScreen.switchToMe()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ServerListScreen_switchToMe_hook(ServerListScreen)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ServerListScreen_switchToMe_original(ServerListScreen)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Session_ME.connect(string, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Session_ME_connect_hook(Session_ME, string, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Session_ME_connect_original(Session_ME, string, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ServerListScreen.show2()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ServerListScreen_show2_hook(ServerListScreen)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ServerListScreen_show2_original(ServerListScreen)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameCanvas.keyPressedz(int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameCanvas_keyPressedz_hook(GameCanvas, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameCanvas_keyPressedz_original(GameCanvas, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameCanvas.keyReleasedz(int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameCanvas_keyReleasedz_hook(GameCanvas, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameCanvas_keyReleasedz_original(GameCanvas, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ChatPopup.addChatPopupMultiLine(string, int, Npc)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ChatPopup_addChatPopupMultiLine_hook(string, int, Npc)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ChatPopup_addChatPopupMultiLine_original(string, int, Npc)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ChatPopup.addBigMessage(string, int, Npc)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ChatPopup_addBigMessage_hook(string, int, Npc)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ChatPopup_addBigMessage_original(string, int, Npc)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Controller.loadInfoMap(Message)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Controller_loadInfoMap_hook(Controller, Message)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Controller_loadInfoMap_original(Controller, Message)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameScr.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paint_hook(GameScr, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paint_original(GameScr, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.setSkillPaint(SkillPaint, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_setSkillPaint_hook(global::Char, SkillPaint, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_setSkillPaint_original(global::Char, SkillPaint, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(InfoMe.addInfo(string, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.InfoMe_addInfo_hook(InfoMe, string, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.InfoMe_addInfo_original(InfoMe, string, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Panel.updateKey()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_updateKey_hook(Panel)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_updateKey_original(Panel)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ItemMap.setPoint(int, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ItemMap_setPoint_hook(ItemMap, int, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ItemMap_setPoint_original(ItemMap, int, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Menu.startAt(MyVector, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Menu_startAt_hook(Menu, MyVector, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Menu_startAt_original(Menu, MyVector, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.addInfo(string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_addInfo_hook(global::Char, string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_addInfo_original(global::Char, string)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameCanvas.paintBGGameScr(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameCanvas_paintBGGameScr_hook(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameCanvas_paintBGGameScr_original(mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Mob.startDie()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Mob_startDie_hook(Mob)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Mob_startDie_original(Mob)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Mob.update()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Mob_update_hook(Mob)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Mob_update_original(Mob)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameScr.chatVip(string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_chatVip_hook(GameScr, string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_chatVip_original(GameScr, string)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Panel.updateScroolMouse(int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_updateScroolMouse_hook(Panel, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_updateScroolMouse_original(Panel, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Panel.hide()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_hide_hook(Panel)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_hide_original(Panel)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Panel.hideNow()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_hideNow_hook(Panel)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_hideNow_original(Panel)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameScr.paintTouchControl(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paintTouchControl_hook(GameScr, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paintTouchControl_original(GameScr, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameScr.paintGamePad(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paintGamePad_hook(GameScr, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paintGamePad_original(GameScr, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Panel.doFireOption()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_doFireOption_hook(Panel)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_doFireOption_original(Panel)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(SoundMn.getStrOption()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.SoundMn_getStrOption_hook(SoundMn)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.SoundMn_getStrOption_original(SoundMn)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameScr.setSkillBarPosition()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_setSkillBarPosition_hook()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_setSkillBarPosition_original()).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GamePad..ctor()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GamePad__ctor_hook(GamePad)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GamePad__ctor_original(GamePad)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GamePad.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GamePad_paint_hook(GamePad, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GamePad_paint_original(GamePad, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameScr.paintSelectedSkill(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paintSelectedSkill_hook(GameScr, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paintSelectedSkill_original(GameScr, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Panel.paintToolInfo(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_paintToolInfo_hook(Panel, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_paintToolInfo_original(Panel, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(mResources.loadLanguague(sbyte)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.mResources_loadLanguague_hook(sbyte)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.mResources_loadLanguague_original(sbyte)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(LoginScr.switchToMe()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.LoginScr_switchToMe_hook(LoginScr)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.LoginScr_switchToMe_original(LoginScr)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Skill.paint(int, int, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Skill_paint_hook(Skill, int, int, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Skill_paint_original(Skill, int, int, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Service.chat(string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Service_chat_hook(Service, string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Service_chat_original(Service, string)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Service.gotoPlayer(int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Service_gotoPlayer_hook(Service, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Service_gotoPlayer_original(Service, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Panel.updateKeyInTabBar()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_updateKeyInTabBar_hook(Panel)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_updateKeyInTabBar_original(Panel)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Panel.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_paint_hook(Panel, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_paint_original(Panel, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Panel.update()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_update_hook(Panel)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_update_original(Panel)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameCanvas.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameCanvas_paint_hook(GameCanvas, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameCanvas_paint_original(GameCanvas, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.update()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_update_hook(global::Char)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_update_original(global::Char)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.removeHoleEff()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_removeHoleEff_hook(global::Char)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_removeHoleEff_original(global::Char)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.setHoldChar(global::Char)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_setHoldChar_hook(global::Char, global::Char)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_setHoldChar_original(global::Char, global::Char)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.setHoldMob(Mob)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_setHoldMob_hook(global::Char, Mob)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_setHoldMob_original(global::Char, Mob)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameScr.paintImageBar(mGraphics, bool, global::Char)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paintImageBar_hook(GameScr, mGraphics, bool, global::Char)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paintImageBar_original(GameScr, mGraphics, bool, global::Char)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(BackgroudEffect..ctor(int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroundEffect__ctor_hook(BackgroudEffect, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroundEffect__ctor_original(BackgroudEffect, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(BackgroudEffect.initCloud()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroudEffect_initCloud_hook()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroudEffect_initCloud_original()).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(BackgroudEffect.updateCloud2()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroudEffect_updateCloud2_hook()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroudEffect_updateCloud2_original()).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(BackgroudEffect.updateFog()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroudEffect_updateFog_hook()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroudEffect_updateFog_original()).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(BackgroudEffect.paintCloud2(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroudEffect_paintCloud2_hook(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroudEffect_paintCloud2_original(mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(BackgroudEffect.paintFog(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroudEffect_paintFog_hook(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroudEffect_paintFog_original(mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(BackgroudEffect.addEffect(int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroudEffect_addEffect_hook(int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BackgroudEffect_addEffect_original(int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(BgItem.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BgItem_paint_hook(BgItem, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.BgItem_paint_original(BgItem, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.updateSuperEff()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_updateSuperEff_hook(global::Char)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_updateSuperEff_original(global::Char)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paint_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paint_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintMount1(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintMount1_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintMount1_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintMount2(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintMount2_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintMount2_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintSuperEffFront(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintSuperEffFront_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintSuperEffFront_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintSuperEffBehind(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintSuperEffBehind_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintSuperEffBehind_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintAuraFront(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintAuraFront_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintAuraFront_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintAuraBehind(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintAuraBehind_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintAuraBehind_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintEffFront(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintEffFront_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintEffFront_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintEffBehind(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintEffBehind_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintEffBehind_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintEff_Lvup_front(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintEff_Lvup_front_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintEff_Lvup_front_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintEff_Lvup_behind(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintEff_Lvup_behind_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintEff_Lvup_behind_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintEff_Pet(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintEff_Pet_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintEff_Pet_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintEffect(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintEffect_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintEffect_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paint_map_line(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paint_map_line_hook(global::Char, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paint_map_line_original(global::Char, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(global::Char.paintCharBody(mGraphics, int, int, int, int, bool)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintCharBody_hook(global::Char, mGraphics, int, int, int, int, bool)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Char_paintCharBody_original(global::Char, mGraphics, int, int, int, int, bool)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Effect.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Effect_paint_hook(Effect, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Effect_paint_original(Effect, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameScr.paintEffect(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paintEffect_hook(GameScr, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paintEffect_original(GameScr, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameScr.paintBgItem(mGraphics, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paintBgItem_hook(GameScr, mGraphics, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_paintBgItem_original(GameScr, mGraphics, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(mGraphics.drawImage(Image, int, int, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.mGraphics_drawImage_hook(mGraphics, Image, int, int, int)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.mGraphics_drawImage_original(mGraphics, Image, int, int, int)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(InfoMe.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.InfoMe_paint_hook(InfoMe, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.InfoMe_paint_original(InfoMe, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ItemMap.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ItemMap_paint_hook(ItemMap, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ItemMap_paint_original(ItemMap, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(MagicTree.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.MagicTree_paint_hook(MagicTree, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.MagicTree_paint_original(MagicTree, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Mob.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Mob_paint_hook(Mob, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Mob_paint_original(Mob, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(TileMap.paintTilemap(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.TileMap_paintTilemap_hook(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.TileMap_paintTilemap_original(mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(TileMap.paintOutTilemap(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.TileMap_paintOutTilemap_hook(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.TileMap_paintOutTilemap_original(mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Npc.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Npc_paint_hook(Npc, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Npc_paint_original(Npc, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ServerEffect.paint(mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ServerEffect_paint_hook(ServerEffect, mGraphics)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ServerEffect_paint_original(ServerEffect, mGraphics)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(ServerListScreen.initCommand()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ServerListScreen_initCommand_hook(ServerListScreen)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.ServerListScreen_initCommand_original(ServerListScreen)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Panel.doFireTool()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_doFireTool_hook(Panel)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_doFireTool_original(Panel)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(SoundMn.getSoundOption()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.SoundMn_getSoundOption_hook(SoundMn)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.SoundMn_getSoundOption_original(SoundMn)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Panel.doFirePet()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_doFirePet_hook(Panel)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Panel_doFirePet_original(Panel)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameScr.openUIZone(Message)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_openUIZone_hook(GameScr, Message)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameScr_openUIZone_original(GameScr, Message)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(GameCanvas.startOKDlg(string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameCanvas_startOKDlg_hook(string)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.GameCanvas_startOKDlg_original(string)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Service.requestChangeMap()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Service_requestChangeMap_hook(Service)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Service_requestChangeMap_original(Service)).MethodHandle));
			GameEventHook.TryInstallHook(MethodBase.GetMethodFromHandle(methodof(Service.getMapOffline()).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Service_getMapOffline_hook(Service)).MethodHandle), MethodBase.GetMethodFromHandle(methodof(GameEventHook.Service_getMapOffline_original(Service)).MethodHandle));
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000994B9 File Offset: 0x000976B9
		private static void Service_getMapOffline_hook(Service _this)
		{
			if (!GameEvents.OnGetMapOffline())
			{
				GameEventHook.Service_getMapOffline_original(_this);
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Service_getMapOffline_original(Service _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000994D4 File Offset: 0x000976D4
		private static void Service_requestChangeMap_hook(Service _this)
		{
			if (!GameEvents.OnRequestChangeMap())
			{
				GameEventHook.Service_requestChangeMap_original(_this);
			}
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Service_requestChangeMap_original(Service _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000994E3 File Offset: 0x000976E3
		private static void GameCanvas_startOKDlg_hook(string info)
		{
			if (!GameEvents.OnStartOKDlg(info))
			{
				GameEventHook.GameCanvas_startOKDlg_original(info);
			}
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameCanvas_startOKDlg_original(string info)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000994F3 File Offset: 0x000976F3
		private static void GameScr_openUIZone_hook(GameScr _this, Message message)
		{
			if (!GameEvents.OnOpenUIZone(_this, message))
			{
				GameEventHook.GameScr_openUIZone_original(_this, message);
			}
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameScr_openUIZone_original(GameScr _this, Message message)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x00099508 File Offset: 0x00097708
		private static void Panel_doFirePet_hook(Panel _this)
		{
			GameEventHook.Panel_doFirePet_original(_this);
			if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
			{
				GameCanvas.panel2 = new Panel();
				GameCanvas.panel2.tabName[7] = new string[][] { new string[] { string.Empty } };
				GameCanvas.panel2.setTypeBodyOnly();
				GameCanvas.panel2.show();
				GameCanvas.panel.setTypePetMain();
				GameCanvas.panel.show();
				return;
			}
			GameCanvas.panel.tabName[21] = mResources.petMainTab;
			GameCanvas.panel.setTypePetMain();
			GameCanvas.panel.show();
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Panel_doFirePet_original(Panel _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000995A5 File Offset: 0x000977A5
		private static void SoundMn_getSoundOption_hook(SoundMn _this)
		{
			if (!GameEvents.OnGetSoundOption())
			{
				GameEventHook.SoundMn_getSoundOption_original(_this);
			}
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void SoundMn_getSoundOption_original(SoundMn _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000995B4 File Offset: 0x000977B4
		private static void Panel_doFireTool_hook(Panel _this)
		{
			if (!GameEvents.OnPanelFireTool(_this))
			{
				GameEventHook.Panel_doFireTool_original(_this);
			}
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Panel_doFireTool_original(Panel _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000995C4 File Offset: 0x000977C4
		private static void ServerListScreen_initCommand_hook(ServerListScreen _this)
		{
			if (!GameEvents.OnServerListScreenInitCommand(_this))
			{
				GameEventHook.ServerListScreen_initCommand_original(_this);
			}
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ServerListScreen_initCommand_original(ServerListScreen _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000995D4 File Offset: 0x000977D4
		private static void ServerEffect_paint_hook(ServerEffect _this, mGraphics g)
		{
			if (!GraphicsReducer.OnServerEffectPaint())
			{
				GameEventHook.ServerEffect_paint_original(_this, g);
			}
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ServerEffect_paint_original(ServerEffect _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000995E4 File Offset: 0x000977E4
		private static void Npc_paint_hook(Npc _this, mGraphics g)
		{
			if (!GraphicsReducer.OnNpcPaint(_this, g))
			{
				GameEventHook.Npc_paint_original(_this, g);
			}
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Npc_paint_original(Npc _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000995F6 File Offset: 0x000977F6
		private static void TileMap_paintOutTilemap_hook(mGraphics g)
		{
			if (!GraphicsReducer.OnTileMapPaintOutTile())
			{
				GameEventHook.TileMap_paintOutTilemap_original(g);
			}
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void TileMap_paintOutTilemap_original(mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x00099605 File Offset: 0x00097805
		private static void TileMap_paintTilemap_hook(mGraphics g)
		{
			if (!GraphicsReducer.OnTileMapPaintTile(g))
			{
				GameEventHook.TileMap_paintTilemap_original(g);
			}
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void TileMap_paintTilemap_original(mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x00099615 File Offset: 0x00097815
		private static void Mob_paint_hook(Mob _this, mGraphics g)
		{
			if (!GraphicsReducer.OnMobPaint(_this, g))
			{
				GameEventHook.Mob_paint_original(_this, g);
			}
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Mob_paint_original(Mob _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x00099627 File Offset: 0x00097827
		private static void MagicTree_paint_hook(MagicTree _this, mGraphics g)
		{
			if (!GraphicsReducer.OnMagicTreePaint(_this, g))
			{
				GameEventHook.MagicTree_paint_original(_this, g);
			}
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void MagicTree_paint_original(MagicTree _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x00099639 File Offset: 0x00097839
		private static void ItemMap_paint_hook(ItemMap _this, mGraphics g)
		{
			if (!GraphicsReducer.OnItemMapPaint(_this, g))
			{
				GameEventHook.ItemMap_paint_original(_this, g);
			}
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ItemMap_paint_original(ItemMap _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x0009964B File Offset: 0x0009784B
		private static void InfoMe_paint_hook(InfoMe _this, mGraphics g)
		{
			if (!GraphicsReducer.OnInfoMePaint(_this, g))
			{
				GameEventHook.InfoMe_paint_original(_this, g);
			}
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void InfoMe_paint_original(InfoMe _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x0009965D File Offset: 0x0009785D
		private static void mGraphics_drawImage_hook(mGraphics g, Image image, int x, int y, int anchor)
		{
			if (!GameEvents.OnMGraphicsDrawImage(image, x, y, anchor))
			{
				GameEventHook.mGraphics_drawImage_original(g, image, x, y, anchor);
				GameEvents.AfterMGraphicsDrawImage(image, x, y, anchor);
			}
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void mGraphics_drawImage_original(mGraphics g, Image image, int x, int y, int anchor)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x00099680 File Offset: 0x00097880
		private static void GameScr_paintBgItem_hook(GameScr _this, mGraphics g, int layer)
		{
			if (!GraphicsReducer.OnGameScrPaintBgItem())
			{
				GameEventHook.GameScr_paintBgItem_original(_this, g, layer);
			}
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameScr_paintBgItem_original(GameScr _this, mGraphics g, int layer)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x00099691 File Offset: 0x00097891
		private static void GameScr_paintEffect_hook(GameScr _this, mGraphics g)
		{
			if (!GraphicsReducer.OnGameScrPaintEffect())
			{
				GameEventHook.GameScr_paintEffect_original(_this, g);
			}
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameScr_paintEffect_original(GameScr _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000996A1 File Offset: 0x000978A1
		private static void Effect_paint_hook(Effect _this, mGraphics g)
		{
			if (!GraphicsReducer.OnEffectPaint())
			{
				GameEventHook.Effect_paint_original(_this, g);
			}
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Effect_paint_original(Effect _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000996B1 File Offset: 0x000978B1
		private static void Char_paintCharBody_hook(global::Char _this, mGraphics g, int cx, int cy, int cdir, int cf, bool isPaintBag)
		{
			if (!GraphicsReducer.OnCharPaintCharBody(_this, g, cx, cy, cdir, isPaintBag))
			{
				GameEventHook.Char_paintCharBody_original(_this, g, cx, cy, cdir, cf, isPaintBag);
			}
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintCharBody_original(global::Char _this, mGraphics g, int cx, int cy, int cdir, int cf, bool isPaintBag)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x000996D1 File Offset: 0x000978D1
		private static void Char_paint_map_line_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintMapLine())
			{
				GameEventHook.Char_paint_map_line_original(_this, g);
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paint_map_line_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000996E1 File Offset: 0x000978E1
		private static void Char_paintEffect_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintEffect())
			{
				GameEventHook.Char_paintEffect_original(_this, g);
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintEffect_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000996F1 File Offset: 0x000978F1
		private static void Char_paintEff_Pet_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintEff_Pet())
			{
				GameEventHook.Char_paintEff_Pet_original(_this, g);
			}
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintEff_Pet_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x00099701 File Offset: 0x00097901
		private static void Char_paintEff_Lvup_front_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintEff_LvUp_Front())
			{
				GameEventHook.Char_paintEff_Lvup_front_original(_this, g);
			}
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintEff_Lvup_front_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x00099711 File Offset: 0x00097911
		private static void Char_paintEff_Lvup_behind_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintEff_LvUp_Behind())
			{
				GameEventHook.Char_paintEff_Lvup_behind_original(_this, g);
			}
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintEff_Lvup_behind_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x00099721 File Offset: 0x00097921
		private static void Char_paintEffFront_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintEffFront())
			{
				GameEventHook.Char_paintEffFront_original(_this, g);
			}
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintEffFront_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x00099731 File Offset: 0x00097931
		private static void Char_paintEffBehind_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintEffBehind())
			{
				GameEventHook.Char_paintEffBehind_original(_this, g);
			}
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintEffBehind_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x00099741 File Offset: 0x00097941
		private static void Char_paintAuraFront_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintAuraFront())
			{
				GameEventHook.Char_paintAuraFront_original(_this, g);
			}
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintAuraFront_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x00099751 File Offset: 0x00097951
		private static void Char_paintAuraBehind_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintAuraBehind())
			{
				GameEventHook.Char_paintAuraBehind_original(_this, g);
			}
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintAuraBehind_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x00099761 File Offset: 0x00097961
		private static void Char_paintSuperEffFront_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintSuperEffFront())
			{
				GameEventHook.Char_paintSuperEffFront_original(_this, g);
			}
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintSuperEffFront_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x00099771 File Offset: 0x00097971
		private static void Char_paintSuperEffBehind_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintSuperEffBehind())
			{
				GameEventHook.Char_paintSuperEffBehind_original(_this, g);
			}
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintSuperEffBehind_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x00099781 File Offset: 0x00097981
		private static void Char_paintMount2_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintMount2())
			{
				GameEventHook.Char_paintMount2_original(_this, g);
			}
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintMount2_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x00099791 File Offset: 0x00097991
		private static void Char_paintMount1_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaintMount1(_this, g))
			{
				GameEventHook.Char_paintMount1_original(_this, g);
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paintMount1_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x000997A3 File Offset: 0x000979A3
		private static void Char_paint_hook(global::Char _this, mGraphics g)
		{
			if (!GraphicsReducer.OnCharPaint())
			{
				GameEventHook.Char_paint_original(_this, g);
			}
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_paint_original(global::Char _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000997B3 File Offset: 0x000979B3
		private static void Char_updateSuperEff_hook(global::Char _this)
		{
			if (!GraphicsReducer.OnCharUpdateSuperEff())
			{
				GameEventHook.Char_updateSuperEff_original(_this);
			}
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_updateSuperEff_original(global::Char _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000997C2 File Offset: 0x000979C2
		private static void BgItem_paint_hook(BgItem _this, mGraphics g)
		{
			if (!GraphicsReducer.OnBgItemPaint())
			{
				GameEventHook.BgItem_paint_original(_this, g);
			}
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void BgItem_paint_original(BgItem _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x000997D2 File Offset: 0x000979D2
		private static void BackgroudEffect_addEffect_hook(int id)
		{
			if (!GraphicsReducer.OnBackgroundEffectAddEffect())
			{
				GameEventHook.BackgroudEffect_addEffect_original(id);
			}
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void BackgroudEffect_addEffect_original(int id)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000997E1 File Offset: 0x000979E1
		private static void BackgroudEffect_paintFog_hook(mGraphics g)
		{
			if (!GraphicsReducer.OnBackgroundEffectPaintFog())
			{
				GameEventHook.BackgroudEffect_paintFog_original(g);
			}
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void BackgroudEffect_paintFog_original(mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000997F0 File Offset: 0x000979F0
		private static void BackgroudEffect_paintCloud2_hook(mGraphics g)
		{
			if (!GraphicsReducer.OnBackgroundEffectPaintCloud2())
			{
				GameEventHook.BackgroudEffect_paintCloud2_original(g);
			}
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void BackgroudEffect_paintCloud2_original(mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000997FF File Offset: 0x000979FF
		private static void BackgroudEffect_updateFog_hook()
		{
			if (!GraphicsReducer.OnBackgroundEffectUpdateFog())
			{
				GameEventHook.BackgroudEffect_updateFog_original();
			}
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void BackgroudEffect_updateFog_original()
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0009980D File Offset: 0x00097A0D
		private static void BackgroudEffect_updateCloud2_hook()
		{
			if (!GraphicsReducer.OnBackgroundEffectUpdateCloud2())
			{
				GameEventHook.BackgroudEffect_updateCloud2_original();
			}
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void BackgroudEffect_updateCloud2_original()
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x0009981B File Offset: 0x00097A1B
		private static void BackgroudEffect_initCloud_hook()
		{
			if (!GraphicsReducer.OnBackgroundEffectInitCloud())
			{
				GameEventHook.BackgroudEffect_initCloud_original();
			}
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void BackgroudEffect_initCloud_original()
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x00099829 File Offset: 0x00097A29
		private static void Service_chat_hook(Service _this, string text)
		{
			if (!GameEvents.OnSendChat(text))
			{
				GameEventHook.Service_chat_original(_this, text);
			}
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Service_chat_original(Service _this, string text)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0009983A File Offset: 0x00097A3A
		private static void Rms_saveRMSString_hook(string filename, string data)
		{
			GameEvents.OnSaveRMSString(ref filename, ref data);
			GameEventHook.Rms_saveRMSString_original(filename, data);
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Rms_saveRMSString_original(string filename, string data)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0009984C File Offset: 0x00097A4C
		private static sbyte[] Rms_loadRMS_hook(string filename)
		{
			return Rms.__loadRMS(filename);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x00099854 File Offset: 0x00097A54
		private static void Rms_saveRMS_hook(string filename, sbyte[] data)
		{
			Rms.__saveRMS(filename, data);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x00099860 File Offset: 0x00097A60
		private static void GameScr_updateKey_hook(GameScr _this)
		{
			if (!Controller.isStopReadMessage && !global::Char.myCharz().isTeleport && !global::Char.myCharz().isPaintNewSkill && !InfoDlg.isLock)
			{
				if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu && !_this.isNotPaintTouchControl() && GameEvents.OnUpdateTouchGameScr(_this))
				{
					return;
				}
				if ((!ChatTextField.gI().isShow || GameCanvas.keyAsciiPress == 0) && !_this.isLockKey && !GameCanvas.menu.showMenu && !_this.isOpenUI() && !global::Char.isLockKey && global::Char.myCharz().skillPaint == null && GameCanvas.keyAsciiPress != 0 && _this.mobCapcha == null && TField.isQwerty)
				{
					GameEvents.OnGameScrPressHotkeys();
					if (!GameCanvas.keyPressed[1] && !GameCanvas.keyPressed[2] && !GameCanvas.keyPressed[3] && !GameCanvas.keyPressed[4] && !GameCanvas.keyPressed[5] && !GameCanvas.keyPressed[6] && !GameCanvas.keyPressed[7] && !GameCanvas.keyPressed[8] && !GameCanvas.keyPressed[9] && !GameCanvas.keyPressed[0] && GameCanvas.keyAsciiPress != 114 && GameCanvas.keyAsciiPress != 47)
					{
						GameEvents.OnGameScrPressHotkeysUnassigned();
					}
				}
			}
			GameEventHook.GameScr_updateKey_original(_this);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameScr_updateKey_original(GameScr _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000999B9 File Offset: 0x00097BB9
		private static void ChatTextField_paint_hook(ChatTextField _this, mGraphics g)
		{
			GameEvents.OnPaintChatTextField(_this, g);
			GameEventHook.ChatTextField_paint_original(_this, g);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ChatTextField_paint_original(ChatTextField _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000999C9 File Offset: 0x00097BC9
		private static void ChatTextField_startChat_hook_1(ChatTextField _this, int firstCharacter, IChatable parentScreen, string to)
		{
			if (!GameEvents.OnStartChatTextField(_this, parentScreen))
			{
				GameEventHook.ChatTextField_startChat_original_1(_this, firstCharacter, parentScreen, to);
			}
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ChatTextField_startChat_original_1(ChatTextField _this, int firstCharacter, IChatable parentScreen, string to)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000999DD File Offset: 0x00097BDD
		private static void ChatTextField_startChat_hook_2(ChatTextField _this, IChatable parentScreen, string to)
		{
			if (!GameEvents.OnStartChatTextField(_this, parentScreen))
			{
				GameEventHook.ChatTextField_startChat_original_2(_this, parentScreen, to);
			}
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ChatTextField_startChat_original_2(ChatTextField _this, IChatable parentScreen, string to)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x000999F0 File Offset: 0x00097BF0
		private static string Rms_GetiPhoneDocumentsPath_hook()
		{
			string text;
			if (GameEvents.OnGetRMSPath(out text))
			{
				return text;
			}
			return GameEventHook.Rms_GetiPhoneDocumentsPath_original();
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x00099A0D File Offset: 0x00097C0D
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static string Rms_GetiPhoneDocumentsPath_original()
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
			return null;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x00099A1A File Offset: 0x00097C1A
		private static void Teleport_update_hook(Teleport _this)
		{
			GameEvents.OnTeleportUpdate(_this);
			GameEventHook.Teleport_update_original(_this);
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Teleport_update_original(Teleport _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x00099A29 File Offset: 0x00097C29
		private static void ChatTextField_update_hook(ChatTextField _this)
		{
			if (!_this.isShow)
			{
				GameEvents.OnUpdateChatTextField(_this);
			}
			GameEventHook.ChatTextField_update_original(_this);
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ChatTextField_update_original(ChatTextField _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x00099A3F File Offset: 0x00097C3F
		private static void Rms_clearAll_hook()
		{
			if (!GameEvents.OnClearAllRMS())
			{
				GameEventHook.Rms_clearAll_original();
			}
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Rms_clearAll_original()
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x00099A4D File Offset: 0x00097C4D
		private static void GameScr_update_hook(GameScr _this)
		{
			GameEvents.OnUpdateGameScr();
			GameEventHook.GameScr_update_original(_this);
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameScr_update_original(GameScr _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x00099A5A File Offset: 0x00097C5A
		private static void Service_login_hook(Service _this, string username, string pass, string version, sbyte type)
		{
			GameEvents.OnLogin(ref username, ref pass, ref type);
			GameEventHook.Service_login_original(_this, username, pass, version, type);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Service_login_original(Service _this, string username, string pass, string version, sbyte type)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x00099A72 File Offset: 0x00097C72
		private static void ServerListScreen_switchToMe_hook(ServerListScreen _this)
		{
			GameEventHook.ServerListScreen_switchToMe_original(_this);
			_this.cmd[1 + _this.nCmdPlay].caption = Strings.accounts;
			GameEvents.OnServerListScreenLoaded(_this);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ServerListScreen_switchToMe_original(ServerListScreen _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x00099A99 File Offset: 0x00097C99
		private static void Session_ME_connect_hook(Session_ME _this, string host, int port)
		{
			GameEvents.OnSessionConnecting(ref host, ref port);
			GameEventHook.Session_ME_connect_original(_this, host, port);
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Session_ME_connect_original(Session_ME _this, string host, int port)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x00099AAC File Offset: 0x00097CAC
		private static void ServerListScreen_show2_hook(ServerListScreen _this)
		{
			GameEventHook.ServerListScreen_show2_original(_this);
			GameEvents.OnScreenDownloadDataShow();
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ServerListScreen_show2_original(ServerListScreen _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x00099AB9 File Offset: 0x00097CB9
		private static void MotherCanvas_checkZoomLevel_hook(MotherCanvas _this, int w, int h)
		{
			if (!GameEvents.OnCheckZoomLevel(w, h))
			{
				GameEventHook.MotherCanvas_checkZoomLevel_original(_this, w, h);
			}
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void MotherCanvas_checkZoomLevel_original(MotherCanvas _this, int w, int h)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x00099ACC File Offset: 0x00097CCC
		private static void GameCanvas_keyPressedz_hook(GameCanvas _this, int keyCode)
		{
			if (!GameEvents.OnKeyPressed(keyCode, false))
			{
				GameEventHook.GameCanvas_keyPressedz_original(_this, keyCode);
			}
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameCanvas_keyPressedz_original(GameCanvas _this, int keyCode)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x00099ADE File Offset: 0x00097CDE
		private static void GameCanvas_keyReleasedz_hook(GameCanvas _this, int keyCode)
		{
			if (!GameEvents.OnKeyReleased(keyCode, false))
			{
				GameEventHook.GameCanvas_keyReleasedz_original(_this, keyCode);
			}
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameCanvas_keyReleasedz_original(GameCanvas _this, int keyCode)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x00099AF0 File Offset: 0x00097CF0
		private static void ChatPopup_addChatPopupMultiLine_hook(string chat, int howLong, Npc c)
		{
			if (!GameEvents.OnChatPopupMultiLine(chat))
			{
				GameEventHook.ChatPopup_addChatPopupMultiLine_original(chat, howLong, c);
			}
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ChatPopup_addChatPopupMultiLine_original(string chat, int howLong, Npc c)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x00099B02 File Offset: 0x00097D02
		private static void ChatPopup_addBigMessage_hook(string chat, int howLong, Npc c)
		{
			if (!GameEvents.OnAddBigMessage(chat, c))
			{
				GameEventHook.ChatPopup_addChatPopupMultiLine_original(chat, howLong, c);
			}
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ChatPopup_addBigMessage_original(string chat, int howLong, Npc c)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x00099B15 File Offset: 0x00097D15
		private static void Controller_loadInfoMap_hook(Controller _this, Message msg)
		{
			GameEventHook.Controller_loadInfoMap_original(_this, msg);
			GameEvents.OnInfoMapLoaded();
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Controller_loadInfoMap_original(Controller _this, Message msg)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x00099B23 File Offset: 0x00097D23
		private static void GameScr_paint_hook(GameScr _this, mGraphics g)
		{
			GameEventHook.GameScr_paint_original(_this, g);
			GameEvents.OnPaintGameScr(g);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameScr_paint_original(GameScr _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x00099B32 File Offset: 0x00097D32
		private static void Char_setSkillPaint_hook(global::Char _this, SkillPaint skillPaint, int sType)
		{
			if (!GameEvents.OnUseSkill(_this))
			{
				GameEventHook.Char_setSkillPaint_original(_this, skillPaint, sType);
			}
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_setSkillPaint_original(global::Char _this, SkillPaint skillPaint, int sType)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00099B44 File Offset: 0x00097D44
		private static void InfoMe_addInfo_hook(InfoMe _this, string s, int Type)
		{
			GameEventHook.InfoMe_addInfo_original(_this, s, Type);
			GameEvents.OnAddInfoMe(s);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void InfoMe_addInfo_original(InfoMe _this, string s, int Type)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x00099B54 File Offset: 0x00097D54
		private static void Panel_updateKey_hook(Panel _this)
		{
			if ((_this.chatTField == null || !_this.chatTField.isShow) && GameCanvas.panel.isDoneCombine && !InfoDlg.isShow)
			{
				GameEvents.OnUpdateKeyPanel(_this);
			}
			if ((_this.tabIcon == null || !_this.tabIcon.isShow) && !_this.isClose && _this.isShow && !_this.cmdClose.isPointerPressInside())
			{
				GameEvents.OnUpdateTouchPanel(_this);
			}
			GameEventHook.Panel_updateKey_original(_this);
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Panel_updateKey_original(Panel _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00099BCD File Offset: 0x00097DCD
		private static void ItemMap_setPoint_hook(ItemMap _this, int xEnd, int yEnd)
		{
			GameEvents.OnSetPointItemMap(xEnd, yEnd);
			GameEventHook.ItemMap_setPoint_original(_this, xEnd, yEnd);
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ItemMap_setPoint_original(ItemMap _this, int xEnd, int yEnd)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00099BDE File Offset: 0x00097DDE
		private static void Menu_startAt_hook(Menu _this, MyVector menuItems, int pos)
		{
			if (!GameEvents.OnMenuStartAt(menuItems))
			{
				GameEventHook.Menu_startAt_original(_this, menuItems, pos);
			}
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Menu_startAt_original(Menu _this, MyVector menuItems, int pos)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00099BF0 File Offset: 0x00097DF0
		private static void Char_addInfo_hook(global::Char _this, string info)
		{
			GameEvents.OnAddInfoChar(_this, info);
			GameEventHook.Char_addInfo_original(_this, info);
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_addInfo_original(global::Char _this, string info)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00099C00 File Offset: 0x00097E00
		private static void GameCanvas_paintBGGameScr_hook(mGraphics g)
		{
			if (!GameEvents.OnPaintBgGameScr(g))
			{
				GameEventHook.GameCanvas_paintBGGameScr_original(g);
			}
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameCanvas_paintBGGameScr_original(mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x00099C10 File Offset: 0x00097E10
		private static void Mob_startDie_hook(Mob _this)
		{
			GameEvents.OnMobStartDie(_this);
			GameEventHook.Mob_startDie_original(_this);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Mob_startDie_original(Mob _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00099C1E File Offset: 0x00097E1E
		private static void Mob_update_hook(Mob _this)
		{
			GameEvents.OnUpdateMob(_this);
			GameEventHook.Mob_update_original(_this);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Mob_update_original(Mob _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x00099C2C File Offset: 0x00097E2C
		private static Image Image_createImage_hook(string filename)
		{
			Image image;
			if (GameEvents.OnCreateImage(filename, out image))
			{
				return image;
			}
			return GameEventHook.Image_createImage_original(filename);
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x00099A0D File Offset: 0x00097C0D
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static Image Image_createImage_original(string filename)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
			return null;
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x00099C4B File Offset: 0x00097E4B
		private static void GameScr_chatVip_hook(GameScr _this, string chatVip)
		{
			GameEvents.OnChatVip(chatVip);
			GameEventHook.GameScr_chatVip_original(_this, chatVip);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameScr_chatVip_original(GameScr _this, string chatVip)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00099C5A File Offset: 0x00097E5A
		private static void Panel_updateScroolMouse_hook(Panel _this, int a)
		{
			GameEvents.OnUpdateScrollMousePanel(_this, ref a);
			GameEventHook.Panel_updateScroolMouse_original(_this, a);
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Panel_updateScroolMouse_original(Panel _this, int a)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00099C6C File Offset: 0x00097E6C
		private static void Panel_hide_hook(Panel _this)
		{
			if (_this.timeShow <= 0)
			{
				GameEvents.OnPanelHide(_this);
			}
			GameEventHook.Panel_hide_original(_this);
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Panel_hide_original(Panel _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x00099C83 File Offset: 0x00097E83
		private static void Panel_hideNow_hook(Panel _this)
		{
			if (_this.timeShow <= 0)
			{
				GameEvents.OnPanelHide(_this);
			}
			GameEventHook.Panel_hideNow_original(_this);
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Panel_hideNow_original(Panel _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00099C9A File Offset: 0x00097E9A
		private static void GameScr_paintTouchControl_hook(GameScr _this, mGraphics g)
		{
			if (!GameEvents.OnPaintTouchControl(_this, g))
			{
				GameEventHook.GameScr_paintTouchControl_original(_this, g);
			}
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameScr_paintTouchControl_original(GameScr _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x00099CAC File Offset: 0x00097EAC
		private static void GameScr_paintGamePad_hook(GameScr _this, mGraphics g)
		{
			if (!GameEvents.OnGameScrPaintGamePad(g))
			{
				GameEventHook.GameScr_paintGamePad_original(_this, g);
			}
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameScr_paintGamePad_original(GameScr _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00099CBD File Offset: 0x00097EBD
		private static void SoundMn_getStrOption_hook(SoundMn _this)
		{
			if (!GameEvents.OnSoundMnGetStrOption())
			{
				GameEventHook.SoundMn_getStrOption_original(_this);
			}
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void SoundMn_getStrOption_original(SoundMn _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x00099CCC File Offset: 0x00097ECC
		private static void Panel_doFireOption_hook(Panel _this)
		{
			if (!GameEvents.OnPanelFireOption(_this))
			{
				GameEventHook.Panel_doFireOption_original(_this);
			}
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Panel_doFireOption_original(Panel _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x00099CDC File Offset: 0x00097EDC
		private static void GamePad_paint_hook(GamePad _this, mGraphics g)
		{
			if (!GameEvents.OnGamepadPaint(_this, g))
			{
				GameEventHook.GamePad_paint_original(_this, g);
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GamePad_paint_original(GamePad _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x00099CEE File Offset: 0x00097EEE
		private static void GamePad__ctor_hook(GamePad _this)
		{
			GameEventHook.GamePad__ctor_original(_this);
			_this.SetGamePadZone();
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GamePad__ctor_original(GamePad _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00099CFC File Offset: 0x00097EFC
		private static void GameScr_setSkillBarPosition_hook()
		{
			if (!GameEvents.OnSetSkillBarPosition())
			{
				GameEventHook.GameScr_setSkillBarPosition_original();
			}
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameScr_setSkillBarPosition_original()
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x00099D0A File Offset: 0x00097F0A
		private static void GameScr_paintSelectedSkill_hook(GameScr _this, mGraphics g)
		{
			if (global::Char.myCharz().IsCharDead())
			{
				return;
			}
			GameEvents.OnGameScrPaintSelectedSkill(_this, g);
			GameEventHook.GameScr_paintSelectedSkill_original(_this, g);
			GameEvents.AfterGameScrPaintSelectedSkill(_this, g);
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameScr_paintSelectedSkill_original(GameScr _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x00099D2E File Offset: 0x00097F2E
		private static void Panel_paintToolInfo_hook(Panel _this, mGraphics g)
		{
			if (!GameEvents.OnPanelPaintToolInfo(g))
			{
				GameEventHook.Panel_paintToolInfo_original(_this, g);
			}
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Panel_paintToolInfo_original(Panel _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00099D3F File Offset: 0x00097F3F
		private static void mResources_loadLanguague_hook(sbyte newLanguage)
		{
			GameEventHook.mResources_loadLanguague_original(newLanguage);
			GameEvents.OnLoadLanguage(newLanguage);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void mResources_loadLanguague_original(sbyte newLanguage)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x00099D4D File Offset: 0x00097F4D
		private static void ServerListScreen_saveIP_hook()
		{
			if (Rms.loadRMSInt("svselect") == -1)
			{
				Rms.saveRMSInt("svselect", ServerListScreen.ipSelect);
			}
			SplashScr.loadIP();
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x00099D70 File Offset: 0x00097F70
		private static void ServerListScreen_loadIP_hook()
		{
			GameEvents.OnLoadIP();
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x00099D77 File Offset: 0x00097F77
		private static void ServerListScreen_getServerList_hook(string obj)
		{
			GameEventHook.ServerListScreen_getServerList_original(Strings.DEFAULT_IP_SERVERS);
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void ServerListScreen_getServerList_original(string obj)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x00099D83 File Offset: 0x00097F83
		private static void LoginScr_switchToMe_hook(LoginScr _this)
		{
			GameEventHook.LoginScr_switchToMe_original(_this);
			_this.tfUser.setText(Rms.loadRMSString("acc"));
			_this.tfPass.setText(Rms.loadRMSString("pass"));
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void LoginScr_switchToMe_original(LoginScr _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x00099DB5 File Offset: 0x00097FB5
		private static void Skill_paint_hook(Skill _this, int x, int y, mGraphics g)
		{
			if (!GameEvents.OnSkillPaint(_this, x, y, g))
			{
				GameEventHook.Skill_paint_original(_this, x, y, g);
			}
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Skill_paint_original(Skill _this, int x, int y, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x00099DCB File Offset: 0x00097FCB
		private static void Service_gotoPlayer_hook(Service _this, int id)
		{
			if (!GameEvents.OnGotoPlayer(id, true))
			{
				GameEventHook.Service_gotoPlayer_original(_this, id);
			}
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		internal static void Service_gotoPlayer_original(Service _this, int id)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x00099DDD File Offset: 0x00097FDD
		private static void Panel_updateKeyInTabBar_hook(Panel _this)
		{
			if (!GameEvents.OnPanelUpdateKeyInTabBar(_this))
			{
				GameEventHook.Panel_updateKeyInTabBar_original(_this);
			}
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Panel_updateKeyInTabBar_original(Panel _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x00099DF0 File Offset: 0x00097FF0
		private static void Panel_paint_hook(Panel _this, mGraphics g)
		{
			if (!GameEvents.OnPaintPanel(_this, g))
			{
				GameEventHook.Panel_paint_original(_this, g);
				GameEvents.OnAfterPaintPanel(_this, g);
				GameScr.resetTranslate(g);
				_this.paintDetail(g);
				if (_this.cmx == _this.cmtoX && !GameCanvas.menu.showMenu)
				{
					_this.cmdClose.paint(g);
				}
				if (_this.tabIcon != null && _this.tabIcon.isShow)
				{
					_this.tabIcon.paint(g);
				}
				g.translate(-g.getTranslateX(), -g.getTranslateY());
				g.translate(_this.X, _this.Y);
				g.translate(-_this.cmx, 0);
			}
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Panel_paint_original(Panel _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x00099E9F File Offset: 0x0009809F
		private static void Panel_update_hook(Panel _this)
		{
			if (!GameEvents.OnUpdatePanel(_this))
			{
				GameEventHook.Panel_update_original(_this);
			}
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Panel_update_original(Panel _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x00099EAF File Offset: 0x000980AF
		private static void GameCanvas_paint_hook(GameCanvas _this, mGraphics g)
		{
			GameEventHook.GameCanvas_paint_original(_this, g);
			GameEvents.OnPaintGameCanvas(_this, g);
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameCanvas_paint_original(GameCanvas _this, mGraphics g)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x00099EBF File Offset: 0x000980BF
		private static void Char_setHoldMob_hook(global::Char _this, Mob r)
		{
			GameEventHook.Char_setHoldMob_original(_this, r);
			GameEvents.OnCharSetHoldMob(_this);
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_setHoldMob_original(global::Char _this, Mob r)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x00099ECE File Offset: 0x000980CE
		private static void Char_setHoldChar_hook(global::Char _this, global::Char r)
		{
			GameEventHook.Char_setHoldChar_original(_this, r);
			GameEvents.OnCharSetHoldChar(_this, r);
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_setHoldChar_original(global::Char _this, global::Char r)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x00099EDE File Offset: 0x000980DE
		private static void Char_removeHoleEff_hook(global::Char _this)
		{
			GameEventHook.Char_removeHoleEff_original(_this);
			GameEvents.OnCharRemoveHoldEff(_this);
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_removeHoleEff_original(global::Char _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x00099EEC File Offset: 0x000980EC
		private static void Char_update_hook(global::Char _this)
		{
			GameEvents.OnUpdateChar(_this);
			GameEventHook.Char_update_original(_this);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Char_update_original(global::Char _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x00099EFA File Offset: 0x000980FA
		private static void GameScr_paintImageBar_hook(GameScr _this, mGraphics g, bool isLeft, global::Char c)
		{
			GameEventHook.GameScr_paintImageBar_original(_this, g, isLeft, c);
			GameEvents.OnPaintImageBar(g, isLeft, c);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void GameScr_paintImageBar_original(GameScr _this, mGraphics g, bool isLeft, global::Char c)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00099F10 File Offset: 0x00098110
		private static void Panel_ctor_hook(Panel _this)
		{
			GameEventHook.Panel__ctor_original(_this);
			_this.tabName = new string[][][]
			{
				null,
				null,
				new string[][]
				{
					mResources.chestt,
					mResources.inventory
				},
				new string[][] { mResources.zonee },
				new string[][] { mResources.mapp },
				null,
				null,
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][]
				{
					mResources.combine,
					mResources.inventory
				},
				new string[][]
				{
					mResources.inventory,
					mResources.item_give,
					mResources.item_receive
				},
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				mResources.petMainTab,
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } },
				new string[][] { new string[] { string.Empty } }
			};
			_this.lastTabIndex = new int[_this.tabName.Length];
			for (int i = 0; i < _this.lastTabIndex.Length; i++)
			{
				_this.lastTabIndex[i] = -1;
			}
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void Panel__ctor_original(Panel _this)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x0009A1C4 File Offset: 0x000983C4
		private static void BackgroundEffect__ctor_hook(BackgroudEffect _this, int typeS)
		{
			GameEventHook.BackgroundEffect__ctor_original(_this, typeS);
			if (typeS == 0 || typeS == 12)
			{
				_this.sum = Res.random(10, 20);
				_this.x = new int[_this.sum];
				_this.y = new int[_this.sum];
				_this.vx = new int[_this.sum];
				_this.vy = new int[_this.sum];
				_this.type = new int[_this.sum];
				_this.t = new int[_this.sum];
				_this.frame = new int[_this.sum];
				_this.isRainEffect = new bool[_this.sum];
				_this.activeEff = new bool[_this.sum];
				for (int i = 0; i < _this.sum; i++)
				{
					_this.y[i] = Res.random(-10, GameCanvas.h + 100) + GameScr.cmy;
					_this.x[i] = Res.random(-10, GameCanvas.w + 300) + GameScr.cmx;
					_this.t[i] = Res.random(0, 1);
					_this.vx[i] = -12;
					_this.vy[i] = 12;
					_this.type[i] = Res.random(1, 3);
					_this.isRainEffect[i] = false;
					if (_this.type[i] == 2 && i % 2 == 0)
					{
						_this.isRainEffect[i] = true;
					}
					_this.activeEff[i] = false;
					_this.frame[i] = Res.random(1, 2);
				}
			}
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x000994C8 File Offset: 0x000976C8
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static void BackgroundEffect__ctor_original(BackgroudEffect _this, int typeS)
		{
			Debug.LogError("If you see this line of text in your log file, it means your hook is not installed, cannot be installed, or is installed incorrectly!");
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x0009A34B File Offset: 0x0009854B
		internal static void InstallAllHooks()
		{
			GameEventHook.InstallAll();
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x0009A352 File Offset: 0x00098552
		private static void TryInstallHook<T1, T2>(T1 hookTargetMethod, T2 hookMethod) where T1 : Delegate where T2 : Delegate
		{
			GameEventHook.TryInstallHook(hookTargetMethod.Method, hookMethod.Method, null);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x0009A370 File Offset: 0x00098570
		private static void TryInstallHook<T1, T2>(T1 hookTargetMethod, T2 hookMethod, T2 originalProxyMethod) where T1 : Delegate where T2 : Delegate
		{
			GameEventHook.TryInstallHook(hookTargetMethod.Method, hookMethod.Method, originalProxyMethod.Method);
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x0009A398 File Offset: 0x00098598
		private static void TryInstallHook(MethodBase hookTargetMethod, MethodBase hookMethod, MethodBase originalProxyMethod)
		{
			try
			{
				GameEventHook.InstallHook(hookTargetMethod, hookMethod, originalProxyMethod);
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x0009A3C8 File Offset: 0x000985C8
		private static void InstallHook(MethodBase hookTargetMethod, MethodBase hookMethod, MethodBase originalProxyMethod)
		{
			MethodHook hook = HookPool.GetHook(hookTargetMethod);
			if (hook != null)
			{
				throw new Exception(string.Format("Hook already installed: [{0}, {1}, {2}] >< [{3}, {4}, {5}]", new object[] { hook.targetMethod, hook.replacementMethod, hook.proxyMethod, hookTargetMethod, hookMethod, originalProxyMethod }));
			}
			Debug.Log(string.Concat(new string[] { "Hooking ", hookTargetMethod.Name, " to ", hookMethod.Name, "..." }));
			new MethodHook(hookTargetMethod, hookMethod, originalProxyMethod, "").Install();
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x0009A468 File Offset: 0x00098668
		internal static void UninstallAll()
		{
			HookPool.UninstallAll();
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0009A46F File Offset: 0x0009866F
		private static void UninstallHook<T1>(T1 hookTargetMethod) where T1 : Delegate
		{
			GameEventHook.UninstallHook(hookTargetMethod.Method);
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0009A481 File Offset: 0x00098681
		private static void UninstallHook(MethodInfo hookTargetMethod)
		{
			MethodHook hook = HookPool.GetHook(hookTargetMethod);
			if (hook == null)
			{
				return;
			}
			hook.Uninstall();
		}
	}
}
