using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

// Token: 0x0200009A RID: 154
public class Session_ME : ISession
{
	// Token: 0x0600089B RID: 2203 RVA: 0x00080233 File Offset: 0x0007E433
	public Session_ME()
	{
		Debug.Log("init Session_ME");
	}

	// Token: 0x0600089C RID: 2204 RVA: 0x00080245 File Offset: 0x0007E445
	public void clearSendingMessage()
	{
		Session_ME.sender.sendingMessage.Clear();
	}

	// Token: 0x0600089D RID: 2205 RVA: 0x00080256 File Offset: 0x0007E456
	public static Session_ME gI()
	{
		if (Session_ME.instance == null)
		{
			Session_ME.instance = new Session_ME();
		}
		return Session_ME.instance;
	}

	// Token: 0x0600089E RID: 2206 RVA: 0x0008026E File Offset: 0x0007E46E
	public bool isConnected()
	{
		return Session_ME.connected && Session_ME.sc != null && Session_ME.dis != null;
	}

	// Token: 0x0600089F RID: 2207 RVA: 0x00080288 File Offset: 0x0007E488
	public void setHandler(IMessageHandler msgHandler)
	{
		Session_ME.messageHandler = msgHandler;
	}

	// Token: 0x060008A0 RID: 2208 RVA: 0x00080290 File Offset: 0x0007E490
	public void connect(string host, int port)
	{
		if (!Session_ME.connected && !Session_ME.connecting && mSystem.currentTimeMillis() >= this.timeWaitConnect)
		{
			this.timeWaitConnect = mSystem.currentTimeMillis() + 50L;
			if (Session_ME.isMainSession)
			{
				ServerListScreen.testConnect = -1;
			}
			this.host = host;
			this.port = port;
			Session_ME.getKeyComplete = false;
			this.close();
			Debug.Log("connecting...!");
			Debug.Log("host: " + host);
			Debug.Log("port: " + port.ToString());
			Session_ME.initThread = new Thread(new ThreadStart(this.NetworkInit));
			Session_ME.initThread.Start();
		}
	}

	// Token: 0x060008A1 RID: 2209 RVA: 0x00080348 File Offset: 0x0007E548
	internal void NetworkInit()
	{
		Session_ME.isCancel = false;
		Session_ME.connecting = true;
		Thread.CurrentThread.Priority = global::System.Threading.ThreadPriority.Highest;
		Session_ME.connected = true;
		try
		{
			this.doConnect(this.host, this.port);
			Session_ME.messageHandler.onConnectOK(Session_ME.isMainSession);
		}
		catch (Exception)
		{
			if (Session_ME.messageHandler != null)
			{
				this.close();
				Session_ME.messageHandler.onConnectionFail(Session_ME.isMainSession);
			}
		}
	}

	// Token: 0x060008A2 RID: 2210 RVA: 0x000803C4 File Offset: 0x0007E5C4
	public void doConnect(string host, int port)
	{
		Session_ME.sc = new TcpClient();
		Session_ME.sc.Connect(host, port);
		Session_ME.dataStream = Session_ME.sc.GetStream();
		Session_ME.dis = new BinaryReader(Session_ME.dataStream, new UTF8Encoding());
		Session_ME.dos = new BinaryWriter(Session_ME.dataStream, new UTF8Encoding());
		Session_ME.sendThread = new Thread(new ThreadStart(Session_ME.sender.run));
		Session_ME.sendThread.Start();
		object obj = new Session_ME.MessageCollector();
		Cout.LogError("new -----");
		Session_ME.collectorThread = new Thread(new ThreadStart(obj.run));
		Session_ME.collectorThread.Start();
		Session_ME.timeConnected = Session_ME.currentTimeMillis();
		Session_ME.connecting = false;
		Session_ME.doSendMessage(new Message(-27));
		Session_ME.key = null;
	}

	// Token: 0x060008A3 RID: 2211 RVA: 0x00080492 File Offset: 0x0007E692
	public void sendMessage(Message message)
	{
		Session_ME.count++;
		Res.outz("SEND MSG: " + message.command.ToString());
		Session_ME.sender.AddMessage(message);
	}

