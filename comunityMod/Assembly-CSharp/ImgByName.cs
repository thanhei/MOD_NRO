using System;
using System.Collections;
using UnityEngine;

// Token: 0x0200004C RID: 76
public class ImgByName
{
	// Token: 0x0600045F RID: 1119 RVA: 0x0004B254 File Offset: 0x00049454
	public static void SetImage(string name, Image img, sbyte nFrame)
	{
		ImgByName.hashImagePath.put(string.Empty + name, new MainImage(img, nFrame));
	}

	// Token: 0x06000460 RID: 1120 RVA: 0x0004B274 File Offset: 0x00049474
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

	// Token: 0x06000461 RID: 1121 RVA: 0x0004B320 File Offset: 0x00049520
	public static MainImage getFromRms(string nameImg)
	{
		string text = mGraphics.zoomLevel.ToString() + "ImgByName_" + nameImg;
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
			Image img = mainImage.img;
		}
		catch (Exception)
		{
			Debug.LogError(text + ">>>>>getFromRms: nulllllllllll 2222");
			return null;
		}
		return mainImage;
	}

	// Token: 0x06000462 RID: 1122 RVA: 0x0004B3A8 File Offset: 0x000495A8
	public static void saveRMS(string nameImg, sbyte nFrame, sbyte[] data)
	{
		string text = mGraphics.zoomLevel.ToString() + "ImgByName_" + nameImg;
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
			Debug.LogError(string.Concat(new string[]
			{
				i.ToString(),
				">>Errr save rms: ",
				text,
				"  ",
				ex.ToString()
			}));
		}
	}

	// Token: 0x06000463 RID: 1123 RVA: 0x0004B454 File Offset: 0x00049654
	public static void checkDelHash(MyHashTable hash, int minute, bool isTrue)
	{
		MyVector myVector = new MyVector("checkDelHash");
		if (isTrue)
		{
			hash.clear();
			return;
		}
		IDictionaryEnumerator enumerator = hash.GetEnumerator();
		while (enumerator.MoveNext())
		{
			MainImage mainImage = (MainImage)enumerator.Value;
			if (GameCanvas.timeNow / 1000L - mainImage.count > (long)(minute * 60))
			{
				myVector.addElement((string)enumerator.Key);
			}
		}
		for (int i = 0; i < myVector.size(); i++)
		{
			hash.remove((string)myVector.elementAt(i));
		}
	}

	// Token: 0x04000908 RID: 2312
	public static MyHashTable hashImagePath = new MyHashTable();
}
