using System;

// Token: 0x020000AA RID: 170
public class SoundMn
{
	// Token: 0x06000916 RID: 2326 RVA: 0x000828F6 File Offset: 0x00080AF6
	public static void init(SoundMn.AssetManager ac)
	{
		Sound.setActivity(ac);
	}

	// Token: 0x06000917 RID: 2327 RVA: 0x000828FE File Offset: 0x00080AFE
	public static SoundMn gI()
	{
		if (SoundMn.gIz == null)
		{
			SoundMn.gIz = new SoundMn();
		}
		return SoundMn.gIz;
	}

	// Token: 0x06000918 RID: 2328 RVA: 0x00082918 File Offset: 0x00080B18
	public void loadSound(int mapID)
	{
		Sound.init(new int[]
		{
			SoundMn.AIR_SHIP,
			SoundMn.RAIN,
			SoundMn.TAITAONANGLUONG
		}, new int[]
		{
			SoundMn.GET_ITEM,
			SoundMn.MOVE,
			SoundMn.LOW_PUNCH,
			SoundMn.LOW_KICK,
			SoundMn.FLY,
			SoundMn.JUMP,
			SoundMn.PANEL_OPEN,
			SoundMn.BUTTON_CLOSE,
			SoundMn.BUTTON_CLICK,
			SoundMn.MEDIUM_PUNCH,
			SoundMn.MEDIUM_KICK,
			SoundMn.PANEL_OPEN,
			SoundMn.EAT_PEAN,
			SoundMn.OPEN_DIALOG,
			SoundMn.NORMAL_KAME,
			SoundMn.NAMEK_KAME,
			SoundMn.XAYDA_KAME,
			SoundMn.EXPLODE_1,
			SoundMn.EXPLODE_2,
			SoundMn.TRAIDAT_KAME,
			SoundMn.HP_UP,
			SoundMn.THAIDUONGHASAN,
			SoundMn.HOISINH,
			SoundMn.GONG,
			SoundMn.KHICHAY,
			SoundMn.BIG_EXPLODE,
			SoundMn.NAMEK_LAZER,
			SoundMn.NAMEK_CHARGE,
			SoundMn.RADAR_CLICK,
			SoundMn.RADAR_ITEM,
			SoundMn.FIREWORK,
			SoundMn.KAMEX10_0,
			SoundMn.KAMEX10_1,
			SoundMn.DESTROY_0,
			SoundMn.DESTROY_1,
			SoundMn.MAFUBA_0,
			SoundMn.MAFUBA_1,
			SoundMn.MAFUBA_2,
			SoundMn.DESTROY_2
		});
	}

	// Token: 0x06000919 RID: 2329 RVA: 0x00082AA8 File Offset: 0x00080CA8
	public void getSoundOption()
	{
		if (GameCanvas.loginScr.isLogin2 && global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 2)
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
				mResources.change_account,
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
					mResources.change_account,
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
				mResources.change_account
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
					mResources.change_account
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
	}

	// Token: 0x0600091A RID: 2330 RVA: 0x00082CC4 File Offset: 0x00080EC4
	public void getStrOption()
	{
		string text = "[x]   ";
		string text2 = "[  ]   ";
		if (Main.isPC)
		{
			Panel.strCauhinh = new string[]
			{
				(!global::Char.isPaintAura) ? (text2 + mResources.aura_off.Trim()) : (text + mResources.aura_off.Trim()),
				(!global::Char.isPaintAura2) ? (text2 + mResources.aura_off_2.Trim()) : (text + mResources.aura_off_2.Trim()),
				(!GameCanvas.isPlaySound) ? (text2 + mResources.turnOffSound) : (text + mResources.turnOffSound),
				(mGraphics.zoomLevel <= 1) ? (text2 + mResources.x2Screen) : (text + mResources.x1Screen)
			};
			return;
		}
		string text3 = ((GameScr.isAnalog != 0) ? (text + mResources.turnOffAnalog) : (text2 + mResources.turnOnAnalog));
		if (!GameCanvas.isTouch)
		{
			text3 = (GameScr.isPaintChatVip ? (text + mResources.serverchat_off) : (text2 + mResources.serverchat_off));
		}
		Panel.strCauhinh = new string[]
		{
			(!global::Char.isPaintAura) ? (text2 + mResources.aura_off.Trim()) : (text + mResources.aura_off.Trim()),
			(!global::Char.isPaintAura2) ? (text2 + mResources.aura_off_2.Trim()) : (text + mResources.aura_off_2.Trim()),
			(!GameCanvas.isPlaySound) ? (text2 + mResources.turnOffSound) : (text + mResources.turnOffSound),
			(!GameCanvas.lowGraphic) ? (text2 + mResources.cauhinhthap) : (text + mResources.cauhinhthap),
			text3
		};
	}

