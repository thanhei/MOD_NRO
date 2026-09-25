using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Newtonsoft.Json;

namespace Mod.ModHelper.CommandMod.Hotkey
{
	// Token: 0x02000150 RID: 336
	public class HotkeyCommandHandler
	{
		// Token: 0x06000FED RID: 4077 RVA: 0x000B19B0 File Offset: 0x000AFBB0
		public static void loadDefault()
		{
			foreach (MethodInfo methodInfo in CommandUtils.GetMethods())
			{
				Attribute[] customAttributes = Attribute.GetCustomAttributes(methodInfo, typeof(HotkeyCommandAttribute));
				for (int j = 0; j < customAttributes.Length; j++)
				{
					HotkeyCommandAttribute hotkeyCommandAttribute = customAttributes[j] as HotkeyCommandAttribute;
					if (hotkeyCommandAttribute != null)
					{
						HotkeyCommand hotkeyCommand = new HotkeyCommand
						{
							key = hotkeyCommandAttribute.key,
							delimiter = hotkeyCommandAttribute.delimiter,
							fullCommand = methodInfo.DeclaringType.FullName + "." + methodInfo.Name,
							method = methodInfo,
							parameterInfos = methodInfo.GetParameters()
						};
						if (hotkeyCommand.canExecute(hotkeyCommandAttribute.agrs, out hotkeyCommand.parameters))
						{
							HotkeyCommandHandler.hotkeyCommands.Add(hotkeyCommand);
						}
					}
				}
			}
			HotkeyCommandHandler.save();
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x000B1A90 File Offset: 0x000AFC90
		public static void save()
		{
			File.WriteAllText(Utils.PathHotkeyCommand, JsonConvert.SerializeObject(HotkeyCommandHandler.hotkeyCommands));
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x000B1AA8 File Offset: 0x000AFCA8
		public static bool handleHotkey(int key)
		{
			foreach (HotkeyCommand hotkeyCommand in HotkeyCommandHandler.hotkeyCommands)
			{
				if ((int)hotkeyCommand.key == key)
				{
					hotkeyCommand.execute();
					return true;
				}
			}
			return false;
		}

		// Token: 0x04001793 RID: 6035
		public static List<HotkeyCommand> hotkeyCommands = new List<HotkeyCommand>();
	}
}
