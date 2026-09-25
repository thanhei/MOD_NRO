using System;
using System.Net.NetworkInformation;
using System.Threading;
using Mod;
using UnityEngine;

// Token: 0x02000065 RID: 101
public class Main : MonoBehaviour
{
	// Token: 0x0600051B RID: 1307 RVA: 0x00051B2C File Offset: 0x0004FD2C
	internal void Start()
	{
		if (Main.started)
		{
			return;
		}
		if (Thread.CurrentThread.Name != "Main")
		{
			Thread.CurrentThread.Name = "Main";
		}
		Main.mainThreadName = Thread.CurrentThread.Name;
		Main.isPC = true;
		Main.started = true;
	}

	// Token: 0x0600051C RID: 1308 RVA: 0x00051B81 File Offset: 0x0004FD81
	internal void SetInit()
	{
		base.enabled = true;
	}

	// Token: 0x0600051D RID: 1309 RVA: 0x00051B8A File Offset: 0x0004FD8A
	internal void OnHideUnity(bool isGameShown)
	{
		if (!isGameShown)
		{
			Time.timeScale = 0f;
			return;
		}
		Time.timeScale = 1f;
	}

	// Token: 0x0600051E RID: 1310 RVA: 0x00051BA4 File Offset: 0x0004FDA4
	internal void OnGUI()
	{
		if (this.count >= 10)
		{
			if (this.fps == 0)
			{
				this.timefps = mSystem.currentTimeMillis();
			}
			else if (mSystem.currentTimeMillis() - this.timefps > 1000L)
			{
				this.max = this.fps;
				this.fps = 0;
				this.timefps = mSystem.currentTimeMillis();
			}
			this.fps++;
			this.checkInput();
			Session_ME.update();
			Session_ME2.update();
			if (Event.current.type.Equals(EventType.Repaint) && this.paintCount <= this.updateCount)
			{
				GameMidlet.gameCanvas.paint(Main.g);
				this.paintCount++;
				Main.g.reset();
			}
		}
	}

	// Token: 0x0600051F RID: 1311 RVA: 0x00051C78 File Offset: 0x0004FE78
	public void setsizeChange()
	{
		if (!this.isRun)
		{
			Application.runInBackground = true;
			base.useGUILayout = false;
			Main.isCompactDevice = Main.detectCompactDevice();
			if (Main.main == null)
			{
				Main.main = this;
			}
			this.isRun = true;
			ScaleGUI.initScaleGUI();
			if (Main.isPC)
			{
				Main.IMEI = SystemInfo.deviceUniqueIdentifier;
			}
			else
			{
				Main.IMEI = this.GetMacAddress();
			}
			Main.isPC = true;
			if (Main.isWindowsPhone)
			{
				Main.typeClient = 6;
			}
			if (Main.isPC)
			{
				Main.typeClient = 4;
			}
			if (Main.IphoneVersionApp)
			{
				Main.typeClient = 5;
			}
			if (iPhoneSettings.generation == iPhoneGeneration.iPodTouch4Gen)
			{
				Main.isIpod = true;
			}
			if (iPhoneSettings.generation == iPhoneGeneration.iPhone4)
			{
				Main.isIphone4 = true;
			}
			Main.g = new mGraphics();
			Main.midlet = new GameMidlet();
			TileMap.loadBg();
			Paint.loadbg();
			PopUp.loadBg();
			GameScr.loadBg();
			InfoMe.gI().loadCharId();
			Panel.loadBg();
			Menu.loadBg();
			Key.mapKeyPC();
			SoundMn.gI().loadSound(TileMap.mapID);
		}
	}

	// Token: 0x06000520 RID: 1312 RVA: 0x00004887 File Offset: 0x00002A87
	public static void setBackupIcloud(string path)
	{
	}

	// Token: 0x06000521 RID: 1313 RVA: 0x00051D80 File Offset: 0x0004FF80
	public string GetMacAddress()
	{
		string empty = string.Empty;
		NetworkInterface[] allNetworkInterfaces = NetworkInterface.GetAllNetworkInterfaces();
		for (int i = 0; i < allNetworkInterfaces.Length; i++)
		{
			PhysicalAddress physicalAddress = allNetworkInterfaces[i].GetPhysicalAddress();
			if (physicalAddress.ToString() != string.Empty)
			{
				return physicalAddress.ToString();
			}
		}
		return string.Empty;
	}

