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
	/// Summary description for frmReturnedProducts.
	/// </summary>
	public class fclsGENBackOrders : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gpbPastOrders;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.GroupBox gpbSearchBy;
		public System.Windows.Forms.ListView lstViewOrders;
		private System.Windows.Forms.ColumnHeader prodName;
		private System.Windows.Forms.ColumnHeader subProdName;
		private System.Windows.Forms.ColumnHeader marCom;
		private System.Windows.Forms.ColumnHeader pack;
		private System.Windows.Forms.ColumnHeader updateDate;
		private System.Windows.Forms.ColumnHeader backorder;				
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label lblReceivedDate;
		private System.Windows.Forms.Label lblOrderedDate;
		private System.Windows.Forms.Label lblOrderdBy;
		private System.Windows.Forms.Label label8;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.ColumnHeader ordered;
		private System.Windows.Forms.Label lblPhone;
		private System.Windows.Forms.Label lblContact;
		private System.Windows.Forms.Label lblSupplier;
		private System.Windows.Forms.Label lblFax;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.ComboBox cmbCancelledBy;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button cmdCancelation;
/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.Windows.Forms.ListBox lbxOrderNumber;
		private System.ComponentModel.Container components = null;
		private OleDbConnection		m_odcConnection;
		private bool				m_blnCancelationSent = false;
		private DataSet				dataSet;
		private int					nOrd = 0, nrTotProd = 0, nrTotBO = 0;
		private static int			nrOrder = 0;
		public int []				intModBO = new int [15];
		private static string		m_strOrderSelect = "", m_strSuplId;
		private string				orderId, subPrId, subPrName, productName, prodId;
		OleDbDataAdapter			m_odaBackOrders, m_odaOrder, m_odaSaveOrder, m_odaCancelOrder;
		DataTable					m_dtBackOrders, m_dtOrder, m_dtSaveOrder, m_dtCancelOrder;
		DataRow						m_drBackOrders, m_drOrder, m_drSaveOrder;
		OleDbDataAdapter			m_odaSupplier;
		DataTable					m_dtSupplier = new DataTable("Suppliers");				
		OleDbDataAdapter			m_odaEmploye;
		DataTable					m_dtEmploye = new DataTable("Employees");
		DataRow						m_drEmploye, m_drSupplier;
		OleDbCommandBuilder			ocbSaveOrder;

        private SupplierInformation         m_siSupplier;

		public fclsGENBackOrders(OleDbConnection odcConnection)
		{
			InitializeComponent();

			m_odcConnection = odcConnection;
			m_odaSupplier = new OleDbDataAdapter("Select * From [Suppliers] Order by CompanyName", m_odcConnection);
			m_odaEmploye = new OleDbDataAdapter("Select * From [Employees] Order by FirstName, LastName", m_odcConnection);
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
			this.gpbPastOrders = new System.Windows.Forms.GroupBox();
			this.lblEmail = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.lblFax = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.lblPhone = new System.Windows.Forms.Label();
			this.lblContact = new System.Windows.Forms.Label();
			this.lblSupplier = new System.Windows.Forms.Label();
			this.label8 = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.cmdClose = new System.Windows.Forms.Button();
			this.gpbSearchBy = new System.Windows.Forms.GroupBox();
			this.lblReceivedDate = new System.Windows.Forms.Label();
			this.lblOrderedDate = new System.Windows.Forms.Label();
			this.lblOrderdBy = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lbxOrderNumber = new System.Windows.Forms.ListBox();
			this.lstViewOrders = new System.Windows.Forms.ListView();
			this.updateDate = new System.Windows.Forms.ColumnHeader();
			this.prodName = new System.Windows.Forms.ColumnHeader();
			this.subProdName = new System.Windows.Forms.ColumnHeader();
			this.marCom = new System.Windows.Forms.ColumnHeader();
			this.ordered = new System.Windows.Forms.ColumnHeader();
			this.backorder = new System.Windows.Forms.ColumnHeader();
			this.pack = new System.Windows.Forms.ColumnHeader();
			this.btnHelp = new System.Windows.Forms.Button();
			this.label16 = new System.Windows.Forms.Label();
			this.cmbCancelledBy = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.cmdCancelation = new System.Windows.Forms.Button();
			this.gpbPastOrders.SuspendLayout();
			this.gpbSearchBy.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpbPastOrders
			// 
			this.gpbPastOrders.Controls.Add(this.lblEmail);
			this.gpbPastOrders.Controls.Add(this.label14);
			this.gpbPastOrders.Controls.Add(this.lblFax);
			this.gpbPastOrders.Controls.Add(this.label12);
			this.gpbPastOrders.Controls.Add(this.lblPhone);
			this.gpbPastOrders.Controls.Add(this.lblContact);
			this.gpbPastOrders.Controls.Add(this.lblSupplier);
			this.gpbPastOrders.Controls.Add(this.label8);
			this.gpbPastOrders.Controls.Add(this.label9);
			this.gpbPastOrders.Controls.Add(this.label10);
			this.gpbPastOrders.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gpbPastOrders.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.gpbPastOrders.Location = new System.Drawing.Point(416, 8);
			this.gpbPastOrders.Name = "gpbPastOrders";
			this.gpbPastOrders.Size = new System.Drawing.Size(560, 128);
			this.gpbPastOrders.TabIndex = 12;
			this.gpbPastOrders.TabStop = false;
			this.gpbPastOrders.Text = "Supplier Info:";
			// 
			// lblEmail
			// 
			this.lblEmail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblEmail.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblEmail.Location = new System.Drawing.Point(88, 96);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(440, 24);
			this.lblEmail.TabIndex = 37;
			this.lblEmail.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label14.Location = new System.Drawing.Point(16, 96);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(72, 24);
			this.label14.TabIndex = 36;
			this.label14.Text = "Email:";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblFax
			// 
			this.lblFax.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblFax.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblFax.Location = new System.Drawing.Point(344, 72);
			this.lblFax.Name = "lblFax";
			this.lblFax.Size = new System.Drawing.Size(184, 24);
			this.lblFax.TabIndex = 35;
			this.lblFax.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label12
			// 
			this.label12.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label12.Location = new System.Drawing.Point(288, 72);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(56, 24);
			this.label12.TabIndex = 34;
			this.label12.Text = "Fax:";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblPhone
			// 
			this.lblPhone.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblPhone.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPhone.Location = new System.Drawing.Point(88, 72);
			this.lblPhone.Name = "lblPhone";
			this.lblPhone.Size = new System.Drawing.Size(184, 24);
			this.lblPhone.TabIndex = 33;
			this.lblPhone.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblContact
			// 
			this.lblContact.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblContact.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblContact.Location = new System.Drawing.Point(88, 48);
			this.lblContact.Name = "lblContact";
			this.lblContact.Size = new System.Drawing.Size(440, 24);
			this.lblContact.TabIndex = 32;
			this.lblContact.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSupplier
			// 
			this.lblSupplier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblSupplier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSupplier.Location = new System.Drawing.Point(88, 24);
			this.lblSupplier.Name = "lblSupplier";
			this.lblSupplier.Size = new System.Drawing.Size(440, 24);
			this.lblSupplier.TabIndex = 31;
			this.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label8
			// 
			this.label8.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label8.Location = new System.Drawing.Point(16, 72);
			this.label8.Name = "label8";
			this.label8.Size = new System.Drawing.Size(72, 24);
			this.label8.TabIndex = 30;
			this.label8.Text = "Phone:";
			this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.Location = new System.Drawing.Point(16, 48);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(72, 24);
			this.label9.TabIndex = 29;
			this.label9.Text = "Contact:";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label10.Location = new System.Drawing.Point(16, 24);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(72, 24);
			this.label10.TabIndex = 28;
			this.label10.Text = "Company:";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cmdClose
			// 
			this.cmdClose.Location = new System.Drawing.Point(800, 424);
			this.cmdClose.Name = "cmdClose";
			this.cmdClose.Size = new System.Drawing.Size(96, 32);
			this.cmdClose.TabIndex = 16;
			this.cmdClose.Text = "Close";
			this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
			// 
			// gpbSearchBy
			// 
			this.gpbSearchBy.Controls.Add(this.lblReceivedDate);
			this.gpbSearchBy.Controls.Add(this.lblOrderedDate);
			this.gpbSearchBy.Controls.Add(this.lblOrderdBy);
			this.gpbSearchBy.Controls.Add(this.label4);
			this.gpbSearchBy.Controls.Add(this.label3);
			this.gpbSearchBy.Controls.Add(this.label2);
			this.gpbSearchBy.Controls.Add(this.label1);
			this.gpbSearchBy.Controls.Add(this.lbxOrderNumber);
			this.gpbSearchBy.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gpbSearchBy.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.gpbSearchBy.Location = new System.Drawing.Point(16, 8);
			this.gpbSearchBy.Name = "gpbSearchBy";
			this.gpbSearchBy.Size = new System.Drawing.Size(392, 152);
			this.gpbSearchBy.TabIndex = 17;
			this.gpbSearchBy.TabStop = false;
			this.gpbSearchBy.Text = "Order Info:";
			// 
			// lblReceivedDate
			// 
			this.lblReceivedDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblReceivedDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblReceivedDate.Location = new System.Drawing.Point(256, 72);
			this.lblReceivedDate.Name = "lblReceivedDate";
			this.lblReceivedDate.Size = new System.Drawing.Size(120, 24);
			this.lblReceivedDate.TabIndex = 27;
			this.lblReceivedDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblOrderedDate
			// 
			this.lblOrderedDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblOrderedDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblOrderedDate.Location = new System.Drawing.Point(256, 48);
			this.lblOrderedDate.Name = "lblOrderedDate";
			this.lblOrderedDate.Size = new System.Drawing.Size(120, 24);
			this.lblOrderedDate.TabIndex = 26;
			this.lblOrderedDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblOrderdBy
			// 
			this.lblOrderdBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblOrderdBy.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblOrderdBy.Location = new System.Drawing.Point(144, 120);
			this.lblOrderdBy.Name = "lblOrderdBy";
			this.lblOrderdBy.Size = new System.Drawing.Size(232, 24);
			this.lblOrderdBy.TabIndex = 25;
			this.lblOrderdBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.Location = new System.Drawing.Point(152, 72);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(104, 24);
			this.label4.TabIndex = 24;
			this.label4.Text = "Received Date:";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.Location = new System.Drawing.Point(152, 48);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 24);
			this.label3.TabIndex = 23;
			this.label3.Text = "Ordered Date:";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.Location = new System.Drawing.Point(16, 120);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(112, 24);
			this.label2.TabIndex = 22;
			this.label2.Text = "Ordered By:";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Arial", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.TabIndex = 21;
			this.label1.Text = "Order Nr.";
			// 
			// lbxOrderNumber
			// 
			this.lbxOrderNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbxOrderNumber.ItemHeight = 16;
			this.lbxOrderNumber.Location = new System.Drawing.Point(16, 48);
			this.lbxOrderNumber.Name = "lbxOrderNumber";
			this.lbxOrderNumber.Size = new System.Drawing.Size(128, 68);
			this.lbxOrderNumber.TabIndex = 20;
			this.lbxOrderNumber.SelectedIndexChanged += new System.EventHandler(this.lbxOrderNumber_SelectedIndexChanged);
			// 
			// lstViewOrders
			// 
			this.lstViewOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																							this.updateDate,
																							this.prodName,
																							this.subProdName,
																							this.marCom,
																							this.ordered,
																							this.backorder,
																							this.pack});
			this.lstViewOrders.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lstViewOrders.FullRowSelect = true;
			this.lstViewOrders.Location = new System.Drawing.Point(16, 192);
			this.lstViewOrders.Name = "lstViewOrders";
			this.lstViewOrders.Size = new System.Drawing.Size(960, 208);
			this.lstViewOrders.TabIndex = 18;
			this.lstViewOrders.View = System.Windows.Forms.View.Details;
			// 
			// updateDate
			// 
			this.updateDate.Text = "Last Update";
			this.updateDate.Width = 80;
			// 
			// prodName
			// 
			this.prodName.Text = "Product Name";
			this.prodName.Width = 220;
			// 
			// subProdName
			// 
			this.subProdName.Text = "Sub-Product Name";
			this.subProdName.Width = 238;
			// 
			// marCom
			// 
			this.marCom.Text = "Trademark";
			this.marCom.Width = 150;
			// 
			// ordered
			// 
			this.ordered.Text = "Ordered";
			this.ordered.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.ordered.Width = 71;
			// 
			// backorder
			// 
			this.backorder.Text = "Backorder";
			this.backorder.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.backorder.Width = 74;
			// 
			// pack
			// 
			this.pack.Text = "Packaging";
			this.pack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.pack.Width = 120;
			// 
			// btnHelp
			// 
			this.btnHelp.Location = new System.Drawing.Point(904, 424);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(72, 32);
			this.btnHelp.TabIndex = 26;
			this.btnHelp.Text = "Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// label16
			// 
			this.label16.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label16.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label16.Location = new System.Drawing.Point(416, 144);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(112, 24);
			this.label16.TabIndex = 27;
			this.label16.Text = "Cancelled By:";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// cmbCancelledBy
			// 
			this.cmbCancelledBy.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmbCancelledBy.Location = new System.Drawing.Point(544, 144);
			this.cmbCancelledBy.Name = "cmbCancelledBy";
			this.cmbCancelledBy.Size = new System.Drawing.Size(432, 24);
			this.cmbCancelledBy.TabIndex = 28;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(0)), ((System.Byte)(0)));
			this.label5.Location = new System.Drawing.Point(16, 168);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(656, 16);
			this.label5.TabIndex = 29;
			this.label5.Text = "To Cancel the Backorder select one or more  Products (with CTRL) and then click o" +
				"n the Cancelation button.";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cmdCancelation
			// 
			this.cmdCancelation.Location = new System.Drawing.Point(672, 424);
			this.cmdCancelation.Name = "cmdCancelation";
			this.cmdCancelation.Size = new System.Drawing.Size(96, 32);
			this.cmdCancelation.TabIndex = 30;
			this.cmdCancelation.Text = "Cancelation";
			this.cmdCancelation.Click += new System.EventHandler(this.cmdCancelation_Click);
			// 
			// fclsGENBackOrders
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(992, 462);
			this.Controls.Add(this.cmdCancelation);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cmbCancelledBy);
			this.Controls.Add(this.label16);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.lstViewOrders);
			this.Controls.Add(this.gpbSearchBy);
			this.Controls.Add(this.cmdClose);
			this.Controls.Add(this.gpbPastOrders);
			this.MaximizeBox = false;
			this.Name = "fclsGENBackOrders";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quick Stock - 30 days Backorder Reminder";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.frmGENBackOrders_Closing);
			this.Load += new System.EventHandler(this.frmGENBackOrders_Load);
			this.gpbPastOrders.ResumeLayout(false);
			this.gpbSearchBy.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
