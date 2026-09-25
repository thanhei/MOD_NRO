using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Mod.CustomPanel;
using Mod.Graphics;
using Mod.ModHelper;
using Mod.ModHelper.CommandMod.Chat;
using Mod.ModHelper.CommandMod.Hotkey;
using Mod.ModHelper.Menu;
using Newtonsoft.Json;
using UnityEngine;

namespace Mod.Set
{
	// Token: 0x02000114 RID: 276
	internal class SetDo : IChatable, IActionListener
	{
		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000DFD RID: 3581 RVA: 0x000A59B8 File Offset: 0x000A3BB8
		[JsonIgnore]
		internal static bool IsCurrentPanelIsSetDoPanel
		{
			get
			{
				if (!GameCanvas.panel.isShow)
				{
					return false;
				}
				string[][] currentTabName = GameCanvas.panel.currentTabName;
				return currentTabName != null && currentTabName.Length != 0 && currentTabName[0].Length >= 2 && currentTabName.Length == Math.Min(4, SetDo.setDos.Count + 1) && (currentTabName[0][0].Contains("Set") || currentTabName[0][1].Contains("Set"));
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000DFE RID: 3582 RVA: 0x000A5A2D File Offset: 0x000A3C2D
		[JsonIgnore]
		internal bool HasAnyItem
		{
			get
			{
				return this.items.Count > 0;
			}
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x000A5A3D File Offset: 0x000A3C3D
		internal SetDo()
		{
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x000A5A50 File Offset: 0x000A3C50
		private SetDo(string name, Item[] items)
		{
			this.Name = name;
			this.items = items.Select<Item, SetDo.ItemSet>((Item i) => new SetDo.ItemSet(i)).ToList<SetDo.ItemSet>();
		}

		// Token: 0x06000E01 RID: 3585 RVA: 0x000A5AA5 File Offset: 0x000A3CA5
		internal static void AddSet(string name, Item[] items = null)
		{
			if (items != null)
			{
				SetDo.setDos.Add(new SetDo(name, items));
			}
		}

		// Token: 0x06000E02 RID: 3586 RVA: 0x000A5ABC File Offset: 0x000A3CBC
		internal void AddOrReplaceItem(Item item)
		{
			if (item == null)
			{
				return;
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				if ((int)item.template.type == this.items[i].type)
				{
					this.items[i] = new SetDo.ItemSet(item);
					return;
				}
			}
			this.items.Add(new SetDo.ItemSet(item));
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x000A5B28 File Offset: 0x000A3D28
		internal void RemoveItem(Item item)
		{
			if (item == null)
			{
				return;
			}
			for (int i = 0; i < this.items.Count; i++)
			{
				if ((int)item.template.type == this.items[i].type)
				{
					this.items.RemoveAt(i);
					return;
				}
			}
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x000A5B7C File Offset: 0x000A3D7C
		internal bool HasItem(Item item)
		{
			return item != null && this.items.Any<SetDo.ItemSet>((SetDo.ItemSet i) => i.fullName == item.GetFullInfo());
		}

		// Token: 0x06000E05 RID: 3589 RVA: 0x000A5BB7 File Offset: 0x000A3DB7
		internal bool CanWearForMe()
		{
			return !this.items.Any<SetDo.ItemSet>((SetDo.ItemSet i) => i.gender != -1 && i.gender != global::Char.myCharz().cgender);
		}

		// Token: 0x06000E06 RID: 3590 RVA: 0x000A5BE8 File Offset: 0x000A3DE8
		internal bool CanWearForPet()
		{
			return !this.items.Where<SetDo.ItemSet>((SetDo.ItemSet i) => i.type < 6).Any<SetDo.ItemSet>((SetDo.ItemSet i) => i.gender != -1 && i.gender != Utils.GetPetGender());
		}

		// Token: 0x06000E07 RID: 3591 RVA: 0x000A5C46 File Offset: 0x000A3E46
		internal void Wear()
		{
			if (!this.CanWearForMe())
			{
				GameScr.info1.addInfo("Set có chứa đồ khác hệ!", 0);
				return;
			}
			new Thread(delegate
			{
				DateTime now;
				do
				{
					now = DateTime.Now;
					using (List<SetDo.ItemSet>.Enumerator enumerator = this.items.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (SetDo.GetItem(enumerator.Current, 4))
							{
								Thread.Sleep(100);
							}
						}
					}
				}
				while (DateTime.Now.Subtract(now).TotalMilliseconds >= 50.0);
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000E08 RID: 3592 RVA: 0x000A5C7E File Offset: 0x000A3E7E
		internal void WearForPet()
		{
			if (!this.CanWearForPet())
			{
				GameScr.info1.addInfo("Set có chứa đồ khác hệ của đệ tử!", 0);
				return;
			}
			new Thread(delegate
			{
				DateTime now;
				do
				{
					now = DateTime.Now;
					using (IEnumerator<SetDo.ItemSet> enumerator = this.items.Where<SetDo.ItemSet>((SetDo.ItemSet i) => i.type < 6).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							if (SetDo.GetItem(enumerator.Current, 4))
							{
								Thread.Sleep(100);
							}
						}
					}
				}
				while (DateTime.Now.Subtract(now).TotalMilliseconds >= 50.0);
			})
			{
				IsBackground = true
			}.Start();
		}

		// Token: 0x06000E09 RID: 3593 RVA: 0x000A5CB8 File Offset: 0x000A3EB8
		internal static bool GetItem(SetDo.ItemSet itemSet, sbyte type = 4)
		{
			if (string.IsNullOrEmpty(itemSet.fullName))
			{
				return false;
			}
			global::Char @char;
			if (type == 6)
			{
				@char = global::Char.myCharz();
			}
			else
			{
				if (type != 4)
				{
					throw new ArgumentOutOfRangeException("type");
				}
				@char = global::Char.myPetz();
			}
			for (int k = 0; k < @char.arrItemBody.Length; k++)
			{
				if (@char.arrItemBody[k] != null && itemSet.fullName == @char.arrItemBody[k].GetFullInfo())
				{
					return false;
				}
			}
			Item[] array = global::Char.myCharz().arrItemBag.Where<Item>((Item i) => i != null && i.isTypeBody()).ToArray<Item>();
			for (int j = 0; j < array.Length; j++)
			{
				Item item = array[j];
				if (itemSet.fullName == item.GetFullInfo())
				{
					MainThreadDispatcher.Dispatch(delegate
					{
						Service.gI().getItem(type, (sbyte)item.indexUI);
					});
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E0A RID: 3594 RVA: 0x000A5DD8 File Offset: 0x000A3FD8
		internal static string GetSetItems(SetDo set)
		{
			return string.Join(", ", set.items.Select<SetDo.ItemSet, string>((SetDo.ItemSet i) => i.name)).TrimEnd(' ').TrimEnd(',');
		}

		// Token: 0x06000E0B RID: 3595 RVA: 0x000A5E28 File Offset: 0x000A4028
		[ChatCommand("set")]
		[HotkeyCommand('`')]
		internal static void ShowMenu()
		{
			string text = string.Format("Bạn đã lưu {0} set đồ.", SetDo.setDos.Count);
			for (int i = 0; i < Math.Min(5, SetDo.setDos.Count); i++)
			{
				text += string.Format("\nSet {0}: {1}", string.IsNullOrEmpty(SetDo.setDos[i].Name) ? (i + 1) : SetDo.setDos[i].Name, SetDo.GetSetItems(SetDo.setDos[i]));
			}
			if (SetDo.setDos.Count > 5)
			{
				text += "\n...";
			}
			new MenuBuilder().setChatPopup(text).map<int>(Enumerable.Range(0, Math.Min(5, SetDo.setDos.Count)), (int index) => new MenuItem(string.Format("Mặc set\n{0}", string.IsNullOrEmpty(SetDo.setDos[index].Name) ? (index + 1) : SetDo.setDos[index].Name), new MenuAction(delegate
			{
				SetDo.setDos[index].Wear();
			}))).addItem(global::Char.myCharz().havePet && SetDo.setDos.Count > 0, "Mặc cho\nđệ tử", new MenuAction(new Action(SetDo.ShowMenuPetSet)))
				.addItem("Mở danh sách set đồ", new MenuAction(delegate
				{
					string[][] array = new string[Math.Min(4, SetDo.setDos.Count + 1)][];
					for (int j = SetDo.offset; j < array.Length + SetDo.offset; j++)
					{
						if (j == SetDo.setDos.Count)
						{
							array[j - SetDo.offset] = new string[] { "Thêm", "Set mới" };
						}
						else
						{
							array[j - SetDo.offset] = new string[]
							{
								"Set đồ",
								string.IsNullOrEmpty(SetDo.setDos[j].Name) ? (j + 1).ToString() : SetDo.setDos[j].Name
							};
						}
					}
					GameCanvas.panel.tabName[CustomPanelMenu.TYPE_CUSTOM_PANEL_MENU] = array;
					CustomPanelMenu.Show(new CustomPanelMenuConfig
					{
						SetTabAction = new Action<Panel>(SetDo.SetTabSetPanel),
						DoFireItemAction = new Action<Panel>(SetDo.DoFireSetPanel),
						PaintAction = new Action<Panel, mGraphics>(SetDo.PaintSetPanel)
					}, null);
				}))
				.addItem(SetDo.setDos.Count > 0, "Xoá hết\nset đồ\nđã lưu", new MenuAction(delegate
				{
					GameCanvas.startYesNoDlg("Bạn có chắc chắn muốn xoá hết set đồ đã lưu không?", new Command(mResources.YES, new SetDo(), 2, null), new Command(mResources.NO, new SetDo(), 100, null));
				}))
				.start();
		}

		// Token: 0x06000E0C RID: 3596 RVA: 0x000A5FC0 File Offset: 0x000A41C0
		private static void ShowMenuPetSet()
		{
			string text = string.Format("Bạn đã lưu {0} set đồ.", SetDo.setDos.Count);
			for (int i = 0; i < Math.Min(5, SetDo.setDos.Count); i++)
			{
				text += string.Format("\nSet {0}: {1}", string.IsNullOrEmpty(SetDo.setDos[i].Name) ? (i + 1) : SetDo.setDos[i].Name, SetDo.GetSetItems(SetDo.setDos[i]));
			}
			if (SetDo.setDos.Count > 5)
			{
				text += "\n...";
			}
			new MenuBuilder().setChatPopup(text).map<int>(Enumerable.Range(0, Math.Min(5, SetDo.setDos.Count)), (int index) => new MenuItem(string.Format("Mặc cho\nđệ set\n{0}", string.IsNullOrEmpty(SetDo.setDos[index].Name) ? (index + 1) : SetDo.setDos[index].Name), new MenuAction(delegate
			{
				SetDo.setDos[index].WearForPet();
			}))).start();
		}

		// Token: 0x06000E0D RID: 3597 RVA: 0x000A60B7 File Offset: 0x000A42B7
		internal static void SetTabSetPanel(Panel panel)
		{
			SetTabPanelTemplates.setTabListTemplate(panel, new int[] { SetDo.getItemCount() });
		}

		// Token: 0x06000E0E RID: 3598 RVA: 0x000A60D0 File Offset: 0x000A42D0
		internal static void PaintSetPanel(Panel panel, mGraphics g)
		{
			g.setColor(16711680);
			if (SetDo.offset > 0)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 5, 1, 61, 0);
			}
			if (SetDo.offset < SetDo.setDos.Count - 3)
			{
				g.drawRegion(Mob.imgHP, 0, 0, 9, 6, 4, GameCanvas.panel.wScroll - 7, 61, 0);
			}
			g.setClip(GameCanvas.panel.xScroll, GameCanvas.panel.yScroll, GameCanvas.panel.wScroll, GameCanvas.panel.hScroll);
			g.translate(0, -GameCanvas.panel.cmy);
			for (int i = 0; i < SetDo.arrItemBody.Length + SetDo.arrItemBag.Length + SetDo.arrItemBodyPet.Length; i++)
			{
				bool flag = i < SetDo.arrItemBody.Length;
				bool flag2 = i >= SetDo.arrItemBody.Length && i < SetDo.arrItemBody.Length + SetDo.arrItemBag.Length;
				bool flag3 = !flag && !flag2;
				int num = GameCanvas.panel.xScroll + 36;
				int num2 = GameCanvas.panel.yScroll + i * GameCanvas.panel.ITEM_HEIGHT;
				int num3 = GameCanvas.panel.wScroll - 36;
				int num4 = GameCanvas.panel.ITEM_HEIGHT - 1;
				int xScroll = GameCanvas.panel.xScroll;
				int num5 = GameCanvas.panel.yScroll + i * GameCanvas.panel.ITEM_HEIGHT;
				int num6 = 34;
				int num7 = GameCanvas.panel.ITEM_HEIGHT - 1;
				if (num2 - GameCanvas.panel.cmy <= GameCanvas.panel.yScroll + GameCanvas.panel.hScroll && num2 - GameCanvas.panel.cmy >= GameCanvas.panel.yScroll - GameCanvas.panel.ITEM_HEIGHT)
				{
					Item item = null;
					if (flag)
					{
						item = SetDo.arrItemBody[i];
					}
					else if (flag2)
					{
						item = SetDo.arrItemBag[i - SetDo.arrItemBody.Length];
					}
					else if (flag3)
					{
						item = SetDo.arrItemBodyPet[i - SetDo.arrItemBody.Length - SetDo.arrItemBag.Length];
					}
					if (flag)
					{
						g.setColor((i != GameCanvas.panel.selected) ? 15196114 : 16383818);
					}
					else if (flag3)
					{
						if (i == GameCanvas.panel.selected)
						{
							g.setColor(16383818);
						}
						else
						{
							g.setColor(new Color(0.9f, 0.87f, 0.72f));
						}
					}
					else
					{
						g.setColor((i != GameCanvas.panel.selected) ? 15723751 : 16383818);
					}
					g.fillRect(num, num2, num3, num4);
					if (i == GameCanvas.panel.selected)
					{
						g.setColor(9541120);
					}
					else if (flag || flag3)
					{
						g.setColor(9993045);
					}
					else
					{
						g.setColor(11837316);
					}
					if (item.isHaveOption(34))
					{
						g.setColor((i != GameCanvas.panel.selected) ? Panel.color1[0] : Panel.color2[0]);
					}
					else if (item.isHaveOption(35))
					{
						g.setColor((i != GameCanvas.panel.selected) ? Panel.color1[1] : Panel.color2[1]);
					}
					else if (item.isHaveOption(36))
					{
						g.setColor((i != GameCanvas.panel.selected) ? Panel.color1[2] : Panel.color2[2]);
					}
					g.fillRect(xScroll, num5, num6, num7);
					if (item.isSelect)
					{
						g.setColor((i != GameCanvas.panel.selected) ? 6047789 : 7040779);
						g.fillRect(xScroll, num5, num6, num7);
					}
					if (GameCanvas.panel.currentTabIndex + SetDo.offset < SetDo.setDos.Count && SetDo.setDos[GameCanvas.panel.currentTabIndex + SetDo.offset].HasItem(item))
					{
						g.setColor(Color.red);
						g.drawRect(xScroll, num5, num3 + num6 + 1, num4);
					}
					string text = string.Empty;
					if (item.itemOption != null)
					{
						for (int j = 0; j < item.itemOption.Length; j++)
						{
							if (item.itemOption[j].optionTemplate.id == 72)
							{
								text = " [+" + item.itemOption[j].param.ToString() + "]";
							}
						}
					}
					CustomGraphics.PaintItemEffectInPanel(g, xScroll + 18, num5 + 12, num6, num7, item);
					mFont.tahoma_7_green2.drawString(g, item.template.name + text, num + 5, num2 + 1, 0);
					string text2 = string.Empty;
					if (item.itemOption != null)
					{
						if (item.itemOption.Length != 0 && item.itemOption[0] != null && item.itemOption[0].optionTemplate.id != 102 && item.itemOption[0].optionTemplate.id != 107)
						{
							text2 += item.itemOption[0].getOptionString();
						}
						mFont mFont = mFont.tahoma_7_blue;
						if (item.compare < 0 && item.template.type != 5)
						{
							mFont = mFont.tahoma_7_red;
						}
						if (item.itemOption.Length > 1)
						{
							for (int k = 1; k < 2; k++)
							{
								if (item.itemOption[k] != null && item.itemOption[k].optionTemplate.id != 102 && item.itemOption[k].optionTemplate.id != 107)
								{
									text2 = text2 + "," + item.itemOption[k].getOptionString();
								}
							}
						}
						mFont.drawString(g, text2, num + 5, num2 + 11, mFont.LEFT);
					}
					SmallImage.drawSmallImage(g, (int)item.template.iconID, xScroll + num6 / 2, num5 + num7 / 2, 0, 3);
					if (item.quantity > 1)
					{
						mFont.tahoma_7_yellow.drawString(g, "x" + item.quantity.ToString(), xScroll + num6, num5 + num7 - mFont.tahoma_7_yellow.getHeight(), 1);
					}
					CustomGraphics.PaintItemOptions(g, GameCanvas.panel, item, num2);
				}
			}
			GameCanvas.panel.paintScrollArrow(g);
		}

		// Token: 0x06000E0F RID: 3599 RVA: 0x000A6714 File Offset: 0x000A4914
		internal static void DoFireSetPanel(Panel panel)
		{
			if (!SetDo.IsCurrentPanelIsSetDoPanel)
			{
				return;
			}
			int selected = GameCanvas.panel.selected;
			if (selected < 0)
			{
				return;
			}
			GameCanvas.panel.currItem = null;
			SetDo.arrItemBody = global::Char.myCharz().arrItemBody.Where<Item>((Item i) => i != null && i.isTypeBody()).ToArray<Item>();
			SetDo.arrItemBag = global::Char.myCharz().arrItemBag.Where<Item>((Item i) => i != null && i.isTypeBody()).ToArray<Item>();
			SetDo.arrItemBodyPet = global::Char.myPetz().arrItemBody.Where<Item>((Item i) => i != null && i.isTypeBody()).ToArray<Item>();
			if (selected >= SetDo.arrItemBody.Length + SetDo.arrItemBag.Length)
			{
				GameCanvas.panel.currItem = SetDo.arrItemBodyPet[selected - SetDo.arrItemBody.Length - SetDo.arrItemBag.Length];
			}
			else if (selected >= SetDo.arrItemBody.Length)
			{
				GameCanvas.panel.currItem = SetDo.arrItemBag[selected - SetDo.arrItemBody.Length];
			}
			else
			{
				GameCanvas.panel.currItem = SetDo.arrItemBody[selected];
			}
			string text = "Thêm vào\nset ";
			if (GameCanvas.panel.currentTabIndex + SetDo.offset == SetDo.setDos.Count)
			{
				text += "mới";
			}
			else
			{
				if (SetDo.setDos[GameCanvas.panel.currentTabIndex + SetDo.offset].HasItem(GameCanvas.panel.currItem))
				{
					text = "Xóa khỏi\nset ";
				}
				string text2 = text;
				object obj = (string.IsNullOrEmpty(SetDo.setDos[GameCanvas.panel.currentTabIndex + SetDo.offset].Name) ? (GameCanvas.panel.currentTabIndex + SetDo.offset + 1) : ("\n" + SetDo.setDos[GameCanvas.panel.currentTabIndex + SetDo.offset].Name));
				text = text2 + ((obj != null) ? obj.ToString() : null);
			}
			new MenuBuilder().addItem(text, new MenuAction(delegate
			{
				if (GameCanvas.panel.currentTabIndex + SetDo.offset == SetDo.setDos.Count)
				{
					SetDo.AddSet("", null);
					SetDo.RefreshPanelTabName();
					GameCanvas.panel.EmulateSetTypePanel(0);
				}
				if (SetDo.setDos[GameCanvas.panel.currentTabIndex + SetDo.offset].HasItem(GameCanvas.panel.currItem))
				{
					SetDo.setDos[GameCanvas.panel.currentTabIndex + SetDo.offset].RemoveItem(GameCanvas.panel.currItem);
					if (!SetDo.setDos[GameCanvas.panel.currentTabIndex + SetDo.offset].HasAnyItem)
					{
						string text3 = (string.IsNullOrEmpty(SetDo.setDos[GameCanvas.panel.currentTabIndex + SetDo.offset].Name) ? (GameCanvas.panel.currentTabIndex + SetDo.offset + 1).ToString() : ("\"" + SetDo.setDos[GameCanvas.panel.currentTabIndex + SetDo.offset].Name + "\""));
						SetDo.setDos.RemoveAt(GameCanvas.panel.currentTabIndex + SetDo.offset);
						if (SetDo.offset >= SetDo.setDos.Count - 3 && SetDo.offset > 0)
						{
							SetDo.offset--;
						}
						if (GameCanvas.panel.currentTabIndex >= Math.Min(4, SetDo.setDos.Count + 1) - 1 && GameCanvas.panel.currentTabIndex > 0)
						{
							GameCanvas.panel.currentTabIndex--;
						}
						SetDo.RefreshPanelTabName();
						GameCanvas.panel.EmulateSetTypePanel(0);
						GameScr.info1.addInfo(string.Concat(new string[] { "Đã xóa set ", text3, " do set ", text3, " không chứa đồ nào!" }), 0);
					}
				}
				else
				{
					SetDo.setDos[GameCanvas.panel.currentTabIndex + SetDo.offset].AddOrReplaceItem(GameCanvas.panel.currItem);
				}
				SetDo.SaveData();
			})).setPos(GameCanvas.panel.X, (selected + 1) * GameCanvas.panel.ITEM_HEIGHT - GameCanvas.panel.cmy + GameCanvas.panel.yScroll).start();
			if (GameCanvas.panel.currItem != null)
			{
				global::Char.myCharz().setPartTemp(GameCanvas.panel.currItem.headTemp, GameCanvas.panel.currItem.bodyTemp, GameCanvas.panel.currItem.legTemp, GameCanvas.panel.currItem.bagTemp);
				GameCanvas.panel.addItemDetail(GameCanvas.panel.currItem);
				return;
			}
			GameCanvas.panel.cp = null;
		}

		// Token: 0x06000E10 RID: 3600 RVA: 0x000A6A10 File Offset: 0x000A4C10
		internal static void Update()
		{
			if (!SetDo.IsCurrentPanelIsSetDoPanel)
			{
				return;
			}
			if (GameCanvas.isMouseFocus(GameCanvas.panel.X, GameCanvas.panel.Y + 50, GameCanvas.panel.W, 28))
			{
				if (GameCanvas.pXYScrollMouse > 0)
				{
					if (GameCanvas.panel.currentTabIndex < Math.Min(4, SetDo.setDos.Count + 1) - 1)
					{
						GameCanvas.panel.currentTabIndex++;
					}
					else if (SetDo.offset < SetDo.setDos.Count - 3)
					{
						SetDo.offset++;
						SetDo.RefreshPanelTabName();
						GameCanvas.panel.EmulateSetTypePanel(0);
					}
				}
				if (GameCanvas.pXYScrollMouse < 0)
				{
					if (GameCanvas.panel.currentTabIndex > 0)
					{
						GameCanvas.panel.currentTabIndex--;
					}
					else if (SetDo.offset > 0)
					{
						SetDo.offset--;
						SetDo.RefreshPanelTabName();
						GameCanvas.panel.EmulateSetTypePanel(0);
					}
				}
			}
			if (SetDo.isShowMenu && mSystem.currentTimeMillis() - SetDo.lastTimeSetDisableCloseMenu > 500L && GameCanvas.menu.showMenu)
			{
				SetDo.isShowMenu = false;
				GameCanvas.menu.disableClose = false;
				GameCanvas.menu.isClose = false;
			}
		}

		// Token: 0x06000E11 RID: 3601 RVA: 0x000A6B4C File Offset: 0x000A4D4C
		internal static void UpdateKey()
		{
			if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
			{
				int i = 0;
				while (i < Math.Min(9, SetDo.setDos.Count))
				{
					int num = GameCanvas.keyAsciiPress;
					switch (num)
					{
					case 33:
						num = 49;
						break;
					case 34:
					case 39:
					case 41:
						break;
					case 35:
						num = 51;
						break;
					case 36:
						num = 52;
						break;
					case 37:
						num = 53;
						break;
					case 38:
						num = 55;
						break;
					case 40:
						num = 57;
						break;
					case 42:
						num = 56;
						break;
					default:
						if (num != 64)
						{
							if (num == 94)
							{
								num = 54;
							}
						}
						else
						{
							num = 50;
						}
						break;
					}
					if (num == i + 49)
					{
						GameCanvas.keyAsciiPress = 0;
						GameCanvas.clearKeyHold();
						GameCanvas.clearKeyPressed();
						if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
						{
							if (SetDo.setDos[i].CanWearForPet())
							{
								GameScr.info1.addInfo(string.Format("Mặc set {0} cho đệ tử!", string.IsNullOrEmpty(SetDo.setDos[i].Name) ? (i + 1) : ("\"" + SetDo.setDos[i].Name + "\"")), 0);
							}
							SetDo.setDos[i].WearForPet();
							return;
						}
						if (SetDo.setDos[i].CanWearForMe())
						{
							GameScr.info1.addInfo(string.Format("Mặc set {0} cho bản thân!", string.IsNullOrEmpty(SetDo.setDos[i].Name) ? (i + 1) : ("\"" + SetDo.setDos[i].Name + "\"")), 0);
						}
						SetDo.setDos[i].Wear();
						return;
					}
					else
					{
						i++;
					}
				}
			}
		}

		// Token: 0x06000E12 RID: 3602 RVA: 0x000A6D28 File Offset: 0x000A4F28
		internal static void UpdateScrollMouse(Panel panel, ref int pXYScrollMouse)
		{
			if (panel != GameCanvas.panel)
			{
				return;
			}
			if (!SetDo.IsCurrentPanelIsSetDoPanel)
			{
				return;
			}
			if (GameCanvas.isMouseFocus(GameCanvas.panel.X, GameCanvas.panel.Y + 50, GameCanvas.panel.W, 28) && pXYScrollMouse != 0)
			{
				pXYScrollMouse = 0;
			}
		}

		// Token: 0x06000E13 RID: 3603 RVA: 0x000A6D78 File Offset: 0x000A4F78
		internal static void UpdateTouch(Panel panel)
		{
			if (panel != GameCanvas.panel)
			{
				return;
			}
			if (!SetDo.IsCurrentPanelIsSetDoPanel)
			{
				return;
			}
			if (SetDo.offset < SetDo.setDos.Count - 3 && GameCanvas.isPointerHoldIn(GameCanvas.panel.wScroll - 7, 61, 6, 9))
			{
				GameCanvas.isPointerJustDown = false;
				GameScr.gI().isPointerDowning = false;
				if (GameCanvas.isPointerClick)
				{
					if (GameCanvas.panel.currentTabIndex < Math.Min(4, SetDo.setDos.Count + 1) - 1)
					{
						GameCanvas.panel.currentTabIndex++;
					}
					else
					{
						SetDo.offset++;
						SetDo.RefreshPanelTabName();
						GameCanvas.panel.EmulateSetTypePanel(0);
					}
				}
				GameCanvas.clearAllPointerEvent();
			}
			else if (SetDo.offset > 0 && GameCanvas.isPointerHoldIn(2, 61, 6, 9))
			{
				GameCanvas.isPointerJustDown = false;
				GameScr.gI().isPointerDowning = false;
				if (GameCanvas.isPointerClick)
				{
					if (GameCanvas.panel.currentTabIndex > 0)
					{
						GameCanvas.panel.currentTabIndex--;
					}
					else
					{
						SetDo.offset--;
						SetDo.RefreshPanelTabName();
						GameCanvas.panel.EmulateSetTypePanel(0);
					}
				}
				GameCanvas.clearAllPointerEvent();
			}
			for (int i = 0; i < GameCanvas.panel.currentTabName.Length; i++)
			{
				if (i + SetDo.offset < SetDo.setDos.Count && GameCanvas.isPointer(GameCanvas.panel.startTabPos + i * GameCanvas.panel.TAB_W, 52, GameCanvas.panel.TAB_W - 1, 25))
				{
					GameCanvas.isPointerJustDown = false;
					GameScr.gI().isPointerDowning = false;
					if (GameCanvas.isPointerClick)
					{
						int index = i;
						new MenuBuilder().addItem(GameCanvas.panel.currentTabIndex != index, "Xem set", new MenuAction(delegate
						{
							GameCanvas.panel.currentTabIndex = index;
						})).addItem("Đổi tên set", new MenuAction(delegate
						{
							SetDo.indexSetToRename = index + SetDo.offset;
							GameCanvas.panel.chatTField = new ChatTextField();
							GameCanvas.panel.chatTField.tfChat.y = GameCanvas.h - 35 - ChatTextField.gI().tfChat.height;
							GameCanvas.panel.chatTField.initChatTextField();
							GameCanvas.panel.chatTField.strChat = string.Empty;
							GameCanvas.panel.chatTField.tfChat.name = "Tên set";
							GameCanvas.panel.chatTField.tfChat.setIputType(TField.INPUT_TYPE_ANY);
							GameCanvas.panel.chatTField.startChat2(new SetDo(), "Nhập tên set mới");
						})).addItem("Xóa set", new MenuAction(delegate
						{
							GameCanvas.startYesNoDlg(string.Format("Bạn có chắc chắn muốn xoá set {0} không?", string.IsNullOrEmpty(SetDo.setDos[index + SetDo.offset].Name) ? (index + SetDo.offset + 1) : ("\"" + SetDo.setDos[index + SetDo.offset].Name + "\"")), new Command(mResources.YES, new SetDo(), 1, index + SetDo.offset), new Command(mResources.NO, new SetDo(), 100, null));
						}))
							.start();
						string text = "Set đồ ";
						object obj = (string.IsNullOrEmpty(SetDo.setDos[i + SetDo.offset].Name) ? (i + SetDo.offset + 1) : SetDo.setDos[i + SetDo.offset].Name);
						string text2 = text + ((obj != null) ? obj.ToString() : null);
						GameCanvas.panel.popUpDetailInit(GameCanvas.panel.cp = new ChatPopup(), text2);
						GameCanvas.panel.idIcon = -1;
						GameCanvas.panel.partID = null;
						GameCanvas.panel.charInfo = null;
						GameCanvas.panel.currItem = null;
						GameCanvas.panel.cp.cy = 30;
						GameCanvas.menu.disableClose = true;
						SetDo.isShowMenu = true;
						SetDo.lastTimeSetDisableCloseMenu = mSystem.currentTimeMillis();
					}
					GameCanvas.clearAllPointerEvent();
				}
			}
		}

		// Token: 0x06000E14 RID: 3604 RVA: 0x000A7074 File Offset: 0x000A5274
		private static void RefreshPanelTabName()
		{
			string[][] array = new string[Math.Min(4, SetDo.setDos.Count + 1)][];
			for (int i = SetDo.offset; i < array.Length + SetDo.offset; i++)
			{
				if (i == SetDo.setDos.Count)
				{
					array[i - SetDo.offset] = new string[] { "Thêm", "Set mới" };
				}
				else
				{
					array[i - SetDo.offset] = new string[]
					{
						"Set đồ",
						string.IsNullOrEmpty(SetDo.setDos[i].Name) ? (i + 1).ToString() : SetDo.setDos[i].Name
					};
				}
			}
			GameCanvas.panel.tabName[CustomPanelMenu.TYPE_CUSTOM_PANEL_MENU] = array;
			GameCanvas.panel.currentTabName = array;
		}

		// Token: 0x06000E15 RID: 3605 RVA: 0x000A7150 File Offset: 0x000A5350
		internal static void LoadData()
		{
			try
			{
				SetDo.setDos = JsonConvert.DeserializeObject<List<SetDo>>(Utils.LoadDataString(string.Format("setdo_{0}_{1}_{2}", Utils.username, Utils.server["ip"], Utils.server["port"]), true));
			}
			catch
			{
			}
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x000A71B0 File Offset: 0x000A53B0
		internal static void SaveData()
		{
			try
			{
				Utils.SaveData(string.Format("setdo_{0}_{1}_{2}", Utils.username, Utils.server["ip"], Utils.server["port"]), JsonConvert.SerializeObject(SetDo.setDos), true);
			}
			catch
			{
			}
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x000A7210 File Offset: 0x000A5410
		public void onChatFromMe(string text, string to)
		{
			if (string.IsNullOrEmpty(text))
			{
				GameCanvas.panel.chatTField.isShow = false;
			}
			else if (to == "Nhập tên set mới")
			{
				string text2 = (string.IsNullOrEmpty(SetDo.setDos[SetDo.indexSetToRename].Name) ? (SetDo.indexSetToRename + 1).ToString() : ("\"" + SetDo.setDos[SetDo.indexSetToRename].Name + "\""));
				SetDo.setDos[SetDo.indexSetToRename].Name = text;
				SetDo.RefreshPanelTabName();
				GameCanvas.panel.EmulateSetTypePanel(0);
				GameScr.info1.addInfo(string.Concat(new string[] { "Đã đổi tên set ", text2, " thành \"", text, "\"!" }), 0);
				SetDo.SaveData();
			}
			GameCanvas.panel.chatTField.ResetTF();
		}

		// Token: 0x06000E18 RID: 3608 RVA: 0x000A730C File Offset: 0x000A550C
		private static int getItemCount()
		{
			SetDo.arrItemBody = global::Char.myCharz().arrItemBody.Where<Item>((Item i) => i != null && i.isTypeBody()).ToArray<Item>();
			SetDo.arrItemBag = global::Char.myCharz().arrItemBag.Where<Item>((Item i) => i != null && i.isTypeBody()).ToArray<Item>();
			SetDo.arrItemBodyPet = global::Char.myPetz().arrItemBody.Where<Item>((Item i) => i != null && i.isTypeBody()).ToArray<Item>();
			return SetDo.arrItemBody.Length + SetDo.arrItemBag.Length + SetDo.arrItemBodyPet.Length;
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x000A73D8 File Offset: 0x000A55D8
		public void onCancelChat()
		{
			GameCanvas.panel.chatTField.ResetTF();
		}

		// Token: 0x06000E1A RID: 3610 RVA: 0x000A73EC File Offset: 0x000A55EC
		public void perform(int idAction, object p)
		{
			if (idAction == 1)
			{
				string text = (string.IsNullOrEmpty(SetDo.setDos[(int)p].Name) ? ((int)p + 1).ToString() : ("\"" + SetDo.setDos[(int)p].Name + "\""));
				SetDo.setDos.RemoveAt((int)p);
				if (SetDo.offset >= SetDo.setDos.Count - 3 && SetDo.offset > 0)
				{
					SetDo.offset--;
				}
				if (GameCanvas.panel.currentTabIndex >= Math.Min(4, SetDo.setDos.Count + 1) - 1 && GameCanvas.panel.currentTabIndex > 0)
				{
					GameCanvas.panel.currentTabIndex--;
				}
				SetDo.RefreshPanelTabName();
				GameCanvas.panel.EmulateSetTypePanel(0);
				GameScr.info1.addInfo("Đã xoá set " + text + "!", 0);
				SetDo.SaveData();
			}
			else if (idAction == 2)
			{
				SetDo.setDos.Clear();
				SetDo.RefreshPanelTabName();
				GameCanvas.panel.EmulateSetTypePanel(0);
				GameScr.info1.addInfo("Đã xoá hết set đồ!", 0);
				SetDo.SaveData();
			}
			InfoDlg.hide();
			GameCanvas.currentDialog = null;
		}

		// Token: 0x04001548 RID: 5448
		internal List<SetDo.ItemSet> items = new List<SetDo.ItemSet>();

		// Token: 0x04001549 RID: 5449
		internal string Name;

		// Token: 0x0400154A RID: 5450
		[JsonIgnore]
		internal static int offset;

		// Token: 0x0400154B RID: 5451
		[JsonIgnore]
		internal static List<SetDo> setDos = new List<SetDo>();

		// Token: 0x0400154C RID: 5452
		[JsonIgnore]
		private static bool isShowMenu;

		// Token: 0x0400154D RID: 5453
		[JsonIgnore]
		private static long lastTimeSetDisableCloseMenu;

		// Token: 0x0400154E RID: 5454
		[JsonIgnore]
		private static int indexSetToRename;

		// Token: 0x0400154F RID: 5455
		[JsonIgnore]
		private static Item[] arrItemBody = new Item[0];

		// Token: 0x04001550 RID: 5456
		[JsonIgnore]
		private static Item[] arrItemBag = new Item[0];

		// Token: 0x04001551 RID: 5457
		[JsonIgnore]
		private static Item[] arrItemBodyPet = new Item[0];

		// Token: 0x02000115 RID: 277
		internal class ItemSet
		{
			// Token: 0x06000E1E RID: 3614 RVA: 0x000A7698 File Offset: 0x000A5898
			internal ItemSet()
			{
			}

			// Token: 0x06000E1F RID: 3615 RVA: 0x000A76C4 File Offset: 0x000A58C4
			internal ItemSet(Item item)
			{
				if (item == null)
				{
					return;
				}
				this.name = item.template.name;
				this.fullName = item.GetFullInfo();
				this.gender = (int)item.template.gender;
				this.type = (int)item.template.type;
			}

			// Token: 0x06000E20 RID: 3616 RVA: 0x000A773E File Offset: 0x000A593E
			internal void Deconstruct()
			{
				this.name = "";
				this.fullName = "";
				this.gender = -1;
			}

			// Token: 0x06000E21 RID: 3617 RVA: 0x000A775D File Offset: 0x000A595D
			public override string ToString()
			{
				return this.fullName;
			}

			// Token: 0x04001552 RID: 5458
			internal string name = "";

			// Token: 0x04001553 RID: 5459
			internal string fullName = "";

			// Token: 0x04001554 RID: 5460
			internal int gender = -1;

			// Token: 0x04001555 RID: 5461
			internal int type = -1;
		}
	}
}
