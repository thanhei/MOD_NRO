using System;
using System.IO;
using System.Threading;
using Mod.ModHelper.Menu;
using Mod.R;
using SFB;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

namespace Mod
{
	// Token: 0x020000E5 RID: 229
	public class IntroPlayer : MonoBehaviour
	{
		// Token: 0x06000CCD RID: 3277 RVA: 0x0009EF34 File Offset: 0x0009D134
		private void Awake()
		{
			GameEvents.OnAwake();
			IntroPlayer.videoPlayer = GameObject.Find("Main Camera").GetComponent<VideoPlayer>();
			Utils.TryLoadDataBool("intro_enabled", out IntroPlayer.isEnabled, true);
			Utils.TryLoadDataString("intro_path", out IntroPlayer.path, true);
			long num;
			if (Utils.TryLoadDataLong("intro_volume", out num, true))
			{
				IntroPlayer.volume = (float)num / 100f;
			}
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0009EF98 File Offset: 0x0009D198
		private void Start()
		{
			GameEvents.OnGameStart();
			if (!File.Exists(IntroPlayer.path) || !IntroPlayer.isEnabled)
			{
				SceneManager.LoadScene("NROL");
				return;
			}
			IntroPlayer.videoPlayer.url = IntroPlayer.path;
			if (IntroPlayer.volume == 0f)
			{
				IntroPlayer.videoPlayer.audioOutputMode = VideoAudioOutputMode.None;
			}
			else
			{
				IntroPlayer.videoPlayer.SetDirectAudioVolume(0, IntroPlayer.volume);
			}
			IntroPlayer.videoPlayer.prepareCompleted += this.VideoPlayer_prepareCompleted;
			IntroPlayer.videoPlayer.Prepare();
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x0009F020 File Offset: 0x0009D220
		private void VideoPlayer_prepareCompleted(VideoPlayer source)
		{
			IntroPlayer.videoPlayer.Play();
			IntroPlayer.isPlaying = true;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x0009F032 File Offset: 0x0009D232
		private void Update()
		{
			if (!IntroPlayer.videoPlayer.isPlaying && IntroPlayer.isPlaying)
			{
				SceneManager.LoadScene("NROL");
			}
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x0009F054 File Offset: 0x0009D254
		private void OnGUI()
		{
			if (IntroPlayer.videoPlayer.isPlaying && IntroPlayer.isPlaying)
			{
				GUI.DrawTexture(new Rect(0f, 0f, (float)Screen.width, (float)Screen.height), IntroPlayer.videoPlayer.texture, ScaleMode.ScaleToFit);
			}
			if (Input.GetMouseButtonDown(0) || GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				IntroPlayer.videoPlayer.Stop();
				SceneManager.LoadScene("NROL");
			}
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0009F0D0 File Offset: 0x0009D2D0
		internal static void ShowMenu()
		{
			MenuBuilder menuBuilder = new MenuBuilder().setChatPopup(Strings.introCurrentPath + ": " + IntroPlayer.path).addItem(Strings.introChangeVideoPath, new MenuAction(new Action(IntroPlayer.SelectVideo))).addItem(Strings.setIntroVolumeTitle, new MenuAction(delegate
			{
				ChatTextField.gI().strChat = Strings.introInputVolume;
				ChatTextField.gI().tfChat.name = Strings.introInputVolumeHint;
				ChatTextField.gI().tfChat.setIputType(TField.INPUT_TYPE_NUMERIC);
				ChatTextField.gI().startChat2(new IntroPlayer.IntroPlayerChatable(), string.Empty);
				ChatTextField.gI().tfChat.setText((IntroPlayer.volume * 100f).ToString());
			}));
			if (string.IsNullOrEmpty(IntroPlayer.path))
			{
				menuBuilder.setChatPopup(Strings.introNoVideo + ".");
			}
			menuBuilder.start();
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0009F16D File Offset: 0x0009D36D
		internal static void SelectVideo()
		{
			string[] paths = null;
			new Thread(delegate
			{
				ExtensionFilter[] array = new ExtensionFilter[]
				{
					new ExtensionFilter(Strings.videoFile, new string[] { "mp4" }),
					new ExtensionFilter(Strings.allFileTypes, new string[] { "*" })
				};
				paths = StandaloneFileBrowser.OpenFilePanel(Strings.introSelectFile, "", array, false);
				if (paths.Length == 0)
				{
					return;
				}
				IntroPlayer.path = paths[0];
				Utils.SaveData("intro_path", IntroPlayer.path, true);
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x040014AC RID: 5292
		internal static bool isEnabled;

		// Token: 0x040014AD RID: 5293
		internal static string path;

		// Token: 0x040014AE RID: 5294
		internal static float volume = 1f;

		// Token: 0x040014AF RID: 5295
		private static VideoPlayer videoPlayer;

		// Token: 0x040014B0 RID: 5296
		private static bool isPlaying;

		// Token: 0x020000E6 RID: 230
		private class IntroPlayerChatable : IChatable
		{
			// Token: 0x06000CD6 RID: 3286 RVA: 0x0009F1A4 File Offset: 0x0009D3A4
			public void onChatFromMe(string text, string to)
			{
				if (string.IsNullOrEmpty(text))
				{
					this.onCancelChat();
					return;
				}
				if (ChatTextField.gI().strChat == Strings.introInputVolume)
				{
					int num;
					if (int.TryParse(text, out num))
					{
						if (num < 0 || num > 100)
						{
							GameCanvas.startOKDlg(string.Format(Strings.inputNumberOutOfRange, 0, 100) + "!");
						}
						else
						{
							IntroPlayer.volume = (float)num / 100f;
							Utils.SaveData("intro_volume", (double)IntroPlayer.volume, true);
							GameScr.info1.addInfo(string.Format(Strings.valueChanged, Strings.setIntroVolumeTitle, num) + "!", 0);
						}
					}
					else
					{
						GameCanvas.startOKDlg(Strings.invalidValue + "!");
					}
				}
				this.onCancelChat();
			}

			// Token: 0x06000CD7 RID: 3287 RVA: 0x0009F278 File Offset: 0x0009D478
			public void onCancelChat()
			{
				ChatTextField.gI().ResetTF();
			}
		}
	}
}
