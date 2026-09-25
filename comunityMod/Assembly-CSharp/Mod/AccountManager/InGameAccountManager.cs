using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using Mod.Graphics;
using Mod.R;
using Newtonsoft.Json;
using UnityEngine;

namespace Mod.AccountManager
{
	// Token: 0x0200018E RID: 398
	internal class InGameAccountManager : mScreen
	{
		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060011C7 RID: 4551 RVA: 0x000BEA77 File Offset: 0x000BCC77
		internal static Account SelectedAccount
		{
			get
			{
				if (InGameAccountManager.selectedAccountIndex != -1)
				{
					return InGameAccountManager.accounts[InGameAccountManager.selectedAccountIndex];
				}
				return null;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x060011C8 RID: 4552 RVA: 0x000BEA92 File Offset: 0x000BCC92
		// (set) Token: 0x060011C9 RID: 4553 RVA: 0x000BEA99 File Offset: 0x000BCC99
		internal static Server SelectedServer { get; set; }

		// Token: 0x060011CA RID: 4554 RVA: 0x000BEAA1 File Offset: 0x000BCCA1
		internal static InGameAccountManager gI()
		{
			if (InGameAccountManager.instance == null)
			{
				InGameAccountManager.instance = new InGameAccountManager();
			}
			return InGameAccountManager.instance;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x000BEABC File Offset: 0x000BCCBC
		public override void paint(mGraphics g)
		{
			g.setColor(0);
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			GameCanvas.paintBGGameScr(g);
			g.reset();
			InGameAccountManager.PaintListAccounts(g);
			InGameAccountManager.PaintCurrentAccountInfo(g);
			base.paint(g);
			if (InGameAccountManager.isImportingAccounts)
			{
				InGameAccountManager.PaintImportAccounts(g);
				return;
			}
			if (InGameAccountManager.isEditingCustomServer)
			{
				InGameAccountManager.PaintEditCustomServer(g);
				return;
			}
			if (InGameAccountManager.isAddingAccount || InGameAccountManager.isEditingAccount)
			{
				InGameAccountManager.PaintInputAccount(g);
			}
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x000BEB34 File Offset: 0x000BCD34
		public override void switchToMe()
		{
			InGameAccountManager.isSwitchedToMe = true;
			if (InGameAccountManager.defaultServers == null)
			{
				InGameAccountManager.defaultServers = new Server[ServerListScreen.nameServer.Length];
				for (int i = 0; i < InGameAccountManager.defaultServers.Length; i++)
				{
					InGameAccountManager.defaultServers[i] = new Server(i);
				}
			}
			InGameAccountManager.isAddingAccount = (InGameAccountManager.isEditingAccount = false);
			int num;
			int num2;
			int num3;
			int num4;
			InGameAccountManager.GetAccountsArea(out num, out num2, out num3, out num4, true);
			InGameAccountManager.scrollableMenuAccounts.X = num;
			InGameAccountManager.scrollableMenuAccounts.Y = num2;
			InGameAccountManager.scrollableMenuAccounts.Width = num3;
			InGameAccountManager.scrollableMenuAccounts.Height = num4;
			InGameAccountManager.scrollableMenuAccounts.Reset();
			InGameAccountManager.editAccount = new Command(Strings.edit, InGameAccountManager.ActionListener.gI(), 3, null)
			{
				w = 50
			};
			InGameAccountManager.deleteAccount = new Command(Strings.delete, InGameAccountManager.ActionListener.gI(), 4, null)
			{
				w = 50
			};
			InGameAccountManager.selectAccountToLogin = new Command(Strings.select, InGameAccountManager.ActionListener.gI(), 6, null)
			{
				w = 50
			};
			InGameAccountManager.finishInputAccount = new Command(Strings.save, InGameAccountManager.ActionListener.gI(), 8, null);
			InGameAccountManager.editCustomServer = new Command(Strings.inGameAccountManagerEditServer, InGameAccountManager.ActionListener.gI(), 10, null);
			InGameAccountManager.finishEditCustomServer = new Command(Strings.save, InGameAccountManager.ActionListener.gI(), 11, null);
			InGameAccountManager.cancelEditCustomServer = new Command(mResources.CANCEL, InGameAccountManager.ActionListener.gI(), 12, null);
			InGameAccountManager.cancelInputAccount = new Command(mResources.CANCEL, InGameAccountManager.ActionListener.gI(), 9, null);
			InGameAccountManager.selectServer = new ComboBox(mResources.server, ServerListScreen.nameServer.ToList<string>().Append(Strings.custom).ToList<string>());
			InGameAccountManager.finishImportAccounts = new Command(Strings.import, InGameAccountManager.ActionListener.gI(), 18, null);
			InGameAccountManager.cancelImportAccounts = new Command(mResources.CANCEL, InGameAccountManager.ActionListener.gI(), 19, null);
			InGameAccountManager.tfUser = new TField
			{
				name = ((mResources.language != 2) ? (mResources.phone + "/") : string.Empty) + mResources.email,
				width = InGameAccountManager.INPUT_ACCOUNT_WIDTH - 10,
				height = mScreen.ITEM_HEIGHT + 2
			};
			InGameAccountManager.tfPass = new TField
			{
				name = mResources.password,
				width = InGameAccountManager.INPUT_ACCOUNT_WIDTH - 10,
				height = mScreen.ITEM_HEIGHT + 2
			};
			InGameAccountManager.tfUser.setIputType(TField.INPUT_TYPE_ANY);
			InGameAccountManager.tfPass.setIputType(TField.INPUT_TYPE_PASSWORD);
			InGameAccountManager.tfCustomServerName = new TField
			{
				name = Strings.inGameAccountManagerServerName,
				width = InGameAccountManager.SELECT_SERVER_WIDTH - 10,
				height = mScreen.ITEM_HEIGHT + 2
			};
			InGameAccountManager.tfCustomServerAddress = new TField
			{
				name = Strings.inGameAccountManagerServerAddress,
				width = InGameAccountManager.SELECT_SERVER_WIDTH - 10,
				height = mScreen.ITEM_HEIGHT + 2
			};
			InGameAccountManager.tfCustomServerPort = new TField
			{
				name = Strings.inGameAccountManagerServerPort,
				width = InGameAccountManager.SELECT_SERVER_WIDTH - 10,
				height = mScreen.ITEM_HEIGHT + 2
			};
			InGameAccountManager.tfInputDataImportAccounts = new TField
			{
				name = Strings.inGameAccountManagerImportAccountsInputData,
				width = InGameAccountManager.IMPORT_ACCOUNTS_WIDTH - 10,
				height = mScreen.ITEM_HEIGHT + 2,
				multiline = true
			};
			InGameAccountManager.tfInputRegexMatchLinesImportAccounts = new TField
			{
				name = Strings.inGameAccountManagerRegexMatchLines,
				width = InGameAccountManager.IMPORT_ACCOUNTS_WIDTH - 10,
				height = mScreen.ITEM_HEIGHT + 2
			};
			InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts = new TField
			{
				name = Strings.inGameAccountManagerRegexMatchAccountInfo,
				width = InGameAccountManager.IMPORT_ACCOUNTS_WIDTH - 10,
				height = mScreen.ITEM_HEIGHT + 2
			};
			InGameAccountManager.tfCustomServerName.setIputType(TField.INPUT_TYPE_ANY);
			InGameAccountManager.tfCustomServerAddress.setIputType(TField.INPUT_TYPE_ANY);
			InGameAccountManager.tfCustomServerPort.setIputType(TField.INPUT_TYPE_NUMERIC);
			InGameAccountManager.tfInputDataImportAccounts.setIputType(TField.INPUT_TYPE_ANY);
			InGameAccountManager.tfInputRegexMatchLinesImportAccounts.setIputType(TField.INPUT_TYPE_ANY);
			InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.setIputType(TField.INPUT_TYPE_ANY);
			InGameAccountManager.tfInputDataImportAccounts.setMaxTextLenght(int.MaxValue);
			InGameAccountManager.UpdateSizeAndPos();
			base.switchToMe();
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x000BEF34 File Offset: 0x000BD134
		public override void update()
		{
			GameScr.cmx++;
			if (GameScr.cmx > GameCanvas.w * 3 + 100)
			{
				GameScr.cmx = 100;
			}
			if (InGameAccountManager.isImportingAccounts && !InGameAccountManager.selectServer.IsShowingListItems)
			{
				if (!InfoDlg.isLock)
				{
					InGameAccountManager.tfInputDataImportAccounts.update();
					InGameAccountManager.tfInputRegexMatchLinesImportAccounts.update();
					InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.update();
				}
			}
			else if (InGameAccountManager.isEditingCustomServer)
			{
				InGameAccountManager.tfCustomServerName.update();
				InGameAccountManager.tfCustomServerAddress.update();
				InGameAccountManager.tfCustomServerPort.update();
			}
			else if ((InGameAccountManager.isAddingAccount || InGameAccountManager.isEditingAccount) && !InGameAccountManager.selectServer.IsShowingListItems)
			{
				InGameAccountManager.tfUser.update();
				InGameAccountManager.tfPass.update();
			}
			InGameAccountManager.scrollableMenuAccounts.Update();
			InGameAccountManager.selectServer.Update();
			if (InGameAccountManager.isImportingAccounts && InGameAccountManager.selectServer.SelectedIndex == InGameAccountManager.selectServer.Items.Count - 1)
			{
				InGameAccountManager.selectServer.SelectedIndex = 0;
			}
			int num;
			int num2;
			int num3;
			int num4;
			InGameAccountManager.GetAccountsArea(out num, out num2, out num3, out num4, true);
			InGameAccountManager.scrollableMenuAccounts.Width = num3;
			InGameAccountManager.scrollableMenuAccounts.Height = num4;
			if (InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex != -1)
			{
				int num5 = GameCanvas.w - InGameAccountManager.WIDTH_ACCOUNT_INFO - 40;
				if (InGameAccountManager.currentAccountInfoX > num5)
				{
					if (InGameAccountManager.currentAccountInfoX - 50 < num5)
					{
						InGameAccountManager.currentAccountInfoX = num5;
					}
					else
					{
						InGameAccountManager.currentAccountInfoX -= 50;
					}
				}
			}
			else if (InGameAccountManager.currentAccountInfoX < GameCanvas.w)
			{
				if (InGameAccountManager.currentAccountInfoX + 50 > GameCanvas.w)
				{
					InGameAccountManager.currentAccountInfoX = GameCanvas.w;
				}
				else
				{
					InGameAccountManager.currentAccountInfoX += 50;
				}
			}
			InGameAccountManager.UpdateButtonsPos();
			base.update();
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000BF0E4 File Offset: 0x000BD2E4
		public override void updateKey()
		{
			if (InGameAccountManager.isImportingAccounts)
			{
				InGameAccountManager.UpdateKeyImportAccounts();
			}
			else if (InGameAccountManager.isEditingCustomServer)
			{
				InGameAccountManager.UpdateKeyEditCustomServer();
			}
			else if (InGameAccountManager.isAddingAccount || InGameAccountManager.isEditingAccount)
			{
				InGameAccountManager.UpdateKeyInputAccount();
			}
			else
			{
				InGameAccountManager.UpdateKeyMain();
			}
			base.updateKey();
			GameCanvas.clearKeyPressed();
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000BF134 File Offset: 0x000BD334
		public override void keyPress(int keyCode)
		{
			if (InGameAccountManager.isImportingAccounts)
			{
				if (InGameAccountManager.tfInputDataImportAccounts.isFocus)
				{
					InGameAccountManager.tfInputDataImportAccounts.keyPressed(keyCode);
				}
				else if (InGameAccountManager.tfInputRegexMatchLinesImportAccounts.isFocus)
				{
					InGameAccountManager.tfInputRegexMatchLinesImportAccounts.keyPressed(keyCode);
				}
				else if (InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.isFocus)
				{
					InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.keyPressed(keyCode);
				}
			}
			else if (InGameAccountManager.isEditingCustomServer)
			{
				if (InGameAccountManager.tfCustomServerName.isFocus)
				{
					InGameAccountManager.tfCustomServerName.keyPressed(keyCode);
				}
				else if (InGameAccountManager.tfCustomServerAddress.isFocus)
				{
					InGameAccountManager.tfCustomServerAddress.keyPressed(keyCode);
				}
				else if (InGameAccountManager.tfCustomServerPort.isFocus)
				{
					InGameAccountManager.tfCustomServerPort.keyPressed(keyCode);
				}
			}
			else if (InGameAccountManager.isAddingAccount || InGameAccountManager.isEditingAccount)
			{
				if (InGameAccountManager.tfUser.isFocus)
				{
					InGameAccountManager.tfUser.keyPressed(keyCode);
				}
				else if (InGameAccountManager.tfPass.isFocus)
				{
					InGameAccountManager.tfPass.keyPressed(keyCode);
				}
			}
			base.keyPress(keyCode);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x000BF240 File Offset: 0x000BD440
		private static void LoadDataAccounts()
		{
			string text;
			if (Utils.TryLoadDataString("account_manager_accounts", out text, true))
			{
				InGameAccountManager.accounts = JsonConvert.DeserializeObject<List<Account>>(text) ?? new List<Account>();
			}
			long num;
			if (Utils.TryLoadDataLong("account_manager_selected_account_index", out num, true))
			{
				InGameAccountManager.selectedAccountIndex = (int)num;
			}
			if (InGameAccountManager.selectedAccountIndex >= InGameAccountManager.accounts.Count)
			{
				InGameAccountManager.selectedAccountIndex = -1;
			}
			if (InGameAccountManager.selectedAccountIndex != -1 && !InGameAccountManager.SelectedAccount.Server.IsCustomIP())
			{
				ServerListScreen.ipSelect = InGameAccountManager.SelectedAccount.Server.index;
				Rms.saveRMSInt("svselect", InGameAccountManager.SelectedAccount.Server.index);
			}
			if (InGameAccountManager.selectedAccountIndex != -1)
			{
				InGameAccountManager.SelectedServer = InGameAccountManager.SelectedAccount.Server;
			}
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x000BF2FA File Offset: 0x000BD4FA
		private static void SaveDataAccounts()
		{
			Utils.SaveData("account_manager_accounts", JsonConvert.SerializeObject(InGameAccountManager.accounts), true);
			Utils.SaveData("account_manager_selected_account_index", (long)InGameAccountManager.selectedAccountIndex, true);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000BF324 File Offset: 0x000BD524
		internal static void AddUserAoToAccountManager()
		{
			string userAo = Rms.loadRMSString("userAo" + ServerListScreen.ipSelect.ToString());
			if (string.IsNullOrEmpty(userAo))
			{
				return;
			}
			if (InGameAccountManager.accounts.Any<Account>((Account acc) => acc.Username == userAo))
			{
				GameCanvas.startOKDlg(Strings.inGameAccountManagerUnregisteredAccountAlreadyAdded + "!");
				return;
			}
			Account account = new Account
			{
				Username = userAo,
				Server = new Server(ServerListScreen.ipSelect),
				LastTimeLogin = DateTime.Now
			};
			InGameAccountManager.accounts.Add(account);
			InGameAccountManager.selectedAccountIndex = InGameAccountManager.accounts.Count - 1;
			Rms.DeleteStorage("userAo" + account.Server.index.ToString());
			InGameAccountManager.SaveDataAccounts();
			GameScr.info1.addInfo(Strings.inGameAccountManagerAccountAdded + "!", 0);
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000BF418 File Offset: 0x000BD618
		internal static void ResetSelectedAccountIndex()
		{
			InGameAccountManager.selectedAccountIndex = -1;
			InGameAccountManager.SaveDataAccounts();
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x000BF428 File Offset: 0x000BD628
		private static void PaintListAccounts(mGraphics g)
		{
			int num;
			int num2;
			int num3;
			int num4;
			InGameAccountManager.GetAccountsArea(out num, out num2, out num3, out num4, false);
			PopUp.paintPopUp(g, num - 10, num2 - 10, num3 + 20, InGameAccountManager.TITLE_HEIGHT + 50, 0, true);
			mFont.tahoma_7b_dark.drawString(g, Strings.accounts, num + 10, num2, 0);
			InGameAccountManager.addAccount.paint(g);
			if (InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex > -1)
			{
				InGameAccountManager.moveAccountDown.paint(g);
				InGameAccountManager.moveAccountUp.paint(g);
			}
			PopUp.paintPopUp(g, num - 10, num2 + InGameAccountManager.TITLE_HEIGHT - 10, num3 + 20, num4 - InGameAccountManager.TITLE_HEIGHT + 20, 0, true);
			int num5;
			int num6;
			InGameAccountManager.GetAccountsArea(out num5, out num2, out num6, out num4, true);
			if (InGameAccountManager.accounts.Count > num4 / InGameAccountManager.ACCOUNT_HEIGHT)
			{
				g.setColor(10062200);
				int num7 = num4 * num4 / InGameAccountManager.ACCOUNT_HEIGHT / InGameAccountManager.accounts.Count;
				int num8 = num2 + Mathf.Clamp(num4 * InGameAccountManager.scrollableMenuAccounts.CurrentOffset / InGameAccountManager.ACCOUNT_HEIGHT / InGameAccountManager.accounts.Count, 0, num4 - num7);
				g.fillRect(num + num3 + 3, num8, 4, num7);
			}
			g.setColor(13870191);
			g.fillRect(num, num2, num3, num4);
			g.setColor(Color.black);
			g.drawRect(num - 1, num2 - 1, num3 + 1, num4 + 1);
			g.setClip(num, num2, num3, num4);
			InGameAccountManager.scrollableMenuAccounts.Paint(g);
			g.reset();
			InGameAccountManager.closeAccountManager.paint(g);
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x000BF59C File Offset: 0x000BD79C
		private static void PaintCurrentAccountInfo(mGraphics g)
		{
			if (InGameAccountManager.currentAccountInfoX == GameCanvas.w)
			{
				return;
			}
			int y_LIST_ACCOUNTS = InGameAccountManager.Y_LIST_ACCOUNTS;
			int num = GameCanvas.h - y_LIST_ACCOUNTS * 2;
			Account account = null;
			if (InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex != -1)
			{
				account = InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex];
			}
			PopUp.paintPopUp(g, InGameAccountManager.currentAccountInfoX - 10, y_LIST_ACCOUNTS - 10, InGameAccountManager.WIDTH_ACCOUNT_INFO + 20, num + 20, (int)((account == null) ? AccountType.Registered : account.Type), true);
			num -= InGameAccountManager.editAccount.h;
			PopUp.paintPopUp(g, InGameAccountManager.currentAccountInfoX, y_LIST_ACCOUNTS, InGameAccountManager.WIDTH_ACCOUNT_INFO, num, 16777215, false);
			InGameAccountManager.editAccount.paint(g);
			InGameAccountManager.deleteAccount.paint(g);
			InGameAccountManager.selectAccountToLogin.paint(g);
			if (InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex == -1)
			{
				return;
			}
			g.setClip(InGameAccountManager.currentAccountInfoX, y_LIST_ACCOUNTS, InGameAccountManager.WIDTH_ACCOUNT_INFO, num);
			InGameAccountManager.EnsureSmallImage(account.Info.Icon);
			int num2 = InGameAccountManager.currentAccountInfoX;
			Texture2D texture2D = InGameAccountManager.icons[account.Info.Icon];
			if (texture2D != null)
			{
				float num3 = (float)(InGameAccountManager.ACCOUNT_HEIGHT * texture2D.width) / (float)texture2D.height;
				num2 += 65;
				InGameAccountManager.DrawTexture((float)(InGameAccountManager.currentAccountInfoX + 10), (float)(y_LIST_ACCOUNTS + 10), num3, (float)InGameAccountManager.ACCOUNT_HEIGHT, texture2D, ScaleMode.ScaleToFit);
				if (account.PetInfo != null)
				{
					InGameAccountManager.EnsureSmallImage(account.PetInfo.Icon);
					Texture2D texture2D2 = InGameAccountManager.icons[account.PetInfo.Icon];
					if (texture2D2 != null)
					{
						float num4 = (float)InGameAccountManager.ACCOUNT_HEIGHT / 2f * (float)texture2D2.width / (float)texture2D2.height;
						num2 -= InGameAccountManager.ACCOUNT_HEIGHT / 2;
						num2 += 30;
						InGameAccountManager.DrawTexture((float)(InGameAccountManager.currentAccountInfoX + 47), (float)(y_LIST_ACCOUNTS + 10) + (float)InGameAccountManager.ACCOUNT_HEIGHT / 2f, num4, (float)InGameAccountManager.ACCOUNT_HEIGHT / 2f, texture2D2, ScaleMode.ScaleToFit);
					}
				}
			}
			else
			{
				num2 += 20;
			}
			mFont mFont = mFont.tahoma_7b_dark;
			if (account.Type == AccountType.Unregistered)
			{
				mFont = mFont.tahoma_7b_green2;
			}
			mFont.drawString(g, account.Info.Name, num2, y_LIST_ACCOUNTS + 5, 0);
			string text = (account.Server.IsCustomIP() ? account.Server.name : ServerListScreen.nameServer[account.Server.index]);
			string text2 = ((account.LastTimeLogin != DateTime.MinValue) ? (Strings.lastLogin + ": ") : "") + account.GetLastTimeLogin();
			if (mFont.tahoma_7_greySmall.getWidth(text2) + num2 - InGameAccountManager.currentAccountInfoX > InGameAccountManager.WIDTH_ACCOUNT_INFO)
			{
				mFont.tahoma_7_greySmall.drawString(g, mResources.server + " " + text, num2, y_LIST_ACCOUNTS + 15, 0);
				mFont.tahoma_7_greySmall.drawString(g, Strings.lastLogin + ":", num2, y_LIST_ACCOUNTS + 25, 0);
				mFont.tahoma_7_greySmall.drawString(g, account.GetLastTimeLogin(), num2, y_LIST_ACCOUNTS + 35, 0);
			}
			else
			{
				mFont.tahoma_7_greySmall.drawString(g, mResources.server + " " + text, num2, y_LIST_ACCOUNTS + 15, 0);
				mFont.tahoma_7_greySmall.drawString(g, text2, num2, y_LIST_ACCOUNTS + 25, 0);
			}
			g.setColor(new Color(0f, 0f, 0f, 0.3f));
			g.fillRect(InGameAccountManager.currentAccountInfoX + 20, y_LIST_ACCOUNTS + 50, InGameAccountManager.WIDTH_ACCOUNT_INFO - 40, 1);
			mFont.tahoma_7b_dark.drawString(g, Strings.info, InGameAccountManager.currentAccountInfoX + InGameAccountManager.WIDTH_ACCOUNT_INFO / 2 - mFont.tahoma_7b_dark.getWidth(Strings.info) / 2, y_LIST_ACCOUNTS + 55, mFont.LEFT);
			int num5 = 65;
			int num6 = InGameAccountManager.currentAccountInfoX;
			if (account.PetInfo != null)
			{
				num6 += 10;
				int num7 = InGameAccountManager.currentAccountInfoX + InGameAccountManager.WIDTH_ACCOUNT_INFO / 2;
				mFont.tahoma_7b_focus.drawString(g, Strings.master, InGameAccountManager.currentAccountInfoX + InGameAccountManager.WIDTH_ACCOUNT_INFO / 4 - mFont.tahoma_7b_focus.getWidth(Strings.master) / 2, y_LIST_ACCOUNTS + num5, 0);
				mFont.tahoma_7b_focus.drawString(g, mResources.pet, InGameAccountManager.currentAccountInfoX + InGameAccountManager.WIDTH_ACCOUNT_INFO * 3 / 4 - mFont.tahoma_7b_focus.getWidth(mResources.pet) / 2, y_LIST_ACCOUNTS + num5, 0);
				num5 += 5;
				g.fillRect(num7, y_LIST_ACCOUNTS + num5, 1, 58);
				num5 += 7;
				mFont.tahoma_7_greySmall.drawString(g, "CharID: " + account.Info.CharID.ToString(), InGameAccountManager.currentAccountInfoX + 10, y_LIST_ACCOUNTS + num5, 0);
				mFont.tahoma_7_greySmall.drawString(g, Strings.name + ": " + account.PetInfo.Name, InGameAccountManager.currentAccountInfoX + 10 + InGameAccountManager.WIDTH_ACCOUNT_INFO / 2, y_LIST_ACCOUNTS + num5, 0);
				num5 += 10;
				mFont.tahoma_7_greySmall.drawString(g, mResources.HP + ": " + Utils.FormatWithSIPrefix((double)account.Info.MaxHP), InGameAccountManager.currentAccountInfoX + 10, y_LIST_ACCOUNTS + num5, 0);
				mFont.tahoma_7_greySmall.drawString(g, mResources.HP + ": " + Utils.FormatWithSIPrefix((double)account.PetInfo.MaxHP), InGameAccountManager.currentAccountInfoX + 10 + InGameAccountManager.WIDTH_ACCOUNT_INFO / 2, y_LIST_ACCOUNTS + num5, 0);
				num5 += 10;
				mFont.tahoma_7_greySmall.drawString(g, mResources.KI + ": " + Utils.FormatWithSIPrefix((double)account.Info.MaxMP), InGameAccountManager.currentAccountInfoX + 10, y_LIST_ACCOUNTS + num5, 0);
				mFont.tahoma_7_greySmall.drawString(g, mResources.KI + ": " + Utils.FormatWithSIPrefix((double)account.PetInfo.MaxMP), InGameAccountManager.currentAccountInfoX + 10 + InGameAccountManager.WIDTH_ACCOUNT_INFO / 2, y_LIST_ACCOUNTS + num5, 0);
				num5 += 10;
				mFont.tahoma_7_greySmall.drawString(g, mResources.power + ": " + Utils.FormatWithSIPrefix((double)account.Info.EXP), InGameAccountManager.currentAccountInfoX + 10, y_LIST_ACCOUNTS + num5, 0);
				mFont.tahoma_7_greySmall.drawString(g, mResources.power + ": " + Utils.FormatWithSIPrefix((double)account.PetInfo.EXP), InGameAccountManager.currentAccountInfoX + 10 + InGameAccountManager.WIDTH_ACCOUNT_INFO / 2, y_LIST_ACCOUNTS + num5, 0);
				num5 += 10;
				mFont.tahoma_7_greySmall.drawString(g, Strings.gender + ": " + account.Info.GetGender(), InGameAccountManager.currentAccountInfoX + 10, y_LIST_ACCOUNTS + num5, 0);
				mFont.tahoma_7_greySmall.drawString(g, Strings.gender + ": " + account.PetInfo.GetGender(), InGameAccountManager.currentAccountInfoX + 10 + InGameAccountManager.WIDTH_ACCOUNT_INFO / 2, y_LIST_ACCOUNTS + num5, 0);
				num5 += 10;
			}
			else
			{
				num5 += 2;
				num6 += 20;
				mFont.tahoma_7_greySmall.drawString(g, "CharID: " + account.Info.CharID.ToString(), InGameAccountManager.currentAccountInfoX + 20, y_LIST_ACCOUNTS + num5, 0);
				num5 += 10;
				mFont.tahoma_7_greySmall.drawString(g, mResources.HP + ": " + Utils.FormatWithSIPrefix((double)account.Info.MaxHP), InGameAccountManager.currentAccountInfoX + 20, y_LIST_ACCOUNTS + num5, 0);
				num5 += 10;
				mFont.tahoma_7_greySmall.drawString(g, mResources.KI + ": " + Utils.FormatWithSIPrefix((double)account.Info.MaxMP), InGameAccountManager.currentAccountInfoX + 20, y_LIST_ACCOUNTS + num5, 0);
				num5 += 10;
				mFont.tahoma_7_greySmall.drawString(g, mResources.power + ": " + Utils.FormatWithSIPrefix((double)account.Info.EXP), InGameAccountManager.currentAccountInfoX + 20, y_LIST_ACCOUNTS + num5, 0);
				num5 += 10;
				mFont.tahoma_7_greySmall.drawString(g, Strings.gender + ": " + account.Info.GetGender(), InGameAccountManager.currentAccountInfoX + 20, y_LIST_ACCOUNTS + num5, 0);
				num5 += 10;
			}
			num5 += 5;
			g.drawImage(Panel.imgXu, num6, y_LIST_ACCOUNTS + num5);
			num6 += Panel.imgXu.getWidth() + 5;
			mFont.tahoma_7_greySmall.drawString(g, Utils.FormatWithSIPrefix((double)account.Gold), num6, y_LIST_ACCOUNTS + num5, 0);
			num6 += mFont.tahoma_7_greySmall.getWidth(Utils.FormatWithSIPrefix((double)account.Gold)) + 5;
			g.drawImage(Panel.imgLuong, num6, y_LIST_ACCOUNTS + num5);
			num6 += Panel.imgLuong.getWidth() + 5;
			mFont.tahoma_7_greySmall.drawString(g, Utils.FormatWithSIPrefix((double)account.Gem), num6, y_LIST_ACCOUNTS + num5, 0);
			num6 += mFont.tahoma_7_greySmall.getWidth(Utils.FormatWithSIPrefix((double)account.Gem)) + 5;
			g.drawImage(Panel.imgLuongKhoa, num6, y_LIST_ACCOUNTS + num5);
			num6 += Panel.imgLuongKhoa.getWidth() + 5;
			mFont.tahoma_7_greySmall.drawString(g, Utils.FormatWithSIPrefix((double)account.Ruby), num6, y_LIST_ACCOUNTS + num5, 0);
			num5 += 15;
			g.fillRect(InGameAccountManager.currentAccountInfoX + 20, y_LIST_ACCOUNTS + num5, InGameAccountManager.WIDTH_ACCOUNT_INFO - 40, 1);
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000BFEC4 File Offset: 0x000BE0C4
		private static void PaintInputAccount(mGraphics g)
		{
			g.setColor(new Color(0f, 0f, 0f, 0.5f));
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			int num;
			int num2;
			int num3;
			int num4;
			InGameAccountManager.GetInputAccountArea(out num, out num2, out num3, out num4, false);
			PopUp.paintPopUp(g, num, num2, num3, InGameAccountManager.TITLE_HEIGHT + 10, -1, true);
			if (InGameAccountManager.isAddingAccount)
			{
				mFont.tahoma_7b_dark.drawString(g, Strings.inGameAccountManagerAddAccount, num + num3 / 2, num2 + 10, mFont.CENTER);
			}
			else if (InGameAccountManager.isEditingAccount)
			{
				mFont.tahoma_7b_dark.drawString(g, Strings.inGameAccountManagerEditAccount, num + num3 / 2, num2 + 10, mFont.CENTER);
			}
			InGameAccountManager.closeInputAccount.paint(g);
			InGameAccountManager.toggleHidePassword.paint(g);
			InGameAccountManager.importAccounts.paint(g);
			int num5;
			int num6;
			InGameAccountManager.GetInputAccountArea(out num5, out num2, out num6, out num4, true);
			PopUp.paintPopUp(g, num, num2, num3, num4, -1, true);
			InGameAccountManager.tfUser.paint(g);
			InGameAccountManager.tfPass.paint(g);
			g.reset();
			if (!InGameAccountManager.selectServer.IsShowingListItems && InGameAccountManager.selectServer.SelectedIndex == InGameAccountManager.selectServer.Items.Count - 1)
			{
				InGameAccountManager.editCustomServer.paint(g);
			}
			else
			{
				InGameAccountManager.finishInputAccount.paint(g);
			}
			InGameAccountManager.cancelInputAccount.paint(g);
			InGameAccountManager.selectServer.Paint(g);
			g.reset();
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x000C0028 File Offset: 0x000BE228
		private static void PaintEditCustomServer(mGraphics g)
		{
			g.setColor(new Color(0f, 0f, 0f, 0.75f));
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			int num;
			int num2;
			int num3;
			int num4;
			InGameAccountManager.GetInputAccountArea(out num, out num2, out num3, out num4, false);
			PopUp.paintPopUp(g, num, num2, num3, num4, -1, true);
			mFont.tahoma_7b_dark.drawString(g, Strings.inGameAccountManagerEditServer, num + num3 / 2, num2 + 10, mFont.CENTER);
			int num5;
			int num6;
			InGameAccountManager.GetInputAccountArea(out num5, out num2, out num6, out num4, true);
			PopUp.paintPopUp(g, num, num2, num3, num4, -1, true);
			InGameAccountManager.tfCustomServerName.paint(g);
			InGameAccountManager.tfCustomServerAddress.paint(g);
			InGameAccountManager.tfCustomServerPort.paint(g);
			g.reset();
			InGameAccountManager.finishEditCustomServer.paint(g);
			InGameAccountManager.cancelEditCustomServer.paint(g);
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x000C00F8 File Offset: 0x000BE2F8
		private static void PaintImportAccounts(mGraphics g)
		{
			g.setColor(new Color(0f, 0f, 0f, 0.75f));
			g.fillRect(0, 0, GameCanvas.w, GameCanvas.h);
			int num;
			int num2;
			int num3;
			int num4;
			InGameAccountManager.GetImportAccountsArea(out num, out num2, out num3, out num4, false);
			PopUp.paintPopUp(g, num, num2, num3, num4, -1, true);
			mFont.tahoma_7b_dark.drawString(g, Strings.inGameAccountManagerImportAccounts, num + num3 / 2, num2 + 10, mFont.CENTER);
			InGameAccountManager.helpImportAccounts.paint(g);
			InGameAccountManager.closeImportAccounts.paint(g);
			int num5;
			int num6;
			InGameAccountManager.GetImportAccountsArea(out num5, out num2, out num6, out num4, true);
			PopUp.paintPopUp(g, num, num2, num3, num4, -1, true);
			InGameAccountManager.tfInputDataImportAccounts.paint(g);
			InGameAccountManager.tfInputRegexMatchLinesImportAccounts.paint(g);
			InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.paint(g);
			g.reset();
			InGameAccountManager.finishImportAccounts.paint(g);
			InGameAccountManager.cancelImportAccounts.paint(g);
			g.reset();
			InGameAccountManager.selectServer.Paint(g);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x000C01EC File Offset: 0x000BE3EC
		private static void UpdateKeyInputAccount()
		{
			InGameAccountManager.selectServer.UpdateKey();
			if (InGameAccountManager.selectServer.IsShowingListItems)
			{
				return;
			}
			if (InGameAccountManager.closeInputAccount.isPointerPressInside())
			{
				InGameAccountManager.closeInputAccount.performAction();
			}
			if (InGameAccountManager.toggleHidePassword.isPointerPressInside())
			{
				InGameAccountManager.toggleHidePassword.performAction();
			}
			if (InGameAccountManager.importAccounts.isPointerPressInside())
			{
				InGameAccountManager.importAccounts.performAction();
			}
			if (!InGameAccountManager.selectServer.IsShowingListItems && InGameAccountManager.selectServer.SelectedIndex == InGameAccountManager.selectServer.Items.Count - 1)
			{
				if (InGameAccountManager.editCustomServer.isPointerPressInside())
				{
					InGameAccountManager.editCustomServer.performAction();
				}
			}
			else if (InGameAccountManager.finishInputAccount.isPointerPressInside())
			{
				InGameAccountManager.finishInputAccount.performAction();
			}
			if (InGameAccountManager.cancelInputAccount.isPointerPressInside())
			{
				InGameAccountManager.cancelInputAccount.performAction();
			}
			if (GameCanvas.keyPressed[16])
			{
				GameCanvas.clearKeyPressed();
				if (InGameAccountManager.tfUser.isFocus)
				{
					InGameAccountManager.tfUser.isFocus = false;
					InGameAccountManager.tfPass.isFocus = true;
					return;
				}
				if (InGameAccountManager.tfPass.isFocus)
				{
					InGameAccountManager.tfPass.isFocus = false;
					InGameAccountManager.selectServer.IsFocus = true;
					return;
				}
				if (InGameAccountManager.selectServer.IsFocus)
				{
					InGameAccountManager.selectServer.IsFocus = false;
					return;
				}
				InGameAccountManager.tfUser.isFocus = true;
			}
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000C033C File Offset: 0x000BE53C
		private static void UpdateKeyEditCustomServer()
		{
			if (InGameAccountManager.finishEditCustomServer.isPointerPressInside())
			{
				InGameAccountManager.finishEditCustomServer.performAction();
			}
			if (InGameAccountManager.cancelEditCustomServer.isPointerPressInside())
			{
				InGameAccountManager.cancelEditCustomServer.performAction();
			}
			if (GameCanvas.keyPressed[16])
			{
				GameCanvas.clearKeyPressed();
				if (InGameAccountManager.tfCustomServerName.isFocus)
				{
					InGameAccountManager.tfCustomServerName.isFocus = false;
					InGameAccountManager.tfCustomServerAddress.isFocus = true;
					return;
				}
				if (InGameAccountManager.tfCustomServerAddress.isFocus)
				{
					InGameAccountManager.tfCustomServerAddress.isFocus = false;
					InGameAccountManager.tfCustomServerPort.isFocus = true;
					return;
				}
				if (InGameAccountManager.tfCustomServerPort.isFocus)
				{
					InGameAccountManager.tfCustomServerPort.isFocus = false;
					return;
				}
				InGameAccountManager.tfCustomServerName.isFocus = true;
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000C03F0 File Offset: 0x000BE5F0
		private static void UpdateKeyImportAccounts()
		{
			if (InfoDlg.isLock)
			{
				return;
			}
			InGameAccountManager.selectServer.UpdateKey();
			if (InGameAccountManager.selectServer.IsShowingListItems)
			{
				return;
			}
			if (InGameAccountManager.finishImportAccounts.isPointerPressInside())
			{
				InGameAccountManager.finishImportAccounts.performAction();
			}
			if (InGameAccountManager.cancelImportAccounts.isPointerPressInside())
			{
				InGameAccountManager.cancelImportAccounts.performAction();
			}
			if (InGameAccountManager.closeImportAccounts.isPointerPressInside())
			{
				InGameAccountManager.closeImportAccounts.performAction();
			}
			if (InGameAccountManager.helpImportAccounts.isPointerPressInside())
			{
				InGameAccountManager.helpImportAccounts.performAction();
			}
			if (GameCanvas.keyPressed[16])
			{
				GameCanvas.clearKeyPressed();
				if (InGameAccountManager.tfInputDataImportAccounts.isFocus)
				{
					InGameAccountManager.tfInputDataImportAccounts.isFocus = false;
					InGameAccountManager.tfInputRegexMatchLinesImportAccounts.isFocus = true;
					return;
				}
				if (InGameAccountManager.tfInputRegexMatchLinesImportAccounts.isFocus)
				{
					InGameAccountManager.tfInputRegexMatchLinesImportAccounts.isFocus = false;
					InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.isFocus = true;
					return;
				}
				if (InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.isFocus)
				{
					InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.isFocus = false;
					return;
				}
				InGameAccountManager.tfInputDataImportAccounts.isFocus = true;
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000C04EC File Offset: 0x000BE6EC
		private static void UpdateKeyMain()
		{
			if (GameCanvas.keyPressed[13] && InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex == -1)
			{
				InGameAccountManager.closeAccountManager.performAction();
			}
			int num;
			int num2;
			int num3;
			int num4;
			InGameAccountManager.GetAccountsArea(out num, out num2, out num3, out num4, true);
			InGameAccountManager.scrollableMenuAccounts.UpdateKey();
			if (InGameAccountManager.closeAccountManager.isPointerPressInside())
			{
				InGameAccountManager.closeAccountManager.performAction();
			}
			if (InGameAccountManager.editAccount.isPointerPressInside())
			{
				InGameAccountManager.editAccount.performAction();
			}
			if (InGameAccountManager.deleteAccount.isPointerPressInside())
			{
				InGameAccountManager.deleteAccount.performAction();
			}
			if (InGameAccountManager.selectAccountToLogin.isPointerPressInside())
			{
				InGameAccountManager.selectAccountToLogin.performAction();
			}
			if (InGameAccountManager.addAccount.isPointerPressInside())
			{
				InGameAccountManager.addAccount.performAction();
			}
			if (InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex > -1)
			{
				if (InGameAccountManager.moveAccountUp.isPointerPressInside())
				{
					InGameAccountManager.moveAccountUp.performAction();
				}
				if (InGameAccountManager.moveAccountDown.isPointerPressInside())
				{
					InGameAccountManager.moveAccountDown.performAction();
				}
			}
			if (GameCanvas.keyPressed[(!Main.isPC) ? 5 : 25] && InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex != -1)
			{
				InGameAccountManager.selectAccountToLogin.performAction();
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000C0604 File Offset: 0x000BE804
		internal static void UpdateSizeAndPos()
		{
			if (!InGameAccountManager.isSwitchedToMe)
			{
				return;
			}
			if (InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex != -1)
			{
				InGameAccountManager.currentAccountInfoX = GameCanvas.w - InGameAccountManager.WIDTH_ACCOUNT_INFO - 40;
			}
			else
			{
				InGameAccountManager.currentAccountInfoX = GameCanvas.w;
			}
			InGameAccountManager.UpdateButtonsPos();
			int num;
			int num2;
			int num3;
			int num4;
			InGameAccountManager.GetInputAccountArea(out num, out num2, out num3, out num4, true);
			TField tfield = InGameAccountManager.tfUser;
			TField tfield2 = InGameAccountManager.tfPass;
			ComboBox comboBox = InGameAccountManager.selectServer;
			TField tfield3 = InGameAccountManager.tfCustomServerName;
			TField tfield4 = InGameAccountManager.tfCustomServerAddress;
			int num5 = (InGameAccountManager.tfCustomServerPort.x = num + 10);
			num5 = (tfield4.x = num5);
			num5 = (tfield3.x = num5);
			int num6 = (comboBox.X = num5);
			num6 = (tfield2.x = num6);
			tfield.x = num6;
			TField tfield5 = InGameAccountManager.tfUser;
			num6 = (InGameAccountManager.tfCustomServerName.y = num2 + 15);
			tfield5.y = num6;
			TField tfield6 = InGameAccountManager.tfPass;
			num6 = (InGameAccountManager.tfCustomServerAddress.y = InGameAccountManager.tfUser.y + mScreen.ITEM_HEIGHT + 15);
			tfield6.y = num6;
			ComboBox comboBox2 = InGameAccountManager.selectServer;
			num6 = (InGameAccountManager.tfCustomServerPort.y = InGameAccountManager.tfPass.y + mScreen.ITEM_HEIGHT + 15);
			comboBox2.Y = num6;
			TField tfield7 = InGameAccountManager.tfUser;
			TField tfield8 = InGameAccountManager.tfPass;
			ComboBox comboBox3 = InGameAccountManager.selectServer;
			TField tfield9 = InGameAccountManager.tfCustomServerName;
			TField tfield10 = InGameAccountManager.tfCustomServerAddress;
			num5 = (InGameAccountManager.tfCustomServerPort.width = num3 - 20);
			num5 = (tfield10.width = num5);
			num5 = (tfield9.width = num5);
			num6 = (comboBox3.Width = num5);
			num6 = (tfield8.width = num6);
			tfield7.width = num6;
			InGameAccountManager.GetImportAccountsArea(out num, out num2, out num3, out num4, true);
			TField tfield11 = InGameAccountManager.tfInputDataImportAccounts;
			TField tfield12 = InGameAccountManager.tfInputRegexMatchLinesImportAccounts;
			num6 = (InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.x = num + 10);
			num6 = (tfield12.x = num6);
			tfield11.x = num6;
			InGameAccountManager.tfInputDataImportAccounts.y = num2 + 15;
			InGameAccountManager.tfInputRegexMatchLinesImportAccounts.y = InGameAccountManager.tfInputDataImportAccounts.y + mScreen.ITEM_HEIGHT + 15;
			InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.y = InGameAccountManager.tfInputRegexMatchLinesImportAccounts.y + mScreen.ITEM_HEIGHT + 15;
			TField tfield13 = InGameAccountManager.tfInputDataImportAccounts;
			TField tfield14 = InGameAccountManager.tfInputRegexMatchLinesImportAccounts;
			num6 = (InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.width = num3 - 20);
			num6 = (tfield14.width = num6);
			tfield13.width = num6;
			if (InGameAccountManager.isImportingAccounts)
			{
				InGameAccountManager.selectServer.X = InGameAccountManager.tfInputDataImportAccounts.x;
				InGameAccountManager.selectServer.Y = InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.y + mScreen.ITEM_HEIGHT + 15;
				InGameAccountManager.selectServer.Width = InGameAccountManager.tfInputDataImportAccounts.width;
			}
			InGameAccountManager.GetAccountsArea(out num6, out num5, out num3, out num4, true);
			InGameAccountManager.scrollableMenuAccounts.Width = num3;
			InGameAccountManager.scrollableMenuAccounts.Height = num4;
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000C08A0 File Offset: 0x000BEAA0
		private static void UpdateButtonsPos()
		{
			InGameAccountManager.closeAccountManager.x = GameCanvas.w - 63;
			InGameAccountManager.closeAccountManager.y = (InGameAccountManager.addAccount.y = (InGameAccountManager.moveAccountDown.y = (InGameAccountManager.moveAccountUp.y = 25)));
			int num = GameCanvas.w - InGameAccountManager.currentAccountInfoX;
			if (num > 0)
			{
				InGameAccountManager.closeAccountManager.x -= num - 20;
			}
			InGameAccountManager.addAccount.x = InGameAccountManager.closeAccountManager.x - InGameAccountManager.addAccount.w - 5;
			InGameAccountManager.moveAccountDown.x = InGameAccountManager.addAccount.x - InGameAccountManager.moveAccountUp.w - 5;
			InGameAccountManager.moveAccountUp.x = InGameAccountManager.moveAccountDown.x - InGameAccountManager.moveAccountUp.w - 5;
			InGameAccountManager.editAccount.y = (InGameAccountManager.deleteAccount.y = (InGameAccountManager.selectAccountToLogin.y = GameCanvas.h - 50));
			InGameAccountManager.editAccount.x = InGameAccountManager.currentAccountInfoX;
			InGameAccountManager.deleteAccount.x = InGameAccountManager.currentAccountInfoX + (InGameAccountManager.WIDTH_ACCOUNT_INFO - InGameAccountManager.deleteAccount.w) / 2;
			InGameAccountManager.selectAccountToLogin.x = InGameAccountManager.currentAccountInfoX + InGameAccountManager.WIDTH_ACCOUNT_INFO - InGameAccountManager.selectAccountToLogin.w;
			int num2;
			int num3;
			int num4;
			int num5;
			InGameAccountManager.GetInputAccountArea(out num2, out num3, out num4, out num5, false);
			InGameAccountManager.closeInputAccount.y = (InGameAccountManager.toggleHidePassword.y = (InGameAccountManager.importAccounts.y = num3 + 5));
			InGameAccountManager.closeInputAccount.x = num2 + num4 - InGameAccountManager.closeInputAccount.img.getWidth() - 5;
			InGameAccountManager.toggleHidePassword.x = InGameAccountManager.closeInputAccount.x - InGameAccountManager.toggleHidePassword.img.getWidth() - 5;
			InGameAccountManager.importAccounts.x = num2 + 5;
			InGameAccountManager.finishInputAccount.x = (InGameAccountManager.editCustomServer.x = (InGameAccountManager.finishEditCustomServer.x = num2 + 10));
			InGameAccountManager.cancelInputAccount.x = (InGameAccountManager.cancelEditCustomServer.x = num2 + num4 - InGameAccountManager.cancelInputAccount.w - 10);
			InGameAccountManager.finishInputAccount.y = (InGameAccountManager.cancelInputAccount.y = (InGameAccountManager.editCustomServer.y = (InGameAccountManager.finishEditCustomServer.y = (InGameAccountManager.cancelEditCustomServer.y = num3 + num5 - InGameAccountManager.finishInputAccount.h - 10))));
			InGameAccountManager.finishInputAccount.w = (InGameAccountManager.editCustomServer.w = (InGameAccountManager.cancelInputAccount.w = (InGameAccountManager.finishEditCustomServer.w = (InGameAccountManager.cancelEditCustomServer.w = (num4 - 30) / 2))));
			InGameAccountManager.GetImportAccountsArea(out num2, out num3, out num4, out num5, false);
			InGameAccountManager.closeImportAccounts.y = (InGameAccountManager.helpImportAccounts.y = num3 + 5);
			InGameAccountManager.closeImportAccounts.x = num2 + num4 - InGameAccountManager.closeImportAccounts.img.getWidth() - 5;
			InGameAccountManager.helpImportAccounts.x = num2 + 5;
			InGameAccountManager.finishImportAccounts.x = num2 + 10;
			InGameAccountManager.cancelImportAccounts.x = num2 + num4 - InGameAccountManager.cancelImportAccounts.w - 10;
			InGameAccountManager.finishImportAccounts.y = (InGameAccountManager.cancelImportAccounts.y = num3 + num5 - InGameAccountManager.finishImportAccounts.h - 10);
			InGameAccountManager.finishImportAccounts.w = (InGameAccountManager.cancelImportAccounts.w = (num4 - 30) / 2);
		}

		// Token: 0x060011DF RID: 4575 RVA: 0x000C0C50 File Offset: 0x000BEE50
		internal static void OnStart()
		{
			InGameAccountManager.closeAccountManager.img = (InGameAccountManager.closeInputAccount.img = (InGameAccountManager.closeImportAccounts.img = GameCanvas.loadImage("/mainImage/myTexture2dbtX.png")));
			InGameAccountManager.add = CustomGraphics.Resize(InGameAccountManager.add, InGameAccountManager.add.width * mGraphics.zoomLevel / 4, InGameAccountManager.add.height * mGraphics.zoomLevel / 4);
			InGameAccountManager.hide = CustomGraphics.Resize(InGameAccountManager.hide, InGameAccountManager.hide.width * mGraphics.zoomLevel / 4, InGameAccountManager.hide.height * mGraphics.zoomLevel / 4);
			InGameAccountManager.show = CustomGraphics.Resize(InGameAccountManager.show, InGameAccountManager.show.width * mGraphics.zoomLevel / 4, InGameAccountManager.show.height * mGraphics.zoomLevel / 4);
			InGameAccountManager.up = CustomGraphics.Resize(InGameAccountManager.up, InGameAccountManager.up.width * mGraphics.zoomLevel / 4, InGameAccountManager.up.height * mGraphics.zoomLevel / 4);
			InGameAccountManager.down = CustomGraphics.Resize(InGameAccountManager.down, InGameAccountManager.down.width * mGraphics.zoomLevel / 4, InGameAccountManager.down.height * mGraphics.zoomLevel / 4);
			InGameAccountManager.addMultiple = CustomGraphics.Resize(InGameAccountManager.addMultiple, InGameAccountManager.addMultiple.width * mGraphics.zoomLevel / 4, InGameAccountManager.addMultiple.height * mGraphics.zoomLevel / 4);
			InGameAccountManager.help = CustomGraphics.Resize(InGameAccountManager.help, InGameAccountManager.help.width * mGraphics.zoomLevel / 4, InGameAccountManager.help.height * mGraphics.zoomLevel / 4);
			InGameAccountManager.LoadDataAccounts();
			InGameAccountManager.scrollableMenuAccounts = new ScrollableMenuItems<Account>(InGameAccountManager.accounts)
			{
				PaintItemAction = new Action<mGraphics, int, int, int, int, int>(InGameAccountManager.PaintAccount),
				CurrentItemIndex = -1,
				ItemHeight = InGameAccountManager.ACCOUNT_HEIGHT,
				StepScroll = InGameAccountManager.DEFAULT_STEP_SCROLL,
				AllowSelectNone = true
			};
			InGameAccountManager.addAccount = new Command("", InGameAccountManager.ActionListener.gI(), 2, null)
			{
				img = Image.createImage(InGameAccountManager.add.width, InGameAccountManager.add.height),
				imgFocus = new Image(),
				w = InGameAccountManager.add.width / mGraphics.zoomLevel,
				h = InGameAccountManager.add.height / mGraphics.zoomLevel
			};
			InGameAccountManager.addAccount.img.texture = InGameAccountManager.add;
			InGameAccountManager.toggleHidePassword = new Command("", InGameAccountManager.ActionListener.gI(), 13, null)
			{
				img = Image.createImage(InGameAccountManager.show.width, InGameAccountManager.show.height),
				imgFocus = new Image(),
				w = InGameAccountManager.show.width / mGraphics.zoomLevel,
				h = InGameAccountManager.show.height / mGraphics.zoomLevel
			};
			InGameAccountManager.toggleHidePassword.img.texture = InGameAccountManager.show;
			InGameAccountManager.importAccounts = new Command("", InGameAccountManager.ActionListener.gI(), 16, null)
			{
				img = Image.createImage(InGameAccountManager.addMultiple.width, InGameAccountManager.addMultiple.height),
				imgFocus = new Image(),
				w = InGameAccountManager.addMultiple.width / mGraphics.zoomLevel,
				h = InGameAccountManager.addMultiple.height / mGraphics.zoomLevel
			};
			InGameAccountManager.importAccounts.img.texture = InGameAccountManager.addMultiple;
			InGameAccountManager.helpImportAccounts = new Command("", InGameAccountManager.ActionListener.gI(), 17, null)
			{
				img = Image.createImage(InGameAccountManager.help.width, InGameAccountManager.help.height),
				imgFocus = new Image(),
				w = InGameAccountManager.help.width / mGraphics.zoomLevel,
				h = InGameAccountManager.help.height / mGraphics.zoomLevel
			};
			InGameAccountManager.helpImportAccounts.img.texture = InGameAccountManager.help;
			InGameAccountManager.moveAccountUp = new Command("", InGameAccountManager.ActionListener.gI(), 14, null)
			{
				img = Image.createImage(InGameAccountManager.up.width, InGameAccountManager.up.height),
				imgFocus = new Image(),
				w = InGameAccountManager.up.width / mGraphics.zoomLevel,
				h = InGameAccountManager.up.height / mGraphics.zoomLevel
			};
			InGameAccountManager.moveAccountUp.img.texture = InGameAccountManager.up;
			InGameAccountManager.moveAccountDown = new Command("", InGameAccountManager.ActionListener.gI(), 15, null)
			{
				img = Image.createImage(InGameAccountManager.down.width, InGameAccountManager.down.height),
				imgFocus = new Image(),
				w = InGameAccountManager.down.width / mGraphics.zoomLevel,
				h = InGameAccountManager.down.height / mGraphics.zoomLevel
			};
			InGameAccountManager.moveAccountDown.img.texture = InGameAccountManager.down;
		}

		// Token: 0x060011E0 RID: 4576 RVA: 0x000C113F File Offset: 0x000BF33F
		internal static void OnCloseAndPause()
		{
			InGameAccountManager.SaveDataAccounts();
		}

		// Token: 0x060011E1 RID: 4577 RVA: 0x000C1148 File Offset: 0x000BF348
		private static void PaintAccount(mGraphics g, int i, int x, int y, int width, int height)
		{
			Account account = InGameAccountManager.accounts[i];
			InGameAccountManager.EnsureSmallImage(account.Info.Icon);
			Texture2D texture2D = InGameAccountManager.icons[account.Info.Icon];
			if (texture2D != null)
			{
				float num = (float)((height - 2) * texture2D.width) / (float)texture2D.height;
				InGameAccountManager.BeginGroup(InGameAccountManager.scrollableMenuAccounts.X, InGameAccountManager.scrollableMenuAccounts.Y, InGameAccountManager.scrollableMenuAccounts.Width, InGameAccountManager.scrollableMenuAccounts.Height);
				InGameAccountManager.DrawTexture(10f, (float)(i * height + 2 - InGameAccountManager.scrollableMenuAccounts.CurrentOffset), num, (float)(height - 2), texture2D, ScaleMode.ScaleToFit);
				GUI.EndGroup();
			}
			if (account.PetInfo != null)
			{
				InGameAccountManager.EnsureSmallImage(account.PetInfo.Icon);
				Texture2D texture2D2 = InGameAccountManager.icons[account.PetInfo.Icon];
				if (texture2D2 != null)
				{
					float num2 = (float)height / 2f * (float)texture2D2.width / (float)texture2D2.height;
					InGameAccountManager.BeginGroup(InGameAccountManager.scrollableMenuAccounts.X, InGameAccountManager.scrollableMenuAccounts.Y, InGameAccountManager.scrollableMenuAccounts.Width, InGameAccountManager.scrollableMenuAccounts.Height);
					InGameAccountManager.DrawTexture(45f, (float)(i * height - InGameAccountManager.scrollableMenuAccounts.CurrentOffset) + (float)height / 2f, num2, (float)height / 2f, texture2D2, ScaleMode.ScaleToFit);
					GUI.EndGroup();
				}
			}
			Texture2D overlay = InGameAccountManager.GetOverlay(account.Info.Gender);
			if (overlay != null)
			{
				float num3 = (float)((height - 1) * overlay.width) / (float)overlay.height;
				InGameAccountManager.BeginGroup(InGameAccountManager.scrollableMenuAccounts.X, InGameAccountManager.scrollableMenuAccounts.Y, InGameAccountManager.scrollableMenuAccounts.Width, InGameAccountManager.scrollableMenuAccounts.Height);
				InGameAccountManager.DrawTexture((float)width - num3, (float)(i * height - InGameAccountManager.scrollableMenuAccounts.CurrentOffset + 1), num3, (float)(height - 1), overlay, ScaleMode.ScaleToFit);
				GUI.EndGroup();
			}
			mFont mFont = mFont.tahoma_7b_dark;
			if (account.Type == AccountType.Unregistered)
			{
				mFont = mFont.tahoma_7b_green2;
			}
			mFont.drawString(g, account.Info.Name, x + 80, y + 2, 0);
			string text = (account.Server.IsCustomIP() ? account.Server.name : ServerListScreen.nameServer[account.Server.index]);
			mFont.tahoma_7_greySmall.drawString(g, account.GetLastTimeLogin(), x + 80, y + height - 12, 0);
			mFont.tahoma_7_greySmall.drawString(g, mResources.server + " " + text, x + 80, y + height - 22, 0);
		}

		// Token: 0x060011E2 RID: 4578 RVA: 0x000C13E8 File Offset: 0x000BF5E8
		private static void EnsureSmallImage(int id)
		{
			Texture2D texture2D = null;
			if (!InGameAccountManager.icons.ContainsKey(id) || InGameAccountManager.icons[id] == null)
			{
				texture2D = Resources.Load(string.Format("{0}/x{1}/SmallImage/Small{2}", Main.res, mGraphics.zoomLevel, id)) as Texture2D;
				if (texture2D == null)
				{
					sbyte[] array = Rms.loadRMS(mGraphics.zoomLevel.ToString() + "Small" + id.ToString());
					if (array != null)
					{
						texture2D = new Texture2D(1, 1);
						texture2D.LoadImage(ArrayCast.cast(array));
					}
				}
				if (texture2D != null)
				{
					texture2D.filterMode = FilterMode.Bilinear;
					texture2D.wrapMode = TextureWrapMode.Clamp;
					texture2D.anisoLevel = 1;
				}
			}
			if (!InGameAccountManager.icons.ContainsKey(id))
			{
				InGameAccountManager.icons.Add(id, texture2D);
				return;
			}
			if (InGameAccountManager.icons[id] == null)
			{
				InGameAccountManager.icons[id] = texture2D;
			}
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x000C14DD File Offset: 0x000BF6DD
		private static Texture2D GetOverlay(sbyte gender)
		{
			switch (gender)
			{
			case 0:
				return InGameAccountManager.earthOverlay;
			case 1:
				return InGameAccountManager.namekOverlay;
			case 2:
				return InGameAccountManager.saiyanOverlay;
			default:
				return null;
			}
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x000C1508 File Offset: 0x000BF708
		private static void GetAccountsArea(out int x, out int y, out int width, out int height, bool withoutTitle)
		{
			x = InGameAccountManager.X_LIST_ACCOUNTS;
			y = InGameAccountManager.Y_LIST_ACCOUNTS;
			width = GameCanvas.w - x * 2;
			height = GameCanvas.h - y * 2;
			if (withoutTitle)
			{
				y += InGameAccountManager.TITLE_HEIGHT;
				height -= InGameAccountManager.TITLE_HEIGHT;
			}
			int num = GameCanvas.w - InGameAccountManager.currentAccountInfoX;
			if (num > 0)
			{
				width -= num - 20;
			}
		}

		// Token: 0x060011E5 RID: 4581 RVA: 0x000C156C File Offset: 0x000BF76C
		private static void GetInputAccountArea(out int x, out int y, out int width, out int height, bool withoutTitle)
		{
			x = GameCanvas.w / 2 - InGameAccountManager.INPUT_ACCOUNT_WIDTH / 2;
			y = GameCanvas.h / 2 - InGameAccountManager.INPUT_ACCOUNT_HEIGHT / 2;
			width = InGameAccountManager.INPUT_ACCOUNT_WIDTH;
			height = InGameAccountManager.INPUT_ACCOUNT_HEIGHT;
			if (withoutTitle)
			{
				y += InGameAccountManager.TITLE_HEIGHT;
				height -= InGameAccountManager.TITLE_HEIGHT;
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x000C15C4 File Offset: 0x000BF7C4
		private static void GetImportAccountsArea(out int x, out int y, out int width, out int height, bool withoutTitle)
		{
			x = GameCanvas.w / 2 - InGameAccountManager.IMPORT_ACCOUNTS_WIDTH / 2;
			y = GameCanvas.h / 2 - InGameAccountManager.IMPORT_ACCOUNTS_HEIGHT / 2;
			width = InGameAccountManager.IMPORT_ACCOUNTS_WIDTH;
			height = InGameAccountManager.IMPORT_ACCOUNTS_HEIGHT;
			if (withoutTitle)
			{
				y += InGameAccountManager.TITLE_HEIGHT;
				height -= InGameAccountManager.TITLE_HEIGHT;
			}
		}

		// Token: 0x060011E7 RID: 4583 RVA: 0x000B283B File Offset: 0x000B0A3B
		private static bool IsPointerIn(int x, int y, int w, int h)
		{
			return GameCanvas.pxMouse >= x && GameCanvas.pxMouse <= x + w && GameCanvas.pyMouse >= y && GameCanvas.pyMouse <= y + h;
		}

		// Token: 0x060011E8 RID: 4584 RVA: 0x000C1619 File Offset: 0x000BF819
		private static void BeginGroup(int x, int y, int w, int h)
		{
			GUI.BeginGroup(new Rect((float)(x * mGraphics.zoomLevel), (float)(y * mGraphics.zoomLevel), (float)(w * mGraphics.zoomLevel), (float)(h * mGraphics.zoomLevel)));
		}

		// Token: 0x060011E9 RID: 4585 RVA: 0x000C1645 File Offset: 0x000BF845
		private static void DrawTexture(float x, float y, float w, float h, Texture texture, ScaleMode scaleMode = ScaleMode.StretchToFill)
		{
			GUI.DrawTexture(new Rect(x * (float)mGraphics.zoomLevel, y * (float)mGraphics.zoomLevel, w * (float)mGraphics.zoomLevel, h * (float)mGraphics.zoomLevel), texture, scaleMode);
		}

		// Token: 0x0400190A RID: 6410
		private static Regex regexMatchUserAo = new Regex("^User[0-9]{1,}$", RegexOptions.Compiled);

		// Token: 0x0400190B RID: 6411
		private static Texture2D earthOverlay = Resources.Load<Texture2D>("InGameAccountManager/img/earthOverlay");

		// Token: 0x0400190C RID: 6412
		private static Texture2D namekOverlay = Resources.Load<Texture2D>("InGameAccountManager/img/namekOverlay");

		// Token: 0x0400190D RID: 6413
		private static Texture2D saiyanOverlay = Resources.Load<Texture2D>("InGameAccountManager/img/saiyanOverlay");

		// Token: 0x0400190E RID: 6414
		private static Texture2D add = Resources.Load<Texture2D>("InGameAccountManager/img/add");

		// Token: 0x0400190F RID: 6415
		private static Texture2D addMultiple = Resources.Load<Texture2D>("InGameAccountManager/img/addMultiple");

		// Token: 0x04001910 RID: 6416
		private static Texture2D hide = Resources.Load<Texture2D>("InGameAccountManager/img/hide");

		// Token: 0x04001911 RID: 6417
		private static Texture2D show = Resources.Load<Texture2D>("InGameAccountManager/img/show");

		// Token: 0x04001912 RID: 6418
		private static Texture2D up = Resources.Load<Texture2D>("InGameAccountManager/img/up");

		// Token: 0x04001913 RID: 6419
		private static Texture2D down = Resources.Load<Texture2D>("InGameAccountManager/img/down");

		// Token: 0x04001914 RID: 6420
		private static Texture2D help = Resources.Load<Texture2D>("InGameAccountManager/img/help");

		// Token: 0x04001915 RID: 6421
		private static Dictionary<int, Texture2D> icons = new Dictionary<int, Texture2D>();

		// Token: 0x04001916 RID: 6422
		private static Command closeAccountManager = new Command("", InGameAccountManager.ActionListener.gI(), 1, null)
		{
			imgFocus = new Image()
		};

		// Token: 0x04001917 RID: 6423
		private static Command selectAccountToLogin;

		// Token: 0x04001918 RID: 6424
		private static Command addAccount;

		// Token: 0x04001919 RID: 6425
		private static Command editAccount;

		// Token: 0x0400191A RID: 6426
		private static Command deleteAccount;

		// Token: 0x0400191B RID: 6427
		private static Command finishInputAccount;

		// Token: 0x0400191C RID: 6428
		private static Command editCustomServer;

		// Token: 0x0400191D RID: 6429
		private static Command finishEditCustomServer;

		// Token: 0x0400191E RID: 6430
		private static Command cancelEditCustomServer;

		// Token: 0x0400191F RID: 6431
		private static Command closeInputAccount = new Command("", InGameAccountManager.ActionListener.gI(), 9, null)
		{
			imgFocus = new Image()
		};

		// Token: 0x04001920 RID: 6432
		private static Command cancelInputAccount;

		// Token: 0x04001921 RID: 6433
		private static Command toggleHidePassword;

		// Token: 0x04001922 RID: 6434
		private static Command importAccounts;

		// Token: 0x04001923 RID: 6435
		private static Command moveAccountUp;

		// Token: 0x04001924 RID: 6436
		private static Command moveAccountDown;

		// Token: 0x04001925 RID: 6437
		private static Command helpImportAccounts;

		// Token: 0x04001926 RID: 6438
		private static Command finishImportAccounts;

		// Token: 0x04001927 RID: 6439
		private static Command cancelImportAccounts;

		// Token: 0x04001928 RID: 6440
		private static Command closeImportAccounts = new Command("", InGameAccountManager.ActionListener.gI(), 19, null)
		{
			imgFocus = new Image()
		};

		// Token: 0x04001929 RID: 6441
		private static ComboBox selectServer;

		// Token: 0x0400192A RID: 6442
		private static List<Account> accounts = new List<Account>();

		// Token: 0x0400192B RID: 6443
		private static ScrollableMenuItems<Account> scrollableMenuAccounts;

		// Token: 0x0400192C RID: 6444
		private static TField tfUser;

		// Token: 0x0400192D RID: 6445
		private static TField tfPass;

		// Token: 0x0400192E RID: 6446
		private static TField tfCustomServerName;

		// Token: 0x0400192F RID: 6447
		private static TField tfCustomServerAddress;

		// Token: 0x04001930 RID: 6448
		private static TField tfCustomServerPort;

		// Token: 0x04001931 RID: 6449
		private static TField tfInputDataImportAccounts;

		// Token: 0x04001932 RID: 6450
		private static TField tfInputRegexMatchLinesImportAccounts;

		// Token: 0x04001933 RID: 6451
		private static TField tfInputRegexMatchAccountInfoImportAccounts;

		// Token: 0x04001935 RID: 6453
		private static Server[] defaultServers;

		// Token: 0x04001936 RID: 6454
		private static Server customServer;

		// Token: 0x04001937 RID: 6455
		private static int selectedAccountIndex = -1;

		// Token: 0x04001938 RID: 6456
		private static int currentAccountInfoX;

		// Token: 0x04001939 RID: 6457
		private static bool isSwitchedToMe;

		// Token: 0x0400193A RID: 6458
		private static bool isAddingAccount;

		// Token: 0x0400193B RID: 6459
		private static bool isEditingAccount;

		// Token: 0x0400193C RID: 6460
		private static bool isEditingCustomServer;

		// Token: 0x0400193D RID: 6461
		private static bool isImportingAccounts;

		// Token: 0x0400193E RID: 6462
		private static readonly int TITLE_HEIGHT = 30;

		// Token: 0x0400193F RID: 6463
		private static readonly int ACCOUNT_HEIGHT = 34;

		// Token: 0x04001940 RID: 6464
		private static readonly int X_LIST_ACCOUNTS = 50;

		// Token: 0x04001941 RID: 6465
		private static readonly int Y_LIST_ACCOUNTS = 30;

		// Token: 0x04001942 RID: 6466
		private static readonly int DEFAULT_STEP_SCROLL = 70;

		// Token: 0x04001943 RID: 6467
		private static readonly int WIDTH_ACCOUNT_INFO = 160;

		// Token: 0x04001944 RID: 6468
		private static readonly int INPUT_ACCOUNT_WIDTH = 200;

		// Token: 0x04001945 RID: 6469
		private static readonly int INPUT_ACCOUNT_HEIGHT = 180;

		// Token: 0x04001946 RID: 6470
		private static readonly int SELECT_SERVER_WIDTH = 500;

		// Token: 0x04001947 RID: 6471
		private static readonly int SELECT_SERVER_HEIGHT = 300;

		// Token: 0x04001948 RID: 6472
		private static readonly int IMPORT_ACCOUNTS_WIDTH = 250;

		// Token: 0x04001949 RID: 6473
		private static readonly int IMPORT_ACCOUNTS_HEIGHT = 215;

		// Token: 0x0400194A RID: 6474
		private static InGameAccountManager instance;

		// Token: 0x0200018F RID: 399
		private enum CommandType
		{
			// Token: 0x0400194C RID: 6476
			CloseAccountManager = 1,
			// Token: 0x0400194D RID: 6477
			AddAccount,
			// Token: 0x0400194E RID: 6478
			EditAccount,
			// Token: 0x0400194F RID: 6479
			ConfirmDeleteAccount,
			// Token: 0x04001950 RID: 6480
			DeleteAccount,
			// Token: 0x04001951 RID: 6481
			SelectAccountToLogin,
			// Token: 0x04001952 RID: 6482
			OpenAccountManager,
			// Token: 0x04001953 RID: 6483
			FinishInputAccount,
			// Token: 0x04001954 RID: 6484
			CloseInputAccount,
			// Token: 0x04001955 RID: 6485
			EditCustomServer,
			// Token: 0x04001956 RID: 6486
			FinishEditCustomServer,
			// Token: 0x04001957 RID: 6487
			CancelEditCustomServer,
			// Token: 0x04001958 RID: 6488
			ToggleHidePassword,
			// Token: 0x04001959 RID: 6489
			MoveAccountUp,
			// Token: 0x0400195A RID: 6490
			MoveAccountDown,
			// Token: 0x0400195B RID: 6491
			ImportAccounts,
			// Token: 0x0400195C RID: 6492
			HelpImportAccounts,
			// Token: 0x0400195D RID: 6493
			FinishImportAccounts,
			// Token: 0x0400195E RID: 6494
			CloseImportAccounts
		}

		// Token: 0x02000190 RID: 400
		internal class ActionListener : IActionListener
		{
			// Token: 0x060011EC RID: 4588 RVA: 0x000C181B File Offset: 0x000BFA1B
			internal static InGameAccountManager.ActionListener gI()
			{
				if (InGameAccountManager.ActionListener.instance == null)
				{
					InGameAccountManager.ActionListener.instance = new InGameAccountManager.ActionListener();
				}
				return InGameAccountManager.ActionListener.instance;
			}

			// Token: 0x060011ED RID: 4589 RVA: 0x000C1834 File Offset: 0x000BFA34
			public void perform(int id, object obj)
			{
				switch (id)
				{
				case 1:
					GameCanvas.serverScreen.switchToMe();
					return;
				case 2:
					InGameAccountManager.isAddingAccount = true;
					InGameAccountManager.tfUser.isFocus = false;
					InGameAccountManager.tfPass.isFocus = false;
					InGameAccountManager.tfUser.setText("");
					InGameAccountManager.tfPass.setText("");
					InGameAccountManager.tfPass.setIputType(TField.INPUT_TYPE_PASSWORD);
					InGameAccountManager.toggleHidePassword.img.texture = InGameAccountManager.show;
					return;
				case 3:
				{
					InGameAccountManager.isEditingAccount = true;
					InGameAccountManager.tfUser.isFocus = false;
					InGameAccountManager.tfPass.isFocus = false;
					InGameAccountManager.tfUser.setText(InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex].Username);
					InGameAccountManager.tfPass.setText(InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex].Password);
					InGameAccountManager.tfPass.setIputType(TField.INPUT_TYPE_PASSWORD);
					InGameAccountManager.toggleHidePassword.img.texture = InGameAccountManager.show;
					Server server = InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex].Server;
					if (server.IsCustomIP())
					{
						InGameAccountManager.customServer = server;
						InGameAccountManager.selectServer.SelectedIndex = InGameAccountManager.selectServer.Items.Count - 1;
						return;
					}
					InGameAccountManager.selectServer.SelectedIndex = server.index;
					return;
				}
				case 4:
					GameCanvas.startYesNoDlg(Strings.inGameAccountManagerConfirmDeleteAcc, new Command(mResources.YES, InGameAccountManager.ActionListener.gI(), 5, null), new Command(mResources.NO, 2001));
					return;
				case 5:
					InGameAccountManager.accounts.RemoveAt(InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex);
					if (InGameAccountManager.accounts.Count == 0)
					{
						InGameAccountManager.selectedAccountIndex = (InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex = -1);
					}
					else if (InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex > InGameAccountManager.accounts.Count - 1)
					{
						if (InGameAccountManager.selectedAccountIndex == InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex)
						{
							InGameAccountManager.selectedAccountIndex = InGameAccountManager.accounts.Count - 1;
						}
						InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex = InGameAccountManager.accounts.Count - 1;
					}
					InfoDlg.hide();
					GameCanvas.currentDialog = null;
					InGameAccountManager.SaveDataAccounts();
					return;
				case 6:
					InGameAccountManager.selectedAccountIndex = InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex;
					InGameAccountManager.SaveDataAccounts();
					InGameAccountManager.SelectedServer = InGameAccountManager.SelectedAccount.Server;
					Rms.saveRMSString("acc", "acc");
					Rms.saveRMSString("pass", "pass");
					Session_ME.gI().close();
					Session_ME2.gI().close();
					GameCanvas.connect();
					GameCanvas.serverScreen.switchToMe();
					return;
				case 7:
					InGameAccountManager.gI().switchToMe();
					return;
				case 8:
					break;
				case 9:
					InGameAccountManager.isAddingAccount = (InGameAccountManager.isEditingAccount = false);
					return;
				case 10:
					if (InGameAccountManager.isEditingAccount && InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex].Server.IsCustomIP())
					{
						InGameAccountManager.tfCustomServerName.setText(InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex].Server.name);
						InGameAccountManager.tfCustomServerAddress.setText(InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex].Server.hostnameOrIPAddress);
						InGameAccountManager.tfCustomServerPort.setText(InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex].Server.port.ToString());
					}
					InGameAccountManager.isEditingCustomServer = true;
					return;
				case 11:
				{
					if (string.IsNullOrEmpty(InGameAccountManager.tfCustomServerName.getText()))
					{
						GameCanvas.startOKDlg(Strings.inGameAccountManagerServerNameBlank + "!");
						return;
					}
					if (string.IsNullOrEmpty(InGameAccountManager.tfCustomServerAddress.getText()))
					{
						GameCanvas.startOKDlg(Strings.inGameAccountManagerServerAddressBlank + "!");
						return;
					}
					if (string.IsNullOrEmpty(InGameAccountManager.tfCustomServerPort.getText()))
					{
						GameCanvas.startOKDlg(Strings.inGameAccountManagerServerPortBlank + "!");
						return;
					}
					int num;
					if (!int.TryParse(InGameAccountManager.tfCustomServerPort.getText(), out num))
					{
						GameCanvas.startOKDlg(Strings.inGameAccountManagerServerPortInvalid + "!");
						return;
					}
					if (num < 0 || num > 65535)
					{
						GameCanvas.startOKDlg(string.Format(Strings.inputNumberOutOfRange, 0, ushort.MaxValue) + "!");
						return;
					}
					InGameAccountManager.customServer = new Server(InGameAccountManager.tfCustomServerName.getText(), InGameAccountManager.tfCustomServerAddress.getText(), (ushort)num);
					InGameAccountManager.isEditingCustomServer = false;
					break;
				}
				case 12:
					InGameAccountManager.isEditingCustomServer = false;
					return;
				case 13:
					InGameAccountManager.tfPass.setIputType((InGameAccountManager.tfPass.inputType == TField.INPUT_TYPE_PASSWORD) ? TField.INPUT_TYPE_ANY : TField.INPUT_TYPE_PASSWORD);
					InGameAccountManager.toggleHidePassword.img.texture = ((InGameAccountManager.tfPass.inputType == TField.INPUT_TYPE_PASSWORD) ? InGameAccountManager.show : InGameAccountManager.hide);
					return;
				case 14:
					if (InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex > 0)
					{
						Account account = InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex];
						InGameAccountManager.accounts.RemoveAt(InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex);
						InGameAccountManager.accounts.Insert(InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex - 1, account);
						ScrollableMenuItems<Account> scrollableMenuAccounts = InGameAccountManager.scrollableMenuAccounts;
						int num2 = scrollableMenuAccounts.CurrentItemIndex;
						scrollableMenuAccounts.CurrentItemIndex = num2 - 1;
						InGameAccountManager.SaveDataAccounts();
						return;
					}
					return;
				case 15:
					if (InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex < InGameAccountManager.accounts.Count - 1)
					{
						Account account2 = InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex];
						InGameAccountManager.accounts.RemoveAt(InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex);
						InGameAccountManager.accounts.Insert(InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex + 1, account2);
						ScrollableMenuItems<Account> scrollableMenuAccounts2 = InGameAccountManager.scrollableMenuAccounts;
						int num2 = scrollableMenuAccounts2.CurrentItemIndex;
						scrollableMenuAccounts2.CurrentItemIndex = num2 + 1;
						InGameAccountManager.SaveDataAccounts();
						return;
					}
					return;
				case 16:
					InGameAccountManager.selectServer.X = InGameAccountManager.tfInputDataImportAccounts.x;
					InGameAccountManager.selectServer.Y = InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.y + mScreen.ITEM_HEIGHT + 15;
					InGameAccountManager.selectServer.Width = InGameAccountManager.tfInputDataImportAccounts.width;
					InGameAccountManager.isImportingAccounts = true;
					return;
				case 17:
					GameCanvas.startOKDlg(Strings.inGameAccountManagerImportAccountsHelp);
					return;
				case 18:
				{
					string data = InGameAccountManager.tfInputDataImportAccounts.getText();
					if (string.IsNullOrWhiteSpace(data))
					{
						GameCanvas.startOKDlg(Strings.inGameAccountManagerImportAccountsInputDataBlank + "!");
						return;
					}
					string text = InGameAccountManager.tfInputRegexMatchLinesImportAccounts.getText();
					if (string.IsNullOrWhiteSpace(text))
					{
						GameCanvas.startOKDlg(Strings.inGameAccountManagerRegexMatchLinesBlank + "!");
						return;
					}
					Regex linesRegex = new Regex(text);
					string accountInfoPattern = InGameAccountManager.tfInputRegexMatchAccountInfoImportAccounts.getText();
					if (string.IsNullOrWhiteSpace(accountInfoPattern))
					{
						GameCanvas.startOKDlg(Strings.inGameAccountManagerRegexMatchAccountInfoBlank + "!");
						return;
					}
					InfoDlg.showWait();
					new Thread(delegate
					{
						Regex regex = new Regex(accountInfoPattern);
						int num7 = 0;
						int num8 = 0;
						for (;;)
						{
							Match match = linesRegex.Match(data);
							if (match == Match.Empty && string.IsNullOrWhiteSpace(data))
							{
								break;
							}
							Match match2;
							if (match == Match.Empty)
							{
								match2 = regex.Match(data);
							}
							else
							{
								match2 = regex.Match(match.Groups[1].Value);
							}
							if (match2 != Match.Empty)
							{
								try
								{
									Account account5 = new Account
									{
										Username = match2.Groups[1].Value,
										Password = match2.Groups[2].Value
									};
									try
									{
										int num9 = int.Parse(match2.Groups[3].Value);
										if (num9 < 0 || num9 >= InGameAccountManager.defaultServers.Length)
										{
											throw new IndexOutOfRangeException();
										}
										account5.Server = new Server(num9);
									}
									catch
									{
										account5.Server = InGameAccountManager.defaultServers[InGameAccountManager.selectServer.SelectedIndex];
									}
									if (InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex == -1)
									{
										InGameAccountManager.accounts.Add(account5);
									}
									else
									{
										InGameAccountManager.accounts.Insert(InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex + 1, account5);
									}
									if (match == Match.Empty)
									{
										data = data.Substring(match2.Index + match2.Length);
									}
									else
									{
										data = data.Substring(match.Index + match.Length);
									}
									num7++;
									continue;
								}
								catch
								{
									num8++;
									data = data.Substring(match.Index + match.Length);
									continue;
								}
								break;
							}
							num8++;
							data = data.Substring(match.Index + match.Length);
						}
						InfoDlg.hide();
						GameCanvas.startOKDlg(string.Format(Strings.inGameAccountManagerImportAccountsResult, new object[]
						{
							num7,
							(float)num7 * 100f / (float)(num7 + num8),
							num8,
							(float)num8 * 100f / (float)(num7 + num8)
						}) + "!");
						int num10;
						int num11;
						int num12;
						int num13;
						InGameAccountManager.GetInputAccountArea(out num10, out num11, out num12, out num13, true);
						InGameAccountManager.selectServer.X = num10 + 10;
						InGameAccountManager.selectServer.Y = (InGameAccountManager.tfCustomServerPort.y = num11 + 15 + mScreen.ITEM_HEIGHT + 15 + mScreen.ITEM_HEIGHT + 15);
						InGameAccountManager.selectServer.Width = num12 - 20;
						InGameAccountManager.isImportingAccounts = false;
					})
					{
						IsBackground = true
					}.Start();
					return;
				}
				case 19:
				{
					int num3;
					int num4;
					int num5;
					int num6;
					InGameAccountManager.GetInputAccountArea(out num3, out num4, out num5, out num6, true);
					InGameAccountManager.selectServer.X = num3 + 10;
					InGameAccountManager.selectServer.Y = (InGameAccountManager.tfCustomServerPort.y = num4 + 15 + mScreen.ITEM_HEIGHT + 15 + mScreen.ITEM_HEIGHT + 15);
					InGameAccountManager.selectServer.Width = num5 - 20;
					InGameAccountManager.isImportingAccounts = false;
					return;
				}
				default:
					return;
				}
				if (string.IsNullOrEmpty(InGameAccountManager.tfUser.getText()))
				{
					GameCanvas.startOKDlg(mResources.userBlank.TrimEnd('.') + "!");
					return;
				}
				bool flag = InGameAccountManager.regexMatchUserAo.IsMatch(InGameAccountManager.tfUser.getText());
				if (string.IsNullOrEmpty(InGameAccountManager.tfPass.getText()) && !flag)
				{
					GameCanvas.startOKDlg(mResources.passwordBlank + "!");
					return;
				}
				if (InGameAccountManager.selectServer.SelectedIndex == -1)
				{
					GameCanvas.startOKDlg(Strings.inGameAccountManagerServerBlank + "!");
					return;
				}
				if (flag && InGameAccountManager.selectServer.SelectedIndex == InGameAccountManager.selectServer.Items.Count - 1)
				{
					GameCanvas.startOKDlg(Strings.inGameAccountManagerUnregisteredAccountMustBeOnTeaMobiServer + "!");
					return;
				}
				if (InGameAccountManager.isAddingAccount)
				{
					Account account3 = new Account
					{
						Username = InGameAccountManager.tfUser.getText(),
						Password = InGameAccountManager.tfPass.getText(),
						Server = InGameAccountManager.customServer
					};
					if (InGameAccountManager.selectServer.SelectedIndex != InGameAccountManager.selectServer.Items.Count - 1)
					{
						account3.Server = InGameAccountManager.defaultServers[InGameAccountManager.selectServer.SelectedIndex];
					}
					if (InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex == -1)
					{
						InGameAccountManager.accounts.Add(account3);
					}
					else
					{
						InGameAccountManager.accounts.Insert(InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex + 1, account3);
					}
				}
				else if (InGameAccountManager.isEditingAccount)
				{
					Account account4 = InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex];
					string text2 = InGameAccountManager.tfUser.getText();
					string text3 = InGameAccountManager.tfPass.getText();
					if (!(account4.Username == text2) || !(account4.Password == text3) || ((account4.Server.IsCustomIP() || account4.Server.index != InGameAccountManager.selectServer.SelectedIndex) && (!account4.Server.IsCustomIP() || !(InGameAccountManager.customServer != null) || !(account4.Server == InGameAccountManager.customServer))))
					{
						InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex] = new Account
						{
							Username = text2,
							Password = text3,
							Server = InGameAccountManager.customServer
						};
						if (InGameAccountManager.selectServer.SelectedIndex != InGameAccountManager.selectServer.Items.Count - 1)
						{
							InGameAccountManager.accounts[InGameAccountManager.scrollableMenuAccounts.CurrentItemIndex].Server = InGameAccountManager.defaultServers[InGameAccountManager.selectServer.SelectedIndex];
						}
					}
				}
				InGameAccountManager.isAddingAccount = (InGameAccountManager.isEditingAccount = false);
				InGameAccountManager.SaveDataAccounts();
			}

			// Token: 0x0400195F RID: 6495
			private static InGameAccountManager.ActionListener instance;
		}
	}
}
