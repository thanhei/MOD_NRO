using System;
using System.Collections.Generic;
using System.Text;

namespace Mod.DungPham.KoiOctiiu957
{
	// Token: 0x020000EF RID: 239
	public class AutoMap : IActionListener, IChatable
	{
		public static AutoMap getInstance()
		{
			if (AutoMap._Instance == null)
			{
				AutoMap._Instance = new AutoMap();
			}
			return AutoMap._Instance;
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000A21EC File Offset: 0x000A03EC
		public static void Update()
		{
			long num = mSystem.currentTimeMillis();
			if (global::Char.myCharz().meDead)
			{
				AutoMap.lastWaitTime = num + 1000L;
			}
			if (TileMap.mapID == AutoMap.IdMapEnd)
			{
				AutoMap.FinishXmap();
				return;
			}
			// Tính delay từ lúc load xong map mới
			if (TileMap.mapID != AutoMap.lastMapID || global::Char.isLoadingMap)
			{
				AutoMap.lastMapID = TileMap.mapID;
				AutoMap.timeArrivedMap = num;
			}
			bool flag = false;
			if (TileMap.mapID == 21 || TileMap.mapID == 22 || TileMap.mapID == 23)
			{
				if (AutoMap.isEatChicken)
				{
					for (int i = 0; i < GameScr.vItemMap.size(); i++)
					{
						ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
						if ((itemMap.playerId == global::Char.myCharz().charID || itemMap.playerId == -1) && itemMap.template.id == 74)
						{
							flag = true;
							global::Char.myCharz().itemFocus = itemMap;
							if (num - AutoMap.lastWaitTime > 600L)
							{
								AutoMap.lastWaitTime = num;
								Service.gI().pickItem(global::Char.myCharz().itemFocus.itemMapID);
								return;
							}
						}
					}
				}
				if (AutoMap.isXmaping && AutoMap.isHarvestPean && GameScr.hpPotion < 10 && GameScr.gI().magicTree.currPeas > 0 && num - AutoMap.lastWaitTime > 500L)
				{
					AutoMap.lastWaitTime = num;
					Service.gI().openMenu(4);
					Service.gI().menu(4, 0, 0);
				}
			}
			if (AutoMap.isXmaping && !flag && !global::Char.isLoadingMap && num - AutoMap.lastWaitTime > 250L && num - AutoMap.timeArrivedMap >= (long)AutoMap.xmapDelay && GameCanvas.gameTick % 4 == 0)
			{
				bool flag2 = true;
				if (AutoMap.isFutureMap(AutoMap.IdMapEnd))
				{
					if (flag2 && TileMap.mapID == 27 && GameScr.findNPCInMap(38) == null)
					{
						flag2 = false;
						AutoMap.UpdateXmap(28);
					}
					if (flag2 && TileMap.mapID == 29 && GameScr.findNPCInMap(38) == null)
					{
						flag2 = false;
						AutoMap.UpdateXmap(28);
					}
					if (flag2 && TileMap.mapID == 28 && GameScr.findNPCInMap(38) == null)
					{
						flag2 = false;
						if (global::Char.myCharz().cx < TileMap.pxw / 2)
						{
							AutoMap.UpdateXmap(29);
						}
						else
						{
							AutoMap.UpdateXmap(27);
						}
					}
				}
				if (flag2)
				{
					AutoMap.UpdateXmap(AutoMap.IdMapEnd);
				}
			}
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000A2410 File Offset: 0x000A0610
		public void perform(int idAction, object p)
		{
			switch (idAction)
			{
			case 1:
				AutoMap.ShowPlanetMenu();
				return;
			case 2:
				AutoMap.isEatChicken = !AutoMap.isEatChicken;
				GameScr.info1.addInfo("Ăn Đùi Gà\n" + (AutoMap.isEatChicken ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				if (AutoMap.isSaveData)
				{
					Rms.saveRMSInt("AutoMapIsEatChicken", AutoMap.isEatChicken ? 1 : 0);
					return;
				}
				break;
			case 3:
				AutoMap.isHarvestPean = !AutoMap.isHarvestPean;
				GameScr.info1.addInfo("Thu Đậu\n" + (AutoMap.isHarvestPean ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				if (AutoMap.isSaveData)
				{
					Rms.saveRMSInt("AutoMapIsHarvestPean", AutoMap.isHarvestPean ? 1 : 0);
					return;
				}
				break;
			case 4:
				AutoMap.isUseCapsule = !AutoMap.isUseCapsule;
				GameScr.info1.addInfo("Sử Dụng Capsule\n" + (AutoMap.isUseCapsule ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				if (AutoMap.isSaveData)
				{
					Rms.saveRMSInt("AutoMapIsUseCsb", AutoMap.isUseCapsule ? 1 : 0);
					return;
				}
				break;
			case 5:
				AutoMap.isSaveData = !AutoMap.isSaveData;
				GameScr.info1.addInfo("Lưu Cài Đặt Auto Map\n" + (AutoMap.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), 0);
				Rms.saveRMSInt("AutoMapIsSaveRms", AutoMap.isSaveData ? 1 : 0);
				if (AutoMap.isSaveData)
				{
					AutoMap.SaveData();
					return;
				}
				break;
			case 6:
				AutoMap.ShowMapsMenu((int[])p);
				return;
			case 8:
				ChatTextField.gI().strChat = AutoMap.inputXmapDelay[0];
				ChatTextField.gI().tfChat.name = AutoMap.inputXmapDelay[1];
				ChatTextField.gI().startChat2(AutoMap.getInstance(), string.Empty);
				return;
			case 7:
				AutoMap.isXmaping = true;
				AutoMap.IdMapEnd = (int)p;
				GameScr.info1.addInfo("Go to " + TileMap.mapNames[AutoMap.IdMapEnd], 0);
				return;
			default:
				return;
			}
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000A25DC File Offset: 0x000A07DC
		public static void ShowMenu()
		{
			AutoMap.LoadData();
			MyVector myVector = new MyVector();
			myVector.addElement(new Command("Load Map", AutoMap.getInstance(), 1, null));
			myVector.addElement(new Command("Ăn Đùi Gà\n" + (AutoMap.isEatChicken ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoMap.getInstance(), 2, null));
			myVector.addElement(new Command("Thu Đậu\n" + (AutoMap.isHarvestPean ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoMap.getInstance(), 3, null));
			myVector.addElement(new Command("Sử Dụng Capsule\n" + (AutoMap.isUseCapsule ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoMap.getInstance(), 4, null));
			myVector.addElement(new Command("Delay Qua Map\n[" + AutoMap.xmapDelay.ToString() + "ms]", AutoMap.getInstance(), 8, null));
			myVector.addElement(new Command("Lưu Cài Đặt\n" + (AutoMap.isSaveData ? "[STATUS: ON]" : "[STATUS: OFF]"), AutoMap.getInstance(), 5, null));
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000A26D4 File Offset: 0x000A08D4
		private static void ShowPlanetMenu()
		{
			MyVector myVector = new MyVector();
			foreach (KeyValuePair<string, int[]> keyValuePair in AutoMap.planetDictionary)
			{
				myVector.addElement(new Command(keyValuePair.Key, AutoMap.getInstance(), 6, keyValuePair.Value));
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000A2750 File Offset: 0x000A0950
		private static void ShowMapsMenu(int[] mapIDs)
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < mapIDs.Length; i++)
			{
				if ((global::Char.myCharz().cgender != 0 || (mapIDs[i] != 22 && mapIDs[i] != 23)) && (global::Char.myCharz().cgender != 1 || (mapIDs[i] != 21 && mapIDs[i] != 23)) && (global::Char.myCharz().cgender != 2 || (mapIDs[i] != 21 && mapIDs[i] != 22)))
				{
					myVector.addElement(new Command(AutoMap.GetMapName(mapIDs[i]), AutoMap.getInstance(), 7, mapIDs[i]));
				}
			}
			GameCanvas.menu.startAt(myVector, 3);
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x0000914D File Offset: 0x0000734D
		public static void StartRunToMapId(int mapID)
		{
			AutoMap.isXmaping = true;
			AutoMap.IdMapEnd = mapID;
		}

		// Token: 0x06000AF1 RID: 2801 RVA: 0x0000915B File Offset: 0x0000735B
		public static void FinishXmap()
		{
			AutoMap.isXmaping = false;
			AutoMap.isUsingCapsule = false;
			AutoMap.isOpeningPanel = false;
		}

		// Token: 0x06000AF2 RID: 2802 RVA: 0x000A27F0 File Offset: 0x000A09F0
		public static void UpdateXmap(int mapID)
		{
			if (AutoMap.linkMaps.ContainsKey(84))
			{
				AutoMap.linkMaps.Remove(84);
			}
			AutoMap.linkMaps.Add(84, new List<AutoMap.NextMap>());
			AutoMap.linkMaps[84].Add(new AutoMap.NextMap(24 + global::Char.myCharz().cgender, 10, 0));
			int[] array = AutoMap.FindWay(mapID);
			if (array == null)
			{
				GameScr.info1.addInfo("Không thể tìm thấy đường đi", 0);
				return;
			}
			if (AutoMap.isUseCapsule)
			{
				if (!AutoMap.isUsingCapsule && array.Length > 3)
				{
					for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
					{
						Item item = global::Char.myCharz().arrItemBag[i];
						if (item != null && (item.template.id == 194 || (item.template.id == 193 && item.quantity > 10)))
						{
							AutoMap.isUsingCapsule = true;
							AutoMap.isOpeningPanel = false;
							AutoMap.lastTimeOpenedPanel = mSystem.currentTimeMillis();
							AutoMap.MarkActionSent();
							GameCanvas.panel.mapNames = null;
							Service.gI().useItem(0, 1, -1, item.template.id);
							return;
						}
					}
				}
				if (AutoMap.isUsingCapsule && !AutoMap.isOpeningPanel && (GameCanvas.panel.mapNames == null || mSystem.currentTimeMillis() - AutoMap.lastTimeOpenedPanel < 500L))
				{
					return;
				}
				if (AutoMap.isUsingCapsule && !AutoMap.isOpeningPanel)
				{
					for (int j = array.Length - 1; j >= 2; j--)
					{
						for (int k = 0; k < GameCanvas.panel.mapNames.Length; k++)
						{
							if (GameCanvas.panel.mapNames[k].Contains(TileMap.mapNames[array[j]]))
							{
								AutoMap.isOpeningPanel = true;
								AutoMap.MarkActionSent();
								Service.gI().requestMapSelect(k);
								return;
							}
						}
					}
					AutoMap.isOpeningPanel = true;
				}
			}
			if (TileMap.mapID == array[0] && !global::Char.ischangingMap && !Controller.isStopReadMessage)
			{
				AutoMap.MarkActionSent();
				AutoMap.Goto(array[1]);
			}
		}

		private static void MarkActionSent()
		{
			AutoMap.lastWaitTime = mSystem.currentTimeMillis();
		}

		// Token: 0x06000AF3 RID: 2803 RVA: 0x0000916F File Offset: 0x0000736F
		public static void LoadMapLeft()
		{
			AutoMap.LoadMap(0);
		}

		// Token: 0x06000AF4 RID: 2804 RVA: 0x00009177 File Offset: 0x00007377
		public static void LoadMapCenter()
		{
			AutoMap.LoadMap(2);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x0000917F File Offset: 0x0000737F
		public static void LoadMapRight()
		{
			AutoMap.LoadMap(1);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000A29D8 File Offset: 0x000A0BD8
		public static void LoadData()
		{
			string text = Rms.loadRMSString("AutoMapXmapDelay");
			int delay;
			if (!string.IsNullOrEmpty(text) && int.TryParse(text, out delay))
			{
				AutoMap.xmapDelay = System.Math.Max(0, delay);
			}

			AutoMap.isSaveData = (Rms.loadRMSInt("AutoMapIsSaveRms") == 1);
			if (AutoMap.isSaveData)
			{
				if (Rms.loadRMSInt("AutoMapIsEatChicken") == -1)
				{
					AutoMap.isEatChicken = true;
				}
				else
				{
					AutoMap.isEatChicken = (Rms.loadRMSInt("AutoMapIsEatChicken") == 1);
				}
				if (Rms.loadRMSInt("AutoMapIsUseCsb") == -1)
				{
					AutoMap.isUseCapsule = true;
				}
				else
				{
					AutoMap.isUseCapsule = (Rms.loadRMSInt("AutoMapIsUseCsb") == 1);
				}
				AutoMap.isHarvestPean = (Rms.loadRMSInt("AutoMapIsHarvestPean") == 1);
			}
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000A2A60 File Offset: 0x000A0C60
		private static void SaveData()
		{
			Rms.saveRMSInt("AutoMapIsEatChicken", AutoMap.isEatChicken ? 1 : 0);
			Rms.saveRMSInt("AutoMapIsHarvestPean", AutoMap.isHarvestPean ? 1 : 0);
			Rms.saveRMSInt("AutoMapIsUseCsb", AutoMap.isUseCapsule ? 1 : 0);
			Rms.saveRMSString("AutoMapXmapDelay", AutoMap.xmapDelay.ToString());
		}

		public void onChatFromMe(string text, string to)
		{
			if (ChatTextField.gI().strChat.Equals(AutoMap.inputXmapDelay[0]))
			{
				try
				{
					int delay = int.Parse(ChatTextField.gI().tfChat.getText());
					if (delay < 0)
					{
						throw new FormatException();
					}
					AutoMap.xmapDelay = delay;
					GameScr.info1.addInfo("Delay Qua Map: " + delay.ToString() + "ms", 0);
					Rms.saveRMSString("AutoMapXmapDelay", AutoMap.xmapDelay.ToString());
				}
				catch
				{
					GameScr.info1.addInfo("Delay Không Hợp Lệ, Vui Lòng Nhập Lại!", 0);
				}
			}
			AutoMap.ResetChatTextField();
		}

		public void onCancelChat()
		{
		}

		private static void ResetChatTextField()
		{
			ChatTextField.gI().strChat = "Chat";
			ChatTextField.gI().tfChat.name = "chat";
			ChatTextField.gI().isShow = false;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000A2AAC File Offset: 0x000A0CAC
		private static void LoadLinkMapsXmap()
		{
			AutoMap.AddLinkMapsXmap(new int[]
			{
				0,
				21
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				1,
				47
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				47,
				111
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				2,
				24
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				5,
				29
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				7,
				22
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				9,
				25
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				13,
				33
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				14,
				23
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				16,
				26
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				20,
				37
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				39,
				21
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				40,
				22
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				41,
				23
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				109,
				105
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				109,
				106
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				106,
				107
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				108,
				105
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				80,
				105
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				3,
				27,
				28,
				29,
				30
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				11,
				31,
				32,
				33,
				34
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				17,
				35,
				36,
				37,
				38
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				109,
				108,
				107,
				110,
				106
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				139,
				140
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				149,
				147,
				152,
				151,
				148
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				131,
				132,
				133
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				42,
				0,
				1,
				2,
				3,
				4,
				5,
				6
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				43,
				7,
				8,
				9,
				11,
				12,
				13,
				10
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				52,
				44,
				14,
				15,
				16,
				17,
				18,
				20,
				19
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				53,
				58,
				59,
				60,
				61,
				62,
				55,
				56,
				54,
				57
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				68,
				69,
				70,
				71,
				72,
				64,
				65,
				63,
				66,
				67,
				73,
				74,
				75,
				76,
				77,
				81,
				82,
				83,
				79,
				80
			});
			AutoMap.AddLinkMapsXmap(new int[]
			{
				102,
				92,
				93,
				94,
				96,
				97,
				98,
				99,
				100,
				103
			});
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000A2D4C File Offset: 0x000A0F4C
		private static void LoadNPCLinkMapsXmap()
		{
			AutoMap.AddNPCLinkMapsXmap(19, 68, 12, 1);
			AutoMap.AddNPCLinkMapsXmap(19, 109, 12, 0);
			AutoMap.AddNPCLinkMapsXmap(24, 25, 10, 0);
			AutoMap.AddNPCLinkMapsXmap(24, 26, 10, 1);
			AutoMap.AddNPCLinkMapsXmap(24, 84, 10, 2);
			AutoMap.AddNPCLinkMapsXmap(25, 24, 11, 0);
			AutoMap.AddNPCLinkMapsXmap(25, 26, 11, 1);
			AutoMap.AddNPCLinkMapsXmap(25, 84, 11, 2);
			AutoMap.AddNPCLinkMapsXmap(26, 24, 12, 0);
			AutoMap.AddNPCLinkMapsXmap(26, 25, 12, 1);
			AutoMap.AddNPCLinkMapsXmap(26, 84, 12, 2);
			AutoMap.AddNPCLinkMapsXmap(27, 102, 38, 1);
			AutoMap.AddNPCLinkMapsXmap(27, 53, 25, 0);
			AutoMap.AddNPCLinkMapsXmap(28, 102, 38, 1);
			AutoMap.AddNPCLinkMapsXmap(29, 102, 38, 1);
			AutoMap.AddNPCLinkMapsXmap(45, 48, 19, 3);
			AutoMap.AddNPCLinkMapsXmap(48, 45, 20, 3, 0);
			AutoMap.AddNPCLinkMapsXmap(50, 48, 44, 0);
			AutoMap.AddNPCLinkMapsXmap(48, 50, 20, 3, 1);
			AutoMap.AddNPCLinkMapsXmap(19, 126, 53, 0);
			AutoMap.AddNPCLinkMapsXmap(126, 19, 53, 0);
			AutoMap.AddNPCLinkMapsXmap(24, 139, 63, 0);
			AutoMap.AddNPCLinkMapsXmap(139, 24, 63, 0);
			AutoMap.AddNPCLinkMapsXmap(139, 25, 63, 1);
			AutoMap.AddNPCLinkMapsXmap(139, 26, 63, 2);
			AutoMap.AddNPCLinkMapsXmap(0, 149, 67, 3, 0);
			AutoMap.AddWaypointLinkXmap(53, 27);
			AutoMap.AddWaypointLinkXmap(47, 46);
			AutoMap.AddWaypointLinkXmap(46, 45);
			AutoMap.AddPositionLinkXmap(45, 46, 576, 552);
			AutoMap.AddPositionLinkXmap(46, 47, 576, 552);
			AutoMap.AddNPCLinkMapsXmap(52, 127, 44, 0);
			AutoMap.AddNPCLinkMapsXmap(52, 129, 23, 3);
			AutoMap.AddNPCLinkMapsXmap(52, 113, 23, 2);
			AutoMap.AddNPCLinkMapsXmap(68, 19, 12, 0);
			AutoMap.AddNPCLinkMapsXmap(80, 131, 60, 0);
			AutoMap.AddNPCLinkMapsXmap(102, 24, 38, 1);
			AutoMap.AddNPCLinkMapsXmap(113, 52, 22, 4);
			AutoMap.AddNPCLinkMapsXmap(127, 52, 44, 2);
			AutoMap.AddNPCLinkMapsXmap(129, 52, 23, 3);
			AutoMap.AddNPCLinkMapsXmap(131, 80, 60, 1);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000A2EA0 File Offset: 0x000A10A0
		private static void AddPlanetXmap()
		{
			AutoMap.planetDictionary.Add("Trái đất", AutoMap.idMapsTraiDat);
			AutoMap.planetDictionary.Add("Namếc", AutoMap.idMapsNamek);
			AutoMap.planetDictionary.Add("Xayda", AutoMap.idMapsXayda);
			AutoMap.planetDictionary.Add("Fide", AutoMap.idMapsNappa);
			AutoMap.planetDictionary.Add("Tương lai", AutoMap.idMapsTuongLai);
			AutoMap.planetDictionary.Add("Cold", AutoMap.idMapsCold);
			AutoMap.planetDictionary.Add("Potaufeu", AutoMap.idMapsPotaufeu);
			AutoMap.planetDictionary.Add("Khí Gas", AutoMap.idMapsKhiGas);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000A2F28 File Offset: 0x000A1128
		private static void AddLinkMapsXmap(params int[] link)
		{
			for (int i = 0; i < link.Length; i++)
			{
				if (!AutoMap.linkMaps.ContainsKey(link[i]))
				{
					AutoMap.linkMaps.Add(link[i], new List<AutoMap.NextMap>());
				}
				if (i != 0)
				{
					AutoMap.linkMaps[link[i]].Add(new AutoMap.NextMap(link[i - 1], -1, -1));
				}
				if (i != link.Length - 1)
				{
					AutoMap.linkMaps[link[i]].Add(new AutoMap.NextMap(link[i + 1], -1, -1));
				}
			}
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00009187 File Offset: 0x00007387
		private static void AddNPCLinkMapsXmap(int currentMapID, int nextMapID, int npcID, params int[] selects)
		{
			AutoMap.AddNextMap(currentMapID, new AutoMap.NextMap(nextMapID, npcID, selects));
		}

		private static void AddWaypointLinkXmap(int currentMapID, int nextMapID)
		{
			AutoMap.AddNextMap(currentMapID, new AutoMap.NextMap(nextMapID, -1, -1));
		}

		private static void AddPositionLinkXmap(int currentMapID, int nextMapID, int x, int y)
		{
			AutoMap.AddNextMap(currentMapID, AutoMap.NextMap.Position(nextMapID, x, y));
		}

		private static void AddNextMap(int currentMapID, AutoMap.NextMap nextMap)
		{
			if (!AutoMap.linkMaps.ContainsKey(currentMapID))
			{
				AutoMap.linkMaps.Add(currentMapID, new List<AutoMap.NextMap>());
			}
			AutoMap.linkMaps[currentMapID].Add(nextMap);
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000A2FAC File Offset: 0x000A11AC
		private static void Goto(int mapID)
		{
			foreach (AutoMap.NextMap nextMap in AutoMap.linkMaps[TileMap.mapID])
			{
				if (nextMap.MapID == mapID)
				{
					nextMap.GotoMap();
					return;
				}
			}
			GameScr.info1.addInfo("Không thể thực hiện", 0);
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000091BE File Offset: 0x000073BE
		private static int[] FindWay(int mapID)
		{
			return AutoMap.FindWay(mapID, new int[]
			{
				TileMap.mapID
			});
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000A3024 File Offset: 0x000A1224
		private static int[] FindWay(int mapIDEnd, int[] mapIDs)
		{
			// BFS theo trạng thái (map trước, map hiện tại) để vẫn loại được đường 27/28/29 -> 102 -> 24
			bool avoidCold = global::Char.myCharz().taskMaint.taskId <= 30;
			int start = mapIDs[mapIDs.Length - 1];
			if (avoidCold && start >= 105 && start <= 110)
			{
				avoidCold = false;
			}
			Dictionary<long, long> parent = new Dictionary<long, long>();
			Queue<long> queue = new Queue<long>();
			long startState = AutoMap.PackState(-1, start);
			parent[startState] = long.MinValue;
			queue.Enqueue(startState);
			while (queue.Count > 0)
			{
				long state = queue.Dequeue();
				int prev = (int)(state >> 32);
				int cur = (int)(state & 0xFFFFFFFFL);
				List<AutoMap.NextMap> nexts;
				if (!AutoMap.linkMaps.TryGetValue(cur, out nexts))
				{
					continue;
				}
				foreach (AutoMap.NextMap nextMap in nexts)
				{
					int next = nextMap.MapID;
					if (avoidCold && next >= 105 && next <= 110)
					{
						continue;
					}
					if (cur == 102 && next == 24 && (prev == 27 || prev == 28 || prev == 29))
					{
						continue;
					}
					long nextState = AutoMap.PackState(cur, next);
					if (parent.ContainsKey(nextState))
					{
						continue;
					}
					parent[nextState] = state;
					if (next == mapIDEnd)
					{
						List<int> path = new List<int>();
						for (long s = nextState; s != long.MinValue; s = parent[s])
						{
							path.Add((int)(s & 0xFFFFFFFFL));
						}
						path.Reverse();
						List<int> result = new List<int>(mapIDs);
						result.AddRange(path.GetRange(1, path.Count - 1));
						return result.ToArray();
					}
					queue.Enqueue(nextState);
				}
			}
			return null;
		}

		private static long PackState(int prev, int cur)
		{
			return ((long)prev << 32) | (uint)cur;
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000A31DC File Offset: 0x000A13DC
		private static string GetMapName(int mapID)
		{
			int num = mapID;
			string result;
			if (num != 113)
			{
				if (num != 129)
				{
					result = TileMap.mapNames[mapID] + "\n[" + mapID.ToString() + "]";
				}
				else
				{
					result = TileMap.mapNames[mapID] + " 23\n[" + mapID.ToString() + "]";
				}
			}
			else
			{
				result = string.Concat(new object[]
				{
					"Siêu hạng\n[",
					mapID,
					"]"
				});
			}
			return result;
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000A3260 File Offset: 0x000A1460
		private static string getTextPopup(PopUp popUp)
		{
			if (popUp == null || popUp.says == null)
			{
				return string.Empty;
			}
			System.Text.StringBuilder stringBuilder = new System.Text.StringBuilder();
			for (int i = 0; i < popUp.says.Length; i++)
			{
				stringBuilder.Append(popUp.says[i]);
				stringBuilder.Append(" ");
			}
			return stringBuilder.ToString().Trim();
		}

		private static void setWaypointChangeMap(Waypoint waypoint)
		{
			int mapID = TileMap.mapID;
			string textPopup = AutoMap.getTextPopup(waypoint.popup);
			if (mapID == 27 && textPopup == TileMap.mapNames[53])
			{
				return;
			}
			if ((mapID == 70 && textPopup == TileMap.mapNames[69]) || (mapID == 73 && textPopup == TileMap.mapNames[67]) || (mapID == 110 && textPopup == TileMap.mapNames[106]))
			{
				AutoMap.wayPointMapLeft = waypoint;
				return;
			}
			if (((mapID == 106 || mapID == 107) && textPopup == TileMap.mapNames[110]) || ((mapID == 105 || mapID == 108) && textPopup == TileMap.mapNames[109]) || (mapID == 109 && textPopup == TileMap.mapNames[105]))
			{
				AutoMap.wayPointMapCenter = waypoint;
				return;
			}
			if ((mapID == 70 && textPopup == TileMap.mapNames[71]) || (mapID == 73 && textPopup == TileMap.mapNames[74]) || (mapID == 77 && textPopup == TileMap.mapNames[76]) || (mapID == 105 && textPopup == TileMap.mapNames[106]) || (mapID == 106 && textPopup == TileMap.mapNames[105]) || (mapID == 107 && textPopup == TileMap.mapNames[106]) || (mapID == 108 && textPopup == TileMap.mapNames[105]) || (mapID == 109 && textPopup == TileMap.mapNames[108]) || (mapID == 110 && textPopup == TileMap.mapNames[109]))
			{
				AutoMap.wayPointMapRight = waypoint;
				return;
			}
			if (waypoint.maxX < 60)
			{
				AutoMap.wayPointMapLeft = waypoint;
				return;
			}
			if ((int)waypoint.minX > TileMap.pxw - 60)
			{
				AutoMap.wayPointMapRight = waypoint;
				return;
			}
			AutoMap.wayPointMapCenter = waypoint;
		}

		private static void LoadWaypointsInMap()
		{
			AutoMap.ResetSavedWaypoints();
			if (TileMap.mapID == 46)
			{
				AutoMap.wayPointMapRight = new Waypoint(570, 576, 570, 576, true, false, TileMap.mapNames[47]);
			}
			int num = TileMap.vGo.size();
			if (num == 0 && TileMap.mapID == 45)
			{
				AutoMap.wayPointMapCenter = new Waypoint(570, 576, 570, 576, true, false, TileMap.mapNames[46]);
			}
			for (int i = 0; i < num; i++)
			{
				AutoMap.setWaypointChangeMap((Waypoint)TileMap.vGo.elementAt(i));
			}
		}

		private static int GetWaypointX(Waypoint wp)
		{
			if (wp.maxX < 60)
			{
				return 15;
			}
			if ((int)wp.minX <= TileMap.pxw - 60)
			{
				return (int)(wp.minX + (wp.maxX - wp.minX) / 2);
			}
			return TileMap.pxw - 15;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000A343C File Offset: 0x000A163C
		internal static int GetYGround(int x)
		{
			int num = 50;
			int i = 0;
			while (i < 30)
			{
				i++;
				num += 24;
				if (TileMap.tileTypeAt(x, num, 2))
				{
					if (num % 24 != 0)
					{
						num -= num % 24;
						break;
					}
					break;
				}
			}
			return num;
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000A3478 File Offset: 0x000A1678
		internal static void TeleportTo(int x, int y)
		{
			global::Char me = global::Char.myCharz();
			me.currentMovePoint = null;
			me.cx = x;
			me.cy = y;
			Service.gI().charMove();
			if (ItemTime.isExistItem(4387))
			{
				return;
			}
			me.cy = y + 1;
			Service.gI().charMove();
			me.cy = y;
			Service.gI().charMove();
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x000091D4 File Offset: 0x000073D4
		private static void ResetSavedWaypoints()
		{
			AutoMap.wayPointMapLeft = null;
			AutoMap.wayPointMapCenter = null;
			AutoMap.wayPointMapRight = null;
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000091F7 File Offset: 0x000073F7
		private static bool isNRDMap(int mapID)
		{
			return mapID >= 85 && mapID <= 91;
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000A3510 File Offset: 0x000A1710
		private static bool isFutureMap(int mapID)
		{
			for (int i = 0; i < AutoMap.idMapsTuongLai.Length; i++)
			{
				if (AutoMap.idMapsTuongLai[i] == mapID)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x00009208 File Offset: 0x00007408
		private static bool isNRD(ItemMap mapID)
		{
			return mapID.template.id >= 372 && mapID.template.id <= 378;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000A353C File Offset: 0x000A173C
		private static void LoadMap(int position)
		{
			if (AutoMap.isNRDMap(TileMap.mapID))
			{
				AutoMap.TeleportInNRDMap(position);
				return;
			}
			AutoMap.LoadWaypointsInMap();
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			Waypoint wp = null;
			switch (position)
			{
			case 0:
				wp = AutoMap.wayPointMapLeft;
				if (wp != null)
				{
					AutoMap.TeleportTo(AutoMap.GetWaypointX(wp), (int)wp.maxY);
				}
				else
				{
					AutoMap.TeleportTo(60, AutoMap.GetYGround(60));
				}
				break;
			case 1:
				wp = AutoMap.wayPointMapRight;
				if (wp != null)
				{
					AutoMap.TeleportTo(AutoMap.GetWaypointX(wp), (int)wp.maxY);
				}
				else
				{
					AutoMap.TeleportTo(TileMap.pxw - 60, AutoMap.GetYGround(TileMap.pxw - 60));
				}
				break;
			case 2:
				wp = AutoMap.wayPointMapCenter;
				if (wp != null)
				{
					AutoMap.TeleportTo(AutoMap.GetWaypointX(wp), (int)wp.maxY);
				}
				else
				{
					AutoMap.TeleportTo(TileMap.pxw / 2, AutoMap.GetYGround(TileMap.pxw / 2));
				}
				break;
			}

			if (TileMap.mapID == 7 || TileMap.mapID == 14 || TileMap.mapID == 0 || (wp != null && wp.isOffline))
			{
				Service.gI().getMapOffline();
				return;
			}
			try
			{
				AutoMap.isAutoChangeMap = true;
				Service.gI().requestChangeMap();
			}
			finally
			{
				AutoMap.isAutoChangeMap = false;
			}
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000A3684 File Offset: 0x000A1884
		private static void TeleportInNRDMap(int position)
		{
			if (position == 0)
			{
				AutoMap.TeleportTo(60, AutoMap.GetYGround(60));
				return;
			}
			if (position != 2)
			{
				AutoMap.TeleportTo(TileMap.pxw - 60, AutoMap.GetYGround(TileMap.pxw - 60));
				return;
			}
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.template.npcTemplateId >= 30 && npc.template.npcTemplateId <= 36)
				{
					global::Char.myCharz().npcFocus = npc;
					AutoMap.TeleportTo(npc.cx, npc.cy - 3);
					return;
				}
			}
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000A3728 File Offset: 0x000A1928
		static AutoMap()
		{
			AutoMap.LoadLinkMapsXmap();
			AutoMap.LoadNPCLinkMapsXmap();
			AutoMap.AddPlanetXmap();
			AutoMap.LoadData();
		}

		// Token: 0x04001594 RID: 5524
		public static AutoMap _Instance;

		// Token: 0x04001595 RID: 5525
		private static Dictionary<int, List<AutoMap.NextMap>> linkMaps = new Dictionary<int, List<AutoMap.NextMap>>();

		// Token: 0x04001596 RID: 5526
		private static Dictionary<string, int[]> planetDictionary = new Dictionary<string, int[]>();

		// Token: 0x04001597 RID: 5527
		public static bool isXmaping;

		// Token: 0x04001598 RID: 5528
		public static int IdMapEnd;

		// Token: 0x04001599 RID: 5529
		private static Waypoint wayPointMapLeft;
		private static Waypoint wayPointMapCenter;
		private static Waypoint wayPointMapRight;

		// Token: 0x0400159C RID: 5532
		private static bool isEatChicken = true;

		// Token: 0x0400159D RID: 5533
		private static bool isHarvestPean;

		// Token: 0x0400159E RID: 5534
		private static bool isUseCapsule = true;

		// Token: 0x0400159F RID: 5535
		private static bool isUsingCapsule;

		// Token: 0x040015A0 RID: 5536
		private static bool isOpeningPanel;

		// Token: 0x040015A1 RID: 5537
		private static long lastTimeOpenedPanel;

		// Token: 0x040015A2 RID: 5538
		private static bool isSaveData;

		// Token: 0x040015A3 RID: 5539
		private static long lastWaitTime;

		// Delay (ms) chờ sau khi vào map mới, chỉ dùng khi xmap
		private static int xmapDelay;

		private static int lastMapID = -1;

		private static long timeArrivedMap;

		private static string[] inputXmapDelay = new string[]
		{
			"Nhập Delay Qua Map (ms)",
			"Delay"
		};

		// Token: 0x040015A4 RID: 5540
		private static int[] idMapsNamek = new int[]
		{
			43,
			22,
			7,
			8,
			9,
			11,
			12,
			13,
			10,
			31,
			32,
			33,
			34,
			43,
			25
		};

		// Token: 0x040015A5 RID: 5541
		private static int[] idMapsXayda = new int[]
		{
			44,
			23,
			14,
			15,
			16,
			17,
			18,
			20,
			19,
			35,
			36,
			37,
			38,
			52,
			44,
			26,
			84,
			113,
			127,
			129,
			126
		};

		// Token: 0x040015A6 RID: 5542
		private static int[] idMapsTraiDat = new int[]
		{
			42,
			21,
			0,
			1,
			2,
			3,
			4,
			5,
			6,
			27,
			28,
			29,
			30,
			47,
			42,
			24,
			46,
			45,
			48,
			50,
			111,
			53,
			58,
			59,
			60,
			61,
			62,
			55,
			56,
			54,
			57
		};

		// Token: 0x040015A7 RID: 5543
		private static int[] idMapsTuongLai = new int[]
		{
			102,
			92,
			93,
			94,
			96,
			97,
			98,
			99,
			100,
			103
		};

		// Token: 0x040015A8 RID: 5544
		private static int[] idMapsCold = new int[]
		{
			109,
			108,
			107,
			110,
			106,
			105
		};

		private static int[] idMapsPotaufeu = new int[]
		{
			139,
			140
		};

		private static int[] idMapsKhiGas = new int[]
		{
			149,
			147,
			152,
			151,
			148
		};

		// Token: 0x040015A9 RID: 5545
		private static int[] idMapsNappa = new int[]
		{
			68,
			69,
			70,
			71,
			72,
			64,
			65,
			63,
			66,
			67,
			73,
			74,
			75,
			76,
			77,
			81,
			82,
			83,
			79,
			80,
			131,
			132,
			133
		};

		// Token: 0x040015AA RID: 5546
		public static bool isAutoChangeMap;

		// Token: 0x020000F0 RID: 240
		public class NextMap
		{
			// Token: 0x06000B0E RID: 2830 RVA: 0x00009233 File Offset: 0x00007433
			public NextMap(int mapID, int npcID, params int[] indexes)
			{
				this.MapID = mapID;
				this.Npc = npcID;
				this.Indexes = indexes;
				this.Index = indexes.Length > 0 ? indexes[0] : -1;
			}

			public static AutoMap.NextMap Position(int mapID, int x, int y)
			{
				return new AutoMap.NextMap(mapID, -1, -1)
				{
					IsPosition = true,
					PosX = x,
					PosY = y
				};
			}

			// Token: 0x06000B0F RID: 2831 RVA: 0x000A37F4 File Offset: 0x000A19F4
			public void GotoMap()
			{
				if (this.IsPosition)
				{
					AutoMap.TeleportTo(this.PosX, this.PosY);
					Service.gI().requestChangeMap();
					AutoMap.MarkActionSent();
					return;
				}
				if (this.Index == -1 && this.Npc == -1)
				{
					Waypoint wayPoint = this.GetWayPoint();
					if (wayPoint != null)
					{
						this.Enter(wayPoint);
						return;
					}
				}
				else if (this.Npc != -1 && this.Index != -1)
				{
					AutoMap.MarkActionSent();
					Service.gI().openMenu(this.Npc);
					for (int i = 0; i < this.Indexes.Length; i++)
					{
						Service.gI().confirmMenu((short)this.Npc, (sbyte)this.Indexes[i]);
					}
				}
			}

			// Token: 0x06000B10 RID: 2832 RVA: 0x000A385C File Offset: 0x000A1A5C
			public Waypoint GetWayPoint()
			{
				for (int i = 0; i < TileMap.vGo.size(); i++)
				{
					Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt(i);
					if (this.GetMapName().Equals(this.GetMapName(waypoint.popup)))
					{
						return waypoint;
					}
				}
				return null;
			}

			// Token: 0x06000B11 RID: 2833 RVA: 0x00009250 File Offset: 0x00007450
			public string GetMapName()
			{
				return TileMap.mapNames[this.MapID];
			}

			// Token: 0x06000B12 RID: 2834 RVA: 0x000A38AC File Offset: 0x000A1AAC
			public void Enter(Waypoint waypoint)
			{
				int num = (waypoint.maxX < 60) ? 15 : (((int)waypoint.minX <= TileMap.pxw - 60) ? ((int)((waypoint.minX + waypoint.maxX) / 2)) : (TileMap.pxw - 15));
				int maxY = (int)waypoint.maxY;
				if (num == -1 || maxY == -1)
				{
					GameScr.info1.addInfo("Có lỗi xảy ra", 0);
					return;
				}
				AutoMap.TeleportTo(num, maxY);
				if (waypoint.isOffline || TileMap.mapID == 7 || TileMap.mapID == 14 || TileMap.mapID == 0)
				{
					AutoMap.MarkActionSent();
					Service.gI().getMapOffline();
					return;
				}
				AutoMap.MarkActionSent();
				Service.gI().requestChangeMap();
			}

			// Token: 0x06000B13 RID: 2835 RVA: 0x000A3938 File Offset: 0x000A1B38
			public string GetMapName(PopUp popup)
			{
				if (popup == null || popup.says == null)
				{
					return string.Empty;
				}
				StringBuilder stringBuilder = new StringBuilder();
				for (int i = 0; i < popup.says.Length; i++)
				{
					stringBuilder.Append(popup.says[i]);
					stringBuilder.Append(" ");
				}
				return stringBuilder.ToString().Trim();
			}

			// Token: 0x06000B14 RID: 2836 RVA: 0x000A3984 File Offset: 0x000A1B84
			public void TeleportTo(int x, int y)
			{
				if (GameScr.canAutoPlay)
				{
					global::Char.myCharz().cx = x;
					global::Char.myCharz().cy = y;
					Service.gI().charMove();
					return;
				}
				global::Char.myCharz().cx = x;
				global::Char.myCharz().cy = y;
				Service.gI().charMove();
				global::Char.myCharz().cx = x;
				global::Char.myCharz().cy = y + 1;
				Service.gI().charMove();
				global::Char.myCharz().cx = x;
				global::Char.myCharz().cy = y;
				Service.gI().charMove();
			}

			// Token: 0x040015AB RID: 5547
			public int MapID;

			// Token: 0x040015AC RID: 5548
			public int Npc;

			// Token: 0x040015AD RID: 5549
			public int Index;

			public int[] Indexes;

			public bool IsPosition;

			public int PosX;

			public int PosY;
		}
	}
}
