using System;
using System.Collections;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsActivation.
	/// </summary>
	public class fclsGENActivation : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblActivationText;
		private System.Windows.Forms.RadioButton rbtnEvaluation;
		private System.Windows.Forms.RadioButton rbtnActivation;
		private System.Windows.Forms.Panel pnlEvaluation;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Label lblEvaluation;
		private System.Windows.Forms.Button btnContinueEvaluation;
		private System.Windows.Forms.Panel pnlActivation;
		private System.Windows.Forms.Panel pblSpacer;
		private System.Windows.Forms.Button btnSaveActivation;
		private System.Windows.Forms.Button btnEmailActivation;
		private System.Windows.Forms.TextBox txtEmail;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.TextBox txtName;
		private System.Windows.Forms.Label lblName;
		private System.Windows.Forms.Button btnInstallActivationKey;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private clsActivation2	m_objActivation;
        private OleDbConnection m_odcConnection;
		private System.Windows.Forms.OpenFileDialog ofdActivationKey;
		private int				m_intControlSpacing;

		public fclsGENActivation(clsActivation2 objActivation, OleDbConnection odcConnection)
		{
            m_odcConnection = odcConnection;
            this.DialogResult = DialogResult.Cancel;
			
            // Required for Windows Form Designer support
			InitializeComponent();

			m_intControlSpacing = this.rbtnActivation.Location.Y - (this.rbtnEvaluation.Location.Y + this.rbtnEvaluation.Height);
			m_objActivation = objActivation;
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
			this.btnExit = new System.Windows.Forms.Button();
			this.lblActivationText = new System.Windows.Forms.Label();
			this.rbtnEvaluation = new System.Windows.Forms.RadioButton();
			this.rbtnActivation = new System.Windows.Forms.RadioButton();
			this.pnlEvaluation = new System.Windows.Forms.Panel();
			this.btnContinueEvaluation = new System.Windows.Forms.Button();
			this.lblEvaluation = new System.Windows.Forms.Label();
			this.pnlActivation = new System.Windows.Forms.Panel();
			this.btnInstallActivationKey = new System.Windows.Forms.Button();
			this.pblSpacer = new System.Windows.Forms.Panel();
			this.btnSaveActivation = new System.Windows.Forms.Button();
			this.btnEmailActivation = new System.Windows.Forms.Button();
			this.txtEmail = new System.Windows.Forms.TextBox();
			this.lblEmail = new System.Windows.Forms.Label();
			this.txtName = new System.Windows.Forms.TextBox();
			this.lblName = new System.Windows.Forms.Label();
			this.ofdActivationKey = new System.Windows.Forms.OpenFileDialog();
			this.pnlEvaluation.SuspendLayout();
			this.pnlActivation.SuspendLayout();
			this.SuspendLayout();
			// 
			// btnExit
			// 
			this.btnExit.BackColor = System.Drawing.SystemColors.Control;
			this.btnExit.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnExit.ForeColor = System.Drawing.SystemColors.ControlText;
			this.btnExit.Location = new System.Drawing.Point(16, 152);
			this.btnExit.Name = "btnExit";
			this.btnExit.Size = new System.Drawing.Size(500, 32);
			this.btnExit.TabIndex = 3;
			this.btnExit.Text = "Exit";
			this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
			// 
			// lblActivationText
			// 
			this.lblActivationText.Font = new System.Drawing.Font("MS Reference Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblActivationText.Location = new System.Drawing.Point(12, 8);
			this.lblActivationText.Name = "lblActivationText";
			this.lblActivationText.Size = new System.Drawing.Size(504, 40);
			this.lblActivationText.TabIndex = 13;
			this.lblActivationText.Text = "This software is not activated. Please select one of the following options:";
			// 
			// rbtnEvaluation
			// 
			this.rbtnEvaluation.Appearance = System.Windows.Forms.Appearance.Button;
			this.rbtnEvaluation.BackColor = System.Drawing.SystemColors.Control;
			this.rbtnEvaluation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
			this.rbtnEvaluation.Location = new System.Drawing.Point(16, 56);
			this.rbtnEvaluation.Name = "rbtnEvaluation";
			this.rbtnEvaluation.Size = new System.Drawing.Size(500, 32);
			this.rbtnEvaluation.TabIndex = 16;
			this.rbtnEvaluation.Text = "Evaluation";
			this.rbtnEvaluation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rbtnEvaluation.CheckedChanged += new System.EventHandler(this.rbtnEvaluation_CheckedChanged);
			// 
			// rbtnActivation
			// 
			this.rbtnActivation.Appearance = System.Windows.Forms.Appearance.Button;
			this.rbtnActivation.BackColor = System.Drawing.SystemColors.Control;
			this.rbtnActivation.FlatStyle = System.Windows.Forms.FlatStyle.System;
			this.rbtnActivation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold);
			this.rbtnActivation.Location = new System.Drawing.Point(16, 104);
			this.rbtnActivation.Name = "rbtnActivation";
			this.rbtnActivation.Size = new System.Drawing.Size(500, 32);
			this.rbtnActivation.TabIndex = 17;
			this.rbtnActivation.Text = "Activation";
			this.rbtnActivation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.rbtnActivation.CheckedChanged += new System.EventHandler(this.rbtnActivation_CheckedChanged);
			// 
			// pnlEvaluation
			// 
			this.pnlEvaluation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlEvaluation.Controls.Add(this.btnContinueEvaluation);
			this.pnlEvaluation.Controls.Add(this.lblEvaluation);
			this.pnlEvaluation.Location = new System.Drawing.Point(16, 96);
			this.pnlEvaluation.Name = "pnlEvaluation";
			this.pnlEvaluation.Size = new System.Drawing.Size(500, 40);
			this.pnlEvaluation.TabIndex = 18;
			this.pnlEvaluation.Visible = false;
			// 
			// btnContinueEvaluation
			// 
			this.btnContinueEvaluation.Location = new System.Drawing.Point(320, 8);
			this.btnContinueEvaluation.Name = "btnContinueEvaluation";
			this.btnContinueEvaluation.Size = new System.Drawing.Size(168, 23);
			this.btnContinueEvaluation.TabIndex = 1;
			this.btnContinueEvaluation.Text = "Continue Evaluation";
			// 
			// lblEvaluation
			// 
			this.lblEvaluation.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblEvaluation.ForeColor = System.Drawing.Color.Red;
			this.lblEvaluation.Location = new System.Drawing.Point(8, 8);
			this.lblEvaluation.Name = "lblEvaluation";
			this.lblEvaluation.Size = new System.Drawing.Size(304, 23);
			this.lblEvaluation.TabIndex = 0;
			this.lblEvaluation.Text = "You have 99 orders remaining";
			this.lblEvaluation.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// pnlActivation
			// 
			this.pnlActivation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pnlActivation.Controls.Add(this.btnInstallActivationKey);
			this.pnlActivation.Controls.Add(this.pblSpacer);
			this.pnlActivation.Controls.Add(this.btnSaveActivation);
			this.pnlActivation.Controls.Add(this.btnEmailActivation);
			this.pnlActivation.Controls.Add(this.txtEmail);
			this.pnlActivation.Controls.Add(this.lblEmail);
			this.pnlActivation.Controls.Add(this.txtName);
			this.pnlActivation.Controls.Add(this.lblName);
			this.pnlActivation.Location = new System.Drawing.Point(16, 144);
			this.pnlActivation.Name = "pnlActivation";
			this.pnlActivation.Size = new System.Drawing.Size(500, 152);
			this.pnlActivation.TabIndex = 20;
			this.pnlActivation.Visible = false;
			// 
			// btnInstallActivationKey
			// 
			this.btnInstallActivationKey.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnInstallActivationKey.ForeColor = System.Drawing.Color.Red;
			this.btnInstallActivationKey.Location = new System.Drawing.Point(149, 120);
			this.btnInstallActivationKey.Name = "btnInstallActivationKey";
			this.btnInstallActivationKey.Size = new System.Drawing.Size(200, 24);
			this.btnInstallActivationKey.TabIndex = 24;
			this.btnInstallActivationKey.Text = "Install Activation Key";
			this.btnInstallActivationKey.Click += new System.EventHandler(this.btnInstallActivationKey_Click);
			// 
			// pblSpacer
			// 
			this.pblSpacer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.pblSpacer.Location = new System.Drawing.Point(0, 112);
			this.pblSpacer.Name = "pblSpacer";
			this.pblSpacer.Size = new System.Drawing.Size(500, 1);
			this.pblSpacer.TabIndex = 23;
			// 
			// btnSaveActivation
			// 
			this.btnSaveActivation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnSaveActivation.ForeColor = System.Drawing.Color.DarkOliveGreen;
			this.btnSaveActivation.Location = new System.Drawing.Point(277, 72);
			this.btnSaveActivation.Name = "btnSaveActivation";
			this.btnSaveActivation.Size = new System.Drawing.Size(200, 24);
			this.btnSaveActivation.TabIndex = 22;
			this.btnSaveActivation.Text = "Save an Activation Request";
			this.btnSaveActivation.Click += new System.EventHandler(this.btnSaveActivation_Click);
			// 
			// btnEmailActivation
			// 
			this.btnEmailActivation.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnEmailActivation.ForeColor = System.Drawing.Color.Red;
			this.btnEmailActivation.Location = new System.Drawing.Point(21, 72);
			this.btnEmailActivation.Name = "btnEmailActivation";
			this.btnEmailActivation.Size = new System.Drawing.Size(200, 24);
			this.btnEmailActivation.TabIndex = 21;
			this.btnEmailActivation.Text = "Send an Activation Request";
			this.btnEmailActivation.Click += new System.EventHandler(this.btnEmailActivation_Click);
			// 
			// txtEmail
			// 
			this.txtEmail.Location = new System.Drawing.Point(136, 40);
			this.txtEmail.Name = "txtEmail";
			this.txtEmail.Size = new System.Drawing.Size(352, 20);
			this.txtEmail.TabIndex = 20;
			this.txtEmail.Text = "";
			// 
			// lblEmail
			// 
			this.lblEmail.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblEmail.Location = new System.Drawing.Point(8, 40);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(40, 16);
			this.lblEmail.TabIndex = 19;
			this.lblEmail.Text = "Email";
			// 
			// txtName
			// 
			this.txtName.Location = new System.Drawing.Point(136, 8);
			this.txtName.Name = "txtName";
			this.txtName.Size = new System.Drawing.Size(352, 20);
			this.txtName.TabIndex = 18;
			this.txtName.Text = "";
			// 
			// lblName
			// 
			this.lblName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblName.Location = new System.Drawing.Point(8, 8);
			this.lblName.Name = "lblName";
			this.lblName.Size = new System.Drawing.Size(128, 16);
			this.lblName.TabIndex = 17;
			this.lblName.Text = "Customer Name";
			// 
			// ofdActivationKey
			// 
			this.ofdActivationKey.FileOk += new System.ComponentModel.CancelEventHandler(this.ofdActivationKey_FileOk);
			// 
			// fclsGENActivation
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(528, 198);
			this.ControlBox = false;
			this.Controls.Add(this.rbtnActivation);
			this.Controls.Add(this.rbtnEvaluation);
			this.Controls.Add(this.lblActivationText);
			this.Controls.Add(this.btnExit);
			this.Controls.Add(this.pnlEvaluation);
			this.Controls.Add(this.pnlActivation);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "fclsGENActivation";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quick Stock -  Activation";
			this.Load += new System.EventHandler(this.fclsGENActivation_Load);
			this.pnlEvaluation.ResumeLayout(false);
			this.pnlActivation.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void fclsGENActivation_Load(object sender, System.EventArgs e)
		{
			if(fclsGENSplashScreen.SplashForm != null )
				fclsGENSplashScreen.SplashForm.Owner = this;
			this.Activate();
			fclsGENSplashScreen.CloseForm();
		}

		private void btnSaveActivation_Click(object sender, System.EventArgs e)
		{
			if(this.txtName.Text.Length > 0)
			{
				if(m_objActivation.CreateActivationRequestFile(this.txtName.Text,this.txtEmail.Text))
				{
					MessageBox.Show("The activation request file has been successfully created.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
					this.Close();
				}
				else
					MessageBox.Show("There was an error while attempting to create the activation request file!\r\nPlease ensure that you have read/write permission to the application folder and that\r\nan '" + clsActivation2.m_cstrActivationRequestFile + "' file does not already exist in the application folder.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			else
				MessageBox.Show("You must enter a name!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
		}

		private void btnEmailActivation_Click(object sender, System.EventArgs e)
		{
            fclsGENSendEmail frmGENSendEmail = new fclsGENSendEmail(fclsGENSendEmail.EmailType.Activation);

			if(this.txtName.Text.Length > 0)
			{
				if(m_objActivation.CreateActivationRequestFile(this.txtName.Text,this.txtEmail.Text))
				{
                    frmGENSendEmail.NewEmail(this.txtName.Text,
                                             this.txtEmail.Text,
                                             clsConfiguration.Internal_ConfigurationFilesPath + "\\" + clsActivation2.m_cstrActivationRequestFile);
                    if (frmGENSendEmail.ShowDialog() == DialogResult.OK)
					{
						MessageBox.Show("The activation request was sent succesfully!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
						this.Close();
					}
				}
				else
					MessageBox.Show("There was an error while attempting to create the activation request file!\r\nPlease ensure that you have read/write permission to the application folder and that\r\nan '" + clsActivation2.m_cstrActivationKeyFile + "' file does not already exist in the application folder.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			else
				MessageBox.Show("You must enter a as well as a valid email address!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
		}

		private void rbtnEvaluation_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.rbtnEvaluation.Checked)
			{
				this.Height += this.pnlEvaluation.Height;
				this.rbtnActivation.Location = new Point(this.rbtnActivation.Location.X,this.pnlEvaluation.Location.Y + this.pnlEvaluation.Height + m_intControlSpacing);
				this.btnExit.Location = new Point(this.btnExit.Location.X,this.rbtnActivation.Location.Y + this.rbtnActivation.Height + m_intControlSpacing);
				
				this.pnlEvaluation.Visible = true;
			}
			else
			{
				this.pnlEvaluation.Visible = false;

				this.rbtnActivation.Location = new Point(this.rbtnActivation.Location.X,this.rbtnEvaluation.Location.Y + this.rbtnEvaluation.Height + m_intControlSpacing);
				this.btnExit.Location = new Point(this.btnExit.Location.X,this.rbtnActivation.Location.Y + this.rbtnActivation.Height + m_intControlSpacing);

				this.Height -= this.pnlEvaluation.Height;
			}
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
            this.Close();
		}

		private void rbtnActivation_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.rbtnActivation.Checked)
			{
				this.Height += this.pnlActivation.Height;
				this.btnExit.Location = new Point(this.btnExit.Location.X,this.pnlActivation.Location.Y + this.pnlActivation.Height + m_intControlSpacing);
				
				this.pnlActivation.Visible = true;
			}
			else
			{
				this.pnlActivation.Visible = false;
				
				this.btnExit.Location = new Point(this.btnExit.Location.X,this.rbtnActivation.Location.Y + this.rbtnActivation.Height + m_intControlSpacing);

				this.Height -= this.pnlActivation.Height;
			}		
		}

		private void btnInstallActivationKey_Click(object sender, System.EventArgs e)
		{
			DialogResult dlgResult;

			// configure and then display open file dialog
			this.ofdActivationKey = new OpenFileDialog();
			this.ofdActivationKey.Filter = "Activation Key File (" + clsActivation2.m_cstrActivationKeyFile +")|" + clsActivation2.m_cstrActivationKeyFile;
			this.ofdActivationKey.Title = "Location of Activation Key File";
			dlgResult = this.ofdActivationKey.ShowDialog();
			
			if(dlgResult == DialogResult.OK)
			{
				try
				{
					// move the activation key file to the application path
                    File.Copy(this.ofdActivationKey.FileName, clsConfiguration.Internal_ConfigurationFilesPath + "\\" + clsActivation2.m_cstrActivationKeyFile, true);

					try
					{
						File.Delete(this.ofdActivationKey.FileName);
					}
					catch
					{
					}

					// check if the activation key is valid, and if so start the application
					if(m_objActivation.IsLicenseValid())
					{
                        this.DialogResult = DialogResult.OK;
						this.Close();
					}
					else
						MessageBox.Show("The selected activation key is not valid!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
				catch(IOException ioException)
				{
					MessageBox.Show("An I/O error has occured. Please ensure that an '" + clsActivation2.m_cstrActivationKeyFile + "' file does not already exist in the application folder\r\n\r\nError message:\r\n" + ioException.Message,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
				catch(UnauthorizedAccessException uaException)
				{
					MessageBox.Show("An unauthorized access exception has occured. Please ensure that you have read/write permission to the application folder and that\r\nan '" + clsActivation2.m_cstrActivationKeyFile + "' file does not already exist in the application folder.\r\n\r\nError message:\r\n" + uaException.Message,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
				catch(Exception ex)
				{
					MessageBox.Show("The following error occured while trying to install the activation key:\r\n" + ex.Message + "\r\n" + ex.StackTrace,this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
		}

		private void ofdActivationKey_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(!clsUtilities.CompareStrings(Path.GetFileName(this.ofdActivationKey.FileName),clsActivation2.m_cstrActivationKeyFile))
			{
				MessageBox.Show("This is not a valid activation key filename!","Location of Activation Key File",MessageBoxButtons.OK,MessageBoxIcon.Error);
				e.Cancel = true;
			}
		}
	}
}