//====================================================================================================
		private void frmGENBackOrders_Load(object sender, System.EventArgs e)
		{
            int intUserID;
			this.openOrders();
			nOrd = 0;							
			m_drBackOrders = m_dtBackOrders.Rows[nrOrder-1];

			String EmplId = m_drBackOrders["EmployeeId"].ToString();
			changeEmpl(EmplId);

			String SuplId = m_drBackOrders["FournisseurId"].ToString();
			changeSupplier(SuplId);
	
			// Open the table Employees
			m_odaEmploye.Fill(m_dtEmploye);
			int nrEmploye = m_dtEmploye.Rows.Count;
			DataRow	m_drEmploye;		
			string Employe;
			System.Object[]	ItemObject = new System.Object[nrEmploye];
			for (int i = 0; i <nrEmploye; i++)
			{
				m_drEmploye = m_dtEmploye.Rows[i];
				Employe = m_drEmploye["Title"].ToString() + " " + m_drEmploye["FirstName"].ToString() + " " + m_drEmploye["LastName"].ToString();
				ItemObject[i] = Employe;
			}
			this.cmbCancelledBy.Items.AddRange(ItemObject);

            intUserID = clsConfiguration.Internal_CurrentUserID;
            this.cmbCancelledBy.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intUserID, m_dtEmploye, 0);

			nOrd = 1;
			lbxOrderNumber_SelectedIndexChanged(null, null);

			// Create the ToolTip and associate with the Form container.
			ToolTip toolTip1 = new ToolTip();

			// Set up the delays for the ToolTip.
			toolTip1.AutoPopDelay = 5000;
			toolTip1.InitialDelay = 1000;
			toolTip1.ReshowDelay = 500;
			// Force the ToolTip text to be displayed whether or not the form is active.
			toolTip1.ShowAlways = true;
      
			// Set up the ToolTip text for the Button and Checkbox.
