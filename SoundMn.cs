using System;

// Token: 0x0200008A RID: 138
public class SoundMn
{
	// Token: 0x06000475 RID: 1141 RVA: 0x00006670 File Offset: 0x00004870
	public static void init(SoundMn.AssetManager ac)
	{
		Sound.setActivity(ac);
	}

	// Token: 0x06000476 RID: 1142 RVA: 0x00006678 File Offset: 0x00004878
	public static SoundMn gI()
	{
		if (SoundMn.gIz == null)
		{
			SoundMn.gIz = new SoundMn();
		}
		return SoundMn.gIz;
	}

	// Token: 0x06000477 RID: 1143 RVA: 0x00029F60 File Offset: 0x00028160
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

	// Token: 0x06000478 RID: 1144 RVA: 0x0002A0F0 File Offset: 0x000282F0
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

	// Token: 0x06000479 RID: 1145 RVA: 0x0002A318 File Offset: 0x00028518
	public void getStrOption()
	{
		string str = "[x]   ";
		string str2 = "[  ]   ";
		if (Main.isPC)
		{
			Panel.strCauhinh = new string[]
			{
				(!global::Char.isPaintAura) ? (str2 + mResources.aura_off.Trim()) : (str + mResources.aura_off.Trim()),
				(!global::Char.isPaintAura2) ? (str2 + mResources.aura_off_2.Trim()) : (str + mResources.aura_off_2.Trim()),
				(!GameCanvas.isPlaySound) ? (str2 + mResources.turnOffSound) : (str + mResources.turnOffSound),
				(mGraphics.zoomLevel <= 1) ? (str2 + mResources.x2Screen) : (str + mResources.x1Screen)
			};
		}
		else
		{
			string text = (GameScr.isAnalog != 0) ? (str + mResources.turnOffAnalog) : (str2 + mResources.turnOnAnalog);
			if (!GameCanvas.isTouch)
			{
				text = (GameScr.isPaintChatVip ? (str + mResources.serverchat_off) : (str2 + mResources.serverchat_off));
			}
			Panel.strCauhinh = new string[]
			{
				(!global::Char.isPaintAura) ? (str2 + mResources.aura_off.Trim()) : (str + mResources.aura_off.Trim()),
				(!global::Char.isPaintAura2) ? (str2 + mResources.aura_off_2.Trim()) : (str + mResources.aura_off_2.Trim()),
				(!GameCanvas.isPlaySound) ? (str2 + mResources.turnOffSound) : (str + mResources.turnOffSound),
				(!GameCanvas.lowGraphic) ? (str2 + mResources.cauhinhthap) : (str + mResources.cauhinhthap),
				text
			};
		}
	}

	// Token: 0x0600047A RID: 1146 RVA: 0x00006693 File Offset: 0x00004893
	public void HP_MPup()
	{
		Sound.playSound(SoundMn.HP_UP, 0.5f);
	}

	// Token: 0x0600047B RID: 1147 RVA: 0x0002A51C File Offset: 0x0002871C
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

