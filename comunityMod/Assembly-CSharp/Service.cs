using System;
using Assets.src.g;

// Token: 0x02000099 RID: 153
public class Service
{
	// Token: 0x06000801 RID: 2049 RVA: 0x0007B55F File Offset: 0x0007975F
	public static Service gI()
	{
		if (Service.instance == null)
		{
			Service.instance = new Service();
		}
		return Service.instance;
	}

	// Token: 0x06000802 RID: 2050 RVA: 0x0007B578 File Offset: 0x00079778
	public void gotoPlayer(int id)
	{
		Message message = null;
		try
		{
			message = new Message(18);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000803 RID: 2051 RVA: 0x0007B5DC File Offset: 0x000797DC
	public void androidPack()
	{
		if (mSystem.android_pack == null)
		{
			return;
		}
		Message message = null;
		try
		{
			message = new Message(126);
			message.writer().writeUTF(mSystem.android_pack);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000804 RID: 2052 RVA: 0x0007B64C File Offset: 0x0007984C
	public void charInfo(string day, string month, string year, string address, string cmnd, string dayCmnd, string noiCapCmnd, string sdt, string name)
	{
		Message message = null;
		try
		{
			message = new Message(42);
			message.writer().writeUTF(day);
			message.writer().writeUTF(month);
			message.writer().writeUTF(year);
			message.writer().writeUTF(address);
			message.writer().writeUTF(cmnd);
			message.writer().writeUTF(dayCmnd);
			message.writer().writeUTF(noiCapCmnd);
			message.writer().writeUTF(sdt);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000805 RID: 2053 RVA: 0x0007B714 File Offset: 0x00079914
	public void androidPack2()
	{
		if (mSystem.android_pack == null)
		{
			return;
		}
		Message message = null;
		try
		{
			message = new Message(126);
			message.writer().writeUTF(mSystem.android_pack);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000806 RID: 2054 RVA: 0x0007B7B8 File Offset: 0x000799B8
	public void checkAd(sbyte status)
	{
		Message message = null;
		try
		{
			message = new Message(-44);
			message.writer().writeByte(status);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000807 RID: 2055 RVA: 0x0007B81C File Offset: 0x00079A1C
	public void combine(sbyte action, MyVector id)
	{
		Res.outz("combine");
		Message message = null;
		try
		{
			message = new Message(-81);
			message.writer().writeByte(action);
			if (action == 1)
			{
				message.writer().writeByte(id.size());
				for (int i = 0; i < id.size(); i++)
				{
					message.writer().writeByte(((Item)id.elementAt(i)).indexUI);
					Res.outz("gui id " + ((Item)id.elementAt(i)).indexUI.ToString());
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000808 RID: 2056 RVA: 0x0007B8E4 File Offset: 0x00079AE4
	public void giaodich(sbyte action, int playerID, sbyte index, int num)
	{
		Res.outz2("giao dich action = " + action.ToString());
		Message message = null;
		try
		{
			message = new Message(-86);
			message.writer().writeByte(action);
			if (action == 0 || action == 1)
			{
				Res.outz2(">>>> len playerID =" + playerID.ToString());
				message.writer().writeInt(playerID);
			}
			if (action == 2)
			{
				Res.outz2("gui len index =" + index.ToString() + " num= " + num.ToString());
				message.writer().writeByte(index);
				message.writer().writeInt(num);
			}
			if (action == 4)
			{
				Res.outz2(">>>> len index =" + index.ToString());
				message.writer().writeByte(index);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000809 RID: 2057 RVA: 0x0007B9E0 File Offset: 0x00079BE0
	public void sendClientInput(TField[] t)
	{
		Message message = null;
		try
		{
			Res.outz(" gui input ");
			message = new Message(-125);
			Res.outz("byte lent = " + t.Length.ToString());
			message.writer().writeByte(t.Length);
			for (int i = 0; i < t.Length; i++)
			{
				message.writer().writeUTF(t[i].getText());
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600080A RID: 2058 RVA: 0x0007BA80 File Offset: 0x00079C80
	public void speacialSkill(sbyte index)
	{
		Message message = null;
		try
		{
			message = new Message(112);
			message.writer().writeByte(index);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600080B RID: 2059 RVA: 0x0007BAE4 File Offset: 0x00079CE4
	public void test(short x, short y)
	{
		Res.outz("gui x= " + x.ToString() + " y= " + y.ToString());
		Message message = null;
		try
		{
			message = new Message(0);
			message.writer().writeShort(x);
			message.writer().writeShort(y);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600080C RID: 2060 RVA: 0x0007BB74 File Offset: 0x00079D74
	public void test2()
	{
		Res.outz("gui test1");
		Message message = null;
		try
		{
			message = new Message(1);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600080D RID: 2061 RVA: 0x00004887 File Offset: 0x00002A87
	public void testJoint()
	{
	}

	// Token: 0x0600080E RID: 2062 RVA: 0x0007BBD4 File Offset: 0x00079DD4
	public void mobCapcha(char ch)
	{
		Res.outz("cap char c= " + ch.ToString());
		Message message = null;
		try
		{
			message = new Message(-85);
			message.writer().writeChar(ch);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600080F RID: 2063 RVA: 0x0007BC44 File Offset: 0x00079E44
	public void friend(sbyte action, int playerId)
	{
		Res.outz("add friend");
		Message message = null;
		try
		{
			message = new Message(-80);
			message.writer().writeByte(action);
			if (playerId != -1)
			{
				message.writer().writeInt(playerId);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000810 RID: 2064 RVA: 0x0007BCCC File Offset: 0x00079ECC
	public void getArchivemnt(int index)
	{
		Res.outz("get ngoc");
		Message message = null;
		try
		{
			message = new Message(-76);
			message.writer().writeByte(index);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000811 RID: 2065 RVA: 0x0007BD44 File Offset: 0x00079F44
	public void getPlayerMenu(int playerID)
	{
		Message message = null;
		try
		{
			message = new Message(-79);
			message.writer().writeInt(playerID);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000812 RID: 2066 RVA: 0x0007BD9C File Offset: 0x00079F9C
	public void clanImage(sbyte id)
	{
		Message message = null;
		try
		{
			message = new Message(-62);
			message.writer().writeByte(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000813 RID: 2067 RVA: 0x0007BE0C File Offset: 0x0007A00C
	public void skill_not_focus(sbyte status)
	{
		Message message = null;
		try
		{
			message = new Message(-45);
			message.writer().writeByte(status);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000814 RID: 2068 RVA: 0x0007BE7C File Offset: 0x0007A07C
	public void clanDonate(int id)
	{
		Message message = null;
		try
		{
			message = new Message(-54);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000815 RID: 2069 RVA: 0x0007BEEC File Offset: 0x0007A0EC
	public void clanMessage(int type, string text, int clanID)
	{
		Message message = null;
		try
		{
			message = new Message(-51);
			message.writer().writeByte(type);
			if (type == 0)
			{
				message.writer().writeUTF(text);
			}
			if (type == 2)
			{
				message.writer().writeInt(clanID);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000816 RID: 2070 RVA: 0x0007BF78 File Offset: 0x0007A178
	public void useItem(sbyte type, sbyte where, sbyte index, short template)
	{
		Cout.println("USE ITEM! " + type.ToString());
		if (global::Char.myCharz().statusMe == 14)
		{
			return;
		}
		Message message = null;
		try
		{
			message = new Message(-43);
			message.writer().writeByte(type);
			message.writer().writeByte(where);
			message.writer().writeByte(index);
			if (index == -1)
			{
				message.writer().writeShort(template);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000817 RID: 2071 RVA: 0x0007C020 File Offset: 0x0007A220
	public void joinClan(int id, sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-49);
			message.writer().writeInt(id);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000818 RID: 2072 RVA: 0x0007C09C File Offset: 0x0007A29C
	public void clanMember(int id)
	{
		Message message = null;
		try
		{
			message = new Message(-50);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000819 RID: 2073 RVA: 0x0007C10C File Offset: 0x0007A30C
	public void searchClan(string text)
	{
		Message message = null;
		try
		{
			message = new Message(-47);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600081A RID: 2074 RVA: 0x0007C17C File Offset: 0x0007A37C
	public void requestClan(short id)
	{
		Message message = null;
		try
		{
			message = new Message(-53);
			message.writer().writeShort(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600081B RID: 2075 RVA: 0x0007C1EC File Offset: 0x0007A3EC
	public void clanRemote(int id, sbyte role)
	{
		Message message = null;
		try
		{
			message = new Message(-56);
			message.writer().writeInt(id);
			message.writer().writeByte(role);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600081C RID: 2076 RVA: 0x0007C268 File Offset: 0x0007A468
	public void leaveClan()
	{
		Message message = null;
		try
		{
			message = new Message(-55);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600081D RID: 2077 RVA: 0x0007C2CC File Offset: 0x0007A4CC
	public void clanInvite(sbyte action, int playerID, int clanID, int code)
	{
		Message message = null;
		try
		{
			message = new Message(-57);
			message.writer().writeByte(action);
			if (action == 0)
			{
				message.writer().writeInt(playerID);
			}
			if (action == 1 || action == 2)
			{
				message.writer().writeInt(clanID);
				message.writer().writeInt(code);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600081E RID: 2078 RVA: 0x0007C36C File Offset: 0x0007A56C
	public void getClan(sbyte action, sbyte id, string text)
	{
		Message message = null;
		try
		{
			message = new Message(-46);
			message.writer().writeByte(action);
			if (action == 2 || action == 4)
			{
				message.writer().writeByte(id);
				message.writer().writeUTF(text);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600081F RID: 2079 RVA: 0x0007C3FC File Offset: 0x0007A5FC
	public void updateCaption(sbyte gender)
	{
		Message message = null;
		try
		{
			message = new Message(-41);
			message.writer().writeByte(gender);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000820 RID: 2080 RVA: 0x0007C46C File Offset: 0x0007A66C
	public void getItem(sbyte type, sbyte id)
	{
		Message message = null;
		try
		{
			message = new Message(-40);
			message.writer().writeByte(type);
			message.writer().writeByte(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000821 RID: 2081 RVA: 0x0007C4E8 File Offset: 0x0007A6E8
	public void getTask(int npcTemplateId, int menuId, int optionId)
	{
		Message message = null;
		try
		{
			message = new Message(40);
			message.writer().writeByte(npcTemplateId);
			message.writer().writeByte(menuId);
			if (optionId >= 0)
			{
				message.writer().writeByte(optionId);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000822 RID: 2082 RVA: 0x0007C574 File Offset: 0x0007A774
	public Message messageNotLogin(sbyte command)
	{
		Message message = new Message(-29);
		message.writer().writeByte(command);
		return message;
	}

	// Token: 0x06000823 RID: 2083 RVA: 0x0007C589 File Offset: 0x0007A789
	public Message messageNotMap(sbyte command)
	{
		Message message = new Message(-28);
		message.writer().writeByte(command);
		return message;
	}

	// Token: 0x06000824 RID: 2084 RVA: 0x0007C59E File Offset: 0x0007A79E
	public static Message messageSubCommand(sbyte command)
	{
		Message message = new Message(-30);
		message.writer().writeByte(command);
		return message;
	}

	// Token: 0x06000825 RID: 2085 RVA: 0x0007C5B4 File Offset: 0x0007A7B4
	public void setClientType()
	{
		if (Rms.loadRMSInt("clienttype") != -1)
		{
			Main.typeClient = Rms.loadRMSInt("clienttype");
		}
		try
		{
			Message message = this.messageNotLogin(2);
			message.writer().writeByte(Main.typeClient);
			message.writer().writeByte(mGraphics.zoomLevel);
			message.writer().writeBoolean(false);
			message.writer().writeInt(GameCanvas.w);
			message.writer().writeInt(GameCanvas.h);
			message.writer().writeBoolean(TField.isQwerty);
			message.writer().writeBoolean(GameCanvas.isTouch);
			message.writer().writeUTF(GameCanvas.getPlatformName() + "|" + GameMidlet.VERSION);
			DataInputStream dataInputStream = MyStream.readFile("/info");
			if (dataInputStream != null)
			{
				sbyte[] array = new sbyte[dataInputStream.r.buffer.Length];
				dataInputStream.read(ref array);
				if (array != null)
				{
					message.writer().writeShort(array.Length);
					message.writer().write(array);
					Res.err("write " + array.Length.ToString() + "|" + GameMidlet.VERSION);
				}
			}
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
	}

	// Token: 0x06000826 RID: 2086 RVA: 0x0007C728 File Offset: 0x0007A928
	public void setClientType2()
	{
		Res.outz("SET CLIENT TYPE");
		if (Rms.loadRMSInt("clienttype") != -1)
		{
			mSystem.clientType = Rms.loadRMSInt("clienttype");
		}
		try
		{
			Res.outz("setType");
			Message message = this.messageNotLogin(2);
			message.writer().writeByte(mSystem.clientType);
			message.writer().writeByte(mGraphics.zoomLevel);
			Res.outz("gui zoomlevel = " + mGraphics.zoomLevel.ToString());
			message.writer().writeBoolean(false);
			message.writer().writeInt(GameCanvas.w);
			message.writer().writeInt(GameCanvas.h);
			message.writer().writeBoolean(TField.isQwerty);
			message.writer().writeBoolean(GameCanvas.isTouch);
			message.writer().writeUTF(GameCanvas.getPlatformName() + "|" + GameMidlet.VERSION);
			DataInputStream dataInputStream = MyStream.readFile("/info");
			if (dataInputStream != null)
			{
				sbyte[] array = new sbyte[dataInputStream.r.buffer.Length];
				dataInputStream.read(ref array);
				if (array != null)
				{
					message.writer().writeShort(array.Length);
					message.writer().write(array);
					Res.err("write " + array.Length.ToString() + "|" + GameMidlet.VERSION);
				}
			}
			this.session = Session_ME2.gI();
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
			message.cleanup();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
	}

	// Token: 0x06000827 RID: 2087 RVA: 0x0007C8D0 File Offset: 0x0007AAD0
	public void sendCheckController()
	{
		Message message = null;
		try
		{
			message = new Message(-120);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			Service.curCheckController = mSystem.currentTimeMillis();
			message.cleanup();
		}
	}

	// Token: 0x06000828 RID: 2088 RVA: 0x0007C928 File Offset: 0x0007AB28
	public void sendCheckMap()
	{
		Message message = null;
		try
		{
			message = new Message(-121);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			Service.curCheckMap = mSystem.currentTimeMillis();
			message.cleanup();
		}
	}

	// Token: 0x06000829 RID: 2089 RVA: 0x0007C980 File Offset: 0x0007AB80
	public void login(string username, string pass, string version, sbyte type)
	{
		try
		{
			Message message = this.messageNotLogin(0);
			message.writer().writeUTF(username);
			message.writer().writeUTF(pass);
			message.writer().writeUTF(version);
			message.writer().writeByte(type);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
	}

	// Token: 0x0600082A RID: 2090 RVA: 0x0007CA04 File Offset: 0x0007AC04
	public void requestRegister(string username, string pass, string usernameAo, string passAo, string version)
	{
		try
		{
			Message message = this.messageNotLogin(1);
			message.writer().writeUTF(username);
			message.writer().writeUTF(pass);
			if (usernameAo != null && !usernameAo.Equals(string.Empty))
			{
				message.writer().writeUTF(usernameAo);
				message.writer().writeUTF("a");
			}
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
	}

	// Token: 0x0600082B RID: 2091 RVA: 0x0007CA9C File Offset: 0x0007AC9C
	public void requestChangeMap()
	{
		Message message = new Message(-23);
		this.session.sendMessage(message);
		message.cleanup();
	}

	// Token: 0x0600082C RID: 2092 RVA: 0x0007CAC4 File Offset: 0x0007ACC4
	public void magicTree(sbyte type)
	{
		Message message = new Message(-34);
		try
		{
			message.writer().writeByte(type);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600082D RID: 2093 RVA: 0x0007CB0C File Offset: 0x0007AD0C
	public void requestChangeZone(int zoneId, int indexUI)
	{
		Message message = new Message(21);
		try
		{
			message.writer().writeByte(zoneId);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600082E RID: 2094 RVA: 0x0007CB54 File Offset: 0x0007AD54
	public void checkMMove(int second)
	{
		Message message = new Message(-78);
		try
		{
			message.writer().writeInt(second);
			this.session.sendMessage(message);
			message.cleanup();
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x0600082F RID: 2095 RVA: 0x0007CB9C File Offset: 0x0007AD9C
	public void charMove()
	{
		int num = global::Char.myCharz().cx - global::Char.myCharz().cxSend;
		int num2 = global::Char.myCharz().cy - global::Char.myCharz().cySend;
		if (global::Char.ischangingMap || (num == 0 && num2 == 0) || Controller.isStopReadMessage || global::Char.myCharz().isTeleport || global::Char.myCharz().cy <= 0 || global::Char.myCharz().telePortSkill)
		{
			return;
		}
		try
		{
			Message message = new Message(-7);
			global::Char.myCharz().cxSend = global::Char.myCharz().cx;
			global::Char.myCharz().cySend = global::Char.myCharz().cy;
			global::Char.myCharz().cdirSend = global::Char.myCharz().cdir;
			global::Char.myCharz().cactFirst = global::Char.myCharz().statusMe;
			if (TileMap.tileTypeAt(global::Char.myCharz().cx / (int)TileMap.size, global::Char.myCharz().cy / (int)TileMap.size) == 0)
			{
				message.writer().writeByte(1);
				if (global::Char.myCharz().canFly)
				{
					if (!global::Char.myCharz().isHaveMount)
					{
						global::Char.myCharz().cMP -= global::Char.myCharz().cMPGoc / 100 * ((global::Char.myCharz().isMonkey != 1) ? 1 : 2);
					}
					if (global::Char.myCharz().cMP < 0)
					{
						global::Char.myCharz().cMP = 0;
					}
					GameScr.gI().isInjureMp = true;
					GameScr.gI().twMp = 0;
				}
			}
			else
			{
				message.writer().writeByte(0);
			}
			message.writer().writeShort(global::Char.myCharz().cx);
			if (num2 != 0)
			{
				message.writer().writeShort(global::Char.myCharz().cy);
			}
			this.session.sendMessage(message);
			GameScr.tickMove++;
			message.cleanup();
		}
		catch (Exception ex)
		{
			Cout.LogError("LOI CHAR MOVE " + ex.ToString());
		}
	}

	// Token: 0x06000830 RID: 2096 RVA: 0x0007CDA4 File Offset: 0x0007AFA4
	public void selectCharToPlay(string charname)
	{
		Message message = new Message(-28);
		try
		{
			message.writer().writeByte(1);
			message.writer().writeUTF(charname);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		this.session.sendMessage(message);
	}

	// Token: 0x06000831 RID: 2097 RVA: 0x00004887 File Offset: 0x00002A87
	public void selectZone(sbyte sub, int value)
	{
	}

	// Token: 0x06000832 RID: 2098 RVA: 0x0007CE08 File Offset: 0x0007B008
	public void createChar(string name, int gender, int hair)
	{
		Message message = new Message(-28);
		try
		{
			message.writer().writeByte(2);
			message.writer().writeUTF(name);
			message.writer().writeByte(gender);
			message.writer().writeByte(hair);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		this.session.sendMessage(message);
	}

	// Token: 0x06000833 RID: 2099 RVA: 0x0007CE84 File Offset: 0x0007B084
	public void requestModTemplate(int modTemplateId)
	{
		Message message = null;
		try
		{
			message = new Message(11);
			message.writer().writeByte(modTemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000834 RID: 2100 RVA: 0x0007CEF4 File Offset: 0x0007B0F4
	public void requestNpcTemplate(int npcTemplateId)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(12);
			message.writer().writeByte(npcTemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000835 RID: 2101 RVA: 0x0007CF64 File Offset: 0x0007B164
	public void requestSkill(int skillId)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(9);
			message.writer().writeShort(skillId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000836 RID: 2102 RVA: 0x0007CFD4 File Offset: 0x0007B1D4
	public void requestItemInfo(int typeUI, int indexUI)
	{
		Message message = null;
		try
		{
			message = new Message(35);
			message.writer().writeByte(typeUI);
			message.writer().writeByte(indexUI);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000837 RID: 2103 RVA: 0x0007D050 File Offset: 0x0007B250
	public void requestItemPlayer(int charId, int indexUI)
	{
		Message message = null;
		try
		{
			message = new Message(90);
			message.writer().writeInt(charId);
			message.writer().writeByte(indexUI);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000838 RID: 2104 RVA: 0x0007D0CC File Offset: 0x0007B2CC
	public void upSkill(int skillTemplateId, int point)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(17);
			message.writer().writeShort(skillTemplateId);
			message.writer().writeByte(point);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000839 RID: 2105 RVA: 0x0007D148 File Offset: 0x0007B348
	public void saleItem(sbyte action, sbyte type, short id)
	{
		Message message = null;
		try
		{
			message = new Message(7);
			message.writer().writeByte(action);
			message.writer().writeByte(type);
			message.writer().writeShort(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600083A RID: 2106 RVA: 0x0007D1CC File Offset: 0x0007B3CC
	public void buyItem(sbyte type, int id, int quantity)
	{
		Message message = null;
		try
		{
			message = new Message(6);
			message.writer().writeByte(type);
			message.writer().writeShort(id);
			if (quantity > 1)
			{
				message.writer().writeShort(quantity);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600083B RID: 2107 RVA: 0x0007D254 File Offset: 0x0007B454
	public void selectSkill(int skillTemplateId)
	{
		Cout.println(global::Char.myCharz().cName + " SELECT SKILL " + skillTemplateId.ToString());
		Message message = null;
		try
		{
			message = new Message(34);
			message.writer().writeShort(skillTemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600083C RID: 2108 RVA: 0x0007D2E4 File Offset: 0x0007B4E4
	public void getEffData(short id)
	{
		Message message = null;
		try
		{
			message = new Message(-66);
			message.writer().writeShort(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600083D RID: 2109 RVA: 0x0007D354 File Offset: 0x0007B554
	public void openUIZone()
	{
		Message message = null;
		try
		{
			message = new Message(29);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600083E RID: 2110 RVA: 0x0007D3B8 File Offset: 0x0007B5B8
	public void confirmMenu(short npcID, sbyte select)
	{
		Res.outz("confirme menu" + select.ToString());
		Message message = null;
		try
		{
			message = new Message(32);
			message.writer().writeShort(npcID);
			message.writer().writeByte(select);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600083F RID: 2111 RVA: 0x0007D448 File Offset: 0x0007B648
	public void openMenu(int npcId)
	{
		Message message = null;
		try
		{
			message = new Message(33);
			message.writer().writeShort(npcId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000840 RID: 2112 RVA: 0x0007D4B8 File Offset: 0x0007B6B8
	public void menu(int npcId, int menuId, int optionId)
	{
		Cout.println("menuid: " + menuId.ToString());
		Message message = null;
		try
		{
			message = new Message(22);
			message.writer().writeByte(npcId);
			message.writer().writeByte(menuId);
			message.writer().writeByte(optionId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000841 RID: 2113 RVA: 0x0007D554 File Offset: 0x0007B754
	public void menuId(short menuId)
	{
		Message message = null;
		try
		{
			message = new Message(27);
			message.writer().writeShort(menuId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000842 RID: 2114 RVA: 0x0007D5C4 File Offset: 0x0007B7C4
	public void textBoxId(short menuId, string str)
	{
		Message message = null;
		try
		{
			message = new Message(88);
			message.writer().writeShort(menuId);
			message.writer().writeUTF(str);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000843 RID: 2115 RVA: 0x0007D640 File Offset: 0x0007B840
	public void requestItem(int typeUI)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(22);
			message.writer().writeByte(typeUI);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000844 RID: 2116 RVA: 0x0007D6B0 File Offset: 0x0007B8B0
	public void boxSort()
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(19);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000845 RID: 2117 RVA: 0x0007D714 File Offset: 0x0007B914
	public void boxCoinOut(int coinOut)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(21);
			message.writer().writeInt(coinOut);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000846 RID: 2118 RVA: 0x0007D784 File Offset: 0x0007B984
	public void upgradeItem(Item item, Item[] items, bool isGold)
	{
		GameCanvas.msgdlg.pleasewait();
		Message message = null;
		try
		{
			message = new Message(14);
			message.writer().writeBoolean(isGold);
			message.writer().writeByte(item.indexUI);
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] != null)
				{
					message.writer().writeByte(items[i].indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000847 RID: 2119 RVA: 0x0007D834 File Offset: 0x0007BA34
	public void crystalCollectLock(Item[] items)
	{
		GameCanvas.msgdlg.pleasewait();
		Message message = null;
		try
		{
			message = new Message(13);
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] != null)
				{
					message.writer().writeByte(items[i].indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000848 RID: 2120 RVA: 0x0007D8C8 File Offset: 0x0007BAC8
	public void acceptInviteTrade(int playerMapId)
	{
		Message message = null;
		try
		{
			message = new Message(37);
			message.writer().writeInt(playerMapId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000849 RID: 2121 RVA: 0x0007D938 File Offset: 0x0007BB38
	public void cancelInviteTrade()
	{
		Message message = null;
		try
		{
			message = new Message(50);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600084A RID: 2122 RVA: 0x0007D99C File Offset: 0x0007BB9C
	public void tradeAccept()
	{
		Message message = null;
		try
		{
			message = new Message(39);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600084B RID: 2123 RVA: 0x0007DA00 File Offset: 0x0007BC00
	public void tradeItemLock(int coin, Item[] items)
	{
		Message message = null;
		try
		{
			message = new Message(38);
			message.writer().writeInt(coin);
			int num = 0;
			for (int i = 0; i < items.Length; i++)
			{
				if (items[i] != null)
				{
					num++;
				}
			}
			message.writer().writeByte(num);
			for (int j = 0; j < items.Length; j++)
			{
				if (items[j] != null)
				{
					message.writer().writeByte(items[j].indexUI);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600084C RID: 2124 RVA: 0x0007DABC File Offset: 0x0007BCBC
	public void sendPlayerAttack(MyVector vMob, MyVector vChar, int type)
	{
		try
		{
			Res.outz(">>SEND ATTACT  vMob=" + vMob.size().ToString() + "  vChar=" + vChar.size().ToString());
			Message message = null;
			if (type != 0)
			{
				if (vMob.size() > 0 && vChar.size() > 0)
				{
					if (type == 1)
					{
						message = new Message(-4);
					}
					else if (type == 2)
					{
						message = new Message(67);
					}
					message.writer().writeByte(vMob.size());
					for (int i = 0; i < vMob.size(); i++)
					{
						Mob mob = (Mob)vMob.elementAt(i);
						message.writer().writeByte(mob.mobId);
					}
					for (int j = 0; j < vChar.size(); j++)
					{
						global::Char @char = (global::Char)vChar.elementAt(j);
						if (@char != null)
						{
							message.writer().writeInt(@char.charID);
						}
						else
						{
							message.writer().writeInt(-1);
						}
					}
				}
				else if (vMob.size() > 0)
				{
					message = new Message(54);
					for (int k = 0; k < vMob.size(); k++)
					{
						Mob mob2 = (Mob)vMob.elementAt(k);
						if (!mob2.isMobMe)
						{
							message.writer().writeByte(mob2.mobId);
						}
						else
						{
							message.writer().writeByte(-1);
							message.writer().writeInt(mob2.mobId);
						}
					}
				}
				else if (vChar.size() > 0)
				{
					message = new Message(-60);
					for (int l = 0; l < vChar.size(); l++)
					{
						global::Char char2 = (global::Char)vChar.elementAt(l);
						message.writer().writeInt(char2.charID);
					}
				}
				message.writer().writeSByte((sbyte)global::Char.myCharz().cdir);
				if (message != null)
				{
					this.session.sendMessage(message);
				}
			}
		}
		catch (Exception)
		{
			Res.err(">>err ATTACT  vMob=" + vMob.size().ToString() + "  vChar=" + vChar.size().ToString());
		}
	}

	// Token: 0x0600084D RID: 2125 RVA: 0x0007DCF4 File Offset: 0x0007BEF4
	public void pickItem(int itemMapId)
	{
		Message message = null;
		try
		{
			message = new Message(-20);
			message.writer().writeShort(itemMapId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600084E RID: 2126 RVA: 0x0007DD64 File Offset: 0x0007BF64
	public void throwItem(int index)
	{
		Message message = null;
		try
		{
			message = new Message(-18);
			message.writer().writeByte(index);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600084F RID: 2127 RVA: 0x0007DDD4 File Offset: 0x0007BFD4
	public void returnTownFromDead()
	{
		Message message = null;
		try
		{
			message = new Message(-15);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000850 RID: 2128 RVA: 0x0007DE38 File Offset: 0x0007C038
	public void wakeUpFromDead()
	{
		Message message = null;
		try
		{
			message = new Message(-16);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000851 RID: 2129 RVA: 0x0007DE9C File Offset: 0x0007C09C
	public void chat(string text)
	{
		Message message = null;
		try
		{
			message = new Message(44);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000852 RID: 2130 RVA: 0x0007DF0C File Offset: 0x0007C10C
	public void updateData()
	{
		Message message = null;
		try
		{
			message = new Message(-87);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000853 RID: 2131 RVA: 0x0007DFA4 File Offset: 0x0007C1A4
	public void updateMap()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(6);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000854 RID: 2132 RVA: 0x0007E03C File Offset: 0x0007C23C
	public void updateSkill()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(7);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000855 RID: 2133 RVA: 0x0007E0C8 File Offset: 0x0007C2C8
	public void updateItem()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(8);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000856 RID: 2134 RVA: 0x0007E154 File Offset: 0x0007C354
	public void clientOk()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(13);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000857 RID: 2135 RVA: 0x0007E1B8 File Offset: 0x0007C3B8
	public void tradeInvite(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(36);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000858 RID: 2136 RVA: 0x0007E228 File Offset: 0x0007C428
	public void addFriend(string name)
	{
		Message message = null;
		try
		{
			message = new Message(53);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000859 RID: 2137 RVA: 0x0007E298 File Offset: 0x0007C498
	public void addPartyAccept(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(76);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085A RID: 2138 RVA: 0x0007E308 File Offset: 0x0007C508
	public void addPartyCancel(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(77);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085B RID: 2139 RVA: 0x0007E378 File Offset: 0x0007C578
	public void testInvite(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(59);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085C RID: 2140 RVA: 0x0007E3E8 File Offset: 0x0007C5E8
	public void addCuuSat(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(62);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085D RID: 2141 RVA: 0x0007E458 File Offset: 0x0007C658
	public void addParty(string name)
	{
		Message message = null;
		try
		{
			message = new Message(75);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085E RID: 2142 RVA: 0x0007E4C8 File Offset: 0x0007C6C8
	public void player_vs_player(sbyte action, sbyte type, int playerId)
	{
		Message message = null;
		try
		{
			message = new Message(-59);
			message.writer().writeByte(action);
			message.writer().writeByte(type);
			message.writer().writeInt(playerId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600085F RID: 2143 RVA: 0x0007E550 File Offset: 0x0007C750
	public void requestMaptemplate(int maptemplateId)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(10);
			message.writer().writeByte(maptemplateId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000860 RID: 2144 RVA: 0x0007E5C0 File Offset: 0x0007C7C0
	public void outParty()
	{
		Message message = null;
		try
		{
			message = new Message(79);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000861 RID: 2145 RVA: 0x0007E624 File Offset: 0x0007C824
	public void requestPlayerInfo(MyVector chars)
	{
		Message message = null;
		try
		{
			message = new Message(18);
			message.writer().writeByte(chars.size());
			for (int i = 0; i < chars.size(); i++)
			{
				global::Char @char = (global::Char)chars.elementAt(i);
				message.writer().writeInt(@char.charID);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000862 RID: 2146 RVA: 0x0007E6C8 File Offset: 0x0007C8C8
	public void pleaseInputParty(string str)
	{
		Message message = null;
		try
		{
			message = new Message(16);
			message.writer().writeUTF(str);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000863 RID: 2147 RVA: 0x0007E738 File Offset: 0x0007C938
	public void acceptPleaseParty(string str)
	{
		Message message = null;
		try
		{
			message = new Message(17);
			message.writer().writeUTF(str);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000864 RID: 2148 RVA: 0x0007E7A8 File Offset: 0x0007C9A8
	public void chatPlayer(string text, int id)
	{
		Res.outz("chat player text = " + text);
		Message message = null;
		try
		{
			message = new Message(-72);
			message.writer().writeInt(id);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000865 RID: 2149 RVA: 0x0007E834 File Offset: 0x0007CA34
	public void chatGlobal(string text)
	{
		Message message = null;
		try
		{
			message = new Message(-71);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000866 RID: 2150 RVA: 0x0007E8A4 File Offset: 0x0007CAA4
	public void chatPrivate(string to, string text)
	{
		Message message = null;
		try
		{
			message = new Message(91);
			message.writer().writeUTF(to);
			message.writer().writeUTF(text);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000867 RID: 2151 RVA: 0x0007E920 File Offset: 0x0007CB20
	public void sendCardInfo(string NAP, string PIN)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(16);
			message.writer().writeUTF(NAP);
			message.writer().writeUTF(PIN);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000868 RID: 2152 RVA: 0x0007E99C File Offset: 0x0007CB9C
	public void saveRms(string key, sbyte[] data)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(60);
			message.writer().writeUTF(key);
			message.writer().writeInt(data.Length);
			message.writer().write(data);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000869 RID: 2153 RVA: 0x0007EA24 File Offset: 0x0007CC24
	public void loadRMS(string key)
	{
		Cout.println("REQUEST RMS");
		Message message = null;
		try
		{
			message = Service.messageSubCommand(61);
			message.writer().writeUTF(key);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086A RID: 2154 RVA: 0x0007EA9C File Offset: 0x0007CC9C
	public void clearTask()
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(17);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086B RID: 2155 RVA: 0x0007EB00 File Offset: 0x0007CD00
	public void changeName(string name, int id)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(18);
			message.writer().writeInt(id);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086C RID: 2156 RVA: 0x0007EB7C File Offset: 0x0007CD7C
	public void requestIcon(int id)
	{
		GameCanvas.connect();
		Message message = null;
		try
		{
			Res.outz("REQUEST ICON " + id.ToString());
			message = new Message(-67);
			message.writer().writeInt(id);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086D RID: 2157 RVA: 0x0007EC3C File Offset: 0x0007CE3C
	public void doConvertUpgrade(int index1, int index2, int index3)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(33);
			message.writer().writeByte(index1);
			message.writer().writeByte(index2);
			message.writer().writeByte(index3);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086E RID: 2158 RVA: 0x0007ECC4 File Offset: 0x0007CEC4
	public void inviteClanDun(string name)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(34);
			message.writer().writeUTF(name);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600086F RID: 2159 RVA: 0x0007ED34 File Offset: 0x0007CF34
	public void inputNumSplit(int indexItem, int numSplit)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(40);
			message.writer().writeByte(indexItem);
			message.writer().writeInt(numSplit);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000870 RID: 2160 RVA: 0x0007EDB0 File Offset: 0x0007CFB0
	public void activeAccProtect(int pass)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(37);
			message.writer().writeInt(pass);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000871 RID: 2161 RVA: 0x0007EE20 File Offset: 0x0007D020
	public void clearAccProtect(int pass)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(41);
			message.writer().writeInt(pass);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000872 RID: 2162 RVA: 0x0007EE90 File Offset: 0x0007D090
	public void updateActive(int passOld, int passNew)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(38);
			message.writer().writeInt(passOld);
			message.writer().writeInt(passNew);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000873 RID: 2163 RVA: 0x0007EF0C File Offset: 0x0007D10C
	public void openLockAccProtect(int pass2)
	{
		Message message = null;
		try
		{
			message = this.messageNotMap(39);
			message.writer().writeInt(pass2);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000874 RID: 2164 RVA: 0x0007EF7C File Offset: 0x0007D17C
	public void getBgTemplate(short id)
	{
		Message message = null;
		try
		{
			message = new Message(-32);
			message.writer().writeShort(id);
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000875 RID: 2165 RVA: 0x0007F020 File Offset: 0x0007D220
	public void getMapOffline()
	{
		Message message = null;
		try
		{
			message = new Message(-33);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000876 RID: 2166 RVA: 0x0007F084 File Offset: 0x0007D284
	public void finishUpdate()
	{
		Message message = null;
		try
		{
			message = new Message(-38);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000877 RID: 2167 RVA: 0x0007F0E8 File Offset: 0x0007D2E8
	public void finishUpdate(int playerID)
	{
		Message message = null;
		try
		{
			message = new Message(-38);
			message.writer().writeInt(playerID);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000878 RID: 2168 RVA: 0x0007F140 File Offset: 0x0007D340
	public void finishLoadMap()
	{
		Message message = null;
		try
		{
			message = new Message(-39);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000879 RID: 2169 RVA: 0x0007F1A4 File Offset: 0x0007D3A4
	public void getChest(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-35);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087A RID: 2170 RVA: 0x0007F214 File Offset: 0x0007D414
	public void requestBagImage(sbyte ID)
	{
		Message message = null;
		try
		{
			message = new Message(-63);
			message.writer().writeByte(ID);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087B RID: 2171 RVA: 0x0007F284 File Offset: 0x0007D484
	public void getBag(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-36);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087C RID: 2172 RVA: 0x0007F2F4 File Offset: 0x0007D4F4
	public void getBody(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-37);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087D RID: 2173 RVA: 0x0007F364 File Offset: 0x0007D564
	public void login2(string user)
	{
		Res.outz("Login 2");
		Message message = null;
		try
		{
			message = new Message(-101);
			message.writer().writeUTF(user);
			message.writer().writeByte(1);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087E RID: 2174 RVA: 0x0007F3D4 File Offset: 0x0007D5D4
	public void getMagicTree(sbyte action)
	{
		Message message = null;
		try
		{
			message = new Message(-34);
			message.writer().writeByte(action);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600087F RID: 2175 RVA: 0x0007F444 File Offset: 0x0007D644
	public void upPotential(int typePotential, int num)
	{
		Message message = null;
		try
		{
			message = Service.messageSubCommand(16);
			message.writer().writeByte(typePotential);
			message.writer().writeShort(num);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000880 RID: 2176 RVA: 0x0007F4C0 File Offset: 0x0007D6C0
	public void getResource(sbyte action, MyVector vResourceIndex)
	{
		Res.outz("request resource action= " + action.ToString());
		Message message = null;
		try
		{
			message = new Message(-74);
			message.writer().writeByte(action);
			if (action == 2 && vResourceIndex != null)
			{
				message.writer().writeShort(vResourceIndex.size());
				for (int i = 0; i < vResourceIndex.size(); i++)
				{
					message.writer().writeShort(short.Parse((string)vResourceIndex.elementAt(i)));
				}
			}
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				Service.reciveFromMainSession = true;
				this.session = Session_ME.gI();
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000881 RID: 2177 RVA: 0x0007F5C4 File Offset: 0x0007D7C4
	public void requestMapSelect(int selected)
	{
		Res.outz("request magic tree");
		Message message = null;
		try
		{
			message = new Message(-91);
			message.writer().writeByte(selected);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000882 RID: 2178 RVA: 0x0007F628 File Offset: 0x0007D828
	public void petInfo()
	{
		Message message = null;
		try
		{
			message = new Message(-107);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000883 RID: 2179 RVA: 0x0007F674 File Offset: 0x0007D874
	public void sendTop(string topName, sbyte selected)
	{
		Message message = null;
		try
		{
			message = new Message(-96);
			message.writer().writeUTF(topName);
			message.writer().writeByte(selected);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000884 RID: 2180 RVA: 0x0007F6D8 File Offset: 0x0007D8D8
	public void enemy(sbyte b, int charID)
	{
		Message message = null;
		Res.outz("add enemy");
		try
		{
			message = new Message(-99);
			message.writer().writeByte(b);
			if (b == 1 || b == 2)
			{
				message.writer().writeInt(charID);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000885 RID: 2181 RVA: 0x0007F750 File Offset: 0x0007D950
	public void kigui(sbyte action, int itemId, sbyte moneyType, int money, int quaintly)
	{
		Message message = null;
		try
		{
			Res.outz("ki gui action= " + action.ToString());
			message = new Message(-100);
			message.writer().writeByte(action);
			if (action == 0)
			{
				message.writer().writeShort(itemId);
				message.writer().writeByte(moneyType);
				message.writer().writeInt(money);
				message.writer().writeInt(quaintly);
			}
			if (action == 1 || action == 2)
			{
				message.writer().writeShort(itemId);
			}
			if (action == 3)
			{
				message.writer().writeShort(itemId);
				message.writer().writeByte(moneyType);
				message.writer().writeInt(money);
			}
			if (action == 4)
			{
				message.writer().writeByte(moneyType);
				message.writer().writeByte(money);
				Res.outz("currTab= " + moneyType.ToString() + " page= " + money.ToString());
			}
			if (action == 5)
			{
				message.writer().writeShort(itemId);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000886 RID: 2182 RVA: 0x0007F880 File Offset: 0x0007DA80
	public void getFlag(sbyte action, sbyte flagType)
	{
		Message message = null;
		try
		{
			message = new Message(-103);
			message.writer().writeByte(action);
			Res.outz("------------service--  " + action.ToString() + "   " + flagType.ToString());
			if (action != 0)
			{
				message.writer().writeByte(flagType);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000887 RID: 2183 RVA: 0x0007F908 File Offset: 0x0007DB08
	public void setLockInventory(int pass)
	{
		Message message = null;
		try
		{
			Res.outz("------------setLockInventory:     " + pass.ToString());
			message = new Message(-104);
			message.writer().writeInt(pass);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000888 RID: 2184 RVA: 0x0007F978 File Offset: 0x0007DB78
	public void petStatus(sbyte status)
	{
		Message message = null;
		try
		{
			message = new Message(-108);
			message.writer().writeByte(status);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000889 RID: 2185 RVA: 0x0007F9D0 File Offset: 0x0007DBD0
	public void transportNow()
	{
		Message message = null;
		try
		{
			Res.outz("------------transportNow  ");
			message = new Message(-105);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088A RID: 2186 RVA: 0x0007FA28 File Offset: 0x0007DC28
	public void funsion(sbyte type)
	{
		Message message = null;
		try
		{
			Res.outz("FUNSION");
			message = new Message(125);
			message.writer().writeByte(type);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088B RID: 2187 RVA: 0x0007FA94 File Offset: 0x0007DC94
	public void imageSource(MyVector vID)
	{
		Message message = null;
		try
		{
			Res.outz("IMAGE SOURCE size= " + vID.size().ToString());
			message = new Message(-111);
			message.writer().writeShort(vID.size());
			if (vID.size() > 0)
			{
				for (int i = 0; i < vID.size(); i++)
				{
					Res.outz("gui len str " + ((ImageSource)vID.elementAt(i)).id);
					message.writer().writeUTF(((ImageSource)vID.elementAt(i)).id);
				}
			}
			if (Session_ME2.gI().isConnected() && !Session_ME2.connecting)
			{
				this.session = Session_ME2.gI();
			}
			else
			{
				this.session = Session_ME.gI();
				Service.reciveFromMainSession = true;
			}
			this.session.sendMessage(message);
			this.session = Session_ME.gI();
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088C RID: 2188 RVA: 0x0007FBAC File Offset: 0x0007DDAC
	public void getQuayso()
	{
		Message message = null;
		try
		{
			message = new Message(-126);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088D RID: 2189 RVA: 0x0007FC04 File Offset: 0x0007DE04
	public void sendServerData(sbyte action, int id, sbyte[] data)
	{
		Message message = null;
		try
		{
			Res.outz("SERVER DATA");
			message = new Message(-110);
			message.writer().writeByte(action);
			if (action == 1)
			{
				message.writer().writeInt(id);
				if (data != null)
				{
					int num = data.Length;
					message.writer().writeShort(num);
					message.writer().write(ref data, 0, num);
				}
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088E RID: 2190 RVA: 0x0007FC98 File Offset: 0x0007DE98
	public void changeOnKeyScr(sbyte[] skill)
	{
		Message message = null;
		try
		{
			message = new Message(-113);
			for (int i = 0; i < GameScr.onScreenSkill.Length; i++)
			{
				message.writer().writeByte(skill[i]);
			}
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x0600088F RID: 2191 RVA: 0x0007FD10 File Offset: 0x0007DF10
	public void requestPean()
	{
		Message message = null;
		try
		{
			message = new Message(-114);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000890 RID: 2192 RVA: 0x0007FD68 File Offset: 0x0007DF68
	public void sendThachDau(int id)
	{
		Res.outz("GUI THACH DAU");
		Message message = null;
		try
		{
			message = new Message(-118);
			message.writer().writeInt(id);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000891 RID: 2193 RVA: 0x0007FDD4 File Offset: 0x0007DFD4
	public void messagePlayerMenu(int charId)
	{
		Message message = null;
		try
		{
			message = new Message(-30);
			message.writer().writeByte(63);
			message.writer().writeInt(charId);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000892 RID: 2194 RVA: 0x0007FE44 File Offset: 0x0007E044
	public void playerMenuAction(int charId, short select)
	{
		Message message = null;
		try
		{
			message = new Message(-30);
			message.writer().writeByte(64);
			message.writer().writeInt(charId);
			message.writer().writeShort(select);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			ex.StackTrace.ToString();
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000893 RID: 2195 RVA: 0x0007FEC0 File Offset: 0x0007E0C0
	public void getImgByName(string nameImg)
	{
		Message message = null;
		try
		{
			message = new Message(66);
			message.writer().writeUTF(nameImg);
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000894 RID: 2196 RVA: 0x0007FF18 File Offset: 0x0007E118
	public void SendCrackBall(byte type, byte soluong)
	{
		Message message = new Message(-127);
		try
		{
			message.writer().writeByte((int)type);
			if (soluong > 0)
			{
				message.writer().writeByte((int)soluong);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000895 RID: 2197 RVA: 0x0007FF80 File Offset: 0x0007E180
	public void SendRada(int i, int id)
	{
		Message message = new Message(sbyte.MaxValue);
		try
		{
			message.writer().writeByte(i);
			if (id != -1)
			{
				message.writer().writeShort(id);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000896 RID: 2198 RVA: 0x0007FFE8 File Offset: 0x0007E1E8
	public void sendOptHat(sbyte action)
	{
		Message message = new Message(24);
		try
		{
			if (action == 1)
			{
				sbyte[] array = Res.TakeSnapShot();
				message.writer().writeByte(1);
				message.writer().writeShort(array.Length);
				message.writer().write(array);
			}
			else
			{
				message.writer().writeByte((global::Char.myCharz().idHat != -1) ? (-1) : 0);
			}
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000897 RID: 2199 RVA: 0x00080080 File Offset: 0x0007E280
	public void sendDelAcc()
	{
		Message message = new Message(69);
		try
		{
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000898 RID: 2200 RVA: 0x000800CC File Offset: 0x0007E2CC
	public void new_skill_not_focus(sbyte idTemplateSkill, sbyte dir, short x, short y)
	{
		Message message = null;
		try
		{
			message = new Message(-45);
			message.writer().writeSByte(20);
			message.writer().writeSByte(idTemplateSkill);
			message.writer().writeShort(global::Char.myCharz().cx);
			message.writer().writeShort(global::Char.myCharz().cy);
			message.writer().writeSByte(dir);
			message.writer().writeShort(x);
			message.writer().writeShort(y);
			this.session.sendMessage(message);
		}
		catch (Exception ex)
		{
			Cout.println(ex.Message + ex.StackTrace);
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x06000899 RID: 2201 RVA: 0x00080198 File Offset: 0x0007E398
	public void sendCT_ready(sbyte sub, sbyte sub_sub)
	{
		Message message = null;
		try
		{
			message = new Message(24);
			message.writer().writeByte(sub);
			message.writer().writeByte(sub_sub);
			Res.err(" =====> SEND OPTION_HAT " + sub.ToString() + "_" + sub_sub.ToString());
			this.session.sendMessage(message);
		}
		catch (Exception)
		{
		}
		finally
		{
			message.cleanup();
		}
	}

	// Token: 0x04000F2F RID: 3887
	internal ISession session = Session_ME.gI();

	// Token: 0x04000F30 RID: 3888
	protected static Service instance;

	// Token: 0x04000F31 RID: 3889
	public static long curCheckController;

	// Token: 0x04000F32 RID: 3890
	public static long curCheckMap;

	// Token: 0x04000F33 RID: 3891
	public static long logController;

	// Token: 0x04000F34 RID: 3892
	public static long logMap;

	// Token: 0x04000F35 RID: 3893
	public int demGui;

	// Token: 0x04000F36 RID: 3894
	public static bool reciveFromMainSession;
}
