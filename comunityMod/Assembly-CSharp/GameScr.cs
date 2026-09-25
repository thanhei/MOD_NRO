using System;
using Assets.src.g;

// Token: 0x02000040 RID: 64
public class GameScr : mScreen, IChatable
{
	// Token: 0x06000348 RID: 840 RVA: 0x0003AB64 File Offset: 0x00038D64
	public GameScr()
	{
		if (GameCanvas.w == 128 || GameCanvas.h <= 208)
		{
			GameScr.indexSize = 20;
		}
		this.cmdback = new Command(string.Empty, 11021);
		this.cmdMenu = new Command("menu", 11000);
		this.cmdFocus = new Command(string.Empty, 11001);
		this.cmdMenu.img = GameScr.imgMenu;
		this.cmdMenu.w = mGraphics.getImageWidth(this.cmdMenu.img) + 20;
		this.cmdMenu.isPlaySoundButton = false;
		this.cmdFocus.img = GameScr.imgFocus;
		if (GameCanvas.isTouch)
		{
			this.cmdMenu.x = 0;
			this.cmdMenu.y = 50;
			this.cmdFocus = null;
		}
		else
		{
			this.cmdMenu.x = 0;
			this.cmdMenu.y = GameScr.gH - 30;
			this.cmdFocus.x = GameScr.gW - 32;
			this.cmdFocus.y = GameScr.gH - 32;
		}
		this.right = this.cmdFocus;
		GameScr.isPaintRada = 1;
		if (GameCanvas.isTouch)
		{
			GameScr.isHaveSelectSkill = true;
		}
		this.cmdDoiCo = new Command("Đổi cờ", GameCanvas.gI(), 100001, null);
		this.cmdLogOut = new Command("Logout", GameCanvas.gI(), 100002, null);
		this.cmdChatTheGioi = new Command("chat world", GameCanvas.gI(), 100003, null);
		this.cmdshowInfo = new Command("InfoLog", GameCanvas.gI(), 100004, null);
		this.cmdDoiCo.setType();
		this.cmdLogOut.setType();
		this.cmdChatTheGioi.setType();
		this.cmdshowInfo.setType();
		this.cmdChatTheGioi.x = GameCanvas.w - this.cmdChatTheGioi.w;
		this.cmdshowInfo.x = GameCanvas.w - this.cmdshowInfo.w;
		this.cmdLogOut.x = GameCanvas.w - this.cmdLogOut.w;
		this.cmdDoiCo.x = GameCanvas.w - this.cmdDoiCo.w;
		this.cmdChatTheGioi.y = this.cmdChatTheGioi.h + mFont.tahoma_7_white.getHeight();
		this.cmdshowInfo.y = this.cmdChatTheGioi.h * 2 + mFont.tahoma_7_white.getHeight();
		this.cmdLogOut.y = this.cmdChatTheGioi.h * 3 + mFont.tahoma_7_white.getHeight();
		this.cmdDoiCo.y = this.cmdChatTheGioi.h * 4 + mFont.tahoma_7_white.getHeight();
	}

	// Token: 0x06000349 RID: 841 RVA: 0x0003AEC0 File Offset: 0x000390C0
	public static void loadBg()
	{
		GameScr.fra_PVE_Bar_0 = new FrameImage(mSystem.loadImage("/mainImage/i_pve_bar_0.png"), 6, 15);
		GameScr.fra_PVE_Bar_1 = new FrameImage(mSystem.loadImage("/mainImage/i_pve_bar_1.png"), 38, 21);
		GameScr.imgVS = mSystem.loadImage("/mainImage/i_vs.png");
		GameScr.imgBall = mSystem.loadImage("/mainImage/i_charlife.png");
		GameScr.imgHP_NEW = mSystem.loadImage("/mainImage/i_hp.png");
		GameScr.imgKhung = mSystem.loadImage("/mainImage/i_khung.png");
		GameScr.imgLbtn = GameCanvas.loadImage("/mainImage/myTexture2dbtnl.png");
		GameScr.imgLbtnFocus = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf.png");
		GameScr.imgLbtn2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnl2.png");
		GameScr.imgLbtnFocus2 = GameCanvas.loadImage("/mainImage/myTexture2dbtnlf2.png");
		GameScr.imgPanel = GameCanvas.loadImage("/mainImage/myTexture2dpanel.png");
		GameScr.imgPanel2 = GameCanvas.loadImage("/mainImage/panel2.png");
		GameScr.imgHP = GameCanvas.loadImage("/mainImage/myTexture2dHP.png");
		GameScr.imgSP = GameCanvas.loadImage("/mainImage/SP.png");
		GameScr.imgHPLost = GameCanvas.loadImage("/mainImage/myTexture2dhpLost.png");
		GameScr.imgMPLost = GameCanvas.loadImage("/mainImage/myTexture2dmpLost.png");
		GameScr.imgMP = GameCanvas.loadImage("/mainImage/myTexture2dMP.png");
		GameScr.imgSkill = GameCanvas.loadImage("/mainImage/myTexture2dskill.png");
		GameScr.imgSkill2 = GameCanvas.loadImage("/mainImage/myTexture2dskill2.png");
		GameScr.imgMenu = GameCanvas.loadImage("/mainImage/myTexture2dmenu.png");
		GameScr.imgFocus = GameCanvas.loadImage("/mainImage/myTexture2dfocus.png");
		GameScr.imgHP_tm_do = GameCanvas.loadImage("/mainImage/tm-do.png");
		GameScr.imgHP_tm_vang = GameCanvas.loadImage("/mainImage/tm-vang.png");
		GameScr.imgHP_tm_xam = GameCanvas.loadImage("/mainImage/tm-xam.png");
		GameScr.imgHP_tm_xanh = GameCanvas.loadImage("/mainImage/tm-xanh.png");
		GameScr.imgChatPC = GameCanvas.loadImage("/pc/chat.png");
		GameScr.imgChatsPC2 = GameCanvas.loadImage("/pc/chat2.png");
		if (GameCanvas.isTouch)
		{
			GameScr.imgArrow = GameCanvas.loadImage("/mainImage/myTexture2darrow.png");
			GameScr.imgArrow2 = GameCanvas.loadImage("/mainImage/myTexture2darrow2.png");
			GameScr.imgChat = GameCanvas.loadImage("/mainImage/myTexture2dchat.png");
			GameScr.imgChat2 = GameCanvas.loadImage("/mainImage/myTexture2dchat2.png");
			GameScr.imgFocus2 = GameCanvas.loadImage("/mainImage/myTexture2dfocus2.png");
			GameScr.imgHP1 = GameCanvas.loadImage("/mainImage/myTexture2dPea0.png");
			GameScr.imgHP2 = GameCanvas.loadImage("/mainImage/myTexture2dPea1.png");
			GameScr.imgAnalog1 = GameCanvas.loadImage("/mainImage/myTexture2danalog1.png");
			GameScr.imgAnalog2 = GameCanvas.loadImage("/mainImage/myTexture2danalog2.png");
			GameScr.imgHP3 = GameCanvas.loadImage("/mainImage/myTexture2dPea2.png");
			GameScr.imgHP4 = GameCanvas.loadImage("/mainImage/myTexture2dPea3.png");
			GameScr.imgFire0 = GameCanvas.loadImage("/mainImage/myTexture2dfirebtn0.png");
			GameScr.imgFire1 = GameCanvas.loadImage("/mainImage/myTexture2dfirebtn1.png");
		}
		GameScr.imgNR1 = GameCanvas.loadImage("/mainImage/myTexture2dPea_0.png");
		GameScr.imgNR2 = GameCanvas.loadImage("/mainImage/myTexture2dPea_1.png");
		GameScr.imgNR3 = GameCanvas.loadImage("/mainImage/myTexture2dPea_2.png");
		GameScr.imgNR4 = GameCanvas.loadImage("/mainImage/myTexture2dPea_3.png");
		GameScr.flyTextX = new int[5];
		GameScr.flyTextY = new int[5];
		GameScr.flyTextDx = new int[5];
		GameScr.flyTextDy = new int[5];
		GameScr.flyTextState = new int[5];
		GameScr.flyTextString = new string[5];
		GameScr.flyTextYTo = new int[5];
		GameScr.flyTime = new int[5];
		GameScr.flyTextColor = new int[8];
		for (int i = 0; i < 5; i++)
		{
			GameScr.flyTextState[i] = -1;
		}
		sbyte[] array = Rms.loadRMS("NRdataVersion");
		sbyte[] array2 = Rms.loadRMS("NRmapVersion");
		sbyte[] array3 = Rms.loadRMS("NRskillVersion");
		sbyte[] array4 = Rms.loadRMS("NRitemVersion");
		if (array != null)
		{
			GameScr.vcData = array[0];
		}
		if (array2 != null)
		{
			GameScr.vcMap = array2[0];
		}
		if (array3 != null)
		{
			GameScr.vcSkill = array3[0];
		}
		if (array4 != null)
		{
			GameScr.vcItem = array4[0];
		}
		GameScr.imgNut = GameCanvas.loadImage("/mainImage/myTexture2dnut.png");
		GameScr.imgNutF = GameCanvas.loadImage("/mainImage/myTexture2dnutF.png");
		MobCapcha.init();
		GameScr.isAnalog = ((Rms.loadRMSInt("analog") == 1) ? 1 : 0);
		GameScr.gamePad = new GamePad();
		GameScr.arrow = GameCanvas.loadImage("/mainImage/myTexture2darrow3.png");
		GameScr.imgTrans = GameCanvas.loadImage("/bg/trans.png");
		GameScr.imgRoomStat = GameCanvas.loadImage("/mainImage/myTexture2dstat.png");
		GameScr.frBarPow0 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor20.png");
		GameScr.frBarPow1 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor21.png");
		GameScr.frBarPow2 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor22.png");
		GameScr.frBarPow20 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor00.png");
		GameScr.frBarPow21 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor01.png");
		GameScr.frBarPow22 = GameCanvas.loadImage("/mainImage/myTexture2dlineColor02.png");
	}

	// Token: 0x0600034A RID: 842 RVA: 0x0003B31A File Offset: 0x0003951A
	public void initSelectChar()
	{
		this.readPart();
		SmallImage.init();
	}

	// Token: 0x0600034B RID: 843 RVA: 0x0003B328 File Offset: 0x00039528
	public static void paintOngMauPercent(Image img0, Image img1, Image img2, float x, float y, int size, float pixelPercent, mGraphics g)
	{
		int clipX = g.getClipX();
		int clipY = g.getClipY();
		int clipWidth = g.getClipWidth();
		int clipHeight = g.getClipHeight();
		g.setClip((int)x, (int)y, (int)pixelPercent, 13);
		int num = size / 15 - 2;
		for (int i = 0; i < num; i++)
		{
			g.drawImage(img1, x + (float)((i + 1) * 15), y, 0);
		}
		g.drawImage(img0, x, y, 0);
		g.drawImage(img1, x + (float)size - 30f, y, 0);
		g.drawImage(img2, x + (float)size - 15f, y, 0);
		g.setClip(clipX, clipY, clipWidth, clipHeight);
	}

	// Token: 0x0600034C RID: 844 RVA: 0x0003B3D8 File Offset: 0x000395D8
	public void initTraining()
	{
		if (CreateCharScr.isCreateChar)
		{
			CreateCharScr.isCreateChar = false;
			this.right = null;
		}
	}

	// Token: 0x0600034D RID: 845 RVA: 0x0003B3EE File Offset: 0x000395EE
	public bool isMapDocNhan()
	{
		return TileMap.mapID >= 53 && TileMap.mapID <= 62;
	}

	// Token: 0x0600034E RID: 846 RVA: 0x0003B405 File Offset: 0x00039605
	public bool isMapFize()
	{
		return TileMap.mapID >= 63;
	}

	// Token: 0x0600034F RID: 847 RVA: 0x0003B414 File Offset: 0x00039614
	public override void switchToMe()
	{
		GameScr.vChatVip.removeAllElements();
		ServerListScreen.isWait = false;
		if (BackgroudEffect.isHaveRain())
		{
			SoundMn.gI().rain();
		}
		LoginScr.isContinueToLogin = false;
		global::Char.isLoadingMap = false;
		if (!GameScr.isPaintOther)
		{
			Service.gI().finishLoadMap();
		}
		if (TileMap.isTrainingMap())
		{
			this.initTraining();
		}
		GameScr.info1.isUpdate = true;
		GameScr.info2.isUpdate = true;
		this.resetButton();
		GameScr.isLoadAllData = true;
		GameScr.isPaintOther = false;
		base.switchToMe();
	}

	// Token: 0x06000350 RID: 848 RVA: 0x0003B49C File Offset: 0x0003969C
	public static int getMaxExp(int level)
	{
		int num = 0;
		for (int i = 0; i <= level; i++)
		{
			num += (int)GameScr.exps[i];
		}
		return num;
	}

	// Token: 0x06000351 RID: 849 RVA: 0x0003B4C4 File Offset: 0x000396C4
	public static void resetAllvector()
	{
		GameScr.vCharInMap.removeAllElements();
		Teleport.vTeleport.removeAllElements();
		GameScr.vItemMap.removeAllElements();
		Effect2.vEffect2.removeAllElements();
		Effect2.vAnimateEffect.removeAllElements();
		Effect2.vEffect2Outside.removeAllElements();
		Effect2.vEffectFeet.removeAllElements();
		Effect2.vEffect3.removeAllElements();
		GameScr.vMobAttack.removeAllElements();
		GameScr.vMob.removeAllElements();
		GameScr.vNpc.removeAllElements();
		global::Char.myCharz().vMovePoints.removeAllElements();
	}

	// Token: 0x06000352 RID: 850 RVA: 0x00004887 File Offset: 0x00002A87
	public void loadSkillShortcut()
	{
	}

	// Token: 0x06000353 RID: 851 RVA: 0x0003B550 File Offset: 0x00039750
	public void onOSkill(sbyte[] oSkillID)
	{
		Cout.println("GET onScreenSkill!");
		GameScr.onScreenSkill = new Skill[10];
		if (oSkillID == null)
		{
			this.loadDefaultonScreenSkill();
			return;
		}
		for (int i = 0; i < oSkillID.Length; i++)
		{
			for (int j = 0; j < global::Char.myCharz().vSkillFight.size(); j++)
			{
				Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(j);
				if (skill.template.id == oSkillID[i])
				{
					GameScr.onScreenSkill[i] = skill;
					break;
				}
			}
		}
	}

	// Token: 0x06000354 RID: 852 RVA: 0x0003B5D8 File Offset: 0x000397D8
	public void onKSkill(sbyte[] kSkillID)
	{
		Cout.println("GET KEYSKILL!");
		GameScr.keySkill = new Skill[10];
		if (kSkillID == null)
		{
			this.loadDefaultKeySkill();
			return;
		}
		for (int i = 0; i < kSkillID.Length; i++)
		{
			for (int j = 0; j < global::Char.myCharz().vSkillFight.size(); j++)
			{
				Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(j);
				if (skill.template.id == kSkillID[i])
				{
					GameScr.keySkill[i] = skill;
					break;
				}
			}
		}
	}

	// Token: 0x06000355 RID: 853 RVA: 0x0003B660 File Offset: 0x00039860
	public void onCSkill(sbyte[] cSkillID)
	{
		Cout.println("GET CURRENTSKILL!");
		if (cSkillID == null || cSkillID.Length == 0)
		{
			if (global::Char.myCharz().vSkillFight.size() > 0)
			{
				global::Char.myCharz().myskill = (Skill)global::Char.myCharz().vSkillFight.elementAt(0);
			}
		}
		else
		{
			for (int i = 0; i < global::Char.myCharz().vSkillFight.size(); i++)
			{
				Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
				if (skill.template.id == cSkillID[0])
				{
					global::Char.myCharz().myskill = skill;
					break;
				}
			}
		}
		if (global::Char.myCharz().myskill != null)
		{
			Service.gI().selectSkill((int)global::Char.myCharz().myskill.template.id);
			this.saveRMSCurrentSkill(global::Char.myCharz().myskill.template.id);
		}
	}

	// Token: 0x06000356 RID: 854 RVA: 0x0003B744 File Offset: 0x00039944
	internal void loadDefaultonScreenSkill()
	{
		Cout.println("LOAD DEFAULT ONmScreen SKILL");
		int num = 0;
		while (num < GameScr.onScreenSkill.Length && num < global::Char.myCharz().vSkillFight.size())
		{
			Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(num);
			GameScr.onScreenSkill[num] = skill;
			num++;
		}
		this.saveonScreenSkillToRMS();
	}

	// Token: 0x06000357 RID: 855 RVA: 0x0003B7A4 File Offset: 0x000399A4
	internal void loadDefaultKeySkill()
	{
		Cout.println("LOAD DEFAULT KEY SKILL");
		int num = 0;
		while (num < GameScr.keySkill.Length && num < global::Char.myCharz().vSkillFight.size())
		{
			Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(num);
			GameScr.keySkill[num] = skill;
			num++;
		}
		this.saveKeySkillToRMS();
	}

