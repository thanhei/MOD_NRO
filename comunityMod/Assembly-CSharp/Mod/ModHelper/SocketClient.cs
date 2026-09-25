using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Mod.ModHelper
{
	// Token: 0x0200013A RID: 314
	public class SocketClient : ThreadAction<SocketClient>
	{
		// Token: 0x06000F93 RID: 3987 RVA: 0x000B0AC0 File Offset: 0x000AECC0
		public void initSender()
		{
			this.loadPort();
			if (this.port == -1)
			{
				return;
			}
			try
			{
				IPAddress ipaddress = Dns.GetHostEntry(Dns.GetHostName()).AddressList[0];
				IPEndPoint ipendPoint = new IPEndPoint(ipaddress, this.port);
				this.sender = new Socket(ipaddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
				this.sender.Connect(ipendPoint);
				this.sendMessage(new
				{
					action = "connected",
					id = Process.GetCurrentProcess().Id
				});
				this.sendMessage(new
				{
					action = "setStatus",
					status = Utils.status
				});
				byte[] array = new byte[1024];
				int num = this.sender.Receive(array);
				JObject jobject = JObject.Parse(Encoding.ASCII.GetString(array, 0, num));
				Utils.username = (string)jobject["username"];
				Utils.password = (string)jobject["password"];
				Utils.server = (JObject)jobject["server"];
				Utils.sizeData = (JObject)jobject["sizeData"];
				this.isConnected = true;
				base.performAction();
			}
			catch (Exception ex)
			{
				this.writeLog(ex.ToString());
			}
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000B0BF8 File Offset: 0x000AEDF8
		private void onMessage(JObject msg)
		{
			string text = (string)msg["action"];
			if (text == "test")
			{
				GameScr.info1.addInfo((string)msg["text"], 0);
				return;
			}
			if (text == "chat")
			{
				Service.gI().chat((string)msg["text"]);
				return;
			}
			if (text == "keyPress")
			{
				GameMidlet.gameCanvas.keyPressedz((int)msg["keyCode"]);
				GameEvents.OnKeyPressed((int)msg["keyCode"], true);
				return;
			}
			if (!(text == "keyRelease"))
			{
				if (!(text == "syncKeyPressed"))
				{
					if (!(text == "syncKeyReleased"))
					{
						this.writeLog(">> Lost action " + text + " \n");
					}
					else if (Utils.channelSyncKey == (int)msg["channelSyncKey"])
					{
						GameMidlet.gameCanvas.keyReleasedz((int)msg["keyCode"]);
						GameEvents.OnKeyReleased((int)msg["keyCode"], true);
						return;
					}
				}
				else if (Utils.channelSyncKey == (int)msg["channelSyncKey"])
				{
					GameMidlet.gameCanvas.keyPressedz((int)msg["keyCode"]);
					GameEvents.OnKeyPressed((int)msg["keyCode"], true);
					return;
				}
				return;
			}
			GameMidlet.gameCanvas.keyReleasedz((int)msg["keyCode"]);
			GameEvents.OnKeyReleased((int)msg["keyCode"], true);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000B0DB8 File Offset: 0x000AEFB8
		public void sendMessage(object obj)
		{
			string text = JsonConvert.SerializeObject(obj);
			byte[] bytes = Encoding.ASCII.GetBytes(text);
			try
			{
				this.sender.Send(bytes);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000B0DFC File Offset: 0x000AEFFC
		protected override void action()
		{
			if (!this.isConnected)
			{
				return;
			}
			byte[] array = new byte[1024];
			for (;;)
			{
				JObject msg;
				try
				{
					int num = this.sender.Receive(array);
					string @string = Encoding.ASCII.GetString(array, 0, num);
					msg = JObject.Parse(@string);
				}
				catch (SocketException)
				{
					GameCanvas.startOKDlg("Mất kết nối với QLTK");
					break;
				}
				catch (ObjectDisposedException)
				{
					GameCanvas.startOKDlg("Mất kết nối với QLTK");
					break;
				}
				catch (Exception ex)
				{
					this.writeLog(ex.ToString());
					continue;
				}
				MainThreadDispatcher.Dispatch(delegate
				{
					this.onMessage(msg);
				});
			}
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000B0EBC File Offset: 0x000AF0BC
		private void loadPort()
		{
			string[] commandLineArgs = Environment.GetCommandLineArgs();
			int num = Array.IndexOf<string>(commandLineArgs, "-port") + 1;
			try
			{
				this.port = int.Parse(commandLineArgs[num]);
			}
			catch (Exception ex)
			{
				this.writeLog(ex.ToString());
			}
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000B0F0C File Offset: 0x000AF10C
		public void close()
		{
			Socket socket = this.sender;
			if (socket != null && socket.Connected)
			{
				this.sendMessage(new
				{
					action = "close-socket"
				});
				this.sender.Shutdown(SocketShutdown.Both);
				this.sender.Close();
			}
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000B0F4C File Offset: 0x000AF14C
		private void writeLog(string log)
		{
			try
			{
				File.AppendAllText(SocketClient.pathLogSocket, log + "\n");
			}
			catch
			{
			}
		}

		// Token: 0x04001765 RID: 5989
		private static readonly string pathLogSocket = Path.Combine(Utils.dataPath, "log_socket_client.txt");

		// Token: 0x04001766 RID: 5990
		public int port = -1;

		// Token: 0x04001767 RID: 5991
		public bool isConnected;

		// Token: 0x04001768 RID: 5992
		private Socket sender;
	}
}
