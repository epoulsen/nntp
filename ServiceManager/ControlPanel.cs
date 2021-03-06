using System;
using System.ComponentModel;
using System.Configuration;
using System.Management;
using System.Windows.Forms;
using Rsdn.Nntp;

namespace Rsdn.RsdnNntp
{
	/// <summary>
	/// Summary description for ControlPanel.
	/// </summary>
	public class ControlPanel : Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private Container components;

		public ControlPanel(Settings settings)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			Settings = settings;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
      var resources = new System.ComponentModel.ComponentResourceManager(typeof(ControlPanel));
      this.okButton = new System.Windows.Forms.Button();
      this.cancelButton = new System.Windows.Forms.Button();
      this.applyButton = new System.Windows.Forms.Button();
      this.alertImage = new System.Windows.Forms.PictureBox();
      this.alertText = new System.Windows.Forms.Label();
      this.serverPropertyGrid = new System.Windows.Forms.PropertyGrid();
      this.tabPage1 = new System.Windows.Forms.TabPage();
      this.tabControl = new System.Windows.Forms.TabControl();
      this.serverSettingsTabPage = new System.Windows.Forms.TabPage();
      this.dataProviderSettingsTabPage = new System.Windows.Forms.TabPage();
      this.dataProviderPropertyGrid = new System.Windows.Forms.PropertyGrid();
      ((System.ComponentModel.ISupportInitialize)(this.alertImage)).BeginInit();
      this.tabControl.SuspendLayout();
      this.serverSettingsTabPage.SuspendLayout();
      this.dataProviderSettingsTabPage.SuspendLayout();
      this.SuspendLayout();
      // 
      // okButton
      // 
      this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
      this.okButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.okButton.Location = new System.Drawing.Point(133, 449);
      this.okButton.Name = "okButton";
      this.okButton.Size = new System.Drawing.Size(75, 23);
      this.okButton.TabIndex = 1;
      this.okButton.Text = "OK";
      this.okButton.Click += new System.EventHandler(this.ApplySettings);
      // 
      // cancelButton
      // 
      this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
      this.cancelButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.cancelButton.Location = new System.Drawing.Point(237, 449);
      this.cancelButton.Name = "cancelButton";
      this.cancelButton.Size = new System.Drawing.Size(75, 23);
      this.cancelButton.TabIndex = 2;
      this.cancelButton.Text = "Cancel";
      // 
      // applyButton
      // 
      this.applyButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
      this.applyButton.Enabled = false;
      this.applyButton.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.applyButton.Location = new System.Drawing.Point(333, 449);
      this.applyButton.Name = "applyButton";
      this.applyButton.Size = new System.Drawing.Size(75, 23);
      this.applyButton.TabIndex = 3;
      this.applyButton.Text = "Apply";
      this.applyButton.Click += new System.EventHandler(this.ApplySettings);
      // 
      // alertImage
      // 
      this.alertImage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.alertImage.Image = ((System.Drawing.Image)(resources.GetObject("alertImage.Image")));
      this.alertImage.Location = new System.Drawing.Point(7, 429);
      this.alertImage.Name = "alertImage";
      this.alertImage.Size = new System.Drawing.Size(16, 16);
      this.alertImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
      this.alertImage.TabIndex = 5;
      this.alertImage.TabStop = false;
      this.alertImage.Visible = false;
      // 
      // alertText
      // 
      this.alertText.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
      this.alertText.AutoSize = true;
      this.alertText.FlatStyle = System.Windows.Forms.FlatStyle.System;
      this.alertText.Location = new System.Drawing.Point(32, 431);
      this.alertText.Name = "alertText";
      this.alertText.Size = new System.Drawing.Size(236, 13);
      this.alertText.TabIndex = 6;
      this.alertText.Text = "Changes will take effect after you restart service.";
      this.alertText.Visible = false;
      // 
      // serverPropertyGrid
      // 
      this.serverPropertyGrid.CommandsBackColor = System.Drawing.SystemColors.Highlight;
      this.serverPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.serverPropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
      this.serverPropertyGrid.Location = new System.Drawing.Point(0, 0);
      this.serverPropertyGrid.Name = "serverPropertyGrid";
      this.serverPropertyGrid.PropertySort = System.Windows.Forms.PropertySort.Categorized;
      this.serverPropertyGrid.Size = new System.Drawing.Size(535, 395);
      this.serverPropertyGrid.TabIndex = 4;
      this.serverPropertyGrid.ToolbarVisible = false;
      this.serverPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
      // 
      // tabPage1
      // 
      this.tabPage1.Location = new System.Drawing.Point(4, 22);
      this.tabPage1.Name = "tabPage1";
      this.tabPage1.Size = new System.Drawing.Size(331, 322);
      this.tabPage1.TabIndex = 2;
      this.tabPage1.Text = "About";
      // 
      // tabControl
      // 
      this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                  | System.Windows.Forms.AnchorStyles.Left)
                  | System.Windows.Forms.AnchorStyles.Right)));
      this.tabControl.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
      this.tabControl.Controls.Add(this.serverSettingsTabPage);
      this.tabControl.Controls.Add(this.dataProviderSettingsTabPage);
      this.tabControl.Location = new System.Drawing.Point(0, 0);
      this.tabControl.Multiline = true;
      this.tabControl.Name = "tabControl";
      this.tabControl.SelectedIndex = 0;
      this.tabControl.Size = new System.Drawing.Size(543, 424);
      this.tabControl.TabIndex = 7;
      // 
      // serverSettingsTabPage
      // 
      this.serverSettingsTabPage.Controls.Add(this.serverPropertyGrid);
      this.serverSettingsTabPage.Location = new System.Drawing.Point(4, 25);
      this.serverSettingsTabPage.Name = "serverSettingsTabPage";
      this.serverSettingsTabPage.Size = new System.Drawing.Size(535, 395);
      this.serverSettingsTabPage.TabIndex = 0;
      this.serverSettingsTabPage.Text = "Server Settings";
      // 
      // dataProviderSettingsTabPage
      // 
      this.dataProviderSettingsTabPage.Controls.Add(this.dataProviderPropertyGrid);
      this.dataProviderSettingsTabPage.Location = new System.Drawing.Point(4, 25);
      this.dataProviderSettingsTabPage.Name = "dataProviderSettingsTabPage";
      this.dataProviderSettingsTabPage.Size = new System.Drawing.Size(535, 395);
      this.dataProviderSettingsTabPage.TabIndex = 1;
      this.dataProviderSettingsTabPage.Text = "Data Provider Settings";
      // 
      // dataProviderPropertyGrid
      // 
      this.dataProviderPropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
      this.dataProviderPropertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
      this.dataProviderPropertyGrid.Location = new System.Drawing.Point(0, 0);
      this.dataProviderPropertyGrid.Name = "dataProviderPropertyGrid";
      this.dataProviderPropertyGrid.Size = new System.Drawing.Size(535, 395);
      this.dataProviderPropertyGrid.TabIndex = 0;
      this.dataProviderPropertyGrid.ToolbarVisible = false;
      this.dataProviderPropertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
      // 
      // ControlPanel
      // 
      this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
      this.ClientSize = new System.Drawing.Size(543, 476);
      this.Controls.Add(this.tabControl);
      this.Controls.Add(this.alertText);
      this.Controls.Add(this.alertImage);
      this.Controls.Add(this.applyButton);
      this.Controls.Add(this.cancelButton);
      this.Controls.Add(this.okButton);
      this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
      this.MaximizeBox = false;
      this.MinimizeBox = false;
      this.Name = "ControlPanel";
      this.Text = "RSDN NNTP Manager";
      ((System.ComponentModel.ISupportInitialize)(this.alertImage)).EndInit();
      this.tabControl.ResumeLayout(false);
      this.serverSettingsTabPage.ResumeLayout(false);
      this.dataProviderSettingsTabPage.ResumeLayout(false);
      this.ResumeLayout(false);
      this.PerformLayout();

		}
		#endregion


		private Button okButton;
		private Button cancelButton;
		private PictureBox alertImage;
		private Label alertText;
		private TabPage tabPage1;
		private TabPage serverSettingsTabPage;
		private TabPage dataProviderSettingsTabPage;
		private TabControl tabControl;
		private PropertyGrid dataProviderPropertyGrid;
		private PropertyGrid serverPropertyGrid;
		private Button applyButton;

		private void ApplySettings(object sender, EventArgs e)
		{
			try 
			{
				applyButton.Enabled = false;
				// write config file
                settings.Serialize(ConfigurationManager.AppSettings["settings.ConfigFile"]);
				// change startup mode
				((Notify)Owner).service.ChangeStartMode((settings.StartupMode == StartupType.Auto) ?
					"Automatic" : settings.StartupMode.ToString());
				// refresh status
				((Notify)Owner).RefreshStatus();
			}
			catch (UnauthorizedAccessException)
			{
				MessageBox.Show(this, "You don't have access rights for config.", "RSDN NNTP Manager",
					MessageBoxButtons.OK,	MessageBoxIcon.Error);
                Settings = new Settings(NntpSettings.Deseriazlize(ConfigurationManager.AppSettings["service.Config"]));
				ShowAlert(false);
			}
			catch (ManagementException) { }
		}

		private void propertyGrid_PropertyValueChanged(object s,
			PropertyValueChangedEventArgs e)
		{
			// if data provider changed
			if (e.ChangedItem.PropertyDescriptor.Name == "DataProviderType")
				dataProviderPropertyGrid.SelectedObject = settings.DataProviderSettings;
			applyButton.Enabled = true;
			ShowAlert(true);
		}

		internal protected void ShowAlert(bool show)
		{
			alertImage.Visible = show;
			alertText.Visible = show;
		}

		protected Settings settings;
		public Settings Settings
		{
			get { return settings; }
			set 
			{
				settings = value; 
				serverPropertyGrid.SelectedObject = settings;
				dataProviderPropertyGrid.SelectedObject = settings.DataProviderSettings;
			}
		}
	}
}
