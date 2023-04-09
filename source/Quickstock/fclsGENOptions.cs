using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsGENOptions.
	/// </summary>
	public class fclsGENOptions : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnAdministratorMode;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.TabControl tbcOptions;
		private System.Windows.Forms.TabPage tbpGeneral;
		private System.Windows.Forms.TabPage tbpEmail;
		private System.Windows.Forms.TabPage tbpSecurity;
		private System.Windows.Forms.GroupBox gpbWorkYear;
		private System.Windows.Forms.ComboBox cmbWorkYear;
		private System.Windows.Forms.GroupBox gpbGeneral_Backup;
		private System.Windows.Forms.CheckBox ckbGeneral_BackupOrders;
		private System.Windows.Forms.GroupBox gpbLanguage;
		private System.Windows.Forms.RadioButton optSpanish;
		private System.Windows.Forms.RadioButton optFrench;
		private System.Windows.Forms.RadioButton optEnglish;
        private System.Windows.Forms.TabPage tbpDentalOfficeInformation;
        private System.Windows.Forms.Button btnHelp;
        private GroupBox gpbGeneral_OrderDefaults;
        private Label lblGeneral_DefaultSupplier;
        private ComboBox cmbGeneral_DefaultSupplier;
        private Label lblGeneral_DefaultUser;
        private ComboBox cmbGeneral_DefaultUser;
        private Label lblDentalOfficeInfo_City;
        private TextBox txtDentalOfficeInfo_Email;
        private TextBox txtDentalOfficeInfo_FaxNumber;
        private TextBox txtDentalOfficeInfo_Country;
        private TextBox txtDentalOfficeInfo_PostalCode;
        private Label lblDentalOfficeInfo_FaxNumber;
        private Label lblDentalOfficeInfo_Country;
        private Label lblDentalOfficeInfo_PostalCode;
        private TextBox txtDentalOfficeInfo_PhoneNumber;
        private TextBox txtDentalOfficeInfo_StateProvince;
        private TextBox txtDentalOfficeInfo_City;
        private TextBox txtDentalOfficeInfo_Address;
        private TextBox txtDentalOfficeInfo_Name;
        private Label lblDentalOfficeInfo_Email;
        private Label lblDentalOfficeInfo_PhoneNumber;
        private Label lblDentalOfficeInfo_StateProvince;
        private Label lblDentalOfficeInfo_Address;
        private GroupBox gpbEmail_OrderEmails;
        private TextBox txtEmail_OrderBody;
        private Label lblEmail_OrderBody;
        private TextBox txtEmail_OrderSubject;
        private Label lblEmail_OrderSubject;
        private GroupBox gpbEmail_OutgoingMailServerInfo;
        private Button btnEmail_AddSMTPServer;
        private Button btnEmail_RemoveSMTPServer;
        private Button btnEmail_EditSMTPServer;
        private Button btnEmail_SMTPDown;
        private Button btnEmail_SMTPUp;
        private ListView lsvEmail_SMTPServersList;
        private ColumnHeader colName;
        private ColumnHeader colSMTPServer;
        private GroupBox gpbSecurity_PasswordChange;
        private GroupBox gpbSecurity_ApplicationSecurity;
        private Button btnSecurity_ChangePassword;
        private TextBox txtSecurity_CurrentPassword;
        private Label lblSecurity_CurrentPassword;
        private TextBox txtSecurity_ConfirmNewPassword;
        private TextBox txtSecurity_NewPassword;
        private Label lblSecurity_ConfirmNewPassword;
        private Label lblSecurity_NewPassword;
        private Label lblSecurity_Employee;
        private ComboBox cmbSecurity_Employees;
        private CheckBox chkSecurity_EmployeeLoginRequired;
        private Label lblDentalOfficeInfo_Administrator;
        private ComboBox cmbDentalOfficeInfo_Administrator;
        private Label lblDentalOfficeInfo_Name;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;
		
        private bool                        m_blnAdministratorMode;
		private clsListViewColumnSorter     m_lvwSMTPServerColumnSorter;
        private DataTable                   m_dtaEmployees, m_dtaSupplier;
        private OleDbCommandBuilder         m_ocbEmployees;
		private OleDbConnection	            m_odcConnection;
        private OleDbDataAdapter            m_odaEmployees;
        private ToolTip                     m_ttToolTip;
        	
		public fclsGENOptions(OleDbConnection odcConnection)
		{
            OleDbDataAdapter odaSupplier;
            string strEmployee;

			InitializeComponent();

            m_blnAdministratorMode = false;
			m_odcConnection = odcConnection;
            m_ttToolTip = new ToolTip();

            // initialize listview sorter
            m_lvwSMTPServerColumnSorter = new clsListViewColumnSorter();
            this.lsvEmail_SMTPServersList.ListViewItemSorter = m_lvwSMTPServerColumnSorter;
            
            //
            // load database data
            //
			// load employee table
            m_odaEmployees = new OleDbDataAdapter("SELECT * FROM Employees ORDER BY FirstName, LastName", m_odcConnection);
            m_ocbEmployees = new OleDbCommandBuilder(m_odaEmployees);
			m_dtaEmployees = new DataTable();
			m_odaEmployees.Fill(m_dtaEmployees);
            foreach (DataRow dtrCurrentRow in m_dtaEmployees.Rows)
            {
                strEmployee = clsUtilities.FormatName_List(dtrCurrentRow["Title"].ToString(), dtrCurrentRow["FirstName"].ToString(), dtrCurrentRow["LastName"].ToString());
                this.cmbGeneral_DefaultUser.Items.Add(strEmployee);
                this.cmbDentalOfficeInfo_Administrator.Items.Add(strEmployee);
                this.cmbSecurity_Employees.Items.Add(strEmployee);
            }

			// load supplier table
			odaSupplier = new OleDbDataAdapter("SELECT * FROM Suppliers ORDER BY CompanyName", m_odcConnection);
			m_dtaSupplier = new DataTable();
			odaSupplier.Fill(m_dtaSupplier);
            foreach (DataRow dtrCurrentRow in m_dtaSupplier.Rows)
                this.cmbGeneral_DefaultSupplier.Items.Add(dtrCurrentRow["CompanyName"]);
			
            //
            // load configuration
            //
            // General
            this.ckbGeneral_BackupOrders.Checked = clsConfiguration.General_BackupOrders;
            this.cmbGeneral_DefaultUser.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(clsConfiguration.General_DefaultUserID,
                                                                                             m_dtaEmployees,
                                                                                             0);
            this.cmbGeneral_DefaultSupplier.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(clsConfiguration.General_DefaultSupplierID,
                                                                                                 m_dtaSupplier,
                                                                                                 0);

            // Dental Office Information
            this.txtDentalOfficeInfo_Name.Text = clsConfiguration.DentalOffice_Name;
            this.txtDentalOfficeInfo_Address.Text = clsConfiguration.DentalOffice_Address;
            this.txtDentalOfficeInfo_City.Text = clsConfiguration.DentalOffice_City;
            this.txtDentalOfficeInfo_PostalCode.Text = clsConfiguration.DentalOffice_PostalCode;
            this.txtDentalOfficeInfo_StateProvince.Text = clsConfiguration.DentalOffice_StateProvince;
            this.txtDentalOfficeInfo_Country.Text = clsConfiguration.DentalOffice_Country;
            this.txtDentalOfficeInfo_PhoneNumber.Text = clsConfiguration.DentalOffice_PhoneNr;
            this.txtDentalOfficeInfo_FaxNumber.Text = clsConfiguration.DentalOffice_FaxNr;
            this.txtDentalOfficeInfo_Email.Text = clsConfiguration.DentalOffice_Email;
            this.cmbDentalOfficeInfo_Administrator.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(clsConfiguration.DentalOffice_AdministratorUserID,
                                                                                                        m_dtaEmployees,
                                                                                                        0);

            // Email Settings
            this.txtEmail_OrderSubject.Text = clsConfiguration.Email_Subject;
            this.txtEmail_OrderBody.Text = clsConfiguration.Email_Body;
            
            clsSMTPListViewItem cslviItem;
            if (clsConfiguration.Email_SMTPServers != null)
            {
                foreach (Object obj in clsConfiguration.Email_SMTPServers)
                {
                    cslviItem = new clsSMTPListViewItem((clsSMTPServer) obj);
                    this.lsvEmail_SMTPServersList.Items.Add(cslviItem);
                }
            }

            // Security Settings
            this.chkSecurity_EmployeeLoginRequired.Checked = clsConfiguration.Security_EmployeeLoginRequired;

            // disable options that can only be changed by the administrator
            if (clsConfiguration.DentalOffice_AdministratorUserID == -1)
                this.EnableAdministratorFields(true);
            else
            {
                string strAdminPassword = m_dtaEmployees.Rows[this.cmbDentalOfficeInfo_Administrator.SelectedIndex]["UserPassword"].ToString();
                if (strAdminPassword.Length == 0)
                    this.EnableAdministratorFields(true);
                else
                    this.EnableAdministratorFields(false);
            }
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
            this.btnClose = new System.Windows.Forms.Button();
            this.btnAdministratorMode = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.tbcOptions = new System.Windows.Forms.TabControl();
            this.tbpGeneral = new System.Windows.Forms.TabPage();
            this.gpbGeneral_OrderDefaults = new System.Windows.Forms.GroupBox();
            this.lblGeneral_DefaultSupplier = new System.Windows.Forms.Label();
            this.cmbGeneral_DefaultSupplier = new System.Windows.Forms.ComboBox();
            this.lblGeneral_DefaultUser = new System.Windows.Forms.Label();
            this.cmbGeneral_DefaultUser = new System.Windows.Forms.ComboBox();
            this.gpbWorkYear = new System.Windows.Forms.GroupBox();
            this.cmbWorkYear = new System.Windows.Forms.ComboBox();
            this.gpbGeneral_Backup = new System.Windows.Forms.GroupBox();
            this.ckbGeneral_BackupOrders = new System.Windows.Forms.CheckBox();
            this.gpbLanguage = new System.Windows.Forms.GroupBox();
            this.optSpanish = new System.Windows.Forms.RadioButton();
            this.optFrench = new System.Windows.Forms.RadioButton();
            this.optEnglish = new System.Windows.Forms.RadioButton();
            this.tbpDentalOfficeInformation = new System.Windows.Forms.TabPage();
            this.lblDentalOfficeInfo_Administrator = new System.Windows.Forms.Label();
            this.cmbDentalOfficeInfo_Administrator = new System.Windows.Forms.ComboBox();
            this.lblDentalOfficeInfo_City = new System.Windows.Forms.Label();
            this.txtDentalOfficeInfo_Email = new System.Windows.Forms.TextBox();
            this.txtDentalOfficeInfo_FaxNumber = new System.Windows.Forms.TextBox();
            this.txtDentalOfficeInfo_Country = new System.Windows.Forms.TextBox();
            this.txtDentalOfficeInfo_PostalCode = new System.Windows.Forms.TextBox();
            this.lblDentalOfficeInfo_FaxNumber = new System.Windows.Forms.Label();
            this.lblDentalOfficeInfo_Country = new System.Windows.Forms.Label();
            this.lblDentalOfficeInfo_PostalCode = new System.Windows.Forms.Label();
            this.txtDentalOfficeInfo_PhoneNumber = new System.Windows.Forms.TextBox();
            this.txtDentalOfficeInfo_StateProvince = new System.Windows.Forms.TextBox();
            this.txtDentalOfficeInfo_City = new System.Windows.Forms.TextBox();
            this.txtDentalOfficeInfo_Address = new System.Windows.Forms.TextBox();
            this.txtDentalOfficeInfo_Name = new System.Windows.Forms.TextBox();
            this.lblDentalOfficeInfo_Email = new System.Windows.Forms.Label();
            this.lblDentalOfficeInfo_PhoneNumber = new System.Windows.Forms.Label();
            this.lblDentalOfficeInfo_StateProvince = new System.Windows.Forms.Label();
            this.lblDentalOfficeInfo_Address = new System.Windows.Forms.Label();
            this.lblDentalOfficeInfo_Name = new System.Windows.Forms.Label();
            this.tbpEmail = new System.Windows.Forms.TabPage();
            this.gpbEmail_OrderEmails = new System.Windows.Forms.GroupBox();
            this.txtEmail_OrderBody = new System.Windows.Forms.TextBox();
            this.lblEmail_OrderBody = new System.Windows.Forms.Label();
            this.txtEmail_OrderSubject = new System.Windows.Forms.TextBox();
            this.lblEmail_OrderSubject = new System.Windows.Forms.Label();
            this.gpbEmail_OutgoingMailServerInfo = new System.Windows.Forms.GroupBox();
            this.btnEmail_AddSMTPServer = new System.Windows.Forms.Button();
            this.btnEmail_RemoveSMTPServer = new System.Windows.Forms.Button();
            this.btnEmail_EditSMTPServer = new System.Windows.Forms.Button();
            this.btnEmail_SMTPDown = new System.Windows.Forms.Button();
            this.btnEmail_SMTPUp = new System.Windows.Forms.Button();
            this.lsvEmail_SMTPServersList = new System.Windows.Forms.ListView();
            this.colName = new System.Windows.Forms.ColumnHeader();
            this.colSMTPServer = new System.Windows.Forms.ColumnHeader();
            this.tbpSecurity = new System.Windows.Forms.TabPage();
            this.gpbSecurity_ApplicationSecurity = new System.Windows.Forms.GroupBox();
            this.chkSecurity_EmployeeLoginRequired = new System.Windows.Forms.CheckBox();
            this.gpbSecurity_PasswordChange = new System.Windows.Forms.GroupBox();
            this.lblSecurity_Employee = new System.Windows.Forms.Label();
            this.cmbSecurity_Employees = new System.Windows.Forms.ComboBox();
            this.btnSecurity_ChangePassword = new System.Windows.Forms.Button();
            this.txtSecurity_CurrentPassword = new System.Windows.Forms.TextBox();
            this.lblSecurity_CurrentPassword = new System.Windows.Forms.Label();
            this.txtSecurity_ConfirmNewPassword = new System.Windows.Forms.TextBox();
            this.txtSecurity_NewPassword = new System.Windows.Forms.TextBox();
            this.lblSecurity_ConfirmNewPassword = new System.Windows.Forms.Label();
            this.lblSecurity_NewPassword = new System.Windows.Forms.Label();
            this.tbcOptions.SuspendLayout();
            this.tbpGeneral.SuspendLayout();
            this.gpbGeneral_OrderDefaults.SuspendLayout();
            this.gpbWorkYear.SuspendLayout();
            this.gpbGeneral_Backup.SuspendLayout();
            this.gpbLanguage.SuspendLayout();
            this.tbpDentalOfficeInformation.SuspendLayout();
            this.tbpEmail.SuspendLayout();
            this.gpbEmail_OrderEmails.SuspendLayout();
            this.gpbEmail_OutgoingMailServerInfo.SuspendLayout();
            this.tbpSecurity.SuspendLayout();
            this.gpbSecurity_ApplicationSecurity.SuspendLayout();
            this.gpbSecurity_PasswordChange.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(306, 367);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 32);
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnAdministratorMode
            // 
            this.btnAdministratorMode.Location = new System.Drawing.Point(8, 367);
            this.btnAdministratorMode.Name = "btnAdministratorMode";
            this.btnAdministratorMode.Size = new System.Drawing.Size(120, 32);
            this.btnAdministratorMode.TabIndex = 12;
            this.btnAdministratorMode.Text = "Administrator Mode";
            this.btnAdministratorMode.Click += new System.EventHandler(this.btnAdministratorMode_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(408, 367);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(96, 32);
            this.btnHelp.TabIndex = 14;
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // tbcOptions
            // 
            this.tbcOptions.Controls.Add(this.tbpGeneral);
            this.tbcOptions.Controls.Add(this.tbpDentalOfficeInformation);
            this.tbcOptions.Controls.Add(this.tbpEmail);
            this.tbcOptions.Controls.Add(this.tbpSecurity);
            this.tbcOptions.Location = new System.Drawing.Point(8, 8);
            this.tbcOptions.Name = "tbcOptions";
            this.tbcOptions.SelectedIndex = 0;
            this.tbcOptions.Size = new System.Drawing.Size(496, 353);
            this.tbcOptions.TabIndex = 0;
            // 
            // tbpGeneral
            // 
            this.tbpGeneral.Controls.Add(this.gpbGeneral_OrderDefaults);
            this.tbpGeneral.Controls.Add(this.gpbWorkYear);
            this.tbpGeneral.Controls.Add(this.gpbGeneral_Backup);
            this.tbpGeneral.Controls.Add(this.gpbLanguage);
            this.tbpGeneral.Location = new System.Drawing.Point(4, 22);
            this.tbpGeneral.Name = "tbpGeneral";
            this.tbpGeneral.Size = new System.Drawing.Size(488, 327);
            this.tbpGeneral.TabIndex = 0;
            this.tbpGeneral.Text = "General";
            this.tbpGeneral.UseVisualStyleBackColor = true;
            // 
            // gpbGeneral_OrderDefaults
            // 
            this.gpbGeneral_OrderDefaults.Controls.Add(this.lblGeneral_DefaultSupplier);
            this.gpbGeneral_OrderDefaults.Controls.Add(this.cmbGeneral_DefaultSupplier);
            this.gpbGeneral_OrderDefaults.Controls.Add(this.lblGeneral_DefaultUser);
            this.gpbGeneral_OrderDefaults.Controls.Add(this.cmbGeneral_DefaultUser);
            this.gpbGeneral_OrderDefaults.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbGeneral_OrderDefaults.Location = new System.Drawing.Point(3, 57);
            this.gpbGeneral_OrderDefaults.Name = "gpbGeneral_OrderDefaults";
            this.gpbGeneral_OrderDefaults.Size = new System.Drawing.Size(482, 90);
            this.gpbGeneral_OrderDefaults.TabIndex = 6;
            this.gpbGeneral_OrderDefaults.TabStop = false;
            this.gpbGeneral_OrderDefaults.Text = "Order Defaults";
            // 
            // lblGeneral_DefaultSupplier
            // 
            this.lblGeneral_DefaultSupplier.AutoSize = true;
            this.lblGeneral_DefaultSupplier.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblGeneral_DefaultSupplier.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblGeneral_DefaultSupplier.Location = new System.Drawing.Point(6, 57);
            this.lblGeneral_DefaultSupplier.Name = "lblGeneral_DefaultSupplier";
            this.lblGeneral_DefaultSupplier.Size = new System.Drawing.Size(93, 14);
            this.lblGeneral_DefaultSupplier.TabIndex = 110;
            this.lblGeneral_DefaultSupplier.Text = "Default Supplier";
            this.lblGeneral_DefaultSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbGeneral_DefaultSupplier
            // 
            this.cmbGeneral_DefaultSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGeneral_DefaultSupplier.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbGeneral_DefaultSupplier.ItemHeight = 13;
            this.cmbGeneral_DefaultSupplier.Location = new System.Drawing.Point(133, 54);
            this.cmbGeneral_DefaultSupplier.Name = "cmbGeneral_DefaultSupplier";
            this.cmbGeneral_DefaultSupplier.Size = new System.Drawing.Size(343, 21);
            this.cmbGeneral_DefaultSupplier.TabIndex = 1;
            // 
            // lblGeneral_DefaultUser
            // 
            this.lblGeneral_DefaultUser.AutoSize = true;
            this.lblGeneral_DefaultUser.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblGeneral_DefaultUser.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblGeneral_DefaultUser.Location = new System.Drawing.Point(6, 27);
            this.lblGeneral_DefaultUser.Name = "lblGeneral_DefaultUser";
            this.lblGeneral_DefaultUser.Size = new System.Drawing.Size(74, 14);
            this.lblGeneral_DefaultUser.TabIndex = 106;
            this.lblGeneral_DefaultUser.Text = "Default User";
            this.lblGeneral_DefaultUser.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbGeneral_DefaultUser
            // 
            this.cmbGeneral_DefaultUser.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbGeneral_DefaultUser.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbGeneral_DefaultUser.ItemHeight = 13;
            this.cmbGeneral_DefaultUser.Location = new System.Drawing.Point(133, 24);
            this.cmbGeneral_DefaultUser.Name = "cmbGeneral_DefaultUser";
            this.cmbGeneral_DefaultUser.Size = new System.Drawing.Size(343, 21);
            this.cmbGeneral_DefaultUser.TabIndex = 0;
            // 
            // gpbWorkYear
            // 
            this.gpbWorkYear.Controls.Add(this.cmbWorkYear);
            this.gpbWorkYear.Location = new System.Drawing.Point(3, 260);
            this.gpbWorkYear.Name = "gpbWorkYear";
            this.gpbWorkYear.Size = new System.Drawing.Size(104, 48);
            this.gpbWorkYear.TabIndex = 5;
            this.gpbWorkYear.TabStop = false;
            this.gpbWorkYear.Text = "Working Year";
            this.gpbWorkYear.Visible = false;
            // 
            // cmbWorkYear
            // 
            this.cmbWorkYear.ItemHeight = 13;
            this.cmbWorkYear.Location = new System.Drawing.Point(16, 18);
            this.cmbWorkYear.Name = "cmbWorkYear";
            this.cmbWorkYear.Size = new System.Drawing.Size(64, 21);
            this.cmbWorkYear.TabIndex = 0;
            this.cmbWorkYear.Visible = false;
            // 
            // gpbGeneral_Backup
            // 
            this.gpbGeneral_Backup.Controls.Add(this.ckbGeneral_BackupOrders);
            this.gpbGeneral_Backup.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbGeneral_Backup.Location = new System.Drawing.Point(3, 3);
            this.gpbGeneral_Backup.Name = "gpbGeneral_Backup";
            this.gpbGeneral_Backup.Size = new System.Drawing.Size(482, 48);
            this.gpbGeneral_Backup.TabIndex = 4;
            this.gpbGeneral_Backup.TabStop = false;
            this.gpbGeneral_Backup.Text = "Backup";
            // 
            // ckbGeneral_BackupOrders
            // 
            this.ckbGeneral_BackupOrders.AutoSize = true;
            this.ckbGeneral_BackupOrders.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbGeneral_BackupOrders.Location = new System.Drawing.Point(172, 18);
            this.ckbGeneral_BackupOrders.Name = "ckbGeneral_BackupOrders";
            this.ckbGeneral_BackupOrders.Size = new System.Drawing.Size(139, 18);
            this.ckbGeneral_BackupOrders.TabIndex = 0;
            this.ckbGeneral_BackupOrders.Text = "Backup Sent Ordrers";
            // 
            // gpbLanguage
            // 
            this.gpbLanguage.Controls.Add(this.optSpanish);
            this.gpbLanguage.Controls.Add(this.optFrench);
            this.gpbLanguage.Controls.Add(this.optEnglish);
            this.gpbLanguage.Location = new System.Drawing.Point(3, 198);
            this.gpbLanguage.Name = "gpbLanguage";
            this.gpbLanguage.Size = new System.Drawing.Size(482, 56);
            this.gpbLanguage.TabIndex = 3;
            this.gpbLanguage.TabStop = false;
            this.gpbLanguage.Text = "Language";
            this.gpbLanguage.Visible = false;
            // 
            // optSpanish
            // 
            this.optSpanish.Enabled = false;
            this.optSpanish.Location = new System.Drawing.Point(376, 24);
            this.optSpanish.Name = "optSpanish";
            this.optSpanish.Size = new System.Drawing.Size(72, 24);
            this.optSpanish.TabIndex = 2;
            this.optSpanish.Text = "Español";
            // 
            // optFrench
            // 
            this.optFrench.Enabled = false;
            this.optFrench.Location = new System.Drawing.Point(184, 24);
            this.optFrench.Name = "optFrench";
            this.optFrench.Size = new System.Drawing.Size(72, 24);
            this.optFrench.TabIndex = 1;
            this.optFrench.Text = "Français";
            // 
            // optEnglish
            // 
            this.optEnglish.Checked = true;
            this.optEnglish.Location = new System.Drawing.Point(8, 24);
            this.optEnglish.Name = "optEnglish";
            this.optEnglish.Size = new System.Drawing.Size(64, 24);
            this.optEnglish.TabIndex = 0;
            this.optEnglish.TabStop = true;
            this.optEnglish.Text = "English";
            // 
            // tbpDentalOfficeInformation
            // 
            this.tbpDentalOfficeInformation.Controls.Add(this.lblDentalOfficeInfo_Administrator);
            this.tbpDentalOfficeInformation.Controls.Add(this.cmbDentalOfficeInfo_Administrator);
            this.tbpDentalOfficeInformation.Controls.Add(this.lblDentalOfficeInfo_City);
            this.tbpDentalOfficeInformation.Controls.Add(this.txtDentalOfficeInfo_Email);
            this.tbpDentalOfficeInformation.Controls.Add(this.txtDentalOfficeInfo_FaxNumber);
            this.tbpDentalOfficeInformation.Controls.Add(this.txtDentalOfficeInfo_Country);
            this.tbpDentalOfficeInformation.Controls.Add(this.txtDentalOfficeInfo_PostalCode);
            this.tbpDentalOfficeInformation.Controls.Add(this.lblDentalOfficeInfo_FaxNumber);
            this.tbpDentalOfficeInformation.Controls.Add(this.lblDentalOfficeInfo_Country);
            this.tbpDentalOfficeInformation.Controls.Add(this.lblDentalOfficeInfo_PostalCode);
            this.tbpDentalOfficeInformation.Controls.Add(this.txtDentalOfficeInfo_PhoneNumber);
            this.tbpDentalOfficeInformation.Controls.Add(this.txtDentalOfficeInfo_StateProvince);
            this.tbpDentalOfficeInformation.Controls.Add(this.txtDentalOfficeInfo_City);
            this.tbpDentalOfficeInformation.Controls.Add(this.txtDentalOfficeInfo_Address);
            this.tbpDentalOfficeInformation.Controls.Add(this.txtDentalOfficeInfo_Name);
            this.tbpDentalOfficeInformation.Controls.Add(this.lblDentalOfficeInfo_Email);
            this.tbpDentalOfficeInformation.Controls.Add(this.lblDentalOfficeInfo_PhoneNumber);
            this.tbpDentalOfficeInformation.Controls.Add(this.lblDentalOfficeInfo_StateProvince);
            this.tbpDentalOfficeInformation.Controls.Add(this.lblDentalOfficeInfo_Address);
            this.tbpDentalOfficeInformation.Controls.Add(this.lblDentalOfficeInfo_Name);
            this.tbpDentalOfficeInformation.Location = new System.Drawing.Point(4, 22);
            this.tbpDentalOfficeInformation.Name = "tbpDentalOfficeInformation";
            this.tbpDentalOfficeInformation.Size = new System.Drawing.Size(488, 327);
            this.tbpDentalOfficeInformation.TabIndex = 3;
            this.tbpDentalOfficeInformation.Text = "Dental Office Information";
            this.tbpDentalOfficeInformation.UseVisualStyleBackColor = true;
            // 
            // lblDentalOfficeInfo_Administrator
            // 
            this.lblDentalOfficeInfo_Administrator.AutoSize = true;
            this.lblDentalOfficeInfo_Administrator.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDentalOfficeInfo_Administrator.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDentalOfficeInfo_Administrator.Location = new System.Drawing.Point(3, 164);
            this.lblDentalOfficeInfo_Administrator.Name = "lblDentalOfficeInfo_Administrator";
            this.lblDentalOfficeInfo_Administrator.Size = new System.Drawing.Size(79, 14);
            this.lblDentalOfficeInfo_Administrator.TabIndex = 122;
            this.lblDentalOfficeInfo_Administrator.Text = "Administrator";
            this.lblDentalOfficeInfo_Administrator.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbDentalOfficeInfo_Administrator
            // 
            this.cmbDentalOfficeInfo_Administrator.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDentalOfficeInfo_Administrator.ItemHeight = 13;
            this.cmbDentalOfficeInfo_Administrator.Location = new System.Drawing.Point(104, 161);
            this.cmbDentalOfficeInfo_Administrator.Name = "cmbDentalOfficeInfo_Administrator";
            this.cmbDentalOfficeInfo_Administrator.Size = new System.Drawing.Size(381, 21);
            this.cmbDentalOfficeInfo_Administrator.TabIndex = 9;
            // 
            // lblDentalOfficeInfo_City
            // 
            this.lblDentalOfficeInfo_City.AutoSize = true;
            this.lblDentalOfficeInfo_City.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDentalOfficeInfo_City.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDentalOfficeInfo_City.Location = new System.Drawing.Point(3, 86);
            this.lblDentalOfficeInfo_City.Name = "lblDentalOfficeInfo_City";
            this.lblDentalOfficeInfo_City.Size = new System.Drawing.Size(27, 14);
            this.lblDentalOfficeInfo_City.TabIndex = 120;
            this.lblDentalOfficeInfo_City.Text = "City";
            this.lblDentalOfficeInfo_City.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDentalOfficeInfo_Email
            // 
            this.txtDentalOfficeInfo_Email.Location = new System.Drawing.Point(104, 135);
            this.txtDentalOfficeInfo_Email.MaxLength = 255;
            this.txtDentalOfficeInfo_Email.Name = "txtDentalOfficeInfo_Email";
            this.txtDentalOfficeInfo_Email.Size = new System.Drawing.Size(381, 20);
            this.txtDentalOfficeInfo_Email.TabIndex = 8;
            // 
            // txtDentalOfficeInfo_FaxNumber
            // 
            this.txtDentalOfficeInfo_FaxNumber.Location = new System.Drawing.Point(339, 109);
            this.txtDentalOfficeInfo_FaxNumber.MaxLength = 255;
            this.txtDentalOfficeInfo_FaxNumber.Name = "txtDentalOfficeInfo_FaxNumber";
            this.txtDentalOfficeInfo_FaxNumber.Size = new System.Drawing.Size(146, 20);
            this.txtDentalOfficeInfo_FaxNumber.TabIndex = 7;
            // 
            // txtDentalOfficeInfo_Country
            // 
            this.txtDentalOfficeInfo_Country.Location = new System.Drawing.Point(339, 83);
            this.txtDentalOfficeInfo_Country.MaxLength = 255;
            this.txtDentalOfficeInfo_Country.Name = "txtDentalOfficeInfo_Country";
            this.txtDentalOfficeInfo_Country.Size = new System.Drawing.Size(146, 20);
            this.txtDentalOfficeInfo_Country.TabIndex = 5;
            // 
            // txtDentalOfficeInfo_PostalCode
            // 
            this.txtDentalOfficeInfo_PostalCode.Location = new System.Drawing.Point(340, 57);
            this.txtDentalOfficeInfo_PostalCode.MaxLength = 255;
            this.txtDentalOfficeInfo_PostalCode.Name = "txtDentalOfficeInfo_PostalCode";
            this.txtDentalOfficeInfo_PostalCode.Size = new System.Drawing.Size(145, 20);
            this.txtDentalOfficeInfo_PostalCode.TabIndex = 3;
            // 
            // lblDentalOfficeInfo_FaxNumber
            // 
            this.lblDentalOfficeInfo_FaxNumber.AutoSize = true;
            this.lblDentalOfficeInfo_FaxNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDentalOfficeInfo_FaxNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDentalOfficeInfo_FaxNumber.Location = new System.Drawing.Point(261, 112);
            this.lblDentalOfficeInfo_FaxNumber.Name = "lblDentalOfficeInfo_FaxNumber";
            this.lblDentalOfficeInfo_FaxNumber.Size = new System.Drawing.Size(72, 14);
            this.lblDentalOfficeInfo_FaxNumber.TabIndex = 119;
            this.lblDentalOfficeInfo_FaxNumber.Text = "Fax Number";
            this.lblDentalOfficeInfo_FaxNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDentalOfficeInfo_Country
            // 
            this.lblDentalOfficeInfo_Country.AutoSize = true;
            this.lblDentalOfficeInfo_Country.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDentalOfficeInfo_Country.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDentalOfficeInfo_Country.Location = new System.Drawing.Point(262, 86);
            this.lblDentalOfficeInfo_Country.Name = "lblDentalOfficeInfo_Country";
            this.lblDentalOfficeInfo_Country.Size = new System.Drawing.Size(50, 14);
            this.lblDentalOfficeInfo_Country.TabIndex = 118;
            this.lblDentalOfficeInfo_Country.Text = "Country";
            this.lblDentalOfficeInfo_Country.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDentalOfficeInfo_PostalCode
            // 
            this.lblDentalOfficeInfo_PostalCode.AutoSize = true;
            this.lblDentalOfficeInfo_PostalCode.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDentalOfficeInfo_PostalCode.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDentalOfficeInfo_PostalCode.Location = new System.Drawing.Point(262, 60);
            this.lblDentalOfficeInfo_PostalCode.Name = "lblDentalOfficeInfo_PostalCode";
            this.lblDentalOfficeInfo_PostalCode.Size = new System.Drawing.Size(71, 14);
            this.lblDentalOfficeInfo_PostalCode.TabIndex = 117;
            this.lblDentalOfficeInfo_PostalCode.Text = "Postal Code";
            this.lblDentalOfficeInfo_PostalCode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txtDentalOfficeInfo_PhoneNumber
            // 
            this.txtDentalOfficeInfo_PhoneNumber.Location = new System.Drawing.Point(104, 109);
            this.txtDentalOfficeInfo_PhoneNumber.MaxLength = 255;
            this.txtDentalOfficeInfo_PhoneNumber.Name = "txtDentalOfficeInfo_PhoneNumber";
            this.txtDentalOfficeInfo_PhoneNumber.Size = new System.Drawing.Size(145, 20);
            this.txtDentalOfficeInfo_PhoneNumber.TabIndex = 6;
            // 
            // txtDentalOfficeInfo_StateProvince
            // 
            this.txtDentalOfficeInfo_StateProvince.Location = new System.Drawing.Point(104, 57);
            this.txtDentalOfficeInfo_StateProvince.MaxLength = 255;
            this.txtDentalOfficeInfo_StateProvince.Name = "txtDentalOfficeInfo_StateProvince";
            this.txtDentalOfficeInfo_StateProvince.Size = new System.Drawing.Size(145, 20);
            this.txtDentalOfficeInfo_StateProvince.TabIndex = 2;
            // 
            // txtDentalOfficeInfo_City
            // 
            this.txtDentalOfficeInfo_City.Location = new System.Drawing.Point(104, 83);
            this.txtDentalOfficeInfo_City.MaxLength = 255;
            this.txtDentalOfficeInfo_City.Name = "txtDentalOfficeInfo_City";
            this.txtDentalOfficeInfo_City.Size = new System.Drawing.Size(145, 20);
            this.txtDentalOfficeInfo_City.TabIndex = 4;
            // 
            // txtDentalOfficeInfo_Address
            // 
            this.txtDentalOfficeInfo_Address.Location = new System.Drawing.Point(104, 31);
            this.txtDentalOfficeInfo_Address.MaxLength = 255;
            this.txtDentalOfficeInfo_Address.Name = "txtDentalOfficeInfo_Address";
            this.txtDentalOfficeInfo_Address.Size = new System.Drawing.Size(381, 20);
            this.txtDentalOfficeInfo_Address.TabIndex = 1;
            // 
            // txtDentalOfficeInfo_Name
            // 
            this.txtDentalOfficeInfo_Name.Location = new System.Drawing.Point(104, 5);
            this.txtDentalOfficeInfo_Name.MaxLength = 255;
            this.txtDentalOfficeInfo_Name.Name = "txtDentalOfficeInfo_Name";
            this.txtDentalOfficeInfo_Name.Size = new System.Drawing.Size(381, 20);
            this.txtDentalOfficeInfo_Name.TabIndex = 0;
            // 
            // lblDentalOfficeInfo_Email
            // 
            this.lblDentalOfficeInfo_Email.AutoSize = true;
            this.lblDentalOfficeInfo_Email.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDentalOfficeInfo_Email.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDentalOfficeInfo_Email.Location = new System.Drawing.Point(3, 138);
            this.lblDentalOfficeInfo_Email.Name = "lblDentalOfficeInfo_Email";
            this.lblDentalOfficeInfo_Email.Size = new System.Drawing.Size(34, 14);
            this.lblDentalOfficeInfo_Email.TabIndex = 116;
            this.lblDentalOfficeInfo_Email.Text = "Email";
            this.lblDentalOfficeInfo_Email.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDentalOfficeInfo_PhoneNumber
            // 
            this.lblDentalOfficeInfo_PhoneNumber.AutoSize = true;
            this.lblDentalOfficeInfo_PhoneNumber.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDentalOfficeInfo_PhoneNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDentalOfficeInfo_PhoneNumber.Location = new System.Drawing.Point(3, 112);
            this.lblDentalOfficeInfo_PhoneNumber.Name = "lblDentalOfficeInfo_PhoneNumber";
            this.lblDentalOfficeInfo_PhoneNumber.Size = new System.Drawing.Size(89, 14);
            this.lblDentalOfficeInfo_PhoneNumber.TabIndex = 115;
            this.lblDentalOfficeInfo_PhoneNumber.Text = "Phone Number";
            this.lblDentalOfficeInfo_PhoneNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDentalOfficeInfo_StateProvince
            // 
            this.lblDentalOfficeInfo_StateProvince.AutoSize = true;
            this.lblDentalOfficeInfo_StateProvince.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDentalOfficeInfo_StateProvince.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDentalOfficeInfo_StateProvince.Location = new System.Drawing.Point(3, 60);
            this.lblDentalOfficeInfo_StateProvince.Name = "lblDentalOfficeInfo_StateProvince";
            this.lblDentalOfficeInfo_StateProvince.Size = new System.Drawing.Size(96, 14);
            this.lblDentalOfficeInfo_StateProvince.TabIndex = 114;
            this.lblDentalOfficeInfo_StateProvince.Text = "State / Province";
            this.lblDentalOfficeInfo_StateProvince.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDentalOfficeInfo_Address
            // 
            this.lblDentalOfficeInfo_Address.AutoSize = true;
            this.lblDentalOfficeInfo_Address.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDentalOfficeInfo_Address.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDentalOfficeInfo_Address.Location = new System.Drawing.Point(3, 34);
            this.lblDentalOfficeInfo_Address.Name = "lblDentalOfficeInfo_Address";
            this.lblDentalOfficeInfo_Address.Size = new System.Drawing.Size(50, 14);
            this.lblDentalOfficeInfo_Address.TabIndex = 113;
            this.lblDentalOfficeInfo_Address.Text = "Address";
            this.lblDentalOfficeInfo_Address.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblDentalOfficeInfo_Name
            // 
            this.lblDentalOfficeInfo_Name.AutoSize = true;
            this.lblDentalOfficeInfo_Name.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDentalOfficeInfo_Name.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDentalOfficeInfo_Name.Location = new System.Drawing.Point(3, 8);
            this.lblDentalOfficeInfo_Name.Name = "lblDentalOfficeInfo_Name";
            this.lblDentalOfficeInfo_Name.Size = new System.Drawing.Size(38, 14);
            this.lblDentalOfficeInfo_Name.TabIndex = 112;
            this.lblDentalOfficeInfo_Name.Text = "Name";
            this.lblDentalOfficeInfo_Name.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tbpEmail
            // 
            this.tbpEmail.Controls.Add(this.gpbEmail_OrderEmails);
            this.tbpEmail.Controls.Add(this.gpbEmail_OutgoingMailServerInfo);
            this.tbpEmail.Location = new System.Drawing.Point(4, 22);
            this.tbpEmail.Name = "tbpEmail";
            this.tbpEmail.Size = new System.Drawing.Size(488, 327);
            this.tbpEmail.TabIndex = 1;
            this.tbpEmail.Text = "Email Settings";
            this.tbpEmail.UseVisualStyleBackColor = true;
            // 
            // gpbEmail_OrderEmails
            // 
            this.gpbEmail_OrderEmails.Controls.Add(this.txtEmail_OrderBody);
            this.gpbEmail_OrderEmails.Controls.Add(this.lblEmail_OrderBody);
            this.gpbEmail_OrderEmails.Controls.Add(this.txtEmail_OrderSubject);
            this.gpbEmail_OrderEmails.Controls.Add(this.lblEmail_OrderSubject);
            this.gpbEmail_OrderEmails.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbEmail_OrderEmails.Location = new System.Drawing.Point(3, 3);
            this.gpbEmail_OrderEmails.Name = "gpbEmail_OrderEmails";
            this.gpbEmail_OrderEmails.Size = new System.Drawing.Size(482, 152);
            this.gpbEmail_OrderEmails.TabIndex = 0;
            this.gpbEmail_OrderEmails.TabStop = false;
            this.gpbEmail_OrderEmails.Text = "Order Emails";
            // 
            // txtEmail_OrderBody
            // 
            this.txtEmail_OrderBody.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtEmail_OrderBody.Location = new System.Drawing.Point(69, 45);
            this.txtEmail_OrderBody.Multiline = true;
            this.txtEmail_OrderBody.Name = "txtEmail_OrderBody";
            this.txtEmail_OrderBody.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtEmail_OrderBody.Size = new System.Drawing.Size(407, 101);
            this.txtEmail_OrderBody.TabIndex = 1;
            // 
            // lblEmail_OrderBody
            // 
            this.lblEmail_OrderBody.AutoSize = true;
            this.lblEmail_OrderBody.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail_OrderBody.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEmail_OrderBody.Location = new System.Drawing.Point(5, 45);
            this.lblEmail_OrderBody.Name = "lblEmail_OrderBody";
            this.lblEmail_OrderBody.Size = new System.Drawing.Size(53, 14);
            this.lblEmail_OrderBody.TabIndex = 27;
            this.lblEmail_OrderBody.Text = "Message";
            // 
            // txtEmail_OrderSubject
            // 
            this.txtEmail_OrderSubject.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtEmail_OrderSubject.Location = new System.Drawing.Point(69, 19);
            this.txtEmail_OrderSubject.Name = "txtEmail_OrderSubject";
            this.txtEmail_OrderSubject.Size = new System.Drawing.Size(407, 20);
            this.txtEmail_OrderSubject.TabIndex = 0;
            // 
            // lblEmail_OrderSubject
            // 
            this.lblEmail_OrderSubject.AutoSize = true;
            this.lblEmail_OrderSubject.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmail_OrderSubject.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblEmail_OrderSubject.Location = new System.Drawing.Point(5, 23);
            this.lblEmail_OrderSubject.Name = "lblEmail_OrderSubject";
            this.lblEmail_OrderSubject.Size = new System.Drawing.Size(49, 14);
            this.lblEmail_OrderSubject.TabIndex = 25;
            this.lblEmail_OrderSubject.Text = "Subject";
            // 
            // gpbEmail_OutgoingMailServerInfo
            // 
            this.gpbEmail_OutgoingMailServerInfo.Controls.Add(this.btnEmail_AddSMTPServer);
            this.gpbEmail_OutgoingMailServerInfo.Controls.Add(this.btnEmail_RemoveSMTPServer);
            this.gpbEmail_OutgoingMailServerInfo.Controls.Add(this.btnEmail_EditSMTPServer);
            this.gpbEmail_OutgoingMailServerInfo.Controls.Add(this.btnEmail_SMTPDown);
            this.gpbEmail_OutgoingMailServerInfo.Controls.Add(this.btnEmail_SMTPUp);
            this.gpbEmail_OutgoingMailServerInfo.Controls.Add(this.lsvEmail_SMTPServersList);
            this.gpbEmail_OutgoingMailServerInfo.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpbEmail_OutgoingMailServerInfo.Location = new System.Drawing.Point(3, 161);
            this.gpbEmail_OutgoingMailServerInfo.Name = "gpbEmail_OutgoingMailServerInfo";
            this.gpbEmail_OutgoingMailServerInfo.Size = new System.Drawing.Size(482, 155);
            this.gpbEmail_OutgoingMailServerInfo.TabIndex = 0;
            this.gpbEmail_OutgoingMailServerInfo.TabStop = false;
            this.gpbEmail_OutgoingMailServerInfo.Text = "Outgoing Mail Server Information";
            // 
            // btnEmail_AddSMTPServer
            // 
            this.btnEmail_AddSMTPServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnEmail_AddSMTPServer.Location = new System.Drawing.Point(291, 123);
            this.btnEmail_AddSMTPServer.Name = "btnEmail_AddSMTPServer";
            this.btnEmail_AddSMTPServer.Size = new System.Drawing.Size(75, 23);
            this.btnEmail_AddSMTPServer.TabIndex = 3;
            this.btnEmail_AddSMTPServer.Text = "Add";
            this.btnEmail_AddSMTPServer.Click += new System.EventHandler(this.btnEmail_AddSMTPServer_Click);
            // 
            // btnEmail_RemoveSMTPServer
            // 
            this.btnEmail_RemoveSMTPServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnEmail_RemoveSMTPServer.Location = new System.Drawing.Point(99, 123);
            this.btnEmail_RemoveSMTPServer.Name = "btnEmail_RemoveSMTPServer";
            this.btnEmail_RemoveSMTPServer.Size = new System.Drawing.Size(75, 23);
            this.btnEmail_RemoveSMTPServer.TabIndex = 2;
            this.btnEmail_RemoveSMTPServer.Text = "Remove";
            this.btnEmail_RemoveSMTPServer.Click += new System.EventHandler(this.btnEmail_RemoveSMTPServer_Click);
            // 
            // btnEmail_EditSMTPServer
            // 
            this.btnEmail_EditSMTPServer.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnEmail_EditSMTPServer.Location = new System.Drawing.Point(195, 123);
            this.btnEmail_EditSMTPServer.Name = "btnEmail_EditSMTPServer";
            this.btnEmail_EditSMTPServer.Size = new System.Drawing.Size(75, 23);
            this.btnEmail_EditSMTPServer.TabIndex = 1;
            this.btnEmail_EditSMTPServer.Text = "Edit";
            this.btnEmail_EditSMTPServer.Click += new System.EventHandler(this.btnEmail_EditSMTPServer_Click);
            // 
            // btnEmail_SMTPDown
            // 
            this.btnEmail_SMTPDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnEmail_SMTPDown.Location = new System.Drawing.Point(460, 77);
            this.btnEmail_SMTPDown.Name = "btnEmail_SMTPDown";
            this.btnEmail_SMTPDown.Size = new System.Drawing.Size(16, 23);
            this.btnEmail_SMTPDown.TabIndex = 31;
            this.btnEmail_SMTPDown.Text = "D";
            this.btnEmail_SMTPDown.Click += new System.EventHandler(this.btnEmail_SMTPDown_Click);
            // 
            // btnEmail_SMTPUp
            // 
            this.btnEmail_SMTPUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnEmail_SMTPUp.Location = new System.Drawing.Point(460, 38);
            this.btnEmail_SMTPUp.Name = "btnEmail_SMTPUp";
            this.btnEmail_SMTPUp.Size = new System.Drawing.Size(16, 23);
            this.btnEmail_SMTPUp.TabIndex = 30;
            this.btnEmail_SMTPUp.Text = "U";
            this.btnEmail_SMTPUp.Click += new System.EventHandler(this.btnEmail_SMTPUp_Click);
            // 
            // lsvEmail_SMTPServersList
            // 
            this.lsvEmail_SMTPServersList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colName,
            this.colSMTPServer});
            this.lsvEmail_SMTPServersList.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lsvEmail_SMTPServersList.FullRowSelect = true;
            this.lsvEmail_SMTPServersList.HideSelection = false;
            this.lsvEmail_SMTPServersList.Location = new System.Drawing.Point(8, 21);
            this.lsvEmail_SMTPServersList.MultiSelect = false;
            this.lsvEmail_SMTPServersList.Name = "lsvEmail_SMTPServersList";
            this.lsvEmail_SMTPServersList.Size = new System.Drawing.Size(446, 96);
            this.lsvEmail_SMTPServersList.TabIndex = 0;
            this.lsvEmail_SMTPServersList.UseCompatibleStateImageBehavior = false;
            this.lsvEmail_SMTPServersList.View = System.Windows.Forms.View.Details;
            this.lsvEmail_SMTPServersList.DoubleClick += new System.EventHandler(this.lsvEmail_SMTPServersList_DoubleClick);
            this.lsvEmail_SMTPServersList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lsvEmail_SMTPServersList_ColumnClick);
            // 
            // colName
            // 
            this.colName.Text = "Name";
            this.colName.Width = 222;
            // 
            // colSMTPServer
            // 
            this.colSMTPServer.Text = "SMTP Server";
            this.colSMTPServer.Width = 220;
            // 
            // tbpSecurity
            // 
            this.tbpSecurity.Controls.Add(this.gpbSecurity_ApplicationSecurity);
            this.tbpSecurity.Controls.Add(this.gpbSecurity_PasswordChange);
            this.tbpSecurity.Location = new System.Drawing.Point(4, 22);
            this.tbpSecurity.Name = "tbpSecurity";
            this.tbpSecurity.Size = new System.Drawing.Size(488, 327);
            this.tbpSecurity.TabIndex = 2;
            this.tbpSecurity.Text = "Security Settings";
            this.tbpSecurity.UseVisualStyleBackColor = true;
            // 
            // gpbSecurity_ApplicationSecurity
            // 
            this.gpbSecurity_ApplicationSecurity.Controls.Add(this.chkSecurity_EmployeeLoginRequired);
            this.gpbSecurity_ApplicationSecurity.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpbSecurity_ApplicationSecurity.Location = new System.Drawing.Point(3, 3);
            this.gpbSecurity_ApplicationSecurity.Name = "gpbSecurity_ApplicationSecurity";
            this.gpbSecurity_ApplicationSecurity.Size = new System.Drawing.Size(482, 66);
            this.gpbSecurity_ApplicationSecurity.TabIndex = 0;
            this.gpbSecurity_ApplicationSecurity.TabStop = false;
            this.gpbSecurity_ApplicationSecurity.Text = "Application Security";
            // 
            // chkSecurity_EmployeeLoginRequired
            // 
            this.chkSecurity_EmployeeLoginRequired.AutoSize = true;
            this.chkSecurity_EmployeeLoginRequired.Font = new System.Drawing.Font("Tahoma", 9F);
            this.chkSecurity_EmployeeLoginRequired.Location = new System.Drawing.Point(159, 28);
            this.chkSecurity_EmployeeLoginRequired.Name = "chkSecurity_EmployeeLoginRequired";
            this.chkSecurity_EmployeeLoginRequired.Size = new System.Drawing.Size(164, 18);
            this.chkSecurity_EmployeeLoginRequired.TabIndex = 0;
            this.chkSecurity_EmployeeLoginRequired.Text = "Employee Login Required";
            this.chkSecurity_EmployeeLoginRequired.UseVisualStyleBackColor = true;
            // 
            // gpbSecurity_PasswordChange
            // 
            this.gpbSecurity_PasswordChange.Controls.Add(this.lblSecurity_Employee);
            this.gpbSecurity_PasswordChange.Controls.Add(this.cmbSecurity_Employees);
            this.gpbSecurity_PasswordChange.Controls.Add(this.btnSecurity_ChangePassword);
            this.gpbSecurity_PasswordChange.Controls.Add(this.txtSecurity_CurrentPassword);
            this.gpbSecurity_PasswordChange.Controls.Add(this.lblSecurity_CurrentPassword);
            this.gpbSecurity_PasswordChange.Controls.Add(this.txtSecurity_ConfirmNewPassword);
            this.gpbSecurity_PasswordChange.Controls.Add(this.txtSecurity_NewPassword);
            this.gpbSecurity_PasswordChange.Controls.Add(this.lblSecurity_ConfirmNewPassword);
            this.gpbSecurity_PasswordChange.Controls.Add(this.lblSecurity_NewPassword);
            this.gpbSecurity_PasswordChange.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpbSecurity_PasswordChange.Location = new System.Drawing.Point(3, 75);
            this.gpbSecurity_PasswordChange.Name = "gpbSecurity_PasswordChange";
            this.gpbSecurity_PasswordChange.Size = new System.Drawing.Size(482, 171);
            this.gpbSecurity_PasswordChange.TabIndex = 1;
            this.gpbSecurity_PasswordChange.TabStop = false;
            this.gpbSecurity_PasswordChange.Text = "Password Change";
            // 
            // lblSecurity_Employee
            // 
            this.lblSecurity_Employee.AutoSize = true;
            this.lblSecurity_Employee.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSecurity_Employee.Location = new System.Drawing.Point(67, 25);
            this.lblSecurity_Employee.Name = "lblSecurity_Employee";
            this.lblSecurity_Employee.Size = new System.Drawing.Size(60, 14);
            this.lblSecurity_Employee.TabIndex = 62;
            this.lblSecurity_Employee.Text = "Employee";
            // 
            // cmbSecurity_Employees
            // 
            this.cmbSecurity_Employees.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSecurity_Employees.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.cmbSecurity_Employees.FormattingEnabled = true;
            this.cmbSecurity_Employees.Location = new System.Drawing.Point(220, 21);
            this.cmbSecurity_Employees.Name = "cmbSecurity_Employees";
            this.cmbSecurity_Employees.Size = new System.Drawing.Size(196, 21);
            this.cmbSecurity_Employees.TabIndex = 0;
            this.cmbSecurity_Employees.SelectedIndexChanged += new System.EventHandler(this.cmbSecurity_Employees_SelectedIndexChanged);
            this.cmbSecurity_Employees.Click += new System.EventHandler(this.cmbSecurity_Employees_Click);
            // 
            // btnSecurity_ChangePassword
            // 
            this.btnSecurity_ChangePassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.btnSecurity_ChangePassword.Location = new System.Drawing.Point(175, 139);
            this.btnSecurity_ChangePassword.Name = "btnSecurity_ChangePassword";
            this.btnSecurity_ChangePassword.Size = new System.Drawing.Size(133, 26);
            this.btnSecurity_ChangePassword.TabIndex = 4;
            this.btnSecurity_ChangePassword.Text = "Change Password";
            this.btnSecurity_ChangePassword.Click += new System.EventHandler(this.btnSecurity_ChangePassword_Click);
            // 
            // txtSecurity_CurrentPassword
            // 
            this.txtSecurity_CurrentPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtSecurity_CurrentPassword.Location = new System.Drawing.Point(220, 51);
            this.txtSecurity_CurrentPassword.Name = "txtSecurity_CurrentPassword";
            this.txtSecurity_CurrentPassword.Size = new System.Drawing.Size(196, 20);
            this.txtSecurity_CurrentPassword.TabIndex = 1;
            this.txtSecurity_CurrentPassword.UseSystemPasswordChar = true;
            this.txtSecurity_CurrentPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSecurity_CurrentPassword_KeyDown);
            // 
            // lblSecurity_CurrentPassword
            // 
            this.lblSecurity_CurrentPassword.AutoSize = true;
            this.lblSecurity_CurrentPassword.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSecurity_CurrentPassword.Location = new System.Drawing.Point(67, 55);
            this.lblSecurity_CurrentPassword.Name = "lblSecurity_CurrentPassword";
            this.lblSecurity_CurrentPassword.Size = new System.Drawing.Size(103, 14);
            this.lblSecurity_CurrentPassword.TabIndex = 56;
            this.lblSecurity_CurrentPassword.Text = "Current Password";
            // 
            // txtSecurity_ConfirmNewPassword
            // 
            this.txtSecurity_ConfirmNewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtSecurity_ConfirmNewPassword.Location = new System.Drawing.Point(220, 109);
            this.txtSecurity_ConfirmNewPassword.Name = "txtSecurity_ConfirmNewPassword";
            this.txtSecurity_ConfirmNewPassword.Size = new System.Drawing.Size(196, 20);
            this.txtSecurity_ConfirmNewPassword.TabIndex = 3;
            this.txtSecurity_ConfirmNewPassword.UseSystemPasswordChar = true;
            // 
            // txtSecurity_NewPassword
            // 
            this.txtSecurity_NewPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.txtSecurity_NewPassword.Location = new System.Drawing.Point(220, 80);
            this.txtSecurity_NewPassword.Name = "txtSecurity_NewPassword";
            this.txtSecurity_NewPassword.Size = new System.Drawing.Size(196, 20);
            this.txtSecurity_NewPassword.TabIndex = 2;
            this.txtSecurity_NewPassword.UseSystemPasswordChar = true;
            // 
            // lblSecurity_ConfirmNewPassword
            // 
            this.lblSecurity_ConfirmNewPassword.AutoSize = true;
            this.lblSecurity_ConfirmNewPassword.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSecurity_ConfirmNewPassword.Location = new System.Drawing.Point(67, 113);
            this.lblSecurity_ConfirmNewPassword.Name = "lblSecurity_ConfirmNewPassword";
            this.lblSecurity_ConfirmNewPassword.Size = new System.Drawing.Size(132, 14);
            this.lblSecurity_ConfirmNewPassword.TabIndex = 53;
            this.lblSecurity_ConfirmNewPassword.Text = "Confirm New Password";
            // 
            // lblSecurity_NewPassword
            // 
            this.lblSecurity_NewPassword.AutoSize = true;
            this.lblSecurity_NewPassword.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblSecurity_NewPassword.Location = new System.Drawing.Point(67, 84);
            this.lblSecurity_NewPassword.Name = "lblSecurity_NewPassword";
            this.lblSecurity_NewPassword.Size = new System.Drawing.Size(87, 14);
            this.lblSecurity_NewPassword.TabIndex = 52;
            this.lblSecurity_NewPassword.Text = "New Password";
            // 
            // fclsGENOptions
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(510, 403);
            this.Controls.Add(this.tbcOptions);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnAdministratorMode);
            this.Controls.Add(this.btnClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "fclsGENOptions";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Stock - Options";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.fclsGENOptions_Closing);
            this.tbcOptions.ResumeLayout(false);
            this.tbpGeneral.ResumeLayout(false);
            this.gpbGeneral_OrderDefaults.ResumeLayout(false);
            this.gpbGeneral_OrderDefaults.PerformLayout();
            this.gpbWorkYear.ResumeLayout(false);
            this.gpbGeneral_Backup.ResumeLayout(false);
            this.gpbGeneral_Backup.PerformLayout();
            this.gpbLanguage.ResumeLayout(false);
            this.tbpDentalOfficeInformation.ResumeLayout(false);
            this.tbpDentalOfficeInformation.PerformLayout();
            this.tbpEmail.ResumeLayout(false);
            this.gpbEmail_OrderEmails.ResumeLayout(false);
            this.gpbEmail_OrderEmails.PerformLayout();
            this.gpbEmail_OutgoingMailServerInfo.ResumeLayout(false);
            this.tbpSecurity.ResumeLayout(false);
            this.gpbSecurity_ApplicationSecurity.ResumeLayout(false);
            this.gpbSecurity_ApplicationSecurity.PerformLayout();
            this.gpbSecurity_PasswordChange.ResumeLayout(false);
            this.gpbSecurity_PasswordChange.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        #region Events
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnEmail_AddSMTPServer_Click(object sender, EventArgs e)
        {
            clsSMTPListViewItem smtpNewItem = new clsSMTPListViewItem();

            fclsGENOptions_SMTPServer frmGENOptions_SMTPServer = new fclsGENOptions_SMTPServer(smtpNewItem);
            if(frmGENOptions_SMTPServer.ShowDialog() == DialogResult.OK)
            {
                smtpNewItem.IsNew = false;
                clsConfiguration.Email_SMTPServers.Add(smtpNewItem.SMTPServerObject);
                this.lsvEmail_SMTPServersList.Items.Add(smtpNewItem);
                this.lsvEmail_SMTPServersList.Sort();
            }
        }

        private void btnEmail_EditSMTPServer_Click(object sender, EventArgs e)
        {
            this.EditSelectedSMTPServer();
        }

        private void btnEmail_RemoveSMTPServer_Click(object sender, EventArgs e)
        {
            if (this.lsvEmail_SMTPServersList.SelectedItems.Count > 0)
            {
                ListViewItem lviItem = this.lsvEmail_SMTPServersList.SelectedItems[0];
                this.lsvEmail_SMTPServersList.Items.Remove(lviItem);
                clsConfiguration.Email_SMTPServers.Remove(((clsSMTPListViewItem) lviItem).SMTPServerObject);
            }
        }

        private void btnEmail_SMTPDown_Click(object sender, EventArgs e)
        {
            if (this.lsvEmail_SMTPServersList.SelectedItems.Count > 0)
                this.MoveListViewItem(this.lsvEmail_SMTPServersList, false);
        }

        private void btnEmail_SMTPUp_Click(object sender, EventArgs e)
        {
            if (this.lsvEmail_SMTPServersList.SelectedItems.Count > 0)
                this.MoveListViewItem(this.lsvEmail_SMTPServersList, true);
        }
        
        private void btnHelp_Click(object sender, System.EventArgs e)
        {
            Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm", "Option.htm");  //
        }

        private void btnSecurity_ChangePassword_Click(object sender, EventArgs e)
        {
            string strPasswordHash;

            if (this.cmbSecurity_Employees.SelectedIndex != -1)
            {
                strPasswordHash = this.m_dtaEmployees.Rows[this.cmbSecurity_Employees.SelectedIndex]["UserPassword"].ToString();
                
                // if current password is empty, it can only be set in administrator mode
                if (!m_blnAdministratorMode && strPasswordHash.Length == 0)
                {
                    MessageBox.Show("The employee that was selected does not have a password set. A password can be set for the first time only when in 'Administrator Mode'.",
                                    clsConfiguration.Internal_CompanyName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    // validate current password or bypass validation if in administrator mode
                    if (m_blnAdministratorMode ||
                        (strPasswordHash.Length > 0 && clsUtilities.String_CompareHashes(this.txtSecurity_CurrentPassword.Text, strPasswordHash)))
                    {
                        // check that the two new password fields match
                        if (String.Compare(this.txtSecurity_NewPassword.Text, this.txtSecurity_ConfirmNewPassword.Text) == 0)
                        {
                            // create password hash
                            if (this.txtSecurity_NewPassword.Text.Length != 0)
                                strPasswordHash = clsUtilities.String_Hash(this.txtSecurity_NewPassword.Text, null);
                            else
                                strPasswordHash = "";

                            // save password hash in datatable
                            DataRow dtrCurrentRow = m_dtaEmployees.Rows[this.cmbSecurity_Employees.SelectedIndex];
                            dtrCurrentRow.BeginEdit();
                            dtrCurrentRow["UserPassword"] = strPasswordHash;
                            dtrCurrentRow.EndEdit();

                            // update database
                            try
                            {
                                m_odaEmployees.Update(m_dtaEmployees);
                                m_dtaEmployees.AcceptChanges();
                            }
                            catch
                            {
                                m_dtaEmployees.RejectChanges();
                                MessageBox.Show("An error occured while attempting to save the new password.",
                                                clsConfiguration.Internal_CompanyName,
                                                MessageBoxButtons.OK,
                                                MessageBoxIcon.Error);
                            }

                            // clear password fields
                            this.ClearPasswordFields();
                            this.cmbSecurity_Employees.SelectedIndex = -1;
                        }
                        else
                        {
                            this.txtSecurity_NewPassword.Clear();
                            this.txtSecurity_ConfirmNewPassword.Clear();
                            this.txtSecurity_NewPassword.Focus();

                            m_ttToolTip.ToolTipTitle = "New Password";
                            m_ttToolTip.Show("The password you typed do not match. Please retype the new password in both boxes.",
                                             this.txtSecurity_ConfirmNewPassword,
                                             0, txtSecurity_ConfirmNewPassword.Size.Height,
                                             5000);
                        }
                    }
                    else
                    {
                        this.txtSecurity_CurrentPassword.Clear();
                        this.txtSecurity_CurrentPassword.Focus();

                        m_ttToolTip.ToolTipTitle = "Invalid Current Password";
                        m_ttToolTip.Show("The current password is incorrect. Please retype the selected employee's current password.",
                                         this.txtSecurity_CurrentPassword,
                                         0, txtSecurity_CurrentPassword.Size.Height,
                                         5000);
                    }
                }
            }
            else
            {
                m_ttToolTip.ToolTipTitle = "No Employee Selected";
                m_ttToolTip.Show("Please select an employee from the dropdown list.",
                                 this.cmbSecurity_Employees,
                                 0, cmbSecurity_Employees.Size.Height,
                                 5000);
            }
        }

        private void cmbSecurity_Employees_Click(object sender, EventArgs e)
        {
            m_ttToolTip.Hide(cmbSecurity_Employees);
        }

        private void cmbSecurity_Employees_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearPasswordFields();
        }

		private void fclsGENOptions_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            // General
            clsConfiguration.General_BackupOrders = this.ckbGeneral_BackupOrders.Checked;
            if (this.cmbGeneral_DefaultUser.SelectedIndex != -1)
                clsConfiguration.General_DefaultUserID = (int)m_dtaEmployees.Rows[this.cmbGeneral_DefaultUser.SelectedIndex]["EmployeeId"];
            if (this.cmbGeneral_DefaultSupplier.SelectedIndex != -1)
                clsConfiguration.General_DefaultSupplierID = (int)m_dtaSupplier.Rows[this.cmbGeneral_DefaultSupplier.SelectedIndex]["FournisseurId"];

            // Dental Office Info
            clsConfiguration.DentalOffice_Name = this.txtDentalOfficeInfo_Name.Text;
            clsConfiguration.DentalOffice_Address = this.txtDentalOfficeInfo_Address.Text;
            clsConfiguration.DentalOffice_City = this.txtDentalOfficeInfo_City.Text;
            clsConfiguration.DentalOffice_PostalCode = this.txtDentalOfficeInfo_PostalCode.Text;
            clsConfiguration.DentalOffice_StateProvince = this.txtDentalOfficeInfo_StateProvince.Text;
            clsConfiguration.DentalOffice_Country = this.txtDentalOfficeInfo_Country.Text;
            clsConfiguration.DentalOffice_PhoneNr = this.txtDentalOfficeInfo_PhoneNumber.Text;
            clsConfiguration.DentalOffice_FaxNr = this.txtDentalOfficeInfo_FaxNumber.Text;
            clsConfiguration.DentalOffice_Email = this.txtDentalOfficeInfo_Email.Text;
            if (this.cmbDentalOfficeInfo_Administrator.SelectedIndex != -1)
                clsConfiguration.DentalOffice_AdministratorUserID = (int)m_dtaEmployees.Rows[this.cmbDentalOfficeInfo_Administrator.SelectedIndex]["EmployeeId"];

            // Email Settings
            clsConfiguration.Email_Subject = this.txtEmail_OrderSubject.Text;
            clsConfiguration.Email_Body = this.txtEmail_OrderBody.Text;
            // NOTE: SMTP server list gets updated together with the listview items

            // Security Settings
            clsConfiguration.Security_EmployeeLoginRequired = this.chkSecurity_EmployeeLoginRequired.Checked;

            // store new settings in configuration files
            try
            {
                clsConfiguration.Save();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message,
                                    clsConfiguration.Internal_ApplicationName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
            }
        }

		private void btnAdministratorMode_Click(object sender, System.EventArgs e)
		{
            if (clsConfiguration.DentalOffice_AdministratorUserID == -1)
            {
                this.EnableAdministratorFields(true);
                return;
            }
            else
            {
                if (!m_blnAdministratorMode)
                {
                    string strPasswordEntered = InputBox.ShowPasswordBox("Please enter the administrator's password:", clsConfiguration.Internal_ApplicationName);
                    if (strPasswordEntered != null)
                    {
                        string strPasswordHash = m_dtaEmployees.Rows[this.cmbDentalOfficeInfo_Administrator.SelectedIndex]["UserPassword"].ToString();
                        if (clsUtilities.String_CompareHashes(strPasswordEntered, strPasswordHash))
                                this.EnableAdministratorFields(true);
                        else
                            MessageBox.Show("Incorrect password entered. Cannot enter administrator mode.",
                                            clsConfiguration.Internal_ApplicationName,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Error);
                    }
                }
                else
                    this.EnableAdministratorFields(false);
            }
		}

        private void lsvEmail_SMTPServersList_ColumnClick(object sender, ColumnClickEventArgs e)
        {
            this.lsvEmail_SMTPServersList.ListViewItemSorter = m_lvwSMTPServerColumnSorter;

            // Determine if clicked column is already the column that is being sorted.
            if (e.Column == m_lvwSMTPServerColumnSorter.SortColumn)
            {
                // Reverse the current sort direction for this column.
                if (m_lvwSMTPServerColumnSorter.Order == SortOrder.Ascending)
                {
                    m_lvwSMTPServerColumnSorter.Order = SortOrder.Descending;
                }
                else
                {
                    m_lvwSMTPServerColumnSorter.Order = SortOrder.Ascending;
                }
            }
            else
            {
                // Set the column number that is to be sorted; default to ascending.
                m_lvwSMTPServerColumnSorter.SortColumn = e.Column;
                m_lvwSMTPServerColumnSorter.Order = SortOrder.Ascending;
            }

            // Perform the sort with these new sort options.
            this.lsvEmail_SMTPServersList.Sort();
        }

        private void lsvEmail_SMTPServersList_DoubleClick(object sender, EventArgs e)
        {
            this.EditSelectedSMTPServer();
        }

        private void txtSecurity_CurrentPassword_KeyDown(object sender, KeyEventArgs e)
        {
            m_ttToolTip.Hide(this.txtSecurity_CurrentPassword);
        }
        #endregion

        #region Methods
        private void ClearPasswordFields()
        {
            this.txtSecurity_CurrentPassword.Clear();
            this.txtSecurity_NewPassword.Clear();
            this.txtSecurity_ConfirmNewPassword.Clear();
        }

        private void EditSelectedSMTPServer()
        {
            if (this.lsvEmail_SMTPServersList.SelectedItems.Count > 0)
            {
                clsSMTPListViewItem smtpItem = (clsSMTPListViewItem)this.lsvEmail_SMTPServersList.SelectedItems[0];

                fclsGENOptions_SMTPServer frmGENOptions_SMTPServer = new fclsGENOptions_SMTPServer(smtpItem);
                frmGENOptions_SMTPServer.ShowDialog();
            }
        }

        private void EnableAdministratorFields(bool blnFieldsEnabled)
        {
            m_blnAdministratorMode = blnFieldsEnabled;

            if (blnFieldsEnabled)
                this.btnAdministratorMode.Text = "Normal Mode";
            else
                this.btnAdministratorMode.Text = "Administrator Mode";
                        
            // General
            this.gpbGeneral_Backup.Enabled = blnFieldsEnabled;
            this.gpbGeneral_OrderDefaults.Enabled = blnFieldsEnabled;

            // Dental Office Info
            this.txtDentalOfficeInfo_Address.Enabled = blnFieldsEnabled;
            this.txtDentalOfficeInfo_City.Enabled = blnFieldsEnabled;
            this.txtDentalOfficeInfo_Country.Enabled = blnFieldsEnabled;
            this.txtDentalOfficeInfo_Email.Enabled = blnFieldsEnabled;
            this.txtDentalOfficeInfo_FaxNumber.Enabled = blnFieldsEnabled;
            this.txtDentalOfficeInfo_Name.Enabled = blnFieldsEnabled;
            this.txtDentalOfficeInfo_PhoneNumber.Enabled = blnFieldsEnabled;
            this.txtDentalOfficeInfo_PostalCode.Enabled = blnFieldsEnabled;
            this.txtDentalOfficeInfo_StateProvince.Enabled = blnFieldsEnabled;
            this.cmbDentalOfficeInfo_Administrator.Enabled = blnFieldsEnabled;

            // Email Settings
            this.gpbEmail_OrderEmails.Enabled = blnFieldsEnabled;
            this.gpbEmail_OutgoingMailServerInfo.Enabled = blnFieldsEnabled;

            // Security Settings
            this.gpbSecurity_ApplicationSecurity.Enabled = blnFieldsEnabled;
        }

        private void MoveListViewItem(ListView lvList, bool blnMoveUp)
        {
            int intSelectedID;

            intSelectedID = lvList.SelectedItems[0].Index;
            if ((blnMoveUp && intSelectedID != 0) ||
                (!blnMoveUp && intSelectedID != lvList.Items.Count - 1))
            {
                // disable automatic sorting
                this.lsvEmail_SMTPServersList.ListViewItemSorter = null;

                // move item
                ListViewItem lviItem = (ListViewItem)lvList.Items[intSelectedID].Clone();
                lvList.Items.RemoveAt(intSelectedID);
                if (blnMoveUp)
                    lvList.Items.Insert(--intSelectedID, lviItem);
                else
                    lvList.Items.Insert(++intSelectedID, lviItem);

                // refresh listview
                lvList.Items[intSelectedID].Selected = true;
                lvList.Refresh();
                lvList.Focus();
            }
        }
        #endregion

    }
}
