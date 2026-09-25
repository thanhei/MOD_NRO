using System;
using System.Runtime.CompilerServices;
using Assets.src.e;
using Assets.src.g;
using Mod.CharEffect;

// Token: 0x02000015 RID: 21
public class Char : IMapObject
{
	// Token: 0x060000E5 RID: 229 RVA: 0x000089D0 File Offset: 0x00006BD0
	public Char()
	{
		this.statusMe = 6;
	}

	// Token: 0x060000E6 RID: 230 RVA: 0x00008C3C File Offset: 0x00006E3C
	public void applyCharLevelPercent()
	{
		try
		{
			long num = 1L;
			long num2 = 0L;
			int num3 = 0;
			for (int i = GameScr.exps.Length - 1; i >= 0; i--)
			{
				if (this.cPower >= GameScr.exps[i])
				{
					num = ((i != GameScr.exps.Length - 1) ? (GameScr.exps[i + 1] - GameScr.exps[i]) : 1L);
					num2 = this.cPower - GameScr.exps[i];
					num3 = i;
					break;
				}
			}
			this.clevel = num3;
			this.cLevelPercent = (long)((int)(num2 * 10000L / num));
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi char level percent: " + ex.ToString());
		}
	}

	// Token: 0x060000E7 RID: 231 RVA: 0x00008CF0 File Offset: 0x00006EF0
	public int getdxSkill()
	{
		if (this.myskill != null)
		{
			return this.myskill.dx;
		}
		return 0;
	}

	// Token: 0x060000E8 RID: 232 RVA: 0x00008D07 File Offset: 0x00006F07
	public int getdySkill()
	{
		if (this.myskill != null)
		{
			return this.myskill.dy;
		}
		return 0;
	}

	// Token: 0x060000E9 RID: 233 RVA: 0x00008D20 File Offset: 0x00006F20
	public static void taskAction(bool isNextStep)
	{
		Task task = global::Char.myCharz().taskMaint;
		if (task.index > task.contentInfo.Length - 1)
		{
			task.index = task.contentInfo.Length - 1;
		}
		string text = task.contentInfo[task.index];
		if (text != null && !text.Equals(string.Empty))
		{
			if (text.StartsWith("#"))
			{
				text = NinjaUtil.replace(text, "#", string.Empty);
				Npc npc = new Npc(5, 0, -100, -100, 5, GameScr.info1.charId[global::Char.myCharz().cgender][2]);
				npc.cx = (npc.cy = -100);
				npc.avatar = GameScr.info1.charId[global::Char.myCharz().cgender][2];
				npc.charID = 5;
				if (GameCanvas.currentScreen == GameScr.instance)
				{
					ChatPopup.addNextPopUpMultiLine(text, npc);
				}
			}
			else if (isNextStep)
			{
				GameScr.info1.addInfo(text, 0);
			}
		}
		GameScr.isHaveSelectSkill = true;
		Cout.println("TASKx " + global::Char.myCharz().taskMaint.taskId.ToString());
		if (global::Char.myCharz().taskMaint.taskId <= 2)
		{
			global::Char.myCharz().canFly = false;
		}
		else
		{
			global::Char.myCharz().canFly = true;
		}
		GameScr.gI().left = null;
		if (task.taskId == 0)
		{
			Hint.isViewMap = false;
			Hint.isViewPotential = false;
			GameScr.gI().right = null;
			GameScr.isHaveSelectSkill = false;
			GameScr.gI().left = null;
			if (task.index < 4)
			{
				MagicTree.isPaint = false;
				GameScr.isPaintRada = -1;
			}
			if (task.index == 4)
			{
				GameScr.isPaintRada = 1;
				MagicTree.isPaint = true;
			}
			if (task.index >= 5)
			{
				GameScr.gI().right = GameScr.gI().cmdFocus;
			}
		}
		if (task.taskId == 1)
		{
			GameScr.isHaveSelectSkill = true;
		}
		if (task.taskId >= 1)
		{
			GameScr.gI().right = GameScr.gI().cmdFocus;
			GameScr.gI().left = GameScr.gI().cmdMenu;
		}
		if (task.taskId >= 0)
		{
			Panel.isPaintMap = true;
		}
		else
		{
			Panel.isPaintMap = false;
		}
		if (task.taskId < 12)
		{
			GameCanvas.panel.mainTabName = mResources.mainTab1;
		}
		else
		{
			GameCanvas.panel.mainTabName = mResources.mainTab2;
		}
		GameCanvas.panel.tabName[0] = GameCanvas.panel.mainTabName;
		if (global::Char.myChar.taskMaint.taskId > 10)
		{
			Rms.saveRMSString("fake", "aa");
		}
	}

	// Token: 0x060000EA RID: 234 RVA: 0x00008FA8 File Offset: 0x000071A8
	public string getStrLevel()
	{
		string text = string.Concat(new string[]
		{
			this.strLevel[this.clevel],
			"+",
			(this.cLevelPercent / 100L).ToString(),
			".",
			(this.cLevelPercent % 100L).ToString(),
			"%"
		});
		if (text.Length > 23 && text.IndexOf("cấp ") >= 0)
		{
			text = Res.replace(text, "cấp ", "c");
		}
		return text;
	}

	// Token: 0x060000EB RID: 235 RVA: 0x0000903D File Offset: 0x0000723D
	public int avatarz()
	{
		return this.getAvatar(this.head);
	}

	// Token: 0x060000EC RID: 236 RVA: 0x0000904C File Offset: 0x0000724C
	public int getAvatar(int headId)
	{
		for (int i = 0; i < global::Char.idHead.Length; i++)
		{
			if (headId == (int)global::Char.idHead[i])
			{
				return (int)global::Char.idAvatar[i];
			}
		}
		return -1;
	}

	// Token: 0x060000ED RID: 237 RVA: 0x00009080 File Offset: 0x00007280
	public void setPowerInfo(string info, short p, short maxP, short sc)
	{
		this.powerPoint = p;
		this.strInfo = info;
		this.maxPowerPoint = maxP;
		this.secondPower = sc;
		this.lastS = (this.currS = mSystem.currentTimeMillis());
	}

	// Token: 0x060000EE RID: 238 RVA: 0x000090BE File Offset: 0x000072BE
	[MethodImpl(MethodImplOptions.NoOptimization)]
	public void addInfo(string info)
	{
		if (this.chatInfo == null)
		{
			this.chatInfo = new Info();
		}
		this.chatInfo.addInfo(info, 0, null, false);
	}

	// Token: 0x060000EF RID: 239 RVA: 0x000090E4 File Offset: 0x000072E4
	public int getSys()
	{
		if (this.nClass.classId == 1 || this.nClass.classId == 2)
		{
			return 1;
		}
		if (this.nClass.classId == 3 || this.nClass.classId == 4)
		{
			return 2;
		}
		if (this.nClass.classId == 5 || this.nClass.classId == 6)
		{
			return 3;
		}
		return 0;
	}

	// Token: 0x060000F0 RID: 240 RVA: 0x0000914C File Offset: 0x0000734C
	public static global::Char myCharz()
	{
		if (global::Char.myChar == null)
		{
			global::Char.myChar = new global::Char();
			global::Char.myChar.me = true;
			global::Char.myChar.cmtoChar = true;
		}
		return global::Char.myChar;
	}

	// Token: 0x060000F1 RID: 241 RVA: 0x0000917A File Offset: 0x0000737A
	public static global::Char myPetz()
	{
		if (global::Char.myPet == null)
		{
			global::Char.myPet = new global::Char();
			global::Char.myPet.me = false;
		}
		return global::Char.myPet;
	}

	// Token: 0x060000F2 RID: 242 RVA: 0x0000919D File Offset: 0x0000739D
	public static void clearMyChar()
	{
		global::Char.myChar = null;
	}

