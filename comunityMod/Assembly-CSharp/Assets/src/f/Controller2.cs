using System;
using Assets.src.g;

namespace Assets.src.f
{
	// Token: 0x020001BA RID: 442
	internal class Controller2
	{
		// Token: 0x060012F1 RID: 4849 RVA: 0x000C7D08 File Offset: 0x000C5F08
		public static void readMessage(Message msg)
		{
			try
			{
				sbyte command = msg.command;
				switch (command)
				{
				case -128:
					Controller2.readInfoEffChar(msg);
					return;
				case -127:
					Controller2.readLuckyRound(msg);
					return;
				case -126:
				{
					sbyte b = msg.reader().readByte();
					Res.outz("type quay= " + b.ToString());
					if (b == 1)
					{
						msg.reader().readByte();
						string text = msg.reader().readUTF();
						string text2 = msg.reader().readUTF();
						GameScr.gI().showWinNumber(text, text2);
					}
					if (b == 0)
					{
						GameScr.gI().showYourNumber(msg.reader().readUTF());
					}
					return;
				}
				case -125:
				{
					ChatTextField.gI().isShow = false;
					string text3 = msg.reader().readUTF();
					Res.outz("titile= " + text3);
					sbyte b2 = msg.reader().readByte();
					ClientInput.gI().setInput((int)b2, text3);
					for (int i = 0; i < (int)b2; i++)
					{
						ClientInput.gI().tf[i].name = msg.reader().readUTF();
						sbyte b3 = msg.reader().readByte();
						if (b3 == 0)
						{
							ClientInput.gI().tf[i].setIputType(TField.INPUT_TYPE_NUMERIC);
						}
						if (b3 == 1)
						{
							ClientInput.gI().tf[i].setIputType(TField.INPUT_TYPE_ANY);
						}
						if (b3 == 2)
						{
							ClientInput.gI().tf[i].setIputType(TField.INPUT_TYPE_PASSWORD);
						}
					}
					return;
				}
				case -124:
				{
					sbyte b4 = msg.reader().readByte();
					sbyte b5 = msg.reader().readByte();
					if (b5 == 0)
					{
						if (b4 == 2)
						{
							int num = msg.reader().readInt();
							if (num == global::Char.myCharz().charID)
							{
								global::Char.myCharz().removeEffect();
							}
							else if (GameScr.findCharInMap(num) != null)
							{
								GameScr.findCharInMap(num).removeEffect();
							}
						}
						int num2 = (int)msg.reader().readUnsignedByte();
						int num3 = msg.reader().readInt();
						if (num2 == 32)
						{
							if (b4 == 1)
							{
								int num4 = msg.reader().readInt();
								if (num3 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().holdEffID = num2;
									GameScr.findCharInMap(num4).setHoldChar(global::Char.myCharz());
								}
								else if (GameScr.findCharInMap(num3) != null && num4 != global::Char.myCharz().charID)
								{
									GameScr.findCharInMap(num3).holdEffID = num2;
									GameScr.findCharInMap(num4).setHoldChar(GameScr.findCharInMap(num3));
								}
								else if (GameScr.findCharInMap(num3) != null && num4 == global::Char.myCharz().charID)
								{
									GameScr.findCharInMap(num3).holdEffID = num2;
									global::Char.myCharz().setHoldChar(GameScr.findCharInMap(num3));
								}
							}
							else if (num3 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().removeHoleEff();
							}
							else if (GameScr.findCharInMap(num3) != null)
							{
								GameScr.findCharInMap(num3).removeHoleEff();
							}
						}
						if (num2 == 33)
						{
							if (b4 == 1)
							{
								if (num3 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().protectEff = true;
								}
								else if (GameScr.findCharInMap(num3) != null)
								{
									GameScr.findCharInMap(num3).protectEff = true;
								}
							}
							else if (num3 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().removeProtectEff();
							}
							else if (GameScr.findCharInMap(num3) != null)
							{
								GameScr.findCharInMap(num3).removeProtectEff();
							}
						}
						if (num2 == 39)
						{
							if (b4 == 1)
							{
								if (num3 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().huytSao = true;
								}
								else if (GameScr.findCharInMap(num3) != null)
								{
									GameScr.findCharInMap(num3).huytSao = true;
								}
							}
							else if (num3 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().removeHuytSao();
							}
							else if (GameScr.findCharInMap(num3) != null)
							{
								GameScr.findCharInMap(num3).removeHuytSao();
							}
						}
						if (num2 == 40)
						{
							if (b4 == 1)
							{
								if (num3 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().blindEff = true;
								}
								else if (GameScr.findCharInMap(num3) != null)
								{
									GameScr.findCharInMap(num3).blindEff = true;
								}
							}
							else if (num3 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().removeBlindEff();
							}
							else if (GameScr.findCharInMap(num3) != null)
							{
								GameScr.findCharInMap(num3).removeBlindEff();
							}
						}
						if (num2 == 41)
						{
							if (b4 == 1)
							{
								if (num3 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().sleepEff = true;
								}
								else if (GameScr.findCharInMap(num3) != null)
								{
									GameScr.findCharInMap(num3).sleepEff = true;
								}
							}
							else if (num3 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().removeSleepEff();
							}
							else if (GameScr.findCharInMap(num3) != null)
							{
								GameScr.findCharInMap(num3).removeSleepEff();
							}
						}
						if (num2 == 42)
						{
							if (b4 == 1)
							{
								if (num3 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().stone = true;
								}
							}
							else if (num3 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().stone = false;
							}
						}
					}
					if (b5 != 1)
					{
						return;
					}
					int num5 = (int)msg.reader().readUnsignedByte();
					sbyte b6 = msg.reader().readByte();
					Res.outz(string.Concat(new string[]
					{
						"modbHoldID= ",
						b6.ToString(),
						" skillID= ",
						num5.ToString(),
						"eff ID= ",
						b4.ToString()
					}));
					if (num5 == 32)
					{
						if (b4 == 1)
						{
							int num6 = msg.reader().readInt();
							if (num6 == global::Char.myCharz().charID)
							{
								GameScr.findMobInMap(b6).holdEffID = num5;
								global::Char.myCharz().setHoldMob(GameScr.findMobInMap(b6));
							}
							else if (GameScr.findCharInMap(num6) != null)
							{
								GameScr.findMobInMap(b6).holdEffID = num5;
								GameScr.findCharInMap(num6).setHoldMob(GameScr.findMobInMap(b6));
							}
						}
						else
						{
							GameScr.findMobInMap(b6).removeHoldEff();
						}
					}
					if (num5 == 40)
					{
						if (b4 == 1)
						{
							GameScr.findMobInMap(b6).blindEff = true;
						}
						else
						{
							GameScr.findMobInMap(b6).removeBlindEff();
						}
					}
					if (num5 == 41)
					{
						if (b4 == 1)
						{
							GameScr.findMobInMap(b6).sleepEff = true;
						}
						else
						{
							GameScr.findMobInMap(b6).removeSleepEff();
						}
					}
					return;
				}
				case -123:
				{
					int num7 = msg.reader().readInt();
					if (GameScr.findCharInMap(num7) != null)
					{
						GameScr.findCharInMap(num7).perCentMp = (int)msg.reader().readByte();
					}
					return;
				}
				case -122:
				{
					Npc npc = GameScr.findNPCInMap(msg.reader().readShort());
					sbyte b7 = msg.reader().readByte();
					npc.duahau = new int[(int)b7];
					Res.outz("N DUA HAU= " + b7.ToString());
					for (int j = 0; j < (int)b7; j++)
					{
						npc.duahau[j] = (int)msg.reader().readShort();
					}
					npc.setStatus(msg.reader().readByte(), msg.reader().readInt());
					return;
				}
				case -121:
					Service.logMap = mSystem.currentTimeMillis() - Service.curCheckMap;
					Service.gI().sendCheckMap();
					return;
				case -120:
					Service.logController = mSystem.currentTimeMillis() - Service.curCheckController;
					Service.gI().sendCheckController();
					return;
				case -119:
					global::Char.myCharz().rank = msg.reader().readInt();
					return;
				case -117:
					GameScr.gI().tMabuEff = 0;
					GameScr.gI().percentMabu = msg.reader().readByte();
					if (GameScr.gI().percentMabu == 100)
					{
						GameScr.gI().mabuEff = true;
					}
					if (GameScr.gI().percentMabu == 101)
					{
						Npc.mabuEff = true;
					}
					return;
				case -116:
					GameScr.canAutoPlay = msg.reader().readByte() == 1;
					return;
				case -115:
					global::Char.myCharz().setPowerInfo(msg.reader().readUTF(), msg.reader().readShort(), msg.reader().readShort(), msg.reader().readShort());
					return;
				case -113:
				{
					sbyte[] array = new sbyte[10];
					for (int k = 0; k < 10; k++)
					{
						array[k] = msg.reader().readByte();
						Res.outz("vlue i= " + array[k].ToString());
					}
					GameScr.gI().onKSkill(array);
					GameScr.gI().onOSkill(array);
					GameScr.gI().onCSkill(array);
					return;
				}
				case -111:
				{
					short num8 = msg.reader().readShort();
					ImageSource.vSource = new MyVector();
					for (int l = 0; l < (int)num8; l++)
					{
						string text4 = msg.reader().readUTF();
						sbyte b8 = msg.reader().readByte();
						ImageSource.vSource.addElement(new ImageSource(text4, b8));
					}
					ImageSource.checkRMS();
					ImageSource.saveRMS();
					return;
				}
				case -110:
				{
					sbyte b9 = msg.reader().readByte();
					if (b9 == 1)
					{
						int num9 = msg.reader().readInt();
						sbyte[] array2 = Rms.loadRMS(num9.ToString() + string.Empty);
						if (array2 == null)
						{
							Service.gI().sendServerData(1, -1, null);
						}
						else
						{
							Service.gI().sendServerData(1, num9, array2);
						}
					}
					if (b9 == 0)
					{
						int num10 = msg.reader().readInt();
						short num11 = msg.reader().readShort();
						sbyte[] array3 = new sbyte[(int)num11];
						msg.reader().read(ref array3, 0, (int)num11);
						Rms.saveRMS(num10.ToString() + string.Empty, array3);
					}
					return;
				}
				case -106:
				{
					short num12 = msg.reader().readShort();
					int num13 = (int)msg.reader().readShort();
					if (ItemTime.isExistItem((int)num12))
					{
						ItemTime.getItemById((int)num12).initTime(num13);
						return;
					}
					ItemTime itemTime = new ItemTime(num12, num13);
					global::Char.vItemTime.addElement(itemTime);
					return;
				}
				case -105:
					TransportScr.gI().time = 0;
					TransportScr.gI().maxTime = msg.reader().readShort();
					TransportScr.gI().last = (TransportScr.gI().curr = mSystem.currentTimeMillis());
					TransportScr.gI().type = msg.reader().readByte();
					TransportScr.gI().switchToMe();
					return;
				case -103:
				{
					sbyte b10 = msg.reader().readByte();
					if (b10 == 0)
					{
						GameCanvas.panel.vFlag.removeAllElements();
						sbyte b11 = msg.reader().readByte();
						for (int m = 0; m < (int)b11; m++)
						{
							Item item = new Item();
							short num14 = msg.reader().readShort();
							if (num14 != -1)
							{
								item.template = ItemTemplates.get(num14);
								sbyte b12 = msg.reader().readByte();
								if (b12 != -1)
								{
									item.itemOption = new ItemOption[(int)b12];
									for (int n = 0; n < item.itemOption.Length; n++)
									{
										int num15 = (int)msg.reader().readUnsignedByte();
										int num16 = (int)msg.reader().readUnsignedShort();
										if (num15 != -1)
										{
											item.itemOption[n] = new ItemOption(num15, num16);
										}
									}
								}
							}
							GameCanvas.panel.vFlag.addElement(item);
						}
						GameCanvas.panel.setTypeFlag();
						GameCanvas.panel.show();
					}
					else if (b10 == 1)
					{
						int num17 = msg.reader().readInt();
						sbyte b13 = msg.reader().readByte();
						Res.outz("---------------actionFlag1:  " + num17.ToString() + " : " + b13.ToString());
						if (num17 == global::Char.myCharz().charID)
						{
							global::Char.myCharz().cFlag = b13;
						}
						else if (GameScr.findCharInMap(num17) != null)
						{
							GameScr.findCharInMap(num17).cFlag = b13;
						}
						GameScr.gI().getFlagImage(num17, b13);
					}
					else if (b10 == 2)
					{
						sbyte b14 = msg.reader().readByte();
						int num18 = (int)msg.reader().readShort();
						PKFlag pkflag = new PKFlag();
						pkflag.cflag = b14;
						pkflag.IDimageFlag = num18;
						GameScr.vFlag.addElement(pkflag);
						for (int num19 = 0; num19 < GameScr.vFlag.size(); num19++)
						{
							PKFlag pkflag2 = (PKFlag)GameScr.vFlag.elementAt(num19);
							Res.outz(string.Concat(new string[]
							{
								"i: ",
								num19.ToString(),
								"  cflag: ",
								pkflag2.cflag.ToString(),
								"   IDimageFlag: ",
								pkflag2.IDimageFlag.ToString()
							}));
						}
						for (int num20 = 0; num20 < GameScr.vCharInMap.size(); num20++)
						{
							global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(num20);
							if (@char != null && @char.cFlag == b14)
							{
								@char.flagImage = num18;
							}
						}
						if (global::Char.myCharz().cFlag == b14)
						{
							global::Char.myCharz().flagImage = num18;
						}
					}
					return;
				}
				case -102:
				{
					sbyte b15 = msg.reader().readByte();
					if (b15 != 0 && b15 == 1)
					{
						GameCanvas.loginScr.isLogin2 = false;
						Service.gI().login(Rms.loadRMSString("acc"), Rms.loadRMSString("pass"), GameMidlet.VERSION, 0);
						LoginScr.isLoggingIn = true;
					}
					return;
				}
				case -101:
				{
					GameCanvas.loginScr.isLogin2 = true;
					GameCanvas.connect();
					string text5 = msg.reader().readUTF();
					Rms.saveRMSString("userAo" + ServerListScreen.ipSelect.ToString(), text5);
					Service.gI().setClientType();
					Service.gI().login(text5, string.Empty, GameMidlet.VERSION, 1);
					return;
				}
				case -100:
				{
					InfoDlg.hide();
					bool flag = false;
					if (GameCanvas.w > 2 * Panel.WIDTH_PANEL)
					{
						flag = true;
					}
					sbyte b16 = msg.reader().readByte();
					if (b16 < 0)
					{
						return;
					}
					Res.outz("t Indxe= " + b16.ToString());
					GameCanvas.panel.maxPageShop[(int)b16] = (int)msg.reader().readByte();
					GameCanvas.panel.currPageShop[(int)b16] = (int)msg.reader().readByte();
					Res.outz("max page= " + GameCanvas.panel.maxPageShop[(int)b16].ToString() + " curr page= " + GameCanvas.panel.currPageShop[(int)b16].ToString());
					int num21 = (int)msg.reader().readUnsignedByte();
					global::Char.myCharz().arrItemShop[(int)b16] = new Item[num21];
					for (int num22 = 0; num22 < num21; num22++)
					{
						short num23 = msg.reader().readShort();
						if (num23 != -1)
						{
							Res.outz("template id= " + num23.ToString());
							global::Char.myCharz().arrItemShop[(int)b16][num22] = new Item();
							global::Char.myCharz().arrItemShop[(int)b16][num22].template = ItemTemplates.get(num23);
							global::Char.myCharz().arrItemShop[(int)b16][num22].itemId = (int)msg.reader().readShort();
							global::Char.myCharz().arrItemShop[(int)b16][num22].buyCoin = msg.reader().readInt();
							global::Char.myCharz().arrItemShop[(int)b16][num22].buyGold = msg.reader().readInt();
							global::Char.myCharz().arrItemShop[(int)b16][num22].buyType = msg.reader().readByte();
							global::Char.myCharz().arrItemShop[(int)b16][num22].quantity = msg.reader().readInt();
							global::Char.myCharz().arrItemShop[(int)b16][num22].isMe = msg.reader().readByte();
							Panel.strWantToBuy = mResources.say_wat_do_u_want_to_buy;
							sbyte b17 = msg.reader().readByte();
							if (b17 != -1)
							{
								global::Char.myCharz().arrItemShop[(int)b16][num22].itemOption = new ItemOption[(int)b17];
								for (int num24 = 0; num24 < global::Char.myCharz().arrItemShop[(int)b16][num22].itemOption.Length; num24++)
								{
									int num25 = (int)msg.reader().readUnsignedByte();
									int num26 = (int)msg.reader().readUnsignedShort();
									if (num25 != -1)
									{
										global::Char.myCharz().arrItemShop[(int)b16][num22].itemOption[num24] = new ItemOption(num25, num26);
										global::Char.myCharz().arrItemShop[(int)b16][num22].compare = GameCanvas.panel.getCompare(global::Char.myCharz().arrItemShop[(int)b16][num22]);
									}
								}
							}
							if (msg.reader().readByte() == 1)
							{
								int num27 = (int)msg.reader().readShort();
								int num28 = (int)msg.reader().readShort();
								int num29 = (int)msg.reader().readShort();
								int num30 = (int)msg.reader().readShort();
								global::Char.myCharz().arrItemShop[(int)b16][num22].setPartTemp(num27, num28, num29, num30);
							}
							if (GameMidlet.intVERSION >= 237)
							{
								global::Char.myCharz().arrItemShop[(int)b16][num22].nameNguoiKyGui = msg.reader().readUTF();
								Res.err("nguoi ki gui  " + global::Char.myCharz().arrItemShop[(int)b16][num22].nameNguoiKyGui);
							}
						}
					}
					if (flag)
					{
						GameCanvas.panel2.setTabKiGui();
					}
					GameCanvas.panel.setTabShop();
					GameCanvas.panel.cmy = (GameCanvas.panel.cmtoY = 0);
					return;
				}
				case -89:
					GameCanvas.open3Hour = msg.reader().readByte() == 1;
					return;
				}
				switch (command)
				{
				case 121:
					mSystem.publicID = msg.reader().readUTF();
					mSystem.strAdmob = msg.reader().readUTF();
					Res.outz("SHOW AD public ID= " + mSystem.publicID);
					mSystem.createAdmob();
					return;
				case 122:
				{
					short num31 = msg.reader().readShort();
					Res.outz("second login = " + num31.ToString());
					LoginScr.timeLogin = num31;
					LoginScr.currTimeLogin = (LoginScr.lastTimeLogin = mSystem.currentTimeMillis());
					GameCanvas.endDlg();
					return;
				}
				case 123:
				{
					Res.outz("SET POSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSSss");
					int num32 = msg.reader().readInt();
					short num33 = msg.reader().readShort();
					short num34 = msg.reader().readShort();
					sbyte b18 = msg.reader().readByte();
					global::Char char2 = null;
					if (num32 == global::Char.myCharz().charID)
					{
						char2 = global::Char.myCharz();
					}
					else if (GameScr.findCharInMap(num32) != null)
					{
						char2 = GameScr.findCharInMap(num32);
					}
					if (char2 != null)
					{
						ServerEffect.addServerEffect((b18 != 0) ? 173 : 60, char2, 1);
						char2.setPos(num33, num34, b18);
					}
					return;
				}
				case 124:
				{
					short num35 = msg.reader().readShort();
					string text6 = msg.reader().readUTF();
					Res.outz("noi chuyen = " + text6 + "npc ID= " + num35.ToString());
					Npc npc2 = GameScr.findNPCInMap(num35);
					if (npc2 != null)
					{
						npc2.addInfo(text6);
					}
					return;
				}
				case 125:
				{
					sbyte b19 = msg.reader().readByte();
					int num36 = msg.reader().readInt();
					if (num36 == global::Char.myCharz().charID)
					{
						global::Char.myCharz().setFusion(b19);
					}
					else if (GameScr.findCharInMap(num36) != null)
					{
						GameScr.findCharInMap(num36).setFusion(b19);
					}
					return;
				}
				case 127:
					Controller2.readInfoRada(msg);
					return;
				}
				switch (command)
				{
				case 48:
					ServerListScreen.ipSelect = (int)msg.reader().readByte();
					GameCanvas.instance.doResetToLoginScr(GameCanvas.serverScreen);
					Session_ME.gI().close();
					GameCanvas.endDlg();
					ServerListScreen.waitToLogin = true;
					return;
				case 51:
				{
					Mabu mabu = (Mabu)GameScr.findCharInMap(msg.reader().readInt());
					sbyte b20 = msg.reader().readByte();
					short num37 = msg.reader().readShort();
					short num38 = msg.reader().readShort();
					sbyte b21 = msg.reader().readByte();
					global::Char[] array4 = new global::Char[(int)b21];
					int[] array5 = new int[(int)b21];
					for (int num39 = 0; num39 < (int)b21; num39++)
					{
						int num40 = msg.reader().readInt();
						Res.outz("char ID=" + num40.ToString());
						array4[num39] = null;
						if (num40 != global::Char.myCharz().charID)
						{
							array4[num39] = GameScr.findCharInMap(num40);
						}
						else
						{
							array4[num39] = global::Char.myCharz();
						}
						array5[num39] = msg.reader().readInt();
					}
					mabu.setSkill(b20, num37, num38, array4, array5);
					return;
				}
				case 52:
				{
					sbyte b22 = msg.reader().readByte();
					if (b22 == 1)
					{
						int num41 = msg.reader().readInt();
						if (num41 == global::Char.myCharz().charID)
						{
							global::Char.myCharz().setMabuHold(true);
							global::Char.myCharz().cx = (int)msg.reader().readShort();
							global::Char.myCharz().cy = (int)msg.reader().readShort();
						}
						else
						{
							global::Char char3 = GameScr.findCharInMap(num41);
							if (char3 != null)
							{
								char3.setMabuHold(true);
								char3.cx = (int)msg.reader().readShort();
								char3.cy = (int)msg.reader().readShort();
							}
						}
					}
					if (b22 == 0)
					{
						int num42 = msg.reader().readInt();
						if (num42 == global::Char.myCharz().charID)
						{
							global::Char.myCharz().setMabuHold(false);
						}
						else
						{
							global::Char char4 = GameScr.findCharInMap(num42);
							if (char4 != null)
							{
								char4.setMabuHold(false);
							}
						}
					}
					if (b22 == 2)
					{
						int num43 = msg.reader().readInt();
						int num44 = msg.reader().readInt();
						((Mabu)GameScr.findCharInMap(num43)).eat(num44);
					}
					if (b22 == 3)
					{
						GameScr.mabuPercent = msg.reader().readByte();
					}
					return;
				}
				}
				switch (command)
				{
				case 100:
				{
					sbyte b23 = msg.reader().readByte();
					sbyte b24 = msg.reader().readByte();
					Item item2 = null;
					if (b23 == 0)
					{
						item2 = global::Char.myCharz().arrItemBody[(int)b24];
					}
					if (b23 == 1)
					{
						item2 = global::Char.myCharz().arrItemBag[(int)b24];
					}
					short num45 = msg.reader().readShort();
					if (num45 != -1)
					{
						item2.template = ItemTemplates.get(num45);
						item2.quantity = msg.reader().readInt();
						item2.info = msg.reader().readUTF();
						item2.content = msg.reader().readUTF();
						sbyte b25 = msg.reader().readByte();
						if (b25 != 0)
						{
							item2.itemOption = new ItemOption[(int)b25];
							for (int num46 = 0; num46 < item2.itemOption.Length; num46++)
							{
								int num47 = (int)msg.reader().readUnsignedByte();
								Res.outz("id o= " + num47.ToString());
								int num48 = (int)msg.reader().readUnsignedShort();
								if (num47 != -1)
								{
									item2.itemOption[num46] = new ItemOption(num47, num48);
								}
							}
						}
						if (item2.quantity <= 0)
						{
						}
					}
					break;
				}
				case 101:
				{
					Res.outz("big boss--------------------------------------------------");
					BigBoss bigBoss = Mob.getBigBoss();
					if (bigBoss != null)
					{
						sbyte b26 = msg.reader().readByte();
						if (b26 == 0 || b26 == 1 || b26 == 2 || b26 == 4 || b26 == 3)
						{
							if (b26 == 3)
							{
								bigBoss.xTo = (bigBoss.xFirst = (int)msg.reader().readShort());
								bigBoss.yTo = (bigBoss.yFirst = (int)msg.reader().readShort());
								bigBoss.setFly();
							}
							else
							{
								sbyte b27 = msg.reader().readByte();
								Res.outz("CHUONG nChar= " + b27.ToString());
								global::Char[] array6 = new global::Char[(int)b27];
								int[] array7 = new int[(int)b27];
								for (int num49 = 0; num49 < (int)b27; num49++)
								{
									int num50 = msg.reader().readInt();
									Res.outz("char ID=" + num50.ToString());
									array6[num49] = null;
									if (num50 != global::Char.myCharz().charID)
									{
										array6[num49] = GameScr.findCharInMap(num50);
									}
									else
									{
										array6[num49] = global::Char.myCharz();
									}
									array7[num49] = msg.reader().readInt();
								}
								bigBoss.setAttack(array6, array7, b26);
							}
						}
						if (b26 == 5)
						{
							bigBoss.haftBody = true;
							bigBoss.status = 2;
						}
						if (b26 == 6)
						{
							bigBoss.getDataB2();
							bigBoss.x = (int)msg.reader().readShort();
							bigBoss.y = (int)msg.reader().readShort();
						}
						if (b26 == 7)
						{
							bigBoss.setAttack(null, null, b26);
						}
						if (b26 == 8)
						{
							bigBoss.xTo = (bigBoss.xFirst = (int)msg.reader().readShort());
							bigBoss.yTo = (bigBoss.yFirst = (int)msg.reader().readShort());
							bigBoss.status = 2;
						}
						if (b26 == 9)
						{
							bigBoss.x = (bigBoss.y = (bigBoss.xTo = (bigBoss.yTo = (bigBoss.xFirst = (bigBoss.yFirst = -1000)))));
						}
					}
					break;
				}
				case 102:
				{
					sbyte b28 = msg.reader().readByte();
					if (b28 == 0 || b28 == 1 || b28 == 2 || b28 == 6)
					{
						BigBoss2 bigBoss2 = Mob.getBigBoss2();
						if (bigBoss2 == null)
						{
							break;
						}
						if (b28 == 6)
						{
							bigBoss2.x = (bigBoss2.y = (bigBoss2.xTo = (bigBoss2.yTo = (bigBoss2.xFirst = (bigBoss2.yFirst = -1000)))));
							break;
						}
						sbyte b29 = msg.reader().readByte();
						global::Char[] array8 = new global::Char[(int)b29];
						int[] array9 = new int[(int)b29];
						for (int num51 = 0; num51 < (int)b29; num51++)
						{
							int num52 = msg.reader().readInt();
							array8[num51] = null;
							if (num52 != global::Char.myCharz().charID)
							{
								array8[num51] = GameScr.findCharInMap(num52);
							}
							else
							{
								array8[num51] = global::Char.myCharz();
							}
							array9[num51] = msg.reader().readInt();
						}
						bigBoss2.setAttack(array8, array9, b28);
					}
					if (b28 == 3 || b28 == 4 || b28 == 5 || b28 == 7)
					{
						BachTuoc bachTuoc = Mob.getBachTuoc();
						if (bachTuoc == null)
						{
							break;
						}
						if (b28 == 7)
						{
							bachTuoc.x = (bachTuoc.y = (bachTuoc.xTo = (bachTuoc.yTo = (bachTuoc.xFirst = (bachTuoc.yFirst = -1000)))));
							break;
						}
						if (b28 == 3 || b28 == 4)
						{
							sbyte b30 = msg.reader().readByte();
							global::Char[] array10 = new global::Char[(int)b30];
							int[] array11 = new int[(int)b30];
							for (int num53 = 0; num53 < (int)b30; num53++)
							{
								int num54 = msg.reader().readInt();
								array10[num53] = null;
								if (num54 != global::Char.myCharz().charID)
								{
									array10[num53] = GameScr.findCharInMap(num54);
								}
								else
								{
									array10[num53] = global::Char.myCharz();
								}
								array11[num53] = msg.reader().readInt();
							}
							bachTuoc.setAttack(array10, array11, b28);
						}
						if (b28 == 5)
						{
							bachTuoc.move(msg.reader().readShort());
						}
					}
					if (b28 > 9 && b28 < 30)
					{
						Controller2.readActionBoss(msg, (int)b28);
					}
					break;
				}
				default:
					if (command != 113)
					{
						if (command == 114)
						{
							try
							{
								msg.reader().readUTF();
								mSystem.curINAPP = msg.reader().readByte();
								mSystem.maxINAPP = msg.reader().readByte();
								break;
							}
							catch (Exception)
							{
								break;
							}
						}
						if (command != 31)
						{
							if (command != 42)
							{
								if (command == 93)
								{
									string text7 = Res.changeString(msg.reader().readUTF());
									GameScr.gI().chatVip(text7);
								}
							}
							else
							{
								GameCanvas.endDlg();
								LoginScr.isContinueToLogin = false;
								global::Char.isLoadingMap = false;
								sbyte b31 = msg.reader().readByte();
								if (GameCanvas.registerScr == null)
								{
									GameCanvas.registerScr = new RegisterScreen(b31);
								}
								GameCanvas.registerScr.switchToMe();
							}
						}
						else
						{
							int num55 = msg.reader().readInt();
							if (msg.reader().readByte() == 1)
							{
								short num56 = msg.reader().readShort();
								sbyte b32 = -1;
								int[] array12 = null;
								short num57 = 0;
								short num58 = 0;
								try
								{
									b32 = msg.reader().readByte();
									if (b32 > 0)
									{
										sbyte b33 = msg.reader().readByte();
										array12 = new int[(int)b33];
										for (int num59 = 0; num59 < (int)b33; num59++)
										{
											array12[num59] = (int)msg.reader().readByte();
										}
										num57 = msg.reader().readShort();
										num58 = msg.reader().readShort();
									}
								}
								catch (Exception)
								{
								}
								if (num55 == global::Char.myCharz().charID)
								{
									global::Char.myCharz().petFollow = new PetFollow();
									global::Char.myCharz().petFollow.smallID = num56;
									if (b32 > 0)
									{
										global::Char.myCharz().petFollow.SetImg((int)b32, array12, (int)num57, (int)num58);
									}
								}
								else
								{
									global::Char char5 = GameScr.findCharInMap(num55);
									char5.petFollow = new PetFollow();
									char5.petFollow.smallID = num56;
									if (b32 > 0)
									{
										char5.petFollow.SetImg((int)b32, array12, (int)num57, (int)num58);
									}
								}
							}
							else if (num55 == global::Char.myCharz().charID)
							{
								global::Char.myCharz().petFollow.remove();
								global::Char.myCharz().petFollow = null;
							}
							else
							{
								global::Char char6 = GameScr.findCharInMap(num55);
								char6.petFollow.remove();
								char6.petFollow = null;
							}
						}
					}
					else
					{
						int num60 = 0;
						int num61 = 0;
						int num62 = 0;
						short num63 = 0;
						short num64 = 0;
						short num65 = -1;
						try
						{
							num60 = (int)msg.reader().readByte();
							num61 = (int)msg.reader().readByte();
							num62 = (int)msg.reader().readUnsignedByte();
							num63 = msg.reader().readShort();
							num64 = msg.reader().readShort();
							num65 = msg.reader().readShort();
						}
						catch (Exception)
						{
						}
						EffecMn.addEff(new Effect(num62, (int)num63, (int)num64, num61, num60, (int)num65));
					}
					break;
				}
			}
			catch (Exception ex)
			{
				Res.outz("=====> Controller2 " + ex.StackTrace);
			}
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000C9C18 File Offset: 0x000C7E18
		internal static void readLuckyRound(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					sbyte b2 = msg.reader().readByte();
					short[] array = new short[(int)b2];
					for (int i = 0; i < (int)b2; i++)
					{
						array[i] = msg.reader().readShort();
					}
					sbyte b3 = msg.reader().readByte();
					int num = msg.reader().readInt();
					short num2 = msg.reader().readShort();
					CrackBallScr.gI().SetCrackBallScr(array, (byte)b3, num, num2);
				}
				else if (b == 1)
				{
					sbyte b4 = msg.reader().readByte();
					short[] array2 = new short[(int)b4];
					for (int j = 0; j < (int)b4; j++)
					{
						array2[j] = msg.reader().readShort();
					}
					CrackBallScr.gI().DoneCrackBallScr(array2);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000C9CFC File Offset: 0x000C7EFC
		internal static void readInfoRada(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				if (b == 0)
				{
					RadarScr.gI();
					MyVector myVector = new MyVector(string.Empty);
					short num = msg.reader().readShort();
					int num2 = 0;
					for (int i = 0; i < (int)num; i++)
					{
						Info_RadaScr info_RadaScr = new Info_RadaScr();
						int num3 = (int)msg.reader().readShort();
						int num4 = i + 1;
						int num5 = (int)msg.reader().readShort();
						sbyte b2 = msg.reader().readByte();
						sbyte b3 = msg.reader().readByte();
						sbyte b4 = msg.reader().readByte();
						short num6 = -1;
						global::Char @char = null;
						sbyte b5 = msg.reader().readByte();
						if (b5 == 0)
						{
							num6 = msg.reader().readShort();
						}
						else
						{
							@char = Info_RadaScr.SetCharInfo((int)msg.reader().readShort(), (int)msg.reader().readShort(), (int)msg.reader().readShort(), (int)msg.reader().readShort());
						}
						string text = msg.reader().readUTF();
						string text2 = msg.reader().readUTF();
						sbyte b6 = msg.reader().readByte();
						sbyte b7 = msg.reader().readByte();
						sbyte b8 = msg.reader().readByte();
						ItemOption[] array = null;
						if (b8 != 0)
						{
							array = new ItemOption[(int)b8];
							for (int j = 0; j < array.Length; j++)
							{
								int num7 = (int)msg.reader().readUnsignedByte();
								int num8 = (int)msg.reader().readUnsignedShort();
								sbyte b9 = msg.reader().readByte();
								if (num7 != -1)
								{
									array[j] = new ItemOption(num7, num8);
									array[j].activeCard = b9;
								}
							}
						}
						info_RadaScr.SetInfo(num3, num4, num5, b2, b5, num6, text, text2, @char, array);
						info_RadaScr.SetLevel(b6);
						info_RadaScr.SetUse(b7);
						info_RadaScr.SetAmount(b3, b4);
						myVector.addElement(info_RadaScr);
						if (b6 > 0)
						{
							num2++;
						}
					}
					RadarScr.gI().SetRadarScr(myVector, num2, (int)num);
					RadarScr.gI().switchToMe();
				}
				else if (b == 1)
				{
					int num9 = (int)msg.reader().readShort();
					sbyte b10 = msg.reader().readByte();
					if (Info_RadaScr.GetInfo(RadarScr.list, num9) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.list, num9).SetUse(b10);
					}
					RadarScr.SetListUse();
				}
				else if (b == 2)
				{
					int num10 = (int)msg.reader().readShort();
					sbyte b11 = msg.reader().readByte();
					int num11 = 0;
					for (int k = 0; k < RadarScr.list.size(); k++)
					{
						Info_RadaScr info_RadaScr2 = (Info_RadaScr)RadarScr.list.elementAt(k);
						if (info_RadaScr2 != null)
						{
							if (info_RadaScr2.id == num10)
							{
								info_RadaScr2.SetLevel(b11);
							}
							if (info_RadaScr2.level > 0)
							{
								num11++;
							}
						}
					}
					RadarScr.SetNum(num11, RadarScr.list.size());
					if (Info_RadaScr.GetInfo(RadarScr.listUse, num10) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.listUse, num10).SetLevel(b11);
					}
				}
				else if (b == 3)
				{
					int num12 = (int)msg.reader().readShort();
					sbyte b12 = msg.reader().readByte();
					sbyte b13 = msg.reader().readByte();
					if (Info_RadaScr.GetInfo(RadarScr.list, num12) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.list, num12).SetAmount(b12, b13);
					}
					if (Info_RadaScr.GetInfo(RadarScr.listUse, num12) != null)
					{
						Info_RadaScr.GetInfo(RadarScr.listUse, num12).SetAmount(b12, b13);
					}
				}
				else if (b == 4)
				{
					int num13 = msg.reader().readInt();
					short num14 = msg.reader().readShort();
					global::Char char2 = ((num13 != global::Char.myCharz().charID) ? GameScr.findCharInMap(num13) : global::Char.myCharz());
					if (char2 != null)
					{
						char2.idAuraEff = num14;
						char2.idEff_Set_Item = (short)msg.reader().readByte();
					}
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000CA0F4 File Offset: 0x000C82F4
		internal static void readInfoEffChar(Message msg)
		{
			try
			{
				sbyte b = msg.reader().readByte();
				int num = msg.reader().readInt();
				global::Char @char = ((num != global::Char.myCharz().charID) ? GameScr.findCharInMap(num) : global::Char.myCharz());
				if (b == 0)
				{
					int num2 = (int)msg.reader().readShort();
					int num3 = (int)msg.reader().readByte();
					int num4 = (int)msg.reader().readByte();
					short num5 = msg.reader().readShort();
					sbyte b2 = msg.reader().readByte();
					if (@char != null)
					{
						@char.addEffChar(new Effect(num2, @char, num3, num4, (int)num5, b2));
					}
				}
				else if (b == 1)
				{
					int num6 = (int)msg.reader().readShort();
					if (@char != null)
					{
						@char.removeEffChar(0, num6);
					}
				}
				else if (b == 2 && @char != null)
				{
					@char.removeEffChar(-1, 0);
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000CA1D8 File Offset: 0x000C83D8
		internal static void readActionBoss(Message msg, int actionBoss)
		{
			try
			{
				NewBoss newBoss = Mob.getNewBoss(msg.reader().readByte());
				if (newBoss != null)
				{
					if (actionBoss == 10)
					{
						newBoss.move(msg.reader().readShort(), msg.reader().readShort());
					}
					if (actionBoss >= 11 && actionBoss <= 20)
					{
						sbyte b = msg.reader().readByte();
						global::Char[] array = new global::Char[(int)b];
						int[] array2 = new int[(int)b];
						for (int i = 0; i < (int)b; i++)
						{
							int num = msg.reader().readInt();
							array[i] = null;
							if (num != global::Char.myCharz().charID)
							{
								array[i] = GameScr.findCharInMap(num);
							}
							else
							{
								array[i] = global::Char.myCharz();
							}
							array2[i] = msg.reader().readInt();
						}
						newBoss.setAttack(array, array2, (sbyte)(actionBoss - 10), msg.reader().readByte());
					}
					if (actionBoss == 21)
					{
						newBoss.xTo = (int)msg.reader().readShort();
						newBoss.yTo = (int)msg.reader().readShort();
						newBoss.setFly();
					}
					if (actionBoss == 23)
					{
						newBoss.setDie();
					}
				}
			}
			catch (Exception)
			{
			}
		}
	}
}
