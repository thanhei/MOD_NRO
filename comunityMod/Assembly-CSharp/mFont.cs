using System;
using System.Collections;
using UnityEngine;

// Token: 0x020000C2 RID: 194
public class mFont
{
	// Token: 0x060009D9 RID: 2521 RVA: 0x0008D7C4 File Offset: 0x0008B9C4
	public mFont(string strFont, string pathImage, string pathData, int space)
	{
		try
		{
			this.strFont = strFont;
			this.space = space;
			this.pathImage = pathImage;
			DataInputStream dataInputStream = null;
			this.reloadImage();
			try
			{
				dataInputStream = MyStream.readFile(pathData);
				this.fImages = new int[(int)dataInputStream.readShort()][];
				for (int i = 0; i < this.fImages.Length; i++)
				{
					this.fImages[i] = new int[4];
					this.fImages[i][0] = (int)dataInputStream.readShort();
					this.fImages[i][1] = (int)dataInputStream.readShort();
					this.fImages[i][2] = (int)dataInputStream.readShort();
					this.fImages[i][3] = (int)dataInputStream.readShort();
					this.setHeight(this.fImages[i][3]);
				}
				dataInputStream.close();
			}
			catch (Exception)
			{
				try
				{
					dataInputStream.close();
				}
				catch (Exception ex)
				{
					ex.StackTrace.ToString();
				}
			}
		}
		catch (Exception ex2)
		{
			ex2.StackTrace.ToString();
		}
	}

	// Token: 0x060009DA RID: 2522 RVA: 0x0008D904 File Offset: 0x0008BB04
	public mFont(sbyte id)
	{
		string text = "chelthm";
		if ((id > 0 && id < 10) || id == 19)
		{
			this.yAdd = 1;
			text = "barmeneb";
		}
		else if (id >= 10 && id <= 18)
		{
			text = "chelthm";
			this.yAdd = 2;
		}
		else if (id > 24)
		{
			text = "staccato";
		}
		this.id = id;
		this.myFont = (Font)Resources.Load("FontSys/x" + mGraphics.zoomLevel.ToString() + "/" + text);
		if (id < 25)
		{
			this.color1 = this.setColorFont(id);
			this.color2 = this.setColorFont(id);
		}
		else
		{
			this.color1 = this.bigColor((int)id);
			this.color2 = this.bigColor((int)id);
		}
		this.wO = this.getWidthExactOf("o");
	}

