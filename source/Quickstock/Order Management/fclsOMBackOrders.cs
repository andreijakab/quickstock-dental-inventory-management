using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for frmReturnedProducts.
	/// </summary>
	public class fclsOMBackOrders : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Label lblPastOrderDate;
		private System.Windows.Forms.Label lblPastOrderNumber;
		private System.Windows.Forms.Label lblLastOrderDate;
		private System.Windows.Forms.Label lblLastOrderNumber;
		private System.Windows.Forms.Label lblLastOrder;
		private System.Windows.Forms.Label lblFirstOrderDate;
		private System.Windows.Forms.Label lblFirstOrderNumber;
		private System.Windows.Forms.Label lblFirstOrder;
        private System.Windows.Forms.GroupBox gpbPastOrders;
		private System.Windows.Forms.ContextMenu ctmListview;
		private System.Windows.Forms.MenuItem mnuUpdateProductInfo;
        private System.Windows.Forms.MenuItem mnuCancelBackorderedProduct;
        private GroupBox gpbSelectedOrder;
        private Label blbHelp;
        private ListView lstViewOrders;
        private ColumnHeader updateDate;
        private ColumnHeader prodName;
        private ColumnHeader subProdName;
        private ColumnHeader marCom;
        private ColumnHeader price;
        private ColumnHeader backorder;
        private ColumnHeader pack;
        private SplitContainer scOrderCommands;
        private PriceTextBox.PriceTextBox txtShippingHandling;
        private PriceTextBox.PriceTextBox txtDuty;
        private PriceTextBox.PriceTextBox txtTaxes;
        private Label lblOrderDate_Data;
        private Label lblOrderDate;
        private Label lblDuty;
        private Label lblShippingHandling;
        private Label lblTaxes;
        private Button btnSaveChanges;
        private GroupBox gpbOrderSearchCriteria;
        private OrderQuery.OrderQuery oqOrderSearch;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		/*private DataSet				dataSet;
		private int					nOrd = 0, nrTotProd = 0, nrTotBO = 0, nrUpdatedProd = 0;
		private static int			nrOrder = 50;
		private static int			m_intModify;						// 1  Modify Backorder	2  Cancel Backorder
		private static string		m_strOrderSelect, m_strSuplId, m_strSubPrId;
		private string				orderId, subPrId, subPrName, productName, prodId, strLast2Digit;
		private static string		m_strNewPrice, m_strNewQty, m_strOldQty, m_strEmployee, m_strReceived;
		public static int			m_intCheckPayment = 0, m_intOldYear;
		
		OleDbDataAdapter			orderDataAdapter, m_odaAllOrders, m_odaOneOrder, m_odaDeleteOrder, m_odaCancelOrder;
		DataTable					m_dtOrder, m_dtAllOrders, m_dtOneOrder, m_dtDeleteOrder, m_dtCancelOrder;
		OleDbDataAdapter			m_odaSubProduct;
		DataTable					m_dtaSubProduct;
		DataRow						m_drOrder;

		public double m_dblSubTotal = 0, m_dblTax = 0.0, m_dblTransport = 0.0, m_dblDuty = 0.0;
		public string strTax, strTransport, strDuty, strTotal, strTotalCatalog;
		public double flTax, flTransport, flDuty, flTotal, flTotalPay;
		public double flRaport = 1.0, flTotalCatalog = 0.0, flCatalogPay;
		public static float [] fltUnits	= new float [15];
		public static double [] flPrice = new double [15];
		public double [] flTX = new double [15];
		public double [] flTR = new double [15];
		public double [] flDU = new double [15];
		public int [] intModBO = new int [15];*/
		
		
		
        private enum UtilityForms : int { UpdateBackorder, CancelBackorder }
        
		// checked variables start here
        private bool                        m_blnOrderPaid, m_blnOrderFinishedLoading, m_blnReadOnly;
		private clsListViewColumnSorter		m_lvwColumnSorter;
		private decimal						m_decAmountPaid, m_decPenalty;
		private DataTable					m_dtaEmployees, m_dtaOrderProducts, m_dtaSuppliers;
		private	DateTime					m_dtPaymentDate;
        private int                         m_intEmployeedID_Cancel, m_intEmployeedID_Change, m_intEmployeedID_Payment;
		private OleDbConnection				m_odcConnection;
        private string                      m_strApplicationTitle;
		private string						m_strPayedPer;
		private string						m_strDecimalSeparator, m_strGroupSeparator;
        private string                      m_strQueriedOrderNumber;
		private SupplierInformation			m_siSupplier;
        private ToolTip                     m_ListViewToolTip;

		public fclsOMBackOrders(string strQueriedOrderNumber, OleDbConnection odcConnection)
		{
			NumberFormatInfo nfiNumberFormat;

			InitializeComponent();
			
			// initialize global variables
			m_lvwColumnSorter = new clsListViewColumnSorter();
			m_odcConnection = odcConnection;

            m_blnReadOnly = m_blnOrderPaid = false;
			m_decAmountPaid = m_decPenalty = -1.0M;
            m_intEmployeedID_Cancel = m_intEmployeedID_Change = m_intEmployeedID_Payment = -1;
			m_strQueriedOrderNumber = strQueriedOrderNumber;
			m_strPayedPer = "";

			// configure listview (sets the listview control's sorter and currency symbol)
			this.lstViewOrders.ListViewItemSorter = m_lvwColumnSorter;
			nfiNumberFormat = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
			this.lstViewOrders.Columns[4].Text += "(" + nfiNumberFormat.CurrencySymbol + ")";

			// Get local number formatting information
			m_strDecimalSeparator = nfiNumberFormat.CurrencyDecimalSeparator;
			m_strGroupSeparator = nfiNumberFormat.CurrencyGroupSeparator;

            // get application title
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Reflection.AssemblyProductAttribute apaProductTitle = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyProductAttribute), false)[0] as System.Reflection.AssemblyProductAttribute;
            m_strApplicationTitle = apaProductTitle.Product;
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
            this.cmdClose = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.lblPastOrderDate = new System.Windows.Forms.Label();
            this.lblPastOrderNumber = new System.Windows.Forms.Label();
            this.lblLastOrderDate = new System.Windows.Forms.Label();
            this.lblLastOrderNumber = new System.Windows.Forms.Label();
            this.lblLastOrder = new System.Windows.Forms.Label();
            this.lblFirstOrderDate = new System.Windows.Forms.Label();
            this.lblFirstOrderNumber = new System.Windows.Forms.Label();
            this.lblFirstOrder = new System.Windows.Forms.Label();
            this.gpbPastOrders = new System.Windows.Forms.GroupBox();
            this.ctmListview = new System.Windows.Forms.ContextMenu();
            this.mnuUpdateProductInfo = new System.Windows.Forms.MenuItem();
            this.mnuCancelBackorderedProduct = new System.Windows.Forms.MenuItem();
            this.gpbOrderSearchCriteria = new System.Windows.Forms.GroupBox();
            this.oqOrderSearch = new OrderQuery.OrderQuery();
            this.gpbSelectedOrder = new System.Windows.Forms.GroupBox();
            this.scOrderCommands = new System.Windows.Forms.SplitContainer();
            this.txtShippingHandling = new PriceTextBox.PriceTextBox();
            this.txtDuty = new PriceTextBox.PriceTextBox();
            this.txtTaxes = new PriceTextBox.PriceTextBox();
            this.lblOrderDate_Data = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblDuty = new System.Windows.Forms.Label();
            this.lblShippingHandling = new System.Windows.Forms.Label();
            this.lblTaxes = new System.Windows.Forms.Label();
            this.btnSaveChanges = new System.Windows.Forms.Button();
            this.blbHelp = new System.Windows.Forms.Label();
            this.lstViewOrders = new System.Windows.Forms.ListView();
            this.updateDate = new System.Windows.Forms.ColumnHeader();
            this.prodName = new System.Windows.Forms.ColumnHeader();
            this.subProdName = new System.Windows.Forms.ColumnHeader();
            this.marCom = new System.Windows.Forms.ColumnHeader();
            this.price = new System.Windows.Forms.ColumnHeader();
            this.backorder = new System.Windows.Forms.ColumnHeader();
            this.pack = new System.Windows.Forms.ColumnHeader();
            this.gpbPastOrders.SuspendLayout();
            this.gpbOrderSearchCriteria.SuspendLayout();
            this.gpbSelectedOrder.SuspendLayout();
            this.scOrderCommands.Panel1.SuspendLayout();
            this.scOrderCommands.Panel2.SuspendLayout();
            this.scOrderCommands.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(784, 521);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(96, 32);
            this.cmdClose.TabIndex = 16;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(888, 521);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(96, 32);
            this.btnHelp.TabIndex = 26;
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // lblPastOrderDate
            // 
            this.lblPastOrderDate.AutoSize = true;
            this.lblPastOrderDate.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPastOrderDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblPastOrderDate.Location = new System.Drawing.Point(18, 88);
            this.lblPastOrderDate.Name = "lblPastOrderDate";
            this.lblPastOrderDate.Size = new System.Drawing.Size(37, 17);
            this.lblPastOrderDate.TabIndex = 20;
            this.lblPastOrderDate.Text = "Date";
            this.lblPastOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblPastOrderNumber
            // 
            this.lblPastOrderNumber.AutoSize = true;
            this.lblPastOrderNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPastOrderNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblPastOrderNumber.Location = new System.Drawing.Point(8, 56);
            this.lblPastOrderNumber.Name = "lblPastOrderNumber";
            this.lblPastOrderNumber.Size = new System.Drawing.Size(57, 17);
            this.lblPastOrderNumber.TabIndex = 19;
            this.lblPastOrderNumber.Text = "Number";
            this.lblPastOrderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLastOrderDate
            // 
            this.lblLastOrderDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLastOrderDate.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastOrderDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLastOrderDate.Location = new System.Drawing.Point(168, 88);
            this.lblLastOrderDate.Name = "lblLastOrderDate";
            this.lblLastOrderDate.Size = new System.Drawing.Size(96, 23);
            this.lblLastOrderDate.TabIndex = 18;
            this.lblLastOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLastOrderNumber
            // 
            this.lblLastOrderNumber.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblLastOrderNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLastOrderNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastOrderNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblLastOrderNumber.Location = new System.Drawing.Point(168, 56);
            this.lblLastOrderNumber.Name = "lblLastOrderNumber";
            this.lblLastOrderNumber.Size = new System.Drawing.Size(96, 23);
            this.lblLastOrderNumber.TabIndex = 17;
            this.lblLastOrderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblLastOrder
            // 
            this.lblLastOrder.AutoSize = true;
            this.lblLastOrder.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLastOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblLastOrder.Location = new System.Drawing.Point(176, 24);
            this.lblLastOrder.Name = "lblLastOrder";
            this.lblLastOrder.Size = new System.Drawing.Size(72, 17);
            this.lblLastOrder.TabIndex = 16;
            this.lblLastOrder.Text = "Last Order";
            this.lblLastOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFirstOrderDate
            // 
            this.lblFirstOrderDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFirstOrderDate.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstOrderDate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFirstOrderDate.Location = new System.Drawing.Point(64, 88);
            this.lblFirstOrderDate.Name = "lblFirstOrderDate";
            this.lblFirstOrderDate.Size = new System.Drawing.Size(96, 23);
            this.lblFirstOrderDate.TabIndex = 13;
            this.lblFirstOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFirstOrderNumber
            // 
            this.lblFirstOrderNumber.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.lblFirstOrderNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblFirstOrderNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstOrderNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblFirstOrderNumber.Location = new System.Drawing.Point(64, 56);
            this.lblFirstOrderNumber.Name = "lblFirstOrderNumber";
            this.lblFirstOrderNumber.Size = new System.Drawing.Size(96, 23);
            this.lblFirstOrderNumber.TabIndex = 12;
            this.lblFirstOrderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblFirstOrder
            // 
            this.lblFirstOrder.AutoSize = true;
            this.lblFirstOrder.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFirstOrder.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblFirstOrder.Location = new System.Drawing.Point(78, 24);
            this.lblFirstOrder.Name = "lblFirstOrder";
            this.lblFirstOrder.Size = new System.Drawing.Size(72, 17);
            this.lblFirstOrder.TabIndex = 10;
            this.lblFirstOrder.Text = "First Order";
            this.lblFirstOrder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // gpbPastOrders
            // 
            this.gpbPastOrders.Controls.Add(this.lblPastOrderDate);
            this.gpbPastOrders.Controls.Add(this.lblPastOrderNumber);
            this.gpbPastOrders.Controls.Add(this.lblLastOrderDate);
            this.gpbPastOrders.Controls.Add(this.lblLastOrderNumber);
            this.gpbPastOrders.Controls.Add(this.lblLastOrder);
            this.gpbPastOrders.Controls.Add(this.lblFirstOrderDate);
            this.gpbPastOrders.Controls.Add(this.lblFirstOrderNumber);
            this.gpbPastOrders.Controls.Add(this.lblFirstOrder);
            this.gpbPastOrders.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbPastOrders.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gpbPastOrders.Location = new System.Drawing.Point(305, 521);
            this.gpbPastOrders.Name = "gpbPastOrders";
            this.gpbPastOrders.Size = new System.Drawing.Size(272, 120);
            this.gpbPastOrders.TabIndex = 12;
            this.gpbPastOrders.TabStop = false;
            this.gpbPastOrders.Text = "Backorder Update for the Orders";
            this.gpbPastOrders.Visible = false;
            // 
            // ctmListview
            // 
            this.ctmListview.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuUpdateProductInfo,
            this.mnuCancelBackorderedProduct});
            this.ctmListview.Popup += new System.EventHandler(this.ctmListview_Popup);
            // 
            // mnuUpdateProductInfo
            // 
            this.mnuUpdateProductInfo.Index = 0;
            this.mnuUpdateProductInfo.Text = "&Update Product(s) Information";
            this.mnuUpdateProductInfo.Click += new System.EventHandler(this.mnuUpdateProductInfo_Click);
            // 
            // mnuCancelBackorderedProduct
            // 
            this.mnuCancelBackorderedProduct.Index = 1;
            this.mnuCancelBackorderedProduct.Text = "&Cancel Backordered Product(s)";
            this.mnuCancelBackorderedProduct.Click += new System.EventHandler(this.mnuCancelBackorderedProduct_Click);
            // 
            // gpbOrderSearchCriteria
            // 
            this.gpbOrderSearchCriteria.Controls.Add(this.oqOrderSearch);
            this.gpbOrderSearchCriteria.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpbOrderSearchCriteria.Location = new System.Drawing.Point(55, 5);
            this.gpbOrderSearchCriteria.Name = "gpbOrderSearchCriteria";
            this.gpbOrderSearchCriteria.Size = new System.Drawing.Size(882, 128);
            this.gpbOrderSearchCriteria.TabIndex = 34;
            this.gpbOrderSearchCriteria.TabStop = false;
            this.gpbOrderSearchCriteria.Text = "Order Search Criteria";
            // 
            // oqOrderSearch
            // 
            this.oqOrderSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.oqOrderSearch.Location = new System.Drawing.Point(13, 25);
            this.oqOrderSearch.Name = "oqOrderSearch";
            this.oqOrderSearch.Size = new System.Drawing.Size(857, 99);
            this.oqOrderSearch.TabIndex = 34;
            this.oqOrderSearch.OnNewSelectedOrderNumber += new OrderQuery.OrderQuery.NewSelectedOrderNumberHandler(this.oqOrderSearch_OnNewSelectedOrderNumber);
            this.oqOrderSearch.OnNoOrdersFound += new OrderQuery.OrderQuery.NoOrdersFound(this.oqOrderSearch_OnNoOrdersFound);
            // 
            // gpbSelectedOrder
            // 
            this.gpbSelectedOrder.Controls.Add(this.scOrderCommands);
            this.gpbSelectedOrder.Controls.Add(this.blbHelp);
            this.gpbSelectedOrder.Controls.Add(this.lstViewOrders);
            this.gpbSelectedOrder.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpbSelectedOrder.Location = new System.Drawing.Point(8, 139);
            this.gpbSelectedOrder.Name = "gpbSelectedOrder";
            this.gpbSelectedOrder.Size = new System.Drawing.Size(976, 376);
            this.gpbSelectedOrder.TabIndex = 35;
            this.gpbSelectedOrder.TabStop = false;
            this.gpbSelectedOrder.Text = "Selected Order";
            // 
            // scOrderCommands
            // 
            this.scOrderCommands.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scOrderCommands.Location = new System.Drawing.Point(6, 270);
            this.scOrderCommands.Name = "scOrderCommands";
            this.scOrderCommands.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // scOrderCommands.Panel1
            // 
            this.scOrderCommands.Panel1.Controls.Add(this.txtShippingHandling);
            this.scOrderCommands.Panel1.Controls.Add(this.txtDuty);
            this.scOrderCommands.Panel1.Controls.Add(this.txtTaxes);
            this.scOrderCommands.Panel1.Controls.Add(this.lblOrderDate_Data);
            this.scOrderCommands.Panel1.Controls.Add(this.lblOrderDate);
            this.scOrderCommands.Panel1.Controls.Add(this.lblDuty);
            this.scOrderCommands.Panel1.Controls.Add(this.lblShippingHandling);
            this.scOrderCommands.Panel1.Controls.Add(this.lblTaxes);
            // 
            // scOrderCommands.Panel2
            // 
            this.scOrderCommands.Panel2.Controls.Add(this.btnSaveChanges);
            this.scOrderCommands.Size = new System.Drawing.Size(964, 100);
            this.scOrderCommands.TabIndex = 33;
            // 
            // txtShippingHandling
            // 
            this.txtShippingHandling.Enabled = false;
            this.txtShippingHandling.Location = new System.Drawing.Point(774, 12);
            this.txtShippingHandling.Name = "txtShippingHandling";
            this.txtShippingHandling.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtShippingHandling.Size = new System.Drawing.Size(60, 22);
            this.txtShippingHandling.TabIndex = 47;
            this.txtShippingHandling.Text = "0,00 ";
            this.txtShippingHandling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtShippingHandling.TextChanged += new System.EventHandler(this.txtShippingHandling_TextChanged);
            // 
            // txtDuty
            // 
            this.txtDuty.Enabled = false;
            this.txtDuty.Location = new System.Drawing.Point(896, 12);
            this.txtDuty.Name = "txtDuty";
            this.txtDuty.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDuty.Size = new System.Drawing.Size(60, 22);
            this.txtDuty.TabIndex = 46;
            this.txtDuty.Text = "0,00 ";
            this.txtDuty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDuty.TextChanged += new System.EventHandler(this.txtDuty_TextChanged);
            // 
            // txtTaxes
            // 
            this.txtTaxes.Enabled = false;
            this.txtTaxes.Location = new System.Drawing.Point(530, 12);
            this.txtTaxes.Name = "txtTaxes";
            this.txtTaxes.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTaxes.Size = new System.Drawing.Size(60, 22);
            this.txtTaxes.TabIndex = 45;
            this.txtTaxes.Text = "0,00 ";
            this.txtTaxes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTaxes.TextChanged += new System.EventHandler(this.txtTaxes_TextChanged);
            // 
            // lblOrderDate_Data
            // 
            this.lblOrderDate_Data.BackColor = System.Drawing.Color.White;
            this.lblOrderDate_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOrderDate_Data.Enabled = false;
            this.lblOrderDate_Data.Location = new System.Drawing.Point(86, 11);
            this.lblOrderDate_Data.Name = "lblOrderDate_Data";
            this.lblOrderDate_Data.Size = new System.Drawing.Size(132, 24);
            this.lblOrderDate_Data.TabIndex = 44;
            this.lblOrderDate_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Location = new System.Drawing.Point(6, 15);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(77, 16);
            this.lblOrderDate.TabIndex = 43;
            this.lblOrderDate.Text = "Order Date";
            // 
            // lblDuty
            // 
            this.lblDuty.AutoSize = true;
            this.lblDuty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblDuty.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblDuty.Location = new System.Drawing.Point(854, 15);
            this.lblDuty.Name = "lblDuty";
            this.lblDuty.Size = new System.Drawing.Size(36, 16);
            this.lblDuty.TabIndex = 42;
            this.lblDuty.Text = "Duty";
            this.lblDuty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblShippingHandling
            // 
            this.lblShippingHandling.AutoSize = true;
            this.lblShippingHandling.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblShippingHandling.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblShippingHandling.Location = new System.Drawing.Point(614, 15);
            this.lblShippingHandling.Name = "lblShippingHandling";
            this.lblShippingHandling.Size = new System.Drawing.Size(154, 16);
            this.lblShippingHandling.TabIndex = 41;
            this.lblShippingHandling.Text = "Shipping and Handling";
            this.lblShippingHandling.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTaxes
            // 
            this.lblTaxes.AutoSize = true;
            this.lblTaxes.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTaxes.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTaxes.Location = new System.Drawing.Point(478, 15);
            this.lblTaxes.Name = "lblTaxes";
            this.lblTaxes.Size = new System.Drawing.Size(46, 16);
            this.lblTaxes.TabIndex = 40;
            this.lblTaxes.Text = "Taxes";
            this.lblTaxes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnSaveChanges
            // 
            this.btnSaveChanges.Enabled = false;
            this.btnSaveChanges.Location = new System.Drawing.Point(825, 11);
            this.btnSaveChanges.Name = "btnSaveChanges";
            this.btnSaveChanges.Size = new System.Drawing.Size(136, 24);
            this.btnSaveChanges.TabIndex = 26;
            this.btnSaveChanges.Text = "Save Changes";
            this.btnSaveChanges.Click += new System.EventHandler(this.btnSaveChanges_Click);
            // 
            // blbHelp
            // 
            this.blbHelp.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.blbHelp.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.blbHelp.Location = new System.Drawing.Point(6, 18);
            this.blbHelp.Name = "blbHelp";
            this.blbHelp.Size = new System.Drawing.Size(656, 16);
            this.blbHelp.TabIndex = 32;
            this.blbHelp.Text = "Click on the line of a Product in order to Update the Backorder and the Product U" +
                "nit Price.";
            this.blbHelp.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstViewOrders
            // 
            this.lstViewOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.updateDate,
            this.prodName,
            this.subProdName,
            this.marCom,
            this.price,
            this.backorder,
            this.pack});
            this.lstViewOrders.ContextMenu = this.ctmListview;
            this.lstViewOrders.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.lstViewOrders.Enabled = false;
            this.lstViewOrders.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.lstViewOrders.FullRowSelect = true;
            this.lstViewOrders.Location = new System.Drawing.Point(6, 42);
            this.lstViewOrders.Name = "lstViewOrders";
            this.lstViewOrders.Size = new System.Drawing.Size(964, 224);
            this.lstViewOrders.TabIndex = 31;
            this.lstViewOrders.UseCompatibleStateImageBehavior = false;
            this.lstViewOrders.View = System.Windows.Forms.View.Details;
            this.lstViewOrders.DoubleClick += new System.EventHandler(this.lstViewOrders_DoubleClick);
            this.lstViewOrders.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstViewOrders_ColumnClick);
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
            // price
            // 
            this.price.Text = "Unit Price";
            this.price.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.price.Width = 71;
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
            // fclsOMBackOrders
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(992, 559);
            this.Controls.Add(this.gpbSelectedOrder);
            this.Controls.Add(this.gpbOrderSearchCriteria);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.gpbPastOrders);
            this.MaximizeBox = false;
            this.Name = "fclsOMBackOrders";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Stock - Backorders";
            this.Load += new System.EventHandler(this.frmOMBackOrders_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fclsOMBackOrders_FormClosing);
            this.gpbPastOrders.ResumeLayout(false);
            this.gpbPastOrders.PerformLayout();
            this.gpbOrderSearchCriteria.ResumeLayout(false);
            this.gpbSelectedOrder.ResumeLayout(false);
            this.scOrderCommands.Panel1.ResumeLayout(false);
            this.scOrderCommands.Panel1.PerformLayout();
            this.scOrderCommands.Panel2.ResumeLayout(false);
            this.scOrderCommands.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		#region Events
		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","BackOrders.htm");  //
		}

        private void btnSaveChanges_Click(object sender, EventArgs e)
		{
            this.SaveChanges(true);
		}

		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void ctmListview_Popup(object sender, System.EventArgs e)
		{
			if(this.lstViewOrders.SelectedIndices.Count == 0)
			{
				this.mnuCancelBackorderedProduct.Enabled = false;
				this.mnuUpdateProductInfo.Enabled = false;
			}
			else
			{
				this.mnuCancelBackorderedProduct.Enabled = true;
				this.mnuUpdateProductInfo.Enabled = true;
			}
		}

        private void fclsOMBackOrders_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult drResult;

            if (this.btnSaveChanges.Enabled)
            {
                drResult = MessageBox.Show("Would you like to save the changes made to order " + this.oqOrderSearch.SelectedOrderNumber + "?",
                                           m_strApplicationTitle,
                                           MessageBoxButtons.YesNoCancel,
                                           MessageBoxIcon.Question,
                                           MessageBoxDefaultButton.Button1);
                switch (drResult)
                { 
                    case DialogResult.Yes:
                        this.SaveChanges(false);
                    break;
                    
                    case DialogResult.Cancel:
                        e.Cancel = true;
                    break;
                }
            }
        }

		private void frmOMBackOrders_Load(object sender, System.EventArgs e)
		{
			// Variable declaration
			OleDbDataAdapter odaEmployees, odaSuppliers;

			// Variable initialization
			m_dtaEmployees = new DataTable("Employees");
			m_dtaSuppliers = new DataTable("Suppliers");
			//m_siSupplier = new SupplierInformation();

			// Create and configure the ToolTip and associate with the Form container.
            m_ListViewToolTip = new ToolTip();
            m_ListViewToolTip.AutoPopDelay = 5000;
            m_ListViewToolTip.InitialDelay = 1000;
            m_ListViewToolTip.ReshowDelay = 500;
            m_ListViewToolTip.ShowAlways = true;
			
			// Set up the ToolTip text for the Button and Checkbox.
            m_ListViewToolTip.SetToolTip(this.lstViewOrders, "Click on the line of a Product\nin order to Update the Backorder and the Product Unit Price");

			// Load employees and populate combo box
			odaEmployees = new OleDbDataAdapter("SELECT * FROM [Employees] ORDER BY LastName", m_odcConnection);
			odaEmployees.Fill(m_dtaEmployees);
			
			// Load suppliers and populate combo box
			odaSuppliers = new OleDbDataAdapter("SELECT * FROM [Suppliers] ORDER BY CompanyName", m_odcConnection);
			odaSuppliers.Fill(m_dtaSuppliers);

            // initialize order query control
            if (m_strQueriedOrderNumber != null && m_strQueriedOrderNumber.Length > 0)
            {
                oqOrderSearch.Initialize(m_odcConnection, OrderQuery.OrderQuery.OrderQueryCaller.Backorders_ReadOnly, m_strQueriedOrderNumber);
                this.SetReadOnly();
            }
            else
                oqOrderSearch.Initialize(m_odcConnection, OrderQuery.OrderQuery.OrderQueryCaller.Backorders);
		}

		private void lstViewOrders_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
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
			this.lstViewOrders.Sort();		
		}

        private void lstViewOrders_DoubleClick(object sender, EventArgs e)
        {
            if (!m_blnReadOnly)
                this.LoadUtilityForm(UtilityForms.UpdateBackorder);
        }
		
		private void mnuCancelBackorderedProduct_Click(object sender, System.EventArgs e)
		{
            this.LoadUtilityForm(UtilityForms.CancelBackorder);
		}

        private void mnuUpdateProductInfo_Click(object sender, EventArgs e)
        {
            this.LoadUtilityForm(UtilityForms.UpdateBackorder);
        }

        private void oqOrderSearch_OnNewSelectedOrderNumber(string strSelectedOrderNumber, DateTime dtOrderDate, SupplierInformation siSupplier)
        {
            if (this.btnSaveChanges.Enabled)
            {
                if (MessageBox.Show("Would you like to save the changes made to order " + this.oqOrderSearch.SelectedOrderNumber + "?",
                                   m_strApplicationTitle,
                                   MessageBoxButtons.YesNo,
                                   MessageBoxIcon.Question,
                                   MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    this.SaveChanges(true);
            }

            if (strSelectedOrderNumber != null && strSelectedOrderNumber.Length > 0)
            {
                m_blnOrderFinishedLoading = false;

                this.SetFieldsEnabled(true);

                // init. form fields
                this.ClearFields();

                // Display the order date
                this.lblOrderDate_Data.Text = dtOrderDate.ToLongDateString();

                // Display the products from the selected Order
                this.ShowSelectedOrderProducts(strSelectedOrderNumber);
                
                m_blnOrderFinishedLoading = true;
                m_siSupplier = siSupplier;
            }
        }

        private void oqOrderSearch_OnNoOrdersFound()
        {
            this.SetFieldsEnabled(false);
        }

        private void txtTaxes_TextChanged(object sender, EventArgs e)
        {
            if(m_blnOrderFinishedLoading)
                this.btnSaveChanges.Enabled = true;
        }

        private void txtShippingHandling_TextChanged(object sender, EventArgs e)
        {
            if (m_blnOrderFinishedLoading)
                this.btnSaveChanges.Enabled = true;
        }

        private void txtDuty_TextChanged(object sender, EventArgs e)
        {
            if (m_blnOrderFinishedLoading)
                this.btnSaveChanges.Enabled = true;
        }
		#endregion

		#region Methods
		private void ClearFields()
		{
            this.btnSaveChanges.Enabled = false;

			// listview
			this.lstViewOrders.Items.Clear();

			// Labels
			this.lblOrderDate_Data.Text = "";
			this.txtDuty.Text = (0.0M).ToString(clsUtilities.FORMAT_CURRENCY);
			this.txtShippingHandling.Text = (0.0M).ToString(clsUtilities.FORMAT_CURRENCY);
			this.txtTaxes.Text = (0.0M).ToString(clsUtilities.FORMAT_CURRENCY);
		}

        private void SetFieldsEnabled(bool blnEnabled)
        {
            // first clear the fields
            this.ClearFields();

            // disable fields
            this.lstViewOrders.Enabled = blnEnabled;
            this.lblOrderDate_Data.Enabled = blnEnabled;
            this.txtDuty.Enabled = blnEnabled;
            this.txtShippingHandling.Enabled = blnEnabled;
            this.txtTaxes.Enabled = blnEnabled;
        }

		/// <summary>
		///		Finds the employee associated with the supplied 'EmployeeId'
		/// </summary>
		/// <returns>
		///		Returns the employee's name.
		/// </returns>		
		private string GetEmployee(int intEmployeeId)
		{
			DataRow dtrRow;
			int intCurrentEmployeeId = -1;
			string strEmployee = "";

			for(int i=0; i < m_dtaEmployees.Rows.Count; i++)
			{
				intCurrentEmployeeId = int.Parse(m_dtaEmployees.Rows[i]["EmployeeId"].ToString());
				if(intCurrentEmployeeId == intEmployeeId)
				{
					dtrRow = m_dtaEmployees.Rows[i];
					strEmployee = clsUtilities.FormatName_List(dtrRow["Title"].ToString(), dtrRow["FirstName"].ToString(), dtrRow["LastName"].ToString());
					break;
				}
			}

			return strEmployee;
		}
		
		/// <summary>
		///		Finds the supplier associated with the supplied 'FournisseurId'
		/// </summary>
		/// <returns>
		///		Returns the supplier's company name.
		/// </returns>
		private string GetSupplier(int intSupplierId)
		{
			int intCurrentSupplierId = -1, i;
			string strSupplier = "";

			for(i=0; i < m_dtaSuppliers.Rows.Count; i++)
			{
				intCurrentSupplierId = int.Parse(m_dtaSuppliers.Rows[i]["FournisseurId"].ToString());
				if(intCurrentSupplierId == intSupplierId)
				{
					strSupplier = m_dtaSuppliers.Rows[i]["CompanyName"].ToString();
					break;
				}
			}

			// Set supplier information for fclsOMCheckOrders_ReturnProd
			m_siSupplier.DatabaseID = int.Parse(m_dtaSuppliers.Rows[i]["FournisseurId"].ToString());
			m_siSupplier.Name = m_dtaSuppliers.Rows[i]["CompanyName"].ToString();
			m_siSupplier.ContactName = clsUtilities.FormatName_Display(m_dtaSuppliers.Rows[i]["ConTitle"].ToString(),m_dtaSuppliers.Rows[i]["ContactFirstName"].ToString(),m_dtaSuppliers.Rows[i]["ContactLastName"].ToString());
			m_siSupplier.PhoneNumber = m_dtaSuppliers.Rows[i]["PhoneNumber"].ToString();
			m_siSupplier.Email = m_dtaSuppliers.Rows[i]["Email"].ToString();

			return strSupplier;
		}

        private void LoadUtilityForm(UtilityForms ufFormType)
        {
            int intNProductBackordered;
            
            // initialize variables
            intNProductBackordered = 0;

            if (this.lstViewOrders.SelectedIndices.Count > 0)
            {
                // generate array list w/ items that were selected
                ArrayList alProducts = new ArrayList();
                clsBackorderListViewItem blviItem;
                for (int i = 0; i < this.lstViewOrders.SelectedIndices.Count; i++)
                {
                    blviItem = (clsBackorderListViewItem) this.lstViewOrders.Items[this.lstViewOrders.SelectedIndices[i]];
                    if (blviItem.NUnitsBackordered > 0)
                    {
                        alProducts.Add(blviItem);
                        intNProductBackordered++;
                    }
                }
                
                // display appropriate form and process its returned data
                if (intNProductBackordered > 0)
                {
                    switch (ufFormType)
                    {
                        case UtilityForms.CancelBackorder:
                            // show product information update form
                            fclsOMReturnProdCanceledBO frmBackOrders_Cancel = new fclsOMReturnProdCanceledBO(this,
                                                                                                             fclsOMReturnProdCanceledBO.Caller.BackOrders,
                                                                                                             this.oqOrderSearch.SelectedOrderNumber,
                                                                                                             m_siSupplier,
                                                                                                             alProducts,
                                                                                                             m_odcConnection);
                            frmBackOrders_Cancel.ShowDialog();
                        break;

                        case UtilityForms.UpdateBackorder:
                            // show product information update form
                            fclsOMBackOrders_Update frmBackOrders_Update;
                            foreach (Object obj in alProducts)
                            {
                                frmBackOrders_Update = new fclsOMBackOrders_Update(this,
                                                                                   this.oqOrderSearch.SelectedOrderNumber,
                                                                                   (clsBackorderListViewItem) obj);
                                frmBackOrders_Update.ShowDialog();
                            }
                        break;
                    }
                }
            }
        }

        private void SaveChanges(bool blnEnablePrompts)
        {
            decimal decProportion, decSubTotalProducts, decTotal;
            decimal[] decDutyPerProduct, decShippingHandlingPerProduct, decTaxesPerProduct, decSubTotalPerProduct, decTotalPerProduct;
            clsBackorderListViewItem cblviItem;
            fclsOIAccounting_Pay frmOIAccountingPay;
            int intNOrderLines;

            // initialize variables
            intNOrderLines = this.lstViewOrders.Items.Count;
            decSubTotalPerProduct = new decimal[intNOrderLines];
            decDutyPerProduct = new decimal[intNOrderLines];
            decShippingHandlingPerProduct = new decimal[intNOrderLines];
            decTaxesPerProduct = new decimal[intNOrderLines];
            decTotalPerProduct = new decimal[intNOrderLines];
            decSubTotalProducts = decTotal = 0.0m;

            if (blnEnablePrompts)
            {
                if ((decimal.Parse(this.txtDuty.Text) == (0.0M)) ||
                   (decimal.Parse(this.txtShippingHandling.Text) == (0.0M)) ||
                   (decimal.Parse(this.txtTaxes.Text) == (0.0M)))
                {
                    if (MessageBox.Show("Please ensure all that all the order information is correct (including duty, shipping and handling and taxes).\nAre you sure you would like to proceed?",
                                       m_strApplicationTitle,
                                       MessageBoxButtons.YesNo,
                                       MessageBoxIcon.Exclamation,
                                       MessageBoxDefaultButton.Button2) == DialogResult.No)
                        return;
                }
            }

            //
            // compute amount owed and distribute the extra costs among the products
            //
            // Calculate the sub-total per product and the order sub-total
            for (int i = 0; i < intNOrderLines; i++)
            {
                cblviItem = (clsBackorderListViewItem) this.lstViewOrders.Items[i];
                //decSubTotalPerProduct[i] = cblviItem.UnitPrice * (rolOrderLines[i].UnitsReceived - rolOrderLines[i].UnitsToReturn);
                decSubTotalProducts += decSubTotalPerProduct[i];
            }

            // calculate the extra costs per product (i.e. duty, taxes and s&h)
            if (decSubTotalProducts > 0)
            {
                for (int i = 0; i < intNOrderLines; i++)
                {
                    //						decProportion = decSubTotalPerProduct[i]/decSubTotalProducts;
                    //						decDutyPerProduct[i] = decProportion * this.olcContainer.Duty;
                    //						decShippingHandlingPerProduct[i] = decProportion * this.olcContainer.ShippingHandling;
                    //						decTaxesPerProduct[i] = decProportion * this.olcContainer.Taxes;
                    //						decTotalPerProduct[i] = decSubTotalPerProduct[i] + decDutyPerProduct[i] + decShippingHandlingPerProduct[i] + decTaxesPerProduct[i];
                    //						decTotal += decTotalPerProduct[i];
                }
            }

            /*
                flTax = flTransport = flDuty = flTotal = flTotalCatalog = 0;
                int	i;
                for(i=0; i<nrTotProd; i++)
                {
                    flTX[i] = 0.0;
                    flTR[i] = 0.0;
                    flDU[i] = 0.0;
                    flTotalCatalog += flPrice[i] * fltUnits[i];
                }
                this.m_dblSubTotal = flTotalCatalog;
				
                strTotalCatalog = flTotalCatalog.ToString();
                strTax = this.txtTaxes.Text;
                strTransport = this.txtShippingHandling.Text;
                strDuty = this.txtDuty.Text;
				
                flTax = double.Parse(strTax);
                this.m_dblTax = flTax;
                flTransport = double.Parse(strTransport);
                this.m_dblTransport = flTransport;
                flDuty = double.Parse(strDuty);
                this.m_dblDuty = flDuty;
				
                flTotal = flTax + flTransport + flDuty;
				
                if((flTotal > 0.0) && (flTotalCatalog > 0.0))
                    for(i=0; i<=nrTotProd; i++)
                    {
                        flRaport = (flPrice[i] * fltUnits[i])/flTotalCatalog;
                        flTX[i] = flRaport * flTax;
                        flTR[i] = flRaport * flTransport;
                        flDU[i] = flRaport * flDuty;
                    }
                flTotal += flTotalCatalog;
                strTotal = flTotal.ToString();
             */

            frmOIAccountingPay = new fclsOIAccounting_Pay(fclsOIAccounting_Pay.Caller.BackOrder,
                                                          this,
                                                          this.oqOrderSearch.SelectedOrderNumber,
                                                          -1,
                                                          decTotal,
                                                          m_odcConnection);
            frmOIAccountingPay.ShowDialog();
            if (m_blnOrderPaid)
            {
                //this.UpdateSubProdTxTrDuty();
                //this.PayTheOrder();
                this.btnSaveChanges.Enabled = false;
            }

            //else
            //	MessageBox.Show("The order has not been completley updated!","Back Orders",MessageBoxButtons.OK,MessageBoxIcon.Error);

            /*int intNUnitsBackordered;
    switch (m_intModify)
    {
        case 1:											//  update the backorder 
        {
            this.cmdClose.Enabled = false;
            m_dtOneOrder = new DataTable("Orders");
            m_odaOneOrder = new OleDbDataAdapter("Select * FROM Orders WHERE [Orders.OrderId]='" 
                + orderId + "'", m_odcConnection);
            m_odaOneOrder.Fill(m_dtOneOrder);
            OleDbCommandBuilder	ocbOrderPrix = new OleDbCommandBuilder(m_odaOneOrder);
            m_strOldQty = m_dtOneOrder.Rows[0]["BackOrderUnits"].ToString();

            DataRow targetRow = m_dtOneOrder.Rows[m_int_clickedProd];
        {
            //			if backorder > 0 update the line in the backorder table
            DateTime dt = DateTime.Now;
            //m_int-clickedProd + 1;
            targetRow.BeginEdit();
            targetRow["BackOrderUpdateDate"]	= dt.ToShortDateString();
            targetRow["BackOrderUnits"]			= float.Parse(m_strNewQty);
            targetRow["Prix"]					= m_strNewPrice;
            float newReceived = float.Parse(m_strReceived);
            float oldReceived = float.Parse(m_dtOneOrder.Rows[m_int_clickedProd]["ReceivedQty"].ToString());
            targetRow["ReceivedQty"]			=oldReceived + newReceived;
            targetRow.EndEdit();
            m_odaOneOrder.Update(m_dtOneOrder);
            m_dtOneOrder.AcceptChanges();
        }

            this.PopulateListBox(1);
            fltUnits[m_int_clickedProd] = 0;
            flPrice[m_int_clickedProd] = 0.0;
            ++nrUpdatedProd;
            this.UpdateSubproducts(m_int_clickedProd);
            this.btnUpdateOrder.Enabled = true;
        }
            intModBO[m_int_clickedProd] = 1;
            break;
        case 2:					//  cancel (delete) the backorder and write in canceled BO
        {
            m_dtDeleteOrder = new DataTable("Orders");
            m_odaDeleteOrder = new OleDbDataAdapter("Select * FROM Orders WHERE ([Orders.OrderId]='" 
                + orderId + "' AND [Orders.SubPrId]=" + subPrId + ")", m_odcConnection);
            m_odaDeleteOrder.Fill(m_dtDeleteOrder);
            OleDbCommandBuilder	ocbOrderDelete = new OleDbCommandBuilder(m_odaDeleteOrder);
            try
            {
                DataRow targetRow = m_dtDeleteOrder.Rows[0];
            {
                DateTime dt = DateTime.Now;
                targetRow.BeginEdit();
                targetRow["BackOrderUpdateDate"]	= dt.ToShortDateString();
                m_strCBOUnits = m_dtDeleteOrder.Rows[0]["BackOrderUnits"].ToString();
                targetRow["BackOrderUnits"]			= 0.0;
                targetRow.EndEdit();
                m_odaDeleteOrder.Update(m_dtDeleteOrder);
                m_dtDeleteOrder.AcceptChanges();
            }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            OleDbCommandBuilder	ocbOrderCancel = new OleDbCommandBuilder(m_odaDeleteOrder);

            this.writeInCanceledBackOrders(0, m_strCBOUnits);

            this.PopulateListBox(1);
        }
            intModBO[m_int_clickedProd] = 1;

            break;
        case 0:
        default:
            break;
    }
}
return;*/
        }

        /*private void UpdateSubproducts(int m_int_clickedProd)						
        {
//          update the SubProducts table with the received quntity
            /*m_dtaSubProduct = new DataTable("SubProducts");
            m_odaSubProduct = new OleDbDataAdapter("Select * FROM SubProducts WHERE SubPrId="+ m_strSubPrId, m_odcConnection);
            m_odaSubProduct.Fill(m_dtaSubProduct);
            OleDbCommandBuilder	ocbSubProduct = new OleDbCommandBuilder(m_odaSubProduct);
            double priceMin = double.Parse(m_dtaSubProduct.Rows[0]["PrixMin"].ToString());
            double priceMax = double.Parse(m_dtaSubProduct.Rows[0]["PrixMax"].ToString());
            double price = double.Parse(m_strNewPrice);
            string priceMinOI = m_dtaSubProduct.Rows[0]["PrixMinOI"].ToString();
            string priceMaxOI = m_dtaSubProduct.Rows[0]["PrixMaxOI"].ToString();
            fltUnits[m_int_clickedProd] = float.Parse(m_strReceived.ToString());
            flPrice[m_int_clickedProd] = price;

            DataRow targetRow = m_dtaSubProduct.Rows[0];
            targetRow.BeginEdit();
            targetRow["Prix"] = m_strNewPrice;
            if ((price <= priceMin) || (priceMin == 0))
            {
                priceMin = price;
                priceMinOI = orderId;
            }
            targetRow["PrixMin"] = priceMin.ToString();
            targetRow["PrixMinOI"] = priceMinOI;
            if (price >= priceMax)
            {
                priceMax = price;
                priceMaxOI = orderId;
            }
            targetRow["PrixMax"] = priceMax.ToString();
            targetRow["PrixMaxOI"] = priceMaxOI;
            targetRow["PrixOrderId"] = orderId;
            targetRow.EndEdit();

            m_odaSubProduct.Update(m_dtaSubProduct);
            m_dtaSubProduct.AcceptChanges();

        }
		
        private void writeInCanceledBackOrders(int m_int_clickedProd, string m_strCBOUnits)
        {
            /*m_odaCancelOrder = new OleDbDataAdapter("Select * FROM Orders WHERE OrderId = '"+orderId+"' AND SubPrId = "+m_strSubPrId , m_odcConnection);
            m_dtCancelOrder = new DataTable("Orders");
            m_odaCancelOrder.Fill(m_dtCancelOrder);
            OleDbCommandBuilder	ocbOrderPrix = new OleDbCommandBuilder(m_odaCancelOrder);

            DateTime dt = DateTime.Now;
            DataRow newRow = m_dtCancelOrder.Rows[0];
            newRow.BeginEdit();
            newRow["CanceledBODate"]		=	dt.ToShortDateString();
            newRow["CanceledBOEmployeeId"]	=	intFindEmplId(m_strEmployee);
            newRow["CanceledBOUnits"]		=	m_strCBOUnits;
            newRow["BackOrderUnits"]		=	0;
            newRow.EndEdit();

            m_odaCancelOrder.Update(m_dtCancelOrder);
            m_dtCancelOrder.AcceptChanges();
        }*/

        /*public void UpdateSubProdTxTrDuty()
        {
//															Update Subproduct Table
        /*	int		  i, m_intSubId;
            OleDbDataAdapter m_odaUpdateSubProd = new OleDbDataAdapter("SELECT * from SubProducts Order BY SubPrId",m_odcConnection);
            DataTable m_dtaUpdateSubProd = new DataTable("SubProducts");
            OleDbCommandBuilder odcUpdateSubProd = new OleDbCommandBuilder(m_odaUpdateSubProd);
            m_odaUpdateSubProd.Fill(m_dtaUpdateSubProd);
            for(i=0; i<nrTotProd; i++)
            {
                if(fltUnits[i] == 0f)
                    continue;
                flCatalogPay = flPrice[i] * fltUnits[i];
                flTotalPay = flCatalogPay +flTX[i] + flTR[i] + flDU[i];
                m_intSubId = int.Parse(this.m_dtAllOrders.Rows[i]["Orders.SubPrId"].ToString());
                int m_intRowIndex = GetRowIndex(m_intSubId, m_dtaUpdateSubProd);
                if(m_intRowIndex == -1)
                {
                    MessageBox.Show("Sub Products introuvable: wrong name or it is not in the Database!!","Sub Product Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return;
                }
                DataRow targetRow = m_dtaUpdateSubProd.Rows[m_intRowIndex];
                targetRow.BeginEdit();
                string m_strPrix = m_dtaUpdateSubProd.Rows[m_intRowIndex]["Prix"].ToString();
                targetRow["Qtty"] = fltUnits[i] + float.Parse(m_dtaUpdateSubProd.Rows[m_intRowIndex]["Qtty"].ToString());
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

        }	*/
        /*public void PayTheOrder()
        {
/*			OleDbDataAdapter m_odaSavePayments = new OleDbDataAdapter("Select * FROM OrderPayment Order by OrderId", m_odcConnection);													 
            DataTable m_dtSavePayments = new DataTable("OrderPayment");
            m_odaSavePayments.Fill(m_dtSavePayments);
            OleDbCommandBuilder	ocbOrderPay = new OleDbCommandBuilder(m_odaSavePayments);
//																	save a new order line with the new datas
            DataRow targetRow = m_dtSavePayments.NewRow();
            targetRow["OrderId"] = orderId.ToString();
            targetRow["PaymentDate"] = m_dtPaymentDate.ToShortDateString();
            targetRow["SubTotal"] = m_dblSubTotal.ToString();
            targetRow["Tax"] = m_dblTax.ToString();
            targetRow["Transport"] = m_dblTransport.ToString();
            targetRow["Duty"] = m_dblDuty.ToString();
            double m_flTotPay = double.Parse(m_strPayedSum);
            targetRow["TotalPay"] = m_flTotPay.ToString();
            double m_flPenalty = double.Parse(m_strPayedPenalty);
            targetRow["Penalty"] = m_flPenalty.ToString();
            targetRow["PayedPer"] = m_strPayedPer;
            targetRow["PayedBy"] = m_intPayedBy;
            double m_flSumDue = flTotal - double.Parse(m_strPayedSum);
            targetRow["SumDue"] = m_flSumDue.ToString();;
            targetRow["checkPayment"] = "0";
            if(m_flSumDue == 0.0)
                targetRow["checkPayment"] = "1";
            //																	Add the new row to the table
            m_dtSavePayments.Rows.Add(targetRow);
            m_odaSavePayments.Update(m_dtSavePayments);
            m_dtSavePayments.AcceptChanges();

            this.Close();
        }*/

		/// <summary>
		///		Function called by fclsOI_Accounting_Pay in order to return payment information for the current order.
		/// </summary>
		public void SetPaymentInformation(bool blnOrderPaid, DateTime dtPaymentDate, decimal decAmoundPaid, decimal decPenalty, string strPaymentMethod, int intPayerEmployeeId)
		{
			m_blnOrderPaid = blnOrderPaid;
			m_dtPaymentDate = dtPaymentDate;
			m_decAmountPaid = decAmoundPaid;
			m_decPenalty = decPenalty;
			m_strPayedPer = strPaymentMethod;
			//m_intPayedBy = intPayerEmployeeId;
		}

        private void SetReadOnly()
        {
            this.Text += " (Viewing Only)";

            this.oqOrderSearch.ReadOnly = true;
            this.txtDuty.Enabled = this.txtTaxes.Enabled = this.txtShippingHandling.Enabled = false;
            m_blnReadOnly = true;
        }

        private void ShowSelectedOrderProducts(string strOrderId)
		{
			// Variable declaration
            clsBackorderListViewItem blviItem;
			DataRow dtrRow;
            decimal decTaxes, decDuty, decShippingHandling;
			OleDbCommandBuilder	ocbOrderProducts;
			OleDbDataAdapter odaOrderProducts;
			string strQuery;

			// Variable initialization
            decTaxes = decDuty = decShippingHandling = 0.0M;
			m_dtaOrderProducts = new DataTable();
            strQuery = "SELECT ALL [Products.MatName], [SubProducts.MatName], [Trademarks.Trademark], [Orders.Pack], [Orders.BackOrderUnits], [Orders.Prix], [Orders.BackOrderUpdateDate], [Orders.SubPrId], [Orders.Tax], [Orders.Transport], [Orders.Duty] " +
					   "FROM Products INNER JOIN ((Trademarks INNER JOIN Orders ON Trademarks.MarComId = Orders.MarComId) INNER JOIN SubProducts ON (SubProducts.SubPrId = Orders.SubPrId) AND (Trademarks.MarComId = SubProducts.MarComId)) ON (Orders.MatId = Products.MatId) AND (Products.MatId = SubProducts.MatId) " +
					   "WHERE (((Orders.OrderId)='" + strOrderId + "'))";
			
			// Get data from database
			try
			{
				odaOrderProducts = new OleDbDataAdapter(strQuery,m_odcConnection);
				ocbOrderProducts = new OleDbCommandBuilder(odaOrderProducts);
				odaOrderProducts.Fill(m_dtaOrderProducts);
			
				// Fill listview
				for (int i=0; i<m_dtaOrderProducts.Rows.Count; i++)
				{
					dtrRow = m_dtaOrderProducts.Rows[i];

                    decTaxes += (decimal) dtrRow["Orders.Tax"];
                    decDuty += (decimal) dtrRow["Orders.Duty"];
                    decShippingHandling += (decimal) dtrRow["Orders.Transport"];

                    blviItem = new clsBackorderListViewItem(dtrRow["Products.MatName"].ToString(),
														    dtrRow["SubProducts.MatName"].ToString(),
														    dtrRow["Trademarks.Trademark"].ToString(),
														    dtrRow["Orders.Pack"].ToString(),
														    decimal.Parse(dtrRow["Orders.Prix"].ToString()),
														    (int) dtrRow["Orders.BackOrderUnits"],
                                                            dtrRow["Orders.BackOrderUpdateDate"]);
					lstViewOrders.Items.Add(blviItem);
				}
			
				// Set the listview's order column and order the list
				m_lvwColumnSorter.SortColumn = 0;
				m_lvwColumnSorter.Order = SortOrder.Ascending;

                this.txtDuty.Price = decDuty;
                this.txtShippingHandling.Price = decShippingHandling;
                this.txtTaxes.Price = decTaxes;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.InnerException + "\n" + ex.StackTrace, this.Text);
			}
		}

        public void UtilityFormChangedData()
        {
            this.btnSaveChanges.Enabled = true;
        }

        public void UtilityFormChangedData(int intEmployeeID)
        {
            this.btnSaveChanges.Enabled = true;
            m_intEmployeedID_Cancel = intEmployeeID;
        }

		//public void UpdateProductData(int iMod, string strNew, string strQty, string strReceived)
		/*public void UpdateProductData(bool blnDataUpdated, int intNUnitsReceived, decimal decUpdatedUnitPrice)
		{
			//m_blnProductDataUpdated = true;
			m_intNUnitsReceived = intNUnitsReceived;
			m_decUpdatedUnitPrice = decUpdatedUnitPrice;
			/*m_intModify = iMod;
			m_strNewPrice = strNew;
			m_strNewQty = strQty;
			m_strReceived = strReceived;
		}*/
		#endregion

		//==============================================================================================================================================================
		//==============================================================================================================================================================
		private void openOrders()
		{
			//																	 View the non checked Past Orders
			/*orderDataAdapter = new OleDbDataAdapter("Select distinct OrderId, OrderDate,FournisseurId, EmployeeId From [Orders] WHERE BackOrderUnits > 0",  m_odcConnection); //AND Orders.OrderId LIKE \'%" + strLast2Digit + "\'Order by OrderId", m_odcConnection);
			m_dtOrder = new DataTable("Orders");
			orderDataAdapter.Fill(m_dtOrder);
			//																	 Open the table Commandes

			nrOrder = m_dtOrder.Rows.Count;
			this.lbxOrderNumber.Items.Clear();
			System.Object[]	ItemObject = new System.Object[nrOrder];
			if(nrOrder <= 0)
				return;
			for (int i = 0; i <nrOrder; i++)
			{
				m_drOrder = m_dtOrder.Rows[i];
				ItemObject[i] = m_drOrder["OrderId"];
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
				this.lbxOrderNumber.SelectedIndex = nrOrder-1;*/

		}
		private int findIndex(string m_strOrderSelect)
		{
			/*string m_strOrderNumber = "0";
			int m_intOrderIndex = 0;
			int m_intOrderNr = m_dtOrder.Rows.Count;
			for(int i=0; i< m_intOrderNr; i++)
			{
				m_strOrderNumber = m_dtOrder.Rows[i]["OrderId"].ToString();
				if(string.Compare(m_strOrderNumber, m_strOrderSelect) == 0)
					return i;
			}*/
			return -1;
		}
		private void PopulateListBox(int nrTime)
		{
			//												                     View Past Orders
			/*dataSet = new DataSet("Orders");
			m_dtAllOrders = new DataTable("Orders");
			m_odaAllOrders = new OleDbDataAdapter("SELECT ALL [Products.MatName], [SubProducts.MatName], " +
				"[Trademarks.Trademark], [Orders.Pack], [Orders.BackOrderUnits], [Orders.Prix], [Orders.BackOrderUpdateDate], [Orders.SubPrId]  " +
				"FROM Products INNER JOIN ((Trademarks INNER JOIN Orders ON Trademarks.MarComId = Orders.MarComId) " +
				"INNER JOIN SubProducts ON (SubProducts.SubPrId = Orders.SubPrId) AND (Trademarks.MarComId = SubProducts.MarComId)) " +
				"ON (Orders.MatId = Products.MatId) AND (Products.MatId = SubProducts.MatId)" +
				" WHERE (((Orders.OrderId)='" + orderId + "'))", m_odcConnection);
			m_odaAllOrders.Fill(m_dtAllOrders);

			lstViewOrders.Items.Clear();
			int nrProd = m_dtAllOrders.Rows.Count;
			nrTotProd = nrProd;
			double dMan = 0.0;
			System.Object[]	ItemObject = new System.Object[nrProd];
			float manUnits = 0f;
//			nrTotBO = 0;
			Color foreColor = new Color();
			ListViewItem lviItem;
			for (int i = 0; i <nrProd; i++)
			{
				foreColor = Color.LightGray;
				manUnits = float.Parse(m_dtAllOrders.Rows[i]["Orders.BackOrderUnits"].ToString());
				if(manUnits > 0)
				{
					if(nrTime == 0)
						++nrTotBO;
					foreColor = Color.Black;
					lviItem = lstViewOrders.Items.Add(((DateTime) (m_dtAllOrders.Rows[i]["Orders.BackOrderUpdateDate"])).ToString("MMM dd, yyyy"));
				}
				else
					lviItem = lstViewOrders.Items.Add("");
				lviItem.ForeColor = foreColor;
				lviItem.SubItems.Add(m_dtAllOrders.Rows[i]["Products.MatName"].ToString());
				lviItem.SubItems.Add(m_dtAllOrders.Rows[i]["SubProducts.MatName"].ToString());
				lviItem.SubItems.Add(m_dtAllOrders.Rows[i]["Trademarks.Trademark"].ToString());
				dMan = double.Parse(m_dtAllOrders.Rows[i]["Orders.Prix"].ToString());
				lviItem.SubItems.Add(dMan.ToString("#,##0.00"));
				lviItem.SubItems.Add(manUnits.ToString());
				lviItem.SubItems.Add(m_dtAllOrders.Rows[i]["Orders.Pack"].ToString());
			}*/
		}																																														


		private void changeSupplier(String SuplId)
		{
			/*supplierDataAdapter.Fill(m_dtSupplier);
			int nrSupplier = m_dtSupplier.Rows.Count;
			int index = 0;
			String strMan;

			for (int i = 0; i <nrSupplier; i++)
			{
				strMan = m_dtSupplier.Rows[i]["FournisseurId"].ToString();
				if(strMan == SuplId)
				{
					index = i;
					break;
				}
			}			
			cmbSupplier.SelectedIndex = index;*/

		}

		private void changeEmpl(String EmplId)
		{
			/*employeDataAdapter.Fill(m_dtEmploye);
			int nrEmploye = m_dtEmploye.Rows.Count;		
			int index = 0;
			String strEmpl;

			for (int i = 0; i <nrEmploye; i++)
			{
				strEmpl = m_dtEmploye.Rows[i]["EmployeeId"].ToString();
				if(strEmpl == EmplId)
				{
					index = i;
					break;
				}
			}
			cmbOrderedBy.SelectedIndex = index;	*/
		}	
		private void changeDate(int index)
		{
			/*orderDataAdapter.Fill(m_dtOrder);
			DataRow	m_drOrder;
			m_drOrder = m_dtOrder.Rows[index];
			this.orderDate.Text = m_drOrder["OrderDate"].ToString();*/
		}



		private void afishSupplier()
		{
			/*int nrSupplier = m_dtSupplier.Rows.Count;
			int strMan;

			for (int i = 0; i <nrSupplier; i++)
			{
				strMan = int.Parse(m_dtSupplier.Rows[i]["FournisseurId"].ToString());
				if(strMan == fclsGENInput.supplId)
				{
					this.cmbSupplier.SelectedIndex = i;
					return;
				}
			}	*/		
		}

		private void orderDate_ValueChanged(object sender, System.EventArgs e)
		{
			/*CultureInfo ciCurrentCulture;
			string sOrdDate ="";

			ciCurrentCulture = (CultureInfo) CultureInfo.CurrentCulture.Clone();
			ciCurrentCulture.DateTimeFormat.DateSeparator = "/";

			if(nOrd == 0)
				return;
			if(this.optOrderDate.Checked)
			{
				sOrdDate = this.orderDate.Value.ToString(clsUtilities.FORMAT_DATE_QUERY, ciCurrentCulture);
				orderDataAdapter = null;
				orderDataAdapter = new OleDbDataAdapter("Select distinct OrderId, OrderDate,FournisseurId, " +
					"EmployeeId From [Orders] Where OrderDate = " + sOrdDate + " AND BackOrderUnits > 0 " +
					"AND Orders.OrderId LIKE \'%" + strLast2Digit + "\'Order by OrderId", m_odcConnection);
				DataTable m_dtOrder = new DataTable("Orders");
				orderDataAdapter.Fill(m_dtOrder);
				int nrOrder = m_dtOrder.Rows.Count;
				if(nrOrder <= 0)
				{
					MessageBox.Show("For this Date does not exist Orders!!!");
					this.lbxOrderNumber.Items.Clear();
					this.lstViewOrders.Items.Clear();

					return;
				}
				DataRow	m_drOrder;
				System.Object[] ItemObject = new System.Object[nrOrder];
				this.lbxOrderNumber.Items.Clear();
				for (int i = 0; i <nrOrder; i++)
				{
					m_drOrder = m_dtOrder.Rows[i];
					ItemObject[i] = m_drOrder["OrderId"];
				}
				this.lbxOrderNumber.Items.AddRange(ItemObject);

				this.lbxOrderNumber.SelectedIndex = nrOrder-1;

				fclsGENInput.supplId = int.Parse(m_dtOrder.Rows[nrOrder-1]["FournisseurId"].ToString());
				afishSupplier();
				fclsGENInput.emplId = int.Parse(m_dtOrder.Rows[nrOrder-1]["EmployeeId"].ToString());
				afishEmployee();
			}		*/
		}
		private void afishEmployee()
		{
			/*int nrEmpl = m_dtEmploye.Rows.Count;
			int strMan;

			for (int i = 0; i <nrEmpl; i++)
			{
				strMan = int.Parse(m_dtEmploye.Rows[i]["EmployeeId"].ToString());
				if(strMan == fclsGENInput.emplId)
				{
					this.cmbOrderedBy.SelectedIndex = i;
					return;
				}
			}	*/		
		}


		public int intFindEmplId(string strName)
		{
			/*int nrRecs = m_dtEmploye.Rows.Count;
			string strComp = strName.Trim();
			for (int i=0; i<nrRecs; i++)
			{
				string strEmpl = m_dtEmploye.Rows[i]["Title"].ToString()+ " " + m_dtEmploye.Rows[i]["FirstName"].ToString() +
					", " + m_dtEmploye.Rows[i]["LastName"].ToString();
				if (strComp == strEmpl)
					return int.Parse(m_dtEmploye.Rows[i]["EmployeeId"].ToString());
			}*/

			return 0;
		}
		public string strFindProductId(string strName)
		{
			/*OleDbDataAdapter m_odaProducts = new OleDbDataAdapter("Select * FROM [Products] ORDER BY MatId", m_odcConnection);
			DataTable m_dtProducts = new DataTable("Products");
			try
			{
				m_odaProducts.Fill(m_dtProducts);
			}
			catch(OleDbException ex)
			{
				MessageBox.Show (ex.Message );
			}
			int nrRecs = m_dtProducts.Rows.Count;
			string strComp = strName.Trim();
			for (int i=0; i<nrRecs; i++)
			{
				string strPrName = m_dtProducts.Rows[i]["MatName"].ToString();
				if (strComp == strPrName)
					return m_dtProducts.Rows[i]["MatId"].ToString();
			}*/
			return "-1";
		}
		public string strFindSubPrId(string strName, string PrId)
		{
			/*OleDbDataAdapter m_odaSubProducts = new OleDbDataAdapter("Select * FROM [SubProducts] WHERE MatId=" + PrId + " ORDER BY SubPrId", m_odcConnection);
			DataTable m_dtSubProducts = new DataTable("SubProducts");
			try
			{
				m_odaSubProducts.Fill(m_dtSubProducts);
			}
			catch(OleDbException ex)
			{
				MessageBox.Show (ex.Message );
			}
			int nrRecs = m_dtSubProducts.Rows.Count;
			string strComp = strName.Trim();
			for (int i=0; i<nrRecs; i++)
			{
				string strSubPrName = m_dtSubProducts.Rows[i]["MatName"].ToString();
				if (strComp == strSubPrName)
					return m_dtSubProducts.Rows[i]["SubPrId"].ToString();
			}*/
			return "-1";
		}

		public static void SetCancelValues(int iCancel, string strEmpl)
		{
			//m_intModify = iCancel;
			//m_strEmployee = strEmpl;
		}

		private void DelBoNull()
		{
//			if backorder = 0 delete line from backorder table
			/*for(int i=nrTotProd-1; i>-1; i--)
			{
				int intNrBo = int.Parse(m_dtOneOrder.Rows[i]["BackOrderQty"].ToString());
				if(intNrBo == 0)
				{
					DataRow deleteRow = m_dtOneOrder.Rows[i];
					deleteRow.Delete();
					m_odaOneOrder.Update(m_dtOneOrder);
					m_dtOneOrder.AcceptChanges();
				}
			}*/

		}

        private int GetRowIndex(int m_intSubId, DataTable m_dtaUpdateSubProd)
        {
            /*int j, m_intSubPrIndex;

            for(j=0; j<m_dtaUpdateSubProd.Rows.Count; j++)
            {
                m_intSubPrIndex = int.Parse(m_dtaUpdateSubProd.Rows[j]["SubPrId"].ToString());
                if(m_intSubId == m_intSubPrIndex)
                    return j;
            }*/
            return -1;
        }
	}
}