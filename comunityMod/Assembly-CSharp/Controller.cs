using System;
using System.Collections;
using Assets.src.e;
using Assets.src.f;
using Assets.src.g;
using UnityEngine;

// Token: 0x02000021 RID: 33
public class Controller : IMessageHandler
{
	// Token: 0x060001DC RID: 476 RVA: 0x0001AC2C File Offset: 0x00018E2C
	public static Controller gI()
	{
		if (Controller.me == null)
		{
			Controller.me = new Controller();
		}
		return Controller.me;
	}

	// Token: 0x060001DD RID: 477 RVA: 0x0001AC44 File Offset: 0x00018E44
	public static Controller gI2()
	{
		if (Controller.me2 == null)
		{
			Controller.me2 = new Controller();
		}
		return Controller.me2;
	}

	// Token: 0x060001DE RID: 478 RVA: 0x0001AC5C File Offset: 0x00018E5C
	public void onConnectOK(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onConnectOK();
	}

	// Token: 0x060001DF RID: 479 RVA: 0x0001AC69 File Offset: 0x00018E69
	public void onConnectionFail(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onConnectionFail();
	}

	// Token: 0x060001E0 RID: 480 RVA: 0x0001AC76 File Offset: 0x00018E76
	public void onDisconnected(bool isMain1)
	{
		Controller.isMain = isMain1;
		mSystem.onDisconnected();
	}

	// Token: 0x060001E1 RID: 481 RVA: 0x0001AC84 File Offset: 0x00018E84
	public void requestItemPlayer(Message msg)
	{
		try
		{
			int num = (int)msg.reader().readUnsignedByte();
			Item item = GameScr.currentCharViewInfo.arrItemBody[num];
			item.saleCoinLock = msg.reader().readInt();
			item.sys = (int)msg.reader().readByte();
			item.options = new MyVector();
			try
			{
				for (;;)
				{
					item.options.addElement(new ItemOption((int)msg.reader().readUnsignedByte(), (int)msg.reader().readUnsignedShort()));
				}
			}
			catch (Exception ex)
			{
				Cout.println("Loi tairequestItemPlayer 1" + ex.ToString());
			}
		}
		catch (Exception ex2)
		{
			Cout.println("Loi tairequestItemPlayer 2" + ex2.ToString());
		}
	}

