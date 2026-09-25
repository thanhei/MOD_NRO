using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

// Token: 0x0200009D RID: 157
public class Session_ME2 : ISession
{
	// Token: 0x060008B8 RID: 2232 RVA: 0x00080233 File Offset: 0x0007E433
	public Session_ME2()
	{
		Debug.Log("init Session_ME");
	}

	// Token: 0x060008B9 RID: 2233 RVA: 0x00080DF8 File Offset: 0x0007EFF8
	public void clearSendingMessage()
	{
		Session_ME2.sender.sendingMessage.Clear();
	}

	// Token: 0x060008BA RID: 2234 RVA: 0x00080E09 File Offset: 0x0007F009
	public static Session_ME2 gI()
	{
		if (Session_ME2.instance == null)
		{
			Session_ME2.instance = new Session_ME2();
		}
		return Session_ME2.instance;
	}

	// Token: 0x060008BB RID: 2235 RVA: 0x00080E21 File Offset: 0x0007F021
	public bool isConnected()
	{
		return Session_ME2.connected && Session_ME2.sc != null && Session_ME2.dis != null;
	}

	// Token: 0x060008BC RID: 2236 RVA: 0x00080E3B File Offset: 0x0007F03B
	public void setHandler(IMessageHandler msgHandler)
	{
		Session_ME2.messageHandler = msgHandler;
	}

	// Token: 0x060008BD RID: 2237 RVA: 0x00080E44 File Offset: 0x0007F044
	public void connect(string host, int port)
	{
		if (!Session_ME2.connected && !Session_ME2.connecting && mSystem.currentTimeMillis() >= this.timeWaitConnect)
		{
			this.timeWaitConnect = mSystem.currentTimeMillis() + 50L;
			this.host = host;
			this.port = port;
			Session_ME2.getKeyComplete = false;
			this.close();
			Debug.Log("connecting...!");
			Debug.Log("host: " + host);
			Debug.Log("port: " + port.ToString());
			Session_ME2.initThread = new Thread(new ThreadStart(this.NetworkInit));
			Session_ME2.initThread.Start();
		}
	}

	// Token: 0x060008BE RID: 2238 RVA: 0x00080EEC File Offset: 0x0007F0EC
	internal void NetworkInit()
	{
		Session_ME2.isCancel = false;
		Session_ME2.connecting = true;
		Thread.CurrentThread.Priority = global::System.Threading.ThreadPriority.Highest;
		Session_ME2.connected = true;
		try
		{
			this.doConnect(this.host, this.port);
			Session_ME2.messageHandler.onConnectOK(Session_ME2.isMainSession);
		}
		catch (Exception)
		{
			if (Session_ME2.messageHandler != null)
			{
				this.close();
				Session_ME2.messageHandler.onConnectionFail(Session_ME2.isMainSession);
			}
		}
	}

	// Token: 0x060008BF RID: 2239 RVA: 0x00080F68 File Offset: 0x0007F168
	public void doConnect(string host, int port)
	{
		Session_ME2.sc = new TcpClient();
		Session_ME2.sc.Connect(host, port);
		Session_ME2.dataStream = Session_ME2.sc.GetStream();
		Session_ME2.dis = new BinaryReader(Session_ME2.dataStream, new UTF8Encoding());
		Session_ME2.dos = new BinaryWriter(Session_ME2.dataStream, new UTF8Encoding());
		Session_ME2.sendThread = new Thread(new ThreadStart(Session_ME2.sender.run));
		Session_ME2.sendThread.Start();
		object obj = new Session_ME2.MessageCollector();
		Cout.LogError("new -----");
		Session_ME2.collectorThread = new Thread(new ThreadStart(obj.run));
		Session_ME2.collectorThread.Start();
		Session_ME2.timeConnected = Session_ME2.currentTimeMillis();
		Session_ME2.connecting = false;
		Session_ME2.doSendMessage(new Message(-27));
	}

	// Token: 0x060008C0 RID: 2240 RVA: 0x00081030 File Offset: 0x0007F230
	public void sendMessage(Message message)
	{
		Res.outz("SEND MSG: " + message.command.ToString());
		Session_ME2.sender.AddMessage(message);
	}

