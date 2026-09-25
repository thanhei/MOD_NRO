using System;
using System.Linq;
using System.Reflection;

namespace Mod.ModHelper.CommandMod
{
	// Token: 0x0200014B RID: 331
	internal static class CommandUtils
	{
		// Token: 0x06000FE2 RID: 4066 RVA: 0x000B1880 File Offset: 0x000AFA80
		public static MethodInfo[] getMethods(string typeFullName)
		{
			return typeof(CommandUtils).Assembly.GetTypes().FirstOrDefault<Type>((Type x) => x.FullName.ToLower() == typeFullName.ToLower()).GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod);
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000B18CC File Offset: 0x000AFACC
		public static MethodInfo[] GetMethods()
		{
			return (from x in typeof(CommandUtils).Assembly.GetTypes()
				where x.IsClass
				select x).SelectMany<Type, MethodInfo>((Type x) => x.GetMethods(BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod)).ToArray<MethodInfo>();
		}

		// Token: 0x04001789 RID: 6025
		private const BindingFlags STATIC_VOID = BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.InvokeMethod;
	}
}