	// Token: 0x060009DB RID: 2523 RVA: 0x0008DA08 File Offset: 0x0008BC08
	public static void init()
	{
		if (mGraphics.zoomLevel == 1)
		{
			mFont.tahoma_7b_red = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_red.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_blue = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_blue.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_white = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_white.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_yellow = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_yellow.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_yellowSmall = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_yellow.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_dark = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_brown.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_green2 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_green2.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_green = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_green.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_focus = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_focus.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7b_unfocus = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7b_unfocus.png", "/myfont/tahoma_7b", 0);
			mFont.tahoma_7 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_blue1 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_blue1.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_green2 = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_green2.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_yellow = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_yellow.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_grey = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_grey.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_red = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_red.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_blue = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_blue.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_green = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_green.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_7_white = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_7_white.png", "/myfont/tahoma_7", 0);
			mFont.tahoma_8b = new mFont(" 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW", "/myfont/tahoma_8b.png", "/myfont/tahoma_8b", -1);
			mFont.number_yellow = new mFont(" 0123456789+-", "/myfont/number_yellow.png", "/myfont/number", 0);
			mFont.number_red = new mFont(" 0123456789+-", "/myfont/number_red.png", "/myfont/number", 0);
			mFont.number_green = new mFont(" 0123456789+-", "/myfont/number_green.png", "/myfont/number", 0);
			mFont.number_gray = new mFont(" 0123456789+-", "/myfont/number_gray.png", "/myfont/number", 0);
			mFont.number_orange = new mFont(" 0123456789+-", "/myfont/number_orange.png", "/myfont/number", 0);
			mFont.bigNumber_red = mFont.number_red;
			mFont.bigNumber_While = mFont.tahoma_7b_white;
			mFont.bigNumber_yellow = mFont.number_yellow;
			mFont.bigNumber_green = mFont.number_green;
			mFont.bigNumber_orange = mFont.number_orange;
			mFont.bigNumber_blue = mFont.tahoma_7_blue1;
			mFont.nameFontRed = mFont.tahoma_7_red;
			mFont.nameFontYellow = mFont.tahoma_7_yellow;
			mFont.nameFontGreen = mFont.tahoma_7_green;
			mFont.tahoma_7_greySmall = mFont.tahoma_7_grey;
			mFont.tahoma_7b_yellowSmall2 = mFont.tahoma_7_yellow;
			mFont.tahoma_7b_green2Small = mFont.tahoma_7b_green2;
			mFont.tahoma_7_whiteSmall = mFont.tahoma_7_white;
			mFont.tahoma_7b_greenSmall = mFont.tahoma_7b_green;
			mFont.tahoma_7_blue1Small = mFont.tahoma_7_blue1;
			return;
		}
		mFont.gI = new mFont(0);
		mFont.tahoma_7b_red = new mFont(1);
		mFont.tahoma_7b_blue = new mFont(2);
		mFont.tahoma_7b_white = new mFont(3);
		mFont.tahoma_7b_yellow = new mFont(4);
		mFont.tahoma_7b_yellowSmall = new mFont(4);
		mFont.tahoma_7b_dark = new mFont(5);
		mFont.tahoma_7b_green2 = new mFont(6);
		mFont.tahoma_7b_green = new mFont(7);
		mFont.tahoma_7b_focus = new mFont(8);
		mFont.tahoma_7b_unfocus = new mFont(9);
		mFont.tahoma_7 = new mFont(10);
		mFont.tahoma_7_blue1 = new mFont(11);
		mFont.tahoma_7_blue1Small = mFont.tahoma_7_blue1;
		mFont.tahoma_7_green2 = new mFont(12);
		mFont.tahoma_7_yellow = new mFont(13);
		mFont.tahoma_7_grey = new mFont(14);
		mFont.tahoma_7_red = new mFont(15);
		mFont.tahoma_7_blue = new mFont(16);
		mFont.tahoma_7_green = new mFont(17);
		mFont.tahoma_7_white = new mFont(18);
		mFont.tahoma_8b = new mFont(19);
		mFont.number_yellow = new mFont(20);
		mFont.number_red = new mFont(21);
		mFont.number_green = new mFont(22);
		mFont.number_gray = new mFont(23);
		mFont.number_orange = new mFont(24);
		mFont.bigNumber_red = new mFont(25);
		mFont.bigNumber_yellow = new mFont(26);
		mFont.bigNumber_green = new mFont(27);
		mFont.bigNumber_While = new mFont(28);
		mFont.bigNumber_blue = new mFont(29);
		mFont.bigNumber_orange = new mFont(30);
		mFont.bigNumber_black = new mFont(31);
		mFont.nameFontRed = mFont.tahoma_7b_red;
		mFont.nameFontYellow = mFont.tahoma_7_yellow;
		mFont.nameFontGreen = mFont.tahoma_7_green;
		mFont.tahoma_7_greySmall = mFont.tahoma_7_grey;
		mFont.tahoma_7b_yellowSmall2 = mFont.tahoma_7_yellow;
		mFont.tahoma_7b_green2Small = mFont.tahoma_7b_green2;
		mFont.tahoma_7_whiteSmall = mFont.tahoma_7_white;
		mFont.tahoma_7b_greenSmall = mFont.tahoma_7b_green;
		mFont.yAddFont = 1;
		if (mGraphics.zoomLevel == 1)
		{
			mFont.yAddFont = -3;
		}
	}

	// Token: 0x060009DC RID: 2524 RVA: 0x0008DF32 File Offset: 0x0008C132
	public void setHeight(int height)
	{
		this.height = height;
	}

	// Token: 0x060009DD RID: 2525 RVA: 0x0008DF3C File Offset: 0x0008C13C
	public Color setColor(int rgb)
	{
		int num = rgb & 255;
		int num2 = (rgb >> 8) & 255;
		float num3 = (float)((rgb >> 16) & 255);
		float num4 = (float)num / 256f;
		float num5 = (float)num2 / 256f;
		return new Color(num3 / 256f, num5, num4);
	}

