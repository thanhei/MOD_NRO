using System;

namespace Mod.CharEffect
{
	// Token: 0x0200016E RID: 366
	internal class CharEffectTime
	{
		// Token: 0x060010E1 RID: 4321 RVA: 0x000B824C File Offset: 0x000B644C
		internal void Update()
		{
			if (this.timeHoldingBlackStarDragonBall > 0 && mSystem.currentTimeMillis() - this.lastTimeHoldingBlackStarDragonBall >= 1000L)
			{
				this.timeHoldingBlackStarDragonBall--;
				this.lastTimeHoldingBlackStarDragonBall = mSystem.currentTimeMillis();
			}
			if (this.timeHypnotized > 0 && mSystem.currentTimeMillis() - this.lastTimeHypnotized >= 1000L)
			{
				this.timeHypnotized--;
				if (this.timeHypnotized == 0)
				{
					this.isHypnotizedByMe = false;
				}
				this.lastTimeHypnotized = mSystem.currentTimeMillis();
			}
			if (this.timeMonkey > 0 && mSystem.currentTimeMillis() - this.lastTimeMonkey >= 1000L)
			{
				this.timeMonkey--;
				this.lastTimeMonkey = mSystem.currentTimeMillis();
			}
			if (this.timeHuytSao > 0 && mSystem.currentTimeMillis() - this.lastTimeHuytSao >= 1000L)
			{
				this.timeHuytSao--;
				if (this.timeHuytSao <= 0)
				{
					this.hasHuytSao = false;
				}
				this.lastTimeHuytSao = mSystem.currentTimeMillis();
			}
			if (this.timeShield > 0 && mSystem.currentTimeMillis() - this.lastTimeShield >= 1000L)
			{
				this.timeShield--;
				this.lastTimeShield = mSystem.currentTimeMillis();
			}
			if (this.timeTeleported > 0 && mSystem.currentTimeMillis() - this.lastTimeTeleported >= 1000L)
			{
				this.timeTeleported--;
				this.lastTimeTeleported = mSystem.currentTimeMillis();
			}
			if (this.timeTied > 0 && mSystem.currentTimeMillis() - this.lastTimeTied >= 1000L)
			{
				this.timeTied--;
				if (this.timeTied == 0)
				{
					this.isTiedByMe = false;
				}
				this.lastTimeTied = mSystem.currentTimeMillis();
			}
			if (this.timeMobMe > 0 && mSystem.currentTimeMillis() - this.lastTimeMobMe >= 1000L)
			{
				this.timeMobMe--;
				this.lastTimeMobMe = mSystem.currentTimeMillis();
			}
			if (this.timeTDHS > 0 && mSystem.currentTimeMillis() - this.lastTimeTDHS >= 1000L)
			{
				this.timeTDHS--;
				this.lastTimeTDHS = mSystem.currentTimeMillis();
			}
			if (this.timeStone > 0 && mSystem.currentTimeMillis() - this.lastTimeStoned >= 1000L)
			{
				this.timeStone--;
				this.lastTimeStoned = mSystem.currentTimeMillis();
			}
			if (this.timeChocolate > 0 && mSystem.currentTimeMillis() - this.lastTimeChocolated >= 1000L)
			{
				this.timeChocolate--;
				this.lastTimeChocolated = mSystem.currentTimeMillis();
			}
			if (this.timeSelfExplode > 0 && mSystem.currentTimeMillis() - this.lastTimeSelfExplode >= 1000L)
			{
				this.timeSelfExplode--;
				this.lastTimeSelfExplode = mSystem.currentTimeMillis();
			}
			if (this.timeQCKK > 0 && mSystem.currentTimeMillis() - this.lastTimeQCKK >= 1000L)
			{
				this.timeQCKK--;
				this.lastTimeQCKK = mSystem.currentTimeMillis();
			}
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000B8548 File Offset: 0x000B6748
		internal bool HasAnyEffect()
		{
			return this.timeTeleported + this.timeTied + this.timeHoldingBlackStarDragonBall + this.timeHuytSao + this.timeMobMe + this.timeMonkey + this.timeShield + this.timeHypnotized + this.timeTDHS + this.timeStone + this.timeChocolate + this.timeSelfExplode + this.timeQCKK > 0 || this.hasNamekianDragonBall;
		}

		// Token: 0x04001834 RID: 6196
		internal bool hasBlackStarDragonBall;

		// Token: 0x04001835 RID: 6197
		internal int timeHoldingBlackStarDragonBall;

		// Token: 0x04001836 RID: 6198
		internal long lastTimeHoldingBlackStarDragonBall;

		// Token: 0x04001837 RID: 6199
		internal bool isHypnotized;

		// Token: 0x04001838 RID: 6200
		internal int timeHypnotized;

		// Token: 0x04001839 RID: 6201
		internal long lastTimeHypnotized;

		// Token: 0x0400183A RID: 6202
		internal bool isHypnotizedByMe;

		// Token: 0x0400183B RID: 6203
		internal bool hasMonkey;

		// Token: 0x0400183C RID: 6204
		internal int timeMonkey;

		// Token: 0x0400183D RID: 6205
		internal long lastTimeMonkey;

		// Token: 0x0400183E RID: 6206
		internal bool hasHuytSao;

		// Token: 0x0400183F RID: 6207
		internal int timeHuytSao;

		// Token: 0x04001840 RID: 6208
		internal long lastTimeHuytSao;

		// Token: 0x04001841 RID: 6209
		internal bool hasShield;

		// Token: 0x04001842 RID: 6210
		internal int timeShield;

		// Token: 0x04001843 RID: 6211
		internal long lastTimeShield;

		// Token: 0x04001844 RID: 6212
		internal bool isTeleported;

		// Token: 0x04001845 RID: 6213
		internal int timeTeleported;

		// Token: 0x04001846 RID: 6214
		internal long lastTimeTeleported;

		// Token: 0x04001847 RID: 6215
		internal bool isTied;

		// Token: 0x04001848 RID: 6216
		internal int timeTied;

		// Token: 0x04001849 RID: 6217
		internal long lastTimeTied;

		// Token: 0x0400184A RID: 6218
		internal bool isTiedByMe;

		// Token: 0x0400184B RID: 6219
		internal bool hasMobMe;

		// Token: 0x0400184C RID: 6220
		internal int timeMobMe;

		// Token: 0x0400184D RID: 6221
		internal long lastTimeMobMe;

		// Token: 0x0400184E RID: 6222
		internal bool isTDHS;

		// Token: 0x0400184F RID: 6223
		internal int timeTDHS;

		// Token: 0x04001850 RID: 6224
		internal long lastTimeTDHS;

		// Token: 0x04001851 RID: 6225
		internal bool isStone;

		// Token: 0x04001852 RID: 6226
		internal int timeStone;

		// Token: 0x04001853 RID: 6227
		internal long lastTimeStoned;

		// Token: 0x04001854 RID: 6228
		internal bool isChocolate;

		// Token: 0x04001855 RID: 6229
		internal int timeChocolate;

		// Token: 0x04001856 RID: 6230
		internal long lastTimeChocolated;

		// Token: 0x04001857 RID: 6231
		internal bool isSelfExplode;

		// Token: 0x04001858 RID: 6232
		internal long lastTimeSelfExplode;

		// Token: 0x04001859 RID: 6233
		internal int timeSelfExplode;

		// Token: 0x0400185A RID: 6234
		internal bool isQCKK;

		// Token: 0x0400185B RID: 6235
		internal long lastTimeQCKK;

		// Token: 0x0400185C RID: 6236
		internal int timeQCKK;

		// Token: 0x0400185D RID: 6237
		internal bool hasNamekianDragonBall;
	}
}
