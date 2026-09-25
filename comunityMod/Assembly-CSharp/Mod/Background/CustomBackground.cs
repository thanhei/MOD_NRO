using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading;
using Mod.CustomPanel;
using Mod.ModHelper.Menu;
using Mod.R;
using SFB;
using UnityEngine;

namespace Mod.Background
{
	// Token: 0x02000170 RID: 368
	internal class CustomBackground : IChatable
	{
		// Token: 0x170000D2 RID: 210
		// (get) Token: 0x060010F0 RID: 4336 RVA: 0x000B8810 File Offset: 0x000B6A10
		// (set) Token: 0x060010F1 RID: 4337 RVA: 0x000B8818 File Offset: 0x000B6A18
		internal static ScaleMode DefaultScaleMode
		{
			get
			{
				return CustomBackground._defaultScaleMode;
			}
			set
			{
				CustomBackground._defaultScaleMode = value;
				if (CustomBackground._defaultScaleMode > ScaleMode.ScaleToFit)
				{
					CustomBackground._defaultScaleMode = ScaleMode.StretchToFill;
				}
				foreach (KeyValuePair<string, IBackground> keyValuePair in CustomBackground.customBgs.Where<KeyValuePair<string, IBackground>>((KeyValuePair<string, IBackground> cBG) => !CustomBackground.overrideScaleMode.ContainsKey(cBG.Key)))
				{
					keyValuePair.Value.ScaleMode = CustomBackground._defaultScaleMode;
				}
			}
		}

