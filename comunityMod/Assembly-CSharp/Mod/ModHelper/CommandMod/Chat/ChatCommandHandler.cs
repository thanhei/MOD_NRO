using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Mod.ModHelper.CommandMod.Chat
{
	// Token: 0x02000153 RID: 339
	public class ChatCommandHandler
	{
		// Token: 0x06000FF4 RID: 4084 RVA: 0x000B1B28 File Offset: 0x000AFD28
		public static void loadDefault()
		{
			MethodInfo[] methods = CommandUtils.GetMethods();
			int num = methods.Length;
			for (int i = 0; i < num; i++)
			{
				MethodInfo methodInfo = methods[i];
				Attribute[] customAttributes = Attribute.GetCustomAttributes(methodInfo, typeof(ChatCommandAttribute));
				for (int j = 0; j < customAttributes.Length; j++)
				{
					ChatCommandAttribute chatCommandAttribute = customAttributes[j] as ChatCommandAttribute;
					if (chatCommandAttribute != null)
					{
						ChatCommandHandler.chatCommands.Add(new ChatCommand
						{
							command = chatCommandAttribute.command,
							delimiter = chatCommandAttribute.delimiter,
							fullCommand = methodInfo.DeclaringType.FullName + "." + methodInfo.Name,
							method = methodInfo,
							parameterInfos = methodInfo.GetParameters()
						});
					}
				}
			}
			ChatCommandHandler.save();
		}

		// Token: 0x06000FF5 RID: 4085 RVA: 0x000B1BEC File Offset: 0x000AFDEC
		public static void save()
		{
			File.WriteAllText(Utils.PathChatCommand, JsonConvert.SerializeObject(ChatCommandHandler.chatCommands));
		}

		// Token: 0x06000FF6 RID: 4086 RVA: 0x000B1C04 File Offset: 0x000AFE04
		public static bool execute(string command)
		{
			foreach (ChatCommand chatCommand in ChatCommandHandler.chatCommands)
			{
				int num = -1;
				if (!string.IsNullOrEmpty(chatCommand.command) && command.StartsWith(chatCommand.command))
				{
					num = chatCommand.command.Length;
				}
				else if (command.StartsWith(chatCommand.fullCommand))
				{
					num = chatCommand.fullCommand.Length;
				}
				if (num != -1)
				{
					string text = command.Substring(num);
					if (chatCommand.execute(text))
					{
						return true;
					}
				}
			}
			return ChatCommandHandler.executeFull(command);
		}

		// Token: 0x06000FF7 RID: 4087 RVA: 0x000B1CBC File Offset: 0x000AFEBC
		public static bool executeFull(string command)
		{
			Match match = Regex.Match(command, "^(([A-Za-z0-9.]+)\\.([A-Za-z0-9]+))(.*)$");
			if (!match.Success)
			{
				return false;
			}
			string value = match.Groups[1].Value;
			string value2 = match.Groups[2].Value;
			string value3 = match.Groups[3].Value;
			string value4 = match.Groups[4].Value;
			MethodInfo[] methods = CommandUtils.getMethods(value2);
			if (methods == null)
			{
				return false;
			}
			foreach (MethodInfo methodInfo in methods)
			{
				if (!(methodInfo.Name.ToLower() != value3.ToLower()) && new ChatCommand
				{
					command = null,
					fullCommand = value,
					method = methodInfo,
					parameterInfos = methodInfo.GetParameters()
				}.execute(value4))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000FF8 RID: 4088 RVA: 0x000B1DA4 File Offset: 0x000AFFA4
		public static bool handleChatText(string text)
		{
			if (!text.StartsWith("/"))
			{
				return false;
			}
			return text.Substring(1).Split(',', StringSplitOptions.None).Aggregate(false, (bool acc, string command) => ChatCommandHandler.execute(command.ToString()) || acc);
		}

		// Token: 0x04001797 RID: 6039
		public static List<ChatCommand> chatCommands = new List<ChatCommand>();
	}
}
