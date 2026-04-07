using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace AccountManagerExe
{
	public sealed class AccountManagerForm : Form
	{
		private readonly Storage storage;
		private readonly List<AccountEntry> accounts;
		private readonly ManagerSettings settings;
		private readonly BindingSource bindingSource;
		private readonly TextBox txtUsername;
		private readonly TextBox txtPassword;
		private readonly NumericUpDown nudServer;
		private readonly NumericUpDown nudWidth;
		private readonly NumericUpDown nudHeight;
		private readonly TextBox txtGameFolder;
		private readonly Button btnAddOrUpdate;
		private readonly Button btnDelete;
		private readonly Button btnSaveSize;
		private readonly Button btnBrowseGameFolder;
		private readonly Button btnNew;
		private readonly DataGridView gridAccounts;

		public AccountManagerForm()
		{
			this.storage = new Storage(AppDomain.CurrentDomain.BaseDirectory);
			this.accounts = this.storage.LoadAccounts();
			this.settings = this.storage.LoadSettings();
			this.bindingSource = new BindingSource();
			this.bindingSource.DataSource = this.accounts;

			this.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point);
			this.Text = "Quản Lý Tài Khoản";
			this.StartPosition = FormStartPosition.CenterScreen;
			this.MinimumSize = new Size(560, 500);
			this.ClientSize = new Size(620, 520);

			TableLayoutPanel root = new TableLayoutPanel();
			root.ColumnCount = 1;
			root.RowCount = 3;
			root.Dock = DockStyle.Fill;
			root.Padding = new Padding(10);
			root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			root.RowStyles.Add(new RowStyle(SizeType.AutoSize));
			root.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
			this.Controls.Add(root);

			TableLayoutPanel editor = new TableLayoutPanel();
			editor.ColumnCount = 4;
			editor.RowCount = 4;
			editor.Dock = DockStyle.Top;
			editor.AutoSize = true;
			editor.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			editor.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			editor.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			editor.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			root.Controls.Add(editor, 0, 0);

			editor.Controls.Add(this.CreateLabel("Tài khoản:"), 0, 0);
			this.txtUsername = this.CreateTextBox();
			editor.Controls.Add(this.txtUsername, 1, 0);
			editor.SetColumnSpan(this.txtUsername, 3);

			editor.Controls.Add(this.CreateLabel("Mật khẩu:"), 0, 1);
			this.txtPassword = this.CreateTextBox();
			this.txtPassword.UseSystemPasswordChar = true;
			editor.Controls.Add(this.txtPassword, 1, 1);
			editor.SetColumnSpan(this.txtPassword, 3);

			editor.Controls.Add(this.CreateLabel("Máy chủ:"), 0, 2);
			this.nudServer = new NumericUpDown();
			this.nudServer.Minimum = 1;
			this.nudServer.Maximum = 99;
			this.nudServer.Width = 90;
			editor.Controls.Add(this.nudServer, 1, 2);

			FlowLayoutPanel actions = new FlowLayoutPanel();
			actions.AutoSize = true;
			actions.FlowDirection = FlowDirection.LeftToRight;
			actions.WrapContents = false;
			editor.Controls.Add(actions, 2, 2);
			editor.SetColumnSpan(actions, 2);

			this.btnNew = new Button();
			this.btnNew.AutoSize = true;
			this.btnNew.Text = "Mới";
			this.btnNew.Click += this.BtnNew_Click;
			actions.Controls.Add(this.btnNew);

			this.btnAddOrUpdate = new Button();
			this.btnAddOrUpdate.AutoSize = true;
			this.btnAddOrUpdate.Text = "Thêm";
			this.btnAddOrUpdate.Click += this.BtnAddOrUpdate_Click;
			actions.Controls.Add(this.btnAddOrUpdate);

			this.btnDelete = new Button();
			this.btnDelete.AutoSize = true;
			this.btnDelete.Text = "Xóa";
			this.btnDelete.Click += this.BtnDelete_Click;
			actions.Controls.Add(this.btnDelete);

			GroupBox grpSize = new GroupBox();
			grpSize.Text = "Cấu hình game";
			grpSize.Dock = DockStyle.Top;
			grpSize.Height = 120;
			root.Controls.Add(grpSize, 0, 1);

			TableLayoutPanel sizeLayout = new TableLayoutPanel();
			sizeLayout.ColumnCount = 5;
			sizeLayout.RowCount = 2;
			sizeLayout.Dock = DockStyle.Fill;
			sizeLayout.Padding = new Padding(10, 8, 10, 8);
			sizeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			sizeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
			sizeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			sizeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			sizeLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
			grpSize.Controls.Add(sizeLayout);

			sizeLayout.Controls.Add(this.CreateLabel("Thư mục game:"), 0, 0);
			this.txtGameFolder = this.CreateTextBox();
			this.txtGameFolder.Text = this.ResolveInitialGameFolder();
			sizeLayout.Controls.Add(this.txtGameFolder, 1, 0);
			this.btnBrowseGameFolder = new Button();
			this.btnBrowseGameFolder.AutoSize = true;
			this.btnBrowseGameFolder.Text = "Chọn...";
			this.btnBrowseGameFolder.Click += this.BtnBrowseGameFolder_Click;
			sizeLayout.Controls.Add(this.btnBrowseGameFolder, 2, 0);

			sizeLayout.Controls.Add(this.CreateLabel("Size:"), 0, 1);
			FlowLayoutPanel sizePanel = new FlowLayoutPanel();
			sizePanel.AutoSize = true;
			sizePanel.FlowDirection = FlowDirection.LeftToRight;
			sizePanel.WrapContents = false;
			sizeLayout.Controls.Add(sizePanel, 1, 1);
			sizeLayout.SetColumnSpan(sizePanel, 4);

			this.nudWidth = new NumericUpDown();
			this.nudWidth.Minimum = 320;
			this.nudWidth.Maximum = 4096;
			this.nudWidth.Value = 1024;
			this.nudWidth.Width = 90;
			sizePanel.Controls.Add(this.nudWidth);

			sizePanel.Controls.Add(this.CreateInlineLabel("X"));

			this.nudHeight = new NumericUpDown();
			this.nudHeight.Minimum = 240;
			this.nudHeight.Maximum = 2160;
			this.nudHeight.Value = 600;
			this.nudHeight.Width = 90;
			sizePanel.Controls.Add(this.nudHeight);

			this.btnSaveSize = new Button();
			this.btnSaveSize.AutoSize = true;
			this.btnSaveSize.Text = "Lưu size";
			this.btnSaveSize.Click += this.BtnSaveSize_Click;
			sizePanel.Controls.Add(this.btnSaveSize);

			this.gridAccounts = new DataGridView();
			this.gridAccounts.Dock = DockStyle.Fill;
			this.gridAccounts.AllowUserToAddRows = false;
			this.gridAccounts.AllowUserToDeleteRows = false;
			this.gridAccounts.AutoGenerateColumns = false;
			this.gridAccounts.MultiSelect = false;
			this.gridAccounts.ReadOnly = true;
			this.gridAccounts.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
			this.gridAccounts.RowHeadersVisible = false;
			this.gridAccounts.DataSource = this.bindingSource;
			this.gridAccounts.SelectionChanged += this.GridAccounts_SelectionChanged;
			this.gridAccounts.CellFormatting += this.GridAccounts_CellFormatting;
			root.Controls.Add(this.gridAccounts, 0, 2);

			this.gridAccounts.Columns.Add(new DataGridViewTextBoxColumn
			{
				HeaderText = "STT",
				Name = "colIndex",
				Width = 50
			});
			this.gridAccounts.Columns.Add(new DataGridViewTextBoxColumn
			{
				HeaderText = "Tài khoản",
				DataPropertyName = "Username",
				AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill
			});
			this.gridAccounts.Columns.Add(new DataGridViewTextBoxColumn
			{
				HeaderText = "Máy chủ",
				DataPropertyName = "Server",
				Width = 80
			});

			this.LoadExistingWindowSize();
			this.UpdateButtons();
		}

		private void BtnNew_Click(object sender, EventArgs e)
		{
			this.ClearEditor();
			this.txtUsername.Focus();
		}

		private void BtnAddOrUpdate_Click(object sender, EventArgs e)
		{
			string username = this.txtUsername.Text.Trim();
			string password = this.txtPassword.Text;
			if (string.IsNullOrWhiteSpace(username))
			{
				MessageBox.Show(this, "Tài khoản không được để trống.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (string.IsNullOrEmpty(password))
			{
				MessageBox.Show(this, "Mật khẩu không được để trống.", "Thiếu dữ liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			AccountEntry current = this.GetSelectedAccount();
			if (current == null)
			{
				current = new AccountEntry();
				this.accounts.Add(current);
			}

			current.Username = username;
			current.PasswordCipher = Storage.EncryptPassword(password);
			current.Server = Decimal.ToInt32(this.nudServer.Value);

			this.storage.SaveAccounts(this.accounts);
			this.bindingSource.ResetBindings(false);
			this.SelectAccount(current);
		}

		private void BtnDelete_Click(object sender, EventArgs e)
		{
			AccountEntry current = this.GetSelectedAccount();
			if (current == null)
			{
				return;
			}
			DialogResult result = MessageBox.Show(this, "Xóa tài khoản đã chọn?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
			if (result != DialogResult.Yes)
			{
				return;
			}
			this.accounts.Remove(current);
			this.storage.SaveAccounts(this.accounts);
			this.bindingSource.ResetBindings(false);
			this.ClearEditor();
		}

		private void BtnBrowseGameFolder_Click(object sender, EventArgs e)
		{
			using (FolderBrowserDialog dialog = new FolderBrowserDialog())
			{
				dialog.Description = "Chọn thư mục chứa file game.exe";
				dialog.SelectedPath = this.txtGameFolder.Text;
				if (dialog.ShowDialog(this) != DialogResult.OK)
				{
					return;
				}
				this.txtGameFolder.Text = dialog.SelectedPath;
				this.settings.GameFolderPath = dialog.SelectedPath;
				this.storage.SaveSettings(this.settings);
				this.LoadExistingWindowSize();
			}
		}

		private void BtnSaveSize_Click(object sender, EventArgs e)
		{
			string gameFolder = this.txtGameFolder.Text.Trim();
			try
			{
				this.storage.SaveGameWindowSize(gameFolder, Decimal.ToInt32(this.nudWidth.Value), Decimal.ToInt32(this.nudHeight.Value));
				this.settings.GameFolderPath = gameFolder;
				this.storage.SaveSettings(this.settings);
				MessageBox.Show(this, "Đã lưu size game vào file pc_window_size.txt", "Thành công", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
			catch (Exception ex)
			{
				MessageBox.Show(this, ex.Message, "Không thể lưu size", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void GridAccounts_SelectionChanged(object sender, EventArgs e)
		{
			AccountEntry current = this.GetSelectedAccount();
			if (current == null)
			{
				this.UpdateButtons();
				return;
			}
			this.txtUsername.Text = current.Username ?? string.Empty;
			this.txtPassword.Text = Storage.DecryptPassword(current.PasswordCipher);
			this.nudServer.Value = this.ClampValue(current.Server, this.nudServer.Minimum, this.nudServer.Maximum);
			this.UpdateButtons();
		}

		private void GridAccounts_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
		{
			if (e.ColumnIndex == 0)
			{
				e.Value = e.RowIndex + 1;
			}
		}

		private AccountEntry GetSelectedAccount()
		{
			if (this.gridAccounts.SelectedRows.Count == 0)
			{
				return null;
			}
			return this.gridAccounts.SelectedRows[0].DataBoundItem as AccountEntry;
		}

		private void SelectAccount(AccountEntry account)
		{
			int index = this.accounts.IndexOf(account);
			if (index < 0)
			{
				return;
			}
			this.bindingSource.Position = index;
			if (index < this.gridAccounts.Rows.Count)
			{
				this.gridAccounts.Rows[index].Selected = true;
			}
			this.UpdateButtons();
		}

		private void ClearEditor()
		{
			this.gridAccounts.ClearSelection();
			this.gridAccounts.CurrentCell = null;
			this.txtUsername.Text = string.Empty;
			this.txtPassword.Text = string.Empty;
			this.nudServer.Value = 1;
			this.UpdateButtons();
		}

		private void UpdateButtons()
		{
			bool hasSelection = this.GetSelectedAccount() != null;
			this.btnAddOrUpdate.Text = hasSelection ? "Lưu" : "Thêm";
			this.btnDelete.Enabled = hasSelection;
		}

		private void LoadExistingWindowSize()
		{
			string path = Path.Combine(this.txtGameFolder.Text.Trim(), "pc_window_size.txt");
			if (!File.Exists(path))
			{
				return;
			}
			string text = File.ReadAllText(path).Trim().ToLower();
			string[] parts = text.Split(new char[] { 'x', ',', ';', ' ' }, StringSplitOptions.RemoveEmptyEntries);
			int width;
			int height;
			if (parts.Length >= 2 && int.TryParse(parts[0], out width) && int.TryParse(parts[1], out height))
			{
				this.nudWidth.Value = this.ClampValue(width, this.nudWidth.Minimum, this.nudWidth.Maximum);
				this.nudHeight.Value = this.ClampValue(height, this.nudHeight.Minimum, this.nudHeight.Maximum);
			}
		}

		private string ResolveInitialGameFolder()
		{
			if (!string.IsNullOrWhiteSpace(this.settings.GameFolderPath))
			{
				return this.settings.GameFolderPath;
			}
			return AppDomain.CurrentDomain.BaseDirectory;
		}

		private decimal ClampValue(int value, decimal minimum, decimal maximum)
		{
			decimal number = value;
			if (number < minimum)
			{
				return minimum;
			}
			if (number > maximum)
			{
				return maximum;
			}
			return number;
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

		private Label CreateInlineLabel(string text)
		{
			Label label = this.CreateLabel(text);
			label.Margin = new Padding(4, 8, 4, 8);
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
