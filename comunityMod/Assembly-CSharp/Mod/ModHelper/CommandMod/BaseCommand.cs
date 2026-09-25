using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Newtonsoft.Json;

namespace Mod.ModHelper.CommandMod
{
	// Token: 0x02000148 RID: 328
	public abstract class BaseCommand
	{
		// Token: 0x06000FD7 RID: 4055 RVA: 0x000B160C File Offset: 0x000AF80C
		public bool canExecute(string args, out object[] parameters)
		{
			parameters = null;
			BaseCommand.preprocessingArgs(ref args);
			string[] array;
			return (args == "" && this.parameterInfos.Length == 0) || (this.checkCountArgs(args, out array) && this.checkTypeArgs(array, out parameters));
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000B1658 File Offset: 0x000AF858
		public bool execute(string args)
		{
			object[] array;
			if (!this.canExecute(args, out array))
			{
				return false;
			}
			this.method.Invoke(null, array);
			return true;
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x000B1681 File Offset: 0x000AF881
		private static void preprocessingArgs(ref string args)
		{
			args = args.Trim();
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x000B168C File Offset: 0x000AF88C
		private bool checkCountArgs(string args, out string[] arguments)
		{
			arguments = args.Split(this.delimiter, StringSplitOptions.None);
			if (this.parameterInfos.Length != 0)
			{
				if (((Attribute[])this.parameterInfos[0].GetCustomAttributes(false)).Any<Attribute>((Attribute a) => a is ParamArrayAttribute))
				{
					return arguments.Length >= this.parameterInfos.Length;
				}
			}
			return this.parameterInfos.Length == arguments.Length;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x000B170C File Offset: 0x000AF90C
		private bool checkTypeArgs(string[] arguments, out object[] parameters)
		{
			parameters = null;
			bool flag;
			try
			{
				if (((Attribute[])this.parameterInfos[0].GetCustomAttributes(false)).Any<Attribute>((Attribute a) => a is ParamArrayAttribute))
				{
					Type elementType = this.parameterInfos[this.parameterInfos.Length - 1].ParameterType.GetElementType();
					List<object> list = new List<object>();
					for (int i = 0; i < this.parameterInfos.Length - 1; i++)
					{
						list.Add(Convert.ChangeType(arguments[i], this.parameterInfos[i].ParameterType));
					}
					Array array = Array.CreateInstance(elementType, arguments.Length - this.parameterInfos.Length + 1);
					for (int j = 0; j < array.Length; j++)
					{
						array.SetValue(Convert.ChangeType(arguments[j], elementType), j);
					}
					list.Add(array);
					parameters = list.ToArray();
				}
				else
				{
					parameters = new object[arguments.Length];
					for (int k = 0; k < arguments.Length; k++)
					{
						parameters[k] = Convert.ChangeType(arguments[k], this.parameterInfos[k].ParameterType);
					}
				}
				flag = true;
			}
			catch (InvalidCastException)
			{
				flag = false;
			}
			return flag;
		}

		// Token: 0x04001782 RID: 6018
		public char delimiter;

		// Token: 0x04001783 RID: 6019
		[JsonIgnore]
		public MethodInfo method;

		// Token: 0x04001784 RID: 6020
		[JsonIgnore]
		public ParameterInfo[] parameterInfos;
	}
}