	// Token: 0x06000522 RID: 1314 RVA: 0x00051DCE File Offset: 0x0004FFCE
	public void doClearRMS()
	{
		if (Main.isPC && Rms.loadRMSInt("lastZoomlevel") != mGraphics.zoomLevel)
		{
			Rms.clearAll();
			Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
			Rms.saveRMSInt("levelScreenKN", this.level);
		}
	}

	// Token: 0x06000523 RID: 1315 RVA: 0x00051E0C File Offset: 0x0005000C
	public static void closeKeyBoard()
	{
		if (TouchScreenKeyboard.visible)
		{
			TField.kb.active = false;
			TField.kb = null;
		}
	}

	// Token: 0x06000524 RID: 1316 RVA: 0x00051E28 File Offset: 0x00050028
	internal void FixedUpdate()
	{
		Rms.update();
		this.count++;
		if (this.count >= 10)
		{
			if (this.up == 0)
			{
				this.timeup = mSystem.currentTimeMillis();
			}
			else if (mSystem.currentTimeMillis() - this.timeup > 1000L)
			{
				this.upmax = this.up;
				this.up = 0;
				this.timeup = mSystem.currentTimeMillis();
			}
			this.up++;
			this.setsizeChange();
			this.updateCount++;
			GameMidlet.gameCanvas.update();
			Image.update();
			DataInputStream.update();
			Net.update();
			Main.f++;
			if (Main.f > 8)
			{
				Main.f = 0;
			}
			if (!Main.isPC)
			{
				int num = 1 / Main.a;
			}
		}
	}

	// Token: 0x06000525 RID: 1317 RVA: 0x00051F00 File Offset: 0x00050100
	internal void Update()
	{
		Res.outz("Some dummy code here");
	}

