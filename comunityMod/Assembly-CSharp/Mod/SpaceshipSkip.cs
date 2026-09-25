using System;

namespace Mod
{
	// Token: 0x020000ED RID: 237
	internal static class SpaceshipSkip
	{
		// Token: 0x06000CFB RID: 3323 RVA: 0x000A0B18 File Offset: 0x0009ED18
		internal static void Update(Teleport teleport)
		{
			if (!SpaceshipSkip.isEnabled)
			{
				return;
			}
			if (!teleport.isMe)
			{
				global::Char @char = GameScr.findCharInMap(teleport.id);
				if (@char != null)
				{
					if (teleport.type == 0)
					{
						if (teleport.isDown)
						{
							teleport.y = teleport.y2;
							return;
						}
					}
					else
					{
						if (@char.isTeleport)
						{
							@char.cy = (teleport.y = teleport.y2);
						}
						@char.isTeleport = false;
					}
				}
				return;
			}
			if (teleport.type == 0)
			{
				Controller.isStopReadMessage = false;
				global::Char.ischangingMap = true;
				Teleport.vTeleport.removeElement(teleport);
				return;
			}
			if (global::Char.myCharz().isTeleport)
			{
				global::Char.myCharz().cy = (teleport.y = teleport.y2);
			}
			global::Char.myCharz().isTeleport = false;
		}

		// Token: 0x040014C8 RID: 5320
		internal static bool isEnabled;
	}
}
