using System;
using UnityEngine;

// Token: 0x02000013 RID: 19
public class BgItemMn
{
	// Token: 0x060000BB RID: 187 RVA: 0x000076C4 File Offset: 0x000058C4
	public static Image blendImage(Image img, int layer, int idImage)
	{
		int num = TileMap.tileID - 1;
		Image image = img;
		if (num == 0 && layer == 1)
		{
			image = mGraphics.blend(img, 0.3f, 807956);
		}
		if (num == 1 && layer == 1)
		{
			image = mGraphics.blend(img, 0.35f, 739339);
		}
		if (num == 2 && layer == 1)
		{
			image = mGraphics.blend(img, 0.1f, 3977975);
		}
		if (num == 3)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.2f, 15265992);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.1f, 15265992);
			}
		}
		if (num == 4)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.3f, 1330178);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.1f, 1330178);
			}
		}
		if (num == 6)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.3f, 420382);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.15f, 420382);
			}
		}
		if (num == 5)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.35f, 3270903);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.15f, 3270903);
			}
		}
		if (num == 8)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.3f, 7094528);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.15f, 7094528);
			}
		}
		if (num == 9)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.3f, 12113627);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.15f, 12113627);
			}
		}
		if (num == 10 && layer == 1)
		{
			image = mGraphics.blend(img, 0.3f, 14938312);
		}
		if (num == 10 && layer == 1)
		{
			image = mGraphics.blend(img, 0.2f, 14938312);
		}
		if (num == 11)
		{
			if (layer == 1)
			{
				image = mGraphics.blend(img, 0.3f, 0);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.15f, 0);
			}
		}
		if (num > 11)
		{
			if (layer == 1 || layer == 2)
			{
				image = mGraphics.blend(img, 0.3f, 0);
			}
			if (layer == 3)
			{
				image = mGraphics.blend(img, 0.15f, 0);
			}
		}
		byte[] byteArray = BgItemMn.getByteArray(image);
		Rms.saveRMS(string.Concat(new string[]
		{
			"x",
			mGraphics.zoomLevel.ToString(),
			"blend",
			idImage.ToString(),
			"layer",
			layer.ToString()
		}), ArrayCast.cast(byteArray));
		return image;
	}

	// Token: 0x060000BC RID: 188 RVA: 0x00007918 File Offset: 0x00005B18
	public static byte[] getByteArray(Image img)
	{
		byte[] array;
		try
		{
			array = img.texture.EncodeToPNG();
		}
		catch (Exception)
		{
			array = null;
		}
		return array;
	}

	// Token: 0x060000BD RID: 189 RVA: 0x0000794C File Offset: 0x00005B4C
	public static void blendcurrBg(short id, Image img)
	{
		for (int i = 0; i < TileMap.vCurrItem.size(); i++)
		{
			BgItem bgItem = (BgItem)TileMap.vCurrItem.elementAt(i);
			if (bgItem.idImage == id && !bgItem.isNotBlend() && bgItem.layer != 2 && bgItem.layer != 4 && !BgItem.imgNew.containsKey(bgItem.idImage.ToString() + "blend" + bgItem.layer.ToString()))
			{
				sbyte[] array = Rms.loadRMS(string.Concat(new string[]
				{
					"x",
					mGraphics.zoomLevel.ToString(),
					"blend",
					id.ToString(),
					"layer",
					bgItem.layer.ToString()
				}));
				if (array == null)
				{
					BgItem.imgNew.put(bgItem.idImage.ToString() + "blend" + bgItem.layer.ToString(), BgItemMn.blendImage(img, (int)bgItem.layer, (int)bgItem.idImage));
				}
				else
				{
					Image image = Image.createImage(array, 0, array.Length);
					BgItem.imgNew.put(bgItem.idImage.ToString() + "blend" + bgItem.layer.ToString(), image);
				}
			}
		}
	}
}
