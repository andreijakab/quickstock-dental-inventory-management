using System;
using System.Collections;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsSMBackOrders_Update.
	/// </summary>
	public class fclsGENOptions_SMTPServer : System.Windows.Forms.Form
    {
        private Label lblAccountName;
        private TextBox txtAccountName;
        private TextBox txtServer;
        private Label lblPortNumber;
        private Label lblServer;
        private MaskedTextBox txtPortNumber;
        private GroupBox gpbLogonInformation;
        private CheckBox chkServerRequiresAuthentication;
        private Label lblServerPassword;
        private TextBox txtServerPassword;
        private TextBox txtServerUserName;
        private Label lblServerUserName;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOk;
        private Label lblTimeout;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private clsSMTPListViewItem     m_lviItem;
        private NumericUpDown nudTimeout;
        private Label lblMinutes;
        private ToolTip                 m_ttToolTip;

        public fclsGENOptions_SMTPServer(clsSMTPListViewItem lviItem)
		{
            m_ttToolTip = new ToolTip();

            InitializeComponent();

            this.DialogResult = DialogResult.Cancel;

            // populate form 
            m_lviItem = lviItem;
            this.txtAccountName.Text = m_lviItem.AccountName;
            this.txtServer.Text = m_lviItem.Address;
            this.txtPortNumber.Text = m_lviItem.Port.ToString();
            this.nudTimeout.Value = m_lviItem.Timeout;
            this.chkServerRequiresAuthentication.Checked = m_lviItem.CredentialsRequired;
            this.txtServerUserName.Text = m_lviItem.UserName;
            this.txtServerPassword.Text = m_lviItem.Password;
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblAccountName = new System.Windows.Forms.Label();
            this.txtAccountName = new System.Windows.Forms.TextBox();
            this.txtServer = new System.Windows.Forms.TextBox();
            this.lblPortNumber = new System.Windows.Forms.Label();
            this.lblServer = new System.Windows.Forms.Label();
            this.txtPortNumber = new System.Windows.Forms.MaskedTextBox();
            this.gpbLogonInformation = new System.Windows.Forms.GroupBox();
            this.lblServerPassword = new System.Windows.Forms.Label();
            this.txtServerPassword = new System.Windows.Forms.TextBox();
            this.txtServerUserName = new System.Windows.Forms.TextBox();
            this.lblServerUserName = new System.Windows.Forms.Label();
            this.chkServerRequiresAuthentication = new System.Windows.Forms.CheckBox();
            this.lblTimeout = new System.Windows.Forms.Label();
            this.nudTimeout = new System.Windows.Forms.NumericUpDown();
            this.lblMinutes = new System.Windows.Forms.Label();
            this.gpbLogonInformation.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(218, 225);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 24);
            this.btnOk.TabIndex = 4;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(312, 225);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 24);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblAccountName
            // 
            this.lblAccountName.AutoSize = true;
            this.lblAccountName.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblAccountName.Location = new System.Drawing.Point(12, 12);
            this.lblAccountName.Name = "lblAccountName";
            this.lblAccountName.Size = new System.Drawing.Size(88, 14);
            this.lblAccountName.TabIndex = 63;
            this.lblAccountName.Text = "Account Name";
            // 
            // txtAccountName
            // 
            this.txtAccountName.Location = new System.Drawing.Point(106, 9);
            this.txtAccountName.Name = "txtAccountName";
            this.txtAccountName.Size = new System.Drawing.Size(294, 20);
            this.txtAccountName.TabIndex = 0;
            this.txtAccountName.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtAccountName_KeyDown);
            // 
            // txtServer
            // 
            this.txtServer.Location = new System.Drawing.Point(106, 35);
            this.txtServer.Name = "txtServer";
            this.txtServer.Size = new System.Drawing.Size(294, 20);
            this.txtServer.TabIndex = 1;
            // 
            // lblPortNumber
            // 
            this.lblPortNumber.AutoSize = true;
            this.lblPortNumber.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPortNumber.Location = new System.Drawing.Point(12, 64);
            this.lblPortNumber.Name = "lblPortNumber";
            this.lblPortNumber.Size = new System.Drawing.Size(77, 14);
            this.lblPortNumber.TabIndex = 68;
            this.lblPortNumber.Text = "Port Number";
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblServer.Location = new System.Drawing.Point(12, 38);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(42, 14);
            this.lblServer.TabIndex = 69;
            this.lblServer.Text = "Server";
            // 
            // txtPortNumber
            // 
            this.txtPortNumber.HidePromptOnLeave = true;
            this.txtPortNumber.Location = new System.Drawing.Point(106, 61);
            this.txtPortNumber.Mask = "00000";
            this.txtPortNumber.Name = "txtPortNumber";
            this.txtPortNumber.Size = new System.Drawing.Size(100, 20);
            this.txtPortNumber.TabIndex = 2;
            this.txtPortNumber.ValidatingType = typeof(int);
            this.txtPortNumber.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPortNumber_KeyDown);
            this.txtPortNumber.MaskInputRejected += new System.Windows.Forms.MaskInputRejectedEventHandler(this.txtPortNumber_MaskInputRejected);
            this.txtPortNumber.TextChanged += new System.EventHandler(this.txtPortNumber_TextChanged);
            // 
            // gpbLogonInformation
            // 
            this.gpbLogonInformation.Controls.Add(this.lblServerPassword);
            this.gpbLogonInformation.Controls.Add(this.txtServerPassword);
            this.gpbLogonInformation.Controls.Add(this.txtServerUserName);
            this.gpbLogonInformation.Controls.Add(this.lblServerUserName);
            this.gpbLogonInformation.Enabled = false;
            this.gpbLogonInformation.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpbLogonInformation.Location = new System.Drawing.Point(15, 137);
            this.gpbLogonInformation.Name = "gpbLogonInformation";
            this.gpbLogonInformation.Size = new System.Drawing.Size(383, 82);
            this.gpbLogonInformation.TabIndex = 71;
            this.gpbLogonInformation.TabStop = false;
            this.gpbLogonInformation.Text = "Logon Information";
            // 
            // lblServerPassword
            // 
            this.lblServerPassword.AutoSize = true;
            this.lblServerPassword.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblServerPassword.Location = new System.Drawing.Point(6, 51);
            this.lblServerPassword.Name = "lblServerPassword";
            this.lblServerPassword.Size = new System.Drawing.Size(58, 14);
            this.lblServerPassword.TabIndex = 73;
            this.lblServerPassword.Text = "Password";
            // 
            // txtServerPassword
            // 
            this.txtServerPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtServerPassword.Location = new System.Drawing.Point(78, 49);
            this.txtServerPassword.Name = "txtServerPassword";
            this.txtServerPassword.Size = new System.Drawing.Size(299, 20);
            this.txtServerPassword.TabIndex = 1;
            this.txtServerPassword.UseSystemPasswordChar = true;
            // 
            // txtServerUserName
            // 
            this.txtServerUserName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtServerUserName.Location = new System.Drawing.Point(78, 23);
            this.txtServerUserName.Name = "txtServerUserName";
            this.txtServerUserName.Size = new System.Drawing.Size(299, 20);
            this.txtServerUserName.TabIndex = 0;
            // 
            // lblServerUserName
            // 
            this.lblServerUserName.AutoSize = true;
            this.lblServerUserName.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblServerUserName.Location = new System.Drawing.Point(6, 25);
            this.lblServerUserName.Name = "lblServerUserName";
            this.lblServerUserName.Size = new System.Drawing.Size(66, 14);
            this.lblServerUserName.TabIndex = 70;
            this.lblServerUserName.Text = "User Name";
            // 
            // chkServerRequiresAuthentication
            // 
            this.chkServerRequiresAuthentication.AutoSize = true;
            this.chkServerRequiresAuthentication.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkServerRequiresAuthentication.Location = new System.Drawing.Point(15, 113);
            this.chkServerRequiresAuthentication.Name = "chkServerRequiresAuthentication";
            this.chkServerRequiresAuthentication.Size = new System.Drawing.Size(196, 18);
            this.chkServerRequiresAuthentication.TabIndex = 4;
            this.chkServerRequiresAuthentication.Text = "Server Requires Authentication";
            this.chkServerRequiresAuthentication.UseVisualStyleBackColor = true;
            this.chkServerRequiresAuthentication.CheckedChanged += new System.EventHandler(this.chkServerRequiresAuthentication_CheckedChanged);
            // 
            // lblTimeout
            // 
            this.lblTimeout.AutoSize = true;
            this.lblTimeout.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblTimeout.Location = new System.Drawing.Point(12, 90);
            this.lblTimeout.Name = "lblTimeout";
            this.lblTimeout.Size = new System.Drawing.Size(53, 14);
            this.lblTimeout.TabIndex = 73;
            this.lblTimeout.Text = "Timeout";
            // 
            // nudTimeout
            // 
            this.nudTimeout.Location = new System.Drawing.Point(106, 87);
            this.nudTimeout.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.nudTimeout.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.nudTimeout.Name = "nudTimeout";
            this.nudTimeout.Size = new System.Drawing.Size(45, 20);
            this.nudTimeout.TabIndex = 74;
            this.nudTimeout.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // lblMinutes
            // 
            this.lblMinutes.AutoSize = true;
            this.lblMinutes.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblMinutes.Location = new System.Drawing.Point(156, 90);
            this.lblMinutes.Name = "lblMinutes";
            this.lblMinutes.Size = new System.Drawing.Size(50, 14);
            this.lblMinutes.TabIndex = 75;
            this.lblMinutes.Text = "minutes";
            // 
            // fclsGENOptions_SMTPServer
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(410, 254);
            this.Controls.Add(this.lblMinutes);
            this.Controls.Add(this.nudTimeout);
            this.Controls.Add(this.lblTimeout);
            this.Controls.Add(this.chkServerRequiresAuthentication);
            this.Controls.Add(this.gpbLogonInformation);
            this.Controls.Add(this.txtPortNumber);
            this.Controls.Add(this.lblServer);
            this.Controls.Add(this.lblPortNumber);
            this.Controls.Add(this.txtServer);
            this.Controls.Add(this.txtAccountName);
            this.Controls.Add(this.lblAccountName);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fclsGENOptions_SMTPServer";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quick Stock - Outgoing Mail Server (SMTP) Properties";
            this.gpbLogonInformation.ResumeLayout(false);
            this.gpbLogonInformation.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudTimeout)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        #region Events
        private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (this.txtAccountName.Text.Length != 0)
            {
                if (m_lviItem.IsNew &&
                    clsConfiguration.Email_SMTPServers.Contains(new clsSMTPServer(this.txtAccountName.Text, "", -1, -1, false, "", "")))
                {
                    this.txtAccountName.Select();
                    m_ttToolTip.ToolTipTitle = "Invalid Account Name";
                    m_ttToolTip.Show("An email server with this name exists already. Change the account name in order to be able to proceed.",
                                     this.txtAccountName,
                                     0, this.txtAccountName.Size.Height,
                                     5000);
                }
                else
                {
                    m_lviItem.AccountName = this.txtAccountName.Text;
                    m_lviItem.Address = this.txtServer.Text;
                    m_lviItem.Port = int.Parse(this.txtPortNumber.Text);
                    m_lviItem.Timeout = (int)this.nudTimeout.Value;
                    m_lviItem.CredentialsRequired = this.chkServerRequiresAuthentication.Checked;
                    if (m_lviItem.CredentialsRequired)
                    {
                        m_lviItem.UserName = this.txtServerUserName.Text;
                        m_lviItem.Password = this.txtServerPassword.Text;
                    }
                    else
                    {
                        m_lviItem.UserName = "";
                        m_lviItem.Password = "";
                    }

                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            else
            {
                this.txtAccountName.Select();
                m_ttToolTip.ToolTipTitle = "Empty Account Name";
                m_ttToolTip.Show("The account name cannot be empty. Please type in a name that will be used to identify this e-mail account.",
                                 this.txtAccountName,
                                 0, this.txtAccountName.Size.Height,
                                 5000);
            }
        }
        
        private void chkServerRequiresAuthentication_CheckedChanged(object sender, EventArgs e)
        {
            this.txtServerPassword.Clear();
            this.txtServerUserName.Clear();

            if (chkServerRequiresAuthentication.Checked)
                this.gpbLogonInformation.Enabled = true;
            else
                this.gpbLogonInformation.Enabled = false;
        }

        private void txtAccountName_KeyDown(object sender, KeyEventArgs e)
        {
            // The balloon tip is visible for five seconds; if the user types any data before it disappears, collapse it ourselves.
            m_ttToolTip.Hide(txtPortNumber);
        }

        private void txtPortNumber_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            if (txtPortNumber.MaskFull)
            {
                m_ttToolTip.ToolTipTitle = "Input Rejected - Too Much Data";
                m_ttToolTip.Show("Only numbers in the 1 - 65535 range can be entered.",
                                 txtPortNumber,
                                 0, txtPortNumber.Size.Height,
                                 5000);
            }
            else if (e.Position == txtPortNumber.Mask.Length)
            {
                m_ttToolTip.ToolTipTitle = "Input Rejected - End of Field";
                m_ttToolTip.Show("The port number cannot be longer than 5 digits.",
                                 txtPortNumber,
                                 0, txtPortNumber.Size.Height,
                                 5000);
            }
            else
            {
                m_ttToolTip.ToolTipTitle = "Input Rejected";
                m_ttToolTip.Show("You can only add numeric characters (0-9) into this field.",
                                 txtPortNumber,
                                 0, txtPortNumber.Size.Height,
                                 5000);
            }
        }

        private void txtPortNumber_KeyDown(object sender, KeyEventArgs e)
        {
            // The balloon tip is visible for five seconds; if the user types any data before it disappears, collapse it ourselves.
            m_ttToolTip.Hide(txtPortNumber);
        }

        private void txtPortNumber_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(this.txtPortNumber.Text) > 65535)
                this.txtPortNumber.Text = (65535).ToString();
        }
        #endregion
    }
}