	// Token: 0x060000F3 RID: 243 RVA: 0x000091A8 File Offset: 0x000073A8
	public void bagSort()
	{
		try
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < this.arrItemBag.Length; i++)
			{
				Item item = this.arrItemBag[i];
				if (item != null && item.template.isUpToUp && !item.isExpires)
				{
					myVector.addElement(item);
				}
			}
			for (int j = 0; j < myVector.size(); j++)
			{
				Item item2 = (Item)myVector.elementAt(j);
				if (item2 != null)
				{
					for (int k = j + 1; k < myVector.size(); k++)
					{
						Item item3 = (Item)myVector.elementAt(k);
						if (item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock)
						{
							item2.quantity += item3.quantity;
							this.arrItemBag[item3.indexUI] = null;
							myVector.setElementAt(null, k);
						}
					}
				}
			}
			for (int l = 0; l < this.arrItemBag.Length; l++)
			{
				if (this.arrItemBag[l] != null)
				{
					for (int m = 0; m <= l; m++)
					{
						if (this.arrItemBag[m] == null)
						{
							this.arrItemBag[m] = this.arrItemBag[l];
							this.arrItemBag[m].indexUI = m;
							this.arrItemBag[l] = null;
							break;
						}
					}
				}
			}
		}
		catch (Exception)
		{
			Cout.println("Char.bagSort()");
		}
	}

	// Token: 0x060000F4 RID: 244 RVA: 0x00009334 File Offset: 0x00007534
	public void boxSort()
	{
		try
		{
			MyVector myVector = new MyVector();
			for (int i = 0; i < this.arrItemBox.Length; i++)
			{
				Item item = this.arrItemBox[i];
				if (item != null && item.template.isUpToUp && !item.isExpires)
				{
					myVector.addElement(item);
				}
			}
			for (int j = 0; j < myVector.size(); j++)
			{
				Item item2 = (Item)myVector.elementAt(j);
				if (item2 != null)
				{
					for (int k = j + 1; k < myVector.size(); k++)
					{
						Item item3 = (Item)myVector.elementAt(k);
						if (item3 != null && item2.template.Equals(item3.template) && item2.isLock == item3.isLock)
						{
							item2.quantity += item3.quantity;
							this.arrItemBox[item3.indexUI] = null;
							myVector.setElementAt(null, k);
						}
					}
				}
			}
			for (int l = 0; l < this.arrItemBox.Length; l++)
			{
				if (this.arrItemBox[l] != null)
				{
					for (int m = 0; m <= l; m++)
					{
						if (this.arrItemBox[m] == null)
						{
							this.arrItemBox[m] = this.arrItemBox[l];
							this.arrItemBox[m].indexUI = m;
							this.arrItemBox[l] = null;
							break;
						}
					}
				}
			}
		}
		catch (Exception)
		{
			Cout.println("Char.boxSort()");
		}
	}

	// Token: 0x060000F5 RID: 245 RVA: 0x000094C0 File Offset: 0x000076C0
	public void useItem(int indexUI)
	{
		Item item = this.arrItemBag[indexUI];
		if (!item.isTypeBody())
		{
			return;
		}
		item.isLock = true;
		item.typeUI = 5;
		Item item2 = this.arrItemBody[(int)item.template.type];
		this.arrItemBag[indexUI] = null;
		if (item2 != null)
		{
			item2.typeUI = 3;
			this.arrItemBody[(int)item.template.type] = null;
			item2.indexUI = indexUI;
			this.arrItemBag[indexUI] = item2;
		}
		item.indexUI = (int)item.template.type;
		this.arrItemBody[item.indexUI] = item;
		for (int i = 0; i < this.arrItemBody.Length; i++)
		{
			Item item3 = this.arrItemBody[i];
			if (item3 != null)
			{
				if (item3.template.type == 0)
				{
					this.body = (int)item3.template.part;
				}
				else if (item3.template.type == 1)
				{
					this.leg = (int)item3.template.part;
				}
			}
		}
	}

	// Token: 0x060000F6 RID: 246 RVA: 0x000095B4 File Offset: 0x000077B4
	public Skill getSkill(SkillTemplate skillTemplate)
	{
		for (int i = 0; i < this.vSkill.size(); i++)
		{
			if (((Skill)this.vSkill.elementAt(i)).template.id == skillTemplate.id)
			{
				return (Skill)this.vSkill.elementAt(i);
			}
		}
		return null;
	}

	// Token: 0x060000F7 RID: 247 RVA: 0x00009610 File Offset: 0x00007810
	public Waypoint isInEnterOfflinePoint()
	{
		Task task = global::Char.myChar.taskMaint;
		if (task != null && task.taskId == 0 && task.index < 6)
		{
			return null;
		}
		int num = TileMap.vGo.size();
		sbyte b = 0;
		while ((int)b < num)
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
			if (PopUp.vPopups.size() >= num && !((PopUp)PopUp.vPopups.elementAt((int)b)).isPaint)
			{
				return null;
			}
			if (this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && waypoint.isEnter && waypoint.isOffline)
			{
				return waypoint;
			}
			b += 1;
		}
		return null;
	}

	// Token: 0x060000F8 RID: 248 RVA: 0x000096E0 File Offset: 0x000078E0
	public Waypoint isInEnterOnlinePoint()
	{
		Task task = global::Char.myChar.taskMaint;
		if (task != null && task.taskId == 0 && task.index < 6)
		{
			return null;
		}
		int num = TileMap.vGo.size();
		sbyte b = 0;
		while ((int)b < num)
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
			if (PopUp.vPopups.size() >= num && !((PopUp)PopUp.vPopups.elementAt((int)b)).isPaint)
			{
				return null;
			}
			if (this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && waypoint.isEnter && !waypoint.isOffline)
			{
				return waypoint;
			}
			b += 1;
		}
		return null;
	}

	// Token: 0x060000F9 RID: 249 RVA: 0x000097B0 File Offset: 0x000079B0
	public bool isInWaypoint()
	{
		if (TileMap.isInAirMap() && this.cy >= TileMap.pxh - 48)
		{
			return true;
		}
		if (this.isTeleport || this.isUsePlane)
		{
			return false;
		}
		int num = TileMap.vGo.size();
		sbyte b = 0;
		while ((int)b < num)
		{
			Waypoint waypoint = (Waypoint)TileMap.vGo.elementAt((int)b);
			if ((TileMap.mapID == 47 || TileMap.isInAirMap()) && this.cy <= (int)(waypoint.minY + waypoint.maxY) && this.cx > (int)waypoint.minX && this.cx < (int)waypoint.maxX)
			{
				return !TileMap.isInAirMap() || this.cTypePk == 0;
			}
			if (this.cx >= (int)waypoint.minX && this.cx <= (int)waypoint.maxX && this.cy >= (int)waypoint.minY && this.cy <= (int)waypoint.maxY && !waypoint.isEnter)
			{
				return true;
			}
			b += 1;
		}
		return false;
	}

	// Token: 0x060000FA RID: 250 RVA: 0x000098B0 File Offset: 0x00007AB0
	public bool isPunchKickSkill()
	{
		return this.skillPaint != null && ((this.skillPaint.id >= 0 && this.skillPaint.id <= 6) || (this.skillPaint.id >= 14 && this.skillPaint.id <= 20) || (this.skillPaint.id >= 28 && this.skillPaint.id <= 34) || (this.skillPaint.id >= 63 && this.skillPaint.id <= 69));
	}

	// Token: 0x060000FB RID: 251 RVA: 0x00009948 File Offset: 0x00007B48
	public void soundUpdate()
	{
		if (this.me && this.statusMe == 10 && this.cf == 8 && this.ty > 20 && GameCanvas.gameTick % 20 == 0)
		{
			SoundMn.gI().charFly();
		}
		if (this.skillPaint != null && this.skillInfoPaint() != null && this.indexSkill < this.skillInfoPaint().Length && this.isPunchKickSkill() && (this.me || (!this.me && this.cx >= GameScr.cmx && this.cx <= GameScr.cmx + GameCanvas.w)) && GameCanvas.gameTick % 5 == 0)
		{
			if (this.cf == 9 || this.cf == 10 || this.cf == 11)
			{
				SoundMn.gI().charPunch(true, (!this.me) ? 0.05f : 0.1f);
				return;
			}
			SoundMn.gI().charPunch(false, (!this.me) ? 0.05f : 0.1f);
		}
	}

	// Token: 0x060000FC RID: 252 RVA: 0x00004887 File Offset: 0x00002A87
	public void updateChargeSkill()
	{
	}

	// Token: 0x060000FD RID: 253 RVA: 0x00009A5C File Offset: 0x00007C5C
	public virtual void update()
	{
		if (this.isMafuba)
		{
			this.cf = 23;
			this.countMafuba += 1;
			if (this.countMafuba > 150)
			{
				this.isMafuba = false;
			}
			return;
		}
		this.countMafuba = 0;
		if (this.isHide || this.isMabuHold)
		{
			return;
		}
		if ((this.isCopy || this.clevel >= 14) && this.statusMe != 1)
		{
			int num = this.statusMe;
		}
		if (this.petFollow != null)
		{
			if (GameCanvas.gameTick % 3 == 0)
			{
				if (global::Char.myCharz().cdir == 1)
				{
					this.petFollow.cmtoX = this.cx - 20;
				}
				if (global::Char.myCharz().cdir == -1)
				{
					this.petFollow.cmtoX = this.cx + 20;
				}
				this.petFollow.cmtoY = this.cy - 40;
				if (this.petFollow.cmx > this.cx)
				{
					this.petFollow.dir = -1;
				}
				else
				{
					this.petFollow.dir = 1;
				}
				if (this.petFollow.cmtoX < 100)
				{
					this.petFollow.cmtoX = 100;
				}
				if (this.petFollow.cmtoX > TileMap.pxw - 100)
				{
					this.petFollow.cmtoX = TileMap.pxw - 100;
				}
			}
			this.petFollow.update();
		}
		if (!this.me && this.cHP <= 0 && this.clanID != -100 && this.statusMe != 14 && this.statusMe != 5)
		{
			this.startDie((short)this.cx, (short)this.cy);
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
		else if (this.dHP > this.cHP)
		{
			int num2 = this.dHP - this.cHP >> 1;
			if (num2 < 1)
			{
				num2 = 1;
			}
			this.dHP -= num2;
		}
		else
		{
			this.dHP = this.cHP;
		}
		if (this.secondPower != 0)
		{
			this.currS = mSystem.currentTimeMillis();
			if (this.currS - this.lastS >= 1000L)
			{
				this.lastS = mSystem.currentTimeMillis();
				this.secondPower -= 1;
			}
		}
		if (this.isPaintNewSkill)
		{
			if (GameCanvas.timeNow > this.timeReset_newSkill || this.statusMe == 14 || this.statusMe == 5)
			{
				this.timeReset_newSkill = 0L;
				this.isPaintNewSkill = false;
			}
			this.UpdSkillPaint_NEW();
			if (this.isShadown)
			{
				this.updateShadown();
				return;
			}
		}
		else
		{
			if (!this.me && GameScr.notPaint)
			{
				return;
			}
			if (this.sleepEff && GameCanvas.gameTick % 10 == 0)
			{
				EffecMn.addEff(new Effect(41, this.cx, this.cy, 3, 1, 1));
			}
			if (this.huytSao)
			{
				this.huytSao = false;
				EffecMn.addEff(new Effect(39, this.cx, this.cy, 3, 3, 1));
			}
			if (this.blindEff && GameCanvas.gameTick % 5 == 0)
			{
				ServerEffect.addServerEffect(113, this, 1);
			}
			if (this.protectEff)
			{
				int num3 = this.cH_new + 73;
				if (GameCanvas.gameTick % 5 == 0)
				{
					this.eProtect = new Effect(33, this.cx, num3, 3, 3, 1);
				}
				if (this.eProtect != null)
				{
					this.eProtect.update();
					this.eProtect.x = this.cx;
					this.eProtect.y = num3;
				}
			}
			if (this.danhHieuEff)
			{
				if (this.eDanhHieu == null)
				{
					string text = (string)GameCanvas.danhHieu.get(this.charID.ToString() + string.Empty);
					if (text != null)
					{
						string[] array = Res.split(text.Trim(), ",", 0);
						short num4 = short.Parse(array[0]);
						short num5 = short.Parse(array[1]);
						this.eDanhHieu = new Effect((int)num4, this.cx, this.cH_new + 73, 1, -1, -1);
						this.eDanhHieu.timeExist = (long)(num5 * 1000) + mSystem.currentTimeMillis();
					}
				}
				if (this.eDanhHieu != null)
				{
					this.eDanhHieu.update();
					this.eDanhHieu.x = this.cx;
					this.eDanhHieu.y = this.cH_new;
					if (this.eDanhHieu.timeExist <= mSystem.currentTimeMillis())
					{
						this.eDanhHieu = null;
						GameCanvas.danhHieu.remove(this.charID.ToString() + string.Empty);
					}
				}
			}
			if (this.charFocus != null && this.charFocus.cy < 0)
			{
				this.charFocus = null;
			}
			if (this.isFusion)
			{
				this.tFusion++;
			}
			if (this.isNhapThe && GameCanvas.gameTick % 25 == 0)
			{
				ServerEffect.addServerEffect(114, this, 1);
			}
			if (this.isSetPos)
			{
				this.tpos++;
				if (this.tpos != 1)
				{
					return;
				}
				this.tpos = 0;
				this.isSetPos = false;
				this.cx = (int)this.xPos;
				this.cy = (int)this.yPos;
				this.cp1 = (this.cp2 = (this.cp3 = 0));
				if (this.typePos == 1)
				{
					if (this.me)
					{
						this.cxSend = this.cx;
						this.cySend = this.cy;
					}
					this.currentMovePoint = null;
					this.telePortSkill = false;
					ServerEffect.addServerEffect(173, this.cx, this.cy, 1);
				}
				else
				{
					ServerEffect.addServerEffect(60, this.cx, this.cy, 1);
				}
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
				{
					this.statusMe = 1;
					return;
				}
				this.statusMe = 4;
				return;
			}
			else
			{
				this.soundUpdate();
				if (this.stone)
				{
					return;
				}
				if (this.isFreez)
				{
					if (GameCanvas.gameTick % 5 == 0)
					{
						ServerEffect.addServerEffect(113, this.cx, this.cy, 1);
					}
					this.cf = 23;
					long num6 = mSystem.currentTimeMillis();
					if (num6 - this.lastFreez >= 1000L)
					{
						this.freezSeconds--;
						this.lastFreez = num6;
						if (this.freezSeconds < 0)
						{
							this.isFreez = false;
							this.seconds = 0;
							if (this.me)
							{
								global::Char.myCharz().isLockMove = false;
								GameScr.gI().dem = 0;
								GameScr.gI().isFreez = false;
							}
						}
					}
					if (TileMap.tileTypeAt(this.cx / (int)TileMap.size, this.cy / (int)TileMap.size) == 0)
					{
						this.ty++;
						this.wt++;
						this.fy += ((!this.wy) ? 1 : (-1));
						if (this.wt == 10)
						{
							this.wt = 0;
							this.wy = !this.wy;
						}
					}
					return;
				}
				if (this.isWaitMonkey)
				{
					this.isLockMove = true;
					this.cf = 17;
					if (GameCanvas.gameTick % 5 == 0)
					{
						ServerEffect.addServerEffect(154, this.cx, this.cy - 10, 2);
					}
					if (GameCanvas.gameTick % 5 == 0)
					{
						ServerEffect.addServerEffect(1, this.cx, this.cy + 10, 1);
					}
					this.chargeCount++;
					if (this.chargeCount == 500)
					{
						this.isWaitMonkey = false;
						this.isLockMove = false;
					}
					return;
				}
				if (this.isStandAndCharge)
				{
					this.chargeCount++;
					bool flag = !TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
					this.updateEffect();
					this.updateSkillPaint();
					this.moveFast = null;
					this.currentMovePoint = null;
					this.cf = 17;
					if (flag && this.cgender != 2)
					{
						this.cf = 12;
					}
					if (this.cgender == 2)
					{
						if (TileMap.mapID == 170)
						{
							int num7 = GameCanvas.gameTick % 4;
							if (GameCanvas.gameTick % 2 == 0)
							{
								if (this.cdir == 1)
								{
									ServerEffect.addServerEffect(70, this.cx - 18, this.cy - this.ch / 2 + 8, 1);
									ServerEffect.addServerEffect(70, this.cx + 23, this.cy - this.ch / 2 + 15, 1);
								}
								else
								{
									ServerEffect.addServerEffect(70, this.cx + 18, this.cy - this.ch / 2 + 8, 1);
									ServerEffect.addServerEffect(70, this.cx - 23, this.cy - this.ch / 2 + 15, 1);
								}
							}
						}
						else
						{
							if (GameCanvas.gameTick % 3 == 0)
							{
								ServerEffect.addServerEffect(154, this.cx, this.cy - this.ch / 2 + 10, 1);
							}
							if (GameCanvas.gameTick % 5 == 0)
							{
								ServerEffect.addServerEffect(114, this.cx + Res.random(-20, 20), this.cy + Res.random(-20, 20), 1);
							}
						}
					}
					if (this.cgender == 1)
					{
						int num8 = GameCanvas.gameTick % 4;
						if (GameCanvas.gameTick % 2 == 0)
						{
							if (this.cdir == 1)
							{
								ServerEffect.addServerEffect(70, this.cx - 18, this.cy - this.ch / 2 + 8, 1);
								ServerEffect.addServerEffect(70, this.cx + 23, this.cy - this.ch / 2 + 15, 1);
							}
							else
							{
								ServerEffect.addServerEffect(70, this.cx + 18, this.cy - this.ch / 2 + 8, 1);
								ServerEffect.addServerEffect(70, this.cx - 23, this.cy - this.ch / 2 + 15, 1);
							}
						}
					}
					if (this.cgender == 0 && GameCanvas.gameTick % 2 == 0)
					{
						if (this.cdir == 1)
						{
							ServerEffect.addServerEffect(70, this.cx - 18, this.cy - this.ch / 2 + 8, 1);
							ServerEffect.addServerEffect(70, this.cx + 23, this.cy - this.ch / 2 + 15, 1);
						}
						else
						{
							ServerEffect.addServerEffect(70, this.cx + 18, this.cy - this.ch / 2 + 8, 1);
							ServerEffect.addServerEffect(70, this.cx - 23, this.cy - this.ch / 2 + 15, 1);
						}
					}
					this.cur = mSystem.currentTimeMillis();
					Res.outz("  7.5 gong namekLazer " + this.cName + "_" + this.cgender.ToString());
					if (this.cur - this.last > (long)this.seconds || this.cur - this.last > 10000L)
					{
						Res.outz("<*> 8  namekLazer gong xong " + this.cName);
						this.stopUseChargeSkill();
						this.isStandAndCharge = false;
						int num9 = (int)this.myskill.skillId;
						if (this.me)
						{
							if (this.cgender == 2)
							{
								Res.outz("<*> 9 [me] xay da xong  " + global::Char.myCharz().myskill.skillId.ToString());
								global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], flag ? 1 : 0);
							}
							if (this.cgender == 1)
							{
								Res.outz("<*> 9 [me] namec xong " + global::Char.myCharz().myskill.skillId.ToString());
								this.isCreateDark = true;
								global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], flag ? 1 : 0);
							}
							if (this.cgender == 0)
							{
								Res.outz("<*> 9 [me] namec xong " + global::Char.myCharz().myskill.skillId.ToString());
								global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], flag ? 1 : 0);
							}
							if (global::Char.myCharz().myskill.skillId >= 77 && global::Char.myCharz().myskill.skillId <= 83)
							{
								Service.gI().skill_not_focus(4);
							}
							num9 = (int)global::Char.myCharz().myskill.skillId;
						}
						else
						{
							if (this.cgender == 2)
							{
								this.setSkillPaint(GameScr.sks[this.skillTemplateId], flag ? 1 : 0);
								Res.outz("<*> 10 xay da xong 111   " + this.skillTemplateId.ToString());
							}
							if (this.cgender == 1)
							{
								this.setSkillPaint(GameScr.sks[this.skillTemplateId], flag ? 1 : 0);
								Res.outz("<*> 10 C_NAMEC xong 222   " + this.skillTemplateId.ToString());
							}
							if (this.cgender == 0)
							{
								this.setSkillPaint(GameScr.sks[this.skillTemplateId], flag ? 1 : 0);
								Res.outz("<*> 10  C_TRAIDAT xong 333   " + this.skillTemplateId.ToString());
							}
							num9 = this.skillTemplateId;
						}
						if (this.cgender == 2 && this.statusMe != 14 && this.statusMe != 5 && (num9 < 77 || num9 > 83))
						{
							GameScr.gI().activeSuperPower(this.cx, this.cy);
						}
						Res.outz("<*> 11 Hoàn thành skill not focus -  STAND");
					}
					this.chargeCount++;
					if (this.chargeCount == 500)
					{
						this.stopUseChargeSkill();
					}
					return;
				}
				if (this.isFlyAndCharge)
				{
					this.updateEffect();
					this.updateSkillPaint();
					this.moveFast = null;
					this.currentMovePoint = null;
					this.posDisY++;
					if (TileMap.tileTypeAt(this.cx, this.cy - this.ch, 8192))
					{
						this.stopUseChargeSkill();
						return;
					}
					if (this.posDisY == 20)
					{
						this.last = mSystem.currentTimeMillis();
					}
					if (this.posDisY > 20)
					{
						this.cur = mSystem.currentTimeMillis();
						if (this.cur - this.last > (long)this.seconds || this.cur - this.last > 10000L)
						{
							Res.outz("<*> 12 kết thúc skill  qua cau kinh khi \tFLY " + this.cName);
							this.isFlyAndCharge = false;
							if (this.me)
							{
								this.isCreateDark = true;
								bool flag2 = TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2);
								this.isUseSkillAfterCharge = true;
								this.setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!flag2) ? 1 : 0);
								return;
							}
							if (TileMap.mapID == 170)
							{
								this.isCreateDark = true;
								this.isUseSkillAfterCharge = true;
								bool flag3 = TileMap.tileTypeAt(this.cx, this.cy, 2);
								this.setSkillPaint(GameScr.sks[this.skillTemplateId], (!flag3) ? 1 : 0);
								return;
							}
						}
						else
						{
							this.cf = 32;
							if (this.cgender == 0 && GameCanvas.gameTick % 3 == 0)
							{
								ServerEffect.addServerEffect(153, this.cx, this.cy - this.ch, 2);
							}
							if (TileMap.mapID == 170 && (this.cgender == 2 || this.cgender == 1) && GameCanvas.gameTick % 3 == 0)
							{
								ServerEffect.addServerEffect(153, this.cx, this.cy - this.ch, 2);
							}
							this.chargeCount++;
							if (this.chargeCount == 500)
							{
								this.stopUseChargeSkill();
								return;
							}
						}
					}
					else
					{
						if (this.statusMe != 14)
						{
							this.statusMe = 3;
						}
						this.cvy = -3;
						this.cy += this.cvy;
						this.cf = 7;
					}
					return;
				}
				else
				{
					if (this.me && GameCanvas.isTouch)
					{
						if (this.charFocus != null && this.charFocus.charID >= 0 && this.charFocus.cx > 100 && this.charFocus.cx < TileMap.pxw - 100 && this.isInEnterOnlinePoint() == null && this.isInEnterOfflinePoint() == null && !this.isAttacPlayerStatus() && TileMap.mapID != 51 && TileMap.mapID != 52 && GameCanvas.panel.vPlayerMenu.size() > 0 && GameScr.gI().popUpYesNo == null)
						{
							int num10 = Math2.abs(this.cx - this.charFocus.cx);
							int num11 = Math2.abs(this.cy - this.charFocus.cy);
							if (num10 < 60 && num11 < 40)
							{
								if (this.cmdMenu == null)
								{
									this.cmdMenu = new Command(mResources.MENU, 11111);
									this.cmdMenu.isPlaySoundButton = false;
								}
								this.cmdMenu.x = this.charFocus.cx - GameScr.cmx;
								this.cmdMenu.y = this.charFocus.cy - this.charFocus.ch - 30 - GameScr.cmy;
							}
							else
							{
								this.cmdMenu = null;
							}
						}
						else
						{
							this.cmdMenu = null;
						}
					}
					if (this.isShadown)
					{
						this.updateShadown();
					}
					if (this.isTeleport)
					{
						return;
					}
					if (this.chatInfo != null)
					{
						this.chatInfo.update();
					}
					if (this.shadowLife > 0)
					{
						this.shadowLife--;
					}
					if (this.resultTest > 0 && GameCanvas.gameTick % 2 == 0)
					{
						this.resultTest -= 1;
						if (this.resultTest == 30 || this.resultTest == 60)
						{
							this.resultTest = 0;
						}
					}
					this.updateSkillPaint();
					if (this.mobMe != null)
					{
						this.updateMobMe();
					}
					if (this.arr != null)
					{
						this.arr.update();
					}
					if (this.dart != null)
					{
						this.dart.update();
					}
					this.updateEffect();
					if (this.holdEffID != 0)
					{
						if (GameCanvas.gameTick % 5 == 0)
						{
							EffecMn.addEff(new Effect(32, this.cx, this.cy + 24, 3, 5, 1));
							return;
						}
					}
					else
					{
						if (this.blindEff || this.sleepEff)
						{
							return;
						}
						if (this.holder)
						{
							if (this.charHold != null && (this.charHold.statusMe == 14 || this.charHold.statusMe == 5))
							{
								this.removeHoleEff();
							}
							if (this.mobHold != null && this.mobHold.status == 1)
							{
								this.removeHoleEff();
							}
							if (this.me && this.statusMe == 2 && this.currentMovePoint != null)
							{
								this.holder = false;
								this.charHold = null;
								this.mobHold = null;
							}
							if (TileMap.tileTypeAt(this.cx, this.cy, 2))
							{
								this.cf = 16;
								return;
							}
							this.cf = 31;
							return;
						}
						else
						{
							if (this.cHP > 0)
							{
								for (int i = 0; i < this.vEff.size(); i++)
								{
									EffectChar effectChar = (EffectChar)this.vEff.elementAt(i);
									if (effectChar.template.type == 0 || effectChar.template.type == 12)
									{
										if (GameCanvas.isEff1)
										{
											this.cHP += (int)effectChar.param;
											this.cMP += (int)effectChar.param;
										}
									}
									else if (effectChar.template.type == 4 || effectChar.template.type == 17)
									{
										if (GameCanvas.isEff1)
										{
											this.cHP += (int)effectChar.param;
										}
									}
									else if (effectChar.template.type == 13 && GameCanvas.isEff1)
									{
										this.cHP -= this.cHPFull * 3 / 100;
										if (this.cHP < 1)
										{
											this.cHP = 1;
										}
									}
								}
								if (this.eff5BuffHp > 0 && GameCanvas.isEff2)
								{
									this.cHP += this.eff5BuffHp;
								}
								if (this.eff5BuffMp > 0 && GameCanvas.isEff2)
								{
									this.cMP += this.eff5BuffMp;
								}
								if (this.cHP > this.cHPFull)
								{
									this.cHP = this.cHPFull;
								}
								if (this.cMP > this.cMPFull)
								{
									this.cMP = this.cMPFull;
								}
							}
							if (this.cmtoChar)
							{
								GameScr.cmtoX = this.cx - GameScr.gW2;
								GameScr.cmtoY = this.cy - GameScr.gH23;
								if (!GameCanvas.isTouchControl)
								{
									GameScr.cmtoX += GameScr.gW6 * this.cdir;
								}
							}
							this.tick = (this.tick + 1) % 100;
							if (this.me)
							{
								if (this.charFocus != null && !GameScr.vCharInMap.contains(this.charFocus))
								{
									this.charFocus = null;
								}
								if (this.cx < 10)
								{
									this.cvx = 0;
									this.cx = 10;
								}
								else if (this.cx > TileMap.pxw - 10)
								{
									this.cx = TileMap.pxw - 10;
									this.cvx = 0;
								}
								if (!global::Char.ischangingMap && this.isInWaypoint())
								{
									Service.gI().charMove();
									if (TileMap.isTrainingMap())
									{
										Service.gI().getMapOffline();
										global::Char.ischangingMap = true;
									}
									else
									{
										Service.gI().requestChangeMap();
									}
									global::Char.isLockKey = true;
									global::Char.ischangingMap = true;
									GameCanvas.clearKeyHold();
									GameCanvas.clearKeyPressed();
									InfoDlg.showWait();
									return;
								}
								if (this.statusMe != 4 && Res.abs(this.cx - this.cxSend) + Res.abs(this.cy - this.cySend) >= 70 && this.cy - this.cySend <= 0 && this.me)
								{
									Service.gI().charMove();
								}
								if (this.isLockMove)
								{
									this.currentMovePoint = null;
								}
								if (this.currentMovePoint != null)
								{
									if (global::Char.abs(this.cx - this.currentMovePoint.xEnd) <= 16 && global::Char.abs(this.cy - this.currentMovePoint.yEnd) <= 16)
									{
										this.cx = (this.currentMovePoint.xEnd + this.cx) / 2;
										this.cy = this.currentMovePoint.yEnd;
										this.currentMovePoint = null;
										GameScr.instance.clickMoving = false;
										this.checkPerformEndMovePointAction();
										this.cvx = (this.cvy = 0);
										if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
										{
											this.statusMe = 1;
										}
										else
										{
											this.setCharFallFromJump();
										}
										Service.gI().charMove();
									}
									else
									{
										this.cdir = ((this.currentMovePoint.xEnd > this.cx) ? 1 : (-1));
										if (TileMap.tileTypeAt(this.cx, this.cy, 2))
										{
											this.statusMe = 2;
											if (this.currentMovePoint != null)
											{
												this.cvx = this.cspeed * this.cdir;
												this.cvy = 0;
											}
											if (global::Char.abs(this.cx - this.currentMovePoint.xEnd) <= 10)
											{
												if (this.currentMovePoint.yEnd > this.cy)
												{
													bool flag4 = false;
													sbyte b = ((this.cdir == 1) ? 1 : -1);
													for (int j = 0; j < 2; j++)
													{
														if (TileMap.tileTypeAt(this.currentMovePoint.xEnd + this.chw * (int)b, this.cy + this.chh * j, 2))
														{
															flag4 = true;
															break;
														}
													}
													if (flag4)
													{
														this.currentMovePoint = null;
														GameScr.instance.clickMoving = false;
														this.statusMe = 1;
														this.cvx = (this.cvy = 0);
														this.checkPerformEndMovePointAction();
													}
													else
													{
														SoundMn.gI().charJump();
														this.cx = this.currentMovePoint.xEnd;
														this.statusMe = 10;
														this.cvy = -5;
														this.cvx = 0;
														Res.outz("Jum lun");
													}
												}
												else
												{
													SoundMn.gI().charJump();
													this.cx = this.currentMovePoint.xEnd;
													this.statusMe = 10;
													this.cvy = -5;
													this.cvx = 0;
												}
											}
											if (this.cdir == 1)
											{
												if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4))
												{
													this.cvx = this.cspeed * this.cdir;
													this.statusMe = 10;
													this.cvy = -5;
												}
											}
											else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8))
											{
												this.cvx = this.cspeed * this.cdir;
												this.statusMe = 10;
												this.cvy = -5;
											}
										}
										else
										{
											if (this.currentMovePoint.yEnd < this.cy + 10)
											{
												this.statusMe = 10;
												this.cvy = -5;
												if (global::Char.abs(this.cy - this.currentMovePoint.yEnd) <= 10)
												{
													this.cy = this.currentMovePoint.yEnd;
													this.cvy = 0;
												}
												if (global::Char.abs(this.cx - this.currentMovePoint.xEnd) <= 10)
												{
													this.cvx = 0;
												}
												else
												{
													this.cvx = this.cspeed * this.cdir;
												}
											}
											else if (TileMap.tileTypeAt(this.cx, this.cy, 2))
											{
												this.currentMovePoint = null;
												GameScr.instance.clickMoving = false;
												this.statusMe = 1;
												this.cvx = (this.cvy = 0);
												this.checkPerformEndMovePointAction();
											}
											else
											{
												if (this.statusMe == 10 || this.statusMe == 2)
												{
													this.cvy = 0;
												}
												this.statusMe = 4;
											}
											if (this.currentMovePoint.yEnd > this.cy)
											{
												if (this.cdir == 1)
												{
													if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4))
													{
														this.cvx = (this.cvy = 0);
														this.statusMe = 4;
														this.currentMovePoint = null;
														GameScr.instance.clickMoving = false;
														this.checkPerformEndMovePointAction();
													}
												}
												else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8))
												{
													this.cvx = (this.cvy = 0);
													this.statusMe = 4;
													this.currentMovePoint = null;
													GameScr.instance.clickMoving = false;
													this.checkPerformEndMovePointAction();
												}
											}
										}
									}
								}
								this.searchFocus();
							}
							else
							{
								this.checkHideCharName();
								if (this.statusMe == 1 || this.statusMe == 6)
								{
									bool flag5 = false;
									if (this.currentMovePoint != null)
									{
										if (global::Char.abs(this.currentMovePoint.xEnd - this.cx) < 17 && global::Char.abs(this.currentMovePoint.yEnd - this.cy) < 25)
										{
											this.cx = this.currentMovePoint.xEnd;
											this.cy = this.currentMovePoint.yEnd;
											this.currentMovePoint = null;
											if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
											{
												this.statusMe = 1;
												this.cp3 = 0;
												GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
												GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
											}
											else
											{
												this.statusMe = 4;
												this.cvy = 0;
												this.cp1 = 0;
											}
											flag5 = true;
										}
										else if ((this.statusBeforeNothing == 10 || this.cf == 8) && this.vMovePoints.size() > 0)
										{
											flag5 = true;
										}
										else if (this.cy == this.currentMovePoint.yEnd)
										{
											if (this.cx != this.currentMovePoint.xEnd)
											{
												this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
												this.cf = GameCanvas.gameTick % 5 + 2;
											}
										}
										else if (this.cy < this.currentMovePoint.yEnd)
										{
											this.cf = 12;
											this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
											if (this.cvy < 0)
											{
												this.cvy = 0;
											}
											this.cy += this.cvy;
											if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
											{
												GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
												GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
											}
											this.cvy++;
											if (this.cvy > 16)
											{
												this.cy = (this.cy + this.currentMovePoint.yEnd) / 2;
											}
										}
										else
										{
											this.cf = 7;
											this.cx = (this.cx + this.currentMovePoint.xEnd) / 2;
											this.cy = (this.cy + this.currentMovePoint.yEnd) / 2;
										}
									}
									else
									{
										flag5 = true;
									}
									if (flag5 && this.vMovePoints.size() > 0)
									{
										this.currentMovePoint = (MovePoint)this.vMovePoints.firstElement();
										this.vMovePoints.removeElementAt(0);
										if (this.currentMovePoint.status == 2)
										{
											if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 12) & 2) != 2)
											{
												this.statusMe = 10;
												this.cp1 = 0;
												this.cp2 = 0;
												this.cvx = -(this.cx - this.currentMovePoint.xEnd) / 10;
												this.cvy = -(this.cy - this.currentMovePoint.yEnd) / 10;
												if (this.cx - this.currentMovePoint.xEnd > 0)
												{
													this.cdir = -1;
												}
												else if (this.cx - this.currentMovePoint.xEnd < 0)
												{
													this.cdir = 1;
												}
											}
											else
											{
												this.statusMe = 2;
												if (this.cx - this.currentMovePoint.xEnd > 0)
												{
													this.cdir = -1;
												}
												else if (this.cx - this.currentMovePoint.xEnd < 0)
												{
													this.cdir = 1;
												}
												this.cvx = this.cspeed * this.cdir;
												this.cvy = 0;
											}
										}
										else if (this.currentMovePoint.status == 3)
										{
											if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 23) & 2) != 2)
											{
												this.statusMe = 10;
												this.cp1 = 0;
												this.cp2 = 0;
												this.cvx = -(this.cx - this.currentMovePoint.xEnd) / 10;
												this.cvy = -(this.cy - this.currentMovePoint.yEnd) / 10;
												if (this.cx - this.currentMovePoint.xEnd > 0)
												{
													this.cdir = -1;
												}
												else if (this.cx - this.currentMovePoint.xEnd < 0)
												{
													this.cdir = 1;
												}
											}
											else
											{
												this.statusMe = 3;
												GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
												GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
												if (this.cx - this.currentMovePoint.xEnd > 0)
												{
													this.cdir = -1;
												}
												else if (this.cx - this.currentMovePoint.xEnd < 0)
												{
													this.cdir = 1;
												}
												this.cvx = global::Char.abs(this.cx - this.currentMovePoint.xEnd) / 10 * this.cdir;
												this.cvy = -10;
											}
										}
										else if (this.currentMovePoint.status == 4)
										{
											this.statusMe = 4;
											if (this.cx - this.currentMovePoint.xEnd > 0)
											{
												this.cdir = -1;
											}
											else if (this.cx - this.currentMovePoint.xEnd < 0)
											{
												this.cdir = 1;
											}
											this.cvx = global::Char.abs(this.cx - this.currentMovePoint.xEnd) / 9 * this.cdir;
											this.cvy = 0;
										}
										else
										{
											this.cx = this.currentMovePoint.xEnd;
											this.cy = this.currentMovePoint.yEnd;
											this.currentMovePoint = null;
										}
									}
								}
							}
							switch (this.statusMe)
							{
							case 1:
								this.updateCharStand();
								break;
							case 2:
								this.updateCharRun();
								break;
							case 3:
								this.updateCharJump();
								break;
							case 4:
								this.updateCharFall();
								break;
							case 5:
								this.updateCharDeadFly();
								break;
							case 6:
								if (this.isInjure <= 0)
								{
									this.cf = 0;
								}
								else if (this.statusBeforeNothing == 10)
								{
									this.cx += this.cvx;
								}
								else if (this.cf <= 1)
								{
									this.cp1++;
									if (this.cp1 > 6)
									{
										this.cf = 0;
									}
									else
									{
										this.cf = 1;
									}
									if (this.cp1 > 10)
									{
										this.cp1 = 0;
									}
								}
								if (this.cf != 7 && this.cf != 12 && (TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) != 2)
								{
									this.cvx = 0;
									this.cvy = 0;
									this.statusMe = 4;
									this.cf = 7;
								}
								if (!this.me)
								{
									this.cp3++;
									if (this.cp3 > 10)
									{
										if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) != 2)
										{
											this.cy += 5;
										}
										else
										{
											this.cf = 0;
										}
									}
									if (this.cp3 > 50)
									{
										this.cp3 = 0;
										this.currentMovePoint = null;
									}
								}
								break;
							case 9:
								this.updateCharAutoJump();
								break;
							case 10:
								this.updateCharFly();
								break;
							case 12:
								this.updateSkillStand();
								break;
							case 13:
								this.updateSkillFall();
								break;
							case 14:
								this.cp1++;
								if (this.cp1 > 30)
								{
									this.cp1 = 0;
								}
								if (this.cp1 % 15 < 5)
								{
									this.cf = 0;
								}
								else
								{
									this.cf = 1;
								}
								break;
							case 16:
								this.updateResetPoint();
								break;
							}
							if (this.isInjure > 0)
							{
								this.cf = 23;
								this.isInjure -= 1;
							}
							if (this.wdx != 0 || this.wdy != 0)
							{
								this.startDie(this.wdx, this.wdy);
								this.wdx = 0;
								this.wdy = 0;
							}
							if (this.moveFast != null)
							{
								if (this.moveFast[0] == 0)
								{
									short[] array2 = this.moveFast;
									int num12 = 0;
									array2[num12] += 1;
									ServerEffect.addServerEffect(60, this, 1);
								}
								else if (this.moveFast[0] < 10)
								{
									short[] array3 = this.moveFast;
									int num13 = 0;
									array3[num13] += 1;
								}
								else
								{
									this.cx = (int)this.moveFast[1];
									this.cy = (int)this.moveFast[2];
									this.moveFast = null;
									ServerEffect.addServerEffect(60, this, 1);
									if (this.me)
									{
										if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
										{
											this.statusMe = 4;
											global::Char.myCharz().setAutoSkillPaint(GameScr.sks[38], 1);
										}
										else
										{
											Service.gI().charMove();
											global::Char.myCharz().setAutoSkillPaint(GameScr.sks[38], 0);
										}
									}
								}
							}
							if (this.statusMe != 10)
							{
								this.fy = 0;
							}
							if (this.isCharge)
							{
								this.cf = 17;
								if (GameCanvas.gameTick % 4 == 0)
								{
									ServerEffect.addServerEffect(1, this.cx, this.cy + GameCanvas.transY, 1);
								}
								if (this.me)
								{
									long num14 = mSystem.currentTimeMillis();
									if (num14 - this.last >= 1000L)
									{
										Res.outz("%= " + this.myskill.damage.ToString());
										this.last = num14;
										this.cHP += this.cHPFull * (int)this.myskill.damage / 100;
										this.cMP += this.cMPFull * (int)this.myskill.damage / 100;
										if (this.cHP < this.cHPFull)
										{
											GameScr.startFlyText("+" + (this.cHPFull * (int)this.myskill.damage / 100).ToString() + " " + mResources.HP, this.cx, this.cy - this.ch - 20, 0, -1, mFont.HP);
										}
										if (this.cMP < this.cMPFull)
										{
											GameScr.startFlyText("+" + (this.cMPFull * (int)this.myskill.damage / 100).ToString() + " " + mResources.KI, this.cx, this.cy - this.ch - 20, 0, -2, mFont.MP);
										}
										Service.gI().skill_not_focus(2);
									}
								}
							}
							if (this.isFlyUp)
							{
								if (this.me)
								{
									global::Char.isLockKey = true;
									this.statusMe = 3;
									this.cvy = -8;
									if (this.cy <= TileMap.pxh - 240)
									{
										this.isFlyUp = false;
										global::Char.isLockKey = false;
										this.statusMe = 4;
									}
								}
								else
								{
									this.statusMe = 3;
									this.cvy = -8;
									if (this.cy <= TileMap.pxh - 240)
									{
										this.cvy = 0;
										this.isFlyUp = false;
										this.cvy = 0;
										this.statusMe = 1;
									}
								}
							}
							this.updateMount();
							this.updEffChar();
							this.updateEye();
							this.updateFHead();
						}
					}
				}
			}
		}
	}

	// Token: 0x060000FE RID: 254 RVA: 0x0000C0B8 File Offset: 0x0000A2B8
	internal void updateEffect()
	{
		if (this.effPaints != null)
		{
			for (int i = 0; i < this.effPaints.Length; i++)
			{
				if (this.effPaints[i] != null)
				{
					if (this.effPaints[i].eMob != null)
					{
						if (!this.effPaints[i].isFly)
						{
							this.effPaints[i].eMob.setInjure();
							this.effPaints[i].eMob.injureBy = this;
							if (this.me)
							{
								this.effPaints[i].eMob.hpInjure = global::Char.myCharz().cDamFull / 2 - global::Char.myCharz().cDamFull * NinjaUtil.randomNumber(11) / 100;
							}
							int num = this.effPaints[i].eMob.h >> 1;
							if (this.effPaints[i].eMob.isBigBoss())
							{
								num = this.effPaints[i].eMob.getY() + 20;
							}
							GameScr.startSplash(this.effPaints[i].eMob.x, this.effPaints[i].eMob.y - num, this.cdir);
							this.effPaints[i].isFly = true;
						}
					}
					else if (this.effPaints[i].eChar != null && !this.effPaints[i].isFly)
					{
						if (this.effPaints[i].eChar.charID >= 0)
						{
							this.effPaints[i].eChar.doInjure();
						}
						GameScr.startSplash(this.effPaints[i].eChar.cx, this.effPaints[i].eChar.cy - (this.effPaints[i].eChar.ch >> 1), this.cdir);
						this.effPaints[i].isFly = true;
					}
					this.effPaints[i].index++;
					if (this.effPaints[i].index >= this.effPaints[i].effCharPaint.arrEfInfo.Length)
					{
						this.effPaints[i] = null;
					}
				}
			}
		}
		if (this.indexEff >= 0 && this.eff != null && GameCanvas.gameTick % 2 == 0)
		{
			this.indexEff++;
			if (this.indexEff >= this.eff.arrEfInfo.Length)
			{
				this.indexEff = -1;
				this.eff = null;
			}
		}
		if (this.indexEffTask >= 0 && this.effTask != null && GameCanvas.gameTick % 2 == 0)
		{
			this.indexEffTask++;
			if (this.indexEffTask >= this.effTask.arrEfInfo.Length)
			{
				this.indexEffTask = -1;
				this.effTask = null;
			}
		}
	}

	// Token: 0x060000FF RID: 255 RVA: 0x0000C371 File Offset: 0x0000A571
	internal void checkPerformEndMovePointAction()
	{
		if (this.endMovePointCommand != null)
		{
			Command command = this.endMovePointCommand;
			this.endMovePointCommand = null;
			command.performAction();
		}
	}

	// Token: 0x06000100 RID: 256 RVA: 0x0000C390 File Offset: 0x0000A590
	internal void checkHideCharName()
	{
		if (GameCanvas.gameTick % 20 != 0 || this.charID < 0)
		{
			return;
		}
		this.paintName = true;
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = null;
			try
			{
				@char = (global::Char)GameScr.vCharInMap.elementAt(i);
			}
			catch (Exception)
			{
			}
			if (@char != null && !@char.Equals(this) && ((@char.cy == this.cy && Res.abs(@char.cx - this.cx) < 35) || (this.cy - @char.cy < 32 && this.cy - @char.cy > 0 && Res.abs(@char.cx - this.cx) < 24)))
			{
				this.paintName = false;
			}
		}
		for (int j = 0; j < GameScr.vNpc.size(); j++)
		{
			Npc npc = null;
			try
			{
				npc = (Npc)GameScr.vNpc.elementAt(j);
			}
			catch (Exception)
			{
			}
			if (npc != null && npc.cy == this.cy && Res.abs(npc.cx - this.cx) < 24)
			{
				this.paintName = false;
			}
		}
	}

	// Token: 0x06000101 RID: 257 RVA: 0x0000C4D0 File Offset: 0x0000A6D0
	internal void updateMobMe()
	{
		if (this.tMobMeBorn != 0)
		{
			this.tMobMeBorn--;
		}
		if (this.tMobMeBorn == 0)
		{
			this.mobMe.xFirst = ((this.cdir != 1) ? (this.cx + 30) : (this.cx - 30));
			this.mobMe.yFirst = this.cy - 60;
			int num = this.mobMe.xFirst - this.mobMe.x;
			int num2 = this.mobMe.yFirst - this.mobMe.y;
			this.mobMe.x += num / 4;
			this.mobMe.y += num2 / 4;
			this.mobMe.dir = this.cdir;
		}
	}

	// Token: 0x06000102 RID: 258 RVA: 0x0000C5A8 File Offset: 0x0000A7A8
	internal void updateSkillPaint()
	{
		if (this.statusMe == 14 || this.statusMe == 5)
		{
			return;
		}
		if (this.skillPaint != null && ((this.charFocus != null && this.isMeCanAttackOtherPlayer(this.charFocus) && this.charFocus.statusMe == 14) || (this.mobFocus != null && this.mobFocus.status == 0)))
		{
			if (!this.me)
			{
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
				{
					this.statusMe = 1;
				}
				else
				{
					this.statusMe = 6;
				}
				this.cp3 = 0;
			}
			this.indexSkill = 0;
			this.skillPaint = null;
			this.skillPaintRandomPaint = null;
			this.eff0 = (this.eff1 = (this.eff2 = null));
			this.i0 = (this.i1 = (this.i2 = 0));
			this.mobFocus = null;
			this.charFocus = null;
			this.effPaints = null;
			this.currentMovePoint = null;
			this.arr = null;
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
			{
				this.delayFall = 5;
			}
		}
		if (this.skillPaint != null && this.arr == null && this.skillInfoPaint() != null && this.indexSkill >= this.skillInfoPaint().Length)
		{
			if (!this.me)
			{
				if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
				{
					this.statusMe = 1;
				}
				else
				{
					this.statusMe = 6;
				}
				this.cp3 = 0;
			}
			this.indexSkill = 0;
			Res.outz("remove 2");
			this.skillPaint = null;
			this.skillPaintRandomPaint = null;
			this.eff0 = (this.eff1 = (this.eff2 = null));
			this.i0 = (this.i1 = (this.i2 = 0));
			this.arr = null;
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
			{
				this.delayFall = 5;
			}
		}
		SkillInfoPaint[] array = this.skillInfoPaint();
		if (array == null || this.indexSkill < 0 || this.indexSkill > array.Length - 1)
		{
			return;
		}
		if (array[this.indexSkill].effS0Id != 0)
		{
			this.eff0 = GameScr.efs[array[this.indexSkill].effS0Id - 1];
			this.i0 = (this.dx0 = (this.dy0 = 0));
		}
		if (array[this.indexSkill].effS1Id != 0)
		{
			this.eff1 = GameScr.efs[array[this.indexSkill].effS1Id - 1];
			this.i1 = (this.dx1 = (this.dy1 = 0));
		}
		if (array[this.indexSkill].effS2Id != 0)
		{
			this.eff2 = GameScr.efs[array[this.indexSkill].effS2Id - 1];
			this.i2 = (this.dx2 = (this.dy2 = 0));
		}
		SkillInfoPaint[] array2 = array;
		int num = this.indexSkill;
		if (array2 != null && array2[num] != null && num >= 0 && num <= array2.Length - 1 && array2[num].arrowId != 0)
		{
			int arrowId = array2[num].arrowId;
			if (arrowId >= 100)
			{
				IMapObject mapObject2;
				if (this.mobFocus == null)
				{
					IMapObject mapObject = this.charFocus;
					mapObject2 = mapObject;
				}
				else
				{
					IMapObject mapObject = this.mobFocus;
					mapObject2 = mapObject;
				}
				IMapObject mapObject3 = mapObject2;
				if (mapObject3 != null)
				{
					int num2;
					if (Res.abs(mapObject3.getX() - this.cx) > 4 * Res.abs(mapObject3.getY() - this.cy))
					{
						num2 = 0;
					}
					else
					{
						num2 = ((mapObject3.getY() >= this.cy) ? 3 : (-3));
						if (mapObject3 is BigBoss && ((BigBoss)mapObject3).haftBody)
						{
							num2 = -20;
						}
					}
					this.dart = new PlayerDart(this, arrowId - 100, this.skillPaintRandomPaint, this.cx + (array2[num].adx - 10) * this.cdir, this.cy + array2[num].ady + num2);
					if (this.myskill != null)
					{
						if (this.myskill.template.id == 1)
						{
							SoundMn.gI().traidatKame();
						}
						else if (this.myskill.template.id == 3)
						{
							SoundMn.gI().namekKame();
						}
						else if (this.myskill.template.id == 5)
						{
							SoundMn.gI().xaydaKame();
						}
						else if (this.myskill.template.id == 11)
						{
							SoundMn.gI().nameLazer();
						}
					}
				}
				else if (this.isFlyAndCharge || this.isUseSkillAfterCharge)
				{
					this.stopUseChargeSkill();
				}
			}
			else
			{
				Res.outz("g");
				this.arr = new Arrow(this, GameScr.arrs[arrowId - 1]);
				this.arr.life = 10;
				this.arr.ax = this.cx + array2[num].adx;
				this.arr.ay = this.cy + array2[num].ady;
			}
		}
		if ((this.mobFocus != null || (!this.me && this.charFocus != null) || (this.me && this.charFocus != null && (this.isMeCanAttackOtherPlayer(this.charFocus) || this.isSelectingSkillBuffToPlayer()) && this.arr == null && this.dart == null)) && this.indexSkill == array.Length - 1)
		{
			this.setAttack();
			if (this.me && this.myskill.template.isAttackSkill())
			{
				this.saveLoadPreviousSkill();
			}
		}
		if (this.me)
		{
			return;
		}
		IMapObject mapObject4 = null;
		if (this.mobFocus != null)
		{
			mapObject4 = this.mobFocus;
		}
		else if (this.charFocus != null)
		{
			mapObject4 = this.charFocus;
		}
		if (mapObject4 == null)
		{
			return;
		}
		if (Res.abs(mapObject4.getX() - this.cx) < 10)
		{
			if (mapObject4.getX() > this.cx)
			{
				this.cx -= 10;
			}
			else
			{
				this.cx += 10;
			}
		}
		if (mapObject4.getX() > this.cx)
		{
			this.cdir = 1;
			return;
		}
		this.cdir = -1;
	}

	// Token: 0x06000103 RID: 259 RVA: 0x00004887 File Offset: 0x00002A87
	public void saveLoadPreviousSkill()
	{
	}

	// Token: 0x06000104 RID: 260 RVA: 0x0000CBE8 File Offset: 0x0000ADE8
	public void setResetPoint(int x, int y)
	{
		InfoDlg.hide();
		this.currentMovePoint = null;
		int num = this.cx;
		if (this.cy - y == 0)
		{
			this.cx = x;
			global::Char.ischangingMap = false;
			global::Char.isLockKey = false;
			return;
		}
		this.statusMe = 16;
		this.cp2 = x;
		this.cp3 = y;
		this.cp1 = 0;
		global::Char.myCharz().cxSend = x;
		global::Char.myCharz().cySend = y;
	}

	// Token: 0x06000105 RID: 261 RVA: 0x0000CC5C File Offset: 0x0000AE5C
	internal void updateCharDeadFly()
	{
		this.isFreez = false;
		if (this.isCharge)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		this.cp1++;
		this.cx += (this.cp2 - this.cx) / 4;
		if (this.cp1 > 7)
		{
			this.cy += (this.cp3 - this.cy) / 4;
		}
		else
		{
			this.cy += this.cp1 - 10;
		}
		if (Res.abs(this.cp2 - this.cx) < 4 && Res.abs(this.cp3 - this.cy) < 10)
		{
			this.cx = this.cp2;
			this.cy = this.cp3;
			this.statusMe = 14;
			if (this.me)
			{
				GameScr.gI().resetButton();
				Service.gI().charMove();
			}
		}
		this.cf = 23;
	}

	// Token: 0x06000106 RID: 262 RVA: 0x0000CD6C File Offset: 0x0000AF6C
	internal void updateResetPoint()
	{
		InfoDlg.hide();
		GameCanvas.clearAllPointerEvent();
		this.currentMovePoint = null;
		this.cp1++;
		this.cx += (this.cp2 - this.cx) / 4;
		if (this.cp1 > 7)
		{
			this.cy += (this.cp3 - this.cy) / 4;
		}
		else
		{
			this.cy += this.cp1 - 10;
		}
		if (Res.abs(this.cp2 - this.cx) < 4 && Res.abs(this.cp3 - this.cy) < 10)
		{
			this.cx = this.cp2;
			this.cy = this.cp3;
			this.statusMe = 1;
			this.cp3 = 0;
			global::Char.ischangingMap = false;
			Service.gI().charMove();
		}
		this.cf = 23;
	}

	// Token: 0x06000107 RID: 263 RVA: 0x00004887 File Offset: 0x00002A87
	public void updateSkillFall()
	{
	}

	// Token: 0x06000108 RID: 264 RVA: 0x0000CE5C File Offset: 0x0000B05C
	public void updateSkillStand()
	{
		this.ty = 0;
		this.cp1++;
		if (this.cdir == 1)
		{
			if ((TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - this.chh) & 4) == 4)
			{
				this.cvx = 0;
			}
		}
		else if ((TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - this.chh) & 8) == 8)
		{
			this.cvx = 0;
		}
		if (this.cy > this.ch && TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192))
		{
			if (!TileMap.tileTypeAt(this.cx, this.cy, 2))
			{
				this.statusMe = 4;
				this.cp1 = 0;
				this.cp2 = 0;
				this.cvy = 1;
			}
			else
			{
				this.cy = TileMap.tileYofPixel(this.cy);
			}
		}
		this.cx += this.cvx;
		this.cy += this.cvy;
		if (this.cy < 0)
		{
			this.cy = (this.cvy = 0);
		}
		if (this.cvy == 0)
		{
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
			{
				this.statusMe = 4;
				this.cvx = (this.cspeed >> 1) * this.cdir;
				this.cp1 = (this.cp2 = 0);
			}
		}
		else if (this.cvy < 0)
		{
			this.cvy++;
			if (this.cvy == 0)
			{
				this.cvy = 1;
			}
		}
		else
		{
			if (this.cvy < 20 && this.cp1 % 5 == 0)
			{
				this.cvy++;
			}
			if (this.cvy > 3)
			{
				this.cvy = 3;
			}
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 3) & 2) == 2 && this.cy <= TileMap.tileXofPixel(this.cy + 3))
			{
				this.cvx = (this.cvy = 0);
				this.cy = TileMap.tileXofPixel(this.cy + 3);
			}
		}
		if (this.cvx > 0)
		{
			this.cvx--;
			return;
		}
		if (this.cvx < 0)
		{
			this.cvx++;
		}
	}

	// Token: 0x06000109 RID: 265 RVA: 0x0000D0C4 File Offset: 0x0000B2C4
	public void updateCharAutoJump()
	{
		this.isFreez = false;
		if (this.isCharge)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		this.cx += this.cvx * this.cdir;
		this.cy += this.cvyJump;
		this.cvyJump++;
		if (this.cp1 == 0)
		{
			this.cf = 7;
		}
		else
		{
			this.cf = 23;
		}
		if (this.cvyJump == -3)
		{
			this.cf = 8;
		}
		else if (this.cvyJump == -2)
		{
			this.cf = 9;
		}
		else if (this.cvyJump == -1)
		{
			this.cf = 10;
		}
		else if (this.cvyJump == 0)
		{
			this.cf = 11;
		}
		if (this.cvyJump == 0)
		{
			this.statusMe = 6;
			this.cp3 = 0;
			((MovePoint)this.vMovePoints.firstElement()).status = 4;
			this.isJump = true;
			this.cp1 = 0;
			this.cvy = 1;
		}
	}

	// Token: 0x0600010A RID: 266 RVA: 0x0000D1DC File Offset: 0x0000B3DC
	public int getVx(int size, int dx, int dy)
	{
		if (dy > 0 && !TileMap.tileTypeAt(this.cx, this.cy, 2))
		{
			if (dx - dy <= 10)
			{
				return 5;
			}
			if (dx - dy <= 30)
			{
				return 6;
			}
			if (dx - dy <= 50)
			{
				return 7;
			}
			if (dx - dy <= 70)
			{
				return 8;
			}
		}
		if (dx <= 30)
		{
			return 4;
		}
		if (dx <= 160)
		{
			return 5;
		}
		if (dx <= 270)
		{
			return 6;
		}
		if (dx <= 320)
		{
			return 7;
		}
		return 8;
	}

	// Token: 0x0600010B RID: 267 RVA: 0x0000D24B File Offset: 0x0000B44B
	public void hide()
	{
		this.isHide = true;
		EffecMn.addEff(new Effect(107, this.cx, this.cy + 25, 3, 15, 1));
	}

	// Token: 0x0600010C RID: 268 RVA: 0x0000D273 File Offset: 0x0000B473
	public void show()
	{
		this.isHide = false;
		EffecMn.addEff(new Effect(107, this.cx, this.cy + 25, 3, 10, 1));
	}

	// Token: 0x0600010D RID: 269 RVA: 0x0000D29B File Offset: 0x0000B49B
	public int getVy(int size, int dx, int dy)
	{
		if (dy <= 10)
		{
			return 5;
		}
		if (dy <= 20)
		{
			return 6;
		}
		if (dy <= 30)
		{
			return 7;
		}
		if (dy <= 40)
		{
			return 8;
		}
		if (dy <= 50)
		{
			return 9;
		}
		return 10;
	}

	// Token: 0x0600010E RID: 270 RVA: 0x0000D2C4 File Offset: 0x0000B4C4
	public int returnAct(int xFirst, int yFirst, int xEnd, int yEnd)
	{
		int num = xEnd - xFirst;
		int num2 = yEnd - yFirst;
		if (num == 0 && num2 == 0)
		{
			return 1;
		}
		if (num2 == 0 && yFirst % 24 == 0 && TileMap.tileTypeAt(xFirst, yFirst, 2))
		{
			return 2;
		}
		if (num2 > 0 && (yFirst % 24 != 0 || !TileMap.tileTypeAt(xFirst, yFirst, 2)))
		{
			return 4;
		}
		this.cvy = -10;
		this.cp1 = 0;
		this.cdir = ((num > 0) ? 1 : (-1));
		if (num <= 5)
		{
			this.cvx = 0;
		}
		else if (num <= 10)
		{
			this.cvx = 3;
		}
		else
		{
			this.cvx = 5;
		}
		return 9;
	}

	// Token: 0x0600010F RID: 271 RVA: 0x0000D350 File Offset: 0x0000B550
	public void setAutoJump()
	{
		int num = ((MovePoint)this.vMovePoints.firstElement()).xEnd - this.cx;
		this.cvyJump = -10;
		this.cp1 = 0;
		this.cdir = ((num > 0) ? 1 : (-1));
		if (num <= 6)
		{
			this.cvx = 0;
			return;
		}
		if (num <= 20)
		{
			this.cvx = 3;
			return;
		}
		this.cvx = 5;
	}

	// Token: 0x06000110 RID: 272 RVA: 0x0000D3B8 File Offset: 0x0000B5B8
	public void updateCharStand()
	{
		this.isSoundJump = false;
		this.isAttack = false;
		this.isAttFly = false;
		this.cvx = 0;
		this.cvy = 0;
		this.cp1++;
		if (this.cp1 > 30)
		{
			this.cp1 = 0;
		}
		if (this.cp1 % 15 < 5)
		{
			this.cf = 0;
		}
		else
		{
			this.cf = 1;
		}
		this.updateCharInBridge();
		if (!this.me)
		{
			this.cp3++;
			if (this.cp3 > 50)
			{
				this.cp3 = 0;
				this.currentMovePoint = null;
			}
		}
		this.updateSuperEff();
		if (!this.me || GameScr.vCharInMap.size() == 0 || TileMap.mapID != 50)
		{
			return;
		}
		global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(0);
		if (!@char.changePos)
		{
			if (@char.statusMe != 2)
			{
				@char.moveTo(this.cx - 45, this.cy, 0);
			}
			@char.lastUpdateTime = mSystem.currentTimeMillis();
			if (Res.abs(this.cx - 45 - @char.cx) <= 10)
			{
				@char.changePos = true;
			}
		}
		else
		{
			if (@char.statusMe != 2)
			{
				@char.moveTo(this.cx + 45, this.cy, 0);
			}
			@char.lastUpdateTime = mSystem.currentTimeMillis();
			if (Res.abs(this.cx + 45 - @char.cx) <= 10)
			{
				@char.changePos = false;
			}
		}
		if (GameCanvas.gameTick % 100 == 0)
		{
			@char.addInfo("Cắc cùm cum");
		}
	}

	// Token: 0x06000111 RID: 273 RVA: 0x0000D540 File Offset: 0x0000B740
	public void updateSuperEff()
	{
		if (this.isCopy || this.isFusion || this.isSetPos || this.isPet || this.isMiniPet || this.isMonkey == 1)
		{
			return;
		}
		if (this.me)
		{
			if (!global::Char.isPaintAura2 && this.idAuraEff > -1)
			{
				return;
			}
		}
		else if (this.idAuraEff > -1)
		{
			return;
		}
		this.ty++;
		if (this.clevel >= 14)
		{
			return;
		}
		if (this.clevel >= 9 && !GameCanvas.lowGraphic && (this.ty == 40 || this.ty == 50))
		{
			GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
			GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
			this.addDustEff(1);
		}
		if (this.ty <= 50 || this.clevel < 9)
		{
			return;
		}
		if (this.cgender == 0)
		{
			if (this.clevel >= 13)
			{
				if (GameCanvas.gameTick % 25 == 0)
				{
					ServerEffect.addServerEffect(114, this, 1);
				}
				if (GameCanvas.gameTick % 4 == 0)
				{
					ServerEffect.addServerEffect(132, this, 1);
				}
			}
			else if (GameCanvas.gameTick % 25 == 0)
			{
				ServerEffect.addServerEffect(173 + (this.clevel - 10), this, 1);
			}
		}
		if (this.cgender == 1)
		{
			if (GameCanvas.gameTick % 4 == 0)
			{
				ServerEffect.addServerEffect(132, this, 1);
			}
			if (this.clevel >= 13 && GameCanvas.gameTick % 7 == 0)
			{
				ServerEffect.addServerEffect(131, this, 1);
			}
		}
		if (this.cgender == 2)
		{
			if (GameCanvas.gameTick % 7 == 0)
			{
				ServerEffect.addServerEffect(131, this, 1);
			}
			if (this.clevel >= 13 && GameCanvas.gameTick % 25 == 0)
			{
				ServerEffect.addServerEffect(114, this, 1);
			}
		}
	}

	// Token: 0x06000112 RID: 274 RVA: 0x0000D708 File Offset: 0x0000B908
	public float getSoundVolumn()
	{
		if (this.me)
		{
			return 0.1f;
		}
		int num = Res.abs(global::Char.myChar.cx - this.cx);
		if (num >= 0 && num <= 50)
		{
			return 0.1f;
		}
		return 0.05f;
	}

	// Token: 0x06000113 RID: 275 RVA: 0x0000D750 File Offset: 0x0000B950
	public void updateCharRun()
	{
		int num = ((this.isMonkey != 1 || this.me) ? 1 : 1);
		if (this.cx >= GameScr.cmx && this.cx <= GameScr.cmx + GameCanvas.w)
		{
			if (this.isMonkey == 0)
			{
				SoundMn.gI().charRun(this.getSoundVolumn());
			}
			else
			{
				SoundMn.gI().monkeyRun(this.getSoundVolumn());
			}
		}
		this.ty = 0;
		this.isFreez = false;
		if (this.isCharge)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		int num2 = 0;
		if (!this.me && this.currentMovePoint != null)
		{
			num2 = global::Char.abs(this.cx - this.currentMovePoint.xEnd);
		}
		this.cp1++;
		if (this.cp1 >= 10)
		{
			this.cp1 = 0;
			this.cBonusSpeed = 0;
		}
		this.cf = (this.cp1 >> 1) + 2;
		if ((TileMap.tileTypeAtPixel(this.cx, this.cy - 1) & 64) == 64)
		{
			this.cx += this.cvx * num >> 1;
		}
		else
		{
			this.cx += this.cvx * num;
		}
		if (this.cdir == 1)
		{
			if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - this.chh, 4))
			{
				if (this.me)
				{
					this.cvx = 0;
					this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
				}
				else
				{
					this.stop();
				}
			}
		}
		else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - this.chh, 8))
		{
			if (this.me)
			{
				this.cvx = 0;
				this.cx = TileMap.tileXofPixel(this.cx - this.chw - 1) + (int)TileMap.size + this.chw;
			}
			else
			{
				this.stop();
			}
		}
		if (this.me)
		{
			if (this.cvx > 0)
			{
				this.cvx--;
			}
			else if (this.cvx < 0)
			{
				this.cvx++;
			}
			else
			{
				if (this.cx - this.cxSend != 0 && this.me)
				{
					Service.gI().charMove();
				}
				this.statusMe = 1;
				this.cBonusSpeed = 0;
			}
		}
		if ((TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) != 2)
		{
			if (this.me)
			{
				if (this.cx - this.cxSend != 0 || this.cy - this.cySend != 0)
				{
					Service.gI().charMove();
				}
				this.cf = 7;
				this.statusMe = 4;
				this.delayFall = 0;
				this.cvx = 3 * this.cdir;
				this.cp2 = 0;
			}
			else
			{
				this.stop();
			}
		}
		if (!this.me && this.currentMovePoint != null && global::Char.abs(this.cx - this.currentMovePoint.xEnd) > num2)
		{
			this.stop();
		}
		GameCanvas.gI().startDust(this.cdir, this.cx - (this.cdir << 3), this.cy);
		this.updateCharInBridge();
		this.addDustEff(2);
	}

	// Token: 0x06000114 RID: 276 RVA: 0x0000DAA4 File Offset: 0x0000BCA4
	internal void stop()
	{
		this.statusMe = 6;
		this.cp3 = 0;
		this.cvx = 0;
		this.cvy = 0;
		this.cp1 = (this.cp2 = 0);
	}

	// Token: 0x06000115 RID: 277 RVA: 0x0000DADD File Offset: 0x0000BCDD
	public static int abs(int i)
	{
		if (i > 0)
		{
			return i;
		}
		return -i;
	}

	// Token: 0x06000116 RID: 278 RVA: 0x0000DAE8 File Offset: 0x0000BCE8
	public void updateCharJump()
	{
		this.setMountIsStart();
		this.ty = 0;
		this.isFreez = false;
		if (this.isCharge)
		{
			this.isCharge = false;
			SoundMn.gI().taitaoPause();
			Service.gI().skill_not_focus(3);
		}
		this.addDustEff(3);
		this.cx += this.cvx;
		this.cy += this.cvy;
		if (this.cy < 0)
		{
			this.cy = 0;
			this.cvy = -1;
		}
		this.cvy++;
		if (this.cvy > 0)
		{
			this.cvy = 0;
		}
		if (!this.me && this.currentMovePoint != null)
		{
			int num = this.currentMovePoint.xEnd - this.cx;
			if (num > 0)
			{
				if (this.cvx > num)
				{
					this.cvx = num;
				}
				if (this.cvx < 0)
				{
					this.cvx = num;
				}
			}
			else if (num < 0)
			{
				if (this.cvx < num)
				{
					this.cvx = num;
				}
				if (this.cvx > 0)
				{
					this.cvx = num;
				}
			}
			else
			{
				this.cvx = num;
			}
		}
		if (this.cdir == 1)
		{
			if ((TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - 1) & 4) == 4 && this.cx <= TileMap.tileXofPixel(this.cx + this.chw) + 12)
			{
				this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
				this.cvx = 0;
			}
		}
		else if ((TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - 1) & 8) == 8 && this.cx >= TileMap.tileXofPixel(this.cx - this.chw) + 12)
		{
			this.cx = TileMap.tileXofPixel(this.cx + 24 - this.chw) + this.chw;
			this.cvx = 0;
		}
		if (this.cvy == 0)
		{
			if (!this.isAttFly)
			{
				if (this.me)
				{
					this.setCharFallFromJump();
				}
				else
				{
					this.stop();
				}
			}
			else
			{
				this.setCharFallFromJump();
			}
		}
		if (this.me && !global::Char.ischangingMap && this.isInWaypoint())
		{
			Service.gI().charMove();
			if (TileMap.isTrainingMap())
			{
				global::Char.ischangingMap = true;
				Service.gI().getMapOffline();
			}
			else
			{
				Service.gI().requestChangeMap();
			}
			global::Char.isLockKey = true;
			global::Char.ischangingMap = true;
			GameCanvas.clearKeyHold();
			GameCanvas.clearKeyPressed();
			InfoDlg.showWait();
			return;
		}
		if (this.statusMe != 16 && (TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192) || this.cy < 0))
		{
			this.statusMe = 4;
			this.cp1 = 0;
			this.cp2 = 0;
			this.cvy = 1;
			this.delayFall = 0;
			if (this.cy < 0)
			{
				this.cy = 0;
			}
			this.cy = TileMap.tileYofPixel(this.cy + 25);
			GameCanvas.clearKeyHold();
		}
		if (this.cp3 < 0)
		{
			this.cp3++;
		}
		this.cf = 7;
		if (!this.me && this.currentMovePoint != null && this.cy < this.currentMovePoint.yEnd)
		{
			this.stop();
		}
	}

	// Token: 0x06000117 RID: 279 RVA: 0x0000DE33 File Offset: 0x0000C033
	public bool checkInRangeJump(int x1, int xw1, int xmob, int y1, int yh1, int ymob)
	{
		return xmob <= xw1 && xmob >= x1 && ymob <= y1 && ymob >= yh1;
	}

	// Token: 0x06000118 RID: 280 RVA: 0x0000DE4C File Offset: 0x0000C04C
	public void setCharFallFromJump()
	{
		this.cyStartFall = this.cy;
		this.cp1 = 0;
		this.cp2 = 0;
		this.statusMe = 10;
		this.cvx = this.cdir << 2;
		this.cvy = 0;
		this.cy = TileMap.tileYofPixel(this.cy) + 12;
		if (this.me && (this.cx - this.cxSend != 0 || this.cy - this.cySend != 0) && (Res.abs(global::Char.myCharz().cx - global::Char.myCharz().cxSend) > 96 || Res.abs(global::Char.myCharz().cy - global::Char.myCharz().cySend) > 24))
		{
			Service.gI().charMove();
		}
	}

	// Token: 0x06000119 RID: 281 RVA: 0x0000DF10 File Offset: 0x0000C110
	public void updateCharFall()
	{
		if (this.holder)
		{
			return;
		}
		this.ty = 0;
		if (this.cy + 4 >= TileMap.pxh)
		{
			this.statusMe = 1;
			if (this.me)
			{
				SoundMn.gI().charFall();
			}
			this.cvx = (this.cvy = 0);
			this.cp3 = 0;
			return;
		}
		if (this.cy % 24 == 0 && (TileMap.tileTypeAtPixel(this.cx, this.cy) & 2) == 2)
		{
			this.delayFall = 0;
			if (this.me)
			{
				if (this.cy - this.cySend > 0)
				{
					Service.gI().charMove();
				}
				else if (this.cx - this.cxSend != 0 || this.cy - this.cySend < 0)
				{
					Service.gI().charMove();
				}
				this.cvx = (this.cvy = 0);
				this.cp1 = (this.cp2 = 0);
				this.statusMe = 1;
				this.cp3 = 0;
				return;
			}
			this.stop();
			this.cf = 0;
			GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
			GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
			this.addDustEff(1);
		}
		if (this.delayFall > 0)
		{
			this.delayFall--;
			if (this.delayFall % 10 > 5)
			{
				this.cy++;
				return;
			}
			this.cy--;
			return;
		}
		else
		{
			if (this.cvy < -4)
			{
				this.cf = 7;
			}
			else
			{
				this.cf = 12;
			}
			this.cx += this.cvx;
			if (!this.me && this.currentMovePoint != null)
			{
				int num = this.currentMovePoint.xEnd - this.cx;
				if (num > 0)
				{
					if (this.cvx > num)
					{
						this.cvx = num;
					}
					if (this.cvx < 0)
					{
						this.cvx = num;
					}
				}
				else if (num < 0)
				{
					if (this.cvx < num)
					{
						this.cvx = num;
					}
					if (this.cvx > 0)
					{
						this.cvx = num;
					}
				}
				else
				{
					this.cvx = num;
				}
			}
			this.cvy++;
			if (this.cvy > 8)
			{
				this.cvy = 8;
			}
			if (this.skillPaintRandomPaint == null)
			{
				this.cy += this.cvy;
			}
			if (this.cdir == 1)
			{
				if ((TileMap.tileTypeAtPixel(this.cx + this.chw, this.cy - 1) & 4) == 4 && this.cx <= TileMap.tileXofPixel(this.cx + this.chw) + 12)
				{
					this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
					this.cvx = 0;
				}
			}
			else if ((TileMap.tileTypeAtPixel(this.cx - this.chw, this.cy - 1) & 8) == 8 && this.cx >= TileMap.tileXofPixel(this.cx - this.chw) + 12)
			{
				this.cx = TileMap.tileXofPixel(this.cx + 24 - this.chw) + this.chw;
				this.cvx = 0;
			}
			if (this.cvy > 3 && (this.cyStartFall == 0 || this.cyStartFall <= TileMap.tileYofPixel(this.cy + 3)) && (TileMap.tileTypeAtPixel(this.cx, this.cy + 3) & 2) == 2)
			{
				if (this.me)
				{
					this.cyStartFall = 0;
					this.cvx = (this.cvy = 0);
					this.cp1 = (this.cp2 = 0);
					this.cy = TileMap.tileXofPixel(this.cy + 3);
					this.statusMe = 1;
					if (this.me)
					{
						SoundMn.gI().charFall();
					}
					this.cp3 = 0;
					GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
					GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
					this.addDustEff(1);
					if (this.cy - this.cySend > 0)
					{
						if (this.me)
						{
							Service.gI().charMove();
							return;
						}
					}
					else if ((this.cx - this.cxSend != 0 || this.cy - this.cySend < 0) && this.me)
					{
						Service.gI().charMove();
						return;
					}
				}
				else
				{
					this.stop();
					this.cy = TileMap.tileXofPixel(this.cy + 3);
					this.cf = 0;
					GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
					GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
					this.addDustEff(1);
					this.currentMovePoint = null;
				}
				return;
			}
			this.cf = 12;
			if (this.me)
			{
				bool flag = this.isAttack;
				return;
			}
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 1) & 2) == 2)
			{
				this.cf = 0;
			}
			if (this.currentMovePoint != null && this.cy > this.currentMovePoint.yEnd)
			{
				this.stop();
				this.cy = TileMap.tileXofPixel(this.cy + 3);
				this.currentMovePoint = null;
			}
			return;
		}
	}

	// Token: 0x0600011A RID: 282 RVA: 0x0000E46C File Offset: 0x0000C66C
	public void updateCharFly()
	{
		int num = ((this.isMonkey != 1 || this.me) ? 1 : 2);
		this.setMountIsStart();
		if (this.statusMe != 16 && (TileMap.tileTypeAt(this.cx, this.cy - this.ch + 24, 8192) || this.cy < 0))
		{
			if (this.cy - this.ch < 0)
			{
				this.cy = this.ch;
			}
			this.cf = 7;
			this.statusMe = 4;
			this.cvx = 0;
			this.cp2 = 0;
			this.currentMovePoint = null;
			return;
		}
		int num2 = this.cy;
		if (this.isHead_Fly(this.head))
		{
			if (GameCanvas.gameTick % 3 == 0)
			{
				this.cp1++;
			}
			if (this.cp1 > 4)
			{
				this.cp1 = 0;
			}
			this.cf = this.cp1 + 2;
		}
		else
		{
			this.cp1++;
			if (this.cp1 >= 9)
			{
				this.cp1 = 0;
				if (!this.me)
				{
					this.cvx = (this.cvy = 0);
				}
				this.cBonusSpeed = 0;
			}
			this.cf = 8;
			if (Res.abs(this.cvx) <= 4 && this.me)
			{
				if (this.currentMovePoint != null)
				{
					int num3 = global::Char.abs(this.cx - this.currentMovePoint.xEnd);
					int num4 = global::Char.abs(this.cy - this.currentMovePoint.yEnd);
					if (num3 > num4 * 10)
					{
						this.cf = 8;
					}
					else if (num3 > num4 && num3 > 48 && num4 > 32)
					{
						this.cf = 8;
					}
					else
					{
						this.cf = 7;
					}
				}
				else
				{
					if (this.cvy < 0)
					{
						this.cvy = 0;
					}
					if (this.cvy > 16)
					{
						this.cvy = 16;
					}
					this.cf = 7;
				}
			}
			if (!this.me)
			{
				if (global::Char.abs(this.cvx) < 2)
				{
					this.cvx = (this.cdir << 1) * num;
				}
				if (this.cvy != 0)
				{
					this.cf = 7;
				}
				if (global::Char.abs(this.cvx) <= 2)
				{
					this.cp2++;
					if (this.cp2 > 32)
					{
						this.statusMe = 4;
						this.cvx = 0;
						this.cvy = 0;
					}
				}
			}
		}
		if (this.cdir == 1)
		{
			if (TileMap.tileTypeAt(this.cx + this.chw, this.cy - 1, 4))
			{
				this.cvx = 0;
				this.cx = TileMap.tileXofPixel(this.cx + this.chw) - this.chw;
				if (this.cvy == 0)
				{
					this.currentMovePoint = null;
				}
			}
		}
		else if (TileMap.tileTypeAt(this.cx - this.chw - 1, this.cy - 1, 8))
		{
			this.cvx = 0;
			this.cx = TileMap.tileXofPixel(this.cx - this.chw - 1) + (int)TileMap.size + this.chw;
			if (this.cvy == 0)
			{
				this.currentMovePoint = null;
			}
		}
		this.cx += this.cvx * num;
		this.cy += this.cvy * num;
		if (!this.isMount && num2 - this.cy == 0)
		{
			this.ty++;
			this.wt++;
			this.fy += ((!this.wy) ? 1 : (-1));
			if (this.wt == 10)
			{
				this.wt = 0;
				this.wy = !this.wy;
			}
			if (this.ty > 20)
			{
				this.delayFall = 10;
				if (GameCanvas.gameTick % 3 == 0)
				{
					ServerEffect.addServerEffect(111, this.cx + ((this.cdir != 1) ? 27 : (-17)), this.cy + this.fy + 13, 1, (this.cdir != 1) ? 2 : 0);
				}
			}
		}
		if (!this.me)
		{
			return;
		}
		if (this.cvx > 0)
		{
			this.cvx--;
		}
		else if (this.cvx < 0)
		{
			this.cvx++;
		}
		else if (this.cvy == 0)
		{
			this.statusMe = 4;
			this.checkDelayFallIfTooHigh();
			Service.gI().charMove();
		}
		if ((TileMap.tileTypeAtPixel(this.cx, this.cy + 20) & 2) == 2 || (TileMap.tileTypeAtPixel(this.cx, this.cy + 40) & 2) == 2)
		{
			if (this.cvy == 0)
			{
				this.delayFall = 0;
			}
			this.cyStartFall = 0;
			this.cvx = (this.cvy = 0);
			this.cp1 = (this.cp2 = 0);
			this.statusMe = 4;
			this.addDustEff(3);
		}
		if (global::Char.abs(this.cx - this.cxSend) > 96 || global::Char.abs(this.cy - this.cySend) > 24)
		{
			Service.gI().charMove();
		}
	}

	// Token: 0x0600011B RID: 283 RVA: 0x0000E970 File Offset: 0x0000CB70
	internal bool isHead_Fly(int head2)
	{
		if (global::Char.Arr_Head_FlyMove.Length != 0)
		{
			for (int i = 0; i < global::Char.Arr_Head_FlyMove.Length; i++)
			{
				if ((int)global::Char.Arr_Head_FlyMove[i] == head2)
				{
					return true;
				}
			}
		}
		return false;
	}

	// Token: 0x0600011C RID: 284 RVA: 0x0000E9A4 File Offset: 0x0000CBA4
	public void setMount(int cid, int ctrans, int cgender)
	{
		this.idcharMount = cid;
		this.transMount = ctrans;
		this.genderMount = cgender;
		this.speedMount = 30;
		if (this.transMount < 0)
		{
			this.transMount = 0;
			this.xMount = GameScr.cmx + GameCanvas.w + 50;
			this.dxMount = -19;
		}
		else if (this.transMount == 1)
		{
			this.transMount = 2;
			this.xMount = GameScr.cmx - 100;
			this.dxMount = -33;
		}
		this.dyMount = -17;
		this.yMount = this.cy;
		this.frameMount = 0;
		this.frameNewMount = 0;
		this.isMount = false;
		this.isEndMount = false;
	}

	// Token: 0x0600011D RID: 285 RVA: 0x0000EA54 File Offset: 0x0000CC54
	public void updateMount()
	{
		this.frameMount++;
		if (this.frameMount > this.FrameMount.Length - 1)
		{
			this.frameMount = 0;
		}
		this.frameNewMount++;
		if (this.frameNewMount > 1000)
		{
			this.frameNewMount = 0;
		}
		if (this.isStartMount && !this.isMount)
		{
			this.yMount = this.cy;
			if (this.transMount == 0)
			{
				if (this.xMount - this.cx >= this.speedMount)
				{
					this.xMount -= this.speedMount;
					return;
				}
				this.xMount = this.cx;
				this.isMount = true;
				this.isEndMount = false;
				return;
			}
			else if (this.transMount == 2)
			{
				if (this.cx - this.xMount >= this.speedMount)
				{
					this.xMount += this.speedMount;
					return;
				}
				this.xMount = this.cx;
				this.isMount = true;
				this.isEndMount = false;
				return;
			}
		}
		else
		{
			if (this.isMount)
			{
				if (this.statusMe == 14 || this.ySd - this.cy < 24)
				{
					this.setMountIsEnd();
				}
				if (this.cp1 % 15 < 5)
				{
					this.cf = 0;
				}
				else
				{
					this.cf = 1;
				}
				this.transMount = this.cdir;
				this.updateSuperEff();
				if (this.transMount < 0)
				{
					this.transMount = 0;
					this.dxMount = -19;
				}
				else if (this.transMount == 1)
				{
					this.transMount = 2;
					this.dxMount = -31;
					if (this.isEventMount)
					{
						this.dxMount = -38;
					}
				}
				if (this.skillInfoPaint() != null)
				{
					this.dyMount = -15;
				}
				else
				{
					this.dyMount = -17;
				}
				this.yMount = this.cy;
				this.xMount = this.cx;
				return;
			}
			if (this.isEndMount)
			{
				if (this.transMount == 0)
				{
					if (this.xMount > GameScr.cmx - 100)
					{
						this.xMount -= 20;
						return;
					}
					this.isStartMount = false;
					this.isMount = false;
					this.isEndMount = false;
					return;
				}
				else if (this.transMount == 2)
				{
					if (this.xMount < GameScr.cmx + GameCanvas.w + 50)
					{
						this.xMount += 20;
						return;
					}
					this.isStartMount = false;
					this.isMount = false;
					this.isEndMount = false;
					return;
				}
			}
			else if (!this.isStartMount || !this.isMount || !this.isEndMount)
			{
				this.xMount = GameScr.cmx - 100;
				this.yMount = GameScr.cmy - 100;
			}
		}
	}

	// Token: 0x0600011E RID: 286 RVA: 0x0000ECFC File Offset: 0x0000CEFC
	public void getMountData()
	{
		if (Mob.arrMobTemplate[50].data == null)
		{
			Mob.arrMobTemplate[50].data = new EffectData();
			string text = "/Mob/" + 50.ToString();
			if (MyStream.readFile(text) != null)
			{
				Mob.arrMobTemplate[50].data.readData(text + "/data");
				Mob.arrMobTemplate[50].data.img = GameCanvas.loadImage(text + "/img.png");
			}
			else
			{
				Service.gI().requestModTemplate(50);
			}
			Mob.lastMob.addElement(50.ToString() + string.Empty);
		}
	}

	// Token: 0x0600011F RID: 287 RVA: 0x0000EDB5 File Offset: 0x0000CFB5
	public void checkFrameTick(int[] array)
	{
		this.t++;
		if (this.t > array.Length - 1)
		{
			this.t = 0;
		}
		this.fM = array[this.t];
	}

	// Token: 0x06000120 RID: 288 RVA: 0x0000EDE8 File Offset: 0x0000CFE8
	public void paintMount1(mGraphics g)
	{
		if (this.xMount <= GameScr.cmx || this.xMount >= GameScr.cmx + GameCanvas.w)
		{
			return;
		}
		if (this.me)
		{
			if (!this.isEndMount && !this.isStartMount && !this.isMount)
			{
				return;
			}
			if (this.idMount >= global::Char.ID_NEW_MOUNT)
			{
				FrameImage fraImage = mSystem.getFraImage(this.strMount + ((int)(this.idMount - global::Char.ID_NEW_MOUNT)).ToString() + "_0");
				if (fraImage != null)
				{
					fraImage.drawFrame(this.frameNewMount / 2 % fraImage.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
					return;
				}
			}
			else
			{
				if (this.isSpeacialMount)
				{
					return;
				}
				if (this.isEventMount)
				{
					g.drawRegion(global::Char.imgEventMountWing, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					return;
				}
				if (this.genderMount == 2)
				{
					if (!this.isMountVip)
					{
						g.drawRegion(global::Char.imgMount_XD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					g.drawRegion(global::Char.imgMount_XD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					return;
				}
				else if (this.genderMount == 1)
				{
					if (!this.isMountVip)
					{
						g.drawRegion(global::Char.imgMount_NM, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					g.drawRegion(global::Char.imgMount_NM_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					return;
				}
			}
		}
		else
		{
			if (this.me)
			{
				return;
			}
			if (this.idMount >= global::Char.ID_NEW_MOUNT)
			{
				FrameImage fraImage2 = mSystem.getFraImage(this.strMount + ((int)(this.idMount - global::Char.ID_NEW_MOUNT)).ToString() + "_0");
				if (fraImage2 != null)
				{
					fraImage2.drawFrame(this.frameNewMount / 2 % fraImage2.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
					return;
				}
			}
			else
			{
				if (this.isSpeacialMount)
				{
					return;
				}
				if (this.isEventMount)
				{
					g.drawRegion(global::Char.imgEventMountWing, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					return;
				}
				if (!this.isMount)
				{
					return;
				}
				if (this.genderMount == 2)
				{
					if (!this.isMountVip)
					{
						g.drawRegion(global::Char.imgMount_XD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					g.drawRegion(global::Char.imgMount_XD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					return;
				}
				else if (this.genderMount == 1)
				{
					if (!this.isMountVip)
					{
						g.drawRegion(global::Char.imgMount_NM, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					g.drawRegion(global::Char.imgMount_NM_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
				}
			}
		}
	}

	// Token: 0x06000121 RID: 289 RVA: 0x0000F274 File Offset: 0x0000D474
	public void paintMount2(mGraphics g)
	{
		if (this.xMount <= GameScr.cmx || this.xMount >= GameScr.cmx + GameCanvas.w)
		{
			return;
		}
		if (this.me)
		{
			if (!this.isEndMount && !this.isStartMount && !this.isMount)
			{
				return;
			}
			if (this.idMount >= global::Char.ID_NEW_MOUNT)
			{
				FrameImage fraImage = mSystem.getFraImage(this.strMount + ((int)(this.idMount - global::Char.ID_NEW_MOUNT)).ToString() + "_1");
				if (fraImage != null)
				{
					fraImage.drawFrame(this.frameNewMount / 2 % fraImage.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
					return;
				}
			}
			else if (this.isSpeacialMount)
			{
				this.checkFrameTick(this.move);
				if (Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null)
				{
					Mob.arrMobTemplate[50].data.paintFrame(g, this.fM, this.xMount + ((this.cdir != 1) ? 8 : (-8)), this.yMount + 35, (this.cdir != 1) ? 1 : 0, 0);
					return;
				}
				this.getMountData();
				return;
			}
			else
			{
				if (this.isEventMount)
				{
					g.drawRegion(global::Char.imgEventMount, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					return;
				}
				if (this.genderMount == 0)
				{
					if (!this.isMountVip)
					{
						g.drawRegion(global::Char.imgMount_TD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					g.drawRegion(global::Char.imgMount_TD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					return;
				}
				else if (this.genderMount == 1)
				{
					if (!this.isMountVip)
					{
						g.drawRegion(global::Char.imgMount_NM_1, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					g.drawRegion(global::Char.imgMount_NM_1_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					return;
				}
			}
		}
		else
		{
			if (this.me)
			{
				return;
			}
			if (this.idMount >= global::Char.ID_NEW_MOUNT)
			{
				FrameImage fraImage2 = mSystem.getFraImage(this.strMount + ((int)(this.idMount - global::Char.ID_NEW_MOUNT)).ToString() + "_1");
				if (fraImage2 != null)
				{
					fraImage2.drawFrame(this.frameNewMount / 2 % fraImage2.nFrame, this.xMount, this.yMount + this.fy, this.transMount, 3, g);
				}
				return;
			}
			if (this.isSpeacialMount)
			{
				this.checkFrameTick(this.move);
				if (Mob.arrMobTemplate[50] != null && Mob.arrMobTemplate[50].data != null)
				{
					Mob.arrMobTemplate[50].data.paintFrame(g, this.fM, this.xMount + ((this.cdir != 1) ? 8 : (-8)), this.yMount + 35, (this.cdir != 1) ? 1 : 0, 0);
					return;
				}
				this.getMountData();
				return;
			}
			else
			{
				if (this.isEventMount)
				{
					g.drawRegion(global::Char.imgEventMount, 0, (int)(this.FrameMount[this.frameMount] * 60), 60, 60, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
				}
				if (!this.isMount)
				{
					return;
				}
				if (this.genderMount == 0)
				{
					if (!this.isMountVip)
					{
						g.drawRegion(global::Char.imgMount_TD, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					g.drawRegion(global::Char.imgMount_TD_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
					return;
				}
				else if (this.genderMount == 1)
				{
					if (!this.isMountVip)
					{
						g.drawRegion(global::Char.imgMount_NM_1, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
						return;
					}
					g.drawRegion(global::Char.imgMount_NM_1_VIP, 0, (int)(this.FrameMount[this.frameMount] * 40), 50, 40, this.transMount, this.xMount + this.dxMount, this.yMount + this.dyMount + this.fy, 0);
				}
			}
		}
	}

	// Token: 0x06000122 RID: 290 RVA: 0x0000F7DC File Offset: 0x0000D9DC
	public void setMountIsStart()
	{
		if (this.me)
		{
			this.isHaveMount = this.checkHaveMount();
			if (TileMap.isVoDaiMap())
			{
				this.isHaveMount = false;
			}
		}
		if (this.isHaveMount)
		{
			if (this.ySd - this.cy <= 20)
			{
				this.xChar = this.cx;
			}
			if (this.xdis < 100)
			{
				this.xdis = Res.abs(this.xChar - this.cx);
			}
			if (this.xdis >= 70 && this.ySd - this.cy > 30 && !this.isStartMount && !this.isEndMount)
			{
				this.setMount(this.charID, this.cdir, this.cgender);
				this.isStartMount = true;
			}
		}
	}

	// Token: 0x06000123 RID: 291 RVA: 0x0000F89F File Offset: 0x0000DA9F
	public void setMountIsEnd()
	{
		if (this.ySd - this.cy < 24 && !this.isEndMount)
		{
			this.isStartMount = false;
			this.isMount = false;
			this.isEndMount = true;
			this.xdis = 0;
		}
	}

	// Token: 0x06000124 RID: 292 RVA: 0x0000F8D8 File Offset: 0x0000DAD8
	public bool checkHaveMount()
	{
		bool flag = false;
		short num = -1;
		Item[] array = this.arrItemBody;
		for (int i = 0; i < array.Length; i++)
		{
			if (array[i] != null && (array[i].template.type == 24 || array[i].template.type == 23))
			{
				num = ((array[i].template.part < 0) ? array[i].template.id : (global::Char.ID_NEW_MOUNT + array[i].template.part));
				flag = true;
				break;
			}
		}
		this.isMountVip = false;
		this.isSpeacialMount = false;
		this.isEventMount = false;
		this.idMount = -1;
		if (num == 349 || num == 350 || num == 351)
		{
			this.isMountVip = true;
		}
		else if (num == 396)
		{
			this.isEventMount = true;
		}
		else if (num == 532)
		{
			this.isSpeacialMount = true;
		}
		else if (num >= global::Char.ID_NEW_MOUNT)
		{
			this.idMount = num;
		}
		return flag;
	}

	// Token: 0x06000125 RID: 293 RVA: 0x0000F9CC File Offset: 0x0000DBCC
	internal void checkDelayFallIfTooHigh()
	{
		bool flag = true;
		for (int i = 0; i < 150; i += 24)
		{
			if ((TileMap.tileTypeAtPixel(this.cx, this.cy + i) & 2) == 2 || this.cy + i > TileMap.tmh * (int)TileMap.size - 24)
			{
				flag = false;
				break;
			}
		}
		if (flag)
		{
			this.delayFall = 40;
		}
	}

	// Token: 0x06000126 RID: 294 RVA: 0x0000FA2B File Offset: 0x0000DC2B
	public void setDefaultPart()
	{
		this.setDefaultWeapon();
		this.setDefaultBody();
		this.setDefaultLeg();
	}

	// Token: 0x06000127 RID: 295 RVA: 0x0000FA3F File Offset: 0x0000DC3F
	public void setDefaultWeapon()
	{
		if (this.cgender == 0)
		{
			this.wp = 0;
		}
	}

	// Token: 0x06000128 RID: 296 RVA: 0x0000FA50 File Offset: 0x0000DC50
	public void setDefaultBody()
	{
		if (this.cgender == 0)
		{
			this.body = 57;
			return;
		}
		if (this.cgender == 1)
		{
			this.body = 59;
			return;
		}
		if (this.cgender == 2)
		{
			this.body = 57;
		}
	}

	// Token: 0x06000129 RID: 297 RVA: 0x0000FA86 File Offset: 0x0000DC86
	public void setDefaultLeg()
	{
		if (this.cgender == 0)
		{
			this.leg = 58;
			return;
		}
		if (this.cgender == 1)
		{
			this.leg = 60;
			return;
		}
		if (this.cgender == 2)
		{
			this.leg = 58;
		}
	}

	// Token: 0x0600012A RID: 298 RVA: 0x0000FABC File Offset: 0x0000DCBC
	public bool isSelectingSkillUseAlone()
	{
		return this.myskill != null && this.myskill.template.isUseAlone();
	}

	// Token: 0x0600012B RID: 299 RVA: 0x0000FAD8 File Offset: 0x0000DCD8
	public bool isUseSkillSpec()
	{
		return this.myskill != null && this.myskill.template.isSkillSpec();
	}

	// Token: 0x0600012C RID: 300 RVA: 0x0000FAF4 File Offset: 0x0000DCF4
	public bool isSelectingSkillBuffToPlayer()
	{
		return this.myskill != null && this.myskill.template.isBuffToPlayer();
	}

	// Token: 0x0600012D RID: 301 RVA: 0x0000FB10 File Offset: 0x0000DD10
	public bool isUseChargeSkill()
	{
		return !this.isUseSkillAfterCharge && this.myskill != null && (this.myskill.template.id == 10 || this.myskill.template.id == 11);
	}

	// Token: 0x0600012E RID: 302 RVA: 0x0000FB50 File Offset: 0x0000DD50
	public void setSkillPaint(SkillPaint skillPaint, int sType)
	{
		this.hasSendAttack = false;
		if (this.stone || (this.me && this.myskill.template.id == 9 && this.cHP <= this.cHPFull / 10))
		{
			return;
		}
		if (this.me)
		{
			if (this.mobFocus == null && this.charFocus == null)
			{
				this.stopUseChargeSkill();
			}
			if (this.mobFocus != null && (this.mobFocus.status == 1 || this.mobFocus.status == 0))
			{
				this.stopUseChargeSkill();
			}
			if (this.charFocus != null && (this.charFocus.statusMe == 14 || this.charFocus.statusMe == 5))
			{
				this.stopUseChargeSkill();
			}
			if ((this.myskill.template.id == 23 && ((this.charFocus != null && this.charFocus.holdEffID != 0) || (this.mobFocus != null && this.mobFocus.holdEffID != 0) || this.holdEffID != 0)) || this.sleepEff || this.blindEff)
			{
				return;
			}
		}
		Res.outz("skill id= " + skillPaint.id.ToString());
		if ((this.me && this.dart != null) || TileMap.isOfflineMap())
		{
			return;
		}
		long num = mSystem.currentTimeMillis();
		if (this.me)
		{
			if (this.isSelectingSkillBuffToPlayer() && this.charFocus == null)
			{
				return;
			}
			if (num - this.myskill.lastTimeUseThisSkill < (long)this.myskill.coolDown)
			{
				this.myskill.paintCanNotUseSkill = true;
				return;
			}
			this.myskill.lastTimeUseThisSkill = num;
			if (this.myskill.template.manaUseType == 2)
			{
				this.cMP = 1;
			}
			else if (this.myskill.template.manaUseType != 1)
			{
				this.cMP -= this.myskill.manaUse;
			}
			else
			{
				this.cMP -= this.myskill.manaUse * this.cMPFull / 100;
			}
			global::Char.myCharz().cStamina--;
			GameScr.gI().isInjureMp = true;
			GameScr.gI().twMp = 0;
			if (this.cMP < 0)
			{
				this.cMP = 0;
			}
		}
		if (this.me)
		{
			if (this.myskill.template.id == 10)
			{
				Service.gI().skill_not_focus(4);
			}
			if (this.myskill.template.id == 11)
			{
				Service.gI().skill_not_focus(4);
			}
			if (this.myskill.template.id == 7)
			{
				SoundMn.gI().hoisinh();
			}
			if (this.myskill.template.id == 6)
			{
				Service.gI().skill_not_focus(0);
				GameScr.gI().isUseFreez = true;
				SoundMn.gI().thaiduonghasan();
			}
			if (this.myskill.template.id == 8)
			{
				if (!this.isCharge)
				{
					SoundMn.gI().taitaoPause();
					Service.gI().skill_not_focus(1);
					this.isCharge = true;
					this.last = (this.cur = mSystem.currentTimeMillis());
				}
				else
				{
					Service.gI().skill_not_focus(3);
					this.isCharge = false;
					SoundMn.gI().taitaoPause();
				}
			}
			if (this.myskill.template.id == 13)
			{
				if (this.isMonkey != 0)
				{
					GameScr.gI().auto = 0;
					return;
				}
				if (!this.isCreateDark)
				{
					SoundMn.gI().gong();
					Service.gI().skill_not_focus(6);
					this.chargeCount = 0;
					this.isWaitMonkey = true;
				}
				return;
			}
			else
			{
				if (this.myskill.template.id == 14)
				{
					SoundMn.gI().gong();
					Service.gI().skill_not_focus(7);
					this.useChargeSkill(true);
				}
				if (this.myskill.template.id == 21)
				{
					Service.gI().skill_not_focus(10);
					return;
				}
				if (this.myskill.template.id == 12)
				{
					Service.gI().skill_not_focus(8);
				}
				if (this.myskill.template.id == 19)
				{
					Service.gI().skill_not_focus(9);
					return;
				}
			}
		}
		if (this.isMonkey == 1 && skillPaint.id >= 35 && skillPaint.id <= 41)
		{
			skillPaint = GameScr.sks[106];
		}
		if (skillPaint.id >= 128 && skillPaint.id <= 134)
		{
			skillPaint = GameScr.sks[skillPaint.id - 65];
			if (this.charFocus != null)
			{
				this.cx = this.charFocus.cx;
				this.cy = this.charFocus.cy;
				this.currentMovePoint = null;
			}
			if (this.mobFocus != null)
			{
				this.cx = this.mobFocus.x;
				this.cy = this.mobFocus.y;
				this.currentMovePoint = null;
			}
			ServerEffect.addServerEffect(60, this.cx, this.cy, 1);
			this.telePortSkill = true;
		}
		if (skillPaint.id >= 107 && skillPaint.id <= 113)
		{
			skillPaint = GameScr.sks[skillPaint.id - 44];
			EffecMn.addEff(new Effect(23, this.cx, this.cy + this.ch / 2, 3, 2, 1));
		}
		this.setAutoSkillPaint(skillPaint, sType);
	}

	// Token: 0x0600012F RID: 303 RVA: 0x000100A0 File Offset: 0x0000E2A0
	public void useSkillNotFocus()
	{
		GameScr.gI().auto = 0;
		global::Char.myCharz().setSkillPaint(GameScr.sks[(int)global::Char.myCharz().myskill.skillId], (!TileMap.tileTypeAt(global::Char.myCharz().cx, global::Char.myCharz().cy, 2)) ? 1 : 0);
	}

	// Token: 0x06000130 RID: 304 RVA: 0x000100F8 File Offset: 0x0000E2F8
	public void sendUseChargeSkill()
	{
		if (this.me && (this.isFreez || this.isUsePlane))
		{
			GameScr.gI().auto = 0;
			return;
		}
		long num = mSystem.currentTimeMillis();
		if (this.me && num - this.myskill.lastTimeUseThisSkill < (long)this.myskill.coolDown)
		{
			this.myskill.paintCanNotUseSkill = true;
			return;
		}
		if (this.myskill.template.id == 10)
		{
			this.useChargeSkill(false);
		}
		if (this.myskill.template.id == 11)
		{
			this.useChargeSkill(true);
		}
	}

	// Token: 0x06000131 RID: 305 RVA: 0x00010198 File Offset: 0x0000E398
	public void stopUseChargeSkill()
	{
		this.isFlyAndCharge = false;
		this.isStandAndCharge = false;
		this.isUseSkillAfterCharge = false;
		this.isCreateDark = false;
		if (this.me && this.statusMe != 14 && this.statusMe != 5)
		{
			this.isLockMove = false;
		}
		GameScr.gI().auto = 0;
	}

	// Token: 0x06000132 RID: 306 RVA: 0x000101F0 File Offset: 0x0000E3F0
	public void useChargeSkill(bool isGround)
	{
		if (this.isCreateDark)
		{
			return;
		}
		GameScr.gI().auto = 0;
		if (!isGround)
		{
			if (!this.isFlyAndCharge)
			{
				if (this.me)
				{
					GameScr.gI().auto = 0;
					this.isLockMove = true;
					Service.gI().skill_not_focus(4);
				}
				this.isUseSkillAfterCharge = false;
				this.chargeCount = 0;
				this.isFlyAndCharge = true;
				this.posDisY = 0;
				this.seconds = 50000;
				this.isFlying = TileMap.tileTypeAt(this.cx, this.cy, 2);
			}
			return;
		}
		if (this.isStandAndCharge)
		{
			return;
		}
		this.chargeCount = 0;
		this.seconds = 50000;
		this.posDisY = 0;
		this.last = mSystem.currentTimeMillis();
		if (this.me)
		{
			this.isLockMove = true;
			if (this.cgender == 1)
			{
				Service.gI().skill_not_focus(4);
			}
			if (TileMap.mapID == 170 && this.cgender != 1)
			{
				Service.gI().skill_not_focus(4);
			}
		}
		if (this.cgender == 1)
		{
			SoundMn.gI().gongName();
		}
		if (TileMap.mapID == 170 && this.cgender != 1)
		{
			SoundMn.gI().gongName();
		}
		this.isStandAndCharge = true;
	}

	// Token: 0x06000133 RID: 307 RVA: 0x00010330 File Offset: 0x0000E530
	public void setAutoSkillPaint(SkillPaint skillPaint, int sType)
	{
		this.skillPaint = skillPaint;
		Res.outz("set auto skill " + ((skillPaint == null) ? "null" : "ko null"));
		if (skillPaint.id >= 0 && skillPaint.id <= 6)
		{
			int num = Res.random(0, skillPaint.id + 4) - 1;
			if (num < 0)
			{
				num = 0;
			}
			if (num > 6)
			{
				num = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num];
		}
		else if (skillPaint.id >= 14 && skillPaint.id <= 20)
		{
			int num2 = Res.random(0, skillPaint.id - 14 + 4) - 1;
			if (num2 < 0)
			{
				num2 = 0;
			}
			if (num2 > 6)
			{
				num2 = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num2 + 14];
		}
		else if (skillPaint.id >= 28 && skillPaint.id <= 34)
		{
			int num3 = Res.random(0, ((this.isMonkey != 1) ? skillPaint.id : 105) - ((this.isMonkey != 1) ? 28 : 105) + 4) - 1;
			if (num3 < 0)
			{
				num3 = 0;
			}
			if (num3 > 6)
			{
				num3 = 6;
			}
			if (this.isMonkey == 1)
			{
				num3 = 0;
			}
			this.skillPaintRandomPaint = GameScr.sks[num3 + ((this.isMonkey != 1) ? 28 : 105)];
		}
		else if (skillPaint.id >= 63 && skillPaint.id <= 69)
		{
			int num4 = Res.random(0, skillPaint.id - 63 + 4) - 1;
			if (num4 < 0)
			{
				num4 = 0;
			}
			if (num4 > 6)
			{
				num4 = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num4 + 63];
		}
		else if (skillPaint.id >= 107 && skillPaint.id <= 109)
		{
			int num5 = Res.random(0, skillPaint.id - 107 + 4) - 1;
			if (num5 < 0)
			{
				num5 = 0;
			}
			if (num5 > 6)
			{
				num5 = 6;
			}
			this.skillPaintRandomPaint = GameScr.sks[num5 + 107];
		}
		else
		{
			this.skillPaintRandomPaint = skillPaint;
		}
		this.sType = sType;
		this.indexSkill = 0;
		this.i0 = (this.i1 = (this.i2 = (this.dx0 = (this.dx1 = (this.dx2 = (this.dy0 = (this.dy1 = (this.dy2 = 0))))))));
		this.eff0 = null;
		this.eff1 = null;
		this.eff2 = null;
		this.cvy = 0;
	}

	// Token: 0x06000134 RID: 308 RVA: 0x00010589 File Offset: 0x0000E789
	public SkillInfoPaint[] skillInfoPaint()
	{
		if (this.skillPaint == null)
		{
			return null;
		}
		if (this.skillPaintRandomPaint == null)
		{
			return null;
		}
		if (this.sType == 0)
		{
			return this.skillPaintRandomPaint.skillStand;
		}
		return this.skillPaintRandomPaint.skillfly;
	}

	// Token: 0x06000135 RID: 309 RVA: 0x000105C0 File Offset: 0x0000E7C0
	public void setAttack()
	{
		if (this.me)
		{
			SkillPaint skillPaint = this.skillPaintRandomPaint;
			if (this.dart != null)
			{
				skillPaint = this.dart.skillPaint;
			}
			if (skillPaint == null)
			{
				return;
			}
			MyVector myVector = new MyVector();
			MyVector myVector2 = new MyVector();
			if (this.charFocus != null)
			{
				myVector2.addElement(this.charFocus);
			}
			else if (this.mobFocus != null)
			{
				myVector.addElement(this.mobFocus);
			}
			this.effPaints = new EffectPaint[myVector.size() + myVector2.size()];
			for (int i = 0; i < myVector.size(); i++)
			{
				this.effPaints[i] = new EffectPaint();
				this.effPaints[i].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
				if (!this.isSelectingSkillUseAlone())
				{
					this.effPaints[i].eMob = (Mob)myVector.elementAt(i);
				}
			}
			for (int j = 0; j < myVector2.size(); j++)
			{
				this.effPaints[j + myVector.size()] = new EffectPaint();
				this.effPaints[j + myVector.size()].effCharPaint = GameScr.efs[skillPaint.effectHappenOnMob - 1];
				this.effPaints[j + myVector.size()].eChar = (global::Char)myVector2.elementAt(j);
			}
			int num = 0;
			if (this.mobFocus != null)
			{
				num = 1;
			}
			else if (this.charFocus != null)
			{
				num = 2;
			}
			if (myVector.size() == 0 && myVector2.size() == 0)
			{
				this.stopUseChargeSkill();
			}
			if (this.me && !this.isSelectingSkillUseAlone() && !this.hasSendAttack)
			{
				Service.gI().sendPlayerAttack(myVector, myVector2, num);
				this.hasSendAttack = true;
			}
			return;
		}
		else
		{
			SkillPaint skillPaint2 = this.skillPaintRandomPaint;
			if (this.dart != null)
			{
				skillPaint2 = this.dart.skillPaint;
			}
			if (skillPaint2 == null)
			{
				return;
			}
			if (this.attMobs != null)
			{
				this.effPaints = new EffectPaint[this.attMobs.Length];
				for (int k = 0; k < this.attMobs.Length; k++)
				{
					this.effPaints[k] = new EffectPaint();
					this.effPaints[k].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
					this.effPaints[k].eMob = this.attMobs[k];
				}
				this.attMobs = null;
				return;
			}
			if (this.attChars != null)
			{
				this.effPaints = new EffectPaint[this.attChars.Length];
				for (int l = 0; l < this.attChars.Length; l++)
				{
					this.effPaints[l] = new EffectPaint();
					this.effPaints[l].effCharPaint = GameScr.efs[skillPaint2.effectHappenOnMob - 1];
					this.effPaints[l].eChar = this.attChars[l];
				}
				this.attChars = null;
			}
			return;
		}
	}

	// Token: 0x06000136 RID: 310 RVA: 0x00010889 File Offset: 0x0000EA89
	public bool isOutX()
	{
		return this.cx < GameScr.cmx || this.cx > GameScr.cmx + GameScr.gW;
	}

	// Token: 0x06000137 RID: 311 RVA: 0x000108B0 File Offset: 0x0000EAB0
	public bool isPaint()
	{
		return this.cy >= GameScr.cmy && this.cy <= GameScr.cmy + GameScr.gH + 30 && !this.isOutX() && !this.isSetPos && !this.isFusion;
	}

	// Token: 0x06000138 RID: 312 RVA: 0x00010903 File Offset: 0x0000EB03
	public void createShadow(int x, int y, int life)
	{
		this.shadowX = x;
		this.shadowY = y;
		this.shadowLife = life;
	}

	// Token: 0x06000139 RID: 313 RVA: 0x0001091A File Offset: 0x0000EB1A
	public void setMabuHold(bool m)
	{
		this.isMabuHold = m;
	}

	// Token: 0x0600013A RID: 314 RVA: 0x00010924 File Offset: 0x0000EB24
	public virtual void paint(mGraphics g)
	{
		if (this.isHide)
		{
			return;
		}
		if (this.isMafuba)
		{
			this.paintCharWithoutSkill(g);
			return;
		}
		if (this.isMabuHold)
		{
			if (this.cmtoChar)
			{
				GameScr.cmtoX = this.cx - GameScr.gW2;
				GameScr.cmtoY = this.cy - GameScr.gH23;
				if (!GameCanvas.isTouchControl)
				{
					GameScr.cmtoX += GameScr.gW6 * this.cdir;
					return;
				}
			}
		}
		else
		{
			if (!this.isPaint() || (!this.me && GameScr.notPaint))
			{
				return;
			}
			if (this.petFollow != null)
			{
				this.petFollow.paint(g);
			}
			this.paintMount1(g);
			if ((TileMap.isInAirMap() && this.cy >= TileMap.pxh - 48) || this.isTeleport)
			{
				return;
			}
			if (this.holder && GameCanvas.gameTick % 2 == 0)
			{
				g.setColor(16185600);
				if (this.charHold != null)
				{
					g.drawLine(this.cx, this.cy - this.ch / 2, this.charHold.cx, this.charHold.cy - this.charHold.ch / 2);
				}
				if (this.mobHold != null)
				{
					g.drawLine(this.cx, this.cy - this.ch / 2, this.mobHold.x, this.mobHold.y - this.mobHold.h / 2);
				}
			}
			this.paintSuperEffBehind(g);
			this.paintAuraBehind(g);
			this.paintEffBehind(g);
			this.paintEff_Lvup_behind(g);
			this.paintEff_Pet(g);
			if (this.shadowLife > 0)
			{
				if (GameCanvas.gameTick % 2 == 0)
				{
					this.paintCharBody(g, this.shadowX, this.shadowY, this.cdir, 25, true);
				}
				else if (this.shadowLife > 5)
				{
					this.paintCharBody(g, this.shadowX, this.shadowY, this.cdir, 7, true);
				}
			}
			if (!this.isPaint() && this.skillPaint != null && (this.skillPaint.id < 70 || this.skillPaint.id > 76) && (this.skillPaint.id < 77 || this.skillPaint.id > 83))
			{
				if (this.skillPaint != null)
				{
					this.indexSkill = this.skillInfoPaint().Length;
					this.skillPaint = null;
				}
				this.effPaints = null;
				this.eff = null;
				this.effTask = null;
				this.indexEff = -1;
				this.indexEffTask = -1;
				return;
			}
			if (this.statusMe != 15 && (this.moveFast == null || this.moveFast[0] <= 0))
			{
				this.paintCharName_HP_MP_Overhead(g);
				if (this.skillPaint == null || this.skillInfoPaint() == null || this.indexSkill >= this.skillInfoPaint().Length)
				{
					this.paintCharWithoutSkill(g);
				}
				if (this.arr != null)
				{
					this.arr.paint(g);
				}
				if (this.dart != null)
				{
					this.dart.paint(g);
				}
				this.paintEffect(g);
				Mob mob = this.mobMe;
				this.paintMount2(g);
				this.paintEff_Lvup_front(g);
				this.paintSuperEffFront(g);
				this.paintAuraFront(g);
				this.paintEffFront(g);
				this.paint_map_line(g);
			}
		}
	}

	// Token: 0x0600013B RID: 315 RVA: 0x00010C5C File Offset: 0x0000EE5C
	internal void paint_map_line(mGraphics g)
	{
		if (this.isPaintNewSkill || this.x_hint == 0 || this.y_hint == 0 || this.statusMe == 14)
		{
			return;
		}
		int num = 0;
		int num2 = this.cx - 30;
		int num3 = this.cy - 15;
		int num4 = -30;
		int num5 = 5;
		if (Res.abs(this.cy - (int)this.y_hint) > 150)
		{
			if (this.cy > (int)this.y_hint)
			{
				num = 7;
				num2 = this.cx;
				num3 = this.cy - 15 - 60;
			}
			else
			{
				num = 5;
				num2 = this.cx;
				num3 = this.cy - 15 + 60;
			}
		}
		else if (this.cx > (int)this.x_hint)
		{
			num = 2;
		}
		else if (this.cx <= (int)this.x_hint)
		{
			num2 = this.cx + 30;
		}
		if (GameCanvas.gameTick % 10 >= 5)
		{
			if (Res.abs(this.cx - (int)this.x_hint) > 100)
			{
				g.drawRegion(GameScr.arrow, 0, 0, 13, 16, num, num2, num3, StaticObj.VCENTER_HCENTER);
				return;
			}
			if (Res.abs(this.cx - (int)this.x_hint) < 50)
			{
				g.drawImage(Panel.imgBantay, (int)this.x_hint + num4, (int)(this.y_hint - 60) + num5, 0);
			}
		}
	}

	// Token: 0x0600013C RID: 316 RVA: 0x00010D9C File Offset: 0x0000EF9C
	internal void paintEff_Pet(mGraphics g)
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			Effect effect = (Effect)this.vEffChar.elementAt(i);
			if (effect.effId >= 201)
			{
				effect.paint(g);
			}
		}
	}

	// Token: 0x0600013D RID: 317 RVA: 0x00010DE8 File Offset: 0x0000EFE8
	internal void paintSuperEffBehind(mGraphics g)
	{
		if ((this.me && !global::Char.isPaintAura2) || (this.idAuraEff > -1 || (this.statusMe != 1 && this.statusMe != 6)) || mSystem.currentTimeMillis() - this.timeBlue <= 0L || this.isCopy || this.clevel < 16)
		{
			return;
		}
		int num = 7598;
		int num2 = 4;
		if (this.clevel >= 19)
		{
			num = 7676;
		}
		if (this.clevel >= 22)
		{
			num = 7677;
		}
		if (this.clevel >= 25)
		{
			num = 7678;
		}
		if (num != -1)
		{
			Small small = SmallImage.imgNew[num];
			if (small == null)
			{
				SmallImage.createImage(num);
				return;
			}
			int num3 = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
			g.drawRegion(small.img, 0, num3, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, this.cx, this.cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
		}
	}

	// Token: 0x0600013E RID: 318 RVA: 0x00010EE8 File Offset: 0x0000F0E8
	internal void paintSuperEffFront(mGraphics g)
	{
		if (!global::Char.isPaintAura2)
		{
			return;
		}
		if (this.statusMe == 1 || this.statusMe == 6)
		{
			if (mSystem.currentTimeMillis() - this.timeBlue <= 0L)
			{
				return;
			}
			if (this.isCopy)
			{
				if (GameCanvas.gameTick % 2 == 0)
				{
					this.tBlue++;
				}
				if (this.tBlue > 6)
				{
					this.tBlue = 0;
				}
				g.drawImage(GameCanvas.imgViolet[this.tBlue], this.cx, this.cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
				return;
			}
			if (this.clevel >= 14 && !GameCanvas.lowGraphic)
			{
				bool flag = false;
				if (mSystem.currentTimeMillis() - this.timeBlue > -1000L && this.IsAddDust1)
				{
					flag = true;
					this.IsAddDust1 = false;
				}
				if (mSystem.currentTimeMillis() - this.timeBlue > -500L && this.IsAddDust2)
				{
					flag = true;
					this.IsAddDust2 = false;
				}
				if (flag)
				{
					GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
					GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
					this.addDustEff(1);
				}
			}
			if (this.clevel == 14)
			{
				if (GameCanvas.gameTick % 2 == 0)
				{
					this.tBlue++;
				}
				if (this.tBlue > 6)
				{
					this.tBlue = 0;
				}
				g.drawImage(GameCanvas.imgBlue[this.tBlue], this.cx, this.cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
				return;
			}
			if (this.clevel == 15)
			{
				if (GameCanvas.gameTick % 2 == 0)
				{
					this.tBlue++;
				}
				if (this.tBlue > 6)
				{
					this.tBlue = 0;
				}
				g.drawImage(GameCanvas.imgViolet[this.tBlue], this.cx, this.cy + 9, mGraphics.BOTTOM | mGraphics.HCENTER);
				return;
			}
			if (this.clevel < 16)
			{
				return;
			}
			int num = -1;
			int num2 = 4;
			if (this.clevel >= 16 && this.clevel < 22)
			{
				num = 7599;
				num2 = 4;
			}
			if (num != -1)
			{
				Small small = SmallImage.imgNew[num];
				if (small == null)
				{
					SmallImage.createImage(num);
					return;
				}
				int num3 = GameCanvas.gameTick / 4 % num2 * (mGraphics.getImageHeight(small.img) / num2);
				g.drawRegion(small.img, 0, num3, mGraphics.getImageWidth(small.img), mGraphics.getImageHeight(small.img) / num2, 0, this.cx, this.cy + 2, mGraphics.BOTTOM | mGraphics.HCENTER);
				return;
			}
		}
		else
		{
			this.timeBlue = mSystem.currentTimeMillis() + 1500L;
			this.IsAddDust1 = true;
			this.IsAddDust2 = true;
		}
	}

	// Token: 0x0600013F RID: 319 RVA: 0x0001119C File Offset: 0x0000F39C
	internal void paintEffect(mGraphics g)
	{
		if (this.effPaints != null)
		{
			for (int i = 0; i < this.effPaints.Length; i++)
			{
				if (this.effPaints[i] != null)
				{
					if (this.effPaints[i].eMob != null)
					{
						int num = this.effPaints[i].eMob.y;
						if (this.effPaints[i].eMob is BigBoss)
						{
							num = this.effPaints[i].eMob.y - 60;
						}
						if (this.effPaints[i].eMob is BigBoss2)
						{
							num = this.effPaints[i].eMob.y - 50;
						}
						if (this.effPaints[i].eMob is BachTuoc)
						{
							num = this.effPaints[i].eMob.y - 40;
						}
						SmallImage.drawSmallImage(g, this.effPaints[i].getImgId(), this.effPaints[i].eMob.x, num, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
					else if (this.effPaints[i].eChar != null)
					{
						SmallImage.drawSmallImage(g, this.effPaints[i].getImgId(), this.effPaints[i].eChar.cx, this.effPaints[i].eChar.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
					}
				}
			}
		}
		if (this.indexEff >= 0 && this.eff != null)
		{
			SmallImage.drawSmallImage(g, this.eff.arrEfInfo[this.indexEff].idImg, this.cx + this.eff.arrEfInfo[this.indexEff].dx, this.cy + this.eff.arrEfInfo[this.indexEff].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
		}
		if (this.indexEffTask >= 0 && this.effTask != null)
		{
			SmallImage.drawSmallImage(g, this.effTask.arrEfInfo[this.indexEffTask].idImg, this.cx + this.effTask.arrEfInfo[this.indexEffTask].dx, this.cy + this.effTask.arrEfInfo[this.indexEffTask].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
		}
	}

	// Token: 0x06000140 RID: 320 RVA: 0x00004887 File Offset: 0x00002A87
	internal void paintArrowAttack(mGraphics g)
	{
	}

	// Token: 0x06000141 RID: 321 RVA: 0x000113F0 File Offset: 0x0000F5F0
	public void paintHp(mGraphics g, int x, int y)
	{
		int num = this.cHP * 100 / this.cHPFull / 10 - 1;
		if (num < 0)
		{
			num = 0;
		}
		if (num > 9)
		{
			num = 9;
		}
		if (!this.me)
		{
			g.drawRegion(Mob.imgHP, 0, 6 * (9 - num), 9, 6, 0, x, y - mFont.tahoma_7.getHeight() - 6, 3);
		}
		if (this.cTypePk == 0 && (global::Char.myCharz().cFlag == 0 || this.cFlag == 0 || (this.cFlag != 8 && global::Char.myCharz().cFlag != 8 && this.cFlag == global::Char.myCharz().cFlag)))
		{
			return;
		}
		this.len = (int)((long)this.cHP * 100L / (long)this.cHPFull * (long)this.w_hp_bar) / 100;
		num = (int)((long)this.cHP * 100L / (long)this.cHPFull);
		if (num < 30)
		{
			this.imgHPtem = GameScr.imgHP_tm_do;
		}
		else if (num < 60)
		{
			this.imgHPtem = GameScr.imgHP_tm_vang;
		}
		else
		{
			this.imgHPtem = GameScr.imgHP_tm_xanh;
		}
		int imageWidth = mGraphics.getImageWidth(GameScr.imgHP_tm_xam);
		int imageHeight = mGraphics.getImageHeight(GameScr.imgHP_tm_xam);
		int num2 = imageWidth * num / 100;
		g.drawImage(GameScr.imgHP_tm_xam, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
		if (this.len < 5)
		{
			if (GameCanvas.gameTick % 6 < 3)
			{
				g.drawRegion(this.imgHPtem, 0, 0, num2, imageHeight, 0, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
				return;
			}
		}
		else
		{
			g.drawRegion(this.imgHPtem, 0, 0, num2, imageHeight, 0, x - (imageWidth >> 1), y - 1, mGraphics.TOP | mGraphics.LEFT);
		}
	}

	// Token: 0x06000142 RID: 322 RVA: 0x00011594 File Offset: 0x0000F794
	public int getClassColor()
	{
		int num = 9145227;
		if (this.nClass.classId == 1 || this.nClass.classId == 2)
		{
			num = 16711680;
		}
		else if (this.nClass.classId == 3 || this.nClass.classId == 4)
		{
			num = 33023;
		}
		else if (this.nClass.classId == 5 || this.nClass.classId == 6)
		{
			num = 7443811;
		}
		return num;
	}

	// Token: 0x06000143 RID: 323 RVA: 0x00011614 File Offset: 0x0000F814
	public void paintNameInSameParty(mGraphics g)
	{
		if (this.cTypePk != 3 && this.cTypePk != 5 && this.isPaint())
		{
			if (global::Char.myCharz().charFocus == null || !global::Char.myCharz().charFocus.Equals(this))
			{
				mFont.tahoma_7_yellow.drawString(g, this.cName, this.cx, this.cy - this.ch - mFont.tahoma_7_green.getHeight() - 5, mFont.CENTER, mFont.tahoma_7_grey);
				return;
			}
			if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this))
			{
				mFont.tahoma_7_yellow.drawString(g, this.cName, this.cx, this.cy - this.ch - mFont.tahoma_7_green.getHeight() - 10, mFont.CENTER, mFont.tahoma_7_grey);
			}
		}
	}

	// Token: 0x06000144 RID: 324 RVA: 0x000116F8 File Offset: 0x0000F8F8
	internal void paintCharName_HP_MP_Overhead(mGraphics g)
	{
		Part part = GameScr.parts[this.getFHead(this.head)];
		int num = global::Char.CharInfo[this.cf][0][2] - (int)part.pi[global::Char.CharInfo[this.cf][0][0]].dy + 5;
		if ((this.isInvisiblez && !this.me) || (!this.me && TileMap.mapID == 113 && this.cy >= 360))
		{
			return;
		}
		if (this.me)
		{
			num += 5;
			this.paintHp(g, this.cx, this.cy - num + 3);
			if (this.fraDanhHieu != null)
			{
				int num2 = this.cx - this.fraDanhHieu.frameWidth / 2;
				int num3 = this.cy - num + 3 - mFont.tahoma_7.getHeight() - (this.fraDanhHieu.frameHeight + 5);
				if (GameCanvas.gameTick % 5 == 0)
				{
					this.danhHieuFramme++;
				}
				if (this.danhHieuFramme >= this.fraDanhHieu.nFrame)
				{
					this.danhHieuFramme = 0;
				}
				this.fraDanhHieu.drawFrame(this.danhHieuFramme, num2, num3, 0, mGraphics.TOP | mGraphics.LEFT, g);
			}
			return;
		}
		bool flag = global::Char.myChar.clan != null && this.clanID == global::Char.myChar.clan.ID;
		bool flag2 = this.cTypePk == 3 || this.cTypePk == 5;
		bool flag3 = this.cTypePk == 4;
		if (this.cName.StartsWith("$"))
		{
			this.cName = this.cName.Substring(1);
			this.isPet = true;
		}
		if (this.cName.StartsWith("#"))
		{
			this.cName = this.cName.Substring(1);
			this.isMiniPet = true;
		}
		if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this))
		{
			num += 5;
			this.paintHp(g, this.cx, this.cy - num + 3);
			if (this.fraDanhHieu != null)
			{
				int num4 = this.cx - this.fraDanhHieu.frameWidth / 2;
				int num5 = this.cy - num + 3 - mFont.tahoma_7.getHeight() - (this.fraDanhHieu.frameHeight + 5);
				if (GameCanvas.gameTick % 5 == 0)
				{
					this.danhHieuFramme++;
				}
				if (this.danhHieuFramme >= this.fraDanhHieu.nFrame)
				{
					this.danhHieuFramme = 0;
				}
				this.fraDanhHieu.drawFrame(this.danhHieuFramme, num4, num5, 0, mGraphics.TOP | mGraphics.LEFT, g);
			}
		}
		num += mFont.tahoma_7_white.getHeight();
		mFont mFont = mFont.tahoma_7_whiteSmall;
		if (this.isPet || this.isMiniPet)
		{
			mFont = mFont.tahoma_7_blue1Small;
		}
		else if (flag2)
		{
			mFont = mFont.nameFontRed;
		}
		else if (flag3)
		{
			mFont = mFont.nameFontYellow;
		}
		else if (flag)
		{
			mFont = mFont.nameFontGreen;
		}
		if (TileMap.mapID == 170)
		{
			if (this.flagImage == 2325)
			{
				mFont = mFont.tahoma_7_blue;
			}
			else if (this.flagImage == 2323)
			{
				mFont = mFont.tahoma_7_red;
			}
		}
		if ((this.paintName || flag2 || flag3) && !flag)
		{
			if (mSystem.clientType == 1)
			{
				mFont.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
			}
			else if (this.charID == -83)
			{
				mFont.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
			}
			else
			{
				mFont.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER);
			}
			num += mFont.tahoma_7.getHeight();
		}
		if (flag)
		{
			if (global::Char.myCharz().charFocus != null && global::Char.myCharz().charFocus.Equals(this))
			{
				mFont.drawString(g, this.cName, this.cx, this.cy - num, mFont.CENTER, mFont.tahoma_7_greySmall);
				return;
			}
			if (this.charFocus == null)
			{
				mFont.drawString(g, this.cName, this.cx - 10, this.cy - num + 3, mFont.LEFT, mFont.tahoma_7_grey);
				this.paintHp(g, this.cx - 16, this.cy - num + 10);
			}
		}
	}

	// Token: 0x06000145 RID: 325 RVA: 0x00011B74 File Offset: 0x0000FD74
	public void paintShadow(mGraphics g)
	{
		if (this.isMabuHold || this.head == 377 || this.leg == 471 || this.isTeleport || this.isFlyUp)
		{
			return;
		}
		int size = (int)TileMap.size;
		if ((TileMap.mapID < 114 || TileMap.mapID > 120) && TileMap.mapID != 127 && TileMap.mapID != 128 && !TileMap.tileTypeAt(this.xSd + size / 2, this.ySd + 1, 4))
		{
			if (TileMap.tileTypeAt((this.xSd - size / 2) / size, (this.ySd + 1) / size) == 0)
			{
				g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, 100, 100);
			}
			else if (TileMap.tileTypeAt((this.xSd + size / 2) / size, (this.ySd + 1) / size) == 0)
			{
				g.setClip(this.xSd / size * size, (this.ySd - 30) / size * size, size, 100);
			}
			else if (TileMap.tileTypeAt(this.xSd - size / 2, this.ySd + 1, 8))
			{
				g.setClip(this.xSd / 24 * size, (this.ySd - 30) / size * size, size, 100);
			}
		}
		g.drawImage(TileMap.bong, this.xSd, this.ySd, 3);
		g.setClip(GameScr.cmx, GameScr.cmy - GameCanvas.transY, GameScr.gW, GameScr.gH + 2 * GameCanvas.transY);
	}

	// Token: 0x06000146 RID: 326 RVA: 0x00011D04 File Offset: 0x0000FF04
	public void updateShadown()
	{
		int i = 0;
		this.xSd = this.cx;
		if (TileMap.tileTypeAt(this.cx, this.cy, 2))
		{
			this.ySd = this.cy;
			return;
		}
		this.ySd = this.cy;
		while (i < 30)
		{
			i++;
			this.ySd += 24;
			if (TileMap.tileTypeAt(this.xSd, this.ySd, 2))
			{
				if (this.ySd % 24 != 0)
				{
					this.ySd -= this.ySd % 24;
					return;
				}
				break;
			}
		}
	}

	// Token: 0x06000147 RID: 327 RVA: 0x00011D9C File Offset: 0x0000FF9C
	internal void paintCharWithoutSkill(mGraphics g)
	{
		try
		{
			if (this.isMafuba)
			{
				this.paintCharBody(g, this.xMFB, this.yMFB, this.cdir, this.cf, false);
			}
			else
			{
				if (this.isInvisiblez)
				{
					if (this.me)
					{
						if (GameCanvas.gameTick % 50 == 48 || GameCanvas.gameTick % 50 == 90)
						{
							SmallImage.drawSmallImage(g, 1196, this.cx, this.cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
						}
						else
						{
							SmallImage.drawSmallImage(g, 1195, this.cx, this.cy - 18, 0, mGraphics.VCENTER | mGraphics.HCENTER);
						}
					}
				}
				else
				{
					this.paintCharBody(g, this.cx, this.cy + this.fy, this.cdir, this.cf, true);
				}
				if (this.isLockAttack)
				{
					SmallImage.drawSmallImage(g, 290, this.cx, this.cy, 0, mGraphics.BOTTOM | mGraphics.HCENTER);
				}
			}
		}
		catch (Exception ex)
		{
			Cout.LogError("Loi paint char without skill: " + ex.ToString());
		}
	}

	// Token: 0x06000148 RID: 328 RVA: 0x00011ED0 File Offset: 0x000100D0
	public void paintBag(mGraphics g, short[] id, int x, int y, int dir, bool isPaintChar)
	{
		int num = 0;
		int num2 = 0;
		if (this.statusMe == 6)
		{
			num = 8;
			num2 = 17;
		}
		if (this.statusMe == 1)
		{
			if (this.cp1 % 15 < 5)
			{
				num = 8;
				num2 = 17;
			}
			else
			{
				num = 8;
				num2 = 18;
			}
		}
		if (this.statusMe == 2)
		{
			if (this.cf <= 3)
			{
				num = 7;
				num2 = 17;
			}
			else
			{
				num = 7;
				num2 = 18;
			}
		}
		if (this.statusMe == 3 || this.statusMe == 9)
		{
			num = 5;
			num2 = 20;
		}
		if (this.statusMe == 4)
		{
			if (this.cf == 8)
			{
				num = 5;
				num2 = 16;
			}
			else
			{
				num = 5;
				num2 = 20;
			}
		}
		if (this.statusMe == 10)
		{
			if (this.cf == 8)
			{
				num = 0;
				num2 = 23;
			}
			else
			{
				num = 5;
				num2 = 22;
			}
		}
		if (this.isInjure > 0)
		{
			num = 5;
			num2 = 18;
		}
		if (this.skillPaint != null && this.skillInfoPaint() != null && this.indexSkill < this.skillInfoPaint().Length)
		{
			num = -1;
			num2 = 17;
		}
		this.fBag++;
		if (this.fBag > 10000)
		{
			this.fBag = 0;
		}
		sbyte b = (sbyte)(this.fBag / 4 % id.Length);
		if (!isPaintChar)
		{
			if (id.Length == 2)
			{
				b = 1;
			}
			if (id.Length == 3)
			{
				if (id[2] >= 0)
				{
					b = 2;
					if (GameCanvas.gameTick % 10 > 5)
					{
						b = 1;
					}
				}
				else
				{
					b = 1;
				}
			}
		}
		else if (id.Length > 1 && (b == 0 || b == 1) && this.statusMe != 1 && this.statusMe != 6)
		{
			this.fBag = 0;
			b = 0;
			if (GameCanvas.gameTick % 10 > 5)
			{
				b = 1;
			}
		}
		SmallImage.drawSmallImage(g, (int)id[(int)b], x + ((dir != 1) ? num : (-num)), y - num2, (dir != 1) ? 2 : 0, StaticObj.VCENTER_HCENTER);
	}

	// Token: 0x06000149 RID: 329 RVA: 0x00012070 File Offset: 0x00010270
	public bool isCharBodyImageID(int id)
	{
		Part part = GameScr.parts[this.head];
		Part part2 = GameScr.parts[this.leg];
		Part part3 = GameScr.parts[this.body];
		for (int i = 0; i < global::Char.CharInfo.Length; i++)
		{
			if (id == (int)part.pi[global::Char.CharInfo[i][0][0]].id)
			{
				return true;
			}
			if (id == (int)part2.pi[global::Char.CharInfo[i][1][0]].id)
			{
				return true;
			}
			if (id == (int)part3.pi[global::Char.CharInfo[i][2][0]].id)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600014A RID: 330 RVA: 0x0001210B File Offset: 0x0001030B
	public void paintHead(mGraphics g, int cx, int cy, int look)
	{
		SmallImage.drawSmallImage(g, (int)GameScr.parts[this.head].pi[global::Char.CharInfo[0][0][0]].id, cx, cy, (look != 0) ? 2 : 0, mGraphics.RIGHT | mGraphics.VCENTER);
	}

	// Token: 0x0600014B RID: 331 RVA: 0x0001214C File Offset: 0x0001034C
	public void paintHeadWithXY(mGraphics g, int x, int y, int look)
	{
		Part part = GameScr.parts[this.head];
		SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[0][0][0]].id, x + global::Char.CharInfo[0][0][1] + (int)part.pi[global::Char.CharInfo[0][0][0]].dx - 3, y + 3, look, mGraphics.LEFT | mGraphics.BOTTOM);
	}

	// Token: 0x0600014C RID: 332 RVA: 0x000121BC File Offset: 0x000103BC
	public void paintCharBody(mGraphics g, int cx, int cy, int cdir, int cf, bool isPaintBag)
	{
		this.ph = GameScr.parts[this.head];
		this.pl = GameScr.parts[this.leg];
		this.pb = GameScr.parts[this.body];
		if (this.bag >= 0 && this.statusMe != 14)
		{
			if (!ClanImage.idImages.containsKey(this.bag.ToString() + string.Empty))
			{
				ClanImage.idImages.put(this.bag.ToString() + string.Empty, new ClanImage());
				Service.gI().requestBagImage((sbyte)this.bag);
			}
			else
			{
				ClanImage clanImage = (ClanImage)ClanImage.idImages.get(this.bag.ToString() + string.Empty);
				if (clanImage.idImage != null && isPaintBag)
				{
					this.paintBag(g, clanImage.idImage, cx, cy, cdir, true);
				}
			}
		}
		int num = 2;
		int num2 = 24;
		int num3 = StaticObj.TOP_RIGHT;
		int num4 = -1;
		if (cdir == 1)
		{
			num = 0;
			num2 = 0;
			num3 = 0;
			num4 = 1;
		}
		if (this.statusMe == 14)
		{
			if (GameCanvas.gameTick % 4 > 0)
			{
				g.drawImage(ItemMap.imageFlare, cx, cy - this.ch - 11, mGraphics.HCENTER | mGraphics.VCENTER);
			}
			int num5 = 0;
			if (this.head == 89 || this.head == 457 || this.head == 460 || this.head == 461 || this.head == 462 || this.head == 463 || this.head == 464 || this.head == 465 || this.head == 466)
			{
				num5 = 15;
			}
			if (this.head == 1291)
			{
				num5 = 23;
			}
			SmallImage.drawSmallImage(g, 834, cx, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy - 2 + num5, num, StaticObj.TOP_CENTER);
			SmallImage.drawSmallImage(g, 79, cx, cy - this.ch - 8, 0, mGraphics.HCENTER | mGraphics.BOTTOM);
			SmallImage.drawSmallImage(g, (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num4, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, num2);
			this.paintHat_behind(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy);
			if (this.isHead_2Fr(this.head))
			{
				Part part = GameScr.parts[this.getFHead(this.head)];
				SmallImage.drawSmallImage(g, (int)part.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dx) * num4, cy - global::Char.CharInfo[cf][0][2] + (int)part.pi[global::Char.CharInfo[cf][0][0]].dy, num, num2);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num4, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, num2);
			}
			this.paintHat_front(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy);
			this.paintRedEye(g, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num4, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, num2);
		}
		else
		{
			this.paintHat_behind(g, cf, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy);
			if (this.isHead_2Fr(this.head))
			{
				Part part2 = GameScr.parts[this.getFHead(this.head)];
				SmallImage.drawSmallImage(g, (int)part2.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)part2.pi[global::Char.CharInfo[cf][0][0]].dx) * num4, cy - global::Char.CharInfo[cf][0][2] + (int)part2.pi[global::Char.CharInfo[cf][0][0]].dy, num, num2);
			}
			else
			{
				SmallImage.drawSmallImage(g, (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].id, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num4, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, num2);
			}
			SmallImage.drawSmallImage(g, (int)this.pl.pi[global::Char.CharInfo[cf][1][0]].id, cx + (global::Char.CharInfo[cf][1][1] + (int)this.pl.pi[global::Char.CharInfo[cf][1][0]].dx) * num4, cy - global::Char.CharInfo[cf][1][2] + (int)this.pl.pi[global::Char.CharInfo[cf][1][0]].dy, num, num2);
			SmallImage.drawSmallImage(g, (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].id, cx + (global::Char.CharInfo[cf][2][1] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dx) * num4, cy - global::Char.CharInfo[cf][2][2] + (int)this.pb.pi[global::Char.CharInfo[cf][2][0]].dy, num, num2);
			this.paintRedEye(g, cx + (global::Char.CharInfo[cf][0][1] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dx) * num4, cy - global::Char.CharInfo[cf][0][2] + (int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy, num, num2);
		}
		this.ch = ((this.isMonkey != 1 && !this.isFusion) ? (global::Char.CharInfo[0][0][2] + (int)this.ph.pi[global::Char.CharInfo[0][0][0]].dy + 10) : 60);
		int num6 = (int)((Res.abs((int)this.ph.pi[global::Char.CharInfo[cf][0][0]].dy) < 22) ? this.ph.pi[global::Char.CharInfo[cf][0][0]].dy : ((this.ph.pi[global::Char.CharInfo[cf][0][0]].dy >= 0) ? (this.ph.pi[global::Char.CharInfo[cf][0][0]].dy - 5) : (this.ph.pi[global::Char.CharInfo[cf][0][0]].dy + 5)));
		this.cH_new = cy - global::Char.CharInfo[cf][0][2] + num6;
		if (this.statusMe == 1 && this.charID > 0 && !this.isMask && !this.isUseChargeSkill() && !this.isWaitMonkey && this.skillPaint == null && cf != 23 && this.bag < 0 && ((GameCanvas.gameTick + this.charID) % 30 == 0 || this.isFreez))
		{
			g.drawImage((this.cgender != 1) ? global::Char.eyeTraiDat : global::Char.eyeNamek, cx + -((this.cgender != 1) ? 2 : 2) * num4, cy - 32 + ((this.cgender != 1) ? 11 : 10) - cf, num3);
		}
		if (this.eProtect != null)
		{
			this.eProtect.paint(g);
		}
		if (this.eDanhHieu != null)
		{
			this.eDanhHieu.paint(g);
		}
		this.paintPKFlag(g);
	}

	// Token: 0x0600014D RID: 333 RVA: 0x00012AEC File Offset: 0x00010CEC
	public void paintCharWithSkill(mGraphics g)
	{
		this.ty = 0;
		SkillInfoPaint[] array = this.skillInfoPaint();
		this.cf = array[this.indexSkill].status;
		this.paintCharWithoutSkill(g);
		if (this.cdir == 1)
		{
			if (this.eff0 != null)
			{
				if (this.dx0 == 0)
				{
					this.dx0 = array[this.indexSkill].e0dx;
				}
				if (this.dy0 == 0)
				{
					this.dy0 = array[this.indexSkill].e0dy;
				}
				SmallImage.drawSmallImage(g, this.eff0.arrEfInfo[this.i0].idImg, this.cx + this.dx0 + this.eff0.arrEfInfo[this.i0].dx, this.cy + this.dy0 + this.eff0.arrEfInfo[this.i0].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i0++;
				if (this.i0 >= this.eff0.arrEfInfo.Length)
				{
					this.eff0 = null;
					this.i0 = (this.dx0 = (this.dy0 = 0));
				}
			}
			if (this.eff1 != null)
			{
				if (this.dx1 == 0)
				{
					this.dx1 = array[this.indexSkill].e1dx;
				}
				if (this.dy1 == 0)
				{
					this.dy1 = array[this.indexSkill].e1dy;
				}
				SmallImage.drawSmallImage(g, this.eff1.arrEfInfo[this.i1].idImg, this.cx + this.dx1 + this.eff1.arrEfInfo[this.i1].dx, this.cy + this.dy1 + this.eff1.arrEfInfo[this.i1].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i1++;
				if (this.i1 >= this.eff1.arrEfInfo.Length)
				{
					this.eff1 = null;
					this.i1 = (this.dx1 = (this.dy1 = 0));
				}
			}
			if (this.eff2 != null)
			{
				if (this.dx2 == 0)
				{
					this.dx2 = array[this.indexSkill].e2dx;
				}
				if (this.dy2 == 0)
				{
					this.dy2 = array[this.indexSkill].e2dy;
				}
				SmallImage.drawSmallImage(g, this.eff2.arrEfInfo[this.i2].idImg, this.cx + this.dx2 + this.eff2.arrEfInfo[this.i2].dx, this.cy + this.dy2 + this.eff2.arrEfInfo[this.i2].dy, 0, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i2++;
				if (this.i2 >= this.eff2.arrEfInfo.Length)
				{
					this.eff2 = null;
					this.i2 = (this.dx2 = (this.dy2 = 0));
				}
			}
		}
		else
		{
			if (this.eff0 != null)
			{
				if (this.dx0 == 0)
				{
					this.dx0 = array[this.indexSkill].e0dx;
				}
				if (this.dy0 == 0)
				{
					this.dy0 = array[this.indexSkill].e0dy;
				}
				SmallImage.drawSmallImage(g, this.eff0.arrEfInfo[this.i0].idImg, this.cx - this.dx0 - this.eff0.arrEfInfo[this.i0].dx, this.cy + this.dy0 + this.eff0.arrEfInfo[this.i0].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i0++;
				if (this.i0 >= this.eff0.arrEfInfo.Length)
				{
					this.eff0 = null;
					this.i0 = 0;
					this.dx0 = 0;
					this.dy0 = 0;
				}
			}
			if (this.eff1 != null)
			{
				if (this.dx1 == 0)
				{
					this.dx1 = array[this.indexSkill].e1dx;
				}
				if (this.dy1 == 0)
				{
					this.dy1 = array[this.indexSkill].e1dy;
				}
				SmallImage.drawSmallImage(g, this.eff1.arrEfInfo[this.i1].idImg, this.cx - this.dx1 - this.eff1.arrEfInfo[this.i1].dx, this.cy + this.dy1 + this.eff1.arrEfInfo[this.i1].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i1++;
				if (this.i1 >= this.eff1.arrEfInfo.Length)
				{
					this.eff1 = null;
					this.i1 = 0;
					this.dx1 = 0;
					this.dy1 = 0;
				}
			}
			if (this.eff2 != null)
			{
				if (this.dx2 == 0)
				{
					this.dx2 = array[this.indexSkill].e2dx;
				}
				if (this.dy2 == 0)
				{
					this.dy2 = array[this.indexSkill].e2dy;
				}
				SmallImage.drawSmallImage(g, this.eff2.arrEfInfo[this.i2].idImg, this.cx - this.dx2 - this.eff2.arrEfInfo[this.i2].dx, this.cy + this.dy2 + this.eff2.arrEfInfo[this.i2].dy, 2, mGraphics.VCENTER | mGraphics.HCENTER);
				this.i2++;
				if (this.i2 >= this.eff2.arrEfInfo.Length)
				{
					this.eff2 = null;
					this.i2 = 0;
					this.dx2 = 0;
					this.dy2 = 0;
				}
			}
		}
		this.indexSkill++;
	}

	// Token: 0x0600014E RID: 334 RVA: 0x00013104 File Offset: 0x00011304
	public static int getIndexChar(int ID)
	{
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			if (((global::Char)GameScr.vCharInMap.elementAt(i)).charID == ID)
			{
				return i;
			}
		}
		return -1;
	}

	// Token: 0x0600014F RID: 335 RVA: 0x00013144 File Offset: 0x00011344
	public void moveTo(int toX, int toY, int type)
	{
		if (type == 1 || Res.abs(toX - this.cx) > 100 || Res.abs(toY - this.cy) > 300)
		{
			this.createShadow(this.cx, this.cy, 10);
			this.cx = toX;
			this.cy = toY;
			this.vMovePoints.removeAllElements();
			this.statusMe = 6;
			this.cp3 = 0;
			this.currentMovePoint = null;
			this.cf = 25;
			return;
		}
		int num = 0;
		int num2 = 0;
		int num3 = toX - this.cx;
		int num4 = toY - this.cy;
		if (num3 == 0 && num4 == 0)
		{
			num2 = 1;
			this.cp3 = 0;
		}
		else if (num4 == 0)
		{
			num2 = 2;
			if (num3 > 0)
			{
				num = 1;
			}
			if (num3 < 0)
			{
				num = -1;
			}
		}
		else if (num4 != 0)
		{
			if (num4 < 0)
			{
				num2 = 3;
			}
			if (num4 > 0)
			{
				num2 = 4;
			}
			if (num3 < 0)
			{
				num = -1;
			}
			if (num3 > 0)
			{
				num = 1;
			}
		}
		this.vMovePoints.addElement(new MovePoint(toX, toY, num2, num));
		if (this.statusMe != 6)
		{
			this.statusBeforeNothing = this.statusMe;
		}
		this.statusMe = 6;
		this.cp3 = 0;
	}

	// Token: 0x06000150 RID: 336 RVA: 0x00013254 File Offset: 0x00011454
	public static void getcharInjure(int cID, int dx, int dy, int HP)
	{
		global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(cID);
		if (@char.vMovePoints.size() != 0)
		{
			MovePoint movePoint = (MovePoint)@char.vMovePoints.lastElement();
			int num = movePoint.xEnd + dx;
			int num2 = movePoint.yEnd + dy;
			global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(cID);
			char2.cHP -= HP;
			if (char2.cHP < 0)
			{
				char2.cHP = 0;
			}
			char2.cHPShow = ((global::Char)GameScr.vCharInMap.elementAt(cID)).cHP - HP;
			char2.statusMe = 6;
			char2.cp3 = 0;
			char2.vMovePoints.addElement(new MovePoint(num, num2, 8, char2.cdir));
		}
	}

	// Token: 0x06000151 RID: 337 RVA: 0x00013318 File Offset: 0x00011518
	public bool isMagicTree()
	{
		if (GameScr.gI().magicTree != null)
		{
			int x = GameScr.gI().magicTree.x;
			int y = GameScr.gI().magicTree.y;
			return this.cx > x - 30 && this.cx < x + 30 && this.cy > y - 30 && this.cy < y + 30;
		}
		return false;
	}

	// Token: 0x06000152 RID: 338 RVA: 0x00013388 File Offset: 0x00011588
	public void searchItem()
	{
		int[] array = new int[] { -1, -1, -1, -1 };
		if (this.itemFocus != null)
		{
			return;
		}
		for (int i = 0; i < GameScr.vItemMap.size(); i++)
		{
			ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(i);
			int num = Math2.abs(global::Char.myCharz().cx - itemMap.x);
			int num2 = Math2.abs(global::Char.myCharz().cy - itemMap.y);
			int num3 = ((num <= num2) ? num2 : num);
			if (num <= 48 && num2 <= 48 && (this.itemFocus == null || num3 < array[3]))
			{
				if (GameScr.gI().auto != 0 && GameScr.gI().isBagFull())
				{
					if (itemMap.template.type == 9)
					{
						this.itemFocus = itemMap;
						array[3] = num3;
					}
				}
				else
				{
					this.itemFocus = itemMap;
					array[3] = num3;
				}
			}
		}
	}

	// Token: 0x06000153 RID: 339 RVA: 0x00013474 File Offset: 0x00011674
	public void searchFocus()
	{
		if (global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null)
		{
			this.timeFocusToMob = 200;
			return;
		}
		if (this.timeFocusToMob > 0)
		{
			this.timeFocusToMob--;
			return;
		}
		if (global::Char.isManualFocus && this.charFocus != null && (this.charFocus.statusMe == 15 || this.charFocus.isInvisiblez))
		{
			this.charFocus = null;
		}
		if (GameCanvas.gameTick % 2 == 0 || this.isMeCanAttackOtherPlayer(this.charFocus))
		{
			return;
		}
		int num = 0;
		if (this.nClass != null && (this.nClass.classId == 0 || this.nClass.classId == 1 || this.nClass.classId == 3 || this.nClass.classId == 5))
		{
			num = 40;
		}
		int[] array = new int[] { -1, -1, -1, -1 };
		int num2 = GameScr.cmx - 10;
		int num3 = GameScr.cmx + GameCanvas.w + 10;
		int num4 = GameScr.cmy;
		int num5 = GameScr.cmy + GameCanvas.h - GameScr.cmdBarH + 10;
		if (global::Char.isManualFocus)
		{
			if ((this.mobFocus != null && this.mobFocus.status != 1 && this.mobFocus.status != 0 && num2 <= this.mobFocus.x && this.mobFocus.x <= num3 && num4 <= this.mobFocus.y && this.mobFocus.y <= num5) || (this.npcFocus != null && num2 <= this.npcFocus.cx && this.npcFocus.cx <= num3 && num4 <= this.npcFocus.cy && this.npcFocus.cy <= num5) || (this.charFocus != null && num2 <= this.charFocus.cx && this.charFocus.cx <= num3 && num4 <= this.charFocus.cy && this.charFocus.cy <= num5) || (this.itemFocus != null && num2 <= this.itemFocus.x && this.itemFocus.x <= num3 && num4 <= this.itemFocus.y && this.itemFocus.y <= num5))
			{
				return;
			}
			global::Char.isManualFocus = false;
		}
		num2 = global::Char.myCharz().cx - 80;
		num3 = global::Char.myCharz().cx + 80;
		num4 = global::Char.myCharz().cy - 30;
		num5 = global::Char.myCharz().cy + 30;
		if (this.npcFocus != null && this.npcFocus.template.npcTemplateId == 6)
		{
			num2 = global::Char.myCharz().cx - 20;
			num3 = global::Char.myCharz().cx + 20;
			num4 = global::Char.myCharz().cy - 10;
			num5 = global::Char.myCharz().cy + 10;
		}
		if (this.npcFocus == null)
		{
			for (int i = 0; i < GameScr.vNpc.size(); i++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(i);
				if (npc.statusMe != 15)
				{
					int num6 = Math2.abs(global::Char.myCharz().cx - npc.cx);
					int num7 = Math2.abs(global::Char.myCharz().cy - npc.cy);
					int num8 = ((num6 <= num7) ? num7 : num6);
					num2 = global::Char.myCharz().cx - 80;
					num3 = global::Char.myCharz().cx + 80;
					num4 = global::Char.myCharz().cy - 30;
					num5 = global::Char.myCharz().cy + 30;
					if (npc.template.npcTemplateId == 6)
					{
						num2 = global::Char.myCharz().cx - 20;
						num3 = global::Char.myCharz().cx + 20;
						num4 = global::Char.myCharz().cy - 10;
						num5 = global::Char.myCharz().cy + 10;
					}
					if (num2 <= npc.cx && npc.cx <= num3 && num4 <= npc.cy && npc.cy <= num5 && (this.npcFocus == null || num8 < array[1]))
					{
						this.npcFocus = npc;
						array[1] = num8;
					}
				}
			}
		}
		else
		{
			if (num2 <= this.npcFocus.cx && this.npcFocus.cx <= num3 && num4 <= this.npcFocus.cy && this.npcFocus.cy <= num5)
			{
				this.clearFocus(1);
				return;
			}
			this.deFocusNPC();
			for (int j = 0; j < GameScr.vNpc.size(); j++)
			{
				Npc npc2 = (Npc)GameScr.vNpc.elementAt(j);
				if (npc2.statusMe != 15)
				{
					int num9 = Math2.abs(global::Char.myCharz().cx - npc2.cx);
					int num10 = Math2.abs(global::Char.myCharz().cy - npc2.cy);
					int num11 = ((num9 <= num10) ? num10 : num9);
					num2 = global::Char.myCharz().cx - 80;
					num3 = global::Char.myCharz().cx + 80;
					num4 = global::Char.myCharz().cy - 30;
					num5 = global::Char.myCharz().cy + 30;
					if (npc2.template.npcTemplateId == 6)
					{
						num2 = global::Char.myCharz().cx - 20;
						num3 = global::Char.myCharz().cx + 20;
						num4 = global::Char.myCharz().cy - 10;
						num5 = global::Char.myCharz().cy + 10;
					}
					if (num2 <= npc2.cx && npc2.cx <= num3 && num4 <= npc2.cy && npc2.cy <= num5 && (this.npcFocus == null || num11 < array[1]))
					{
						this.npcFocus = npc2;
						array[1] = num11;
					}
				}
			}
		}
		if (this.itemFocus == null)
		{
			for (int k = 0; k < GameScr.vItemMap.size(); k++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(k);
				int num12 = Math2.abs(global::Char.myCharz().cx - itemMap.x);
				int num13 = Math2.abs(global::Char.myCharz().cy - itemMap.y);
				int num14 = ((num12 <= num13) ? num13 : num12);
				if (num12 <= 48 && num13 <= 48 && (this.itemFocus == null || num14 < array[3]))
				{
					if (GameScr.gI().auto != 0 && GameScr.gI().isBagFull())
					{
						if (itemMap.template.type == 9)
						{
							this.itemFocus = itemMap;
							array[3] = num14;
						}
					}
					else
					{
						this.itemFocus = itemMap;
						array[3] = num14;
					}
				}
			}
		}
		else
		{
			if (num2 <= this.itemFocus.x && this.itemFocus.x <= num3 && num4 <= this.itemFocus.y && this.itemFocus.y <= num5)
			{
				this.clearFocus(3);
				return;
			}
			this.itemFocus = null;
			for (int l = 0; l < GameScr.vItemMap.size(); l++)
			{
				ItemMap itemMap2 = (ItemMap)GameScr.vItemMap.elementAt(l);
				int num15 = Math2.abs(global::Char.myCharz().cx - itemMap2.x);
				int num16 = Math2.abs(global::Char.myCharz().cy - itemMap2.y);
				int num17 = ((num15 <= num16) ? num16 : num15);
				if (num2 <= itemMap2.x && itemMap2.x <= num3 && num4 <= itemMap2.y && itemMap2.y <= num5 && (this.itemFocus == null || num17 < array[3]))
				{
					if (GameScr.gI().auto != 0 && GameScr.gI().isBagFull())
					{
						if (itemMap2.template.type == 9)
						{
							this.itemFocus = itemMap2;
							array[3] = num17;
						}
					}
					else
					{
						this.itemFocus = itemMap2;
						array[3] = num17;
					}
				}
			}
		}
		num2 = global::Char.myCharz().cx - global::Char.myCharz().getdxSkill() - 10;
		num3 = global::Char.myCharz().cx + global::Char.myCharz().getdxSkill() + 10;
		num4 = global::Char.myCharz().cy - global::Char.myCharz().getdySkill() - num - 20;
		num5 = global::Char.myCharz().cy + global::Char.myCharz().getdySkill() + 20;
		if (num5 > global::Char.myCharz().cy + 30)
		{
			num5 = global::Char.myCharz().cy + 30;
		}
		if (this.mobFocus == null)
		{
			for (int m = 0; m < GameScr.vMob.size(); m++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(m);
				int num18 = Math2.abs(global::Char.myCharz().cx - mob.x);
				int num19 = Math2.abs(global::Char.myCharz().cy - mob.y);
				int num20 = ((num18 <= num19) ? num19 : num18);
				if (num2 <= mob.x && mob.x <= num3 && num4 <= mob.y && mob.y <= num5 && (this.mobFocus == null || num20 < array[0]))
				{
					this.mobFocus = mob;
					array[0] = num20;
				}
			}
		}
		else
		{
			if (this.mobFocus.status != 1 && this.mobFocus.status != 0 && num2 <= this.mobFocus.x && this.mobFocus.x <= num3 && num4 <= this.mobFocus.y && this.mobFocus.y <= num5)
			{
				this.clearFocus(0);
				return;
			}
			this.mobFocus = null;
			for (int n = 0; n < GameScr.vMob.size(); n++)
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(n);
				int num21 = Math2.abs(global::Char.myCharz().cx - mob2.x);
				int num22 = Math2.abs(global::Char.myCharz().cy - mob2.y);
				int num23 = ((num21 <= num22) ? num22 : num21);
				if (num2 <= mob2.x && mob2.x <= num3 && num4 <= mob2.y && mob2.y <= num5 && (this.mobFocus == null || num23 < array[0]))
				{
					this.mobFocus = mob2;
					array[0] = num23;
				}
			}
		}
		if (this.charFocus == null)
		{
			for (int num24 = 0; num24 < GameScr.vCharInMap.size(); num24++)
			{
				global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(num24);
				if (@char.statusMe != 15 && !@char.isInvisiblez && this.wdx == 0 && this.wdy == 0)
				{
					int num25 = Math2.abs(global::Char.myCharz().cx - @char.cx);
					int num26 = Math2.abs(global::Char.myCharz().cy - @char.cy);
					int num27 = ((num25 <= num26) ? num26 : num25);
					if (num2 <= @char.cx && @char.cx <= num3 && num4 <= @char.cy && @char.cy <= num5 && (this.charFocus == null || num27 < array[2]))
					{
						this.charFocus = @char;
						array[2] = num27;
					}
				}
			}
		}
		else
		{
			if (num2 <= this.charFocus.cx && this.charFocus.cx <= num3 && num4 <= this.charFocus.cy && this.charFocus.cy <= num5 && this.charFocus.statusMe != 15 && !this.charFocus.isInvisiblez)
			{
				this.clearFocus(2);
				return;
			}
			this.charFocus = null;
			for (int num28 = 0; num28 < GameScr.vCharInMap.size(); num28++)
			{
				global::Char char2 = (global::Char)GameScr.vCharInMap.elementAt(num28);
				if (char2.statusMe != 15 && !char2.isInvisiblez && this.wdx == 0 && this.wdy == 0)
				{
					int num29 = Math2.abs(global::Char.myCharz().cx - char2.cx);
					int num30 = Math2.abs(global::Char.myCharz().cy - char2.cy);
					int num31 = ((num29 <= num30) ? num30 : num29);
					if (num2 <= char2.cx && char2.cx <= num3 && num4 <= char2.cy && char2.cy <= num5 && (this.charFocus == null || num31 < array[2]))
					{
						this.charFocus = char2;
						array[2] = num31;
					}
				}
			}
		}
		int num32 = -1;
		for (int num33 = 0; num33 < array.Length; num33++)
		{
			if (num32 == -1)
			{
				if (array[num33] != -1)
				{
					num32 = num33;
				}
			}
			else if (array[num33] < array[num32] && array[num33] != -1)
			{
				num32 = num33;
			}
		}
		this.clearFocus(num32);
		if (this.me && this.isAttacPlayerStatus())
		{
			if (this.mobFocus != null && !this.mobFocus.isMobMe)
			{
				this.mobFocus = null;
			}
			this.npcFocus = null;
			this.itemFocus = null;
		}
	}

	// Token: 0x06000154 RID: 340 RVA: 0x0001419C File Offset: 0x0001239C
	public void clearFocus(int index)
	{
		if (index == 0)
		{
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
			return;
		}
		if (index == 1)
		{
			this.mobFocus = null;
			this.charFocus = null;
			this.itemFocus = null;
			return;
		}
		if (index == 2)
		{
			this.mobFocus = null;
			this.deFocusNPC();
			this.itemFocus = null;
			return;
		}
		if (index == 3)
		{
			this.mobFocus = null;
			this.deFocusNPC();
			this.charFocus = null;
		}
	}

	// Token: 0x06000155 RID: 341 RVA: 0x0001420C File Offset: 0x0001240C
	public static bool isCharInScreen(global::Char c)
	{
		int cmx = GameScr.cmx;
		int num = GameScr.cmx + GameCanvas.w;
		int num2 = GameScr.cmy + 10;
		int num3 = GameScr.cmy + GameScr.gH;
		return c.statusMe != 15 && !c.isInvisiblez && cmx <= c.cx && c.cx <= num && num2 <= c.cy && c.cy <= num3;
	}

	// Token: 0x06000156 RID: 342 RVA: 0x00014279 File Offset: 0x00012479
	public bool isAttacPlayerStatus()
	{
		return this.cTypePk == 4 || this.cTypePk == 3;
	}

	// Token: 0x06000157 RID: 343 RVA: 0x0001428F File Offset: 0x0001248F
	[MethodImpl(MethodImplOptions.NoOptimization)]
	public void setHoldChar(global::Char r)
	{
		if (this.cx < r.cx)
		{
			this.cdir = 1;
		}
		else
		{
			this.cdir = -1;
		}
		this.charHold = r;
		this.holder = true;
	}

	// Token: 0x06000158 RID: 344 RVA: 0x000142BD File Offset: 0x000124BD
	[MethodImpl(MethodImplOptions.NoOptimization)]
	public void setHoldMob(Mob r)
	{
		if (this.cx < r.x)
		{
			this.cdir = 1;
		}
		else
		{
			this.cdir = -1;
		}
		this.mobHold = r;
		this.holder = true;
	}

	// Token: 0x06000159 RID: 345 RVA: 0x000142EC File Offset: 0x000124EC
	public void findNextFocusByKey()
	{
		Res.outz("focus size= " + this.focus.size().ToString());
		if ((global::Char.myCharz().skillPaint != null || global::Char.myCharz().arr != null || global::Char.myCharz().dart != null || global::Char.myCharz().skillInfoPaint() != null) && this.focus.size() == 0)
		{
			return;
		}
		this.focus.removeAllElements();
		int num = 0;
		int num2 = GameScr.cmx + 10;
		int num3 = GameScr.cmx + GameCanvas.w - 10;
		int num4 = GameScr.cmy + 10;
		int num5 = GameScr.cmy + GameScr.gH;
		for (int i = 0; i < GameScr.vCharInMap.size(); i++)
		{
			global::Char @char = (global::Char)GameScr.vCharInMap.elementAt(i);
			if (@char.statusMe != 15 && !@char.isInvisiblez && num2 <= @char.cx && @char.cx <= num3 && num4 <= @char.cy && @char.cy <= num5 && @char.charID != -114 && (TileMap.mapID != 129 || (TileMap.mapID == 129 && global::Char.myCharz().cy > 264)))
			{
				this.focus.addElement(@char);
				if (this.charFocus != null && @char.Equals(this.charFocus))
				{
					num = this.focus.size();
				}
			}
		}
		if (this.me && this.isAttacPlayerStatus())
		{
			Res.outz("co the tan cong nguoi");
			for (int j = 0; j < GameScr.vMob.size(); j++)
			{
				Mob mob = (Mob)GameScr.vMob.elementAt(j);
				if (!GameScr.gI().isMeCanAttackMob(mob))
				{
					Res.outz("khong the tan cong quai");
					this.mobFocus = null;
				}
				else
				{
					Res.outz("co the tan ong quai");
					this.focus.addElement(mob);
					if (this.mobFocus != null)
					{
						num = this.focus.size();
					}
				}
			}
			this.npcFocus = null;
			this.itemFocus = null;
			if (this.focus.size() > 0)
			{
				if (num >= this.focus.size())
				{
					num = 0;
				}
				this.focusManualTo(this.focus.elementAt(num));
				return;
			}
			this.mobFocus = null;
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
			global::Char.isManualFocus = false;
			return;
		}
		else
		{
			for (int k = 0; k < GameScr.vItemMap.size(); k++)
			{
				ItemMap itemMap = (ItemMap)GameScr.vItemMap.elementAt(k);
				if (num2 <= itemMap.x && itemMap.x <= num3 && num4 <= itemMap.y && itemMap.y <= num5)
				{
					this.focus.addElement(itemMap);
					if (this.itemFocus != null && itemMap.Equals(this.itemFocus))
					{
						num = this.focus.size();
					}
				}
			}
			for (int l = 0; l < GameScr.vMob.size(); l++)
			{
				Mob mob2 = (Mob)GameScr.vMob.elementAt(l);
				if (mob2.status != 1 && mob2.status != 0 && num2 <= mob2.x && mob2.x <= num3 && num4 <= mob2.y && mob2.y <= num5)
				{
					this.focus.addElement(mob2);
					if (this.mobFocus != null && mob2.Equals(this.mobFocus))
					{
						num = this.focus.size();
					}
				}
			}
			for (int m = 0; m < GameScr.vNpc.size(); m++)
			{
				Npc npc = (Npc)GameScr.vNpc.elementAt(m);
				if (npc.statusMe != 15 && num2 <= npc.cx && npc.cx <= num3 && num4 <= npc.cy && npc.cy <= num5)
				{
					this.focus.addElement(npc);
					if (this.npcFocus != null && npc.Equals(this.npcFocus))
					{
						num = this.focus.size();
					}
				}
			}
			if (this.focus.size() > 0)
			{
				if (num >= this.focus.size())
				{
					num = 0;
				}
				this.focusManualTo(this.focus.elementAt(num));
				return;
			}
			this.mobFocus = null;
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
			global::Char.isManualFocus = false;
			return;
		}
	}

	// Token: 0x0600015A RID: 346 RVA: 0x00014772 File Offset: 0x00012972
	public void deFocusNPC()
	{
		if (this.me && this.npcFocus != null)
		{
			if (!GameCanvas.menu.showMenu)
			{
				global::Char.chatPopup = null;
			}
			this.npcFocus = null;
		}
	}

	// Token: 0x0600015B RID: 347 RVA: 0x000147A0 File Offset: 0x000129A0
	public void updateCharInBridge()
	{
		if (!GameCanvas.lowGraphic)
		{
			if (TileMap.tileTypeAt(this.cx, this.cy + 1, 1024))
			{
				TileMap.setTileTypeAtPixel(this.cx, this.cy + 1, 512);
				TileMap.setTileTypeAtPixel(this.cx, this.cy - 2, 512);
			}
			if (TileMap.tileTypeAt(this.cx - (int)TileMap.size, this.cy + 1, 512))
			{
				TileMap.killTileTypeAt(this.cx - (int)TileMap.size, this.cy + 1, 512);
				TileMap.killTileTypeAt(this.cx - (int)TileMap.size, this.cy - 2, 512);
			}
			if (TileMap.tileTypeAt(this.cx + (int)TileMap.size, this.cy + 1, 512))
			{
				TileMap.killTileTypeAt(this.cx + (int)TileMap.size, this.cy + 1, 512);
				TileMap.killTileTypeAt(this.cx + (int)TileMap.size, this.cy - 2, 512);
			}
		}
	}

	// Token: 0x0600015C RID: 348 RVA: 0x000148BC File Offset: 0x00012ABC
	public static void sort(int[] data)
	{
		int num = 5;
		for (int i = 0; i < num - 1; i++)
		{
			for (int j = i + 1; j < num; j++)
			{
				if (data[i] < data[j])
				{
					int num2 = data[j];
					data[j] = data[i];
					data[i] = num2;
				}
			}
		}
	}

	// Token: 0x0600015D RID: 349 RVA: 0x000148FD File Offset: 0x00012AFD
	public static bool setInsc(int cmX, int cmWx, int x, int cmy, int cmyH, int y)
	{
		return x <= cmWx && x >= cmX && y <= cmyH && y >= cmy;
	}

	// Token: 0x0600015E RID: 350 RVA: 0x00014918 File Offset: 0x00012B18
	public void kickOption(Item item, int maxKick)
	{
		int num = 0;
		if (item == null || item.options == null)
		{
			return;
		}
		for (int i = 0; i < item.options.size(); i++)
		{
			ItemOption itemOption = (ItemOption)item.options.elementAt(i);
			itemOption.active = 0;
			if (itemOption.optionTemplate.type == 2)
			{
				if (num < maxKick)
				{
					itemOption.active = 1;
					num++;
				}
			}
			else if (itemOption.optionTemplate.type == 3 && item.upgrade >= 4)
			{
				itemOption.active = 1;
			}
			else if (itemOption.optionTemplate.type == 4 && item.upgrade >= 8)
			{
				itemOption.active = 1;
			}
			else if (itemOption.optionTemplate.type == 5 && item.upgrade >= 12)
			{
				itemOption.active = 1;
			}
			else if (itemOption.optionTemplate.type == 6 && item.upgrade >= 14)
			{
				itemOption.active = 1;
			}
			else if (itemOption.optionTemplate.type == 7 && item.upgrade >= 16)
			{
				itemOption.active = 1;
			}
		}
	}

	// Token: 0x0600015F RID: 351 RVA: 0x00014A34 File Offset: 0x00012C34
	public void doInjure(int HPShow, int MPShow, bool isCrit, bool isMob)
	{
		this.isCrit = isCrit;
		this.isMob = isMob;
		Res.outz(string.Concat(new string[]
		{
			"CHP= ",
			this.cHP.ToString(),
			" dame -= ",
			HPShow.ToString(),
			" HP FULL= ",
			this.cHPFull.ToString()
		}));
		this.cHP -= HPShow;
		this.cMP -= MPShow;
		GameScr.gI().isInjureHp = true;
		GameScr.gI().twHp = 0;
		GameScr.gI().isInjureMp = true;
		GameScr.gI().twMp = 0;
		if (this.cHP < 0)
		{
			this.cHP = 0;
		}
		if (this.cMP < 0)
		{
			this.cMP = 0;
		}
		if (isMob || (!isMob && this.cTypePk != 4 && this.damMP != -100))
		{
			if (HPShow <= 0)
			{
				if (this.me)
				{
					GameScr.startFlyText(mResources.miss, this.cx, this.cy - this.ch, 0, -2, mFont.MISS_ME);
				}
				else
				{
					GameScr.startFlyText(mResources.miss, this.cx, this.cy - this.ch, 0, -2, mFont.MISS);
				}
			}
			else
			{
				GameScr.startFlyText("-" + HPShow.ToString(), this.cx, this.cy - this.ch, 0, -2, isCrit ? mFont.FATAL : mFont.RED);
			}
		}
		if (HPShow > 0)
		{
			this.isInjure = 6;
		}
		ServerEffect.addServerEffect(80, this, 1);
		if (this.isDie)
		{
			this.isDie = false;
			global::Char.isLockKey = false;
			this.startDie((short)this.xSd, (short)this.ySd);
		}
	}

	// Token: 0x06000160 RID: 352 RVA: 0x00014BFC File Offset: 0x00012DFC
	public void doInjure()
	{
		GameScr.gI().isInjureHp = true;
		GameScr.gI().twHp = 0;
		GameScr.gI().isInjureMp = true;
		GameScr.gI().twMp = 0;
		this.isInjure = 6;
		ServerEffect.addServerEffect(8, this, 1);
		this.isInjureHp = true;
		this.twHp = 0;
	}

	// Token: 0x06000161 RID: 353 RVA: 0x00014C54 File Offset: 0x00012E54
	public void startDie(short toX, short toY)
	{
		this.isMonkey = 0;
		this.isWaitMonkey = false;
		if (this.me && this.isDie)
		{
			return;
		}
		if (this.me)
		{
			this.isLockMove = true;
			for (int i = 0; i < GameScr.vCharInMap.size(); i++)
			{
				((global::Char)GameScr.vCharInMap.elementAt(i)).killCharId = -9999;
			}
			if (GameCanvas.panel != null && GameCanvas.panel.cp != null)
			{
				GameCanvas.panel.cp = null;
			}
			if (GameCanvas.panel2 != null && GameCanvas.panel2.cp != null)
			{
				GameCanvas.panel2.cp = null;
			}
		}
		this.statusMe = 5;
		this.cp2 = (int)toX;
		this.cp3 = (int)toY;
		this.cp1 = 0;
		this.cHP = 0;
		this.testCharId = -9999;
		this.killCharId = -9999;
		if (this.me && this.myskill != null && this.myskill.template.id != 14)
		{
			this.stopUseChargeSkill();
		}
		this.cTypePk = 0;
	}

	// Token: 0x06000162 RID: 354 RVA: 0x00014D64 File Offset: 0x00012F64
	public void waitToDie(short toX, short toY)
	{
		this.wdx = toX;
		this.wdy = toY;
	}

	// Token: 0x06000163 RID: 355 RVA: 0x00014D74 File Offset: 0x00012F74
	public void liveFromDead()
	{
		this.cHP = this.cHPFull;
		this.cMP = this.cMPFull;
		this.statusMe = 1;
		this.cp1 = (this.cp2 = (this.cp3 = 0));
		ServerEffect.addServerEffect(109, this, 2);
		GameScr.gI().center = null;
		GameScr.isHaveSelectSkill = true;
	}

	// Token: 0x06000164 RID: 356 RVA: 0x00014DD4 File Offset: 0x00012FD4
	public bool doUsePotion()
	{
		if (this.arrItemBag == null)
		{
			return false;
		}
		for (int i = 0; i < this.arrItemBag.Length; i++)
		{
			if (this.arrItemBag[i] != null && this.arrItemBag[i].template.type == 6)
			{
				Service.gI().useItem(0, 1, -1, this.arrItemBag[i].template.id);
				return true;
			}
		}
		return false;
	}

	// Token: 0x06000165 RID: 357 RVA: 0x00014E40 File Offset: 0x00013040
	public bool isLang()
	{
		return TileMap.mapID == 1 || TileMap.mapID == 27 || TileMap.mapID == 72 || TileMap.mapID == 10 || TileMap.mapID == 17 || TileMap.mapID == 22 || TileMap.mapID == 32 || TileMap.mapID == 38 || TileMap.mapID == 43 || TileMap.mapID == 48;
	}

	// Token: 0x06000166 RID: 358 RVA: 0x00014EAC File Offset: 0x000130AC
	public bool isMeCanAttackOtherPlayer(global::Char cAtt)
	{
		return cAtt != null && global::Char.myCharz().myskill != null && global::Char.myCharz().myskill.template.type != 2 && (global::Char.myCharz().myskill.template.type != 4 || cAtt.statusMe == 14 || cAtt.statusMe == 5) && (((cAtt.cTypePk == 3 && global::Char.myCharz().cTypePk == 3) || (global::Char.myCharz().cTypePk == 5 || cAtt.cTypePk == 5 || (global::Char.myCharz().cTypePk == 1 && cAtt.cTypePk == 1)) || (global::Char.myCharz().cTypePk == 4 && cAtt.cTypePk == 4) || (global::Char.myCharz().testCharId >= 0 && global::Char.myCharz().testCharId == cAtt.charID) || (global::Char.myCharz().killCharId >= 0 && global::Char.myCharz().killCharId == cAtt.charID && !this.isLang()) || (cAtt.killCharId >= 0 && cAtt.killCharId == global::Char.myCharz().charID && !this.isLang()) || (global::Char.myCharz().cFlag == 8 && cAtt.cFlag != 0) || (global::Char.myCharz().cFlag != 0 && cAtt.cFlag == 8) || (global::Char.myCharz().cFlag != cAtt.cFlag && global::Char.myCharz().cFlag != 0 && cAtt.cFlag != 0)) && cAtt.statusMe != 14) && cAtt.statusMe != 5;
	}

	// Token: 0x06000167 RID: 359 RVA: 0x00015048 File Offset: 0x00013248
	public void clearTask()
	{
		global::Char.myCharz().taskMaint = null;
		for (int i = 0; i < global::Char.myCharz().arrItemBag.Length; i++)
		{
			if (global::Char.myCharz().arrItemBag[i] != null && global::Char.myCharz().arrItemBag[i].template.type == 8)
			{
				global::Char.myCharz().arrItemBag[i] = null;
			}
		}
		Npc.clearEffTask();
	}

	// Token: 0x06000168 RID: 360 RVA: 0x000150B0 File Offset: 0x000132B0
	public int getX()
	{
		return this.cx;
	}

	// Token: 0x06000169 RID: 361 RVA: 0x000150B8 File Offset: 0x000132B8
	public int getY()
	{
		return this.cy;
	}

	// Token: 0x0600016A RID: 362 RVA: 0x000150C0 File Offset: 0x000132C0
	public int getH()
	{
		return 32;
	}

	// Token: 0x0600016B RID: 363 RVA: 0x000150C4 File Offset: 0x000132C4
	public int getW()
	{
		return 24;
	}

	// Token: 0x0600016C RID: 364 RVA: 0x000150C8 File Offset: 0x000132C8
	public void focusManualTo(object objectz)
	{
		if (objectz is Mob)
		{
			this.mobFocus = (Mob)objectz;
			this.deFocusNPC();
			this.charFocus = null;
			this.itemFocus = null;
		}
		else if (objectz is Npc)
		{
			global::Char.myCharz().mobFocus = null;
			global::Char.myCharz().deFocusNPC();
			global::Char.myCharz().npcFocus = (Npc)objectz;
			global::Char.myCharz().charFocus = null;
			global::Char.myCharz().itemFocus = null;
		}
		else if (objectz is global::Char)
		{
			global::Char.myCharz().mobFocus = null;
			global::Char.myCharz().deFocusNPC();
			global::Char.myCharz().charFocus = (global::Char)objectz;
			global::Char.myCharz().itemFocus = null;
		}
		else if (objectz is ItemMap)
		{
			global::Char.myCharz().mobFocus = null;
			global::Char.myCharz().deFocusNPC();
			global::Char.myCharz().charFocus = null;
			global::Char.myCharz().itemFocus = (ItemMap)objectz;
		}
		global::Char.isManualFocus = true;
	}

	// Token: 0x0600016D RID: 365 RVA: 0x00004887 File Offset: 0x00002A87
	public void stopMoving()
	{
	}

	// Token: 0x0600016E RID: 366 RVA: 0x00004887 File Offset: 0x00002A87
	public void cancelAttack()
	{
	}

	// Token: 0x0600016F RID: 367 RVA: 0x000151BF File Offset: 0x000133BF
	public bool isInvisible()
	{
		return false;
	}

	// Token: 0x06000170 RID: 368 RVA: 0x000151C2 File Offset: 0x000133C2
	public bool focusToAttack()
	{
		return this.mobFocus != null || (this.charFocus != null && this.isMeCanAttackOtherPlayer(this.charFocus));
	}

	// Token: 0x06000171 RID: 369 RVA: 0x000151E4 File Offset: 0x000133E4
	public void addDustEff(int type)
	{
		if (GameCanvas.lowGraphic)
		{
			return;
		}
		if (type == 1)
		{
			if (this.clevel >= 9)
			{
				EffecMn.addEff(new Effect(19, this.cx - 5, this.cy + 20, 2, 1, -1));
				return;
			}
		}
		else if (type == 2)
		{
			if ((!this.me || this.isMonkey != 1) && this.isNhapThe && GameCanvas.gameTick % 5 == 0)
			{
				EffecMn.addEff(new Effect(22, this.cx - 5, this.cy + 35, 2, 1, -1));
				return;
			}
		}
		else if (type == 3 && this.clevel >= 9 && this.ySd - this.cy <= 5)
		{
			EffecMn.addEff(new Effect(19, this.cx - 5, this.ySd + 20, 2, 1, -1));
		}
	}

	// Token: 0x06000172 RID: 370 RVA: 0x000152B0 File Offset: 0x000134B0
	public bool isGetFlagImage(sbyte getFlag)
	{
		bool flag = true;
		for (int i = 0; i < GameScr.vFlag.size(); i++)
		{
			PKFlag pkflag = (PKFlag)GameScr.vFlag.elementAt(i);
			if (pkflag != null)
			{
				if (pkflag.cflag == getFlag)
				{
					return true;
				}
				flag = false;
			}
		}
		return flag;
	}

	// Token: 0x06000173 RID: 371 RVA: 0x000152F8 File Offset: 0x000134F8
	internal void paintPKFlag(mGraphics g)
	{
		if (this.cdir == 1)
		{
			if (this.cFlag != 0 && this.cFlag != -1)
			{
				SmallImage.drawSmallImage(g, this.flagImage, this.cx - 10, this.cy - this.ch - ((!this.me) ? 30 : 30) + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), 2, 0);
				return;
			}
		}
		else if (this.cFlag != 0 && this.cFlag != -1)
		{
			SmallImage.drawSmallImage(g, this.flagImage, this.cx, this.cy - this.ch - ((!this.me) ? 30 : 30) + ((GameCanvas.gameTick % 20 > 10) ? (GameCanvas.gameTick % 4 / 2) : 0), 0, 0);
		}
	}

	// Token: 0x06000174 RID: 372 RVA: 0x000153CC File Offset: 0x000135CC
	[MethodImpl(MethodImplOptions.NoOptimization)]
	public void removeHoleEff()
	{
		if (this.holder)
		{
			this.holder = false;
			this.charHold = null;
			this.mobHold = null;
			return;
		}
		this.holdEffID = 0;
		this.charHold = null;
		this.mobHold = null;
	}

	// Token: 0x06000175 RID: 373 RVA: 0x00015401 File Offset: 0x00013601
	public void removeProtectEff()
	{
		this.protectEff = false;
		this.eProtect = null;
	}

	// Token: 0x06000176 RID: 374 RVA: 0x00015411 File Offset: 0x00013611
	public void removeBlindEff()
	{
		this.blindEff = false;
	}

	// Token: 0x06000177 RID: 375 RVA: 0x0001541C File Offset: 0x0001361C
	public void removeEffect()
	{
		if (this.holdEffID != 0)
		{
			this.holdEffID = 0;
		}
		if (this.holder)
		{
			this.holder = false;
		}
		if (this.protectEff)
		{
			this.protectEff = false;
		}
		this.eProtect = null;
		this.charHold = null;
		this.mobHold = null;
		this.blindEff = false;
		this.sleepEff = false;
	}

	// Token: 0x06000178 RID: 376 RVA: 0x0001547C File Offset: 0x0001367C
	public void setPos(short xPos, short yPos, sbyte typePos)
	{
		this.isSetPos = true;
		this.xPos = xPos;
		this.yPos = yPos;
		this.typePos = typePos;
		this.tpos = 0;
		if (this.me)
		{
			if (GameCanvas.panel != null)
			{
				GameCanvas.panel.hide();
			}
			if (GameCanvas.panel2 != null)
			{
				GameCanvas.panel2.hide();
			}
		}
	}

	// Token: 0x06000179 RID: 377 RVA: 0x000154D6 File Offset: 0x000136D6
	public void removeHuytSao()
	{
		this.huytSao = false;
	}

	// Token: 0x0600017A RID: 378 RVA: 0x000154DF File Offset: 0x000136DF
	public void fusionComplete()
	{
		this.isFusion = false;
		global::Char.isLockKey = false;
		this.tFusion = 0;
	}

	// Token: 0x0600017B RID: 379 RVA: 0x000154F8 File Offset: 0x000136F8
	public void setFusion(sbyte fusion)
	{
		this.tFusion = 0;
		if (fusion == 4 || fusion == 5)
		{
			if (this.me)
			{
				Service.gI().funsion(fusion);
			}
			EffecMn.addEff(new Effect(34, this.cx, this.cy + 12, 2, 1, -1));
		}
		if (fusion == 6)
		{
			EffecMn.addEff(new Effect(38, this.cx, this.cy + 12, 2, 1, -1));
		}
		if (this.me)
		{
			GameCanvas.panel.hideNow();
			global::Char.isLockKey = true;
		}
		this.isFusion = true;
		if (fusion == 1)
		{
			this.isNhapThe = false;
			return;
		}
		this.isNhapThe = true;
	}

	// Token: 0x0600017C RID: 380 RVA: 0x00015599 File Offset: 0x00013799
	public void removeSleepEff()
	{
		this.sleepEff = false;
	}

	// Token: 0x0600017D RID: 381 RVA: 0x000155A2 File Offset: 0x000137A2
	public void setPartOld()
	{
		this.headTemp = this.head;
		this.bodyTemp = this.body;
		this.legTemp = this.leg;
		this.bagTemp = this.bag;
	}

	// Token: 0x0600017E RID: 382 RVA: 0x000155D4 File Offset: 0x000137D4
	public void setPartTemp(int head, int body, int leg, int bag)
	{
		if (head != -1)
		{
			this.head = head;
		}
		if (body != -1)
		{
			this.body = body;
		}
		if (leg != -1)
		{
			this.leg = leg;
		}
		if (bag != -1)
		{
			this.bag = bag;
		}
	}

	// Token: 0x0600017F RID: 383 RVA: 0x00015604 File Offset: 0x00013804
	public void resetPartTemp()
	{
		if (this.headTemp != -1)
		{
			this.head = this.headTemp;
			this.headTemp = -1;
		}
		if (this.bodyTemp != -1)
		{
			this.body = this.bodyTemp;
			this.bodyTemp = -1;
		}
		if (this.legTemp != -1)
		{
			this.leg = this.legTemp;
			this.legTemp = -1;
		}
		if (this.bagTemp != -1)
		{
			this.bag = this.bagTemp;
			this.bagTemp = -1;
		}
	}

	// Token: 0x06000180 RID: 384 RVA: 0x00015684 File Offset: 0x00013884
	public Effect getEffById(int id)
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			Effect effect = (Effect)this.vEffChar.elementAt(i);
			if (effect.effId == id)
			{
				return effect;
			}
		}
		return null;
	}

	// Token: 0x06000181 RID: 385 RVA: 0x000156C5 File Offset: 0x000138C5
	public void addEffChar(Effect e)
	{
		this.removeEffChar(0, e.effId);
		this.vEffChar.addElement(e);
	}

	// Token: 0x06000182 RID: 386 RVA: 0x000156E0 File Offset: 0x000138E0
	public void removeEffChar(int type, int id)
	{
		if (type == -1)
		{
			this.vEffChar.removeAllElements();
			return;
		}
		if (this.getEffById(id) != null)
		{
			this.vEffChar.removeElement(this.getEffById(id));
		}
	}

	// Token: 0x06000183 RID: 387 RVA: 0x00015710 File Offset: 0x00013910
	public void paintEffBehind(mGraphics g)
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			Effect effect = (Effect)this.vEffChar.elementAt(i);
			if (effect.layer == 0)
			{
				bool flag = true;
				if (effect.isStand == 0)
				{
					flag = this.statusMe == 1 || this.statusMe == 6;
				}
				if (flag)
				{
					effect.paint(g);
				}
			}
		}
	}

	// Token: 0x06000184 RID: 388 RVA: 0x00015778 File Offset: 0x00013978
	public void paintEffFront(mGraphics g)
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			Effect effect = (Effect)this.vEffChar.elementAt(i);
			if (effect.layer == 1)
			{
				bool flag = true;
				if (effect.isStand == 0)
				{
					flag = this.statusMe == 1 || this.statusMe == 6;
				}
				if (flag)
				{
					effect.paint(g);
				}
			}
		}
	}

	// Token: 0x06000185 RID: 389 RVA: 0x000157E4 File Offset: 0x000139E4
	public void updEffChar()
	{
		for (int i = 0; i < this.vEffChar.size(); i++)
		{
			((Effect)this.vEffChar.elementAt(i)).update();
		}
	}

	// Token: 0x06000186 RID: 390 RVA: 0x0001581D File Offset: 0x00013A1D
	public int checkLuong()
	{
		return this.luong + this.luongKhoa;
	}

	// Token: 0x06000187 RID: 391 RVA: 0x0001582C File Offset: 0x00013A2C
	public void updateEye()
	{
		if (this.head != 934)
		{
			return;
		}
		if (GameCanvas.timeNow - this.timeAddChopmat > 0L)
		{
			this.fChopmat++;
			if (this.fChopmat > this.frEye.Length - 1)
			{
				this.fChopmat = 0;
				this.timeAddChopmat = GameCanvas.timeNow + (long)Res.random(2000, 3500);
				this.frEye = this.frChopCham;
				if (Res.random(2) == 0)
				{
					this.frEye = this.frChopNhanh;
					return;
				}
			}
		}
		else
		{
			this.fChopmat = 0;
		}
	}

	// Token: 0x06000188 RID: 392 RVA: 0x000158C4 File Offset: 0x00013AC4
	internal void paintRedEye(mGraphics g, int xx, int yy, int trans, int anchor)
	{
		if (this.head != 934 || (this.statusMe != 1 && this.statusMe != 6))
		{
			return;
		}
		if (global::Char.fraRedEye == null || global::Char.fraRedEye.imgFrame == null)
		{
			global::Char.fraRedEye = new FrameImage(mSystem.loadImage("/redeye.png"), 14, 10);
			return;
		}
		if (this.frEye[this.fChopmat] != -1)
		{
			int num = 8;
			int num2 = 15;
			if (trans == 2)
			{
				num = -8;
			}
			global::Char.fraRedEye.drawFrame(this.frEye[this.fChopmat], xx + num, yy + num2, trans, anchor, g);
		}
	}

	// Token: 0x06000189 RID: 393 RVA: 0x0001595C File Offset: 0x00013B5C
	public bool isHead_2Fr(int idHead)
	{
		for (int i = 0; i < global::Char.Arr_Head_2Fr.Length; i++)
		{
			if (global::Char.Arr_Head_2Fr[i][0] == idHead)
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x0600018A RID: 394 RVA: 0x0001598A File Offset: 0x00013B8A
	internal void updateFHead()
	{
		if (this.isHead_2Fr(this.head))
		{
			this.fHead++;
			if (this.fHead > 10000)
			{
				this.fHead = 0;
				return;
			}
		}
		else
		{
			this.fHead = 0;
		}
	}

	// Token: 0x0600018B RID: 395 RVA: 0x000159C4 File Offset: 0x00013BC4
	internal int getFHead(int idHead)
	{
		for (int i = 0; i < global::Char.Arr_Head_2Fr.Length; i++)
		{
			if (global::Char.Arr_Head_2Fr[i][0] == idHead)
			{
				return global::Char.Arr_Head_2Fr[i][this.fHead / 4 % global::Char.Arr_Head_2Fr[i].Length];
			}
		}
		return idHead;
	}

	// Token: 0x0600018C RID: 396 RVA: 0x00015A0C File Offset: 0x00013C0C
	public void paintAuraBehind(mGraphics g)
	{
		if ((!this.me || global::Char.isPaintAura) && this.idAuraEff > -1 && (this.statusMe == 1 || this.statusMe == 6) && !GameCanvas.panel.isShow && mSystem.currentTimeMillis() - this.timeBlue > 0L)
		{
			FrameImage fraImage = mSystem.getFraImage(this.strEffAura + this.idAuraEff.ToString() + "_0");
			if (fraImage != null)
			{
				fraImage.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, this.cx, this.cy, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
			}
		}
	}

	// Token: 0x0600018D RID: 397 RVA: 0x00015AC4 File Offset: 0x00013CC4
	public void paintAuraFront(mGraphics g)
	{
		if ((this.me && !global::Char.isPaintAura) || this.idAuraEff <= -1)
		{
			return;
		}
		if (this.statusMe == 1 || this.statusMe == 6)
		{
			if (!GameCanvas.panel.isShow && !GameCanvas.lowGraphic)
			{
				bool flag = false;
				if (mSystem.currentTimeMillis() - this.timeBlue > -1000L && this.IsAddDust1)
				{
					flag = true;
					this.IsAddDust1 = false;
				}
				if (mSystem.currentTimeMillis() - this.timeBlue > -500L && this.IsAddDust2)
				{
					flag = true;
					this.IsAddDust2 = false;
				}
				if (flag)
				{
					GameCanvas.gI().startDust(-1, this.cx - -8, this.cy);
					GameCanvas.gI().startDust(1, this.cx - 8, this.cy);
					this.addDustEff(1);
				}
				if (mSystem.currentTimeMillis() - this.timeBlue > 0L)
				{
					FrameImage fraImage = mSystem.getFraImage(this.strEffAura + this.idAuraEff.ToString() + "_1");
					if (fraImage != null)
					{
						fraImage.drawFrame(GameCanvas.gameTick / 4 % fraImage.nFrame, this.cx, this.cy + 2, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
						return;
					}
				}
			}
		}
		else
		{
			this.timeBlue = mSystem.currentTimeMillis() + 1500L;
			this.IsAddDust1 = true;
			this.IsAddDust2 = true;
		}
	}

	// Token: 0x0600018E RID: 398 RVA: 0x00015C38 File Offset: 0x00013E38
	public void paintEff_Lvup_behind(mGraphics g)
	{
		if (this.idEff_Set_Item != -1)
		{
			if (this.fraEff != null)
			{
				this.fraEff.drawFrame(GameCanvas.gameTick / 4 % this.fraEff.nFrame, this.cx, this.cy + 3, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
				return;
			}
			this.fraEff = mSystem.getFraImage(this.strEff_Set_Item + this.idEff_Set_Item.ToString() + "_0");
		}
	}

	// Token: 0x0600018F RID: 399 RVA: 0x00015CC4 File Offset: 0x00013EC4
	public void paintEff_Lvup_front(mGraphics g)
	{
		if (this.idEff_Set_Item != -1)
		{
			if (this.fraEffSub != null)
			{
				this.fraEffSub.drawFrame(GameCanvas.gameTick / 4 % this.fraEffSub.nFrame, this.cx, this.cy + 8, (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
				return;
			}
			this.fraEffSub = mSystem.getFraImage(this.strEff_Set_Item + this.idEff_Set_Item.ToString() + "_1");
		}
	}

	// Token: 0x06000190 RID: 400 RVA: 0x00015D50 File Offset: 0x00013F50
	public void paintHat_behind(mGraphics g, int cf, int yh)
	{
		try
		{
			if (this.idHat != -1)
			{
				if (this.isFrNgang(cf))
				{
					if (this.fraHat_behind_2 != null)
					{
						this.fraHat_behind_2.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_behind_2.nFrame, this.cx + global::Char.hatInfo[cf][0] * ((this.cdir == 1) ? 1 : (-1)), yh + global::Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						this.fraHat_behind_2 = mSystem.getFraImage(this.strHat_behind + this.strNgang + this.idHat.ToString());
					}
				}
				else if (this.fraHat_behind != null)
				{
					this.fraHat_behind.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_behind.nFrame, this.cx + global::Char.hatInfo[cf][0] * ((this.cdir == 1) ? 1 : (-1)), yh + global::Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
				}
				else
				{
					this.fraHat_behind = mSystem.getFraImage(this.strHat_behind + this.idHat.ToString());
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000191 RID: 401 RVA: 0x00015EBC File Offset: 0x000140BC
	public void paintHat_front(mGraphics g, int cf, int yh)
	{
		try
		{
			if (this.idHat != -1)
			{
				if (this.isFrNgang(cf))
				{
					if (this.fraHat_font_2 != null)
					{
						this.fraHat_font_2.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_font_2.nFrame, this.cx + global::Char.hatInfo[cf][0] * ((this.cdir == 1) ? 1 : (-1)), yh + global::Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
					}
					else
					{
						this.fraHat_font_2 = mSystem.getFraImage(this.strHat_font + this.strNgang + this.idHat.ToString());
					}
				}
				else if (this.fraHat_font != null)
				{
					this.fraHat_font.drawFrame(GameCanvas.gameTick / 4 % this.fraHat_font.nFrame, this.cx + global::Char.hatInfo[cf][0] * ((this.cdir == 1) ? 1 : (-1)), yh + global::Char.hatInfo[cf][1], (this.cdir != 1) ? 2 : 0, mGraphics.BOTTOM | mGraphics.HCENTER, g);
				}
				else
				{
					this.fraHat_font = mSystem.getFraImage(this.strHat_font + this.idHat.ToString());
				}
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x06000192 RID: 402 RVA: 0x00016028 File Offset: 0x00014228
	public bool isFrNgang(int fr)
	{
		return fr == 2 || fr == 3 || fr == 4 || fr == 5 || fr == 6 || fr == 9 || fr == 10 || fr == 13 || fr == 14 || fr == 15 || fr == 16 || fr == 26 || fr == 27 || fr == 28 || fr == 29;
	}

	// Token: 0x06000193 RID: 403 RVA: 0x00016080 File Offset: 0x00014280
	public void sendNewAttack(short idTemplateSkill)
	{
		short num = -1;
		short num2 = -1;
		if (this.mobFocus != null)
		{
			num = (short)this.mobFocus.x;
			num2 = (short)this.mobFocus.y;
		}
		if (this.charFocus != null && !this.charFocus.isPet && !this.charFocus.isMiniPet)
		{
			num = (short)this.charFocus.cx;
			num2 = (short)this.charFocus.cy;
		}
		Service.gI().new_skill_not_focus((sbyte)idTemplateSkill, (sbyte)this.cdir, num, num2);
	}

	// Token: 0x06000194 RID: 404 RVA: 0x00016104 File Offset: 0x00014304
	public void SetSkillPaint_NEW(short idskillPaint, bool isFly, sbyte typeFrame, sbyte typePaint, sbyte dir, short timeGong, sbyte typeItem)
	{
		this.isPaintNewSkill = true;
		this.timeReset_newSkill = GameCanvas.timeNow + 10000L;
		this.idskillPaint = idskillPaint;
		this.isFly = isFly;
		this.typeFrame = typeFrame;
		this.typePaint = typePaint;
		this.typeItem = typeItem;
		this.cdir = (int)dir;
		this.count_NEW = 0;
		this.stt = 0;
		long num = mSystem.currentTimeMillis();
		if (this.me)
		{
			this.saveLoadPreviousSkill();
			this.myskill.lastTimeUseThisSkill = num;
			if (this.myskill.template.manaUseType == 2)
			{
				this.cMP = 1;
			}
			else if (this.myskill.template.manaUseType != 1)
			{
				this.cMP -= this.myskill.manaUse;
			}
			else
			{
				this.cMP -= this.myskill.manaUse * this.cMPFull / 100;
			}
			global::Char.myCharz().cStamina--;
			GameScr.gI().isInjureMp = true;
			GameScr.gI().twMp = 0;
			if (this.cMP < 0)
			{
				this.cMP = 0;
			}
		}
		if (idskillPaint == 24)
		{
			GameScr.addEffectEnd_Target(18, 0, (int)typePaint, this.clone(), null, 3, timeGong, 0);
			GameScr.addEffectEnd_Target(21, 0, (int)typePaint, this.clone(), null, 1, timeGong, 0);
		}
		else if (idskillPaint == 25)
		{
			GameScr.addEffectEnd_Target(19, 0, (int)typePaint, this.clone(), null, 3, timeGong, 0);
			GameScr.addEffectEnd_Target(22, 0, (int)typePaint, this.clone(), null, 1, timeGong, 0);
		}
		else if (idskillPaint == 26)
		{
			GameScr.addEffectEnd_Target(20, 0, (int)typePaint, this.clone(), null, 3, timeGong, 0);
			GameScr.addEffectEnd_Target(23, 0, (int)typePaint, this.clone(), null, 1, timeGong, 0);
		}
		if (this.typeFrame == 1)
		{
			if (!this.isFly)
			{
				this.fr_start = new byte[] { 20, 20, 20, 20, 20, 20, 19 };
				this.fr_atk = new byte[] { 20 };
				this.fr_end = new byte[1];
			}
			else
			{
				this.fr_start = new byte[] { 31, 31, 31, 31, 31, 31, 30 };
				this.fr_atk = new byte[] { 31 };
				this.fr_end = new byte[] { 12 };
			}
		}
		if (this.typeFrame == 2)
		{
			if (!this.isFly)
			{
				this.fr_start = new byte[] { 20 };
				this.fr_atk = new byte[] { 13, 13, 13, 14, 14, 14 };
				this.fr_end = new byte[1];
			}
			else
			{
				this.fr_start = new byte[] { 31 };
				this.fr_atk = new byte[] { 26, 26, 26, 27, 27, 27 };
				this.fr_end = new byte[] { 12 };
			}
		}
		if (this.typeFrame == 4)
		{
			if (!this.isFly)
			{
				this.fr_start = new byte[] { 17, 17, 17, 18, 18, 18 };
				this.fr_atk = new byte[] { 18 };
				this.fr_end = new byte[1];
			}
			else
			{
				this.fr_start = new byte[] { 7, 7, 7, 12, 12, 12, 12 };
				this.fr_atk = new byte[] { 12 };
				this.fr_end = new byte[] { 12 };
			}
		}
		if (this.typeFrame == 3)
		{
			if (!this.isFly)
			{
				this.fr_start = new byte[] { 24, 24, 24, 17, 17, 17, 18, 18, 18 };
				this.fr_atk = new byte[] { 20 };
				this.fr_end = new byte[1];
				return;
			}
			this.fr_start = new byte[] { 23, 23, 23, 7, 7, 7, 12, 12, 12, 12 };
			this.fr_atk = new byte[] { 31 };
			this.fr_end = new byte[] { 12 };
		}
	}

	// Token: 0x06000195 RID: 405 RVA: 0x000164C0 File Offset: 0x000146C0
	public void SetSkillPaint_STT(int stt, short idskillPaint, Point targetDame, short timeDame, short rangeDame, sbyte typePaint, Point[] listObj, sbyte typeItem)
	{
		this.stt = stt;
		this.idskillPaint = idskillPaint;
		this.count_NEW = 0;
		this.targetDame = targetDame;
		this.typePaint = typePaint;
		this.timeDame = mSystem.currentTimeMillis() + (long)timeDame;
		this.rangeDame = rangeDame;
		this.typeItem = typeItem;
		if (this.stt == 1)
		{
			if (this.idskillPaint == 24)
			{
				GameScr.addEffectEnd_Target(18, 1, (int)typePaint, this, null, 3, timeDame, 0);
				GameScr.addEffectEnd_Target(24, 0, (int)typePaint, this, this.targetDame, 1, timeDame, rangeDame);
			}
			if (this.idskillPaint == 25)
			{
				GameScr.addEffectEnd_Target(19, 0, (int)typePaint, this, null, 3, timeDame, 0);
				GameScr.addEffectEnd_Target(25, 0, (int)typePaint, this, this.targetDame, 1, timeDame, rangeDame);
			}
			if (this.idskillPaint == 26)
			{
				GameScr.addEffectEnd_Target(20, 0, (int)typePaint, this, null, 3, timeDame, 0);
				GameScr.addEffectEnd(26, (int)typeItem, (int)typePaint, targetDame.x, targetDame.y, 1, 0, timeDame, listObj);
			}
		}
	}

	// Token: 0x06000196 RID: 406 RVA: 0x000165B4 File Offset: 0x000147B4
	public void UpdSkillPaint_NEW()
	{
		if (this.stt == 0)
		{
			if (this.isFly && this.count_NEW < 20)
			{
				this.cvy = -3;
				this.cy += this.cvy;
			}
			if (this.fr_start.Length == 1)
			{
				this.cf = (int)this.fr_start[0];
			}
			else if (this.count_NEW > this.fr_start.Length - 1)
			{
				this.cf = (int)this.fr_start[this.fr_start.Length - 1];
			}
			else
			{
				this.cf = (int)this.fr_start[this.count_NEW];
			}
		}
		else if (this.stt == 1)
		{
			this.cf = (int)this.fr_atk[this.count_NEW % this.fr_atk.Length];
			if (mSystem.currentTimeMillis() - this.timeDame > 0L)
			{
				this.SetSkillPaint_STT(2, 0, null, 0, 0, 0, null, 0);
			}
			if (this.count_NEW % 5 == 0)
			{
				GameScr.shock_scr = 5;
			}
			if (this.typeFrame == 1 && this.count_NEW < 10 && !TileMap.tileTypeAt(this.cx - (this.chw + 1) * this.cdir, this.cy, (this.cdir != 1) ? 4 : 8))
			{
				this.cx -= this.cdir;
			}
			if (this.typeFrame != 2)
			{
			}
		}
		else if (this.stt == 2)
		{
			if (this.fr_end.Length == 1)
			{
				this.cf = (int)this.fr_end[0];
			}
			else if (this.count_NEW > this.fr_end.Length - 1)
			{
				this.cf = (int)this.fr_end[this.fr_end.Length - 1];
			}
			else
			{
				this.cf = (int)this.fr_end[this.count_NEW];
			}
			if (this.isFly)
			{
				this.cvx = (this.cvy = 0);
				this.statusMe = 4;
			}
			this.isPaintNewSkill = false;
		}
		this.count_NEW++;
	}

	// Token: 0x06000197 RID: 407 RVA: 0x000167B0 File Offset: 0x000149B0
	public global::Char clone()
	{
		global::Char @char = new global::Char();
		@char.charID = this.charID;
		@char.cx = this.cx;
		@char.cy = this.cy;
		@char.cdir = this.cdir;
		if (this.arrItemBody != null)
		{
			@char.arrItemBody = new Item[this.arrItemBody.Length];
			for (int i = 0; i < this.arrItemBody.Length; i++)
			{
				if (this.arrItemBody[i] == null)
				{
					@char.arrItemBody[i] = null;
				}
				else
				{
					@char.arrItemBody[i] = this.arrItemBody[i].clone();
				}
			}
		}
		return @char;
	}

	// Token: 0x06000198 RID: 408 RVA: 0x0001684C File Offset: 0x00014A4C
	public bool containsCaiTrang(int v)
	{
		if (this.arrItemBody != null)
		{
			for (int i = 0; i < this.arrItemBody.Length; i++)
			{
				if (this.arrItemBody[i] != null && this.arrItemBody[i].template != null && (int)this.arrItemBody[i].template.id == v)
				{
					return true;
				}
			}
		}
		Res.err("tim kiem id cai trang " + v.ToString() + " ko tim thay");
		return false;
	}

	// Token: 0x06000199 RID: 409 RVA: 0x000168C0 File Offset: 0x00014AC0
	public void printlog()
	{
		Res.outz(string.Empty + "isInjure " + this.isInjure.ToString() + "\n" + "isInjure " + this.isMonkey.ToString() + "\n" + "isInjure " + this.isAddChopMat.ToString() + "\n" + "isInjure " + this.isAttack.ToString() + "\n" + "isInjure " + this.isAttFly.ToString() + "\n" + "isInjure " + global::Char.ischangingMap.ToString() + "\n" + "isInjure " + this.isCharge.ToString() + "\n" + "isInjure " + this.isCopy.ToString() + "\n" + "isInjure " + this.isCreateDark.ToString() + "\n" + "isInjure " + this.isCrit.ToString() + "\n" + "isInjure " + this.isDirtyPostion.ToString() + "\n" + "isInjure " + this.isEndMount.ToString() + "\n" + "isInjure " + this.isEventMount.ToString() + "\n" + "isInjure " + this.isMafuba.ToString() + "\n" + "isInjure " + this.isFusion.ToString() + "\n" + "isInjure " + this.isFeetEff.ToString() + "\n" + "isInjure " + this.isFlying.ToString() + "\n" + "isInjure " + this.isWaitMonkey.ToString() + "\n" + "isInjure " + this.isUseSkillSpec().ToString() + "\n" + "isInjure " + this.isDie.ToString() + "\n" + "isInjure " + this.isDie.ToString() + "\n" + "isInjure " + this.isDie.ToString() + "\n" + "isInjure " + this.isDie.ToString() + "\n");
	}

	// Token: 0x0600019A RID: 410 RVA: 0x00016B30 File Offset: 0x00014D30
	public void setDanhHieu(int smallDanhHieu, int frame)
	{
		if (this.mainImg == null)
		{
			this.mainImg = ImgByName.getImagePath("banner_" + 0.ToString(), ImgByName.hashImagePath);
		}
		if (this.mainImg.img != null)
		{
			int num = this.mainImg.img.getHeight() / (int)this.mainImg.nFrame;
			if (num < 1)
			{
				num = 1;
			}
			this.fraDanhHieu = new FrameImage(this.mainImg.img, this.mainImg.img.getWidth(), num);
		}
		Res.err("===== tim thay DanhHieu ve danh hieu ra");
	}

	// Token: 0x0600019B RID: 411 RVA: 0x00016BD0 File Offset: 0x00014DD0
	// Note: this type is marked as 'beforefieldinit'.
	static Char()
	{
		int[][] array = new int[32][];
		array[0] = new int[] { 5, -7 };
		array[1] = new int[] { 5, -7 };
		array[2] = new int[] { 5, -8 };
		array[3] = new int[] { 5, -7 };
		array[4] = new int[] { 5, -6 };
		array[5] = new int[] { 5, -8 };
		array[6] = new int[] { 5, -7 };
		int num = 7;
		int[] array2 = new int[2];
		array2[0] = 9;
		array[num] = array2;
		array[8] = new int[] { 11, 1 };
		int num2 = 9;
		int[] array3 = new int[2];
		array3[0] = 4;
		array[num2] = array3;
		array[10] = new int[] { 4, -1 };
		array[11] = new int[] { 4, 8 };
		array[12] = new int[] { 6, 5 };
		array[13] = new int[] { 6, -6 };
		array[14] = new int[] { 2, -5 };
		array[15] = new int[] { 7, -8 };
		array[16] = new int[] { 7, -6 };
		int num3 = 17;
		int[] array4 = new int[2];
		array4[0] = 8;
		array[num3] = array4;
		array[18] = new int[] { 7, 5 };
		array[19] = new int[] { 9, -7 };
		array[20] = new int[] { 7, -3 };
		array[21] = new int[] { 2, 8 };
		array[22] = new int[] { 4, 5 };
		array[23] = new int[] { 10, -5 };
		array[24] = new int[] { 9, -5 };
		array[25] = new int[] { 9, -5 };
		array[26] = new int[] { 6, -6 };
		array[27] = new int[] { 2, -5 };
		array[28] = new int[] { 7, -8 };
		array[29] = new int[] { 7, -6 };
		array[30] = new int[] { 9, -7 };
		array[31] = new int[] { 7, -3 };
		global::Char.hatInfo = array;
		global::Char.Arr_Head_FlyMove = new short[0];
	}

	// Token: 0x04000120 RID: 288
	internal CharEffectTime charEffectTime = new CharEffectTime();

	// Token: 0x04000121 RID: 289
	public string xuStr;

	// Token: 0x04000122 RID: 290
	public string luongStr;

	// Token: 0x04000123 RID: 291
	public string luongKhoaStr;

	// Token: 0x04000124 RID: 292
	public long lastUpdateTime;

	// Token: 0x04000125 RID: 293
	public bool meLive;

	// Token: 0x04000126 RID: 294
	public bool isMask;

	// Token: 0x04000127 RID: 295
	public bool isTeleport;

	// Token: 0x04000128 RID: 296
	public bool isUsePlane;

	// Token: 0x04000129 RID: 297
	public int shadowX;

	// Token: 0x0400012A RID: 298
	public int shadowY;

	// Token: 0x0400012B RID: 299
	public int shadowLife;

	// Token: 0x0400012C RID: 300
	public bool isNhapThe;

	// Token: 0x0400012D RID: 301
	public PetFollow petFollow;

	// Token: 0x0400012E RID: 302
	public int rank;

	// Token: 0x0400012F RID: 303
	public const sbyte A_STAND = 1;

	// Token: 0x04000130 RID: 304
	public const sbyte A_RUN = 2;

	// Token: 0x04000131 RID: 305
	public const sbyte A_JUMP = 3;

	// Token: 0x04000132 RID: 306
	public const sbyte A_FALL = 4;

	// Token: 0x04000133 RID: 307
	public const sbyte A_DEADFLY = 5;

	// Token: 0x04000134 RID: 308
	public const sbyte A_NOTHING = 6;

	// Token: 0x04000135 RID: 309
	public const sbyte A_ATTK = 7;

	// Token: 0x04000136 RID: 310
	public const sbyte A_INJURE = 8;

	// Token: 0x04000137 RID: 311
	public const sbyte A_AUTOJUMP = 9;

	// Token: 0x04000138 RID: 312
	public const sbyte A_FLY = 10;

	// Token: 0x04000139 RID: 313
	public const sbyte SKILL_STAND = 12;

	// Token: 0x0400013A RID: 314
	public const sbyte SKILL_FALL = 13;

	// Token: 0x0400013B RID: 315
	public const sbyte A_DEAD = 14;

	// Token: 0x0400013C RID: 316
	public const sbyte A_HIDE = 15;

	// Token: 0x0400013D RID: 317
	public const sbyte A_RESETPOINT = 16;

	// Token: 0x0400013E RID: 318
	public static ChatPopup chatPopup;

	// Token: 0x0400013F RID: 319
	public long cPower;

	// Token: 0x04000140 RID: 320
	public Info chatInfo;

	// Token: 0x04000141 RID: 321
	public sbyte petStatus;

	// Token: 0x04000142 RID: 322
	public int cx = 24;

	// Token: 0x04000143 RID: 323
	public int cy = 24;

	// Token: 0x04000144 RID: 324
	public int cvx;

	// Token: 0x04000145 RID: 325
	public int cvy;

	// Token: 0x04000146 RID: 326
	public int cp1;

	// Token: 0x04000147 RID: 327
	public int cp2;

	// Token: 0x04000148 RID: 328
	public int cp3;

	// Token: 0x04000149 RID: 329
	public int statusMe = 5;

	// Token: 0x0400014A RID: 330
	public int cdir = 1;

	// Token: 0x0400014B RID: 331
	public int charID;

	// Token: 0x0400014C RID: 332
	public int cgender;

	// Token: 0x0400014D RID: 333
	public int ctaskId;

	// Token: 0x0400014E RID: 334
	public int menuSelect;

	// Token: 0x0400014F RID: 335
	public int cBonusSpeed;

	// Token: 0x04000150 RID: 336
	public int cspeed = 4;

	// Token: 0x04000151 RID: 337
	public int ccurrentAttack;

	// Token: 0x04000152 RID: 338
	public int cDamFull;

	// Token: 0x04000153 RID: 339
	public int cDefull;

	// Token: 0x04000154 RID: 340
	public int cCriticalFull;

	// Token: 0x04000155 RID: 341
	public int clevel;

	// Token: 0x04000156 RID: 342
	public int cMP;

	// Token: 0x04000157 RID: 343
	public int cHP;

	// Token: 0x04000158 RID: 344
	public int cHPNew;

	// Token: 0x04000159 RID: 345
	public int cMaxEXP;

	// Token: 0x0400015A RID: 346
	public int cHPShow;

	// Token: 0x0400015B RID: 347
	public int xReload;

	// Token: 0x0400015C RID: 348
	public int yReload;

	// Token: 0x0400015D RID: 349
	public int cyStartFall;

	// Token: 0x0400015E RID: 350
	public int saveStatus;

	// Token: 0x0400015F RID: 351
	public int eff5BuffHp;

	// Token: 0x04000160 RID: 352
	public int eff5BuffMp;

	// Token: 0x04000161 RID: 353
	public int cHPFull;

	// Token: 0x04000162 RID: 354
	public int cMPFull;

	// Token: 0x04000163 RID: 355
	public int cdameDown;

	// Token: 0x04000164 RID: 356
	public int cStr;

	// Token: 0x04000165 RID: 357
	public long cLevelPercent;

	// Token: 0x04000166 RID: 358
	public long cTiemNang;

	// Token: 0x04000167 RID: 359
	public long cNangdong;

	// Token: 0x04000168 RID: 360
	public int damHP;

	// Token: 0x04000169 RID: 361
	public int damMP;

	// Token: 0x0400016A RID: 362
	public bool isMob;

	// Token: 0x0400016B RID: 363
	public bool isCrit;

	// Token: 0x0400016C RID: 364
	public bool isDie;

	// Token: 0x0400016D RID: 365
	public int pointUydanh;

	// Token: 0x0400016E RID: 366
	public int pointNon;

	// Token: 0x0400016F RID: 367
	public int pointVukhi;

	// Token: 0x04000170 RID: 368
	public int pointAo;

	// Token: 0x04000171 RID: 369
	public int pointLien;

	// Token: 0x04000172 RID: 370
	public int pointGangtay;

	// Token: 0x04000173 RID: 371
	public int pointNhan;

	// Token: 0x04000174 RID: 372
	public int pointQuan;

	// Token: 0x04000175 RID: 373
	public int pointNgocboi;

	// Token: 0x04000176 RID: 374
	public int pointGiay;

	// Token: 0x04000177 RID: 375
	public int pointPhu;

	// Token: 0x04000178 RID: 376
	public int countFinishDay;

	// Token: 0x04000179 RID: 377
	public int countLoopBoos;

	// Token: 0x0400017A RID: 378
	public int limitTiemnangso;

	// Token: 0x0400017B RID: 379
	public int limitKynangso;

	// Token: 0x0400017C RID: 380
	public short[] potential = new short[4];

	// Token: 0x0400017D RID: 381
	public string cName = string.Empty;

	// Token: 0x0400017E RID: 382
	public int clanID;

	// Token: 0x0400017F RID: 383
	public sbyte ctypeClan;

	// Token: 0x04000180 RID: 384
	public Clan clan;

	// Token: 0x04000181 RID: 385
	public sbyte role;

	// Token: 0x04000182 RID: 386
	public int cw = 22;

	// Token: 0x04000183 RID: 387
	public int ch = 32;

	// Token: 0x04000184 RID: 388
	public int chw = 11;

	// Token: 0x04000185 RID: 389
	public int chh = 16;

	// Token: 0x04000186 RID: 390
	public Command cmdMenu;

	// Token: 0x04000187 RID: 391
	public bool canFly = true;

	// Token: 0x04000188 RID: 392
	public bool cmtoChar;

	// Token: 0x04000189 RID: 393
	public bool me;

	// Token: 0x0400018A RID: 394
	public bool cFinishedAttack;

	// Token: 0x0400018B RID: 395
	public bool cchistlast;

	// Token: 0x0400018C RID: 396
	public bool isAttack;

	// Token: 0x0400018D RID: 397
	public bool isAttFly;

	// Token: 0x0400018E RID: 398
	public int cwpt;

	// Token: 0x0400018F RID: 399
	public int cwplv;

	// Token: 0x04000190 RID: 400
	public int cf;

	// Token: 0x04000191 RID: 401
	public int tick;

	// Token: 0x04000192 RID: 402
	public static bool fallAttack;

	// Token: 0x04000193 RID: 403
	public bool isJump;

	// Token: 0x04000194 RID: 404
	public bool autoFall;

	// Token: 0x04000195 RID: 405
	public bool attack = true;

	// Token: 0x04000196 RID: 406
	public long xu;

	// Token: 0x04000197 RID: 407
	public int xuInBox;

	// Token: 0x04000198 RID: 408
	public int yen;

	// Token: 0x04000199 RID: 409
	public int gold_lock;

	// Token: 0x0400019A RID: 410
	public int luong;

	// Token: 0x0400019B RID: 411
	public int luongKhoa;

	// Token: 0x0400019C RID: 412
	public NClass nClass;

	// Token: 0x0400019D RID: 413
	public Command endMovePointCommand;

	// Token: 0x0400019E RID: 414
	public MyVector vSkill = new MyVector();

	// Token: 0x0400019F RID: 415
	public MyVector vSkillFight = new MyVector();

	// Token: 0x040001A0 RID: 416
	public MyVector vEff = new MyVector();

	// Token: 0x040001A1 RID: 417
	public Skill myskill;

	// Token: 0x040001A2 RID: 418
	public Task taskMaint;

	// Token: 0x040001A3 RID: 419
	public bool paintName = true;

	// Token: 0x040001A4 RID: 420
	public Archivement[] arrArchive;

	// Token: 0x040001A5 RID: 421
	public Item[] arrItemBag;

	// Token: 0x040001A6 RID: 422
	public Item[] arrItemBox;

	// Token: 0x040001A7 RID: 423
	public Item[] arrItemBody;

	// Token: 0x040001A8 RID: 424
	public Skill[] arrPetSkill;

	// Token: 0x040001A9 RID: 425
	public Item[][] arrItemShop;

	// Token: 0x040001AA RID: 426
	public string[][] infoSpeacialSkill;

	// Token: 0x040001AB RID: 427
	public short[][] imgSpeacialSkill;

	// Token: 0x040001AC RID: 428
	public short cResFire;

	// Token: 0x040001AD RID: 429
	public short cResIce;

	// Token: 0x040001AE RID: 430
	public short cResWind;

	// Token: 0x040001AF RID: 431
	public short cMiss;

	// Token: 0x040001B0 RID: 432
	public short cExactly;

	// Token: 0x040001B1 RID: 433
	public short cFatal;

	// Token: 0x040001B2 RID: 434
	public sbyte cPk;

	// Token: 0x040001B3 RID: 435
	public sbyte cTypePk;

	// Token: 0x040001B4 RID: 436
	public short cReactDame;

	// Token: 0x040001B5 RID: 437
	public short sysUp;

	// Token: 0x040001B6 RID: 438
	public short sysDown;

	// Token: 0x040001B7 RID: 439
	public int avatar;

	// Token: 0x040001B8 RID: 440
	public int skillTemplateId;

	// Token: 0x040001B9 RID: 441
	public Mob mobFocus;

	// Token: 0x040001BA RID: 442
	public Mob mobMe;

	// Token: 0x040001BB RID: 443
	public int tMobMeBorn;

	// Token: 0x040001BC RID: 444
	public Npc npcFocus;

	// Token: 0x040001BD RID: 445
	public global::Char charFocus;

	// Token: 0x040001BE RID: 446
	public ItemMap itemFocus;

	// Token: 0x040001BF RID: 447
	public MyVector focus = new MyVector();

	// Token: 0x040001C0 RID: 448
	public Mob[] attMobs;

	// Token: 0x040001C1 RID: 449
	public global::Char[] attChars;

	// Token: 0x040001C2 RID: 450
	public short[] moveFast;

	// Token: 0x040001C3 RID: 451
	public int testCharId = -9999;

	// Token: 0x040001C4 RID: 452
	public int killCharId = -9999;

	// Token: 0x040001C5 RID: 453
	public sbyte resultTest;

	// Token: 0x040001C6 RID: 454
	public int countKill;

	// Token: 0x040001C7 RID: 455
	public int countKillMax;

	// Token: 0x040001C8 RID: 456
	public bool isInvisiblez;

	// Token: 0x040001C9 RID: 457
	public bool isShadown = true;

	// Token: 0x040001CA RID: 458
	public const sbyte PK_NORMAL = 0;

	// Token: 0x040001CB RID: 459
	public const sbyte PK_PHE = 1;

	// Token: 0x040001CC RID: 460
	public const sbyte PK_BANG = 2;

	// Token: 0x040001CD RID: 461
	public const sbyte PK_THIDAU = 3;

	// Token: 0x040001CE RID: 462
	public const sbyte PK_LUYENTAP = 4;

	// Token: 0x040001CF RID: 463
	public const sbyte PK_TUDO = 5;

	// Token: 0x040001D0 RID: 464
	public MyVector taskOrders = new MyVector();

	// Token: 0x040001D1 RID: 465
	public int cStamina;

	// Token: 0x040001D2 RID: 466
	public static short[] idHead;

	// Token: 0x040001D3 RID: 467
	public static short[] idAvatar;

	// Token: 0x040001D4 RID: 468
	public int exp;

	// Token: 0x040001D5 RID: 469
	public string[] strLevel;

	// Token: 0x040001D6 RID: 470
	public string currStrLevel;

	// Token: 0x040001D7 RID: 471
	public static Image eyeTraiDat = GameCanvas.loadImage("/mainImage/myTexture2dmat-trai-dat.png");

	// Token: 0x040001D8 RID: 472
	public static Image eyeNamek = GameCanvas.loadImage("/mainImage/myTexture2dmat-namek.png");

	// Token: 0x040001D9 RID: 473
	public bool isFreez;

	// Token: 0x040001DA RID: 474
	public bool isCharge;

	// Token: 0x040001DB RID: 475
	public int seconds;

	// Token: 0x040001DC RID: 476
	public int freezSeconds;

	// Token: 0x040001DD RID: 477
	public long last;

	// Token: 0x040001DE RID: 478
	public long cur;

	// Token: 0x040001DF RID: 479
	public long lastFreez;

	// Token: 0x040001E0 RID: 480
	public long currFreez;

	// Token: 0x040001E1 RID: 481
	public bool isFlyUp;

	// Token: 0x040001E2 RID: 482
	public static MyVector vItemTime = new MyVector();

	// Token: 0x040001E3 RID: 483
	public static short ID_NEW_MOUNT = 30000;

	// Token: 0x040001E4 RID: 484
	public short idMount;

	// Token: 0x040001E5 RID: 485
	public bool isHaveMount;

	// Token: 0x040001E6 RID: 486
	public bool isMountVip;

	// Token: 0x040001E7 RID: 487
	public bool isEventMount;

	// Token: 0x040001E8 RID: 488
	public bool isSpeacialMount;

	// Token: 0x040001E9 RID: 489
	public static Image imgMount_TD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi10.png");

	// Token: 0x040001EA RID: 490
	public static Image imgMount_NM = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi20.png");

	// Token: 0x040001EB RID: 491
	public static Image imgMount_NM_1 = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi21.png");

	// Token: 0x040001EC RID: 492
	public static Image imgMount_XD = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi30.png");

	// Token: 0x040001ED RID: 493
	public static Image imgMount_TD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi11.png");

	// Token: 0x040001EE RID: 494
	public static Image imgMount_NM_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi22.png");

	// Token: 0x040001EF RID: 495
	public static Image imgMount_NM_1_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi23.png");

	// Token: 0x040001F0 RID: 496
	public static Image imgMount_XD_VIP = GameCanvas.loadImage("/mainImage/myTexture2dthucuoi31.png");

	// Token: 0x040001F1 RID: 497
	public static Image imgEventMount = GameCanvas.loadImage("/mainImage/myTexture2drong.png");

	// Token: 0x040001F2 RID: 498
	public static Image imgEventMountWing = GameCanvas.loadImage("/mainImage/myTexture2dcanhrong.png");

	// Token: 0x040001F3 RID: 499
	public sbyte[] FrameMount = new sbyte[] { 0, 0, 1, 1, 2, 2, 1, 1 };

	// Token: 0x040001F4 RID: 500
	public int frameMount;

	// Token: 0x040001F5 RID: 501
	public int frameNewMount;

	// Token: 0x040001F6 RID: 502
	public int transMount;

	// Token: 0x040001F7 RID: 503
	public int genderMount;

	// Token: 0x040001F8 RID: 504
	public int idcharMount;

	// Token: 0x040001F9 RID: 505
	public int xMount;

	// Token: 0x040001FA RID: 506
	public int yMount;

	// Token: 0x040001FB RID: 507
	public int dxMount;

	// Token: 0x040001FC RID: 508
	public int dyMount;

	// Token: 0x040001FD RID: 509
	public int xChar;

	// Token: 0x040001FE RID: 510
	public int xdis;

	// Token: 0x040001FF RID: 511
	public int speedMount;

	// Token: 0x04000200 RID: 512
	public bool isStartMount;

	// Token: 0x04000201 RID: 513
	public bool isMount;

	// Token: 0x04000202 RID: 514
	public bool isEndMount;

	// Token: 0x04000203 RID: 515
	public sbyte cFlag;

	// Token: 0x04000204 RID: 516
	public int flagImage;

	// Token: 0x04000205 RID: 517
	public short x_hint;

	// Token: 0x04000206 RID: 518
	public short y_hint;

	// Token: 0x04000207 RID: 519
	public short s_danhHieu1;

	// Token: 0x04000208 RID: 520
	public static int[][][] CharInfo = new int[][][]
	{
		new int[][]
		{
			new int[] { 0, -13, 34 },
			new int[] { 1, -8, 10 },
			new int[] { 1, -9, 16 },
			new int[] { 1, -9, 45 }
		},
		new int[][]
		{
			new int[] { 0, -13, 35 },
			new int[] { 1, -8, 10 },
			new int[] { 1, -9, 17 },
			new int[] { 1, -9, 46 }
		},
		new int[][]
		{
			new int[] { 1, -10, 33 },
			new int[] { 2, -10, 11 },
			new int[] { 2, -8, 16 },
			new int[] { 1, -12, 49 }
		},
		new int[][]
		{
			new int[] { 1, -10, 32 },
			new int[] { 3, -12, 10 },
			new int[] { 3, -11, 15 },
			new int[] { 1, -13, 47 }
		},
		new int[][]
		{
			new int[] { 1, -10, 34 },
			new int[] { 4, -8, 11 },
			new int[] { 4, -7, 17 },
			new int[] { 1, -12, 47 }
		},
		new int[][]
		{
			new int[] { 1, -10, 34 },
			new int[] { 5, -12, 11 },
			new int[] { 5, -9, 17 },
			new int[] { 1, -13, 49 }
		},
		new int[][]
		{
			new int[] { 1, -10, 33 },
			new int[] { 6, -10, 10 },
			new int[] { 6, -8, 16 },
			new int[] { 1, -12, 47 }
		},
		new int[][]
		{
			new int[] { 0, -9, 36 },
			new int[] { 7, -5, 17 },
			new int[] { 7, -11, 25 },
			new int[] { 1, -8, 49 }
		},
		new int[][]
		{
			new int[] { 0, -7, 35 },
			new int[] { 0, -18, 22 },
			new int[] { 7, -10, 25 },
			new int[] { 1, -7, 48 }
		},
		new int[][]
		{
			new int[] { 1, -11, 35 },
			new int[] { 10, -3, 25 },
			new int[] { 12, -10, 26 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 1, -11, 37 },
			new int[] { 11, -3, 25 },
			new int[] { 12, -11, 27 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -14, 34 },
			new int[] { 12, -8, 21 },
			new int[] { 9, -7, 31 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -12, 35 },
			new int[] { 8, -5, 14 },
			new int[] { 8, -15, 29 },
			new int[] { 1, -9, 49 }
		},
		new int[][]
		{
			new int[] { 1, -9, 34 },
			new int[] { 9, -12, 9 },
			new int[] { 10, -7, 19 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 1, -13, 34 },
			new int[] { 9, -12, 9 },
			new int[] { 11, -10, 19 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 1, -8, 32 },
			new int[] { 9, -12, 9 },
			new int[] { 2, -6, 15 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 1, -8, 32 },
			new int[] { 9, -12, 9 },
			new int[] { 13, -12, 16 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -10, 31 },
			new int[] { 9, -12, 9 },
			new int[] { 7, -13, 20 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -11, 32 },
			new int[] { 9, -12, 9 },
			new int[] { 8, -15, 26 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -9, 33 },
			new int[] { 9, -12, 9 },
			new int[] { 14, -8, 18 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -11, 33 },
			new int[] { 9, -12, 9 },
			new int[] { 15, -6, 19 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -16, 31 },
			new int[] { 9, -12, 9 },
			new int[] { 9, -8, 28 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -14, 34 },
			new int[] { 1, -8, 10 },
			new int[] { 8, -16, 28 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -8, 36 },
			new int[] { 7, -5, 17 },
			new int[] { 0, -5, 25 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -9, 31 },
			new int[] { 9, -12, 9 },
			new int[] { 0, -6, 20 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 2, -9, 36 },
			new int[] { 13, -5, 17 },
			new int[] { 16, -11, 25 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 1, -9, 34 },
			new int[] { 8, -5, 13 },
			new int[] { 10, -7, 19 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 1, -13, 34 },
			new int[] { 8, -5, 13 },
			new int[] { 11, -10, 19 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 1, -8, 32 },
			new int[] { 8, -5, 13 },
			new int[] { 2, -6, 15 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 1, -8, 32 },
			new int[] { 8, -5, 13 },
			new int[] { 13, -12, 16 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -9, 33 },
			new int[] { 8, -5, 13 },
			new int[] { 14, -8, 18 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -11, 33 },
			new int[] { 8, -5, 13 },
			new int[] { 15, -6, 19 },
			new int[3]
		},
		new int[][]
		{
			new int[] { 0, -16, 32 },
			new int[] { 8, -5, 13 },
			new int[] { 9, -8, 29 },
			new int[3]
		}
	};

	// Token: 0x04000209 RID: 521
	public static int[] CHAR_WEAPONX = new int[]
	{
		-2, -6, 22, 21, 19, 22, 10, -2, -2, 5,
		19
	};

	// Token: 0x0400020A RID: 522
	public static int[] CHAR_WEAPONY = new int[]
	{
		9, 22, 25, 17, 26, 37, 36, 49, 50, 52,
		36
	};

	// Token: 0x0400020B RID: 523
	internal static global::Char myChar;

	// Token: 0x0400020C RID: 524
	internal static global::Char myPet;

	// Token: 0x0400020D RID: 525
	public static int[] listAttack;

	// Token: 0x0400020E RID: 526
	public static int[][] listIonC;

	// Token: 0x0400020F RID: 527
	public int cvyJump;

	// Token: 0x04000210 RID: 528
	internal int indexUseSkill = -1;

	// Token: 0x04000211 RID: 529
	public int cxSend;

	// Token: 0x04000212 RID: 530
	public int cySend;

	// Token: 0x04000213 RID: 531
	public int cdirSend = 1;

	// Token: 0x04000214 RID: 532
	public int cxFocus;

	// Token: 0x04000215 RID: 533
	public int cyFocus;

	// Token: 0x04000216 RID: 534
	public int cactFirst = 5;

	// Token: 0x04000217 RID: 535
	public MyVector vMovePoints = new MyVector();

	// Token: 0x04000218 RID: 536
	public static string[][] inforClass = new string[][]
	{
		new string[] { "1", "1", "chiêu 1", "0" },
		new string[] { "2", "2", "chiêu 2", "5" }
	};

	// Token: 0x04000219 RID: 537
	public static int[][] inforSkill = new int[][]
	{
		new int[]
		{
			1, 0, 1, 1000, 40, 1, 0, 20, 0, 0,
			0, 0
		},
		new int[]
		{
			2, 1, 10, 1000, 100, 1, 0, 40, 0, 0,
			0, 0
		},
		new int[]
		{
			2, 2, 11, 800, 100, 1, 0, 45, 0, 0,
			0, 0
		},
		new int[]
		{
			2, 3, 12, 600, 100, 1, 0, 50, 0, 0,
			0, 0
		},
		new int[]
		{
			2, 4, 13, 500, 100, 1, 0, 55, 0, 0,
			0, 0
		},
		new int[]
		{
			3, 1, 14, 500, 100, 1, 0, 60, 0, 0,
			0, 0
		},
		new int[]
		{
			3, 2, 14, 500, 100, 1, 0, 60, 0, 0,
			0, 0
		},
		new int[]
		{
			3, 3, 14, 500, 100, 1, 0, 60, 0, 0,
			0, 0
		},
		new int[]
		{
			3, 4, 14, 500, 100, 1, 0, 60, 0, 0,
			0, 0
		},
		new int[]
		{
			3, 5, 14, 500, 100, 1, 0, 60, 0, 0,
			0, 0
		}
	};

	// Token: 0x0400021A RID: 538
	public static bool flag;

	// Token: 0x0400021B RID: 539
	public static bool ischangingMap;

	// Token: 0x0400021C RID: 540
	public static bool isLockKey;

	// Token: 0x0400021D RID: 541
	public static bool isLoadingMap;

	// Token: 0x0400021E RID: 542
	public bool isLockMove;

	// Token: 0x0400021F RID: 543
	public bool isLockAttack;

	// Token: 0x04000220 RID: 544
	public string strInfo;

	// Token: 0x04000221 RID: 545
	public short powerPoint;

	// Token: 0x04000222 RID: 546
	public short maxPowerPoint;

	// Token: 0x04000223 RID: 547
	public short secondPower;

	// Token: 0x04000224 RID: 548
	public long lastS;

	// Token: 0x04000225 RID: 549
	public long currS;

	// Token: 0x04000226 RID: 550
	public const int C_XAYDA_2 = 2;

	// Token: 0x04000227 RID: 551
	public const int C_NAMEC_1 = 1;

	// Token: 0x04000228 RID: 552
	public const int C_TRAIDAT_0 = 0;

	// Token: 0x04000229 RID: 553
	public bool havePet = true;

	// Token: 0x0400022A RID: 554
	public MovePoint currentMovePoint;

	// Token: 0x0400022B RID: 555
	public int bom;

	// Token: 0x0400022C RID: 556
	public int delayFall;

	// Token: 0x0400022D RID: 557
	internal bool isSoundJump;

	// Token: 0x0400022E RID: 558
	public int lastFrame;

	// Token: 0x0400022F RID: 559
	internal Effect eProtect;

	// Token: 0x04000230 RID: 560
	internal Effect eDanhHieu;

	// Token: 0x04000231 RID: 561
	internal int twHp;

	// Token: 0x04000232 RID: 562
	public bool isInjureHp;

	// Token: 0x04000233 RID: 563
	public bool changePos;

	// Token: 0x04000234 RID: 564
	public bool isHide;

	// Token: 0x04000235 RID: 565
	internal int count;

	// Token: 0x04000236 RID: 566
	internal bool wy;

	// Token: 0x04000237 RID: 567
	public int wt;

	// Token: 0x04000238 RID: 568
	public int fy;

	// Token: 0x04000239 RID: 569
	public int ty;

	// Token: 0x0400023A RID: 570
	internal int t;

	// Token: 0x0400023B RID: 571
	internal int fM;

	// Token: 0x0400023C RID: 572
	public int[] move = new int[]
	{
		1, 1, 1, 1, 2, 2, 2, 2, 3, 3,
		3, 3, 2, 2, 2
	};

	// Token: 0x0400023D RID: 573
	internal string strMount = "mount_";

	// Token: 0x0400023E RID: 574
	public int headICON = -1;

	// Token: 0x0400023F RID: 575
	public int head;

	// Token: 0x04000240 RID: 576
	public int leg;

	// Token: 0x04000241 RID: 577
	public int body;

	// Token: 0x04000242 RID: 578
	public int bag;

	// Token: 0x04000243 RID: 579
	public int wp;

	// Token: 0x04000244 RID: 580
	public int indexEff = -1;

	// Token: 0x04000245 RID: 581
	public int indexEffTask = -1;

	// Token: 0x04000246 RID: 582
	public EffectCharPaint eff;

	// Token: 0x04000247 RID: 583
	public EffectCharPaint effTask;

	// Token: 0x04000248 RID: 584
	public int indexSkill;

	// Token: 0x04000249 RID: 585
	public int i0;

	// Token: 0x0400024A RID: 586
	public int i1;

	// Token: 0x0400024B RID: 587
	public int i2;

	// Token: 0x0400024C RID: 588
	public int dx0;

	// Token: 0x0400024D RID: 589
	public int dx1;

	// Token: 0x0400024E RID: 590
	public int dx2;

	// Token: 0x0400024F RID: 591
	public int dy0;

	// Token: 0x04000250 RID: 592
	public int dy1;

	// Token: 0x04000251 RID: 593
	public int dy2;

	// Token: 0x04000252 RID: 594
	public EffectCharPaint eff0;

	// Token: 0x04000253 RID: 595
	public EffectCharPaint eff1;

	// Token: 0x04000254 RID: 596
	public EffectCharPaint eff2;

	// Token: 0x04000255 RID: 597
	public Arrow arr;

	// Token: 0x04000256 RID: 598
	public PlayerDart dart;

	// Token: 0x04000257 RID: 599
	public bool isCreateDark;

	// Token: 0x04000258 RID: 600
	public SkillPaint skillPaint;

	// Token: 0x04000259 RID: 601
	public SkillPaint skillPaintRandomPaint;

	// Token: 0x0400025A RID: 602
	public EffectPaint[] effPaints;

	// Token: 0x0400025B RID: 603
	public int sType;

	// Token: 0x0400025C RID: 604
	public sbyte isInjure;

	// Token: 0x0400025D RID: 605
	public bool isUseSkillAfterCharge;

	// Token: 0x0400025E RID: 606
	public bool isFlyAndCharge;

	// Token: 0x0400025F RID: 607
	public bool isStandAndCharge;

	// Token: 0x04000260 RID: 608
	internal bool isFlying;

	// Token: 0x04000261 RID: 609
	public int posDisY;

	// Token: 0x04000262 RID: 610
	internal int chargeCount;

	// Token: 0x04000263 RID: 611
	internal bool hasSendAttack;

	// Token: 0x04000264 RID: 612
	public bool isMabuHold;

	// Token: 0x04000265 RID: 613
	internal long timeBlue;

	// Token: 0x04000266 RID: 614
	internal int tBlue;

	// Token: 0x04000267 RID: 615
	internal bool IsAddDust1;

	// Token: 0x04000268 RID: 616
	internal bool IsAddDust2;

	// Token: 0x04000269 RID: 617
	public int len = 24;

	// Token: 0x0400026A RID: 618
	public int w_hp_bar = 24;

	// Token: 0x0400026B RID: 619
	internal int per = 100;

	// Token: 0x0400026C RID: 620
	internal int per_tem = 100;

	// Token: 0x0400026D RID: 621
	internal Image imgHPtem;

	// Token: 0x0400026E RID: 622
	internal bool isPet;

	// Token: 0x0400026F RID: 623
	internal bool isMiniPet;

	// Token: 0x04000270 RID: 624
	internal int iiii;

	// Token: 0x04000271 RID: 625
	internal int danhHieuFramme;

	// Token: 0x04000272 RID: 626
	public int xSd;

	// Token: 0x04000273 RID: 627
	public int ySd;

	// Token: 0x04000274 RID: 628
	internal bool isOutMap;

	// Token: 0x04000275 RID: 629
	internal int fBag;

	// Token: 0x04000276 RID: 630
	internal Part ph;

	// Token: 0x04000277 RID: 631
	internal Part pl;

	// Token: 0x04000278 RID: 632
	internal Part pb;

	// Token: 0x04000279 RID: 633
	public int cH_new = 32;

	// Token: 0x0400027A RID: 634
	internal int statusBeforeNothing;

	// Token: 0x0400027B RID: 635
	internal int timeFocusToMob;

	// Token: 0x0400027C RID: 636
	public static bool isManualFocus = false;

	// Token: 0x0400027D RID: 637
	internal global::Char charHold;

	// Token: 0x0400027E RID: 638
	internal Mob mobHold;

	// Token: 0x0400027F RID: 639
	internal int nInjure;

	// Token: 0x04000280 RID: 640
	public short wdx;

	// Token: 0x04000281 RID: 641
	public short wdy;

	// Token: 0x04000282 RID: 642
	public bool isDirtyPostion;

	// Token: 0x04000283 RID: 643
	public Skill lastNormalSkill;

	// Token: 0x04000284 RID: 644
	public bool currentFireByShortcut;

	// Token: 0x04000285 RID: 645
	public int cDamGoc;

	// Token: 0x04000286 RID: 646
	public int cHPGoc;

	// Token: 0x04000287 RID: 647
	public int cMPGoc;

	// Token: 0x04000288 RID: 648
	public int cDefGoc;

	// Token: 0x04000289 RID: 649
	public int cCriticalGoc;

	// Token: 0x0400028A RID: 650
	public sbyte hpFrom1000TiemNang;

	// Token: 0x0400028B RID: 651
	public sbyte mpFrom1000TiemNang;

	// Token: 0x0400028C RID: 652
	public sbyte damFrom1000TiemNang;

	// Token: 0x0400028D RID: 653
	public sbyte defFrom1000TiemNang = 1;

	// Token: 0x0400028E RID: 654
	public sbyte criticalFrom1000Tiemnang = 1;

	// Token: 0x0400028F RID: 655
	public short cMaxStamina;

	// Token: 0x04000290 RID: 656
	public short expForOneAdd;

	// Token: 0x04000291 RID: 657
	public sbyte isMonkey;

	// Token: 0x04000292 RID: 658
	public bool isCopy;

	// Token: 0x04000293 RID: 659
	public bool isWaitMonkey;

	// Token: 0x04000294 RID: 660
	internal bool isFeetEff;

	// Token: 0x04000295 RID: 661
	public bool meDead;

	// Token: 0x04000296 RID: 662
	public int holdEffID;

	// Token: 0x04000297 RID: 663
	public bool holder;

	// Token: 0x04000298 RID: 664
	public bool protectEff;

	// Token: 0x04000299 RID: 665
	public bool danhHieuEff = true;

	// Token: 0x0400029A RID: 666
	internal bool isSetPos;

	// Token: 0x0400029B RID: 667
	internal int tpos;

	// Token: 0x0400029C RID: 668
	internal short xPos;

	// Token: 0x0400029D RID: 669
	internal short yPos;

	// Token: 0x0400029E RID: 670
	internal sbyte typePos;

	// Token: 0x0400029F RID: 671
	internal bool isMyFusion;

	// Token: 0x040002A0 RID: 672
	public bool isFusion;

	// Token: 0x040002A1 RID: 673
	public int tFusion;

	// Token: 0x040002A2 RID: 674
	public bool huytSao;

	// Token: 0x040002A3 RID: 675
	public bool blindEff;

	// Token: 0x040002A4 RID: 676
	public bool telePortSkill;

	// Token: 0x040002A5 RID: 677
	public bool sleepEff;

	// Token: 0x040002A6 RID: 678
	public bool stone;

	// Token: 0x040002A7 RID: 679
	public int perCentMp = 100;

	// Token: 0x040002A8 RID: 680
	public int dHP;

	// Token: 0x040002A9 RID: 681
	public int headTemp = -1;

	// Token: 0x040002AA RID: 682
	public int bodyTemp = -1;

	// Token: 0x040002AB RID: 683
	public int legTemp = -1;

	// Token: 0x040002AC RID: 684
	public int bagTemp = -1;

	// Token: 0x040002AD RID: 685
	public int wpTemp = -1;

	// Token: 0x040002AE RID: 686
	public MyVector vEffChar = new MyVector("vEff");

	// Token: 0x040002AF RID: 687
	public static FrameImage fraRedEye;

	// Token: 0x040002B0 RID: 688
	internal int fChopmat;

	// Token: 0x040002B1 RID: 689
	internal bool isAddChopMat;

	// Token: 0x040002B2 RID: 690
	internal long timeAddChopmat;

	// Token: 0x040002B3 RID: 691
	internal int[] frChopNhanh = new int[]
	{
		-1, -1, -1, -1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		-1, -1, -1, -1
	};

	// Token: 0x040002B4 RID: 692
	internal int[] frChopCham = new int[]
	{
		-1, -1, -1, -1, 0, 0, 1, 1, 1, 0,
		0, 1, 1, 1, 0, 0, 1, 1, 1, -1,
		-1, -1, -1
	};

	// Token: 0x040002B5 RID: 693
	internal int[] frEye = new int[]
	{
		-1, -1, 0, 0, 1, 1, 0, 0, 1, 1,
		0, 0, 1, 1, 0, 0, 1, 1, 0, 0,
		1, 1, 0, 0, 1, 1, 0, 0, -1, -1
	};

	// Token: 0x040002B6 RID: 694
	public static int[][] Arr_Head_2Fr = new int[][] { new int[] { 542, 543 } };

	// Token: 0x040002B7 RID: 695
	internal int fHead;

	// Token: 0x040002B8 RID: 696
	internal string strEffAura = "aura_";

	// Token: 0x040002B9 RID: 697
	public short idAuraEff = -1;

	// Token: 0x040002BA RID: 698
	public static bool isPaintAura = true;

	// Token: 0x040002BB RID: 699
	public static bool isPaintAura2 = true;

	// Token: 0x040002BC RID: 700
	internal FrameImage fraEff;

	// Token: 0x040002BD RID: 701
	internal FrameImage fraEffSub;

	// Token: 0x040002BE RID: 702
	internal string strEff_Set_Item = "set_eff_";

	// Token: 0x040002BF RID: 703
	public short idEff_Set_Item = -1;

	// Token: 0x040002C0 RID: 704
	internal FrameImage fraHat_behind;

	// Token: 0x040002C1 RID: 705
	internal FrameImage fraHat_font;

	// Token: 0x040002C2 RID: 706
	internal FrameImage fraHat_behind_2;

	// Token: 0x040002C3 RID: 707
	internal FrameImage fraHat_font_2;

	// Token: 0x040002C4 RID: 708
	internal string strHat_behind = "hat_sau_";

	// Token: 0x040002C5 RID: 709
	internal string strHat_font = "hat_truoc_";

	// Token: 0x040002C6 RID: 710
	internal string strNgang = "ngang_";

	// Token: 0x040002C7 RID: 711
	public short idHat = -1;

	// Token: 0x040002C8 RID: 712
	public static int[][] hatInfo;

	// Token: 0x040002C9 RID: 713
	public static short[] Arr_Head_FlyMove;

	// Token: 0x040002CA RID: 714
	public const byte TYPE_SKILL_KAMEX10 = 1;

	// Token: 0x040002CB RID: 715
	public const byte TYPE_SKILL_FINAL = 2;

	// Token: 0x040002CC RID: 716
	public const byte TYPE_SKILL_MAFUBA = 3;

	// Token: 0x040002CD RID: 717
	public const byte TYPE_SKILL_GENKI = 4;

	// Token: 0x040002CE RID: 718
	public bool isPaintNewSkill;

	// Token: 0x040002CF RID: 719
	internal bool isFly;

	// Token: 0x040002D0 RID: 720
	internal long timeReset_newSkill;

	// Token: 0x040002D1 RID: 721
	internal sbyte typeFrame;

	// Token: 0x040002D2 RID: 722
	internal short idskillPaint;

	// Token: 0x040002D3 RID: 723
	internal byte[] fr_start;

	// Token: 0x040002D4 RID: 724
	internal byte[] fr_atk;

	// Token: 0x040002D5 RID: 725
	internal byte[] fr_end;

	// Token: 0x040002D6 RID: 726
	internal int count_NEW;

	// Token: 0x040002D7 RID: 727
	internal int stt;

	// Token: 0x040002D8 RID: 728
	internal short rangeDame;

	// Token: 0x040002D9 RID: 729
	internal sbyte typePaint;

	// Token: 0x040002DA RID: 730
	internal sbyte typeItem;

	// Token: 0x040002DB RID: 731
	internal Point targetDame;

	// Token: 0x040002DC RID: 732
	internal long timeDame;

	// Token: 0x040002DD RID: 733
	public bool isMafuba;

	// Token: 0x040002DE RID: 734
	internal short countMafuba;

	// Token: 0x040002DF RID: 735
	public int xMFB;

	// Token: 0x040002E0 RID: 736
	public int yMFB;

	// Token: 0x040002E1 RID: 737
	public int timeGongSkill;

	// Token: 0x040002E2 RID: 738
	internal FrameImage fraDanhHieu;

	// Token: 0x040002E3 RID: 739
	internal MainImage mainImg;
}