	// Token: 0x060008C1 RID: 2241 RVA: 0x00081058 File Offset: 0x0007F258
	internal static void doSendMessage(Message m)
	{
		sbyte[] data = m.getData();
		try
		{
			if (Session_ME2.getKeyComplete)
			{
				sbyte b = Session_ME2.writeKey(m.command);
				Session_ME2.dos.Write(b);
			}
			else
			{
				Session_ME2.dos.Write(m.command);
			}
			if (data != null)
			{
				int num = data.Length;
				if (Session_ME2.getKeyComplete)
				{
					int num2 = (int)Session_ME2.writeKey((sbyte)(num >> 8));
					Session_ME2.dos.Write((sbyte)num2);
					int num3 = (int)Session_ME2.writeKey((sbyte)(num & 255));
					Session_ME2.dos.Write((sbyte)num3);
				}
				else
				{
					Session_ME2.dos.Write((ushort)num);
				}
				if (Session_ME2.getKeyComplete)
				{
					for (int i = 0; i < data.Length; i++)
					{
						sbyte b2 = Session_ME2.writeKey(data[i]);
						Session_ME2.dos.Write(b2);
					}
				}
				Session_ME2.sendByteCount += 5 + data.Length;
			}
			else
			{
				if (Session_ME2.getKeyComplete)
				{
					int num4 = 0;
					int num5 = (int)Session_ME2.writeKey((sbyte)(num4 >> 8));
					Session_ME2.dos.Write((sbyte)num5);
					int num6 = (int)Session_ME2.writeKey((sbyte)(num4 & 255));
					Session_ME2.dos.Write((sbyte)num6);
				}
				else
				{
					Session_ME2.dos.Write(0);
				}
				Session_ME2.sendByteCount += 5;
			}
			Session_ME2.dos.Flush();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.StackTrace);
		}
	}

	// Token: 0x060008C2 RID: 2242 RVA: 0x000811B8 File Offset: 0x0007F3B8
	public static sbyte readKey(sbyte b)
	{
		sbyte[] array = Session_ME2.key;
		sbyte b2 = Session_ME2.curR;
		Session_ME2.curR = b2 + 1;
		sbyte b3 = (sbyte)((array[(int)b2] & 255) ^ ((int)b & 255));
		if ((int)Session_ME2.curR >= Session_ME2.key.Length)
		{
			Session_ME2.curR %= (sbyte)Session_ME2.key.Length;
		}
		return b3;
	}

	// Token: 0x060008C3 RID: 2243 RVA: 0x0008120C File Offset: 0x0007F40C
	public static sbyte writeKey(sbyte b)
	{
		sbyte[] array = Session_ME2.key;
		sbyte b2 = Session_ME2.curW;
		Session_ME2.curW = b2 + 1;
		sbyte b3 = (sbyte)((array[(int)b2] & 255) ^ ((int)b & 255));
		if ((int)Session_ME2.curW >= Session_ME2.key.Length)
		{
			Session_ME2.curW %= (sbyte)Session_ME2.key.Length;
		}
		return b3;
	}

	// Token: 0x060008C4 RID: 2244 RVA: 0x00081260 File Offset: 0x0007F460
	public static void onRecieveMsg(Message msg)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Session_ME2.messageHandler.onMessage(msg);
			return;
		}
		Session_ME2.recieveMsg.addElement(msg);
	}

	// Token: 0x060008C5 RID: 2245 RVA: 0x00081290 File Offset: 0x0007F490
	public static void update()
	{
		while (Session_ME2.recieveMsg.size() > 0)
		{
			Message message = (Message)Session_ME2.recieveMsg.elementAt(0);
			if (Controller.isStopReadMessage)
			{
				break;
			}
			if (message == null)
			{
				Session_ME2.recieveMsg.removeElementAt(0);
				return;
			}
			Session_ME2.messageHandler.onMessage(message);
			Session_ME2.recieveMsg.removeElementAt(0);
		}
	}

	// Token: 0x060008C6 RID: 2246 RVA: 0x000812E9 File Offset: 0x0007F4E9
	public void close()
	{
		Session_ME2.cleanNetwork();
	}

	// Token: 0x060008C7 RID: 2247 RVA: 0x000812F0 File Offset: 0x0007F4F0
	internal static void cleanNetwork()
	{
		Session_ME2.key = null;
		Session_ME2.curR = 0;
		Session_ME2.curW = 0;
		try
		{
			Session_ME2.connected = false;
			Session_ME2.connecting = false;
			if (Session_ME2.sc != null)
			{
				Session_ME2.sc.Close();
				Session_ME2.sc = null;
			}
			if (Session_ME2.dataStream != null)
			{
				Session_ME2.dataStream.Close();
				Session_ME2.dataStream = null;
			}
			if (Session_ME2.dos != null)
			{
				Session_ME2.dos.Close();
				Session_ME2.dos = null;
			}
			if (Session_ME2.dis != null)
			{
				Session_ME2.dis.Close();
				Session_ME2.dis = null;
			}
			Session_ME2.sendThread = null;
			Session_ME2.collectorThread = null;
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060008C8 RID: 2248 RVA: 0x00080884 File Offset: 0x0007EA84
	public static int currentTimeMillis()
	{
		return Environment.TickCount;
	}

	// Token: 0x060008C9 RID: 2249 RVA: 0x0004AAED File Offset: 0x00048CED
	public static byte convertSbyteToByte(sbyte var)
	{
		if (var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x060008CA RID: 2250 RVA: 0x00081398 File Offset: 0x0007F598
	public static byte[] convertSbyteToByte(sbyte[] var)
	{
		byte[] array = new byte[var.Length];
		for (int i = 0; i < var.Length; i++)
		{
			if (var[i] > 0)
			{
				array[i] = (byte)var[i];
			}
			else
			{
				array[i] = (byte)((int)var[i] + 256);
			}
		}
		return array;
	}

	// Token: 0x04000F54 RID: 3924
	protected static Session_ME2 instance = new Session_ME2();

	// Token: 0x04000F55 RID: 3925
	internal static NetworkStream dataStream;

	// Token: 0x04000F56 RID: 3926
	internal static BinaryReader dis;

	// Token: 0x04000F57 RID: 3927
	internal static BinaryWriter dos;

	// Token: 0x04000F58 RID: 3928
	public static IMessageHandler messageHandler;

	// Token: 0x04000F59 RID: 3929
	public static bool isMainSession = true;

	// Token: 0x04000F5A RID: 3930
	internal static TcpClient sc;

	// Token: 0x04000F5B RID: 3931
	public static bool connected;

	// Token: 0x04000F5C RID: 3932
	public static bool connecting;

	// Token: 0x04000F5D RID: 3933
	internal static Session_ME2.Sender sender = new Session_ME2.Sender();

	// Token: 0x04000F5E RID: 3934
	public static Thread initThread;

	// Token: 0x04000F5F RID: 3935
	public static Thread collectorThread;

	// Token: 0x04000F60 RID: 3936
	public static Thread sendThread;

	// Token: 0x04000F61 RID: 3937
	public static int sendByteCount;

	// Token: 0x04000F62 RID: 3938
	public static int recvByteCount;

	// Token: 0x04000F63 RID: 3939
	internal static bool getKeyComplete;

	// Token: 0x04000F64 RID: 3940
	public static sbyte[] key = null;

	// Token: 0x04000F65 RID: 3941
	internal static sbyte curR;

	// Token: 0x04000F66 RID: 3942
	internal static sbyte curW;

	// Token: 0x04000F67 RID: 3943
	internal static int timeConnected;

	// Token: 0x04000F68 RID: 3944
	internal long lastTimeConn;

	// Token: 0x04000F69 RID: 3945
	public static string strRecvByteCount = string.Empty;

	// Token: 0x04000F6A RID: 3946
	public static bool isCancel;

	// Token: 0x04000F6B RID: 3947
	internal string host;

	// Token: 0x04000F6C RID: 3948
	internal int port;

	// Token: 0x04000F6D RID: 3949
	internal long timeWaitConnect;

	// Token: 0x04000F6E RID: 3950
	public static MyVector recieveMsg = new MyVector();

	// Token: 0x0200009E RID: 158
	public class Sender
	{
		// Token: 0x060008CC RID: 2252 RVA: 0x0008140F File Offset: 0x0007F60F
		public Sender()
		{
			this.sendingMessage = new List<Message>();
		}

		// Token: 0x060008CD RID: 2253 RVA: 0x00081422 File Offset: 0x0007F622
		public void AddMessage(Message message)
		{
			this.sendingMessage.Add(message);
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x00081430 File Offset: 0x0007F630
		public void run()
		{
			while (Session_ME2.connected)
			{
				try
				{
					if (Session_ME2.getKeyComplete)
					{
						while (this.sendingMessage.Count > 0)
						{
							Session_ME2.doSendMessage(this.sendingMessage[0]);
							this.sendingMessage.RemoveAt(0);
						}
					}
					try
					{
						Thread.Sleep(5);
					}
					catch (Exception ex)
					{
						Cout.LogError(ex.ToString());
					}
				}
				catch (Exception)
				{
					Res.outz("error send message! ");
				}
			}
		}

		// Token: 0x04000F6F RID: 3951
		public List<Message> sendingMessage;
	}

	// Token: 0x0200009F RID: 159
	private class MessageCollector
	{
		// Token: 0x060008CF RID: 2255 RVA: 0x000814BC File Offset: 0x0007F6BC
		public void run()
		{
			try
			{
				while (Session_ME2.connected)
				{
					Message message = this.readMessage();
					if (message == null)
					{
						break;
					}
					try
					{
						if (message.command == -27)
						{
							this.getKey(message);
						}
						else
						{
							Session_ME2.onRecieveMsg(message);
						}
					}
					catch (Exception)
					{
						Cout.println("LOI NHAN  MESS THU 1");
					}
					try
					{
						Thread.Sleep(5);
					}
					catch (Exception)
					{
						Cout.println("LOI NHAN  MESS THU 2");
					}
				}
			}
			catch (Exception ex)
			{
				Debug.Log("error read message!");
				Debug.Log(ex.Message.ToString());
			}
			if (!Session_ME2.connected)
			{
				return;
			}
			if (Session_ME2.messageHandler != null)
			{
				if (Session_ME2.currentTimeMillis() - Session_ME2.timeConnected > 500)
				{
					Session_ME2.messageHandler.onDisconnected(Session_ME2.isMainSession);
				}
				else
				{
					Session_ME2.messageHandler.onConnectionFail(Session_ME2.isMainSession);
				}
			}
			if (Session_ME2.sc != null)
			{
				Session_ME2.cleanNetwork();
			}
		}

		// Token: 0x060008D0 RID: 2256 RVA: 0x000815B0 File Offset: 0x0007F7B0
		internal void getKey(Message message)
		{
			try
			{
				sbyte b = message.reader().readSByte();
				Session_ME2.key = new sbyte[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					Session_ME2.key[i] = message.reader().readSByte();
				}
				for (int j = 0; j < Session_ME2.key.Length - 1; j++)
				{
					sbyte[] key = Session_ME2.key;
					int num = j + 1;
					key[num] ^= Session_ME2.key[j];
				}
				Session_ME2.getKeyComplete = true;
				GameMidlet.IP2 = message.reader().readUTF();
				GameMidlet.PORT2 = message.reader().readInt();
				GameMidlet.isConnect2 = message.reader().readByte() != 0;
				if (Session_ME2.isMainSession && GameMidlet.isConnect2)
				{
					GameCanvas.connect2();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00081684 File Offset: 0x0007F884
		internal Message readMessage2(sbyte cmd)
		{
			int num = (int)Session_ME2.readKey(Session_ME2.dis.ReadSByte()) + 128;
			int num2 = (int)Session_ME2.readKey(Session_ME2.dis.ReadSByte()) + 128;
			int num3 = (((int)Session_ME2.readKey(Session_ME2.dis.ReadSByte()) + 128) * 256 + num2) * 256 + num;
			Cout.LogError("SIZE = " + num3.ToString());
			sbyte[] array = new sbyte[num3];
			Buffer.BlockCopy(Session_ME2.dis.ReadBytes(num3), 0, array, 0, num3);
			Session_ME2.recvByteCount += 5 + num3;
			int num4 = Session_ME2.recvByteCount + Session_ME2.sendByteCount;
			Session_ME2.strRecvByteCount = (num4 / 1024).ToString() + "." + (num4 % 1024 / 102).ToString() + "Kb";
			if (Session_ME2.getKeyComplete)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Session_ME2.readKey(array[i]);
				}
			}
			return new Message(cmd, array);
		}

		// Token: 0x060008D2 RID: 2258 RVA: 0x00081798 File Offset: 0x0007F998
		internal Message readMessage()
		{
			try
			{
				sbyte b = Session_ME2.dis.ReadSByte();
				if (Session_ME2.getKeyComplete)
				{
					b = Session_ME2.readKey(b);
				}
				if (b == -32 || b == -66 || b == 11 || b == -67 || b == -74 || b == -87)
				{
					return this.readMessage2(b);
				}
				int num;
				if (Session_ME2.getKeyComplete)
				{
					sbyte b2 = Session_ME2.dis.ReadSByte();
					sbyte b3 = Session_ME2.dis.ReadSByte();
					num = (((int)Session_ME2.readKey(b2) & 255) << 8) | ((int)Session_ME2.readKey(b3) & 255);
				}
				else
				{
					num = ((int)Session_ME2.dis.ReadSByte() & 65280) | ((int)Session_ME2.dis.ReadSByte() & 255);
				}
				sbyte[] array = new sbyte[num];
				Buffer.BlockCopy(Session_ME2.dis.ReadBytes(num), 0, array, 0, num);
				Session_ME2.recvByteCount += 5 + num;
				int num2 = Session_ME2.recvByteCount + Session_ME2.sendByteCount;
				Session_ME2.strRecvByteCount = (num2 / 1024).ToString() + "." + (num2 % 1024 / 102).ToString() + "Kb";
				if (Session_ME2.getKeyComplete)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Session_ME2.readKey(array[i]);
					}
				}
				return new Message(b, array);
			}
			catch (Exception ex)
			{
				Debug.Log(ex.StackTrace.ToString());
			}
			return null;
		}
	}
}