	// Token: 0x060009DE RID: 2526 RVA: 0x0008DF88 File Offset: 0x0008C188
	public Color bigColor(int id)
	{
		return (new Color[]
		{
			Color.red,
			Color.yellow,
			Color.green,
			Color.white,
			this.setColor(40404),
			Color.red,
			Color.black
		})[id - 25];
	}

	// Token: 0x060009DF RID: 2527 RVA: 0x0008DFFE File Offset: 0x0008C1FE
	public void setColorByID(int ID)
	{
		this.color1 = this.setColor(mFont.colorJava[ID]);
		this.color2 = this.setColor(mFont.colorJava[ID]);
	}

	// Token: 0x060009E0 RID: 2528 RVA: 0x0008E028 File Offset: 0x0008C228
	public void setTypePaint(mGraphics g, string st, int x, int y, int align, sbyte idFont)
	{
		sbyte b = this.id;
		if (idFont > 0)
		{
			b = idFont;
		}
		x--;
		if (this.id > 24)
		{
			Color[] array = new Color[]
			{
				this.setColor(6029312),
				this.setColor(7169025),
				this.setColor(7680),
				this.setColor(0),
				this.setColor(9264),
				this.setColor(6029312)
			};
			this.color1 = array[(int)(this.id - 25)];
			this.color2 = array[(int)(this.id - 25)];
			this._drawString(g, st, x + 1, y, align);
			this._drawString(g, st, x - 1, y, align);
			this._drawString(g, st, x, y - 1, align);
			this._drawString(g, st, x, y + 1, align);
			this._drawString(g, st, x + 1, y + 1, align);
			this._drawString(g, st, x + 1, y - 1, align);
			this._drawString(g, st, x - 1, y - 1, align);
			this._drawString(g, st, x - 1, y + 1, align);
			this.color1 = this.bigColor((int)this.id);
			this.color2 = this.bigColor((int)this.id);
		}
		else
		{
			this.setColorByID((int)b);
		}
		this._drawString(g, st, x, y - this.yAdd, align);
	}

	// Token: 0x060009E1 RID: 2529 RVA: 0x0008E1B0 File Offset: 0x0008C3B0
	public Color setColorFont(sbyte id)
	{
		return this.setColor(mFont.colorJava[(int)id]);
	}

	// Token: 0x060009E2 RID: 2530 RVA: 0x0008E1C0 File Offset: 0x0008C3C0
	public void drawString(mGraphics g, string st, int x, int y, int align)
	{
		if (mGraphics.zoomLevel == 1)
		{
			int length = st.Length;
			int num = ((align == 0) ? x : ((align != 1) ? (x - (this.getWidth(st) >> 1)) : (x - this.getWidth(st))));
			for (int i = 0; i < length; i++)
			{
				int num2 = this.strFont.IndexOf(st[i].ToString() + string.Empty);
				if (num2 == -1)
				{
					num2 = 0;
				}
				if (num2 > -1)
				{
					int num3 = this.fImages[num2][0];
					int num4 = this.fImages[num2][1];
					int num5 = this.fImages[num2][2];
					int num6 = this.fImages[num2][3];
					if (num4 + num6 > this.imgFont.texture.height)
					{
						num4 -= this.imgFont.texture.height;
						num3 = this.imgFont.texture.width / 2;
					}
					g.drawRegion(this.imgFont, num3, num4, num5, num6, 0, num, y, 20);
				}
				num += this.fImages[num2][2] + this.space;
			}
			return;
		}
		this.setTypePaint(g, st, x, y, align, 0);
	}

	// Token: 0x060009E3 RID: 2531 RVA: 0x0008E2F4 File Offset: 0x0008C4F4
	public void drawStringBorder(mGraphics g, string st, int x, int y, int align)
	{
		if (mGraphics.zoomLevel == 1)
		{
			this.drawString(g, st, x, y, align);
			return;
		}
		this.setTypePaint(g, st, x, y, align, 0);
	}

