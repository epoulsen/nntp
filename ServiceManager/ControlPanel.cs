using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Configuration;
using System.Xml;
using System.Security;

namespace derIgel.RsdnNntp
{
	/// <summary>
	/// Summary description for ControlPanel.
	/// </summary>
	public class ControlPanel : System.Windows.Forms.Form
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ControlPanel()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
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
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ControlPanel));
			this.okButton = new System.Windows.Forms.Button();
			this.cancelButton = new System.Windows.Forms.Button();
			this.applyButton = new System.Windows.Forms.Button();
			this.propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.alertImage = new System.Windows.Forms.PictureBox();
			this.alertText = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// okButton
			// 
			this.okButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.okButton.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.okButton.Location = new System.Drawing.Point(33, 319);
			this.okButton.Name = "okButton";
			this.okButton.TabIndex = 1;
			this.okButton.Text = "OK";
			this.okButton.Click += new System.EventHandler(this.ApplySettings);
			// 
			// cancelButton
			// 
			this.cancelButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Location = new System.Drawing.Point(137, 319);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.TabIndex = 2;
			this.cancelButton.Text = "Cancel";
			// 
			// applyButton
			// 
			this.applyButton.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
			this.applyButton.Enabled = false;
			this.applyButton.Location = new System.Drawing.Point(233, 319);
			this.applyButton.Name = "applyButton";
			this.applyButton.TabIndex = 3;
			this.applyButton.Text = "Apply";
			this.applyButton.Click += new System.EventHandler(this.ApplySettings);
			// 
			// propertyGrid
			// 
			this.propertyGrid.Anchor = (((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.propertyGrid.CommandsBackColor = System.Drawing.SystemColors.Highlight;
			this.propertyGrid.CommandsVisibleIfAvailable = true;
			this.propertyGrid.LargeButtons = false;
			this.propertyGrid.LineColor = System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid.Name = "propertyGrid";
			this.propertyGrid.Size = new System.Drawing.Size(343, 291);
			this.propertyGrid.TabIndex = 4;
			this.propertyGrid.Text = "propertyGrid";
			this.propertyGrid.ViewBackColor = System.Drawing.SystemColors.Window;
			this.propertyGrid.ViewForeColor = System.Drawing.SystemColors.WindowText;
			this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
			// 
			// alertImage
			// 
			this.alertImage.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.alertImage.Image = ((System.Drawing.Bitmap)(resources.GetObject("alertImage.Image")));
			this.alertImage.Location = new System.Drawing.Point(7, 297);
			this.alertImage.Name = "alertImage";
			this.alertImage.Size = new System.Drawing.Size(16, 16);
			this.alertImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
			this.alertImage.TabIndex = 5;
			this.alertImage.TabStop = false;
			this.alertImage.Visible = false;
			// 
			// alertText
			// 
			this.alertText.Anchor = (System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left);
			this.alertText.AutoSize = true;
			this.alertText.Location = new System.Drawing.Point(32, 299);
			this.alertText.Name = "alertText";
			this.alertText.Size = new System.Drawing.Size(248, 13);
			this.alertText.TabIndex = 6;
			this.alertText.Text = "Changes will take effect after you restart service.";
			this.alertText.Visible = false;
			// 
			// ControlPanel
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(343, 344);
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																																	this.alertText,
																																	this.alertImage,
																																	this.propertyGrid,
																																	this.applyButton,
																																	this.cancelButton,
																																	this.okButton});
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ControlPanel";
			this.Text = "RSDN NNTP Manager";
			this.TopMost = true;
			this.ResumeLayout(false);

		}
		#endregion

		internal System.Windows.Forms.PropertyGrid propertyGrid;

		private System.Windows.Forms.Button okButton;
		private System.Windows.Forms.Button cancelButton;
		private System.Windows.Forms.PictureBox alertImage;
		private System.Windows.Forms.Label alertText;
		private System.Windows.Forms.Button applyButton;

		private void ApplySettings(object sender, System.EventArgs e)
		{
			RsdnNntpSettings settings = (RsdnNntpSettings)propertyGrid.SelectedObject;
			try 
			{
				applyButton.Enabled = false;
				// write config file
				settings.Serialize(ConfigurationSettings.AppSettings["service.Config"]);
				// change startup mode
				((Notify)Owner).service.ChangeStartMode((settings.StartupMode == RsdnNntpSettings.StartupType.Auto) ?
					"Automatic" : settings.StartupMode.ToString());
				// refresh status
				((Notify)Owner).RefreshStatus();
			}
			catch (UnauthorizedAccessException)
			{
				MessageBox.Show(this, "You don't have access rights for config.", "RSDN NNTP Manager",
					MessageBoxButtons.OK,	MessageBoxIcon.Error);
				propertyGrid.SelectedObject = settings = (RsdnNntpSettings)RsdnNntpSettings.Deseriazlize(
					ConfigurationSettings.AppSettings["service.Config"], typeof(RsdnNntpSettings));
				ShowAlert(false);
			}
		}

		private void propertyGrid_PropertyValueChanged(object s,
			System.Windows.Forms.PropertyValueChangedEventArgs e)
		{
			applyButton.Enabled = true;
			ShowAlert(true);
		}

		internal protected void ShowAlert(bool show)
		{
			alertImage.Visible = show;
			alertText.Visible = show;
		}
	}
}