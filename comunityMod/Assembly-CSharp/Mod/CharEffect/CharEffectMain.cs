using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Mod.CharEffect
{
	// Token: 0x0200016D RID: 365
	internal class CharEffectMain
	{
		// Token: 0x060010D3 RID: 4307 RVA: 0x000B7138 File Offset: 0x000B5338
		internal static void Init()
		{
			CharEffectMain.charEffectImages = new Image[]
			{
				GameCanvas.loadImage("/CharEffect/nrd1"),
				GameCanvas.loadImage("/CharEffect/nrd2"),
				GameCanvas.loadImage("/CharEffect/nrd3"),
				GameCanvas.loadImage("/CharEffect/nrd4"),
				GameCanvas.loadImage("/CharEffect/nrd5"),
				GameCanvas.loadImage("/CharEffect/nrd6"),
				GameCanvas.loadImage("/CharEffect/nrd7"),
				GameCanvas.loadImage("/CharEffect/shield"),
				GameCanvas.loadImage("/CharEffect/monkey"),
				GameCanvas.loadImage("/CharEffect/whistle"),
				GameCanvas.loadImage("/CharEffect/mobme"),
				GameCanvas.loadImage("/CharEffect/hypnotize"),
				GameCanvas.loadImage("/CharEffect/teleport"),
				GameCanvas.loadImage("/CharEffect/blind"),
				GameCanvas.loadImage("/CharEffect/tie"),
				GameCanvas.loadImage("/CharEffect/stone"),
				GameCanvas.loadImage("/CharEffect/choco"),
				GameCanvas.loadImage("/CharEffect/selfexplode"),
				GameCanvas.loadImage("/CharEffect/nrnm"),
				GameCanvas.loadImage("/CharEffect/qckk")
			};
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000B7260 File Offset: 0x000B5460
		internal static void updateMe()
		{
			CharEffectTime charEffectTime = global::Char.myCharz().charEffectTime;
			if (charEffectTime.hasMobMe)
			{
				if (!CharEffectMain.isMobMeAdded)
				{
					CharEffectMain.isMobMeAdded = true;
					if (Utils.isMeWearingPikkoroDaimaoSet())
					{
						global::Char.vItemTime.addElement(new ItemTime(722, true));
					}
					else
					{
						global::Char.vItemTime.addElement(new ItemTime(722, global::Char.myCharz().GetTimeMobMe()));
					}
				}
			}
			else if (CharEffectMain.isMobMeAdded)
			{
				CharEffectMain.isMobMeAdded = false;
				if (Utils.isMeWearingPikkoroDaimaoSet())
				{
					CharEffectMain.removeElement(new ItemTime(722, true));
				}
				else
				{
					CharEffectMain.removeElement(new ItemTime(722, 0));
				}
			}
			if (charEffectTime.isTied)
			{
				if (!CharEffectMain.isTieAdded)
				{
					CharEffectMain.isTieAdded = true;
					global::Char.vItemTime.addElement(new ItemTime(3779, 35, true));
				}
			}
			else if (CharEffectMain.isTieAdded)
			{
				CharEffectMain.isTieAdded = false;
				CharEffectMain.removeElement(new ItemTime(3779, 0, true));
			}
			if (charEffectTime.isTDHS)
			{
				if (!CharEffectMain.isTDHSAdded)
				{
					CharEffectMain.isTDHSAdded = true;
					global::Char.vItemTime.addElement(new ItemTime(717, global::Char.myCharz().freezSeconds));
				}
			}
			else if (CharEffectMain.isTDHSAdded)
			{
				CharEffectMain.isTDHSAdded = false;
				CharEffectMain.removeElement(new ItemTime(717, 0));
			}
			if (charEffectTime.hasMonkey)
			{
				if (!CharEffectMain.isMonkeyAdded)
				{
					CharEffectMain.isMonkeyAdded = true;
					if (Utils.isMeWearingCadicSet())
					{
						global::Char.vItemTime.addElement(new ItemTime(718, global::Char.myCharz().GetTimeMonkey() * 5));
					}
					else
					{
						global::Char.vItemTime.addElement(new ItemTime(718, global::Char.myCharz().GetTimeMonkey()));
					}
				}
			}
			else if (CharEffectMain.isMonkeyAdded)
			{
				CharEffectMain.isMonkeyAdded = false;
				CharEffectMain.removeElement(new ItemTime(718, 0));
			}
			if (charEffectTime.hasBlackStarDragonBall)
			{
				if (!CharEffectMain.isNRDAdded)
				{
					CharEffectMain.isNRDAdded = true;
					CharEffectMain.NRSDImageId = Utils.getNRSDId();
					global::Char.vItemTime.addElement(new ItemTime(CharEffectMain.NRSDImageId, 300));
					return;
				}
			}
			else if (CharEffectMain.isNRDAdded)
			{
				CharEffectMain.isNRDAdded = false;
				CharEffectMain.removeElement(new ItemTime(CharEffectMain.NRSDImageId, 0));
			}
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x000B7470 File Offset: 0x000B5670
		internal static void removeElement(ItemTime item)
		{
			for (int i = 0; i < global::Char.vItemTime.size(); i++)
			{
				ItemTime itemTime = global::Char.vItemTime.elementAt(i) as ItemTime;
				if (itemTime.idIcon == item.idIcon && itemTime.isEquivalence == item.isEquivalence && itemTime.isInfinity == item.isInfinity)
				{
					global::Char.vItemTime.removeElementAt(i);
					return;
				}
			}
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x000B74DC File Offset: 0x000B56DC
		internal static void Update()
		{
			CharEffectMain.updateMe();
			for (int i = CharEffectMain.storedChars.Count - 1; i >= 0; i--)
			{
				global::Char @char = CharEffectMain.storedChars.ElementAt<global::Char>(i);
				global::Char char2 = GameScr.findCharInMap(@char.charID);
				if (!@char.charEffectTime.HasAnyEffect())
				{
					CharEffectMain.storedChars.RemoveAt(i);
				}
				else if (char2 == null)
				{
					@char.charEffectTime.Update();
				}
				else
				{
					GameScr.findCharInMap(@char.charID).charEffectTime = @char.charEffectTime;
					CharEffectMain.storedChars[i] = char2;
				}
			}
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x000B756C File Offset: 0x000B576C
		internal static bool isContains(int charId)
		{
			using (List<global::Char>.Enumerator enumerator = CharEffectMain.storedChars.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.charID == charId)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x000B75C8 File Offset: 0x000B57C8
		internal static void Paint(mGraphics g)
		{
			if (!CharEffectMain.isEnabled)
			{
				return;
			}
			global::Char charFocus = global::Char.myCharz().charFocus;
			if (charFocus == null)
			{
				return;
			}
			string text = string.Concat(new string[]
			{
				charFocus.cName,
				" - ",
				Utils.FormatWithSIPrefix((double)charFocus.cHP),
				"/",
				Utils.FormatWithSIPrefix((double)charFocus.cHPFull)
			});
			if (charFocus.charID > 0)
			{
				text += string.Format(" [{0}]", charFocus.charID);
			}
			g.setColor(new Color(0f, 0f, 0f, 0.4f));
			g.fillRect(GameCanvas.w / 2 - mFont.tahoma_7b_red.getWidth(text) / 2 - 5, 51, mFont.tahoma_7b_red.getWidth(text) + 8, 10);
			mFont.tahoma_7b_red.drawString(g, text, GameCanvas.w / 2, 50, mFont.CENTER);
			if (charFocus.charEffectTime != null && charFocus.charEffectTime.HasAnyEffect())
			{
				Dictionary<Image, string> dictionary = new Dictionary<Image, string>();
				if (charFocus.charEffectTime.hasBlackStarDragonBall)
				{
					dictionary.Add(CharEffectMain.charEffectImages[TileMap.mapID - 85], charFocus.charEffectTime.timeHoldingBlackStarDragonBall.ToString() + "s");
				}
				if (charFocus.charEffectTime.hasShield)
				{
					dictionary.Add(CharEffectMain.charEffectImages[7], "~" + charFocus.charEffectTime.timeShield.ToString() + "s");
				}
				if (charFocus.charEffectTime.hasMonkey)
				{
					dictionary.Add(CharEffectMain.charEffectImages[8], charFocus.charEffectTime.timeMonkey.ToString() + "s");
				}
				if (charFocus.charEffectTime.hasHuytSao)
				{
					dictionary.Add(CharEffectMain.charEffectImages[9], charFocus.charEffectTime.timeHuytSao.ToString() + "s");
				}
				if (charFocus.charEffectTime.hasMobMe)
				{
					dictionary.Add(CharEffectMain.charEffectImages[10], charFocus.charEffectTime.timeMobMe.ToString() + "s");
				}
				if (charFocus.charEffectTime.isHypnotized)
				{
					dictionary.Add(CharEffectMain.charEffectImages[11], charFocus.charEffectTime.isHypnotizedByMe ? "" : ("~" + charFocus.charEffectTime.timeHypnotized.ToString() + "s"));
				}
				if (charFocus.charEffectTime.isTeleported)
				{
					dictionary.Add(CharEffectMain.charEffectImages[12], charFocus.charEffectTime.timeTeleported.ToString() + "s");
				}
				if (charFocus.charEffectTime.isTDHS)
				{
					dictionary.Add(CharEffectMain.charEffectImages[13], charFocus.charEffectTime.timeTDHS.ToString() + "s");
				}
				if (charFocus.charEffectTime.isTied)
				{
					dictionary.Add(CharEffectMain.charEffectImages[14], charFocus.charEffectTime.isTiedByMe ? "" : ("~" + charFocus.charEffectTime.timeTied.ToString() + "s"));
				}
				if (charFocus.charEffectTime.isStone)
				{
					dictionary.Add(CharEffectMain.charEffectImages[15], charFocus.charEffectTime.timeStone.ToString() + "s");
				}
				if (charFocus.charEffectTime.isChocolate)
				{
					dictionary.Add(CharEffectMain.charEffectImages[16], charFocus.charEffectTime.timeChocolate.ToString() + "s");
				}
				if (charFocus.charEffectTime.isSelfExplode)
				{
					dictionary.Add(CharEffectMain.charEffectImages[17], charFocus.charEffectTime.timeSelfExplode.ToString() + "s");
				}
				if (charFocus.charEffectTime.hasNamekianDragonBall)
				{
					dictionary.Add(CharEffectMain.charEffectImages[18], "???");
				}
				if (charFocus.charEffectTime.isQCKK)
				{
					dictionary.Add(CharEffectMain.charEffectImages[19], charFocus.charEffectTime.timeQCKK.ToString() + "s");
				}
				int num = CharEffectMain.charEffectImages[0].w / mGraphics.zoomLevel * dictionary.Count + CharEffectMain.PADDING * (dictionary.Count - 1);
				int num2 = GameCanvas.w / 2 - num / 2;
				g.fillRect(num2 - 3, 61, num + 6, 20 + mFont.tahoma_7b_white.getHeight() + 6);
				foreach (KeyValuePair<Image, string> keyValuePair in dictionary)
				{
					g.drawImage(keyValuePair.Key, num2, 64);
					mFont.tahoma_7_white.drawString(g, keyValuePair.Value, num2 + 10, 64 + mFont.tahoma_7b_white.getHeight() + 10, mFont.CENTER, mFont.tahoma_7b_dark);
					num2 += 20 + CharEffectMain.PADDING;
				}
			}
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000B7AC8 File Offset: 0x000B5CC8
		internal static void AddEffectCreatedByMe(Skill skill)
		{
			if (global::Char.myCharz().charFocus != null)
			{
				if (skill.template.id == 22)
				{
					global::Char.myCharz().charFocus.charEffectTime.isHypnotizedByMe = true;
				}
				if (skill.template.id == 23)
				{
					global::Char.myCharz().charFocus.charEffectTime.isTiedByMe = true;
				}
			}
			if (skill.template.id == 6 && mSystem.currentTimeMillis() - skill.lastTimeUseThisSkill > (long)skill.coolDown)
			{
				global::Char.vItemTime.addElement(new ItemTime(717, global::Char.myCharz().GetTimeBlind() - 1));
			}
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x000B7B6C File Offset: 0x000B5D6C
		internal static void setState(bool value)
		{
			CharEffectMain.isEnabled = value;
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000B7B74 File Offset: 0x000B5D74
		internal static void UpdateChar(global::Char ch)
		{
			if (ch.charEffectTime.HasAnyEffect() && !CharEffectMain.isContains(ch.charID))
			{
				CharEffectMain.storedChars.Add(ch);
			}
			ch.charEffectTime.Update();
			if (ch.head == 412 && ch.body == 413 && ch.leg == 414)
			{
				if (!ch.charEffectTime.isChocolate)
				{
					ch.charEffectTime.isChocolate = true;
					ch.charEffectTime.lastTimeChocolated = mSystem.currentTimeMillis();
					ch.charEffectTime.timeChocolate = ch.GetTimeChocolate() + 1;
				}
			}
			else
			{
				ch.charEffectTime.isChocolate = false;
				ch.charEffectTime.timeChocolate = 0;
			}
			if (ch.bag >= 0 && ClanImage.idImages.containsKey(ch.bag.ToString()))
			{
				ClanImage clanImage = (ClanImage)ClanImage.idImages.get(ch.bag.ToString());
				bool flag = true;
				bool flag2 = false;
				if (clanImage.idImage != null)
				{
					int i = 0;
					while (i < clanImage.idImage.Length)
					{
						if (clanImage.idImage[i] == 2322 && Utils.IsMeInNRDMap())
						{
							ch.charEffectTime.hasBlackStarDragonBall = true;
							flag = false;
							if (ch.charEffectTime.timeHoldingBlackStarDragonBall == 0)
							{
								ch.charEffectTime.timeHoldingBlackStarDragonBall = 302;
								break;
							}
							break;
						}
						else
						{
							if (clanImage.idImage[i] == 2287)
							{
								flag2 = true;
							}
							i++;
						}
					}
				}
				if (flag)
				{
					ch.charEffectTime.hasBlackStarDragonBall = false;
					ch.charEffectTime.timeHoldingBlackStarDragonBall = 0;
				}
				ch.charEffectTime.hasNamekianDragonBall = flag2;
			}
			if (ch.sleepEff)
			{
				if (!ch.charEffectTime.isHypnotized)
				{
					ch.charEffectTime.isHypnotized = true;
					ch.charEffectTime.lastTimeHypnotized = mSystem.currentTimeMillis();
					if (ch.charEffectTime.timeHypnotized <= 0)
					{
						ch.charEffectTime.timeHypnotized = ch.GetTimeHypnotize() + 1;
					}
				}
			}
			else
			{
				ch.charEffectTime.isHypnotized = false;
				ch.charEffectTime.timeHypnotized = 0;
			}
			if (ch.isMonkey == 1)
			{
				if (!ch.charEffectTime.hasMonkey)
				{
					ch.charEffectTime.hasMonkey = true;
					ch.charEffectTime.lastTimeMonkey = mSystem.currentTimeMillis();
					if (ch.charEffectTime.timeMonkey <= 0)
					{
						ch.charEffectTime.timeMonkey = ch.GetTimeMonkey();
					}
				}
			}
			else
			{
				ch.charEffectTime.hasMonkey = false;
				ch.charEffectTime.timeMonkey = 0;
			}
			if (ch.huytSao && !ch.charEffectTime.hasHuytSao)
			{
				ch.charEffectTime.hasHuytSao = true;
				ch.charEffectTime.lastTimeHuytSao = mSystem.currentTimeMillis();
				if (ch.charEffectTime.timeHuytSao <= 0)
				{
					ch.charEffectTime.timeHuytSao = ch.GetTimeWhistle() + 1;
				}
			}
			if (ch.blindEff)
			{
				if (!ch.charEffectTime.isTeleported)
				{
					ch.charEffectTime.isTeleported = true;
					ch.charEffectTime.lastTimeTeleported = mSystem.currentTimeMillis();
					if (ch.charEffectTime.timeTeleported <= 0)
					{
						ch.charEffectTime.timeTeleported = 7;
					}
				}
			}
			else
			{
				ch.charEffectTime.isTeleported = false;
				ch.charEffectTime.timeTeleported = 0;
			}
			if (ch.protectEff)
			{
				if (!ch.charEffectTime.hasShield)
				{
					ch.charEffectTime.hasShield = true;
					ch.charEffectTime.lastTimeShield = mSystem.currentTimeMillis();
					if (ch.charEffectTime.timeShield <= 0)
					{
						ch.charEffectTime.timeShield = ch.GetTimeShield() + 1;
					}
				}
			}
			else
			{
				ch.charEffectTime.hasShield = false;
				ch.charEffectTime.timeShield = 0;
			}
			if (ch.stone)
			{
				if (!ch.charEffectTime.isStone)
				{
					ch.charEffectTime.isStone = true;
					ch.charEffectTime.lastTimeStoned = mSystem.currentTimeMillis();
					if (ch.charEffectTime.timeStone <= 0)
					{
						ch.charEffectTime.timeStone = ch.GetTimeStone() + 1;
					}
				}
			}
			else
			{
				ch.charEffectTime.isStone = false;
				ch.charEffectTime.timeStone = 0;
			}
			if (ch.isFreez)
			{
				ch.charEffectTime.isTDHS = true;
				if (ch.me && ch.meDead)
				{
					ch.freezSeconds = 0;
					ch.charEffectTime.isTDHS = false;
					ch.charEffectTime.timeTDHS = 0;
				}
				else
				{
					ch.charEffectTime.lastTimeTDHS = mSystem.currentTimeMillis();
					ch.charEffectTime.timeTDHS = ch.freezSeconds + 1;
				}
			}
			else
			{
				ch.charEffectTime.isTDHS = false;
				ch.charEffectTime.timeTDHS = 0;
			}
			if (ch.mobMe != null)
			{
				if (ch.mobMe.isDie || ch.mobMe.hp <= 0)
				{
					ch.charEffectTime.hasMobMe = false;
				}
				else if (!ch.charEffectTime.hasMobMe)
				{
					ch.charEffectTime.hasMobMe = true;
					ch.charEffectTime.lastTimeMobMe = mSystem.currentTimeMillis();
					if (ch.charEffectTime.timeMobMe <= 0)
					{
						ch.charEffectTime.timeMobMe = ch.GetTimeMobMe() + 1;
					}
				}
			}
			else
			{
				ch.charEffectTime.hasMobMe = false;
			}
			if (ch.holdEffID == 0 && !ch.blindEff && !ch.sleepEff && ch.holder && ch.me && ch.statusMe == 2 && ch.currentMovePoint != null)
			{
				ch.charEffectTime.isTied = false;
				if (ch.charHold != null)
				{
					ch.charHold.charEffectTime.isTiedByMe = false;
				}
			}
			if (ch.isStandAndCharge)
			{
				if (!ch.charEffectTime.isSelfExplode)
				{
					ch.charEffectTime.isSelfExplode = true;
					ch.charEffectTime.lastTimeSelfExplode = mSystem.currentTimeMillis();
					if (ch.charEffectTime.timeSelfExplode <= 0)
					{
						ch.charEffectTime.timeSelfExplode = ch.GetTimeSelfExplode();
					}
				}
			}
			else
			{
				ch.charEffectTime.isSelfExplode = false;
				ch.charEffectTime.timeSelfExplode = 0;
			}
			if (ch.isFlyAndCharge)
			{
				if (!ch.charEffectTime.isQCKK)
				{
					ch.charEffectTime.isQCKK = true;
					ch.charEffectTime.lastTimeQCKK = mSystem.currentTimeMillis();
					if (ch.charEffectTime.timeQCKK <= 0)
					{
						ch.charEffectTime.timeQCKK = ch.GetTimeQCKK();
						return;
					}
				}
			}
			else
			{
				ch.charEffectTime.isQCKK = false;
				ch.charEffectTime.timeQCKK = 0;
			}
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000B81AC File Offset: 0x000B63AC
		internal static void AddCharHoldChar(global::Char ch, global::Char r)
		{
			r.charEffectTime.isTied = true;
			r.charEffectTime.timeTied = r.GetTimeHold() + 1;
			if (ch.me)
			{
				global::Char.vItemTime.addElement(new ItemTime(3779, ch.GetTimeHold()));
			}
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x000B81FA File Offset: 0x000B63FA
		internal static void AddCharHoldMob(global::Char ch)
		{
			if (ch.me)
			{
				global::Char.vItemTime.addElement(new ItemTime(3779, ch.GetTimeHold()));
			}
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x000B821E File Offset: 0x000B641E
		internal static void RemoveHold(global::Char ch)
		{
			if (ch.me)
			{
				CharEffectMain.removeElement(new ItemTime(3779, 0));
			}
		}

		// Token: 0x0400182A RID: 6186
		internal static bool isEnabled;

		// Token: 0x0400182B RID: 6187
		internal static List<global::Char> storedChars = new List<global::Char>();

		// Token: 0x0400182C RID: 6188
		internal static bool isNRDAdded;

		// Token: 0x0400182D RID: 6189
		internal static bool isTieAdded;

		// Token: 0x0400182E RID: 6190
		internal static bool isTDHSAdded;

		// Token: 0x0400182F RID: 6191
		internal static bool isMobMeAdded;

		// Token: 0x04001830 RID: 6192
		internal static bool isMonkeyAdded;

		// Token: 0x04001831 RID: 6193
		private static short NRSDImageId;

		// Token: 0x04001832 RID: 6194
		private static readonly int PADDING = 5;

		// Token: 0x04001833 RID: 6195
		private static Image[] charEffectImages;
	}
}
