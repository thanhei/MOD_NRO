using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Windows.Forms;

namespace LauncherExe
{
	public sealed class LauncherForm : Form
	{
		private readonly LauncherStorage storage;
		private LauncherSettings settings;
		private readonly TextBox txtManifestUrl;
		private readonly TextBox txtInstallFolder;
		private readonly TextBox txtGameRelativePath;
		private readonly TextBox txtAccountToolRelativePath;
		private readonly ProgressBar progressBar;
		private readonly TextBox txtLog;
		private readonly Button btnSaveSettings;
		private readonly Button btnUpdate;
		private readonly Button btnOpenGame;
		private readonly Button btnOpenTool;

		public LauncherForm()
		{
			this.storage = new LauncherStorage(AppDomain.CurrentDomain.BaseDirectory);
			this.settings = this.storage.LoadSettings();

			this.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point);
			this.Text = "MOD NRO Launcher";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.MinimumSize = new Size(720, 520);
			this.ClientSize = new Size(820, 580);

			TableLayoutPanel root = new TableLayoutPanel();
			root.ColumnCount = 1;
			root.RowCount = 4;
			root.Dock = DockStyle.Fill;
			root.Padding = new Padding(12);
			root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			root.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.Controls.Add(root);

			GroupBox grpConfig = new GroupBox();
			grpConfig.Text = "Cấu hình launcher";
			grpConfig.Dock = DockStyle.Top;
			root.Controls.Add(grpConfig, 0, 0);

			TableLayoutPanel config = new TableLayoutPanel();
			config.ColumnCount = 3;
			config.RowCount = 4;
			config.Dock = DockStyle.Fill;
			config.Padding = new Padding(10, 8, 10, 8);
			config.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			config.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			config.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			grpConfig.Controls.Add(config);

			config.Controls.Add(this.CreateLabel("Manifest URL:"), 0, 0);
			this.txtManifestUrl = this.CreateTextBox();
			this.txtManifestUrl.Text = this.settings.ManifestUrl ?? string.Empty;
			config.Controls.Add(this.txtManifestUrl, 1, 0);
			config.SetColumnSpan(this.txtManifestUrl, 2);

			config.Controls.Add(this.CreateLabel("Thư mục cài:"), 0, 1);
			this.txtInstallFolder = this.CreateTextBox();
			this.txtInstallFolder.Text = this.settings.InstallFolder ?? string.Empty;
			config.Controls.Add(this.txtInstallFolder, 1, 1);
			Button btnBrowseInstallFolder = new Button();
			btnBrowseInstallFolder.AutoSize = true;
			btnBrowseInstallFolder.Text = "Chọn...";
			btnBrowseInstallFolder.Click += this.BtnBrowseInstallFolder_Click;
			config.Controls.Add(btnBrowseInstallFolder, 2, 1);

			config.Controls.Add(this.CreateLabel("Game path:"), 0, 2);
			this.txtGameRelativePath = this.CreateTextBox();
			this.txtGameRelativePath.Text = string.IsNullOrWhiteSpace(this.settings.GameRelativePath) ? @"Game\game.exe" : this.settings.GameRelativePath;
			config.Controls.Add(this.txtGameRelativePath, 1, 2);
			config.SetColumnSpan(this.txtGameRelativePath, 2);

			config.Controls.Add(this.CreateLabel("QLTK path:"), 0, 3);
			this.txtAccountToolRelativePath = this.CreateTextBox();
			this.txtAccountToolRelativePath.Text = string.IsNullOrWhiteSpace(this.settings.AccountManagerRelativePath) ? @"Tools\AccountManagerExe.exe" : this.settings.AccountManagerRelativePath;
			config.Controls.Add(this.txtAccountToolRelativePath, 1, 3);
			config.SetColumnSpan(this.txtAccountToolRelativePath, 2);

			FlowLayoutPanel actions = new FlowLayoutPanel();
			actions.Dock = DockStyle.Top;
			actions.AutoSize = true;
			actions.WrapContents = false;
			actions.Margin = new Padding(0, 10, 0, 10);
			root.Controls.Add(actions, 0, 1);

			this.btnSaveSettings = new Button();
			this.btnSaveSettings.AutoSize = true;
			this.btnSaveSettings.Text = "Lưu cấu hình";
			this.btnSaveSettings.Click += this.BtnSaveSettings_Click;
			actions.Controls.Add(this.btnSaveSettings);

