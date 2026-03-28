using System;
using System.Collections;
using UnityEngine;

// Token: 0x02000044 RID: 68
public class ImgByName
{
	// Token: 0x0600029C RID: 668 RVA: 0x000056E7 File Offset: 0x000038E7
	public static void SetImage(string name, Image img, sbyte nFrame)
	{
		ImgByName.hashImagePath.put(string.Empty + name, new MainImage(img, nFrame));
	}

	// Token: 0x0600029D RID: 669 RVA: 0x0001A0BC File Offset: 0x000182BC
	public static MainImage getImagePath(string nameImg, MyHashTable hash)
	{
		MainImage mainImage = (MainImage)hash.get(string.Empty + nameImg);
		if (mainImage == null)
		{
			mainImage = new MainImage();
			MainImage fromRms = ImgByName.getFromRms(nameImg);
			if (fromRms != null)
			{
				mainImage.img = fromRms.img;
				mainImage.nFrame = fromRms.nFrame;
			}
			hash.put(string.Empty + nameImg, mainImage);
		}
		mainImage.count = GameCanvas.timeNow / 1000L;
		if (mainImage.img == null)
		{
			mainImage.timeImageNull--;
			if (mainImage.timeImageNull <= 0)
			{
				Service.gI().getImgByName(nameImg);
				mainImage.timeImageNull = 200;
			}
		}
		return mainImage;
	}

	// Token: 0x0600029E RID: 670 RVA: 0x0001A174 File Offset: 0x00018374
	public static MainImage getFromRms(string nameImg)
	{
		string text = mGraphics.zoomLevel + "ImgByName_" + nameImg;
		MainImage mainImage = null;
		sbyte[] array = Rms.loadRMS(text);
		if (array == null)
		{
			return mainImage;
		}
		try
		{
			mainImage = new MainImage();
			mainImage.nFrame = array[0];
			mainImage.img = Image.createImage(array, 1, array.Length - 1);
			if (mainImage.img == null)
			{
			}
		}
		catch (Exception ex)
		{
			Debug.LogError(text + ">>>>>getFromRms: nulllllllllll 2222");
			return null;
		}
		return mainImage;
	}

	// Token: 0x0600029F RID: 671 RVA: 0x0001A208 File Offset: 0x00018408
	public static void saveRMS(string nameImg, sbyte nFrame, sbyte[] data)
	{
		string text = mGraphics.zoomLevel + "ImgByName_" + nameImg;
		DataOutputStream dataOutputStream = new DataOutputStream(data.Length + 1);
		int i = 0;
		try
		{
			dataOutputStream.writeByte(nFrame);
			for (i = 0; i < data.Length; i++)
			{
				dataOutputStream.writeByte(data[i]);
			}
			Rms.saveRMS(text, dataOutputStream.toByteArray());
			dataOutputStream.close();
		}
		catch (Exception ex)
		{
			Debug.LogError(string.Concat(new object[]
			{
				i,
				">>Errr save rms: ",
				text,
				"  ",
				ex.ToString()
			}));
		}
	}

	// Token: 0x060002A0 RID: 672 RVA: 0x0001A2C0 File Offset: 0x000184C0
	public static void checkDelHash(MyHashTable hash, int minute, bool isTrue)
	{
		MyVector myVector = new MyVector("checkDelHash");
		if (isTrue)
		{
			hash.clear();
		}
		else
		{
			IDictionaryEnumerator enumerator = hash.GetEnumerator();
			while (enumerator.MoveNext())
			{
				MainImage mainImage = (MainImage)enumerator.Value;
				if (GameCanvas.timeNow / 1000L - mainImage.count > (long)(minute * 60))
				{
					string o = (string)enumerator.Key;
					myVector.addElement(o);
				}
			}
			for (int i = 0; i < myVector.size(); i++)
			{
				hash.remove((string)myVector.elementAt(i));
			}
		}
	}

	// Token: 0x04000339 RID: 825
	public static MyHashTable hashImagePath = new MyHashTable();
}