//			toolTip1.SetToolTip(this.lstViewOrders, "Click on a line of a Product\nin order to Cancel the Backorder.");
		}

		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","BackOrders.htm"); 
		}
//====================================================================================================
		private void openOrders()
		{
//																View Orders non received after 30 days
			DateTime dtBo30;
			System.TimeSpan timeToCancelBO = new System.TimeSpan(30, 0, 0, 0);
			dtBo30 = DateTime.Now.Subtract(timeToCancelBO);
			string dtCompare = dtBo30.ToString(Utilities.clsUtilities.FORMAT_DATE_QUERY);
			
            //																	 Open the table Commandes
			m_odaBackOrders = new OleDbDataAdapter("Select distinct OrderId, OrderDate, CheckDate, FournisseurId, EmployeeId From [Orders] " +
				"WHERE (((Orders.BackOrderUnits)>0) AND ((Orders.OrderDate)<=#"+dtCompare+"#))",  m_odcConnection);
			m_dtBackOrders = new DataTable("Orders");
			m_odaBackOrders.Fill(m_dtBackOrders);

			nrOrder = m_dtBackOrders.Rows.Count;
			this.lbxOrderNumber.Items.Clear();
			System.Object[]	ItemObject = new System.Object[nrOrder];
			if(nrOrder <= 0)
				return;
			for (int i = 0; i <nrOrder; i++)
			{
				m_drBackOrders = m_dtBackOrders.Rows[i];
				ItemObject[i] = m_drBackOrders["OrderId"];
			}
			this.lbxOrderNumber.Items.AddRange(ItemObject);

			int m_intOrderIndex = 0;
			if(string.Compare(m_strOrderSelect, "0",true) > 0)
			{
				// from Remind Me
				m_intOrderIndex = findIndex(m_strOrderSelect);
				this.lbxOrderNumber.SelectedIndex = m_intOrderIndex;
			}
			else
				this.lbxOrderNumber.SelectedIndex = nrOrder-1;
		}

		private int findIndex(string m_strOrderSelect)
		{
			string m_strOrderNumber = "0";
			int m_intOrderIndex = 0;
			int m_intOrderNr = m_dtOrder.Rows.Count;
			for(int i=0; i< m_intOrderNr; i++)
			{
				m_strOrderNumber = m_dtOrder.Rows[i]["OrderId"].ToString();
				if(string.Compare(m_strOrderNumber, m_strOrderSelect) == 0)
					return i;
			}
			return m_intOrderIndex;
		}

		private void lbxOrderNumber_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			int index = 0;
			for(int i=0; i<15; i++)
				intModBO[i] = 0;
			if(nOrd == 0) return;
			this.lstViewOrders.Items.Clear();
			index = lbxOrderNumber.SelectedIndex;
			if(index < 0)
				return;
			m_drBackOrders = m_dtBackOrders.Rows[index];
			orderId =m_drBackOrders["OrderId"].ToString();
			this.lblOrderedDate.Text = ((DateTime) m_drBackOrders["OrderDate"]).ToString("MMM dd, yyyy");
			this.lblReceivedDate.Text = ((DateTime) m_drBackOrders["CheckDate"]).ToString("MMM dd, yyyy");
			fclsGENInput.orderId = orderId;
			String EmplId = m_drBackOrders["EmployeeId"].ToString();
			changeEmpl(EmplId);
			String SuplId = m_drBackOrders["FournisseurId"].ToString();
			changeSupplier(SuplId);
			m_strSuplId = SuplId;
