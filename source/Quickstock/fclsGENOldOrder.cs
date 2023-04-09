using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for frmOrder.
	/// </summary>
	public class fclsGENOldOrder : System.Windows.Forms.Form
	{
		private System.Windows.Forms.ListBox lbxCategories;
		private System.Windows.Forms.Button btnCancelList;
		private System.Windows.Forms.Button btnAddToCart;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnSaveOrder;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.ListBox lbxProducts;
		private System.Windows.Forms.ListBox lbxSubProducts;
		private System.Windows.Forms.TextBox txtOrderId;
		private System.Windows.Forms.TextBox txtShippingHandling;
		private System.Windows.Forms.TextBox txtTaxes;
		private System.Windows.Forms.TextBox txtDuty;
		private System.Windows.Forms.ComboBox cmbSuppliers;
		private System.Windows.Forms.ComboBox cmbEmployees;
		private System.Windows.Forms.DateTimePicker dtpDate;
		private System.Windows.Forms.DateTimePicker dtpPayment;
		private System.Windows.Forms.Label lblSelectCategory;
		private System.Windows.Forms.Label lblSelectSubProduct;
		private System.Windows.Forms.Label lblSelectProduct;
		private System.Windows.Forms.Label lblOrderNumber;
		private System.Windows.Forms.Label lblSupplier;
		private System.Windows.Forms.Label lblOrderMadeBy;
		private System.Windows.Forms.Label lblTrademark;
		private System.Windows.Forms.Label lblProduct;
		private System.Windows.Forms.Label lblPackaging;
		private System.Windows.Forms.Label lblUnits;
		private System.Windows.Forms.Label lblDuty;
		private System.Windows.Forms.Label lblShippingHandling;
		private System.Windows.Forms.Label lblTaxes;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label1;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection		m_odcConnection;
		private bool				m_blnOrderSent = false;//, m_blnSendOrderClick = false;
		private OleDbDataAdapter	m_odaSaveNewOrder, m_odaSuppliers, m_odaSaveNewPayment, m_odaUpdateSubProd;
		private DataTable			m_dtaNewOrder, m_dtaNewPayment, m_dtaUpdateSubProd;
		private DataTable			m_dtaEmployees, m_dtaSuppliers, m_dtaTrademarks;
		private OleDbDataAdapter	m_odaCategories,m_odaProducts,m_odaSubProducts;
		private DataTable			m_dtaCategories, m_dtaProducts, m_dtaSubProducts;
		private int					m_intNrTrademarks = -1, m_intOrderLineNr = 0;
		private int					m_intSupplierId = -1 , m_intEmployeeId = -1;
		private int					m_intFormHeight;
		private int					m_intButtonYPos, m_intOrderLineYPos, m_intVerticalSpacing;
		private string				m_strNewOrderNumber;
		public static int			m_intOptionType;		// 0 All Orders		1 Payed Orders	2 Not Payed Orders
		public static int			m_intPayedBy;
		public static string		m_strPaymentDate, m_strPayedSum, m_strPayedPenalty, m_strPayedPer;
		public static DateTime		m_dtPaymentDate;
		private OldOrderLine[]		orderLines = new OldOrderLine[16];

		string						strTax, strTransport, strDuty, strTotal, strTotalCatalog;
		double						flTax, flTransport, flDuty, flTotal, flTotalPay;
		double						flRaport = 1.0, flTotalCatalog = 0.0, flCatalogPay;
		double []					flTX = new double [16];
		double []					flTR = new double [16];
		double []					flDU = new double [16];

		private System.Windows.Forms.MenuItem menuCategory;
		private System.Windows.Forms.MenuItem menuProduct;
		private System.Windows.Forms.ContextMenu ctmRightClick;
		private System.Windows.Forms.MenuItem mnuAdd;
		private System.Windows.Forms.MenuItem mnuEdit;
		private System.Windows.Forms.MenuItem mnuRemove;

		private bool		m_blnCategoriesLoaded = false, m_blnProductsLoaded = false, m_blnSubProductsLoaded = false;
		private int			m_intCurrentListBox = 0;// 0 = default;1 = lbxCategories;2 = lbxProducts;3 = lbxSubProducts
		private int			m_intMaxCategoryKey = -1, m_intMaxProductKey = -1, m_intMaxSubProductKey = -1;
		private int			m_intSelectedCategoryKey = -1, m_intSelectedProductKey = -1, m_intSelectedSubProductKey = -1;
		private int			m_intSelectedSubProductIndex = -1, m_intOldYear;

		private bool m_blnOrderPaid;

		public fclsGENOldOrder(OleDbConnection odcConnection, int oldYear)
		{
			InitializeComponent();

			m_blnOrderPaid = false;
			m_odcConnection = odcConnection;
			m_intOldYear = oldYear;

			m_intVerticalSpacing = 21;
			m_intOrderLineYPos = this.lbxCategories.Location.Y + 276;
			m_intButtonYPos = this.lbxCategories.Location.Y + 310;
			this.SetBounds((Screen.PrimaryScreen.Bounds.Width - this.Width) / 2,this.Location.Y,this.Width,this.Height);
			m_intFormHeight = this.Height;
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
			this.txtOrderId = new System.Windows.Forms.TextBox();
			this.cmbSuppliers = new System.Windows.Forms.ComboBox();
			this.cmbEmployees = new System.Windows.Forms.ComboBox();
			this.lblTrademark = new System.Windows.Forms.Label();
			this.lblProduct = new System.Windows.Forms.Label();
			this.lblPackaging = new System.Windows.Forms.Label();
			this.lblUnits = new System.Windows.Forms.Label();
			this.btnAddToCart = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnCancelList = new System.Windows.Forms.Button();
			this.btnSaveOrder = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.dtpDate = new System.Windows.Forms.DateTimePicker();
			this.lblDuty = new System.Windows.Forms.Label();
			this.txtShippingHandling = new System.Windows.Forms.TextBox();
			this.lblShippingHandling = new System.Windows.Forms.Label();
			this.txtTaxes = new System.Windows.Forms.TextBox();
			this.lblTaxes = new System.Windows.Forms.Label();
			this.txtDuty = new System.Windows.Forms.TextBox();
			this.dtpPayment = new System.Windows.Forms.DateTimePicker();
			this.label2 = new System.Windows.Forms.Label();
			this.menuCategory = new System.Windows.Forms.MenuItem();
			this.menuProduct = new System.Windows.Forms.MenuItem();
			this.btnHelp = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// lblSelectCategory
			// 
			this.lblSelectCategory.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSelectCategory.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSelectCategory.Location = new System.Drawing.Point(16, 16);
			this.lblSelectCategory.Name = "lblSelectCategory";
			this.lblSelectCategory.Size = new System.Drawing.Size(208, 24);
			this.lblSelectCategory.TabIndex = 0;
			this.lblSelectCategory.Text = "1.  Select Category";
			this.lblSelectCategory.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSelectSubProduct
			// 
			this.lblSelectSubProduct.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSelectSubProduct.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSelectSubProduct.Location = new System.Drawing.Point(576, 16);
			this.lblSelectSubProduct.Name = "lblSelectSubProduct";
			this.lblSelectSubProduct.Size = new System.Drawing.Size(232, 24);
			this.lblSelectSubProduct.TabIndex = 2;
			this.lblSelectSubProduct.Text = "3.  Select Sub-Product";
			this.lblSelectSubProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSelectProduct
			// 
			this.lblSelectProduct.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSelectProduct.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSelectProduct.Location = new System.Drawing.Point(296, 16);
			this.lblSelectProduct.Name = "lblSelectProduct";
			this.lblSelectProduct.Size = new System.Drawing.Size(208, 24);
			this.lblSelectProduct.TabIndex = 3;
			this.lblSelectProduct.Text = "2.  Select Product";
			this.lblSelectProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lbxCategories
			// 
			this.lbxCategories.ContextMenu = this.ctmRightClick;
			this.lbxCategories.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lbxCategories.ForeColor = System.Drawing.Color.Red;
			this.lbxCategories.Location = new System.Drawing.Point(16, 40);
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
			this.ctmRightClick.Popup += new System.EventHandler(this.ctmRightclick_Popup);
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
			this.lbxProducts.Location = new System.Drawing.Point(304, 40);
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
			this.lbxSubProducts.Location = new System.Drawing.Point(576, 40);
			this.lbxSubProducts.Name = "lbxSubProducts";
			this.lbxSubProducts.Size = new System.Drawing.Size(330, 238);
			this.lbxSubProducts.TabIndex = 6;
			this.lbxSubProducts.MouseEnter += new System.EventHandler(this.lbxSubProducts_MouseEnter);
			this.lbxSubProducts.SelectedIndexChanged += new System.EventHandler(this.lbxSubProducts_SelectedIndexChanged);
			// 
			// lblOrderNumber
			// 
			this.lblOrderNumber.AutoSize = true;
			this.lblOrderNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblOrderNumber.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblOrderNumber.Location = new System.Drawing.Point(16, 291);
			this.lblOrderNumber.Name = "lblOrderNumber";
			this.lblOrderNumber.Size = new System.Drawing.Size(67, 19);
			this.lblOrderNumber.TabIndex = 7;
			this.lblOrderNumber.Text = "Order Nr.";
			this.lblOrderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSupplier
			// 
			this.lblSupplier.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSupplier.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSupplier.Location = new System.Drawing.Point(304, 292);
			this.lblSupplier.Name = "lblSupplier";
			this.lblSupplier.Size = new System.Drawing.Size(64, 16);
			this.lblSupplier.TabIndex = 8;
			this.lblSupplier.Text = "Supplier";
			this.lblSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblOrderMadeBy
			// 
			this.lblOrderMadeBy.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblOrderMadeBy.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblOrderMadeBy.Location = new System.Drawing.Point(536, 292);
			this.lblOrderMadeBy.Name = "lblOrderMadeBy";
			this.lblOrderMadeBy.Size = new System.Drawing.Size(104, 16);
			this.lblOrderMadeBy.TabIndex = 9;
			this.lblOrderMadeBy.Text = "Order made by";
			this.lblOrderMadeBy.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtOrderId
			// 
			this.txtOrderId.Enabled = false;
			this.txtOrderId.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtOrderId.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.txtOrderId.Location = new System.Drawing.Point(80, 289);
			this.txtOrderId.Name = "txtOrderId";
			this.txtOrderId.Size = new System.Drawing.Size(64, 23);
			this.txtOrderId.TabIndex = 10;
			this.txtOrderId.Text = "";
			// 
			// cmbSuppliers
			// 
			this.cmbSuppliers.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbSuppliers.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmbSuppliers.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmbSuppliers.Location = new System.Drawing.Point(360, 288);
			this.cmbSuppliers.Name = "cmbSuppliers";
			this.cmbSuppliers.Size = new System.Drawing.Size(176, 24);
			this.cmbSuppliers.TabIndex = 11;
			// 
			// cmbEmployees
			// 
			this.cmbEmployees.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbEmployees.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmbEmployees.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmbEmployees.Location = new System.Drawing.Point(640, 288);
			this.cmbEmployees.Name = "cmbEmployees";
			this.cmbEmployees.Size = new System.Drawing.Size(168, 24);
			this.cmbEmployees.TabIndex = 12;
			// 
			// lblTrademark
			// 
			this.lblTrademark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTrademark.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblTrademark.Location = new System.Drawing.Point(504, 320);
			this.lblTrademark.Name = "lblTrademark";
			this.lblTrademark.Size = new System.Drawing.Size(96, 16);
			this.lblTrademark.TabIndex = 13;
			this.lblTrademark.Text = "Trademark";
			// 
			// lblProduct
			// 
			this.lblProduct.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProduct.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblProduct.Location = new System.Drawing.Point(48, 320);
			this.lblProduct.Name = "lblProduct";
			this.lblProduct.Size = new System.Drawing.Size(96, 16);
			this.lblProduct.TabIndex = 14;
			this.lblProduct.Text = "Product";
			// 
			// lblPackaging
			// 
			this.lblPackaging.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPackaging.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblPackaging.Location = new System.Drawing.Point(648, 320);
			this.lblPackaging.Name = "lblPackaging";
			this.lblPackaging.Size = new System.Drawing.Size(96, 16);
			this.lblPackaging.TabIndex = 15;
			this.lblPackaging.Text = "Packaging";
			// 
			// lblUnits
			// 
			this.lblUnits.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUnits.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblUnits.Location = new System.Drawing.Point(816, 320);
			this.lblUnits.Name = "lblUnits";
			this.lblUnits.Size = new System.Drawing.Size(104, 16);
			this.lblUnits.TabIndex = 16;
			this.lblUnits.Text = "Units      Price";
			// 
			// btnAddToCart
			// 
			this.btnAddToCart.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnAddToCart.ForeColor = System.Drawing.Color.Green;
			this.btnAddToCart.Location = new System.Drawing.Point(816, 288);
			this.btnAddToCart.Name = "btnAddToCart";
			this.btnAddToCart.Size = new System.Drawing.Size(96, 24);
			this.btnAddToCart.TabIndex = 17;
			this.btnAddToCart.Text = "Add to Cart";
			this.btnAddToCart.Click += new System.EventHandler(this.btnAddToCart_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(736, 344);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(120, 24);
			this.btnClose.TabIndex = 18;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnCloseForm_Click);
			// 
			// btnCancelList
			// 
			this.btnCancelList.Location = new System.Drawing.Point(600, 344);
			this.btnCancelList.Name = "btnCancelList";
			this.btnCancelList.Size = new System.Drawing.Size(120, 24);
			this.btnCancelList.TabIndex = 19;
			this.btnCancelList.Text = "Cancel List";
			this.btnCancelList.Click += new System.EventHandler(this.btnCancelList_Click);
			// 
			// btnSaveOrder
			// 
			this.btnSaveOrder.Location = new System.Drawing.Point(464, 344);
			this.btnSaveOrder.Name = "btnSaveOrder";
			this.btnSaveOrder.Size = new System.Drawing.Size(120, 24);
			this.btnSaveOrder.TabIndex = 20;
			this.btnSaveOrder.Text = "Save this Order";
			this.btnSaveOrder.Click += new System.EventHandler(this.btnSaveOrder_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label1.Location = new System.Drawing.Point(144, 280);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(48, 32);
			this.label1.TabIndex = 21;
			this.label1.Text = "Order Date";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dtpDate
			// 
			this.dtpDate.Location = new System.Drawing.Point(184, 288);
			this.dtpDate.Name = "dtpDate";
			this.dtpDate.Size = new System.Drawing.Size(120, 20);
			this.dtpDate.TabIndex = 22;
			// 
			// lblDuty
			// 
			this.lblDuty.AutoSize = true;
			this.lblDuty.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.lblDuty.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblDuty.Location = new System.Drawing.Point(344, 344);
			this.lblDuty.Name = "lblDuty";
			this.lblDuty.Size = new System.Drawing.Size(33, 19);
			this.lblDuty.TabIndex = 29;
			this.lblDuty.Text = "Duty";
			// 
			// txtShippingHandling
			// 
			this.txtShippingHandling.Location = new System.Drawing.Point(272, 344);
			this.txtShippingHandling.Name = "txtShippingHandling";
			this.txtShippingHandling.Size = new System.Drawing.Size(56, 20);
			this.txtShippingHandling.TabIndex = 28;
			this.txtShippingHandling.Text = "0.00";
			this.txtShippingHandling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtShippingHandling.TextChanged += new System.EventHandler(this.txtShippingHandling_TextChanged);
			this.txtShippingHandling.Leave += new System.EventHandler(this.txtShippingHandling_Leave);
			// 
			// lblShippingHandling
			// 
			this.lblShippingHandling.AutoSize = true;
			this.lblShippingHandling.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.lblShippingHandling.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblShippingHandling.Location = new System.Drawing.Point(128, 344);
			this.lblShippingHandling.Name = "lblShippingHandling";
			this.lblShippingHandling.Size = new System.Drawing.Size(138, 19);
			this.lblShippingHandling.TabIndex = 27;
			this.lblShippingHandling.Text = "Shipping and Handling";
			// 
			// txtTaxes
			// 
			this.txtTaxes.Location = new System.Drawing.Point(56, 344);
			this.txtTaxes.Name = "txtTaxes";
			this.txtTaxes.Size = new System.Drawing.Size(56, 20);
			this.txtTaxes.TabIndex = 26;
			this.txtTaxes.Text = "0.00";
			this.txtTaxes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtTaxes.TextChanged += new System.EventHandler(this.txtTaxes_TextChanged);
			this.txtTaxes.Leave += new System.EventHandler(this.txtTaxes_Leave);
			// 
			// lblTaxes
			// 
			this.lblTaxes.AutoSize = true;
			this.lblTaxes.Font = new System.Drawing.Font("Tahoma", 9.75F);
			this.lblTaxes.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblTaxes.Location = new System.Drawing.Point(8, 344);
			this.lblTaxes.Name = "lblTaxes";
			this.lblTaxes.Size = new System.Drawing.Size(39, 19);
			this.lblTaxes.TabIndex = 25;
			this.lblTaxes.Text = "Taxes";
			// 
			// txtDuty
			// 
			this.txtDuty.Location = new System.Drawing.Point(384, 344);
			this.txtDuty.Name = "txtDuty";
			this.txtDuty.Size = new System.Drawing.Size(56, 20);
			this.txtDuty.TabIndex = 30;
			this.txtDuty.Text = "0.00";
			this.txtDuty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			this.txtDuty.TextChanged += new System.EventHandler(this.txtDuty_TextChanged);
			this.txtDuty.Leave += new System.EventHandler(this.txtDuty_Leave);
			// 
			// dtpPayment
			// 
			this.dtpPayment.CustomFormat = "";
			this.dtpPayment.Location = new System.Drawing.Point(184, 312);
			this.dtpPayment.Name = "dtpPayment";
			this.dtpPayment.Size = new System.Drawing.Size(120, 20);
			this.dtpPayment.TabIndex = 32;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label2.Location = new System.Drawing.Point(120, 314);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(72, 16);
			this.label2.TabIndex = 31;
			this.label2.Text = "Pay Date";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// menuCategory
			// 
			this.menuCategory.Index = -1;
			this.menuCategory.Text = "";
			// 
			// menuProduct
			// 
			this.menuProduct.Index = -1;
			this.menuProduct.Text = "";
			// 
			// btnHelp
			// 
			this.btnHelp.Location = new System.Drawing.Point(864, 344);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(56, 23);
			this.btnHelp.TabIndex = 33;
			this.btnHelp.Text = "Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// fclsGENOldOrder
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(932, 382);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.dtpPayment);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.txtDuty);
			this.Controls.Add(this.lblDuty);
			this.Controls.Add(this.txtShippingHandling);
			this.Controls.Add(this.lblShippingHandling);
			this.Controls.Add(this.txtTaxes);
			this.Controls.Add(this.lblTaxes);
			this.Controls.Add(this.txtOrderId);
			this.Controls.Add(this.lblOrderNumber);
			this.Controls.Add(this.dtpDate);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnSaveOrder);
			this.Controls.Add(this.btnCancelList);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnAddToCart);
			this.Controls.Add(this.lblUnits);
			this.Controls.Add(this.lblPackaging);
			this.Controls.Add(this.lblProduct);
			this.Controls.Add(this.lblTrademark);
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
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Location = new System.Drawing.Point(50, 0);
			this.Name = "fclsGENOldOrder";
			this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
			this.Text = "Quick Stock - Past Invoices";
			this.Load += new System.EventHandler(this.fclsGENOldOrder_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void fclsGENOldOrder_Load(object sender, System.EventArgs e)
		{
			//MessageBox.Show(System.Threading.Thread.CurrentThread.CurrentCulture.ToString());
			DataRow dtrEmployee;
			string strEmployee;

			this.lblProduct.Visible = false;
			this.lblTrademark.Visible = false;
			this.lblPackaging.Visible = false;
			this.lblUnits.Visible = false;
			this.btnAddToCart.Visible = false;
			this.btnCancelList.Visible = false;
			this.btnSaveOrder.Visible = false;
			this.lblDuty.Visible = false;
			this.lblShippingHandling.Visible = false;
			this.lblTaxes.Visible = false;
			this.txtDuty.Visible = false;
			this.txtShippingHandling.Visible = false;
			this.txtTaxes.Visible = false;
			fclsGENInput.indPayFrom = 3;

			m_strNewOrderNumber = GetNewOrderNumber();
			txtOrderId.Text = m_strNewOrderNumber;

			this.LoadTradeMarks();

			// Open the table Suppliers
			m_odaSuppliers = new OleDbDataAdapter("SELECT * FROM [Suppliers] ORDER BY CompanyName", m_odcConnection);
			m_dtaSuppliers = new DataTable("Suppliers");				
			m_odaSuppliers.Fill(m_dtaSuppliers);

			for (int i = 0; i < m_dtaSuppliers.Rows.Count; i++)
			{
				this.cmbSuppliers.Items.Add(m_dtaSuppliers.Rows[i]["CompanyName"].ToString());
			}

            int intDefaultSupplier = clsConfiguration.General_DefaultSupplierID;
            this.cmbSuppliers.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intDefaultSupplier, m_dtaSuppliers, 0);
				
			// Open the table Employees
			OleDbDataAdapter odaEmployees = new OleDbDataAdapter("SELECT * FROM [Employees] WHERE Status = 1 ORDER BY FirstName, LastName", m_odcConnection);
			m_dtaEmployees = new DataTable("Employees");				
			odaEmployees.Fill(m_dtaEmployees);
		
			for (int i = 0; i < m_dtaEmployees.Rows.Count; i++)
			{
				dtrEmployee = m_dtaEmployees.Rows[i];
				if(int.Parse(dtrEmployee["Status"].ToString()) == 1)
				{
					strEmployee = dtrEmployee["Title"].ToString() + " " + dtrEmployee["FirstName"].ToString() + ", " + dtrEmployee["LastName"].ToString();
					this.cmbEmployees.Items.Add(strEmployee);
				}
			}
			
			this.LoadData("Categories",-1);

            int intDefaultUser = clsConfiguration.Internal_CurrentUserID;
            this.cmbEmployees.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intDefaultUser, m_dtaEmployees, 0);
        }

		private string GetNewOrderNumber()
		{
			int intLastOrderNumber = -1;
			string strLastOrderNumber;
            string[] strMan;
			string strLast2Digit = m_intOldYear.ToString().Substring(2,2);
		    
			// Open the table Orders
			OleDbDataAdapter odaOrders = new OleDbDataAdapter("SELECT * FROM Orders WHERE Orders.OrderId LIKE \'%" + strLast2Digit + "\' ORDER BY Orders.OrderId", m_odcConnection);
			DataTable dtaOrders = new DataTable();
			try
			{
				odaOrders.Fill(dtaOrders);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			intLastOrderNumber = 0;
			if(dtaOrders.Rows.Count > 0)				
			{
				int i = dtaOrders.Rows.Count - 1;
				strLastOrderNumber = dtaOrders.Rows[i]["OrderId"].ToString();
				strMan = strLastOrderNumber.Split('-');
				if(int.Parse(strMan[0]) > intLastOrderNumber)
					intLastOrderNumber = int.Parse(strMan[0]);
			}
			string strNewOrderNr ="";
			if(intLastOrderNumber < 9)
				strNewOrderNr = "00";
			if((intLastOrderNumber > 8) && (intLastOrderNumber < 99))
				strNewOrderNr = "0";
			strNewOrderNr += (++intLastOrderNumber).ToString() + "-" + strLast2Digit;
			return strNewOrderNr;
		}

		private void btnCloseForm_Click(object sender, System.EventArgs e)
		{
			this.Close();
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

		private void lbxSubProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.lbxSubProducts.SelectedIndex != -1)
			{
				m_intSelectedSubProductIndex = lbxSubProducts.SelectedIndex;
				m_intSelectedSubProductKey = int.Parse(m_dtaSubProducts.Rows[m_intSelectedSubProductIndex]["SubPrId"].ToString());
				this.btnAddToCart.Visible = true;
			}
		}

		private void btnAddToCart_Click(object sender, System.EventArgs e)
		{
			this.ChangeLabelVisibility(true);
//			frmPrevOrders.Visible = false;
//			this.m_blnSendOrderClick = false;

			this.btnCancelList.Visible = true;
			this.btnSaveOrder.Visible = true;
			this.btnAddToCart.Visible = false;
			++m_intOrderLineNr;
			if(m_intOrderLineNr >15)
			{
				MessageBox.Show("No more room for another product!!!");
				--m_intOrderLineNr;
				return;
			}
			if(m_intOrderLineNr > 1)
				for(int i=1; i<m_intOrderLineNr; i++)
					if(orderLines[i].SubProductId == m_intSelectedSubProductKey)
					{
						MessageBox.Show("This product is already in the list.\n" +
							"Please choose an another product!!");
						--m_intOrderLineNr;
						return;
					}

			this.AddNewOrderLine();
			orderLines[m_intOrderLineNr].ProdName = this.lbxProducts.SelectedItem.ToString() + ", " + this.lbxSubProducts.SelectedItem.ToString();
			orderLines[m_intOrderLineNr].CategoryId = m_intSelectedCategoryKey;
			orderLines[m_intOrderLineNr].ProductId = m_intSelectedProductKey;
			orderLines[m_intOrderLineNr].SubProductId = m_intSelectedSubProductKey;
			orderLines[m_intOrderLineNr].Number = m_intOrderLineNr.ToString();
			orderLines[m_intOrderLineNr].Packaging = m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["Pack"].ToString();
			orderLines[m_intOrderLineNr].TradeMarkId = int.Parse(m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["MarComId"].ToString());
			orderLines[m_intOrderLineNr].TradeMark = this.GetTradeMark(orderLines[m_intOrderLineNr].TradeMarkId);
	}

		private void orderLines_Click(object sender, System.EventArgs e)
		{
			System.Windows.Forms.Label label = (System.Windows.Forms.Label) sender;
			int clickIndex = int.Parse(label.Text);
						
			switch(MessageBox.Show("Do you really want to remove the product on the line "+clickIndex+"\n from this order ?",
				"Remove Line",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2))
			{
				case DialogResult.Yes:
					if(clickIndex != m_intOrderLineNr)
					{
						for(int i=clickIndex; i<m_intOrderLineNr && i>0; i++)
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
					this.RemoveLastOrderline();
					if(m_intOrderLineNr == 0)
					{
						this.ChangeLabelVisibility(false);
						this.btnAddToCart.Visible = false;
						this.btnCancelList.Visible = false;
						this.btnSaveOrder.Visible = false;
					}
					break;
			}
		}

		public void AddNewOrderLine()
		{
			m_intButtonYPos += m_intVerticalSpacing;
			m_intOrderLineYPos += m_intVerticalSpacing;
			m_intFormHeight = m_intButtonYPos + 30;
			this.ChangeButtonYPosition(m_intButtonYPos);
			this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, m_intFormHeight);

			orderLines[m_intOrderLineNr] = new OldOrderLine();
			orderLines[m_intOrderLineNr].Location = new System.Drawing.Point(0, m_intOrderLineYPos);
			orderLines[m_intOrderLineNr].Name = "orderLine1";
			orderLines[m_intOrderLineNr].TabIndex = 40;
			orderLines[m_intOrderLineNr].Size = new System.Drawing.Size(928, 21);
			orderLines[m_intOrderLineNr].lblNumber.Click += new System.EventHandler(this.orderLines_Click);
			this.Controls.Add(orderLines[m_intOrderLineNr]);
		}

		public void RemoveLastOrderline()
		{
			this.Controls.Remove(orderLines[m_intOrderLineNr]);
			--m_intOrderLineNr;
			m_intButtonYPos -= m_intVerticalSpacing;
			m_intOrderLineYPos -= m_intVerticalSpacing;
			m_intFormHeight = m_intButtonYPos + 30;
			this.ChangeButtonYPosition(m_intButtonYPos);
			this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, m_intFormHeight);
		}

		private string GetTradeMark(int intTradeMarkId)
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

		private void btnCancelList_Click(object sender, System.EventArgs e)
		{
			for(int i=m_intOrderLineNr; i>=1; i--)
			{
				this.Controls.Remove(orderLines[i]);
				m_intButtonYPos -= m_intVerticalSpacing;
				m_intFormHeight = m_intButtonYPos + 30;
				this.ChangeButtonYPosition(m_intButtonYPos);
				this.ClientSize = new System.Drawing.Size(this.ClientSize.Width, m_intFormHeight);
			}
			this.ChangeLabelVisibility(false);
			this.btnAddToCart.Visible = false;
			this.btnCancelList.Visible = false;
			this.btnSaveOrder.Visible = false;
			this.cmbSuppliers.SelectedIndex = -1;
			m_intOrderLineNr = 0;
			m_intOrderLineYPos = this.lbxCategories.Location.Y + 276;
			m_intButtonYPos = this.lbxCategories.Location.Y + 310;
			m_intFormHeight = this.Height;

            int intDefaultSupplier = clsConfiguration.General_DefaultSupplierID;
            this.cmbSuppliers.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intDefaultSupplier, m_dtaSuppliers, 0);
		}

		private void btnSaveOrder_Click(object sender, System.EventArgs e)
		{
			DialogResult drResult;

			if(this.cmbSuppliers.SelectedIndex == -1)
			{
				MessageBox.Show("You must select a supplier","Order Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			else
				m_intSupplierId = int.Parse(m_dtaSuppliers.Rows[this.cmbSuppliers.SelectedIndex]["FournisseurId"].ToString());

			if(this.cmbEmployees.SelectedIndex == -1)
			{
				MessageBox.Show("You must select a name!","Order Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			else
				m_intEmployeeId = int.Parse(m_dtaEmployees.Rows[this.cmbEmployees.SelectedIndex]["EmployeeId"].ToString());

			m_strNewOrderNumber = txtOrderId.Text;
			if(m_strNewOrderNumber.Length == 0)
			{
				MessageBox.Show("You must type an order number!","Order Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
				return;
			}
			else
				m_strNewOrderNumber = txtOrderId.Text;

			DateTime	dtToday = DateTime.Now;
			string		format = "MMMM dd, yyyy";
			string		todayDate = dtToday.ToString(format);
			string		m_strOrderDate = dtpDate.Text.ToString();
			if(this.dtpDate.Value.Date == dtToday.Date)
			{
				drResult = MessageBox.Show("Is today the order date?","Attention to the Order Date",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
				if(drResult == DialogResult.No)
					return;
			}

			if((txtTaxes.Text == "0.00") || (txtShippingHandling.Text == "0.00") || (txtDuty.Text == "0.00"))
			{
				drResult = MessageBox.Show("The Taxes, Shipping Handling or Duty can be $0.00?","Attention to the Prices",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
				if(drResult == DialogResult.No)
					return;
			}

			for(int i=1; i<=m_intOrderLineNr; i++)
			{
				if(orderLines[i].UnitPrice == "0.00")
				{
					drResult = MessageBox.Show("The Unit price(s) can be $0.00?","Attention to the Prices",MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation);
					if(drResult == DialogResult.No)
						return;
					else
						break;
				}
			}
//				Payment calculation per Produit
			PaymentCalculation();

			fclsOIAccounting_Pay frmOIAccounting = new fclsOIAccounting_Pay(fclsOIAccounting_Pay.Caller.OldOrders,this,m_strNewOrderNumber,-1,(decimal)flTotal,m_odcConnection);
			frmOIAccounting.ShowDialog();
			if(!m_blnOrderPaid)
				return;

			{
				m_odaSaveNewOrder = new OleDbDataAdapter("Select * From [Orders]", m_odcConnection);
				OleDbCommandBuilder ocbSaveNewOrder = new OleDbCommandBuilder(m_odaSaveNewOrder);
				m_dtaNewOrder = new DataTable("Orders");				
				m_odaSaveNewOrder.Fill(m_dtaNewOrder);

				m_odaSaveNewPayment = new OleDbDataAdapter("Select * From [OrderPayment]", m_odcConnection);
				OleDbCommandBuilder ocbSaveNewPayment = new OleDbCommandBuilder(m_odaSaveNewPayment);
				m_dtaNewPayment = new DataTable();
				m_odaSaveNewPayment.Fill(m_dtaNewPayment);

				m_odaUpdateSubProd = new OleDbDataAdapter("Select * From [SubProducts]", m_odcConnection);
				OleDbCommandBuilder ocbUpdateSubProd = new OleDbCommandBuilder(m_odaUpdateSubProd);
				m_dtaUpdateSubProd = new DataTable();
				m_odaUpdateSubProd.Fill(m_dtaUpdateSubProd);

//					Save the Payment per Product in Subproduct Table and the Payment per Order in Orderpayment Table	
				int m_intSave = saveTaxTranspDuty();
				if(m_intSave == -1)
					return;
				for(int i=1; i <= m_intOrderLineNr; i++)
					this.SaveOrder(i);
				btnCancelList_Click(null, null);
				m_strNewOrderNumber = GetNewOrderNumber();
				txtOrderId.Text = m_strNewOrderNumber;
				dtpDate.Value = DateTime.Today;
				dtpPayment.Value = DateTime.Today;
				this.txtTaxes.Text = "0.00";
				this.txtDuty.Text = "0.00";
				this.txtShippingHandling.Text = "0.00";

                int intDefaultSupplier = clsConfiguration.General_DefaultSupplierID;
                this.cmbSuppliers.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intDefaultSupplier, m_dtaSuppliers, 0);
			}

		//	fclsGENInput.indViewOrder = 0;
		}
		private void PaymentCalculation()
		{
			flTax = flTransport = flDuty = flTotal = flTotalCatalog = 0;
			int	i;
			for(i=1; i<=m_intOrderLineNr; i++)
			{
				flTX[i] = 0.0;
				flTR[i] = 0.0;
				flDU[i] = 0.0;
				flTotalCatalog += double.Parse(orderLines[i].UnitPrice) * double.Parse(orderLines[i].Units);
			}
			strTotalCatalog = flTotalCatalog.ToString();
			strTax = this.txtTaxes.Text;
			strTransport = this.txtShippingHandling.Text;
			strDuty = this.txtDuty.Text;
			flTax = double.Parse(strTax);			
			flTransport = double.Parse(strTransport);			
			flDuty = double.Parse(strDuty);
			flTotal = flTax + flTransport + flDuty;
			if((flTotal > 0.0) && (flTotalCatalog > 0.0))
				for(i=1; i<=m_intOrderLineNr; i++)
				{
					flRaport = (double.Parse(orderLines[i].UnitPrice) * double.Parse(orderLines[i].Units))/flTotalCatalog;
					flTX[i] = flRaport * flTax;
					flTR[i] = flRaport * flTransport;
					flDU[i] = flRaport * flDuty;
				}
			flTotal += flTotalCatalog;
			strTotal = flTotal.ToString();
		}

		private int saveTaxTranspDuty()
		{
			int		  i, m_intSubId;
//															Update Subproduct Table
			for(i=1; i<=m_intOrderLineNr; i++)
			{
				flCatalogPay = double.Parse(orderLines[i].UnitPrice) * double.Parse(orderLines[i].Units);
				flTotalPay = flCatalogPay +flTX[i] + flTR[i] + flDU[i];
				m_intSubId = orderLines[i].SubProductId;
				int m_intRowIndex = GetRowIndex(m_intSubId);
				if(m_intRowIndex == -1)
				{
					MessageBox.Show("Sub Products introuvable: wrong name or it is not in the Database!!","Old Order Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return -1;
				}
				DataRow targetRow = m_dtaUpdateSubProd.Rows[m_intRowIndex];
				targetRow.BeginEdit();
				string m_strPrix = m_dtaUpdateSubProd.Rows[m_intRowIndex]["Prix"].ToString();
				if(m_strPrix == "0")
				{
					targetRow["Prix"] = orderLines[i].UnitPrice;
					targetRow["PrixMin"] = orderLines[i].UnitPrice;
					targetRow["PrixMax"] = orderLines[i].UnitPrice;
				}
				if(double.Parse(m_dtaUpdateSubProd.Rows[m_intRowIndex]["PrixMin"].ToString()) >= double.Parse(orderLines[i].UnitPrice))
				{
					targetRow["PrixMin"] = orderLines[i].UnitPrice;
					targetRow["PrixMinOI"] = m_strNewOrderNumber;
				}
				if(double.Parse(m_dtaUpdateSubProd.Rows[m_intRowIndex]["PrixMax"].ToString()) <= double.Parse(orderLines[i].UnitPrice))
				{
					targetRow["PrixMax"] = orderLines[i].UnitPrice;
					targetRow["PrixMaxOI"] = m_strNewOrderNumber;
				}
				targetRow["PrixOrderId"] = m_strNewOrderNumber;
				targetRow["Qtty"] = double.Parse(orderLines[i].Units) + double.Parse(m_dtaUpdateSubProd.Rows[m_intRowIndex]["Qtty"].ToString());
				targetRow["CatalogPay"] = flCatalogPay + double.Parse(m_dtaUpdateSubProd.Rows[m_intRowIndex]["CatalogPay"].ToString());
				targetRow["Tax"] = flTX[i]+ double.Parse(m_dtaUpdateSubProd.Rows[m_intRowIndex]["Tax"].ToString());
				targetRow["Transport"] = flTR[i]+ double.Parse(m_dtaUpdateSubProd.Rows[m_intRowIndex]["Transport"].ToString());
				targetRow["Duty"] = flDU[i]+ double.Parse(m_dtaUpdateSubProd.Rows[m_intRowIndex]["Duty"].ToString());
				targetRow["TotalPay"] = flTotalPay + double.Parse(m_dtaUpdateSubProd.Rows[m_intRowIndex]["TotalPay"].ToString());
				targetRow.EndEdit();
				try
				{
					m_odaUpdateSubProd.Update(m_dtaUpdateSubProd);
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
			}
//																	Write in OrderPayment Table
			DataRow dtrNewPaymentLine = m_dtaNewPayment.NewRow();
			dtrNewPaymentLine["OrderId"]			= m_strNewOrderNumber;
			dtrNewPaymentLine["PaymentDate"]		= this.dtpPayment.Value.ToShortDateString();
			dtrNewPaymentLine["SubTotal"]			= strTotalCatalog;
			dtrNewPaymentLine["Tax"]				= strTax;
			dtrNewPaymentLine["Transport"]			= strTransport;
			dtrNewPaymentLine["Duty"]				= strDuty;
			dtrNewPaymentLine["TotalPay"]			= strTotal;
			dtrNewPaymentLine["checkPayment"]		= "1";
			dtrNewPaymentLine["SumDue"]				= "0.00";
			dtrNewPaymentLine["Penalty"]			= "0.00";
			dtrNewPaymentLine["PayedPer"]			= m_strPayedPer;
			dtrNewPaymentLine["PayedBy"]			= m_intEmployeeId;
			//Add the new row to the table
			m_dtaNewPayment.Rows.Add(dtrNewPaymentLine);	
			//							update the Database
			try
			{
				m_odaSaveNewPayment.Update(m_dtaNewPayment);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			return 1;
		}
		private int GetRowIndex(int m_intSubId)
		{
			int j, m_intSubPrIndex;

			for(j=0; j<m_dtaUpdateSubProd.Rows.Count; j++)
			{
				m_intSubPrIndex = int.Parse(m_dtaUpdateSubProd.Rows[j]["SubPrId"].ToString());
				if(m_intSubId == m_intSubPrIndex)
					return j;
			}
			return -1;
		}

		private void SaveOrder(int intOrderLine)
		{
			DataRow dtrNewOrderLine = m_dtaNewOrder.NewRow();
			dtrNewOrderLine["OrderId"]			= m_strNewOrderNumber;
			dtrNewOrderLine["OrderDate"]		= this.dtpDate.Value.ToShortDateString();
			dtrNewOrderLine["MatId"]			= orderLines[intOrderLine].ProductId;
			dtrNewOrderLine["SubPrId"]			= orderLines[intOrderLine].SubProductId;
			dtrNewOrderLine["MarComId"]			= orderLines[intOrderLine].TradeMarkId;
			dtrNewOrderLine["FournisseurId"]	= m_intSupplierId.ToString();
			dtrNewOrderLine["EmployeeId"]		= m_intEmployeeId.ToString();
			dtrNewOrderLine["OrderQty"]			= orderLines[intOrderLine].Units;
			dtrNewOrderLine["Pack"]				= orderLines[intOrderLine].Packaging;
			dtrNewOrderLine["CategoryId"]		= orderLines[intOrderLine].CategoryId;
			dtrNewOrderLine["Prix"]				= orderLines[intOrderLine].UnitPrice;
			dtrNewOrderLine["Checked"]			= "1";
			dtrNewOrderLine["CheckedBy"]		= m_intEmployeeId;
			dtrNewOrderLine["BackOrderUnits"]	= 0;
			dtrNewOrderLine["CanceledBOUnits"]	= 0;
			dtrNewOrderLine["ReturnUnits"]		= 0;
			double flCatPay = double.Parse(orderLines[intOrderLine].UnitPrice) * double.Parse(orderLines[intOrderLine].Units);
			dtrNewOrderLine["CatalogPay"]		= flCatPay;
			dtrNewOrderLine["Tax"]				= flTX[intOrderLine];
			dtrNewOrderLine["Transport"]		= flTR[intOrderLine];
			dtrNewOrderLine["Duty"]				= flDU[intOrderLine];
			double flSuplim = flTX[intOrderLine] + flTR[intOrderLine] + flDU[intOrderLine];
			dtrNewOrderLine["TotalPay"]			= flCatPay + flSuplim;
			
			//Add the new row to the table
			m_dtaNewOrder.Rows.Add(dtrNewOrderLine);	
			//							update the Database
			try
			{
				m_odaSaveNewOrder.Update(m_dtaNewOrder);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
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
			this.lblProduct.Visible = blnVisible;
			this.lblTrademark.Visible = blnVisible;
			this.lblPackaging.Visible = blnVisible;
			this.lblUnits.Visible = blnVisible;
			this.lblDuty.Visible = blnVisible;
			this.lblShippingHandling.Visible = blnVisible;
			this.lblTaxes.Visible = blnVisible;
			this.txtDuty.Visible = blnVisible;
			this.txtShippingHandling.Visible = blnVisible;
			this.txtTaxes.Visible = blnVisible;
		}
	
		// Changes the vertical position of the buttons (except Add to Cart)
		private void ChangeButtonYPosition(int intButtonYPos)
		{
			this.btnClose.Location = new System.Drawing.Point(this.btnClose.Location.X, intButtonYPos);
			this.btnHelp.Location = new System.Drawing.Point(this.btnHelp.Location.X, intButtonYPos);
			this.btnCancelList.Location = new System.Drawing.Point(this.btnCancelList.Location.X, intButtonYPos);
			this.btnSaveOrder.Location = new System.Drawing.Point(this.btnSaveOrder.Location.X, intButtonYPos);
			this.lblDuty.Location = new System.Drawing.Point(this.lblDuty.Location.X, intButtonYPos);
			this.lblShippingHandling.Location = new System.Drawing.Point(this.lblShippingHandling.Location.X, intButtonYPos);
			this.lblTaxes.Location = new System.Drawing.Point(this.lblTaxes.Location.X, intButtonYPos);
			this.txtDuty.Location = new System.Drawing.Point(this.txtDuty.Location.X, intButtonYPos);
			this.txtShippingHandling.Location = new System.Drawing.Point(this.txtShippingHandling.Location.X, intButtonYPos);
			this.txtTaxes.Location = new System.Drawing.Point(this.txtTaxes.Location.X, intButtonYPos);
		}
	
		// True if order sent, false if not
		public void SetOrderStatus(bool blnOrderSent)
		{
			m_blnOrderSent = blnOrderSent;
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

		/// <summary>
		///		Function called by fclsOI_Accounting_Pay in order to return payment information for the current order.
		/// </summary>
		public void SetPaymentInformation(bool blnOrderPaid, DateTime dtPaymentDate, string strAmoundPaid, string strPenalty, string strPaymentMethod, int intPayerEmployeeId)
		{
			m_blnOrderPaid = blnOrderPaid;
			m_dtPaymentDate = dtPaymentDate;
			m_strPayedSum = strAmoundPaid;
			m_strPayedPenalty = strPenalty;
			m_strPayedPer = strPaymentMethod;
			m_intPayedBy = intPayerEmployeeId;
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

		private void mnuAdd_Click(object sender, System.EventArgs e)
		{
			string strResponse = "";

			switch(m_intCurrentListBox)
			{
				case 1:
					strResponse = InputBox.ShowInputBox("Please enter the name of the new category:","Add New Category");
                    if (strResponse != null && strResponse.Length > 0)
					{
						int n_nrCategory = m_dtaCategories.Rows.Count;
						if(!checkName(strResponse, n_nrCategory, m_dtaCategories, "Category"))
							return;
						m_intMaxCategoryKey++;
						DataRow	dtrNewRow = m_dtaCategories.NewRow();
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
							MessageBox.Show(ex.Message);
						}
					}
					break;

				case 2:
					strResponse = InputBox.ShowInputBox("Please enter the name of the new product:","Add New Product");
                    if (strResponse != null && strResponse.Length > 0)
					{
						int n_nrProduct = m_dtaProducts.Rows.Count;
						if(!checkName(strResponse, n_nrProduct, m_dtaProducts, "Product"))
							return;
						m_intMaxProductKey++;
						DataRow	dtrNewRow = m_dtaProducts.NewRow();
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
							MessageBox.Show(ex.Message);
						}
					}
					break;

				case 3:
					fclsDMModifyProduct_SubProd frmSubProduct = new fclsDMModifyProduct_SubProd(m_odcConnection,
																								m_dtaSubProducts,
																								DSMS.fclsDMModifyProduct_SubProd.WindowPurpose.Add);
					if(frmSubProduct.ShowDialog() == DialogResult.OK)
					{
						object[] objSubProductData = frmSubProduct.GetSubProductData();

						DataRow	dtrNewRow = m_dtaSubProducts.NewRow();
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
						dtrNewRow["PrixMinOi"]	= "0";
						dtrNewRow["PrixOrderId"]= "0";
						dtrNewRow["PrixMaxOi"]	= "0";
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
							MessageBox.Show(ex.Message);
						}	
					}

					// refresh trademarks datatable (could've been modified while in fclsDMModifyProduct_SubProd
					this.LoadTradeMarks();
				break;
			}
		}

		private void ctmRightclick_Popup(object sender, System.EventArgs e)
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

		private void mnuRemove_Click(object sender, System.EventArgs e)
		{
			string strName;
			switch(m_intCurrentListBox)
			{
				case 1:
					strName = m_dtaCategories.Rows[lbxCategories.SelectedIndex]["CategName"].ToString();
					if(MessageBox.Show("Are you sure you want to remove the '" + strName + "' Category?","Remove Category",
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
							MessageBox.Show(ex.Message);
						}
					}
					break;

				case 2:
//					if(this.lbxSubProducts.Items.Count > 0)
					{
					if(MessageBox.Show("Are you sure you want to delete this product and all its associated sub-products?","Delete Product",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
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
							MessageBox.Show(ex.Message);
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
							MessageBox.Show(ex.Message);
						}
					}
				}
					break;
				
				case 3:
					strName = m_dtaSubProducts.Rows[lbxSubProducts.SelectedIndex]["MatName"].ToString();
					if(MessageBox.Show("Are you sure you want to remove the '" + strName + "' Sub-Product?","Remove Sub-Product",
						MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
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
							MessageBox.Show(ex.Message);
						}
					}
				break;
			}

		}

		private void mnuEdit_Click(object sender, System.EventArgs e)
		{
			string strResponse = "";

			switch(m_intCurrentListBox)
			{
				case 1:
					strResponse = InputBox.ShowInputBox("Please change the name of the category:","Edit Category",this.lbxCategories.SelectedItem.ToString());
                    if (strResponse != null && strResponse.Length > 0)
					{
						int n_nrCategory = m_dtaCategories.Rows.Count;
						if(!checkName(strResponse, n_nrCategory, m_dtaCategories, "Category"))
							return;
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
							MessageBox.Show(ex.Message);
						}
					}
				break;

				case 2:
					strResponse = InputBox.ShowInputBox("Please change the name of the product:","Edit Product",this.lbxProducts.SelectedItem.ToString());
                    if (strResponse != null && strResponse.Length > 0)
					{
						int n_nrProduct = m_dtaProducts.Rows.Count;
						if(!checkName(strResponse, n_nrProduct, m_dtaProducts, "Product"))
							return;
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
							MessageBox.Show(ex.Message);
						}
					}
				break;
				
				case 3:
					
					object[] objSubProductData = new object[4];
					objSubProductData[0] = m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["MatName"].ToString();
					objSubProductData[1] = m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["MarComId"].ToString();
					objSubProductData[2] = m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["Pack"].ToString();
					objSubProductData[3] = m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["Reorder"].ToString();

					fclsDMModifyProduct_SubProd frmSubProduct = new fclsDMModifyProduct_SubProd(m_odcConnection,
																								m_dtaSubProducts,
																								DSMS.fclsDMModifyProduct_SubProd.WindowPurpose.Modify);
					if(frmSubProduct.ShowDialog() == DialogResult.OK)
					{
						objSubProductData = frmSubProduct.GetSubProductData();
						m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["MatName"]	 = (string) objSubProductData[0];
						m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["MarComId"] = (int) objSubProductData[1];
						m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["Pack"]	 = (string) objSubProductData[2];
						m_dtaSubProducts.Rows[this.lbxSubProducts.SelectedIndex]["Reorder"]	 = (int) objSubProductData[3];

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
							MessageBox.Show(ex.Message);
						}
					}

					// refresh trademarks datatable (could've been modified while in fclsDMModifyProduct_SubProd
					this.LoadTradeMarks();
				break;
			}
		
		}

		private void LoadTradeMarks()
		{
			// Open the table Trademarks
			OleDbDataAdapter odaTrademarks = new OleDbDataAdapter("SELECT * FROM [Trademarks] ORDER BY Trademark", m_odcConnection);
			m_dtaTrademarks = new DataTable("Trademarks");
			try
			{
				odaTrademarks.Fill(m_dtaTrademarks);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			m_intNrTrademarks = m_dtaTrademarks.Rows.Count;
		}

		private void txtTaxes_Leave(object sender, System.EventArgs e)
		{
			if(this.txtTaxes.Text.Length == 0)
			{
				this.txtTaxes.Text = "0.00";
			}
		}

		private void txtShippingHandling_Leave(object sender, System.EventArgs e)
		{
			if(this.txtShippingHandling.Text.Length == 0)
			{
				this.txtShippingHandling.Text = "0.00";
			}
		}

		private void txtDuty_Leave(object sender, System.EventArgs e)
		{
			if(this.txtDuty.Text.Length == 0.00)
			{
				this.txtDuty.Text = "0.00";
			}
		}

		private void txtTaxes_TextChanged(object sender, System.EventArgs e)
		{
			if(this.txtTaxes.Text.Length > 0)
			{
				this.txtTaxes.Text = clsUtilities.ValidateCurrency(this.txtTaxes.Text);
			}
			
			this.txtTaxes.Select(this.txtTaxes.Text.Length,0);
		}

		private void txtShippingHandling_TextChanged(object sender, System.EventArgs e)
		{
			if(this.txtShippingHandling.Text.Length > 0)
			{
				this.txtShippingHandling.Text = clsUtilities.ValidateCurrency(this.txtShippingHandling.Text);
			}
		
			this.txtShippingHandling.Select(this.txtShippingHandling.Text.Length,0);
		}

		private void txtDuty_TextChanged(object sender, System.EventArgs e)
		{
			if(this.txtDuty.Text.Length > 0)
			{
				this.txtDuty.Text = clsUtilities.ValidateCurrency(this.txtDuty.Text);
			}
		
			this.txtDuty.Select(this.txtDuty.Text.Length,0);
		}
		private bool checkName(string strResponse, int nrCheck, DataTable m_dtaCheck, string strCase)
		{
			string strName;
			string strColumn = "MatName";
			if (strCase == "Category")
				strColumn = "CategName";
			string msgText = strCase;
			msgText += " Name Error";
			for(int i=0; i<nrCheck; i++)
			{
				strName = m_dtaCheck.Rows[i][strColumn].ToString();
				if(strName == strResponse)
				{
					MessageBox.Show("This name is already in the database!\n" +
						"You must change the name!",msgText);
					return false;
				}
			}
			return true;
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","PastInvoices.htm");  //

		}
	}
}