	// Token: 0x060009E4 RID: 2532 RVA: 0x0008E31A File Offset: 0x0008C51A
	public void drawStringBorder(mGraphics g, string st, int x, int y, int align, mFont font2)
	{
		if (mGraphics.zoomLevel == 1)
		{
			this.drawString(g, st, x, y, align, font2);
			return;
		}
		this.drawStringBd(g, st, x, y, align, font2);
	}

	// Token: 0x060009E5 RID: 2533 RVA: 0x0008E344 File Offset: 0x0008C544
	public void drawStringBd(mGraphics g, string st, int x, int y, int align, mFont font)
	{
		this.setTypePaint(g, st, x - 1, y - 1, align, 0);
		this.setTypePaint(g, st, x - 1, y + 1, align, 0);
		this.setTypePaint(g, st, x + 1, y - 1, align, 0);
		this.setTypePaint(g, st, x + 1, y + 1, align, 0);
		this.setTypePaint(g, st, x, y - 1, align, 0);
		this.setTypePaint(g, st, x, y + 1, align, 0);
		this.setTypePaint(g, st, x + 1, y, align, 0);
		this.setTypePaint(g, st, x - 1, y, align, 0);
		this.setTypePaint(g, st, x, y, align, 0);
	}

	// Token: 0x060009E6 RID: 2534 RVA: 0x0008E3E8 File Offset: 0x0008C5E8
	public void drawString(mGraphics g, string st, int x, int y, int align, mFont font)
	{
		if (mGraphics.zoomLevel == 1)
		{
			int length = st.Length;
			int num = ((align == 0) ? x : ((align != 1) ? (x - (this.getWidth(st) >> 1)) : (x - this.getWidth(st))));
			for (int i = 0; i < length; i++)
			{
				int num2 = this.strFont.IndexOf(st[i]);
				if (num2 == -1)
				{
					num2 = 0;
				}
				if (num2 > -1)
				{
					int num3 = this.fImages[num2][0];
					int num4 = this.fImages[num2][1];
					int num5 = this.fImages[num2][2];
					int num6 = this.fImages[num2][3];
					if (num4 + num6 > this.imgFont.texture.height)
					{
						num4 -= this.imgFont.texture.height;
						num3 = this.imgFont.texture.width / 2;
					}
					if (!GameCanvas.lowGraphic && font != null)
					{
						g.drawRegion(font.imgFont, num3, num4, num5, num6, 0, num + 1, y, 20);
						g.drawRegion(font.imgFont, num3, num4, num5, num6, 0, num, y + 1, 20);
					}
					g.drawRegion(this.imgFont, num3, num4, num5, num6, 0, num, y, 20);
				}
				num += this.fImages[num2][2] + this.space;
			}
			return;
		}
		this.setTypePaint(g, st, x, y + 1, align, font.id);
		this.setTypePaint(g, st, x, y, align, 0);
	}

	// Token: 0x060009E7 RID: 2535 RVA: 0x0008E564 File Offset: 0x0008C764
	public MyVector splitFontVector(string src, int lineWidth)
	{
		MyVector myVector = new MyVector();
		string text = string.Empty;
		for (int i = 0; i < src.Length; i++)
		{
			if (src[i] == '\n' || src[i] == '\b')
			{
				myVector.addElement(text);
				text = string.Empty;
			}
			else
			{
				text += src[i].ToString();
				if (this.getWidth(text) > lineWidth)
				{
					int num = text.Length - 1;
					while (num >= 0 && text[num] != ' ')
					{
						num--;
					}
					if (num < 0)
					{
						num = text.Length - 1;
					}
					myVector.addElement(text.Substring(0, num));
					i = i - (text.Length - num) + 1;
					text = string.Empty;
				}
				if (i == src.Length - 1 && !text.Trim().Equals(string.Empty))
				{
					myVector.addElement(text);
				}
			}
		}
		return myVector;
	}

	// Token: 0x060009E8 RID: 2536 RVA: 0x0008E658 File Offset: 0x0008C858
	public string splitFirst(string str)
	{
		string text = string.Empty;
		bool flag = false;
		for (int i = 0; i < str.Length; i++)
		{
			if (!flag)
			{
				string text2 = str.Substring(i);
				text = ((!this.compare(text2, " ")) ? (text + text2) : (text + str[i].ToString() + "-"));
				flag = true;
			}
			else if (str[i] == ' ')
			{
				flag = false;
			}
		}
		return text;
	}

