using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Web.Mail;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// With frmOrder you can make an emergency order.
	/// </summary>
	public class fclsOMStandByOrder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox lbxCategories;
		private System.Windows.Forms.ListBox lbxProducts;
		private System.Windows.Forms.ListBox lbxSubProducts;
		private System.Windows.Forms.Label		lblEmployee;
		private System.Windows.Forms.ComboBox	cmbEmployees;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private DSMS.OrderLineContainer olcNewOrder;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TabPage tabpTender;
		private System.Windows.Forms.ComboBox cmbSuppliers;
		private System.Windows.Forms.Label lblSupplier;
		private System.Windows.Forms.TabPage tabpPriceComparison;
		private System.Windows.Forms.Button btnResetOrder;
		private System.Windows.Forms.ContextMenu ctmRightClick;
		private System.Windows.Forms.MenuItem mnuAdd;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuRemove;
		private System.Windows.Forms.Button btnAddToOrder;
		private System.Windows.Forms.Label lblSelectCategory;
		private System.Windows.Forms.Label lblSelectProduct;
		private System.Windows.Forms.Label lblSelectSubProduct;
		private System.Windows.Forms.TabControl tabcOrderControl;
		private System.Windows.Forms.Button btnComparePrices;
        private System.Windows.Forms.Button btnSendTender;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private enum DatabaseTable:int {tempPriceComparison, tempTender};
		private enum ItemCheckType:int {Category, Product};
        private enum FormClosingOption : int { Query, DontQuery_SaveTempOrder, DontQuery_DontSaveTempOrder };
		
		private bool				m_blnCategoriesLoaded, m_blnProductsLoaded, m_blnSubProductsLoaded;
		private bool				m_blnOrdersSent, m_blnTenderSent;
		private DataTable			m_dtaEmployees, m_dtaSuppliers, m_dtaTrademarks;
		private DataTable			m_dtaCategories, m_dtaProducts, m_dtaSubProducts;
		private DataTable			m_dtaAllProducts, m_dtaAllSubProducts;
		private double				m_dblCategoryListboxProportion, m_dblProductListboxProportion, m_dblSubProductListboxProportion;
        private FormClosingOption   m_fcoClosingOptions;
		private int					m_intMaxCategoryKey, m_intMaxProductKey, m_intMaxSubProductKey;
		private int					m_intCurrentListBox;// 0 = default;1 = lbxCategories;2 = lbxProducts;3 = lbxSubProducts
		private int					m_intSelectedCategoryKey, m_intSelectedProductKey, m_intSelectedSubProductKey;
		private int					m_intSelectedSubProductIndex;
		private OleDbConnection		m_odcConnection;
		private OleDbDataAdapter	m_odaCategories, m_odaProducts, m_odaSubProducts;

		public fclsOMStandByOrder(OleDbConnection odcConnection)
		{
			InitializeComponent();

			// initialize global variables
			m_blnCategoriesLoaded = m_blnProductsLoaded = m_blnSubProductsLoaded = false;
			m_blnOrdersSent = m_blnTenderSent = false;
			m_dblCategoryListboxProportion = ((double) this.lbxCategories.Width)/((double) this.Width);
			m_dblProductListboxProportion = ((double) this.lbxProducts.Width)/((double) this.Width);
			m_dblSubProductListboxProportion = ((double) this.lbxSubProducts.Width)/((double) this.Width);
            m_fcoClosingOptions = FormClosingOption.Query;
			m_intMaxCategoryKey = m_intMaxProductKey = m_intMaxSubProductKey = -1;
			m_intCurrentListBox = 0;
			m_intSelectedCategoryKey = m_intSelectedProductKey = m_intSelectedSubProductKey = -1;
			m_intSelectedSubProductIndex = -1;
			m_odcConnection = odcConnection;

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
			this.btnAddToOrder = new System.Windows.Forms.Button();
			this.cmbEmployees = new System.Windows.Forms.ComboBox();
			this.lblEmployee = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnResetOrder = new System.Windows.Forms.Button();
			this.olcNewOrder = new DSMS.OrderLineContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.tabcOrderControl = new System.Windows.Forms.TabControl();
			this.tabpPriceComparison = new System.Windows.Forms.TabPage();
			this.btnComparePrices = new System.Windows.Forms.Button();
			this.tabpTender = new System.Windows.Forms.TabPage();
			this.btnSendTender = new System.Windows.Forms.Button();
			this.cmbSuppliers = new System.Windows.Forms.ComboBox();
			this.lblSupplier = new System.Windows.Forms.Label();
			this.panel1.SuspendLayout();
			this.tabcOrderControl.SuspendLayout();
			this.tabpPriceComparison.SuspendLayout();
			this.tabpTender.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblSelectCategory
			// 
			this.lblSelectCategory.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSelectCategory.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSelectCategory.Location = new System.Drawing.Point(26, 16);
			this.lblSelectCategory.Name = "lblSelectCategory";
			this.lblSelectCategory.Size = new System.Drawing.Size(208, 24);
			this.lblSelectCategory.TabIndex = 0;
			this.lblSelectCategory.Text = "1.  Select a category";
			this.lblSelectCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSelectSubProduct
			// 
			this.lblSelectSubProduct.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSelectSubProduct.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSelectSubProduct.Location = new System.Drawing.Point(577, 16);
			this.lblSelectSubProduct.Name = "lblSelectSubProduct";
			this.lblSelectSubProduct.Size = new System.Drawing.Size(232, 24);
			this.lblSelectSubProduct.TabIndex = 2;
			this.lblSelectSubProduct.Text = "3.  Select a sub-product";
			this.lblSelectSubProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSelectProduct
			// 
			this.lblSelectProduct.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSelectProduct.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
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
			this.lbxCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbxCategories.ForeColor = System.Drawing.Color.Red;
			this.lbxCategories.Location = new System.Drawing.Point(26, 40);
			this.lbxCategories.Name = "lbxCategories";
			this.lbxCategories.Size = new System.Drawing.Size(250, 238);
			this.lbxCategories.TabIndex = 4;
			this.lbxCategories.MouseEnter += new System.EventHandler(this.lbxCategories_MouseEnter);
			this.lbxCategories.SelectedIndexChanged += new System.EventHandler(this.lbxCategories_SelectedIndexChanged);
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
			this.lbxProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbxProducts.ForeColor = System.Drawing.Color.Green;
			this.lbxProducts.Location = new System.Drawing.Point(302, 40);
			this.lbxProducts.Name = "lbxProducts";
			this.lbxProducts.Size = new System.Drawing.Size(250, 238);
			this.lbxProducts.TabIndex = 5;
			this.lbxProducts.MouseEnter += new System.EventHandler(this.lbxProducts_MouseEnter);
			this.lbxProducts.SelectedIndexChanged += new System.EventHandler(this.lbxProducts_SelectedIndexChanged);
			// 
			// lbxSubProducts
			// 
			this.lbxSubProducts.ContextMenu = this.ctmRightClick;
			this.lbxSubProducts.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbxSubProducts.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(192)), ((System.Byte)(0)));
			this.lbxSubProducts.Location = new System.Drawing.Point(577, 40);
			this.lbxSubProducts.Name = "lbxSubProducts";
			this.lbxSubProducts.Size = new System.Drawing.Size(330, 238);
			this.lbxSubProducts.TabIndex = 6;
			this.lbxSubProducts.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.lbxSubProducts_KeyPress);
			this.lbxSubProducts.DoubleClick += new System.EventHandler(this.lbxSubProducts_DoubleClick);
			this.lbxSubProducts.MouseEnter += new System.EventHandler(this.lbxSubProducts_MouseEnter);
			this.lbxSubProducts.SelectedIndexChanged += new System.EventHandler(this.lbxSubProducts_SelectedIndexChanged);
			// 
			// btnAddToOrder
			// 
			this.btnAddToOrder.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnAddToOrder.Enabled = false;
			this.btnAddToOrder.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnAddToOrder.ForeColor = System.Drawing.Color.Green;
			this.btnAddToOrder.Location = new System.Drawing.Point(795, 288);
			this.btnAddToOrder.Name = "btnAddToOrder";
			this.btnAddToOrder.Size = new System.Drawing.Size(112, 24);
			this.btnAddToOrder.TabIndex = 17;
			this.btnAddToOrder.Text = "Add to Order";
			this.btnAddToOrder.Click += new System.EventHandler(this.btnAddToOrder_Click);
			// 
			// cmbEmployees
			// 
			this.cmbEmployees.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.cmbEmployees.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbEmployees.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmbEmployees.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmbEmployees.Location = new System.Drawing.Point(129, 288);
			this.cmbEmployees.Name = "cmbEmployees";
			this.cmbEmployees.Size = new System.Drawing.Size(184, 24);
			this.cmbEmployees.TabIndex = 27;
			// 
			// lblEmployee
			// 
			this.lblEmployee.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblEmployee.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblEmployee.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblEmployee.Location = new System.Drawing.Point(26, 292);
			this.lblEmployee.Name = "lblEmployee";
			this.lblEmployee.Size = new System.Drawing.Size(104, 16);
			this.lblEmployee.TabIndex = 25;
			this.lblEmployee.Text = "Order made by";
			this.lblEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(712, 680);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(96, 32);
			this.btnClose.TabIndex = 45;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.Location = new System.Drawing.Point(824, 680);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(96, 32);
			this.btnHelp.TabIndex = 44;
			this.btnHelp.Text = "Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// btnResetOrder
			// 
			this.btnResetOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnResetOrder.Enabled = false;
			this.btnResetOrder.Location = new System.Drawing.Point(440, 680);
			this.btnResetOrder.Name = "btnResetOrder";
			this.btnResetOrder.Size = new System.Drawing.Size(96, 32);
			this.btnResetOrder.TabIndex = 40;
			this.btnResetOrder.Text = "Reset Order";
			this.btnResetOrder.Click += new System.EventHandler(this.btnResetOrder_Click);
			// 
			// olcNewOrder
			// 
			this.olcNewOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.olcNewOrder.Location = new System.Drawing.Point(5, 328);
			this.olcNewOrder.MaxOrderLines = 15;
			this.olcNewOrder.Name = "olcNewOrder";
			this.olcNewOrder.Size = new System.Drawing.Size(924, 322);
			this.olcNewOrder.TabIndex = 46;
			this.olcNewOrder.OnEmptyOrderLineContainer += new DSMS.fclsOMEmergencyOrder.EmptyOrderLineContainerHandler(this.EmptyOrderLineContainer);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel1.Controls.Add(this.tabcOrderControl);
			this.panel1.Location = new System.Drawing.Point(5, 650);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(416, 64);
			this.panel1.TabIndex = 49;
			// 
			// tabcOrderControl
			// 
			this.tabcOrderControl.Controls.Add(this.tabpPriceComparison);
			this.tabcOrderControl.Controls.Add(this.tabpTender);
			this.tabcOrderControl.Enabled = false;
			this.tabcOrderControl.Location = new System.Drawing.Point(0, 0);
			this.tabcOrderControl.Name = "tabcOrderControl";
			this.tabcOrderControl.SelectedIndex = 0;
			this.tabcOrderControl.Size = new System.Drawing.Size(416, 64);
			this.tabcOrderControl.TabIndex = 49;
			// 
			// tabpPriceComparison
			// 
			this.tabpPriceComparison.Controls.Add(this.btnComparePrices);
			this.tabpPriceComparison.Location = new System.Drawing.Point(4, 22);
			this.tabpPriceComparison.Name = "tabpPriceComparison";
			this.tabpPriceComparison.Size = new System.Drawing.Size(408, 38);
			this.tabpPriceComparison.TabIndex = 1;
			this.tabpPriceComparison.Text = "Price Comparison";
			this.tabpPriceComparison.Visible = false;
			// 
			// btnComparePrices
			// 
			this.btnComparePrices.Location = new System.Drawing.Point(156, 7);
			this.btnComparePrices.Name = "btnComparePrices";
			this.btnComparePrices.Size = new System.Drawing.Size(96, 24);
			this.btnComparePrices.TabIndex = 43;
			this.btnComparePrices.Text = "Compare Prices";
			this.btnComparePrices.Click += new System.EventHandler(this.btnComparePrices_Click);
			// 
			// tabpTender
			// 
			this.tabpTender.Controls.Add(this.btnSendTender);
			this.tabpTender.Controls.Add(this.cmbSuppliers);
			this.tabpTender.Controls.Add(this.lblSupplier);
			this.tabpTender.Location = new System.Drawing.Point(4, 22);
			this.tabpTender.Name = "tabpTender";
			this.tabpTender.Size = new System.Drawing.Size(408, 38);
			this.tabpTender.TabIndex = 0;
			this.tabpTender.Text = "Tender";
			// 
			// btnSendTender
			// 
			this.btnSendTender.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnSendTender.Location = new System.Drawing.Point(304, 8);
			this.btnSendTender.Name = "btnSendTender";
			this.btnSendTender.Size = new System.Drawing.Size(96, 24);
			this.btnSendTender.TabIndex = 44;
			this.btnSendTender.Text = "Send Tender";
			this.btnSendTender.Click += new System.EventHandler(this.btnSendTender_Click);
			// 
			// cmbSuppliers
			// 
			this.cmbSuppliers.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.cmbSuppliers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSuppliers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmbSuppliers.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmbSuppliers.Location = new System.Drawing.Point(72, 8);
			this.cmbSuppliers.Name = "cmbSuppliers";
			this.cmbSuppliers.Size = new System.Drawing.Size(216, 24);
			this.cmbSuppliers.TabIndex = 28;
			// 
			// lblSupplier
			// 
			this.lblSupplier.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblSupplier.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSupplier.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSupplier.Location = new System.Drawing.Point(8, 12);
			this.lblSupplier.Name = "lblSupplier";
			this.lblSupplier.Size = new System.Drawing.Size(64, 16);
			this.lblSupplier.TabIndex = 27;
			this.lblSupplier.Text = "Supplier";
			this.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// fclsOMStandByOrder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(930, 720);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.olcNewOrder);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnResetOrder);
			this.Controls.Add(this.cmbEmployees);
			this.Controls.Add(this.lblEmployee);
			this.Controls.Add(this.btnAddToOrder);
			this.Controls.Add(this.lbxSubProducts);
			this.Controls.Add(this.lbxProducts);
			this.Controls.Add(this.lbxCategories);
			this.Controls.Add(this.lblSelectProduct);
			this.Controls.Add(this.lblSelectSubProduct);
			this.Controls.Add(this.lblSelectCategory);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Location = new System.Drawing.Point(50, 0);
			this.Name = "fclsOMStandByOrder";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quick Stock - Regular Order";
			this.Resize += new System.EventHandler(this.fclsOMStandByOrder_Resize);
			this.Closing += new System.ComponentModel.CancelEventHandler(this.fclsSMStandByOrder_Closing);
			this.Load += new System.EventHandler(this.frmStandByOrder_Load);
			this.panel1.ResumeLayout(false);
			this.tabcOrderControl.ResumeLayout(false);
			this.tabpPriceComparison.ResumeLayout(false);
			this.tabpTender.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		#region Events		
		private void frmStandByOrder_Load(object sender, System.EventArgs e)
		{
			DataTable dtaSavedOrder;
            int intTemp;
			OleDbCommand odcCommand;
			OleDbDataAdapter odaMiscellaneous;
			OleDbTransaction odtTransaction;
			OrderLine olNewProduct;
			
			// load trademarks
			this.LoadTradeMarks();

			// Open the table Categories
			this.LoadData("Categories",-1);

			// load all products
			odaMiscellaneous = new OleDbDataAdapter("SELECT * FROM [Products]", m_odcConnection);
			m_dtaAllProducts = new DataTable();
			odaMiscellaneous.Fill(m_dtaAllProducts);
			
			// load all subproducts
			odaMiscellaneous = new OleDbDataAdapter("SELECT * FROM [SubProducts]", m_odcConnection);
			m_dtaAllSubProducts = new DataTable();
			odaMiscellaneous.Fill(m_dtaAllSubProducts);

			// load employees from database and add them to cmbEmployees
			odaMiscellaneous = new OleDbDataAdapter("SELECT EmployeeId, Title, FirstName, LastName FROM Employees WHERE Status = 1 ORDER BY FirstName, LastName",m_odcConnection);
			m_dtaEmployees = new DataTable();
			odaMiscellaneous.Fill(m_dtaEmployees);
			foreach(DataRow dtrEmployee in m_dtaEmployees.Rows)
				this.cmbEmployees.Items.Add(clsUtilities.FormatName_List(dtrEmployee["Title"].ToString(), dtrEmployee["FirstName"].ToString(), dtrEmployee["LastName"].ToString()));
            intTemp = clsConfiguration.Internal_CurrentUserID;
            this.cmbEmployees.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intTemp, m_dtaEmployees, 0);

			// load suppliers from database and add them to cmbSuppliers
            odaMiscellaneous = new OleDbDataAdapter("SELECT * FROM Suppliers WHERE Status = 1 ORDER BY CompanyName", m_odcConnection);
			m_dtaSuppliers = new DataTable();
			odaMiscellaneous.Fill(m_dtaSuppliers);
			foreach(DataRow dtrSupplier in m_dtaSuppliers.Rows)
				this.cmbSuppliers.Items.Add(dtrSupplier["CompanyName"].ToString());
            intTemp = clsConfiguration.General_DefaultSupplierID;
            this.cmbSuppliers.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intTemp, m_dtaSuppliers, 0);
		
			// check if there is a saved order, and if so, load it
			odaMiscellaneous = new OleDbDataAdapter("SELECT * FROM tempPriceComparison", m_odcConnection);
			dtaSavedOrder = new DataTable();
			odaMiscellaneous.Fill(dtaSavedOrder);
			if(dtaSavedOrder.Rows.Count > 0)
			{
				// load the order
				foreach(DataRow dtrRow in dtaSavedOrder.Rows)
				{
					olNewProduct = new OrderLine(this.olcNewOrder);

					olNewProduct.LineNumber = this.olcNewOrder.NOrderLines + 1;
					olNewProduct.CategoryId = int.Parse(dtrRow["CategId"].ToString());
					olNewProduct.ProductId = int.Parse(dtrRow["MatId"].ToString());
					olNewProduct.SubProductId = int.Parse(dtrRow["SubPrId"].ToString());
					olNewProduct.TradeMarkId = int.Parse(dtrRow["MarComId"].ToString());
					olNewProduct.Product = this.GetProductName(olNewProduct.ProductId) + " - " + this.GetSubProductName(olNewProduct.SubProductId);
					olNewProduct.Packaging = dtrRow["Pack"].ToString();
					olNewProduct.TradeMark = GetTrademarkName(olNewProduct.TradeMarkId);
					olNewProduct.Units = int.Parse(dtrRow["ordUnits"].ToString());
					olNewProduct.UnitPrice1 = decimal.Parse(dtrRow["Comp1Prix"].ToString());
					olNewProduct.UnitPrice2 = decimal.Parse(dtrRow["Comp2Prix"].ToString());
					olNewProduct.UnitPrice3 = decimal.Parse(dtrRow["Comp3Prix"].ToString());
					olNewProduct.UnitPrice4 = decimal.Parse(dtrRow["Comp4Prix"].ToString());
					olNewProduct.Comments = dtrRow["Comments"].ToString();

					this.olcNewOrder.Add(olNewProduct);
				}

				// enable order control buttons
				this.SetOrderControlButtonsEnable(true);

				// delete saved order from database
				odcCommand = m_odcConnection.CreateCommand();
				odtTransaction = m_odcConnection.BeginTransaction();
				odcCommand.Connection = m_odcConnection;
				odcCommand.Transaction = odtTransaction;
				try
				{
					odcCommand.CommandText = "DELETE * FROM tempPriceComparison";
					odcCommand.ExecuteNonQuery();
				
					odtTransaction.Commit();
				}
				catch
				{
					try
					{
						odtTransaction.Rollback();
					}
					catch (OleDbException ex)
					{
						if (odtTransaction.Connection != null)
							MessageBox.Show("An exception of type " + ex.GetType() + " was encountered while attempting to roll back the transaction.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
					}
				}
			}
		}

		private void fclsSMStandByOrder_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
            if (this.olcNewOrder.NOrderLines > 0)
            {
                switch (this.m_fcoClosingOptions)
                {
                    case FormClosingOption.Query:
                        DialogResult dlgResult = MessageBox.Show(this,
                                                                 "Would you like to save this order?",
                                                                 this.Text,
                                                                 MessageBoxButtons.YesNoCancel,
                                                                 MessageBoxIcon.Question,
                                                                 MessageBoxDefaultButton.Button1);
                        switch (dlgResult)
                        {
                            case DialogResult.Yes:
                                this.SaveList(DatabaseTable.tempPriceComparison);
                            break;

                            case DialogResult.Cancel:
                                e.Cancel = true;
                            break;
                        }
                    break;

                    case FormClosingOption.DontQuery_SaveTempOrder:
                        this.SaveList(DatabaseTable.tempPriceComparison);
                    break;
                }
            }
			/*if((this.olcNewOrder.NOrderLines > 0) && !m_blnTenderSent && !m_blnOrdersSent)
			{
				DialogResult dlgResult = MessageBox.Show(this, "Would you like to save this order?", this.Text, MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
				switch(dlgResult)
				{
					case DialogResult.Yes:
						this.SaveList(DatabaseTable.tempPriceComparison);
					break;

					case DialogResult.Cancel:
						e.Cancel = true;
					break;
				}
			}*/
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","StandByOrder.htm");  //

		}

		private void lbxCategories_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(lbxCategories.SelectedIndex != -1)
			{
				m_intSelectedCategoryKey = int.Parse(m_dtaCategories.Rows[lbxCategories.SelectedIndex]["CategoryId"].ToString());
				this.LoadData("Products", m_intSelectedCategoryKey);
			}
		}

		private void lbxProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(lbxProducts.SelectedIndex != -1)
			{
				m_intSelectedProductKey = int.Parse(m_dtaProducts.Rows[lbxProducts.SelectedIndex]["MatId"].ToString());
				this.LoadData("Sub-Products", m_intSelectedProductKey);
			}	
		}

		private void btnAddToOrder_Click(object sender, System.EventArgs e)
		{
			if(this.lbxSubProducts.SelectedIndex != -1)
				this.AddToCart();		
		}

		private void btnResetOrder_Click(object sender, System.EventArgs e)
		{
			DialogResult dlgResult;

			if(this.olcNewOrder.NOrderLines > 0)
			{
				dlgResult = MessageBox.Show("Do you really want to cancel this order?",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

				if (dlgResult == DialogResult.Yes)
				{
					this.SetOrderControlButtonsEnable(false);
					
					this.olcNewOrder.ClearAll();
				}
			}
		}

		private void fclsOMStandByOrder_Resize(object sender, System.EventArgs e)
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

		private void lbxSubProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.lbxSubProducts.SelectedIndex != -1)
			{
				m_intSelectedSubProductIndex = lbxSubProducts.SelectedIndex;
				m_intSelectedSubProductKey = int.Parse(m_dtaSubProducts.Rows[m_intSelectedSubProductIndex]["SubPrId"].ToString());
				this.btnAddToOrder.Enabled = true;
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
			if((!m_blnCategoriesLoaded && m_intCurrentListBox == 1) ||
			   (!m_blnProductsLoaded && m_intCurrentListBox == 2) ||
			   (!m_blnSubProductsLoaded && m_intCurrentListBox == 3))
			{
				this.mnuAdd.Enabled = false;
				this.mnuEdit.Enabled = false;
				this.mnuRemove.Enabled = false;
			}
			else
			{
				if((m_intCurrentListBox == 1 && this.lbxCategories.SelectedIndex == -1) ||
				   (m_intCurrentListBox == 2 && this.lbxProducts.SelectedIndex == -1) ||
				   (m_intCurrentListBox == 3 && this.lbxSubProducts.SelectedIndex == -1))
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
		private void btnSendTender_Click(object sender, System.EventArgs e)
		{
			fclsOIViewOrdRpt frmOIViewOrdRpt;
			OleDbCommand odcCommand;
			OleDbTransaction odtTransaction;
			string m_strNewOrderNumber = "0";

			if(this.cmbEmployees.SelectedIndex != -1)
			{
				if(this.cmbSuppliers.SelectedIndex != -1)
				{
					this.SaveList(DatabaseTable.tempTender);

                    // get supplier information
                    SupplierInformation siSupplier = new SupplierInformation(m_dtaSuppliers.Rows[this.cmbSuppliers.SelectedIndex]);

                    // show report
					frmOIViewOrdRpt = new fclsOIViewOrdRpt(this,fclsOIViewOrdRpt.ViewOrderReportCaller.Tender,m_odcConnection);
					frmOIViewOrdRpt.SetOrderInformation(m_strNewOrderNumber,
                                                        siSupplier);
					frmOIViewOrdRpt.ShowDialog();
					
					// delete saved order from database
					odcCommand = m_odcConnection.CreateCommand();
					odtTransaction = m_odcConnection.BeginTransaction();
					odcCommand.Connection = m_odcConnection;
					odcCommand.Transaction = odtTransaction;
					try
					{
						odcCommand.CommandText = "DELETE * FROM tempTender";
						odcCommand.ExecuteNonQuery();
				
						odtTransaction.Commit();
					}
					catch
					{
						try
						{
							odtTransaction.Rollback();
						}
						catch (OleDbException ex)
						{
							if (odtTransaction.Connection != null)
								MessageBox.Show("An exception of type " + ex.GetType() + " was encountered while attempting to roll back the transaction.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
						}
					}

                    if (m_blnTenderSent)
                    {
                        m_fcoClosingOptions = FormClosingOption.DontQuery_DontSaveTempOrder;
                        this.Close();
                    }
				}
				else
					MessageBox.Show("You must first select a supplier!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			else
				MessageBox.Show("You must first select an employee!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
		}
		private void lbxSubProducts_DoubleClick(object sender, System.EventArgs e)
		{
			if(this.lbxSubProducts.SelectedIndex != -1)
				this.AddToCart();	
		}
		private void mnuAdd_Click(object sender, System.EventArgs e)
		{
			DataRow	dtrNewRow;
			fclsDMModifyProduct_SubProd frmDMModifyProduct_SubProd;
			string strResponse = "";

			switch(m_intCurrentListBox)
			{
				case 1:
					strResponse = InputBox.ShowInputBox("Please enter the name of the new category:","Add New Category");
                    if (strResponse != null && strResponse.Length > 0)
					{
						if(!this.DoesItemExist(strResponse, ItemCheckType.Category))
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
						if(!this.DoesItemExist(strResponse, ItemCheckType.Product))
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
						object[] objSubProductData = frmDMModifyProduct_SubProd.GetSubProductData();

						dtrNewRow = m_dtaSubProducts.NewRow();
						dtrNewRow["SubPrId"]	= ++m_intMaxSubProductKey;
						dtrNewRow["MatId"]		= m_intSelectedProductKey;
						dtrNewRow["MatName"]	= (string) objSubProductData[0];
						dtrNewRow["MarComId"]	= (int) objSubProductData[1];
						dtrNewRow["SuplId"]		= 0;
						dtrNewRow["Prix"]		= 0;
						dtrNewRow["Pack"]		= (string) objSubProductData[2];
						dtrNewRow["Reorder"]	= (int) objSubProductData[3];
						dtrNewRow["Invent"]		= 0;
						dtrNewRow["Qtty"]		= 0;
						dtrNewRow["PrixMin"]	= 0;
						dtrNewRow["PrixMax"]	= 0;
						dtrNewRow["PrixMinOi"]	= 0;
						dtrNewRow["PrixOrderId"]= 0;
						dtrNewRow["PrixMaxOi"]	= 0;
						dtrNewRow["CatalogPay"]	= 0;
						dtrNewRow["Tax"]		= 0;
						dtrNewRow["Transport"]	= 0;
						dtrNewRow["Duty"]		= 0;
						dtrNewRow["TotalPay"]	= 0;
						dtrNewRow["Status"]		= 1;

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
							MessageBox.Show(ex.Message, this.Text);
						}
					}

					// refresh trademarks datatable (could've been modified while in fclsDMModifyProduct_SubProd
					this.LoadTradeMarks();
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
						if(!this.DoesItemExist(strResponse, ItemCheckType.Category))
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
								MessageBox.Show(ex.Message, this.Text);
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
						if(!this.DoesItemExist(strResponse, ItemCheckType.Product))
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
								MessageBox.Show(ex.Message, this.Text);
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
							this.LoadTradeMarks();
							this.LoadData("Sub-Products",m_intSelectedProductKey);
							this.lbxSubProducts.SelectedIndex = m_intSelectedSubProductIndex;
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
		private void mnuRemove_Click(object sender, System.EventArgs e)
		{
			DialogResult dlgResult;

			switch(m_intCurrentListBox)
			{
				case 1:
					dlgResult = MessageBox.Show("Are you sure you want to remove the '" + this.lbxCategories.SelectedItem.ToString() + "' category and all its associated products and sub-products?",this.Text,
						MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
					if(dlgResult == DialogResult.Yes)
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
					dlgResult = MessageBox.Show("Are you sure you want to delete the '" + this.lbxProducts.SelectedItem.ToString() + "' product and all its associated sub-products?",this.Text,
						MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
					if(dlgResult == DialogResult.Yes)
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

					break;
				
				case 3:
					dlgResult = MessageBox.Show("Are you sure you want to remove the '" + this.lbxSubProducts.SelectedItem.ToString() + "' Sub-Product?",this.Text,
						MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
					if(dlgResult == DialogResult.Yes)
					{
						m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex].Delete();

						// update the database
						try
						{
							this.m_odaSubProducts.Update(m_dtaSubProducts);

							// accept the changes and repopulate the list box
							m_dtaSubProducts.AcceptChanges();
							this.LoadTradeMarks();
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

		private void btnComparePrices_Click(object sender, System.EventArgs e)
		{
			fclsOMComparePrices frmOMComparePrices = new fclsOMComparePrices(this, int.Parse(this.m_dtaEmployees.Rows[this.cmbEmployees.SelectedIndex]["EmployeeId"].ToString()), m_odcConnection);
			frmOMComparePrices.LoadPriceComparisonData(this.olcNewOrder.OrderLines);
			DialogResult dlgResult = frmOMComparePrices.ShowDialog();
			
			// if all the orders were sent or 'save & close' was pressed in fclsOMComparePRices, close the form
            if (dlgResult == DialogResult.OK)
            {
                m_fcoClosingOptions = FormClosingOption.DontQuery_SaveTempOrder;
                this.Close();
            }
            else if (m_blnOrdersSent)
            {
                m_fcoClosingOptions = FormClosingOption.DontQuery_DontSaveTempOrder;
                this.Close();
            }
		}

		private void lbxSubProducts_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			if ((e.KeyChar == (char)13) && (this.lbxSubProducts.SelectedIndex != -1))
			{
				this.AddToCart();
				e.Handled = true;
			}
		}
		#endregion
		
		#region Methods
		private string GetProductName(int intProductId)
		{
			string strProductName = "";

			foreach(DataRow dtrRow in m_dtaAllProducts.Rows)
			{
				if(intProductId == int.Parse(dtrRow["MatId"].ToString()))
				{
					strProductName = dtrRow["MatName"].ToString();
					break;
				}
			}
			
			return strProductName;
		}

		private string GetSubProductName(int intSubProductId)
		{
			string strSubProductName = "";

			foreach(DataRow dtrRow in m_dtaAllSubProducts.Rows)
			{
				if(intSubProductId == int.Parse(dtrRow["SubPrId"].ToString()))
				{
					strSubProductName = dtrRow["MatName"].ToString();
					break;
				}
			}

			return strSubProductName;
		}

		private string GetTrademarkName(int intTradermarkId)
		{
			string strTrademark = "";

			for(int i=0; i<m_dtaTrademarks.Rows.Count; i++)
			{
				if(intTradermarkId == int.Parse(m_dtaTrademarks.Rows[i]["MarComId"].ToString()))
				{
					strTrademark = m_dtaTrademarks.Rows[i]["TradeMark"].ToString();
					break;
				}				
			}

			return strTrademark;
		}

		private void SaveList(DatabaseTable enuDatabaseTable)
		{
			DataRow	dtrNewRow;
			DataRow[] dtrFoundRows;
			DataTable dtaMiscellaneous;
			OleDbCommandBuilder ocbMiscellaneous;
			OleDbDataAdapter odaMiscellaneous;
			string strDatabaseTable = "";

			switch(enuDatabaseTable)
			{
				case DatabaseTable.tempPriceComparison:
					strDatabaseTable = "tempPriceComparison";
				break;

				case DatabaseTable.tempTender:
					strDatabaseTable = "tempTender";
				break;
			}
			
			try
			{
				odaMiscellaneous = new OleDbDataAdapter("SELECT * FROM " + strDatabaseTable, m_odcConnection);
				ocbMiscellaneous = new OleDbCommandBuilder(odaMiscellaneous);
				dtaMiscellaneous = new DataTable();
				odaMiscellaneous.Fill(dtaMiscellaneous);
				dtaMiscellaneous.Rows.Clear();

				foreach(OrderLine olOrderLine in this.olcNewOrder.OrderLines)
				{
					dtrNewRow						= dtaMiscellaneous.NewRow();
					dtrNewRow["MatId"]				= olOrderLine.ProductId;
					dtrNewRow["SubPrId"]			= olOrderLine.SubProductId;
					dtrNewRow["MarComId"]			= olOrderLine.TradeMarkId;
					dtrNewRow["Pack"]				= olOrderLine.Packaging;
					dtrNewRow["OrdUnits"]			= olOrderLine.Units;

					if(enuDatabaseTable == DatabaseTable.tempTender)
					{
						dtrNewRow["FournisseurId"] = this.m_dtaSuppliers.Rows[this.cmbSuppliers.SelectedIndex]["FournisseurId"].ToString();
						dtrNewRow["EmployeeId"] = this.m_dtaEmployees.Rows[this.cmbEmployees.SelectedIndex]["EmployeeId"].ToString();
					}

					if(enuDatabaseTable == DatabaseTable.tempPriceComparison)
					{
						dtrNewRow["Comp1Prix"] = olOrderLine.UnitPrice1;
						dtrNewRow["Comp2Prix"] = olOrderLine.UnitPrice2;
						dtrNewRow["Comp3Prix"] = olOrderLine.UnitPrice3;
						dtrNewRow["Comp4Prix"] = olOrderLine.UnitPrice4;
                        dtrNewRow["Comments"]  = olOrderLine.Comments;

						dtrFoundRows = this.m_dtaAllSubProducts.Select("SubPrId =" + olOrderLine.SubProductId);
						
						dtrNewRow["CategId"]			= olOrderLine.CategoryId;
						dtrNewRow["Prix"]				= dtrFoundRows[0]["Prix"];
						dtrNewRow["PrixOrderIdMin"]		= dtrFoundRows[0]["PrixMinOI"];
						dtrNewRow["PrixOrderId"]		= dtrFoundRows[0]["PrixOrderId"];
						dtrNewRow["PrixOrderIdMax"]		= dtrFoundRows[0]["PrixMaxOI"];
					}

					// Add the new row to the table
					dtaMiscellaneous.Rows.Add(dtrNewRow);
				}
			
				// Update the Database
				try
				{
					odaMiscellaneous.Update(dtaMiscellaneous);
					dtaMiscellaneous.AcceptChanges();
				} 
				catch (OleDbException ex)
				{
					dtaMiscellaneous.RejectChanges();
					MessageBox.Show(ex.Message,this.Text);
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace,this.Text);
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

			this.btnAddToOrder.Enabled = false;
		}

		private string GetTrademark(int intTrademarkId)
		{
			for(int i = 0; i < m_dtaTrademarks.Rows.Count; i++)
			{
				if(int.Parse(m_dtaTrademarks.Rows[i]["MarComId"].ToString()) == intTrademarkId)
					return m_dtaTrademarks.Rows[i]["Trademark"].ToString();
			}
			return "";
		}

		private void LoadTradeMarks()
		{
			OleDbDataAdapter odaTrademarks;

			odaTrademarks = new OleDbDataAdapter("SELECT * FROM [Trademarks] ORDER BY Trademark", m_odcConnection);
			m_dtaTrademarks = new DataTable();
			odaTrademarks.Fill(m_dtaTrademarks);
		}
		
		private bool DoesItemExist(string strItemName, ItemCheckType enuCheckType)
		{
			bool blnItemExists = false;
			DataRow[] dtrItemsFound;

			switch(enuCheckType)
			{
				case ItemCheckType.Category:
					dtrItemsFound = m_dtaCategories.Select("[CategName] LIKE \'" + strItemName + "\'");

					if(dtrItemsFound.GetLength(0) > 0)
						blnItemExists = true;
				break;

				case ItemCheckType.Product:
					dtrItemsFound = m_dtaProducts.Select("[MatName] LIKE \'" + strItemName + "\'");

					if(dtrItemsFound.GetLength(0) > 0)
						blnItemExists = true;
				break;
			}

			return blnItemExists;
		}

		private void AddToCart()
		{
			OrderLine olNewProduct;

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
                    if (this.olcNewOrder.NOrderLines > 0)
                        olNewProduct.Comments = ((OrderLine) this.olcNewOrder.OrderLines[this.olcNewOrder.NOrderLines - 1]).Comments;

					this.olcNewOrder.Add(olNewProduct);

					this.SetOrderControlButtonsEnable(true);
				}
				else
					MessageBox.Show("No more products can be added to this order.", this.Text);
			}
			else
				MessageBox.Show("This product is already in the list.\nPlease choose an another product.",this.Text);
		}

		private void SetOrderControlButtonsEnable(bool blnEnabled)
		{
			this.tabcOrderControl.Enabled = blnEnabled;
			this.btnResetOrder.Enabled = blnEnabled;
		}
		public void SetTenderSentStatus(bool blnTenderSent)
		{
			m_blnTenderSent = blnTenderSent;
		}
		public void SetOrdersSentStatus(bool blnOrdersSent)
		{
			m_blnOrdersSent = blnOrdersSent;
		}
		private void EmptyOrderLineContainer()
		{
			this.SetOrderControlButtonsEnable(false);
		}

		public void SetPriceComparisonData(ArrayList alPriceComparisonLines, string strComments)
		{
			ComparePricesLine cplCurrentComparePricesLine;
			OrderLine olCurrentOrderLine;
			
			for(int i=0; i<alPriceComparisonLines.Count; i++)
			{
				cplCurrentComparePricesLine = (ComparePricesLine) alPriceComparisonLines[i];
				olCurrentOrderLine = (OrderLine) this.olcNewOrder.OrderLines[i];
				
				olCurrentOrderLine.Units = cplCurrentComparePricesLine.Units;
				olCurrentOrderLine.UnitPrice1 = cplCurrentComparePricesLine.UnitPrice1;
				olCurrentOrderLine.UnitPrice2 = cplCurrentComparePricesLine.UnitPrice2;
				olCurrentOrderLine.UnitPrice3 = cplCurrentComparePricesLine.UnitPrice3;
				olCurrentOrderLine.UnitPrice4 = cplCurrentComparePricesLine.UnitPrice4;
				olCurrentOrderLine.Comments = strComments;
			}
		}
		#endregion


	}
}