		// Token: 0x170000D3 RID: 211
		// (get) Token: 0x060010F2 RID: 4338 RVA: 0x000B88A8 File Offset: 0x000B6AA8
		internal static IBackground CurrentBg
		{
			get
			{
				return CustomBackground.customBgs.ElementAt<KeyValuePair<string, IBackground>>(CustomBackground.bgIndex).Value;
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000B88CC File Offset: 0x000B6ACC
		internal static void ShowMenu()
		{
			new MenuBuilder().setChatPopup(Strings.customBgChatPopup).addItem(CustomBackground.customBgs.Count > 0, Strings.customBgOpenBgList, new MenuAction(delegate
			{
				CustomPanelMenu.Show(new CustomPanelMenuConfig
				{
					SetTabAction = new Action<Panel>(CustomBackground.SetTabCustomBackgroundPanel),
					DoFireItemAction = new Action<Panel>(CustomBackground.DoFireCustomBackgroundListPanel),
					PaintTabHeaderAction = new Action<Panel, mGraphics>(CustomBackground.PaintTabHeader),
					PaintAction = new Action<Panel, mGraphics>(CustomBackground.PaintCustomBackgroundPanel)
				}, null);
			})).addItem(Strings.customBgAddNewBg, new MenuAction(new Action(CustomBackground.SelectBackgrounds)))
				.addItem(CustomBackground.customBgs.Count > 0, Strings.customBgRemoveAll, new MenuAction(delegate
				{
					foreach (BackgroundVideo backgroundVideo in CustomBackground.customBgs.OfType<BackgroundVideo>())
					{
						backgroundVideo.Stop();
					}
					CustomBackground.customBgs.Clear();
					GameScr.info1.addInfo(Strings.customBgAllBgRemoved + "!", 0);
				}))
				.addItem(Strings.customBgAutoChangeBg + ": " + Strings.OnOffStatus(CustomBackground.isChangeBg), new MenuAction(delegate
				{
					CustomBackground.isChangeBg = !CustomBackground.isChangeBg;
					CustomBackground.lastTimeChangedBg = mSystem.currentTimeMillis();
					GameScr.info1.addInfo(Strings.customBgAutoChangeBg + ": " + Strings.OnOffStatus(CustomBackground.isChangeBg), 0);
				}))
				.addItem(Strings.customBgDefaultScaleModeTitle + ": " + CustomBackground.DefaultScaleMode.GetName(), new MenuAction(delegate
				{
					CustomBackground.DefaultScaleMode++;
					GameScr.info1.addInfo(Strings.customBgDefaultScaleModeTitle + ": " + CustomBackground.DefaultScaleMode.GetName(), 0);
				}))
				.addItem(Strings.customBgSetTimeChange, new MenuAction(delegate
				{
					ChatTextField.gI().strChat = Strings.inputTimeChangeBg;
					ChatTextField.gI().tfChat.name = Strings.inputTimeChangeBgHint;
					ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
					ChatTextField.gI().startChat2(CustomBackground.instance, string.Empty);
				}))
				.addItem(Strings.customBgChangeGifSpeed, new MenuAction(delegate
				{
					ChatTextField.gI().strChat = Strings.customBgInputGifSpeed;
					ChatTextField.gI().tfChat.name = Strings.speed;
					ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_ANY);
					ChatTextField.gI().startChat2(CustomBackground.instance, string.Empty);
					ChatTextField.gI().tfChat.setText(CustomBackground.speed.ToString());
				}))
				.start();
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000B8A60 File Offset: 0x000B6C60
		internal static void StopAllBackgroundVideo()
		{
			foreach (BackgroundVideo backgroundVideo in from v in CustomBackground.customBgs.Values.OfType<BackgroundVideo>()
				where v.isPlaying
				select v)
			{
				backgroundVideo.Stop();
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000B8AD8 File Offset: 0x000B6CD8
		internal static void SelectBackgrounds()
		{
			string[] paths = null;
			new Thread(delegate
			{
				ExtensionFilter[] array = new ExtensionFilter[]
				{
					new ExtensionFilter(Strings.imageVideoFile, new string[] { "png", "jpg", "jpeg", "gif", "mp4" }),
					new ExtensionFilter(Strings.allFileTypes, new string[] { "*" })
				};
				paths = StandaloneFileBrowser.OpenFilePanel(Strings.customBgSelectBgFiles, "", array, true);
				if (paths.Length == 0)
				{
					return;
				}
				foreach (string text in paths)
				{
					CustomBackground.customBgs.Add(text, null);
				}
				CustomBackground.isAllBgsLoaded = false;
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000B8B04 File Offset: 0x000B6D04
		internal static void Update()
		{
			if (!CustomBackground.isAllBgsLoaded)
			{
				List<string> list = new List<string>(CustomBackground.customBgs.Keys);
				for (int i = list.Count - 1; i >= 0; i--)
				{
					string text = list[i];
					try
					{
						if (CustomBackground.customBgs[text] == null || !CustomBackground.customBgs[text].IsLoaded)
						{
							if (text.EndsWith(".gif"))
							{
								CustomBackground.customBgs[text] = new GifImage(text);
							}
							else if (text.EndsWith(".mp4"))
							{
								CustomBackground.customBgs[text] = new BackgroundVideo(text);
							}
							else
							{
								CustomBackground.customBgs[text] = new StaticImage(text);
							}
							if (CustomBackground.overrideScaleMode.ContainsKey(text))
							{
								CustomBackground.customBgs[text].ScaleMode = CustomBackground.overrideScaleMode[text];
							}
							else
							{
								CustomBackground.customBgs[text].ScaleMode = CustomBackground.DefaultScaleMode;
							}
						}
					}
					catch (FileNotFoundException)
					{
						CustomBackground.customBgs.Remove(text);
					}
					catch (IsolatedStorageException)
					{
						CustomBackground.customBgs.Remove(text);
					}
					catch (Exception ex)
					{
						Debug.LogException(ex);
					}
				}
				CustomBackground.lastTimeChangedBg = mSystem.currentTimeMillis();
				CustomBackground.isAllBgsLoaded = true;
				CustomBackground.SaveData();
			}
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000B8C64 File Offset: 0x000B6E64
		internal static void Paint(mGraphics g)
		{
			if (!CustomBackground.isEnabled || CustomBackground.customBgs.Count <= 0)
			{
				return;
			}
			try
			{
				if (CustomBackground.bgIndex >= CustomBackground.customBgs.Count)
				{
					CustomBackground.bgIndex = 0;
				}
				IBackground value = CustomBackground.customBgs.ElementAt<KeyValuePair<string, IBackground>>(CustomBackground.bgIndex).Value;
				if (value != null)
				{
					BackgroundVideo backgroundVideo = value as BackgroundVideo;
					if (backgroundVideo != null && !backgroundVideo.isPlaying)
					{
						if (!backgroundVideo.IsLoaded && !backgroundVideo.isPreparing)
						{
							backgroundVideo.Prepare();
						}
						backgroundVideo.Play();
					}
					GifImage gifImage = value as GifImage;
					if (gifImage != null && gifImage.speed != CustomBackground.speed)
					{
						gifImage.speed = CustomBackground.speed;
					}
					value.Paint(g, 0, 0);
					if (CustomBackground.isChangeBg)
					{
						if (mSystem.currentTimeMillis() - CustomBackground.lastTimeChangedBg > (long)(CustomBackground.intervalChangeBg - 2000))
						{
							int num = CustomBackground.bgIndex + 1;
							if (num >= CustomBackground.customBgs.Count)
							{
								num = 0;
							}
							BackgroundVideo backgroundVideo2 = CustomBackground.customBgs.ElementAt<KeyValuePair<string, IBackground>>(num).Value as BackgroundVideo;
							if (backgroundVideo2 != null && !backgroundVideo2.isPreparing && !backgroundVideo2.IsLoaded)
							{
								backgroundVideo2.Prepare();
							}
						}
						if (mSystem.currentTimeMillis() - CustomBackground.lastTimeChangedBg > (long)CustomBackground.intervalChangeBg)
						{
							CustomBackground.lastTimeChangedBg = mSystem.currentTimeMillis();
							BackgroundVideo backgroundVideo3 = value as BackgroundVideo;
							if (backgroundVideo3 != null && backgroundVideo3.isPlaying)
							{
								backgroundVideo3.Stop();
							}
							CustomBackground.bgIndex++;
						}
					}
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000B8DF8 File Offset: 0x000B6FF8
		internal static void PaintCustomBackgroundPanel(Panel panel, mGraphics g)
		{
			g.setClip(GameCanvas.panel.xScroll, GameCanvas.panel.yScroll, GameCanvas.panel.wScroll, GameCanvas.panel.hScroll);
			g.translate(0, -GameCanvas.panel.cmy);
			g.setColor(0);
			if (CustomBackground.customBgs.Count != GameCanvas.panel.currentListLength)
			{
				return;
			}
			int num = Math.Max(panel.cmy / panel.ITEM_HEIGHT, 0);
			for (int i = num; i < Mathf.Clamp(num + panel.hScroll / panel.ITEM_HEIGHT + 2, 0, panel.currentListLength); i++)
			{
				int xScroll = GameCanvas.panel.xScroll;
				int num2 = GameCanvas.panel.yScroll + i * GameCanvas.panel.ITEM_HEIGHT;
				int wScroll = GameCanvas.panel.wScroll;
				int num3 = GameCanvas.panel.ITEM_HEIGHT - 1;
				if (CustomBackground.bgIndex == i)
				{
					g.setColor((i != GameCanvas.panel.selected) ? new Color(0.5f, 1f, 0f) : new Color(0.375f, 0.75f, 0f));
				}
				else
				{
					g.setColor((i != GameCanvas.panel.selected) ? 15196114 : 16383818);
				}
				g.fillRect(xScroll, num2, wScroll, num3);
				mFont.tahoma_7_green2.drawString(g, (i + 1).ToString() + ". " + Path.GetFileName(CustomBackground.customBgs.ElementAt<KeyValuePair<string, IBackground>>(i).Key), xScroll + 5, num2, 0);
				mFont.tahoma_7_blue.drawString(g, Strings.fullPath + ": " + CustomBackground.customBgs.ElementAt<KeyValuePair<string, IBackground>>(i).Key, xScroll + 5, num2 + 11, 0);
			}
			GameCanvas.panel.paintScrollArrow(g);
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000B8FD4 File Offset: 0x000B71D4
		internal static void PaintTabHeader(Panel panel, mGraphics g)
		{
			PaintPanelTemplates.PaintTabHeaderTemplate(panel, g, Strings.customBgList);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000B8FE2 File Offset: 0x000B71E2
		internal static void SetTabCustomBackgroundPanel(Panel panel)
		{
			SetTabPanelTemplates.setTabListTemplate(panel, new ICollection[] { CustomBackground.customBgs });
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x000B8FF8 File Offset: 0x000B71F8
		internal static void DoFireCustomBackgroundListPanel(Panel panel)
		{
			int selected = panel.selected;
			if (selected < 0)
			{
				return;
			}
			KeyValuePair<string, IBackground> customBg = CustomBackground.customBgs.ElementAt<KeyValuePair<string, IBackground>>(selected);
			new MenuBuilder().addItem(CustomBackground.bgIndex != selected, Strings.customBgSwitchToThisBg, new MenuAction(delegate
			{
				CustomBackground.StopAllBackgroundVideo();
				CustomBackground.bgIndex = selected;
				CustomBackground.lastTimeChangedBg = mSystem.currentTimeMillis();
			})).addItem(Strings.delete, new MenuAction(delegate
			{
				BackgroundVideo backgroundVideo = customBg.Value as BackgroundVideo;
				if (backgroundVideo != null && backgroundVideo.isPlaying)
				{
					backgroundVideo.Stop();
				}
				string key = customBg.Key;
				CustomBackground.customBgs.Remove(customBg.Key);
				if (selected < CustomBackground.bgIndex)
				{
					CustomBackground.bgIndex--;
					CustomBackground.lastTimeChangedBg = mSystem.currentTimeMillis();
				}
				else if (selected == CustomBackground.bgIndex && CustomBackground.customBgs.Count == CustomBackground.bgIndex)
				{
					CustomBackground.bgIndex = 0;
					CustomBackground.lastTimeChangedBg = mSystem.currentTimeMillis();
				}
				GameScr.info1.addInfo(string.Format(Strings.customBgRemovedBg, key) + "!", 0);
				CustomBackground.SetTabCustomBackgroundPanel(panel);
				CustomBackground.SaveData();
			})).addItem(Strings.customBgScaleMode + ": " + customBg.Value.ScaleMode.GetName(), new MenuAction(delegate
			{
				if (CustomBackground.overrideScaleMode.ContainsKey(customBg.Key))
				{
					Dictionary<string, ScaleMode> dictionary = CustomBackground.overrideScaleMode;
					string key2 = customBg.Key;
					ScaleMode scaleMode = dictionary[key2];
					dictionary[key2] = scaleMode + 1;
				}
				else
				{
					CustomBackground.overrideScaleMode.Add(customBg.Key, customBg.Value.ScaleMode + 1);
				}
				if (CustomBackground.overrideScaleMode[customBg.Key] > ScaleMode.ScaleToFit)
				{
					CustomBackground.overrideScaleMode[customBg.Key] = ScaleMode.StretchToFill;
				}
				customBg.Value.ScaleMode = CustomBackground.overrideScaleMode[customBg.Key];
				GameScr.info1.addInfo(Strings.customBgScaleMode + ": " + CustomBackground.overrideScaleMode[customBg.Key].GetName(), 0);
			}))
				.addItem(CustomBackground.overrideScaleMode.ContainsKey(customBg.Key), Strings.customBgResetScaleModeToDefault, new MenuAction(delegate
				{
					if (CustomBackground.overrideScaleMode.ContainsKey(customBg.Key))
					{
						CustomBackground.overrideScaleMode.Remove(customBg.Key);
					}
					customBg.Value.ScaleMode = CustomBackground.DefaultScaleMode;
				}))
				.setPos(panel.X, (selected + 1) * panel.ITEM_HEIGHT - panel.cmy + panel.yScroll)
				.start();
			string fileName = Path.GetFileName(customBg.Key);
			panel.cp = new ChatPopup();
			panel.cp.isClip = false;
			panel.cp.sayWidth = 180;
			panel.cp.cx = 3 + panel.X;
			if (panel.X != 0)
			{
				panel.cp.cx -= Res.abs(panel.cp.sayWidth - panel.W) + 8;
			}
			panel.cp.says = mFont.tahoma_7_red.splitFontArray(string.Concat(new string[]
			{
				"|0|2|",
				fileName,
				"\n--\n|6|",
				Strings.fullPath,
				": ",
				customBg.Key
			}), panel.cp.sayWidth - 10);
			panel.cp.delay = 10000000;
			panel.cp.c = null;
			panel.cp.sayRun = 7;
			panel.cp.ch = 15 - panel.cp.sayRun + panel.cp.says.Length * 12 + 10;
			if (panel.cp.ch > GameCanvas.h - 80)
			{
				panel.cp.ch = GameCanvas.h - 80;
				panel.cp.lim = panel.cp.says.Length * 12 - panel.cp.ch + 17;
				if (panel.cp.lim < 0)
				{
					panel.cp.lim = 0;
				}
				ChatPopup.cmyText = 0;
				panel.cp.isClip = true;
			}
			panel.cp.cy = GameCanvas.menu.menuY - panel.cp.ch;
			while (panel.cp.cy < 10)
			{
				panel.cp.cy++;
				GameCanvas.menu.menuY++;
			}
			panel.cp.mH = 0;
			panel.cp.strY = 10;
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x000B93F8 File Offset: 0x000B75F8
		internal static void LoadData()
		{
			try
			{
				string text;
				if (Utils.TryLoadDataString("custom_bg_override_scale_modes", out text, true))
				{
					string[] array = text.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
					for (int i = 0; i < array.Length; i++)
					{
						string[] array2 = array[i].Split('|', StringSplitOptions.None);
						CustomBackground.overrideScaleMode.Add(array2[0], Enum.Parse<ScaleMode>(array2[1]));
					}
				}
				foreach (string text2 in Utils.LoadDataString("custom_bg_paths", true).Split('|', StringSplitOptions.None))
				{
					if (!string.IsNullOrEmpty(text2))
					{
						CustomBackground.customBgs.Add(text2, null);
					}
				}
				CustomBackground.isAllBgsLoaded = false;
				Utils.TryLoadDataBool("custom_bg_change", out CustomBackground.isChangeBg, true);
				long num;
				if (Utils.TryLoadDataLong("custom_bg_index", out num, true))
				{
					CustomBackground.bgIndex = (int)num;
				}
				long num2;
				if (Utils.TryLoadDataLong("custom_bg_default_scale_mode", out num2, true))
				{
					CustomBackground._defaultScaleMode = (ScaleMode)num2;
				}
				if (CustomBackground.bgIndex >= CustomBackground.customBgs.Count)
				{
					CustomBackground.bgIndex = 0;
				}
				double num3;
				if (Utils.TryLoadDataDouble("custom_bg_gif_speed", out num3, true))
				{
					CustomBackground.speed = Mathf.Clamp((float)num3, 0f, 100f);
				}
				long num4;
				if (Utils.TryLoadDataLong("custom_bg_interval", out num4, true))
				{
					CustomBackground.intervalChangeBg = (int)num4;
				}
			}
			catch (Exception ex)
			{
				Debug.LogException(ex);
			}
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000B9560 File Offset: 0x000B7760
		internal static void SaveData()
		{
			string text = string.Join("|", CustomBackground.customBgs.Keys.ToArray<string>());
			Utils.SaveData("custom_bg_paths", text, true);
			Utils.SaveData("custom_bg_change", CustomBackground.isChangeBg, true);
			Utils.SaveData("custom_bg_index", (long)CustomBackground.bgIndex, true);
			Utils.SaveData("custom_bg_gif_speed", (double)CustomBackground.speed, true);
			Utils.SaveData("custom_bg_interval", (long)CustomBackground.intervalChangeBg, true);
			Utils.SaveData("custom_bg_default_scale_mode", (long)CustomBackground.DefaultScaleMode, true);
			Utils.SaveData("custom_bg_override_scale_modes", string.Join(Environment.NewLine, CustomBackground.overrideScaleMode.Select<KeyValuePair<string, ScaleMode>, string>((KeyValuePair<string, ScaleMode> kVP) => kVP.Key + "|" + kVP.Value.ToString())), true);
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x000B9628 File Offset: 0x000B7828
		internal static void SetState(bool value)
		{
			CustomBackground.isEnabled = value;
			if (value)
			{
				return;
			}
			foreach (IBackground background2 in CustomBackground.customBgs.Values.Where<IBackground>((IBackground background) => background is BackgroundVideo))
			{
				((BackgroundVideo)background2).Stop();
			}
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x000B96AC File Offset: 0x000B78AC
		public void onChatFromMe(string text, string to)
		{
			if (string.IsNullOrEmpty(text))
			{
				this.onCancelChat();
				return;
			}
			if (ChatTextField.gI().strChat == Strings.customBgInputGifSpeed)
			{
				try
				{
					float num = float.Parse(text);
					if (num > 10f || num < 0.1f)
					{
						GameCanvas.startOKDlg(string.Format(Strings.inputNumberOutOfRange, 0.1, 10) + "!");
					}
					else
					{
						if (num != CustomBackground.speed)
						{
							CustomBackground.speed = num;
						}
						GameScr.info1.addInfo(string.Format(Strings.valueChanged, Strings.customBgGifSpeed, num) + "!", 0);
						CustomBackground.SaveData();
					}
					goto IL_0165;
				}
				catch (Exception)
				{
					GameCanvas.startOKDlg(Strings.invalidValue + "!");
					goto IL_0165;
				}
			}
			if (ChatTextField.gI().strChat == Strings.inputTimeChangeBg)
			{
				try
				{
					int num2 = int.Parse(text);
					if (num2 < 10)
					{
						GameCanvas.startOKDlg(string.Format(Strings.inputNumberMustBeBiggerThanOrEqual, 10) + "!");
					}
					else
					{
						CustomBackground.intervalChangeBg = num2;
						GameScr.info1.addInfo(string.Format(Strings.valueChanged, Strings.setTimeChangeCustomBgTitle.ToLower(), num2) + "!", 0);
						CustomBackground.SaveData();
					}
				}
				catch (Exception)
				{
					GameCanvas.startOKDlg(Strings.invalidValue + "!");
				}
			}
			IL_0165:
			this.onCancelChat();
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x0009F278 File Offset: 0x0009D478
		public void onCancelChat()
		{
			ChatTextField.gI().ResetTF();
		}

		// Token: 0x04001865 RID: 6245
		internal static bool isEnabled;

		// Token: 0x04001866 RID: 6246
		internal static Dictionary<string, IBackground> customBgs = new Dictionary<string, IBackground>();

		// Token: 0x04001867 RID: 6247
		private static ScaleMode _defaultScaleMode = ScaleMode.StretchToFill;

		// Token: 0x04001868 RID: 6248
		internal static Dictionary<string, ScaleMode> overrideScaleMode = new Dictionary<string, ScaleMode>();

		// Token: 0x04001869 RID: 6249
		internal static int intervalChangeBg = 30000;

		// Token: 0x0400186A RID: 6250
		private static int bgIndex;

		// Token: 0x0400186B RID: 6251
		private static bool isAllBgsLoaded;

		// Token: 0x0400186C RID: 6252
		private static long lastTimeChangedBg;

		// Token: 0x0400186D RID: 6253
		private static bool isChangeBg = true;

		// Token: 0x0400186E RID: 6254
		private static float speed = 1f;

		// Token: 0x0400186F RID: 6255
		private static CustomBackground instance = new CustomBackground();
	}
}