			this.btnUpdate = new Button();
			this.btnUpdate.AutoSize = true;
			this.btnUpdate.Text = "Kiểm tra / cập nhật";
			this.btnUpdate.Click += this.BtnUpdate_Click;
			actions.Controls.Add(this.btnUpdate);

			this.btnOpenGame = new Button();
			this.btnOpenGame.AutoSize = true;
			this.btnOpenGame.Text = "Chơi game";
			this.btnOpenGame.Click += this.BtnOpenGame_Click;
			actions.Controls.Add(this.btnOpenGame);

			this.btnOpenTool = new Button();
			this.btnOpenTool.AutoSize = true;
			this.btnOpenTool.Text = "Quản lý tài khoản";
			this.btnOpenTool.Click += this.BtnOpenTool_Click;
			actions.Controls.Add(this.btnOpenTool);

			this.progressBar = new ProgressBar();
			this.progressBar.Dock = DockStyle.Top;
			this.progressBar.Height = 24;
			root.Controls.Add(this.progressBar, 0, 2);

			this.txtLog = new TextBox();
			this.txtLog.Dock = DockStyle.Fill;
			this.txtLog.Multiline = true;
			this.txtLog.ScrollBars = ScrollBars.Vertical;
			this.txtLog.ReadOnly = true;
			root.Controls.Add(this.txtLog, 0, 3);

			this.RefreshLaunchButtons();
			this.Log("Launcher sẵn sàng.");
		}

