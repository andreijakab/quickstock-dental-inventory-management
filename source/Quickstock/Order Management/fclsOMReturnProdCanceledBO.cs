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
	/// Summary description for fclsOMReturnProdCanceledBO.
	/// </summary>
	public class fclsOMReturnProdCanceledBO : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.ListView lsvReturnedProducts;
		private System.Windows.Forms.ColumnHeader colProduct;
		private System.Windows.Forms.ColumnHeader colUnits;
		private System.Windows.Forms.ColumnHeader colTrademark;
		private System.Windows.Forms.GroupBox gpbSupplier;
		private System.Windows.Forms.Label lblSupplierContact_Data;
		private System.Windows.Forms.Label lblSupplierContact;
		private System.Windows.Forms.Label lblSupplierPhone_Data;
		private System.Windows.Forms.Label lblSupplierPhone;
		private System.Windows.Forms.Label lblSupplierName_Data;
		private System.Windows.Forms.Label lblSupplierName;
		private System.Windows.Forms.Button btnSendEmail;
		private System.Windows.Forms.Label lblOrderNr;
		private System.Windows.Forms.Label lblReturnNo;
		private System.Windows.Forms.TextBox txtReturnNr;
        private System.Windows.Forms.Label lblEmployee;
        private System.Windows.Forms.ComboBox cmbEmployee;
        private System.Windows.Forms.Label lblDate;
        private System.Windows.Forms.DateTimePicker dtpDate;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		public enum Caller:int	{ BackOrders, CheckOrders, ViewOrders };

		public struct ReturnedProduct
		{
			public string Product;
			public string Trademark;
			public int UnitsReturned;
		}

        private ArrayList               m_alProductList;
		private Caller			        m_cCaller;
		private DataTable		        m_dtaEmployees;
		private Form			        m_frmCaller;
    	private OleDbConnection         m_odcConnection;
          
		public fclsOMReturnProdCanceledBO(Form frmCaller,
										  Caller cCaller,
										  string strOrderID,
										  SupplierInformation siSupplier,
                                          ArrayList alProducts,
										  OleDbConnection odcConnection)
		{
            int intTemp;
            ListViewItem lviItem;
            OleDbDataAdapter odaEmployees;

			InitializeComponent();

            m_alProductList = alProducts;
			m_cCaller = cCaller;
			m_frmCaller = frmCaller;
			m_odcConnection = odcConnection;
			
			// add products to listview
            if (m_cCaller == Caller.BackOrders)
            {
                clsBackorderListViewItem Product;

                foreach (Object objProduct in m_alProductList)
                {
                    Product = (clsBackorderListViewItem) objProduct;

                    lviItem = this.lsvReturnedProducts.Items.Add(Product.ProductName);
                    lviItem.SubItems.Add(Product.NUnitsBackordered.ToString());
                    lviItem.SubItems.Add(Product.Trademark);
                }
            }
            else
            {
                ReturnedProduct rpCurrentProduct;

                foreach (Object objProduct in m_alProductList)
                {
                    rpCurrentProduct = (ReturnedProduct) objProduct;

                    lviItem = this.lsvReturnedProducts.Items.Add(rpCurrentProduct.Product);
                    lviItem.SubItems.Add(rpCurrentProduct.UnitsReturned.ToString());
                    lviItem.SubItems.Add(rpCurrentProduct.Trademark);
                }
            }

            // display order number and supplier information
            this.lblOrderNr.Text = "Order Nr. " + strOrderID;
            this.lblSupplierName_Data.Text = siSupplier.Name;
            this.lblSupplierContact_Data.Text = siSupplier.ContactName;
            this.lblSupplierPhone_Data.Text = siSupplier.PhoneNumber;

            // perform caller-specific customization
            switch (m_cCaller)
            {
                case Caller.BackOrders:
                    this.Text = "Quick Stock - Cancel Backordered Product";
                    this.lblReturnNo.Visible = false;
                    this.txtReturnNr.Visible = false;

                    // load employees data
                    m_dtaEmployees = new DataTable();
                    odaEmployees = new OleDbDataAdapter("SELECT * FROM [Employees] WHERE Status = 1 ORDER BY LastName, FirstName", m_odcConnection);
                    odaEmployees.Fill(m_dtaEmployees);
                break;

                case Caller.CheckOrders:
                    this.dtpDate.Enabled = false;
                    this.cmbEmployee.Enabled = false;

                    // load employees data
                    m_dtaEmployees = new DataTable();
                    odaEmployees = new OleDbDataAdapter("SELECT * FROM [Employees] WHERE Status = 1 ORDER BY LastName, FirstName", m_odcConnection);
                    odaEmployees.Fill(m_dtaEmployees);
                break;

                case Caller.ViewOrders:
                    // load employees data
                    m_dtaEmployees = new DataTable();
                    odaEmployees = new OleDbDataAdapter("SELECT * FROM [Employees] ORDER BY LastName, FirstName", m_odcConnection);
                    odaEmployees.Fill(m_dtaEmployees);
                break;
            }

            // add employees to combo-box
            foreach (DataRow dtrEmployee in m_dtaEmployees.Rows)
                this.cmbEmployee.Items.Add(clsUtilities.FormatName_List(dtrEmployee["Title"].ToString(),
                                                                        dtrEmployee["FirstName"].ToString(),
                                                                        dtrEmployee["LastName"].ToString()));
            // Set default employee according to configuration file
            intTemp = clsConfiguration.Internal_CurrentUserID;
            this.cmbEmployee.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intTemp, m_dtaEmployees, 0);
        }

		public void SetReturnInformation(DateTime dtReturnDate, string strReturnNr, int intReturnByID)
		{
			this.dtpDate.Value = dtReturnDate;
			this.txtReturnNr.Text = strReturnNr;
			this.cmbEmployee.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intReturnByID, m_dtaEmployees, 0);
		}

		public void SetReturnInformation(int intReturnByID)
		{
			this.dtpDate.Value = DateTime.Now;
			this.txtReturnNr.Text = "0";
			this.cmbEmployee.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intReturnByID, m_dtaEmployees, 0);
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
            this.lblReturnNo = new System.Windows.Forms.Label();
            this.txtReturnNr = new System.Windows.Forms.TextBox();
            this.lblOrderNr = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lsvReturnedProducts = new System.Windows.Forms.ListView();
            this.colProduct = new System.Windows.Forms.ColumnHeader();
            this.colUnits = new System.Windows.Forms.ColumnHeader();
            this.colTrademark = new System.Windows.Forms.ColumnHeader();
            this.gpbSupplier = new System.Windows.Forms.GroupBox();
            this.lblSupplierContact_Data = new System.Windows.Forms.Label();
            this.lblSupplierContact = new System.Windows.Forms.Label();
            this.lblSupplierPhone_Data = new System.Windows.Forms.Label();
            this.lblSupplierPhone = new System.Windows.Forms.Label();
            this.lblSupplierName_Data = new System.Windows.Forms.Label();
            this.lblSupplierName = new System.Windows.Forms.Label();
            this.btnSendEmail = new System.Windows.Forms.Button();
            this.cmbEmployee = new System.Windows.Forms.ComboBox();
            this.dtpDate = new System.Windows.Forms.DateTimePicker();
            this.lblDate = new System.Windows.Forms.Label();
            this.gpbSupplier.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblReturnNo
            // 
            this.lblReturnNo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnNo.ForeColor = System.Drawing.Color.Red;
            this.lblReturnNo.Location = new System.Drawing.Point(8, 368);
            this.lblReturnNo.Name = "lblReturnNo";
            this.lblReturnNo.Size = new System.Drawing.Size(80, 16);
            this.lblReturnNo.TabIndex = 6;
            this.lblReturnNo.Text = "Return Nr.";
            // 
            // txtReturnNr
            // 
            this.txtReturnNr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtReturnNr.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtReturnNr.Location = new System.Drawing.Point(104, 365);
            this.txtReturnNr.Name = "txtReturnNr";
            this.txtReturnNr.Size = new System.Drawing.Size(192, 22);
            this.txtReturnNr.TabIndex = 0;
            this.txtReturnNr.Text = "0";
            // 
            // lblOrderNr
            // 
            this.lblOrderNr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNr.ForeColor = System.Drawing.Color.Red;
            this.lblOrderNr.Location = new System.Drawing.Point(16, 8);
            this.lblOrderNr.Name = "lblOrderNr";
            this.lblOrderNr.Size = new System.Drawing.Size(472, 32);
            this.lblOrderNr.TabIndex = 8;
            this.lblOrderNr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(388, 400);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblEmployee
            // 
            this.lblEmployee.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployee.ForeColor = System.Drawing.Color.Red;
            this.lblEmployee.Location = new System.Drawing.Point(8, 336);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(96, 16);
            this.lblEmployee.TabIndex = 12;
            this.lblEmployee.Text = "Returned by";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(280, 400);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 23);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lsvReturnedProducts
            // 
            this.lsvReturnedProducts.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colProduct,
            this.colUnits,
            this.colTrademark});
            this.lsvReturnedProducts.FullRowSelect = true;
            this.lsvReturnedProducts.Location = new System.Drawing.Point(8, 40);
            this.lsvReturnedProducts.MultiSelect = false;
            this.lsvReturnedProducts.Name = "lsvReturnedProducts";
            this.lsvReturnedProducts.Size = new System.Drawing.Size(480, 97);
            this.lsvReturnedProducts.TabIndex = 13;
            this.lsvReturnedProducts.UseCompatibleStateImageBehavior = false;
            this.lsvReturnedProducts.View = System.Windows.Forms.View.Details;
            // 
            // colProduct
            // 
            this.colProduct.Text = "Product";
            this.colProduct.Width = 300;
            // 
            // colUnits
            // 
            this.colUnits.Text = "Units";
            this.colUnits.Width = 40;
            // 
            // colTrademark
            // 
            this.colTrademark.Text = "Trademark";
            this.colTrademark.Width = 136;
            // 
            // gpbSupplier
            // 
            this.gpbSupplier.Controls.Add(this.lblSupplierContact_Data);
            this.gpbSupplier.Controls.Add(this.lblSupplierContact);
            this.gpbSupplier.Controls.Add(this.lblSupplierPhone_Data);
            this.gpbSupplier.Controls.Add(this.lblSupplierPhone);
            this.gpbSupplier.Controls.Add(this.lblSupplierName_Data);
            this.gpbSupplier.Controls.Add(this.lblSupplierName);
            this.gpbSupplier.Controls.Add(this.btnSendEmail);
            this.gpbSupplier.Location = new System.Drawing.Point(8, 144);
            this.gpbSupplier.Name = "gpbSupplier";
            this.gpbSupplier.Size = new System.Drawing.Size(480, 152);
            this.gpbSupplier.TabIndex = 17;
            this.gpbSupplier.TabStop = false;
            this.gpbSupplier.Text = "Supplier Contact Information";
            // 
            // lblSupplierContact_Data
            // 
            this.lblSupplierContact_Data.BackColor = System.Drawing.Color.White;
            this.lblSupplierContact_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSupplierContact_Data.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierContact_Data.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSupplierContact_Data.Location = new System.Drawing.Point(96, 52);
            this.lblSupplierContact_Data.Name = "lblSupplierContact_Data";
            this.lblSupplierContact_Data.Size = new System.Drawing.Size(378, 24);
            this.lblSupplierContact_Data.TabIndex = 11;
            this.lblSupplierContact_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupplierContact
            // 
            this.lblSupplierContact.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierContact.ForeColor = System.Drawing.Color.Red;
            this.lblSupplierContact.Location = new System.Drawing.Point(8, 56);
            this.lblSupplierContact.Name = "lblSupplierContact";
            this.lblSupplierContact.Size = new System.Drawing.Size(72, 16);
            this.lblSupplierContact.TabIndex = 10;
            this.lblSupplierContact.Text = "Contact";
            // 
            // lblSupplierPhone_Data
            // 
            this.lblSupplierPhone_Data.BackColor = System.Drawing.Color.White;
            this.lblSupplierPhone_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSupplierPhone_Data.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierPhone_Data.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSupplierPhone_Data.Location = new System.Drawing.Point(96, 84);
            this.lblSupplierPhone_Data.Name = "lblSupplierPhone_Data";
            this.lblSupplierPhone_Data.Size = new System.Drawing.Size(192, 24);
            this.lblSupplierPhone_Data.TabIndex = 9;
            this.lblSupplierPhone_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupplierPhone
            // 
            this.lblSupplierPhone.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierPhone.ForeColor = System.Drawing.Color.Red;
            this.lblSupplierPhone.Location = new System.Drawing.Point(8, 88);
            this.lblSupplierPhone.Name = "lblSupplierPhone";
            this.lblSupplierPhone.Size = new System.Drawing.Size(72, 16);
            this.lblSupplierPhone.TabIndex = 8;
            this.lblSupplierPhone.Text = "Phone";
            // 
            // lblSupplierName_Data
            // 
            this.lblSupplierName_Data.BackColor = System.Drawing.Color.White;
            this.lblSupplierName_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblSupplierName_Data.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierName_Data.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSupplierName_Data.Location = new System.Drawing.Point(96, 20);
            this.lblSupplierName_Data.Name = "lblSupplierName_Data";
            this.lblSupplierName_Data.Size = new System.Drawing.Size(378, 24);
            this.lblSupplierName_Data.TabIndex = 7;
            this.lblSupplierName_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupplierName
            // 
            this.lblSupplierName.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierName.ForeColor = System.Drawing.Color.Red;
            this.lblSupplierName.Location = new System.Drawing.Point(8, 24);
            this.lblSupplierName.Name = "lblSupplierName";
            this.lblSupplierName.Size = new System.Drawing.Size(72, 16);
            this.lblSupplierName.TabIndex = 6;
            this.lblSupplierName.Text = "Supplier";
            // 
            // btnSendEmail
            // 
            this.btnSendEmail.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnSendEmail.Location = new System.Drawing.Point(190, 120);
            this.btnSendEmail.Name = "btnSendEmail";
            this.btnSendEmail.Size = new System.Drawing.Size(100, 23);
            this.btnSendEmail.TabIndex = 18;
            this.btnSendEmail.Text = "Send Email";
            this.btnSendEmail.Click += new System.EventHandler(this.btnSendEmail_Click);
            // 
            // cmbEmployee
            // 
            this.cmbEmployee.Location = new System.Drawing.Point(104, 334);
            this.cmbEmployee.Name = "cmbEmployee";
            this.cmbEmployee.Size = new System.Drawing.Size(384, 21);
            this.cmbEmployee.TabIndex = 18;
            // 
            // dtpDate
            // 
            this.dtpDate.Location = new System.Drawing.Point(104, 302);
            this.dtpDate.Name = "dtpDate";
            this.dtpDate.Size = new System.Drawing.Size(160, 20);
            this.dtpDate.TabIndex = 19;
            // 
            // lblDate
            // 
            this.lblDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDate.ForeColor = System.Drawing.Color.Red;
            this.lblDate.Location = new System.Drawing.Point(8, 304);
            this.lblDate.Name = "lblDate";
            this.lblDate.Size = new System.Drawing.Size(96, 16);
            this.lblDate.TabIndex = 20;
            this.lblDate.Text = "Return Date";
            // 
            // fclsOMReturnProdCanceledBO
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(494, 432);
            this.Controls.Add(this.lblDate);
            this.Controls.Add(this.dtpDate);
            this.Controls.Add(this.cmbEmployee);
            this.Controls.Add(this.gpbSupplier);
            this.Controls.Add(this.lsvReturnedProducts);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.lblEmployee);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.lblOrderNr);
            this.Controls.Add(this.txtReturnNr);
            this.Controls.Add(this.lblReturnNo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fclsOMReturnProdCanceledBO";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quick Stock - Returned Product(s)";
            this.gpbSupplier.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion


		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			string strReturnNr = this.txtReturnNr.Text;

			if(strReturnNr.Length == 0)
				strReturnNr = "0";
			
			if(this.cmbEmployee.SelectedIndex > -1)
			{
				switch(m_cCaller)
				{
                    case Caller.BackOrders:
                        clsBackorderListViewItem blviItem;

                        foreach (Object obj in m_alProductList)
                        {
                            blviItem = (clsBackorderListViewItem)obj;

                            blviItem.LastChanged = DateTime.Now;
                            blviItem.NUnitsBackordered = 0;
                            blviItem.State = clsBackorderListViewItem.ChangeState.Canceled;
                        }

                        ((fclsOMBackOrders) m_frmCaller).UtilityFormChangedData((int)m_dtaEmployees.Rows[this.cmbEmployee.SelectedIndex]["EmployeeId"]);
                    break;

					case Caller.CheckOrders:
						((fclsOMCheckOrders) m_frmCaller).SetReturnedProductInformation(strReturnNr);
					break;

					case Caller.ViewOrders:
						((fclsOIViewOrders) m_frmCaller).SetReturnedProductInformation(this.dtpDate.Value,
																					   (int) m_dtaEmployees.Rows[this.cmbEmployee.SelectedIndex]["EmployeeId"],
																					   strReturnNr);
					break;
				}
                
                this.Close();
			}
			else
				MessageBox.Show("An employee must be first selected from the list.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

        private void btnSendEmail_Click(object sender, EventArgs e)
        {

        }
	}
}