	// Token: 0x060008A4 RID: 2212 RVA: 0x000804C8 File Offset: 0x0007E6C8
	internal static void doSendMessage(Message m)
	{
		sbyte[] data = m.getData();
		try
		{
			if (Session_ME.getKeyComplete)
			{
				sbyte b = Session_ME.writeKey(m.command);
				Session_ME.dos.Write(b);
			}
			else
			{
				Session_ME.dos.Write(m.command);
			}
			if (data != null)
			{
				int num = data.Length;
				if (Session_ME.getKeyComplete)
				{
					int num2 = (int)Session_ME.writeKey((sbyte)(num >> 8));
					Session_ME.dos.Write((sbyte)num2);
					int num3 = (int)Session_ME.writeKey((sbyte)(num & 255));
					Session_ME.dos.Write((sbyte)num3);
				}
				else
				{
					Session_ME.dos.Write((ushort)num);
				}
				if (Session_ME.getKeyComplete)
				{
					for (int i = 0; i < data.Length; i++)
					{
						sbyte b2 = Session_ME.writeKey(data[i]);
						Session_ME.dos.Write(b2);
					}
				}
				Session_ME.sendByteCount += 5 + data.Length;
			}
			else
			{
				if (Session_ME.getKeyComplete)
				{
					int num4 = 0;
					int num5 = (int)Session_ME.writeKey((sbyte)(num4 >> 8));
					Session_ME.dos.Write((sbyte)num5);
					int num6 = (int)Session_ME.writeKey((sbyte)(num4 & 255));
					Session_ME.dos.Write((sbyte)num6);
				}
				else
				{
					Session_ME.dos.Write(0);
				}
				Session_ME.sendByteCount += 5;
			}
			Session_ME.dos.Flush();
		}
		catch (Exception ex)
		{
			Debug.Log(ex.StackTrace);
			Session_ME.dos.Flush();
		}
	}

	// Token: 0x060008A5 RID: 2213 RVA: 0x00080634 File Offset: 0x0007E834
	public static sbyte readKey(sbyte b)
	{
		sbyte[] array = Session_ME.key;
		sbyte b2 = Session_ME.curR;
		Session_ME.curR = b2 + 1;
		sbyte b3 = (sbyte)((array[(int)b2] & 255) ^ ((int)b & 255));
		if ((int)Session_ME.curR >= Session_ME.key.Length)
		{
			Session_ME.curR %= (sbyte)Session_ME.key.Length;
		}
		return b3;
	}

	// Token: 0x060008A6 RID: 2214 RVA: 0x00080688 File Offset: 0x0007E888
	public static sbyte writeKey(sbyte b)
	{
		sbyte[] array = Session_ME.key;
		sbyte b2 = Session_ME.curW;
		Session_ME.curW = b2 + 1;
		sbyte b3 = (sbyte)((array[(int)b2] & 255) ^ ((int)b & 255));
		if ((int)Session_ME.curW >= Session_ME.key.Length)
		{
			Session_ME.curW %= (sbyte)Session_ME.key.Length;
		}
		return b3;
	}

	// Token: 0x060008A7 RID: 2215 RVA: 0x000806DC File Offset: 0x0007E8DC
	public static void onRecieveMsg(Message msg)
	{
		if (Thread.CurrentThread.Name == Main.mainThreadName)
		{
			Session_ME.messageHandler.onMessage(msg);
			return;
		}
		Session_ME.recieveMsg.addElement(msg);
	}

	// Token: 0x060008A8 RID: 2216 RVA: 0x0008070C File Offset: 0x0007E90C
	public static void update()
	{
		while (Session_ME.recieveMsg.size() > 0)
		{
			Message message = (Message)Session_ME.recieveMsg.elementAt(0);
			if (Controller.isStopReadMessage)
			{
				break;
			}
			if (message == null)
			{
				Session_ME.recieveMsg.removeElementAt(0);
				return;
			}
			Session_ME.messageHandler.onMessage(message);
			Session_ME.recieveMsg.removeElementAt(0);
		}
	}

	// Token: 0x060008A9 RID: 2217 RVA: 0x00080765 File Offset: 0x0007E965
	public void close()
	{
		Session_ME.cleanNetwork();
	}

	// Token: 0x060008AA RID: 2218 RVA: 0x0008076C File Offset: 0x0007E96C
	internal static void cleanNetwork()
	{
		Session_ME.key = null;
		Session_ME.curR = 0;
		Session_ME.curW = 0;
		try
		{
			Session_ME.connected = false;
			Session_ME.connecting = false;
			if (Session_ME.sc != null)
			{
				Session_ME.sc.Close();
				Session_ME.sc = null;
			}
			if (Session_ME.dataStream != null)
			{
				Session_ME.dataStream.Close();
				Session_ME.dataStream = null;
			}
			if (Session_ME.dos != null)
			{
				Session_ME.dos.Close();
				Session_ME.dos = null;
			}
			if (Session_ME.dis != null)
			{
				Session_ME.dis.Close();
				Session_ME.dis = null;
			}
			if (Thread.CurrentThread.Name == Main.mainThreadName)
			{
				if (Session_ME.sendThread != null)
				{
					Session_ME.sendThread.Abort();
				}
				Session_ME.sendThread = null;
				if (Session_ME.initThread != null)
				{
					Session_ME.initThread.Abort();
				}
				Session_ME.initThread = null;
				if (Session_ME.collectorThread != null)
				{
					Session_ME.collectorThread.Abort();
				}
				Session_ME.collectorThread = null;
			}
			else
			{
				Session_ME.sendThread = null;
				Session_ME.initThread = null;
				Session_ME.collectorThread = null;
			}
			if (Session_ME.isMainSession)
			{
				ServerListScreen.testConnect = 0;
			}
		}
		catch (Exception)
		{
		}
	}