	// Token: 0x060009E9 RID: 2537 RVA: 0x0008E6D0 File Offset: 0x0008C8D0
	public string[] splitStrInLine(string src, int lineWidth)
	{
		ArrayList arrayList = this.splitStrInLineA(src, lineWidth);
		string[] array = new string[arrayList.Count];
		for (int i = 0; i < arrayList.Count; i++)
		{
			array[i] = (string)arrayList[i];
		}
		return array;
	}

	// Token: 0x060009EA RID: 2538 RVA: 0x0008E714 File Offset: 0x0008C914
	public ArrayList splitStrInLineA(string src, int lineWidth)
	{
		ArrayList arrayList = new ArrayList();
		int num = 0;
		int num2 = 0;
		int length = src.Length;
		if (length < 5)
		{
			arrayList.Add(src);
			return arrayList;
		}
		string text = string.Empty;
		try
		{
			for (;;)
			{
				if (this.getWidthNotExactOf(text) < lineWidth)
				{
					text += src[num2].ToString();
					num2++;
					if (src[num2] != '\n')
					{
						if (num2 < length - 1)
						{
							continue;
						}
						num2 = length - 1;
					}
				}
				if (num2 != length - 1 && src[num2 + 1] != ' ')
				{
					int num3 = num2;
					while (src[num2 + 1] != '\n' && (src[num2 + 1] != ' ' || src[num2] == ' ') && num2 != num)
					{
						num2--;
					}
					if (num2 == num)
					{
						num2 = num3;
					}
				}
				string text2 = src.Substring(num, num2 + 1 - num);
				if (text2[0] == '\n')
				{
					text2 = text2.Substring(1, text2.Length - 1);
				}
				if (text2[text2.Length - 1] == '\n')
				{
					text2 = text2.Substring(0, text2.Length - 1);
				}
				arrayList.Add(text2);
				if (num2 == length - 1)
				{
					break;
				}
				num = num2 + 1;
				while (num != length - 1 && src[num] == ' ')
				{
					num++;
				}
				if (num == length - 1)
				{
					break;
				}
				num2 = num;
				text = string.Empty;
			}
		}
		catch (Exception ex)
		{
			Cout.LogWarning(string.Concat(new string[]
			{
				"EXCEPTION WHEN REAL SPLIT ",
				src,
				"\nend=",
				num2.ToString(),
				"\n",
				ex.Message,
				"\n",
				ex.StackTrace
			}));
			arrayList.Add(src);
		}
		return arrayList;
	}

	// Token: 0x060009EB RID: 2539 RVA: 0x0008E8E0 File Offset: 0x0008CAE0
	public string[] splitFontArray(string src, int lineWidth)
	{
		MyVector myVector = this.splitFontVector(src, lineWidth);
		string[] array = new string[myVector.size()];
		for (int i = 0; i < myVector.size(); i++)
		{
			array[i] = (string)myVector.elementAt(i);
		}
		return array;
	}

	// Token: 0x060009EC RID: 2540 RVA: 0x0008E924 File Offset: 0x0008CB24
	public bool compare(string strSource, string str)
	{
		for (int i = 0; i < strSource.Length; i++)
		{
			if ((string.Empty + strSource[i].ToString()).Equals(str))
			{
				return true;
			}
		}
		return false;
	}

	// Token: 0x060009ED RID: 2541 RVA: 0x0008E968 File Offset: 0x0008CB68
	public int getWidth(string s)
	{
		if (mGraphics.zoomLevel == 1)
		{
			int num = 0;
			for (int i = 0; i < s.Length; i++)
			{
				int num2 = this.strFont.IndexOf(s[i]);
				if (num2 == -1)
				{
					num2 = 0;
				}
				num += this.fImages[num2][2] + this.space;
			}
			return num;
		}
		return this.getWidthExactOf(s);
	}

