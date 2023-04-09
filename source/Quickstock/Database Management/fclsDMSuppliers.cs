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
	/// Summary description for frmSuppliers.
	/// </summary>
	public class fclsDMSuppliers : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblMessage;
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.ListView lsvSuppliers;
		private System.Windows.Forms.ColumnHeader colSupplier;
		private System.Windows.Forms.ColumnHeader colContact;
		private System.Windows.Forms.ColumnHeader colCustomerNumber;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.CheckBox ckbStatus;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label lblCustomerNumber;
		private System.Windows.Forms.TextBox txtCustomerNumber;
		private System.Windows.Forms.GroupBox gpbContactInformation;
		private System.Windows.Forms.TextBox txtContactEmail;
		private System.Windows.Forms.Label lblContactEmail;
		private System.Windows.Forms.TextBox txtContactFaxNumber;
		private System.Windows.Forms.Label lblContactFaxNumber;
		private System.Windows.Forms.TextBox txtContactPhoneNumber;
		private System.Windows.Forms.Label lblContactPhoneNumber;
		private System.Windows.Forms.TextBox txtContactLastName;
		private System.Windows.Forms.TextBox txtContactFirstName;
		private System.Windows.Forms.Label lblContactLastName;
		private System.Windows.Forms.Label lblContactFirstName;
		private System.Windows.Forms.Label lblContactTitle;
		private System.Windows.Forms.GroupBox gpbSupplierInformation;
		private System.Windows.Forms.Label lblSupplierCity;
		private System.Windows.Forms.TextBox txtSupplierCountry;
		private System.Windows.Forms.TextBox txtSupplierPostalCode;
		private System.Windows.Forms.Label lblSupplierCountry;
		private System.Windows.Forms.Label lblSupplierPostalCode;
		private System.Windows.Forms.TextBox txtSupplierStateProvince;
		private System.Windows.Forms.TextBox txtSupplierCity;
		private System.Windows.Forms.TextBox txtSupplierAddress;
		private System.Windows.Forms.TextBox txtSupplierName;
		private System.Windows.Forms.Label lblSupplierStateProvince;
		private System.Windows.Forms.Label lblSupplierAddress;
		private System.Windows.Forms.Label lblSupplierName;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.ColumnHeader colActive;
		private System.Windows.Forms.TextBox txtContactTitle;
		
		private ArrayList m_alSupplierList;
		private bool m_blnCancelButton,	m_blnNewButton, m_blnOkButton;
		private bool m_blnChangesMade;
		private clsListViewColumnSorter m_lvwColumnSorter;
		private DataTable m_dtaSuppliers;
		private OleDbConnection	m_odcConnection;
		private OleDbDataAdapter m_odaSuppliers;
		private int m_intLastUsedSupplierId;
		private System.Windows.Forms.Button btnHelp;
		private SupplierListViewItem m_slviSelectedItem;

		public fclsDMSuppliers(OleDbConnection odcConnection)
		{
			int intCurrentSupplierId;
			OleDbCommandBuilder ocbSupplier;

			InitializeComponent();
			
			// Variable initialization
			m_alSupplierList = new ArrayList();
			m_blnCancelButton = m_blnNewButton = m_blnOkButton = false;
			m_blnChangesMade = false;
			m_dtaSuppliers = new DataTable();
			intCurrentSupplierId = m_intLastUsedSupplierId = -1;
			m_lvwColumnSorter = new clsListViewColumnSorter();
			m_odcConnection = odcConnection;
			SupplierListViewItem elviItem;
			
			// Get data from database and store it in DataTable m_dtaSuppliers
			m_odaSuppliers = new OleDbDataAdapter("SELECT * FROM [Suppliers] ORDER BY [CompanyName]", m_odcConnection);
			ocbSupplier = new OleDbCommandBuilder(m_odaSuppliers);
			m_odaSuppliers.Fill(m_dtaSuppliers);
			
			// Populate ListView and get last used EmployeeId and store it in m_intLastUsedSupplierId
			foreach(DataRow dtrRow in m_dtaSuppliers.Rows)
			{
				intCurrentSupplierId = int.Parse(dtrRow["FournisseurId"].ToString());
				if(intCurrentSupplierId > m_intLastUsedSupplierId)
					m_intLastUsedSupplierId = intCurrentSupplierId;

				elviItem = new SupplierListViewItem(intCurrentSupplierId,
													dtrRow["CompanyName"].ToString(),
													dtrRow["BillingAdress"].ToString(),
													dtrRow["City"].ToString(),
													dtrRow["StateOrProvince"].ToString(),
													dtrRow["PostalCode"].ToString(),
													dtrRow["Country"].ToString(),
													dtrRow["ConTitle"].ToString(),
													dtrRow["ContactFirstName"].ToString(),
													dtrRow["ContactLastName"].ToString(),
													dtrRow["PhoneNumber"].ToString(),
													dtrRow["FaxNumber"].ToString(),
													dtrRow["Email"].ToString(),
													dtrRow["CustomId"].ToString(),
													int.Parse(dtrRow["Status"].ToString()));
				
				this.lsvSuppliers.Items.Add(elviItem);
			}

			// Sets the listview control's sorter and initialize the sorter
			this.lsvSuppliers.ListViewItemSorter = m_lvwColumnSorter;
			m_lvwColumnSorter.SortColumn = 0;
			m_lvwColumnSorter.Order = SortOrder.Ascending;

			this.SetControlsEnabledState(false);
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
			this.lblMessage = new System.Windows.Forms.Label();
			this.lsvSuppliers = new System.Windows.Forms.ListView();
			this.colSupplier = new System.Windows.Forms.ColumnHeader();
			this.colContact = new System.Windows.Forms.ColumnHeader();
			this.colCustomerNumber = new System.Windows.Forms.ColumnHeader();
			this.colActive = new System.Windows.Forms.ColumnHeader();
			this.panel1 = new System.Windows.Forms.Panel();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.ckbStatus = new System.Windows.Forms.CheckBox();
			this.lblStatus = new System.Windows.Forms.Label();
			this.lblCustomerNumber = new System.Windows.Forms.Label();
			this.txtCustomerNumber = new System.Windows.Forms.TextBox();
			this.gpbContactInformation = new System.Windows.Forms.GroupBox();
			this.txtContactTitle = new System.Windows.Forms.TextBox();
			this.txtContactEmail = new System.Windows.Forms.TextBox();
			this.lblContactEmail = new System.Windows.Forms.Label();
			this.txtContactFaxNumber = new System.Windows.Forms.TextBox();
			this.lblContactFaxNumber = new System.Windows.Forms.Label();
			this.txtContactPhoneNumber = new System.Windows.Forms.TextBox();
			this.lblContactPhoneNumber = new System.Windows.Forms.Label();
			this.txtContactLastName = new System.Windows.Forms.TextBox();
			this.txtContactFirstName = new System.Windows.Forms.TextBox();
			this.lblContactLastName = new System.Windows.Forms.Label();
			this.lblContactFirstName = new System.Windows.Forms.Label();
			this.lblContactTitle = new System.Windows.Forms.Label();
			this.gpbSupplierInformation = new System.Windows.Forms.GroupBox();
			this.lblSupplierCity = new System.Windows.Forms.Label();
			this.txtSupplierCountry = new System.Windows.Forms.TextBox();
			this.txtSupplierPostalCode = new System.Windows.Forms.TextBox();
			this.lblSupplierCountry = new System.Windows.Forms.Label();
			this.lblSupplierPostalCode = new System.Windows.Forms.Label();
			this.txtSupplierStateProvince = new System.Windows.Forms.TextBox();
			this.txtSupplierCity = new System.Windows.Forms.TextBox();
			this.txtSupplierAddress = new System.Windows.Forms.TextBox();
			this.txtSupplierName = new System.Windows.Forms.TextBox();
			this.lblSupplierStateProvince = new System.Windows.Forms.Label();
			this.lblSupplierAddress = new System.Windows.Forms.Label();
			this.lblSupplierName = new System.Windows.Forms.Label();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.panel1.SuspendLayout();
			this.gpbContactInformation.SuspendLayout();
			this.gpbSupplierInformation.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.Location = new System.Drawing.Point(16, 520);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(120, 40);
			this.lblMessage.TabIndex = 70;
			this.lblMessage.Text = "Press Add or\nselect a name and Press Modify or Remove.";
			// 
			// lsvSuppliers
			// 
			this.lsvSuppliers.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						   this.colSupplier,
																						   this.colContact,
																						   this.colCustomerNumber,
																						   this.colActive});
			this.lsvSuppliers.FullRowSelect = true;
			this.lsvSuppliers.Location = new System.Drawing.Point(8, 8);
			this.lsvSuppliers.MultiSelect = false;
			this.lsvSuppliers.Name = "lsvSuppliers";
			this.lsvSuppliers.Size = new System.Drawing.Size(592, 144);
			this.lsvSuppliers.TabIndex = 80;
			this.lsvSuppliers.View = System.Windows.Forms.View.Details;
			this.lsvSuppliers.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lsvSuppliers_ColumnClick);
			this.lsvSuppliers.SelectedIndexChanged += new System.EventHandler(this.lsvSuppliers_SelectedIndexChanged);
			// 
			// colSupplier
			// 
			this.colSupplier.Text = "Supplier";
			this.colSupplier.Width = 191;
			// 
			// colContact
			// 
			this.colContact.Text = "Contact";
			this.colContact.Width = 196;
			// 
			// colCustomerNumber
			// 
			this.colCustomerNumber.Text = "Customer #";
			this.colCustomerNumber.Width = 139;
			// 
			// colActive
			// 
			this.colActive.Text = "Active";
			this.colActive.Width = 44;
			// 
			// panel1
			// 
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.btnRemove);
			this.panel1.Controls.Add(this.btnNew);
			this.panel1.Controls.Add(this.ckbStatus);
			this.panel1.Controls.Add(this.lblStatus);
			this.panel1.Controls.Add(this.lblCustomerNumber);
			this.panel1.Controls.Add(this.txtCustomerNumber);
			this.panel1.Controls.Add(this.gpbContactInformation);
			this.panel1.Controls.Add(this.gpbSupplierInformation);
			this.panel1.Location = new System.Drawing.Point(8, 160);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(592, 352);
			this.panel1.TabIndex = 81;
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(488, 199);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(80, 32);
			this.btnRemove.TabIndex = 85;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(488, 119);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(80, 32);
			this.btnNew.TabIndex = 84;
			this.btnNew.Text = "Add New Supplier";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// ckbStatus
			// 
			this.ckbStatus.BackColor = System.Drawing.Color.White;
			this.ckbStatus.Location = new System.Drawing.Point(136, 320);
			this.ckbStatus.Name = "ckbStatus";
			this.ckbStatus.Size = new System.Drawing.Size(56, 20);
			this.ckbStatus.TabIndex = 83;
			this.ckbStatus.Text = "Active";
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblStatus.Location = new System.Drawing.Point(16, 320);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(43, 20);
			this.lblStatus.TabIndex = 82;
			this.lblStatus.Text = "Status";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblCustomerNumber
			// 
			this.lblCustomerNumber.AutoSize = true;
			this.lblCustomerNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCustomerNumber.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblCustomerNumber.Location = new System.Drawing.Point(16, 288);
			this.lblCustomerNumber.Name = "lblCustomerNumber";
			this.lblCustomerNumber.Size = new System.Drawing.Size(115, 20);
			this.lblCustomerNumber.TabIndex = 81;
			this.lblCustomerNumber.Text = "Customer Number";
			this.lblCustomerNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtCustomerNumber
			// 
			this.txtCustomerNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtCustomerNumber.Location = new System.Drawing.Point(136, 288);
			this.txtCustomerNumber.Name = "txtCustomerNumber";
			this.txtCustomerNumber.Size = new System.Drawing.Size(320, 23);
			this.txtCustomerNumber.TabIndex = 80;
			this.txtCustomerNumber.Text = "";
			// 
			// gpbContactInformation
			// 
			this.gpbContactInformation.Controls.Add(this.txtContactTitle);
			this.gpbContactInformation.Controls.Add(this.txtContactEmail);
			this.gpbContactInformation.Controls.Add(this.lblContactEmail);
			this.gpbContactInformation.Controls.Add(this.txtContactFaxNumber);
			this.gpbContactInformation.Controls.Add(this.lblContactFaxNumber);
			this.gpbContactInformation.Controls.Add(this.txtContactPhoneNumber);
			this.gpbContactInformation.Controls.Add(this.lblContactPhoneNumber);
			this.gpbContactInformation.Controls.Add(this.txtContactLastName);
			this.gpbContactInformation.Controls.Add(this.txtContactFirstName);
			this.gpbContactInformation.Controls.Add(this.lblContactLastName);
			this.gpbContactInformation.Controls.Add(this.lblContactFirstName);
			this.gpbContactInformation.Controls.Add(this.lblContactTitle);
			this.gpbContactInformation.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gpbContactInformation.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.gpbContactInformation.Location = new System.Drawing.Point(8, 128);
			this.gpbContactInformation.Name = "gpbContactInformation";
			this.gpbContactInformation.Size = new System.Drawing.Size(464, 152);
			this.gpbContactInformation.TabIndex = 79;
			this.gpbContactInformation.TabStop = false;
			this.gpbContactInformation.Text = "Contact Information";
			// 
			// txtContactTitle
			// 
			this.txtContactTitle.Location = new System.Drawing.Point(136, 24);
			this.txtContactTitle.Name = "txtContactTitle";
			this.txtContactTitle.TabIndex = 65;
			this.txtContactTitle.Text = "";
			// 
			// txtContactEmail
			// 
			this.txtContactEmail.Location = new System.Drawing.Point(136, 120);
			this.txtContactEmail.Name = "txtContactEmail";
			this.txtContactEmail.Size = new System.Drawing.Size(320, 24);
			this.txtContactEmail.TabIndex = 5;
			this.txtContactEmail.Text = "";
			// 
			// lblContactEmail
			// 
			this.lblContactEmail.AutoSize = true;
			this.lblContactEmail.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblContactEmail.ForeColor = System.Drawing.Color.Red;
			this.lblContactEmail.Location = new System.Drawing.Point(16, 120);
			this.lblContactEmail.Name = "lblContactEmail";
			this.lblContactEmail.Size = new System.Drawing.Size(38, 20);
			this.lblContactEmail.TabIndex = 64;
			this.lblContactEmail.Text = "Email";
			this.lblContactEmail.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtContactFaxNumber
			// 
			this.txtContactFaxNumber.Location = new System.Drawing.Point(344, 96);
			this.txtContactFaxNumber.Name = "txtContactFaxNumber";
			this.txtContactFaxNumber.Size = new System.Drawing.Size(112, 24);
			this.txtContactFaxNumber.TabIndex = 4;
			this.txtContactFaxNumber.Text = "";
			// 
			// lblContactFaxNumber
			// 
			this.lblContactFaxNumber.AutoSize = true;
			this.lblContactFaxNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblContactFaxNumber.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblContactFaxNumber.Location = new System.Drawing.Point(264, 96);
			this.lblContactFaxNumber.Name = "lblContactFaxNumber";
			this.lblContactFaxNumber.Size = new System.Drawing.Size(79, 20);
			this.lblContactFaxNumber.TabIndex = 62;
			this.lblContactFaxNumber.Text = "Fax Number";
			this.lblContactFaxNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtContactPhoneNumber
			// 
			this.txtContactPhoneNumber.Location = new System.Drawing.Point(136, 96);
			this.txtContactPhoneNumber.Name = "txtContactPhoneNumber";
			this.txtContactPhoneNumber.Size = new System.Drawing.Size(112, 24);
			this.txtContactPhoneNumber.TabIndex = 3;
			this.txtContactPhoneNumber.Text = "";
			// 
			// lblContactPhoneNumber
			// 
			this.lblContactPhoneNumber.AutoSize = true;
			this.lblContactPhoneNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblContactPhoneNumber.ForeColor = System.Drawing.Color.Red;
			this.lblContactPhoneNumber.Location = new System.Drawing.Point(16, 96);
			this.lblContactPhoneNumber.Name = "lblContactPhoneNumber";
			this.lblContactPhoneNumber.Size = new System.Drawing.Size(95, 20);
			this.lblContactPhoneNumber.TabIndex = 60;
			this.lblContactPhoneNumber.Text = "Phone Number";
			this.lblContactPhoneNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtContactLastName
			// 
			this.txtContactLastName.Location = new System.Drawing.Point(136, 72);
			this.txtContactLastName.Name = "txtContactLastName";
			this.txtContactLastName.Size = new System.Drawing.Size(160, 24);
			this.txtContactLastName.TabIndex = 2;
			this.txtContactLastName.Text = "";
			// 
			// txtContactFirstName
			// 
			this.txtContactFirstName.Location = new System.Drawing.Point(136, 48);
			this.txtContactFirstName.Name = "txtContactFirstName";
			this.txtContactFirstName.Size = new System.Drawing.Size(320, 24);
			this.txtContactFirstName.TabIndex = 1;
			this.txtContactFirstName.Text = "";
			// 
			// lblContactLastName
			// 
			this.lblContactLastName.AutoSize = true;
			this.lblContactLastName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblContactLastName.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblContactLastName.Location = new System.Drawing.Point(16, 72);
			this.lblContactLastName.Name = "lblContactLastName";
			this.lblContactLastName.Size = new System.Drawing.Size(69, 20);
			this.lblContactLastName.TabIndex = 22;
			this.lblContactLastName.Text = "Last Name";
			this.lblContactLastName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblContactFirstName
			// 
			this.lblContactFirstName.AutoSize = true;
			this.lblContactFirstName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblContactFirstName.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblContactFirstName.Location = new System.Drawing.Point(16, 48);
			this.lblContactFirstName.Name = "lblContactFirstName";
			this.lblContactFirstName.Size = new System.Drawing.Size(70, 20);
			this.lblContactFirstName.TabIndex = 21;
			this.lblContactFirstName.Text = "First Name";
			this.lblContactFirstName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblContactTitle
			// 
			this.lblContactTitle.AutoSize = true;
			this.lblContactTitle.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblContactTitle.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblContactTitle.Location = new System.Drawing.Point(16, 24);
			this.lblContactTitle.Name = "lblContactTitle";
			this.lblContactTitle.Size = new System.Drawing.Size(31, 20);
			this.lblContactTitle.TabIndex = 20;
			this.lblContactTitle.Text = "Title";
			this.lblContactTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// gpbSupplierInformation
			// 
			this.gpbSupplierInformation.Controls.Add(this.lblSupplierCity);
			this.gpbSupplierInformation.Controls.Add(this.txtSupplierCountry);
			this.gpbSupplierInformation.Controls.Add(this.txtSupplierPostalCode);
			this.gpbSupplierInformation.Controls.Add(this.lblSupplierCountry);
			this.gpbSupplierInformation.Controls.Add(this.lblSupplierPostalCode);
			this.gpbSupplierInformation.Controls.Add(this.txtSupplierStateProvince);
			this.gpbSupplierInformation.Controls.Add(this.txtSupplierCity);
			this.gpbSupplierInformation.Controls.Add(this.txtSupplierAddress);
			this.gpbSupplierInformation.Controls.Add(this.txtSupplierName);
			this.gpbSupplierInformation.Controls.Add(this.lblSupplierStateProvince);
			this.gpbSupplierInformation.Controls.Add(this.lblSupplierAddress);
			this.gpbSupplierInformation.Controls.Add(this.lblSupplierName);
			this.gpbSupplierInformation.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gpbSupplierInformation.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.gpbSupplierInformation.Location = new System.Drawing.Point(8, 0);
			this.gpbSupplierInformation.Name = "gpbSupplierInformation";
			this.gpbSupplierInformation.Size = new System.Drawing.Size(464, 128);
			this.gpbSupplierInformation.TabIndex = 78;
			this.gpbSupplierInformation.TabStop = false;
			this.gpbSupplierInformation.Text = "Supplier Information";
			// 
			// lblSupplierCity
			// 
			this.lblSupplierCity.AutoSize = true;
			this.lblSupplierCity.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSupplierCity.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSupplierCity.Location = new System.Drawing.Point(16, 72);
			this.lblSupplierCity.Name = "lblSupplierCity";
			this.lblSupplierCity.Size = new System.Drawing.Size(28, 20);
			this.lblSupplierCity.TabIndex = 53;
			this.lblSupplierCity.Text = "City";
			this.lblSupplierCity.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtSupplierCountry
			// 
			this.txtSupplierCountry.Location = new System.Drawing.Point(344, 96);
			this.txtSupplierCountry.Name = "txtSupplierCountry";
			this.txtSupplierCountry.Size = new System.Drawing.Size(112, 24);
			this.txtSupplierCountry.TabIndex = 5;
			this.txtSupplierCountry.Text = "";
			// 
			// txtSupplierPostalCode
			// 
			this.txtSupplierPostalCode.Location = new System.Drawing.Point(344, 72);
			this.txtSupplierPostalCode.Name = "txtSupplierPostalCode";
			this.txtSupplierPostalCode.Size = new System.Drawing.Size(112, 24);
			this.txtSupplierPostalCode.TabIndex = 3;
			this.txtSupplierPostalCode.Text = "";
			// 
			// lblSupplierCountry
			// 
			this.lblSupplierCountry.AutoSize = true;
			this.lblSupplierCountry.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSupplierCountry.ForeColor = System.Drawing.Color.Red;
			this.lblSupplierCountry.Location = new System.Drawing.Point(264, 96);
			this.lblSupplierCountry.Name = "lblSupplierCountry";
			this.lblSupplierCountry.Size = new System.Drawing.Size(52, 20);
			this.lblSupplierCountry.TabIndex = 49;
			this.lblSupplierCountry.Text = "Country";
			this.lblSupplierCountry.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblSupplierPostalCode
			// 
			this.lblSupplierPostalCode.AutoSize = true;
			this.lblSupplierPostalCode.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSupplierPostalCode.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSupplierPostalCode.Location = new System.Drawing.Point(264, 72);
			this.lblSupplierPostalCode.Name = "lblSupplierPostalCode";
			this.lblSupplierPostalCode.Size = new System.Drawing.Size(76, 20);
			this.lblSupplierPostalCode.TabIndex = 48;
			this.lblSupplierPostalCode.Text = "Postal Code";
			this.lblSupplierPostalCode.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtSupplierStateProvince
			// 
			this.txtSupplierStateProvince.Location = new System.Drawing.Point(136, 96);
			this.txtSupplierStateProvince.Name = "txtSupplierStateProvince";
			this.txtSupplierStateProvince.Size = new System.Drawing.Size(112, 24);
			this.txtSupplierStateProvince.TabIndex = 4;
			this.txtSupplierStateProvince.Text = "";
			// 
			// txtSupplierCity
			// 
			this.txtSupplierCity.Location = new System.Drawing.Point(136, 72);
			this.txtSupplierCity.Name = "txtSupplierCity";
			this.txtSupplierCity.Size = new System.Drawing.Size(112, 24);
			this.txtSupplierCity.TabIndex = 2;
			this.txtSupplierCity.Text = "";
			// 
			// txtSupplierAddress
			// 
			this.txtSupplierAddress.Location = new System.Drawing.Point(136, 48);
			this.txtSupplierAddress.Name = "txtSupplierAddress";
			this.txtSupplierAddress.Size = new System.Drawing.Size(320, 24);
			this.txtSupplierAddress.TabIndex = 1;
			this.txtSupplierAddress.Text = "";
			// 
			// txtSupplierName
			// 
			this.txtSupplierName.Location = new System.Drawing.Point(136, 24);
			this.txtSupplierName.Name = "txtSupplierName";
			this.txtSupplierName.Size = new System.Drawing.Size(320, 24);
			this.txtSupplierName.TabIndex = 0;
			this.txtSupplierName.Text = "";
			// 
			// lblSupplierStateProvince
			// 
			this.lblSupplierStateProvince.AutoSize = true;
			this.lblSupplierStateProvince.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSupplierStateProvince.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSupplierStateProvince.Location = new System.Drawing.Point(16, 96);
			this.lblSupplierStateProvince.Name = "lblSupplierStateProvince";
			this.lblSupplierStateProvince.Size = new System.Drawing.Size(101, 20);
			this.lblSupplierStateProvince.TabIndex = 40;
			this.lblSupplierStateProvince.Text = "State / Province";
			this.lblSupplierStateProvince.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSupplierAddress
			// 
			this.lblSupplierAddress.AutoSize = true;
			this.lblSupplierAddress.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSupplierAddress.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSupplierAddress.Location = new System.Drawing.Point(16, 48);
			this.lblSupplierAddress.Name = "lblSupplierAddress";
			this.lblSupplierAddress.Size = new System.Drawing.Size(53, 20);
			this.lblSupplierAddress.TabIndex = 39;
			this.lblSupplierAddress.Text = "Address";
			this.lblSupplierAddress.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSupplierName
			// 
			this.lblSupplierName.AutoSize = true;
			this.lblSupplierName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSupplierName.ForeColor = System.Drawing.Color.Red;
			this.lblSupplierName.Location = new System.Drawing.Point(16, 24);
			this.lblSupplierName.Name = "lblSupplierName";
			this.lblSupplierName.Size = new System.Drawing.Size(40, 20);
			this.lblSupplierName.TabIndex = 38;
			this.lblSupplierName.Text = "Name";
			this.lblSupplierName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnOk
			// 
			this.btnOk.Location = new System.Drawing.Point(360, 528);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 32);
			this.btnOk.TabIndex = 84;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Location = new System.Drawing.Point(168, 528);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(80, 32);
			this.btnHelp.TabIndex = 83;
			this.btnHelp.Text = "Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(264, 528);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 32);
			this.btnCancel.TabIndex = 82;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// fclsDMSuppliers
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(608, 574);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.lsvSuppliers);
			this.Controls.Add(this.lblMessage);
			this.Name = "fclsDMSuppliers";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quick Stock - Suppliers";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.fclsDMSuppliers_Closing);
			this.panel1.ResumeLayout(false);
			this.gpbContactInformation.ResumeLayout(false);
			this.gpbSupplierInformation.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
	
		private void ClearCurrent()
		{
			this.txtCustomerNumber.Text = this.txtSupplierName.Text = this.txtSupplierAddress.Text = "";
			this.txtSupplierCity.Text = this.txtSupplierPostalCode.Text = this.txtSupplierStateProvince.Text = "";
			this.txtSupplierCountry.Text = this.txtContactTitle.Text = this.txtContactFirstName.Text = "";
			this.txtContactLastName.Text = this.txtContactPhoneNumber.Text = this.txtContactFaxNumber.Text = "";
			this.txtContactEmail.Text = "";
			this.ckbStatus.Checked = false;
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private int CheckIfAlreadyInSupplierList(string strSupplierName)
		{
			int intItemIndex = -1;
			
			foreach(SupplierListViewItem slviSupplier in this.lsvSuppliers.Items)
			{
				if(clsUtilities.CompareStrings(slviSupplier.SubItems[0].Text,strSupplierName))
				{
					intItemIndex = slviSupplier.Index;
					break;
				}
			}

			return intItemIndex;
		}

		private void lsvSuppliers_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Determine if clicked column is already the column that is being sorted.
			if (e.Column == m_lvwColumnSorter.SortColumn)
			{
				// Reverse the current sort direction for this column.
				if (m_lvwColumnSorter.Order == SortOrder.Ascending)
				{
					m_lvwColumnSorter.Order = SortOrder.Descending;
				}
				else
				{
					m_lvwColumnSorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
				m_lvwColumnSorter.SortColumn = e.Column;
				m_lvwColumnSorter.Order = SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
			this.lsvSuppliers.Sort();		
		}
		
		private void SetControlsEnabledState(bool blnTextBoxes)
		{
			this.txtSupplierName.Enabled = this.txtSupplierAddress.Enabled = blnTextBoxes;
			this.txtSupplierCity.Enabled = this.txtSupplierPostalCode.Enabled = blnTextBoxes;
			this.txtSupplierStateProvince.Enabled = this.txtSupplierCountry.Enabled = blnTextBoxes;
			this.txtContactTitle.Enabled = this.txtContactFirstName.Enabled = blnTextBoxes;
			this.txtContactLastName.Enabled = this.txtContactPhoneNumber.Enabled = blnTextBoxes;
			this.txtContactFaxNumber.Enabled = this.txtContactEmail.Enabled = blnTextBoxes;
			this.txtCustomerNumber.Enabled = this.ckbStatus.Enabled = blnTextBoxes;
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","ModifySuppliers.htm");
		}

		private void btnNew_Click(object sender, System.EventArgs e)
		{
			if(m_blnNewButton)
			{
				if(this.SaveToListView(0))
				{
					this.ClearCurrent();
					m_blnNewButton = false;
					this.btnNew.Text = "New";
					this.SetControlsEnabledState(false);
				}
			}
			else
			{
				this.lblMessage.Text = "Fill at least all the red labeled Fieds and press the Save button.";
				this.lblMessage.Update();

				this.ClearCurrent();
				m_blnNewButton = true;
				this.btnNew.Text = "Save";
				this.SetControlsEnabledState(true);
				m_slviSelectedItem = null;
			}
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			if(m_slviSelectedItem != null)
			{	
				// If employee was just added, it won't be present in the db
				if(m_slviSelectedItem.State != SupplierListViewItem.LineState.Added)
				{
					m_slviSelectedItem.State = SupplierListViewItem.LineState.Removed;
					m_alSupplierList.Add(m_slviSelectedItem);
				}
				this.lsvSuppliers.Items[this.m_slviSelectedItem.Index].Remove();
				this.lsvSuppliers.Sort();
				this.ClearCurrent();
				this.SetControlsEnabledState(false);
			}		
		}

		private void SaveToDatabase()
		{
			DataRow	dtrNewRow;
			DataRow[] dtrFoundRows;
			
			// Add item in ListView to arraylist that already contains the removed items
			m_alSupplierList.AddRange(this.lsvSuppliers.Items);

			foreach(SupplierListViewItem slviSupplier in m_alSupplierList)
			{
				switch(slviSupplier.State)
				{
					case SupplierListViewItem.LineState.Added:
						dtrNewRow = m_dtaSuppliers.NewRow();

						dtrNewRow["CompanyName"] = slviSupplier.SupplierName;
						dtrNewRow["BillingAdress"] = slviSupplier.SupplierAddress;
						dtrNewRow["City"] = slviSupplier.SupplierCity;
						dtrNewRow["StateOrProvince"] = slviSupplier.SupplierStateProvince;
						dtrNewRow["PostalCode"] = slviSupplier.SupplierPostalCode;
						dtrNewRow["Country"] = slviSupplier.SupplierCountry;
						dtrNewRow["ConTitle"] = slviSupplier.ContactTitle;
						dtrNewRow["ContactFirstName"] = slviSupplier.ContactFirstName;
						dtrNewRow["ContactLastName"] = slviSupplier.ContactLastName;
						dtrNewRow["PhoneNumber"] = slviSupplier.ContactPhoneNumber;
						dtrNewRow["FaxNumber"] = slviSupplier.ContactFaxNumber;
						dtrNewRow["Email"] = slviSupplier.ContactEmail;
						dtrNewRow["CustomId"] = slviSupplier.CustomerNumber;

						if(slviSupplier.SupplierStatus)
							dtrNewRow["Status"] = 1;
						else
							dtrNewRow["Status"] = 0;

						// Add the new row to the table
						m_dtaSuppliers.Rows.Add(dtrNewRow);
						break;

					case SupplierListViewItem.LineState.Edited:
						dtrFoundRows = m_dtaSuppliers.Select("FournisseurId = " + slviSupplier.SupplierId);
						if(dtrFoundRows.Length == 1)
						{
							dtrFoundRows[0]["CompanyName"] = slviSupplier.SupplierName;
							dtrFoundRows[0]["BillingAdress"] = slviSupplier.SupplierAddress;
							dtrFoundRows[0]["City"] = slviSupplier.SupplierCity;
							dtrFoundRows[0]["StateOrProvince"] = slviSupplier.SupplierStateProvince;
							dtrFoundRows[0]["PostalCode"] = slviSupplier.SupplierPostalCode;
							dtrFoundRows[0]["Country"] = slviSupplier.SupplierCountry;
							dtrFoundRows[0]["ConTitle"] = slviSupplier.ContactTitle;
							dtrFoundRows[0]["ContactFirstName"] = slviSupplier.ContactFirstName;
							dtrFoundRows[0]["ContactLastName"] = slviSupplier.ContactLastName;
							dtrFoundRows[0]["PhoneNumber"] = slviSupplier.ContactPhoneNumber;
							dtrFoundRows[0]["FaxNumber"] = slviSupplier.ContactFaxNumber;
							dtrFoundRows[0]["Email"] = slviSupplier.ContactEmail;
							dtrFoundRows[0]["CustomId"] = slviSupplier.CustomerNumber;
							
							if(slviSupplier.SupplierStatus)
								dtrFoundRows[0]["Status"] = 1;
							else
								dtrFoundRows[0]["Status"] = 0;
						}
						break;

					case SupplierListViewItem.LineState.Removed:
						dtrFoundRows = m_dtaSuppliers.Select("FournisseurId = " + slviSupplier.SupplierId);
						if(dtrFoundRows.Length == 1)
							dtrFoundRows[0].Delete();
						break;
				}
			}

			// Update the Database
			try
			{
				m_odaSuppliers.Update(m_dtaSuppliers);
				m_dtaSuppliers.AcceptChanges();

				// Inform the user
				this.lblMessage.Text = "Changes have been saved.";
				this.lblMessage.Update();
							
			} 
			catch (OleDbException ex)
			{
				m_dtaSuppliers.RejectChanges();
				MessageBox.Show(ex.Message);
			}
		}
		
		private bool SaveToListView(int intSelectErrorMessage)
		{
			bool blnCompleted = false;
			int intIdenticalItemIndex = -1;
			SupplierListViewItem slviSupplier;
			
			intIdenticalItemIndex = CheckIfAlreadyInSupplierList(this.txtSupplierName.Text);
			if(this.txtSupplierName.Text.Length >0 && this.txtSupplierCountry.Text.Length > 0 && this.txtContactEmail.Text.Length > 0 && this.txtContactPhoneNumber.Text.Length > 0)
			{
				if(m_blnNewButton)
				{
					if(intIdenticalItemIndex == -1)
					{
						slviSupplier = new SupplierListViewItem(++m_intLastUsedSupplierId,this.txtSupplierName.Text,this.txtSupplierAddress.Text,this.txtSupplierCity.Text,this.txtSupplierStateProvince.Text,this.txtSupplierPostalCode.Text,this.txtSupplierCountry.Text,this.txtContactTitle.Text,this.txtContactFirstName.Text,this.txtContactLastName.Text,this.txtContactPhoneNumber.Text,this.txtContactFaxNumber.Text,this.txtContactEmail.Text,this.txtCustomerNumber.Text,this.ckbStatus.Checked);
						slviSupplier.State = SupplierListViewItem.LineState.Added;
						this.lsvSuppliers.Items.Add(slviSupplier);

						m_blnChangesMade = true;
					
						this.lsvSuppliers.Sort();
						this.ClearCurrent();
						blnCompleted = true;
					}
					else
						MessageBox.Show(this,"This supplier already exists!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				}
				else if(m_slviSelectedItem != null)
				{
					if(intIdenticalItemIndex == -1 || intIdenticalItemIndex == m_slviSelectedItem.Index)
					{
						m_slviSelectedItem.SupplierName = this.txtSupplierName.Text;
						m_slviSelectedItem.SupplierAddress = this.txtSupplierAddress.Text;
						m_slviSelectedItem.SupplierCity = this.txtSupplierCity.Text;
						m_slviSelectedItem.SupplierStateProvince = this.txtSupplierStateProvince.Text;
						m_slviSelectedItem.SupplierPostalCode = this.txtSupplierPostalCode.Text;
						m_slviSelectedItem.SupplierCountry = this.txtSupplierCountry.Text;
						m_slviSelectedItem.ContactTitle = this.txtContactTitle.Text;
						m_slviSelectedItem.ContactFirstName = this.txtContactFirstName.Text;
						m_slviSelectedItem.ContactLastName = this.txtContactLastName.Text;
						m_slviSelectedItem.ContactPhoneNumber = this.txtContactPhoneNumber.Text;
						m_slviSelectedItem.ContactFaxNumber = this.txtContactFaxNumber.Text;
						m_slviSelectedItem.ContactEmail = this.txtContactEmail.Text;
						m_slviSelectedItem.CustomerNumber = this.txtCustomerNumber.Text;
						m_slviSelectedItem.SupplierStatus = this.ckbStatus.Checked;
						m_slviSelectedItem.State = SupplierListViewItem.LineState.Edited;
					
						m_blnChangesMade = true;

						this.lsvSuppliers.Sort();
						this.ClearCurrent();
						blnCompleted = true;
					}
					else
						MessageBox.Show(this,"The item you are trying to add is identical to an item already in the list.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				}
			}
			else
			{
				switch(intSelectErrorMessage)
				{
					case 0:
						MessageBox.Show(this,"Please fill in the red fields!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
					break;

					case 1:
						MessageBox.Show(this,"One or more red fileds have not been filled in. Changes made to this item will not be saved.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
					break;
				}
			}

			return blnCompleted;
		}

		private void fclsDMSuppliers_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			DialogResult dlgResult;

			if(!(m_blnCancelButton || m_blnOkButton) && (m_blnChangesMade || m_alSupplierList.Count > 0))
			{
				dlgResult = MessageBox.Show(this,"Do you want to save the changes before closing?",this.Text,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);

				switch(dlgResult)
				{
					case DialogResult.Yes:
						if(this.SaveToListView(0))
							this.SaveToDatabase();
						else
							e.Cancel = true;
					break;
					
					case DialogResult.Cancel:
						e.Cancel = true;
					break;
				}
			}		
		}

		private void lsvSuppliers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
 
			if(m_slviSelectedItem != null)
				this.SaveToListView(1);

			if(this.lsvSuppliers.SelectedItems.Count != 0)
			{
				m_slviSelectedItem = (SupplierListViewItem) this.lsvSuppliers.SelectedItems[0];
			
				this.txtSupplierName.Text = m_slviSelectedItem.SupplierName;
				this.txtSupplierAddress.Text = m_slviSelectedItem.SupplierAddress;
				this.txtSupplierCity.Text = m_slviSelectedItem.SupplierCity;
				this.txtSupplierPostalCode.Text = m_slviSelectedItem.SupplierPostalCode;
				this.txtSupplierStateProvince.Text = m_slviSelectedItem.SupplierStateProvince;
				this.txtSupplierCountry.Text = m_slviSelectedItem.SupplierCountry;
				this.txtContactTitle.Text = m_slviSelectedItem.ContactTitle;
				this.txtContactFirstName.Text = m_slviSelectedItem.ContactFirstName;
				this.txtContactLastName.Text = m_slviSelectedItem.ContactLastName;
				this.txtContactPhoneNumber.Text = m_slviSelectedItem.ContactPhoneNumber;
				this.txtContactFaxNumber.Text = m_slviSelectedItem.ContactFaxNumber;
				this.txtContactEmail.Text = m_slviSelectedItem.ContactEmail;
				this.txtCustomerNumber.Text = m_slviSelectedItem.CustomerNumber;
				this.ckbStatus.Checked = m_slviSelectedItem.SupplierStatus;
			
				m_blnNewButton = false;
				this.SetControlsEnabledState(true);
			}
			else
				m_slviSelectedItem = null;
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			m_blnCancelButton = true;
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			m_blnOkButton = true;
			if(this.txtSupplierName.Enabled == true)
			{
				if(this.SaveToListView(0))
					m_blnChangesMade = false;
				else
					return;
			}
			
			this.SaveToDatabase();
			this.Close();
		}

	}
	
	public class SupplierListViewItem:ListViewItem
	{	
		public enum LineState
		{
			Added,
			Edited,
			Removed,
			Unchanged
		}
		
		private LineState m_enuLineState;
		private int m_intSupplierId, m_intStatus;
		private string	m_strSupplierName, m_strSupplierAddress, m_strSupplierCity,
						m_strSupplierStateProvince,m_strSupplierPostalCode,m_strSupplierCountry,
						m_strContactTitle,m_strContactFirstName,m_strContactLastName,m_strContactPhoneNumber,
						m_strContactFaxNumber,m_strContactEmail,m_strCustomerNumber;
		
		public SupplierListViewItem(int intSupplierId, string strSupplierName, string strSupplierAddress, string strSupplierCity, string strSupplierStateProvince, string strSupplierPostalCode, string strSupplierCountry, string strContactTitle, string strContactFirstName, string strContactLastName, string strContactPhoneNumber, string strContactFaxNumber, string strContactEmail, string strCustomerNumber, int intStatus)
		{
			this.Text = "";
			this.SubItems.Add("");
			this.SubItems.Add("");
			this.SubItems.Add("");

			m_intSupplierId = intSupplierId;
			this.SupplierName = strSupplierName;
			this.SupplierAddress = strSupplierAddress;
			this.SupplierCity = strSupplierCity;
			this.SupplierStateProvince = strSupplierStateProvince;
			this.SupplierPostalCode = strSupplierPostalCode;
			this.SupplierCountry = strSupplierCountry;
			this.ContactTitle = strContactTitle;
			this.ContactFirstName = strContactFirstName;
			this.ContactLastName = strContactLastName;
			this.ContactPhoneNumber = strContactPhoneNumber;
			this.ContactFaxNumber = strContactFaxNumber;
			this.ContactEmail = strContactEmail;
			this.CustomerNumber = strCustomerNumber;
			m_intStatus = intStatus;
			
			if(m_intStatus == 1)
				this.SupplierStatus = true;
			else
				this.SupplierStatus = false;

			this.State = LineState.Unchanged;
		}

		public SupplierListViewItem(int intSupplierId, string strSupplierName, string strSupplierAddress, string strSupplierCity, string strSupplierStateProvince, string strSupplierPostalCode, string strSupplierCountry, string strContactTitle, string strContactFirstName, string strContactLastName, string strContactPhoneNumber, string strContactFaxNumber, string strContactEmail, string strCustomerNumber, bool blnStatus)
		{
			this.Text = "";
			this.SubItems.Add("");
			this.SubItems.Add("");
			this.SubItems.Add("");

			m_intSupplierId = intSupplierId;
			this.SupplierName = strSupplierName;
			this.SupplierAddress = strSupplierAddress;
			this.SupplierCity = strSupplierCity;
			this.SupplierStateProvince = strSupplierStateProvince;
			this.SupplierPostalCode = strSupplierPostalCode;
			this.SupplierCountry = strSupplierCountry;
			this.ContactTitle = strContactTitle;
			this.ContactFirstName = strContactFirstName;
			this.ContactLastName = strContactLastName;
			this.ContactPhoneNumber = strContactPhoneNumber;
			this.ContactFaxNumber = strContactFaxNumber;
			this.ContactEmail = strContactEmail;
			this.CustomerNumber = strCustomerNumber;
			this.SupplierStatus = blnStatus;
		
			this.State = LineState.Unchanged;
		}

		public int SupplierId
		{
			get
			{
				return m_intSupplierId;
			}
		}

		public string SupplierName
		{
			get
			{
				return m_strSupplierName;
			}
			set
			{
				m_strSupplierName = value;
				this.SubItems[0].Text = m_strSupplierName;
			}
		}

		public string SupplierAddress
		{
			get
			{
				return m_strSupplierAddress;
			}
			set
			{
				m_strSupplierAddress = value;
			}
		}

		public string SupplierCity
		{
			get
			{
				return m_strSupplierCity;
			}
			set
			{
				m_strSupplierCity = value;
			}
		}

		public string SupplierStateProvince
		{
			get
			{
				return m_strSupplierStateProvince;
			}
			set
			{
				m_strSupplierStateProvince = value;
			}
		}
		
		public string SupplierPostalCode
		{
			get
			{
				return m_strSupplierPostalCode;
			}
			set
			{
				m_strSupplierPostalCode = value;
			}
		}

		public string SupplierCountry
		{
			get
			{
				return m_strSupplierCountry;
			}
			set
			{
				m_strSupplierCountry = value;
			}
        }

		public string ContactTitle
		{
			get
			{
				return m_strContactTitle;
			}
			set
			{
				m_strContactTitle = value;
				this.SubItems[1].Text = this.FormatName(m_strContactTitle,this.ContactFirstName,this.ContactLastName);
			}
		}

		public string ContactFirstName
		{
			get
			{
				return m_strContactFirstName;
			}
			set
			{
				m_strContactFirstName = value;
				this.SubItems[1].Text = this.FormatName(this.ContactTitle,m_strContactFirstName,this.ContactLastName);
			}
		}
		
		public string ContactLastName
		{
			get
			{
				return m_strContactLastName;
			}
			set
			{
				m_strContactLastName = value;
				this.SubItems[1].Text = this.FormatName(this.ContactTitle,this.ContactFirstName,m_strContactLastName);
			}
		}

		public string ContactPhoneNumber
		{
			get
			{
				return m_strContactPhoneNumber;
			}
			set
			{
				m_strContactPhoneNumber = value;
			}		
		}

		public string ContactFaxNumber
		{
			get
			{
				return m_strContactFaxNumber;
			}
			set
			{
				m_strContactFaxNumber = value;
			}		
		}

		public string ContactEmail
		{
			get
			{
				return m_strContactEmail;
			}
			set
			{
				m_strContactEmail = value;
			}		
		}

		public string CustomerNumber
		{
			get
			{
				return m_strCustomerNumber;
			}
			set
			{
				m_strCustomerNumber = value;
				this.SubItems[2].Text = m_strCustomerNumber;
			}		
		}

		public bool SupplierStatus
		{
			get
			{
				if(m_intStatus == 1)
					return true;
				else
					return false;
			}
			set
			{
				this.SubItems[3].Text = value.ToString();

				if(value == true)
					m_intStatus = 1;
				else
					m_intStatus = 0;
			}
		}

		public int DbEmployeeStatus
		{
			get
			{
				return m_intStatus;
			}
		}

		public LineState State
		{
			get
			{
				return m_enuLineState;
			}
			set
			{
				if(!(m_enuLineState == LineState.Added && value == LineState.Edited))
					m_enuLineState = value;
			}
		}

		private string FormatName(string strTitle, string strFirstName, string strLastName)
		{
			string strFormattedName = "";

			if(strTitle != null && strTitle.Length > 0)
				strFormattedName += strTitle + " ";

			if(strFirstName != null && strFirstName.Length > 0)
				strFormattedName += strFirstName + " ";

			if(strLastName != null && strLastName.Length > 0)
				strFormattedName += strLastName;

			return strFormattedName;
		}

	}
}
