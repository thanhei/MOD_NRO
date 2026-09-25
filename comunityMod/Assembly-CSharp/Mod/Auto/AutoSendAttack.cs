using System;
using Mod.ModHelper;
using Mod.ModHelper.CommandMod.Chat;
using Mod.R;

namespace Mod.Auto
{
	// Token: 0x02000180 RID: 384
	internal class AutoSendAttack : ThreadActionUpdate<AutoSendAttack>
	{
		// Token: 0x170000E5 RID: 229
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x000A3760 File Offset: 0x000A1960
		internal override int Interval
		{
			get
			{
				return 100;
			}
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000BB014 File Offset: 0x000B9214
		protected override void update()
		{
			MyVector myVector = new MyVector();
			MyVector myVector2 = new MyVector();
			global::Char @char = global::Char.myCharz();
			if (@char.mobFocus != null)
			{
				myVector.addElement(@char.mobFocus);
			}
			else if (@char.charFocus != null)
			{
				myVector2.addElement(@char.charFocus);
			}
			if (myVector.size() > 0 || myVector2.size() > 0)
			{
				Skill myskill = @char.myskill;
				long num = mSystem.currentTimeMillis();
				if (num - myskill.lastTimeUseThisSkill > (long)myskill.coolDown)
				{
					Service.gI().sendPlayerAttack(myVector, myVector2, -1);
					myskill.lastTimeUseThisSkill = num;
				}
			}
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000BB0A4 File Offset: 0x000B92A4
		[ChatCommand("ak")]
		internal static void toggleAutoAttack()
		{
			ThreadAction<AutoSendAttack>.gI.toggle(null);
			GameScr.info1.addInfo(Strings.autoAttack + ": " + (ThreadAction<AutoSendAttack>.gI.IsActing ? mResources.ON : mResources.OFF) + "!", 0);
		}
	}
}
