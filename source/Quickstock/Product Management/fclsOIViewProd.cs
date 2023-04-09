using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for ViewProd.
	/// </summary>
	public class fclsOIViewProd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.TabPage tbpCategory;
		private System.Windows.Forms.TabPage tbpProduct;
		private System.Windows.Forms.TabControl tabSearch;
		private System.Windows.Forms.GroupBox grpInfo;
		private System.Windows.Forms.Label lblCategorySC;
		private System.Windows.Forms.Label lblProductsSC;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.Label lblTotalPay;
		private System.Windows.Forms.Label lblTaxTranspDutyPay;
		private System.Windows.Forms.Label lblCatalogPay;
		private System.Windows.Forms.Label lblBackOrder;
		private System.Windows.Forms.Label lblReceivedQty;
		private System.Windows.Forms.Label lblReordLevel;
		private System.Windows.Forms.Label lblMaxPrice;
		private System.Windows.Forms.Label lblLastPrice;
		private System.Windows.Forms.Label lblSubProductsSC;
		private System.Windows.Forms.Label lblMinPrice;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label10;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label12;
		private System.Windows.Forms.Label label16;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label18;
		private System.Windows.Forms.Label label20;
		private System.Windows.Forms.Label label21;
		private System.Windows.Forms.ListBox lbxProducts;
		private System.Windows.Forms.ListBox lbxSubProducts;
		private System.Windows.Forms.ComboBox cmbCategory;
		private System.Windows.Forms.ComboBox cmbProducts;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.GroupBox grpDate;
		private System.Windows.Forms.Label lblEndDate;
		private System.Windows.Forms.Label lblStartDate;
		public System.Windows.Forms.DateTimePicker dtpEnd;
		public System.Windows.Forms.DateTimePicker dtpStart;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection m_odcConnection;
		private OleDbDataAdapter m_odaOrders, m_odaCategories, m_odaProducts, m_odaSubProducts, m_odaCmbProducts;
		private DataTable m_dtaOrders, m_dtaCategories, m_dtaProducts, m_dtaSubProducts, m_dtaCmbProducts;
		private int			m_intCategoryId = -1, m_intProductId = -1, m_intSubProductId = -1;
		private int			m_intCategoriesSelectedIndex = -1, m_intProductsSelectedIndex = -1, m_intSubProductsSelectedIndex = -1;
		private double		m_dblCatPay, m_dblTotPay, m_dblTxTrDu;
		private double		m_dblBackOrder, m_dblRecQty, m_dblMaxPrice, m_dblMinPrice;

		public fclsOIViewProd(OleDbConnection odcConnection)
		{
			InitializeComponent();

			m_odcConnection = odcConnection;
			m_dtaCategories = new DataTable();
			m_odaCategories = new OleDbDataAdapter("SELECT * FROM Categories ORDER BY CategName",m_odcConnection);
			m_odaCategories.Fill(m_dtaCategories);
			for(int i=0; i < m_dtaCategories.Rows.Count; i++)
			{
				this.cmbCategory.Items.Add(m_dtaCategories.Rows[i]["CategName"].ToString());
			}
			m_dtaCmbProducts = new DataTable();
			m_odaCmbProducts = new OleDbDataAdapter("SELECT * FROM Products ORDER BY MatName",m_odcConnection);
			m_odaCmbProducts.Fill(m_dtaCmbProducts);
			for(int i=0; i < m_dtaCmbProducts.Rows.Count; i++)
			{
				this.cmbProducts.Items.Add(m_dtaCmbProducts.Rows[i]["MatName"].ToString());
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
			this.tabSearch = new System.Windows.Forms.TabControl();
			this.tbpCategory = new System.Windows.Forms.TabPage();
			this.lblProductsSC = new System.Windows.Forms.Label();
			this.lbxProducts = new System.Windows.Forms.ListBox();
			this.cmbCategory = new System.Windows.Forms.ComboBox();
			this.lblCategorySC = new System.Windows.Forms.Label();
			this.tbpProduct = new System.Windows.Forms.TabPage();
			this.label21 = new System.Windows.Forms.Label();
			this.label20 = new System.Windows.Forms.Label();
			this.cmbProducts = new System.Windows.Forms.ComboBox();
			this.lblCategory = new System.Windows.Forms.Label();
			this.grpInfo = new System.Windows.Forms.GroupBox();
			this.lblTotalPay = new System.Windows.Forms.Label();
			this.lblTaxTranspDutyPay = new System.Windows.Forms.Label();
			this.lblCatalogPay = new System.Windows.Forms.Label();
			this.label16 = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.label18 = new System.Windows.Forms.Label();
			this.lblBackOrder = new System.Windows.Forms.Label();
			this.lblReceivedQty = new System.Windows.Forms.Label();
			this.lblReordLevel = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.label12 = new System.Windows.Forms.Label();
			this.lblMaxPrice = new System.Windows.Forms.Label();
			this.lblLastPrice = new System.Windows.Forms.Label();
			this.lblMinPrice = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.btnClose = new System.Windows.Forms.Button();
			this.lblSubProductsSC = new System.Windows.Forms.Label();
			this.lbxSubProducts = new System.Windows.Forms.ListBox();
			this.btnHelp = new System.Windows.Forms.Button();
			this.grpDate = new System.Windows.Forms.GroupBox();
			this.lblEndDate = new System.Windows.Forms.Label();
			this.lblStartDate = new System.Windows.Forms.Label();
			this.dtpEnd = new System.Windows.Forms.DateTimePicker();
			this.dtpStart = new System.Windows.Forms.DateTimePicker();
			this.tabSearch.SuspendLayout();
			this.tbpCategory.SuspendLayout();
			this.tbpProduct.SuspendLayout();
			this.grpInfo.SuspendLayout();
			this.grpDate.SuspendLayout();
			this.SuspendLayout();
			// 
			// tabSearch
			// 
			this.tabSearch.Controls.Add(this.tbpCategory);
			this.tabSearch.Controls.Add(this.tbpProduct);
			this.tabSearch.Location = new System.Drawing.Point(8, 8);
			this.tabSearch.Name = "tabSearch";
			this.tabSearch.SelectedIndex = 0;
			this.tabSearch.Size = new System.Drawing.Size(360, 208);
			this.tabSearch.TabIndex = 0;
			// 
			// tbpCategory
			// 
			this.tbpCategory.BackColor = System.Drawing.SystemColors.Control;
			this.tbpCategory.Controls.Add(this.lblProductsSC);
			this.tbpCategory.Controls.Add(this.lbxProducts);
			this.tbpCategory.Controls.Add(this.cmbCategory);
			this.tbpCategory.Controls.Add(this.lblCategorySC);
			this.tbpCategory.Location = new System.Drawing.Point(4, 22);
			this.tbpCategory.Name = "tbpCategory";
			this.tbpCategory.Size = new System.Drawing.Size(352, 182);
			this.tbpCategory.TabIndex = 0;
			this.tbpCategory.Text = "Search by Category";
			// 
			// lblProductsSC
			// 
			this.lblProductsSC.AutoSize = true;
			this.lblProductsSC.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProductsSC.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblProductsSC.Location = new System.Drawing.Point(16, 64);
			this.lblProductsSC.Name = "lblProductsSC";
			this.lblProductsSC.Size = new System.Drawing.Size(57, 20);
			this.lblProductsSC.TabIndex = 26;
			this.lblProductsSC.Text = "Products";
			this.lblProductsSC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbxProducts
			// 
			this.lbxProducts.Location = new System.Drawing.Point(80, 64);
			this.lbxProducts.Name = "lbxProducts";
			this.lbxProducts.Size = new System.Drawing.Size(240, 95);
			this.lbxProducts.TabIndex = 25;
			this.lbxProducts.SelectedIndexChanged += new System.EventHandler(this.lbxProducts_SelectedIndexChanged);
			// 
			// cmbCategory
			// 
			this.cmbCategory.Location = new System.Drawing.Point(80, 24);
			this.cmbCategory.Name = "cmbCategory";
			this.cmbCategory.Size = new System.Drawing.Size(240, 21);
			this.cmbCategory.TabIndex = 21;
			this.cmbCategory.SelectedIndexChanged += new System.EventHandler(this.cmbCategory_SelectedIndexChanged);
			// 
			// lblCategorySC
			// 
			this.lblCategorySC.AutoSize = true;
			this.lblCategorySC.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCategorySC.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblCategorySC.Location = new System.Drawing.Point(16, 24);
			this.lblCategorySC.Name = "lblCategorySC";
			this.lblCategorySC.Size = new System.Drawing.Size(59, 20);
			this.lblCategorySC.TabIndex = 22;
			this.lblCategorySC.Text = "Category";
			this.lblCategorySC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// tbpProduct
			// 
			this.tbpProduct.Controls.Add(this.label21);
			this.tbpProduct.Controls.Add(this.label20);
			this.tbpProduct.Controls.Add(this.cmbProducts);
			this.tbpProduct.Controls.Add(this.lblCategory);
			this.tbpProduct.Location = new System.Drawing.Point(4, 22);
			this.tbpProduct.Name = "tbpProduct";
			this.tbpProduct.Size = new System.Drawing.Size(352, 182);
			this.tbpProduct.TabIndex = 1;
			this.tbpProduct.Text = "Search by Product";
			// 
			// label21
			// 
			this.label21.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label21.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label21.Location = new System.Drawing.Point(16, 64);
			this.label21.Name = "label21";
			this.label21.Size = new System.Drawing.Size(59, 20);
			this.label21.TabIndex = 4;
			this.label21.Text = "Products";
			// 
			// label20
			// 
			this.label20.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label20.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label20.Location = new System.Drawing.Point(16, 24);
			this.label20.Name = "label20";
			this.label20.Size = new System.Drawing.Size(59, 20);
			this.label20.TabIndex = 3;
			this.label20.Text = "Category";
			// 
			// cmbProducts
			// 
			this.cmbProducts.Location = new System.Drawing.Point(80, 64);
			this.cmbProducts.Name = "cmbProducts";
			this.cmbProducts.Size = new System.Drawing.Size(248, 21);
			this.cmbProducts.TabIndex = 2;
			this.cmbProducts.SelectedIndexChanged += new System.EventHandler(this.cmbProducts_SelectedIndexChanged);
			// 
			// lblCategory
			// 
			this.lblCategory.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblCategory.Location = new System.Drawing.Point(80, 24);
			this.lblCategory.Name = "lblCategory";
			this.lblCategory.Size = new System.Drawing.Size(248, 21);
			this.lblCategory.TabIndex = 1;
			// 
			// grpInfo
			// 
			this.grpInfo.Controls.Add(this.lblTotalPay);
			this.grpInfo.Controls.Add(this.lblTaxTranspDutyPay);
			this.grpInfo.Controls.Add(this.lblCatalogPay);
			this.grpInfo.Controls.Add(this.label16);
			this.grpInfo.Controls.Add(this.label17);
			this.grpInfo.Controls.Add(this.label18);
			this.grpInfo.Controls.Add(this.lblBackOrder);
			this.grpInfo.Controls.Add(this.lblReceivedQty);
			this.grpInfo.Controls.Add(this.lblReordLevel);
			this.grpInfo.Controls.Add(this.label10);
			this.grpInfo.Controls.Add(this.label11);
			this.grpInfo.Controls.Add(this.label12);
			this.grpInfo.Controls.Add(this.lblMaxPrice);
			this.grpInfo.Controls.Add(this.lblLastPrice);
			this.grpInfo.Controls.Add(this.lblMinPrice);
			this.grpInfo.Controls.Add(this.label3);
			this.grpInfo.Controls.Add(this.label2);
			this.grpInfo.Controls.Add(this.label1);
			this.grpInfo.Location = new System.Drawing.Point(8, 224);
			this.grpInfo.Name = "grpInfo";
			this.grpInfo.Size = new System.Drawing.Size(656, 104);
			this.grpInfo.TabIndex = 1;
			this.grpInfo.TabStop = false;
			this.grpInfo.Text = "Product Info";
			// 
			// lblTotalPay
			// 
			this.lblTotalPay.BackColor = System.Drawing.SystemColors.Control;
			this.lblTotalPay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblTotalPay.Location = new System.Drawing.Point(552, 72);
			this.lblTotalPay.Name = "lblTotalPay";
			this.lblTotalPay.Size = new System.Drawing.Size(88, 16);
			this.lblTotalPay.TabIndex = 17;
			this.lblTotalPay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblTaxTranspDutyPay
			// 
			this.lblTaxTranspDutyPay.BackColor = System.Drawing.SystemColors.Control;
			this.lblTaxTranspDutyPay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblTaxTranspDutyPay.Location = new System.Drawing.Point(552, 48);
			this.lblTaxTranspDutyPay.Name = "lblTaxTranspDutyPay";
			this.lblTaxTranspDutyPay.Size = new System.Drawing.Size(88, 16);
			this.lblTaxTranspDutyPay.TabIndex = 16;
			this.lblTaxTranspDutyPay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblCatalogPay
			// 
			this.lblCatalogPay.BackColor = System.Drawing.SystemColors.Control;
			this.lblCatalogPay.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblCatalogPay.Location = new System.Drawing.Point(552, 24);
			this.lblCatalogPay.Name = "lblCatalogPay";
			this.lblCatalogPay.Size = new System.Drawing.Size(88, 16);
			this.lblCatalogPay.TabIndex = 15;
			this.lblCatalogPay.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label16
			// 
			this.label16.Location = new System.Drawing.Point(440, 72);
			this.label16.Name = "label16";
			this.label16.Size = new System.Drawing.Size(112, 16);
			this.label16.TabIndex = 14;
			this.label16.Text = "Grand Total $";
			this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label17
			// 
			this.label17.Location = new System.Drawing.Point(440, 48);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(112, 16);
			this.label17.TabIndex = 13;
			this.label17.Text = "Tax+Transp+Duty $";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label18
			// 
			this.label18.Location = new System.Drawing.Point(440, 24);
			this.label18.Name = "label18";
			this.label18.Size = new System.Drawing.Size(112, 16);
			this.label18.TabIndex = 12;
			this.label18.Text = "Cumulative cost $";
			this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblBackOrder
			// 
			this.lblBackOrder.BackColor = System.Drawing.SystemColors.Control;
			this.lblBackOrder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblBackOrder.Location = new System.Drawing.Point(344, 72);
			this.lblBackOrder.Name = "lblBackOrder";
			this.lblBackOrder.Size = new System.Drawing.Size(88, 16);
			this.lblBackOrder.TabIndex = 11;
			this.lblBackOrder.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblReceivedQty
			// 
			this.lblReceivedQty.BackColor = System.Drawing.SystemColors.Control;
			this.lblReceivedQty.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblReceivedQty.Location = new System.Drawing.Point(344, 48);
			this.lblReceivedQty.Name = "lblReceivedQty";
			this.lblReceivedQty.Size = new System.Drawing.Size(88, 16);
			this.lblReceivedQty.TabIndex = 10;
			this.lblReceivedQty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblReordLevel
			// 
			this.lblReordLevel.BackColor = System.Drawing.SystemColors.Control;
			this.lblReordLevel.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblReordLevel.Location = new System.Drawing.Point(344, 24);
			this.lblReordLevel.Name = "lblReordLevel";
			this.lblReordLevel.Size = new System.Drawing.Size(88, 16);
			this.lblReordLevel.TabIndex = 9;
			this.lblReordLevel.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label10
			// 
			this.label10.Location = new System.Drawing.Point(216, 72);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(128, 16);
			this.label10.TabIndex = 8;
			this.label10.Text = "Back Order, units";
			this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label11
			// 
			this.label11.Location = new System.Drawing.Point(216, 48);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(128, 16);
			this.label11.TabIndex = 7;
			this.label11.Text = "Received Quantity, units";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label12
			// 
			this.label12.Location = new System.Drawing.Point(216, 24);
			this.label12.Name = "label12";
			this.label12.Size = new System.Drawing.Size(128, 16);
			this.label12.TabIndex = 6;
			this.label12.Text = "Reordering Level, units";
			this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblMaxPrice
			// 
			this.lblMaxPrice.BackColor = System.Drawing.SystemColors.Control;
			this.lblMaxPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblMaxPrice.Location = new System.Drawing.Point(120, 72);
			this.lblMaxPrice.Name = "lblMaxPrice";
			this.lblMaxPrice.Size = new System.Drawing.Size(88, 16);
			this.lblMaxPrice.TabIndex = 5;
			this.lblMaxPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblLastPrice
			// 
			this.lblLastPrice.BackColor = System.Drawing.SystemColors.Control;
			this.lblLastPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLastPrice.Location = new System.Drawing.Point(120, 48);
			this.lblLastPrice.Name = "lblLastPrice";
			this.lblLastPrice.Size = new System.Drawing.Size(88, 16);
			this.lblLastPrice.TabIndex = 4;
			this.lblLastPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblMinPrice
			// 
			this.lblMinPrice.BackColor = System.Drawing.SystemColors.Control;
			this.lblMinPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblMinPrice.Location = new System.Drawing.Point(120, 24);
			this.lblMinPrice.Name = "lblMinPrice";
			this.lblMinPrice.Size = new System.Drawing.Size(88, 16);
			this.lblMinPrice.TabIndex = 3;
			this.lblMinPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Location = new System.Drawing.Point(8, 72);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(104, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Maximal Price, $";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(8, 48);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(104, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Last Order Price, $";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(16, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Minimal Price, $";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(416, 344);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(144, 24);
			this.btnClose.TabIndex = 18;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lblSubProductsSC
			// 
			this.lblSubProductsSC.AutoSize = true;
			this.lblSubProductsSC.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSubProductsSC.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSubProductsSC.Location = new System.Drawing.Point(456, 8);
			this.lblSubProductsSC.Name = "lblSubProductsSC";
			this.lblSubProductsSC.Size = new System.Drawing.Size(85, 20);
			this.lblSubProductsSC.TabIndex = 30;
			this.lblSubProductsSC.Text = "Sub-Products";
			this.lblSubProductsSC.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lbxSubProducts
			// 
			this.lbxSubProducts.Location = new System.Drawing.Point(384, 32);
			this.lbxSubProducts.Name = "lbxSubProducts";
			this.lbxSubProducts.Size = new System.Drawing.Size(272, 186);
			this.lbxSubProducts.TabIndex = 29;
			this.lbxSubProducts.SelectedIndexChanged += new System.EventHandler(this.lbxSubProducts_SelectedIndexChanged);
			// 
			// btnHelp
			// 
			this.btnHelp.Location = new System.Drawing.Point(416, 384);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(144, 23);
			this.btnHelp.TabIndex = 31;
			this.btnHelp.Text = "Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// grpDate
			// 
			this.grpDate.Controls.Add(this.lblEndDate);
			this.grpDate.Controls.Add(this.lblStartDate);
			this.grpDate.Controls.Add(this.dtpEnd);
			this.grpDate.Controls.Add(this.dtpStart);
			this.grpDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.grpDate.Location = new System.Drawing.Point(48, 336);
			this.grpDate.Name = "grpDate";
			this.grpDate.Size = new System.Drawing.Size(288, 72);
			this.grpDate.TabIndex = 32;
			this.grpDate.TabStop = false;
			this.grpDate.Text = "Time Periode";
			// 
			// lblEndDate
			// 
			this.lblEndDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblEndDate.Location = new System.Drawing.Point(24, 42);
			this.lblEndDate.Name = "lblEndDate";
			this.lblEndDate.Size = new System.Drawing.Size(88, 16);
			this.lblEndDate.TabIndex = 3;
			this.lblEndDate.Text = "End Date";
			this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblStartDate
			// 
			this.lblStartDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStartDate.Location = new System.Drawing.Point(24, 24);
			this.lblStartDate.Name = "lblStartDate";
			this.lblStartDate.Size = new System.Drawing.Size(88, 16);
			this.lblStartDate.TabIndex = 2;
			this.lblStartDate.Text = "Start Date";
			this.lblStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// dtpEnd
			// 
			this.dtpEnd.Location = new System.Drawing.Point(120, 40);
			this.dtpEnd.Name = "dtpEnd";
			this.dtpEnd.Size = new System.Drawing.Size(152, 22);
			this.dtpEnd.TabIndex = 1;
			this.dtpEnd.CloseUp += new System.EventHandler(this.dtpEnd_CloseUp);
			// 
			// dtpStart
			// 
			this.dtpStart.Location = new System.Drawing.Point(120, 16);
			this.dtpStart.Name = "dtpStart";
			this.dtpStart.Size = new System.Drawing.Size(152, 22);
			this.dtpStart.TabIndex = 0;
			this.dtpStart.Value = new System.DateTime(2005, 1, 1, 0, 0, 0, 0);
			this.dtpStart.CloseUp += new System.EventHandler(this.dtpStart_CloseUp);
			// 
			// fclsOIViewProd
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(672, 430);
			this.Controls.Add(this.grpDate);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.lblSubProductsSC);
			this.Controls.Add(this.grpInfo);
			this.Controls.Add(this.tabSearch);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.lbxSubProducts);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.Name = "fclsOIViewProd";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quick Stock - Cumulative Info per Product";
			this.tabSearch.ResumeLayout(false);
			this.tbpCategory.ResumeLayout(false);
			this.tbpProduct.ResumeLayout(false);
			this.grpInfo.ResumeLayout(false);
			this.grpDate.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","ProdInfo.htm");  
		}
//============================================================================================
		private void cmbCategory_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.cmbCategory.SelectedIndex != -1)
			{
				m_intCategoriesSelectedIndex = this.cmbCategory.SelectedIndex;
				m_intCategoryId = int.Parse(m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["CategoryId"].ToString());
				this.LoadData("Products",m_intCategoryId);
				this.lbxSubProducts.Items.Clear();
				labelClear();
			}		
		}

		private void cmbProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.cmbProducts.SelectedIndex != -1)
			{
				m_intProductsSelectedIndex = this.cmbProducts.SelectedIndex;
				m_intProductId = int.Parse(m_dtaCmbProducts.Rows[m_intProductsSelectedIndex]["MatId"].ToString());
				this.LoadData("Sub-Products",m_intProductId);
				m_intCategoryId = int.Parse(m_dtaCmbProducts.Rows[m_intProductsSelectedIndex]["CategoryId"].ToString());
				this.lblCategory.Text = categName();
				labelClear();
			}		
		}

		private string categName()
		{
			string	Name = "";
			int		categoryId = 0;

			for(int i=0; i < m_dtaCategories.Rows.Count; i++)
			{
				categoryId = int.Parse(m_dtaCategories.Rows[i]["CategoryId"].ToString());
				if(categoryId == m_intCategoryId)
				{
					Name = m_dtaCategories.Rows[i]["CategName"].ToString();
					return Name;
				}
			}

			return Name;
		}