	// Token: 0x0600047C RID: 1148 RVA: 0x000066A4 File Offset: 0x000048A4
	public void thaiduonghasan()
	{
		Sound.playSound(SoundMn.THAIDUONGHASAN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600047D RID: 1149 RVA: 0x000066C3 File Offset: 0x000048C3
	public void rain()
	{
		Sound.playMus(SoundMn.RAIN, 0.3f, true);
	}

	// Token: 0x0600047E RID: 1150 RVA: 0x000066D5 File Offset: 0x000048D5
	public void gongName()
	{
		Sound.playSound(SoundMn.NAMEK_CHARGE, 0.3f);
		this.poolCount++;
	}

	// Token: 0x0600047F RID: 1151 RVA: 0x000066F4 File Offset: 0x000048F4
	public void gong()
	{
		Sound.playSound(SoundMn.GONG, 0.2f);
		this.poolCount++;
	}

	// Token: 0x06000480 RID: 1152 RVA: 0x00006713 File Offset: 0x00004913
	public void getItem()
	{
		Sound.playSound(SoundMn.GET_ITEM, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000481 RID: 1153 RVA: 0x0002A5BC File Offset: 0x000287BC
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

	// Token: 0x06000482 RID: 1154 RVA: 0x00006732 File Offset: 0x00004932
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

	// Token: 0x06000483 RID: 1155 RVA: 0x0002A61C File Offset: 0x0002881C
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

	// Token: 0x06000484 RID: 1156 RVA: 0x0000676C File Offset: 0x0000496C
	public void CaseAnalog()
	{
		if (!Main.isPC)
		{
			if (!GameCanvas.isTouch)
			{
				this.chatVipToolOption();
			}
			else
			{
				this.analogToolOption();
			}
		}
	}

	// Token: 0x06000485 RID: 1157 RVA: 0x0002A674 File Offset: 0x00028874
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

	// Token: 0x06000486 RID: 1158 RVA: 0x00006798 File Offset: 0x00004998
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

	// Token: 0x06000487 RID: 1159 RVA: 0x000067D1 File Offset: 0x000049D1
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

	// Token: 0x06000488 RID: 1160 RVA: 0x0000680A File Offset: 0x00004A0A
	public void HatToolOption()
	{
		Service.gI().sendOptHat(0);
	}

	// Token: 0x06000489 RID: 1161 RVA: 0x000045ED File Offset: 0x000027ED
	public void update()
	{
	}

	// Token: 0x0600048A RID: 1162 RVA: 0x00006817 File Offset: 0x00004A17
	public void closeSound()
	{
		Sound.stopAll = true;
		this.stopAll();
	}

	// Token: 0x0600048B RID: 1163 RVA: 0x00006825 File Offset: 0x00004A25
	public void openSound()
	{
		if (Sound.music == null)
		{
			this.loadSound(0);
		}
		Sound.stopAll = false;
	}

	// Token: 0x0600048C RID: 1164 RVA: 0x0000683E File Offset: 0x00004A3E
	public void bigeExlode()
	{
		Sound.playSound(SoundMn.BIG_EXPLODE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600048D RID: 1165 RVA: 0x0000685D File Offset: 0x00004A5D
	public void explode_1()
	{
		Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600048E RID: 1166 RVA: 0x0000685D File Offset: 0x00004A5D
	public void explode_2()
	{
		Sound.playSound(SoundMn.EXPLODE_1, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600048F RID: 1167 RVA: 0x0000687C File Offset: 0x00004A7C
	public void traidatKame()
	{
		Sound.playSound(SoundMn.TRAIDAT_KAME, 1f);
		this.poolCount++;
	}

	// Token: 0x06000490 RID: 1168 RVA: 0x0000689B File Offset: 0x00004A9B
	public void namekKame()
	{
		Sound.playSound(SoundMn.NAMEK_KAME, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000491 RID: 1169 RVA: 0x000068BA File Offset: 0x00004ABA
	public void nameLazer()
	{
		Sound.playSound(SoundMn.NAMEK_LAZER, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000492 RID: 1170 RVA: 0x000068D9 File Offset: 0x00004AD9
	public void xaydaKame()
	{
		Sound.playSound(SoundMn.XAYDA_KAME, 0.3f);
		this.poolCount++;
	}

	// Token: 0x06000493 RID: 1171 RVA: 0x0002A6CC File Offset: 0x000288CC
	public void mobKame(int type)
	{
		int id = SoundMn.XAYDA_KAME;
		if (type == 13)
		{
			id = SoundMn.NORMAL_KAME;
		}
		Sound.playSound(id, 0.1f);
		this.poolCount++;
	}

	// Token: 0x06000494 RID: 1172 RVA: 0x0002A708 File Offset: 0x00028908
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

	// Token: 0x06000495 RID: 1173 RVA: 0x000068F8 File Offset: 0x00004AF8
	public void monkeyRun(float volumn)
	{
		if (GameCanvas.gameTick % 8 == 0)
		{
			Sound.playSound(SoundMn.KHICHAY, 0.2f);
			this.poolCount++;
		}
	}

	// Token: 0x06000496 RID: 1174 RVA: 0x00006923 File Offset: 0x00004B23
	public void charFall()
	{
		Sound.playSound(SoundMn.MOVE, 0.1f);
		this.poolCount++;
	}

	// Token: 0x06000497 RID: 1175 RVA: 0x00006942 File Offset: 0x00004B42
	public void charJump()
	{
		Sound.playSound(SoundMn.MOVE, 0.2f);
		this.poolCount++;
	}

	// Token: 0x06000498 RID: 1176 RVA: 0x00006961 File Offset: 0x00004B61
	public void panelOpen()
	{
		Sound.playSound(SoundMn.PANEL_OPEN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x06000499 RID: 1177 RVA: 0x00006980 File Offset: 0x00004B80
	public void buttonClose()
	{
		Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600049A RID: 1178 RVA: 0x0000699F File Offset: 0x00004B9F
	public void buttonClick()
	{
		Sound.playSound(SoundMn.BUTTON_CLICK, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600049B RID: 1179 RVA: 0x000045ED File Offset: 0x000027ED
	public void stopMove()
	{
	}

	// Token: 0x0600049C RID: 1180 RVA: 0x000069BE File Offset: 0x00004BBE
	public void charFly()
	{
		Sound.playSound(SoundMn.FLY, 0.2f);
		this.poolCount++;
	}

	// Token: 0x0600049D RID: 1181 RVA: 0x000045ED File Offset: 0x000027ED
	public void stopFly()
	{
	}

	// Token: 0x0600049E RID: 1182 RVA: 0x00006980 File Offset: 0x00004B80
	public void openMenu()
	{
		Sound.playSound(SoundMn.BUTTON_CLOSE, 0.5f);
		this.poolCount++;
	}

	// Token: 0x0600049F RID: 1183 RVA: 0x000069DD File Offset: 0x00004BDD
	public void panelClick()
	{
		Sound.playSound(SoundMn.PANEL_CLICK, 0.5f);
		this.poolCount++;
	}

	// Token: 0x060004A0 RID: 1184 RVA: 0x000069FC File Offset: 0x00004BFC
	public void eatPeans()
	{
		Sound.playSound(SoundMn.EAT_PEAN, 0.5f);
		this.poolCount++;
	}

	// Token: 0x060004A1 RID: 1185 RVA: 0x00006A1B File Offset: 0x00004C1B
	public void openDialog()
	{
		Sound.playSound(SoundMn.OPEN_DIALOG, 0.5f);
	}

	// Token: 0x060004A2 RID: 1186 RVA: 0x00006A2C File Offset: 0x00004C2C
	public void hoisinh()
	{
		Sound.playSound(SoundMn.HOISINH, 0.5f);
		this.poolCount++;
	}

	// Token: 0x060004A3 RID: 1187 RVA: 0x00006A4B File Offset: 0x00004C4B
	public void taitao()
	{
		Sound.playMus(SoundMn.TAITAONANGLUONG, 0.5f, true);
	}

	// Token: 0x060004A4 RID: 1188 RVA: 0x000045ED File Offset: 0x000027ED
	public void taitaoPause()
	{
	}

	// Token: 0x060004A5 RID: 1189 RVA: 0x0002A76C File Offset: 0x0002896C
	public bool isPlayRain()
	{
		bool result;
		try
		{
			result = Sound.isPlayingSound();
		}
		catch (Exception ex)
		{
			result = false;
		}
		return result;
	}

	// Token: 0x060004A6 RID: 1190 RVA: 0x00004381 File Offset: 0x00002581
	public bool isPlayAirShip()
	{
		return false;
	}

	// Token: 0x060004A7 RID: 1191 RVA: 0x00006A5D File Offset: 0x00004C5D
	public void airShip()
	{
		SoundMn.cout++;
		if (SoundMn.cout % 2 == 0)
		{
			Sound.playMus(SoundMn.AIR_SHIP, 0.3f, false);
		}
	}

	// Token: 0x060004A8 RID: 1192 RVA: 0x000045ED File Offset: 0x000027ED
	public void pauseAirShip()
	{
	}

	// Token: 0x060004A9 RID: 1193 RVA: 0x000045ED File Offset: 0x000027ED
	public void resumeAirShip()
	{
	}

	// Token: 0x060004AA RID: 1194 RVA: 0x00006A87 File Offset: 0x00004C87
	public void stopAll()
	{
		Sound.stopAllz();
	}

	// Token: 0x060004AB RID: 1195 RVA: 0x00006A8E File Offset: 0x00004C8E
	public void backToRegister()
	{
		Session_ME.gI().close();
		GameCanvas.panel.hide();
		GameCanvas.loginScr.actRegister();
		GameCanvas.loginScr.switchToMe();
	}

	// Token: 0x060004AC RID: 1196 RVA: 0x00006AB8 File Offset: 0x00004CB8
	public void newKame()
	{
		this.poolCount++;
		if (this.poolCount % 15 == 0)
		{
			Sound.playSound(SoundMn.TRAIDAT_KAME, 0.5f);
		}
	}

	// Token: 0x060004AD RID: 1197 RVA: 0x00006AE5 File Offset: 0x00004CE5
	public void radarClick()
	{
		Sound.playSound(SoundMn.RADAR_CLICK, 0.5f);
	}

	// Token: 0x060004AE RID: 1198 RVA: 0x00006AF6 File Offset: 0x00004CF6
	public void radarItem()
	{
		Sound.playSound(SoundMn.RADAR_ITEM, 0.5f);
	}

	// Token: 0x060004AF RID: 1199 RVA: 0x00006B07 File Offset: 0x00004D07
	public static void playSound(int x, int y, int id, float volume)
	{
		Sound.playSound(id, volume);
	}

	// Token: 0x040007EA RID: 2026
	public static bool IsDelAcc;

	// Token: 0x040007EB RID: 2027
	public static SoundMn gIz;

	// Token: 0x040007EC RID: 2028
	public static bool isSound = true;

	// Token: 0x040007ED RID: 2029
	public static float volume = 0.5f;

	// Token: 0x040007EE RID: 2030
	private static int MAX_VOLUME = 10;

	// Token: 0x040007EF RID: 2031
	public static SoundMn.MediaPlayer[] music;

	// Token: 0x040007F0 RID: 2032
	public static SoundMn.SoundPool[] sound;

	// Token: 0x040007F1 RID: 2033
	public static int[] soundID;

	// Token: 0x040007F2 RID: 2034
	public static int AIR_SHIP;

	// Token: 0x040007F3 RID: 2035
	public static int RAIN = 1;

	// Token: 0x040007F4 RID: 2036
	public static int TAITAONANGLUONG = 2;

	// Token: 0x040007F5 RID: 2037
	public static int GET_ITEM;

	// Token: 0x040007F6 RID: 2038
	public static int MOVE = 1;

	// Token: 0x040007F7 RID: 2039
	public static int LOW_PUNCH = 2;

	// Token: 0x040007F8 RID: 2040
	public static int LOW_KICK = 3;

	// Token: 0x040007F9 RID: 2041
	public static int FLY = 4;

	// Token: 0x040007FA RID: 2042
	public static int JUMP = 5;

	// Token: 0x040007FB RID: 2043
	public static int PANEL_OPEN = 6;

	// Token: 0x040007FC RID: 2044
	public static int BUTTON_CLOSE = 7;

	// Token: 0x040007FD RID: 2045
	public static int BUTTON_CLICK = 8;

	// Token: 0x040007FE RID: 2046
	public static int MEDIUM_PUNCH = 9;

	// Token: 0x040007FF RID: 2047
	public static int MEDIUM_KICK = 10;

	// Token: 0x04000800 RID: 2048
	public static int PANEL_CLICK = 11;

	// Token: 0x04000801 RID: 2049
	public static int EAT_PEAN = 12;

	// Token: 0x04000802 RID: 2050
	public static int OPEN_DIALOG = 13;

	// Token: 0x04000803 RID: 2051
	public static int NORMAL_KAME = 14;

	// Token: 0x04000804 RID: 2052
	public static int NAMEK_KAME = 15;

	// Token: 0x04000805 RID: 2053
	public static int XAYDA_KAME = 16;

	// Token: 0x04000806 RID: 2054
	public static int EXPLODE_1 = 17;

	// Token: 0x04000807 RID: 2055
	public static int EXPLODE_2 = 18;

	// Token: 0x04000808 RID: 2056
	public static int TRAIDAT_KAME = 19;

	// Token: 0x04000809 RID: 2057
	public static int HP_UP = 20;

	// Token: 0x0400080A RID: 2058
	public static int THAIDUONGHASAN = 21;

	// Token: 0x0400080B RID: 2059
	public static int HOISINH = 22;

	// Token: 0x0400080C RID: 2060
	public static int GONG = 23;

	// Token: 0x0400080D RID: 2061
	public static int KHICHAY = 24;

	// Token: 0x0400080E RID: 2062
	public static int BIG_EXPLODE = 25;

	// Token: 0x0400080F RID: 2063
	public static int NAMEK_LAZER = 26;

	// Token: 0x04000810 RID: 2064
	public static int NAMEK_CHARGE = 27;

	// Token: 0x04000811 RID: 2065
	public static int RADAR_CLICK = 28;

	// Token: 0x04000812 RID: 2066
	public static int RADAR_ITEM = 29;

	// Token: 0x04000813 RID: 2067
	public static int FIREWORK = 30;

	// Token: 0x04000814 RID: 2068
	public static int KAMEX10_0 = 31;

	// Token: 0x04000815 RID: 2069
	public static int KAMEX10_1 = 32;

	// Token: 0x04000816 RID: 2070
	public static int DESTROY_0 = 33;

	// Token: 0x04000817 RID: 2071
	public static int DESTROY_1 = 34;

	// Token: 0x04000818 RID: 2072
	public static int MAFUBA_0 = 35;

	// Token: 0x04000819 RID: 2073
	public static int MAFUBA_1 = 36;

	// Token: 0x0400081A RID: 2074
	public static int MAFUBA_2 = 37;

	// Token: 0x0400081B RID: 2075
	public static int DESTROY_2 = 38;

	// Token: 0x0400081C RID: 2076
	public bool freePool;

	// Token: 0x0400081D RID: 2077
	public int poolCount;

	// Token: 0x0400081E RID: 2078
	public static int cout = 1;

	// Token: 0x0200008B RID: 139
	public class MediaPlayer
	{
	}

	// Token: 0x0200008C RID: 140
	public class SoundPool
	{
	}

	// Token: 0x0200008D RID: 141
	public class AssetManager
	{
	}
}