	// Token: 0x060001E2 RID: 482 RVA: 0x0001AD50 File Offset: 0x00018F50
	public void onMessage(Message msg)
	{
		GameCanvas.debugSession.removeAllElements();
		GameCanvas.debug("SA1", 2);
		try
		{
			if (msg.command != -74)
			{
				Res.outz("=========> [READ] cmd= " + msg.command.ToString());
			}
			global::Char @char = null;
			MyVector myVector = new MyVector();
			int i = 0;
			GameCanvas.timeLoading = 15;
			Controller2.readMessage(msg);
			sbyte command = msg.command;
			switch (command)
			{
			case -99:
				InfoDlg.hide();
				if (msg.reader().readByte() == 0)
				{
					GameCanvas.panel.vEnemy.removeAllElements();
					int num = (int)msg.reader().readUnsignedByte();
					for (int j = 0; j < num; j++)
					{
						global::Char char2 = new global::Char();
						char2.charID = msg.reader().readInt();
						char2.head = (int)msg.reader().readShort();
						char2.headICON = (int)msg.reader().readShort();
						char2.body = (int)msg.reader().readShort();
						char2.leg = (int)msg.reader().readShort();
						char2.bag = (int)msg.reader().readShort();
						char2.cName = msg.reader().readUTF();
						InfoItem infoItem = new InfoItem(msg.reader().readUTF());
						bool flag = msg.reader().readBoolean();
						infoItem.charInfo = char2;
						infoItem.isOnline = flag;
						Res.outz("isonline = " + flag.ToString());
						GameCanvas.panel.vEnemy.addElement(infoItem);
					}
					GameCanvas.panel.setTypeEnemy();
					GameCanvas.panel.show();
					goto IL_8EFE;
				}
				goto IL_8EFE;
			case -98:
			{
				bool flag2 = msg.reader().readByte() != 0;
				GameCanvas.menu.showMenu = false;
				if (!flag2)
				{
					GameCanvas.startYesNoDlg(msg.reader().readUTF(), new Command(mResources.YES, GameCanvas.instance, 888397, msg.reader().readUTF()), new Command(mResources.NO, GameCanvas.instance, 888396, null));
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -97:
				global::Char.myCharz().cNangdong = (long)msg.reader().readInt();
				goto IL_8EFE;
			case -96:
			{
				sbyte b = msg.reader().readByte();
				GameCanvas.panel.vTop.removeAllElements();
				string text = msg.reader().readUTF();
				sbyte b2 = msg.reader().readByte();
				for (int k = 0; k < (int)b2; k++)
				{
					int num2 = msg.reader().readInt();
					int num3 = msg.reader().readInt();
					short num4 = msg.reader().readShort();
					short num5 = msg.reader().readShort();
					short num6 = msg.reader().readShort();
					short num7 = msg.reader().readShort();
					string text2 = msg.reader().readUTF();
					string text3 = msg.reader().readUTF();
					TopInfo topInfo = new TopInfo();
					topInfo.rank = num2;
					topInfo.headID = (int)num4;
					topInfo.headICON = (int)num5;
					topInfo.body = num6;
					topInfo.leg = num7;
					topInfo.name = text2;
					topInfo.info = text3;
					topInfo.info2 = msg.reader().readUTF();
					topInfo.pId = num3;
					GameCanvas.panel.vTop.addElement(topInfo);
				}
				GameCanvas.panel.topName = text;
				GameCanvas.panel.setTypeTop(b);
				GameCanvas.panel.show();
				goto IL_8EFE;
			}
			case -95:
			{
				sbyte b3 = msg.reader().readByte();
				Res.outz("type= " + b3.ToString());
				if (b3 == 0)
				{
					int num8 = msg.reader().readInt();
					short num9 = msg.reader().readShort();
					int num10 = msg.readInt3Byte();
					SoundMn.gI().explode_1();
					if (num8 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().mobMe = new Mob(num8, false, false, false, false, false, (int)num9, 1, num10, 0, num10, (short)(global::Char.myCharz().cx + ((global::Char.myCharz().cdir != 1) ? (-40) : 40)), (short)global::Char.myCharz().cy, 4, 0);
						global::Char.myCharz().mobMe.isMobMe = true;
						EffecMn.addEff(new Effect(18, global::Char.myCharz().mobMe.x, global::Char.myCharz().mobMe.y, 2, 10, -1));
						global::Char.myCharz().tMobMeBorn = 30;
						GameScr.vMob.addElement(global::Char.myCharz().mobMe);
					}
					else
					{
						@char = GameScr.findCharInMap(num8);
						if (@char != null)
						{
							@char.mobMe = new Mob(num8, false, false, false, false, false, (int)num9, 1, num10, 0, num10, (short)@char.cx, (short)@char.cy, 4, 0)
							{
								isMobMe = true
							};
							GameScr.vMob.addElement(@char.mobMe);
						}
						else if (GameScr.findMobInMap(num8) == null)
						{
							Mob mob = new Mob(num8, false, false, false, false, false, (int)num9, 1, num10, 0, num10, -100, -100, 4, 0);
							mob.isMobMe = true;
							GameScr.vMob.addElement(mob);
						}
					}
				}
				if (b3 == 1)
				{
					int num11 = msg.reader().readInt();
					int num12 = (int)msg.reader().readByte();
					Res.outz("mod attack id= " + num11.ToString());
					if (num11 == global::Char.myCharz().charID)
					{
						if (GameScr.findMobInMap(num12) != null)
						{
							global::Char.myCharz().mobMe.attackOtherMob(GameScr.findMobInMap(num12));
						}
					}
					else
					{
						@char = GameScr.findCharInMap(num11);
						if (@char != null && GameScr.findMobInMap(num12) != null)
						{
							@char.mobMe.attackOtherMob(GameScr.findMobInMap(num12));
						}
					}
				}
				if (b3 == 2)
				{
					int num13 = msg.reader().readInt();
					int num14 = msg.reader().readInt();
					int num15 = msg.readInt3Byte();
					int num16 = msg.readInt3Byte();
					if (num13 == global::Char.myCharz().charID)
					{
						Res.outz("mob dame= " + num15.ToString());
						@char = GameScr.findCharInMap(num14);
						if (@char != null)
						{
							@char.cHPNew = num16;
							if (global::Char.myCharz().mobMe.isBusyAttackSomeOne)
							{
								@char.doInjure(num15, 0, false, true);
							}
							else
							{
								global::Char.myCharz().mobMe.dame = num15;
								global::Char.myCharz().mobMe.setAttack(@char);
							}
						}
					}
					else
					{
						Mob mob2 = GameScr.findMobInMap(num13);
						if (mob2 != null)
						{
							if (num14 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().cHPNew = num16;
								if (mob2.isBusyAttackSomeOne)
								{
									global::Char.myCharz().doInjure(num15, 0, false, true);
								}
								else
								{
									mob2.dame = num15;
									mob2.setAttack(global::Char.myCharz());
								}
							}
							else
							{
								@char = GameScr.findCharInMap(num14);
								if (@char != null)
								{
									@char.cHPNew = num16;
									if (mob2.isBusyAttackSomeOne)
									{
										@char.doInjure(num15, 0, false, true);
									}
									else
									{
										mob2.dame = num15;
										mob2.setAttack(@char);
									}
								}
							}
						}
					}
				}
				if (b3 == 3)
				{
					int num17 = msg.reader().readInt();
					int num18 = msg.reader().readInt();
					int num19 = msg.readInt3Byte();
					int num20 = msg.readInt3Byte();
					@char = null;
					@char = ((global::Char.myCharz().charID != num17) ? GameScr.findCharInMap(num17) : global::Char.myCharz());
					if (@char != null)
					{
						Mob mob2 = GameScr.findMobInMap(num18);
						if (@char.mobMe != null)
						{
							@char.mobMe.attackOtherMob(mob2);
						}
						if (mob2 != null)
						{
							mob2.hp = num19;
							mob2.updateHp_bar();
							if (num20 == 0)
							{
								mob2.x = mob2.xFirst;
								mob2.y = mob2.yFirst;
								GameScr.startFlyText(mResources.miss, mob2.x, mob2.y - mob2.h, 0, -2, mFont.MISS);
							}
							else
							{
								GameScr.startFlyText("-" + num20.ToString(), mob2.x, mob2.y - mob2.h, 0, -2, mFont.ORANGE);
							}
						}
					}
				}
				if (b3 == 5)
				{
					int num21 = msg.reader().readInt();
					sbyte b4 = msg.reader().readByte();
					int num22 = msg.reader().readInt();
					int num23 = msg.readInt3Byte();
					int num24 = msg.readInt3Byte();
					@char = null;
					@char = ((num21 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num21) : global::Char.myCharz());
					if (@char == null)
					{
						return;
					}
					if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
					{
						@char.setSkillPaint(GameScr.sks[(int)b4], 0);
					}
					else
					{
						@char.setSkillPaint(GameScr.sks[(int)b4], 1);
					}
					Mob mob3 = GameScr.findMobInMap(num22);
					if (@char.cx <= mob3.x)
					{
						@char.cdir = 1;
					}
					else
					{
						@char.cdir = -1;
					}
					@char.mobFocus = mob3;
					mob3.hp = num24;
					mob3.updateHp_bar();
					GameCanvas.debug("SA83v2", 2);
					if (num23 == 0)
					{
						mob3.x = mob3.xFirst;
						mob3.y = mob3.yFirst;
						GameScr.startFlyText(mResources.miss, mob3.x, mob3.y - mob3.h, 0, -2, mFont.MISS);
					}
					else
					{
						GameScr.startFlyText("-" + num23.ToString(), mob3.x, mob3.y - mob3.h, 0, -2, mFont.ORANGE);
					}
				}
				if (b3 == 6)
				{
					int num25 = msg.reader().readInt();
					if (num25 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().mobMe.startDie();
					}
					else
					{
						global::Char char3 = GameScr.findCharInMap(num25);
						if (char3 != null)
						{
							char3.mobMe.startDie();
						}
					}
				}
				if (b3 != 7)
				{
					goto IL_8EFE;
				}
				int num26 = msg.reader().readInt();
				if (num26 == global::Char.myCharz().charID)
				{
					global::Char.myCharz().mobMe = null;
					for (int l = 0; l < GameScr.vMob.size(); l++)
					{
						if (((Mob)GameScr.vMob.elementAt(l)).mobId == num26)
						{
							GameScr.vMob.removeElementAt(l);
						}
					}
					goto IL_8EFE;
				}
				@char = GameScr.findCharInMap(num26);
				for (int m = 0; m < GameScr.vMob.size(); m++)
				{
					if (((Mob)GameScr.vMob.elementAt(m)).mobId == num26)
					{
						GameScr.vMob.removeElementAt(m);
					}
				}
				if (@char != null)
				{
					@char.mobMe = null;
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -94:
				while (msg.reader().available() > 0)
				{
					short num27 = msg.reader().readShort();
					int num28 = msg.reader().readInt();
					for (int n = 0; n < global::Char.myCharz().vSkill.size(); n++)
					{
						Skill skill = (Skill)global::Char.myCharz().vSkill.elementAt(n);
						if (skill != null && skill.skillId == num27)
						{
							if (num28 < skill.coolDown)
							{
								skill.lastTimeUseThisSkill = mSystem.currentTimeMillis() - (long)(skill.coolDown - num28);
							}
							Res.outz(string.Concat(new string[]
							{
								"1 chieu id= ",
								skill.template.id.ToString(),
								" cooldown= ",
								num28.ToString(),
								"curr cool down= ",
								skill.coolDown.ToString()
							}));
						}
					}
				}
				goto IL_8EFE;
			case -93:
			{
				short num29 = msg.reader().readShort();
				BgItem.newSmallVersion = new sbyte[(int)num29];
				for (int num30 = 0; num30 < (int)num29; num30++)
				{
					BgItem.newSmallVersion[num30] = msg.reader().readByte();
				}
				goto IL_8EFE;
			}
			case -92:
				Main.typeClient = (int)msg.reader().readByte();
				if (Rms.loadRMSString("ResVersion") == null)
				{
					Rms.clearAll();
				}
				Rms.saveRMSInt("clienttype", Main.typeClient);
				Rms.saveRMSInt("lastZoomlevel", mGraphics.zoomLevel);
				if (Rms.loadRMSString("ResVersion") == null)
				{
					GameCanvas.startOK(mResources.plsRestartGame, 8885, null);
					goto IL_8EFE;
				}
				goto IL_8EFE;
			case -91:
			{
				sbyte b5 = msg.reader().readByte();
				GameCanvas.panel.mapNames = new string[(int)b5];
				GameCanvas.panel.planetNames = new string[(int)b5];
				for (int num31 = 0; num31 < (int)b5; num31++)
				{
					GameCanvas.panel.mapNames[num31] = msg.reader().readUTF();
					GameCanvas.panel.planetNames[num31] = msg.reader().readUTF();
				}
				GameCanvas.panel.setTypeMapTrans();
				GameCanvas.panel.show();
				goto IL_8EFE;
			}
			case -90:
			{
				sbyte b6 = msg.reader().readByte();
				int num32 = msg.reader().readInt();
				Res.outz("===> UPDATE_BODY:    type = " + b6.ToString());
				@char = ((global::Char.myCharz().charID != num32) ? GameScr.findCharInMap(num32) : global::Char.myCharz());
				if (b6 != -1)
				{
					short num33 = msg.reader().readShort();
					short num34 = msg.reader().readShort();
					short num35 = msg.reader().readShort();
					sbyte b7 = msg.reader().readByte();
					if (@char != null)
					{
						if (@char.charID == num32)
						{
							@char.isMask = true;
							@char.isMonkey = b7;
							if (@char.isMonkey != 0)
							{
								@char.isWaitMonkey = false;
								@char.isLockMove = false;
							}
						}
						else if (@char != null)
						{
							@char.isMask = true;
							@char.isMonkey = b7;
						}
						if (num33 != -1)
						{
							@char.head = (int)num33;
						}
						if (num34 != -1)
						{
							@char.body = (int)num34;
						}
						if (num35 != -1)
						{
							@char.leg = (int)num35;
						}
					}
				}
				if (b6 == -1 && @char != null)
				{
					@char.isMask = false;
					@char.isMonkey = 0;
				}
				if (@char == null)
				{
					goto IL_8EFE;
				}
				for (int num36 = 0; num36 < 54; num36++)
				{
					@char.removeEffChar(0, 201 + num36);
				}
				if (@char.bag >= 201 && @char.bag < 255)
				{
					@char.addEffChar(new Effect(@char.bag, @char, 2, -1, 10, 1)
					{
						typeEff = 5
					});
				}
				if (@char.bag == 30 && @char.me)
				{
					GameScr.isPickNgocRong = true;
				}
				if (@char.me)
				{
					GameScr.isudungCapsun4 = false;
					GameScr.isudungCapsun3 = false;
					for (int num37 = 0; num37 < global::Char.myCharz().arrItemBag.Length; num37++)
					{
						Item item = global::Char.myCharz().arrItemBag[num37];
						if (item != null)
						{
							if (item.template.id == 194)
							{
								GameScr.isudungCapsun4 = item.quantity > 0;
								if (GameScr.isudungCapsun4)
								{
									break;
								}
							}
							else if (item.template.id == 193)
							{
								GameScr.isudungCapsun3 = item.quantity > 0;
							}
						}
					}
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			default:
				if (command != -112)
				{
					if (command != -107)
					{
						goto IL_8EFE;
					}
					sbyte b8 = msg.reader().readByte();
					if (b8 == 0)
					{
						global::Char.myCharz().havePet = false;
					}
					if (b8 == 1)
					{
						global::Char.myCharz().havePet = true;
					}
					if (b8 == 2)
					{
						InfoDlg.hide();
						global::Char.myPetz().head = (int)msg.reader().readShort();
						global::Char.myPetz().setDefaultPart();
						int num38 = (int)msg.reader().readUnsignedByte();
						Res.outz("num body = " + num38.ToString());
						global::Char.myPetz().arrItemBody = new Item[num38];
						for (int num39 = 0; num39 < num38; num39++)
						{
							short num40 = msg.reader().readShort();
							Res.outz("template id= " + num40.ToString());
							if (num40 != -1)
							{
								Res.outz("1");
								global::Char.myPetz().arrItemBody[num39] = new Item();
								global::Char.myPetz().arrItemBody[num39].template = ItemTemplates.get(num40);
								int type = (int)global::Char.myPetz().arrItemBody[num39].template.type;
								global::Char.myPetz().arrItemBody[num39].quantity = msg.reader().readInt();
								Res.outz("3");
								global::Char.myPetz().arrItemBody[num39].info = msg.reader().readUTF();
								global::Char.myPetz().arrItemBody[num39].content = msg.reader().readUTF();
								int num41 = (int)msg.reader().readUnsignedByte();
								Res.outz("option size= " + num41.ToString());
								if (num41 != 0)
								{
									global::Char.myPetz().arrItemBody[num39].itemOption = new ItemOption[num41];
									for (int num42 = 0; num42 < global::Char.myPetz().arrItemBody[num39].itemOption.Length; num42++)
									{
										int num43 = (int)msg.reader().readUnsignedByte();
										int num44 = (int)msg.reader().readUnsignedShort();
										if (num43 != -1)
										{
											global::Char.myPetz().arrItemBody[num39].itemOption[num42] = new ItemOption(num43, num44);
										}
									}
								}
								if (type == 0)
								{
									global::Char.myPetz().body = (int)global::Char.myPetz().arrItemBody[num39].template.part;
								}
								else if (type == 1)
								{
									global::Char.myPetz().leg = (int)global::Char.myPetz().arrItemBody[num39].template.part;
								}
							}
						}
						global::Char.myPetz().cHP = msg.readInt3Byte();
						global::Char.myPetz().cHPFull = msg.readInt3Byte();
						global::Char.myPetz().cMP = msg.readInt3Byte();
						global::Char.myPetz().cMPFull = msg.readInt3Byte();
						global::Char.myPetz().cDamFull = msg.readInt3Byte();
						global::Char.myPetz().cName = msg.reader().readUTF();
						global::Char.myPetz().currStrLevel = msg.reader().readUTF();
						global::Char.myPetz().cPower = msg.reader().readLong();
						global::Char.myPetz().cTiemNang = msg.reader().readLong();
						global::Char.myPetz().petStatus = msg.reader().readByte();
						global::Char.myPetz().cStamina = (int)msg.reader().readShort();
						global::Char.myPetz().cMaxStamina = msg.reader().readShort();
						global::Char.myPetz().cCriticalFull = (int)msg.reader().readByte();
						global::Char.myPetz().cDefull = (int)msg.reader().readShort();
						global::Char.myPetz().arrPetSkill = new Skill[(int)msg.reader().readByte()];
						string text4 = "SKILLENT = ";
						Skill[] arrPetSkill = global::Char.myPetz().arrPetSkill;
						Res.outz(text4 + ((arrPetSkill != null) ? arrPetSkill.ToString() : null));
						for (int num45 = 0; num45 < global::Char.myPetz().arrPetSkill.Length; num45++)
						{
							short num46 = msg.reader().readShort();
							if (num46 != -1)
							{
								global::Char.myPetz().arrPetSkill[num45] = Skills.get(num46);
							}
							else
							{
								global::Char.myPetz().arrPetSkill[num45] = new Skill();
								global::Char.myPetz().arrPetSkill[num45].template = null;
								global::Char.myPetz().arrPetSkill[num45].moreInfo = msg.reader().readUTF();
							}
						}
						goto IL_8EFE;
					}
					goto IL_8EFE;
				}
				else
				{
					sbyte b9 = msg.reader().readByte();
					if (b9 == 0)
					{
						GameScr.findMobInMap(msg.reader().readByte()).clearBody();
					}
					if (b9 == 1)
					{
						GameScr.findMobInMap(msg.reader().readByte()).setBody(msg.reader().readShort());
						goto IL_8EFE;
					}
					goto IL_8EFE;
				}
				break;
			case -88:
				GameCanvas.endDlg();
				GameCanvas.serverScreen.switchToMe();
				goto IL_8EFE;
			case -87:
			{
				Res.outz("GET UPDATE_DATA " + msg.reader().available().ToString() + " bytes");
				msg.reader().mark(100000);
				this.createData(msg.reader(), true);
				msg.reader().reset();
				sbyte[] array = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref array);
				Rms.saveRMS("NRdataVersion", new sbyte[] { GameScr.vcData });
				LoginScr.isUpdateData = false;
				if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
				{
					Res.outz(string.Concat(new string[]
					{
						GameScr.vsData.ToString(),
						",",
						GameScr.vsMap.ToString(),
						",",
						GameScr.vsSkill.ToString(),
						",",
						GameScr.vsItem.ToString()
					}));
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
					return;
				}
				goto IL_8EFE;
			}
			case -86:
			{
				sbyte b10 = msg.reader().readByte();
				Res.outz("server gui ve giao dich action = " + b10.ToString());
				if (b10 == 0)
				{
					int num47 = msg.reader().readInt();
					GameScr.gI().giaodich(num47);
				}
				if (b10 == 1)
				{
					int num48 = msg.reader().readInt();
					global::Char char4 = GameScr.findCharInMap(num48);
					if (char4 == null)
					{
						return;
					}
					GameCanvas.panel.setTypeGiaoDich(char4);
					GameCanvas.panel.show();
					Service.gI().getPlayerMenu(num48);
				}
				if (b10 == 2)
				{
					sbyte b11 = msg.reader().readByte();
					for (int num49 = 0; num49 < GameCanvas.panel.vMyGD.size(); num49++)
					{
						Item item2 = (Item)GameCanvas.panel.vMyGD.elementAt(num49);
						if (item2.indexUI == (int)b11)
						{
							GameCanvas.panel.vMyGD.removeElement(item2);
							break;
						}
					}
				}
				if (b10 == 6)
				{
					GameCanvas.panel.isFriendLock = true;
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.isFriendLock = true;
					}
					GameCanvas.panel.vFriendGD.removeAllElements();
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.vFriendGD.removeAllElements();
					}
					int num50 = msg.reader().readInt();
					sbyte b12 = msg.reader().readByte();
					Res.outz("item size = " + b12.ToString());
					for (int num51 = 0; num51 < (int)b12; num51++)
					{
						Item item3 = new Item();
						item3.template = ItemTemplates.get(msg.reader().readShort());
						item3.quantity = msg.reader().readInt();
						int num52 = (int)msg.reader().readUnsignedByte();
						if (num52 != 0)
						{
							item3.itemOption = new ItemOption[num52];
							for (int num53 = 0; num53 < item3.itemOption.Length; num53++)
							{
								int num54 = (int)msg.reader().readUnsignedByte();
								int num55 = (int)msg.reader().readUnsignedShort();
								if (num54 != -1)
								{
									item3.itemOption[num53] = new ItemOption(num54, num55);
									item3.compare = GameCanvas.panel.getCompare(item3);
								}
							}
						}
						if (GameCanvas.panel2 != null)
						{
							GameCanvas.panel2.vFriendGD.addElement(item3);
						}
						else
						{
							GameCanvas.panel.vFriendGD.addElement(item3);
						}
					}
					if (GameCanvas.panel2 != null)
					{
						GameCanvas.panel2.setTabGiaoDich(false);
						GameCanvas.panel2.friendMoneyGD = num50;
					}
					else
					{
						GameCanvas.panel.friendMoneyGD = num50;
						if (GameCanvas.panel.currentTabIndex == 2)
						{
							GameCanvas.panel.setTabGiaoDich(false);
						}
					}
				}
				if (b10 != 7)
				{
					goto IL_8EFE;
				}
				InfoDlg.hide();
				if (GameCanvas.panel.isShow)
				{
					GameCanvas.panel.hide();
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -85:
			{
				Res.outz("CAP CHAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAAA");
				sbyte b13 = msg.reader().readByte();
				if (b13 == 0)
				{
					int num56 = (int)msg.reader().readUnsignedShort();
					Res.outz("lent =" + num56.ToString());
					sbyte[] array2 = new sbyte[num56];
					msg.reader().read(ref array2, 0, num56);
					GameScr.imgCapcha = Image.createImage(array2, 0, num56);
					GameScr.gI().keyInput = "-----";
					GameScr.gI().strCapcha = msg.reader().readUTF();
					GameScr.gI().keyCapcha = new int[GameScr.gI().strCapcha.Length];
					GameScr.gI().mobCapcha = new Mob();
					GameScr.gI().right = null;
				}
				if (b13 == 1)
				{
					MobCapcha.isAttack = true;
				}
				if (b13 == 2)
				{
					MobCapcha.explode = true;
					GameScr.gI().right = GameScr.gI().cmdFocus;
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -84:
			{
				int num57 = (int)msg.reader().readUnsignedByte();
				Mob mob4 = null;
				try
				{
					mob4 = (Mob)GameScr.vMob.elementAt(num57);
				}
				catch (Exception)
				{
				}
				if (mob4 != null)
				{
					mob4.maxHp = msg.reader().readInt();
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -83:
			{
				sbyte b14 = msg.reader().readByte();
				if (b14 == 0)
				{
					int num58 = (int)msg.reader().readShort();
					int num59 = (int)msg.reader().readShort();
					int num60 = (int)msg.reader().readUnsignedByte();
					int num61 = msg.reader().readInt();
					msg.reader().readUTF();
					int num62 = (int)msg.reader().readShort();
					int num63 = (int)msg.reader().readShort();
					if (msg.reader().readByte() == 1)
					{
						GameScr.gI().isRongNamek = true;
					}
					else
					{
						GameScr.gI().isRongNamek = false;
					}
					GameScr.gI().xR = num62;
					GameScr.gI().yR = num63;
					Res.outz(string.Concat(new string[]
					{
						"xR= ",
						num62.ToString(),
						" yR= ",
						num63.ToString(),
						" +++++++++++++++++++++++++++++++++++++++"
					}));
					if (global::Char.myCharz().charID == num61)
					{
						GameCanvas.panel.hideNow();
						GameScr.gI().activeRongThanEff(true);
					}
					else if (TileMap.mapID == num58 && TileMap.zoneID == num60)
					{
						GameScr.gI().activeRongThanEff(false);
					}
					else if (mGraphics.zoomLevel > 1)
					{
						GameScr.gI().doiMauTroi();
					}
					GameScr.gI().mapRID = num58;
					GameScr.gI().bgRID = num59;
					GameScr.gI().zoneRID = num60;
				}
				if (b14 == 1)
				{
					Res.outz("map RID = " + GameScr.gI().mapRID.ToString() + " zone RID= " + GameScr.gI().zoneRID.ToString());
					Res.outz("map ID = " + TileMap.mapID.ToString() + " zone ID= " + TileMap.zoneID.ToString());
					if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
					{
						GameScr.gI().hideRongThanEff();
					}
					else
					{
						GameScr.gI().isRongThanXuatHien = false;
						if (GameScr.gI().isRongNamek)
						{
							GameScr.gI().isRongNamek = false;
						}
					}
				}
				if (b14 != 2)
				{
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -82:
			{
				sbyte b15 = msg.reader().readByte();
				TileMap.tileIndex = new int[(int)b15][][];
				TileMap.tileType = new int[(int)b15][];
				for (int num64 = 0; num64 < (int)b15; num64++)
				{
					sbyte b16 = msg.reader().readByte();
					TileMap.tileType[num64] = new int[(int)b16];
					TileMap.tileIndex[num64] = new int[(int)b16][];
					for (int num65 = 0; num65 < (int)b16; num65++)
					{
						TileMap.tileType[num64][num65] = msg.reader().readInt();
						sbyte b17 = msg.reader().readByte();
						TileMap.tileIndex[num64][num65] = new int[(int)b17];
						for (int num66 = 0; num66 < (int)b17; num66++)
						{
							TileMap.tileIndex[num64][num65][num66] = (int)msg.reader().readByte();
						}
					}
				}
				goto IL_8EFE;
			}
			case -81:
			{
				sbyte b18 = msg.reader().readByte();
				if (b18 == 0)
				{
					string text5 = msg.reader().readUTF();
					string text6 = msg.reader().readUTF();
					GameCanvas.panel.setTypeCombine();
					GameCanvas.panel.combineInfo = mFont.tahoma_7b_blue.splitFontArray(text5, Panel.WIDTH_PANEL);
					GameCanvas.panel.combineTopInfo = mFont.tahoma_7.splitFontArray(text6, Panel.WIDTH_PANEL);
					GameCanvas.panel.show();
				}
				if (b18 == 1)
				{
					GameCanvas.panel.vItemCombine.removeAllElements();
					sbyte b19 = msg.reader().readByte();
					for (int num67 = 0; num67 < (int)b19; num67++)
					{
						sbyte b20 = msg.reader().readByte();
						for (int num68 = 0; num68 < global::Char.myCharz().arrItemBag.Length; num68++)
						{
							Item item4 = global::Char.myCharz().arrItemBag[num68];
							if (item4 != null && item4.indexUI == (int)b20)
							{
								item4.isSelect = true;
								GameCanvas.panel.vItemCombine.addElement(item4);
							}
						}
					}
					if (GameCanvas.panel.isShow)
					{
						GameCanvas.panel.setTabCombine();
					}
				}
				if (b18 == 2)
				{
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(0);
				}
				if (b18 == 3)
				{
					GameCanvas.panel.combineSuccess = 1;
					GameCanvas.panel.setCombineEff(0);
				}
				if (b18 == 4)
				{
					short num69 = msg.reader().readShort();
					GameCanvas.panel.iconID3 = num69;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(1);
				}
				if (b18 == 5)
				{
					short num70 = msg.reader().readShort();
					GameCanvas.panel.iconID3 = num70;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(2);
				}
				if (b18 == 6)
				{
					short num71 = msg.reader().readShort();
					short num72 = msg.reader().readShort();
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(3);
					GameCanvas.panel.iconID1 = num71;
					GameCanvas.panel.iconID3 = num72;
				}
				if (b18 == 7)
				{
					short num73 = msg.reader().readShort();
					GameCanvas.panel.iconID3 = num73;
					GameCanvas.panel.combineSuccess = 0;
					GameCanvas.panel.setCombineEff(4);
				}
				if (b18 == 8)
				{
					GameCanvas.panel.iconID3 = -1;
					GameCanvas.panel.combineSuccess = 1;
					GameCanvas.panel.setCombineEff(4);
				}
				short num74 = 21;
				try
				{
					num74 = msg.reader().readShort();
					int num75 = (int)msg.reader().readShort();
					int num76 = (int)msg.reader().readShort();
					GameCanvas.panel.xS = num75 - GameScr.cmx;
					GameCanvas.panel.yS = num76 - GameScr.cmy;
				}
				catch (Exception)
				{
				}
				for (int num77 = 0; num77 < GameScr.vNpc.size(); num77++)
				{
					Npc npc = (Npc)GameScr.vNpc.elementAt(num77);
					if (npc.template.npcTemplateId == (int)num74)
					{
						GameCanvas.panel.xS = npc.cx - GameScr.cmx;
						GameCanvas.panel.yS = npc.cy - GameScr.cmy;
						GameCanvas.panel.idNPC = (int)num74;
						break;
					}
				}
				goto IL_8EFE;
			}
			case -80:
			{
				sbyte b21 = msg.reader().readByte();
				InfoDlg.hide();
				if (b21 == 0)
				{
					GameCanvas.panel.vFriend.removeAllElements();
					int num78 = (int)msg.reader().readUnsignedByte();
					for (int num79 = 0; num79 < num78; num79++)
					{
						global::Char char5 = new global::Char();
						char5.charID = msg.reader().readInt();
						char5.head = (int)msg.reader().readShort();
						char5.headICON = (int)msg.reader().readShort();
						char5.body = (int)msg.reader().readShort();
						char5.leg = (int)msg.reader().readShort();
						char5.bag = (int)msg.reader().readUnsignedByte();
						char5.cName = msg.reader().readUTF();
						bool flag3 = msg.reader().readBoolean();
						InfoItem infoItem2 = new InfoItem(mResources.power + ": " + msg.reader().readUTF());
						infoItem2.charInfo = char5;
						infoItem2.isOnline = flag3;
						GameCanvas.panel.vFriend.addElement(infoItem2);
					}
					GameCanvas.panel.setTypeFriend();
					GameCanvas.panel.show();
				}
				if (b21 == 3)
				{
					MyVector vFriend = GameCanvas.panel.vFriend;
					int num80 = msg.reader().readInt();
					Res.outz("online offline id=" + num80.ToString());
					for (int num81 = 0; num81 < vFriend.size(); num81++)
					{
						InfoItem infoItem3 = (InfoItem)vFriend.elementAt(num81);
						if (infoItem3.charInfo != null && infoItem3.charInfo.charID == num80)
						{
							Res.outz("online= " + infoItem3.isOnline.ToString());
							infoItem3.isOnline = msg.reader().readBoolean();
							break;
						}
					}
				}
				if (b21 != 2)
				{
					goto IL_8EFE;
				}
				MyVector vFriend2 = GameCanvas.panel.vFriend;
				int num82 = msg.reader().readInt();
				for (int num83 = 0; num83 < vFriend2.size(); num83++)
				{
					InfoItem infoItem4 = (InfoItem)vFriend2.elementAt(num83);
					if (infoItem4.charInfo != null && infoItem4.charInfo.charID == num82)
					{
						vFriend2.removeElement(infoItem4);
						break;
					}
				}
				if (GameCanvas.panel.isShow)
				{
					GameCanvas.panel.setTabFriend();
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -79:
			{
				InfoDlg.hide();
				msg.reader().readInt();
				global::Char charMenu = GameCanvas.panel.charMenu;
				if (charMenu == null)
				{
					return;
				}
				charMenu.cPower = msg.reader().readLong();
				charMenu.currStrLevel = msg.reader().readUTF();
				goto IL_8EFE;
			}
			case -77:
			{
				short num84 = msg.reader().readShort();
				SmallImage.newSmallVersion = new sbyte[(int)num84];
				SmallImage.maxSmall = num84;
				SmallImage.imgNew = new Small[(int)num84];
				for (int num85 = 0; num85 < (int)num84; num85++)
				{
					SmallImage.newSmallVersion[num85] = msg.reader().readByte();
				}
				goto IL_8EFE;
			}
			case -76:
			{
				sbyte b22 = msg.reader().readByte();
				if (b22 == 0)
				{
					sbyte b23 = msg.reader().readByte();
					if (b23 <= 0)
					{
						return;
					}
					global::Char.myCharz().arrArchive = new Archivement[(int)b23];
					for (int num86 = 0; num86 < (int)b23; num86++)
					{
						global::Char.myCharz().arrArchive[num86] = new Archivement();
						global::Char.myCharz().arrArchive[num86].info1 = (num86 + 1).ToString() + ". " + msg.reader().readUTF();
						global::Char.myCharz().arrArchive[num86].info2 = msg.reader().readUTF();
						global::Char.myCharz().arrArchive[num86].money = (int)msg.reader().readShort();
						global::Char.myCharz().arrArchive[num86].isFinish = msg.reader().readBoolean();
						global::Char.myCharz().arrArchive[num86].isRecieve = msg.reader().readBoolean();
					}
					GameCanvas.panel.setTypeArchivement();
					GameCanvas.panel.show();
					goto IL_8EFE;
				}
				else
				{
					if (b22 != 1)
					{
						goto IL_8EFE;
					}
					int num87 = (int)msg.reader().readUnsignedByte();
					if (global::Char.myCharz().arrArchive[num87] != null)
					{
						global::Char.myCharz().arrArchive[num87].isRecieve = true;
						goto IL_8EFE;
					}
					goto IL_8EFE;
				}
				break;
			}
			case -74:
			{
				if (ServerListScreen.stopDownload)
				{
					return;
				}
				if (!GameCanvas.isGetResourceFromServer())
				{
					Service.gI().getResource(3, null);
					SmallImage.loadBigRMS();
					SplashScr.imgLogo = null;
					if (Rms.loadRMSString("acc") != null || Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString()) != null)
					{
						LoginScr.isContinueToLogin = true;
					}
					GameCanvas.loginScr = new LoginScr();
					GameCanvas.loginScr.switchToMe();
					return;
				}
				bool flag4 = true;
				sbyte b24 = msg.reader().readByte();
				if (b24 == 0)
				{
					int num88 = msg.reader().readInt();
					string text7 = Rms.loadRMSString("ResVersion");
					int num89 = ((text7 == null || !(text7 != string.Empty)) ? (-1) : int.Parse(text7));
					if (text7 != null)
					{
						mSystem.println(string.Concat(new string[]
						{
							num88.ToString(),
							">>>strVersion: ",
							text7,
							" >> ",
							num89.ToString()
						}));
					}
					else
					{
						mSystem.println(">>>strVersion: nulll: " + num89.ToString());
					}
					if (Session_ME.gI().isCompareIPConnect())
					{
						if (num89 == -1 || num89 != num88)
						{
							GameCanvas.serverScreen.show2();
						}
						else
						{
							Res.outz("login ngay");
							SmallImage.loadBigRMS();
							SplashScr.imgLogo = null;
							ServerListScreen.loadScreen = true;
							if (GameCanvas.currentScreen != GameCanvas.loginScr)
							{
								GameCanvas.serverScreen.switchToMe();
							}
							else
							{
								if (GameCanvas.loginScr == null)
								{
									GameCanvas.loginScr = new LoginScr();
								}
								GameCanvas.loginScr.doLogin();
							}
						}
					}
					else
					{
						Session_ME.gI().close();
						ServerListScreen.loadScreen = true;
						ServerListScreen.isAutoConect = false;
						ServerListScreen.countDieConnect = 1000;
						GameCanvas.serverScreen.switchToMe();
					}
				}
				if (b24 == 1)
				{
					ServerListScreen.strWait = mResources.downloading_data;
					ServerListScreen.nBig = (int)msg.reader().readShort();
					Service.gI().getResource(2, null);
				}
				if (b24 == 2)
				{
					try
					{
						Controller.isLoadingData = true;
						GameCanvas.endDlg();
						ServerListScreen.demPercent++;
						ServerListScreen.percent = ServerListScreen.demPercent * 100 / ServerListScreen.nBig;
						string[] array3 = Res.split(msg.reader().readUTF(), "/", 0);
						string text8 = "x" + mGraphics.zoomLevel.ToString() + array3[array3.Length - 1];
						int num90 = msg.reader().readInt();
						sbyte[] array4 = new sbyte[num90];
						msg.reader().read(ref array4, 0, num90);
						Rms.saveRMS(text8, array4);
					}
					catch (Exception)
					{
						GameCanvas.startOK(mResources.pls_restart_game_error, 8885, null);
					}
				}
				if (b24 != 3 || !flag4)
				{
					goto IL_8EFE;
				}
				Controller.isLoadingData = false;
				int num91 = msg.reader().readInt();
				Res.outz("last version= " + num91.ToString());
				Rms.saveRMSString("ResVersion", num91.ToString() + string.Empty);
				Service.gI().getResource(3, null);
				GameCanvas.endDlg();
				SplashScr.imgLogo = null;
				SmallImage.loadBigRMS();
				mSystem.gcc();
				ServerListScreen.bigOk = true;
				ServerListScreen.loadScreen = true;
				GameScr.gI().loadGameScr();
				if (GameCanvas.currentScreen != GameCanvas.loginScr)
				{
					GameCanvas.serverScreen.switchToMe();
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -70:
			{
				Res.outz("BIG MESSAGE .......................................");
				GameCanvas.endDlg();
				int num92 = (int)msg.reader().readShort();
				ChatPopup.addBigMessage(msg.reader().readUTF(), 100000, new Npc(-1, 0, 0, 0, 0, 0)
				{
					avatar = num92
				});
				sbyte b25 = msg.reader().readByte();
				if (b25 == 0)
				{
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 35;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
				}
				if (b25 == 1)
				{
					string text9 = msg.reader().readUTF();
					string text10 = msg.reader().readUTF();
					ChatPopup.serverChatPopUp.cmdMsg1 = new Command(text10, ChatPopup.serverChatPopUp, 1000, text9);
					ChatPopup.serverChatPopUp.cmdMsg1.x = GameCanvas.w / 2 - 75;
					ChatPopup.serverChatPopUp.cmdMsg1.y = GameCanvas.h - 35;
					ChatPopup.serverChatPopUp.cmdMsg2 = new Command(mResources.CLOSE, ChatPopup.serverChatPopUp, 1001, null);
					ChatPopup.serverChatPopUp.cmdMsg2.x = GameCanvas.w / 2 + 11;
					ChatPopup.serverChatPopUp.cmdMsg2.y = GameCanvas.h - 35;
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -69:
				global::Char.myCharz().cMaxStamina = msg.reader().readShort();
				goto IL_8EFE;
			case -68:
				global::Char.myCharz().cStamina = (int)msg.reader().readShort();
				goto IL_8EFE;
			case -67:
			{
				this.demCount += 1f;
				int num93 = msg.reader().readInt();
				sbyte[] array5 = null;
				try
				{
					array5 = NinjaUtil.readByteArray(msg);
					SmallImage.imgNew[num93].img = this.createImage(array5);
				}
				catch (Exception)
				{
					array5 = null;
					SmallImage.imgNew[num93].img = Image.createRGBImage(new int[1], 1, 1, true);
				}
				if (array5 != null && mGraphics.zoomLevel > 1)
				{
					Rms.saveRMS(mGraphics.zoomLevel.ToString() + "Small" + num93.ToString(), array5);
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -66:
			{
				int num94 = (int)msg.reader().readShort();
				sbyte[] array6 = NinjaUtil.readByteArray(msg);
				EffectData effDataById = Effect.getEffDataById(num94);
				sbyte b26 = msg.reader().readSByte();
				if (b26 == 0)
				{
					effDataById.readData(array6);
				}
				else
				{
					effDataById.readDataNewBoss(array6, b26);
				}
				sbyte[] array7 = NinjaUtil.readByteArray(msg);
				effDataById.img = Image.createImage(array7, 0, array7.Length);
				goto IL_8EFE;
			}
			case -65:
			{
				InfoDlg.hide();
				int num95 = msg.reader().readInt();
				sbyte b27 = msg.reader().readByte();
				if (b27 == 0)
				{
					goto IL_8EFE;
				}
				if (global::Char.myCharz().charID == num95)
				{
					Controller.isStopReadMessage = true;
					GameScr.lockTick = 500;
					GameScr.gI().center = null;
					if (b27 == 0 || b27 == 1 || b27 == 3)
					{
						Teleport.addTeleport(new Teleport(global::Char.myCharz().cx, global::Char.myCharz().cy, global::Char.myCharz().head, global::Char.myCharz().cdir, 0, true, (b27 != 1) ? ((int)b27) : global::Char.myCharz().cgender));
					}
					if (b27 == 2)
					{
						GameScr.lockTick = 50;
						global::Char.myCharz().hide();
						goto IL_8EFE;
					}
					goto IL_8EFE;
				}
				else
				{
					global::Char char6 = GameScr.findCharInMap(num95);
					if ((b27 == 0 || b27 == 1 || b27 == 3) && char6 != null)
					{
						char6.isUsePlane = true;
						Teleport.addTeleport(new Teleport(char6.cx, char6.cy, char6.head, char6.cdir, 0, false, (b27 != 1) ? ((int)b27) : char6.cgender)
						{
							id = num95
						});
					}
					if (b27 == 2)
					{
						char6.hide();
						goto IL_8EFE;
					}
					goto IL_8EFE;
				}
				break;
			}
			case -64:
			{
				int num96 = msg.reader().readInt();
				int num97 = (int)msg.reader().readUnsignedByte();
				@char = null;
				@char = ((num96 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num96) : global::Char.myCharz());
				if (@char == null)
				{
					return;
				}
				@char.bag = num97;
				for (int num98 = 0; num98 < 54; num98++)
				{
					@char.removeEffChar(0, 201 + num98);
				}
				if (@char.bag >= 201 && @char.bag < 255)
				{
					@char.addEffChar(new Effect(@char.bag, @char, 2, -1, 10, 1)
					{
						typeEff = 5
					});
				}
				Res.outz(string.Concat(new string[]
				{
					"cmd:-64 UPDATE BAG PLAER = ",
					(@char != null) ? @char.cName : string.Empty,
					num96.ToString(),
					" BAG ID= ",
					num97.ToString()
				}));
				if (num97 == 30 && @char.me)
				{
					GameScr.isPickNgocRong = true;
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -63:
			{
				Res.outz("GET BAG");
				int num99 = (int)msg.reader().readUnsignedByte();
				sbyte b28 = msg.reader().readByte();
				ClanImage clanImage = new ClanImage();
				clanImage.ID = num99;
				if (b28 > 0)
				{
					clanImage.idImage = new short[(int)b28];
					for (int num100 = 0; num100 < (int)b28; num100++)
					{
						clanImage.idImage[num100] = msg.reader().readShort();
						Res.outz("ID=  " + num99.ToString() + " frame= " + clanImage.idImage[num100].ToString());
					}
					ClanImage.idImages.put(num99.ToString() + string.Empty, clanImage);
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -62:
			{
				int num101 = (int)msg.reader().readUnsignedByte();
				sbyte b29 = msg.reader().readByte();
				if (b29 <= 0)
				{
					goto IL_8EFE;
				}
				ClanImage clanImage2 = ClanImage.getClanImage((short)num101);
				if (clanImage2 != null)
				{
					clanImage2.idImage = new short[(int)b29];
					for (int num102 = 0; num102 < (int)b29; num102++)
					{
						clanImage2.idImage[num102] = msg.reader().readShort();
						if (clanImage2.idImage[num102] > 0)
						{
							SmallImage.vKeys.addElement(clanImage2.idImage[num102].ToString() + string.Empty);
						}
					}
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -61:
			{
				int num103 = msg.reader().readInt();
				if (num103 != global::Char.myCharz().charID)
				{
					if (GameScr.findCharInMap(num103) == null)
					{
						goto IL_8EFE;
					}
					GameScr.findCharInMap(num103).clanID = msg.reader().readInt();
					if (GameScr.findCharInMap(num103).clanID == -2)
					{
						GameScr.findCharInMap(num103).isCopy = true;
						goto IL_8EFE;
					}
					goto IL_8EFE;
				}
				else
				{
					if (global::Char.myCharz().clan != null)
					{
						global::Char.myCharz().clan.ID = msg.reader().readInt();
						goto IL_8EFE;
					}
					goto IL_8EFE;
				}
				break;
			}
			case -60:
			{
				GameCanvas.debug("SA7666", 2);
				int num104 = msg.reader().readInt();
				int num105 = -1;
				if (num104 != global::Char.myCharz().charID)
				{
					global::Char char7 = GameScr.findCharInMap(num104);
					if (char7 == null)
					{
						return;
					}
					if (char7.currentMovePoint != null)
					{
						char7.createShadow(char7.cx, char7.cy, 10);
						char7.cx = char7.currentMovePoint.xEnd;
						char7.cy = char7.currentMovePoint.yEnd;
					}
					int num106 = (int)msg.reader().readUnsignedByte();
					if ((TileMap.tileTypeAtPixel(char7.cx, char7.cy) & 2) == 2)
					{
						char7.setSkillPaint(GameScr.sks[num106], 0);
					}
					else
					{
						char7.setSkillPaint(GameScr.sks[num106], 1);
					}
					global::Char[] array8 = new global::Char[(int)msg.reader().readByte()];
					for (i = 0; i < array8.Length; i++)
					{
						num105 = msg.reader().readInt();
						global::Char char8;
						if (num105 == global::Char.myCharz().charID)
						{
							char8 = global::Char.myCharz();
							if (!GameScr.isChangeZone && GameScr.isAutoPlay && GameScr.canAutoPlay)
							{
								Service.gI().requestChangeZone(-1, -1);
								GameScr.isChangeZone = true;
							}
						}
						else
						{
							char8 = GameScr.findCharInMap(num105);
						}
						array8[i] = char8;
						if (i == 0)
						{
							if (char7.cx <= char8.cx)
							{
								char7.cdir = 1;
							}
							else
							{
								char7.cdir = -1;
							}
						}
					}
					if (i > 0)
					{
						char7.attChars = new global::Char[i];
						for (i = 0; i < char7.attChars.Length; i++)
						{
							char7.attChars[i] = array8[i];
						}
						char7.mobFocus = null;
						char7.charFocus = char7.attChars[0];
					}
				}
				else
				{
					msg.reader().readByte();
					msg.reader().readByte();
					num105 = msg.reader().readInt();
				}
				try
				{
					sbyte b30 = msg.reader().readByte();
					Res.outz("isRead continue = " + b30.ToString());
					if (b30 == 1)
					{
						sbyte b31 = msg.reader().readByte();
						Res.outz("type skill = " + b31.ToString());
						if (num105 == global::Char.myCharz().charID)
						{
							@char = global::Char.myCharz();
							int num107 = msg.readInt3Byte();
							Res.outz("dame hit = " + num107.ToString());
							@char.isDie = msg.reader().readBoolean();
							if (@char.isDie)
							{
								global::Char.isLockKey = true;
							}
							Res.outz("isDie=" + @char.isDie.ToString() + "---------------------------------------");
							int num108 = 0;
							bool flag5 = (@char.isCrit = msg.reader().readBoolean());
							@char.isMob = false;
							num107 = (@char.damHP = num107 + num108);
							if (b31 == 0)
							{
								@char.doInjure(num107, 0, flag5, false);
							}
						}
						else
						{
							@char = GameScr.findCharInMap(num105);
							if (@char == null)
							{
								return;
							}
							int num109 = msg.readInt3Byte();
							Res.outz("dame hit= " + num109.ToString());
							@char.isDie = msg.reader().readBoolean();
							Res.outz("isDie=" + @char.isDie.ToString() + "---------------------------------------");
							int num110 = 0;
							bool flag6 = (@char.isCrit = msg.reader().readBoolean());
							@char.isMob = false;
							num109 = (@char.damHP = num109 + num110);
							if (b31 == 0)
							{
								@char.doInjure(num109, 0, flag6, false);
							}
						}
					}
				}
				catch (Exception)
				{
				}
				goto IL_8EFE;
			}
			case -59:
			{
				sbyte b32 = msg.reader().readByte();
				GameScr.gI().player_vs_player(msg.reader().readInt(), msg.reader().readInt(), msg.reader().readUTF(), b32);
				goto IL_8EFE;
			}
			case -58:
			case 78:
			case 79:
				goto IL_8EFE;
			case -57:
			{
				string text11 = msg.reader().readUTF();
				int num111 = msg.reader().readInt();
				int num112 = msg.reader().readInt();
				GameScr.gI().clanInvite(text11, num111, num112);
				goto IL_8EFE;
			}
			case -53:
			{
				InfoDlg.hide();
				bool flag7 = false;
				int num113 = msg.reader().readInt();
				Res.outz("clanId= " + num113.ToString());
				if (num113 == -1)
				{
					global::Char.myCharz().clan = null;
					ClanMessage.vMessage.removeAllElements();
					if (GameCanvas.panel.member != null)
					{
						GameCanvas.panel.member.removeAllElements();
					}
					if (GameCanvas.panel.myMember != null)
					{
						GameCanvas.panel.myMember.removeAllElements();
					}
					if (GameCanvas.currentScreen == GameScr.gI())
					{
						GameCanvas.panel.setTabClans();
					}
					return;
				}
				GameCanvas.panel.tabIcon = null;
				if (global::Char.myCharz().clan == null)
				{
					global::Char.myCharz().clan = new Clan();
				}
				global::Char.myCharz().clan.ID = num113;
				global::Char.myCharz().clan.name = msg.reader().readUTF();
				global::Char.myCharz().clan.slogan = msg.reader().readUTF();
				global::Char.myCharz().clan.imgID = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().clan.powerPoint = msg.reader().readUTF();
				global::Char.myCharz().clan.leaderName = msg.reader().readUTF();
				global::Char.myCharz().clan.currMember = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().clan.maxMember = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().role = msg.reader().readByte();
				global::Char.myCharz().clan.clanPoint = msg.reader().readInt();
				global::Char.myCharz().clan.level = (int)msg.reader().readByte();
				GameCanvas.panel.myMember = new MyVector();
				for (int num114 = 0; num114 < global::Char.myCharz().clan.currMember; num114++)
				{
					Member member = new Member();
					member.ID = msg.reader().readInt();
					member.head = msg.reader().readShort();
					member.headICON = msg.reader().readShort();
					member.leg = msg.reader().readShort();
					member.body = msg.reader().readShort();
					member.name = msg.reader().readUTF();
					member.role = msg.reader().readByte();
					member.powerPoint = msg.reader().readUTF();
					member.donate = msg.reader().readInt();
					member.receive_donate = msg.reader().readInt();
					member.clanPoint = msg.reader().readInt();
					member.curClanPoint = msg.reader().readInt();
					member.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					GameCanvas.panel.myMember.addElement(member);
				}
				int num115 = (int)msg.reader().readUnsignedByte();
				for (int num116 = 0; num116 < num115; num116++)
				{
					this.readClanMsg(msg, -1);
				}
				if (GameCanvas.panel.isSearchClan || GameCanvas.panel.isViewMember || GameCanvas.panel.isMessage)
				{
					GameCanvas.panel.setTabClans();
				}
				if (flag7)
				{
					GameCanvas.panel.setTabClans();
				}
				Res.outz("=>>>>>>>>>>>>>>>>>>>>>> -537 MY CLAN INFO");
				goto IL_8EFE;
			}
			case -52:
			{
				sbyte b33 = msg.reader().readByte();
				if (b33 == 0)
				{
					Member member2 = new Member();
					member2.ID = msg.reader().readInt();
					member2.head = msg.reader().readShort();
					member2.headICON = msg.reader().readShort();
					member2.leg = msg.reader().readShort();
					member2.body = msg.reader().readShort();
					member2.name = msg.reader().readUTF();
					member2.role = msg.reader().readByte();
					member2.powerPoint = msg.reader().readUTF();
					member2.donate = msg.reader().readInt();
					member2.receive_donate = msg.reader().readInt();
					member2.clanPoint = msg.reader().readInt();
					member2.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					if (GameCanvas.panel.myMember == null)
					{
						GameCanvas.panel.myMember = new MyVector();
					}
					GameCanvas.panel.myMember.addElement(member2);
					GameCanvas.panel.initTabClans();
				}
				if (b33 == 1)
				{
					GameCanvas.panel.myMember.removeElementAt((int)msg.reader().readByte());
					GameCanvas.panel.currentListLength--;
					GameCanvas.panel.initTabClans();
				}
				if (b33 == 2)
				{
					Member member3 = new Member();
					member3.ID = msg.reader().readInt();
					member3.head = msg.reader().readShort();
					member3.headICON = msg.reader().readShort();
					member3.leg = msg.reader().readShort();
					member3.body = msg.reader().readShort();
					member3.name = msg.reader().readUTF();
					member3.role = msg.reader().readByte();
					member3.powerPoint = msg.reader().readUTF();
					member3.donate = msg.reader().readInt();
					member3.receive_donate = msg.reader().readInt();
					member3.clanPoint = msg.reader().readInt();
					member3.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					for (int num117 = 0; num117 < GameCanvas.panel.myMember.size(); num117++)
					{
						Member member4 = (Member)GameCanvas.panel.myMember.elementAt(num117);
						if (member4.ID == member3.ID)
						{
							if (global::Char.myCharz().charID == member3.ID)
							{
								global::Char.myCharz().role = member3.role;
							}
							Member member5 = member3;
							GameCanvas.panel.myMember.removeElement(member4);
							GameCanvas.panel.myMember.insertElementAt(member5, num117);
							return;
						}
					}
				}
				Res.outz("=>>>>>>>>>>>>>>>>>>>>>> -52  MY CLAN UPDSTE");
				goto IL_8EFE;
			}
			case -51:
				InfoDlg.hide();
				this.readClanMsg(msg, 0);
				if (GameCanvas.panel.isMessage && GameCanvas.panel.type == 5)
				{
					GameCanvas.panel.initTabClans();
					goto IL_8EFE;
				}
				goto IL_8EFE;
			case -50:
			{
				InfoDlg.hide();
				GameCanvas.panel.member = new MyVector();
				sbyte b34 = msg.reader().readByte();
				for (int num118 = 0; num118 < (int)b34; num118++)
				{
					Member member6 = new Member();
					member6.ID = msg.reader().readInt();
					member6.head = msg.reader().readShort();
					member6.headICON = msg.reader().readShort();
					member6.leg = msg.reader().readShort();
					member6.body = msg.reader().readShort();
					member6.name = msg.reader().readUTF();
					member6.role = msg.reader().readByte();
					member6.powerPoint = msg.reader().readUTF();
					member6.donate = msg.reader().readInt();
					member6.receive_donate = msg.reader().readInt();
					member6.clanPoint = msg.reader().readInt();
					member6.joinTime = NinjaUtil.getDate(msg.reader().readInt());
					GameCanvas.panel.member.addElement(member6);
				}
				GameCanvas.panel.isViewMember = true;
				GameCanvas.panel.isSearchClan = false;
				GameCanvas.panel.isMessage = false;
				GameCanvas.panel.currentListLength = GameCanvas.panel.member.size() + 2;
				GameCanvas.panel.initTabClans();
				goto IL_8EFE;
			}
			case -47:
			{
				InfoDlg.hide();
				sbyte b35 = msg.reader().readByte();
				Res.outz("clan = " + b35.ToString());
				if (b35 == 0)
				{
					GameCanvas.panel.clanReport = mResources.cannot_find_clan;
					GameCanvas.panel.clans = null;
				}
				else
				{
					GameCanvas.panel.clans = new Clan[(int)b35];
					Res.outz("clan search lent= " + GameCanvas.panel.clans.Length.ToString());
					for (int num119 = 0; num119 < GameCanvas.panel.clans.Length; num119++)
					{
						GameCanvas.panel.clans[num119] = new Clan();
						GameCanvas.panel.clans[num119].ID = msg.reader().readInt();
						GameCanvas.panel.clans[num119].name = msg.reader().readUTF();
						GameCanvas.panel.clans[num119].slogan = msg.reader().readUTF();
						GameCanvas.panel.clans[num119].imgID = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num119].powerPoint = msg.reader().readUTF();
						GameCanvas.panel.clans[num119].leaderName = msg.reader().readUTF();
						GameCanvas.panel.clans[num119].currMember = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num119].maxMember = (int)msg.reader().readUnsignedByte();
						GameCanvas.panel.clans[num119].date = msg.reader().readInt();
					}
				}
				GameCanvas.panel.isSearchClan = true;
				GameCanvas.panel.isViewMember = false;
				GameCanvas.panel.isMessage = false;
				if (GameCanvas.panel.isSearchClan)
				{
					GameCanvas.panel.initTabClans();
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -46:
			{
				InfoDlg.hide();
				sbyte b36 = msg.reader().readByte();
				if (b36 == 1 || b36 == 3)
				{
					GameCanvas.endDlg();
					ClanImage.vClanImage.removeAllElements();
					int num120 = (int)msg.reader().readUnsignedByte();
					for (int num121 = 0; num121 < num120; num121++)
					{
						ClanImage clanImage3 = new ClanImage();
						clanImage3.ID = (int)msg.reader().readUnsignedByte();
						clanImage3.name = msg.reader().readUTF();
						clanImage3.xu = msg.reader().readInt();
						clanImage3.luong = msg.reader().readInt();
						if (!ClanImage.isExistClanImage(clanImage3.ID))
						{
							ClanImage.addClanImage(clanImage3);
						}
						else
						{
							ClanImage.getClanImage((short)clanImage3.ID).name = clanImage3.name;
							ClanImage.getClanImage((short)clanImage3.ID).xu = clanImage3.xu;
							ClanImage.getClanImage((short)clanImage3.ID).luong = clanImage3.luong;
						}
					}
					if (global::Char.myCharz().clan != null)
					{
						GameCanvas.panel.changeIcon();
					}
				}
				if (b36 == 4)
				{
					global::Char.myCharz().clan.imgID = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().clan.slogan = msg.reader().readUTF();
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -45:
			{
				sbyte b37 = msg.reader().readByte();
				int num122 = msg.reader().readInt();
				short num123 = msg.reader().readShort();
				Res.outz(string.Concat(new string[]
				{
					">.SKILL_NOT_FOCUS      skillNotFocusID: ",
					num123.ToString(),
					" skill type= ",
					b37.ToString(),
					"   player use= ",
					num122.ToString()
				}));
				if (b37 == 20)
				{
					sbyte b38 = msg.reader().readByte();
					sbyte b39 = msg.reader().readByte();
					short num124 = msg.reader().readShort();
					bool flag8 = msg.reader().readByte() != 0;
					sbyte b40 = msg.reader().readByte();
					sbyte b41 = -1;
					try
					{
						b41 = msg.reader().readByte();
					}
					catch (Exception)
					{
					}
					Res.outz(">.SKILL_NOT_FOCUS  skill typeFrame= " + b38.ToString());
					@char = ((global::Char.myCharz().charID != num122) ? GameScr.findCharInMap(num122) : global::Char.myCharz());
					@char.SetSkillPaint_NEW(num123, flag8, b38, b40, b39, num124, b41);
				}
				if (b37 == 21)
				{
					Point point = new Point();
					point.x = (int)msg.reader().readShort();
					point.y = (int)msg.reader().readShort();
					short num125 = msg.reader().readShort();
					short num126 = msg.reader().readShort();
					sbyte b42 = 0;
					sbyte b43 = -1;
					Point[] array9 = null;
					@char = ((global::Char.myCharz().charID != num122) ? GameScr.findCharInMap(num122) : global::Char.myCharz());
					try
					{
						b42 = msg.reader().readByte();
						sbyte b44 = msg.reader().readByte();
						if (b44 > 0)
						{
							array9 = new Point[(int)b44];
							for (int num127 = 0; num127 < array9.Length; num127++)
							{
								array9[num127] = new Point();
								array9[num127].type = msg.reader().readByte();
								if (array9[num127].type == 0)
								{
									array9[num127].id = (int)msg.reader().readByte();
								}
								else
								{
									array9[num127].id = msg.reader().readInt();
								}
							}
						}
					}
					catch (Exception)
					{
					}
					try
					{
						b43 = msg.reader().readByte();
					}
					catch (Exception)
					{
					}
					Res.outz(string.Concat(new string[]
					{
						">.SKILL_NOT_FOCUS  skill targetDame= ",
						point.x.ToString(),
						":",
						point.y.ToString(),
						"    c:",
						@char.cx.ToString(),
						":",
						@char.cy.ToString(),
						"   cdir:",
						@char.cdir.ToString()
					}));
					@char.SetSkillPaint_STT(1, num123, point, num125, num126, b42, array9, b43);
				}
				if (b37 == 0)
				{
					Res.outz("id use= " + num122.ToString());
					if (global::Char.myCharz().charID != num122)
					{
						@char = GameScr.findCharInMap(num122);
						if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
						{
							@char.setSkillPaint(GameScr.sks[(int)num123], 0);
						}
						else
						{
							@char.setSkillPaint(GameScr.sks[(int)num123], 1);
							@char.delayFall = 20;
						}
					}
					else
					{
						global::Char.myCharz().saveLoadPreviousSkill();
						Res.outz("LOAD LAST SKILL");
					}
					sbyte b45 = msg.reader().readByte();
					Res.outz("npc size= " + b45.ToString());
					for (int num128 = 0; num128 < (int)b45; num128++)
					{
						sbyte b46 = msg.reader().readByte();
						sbyte b47 = msg.reader().readByte();
						Res.outz("index= " + b46.ToString());
						if (num123 >= 42 && num123 <= 48)
						{
							((Mob)GameScr.vMob.elementAt((int)b46)).isFreez = true;
							((Mob)GameScr.vMob.elementAt((int)b46)).seconds = (int)b47;
							((Mob)GameScr.vMob.elementAt((int)b46)).last = (((Mob)GameScr.vMob.elementAt((int)b46)).cur = mSystem.currentTimeMillis());
						}
					}
					sbyte b48 = msg.reader().readByte();
					for (int num129 = 0; num129 < (int)b48; num129++)
					{
						int num130 = msg.reader().readInt();
						sbyte b49 = msg.reader().readByte();
						Res.outz("player ID= " + num130.ToString() + " my ID= " + global::Char.myCharz().charID.ToString());
						if (num123 >= 42 && num123 <= 48)
						{
							if (num130 == global::Char.myCharz().charID)
							{
								if (!global::Char.myCharz().isFlyAndCharge && !global::Char.myCharz().isStandAndCharge)
								{
									GameScr.gI().isFreez = true;
									global::Char.myCharz().isFreez = true;
									global::Char.myCharz().freezSeconds = (int)b49;
									global::Char.myCharz().lastFreez = (global::Char.myCharz().currFreez = mSystem.currentTimeMillis());
									global::Char.myCharz().isLockMove = true;
								}
							}
							else
							{
								@char = GameScr.findCharInMap(num130);
								if (@char != null && !@char.isFlyAndCharge && !@char.isStandAndCharge)
								{
									@char.isFreez = true;
									@char.seconds = (int)b49;
									@char.freezSeconds = (int)b49;
									@char.lastFreez = (GameScr.findCharInMap(num130).currFreez = mSystem.currentTimeMillis());
								}
							}
						}
					}
				}
				if (b37 == 1 && num122 != global::Char.myCharz().charID)
				{
					try
					{
						GameScr.findCharInMap(num122).isCharge = true;
					}
					catch (Exception)
					{
					}
				}
				if (b37 == 3)
				{
					if (num122 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().isCharge = false;
						SoundMn.gI().taitaoPause();
						global::Char.myCharz().saveLoadPreviousSkill();
					}
					else
					{
						GameScr.findCharInMap(num122).isCharge = false;
					}
				}
				if (b37 == 4)
				{
					if (num122 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().seconds = (int)(msg.reader().readShort() - 1000);
						global::Char.myCharz().last = mSystem.currentTimeMillis();
						Res.outz("second= " + global::Char.myCharz().seconds.ToString() + " last= " + global::Char.myCharz().last.ToString());
					}
					else if (GameScr.findCharInMap(num122) != null)
					{
						int cgender = GameScr.findCharInMap(num122).cgender;
						if (cgender == 0)
						{
							if (TileMap.mapID != 170)
							{
								@char.useChargeSkill(false);
							}
							else
							{
								if (num123 >= 77 && num123 <= 83)
								{
									@char.useChargeSkill(true);
								}
								if (num123 >= 70 && num123 <= 76)
								{
									@char.useChargeSkill(false);
								}
							}
						}
						else if (cgender == 1)
						{
							if (TileMap.mapID != 170)
							{
								@char.useChargeSkill(true);
							}
							else
							{
								bool flag9 = true;
								if (num123 >= 70 && num123 <= 76)
								{
									flag9 = false;
								}
								if (num123 >= 77 && num123 <= 83)
								{
									flag9 = true;
								}
								@char.useChargeSkill(flag9);
							}
						}
						else if (TileMap.mapID == 170)
						{
							bool flag10 = true;
							if (num123 >= 70 && num123 <= 76)
							{
								flag10 = false;
							}
							if (num123 >= 77 && num123 <= 83)
							{
								flag10 = true;
							}
							@char.useChargeSkill(flag10);
						}
						@char.skillTemplateId = (int)num123;
						if (num123 >= 70 && num123 <= 76)
						{
							@char.isUseSkillAfterCharge = true;
						}
						@char.seconds = (int)msg.reader().readShort();
						@char.last = mSystem.currentTimeMillis();
					}
				}
				if (b37 == 5)
				{
					if (num122 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().stopUseChargeSkill();
					}
					else if (GameScr.findCharInMap(num122) != null)
					{
						GameScr.findCharInMap(num122).stopUseChargeSkill();
					}
				}
				if (b37 == 6)
				{
					if (num122 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().setAutoSkillPaint(GameScr.sks[(int)num123], 0);
					}
					else if (GameScr.findCharInMap(num122) != null)
					{
						GameScr.findCharInMap(num122).setAutoSkillPaint(GameScr.sks[(int)num123], 0);
						SoundMn.gI().gong();
					}
				}
				if (b37 == 7)
				{
					if (num122 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().seconds = (int)msg.reader().readShort();
						Res.outz("second = " + global::Char.myCharz().seconds.ToString());
						global::Char.myCharz().last = mSystem.currentTimeMillis();
					}
					else if (GameScr.findCharInMap(num122) != null)
					{
						GameScr.findCharInMap(num122).useChargeSkill(true);
						GameScr.findCharInMap(num122).seconds = (int)msg.reader().readShort();
						GameScr.findCharInMap(num122).last = mSystem.currentTimeMillis();
						SoundMn.gI().gong();
					}
				}
				if (b37 == 8 && num122 != global::Char.myCharz().charID && GameScr.findCharInMap(num122) != null)
				{
					GameScr.findCharInMap(num122).setAutoSkillPaint(GameScr.sks[(int)num123], 0);
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -44:
			{
				bool flag11 = false;
				if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
				{
					flag11 = true;
				}
				sbyte b50 = msg.reader().readByte();
				int num131 = (int)msg.reader().readUnsignedByte();
				global::Char.myCharz().arrItemShop = new Item[num131][];
				GameCanvas.panel.shopTabName = new string[num131 + ((!flag11) ? 1 : 0)][];
				for (int num132 = 0; num132 < GameCanvas.panel.shopTabName.Length; num132++)
				{
					GameCanvas.panel.shopTabName[num132] = new string[2];
				}
				if (b50 == 2)
				{
					GameCanvas.panel.maxPageShop = new int[num131];
					GameCanvas.panel.currPageShop = new int[num131];
				}
				if (!flag11)
				{
					GameCanvas.panel.shopTabName[num131] = mResources.inventory;
				}
				for (int num133 = 0; num133 < num131; num133++)
				{
					string[] array10 = Res.split(msg.reader().readUTF(), "\n", 0);
					if (b50 == 2)
					{
						GameCanvas.panel.maxPageShop[num133] = (int)msg.reader().readUnsignedByte();
					}
					if (array10.Length == 2)
					{
						GameCanvas.panel.shopTabName[num133] = array10;
					}
					if (array10.Length == 1)
					{
						GameCanvas.panel.shopTabName[num133][0] = array10[0];
						GameCanvas.panel.shopTabName[num133][1] = string.Empty;
					}
					int num134 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemShop[num133] = new Item[num134];
					Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
					if (b50 == 1)
					{
						Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy2;
					}
					for (int num135 = 0; num135 < num134; num135++)
					{
						short num136 = msg.reader().readShort();
						if (num136 != -1)
						{
							global::Char.myCharz().arrItemShop[num133][num135] = new Item();
							global::Char.myCharz().arrItemShop[num133][num135].template = ItemTemplates.get(num136);
							Res.outz(string.Concat(new string[]
							{
								"name ",
								num133.ToString(),
								" = ",
								global::Char.myCharz().arrItemShop[num133][num135].template.name,
								" id templat= ",
								global::Char.myCharz().arrItemShop[num133][num135].template.id.ToString()
							}));
							if (b50 == 8)
							{
								global::Char.myCharz().arrItemShop[num133][num135].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num133][num135].buyGold = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num133][num135].quantity = msg.reader().readInt();
							}
							else if (b50 == 4)
							{
								global::Char.myCharz().arrItemShop[num133][num135].reason = msg.reader().readUTF();
							}
							else if (b50 == 0)
							{
								global::Char.myCharz().arrItemShop[num133][num135].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num133][num135].buyGold = msg.reader().readInt();
							}
							else if (b50 == 1)
							{
								global::Char.myCharz().arrItemShop[num133][num135].powerRequire = msg.reader().readLong();
							}
							else if (b50 == 2)
							{
								global::Char.myCharz().arrItemShop[num133][num135].itemId = (int)msg.reader().readShort();
								global::Char.myCharz().arrItemShop[num133][num135].buyCoin = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num133][num135].buyGold = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num133][num135].buyType = msg.reader().readByte();
								global::Char.myCharz().arrItemShop[num133][num135].quantity = msg.reader().readInt();
								global::Char.myCharz().arrItemShop[num133][num135].isMe = msg.reader().readByte();
							}
							else if (b50 == 3)
							{
								global::Char.myCharz().arrItemShop[num133][num135].isBuySpec = true;
								global::Char.myCharz().arrItemShop[num133][num135].iconSpec = msg.reader().readShort();
								global::Char.myCharz().arrItemShop[num133][num135].buySpec = msg.reader().readInt();
							}
							int num137 = (int)msg.reader().readUnsignedByte();
							if (num137 != 0)
							{
								global::Char.myCharz().arrItemShop[num133][num135].itemOption = new ItemOption[num137];
								for (int num138 = 0; num138 < global::Char.myCharz().arrItemShop[num133][num135].itemOption.Length; num138++)
								{
									int num139 = (int)msg.reader().readUnsignedByte();
									int num140 = (int)msg.reader().readUnsignedShort();
									if (num139 != -1)
									{
										global::Char.myCharz().arrItemShop[num133][num135].itemOption[num138] = new ItemOption(num139, num140);
										global::Char.myCharz().arrItemShop[num133][num135].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemShop[num133][num135]);
									}
								}
							}
							sbyte b51 = msg.reader().readByte();
							global::Char.myCharz().arrItemShop[num133][num135].newItem = b51 != 0;
							if (msg.reader().readByte() == 1)
							{
								int num141 = (int)msg.reader().readShort();
								int num142 = (int)msg.reader().readShort();
								int num143 = (int)msg.reader().readShort();
								int num144 = (int)msg.reader().readShort();
								global::Char.myCharz().arrItemShop[num133][num135].setPartTemp(num141, num142, num143, num144);
							}
							if (b50 == 2 && GameMidlet.intVERSION >= 237)
							{
								global::Char.myCharz().arrItemShop[num133][num135].nameNguoiKyGui = msg.reader().readUTF();
								Res.err("nguoi ki gui  " + global::Char.myCharz().arrItemShop[num133][num135].nameNguoiKyGui);
							}
						}
					}
				}
				if (flag11)
				{
					if (b50 != 2)
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.tabName[7] = new string[][] { new string[] { string.Empty } };
						GameCanvas.panel2.setTypeBodyOnly();
						GameCanvas.panel2.show();
					}
					else
					{
						GameCanvas.panel2 = new Panel();
						GameCanvas.panel2.setTypeKiGuiOnly();
						GameCanvas.panel2.show();
					}
				}
				GameCanvas.panel.tabName[1] = GameCanvas.panel.shopTabName;
				if (b50 == 2)
				{
					string[][] array11 = GameCanvas.panel.tabName[1];
					if (flag11)
					{
						GameCanvas.panel.tabName[1] = new string[][]
						{
							array11[0],
							array11[1],
							array11[2],
							array11[3]
						};
					}
					else
					{
						GameCanvas.panel.tabName[1] = new string[][]
						{
							array11[0],
							array11[1],
							array11[2],
							array11[3],
							array11[4]
						};
					}
				}
				GameCanvas.panel.setTypeShop((int)b50);
				GameCanvas.panel.show();
				goto IL_8EFE;
			}
			case -43:
			{
				sbyte b52 = msg.reader().readByte();
				sbyte b53 = msg.reader().readByte();
				sbyte b54 = msg.reader().readByte();
				string text12 = msg.reader().readUTF();
				GameCanvas.panel.itemRequest(b52, text12, b53, b54);
				goto IL_8EFE;
			}
			case -42:
				global::Char.myCharz().cHPGoc = msg.readInt3Byte();
				global::Char.myCharz().cMPGoc = msg.readInt3Byte();
				global::Char.myCharz().cDamGoc = msg.reader().readInt();
				global::Char.myCharz().cHPFull = msg.readInt3Byte();
				global::Char.myCharz().cMPFull = msg.readInt3Byte();
				global::Char.myCharz().cHP = msg.readInt3Byte();
				global::Char.myCharz().cMP = msg.readInt3Byte();
				global::Char.myCharz().cspeed = (int)msg.reader().readByte();
				global::Char.myCharz().hpFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().mpFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().damFrom1000TiemNang = msg.reader().readByte();
				global::Char.myCharz().cDamFull = msg.reader().readInt();
				global::Char.myCharz().cDefull = msg.reader().readInt();
				global::Char.myCharz().cCriticalFull = (int)msg.reader().readByte();
				global::Char.myCharz().cTiemNang = msg.reader().readLong();
				global::Char.myCharz().expForOneAdd = msg.reader().readShort();
				global::Char.myCharz().cDefGoc = (int)msg.reader().readShort();
				global::Char.myCharz().cCriticalGoc = (int)msg.reader().readByte();
				InfoDlg.hide();
				goto IL_8EFE;
			case -41:
			{
				sbyte b55 = msg.reader().readByte();
				global::Char.myCharz().strLevel = new string[(int)b55];
				for (int num145 = 0; num145 < (int)b55; num145++)
				{
					string text13 = msg.reader().readUTF();
					global::Char.myCharz().strLevel[num145] = text13;
				}
				Res.outz("---   xong  level caption cmd : " + msg.command.ToString());
				goto IL_8EFE;
			}
			case -37:
			{
				sbyte b56 = msg.reader().readByte();
				Res.outz("cAction= " + b56.ToString());
				if (b56 == 0)
				{
					global::Char.myCharz().head = (int)msg.reader().readShort();
					global::Char.myCharz().setDefaultPart();
					int num146 = (int)msg.reader().readUnsignedByte();
					Res.outz("num body = " + num146.ToString());
					global::Char.myCharz().arrItemBody = new Item[num146];
					for (int num147 = 0; num147 < num146; num147++)
					{
						short num148 = msg.reader().readShort();
						if (num148 != -1)
						{
							global::Char.myCharz().arrItemBody[num147] = new Item();
							global::Char.myCharz().arrItemBody[num147].template = ItemTemplates.get(num148);
							int type2 = (int)global::Char.myCharz().arrItemBody[num147].template.type;
							global::Char.myCharz().arrItemBody[num147].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBody[num147].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBody[num147].content = msg.reader().readUTF();
							int num149 = (int)msg.reader().readUnsignedByte();
							if (num149 != 0)
							{
								global::Char.myCharz().arrItemBody[num147].itemOption = new ItemOption[num149];
								for (int num150 = 0; num150 < global::Char.myCharz().arrItemBody[num147].itemOption.Length; num150++)
								{
									int num151 = (int)msg.reader().readUnsignedByte();
									int num152 = (int)msg.reader().readUnsignedShort();
									if (num151 != -1)
									{
										global::Char.myCharz().arrItemBody[num147].itemOption[num150] = new ItemOption(num151, num152);
									}
								}
							}
							if (type2 == 0)
							{
								global::Char.myCharz().body = (int)global::Char.myCharz().arrItemBody[num147].template.part;
							}
							else if (type2 == 1)
							{
								global::Char.myCharz().leg = (int)global::Char.myCharz().arrItemBody[num147].template.part;
							}
						}
					}
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -36:
			{
				sbyte b57 = msg.reader().readByte();
				Res.outz("cAction= " + b57.ToString());
				GameScr.isudungCapsun4 = false;
				GameScr.isudungCapsun3 = false;
				if (b57 == 0)
				{
					int num153 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemBag = new Item[num153];
					GameScr.hpPotion = 0;
					Res.outz("numC=" + num153.ToString());
					for (int num154 = 0; num154 < num153; num154++)
					{
						short num155 = msg.reader().readShort();
						if (num155 != -1)
						{
							global::Char.myCharz().arrItemBag[num154] = new Item();
							global::Char.myCharz().arrItemBag[num154].template = ItemTemplates.get(num155);
							global::Char.myCharz().arrItemBag[num154].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBag[num154].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBag[num154].content = msg.reader().readUTF();
							global::Char.myCharz().arrItemBag[num154].indexUI = num154;
							int num156 = (int)msg.reader().readUnsignedByte();
							if (num156 != 0)
							{
								global::Char.myCharz().arrItemBag[num154].itemOption = new ItemOption[num156];
								for (int num157 = 0; num157 < global::Char.myCharz().arrItemBag[num154].itemOption.Length; num157++)
								{
									int num158 = (int)msg.reader().readUnsignedByte();
									int num159 = (int)msg.reader().readUnsignedShort();
									if (num158 != -1)
									{
										global::Char.myCharz().arrItemBag[num154].itemOption[num157] = new ItemOption(num158, num159);
									}
								}
								global::Char.myCharz().arrItemBag[num154].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemBag[num154]);
							}
							sbyte type3 = global::Char.myCharz().arrItemBag[num154].template.type;
							if (global::Char.myCharz().arrItemBag[num154].template.type == 6)
							{
								GameScr.hpPotion += global::Char.myCharz().arrItemBag[num154].quantity;
							}
							if (global::Char.myCharz().arrItemBag[num154].template.id == 194)
							{
								GameScr.isudungCapsun4 = global::Char.myCharz().arrItemBag[num154].quantity > 0;
							}
							else if (global::Char.myCharz().arrItemBag[num154].template.id == 193 && !GameScr.isudungCapsun4)
							{
								GameScr.isudungCapsun3 = global::Char.myCharz().arrItemBag[num154].quantity > 0;
							}
						}
					}
				}
				if (b57 != 2)
				{
					goto IL_8EFE;
				}
				sbyte b58 = msg.reader().readByte();
				int num160 = msg.reader().readInt();
				int quantity = global::Char.myCharz().arrItemBag[(int)b58].quantity;
				int id = (int)global::Char.myCharz().arrItemBag[(int)b58].template.id;
				global::Char.myCharz().arrItemBag[(int)b58].quantity = num160;
				if (global::Char.myCharz().arrItemBag[(int)b58].quantity < quantity && global::Char.myCharz().arrItemBag[(int)b58].template.type == 6)
				{
					GameScr.hpPotion -= quantity - global::Char.myCharz().arrItemBag[(int)b58].quantity;
				}
				if (global::Char.myCharz().arrItemBag[(int)b58].quantity == 0)
				{
					global::Char.myCharz().arrItemBag[(int)b58] = null;
				}
				if (id == 194)
				{
					GameScr.isudungCapsun4 = num160 > 0;
					goto IL_8EFE;
				}
				if (id == 193)
				{
					GameScr.isudungCapsun3 = num160 > 0;
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -35:
			{
				sbyte b59 = msg.reader().readByte();
				Res.outz("cAction= " + b59.ToString());
				if (b59 == 0)
				{
					int num161 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemBox = new Item[num161];
					GameCanvas.panel.hasUse = 0;
					for (int num162 = 0; num162 < num161; num162++)
					{
						short num163 = msg.reader().readShort();
						if (num163 != -1)
						{
							global::Char.myCharz().arrItemBox[num162] = new Item();
							global::Char.myCharz().arrItemBox[num162].template = ItemTemplates.get(num163);
							global::Char.myCharz().arrItemBox[num162].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBox[num162].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBox[num162].content = msg.reader().readUTF();
							int num164 = (int)msg.reader().readUnsignedByte();
							if (num164 != 0)
							{
								global::Char.myCharz().arrItemBox[num162].itemOption = new ItemOption[num164];
								for (int num165 = 0; num165 < global::Char.myCharz().arrItemBox[num162].itemOption.Length; num165++)
								{
									int num166 = (int)msg.reader().readUnsignedByte();
									int num167 = (int)msg.reader().readUnsignedShort();
									if (num166 != -1)
									{
										global::Char.myCharz().arrItemBox[num162].itemOption[num165] = new ItemOption(num166, num167);
									}
								}
							}
							GameCanvas.panel.hasUse++;
						}
					}
				}
				if (b59 == 1)
				{
					bool flag12 = false;
					try
					{
						if (msg.reader().readByte() == 1)
						{
							flag12 = true;
						}
					}
					catch (Exception)
					{
					}
					GameCanvas.panel.setTypeBox();
					GameCanvas.panel.isBoxClan = flag12;
					GameCanvas.panel.show();
				}
				if (b59 != 2)
				{
					goto IL_8EFE;
				}
				sbyte b60 = msg.reader().readByte();
				int num168 = msg.reader().readInt();
				global::Char.myCharz().arrItemBox[(int)b60].quantity = num168;
				if (global::Char.myCharz().arrItemBox[(int)b60].quantity == 0)
				{
					global::Char.myCharz().arrItemBox[(int)b60] = null;
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -34:
			{
				sbyte b61 = msg.reader().readByte();
				Res.outz("act= " + b61.ToString());
				if (b61 == 0 && GameScr.gI().magicTree != null)
				{
					Res.outz("toi duoc day");
					MagicTree magicTree = GameScr.gI().magicTree;
					magicTree.id = (int)msg.reader().readShort();
					magicTree.name = msg.reader().readUTF();
					magicTree.name = Res.changeString(magicTree.name);
					magicTree.x = (int)msg.reader().readShort();
					magicTree.y = (int)msg.reader().readShort();
					magicTree.level = (int)msg.reader().readByte();
					magicTree.currPeas = (int)msg.reader().readShort();
					magicTree.maxPeas = (int)msg.reader().readShort();
					Res.outz("curr Peas= " + magicTree.currPeas.ToString());
					magicTree.strInfo = msg.reader().readUTF();
					magicTree.seconds = msg.reader().readInt();
					magicTree.timeToRecieve = magicTree.seconds;
					sbyte b62 = msg.reader().readByte();
					magicTree.peaPostionX = new int[(int)b62];
					magicTree.peaPostionY = new int[(int)b62];
					for (int num169 = 0; num169 < (int)b62; num169++)
					{
						magicTree.peaPostionX[num169] = (int)msg.reader().readByte();
						magicTree.peaPostionY[num169] = (int)msg.reader().readByte();
					}
					magicTree.isUpdate = msg.reader().readBool();
					magicTree.last = (magicTree.cur = mSystem.currentTimeMillis());
					GameScr.gI().magicTree.isUpdateTree = true;
				}
				if (b61 == 1)
				{
					myVector = new MyVector();
					try
					{
						while (msg.reader().available() > 0)
						{
							myVector.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 888392, null));
						}
					}
					catch (Exception ex)
					{
						Cout.println("Loi MAGIC_TREE " + ex.ToString());
					}
					GameCanvas.menu.startAt(myVector, 3);
				}
				if (b61 == 2)
				{
					GameScr.gI().magicTree.remainPeas = (int)msg.reader().readShort();
					GameScr.gI().magicTree.seconds = msg.reader().readInt();
					GameScr.gI().magicTree.last = (GameScr.gI().magicTree.cur = mSystem.currentTimeMillis());
					GameScr.gI().magicTree.isUpdateTree = true;
					GameScr.gI().magicTree.isPeasEffect = true;
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -32:
			{
				short num170 = msg.reader().readShort();
				int num171 = msg.reader().readInt();
				sbyte[] array12 = null;
				Image image = null;
				try
				{
					array12 = new sbyte[num171];
					for (int num172 = 0; num172 < num171; num172++)
					{
						array12[num172] = msg.reader().readByte();
					}
					image = Image.createImage(array12, 0, num171);
					BgItem.imgNew.put(num170.ToString() + string.Empty, image);
				}
				catch (Exception)
				{
					array12 = null;
					BgItem.imgNew.put(num170.ToString() + string.Empty, Image.createRGBImage(new int[1], 1, 1, true));
				}
				if (array12 != null)
				{
					if (mGraphics.zoomLevel > 1)
					{
						Rms.saveRMS(mGraphics.zoomLevel.ToString() + "bgItem" + num170.ToString(), array12);
					}
					BgItemMn.blendcurrBg(num170, image);
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case -31:
			{
				TileMap.vItemBg.removeAllElements();
				short num173 = msg.reader().readShort();
				Res.err("[ITEM_BACKGROUND] nItem= " + num173.ToString());
				for (int num174 = 0; num174 < (int)num173; num174++)
				{
					BgItem bgItem = new BgItem();
					bgItem.id = num174;
					bgItem.idImage = msg.reader().readShort();
					bgItem.layer = msg.reader().readByte();
					bgItem.dx = (int)msg.reader().readShort();
					bgItem.dy = (int)msg.reader().readShort();
					sbyte b63 = msg.reader().readByte();
					bgItem.tileX = new int[(int)b63];
					bgItem.tileY = new int[(int)b63];
					for (int num175 = 0; num175 < (int)b63; num175++)
					{
						bgItem.tileX[num174] = (int)msg.reader().readByte();
						bgItem.tileY[num174] = (int)msg.reader().readByte();
					}
					TileMap.vItemBg.addElement(bgItem);
				}
				goto IL_8EFE;
			}
			case -30:
				this.messageSubCommand(msg);
				goto IL_8EFE;
			case -29:
				this.messageNotLogin(msg);
				goto IL_8EFE;
			case -28:
				this.messageNotMap(msg);
				goto IL_8EFE;
			case -26:
				ServerListScreen.testConnect = 2;
				GameCanvas.debug("SA2", 2);
				GameCanvas.startOKDlg(msg.reader().readUTF());
				InfoDlg.hide();
				LoginScr.isContinueToLogin = false;
				global::Char.isLoadingMap = false;
				if (GameCanvas.currentScreen == GameCanvas.loginScr)
				{
					GameCanvas.serverScreen.switchToMe();
					goto IL_8EFE;
				}
				goto IL_8EFE;
			case -25:
				GameCanvas.debug("SA3", 2);
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				goto IL_8EFE;
			case -24:
				Res.outz("***************MAP_INFO**************");
				GameScr.isPickNgocRong = false;
				global::Char.isLoadingMap = true;
				Cout.println("GET MAP INFO");
				GameScr.gI().magicTree = null;
				GameCanvas.isLoading = true;
				GameCanvas.debug("SA75", 2);
				GameScr.resetAllvector();
				GameCanvas.endDlg();
				TileMap.vGo.removeAllElements();
				PopUp.vPopups.removeAllElements();
				mSystem.gcc();
				TileMap.mapID = (int)msg.reader().readUnsignedByte();
				TileMap.planetID = msg.reader().readByte();
				TileMap.tileID = (int)msg.reader().readByte();
				TileMap.bgID = (int)msg.reader().readByte();
				GameScr.isPaint_CT = TileMap.mapID != 170;
				Cout.println(string.Concat(new string[]
				{
					"load planet from server: ",
					TileMap.planetID.ToString(),
					"bgType= ",
					TileMap.bgType.ToString(),
					"............................."
				}));
				TileMap.typeMap = (int)msg.reader().readByte();
				TileMap.mapName = msg.reader().readUTF();
				TileMap.zoneID = (int)msg.reader().readByte();
				GameCanvas.debug("SA75x1", 2);
				try
				{
					TileMap.loadMapFromResource(TileMap.mapID);
				}
				catch (Exception)
				{
					Service.gI().requestMaptemplate(TileMap.mapID);
					this.messWait = msg;
					goto IL_8EFE;
				}
				this.loadInfoMap(msg);
				try
				{
					TileMap.isMapDouble = msg.reader().readByte() != 0;
				}
				catch (Exception)
				{
				}
				GameScr.cmx = GameScr.cmtoX;
				GameScr.cmy = GameScr.cmtoY;
				GameCanvas.isRequestMapID = 2;
				GameCanvas.waitingTimeChangeMap = mSystem.currentTimeMillis() + 1000L;
				goto IL_8EFE;
			case -22:
				GameCanvas.debug("SA65", 2);
				global::Char.isLockKey = true;
				global::Char.ischangingMap = true;
				GameScr.gI().timeStartMap = 0;
				GameScr.gI().timeLengthMap = 0;
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().npcFocus = null;
				global::Char.myCharz().charFocus = null;
				global::Char.myCharz().itemFocus = null;
				global::Char.myCharz().focus.removeAllElements();
				global::Char.myCharz().testCharId = -9999;
				global::Char.myCharz().killCharId = -9999;
				GameCanvas.resetBg();
				GameScr.gI().resetButton();
				GameScr.gI().center = null;
				if (Effect.vEffData.size() > 15)
				{
					for (int num176 = 0; num176 < 5; num176++)
					{
						Effect.vEffData.removeElementAt(0);
					}
					goto IL_8EFE;
				}
				goto IL_8EFE;
			case -21:
			{
				GameCanvas.debug("SA60", 2);
				short num177 = msg.reader().readShort();
				for (int num178 = 0; num178 < GameScr.vItemMap.size(); num178++)
				{
					if (((ItemMap)GameScr.vItemMap.elementAt(num178)).itemMapID == (int)num177)
					{
						GameScr.vItemMap.removeElementAt(num178);
						break;
					}
				}
				goto IL_8EFE;
			}
			case -20:
			{
				GameCanvas.debug("SA61", 2);
				global::Char.myCharz().itemFocus = null;
				short num179 = msg.reader().readShort();
				int num180 = 0;
				while (num180 < GameScr.vItemMap.size())
				{
					ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(num180);
					if (itemMap.itemMapID == (int)num179)
					{
						itemMap.setPoint(global::Char.myCharz().cx, global::Char.myCharz().cy - 10);
						string text14 = msg.reader().readUTF();
						i = 0;
						try
						{
							i = (int)msg.reader().readShort();
							if (itemMap.template.type == 9)
							{
								i = (int)msg.reader().readShort();
								global::Char.myCharz().xu += (long)i;
								global::Char.myCharz().xuStr = Res.formatNumber(global::Char.myCharz().xu);
							}
							else if (itemMap.template.type == 10)
							{
								i = (int)msg.reader().readShort();
								global::Char.myCharz().luong += i;
								global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
							}
							else if (itemMap.template.type == 34)
							{
								i = (int)msg.reader().readShort();
								global::Char.myCharz().luongKhoa += i;
								global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
							}
						}
						catch (Exception)
						{
						}
						if (text14.Equals(string.Empty))
						{
							if (itemMap.template.type == 9)
							{
								GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i.ToString(), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.YELLOW);
								SoundMn.gI().getItem();
							}
							else if (itemMap.template.type == 10)
							{
								GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i.ToString(), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.GREEN);
								SoundMn.gI().getItem();
							}
							else if (itemMap.template.type == 34)
							{
								GameScr.startFlyText(((i >= 0) ? "+" : string.Empty) + i.ToString(), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -2, mFont.RED);
								SoundMn.gI().getItem();
							}
							else
							{
								GameScr.info1.addInfo(mResources.you_receive + " " + ((i <= 0) ? string.Empty : (i.ToString() + " ")) + itemMap.template.name, 0);
								SoundMn.gI().getItem();
							}
							if (i > 0 && global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 4683)
							{
								ServerEffect.addServerEffect(55, global::Char.myCharz().petFollow.cmx, global::Char.myCharz().petFollow.cmy, 1);
								ServerEffect.addServerEffect(55, global::Char.myCharz().cx, global::Char.myCharz().cy, 1);
								break;
							}
							break;
						}
						else
						{
							if (text14.Length == 1)
							{
								Cout.LogError3("strInf.Length =1:  " + text14);
								break;
							}
							GameScr.info1.addInfo(text14, 0);
							break;
						}
					}
					else
					{
						num180++;
					}
				}
				goto IL_8EFE;
			}
			case -19:
			{
				GameCanvas.debug("SA62", 2);
				short num181 = msg.reader().readShort();
				@char = GameScr.findCharInMap(msg.reader().readInt());
				int num182 = 0;
				while (num182 < GameScr.vItemMap.size())
				{
					ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(num182);
					if (itemMap2.itemMapID == (int)num181)
					{
						if (@char == null)
						{
							return;
						}
						itemMap2.setPoint(@char.cx, @char.cy - 10);
						if (itemMap2.x < @char.cx)
						{
							@char.cdir = -1;
							break;
						}
						if (itemMap2.x > @char.cx)
						{
							@char.cdir = 1;
							break;
						}
						break;
					}
					else
					{
						num182++;
					}
				}
				goto IL_8EFE;
			}
			case -18:
			{
				GameCanvas.debug("SA63", 2);
				int num183 = (int)msg.reader().readByte();
				GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), global::Char.myCharz().arrItemBag[num183].template.id, global::Char.myCharz().cx, global::Char.myCharz().cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
				global::Char.myCharz().arrItemBag[num183] = null;
				goto IL_8EFE;
			}
			case -14:
				GameCanvas.debug("SA64", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				GameScr.vItemMap.addElement(new ItemMap(msg.reader().readShort(), msg.reader().readShort(), @char.cx, @char.cy, (int)msg.reader().readShort(), (int)msg.reader().readShort()));
				goto IL_8EFE;
			case -4:
			{
				GameCanvas.debug("SA76", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				GameCanvas.debug("SA76v1", 2);
				if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
				{
					@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 0);
				}
				else
				{
					@char.setSkillPaint(GameScr.sks[(int)msg.reader().readUnsignedByte()], 1);
				}
				GameCanvas.debug("SA76v2", 2);
				@char.attMobs = new Mob[(int)msg.reader().readByte()];
				for (int num184 = 0; num184 < @char.attMobs.Length; num184++)
				{
					Mob mob5 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte());
					@char.attMobs[num184] = mob5;
					if (num184 == 0)
					{
						if (@char.cx <= mob5.x)
						{
							@char.cdir = 1;
						}
						else
						{
							@char.cdir = -1;
						}
					}
				}
				GameCanvas.debug("SA76v3", 2);
				@char.charFocus = null;
				@char.mobFocus = @char.attMobs[0];
				global::Char[] array13 = new global::Char[10];
				i = 0;
				try
				{
					for (i = 0; i < array13.Length; i++)
					{
						int num185 = msg.reader().readInt();
						global::Char char9 = (array13[i] = ((num185 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num185) : global::Char.myCharz()));
						if (i == 0)
						{
							if (@char.cx <= char9.cx)
							{
								@char.cdir = 1;
							}
							else
							{
								@char.cdir = -1;
							}
						}
					}
				}
				catch (Exception ex2)
				{
					Cout.println("Loi PLAYER_ATTACK_N_P " + ex2.ToString());
				}
				GameCanvas.debug("SA76v4", 2);
				if (i > 0)
				{
					@char.attChars = new global::Char[i];
					for (i = 0; i < @char.attChars.Length; i++)
					{
						@char.attChars[i] = array13[i];
					}
					@char.charFocus = @char.attChars[0];
					@char.mobFocus = null;
				}
				GameCanvas.debug("SA76v5", 2);
				goto IL_8EFE;
			}
			case 0:
				this.readLogin(msg);
				goto IL_8EFE;
			case 1:
			{
				bool flag13 = msg.reader().readBool();
				Res.outz("isRes= " + flag13.ToString());
				if (!flag13)
				{
					GameCanvas.startOKDlg(msg.reader().readUTF());
					goto IL_8EFE;
				}
				GameCanvas.loginScr.isLogin2 = false;
				Rms.saveRMSString("userAo" + ServerListScreen.ipSelect.ToString(), string.Empty);
				GameCanvas.endDlg();
				GameCanvas.loginScr.doLogin();
				goto IL_8EFE;
			}
			case 2:
				global::Char.isLoadingMap = false;
				LoginScr.isLoggingIn = false;
				if (!GameScr.isLoadAllData)
				{
					GameScr.gI().initSelectChar();
				}
				BgItem.clearHashTable();
				GameCanvas.endDlg();
				CreateCharScr.isCreateChar = true;
				CreateCharScr.gI().switchToMe();
				goto IL_8EFE;
			case 6:
				GameCanvas.debug("SA70", 2);
				global::Char.myCharz().xu = msg.reader().readLong();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().luongKhoa = msg.reader().readInt();
				global::Char.myCharz().xuStr = Res.formatNumber(global::Char.myCharz().xu);
				global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
				global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
				GameCanvas.endDlg();
				goto IL_8EFE;
			case 7:
			{
				sbyte b64 = msg.reader().readByte();
				short num186 = msg.reader().readShort();
				string text15 = msg.reader().readUTF();
				GameCanvas.panel.saleRequest(b64, text15, num186);
				goto IL_8EFE;
			}
			case 11:
			{
				GameCanvas.debug("SA9", 2);
				int num187 = (int)msg.reader().readByte();
				sbyte b65 = msg.reader().readByte();
				if (b65 != 0)
				{
					Mob.arrMobTemplate[num187].data.readDataNewBoss(NinjaUtil.readByteArray(msg), b65);
				}
				else
				{
					Mob.arrMobTemplate[num187].data.readData(NinjaUtil.readByteArray(msg));
				}
				for (int num188 = 0; num188 < GameScr.vMob.size(); num188++)
				{
					Mob mob2 = (Mob)GameScr.vMob.elementAt(num188);
					if (mob2.templateId == num187)
					{
						mob2.w = Mob.arrMobTemplate[num187].data.width;
						mob2.h = Mob.arrMobTemplate[num187].data.height;
					}
				}
				sbyte[] array14 = NinjaUtil.readByteArray(msg);
				Image image2 = Image.createImage(array14, 0, array14.Length);
				Mob.arrMobTemplate[num187].data.img = image2;
				int num189 = (int)msg.reader().readByte();
				Mob.arrMobTemplate[num187].data.typeData = num189;
				if (num189 == 1 || num189 == 2)
				{
					this.readFrameBoss(msg, num187);
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case 20:
				this.phuban_Info(msg);
				goto IL_8EFE;
			case 24:
				this.read_opt(msg);
				goto IL_8EFE;
			case 27:
			{
				myVector = new MyVector();
				msg.reader().readUTF();
				int num190 = (int)msg.reader().readByte();
				for (int num191 = 0; num191 < num190; num191++)
				{
					string text16 = msg.reader().readUTF();
					short num192 = msg.reader().readShort();
					myVector.addElement(new Command(text16, GameCanvas.instance, 88819, num192));
				}
				GameCanvas.menu.startWithoutCloseButton(myVector, 3);
				goto IL_8EFE;
			}
			case 29:
				GameCanvas.debug("SA58", 2);
				GameScr.gI().openUIZone(msg);
				goto IL_8EFE;
			case 32:
			{
				GameCanvas.debug("SA68", 2);
				int num193 = (int)msg.reader().readShort();
				for (int num194 = 0; num194 < GameScr.vNpc.size(); num194++)
				{
					Npc npc2 = (Npc)GameScr.vNpc.elementAt(num194);
					if (npc2.template.npcTemplateId == num193 && npc2.Equals(global::Char.myCharz().npcFocus))
					{
						string text17 = msg.reader().readUTF();
						string[] array15 = new string[(int)msg.reader().readByte()];
						for (int num195 = 0; num195 < array15.Length; num195++)
						{
							array15[num195] = msg.reader().readUTF();
						}
						GameScr.gI().createMenu(array15, npc2);
						ChatPopup.addChatPopup(text17, 100000, npc2);
						return;
					}
				}
				Npc npc3 = new Npc(num193, 0, -100, 100, num193, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				Res.outz((global::Char.myCharz().npcFocus == null) ? "null" : "!null");
				string text18 = msg.reader().readUTF();
				string[] array16 = new string[(int)msg.reader().readByte()];
				for (int num196 = 0; num196 < array16.Length; num196++)
				{
					array16[num196] = msg.reader().readUTF();
				}
				try
				{
					npc3.avatar = (int)msg.reader().readShort();
				}
				catch (Exception)
				{
				}
				Res.outz((global::Char.myCharz().npcFocus == null) ? "null" : "!null");
				GameScr.gI().createMenu(array16, npc3);
				ChatPopup.addChatPopup(text18, 100000, npc3);
				goto IL_8EFE;
			}
			case 33:
			{
				GameCanvas.debug("SA51", 2);
				InfoDlg.hide();
				GameCanvas.clearKeyHold();
				GameCanvas.clearKeyPressed();
				myVector = new MyVector();
				try
				{
					for (;;)
					{
						myVector.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 88822, null));
					}
				}
				catch (Exception ex3)
				{
					Cout.println("Loi OPEN_UI_MENU " + ex3.ToString());
				}
				if (global::Char.myCharz().npcFocus == null)
				{
					return;
				}
				for (int num197 = 0; num197 < global::Char.myCharz().npcFocus.template.menu.Length; num197++)
				{
					string[] array17 = global::Char.myCharz().npcFocus.template.menu[num197];
					myVector.addElement(new Command(array17[0], GameCanvas.instance, 88820, array17));
				}
				GameCanvas.menu.startAt(myVector, 3);
				goto IL_8EFE;
			}
			case 38:
			{
				GameCanvas.debug("SA67", 2);
				InfoDlg.hide();
				int num198 = (int)msg.reader().readShort();
				Res.outz("OPEN_UI_SAY ID= " + num198.ToString());
				string text19 = Res.changeString(msg.reader().readUTF());
				for (int num199 = 0; num199 < GameScr.vNpc.size(); num199++)
				{
					Npc npc4 = (Npc)GameScr.vNpc.elementAt(num199);
					Res.outz("npc id= " + npc4.template.npcTemplateId.ToString());
					if (npc4.template.npcTemplateId == num198)
					{
						ChatPopup.addChatPopupMultiLine(text19, 100000, npc4);
						GameCanvas.panel.hideNow();
						return;
					}
				}
				Npc npc5 = new Npc(num198, 0, 0, 0, num198, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				if (npc5.template.npcTemplateId == 5)
				{
					npc5.charID = 5;
				}
				try
				{
					npc5.avatar = (int)msg.reader().readShort();
				}
				catch (Exception)
				{
				}
				ChatPopup.addChatPopupMultiLine(text19, 100000, npc5);
				GameCanvas.panel.hideNow();
				goto IL_8EFE;
			}
			case 39:
				GameCanvas.debug("SA49", 2);
				GameScr.gI().typeTradeOrder = 2;
				if (GameScr.gI().typeTrade >= 2 && GameScr.gI().typeTradeOrder >= 2)
				{
					InfoDlg.showWait();
					goto IL_8EFE;
				}
				goto IL_8EFE;
			case 40:
			{
				GameCanvas.debug("SA52", 2);
				GameCanvas.taskTick = 150;
				short num200 = msg.reader().readShort();
				sbyte b66 = msg.reader().readByte();
				string text20 = Res.changeString(msg.reader().readUTF());
				string text21 = Res.changeString(msg.reader().readUTF());
				string[] array18 = new string[(int)msg.reader().readByte()];
				string[] array19 = new string[array18.Length];
				GameScr.tasks = new int[array18.Length];
				GameScr.mapTasks = new int[array18.Length];
				short[] array20 = new short[array18.Length];
				short num201 = -1;
				for (int num202 = 0; num202 < array18.Length; num202++)
				{
					string text22 = Res.changeString(msg.reader().readUTF());
					GameScr.tasks[num202] = (int)msg.reader().readByte();
					GameScr.mapTasks[num202] = (int)msg.reader().readShort();
					string text23 = Res.changeString(msg.reader().readUTF());
					array20[num202] = -1;
					array18[num202] = text22;
					if (!text23.Equals(string.Empty))
					{
						array19[num202] = text23;
					}
				}
				try
				{
					num201 = msg.reader().readShort();
					for (int num203 = 0; num203 < array18.Length; num203++)
					{
						array20[num203] = msg.reader().readShort();
					}
				}
				catch (Exception ex4)
				{
					Cout.println("Loi TASK_GET " + ex4.ToString());
				}
				global::Char.myCharz().taskMaint = new Task(num200, b66, text20, text21, array18, array20, num201, array19);
				if (global::Char.myCharz().npcFocus != null)
				{
					Npc.clearEffTask();
				}
				global::Char.taskAction(true);
				goto IL_8EFE;
			}
			case 41:
				GameCanvas.debug("SA53", 2);
				GameCanvas.taskTick = 100;
				Res.outz("TASK NEXT");
				global::Char.myCharz().taskMaint.index++;
				global::Char.myCharz().taskMaint.count = 0;
				Npc.clearEffTask();
				global::Char.taskAction(true);
				goto IL_8EFE;
			case 43:
				GameCanvas.taskTick = 50;
				GameCanvas.debug("SA55", 2);
				global::Char.myCharz().taskMaint.count = msg.reader().readShort();
				if (global::Char.myCharz().npcFocus != null)
				{
					Npc.clearEffTask();
				}
				try
				{
					short num204 = msg.reader().readShort();
					short num205 = msg.reader().readShort();
					global::Char.myCharz().x_hint = num204;
					global::Char.myCharz().y_hint = num205;
					goto IL_8EFE;
				}
				catch (Exception)
				{
					goto IL_8EFE;
				}
				goto IL_7426;
			case 46:
				GameCanvas.debug("SA5", 2);
				Cout.LogWarning("Controler RESET_POINT  " + global::Char.ischangingMap.ToString());
				global::Char.isLockKey = false;
				global::Char.myCharz().setResetPoint((int)msg.reader().readShort(), (int)msg.reader().readShort());
				goto IL_8EFE;
			case 47:
				GameCanvas.debug("SA4", 2);
				GameScr.gI().resetButton();
				goto IL_8EFE;
			case 50:
			{
				sbyte b67 = msg.reader().readByte();
				Panel.vGameInfo.removeAllElements();
				for (int num206 = 0; num206 < (int)b67; num206++)
				{
					GameInfo gameInfo = new GameInfo();
					gameInfo.id = msg.reader().readShort();
					gameInfo.main = msg.reader().readUTF();
					gameInfo.content = msg.reader().readUTF();
					Panel.vGameInfo.addElement(gameInfo);
					gameInfo.hasRead = Rms.loadRMSInt(gameInfo.id.ToString() + string.Empty) != -1;
				}
				goto IL_8EFE;
			}
			case 54:
			{
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				int num207 = (int)msg.reader().readUnsignedByte();
				if ((TileMap.tileTypeAtPixel(@char.cx, @char.cy) & 2) == 2)
				{
					@char.setSkillPaint(GameScr.sks[num207], 0);
				}
				else
				{
					@char.setSkillPaint(GameScr.sks[num207], 1);
				}
				Mob[] array21 = new Mob[10];
				i = 0;
				try
				{
					for (i = 0; i < array21.Length; i++)
					{
						Mob mob6 = (array21[i] = (Mob)GameScr.vMob.elementAt((int)msg.reader().readByte()));
						if (i == 0)
						{
							if (@char.cx <= mob6.x)
							{
								@char.cdir = 1;
							}
							else
							{
								@char.cdir = -1;
							}
						}
					}
				}
				catch (Exception)
				{
				}
				if (i > 0)
				{
					@char.attMobs = new Mob[i];
					for (i = 0; i < @char.attMobs.Length; i++)
					{
						@char.attMobs[i] = array21[i];
					}
					@char.charFocus = null;
					@char.mobFocus = @char.attMobs[0];
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case 56:
			{
				GameCanvas.debug("SXX6", 2);
				@char = null;
				int num208 = msg.reader().readInt();
				if (num208 == global::Char.myCharz().charID)
				{
					bool flag14 = false;
					@char = global::Char.myCharz();
					@char.cHP = msg.readInt3Byte();
					int num209 = msg.readInt3Byte();
					Res.outz("dame hit = " + num209.ToString());
					if (num209 != 0)
					{
						@char.doInjure();
					}
					int num210 = 0;
					try
					{
						flag14 = msg.reader().readBoolean();
						sbyte b68 = msg.reader().readByte();
						if (b68 != -1)
						{
							Res.outz("hit eff= " + b68.ToString());
							EffecMn.addEff(new Effect((int)b68, @char.cx, @char.cy, 3, 1, -1));
						}
					}
					catch (Exception)
					{
					}
					num209 += num210;
					if (global::Char.myCharz().cTypePk == 4)
					{
						goto IL_8EFE;
					}
					if (num209 == 0)
					{
						GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS_ME);
						goto IL_8EFE;
					}
					GameScr.startFlyText("-" + num209.ToString(), @char.cx, @char.cy - @char.ch, 0, -3, flag14 ? mFont.FATAL : mFont.RED);
					goto IL_8EFE;
				}
				else
				{
					@char = GameScr.findCharInMap(num208);
					if (@char == null)
					{
						return;
					}
					@char.cHP = msg.readInt3Byte();
					bool flag15 = false;
					int num211 = msg.readInt3Byte();
					if (num211 != 0)
					{
						@char.doInjure();
					}
					int num212 = 0;
					try
					{
						flag15 = msg.reader().readBoolean();
						sbyte b69 = msg.reader().readByte();
						if (b69 != -1)
						{
							Res.outz("hit eff= " + b69.ToString());
							EffecMn.addEff(new Effect((int)b69, @char.cx, @char.cy, 3, 1, -1));
						}
					}
					catch (Exception)
					{
					}
					num211 += num212;
					if (@char.cTypePk == 4)
					{
						goto IL_8EFE;
					}
					if (num211 == 0)
					{
						GameScr.startFlyText(mResources.miss, @char.cx, @char.cy - @char.ch, 0, -3, mFont.MISS);
						goto IL_8EFE;
					}
					GameScr.startFlyText("-" + num211.ToString(), @char.cx, @char.cy - @char.ch, 0, -3, flag15 ? mFont.FATAL : mFont.ORANGE);
					goto IL_8EFE;
				}
				break;
			}
			case 57:
			{
				GameCanvas.debug("SZ6", 2);
				MyVector myVector2 = new MyVector();
				myVector2.addElement(new Command(msg.reader().readUTF(), GameCanvas.instance, 88817, null));
				GameCanvas.menu.startAt(myVector2, 3);
				goto IL_8EFE;
			}
			case 58:
			{
				GameCanvas.debug("SZ7", 2);
				int num213 = msg.reader().readInt();
				global::Char char10 = ((num213 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num213) : global::Char.myCharz());
				char10.moveFast = new short[3];
				char10.moveFast[0] = 0;
				short num214 = msg.reader().readShort();
				short num215 = msg.reader().readShort();
				char10.moveFast[1] = num214;
				char10.moveFast[2] = num215;
				try
				{
					num213 = msg.reader().readInt();
					global::Char char11 = ((num213 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num213) : global::Char.myCharz());
					char11.cx = (int)num214;
					char11.cy = (int)num215;
					goto IL_8EFE;
				}
				catch (Exception ex5)
				{
					Cout.println("Loi MOVE_FAST " + ex5.ToString());
					goto IL_8EFE;
				}
				break;
			}
			case 62:
				GameCanvas.debug("SZ3", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char != null)
				{
					@char.killCharId = global::Char.myCharz().charID;
					global::Char.myCharz().npcFocus = null;
					global::Char.myCharz().mobFocus = null;
					global::Char.myCharz().itemFocus = null;
					global::Char.myCharz().charFocus = @char;
					global::Char.isManualFocus = true;
					GameScr.info1.addInfo(@char.cName + mResources.CUU_SAT, 0);
					goto IL_8EFE;
				}
				goto IL_8EFE;
			case 63:
				GameCanvas.debug("SZ4", 2);
				global::Char.myCharz().killCharId = msg.reader().readInt();
				global::Char.myCharz().npcFocus = null;
				global::Char.myCharz().mobFocus = null;
				global::Char.myCharz().itemFocus = null;
				global::Char.myCharz().charFocus = GameScr.findCharInMap(global::Char.myCharz().killCharId);
				global::Char.isManualFocus = true;
				goto IL_8EFE;
			case 64:
				GameCanvas.debug("SZ5", 2);
				@char = global::Char.myCharz();
				try
				{
					@char = GameScr.findCharInMap(msg.reader().readInt());
				}
				catch (Exception ex6)
				{
					Cout.println("Loi CLEAR_CUU_SAT " + ex6.ToString());
				}
				@char.killCharId = -9999;
				goto IL_8EFE;
			case 65:
			{
				sbyte b70 = msg.reader().readSByte();
				string text24 = msg.reader().readUTF();
				short num216 = msg.reader().readShort();
				if (!ItemTime.isExistMessage((int)b70))
				{
					ItemTime itemTime = new ItemTime();
					itemTime.initTimeText(b70, text24, (int)num216);
					GameScr.textTime.addElement(itemTime);
					goto IL_8EFE;
				}
				if (num216 != 0)
				{
					ItemTime.getMessageById((int)b70).initTimeText(b70, text24, (int)num216);
					goto IL_8EFE;
				}
				GameScr.textTime.removeElement(ItemTime.getMessageById((int)b70));
				goto IL_8EFE;
			}
			case 66:
				this.readGetImgByName(msg);
				goto IL_8EFE;
			case 68:
			{
				Res.outz("ADD ITEM TO MAP --------------------------------------");
				GameCanvas.debug("SA6333", 2);
				short num217 = msg.reader().readShort();
				short num218 = msg.reader().readShort();
				int num219 = (int)msg.reader().readShort();
				int num220 = (int)msg.reader().readShort();
				int num221 = msg.reader().readInt();
				short num222 = 0;
				if (num221 == -2)
				{
					num222 = msg.reader().readShort();
				}
				ItemMap itemMap3 = new ItemMap(num221, num217, num218, num219, num220, num222);
				bool flag16 = false;
				for (int num223 = 0; num223 < GameScr.vItemMap.size(); num223++)
				{
					if (((ItemMap)GameScr.vItemMap.elementAt(num223)).itemMapID == itemMap3.itemMapID)
					{
						flag16 = true;
						break;
					}
				}
				if (!flag16)
				{
					GameScr.vItemMap.addElement(itemMap3);
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case 69:
				SoundMn.IsDelAcc = msg.reader().readByte() != 0;
				goto IL_8EFE;
			case 81:
				GameCanvas.debug("SXX4", 2);
				((Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte())).isDisable = msg.reader().readBool();
				goto IL_8EFE;
			case 82:
				GameCanvas.debug("SXX5", 2);
				((Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte())).isDontMove = msg.reader().readBool();
				goto IL_8EFE;
			case 83:
			{
				GameCanvas.debug("SXX8", 2);
				int num224 = msg.reader().readInt();
				@char = ((num224 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num224) : global::Char.myCharz());
				if (@char == null)
				{
					return;
				}
				Mob mob7 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				if (@char.mobMe != null)
				{
					@char.mobMe.attackOtherMob(mob7);
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case 84:
			{
				int num225 = msg.reader().readInt();
				if (num225 == global::Char.myCharz().charID)
				{
					@char = global::Char.myCharz();
				}
				else
				{
					@char = GameScr.findCharInMap(num225);
					if (@char == null)
					{
						return;
					}
				}
				@char.cHP = @char.cHPFull;
				@char.cMP = @char.cMPFull;
				@char.cx = (int)msg.reader().readShort();
				@char.cy = (int)msg.reader().readShort();
				@char.liveFromDead();
				goto IL_8EFE;
			}
			case 85:
				GameCanvas.debug("SXX5", 2);
				((Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte())).isFire = msg.reader().readBool();
				goto IL_8EFE;
			case 86:
			{
				GameCanvas.debug("SXX5", 2);
				Mob mob8 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				mob8.isIce = msg.reader().readBool();
				if (!mob8.isIce)
				{
					ServerEffect.addServerEffect(77, mob8.x, mob8.y - 9, 1);
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case 87:
				GameCanvas.debug("SXX5", 2);
				((Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte())).isWind = msg.reader().readBool();
				goto IL_8EFE;
			case 88:
				break;
			case 90:
				goto IL_7426;
			case 92:
			{
				if (GameCanvas.currentScreen == GameScr.instance)
				{
					GameCanvas.endDlg();
				}
				string text25 = msg.reader().readUTF();
				string text26 = Res.changeString(msg.reader().readUTF());
				string text27 = string.Empty;
				global::Char char12 = null;
				sbyte b71 = 0;
				if (!text25.Equals(string.Empty))
				{
					char12 = new global::Char();
					char12.charID = msg.reader().readInt();
					char12.head = (int)msg.reader().readShort();
					char12.headICON = (int)msg.reader().readShort();
					char12.body = (int)msg.reader().readShort();
					char12.bag = (int)msg.reader().readShort();
					char12.leg = (int)msg.reader().readShort();
					b71 = msg.reader().readByte();
					char12.cName = text25;
				}
				text27 += text26;
				InfoDlg.hide();
				if (text25.Equals(string.Empty))
				{
					GameScr.info1.addInfo(text27, 0);
					goto IL_8EFE;
				}
				GameScr.info2.addInfoWithChar(text27, char12, b71 == 0);
				if (GameCanvas.panel.isShow && GameCanvas.panel.type == 8)
				{
					GameCanvas.panel.initLogMessage();
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			case 94:
				GameCanvas.debug("SA3", 2);
				GameScr.info1.addInfo(msg.reader().readUTF(), 0);
				goto IL_8EFE;
			case 112:
			{
				sbyte b72 = msg.reader().readByte();
				Res.outz("spec type= " + b72.ToString());
				if (b72 == 0)
				{
					Panel.spearcialImage = msg.reader().readShort();
					Panel.specialInfo = msg.reader().readUTF();
					goto IL_8EFE;
				}
				if (b72 == 1)
				{
					sbyte b73 = msg.reader().readByte();
					global::Char.myCharz().infoSpeacialSkill = new string[(int)b73][];
					global::Char.myCharz().imgSpeacialSkill = new short[(int)b73][];
					GameCanvas.panel.speacialTabName = new string[(int)b73][];
					for (int num226 = 0; num226 < (int)b73; num226++)
					{
						GameCanvas.panel.speacialTabName[num226] = new string[2];
						string[] array22 = Res.split(msg.reader().readUTF(), "\n", 0);
						if (array22.Length == 2)
						{
							GameCanvas.panel.speacialTabName[num226] = array22;
						}
						if (array22.Length == 1)
						{
							GameCanvas.panel.speacialTabName[num226][0] = array22[0];
							GameCanvas.panel.speacialTabName[num226][1] = string.Empty;
						}
						int num227 = (int)msg.reader().readByte();
						global::Char.myCharz().infoSpeacialSkill[num226] = new string[num227];
						global::Char.myCharz().imgSpeacialSkill[num226] = new short[num227];
						for (int num228 = 0; num228 < num227; num228++)
						{
							global::Char.myCharz().imgSpeacialSkill[num226][num228] = msg.reader().readShort();
							global::Char.myCharz().infoSpeacialSkill[num226][num228] = msg.reader().readUTF();
						}
					}
					GameCanvas.panel.tabName[25] = GameCanvas.panel.speacialTabName;
					GameCanvas.panel.setTypeSpeacialSkill();
					GameCanvas.panel.show();
					goto IL_8EFE;
				}
				goto IL_8EFE;
			}
			}
			string text28 = msg.reader().readUTF();
			short num229 = msg.reader().readShort();
			GameCanvas.inputDlg.show(text28, new Command(mResources.ACCEPT, GameCanvas.instance, 88818, num229), TField.INPUT_TYPE_ANY);
			goto IL_8EFE;
			IL_7426:
			GameCanvas.debug("SA577", 2);
			this.requestItemPlayer(msg);
			IL_8EFE:
			sbyte command2 = msg.command;
			switch (command2)
			{
			case -17:
				GameCanvas.debug("SA88", 2);
				global::Char.myCharz().meDead = true;
				global::Char.myCharz().cPk = msg.reader().readByte();
				global::Char.myCharz().startDie(msg.reader().readShort(), msg.reader().readShort());
				try
				{
					global::Char.myCharz().cPower = msg.reader().readLong();
					global::Char.myCharz().applyCharLevelPercent();
				}
				catch (Exception)
				{
					Cout.println("Loi tai ME_DIE " + msg.command.ToString());
				}
				global::Char.myCharz().countKill = 0;
				goto IL_A990;
			case -16:
				GameCanvas.debug("SA90", 2);
				if (global::Char.myCharz().wdx != 0 || global::Char.myCharz().wdy != 0)
				{
					global::Char.myCharz().cx = (int)global::Char.myCharz().wdx;
					global::Char.myCharz().cy = (int)global::Char.myCharz().wdy;
					global::Char.myCharz().wdx = (global::Char.myCharz().wdy = 0);
				}
				global::Char.myCharz().liveFromDead();
				global::Char.myCharz().isLockMove = false;
				global::Char.myCharz().meDead = false;
				goto IL_A990;
			default:
				switch (command2)
				{
				case 95:
				{
					GameCanvas.debug("SA77", 22);
					int num230 = msg.reader().readInt();
					global::Char.myCharz().xu += (long)num230;
					global::Char.myCharz().xuStr = Res.formatNumber(global::Char.myCharz().xu);
					GameScr.startFlyText((num230 <= 0) ? (string.Empty + num230.ToString()) : ("+" + num230.ToString()), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
					goto IL_A990;
				}
				case 96:
					GameCanvas.debug("SA77a", 22);
					global::Char.myCharz().taskOrders.addElement(new TaskOrder(msg.reader().readByte(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readUTF(), msg.reader().readUTF(), msg.reader().readByte(), msg.reader().readByte()));
					goto IL_A990;
				case 97:
				{
					sbyte b74 = msg.reader().readByte();
					for (int num231 = 0; num231 < global::Char.myCharz().taskOrders.size(); num231++)
					{
						TaskOrder taskOrder = (TaskOrder)global::Char.myCharz().taskOrders.elementAt(num231);
						if (taskOrder.taskId == (int)b74)
						{
							taskOrder.count = (int)msg.reader().readShort();
							break;
						}
					}
					goto IL_A990;
				}
				default:
					if (command2 != -75)
					{
						if (command2 == -73)
						{
							sbyte b75 = msg.reader().readByte();
							int num232 = 0;
							while (num232 < GameScr.vNpc.size())
							{
								Npc npc6 = (Npc)GameScr.vNpc.elementAt(num232);
								if (npc6.template.npcTemplateId == (int)b75)
								{
									if (msg.reader().readByte() == 0)
									{
										npc6.isHide = true;
										break;
									}
									npc6.isHide = false;
									break;
								}
								else
								{
									num232++;
								}
							}
							goto IL_A990;
						}
						if (command2 == 18)
						{
							sbyte b76 = msg.reader().readByte();
							for (int num233 = 0; num233 < (int)b76; num233++)
							{
								int num234 = msg.reader().readInt();
								int num235 = (int)msg.reader().readShort();
								int num236 = (int)msg.reader().readShort();
								int num237 = msg.readInt3Byte();
								global::Char char13 = GameScr.findCharInMap(num234);
								if (char13 != null)
								{
									char13.cx = num235;
									char13.cy = num236;
									char13.cHP = (char13.cHPShow = num237);
									char13.lastUpdateTime = mSystem.currentTimeMillis();
								}
							}
							goto IL_A990;
						}
						if (command2 == 19)
						{
							global::Char.myCharz().countKill = (int)msg.reader().readUnsignedShort();
							global::Char.myCharz().countKillMax = (int)msg.reader().readUnsignedShort();
							goto IL_A990;
						}
						if (command2 != 44)
						{
							if (command2 != 45)
							{
								if (command2 == 66)
								{
									Res.outz("ME DIE XP DOWN NOT IMPLEMENT YET!!!!!!!!!!!!!!!!!!!!!!!!!!");
									goto IL_A990;
								}
								if (command2 != 74)
								{
									goto IL_A990;
								}
								GameCanvas.debug("SA85", 2);
								Mob mob9 = null;
								try
								{
									mob9 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
								}
								catch (Exception)
								{
									Cout.println("Loi tai NPC CHANGE " + msg.command.ToString());
								}
								if (mob9 == null || mob9.status == 0 || mob9.status == 0)
								{
									goto IL_A990;
								}
								mob9.status = 0;
								ServerEffect.addServerEffect(60, mob9.x, mob9.y, 1);
								ItemMap itemMap4 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob9.x, mob9.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
								GameScr.vItemMap.addElement(itemMap4);
								if (Res.abs(itemMap4.y - global::Char.myCharz().cy) < 24 && Res.abs(itemMap4.x - global::Char.myCharz().cx) < 24)
								{
									global::Char.myCharz().charFocus = null;
									goto IL_A990;
								}
								goto IL_A990;
							}
							else
							{
								GameCanvas.debug("SA84", 2);
								Mob mob10 = null;
								try
								{
									mob10 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
								}
								catch (Exception ex7)
								{
									Cout.println("Loi tai NPC_MISS  " + ex7.ToString());
								}
								if (mob10 != null)
								{
									mob10.hp = msg.reader().readInt();
									mob10.updateHp_bar();
									GameScr.startFlyText(mResources.miss, mob10.x, mob10.y - mob10.h, 0, -2, mFont.MISS);
									goto IL_A990;
								}
								goto IL_A990;
							}
						}
						else
						{
							GameCanvas.debug("SA91", 2);
							int num238 = msg.reader().readInt();
							string text29 = msg.reader().readUTF();
							Res.outz("user id= " + num238.ToString() + " text= " + text29);
							@char = ((global::Char.myCharz().charID != num238) ? GameScr.findCharInMap(num238) : global::Char.myCharz());
							if (@char == null)
							{
								return;
							}
							@char.addInfo(text29);
							goto IL_A990;
						}
					}
					else
					{
						Mob mob11 = null;
						try
						{
							mob11 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
						}
						catch (Exception)
						{
						}
						if (mob11 == null)
						{
							goto IL_A990;
						}
						mob11.levelBoss = msg.reader().readByte();
						if (mob11.levelBoss > 0)
						{
							mob11.typeSuperEff = Res.random(0, 3);
							goto IL_A990;
						}
						goto IL_A990;
					}
					break;
				}
				break;
			case -13:
			{
				GameCanvas.debug("SA82", 2);
				int num239 = (int)msg.reader().readUnsignedByte();
				if (num239 > GameScr.vMob.size() - 1 || num239 < 0)
				{
					return;
				}
				Mob mob12 = (Mob)GameScr.vMob.elementAt(num239);
				mob12.sys = (int)msg.reader().readByte();
				mob12.levelBoss = msg.reader().readByte();
				if (mob12.levelBoss != 0)
				{
					mob12.typeSuperEff = Res.random(0, 3);
				}
				mob12.x = mob12.xFirst;
				mob12.y = mob12.yFirst;
				mob12.status = 5;
				mob12.injureThenDie = false;
				mob12.hp = msg.reader().readInt();
				mob12.maxHp = mob12.hp;
				mob12.updateHp_bar();
				ServerEffect.addServerEffect(60, mob12.x, mob12.y, 1);
				goto IL_A990;
			}
			case -12:
			{
				Res.outz("SERVER SEND MOB DIE");
				GameCanvas.debug("SA85", 2);
				Mob mob13 = null;
				try
				{
					mob13 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception)
				{
					Cout.println("LOi tai NPC_DIE cmd " + msg.command.ToString());
				}
				if (mob13 == null || mob13.status == 0 || mob13.status == 0)
				{
					goto IL_A990;
				}
				mob13.startDie();
				try
				{
					int num240 = msg.readInt3Byte();
					if (msg.reader().readBool())
					{
						GameScr.startFlyText("-" + num240.ToString(), mob13.x, mob13.y - mob13.h, 0, -2, mFont.FATAL);
					}
					else
					{
						GameScr.startFlyText("-" + num240.ToString(), mob13.x, mob13.y - mob13.h, 0, -2, mFont.ORANGE);
					}
					sbyte b77 = msg.reader().readByte();
					for (int num241 = 0; num241 < (int)b77; num241++)
					{
						ItemMap itemMap5 = new ItemMap(msg.reader().readShort(), msg.reader().readShort(), mob13.x, mob13.y, (int)msg.reader().readShort(), (int)msg.reader().readShort());
						int num242 = (itemMap5.playerId = msg.reader().readInt());
						Res.outz("playerid= " + num242.ToString() + " my id= " + global::Char.myCharz().charID.ToString());
						GameScr.vItemMap.addElement(itemMap5);
						if (Res.abs(itemMap5.y - global::Char.myCharz().cy) < 24 && Res.abs(itemMap5.x - global::Char.myCharz().cx) < 24)
						{
							global::Char.myCharz().charFocus = null;
						}
					}
					goto IL_A990;
				}
				catch (Exception)
				{
					goto IL_A990;
				}
				break;
			}
			case -11:
				break;
			case -10:
			{
				GameCanvas.debug("SA87", 2);
				Mob mob14 = null;
				try
				{
					mob14 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception)
				{
				}
				GameCanvas.debug("SA87x1", 2);
				if (mob14 == null)
				{
					goto IL_A990;
				}
				GameCanvas.debug("SA87x2", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				GameCanvas.debug("SA87x3", 2);
				int num243 = msg.readInt3Byte();
				mob14.dame = @char.cHP - num243;
				@char.cHPNew = num243;
				GameCanvas.debug("SA87x4", 2);
				try
				{
					@char.cMP = msg.readInt3Byte();
				}
				catch (Exception)
				{
				}
				GameCanvas.debug("SA87x5", 2);
				if (mob14.isBusyAttackSomeOne)
				{
					@char.doInjure(mob14.dame, 0, false, true);
				}
				else
				{
					mob14.setAttack(@char);
				}
				GameCanvas.debug("SA87x6", 2);
				goto IL_A990;
			}
			case -9:
			{
				GameCanvas.debug("SA83", 2);
				Mob mob15 = null;
				try
				{
					mob15 = (Mob)GameScr.vMob.elementAt((int)msg.reader().readUnsignedByte());
				}
				catch (Exception)
				{
				}
				GameCanvas.debug("SA83v1", 2);
				if (mob15 != null)
				{
					mob15.hp = msg.readInt3Byte();
					mob15.updateHp_bar();
					int num244 = msg.readInt3Byte();
					if (num244 == 1)
					{
						return;
					}
					if (num244 > 1)
					{
						mob15.setInjure();
					}
					bool flag17 = false;
					try
					{
						flag17 = msg.reader().readBoolean();
					}
					catch (Exception)
					{
					}
					sbyte b78 = msg.reader().readByte();
					if (b78 != -1)
					{
						EffecMn.addEff(new Effect((int)b78, mob15.x, mob15.getY(), 3, 1, -1));
					}
					GameCanvas.debug("SA83v2", 2);
					if (flag17)
					{
						GameScr.startFlyText("-" + num244.ToString(), mob15.x, mob15.getY() - mob15.getH(), 0, -2, mFont.FATAL);
					}
					else if (num244 == 0)
					{
						mob15.x = mob15.xFirst;
						mob15.y = mob15.yFirst;
						GameScr.startFlyText(mResources.miss, mob15.x, mob15.getY() - mob15.getH(), 0, -2, mFont.MISS);
					}
					else if (num244 > 1)
					{
						GameScr.startFlyText("-" + num244.ToString(), mob15.x, mob15.getY() - mob15.getH(), 0, -2, mFont.ORANGE);
					}
				}
				GameCanvas.debug("SA83v3", 2);
				goto IL_A990;
			}
			case -8:
				GameCanvas.debug("SA89", 2);
				@char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				@char.cPk = msg.reader().readByte();
				@char.waitToDie(msg.reader().readShort(), msg.reader().readShort());
				goto IL_A990;
			case -7:
			{
				GameCanvas.debug("SA80", 2);
				int num245 = msg.reader().readInt();
				int num246 = 0;
				while (num246 < GameScr.vCharInMap.size())
				{
					global::Char char14 = null;
					try
					{
						char14 = (global::Char)GameScr.vCharInMap.elementAt(num246);
					}
					catch (Exception)
					{
						goto IL_9F10;
					}
					goto IL_9EA6;
					IL_9F10:
					num246++;
					continue;
					IL_9EA6:
					if (char14 != null && char14.charID == num245)
					{
						GameCanvas.debug("SA8x2y" + num246.ToString(), 2);
						char14.moveTo((int)msg.reader().readShort(), (int)msg.reader().readShort(), 0);
						char14.lastUpdateTime = mSystem.currentTimeMillis();
						break;
					}
					goto IL_9F10;
				}
				GameCanvas.debug("SA80x3", 2);
				goto IL_A990;
			}
			case -6:
			{
				GameCanvas.debug("SA81", 2);
				int num247 = msg.reader().readInt();
				for (int num248 = 0; num248 < GameScr.vCharInMap.size(); num248++)
				{
					global::Char char15 = (global::Char)GameScr.vCharInMap.elementAt(num248);
					if (char15 != null && char15.charID == num247)
					{
						if (!char15.isInvisiblez && !char15.isUsePlane)
						{
							ServerEffect.addServerEffect(60, char15.cx, char15.cy, 1);
						}
						if (!char15.isUsePlane)
						{
							GameScr.vCharInMap.removeElementAt(num248);
						}
						return;
					}
				}
				goto IL_A990;
			}
			case -5:
			{
				GameCanvas.debug("SA79", 2);
				int num249 = msg.reader().readInt();
				int num250 = msg.reader().readInt();
				global::Char char16;
				if (num250 != -100)
				{
					char16 = new global::Char();
					char16.charID = num249;
					char16.clanID = num250;
				}
				else
				{
					char16 = new Mabu();
					char16.charID = num249;
					char16.clanID = num250;
				}
				if (char16.clanID == -2)
				{
					char16.isCopy = true;
				}
				if (this.readCharInfo(char16, msg))
				{
					sbyte b79 = msg.reader().readByte();
					if (char16.cy <= 10 && b79 != 0 && b79 != 2)
					{
						Res.outz("nhân vật bay trên trời xuống x= " + char16.cx.ToString() + " y= " + char16.cy.ToString());
						Teleport teleport = new Teleport(char16.cx, char16.cy, char16.head, char16.cdir, 1, false, (b79 != 1) ? ((int)b79) : char16.cgender);
						teleport.id = char16.charID;
						char16.isTeleport = true;
						Teleport.addTeleport(teleport);
					}
					if (b79 == 2)
					{
						char16.show();
					}
					for (int num251 = 0; num251 < GameScr.vMob.size(); num251++)
					{
						Mob mob16 = (Mob)GameScr.vMob.elementAt(num251);
						if (mob16 != null && mob16.isMobMe && mob16.mobId == char16.charID)
						{
							Res.outz("co 1 con quai");
							char16.mobMe = mob16;
							char16.mobMe.x = char16.cx;
							char16.mobMe.y = char16.cy - 40;
							break;
						}
					}
					if (GameScr.findCharInMap(char16.charID) == null)
					{
						GameScr.vCharInMap.addElement(char16);
					}
					char16.isMonkey = msg.reader().readByte();
					short num252 = msg.reader().readShort();
					Res.outz("mount id= " + num252.ToString() + "+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++");
					if (num252 != -1)
					{
						char16.isHaveMount = true;
						if (num252 == 346 || num252 == 347 || num252 == 348)
						{
							char16.isMountVip = false;
						}
						else if (num252 == 349 || num252 == 350 || num252 == 351)
						{
							char16.isMountVip = true;
						}
						else if (num252 == 396)
						{
							char16.isEventMount = true;
						}
						else if (num252 == 532)
						{
							char16.isSpeacialMount = true;
						}
						else if (num252 >= global::Char.ID_NEW_MOUNT)
						{
							char16.idMount = num252;
						}
					}
					else
					{
						char16.isHaveMount = false;
					}
				}
				sbyte b80 = msg.reader().readByte();
				Res.outz("addplayer:   " + b80.ToString());
				char16.cFlag = b80;
				char16.isNhapThe = msg.reader().readByte() == 1;
				try
				{
					char16.idAuraEff = msg.reader().readShort();
					char16.idEff_Set_Item = (short)msg.reader().readSByte();
					char16.idHat = msg.reader().readShort();
					if (char16.bag >= 201 && char16.bag < 255)
					{
						char16.addEffChar(new Effect(char16.bag, char16, 2, -1, 10, 1)
						{
							typeEff = 5
						});
					}
					else
					{
						for (int num253 = 0; num253 < 54; num253++)
						{
							char16.removeEffChar(0, 201 + num253);
						}
					}
				}
				catch (Exception ex8)
				{
					Res.outz("cmd: -5 err: " + ex8.StackTrace);
				}
				GameScr.gI().getFlagImage(char16.charID, char16.cFlag);
				goto IL_A990;
			}
			case -3:
			{
				GameCanvas.debug("SA78", 2);
				sbyte b81 = msg.reader().readByte();
				int num254 = msg.reader().readInt();
				if (b81 == 0)
				{
					global::Char.myCharz().cPower += (long)num254;
				}
				if (b81 == 1)
				{
					global::Char.myCharz().cTiemNang += (long)num254;
				}
				if (b81 == 2)
				{
					global::Char.myCharz().cPower += (long)num254;
					global::Char.myCharz().cTiemNang += (long)num254;
				}
				global::Char.myCharz().applyCharLevelPercent();
				if (global::Char.myCharz().cTypePk == 3)
				{
					goto IL_A990;
				}
				GameScr.startFlyText(((num254 <= 0) ? string.Empty : "+") + num254.ToString(), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch, 0, -4, mFont.GREEN);
				if (num254 > 0 && global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5002)
				{
					ServerEffect.addServerEffect(55, global::Char.myCharz().petFollow.cmx, global::Char.myCharz().petFollow.cmy, 1);
					ServerEffect.addServerEffect(55, global::Char.myCharz().cx, global::Char.myCharz().cy, 1);
					goto IL_A990;
				}
				goto IL_A990;
			}
			case -2:
			{
				GameCanvas.debug("SA77", 22);
				int num255 = msg.reader().readInt();
				global::Char.myCharz().yen += num255;
				GameScr.startFlyText((num255 <= 0) ? (string.Empty + num255.ToString()) : ("+" + num255.ToString()), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
				goto IL_A990;
			}
			case -1:
			{
				GameCanvas.debug("SA77", 222);
				int num256 = msg.reader().readInt();
				global::Char.myCharz().xu += (long)num256;
				global::Char.myCharz().xuStr = Res.formatNumber(global::Char.myCharz().xu);
				global::Char.myCharz().yen -= num256;
				GameScr.startFlyText("+" + num256.ToString(), global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 10, 0, -2, mFont.YELLOW);
				goto IL_A990;
			}
			}
			GameCanvas.debug("SA86", 2);
			Mob mob17 = null;
			try
			{
				int num257 = (int)msg.reader().readUnsignedByte();
				mob17 = (Mob)GameScr.vMob.elementAt(num257);
			}
			catch (Exception ex9)
			{
				Res.outz("Loi tai NPC_ATTACK_ME " + msg.command.ToString() + " err= " + ex9.StackTrace);
			}
			if (mob17 != null)
			{
				global::Char.myCharz().isDie = false;
				global::Char.isLockKey = false;
				int num258 = msg.readInt3Byte();
				int num259;
				try
				{
					num259 = msg.readInt3Byte();
				}
				catch (Exception)
				{
					num259 = 0;
				}
				if (mob17.isBusyAttackSomeOne)
				{
					global::Char.myCharz().doInjure(num258, num259, false, true);
				}
				else
				{
					mob17.dame = num258;
					mob17.dameMp = num259;
					mob17.setAttack(global::Char.myCharz());
				}
			}
			IL_A990:
			GameCanvas.debug("SA92", 2);
		}
		catch (Exception ex10)
		{
			string[] array23 = new string[6];
			array23[0] = "[Controller] [error] ";
			array23[1] = ex10.StackTrace;
			array23[2] = " msg: ";
			array23[3] = ex10.Message;
			array23[4] = " cause ";
			int num260 = 5;
			IDictionary data = ex10.Data;
			array23[num260] = ((data != null) ? data.ToString() : null);
			Res.err(string.Concat(array23));
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x060001E3 RID: 483 RVA: 0x00025B60 File Offset: 0x00023D60
	internal void readLogin(Message msg)
	{
		sbyte b = msg.reader().readByte();
		ChooseCharScr.playerData = new PlayerData[(int)b];
		Res.outz("[LEN] sl nguoi choi " + b.ToString());
		for (int i = 0; i < (int)b; i++)
		{
			int num = msg.reader().readInt();
			string text = msg.reader().readUTF();
			short num2 = msg.reader().readShort();
			short num3 = msg.reader().readShort();
			short num4 = msg.reader().readShort();
			long num5 = msg.reader().readLong();
			ChooseCharScr.playerData[i] = new PlayerData(num, text, num2, num3, num4, num5);
		}
		GameCanvas.chooseCharScr.switchToMe();
		GameCanvas.chooseCharScr.updateChooseCharacter((byte)b);
	}

	// Token: 0x060001E4 RID: 484 RVA: 0x00025C20 File Offset: 0x00023E20
	internal void createItem(myReader d)
	{
		GameScr.vcItem = d.readByte();
		ItemTemplates.itemTemplates.clear();
		GameScr.gI().iOptionTemplates = new ItemOptionTemplate[(int)d.readUnsignedByte()];
		for (int i = 0; i < GameScr.gI().iOptionTemplates.Length; i++)
		{
			GameScr.gI().iOptionTemplates[i] = new ItemOptionTemplate();
			GameScr.gI().iOptionTemplates[i].id = i;
			GameScr.gI().iOptionTemplates[i].name = d.readUTF();
			GameScr.gI().iOptionTemplates[i].type = (int)d.readByte();
		}
		int num = (int)d.readShort();
		for (int j = 0; j < num; j++)
		{
			ItemTemplates.add(new ItemTemplate((short)j, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBool()));
		}
	}

	// Token: 0x060001E5 RID: 485 RVA: 0x00025D14 File Offset: 0x00023F14
	internal void createSkill(myReader d)
	{
		GameScr.vcSkill = d.readByte();
		GameScr.gI().sOptionTemplates = new SkillOptionTemplate[(int)d.readByte()];
		for (int i = 0; i < GameScr.gI().sOptionTemplates.Length; i++)
		{
			GameScr.gI().sOptionTemplates[i] = new SkillOptionTemplate();
			GameScr.gI().sOptionTemplates[i].id = i;
			GameScr.gI().sOptionTemplates[i].name = d.readUTF();
		}
		GameScr.nClasss = new NClass[(int)d.readByte()];
		for (int j = 0; j < GameScr.nClasss.Length; j++)
		{
			GameScr.nClasss[j] = new NClass();
			GameScr.nClasss[j].classId = j;
			GameScr.nClasss[j].name = d.readUTF();
			GameScr.nClasss[j].skillTemplates = new SkillTemplate[(int)d.readByte()];
			for (int k = 0; k < GameScr.nClasss[j].skillTemplates.Length; k++)
			{
				GameScr.nClasss[j].skillTemplates[k] = new SkillTemplate();
				GameScr.nClasss[j].skillTemplates[k].id = d.readByte();
				GameScr.nClasss[j].skillTemplates[k].name = d.readUTF();
				GameScr.nClasss[j].skillTemplates[k].maxPoint = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].manaUseType = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].type = (int)d.readByte();
				GameScr.nClasss[j].skillTemplates[k].iconId = (int)d.readShort();
				GameScr.nClasss[j].skillTemplates[k].damInfo = d.readUTF();
				int num = 130;
				if (GameCanvas.w == 128 || GameCanvas.h <= 208)
				{
					num = 100;
				}
				GameScr.nClasss[j].skillTemplates[k].description = mFont.tahoma_7_green2.splitFontArray(d.readUTF(), num);
				GameScr.nClasss[j].skillTemplates[k].skills = new Skill[(int)d.readByte()];
				for (int l = 0; l < GameScr.nClasss[j].skillTemplates[k].skills.Length; l++)
				{
					GameScr.nClasss[j].skillTemplates[k].skills[l] = new Skill();
					GameScr.nClasss[j].skillTemplates[k].skills[l].skillId = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].template = GameScr.nClasss[j].skillTemplates[k];
					GameScr.nClasss[j].skillTemplates[k].skills[l].point = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].skills[l].powRequire = d.readLong();
					GameScr.nClasss[j].skillTemplates[k].skills[l].manaUse = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].coolDown = d.readInt();
					GameScr.nClasss[j].skillTemplates[k].skills[l].dx = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].dy = (int)d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].maxFight = (int)d.readByte();
					GameScr.nClasss[j].skillTemplates[k].skills[l].damage = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].price = d.readShort();
					GameScr.nClasss[j].skillTemplates[k].skills[l].moreInfo = d.readUTF();
					Skills.add(GameScr.nClasss[j].skillTemplates[k].skills[l]);
				}
			}
		}
	}

	// Token: 0x060001E6 RID: 486 RVA: 0x00026134 File Offset: 0x00024334
	internal void createMap(myReader d)
	{
		GameScr.vcMap = d.readByte();
		TileMap.mapNames = new string[(int)d.readUnsignedByte()];
		for (int i = 0; i < TileMap.mapNames.Length; i++)
		{
			TileMap.mapNames[i] = d.readUTF();
		}
		Npc.arrNpcTemplate = new NpcTemplate[(int)d.readByte()];
		sbyte b = 0;
		while ((int)b < Npc.arrNpcTemplate.Length)
		{
			Npc.arrNpcTemplate[(int)b] = new NpcTemplate();
			Npc.arrNpcTemplate[(int)b].npcTemplateId = (int)b;
			Npc.arrNpcTemplate[(int)b].name = d.readUTF();
			Npc.arrNpcTemplate[(int)b].headId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].bodyId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].legId = (int)d.readShort();
			Npc.arrNpcTemplate[(int)b].menu = new string[(int)d.readByte()][];
			for (int j = 0; j < Npc.arrNpcTemplate[(int)b].menu.Length; j++)
			{
				Npc.arrNpcTemplate[(int)b].menu[j] = new string[(int)d.readByte()];
				for (int k = 0; k < Npc.arrNpcTemplate[(int)b].menu[j].Length; k++)
				{
					Npc.arrNpcTemplate[(int)b].menu[j][k] = d.readUTF();
				}
			}
			b += 1;
		}
		Mob.arrMobTemplate = new MobTemplate[(int)d.readByte()];
		sbyte b2 = 0;
		while ((int)b2 < Mob.arrMobTemplate.Length)
		{
			Mob.arrMobTemplate[(int)b2] = new MobTemplate();
			Mob.arrMobTemplate[(int)b2].mobTemplateId = b2;
			Mob.arrMobTemplate[(int)b2].type = d.readByte();
			Mob.arrMobTemplate[(int)b2].name = d.readUTF();
			Mob.arrMobTemplate[(int)b2].hp = d.readInt();
			Mob.arrMobTemplate[(int)b2].rangeMove = d.readByte();
			Mob.arrMobTemplate[(int)b2].speed = d.readByte();
			Mob.arrMobTemplate[(int)b2].dartType = d.readByte();
			b2 += 1;
		}
	}

	// Token: 0x060001E7 RID: 487 RVA: 0x0002633C File Offset: 0x0002453C
	internal void createData(myReader d, bool isSaveRMS)
	{
		GameScr.vcData = d.readByte();
		if (isSaveRMS)
		{
			Rms.saveRMS("NR_dart", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_arrow", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_effect", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_image", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_part", NinjaUtil.readByteArray(d));
			Rms.saveRMS("NR_skill", NinjaUtil.readByteArray(d));
			Rms.DeleteStorage("NRdata");
		}
	}

	// Token: 0x060001E8 RID: 488 RVA: 0x000263C4 File Offset: 0x000245C4
	internal Image createImage(sbyte[] arr)
	{
		try
		{
			return Image.createImage(arr, 0, arr.Length);
		}
		catch (Exception)
		{
		}
		return null;
	}

	// Token: 0x060001E9 RID: 489 RVA: 0x000263F4 File Offset: 0x000245F4
	public int[] arrayByte2Int(sbyte[] b)
	{
		int[] array = new int[b.Length];
		for (int i = 0; i < b.Length; i++)
		{
			int num = (int)b[i];
			if (num < 0)
			{
				num += 256;
			}
			array[i] = num;
		}
		return array;
	}

	// Token: 0x060001EA RID: 490 RVA: 0x00026430 File Offset: 0x00024630
	public void readClanMsg(Message msg, int index)
	{
		try
		{
			ClanMessage clanMessage = new ClanMessage();
			sbyte b = msg.reader().readByte();
			clanMessage.type = (int)b;
			clanMessage.id = msg.reader().readInt();
			clanMessage.playerId = msg.reader().readInt();
			clanMessage.playerName = msg.reader().readUTF();
			clanMessage.role = msg.reader().readByte();
			clanMessage.time = (long)(msg.reader().readInt() + 1000000000);
			bool flag = false;
			GameScr.isNewClanMessage = false;
			if (b == 0)
			{
				string text = msg.reader().readUTF();
				GameScr.isNewClanMessage = true;
				if (mFont.tahoma_7.getWidth(text) > Panel.WIDTH_PANEL - 60)
				{
					clanMessage.chat = mFont.tahoma_7.splitFontArray(text, Panel.WIDTH_PANEL - 10);
				}
				else
				{
					clanMessage.chat = new string[1];
					clanMessage.chat[0] = text;
				}
				clanMessage.color = msg.reader().readByte();
			}
			else if (b == 1)
			{
				clanMessage.recieve = (int)msg.reader().readByte();
				clanMessage.maxCap = (int)msg.reader().readByte();
				flag = msg.reader().readByte() == 1;
				if (flag)
				{
					GameScr.isNewClanMessage = true;
				}
				if (clanMessage.playerId != global::Char.myCharz().charID)
				{
					if (clanMessage.recieve < clanMessage.maxCap)
					{
						clanMessage.option = new string[] { mResources.donate };
					}
					else
					{
						clanMessage.option = null;
					}
				}
				if (GameCanvas.panel.cp != null)
				{
					GameCanvas.panel.updateRequest(clanMessage.recieve, clanMessage.maxCap);
				}
			}
			else if (b == 2 && global::Char.myCharz().role == 0)
			{
				GameScr.isNewClanMessage = true;
				clanMessage.option = new string[]
				{
					mResources.CANCEL,
					mResources.receive
				};
			}
			if (GameCanvas.currentScreen != GameScr.instance)
			{
				GameScr.isNewClanMessage = false;
			}
			else if (GameCanvas.panel.isShow && GameCanvas.panel.type == 0 && GameCanvas.panel.currentTabIndex == 3)
			{
				GameScr.isNewClanMessage = false;
			}
			ClanMessage.addMessage(clanMessage, index, flag);
		}
		catch (Exception)
		{
			Cout.println("LOI TAI CMD -= " + msg.command.ToString());
		}
	}

	// Token: 0x060001EB RID: 491 RVA: 0x00026680 File Offset: 0x00024880
	public void loadCurrMap(sbyte teleport3)
	{
		Res.outz("[CONTROLER] start load map " + teleport3.ToString());
		GameScr.gI().auto = 0;
		GameScr.isChangeZone = false;
		CreateCharScr.instance = null;
		GameScr.info1.isUpdate = false;
		GameScr.info2.isUpdate = false;
		GameScr.lockTick = 0;
		GameCanvas.panel.isShow = false;
		SoundMn.gI().stopAll();
		if (!GameScr.isLoadAllData && !CreateCharScr.isCreateChar)
		{
			GameScr.gI().initSelectChar();
		}
		GameScr.loadCamera(false, (teleport3 != 1) ? (-1) : global::Char.myCharz().cx, (teleport3 == 0) ? (-1) : 0);
		TileMap.loadMainTile();
		TileMap.loadMap(TileMap.tileID);
		Res.outz("LOAD GAMESCR 2");
		global::Char.myCharz().cvx = 0;
		global::Char.myCharz().statusMe = 4;
		global::Char.myCharz().currentMovePoint = null;
		global::Char.myCharz().mobFocus = null;
		global::Char.myCharz().charFocus = null;
		global::Char.myCharz().npcFocus = null;
		global::Char.myCharz().itemFocus = null;
		global::Char.myCharz().skillPaint = null;
		global::Char.myCharz().setMabuHold(false);
		global::Char.myCharz().skillPaintRandomPaint = null;
		GameCanvas.clearAllPointerEvent();
		if (global::Char.myCharz().cy >= TileMap.pxh - 100)
		{
			global::Char.myCharz().isFlyUp = true;
			global::Char.myCharz().cx += Res.abs(Res.random(0, 80));
			Service.gI().charMove();
		}
		GameScr.gI().loadGameScr();
		GameCanvas.loadBG(TileMap.bgID);
		global::Char.isLockKey = false;
		Res.outz("cy= " + global::Char.myCharz().cy.ToString() + "---------------------------------------------");
		for (int i = 0; i < global::Char.myCharz().vEff.size(); i++)
		{
			if (((EffectChar)global::Char.myCharz().vEff.elementAt(i)).template.type == 10)
			{
				global::Char.isLockKey = true;
				break;
			}
		}
		GameCanvas.clearKeyHold();
		GameCanvas.clearKeyPressed();
		GameScr.gI().dHP = global::Char.myCharz().cHP;
		GameScr.gI().dMP = global::Char.myCharz().cMP;
		global::Char.ischangingMap = false;
		GameScr.gI().switchToMe();
		if (global::Char.myCharz().cy <= 10 && teleport3 != 0 && teleport3 != 2)
		{
			Teleport.addTeleport(new Teleport(global::Char.myCharz().cx, global::Char.myCharz().cy, global::Char.myCharz().head, global::Char.myCharz().cdir, 1, true, (teleport3 != 1) ? ((int)teleport3) : global::Char.myCharz().cgender));
			global::Char.myCharz().isTeleport = true;
		}
		if (teleport3 == 2)
		{
			global::Char.myCharz().show();
		}
		if (GameScr.gI().isRongThanXuatHien)
		{
			if (TileMap.mapID == GameScr.gI().mapRID && TileMap.zoneID == GameScr.gI().zoneRID)
			{
				GameScr.gI().callRongThan(GameScr.gI().xR, GameScr.gI().yR);
			}
			if (mGraphics.zoomLevel > 1)
			{
				GameScr.gI().doiMauTroi();
			}
		}
		InfoDlg.hide();
		InfoDlg.show(TileMap.mapName, mResources.zone + " " + TileMap.zoneID.ToString(), 30);
		GameCanvas.endDlg();
		GameCanvas.isLoading = false;
		Hint.clickMob();
		Hint.clickNpc();
		GameCanvas.debug("SA75x9", 2);
		GameCanvas.isRequestMapID = 2;
		GameCanvas.waitingTimeChangeMap = mSystem.currentTimeMillis() + 1000L;
		Res.outz("[CONTROLLER] loadMap DONE!!!!!!!!!");
	}

	// Token: 0x060001EC RID: 492 RVA: 0x000269F4 File Offset: 0x00024BF4
	public void loadInfoMap(Message msg)
	{
		try
		{
			if (mGraphics.zoomLevel == 1)
			{
				SmallImage.clearHastable();
			}
			global::Char.myCharz().cx = (global::Char.myCharz().cxSend = (global::Char.myCharz().cxFocus = (int)msg.reader().readShort()));
			global::Char.myCharz().cy = (global::Char.myCharz().cySend = (global::Char.myCharz().cyFocus = (int)msg.reader().readShort()));
			global::Char.myCharz().xSd = global::Char.myCharz().cx;
			global::Char.myCharz().ySd = global::Char.myCharz().cy;
			Res.outz(string.Concat(new string[]
			{
				"head= ",
				global::Char.myCharz().head.ToString(),
				" body= ",
				global::Char.myCharz().body.ToString(),
				" left= ",
				global::Char.myCharz().leg.ToString(),
				" x= ",
				global::Char.myCharz().cx.ToString(),
				" y= ",
				global::Char.myCharz().cy.ToString(),
				" chung toc= ",
				global::Char.myCharz().cgender.ToString()
			}));
			if (global::Char.myCharz().cx >= 0 && global::Char.myCharz().cx <= 100)
			{
				global::Char.myCharz().cdir = 1;
			}
			else if (global::Char.myCharz().cx >= TileMap.tmw - 100 && global::Char.myCharz().cx <= TileMap.tmw)
			{
				global::Char.myCharz().cdir = -1;
			}
			GameCanvas.debug("SA75x4", 2);
			int num = (int)msg.reader().readByte();
			Res.outz("vGo size= " + num.ToString());
			if (!GameScr.info1.isDone)
			{
				GameScr.info1.cmx = global::Char.myCharz().cx - GameScr.cmx;
				GameScr.info1.cmy = global::Char.myCharz().cy - GameScr.cmy;
			}
			for (int i = 0; i < num; i++)
			{
				Waypoint waypoint = new Waypoint(msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readUTF());
				if ((TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23) && waypoint.minX >= 0)
				{
					short minX = waypoint.minX;
				}
			}
			Resources.UnloadUnusedAssets();
			GC.Collect();
			GameCanvas.debug("SA75x5", 2);
			num = (int)msg.reader().readByte();
			Mob.newMob.removeAllElements();
			sbyte b = 0;
			while ((int)b < num)
			{
				Mob mob = new Mob((int)b, msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), msg.reader().readBoolean(), (int)msg.reader().readByte(), (int)msg.reader().readByte(), msg.reader().readInt(), msg.reader().readByte(), msg.reader().readInt(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readByte(), msg.reader().readByte());
				mob.xSd = mob.x;
				mob.ySd = mob.y;
				mob.isBoss = msg.reader().readBoolean();
				if (Mob.arrMobTemplate[mob.templateId].type != 0)
				{
					if (b % 3 == 0)
					{
						mob.dir = -1;
					}
					else
					{
						mob.dir = 1;
					}
					mob.x += (int)(10 - b % 20);
				}
				mob.isMobMe = false;
				BigBoss bigBoss = null;
				BachTuoc bachTuoc = null;
				BigBoss2 bigBoss2 = null;
				NewBoss newBoss = null;
				if (mob.templateId == 70)
				{
					bigBoss = new BigBoss((int)b, (short)mob.x, (short)mob.y, 70, mob.hp, mob.maxHp, mob.sys);
				}
				if (mob.templateId == 71)
				{
					bachTuoc = new BachTuoc((int)b, (short)mob.x, (short)mob.y, 71, mob.hp, mob.maxHp, mob.sys);
				}
				if (mob.templateId == 72)
				{
					bigBoss2 = new BigBoss2((int)b, (short)mob.x, (short)mob.y, 72, mob.hp, mob.maxHp, 3);
				}
				if (mob.isBoss)
				{
					newBoss = new NewBoss((int)b, (short)mob.x, (short)mob.y, mob.templateId, mob.hp, mob.maxHp, mob.sys);
				}
				if (newBoss != null)
				{
					GameScr.vMob.addElement(newBoss);
				}
				else if (bigBoss != null)
				{
					GameScr.vMob.addElement(bigBoss);
				}
				else if (bachTuoc != null)
				{
					GameScr.vMob.addElement(bachTuoc);
				}
				else if (bigBoss2 != null)
				{
					GameScr.vMob.addElement(bigBoss2);
				}
				else
				{
					GameScr.vMob.addElement(mob);
				}
				b += 1;
			}
			if (global::Char.myCharz().mobMe != null && GameScr.findMobInMap(global::Char.myCharz().mobMe.mobId) == null)
			{
				global::Char.myCharz().mobMe.getData();
				global::Char.myCharz().mobMe.x = global::Char.myCharz().cx;
				global::Char.myCharz().mobMe.y = global::Char.myCharz().cy - 40;
				GameScr.vMob.addElement(global::Char.myCharz().mobMe);
			}
			num = (int)msg.reader().readByte();
			byte b2 = 0;
			while ((int)b2 < num)
			{
				b2 += 1;
			}
			GameCanvas.debug("SA75x6", 2);
			num = (int)msg.reader().readByte();
			Res.outz("NPC size= " + num.ToString());
			for (int j = 0; j < num; j++)
			{
				sbyte b3 = msg.reader().readByte();
				short num2 = msg.reader().readShort();
				short num3 = msg.reader().readShort();
				sbyte b4 = msg.reader().readByte();
				short num4 = msg.reader().readShort();
				if (b4 != 6 && ((global::Char.myCharz().taskMaint.taskId >= 7 && (global::Char.myCharz().taskMaint.taskId != 7 || global::Char.myCharz().taskMaint.index > 1)) || (b4 != 7 && b4 != 8 && b4 != 9)) && (global::Char.myCharz().taskMaint.taskId >= 6 || b4 != 16))
				{
					if (b4 == 4)
					{
						GameScr.gI().magicTree = new MagicTree(j, (int)b3, (int)num2, (int)num3, (int)b4, (int)num4);
						Service.gI().magicTree(2);
						GameScr.vNpc.addElement(GameScr.gI().magicTree);
					}
					else
					{
						Npc npc = new Npc(j, (int)b3, (int)num2, (int)(num3 + 3), (int)b4, (int)num4);
						GameScr.vNpc.addElement(npc);
					}
				}
			}
			GameCanvas.debug("SA75x7", 2);
			num = (int)msg.reader().readByte();
			string text = string.Empty;
			Res.outz("item size = " + num.ToString());
			text = text + "item: " + num.ToString();
			for (int k = 0; k < num; k++)
			{
				short num5 = msg.reader().readShort();
				short num6 = msg.reader().readShort();
				int num7 = (int)msg.reader().readShort();
				int num8 = (int)msg.reader().readShort();
				int num9 = msg.reader().readInt();
				short num10 = 0;
				if (num9 == -2)
				{
					num10 = msg.reader().readShort();
				}
				ItemMap itemMap = new ItemMap(num9, num5, num6, num7, num8, num10);
				bool flag = false;
				for (int l = 0; l < GameScr.vItemMap.size(); l++)
				{
					if (((ItemMap)GameScr.vItemMap.elementAt(l)).itemMapID == itemMap.itemMapID)
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					GameScr.vItemMap.addElement(itemMap);
				}
				text = text + num6.ToString() + ",";
			}
			Res.err("sl item on map " + text + "\n");
			TileMap.vCurrItem.removeAllElements();
			if (mGraphics.zoomLevel == 1)
			{
				BgItem.clearHashTable();
			}
			BgItem.vKeysNew.removeAllElements();
			if (!GameCanvas.lowGraphic || (GameCanvas.lowGraphic && TileMap.isVoDaiMap()) || TileMap.mapID == 45 || TileMap.mapID == 46 || TileMap.mapID == 47 || TileMap.mapID == 48 || TileMap.mapID == 120 || TileMap.mapID == 128 || TileMap.mapID == 170 || TileMap.mapID == 49)
			{
				short num11 = msg.reader().readShort();
				text = "item high graphic: ";
				for (int m = 0; m < (int)num11; m++)
				{
					short num12 = msg.reader().readShort();
					short num13 = msg.reader().readShort();
					short num14 = msg.reader().readShort();
					if (TileMap.getBIById((int)num12) != null)
					{
						BgItem bibyId = TileMap.getBIById((int)num12);
						BgItem bgItem = new BgItem();
						bgItem.id = (int)num12;
						bgItem.idImage = bibyId.idImage;
						bgItem.dx = bibyId.dx;
						bgItem.dy = bibyId.dy;
						bgItem.x = (int)(num13 * (short)TileMap.size);
						bgItem.y = (int)(num14 * (short)TileMap.size);
						bgItem.layer = bibyId.layer;
						if (TileMap.isExistMoreOne(bgItem.id))
						{
							bgItem.trans = ((m % 2 != 0) ? 2 : 0);
							if (TileMap.mapID == 45)
							{
								bgItem.trans = 0;
							}
						}
						if (!BgItem.imgNew.containsKey(bgItem.idImage.ToString() + string.Empty))
						{
							if (mGraphics.zoomLevel == 1)
							{
								Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage.ToString() + ".png");
								if (image == null)
								{
									image = Image.createRGBImage(new int[1], 1, 1, true);
									Service.gI().getBgTemplate(bgItem.idImage);
								}
								BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image);
							}
							else
							{
								bool flag2 = false;
								sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel.ToString() + "bgItem" + bgItem.idImage.ToString());
								if (array != null)
								{
									if (BgItem.newSmallVersion != null)
									{
										Res.outz("Small  last= " + (array.Length % 127).ToString() + "new Version= " + BgItem.newSmallVersion[(int)bgItem.idImage].ToString());
										if (array.Length % 127 != (int)BgItem.newSmallVersion[(int)bgItem.idImage])
										{
											flag2 = true;
										}
									}
									if (!flag2)
									{
										Image image = Image.createImage(array, 0, array.Length);
										if (image != null)
										{
											BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image);
										}
										else
										{
											flag2 = true;
										}
									}
								}
								else
								{
									flag2 = true;
								}
								if (flag2)
								{
									Image image = GameCanvas.loadImage("/mapBackGround/" + bgItem.idImage.ToString() + ".png");
									if (image == null)
									{
										image = Image.createRGBImage(new int[1], 1, 1, true);
										Service.gI().getBgTemplate(bgItem.idImage);
									}
									BgItem.imgNew.put(bgItem.idImage.ToString() + string.Empty, image);
								}
							}
							BgItem.vKeysLast.addElement(bgItem.idImage.ToString() + string.Empty);
						}
						if (!BgItem.isExistKeyNews(bgItem.idImage.ToString() + string.Empty))
						{
							BgItem.vKeysNew.addElement(bgItem.idImage.ToString() + string.Empty);
						}
						bgItem.changeColor();
						TileMap.vCurrItem.addElement(bgItem);
					}
					text = text + num12.ToString() + ",";
				}
				Res.err("item High Graphics: " + text);
				for (int n = 0; n < BgItem.vKeysLast.size(); n++)
				{
					string text2 = (string)BgItem.vKeysLast.elementAt(n);
					if (!BgItem.isExistKeyNews(text2))
					{
						BgItem.imgNew.remove(text2);
						if (BgItem.imgNew.containsKey(text2 + "blend" + 1.ToString()))
						{
							BgItem.imgNew.remove(text2 + "blend" + 1.ToString());
						}
						if (BgItem.imgNew.containsKey(text2 + "blend" + 3.ToString()))
						{
							BgItem.imgNew.remove(text2 + "blend" + 3.ToString());
						}
						BgItem.vKeysLast.removeElementAt(n);
						n--;
					}
				}
				BackgroudEffect.isFog = false;
				BackgroudEffect.nCloud = 0;
				EffecMn.vEff.removeAllElements();
				BackgroudEffect.vBgEffect.removeAllElements();
				Effect.newEff.removeAllElements();
				short num15 = msg.reader().readShort();
				for (int num16 = 0; num16 < (int)num15; num16++)
				{
					this.keyValueAction(msg.reader().readUTF(), msg.reader().readUTF());
				}
			}
			else
			{
				short num17 = msg.reader().readShort();
				for (int num18 = 0; num18 < (int)num17; num18++)
				{
					msg.reader().readShort();
					msg.reader().readShort();
					msg.reader().readShort();
				}
				short num19 = msg.reader().readShort();
				for (int num20 = 0; num20 < (int)num19; num20++)
				{
					msg.reader().readUTF();
					msg.reader().readUTF();
				}
			}
			TileMap.bgType = (int)msg.reader().readByte();
			this.loadCurrMap(msg.reader().readByte());
			GameCanvas.debug("SA75x8", 2);
		}
		catch (Exception)
		{
			Res.err(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Loadmap khong thanh cong");
			GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
			ServerListScreen.waitToLogin = true;
			GameCanvas.endDlg();
		}
		GameCanvas.isLoading = false;
		Res.err(">>>>>>>>>>>>>>>>>>>>>>>>>>>>>>> Loadmap thanh cong");
	}

	// Token: 0x060001ED RID: 493 RVA: 0x00027890 File Offset: 0x00025A90
	public void keyValueAction(string key, string value)
	{
		if (!key.Equals("eff"))
		{
			if (key.Equals("beff") && Panel.graphics <= 1)
			{
				BackgroudEffect.addEffect(int.Parse(value));
			}
			return;
		}
		if (Panel.graphics > 0)
		{
			return;
		}
		string[] array = Res.split(value, ".", 0);
		int num = int.Parse(array[0]);
		int num2 = int.Parse(array[1]);
		int num3 = int.Parse(array[2]);
		int num4 = int.Parse(array[3]);
		int num5;
		int num6;
		if (array.Length <= 4)
		{
			num5 = -1;
			num6 = 1;
		}
		else
		{
			num5 = int.Parse(array[4]);
			num6 = int.Parse(array[5]);
		}
		Effect effect = new Effect(num, num3, num4, num2, num5, num6);
		if (array.Length > 6)
		{
			effect.typeEff = int.Parse(array[6]);
			if (array.Length > 7)
			{
				effect.indexFrom = int.Parse(array[7]);
				effect.indexTo = int.Parse(array[8]);
			}
		}
		EffecMn.addEff(effect);
	}

	// Token: 0x060001EE RID: 494 RVA: 0x00027978 File Offset: 0x00025B78
	public void messageNotMap(Message msg)
	{
		GameCanvas.debug("SA6", 2);
		try
		{
			sbyte b = msg.reader().readByte();
			Res.outz("---messageNotMap : " + b.ToString());
			switch (b)
			{
			case 4:
			{
				GameCanvas.debug("SA8", 2);
				GameCanvas.loginScr.savePass();
				GameScr.isAutoPlay = false;
				GameScr.canAutoPlay = false;
				LoginScr.isUpdateAll = true;
				LoginScr.isUpdateData = true;
				LoginScr.isUpdateMap = true;
				LoginScr.isUpdateSkill = true;
				LoginScr.isUpdateItem = true;
				GameScr.vsData = msg.reader().readByte();
				GameScr.vsMap = msg.reader().readByte();
				GameScr.vsSkill = msg.reader().readByte();
				GameScr.vsItem = msg.reader().readByte();
				msg.reader().readByte();
				if (GameCanvas.loginScr.isLogin2)
				{
					Rms.saveRMSString("acc", string.Empty);
					Rms.saveRMSString("pass", string.Empty);
				}
				else
				{
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect.ToString(), string.Empty);
				}
				if (GameScr.vsData != GameScr.vcData)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateData();
				}
				else
				{
					try
					{
						LoginScr.isUpdateData = false;
					}
					catch (Exception)
					{
						GameScr.vcData = -1;
						Service.gI().updateData();
					}
				}
				if (GameScr.vsMap != GameScr.vcMap)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateMap();
				}
				else
				{
					try
					{
						if (!GameScr.isLoadAllData)
						{
							this.createMap(new DataInputStream(Rms.loadRMS("NRmap")).r);
						}
						LoginScr.isUpdateMap = false;
					}
					catch (Exception)
					{
						GameScr.vcMap = -1;
						Service.gI().updateMap();
					}
				}
				if (GameScr.vsSkill != GameScr.vcSkill)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateSkill();
				}
				else
				{
					try
					{
						if (!GameScr.isLoadAllData)
						{
							this.createSkill(new DataInputStream(Rms.loadRMS("NRskill")).r);
						}
						LoginScr.isUpdateSkill = false;
					}
					catch (Exception)
					{
						GameScr.vcSkill = -1;
						Service.gI().updateSkill();
					}
				}
				if (GameScr.vsItem != GameScr.vcItem)
				{
					GameScr.isLoadAllData = false;
					Service.gI().updateItem();
				}
				else
				{
					try
					{
						this.loadItemNew(new DataInputStream(Rms.loadRMS("NRitem0")).r, 0, false);
						this.loadItemNew(new DataInputStream(Rms.loadRMS("NRitem1")).r, 1, false);
						this.loadItemNew(new DataInputStream(Rms.loadRMS("NRitem2")).r, 2, false);
						this.loadItemNew(new DataInputStream(Rms.loadRMS("NRitem100")).r, 100, false);
						LoginScr.isUpdateItem = false;
					}
					catch (Exception)
					{
						GameScr.vcItem = -1;
						Service.gI().updateItem();
					}
					try
					{
						this.loadItemNew(new DataInputStream(Rms.loadRMS("NRitem101")).r, 101, false);
					}
					catch (Exception)
					{
					}
				}
				if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
				{
					if (!GameScr.isLoadAllData)
					{
						GameScr.gI().readDart();
						GameScr.gI().readEfect();
						GameScr.gI().readArrow();
						GameScr.gI().readSkill();
					}
					Service.gI().clientOk();
				}
				sbyte b2 = msg.reader().readByte();
				Res.outz("CAPTION LENT= " + b2.ToString());
				GameScr.exps = new long[(int)b2];
				for (int i = 0; i < GameScr.exps.Length; i++)
				{
					GameScr.exps[i] = msg.reader().readLong();
				}
				break;
			}
			default:
				switch (b)
				{
				case 35:
					GameCanvas.endDlg();
					GameScr.gI().resetButton();
					GameScr.info1.addInfo(msg.reader().readUTF(), 0);
					break;
				case 36:
					GameScr.typeActive = msg.reader().readByte();
					Res.outz("load Me Active: " + GameScr.typeActive.ToString());
					break;
				}
				break;
			case 6:
			{
				Res.outz("GET UPDATE_MAP " + msg.reader().available().ToString() + " bytes");
				msg.reader().mark(100000);
				this.createMap(msg.reader());
				msg.reader().reset();
				sbyte[] array = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref array);
				Rms.saveRMS("NRmap", array);
				Rms.saveRMS("NRmapVersion", new sbyte[] { GameScr.vcMap });
				LoginScr.isUpdateMap = false;
				if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				break;
			}
			case 7:
			{
				Res.outz("GET UPDATE_SKILL " + msg.reader().available().ToString() + " bytes");
				msg.reader().mark(100000);
				this.createSkill(msg.reader());
				msg.reader().reset();
				sbyte[] array2 = new sbyte[msg.reader().available()];
				msg.reader().readFully(ref array2);
				Rms.saveRMS("NRskill", array2);
				Rms.saveRMS("NRskillVersion", new sbyte[] { GameScr.vcSkill });
				LoginScr.isUpdateSkill = false;
				if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
				{
					GameScr.gI().readDart();
					GameScr.gI().readEfect();
					GameScr.gI().readArrow();
					GameScr.gI().readSkill();
					Service.gI().clientOk();
				}
				break;
			}
			case 8:
				Res.outz("GET UPDATE_ITEM " + msg.reader().available().ToString() + " bytes");
				this.createItemNew(msg.reader());
				break;
			case 9:
				GameCanvas.debug("SA11", 2);
				break;
			case 10:
				try
				{
					global::Char.isLoadingMap = true;
					Res.outz("REQUEST MAP TEMPLATE");
					GameCanvas.isLoading = true;
					TileMap.maps = null;
					TileMap.types = null;
					mSystem.gcc();
					GameCanvas.debug("SA99", 2);
					TileMap.tmw = (int)msg.reader().readByte();
					TileMap.tmh = (int)msg.reader().readByte();
					TileMap.maps = new int[TileMap.tmw * TileMap.tmh];
					Res.err("   M apsize= " + (TileMap.tmw * TileMap.tmh).ToString());
					for (int j = 0; j < TileMap.maps.Length; j++)
					{
						int num = (int)msg.reader().readByte();
						if (num < 0)
						{
							num += 256;
						}
						TileMap.maps[j] = (int)((ushort)num);
					}
					TileMap.types = new int[TileMap.maps.Length];
					msg = this.messWait;
					this.loadInfoMap(msg);
					try
					{
						TileMap.isMapDouble = msg.reader().readByte() != 0;
					}
					catch (Exception ex)
					{
						Res.err(" 1 LOI TAI CASE REQUEST_MAPTEMPLATE " + ex.ToString());
					}
				}
				catch (Exception ex2)
				{
					Res.err("2 LOI TAI CASE REQUEST_MAPTEMPLATE " + ex2.ToString());
				}
				msg.cleanup();
				this.messWait.cleanup();
				msg = (this.messWait = null);
				GameScr.gI().switchToMe();
				break;
			case 12:
				GameCanvas.debug("SA10", 2);
				break;
			case 16:
				MoneyCharge.gI().switchToMe();
				break;
			case 17:
				GameCanvas.debug("SYB123", 2);
				global::Char.myCharz().clearTask();
				break;
			case 18:
			{
				GameCanvas.isLoading = false;
				GameCanvas.endDlg();
				int num2 = msg.reader().readInt();
				GameCanvas.inputDlg.show(mResources.changeNameChar, new Command(mResources.OK, GameCanvas.instance, 88829, num2), TField.INPUT_TYPE_ANY);
				break;
			}
			case 20:
				global::Char.myCharz().cPk = msg.reader().readByte();
				GameScr.info1.addInfo(mResources.PK_NOW + " " + global::Char.myCharz().cPk.ToString(), 0);
				break;
			}
		}
		catch (Exception)
		{
			Cout.LogError("LOI TAI messageNotMap + " + msg.command.ToString());
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x060001EF RID: 495 RVA: 0x0002834C File Offset: 0x0002654C
	public void messageNotLogin(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			Res.outz("---messageNotLogin : " + b.ToString());
			if (b == 2)
			{
				ServerListScreen.linkDefault = msg.reader().readUTF();
				mSystem.AddIpTest();
				ServerListScreen.getServerList(ServerListScreen.linkDefault);
				try
				{
					Panel.CanNapTien = msg.reader().readByte() == 1;
				}
				catch (Exception)
				{
				}
			}
		}
		catch (Exception)
		{
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x060001F0 RID: 496 RVA: 0x000283F0 File Offset: 0x000265F0
	public void messageSubCommand(Message msg)
	{
		try
		{
			GameCanvas.debug("SA12", 2);
			sbyte b = msg.reader().readByte();
			Res.outz("---messageSubCommand : " + b.ToString());
			switch (b)
			{
			case 0:
			{
				GameCanvas.debug("SA21", 2);
				RadarScr.list = new MyVector();
				Teleport.vTeleport.removeAllElements();
				GameScr.vCharInMap.removeAllElements();
				GameScr.vItemMap.removeAllElements();
				global::Char.vItemTime.removeAllElements();
				GameScr.loadImg();
				GameScr.currentCharViewInfo = global::Char.myCharz();
				global::Char.myCharz().charID = msg.reader().readInt();
				global::Char.myCharz().ctaskId = (int)msg.reader().readByte();
				global::Char.myCharz().cgender = (int)msg.reader().readByte();
				global::Char.myCharz().head = (int)msg.reader().readShort();
				global::Char.myCharz().cName = msg.reader().readUTF();
				global::Char.myCharz().cPk = msg.reader().readByte();
				global::Char.myCharz().cTypePk = msg.reader().readByte();
				global::Char.myCharz().cPower = msg.reader().readLong();
				global::Char.myCharz().applyCharLevelPercent();
				global::Char.myCharz().eff5BuffHp = (int)msg.reader().readShort();
				global::Char.myCharz().eff5BuffMp = (int)msg.reader().readShort();
				global::Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				GameScr.gI().dHP = global::Char.myCharz().cHP;
				GameScr.gI().dMP = global::Char.myCharz().cMP;
				sbyte b2 = msg.reader().readByte();
				for (sbyte b3 = 0; b3 < b2; b3 += 1)
				{
					this.useSkill(Skills.get(msg.reader().readShort()));
				}
				GameScr.gI().sortSkill();
				GameScr.gI().loadSkillShortcut();
				global::Char.myCharz().xu = msg.reader().readLong();
				global::Char.myCharz().luongKhoa = msg.reader().readInt();
				global::Char.myCharz().luong = msg.reader().readInt();
				global::Char.myCharz().xuStr = Res.formatNumber(global::Char.myCharz().xu);
				global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
				global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
				global::Char.myCharz().arrItemBody = new Item[(int)msg.reader().readByte()];
				try
				{
					global::Char.myCharz().setDefaultPart();
					for (int i = 0; i < global::Char.myCharz().arrItemBody.Length; i++)
					{
						short num = msg.reader().readShort();
						if (num != -1)
						{
							ItemTemplate itemTemplate = ItemTemplates.get(num);
							int type = (int)itemTemplate.type;
							global::Char.myCharz().arrItemBody[i] = new Item();
							global::Char.myCharz().arrItemBody[i].template = itemTemplate;
							global::Char.myCharz().arrItemBody[i].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemBody[i].info = msg.reader().readUTF();
							global::Char.myCharz().arrItemBody[i].content = msg.reader().readUTF();
							int num2 = (int)msg.reader().readUnsignedByte();
							if (num2 != 0)
							{
								global::Char.myCharz().arrItemBody[i].itemOption = new ItemOption[num2];
								for (int j = 0; j < global::Char.myCharz().arrItemBody[i].itemOption.Length; j++)
								{
									int num3 = (int)msg.reader().readUnsignedByte();
									int num4 = (int)msg.reader().readUnsignedShort();
									if (num3 != -1)
									{
										global::Char.myCharz().arrItemBody[i].itemOption[j] = new ItemOption(num3, num4);
									}
								}
							}
							if (type == 0)
							{
								Res.outz("toi day =======================================" + global::Char.myCharz().body.ToString());
								global::Char.myCharz().body = (int)global::Char.myCharz().arrItemBody[i].template.part;
							}
							else if (type == 1)
							{
								global::Char.myCharz().leg = (int)global::Char.myCharz().arrItemBody[i].template.part;
								Res.outz("toi day =======================================" + global::Char.myCharz().leg.ToString());
							}
						}
					}
				}
				catch (Exception)
				{
				}
				global::Char.myCharz().arrItemBag = new Item[(int)msg.reader().readByte()];
				GameScr.hpPotion = 0;
				GameScr.isudungCapsun4 = false;
				GameScr.isudungCapsun3 = false;
				for (int k = 0; k < global::Char.myCharz().arrItemBag.Length; k++)
				{
					short num5 = msg.reader().readShort();
					if (num5 != -1)
					{
						global::Char.myCharz().arrItemBag[k] = new Item();
						global::Char.myCharz().arrItemBag[k].template = ItemTemplates.get(num5);
						global::Char.myCharz().arrItemBag[k].quantity = msg.reader().readInt();
						global::Char.myCharz().arrItemBag[k].info = msg.reader().readUTF();
						global::Char.myCharz().arrItemBag[k].content = msg.reader().readUTF();
						global::Char.myCharz().arrItemBag[k].indexUI = k;
						sbyte b4 = msg.reader().readByte();
						if (b4 != 0)
						{
							global::Char.myCharz().arrItemBag[k].itemOption = new ItemOption[(int)b4];
							for (int l = 0; l < global::Char.myCharz().arrItemBag[k].itemOption.Length; l++)
							{
								int num6 = (int)msg.reader().readUnsignedByte();
								int num7 = (int)msg.reader().readUnsignedShort();
								if (num6 != -1)
								{
									global::Char.myCharz().arrItemBag[k].itemOption[l] = new ItemOption(num6, num7);
									global::Char.myCharz().arrItemBag[k].getCompare();
								}
							}
						}
						if (global::Char.myCharz().arrItemBag[k].template.type == 6)
						{
							GameScr.hpPotion += global::Char.myCharz().arrItemBag[k].quantity;
						}
						if (num5 == 194)
						{
							GameScr.isudungCapsun4 = global::Char.myCharz().arrItemBag[k].quantity > 0;
						}
						else if (num5 == 193 && !GameScr.isudungCapsun4)
						{
							GameScr.isudungCapsun3 = global::Char.myCharz().arrItemBag[k].quantity > 0;
						}
					}
				}
				global::Char.myCharz().arrItemBox = new Item[(int)msg.reader().readByte()];
				GameCanvas.panel.hasUse = 0;
				for (int m = 0; m < global::Char.myCharz().arrItemBox.Length; m++)
				{
					short num8 = msg.reader().readShort();
					if (num8 != -1)
					{
						global::Char.myCharz().arrItemBox[m] = new Item();
						global::Char.myCharz().arrItemBox[m].template = ItemTemplates.get(num8);
						global::Char.myCharz().arrItemBox[m].quantity = msg.reader().readInt();
						global::Char.myCharz().arrItemBox[m].info = msg.reader().readUTF();
						global::Char.myCharz().arrItemBox[m].content = msg.reader().readUTF();
						global::Char.myCharz().arrItemBox[m].itemOption = new ItemOption[(int)msg.reader().readByte()];
						for (int n = 0; n < global::Char.myCharz().arrItemBox[m].itemOption.Length; n++)
						{
							int num9 = (int)msg.reader().readUnsignedByte();
							int num10 = (int)msg.reader().readUnsignedShort();
							if (num9 != -1)
							{
								global::Char.myCharz().arrItemBox[m].itemOption[n] = new ItemOption(num9, num10);
								global::Char.myCharz().arrItemBox[m].getCompare();
							}
						}
						GameCanvas.panel.hasUse++;
					}
				}
				global::Char.myCharz().statusMe = 4;
				if (Rms.loadRMSInt(global::Char.myCharz().cName + "vci") < 1)
				{
					GameScr.isViewClanInvite = false;
				}
				else
				{
					GameScr.isViewClanInvite = true;
				}
				short num11 = msg.reader().readShort();
				global::Char.idHead = new short[(int)num11];
				global::Char.idAvatar = new short[(int)num11];
				for (int num12 = 0; num12 < (int)num11; num12++)
				{
					global::Char.idHead[num12] = msg.reader().readShort();
					global::Char.idAvatar[num12] = msg.reader().readShort();
				}
				for (int num13 = 0; num13 < GameScr.info1.charId.Length; num13++)
				{
					GameScr.info1.charId[num13] = new int[3];
				}
				GameScr.info1.charId[global::Char.myCharz().cgender][0] = (int)msg.reader().readShort();
				GameScr.info1.charId[global::Char.myCharz().cgender][1] = (int)msg.reader().readShort();
				GameScr.info1.charId[global::Char.myCharz().cgender][2] = (int)msg.reader().readShort();
				global::Char.myCharz().isNhapThe = msg.reader().readByte() == 1;
				Res.outz("NHAP THE= " + global::Char.myCharz().isNhapThe.ToString());
				GameScr.deltaTime = mSystem.currentTimeMillis() - (long)msg.reader().readInt() * 1000L;
				GameScr.isNewMember = msg.reader().readByte();
				Service.gI().updateCaption((sbyte)global::Char.myCharz().cgender);
				Service.gI().androidPack();
				try
				{
					global::Char.myCharz().idAuraEff = msg.reader().readShort();
					global::Char.myCharz().idEff_Set_Item = (short)msg.reader().readSByte();
					global::Char.myCharz().idHat = msg.reader().readShort();
					return;
				}
				catch (Exception)
				{
					return;
				}
				break;
			}
			case 1:
				GameCanvas.debug("SA13", 2);
				global::Char.myCharz().nClass = GameScr.nClasss[(int)msg.reader().readByte()];
				global::Char.myCharz().cTiemNang = msg.reader().readLong();
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				global::Char.myCharz().myskill = null;
				return;
			case 2:
			{
				GameCanvas.debug("SA14", 2);
				if (global::Char.myCharz().statusMe != 14 && global::Char.myCharz().statusMe != 5)
				{
					global::Char.myCharz().cHP = global::Char.myCharz().cHPFull;
					global::Char.myCharz().cMP = global::Char.myCharz().cMPFull;
					Cout.LogError2(" ME_LOAD_SKILL");
				}
				global::Char.myCharz().vSkill.removeAllElements();
				global::Char.myCharz().vSkillFight.removeAllElements();
				sbyte b5 = msg.reader().readByte();
				for (sbyte b6 = 0; b6 < b5; b6 += 1)
				{
					this.useSkill(Skills.get(msg.reader().readShort()));
				}
				GameScr.gI().sortSkill();
				if (GameScr.isPaintInfoMe)
				{
					GameScr.indexRow = -1;
					GameScr.gI().left = (GameScr.gI().center = null);
				}
				return;
			}
			case 3:
			case 16:
			case 17:
			case 18:
			case 20:
			case 22:
			case 24:
			case 25:
			case 26:
			case 27:
			case 28:
			case 29:
			case 30:
			case 31:
			case 32:
			case 33:
			case 34:
				goto IL_170C;
			case 4:
				break;
			case 5:
			{
				GameCanvas.debug("SA24", 2);
				int cHP = global::Char.myCharz().cHP;
				global::Char.myCharz().cHP = msg.readInt3Byte();
				if (global::Char.myCharz().cHP > cHP && global::Char.myCharz().cTypePk != 4)
				{
					GameScr.startFlyText("+" + (global::Char.myCharz().cHP - cHP).ToString() + " " + mResources.HP, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 20, 0, -1, mFont.HP);
					SoundMn.gI().HP_MPup();
					if (global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5003)
					{
						MonsterDart.addMonsterDart(global::Char.myCharz().petFollow.cmx + ((global::Char.myCharz().petFollow.dir != 1) ? (-10) : 10), global::Char.myCharz().petFollow.cmy + 10, true, -1, -1, global::Char.myCharz(), 29);
					}
				}
				if (global::Char.myCharz().cHP < cHP)
				{
					GameScr.startFlyText("-" + (cHP - global::Char.myCharz().cHP).ToString() + " " + mResources.HP, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 20, 0, -1, mFont.HP);
				}
				GameScr.gI().dHP = global::Char.myCharz().cHP;
				bool isPaintInfoMe = GameScr.isPaintInfoMe;
				return;
			}
			case 6:
			{
				GameCanvas.debug("SA25", 2);
				if (global::Char.myCharz().statusMe == 14 || global::Char.myCharz().statusMe == 5)
				{
					return;
				}
				int cMP = global::Char.myCharz().cMP;
				global::Char.myCharz().cMP = msg.readInt3Byte();
				if (global::Char.myCharz().cMP > cMP)
				{
					GameScr.startFlyText("+" + (global::Char.myCharz().cMP - cMP).ToString() + " " + mResources.KI, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 23, 0, -2, mFont.MP);
					SoundMn.gI().HP_MPup();
					if (global::Char.myCharz().petFollow != null && global::Char.myCharz().petFollow.smallID == 5001)
					{
						MonsterDart.addMonsterDart(global::Char.myCharz().petFollow.cmx + ((global::Char.myCharz().petFollow.dir != 1) ? (-10) : 10), global::Char.myCharz().petFollow.cmy + 10, true, -1, -1, global::Char.myCharz(), 29);
					}
				}
				if (global::Char.myCharz().cMP < cMP)
				{
					GameScr.startFlyText("-" + (cMP - global::Char.myCharz().cMP).ToString() + " " + mResources.KI, global::Char.myCharz().cx, global::Char.myCharz().cy - global::Char.myCharz().ch - 23, 0, -2, mFont.MP);
				}
				Res.outz("curr MP= " + global::Char.myCharz().cMP.ToString());
				GameScr.gI().dMP = global::Char.myCharz().cMP;
				bool isPaintInfoMe2 = GameScr.isPaintInfoMe;
				return;
			}
			case 7:
			{
				global::Char @char = GameScr.findCharInMap(msg.reader().readInt());
				if (@char == null)
				{
					return;
				}
				@char.clanID = msg.reader().readInt();
				if (@char.clanID == -2)
				{
					@char.isCopy = true;
				}
				this.readCharInfo(@char, msg);
				try
				{
					@char.idAuraEff = msg.reader().readShort();
					@char.idEff_Set_Item = (short)msg.reader().readSByte();
					@char.idHat = msg.reader().readShort();
					if (@char.bag >= 201)
					{
						@char.addEffChar(new Effect(@char.bag, @char, 2, -1, 10, 1)
						{
							typeEff = 5
						});
					}
					else
					{
						@char.removeEffChar(0, 201);
					}
					return;
				}
				catch (Exception)
				{
					return;
				}
				goto IL_1160;
			}
			case 8:
				goto IL_1160;
			case 9:
			{
				GameCanvas.debug("SA27", 2);
				global::Char char2 = GameScr.findCharInMap(msg.reader().readInt());
				if (char2 != null)
				{
					char2.cHP = msg.readInt3Byte();
					char2.cHPFull = msg.readInt3Byte();
				}
				return;
			}
			case 10:
			{
				GameCanvas.debug("SA28", 2);
				global::Char char3 = GameScr.findCharInMap(msg.reader().readInt());
				if (char3 != null)
				{
					char3.cHP = msg.readInt3Byte();
					char3.cHPFull = msg.readInt3Byte();
					char3.eff5BuffHp = (int)msg.reader().readShort();
					char3.eff5BuffMp = (int)msg.reader().readShort();
					char3.wp = (int)msg.reader().readShort();
					if (char3.wp == -1)
					{
						char3.setDefaultWeapon();
					}
				}
				return;
			}
			case 11:
			{
				GameCanvas.debug("SA29", 2);
				global::Char char4 = GameScr.findCharInMap(msg.reader().readInt());
				if (char4 != null)
				{
					char4.cHP = msg.readInt3Byte();
					char4.cHPFull = msg.readInt3Byte();
					char4.eff5BuffHp = (int)msg.reader().readShort();
					char4.eff5BuffMp = (int)msg.reader().readShort();
					char4.body = (int)msg.reader().readShort();
					if (char4.body == -1)
					{
						char4.setDefaultBody();
					}
				}
				return;
			}
			case 12:
			{
				GameCanvas.debug("SA30", 2);
				global::Char char5 = GameScr.findCharInMap(msg.reader().readInt());
				if (char5 != null)
				{
					char5.cHP = msg.readInt3Byte();
					char5.cHPFull = msg.readInt3Byte();
					char5.eff5BuffHp = (int)msg.reader().readShort();
					char5.eff5BuffMp = (int)msg.reader().readShort();
					char5.leg = (int)msg.reader().readShort();
					if (char5.leg == -1)
					{
						char5.setDefaultLeg();
					}
				}
				return;
			}
			case 13:
			{
				GameCanvas.debug("SA31", 2);
				int num14 = msg.reader().readInt();
				global::Char char6 = ((num14 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num14) : global::Char.myCharz());
				if (char6 != null)
				{
					char6.cHP = msg.readInt3Byte();
					char6.cHPFull = msg.readInt3Byte();
					char6.eff5BuffHp = (int)msg.reader().readShort();
					char6.eff5BuffMp = (int)msg.reader().readShort();
				}
				return;
			}
			case 14:
			{
				GameCanvas.debug("SA32", 2);
				global::Char char7 = GameScr.findCharInMap(msg.reader().readInt());
				if (char7 == null)
				{
					return;
				}
				char7.cHP = msg.readInt3Byte();
				sbyte b7 = msg.reader().readByte();
				Res.outz("player load hp type= " + b7.ToString());
				if (b7 == 1)
				{
					ServerEffect.addServerEffect(11, char7, 5);
					ServerEffect.addServerEffect(104, char7, 4);
				}
				if (b7 == 2)
				{
					char7.doInjure();
				}
				try
				{
					char7.cHPFull = msg.readInt3Byte();
					return;
				}
				catch (Exception)
				{
					return;
				}
				goto IL_147E;
			}
			case 15:
				goto IL_147E;
			case 19:
				GameCanvas.debug("SA17", 2);
				global::Char.myCharz().boxSort();
				return;
			case 21:
			{
				GameCanvas.debug("SA19", 2);
				int num15 = msg.reader().readInt();
				global::Char.myCharz().xuInBox -= num15;
				global::Char.myCharz().xu += (long)num15;
				global::Char.myCharz().xuStr = mSystem.numberTostring(global::Char.myCharz().xu);
				return;
			}
			case 23:
			{
				short num16 = msg.reader().readShort();
				Skill skill = Skills.get(num16);
				this.useSkill(skill);
				if (num16 != 0 && num16 != 14 && num16 != 28)
				{
					GameScr.info1.addInfo(mResources.LEARN_SKILL + " " + skill.template.name, 0);
				}
				return;
			}
			case 35:
			{
				GameCanvas.debug("SY3", 2);
				int num17 = msg.reader().readInt();
				Res.outz("CID = " + num17.ToString());
				if (TileMap.mapID == 130)
				{
					GameScr.gI().starVS();
				}
				if (num17 == global::Char.myCharz().charID)
				{
					global::Char.myCharz().cTypePk = msg.reader().readByte();
					if (GameScr.gI().isVS() && global::Char.myCharz().cTypePk != 0)
					{
						GameScr.gI().starVS();
					}
					Res.outz("type pk= " + global::Char.myCharz().cTypePk.ToString());
					global::Char.myCharz().npcFocus = null;
					if (!GameScr.gI().isMeCanAttackMob(global::Char.myCharz().mobFocus))
					{
						global::Char.myCharz().mobFocus = null;
					}
					global::Char.myCharz().itemFocus = null;
				}
				else
				{
					global::Char char8 = GameScr.findCharInMap(num17);
					if (char8 != null)
					{
						Res.outz("type pk= " + char8.cTypePk.ToString());
						char8.cTypePk = msg.reader().readByte();
						if (char8.isAttacPlayerStatus())
						{
							global::Char.myCharz().charFocus = char8;
						}
					}
				}
				for (int num18 = 0; num18 < GameScr.vCharInMap.size(); num18++)
				{
					global::Char char9 = GameScr.findCharInMap(num18);
					if (char9 != null && char9.cTypePk != 0 && char9.cTypePk == global::Char.myCharz().cTypePk)
					{
						if (!global::Char.myCharz().mobFocus.isMobMe)
						{
							global::Char.myCharz().mobFocus = null;
						}
						global::Char.myCharz().npcFocus = null;
						global::Char.myCharz().itemFocus = null;
						break;
					}
				}
				Res.outz("update type pk= ");
				return;
			}
			default:
				goto IL_170C;
			}
			GameCanvas.debug("SA23", 2);
			global::Char.myCharz().xu = msg.reader().readLong();
			global::Char.myCharz().luong = msg.reader().readInt();
			global::Char.myCharz().cHP = msg.readInt3Byte();
			global::Char.myCharz().cMP = msg.readInt3Byte();
			global::Char.myCharz().luongKhoa = msg.reader().readInt();
			global::Char.myCharz().xuStr = Res.formatNumber2(global::Char.myCharz().xu);
			global::Char.myCharz().luongStr = mSystem.numberTostring((long)global::Char.myCharz().luong);
			global::Char.myCharz().luongKhoaStr = mSystem.numberTostring((long)global::Char.myCharz().luongKhoa);
			return;
			IL_1160:
			GameCanvas.debug("SA26", 2);
			global::Char char10 = GameScr.findCharInMap(msg.reader().readInt());
			if (char10 != null)
			{
				char10.cspeed = (int)msg.reader().readByte();
			}
			return;
			IL_147E:
			GameCanvas.debug("SA33", 2);
			global::Char char11 = GameScr.findCharInMap(msg.reader().readInt());
			if (char11 != null)
			{
				char11.cHP = msg.readInt3Byte();
				char11.cHPFull = msg.readInt3Byte();
				char11.cx = (int)msg.reader().readShort();
				char11.cy = (int)msg.reader().readShort();
				char11.statusMe = 1;
				char11.cp3 = 3;
				ServerEffect.addServerEffect(109, char11, 2);
			}
			return;
			IL_170C:
			switch (b)
			{
			case 61:
			{
				string text = msg.reader().readUTF();
				sbyte[] array = new sbyte[msg.reader().readInt()];
				msg.reader().read(ref array);
				if (array.Length == 0)
				{
					array = null;
				}
				if (text.Equals("KSkill"))
				{
					GameScr.gI().onKSkill(array);
				}
				else if (text.Equals("OSkill"))
				{
					GameScr.gI().onOSkill(array);
				}
				else if (text.Equals("CSkill"))
				{
					GameScr.gI().onCSkill(array);
				}
				break;
			}
			case 62:
				Res.outz("ME UPDATE SKILL");
				this.read_UpdateSkill(msg);
				break;
			case 63:
			{
				sbyte b8 = msg.reader().readByte();
				if (b8 > 0)
				{
					GameCanvas.panel.vPlayerMenu_id.removeAllElements();
					InfoDlg.showWait();
					MyVector vPlayerMenu = GameCanvas.panel.vPlayerMenu;
					for (int num19 = 0; num19 < (int)b8; num19++)
					{
						string text2 = msg.reader().readUTF();
						string text3 = msg.reader().readUTF();
						short num20 = msg.reader().readShort();
						GameCanvas.panel.vPlayerMenu_id.addElement(num20.ToString() + string.Empty);
						global::Char.myCharz().charFocus.menuSelect = (int)num20;
						vPlayerMenu.addElement(new Command(text2, 11115, global::Char.myCharz().charFocus)
						{
							caption2 = text3
						});
					}
					InfoDlg.hide();
					GameCanvas.panel.setTabPlayerMenu();
				}
				break;
			}
			}
		}
		catch (Exception ex)
		{
			Cout.println("Loi tai Sub : " + ex.ToString());
		}
		finally
		{
			if (msg != null)
			{
				msg.cleanup();
			}
		}
	}

	// Token: 0x060001F1 RID: 497 RVA: 0x00029D58 File Offset: 0x00027F58
	internal void useSkill(Skill skill)
	{
		if (global::Char.myCharz().myskill == null)
		{
			global::Char.myCharz().myskill = skill;
		}
		else if (skill.template.Equals(global::Char.myCharz().myskill.template))
		{
			global::Char.myCharz().myskill = skill;
		}
		global::Char.myCharz().vSkill.addElement(skill);
		if ((skill.template.type == 1 || skill.template.type == 4 || skill.template.type == 2 || skill.template.type == 3) && (skill.template.maxPoint == 0 || (skill.template.maxPoint > 0 && skill.point > 0)))
		{
			if ((int)skill.template.id == global::Char.myCharz().skillTemplateId)
			{
				Service.gI().selectSkill(global::Char.myCharz().skillTemplateId);
			}
			global::Char.myCharz().vSkillFight.addElement(skill);
		}
	}

	// Token: 0x060001F2 RID: 498 RVA: 0x00029E4C File Offset: 0x0002804C
	public bool readCharInfo(global::Char c, Message msg)
	{
		try
		{
			c.clevel = (int)msg.reader().readByte();
			c.isInvisiblez = msg.reader().readBoolean();
			c.cTypePk = msg.reader().readByte();
			Res.outz(string.Concat(new string[]
			{
				"ADD TYPE PK= ",
				c.cTypePk.ToString(),
				" to player ",
				c.charID.ToString(),
				" @@ ",
				c.cName
			}));
			c.nClass = GameScr.nClasss[(int)msg.reader().readByte()];
			c.cgender = (int)msg.reader().readByte();
			c.head = (int)msg.reader().readShort();
			c.cName = msg.reader().readUTF();
			c.cHP = msg.readInt3Byte();
			c.dHP = c.cHP;
			if (c.cHP == 0)
			{
				c.statusMe = 14;
			}
			c.cHPFull = msg.readInt3Byte();
			if (c.cy >= TileMap.pxh - 100)
			{
				c.isFlyUp = true;
			}
			c.body = (int)msg.reader().readShort();
			c.leg = (int)msg.reader().readShort();
			c.bag = (int)msg.reader().readUnsignedByte();
			Res.outz(string.Concat(new string[]
			{
				" body= ",
				c.body.ToString(),
				" leg= ",
				c.leg.ToString(),
				" bag=",
				c.bag.ToString(),
				"BAG ==",
				c.bag.ToString(),
				"*********************************"
			}));
			c.isShadown = true;
			msg.reader().readByte();
			if (c.wp == -1)
			{
				c.setDefaultWeapon();
			}
			if (c.body == -1)
			{
				c.setDefaultBody();
			}
			if (c.leg == -1)
			{
				c.setDefaultLeg();
			}
			c.cx = (int)msg.reader().readShort();
			c.cy = (int)msg.reader().readShort();
			c.xSd = c.cx;
			c.ySd = c.cy;
			c.eff5BuffHp = (int)msg.reader().readShort();
			c.eff5BuffMp = (int)msg.reader().readShort();
			int num = (int)msg.reader().readByte();
			for (int i = 0; i < num; i++)
			{
				EffectChar effectChar = new EffectChar(msg.reader().readByte(), msg.reader().readInt(), msg.reader().readInt(), msg.reader().readShort());
				c.vEff.addElement(effectChar);
				if (effectChar.template.type == 12 || effectChar.template.type == 11)
				{
					c.isInvisiblez = true;
				}
			}
			return true;
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		return false;
	}

	// Token: 0x060001F3 RID: 499 RVA: 0x0002A15C File Offset: 0x0002835C
	internal void readGetImgByName(Message msg)
	{
		try
		{
			string text = msg.reader().readUTF();
			sbyte b = msg.reader().readByte();
			sbyte[] array = NinjaUtil.readByteArray(msg);
			ImgByName.SetImage(text, this.createImage(array), b);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060001F4 RID: 500 RVA: 0x0002A1B0 File Offset: 0x000283B0
	internal void createItemNew(myReader d)
	{
		try
		{
			this.loadItemNew(d, -1, true);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060001F5 RID: 501 RVA: 0x0002A1DC File Offset: 0x000283DC
	internal void loadItemNew(myReader d, sbyte type, bool isSave)
	{
		try
		{
			d.mark(100000);
			GameScr.vcItem = d.readByte();
			type = d.readByte();
			if (type == 0)
			{
				GameScr.gI().iOptionTemplates = new ItemOptionTemplate[(int)d.readUnsignedByte()];
				for (int i = 0; i < GameScr.gI().iOptionTemplates.Length; i++)
				{
					GameScr.gI().iOptionTemplates[i] = new ItemOptionTemplate();
					GameScr.gI().iOptionTemplates[i].id = i;
					GameScr.gI().iOptionTemplates[i].name = d.readUTF();
					GameScr.gI().iOptionTemplates[i].type = (int)d.readByte();
				}
				if (isSave)
				{
					d.reset();
					sbyte[] array = new sbyte[d.available()];
					d.readFully(ref array);
					Rms.saveRMS("NRitem0", array);
				}
			}
			else if (type == 1)
			{
				ItemTemplates.itemTemplates.clear();
				int num = (int)d.readShort();
				for (int j = 0; j < num; j++)
				{
					ItemTemplates.add(new ItemTemplate((short)j, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBoolean()));
				}
				if (isSave)
				{
					d.reset();
					sbyte[] array2 = new sbyte[d.available()];
					d.readFully(ref array2);
					Rms.saveRMS("NRitem1", array2);
				}
			}
			else if (type == 2)
			{
				int num2 = (int)d.readShort();
				int num3 = (int)d.readShort();
				for (int k = num2; k < num3; k++)
				{
					ItemTemplates.add(new ItemTemplate((short)k, d.readByte(), d.readByte(), d.readUTF(), d.readUTF(), d.readByte(), d.readInt(), d.readShort(), d.readShort(), d.readBoolean()));
				}
				if (isSave)
				{
					d.reset();
					sbyte[] array3 = new sbyte[d.available()];
					d.readFully(ref array3);
					Rms.saveRMS("NRitem2", array3);
					Rms.saveRMS("NRitemVersion", new sbyte[] { GameScr.vcItem });
					LoginScr.isUpdateItem = false;
					if (GameScr.vsData == GameScr.vcData && GameScr.vsMap == GameScr.vcMap && GameScr.vsSkill == GameScr.vcSkill && GameScr.vsItem == GameScr.vcItem)
					{
						GameScr.gI().readDart();
						GameScr.gI().readEfect();
						GameScr.gI().readArrow();
						GameScr.gI().readSkill();
						Service.gI().clientOk();
					}
				}
			}
			else if (type == 100)
			{
				global::Char.Arr_Head_2Fr = this.readArrHead(d);
				if (isSave)
				{
					d.reset();
					sbyte[] array4 = new sbyte[d.available()];
					d.readFully(ref array4);
					Rms.saveRMS("NRitem100", array4);
				}
			}
			else if (type == 101)
			{
				try
				{
					int num4 = (int)d.readShort();
					global::Char.Arr_Head_FlyMove = new short[num4];
					for (int l = 0; l < num4; l++)
					{
						short num5 = d.readShort();
						global::Char.Arr_Head_FlyMove[l] = num5;
					}
					if (isSave)
					{
						d.reset();
						sbyte[] array5 = new sbyte[d.available()];
						d.readFully(ref array5);
						Rms.saveRMS("NRitem101", array5);
					}
				}
				catch (Exception)
				{
					global::Char.Arr_Head_FlyMove = new short[0];
				}
			}
		}
		catch (Exception ex)
		{
			ex.ToString();
		}
	}

	// Token: 0x060001F6 RID: 502 RVA: 0x0002A570 File Offset: 0x00028770
	internal void readFrameBoss(Message msg, int mobTemplateId)
	{
		try
		{
			int num = (int)msg.reader().readByte();
			int[][] array = new int[num][];
			for (int i = 0; i < num; i++)
			{
				int num2 = (int)msg.reader().readByte();
				array[i] = new int[num2];
				for (int j = 0; j < num2; j++)
				{
					array[i][j] = (int)msg.reader().readByte();
				}
			}
			Controller.frameHT_NEWBOSS.put(mobTemplateId.ToString() + string.Empty, array);
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060001F7 RID: 503 RVA: 0x0002A604 File Offset: 0x00028804
	internal int[][] readArrHead(myReader d)
	{
		int[][] array = new int[][] { new int[] { 542, 543 } };
		try
		{
			array = new int[(int)d.readShort()][];
			for (int i = 0; i < array.Length; i++)
			{
				int num = (int)d.readByte();
				array[i] = new int[num];
				for (int j = 0; j < num; j++)
				{
					array[i][j] = (int)d.readShort();
				}
			}
		}
		catch (Exception)
		{
		}
		return array;
	}

	// Token: 0x060001F8 RID: 504 RVA: 0x0002A688 File Offset: 0x00028888
	public void phuban_Info(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if (b == 0)
			{
				this.readPhuBan_CHIENTRUONGNAMEK(msg, (int)b);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060001F9 RID: 505 RVA: 0x0002A6C4 File Offset: 0x000288C4
	internal void readPhuBan_CHIENTRUONGNAMEK(Message msg, int type_PB)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if (b == 0)
			{
				short num = msg.reader().readShort();
				string text = msg.reader().readUTF();
				string text2 = msg.reader().readUTF();
				int num2 = msg.reader().readInt();
				short num3 = msg.reader().readShort();
				int num4 = (int)msg.reader().readByte();
				GameScr.phuban_Info = new InfoPhuBan(type_PB, num, text, text2, num2, num3);
				GameScr.phuban_Info.maxLife = num4;
				GameScr.phuban_Info.updateLife(type_PB, 0, 0);
			}
			else if (b == 1)
			{
				int num5 = msg.reader().readInt();
				int num6 = msg.reader().readInt();
				if (GameScr.phuban_Info != null)
				{
					GameScr.phuban_Info.updatePoint(type_PB, num5, num6);
				}
			}
			else if (b == 2)
			{
				sbyte b2 = msg.reader().readByte();
				short num7 = 0;
				if (b2 == 1)
				{
					num7 = 1;
				}
				else if (b2 == 2)
				{
					num7 = 2;
				}
				short num8 = -1;
				GameScr.phuban_Info = null;
				GameScr.addEffectEnd((int)num7, (int)num8, 0, GameCanvas.hw, GameCanvas.hh, 0, 0, -1, null);
			}
			else if (b == 5)
			{
				short num9 = msg.reader().readShort();
				if (GameScr.phuban_Info != null)
				{
					GameScr.phuban_Info.updateTime(type_PB, num9);
				}
			}
			else if (b == 4)
			{
				int num10 = (int)msg.reader().readByte();
				int num11 = (int)msg.reader().readByte();
				if (GameScr.phuban_Info != null)
				{
					GameScr.phuban_Info.updateLife(type_PB, num10, num11);
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060001FA RID: 506 RVA: 0x0002A864 File Offset: 0x00028A64
	public void read_opt(Message msg)
	{
		try
		{
			sbyte b = msg.reader().readByte();
			if (b == 0)
			{
				short num = msg.reader().readShort();
				global::Char.myCharz().idHat = num;
				SoundMn.gI().getStrOption();
			}
			else if (b == 2)
			{
				int num2 = msg.reader().readInt();
				sbyte b2 = msg.reader().readByte();
				short num3 = msg.reader().readShort();
				string text = num3.ToString() + "," + b2.ToString();
				ImgByName.getImagePath("banner_" + num3.ToString(), ImgByName.hashImagePath);
				GameCanvas.danhHieu.put(num2.ToString() + string.Empty, text);
			}
			else if (b == 3)
			{
				short num4 = msg.reader().readShort();
				SmallImage.createImage((int)num4);
				BackgroudEffect.id_water1 = num4;
			}
			else if (b == 4)
			{
				string text2 = msg.reader().readUTF();
				GameCanvas.messageServer.addElement(text2);
			}
			else if (b == 5)
			{
				string text3 = "\n|ChienTruong|Log: ";
				sbyte b3 = msg.reader().readByte();
				if (b3 == 0)
				{
					GameScr.nCT_team = msg.reader().readUTF();
					GameScr.nCT_TeamA = (GameScr.nCT_TeamB = (int)msg.reader().readByte());
					GameScr.nCT_nBoyBaller = GameScr.nCT_TeamA * 2;
					GameScr.isPaint_CT = false;
					string text4 = text3;
					text3 = string.Concat(new string[]
					{
						text4,
						"\tsub    0|  nCT_team= ",
						GameScr.nCT_team,
						"|nCT_TeamA =",
						GameScr.nCT_TeamA.ToString(),
						"  isPaint_CT=false \n"
					});
				}
				else if (b3 == 1)
				{
					int num5 = msg.reader().readInt();
					sbyte b4 = (GameScr.nCT_floor = msg.reader().readByte());
					GameScr.nCT_timeBallte = (long)(num5 * 1000) + mSystem.currentTimeMillis();
					GameScr.isPaint_CT = true;
					string text5 = text3;
					text3 = string.Concat(new string[]
					{
						text5,
						"\tsub    1 floor= ",
						b4.ToString(),
						"|timeBallte= ",
						num5.ToString(),
						"isPaint_CT=true \n"
					});
				}
				else if (b3 == 2)
				{
					GameScr.nCT_TeamA = (int)msg.reader().readByte();
					GameScr.nCT_TeamB = (int)msg.reader().readByte();
					GameScr.res_CT.removeAllElements();
					sbyte b5 = msg.reader().readByte();
					for (int i = 0; i < (int)b5; i++)
					{
						string text6 = string.Empty + msg.reader().readByte().ToString() + "|" + msg.reader().readUTF() + "|" + msg.reader().readShort() + "|" + msg.reader().readInt();
						GameScr.res_CT.addElement(text6);
					}
					string text7 = text3;
					text3 = string.Concat(new string[]
					{
						text7,
						"\tsub   2|  A= ",
						GameScr.nCT_TeamA.ToString(),
						"|B =",
						GameScr.nCT_TeamB.ToString(),
						"  isPaint_CT=true \n"
					});
				}
				else if (b3 == 3)
				{
					Service.gI().sendCT_ready(b, b3);
					GameScr.nCT_floor = 0;
					GameScr.nCT_timeBallte = 0L;
					GameScr.isPaint_CT = false;
					text3 += "\tsub    3|  isPaint_CT=false \n";
				}
				else if (b3 == 4)
				{
					GameScr.nUSER_CT = (int)msg.reader().readByte();
					GameScr.nUSER_MAX_CT = (int)msg.reader().readByte();
				}
				Res.err(text3 + "END LOG CT.");
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060001FB RID: 507 RVA: 0x0002AC3C File Offset: 0x00028E3C
	public void read_UpdateSkill(Message msg)
	{
		try
		{
			short num = msg.reader().readShort();
			sbyte b = -1;
			try
			{
				b = msg.reader().readSByte();
			}
			catch (Exception)
			{
			}
			if (b == 0)
			{
				short num2 = msg.reader().readShort();
				for (int i = 0; i < global::Char.myCharz().vSkill.size(); i++)
				{
					Skill skill = (Skill)global::Char.myCharz().vSkill.elementAt(i);
					if (skill.skillId == num)
					{
						skill.curExp = num2;
						break;
					}
				}
			}
			else if (b == 1)
			{
				sbyte b2 = msg.reader().readByte();
				for (int j = 0; j < global::Char.myCharz().vSkill.size(); j++)
				{
					Skill skill2 = (Skill)global::Char.myCharz().vSkill.elementAt(j);
					if (skill2.skillId == num)
					{
						for (int k = 0; k < 20; k++)
						{
							ImgByName.getImagePath(string.Concat(new string[]
							{
								"Skills_",
								skill2.template.id.ToString(),
								"_",
								b2.ToString(),
								"_",
								k.ToString()
							}), ImgByName.hashImagePath);
						}
						break;
					}
				}
			}
			else if (b == -1)
			{
				Skill skill3 = Skills.get(num);
				for (int l = 0; l < global::Char.myCharz().vSkill.size(); l++)
				{
					if (((Skill)global::Char.myCharz().vSkill.elementAt(l)).template.id == skill3.template.id)
					{
						global::Char.myCharz().vSkill.setElementAt(skill3, l);
						break;
					}
				}
				for (int m = 0; m < global::Char.myCharz().vSkillFight.size(); m++)
				{
					if (((Skill)global::Char.myCharz().vSkillFight.elementAt(m)).template.id == skill3.template.id)
					{
						global::Char.myCharz().vSkillFight.setElementAt(skill3, m);
						break;
					}
				}
				for (int n = 0; n < GameScr.onScreenSkill.Length; n++)
				{
					if (GameScr.onScreenSkill[n] != null && GameScr.onScreenSkill[n].template.id == skill3.template.id)
					{
						GameScr.onScreenSkill[n] = skill3;
						break;
					}
				}
				for (int num3 = 0; num3 < GameScr.keySkill.Length; num3++)
				{
					if (GameScr.keySkill[num3] != null && GameScr.keySkill[num3].template.id == skill3.template.id)
					{
						GameScr.keySkill[num3] = skill3;
						break;
					}
				}
				if (global::Char.myCharz().myskill.template.id == skill3.template.id)
				{
					global::Char.myCharz().myskill = skill3;
				}
				GameScr.info1.addInfo(mResources.hasJustUpgrade1 + skill3.template.name + mResources.hasJustUpgrade2 + skill3.point.ToString(), 0);
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x04000490 RID: 1168
	protected static Controller me;

	// Token: 0x04000491 RID: 1169
	protected static Controller me2;

	// Token: 0x04000492 RID: 1170
	public Message messWait;

	// Token: 0x04000493 RID: 1171
	public static bool isLoadingData = false;

	// Token: 0x04000494 RID: 1172
	public static bool isConnectOK;

	// Token: 0x04000495 RID: 1173
	public static bool isConnectionFail;

	// Token: 0x04000496 RID: 1174
	public static bool isDisconnected;

	// Token: 0x04000497 RID: 1175
	public static bool isMain;

	// Token: 0x04000498 RID: 1176
	internal float demCount;

	// Token: 0x04000499 RID: 1177
	internal int move;

	// Token: 0x0400049A RID: 1178
	internal int total;

	// Token: 0x0400049B RID: 1179
	public static bool isStopReadMessage;

	// Token: 0x0400049C RID: 1180
	public static MyHashTable frameHT_NEWBOSS = new MyHashTable();

	// Token: 0x0400049D RID: 1181
	public const sbyte PHUBAN_TYPE_CHIENTRUONGNAMEK = 0;

	// Token: 0x0400049E RID: 1182
	public const sbyte PHUBAN_START = 0;

	// Token: 0x0400049F RID: 1183
	public const sbyte PHUBAN_UPDATE_POINT = 1;

	// Token: 0x040004A0 RID: 1184
	public const sbyte PHUBAN_END = 2;

	// Token: 0x040004A1 RID: 1185
	public const sbyte PHUBAN_LIFE = 4;

	// Token: 0x040004A2 RID: 1186
	public const sbyte PHUBAN_INFO = 5;
}