//																	Populate ListBox first time
			this.PopulateListBox(0);
		}

		private void PopulateListBox(int nrTime)
		{
//												                     View Back Order
			dataSet = new DataSet("Orders");
			m_dtOrder = new DataTable("Orders");
			m_odaOrder = new OleDbDataAdapter("SELECT Orders.*, [Products.MatName], [SubProducts.MatName], [Trademarks.Trademark] " +
				"FROM Products INNER JOIN ((Trademarks INNER JOIN Orders ON Trademarks.MarComId = Orders.MarComId) " +
				"INNER JOIN SubProducts ON (SubProducts.SubPrId = Orders.SubPrId) AND (Trademarks.MarComId = SubProducts.MarComId)) " +
				"ON (Orders.MatId = Products.MatId) AND (Products.MatId = SubProducts.MatId)" +
				" WHERE (((Orders.OrderId)='" + orderId + "'))", m_odcConnection);
			m_odaOrder.Fill(m_dtOrder);

			lstViewOrders.Items.Clear();
			int nrProd = m_dtOrder.Rows.Count;
			nrTotProd = nrProd;
			double dMan = 0.0;
			System.Object[]	ItemObject = new System.Object[nrProd];
			float manUnits = 0f;
//			nrTotBO = 0;
			Color foreColor = new Color();
			ListViewItem lviItem;
			for (int i = 0; i <nrProd; i++)
			{
				m_drOrder = m_dtOrder.Rows[i];
				foreColor = Color.LightGray;
				manUnits = float.Parse(m_drOrder["BackOrderUnits"].ToString());
				if(manUnits > 0)
				{
					if(nrTime == 0)
						++nrTotBO;
					foreColor = Color.Black;
					lviItem = lstViewOrders.Items.Add(((DateTime) (m_drOrder["BackOrderUpdateDate"])).ToString("MMM dd, yyyy"));
				}
				else
					lviItem = lstViewOrders.Items.Add("");
				lviItem.ForeColor = foreColor;
				lviItem.SubItems.Add(m_drOrder["Products.MatName"].ToString());
				lviItem.SubItems.Add(m_drOrder["SubProducts.MatName"].ToString());
				lviItem.SubItems.Add(m_drOrder["Trademarks.Trademark"].ToString());
				dMan = double.Parse(m_drOrder["OrderQty"].ToString());
				lviItem.SubItems.Add(dMan.ToString("#,##0.00"));
				lviItem.SubItems.Add(manUnits.ToString());
				lviItem.SubItems.Add(m_drOrder["Pack"].ToString());
			}
		}																																														

		private void changeSupplier(String SuplId)
		{
			m_odaSupplier.Fill(m_dtSupplier);
			int nrSupplier = m_dtSupplier.Rows.Count;
			String strMan;

			for (int i = 0; i <nrSupplier; i++)
			{
				strMan = m_dtSupplier.Rows[i]["FournisseurId"].ToString();
				if(strMan == SuplId)
				{
					m_drSupplier = m_dtSupplier.Rows[i];

                    this.lblContact.Text = m_siSupplier.ContactName = clsUtilities.FormatName_Display(m_drSupplier["ConTitle"].ToString(),
                                                                                                      m_drSupplier["ContactFirstName"].ToString(),
                                                                                                      m_drSupplier["ContactLastName"].ToString());
                    m_siSupplier.DatabaseID = (int) m_drSupplier[""];
                    this.lblEmail.Text = m_siSupplier.Email = m_drSupplier["Email"].ToString();
                    this.lblSupplier.Text = m_siSupplier.Name = m_drSupplier["CompanyName"].ToString();
                    this.lblPhone.Text = m_siSupplier.PhoneNumber = m_drSupplier["PhoneNumber"].ToString();
                    this.lblFax.Text = m_siSupplier.FaxNumber = m_drSupplier["FaxNumber"].ToString();

					break;
				}
			}
		}

		private void changeEmpl(String EmplId)
		{
			m_odaEmploye.Fill(m_dtEmploye);
			int nrEmploye = m_dtEmploye.Rows.Count;		
			String strEmpl;

			for (int i = 0; i <nrEmploye; i++)
			{
				strEmpl = m_dtEmploye.Rows[i]["EmployeeId"].ToString();
				if(strEmpl == EmplId)
				{
					m_drEmploye = m_dtEmploye.Rows[i];
					this.lblOrderdBy.Text = m_drEmploye["Title"] + " " + m_drEmploye["FirstName"] + " " + m_drEmploye["LastName"]; 
					break;
				}
			}
		}
			