	// Token: 0x060008AB RID: 2219 RVA: 0x00080884 File Offset: 0x0007EA84
	public static int currentTimeMillis()
	{
		return Environment.TickCount;
	}

	// Token: 0x060008AC RID: 2220 RVA: 0x0004AAED File Offset: 0x00048CED
	public static byte convertSbyteToByte(sbyte var)
	{
		if (var > 0)
		{
			return (byte)var;
		}
		return (byte)((int)var + 256);
	}

	// Token: 0x060008AD RID: 2221 RVA: 0x0008088C File Offset: 0x0007EA8C
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

	// Token: 0x060008AE RID: 2222 RVA: 0x00039302 File Offset: 0x00037502
	public bool isCompareIPConnect()
	{
		return true;
	}

	// Token: 0x04000F37 RID: 3895
	protected static Session_ME instance = new Session_ME();

	// Token: 0x04000F38 RID: 3896
	internal static NetworkStream dataStream;

	// Token: 0x04000F39 RID: 3897
	internal static BinaryReader dis;

	// Token: 0x04000F3A RID: 3898
	internal static BinaryWriter dos;

	// Token: 0x04000F3B RID: 3899
	public static IMessageHandler messageHandler;

	// Token: 0x04000F3C RID: 3900
	public static bool isMainSession = true;

	// Token: 0x04000F3D RID: 3901
	internal static TcpClient sc;

	// Token: 0x04000F3E RID: 3902
	public static bool connected;

	// Token: 0x04000F3F RID: 3903
	public static bool connecting;

	// Token: 0x04000F40 RID: 3904
	internal static Session_ME.Sender sender = new Session_ME.Sender();

	// Token: 0x04000F41 RID: 3905
	public static Thread initThread;

	// Token: 0x04000F42 RID: 3906
	public static Thread collectorThread;

	// Token: 0x04000F43 RID: 3907
	public static Thread sendThread;

	// Token: 0x04000F44 RID: 3908
	public static int sendByteCount;

	// Token: 0x04000F45 RID: 3909
	public static int recvByteCount;

	// Token: 0x04000F46 RID: 3910
	internal static bool getKeyComplete;

	// Token: 0x04000F47 RID: 3911
	public static sbyte[] key = null;

	// Token: 0x04000F48 RID: 3912
	internal static sbyte curR;

	// Token: 0x04000F49 RID: 3913
	internal static sbyte curW;

	// Token: 0x04000F4A RID: 3914
	internal static int timeConnected;

	// Token: 0x04000F4B RID: 3915
	internal long lastTimeConn;

	// Token: 0x04000F4C RID: 3916
	public static string strRecvByteCount = string.Empty;

	// Token: 0x04000F4D RID: 3917
	public static bool isCancel;

	// Token: 0x04000F4E RID: 3918
	internal string host;

	// Token: 0x04000F4F RID: 3919
	internal int port;

	// Token: 0x04000F50 RID: 3920
	internal long timeWaitConnect;

	// Token: 0x04000F51 RID: 3921
	public static int count;

	// Token: 0x04000F52 RID: 3922
	public static MyVector recieveMsg = new MyVector();

	// Token: 0x0200009B RID: 155
	public class Sender
	{
		// Token: 0x060008B0 RID: 2224 RVA: 0x00080903 File Offset: 0x0007EB03
		public Sender()
		{
			this.sendingMessage = new List<Message>();
		}

		// Token: 0x060008B1 RID: 2225 RVA: 0x00080916 File Offset: 0x0007EB16
		public void AddMessage(Message message)
		{
			this.sendingMessage.Add(message);
		}