		private async void BtnUpdate_Click(object sender, EventArgs e)
		{
			try
			{
				this.ToggleBusy(true);
				this.LoadSettingsFromForm();
				this.ValidateSettings();
				this.storage.SaveSettings(this.settings);
				Directory.CreateDirectory(this.settings.InstallFolder);

				this.Log("Đang tải manifest...");
				string json;
				using (WebClient client = new WebClient())
				{
					json = await client.DownloadStringTaskAsync(new Uri(this.settings.ManifestUrl));
				}

				LauncherManifest manifest = this.storage.ReadJsonFromString<LauncherManifest>(json);
				if (manifest == null || manifest.Files == null || manifest.Files.Count == 0)
				{
					throw new InvalidOperationException("Manifest không có file nào để cập nhật.");
				}

				this.Log("Phiên bản manifest: " + (manifest.Version ?? "không rõ"));
				for (int i = 0; i < manifest.Files.Count; i++)
				{
					ManifestFile file = manifest.Files[i];
					await this.ProcessManifestFileAsync(file, i + 1, manifest.Files.Count);
				}

				this.progressBar.Value = 100;
				this.Log("Cập nhật hoàn tất.");
			}
			catch (Exception ex)
			{
				this.Log("Lỗi: " + ex.Message);
				MessageBox.Show(this, ex.Message, "Cập nhật thất bại", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				this.ToggleBusy(false);
				this.RefreshLaunchButtons();
			}
		}

		private void BtnSaveSettings_Click(object sender, EventArgs e)
		{
			try
			{
				this.LoadSettingsFromForm();
				this.ValidateSettings(allowEmptyManifest: true);
				this.storage.SaveSettings(this.settings);
				this.Log("Đã lưu cấu hình launcher.");
				this.RefreshLaunchButtons();
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Cấu hình không hợp lệ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		private void BtnOpenGame_Click(object sender, EventArgs e)
		{
			this.StartInstalledFile(this.settings.GameRelativePath, "Không tìm thấy file game.");
		}

		private void BtnOpenTool_Click(object sender, EventArgs e)
		{
			this.StartInstalledFile(this.settings.AccountManagerRelativePath, "Không tìm thấy file quản lý tài khoản.");
		}

		private void BtnBrowseInstallFolder_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				dialog.Description = "Chọn thư mục cài đặt local";
				dialog.SelectedPath = this.txtInstallFolder.Text;
				if (dialog.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				this.txtInstallFolder.Text = dialog.SelectedPath;
			}
		}

		private async System.Threading.Tasks.Task ProcessManifestFileAsync(ManifestFile file, int currentIndex, int totalFiles)
		{
			this.ValidateManifestFile(file);
			string relativePath = file.RelativePath.Replace('/', Path.DirectorySeparatorChar).TrimStart(Path.DirectorySeparatorChar);
			string localPackagePath = this.GetSafeInstallPath(relativePath);
			string packageDirectory = Path.GetDirectoryName(localPackagePath);
			if (!string.IsNullOrEmpty(packageDirectory))
			{
				Directory.CreateDirectory(packageDirectory);
			}

			bool shouldDownload = !File.Exists(localPackagePath);
			if (!shouldDownload && !string.IsNullOrWhiteSpace(file.Sha256))
			{
				string currentHash = this.storage.ComputeSha256(localPackagePath);
				shouldDownload = !currentHash.Equals(file.Sha256, StringComparison.OrdinalIgnoreCase);
			}

			if (shouldDownload)
			{
				this.Log(string.Format("({0}/{1}) Tải {2}", currentIndex, totalFiles, relativePath));
				await this.DownloadFileAsync(file.Url, localPackagePath, currentIndex, totalFiles);
				if (!string.IsNullOrWhiteSpace(file.Sha256))
				{
					string downloadedHash = this.storage.ComputeSha256(localPackagePath);
					if (!downloadedHash.Equals(file.Sha256, StringComparison.OrdinalIgnoreCase))
					{
						throw new InvalidOperationException("SHA256 không khớp: " + relativePath);
					}
				}
			}
			else
			{
				this.Log(string.Format("({0}/{1}) Đã mới nhất: {2}", currentIndex, totalFiles, relativePath));
				this.progressBar.Value = Math.Min(100, currentIndex * 100 / totalFiles);
			}

			if (file.Extract)
			{
				string extractTarget = string.IsNullOrWhiteSpace(file.ExtractTo) ? Path.GetDirectoryName(localPackagePath) : this.GetSafeInstallPath(file.ExtractTo.Replace('/', Path.DirectorySeparatorChar));
				if (shouldDownload || !Directory.Exists(extractTarget))
				{
					this.ExtractPackage(localPackagePath, extractTarget);
					this.Log("Đã giải nén vào: " + extractTarget);
				}
			}
		}

		private System.Threading.Tasks.Task DownloadFileAsync(string url, string localPath, int currentIndex, int totalFiles)
		{
			var tcs = new System.Threading.Tasks.TaskCompletionSource<object>();
			WebClient client = new WebClient();
			AsyncCompletedEventHandler completed = null;
			DownloadProgressChangedEventHandler progress = null;

			progress = delegate(object sender, DownloadProgressChangedEventArgs e)
			{
				int baseProgress = (currentIndex - 1) * 100 / totalFiles;
				int segmentSize = 100 / totalFiles;
				int value = baseProgress + e.ProgressPercentage * Math.Max(1, segmentSize) / 100;
				this.progressBar.Value = Math.Max(0, Math.Min(100, value));
			};

			completed = delegate(object sender, AsyncCompletedEventArgs e)
			{
				client.DownloadProgressChanged -= progress;
				client.DownloadFileCompleted -= completed;
				client.Dispose();
				if (e.Error != null)
				{
					tcs.TrySetException(e.Error);
					return;
				}
				if (e.Cancelled)
				{
					tcs.TrySetCanceled();
					return;
				}
				this.progressBar.Value = Math.Min(100, currentIndex * 100 / totalFiles);
				tcs.TrySetResult(null);
			};

			client.DownloadProgressChanged += progress;
			client.DownloadFileCompleted += completed;
			client.DownloadFileAsync(new Uri(url), localPath);
			return tcs.Task;
		}

		private void ExtractPackage(string zipPath, string extractTarget)
		{
			if (string.IsNullOrWhiteSpace(extractTarget))
			{
				throw new InvalidOperationException("Thiếu thư mục giải nén.");
			}
			string installRoot = Path.GetFullPath(this.settings.InstallFolder);
			string extractFullPath = Path.GetFullPath(extractTarget);
			if (!extractFullPath.StartsWith(installRoot, StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException("Thư mục giải nén phải nằm trong thư mục cài đặt.");
			}
			if (Directory.Exists(extractFullPath))
			{
				Directory.Delete(extractFullPath, true);
			}
			Directory.CreateDirectory(extractFullPath);
			ZipFile.ExtractToDirectory(zipPath, extractFullPath);
		}

		private void StartInstalledFile(string relativePath, string missingMessage)
		{
			this.LoadSettingsFromForm();
			string fullPath = this.GetSafeInstallPath((relativePath ?? string.Empty).Replace('/', Path.DirectorySeparatorChar));
			if (!File.Exists(fullPath))
			{
				MessageBox.Show(this, missingMessage + "\n" + fullPath, "Thiếu file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			Process.Start(new ProcessStartInfo
			{
				FileName = fullPath,
				WorkingDirectory = Path.GetDirectoryName(fullPath)
			});
		}

		private void RefreshLaunchButtons()
		{
			try
			{
				string gamePath = this.GetSafeInstallPath(this.txtGameRelativePath.Text.Trim().Replace('/', Path.DirectorySeparatorChar));
				this.btnOpenGame.Enabled = File.Exists(gamePath);
			}
			catch
			{
				this.btnOpenGame.Enabled = false;
			}
			try
			{
				string toolPath = this.GetSafeInstallPath(this.txtAccountToolRelativePath.Text.Trim().Replace('/', Path.DirectorySeparatorChar));
				this.btnOpenTool.Enabled = File.Exists(toolPath);
			}
			catch
			{
				this.btnOpenTool.Enabled = false;
			}
		}

		private void ToggleBusy(bool busy)
		{
			this.btnSaveSettings.Enabled = !busy;
			this.btnUpdate.Enabled = !busy;
			if (busy)
			{
				this.btnOpenGame.Enabled = false;
				this.btnOpenTool.Enabled = false;
			}
			this.UseWaitCursor = busy;
		}

		private void LoadSettingsFromForm()
		{
			this.settings.ManifestUrl = this.txtManifestUrl.Text.Trim();
			this.settings.InstallFolder = this.txtInstallFolder.Text.Trim();
			this.settings.GameRelativePath = this.txtGameRelativePath.Text.Trim();
			this.settings.AccountManagerRelativePath = this.txtAccountToolRelativePath.Text.Trim();
		}

		private void ValidateSettings(bool allowEmptyManifest = false)
		{
			if (!allowEmptyManifest && string.IsNullOrWhiteSpace(this.settings.ManifestUrl))
			{
				throw new InvalidOperationException("Bạn cần nhập Manifest URL.");
			}
			if (!string.IsNullOrWhiteSpace(this.settings.ManifestUrl) && !Uri.IsWellFormedUriString(this.settings.ManifestUrl, UriKind.Absolute))
			{
				throw new InvalidOperationException("Manifest URL không hợp lệ.");
			}
			if (string.IsNullOrWhiteSpace(this.settings.InstallFolder))
			{
				throw new InvalidOperationException("Bạn cần chọn thư mục cài đặt.");
			}
			if (string.IsNullOrWhiteSpace(this.settings.GameRelativePath))
			{
				throw new InvalidOperationException("Game path không được để trống.");
			}
			if (string.IsNullOrWhiteSpace(this.settings.AccountManagerRelativePath))
			{
				throw new InvalidOperationException("QLTK path không được để trống.");
			}
		}

		private void ValidateManifestFile(ManifestFile file)
		{
			if (file == null)
			{
				throw new InvalidOperationException("Manifest có item rỗng.");
			}
			if (string.IsNullOrWhiteSpace(file.RelativePath))
			{
				throw new InvalidOperationException("Manifest thiếu path.");
			}
			if (string.IsNullOrWhiteSpace(file.Url) || !Uri.IsWellFormedUriString(file.Url, UriKind.Absolute))
			{
				throw new InvalidOperationException("Manifest có url không hợp lệ: " + file.RelativePath);
			}
		}

		private string GetSafeInstallPath(string relativePath)
		{
			if (string.IsNullOrWhiteSpace(this.settings.InstallFolder))
			{
				throw new InvalidOperationException("Bạn cần chọn thư mục cài đặt.");
			}
			string installRoot = Path.GetFullPath(this.settings.InstallFolder);
			string combined = Path.GetFullPath(Path.Combine(installRoot, relativePath ?? string.Empty));
			if (!combined.StartsWith(installRoot, StringComparison.OrdinalIgnoreCase))
			{
				throw new InvalidOperationException("Đường dẫn phải nằm trong thư mục cài đặt.");
			}
			return combined;
		}

		private void Log(string message)
		{
			this.txtLog.AppendText(DateTime.Now.ToString("HH:mm:ss") + "  " + message + Environment.NewLine);
		}

		private Label CreateLabel(string text)
		{
			Label label = new Label();
			label.Text = text;
			label.AutoSize = true;
			label.Anchor = AnchorStyles.Left;
			label.Margin = new Padding(0, 8, 8, 8);
			return label;
		}

		private TextBox CreateTextBox()
		{
			TextBox textBox = new TextBox();
			textBox.Dock = DockStyle.Top;
			textBox.Margin = new Padding(0, 4, 8, 4);
			return textBox;
		}
	}
}