	// Token: 0x060009EE RID: 2542 RVA: 0x0008E9C8 File Offset: 0x0008CBC8
	public int getWidthExactOf(string s)
	{
		int num;
		try
		{
			num = (int)new GUIStyle
			{
				font = this.myFont
			}.CalcSize(new GUIContent(s)).x / mGraphics.zoomLevel;
		}
		catch (Exception ex)
		{
			Cout.LogError(string.Concat(new string[] { "GET WIDTH OF ", s, " FAIL.\n", ex.Message, "\n", ex.StackTrace }));
			num = this.getWidthNotExactOf(s);
		}
		return num;
	}

	// Token: 0x060009EF RID: 2543 RVA: 0x0008EA5C File Offset: 0x0008CC5C
	public int getWidthNotExactOf(string s)
	{
		return s.Length * this.wO / mGraphics.zoomLevel;
	}

	// Token: 0x060009F0 RID: 2544 RVA: 0x0008EA74 File Offset: 0x0008CC74
	public int getHeight()
	{
		if (mGraphics.zoomLevel == 1)
		{
			return this.height;
		}
		if (this.height > 0)
		{
			return this.height / mGraphics.zoomLevel;
		}
		GUIStyle guistyle = new GUIStyle();
		guistyle.font = this.myFont;
		try
		{
			this.height = (int)guistyle.CalcSize(new GUIContent("Adg")).y + 2;
		}
		catch (Exception ex)
		{
			Cout.LogError("FAIL GET HEIGHT " + ex.StackTrace);
			this.height = 20;
		}
		return this.height / mGraphics.zoomLevel;
	}

	// Token: 0x060009F1 RID: 2545 RVA: 0x0008EB14 File Offset: 0x0008CD14
	public void _drawString(mGraphics g, string st, int x0, int y0, int align)
	{
		y0 += mFont.yAddFont;
		GUIStyle guistyle = new GUIStyle(GUI.skin.label);
		guistyle.font = this.myFont;
		float num = 0f;
		float num2 = 0f;
		switch (align)
		{
		case 0:
			num = (float)x0;
			num2 = (float)y0;
			guistyle.alignment = TextAnchor.UpperLeft;
			break;
		case 1:
			num = (float)(x0 - GameCanvas.w);
			num2 = (float)y0;
			guistyle.alignment = TextAnchor.UpperRight;
			break;
		case 2:
		case 3:
			num = (float)(x0 - GameCanvas.w / 2);
			num2 = (float)y0;
			guistyle.alignment = TextAnchor.UpperCenter;
			break;
		}
		guistyle.normal.textColor = (guistyle.hover.textColor = this.color1);
		g.drawString(st, (int)num, (int)num2, guistyle);
	}

	// Token: 0x060009F2 RID: 2546 RVA: 0x0008EBD4 File Offset: 0x0008CDD4
	public static string[] splitStringSv(string _text, string _searchStr)
	{
		int num = 0;
		int num2 = 0;
		int length = _searchStr.Length;
		int num3 = _text.IndexOf(_searchStr, num2);
		while (num3 != -1)
		{
			num3 = _text.IndexOf(_searchStr, num3 + length);
			num++;
		}
		string[] array = new string[num + 1];
		int num4 = _text.IndexOf(_searchStr);
		int num5 = 0;
		int num6 = 0;
		while (num4 != -1)
		{
			array[num6] = _text.Substring(num5, num4 - num5);
			num5 = num4 + length;
			num4 = _text.IndexOf(_searchStr, num5);
			num6++;
		}
		array[num6] = _text.Substring(num5, _text.Length - num5);
		return array;
	}

	// Token: 0x060009F3 RID: 2547 RVA: 0x0008EC6B File Offset: 0x0008CE6B
	public void reloadImage()
	{
		if (mGraphics.zoomLevel == 1)
		{
			this.imgFont = GameCanvas.loadImage(this.pathImage);
		}
	}

	// Token: 0x060009F4 RID: 2548 RVA: 0x00004887 File Offset: 0x00002A87
	public void freeImage()
	{
	}

	// Token: 0x04001194 RID: 4500
	public static int LEFT = 0;

	// Token: 0x04001195 RID: 4501
	public static int RIGHT = 1;