	// Token: 0x06000358 RID: 856 RVA: 0x0003B804 File Offset: 0x00039A04
	public void doSetOnScreenSkill(SkillTemplate skillTemplate)
	{
		Skill skill = global::Char.myCharz().getSkill(skillTemplate);
		MyVector myVector = new MyVector();
		for (int i = 0; i < 10; i++)
		{
			object[] array = new object[]
			{
				skill,
				i.ToString() + string.Empty
			};
			Command command = new Command(mResources.into_place + (i + 1).ToString(), 11120, array);
			if (GameScr.onScreenSkill[i] != null)
			{
				command.isDisplay = true;
			}
			myVector.addElement(command);
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x06000359 RID: 857 RVA: 0x0003B898 File Offset: 0x00039A98
	public void doSetKeySkill(SkillTemplate skillTemplate)
	{
		Cout.println("DO SET KEY SKILL");
		Skill skill = global::Char.myCharz().getSkill(skillTemplate);
		string[] array = ((!TField.isQwerty) ? mResources.key_skill : mResources.key_skill_qwerty);
		MyVector myVector = new MyVector();
		for (int i = 0; i < 10; i++)
		{
			object[] array2 = new object[]
			{
				skill,
				i.ToString() + string.Empty
			};
			myVector.addElement(new Command(array[i], 11121, array2));
		}
		GameCanvas.menu.startAt(myVector, 0);
	}

	// Token: 0x0600035A RID: 858 RVA: 0x0003B924 File Offset: 0x00039B24
	public void saveonScreenSkillToRMS()
	{
		sbyte[] array = new sbyte[GameScr.onScreenSkill.Length];
		for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
		{
			if (GameScr.onScreenSkill[i] == null)
			{
				array[i] = -1;
			}
			else
			{
				array[i] = GameScr.onScreenSkill[i].template.id;
			}
		}
		Service.gI().changeOnKeyScr(array);
	}

	// Token: 0x0600035B RID: 859 RVA: 0x0003B980 File Offset: 0x00039B80
	public void saveKeySkillToRMS()
	{
		sbyte[] array = new sbyte[GameScr.keySkill.Length];
		for (int i = 0; i < GameScr.keySkill.Length; i++)
		{
			if (GameScr.keySkill[i] == null)
			{
				array[i] = -1;
			}
			else
			{
				array[i] = GameScr.keySkill[i].template.id;
			}
		}
		Service.gI().changeOnKeyScr(array);
	}

	// Token: 0x0600035C RID: 860 RVA: 0x00004887 File Offset: 0x00002A87
	public void saveRMSCurrentSkill(sbyte id)
	{
	}

	// Token: 0x0600035D RID: 861 RVA: 0x0003B9DC File Offset: 0x00039BDC
	public void addSkillShortcut(Skill skill)
	{
		Cout.println("ADD SKILL SHORTCUT TO SKILL " + skill.template.id.ToString());
		for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
		{
			if (GameScr.onScreenSkill[i] == null)
			{
				GameScr.onScreenSkill[i] = skill;
				break;
			}
		}
		for (int j = 0; j < GameScr.keySkill.Length; j++)
		{
			if (GameScr.keySkill[j] == null)
			{
				GameScr.keySkill[j] = skill;
				break;
			}
		}
		if (global::Char.myCharz().myskill == null)
		{
			global::Char.myCharz().myskill = skill;
		}
		this.saveKeySkillToRMS();
		this.saveonScreenSkillToRMS();
	}

	// Token: 0x0600035E RID: 862 RVA: 0x0003BA78 File Offset: 0x00039C78
	public bool isBagFull()
	{
		for (int i = global::Char.myCharz().arrItemBag.Length - 1; i >= 0; i--)
		{
			if (global::Char.myCharz().arrItemBag[i] == null)
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600035F RID: 863 RVA: 0x0003BAAF File Offset: 0x00039CAF
	public void createConfirm(string[] menu, Npc npc)
	{
		this.resetButton();
		this.isLockKey = true;
		this.left = new Command(menu[0], 130011, npc);
		this.right = new Command(menu[1], 130012, npc);
	}

	// Token: 0x06000360 RID: 864 RVA: 0x0003BAE8 File Offset: 0x00039CE8
	public void createMenu(string[] menu, Npc npc)
	{
		MyVector myVector = new MyVector();
		for (int i = 0; i < menu.Length; i++)
		{
			myVector.addElement(new Command(menu[i], 11057, npc));
		}
		GameCanvas.menu.startAt(myVector, 2);
	}

	// Token: 0x06000361 RID: 865 RVA: 0x0003BB2C File Offset: 0x00039D2C
	public void readPart()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_part"));
			int num = (int)dataInputStream.readShort();
			GameScr.parts = new Part[num];
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)dataInputStream.readByte();
				GameScr.parts[i] = new Part(num2);
				for (int j = 0; j < GameScr.parts[i].pi.Length; j++)
				{
					GameScr.parts[i].pi[j] = new PartImage();
					GameScr.parts[i].pi[j].id = dataInputStream.readShort();
					GameScr.parts[i].pi[j].dx = dataInputStream.readByte();
					GameScr.parts[i].pi[j].dy = dataInputStream.readByte();
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI TAI readPart " + ex.ToString());
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Res.outz2("LOI TAI readPart 2" + ex2.StackTrace);
			}
		}
	}

	// Token: 0x06000362 RID: 866 RVA: 0x0003BC6C File Offset: 0x00039E6C
	public void readEfect()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_effect"));
			int num = (int)dataInputStream.readShort();
			GameScr.efs = new EffectCharPaint[num];
			for (int i = 0; i < num; i++)
			{
				GameScr.efs[i] = new EffectCharPaint();
				GameScr.efs[i].idEf = (int)dataInputStream.readShort();
				GameScr.efs[i].arrEfInfo = new EffectInfoPaint[(int)dataInputStream.readByte()];
				for (int j = 0; j < GameScr.efs[i].arrEfInfo.Length; j++)
				{
					GameScr.efs[i].arrEfInfo[j] = new EffectInfoPaint();
					GameScr.efs[i].arrEfInfo[j].idImg = (int)dataInputStream.readShort();
					GameScr.efs[i].arrEfInfo[j].dx = (int)dataInputStream.readByte();
					GameScr.efs[i].arrEfInfo[j].dy = (int)dataInputStream.readByte();
				}
			}
		}
		catch (Exception)
		{
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex)
			{
				Cout.LogError("Loi ham Eff: " + ex.ToString());
			}
		}
	}

	// Token: 0x06000363 RID: 867 RVA: 0x0003BDAC File Offset: 0x00039FAC
	public void readArrow()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_arrow"));
			int num = (int)dataInputStream.readShort();
			GameScr.arrs = new Arrowpaint[num];
			for (int i = 0; i < num; i++)
			{
				GameScr.arrs[i] = new Arrowpaint();
				GameScr.arrs[i].id = (int)dataInputStream.readShort();
				GameScr.arrs[i].imgId[0] = (int)dataInputStream.readShort();
				GameScr.arrs[i].imgId[1] = (int)dataInputStream.readShort();
				GameScr.arrs[i].imgId[2] = (int)dataInputStream.readShort();
			}
		}
		catch (Exception)
		{
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex)
			{
				Cout.LogError("Loi ham readArrow: " + ex.ToString());
			}
		}
	}

	// Token: 0x06000364 RID: 868 RVA: 0x0003BE94 File Offset: 0x0003A094
	public void readDart()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_dart"));
			int num = (int)dataInputStream.readShort();
			GameScr.darts = new DartInfo[num];
			for (int i = 0; i < num; i++)
			{
				GameScr.darts[i] = new DartInfo();
				GameScr.darts[i].id = dataInputStream.readShort();
				GameScr.darts[i].nUpdate = dataInputStream.readShort();
				GameScr.darts[i].va = (int)(dataInputStream.readShort() * 256);
				GameScr.darts[i].xdPercent = dataInputStream.readShort();
				int num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].tail = new short[num2];
				for (int j = 0; j < num2; j++)
				{
					GameScr.darts[i].tail[j] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].tailBorder = new short[num2];
				for (int k = 0; k < num2; k++)
				{
					GameScr.darts[i].tailBorder[k] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].xd1 = new short[num2];
				for (int l = 0; l < num2; l++)
				{
					GameScr.darts[i].xd1[l] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].xd2 = new short[num2];
				for (int m = 0; m < num2; m++)
				{
					GameScr.darts[i].xd2[m] = dataInputStream.readShort();
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].head = new short[num2][];
				for (int n = 0; n < num2; n++)
				{
					short num3 = dataInputStream.readShort();
					GameScr.darts[i].head[n] = new short[(int)num3];
					for (int num4 = 0; num4 < (int)num3; num4++)
					{
						GameScr.darts[i].head[n][num4] = dataInputStream.readShort();
					}
				}
				num2 = (int)dataInputStream.readShort();
				GameScr.darts[i].headBorder = new short[num2][];
				for (int num5 = 0; num5 < num2; num5++)
				{
					short num6 = dataInputStream.readShort();
					GameScr.darts[i].headBorder[num5] = new short[(int)num6];
					for (int num7 = 0; num7 < (int)num6; num7++)
					{
						GameScr.darts[i].headBorder[num5][num7] = dataInputStream.readShort();
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham ReadDart: " + ex.ToString());
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham reaaDart: " + ex2.ToString());
			}
		}
	}

	// Token: 0x06000365 RID: 869 RVA: 0x0003C198 File Offset: 0x0003A398
	public void readSkill()
	{
		DataInputStream dataInputStream = null;
		try
		{
			dataInputStream = new DataInputStream(Rms.loadRMS("NR_skill"));
			int num = (int)dataInputStream.readShort();
			GameScr.sks = new SkillPaint[Skills.skills.size()];
			for (int i = 0; i < num; i++)
			{
				short num2 = dataInputStream.readShort();
				if (num2 == 1111)
				{
					num2 = (short)(num - 1);
				}
				GameScr.sks[(int)num2] = new SkillPaint();
				GameScr.sks[(int)num2].id = (int)num2;
				GameScr.sks[(int)num2].effectHappenOnMob = (int)dataInputStream.readShort();
				if (GameScr.sks[(int)num2].effectHappenOnMob <= 0)
				{
					GameScr.sks[(int)num2].effectHappenOnMob = 80;
				}
				GameScr.sks[(int)num2].numEff = (int)dataInputStream.readByte();
				GameScr.sks[(int)num2].skillStand = new SkillInfoPaint[(int)dataInputStream.readByte()];
				for (int j = 0; j < GameScr.sks[(int)num2].skillStand.Length; j++)
				{
					GameScr.sks[(int)num2].skillStand[j] = new SkillInfoPaint();
					GameScr.sks[(int)num2].skillStand[j].status = (int)dataInputStream.readByte();
					GameScr.sks[(int)num2].skillStand[j].effS0Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].e0dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].e0dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].effS1Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].e1dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].e1dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].effS2Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].e2dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].e2dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].arrowId = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].adx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillStand[j].ady = (int)dataInputStream.readShort();
				}
				GameScr.sks[(int)num2].skillfly = new SkillInfoPaint[(int)dataInputStream.readByte()];
				for (int k = 0; k < GameScr.sks[(int)num2].skillfly.Length; k++)
				{
					GameScr.sks[(int)num2].skillfly[k] = new SkillInfoPaint();
					GameScr.sks[(int)num2].skillfly[k].status = (int)dataInputStream.readByte();
					GameScr.sks[(int)num2].skillfly[k].effS0Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].e0dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].e0dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].effS1Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].e1dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].e1dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].effS2Id = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].e2dx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].e2dy = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].arrowId = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].adx = (int)dataInputStream.readShort();
					GameScr.sks[(int)num2].skillfly[k].ady = (int)dataInputStream.readShort();
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham readSkill: " + ex.ToString());
		}
		finally
		{
			try
			{
				dataInputStream.close();
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham readskill: " + ex2.ToString());
			}
		}
	}

	// Token: 0x06000366 RID: 870 RVA: 0x0003C620 File Offset: 0x0003A820
	public static GameScr gI()
	{
		if (GameScr.instance == null)
		{
			GameScr.instance = new GameScr();
		}
		return GameScr.instance;
	}

	// Token: 0x06000367 RID: 871 RVA: 0x0003C638 File Offset: 0x0003A838
	public static void clearGameScr()
	{
		GameScr.instance = null;
	}

	// Token: 0x06000368 RID: 872 RVA: 0x0003C640 File Offset: 0x0003A840
	public void loadGameScr()
	{
		GameScr.loadSplash();
		Res.init();
		this.loadInforBar();
	}

	// Token: 0x06000369 RID: 873 RVA: 0x0003C654 File Offset: 0x0003A854
	public void doMenuInforMe()
	{
		GameScr.scrMain.clear();
		GameScr.scrInfo.clear();
		GameScr.isViewNext = false;
		this.cmdBag = new Command(mResources.MENUME[0], 1100011);
		this.cmdSkill = new Command(mResources.MENUME[1], 1100012);
		this.cmdTiemnang = new Command(mResources.MENUME[2], 1100013);
		this.cmdInfo = new Command(mResources.MENUME[3], 1100014);
		this.cmdtrangbi = new Command(mResources.MENUME[4], 1100015);
		MyVector myVector = new MyVector();
		myVector.addElement(this.cmdBag);
		myVector.addElement(this.cmdSkill);
		myVector.addElement(this.cmdTiemnang);
		myVector.addElement(this.cmdInfo);
		myVector.addElement(this.cmdtrangbi);
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x0600036A RID: 874 RVA: 0x0003C73C File Offset: 0x0003A93C
	public void doMenusynthesis()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.SYNTHESIS[0], 110002));
		myVector.addElement(new Command(mResources.SYNTHESIS[1], 1100032));
		myVector.addElement(new Command(mResources.SYNTHESIS[2], 1100033));
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x0600036B RID: 875 RVA: 0x0003C7A0 File Offset: 0x0003A9A0
	public static void loadCamera(bool fullmScreen, int cx, int cy)
	{
		GameScr.gW = GameCanvas.w;
		GameScr.cmdBarH = 39;
		GameScr.gH = GameCanvas.h;
		GameScr.cmdBarW = GameScr.gW;
		GameScr.cmdBarX = 0;
		GameScr.cmdBarY = GameCanvas.h - Paint.hTab - GameScr.cmdBarH;
		GameScr.girlHPBarY = 0;
		GameScr.csPadMaxH = GameCanvas.h / 6;
		if (GameScr.csPadMaxH < 48)
		{
			GameScr.csPadMaxH = 48;
		}
		GameScr.gW2 = GameScr.gW >> 1;
		GameScr.gH2 = GameScr.gH >> 1;
		GameScr.gW3 = GameScr.gW / 3;
		GameScr.gH3 = GameScr.gH / 3;
		GameScr.gW23 = GameScr.gH - 120;
		GameScr.gH23 = GameScr.gH * 2 / 3;
		GameScr.gW34 = 3 * GameScr.gW / 4;
		GameScr.gH34 = 3 * GameScr.gH / 4;
		GameScr.gW6 = GameScr.gW / 6;
		GameScr.gH6 = GameScr.gH / 6;
		GameScr.gssw = GameScr.gW / (int)TileMap.size + 2;
		GameScr.gssh = GameScr.gH / (int)TileMap.size + 2;
		if (GameScr.gW % 24 != 0)
		{
			GameScr.gssw++;
		}
		GameScr.cmxLim = (TileMap.tmw - 1) * (int)TileMap.size - GameScr.gW;
		GameScr.cmyLim = (TileMap.tmh - 1) * (int)TileMap.size - GameScr.gH;
		if (cx == -1 && cy == -1)
		{
			GameScr.cmx = (GameScr.cmtoX = global::Char.myCharz().cx - GameScr.gW2 + GameScr.gW6 * global::Char.myCharz().cdir);
			GameScr.cmy = (GameScr.cmtoY = global::Char.myCharz().cy - GameScr.gH23);
		}
		else
		{
			GameScr.cmx = (GameScr.cmtoX = cx - GameScr.gW23 + GameScr.gW6 * global::Char.myCharz().cdir);
			GameScr.cmy = (GameScr.cmtoY = cy - GameScr.gH23);
		}
		GameScr.firstY = GameScr.cmy;
		if (GameScr.cmx < 24)
		{
			GameScr.cmx = (GameScr.cmtoX = 24);
		}
		if (GameScr.cmx > GameScr.cmxLim)
		{
			GameScr.cmx = (GameScr.cmtoX = GameScr.cmxLim);
		}
		if (GameScr.cmy < 0)
		{
			GameScr.cmy = (GameScr.cmtoY = 0);
		}
		if (GameScr.cmy > GameScr.cmyLim)
		{
			GameScr.cmy = (GameScr.cmtoY = GameScr.cmyLim);
		}
		GameScr.gssx = GameScr.cmx / (int)TileMap.size - 1;
		if (GameScr.gssx < 0)
		{
			GameScr.gssx = 0;
		}
		GameScr.gssy = GameScr.cmy / (int)TileMap.size;
		GameScr.gssxe = GameScr.gssx + GameScr.gssw;
		GameScr.gssye = GameScr.gssy + GameScr.gssh;
		if (GameScr.gssy < 0)
		{
			GameScr.gssy = 0;
		}
		if (GameScr.gssye > TileMap.tmh - 1)
		{
			GameScr.gssye = TileMap.tmh - 1;
		}
		TileMap.countx = (GameScr.gssxe - GameScr.gssx) * 4;
		if (TileMap.countx > TileMap.tmw)
		{
			TileMap.countx = TileMap.tmw;
		}
		TileMap.county = (GameScr.gssye - GameScr.gssy) * 4;
		if (TileMap.county > TileMap.tmh)
		{
			TileMap.county = TileMap.tmh;
		}
		TileMap.gssx = (global::Char.myCharz().cx - 2 * GameScr.gW) / (int)TileMap.size;
		if (TileMap.gssx < 0)
		{
			TileMap.gssx = 0;
		}
		TileMap.gssxe = TileMap.gssx + TileMap.countx;
		if (TileMap.gssxe > TileMap.tmw)
		{
			TileMap.gssxe = TileMap.tmw;
		}
		TileMap.gssy = (global::Char.myCharz().cy - 2 * GameScr.gH) / (int)TileMap.size;
		if (TileMap.gssy < 0)
		{
			TileMap.gssy = 0;
		}
		TileMap.gssye = TileMap.gssy + TileMap.county;
		if (TileMap.gssye > TileMap.tmh)
		{
			TileMap.gssye = TileMap.tmh;
		}
		ChatTextField.gI().parentScreen = GameScr.instance;
		ChatTextField.gI().tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
		ChatTextField.gI().initChatTextField();
		if (GameCanvas.isTouch)
		{
			GameScr.yTouchBar = GameScr.gH - 88;
			GameScr.xC = GameScr.gW - 40;
			GameScr.yC = 2;
			if (GameCanvas.w <= 240)
			{
				GameScr.xC = GameScr.gW - 35;
				GameScr.yC = 5;
			}
			GameScr.xF = GameScr.gW - 55;
			GameScr.yF = GameScr.yTouchBar + 35;
			GameScr.xTG = GameScr.gW - 37;
			GameScr.yTG = GameScr.yTouchBar - 1;
			if (GameCanvas.w >= 450)
			{
				GameScr.yTG -= 12;
				GameScr.yHP -= 7;
				GameScr.xF -= 10;
				GameScr.yF -= 5;
				GameScr.xTG -= 10;
			}
		}
		GameScr.setSkillBarPosition();
		GameScr.disXC = ((GameCanvas.w <= 200) ? 30 : 40);
		if (Rms.loadRMSInt("viewchat") == -1)
		{
			GameCanvas.panel.isViewChatServer = true;
			return;
		}
		GameCanvas.panel.isViewChatServer = Rms.loadRMSInt("viewchat") == 1;
	}

	// Token: 0x0600036C RID: 876 RVA: 0x0003CCA8 File Offset: 0x0003AEA8
	public static void setSkillBarPosition()
	{
		Skill[] array = ((!GameCanvas.isTouch) ? GameScr.keySkill : GameScr.onScreenSkill);
		GameScr.xS = new int[array.Length];
		GameScr.yS = new int[array.Length];
		if (GameCanvas.isTouchControlSmallScreen && GameScr.isUseTouch)
		{
			GameScr.xSkill = 23;
			GameScr.ySkill = 52;
			GameScr.padSkill = 5;
			for (int i = 0; i < GameScr.xS.Length; i++)
			{
				GameScr.xS[i] = i * (25 + GameScr.padSkill);
				GameScr.yS[i] = GameScr.ySkill;
				if (GameScr.xS.Length > 5 && i >= GameScr.xS.Length / 2)
				{
					GameScr.xS[i] = (i - GameScr.xS.Length / 2) * (25 + GameScr.padSkill);
					GameScr.yS[i] = GameScr.ySkill - 32;
				}
			}
			GameScr.xHP = array.Length * (25 + GameScr.padSkill);
			GameScr.yHP = GameScr.ySkill;
		}
		else
		{
			GameScr.wSkill = 30;
			if (GameCanvas.w <= 320)
			{
				GameScr.ySkill = GameScr.gH - GameScr.wSkill - 6;
				GameScr.xSkill = GameScr.gW2 - array.Length * GameScr.wSkill / 2 - 25;
			}
			else
			{
				GameScr.wSkill = 40;
				GameScr.xSkill = 10;
				GameScr.ySkill = GameCanvas.h - GameScr.wSkill + 7;
			}
			for (int j = 0; j < GameScr.xS.Length; j++)
			{
				GameScr.xS[j] = j * GameScr.wSkill;
				GameScr.yS[j] = GameScr.ySkill;
				if (GameScr.xS.Length > 5 && j >= GameScr.xS.Length / 2)
				{
					GameScr.xS[j] = (j - GameScr.xS.Length / 2) * GameScr.wSkill;
					GameScr.yS[j] = GameScr.ySkill - 32;
				}
			}
			GameScr.xHP = array.Length * GameScr.wSkill;
			GameScr.yHP = GameScr.ySkill;
		}
		if (!GameCanvas.isTouch)
		{
			return;
		}
		GameScr.xSkill = 17;
		GameScr.ySkill = GameCanvas.h - 40;
		if (GameScr.gamePad.isSmallGamePad && GameScr.isAnalog == 1)
		{
			GameScr.xHP = array.Length * GameScr.wSkill;
			GameScr.yHP = GameScr.ySkill;
		}
		else
		{
			GameScr.xHP = GameCanvas.w - 45;
			GameScr.yHP = GameCanvas.h - 45;
		}
		GameScr.setTouchBtn();
		for (int k = 0; k < GameScr.xS.Length; k++)
		{
			GameScr.xS[k] = k * GameScr.wSkill;
			GameScr.yS[k] = GameScr.ySkill;
			if (GameScr.xS.Length > 5 && k >= GameScr.xS.Length / 2)
			{
				GameScr.xS[k] = (k - GameScr.xS.Length / 2) * GameScr.wSkill;
				GameScr.yS[k] = GameScr.ySkill - 32;
			}
		}
	}

	// Token: 0x0600036D RID: 877 RVA: 0x0003CF50 File Offset: 0x0003B150
	internal static void updateCamera()
	{
		if (GameScr.isPaintOther)
		{
			return;
		}
		if (GameScr.cmx != GameScr.cmtoX || GameScr.cmy != GameScr.cmtoY)
		{
			GameScr.cmvx = GameScr.cmtoX - GameScr.cmx << 2;
			GameScr.cmvy = GameScr.cmtoY - GameScr.cmy << 2;
			GameScr.cmdx += GameScr.cmvx;
			GameScr.cmx += GameScr.cmdx >> 4;
			GameScr.cmdx &= 15;
			GameScr.cmdy += GameScr.cmvy;
			GameScr.cmy += GameScr.cmdy >> 4;
			GameScr.cmdy &= 15;
			if (GameScr.cmx < 24)
			{
				GameScr.cmx = 24;
			}
			if (GameScr.cmx > GameScr.cmxLim)
			{
				GameScr.cmx = GameScr.cmxLim;
			}
			if (GameScr.cmy < 0)
			{
				GameScr.cmy = 0;
			}
			if (GameScr.cmy > GameScr.cmyLim)
			{
				GameScr.cmy = GameScr.cmyLim;
			}
		}
		GameScr.gssx = GameScr.cmx / (int)TileMap.size - 1;
		if (GameScr.gssx < 0)
		{
			GameScr.gssx = 0;
		}
		GameScr.gssy = GameScr.cmy / (int)TileMap.size;
		GameScr.gssxe = GameScr.gssx + GameScr.gssw;
		GameScr.gssye = GameScr.gssy + GameScr.gssh;
		if (GameScr.gssy < 0)
		{
			GameScr.gssy = 0;
		}
		if (GameScr.gssye > TileMap.tmh - 1)
		{
			GameScr.gssye = TileMap.tmh - 1;
		}
		TileMap.gssx = (global::Char.myCharz().cx - 2 * GameScr.gW) / (int)TileMap.size;
		if (TileMap.gssx < 0)
		{
			TileMap.gssx = 0;
		}
		TileMap.gssxe = TileMap.gssx + TileMap.countx;
		if (TileMap.gssxe > TileMap.tmw)
		{
			TileMap.gssxe = TileMap.tmw;
			TileMap.gssx = TileMap.gssxe - TileMap.countx;
		}
		TileMap.gssy = (global::Char.myCharz().cy - 2 * GameScr.gH) / (int)TileMap.size;
		if (TileMap.gssy < 0)
		{
			TileMap.gssy = 0;
		}
		TileMap.gssye = TileMap.gssy + TileMap.county;
		if (TileMap.gssye > TileMap.tmh)
		{
			TileMap.gssye = TileMap.tmh;
			TileMap.gssy = TileMap.gssye - TileMap.county;
		}
		GameScr.scrMain.updatecm();
		GameScr.scrInfo.updatecm();
	}

	// Token: 0x0600036E RID: 878 RVA: 0x0003D19C File Offset: 0x0003B39C
	public bool testAct()
	{
		for (sbyte b = 2; b < 9; b += 2)
		{
			if (GameCanvas.keyHold[(int)b])
			{
				return false;
			}
		}
		return true;
	}

	// Token: 0x0600036F RID: 879 RVA: 0x0003D1C4 File Offset: 0x0003B3C4
	public void clanInvite(string strInvite, int clanID, int code)
	{
		ClanObject clanObject = new ClanObject();
		clanObject.code = code;
		clanObject.clanID = clanID;
		this.startYesNoPopUp(strInvite, new Command(mResources.YES, 12002, clanObject), new Command(mResources.NO, 12003, clanObject));
	}

	// Token: 0x06000370 RID: 880 RVA: 0x0003D20C File Offset: 0x0003B40C
	public void playerMenu(global::Char c)
	{
		this.auto = 0;
		GameCanvas.clearKeyHold();
		if (global::Char.myCharz().charFocus.charID < 0 || global::Char.myCharz().charID < 0)
		{
			return;
		}
		MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
		if (vPlayerMenu.size() > 0)
		{
			return;
		}
		if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId > 1)
		{
			vPlayerMenu.addElement(new Command(mResources.make_friend, 11112, global::Char.myCharz().charFocus));
			vPlayerMenu.addElement(new Command(mResources.trade, 11113, global::Char.myCharz().charFocus));
		}
		if (global::Char.myCharz().clan != null && global::Char.myCharz().role < 2 && global::Char.myCharz().charFocus.clanID == -1)
		{
			vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[4], 110391));
		}
		if (global::Char.myCharz().charFocus.statusMe != 14 && global::Char.myCharz().charFocus.statusMe != 5)
		{
			if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 14)
			{
				vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[0], 2003));
			}
		}
		else
		{
			int type = global::Char.myCharz().myskill.template.type;
		}
		if (global::Char.myCharz().clan != null && global::Char.myCharz().clan.ID == global::Char.myCharz().charFocus.clanID && global::Char.myCharz().charFocus.statusMe != 14 && global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId >= 14)
		{
			vPlayerMenu.addElement(new Command(mResources.CHAR_ORDER[1], 2004));
		}
		int num = global::Char.myCharz().nClass.skillTemplates.Length;
		for (int i = 0; i < num; i++)
		{
			SkillTemplate skillTemplate = global::Char.myCharz().nClass.skillTemplates[i];
			Skill skill = global::Char.myCharz().getSkill(skillTemplate);
			if (skill != null && skillTemplate.isBuffToPlayer() && skill.point >= 1)
			{
				vPlayerMenu.addElement(new Command(skillTemplate.name, 12004, skill));
			}
		}
	}

	// Token: 0x06000371 RID: 881 RVA: 0x0003D44C File Offset: 0x0003B64C
	public bool isAttack()
	{
		if (this.checkClickToBotton(global::Char.myCharz().charFocus))
		{
			return false;
		}
		if (this.checkClickToBotton(global::Char.myCharz().mobFocus))
		{
			return false;
		}
		if (this.checkClickToBotton(global::Char.myCharz().npcFocus))
		{
			return false;
		}
		if (ChatTextField.gI().isShow)
		{
			return false;
		}
		if (InfoDlg.isLock || global::Char.myCharz().isLockAttack || global::Char.isLockKey)
		{
			return false;
		}
		if (global::Char.myCharz().myskill != null && global::Char.myCharz().myskill.template.id == 6 && global::Char.myCharz().itemFocus != null)
		{
			this.pickItem();
			return false;
		}
		if (global::Char.myCharz().myskill != null && global::Char.myCharz().myskill.template.type == 2 && global::Char.myCharz().npcFocus == null && global::Char.myCharz().myskill.template.id != 6)
		{
			return this.checkSkillValid();
		}
		if (global::Char.myCharz().skillPaint != null || (global::Char.myCharz().mobFocus == null && global::Char.myCharz().npcFocus == null && global::Char.myCharz().charFocus == null && global::Char.myCharz().itemFocus == null))
		{
			return false;
		}
		if (global::Char.myCharz().mobFocus != null)
		{
			if (global::Char.myCharz().mobFocus.isBigBoss() && global::Char.myCharz().mobFocus.status == 4)
			{
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().currentMovePoint = null;
			}
			GameScr.isAutoPlay = true;
			if (!this.isMeCanAttackMob(global::Char.myCharz().mobFocus))
			{
				Res.outz("can not attack");
				return false;
			}
			if (this.mobCapcha != null)
			{
				return false;
			}
			if (global::Char.myCharz().myskill == null)
			{
				return false;
			}
			if (global::Char.myCharz().isSelectingSkillUseAlone())
			{
				return false;
			}
			int num = -1;
			int num2 = Res.abs(global::Char.myCharz().cx - GameScr.cmx) * mGraphics.zoomLevel;
			if (global::Char.myCharz().charFocus != null)
			{
				num = Res.abs(global::Char.myCharz().cx - global::Char.myCharz().charFocus.cx) * mGraphics.zoomLevel;
			}
			else if (global::Char.myCharz().mobFocus != null)
			{
				num = Res.abs(global::Char.myCharz().cx - global::Char.myCharz().mobFocus.x) * mGraphics.zoomLevel;
			}
			if ((global::Char.myCharz().mobFocus.status == 1 || global::Char.myCharz().mobFocus.status == 0 || global::Char.myCharz().myskill.template.type == 4 || num == -1 || num > num2) && global::Char.myCharz().myskill.template.type == 4)
			{
				if (global::Char.myCharz().mobFocus.x < global::Char.myCharz().cx)
				{
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					global::Char.myCharz().cdir = 1;
				}
				this.doSelectSkill(global::Char.myCharz().myskill, true);
			}
			if (!this.checkSkillValid())
			{
				return false;
			}
			if (global::Char.myCharz().cx < global::Char.myCharz().mobFocus.getX())
			{
				global::Char.myCharz().cdir = 1;
			}
			else
			{
				global::Char.myCharz().cdir = -1;
			}
			int num3 = Math2.abs(global::Char.myCharz().cx - global::Char.myCharz().mobFocus.getX());
			int num4 = Math2.abs(global::Char.myCharz().cy - global::Char.myCharz().mobFocus.getY());
			global::Char.myCharz().cvx = 0;
			if (num3 > global::Char.myCharz().myskill.dx || num4 > global::Char.myCharz().myskill.dy)
			{
				bool flag = false;
				if (global::Char.myCharz().mobFocus is BigBoss || global::Char.myCharz().mobFocus is BigBoss2)
				{
					flag = true;
				}
				int num5 = (global::Char.myCharz().myskill.dx - ((!flag) ? 20 : 50)) * ((global::Char.myCharz().cx > global::Char.myCharz().mobFocus.getX()) ? 1 : (-1));
				if (num3 <= global::Char.myCharz().myskill.dx)
				{
					num5 = 0;
				}
				global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().mobFocus.getX() + num5, global::Char.myCharz().mobFocus.getY());
				global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return false;
			}
			if (global::Char.myCharz().myskill.template.id == 20)
			{
				return true;
			}
			if (num4 > num3 && Res.abs(global::Char.myCharz().cy - global::Char.myCharz().mobFocus.getY()) > 30 && global::Char.myCharz().mobFocus.getTemplate().type == 4)
			{
				global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().cx + global::Char.myCharz().cdir, global::Char.myCharz().mobFocus.getY());
				global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return false;
			}
			int num6 = 20;
			bool flag2 = false;
			if (global::Char.myCharz().mobFocus is BigBoss || global::Char.myCharz().mobFocus is BigBoss2)
			{
				flag2 = true;
			}
			if (global::Char.myCharz().myskill.dx > 100)
			{
				num6 = 60;
				if (num3 < 20)
				{
					global::Char.myCharz().createShadow(global::Char.myCharz().cx, global::Char.myCharz().cy, 10);
				}
			}
			bool flag3 = false;
			if ((TileMap.tileTypeAtPixel(global::Char.myCharz().cx, global::Char.myCharz().cy + 3) & 2) == 2)
			{
				int num7 = ((global::Char.myCharz().cx > global::Char.myCharz().mobFocus.getX()) ? 1 : (-1));
				if ((TileMap.tileTypeAtPixel(global::Char.myCharz().mobFocus.getX() + num6 * num7, global::Char.myCharz().cy + 3) & 2) != 2)
				{
					flag3 = true;
				}
			}
			if (num3 <= num6 && !flag3)
			{
				if (global::Char.myCharz().cx > global::Char.myCharz().mobFocus.getX())
				{
					int num8 = global::Char.myCharz().mobFocus.getX() + num6 + (flag2 ? 30 : 0);
					int i = global::Char.myCharz().mobFocus.getX();
					bool flag4 = false;
					while (i < num8)
					{
						if (TileMap.tileTypeAtPixel(i, global::Char.myCharz().cy + 3) == 8 || TileMap.tileTypeAtPixel(i, global::Char.myCharz().cy + 3) == 4)
						{
							flag4 = true;
							break;
						}
						i += 24;
					}
					if (flag4)
					{
						global::Char.myCharz().cx = i - 24;
					}
					else
					{
						global::Char.myCharz().cx = num8;
					}
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					int num9 = global::Char.myCharz().mobFocus.getX() - num6 - (flag2 ? 30 : 0);
					int j = global::Char.myCharz().mobFocus.getX();
					bool flag5 = false;
					while (j > num9)
					{
						if (TileMap.tileTypeAtPixel(j, global::Char.myCharz().cy + 3) == 8 || TileMap.tileTypeAtPixel(j, global::Char.myCharz().cy + 3) == 4)
						{
							flag5 = true;
							break;
						}
						j -= 24;
					}
					if (flag5)
					{
						global::Char.myCharz().cx = j + 24;
					}
					else
					{
						global::Char.myCharz().cx = num9;
					}
					global::Char.myCharz().cdir = 1;
				}
				Service.gI().charMove();
			}
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			return true;
		}
		else if (global::Char.myCharz().npcFocus != null)
		{
			if (global::Char.myCharz().npcFocus.isHide)
			{
				return false;
			}
			if (global::Char.myCharz().cx < global::Char.myCharz().npcFocus.cx)
			{
				global::Char.myCharz().cdir = 1;
			}
			else
			{
				global::Char.myCharz().cdir = -1;
			}
			if (global::Char.myCharz().cx < global::Char.myCharz().npcFocus.cx)
			{
				global::Char.myCharz().npcFocus.cdir = -1;
			}
			else
			{
				global::Char.myCharz().npcFocus.cdir = 1;
			}
			int num10 = Math2.abs(global::Char.myCharz().cx - global::Char.myCharz().npcFocus.cx);
			if (Math2.abs(global::Char.myCharz().cy - global::Char.myCharz().npcFocus.cy) > 40)
			{
				global::Char.myCharz().cy = global::Char.myCharz().npcFocus.cy - 40;
			}
			if (num10 < 60)
			{
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				if (this.tMenuDelay == 0)
				{
					if (global::Char.myCharz().taskMaint != null && global::Char.myCharz().taskMaint.taskId == 0)
					{
						if (global::Char.myCharz().taskMaint.index < 4 && global::Char.myCharz().npcFocus.template.npcTemplateId == 4)
						{
							return false;
						}
						if (global::Char.myCharz().taskMaint.index < 3 && global::Char.myCharz().npcFocus.template.npcTemplateId == 3)
						{
							return false;
						}
					}
					this.tMenuDelay = 50;
					InfoDlg.showWait();
					Service.gI().charMove();
					Service.gI().openMenu(global::Char.myCharz().npcFocus.template.npcTemplateId);
				}
			}
			else
			{
				int num11 = 20 + Res.r.nextInt(20);
				int num12 = ((global::Char.myCharz().cx > global::Char.myCharz().npcFocus.cx) ? 1 : (-1));
				global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().npcFocus.cx + num11 * num12, global::Char.myCharz().cy);
				global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
			}
			return false;
		}
		else if (global::Char.myCharz().charFocus != null)
		{
			if (this.mobCapcha != null)
			{
				return false;
			}
			if (global::Char.myCharz().cx < global::Char.myCharz().charFocus.cx)
			{
				global::Char.myCharz().cdir = 1;
			}
			else
			{
				global::Char.myCharz().cdir = -1;
			}
			int num13 = Math2.abs(global::Char.myCharz().cx - global::Char.myCharz().charFocus.cx);
			int num14 = Math2.abs(global::Char.myCharz().cy - global::Char.myCharz().charFocus.cy);
			if (!global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus) && !global::Char.myCharz().isSelectingSkillBuffToPlayer())
			{
				if (num13 < 60 && num14 < 40)
				{
					this.playerMenu(global::Char.myCharz().charFocus);
					if (!GameCanvas.isTouch && global::Char.myCharz().charFocus.charID >= 0 && TileMap.mapID != 51 && TileMap.mapID != 52 && this.popUpYesNo == null)
					{
						GameCanvas.panel.setTypePlayerMenu(global::Char.myCharz().charFocus);
						GameCanvas.panel.show();
						Service.gI().getPlayerMenu(global::Char.myCharz().charFocus.charID);
						Service.gI().messagePlayerMenu(global::Char.myCharz().charFocus.charID);
					}
				}
				else
				{
					int num15 = 20 + Res.r.nextInt(20);
					int num16 = ((global::Char.myCharz().cx > global::Char.myCharz().charFocus.cx) ? 1 : (-1));
					global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().charFocus.cx + num15 * num16, global::Char.myCharz().charFocus.cy);
					global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
					GameCanvas.clearKeyHold();
					GameCanvas.clearKeyPressed();
				}
				return false;
			}
			if (global::Char.myCharz().myskill == null)
			{
				return false;
			}
			if (!this.checkSkillValid())
			{
				return false;
			}
			if (global::Char.myCharz().cx < global::Char.myCharz().charFocus.cx)
			{
				global::Char.myCharz().cdir = 1;
			}
			else
			{
				global::Char.myCharz().cdir = -1;
			}
			global::Char.myCharz().cvx = 0;
			if (num13 > global::Char.myCharz().myskill.dx || num14 > global::Char.myCharz().myskill.dy)
			{
				int num17 = (global::Char.myCharz().myskill.dx - 20) * ((global::Char.myCharz().cx > global::Char.myCharz().charFocus.cx) ? 1 : (-1));
				if (num13 <= global::Char.myCharz().myskill.dx)
				{
					num17 = 0;
				}
				global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().charFocus.cx + num17, global::Char.myCharz().charFocus.cy);
				global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				return false;
			}
			if (global::Char.myCharz().myskill.template.id == 20)
			{
				return true;
			}
			int num18 = 20;
			if (global::Char.myCharz().myskill.dx > 60)
			{
				num18 = 60;
				if (num13 < 20)
				{
					global::Char.myCharz().createShadow(global::Char.myCharz().cx, global::Char.myCharz().cy, 10);
				}
			}
			bool flag6 = false;
			if ((TileMap.tileTypeAtPixel(global::Char.myCharz().cx, global::Char.myCharz().cy + 3) & 2) == 2)
			{
				int num19 = ((global::Char.myCharz().cx > global::Char.myCharz().charFocus.cx) ? 1 : (-1));
				if ((TileMap.tileTypeAtPixel(global::Char.myCharz().charFocus.cx + num18 * num19, global::Char.myCharz().cy + 3) & 2) != 2)
				{
					flag6 = true;
				}
			}
			if (num13 <= num18 && !flag6)
			{
				if (global::Char.myCharz().cx > global::Char.myCharz().charFocus.cx)
				{
					global::Char.myCharz().cx = global::Char.myCharz().charFocus.cx + num18;
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					global::Char.myCharz().cx = global::Char.myCharz().charFocus.cx - num18;
					global::Char.myCharz().cdir = 1;
				}
				Service.gI().charMove();
			}
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			return true;
		}
		else
		{
			if (global::Char.myCharz().itemFocus != null)
			{
				this.pickItem();
				return false;
			}
			return true;
		}
	}

	// Token: 0x06000372 RID: 882 RVA: 0x0003E284 File Offset: 0x0003C484
	public bool isMeCanAttackMob(Mob m)
	{
		if (m == null)
		{
			return false;
		}
		if (global::Char.myCharz().cTypePk == 5)
		{
			return true;
		}
		if (global::Char.myCharz().isAttacPlayerStatus() && !m.isMobMe)
		{
			return false;
		}
		if (global::Char.myCharz().mobMe != null && m.Equals(global::Char.myCharz().mobMe))
		{
			return false;
		}
		global::Char @char = GameScr.findCharInMap(m.mobId);
		return @char == null || @char.cTypePk == 5 || global::Char.myCharz().isMeCanAttackOtherPlayer(@char);
	}

	// Token: 0x06000373 RID: 883 RVA: 0x0003E308 File Offset: 0x0003C508
	internal bool checkSkillValid()
	{
		if (global::Char.myCharz().myskill != null && ((global::Char.myCharz().myskill.template.manaUseType != 1 && global::Char.myCharz().cMP < global::Char.myCharz().myskill.manaUse) || (global::Char.myCharz().myskill.template.manaUseType == 1 && global::Char.myCharz().cMP < global::Char.myCharz().cMPFull * global::Char.myCharz().myskill.manaUse / 100)))
		{
			GameScr.info1.addInfo(mResources.NOT_ENOUGH_MP, 0);
			this.auto = 0;
			return false;
		}
		if (global::Char.myCharz().myskill == null || (global::Char.myCharz().myskill.template.maxPoint > 0 && global::Char.myCharz().myskill.point == 0))
		{
			GameCanvas.startOKDlg(mResources.SKILL_FAIL);
			return false;
		}
		return true;
	}

	// Token: 0x06000374 RID: 884 RVA: 0x0003E3F0 File Offset: 0x0003C5F0
	internal bool checkSkillValid2()
	{
		return (global::Char.myCharz().myskill == null || ((global::Char.myCharz().myskill.template.manaUseType == 1 || global::Char.myCharz().cMP >= global::Char.myCharz().myskill.manaUse) && (global::Char.myCharz().myskill.template.manaUseType != 1 || global::Char.myCharz().cMP >= global::Char.myCharz().cMPFull * global::Char.myCharz().myskill.manaUse / 100))) && global::Char.myCharz().myskill != null && (global::Char.myCharz().myskill.template.maxPoint <= 0 || global::Char.myCharz().myskill.point != 0);
	}

	// Token: 0x06000375 RID: 885 RVA: 0x0003E4B4 File Offset: 0x0003C6B4
	public void resetButton()
	{
		GameCanvas.menu.showMenu = false;
		ChatTextField.gI().close();
		ChatTextField.gI().center = null;
		this.isLockKey = false;
		this.typeTrade = 0;
		GameScr.indexMenu = 0;
		GameScr.indexSelect = 0;
		this.indexItemUse = -1;
		GameScr.indexRow = -1;
		GameScr.indexRowMax = 0;
		GameScr.indexTitle = 0;
		this.typeTrade = (this.typeTradeOrder = 0);
		mSystem.endKey();
		if (global::Char.myCharz().cHP <= 0 || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5)
		{
			if (global::Char.myCharz().meDead)
			{
				this.cmdDead = new Command(mResources.DIES[0], 11038);
				this.center = this.cmdDead;
				global::Char.myCharz().cHP = 0;
			}
			GameScr.isHaveSelectSkill = false;
		}
		else
		{
			GameScr.isHaveSelectSkill = true;
		}
		GameScr.scrMain.clear();
	}

	// Token: 0x06000376 RID: 886 RVA: 0x0003E5A3 File Offset: 0x0003C7A3
	public override void keyPress(int keyCode)
	{
		base.keyPress(keyCode);
	}

	// Token: 0x06000377 RID: 887 RVA: 0x0003E5AC File Offset: 0x0003C7AC
	public override void updateKey()
	{
		if (Controller.isStopReadMessage || global::Char.myCharz().isTeleport || global::Char.myCharz().isPaintNewSkill || InfoDlg.isLock)
		{
			return;
		}
		if (GameCanvas.isTouch && !ChatTextField.gI().isShow && !GameCanvas.menu.showMenu)
		{
			this.updateKeyTouchControl();
		}
		this.checkAuto();
		GameCanvas.debug("F2", 0);
		if (ChatPopup.currChatPopup != null)
		{
			Command cmdNextLine = ChatPopup.currChatPopup.cmdNextLine;
			if ((GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(cmdNextLine)) && cmdNextLine != null)
			{
				GameCanvas.isPointerJustRelease = false;
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				mScreen.keyTouch = -1;
				if (cmdNextLine != null)
				{
					cmdNextLine.performAction();
				}
			}
		}
		else if (!ChatTextField.gI().isShow)
		{
			if ((GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.left)) && this.left != null)
			{
				GameCanvas.isPointerJustRelease = false;
				GameCanvas.isPointerClick = false;
				GameCanvas.keyPressed[12] = false;
				mScreen.keyTouch = -1;
				if (this.left != null)
				{
					this.left.performAction();
				}
			}
			if ((GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.right)) && this.right != null)
			{
				GameCanvas.isPointerJustRelease = false;
				GameCanvas.isPointerClick = false;
				GameCanvas.keyPressed[13] = false;
				mScreen.keyTouch = -1;
				if (this.right != null)
				{
					this.right.performAction();
				}
			}
			if ((GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(GameCanvas.currentScreen.center)) && this.center != null)
			{
				GameCanvas.isPointerJustRelease = false;
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				mScreen.keyTouch = -1;
				if (this.center != null)
				{
					this.center.performAction();
				}
			}
		}
		else
		{
			if (ChatTextField.gI().left != null && (GameCanvas.keyPressed[12] || mScreen.getCmdPointerLast(ChatTextField.gI().left)) && ChatTextField.gI().left != null)
			{
				ChatTextField.gI().left.performAction();
			}
			if (ChatTextField.gI().right != null && (GameCanvas.keyPressed[13] || mScreen.getCmdPointerLast(ChatTextField.gI().right)) && ChatTextField.gI().right != null)
			{
				ChatTextField.gI().right.performAction();
			}
			if (ChatTextField.gI().center != null && (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] || mScreen.getCmdPointerLast(ChatTextField.gI().center)) && ChatTextField.gI().center != null)
			{
				ChatTextField.gI().center.performAction();
			}
		}
		GameCanvas.debug("F6", 0);
		this.updateKeyAlert();
		GameCanvas.debug("F7", 0);
		if (global::Char.myCharz().currentMovePoint != null)
		{
			for (int i = 0; i < GameCanvas.keyPressed.Length; i++)
			{
				if (GameCanvas.keyPressed[i])
				{
					global::Char.myCharz().currentMovePoint = null;
					break;
				}
			}
		}
		GameCanvas.debug("F8", 0);
		if (ChatTextField.gI().isShow && GameCanvas.keyAsciiPress != 0)
		{
			ChatTextField.gI().keyPressed(GameCanvas.keyAsciiPress);
			GameCanvas.keyAsciiPress = 0;
			return;
		}
		if (this.isLockKey)
		{
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			return;
		}
		if (GameCanvas.menu.showMenu || this.isOpenUI() || global::Char.isLockKey)
		{
			return;
		}
		if (GameCanvas.keyPressed[10])
		{
			GameCanvas.keyPressed[10] = false;
			this.doUseHP();
			GameCanvas.clearKeyPressed();
		}
		if (GameCanvas.keyPressed[11] && this.mobCapcha == null)
		{
			if (this.popUpYesNo != null)
			{
				this.popUpYesNo.cmdYes.performAction();
			}
			else if (GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null)
			{
				GameCanvas.panel.setTypeMessage();
				GameCanvas.panel.show();
			}
			GameCanvas.keyPressed[11] = false;
			GameCanvas.clearKeyPressed();
		}
		if (GameCanvas.keyAsciiPress != 0 && TField.isQwerty && GameCanvas.keyAsciiPress == 32)
		{
			this.doUseHP();
			GameCanvas.keyAsciiPress = 0;
			GameCanvas.clearKeyPressed();
		}
		if (GameCanvas.keyAsciiPress != 0 && this.mobCapcha == null && TField.isQwerty && GameCanvas.keyAsciiPress == 121)
		{
			if (this.popUpYesNo != null)
			{
				this.popUpYesNo.cmdYes.performAction();
				GameCanvas.keyAsciiPress = 0;
				GameCanvas.clearKeyPressed();
			}
			else if (GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null)
			{
				GameCanvas.panel.setTypeMessage();
				GameCanvas.panel.show();
				GameCanvas.keyAsciiPress = 0;
				GameCanvas.clearKeyPressed();
			}
		}
		if (GameCanvas.keyPressed[10] && this.mobCapcha == null)
		{
			GameCanvas.keyPressed[10] = false;
			GameScr.info2.doClick(10);
			GameCanvas.clearKeyPressed();
		}
		this.checkDrag();
		if (!global::Char.myCharz().isFlyAndCharge)
		{
			this.checkClick();
		}
		if (global::Char.myCharz().cmdMenu != null && global::Char.myCharz().cmdMenu.isPointerPressInside())
		{
			global::Char.myCharz().cmdMenu.performAction();
		}
		if (global::Char.myCharz().skillPaint != null)
		{
			return;
		}
		if (GameCanvas.keyAsciiPress != 0)
		{
			if (this.mobCapcha == null)
			{
				if (TField.isQwerty)
				{
					if (GameCanvas.keyPressed[1])
					{
						if (GameScr.keySkill[0] != null)
						{
							this.doSelectSkill(GameScr.keySkill[0], true);
						}
					}
					else if (GameCanvas.keyPressed[2])
					{
						if (GameScr.keySkill[1] != null)
						{
							this.doSelectSkill(GameScr.keySkill[1], true);
						}
					}
					else if (GameCanvas.keyPressed[3])
					{
						if (GameScr.keySkill[2] != null)
						{
							this.doSelectSkill(GameScr.keySkill[2], true);
						}
					}
					else if (GameCanvas.keyPressed[4])
					{
						if (GameScr.keySkill[3] != null)
						{
							this.doSelectSkill(GameScr.keySkill[3], true);
						}
					}
					else if (GameCanvas.keyPressed[5])
					{
						if (GameScr.keySkill[4] != null)
						{
							this.doSelectSkill(GameScr.keySkill[4], true);
						}
					}
					else if (GameCanvas.keyPressed[6])
					{
						if (GameScr.keySkill[5] != null)
						{
							this.doSelectSkill(GameScr.keySkill[5], true);
						}
					}
					else if (GameCanvas.keyPressed[7])
					{
						if (GameScr.keySkill[6] != null)
						{
							this.doSelectSkill(GameScr.keySkill[6], true);
						}
					}
					else if (GameCanvas.keyPressed[8])
					{
						if (GameScr.keySkill[7] != null)
						{
							this.doSelectSkill(GameScr.keySkill[7], true);
						}
					}
					else if (GameCanvas.keyPressed[9])
					{
						if (GameScr.keySkill[8] != null)
						{
							this.doSelectSkill(GameScr.keySkill[8], true);
						}
					}
					else if (GameCanvas.keyPressed[0])
					{
						if (GameScr.keySkill[9] != null)
						{
							this.doSelectSkill(GameScr.keySkill[9], true);
						}
					}
					else if (GameCanvas.keyAsciiPress == 114)
					{
						ChatTextField.gI().startChat(this, string.Empty);
					}
				}
				else if (!GameCanvas.isMoveNumberPad)
				{
					ChatTextField.gI().startChat(GameCanvas.keyAsciiPress, this, string.Empty);
				}
				else if (GameCanvas.keyAsciiPress == 55)
				{
					if (GameScr.keySkill[0] != null)
					{
						this.doSelectSkill(GameScr.keySkill[0], true);
					}
				}
				else if (GameCanvas.keyAsciiPress == 56)
				{
					if (GameScr.keySkill[1] != null)
					{
						this.doSelectSkill(GameScr.keySkill[1], true);
					}
				}
				else if (GameCanvas.keyAsciiPress == 57)
				{
					if (GameScr.keySkill[(!Main.isPC) ? 2 : 21] != null)
					{
						this.doSelectSkill(GameScr.keySkill[2], true);
					}
				}
				else if (GameCanvas.keyAsciiPress == 48)
				{
					ChatTextField.gI().startChat(this, string.Empty);
				}
			}
			else
			{
				char[] array = this.keyInput.ToCharArray();
				MyVector myVector = new MyVector();
				for (int j = 0; j < array.Length; j++)
				{
					myVector.addElement(array[j].ToString() + string.Empty);
				}
				myVector.removeElementAt(0);
				string text = ((char)GameCanvas.keyAsciiPress).ToString() + string.Empty;
				if (text.Equals(string.Empty) || text == null || text.Equals("\n"))
				{
					text = "-";
				}
				myVector.insertElementAt(text, myVector.size());
				this.keyInput = string.Empty;
				for (int k = 0; k < myVector.size(); k++)
				{
					this.keyInput += ((string)myVector.elementAt(k)).ToUpper();
				}
				Service.gI().mobCapcha((char)GameCanvas.keyAsciiPress);
			}
			GameCanvas.keyAsciiPress = 0;
		}
		if (global::Char.myCharz().statusMe == 1)
		{
			GameCanvas.debug("F10", 0);
			if (!this.doSeleckSkillFlag)
			{
				if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
				{
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
					this.doFire(false, false);
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
				{
					if (!global::Char.myCharz().isLockMove)
					{
						this.setCharJump(0);
					}
				}
				else if (GameCanvas.keyHold[1] && this.mobCapcha == null)
				{
					if (!Main.isPC)
					{
						global::Char.myCharz().cdir = -1;
						if (!global::Char.myCharz().isLockMove)
						{
							this.setCharJump(-4);
						}
					}
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 5 : 25] && this.mobCapcha == null)
				{
					if (!Main.isPC)
					{
						global::Char.myCharz().cdir = 1;
						if (!global::Char.myCharz().isLockMove)
						{
							this.setCharJump(4);
						}
					}
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
				{
					GameScr.isAutoPlay = false;
					global::Char.myCharz().isAttack = false;
					if (global::Char.myCharz().cdir == 1)
					{
						global::Char.myCharz().cdir = -1;
					}
					else if (!global::Char.myCharz().isLockMove)
					{
						if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0)
						{
							Service.gI().charMove();
						}
						global::Char.myCharz().statusMe = 2;
						global::Char.myCharz().cvx = -global::Char.myCharz().cspeed;
					}
					global::Char.myCharz().holder = false;
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
				{
					GameScr.isAutoPlay = false;
					global::Char.myCharz().isAttack = false;
					if (global::Char.myCharz().cdir == -1)
					{
						global::Char.myCharz().cdir = 1;
					}
					else if (!global::Char.myCharz().isLockMove)
					{
						if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0)
						{
							Service.gI().charMove();
						}
						global::Char.myCharz().statusMe = 2;
						global::Char.myCharz().cvx = global::Char.myCharz().cspeed;
					}
					global::Char.myCharz().holder = false;
				}
			}
		}
		else if (global::Char.myCharz().statusMe == 2)
		{
			GameCanvas.debug("F11", 0);
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				this.doFire(false, true);
			}
			else if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
			{
				if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0)
				{
					Service.gI().charMove();
				}
				global::Char.myCharz().cvy = -10;
				global::Char.myCharz().statusMe = 3;
				global::Char.myCharz().cp1 = 0;
			}
			else if (GameCanvas.keyHold[1] && this.mobCapcha == null)
			{
				if (Main.isPC)
				{
					if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0)
					{
						Service.gI().charMove();
					}
					global::Char.myCharz().cdir = -1;
					global::Char.myCharz().cvy = -10;
					global::Char.myCharz().cvx = -4;
					global::Char.myCharz().statusMe = 3;
					global::Char.myCharz().cp1 = 0;
				}
			}
			else if (GameCanvas.keyHold[3] && this.mobCapcha == null)
			{
				if (!Main.isPC)
				{
					if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0)
					{
						Service.gI().charMove();
					}
					global::Char.myCharz().cdir = 1;
					global::Char.myCharz().cvy = -10;
					global::Char.myCharz().cvx = 4;
					global::Char.myCharz().statusMe = 3;
					global::Char.myCharz().cp1 = 0;
				}
			}
			else if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
			{
				GameScr.isAutoPlay = false;
				if (global::Char.myCharz().cdir == 1)
				{
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					global::Char.myCharz().cvx = -global::Char.myCharz().cspeed + global::Char.myCharz().cBonusSpeed;
				}
			}
			else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
			{
				GameScr.isAutoPlay = false;
				if (global::Char.myCharz().cdir == -1)
				{
					global::Char.myCharz().cdir = 1;
				}
				else
				{
					global::Char.myCharz().cvx = global::Char.myCharz().cspeed + global::Char.myCharz().cBonusSpeed;
				}
			}
		}
		else if (global::Char.myCharz().statusMe == 3)
		{
			GameScr.isAutoPlay = false;
			GameCanvas.debug("F12", 0);
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				this.doFire(false, true);
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] || (GameCanvas.keyHold[1] && this.mobCapcha == null))
			{
				if (global::Char.myCharz().cdir == 1)
				{
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					global::Char.myCharz().cvx = -global::Char.myCharz().cspeed;
				}
			}
			else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] || (GameCanvas.keyHold[3] && this.mobCapcha == null))
			{
				if (global::Char.myCharz().cdir == -1)
				{
					global::Char.myCharz().cdir = 1;
				}
				else
				{
					global::Char.myCharz().cvx = global::Char.myCharz().cspeed;
				}
			}
			if ((GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] || ((GameCanvas.keyHold[1] || GameCanvas.keyHold[3]) && this.mobCapcha == null)) && global::Char.myCharz().canFly && global::Char.myCharz().cMP > 0 && global::Char.myCharz().cp1 < 8 && global::Char.myCharz().cvy > -4)
			{
				global::Char.myCharz().cp1++;
				global::Char.myCharz().cvy = -7;
			}
		}
		else if (global::Char.myCharz().statusMe == 4)
		{
			GameCanvas.debug("F13", 0);
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				this.doFire(false, true);
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] && global::Char.myCharz().cMP > 0 && global::Char.myCharz().canFly)
			{
				GameScr.isAutoPlay = false;
				if ((global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24))
				{
					Service.gI().charMove();
				}
				global::Char.myCharz().cvy = -10;
				global::Char.myCharz().statusMe = 3;
				global::Char.myCharz().cp1 = 0;
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
			{
				GameScr.isAutoPlay = false;
				if (global::Char.myCharz().cdir == 1)
				{
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					global::Char.myCharz().cp1++;
					global::Char.myCharz().cvx = -global::Char.myCharz().cspeed;
					if (global::Char.myCharz().cp1 > 5 && global::Char.myCharz().cvy > 6)
					{
						global::Char.myCharz().statusMe = 10;
						global::Char.myCharz().cp1 = 0;
						global::Char.myCharz().cvy = 0;
					}
				}
			}
			else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
			{
				GameScr.isAutoPlay = false;
				if (global::Char.myCharz().cdir == -1)
				{
					global::Char.myCharz().cdir = 1;
				}
				else
				{
					global::Char.myCharz().cp1++;
					global::Char.myCharz().cvx = global::Char.myCharz().cspeed;
					if (global::Char.myCharz().cp1 > 5 && global::Char.myCharz().cvy > 6)
					{
						global::Char.myCharz().statusMe = 10;
						global::Char.myCharz().cp1 = 0;
						global::Char.myCharz().cvy = 0;
					}
				}
			}
		}
		else if (global::Char.myCharz().statusMe == 10)
		{
			GameCanvas.debug("F14", 0);
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				this.doFire(false, true);
			}
			if (global::Char.myCharz().canFly && global::Char.myCharz().cMP > 0)
			{
				if (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21])
				{
					GameScr.isAutoPlay = false;
					if ((global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24))
					{
						Service.gI().charMove();
					}
					global::Char.myCharz().cvy = -10;
					global::Char.myCharz().statusMe = 3;
					global::Char.myCharz().cp1 = 0;
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
				{
					GameScr.isAutoPlay = false;
					if (global::Char.myCharz().cdir == 1)
					{
						global::Char.myCharz().cdir = -1;
					}
					else
					{
						global::Char.myCharz().cvx = -(global::Char.myCharz().cspeed + 1);
					}
				}
				else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
				{
					if (global::Char.myCharz().cdir == -1)
					{
						global::Char.myCharz().cdir = 1;
					}
					else
					{
						global::Char.myCharz().cvx = global::Char.myCharz().cspeed + 1;
					}
				}
			}
		}
		else if (global::Char.myCharz().statusMe == 7)
		{
			GameCanvas.debug("F15", 0);
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25])
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
			}
			if (GameCanvas.keyHold[(!Main.isPC) ? 4 : 23])
			{
				GameScr.isAutoPlay = false;
				if (global::Char.myCharz().cdir == 1)
				{
					global::Char.myCharz().cdir = -1;
				}
				else
				{
					global::Char.myCharz().cvx = -global::Char.myCharz().cspeed + 2;
				}
			}
			else if (GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
			{
				GameScr.isAutoPlay = false;
				if (global::Char.myCharz().cdir == -1)
				{
					global::Char.myCharz().cdir = 1;
				}
				else
				{
					global::Char.myCharz().cvx = global::Char.myCharz().cspeed - 2;
				}
			}
		}
		GameCanvas.debug("F17", 0);
		if (GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] && GameCanvas.keyAsciiPress != 56)
		{
			GameCanvas.keyPressed[(!Main.isPC) ? 8 : 22] = false;
			global::Char.myCharz().delayFall = 0;
		}
		if (GameCanvas.keyPressed[10])
		{
			GameCanvas.keyPressed[10] = false;
			this.doUseHP();
		}
		GameCanvas.debug("F20", 0);
		GameCanvas.clearKeyPressed();
		GameCanvas.debug("F23", 0);
		this.doSeleckSkillFlag = false;
	}

	// Token: 0x06000378 RID: 888 RVA: 0x00039302 File Offset: 0x00037502
	public bool isVsMap()
	{
		return true;
	}

	// Token: 0x06000379 RID: 889 RVA: 0x0003FA80 File Offset: 0x0003DC80
	internal void checkDrag()
	{
		if (GameScr.isAnalog == 1 || GameScr.gamePad.disableCheckDrag())
		{
			return;
		}
		global::Char.myCharz().cmtoChar = true;
		if (GameScr.isUseTouch)
		{
			return;
		}
		if (GameCanvas.isPointerJustDown)
		{
			GameCanvas.isPointerJustDown = false;
			this.isPointerDowning = true;
			this.ptDownTime = 0;
			this.ptLastDownX = (this.ptFirstDownX = GameCanvas.px);
			this.ptLastDownY = (this.ptFirstDownY = GameCanvas.py);
		}
		if (this.isPointerDowning)
		{
			int num = GameCanvas.px - this.ptLastDownX;
			int num2 = GameCanvas.py - this.ptLastDownY;
			if (!this.isChangingCameraMode && (Res.abs(GameCanvas.px - this.ptFirstDownX) > 15 || Res.abs(GameCanvas.py - this.ptFirstDownY) > 15))
			{
				this.isChangingCameraMode = true;
			}
			this.ptLastDownX = GameCanvas.px;
			this.ptLastDownY = GameCanvas.py;
			this.ptDownTime++;
			if (this.isChangingCameraMode)
			{
				global::Char.myCharz().cmtoChar = false;
				GameScr.cmx -= num;
				GameScr.cmy -= num2;
				if (GameScr.cmx < 24)
				{
					int num3 = (24 - GameScr.cmx) / 3;
					if (num3 != 0)
					{
						GameScr.cmx += num - num / num3;
					}
				}
				if (GameScr.cmx < (this.isVsMap() ? 24 : 0))
				{
					GameScr.cmx = (this.isVsMap() ? 24 : 0);
				}
				if (GameScr.cmx > GameScr.cmxLim)
				{
					int num4 = (GameScr.cmx - GameScr.cmxLim) / 3;
					if (num4 != 0)
					{
						GameScr.cmx += num - num / num4;
					}
				}
				if (GameScr.cmx > GameScr.cmxLim + ((!this.isVsMap()) ? 24 : 0))
				{
					GameScr.cmx = GameScr.cmxLim + ((!this.isVsMap()) ? 24 : 0);
				}
				if (GameScr.cmy < 0)
				{
					int num5 = -GameScr.cmy / 3;
					if (num5 != 0)
					{
						GameScr.cmy += num2 - num2 / num5;
					}
				}
				if (GameScr.cmy < -((!this.isVsMap()) ? 24 : 0))
				{
					GameScr.cmy = -((!this.isVsMap()) ? 24 : 0);
				}
				if (GameScr.cmy > GameScr.cmyLim)
				{
					GameScr.cmy = GameScr.cmyLim;
				}
				GameScr.cmtoX = GameScr.cmx;
				GameScr.cmtoY = GameScr.cmy;
			}
		}
		if (this.isPointerDowning && GameCanvas.isPointerJustRelease)
		{
			this.isPointerDowning = false;
			this.isChangingCameraMode = false;
			if (Res.abs(GameCanvas.px - this.ptFirstDownX) > 15 || Res.abs(GameCanvas.py - this.ptFirstDownY) > 15)
			{
				GameCanvas.isPointerJustRelease = false;
			}
		}
	}

	// Token: 0x0600037A RID: 890 RVA: 0x0003FD20 File Offset: 0x0003DF20
	internal void checkClick()
	{
		if (this.isCharging())
		{
			return;
		}
		if (this.popUpYesNo != null && this.popUpYesNo.cmdYes != null && this.popUpYesNo.cmdYes.isPointerPressInside())
		{
			this.popUpYesNo.cmdYes.performAction();
			return;
		}
		if (this.checkClickToCapcha())
		{
			return;
		}
		long num = mSystem.currentTimeMillis();
		if (this.lastSingleClick != 0L)
		{
			this.lastSingleClick = 0L;
			GameCanvas.isPointerJustDown = false;
			if (!this.disableSingleClick)
			{
				this.checkSingleClick();
				GameCanvas.isPointerJustRelease = false;
				this.isWaitingDoubleClick = true;
				this.timeStartDblClick = mSystem.currentTimeMillis();
			}
		}
		if (this.isWaitingDoubleClick)
		{
			this.timeEndDblClick = mSystem.currentTimeMillis();
			if (this.timeEndDblClick - this.timeStartDblClick < 300L && GameCanvas.isPointerJustRelease)
			{
				this.isWaitingDoubleClick = false;
				this.checkDoubleClick();
			}
		}
		if (GameCanvas.isPointerJustRelease)
		{
			this.disableSingleClick = this.checkSingleClickEarly();
			this.lastSingleClick = num;
			this.lastClickCMX = GameScr.cmx;
			this.lastClickCMY = GameScr.cmy;
			GameCanvas.isPointerJustRelease = false;
		}
	}

	// Token: 0x0600037B RID: 891 RVA: 0x0003FE2C File Offset: 0x0003E02C
	internal IMapObject findClickToItem(int px, int py)
	{
		IMapObject mapObject = null;
		int num = 0;
		int num2 = 30;
		MyVector[] array = new MyVector[]
		{
			GameScr.vMob,
			GameScr.vNpc,
			GameScr.vItemMap,
			GameScr.vCharInMap
		};
		for (int i = 0; i < array.Length; i++)
		{
			for (int j = 0; j < array[i].size(); j++)
			{
				IMapObject mapObject2 = (IMapObject)array[i].elementAt(j);
				if (!mapObject2.isInvisible())
				{
					if (mapObject2 is Mob)
					{
						Mob mob = (Mob)mapObject2;
						if (mob.isMobMe && mob.Equals(global::Char.myCharz().mobMe))
						{
							goto IL_0118;
						}
					}
					int x = mapObject2.getX();
					int y = mapObject2.getY();
					int w = mapObject2.getW();
					int h = mapObject2.getH();
					if (this.inRectangle(px, py, x - w / 2 - num2, y - h - num2, w + num2 * 2, h + num2 * 2))
					{
						if (mapObject == null)
						{
							mapObject = mapObject2;
							num = Res.abs(px - x) + Res.abs(py - y);
							if (i == 1)
							{
								return mapObject;
							}
						}
						else
						{
							int num3 = Res.abs(px - x) + Res.abs(py - y);
							if (num3 < num)
							{
								mapObject = mapObject2;
								num = num3;
							}
						}
					}
				}
				IL_0118:;
			}
		}
		return mapObject;
	}

	// Token: 0x0600037C RID: 892 RVA: 0x0003FF78 File Offset: 0x0003E178
	internal Mob findClickToMOB(int px, int py)
	{
		int num = 30;
		Mob mob = null;
		int num2 = 0;
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob2 = (Mob)GameScr.vMob.elementAt(i);
			if (!mob2.isInvisible())
			{
				if (mob2 != null)
				{
					Mob mob3 = mob2;
					if (mob3.isMobMe && mob3.Equals(global::Char.myCharz().mobMe))
					{
						goto IL_00D9;
					}
				}
				int x = mob2.getX();
				int y = mob2.getY();
				int w = mob2.getW();
				int h = mob2.getH();
				if (this.inRectangle(px, py, x - w / 2 - num, y - h - num, w + num * 2, h + num * 2))
				{
					if (mob == null)
					{
						mob = mob2;
						num2 = Res.abs(px - x) + Res.abs(py - y);
					}
					else
					{
						int num3 = Res.abs(px - x) + Res.abs(py - y);
						if (num3 < num2)
						{
							mob = mob2;
							num2 = num3;
						}
					}
				}
			}
			IL_00D9:;
		}
		return mob;
	}

	// Token: 0x0600037D RID: 893 RVA: 0x00040073 File Offset: 0x0003E273
	internal bool inRectangle(int xClick, int yClick, int x, int y, int w, int h)
	{
		return xClick >= x && xClick <= x + w && yClick >= y && yClick <= y + h;
	}

	// Token: 0x0600037E RID: 894 RVA: 0x00040094 File Offset: 0x0003E294
	internal bool checkSingleClickEarly()
	{
		int num = GameCanvas.px + GameScr.cmx;
		int num2 = GameCanvas.py + GameScr.cmy;
		global::Char.myCharz().cancelAttack();
		IMapObject mapObject = this.findClickToItem(num, num2);
		if (mapObject == null)
		{
			return false;
		}
		if (global::Char.myCharz().isAttacPlayerStatus() && global::Char.myCharz().charFocus != null && !mapObject.Equals(global::Char.myCharz().charFocus) && !mapObject.Equals(global::Char.myCharz().charFocus.mobMe) && mapObject is global::Char)
		{
			global::Char @char = (global::Char)mapObject;
			if (@char.cTypePk != 5 && !@char.isAttacPlayerStatus())
			{
				this.checkClickMoveTo(num, num2, 2);
				return false;
			}
		}
		if (global::Char.myCharz().mobFocus == mapObject || global::Char.myCharz().itemFocus == mapObject)
		{
			this.doDoubleClickToObj(mapObject);
			return true;
		}
		if (TileMap.mapID == 51 && mapObject.Equals(global::Char.myCharz().npcFocus))
		{
			this.checkClickMoveTo(num, num2, 3);
			return false;
		}
		if (global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null || global::Char.myCharz().skillInfoPaint() != null)
		{
			return false;
		}
		global::Char.myCharz().focusManualTo(mapObject);
		mapObject.stopMoving();
		return false;
	}

	// Token: 0x0600037F RID: 895 RVA: 0x000401D0 File Offset: 0x0003E3D0
	internal void checkDoubleClick()
	{
		int num = GameCanvas.px + this.lastClickCMX;
		int num2 = GameCanvas.py + this.lastClickCMY;
		int cy = global::Char.myCharz().cy;
		if (this.isLockKey)
		{
			return;
		}
		IMapObject mapObject = this.findClickToItem(num, num2);
		if (mapObject == null)
		{
			if (!this.checkClickToPopup(num, num2) && !this.checkClipTopChatPopUp(num, num2) && !Main.isPC)
			{
				this.checkClickMoveTo(num, num2, 7);
			}
			return;
		}
		if (mapObject is Mob && !this.isMeCanAttackMob((Mob)mapObject))
		{
			this.checkClickMoveTo(num, num2, 4);
			return;
		}
		if (this.checkClickToBotton(mapObject) || (!mapObject.Equals(global::Char.myCharz().npcFocus) && this.mobCapcha != null))
		{
			return;
		}
		if (global::Char.myCharz().isAttacPlayerStatus() && global::Char.myCharz().charFocus != null && !mapObject.Equals(global::Char.myCharz().charFocus) && !mapObject.Equals(global::Char.myCharz().charFocus.mobMe) && mapObject is global::Char)
		{
			global::Char @char = (global::Char)mapObject;
			if (@char.cTypePk != 5 && !@char.isAttacPlayerStatus())
			{
				this.checkClickMoveTo(num, num2, 5);
				return;
			}
		}
		if (TileMap.mapID == 51 && mapObject.Equals(global::Char.myCharz().npcFocus))
		{
			this.checkClickMoveTo(num, num2, 6);
			return;
		}
		this.doDoubleClickToObj(mapObject);
	}

	// Token: 0x06000380 RID: 896 RVA: 0x0004031C File Offset: 0x0003E51C
	internal bool checkClickToBotton(IMapObject Object)
	{
		if (Object == null)
		{
			return false;
		}
		int i = Object.getY();
		int num = global::Char.myCharz().cy;
		if (i < num)
		{
			while (i < num)
			{
				num -= 5;
				if (TileMap.tileTypeAt(global::Char.myCharz().cx, num, 8192))
				{
					this.auto = 0;
					global::Char.myCharz().cancelAttack();
					global::Char.myCharz().currentMovePoint = null;
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000381 RID: 897 RVA: 0x00040384 File Offset: 0x0003E584
	internal void doDoubleClickToObj(IMapObject obj)
	{
		if ((obj.Equals(global::Char.myCharz().npcFocus) || this.mobCapcha == null) && !this.checkClickToBotton(obj))
		{
			this.checkEffToObj(obj, false);
			global::Char.myCharz().cancelAttack();
			global::Char.myCharz().currentMovePoint = null;
			global::Char.myCharz().cvx = (global::Char.myCharz().cvy = 0);
			obj.stopMoving();
			this.auto = 10;
			this.doFire(false, true);
			this.clickToX = obj.getX();
			this.clickToY = obj.getY();
			this.clickOnTileTop = false;
			this.clickMoving = true;
			this.clickMovingRed = true;
			this.clickMovingTimeOut = 20;
			this.clickMovingP1 = 30;
		}
	}

	// Token: 0x06000382 RID: 898 RVA: 0x00040444 File Offset: 0x0003E644
	internal void checkSingleClick()
	{
		int num = GameCanvas.px + this.lastClickCMX;
		int num2 = GameCanvas.py + this.lastClickCMY;
		if (!this.isLockKey && !this.checkClickToPopup(num, num2) && !this.checkClipTopChatPopUp(num, num2))
		{
			this.checkClickMoveTo(num, num2, 0);
		}
	}

	// Token: 0x06000383 RID: 899 RVA: 0x00040490 File Offset: 0x0003E690
	internal bool checkClipTopChatPopUp(int xClick, int yClick)
	{
		if (this.Equals(GameScr.info2) && GameScr.gI().popUpYesNo != null)
		{
			return false;
		}
		if (GameScr.info2.info.info != null && GameScr.info2.info.info.charInfo != null)
		{
			int num = Res.abs(GameScr.info2.cmx) + GameScr.info2.info.X - 40;
			int num2 = Res.abs(GameScr.info2.cmy) + GameScr.info2.info.Y;
			if (this.inRectangle(xClick - GameScr.cmx, yClick - GameScr.cmy, num, num2, 200, GameScr.info2.info.H))
			{
				GameScr.info2.doClick(10);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000384 RID: 900 RVA: 0x00040568 File Offset: 0x0003E768
	internal bool checkClickToPopup(int xClick, int yClick)
	{
		for (int i = 0; i < PopUp.vPopups.size(); i++)
		{
			PopUp popUp = (PopUp)PopUp.vPopups.elementAt(i);
			if (this.inRectangle(xClick, yClick, popUp.cx, popUp.cy, popUp.cw, popUp.ch))
			{
				if (popUp.cy <= 24 && TileMap.isInAirMap() && global::Char.myCharz().cTypePk != 0)
				{
					return false;
				}
				if (popUp.isPaint)
				{
					popUp.doClick(10);
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x06000385 RID: 901 RVA: 0x000405F0 File Offset: 0x0003E7F0
	internal void checkClickMoveTo(int xClick, int yClick, int index)
	{
		if (GameScr.gamePad.disableClickMove())
		{
			return;
		}
		global::Char.myCharz().cancelAttack();
		if (xClick < TileMap.pxw && xClick > TileMap.pxw - 32)
		{
			global::Char.myCharz().currentMovePoint = new MovePoint(TileMap.pxw, yClick);
			return;
		}
		if (xClick < 32 && xClick > 0)
		{
			global::Char.myCharz().currentMovePoint = new MovePoint(0, yClick);
			return;
		}
		if (xClick < TileMap.pxw && xClick > TileMap.pxw - 48)
		{
			global::Char.myCharz().currentMovePoint = new MovePoint(TileMap.pxw, yClick);
			return;
		}
		if (xClick < 48 && xClick > 0)
		{
			global::Char.myCharz().currentMovePoint = new MovePoint(0, yClick);
			return;
		}
		this.clickToX = xClick;
		this.clickToY = yClick;
		this.clickOnTileTop = false;
		global::Char.myCharz().delayFall = 0;
		int num = ((!global::Char.myCharz().canFly || global::Char.myCharz().cMP <= 0) ? 1000 : 0);
		if (this.clickToY > global::Char.myCharz().cy && Res.abs(this.clickToX - global::Char.myCharz().cx) < 12)
		{
			return;
		}
		int num2 = 0;
		while (num2 < 60 + num && this.clickToY + num2 < TileMap.pxh - 24)
		{
			if (TileMap.tileTypeAt(this.clickToX, this.clickToY + num2, 2))
			{
				this.clickToY = TileMap.tileYofPixel(this.clickToY + num2);
				this.clickOnTileTop = true;
				break;
			}
			num2 += 24;
		}
		for (int i = 0; i < 40 + num; i += 24)
		{
			if (TileMap.tileTypeAt(this.clickToX, this.clickToY - i, 2))
			{
				this.clickToY = TileMap.tileYofPixel(this.clickToY - i);
				this.clickOnTileTop = true;
				break;
			}
		}
		this.clickMoving = true;
		this.clickMovingRed = false;
		this.clickMovingP1 = ((!this.clickOnTileTop) ? 30 : ((yClick >= this.clickToY) ? this.clickToY : yClick));
		global::Char.myCharz().delayFall = 0;
		if (!this.clickOnTileTop && this.clickToY < global::Char.myCharz().cy - 50)
		{
			global::Char.myCharz().delayFall = 20;
		}
		this.clickMovingTimeOut = 30;
		this.auto = 0;
		if (global::Char.myCharz().holder)
		{
			global::Char.myCharz().removeHoleEff();
		}
		global::Char.myCharz().currentMovePoint = new MovePoint(this.clickToX, this.clickToY);
		global::Char.myCharz().cdir = ((global::Char.myCharz().cx - global::Char.myCharz().currentMovePoint.xEnd <= 0) ? 1 : (-1));
		global::Char.myCharz().endMovePointCommand = null;
		GameScr.isAutoPlay = false;
	}

	// Token: 0x06000386 RID: 902 RVA: 0x00040884 File Offset: 0x0003EA84
	internal void checkAuto()
	{
		long num = mSystem.currentTimeMillis();
		if (GameCanvas.keyPressed[(!Main.isPC) ? 2 : 21] || GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] || GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] || GameCanvas.keyPressed[1] || GameCanvas.keyPressed[3])
		{
			this.auto = 0;
			GameScr.isAutoPlay = false;
		}
		if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && !this.isPaintPopup())
		{
			if (this.auto == 0)
			{
				if (num - this.lastFire < 800L && this.checkSkillValid2() && (global::Char.myCharz().mobFocus != null || (global::Char.myCharz().charFocus != null && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus))))
				{
					Res.outz("toi day");
					this.auto = 10;
					GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = false;
				}
			}
			else
			{
				this.auto = 0;
				GameCanvas.keyPressed[(!Main.isPC) ? 4 : 23] = (GameCanvas.keyPressed[(!Main.isPC) ? 6 : 24] = false);
			}
			this.lastFire = num;
		}
		if (GameCanvas.gameTick % 5 == 0 && this.auto > 0 && global::Char.myCharz().currentMovePoint == null)
		{
			if (global::Char.myCharz().myskill != null && (global::Char.myCharz().myskill.template.isUseAlone() || global::Char.myCharz().myskill.paintCanNotUseSkill))
			{
				return;
			}
			if ((global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.status != 1 && global::Char.myCharz().mobFocus.status != 0 && global::Char.myCharz().charFocus == null) || (global::Char.myCharz().charFocus != null && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus)))
			{
				if (global::Char.myCharz().myskill.paintCanNotUseSkill)
				{
					return;
				}
				this.doFire(false, true);
			}
		}
		if (this.auto > 1)
		{
			this.auto--;
		}
	}

	// Token: 0x06000387 RID: 903 RVA: 0x00040AB0 File Offset: 0x0003ECB0
	public void doUseHP()
	{
		if (global::Char.myCharz().stone || global::Char.myCharz().blindEff || global::Char.myCharz().holdEffID > 0)
		{
			return;
		}
		long num = mSystem.currentTimeMillis();
		if (num - this.lastUsePotion >= 10000L)
		{
			if (!global::Char.myCharz().doUsePotion())
			{
				GameScr.info1.addInfo(mResources.HP_EMPTY, 0);
				return;
			}
			ServerEffect.addServerEffect(11, global::Char.myCharz(), 5);
			ServerEffect.addServerEffect(104, global::Char.myCharz(), 4);
			this.lastUsePotion = num;
			SoundMn.gI().eatPeans();
		}
	}

	// Token: 0x06000388 RID: 904 RVA: 0x00040B44 File Offset: 0x0003ED44
	public void activeSuperPower(int x, int y)
	{
		if (!this.isSuperPower)
		{
			SoundMn.gI().bigeExlode();
			this.isSuperPower = true;
			this.tPower = 0;
			this.dxPower = 0;
			this.xPower = x - GameScr.cmx;
			this.yPower = y - GameScr.cmy;
		}
	}

	// Token: 0x06000389 RID: 905 RVA: 0x00040B92 File Offset: 0x0003ED92
	public void activeRongThanEff(bool isMe)
	{
		this.activeRongThan = true;
		this.isUseFreez = true;
		this.isMeCallRongThan = true;
		if (isMe)
		{
			EffecMn.addEff(new Effect(20, global::Char.myCharz().cx, global::Char.myCharz().cy - 77, 2, 8, 1));
		}
	}

	// Token: 0x0600038A RID: 906 RVA: 0x00040BD2 File Offset: 0x0003EDD2
	public void hideRongThanEff()
	{
		this.activeRongThan = false;
		this.isUseFreez = true;
		this.isMeCallRongThan = false;
	}

	// Token: 0x0600038B RID: 907 RVA: 0x00040BE9 File Offset: 0x0003EDE9
	public void doiMauTroi()
	{
		this.isRongThanXuatHien = true;
		this.mautroi = mGraphics.blendColor(0.4f, 0, GameCanvas.colorTop[GameCanvas.colorTop.Length - 1]);
	}

	// Token: 0x0600038C RID: 908 RVA: 0x00040C14 File Offset: 0x0003EE14
	public void callRongThan(int x, int y)
	{
		Res.outz("VE RONG THAN O VI TRI x= " + x.ToString() + " y=" + y.ToString());
		this.doiMauTroi();
		EffecMn.addEff(new Effect((!this.isRongNamek) ? 17 : 25, x, y - 77, 2, -1, 1));
	}

	// Token: 0x0600038D RID: 909 RVA: 0x00040C69 File Offset: 0x0003EE69
	public void hideRongThan()
	{
		this.isRongThanXuatHien = false;
		EffecMn.removeEff(17);
		if (this.isRongNamek)
		{
			this.isRongNamek = false;
			EffecMn.removeEff(25);
		}
	}

	// Token: 0x0600038E RID: 910 RVA: 0x00040C90 File Offset: 0x0003EE90
	internal void autoPlay()
	{
		if (this.timeSkill > 0)
		{
			this.timeSkill--;
		}
		if (!GameScr.canAutoPlay || GameScr.isChangeZone || global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5 || global::Char.myCharz().isCharge || global::Char.myCharz().isFlyAndCharge || global::Char.myCharz().isUseChargeSkill())
		{
			return;
		}
		bool flag = false;
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob.status != 0 && mob.status != 1)
			{
				flag = true;
			}
		}
		if (!flag)
		{
			return;
		}
		bool flag2 = false;
		for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
		{
			Item item = global::Char.myCharz().arrItemBag[j];
			if (item != null && item.template.type == 6)
			{
				flag2 = true;
				break;
			}
		}
		if (!flag2 && GameCanvas.gameTick % 150 == 0)
		{
			Service.gI().requestPean();
		}
		if (global::Char.myCharz().cHP <= global::Char.myCharz().cHPFull * 20 / 100 || global::Char.myCharz().cMP <= global::Char.myCharz().cMPFull * 20 / 100)
		{
			this.doUseHP();
		}
		if (global::Char.myCharz().mobFocus == null || (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.isMobMe))
		{
			for (int k = 0; k < GameScr.vMob.size(); k++)
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(k);
				if (mob2.status != 0 && mob2.status != 1 && mob2.hp > 0 && !mob2.isMobMe)
				{
					global::Char.myCharz().cx = mob2.x;
					global::Char.myCharz().cy = mob2.y;
					global::Char.myCharz().mobFocus = mob2;
					Service.gI().charMove();
					break;
				}
			}
		}
		else if (global::Char.myCharz().mobFocus.hp <= 0 || global::Char.myCharz().mobFocus.status == 1 || global::Char.myCharz().mobFocus.status == 0)
		{
			global::Char.myCharz().mobFocus = null;
		}
		if (global::Char.myCharz().mobFocus == null || this.timeSkill != 0 || (global::Char.myCharz().skillInfoPaint() != null && global::Char.myCharz().indexSkill < global::Char.myCharz().skillInfoPaint().Length && global::Char.myCharz().dart != null && global::Char.myCharz().arr != null))
		{
			return;
		}
		Skill skill = null;
		if (GameCanvas.isTouch)
		{
			for (int l = 0; l < GameScr.onScreenSkill.Length; l++)
			{
				if (GameScr.onScreenSkill[l] != null && !GameScr.onScreenSkill[l].paintCanNotUseSkill && GameScr.onScreenSkill[l].template.id != 10 && GameScr.onScreenSkill[l].template.id != 11 && GameScr.onScreenSkill[l].template.id != 14 && GameScr.onScreenSkill[l].template.id != 23 && GameScr.onScreenSkill[l].template.id != 7 && global::Char.myCharz().skillInfoPaint() == null && !GameScr.onScreenSkill[l].template.isSkillSpec())
				{
					int num = ((GameScr.onScreenSkill[l].template.manaUseType == 2) ? 1 : ((GameScr.onScreenSkill[l].template.manaUseType == 1) ? (GameScr.onScreenSkill[l].manaUse * global::Char.myCharz().cMPFull / 100) : GameScr.onScreenSkill[l].manaUse));
					if (global::Char.myCharz().cMP >= num)
					{
						if (skill == null)
						{
							skill = GameScr.onScreenSkill[l];
						}
						else if (skill.coolDown < GameScr.onScreenSkill[l].coolDown)
						{
							skill = GameScr.onScreenSkill[l];
						}
					}
				}
			}
			if (skill != null)
			{
				this.doSelectSkill(skill, true);
				this.doDoubleClickToObj(global::Char.myCharz().mobFocus);
			}
			return;
		}
		for (int m = 0; m < GameScr.keySkill.Length; m++)
		{
			if (GameScr.keySkill[m] != null && !GameScr.keySkill[m].paintCanNotUseSkill && GameScr.keySkill[m].template.id != 10 && GameScr.keySkill[m].template.id != 11 && GameScr.keySkill[m].template.id != 14 && GameScr.keySkill[m].template.id != 23 && GameScr.keySkill[m].template.id != 7 && global::Char.myCharz().skillInfoPaint() == null)
			{
				int num2 = ((GameScr.keySkill[m].template.manaUseType == 2) ? 1 : ((GameScr.keySkill[m].template.manaUseType == 1) ? (GameScr.keySkill[m].manaUse * global::Char.myCharz().cMPFull / 100) : GameScr.keySkill[m].manaUse));
				if (global::Char.myCharz().cMP >= num2)
				{
					if (skill == null)
					{
						skill = GameScr.keySkill[m];
					}
					else if (skill.coolDown < GameScr.keySkill[m].coolDown)
					{
						skill = GameScr.keySkill[m];
					}
				}
			}
		}
		if (skill != null)
		{
			this.doSelectSkill(skill, true);
			this.doDoubleClickToObj(global::Char.myCharz().mobFocus);
		}
	}

	// Token: 0x0600038F RID: 911 RVA: 0x00041244 File Offset: 0x0003F444
	internal void doFire(bool isFireByShortCut, bool skipWaypoint)
	{
		GameScr.tam++;
		Waypoint waypoint = global::Char.myCharz().isInEnterOfflinePoint();
		Waypoint waypoint2 = global::Char.myCharz().isInEnterOnlinePoint();
		if (!skipWaypoint && waypoint != null && (global::Char.myCharz().mobFocus == null || (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.templateId == 0)))
		{
			waypoint.popup.command.performAction();
			return;
		}
		if (!skipWaypoint && waypoint2 != null && (global::Char.myCharz().mobFocus == null || (global::Char.myCharz().mobFocus != null && global::Char.myCharz().mobFocus.templateId == 0)))
		{
			waypoint2.popup.command.performAction();
			return;
		}
		if ((TileMap.mapID == 51 && global::Char.myCharz().npcFocus != null) || global::Char.myCharz().statusMe == 14)
		{
			return;
		}
		global::Char.myCharz().cvx = (global::Char.myCharz().cvy = 0);
		if (global::Char.myCharz().isSelectingSkillUseAlone() && global::Char.myCharz().focusToAttack())
		{
			if (this.checkSkillValid())
			{
				global::Char.myCharz().currentFireByShortcut = isFireByShortCut;
				global::Char.myCharz().useSkillNotFocus();
			}
		}
		else if (this.isAttack())
		{
			if (global::Char.myCharz().isUseChargeSkill() && global::Char.myCharz().focusToAttack())
			{
				if (this.checkSkillValid())
				{
					global::Char.myCharz().currentFireByShortcut = isFireByShortCut;
					global::Char.myCharz().sendUseChargeSkill();
				}
				else
				{
					global::Char.myCharz().stopUseChargeSkill();
				}
			}
			else
			{
				bool flag = TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
				global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!flag) ? 1 : 0);
				if (flag)
				{
					global::Char.myCharz().delayFall = 20;
				}
				global::Char.myCharz().currentFireByShortcut = isFireByShortCut;
			}
		}
		if (global::Char.myCharz().isSelectingSkillBuffToPlayer())
		{
			this.auto = 0;
		}
	}

	// Token: 0x06000390 RID: 912 RVA: 0x00041428 File Offset: 0x0003F628
	internal void askToPick()
	{
		Npc npc = new Npc(5, 0, -100, 100, 5, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
		string nhatvatpham = mResources.nhatvatpham;
		string[] array = new string[]
		{
			mResources.YES,
			mResources.NO
		};
		npc.idItem = 673;
		GameScr.gI().createMenu(array, npc);
		ChatPopup.addChatPopupWithIcon(nhatvatpham, 100000, npc, 5820);
	}

	// Token: 0x06000391 RID: 913 RVA: 0x000414A0 File Offset: 0x0003F6A0
	internal void pickItem()
	{
		if (global::Char.myCharz().itemFocus == null)
		{
			return;
		}
		if (global::Char.myCharz().cx < global::Char.myCharz().itemFocus.x)
		{
			global::Char.myCharz().cdir = 1;
		}
		else
		{
			global::Char.myCharz().cdir = -1;
		}
		int num = Math2.abs(global::Char.myCharz().cx - global::Char.myCharz().itemFocus.x);
		int num2 = Math2.abs(global::Char.myCharz().cy - global::Char.myCharz().itemFocus.y);
		if (num > 40 || num2 >= 40)
		{
			global::Char.myCharz().currentMovePoint = new MovePoint(global::Char.myCharz().itemFocus.x, global::Char.myCharz().itemFocus.y);
			global::Char.myCharz().endMovePointCommand = new Command(null, null, 8002, null);
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			return;
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
		if (global::Char.myCharz().itemFocus.template.id != 673)
		{
			Service.gI().pickItem(global::Char.myCharz().itemFocus.itemMapID);
			return;
		}
		this.askToPick();
	}

	// Token: 0x06000392 RID: 914 RVA: 0x000415CC File Offset: 0x0003F7CC
	public bool isCharging()
	{
		return global::Char.myCharz().isFlyAndCharge || global::Char.myCharz().isUseSkillAfterCharge || global::Char.myCharz().isStandAndCharge || global::Char.myCharz().isWaitMonkey || this.isSuperPower || global::Char.myCharz().isFreez;
	}

	// Token: 0x06000393 RID: 915 RVA: 0x00041620 File Offset: 0x0003F820
	public void doSelectSkill(Skill skill, bool isShortcut)
	{
		if (global::Char.myCharz().isCreateDark || this.isCharging() || global::Char.myCharz().taskMaint.taskId <= 1)
		{
			return;
		}
		global::Char.myCharz().myskill = skill;
		if (this.lastSkill != skill && this.lastSkill != null)
		{
			Service.gI().selectSkill((int)skill.template.id);
			this.saveRMSCurrentSkill(skill.template.id);
			this.resetButton();
			this.lastSkill = skill;
			this.selectedIndexSkill = -1;
			GameScr.gI().auto = 0;
			return;
		}
		if (global::Char.myCharz().isUseSkillSpec())
		{
			Res.outz(">>>use skill spec: " + skill.template.id.ToString());
			global::Char.myCharz().sendNewAttack((short)skill.template.id);
			this.saveRMSCurrentSkill(skill.template.id);
			this.resetButton();
			this.lastSkill = skill;
			this.selectedIndexSkill = -1;
			GameScr.gI().auto = 0;
			return;
		}
		if (global::Char.myCharz().isSelectingSkillUseAlone())
		{
			Res.outz("use skill not focus");
			this.doUseSkillNotFocus(skill);
			this.lastSkill = skill;
			return;
		}
		this.selectedIndexSkill = -1;
		if (skill == null)
		{
			return;
		}
		Res.outz("only select skill");
		if (this.lastSkill != skill)
		{
			Service.gI().selectSkill((int)skill.template.id);
			this.saveRMSCurrentSkill(skill.template.id);
			this.resetButton();
		}
		if (global::Char.myCharz().charFocus != null || !global::Char.myCharz().isSelectingSkillBuffToPlayer())
		{
			if (global::Char.myCharz().focusToAttack())
			{
				this.doFire(isShortcut, true);
				this.doSeleckSkillFlag = true;
			}
			this.lastSkill = skill;
		}
	}

	// Token: 0x06000394 RID: 916 RVA: 0x000417D0 File Offset: 0x0003F9D0
	public void doUseSkill(Skill skill, bool isShortcut)
	{
		if ((TileMap.mapID == 112 || TileMap.mapID == 113) && global::Char.myCharz().cTypePk == 0)
		{
			return;
		}
		if (global::Char.myCharz().isSelectingSkillUseAlone())
		{
			Res.outz("HERE");
			this.doUseSkillNotFocus(skill);
			return;
		}
		this.selectedIndexSkill = -1;
		if (skill != null)
		{
			Service.gI().selectSkill((int)skill.template.id);
			this.saveRMSCurrentSkill(skill.template.id);
			this.resetButton();
			global::Char.myCharz().myskill = skill;
			this.doFire(isShortcut, true);
		}
	}

	// Token: 0x06000395 RID: 917 RVA: 0x00041864 File Offset: 0x0003FA64
	public void doUseSkillNotFocus(Skill skill)
	{
		if (((TileMap.mapID != 112 && TileMap.mapID != 113) || global::Char.myCharz().cTypePk != 0) && this.checkSkillValid())
		{
			this.selectedIndexSkill = -1;
			if (skill != null)
			{
				Service.gI().selectSkill((int)skill.template.id);
				this.saveRMSCurrentSkill(skill.template.id);
				this.resetButton();
				global::Char.myCharz().myskill = skill;
				global::Char.myCharz().useSkillNotFocus();
				global::Char.myCharz().currentFireByShortcut = true;
				this.auto = 0;
			}
		}
	}

	// Token: 0x06000396 RID: 918 RVA: 0x000418F4 File Offset: 0x0003FAF4
	public void sortSkill()
	{
		for (int i = 0; i < global::Char.myCharz().vSkillFight.size() - 1; i++)
		{
			Skill skill = (Skill)global::Char.myCharz().vSkillFight.elementAt(i);
			for (int j = i + 1; j < global::Char.myCharz().vSkillFight.size(); j++)
			{
				Skill skill2 = (Skill)global::Char.myCharz().vSkillFight.elementAt(j);
				if (skill2.template.id < skill.template.id)
				{
					Skill skill3 = skill2;
					skill2 = skill;
					skill = skill3;
					global::Char.myCharz().vSkillFight.setElementAt(skill, i);
					global::Char.myCharz().vSkillFight.setElementAt(skill2, j);
				}
			}
		}
	}

	// Token: 0x06000397 RID: 919 RVA: 0x000419AC File Offset: 0x0003FBAC
	public void updateKeyTouchCapcha()
	{
		if (this.isNotPaintTouchControl())
		{
			return;
		}
		for (int i = 0; i < this.strCapcha.Length; i++)
		{
			this.keyCapcha[i] = -1;
			if (GameCanvas.isTouchControl)
			{
				int num = (GameCanvas.w - this.strCapcha.Length * GameScr.disXC) / 2;
				int num2 = this.strCapcha.Length * GameScr.disXC;
				if (GameCanvas.isPointerHoldIn(num, GameCanvas.h - 40, num2, GameScr.disXC))
				{
					int num3 = (GameCanvas.px - num) / GameScr.disXC;
					if (i == num3)
					{
						this.keyCapcha[i] = 1;
					}
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease && i == num3)
					{
						char[] array = this.keyInput.ToCharArray();
						MyVector myVector = new MyVector();
						for (int j = 0; j < array.Length; j++)
						{
							myVector.addElement(array[j].ToString() + string.Empty);
						}
						myVector.removeElementAt(0);
						myVector.insertElementAt(this.strCapcha[i].ToString() + string.Empty, myVector.size());
						this.keyInput = string.Empty;
						for (int k = 0; k < myVector.size(); k++)
						{
							this.keyInput += ((string)myVector.elementAt(k)).ToUpper();
						}
						Service.gI().mobCapcha(this.strCapcha[i]);
					}
				}
			}
		}
	}

	// Token: 0x06000398 RID: 920 RVA: 0x00041B44 File Offset: 0x0003FD44
	public bool checkClickToCapcha()
	{
		if (this.mobCapcha == null)
		{
			return false;
		}
		int num = (GameCanvas.w - 5 * GameScr.disXC) / 2;
		int num2 = 5 * GameScr.disXC;
		return GameCanvas.isPointerHoldIn(num, GameCanvas.h - 40, num2, GameScr.disXC);
	}

	// Token: 0x06000399 RID: 921 RVA: 0x00041B8C File Offset: 0x0003FD8C
	public void checkMouseChat()
	{
		if (GameCanvas.isMouseFocus(GameScr.xC, GameScr.yC, 34, 34))
		{
			if (!TileMap.isOfflineMap())
			{
				mScreen.keyMouse = 15;
				return;
			}
		}
		else if (GameCanvas.isMouseFocus(GameScr.xHP, GameScr.yHP, 40, 40))
		{
			if (global::Char.myCharz().statusMe != 14)
			{
				mScreen.keyMouse = 10;
				return;
			}
		}
		else if (GameCanvas.isMouseFocus(GameScr.xF, GameScr.yF, 40, 40))
		{
			if (global::Char.myCharz().statusMe != 14)
			{
				mScreen.keyMouse = 5;
				return;
			}
		}
		else
		{
			if (this.cmdMenu != null && GameCanvas.isMouseFocus(this.cmdMenu.x, this.cmdMenu.y, this.cmdMenu.w / 2, this.cmdMenu.h))
			{
				mScreen.keyMouse = 1;
				return;
			}
			mScreen.keyMouse = -1;
		}
	}

	// Token: 0x0600039A RID: 922 RVA: 0x00041C60 File Offset: 0x0003FE60
	internal void updateKeyTouchControl()
	{
		if (this.isNotPaintTouchControl())
		{
			return;
		}
		mScreen.keyTouch = -1;
		if (GameCanvas.isTouchControl)
		{
			if (GameCanvas.isPointerHoldIn(0, 0, 60, 50) && GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				if (global::Char.myCharz().cmdMenu != null)
				{
					global::Char.myCharz().cmdMenu.performAction();
				}
				global::Char.myCharz().currentMovePoint = null;
				GameCanvas.clearAllPointerEvent();
				this.flareFindFocus = true;
				this.flareTime = 5;
				return;
			}
			if (Main.isPC)
			{
				this.checkMouseChat();
			}
			if (!TileMap.isOfflineMap() && GameCanvas.isPointerHoldIn(GameScr.xC, GameScr.yC, 34, 34))
			{
				mScreen.keyTouch = 15;
				GameCanvas.isPointerJustDown = false;
				this.isPointerDowning = false;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					ChatTextField.gI().startChat(this, string.Empty);
					SoundMn.gI().buttonClick();
					global::Char.myCharz().currentMovePoint = null;
					GameCanvas.clearAllPointerEvent();
					return;
				}
			}
			if (global::Char.myCharz().cmdMenu != null && GameCanvas.isPointerHoldIn(global::Char.myCharz().cmdMenu.x - 17, global::Char.myCharz().cmdMenu.y - 17, 34, 34))
			{
				mScreen.keyTouch = 20;
				GameCanvas.isPointerJustDown = false;
				this.isPointerDowning = false;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					GameCanvas.clearAllPointerEvent();
					global::Char.myCharz().cmdMenu.performAction();
					return;
				}
			}
			this.updateGamePad();
			if (((GameScr.isAnalog != 0) ? GameCanvas.isPointerHoldIn(GameScr.xHP, GameScr.yHP + 10, 34, 34) : GameCanvas.isPointerHoldIn(GameScr.xHP, GameScr.yHP + 10, 40, 40)) && global::Char.myCharz().statusMe != 14 && this.mobCapcha == null)
			{
				mScreen.keyTouch = 10;
				GameCanvas.isPointerJustDown = false;
				this.isPointerDowning = false;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					GameCanvas.keyPressed[10] = true;
					GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
				}
			}
			if (((GameScr.isAnalog != 0) ? GameCanvas.isPointerHoldIn(GameScr.xHP + 5, GameScr.yHP - 6 - 34 + 10, 34, 34) : GameCanvas.isPointerHoldIn(GameScr.xHP + 5, GameScr.yHP - 6 - 40 + 10, 40, 40)) && global::Char.myCharz().statusMe != 14 && this.mobCapcha == null)
			{
				if (GameScr.isPickNgocRong)
				{
					mScreen.keyTouch = 14;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						GameCanvas.keyPressed[14] = true;
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
						GameScr.isPickNgocRong = false;
						Service.gI().useItem(-1, -1, -1, -1);
					}
				}
				else if (GameScr.isudungCapsun4)
				{
					mScreen.keyTouch = 14;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						GameCanvas.keyPressed[14] = true;
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
						for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
						{
							Item item = global::Char.myCharz().arrItemBag[i];
							if (item != null)
							{
								Res.err("find " + item.template.id.ToString());
								if (item.template.id == 194)
								{
									GameScr.isudungCapsun4 = item.quantity > 0;
									if (GameScr.isudungCapsun4)
									{
										Service.gI().useItem(0, 1, (sbyte)i, -1);
										break;
									}
								}
							}
						}
					}
				}
				else if (GameScr.isudungCapsun3)
				{
					mScreen.keyTouch = 14;
					GameCanvas.isPointerJustDown = false;
					this.isPointerDowning = false;
					if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
					{
						GameCanvas.keyPressed[14] = true;
						GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
						for (int j = 0; j < global::Char.myCharz().arrItemBag.Length; j++)
						{
							Item item2 = global::Char.myCharz().arrItemBag[j];
							if (item2 != null && item2.template.id == 193)
							{
								GameScr.isudungCapsun3 = item2.quantity > 0;
								if (GameScr.isudungCapsun3)
								{
									Service.gI().useItem(0, 1, (sbyte)j, -1);
									break;
								}
							}
						}
					}
				}
			}
		}
		if (this.mobCapcha != null)
		{
			this.updateKeyTouchCapcha();
		}
		else if (GameScr.isHaveSelectSkill)
		{
			if (this.isCharging())
			{
				return;
			}
			this.keyTouchSkill = -1;
			bool flag = false;
			if (GameScr.onScreenSkill.Length > 5 && (GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill) || GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[5] - GameScr.wSkill / 2 + 12, GameScr.yS[5] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill)))
			{
				flag = true;
			}
			if (flag || GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, 5 * GameScr.wSkill, GameScr.wSkill) || (!GameCanvas.isTouchControl && GameCanvas.isPointerHoldIn(GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12, GameScr.yS[0] - GameScr.wSkill / 2 + 12, GameScr.wSkill, GameScr.onScreenSkill.Length * GameScr.wSkill)))
			{
				GameCanvas.isPointerJustDown = false;
				this.isPointerDowning = false;
				int num = (GameCanvas.pxLast - (GameScr.xSkill + GameScr.xS[0] - GameScr.wSkill / 2 + 12)) / GameScr.wSkill;
				if (flag && GameCanvas.pyLast < GameScr.yS[0])
				{
					num += 5;
				}
				this.keyTouchSkill = num;
				if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
				{
					GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
					this.selectedIndexSkill = num;
					if (GameScr.indexSelect < 0)
					{
						GameScr.indexSelect = 0;
					}
					if (!Main.isPC)
					{
						if (this.selectedIndexSkill > GameScr.onScreenSkill.Length - 1)
						{
							this.selectedIndexSkill = GameScr.onScreenSkill.Length - 1;
						}
					}
					else if (this.selectedIndexSkill > GameScr.keySkill.Length - 1)
					{
						this.selectedIndexSkill = GameScr.keySkill.Length - 1;
					}
					Skill skill = (Main.isPC ? GameScr.keySkill[this.selectedIndexSkill] : GameScr.onScreenSkill[this.selectedIndexSkill]);
					if (skill != null)
					{
						this.doSelectSkill(skill, true);
					}
				}
			}
		}
		if (GameCanvas.isPointerJustRelease)
		{
			if (GameCanvas.keyHold[1] || (GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] || GameCanvas.keyHold[3]) || GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] || GameCanvas.keyHold[(!Main.isPC) ? 6 : 24])
			{
				GameCanvas.isPointerJustRelease = false;
			}
			GameCanvas.keyHold[1] = false;
			GameCanvas.keyHold[(!Main.isPC) ? 2 : 21] = false;
			GameCanvas.keyHold[3] = false;
			GameCanvas.keyHold[(!Main.isPC) ? 4 : 23] = false;
			GameCanvas.keyHold[(!Main.isPC) ? 6 : 24] = false;
		}
	}

	// Token: 0x0600039B RID: 923 RVA: 0x00042393 File Offset: 0x00040593
	public void setCharJumpAtt()
	{
		global::Char.myCharz().cvy = -10;
		global::Char.myCharz().statusMe = 3;
		global::Char.myCharz().cp1 = 0;
	}

	// Token: 0x0600039C RID: 924 RVA: 0x000423B8 File Offset: 0x000405B8
	public void setCharJump(int cvx)
	{
		if (global::Char.myCharz().cx - global::Char.myCharz().cxSend != 0 || global::Char.myCharz().cy - global::Char.myCharz().cySend != 0)
		{
			Service.gI().charMove();
		}
		global::Char.myCharz().cvy = -10;
		global::Char.myCharz().cvx = cvx;
		global::Char.myCharz().statusMe = 3;
		global::Char.myCharz().cp1 = 0;
	}

	// Token: 0x0600039D RID: 925 RVA: 0x0004242C File Offset: 0x0004062C
	public void updateOpen()
	{
		if (this.isstarOpen)
		{
			if (this.moveUp > -3)
			{
				this.moveUp -= 4;
			}
			else
			{
				this.moveUp = -2;
			}
			if (this.moveDow < GameCanvas.h + 3)
			{
				this.moveDow += 4;
			}
			else
			{
				this.moveDow = GameCanvas.h + 2;
			}
			if (this.moveUp <= -2 && this.moveDow >= GameCanvas.h + 2)
			{
				this.isstarOpen = false;
			}
		}
	}

	// Token: 0x0600039E RID: 926 RVA: 0x00004887 File Offset: 0x00002A87
	public void initCreateCommand()
	{
	}

	// Token: 0x0600039F RID: 927 RVA: 0x00004887 File Offset: 0x00002A87
	public void checkCharFocus()
	{
	}

	// Token: 0x060003A0 RID: 928 RVA: 0x000424B0 File Offset: 0x000406B0
	public void updateXoSo()
	{
		if (this.tShow == 0)
		{
			return;
		}
		GameScr.currXS = mSystem.currentTimeMillis();
		if (GameScr.currXS - GameScr.lastXS > 1000L)
		{
			GameScr.lastXS = mSystem.currentTimeMillis();
			GameScr.secondXS++;
		}
		if (GameScr.secondXS > 20)
		{
			for (int i = 0; i < this.winnumber.Length; i++)
			{
				this.randomNumber[i] = this.winnumber[i];
			}
			this.tShow--;
			if (this.tShow == 0)
			{
				this.yourNumber = string.Empty;
				GameScr.info1.addInfo(this.strFinish, 0);
				GameScr.secondXS = 0;
			}
			return;
		}
		if (this.moveIndex > this.winnumber.Length - 1)
		{
			this.tShow--;
			if (this.tShow == 0)
			{
				this.yourNumber = string.Empty;
				GameScr.info1.addInfo(this.strFinish, 0);
			}
			return;
		}
		if (this.moveIndex < this.randomNumber.Length)
		{
			if (this.tMove[this.moveIndex] == 15)
			{
				if (this.randomNumber[this.moveIndex] == this.winnumber[this.moveIndex] - 1)
				{
					this.delayMove[this.moveIndex] = 10;
				}
				if (this.randomNumber[this.moveIndex] == this.winnumber[this.moveIndex])
				{
					this.tMove[this.moveIndex] = -1;
					this.moveIndex++;
				}
			}
			else if (GameCanvas.gameTick % 5 == 0)
			{
				this.tMove[this.moveIndex]++;
			}
		}
		for (int j = 0; j < this.winnumber.Length; j++)
		{
			if (this.tMove[j] != -1)
			{
				this.moveCount[j]++;
				if (this.moveCount[j] > this.tMove[j] + this.delayMove[j])
				{
					this.moveCount[j] = 0;
					this.randomNumber[j]++;
					if (this.randomNumber[j] >= 10)
					{
						this.randomNumber[j] = 0;
					}
				}
			}
		}
	}

	// Token: 0x060003A1 RID: 929 RVA: 0x000426C8 File Offset: 0x000408C8
	public override void update()
	{
		if (GameCanvas.keyPressed[16])
		{
			GameCanvas.keyPressed[16] = false;
			global::Char.myCharz().findNextFocusByKey();
		}
		if (GameCanvas.keyPressed[13] && !GameCanvas.panel.isShow && (GameCanvas.panel2 == null || !GameCanvas.panel2.isShow))
		{
			GameCanvas.keyPressed[13] = false;
			global::Char.myCharz().findNextFocusByKey();
		}
		if (GameCanvas.keyPressed[17])
		{
			GameCanvas.keyPressed[17] = false;
			global::Char.myCharz().searchItem();
			if (global::Char.myCharz().itemFocus != null)
			{
				this.pickItem();
			}
		}
		if (GameCanvas.gameTick % 100 == 0 && TileMap.mapID == 137)
		{
			GameScr.shock_scr = 30;
		}
		if (GameScr.isAutoPlay && GameCanvas.gameTick % 20 == 0)
		{
			this.autoPlay();
		}
		this.updateXoSo();
		mSystem.checkAdComlete();
		SmallImage.update();
		try
		{
			if (LoginScr.isContinueToLogin)
			{
				LoginScr.isContinueToLogin = false;
			}
			if (GameScr.tickMove == 1)
			{
				GameScr.lastTick = mSystem.currentTimeMillis();
			}
			if (GameScr.tickMove == 100)
			{
				GameScr.tickMove = 0;
				GameScr.currTick = mSystem.currentTimeMillis();
				int num = (int)(GameScr.currTick - GameScr.lastTick) / 1000;
				Service.gI().checkMMove(num);
			}
			if (GameScr.lockTick > 0)
			{
				GameScr.lockTick--;
				if (GameScr.lockTick == 0)
				{
					Controller.isStopReadMessage = false;
				}
			}
			this.checkCharFocus();
			GameCanvas.debug("E1", 0);
			GameScr.updateCamera();
			GameCanvas.debug("E2", 0);
			ChatTextField.gI().update();
			GameCanvas.debug("E3", 0);
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				((global::Char)GameScr.vCharInMap.elementAt(i)).update();
			}
			for (int j = 0; j < Teleport.vTeleport.size(); j++)
			{
				((Teleport)Teleport.vTeleport.elementAt(j)).update();
			}
			global::Char.myCharz().update();
			int statusMe = global::Char.myCharz().statusMe;
			if (this.popUpYesNo != null)
			{
				this.popUpYesNo.update();
			}
			EffecMn.update();
			GameCanvas.debug("E5x", 0);
			for (int k = 0; k < GameScr.vMob.size(); k++)
			{
				((Mob)GameScr.vMob.elementAt(k)).update();
			}
			GameCanvas.debug("E6", 0);
			for (int l = 0; l < GameScr.vNpc.size(); l++)
			{
				((Npc)GameScr.vNpc.elementAt(l)).update();
			}
			this.nSkill = GameScr.onScreenSkill.Length;
			for (int m = GameScr.onScreenSkill.Length - 1; m >= 0; m--)
			{
				if (GameScr.onScreenSkill[m] != null)
				{
					this.nSkill = m + 1;
					break;
				}
				this.nSkill--;
			}
			GameScr.setSkillBarPosition();
			GameCanvas.debug("E7", 0);
			GameCanvas.gI().updateDust();
			GameCanvas.debug("E8", 0);
			GameScr.updateFlyText();
			PopUp.updateAll();
			GameScr.updateSplash();
			this.updateSS();
			GameCanvas.updateBG();
			GameCanvas.debug("E9", 0);
			this.updateClickToArrow();
			GameCanvas.debug("E10", 0);
			for (int n = 0; n < GameScr.vItemMap.size(); n++)
			{
				((ItemMap)GameScr.vItemMap.elementAt(n)).update();
			}
			GameCanvas.debug("E11", 0);
			GameCanvas.debug("E13", 0);
			for (int num2 = Effect2.vRemoveEffect2.size() - 1; num2 >= 0; num2--)
			{
				Effect2.vEffect2.removeElement(Effect2.vRemoveEffect2.elementAt(num2));
				Effect2.vRemoveEffect2.removeElementAt(num2);
			}
			for (int num3 = 0; num3 < Effect2.vEffect2.size(); num3++)
			{
				((Effect2)Effect2.vEffect2.elementAt(num3)).update();
			}
			for (int num4 = 0; num4 < Effect2.vEffect2Outside.size(); num4++)
			{
				((Effect2)Effect2.vEffect2Outside.elementAt(num4)).update();
			}
			for (int num5 = 0; num5 < Effect2.vAnimateEffect.size(); num5++)
			{
				((Effect2)Effect2.vAnimateEffect.elementAt(num5)).update();
			}
			for (int num6 = 0; num6 < Effect2.vEffectFeet.size(); num6++)
			{
				((Effect2)Effect2.vEffectFeet.elementAt(num6)).update();
			}
			for (int num7 = 0; num7 < Effect2.vEffect3.size(); num7++)
			{
				((Effect2)Effect2.vEffect3.elementAt(num7)).update();
			}
			BackgroudEffect.updateEff();
			GameScr.info1.update();
			GameScr.info2.update();
			GameCanvas.debug("E15", 0);
			if (GameScr.currentCharViewInfo != null && !GameScr.currentCharViewInfo.Equals(global::Char.myCharz()))
			{
				GameScr.currentCharViewInfo.update();
			}
			this.runArrow++;
			if (this.runArrow > 3)
			{
				this.runArrow = 0;
			}
			if (this.isInjureHp)
			{
				this.twHp++;
				if (this.twHp == 20)
				{
					this.twHp = 0;
					this.isInjureHp = false;
				}
			}
			else if (this.dHP > global::Char.myCharz().cHP)
			{
				int num8 = this.dHP - global::Char.myCharz().cHP >> 1;
				if (num8 < 1)
				{
					num8 = 1;
				}
				this.dHP -= num8;
			}
			else
			{
				this.dHP = global::Char.myCharz().cHP;
			}
			if (this.isInjureMp)
			{
				this.twMp++;
				if (this.twMp == 20)
				{
					this.twMp = 0;
					this.isInjureMp = false;
				}
			}
			else if (this.dMP > global::Char.myCharz().cMP)
			{
				int num9 = this.dMP - global::Char.myCharz().cMP >> 1;
				if (num9 < 1)
				{
					num9 = 1;
				}
				this.dMP -= num9;
			}
			else
			{
				this.dMP = global::Char.myCharz().cMP;
			}
			if (this.tMenuDelay > 0)
			{
				this.tMenuDelay--;
			}
			if (this.isRongThanMenu())
			{
				int num10 = 100;
				while (this.yR - num10 < GameScr.cmy)
				{
					GameScr.cmy--;
				}
			}
			for (int num11 = 0; num11 < global::Char.vItemTime.size(); num11++)
			{
				((ItemTime)global::Char.vItemTime.elementAt(num11)).update();
			}
			for (int num12 = 0; num12 < GameScr.textTime.size(); num12++)
			{
				((ItemTime)GameScr.textTime.elementAt(num12)).update();
			}
			this.updateChatVip();
		}
		catch (Exception)
		{
		}
		if (GameCanvas.gameTick % 4000 == 1000)
		{
			GameScr.checkRemoveImage();
		}
		EffectManager.update();
	}

	// Token: 0x060003A2 RID: 930 RVA: 0x00004887 File Offset: 0x00002A87
	public void updateKeyChatPopUp()
	{
	}

	// Token: 0x060003A3 RID: 931 RVA: 0x00042D98 File Offset: 0x00040F98
	public bool isRongThanMenu()
	{
		return this.isMeCallRongThan;
	}

	// Token: 0x060003A4 RID: 932 RVA: 0x00042DA8 File Offset: 0x00040FA8
	public void paintEffect(mGraphics g)
	{
		for (int i = 0; i < Effect2.vEffect2.size(); i++)
		{
			Effect2 effect = (Effect2)Effect2.vEffect2.elementAt(i);
			if (effect != null && !(effect is ChatPopup))
			{
				effect.paint(g);
			}
		}
		if (!GameCanvas.lowGraphic)
		{
			for (int j = 0; j < Effect2.vAnimateEffect.size(); j++)
			{
				((Effect2)Effect2.vAnimateEffect.elementAt(j)).paint(g);
			}
		}
		for (int k = 0; k < Effect2.vEffect2Outside.size(); k++)
		{
			((Effect2)Effect2.vEffect2Outside.elementAt(k)).paint(g);
		}
	}

	// Token: 0x060003A5 RID: 933 RVA: 0x00042E4C File Offset: 0x0004104C
	public void paintBgItem(mGraphics g, int layer)
	{
		for (int i = 0; i < TileMap.vCurrItem.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
			if (bgItem.idImage != -1 && (int)bgItem.layer == layer)
			{
				bgItem.paint(g);
			}
		}
		if (TileMap.mapID == 48 && layer == 3 && GameCanvas.bgW != null && GameCanvas.bgW[0] != 0)
		{
			for (int j = 0; j < TileMap.pxw / GameCanvas.bgW[0] + 1; j++)
			{
				g.drawImage(GameCanvas.imgBG[0], j * GameCanvas.bgW[0], TileMap.pxh - GameCanvas.bgH[0] - 70, 0);
			}
		}
	}

	// Token: 0x060003A6 RID: 934 RVA: 0x00042EF6 File Offset: 0x000410F6
	public void paintBlackSky(mGraphics g)
	{
		if (!GameCanvas.lowGraphic)
		{
			g.fillTrans(GameScr.imgTrans, 0, 0, GameCanvas.w, GameCanvas.h);
		}
	}

	// Token: 0x060003A7 RID: 935 RVA: 0x00042F18 File Offset: 0x00041118
	public void paintCapcha(mGraphics g)
	{
		MobCapcha.paint(g, global::Char.myCharz().cx, global::Char.myCharz().cy);
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if (GameCanvas.menu.showMenu || GameCanvas.panel.isShow || ChatPopup.currChatPopup != null || !GameCanvas.isTouch)
		{
			return;
		}
		for (int i = 0; i < this.strCapcha.Length; i++)
		{
			int num = (GameCanvas.w - this.strCapcha.Length * GameScr.disXC) / 2 + i * GameScr.disXC + GameScr.disXC / 2;
			if (this.keyCapcha[i] == -1)
			{
				g.drawImage(GameScr.imgNut, num, GameCanvas.h - 25, 3);
				mFont.tahoma_7b_dark.drawString(g, this.strCapcha[i].ToString() + string.Empty, num, GameCanvas.h - 30, 2);
			}
			else
			{
				g.drawImage(GameScr.imgNutF, num, GameCanvas.h - 25, 3);
				mFont.tahoma_7b_green2.drawString(g, this.strCapcha[i].ToString() + string.Empty, num, GameCanvas.h - 30, 2);
			}
		}
	}

	// Token: 0x060003A8 RID: 936 RVA: 0x00043060 File Offset: 0x00041260
	public override void paint(mGraphics g)
	{
		GameScr.countEff = 0;
		if (!GameScr.isPaint)
		{
			return;
		}
		GameCanvas.debug("PA1", 1);
		if (this.isFreez || (this.isUseFreez && ChatPopup.currChatPopup == null))
		{
			this.dem++;
			if ((this.dem < 30 && this.dem >= 0 && GameCanvas.gameTick % 4 == 0) || (this.dem >= 30 && this.dem <= 50 && GameCanvas.gameTick % 3 == 0) || this.dem > 50)
			{
				g.setColor(16777215);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				if (this.dem <= 50)
				{
					return;
				}
				if (this.isUseFreez)
				{
					this.isUseFreez = false;
					this.dem = 0;
					if (this.activeRongThan)
					{
						this.callRongThan(this.xR, this.yR);
					}
					else
					{
						this.hideRongThan();
					}
				}
				this.paintInfoBar(g);
				g.translate(-GameScr.cmx, -GameScr.cmy);
				g.translate(0, GameCanvas.transY);
				global::Char.myCharz().paint(g);
				mSystem.paintFlyText(g);
				GameScr.resetTranslate(g);
				this.paintSelectedSkill(g);
				return;
			}
		}
		GameCanvas.debug("PA2", 1);
		GameCanvas.paintBGGameScr(g);
		this.paint_ios_bg(g);
		if ((this.isRongThanXuatHien || this.isFireWorks) && TileMap.bgID != 3)
		{
			this.paintBlackSky(g);
		}
		GameCanvas.debug("PA3", 1);
		if (GameScr.shock_scr > 0)
		{
			g.translate(-GameScr.cmx + GameScr.shock_x[GameScr.shock_scr % GameScr.shock_x.Length], -GameScr.cmy + GameScr.shock_y[GameScr.shock_scr % GameScr.shock_y.Length]);
			GameScr.shock_scr--;
		}
		else
		{
			g.translate(-GameScr.cmx, -GameScr.cmy);
		}
		if (this.isSuperPower)
		{
			g.translate((GameCanvas.gameTick % 3 != 0) ? (-3) : 3, 0);
		}
		BackgroudEffect.paintBehindTileAll(g);
		EffecMn.paintLayer1(g);
		TileMap.paintTilemap(g);
		TileMap.paintOutTilemap(g);
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.isMabuHold && TileMap.mapID == 128)
			{
				@char.paintHeadWithXY(g, @char.cx, @char.cy, 0);
			}
		}
		if (global::Char.myCharz().isMabuHold && TileMap.mapID == 128)
		{
			global::Char.myCharz().paintHeadWithXY(g, global::Char.myCharz().cx, global::Char.myCharz().cy, 0);
		}
		this.paintBgItem(g, 2);
		if (global::Char.myCharz().cmdMenu != null && GameCanvas.isTouch)
		{
			if (mScreen.keyTouch == 20)
			{
				g.drawImage(GameScr.imgChat2, global::Char.myCharz().cmdMenu.x + GameScr.cmx, global::Char.myCharz().cmdMenu.y + GameScr.cmy, mGraphics.HCENTER | mGraphics.VCENTER);
			}
			else
			{
				g.drawImage(GameScr.imgChat, global::Char.myCharz().cmdMenu.x + GameScr.cmx, global::Char.myCharz().cmdMenu.y + GameScr.cmy, mGraphics.HCENTER | mGraphics.VCENTER);
			}
		}
		GameCanvas.debug("PA4", 1);
		GameCanvas.debug("PA5", 1);
		BackgroudEffect.paintBackAll(g);
		EffectManager.lowEffects.paintAll(g);
		for (int j = 0; j < Effect2.vEffectFeet.size(); j++)
		{
			((Effect2)Effect2.vEffectFeet.elementAt(j)).paint(g);
		}
		for (int k = 0; k < Teleport.vTeleport.size(); k++)
		{
			((Teleport)Teleport.vTeleport.elementAt(k)).paintHole(g);
		}
		for (int l = 0; l < GameScr.vNpc.size(); l++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(l);
			if (npc.cHP > 0)
			{
				npc.paintShadow(g);
			}
		}
		for (int m = 0; m < GameScr.vNpc.size(); m++)
		{
			((Npc)GameScr.vNpc.elementAt(m)).paint(g);
		}
		g.translate(0, GameCanvas.transY);
		GameCanvas.debug("PA7", 1);
		GameCanvas.debug("PA8", 1);
		for (int n = 0; n < GameScr.vCharInMap.size(); n++)
		{
			global::Char char2 = null;
			try
			{
				char2 = (global::Char)GameScr.vCharInMap.elementAt(n);
			}
			catch (Exception ex)
			{
				Cout.LogError("Loi ham paint char gamesc: " + ex.ToString());
			}
			if (char2 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()) && char2.isShadown)
			{
				char2.paintShadow(g);
			}
		}
		global::Char.myCharz().paintShadow(g);
		EffecMn.paintLayer2(g);
		for (int num = 0; num < GameScr.vMob.size(); num++)
		{
			((Mob)GameScr.vMob.elementAt(num)).paint(g);
		}
		for (int num2 = 0; num2 < Teleport.vTeleport.size(); num2++)
		{
			((Teleport)Teleport.vTeleport.elementAt(num2)).paint(g);
		}
		for (int num3 = 0; num3 < GameScr.vCharInMap.size(); num3++)
		{
			global::Char char3 = null;
			try
			{
				char3 = (global::Char)GameScr.vCharInMap.elementAt(num3);
			}
			catch (Exception)
			{
			}
			if (char3 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()))
			{
				char3.paint(g);
			}
		}
		global::Char.myCharz().paint(g);
		if (global::Char.myCharz().skillPaint != null && global::Char.myCharz().skillInfoPaint() != null && global::Char.myCharz().indexSkill < global::Char.myCharz().skillInfoPaint().Length)
		{
			global::Char.myCharz().paintCharWithSkill(g);
			global::Char.myCharz().paintMount2(g);
		}
		for (int num4 = 0; num4 < GameScr.vCharInMap.size(); num4++)
		{
			global::Char char4 = null;
			try
			{
				char4 = (global::Char)GameScr.vCharInMap.elementAt(num4);
			}
			catch (Exception ex2)
			{
				Cout.LogError("Loi ham paint char gamescr: " + ex2.ToString());
			}
			if (char4 != null && (!GameCanvas.panel.isShow || !GameCanvas.panel.isTypeShop()) && char4.skillPaint != null && char4.skillInfoPaint() != null && char4.indexSkill < char4.skillInfoPaint().Length)
			{
				char4.paintCharWithSkill(g);
				char4.paintMount2(g);
			}
		}
		for (int num5 = 0; num5 < GameScr.vItemMap.size(); num5++)
		{
			((ItemMap)GameScr.vItemMap.elementAt(num5)).paint(g);
		}
		g.translate(0, -GameCanvas.transY);
		GameCanvas.debug("PA9", 1);
		GameScr.paintSplash(g);
		GameCanvas.debug("PA10", 1);
		GameCanvas.debug("PA11", 1);
		GameCanvas.debug("PA13", 1);
		this.paintEffect(g);
		this.paintBgItem(g, 3);
		for (int num6 = 0; num6 < GameScr.vNpc.size(); num6++)
		{
			((Npc)GameScr.vNpc.elementAt(num6)).paintName(g);
		}
		EffecMn.paintLayer3(g);
		for (int num7 = 0; num7 < GameScr.vNpc.size(); num7++)
		{
			Npc npc2 = (Npc)GameScr.vNpc.elementAt(num7);
			if (npc2.chatInfo != null && npc2 != null)
			{
				npc2.chatInfo.paint(g, npc2.cx, npc2.cy - npc2.ch - GameCanvas.transY, npc2.cdir);
			}
		}
		for (int num8 = 0; num8 < GameScr.vCharInMap.size(); num8++)
		{
			global::Char char5 = null;
			try
			{
				char5 = (global::Char)GameScr.vCharInMap.elementAt(num8);
			}
			catch (Exception)
			{
			}
			if (char5 != null && char5.chatInfo != null)
			{
				char5.chatInfo.paint(g, char5.cx, char5.cy - char5.ch, char5.cdir);
			}
		}
		if (global::Char.myCharz().chatInfo != null)
		{
			global::Char.myCharz().chatInfo.paint(g, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, global::Char.myCharz().cdir);
		}
		EffectManager.mid_2Effects.paintAll(g);
		EffectManager.midEffects.paintAll(g);
		BackgroudEffect.paintFrontAll(g);
		for (int num9 = 0; num9 < TileMap.vCurrItem.size(); num9++)
		{
			BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(num9);
			if (bgItem.idImage != -1 && bgItem.layer > 3)
			{
				bgItem.paint(g);
			}
		}
		PopUp.paintAll(g);
		if (TileMap.mapID == 120)
		{
			if (this.percentMabu != 100)
			{
				int num10 = (int)this.percentMabu * mGraphics.getImageWidth(GameScr.imgHPLost) / 100;
				sbyte b = this.percentMabu;
				g.drawImage(GameScr.imgHPLost, TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, 0);
				g.setClip(TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, num10, 10);
				g.drawImage(GameScr.imgHP, TileMap.pxw / 2 - mGraphics.getImageWidth(GameScr.imgHPLost) / 2, 220, 0);
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			}
			if (this.mabuEff)
			{
				this.tMabuEff++;
				if (GameCanvas.gameTick % 3 == 0)
				{
					EffecMn.addEff(new Effect(19, Res.random(TileMap.pxw / 2 - 50, TileMap.pxw / 2 + 50), 340, 2, 1, -1));
				}
				if (GameCanvas.gameTick % 15 == 0)
				{
					EffecMn.addEff(new Effect(18, Res.random(TileMap.pxw / 2 - 5, TileMap.pxw / 2 + 5), Res.random(300, 320), 2, 1, -1));
				}
				if (this.tMabuEff == 100)
				{
					this.activeSuperPower(TileMap.pxw / 2, 300);
				}
				if (this.tMabuEff == 110)
				{
					this.tMabuEff = 0;
					this.mabuEff = false;
				}
			}
		}
		BackgroudEffect.paintFog(g);
		bool flag = true;
		for (int num11 = 0; num11 < BackgroudEffect.vBgEffect.size(); num11++)
		{
			if (((BackgroudEffect)BackgroudEffect.vBgEffect.elementAt(num11)).typeEff == 0)
			{
				flag = false;
				break;
			}
		}
		if (mGraphics.zoomLevel <= 1 || Main.isIpod || Main.isIphone4)
		{
			flag = false;
		}
		if (flag && !this.isRongThanXuatHien)
		{
			int num12 = TileMap.pxw / (mGraphics.getImageWidth(TileMap.imgLight) + 50);
			if (num12 <= 0)
			{
				num12 = 1;
			}
			if (TileMap.tileID != 28)
			{
				for (int num13 = 0; num13 < num12; num13++)
				{
					int num14 = 100 + num13 * (mGraphics.getImageWidth(TileMap.imgLight) + 50) - GameScr.cmx / 2;
					int num15 = -20;
					if (num14 + mGraphics.getImageWidth(TileMap.imgLight) >= GameScr.cmx && num14 <= GameScr.cmx + GameCanvas.w && num15 + mGraphics.getImageHeight(TileMap.imgLight) >= GameScr.cmy && num15 <= GameScr.cmy + GameCanvas.h)
					{
						g.drawImage(TileMap.imgLight, 100 + num13 * (mGraphics.getImageWidth(TileMap.imgLight) + 50) - GameScr.cmx / 2, num15, 0);
					}
				}
			}
		}
		mSystem.paintFlyText(g);
		GameCanvas.debug("PA14", 1);
		GameCanvas.debug("PA15", 1);
		GameCanvas.debug("PA16", 1);
		this.paintArrowPointToNPC(g);
		GameCanvas.debug("PA17", 1);
		if (!GameScr.isPaintOther && GameScr.isPaintRada == 1 && !GameCanvas.panel.isShow)
		{
			this.paintInfoBar(g);
		}
		GameScr.resetTranslate(g);
		this.paint_xp_bar(g);
		if (!GameScr.isPaintOther)
		{
			if (GameCanvas.open3Hour && TileMap.mapID != 170)
			{
				if (GameCanvas.w > 250)
				{
					g.drawImage(GameCanvas.img12, 160, 6, 0);
					mFont.tahoma_7_white.drawString(g, "Dành cho người chơi trên 12 tuổi.", 180, 2, 0);
					mFont.tahoma_7_white.drawString(g, "Chơi quá 180 phút mỗi ngày ", 180, 12, 0);
					mFont.tahoma_7_white.drawString(g, "sẽ hại sức khỏe.", 180, 22, 0);
				}
				else
				{
					g.drawImage(GameCanvas.img12, 5, GameCanvas.h - 67, 0);
					mFont.tahoma_7_white.drawString(g, "Dành cho người chơi trên 12 tuổi.", 25, GameCanvas.h - 70, 0);
					mFont.tahoma_7_white.drawString(g, "Chơi quá 180 phút mỗi ngày sẽ hại sức khỏe.", 25, GameCanvas.h - 60, 0);
				}
			}
			GameCanvas.debug("PA21", 1);
			GameCanvas.debug("PA18", 1);
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			if ((TileMap.mapID == 128 || TileMap.mapID == 127) && GameScr.mabuPercent != 0)
			{
				int num16 = 30;
				int num17 = 200;
				g.setColor(0);
				g.fillRect(num16 - 27, num17 - 112, 54, 8);
				g.setColor(16711680);
				g.setClip(num16 - 25, num17 - 110, (int)GameScr.mabuPercent, 4);
				g.fillRect(num16 - 25, num17 - 110, 50, 4);
				g.setClip(0, 0, 3000, 3000);
				mFont.tahoma_7b_white.drawString(g, "Mabu", num16, num17 - 112 + 10, 2, mFont.tahoma_7b_dark);
			}
			if (global::Char.myCharz().isFusion)
			{
				global::Char.myCharz().tFusion++;
				if (GameCanvas.gameTick % 3 == 0)
				{
					g.setColor(16777215);
					g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				}
				if (global::Char.myCharz().tFusion >= 100)
				{
					global::Char.myCharz().fusionComplete();
				}
			}
			for (int num18 = 0; num18 < GameScr.vCharInMap.size(); num18++)
			{
				global::Char char6 = null;
				try
				{
					char6 = (global::Char)GameScr.vCharInMap.elementAt(num18);
				}
				catch (Exception)
				{
				}
				if (char6 != null && char6.isFusion && global::Char.isCharInScreen(char6))
				{
					char6.tFusion++;
					if (GameCanvas.gameTick % 3 == 0)
					{
						g.setColor(16777215);
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					}
					if (char6.tFusion >= 100)
					{
						char6.fusionComplete();
					}
				}
			}
			GameCanvas.paintz.paintTabSoft(g);
			GameCanvas.debug("PA19", 1);
			GameCanvas.debug("PA20", 1);
			GameScr.resetTranslate(g);
			this.paintSelectedSkill(g);
			GameCanvas.debug("PA22", 1);
			GameScr.resetTranslate(g);
			if (GameCanvas.isTouch && GameCanvas.isTouchControl)
			{
				this.paintTouchControl(g);
			}
			GameScr.resetTranslate(g);
			this.paintChatVip(g);
			if (!GameCanvas.panel.isShow && GameCanvas.currentDialog == null && ChatPopup.currChatPopup == null && ChatPopup.serverChatPopUp == null && GameCanvas.currentScreen.Equals(GameScr.instance))
			{
				base.paint(g);
				if (mScreen.keyMouse == 1 && this.cmdMenu != null)
				{
					g.drawImage(ItemMap.imageFlare, this.cmdMenu.x + 7, this.cmdMenu.y + 15, 3);
				}
			}
			GameScr.resetTranslate(g);
			int num19 = 100 + ((global::Char.vItemTime.size() != 0) ? (GameScr.textTime.size() * 12) : 0);
			if (global::Char.myCharz().clan != null)
			{
				int num20 = 0;
				int num21 = 0;
				int num22 = (GameCanvas.h - 100 - 60) / 12;
				for (int num23 = 0; num23 < GameScr.vCharInMap.size(); num23++)
				{
					global::Char char7 = (global::Char)GameScr.vCharInMap.elementAt(num23);
					if (char7.clanID != -1 && char7.clanID == global::Char.myCharz().clan.ID)
					{
						if (char7.isOutX() && char7.cx < global::Char.myCharz().cx)
						{
							int num24 = num22;
							if (global::Char.vItemTime.size() != 0)
							{
								num24 -= GameScr.textTime.size();
							}
							if (num20 <= num24)
							{
								mFont.tahoma_7_green.drawString(g, char7.cName, 20, num19 - 12 + num20 * 12, mFont.LEFT, mFont.tahoma_7_grey);
								char7.paintHp(g, 10, num19 + num20 * 12 - 5);
								num20++;
							}
						}
						else if (char7.isOutX() && char7.cx > global::Char.myCharz().cx && num21 <= num22)
						{
							mFont.tahoma_7_green.drawString(g, char7.cName, GameCanvas.w - 25, num19 - 12 + num21 * 12, mFont.RIGHT, mFont.tahoma_7_grey);
							char7.paintHp(g, GameCanvas.w - 15, num19 + num21 * 12 - 5);
							num21++;
						}
					}
				}
			}
			ChatTextField.gI().paint(g);
			if (GameScr.isNewClanMessage && !GameCanvas.panel.isShow && GameCanvas.gameTick % 4 == 0)
			{
				g.drawImage(ItemMap.imageFlare, this.cmdMenu.x + 15, this.cmdMenu.y + 30, mGraphics.BOTTOM | mGraphics.HCENTER);
			}
			if (this.isSuperPower)
			{
				this.dxPower += 5;
				if (this.tPower >= 0)
				{
					this.tPower += this.dxPower;
				}
				Res.outz("x power= " + this.xPower.ToString());
				if (this.tPower < 0)
				{
					this.tPower--;
					if (this.tPower == -20)
					{
						this.isSuperPower = false;
						this.tPower = 0;
						this.dxPower = 0;
					}
				}
				else if ((this.xPower - this.tPower > 0 || this.tPower < TileMap.pxw) && this.tPower > 0)
				{
					g.setColor(16777215);
					if (!GameCanvas.lowGraphic)
					{
						g.fillArg(0, 0, GameCanvas.w, GameCanvas.h, 0, 0);
					}
					else
					{
						g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
					}
				}
				else
				{
					this.tPower = -1;
				}
			}
			for (int num25 = 0; num25 < global::Char.vItemTime.size(); num25++)
			{
				((ItemTime)global::Char.vItemTime.elementAt(num25)).paint(g, this.cmdMenu.x + 32 + num25 * 24, 55);
			}
			for (int num26 = 0; num26 < GameScr.textTime.size(); num26++)
			{
				((ItemTime)GameScr.textTime.elementAt(num26)).paintText(g, this.cmdMenu.x + ((global::Char.vItemTime.size() == 0) ? 25 : 5), ((global::Char.vItemTime.size() == 0) ? 45 : 90) + num26 * 12);
			}
			this.paintXoSo(g);
			if (mResources.language == 1)
			{
				long num27 = mSystem.currentTimeMillis() - GameScr.deltaTime;
				mFont.tahoma_7b_white.drawString(g, NinjaUtil.getDate2(num27), 10, GameCanvas.h - 65, 0, mFont.tahoma_7b_dark);
			}
			if (!this.yourNumber.Equals(string.Empty))
			{
				for (int num28 = 0; num28 < this.strPaint.Length; num28++)
				{
					mFont.tahoma_7b_white.drawString(g, this.strPaint[num28], 5, 85 + num28 * 18, 0, mFont.tahoma_7b_dark);
				}
			}
		}
		int num29 = 0;
		int num30 = GameCanvas.hw;
		if (num30 > 200)
		{
			num30 = 200;
		}
		this.paintPhuBanBar(g, num29 + GameCanvas.w / 2, 0, num30);
		EffectManager.hiEffects.paintAll(g);
		if (GameScr.nCT_timeBallte > mSystem.currentTimeMillis() && TileMap.mapID == 170 && GameScr.isPaint_CT && GameScr.nCT_nBoyBaller / 2 > 0)
		{
			try
			{
				this.paint_CT(g, num29 + GameCanvas.w / 2, 0, num30);
			}
			catch (Exception)
			{
			}
		}
		if (TileMap.mapID == 172)
		{
			string.Concat(new string[]
			{
				mResources.WAIT,
				"  ",
				GameScr.nUSER_CT.ToString(),
				"/",
				GameScr.nUSER_MAX_CT.ToString()
			});
			mFont.tahoma_7b_dark.drawString(g, string.Concat(new string[]
			{
				mResources.WAIT,
				"  ",
				GameScr.nUSER_CT.ToString(),
				"/",
				GameScr.nUSER_MAX_CT.ToString()
			}), GameCanvas.w - 10, 40, 1);
		}
	}

	// Token: 0x060003A9 RID: 937 RVA: 0x00044534 File Offset: 0x00042734
	internal void paintXoSo(mGraphics g)
	{
		if (this.tShow != 0)
		{
			string text = string.Empty;
			for (int i = 0; i < this.winnumber.Length; i++)
			{
				text = text + this.randomNumber[i].ToString() + " ";
			}
			PopUp.paintPopUp(g, 20, 45, 95, 35, 16777215, false);
			mFont.tahoma_7b_dark.drawString(g, mResources.kquaVongQuay, 68, 50, 2);
			mFont.tahoma_7b_dark.drawString(g, text + string.Empty, 68, 65, 2);
		}
	}

	// Token: 0x060003AA RID: 938 RVA: 0x000445C4 File Offset: 0x000427C4
	internal void checkEffToObj(IMapObject obj, bool isnew)
	{
		if (obj == null || this.tDoubleDelay > 0)
		{
			return;
		}
		this.tDoubleDelay = 10;
		int x = obj.getX();
		int num = Res.abs(global::Char.myCharz().cx - x);
		int num2 = ((num <= 80) ? 1 : ((num > 80 && num <= 200) ? 2 : ((num <= 200 || num > 400) ? 4 : 3)));
		if (!isnew)
		{
			if (obj.Equals(global::Char.myCharz().mobFocus) || (obj.Equals(global::Char.myCharz().charFocus) && global::Char.myCharz().isMeCanAttackOtherPlayer(global::Char.myCharz().charFocus)))
			{
				ServerEffect.addServerEffect(135, obj.getX(), obj.getY(), num2);
				return;
			}
			if (obj.Equals(global::Char.myCharz().npcFocus) || obj.Equals(global::Char.myCharz().itemFocus) || obj.Equals(global::Char.myCharz().charFocus))
			{
				ServerEffect.addServerEffect(136, obj.getX(), obj.getY(), num2);
				return;
			}
		}
		else
		{
			ServerEffect.addServerEffect(136, obj.getX(), obj.getY(), num2);
		}
	}

	// Token: 0x060003AB RID: 939 RVA: 0x000446EC File Offset: 0x000428EC
	internal void updateClickToArrow()
	{
		if (this.tDoubleDelay > 0)
		{
			this.tDoubleDelay--;
		}
		if (this.clickMoving)
		{
			this.clickMoving = false;
			IMapObject mapObject = this.findClickToItem(this.clickToX, this.clickToY);
			if (mapObject == null || (mapObject != null && mapObject.Equals(global::Char.myCharz().npcFocus) && TileMap.mapID == 51))
			{
				ServerEffect.addServerEffect(134, this.clickToX, this.clickToY + GameCanvas.transY / 2, 3);
			}
		}
	}

	// Token: 0x060003AC RID: 940 RVA: 0x00044774 File Offset: 0x00042974
	internal void paintWaypointArrow(mGraphics g)
	{
		int num = 10;
		Task taskMaint = global::Char.myCharz().taskMaint;
		if (taskMaint != null && taskMaint.taskId == 0 && ((taskMaint.index != 1 && taskMaint.index < 6) || taskMaint.index == 0))
		{
			return;
		}
		for (int i = 0; i < TileMap.vGo.size(); i++)
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
			if (waypoint.minY == 0 || (int)waypoint.maxY >= TileMap.pxh - 24)
			{
				if ((int)waypoint.maxY <= TileMap.pxh / 2)
				{
					int num2 = (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2);
					int num3 = (int)(waypoint.minY + (waypoint.maxY - waypoint.minY) / 2) + this.runArrow;
					if (GameCanvas.isTouch)
					{
						num3 = (int)(waypoint.maxY + (waypoint.maxY - waypoint.minY)) + this.runArrow + num;
					}
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 6, num2, num3, StaticObj.VCENTER_HCENTER);
				}
				else if ((int)waypoint.minY >= TileMap.pxh / 2)
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2), (int)(waypoint.minY - 12) - this.runArrow, StaticObj.VCENTER_HCENTER);
				}
			}
			else if (waypoint.minX >= 0 && waypoint.minX < 24)
			{
				if (!GameCanvas.isTouch)
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 2, (int)(waypoint.maxX + 12) + this.runArrow, (int)(waypoint.maxY - 12), StaticObj.VCENTER_HCENTER);
				}
				else
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 2, (int)(waypoint.maxX + 12) + this.runArrow, (int)(waypoint.maxY - 32), StaticObj.VCENTER_HCENTER);
				}
			}
			else if ((int)waypoint.minX <= TileMap.tmw * 24 && (int)waypoint.minX >= TileMap.tmw * 24 - 48)
			{
				if (!GameCanvas.isTouch)
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, (int)(waypoint.minX - 12) - this.runArrow, (int)(waypoint.maxY - 12), StaticObj.VCENTER_HCENTER);
				}
				else
				{
					g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 0, (int)(waypoint.minX - 12) - this.runArrow, (int)(waypoint.maxY - 32), StaticObj.VCENTER_HCENTER);
				}
			}
			else
			{
				g.drawRegion(GameScr.arrow, 0, 0, 13, 16, 4, (int)(waypoint.minX + (waypoint.maxX - waypoint.minX) / 2), (int)(waypoint.maxY - 48) - this.runArrow, StaticObj.VCENTER_HCENTER);
			}
		}
	}

	// Token: 0x060003AD RID: 941 RVA: 0x00044A2C File Offset: 0x00042C2C
	public static Npc findNPCInMap(short id)
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			if (npc.template.npcTemplateId == (int)id)
			{
				return npc;
			}
		}
		return null;
	}

	// Token: 0x060003AE RID: 942 RVA: 0x00044A70 File Offset: 0x00042C70
	public static global::Char findCharInMap(int charId)
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.charID == charId)
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x060003AF RID: 943 RVA: 0x00044AAF File Offset: 0x00042CAF
	public static Mob findMobInMap(sbyte mobIndex)
	{
		return (Mob)GameScr.vMob.elementAt((int)mobIndex);
	}

	// Token: 0x060003B0 RID: 944 RVA: 0x00044AC4 File Offset: 0x00042CC4
	public static Mob findMobInMap(int mobId)
	{
		for (int i = 0; i < GameScr.vMob.size(); i++)
		{
			Mob mob = (Mob)GameScr.vMob.elementAt(i);
			if (mob.mobId == mobId)
			{
				return mob;
			}
		}
		return null;
	}

	// Token: 0x060003B1 RID: 945 RVA: 0x00044B04 File Offset: 0x00042D04
	public static Npc getNpcTask()
	{
		for (int i = 0; i < GameScr.vNpc.size(); i++)
		{
			Npc npc = (Npc)GameScr.vNpc.elementAt(i);
			if (npc.template.npcTemplateId == (int)GameScr.getTaskNpcId())
			{
				return npc;
			}
		}
		return null;
	}

	// Token: 0x060003B2 RID: 946 RVA: 0x00044B4C File Offset: 0x00042D4C
	internal void paintArrowPointToNPC(mGraphics g)
	{
		try
		{
			if (ChatPopup.currChatPopup == null)
			{
				int taskNpcId = (int)GameScr.getTaskNpcId();
				if (taskNpcId != -1)
				{
					Npc npc = null;
					for (int i = 0; i < GameScr.vNpc.size(); i++)
					{
						Npc npc2 = (Npc)GameScr.vNpc.elementAt(i);
						if (npc2.template.npcTemplateId == taskNpcId)
						{
							if (npc == null)
							{
								npc = npc2;
							}
							else if (Res.abs(npc2.cx - global::Char.myCharz().cx) < Res.abs(npc.cx - global::Char.myCharz().cx))
							{
								npc = npc2;
							}
						}
					}
					if (npc != null && npc.statusMe != 15 && (npc.cx <= GameScr.cmx || npc.cx >= GameScr.cmx + GameScr.gW || npc.cy <= GameScr.cmy || npc.cy >= GameScr.cmy + GameScr.gH) && GameCanvas.gameTick % 10 >= 5)
					{
						int num = npc.cx - global::Char.myCharz().cx;
						int num2 = npc.cy - global::Char.myCharz().cy;
						int num3 = 0;
						int num4 = 0;
						int num5 = 0;
						if (num > 0 && num2 >= 0)
						{
							if (Res.abs(num) >= Res.abs(num2))
							{
								num3 = GameScr.gW - 10;
								num4 = GameScr.gH / 2 + 30;
								if (GameCanvas.isTouch)
								{
									num4 = GameScr.gH / 2 + 10;
								}
								num5 = 0;
							}
							else
							{
								num3 = GameScr.gW / 2;
								num4 = GameScr.gH - 10;
								num5 = 5;
							}
						}
						else if (num >= 0 && num2 < 0)
						{
							if (Res.abs(num) >= Res.abs(num2))
							{
								num3 = GameScr.gW - 10;
								num4 = GameScr.gH / 2 + 30;
								if (GameCanvas.isTouch)
								{
									num4 = GameScr.gH / 2 + 10;
								}
								num5 = 0;
							}
							else
							{
								num3 = GameScr.gW / 2;
								num4 = 10;
								num5 = 6;
							}
						}
						if (num < 0 && num2 >= 0)
						{
							if (Res.abs(num) >= Res.abs(num2))
							{
								num3 = 10;
								num4 = GameScr.gH / 2 + 30;
								if (GameCanvas.isTouch)
								{
									num4 = GameScr.gH / 2 + 10;
								}
								num5 = 3;
							}
							else
							{
								num3 = GameScr.gW / 2;
								num4 = GameScr.gH - 10;
								num5 = 5;
							}
						}
						else if (num <= 0 && num2 < 0)
						{
							if (Res.abs(num) >= Res.abs(num2))
							{
								num3 = 10;
								num4 = GameScr.gH / 2 + 30;
								if (GameCanvas.isTouch)
								{
									num4 = GameScr.gH / 2 + 10;
								}
								num5 = 3;
							}
							else
							{
								num3 = GameScr.gW / 2;
								num4 = 10;
								num5 = 6;
							}
						}
						GameScr.resetTranslate(g);
						g.drawRegion(GameScr.arrow, 0, 0, 13, 16, num5, num3, num4, StaticObj.VCENTER_HCENTER);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham arrow to npc: " + ex.ToString());
		}
	}

	// Token: 0x060003B3 RID: 947 RVA: 0x00044E24 File Offset: 0x00043024
	public static void resetTranslate(mGraphics g)
	{
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		g.setClip(0, -200, GameCanvas.w, 200 + GameCanvas.h);
	}

	// Token: 0x060003B4 RID: 948 RVA: 0x00044E58 File Offset: 0x00043058
	internal void paintTouchControl(mGraphics g)
	{
		if (this.isNotPaintTouchControl())
		{
			return;
		}
		GameScr.resetTranslate(g);
		if (!TileMap.isOfflineMap() && !this.isVS())
		{
			if (mScreen.keyTouch == 15 || mScreen.keyMouse == 15)
			{
				g.drawImage((!Main.isPC) ? GameScr.imgChat2 : GameScr.imgChatsPC2, GameScr.xC + 17, GameScr.yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
			}
			else
			{
				g.drawImage((!Main.isPC) ? GameScr.imgChat : GameScr.imgChatPC, GameScr.xC + 17, GameScr.yC + 17 + mGraphics.addYWhenOpenKeyBoard, mGraphics.HCENTER | mGraphics.VCENTER);
			}
		}
		bool flag = GameScr.isUseTouch;
	}

	// Token: 0x060003B5 RID: 949 RVA: 0x00044F18 File Offset: 0x00043118
	public void paintImageBarRight(mGraphics g, global::Char c)
	{
		int num = (int)((long)c.cHP * GameScr.hpBarW / (long)c.cHPFull);
		int num2 = c.cMP * GameScr.mpBarW;
		int num3 = (int)((long)this.dHP * GameScr.hpBarW / (long)c.cHPFull);
		int num4 = this.dMP * GameScr.mpBarW;
		g.setClip(GameCanvas.w / 2 + 58 - mGraphics.getImageWidth(GameScr.imgPanel), 0, 95, 100);
		g.drawRegion(GameScr.imgPanel, 0, 0, mGraphics.getImageWidth(GameScr.imgPanel), mGraphics.getImageHeight(GameScr.imgPanel), 2, GameCanvas.w / 2 + 60, 0, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83) - GameScr.hpBarW + GameScr.hpBarW - (long)num3), 5, num3, 10);
		g.drawImage(GameScr.imgHPLost, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83) - GameScr.hpBarW + GameScr.hpBarW - (long)num), 5, num, 10);
		g.drawImage(GameScr.imgHP, GameCanvas.w / 2 + 60 - 83, 5, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83 - GameScr.mpBarW) + GameScr.hpBarW - (long)num4), 20, num4, 6);
		g.drawImage(GameScr.imgMPLost, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
		g.setClip((int)((long)(GameCanvas.w / 2 + 60 - 83 - GameScr.mpBarW) + GameScr.hpBarW - (long)num2), 20, num2, 6);
		g.drawImage(GameScr.imgMP, GameCanvas.w / 2 + 60 - 83, 20, mGraphics.RIGHT | mGraphics.TOP);
		g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
	}

	// Token: 0x060003B6 RID: 950 RVA: 0x00045140 File Offset: 0x00043340
	internal void paintImageBar(mGraphics g, bool isLeft, global::Char c)
	{
		if (c != null)
		{
			int num;
			int num2;
			int num3;
			int num4;
			if (c.charID == global::Char.myCharz().charID)
			{
				num = (int)((long)this.dHP * GameScr.hpBarW / (long)c.cHPFull);
				num2 = this.dMP * GameScr.mpBarW / c.cMPFull;
				num3 = (int)((long)c.cHP * GameScr.hpBarW / (long)c.cHPFull);
				num4 = c.cMP * GameScr.mpBarW / c.cMPFull;
			}
			else
			{
				num = (int)((long)c.dHP * GameScr.hpBarW / (long)c.cHPFull);
				num2 = c.perCentMp * GameScr.mpBarW / 100;
				num3 = (int)((long)c.cHP * GameScr.hpBarW / (long)c.cHPFull);
				num4 = c.perCentMp * GameScr.mpBarW / 100;
			}
			if (global::Char.myCharz().secondPower > 0)
			{
				int num5 = (int)global::Char.myCharz().powerPoint * GameScr.spBarW / (int)global::Char.myCharz().maxPowerPoint;
				g.drawImage(GameScr.imgPanel2, 58, 29, 0);
				g.setClip(83, 31, num5, 10);
				g.drawImage(GameScr.imgSP, 83, 31, 0);
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
				mFont.tahoma_7_white.drawString(g, string.Concat(new string[]
				{
					global::Char.myCharz().strInfo,
					":",
					global::Char.myCharz().powerPoint.ToString(),
					"/",
					global::Char.myCharz().maxPowerPoint.ToString()
				}), 115, 29, 2);
			}
			if (c.charID != global::Char.myCharz().charID)
			{
				g.setClip(mGraphics.getImageWidth(GameScr.imgPanel) - 95, 0, 95, 100);
			}
			g.drawImage(GameScr.imgPanel, 0, 0, 0);
			if (isLeft)
			{
				g.setClip(83, 5, num, 10);
			}
			else
			{
				g.setClip((int)(83L + GameScr.hpBarW - (long)num), 5, num, 10);
			}
			g.drawImage(GameScr.imgHPLost, 83, 5, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (isLeft)
			{
				g.setClip(83, 5, num3, 10);
			}
			else
			{
				g.setClip((int)(83L + GameScr.hpBarW - (long)num3), 5, num3, 10);
			}
			g.drawImage(GameScr.imgHP, 83, 5, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (isLeft)
			{
				g.setClip(83, 20, num2, 6);
			}
			else
			{
				g.setClip(83 + GameScr.mpBarW - num2, 20, num2, 6);
			}
			g.drawImage(GameScr.imgMPLost, 83, 20, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (isLeft)
			{
				g.setClip(83, 20, num2, 6);
			}
			else
			{
				g.setClip(83 + GameScr.mpBarW - num4, 20, num4, 6);
			}
			g.drawImage(GameScr.imgMP, 83, 20, 0);
			g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			if (global::Char.myCharz().cMP == 0 && GameCanvas.gameTick % 10 > 5)
			{
				g.setClip(83, 20, 2, 6);
				g.drawImage(GameScr.imgMPLost, 83, 20, 0);
				g.setClip(0, 0, GameCanvas.w, GameCanvas.h);
			}
		}
	}

	// Token: 0x060003B7 RID: 951 RVA: 0x00004887 File Offset: 0x00002A87
	public void getInjure()
	{
	}

	// Token: 0x060003B8 RID: 952 RVA: 0x0004547C File Offset: 0x0004367C
	public void starVS()
	{
		this.curr = (this.last = mSystem.currentTimeMillis());
		this.secondVS = 180;
	}

	// Token: 0x060003B9 RID: 953 RVA: 0x000454A8 File Offset: 0x000436A8
	internal global::Char findCharVS1()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.cTypePk != 0)
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x060003BA RID: 954 RVA: 0x000454E8 File Offset: 0x000436E8
	internal global::Char findCharVS2()
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.cTypePk != 0 && @char != this.findCharVS1())
			{
				return @char;
			}
		}
		return null;
	}

	// Token: 0x060003BB RID: 955 RVA: 0x00045530 File Offset: 0x00043730
	internal void paintInfoBar(mGraphics g)
	{
		GameScr.resetTranslate(g);
		if (TileMap.mapID == 130 && this.findCharVS1() != null && this.findCharVS2() != null)
		{
			g.translate(GameCanvas.w / 2 - 62, 0);
			this.paintImageBar(g, true, this.findCharVS1());
			g.translate(-(GameCanvas.w / 2 - 65), 0);
			this.paintImageBarRight(g, this.findCharVS2());
			this.findCharVS1().paintHeadWithXY(g, 137, 25, 0);
			this.findCharVS2().paintHeadWithXY(g, GameCanvas.w - 15 - 122, 25, 2);
		}
		else if (this.isVS() && global::Char.myCharz().charFocus != null)
		{
			g.translate(GameCanvas.w / 2 - 62, 0);
			this.paintImageBar(g, true, global::Char.myCharz().charFocus);
			g.translate(-(GameCanvas.w / 2 - 65), 0);
			this.paintImageBarRight(g, global::Char.myCharz());
			global::Char.myCharz().paintHeadWithXY(g, 137, 25, 0);
			global::Char.myCharz().charFocus.paintHeadWithXY(g, GameCanvas.w - 15 - 122, 25, 2);
		}
		else if (GameScr.ispaintPhubangBar() && GameScr.isSmallScr())
		{
			GameScr.paintHPBar_NEW(g, 1, 1, global::Char.myCharz());
		}
		else
		{
			this.paintImageBar(g, true, global::Char.myCharz());
			if (global::Char.myCharz().isInEnterOfflinePoint() != null || global::Char.myCharz().isInEnterOnlinePoint() != null)
			{
				mFont.tahoma_7_green2.drawString(g, mResources.enter, this.imgScrW / 2, 8 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
			}
			else if (global::Char.myCharz().mobFocus != null)
			{
				if (global::Char.myCharz().mobFocus.getTemplate() != null)
				{
					mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().mobFocus.getTemplate().name, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				}
				if (global::Char.myCharz().mobFocus.templateId != 0)
				{
					mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys((long)global::Char.myCharz().mobFocus.hp) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				}
			}
			else if (global::Char.myCharz().npcFocus != null)
			{
				mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().npcFocus.template.name, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				if (global::Char.myCharz().npcFocus.template.npcTemplateId == 4)
				{
					mFont.tahoma_7b_green2.drawString(g, GameScr.gI().magicTree.currPeas.ToString() + "/" + GameScr.gI().magicTree.maxPeas.ToString(), this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				}
			}
			else if (global::Char.myCharz().charFocus != null)
			{
				mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().charFocus.cName, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys((long)global::Char.myCharz().charFocus.cHP) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
			}
			else
			{
				mFont.tahoma_7b_green2.drawString(g, global::Char.myCharz().cName, this.imgScrW / 2, 9 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
				mFont.tahoma_7b_green2.drawString(g, NinjaUtil.getMoneys(global::Char.myCharz().cPower) + string.Empty, this.imgScrW / 2, 22 + mGraphics.addYWhenOpenKeyBoard, mFont.CENTER);
			}
		}
		g.translate(-g.getTranslateX(), -g.getTranslateY());
		if (this.isVS() && this.secondVS > 0)
		{
			this.curr = mSystem.currentTimeMillis();
			if (this.curr - this.last >= 1000L)
			{
				this.last = mSystem.currentTimeMillis();
				this.secondVS--;
			}
			mFont.tahoma_7b_white.drawString(g, this.secondVS.ToString() + string.Empty, GameCanvas.w / 2, 13, 2, mFont.tahoma_7b_dark);
		}
		if (this.flareFindFocus)
		{
			g.drawImage(ItemMap.imageFlare, 40, 35, mGraphics.BOTTOM | mGraphics.HCENTER);
			this.flareTime--;
			if (this.flareTime < 0)
			{
				this.flareTime = 0;
				this.flareFindFocus = false;
			}
		}
	}

	// Token: 0x060003BC RID: 956 RVA: 0x000459E2 File Offset: 0x00043BE2
	public bool isVS()
	{
		return TileMap.isVoDaiMap() && (global::Char.myCharz().cTypePk != 0 || (TileMap.mapID == 130 && this.findCharVS1() != null && this.findCharVS2() != null));
	}

	// Token: 0x060003BD RID: 957 RVA: 0x00045A18 File Offset: 0x00043C18
	internal void paintSelectedSkill(mGraphics g)
	{
		if (this.mobCapcha != null)
		{
			this.paintCapcha(g);
			return;
		}
		if (GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || this.isPaintPopup() || GameCanvas.panel.isShow || global::Char.myCharz().taskMaint.taskId == 0 || ChatTextField.gI().isShow || GameCanvas.currentScreen == MoneyCharge.instance)
		{
			return;
		}
		long num = mSystem.currentTimeMillis() - this.lastUsePotion;
		int num2 = 0;
		if (num < 10000L)
		{
			num2 = (int)(num * 20L / 10000L);
		}
		if (!GameCanvas.isTouch)
		{
			g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgSkill : GameScr.imgSkill2, GameScr.xSkill + GameScr.xHP - 1, GameScr.yHP - 1, 0);
			SmallImage.drawSmallImage(g, 542, GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3, 0, 0);
			mFont.number_gray.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xSkill + GameScr.xHP + 22, GameScr.yHP + 15, 1);
			if (num < 10000L)
			{
				g.setColor(2721889);
				num2 = (int)(num * 20L / 10000L);
				g.fillRect(GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3 + num2, 20, 20 - num2);
			}
		}
		else if (global::Char.myCharz().statusMe != 14)
		{
			if (GameScr.gamePad.isSmallGamePad)
			{
				if (GameScr.isAnalog != 1)
				{
					g.setColor(9670800);
					g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 + 10, 22, 20);
					g.setColor(16777215);
					g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 + ((num2 != 0) ? (20 - num2) : 0) + 10, 22, (num2 == 0) ? 20 : num2);
					g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP1 : GameScr.imgHP2, GameScr.xHP, GameScr.yHP + 10, 0);
					mFont.tahoma_7_red.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xHP + 20, GameScr.yHP + 15 + 10, 2);
					if (GameScr.isPickNgocRong)
					{
						g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNR1 : GameScr.imgNR2, GameScr.xHP + 5, GameScr.yHP - 6 - 40 + 10, 0);
					}
					else if (GameScr.isudungCapsun4)
					{
						g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNutF : GameScr.imgNut, GameScr.xHP + 5, GameScr.yHP - 6 - 40 + 10, 0);
						SmallImage.drawSmallImage(g, 1088, GameScr.xHP - 7 + 5, GameScr.yHP - 6 - 40 - 7 + 10, 0, 0);
					}
					else if (GameScr.isudungCapsun3)
					{
						g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNutF : GameScr.imgNut, GameScr.xHP + 5, GameScr.yHP - 6 - 40 + 10, 0);
						SmallImage.drawSmallImage(g, 1087, GameScr.xHP - 7 + 5, GameScr.yHP - 6 - 40 - 7 + 10, 0, 0);
					}
				}
				else if (GameScr.isAnalog == 1)
				{
					int num3 = 10;
					g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgSkill : GameScr.imgSkill2, GameScr.xSkill + GameScr.xHP - 1, GameScr.yHP - 1 + num3, 0);
					SmallImage.drawSmallImage(g, 542, GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3 + num3, 0, 0);
					mFont.number_gray.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xSkill + GameScr.xHP + 22, GameScr.yHP + 13 + num3, 1);
					if (num < 10000L)
					{
						g.setColor(2721889);
						num2 = (int)(num * 20L / 10000L);
						g.fillRect(GameScr.xSkill + GameScr.xHP + 3, GameScr.yHP + 3 + num2 + num3, 20, 20 - num2);
					}
					if (GameScr.isPickNgocRong)
					{
						g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNR3 : GameScr.imgNR4, GameScr.xHP + 20 + 5, GameScr.yHP + 20 - 6 - 40 + 10, mGraphics.HCENTER | mGraphics.VCENTER);
					}
					else if (GameScr.isudungCapsun4)
					{
						g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNut : GameScr.imgNutF, GameScr.xHP + 20 + 5, GameScr.yHP + 20 - 6 - 40 + 10, mGraphics.HCENTER | mGraphics.VCENTER);
						SmallImage.drawSmallImage(g, 1088, GameScr.xHP + 20 - 7 + 5, GameScr.yHP + 20 - 6 - 40 - 7 + 10, 0, 0);
					}
					else if (GameScr.isudungCapsun3)
					{
						g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNut : GameScr.imgNutF, GameScr.xHP + 20 + 5, GameScr.yHP + 20 - 6 - 40 + 10, mGraphics.HCENTER | mGraphics.VCENTER);
						SmallImage.drawSmallImage(g, 1087, GameScr.xHP + 20 - 7 + 5, GameScr.yHP + 20 - 6 - 40 - 7 + 10, 0, 0);
					}
				}
			}
			else if (GameScr.isAnalog != 1)
			{
				g.setColor(9670800);
				g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 - 6, 22, 20);
				g.setColor(16777215);
				g.fillRect(GameScr.xHP + 9, GameScr.yHP + 10 + ((num2 != 0) ? (20 - num2) : 0) - 6, 22, (num2 == 0) ? 20 : num2);
				g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP1 : GameScr.imgHP2, GameScr.xHP, GameScr.yHP - 6, 0);
				mFont.tahoma_7_red.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xHP + 20, GameScr.yHP + 15 - 6, 2);
				if (GameScr.isPickNgocRong)
				{
					g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNR1 : GameScr.imgNR2, GameScr.xHP, GameScr.yHP - 6 - 40, 0);
				}
				else if (GameScr.isudungCapsun4)
				{
					g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNut : GameScr.imgNutF, GameScr.xHP + 20, GameScr.yHP + 20 - 6 - 40, mGraphics.HCENTER | mGraphics.VCENTER);
					SmallImage.drawSmallImage(g, 1088, GameScr.xHP + 20 - 7, GameScr.yHP + 20 - 6 - 40 - 7, 0, 0);
				}
				else if (GameScr.isudungCapsun3)
				{
					g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNut : GameScr.imgNutF, GameScr.xHP + 20, GameScr.yHP + 20 - 6 - 40, mGraphics.HCENTER | mGraphics.VCENTER);
					SmallImage.drawSmallImage(g, 1087, GameScr.xHP + 20 - 7, GameScr.yHP + 20 - 6 - 40 - 7, 0, 0);
				}
			}
			else
			{
				g.setColor(9670800);
				g.fillRect(GameScr.xHP + 10, GameScr.yHP + 10 - 6 + 10, 20, 18);
				g.setColor(16777215);
				g.fillRect(GameScr.xHP + 10, GameScr.yHP + 10 + ((num2 != 0) ? (20 - num2) : 0) - 6 + 10, 20, (num2 == 0) ? 18 : num2);
				g.drawImage((mScreen.keyTouch != 10) ? GameScr.imgHP3 : GameScr.imgHP4, GameScr.xHP + 20, GameScr.yHP + 20 - 6 + 10, mGraphics.HCENTER | mGraphics.VCENTER);
				mFont.tahoma_7_red.drawString(g, string.Empty + GameScr.hpPotion.ToString(), GameScr.xHP + 20, GameScr.yHP + 15 - 6 + 10, 2);
				if (GameScr.isPickNgocRong)
				{
					g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNR3 : GameScr.imgNR4, GameScr.xHP + 20 + 5, GameScr.yHP + 20 - 6 - 40 + 10, mGraphics.HCENTER | mGraphics.VCENTER);
				}
				else if (GameScr.isudungCapsun4)
				{
					g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNut : GameScr.imgNutF, GameScr.xHP + 20 + 5, GameScr.yHP + 20 - 6 - 40 + 10, mGraphics.HCENTER | mGraphics.VCENTER);
					SmallImage.drawSmallImage(g, 1088, GameScr.xHP + 20 - 7 + 5, GameScr.yHP + 20 - 6 - 40 - 7 + 10, 0, 0);
				}
				else if (GameScr.isudungCapsun3)
				{
					g.drawImage((mScreen.keyTouch != 14) ? GameScr.imgNut : GameScr.imgNutF, GameScr.xHP + 20 + 5, GameScr.yHP + 20 - 6 - 40 + 10, mGraphics.HCENTER | mGraphics.VCENTER);
					SmallImage.drawSmallImage(g, 1087, GameScr.xHP + 20 - 7 + 5, GameScr.yHP + 20 - 6 - 40 - 7 + 10, 0, 0);
				}
			}
		}
		if (GameScr.isHaveSelectSkill)
		{
			Skill[] array = (Main.isPC ? GameScr.keySkill : ((!GameCanvas.isTouch) ? GameScr.keySkill : GameScr.onScreenSkill));
			int keyTouch = mScreen.keyTouch;
			if (!GameCanvas.isTouch)
			{
				g.setColor(11152401);
				g.fillRect(GameScr.xSkill + GameScr.xHP + 2, GameScr.yHP - 10 + 6, 20, 10);
				mFont.tahoma_7_white.drawString(g, "*", GameScr.xSkill + GameScr.xHP + 12, GameScr.yHP - 8 + 6, mFont.CENTER);
			}
			int num4 = (Main.isPC ? array.Length : ((!GameCanvas.isTouch) ? array.Length : this.nSkill));
			for (int i = 0; i < num4; i++)
			{
				if (Main.isPC)
				{
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
					int num5 = -13;
					if (num4 > 5 && i < 5)
					{
						num5 = 27;
					}
					mFont.tahoma_7b_dark.drawString(g, array5[i], GameScr.xSkill + GameScr.xS[i] + 14, GameScr.yS[i] + num5, mFont.CENTER);
					mFont.tahoma_7b_white.drawString(g, array5[i], GameScr.xSkill + GameScr.xS[i] + 14, GameScr.yS[i] + num5 + 1, mFont.CENTER);
				}
				else if (!GameCanvas.isTouch)
				{
					string[] array7;
					if (!TField.isQwerty)
					{
						string[] array6 = new string[5];
						array6[0] = "7";
						array6[1] = "8";
						array6[2] = "9";
						array6[3] = "1";
						array7 = array6;
						array6[4] = "3";
					}
					else
					{
						string[] array8 = new string[5];
						array8[0] = "Q";
						array8[1] = "W";
						array8[2] = "E";
						array8[3] = "R";
						array7 = array8;
						array8[4] = "T";
					}
					string[] array9 = array7;
					g.setColor(11152401);
					g.fillRect(GameScr.xSkill + GameScr.xS[i] + 2, GameScr.yS[i] - 10 + 8, 20, 10);
					mFont.tahoma_7_white.drawString(g, array9[i], GameScr.xSkill + GameScr.xS[i] + 12, GameScr.yS[i] - 10 + 6, mFont.CENTER);
				}
				Skill skill = array[i];
				if (skill != global::Char.myCharz().myskill)
				{
					g.drawImage(GameScr.imgSkill, GameScr.xSkill + GameScr.xS[i] - 1, GameScr.yS[i] - 1, 0);
				}
				if (skill != null)
				{
					if (skill == global::Char.myCharz().myskill)
					{
						g.drawImage(GameScr.imgSkill2, GameScr.xSkill + GameScr.xS[i] - 1, GameScr.yS[i] - 1, 0);
						if (GameCanvas.isTouch && !Main.isPC)
						{
							g.drawRegion(Mob.imgHP, 0, 12, 9, 6, 0, GameScr.xSkill + GameScr.xS[i] + 8, GameScr.yS[i] - 7, 0);
						}
					}
					skill.paint(GameScr.xSkill + GameScr.xS[i] + 13, GameScr.yS[i] + 13, g);
					if ((i == this.selectedIndexSkill && !this.isPaintUI() && GameCanvas.gameTick % 10 > 5) || i == this.keyTouchSkill)
					{
						g.drawImage(ItemMap.imageFlare, GameScr.xSkill + GameScr.xS[i] + 13, GameScr.yS[i] + 14, 3);
					}
				}
			}
		}
		this.paintGamePad(g);
	}

	// Token: 0x060003BE RID: 958 RVA: 0x00046758 File Offset: 0x00044958
	public void paintOpen(mGraphics g)
	{
		if (this.isstarOpen)
		{
			g.translate(-g.getTranslateX(), -g.getTranslateY());
			g.fillRect(0, 0, GameCanvas.w, this.moveUp);
			g.setColor(10275899);
			g.fillRect(0, this.moveUp - 1, GameCanvas.w, 1);
			g.fillRect(0, this.moveDow + 1, GameCanvas.w, 1);
		}
	}

	// Token: 0x060003BF RID: 959 RVA: 0x000467CC File Offset: 0x000449CC
	public static void startFlyText(string flyString, int x, int y, int dx, int dy, int color)
	{
		int num = -1;
		for (int i = 0; i < 5; i++)
		{
			if (GameScr.flyTextState[i] == -1)
			{
				num = i;
				break;
			}
		}
		if (num == -1)
		{
			return;
		}
		GameScr.flyTextColor[num] = color;
		GameScr.flyTextString[num] = flyString;
		GameScr.flyTextX[num] = x;
		GameScr.flyTextY[num] = y;
		GameScr.flyTextDx[num] = dx;
		GameScr.flyTextDy[num] = ((dy >= 0) ? 5 : (-5));
		GameScr.flyTextState[num] = 0;
		GameScr.flyTime[num] = 0;
		GameScr.flyTextYTo[num] = 10;
		for (int j = 0; j < 5; j++)
		{
			if (GameScr.flyTextState[j] != -1 && num != j && GameScr.flyTextDy[num] < 0 && Res.abs(GameScr.flyTextX[num] - GameScr.flyTextX[j]) <= 20 && GameScr.flyTextYTo[num] == GameScr.flyTextYTo[j])
			{
				GameScr.flyTextYTo[num] += 10;
			}
		}
	}

	// Token: 0x060003C0 RID: 960 RVA: 0x000468AC File Offset: 0x00044AAC
	public static void updateFlyText()
	{
		for (int i = 0; i < 5; i++)
		{
			if (GameScr.flyTextState[i] != -1)
			{
				if (GameScr.flyTextState[i] > GameScr.flyTextYTo[i])
				{
					GameScr.flyTime[i]++;
					if (GameScr.flyTime[i] == 25)
					{
						GameScr.flyTime[i] = 0;
						GameScr.flyTextState[i] = -1;
						GameScr.flyTextYTo[i] = 0;
						GameScr.flyTextDx[i] = 0;
						GameScr.flyTextX[i] = 0;
					}
				}
				else
				{
					GameScr.flyTextState[i] += Res.abs(GameScr.flyTextDy[i]);
					GameScr.flyTextX[i] += GameScr.flyTextDx[i];
					GameScr.flyTextY[i] += GameScr.flyTextDy[i];
				}
			}
		}
	}

	// Token: 0x060003C1 RID: 961 RVA: 0x00046974 File Offset: 0x00044B74
	public static void loadSplash()
	{
		if (GameScr.imgSplash == null)
		{
			GameScr.imgSplash = new Image[3];
			for (int i = 0; i < 3; i++)
			{
				GameScr.imgSplash[i] = GameCanvas.loadImage("/e/sp" + i.ToString() + ".png");
			}
		}
		GameScr.splashX = new int[2];
		GameScr.splashY = new int[2];
		GameScr.splashState = new int[2];
		GameScr.splashF = new int[2];
		GameScr.splashDir = new int[2];
		GameScr.splashState[0] = (GameScr.splashState[1] = -1);
	}

	// Token: 0x060003C2 RID: 962 RVA: 0x00046A0C File Offset: 0x00044C0C
	public static bool startSplash(int x, int y, int dir)
	{
		int num = ((GameScr.splashState[0] != -1) ? 1 : 0);
		if (GameScr.splashState[num] != -1)
		{
			return false;
		}
		GameScr.splashState[num] = 0;
		GameScr.splashDir[num] = dir;
		GameScr.splashX[num] = x;
		GameScr.splashY[num] = y;
		return true;
	}

	// Token: 0x060003C3 RID: 963 RVA: 0x00046A58 File Offset: 0x00044C58
	public static void updateSplash()
	{
		for (int i = 0; i < 2; i++)
		{
			if (GameScr.splashState[i] != -1)
			{
				GameScr.splashState[i]++;
				GameScr.splashX[i] += GameScr.splashDir[i] << 2;
				GameScr.splashY[i]--;
				if (GameScr.splashState[i] >= 6)
				{
					GameScr.splashState[i] = -1;
				}
				else
				{
					GameScr.splashF[i] = (GameScr.splashState[i] >> 1) % 3;
				}
			}
		}
	}

	// Token: 0x060003C4 RID: 964 RVA: 0x00046ADC File Offset: 0x00044CDC
	public static void paintSplash(mGraphics g)
	{
		for (int i = 0; i < 2; i++)
		{
			if (GameScr.splashState[i] != -1)
			{
				if (GameScr.splashDir[i] == 1)
				{
					g.drawImage(GameScr.imgSplash[GameScr.splashF[i]], GameScr.splashX[i], GameScr.splashY[i], 3);
				}
				else
				{
					g.drawRegion(GameScr.imgSplash[GameScr.splashF[i]], 0, 0, mGraphics.getImageWidth(GameScr.imgSplash[GameScr.splashF[i]]), mGraphics.getImageHeight(GameScr.imgSplash[GameScr.splashF[i]]), 2, GameScr.splashX[i], GameScr.splashY[i], 3);
				}
			}
		}
	}

	// Token: 0x060003C5 RID: 965 RVA: 0x00046B7C File Offset: 0x00044D7C
	internal void loadInforBar()
	{
		this.imgScrW = 84;
		GameScr.hpBarW = 66L;
		GameScr.mpBarW = 59;
		GameScr.hpBarX = 52;
		GameScr.hpBarY = 10;
		GameScr.spBarW = 61;
		GameScr.expBarW = GameScr.gW - 61;
	}

	// Token: 0x060003C6 RID: 966 RVA: 0x00046BB8 File Offset: 0x00044DB8
	public void updateSS()
	{
		if (GameScr.indexMenu != -1)
		{
			if (GameScr.cmySK != GameScr.cmtoYSK)
			{
				GameScr.cmvySK = GameScr.cmtoYSK - GameScr.cmySK << 2;
				GameScr.cmdySK += GameScr.cmvySK;
				GameScr.cmySK += GameScr.cmdySK >> 4;
				GameScr.cmdySK &= 15;
			}
			if (Math2.abs(GameScr.cmtoYSK - GameScr.cmySK) < 15 && GameScr.cmySK < 0)
			{
				GameScr.cmtoYSK = 0;
			}
			if (Math2.abs(GameScr.cmtoYSK - GameScr.cmySK) < 15 && GameScr.cmySK > GameScr.cmyLimSK)
			{
				GameScr.cmtoYSK = GameScr.cmyLimSK;
			}
		}
	}

	// Token: 0x060003C7 RID: 967 RVA: 0x00046C6C File Offset: 0x00044E6C
	public void updateKeyAlert()
	{
		if (!GameScr.isPaintAlert || GameCanvas.currentDialog != null)
		{
			return;
		}
		bool flag = false;
		if (GameCanvas.keyPressed[Key.NUM8])
		{
			GameScr.indexRow++;
			if (GameScr.indexRow >= this.texts.size())
			{
				GameScr.indexRow = 0;
			}
			flag = true;
		}
		else if (GameCanvas.keyPressed[Key.NUM2])
		{
			GameScr.indexRow--;
			if (GameScr.indexRow < 0)
			{
				GameScr.indexRow = this.texts.size() - 1;
			}
			flag = true;
		}
		if (flag)
		{
			GameScr.scrMain.moveTo(GameScr.indexRow * GameScr.scrMain.ITEM_SIZE);
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
		}
		if (GameCanvas.isTouch)
		{
			ScrollResult scrollResult = GameScr.scrMain.updateKey();
			if (scrollResult.isDowning || scrollResult.isFinish)
			{
				GameScr.indexRow = scrollResult.selected;
				flag = true;
			}
		}
		if (!flag || GameScr.indexRow < 0 || GameScr.indexRow >= this.texts.size())
		{
			return;
		}
		string text = (string)this.texts.elementAt(GameScr.indexRow);
		this.fnick = null;
		this.alertURL = null;
		this.center = null;
		ChatTextField.gI().center = null;
		int num;
		if ((num = text.IndexOf("http://")) >= 0)
		{
			Cout.println("currentLine: " + text);
			this.alertURL = text.Substring(num);
			this.center = new Command(mResources.open_link, 12000);
			if (!GameCanvas.isTouch)
			{
				ChatTextField.gI().center = new Command(mResources.open_link, null, 12000, null);
				return;
			}
		}
		else
		{
			if (text.IndexOf("@") < 0)
			{
				return;
			}
			string text2 = text.Substring(2).Trim();
			num = text2.IndexOf("@");
			string text3 = text2.Substring(num);
			int num2 = text3.IndexOf(" ");
			this.fnick = text2.Substring(num + 1, (num2 > 0) ? (num2 + num) : (num + text3.Length));
			if (!this.fnick.Equals(string.Empty) && !this.fnick.Equals(global::Char.myCharz().cName))
			{
				this.center = new Command(mResources.SELECT, 12009, this.fnick);
				if (!GameCanvas.isTouch)
				{
					ChatTextField.gI().center = new Command(mResources.SELECT, null, 12009, this.fnick);
					return;
				}
			}
			else
			{
				this.fnick = null;
				this.center = null;
			}
		}
	}

	// Token: 0x060003C8 RID: 968 RVA: 0x00046EF0 File Offset: 0x000450F0
	public bool isPaintPopup()
	{
		return GameScr.isPaintItemInfo || GameScr.isPaintInfoMe || GameScr.isPaintStore || GameScr.isPaintWeapon || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintSplit || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintTrade || GameScr.isPaintAlert || GameScr.isPaintZone || GameScr.isPaintTeam || GameScr.isPaintClan || GameScr.isPaintFindTeam || GameScr.isPaintTask || GameScr.isPaintFriend || GameScr.isPaintEnemies || GameScr.isPaintCharInMap || GameScr.isPaintMessage;
	}

	// Token: 0x060003C9 RID: 969 RVA: 0x00047044 File Offset: 0x00045244
	public bool isNotPaintTouchControl()
	{
		return (!GameCanvas.isTouchControl && GameCanvas.currentScreen == GameScr.gI()) || !GameCanvas.isTouch || ChatTextField.gI().isShow || InfoDlg.isShow || (GameCanvas.currentDialog != null || ChatPopup.currChatPopup != null || GameCanvas.menu.showMenu || GameCanvas.panel.isShow || this.isPaintPopup());
	}

	// Token: 0x060003CA RID: 970 RVA: 0x000470B8 File Offset: 0x000452B8
	public bool isPaintUI()
	{
		return GameScr.isPaintStore || GameScr.isPaintWeapon || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintSplit || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintTrade;
	}

	// Token: 0x060003CB RID: 971 RVA: 0x00047194 File Offset: 0x00045394
	public bool isOpenUI()
	{
		return GameScr.isPaintItemInfo || GameScr.isPaintInfoMe || GameScr.isPaintStore || GameScr.isPaintNonNam || GameScr.isPaintNonNu || GameScr.isPaintAoNam || GameScr.isPaintAoNu || GameScr.isPaintGangTayNam || GameScr.isPaintGangTayNu || GameScr.isPaintQuanNam || GameScr.isPaintQuanNu || GameScr.isPaintGiayNam || GameScr.isPaintGiayNu || GameScr.isPaintLien || GameScr.isPaintNhan || GameScr.isPaintNgocBoi || GameScr.isPaintPhu || GameScr.isPaintWeapon || GameScr.isPaintStack || GameScr.isPaintStackLock || GameScr.isPaintGrocery || GameScr.isPaintGroceryLock || GameScr.isPaintUpGrade || GameScr.isPaintConvert || GameScr.isPaintUpPearl || GameScr.isPaintBox || GameScr.isPaintSplit || GameScr.isPaintTrade;
	}

	// Token: 0x060003CC RID: 972 RVA: 0x00047284 File Offset: 0x00045484
	public static void setPopupSize(int w, int h)
	{
		if (GameCanvas.w == 128 || GameCanvas.h <= 208)
		{
			w = 126;
			h = 160;
		}
		GameScr.indexTitle = 0;
		GameScr.popupW = w;
		GameScr.popupH = h;
		GameScr.popupX = GameScr.gW2 - w / 2;
		GameScr.popupY = GameScr.gH2 - h / 2;
		if (GameCanvas.isTouch && !GameScr.isPaintZone && !GameScr.isPaintTeam && !GameScr.isPaintClan && !GameScr.isPaintCharInMap && !GameScr.isPaintFindTeam && !GameScr.isPaintFriend && !GameScr.isPaintEnemies && !GameScr.isPaintTask && !GameScr.isPaintMessage)
		{
			if (GameCanvas.h <= 240)
			{
				GameScr.popupY -= 10;
			}
			if (GameCanvas.isTouch && !GameCanvas.isTouchControlSmallScreen && GameCanvas.currentScreen is GameScr)
			{
				GameScr.popupW = 310;
				GameScr.popupX = GameScr.gW / 2 - GameScr.popupW / 2;
				if (GameScr.isPaintInfoMe && GameScr.indexMenu > 0)
				{
					GameScr.popupW = w;
					GameScr.popupX = GameScr.gW2 - w / 2;
				}
			}
		}
		if (GameScr.popupY < -10)
		{
			GameScr.popupY = -10;
		}
		if (GameCanvas.h > 208 && GameScr.popupY < 0)
		{
			GameScr.popupY = 0;
		}
		if (GameCanvas.h == 208 && GameScr.popupY < 10)
		{
			GameScr.popupY = 10;
		}
	}

	// Token: 0x060003CD RID: 973 RVA: 0x000473FA File Offset: 0x000455FA
	public static void loadImg()
	{
		TileMap.loadTileImage();
	}

	// Token: 0x060003CE RID: 974 RVA: 0x00047404 File Offset: 0x00045604
	public void paintTitle(mGraphics g, string title, bool arrow)
	{
		int num = GameScr.gW / 2;
		g.setColor(Paint.COLORDARK);
		g.fillRoundRect(num - mFont.tahoma_8b.getWidth(title) / 2 - 12, GameScr.popupY + 4, mFont.tahoma_8b.getWidth(title) + 22, 24, 6, 6);
		if ((GameScr.indexTitle == 0 || GameCanvas.isTouch) && arrow)
		{
			SmallImage.drawSmallImage(g, 989, num - mFont.tahoma_8b.getWidth(title) / 2 - 15 - 7 - ((GameCanvas.gameTick % 8 <= 3) ? 2 : 0), GameScr.popupY + 16, 2, StaticObj.VCENTER_HCENTER);
			SmallImage.drawSmallImage(g, 989, num + mFont.tahoma_8b.getWidth(title) / 2 + 15 + 5 + ((GameCanvas.gameTick % 8 <= 3) ? 2 : 0), GameScr.popupY + 16, 0, StaticObj.VCENTER_HCENTER);
		}
		if (GameScr.indexTitle == 0)
		{
			g.setColor(Paint.COLORFOCUS);
		}
		else
		{
			g.setColor(Paint.COLORBORDER);
		}
		g.drawRoundRect(num - mFont.tahoma_8b.getWidth(title) / 2 - 12, GameScr.popupY + 4, mFont.tahoma_8b.getWidth(title) + 22, 24, 6, 6);
		mFont.tahoma_8b.drawString(g, title, num, GameScr.popupY + 9, 2);
	}

	// Token: 0x060003CF RID: 975 RVA: 0x00047548 File Offset: 0x00045748
	public static int getTaskMapId()
	{
		if (global::Char.myCharz().taskMaint == null)
		{
			return -1;
		}
		return GameScr.mapTasks[global::Char.myCharz().taskMaint.index];
	}

	// Token: 0x060003D0 RID: 976 RVA: 0x00047570 File Offset: 0x00045770
	public static sbyte getTaskNpcId()
	{
		sbyte b = 0;
		if (global::Char.myCharz().taskMaint == null)
		{
			b = -1;
		}
		else if (global::Char.myCharz().taskMaint.index <= GameScr.tasks.Length - 1)
		{
			b = (sbyte)GameScr.tasks[global::Char.myCharz().taskMaint.index];
		}
		return b;
	}

	// Token: 0x060003D1 RID: 977 RVA: 0x00004887 File Offset: 0x00002A87
	public void refreshTeam()
	{
	}

	// Token: 0x060003D2 RID: 978 RVA: 0x000475C4 File Offset: 0x000457C4
	public void onChatFromMe(string text, string to)
	{
		Res.outz("CHAT");
		if (!GameScr.isPaintMessage || GameCanvas.isTouch)
		{
			ChatTextField.gI().isShow = false;
		}
		if (to.Equals(mResources.chat_player))
		{
			if (GameScr.info2.playerID != global::Char.myCharz().charID)
			{
				Service.gI().chatPlayer(text, GameScr.info2.playerID);
				return;
			}
		}
		else if (!text.Equals(string.Empty))
		{
			Service.gI().chat(text);
		}
	}

	// Token: 0x060003D3 RID: 979 RVA: 0x00047645 File Offset: 0x00045845
	public void onCancelChat()
	{
		if (GameScr.isPaintMessage)
		{
			GameScr.isPaintMessage = false;
			ChatTextField.gI().center = null;
		}
	}

	// Token: 0x060003D4 RID: 980 RVA: 0x00047660 File Offset: 0x00045860
	public void openWeb(string strLeft, string strRight, string url, string title, string str)
	{
		GameScr.isPaintAlert = true;
		this.isLockKey = true;
		GameScr.indexRow = 0;
		GameScr.setPopupSize(175, 200);
		this.textsTitle = title;
		this.texts = mFont.tahoma_7.splitFontVector(str, GameScr.popupW - 30);
		this.center = null;
		this.left = new Command(strLeft, 11068, url);
		this.right = new Command(strRight, 11069);
	}

	// Token: 0x060003D5 RID: 981 RVA: 0x000476DC File Offset: 0x000458DC
	public void sendSms(string strLeft, string strRight, short port, string syntax, string title, string str)
	{
		GameScr.isPaintAlert = true;
		this.isLockKey = true;
		GameScr.indexRow = 0;
		GameScr.setPopupSize(175, 200);
		this.textsTitle = title;
		this.texts = mFont.tahoma_7.splitFontVector(str, GameScr.popupW - 30);
		this.center = null;
		MyVector myVector = new MyVector();
		myVector.addElement(string.Empty + port.ToString());
		myVector.addElement(syntax);
		this.left = new Command(strLeft, 11074);
		this.right = new Command(strRight, 11075);
	}

	// Token: 0x060003D6 RID: 982 RVA: 0x00047779 File Offset: 0x00045979
	public void actMenu()
	{
		GameCanvas.panel.setTypeMain();
		GameCanvas.panel.show();
	}

	// Token: 0x060003D7 RID: 983 RVA: 0x00047790 File Offset: 0x00045990
	public void openUIZone(Message message)
	{
		InfoDlg.hide();
		try
		{
			this.zones = new int[(int)message.reader().readByte()];
			this.pts = new int[this.zones.Length];
			this.numPlayer = new int[this.zones.Length];
			this.maxPlayer = new int[this.zones.Length];
			this.rank1 = new int[this.zones.Length];
			this.rankName1 = new string[this.zones.Length];
			this.rank2 = new int[this.zones.Length];
			this.rankName2 = new string[this.zones.Length];
			for (int i = 0; i < this.zones.Length; i++)
			{
				this.zones[i] = (int)message.reader().readByte();
				this.pts[i] = (int)message.reader().readByte();
				this.numPlayer[i] = (int)message.reader().readByte();
				this.maxPlayer[i] = (int)message.reader().readByte();
				if (message.reader().readByte() == 1)
				{
					this.rankName1[i] = message.reader().readUTF();
					this.rank1[i] = message.reader().readInt();
					this.rankName2[i] = message.reader().readUTF();
					this.rank2[i] = message.reader().readInt();
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi ham OPEN UIZONE " + ex.ToString());
		}
		GameCanvas.panel.setTypeZone();
		GameCanvas.panel.show();
	}

	// Token: 0x060003D8 RID: 984 RVA: 0x00047948 File Offset: 0x00045B48
	public void showViewInfo()
	{
		GameScr.indexMenu = 3;
		GameScr.isPaintInfoMe = true;
		GameScr.setPopupSize(175, 200);
	}

	// Token: 0x060003D9 RID: 985 RVA: 0x00047968 File Offset: 0x00045B68
	internal void actDead()
	{
		MyVector myVector = new MyVector();
		myVector.addElement(new Command(mResources.DIES[1], 110381));
		myVector.addElement(new Command(mResources.DIES[2], 110382));
		myVector.addElement(new Command(mResources.DIES[3], 110383));
		GameCanvas.menu.startAt(myVector, 3);
	}

	// Token: 0x060003DA RID: 986 RVA: 0x000479CC File Offset: 0x00045BCC
	public void startYesNoPopUp(string info, Command cmdYes, Command cmdNo)
	{
		this.popUpYesNo = new PopUpYesNo();
		this.popUpYesNo.setPopUp(info, cmdYes, cmdNo);
	}

	// Token: 0x060003DB RID: 987 RVA: 0x000479E8 File Offset: 0x00045BE8
	public void player_vs_player(int playerId, int xu, string info, sbyte typePK)
	{
		global::Char @char = GameScr.findCharInMap(playerId);
		if (@char != null)
		{
			if (typePK == 3)
			{
				this.startYesNoPopUp(info, new Command(mResources.OK, 2000, @char), new Command(mResources.CLOSE, 2009, @char));
			}
			if (typePK == 4)
			{
				this.startYesNoPopUp(info, new Command(mResources.OK, 2005, @char), new Command(mResources.CLOSE, 2009, @char));
			}
		}
	}

	// Token: 0x060003DC RID: 988 RVA: 0x00047A58 File Offset: 0x00045C58
	public void giaodich(int playerID)
	{
		global::Char @char = GameScr.findCharInMap(playerID);
		if (@char != null)
		{
			this.startYesNoPopUp(@char.cName + mResources.want_to_trade, new Command(mResources.YES, 11114, @char), new Command(mResources.NO, 2009, @char));
		}
	}

	// Token: 0x060003DD RID: 989 RVA: 0x00047AA8 File Offset: 0x00045CA8
	public void getFlagImage(int charID, sbyte cflag)
	{
		if (GameScr.vFlag.size() == 0)
		{
			Service.gI().getFlag(2, cflag);
			Res.outz("getFlag1");
			return;
		}
		if (charID == global::Char.myCharz().charID)
		{
			Res.outz("my cflag: isme");
			if (global::Char.myCharz().isGetFlagImage(cflag))
			{
				Res.outz("my cflag: true");
				for (int i = 0; i < GameScr.vFlag.size(); i++)
				{
					PKFlag pkflag = (PKFlag)GameScr.vFlag.elementAt(i);
					if (pkflag != null && pkflag.cflag == cflag)
					{
						Res.outz("my cflag: cflag==");
						global::Char.myCharz().flagImage = pkflag.IDimageFlag;
					}
				}
				return;
			}
			if (!global::Char.myCharz().isGetFlagImage(cflag))
			{
				Res.outz("my cflag: false");
				Service.gI().getFlag(2, cflag);
			}
			return;
		}
		else
		{
			Res.outz("my cflag: not me");
			if (GameScr.findCharInMap(charID) == null)
			{
				return;
			}
			if (GameScr.findCharInMap(charID).isGetFlagImage(cflag))
			{
				Res.outz("my cflag: true");
				for (int j = 0; j < GameScr.vFlag.size(); j++)
				{
					PKFlag pkflag2 = (PKFlag)GameScr.vFlag.elementAt(j);
					if (pkflag2 != null && pkflag2.cflag == cflag)
					{
						Res.outz("my cflag: cflag==");
						GameScr.findCharInMap(charID).flagImage = pkflag2.IDimageFlag;
					}
				}
				return;
			}
			if (!GameScr.findCharInMap(charID).isGetFlagImage(cflag))
			{
				Res.outz("my cflag: false");
				Service.gI().getFlag(2, cflag);
			}
			return;
		}
	}

	// Token: 0x060003DE RID: 990 RVA: 0x00047C18 File Offset: 0x00045E18
	public void actionPerform(int idAction, object p)
	{
		Cout.println("PERFORM WITH ID = " + idAction.ToString());
		switch (idAction)
		{
		case 2000:
			this.popUpYesNo = null;
			GameCanvas.endDlg();
			if ((global::Char)p == null)
			{
				Service.gI().player_vs_player(1, 3, -1);
				return;
			}
			Service.gI().player_vs_player(1, 3, ((global::Char)p).charID);
			Service.gI().charMove();
			return;
		case 2001:
			GameCanvas.endDlg();
			return;
		case 2003:
			GameCanvas.endDlg();
			InfoDlg.showWait();
			Service.gI().player_vs_player(0, 3, global::Char.myCharz().charFocus.charID);
			return;
		case 2004:
			GameCanvas.endDlg();
			Service.gI().player_vs_player(0, 4, global::Char.myCharz().charFocus.charID);
			return;
		case 2005:
			GameCanvas.endDlg();
			this.popUpYesNo = null;
			if ((global::Char)p == null)
			{
				Service.gI().player_vs_player(1, 4, -1);
				return;
			}
			Service.gI().player_vs_player(1, 4, ((global::Char)p).charID);
			return;
		case 2006:
			GameCanvas.endDlg();
			Service.gI().player_vs_player(2, 4, global::Char.myCharz().charFocus.charID);
			return;
		case 2007:
			GameCanvas.endDlg();
			GameMidlet.instance.exit();
			return;
		case 2009:
			this.popUpYesNo = null;
			return;
		}
		switch (idAction)
		{
		case 11111:
			if (global::Char.myCharz().charFocus != null)
			{
				InfoDlg.showWait();
				if (GameCanvas.panel.vPlayerMenu.size() <= 0)
				{
					this.playerMenu(global::Char.myCharz().charFocus);
				}
				GameCanvas.panel.setTypePlayerMenu(global::Char.myCharz().charFocus);
				GameCanvas.panel.show();
				Service.gI().getPlayerMenu(global::Char.myCharz().charFocus.charID);
				Service.gI().messagePlayerMenu(global::Char.myCharz().charFocus.charID);
			}
			return;
		case 11112:
		{
			global::Char @char = (global::Char)p;
			Service.gI().friend(1, @char.charID);
			return;
		}
		case 11113:
		{
			global::Char char2 = (global::Char)p;
			if (char2 != null)
			{
				Service.gI().giaodich(0, char2.charID, -1, -1);
			}
			return;
		}
		case 11114:
		{
			this.popUpYesNo = null;
			GameCanvas.endDlg();
			global::Char char3 = (global::Char)p;
			if (char3 != null)
			{
				Service.gI().giaodich(1, char3.charID, -1, -1);
			}
			return;
		}
		case 11115:
			if (global::Char.myCharz().charFocus != null)
			{
				InfoDlg.showWait();
				Service.gI().playerMenuAction(global::Char.myCharz().charFocus.charID, (short)global::Char.myCharz().charFocus.menuSelect);
			}
			return;
		case 11120:
		{
			object[] array = (object[])p;
			Skill skill = (Skill)array[0];
			int num = int.Parse((string)array[1]);
			for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
			{
				if (GameScr.onScreenSkill[i] == skill)
				{
					GameScr.onScreenSkill[i] = null;
				}
			}
			GameScr.onScreenSkill[num] = skill;
			this.saveonScreenSkillToRMS();
			return;
		}
		case 11121:
		{
			object[] array2 = (object[])p;
			Skill skill2 = (Skill)array2[0];
			int num2 = int.Parse((string)array2[1]);
			for (int j = 0; j < GameScr.keySkill.Length; j++)
			{
				if (GameScr.keySkill[j] == skill2)
				{
					GameScr.keySkill[j] = null;
				}
			}
			GameScr.keySkill[num2] = skill2;
			this.saveKeySkillToRMS();
			return;
		}
		}
		switch (idAction)
		{
		case 12000:
			Service.gI().getClan(1, -1, null);
			return;
		case 12001:
			GameCanvas.endDlg();
			return;
		case 12002:
		{
			GameCanvas.endDlg();
			ClanObject clanObject = (ClanObject)p;
			Service.gI().clanInvite(1, -1, clanObject.clanID, clanObject.code);
			this.popUpYesNo = null;
			return;
		}
		case 12003:
		{
			ClanObject clanObject2 = (ClanObject)p;
			GameCanvas.endDlg();
			Service.gI().clanInvite(2, -1, clanObject2.clanID, clanObject2.code);
			this.popUpYesNo = null;
			return;
		}
		case 12004:
			this.doUseSkill((Skill)p, true);
			global::Char.myCharz().saveLoadPreviousSkill();
			return;
		case 12005:
			if (GameCanvas.serverScr == null)
			{
				GameCanvas.serverScr = new ServerScr();
			}
			GameCanvas.serverScr.switchToMe();
			GameCanvas.endDlg();
			return;
		case 12006:
			GameMidlet.instance.exit();
			return;
		default:
			switch (idAction)
			{
			case 11000:
				this.actMenu();
				return;
			case 11001:
				global::Char.myCharz().findNextFocusByKey();
				return;
			case 11002:
				GameCanvas.panel.hide();
				return;
			default:
				if (idAction != 1)
				{
					if (idAction == 2)
					{
						GameCanvas.menu.showMenu = false;
						return;
					}
					if (idAction != 11057)
					{
						if (idAction == 11059)
						{
							this.doUseSkill(GameScr.onScreenSkill[this.selectedIndexSkill], false);
							this.center = null;
							return;
						}
						if (idAction == 110001)
						{
							GameCanvas.panel.setTypeMain();
							GameCanvas.panel.show();
							return;
						}
						if (idAction == 110004)
						{
							GameCanvas.menu.showMenu = false;
							return;
						}
						if (idAction == 110382)
						{
							Service.gI().returnTownFromDead();
							return;
						}
						if (idAction == 110383)
						{
							Service.gI().wakeUpFromDead();
							return;
						}
						if (idAction == 8002)
						{
							this.doFire(false, true);
							GameCanvas.clearKeyHold();
							GameCanvas.clearKeyPressed();
							return;
						}
						if (idAction == 11038)
						{
							this.actDead();
							return;
						}
						if (idAction != 11067)
						{
							if (idAction == 110391)
							{
								Service.gI().clanInvite(0, global::Char.myCharz().charFocus.charID, -1, -1);
								return;
							}
							if (idAction == 888351)
							{
								Service.gI().petStatus(5);
								GameCanvas.endDlg();
								return;
							}
						}
						else
						{
							if (TileMap.zoneID != GameScr.indexSelect)
							{
								Service.gI().requestChangeZone(GameScr.indexSelect, this.indexItemUse);
								InfoDlg.showWait();
								return;
							}
							GameScr.info1.addInfo(mResources.ZONE_HERE, 0);
							return;
						}
					}
					else
					{
						Effect2.vEffect2Outside.removeAllElements();
						Effect2.vEffect2.removeAllElements();
						Npc npc = (Npc)p;
						if (npc.idItem == 0)
						{
							Service.gI().confirmMenu((short)npc.template.npcTemplateId, (sbyte)GameCanvas.menu.menuSelectedItem);
							return;
						}
						if (GameCanvas.menu.menuSelectedItem == 0)
						{
							Service.gI().pickItem(npc.idItem);
						}
						return;
					}
				}
				else
				{
					GameCanvas.endDlg();
				}
				return;
			}
			break;
		}
	}

	// Token: 0x060003DF RID: 991 RVA: 0x00048268 File Offset: 0x00046468
	internal static void setTouchBtn()
	{
		if (GameScr.isAnalog != 0)
		{
			GameScr.xTG = (GameScr.xF = GameCanvas.w - 45);
			if (GameScr.gamePad.isLargeGamePad)
			{
				GameScr.xSkill = GameScr.gamePad.wZone + 20;
				GameScr.wSkill = 35;
				GameScr.xHP = GameScr.xF - 45;
			}
			else if (GameScr.gamePad.isMediumGamePad)
			{
				GameScr.xHP = GameScr.xF - 45;
			}
			GameScr.yF = GameCanvas.h - 45;
			GameScr.yTG = GameScr.yF - 45;
		}
	}

	// Token: 0x060003E0 RID: 992 RVA: 0x000482F8 File Offset: 0x000464F8
	internal void updateGamePad()
	{
		if (GameScr.isAnalog == 0 || global::Char.myCharz().statusMe == 14)
		{
			return;
		}
		if (GameCanvas.isPointerHoldIn(GameScr.xF, GameScr.yF, 40, 40))
		{
			mScreen.keyTouch = 5;
			if (GameCanvas.isPointerJustRelease)
			{
				GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] = true;
				GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
			}
		}
		GameScr.gamePad.update();
		if (GameCanvas.isPointerHoldIn(GameScr.xTG, GameScr.yTG, 34, 34))
		{
			mScreen.keyTouch = 13;
			GameCanvas.isPointerJustDown = false;
			this.isPointerDowning = false;
			if (GameCanvas.isPointerClick && GameCanvas.isPointerJustRelease)
			{
				global::Char.myCharz().findNextFocusByKey();
				GameCanvas.isPointerClick = (GameCanvas.isPointerJustDown = (GameCanvas.isPointerJustRelease = false));
			}
		}
	}

	// Token: 0x060003E1 RID: 993 RVA: 0x000483C0 File Offset: 0x000465C0
	internal void paintGamePad(mGraphics g)
	{
		if (GameScr.isAnalog != 0 && global::Char.myCharz().statusMe != 14)
		{
			g.drawImage((mScreen.keyTouch != 5 && mScreen.keyMouse != 5) ? GameScr.imgFire0 : GameScr.imgFire1, GameScr.xF + 20, GameScr.yF + 20, mGraphics.HCENTER | mGraphics.VCENTER);
			GameScr.gamePad.paint(g);
			g.drawImage((mScreen.keyTouch != 13) ? GameScr.imgFocus : GameScr.imgFocus2, GameScr.xTG + 20, GameScr.yTG + 20, mGraphics.HCENTER | mGraphics.VCENTER);
		}
	}

	// Token: 0x060003E2 RID: 994 RVA: 0x00048464 File Offset: 0x00046664
	public void showWinNumber(string num, string finish)
	{
		this.winnumber = new int[num.Length];
		this.randomNumber = new int[num.Length];
		this.tMove = new int[num.Length];
		this.moveCount = new int[num.Length];
		this.delayMove = new int[num.Length];
		try
		{
			for (int i = 0; i < num.Length; i++)
			{
				this.winnumber[i] = (int)short.Parse(num[i].ToString());
				this.randomNumber[i] = Res.random(0, 11);
				this.tMove[i] = 1;
				this.delayMove[i] = 0;
			}
		}
		catch (Exception)
		{
		}
		this.tShow = 100;
		this.moveIndex = 0;
		this.strFinish = finish;
		GameScr.lastXS = (GameScr.currXS = mSystem.currentTimeMillis());
	}

	// Token: 0x060003E3 RID: 995 RVA: 0x00048550 File Offset: 0x00046750
	public void chatVip(string chatVip)
	{
		if (!this.startChat)
		{
			this.currChatWidth = mFont.tahoma_7b_yellowSmall.getWidth(chatVip);
			this.xChatVip = GameCanvas.w;
			this.startChat = true;
		}
		if (chatVip.StartsWith("!"))
		{
			chatVip = chatVip.Substring(1, chatVip.Length);
			this.isFireWorks = true;
		}
		GameScr.vChatVip.addElement(chatVip);
	}

	// Token: 0x060003E4 RID: 996 RVA: 0x000485B6 File Offset: 0x000467B6
	public void clearChatVip()
	{
		GameScr.vChatVip.removeAllElements();
		this.xChatVip = GameCanvas.w;
		this.startChat = false;
	}

	// Token: 0x060003E5 RID: 997 RVA: 0x000485D4 File Offset: 0x000467D4
	public void paintChatVip(mGraphics g)
	{
		if (GameScr.vChatVip.size() != 0 && GameScr.isPaintChatVip)
		{
			g.setClip(0, GameCanvas.h - 13, GameCanvas.w, 15);
			g.fillRect(0, GameCanvas.h - 13, GameCanvas.w, 15, 0, 90);
			string text = (string)GameScr.vChatVip.elementAt(0);
			mFont.tahoma_7b_yellow.drawString(g, text, this.xChatVip, GameCanvas.h - 13, 0, mFont.tahoma_7b_dark);
		}
	}

	// Token: 0x060003E6 RID: 998 RVA: 0x00048654 File Offset: 0x00046854
	public void updateChatVip()
	{
		if (!this.startChat)
		{
			return;
		}
		this.xChatVip -= 2;
		if (this.xChatVip < -this.currChatWidth)
		{
			this.xChatVip = GameCanvas.w;
			GameScr.vChatVip.removeElementAt(0);
			if (GameScr.vChatVip.size() == 0)
			{
				this.isFireWorks = false;
				this.startChat = false;
				return;
			}
			this.currChatWidth = mFont.tahoma_7b_white.getWidth((string)GameScr.vChatVip.elementAt(0));
		}
	}

	// Token: 0x060003E7 RID: 999 RVA: 0x000486D8 File Offset: 0x000468D8
	public void showYourNumber(string strNum)
	{
		this.yourNumber = strNum;
		this.strPaint = mFont.tahoma_7.splitFontArray(this.yourNumber, 500);
	}

	// Token: 0x060003E8 RID: 1000 RVA: 0x000486FC File Offset: 0x000468FC
	public static void checkRemoveImage()
	{
		ImgByName.checkDelHash(ImgByName.hashImagePath, 10, false);
	}

	// Token: 0x060003E9 RID: 1001 RVA: 0x0004870C File Offset: 0x0004690C
	public static void StartServerPopUp(string strMsg)
	{
		GameCanvas.endDlg();
		int num = 1139;
		ChatPopup.addBigMessage(strMsg, 100000, new Npc(-1, 0, 0, 0, 0, 0)
		{
			avatar = num
		});
		ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
		ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
		ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
	}

	// Token: 0x060003EA RID: 1002 RVA: 0x00048792 File Offset: 0x00046992
	public static bool ispaintPhubangBar()
	{
		return TileMap.mapPhuBang() && GameScr.phuban_Info.type_PB == 0;
	}

	// Token: 0x060003EB RID: 1003 RVA: 0x000487AC File Offset: 0x000469AC
	public void paintPhuBanBar(mGraphics g, int x, int y, int w)
	{
		if (GameScr.phuban_Info == null || GameScr.isPaintOther || GameScr.isPaintRada != 1 || GameCanvas.panel.isShow || !GameScr.ispaintPhubangBar())
		{
			return;
		}
		if (w < GameScr.fra_PVE_Bar_1.frameWidth + GameScr.fra_PVE_Bar_0.frameWidth * 4)
		{
			w = GameScr.fra_PVE_Bar_1.frameWidth + GameScr.fra_PVE_Bar_0.frameWidth * 4;
		}
		if (x > GameCanvas.w - w / 2)
		{
			x = GameCanvas.w - w / 2;
		}
		if (x < mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10)
		{
			x = mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10;
		}
		int frameHeight = GameScr.fra_PVE_Bar_0.frameHeight;
		int num = y + frameHeight + mGraphics.getImageHeight(GameScr.imgBall) / 2 + 2;
		int frameWidth = GameScr.fra_PVE_Bar_1.frameWidth;
		int num2 = w / 2 - frameWidth / 2;
		int num3 = x - w / 2;
		int num4 = x + frameWidth / 2;
		int num5 = y + 3;
		int num6 = num2 - GameScr.fra_PVE_Bar_0.frameWidth;
		int num7 = num6 / GameScr.fra_PVE_Bar_0.frameWidth;
		if (num6 % GameScr.fra_PVE_Bar_0.frameWidth > 0)
		{
			num7++;
		}
		for (int i = 0; i < num7; i++)
		{
			if (i < num7 - 1)
			{
				GameScr.fra_PVE_Bar_0.drawFrame(1, num3 + GameScr.fra_PVE_Bar_0.frameWidth + i * GameScr.fra_PVE_Bar_0.frameWidth, num5, 0, 0, g);
			}
			else
			{
				GameScr.fra_PVE_Bar_0.drawFrame(1, num3 + num6, num5, 0, 0, g);
			}
			if (i < num7 - 1)
			{
				GameScr.fra_PVE_Bar_0.drawFrame(1, num4 + i * GameScr.fra_PVE_Bar_0.frameWidth, num5, 0, 0, g);
			}
			else
			{
				GameScr.fra_PVE_Bar_0.drawFrame(1, num4 + num6 - GameScr.fra_PVE_Bar_0.frameWidth, num5, 0, 0, g);
			}
		}
		GameScr.fra_PVE_Bar_0.drawFrame(0, num3, num5, 2, 0, g);
		GameScr.fra_PVE_Bar_0.drawFrame(0, num4 + num6, num5, 0, 0, g);
		if (GameScr.phuban_Info.pointTeam1 > 0)
		{
			int num8 = 2;
			int num9 = 3;
			if (GameScr.phuban_Info.color_1 == 4)
			{
				num8 = 4;
				num9 = 5;
			}
			int num10 = GameScr.phuban_Info.pointTeam1 * num2 / GameScr.phuban_Info.maxPoint;
			if (num10 < 0)
			{
				num10 = 0;
			}
			if (num10 > num2)
			{
				num10 = num2;
			}
			g.setClip(num3 + num2 - num10, num5, num10, frameHeight);
			for (int j = 0; j < num7; j++)
			{
				if (j < num7 - 1)
				{
					GameScr.fra_PVE_Bar_0.drawFrame(num9, num3 + GameScr.fra_PVE_Bar_0.frameWidth + j * GameScr.fra_PVE_Bar_0.frameWidth, num5, 0, 0, g);
				}
				else
				{
					GameScr.fra_PVE_Bar_0.drawFrame(num9, num3 + num6, num5, 0, 0, g);
				}
			}
			GameScr.fra_PVE_Bar_0.drawFrame(num8, num3, num5, 2, 0, g);
			GameCanvas.resetTrans(g);
		}
		if (GameScr.phuban_Info.pointTeam2 > 0)
		{
			int num11 = 2;
			int num12 = 3;
			if (GameScr.phuban_Info.color_2 == 4)
			{
				num11 = 4;
				num12 = 5;
			}
			int num13 = GameScr.phuban_Info.pointTeam2 * num2 / GameScr.phuban_Info.maxPoint;
			if (num13 < 0)
			{
				num13 = 0;
			}
			if (num13 > num2)
			{
				num13 = num2;
			}
			g.setClip(num4, num5, num13, frameHeight);
			for (int k = 0; k < num7; k++)
			{
				if (k < num7 - 1)
				{
					GameScr.fra_PVE_Bar_0.drawFrame(num12, num4 + k * GameScr.fra_PVE_Bar_0.frameWidth, num5, 0, 0, g);
				}
				else
				{
					GameScr.fra_PVE_Bar_0.drawFrame(num12, num4 + num6 - GameScr.fra_PVE_Bar_0.frameWidth, num5, 0, 0, g);
				}
			}
			GameScr.fra_PVE_Bar_0.drawFrame(num11, num4 + num6, num5, 0, 0, g);
			GameCanvas.resetTrans(g);
		}
		GameScr.fra_PVE_Bar_1.drawFrame(0, x - frameWidth / 2, y, 0, 0, g);
		string timeCountDown = mSystem.getTimeCountDown(GameScr.phuban_Info.timeStart, (int)GameScr.phuban_Info.timeSecond, true, false);
		mFont.tahoma_7b_yellow.drawString(g, timeCountDown, x + 1, y + GameScr.fra_PVE_Bar_1.frameHeight / 2 - mFont.tahoma_7b_green2.getHeight() / 2, 2);
		Panel.setTextColor(GameScr.phuban_Info.color_1, 1).drawString(g, GameScr.phuban_Info.nameTeam1, x - 5, num + 5, 1);
		Panel.setTextColor(GameScr.phuban_Info.color_2, 1).drawString(g, GameScr.phuban_Info.nameTeam2, x + 5, num + 5, 0);
		if (GameScr.phuban_Info.type_PB != 0)
		{
			int num14 = y + frameHeight / 2 - 2;
			mFont.bigNumber_While.drawString(g, string.Empty + GameScr.phuban_Info.pointTeam1.ToString(), num3 + num2 / 2, num14, 2);
			mFont.bigNumber_While.drawString(g, string.Empty + GameScr.phuban_Info.pointTeam2.ToString(), num4 + num2 / 2, num14, 2);
		}
		g.drawImage(GameScr.imgVS, x, y + GameScr.fra_PVE_Bar_1.frameHeight + 2, 3);
		if (GameScr.phuban_Info.type_PB == 0)
		{
			GameScr.paintChienTruong_Life(g, GameScr.phuban_Info.maxLife, GameScr.phuban_Info.color_1, GameScr.phuban_Info.lifeTeam1, x - 13, GameScr.phuban_Info.color_2, GameScr.phuban_Info.lifeTeam2, x + 13, num);
		}
	}

	// Token: 0x060003EC RID: 1004 RVA: 0x00048CD8 File Offset: 0x00046ED8
	public static void paintChienTruong_Life(mGraphics g, int maxLife, int cl1, int lifeTeam1, int x1, int cl2, int lifeTeam2, int x2, int y)
	{
		if (GameScr.imgBall == null)
		{
			return;
		}
		int num = mGraphics.getImageHeight(GameScr.imgBall) / 2;
		for (int i = 0; i < maxLife; i++)
		{
			int num2 = 0;
			if (i < lifeTeam1)
			{
				num2 = 1;
			}
			g.drawRegion(GameScr.imgBall, 0, num2 * num, mGraphics.getImageWidth(GameScr.imgBall), num, 0, x1 - i * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
		}
		for (int j = 0; j < maxLife; j++)
		{
			int num3 = 0;
			if (j < lifeTeam2)
			{
				num3 = 1;
			}
			g.drawRegion(GameScr.imgBall, 0, num3 * num, mGraphics.getImageWidth(GameScr.imgBall), num, 0, x2 + j * (num + 1), y, mGraphics.VCENTER | mGraphics.HCENTER);
		}
	}

	// Token: 0x060003ED RID: 1005 RVA: 0x00048D88 File Offset: 0x00046F88
	public static void paintHPBar_NEW(mGraphics g, int x, int y, global::Char c)
	{
		g.drawImage(GameScr.imgKhung, x, y, 0);
		int num = x + 3;
		int num2 = y + 19;
		int width = GameScr.imgHP_NEW.getWidth();
		int num3 = GameScr.imgHP_NEW.getHeight() / 2;
		int num4 = c.cHP * width / c.cHPFull;
		if (num4 <= 0)
		{
			num4 = 1;
		}
		else if (num4 > width)
		{
			num4 = width;
		}
		g.drawRegion(GameScr.imgHP_NEW, 0, num3, num4, num3, 0, num, num2, 0);
		int num5 = c.cMP * width / c.cMPFull;
		if (num5 <= 0)
		{
			num5 = 1;
		}
		else if (num5 > width)
		{
			num5 = width;
		}
		g.drawRegion(GameScr.imgHP_NEW, 0, 0, num5, num3, 0, num, num2 + 6, 0);
		int num6 = x + GameScr.imgKhung.getWidth() / 2 + 1;
		int num7 = num2 + 13;
		mFont.tahoma_7_green2.drawString(g, c.cName, num6, y + 4, 2);
		if (c.mobFocus != null)
		{
			if (c.mobFocus.getTemplate() != null)
			{
				mFont.tahoma_7_green2.drawString(g, c.mobFocus.getTemplate().name, num6, num7, 2);
				return;
			}
		}
		else
		{
			if (c.npcFocus != null)
			{
				mFont.tahoma_7_green2.drawString(g, c.npcFocus.template.name, num6, num7, 2);
				return;
			}
			if (c.charFocus != null)
			{
				mFont.tahoma_7_green2.drawString(g, c.charFocus.cName, num6, num7, 2);
			}
		}
	}

	// Token: 0x060003EE RID: 1006 RVA: 0x00048EE8 File Offset: 0x000470E8
	public static void addEffectEnd(int type, int subtype, int typePaint, int x, int y, int levelPaint, int dir, short timeRemove, Point[] listObj)
	{
		GameScr.addEffect2Vector(new Effect_End(type, subtype, typePaint, x, y, levelPaint, dir, timeRemove, listObj));
	}

	// Token: 0x060003EF RID: 1007 RVA: 0x00048F0D File Offset: 0x0004710D
	public static void addEffectEnd_Target(int type, int subtype, int typePaint, global::Char charUse, Point target, int levelPaint, short timeRemove, short range)
	{
		GameScr.addEffect2Vector(new Effect_End(type, subtype, typePaint, charUse.clone(), target, levelPaint, timeRemove, range));
	}

	// Token: 0x060003F0 RID: 1008 RVA: 0x00048F2A File Offset: 0x0004712A
	public static void addEffect2Vector(Effect_End eff)
	{
		if (eff.levelPaint == 0)
		{
			EffectManager.addHiEffect(eff);
			return;
		}
		if (eff.levelPaint == 1)
		{
			EffectManager.addMidEffects(eff);
			return;
		}
		if (eff.levelPaint == 2)
		{
			EffectManager.addMid_2Effects(eff);
			return;
		}
		EffectManager.addLowEffect(eff);
	}

	// Token: 0x060003F1 RID: 1009 RVA: 0x00048F61 File Offset: 0x00047161
	public static bool setIsInScreen(int x, int y, int wOne, int hOne)
	{
		return x >= GameScr.cmx - wOne && x <= GameScr.cmx + GameCanvas.w + wOne && y >= GameScr.cmy - hOne && y <= GameScr.cmy + GameCanvas.h + hOne * 3 / 2;
	}

	// Token: 0x060003F2 RID: 1010 RVA: 0x00048F9E File Offset: 0x0004719E
	public static bool isSmallScr()
	{
		return GameCanvas.w <= 320;
	}

	// Token: 0x060003F3 RID: 1011 RVA: 0x00048FB0 File Offset: 0x000471B0
	internal void paint_xp_bar(mGraphics g)
	{
		g.setColor(8421504);
		g.fillRect(0, GameCanvas.h - 2, GameCanvas.w, 2);
		int num = (int)(global::Char.myCharz().cLevelPercent * (long)GameCanvas.w / 10000L);
		g.setColor(16777215);
		g.fillRect(0, GameCanvas.h - 2, num, 2);
		g.setColor(0);
		num = GameCanvas.w / 10;
		for (int i = 1; i < 10; i++)
		{
			g.fillRect(i * num, GameCanvas.h - 2, 1, 2);
		}
	}

	// Token: 0x060003F4 RID: 1012 RVA: 0x00049040 File Offset: 0x00047240
	internal void paint_ios_bg(mGraphics g)
	{
		if (mSystem.clientType == 5)
		{
			if (GameScr.imgBgIOS != null)
			{
				g.setColor(16777215);
				g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
				g.drawImage(GameScr.imgBgIOS, GameCanvas.w / 2, GameCanvas.h / 2, mGraphics.VCENTER | mGraphics.HCENTER);
				return;
			}
			GameScr.imgBgIOS = GameCanvas.loadImage("/bg/bg_ios_" + ((TileMap.bgID % 2 != 0) ? 1 : 2).ToString() + ".png");
		}
	}

	// Token: 0x060003F5 RID: 1013 RVA: 0x000490CC File Offset: 0x000472CC
	public void paint_CT(mGraphics g, int x, int y, int w)
	{
		w = 194;
		w = 182;
		w = 170;
		int num = 66;
		int num2 = 11;
		if (x > GameCanvas.w - w / 2)
		{
			x = GameCanvas.w - w / 2;
		}
		if (x < mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10)
		{
			x = mGraphics.getImageWidth(GameScr.imgKhung) + w / 2 + 10;
		}
		int frameHeight = GameScr.fra_PVE_Bar_0.frameHeight;
		int num3 = mGraphics.getImageHeight(GameScr.imgBall) / 2;
		int frameWidth = GameScr.fra_PVE_Bar_1.frameWidth;
		int num4 = w / 2 - frameWidth / 2;
		int num5 = x - w / 2 + 3;
		int num6 = x + frameWidth / 2;
		int num7 = y + 3;
		int num8 = num4 - GameScr.fra_PVE_Bar_0.frameWidth;
		int num9 = num8 / GameScr.fra_PVE_Bar_0.frameWidth;
		if (num8 % GameScr.fra_PVE_Bar_0.frameWidth > 0)
		{
			num9++;
		}
		for (int i = 0; i < num9; i++)
		{
			if (i < num9 - 1)
			{
				g.drawRegion(GameScr.img_ct_bar_0, 0, 15, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 2, num5 + GameScr.fra_PVE_Bar_0.frameWidth + i * GameScr.fra_PVE_Bar_0.frameWidth, num7, mGraphics.TOP | mGraphics.LEFT, true);
			}
			else
			{
				g.drawRegion(GameScr.img_ct_bar_0, 0, 15, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 2, num5 + num8, num7, mGraphics.TOP | mGraphics.LEFT, true);
			}
			if (i < num9 - 1)
			{
				g.drawRegion(GameScr.img_ct_bar_0, 0, 15, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 2, num6 + i * GameScr.fra_PVE_Bar_0.frameWidth, num7, mGraphics.TOP | mGraphics.LEFT, true);
			}
			else
			{
				g.drawRegion(GameScr.img_ct_bar_0, 0, 15, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 2, num6 + num8 - GameScr.fra_PVE_Bar_0.frameWidth, num7, mGraphics.TOP | mGraphics.LEFT, true);
			}
		}
		GameScr.fra_PVE_Bar_0.drawFrame(0, num5, num7, 2, 0, g);
		GameScr.fra_PVE_Bar_0.drawFrame(0, num6 + num8, num7, 0, 0, g);
		int num10 = GameScr.nCT_TeamA * 100 / (GameScr.nCT_nBoyBaller / 2) * num / 100;
		if (num10 > 0)
		{
			if (num10 < 6)
			{
				num10 = 6;
			}
			g.setClip(num5, num7, num10, 15);
		}
		if (GameScr.nCT_TeamA > 0)
		{
			for (int j = 0; j < num2; j++)
			{
				if (j == 0)
				{
					g.drawRegion(GameScr.img_ct_bar_0, 0, 60, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 2, num5, num7, mGraphics.TOP | mGraphics.LEFT, true);
				}
				else
				{
					g.drawRegion(GameScr.img_ct_bar_0, 0, 75, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 2, num5 + j * 6, num7, mGraphics.TOP | mGraphics.LEFT, true);
				}
			}
		}
		GameCanvas.resetTrans(g);
		int num11 = GameScr.nCT_TeamB * 100 / (GameScr.nCT_nBoyBaller / 2) * num / 100;
		if (num - (num - num11) > 0)
		{
			if (num11 < 6)
			{
				num11 = 6;
			}
			g.setClip(num6 + num - num11, num7, num - (num - num11), 15);
		}
		if (GameScr.nCT_TeamB > 0)
		{
			for (int k = 0; k < num2; k++)
			{
				if (k == 0)
				{
					g.drawRegion(GameScr.img_ct_bar_0, 0, 30, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 0, num6 + num8, num7, mGraphics.TOP | mGraphics.LEFT, true);
				}
				else
				{
					g.drawRegion(GameScr.img_ct_bar_0, 0, 45, mGraphics.getImageWidth(GameScr.img_ct_bar_0), 15, 0, num6 + num8 - k * 6, num7, mGraphics.TOP | mGraphics.LEFT, true);
				}
			}
		}
		GameCanvas.resetTrans(g);
		GameScr.fra_PVE_Bar_1.drawFrame(0, x - frameWidth / 2 + 1, y, 0, 0, g);
		string text = NinjaUtil.getTime((int)((GameScr.nCT_timeBallte - mSystem.currentTimeMillis()) / 1000L)) + string.Empty;
		mFont.tahoma_7b_yellow.drawString(g, text, num5 + w / 2 - 2, y + 5, 2);
		mFont.tahoma_7_grey.drawString(g, "Tầng " + GameScr.nCT_floor.ToString(), num5 + w / 2 - 3, y + GameScr.fra_PVE_Bar_1.frameHeight, mFont.CENTER);
		int num12 = mFont.tahoma_7b_red.getWidth(GameScr.nCT_TeamA.ToString() + string.Empty);
		mFont.tahoma_7b_blue.drawString(g, GameScr.nCT_TeamA.ToString() + string.Empty, x - frameWidth / 2 - num12, num7 + GameScr.fra_PVE_Bar_1.frameHeight, 0);
		SmallImage.drawSmallImage(g, 2325, x - frameWidth / 2 - num12 - 15, num7 + GameScr.fra_PVE_Bar_1.frameHeight, 2, mGraphics.TOP | mGraphics.LEFT);
		num12 = mFont.tahoma_7b_red.getWidth(GameScr.nCT_TeamB.ToString() + string.Empty);
		mFont.tahoma_7b_red.drawString(g, GameScr.nCT_TeamB.ToString() + string.Empty, x + frameWidth / 2, num7 + GameScr.fra_PVE_Bar_1.frameHeight, 0);
		SmallImage.drawSmallImage(g, 2323, x + frameWidth / 2 + num12 + 3, num7 + GameScr.fra_PVE_Bar_1.frameHeight, 0, mGraphics.TOP | mGraphics.LEFT);
		this.paint_board_CT(g, GameCanvas.w - mFont.tahoma_7b_dark.getWidth("#01 AAAAAAAAAA"), 40);
		GameCanvas.resetTrans(g);
	}

	// Token: 0x060003F6 RID: 1014 RVA: 0x00049600 File Offset: 0x00047800
	internal void paint_board_CT(mGraphics g, int x, int y)
	{
		if (!GameScr.is_Paint_boardCT_Expand)
		{
			int width = mFont.tahoma_7.getWidth("#01 nnnnnnnnnnnn");
			int num = GameCanvas.w - width - 20;
			for (int i = 0; i < GameScr.nTop; i++)
			{
				mFont mFont = mFont.tahoma_7_white;
				if (i == 0)
				{
					mFont = mFont.tahoma_7_red;
				}
				else if (i == 1)
				{
					mFont = mFont.tahoma_7_yellow;
				}
				else if (i == 2)
				{
					mFont = mFont.tahoma_7_blue;
				}
				if (i == GameScr.nTop - 1)
				{
					mFont = mFont.tahoma_7_green;
				}
				string[] array = Res.split((string)GameScr.res_CT.elementAt(i), "|", 0);
				int[] array2 = new int[] { 0, 18 };
				for (int j = 0; j < 2; j++)
				{
					mFont.drawString(g, array[j], num + array2[j], y + i * mFont.tahoma_7.getHeight(), 0, mFont.tahoma_7);
				}
			}
			GameCanvas.resetTrans(g);
			GameScr.xRect = num;
			GameScr.yRect = y;
			GameScr.wRect = width + 10;
			GameScr.hRect = mFont.tahoma_7b_dark.getHeight() * 6;
		}
		else
		{
			string text = "#01 namec1000000 0001   00000";
			int[] array3 = new int[] { 0, 18, 80, 101 };
			int width2 = mFont.tahoma_7.getWidth(text);
			int num2 = GameCanvas.w - width2 - 20;
			for (int k = 0; k < GameScr.nTop; k++)
			{
				string[] array4 = Res.split((string)GameScr.res_CT.elementAt(k), "|", 0);
				mFont mFont2 = mFont.tahoma_7_white;
				if (k == 0)
				{
					mFont2 = mFont.tahoma_7_red;
				}
				else if (k == 1)
				{
					mFont2 = mFont.tahoma_7_yellow;
				}
				else if (k == 2)
				{
					mFont2 = mFont.tahoma_7_blue;
				}
				if (k == GameScr.nTop - 1)
				{
					mFont2 = mFont.tahoma_7_green;
				}
				int num3 = k * mFont.tahoma_7_white.getHeight() + y;
				for (int l = 0; l < array3.Length; l++)
				{
					mFont2.drawString(g, array4[l], num2 + array3[l], num3, 0, mFont.tahoma_7);
				}
			}
			GameScr.xRect = num2;
			GameScr.yRect = y;
			GameScr.wRect = width2 + 10;
			GameScr.hRect = mFont.tahoma_7b_dark.getHeight() * 6;
		}
		GameCanvas.resetTrans(g);
	}

	// Token: 0x060003F7 RID: 1015 RVA: 0x00049830 File Offset: 0x00047A30
	internal void paintHPCT(mGraphics g, int x, int y, global::Char c)
	{
		g.drawImage(GameScr.imgKhung, x, y, 0);
		int num = x + 3;
		int num2 = y + 19;
		int width = GameScr.imgHP_NEW.getWidth();
		int num3 = GameScr.imgHP_NEW.getHeight() / 2;
		int num4 = c.cHP * width / c.cHPFull;
		if (num4 > 0)
		{
			if (num4 > width)
			{
			}
		}
		g.drawRegion(GameScr.imgHP_NEW, 0, num3, 80, num3, 0, num, num2, 0);
		int num5 = c.cMP * width / c.cMPFull;
		if (num5 > 0)
		{
			if (num5 > width)
			{
			}
		}
		g.drawRegion(GameScr.imgHP_NEW, 0, 0, 80, num3, 0, num, num2 + 6, 0);
	}

	// Token: 0x040006F1 RID: 1777
	public bool isWaitingDoubleClick;

	// Token: 0x040006F2 RID: 1778
	public long timeStartDblClick;

	// Token: 0x040006F3 RID: 1779
	public long timeEndDblClick;

	// Token: 0x040006F4 RID: 1780
	public static bool isPaintOther = false;

	// Token: 0x040006F5 RID: 1781
	public static MyVector textTime = new MyVector(string.Empty);

	// Token: 0x040006F6 RID: 1782
	public static bool isLoadAllData = false;

	// Token: 0x040006F7 RID: 1783
	public static GameScr instance;

	// Token: 0x040006F8 RID: 1784
	public static int gW;

	// Token: 0x040006F9 RID: 1785
	public static int gH;

	// Token: 0x040006FA RID: 1786
	public static int gW2;

	// Token: 0x040006FB RID: 1787
	public static int gssw;

	// Token: 0x040006FC RID: 1788
	public static int gssh;

	// Token: 0x040006FD RID: 1789
	public static int gH34;

	// Token: 0x040006FE RID: 1790
	public static int gW3;

	// Token: 0x040006FF RID: 1791
	public static int gH3;

	// Token: 0x04000700 RID: 1792
	public static int gH23;

	// Token: 0x04000701 RID: 1793
	public static int gW23;

	// Token: 0x04000702 RID: 1794
	public static int gH2;

	// Token: 0x04000703 RID: 1795
	public static int csPadMaxH;

	// Token: 0x04000704 RID: 1796
	public static int cmdBarH;

	// Token: 0x04000705 RID: 1797
	public static int gW34;

	// Token: 0x04000706 RID: 1798
	public static int gW6;

	// Token: 0x04000707 RID: 1799
	public static int gH6;

	// Token: 0x04000708 RID: 1800
	public static int cmx;

	// Token: 0x04000709 RID: 1801
	public static int cmy;

	// Token: 0x0400070A RID: 1802
	public static int cmdx;

	// Token: 0x0400070B RID: 1803
	public static int cmdy;

	// Token: 0x0400070C RID: 1804
	public static int cmvx;

	// Token: 0x0400070D RID: 1805
	public static int cmvy;

	// Token: 0x0400070E RID: 1806
	public static int cmtoX;

	// Token: 0x0400070F RID: 1807
	public static int cmtoY;

	// Token: 0x04000710 RID: 1808
	public static int cmxLim;

	// Token: 0x04000711 RID: 1809
	public static int cmyLim;

	// Token: 0x04000712 RID: 1810
	public static int gssx;

	// Token: 0x04000713 RID: 1811
	public static int gssy;

	// Token: 0x04000714 RID: 1812
	public static int gssxe;

	// Token: 0x04000715 RID: 1813
	public static int gssye;

	// Token: 0x04000716 RID: 1814
	public Command cmdback;

	// Token: 0x04000717 RID: 1815
	public Command cmdBag;

	// Token: 0x04000718 RID: 1816
	public Command cmdSkill;

	// Token: 0x04000719 RID: 1817
	public Command cmdTiemnang;

	// Token: 0x0400071A RID: 1818
	public Command cmdtrangbi;

	// Token: 0x0400071B RID: 1819
	public Command cmdInfo;

	// Token: 0x0400071C RID: 1820
	public Command cmdFocus;

	// Token: 0x0400071D RID: 1821
	public Command cmdFire;

	// Token: 0x0400071E RID: 1822
	public static int d;

	// Token: 0x0400071F RID: 1823
	public static int hpPotion;

	// Token: 0x04000720 RID: 1824
	public static SkillPaint[] sks;

	// Token: 0x04000721 RID: 1825
	public static Arrowpaint[] arrs;

	// Token: 0x04000722 RID: 1826
	public static DartInfo[] darts;

	// Token: 0x04000723 RID: 1827
	public static Part[] parts;

	// Token: 0x04000724 RID: 1828
	public static EffectCharPaint[] efs;

	// Token: 0x04000725 RID: 1829
	public static int lockTick;

	// Token: 0x04000726 RID: 1830
	internal int moveUp;

	// Token: 0x04000727 RID: 1831
	internal int moveDow;

	// Token: 0x04000728 RID: 1832
	internal int idTypeTask;

	// Token: 0x04000729 RID: 1833
	internal bool isstarOpen;

	// Token: 0x0400072A RID: 1834
	internal bool isChangeSkill;

	// Token: 0x0400072B RID: 1835
	public static MyVector vClan = new MyVector();

	// Token: 0x0400072C RID: 1836
	public static MyVector vPtMap = new MyVector();

	// Token: 0x0400072D RID: 1837
	public static MyVector vFriend = new MyVector();

	// Token: 0x0400072E RID: 1838
	public static MyVector vEnemies = new MyVector();

	// Token: 0x0400072F RID: 1839
	public static MyVector vCharInMap = new MyVector();

	// Token: 0x04000730 RID: 1840
	public static MyVector vItemMap = new MyVector();

	// Token: 0x04000731 RID: 1841
	public static MyVector vMobAttack = new MyVector();

	// Token: 0x04000732 RID: 1842
	public static MyVector vSet = new MyVector();

	// Token: 0x04000733 RID: 1843
	public static MyVector vMob = new MyVector();

	// Token: 0x04000734 RID: 1844
	public static MyVector vNpc = new MyVector();

	// Token: 0x04000735 RID: 1845
	public static MyVector vFlag = new MyVector();

	// Token: 0x04000736 RID: 1846
	public static NClass[] nClasss;

	// Token: 0x04000737 RID: 1847
	public static int indexSize = 28;

	// Token: 0x04000738 RID: 1848
	public static int indexTitle = 0;

	// Token: 0x04000739 RID: 1849
	public static int indexSelect = 0;

	// Token: 0x0400073A RID: 1850
	public static int indexRow = -1;

	// Token: 0x0400073B RID: 1851
	public static int indexRowMax;

	// Token: 0x0400073C RID: 1852
	public static int indexMenu = 0;

	// Token: 0x0400073D RID: 1853
	public Item itemFocus;

	// Token: 0x0400073E RID: 1854
	public ItemOptionTemplate[] iOptionTemplates;

	// Token: 0x0400073F RID: 1855
	public SkillOptionTemplate[] sOptionTemplates;

	// Token: 0x04000740 RID: 1856
	internal static Scroll scrInfo = new Scroll();

	// Token: 0x04000741 RID: 1857
	public static Scroll scrMain = new Scroll();

	// Token: 0x04000742 RID: 1858
	public static MyVector vItemUpGrade = new MyVector();

	// Token: 0x04000743 RID: 1859
	public static bool isTypeXu;

	// Token: 0x04000744 RID: 1860
	public static bool isViewNext;

	// Token: 0x04000745 RID: 1861
	public static bool isViewClanMemOnline = false;

	// Token: 0x04000746 RID: 1862
	public static bool isViewClanInvite = true;

	// Token: 0x04000747 RID: 1863
	public static bool isChop;

	// Token: 0x04000748 RID: 1864
	public static string titleInputText = string.Empty;

	// Token: 0x04000749 RID: 1865
	public static int tickMove;

	// Token: 0x0400074A RID: 1866
	public static bool isPaintAlert = false;

	// Token: 0x0400074B RID: 1867
	public static bool isPaintTask = false;

	// Token: 0x0400074C RID: 1868
	public static bool isPaintTeam = false;

	// Token: 0x0400074D RID: 1869
	public static bool isPaintFindTeam = false;

	// Token: 0x0400074E RID: 1870
	public static bool isPaintFriend = false;

	// Token: 0x0400074F RID: 1871
	public static bool isPaintEnemies = false;

	// Token: 0x04000750 RID: 1872
	public static bool isPaintItemInfo = false;

	// Token: 0x04000751 RID: 1873
	public static bool isHaveSelectSkill = false;

	// Token: 0x04000752 RID: 1874
	public static bool isPaintSkill = false;

	// Token: 0x04000753 RID: 1875
	public static bool isPaintInfoMe = false;

	// Token: 0x04000754 RID: 1876
	public static bool isPaintStore = false;

	// Token: 0x04000755 RID: 1877
	public static bool isPaintNonNam = false;

	// Token: 0x04000756 RID: 1878
	public static bool isPaintNonNu = false;

	// Token: 0x04000757 RID: 1879
	public static bool isPaintAoNam = false;

	// Token: 0x04000758 RID: 1880
	public static bool isPaintAoNu = false;

	// Token: 0x04000759 RID: 1881
	public static bool isPaintGangTayNam = false;

	// Token: 0x0400075A RID: 1882
	public static bool isPaintGangTayNu = false;

	// Token: 0x0400075B RID: 1883
	public static bool isPaintQuanNam = false;

	// Token: 0x0400075C RID: 1884
	public static bool isPaintQuanNu = false;

	// Token: 0x0400075D RID: 1885
	public static bool isPaintGiayNam = false;

	// Token: 0x0400075E RID: 1886
	public static bool isPaintGiayNu = false;

	// Token: 0x0400075F RID: 1887
	public static bool isPaintLien = false;

	// Token: 0x04000760 RID: 1888
	public static bool isPaintNhan = false;

	// Token: 0x04000761 RID: 1889
	public static bool isPaintNgocBoi = false;

	// Token: 0x04000762 RID: 1890
	public static bool isPaintPhu = false;

	// Token: 0x04000763 RID: 1891
	public static bool isPaintWeapon = false;

	// Token: 0x04000764 RID: 1892
	public static bool isPaintStack = false;

	// Token: 0x04000765 RID: 1893
	public static bool isPaintStackLock = false;

	// Token: 0x04000766 RID: 1894
	public static bool isPaintGrocery = false;

	// Token: 0x04000767 RID: 1895
	public static bool isPaintGroceryLock = false;

	// Token: 0x04000768 RID: 1896
	public static bool isPaintUpGrade = false;

	// Token: 0x04000769 RID: 1897
	public static bool isPaintConvert = false;

	// Token: 0x0400076A RID: 1898
	public static bool isPaintUpGradeGold = false;

	// Token: 0x0400076B RID: 1899
	public static bool isPaintUpPearl = false;

	// Token: 0x0400076C RID: 1900
	public static bool isPaintBox = false;

	// Token: 0x0400076D RID: 1901
	public static bool isPaintSplit = false;

	// Token: 0x0400076E RID: 1902
	public static bool isPaintCharInMap = false;

	// Token: 0x0400076F RID: 1903
	public static bool isPaintTrade = false;

	// Token: 0x04000770 RID: 1904
	public static bool isPaintZone = false;

	// Token: 0x04000771 RID: 1905
	public static bool isPaintMessage = false;

	// Token: 0x04000772 RID: 1906
	public static bool isPaintClan = false;

	// Token: 0x04000773 RID: 1907
	public static bool isRequestMember = false;

	// Token: 0x04000774 RID: 1908
	public static global::Char currentCharViewInfo;

	// Token: 0x04000775 RID: 1909
	public static long[] exps;

	// Token: 0x04000776 RID: 1910
	public static int[] crystals;

	// Token: 0x04000777 RID: 1911
	public static int[] upClothe;

	// Token: 0x04000778 RID: 1912
	public static int[] upAdorn;

	// Token: 0x04000779 RID: 1913
	public static int[] upWeapon;

	// Token: 0x0400077A RID: 1914
	public static int[] coinUpCrystals;

	// Token: 0x0400077B RID: 1915
	public static int[] coinUpClothes;

	// Token: 0x0400077C RID: 1916
	public static int[] coinUpAdorns;

	// Token: 0x0400077D RID: 1917
	public static int[] coinUpWeapons;

	// Token: 0x0400077E RID: 1918
	public static int[] maxPercents;

	// Token: 0x0400077F RID: 1919
	public static int[] goldUps;

	// Token: 0x04000780 RID: 1920
	public int tMenuDelay;

	// Token: 0x04000781 RID: 1921
	public int zoneCol = 6;

	// Token: 0x04000782 RID: 1922
	public int[] zones;

	// Token: 0x04000783 RID: 1923
	public int[] pts;

	// Token: 0x04000784 RID: 1924
	public int[] numPlayer;

	// Token: 0x04000785 RID: 1925
	public int[] maxPlayer;

	// Token: 0x04000786 RID: 1926
	public int[] rank1;

	// Token: 0x04000787 RID: 1927
	public int[] rank2;

	// Token: 0x04000788 RID: 1928
	public string[] rankName1;

	// Token: 0x04000789 RID: 1929
	public string[] rankName2;

	// Token: 0x0400078A RID: 1930
	public int typeTrade;

	// Token: 0x0400078B RID: 1931
	public int typeTradeOrder;

	// Token: 0x0400078C RID: 1932
	public int coinTrade;

	// Token: 0x0400078D RID: 1933
	public int coinTradeOrder;

	// Token: 0x0400078E RID: 1934
	public int timeTrade;

	// Token: 0x0400078F RID: 1935
	public int indexItemUse = -1;

	// Token: 0x04000790 RID: 1936
	public int cLastFocusID = -1;

	// Token: 0x04000791 RID: 1937
	public int cPreFocusID = -1;

	// Token: 0x04000792 RID: 1938
	public bool isLockKey;

	// Token: 0x04000793 RID: 1939
	public static int[] tasks;

	// Token: 0x04000794 RID: 1940
	public static int[] mapTasks;

	// Token: 0x04000795 RID: 1941
	public static Image imgRoomStat;

	// Token: 0x04000796 RID: 1942
	public static Image frBarPow0;

	// Token: 0x04000797 RID: 1943
	public static Image frBarPow1;

	// Token: 0x04000798 RID: 1944
	public static Image frBarPow2;

	// Token: 0x04000799 RID: 1945
	public static Image frBarPow20;

	// Token: 0x0400079A RID: 1946
	public static Image frBarPow21;

	// Token: 0x0400079B RID: 1947
	public static Image frBarPow22;

	// Token: 0x0400079C RID: 1948
	public MyVector texts;

	// Token: 0x0400079D RID: 1949
	public string textsTitle;

	// Token: 0x0400079E RID: 1950
	public static sbyte vcData;

	// Token: 0x0400079F RID: 1951
	public static sbyte vcMap;

	// Token: 0x040007A0 RID: 1952
	public static sbyte vcSkill;

	// Token: 0x040007A1 RID: 1953
	public static sbyte vcItem;

	// Token: 0x040007A2 RID: 1954
	public static sbyte vsData;

	// Token: 0x040007A3 RID: 1955
	public static sbyte vsMap;

	// Token: 0x040007A4 RID: 1956
	public static sbyte vsSkill;

	// Token: 0x040007A5 RID: 1957
	public static sbyte vsItem;

	// Token: 0x040007A6 RID: 1958
	public static sbyte vcTask;

	// Token: 0x040007A7 RID: 1959
	public static Image imgArrow;

	// Token: 0x040007A8 RID: 1960
	public static Image imgArrow2;

	// Token: 0x040007A9 RID: 1961
	public static Image imgChat;

	// Token: 0x040007AA RID: 1962
	public static Image imgChat2;

	// Token: 0x040007AB RID: 1963
	public static Image imgMenu;

	// Token: 0x040007AC RID: 1964
	public static Image imgFocus;

	// Token: 0x040007AD RID: 1965
	public static Image imgFocus2;

	// Token: 0x040007AE RID: 1966
	public static Image imgSkill;

	// Token: 0x040007AF RID: 1967
	public static Image imgSkill2;

	// Token: 0x040007B0 RID: 1968
	public static Image imgHP1;

	// Token: 0x040007B1 RID: 1969
	public static Image imgHP2;

	// Token: 0x040007B2 RID: 1970
	public static Image imgHP3;

	// Token: 0x040007B3 RID: 1971
	public static Image imgHP4;

	// Token: 0x040007B4 RID: 1972
	public static Image imgFire0;

	// Token: 0x040007B5 RID: 1973
	public static Image imgFire1;

	// Token: 0x040007B6 RID: 1974
	public static Image imgNR1;

	// Token: 0x040007B7 RID: 1975
	public static Image imgNR2;

	// Token: 0x040007B8 RID: 1976
	public static Image imgNR3;

	// Token: 0x040007B9 RID: 1977
	public static Image imgNR4;

	// Token: 0x040007BA RID: 1978
	public static Image imgLbtn;

	// Token: 0x040007BB RID: 1979
	public static Image imgLbtnFocus;

	// Token: 0x040007BC RID: 1980
	public static Image imgLbtn2;

	// Token: 0x040007BD RID: 1981
	public static Image imgLbtnFocus2;

	// Token: 0x040007BE RID: 1982
	public static Image imgAnalog1;

	// Token: 0x040007BF RID: 1983
	public static Image imgAnalog2;

	// Token: 0x040007C0 RID: 1984
	public string tradeName = string.Empty;

	// Token: 0x040007C1 RID: 1985
	public string tradeItemName = string.Empty;

	// Token: 0x040007C2 RID: 1986
	public int timeLengthMap;

	// Token: 0x040007C3 RID: 1987
	public int timeStartMap;

	// Token: 0x040007C4 RID: 1988
	public static sbyte typeViewInfo = 0;

	// Token: 0x040007C5 RID: 1989
	public static sbyte typeActive = 0;

	// Token: 0x040007C6 RID: 1990
	public static InfoMe info1 = new InfoMe();

	// Token: 0x040007C7 RID: 1991
	public static InfoMe info2 = new InfoMe();

	// Token: 0x040007C8 RID: 1992
	public static Image imgPanel;

	// Token: 0x040007C9 RID: 1993
	public static Image imgPanel2;

	// Token: 0x040007CA RID: 1994
	public static Image imgHP;

	// Token: 0x040007CB RID: 1995
	public static Image imgMP;

	// Token: 0x040007CC RID: 1996
	public static Image imgSP;

	// Token: 0x040007CD RID: 1997
	public static Image imgHPLost;

	// Token: 0x040007CE RID: 1998
	public static Image imgMPLost;

	// Token: 0x040007CF RID: 1999
	public static Image imgHP_tm_do;

	// Token: 0x040007D0 RID: 2000
	public static Image imgHP_tm_vang;

	// Token: 0x040007D1 RID: 2001
	public static Image imgHP_tm_xam;

	// Token: 0x040007D2 RID: 2002
	public static Image imgHP_tm_xanh;

	// Token: 0x040007D3 RID: 2003
	public Mob mobCapcha;

	// Token: 0x040007D4 RID: 2004
	public MagicTree magicTree;

	// Token: 0x040007D5 RID: 2005
	internal short l;

	// Token: 0x040007D6 RID: 2006
	public static int countEff;

	// Token: 0x040007D7 RID: 2007
	public static GamePad gamePad = new GamePad();

	// Token: 0x040007D8 RID: 2008
	public static Image imgChatPC;

	// Token: 0x040007D9 RID: 2009
	public static Image imgChatsPC2;

	// Token: 0x040007DA RID: 2010
	public static int isAnalog = 0;

	// Token: 0x040007DB RID: 2011
	public static Image img_ct_bar_0 = mSystem.loadImage("/mainImage/i_pve_bar_0.png");

	// Token: 0x040007DC RID: 2012
	public static Image img_ct_bar_1 = mSystem.loadImage("/mainImage/i_pve_bar_1.png");

	// Token: 0x040007DD RID: 2013
	public static bool isUseTouch;

	// Token: 0x040007DE RID: 2014
	public Command cmdDoiCo;

	// Token: 0x040007DF RID: 2015
	public Command cmdLogOut;

	// Token: 0x040007E0 RID: 2016
	public Command cmdChatTheGioi;

	// Token: 0x040007E1 RID: 2017
	public Command cmdshowInfo;

	// Token: 0x040007E2 RID: 2018
	internal static Command[] cmdTestLogin = null;

	// Token: 0x040007E3 RID: 2019
	public const int numSkill = 10;

	// Token: 0x040007E4 RID: 2020
	public const int numSkill_2 = 5;

	// Token: 0x040007E5 RID: 2021
	public static Skill[] keySkill = new Skill[10];

	// Token: 0x040007E6 RID: 2022
	public static Skill[] onScreenSkill = new Skill[10];

	// Token: 0x040007E7 RID: 2023
	public Command cmdMenu;

	// Token: 0x040007E8 RID: 2024
	public static int firstY;

	// Token: 0x040007E9 RID: 2025
	public static int wSkill;

	// Token: 0x040007EA RID: 2026
	public static long deltaTime;

	// Token: 0x040007EB RID: 2027
	public bool isPointerDowning;

	// Token: 0x040007EC RID: 2028
	public bool isChangingCameraMode;

	// Token: 0x040007ED RID: 2029
	internal int ptLastDownX;

	// Token: 0x040007EE RID: 2030
	internal int ptLastDownY;

	// Token: 0x040007EF RID: 2031
	internal int ptFirstDownX;

	// Token: 0x040007F0 RID: 2032
	internal int ptFirstDownY;

	// Token: 0x040007F1 RID: 2033
	internal int ptDownTime;

	// Token: 0x040007F2 RID: 2034
	internal bool disableSingleClick;

	// Token: 0x040007F3 RID: 2035
	public long lastSingleClick;

	// Token: 0x040007F4 RID: 2036
	public bool clickMoving;

	// Token: 0x040007F5 RID: 2037
	public bool clickOnTileTop;

	// Token: 0x040007F6 RID: 2038
	public bool clickMovingRed;

	// Token: 0x040007F7 RID: 2039
	internal int clickToX;

	// Token: 0x040007F8 RID: 2040
	internal int clickToY;

	// Token: 0x040007F9 RID: 2041
	internal int lastClickCMX;

	// Token: 0x040007FA RID: 2042
	internal int lastClickCMY;

	// Token: 0x040007FB RID: 2043
	internal int clickMovingP1;

	// Token: 0x040007FC RID: 2044
	internal int clickMovingTimeOut;

	// Token: 0x040007FD RID: 2045
	internal long lastMove;

	// Token: 0x040007FE RID: 2046
	public static bool isNewClanMessage;

	// Token: 0x040007FF RID: 2047
	internal long lastFire;

	// Token: 0x04000800 RID: 2048
	internal long lastUsePotion;

	// Token: 0x04000801 RID: 2049
	public int auto;

	// Token: 0x04000802 RID: 2050
	public int dem;

	// Token: 0x04000803 RID: 2051
	internal string strTam = string.Empty;

	// Token: 0x04000804 RID: 2052
	internal int a;

	// Token: 0x04000805 RID: 2053
	public bool isFreez;

	// Token: 0x04000806 RID: 2054
	public bool isUseFreez;

	// Token: 0x04000807 RID: 2055
	public static Image imgTrans;

	// Token: 0x04000808 RID: 2056
	public bool isRongThanXuatHien;

	// Token: 0x04000809 RID: 2057
	public bool isRongNamek;

	// Token: 0x0400080A RID: 2058
	public bool isSuperPower;

	// Token: 0x0400080B RID: 2059
	public int tPower;

	// Token: 0x0400080C RID: 2060
	public int xPower;

	// Token: 0x0400080D RID: 2061
	public int yPower;

	// Token: 0x0400080E RID: 2062
	public int dxPower;

	// Token: 0x0400080F RID: 2063
	public bool activeRongThan;

	// Token: 0x04000810 RID: 2064
	public bool isMeCallRongThan;

	// Token: 0x04000811 RID: 2065
	public int mautroi;

	// Token: 0x04000812 RID: 2066
	public int mapRID;

	// Token: 0x04000813 RID: 2067
	public int zoneRID;

	// Token: 0x04000814 RID: 2068
	public int bgRID = -1;

	// Token: 0x04000815 RID: 2069
	public static int tam = 0;

	// Token: 0x04000816 RID: 2070
	public static bool isAutoPlay;

	// Token: 0x04000817 RID: 2071
	public static bool canAutoPlay;

	// Token: 0x04000818 RID: 2072
	public static bool isChangeZone;

	// Token: 0x04000819 RID: 2073
	internal int timeSkill;

	// Token: 0x0400081A RID: 2074
	internal int nSkill;

	// Token: 0x0400081B RID: 2075
	internal int selectedIndexSkill = -1;

	// Token: 0x0400081C RID: 2076
	internal Skill lastSkill;

	// Token: 0x0400081D RID: 2077
	internal bool doSeleckSkillFlag;

	// Token: 0x0400081E RID: 2078
	public string strCapcha;

	// Token: 0x0400081F RID: 2079
	internal long longPress;

	// Token: 0x04000820 RID: 2080
	internal int move;

	// Token: 0x04000821 RID: 2081
	public bool flareFindFocus;

	// Token: 0x04000822 RID: 2082
	internal int flareTime;

	// Token: 0x04000823 RID: 2083
	public int keyTouchSkill = -1;

	// Token: 0x04000824 RID: 2084
	internal long lastSendUpdatePostion;

	// Token: 0x04000825 RID: 2085
	public static long lastTick;

	// Token: 0x04000826 RID: 2086
	public static long currTick;

	// Token: 0x04000827 RID: 2087
	internal int timeAuto;

	// Token: 0x04000828 RID: 2088
	public static long lastXS;

	// Token: 0x04000829 RID: 2089
	public static long currXS;

	// Token: 0x0400082A RID: 2090
	public static int secondXS;

	// Token: 0x0400082B RID: 2091
	public int runArrow;

	// Token: 0x0400082C RID: 2092
	public static int isPaintRada;

	// Token: 0x0400082D RID: 2093
	public static Image imgNut;

	// Token: 0x0400082E RID: 2094
	public static Image imgNutF;

	// Token: 0x0400082F RID: 2095
	public int[] keyCapcha;

	// Token: 0x04000830 RID: 2096
	public static Image imgCapcha;

	// Token: 0x04000831 RID: 2097
	public string keyInput;

	// Token: 0x04000832 RID: 2098
	public static int disXC;

	// Token: 0x04000833 RID: 2099
	public static bool isPaint = true;

	// Token: 0x04000834 RID: 2100
	public static int shock_scr;

	// Token: 0x04000835 RID: 2101
	internal static int[] shock_x = new int[] { 1, -1, 1, -1 };

	// Token: 0x04000836 RID: 2102
	internal static int[] shock_y = new int[] { 1, -1, -1, 1 };

	// Token: 0x04000837 RID: 2103
	internal int tDoubleDelay;

	// Token: 0x04000838 RID: 2104
	public static Image arrow;

	// Token: 0x04000839 RID: 2105
	internal static int yTouchBar;

	// Token: 0x0400083A RID: 2106
	internal static int xC;

	// Token: 0x0400083B RID: 2107
	internal static int yC;

	// Token: 0x0400083C RID: 2108
	internal static int xL;

	// Token: 0x0400083D RID: 2109
	internal static int yL;

	// Token: 0x0400083E RID: 2110
	public int xR;

	// Token: 0x0400083F RID: 2111
	public int yR;

	// Token: 0x04000840 RID: 2112
	internal static int xU;

	// Token: 0x04000841 RID: 2113
	internal static int yU;

	// Token: 0x04000842 RID: 2114
	internal static int xF;

	// Token: 0x04000843 RID: 2115
	internal static int yF;

	// Token: 0x04000844 RID: 2116
	public static int xHP;

	// Token: 0x04000845 RID: 2117
	public static int yHP;

	// Token: 0x04000846 RID: 2118
	internal static int xTG;

	// Token: 0x04000847 RID: 2119
	internal static int yTG;

	// Token: 0x04000848 RID: 2120
	public static int[] xS;

	// Token: 0x04000849 RID: 2121
	public static int[] yS;

	// Token: 0x0400084A RID: 2122
	public static int xSkill;

	// Token: 0x0400084B RID: 2123
	public static int ySkill;

	// Token: 0x0400084C RID: 2124
	public static int padSkill;

	// Token: 0x0400084D RID: 2125
	public int dMP;

	// Token: 0x0400084E RID: 2126
	public int twMp;

	// Token: 0x0400084F RID: 2127
	public bool isInjureMp;

	// Token: 0x04000850 RID: 2128
	public int dHP;

	// Token: 0x04000851 RID: 2129
	public int twHp;

	// Token: 0x04000852 RID: 2130
	public bool isInjureHp;

	// Token: 0x04000853 RID: 2131
	internal long curr;

	// Token: 0x04000854 RID: 2132
	internal long last;

	// Token: 0x04000855 RID: 2133
	internal int secondVS;

	// Token: 0x04000856 RID: 2134
	internal int[] idVS = new int[] { -1, -1 };

	// Token: 0x04000857 RID: 2135
	public static string[] flyTextString;

	// Token: 0x04000858 RID: 2136
	public static int[] flyTextX;

	// Token: 0x04000859 RID: 2137
	public static int[] flyTextY;

	// Token: 0x0400085A RID: 2138
	public static int[] flyTextYTo;

	// Token: 0x0400085B RID: 2139
	public static int[] flyTextDx;

	// Token: 0x0400085C RID: 2140
	public static int[] flyTextDy;

	// Token: 0x0400085D RID: 2141
	public static int[] flyTextState;

	// Token: 0x0400085E RID: 2142
	public static int[] flyTextColor;

	// Token: 0x0400085F RID: 2143
	public static int[] flyTime;

	// Token: 0x04000860 RID: 2144
	public static int[] splashX;

	// Token: 0x04000861 RID: 2145
	public static int[] splashY;

	// Token: 0x04000862 RID: 2146
	public static int[] splashState;

	// Token: 0x04000863 RID: 2147
	public static int[] splashF;

	// Token: 0x04000864 RID: 2148
	public static int[] splashDir;

	// Token: 0x04000865 RID: 2149
	public static Image[] imgSplash;

	// Token: 0x04000866 RID: 2150
	public static int cmdBarX;

	// Token: 0x04000867 RID: 2151
	public static int cmdBarY;

	// Token: 0x04000868 RID: 2152
	public static int cmdBarW;

	// Token: 0x04000869 RID: 2153
	public static int cmdBarLeftW;

	// Token: 0x0400086A RID: 2154
	public static int cmdBarRightW;

	// Token: 0x0400086B RID: 2155
	public static int cmdBarCenterW;

	// Token: 0x0400086C RID: 2156
	public static int hpBarX;

	// Token: 0x0400086D RID: 2157
	public static int hpBarY;

	// Token: 0x0400086E RID: 2158
	public static int spBarW;

	// Token: 0x0400086F RID: 2159
	public static int mpBarW;

	// Token: 0x04000870 RID: 2160
	public static int expBarW;

	// Token: 0x04000871 RID: 2161
	public static int lvPosX;

	// Token: 0x04000872 RID: 2162
	public static int moneyPosX;

	// Token: 0x04000873 RID: 2163
	public static int hpBarH;

	// Token: 0x04000874 RID: 2164
	public static int girlHPBarY;

	// Token: 0x04000875 RID: 2165
	public static long hpBarW;

	// Token: 0x04000876 RID: 2166
	public static Image[] imgCmdBar;

	// Token: 0x04000877 RID: 2167
	internal int imgScrW;

	// Token: 0x04000878 RID: 2168
	public static int popupY;

	// Token: 0x04000879 RID: 2169
	public static int popupX;

	// Token: 0x0400087A RID: 2170
	public static int isborderIndex;

	// Token: 0x0400087B RID: 2171
	public static int isselectedRow;

	// Token: 0x0400087C RID: 2172
	internal static Image imgNolearn;

	// Token: 0x0400087D RID: 2173
	public int cmxp;

	// Token: 0x0400087E RID: 2174
	public int cmvxp;

	// Token: 0x0400087F RID: 2175
	public int cmdxp;

	// Token: 0x04000880 RID: 2176
	public int cmxLimp;

	// Token: 0x04000881 RID: 2177
	public int cmyLimp;

	// Token: 0x04000882 RID: 2178
	public int cmyp;

	// Token: 0x04000883 RID: 2179
	public int cmvyp;

	// Token: 0x04000884 RID: 2180
	public int cmdyp;

	// Token: 0x04000885 RID: 2181
	internal int indexTiemNang;

	// Token: 0x04000886 RID: 2182
	internal string alertURL;

	// Token: 0x04000887 RID: 2183
	internal string fnick;

	// Token: 0x04000888 RID: 2184
	public static int xstart;

	// Token: 0x04000889 RID: 2185
	public static int ystart;

	// Token: 0x0400088A RID: 2186
	public static int popupW = 140;

	// Token: 0x0400088B RID: 2187
	public static int popupH = 160;

	// Token: 0x0400088C RID: 2188
	public static int cmySK;

	// Token: 0x0400088D RID: 2189
	public static int cmtoYSK;

	// Token: 0x0400088E RID: 2190
	public static int cmdySK;

	// Token: 0x0400088F RID: 2191
	public static int cmvySK;

	// Token: 0x04000890 RID: 2192
	public static int cmyLimSK;

	// Token: 0x04000891 RID: 2193
	public static int columns = 6;

	// Token: 0x04000892 RID: 2194
	public static int rows;

	// Token: 0x04000893 RID: 2195
	internal int totalRowInfo;

	// Token: 0x04000894 RID: 2196
	internal int ypaintKill;

	// Token: 0x04000895 RID: 2197
	internal int ylimUp;

	// Token: 0x04000896 RID: 2198
	internal int ylimDow;

	// Token: 0x04000897 RID: 2199
	internal int yPaint;

	// Token: 0x04000898 RID: 2200
	public static int indexEff = 0;

	// Token: 0x04000899 RID: 2201
	public static EffectCharPaint effUpok;

	// Token: 0x0400089A RID: 2202
	public static int inforX;

	// Token: 0x0400089B RID: 2203
	public static int inforY;

	// Token: 0x0400089C RID: 2204
	public static int inforW;

	// Token: 0x0400089D RID: 2205
	public static int inforH;

	// Token: 0x0400089E RID: 2206
	public Command cmdDead;

	// Token: 0x0400089F RID: 2207
	public static bool notPaint = false;

	// Token: 0x040008A0 RID: 2208
	public static bool isPing = false;

	// Token: 0x040008A1 RID: 2209
	public static int INFO = 0;

	// Token: 0x040008A2 RID: 2210
	public static int STORE = 1;

	// Token: 0x040008A3 RID: 2211
	public static int ZONE = 2;

	// Token: 0x040008A4 RID: 2212
	public static int UPGRADE = 3;

	// Token: 0x040008A5 RID: 2213
	internal int Hitem = 30;

	// Token: 0x040008A6 RID: 2214
	internal int maxSizeRow = 5;

	// Token: 0x040008A7 RID: 2215
	internal int isTranKyNang;

	// Token: 0x040008A8 RID: 2216
	internal bool isTran;

	// Token: 0x040008A9 RID: 2217
	internal int cmY_Old;

	// Token: 0x040008AA RID: 2218
	internal int cmX_Old;

	// Token: 0x040008AB RID: 2219
	public PopUpYesNo popUpYesNo;

	// Token: 0x040008AC RID: 2220
	public static MyVector vChatVip = new MyVector();

	// Token: 0x040008AD RID: 2221
	public static int vBig;

	// Token: 0x040008AE RID: 2222
	public bool isFireWorks;

	// Token: 0x040008AF RID: 2223
	public int[] winnumber;

	// Token: 0x040008B0 RID: 2224
	public int[] randomNumber;

	// Token: 0x040008B1 RID: 2225
	public int[] tMove;

	// Token: 0x040008B2 RID: 2226
	public int[] moveCount;

	// Token: 0x040008B3 RID: 2227
	public int[] delayMove;

	// Token: 0x040008B4 RID: 2228
	public int moveIndex;

	// Token: 0x040008B5 RID: 2229
	internal bool isWin;

	// Token: 0x040008B6 RID: 2230
	internal string strFinish;

	// Token: 0x040008B7 RID: 2231
	internal int tShow;

	// Token: 0x040008B8 RID: 2232
	internal int xChatVip;

	// Token: 0x040008B9 RID: 2233
	internal int currChatWidth;

	// Token: 0x040008BA RID: 2234
	internal bool startChat;

	// Token: 0x040008BB RID: 2235
	public sbyte percentMabu;

	// Token: 0x040008BC RID: 2236
	public bool mabuEff;

	// Token: 0x040008BD RID: 2237
	public int tMabuEff;

	// Token: 0x040008BE RID: 2238
	public static bool isPaintChatVip;

	// Token: 0x040008BF RID: 2239
	public static sbyte mabuPercent;

	// Token: 0x040008C0 RID: 2240
	public static sbyte isNewMember;

	// Token: 0x040008C1 RID: 2241
	internal string yourNumber = string.Empty;

	// Token: 0x040008C2 RID: 2242
	internal string[] strPaint;

	// Token: 0x040008C3 RID: 2243
	public static Image imgHP_NEW;

	// Token: 0x040008C4 RID: 2244
	public static InfoPhuBan phuban_Info;

	// Token: 0x040008C5 RID: 2245
	public static FrameImage fra_PVE_Bar_0;

	// Token: 0x040008C6 RID: 2246
	public static FrameImage fra_PVE_Bar_1;

	// Token: 0x040008C7 RID: 2247
	public static Image imgVS;

	// Token: 0x040008C8 RID: 2248
	public static Image imgBall;

	// Token: 0x040008C9 RID: 2249
	public static Image imgKhung;

	// Token: 0x040008CA RID: 2250
	public int countFrameSkill;

	// Token: 0x040008CB RID: 2251
	public static Image imgBgIOS;

	// Token: 0x040008CC RID: 2252
	public static int nCT_TeamB = 50;

	// Token: 0x040008CD RID: 2253
	public static int nCT_TeamA = 50;

	// Token: 0x040008CE RID: 2254
	public static long nCT_timeBallte;

	// Token: 0x040008CF RID: 2255
	public static string nCT_team;

	// Token: 0x040008D0 RID: 2256
	public static int nCT_nBoyBaller = 100;

	// Token: 0x040008D1 RID: 2257
	public static bool isPaint_CT;

	// Token: 0x040008D2 RID: 2258
	public static sbyte nCT_floor;

	// Token: 0x040008D3 RID: 2259
	public static bool is_Paint_boardCT_Expand;

	// Token: 0x040008D4 RID: 2260
	internal static int xRect;

	// Token: 0x040008D5 RID: 2261
	internal static int yRect;

	// Token: 0x040008D6 RID: 2262
	internal static int wRect;

	// Token: 0x040008D7 RID: 2263
	internal static int hRect;

	// Token: 0x040008D8 RID: 2264
	public static MyVector res_CT = new MyVector();

	// Token: 0x040008D9 RID: 2265
	public static int nTop = 1;

	// Token: 0x040008DA RID: 2266
	public static bool isPickNgocRong = false;

	// Token: 0x040008DB RID: 2267
	public static int nUSER_CT;

	// Token: 0x040008DC RID: 2268
	public static int nUSER_MAX_CT;

	// Token: 0x040008DD RID: 2269
	public static bool isudungCapsun;

	// Token: 0x040008DE RID: 2270
	public static bool isudungCapsun4;

	// Token: 0x040008DF RID: 2271
	public static bool isudungCapsun3;
}
