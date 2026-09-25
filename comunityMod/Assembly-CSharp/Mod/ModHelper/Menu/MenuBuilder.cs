using System;
using System.Collections.Generic;
using System.Linq;

namespace Mod.ModHelper.Menu
{
	// Token: 0x02000144 RID: 324
	public class MenuBuilder : IActionListener
	{
		// Token: 0x06000FC1 RID: 4033 RVA: 0x000B1248 File Offset: 0x000AF448
		public static void example1OpenMenu()
		{
			new MenuBuilder().addItem("test", new MenuAction(delegate(int selected, string caption, string[] captions)
			{
				GameScr.info1.addInfo(string.Format("selected {0}, {1}", selected, caption), 0);
			})).addItem("Test 1", new MenuAction(delegate
			{
				GameScr.info1.addInfo("meow meow", 0);
			})).addItem("Test 2", new MenuAction(delegate
			{
				GameScr.info1.addInfo("gau gau", 0);
			}))
				.start();
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000B12E9 File Offset: 0x000AF4E9
		public MenuBuilder setChatPopup(string chatPopup)
		{
			this.chatPopup = chatPopup;
			return this;
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000B12F3 File Offset: 0x000AF4F3
		public MenuBuilder setPos(int x, int y)
		{
			this.isPosDefault = false;
			this.x = x;
			this.y = y;
			return this;
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000B130B File Offset: 0x000AF50B
		public MenuBuilder addItem(string caption, MenuAction action)
		{
			this.menuItems.Add(new MenuItem(caption, action));
			return this;
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000B1320 File Offset: 0x000AF520
		public MenuBuilder addItem(bool ifCondition, string caption, MenuAction action)
		{
			if (ifCondition)
			{
				this.menuItems.Add(new MenuItem(caption, action));
			}
			return this;
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000B1338 File Offset: 0x000AF538
		public MenuBuilder map<T>(MyVector myVector, Func<T, MenuItem> func)
		{
			for (int i = 0; i < myVector.size(); i++)
			{
				T t = (T)((object)myVector.elementAt(i));
				this.menuItems.Add(func(t));
			}
			return this;
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x000B1378 File Offset: 0x000AF578
		public MenuBuilder map<T>(IEnumerable<T> values, Func<T, MenuItem> func)
		{
			foreach (T t in values)
			{
				this.menuItems.Add(func(t));
			}
			return this;
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x000B13CC File Offset: 0x000AF5CC
		public void start()
		{
			MyVector myVectorStartMenu = this.getMyVectorStartMenu();
			if (myVectorStartMenu.size() > 0)
			{
				if (this.isPosDefault)
				{
					GameCanvas.menu.startAt(myVectorStartMenu, 3);
				}
				else
				{
					GameCanvas.menu.startAt(myVectorStartMenu, this.x, this.y);
				}
			}
			if (!string.IsNullOrEmpty(this.chatPopup))
			{
				ChatPopup.addChatPopup(this.chatPopup, 100000, new Npc(5, 0, -100, 100, 5, (int)Utils.ID_NPC_MOD_FACE));
			}
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x000B1448 File Offset: 0x000AF648
		private MyVector getMyVectorStartMenu()
		{
			IEnumerable<string> enumerable = this.menuItems.Select<MenuItem, string>((MenuItem menuItem) => menuItem.caption);
			MyVector myVector = new MyVector();
			for (int i = 0; i < this.menuItems.Count; i++)
			{
				MenuItem menuItem2 = this.menuItems[i];
				myVector.addElement(new Command(menuItem2.caption, this, 1, new
				{
					selected = i,
					action = menuItem2.action,
					captions = enumerable.ToArray<string>()
				}));
			}
			return myVector;
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x000B14D0 File Offset: 0x000AF6D0
		public void perform(int idAction, object p)
		{
			if (idAction != 0 && idAction == 1)
			{
				MenuBuilder.onMenuSelected(p);
			}
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x000B14EC File Offset: 0x000AF6EC
		private static void onMenuSelected(object p)
		{
			int valueProperty = p.getValueProperty<int>("selected");
			MenuAction valueProperty2 = p.getValueProperty<MenuAction>("action");
			string[] valueProperty3 = p.getValueProperty<string[]>("captions");
			string text = valueProperty3[valueProperty];
			if (global::Char.chatPopup != null && global::Char.chatPopup.c.avatar == (int)Utils.ID_NPC_MOD_FACE)
			{
				global::Char.chatPopup = null;
			}
			valueProperty2.Invoke(valueProperty, text, valueProperty3);
		}

		// Token: 0x04001775 RID: 6005
		private string chatPopup;

		// Token: 0x04001776 RID: 6006
		private bool isPosDefault = true;

		// Token: 0x04001777 RID: 6007
		public int x;

		// Token: 0x04001778 RID: 6008
		public int y;

		// Token: 0x04001779 RID: 6009
		public List<MenuItem> menuItems = new List<MenuItem>();
	}
}