	// Token: 0x04001196 RID: 4502
	public static int CENTER = 2;

	// Token: 0x04001197 RID: 4503
	public static int RED = 0;

	// Token: 0x04001198 RID: 4504
	public static int YELLOW = 1;

	// Token: 0x04001199 RID: 4505
	public static int GREEN = 2;

	// Token: 0x0400119A RID: 4506
	public static int FATAL = 3;

	// Token: 0x0400119B RID: 4507
	public static int MISS = 4;

	// Token: 0x0400119C RID: 4508
	public static int ORANGE = 5;

	// Token: 0x0400119D RID: 4509
	public static int ADDMONEY = 6;

	// Token: 0x0400119E RID: 4510
	public static int MISS_ME = 7;

	// Token: 0x0400119F RID: 4511
	public static int FATAL_ME = 8;

	// Token: 0x040011A0 RID: 4512
	public static int HP = 9;

	// Token: 0x040011A1 RID: 4513
	public static int MP = 10;

	// Token: 0x040011A2 RID: 4514
	internal int space;

	// Token: 0x040011A3 RID: 4515
	internal Image imgFont;

	// Token: 0x040011A4 RID: 4516
	internal string strFont;

	// Token: 0x040011A5 RID: 4517
	internal int[][] fImages;

	// Token: 0x040011A6 RID: 4518
	public static int yAddFont;

	// Token: 0x040011A7 RID: 4519
	public static int[] colorJava = new int[]
	{
		0, 16711680, 6520319, 16777215, 16755200, 5449989, 21285, 52224, 7386228, 16771788,
		0, 65535, 21285, 16776960, 5592405, 16742263, 33023, 8701737, 15723503, 7999781,
		16768815, 14961237, 4124899, 4671303, 16096312, 16711680, 16755200, 52224, 16777215, 6520319,
		16096312
	};

	// Token: 0x040011A8 RID: 4520
	public static mFont gI;

	// Token: 0x040011A9 RID: 4521
	public static mFont tahoma_7b_red;

	// Token: 0x040011AA RID: 4522
	public static mFont tahoma_7b_blue;

	// Token: 0x040011AB RID: 4523
	public static mFont tahoma_7b_white;

	// Token: 0x040011AC RID: 4524
	public static mFont tahoma_7b_yellow;

	// Token: 0x040011AD RID: 4525
	public static mFont tahoma_7b_yellowSmall;

	// Token: 0x040011AE RID: 4526
	public static mFont tahoma_7b_dark;

	// Token: 0x040011AF RID: 4527
	public static mFont tahoma_7b_green2;

	// Token: 0x040011B0 RID: 4528
	public static mFont tahoma_7b_green;

	// Token: 0x040011B1 RID: 4529
	public static mFont tahoma_7b_focus;

	// Token: 0x040011B2 RID: 4530
	public static mFont tahoma_7b_unfocus;

	// Token: 0x040011B3 RID: 4531
	public static mFont tahoma_7;

	// Token: 0x040011B4 RID: 4532
	public static mFont tahoma_7_blue1;

	// Token: 0x040011B5 RID: 4533
	public static mFont tahoma_7_blue1Small;

	// Token: 0x040011B6 RID: 4534
	public static mFont tahoma_7_green2;

	// Token: 0x040011B7 RID: 4535
	public static mFont tahoma_7_yellow;

	// Token: 0x040011B8 RID: 4536
	public static mFont tahoma_7_grey;

	// Token: 0x040011B9 RID: 4537
	public static mFont tahoma_7_red;

	// Token: 0x040011BA RID: 4538
	public static mFont tahoma_7_blue;

	// Token: 0x040011BB RID: 4539
	public static mFont tahoma_7_green;

	// Token: 0x040011BC RID: 4540
	public static mFont tahoma_7_white;

	// Token: 0x040011BD RID: 4541
	public static mFont tahoma_8b;

	// Token: 0x040011BE RID: 4542
	public static mFont number_yellow;

	// Token: 0x040011BF RID: 4543
	public static mFont number_red;

	// Token: 0x040011C0 RID: 4544
	public static mFont number_green;

	// Token: 0x040011C1 RID: 4545
	public static mFont number_gray;

