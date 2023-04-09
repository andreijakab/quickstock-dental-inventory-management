using System;
using System.Collections;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows.Forms;
using System.Data;
using System.Web;
using System.Web.Mail;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsSIViewOrdRpt.
	/// </summary>
	public class fclsGENSendEmail : System.Windows.Forms.Form
	{
        private System.Windows.Forms.Button btnCancel;
        private IContainer components;
        private System.Windows.Forms.TextBox txtTo;
        private System.Windows.Forms.TextBox txtFrom;
        private System.Windows.Forms.TextBox txtSubject;
        private System.Windows.Forms.Label lblTo;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblSubject;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.TextBox txtAttachment;
        private System.Windows.Forms.TextBox txtBody;
        private System.Windows.Forms.Label lblAttachment;
        private System.Windows.Forms.Label lblAccount;
        private System.Windows.Forms.ComboBox cmbAccount;
        private Label lblPriority;
        private RadioButton rbtnPriority_High;
        private RadioButton rbtnPriority_Normal;
        private RadioButton rbtnPriority_Low;
        private StatusStrip ssStatusBar;
        private ToolStripProgressBar tsProgressBar;
        private ToolStripStatusLabel tsStatusLabel;
        private BackgroundWorker bwSendEmail;
		/// <summary>
		/// Required designer variable.
		/// </summary>
        public enum EmailType : int { Activation, Order, Tender, CanceledBackorder }

		private EmailType               m_etEmailType;
        private SupplierInformation     m_siSupplier;
        private Timer tmrStatusUpdate;
        private clsEmailer.EmailMessage m_emMessage;
        private bool m_blnOrderSent = false;

        public fclsGENSendEmail(EmailType etEmailType)
		{
            clsSMTPServer smtpServer;

			InitializeComponent();

            // Variable initialization
            m_etEmailType = etEmailType;
            			
            //
            // form init
            //
			// add configured SMTP servers to combo box
            foreach (Object obj in clsConfiguration.Email_SMTPServers)
            {
                smtpServer = (clsSMTPServer)obj;
                this.cmbAccount.Items.Add(clsUtilities.FormatSMTPServer_List(smtpServer.AccountName, smtpServer.Address));
            }
            this.cmbAccount.SelectedIndex = 0;

            // default result
            this.DialogResult = DialogResult.Cancel;
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
            this.components = new System.ComponentModel.Container();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnSend = new System.Windows.Forms.Button();
            this.txtTo = new System.Windows.Forms.TextBox();
            this.lblTo = new System.Windows.Forms.Label();
            this.lblFrom = new System.Windows.Forms.Label();
            this.txtFrom = new System.Windows.Forms.TextBox();
            this.lblSubject = new System.Windows.Forms.Label();
            this.txtSubject = new System.Windows.Forms.TextBox();
            this.lblAttachment = new System.Windows.Forms.Label();
            this.txtAttachment = new System.Windows.Forms.TextBox();
            this.txtBody = new System.Windows.Forms.TextBox();
            this.lblAccount = new System.Windows.Forms.Label();
            this.cmbAccount = new System.Windows.Forms.ComboBox();
            this.lblPriority = new System.Windows.Forms.Label();
            this.rbtnPriority_High = new System.Windows.Forms.RadioButton();
            this.rbtnPriority_Normal = new System.Windows.Forms.RadioButton();
            this.rbtnPriority_Low = new System.Windows.Forms.RadioButton();
            this.ssStatusBar = new System.Windows.Forms.StatusStrip();
            this.tsProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.tsStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.bwSendEmail = new System.ComponentModel.BackgroundWorker();
            this.tmrStatusUpdate = new System.Windows.Forms.Timer(this.components);
            this.ssStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(522, 406);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(96, 32);
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnSend
            // 
            this.btnSend.Location = new System.Drawing.Point(420, 406);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(96, 32);
            this.btnSend.TabIndex = 2;
            this.btnSend.Text = "Send";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // txtTo
            // 
            this.txtTo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtTo.Location = new System.Drawing.Point(92, 66);
            this.txtTo.Name = "txtTo";
            this.txtTo.Size = new System.Drawing.Size(526, 20);
            this.txtTo.TabIndex = 3;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblTo.Location = new System.Drawing.Point(64, 69);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(22, 14);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "To";
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblFrom.Location = new System.Drawing.Point(52, 42);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(34, 14);
            this.lblFrom.TabIndex = 6;
            this.lblFrom.Text = "From";
            // 
            // txtFrom
            // 
            this.txtFrom.Enabled = false;
            this.txtFrom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtFrom.Location = new System.Drawing.Point(92, 39);
            this.txtFrom.Name = "txtFrom";
            this.txtFrom.Size = new System.Drawing.Size(526, 20);
            this.txtFrom.TabIndex = 5;
            // 
            // lblSubject
            // 
            this.lblSubject.AutoSize = true;
            this.lblSubject.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSubject.Location = new System.Drawing.Point(37, 96);
            this.lblSubject.Name = "lblSubject";
            this.lblSubject.Size = new System.Drawing.Size(49, 14);
            this.lblSubject.TabIndex = 8;
            this.lblSubject.Text = "Subject";
            // 
            // txtSubject
            // 
            this.txtSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtSubject.Location = new System.Drawing.Point(92, 93);
            this.txtSubject.Name = "txtSubject";
            this.txtSubject.Size = new System.Drawing.Size(526, 20);
            this.txtSubject.TabIndex = 7;
            // 
            // lblAttachment
            // 
            this.lblAttachment.AutoSize = true;
            this.lblAttachment.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblAttachment.Location = new System.Drawing.Point(13, 123);
            this.lblAttachment.Name = "lblAttachment";
            this.lblAttachment.Size = new System.Drawing.Size(73, 14);
            this.lblAttachment.TabIndex = 10;
            this.lblAttachment.Text = "Attachment";
            // 
            // txtAttachment
            // 
            this.txtAttachment.Enabled = false;
            this.txtAttachment.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtAttachment.Location = new System.Drawing.Point(92, 120);
            this.txtAttachment.Name = "txtAttachment";
            this.txtAttachment.Size = new System.Drawing.Size(526, 20);
            this.txtAttachment.TabIndex = 9;
            // 
            // txtBody
            // 
            this.txtBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBody.Location = new System.Drawing.Point(16, 170);
            this.txtBody.Multiline = true;
            this.txtBody.Name = "txtBody";
            this.txtBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBody.Size = new System.Drawing.Size(602, 230);
            this.txtBody.TabIndex = 11;
            // 
            // lblAccount
            // 
            this.lblAccount.AutoSize = true;
            this.lblAccount.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblAccount.Location = new System.Drawing.Point(33, 15);
            this.lblAccount.Name = "lblAccount";
            this.lblAccount.Size = new System.Drawing.Size(53, 14);
            this.lblAccount.TabIndex = 14;
            this.lblAccount.Text = "Account";
            // 
            // cmbAccount
            // 
            this.cmbAccount.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAccount.Location = new System.Drawing.Point(92, 12);
            this.cmbAccount.Name = "cmbAccount";
            this.cmbAccount.Size = new System.Drawing.Size(526, 21);
            this.cmbAccount.TabIndex = 15;
            // 
            // lblPriority
            // 
            this.lblPriority.AutoSize = true;
            this.lblPriority.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPriority.Location = new System.Drawing.Point(42, 148);
            this.lblPriority.Name = "lblPriority";
            this.lblPriority.Size = new System.Drawing.Size(44, 14);
            this.lblPriority.TabIndex = 16;
            this.lblPriority.Text = "Priority";
            // 
            // rbtnPriority_High
            // 
            this.rbtnPriority_High.AutoSize = true;
            this.rbtnPriority_High.Location = new System.Drawing.Point(92, 147);
            this.rbtnPriority_High.Name = "rbtnPriority_High";
            this.rbtnPriority_High.Size = new System.Drawing.Size(47, 17);
            this.rbtnPriority_High.TabIndex = 17;
            this.rbtnPriority_High.Text = "High";
            this.rbtnPriority_High.UseVisualStyleBackColor = true;
            // 
            // rbtnPriority_Normal
            // 
            this.rbtnPriority_Normal.AutoSize = true;
            this.rbtnPriority_Normal.Checked = true;
            this.rbtnPriority_Normal.Location = new System.Drawing.Point(145, 147);
            this.rbtnPriority_Normal.Name = "rbtnPriority_Normal";
            this.rbtnPriority_Normal.Size = new System.Drawing.Size(58, 17);
            this.rbtnPriority_Normal.TabIndex = 18;
            this.rbtnPriority_Normal.TabStop = true;
            this.rbtnPriority_Normal.Text = "Normal";
            this.rbtnPriority_Normal.UseVisualStyleBackColor = true;
            // 
            // rbtnPriority_Low
            // 
            this.rbtnPriority_Low.AutoSize = true;
            this.rbtnPriority_Low.Location = new System.Drawing.Point(209, 147);
            this.rbtnPriority_Low.Name = "rbtnPriority_Low";
            this.rbtnPriority_Low.Size = new System.Drawing.Size(45, 17);
            this.rbtnPriority_Low.TabIndex = 19;
            this.rbtnPriority_Low.Text = "Low";
            this.rbtnPriority_Low.UseVisualStyleBackColor = true;
            // 
            // ssStatusBar
            // 
            this.ssStatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsProgressBar,
            this.tsStatusLabel});
            this.ssStatusBar.Location = new System.Drawing.Point(0, 448);
            this.ssStatusBar.Name = "ssStatusBar";
            this.ssStatusBar.Size = new System.Drawing.Size(630, 22);
            this.ssStatusBar.SizingGrip = false;
            this.ssStatusBar.TabIndex = 20;
            this.ssStatusBar.Text = "statusStrip1";
            // 
            // tsProgressBar
            // 
            this.tsProgressBar.Name = "tsProgressBar";
            this.tsProgressBar.Size = new System.Drawing.Size(100, 16);
            this.tsProgressBar.Step = 5;
            // 
            // tsStatusLabel
            // 
            this.tsStatusLabel.Name = "tsStatusLabel";
            this.tsStatusLabel.Size = new System.Drawing.Size(0, 17);
            // 
            // bwSendEmail
            // 
            this.bwSendEmail.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSendEmail_DoWork);
            this.bwSendEmail.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSendEmail_RunWorkerCompleted);
            // 
            // tmrStatusUpdate
            // 
            this.tmrStatusUpdate.Interval = 1000;
            this.tmrStatusUpdate.Tick += new System.EventHandler(this.tmrStatusUpdate_Tick);
            // 
            // fclsGENSendEmail
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(630, 470);
            this.Controls.Add(this.ssStatusBar);
            this.Controls.Add(this.rbtnPriority_Low);
            this.Controls.Add(this.rbtnPriority_Normal);
            this.Controls.Add(this.rbtnPriority_High);
            this.Controls.Add(this.lblPriority);
            this.Controls.Add(this.cmbAccount);
            this.Controls.Add(this.lblAccount);
            this.Controls.Add(this.txtBody);
            this.Controls.Add(this.txtAttachment);
            this.Controls.Add(this.txtSubject);
            this.Controls.Add(this.txtFrom);
            this.Controls.Add(this.txtTo);
            this.Controls.Add(this.lblAttachment);
            this.Controls.Add(this.lblSubject);
            this.Controls.Add(this.lblFrom);
            this.Controls.Add(this.lblTo);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.btnCancel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "fclsGENSendEmail";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Stock - Send Email";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fclsGENSendEmail_FormClosing);
            this.ssStatusBar.ResumeLayout(false);
            this.ssStatusBar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private void btnCancel_Click(object sender, EventArgs e)
        {
            bwSendEmail.Dispose();
            this.Close();
        }

        private void btnSend_Click(object sender, System.EventArgs e)
        {
            m_emMessage = new clsEmailer.EmailMessage();
            
            // set email message properties
            m_emMessage.SMTP = (clsSMTPServer)clsConfiguration.Email_SMTPServers[this.cmbAccount.SelectedIndex];
            m_emMessage.From_Address = clsConfiguration.DentalOffice_Email;
            m_emMessage.From_Name = clsConfiguration.DentalOffice_Name;
            m_emMessage.To = txtTo.Text;
            m_emMessage.Subject = txtSubject.Text;
            m_emMessage.Attachment = txtAttachment.Text;
            m_emMessage.Body = txtBody.Text;
            if (rbtnPriority_High.Checked)
                m_emMessage.Priority = System.Net.Mail.MailPriority.High;
            else if (rbtnPriority_Low.Checked)
                m_emMessage.Priority = System.Net.Mail.MailPriority.Low;
            else
                m_emMessage.Priority = System.Net.Mail.MailPriority.Normal;
            
            // start background work thread that will attempt to send message
            bwSendEmail.RunWorkerAsync();

            // set progress bar step based on the SMTP server's timeout value
            int intStep = (int) Math.Floor(100M/(m_emMessage.SMTP.Timeout*60));
            this.tsProgressBar.Step = intStep > 0 ? intStep: 1;
            
            // update status bar
            this.tmrStatusUpdate.Start();
            this.tsStatusLabel.Text = "Email sending in progress...";

            // disable this button
            this.btnCancel.Enabled = false;
            this.btnSend.Enabled = false;
                        
			/*MailMessage mailMsg = new MailMessage();
			mailMsg.From = "me@home.com";
			mailMsg.To = "drei222@gmail.com";
			mailMsg.Subject = "Test";
			mailMsg.BodyFormat = MailFormat.Text;
			mailMsg.Body = "Testing !";
			mailMsg.Priority = MailPriority.High;
			// Smtp configuration

			SmtpMail.SmtpServer = "smtp.gmail.com";
			// - smtp.gmail.com use smtp authentication

			mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpauthenticate", "1");
			mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendusername", "quickstock22@gmail.com");
			mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/sendpassword", "zlV6+L*0Ewkm5a_QZAORT+zfUj");
			// - smtp.gmail.com use port 465 or 587

			mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpserverport", "465");
			// - smtp.gmail.com use STARTTLS (some call this SSL)

			mailMsg.Fields.Add("http://schemas.microsoft.com/cdo/configuration/smtpusessl", "true");
			// try to send Mail

			try 
			{
				SmtpMail.Send(mailMsg);
				//return "";
			}
			catch (Exception ex) 
			{
				MessageBox.Show(ex.Message);
			}*/
		}

        private void fclsGENSendEmail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.DialogResult == DialogResult.Cancel &&
                bwSendEmail.IsBusy)
            {
                DialogResult dlg = MessageBox.Show("This window cannot be closed until the e-mail has finished sending.",
                                                   clsConfiguration.Internal_ApplicationName,
                                                   MessageBoxButtons.OK,
                                                   MessageBoxIcon.Information);
                e.Cancel = true;
            }
        }

        private void tmrStatusUpdate_Tick(object sender, EventArgs e)
        {
            if (this.tsProgressBar.Value == this.tsProgressBar.Maximum)
                this.tsProgressBar.Value = 0;

            this.tsProgressBar.PerformStep();
        }

        public void NewEmail(string strName, string strEmail, string strActivationFile)
		{
			this.Name = "Email Activation Request";
			this.txtTo.Text = clsUtilities.ACTIVATION_EMAIL;
			this.txtFrom.Text = strEmail;
			this.txtSubject.Text = "Activation Request";
			this.txtBody.Text = "Name: " + strName;
			this.txtAttachment.Text = strActivationFile;
		}

        public void NewEmail(SupplierInformation siSupplier, string strAttachment)
        {
            m_siSupplier = siSupplier;

            // general init
            this.txtTo.Text = siSupplier.Email;
            this.txtFrom.Text = clsUtilities.FormatEmail_Friendly(clsConfiguration.DentalOffice_Name,
                                                                  clsConfiguration.DentalOffice_Email);
            this.txtAttachment.Text = strAttachment;

            // type-specific form init
            switch (m_etEmailType)
            {
                case EmailType.CanceledBackorder:
                    this.txtSubject.Text = clsConfiguration.Email_Subject;
                    this.txtBody.Text = clsConfiguration.Email_Body;
                break;

                case EmailType.Order:
                    this.txtSubject.Text = clsConfiguration.Email_Subject;
                    this.txtBody.Text = clsConfiguration.Email_Body;
                break;

                case EmailType.Tender:
                    this.txtSubject.Text = clsConfiguration.Email_Subject;
                    this.txtBody.Text = clsConfiguration.Email_Body;
                break;
            }
        }

        #region BackgroundWorker
        private struct EmailSendingResult
        {
            public bool EmailSentSucessfully;
            public string CompletionMessage;
        }

        private void bwSendEmail_DoWork(object sender, DoWorkEventArgs e)
        {
            // create & init result struct
            EmailSendingResult esrResult = new EmailSendingResult();

            // attempt to send e-mail
            try
            {
                // send email message to emailer class
                clsEmailer.SendMessageWithAttachment(m_emMessage);

                // if we got to here, e-mail was sent successfully
                esrResult.EmailSentSucessfully = true;

                switch (m_etEmailType)
                {
                    case EmailType.Activation:
                        esrResult.CompletionMessage = "The activation request was e-mailed succesfully.";
                    break;

                    case EmailType.Order:
                        esrResult.CompletionMessage = "The order was e-mailed succesfully.";
                    break;

                    case EmailType.Tender:
                        esrResult.CompletionMessage = "The tender was e-mailed succesfully.";
                    break;

                    case EmailType.CanceledBackorder:
                        esrResult.CompletionMessage = "The cancelation was sent succesfully.";
                    break;
                }

            }
            catch (Exception ex)
            {
                esrResult.EmailSentSucessfully = false;
                esrResult.CompletionMessage = ex.Message;
            }

            // set result struct as background worker result object
            e.Result = esrResult;
            m_blnOrderSent = esrResult.EmailSentSucessfully;
            
        }
        private void SetStatus_btnSendbyEmail()
        {
            //fclsOIViewOrdRpt.SetOrderSentbyEmailStatus (m_blnOrderSent);
        }

        private void bwSendEmail_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            EmailSendingResult esrResult = (EmailSendingResult)e.Result;
            
            // update status bar to reflect the endin
            this.tsProgressBar.Value = 100;
            this.tmrStatusUpdate.Stop();

            if (esrResult.EmailSentSucessfully)
            {
                this.tsStatusLabel.Text = "Email sent successfully.";
                MessageBox.Show(esrResult.CompletionMessage,
                                clsConfiguration.Internal_ApplicationName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                this.tsStatusLabel.Text = "An error occured while sending the email.";
                MessageBox.Show(esrResult.CompletionMessage,
                                clsConfiguration.Internal_ApplicationName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Warning);

                // reset status bar
                this.tsStatusLabel.Text = "";
                this.tsProgressBar.Value = 0;
                
                // re-enable send button
                this.btnSend.Enabled = true;
                this.btnCancel.Enabled = true;
            }
        }
        #endregion


    }
}