	// Token: 0x06000526 RID: 1318 RVA: 0x00051F0C File Offset: 0x0005010C
	internal void checkInput()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePosition = Input.mousePosition;
			GameMidlet.gameCanvas.pointerPressed((int)(mousePosition.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			this.lastMousePos.x = mousePosition.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
		}
		if (Input.GetMouseButton(0))
		{
			Vector3 mousePosition2 = Input.mousePosition;
			GameMidlet.gameCanvas.pointerDragged((int)(mousePosition2.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition2.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
			this.lastMousePos.x = mousePosition2.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition2.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
		}
		if (Input.GetMouseButtonUp(0))
		{
			Vector3 mousePosition3 = Input.mousePosition;
			this.lastMousePos.x = mousePosition3.x / (float)mGraphics.zoomLevel;
			this.lastMousePos.y = mousePosition3.y / (float)mGraphics.zoomLevel + (float)mGraphics.addYWhenOpenKeyBoard;
			GameMidlet.gameCanvas.pointerReleased((int)(mousePosition3.x / (float)mGraphics.zoomLevel), (int)(((float)Screen.height - mousePosition3.y) / (float)mGraphics.zoomLevel) + mGraphics.addYWhenOpenKeyBoard);
		}
		if (TField.currentTField != null && TField.currentTField.isFocus)
		{
			TField.currentTField.HandleInputText();
		}
		if (Input.anyKeyDown && Event.current.type == EventType.KeyDown)
		{
			int num = MyKeyMap.map(Event.current.keyCode);
			if (num == -30)
			{
				Utils.CheckBackButtonPress();
			}
			if (num != 0)
			{
				GameMidlet.gameCanvas.keyPressedz(num);
			}
		}
		if (Event.current.type == EventType.KeyUp)
		{
			int num2 = MyKeyMap.map(Event.current.keyCode);
			if (num2 != 0)
			{
				GameMidlet.gameCanvas.keyReleasedz(num2);
			}
		}
		GameMidlet.gameCanvas.scrollMouse((int)(Input.GetAxis("Mouse ScrollWheel") * 10f));
		int x = (int)Input.mousePosition.x;
		float y = Input.mousePosition.y;
		int num3 = x / mGraphics.zoomLevel;
		int num4 = (Screen.height - (int)y) / mGraphics.zoomLevel;
		GameMidlet.gameCanvas.pointerMouse(num3, num4);
	}

	// Token: 0x06000527 RID: 1319 RVA: 0x00052169 File Offset: 0x00050369
	internal void OnApplicationQuit()
	{
		GameCanvas.bRun = false;
		Session_ME.gI().close();
		Session_ME2.gI().close();
		if (Main.isPC)
		{
			Application.Quit();
		}
	}

	// Token: 0x06000528 RID: 1320 RVA: 0x00052191 File Offset: 0x00050391
	internal void OnApplicationPause(bool paused)
	{
		Main.isResume = false;
		if (!paused)
		{
			Main.isResume = true;
		}
		if (TouchScreenKeyboard.visible)
		{
			TField.kb.active = false;
			TField.kb = null;
		}
	}

	// Token: 0x06000529 RID: 1321 RVA: 0x000521BA File Offset: 0x000503BA
	public static void exit()
	{
		Main.main.OnApplicationQuit();
	}

	// Token: 0x0600052A RID: 1322 RVA: 0x000521C6 File Offset: 0x000503C6
	public static bool detectCompactDevice()
	{
		return iPhoneSettings.generation != iPhoneGeneration.iPhone && iPhoneSettings.generation != iPhoneGeneration.iPhone3G && iPhoneSettings.generation != iPhoneGeneration.iPodTouch1Gen && iPhoneSettings.generation != iPhoneGeneration.iPodTouch2Gen;
	}

	// Token: 0x0600052B RID: 1323 RVA: 0x000521EB File Offset: 0x000503EB
	public static bool checkCanSendSMS()
	{
		return iPhoneSettings.generation == iPhoneGeneration.iPhone3GS || iPhoneSettings.generation == iPhoneGeneration.iPhone4 || iPhoneSettings.generation > iPhoneGeneration.iPodTouch4Gen;
	}

	// Token: 0x04000ACC RID: 2764
	public static Main main;

	// Token: 0x04000ACD RID: 2765
	public static mGraphics g;

	// Token: 0x04000ACE RID: 2766
	public static GameMidlet midlet;

	// Token: 0x04000ACF RID: 2767
	public static string res = "res";

	// Token: 0x04000AD0 RID: 2768
	public static string mainThreadName;

	// Token: 0x04000AD1 RID: 2769
	public static bool started;

	// Token: 0x04000AD2 RID: 2770
	public static bool isIpod;

	// Token: 0x04000AD3 RID: 2771
	public static bool isIphone4;

	// Token: 0x04000AD4 RID: 2772
	public static bool isPC;

	// Token: 0x04000AD5 RID: 2773
	public static bool isWindowsPhone;

	// Token: 0x04000AD6 RID: 2774
	public static bool isIPhone;

	// Token: 0x04000AD7 RID: 2775
	public static bool IphoneVersionApp;

	// Token: 0x04000AD8 RID: 2776
	public static string IMEI;

	// Token: 0x04000AD9 RID: 2777
	public static int versionIp;

	// Token: 0x04000ADA RID: 2778
	public static int numberQuit = 1;

	// Token: 0x04000ADB RID: 2779
	public static int typeClient = 4;

	// Token: 0x04000ADC RID: 2780
	public const sbyte PC_VERSION = 4;

	// Token: 0x04000ADD RID: 2781
	public const sbyte IP_APPSTORE = 5;

	// Token: 0x04000ADE RID: 2782
	public const sbyte WINDOWSPHONE = 6;

	// Token: 0x04000ADF RID: 2783
	internal int level;

	// Token: 0x04000AE0 RID: 2784
	public const sbyte IP_JB = 3;

	// Token: 0x04000AE1 RID: 2785
	internal int updateCount;

	// Token: 0x04000AE2 RID: 2786
	internal int paintCount;

	// Token: 0x04000AE3 RID: 2787
	internal int count;

	// Token: 0x04000AE4 RID: 2788
	internal int fps;

	// Token: 0x04000AE5 RID: 2789
	internal int max;

	// Token: 0x04000AE6 RID: 2790
	internal int up;

	// Token: 0x04000AE7 RID: 2791
	internal int upmax;

	// Token: 0x04000AE8 RID: 2792
	internal long timefps;

	// Token: 0x04000AE9 RID: 2793
	internal long timeup;

	// Token: 0x04000AEA RID: 2794
	internal bool isRun;

	// Token: 0x04000AEB RID: 2795
	public static int waitTick;

	// Token: 0x04000AEC RID: 2796
	public static int f;

	// Token: 0x04000AED RID: 2797
	public static bool isResume;

	// Token: 0x04000AEE RID: 2798
	public static bool isMiniApp = true;

	// Token: 0x04000AEF RID: 2799
	public static bool isQuitApp;

	// Token: 0x04000AF0 RID: 2800
	internal Vector2 lastMousePos;

	// Token: 0x04000AF1 RID: 2801
	public static int a = 1;

	// Token: 0x04000AF2 RID: 2802
	public static bool isCompactDevice = true;
}
