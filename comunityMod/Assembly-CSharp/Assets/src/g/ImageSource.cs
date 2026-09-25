using System;

namespace Assets.src.g
{
	// Token: 0x020001B6 RID: 438
	internal class ImageSource
	{
		// Token: 0x060012C1 RID: 4801 RVA: 0x000C5182 File Offset: 0x000C3382
		public ImageSource(string ID, sbyte version)
		{
			this.id = ID;
			this.version = version;
		}

		// Token: 0x060012C2 RID: 4802 RVA: 0x000C5198 File Offset: 0x000C3398
		public static void checkRMS()
		{
			MyVector myVector = new MyVector();
			sbyte[] array = Rms.loadRMS("ImageSource");
			if (array == null)
			{
				Service.gI().imageSource(myVector);
				return;
			}
			ImageSource.vRms = new MyVector();
			DataInputStream dataInputStream = new DataInputStream(array);
			if (dataInputStream == null)
			{
				return;
			}
			try
			{
				short num = dataInputStream.readShort();
				string[] array2 = new string[(int)num];
				sbyte[] array3 = new sbyte[(int)num];
				for (int i = 0; i < (int)num; i++)
				{
					array2[i] = dataInputStream.readUTF();
					array3[i] = dataInputStream.readByte();
					ImageSource.vRms.addElement(new ImageSource(array2[i], array3[i]));
				}
				dataInputStream.close();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
			Res.outz("vS size= " + ImageSource.vSource.size().ToString() + " vRMS size= " + ImageSource.vRms.size().ToString());
			Service.gI().imageSource(myVector);
		}

		// Token: 0x060012C3 RID: 4803 RVA: 0x000C529C File Offset: 0x000C349C
		public static sbyte getVersionRMSByID(string id)
		{
			for (int i = 0; i < ImageSource.vRms.size(); i++)
			{
				if (id.Equals(((ImageSource)ImageSource.vRms.elementAt(i)).id))
				{
					return ((ImageSource)ImageSource.vRms.elementAt(i)).version;
				}
			}
			return -1;
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x000C52F4 File Offset: 0x000C34F4
		public static sbyte getCurrVersionByID(string id)
		{
			for (int i = 0; i < ImageSource.vSource.size(); i++)
			{
				if (id.Equals(((ImageSource)ImageSource.vSource.elementAt(i)).id))
				{
					return ((ImageSource)ImageSource.vSource.elementAt(i)).version;
				}
			}
			return -1;
		}

		// Token: 0x060012C5 RID: 4805 RVA: 0x000C534C File Offset: 0x000C354C
		public static bool isExistID(string id)
		{
			for (int i = 0; i < ImageSource.vRms.size(); i++)
			{
				if (id.Equals(((ImageSource)ImageSource.vRms.elementAt(i)).id))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060012C6 RID: 4806 RVA: 0x000C5390 File Offset: 0x000C3590
		public static void saveRMS()
		{
			DataOutputStream dataOutputStream = new DataOutputStream();
			try
			{
				dataOutputStream.writeShort((short)ImageSource.vSource.size());
				for (int i = 0; i < ImageSource.vSource.size(); i++)
				{
					dataOutputStream.writeUTF(((ImageSource)ImageSource.vSource.elementAt(i)).id);
					dataOutputStream.writeByte(((ImageSource)ImageSource.vSource.elementAt(i)).version);
				}
				Rms.saveRMS("ImageSource", dataOutputStream.toByteArray());
				dataOutputStream.close();
			}
			catch (Exception ex)
			{
				ex.StackTrace.ToString();
			}
		}

		// Token: 0x040019DC RID: 6620
		public sbyte version;

		// Token: 0x040019DD RID: 6621
		public string id;

		// Token: 0x040019DE RID: 6622
		public static MyVector vSource = new MyVector();

		// Token: 0x040019DF RID: 6623
		public static MyVector vRms = new MyVector();
	}
}
