using System;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using InputMap;
using Mod.AccountManager;
using Mod.Auto;
using Mod.Auto.AutoChat;
using Mod.Background;
using Mod.CharEffect;
using Mod.CustomPanel;
using Mod.Graphics;
using Mod.ModHelper;
using Mod.ModMenu;
using Mod.PickMob;
using Mod.R;
using Mod.Set;
using Mod.TeleportMenu;
using Mod.Xmap;
using UnityEngine;

namespace Mod
{
	// Token: 0x020000DE RID: 222
	internal static class GameEvents
	{
		// Token: 0x06000C66 RID: 3174 RVA: 0x00004887 File Offset: 0x00002A87
		internal static void OnAwake()
		{
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0009A494 File Offset: 0x00098694
		internal static void OnGameStart()
		{
			Application.targetFrameRate = (int)Screen.currentResolution.refreshRateRatio.value;
			QualitySettings.vSyncCount = 1;
			if (Utils.IsAndroidBuild())
			{
				Screen.sleepTimeout = -1;
				Screen.fullScreenMode = FullScreenMode.FullScreenWindow;
				Screen.orientation = ScreenOrientation.AutoRotation;
				Screen.autorotateToLandscapeLeft = true;
				Screen.autorotateToLandscapeRight = true;
				Screen.autorotateToPortrait = false;
				Screen.autorotateToPortraitUpsideDown = false;
			}
			GameEvents.OnSetResolution();
			GameEvents.OnCheckZoomLevel(Screen.width, Screen.height);
			GameEventHook.InstallAllHooks();
			if (!Directory.Exists(Utils.dataPath))
			{
				Directory.CreateDirectory(Utils.dataPath);
			}
			CustomBackground.LoadData();
			CharEffectMain.Init();
			Setup.loadFile();
			SetDo.LoadData();
			GraphicsReducer.InitializeTileMap(true);
			VietnameseInput.LoadData();
			if (!Utils.IsOpenedByExternalAccountManager)
			{
				InGameAccountManager.OnStart();
			}
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x000151BF File Offset: 0x000133BF
		internal static bool OnSendChat(string text)
		{
			return false;
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0009A54F File Offset: 0x0009874F
		internal static void OnMainStart()
		{
			UIImage.OnStart();
			if (Rms.loadRMSInt("svselect") == -1)
			{
				ServerListScreen.linkDefault = Strings.DEFAULT_IP_SERVERS;
				ServerListScreen.getServerList(Strings.DEFAULT_IP_SERVERS);
			}
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0009A578 File Offset: 0x00098778
		internal static void OnGamePause(bool paused)
		{
			if (mSystem.currentTimeMillis() - GameEvents.lastTimeGamePause > 1000L && !GameEvents.isFirstPause)
			{
				ModMenuMain.SaveData();
				CustomBackground.SaveData();
				if (!Utils.IsOpenedByExternalAccountManager)
				{
					InGameAccountManager.OnCloseAndPause();
				}
			}
			GameEvents.lastTimeGamePause = mSystem.currentTimeMillis();
			if (GameEvents.isFirstPause)
			{
				GameEvents.isFirstPause = false;
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0009A5CC File Offset: 0x000987CC
		internal static void OnGameClosing()
		{
			ModMenuMain.SaveData();
			CustomBackground.SaveData();
			Setup.clearStringTrash();
			TeleportMenuMain.SaveData();
			CustomBackground.SaveData();
			SetDo.SaveData();
			VietnameseInput.SaveData();
			if (!Utils.IsOpenedByExternalAccountManager)
			{
				InGameAccountManager.OnCloseAndPause();
			}
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0009A600 File Offset: 0x00098800
		internal static void OnFixedUpdateMain()
		{
			if (GameCanvas.currentScreen != null)
			{
				if (!GameCanvas.panel.isShow && GameCanvas.panel2 != null && GameCanvas.panel2.isShow)
				{
					GameCanvas.isFocusPanel2 = true;
					Panel panel = GameCanvas.panel2;
					if (panel != null)
					{
						panel.update();
					}
					Panel panel2 = GameCanvas.panel2;
					if (((panel2 != null) ? panel2.chatTField : null) != null && GameCanvas.panel2.chatTField.isShow)
					{
						Panel panel3 = GameCanvas.panel2;
						if (panel3 != null)
						{
							panel3.chatTFUpdateKey();
						}
					}
					else
					{
						Panel panel4 = GameCanvas.panel2;
						if (panel4 != null)
						{
							panel4.updateKey();
						}
					}
				}
				if (!GameCanvas.panel.isShow && GameCanvas.panel2 != null && GameCanvas.panel2.isShow && !GameCanvas.isPointer(GameCanvas.panel2.X, GameCanvas.panel2.Y, GameCanvas.panel2.W, GameCanvas.panel2.H) && GameCanvas.isPointerJustRelease && GameCanvas.panel2.isDoneCombine)
				{
					Panel panel5 = GameCanvas.panel2;
					if (panel5 != null)
					{
						panel5.hide();
					}
				}
			}
			CustomBackground.Update();
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0009A708 File Offset: 0x00098908
		internal static void OnUpdateMain()
		{
			if (!Main.started)
			{
				return;
			}
			if (GameEvents._previousWidth != (float)Screen.width || GameEvents._previousHeight != (float)Screen.height)
			{
				GameEvents._previousWidth = (float)Screen.width;
				GameEvents._previousHeight = (float)Screen.height;
				ScaleGUI.initScaleGUI();
				GameCanvas instance = GameCanvas.instance;
				if (instance != null)
				{
					instance.ResetSize();
				}
				Utils.ResetTextField(ChatTextField.gI());
				GamePad gamePad = GameScr.gamePad;
				if (gamePad != null)
				{
					gamePad.SetGamePadZone();
				}
				GameScr.loadCamera(false, -1, -1);
				if (GameCanvas.panel2 != null)
				{
					GameCanvas.panel2.EmulateSetTypePanel(1);
				}
				ModMenuMain.UpdatePosition();
				if (!Utils.IsOpenedByExternalAccountManager)
				{
					InGameAccountManager.UpdateSizeAndPos();
				}
			}
			MainThreadDispatcher.update();
			AutoLogin.Update();
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0009A7B1 File Offset: 0x000989B1
		internal static void OnSaveRMSString(ref string filename, ref string data)
		{
			if (filename.StartsWith("userAo") && string.IsNullOrEmpty(data))
			{
				filename = "";
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0009A7D1 File Offset: 0x000989D1
		internal static void OnLoadLanguage(sbyte newLanguage)
		{
			Strings.LoadLanguage(newLanguage);
			ModMenuMain.UpdateLanguage(newLanguage);
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0009A7E0 File Offset: 0x000989E0
		internal static void OnSetResolution()
		{
			if (Utils.IsAndroidBuild())
			{
				return;
			}
			if (Utils.sizeData != null)
			{
				int num = (int)Utils.sizeData["width"];
				int num2 = (int)Utils.sizeData["height"];
				bool fullScreen = (bool)Utils.sizeData["fullScreen"];
				if (Screen.width != num || Screen.height != num2)
				{
					Screen.SetResolution(num, num2, fullScreen);
				}
				new Thread(delegate
				{
					while (Screen.fullScreen != fullScreen)
					{
						Screen.fullScreen = fullScreen;
						Thread.Sleep(100);
					}
				}).Start();
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x00004887 File Offset: 0x00002A87
		internal static void OnGameScrPressHotkeysUnassigned()
		{
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x0009A879 File Offset: 0x00098A79
		internal static void OnGameScrPressHotkeys()
		{
			SetDo.UpdateKey();
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x00004887 File Offset: 0x00002A87
		internal static void OnPaintChatTextField(ChatTextField instance, mGraphics g)
		{
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x0009A880 File Offset: 0x00098A80
		internal static bool OnStartChatTextField(ChatTextField sender, IChatable parentScreen)
		{
			sender.parentScreen = parentScreen;
			if (!(sender.strChat.Replace(" ", "") != "Chat"))
			{
				sender.tfChat.name != "chat";
			}
			return false;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0009A8CC File Offset: 0x00098ACC
		internal static bool OnGetRMSPath(out string result)
		{
			string text = "TeaMobi";
			result = Utils.GetRootDataPath();
			result = Path.Combine(result, text);
			if (!Directory.Exists(result))
			{
				Directory.CreateDirectory(result);
			}
			return true;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x0009A902 File Offset: 0x00098B02
		internal static bool OnTeleportUpdate(Teleport teleport)
		{
			if (SpaceshipSkip.isEnabled)
			{
				SpaceshipSkip.Update(teleport);
				return true;
			}
			return false;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x0009A914 File Offset: 0x00098B14
		internal static void OnUpdateChatTextField(ChatTextField sender)
		{
			if (!string.IsNullOrEmpty(sender.tfChat.getText()))
			{
				GameCanvas.keyPressed[14] = false;
			}
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x0009A934 File Offset: 0x00098B34
		internal static bool OnClearAllRMS()
		{
			foreach (FileInfo fileInfo in from f in new DirectoryInfo(Rms.GetiPhoneDocumentsPath() + "/").GetFiles()
				where f.Extension != ".log"
				select f)
			{
				try
				{
					if (fileInfo.Name != "isPlaySound")
					{
						fileInfo.Delete();
					}
				}
				catch
				{
				}
			}
			return true;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x0009A9DC File Offset: 0x00098BDC
		internal static void OnUpdateGameScr()
		{
			if (!Utils.IsOpenedByExternalAccountManager && (float)GameCanvas.gameTick % (60f * Time.timeScale) == 0f)
			{
				Account selectedAccount = InGameAccountManager.SelectedAccount;
				if (selectedAccount != null && selectedAccount.Server.hostnameOrIPAddress == GameEvents.currentHost && selectedAccount.Server.port == GameEvents.currentPort)
				{
					selectedAccount.Gold = global::Char.myCharz().xu;
					selectedAccount.Gem = (long)global::Char.myCharz().luong;
					selectedAccount.Ruby = (long)global::Char.myCharz().luongKhoa;
					selectedAccount.Info.Name = global::Char.myCharz().cName;
					selectedAccount.Info.CharID = global::Char.myCharz().charID;
					selectedAccount.Info.Gender = (sbyte)global::Char.myCharz().cgender;
					selectedAccount.Info.EXP = global::Char.myCharz().cPower;
					selectedAccount.Info.MaxHP = (long)global::Char.myCharz().cHPFull;
					selectedAccount.Info.MaxMP = (long)global::Char.myCharz().cMPFull;
					selectedAccount.Info.Icon = global::Char.myCharz().avatarz();
					if (global::Char.myCharz().havePet)
					{
						if (selectedAccount.PetInfo == null)
						{
							selectedAccount.PetInfo = new Mod.AccountManager.CharacterInfo();
						}
						selectedAccount.PetInfo.Name = global::Char.myPetz().cName;
						selectedAccount.PetInfo.CharID = global::Char.myPetz().charID;
						selectedAccount.PetInfo.Gender = (sbyte)Utils.GetPetGender();
						selectedAccount.PetInfo.EXP = global::Char.myPetz().cPower;
						selectedAccount.PetInfo.MaxHP = (long)global::Char.myPetz().cHPFull;
						selectedAccount.PetInfo.MaxMP = (long)global::Char.myPetz().cMPFull;
						selectedAccount.PetInfo.Icon = global::Char.myPetz().avatarz();
					}
					else
					{
						selectedAccount.PetInfo = null;
					}
				}
			}
			if (global::Char.myCharz().havePet && mSystem.currentTimeMillis() - GameEvents.lastTimeRequestPetInfo > GameEvents.delayRequestPetInfo)
			{
				GameEvents.delayRequestPetInfo = (long)Res.random(750, 1000);
				GameEvents.lastTimeRequestPetInfo = mSystem.currentTimeMillis();
				Service.gI().petInfo();
			}
			if (mSystem.currentTimeMillis() - GameEvents.lastTimeRequestZoneInfo > GameEvents.delayRequestZoneInfo)
			{
				GameEvents.delayRequestZoneInfo = (long)Res.random(200, 300);
				GameEvents.lastTimeRequestZoneInfo = mSystem.currentTimeMillis();
				Service.gI().openUIZone();
			}
			global::Char.myCharz().cspeed = Utils.myCharSpeed;
			CharEffectMain.Update();
			TeleportMenuMain.Update();
			AutoGoback.Update();
			if (!AutoSellTrashItems.IsRunning)
			{
				AutoTrainNewAccount.Update();
			}
			AutoTrainPet.Update();
			AutoSellTrashItems.Update();
			AutoLogin.OnGameScrUpdate();
			if (!AutoTrainNewAccount.isEnabled && !AutoGoback.IsGoingBack && !AutoSellTrashItems.IsRunning && !AutoLogin.IsRunning)
			{
				if (Pk9rPickMob.IsTanSat)
				{
					GameScr.isAutoPlay = (GameScr.canAutoPlay = false);
				}
				Pk9rPickMob.Update();
			}
			Boss.Update();
			SetDo.Update();
			AutoPean.Update();
			AutoSkill.Update();
			if (!(ChatTextField.gI().strChat.Replace(" ", "") != "Chat"))
			{
				ChatTextField.gI().tfChat.name != "chat";
			}
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x0009AD10 File Offset: 0x00098F10
		internal static void OnLogin(ref string username, ref string pass, ref sbyte type)
		{
			if (!Utils.IsOpenedByExternalAccountManager)
			{
				if (type == 1)
				{
					InGameAccountManager.ResetSelectedAccountIndex();
					Rms.DeleteStorage("acc");
					Rms.DeleteStorage("pass");
					return;
				}
				Account selectedAccount = InGameAccountManager.SelectedAccount;
				if (selectedAccount == null)
				{
					return;
				}
				type = (sbyte)selectedAccount.Type;
				username = selectedAccount.Username;
				if (selectedAccount.Type == AccountType.Registered)
				{
					pass = selectedAccount.Password;
				}
				else
				{
					pass = string.Empty;
					Rms.DeleteStorage("userAo" + selectedAccount.Server.index.ToString());
				}
				selectedAccount.LastTimeLogin = DateTime.Now;
				return;
			}
			else
			{
				username = ((Utils.username == "") ? username : Utils.username);
				if (username.StartsWith("User"))
				{
					pass = string.Empty;
					type = 1;
					return;
				}
				pass = ((Utils.password == "") ? pass : Utils.password);
				return;
			}
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x0009ADF4 File Offset: 0x00098FF4
		internal static void OnServerListScreenLoaded(ServerListScreen serverListScreen)
		{
			ModMenuMain.Initialize();
			TeleportMenuMain.LoadData();
			AutoTrainPet.isFirstTimeCheckPet = true;
			if (!Utils.IsOpenedByExternalAccountManager)
			{
				if (string.IsNullOrEmpty(GameEvents.nameCustomServer))
				{
					return;
				}
				serverListScreen.cmd[2 + serverListScreen.nCmdPlay].caption = mResources.server + ": [custom] " + GameEvents.nameCustomServer;
			}
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x0009AE50 File Offset: 0x00099050
		internal static void OnSessionConnecting(ref string host, ref int port)
		{
			if (Utils.IsOpenedByExternalAccountManager)
			{
				if (Utils.server != null)
				{
					host = (string)Utils.server["ip"];
					port = (int)Utils.server["port"];
				}
				return;
			}
			GameEvents.nameCustomServer = "";
			if (InGameAccountManager.SelectedAccount == null)
			{
				return;
			}
			Server selectedServer = InGameAccountManager.SelectedServer;
			if (selectedServer == null)
			{
				return;
			}
			InGameAccountManager.SelectedServer = null;
			if (selectedServer.IsCustomIP())
			{
				host = (GameEvents.currentHost = selectedServer.hostnameOrIPAddress);
				port = (int)(GameEvents.currentPort = selectedServer.port);
				GameEvents.nameCustomServer = selectedServer.name;
				return;
			}
			host = (GameEvents.currentHost = ServerListScreen.address[selectedServer.index]);
			port = (int)(GameEvents.currentPort = (ushort)ServerListScreen.port[selectedServer.index]);
			ServerListScreen.ipSelect = selectedServer.index;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x00004887 File Offset: 0x00002A87
		internal static void OnScreenDownloadDataShow()
		{
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x0009AF28 File Offset: 0x00099128
		internal static bool OnCheckZoomLevel(int w, int h)
		{
			if (Utils.IsAndroidBuild())
			{
				if (w * h >= 2073600)
				{
					mGraphics.zoomLevel = 4;
				}
				else if (w * h >= 691200)
				{
					mGraphics.zoomLevel = 3;
				}
				else if (w * h > 153600)
				{
					mGraphics.zoomLevel = 2;
				}
				else
				{
					mGraphics.zoomLevel = 1;
				}
			}
			else
			{
				mGraphics.zoomLevel = 2;
				if (w * h < 480000)
				{
					mGraphics.zoomLevel = 1;
				}
			}
			return true;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x0009AF91 File Offset: 0x00099191
		internal static bool OnKeyPressed(int keyCode, bool isFromSync)
		{
			if (Utils.channelSyncKey != -1)
			{
			}
			return false;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x0009AF91 File Offset: 0x00099191
		internal static bool OnKeyReleased(int keyCode, bool isFromSync)
		{
			if (Utils.channelSyncKey != -1)
			{
			}
			return false;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x0009AF9E File Offset: 0x0009919E
		internal static bool OnChatPopupMultiLine(string chat)
		{
			if (AutoTrainNewAccount.isEnabled || AutoSellTrashItems.IsRunning || AutoGoback.IsGoingBack || AutoLogin.IsRunning)
			{
				GameScr.info1.addInfo(chat, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x0009AFCC File Offset: 0x000991CC
		internal static bool OnAddBigMessage(string chat, Npc npc)
		{
			if ((npc.avatar == 1139 || AutoTrainNewAccount.isEnabled) && new string[] { "NGOCRONGONLINE.COM", "Hack, Mod" }.Any<string>((string s) => chat.Contains(s)))
			{
				GameScr.info1.addInfo(chat, 0);
				return true;
			}
			return false;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0009B037 File Offset: 0x00099237
		internal static void OnInfoMapLoaded()
		{
			Utils.UpdateWaypointChangeMap();
			GameScr.gI().pts = null;
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x0009B049 File Offset: 0x00099249
		internal static void OnPaintGameScr(mGraphics g)
		{
			ModMenuMain.Paint(g);
			CharEffectMain.Paint(g);
			PaintControllerButtons.PaintHelp(g);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x0009B05D File Offset: 0x0009925D
		internal static bool OnUseSkill(global::Char ch)
		{
			if (ch.me)
			{
				CharEffectMain.AddEffectCreatedByMe(ch.myskill);
			}
			return false;
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x0009B073 File Offset: 0x00099273
		internal static void OnAddInfoMe(string str)
		{
			Pk9rXmap.Info(str);
			if (LocalizedString.senzuBeanHarvested.StartsWithReversed(str))
			{
				AutoTrainNewAccount.isHarvestingPean = false;
			}
			if (LocalizedString.free1hCharmReceived.ContainsReversed(str))
			{
				AutoTrainNewAccount.isNhanBua = true;
			}
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x0009B0A4 File Offset: 0x000992A4
		internal static bool OnUpdateTouchGameScr(GameScr instance)
		{
			ModMenuMain.UpdateTouch();
			if (GameCanvas.isTouchControl)
			{
				if (!TileMap.isOfflineMap())
				{
					return false;
				}
				if (GameCanvas.isMouseFocus(GameScr.xC, GameScr.yC, 34, 34))
				{
					mScreen.keyMouse = 15;
				}
				else if (GameCanvas.isMouseFocus(GameScr.xHP, GameScr.yHP, 40, 40))
				{
					if (global::Char.myCharz().statusMe != 14)
					{
						mScreen.keyMouse = 10;
					}
				}
				else if (GameCanvas.isMouseFocus(GameScr.xF, GameScr.yF, 40, 40))
				{
					if (global::Char.myCharz().statusMe != 14)
					{
						mScreen.keyMouse = 5;
					}
				}
				else if (instance.cmdMenu != null && GameCanvas.isMouseFocus(instance.cmdMenu.x, instance.cmdMenu.y, instance.cmdMenu.w / 2, instance.cmdMenu.h))
				{
					mScreen.keyMouse = 1;
				}
				else
				{
					mScreen.keyMouse = -1;
				}
				if (GameCanvas.isPointerHoldIn(GameScr.xC, GameScr.yC, 34, 34))
				{
					mScreen.keyTouch = 15;
					GameCanvas.isPointerJustDown = false;
					instance.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						ChatTextField.gI().startChat(instance, string.Empty);
						SoundMn.gI().buttonClick();
						global::Char.myCharz().currentMovePoint = null;
						GameCanvas.clearAllPointerEvent();
					}
					return true;
				}
			}
			if (instance.mobCapcha == null && GameScr.isHaveSelectSkill)
			{
				if (global::Char.myCharz().IsCharDead())
				{
					return false;
				}
				if (!instance.isCharging())
				{
					Skill[] array = (Main.isPC ? GameScr.keySkill : GameScr.onScreenSkill);
					int num = int.MinValue;
					int num2 = int.MaxValue;
					int num3 = int.MinValue;
					int num4 = int.MaxValue;
					for (int i = array.Length - 1; i >= 0; i--)
					{
						if (array[i] != null)
						{
							num = Math.Max(GameScr.xS[i], num);
							num2 = Math.Min(GameScr.xS[i], num2);
							num3 = Math.Max(GameScr.yS[i], num3);
							num4 = Math.Min(GameScr.yS[i], num4);
						}
					}
					if (GameCanvas.isPointerHoldIn(GameScr.xSkill - 5, num4 - 5, num - num2 + GameScr.wSkill, num3 - num4 + GameScr.wSkill))
					{
						for (int j = 0; j < GameScr.onScreenSkill.Length; j++)
						{
							if (GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[j], GameScr.yS[j], GameScr.wSkill, GameScr.wSkill))
							{
								GameCanvas.isPointerJustDown = false;
								instance.isPointerDowning = false;
								instance.keyTouchSkill = j;
								if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
								{
									GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
									instance.selectedIndexSkill = j;
									if (GameScr.indexSelect < 0)
									{
										GameScr.indexSelect = 0;
									}
									if (!Main.isPC)
									{
										if (instance.selectedIndexSkill > GameScr.onScreenSkill.Length - 1)
										{
											instance.selectedIndexSkill = GameScr.onScreenSkill.Length - 1;
										}
									}
									else if (instance.selectedIndexSkill > GameScr.keySkill.Length - 1)
									{
										instance.selectedIndexSkill = GameScr.keySkill.Length - 1;
									}
									Skill skill = (Main.isPC ? GameScr.keySkill[instance.selectedIndexSkill] : GameScr.onScreenSkill[instance.selectedIndexSkill]);
									if (skill != null)
									{
										instance.doSelectSkill(skill, true);
										break;
									}
									break;
								}
							}
						}
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x0009B3DF File Offset: 0x000995DF
		internal static void OnUpdateTouchPanel(Panel instance)
		{
			if (instance.type == CustomPanelMenu.TYPE_CUSTOM_PANEL_MENU)
			{
				instance.updateKeyScrollView();
			}
			SetDo.UpdateTouch(instance);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0009B3FA File Offset: 0x000995FA
		internal static void OnSetPointItemMap(int xEnd, int yEnd)
		{
			if (xEnd == global::Char.myCharz().cx && yEnd == global::Char.myCharz().cy - 10)
			{
				if (AutoTrainNewAccount.isEnabled)
				{
					AutoTrainNewAccount.isPicking = false;
				}
				if (AutoTrainPet.Mode > AutoTrainPetMode.Disabled)
				{
					AutoTrainPet.isPicking = false;
				}
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0009B434 File Offset: 0x00099634
		internal static bool OnMenuStartAt(MyVector menuItems)
		{
			if (AutoTrainNewAccount.isEnabled && menuItems.size() == 2)
			{
				Command command = (Command)menuItems.elementAt(0);
				Command command2 = (Command)menuItems.elementAt(1);
				if (command.caption == LocalizedString.getGift && command2.caption == LocalizedString.rejectGift)
				{
					GameCanvas.menu.menuSelectedItem = 0;
					command.performAction();
					AutoTrainNewAccount.isNhapCodeTanThu = true;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x0009B4A9 File Offset: 0x000996A9
		internal static void OnAddInfoChar(global::Char c, string info)
		{
			if (LocalizedString.saoMayLuoiThe.ContainsReversed(info.ToLower()) && AutoTrainPet.Mode > AutoTrainPetMode.Disabled && c.charID == -global::Char.myCharz().charID)
			{
				AutoTrainPet.saoMayLuoiThe = true;
			}
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x0009B4E0 File Offset: 0x000996E0
		internal static bool OnPaintBgGameScr(mGraphics g)
		{
			bool flag = false;
			if (GraphicsReducer.Level > ReduceGraphicsLevel.Off || (CustomBackground.isEnabled && CustomBackground.customBgs.Count > 0))
			{
				g.setColor(0);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				flag = true;
			}
			if (CustomBackground.isEnabled && CustomBackground.customBgs.Count > 0)
			{
				CustomBackground.Paint(g);
				flag = true;
			}
			return flag;
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0009B543 File Offset: 0x00099743
		internal static void OnMobStartDie(Mob mob)
		{
			Pk9rPickMob.MobStartDie(mob);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x0009B54B File Offset: 0x0009974B
		internal static void OnUpdateMob(Mob mob)
		{
			Pk9rPickMob.UpdateCountDieMob(mob);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0009B554 File Offset: 0x00099754
		internal static bool OnCreateImage(string filename, out Image image)
		{
			string text = Application.streamingAssetsPath;
			if (Utils.IsAndroidBuild())
			{
				text = Path.Combine(Utils.PersistentDataPath, "StreamingAssets");
			}
			string text2 = Path.Combine(text, "CustomAssets");
			image = new Image();
			if (!Utils.IsEditor() && !Directory.Exists(text2))
			{
				Directory.CreateDirectory(text2);
			}
			string text3 = Path.Combine(text2, filename.Replace('/', Path.DirectorySeparatorChar) + ".png");
			Texture2D texture2D;
			if (File.Exists(text3))
			{
				texture2D = new Texture2D(1, 1);
				texture2D.LoadImage(File.ReadAllBytes(text3));
			}
			else
			{
				texture2D = Resources.Load<Texture2D>(filename);
			}
			if (texture2D == null)
			{
				throw new NullReferenceException("texture2D");
			}
			image.texture = texture2D;
			image.w = image.texture.width;
			image.h = image.texture.height;
			image.texture.anisoLevel = 0;
			image.texture.filterMode = FilterMode.Point;
			image.texture.mipMapBias = 0f;
			image.texture.wrapMode = TextureWrapMode.Clamp;
			return true;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0009B666 File Offset: 0x00099866
		internal static void OnChatVip(string chatVip)
		{
			Boss.AddBoss(chatVip);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0009B66E File Offset: 0x0009986E
		internal static bool OnUpdateScrollMousePanel(Panel panel, ref int pXYScrollMouse)
		{
			SetDo.UpdateScrollMouse(panel, ref pXYScrollMouse);
			return false;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x00004887 File Offset: 0x00002A87
		internal static void OnPanelHide(Panel instance)
		{
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x00004887 File Offset: 0x00002A87
		internal static void OnUpdateKeyPanel(Panel instance)
		{
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0009B678 File Offset: 0x00099878
		internal static void OnUpdateChar(global::Char ch)
		{
			CharEffectMain.UpdateChar(ch);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0009B680 File Offset: 0x00099880
		internal static void OnCharRemoveHoldEff(global::Char ch)
		{
			CharEffectMain.RemoveHold(ch);
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0009B688 File Offset: 0x00099888
		internal static void OnCharSetHoldChar(global::Char ch, global::Char r)
		{
			CharEffectMain.AddCharHoldChar(ch, r);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0009B691 File Offset: 0x00099891
		internal static void OnCharSetHoldMob(global::Char ch)
		{
			CharEffectMain.AddCharHoldMob(ch);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0009B69C File Offset: 0x0009989C
		internal static bool OnPaintTouchControl(GameScr instance, mGraphics g)
		{
			if (instance.isNotPaintTouchControl())
			{
				return false;
			}
			GameScr.resetTranslate(g);
			if (mScreen.keyTouch == 15 || (Utils.IsPC() && mScreen.keyMouse == 15))
			{
				g.drawImage(Utils.IsPC() ? GameScr.imgChatsPC2 : GameScr.imgChat2, GameScr.xC + 17, GameScr.yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
			}
			else
			{
				g.drawImage(Utils.IsPC() ? GameScr.imgChatPC : GameScr.imgChat, GameScr.xC + 17, GameScr.yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
			}
			return true;
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x0009B74C File Offset: 0x0009994C
		internal static bool OnGameScrPaintGamePad(mGraphics g)
		{
			GameScr.isHaveSelectSkill = GameEvents.isHaveSelectSkill_old;
			if (GameScr.isAnalog != 0 && global::Char.myCharz().statusMe != 14)
			{
				g.drawImage((mScreen.keyTouch == 5) ? GameScr.imgFire1 : GameScr.imgFire0, GameScr.xF + 20, GameScr.yF + 20, mGraphics.HCENTER | mGraphics.VCENTER);
				GameScr.gamePad.paint(g);
				g.drawImage((mScreen.keyTouch != 13) ? GameScr.imgFocus : GameScr.imgFocus2, GameScr.xTG + 20, GameScr.yTG + 20, mGraphics.HCENTER | mGraphics.VCENTER);
			}
			return true;
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x0009B7F4 File Offset: 0x000999F4
		internal static bool OnPanelFireOption(Panel panel)
		{
			if (panel.selected >= 0)
			{
				switch (panel.selected)
				{
				case 0:
					SoundMn.gI().AuraToolOption();
					break;
				case 1:
					SoundMn.gI().AuraToolOption2();
					break;
				case 2:
					SoundMn.gI().chatVipToolOption();
					break;
				case 3:
					SoundMn.gI().soundToolOption();
					break;
				case 4:
					SoundMn.gI().analogToolOption();
					break;
				case 5:
					SoundMn.gI().CaseSizeScr();
					break;
				case 6:
					GameCanvas.startYesNoDlg(mResources.changeSizeScreen, new Command(mResources.YES, panel, 170391, null), new Command(mResources.NO, panel, 4005, null));
					break;
				}
			}
			return true;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x0009B8B0 File Offset: 0x00099AB0
		internal static bool OnSoundMnGetStrOption()
		{
			string[] array = new string[7];
			int num = 0;
			string aura_off = mResources.aura_off;
			array[num] = ((aura_off != null) ? aura_off.Trim() : null) + ": " + Strings.OnOffStatus(global::Char.isPaintAura);
			int num2 = 1;
			string aura_off_ = mResources.aura_off_2;
			array[num2] = ((aura_off_ != null) ? aura_off_.Trim() : null) + ": " + Strings.OnOffStatus(global::Char.isPaintAura2);
			int num3 = 2;
			string serverchat_off = mResources.serverchat_off;
			array[num3] = ((serverchat_off != null) ? serverchat_off.Trim() : null) + ": " + Strings.OnOffStatus(GameScr.isPaintChatVip);
			int num4 = 3;
			string turnOffSound = mResources.turnOffSound;
			array[num4] = ((turnOffSound != null) ? turnOffSound.Trim() : null) + ": " + Strings.OnOffStatus(GameCanvas.isPlaySound);
			int num5 = 4;
			string analog = mResources.analog;
			array[num5] = ((analog != null) ? analog.Trim() : null) + ": " + Strings.OnOffStatus(GameScr.isAnalog != 0);
			int num6 = 5;
			string text = (GameCanvas.lowGraphic ? mResources.cauhinhcao : mResources.cauhinhthap);
			array[num6] = ((text != null) ? text.Trim() : null);
			array[6] = ((mGraphics.zoomLevel <= 1) ? mResources.x2Screen : mResources.x1Screen);
			Panel.strCauhinh = array;
			return true;
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x0009B9D0 File Offset: 0x00099BD0
		internal static bool OnSetSkillBarPosition()
		{
			Skill[] keySkill = GameScr.keySkill;
			GameScr.xS = new int[keySkill.Length];
			GameScr.yS = new int[keySkill.Length];
			if (GameCanvas.isTouchControlSmallScreen && GameScr.isUseTouch)
			{
				GameScr.padSkill = 5;
			}
			else
			{
				GameScr.wSkill = 30;
				if (GameCanvas.w <= 320)
				{
					GameScr.ySkill = GameScr.gH - GameScr.wSkill - 6;
				}
				else
				{
					GameScr.wSkill = 40;
				}
			}
			GameScr.xSkill = 17;
			if (InputDeviceDetector.IsController() && GameScr.gamePad.isLargeGamePad)
			{
				GameScr.xSkill = 40;
			}
			GameScr.ySkill = GameCanvas.h - 40;
			if (GameScr.gamePad.isSmallGamePad && GameScr.isAnalog == 1)
			{
				GameScr.xHP = Math.Min(keySkill.Length, 5) * GameScr.wSkill;
				GameScr.yHP = GameScr.ySkill;
			}
			else
			{
				GameScr.xHP = GameCanvas.w - 45;
				GameScr.yHP = GameCanvas.h - 45;
			}
			if (GameScr.isAnalog != 0)
			{
				GameScr.xTG = (GameScr.xF = GameCanvas.w - 45);
				if (GameScr.gamePad.isLargeGamePad)
				{
					int num = keySkill.Length;
					int num2 = keySkill.Length - 1;
					while (num2 >= 0 && keySkill[num2] == null)
					{
						num--;
						num2--;
					}
					GameScr.wSkill = 35;
					GameScr.xSkill = Math.Max(GameScr.gamePad.wZone + 20, GameCanvas.hw - num * GameScr.wSkill / 2);
					GameScr.xHP = GameScr.xF - 45;
				}
				else if (GameScr.gamePad.isMediumGamePad)
				{
					GameScr.xHP = GameScr.xF - 45;
				}
				GameScr.yF = GameCanvas.h - 45;
				GameScr.yTG = GameScr.yF - 45;
			}
			if ((GameCanvas.isTouchControlSmallScreen && GameScr.isUseTouch) || (!GameScr.gamePad.isLargeGamePad && GameScr.isAnalog == 1))
			{
				for (int i = 0; i < GameScr.xS.Length; i++)
				{
					GameScr.xS[i] = i * GameScr.wSkill;
					GameScr.yS[i] = GameScr.ySkill;
					if (GameScr.xS.Length > 5 && i >= GameScr.xS.Length / 2)
					{
						GameScr.xS[i] = (i - GameScr.xS.Length / 2) * GameScr.wSkill;
						GameScr.yS[i] = GameScr.ySkill - 32;
					}
				}
			}
			else
			{
				int num3 = 0;
				for (int j = 0; j < GameScr.xS.Length; j++)
				{
					GameScr.xS[j] = j * GameScr.wSkill;
					GameScr.yS[j] = GameScr.ySkill;
					if (num3 == 0 && GameScr.xSkill + j * GameScr.wSkill > GameScr.xHP - 30)
					{
						num3 = j;
					}
					if (GameScr.xS.Length > 5 && num3 > 0 && j >= num3)
					{
						GameScr.xS[j] = (j - num3) * GameScr.wSkill;
						GameScr.yS[j] = GameScr.ySkill - 32;
					}
				}
			}
			return true;
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0009BC94 File Offset: 0x00099E94
		internal static bool OnGamepadPaint(GamePad instance, mGraphics g)
		{
			if (GameScr.isAnalog != 0)
			{
				if (!InputDeviceDetector.IsController())
				{
					g.drawImage(GameScr.imgAnalog1, instance.xC, instance.yC, mGraphics.HCENTER | mGraphics.VCENTER);
					g.drawImage(GameScr.imgAnalog2, instance.xM, instance.yM, mGraphics.HCENTER | mGraphics.VCENTER);
				}
				return true;
			}
			return false;
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x0009BCF8 File Offset: 0x00099EF8
		internal static void OnGameScrPaintSelectedSkill(GameScr instance, mGraphics g)
		{
			if (!GameScr.isHaveSelectSkill)
			{
				return;
			}
			GameEvents.isHaveSelectSkill_old = GameScr.isHaveSelectSkill;
			GameScr.isHaveSelectSkill = false;
			if (HideGameUI.isEnabled)
			{
				return;
			}
			Skill[] array;
			if (Main.isPC)
			{
				array = GameScr.keySkill;
			}
			else if (GameCanvas.isTouch)
			{
				array = GameScr.onScreenSkill;
			}
			else
			{
				array = GameScr.keySkill;
			}
			if (!GameCanvas.isTouch)
			{
				g.setColor(11152401);
				g.fillRect(GameScr.xSkill + GameScr.xHP + 2, GameScr.yHP - 10 + 6, 20, 10);
				mFont.tahoma_7_white.drawString(g, "*", GameScr.xSkill + GameScr.xHP + 12, GameScr.yHP - 8 + 6, mFont.CENTER);
			}
			int num = instance.nSkill;
			if (Main.isPC || !GameCanvas.isTouch)
			{
				num = array.Length;
			}
			string[] array3;
			if (!TField.isQwerty)
			{
				string[] array2 = new string[5];
				array2[0] = "7";
				array2[1] = "8";
				array2[2] = "9";
				array2[3] = "10";
				array3 = array2;
				array2[4] = "11";
			}
			else
			{
				string[] array4 = new string[10];
				array4[0] = "1";
				array4[1] = "2";
				array4[2] = "3";
				array4[3] = "4";
				array4[4] = "5";
				array4[5] = "6";
				array4[6] = "7";
				array4[7] = "8";
				array4[8] = "9";
				array3 = array4;
				array4[9] = "0";
			}
			string[] array5 = array3;
			bool flag = false;
			bool flag2 = false;
			for (int i = num - 1; i >= 0; i--)
			{
				if (array[i] != null)
				{
					flag2 = true;
				}
				if (flag2 && GameScr.yS[i] == GameScr.ySkill - 32)
				{
					flag = true;
					break;
				}
			}
			flag2 = false;
			for (int j = num - 1; j >= 0; j--)
			{
				Skill skill = array[j];
				if (skill != null)
				{
					flag2 = true;
					if (skill != global::Char.myCharz().myskill)
					{
						g.drawImage(GameScr.imgSkill, GameScr.xSkill + GameScr.xS[j] - 1, GameScr.yS[j] - 1, 0);
					}
					else
					{
						g.drawImage(GameScr.imgSkill2, GameScr.xSkill + GameScr.xS[j] - 1, GameScr.yS[j] - 1, 0);
					}
					if (Utils.IsPC())
					{
						int num2 = 27;
						if (flag)
						{
							if (GameScr.yS[j] == GameScr.ySkill - 32)
							{
								num2 = -13;
							}
						}
						else
						{
							num2 = -13;
						}
						mFont.tahoma_7b_white.drawString(g, array5[j], GameScr.xSkill + GameScr.xS[j] + 14, GameScr.yS[j] + num2 + 1, mFont.CENTER, mFont.tahoma_7b_dark);
					}
					skill.paint(GameScr.xSkill + GameScr.xS[j] + 13, GameScr.yS[j] + 13, g);
					if ((j == instance.selectedIndexSkill && !instance.isPaintUI() && GameCanvas.gameTick % 10 > 5) || j == instance.keyTouchSkill)
					{
						g.drawImage(ItemMap.imageFlare, GameScr.xSkill + GameScr.xS[j] + 13, GameScr.yS[j] + 14, 3);
					}
				}
				else if (flag2)
				{
					g.drawImage(GameScr.imgSkill, GameScr.xSkill + GameScr.xS[j] - 1, GameScr.yS[j] - 1, 0);
				}
			}
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x0009C017 File Offset: 0x0009A217
		internal static void AfterGameScrPaintSelectedSkill(GameScr instance, mGraphics g)
		{
			if (!HideGameUI.isEnabled && InputDeviceDetector.IsController())
			{
				PaintControllerButtons.PaintSelectedSkill(instance, g);
			}
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x0009C030 File Offset: 0x0009A230
		internal static bool OnPanelPaintToolInfo(mGraphics g)
		{
			mFont.tahoma_7b_white.drawString(g, Strings.communityMod, 60, 4, mFont.LEFT, mFont.tahoma_7b_dark);
			mFont.tahoma_7_yellow.drawString(g, Strings.gameVersion + ": v" + GameMidlet.VERSION, 60, 16, mFont.LEFT, mFont.tahoma_7_grey);
			mFont.tahoma_7_yellow.drawString(g, mResources.character + ": " + global::Char.myCharz().cName, 60, 27, mFont.LEFT, mFont.tahoma_7_grey);
			Account selectedAccount = InGameAccountManager.SelectedAccount;
			string text = (selectedAccount.Server.IsCustomIP() ? selectedAccount.Server.name : ServerListScreen.nameServer[selectedAccount.Server.index]);
			mFont.tahoma_7_yellow.drawString(g, string.Concat(new string[]
			{
				mResources.account,
				" ",
				mResources.account_server.ToLower(),
				" ",
				text
			}), 60, 39, mFont.LEFT, mFont.tahoma_7_grey);
			return true;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x0009C13C File Offset: 0x0009A33C
		internal static bool OnSkillPaint(Skill skill, int x, int y, mGraphics g)
		{
			if (!HideGameUI.isEnabled)
			{
				SmallImage.drawSmallImage(g, skill.template.iconId, x, y, 0, StaticObj.VCENTER_HCENTER);
			}
			long num = mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill;
			if (num < (long)skill.coolDown)
			{
				float num2 = 0.6f;
				int num3 = x - 11;
				int num4 = y - 11;
				Color color = new Color(0f, 0f, 0f, num2);
				Color color2 = new Color(0f, 0f, 0f, num2 / 2f);
				g.setColor(color2);
				g.fillRect(num3, num4, 22, 22);
				float num5 = 1f - (float)num / (float)skill.coolDown;
				CustomGraphics.drawCooldownRect((float)x, (float)y, 22f, 22f, num5, color);
				string text = string.Format("{0:#.0}", (float)((long)skill.coolDown - num) / 1000f).Replace(',', '.');
				if (text.Length > 4)
				{
					text = text.Substring(0, text.IndexOf('.'));
				}
				mFont.tahoma_7_yellow.drawString(g, text, x + 1, y - 12 + mFont.tahoma_7.getHeight() / 2, mFont.CENTER);
			}
			else
			{
				skill.paintCanNotUseSkill = false;
			}
			return true;
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x0009C27C File Offset: 0x0009A47C
		internal static bool OnGotoPlayer(int id, bool isAutoUseYardrat = true)
		{
			if (isAutoUseYardrat)
			{
				new Thread(delegate
				{
					int num = -1;
					if (global::Char.myCharz().arrItemBody[5] == null || (global::Char.myCharz().arrItemBody[5] != null && (global::Char.myCharz().arrItemBody[5].template.id < 592 || global::Char.myCharz().arrItemBody[5].template.id > 594)))
					{
						if (global::Char.myCharz().arrItemBody[5] != null)
						{
							num = (int)global::Char.myCharz().arrItemBody[5].template.id;
						}
						for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
						{
							Item item = global::Char.myCharz().arrItemBag[i];
							if (item != null && item.template.id >= 592 && item.template.id <= 594)
							{
								do
								{
									Service.gI().getItem(4, (sbyte)i);
									Thread.Sleep(250);
								}
								while (global::Char.myCharz().arrItemBody[5].template.id < 592 || global::Char.myCharz().arrItemBody[5].template.id > 594);
								break;
							}
						}
					}
					GameEventHook.Service_gotoPlayer_original(Service.gI(), id);
					if (num != -1)
					{
						Thread.Sleep(500);
						for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
						{
							Item item2 = global::Char.myCharz().arrItemBag[j];
							if (item2 != null && (int)item2.template.id == num)
							{
								do
								{
									Service.gI().getItem(4, (sbyte)j);
									Thread.Sleep(250);
								}
								while ((int)global::Char.myCharz().arrItemBody[5].template.id != num);
								return;
							}
						}
					}
				}).Start();
				return true;
			}
			return false;
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x0009C2B4 File Offset: 0x0009A4B4
		internal static bool OnPaintPanel(Panel panel, mGraphics g)
		{
			if (panel.type != CustomPanelMenu.TYPE_CUSTOM_PANEL_MENU)
			{
				return false;
			}
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.translate(-panel.cmx, 0);
			g.translate(panel.X, panel.Y);
			GameCanvas.paintz.paintFrameSimple(panel.X, panel.Y, panel.W, panel.H, g);
			g.setClip(panel.X + 1, panel.Y, panel.W - 2, panel.yScroll - 2);
			g.setColor(9993045);
			g.fillRect(panel.X, panel.Y, panel.W - 2, 50);
			CustomPanelMenu.PaintTopInfo(panel, g);
			panel.paintBottomMoneyInfo(g);
			if (!CustomPanelMenu.PaintTabHeader(panel, g))
			{
				panel.paintTab(g);
			}
			CustomPanelMenu.Paint(panel, g);
			GameScr.resetTranslate(g);
			panel.paintDetail(g);
			if (panel.cmx == panel.cmtoX)
			{
				panel.cmdClose.paint(g);
			}
			if (panel.tabIcon != null && panel.tabIcon.isShow)
			{
				panel.tabIcon.paint(g);
			}
			return true;
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x0009C3E0 File Offset: 0x0009A5E0
		internal static void OnPaintGameCanvas(GameCanvas instance, mGraphics g)
		{
			if (GameEvents.style == null)
			{
				GameEvents.style = new GUIStyle(GUI.skin.label)
				{
					fontStyle = FontStyle.Bold,
					fontSize = (int)(8.5 * (double)mGraphics.zoomLevel)
				};
				GameEvents.style.normal.textColor = (GameEvents.style.hover.textColor = Color.yellow);
			}
			if (!GameCanvas.panel.isShow && GameCanvas.panel2 != null)
			{
				g.translate(-g.getTranslateX(), -g.getTranslateY());
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				if (GameCanvas.panel2.isShow)
				{
					GameCanvas.panel2.paint(g);
				}
				if (GameCanvas.panel2.chatTField != null && GameCanvas.panel2.chatTField.isShow)
				{
					GameCanvas.panel2.chatTField.paint(g);
				}
			}
			g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.6f));
			string text = Math.Round((double)(1f / Time.smoothDeltaTime * Time.timeScale), 1).ToString("F1").Replace(',', '.');
			g.fillRect(0, 0, mFont.tahoma_7b_red.getWidth(text) + 2, 12);
			mFont.tahoma_7b_red.drawString(g, text, 2, 0, 0);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0009C540 File Offset: 0x0009A740
		internal static bool OnUpdatePanel(Panel instance)
		{
			if (instance == GameCanvas.panel)
			{
				ModMenuMain.UpdateTouch();
				Panel currentPanel = ModMenuMain.currentPanel;
				if (currentPanel != null && currentPanel.isShow && GameCanvas.isPointerJustRelease && !GameCanvas.isPointer(instance.X, instance.Y, instance.W, instance.H) && !GameCanvas.isPointer(currentPanel.X, currentPanel.Y, currentPanel.W, currentPanel.H) && !instance.pointerIsDowning)
				{
					instance.hide();
					return false;
				}
			}
			if (instance.type == CustomPanelMenu.TYPE_CUSTOM_PANEL_MENU && (instance.chatTField == null || !instance.chatTField.isShow) && !instance.isKiguiXu && !instance.isKiguiLuong && (instance.tabIcon == null || !instance.tabIcon.isShow) && instance.waitToPerform > 0 && instance.waitToPerform - 1 == 0)
			{
				instance.waitToPerform--;
				instance.lastSelect[instance.currentTabIndex] = instance.selected;
				CustomPanelMenu.DoFire(instance);
			}
			return false;
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x0009C644 File Offset: 0x0009A844
		internal static bool OnPanelUpdateKeyInTabBar(Panel instance)
		{
			if (instance.type != CustomPanelMenu.TYPE_CUSTOM_PANEL_MENU)
			{
				return false;
			}
			if ((instance.scroll != null && instance.scroll.pointerIsDowning) || instance.pointerIsDowning)
			{
				return true;
			}
			int currentTabIndex = instance.currentTabIndex;
			if (instance.isTabInven() && instance.isnewInventory)
			{
				if (instance.selected == -1)
				{
					if (GameCanvas.keyPressed[6])
					{
						instance.currentTabIndex++;
						if (instance.currentTabIndex >= instance.currentTabName.Length)
						{
							if (GameCanvas.panel2 != null)
							{
								instance.currentTabIndex = instance.currentTabName.Length - 1;
								GameCanvas.isFocusPanel2 = true;
							}
							else
							{
								instance.currentTabIndex = 0;
							}
						}
						instance.selected = instance.lastSelect[instance.currentTabIndex];
						instance.lastTabIndex[instance.type] = instance.currentTabIndex;
					}
					if (GameCanvas.keyPressed[4])
					{
						instance.currentTabIndex--;
						if (instance.currentTabIndex < 0)
						{
							instance.currentTabIndex = instance.currentTabName.Length - 1;
						}
						if (GameCanvas.isFocusPanel2)
						{
							GameCanvas.isFocusPanel2 = false;
						}
						instance.selected = instance.lastSelect[instance.currentTabIndex];
						instance.lastTabIndex[instance.type] = instance.currentTabIndex;
					}
				}
				else if (instance.selected > 0)
				{
					if (GameCanvas.keyPressed[8])
					{
						if (instance.newSelected == 0)
						{
							instance.sellectInventory++;
						}
						else
						{
							instance.sellectInventory += 5;
						}
					}
					else if (GameCanvas.keyPressed[2])
					{
						if (instance.newSelected == 0)
						{
							instance.sellectInventory--;
						}
						else
						{
							instance.sellectInventory -= 5;
						}
					}
					else if (GameCanvas.keyPressed[4])
					{
						if (instance.newSelected == 0)
						{
							instance.sellectInventory -= 5;
						}
						else
						{
							instance.sellectInventory--;
						}
					}
					else if (GameCanvas.keyPressed[6])
					{
						if (instance.newSelected == 0)
						{
							instance.sellectInventory += 5;
						}
						else
						{
							instance.sellectInventory++;
						}
					}
				}
				if (instance.sellectInventory == instance.nTableItem)
				{
					instance.sellectInventory = 0;
				}
			}
			else if (!instance.IsTabOption())
			{
				if (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24])
				{
					if (instance.isTabInven())
					{
						if (instance.selected >= 0)
						{
							instance.updateKeyInvenTab();
						}
						else
						{
							instance.currentTabIndex++;
							if (instance.currentTabIndex >= instance.currentTabName.Length)
							{
								if (GameCanvas.panel2 != null)
								{
									instance.currentTabIndex = instance.currentTabName.Length - 1;
									GameCanvas.isFocusPanel2 = true;
								}
								else
								{
									instance.currentTabIndex = 0;
								}
							}
							instance.selected = instance.lastSelect[instance.currentTabIndex];
							instance.lastTabIndex[instance.type] = instance.currentTabIndex;
						}
					}
					else
					{
						instance.currentTabIndex++;
						if (instance.currentTabIndex >= instance.currentTabName.Length)
						{
							if (GameCanvas.panel2 != null)
							{
								instance.currentTabIndex = instance.currentTabName.Length - 1;
								GameCanvas.isFocusPanel2 = true;
							}
							else
							{
								instance.currentTabIndex = 0;
							}
						}
						instance.selected = instance.lastSelect[instance.currentTabIndex];
						instance.lastTabIndex[instance.type] = instance.currentTabIndex;
					}
				}
				if (GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23])
				{
					instance.currentTabIndex--;
					if (instance.currentTabIndex < 0)
					{
						instance.currentTabIndex = instance.currentTabName.Length - 1;
					}
					if (GameCanvas.isFocusPanel2)
					{
						GameCanvas.isFocusPanel2 = false;
					}
					instance.selected = instance.lastSelect[instance.currentTabIndex];
					instance.lastTabIndex[instance.type] = instance.currentTabIndex;
				}
			}
			instance.keyTouchTab = -1;
			for (int i = 0; i < instance.currentTabName.Length; i++)
			{
				if (GameCanvas.isPointer(instance.startTabPos + i * instance.TAB_W, 52, instance.TAB_W - 1, 25))
				{
					instance.keyTouchTab = i;
					if (GameCanvas.isPointerJustRelease)
					{
						instance.currentTabIndex = i;
						instance.lastTabIndex[instance.type] = i;
						GameCanvas.isPointerJustRelease = false;
						instance.selected = instance.lastSelect[instance.currentTabIndex];
						if (currentTabIndex == instance.currentTabIndex && instance.cmRun == 0)
						{
							instance.cmtoY = 0;
							instance.selected = (GameCanvas.isTouch ? (-1) : 0);
							break;
						}
						break;
					}
				}
			}
			if (currentTabIndex == instance.currentTabIndex)
			{
				return true;
			}
			instance.size_tab = 0;
			SoundMn.gI().panelClick();
			CustomPanelMenu.SetTab(instance);
			instance.selected = instance.lastSelect[instance.currentTabIndex];
			return true;
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x0009CAE0 File Offset: 0x0009ACE0
		internal static void OnPaintImageBar(mGraphics g, bool isLeft, global::Char c)
		{
			if (!isLeft)
			{
				return;
			}
			if (c != global::Char.myCharz())
			{
				return;
			}
			int num = 85;
			int num2 = num;
			int num3 = 4;
			int num4 = 19;
			string text = Utils.FormatWithSIPrefix((double)global::Char.myCharz().cHP);
			string text2 = Utils.FormatWithSIPrefix((double)global::Char.myCharz().cMP);
			g.setColor(new Color(0.2f, 0.2f, 0.2f, 0.6f));
			if (mGraphics.zoomLevel > 1)
			{
				GameEvents.style.fontSize = (int)(8.5 * (double)mGraphics.zoomLevel);
				g.fillRect(num, num3 + 1, Utils.getWidth(GameEvents.style, text) + 1, Utils.getHeight(GameEvents.style, text) - 2);
				g.drawString(text, num, num3, GameEvents.style);
				GameEvents.style.fontSize = 5 * mGraphics.zoomLevel;
				g.fillRect(num2 - 1, num4 + 1, Utils.getWidth(GameEvents.style, text2) + 1, Utils.getHeight(GameEvents.style, text2) - 2);
				g.drawString(text2, num2, num4, GameEvents.style);
				return;
			}
			g.fillRect(num - 1, num3 + 1, mFont.tahoma_7b_yellow.getWidth(text), mFont.tahoma_7b_yellow.getHeight() - 2);
			mFont.tahoma_7b_yellow.drawString(g, text, num, num3, mFont.LEFT);
			g.fillRect(num2 - 1, num4 + 1, mFont.tahoma_7_yellow.getWidth(text2), mFont.tahoma_7_yellow.getHeight() - 2);
			mFont.tahoma_7_yellow.drawString(g, text2, num2, num4, mFont.LEFT);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x0009CC5B File Offset: 0x0009AE5B
		internal static void OnLoadIP()
		{
			ServerListScreen.getServerList(Strings.DEFAULT_IP_SERVERS);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0009CC68 File Offset: 0x0009AE68
		internal static void OnAfterPaintPanel(Panel panel, mGraphics g)
		{
			if (GameCanvas.panel.combineSuccess != -1)
			{
				return;
			}
			g.translate(-(panel.cmx - panel.cmtoX), -panel.cmy);
			if (panel.type == 13)
			{
				bool? flag = null;
				if (panel.currentTabIndex == 0 && panel != GameCanvas.panel)
				{
					flag = new bool?(false);
				}
				if (panel.currentTabIndex == 2)
				{
					flag = new bool?(false);
				}
				if (panel.currentTabIndex == 1)
				{
					flag = new bool?(true);
				}
				if (flag != null)
				{
					MyVector myVector = (flag.Value ? panel.vMyGD : panel.vFriendGD);
					if (myVector.size() <= 0)
					{
						return;
					}
					int num = Math.Max(panel.cmy / panel.ITEM_HEIGHT, 0);
					for (int i = num; i < Mathf.Clamp(num + panel.hScroll / panel.ITEM_HEIGHT + 2, 0, myVector.size()); i++)
					{
						Item item = (Item)myVector.elementAt(i);
						if (item != null)
						{
							int num2 = panel.yScroll + i * panel.ITEM_HEIGHT;
							if (item.itemOption != null)
							{
								ItemOption bestItemOption = item.GetBestItemOption();
								if (bestItemOption != null)
								{
									int num3 = bestItemOption.param;
									int id = bestItemOption.optionTemplate.id;
									if (num3 > 7 || (id >= 127 && id <= 135))
									{
										num3 = 7;
									}
									if (id == 107)
									{
										if (num3 > 1)
										{
											num3 = (int)Math.Ceiling((double)num3 / 2.0);
										}
										else if (num3 == 1)
										{
											goto IL_025D;
										}
									}
									if (num3 > 0)
									{
										g.setColor((i == panel.selected) ? 9541120 : 9993045);
										for (int j = 0; j < item.itemOption.Length; j++)
										{
											if (item.itemOption[j].optionTemplate.id == 72 && item.itemOption[j].param > 0)
											{
												byte b = (byte)GameEvents.<OnAfterPaintPanel>g__GetColor_Item_Upgrade|81_3(item.itemOption[j].param);
												if (GameEvents.<OnAfterPaintPanel>g__GetColor_ItemBg|81_4((int)b) != -1)
												{
													g.setColor(GameEvents.<OnAfterPaintPanel>g__GetColor_ItemBg|81_4((int)b));
												}
											}
										}
										g.fillRect(panel.xScroll, num2, 34, panel.ITEM_HEIGHT - 1);
										CustomGraphics.PaintItemEffectInPanel(g, panel.xScroll + 17, num2 + 11, 34, panel.ITEM_HEIGHT - 1, item);
										SmallImage.drawSmallImage(g, (int)item.template.iconID, panel.xScroll + 17, panel.yScroll + i * panel.ITEM_HEIGHT + (panel.ITEM_HEIGHT - 1) / 2, 0, 3);
									}
								}
							}
							IL_025D:
							CustomGraphics.PaintItemOptions(g, panel, item, num2);
						}
					}
					return;
				}
			}
			else if (panel.type == 1 || panel.type == 17)
			{
				if (panel.type == 1 && panel.currentTabIndex == panel.currentTabName.Length - 1 && GameCanvas.panel2 == null && panel.typeShop != 2)
				{
					return;
				}
				if (panel.typeShop == 2 && panel == GameCanvas.panel && global::Char.myCharz().arrItemShop[panel.currentTabIndex].Length == 0 && panel.type != 17)
				{
					return;
				}
				Item[] array = global::Char.myCharz().arrItemShop[panel.currentTabIndex];
				if (panel.typeShop == 2 && (panel.currentTabIndex == 4 || panel.type == 17))
				{
					array = global::Char.myCharz().arrItemShop[4];
					if (array.Length == 0)
					{
						return;
					}
				}
				for (int k = 0; k < array.Length; k++)
				{
					int num4 = panel.yScroll + k * panel.ITEM_HEIGHT;
					if (num4 - panel.cmy <= panel.yScroll + panel.hScroll && num4 - panel.cmy >= panel.yScroll - panel.ITEM_HEIGHT)
					{
						Item item2 = array[k];
						if (item2 != null)
						{
							if (item2.itemOption != null)
							{
								ItemOption bestItemOption2 = item2.GetBestItemOption();
								if (bestItemOption2 != null)
								{
									int num5 = bestItemOption2.param;
									int id2 = bestItemOption2.optionTemplate.id;
									if (num5 > 7 || (id2 >= 127 && id2 <= 135))
									{
										num5 = 7;
									}
									if (id2 == 107)
									{
										if (num5 > 1)
										{
											num5 = (int)Math.Ceiling((double)num5 / 2.0);
										}
										else if (num5 == 1)
										{
											goto IL_051B;
										}
									}
									if (num5 > 0)
									{
										g.setColor((k == panel.selected) ? 9541120 : 9993045);
										for (int l = 0; l < item2.itemOption.Length; l++)
										{
											if (item2.itemOption[l].optionTemplate.id == 72 && item2.itemOption[l].param > 0)
											{
												byte b2 = (byte)GameEvents.<OnAfterPaintPanel>g__GetColor_Item_Upgrade|81_3(item2.itemOption[l].param);
												if (GameEvents.<OnAfterPaintPanel>g__GetColor_ItemBg|81_4((int)b2) != -1)
												{
													g.setColor(GameEvents.<OnAfterPaintPanel>g__GetColor_ItemBg|81_4((int)b2));
												}
											}
										}
										g.fillRect(panel.xScroll, num4, 24, panel.ITEM_HEIGHT - 1);
										CustomGraphics.PaintItemEffectInPanel(g, panel.xScroll + 12, num4 + 11, 24, panel.ITEM_HEIGHT - 1, item2);
										SmallImage.drawSmallImage(g, (int)item2.template.iconID, panel.xScroll + 12, panel.yScroll + k * panel.ITEM_HEIGHT + (panel.ITEM_HEIGHT - 1) / 2, 0, 3);
									}
								}
							}
							IL_051B:
							if (panel.type == 17)
							{
								CustomGraphics.PaintItemOptions(g, panel, item2, num4 + mFont.tahoma_7b_blue.getHeight() + 2);
							}
							else if (panel.type == 1)
							{
								if (!string.IsNullOrEmpty(item2.nameNguoiKyGui))
								{
									uint num6;
									uint num7;
									if (GameCanvas.gameTick % 120 > 60 && (Utils.HasStarOption(item2, out num6, out num7) || Utils.HasActivateOption(item2)))
									{
										int num8 = mFont.tahoma_7b_green.getWidth(item2.nameNguoiKyGui) + 5;
										g.setColor((k != panel.selected) ? 15196114 : 16383818);
										g.fillRect(panel.X + Panel.WIDTH_PANEL - 2 - num8, num4 + mFont.tahoma_7b_blue.getHeight() + 2, num8, mFont.tahoma_7b_green.getHeight());
										CustomGraphics.PaintItemOptions(g, panel, item2, num4 + mFont.tahoma_7b_blue.getHeight() + 2);
									}
								}
								else
								{
									CustomGraphics.PaintItemOptions(g, panel, item2, num4 + mFont.tahoma_7b_blue.getHeight() + 2);
								}
							}
							else
							{
								CustomGraphics.PaintItemOptions(g, panel, item2, num4);
							}
						}
					}
				}
				return;
			}
			else
			{
				if (panel.type == 21 && panel.currentTabIndex == 0)
				{
					Item[] arrItemBody = global::Char.myPetz().arrItemBody;
					for (int m = 0; m < arrItemBody.Length; m++)
					{
						int num9 = panel.yScroll + m * panel.ITEM_HEIGHT;
						if (num9 - panel.cmy <= panel.yScroll + panel.hScroll && num9 - panel.cmy >= panel.yScroll - panel.ITEM_HEIGHT)
						{
							Item item3 = arrItemBody[m];
							if (item3 != null)
							{
								if (item3.itemOption != null)
								{
									ItemOption bestItemOption3 = item3.GetBestItemOption();
									if (bestItemOption3 != null)
									{
										int num10 = bestItemOption3.param;
										int id3 = bestItemOption3.optionTemplate.id;
										if (num10 > 7 || (id3 >= 127 && id3 <= 135))
										{
											num10 = 7;
										}
										if (id3 == 107)
										{
											if (num10 > 1)
											{
												num10 = (int)Math.Ceiling((double)num10 / 2.0);
											}
											else if (num10 == 1)
											{
												goto IL_0835;
											}
										}
										if (num10 > 0)
										{
											g.setColor((m == panel.selected) ? 9541120 : 9993045);
											for (int n = 0; n < item3.itemOption.Length; n++)
											{
												if (item3.itemOption[n].optionTemplate.id == 72 && item3.itemOption[n].param > 0)
												{
													byte b3 = (byte)GameEvents.<OnAfterPaintPanel>g__GetColor_Item_Upgrade|81_3(item3.itemOption[n].param);
													if (GameEvents.<OnAfterPaintPanel>g__GetColor_ItemBg|81_4((int)b3) != -1)
													{
														g.setColor(GameEvents.<OnAfterPaintPanel>g__GetColor_ItemBg|81_4((int)b3));
													}
												}
											}
											g.fillRect(panel.xScroll, num9, 34, panel.ITEM_HEIGHT - 1);
											CustomGraphics.PaintItemEffectInPanel(g, panel.xScroll + 17, num9 + 14, 34, panel.ITEM_HEIGHT - 1, item3);
											SmallImage.drawSmallImage(g, (int)item3.template.iconID, panel.xScroll + 17, panel.yScroll + m * panel.ITEM_HEIGHT + (panel.ITEM_HEIGHT - 1) / 2, 0, 3);
										}
									}
								}
								IL_0835:
								CustomGraphics.PaintItemOptions(g, panel, item3, num9);
							}
						}
					}
					return;
				}
				if (panel.type == 2 && panel.currentTabIndex == 0)
				{
					Item[] arrItemBox = global::Char.myCharz().arrItemBox;
					int num11 = Math.Max(panel.cmy / panel.ITEM_HEIGHT - 1, 0);
					for (int num12 = num11; num12 < Mathf.Clamp(num11 + panel.hScroll / panel.ITEM_HEIGHT + 2, 0, arrItemBox.Length); num12++)
					{
						int num13 = panel.yScroll + (num12 + 1) * panel.ITEM_HEIGHT;
						if (num13 - panel.cmy <= panel.yScroll + panel.hScroll && num13 - panel.cmy >= panel.yScroll - panel.ITEM_HEIGHT && num12 != 0)
						{
							Item item4 = arrItemBox[num12];
							if (item4 != null)
							{
								if (item4.itemOption != null)
								{
									ItemOption bestItemOption4 = item4.GetBestItemOption();
									if (bestItemOption4 != null)
									{
										int num14 = bestItemOption4.param;
										int id4 = bestItemOption4.optionTemplate.id;
										if (num14 > 7 || (id4 >= 127 && id4 <= 135))
										{
											num14 = 7;
										}
										if (id4 == 107)
										{
											if (num14 > 1)
											{
												num14 = (int)Math.Ceiling((double)num14 / 2.0);
											}
											else if (num14 == 1)
											{
												goto IL_0A5E;
											}
										}
										if (num14 > 0)
										{
											g.setColor((num12 == panel.selected) ? 9541120 : 9993045);
											for (int num15 = 0; num15 < item4.itemOption.Length; num15++)
											{
												if (item4.itemOption[num15].optionTemplate.id == 72 && item4.itemOption[num15].param > 0)
												{
													byte b4 = (byte)GameEvents.<OnAfterPaintPanel>g__GetColor_Item_Upgrade|81_3(item4.itemOption[num15].param);
													if (GameEvents.<OnAfterPaintPanel>g__GetColor_ItemBg|81_4((int)b4) != -1)
													{
														g.setColor(GameEvents.<OnAfterPaintPanel>g__GetColor_ItemBg|81_4((int)b4));
													}
												}
											}
											g.fillRect(panel.xScroll, num13, 34, panel.ITEM_HEIGHT - 1);
											CustomGraphics.PaintItemEffectInPanel(g, panel.xScroll + 17, num13 + 11, 34, panel.ITEM_HEIGHT - 1, item4);
											SmallImage.drawSmallImage(g, (int)item4.template.iconID, panel.xScroll + 17, num13 + (panel.ITEM_HEIGHT - 1) / 2, 0, 3);
										}
									}
								}
								IL_0A5E:
								CustomGraphics.PaintItemOptions(g, panel, item4, num13);
							}
						}
					}
					return;
				}
				if (panel.type == 12 && panel.currentTabIndex == 0)
				{
					if (panel.vItemCombine.size() == 0)
					{
						return;
					}
					int num16 = Math.Max(panel.cmy / panel.ITEM_HEIGHT, 0);
					for (int num17 = num16; num17 < Mathf.Clamp(num16 + panel.hScroll / panel.ITEM_HEIGHT + 2, 0, panel.vItemCombine.size() + 1); num17++)
					{
						int num18 = panel.yScroll + num17 * panel.ITEM_HEIGHT;
						if (num18 - panel.cmy <= panel.yScroll + panel.hScroll && num18 - panel.cmy >= panel.yScroll - panel.ITEM_HEIGHT && num17 != panel.vItemCombine.size())
						{
							Item item5 = (Item)panel.vItemCombine.elementAt(num17);
							if (item5 != null)
							{
								if (item5.itemOption != null)
								{
									ItemOption bestItemOption5 = item5.GetBestItemOption();
									if (bestItemOption5 != null)
									{
										int num19 = bestItemOption5.param;
										int id5 = bestItemOption5.optionTemplate.id;
										if (num19 > 7 || (id5 >= 127 && id5 <= 135))
										{
											num19 = 7;
										}
										if (id5 == 107)
										{
											if (num19 > 1)
											{
												num19 = (int)Math.Ceiling((double)num19 / 2.0);
											}
											else if (num19 == 1)
											{
												goto IL_0CC4;
											}
										}
										if (num19 > 0)
										{
											g.setColor((num17 == panel.selected) ? 9541120 : 9993045);
											for (int num20 = 0; num20 < item5.itemOption.Length; num20++)
											{
												if (item5.itemOption[num20].optionTemplate.id == 72 && item5.itemOption[num20].param > 0)
												{
													byte b5 = (byte)GameEvents.<OnAfterPaintPanel>g__GetColor_Item_Upgrade|81_3(item5.itemOption[num20].param);
													if (GameEvents.<OnAfterPaintPanel>g__GetColor_ItemBg|81_4((int)b5) != -1)
													{
														g.setColor(GameEvents.<OnAfterPaintPanel>g__GetColor_ItemBg|81_4((int)b5));
													}
												}
											}
											g.fillRect(panel.xScroll, num18, 34, panel.ITEM_HEIGHT - 1);
											CustomGraphics.PaintItemEffectInPanel(g, panel.xScroll + 17, num18 + 11, 34, panel.ITEM_HEIGHT - 1, item5);
											SmallImage.drawSmallImage(g, (int)item5.template.iconID, panel.xScroll + 17, panel.yScroll + num17 * panel.ITEM_HEIGHT + (panel.ITEM_HEIGHT - 1) / 2, 0, 3);
										}
									}
								}
								IL_0CC4:
								CustomGraphics.PaintItemOptions(g, panel, item5, num18);
							}
						}
					}
					return;
				}
				else if ((panel.type == 21 && panel.currentTabIndex == 2) || (panel.type == 0 && panel.currentTabIndex == 1) || (panel.type == 2 && panel.currentTabIndex == 1) || (panel.type == 7 || (panel.type == 12 && panel.currentTabIndex == 1)) || (panel.type == 13 && panel.currentTabIndex == 0 && panel == GameCanvas.panel) || (panel.type == 1 && panel.currentTabIndex == panel.currentTabName.Length - 1 && GameCanvas.panel2 == null && panel.typeShop != 2))
				{
					Item[] arrItemBody2 = global::Char.myCharz().arrItemBody;
					Item[] arrItemBag = global::Char.myCharz().arrItemBag;
					int num21 = Math.Max(panel.cmy / panel.ITEM_HEIGHT, 1);
					for (int num22 = num21; num22 < Mathf.Clamp(num21 + (panel.hScroll - 21) / panel.ITEM_HEIGHT + 2, 0, panel.currentListLength); num22++)
					{
						int num23 = panel.yScroll + num22 * panel.ITEM_HEIGHT;
						if (num23 - panel.cmy <= panel.yScroll + panel.hScroll && num23 - panel.cmy >= panel.yScroll - panel.ITEM_HEIGHT)
						{
							bool flag2 = GameEvents.<OnAfterPaintPanel>g__GetInventorySelect_isbody|81_0(num22, panel.newSelected, arrItemBody2);
							int num24 = GameEvents.<OnAfterPaintPanel>g__GetInventorySelect_body|81_1(num22, panel.newSelected);
							int num25 = GameEvents.<OnAfterPaintPanel>g__GetInventorySelect_bag|81_2(num22, panel.newSelected, arrItemBody2);
							Item item6 = ((!flag2) ? arrItemBag[num25] : arrItemBody2[num24]);
							if (item6 != null)
							{
								if (item6.itemOption != null)
								{
									ItemOption bestItemOption6 = item6.GetBestItemOption();
									if (bestItemOption6 != null)
									{
										int num26 = bestItemOption6.param;
										int id6 = bestItemOption6.optionTemplate.id;
										if (num26 > 7 || (id6 >= 127 && id6 <= 135))
										{
											num26 = 7;
										}
										if (id6 == 107)
										{
											if (num26 > 1)
											{
												num26 = (int)Math.Ceiling((double)num26 / 2.0);
											}
											else if (num26 == 1)
											{
												goto IL_100C;
											}
										}
										if (num26 > 0)
										{
											if (num22 == panel.selected)
											{
												g.setColor(9541120);
											}
											else if (flag2)
											{
												g.setColor(9993045);
											}
											else
											{
												g.setColor(11837316);
											}
											for (int num27 = 0; num27 < item6.itemOption.Length; num27++)
											{
												if (item6.itemOption[num27].optionTemplate.id == 72 && item6.itemOption[num27].param > 0)
												{
													byte b6 = (byte)GameEvents.<OnAfterPaintPanel>g__GetColor_Item_Upgrade|81_3(item6.itemOption[num27].param);
													if (GameEvents.<OnAfterPaintPanel>g__GetColor_ItemBg|81_4((int)b6) != -1)
													{
														g.setColor(GameEvents.<OnAfterPaintPanel>g__GetColor_ItemBg|81_4((int)b6));
													}
												}
											}
											g.fillRect(panel.xScroll, num23, 34, panel.ITEM_HEIGHT - 1);
											CustomGraphics.PaintItemEffectInPanel(g, panel.xScroll + 17 + ((panel == GameCanvas.panel2) ? 2 : 0), num23 + 11, 34, panel.ITEM_HEIGHT - 1, item6);
											SmallImage.drawSmallImage(g, (int)item6.template.iconID, panel.xScroll + 17, panel.yScroll + num22 * panel.ITEM_HEIGHT + (panel.ITEM_HEIGHT - 1) / 2, 0, 3);
										}
									}
								}
								IL_100C:
								CustomGraphics.PaintItemOptions(g, panel, item6, num23);
							}
						}
					}
				}
			}
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x0009DCBC File Offset: 0x0009BEBC
		internal static bool OnServerListScreenInitCommand(ServerListScreen screen)
		{
			screen.nCmdPlay = 0;
			string text = Rms.loadRMSString("acc");
			sbyte[] array = Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString());
			if (text == null)
			{
				if (array != null)
				{
					screen.nCmdPlay = 1;
				}
			}
			else if (text.Equals(string.Empty))
			{
				if (array != null)
				{
					screen.nCmdPlay = 1;
				}
			}
			else
			{
				screen.nCmdPlay = 1;
			}
			screen.cmd = new Command[4 + screen.nCmdPlay];
			int num = GameCanvas.hh - 15 * screen.cmd.Length + 28;
			for (int i = 0; i < screen.cmd.Length; i++)
			{
				switch (i)
				{
				case 0:
					screen.cmd[0] = new Command(string.Empty, screen, 3, null);
					if (string.IsNullOrEmpty(text))
					{
						screen.cmd[0].caption = mResources.playNew ?? "";
						if (Rms.loadRMS("userAo" + ServerListScreen.ipSelect.ToString()) != null)
						{
							screen.cmd[0].caption = mResources.choitiep ?? "";
						}
					}
					else if (!Utils.IsOpenedByExternalAccountManager)
					{
						Account selectedAccount = InGameAccountManager.SelectedAccount;
						if (selectedAccount == null)
						{
							screen.cmd[0].caption = mResources.playAcc + ": " + new string('*', text.Length);
						}
						else
						{
							screen.cmd[0].caption = mResources.playAcc + ": " + selectedAccount.Info.Name;
						}
					}
					else
					{
						screen.cmd[0].caption = mResources.playAcc + ": " + new string('*', text.Length);
					}
					if (screen.cmd[0].caption.Length > 23)
					{
						screen.cmd[0].caption = screen.cmd[0].caption.Substring(0, 23);
						Command command = screen.cmd[0];
						command.caption += "...";
					}
					break;
				case 1:
					if (screen.nCmdPlay == 1)
					{
						screen.cmd[1] = new Command(string.Empty, screen, 10100, null);
						screen.cmd[1].caption = mResources.playNew;
					}
					else if (!Utils.IsOpenedByExternalAccountManager)
					{
						screen.cmd[1] = new Command(Strings.accounts, new InGameAccountManager.ActionListener(), 7, null);
					}
					else
					{
						screen.cmd[1] = new Command(mResources.change_account, screen, 7, null);
					}
					break;
				case 2:
					if (screen.nCmdPlay == 1)
					{
						if (!Utils.IsOpenedByExternalAccountManager)
						{
							screen.cmd[2] = new Command(Strings.accounts, new InGameAccountManager.ActionListener(), 7, null);
						}
						else
						{
							screen.cmd[2] = new Command(mResources.change_account, screen, 7, null);
						}
					}
					else
					{
						screen.cmd[2] = new Command(string.Empty, screen, 17, null);
					}
					break;
				case 3:
					if (screen.nCmdPlay == 1)
					{
						screen.cmd[3] = new Command(string.Empty, screen, 17, null);
					}
					else
					{
						screen.cmd[3] = new Command(mResources.option, screen, 8, null);
					}
					break;
				case 4:
					screen.cmd[4] = new Command(mResources.option, screen, 8, null);
					break;
				}
				screen.cmd[i].y = num;
				screen.cmd[i].setType();
				screen.cmd[i].x = (GameCanvas.w - screen.cmd[i].w) / 2;
				num += 30;
			}
			return true;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x0009E04C File Offset: 0x0009C24C
		internal static bool OnPanelFireTool(Panel panel)
		{
			if (panel.selected < 0)
			{
				return false;
			}
			if (SoundMn.IsDelAcc && panel.selected == Panel.strTool.Length - 1)
			{
				return false;
			}
			if (!global::Char.myCharz().havePet)
			{
				int num = panel.selected;
				if (num == 4)
				{
					if (GameScr.gI().pts != null)
					{
						Utils.menuZone();
					}
					GameEvents.isOpenZoneUI = true;
					return true;
				}
				if (num == 8)
				{
					GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
					ServerListScreen.countDieConnect = 0;
					GameCanvas.instance.resetToLoginScr = false;
					GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
					return true;
				}
			}
			else
			{
				int num = panel.selected;
				if (num == 5)
				{
					if (GameScr.gI().pts != null)
					{
						Utils.menuZone();
					}
					GameEvents.isOpenZoneUI = true;
					return true;
				}
				if (num == 9)
				{
					GameCanvas.timeBreakLoading = mSystem.currentTimeMillis() + 30000L;
					ServerListScreen.countDieConnect = 0;
					GameCanvas.instance.resetToLoginScr = false;
					GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x0009E14C File Offset: 0x0009C34C
		internal static bool OnGetSoundOption()
		{
			if ((GameCanvas.loginScr.isLogin2 || (!Utils.IsOpenedByExternalAccountManager && InGameAccountManager.SelectedAccount != null && InGameAccountManager.SelectedAccount.Type == AccountType.Unregistered)) && global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 2)
			{
				Panel.strTool = new string[]
				{
					mResources.radaCard,
					mResources.quayso,
					mResources.gameInfo,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					Utils.IsOpenedByExternalAccountManager ? mResources.change_account : Strings.logout,
					mResources.REGISTOPROTECT
				};
				if (global::Char.myCharz().havePet)
				{
					Panel.strTool = new string[]
					{
						mResources.radaCard,
						mResources.quayso,
						mResources.gameInfo,
						mResources.pet,
						mResources.change_flag,
						mResources.change_zone,
						mResources.chat_world,
						mResources.account,
						mResources.option,
						Utils.IsOpenedByExternalAccountManager ? mResources.change_account : Strings.logout,
						mResources.REGISTOPROTECT
					};
				}
			}
			else
			{
				Panel.strTool = new string[]
				{
					mResources.radaCard,
					mResources.quayso,
					mResources.gameInfo,
					mResources.change_flag,
					mResources.change_zone,
					mResources.chat_world,
					mResources.account,
					mResources.option,
					Utils.IsOpenedByExternalAccountManager ? mResources.change_account : Strings.logout
				};
				if (global::Char.myCharz().havePet)
				{
					Panel.strTool = new string[]
					{
						mResources.radaCard,
						mResources.quayso,
						mResources.gameInfo,
						mResources.pet,
						mResources.change_flag,
						mResources.change_zone,
						mResources.chat_world,
						mResources.account,
						mResources.option,
						Utils.IsOpenedByExternalAccountManager ? mResources.change_account : Strings.logout
					};
				}
			}
			if (SoundMn.IsDelAcc)
			{
				string[] array = new string[Panel.strTool.Length + 1];
				for (int i = 0; i < Panel.strTool.Length; i++)
				{
					array[i] = Panel.strTool[i];
				}
				array[Panel.strTool.Length] = mResources.delacc;
				Panel.strTool = array;
			}
			return true;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x0009E3C4 File Offset: 0x0009C5C4
		internal static bool OnOpenUIZone(GameScr instance, Message message)
		{
			InfoDlg.hide();
			try
			{
				instance.zones = new int[(int)message.reader().readByte()];
				instance.pts = new int[instance.zones.Length];
				instance.numPlayer = new int[instance.zones.Length];
				instance.maxPlayer = new int[instance.zones.Length];
				instance.rank1 = new int[instance.zones.Length];
				instance.rankName1 = new string[instance.zones.Length];
				instance.rank2 = new int[instance.zones.Length];
				instance.rankName2 = new string[instance.zones.Length];
				for (int i = 0; i < instance.zones.Length; i++)
				{
					instance.zones[i] = (int)message.reader().readByte();
					instance.pts[i] = (int)message.reader().readByte();
					instance.numPlayer[i] = (int)message.reader().readByte();
					instance.maxPlayer[i] = (int)message.reader().readByte();
					if (message.reader().readByte() == 1)
					{
						instance.rankName1[i] = message.reader().readUTF();
						instance.rank1[i] = message.reader().readInt();
						instance.rankName2[i] = message.reader().readUTF();
						instance.rank2[i] = message.reader().readInt();
					}
				}
			}
			catch (Exception ex)
			{
				Cout.LogError("Loi ham OPEN UIZONE " + ex.ToString());
			}
			return true;
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x0009E568 File Offset: 0x0009C768
		internal static bool OnStartOKDlg(string info)
		{
			if (!(info == LocalizedString.cantChangeZoneInThisMap))
			{
				return false;
			}
			if (GameEvents.isOpenZoneUI)
			{
				GameEvents.isOpenZoneUI = false;
				return false;
			}
			return true;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x0009E589 File Offset: 0x0009C789
		internal static bool OnRequestChangeMap()
		{
			GameEvents.isOpenZoneUI = false;
			return false;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x0009E589 File Offset: 0x0009C789
		internal static bool OnGetMapOffline()
		{
			GameEvents.isOpenZoneUI = false;
			return false;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x0009E592 File Offset: 0x0009C792
		internal static bool OnMGraphicsDrawImage(Image image, int x, int y, int anchor)
		{
			return (HideGameUI.isEnabled && !HideGameUI.ShouldDrawImage(image)) || (GraphicsReducer.IsEnabled && !GraphicsReducer.ShouldDrawImage(image));
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x0009E5B7 File Offset: 0x0009C7B7
		internal static void AfterMGraphicsDrawImage(Image image, int x, int y, int anchor)
		{
			if (!HideGameUI.isEnabled && InputDeviceDetector.IsController())
			{
				PaintControllerButtons.PaintByImage(image, x, y, anchor);
			}
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0009E626 File Offset: 0x0009C826
		[CompilerGenerated]
		internal static bool <OnAfterPaintPanel>g__GetInventorySelect_isbody|81_0(int select, int subSelect, Item[] arrItem)
		{
			return subSelect == 0 && select - 1 + subSelect * 20 < arrItem.Length;
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x0009E63A File Offset: 0x0009C83A
		[CompilerGenerated]
		internal static int <OnAfterPaintPanel>g__GetInventorySelect_body|81_1(int select, int subSelect)
		{
			return select - 1 + subSelect * 20;
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x0009E644 File Offset: 0x0009C844
		[CompilerGenerated]
		internal static int <OnAfterPaintPanel>g__GetInventorySelect_bag|81_2(int select, int subSelect, Item[] arrItem)
		{
			return select - 1 + subSelect * 20 - arrItem.Length;
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x0009E652 File Offset: 0x0009C852
		[CompilerGenerated]
		internal static sbyte <OnAfterPaintPanel>g__GetColor_Item_Upgrade|81_3(int lv)
		{
			if (lv < 8)
			{
				return 0;
			}
			if (lv == 9)
			{
				return 4;
			}
			if (lv == 10)
			{
				return 1;
			}
			if (lv == 11)
			{
				return 5;
			}
			if (lv == 12)
			{
				return 3;
			}
			if (lv == 13)
			{
				return 2;
			}
			return 6;
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0009E680 File Offset: 0x0009C880
		[CompilerGenerated]
		internal static int <OnAfterPaintPanel>g__GetColor_ItemBg|81_4(int id)
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

		// Token: 0x0400148B RID: 5259
		private static float _previousWidth = (float)Screen.width;

		// Token: 0x0400148C RID: 5260
		private static float _previousHeight = (float)Screen.height;

		// Token: 0x0400148D RID: 5261
		private static bool isHaveSelectSkill_old;

		// Token: 0x0400148E RID: 5262
		private static long lastTimeGamePause;

		// Token: 0x0400148F RID: 5263
		private static long lastTimeRequestPetInfo;

		// Token: 0x04001490 RID: 5264
		private static long delayRequestPetInfo = 1000L;

		// Token: 0x04001491 RID: 5265
		private static long lastTimeRequestZoneInfo;

		// Token: 0x04001492 RID: 5266
		private static long delayRequestZoneInfo = 100L;

		// Token: 0x04001493 RID: 5267
		private static bool isFirstPause = true;

		// Token: 0x04001494 RID: 5268
		private static bool isOpenZoneUI;

		// Token: 0x04001495 RID: 5269
		private static GUIStyle style;

		// Token: 0x04001496 RID: 5270
		private static string nameCustomServer = "";

		// Token: 0x04001497 RID: 5271
		private static string currentHost = "";

		// Token: 0x04001498 RID: 5272
		private static ushort currentPort = 0;
	}
}