	// Token: 0x040011C2 RID: 4546
	public static mFont number_orange;

	// Token: 0x040011C3 RID: 4547
	public static mFont bigNumber_red;

	// Token: 0x040011C4 RID: 4548
	public static mFont bigNumber_While;

	// Token: 0x040011C5 RID: 4549
	public static mFont bigNumber_yellow;

	// Token: 0x040011C6 RID: 4550
	public static mFont bigNumber_green;

	// Token: 0x040011C7 RID: 4551
	public static mFont bigNumber_orange;

	// Token: 0x040011C8 RID: 4552
	public static mFont bigNumber_blue;

	// Token: 0x040011C9 RID: 4553
	public static mFont bigNumber_black;

	// Token: 0x040011CA RID: 4554
	public static mFont nameFontRed;

	// Token: 0x040011CB RID: 4555
	public static mFont nameFontYellow;

	// Token: 0x040011CC RID: 4556
	public static mFont nameFontGreen;

	// Token: 0x040011CD RID: 4557
	public static mFont tahoma_7_greySmall;

	// Token: 0x040011CE RID: 4558
	public static mFont tahoma_7b_yellowSmall2;

	// Token: 0x040011CF RID: 4559
	public static mFont tahoma_7b_green2Small;

	// Token: 0x040011D0 RID: 4560
	public static mFont tahoma_7_whiteSmall;

	// Token: 0x040011D1 RID: 4561
	public static mFont tahoma_7b_greenSmall;

	// Token: 0x040011D2 RID: 4562
	public Font myFont;

	// Token: 0x040011D3 RID: 4563
	internal int height;

	// Token: 0x040011D4 RID: 4564
	internal int wO;

	// Token: 0x040011D5 RID: 4565
	public Color color1 = Color.white;

	// Token: 0x040011D6 RID: 4566
	public Color color2 = Color.gray;

	// Token: 0x040011D7 RID: 4567
	public sbyte id;

	// Token: 0x040011D8 RID: 4568
	public int fstyle;

	// Token: 0x040011D9 RID: 4569
	public string st1 = "áàảãạăắằẳẵặâấầẩẫậéèẻẽẹêếềểễệíìỉĩịóòỏõọôốồổỗộơớờởỡợúùủũụưứừửữựýỳỷỹỵđÁÀẢÃẠĂẮẰẲẴẶÂẤẦẨẪẬÉÈẺẼẸÊẾỀỂỄỆÍÌỈĨỊÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢÚÙỦŨỤƯỨỪỬỮỰÝỲỶỸỴĐ";

	// Token: 0x040011DA RID: 4570
	public string st2 = "\u00b8µ¶·¹\u00a8¾»¼½Æ©ÊÇÈÉËÐÌÎÏÑªÕÒÓÔÖÝ×ØÜÞãßáâä«èåæçé¬íêëìîóïñòô\u00adøõö÷ùýúûüþ®\u00b8µ¶·¹¡¾»¼½Æ¢ÊÇÈÉËÐÌÎÏÑ£ÕÒÓÔÖÝ×ØÜÞãßáâä¤èåæçé¥íêëìîóïñòô¦øõö÷ùýúûüþ§";

	// Token: 0x040011DB RID: 4571
	public const string str = " 0123456789+-*='_?.,<>/[]{}!@#$%^&*():aáàảãạâấầẩẫậăắằẳẵặbcdđeéèẻẽẹêếềểễệfghiíìỉĩịjklmnoóòỏõọôốồổỗộơớờởỡợpqrstuúùủũụưứừửữựvxyýỳỷỹỵzwAÁÀẢÃẠĂẰẮẲẴẶÂẤẦẨẪẬBCDĐEÉÈẺẼẸÊẾỀỂỄỆFGHIÍÌỈĨỊJKLMNOÓÒỎÕỌÔỐỒỔỖỘƠỚỜỞỠỢPQRSTUÚÙỦŨỤƯỨỪỬỮỰVXYÝỲỶỸỴZW";

	// Token: 0x040011DC RID: 4572
	internal int yAdd;

	// Token: 0x040011DD RID: 4573
	internal string pathImage;
}