//============================================================================================
		private void lbxProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.lbxProducts.SelectedIndex != -1)
			{
				m_intProductsSelectedIndex = this.lbxProducts.SelectedIndex;
				m_intProductId = int.Parse(m_dtaProducts.Rows[this.lbxProducts.SelectedIndex]["MatId"].ToString());
				this.LoadData("Sub-Products",m_intProductId);
				labelClear();
			}		
		}

		private void labelClear()
		{
			this.lblMinPrice.Text = "";
			this.lblLastPrice.Text = "";
			this.lblMaxPrice.Text = "";
			this.lblReordLevel.Text = "";
			this.lblReceivedQty.Text = "";
			this.lblBackOrder.Text = "";
			this.lblCatalogPay.Text = "";
			this.lblTaxTranspDutyPay.Text = "";
			this.lblTotalPay.Text = "";
		}
//============================================================================================
		private void lbxSubProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.Update_Data();		
		}
//============================================================================================
/*		private string backOrder()
		{
			float nBackOrder = 0;

			DataTable			m_dtaBackOrder = new DataTable();
			OleDbDataAdapter	m_odaBackOrder = new OleDbDataAdapter("SELECT * FROM Orders WHERE SubPrId=" + m_intSubProductId + " AND BackOrderUnits > 0",m_odcConnection);
			m_odaBackOrder.Fill(m_dtaBackOrder);
			
			nBackOrder = 0;
			int nrSubPr = m_dtaBackOrder.Rows.Count;
			if(nrSubPr > 0)
				for(int i=0; i<nrSubPr; i++)
					nBackOrder += float.Parse(m_dtaBackOrder.Rows[i]["BackOrderUnits"].ToString());

			return nBackOrder.ToString();
		}*/
