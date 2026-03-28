using System;

namespace Assets.src.g
{
	// Token: 0x020000AD RID: 173
	internal class ImageSource
	{
		// Token: 0x060007D8 RID: 2008 RVA: 0x00007A38 File Offset: 0x00005C38
		public ImageSource(string ID, sbyte version)
		{
			this.id = ID;
			this.version = version;
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x000711A8 File Offset: 0x0006F3A8
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
			Res.outz(string.Concat(new object[]
			{
				"vS size= ",
				ImageSource.vSource.size(),
				" vRMS size= ",
				ImageSource.vRms.size()
			}));
			bool flag = false;
			if (flag)
			{
				for (int j = 0; j < ImageSource.vSource.size(); j++)
				{
					ImageSource imageSource = (ImageSource)ImageSource.vSource.elementAt(j);
					if (!ImageSource.isExistID(imageSource.id))
					{
						myVector.addElement(imageSource);
					}
				}
				for (int k = 0; k < ImageSource.vRms.size(); k++)
				{
					ImageSource imageSource2 = (ImageSource)ImageSource.vRms.elementAt(k);
					if ((int)ImageSource.getVersionRMSByID(imageSource2.id) != (int)ImageSource.getCurrVersionByID(imageSource2.id))
					{
						myVector.addElement(imageSource2);
					}
				}
			}
			Service.gI().imageSource(myVector);
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00071378 File Offset: 0x0006F578
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

		// Token: 0x060007DB RID: 2011 RVA: 0x000713D8 File Offset: 0x0006F5D8
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

		// Token: 0x060007DC RID: 2012 RVA: 0x00071438 File Offset: 0x0006F638
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

		// Token: 0x060007DD RID: 2013 RVA: 0x00071484 File Offset: 0x0006F684
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

		// Token: 0x04000F08 RID: 3848
		public sbyte version;

		// Token: 0x04000F09 RID: 3849
		public string id;

		// Token: 0x04000F0A RID: 3850
		public static MyVector vSource = new MyVector();

		// Token: 0x04000F0B RID: 3851
		public static MyVector vRms = new MyVector();
	}
}
