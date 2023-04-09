using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Web.Mail;
using System.Data.OleDb;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for frmOrder.
	/// </summary>
	public class fclsOMEmergencyOrder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox	lbxCategories;
		private System.Windows.Forms.ListBox	lbxProducts;
		private System.Windows.Forms.ListBox	lbxSubProducts;
		private System.Windows.Forms.ComboBox	cmbSuppliers;
		private System.Windows.Forms.ComboBox	cmbEmployees;
		private System.Windows.Forms.Label		lblSelectCategory;
		private System.Windows.Forms.Label		lblSelectSubProduct;
		private System.Windows.Forms.Label		lblSelectProduct;
		private System.Windows.Forms.Label		lblOrderNumber;
		private System.Windows.Forms.Label		lblSupplier;
		private System.Windows.Forms.Label		lblOrderMadeBy;
		private System.Windows.Forms.ContextMenu ctmRightClick;
		private System.Windows.Forms.MenuItem mnuAdd;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuRemove;
		private System.Windows.Forms.Button btnAddToOrder;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnSendOrder;
		private DSMS.OrderLineContainer olcNewOrder;
		private System.Windows.Forms.TextBox txtOrderId;
		private System.Windows.Forms.Button btnResetOrder;
		private System.ComponentModel.Container components = null;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private enum NameCheck:int {Category,Product};
		public delegate void		EmptyOrderLineContainerHandler();

		private bool				m_blnOrderSent = false;	//, m_blnSendOrderClick = false;
		private bool				m_blnCategoriesLoaded = false, m_blnProductsLoaded = false, m_blnSubProductsLoaded = false;
		private DataTable			m_dtaEmployees, m_dtaSuppliers, m_dtaTrademarks;
		private DataTable			m_dtaCategories, m_dtaProducts, m_dtaSubProducts;
		private double				m_dblCategoryListboxProportion, m_dblProductListboxProportion, m_dblSubProductListboxProportion;
		private fclsPrevOrders		m_frmPrevOrders;
		private int					m_intSelectedCategoryKey = -1, m_intSelectedProductKey = -1, m_intSelectedSubProductKey = -1;
		private int					m_intNrTrademarks = -1;
		private int					m_intSupplierId = -1 , m_intEmployeeId = -1;
		private string				m_strNewOrderNumber;
		private int					m_intCurrentListBox = 0;// 0 = default;1 = lbxCategories;2 = lbxProducts;3 = lbxSubProducts
		private int					m_intMaxCategoryKey = -1, m_intMaxProductKey = -1, m_intMaxSubProductKey = -1;
		private int					m_intSelectedSubProductIndex = -1;
		private OleDbConnection		m_odcConnection;
		private OleDbDataAdapter	m_odaSuppliers;
		private OleDbDataAdapter    m_odaCategories, m_odaProducts, m_odaSubProducts;
		
		public fclsOMEmergencyOrder(OleDbConnection odcConnection)
		{
			InitializeComponent();

			m_odcConnection = odcConnection;

			m_frmPrevOrders = new fclsPrevOrders(this);
			
			// initialize resizing variables
			m_dblCategoryListboxProportion = ((double) this.lbxCategories.Width)/((double) this.Width);
			m_dblProductListboxProportion = ((double) this.lbxProducts.Width)/((double) this.Width);
			m_dblSubProductListboxProportion = ((double) this.lbxSubProducts.Width)/((double) this.Width);
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
            this.lblSelectCategory = new System.Windows.Forms.Label();
            this.lblSelectSubProduct = new System.Windows.Forms.Label();
            this.lblSelectProduct = new System.Windows.Forms.Label();
            this.lbxCategories = new System.Windows.Forms.ListBox();
            this.ctmRightClick = new System.Windows.Forms.ContextMenu();
            this.mnuAdd = new System.Windows.Forms.MenuItem();
            this.mnuEdit = new System.Windows.Forms.MenuItem();
            this.mnuRemove = new System.Windows.Forms.MenuItem();
            this.lbxProducts = new System.Windows.Forms.ListBox();
            this.lbxSubProducts = new System.Windows.Forms.ListBox();
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.lblSupplier = new System.Windows.Forms.Label();
            this.lblOrderMadeBy = new System.Windows.Forms.Label();
            this.cmbSuppliers = new System.Windows.Forms.ComboBox();
            this.cmbEmployees = new System.Windows.Forms.ComboBox();
            this.btnAddToOrder = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnSendOrder = new System.Windows.Forms.Button();
            this.btnResetOrder = new System.Windows.Forms.Button();
            this.olcNewOrder = new DSMS.OrderLineContainer();
            this.txtOrderId = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // lblSelectCategory
            // 
            this.lblSelectCategory.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSelectCategory.Location = new System.Drawing.Point(26, 16);
            this.lblSelectCategory.Name = "lblSelectCategory";
            this.lblSelectCategory.Size = new System.Drawing.Size(208, 24);
            this.lblSelectCategory.TabIndex = 0;
            this.lblSelectCategory.Text = "1.  Select a category";
            this.lblSelectCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSelectSubProduct
            // 
            this.lblSelectSubProduct.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectSubProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSelectSubProduct.Location = new System.Drawing.Point(577, 16);
            this.lblSelectSubProduct.Name = "lblSelectSubProduct";
            this.lblSelectSubProduct.Size = new System.Drawing.Size(232, 24);
            this.lblSelectSubProduct.TabIndex = 2;
            this.lblSelectSubProduct.Text = "3.  Select a sub-product";
            this.lblSelectSubProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSelectProduct
            // 
            this.lblSelectProduct.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectProduct.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSelectProduct.Location = new System.Drawing.Point(302, 16);
            this.lblSelectProduct.Name = "lblSelectProduct";
            this.lblSelectProduct.Size = new System.Drawing.Size(208, 24);
            this.lblSelectProduct.TabIndex = 3;
            this.lblSelectProduct.Text = "2.  Select a product";
            this.lblSelectProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lbxCategories
            // 
            this.lbxCategories.ContextMenu = this.ctmRightClick;
            this.lbxCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxCategories.ForeColor = System.Drawing.Color.Red;
            this.lbxCategories.Location = new System.Drawing.Point(26, 40);
            this.lbxCategories.Name = "lbxCategories";
            this.lbxCategories.Size = new System.Drawing.Size(250, 238);
            this.lbxCategories.TabIndex = 4;
            this.lbxCategories.SelectedIndexChanged += new System.EventHandler(this.lbxCategories_SelectedIndexChanged);
            this.lbxCategories.MouseEnter += new System.EventHandler(this.lbxCategories_MouseEnter);
            // 
            // ctmRightClick
            // 
            this.ctmRightClick.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuAdd,
            this.mnuEdit,
            this.mnuRemove});
            this.ctmRightClick.Popup += new System.EventHandler(this.ctmRightClick_Popup);
            // 
            // mnuAdd
            // 
            this.mnuAdd.Index = 0;
            this.mnuAdd.Text = "&Add";
            this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
            // 
            // mnuEdit
            // 
            this.mnuEdit.Index = 1;
            this.mnuEdit.Text = "&Edit";
            this.mnuEdit.Click += new System.EventHandler(this.mnuEdit_Click);
            // 
            // mnuRemove
            // 
            this.mnuRemove.Index = 2;
            this.mnuRemove.Text = "&Remove";
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // lbxProducts
            // 
            this.lbxProducts.ContextMenu = this.ctmRightClick;
            this.lbxProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxProducts.ForeColor = System.Drawing.Color.Green;
            this.lbxProducts.Location = new System.Drawing.Point(302, 40);
            this.lbxProducts.Name = "lbxProducts";
            this.lbxProducts.Size = new System.Drawing.Size(250, 238);
            this.lbxProducts.TabIndex = 5;
            this.lbxProducts.SelectedIndexChanged += new System.EventHandler(this.lbxProducts_SelectedIndexChanged);
            this.lbxProducts.MouseEnter += new System.EventHandler(this.lbxProducts_MouseEnter);
            // 
            // lbxSubProducts
            // 
            this.lbxSubProducts.ContextMenu = this.ctmRightClick;
            this.lbxSubProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxSubProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.lbxSubProducts.Location = new System.Drawing.Point(577, 40);
            this.lbxSubProducts.Name = "lbxSubProducts";
            this.lbxSubProducts.Size = new System.Drawing.Size(330, 238);
            this.lbxSubProducts.TabIndex = 6;
            this.lbxSubProducts.SelectedIndexChanged += new System.EventHandler(this.lbxSubProducts_SelectedIndexChanged);
            this.lbxSubProducts.MouseEnter += new System.EventHandler(this.lbxSubProducts_MouseEnter);
            this.lbxSubProducts.DoubleClick += new System.EventHandler(this.lbxSubProducts_DoubleClick);
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.AutoSize = true;
            this.lblOrderNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblOrderNumber.Location = new System.Drawing.Point(26, 291);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(67, 16);
            this.lblOrderNumber.TabIndex = 7;
            this.lblOrderNumber.Text = "Order Nr.";
            this.lblOrderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupplier
            // 
            this.lblSupplier.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSupplier.Location = new System.Drawing.Point(168, 292);
            this.lblSupplier.Name = "lblSupplier";
            this.lblSupplier.Size = new System.Drawing.Size(80, 16);
            this.lblSupplier.TabIndex = 8;
            this.lblSupplier.Text = "Supplier";
            this.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderMadeBy
            // 
            this.lblOrderMadeBy.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderMadeBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblOrderMadeBy.Location = new System.Drawing.Point(472, 292);
            this.lblOrderMadeBy.Name = "lblOrderMadeBy";
            this.lblOrderMadeBy.Size = new System.Drawing.Size(104, 16);
            this.lblOrderMadeBy.TabIndex = 9;
            this.lblOrderMadeBy.Text = "Order made by";
            this.lblOrderMadeBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbSuppliers
            // 
            this.cmbSuppliers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSuppliers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSuppliers.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cmbSuppliers.Location = new System.Drawing.Point(232, 288);
            this.cmbSuppliers.Name = "cmbSuppliers";
            this.cmbSuppliers.Size = new System.Drawing.Size(216, 24);
            this.cmbSuppliers.TabIndex = 11;
            // 
            // cmbEmployees
            // 
            this.cmbEmployees.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployees.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmployees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cmbEmployees.Location = new System.Drawing.Point(576, 288);
            this.cmbEmployees.Name = "cmbEmployees";
            this.cmbEmployees.Size = new System.Drawing.Size(184, 24);
            this.cmbEmployees.TabIndex = 12;
            // 
            // btnAddToOrder
            // 
            this.btnAddToOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddToOrder.Enabled = false;
            this.btnAddToOrder.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddToOrder.ForeColor = System.Drawing.Color.Green;
            this.btnAddToOrder.Location = new System.Drawing.Point(797, 288);
            this.btnAddToOrder.Name = "btnAddToOrder";
            this.btnAddToOrder.Size = new System.Drawing.Size(112, 24);
            this.btnAddToOrder.TabIndex = 17;
            this.btnAddToOrder.Text = "Add to Order";
            this.btnAddToOrder.Click += new System.EventHandler(this.btnAddToOrder_Click);
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Location = new System.Drawing.Point(720, 633);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(96, 32);
            this.btnClose.TabIndex = 30;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(822, 633);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(96, 32);
            this.btnHelp.TabIndex = 29;
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnSendOrder
            // 
            this.btnSendOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSendOrder.Enabled = false;
            this.btnSendOrder.Location = new System.Drawing.Point(482, 633);
            this.btnSendOrder.Name = "btnSendOrder";
            this.btnSendOrder.Size = new System.Drawing.Size(96, 32);
            this.btnSendOrder.TabIndex = 28;
            this.btnSendOrder.Text = "Send Order";
            this.btnSendOrder.Click += new System.EventHandler(this.btnSendOrder_Click);
            // 
            // btnResetOrder
            // 
            this.btnResetOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetOrder.Enabled = false;
            this.btnResetOrder.Location = new System.Drawing.Point(584, 633);
            this.btnResetOrder.Name = "btnResetOrder";
            this.btnResetOrder.Size = new System.Drawing.Size(96, 32);
            this.btnResetOrder.TabIndex = 27;
            this.btnResetOrder.Text = "Reset Order";
            this.btnResetOrder.Click += new System.EventHandler(this.btnResetOrder_Click);
            // 
            // olcNewOrder
            // 
            this.olcNewOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.olcNewOrder.Location = new System.Drawing.Point(5, 328);
            this.olcNewOrder.MaxOrderLines = 99;
            this.olcNewOrder.Name = "olcNewOrder";
            this.olcNewOrder.Size = new System.Drawing.Size(924, 304);
            this.olcNewOrder.TabIndex = 31;
            this.olcNewOrder.OnEmptyOrderLineContainer += new DSMS.fclsOMEmergencyOrder.EmptyOrderLineContainerHandler(this.EmptyOrderLineContainer);
            // 
            // txtOrderId
            // 
            this.txtOrderId.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtOrderId.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtOrderId.Location = new System.Drawing.Point(96, 289);
            this.txtOrderId.Name = "txtOrderId";
            this.txtOrderId.Size = new System.Drawing.Size(56, 23);
            this.txtOrderId.TabIndex = 10;
            // 
            // fclsOMEmergencyOrder
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(930, 670);
            this.Controls.Add(this.olcNewOrder);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnSendOrder);
            this.Controls.Add(this.btnResetOrder);
            this.Controls.Add(this.btnAddToOrder);
            this.Controls.Add(this.txtOrderId);
            this.Controls.Add(this.lblOrderNumber);
            this.Controls.Add(this.cmbEmployees);
            this.Controls.Add(this.cmbSuppliers);
            this.Controls.Add(this.lblOrderMadeBy);
            this.Controls.Add(this.lblSupplier);
            this.Controls.Add(this.lbxSubProducts);
            this.Controls.Add(this.lbxProducts);
            this.Controls.Add(this.lbxCategories);
            this.Controls.Add(this.lblSelectProduct);
            this.Controls.Add(this.lblSelectSubProduct);
            this.Controls.Add(this.lblSelectCategory);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Location = new System.Drawing.Point(50, 0);
            this.Name = "fclsOMEmergencyOrder";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Stock - Express Order";
            this.Load += new System.EventHandler(this.frmEmergencyOrder_Load);
            this.Closing += new System.ComponentModel.CancelEventHandler(this.fclsSMEmergencyOrder_Closing);
            this.Resize += new System.EventHandler(this.fclsOMEmergencyOrder_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		private void frmEmergencyOrder_Load(object sender, System.EventArgs e)
		{
			// Variable declaration
            int intTemp;
			OleDbDataAdapter odaEmployees;
			DataRow dtrEmployee;
			string strEmployee;

			// Initialize variables
			m_dtaSuppliers = new DataTable("Suppliers");
			m_dtaTrademarks = new DataTable("Trademarks");
						
			// Hide order labels
			this.ChangeLabelVisibility(false);

			this.txtOrderId.Text = m_strNewOrderNumber = GetNewOrderNumber();
			
			try
			{
				// Open the table Trademarks
				this.LoadTrademarks();
				
				//
				// Suppliers
				//
				// Open the table Suppliers
				m_odaSuppliers = new OleDbDataAdapter("SELECT * FROM [Suppliers] ORDER BY CompanyName", m_odcConnection);
				m_odaSuppliers.Fill(m_dtaSuppliers);
				
				// Add suppliers to combo-box
				for (int i = 0; i < m_dtaSuppliers.Rows.Count; i++)
					this.cmbSuppliers.Items.Add(m_dtaSuppliers.Rows[i]["CompanyName"].ToString());
				
				// Set default supplier according to configuration file
                intTemp = clsConfiguration.General_DefaultSupplierID;
				this.cmbSuppliers.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intTemp, m_dtaSuppliers, 0);
				
				//
				// Employees
				//
				// Open the table 'Employees'
				odaEmployees = new OleDbDataAdapter("SELECT * FROM [Employees] WHERE Status = 1 ORDER BY FirstName, LastName", m_odcConnection);
				m_dtaEmployees = new DataTable("Employees");				
				odaEmployees.Fill(m_dtaEmployees);
			
				// Add employees to combo-box
				for (int i = 0; i < m_dtaEmployees.Rows.Count; i++)
				{
					dtrEmployee = m_dtaEmployees.Rows[i];
					if(int.Parse(dtrEmployee["Status"].ToString()) == 1)
					{
						strEmployee = clsUtilities.FormatName_Display(dtrEmployee["Title"].ToString(), dtrEmployee["FirstName"].ToString(), dtrEmployee["LastName"].ToString());
						this.cmbEmployees.Items.Add(strEmployee);
					}
				}
				
				// Set default supplier according to configuration file
                intTemp = clsConfiguration.Internal_CurrentUserID;
                this.cmbEmployees.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intTemp, m_dtaEmployees, 0);

				// Load product categories
				this.LoadData("Categories",-1);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text);
			}
		}

		private void EmptyOrderLineContainer()
		{
			this.SetOrderControlButtonsEnable(false);
		}

		private void SetOrderControlButtonsEnable(bool blnEnabled)
		{
			this.btnResetOrder.Enabled = blnEnabled;
			this.btnSendOrder.Enabled = blnEnabled;
		}

		private string GetNewOrderNumber()
		{
			// Variable declaration
			DataTable dtaOrders;
			int intLastOrderNumber;
			OleDbDataAdapter odaOrders;
			string strCurrentYearLast2Digits, strLastOrderNumber, strNewOrderNr;
            string[] strMan;
			
			// Variable initialization
			intLastOrderNumber = 0;
			dtaOrders = new DataTable("Orders");
			strNewOrderNr = "";
			strCurrentYearLast2Digits = DateTime.Now.ToString("yyyy").Substring(2,2);
		    
			// Open the table Orders
			odaOrders = new OleDbDataAdapter("SELECT Orders.OrderId FROM Orders WHERE Orders.OrderId LIKE \'%" + strCurrentYearLast2Digits + "\' ORDER BY Orders.OrderId", m_odcConnection);
			odaOrders.Fill(dtaOrders);
			
			// Get last used order number for the current year
			if(dtaOrders.Rows.Count > 0)								
			{
				strLastOrderNumber = dtaOrders.Rows[dtaOrders.Rows.Count - 1]["OrderId"].ToString();
				strMan = strLastOrderNumber.Split('-');
				if(int.Parse(strMan[0]) > intLastOrderNumber)
					intLastOrderNumber = int.Parse(strMan[0]);
			}
			
			// Generate new order number
			intLastOrderNumber++;
			strNewOrderNr = intLastOrderNumber.ToString("000") + "-" + strCurrentYearLast2Digits;
			
			return strNewOrderNr;
		}

		private void btnCloseForm_Click(object sender, System.EventArgs e)
		{
			this.m_frmPrevOrders.Close();
			this.Close();
		}

		private void lbxCategories_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(lbxCategories.SelectedIndex != -1)
			{
				m_frmPrevOrders.Close();
				m_intSelectedCategoryKey = int.Parse(m_dtaCategories.Rows[lbxCategories.SelectedIndex]["CategoryId"].ToString());
				this.LoadData("Products", m_intSelectedCategoryKey);
			}
		}

		private void lbxProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(lbxProducts.SelectedIndex != -1)
			{
				m_frmPrevOrders.Close();
				m_intSelectedProductKey = int.Parse(m_dtaProducts.Rows[lbxProducts.SelectedIndex]["MatId"].ToString());
				this.LoadData("Sub-Products", m_intSelectedProductKey);
			}	
		}

		private void lbxSubProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			double dMan = 0.0;
			DateTime dDate;
			m_intSelectedSubProductIndex = lbxSubProducts.SelectedIndex;
			if(m_intSelectedSubProductIndex != -1)
			{
                // initialize 'previous orders' form
                m_frmPrevOrders.Close();
                m_frmPrevOrders = new fclsPrevOrders(this);
				
				DataTable dtaPriceComparison;

				OleDbDataAdapter odaPriceComparison;
				string strQuery;

				m_intSelectedSubProductKey = int.Parse(m_dtaSubProducts.Rows[lbxSubProducts.SelectedIndex]["SubPrId"].ToString());
				this.btnAddToOrder.Enabled = true;

				strQuery = "SELECT Orders.OrderDate, Suppliers.CompanyName, Orders.Prix " +
					"FROM Suppliers INNER JOIN Orders ON Orders.FournisseurId = Suppliers.FournisseurId WHERE (Orders.SubPrId = " + m_intSelectedSubProductKey + ") ORDER BY Orders.OrderDate"; 
			
				odaPriceComparison = new OleDbDataAdapter(strQuery,m_odcConnection);
				dtaPriceComparison = new DataTable("Orders");				
				odaPriceComparison.Fill(dtaPriceComparison);

				if(dtaPriceComparison.Rows.Count > 0)
				{
					ListViewItem lviItem;
					for (int i = 0; i < dtaPriceComparison.Rows.Count; i++)
					{
                        // pre-internationalization code
						/*dDate = DateTime.Parse(dtaPriceComparison.Rows[i]["OrderDate"].ToString());
                        lviItem = m_frmPrevOrders.lstViewOrder.Items.Add(dDate.ToString("MMM dd, yyyy"));
                        lviItem.SubItems.Add(dtaPriceComparison.Rows[i]["CompanyName"].ToString());
                        dMan = double.Parse(dtaPriceComparison.Rows[i]["Prix"].ToString());
                        lviItem.SubItems.Add(dMan.ToString("#,##0.00"));*/

                        // post-internationalization code
                        dDate = (DateTime) dtaPriceComparison.Rows[i]["OrderDate"];
                        lviItem = m_frmPrevOrders.lstViewOrder.Items.Add(dDate.ToString(clsUtilities.FORMAT_DATE_ORDERED));	
						lviItem.SubItems.Add(dtaPriceComparison.Rows[i]["CompanyName"].ToString());
						dMan = double.Parse(dtaPriceComparison.Rows[i]["Prix"].ToString());
                        lviItem.SubItems.Add(dMan.ToString(clsUtilities.FORMAT_CURRENCY));
					}
					m_frmPrevOrders.Text = "Prices from Previous Orders";
					m_frmPrevOrders.Visible = true;
				}
				else
				{
					strQuery = "SELECT SubProducts.Prix, Suppliers.CompanyName " +
						"FROM Suppliers INNER JOIN SubProducts ON SubProducts.SuplId = Suppliers.FournisseurId WHERE (SubProducts.SubPrId = " + m_intSelectedSubProductKey + ")"; 
			
					odaPriceComparison = new OleDbDataAdapter(strQuery,m_odcConnection);
					dtaPriceComparison = new DataTable("SubProducts");				
					odaPriceComparison.Fill(dtaPriceComparison);

					ListViewItem lviItem;
                    // pre-internationalization code
					/*lviItem = m_frmPrevOrders.lstViewOrder.Items.Add((DateTime.Now.ToString("MMM dd, yyyy")));
					lviItem.SubItems.Add(dtaPriceComparison.Rows[0]["CompanyName"].ToString());
					dMan = double.Parse(dtaPriceComparison.Rows[0]["Prix"].ToString());
					lviItem.SubItems.Add(dMan.ToString("#,##0.00"));*/

                    // post-internationalization code
                    lviItem = m_frmPrevOrders.lstViewOrder.Items.Add(DateTime.Now.ToString(clsUtilities.FORMAT_DATE_ORDERED));
                    lviItem.SubItems.Add(dtaPriceComparison.Rows[0]["CompanyName"].ToString());
                    dMan = double.Parse(dtaPriceComparison.Rows[0]["Prix"].ToString());
                    lviItem.SubItems.Add(dMan.ToString(clsUtilities.FORMAT_CURRENCY));

					m_frmPrevOrders.Text = "Flyer or Catalog Price";
					m_frmPrevOrders.Visible = true;
				}
			}
		}

		/*private void orderLines_Click(object sender, clsRemoveOrderLineClickEventArgs e)
		{
			int intLineIndex = e.GetLineIndex();
			string strProductName = e.GetProductName();
			
			DialogResult dlgResult = MessageBox.Show("Are you sure you want to remove the product\r\n" + strProductName + "\r\nfrom this order?",
													 this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

			switch(dlgResult)
			{
				case DialogResult.Yes:
					if(intLineIndex != (m_intOrderLineNr - 1))
					{
						// Shift all the order data up one line
						for(int i=intLineIndex; i < m_intOrderLineNr - 1; i++)
						{
							orderLines[i].TradeMark = orderLines[i+1].TradeMark;
							orderLines[i].TradeMarkId = orderLines[i+1].TradeMarkId;
							orderLines[i].CategoryId= orderLines[i+1].CategoryId;
							orderLines[i].ProductId = orderLines[i+1].ProductId;
							orderLines[i].SubProductId = orderLines[i+1].SubProductId;
							orderLines[i].ProdName = orderLines[i+1].ProdName;
							orderLines[i].Packaging = orderLines[i+1].Packaging;
							orderLines[i].Units = orderLines[i+1].Units;
						}					
					}

					// Remove last order line
					m_intOrderLineNr--;
					this.Controls.Remove(orderLines[m_intOrderLineNr]);
					m_intOrderLineYPos -= m_intVerticalSpacing;
					//this.pnlButtons.Location = new System.Drawing.Point(this.pnlButtons.Location.X, this.pnlButtons.Location.Y - m_intVerticalSpacing);
					//this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, this.pnlButtons.Location.Y + 30);

					if(m_intOrderLineNr == 0)
					{
						this.ChangeLabelVisibility(false);
						this.btnAddToOrder.Enabled = false;
						this.btnResetOrder.Enabled = false;
						this.btnSendOrder.Enabled = false;
					}
				break;
			}
		}*/

		private string GetTrademark(int intTradeMarkId)
		{
			for(int i=0; i < m_dtaTrademarks.Rows.Count; i++)
			{
				if(intTradeMarkId == int.Parse(m_dtaTrademarks.Rows[i]["MarComId"].ToString()))
				{
					return m_dtaTrademarks.Rows[i]["TradeMark"].ToString();
				}				
			}
			return "";
		}

		private void btnSendOrder_Click(object sender, System.EventArgs e)
		{
			// Variable declaration
			DataRow dtrNewOrderLine;
			DataTable dtaNewOrder;
			OleDbCommand odcCommand;
			OleDbDataAdapter odaSaveNewOrder;
			OleDbCommandBuilder ocbSaveNewOrder;
			OleDbTransaction odtTransaction;

			// Variable initialization
			m_blnOrderSent = false;
			dtaNewOrder = new DataTable("Orders");
			
			// Check that a supplier is selected
			if(this.cmbSuppliers.SelectedIndex == -1)
			{
				MessageBox.Show("You must first select a supplier.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			else
				m_intSupplierId = int.Parse(m_dtaSuppliers.Rows[this.cmbSuppliers.SelectedIndex]["FournisseurId"].ToString());

			// Check that an employee is selected
			if(this.cmbEmployees.SelectedIndex == -1)
			{
				MessageBox.Show("You must first select an employee.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			else
				m_intEmployeeId = int.Parse(m_dtaEmployees.Rows[this.cmbEmployees.SelectedIndex]["EmployeeId"].ToString());
			
			try
			{
				odaSaveNewOrder = new OleDbDataAdapter("SELECT * FROM [Orders]", m_odcConnection);
				ocbSaveNewOrder = new OleDbCommandBuilder(odaSaveNewOrder);
				odaSaveNewOrder.Fill(dtaNewOrder);

				foreach(OrderLine olOrderLine in this.olcNewOrder.OrderLines)
				{
					dtrNewOrderLine						= dtaNewOrder.NewRow();
					dtrNewOrderLine["OrderId"]			= m_strNewOrderNumber;
					dtrNewOrderLine["OrderDate"]		= DateTime.Now.ToShortDateString();
					dtrNewOrderLine["MatId"]			= olOrderLine.ProductId;
					dtrNewOrderLine["SubPrId"]			= olOrderLine.SubProductId;
					dtrNewOrderLine["MarComId"]			= olOrderLine.TradeMarkId;
					dtrNewOrderLine["FournisseurId"]	= m_intSupplierId;
					dtrNewOrderLine["EmployeeId"]		= m_intEmployeeId;
					dtrNewOrderLine["OrderQty"]			= olOrderLine.Units;
					dtrNewOrderLine["Pack"]				= olOrderLine.Packaging;
					dtrNewOrderLine["CategoryId"]		= olOrderLine.CategoryId;
					dtrNewOrderLine["Prix"]				= 0;
					dtrNewOrderLine["Checked"]			= 0;
					dtrNewOrderLine["ReceivedQty"]		= 0;
					dtrNewOrderLine["BackOrderUnits"]	= 0;
					dtrNewOrderLine["CanceledBOUnits"]	= 0;
					dtrNewOrderLine["ReturnUnits"]		= 0;
					dtrNewOrderLine["CatalogPay"]		= 0;
					dtrNewOrderLine["Tax"]				= 0;
					dtrNewOrderLine["Transport"]		= 0;
					dtrNewOrderLine["Duty"]				= 0;
					dtrNewOrderLine["TotalPay"]			= 0;
					dtaNewOrder.Rows.Add(dtrNewOrderLine);
				}
				odaSaveNewOrder.Update(dtaNewOrder);
				dtaNewOrder.AcceptChanges();

                // get supplier info
                SupplierInformation siSupplier = new SupplierInformation(m_dtaSuppliers.Rows[this.cmbSuppliers.SelectedIndex]);

				// Display the new order
				fclsOIViewOrdRpt frmOIViewOrdRpt = new fclsOIViewOrdRpt(this, fclsOIViewOrdRpt.ViewOrderReportCaller.ExpressOrder,m_odcConnection);
				frmOIViewOrdRpt.SetOrderInformation(m_strNewOrderNumber,
                                                    siSupplier);

                if (frmOIViewOrdRpt.ShowDialog() == DialogResult.OK)
					this.Close();
				else
				{
					// Delete the new order from the 'Orders' table because it wasn't sent
					odcCommand = m_odcConnection.CreateCommand();
					odtTransaction = m_odcConnection.BeginTransaction();
					odcCommand.Connection = m_odcConnection;
					odcCommand.Transaction = odtTransaction;

					odcCommand.CommandText = "DELETE FROM [Orders] WHERE OrderId=\'" + m_strNewOrderNumber + "\'";
					odcCommand.ExecuteNonQuery();
					odtTransaction.Commit();
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text);
			}
		}

		private void LoadData(string strLevel, int intId)
		{
			int intCurrentId = -1;
			switch(strLevel)
			{
				case "Categories":
					// Open the table Categories
					this.lbxCategories.Items.Clear();
					m_odaCategories = new OleDbDataAdapter("SELECT * FROM [Categories] ORDER BY CategName", m_odcConnection);
					OleDbCommandBuilder ocbCategories = new OleDbCommandBuilder(m_odaCategories);
					m_dtaCategories = new DataTable("Categories");				
					m_odaCategories.Fill(m_dtaCategories);
		
					for (int i = 0; i < m_dtaCategories.Rows.Count; i++)
					{
						intCurrentId = int.Parse(m_dtaCategories.Rows[i]["CategoryId"].ToString());
						if(m_intMaxCategoryKey < intCurrentId)
							m_intMaxCategoryKey = intCurrentId;
						this.lbxCategories.Items.Add(m_dtaCategories.Rows[i]["CategName"].ToString());
					}
					
					m_blnCategoriesLoaded = true;
					m_blnProductsLoaded = false;
					m_blnSubProductsLoaded = false;
				break;

				case "Products":
					lbxProducts.Items.Clear();
					lbxSubProducts.Items.Clear();
					// Open the table Products
					m_odaProducts = new OleDbDataAdapter("SELECT * FROM [Products] WHERE CategoryId = " + intId + " ORDER BY MatName", m_odcConnection);
					OleDbCommandBuilder ocbProducts = new OleDbCommandBuilder(m_odaProducts);
					m_dtaProducts = new DataTable("Products");				
					m_odaProducts.Fill(m_dtaProducts);
					for (int i = 0; i < m_dtaProducts.Rows.Count; i++)
					{
						intCurrentId = int.Parse(m_dtaProducts.Rows[i]["MatId"].ToString());
						if(m_intMaxProductKey < intCurrentId)
							m_intMaxProductKey = intCurrentId;
						this.lbxProducts.Items.Add(m_dtaProducts.Rows[i]["MatName"].ToString());
					}
					
					m_blnProductsLoaded = true;
					m_blnSubProductsLoaded = false;
				break;

				case "Sub-Products":
					// Open the table SubMat
					lbxSubProducts.Items.Clear();
					m_odaSubProducts = new OleDbDataAdapter("SELECT * FROM [SubProducts] WHERE MatId = " + intId + " ORDER BY MatName", m_odcConnection);
					OleDbCommandBuilder ocbSubProducts = new OleDbCommandBuilder(m_odaSubProducts);
					m_dtaSubProducts = new DataTable("SubProducts");				
					m_odaSubProducts.Fill(m_dtaSubProducts);
		
					for (int i = 0; i < m_dtaSubProducts.Rows.Count; i++)
					{
						intCurrentId = int.Parse(m_dtaSubProducts.Rows[i]["SubPrId"].ToString());
						if(m_intMaxSubProductKey < intCurrentId)
							m_intMaxSubProductKey = intCurrentId;
						this.lbxSubProducts.Items.Add(m_dtaSubProducts.Rows[i]["MatName"].ToString() + " [" + this.GetTrademark(int.Parse(m_dtaSubProducts.Rows[i]["MarComId"].ToString())) + "]");
					}
					m_blnSubProductsLoaded = true;
				break;
			}
		}
		
		// Shows / Hides the Product, Packaging, Trademark, Units labels
		private void ChangeLabelVisibility(bool blnVisible)
		{
			/*this.lblProduct.Visible = blnVisible;
			this.lblTrademark.Visible = blnVisible;
			this.lblPackaging.Visible = blnVisible;
			this.lblUnits.Visible = blnVisible;*/
		}
	
		// True if order sent, false if not
		public void SetOrderSentStatus(bool blnOrderSent)
		{
			m_blnOrderSent = blnOrderSent;
		}

		private void fclsSMEmergencyOrder_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(!m_blnOrderSent)
			{
				DialogResult dlgResult;

				if(this.olcNewOrder.NOrderLines > 0)
				{
					dlgResult = MessageBox.Show("Do you really want to close the window and cancel this order?", this.Text, MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

					if (dlgResult == DialogResult.No)
						e.Cancel = true;
					else
						this.m_frmPrevOrders.Close();
				}
			}
		}

		private void lbxCategories_MouseEnter(object sender, System.EventArgs e)
		{
			m_intCurrentListBox = 1;		
		}

		private void lbxProducts_MouseEnter(object sender, System.EventArgs e)
		{
			m_intCurrentListBox = 2;
		}

		private void lbxSubProducts_MouseEnter(object sender, System.EventArgs e)
		{
			m_intCurrentListBox = 3;
		}

		private void ctmRightClick_Popup(object sender, System.EventArgs e)
		{
			if((!m_blnCategoriesLoaded && m_intCurrentListBox == 1) || (!m_blnProductsLoaded && m_intCurrentListBox == 2) || (!m_blnSubProductsLoaded && m_intCurrentListBox == 3))
			{
				this.mnuAdd.Enabled = false;
				this.mnuEdit.Enabled = false;
				this.mnuRemove.Enabled = false;
			}
			else
			{
				if((m_intCurrentListBox == 1 && this.lbxCategories.SelectedIndex == -1) || (m_intCurrentListBox == 2 && this.lbxProducts.SelectedIndex == -1) || (m_intCurrentListBox == 3 && this.lbxSubProducts.SelectedIndex == -1))
				{
					this.mnuEdit.Enabled = false;
					this.mnuRemove.Enabled = false;
				}
				else
				{
					this.mnuEdit.Enabled = true;
					this.mnuRemove.Enabled = true;
				}
				this.mnuAdd.Enabled = true;
			}
		}


		private void mnuAdd_Click(object sender, System.EventArgs e)
		{
			DataRow dtrNewRow;
			fclsDMModifyProduct_SubProd frmDMModifyProduct_SubProd;
			string strResponse = "";

			switch(m_intCurrentListBox)
			{
				case 1:
					strResponse = InputBox.ShowInputBox("Please enter the name of the new category:","Add New Category");
                    if (strResponse != null && strResponse.Length > 0)
					{
						if(!this.DoesItemExist(strResponse,m_dtaCategories,NameCheck.Category))
						{
							m_intMaxCategoryKey++;
							dtrNewRow = m_dtaCategories.NewRow();
							dtrNewRow["CategoryId"] = m_intMaxCategoryKey;
							dtrNewRow["CategName"] = strResponse;
							dtrNewRow["Status"] = 1;

							// Add the new row to the table
							m_dtaCategories.Rows.Add(dtrNewRow);

							// Update the Database
							try
							{
								m_odaCategories.Update(m_dtaCategories);
								m_dtaCategories.AcceptChanges();

								this.LoadData("Categories",-1);
								this.lbxCategories.SelectedIndex = clsUtilities.FindItemIndex(strResponse,this.lbxCategories);
							} 
							catch (OleDbException ex)
							{
								m_dtaCategories.RejectChanges();
								MessageBox.Show(ex.Message, this.Text);
							}
						}
						else
							MessageBox.Show("This category is already in the database!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
				break;

				case 2:
					strResponse = InputBox.ShowInputBox("Please enter the name of the new product:","Add New Product");
                    if (strResponse != null && strResponse.Length > 0)
					{
						if(!this.DoesItemExist(strResponse,m_dtaProducts,NameCheck.Product))
						{
							m_intMaxProductKey++;
							dtrNewRow = m_dtaProducts.NewRow();
							dtrNewRow["MatId"] = m_intMaxProductKey;
							dtrNewRow["MatName"] = strResponse;
							dtrNewRow["CategoryId"] = m_intSelectedCategoryKey;
							dtrNewRow["Status"] = 1;

							// Add the new row to the table
							m_dtaProducts.Rows.Add(dtrNewRow);

							// Update the Database
							try
							{
								m_odaProducts.Update(m_dtaProducts);
								m_dtaProducts.AcceptChanges();

								this.LoadData("Products",m_intSelectedCategoryKey);
								this.lbxProducts.SelectedIndex = clsUtilities.FindItemIndex(strResponse,this.lbxProducts);
							} 
							catch (OleDbException ex)
							{
								m_dtaProducts.RejectChanges();
								MessageBox.Show(ex.Message,this.Text);
							}
						}
						else
							MessageBox.Show("This product is already in the database!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
				break;

				case 3:
					frmDMModifyProduct_SubProd = new fclsDMModifyProduct_SubProd(m_odcConnection,
																				 m_dtaSubProducts,
																				 DSMS.fclsDMModifyProduct_SubProd.WindowPurpose.Add);
					if(frmDMModifyProduct_SubProd.ShowDialog() == DialogResult.OK)
					{
							object[] objSubProductData = new object[4];
							objSubProductData = frmDMModifyProduct_SubProd.GetSubProductData();

							dtrNewRow = m_dtaSubProducts.NewRow();
							dtrNewRow["SubPrId"]		= ++m_intMaxSubProductKey;
							dtrNewRow["MatId"]			= m_intSelectedProductKey;
							dtrNewRow["MatName"]		= (string) objSubProductData[0];
							dtrNewRow["MarComId"]		= (int) objSubProductData[1];
							dtrNewRow["SuplId"]			= 0;
							dtrNewRow["Prix"]			= 0;
							dtrNewRow["Pack"]			= (string) objSubProductData[2];
							dtrNewRow["Reorder"]		= (int) objSubProductData[3];
							dtrNewRow["Invent"]			= 0;
							dtrNewRow["Qtty"]			= 0;
							dtrNewRow["PrixMin"]		= 0;
							dtrNewRow["PrixMax"]		= 0;
							dtrNewRow["PrixMinOi"]		= "0";
							dtrNewRow["PrixOrderId"]	= "0";
							dtrNewRow["PrixMaxOi"]		= "0";
							dtrNewRow["CatalogPay"]		= 0;
							dtrNewRow["Tax"]			= 0;
							dtrNewRow["Transport"]		= 0;
							dtrNewRow["Duty"]			= 0;
							dtrNewRow["TotalPay"]		= 0;
							dtrNewRow["Status"]			= 1;

							// Add the new row to the table
							m_dtaSubProducts.Rows.Add(dtrNewRow);

							// Update the Database
							try
							{
								m_odaSubProducts.Update(m_dtaSubProducts);
								m_dtaSubProducts.AcceptChanges();
							
								this.LoadData("Sub-Products",m_intSelectedProductKey);
								this.lbxSubProducts.SelectedIndex = clsUtilities.FindItemIndex((string) objSubProductData[0], this.lbxSubProducts);
							} 
							catch (OleDbException ex)
							{
								m_dtaSubProducts.RejectChanges();
								MessageBox.Show(ex.Message,this.Text);
							}
					}

					// refresh trademarks datatable (could've been modified while in fclsDMModifyProduct_SubProd
					this.LoadTrademarks();
				break;
			}
		
		}

		private void mnuEdit_Click(object sender, System.EventArgs e)
		{
			fclsDMModifyProduct_SubProd frmDMModifyProduct_SubProd;
			string strResponse = "";

			switch(m_intCurrentListBox)
			{
				case 1:
					strResponse = InputBox.ShowInputBox("Please change the name of the category:","Edit Category",this.lbxCategories.SelectedItem.ToString());
                    if (strResponse != null && strResponse.Length > 0)
					{
						if(!this.DoesItemExist(strResponse,m_dtaCategories,NameCheck.Category))
						{
							m_dtaCategories.Rows[this.lbxCategories.SelectedIndex]["CategName"] = strResponse;

							// update the database
							try
							{
								m_odaCategories.Update(m_dtaCategories);
								// accept the changes and repopulate the list box
								m_dtaCategories.AcceptChanges();
								this.LoadData("Categories",-1);
								this.lbxCategories.SelectedIndex = clsUtilities.FindItemIndex(strResponse,this.lbxCategories);
							}
							catch (OleDbException ex)
							{
								m_dtaCategories.RejectChanges();
								MessageBox.Show(ex.Message,this.Text);
							}
						}
						else
							MessageBox.Show("This category is already in the database!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
				break;

				case 2:
					strResponse = InputBox.ShowInputBox("Please change the name of the product:","Edit Product",this.lbxProducts.SelectedItem.ToString());
                    if (strResponse != null && strResponse.Length > 0)
					{
						if(!this.DoesItemExist(strResponse,m_dtaProducts,NameCheck.Product))
						{
							m_dtaProducts.Rows[this.lbxProducts.SelectedIndex]["MatName"] = strResponse;
							// update the database
							try
							{
								m_odaProducts.Update(m_dtaProducts);

								// accept the changes and repopulate the list box
								m_dtaProducts.AcceptChanges();
								this.LoadData("Products",m_intSelectedCategoryKey);
								this.lbxProducts.SelectedIndex = clsUtilities.FindItemIndex(strResponse,this.lbxProducts);
							}
							catch (OleDbException ex)
							{
								m_dtaProducts.RejectChanges();
								MessageBox.Show(ex.Message,this.Text);
							}
						}
						else
							MessageBox.Show("This product is already in the database!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
					}
				break;
				
				case 3:
					object[] objSubProductData = new object[4];
					objSubProductData[0] = m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["MatName"];
					objSubProductData[1] = m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["MarComId"];
					objSubProductData[2] = m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["Pack"];
					objSubProductData[3] = m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["Reorder"];
					
					frmDMModifyProduct_SubProd = new fclsDMModifyProduct_SubProd(m_odcConnection,
																				 m_dtaSubProducts,
																				 DSMS.fclsDMModifyProduct_SubProd.WindowPurpose.Modify);
					frmDMModifyProduct_SubProd.SetSubProductData(objSubProductData);
					if(frmDMModifyProduct_SubProd.ShowDialog() == DialogResult.OK)
					{
						objSubProductData = frmDMModifyProduct_SubProd.GetSubProductData();

						m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["MatName"]  = (string) objSubProductData[0];
						m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["MarComId"] = (int) objSubProductData[1];
						m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["Pack"]	 = (string) objSubProductData[2];
						m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["Reorder"]  = (int) objSubProductData[3];

						try
						{
							this.m_odaSubProducts.Update(m_dtaSubProducts);

							// accept the changes and repopulate the list box
							m_dtaSubProducts.AcceptChanges();
							this.LoadData("Sub-Products",m_intSelectedProductKey);
							this.lbxSubProducts.SelectedIndex = m_intSelectedSubProductIndex;
						}
						catch (OleDbException ex)
						{
							m_dtaSubProducts.RejectChanges();
							MessageBox.Show(ex.Message,this.Text);
						}
					}

					// refresh trademarks datatable (could've been modified while in fclsDMModifyProduct_SubProd
					this.LoadTrademarks();
				break;
			}		
		}

		private void mnuRemove_Click(object sender, System.EventArgs e)
		{
			string strName;
			switch(m_intCurrentListBox)
			{
				case 1:
					strName = m_dtaCategories.Rows[lbxCategories.SelectedIndex]["CategName"].ToString();
					if(MessageBox.Show("Are you sure you want to remove the '" + strName + "' Category?",this.Text,
						MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						m_dtaCategories.Rows[this.lbxCategories.SelectedIndex].Delete();

						// update the database
						try
						{
							m_odaCategories.Update(m_dtaCategories);

							// accept the changes and repopulate the list box
							m_dtaCategories.AcceptChanges();
							this.LoadData("Categories",-1);
						}
						catch (OleDbException ex)
						{
							m_dtaCategories.RejectChanges();
							MessageBox.Show(ex.Message, this.Text);
						}
					}
					break;

				case 2:
					//					if(this.lbxSubProducts.Items.Count > 0)
				{
					if(MessageBox.Show("Are you sure you want to delete this product and all its associated sub-products?",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						foreach(DataRow dtrCurrentRow in m_dtaSubProducts.Rows)
						{
							dtrCurrentRow.Delete();
						}

						// update the database
						try
						{
							m_odaSubProducts.Update(m_dtaSubProducts);
							m_dtaSubProducts.AcceptChanges();
						}
						catch (OleDbException ex)
						{
							m_dtaSubProducts.RejectChanges();
							MessageBox.Show(ex.Message, this.Text);
						}
					
					

						m_dtaProducts.Rows[this.lbxProducts.SelectedIndex].Delete();

						// update the database
						try
						{
							m_odaProducts.Update(m_dtaProducts);

							// accept the changes and repopulate the list box
							m_dtaProducts.AcceptChanges();
							this.LoadData("Products",m_intSelectedCategoryKey);
						}
						catch (OleDbException ex)
						{
							m_dtaProducts.RejectChanges();
							MessageBox.Show(ex.Message, this.Text);
						}
					}
				}
					break;
				
				case 3:
					strName = m_dtaSubProducts.Rows[lbxSubProducts.SelectedIndex]["MatName"].ToString();
					if(MessageBox.Show("Are you sure you want to remove the '" + strName + "' Sub-Product?",this.Text,
						MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex].Delete();

						// update the database
						try
						{
							this.m_odaSubProducts.Update(m_dtaSubProducts);

							// accept the changes and repopulate the list box
							m_dtaSubProducts.AcceptChanges();
							this.LoadData("Sub-Products",m_intSelectedProductKey);
						}
						catch (OleDbException ex)
						{
							m_dtaSubProducts.RejectChanges();
							MessageBox.Show(ex.Message, this.Text);
						}
					}
					break;
			}		
		}
		private void LoadTrademarks()
		{
			// Variable declaration
			OleDbDataAdapter odaTrademarks;

			// Variable initialization
			m_dtaTrademarks = new DataTable("Trademarks");

			// Open the table Trademarks
			try
			{
				odaTrademarks = new OleDbDataAdapter("SELECT * FROM [Trademarks] ORDER BY Trademark", m_odcConnection);
				odaTrademarks.Fill(m_dtaTrademarks);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message, this.Text);
			}

			m_intNrTrademarks = m_dtaTrademarks.Rows.Count;
		}

		private bool DoesItemExist(string strItemName, DataTable dtaTable, NameCheck enuNameCheck)
		{
			bool blnItemExists = false;
			DataRow[] dtrItemsFound;

			switch(enuNameCheck)
			{
				case NameCheck.Category:
					dtrItemsFound = m_dtaCategories.Select("[CategName] LIKE \'" + strItemName + "\'");

					if(dtrItemsFound.GetLength(0) > 0)
						blnItemExists = true;
				break;

				case NameCheck.Product:
					dtrItemsFound = m_dtaProducts.Select("[MatName] LIKE \'" + strItemName + "\'");

					if(dtrItemsFound.GetLength(0) > 0)
						blnItemExists = true;
				break;
			}

			return blnItemExists;
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","EmergencyOrder.htm");  //

		}

		private void AddToCart()
		{
			OrderLine olNewProduct;

			m_frmPrevOrders.Close();
			
			if(!this.olcNewOrder.IsSubProductAlreadyInOrder(m_intSelectedSubProductKey))
			{
				if(this.olcNewOrder.CanAddOneProduct())
				{
					olNewProduct = new OrderLine(this.olcNewOrder);

					olNewProduct.LineNumber = this.olcNewOrder.NOrderLines + 1;
					olNewProduct.Product = clsUtilities.FormatProduct_Display(this.lbxProducts.SelectedItem.ToString(), m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["MatName"].ToString());
					olNewProduct.CategoryId = m_intSelectedCategoryKey;
					olNewProduct.ProductId = m_intSelectedProductKey;
					olNewProduct.SubProductId = m_intSelectedSubProductKey;
					olNewProduct.Packaging = m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["Pack"].ToString();
					olNewProduct.TradeMarkId = int.Parse(m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["MarComId"].ToString());
					olNewProduct.TradeMark = this.GetTrademark(olNewProduct.TradeMarkId);

					this.olcNewOrder.Add(olNewProduct);

					this.SetOrderControlButtonsEnable(true);
				}
				else
					MessageBox.Show("No more products can be added to this order.", this.Text);
			}
			else
				MessageBox.Show("This product is already in the list.\nPlease choose an another product.",this.Text);
		}

		private void lbxSubProducts_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.lbxSubProducts.SelectedIndex != -1)
				this.AddToCart();
		}

		private void btnResetOrder_Click(object sender, System.EventArgs e)
		{
            int intSupplierID;
			DialogResult dlgResult;

			if(this.olcNewOrder.NOrderLines > 0)
			{
				dlgResult = MessageBox.Show("Do you really want to reset this order?",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

				if (dlgResult == DialogResult.Yes)
				{
					this.olcNewOrder.ClearAll();
					
					// Reset button states
					this.SetOrderControlButtonsEnable(false);

                    intSupplierID = clsConfiguration.General_DefaultSupplierID;
                    this.cmbSuppliers.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intSupplierID, m_dtaSuppliers, 0);
				}
			}		
		}

		private void btnAddToOrder_Click(object sender, System.EventArgs e)
		{
			if(this.lbxSubProducts.SelectedIndex != -1)
				this.AddToCart();		
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void fclsOMEmergencyOrder_Resize(object sender, System.EventArgs e)
		{
			int intListboxSpacing;
			
			// listbox width
			this.lbxCategories.Width = (int) (m_dblCategoryListboxProportion*this.Width);
			this.lbxProducts.Width = (int) (m_dblProductListboxProportion*this.Width);
			this.lbxSubProducts.Width = (int) (m_dblSubProductListboxProportion*this.Width);

			// listbox location
			intListboxSpacing = (this.Width - (this.lbxCategories.Width + this.lbxProducts.Width + this.lbxSubProducts.Width))/4;
			this.lbxCategories.Location = new Point(intListboxSpacing,this.lbxCategories.Location.Y);
			this.lbxProducts.Location = new Point(this.lbxCategories.Location.X + this.lbxCategories.Width + intListboxSpacing,this.lbxProducts.Location.Y);
			this.lbxSubProducts.Location = new Point(this.lbxProducts.Location.X + this.lbxProducts.Width + intListboxSpacing,this.lbxSubProducts.Location.Y);

			// label location
			this.lblSelectCategory.Location = new Point(this.lbxCategories.Location.X,this.lblSelectCategory.Location.Y);
			this.lblSelectProduct.Location = new Point(this.lbxProducts.Location.X,this.lblSelectProduct.Location.Y);
			this.lblSelectSubProduct.Location = new Point(this.lbxSubProducts.Location.X,this.lblSelectSubProduct.Location.Y);
        }
	}
}