//============================================================================================
		// Loads data into the ListBoxes depending on the parameters received
		private void LoadData(string strLevel, int intId)
		{
			switch(strLevel)
			{
				case "Products":
					m_dtaProducts = new DataTable();
					this.lbxProducts.Items.Clear();
					m_odaProducts = new OleDbDataAdapter("SELECT * FROM Products WHERE (((Products.CategoryId)=" + intId + ")) ORDER BY Products.MatName",m_odcConnection);
					OleDbCommandBuilder ocbProducts = new OleDbCommandBuilder(m_odaProducts);
					m_odaProducts.Fill(m_dtaProducts);
					for(int i=0 ; i < m_dtaProducts.Rows.Count; i++)
					{
						this.lbxProducts.Items.Add(m_dtaProducts.Rows[i]["MatName"].ToString());
					}
					break;

				case "Sub-Products":
					m_dtaSubProducts = new DataTable();
					this.lbxSubProducts.Items.Clear();
					m_odaSubProducts = new OleDbDataAdapter("SELECT * FROM SubProducts WHERE SubProducts.MatId=" + intId + " ORDER BY SubProducts.MatName",m_odcConnection);
					OleDbCommandBuilder ocbSubProducts = new OleDbCommandBuilder(m_odaSubProducts);
					m_odaSubProducts.Fill(m_dtaSubProducts);
					for(int i=0; i < m_dtaSubProducts.Rows.Count; i++)
					{
						this.lbxSubProducts.Items.Add(m_dtaSubProducts.Rows[i]["MatName"].ToString());
					}
					break;
			}
		}