		// Token: 0x060008B2 RID: 2226 RVA: 0x00080924 File Offset: 0x0007EB24
		public void run()
		{
			while (Session_ME.connected)
			{
				try
				{
					if (Session_ME.getKeyComplete)
					{
						while (this.sendingMessage.Count > 0)
						{
							Session_ME.doSendMessage(this.sendingMessage[0]);
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

		// Token: 0x04000F53 RID: 3923
		public List<Message> sendingMessage;
	}

	// Token: 0x0200009C RID: 156
	private class MessageCollector
	{
		// Token: 0x060008B3 RID: 2227 RVA: 0x000809B0 File Offset: 0x0007EBB0
		public void run()
		{
			try
			{
				while (Session_ME.connected)
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
							Session_ME.onRecieveMsg(message);
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
			if (!Session_ME.connected)
			{
				return;
			}
			if (Session_ME.messageHandler != null)
			{
				if (Session_ME.currentTimeMillis() - Session_ME.timeConnected > 500)
				{
					Session_ME.messageHandler.onDisconnected(Session_ME.isMainSession);
				}
				else
				{
					Session_ME.messageHandler.onConnectionFail(Session_ME.isMainSession);
				}
			}
			if (Session_ME.sc != null)
			{
				Session_ME.cleanNetwork();
			}
		}

		// Token: 0x060008B4 RID: 2228 RVA: 0x00080AA4 File Offset: 0x0007ECA4
		internal void getKey(Message message)
		{
			try
			{
				sbyte b = message.reader().readSByte();
				Session_ME.key = new sbyte[(int)b];
				for (int i = 0; i < (int)b; i++)
				{
					Session_ME.key[i] = message.reader().readSByte();
				}
				for (int j = 0; j < Session_ME.key.Length - 1; j++)
				{
					sbyte[] key = Session_ME.key;
					int num = j + 1;
					key[num] ^= Session_ME.key[j];
				}
				Session_ME.getKeyComplete = true;
				GameMidlet.IP2 = message.reader().readUTF();
				GameMidlet.PORT2 = message.reader().readInt();
				GameMidlet.isConnect2 = message.reader().readByte() != 0;
				if (Session_ME.isMainSession && GameMidlet.isConnect2)
				{
					GameCanvas.connect2();
				}
			}
			catch (Exception)
			{
			}
		}

		// Token: 0x060008B5 RID: 2229 RVA: 0x00080B78 File Offset: 0x0007ED78
		internal Message readMessage2(sbyte cmd)
		{
			int num = (int)Session_ME.readKey(Session_ME.dis.ReadSByte()) + 128;
			int num2 = (int)Session_ME.readKey(Session_ME.dis.ReadSByte()) + 128;
			int num3 = (((int)Session_ME.readKey(Session_ME.dis.ReadSByte()) + 128) * 256 + num2) * 256 + num;
			sbyte[] array = new sbyte[num3];
			Buffer.BlockCopy(Session_ME.dis.ReadBytes(num3), 0, array, 0, num3);
			Session_ME.recvByteCount += 5 + num3;
			int num4 = Session_ME.recvByteCount + Session_ME.sendByteCount;
			Session_ME.strRecvByteCount = (num4 / 1024).ToString() + "." + (num4 % 1024 / 102).ToString() + "Kb";
			if (Session_ME.getKeyComplete)
			{
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = Session_ME.readKey(array[i]);
				}
			}
			return new Message(cmd, array);
		}

		// Token: 0x060008B6 RID: 2230 RVA: 0x00080C74 File Offset: 0x0007EE74
		internal Message readMessage()
		{
			try
			{
				sbyte b = Session_ME.dis.ReadSByte();
				if (Session_ME.getKeyComplete)
				{
					b = Session_ME.readKey(b);
				}
				if (b == -32 || b == -66 || b == 11 || b == -67 || b == -74 || b == -87 || b == 66)
				{
					return this.readMessage2(b);
				}
				int num;
				if (Session_ME.getKeyComplete)
				{
					sbyte b2 = Session_ME.dis.ReadSByte();
					sbyte b3 = Session_ME.dis.ReadSByte();
					num = (((int)Session_ME.readKey(b2) & 255) << 8) | ((int)Session_ME.readKey(b3) & 255);
				}
				else
				{
					num = ((int)Session_ME.dis.ReadSByte() & 65280) | ((int)Session_ME.dis.ReadSByte() & 255);
				}
				sbyte[] array = new sbyte[num];
				Buffer.BlockCopy(Session_ME.dis.ReadBytes(num), 0, array, 0, num);
				Session_ME.recvByteCount += 5 + num;
				int num2 = Session_ME.recvByteCount + Session_ME.sendByteCount;
				Session_ME.strRecvByteCount = (num2 / 1024).ToString() + "." + (num2 % 1024 / 102).ToString() + "Kb";
				if (Session_ME.getKeyComplete)
				{
					for (int i = 0; i < array.Length; i++)
					{
						array[i] = Session_ME.readKey(array[i]);
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