	// Token: 0x0600091B RID: 2331 RVA: 0x00082E82 File Offset: 0x00081082
	public void HP_MPup()
	{
		Sound.playSound(SoundMn.HP_UP, 0.5f);
	}

	// Token: 0x0600091C RID: 2332 RVA: 0x00082E94 File Offset: 0x00081094
	public void charPunch(bool isKick, float volumn)
	{
		if (!global::Char.myCharz().me)
		{
			SoundMn.volume /= 2f;
		}
		if (volumn <= 0f)
		{
			volumn = 0.01f;
		}
		int num = Res.random(0, 3);
		if (isKick)
		{
			Sound.playSound((num != 0) ? SoundMn.MEDIUM_KICK : SoundMn.LOW_KICK, 0.1f);
		}
		else
		{
			Sound.playSound((num != 0) ? SoundMn.MEDIUM_PUNCH : SoundMn.LOW_PUNCH, 0.1f);
		}
		this.poolCount++;
	}

	// Token: 0x0600091D RID: 2333 RVA: 0x00082F19 File Offset: 0x00081119
	public void thaiduonghasan()
	{
		Sound.playSound(SoundMn.THAIDUONGHASAN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600091E RID: 2334 RVA: 0x00082F38 File Offset: 0x00081138
	public void rain()
	{
		Sound.playMus(SoundMn.RAIN, 0.3f, true);
	}

	// Token: 0x0600091F RID: 2335 RVA: 0x00082F4A File Offset: 0x0008114A
	public void gongName()
	{
		Sound.playSound(SoundMn.NAMEK_CHARGE, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000920 RID: 2336 RVA: 0x00082F69 File Offset: 0x00081169
	public void gong()
	{
		Sound.playSound(SoundMn.GONG, 0.2f);
		this.poolCount++;
	}

	// Token: 0x06000921 RID: 2337 RVA: 0x00082F88 File Offset: 0x00081188
	public void getItem()
	{
		Sound.playSound(SoundMn.GET_ITEM, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000922 RID: 2338 RVA: 0x00082FA8 File Offset: 0x000811A8
	public void soundToolOption()
	{
		GameCanvas.isPlaySound = !GameCanvas.isPlaySound;
		if (GameCanvas.isPlaySound)
		{
			SoundMn.gI().loadSound(TileMap.mapID);
			Rms.saveRMSInt("isPlaySound", 1);
		}
		else
		{
			SoundMn.gI().closeSound();
			Rms.saveRMSInt("isPlaySound", 0);
		}
		this.getStrOption();
	}

	// Token: 0x06000923 RID: 2339 RVA: 0x00083000 File Offset: 0x00081200
	public void chatVipToolOption()
	{
		GameScr.isPaintChatVip = !GameScr.isPaintChatVip;
		if (GameScr.isPaintChatVip)
		{
			Rms.saveRMSInt("serverchat", 0);
		}
		else
		{
			Rms.saveRMSInt("serverchat", 1);
		}
		this.getStrOption();
	}

	// Token: 0x06000924 RID: 2340 RVA: 0x00083034 File Offset: 0x00081234
	public void analogToolOption()
	{
		if (GameScr.isAnalog == 0)
		{
			GameScr.isAnalog = 1;
			Rms.saveRMSInt("analog", GameScr.isAnalog);
			GameScr.setSkillBarPosition();
		}
		else
		{
			GameScr.isAnalog = 0;
			Rms.saveRMSInt("analog", GameScr.isAnalog);
			GameScr.setSkillBarPosition();
		}
		this.getStrOption();
	}

	// Token: 0x06000925 RID: 2341 RVA: 0x00083084 File Offset: 0x00081284
	public void CaseAnalog()
	{
		if (!Main.isPC)
		{
			if (!GameCanvas.isTouch)
			{
				this.chatVipToolOption();
				return;
			}
			this.analogToolOption();
		}
	}

	// Token: 0x06000926 RID: 2342 RVA: 0x000830A4 File Offset: 0x000812A4
	public void CaseSizeScr()
	{
		if (GameCanvas.lowGraphic)
		{
			Rms.saveRMSInt("lowGraphic", 0);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		else
		{
			Rms.saveRMSInt("lowGraphic", 1);
			GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
		}
		this.getStrOption();
	}

	// Token: 0x06000927 RID: 2343 RVA: 0x000830F6 File Offset: 0x000812F6
	public void AuraToolOption()
	{
		if (global::Char.isPaintAura)
		{
			Rms.saveRMSInt("isPaintAura", 0);
			global::Char.isPaintAura = false;
		}
		else
		{
			Rms.saveRMSInt("isPaintAura", 1);
			global::Char.isPaintAura = true;
		}
		this.getStrOption();
	}

	// Token: 0x06000928 RID: 2344 RVA: 0x00083129 File Offset: 0x00081329
	public void AuraToolOption2()
	{
		if (global::Char.isPaintAura2)
		{
			Rms.saveRMSInt("isPaintAura2", 0);
			global::Char.isPaintAura2 = false;
		}
		else
		{
			Rms.saveRMSInt("isPaintAura2", 1);
			global::Char.isPaintAura2 = true;
		}
		this.getStrOption();
	}

	// Token: 0x06000929 RID: 2345 RVA: 0x0008315C File Offset: 0x0008135C
	public void HatToolOption()
	{
		Service.gI().sendOptHat(0);
	}

	// Token: 0x0600092A RID: 2346 RVA: 0x00004887 File Offset: 0x00002A87
	public void update()
	{
	}

	// Token: 0x0600092B RID: 2347 RVA: 0x00083169 File Offset: 0x00081369
	public void closeSound()
	{
		Sound.stopAll = true;
		this.stopAll();
	}

	// Token: 0x0600092C RID: 2348 RVA: 0x00083177 File Offset: 0x00081377
	public void openSound()
	{
		if (Sound.music == null)
		{
			this.loadSound(0);
		}
		Sound.stopAll = false;
	}

	// Token: 0x0600092D RID: 2349 RVA: 0x0008318D File Offset: 0x0008138D
	public void bigeExlode()
	{
		Sound.playSound(SoundMn.BIG_EXPLODE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600092E RID: 2350 RVA: 0x000831AC File Offset: 0x000813AC
	public void explode_1()
	{
		Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600092F RID: 2351 RVA: 0x000831AC File Offset: 0x000813AC
	public void explode_2()
	{
		Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000930 RID: 2352 RVA: 0x000831CB File Offset: 0x000813CB
	public void traidatKame()
	{
		Sound.playSound(SoundMn.TRAIDAT_KAME, 1f);
		this.poolCount++;
	}

	// Token: 0x06000931 RID: 2353 RVA: 0x000831EA File Offset: 0x000813EA
	public void namekKame()
	{
		Sound.playSound(SoundMn.NAMEK_KAME, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000932 RID: 2354 RVA: 0x00083209 File Offset: 0x00081409
	public void nameLazer()
	{
		Sound.playSound(SoundMn.NAMEK_LAZER, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000933 RID: 2355 RVA: 0x00083228 File Offset: 0x00081428
	public void xaydaKame()
	{
		Sound.playSound(SoundMn.XAYDA_KAME, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000934 RID: 2356 RVA: 0x00083248 File Offset: 0x00081448
	public void mobKame(int type)
	{
		int num = SoundMn.XAYDA_KAME;
		if (type == 13)
		{
			num = SoundMn.NORMAL_KAME;
		}
		Sound.playSound(num, 0.1f);
		this.poolCount++;
	}

	// Token: 0x06000935 RID: 2357 RVA: 0x00083280 File Offset: 0x00081480
	public void charRun(float volumn)
	{
		if (!global::Char.myCharz().me)
		{
			SoundMn.volume /= 2f;
			if (volumn <= 0f)
			{
				volumn = 0.01f;
			}
		}
		if (GameCanvas.gameTick % 8 == 0)
		{
			Sound.playSound(SoundMn.MOVE, volumn);
			this.poolCount++;
		}
	}

	// Token: 0x06000936 RID: 2358 RVA: 0x000832DA File Offset: 0x000814DA
	public void monkeyRun(float volumn)
	{
		if (GameCanvas.gameTick % 8 == 0)
		{
			Sound.playSound(SoundMn.KHICHAY, 0.2f);
			this.poolCount++;
		}
	}

	// Token: 0x06000937 RID: 2359 RVA: 0x00083302 File Offset: 0x00081502
	public void charFall()
	{
		Sound.playSound(SoundMn.MOVE, 0.1f);
		this.poolCount++;
	}

	// Token: 0x06000938 RID: 2360 RVA: 0x00083321 File Offset: 0x00081521
	public void charJump()
	{
		Sound.playSound(SoundMn.MOVE, 0.2f);
		this.poolCount++;
	}

	// Token: 0x06000939 RID: 2361 RVA: 0x00083340 File Offset: 0x00081540
	public void panelOpen()
	{
		Sound.playSound(SoundMn.PANEL_OPEN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600093A RID: 2362 RVA: 0x0008335F File Offset: 0x0008155F
	public void buttonClose()
	{
		Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600093B RID: 2363 RVA: 0x0008337E File Offset: 0x0008157E
	public void buttonClick()
	{
		Sound.playSound(SoundMn.BUTTON_CLICK, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600093C RID: 2364 RVA: 0x00004887 File Offset: 0x00002A87
	public void stopMove()
	{
	}

	// Token: 0x0600093D RID: 2365 RVA: 0x0008339D File Offset: 0x0008159D
	public void charFly()
	{
		Sound.playSound(SoundMn.FLY, 0.2f);
		this.poolCount++;
	}

	// Token: 0x0600093E RID: 2366 RVA: 0x00004887 File Offset: 0x00002A87
	public void stopFly()
	{
	}

	// Token: 0x0600093F RID: 2367 RVA: 0x0008335F File Offset: 0x0008155F
	public void openMenu()
	{
		Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000940 RID: 2368 RVA: 0x000833BC File Offset: 0x000815BC
	public void panelClick()
	{
		Sound.playSound(SoundMn.PANEL_CLICK, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000941 RID: 2369 RVA: 0x000833DB File Offset: 0x000815DB
	public void eatPeans()
	{
		Sound.playSound(SoundMn.EAT_PEAN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000942 RID: 2370 RVA: 0x000833FA File Offset: 0x000815FA
	public void openDialog()
	{
		Sound.playSound(SoundMn.OPEN_DIALOG, 0.5f);
	}

	// Token: 0x06000943 RID: 2371 RVA: 0x0008340B File Offset: 0x0008160B
	public void hoisinh()
	{
		Sound.playSound(SoundMn.HOISINH, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000944 RID: 2372 RVA: 0x0008342A File Offset: 0x0008162A
	public void taitao()
	{
		Sound.playMus(SoundMn.TAITAONANGLUONG, 0.5f, true);
	}

	// Token: 0x06000945 RID: 2373 RVA: 0x00004887 File Offset: 0x00002A87
	public void taitaoPause()
	{
	}

	// Token: 0x06000946 RID: 2374 RVA: 0x0008343C File Offset: 0x0008163C
	public bool isPlayRain()
	{
		bool flag;
		try
		{
			flag = Sound.isPlayingSound();
		}
		catch (Exception)
		{
			flag = false;
		}
		return flag;
	}

	// Token: 0x06000947 RID: 2375 RVA: 0x000151BF File Offset: 0x000133BF
	public bool isPlayAirShip()
	{
		return false;
	}

	// Token: 0x06000948 RID: 2376 RVA: 0x00083468 File Offset: 0x00081668
	public void airShip()
	{
		SoundMn.cout++;
		if (SoundMn.cout % 2 == 0)
		{
			Sound.playMus(SoundMn.AIR_SHIP, 0.3f, false);
		}
	}

	// Token: 0x06000949 RID: 2377 RVA: 0x00004887 File Offset: 0x00002A87
	public void pauseAirShip()
	{
	}

	// Token: 0x0600094A RID: 2378 RVA: 0x00004887 File Offset: 0x00002A87
	public void resumeAirShip()
	{
	}

	// Token: 0x0600094B RID: 2379 RVA: 0x0008348F File Offset: 0x0008168F
	public void stopAll()
	{
		Sound.stopAllz();
	}

	// Token: 0x0600094C RID: 2380 RVA: 0x00083496 File Offset: 0x00081696
	public void backToRegister()
	{
		Session_ME.gI().close();
		GameCanvas.panel.hide();
		GameCanvas.loginScr.actRegister();
		GameCanvas.loginScr.switchToMe();
	}

	// Token: 0x0600094D RID: 2381 RVA: 0x000834C0 File Offset: 0x000816C0
	public void newKame()
	{
		this.poolCount++;
		if (this.poolCount % 15 == 0)
		{
			Sound.playSound(SoundMn.TRAIDAT_KAME, 0.5f);
		}
	}

	// Token: 0x0600094E RID: 2382 RVA: 0x000834EA File Offset: 0x000816EA
	public void radarClick()
	{
		Sound.playSound(SoundMn.RADAR_CLICK, 0.5f);
	}

	// Token: 0x0600094F RID: 2383 RVA: 0x000834FB File Offset: 0x000816FB
	public void radarItem()
	{
		Sound.playSound(SoundMn.RADAR_ITEM, 0.5f);
	}

	// Token: 0x06000950 RID: 2384 RVA: 0x0008350C File Offset: 0x0008170C
	public static void playSound(int x, int y, int id, float volume)
	{
		Sound.playSound(id, volume);
	}

	// Token: 0x04000FE8 RID: 4072
	public static bool IsDelAcc;

	// Token: 0x04000FE9 RID: 4073
	public static SoundMn gIz;

	// Token: 0x04000FEA RID: 4074
	public static bool isSound = true;

	// Token: 0x04000FEB RID: 4075
	public static float volume = 0.5f;

	// Token: 0x04000FEC RID: 4076
	internal static int MAX_VOLUME = 10;

	// Token: 0x04000FED RID: 4077
	public static SoundMn.MediaPlayer[] music;

	// Token: 0x04000FEE RID: 4078
	public static SoundMn.SoundPool[] sound;

	// Token: 0x04000FEF RID: 4079
	public static int[] soundID;

	// Token: 0x04000FF0 RID: 4080
	public static int AIR_SHIP;

	// Token: 0x04000FF1 RID: 4081
	public static int RAIN = 1;

	// Token: 0x04000FF2 RID: 4082
	public static int TAITAONANGLUONG = 2;

	// Token: 0x04000FF3 RID: 4083
	public static int GET_ITEM;

	// Token: 0x04000FF4 RID: 4084
	public static int MOVE = 1;

	// Token: 0x04000FF5 RID: 4085
	public static int LOW_PUNCH = 2;

	// Token: 0x04000FF6 RID: 4086
	public static int LOW_KICK = 3;

	// Token: 0x04000FF7 RID: 4087
	public static int FLY = 4;

	// Token: 0x04000FF8 RID: 4088
	public static int JUMP = 5;

	// Token: 0x04000FF9 RID: 4089
	public static int PANEL_OPEN = 6;

	// Token: 0x04000FFA RID: 4090
	public static int BUTTON_CLOSE = 7;

	// Token: 0x04000FFB RID: 4091
	public static int BUTTON_CLICK = 8;

	// Token: 0x04000FFC RID: 4092
	public static int MEDIUM_PUNCH = 9;

	// Token: 0x04000FFD RID: 4093
	public static int MEDIUM_KICK = 10;

	// Token: 0x04000FFE RID: 4094
	public static int PANEL_CLICK = 11;

	// Token: 0x04000FFF RID: 4095
	public static int EAT_PEAN = 12;

	// Token: 0x04001000 RID: 4096
	public static int OPEN_DIALOG = 13;

	// Token: 0x04001001 RID: 4097
	public static int NORMAL_KAME = 14;

	// Token: 0x04001002 RID: 4098
	public static int NAMEK_KAME = 15;

	// Token: 0x04001003 RID: 4099
	public static int XAYDA_KAME = 16;

	// Token: 0x04001004 RID: 4100
	public static int EXPLODE_1 = 17;

	// Token: 0x04001005 RID: 4101
	public static int EXPLODE_2 = 18;

	// Token: 0x04001006 RID: 4102
	public static int TRAIDAT_KAME = 19;

	// Token: 0x04001007 RID: 4103
	public static int HP_UP = 20;

	// Token: 0x04001008 RID: 4104
	public static int THAIDUONGHASAN = 21;

	// Token: 0x04001009 RID: 4105
	public static int HOISINH = 22;

	// Token: 0x0400100A RID: 4106
	public static int GONG = 23;

	// Token: 0x0400100B RID: 4107
	public static int KHICHAY = 24;

	// Token: 0x0400100C RID: 4108
	public static int BIG_EXPLODE = 25;

	// Token: 0x0400100D RID: 4109
	public static int NAMEK_LAZER = 26;

	// Token: 0x0400100E RID: 4110
	public static int NAMEK_CHARGE = 27;

	// Token: 0x0400100F RID: 4111
	public static int RADAR_CLICK = 28;

	// Token: 0x04001010 RID: 4112
	public static int RADAR_ITEM = 29;

	// Token: 0x04001011 RID: 4113
	public static int FIREWORK = 30;

	// Token: 0x04001012 RID: 4114
	public static int KAMEX10_0 = 31;

	// Token: 0x04001013 RID: 4115
	public static int KAMEX10_1 = 32;

	// Token: 0x04001014 RID: 4116
	public static int DESTROY_0 = 33;

	// Token: 0x04001015 RID: 4117
	public static int DESTROY_1 = 34;

	// Token: 0x04001016 RID: 4118
	public static int MAFUBA_0 = 35;

	// Token: 0x04001017 RID: 4119
	public static int MAFUBA_1 = 36;

	// Token: 0x04001018 RID: 4120
	public static int MAFUBA_2 = 37;

	// Token: 0x04001019 RID: 4121
	public static int DESTROY_2 = 38;

	// Token: 0x0400101A RID: 4122
	public bool freePool;

	// Token: 0x0400101B RID: 4123
	public int poolCount;

	// Token: 0x0400101C RID: 4124
	public static int cout = 1;

	// Token: 0x020000AB RID: 171
	public class MediaPlayer
	{
	}

	// Token: 0x020000AC RID: 172
	public class SoundPool
	{
	}

	// Token: 0x020000AD RID: 173
	public class AssetManager
	{
	}
}