//		private void lstViewOrders_Click(object sender, System.EventArgs e)
		private void cancelBackorder()
		{
//			string m_strCBOUnits = "0";
			m_odaSaveOrder = new OleDbDataAdapter("Select * From tempBackOrder", m_odcConnection);
			ocbSaveOrder = new OleDbCommandBuilder(m_odaSaveOrder);
			m_dtSaveOrder = new DataTable();
			m_odaSaveOrder.Fill(m_dtSaveOrder);
			ListView.SelectedIndexCollection index = lstViewOrders.SelectedIndices;
			foreach(int m_int_clickedProd in index)
			{
				m_drOrder =m_dtOrder.Rows[m_int_clickedProd];
				if(intModBO[m_int_clickedProd] == 1)
					return;
//				intModBO[m_int_clickedProd] = 1;
				float fltManUnits = float.Parse(m_drOrder["BackOrderUnits"].ToString());
				if(fltManUnits == 0)
					return;
				subPrName = m_drOrder["SubProducts.MatName"].ToString();
				productName = m_drOrder["Products.MatName"].ToString();
				prodId = m_drOrder["MatId"].ToString();
				subPrId = m_drOrder["SubPrId"].ToString();
//									save selected line in temporary TABLE  (tempBackOrder)
				{
				m_drSaveOrder = m_dtSaveOrder.NewRow();
				m_drSaveOrder["OrderId"]			= orderId;
				m_drSaveOrder["OrderDate"]			= m_drOrder["OrderDate"];	
				m_drSaveOrder["MatId"]				= prodId;
				m_drSaveOrder["SubPrId"]			= subPrId;
				m_drSaveOrder["MarComId"]			= m_drOrder["MarComId"].ToString();
				m_drSaveOrder["FournisseurId"]		= m_drOrder["FournisseurId"].ToString();
				m_drSaveOrder["EmployeeId"]			= m_drOrder["EmployeeId"].ToString();
				//					if(orderLines[intOrderLine].Units.ToString() == "1/4")
				//						nrUnits = 0.25f;
				//					else if(orderLines[intOrderLine].Units.ToString() == "1/2")
				//							nrUnits = 0.5f;
				//						else
				//							nrUnits = Single.Parse(orderLines[intOrderLine].Units.ToString());
				m_drSaveOrder["OrderQty"]			= m_drOrder["OrderQty"].ToString();
				m_drSaveOrder["Pack"]				= m_drOrder["Pack"].ToString();
				m_drSaveOrder["CategoryId"]			= m_drOrder["CategoryId"].ToString();
				m_drSaveOrder["Prix"]				= m_drOrder["Prix"].ToString();
				m_drSaveOrder["Checked"]			= m_drOrder["Checked"].ToString();
				m_drSaveOrder["ReceivedQty"]		= m_drOrder["ReceivedQty"].ToString();
				m_drSaveOrder["BackOrderUnits"]		= m_drOrder["BackOrderUnits"].ToString();
				m_drSaveOrder["CanceledBOUnits"]	= m_drOrder["CanceledBOUnits"].ToString();
				m_drSaveOrder["ReturnUnits"]		= m_drOrder["ReturnUnits"].ToString();
				m_drSaveOrder["CatalogPay"]			= m_drOrder["CatalogPay"].ToString();
				m_drSaveOrder["Tax"]				= m_drOrder["Tax"].ToString();
				m_drSaveOrder["Transport"]			= m_drOrder["Transport"].ToString();
				m_drSaveOrder["Duty"]				= m_drOrder["Duty"].ToString();
				m_drSaveOrder["TotalPay"]			= m_drOrder["TotalPay"].ToString();
					
				//Add the new row to the table
				m_dtSaveOrder.Rows.Add(m_drSaveOrder);	
				try
				{
					m_odaSaveOrder.Update(m_dtSaveOrder);

				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				}
			}

				fclsOIViewOrdRpt frmOIViewOrdRpt = new fclsOIViewOrdRpt(this, fclsOIViewOrdRpt.ViewOrderReportCaller.CanceledBackorder, m_odcConnection);
				frmOIViewOrdRpt.SetOrderInformation(orderId,
                                                    m_siSupplier);
				frmOIViewOrdRpt.ShowDialog();


//									delete temporary TABLE (tempBackOrder)
				m_odaSaveOrder = new OleDbDataAdapter("Select * From tempBackOrder", m_odcConnection);
				ocbSaveOrder = new OleDbCommandBuilder(m_odaSaveOrder);
				m_dtSaveOrder = new DataTable();
				m_odaSaveOrder.Fill(m_dtSaveOrder);
				for(int i=m_dtSaveOrder.Rows.Count-1; i >= 0; i--)
				{
					m_dtSaveOrder.Rows[i].Delete();
					try
					{
						m_odaSaveOrder.Update(m_dtSaveOrder);
						m_dtSaveOrder.AcceptChanges();
					} 
					catch(Exception ex)
					{
						MessageBox.Show(ex.Message);
					}
				}
//													cancel the backorder and update the order
			foreach(int m_int_clickedProd in index)
			{
				m_drOrder =m_dtOrder.Rows[m_int_clickedProd];
				intModBO[m_int_clickedProd] = 1;
				// comment only for test
/*				subPrId = m_drOrder["SubPrId"].ToString();
				
				m_dtCancelOrder = new DataTable("Orders");
				m_odaCancelOrder = new OleDbDataAdapter("Select * FROM Orders WHERE ([Orders.OrderId]='" 
					+ orderId + "' AND [Orders.SubPrId]=" + subPrId + ")", m_odcConnection);
				m_odaCancelOrder.Fill(m_dtCancelOrder);
				OleDbCommandBuilder	ocbOrderCancel = new OleDbCommandBuilder(m_odaCancelOrder);
				try
				{
					DataRow targetRow = m_dtCancelOrder.Rows[0];
					{
						DateTime dt = DateTime.Now;
						targetRow.BeginEdit();
//						targetRow["BackOrderUpdateDate"]	= dt.ToString("MM/dd/yyyy");
						targetRow["CanceledBODate"]			= dt.ToString("MM/dd/yyyy");
						string m_strEmployee = this.cmbCancelledBy.Text.ToString();
						targetRow["CanceledBOEmployeeId"]	= intFindEmplId(m_strEmployee);
						targetRow["CanceledBOUnits"]		= targetRow["BackOrderUnits"].ToString();
						targetRow["BackOrderUnits"]			= 0.0;
						targetRow.EndEdit();
						m_odaCancelOrder.Update(m_dtCancelOrder);
						m_dtCancelOrder.AcceptChanges();
					}
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}

				this.PopulateListBox(1);*/
			}
		}

		public int intFindEmplId(string strName)
		{
			int nrRecs = m_dtEmploye.Rows.Count;
			string strComp = strName.Trim();
			for (int i=0; i<nrRecs; i++)
			{
				string strEmpl = m_dtEmploye.Rows[i]["Title"].ToString()+ " " + m_dtEmploye.Rows[i]["FirstName"].ToString() +
					" " + m_dtEmploye.Rows[i]["LastName"].ToString();
				if (strComp == strEmpl)
					return int.Parse(m_dtEmploye.Rows[i]["EmployeeId"].ToString());
			}

			return 0;
		}

		private void cmdCancelation_Click(object sender, System.EventArgs e)
		{
			m_blnCancelationSent = false;
			DialogResult dlgResult;
			dlgResult = MessageBox.Show("Would you like to cancel the backorder for this or these products\nand send an Email to the Supplier?", "Backorder cancelation!",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
			if(dlgResult == DialogResult.No || dlgResult == DialogResult.Cancel)
				return;
			cancelBackorder();		
		}

		public void SetCancelationSentStatus(bool blnCancelationSent)
		{
			m_blnCancelationSent = blnCancelationSent;
		}

		private void frmGENBackOrders_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
		
		}
	}
}