//============================================================================================
		private void dtpStart_CloseUp(object sender, System.EventArgs e)
		{
			this.Update_Data();
		}

		private void dtpEnd_CloseUp(object sender, System.EventArgs e)
		{
			this.Update_Data();
		}

		private void Update_Data()			
		{
            CultureInfo ciCurrentCulture;
			double	m_dblMan = 0.0;
			int		m_intNrProd = 0;
			string	strQueryStartDate, strQueryEndDate;

            // Variable initialization
            ciCurrentCulture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ciCurrentCulture.DateTimeFormat.DateSeparator = "/";

            strQueryStartDate = this.dtpStart.Value.ToString(clsUtilities.FORMAT_DATE_QUERY, ciCurrentCulture);
            strQueryEndDate = this.dtpEnd.Value.ToString(clsUtilities.FORMAT_DATE_QUERY, ciCurrentCulture);

			if(this.lbxSubProducts.SelectedIndex != -1)
			{
				m_intSubProductsSelectedIndex = this.lbxSubProducts.SelectedIndex;
				m_intSubProductId = int.Parse(m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["SubPrId"].ToString());
				if(m_intSubProductId == -1)
					return;
				m_dblMan = double.Parse(m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["Reorder"].ToString());
				this.lblReordLevel.Text = m_dblMan.ToString("#0.00");
				m_dblMan = double.Parse(m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["Prix"].ToString());
				this.lblLastPrice.Text = m_dblMan.ToString("#,##0.00");

				string m_strSubPrId = m_intSubProductId.ToString();
				m_odaOrders = new OleDbDataAdapter("SELECT * FROM Orders " +
					"WHERE (((Orders.OrderDate) BETWEEN #" + strQueryStartDate + "# AND #" + strQueryEndDate + "#)) AND " +
					"(Orders.SubPrId="+ m_intSubProductId + ")", m_odcConnection);
				m_dtaOrders = new DataTable("Orders");
				try
				{
					m_odaOrders.Fill(m_dtaOrders);
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message);
				}
				initValues();
				m_intNrProd = m_dtaOrders.Rows.Count;
				if(m_intNrProd > 0)
					for(int i=0; i<m_intNrProd; i++)
					{
						m_dblMan = double.Parse(m_dtaOrders.Rows[i]["Prix"].ToString());
						if((m_dblMan > 0) && (m_dblMan > m_dblMaxPrice))
							m_dblMaxPrice = m_dblMan;
						if((m_dblMan > 0) && (m_dblMan < m_dblMinPrice))
							m_dblMinPrice = m_dblMan;

						m_dblRecQty += double.Parse(m_dtaOrders.Rows[i]["ReceivedQty"].ToString());
						m_dblBackOrder += double.Parse(m_dtaOrders.Rows[i]["BackOrderUnits"].ToString());
						m_dblCatPay += double.Parse(m_dtaOrders.Rows[i]["CatalogPay"].ToString());
						m_dblTxTrDu += double.Parse(m_dtaOrders.Rows[i]["Tax"].ToString());
						m_dblTxTrDu += double.Parse(m_dtaOrders.Rows[i]["Transport"].ToString());
						m_dblTxTrDu += double.Parse(m_dtaOrders.Rows[i]["Duty"].ToString());
						m_dblTotPay += double.Parse(m_dtaOrders.Rows[i]["TotalPay"].ToString());
					}
				this.lblMinPrice.Text = m_dblMinPrice.ToString("#,##0.00");
				this.lblMaxPrice.Text = m_dblMaxPrice.ToString("#,##0.00");
				this.lblReceivedQty.Text = m_dblRecQty.ToString("#0");
				this.lblBackOrder.Text = m_dblBackOrder.ToString("#0");
				this.lblCatalogPay.Text = m_dblCatPay.ToString("#,##0.00");
				this.lblTaxTranspDutyPay.Text = m_dblTxTrDu.ToString("#,##0.00");
				this.lblTotalPay.Text = m_dblTotPay.ToString("#,##0.00");

			}		
		}

		private void initValues()
		{
			m_dblMinPrice =		0.0;
			m_dblMaxPrice =		0.0;
			m_dblRecQty =		0.0;
			m_dblBackOrder =	0.0;
			m_dblCatPay =		0.0;
			m_dblTxTrDu =		0.0;
			m_dblTotPay =		0.0;
		}
	}
}
