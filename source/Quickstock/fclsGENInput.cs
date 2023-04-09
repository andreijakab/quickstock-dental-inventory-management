using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for frmInput.
	/// </summary>
	public class fclsGENInput : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnProdByCateg;
		private System.Windows.Forms.Button btnEmployeeList;
		private System.Windows.Forms.Button btnSupplierList;
		private System.Windows.Forms.Button btnViewOrder;
		private System.Windows.Forms.Button btnEmergencyOrder;
		private System.Windows.Forms.Button btnCheckBackOrder;
		private System.Windows.Forms.Button btnCheckSupplDelivery;
		private System.Windows.Forms.Button btnStandByOrder;
		private System.Windows.Forms.Label lblMyComp;
		private System.Windows.Forms.Label lblToday;
		public System.Windows.Forms.Button btnIndexAlphabetic;
		private System.Windows.Forms.Button btnAddModDelSuppl;
		private System.Windows.Forms.Button btnAddModDelEmpl;
		private System.Windows.Forms.Button btnAddModDelProd;
		private System.Windows.Forms.Button cmdOptions;
		private System.Windows.Forms.Button btnModifyTrademarks;
		private System.Windows.Forms.Button btnExit;
		private System.Windows.Forms.Panel panel1;
		private System.ComponentModel.IContainer components;
		private System.Windows.Forms.Button btnOldOrders;
		private System.Windows.Forms.Button btnAccounting;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnCanceledBo;
		private System.Windows.Forms.Button btnExpStatistics;
		private System.Windows.Forms.Button btnReturnedProd;
		private System.Windows.Forms.Button btnProdInfo;
		private System.Windows.Forms.Button btnReprintResendOrder;
		public System.Windows.Forms.Label lblOrderToPay;
		public System.Windows.Forms.Label lblSentOrders;
		public System.Windows.Forms.Label lblBackOrders;
		public System.Windows.Forms.Label lblReturnedProducts;
		private System.Windows.Forms.GroupBox grpOrderManagement;
		private System.Windows.Forms.GroupBox grpReports;
		private System.Windows.Forms.GroupBox grpOperationalStatistics;
		private System.Windows.Forms.GroupBox grpDatabaseManagement;
		public System.Windows.Forms.Timer BlinkTimer;
		
		public int m_intBlinkinterval = 1000;

		public static int categId;
		public static int prodId;
		public static int subProdId;
		public static string subProdName;
		public static int supplId;
		public static string supplEmail;
		public static int emplId;
		public static string doiEmail;
		public static int markId;
		public static int indRemindMe;		// 0 for sent Orders	1 for Payment	2 for Backorders	 
		public static int indPayFrom;		// 0 from Check Orders	1 from Accounting
		// 2 from Backorders	3 from OldOrders
		public static int indexLine;
		public static string orderId;
		public static string orderDate;
		public static int oldYearComp = int.Parse(DateTime.Now.ToString("yyyy"));
		public static int oldYear = oldYearComp;
		public static int m_intOldYear = oldYear;
		public static int m_intStatisticYear = oldYear;
		
		

        private static fclsGENSplashScreen2 m_frmGENSplashScreen2;
        private static OleDbConnection      m_odcDatabaseConnection;

        private const double COL_PROPORTION = 0.4498;
        private const double ROW_PROPORTION = 0.3107;
        private const int mc_intMinAmountOfSplashTime_ms = 5000;
        private const int mc_intSplashUpdateInterval_ms = 22;

        private bool    m_blnAccessAllowed;
        private int     m_intLanguageID;
        private string  m_strApplicationTitle, m_strComannyName;

		public fclsGENInput()
		{
			InitializeComponent();
			
			m_blnAccessAllowed = false;

            // retrieve product & company name
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Reflection.AssemblyProductAttribute apaProductTitle = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyProductAttribute), false)[0] as System.Reflection.AssemblyProductAttribute;
            System.Reflection.AssemblyCompanyAttribute acaCompany = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyCompanyAttribute), false)[0] as System.Reflection.AssemblyCompanyAttribute;
            m_strApplicationTitle = apaProductTitle.Product;
            m_strComannyName = acaCompany.Company;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.grpReports = new System.Windows.Forms.GroupBox();
            this.btnSupplierList = new System.Windows.Forms.Button();
            this.btnEmployeeList = new System.Windows.Forms.Button();
            this.btnProdByCateg = new System.Windows.Forms.Button();
            this.btnIndexAlphabetic = new System.Windows.Forms.Button();
            this.grpOperationalStatistics = new System.Windows.Forms.GroupBox();
            this.btnExpStatistics = new System.Windows.Forms.Button();
            this.btnCanceledBo = new System.Windows.Forms.Button();
            this.btnAccounting = new System.Windows.Forms.Button();
            this.btnProdInfo = new System.Windows.Forms.Button();
            this.btnViewOrder = new System.Windows.Forms.Button();
            this.btnReturnedProd = new System.Windows.Forms.Button();
            this.grpDatabaseManagement = new System.Windows.Forms.GroupBox();
            this.btnModifyTrademarks = new System.Windows.Forms.Button();
            this.btnAddModDelEmpl = new System.Windows.Forms.Button();
            this.btnAddModDelSuppl = new System.Windows.Forms.Button();
            this.btnAddModDelProd = new System.Windows.Forms.Button();
            this.grpOrderManagement = new System.Windows.Forms.GroupBox();
            this.btnReprintResendOrder = new System.Windows.Forms.Button();
            this.btnCheckBackOrder = new System.Windows.Forms.Button();
            this.btnCheckSupplDelivery = new System.Windows.Forms.Button();
            this.btnEmergencyOrder = new System.Windows.Forms.Button();
            this.btnStandByOrder = new System.Windows.Forms.Button();
            this.lblMyComp = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnOldOrders = new System.Windows.Forms.Button();
            this.cmdOptions = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.lblToday = new System.Windows.Forms.Label();
            this.BlinkTimer = new System.Windows.Forms.Timer(this.components);
            this.lblOrderToPay = new System.Windows.Forms.Label();
            this.lblSentOrders = new System.Windows.Forms.Label();
            this.lblBackOrders = new System.Windows.Forms.Label();
            this.lblReturnedProducts = new System.Windows.Forms.Label();
            this.grpReports.SuspendLayout();
            this.grpOperationalStatistics.SuspendLayout();
            this.grpDatabaseManagement.SuspendLayout();
            this.grpOrderManagement.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpReports
            // 
            this.grpReports.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.grpReports.BackColor = System.Drawing.Color.AliceBlue;
            this.grpReports.Controls.Add(this.btnSupplierList);
            this.grpReports.Controls.Add(this.btnEmployeeList);
            this.grpReports.Controls.Add(this.btnProdByCateg);
            this.grpReports.Controls.Add(this.btnIndexAlphabetic);
            this.grpReports.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpReports.ForeColor = System.Drawing.Color.Blue;
            this.grpReports.Location = new System.Drawing.Point(480, 312);
            this.grpReports.Name = "grpReports";
            this.grpReports.Size = new System.Drawing.Size(439, 192);
            this.grpReports.TabIndex = 14;
            this.grpReports.TabStop = false;
            this.grpReports.Text = "REPORTS";
            // 
            // btnSupplierList
            // 
            this.btnSupplierList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnSupplierList.BackColor = System.Drawing.Color.Azure;
            this.btnSupplierList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSupplierList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSupplierList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSupplierList.Location = new System.Drawing.Point(219, 103);
            this.btnSupplierList.Name = "btnSupplierList";
            this.btnSupplierList.Size = new System.Drawing.Size(200, 72);
            this.btnSupplierList.TabIndex = 3;
            this.btnSupplierList.Text = "Dental Clinic Supplier List ";
            this.btnSupplierList.UseVisualStyleBackColor = false;
            this.btnSupplierList.Click += new System.EventHandler(this.btnSupplierList_Click);
            // 
            // btnEmployeeList
            // 
            this.btnEmployeeList.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEmployeeList.BackColor = System.Drawing.Color.Azure;
            this.btnEmployeeList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmployeeList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmployeeList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmployeeList.Location = new System.Drawing.Point(19, 103);
            this.btnEmployeeList.Name = "btnEmployeeList";
            this.btnEmployeeList.Size = new System.Drawing.Size(200, 72);
            this.btnEmployeeList.TabIndex = 2;
            this.btnEmployeeList.Text = "Dental Clinic Associate/Employee List ";
            this.btnEmployeeList.UseVisualStyleBackColor = false;
            this.btnEmployeeList.Click += new System.EventHandler(this.btnEmployeeList_Click);
            // 
            // btnProdByCateg
            // 
            this.btnProdByCateg.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnProdByCateg.BackColor = System.Drawing.Color.Azure;
            this.btnProdByCateg.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProdByCateg.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProdByCateg.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProdByCateg.Location = new System.Drawing.Point(219, 31);
            this.btnProdByCateg.Name = "btnProdByCateg";
            this.btnProdByCateg.Size = new System.Drawing.Size(200, 72);
            this.btnProdByCateg.TabIndex = 4;
            this.btnProdByCateg.Text = "Dental Supply Index by Category ";
            this.btnProdByCateg.UseVisualStyleBackColor = false;
            this.btnProdByCateg.Click += new System.EventHandler(this.btnProdByCateg_Click);
            // 
            // btnIndexAlphabetic
            // 
            this.btnIndexAlphabetic.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnIndexAlphabetic.BackColor = System.Drawing.Color.Azure;
            this.btnIndexAlphabetic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnIndexAlphabetic.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnIndexAlphabetic.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnIndexAlphabetic.Location = new System.Drawing.Point(19, 31);
            this.btnIndexAlphabetic.Name = "btnIndexAlphabetic";
            this.btnIndexAlphabetic.Size = new System.Drawing.Size(200, 72);
            this.btnIndexAlphabetic.TabIndex = 0;
            this.btnIndexAlphabetic.Text = "Dental Supply Alphabetical Index ";
            this.btnIndexAlphabetic.UseVisualStyleBackColor = false;
            this.btnIndexAlphabetic.Click += new System.EventHandler(this.btnIndexAlphabetic_Click);
            // 
            // grpOperationalStatistics
            // 
            this.grpOperationalStatistics.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.grpOperationalStatistics.BackColor = System.Drawing.Color.Ivory;
            this.grpOperationalStatistics.Controls.Add(this.btnExpStatistics);
            this.grpOperationalStatistics.Controls.Add(this.btnCanceledBo);
            this.grpOperationalStatistics.Controls.Add(this.btnAccounting);
            this.grpOperationalStatistics.Controls.Add(this.btnProdInfo);
            this.grpOperationalStatistics.Controls.Add(this.btnViewOrder);
            this.grpOperationalStatistics.Controls.Add(this.btnReturnedProd);
            this.grpOperationalStatistics.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOperationalStatistics.ForeColor = System.Drawing.Color.Red;
            this.grpOperationalStatistics.Location = new System.Drawing.Point(40, 312);
            this.grpOperationalStatistics.Name = "grpOperationalStatistics";
            this.grpOperationalStatistics.Size = new System.Drawing.Size(439, 192);
            this.grpOperationalStatistics.TabIndex = 13;
            this.grpOperationalStatistics.TabStop = false;
            this.grpOperationalStatistics.Text = "PRODUCT MANAGEMENT / OPERATIONAL STATISTICS";
            // 
            // btnExpStatistics
            // 
            this.btnExpStatistics.BackColor = System.Drawing.Color.LightYellow;
            this.btnExpStatistics.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExpStatistics.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExpStatistics.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExpStatistics.Location = new System.Drawing.Point(160, 31);
            this.btnExpStatistics.Name = "btnExpStatistics";
            this.btnExpStatistics.Size = new System.Drawing.Size(132, 72);
            this.btnExpStatistics.TabIndex = 24;
            this.btnExpStatistics.Text = "Supply Statistics";
            this.btnExpStatistics.UseVisualStyleBackColor = false;
            this.btnExpStatistics.Click += new System.EventHandler(this.btnExpStatistics_Click);
            // 
            // btnCanceledBo
            // 
            this.btnCanceledBo.BackColor = System.Drawing.Color.LightYellow;
            this.btnCanceledBo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCanceledBo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCanceledBo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCanceledBo.Location = new System.Drawing.Point(160, 103);
            this.btnCanceledBo.Name = "btnCanceledBo";
            this.btnCanceledBo.Size = new System.Drawing.Size(132, 72);
            this.btnCanceledBo.TabIndex = 23;
            this.btnCanceledBo.Text = "Canceled Backorders";
            this.btnCanceledBo.UseVisualStyleBackColor = false;
            this.btnCanceledBo.Click += new System.EventHandler(this.btnCanceledBo_Click);
            // 
            // btnAccounting
            // 
            this.btnAccounting.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAccounting.BackColor = System.Drawing.Color.LightYellow;
            this.btnAccounting.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAccounting.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAccounting.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAccounting.Location = new System.Drawing.Point(292, 31);
            this.btnAccounting.Name = "btnAccounting";
            this.btnAccounting.Size = new System.Drawing.Size(132, 72);
            this.btnAccounting.TabIndex = 22;
            this.btnAccounting.Text = "Expenses Management";
            this.btnAccounting.UseVisualStyleBackColor = false;
            this.btnAccounting.Click += new System.EventHandler(this.btnAccounting_Click);
            // 
            // btnProdInfo
            // 
            this.btnProdInfo.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnProdInfo.BackColor = System.Drawing.Color.LightYellow;
            this.btnProdInfo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnProdInfo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnProdInfo.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnProdInfo.ForeColor = System.Drawing.Color.Red;
            this.btnProdInfo.Location = new System.Drawing.Point(24, 31);
            this.btnProdInfo.Name = "btnProdInfo";
            this.btnProdInfo.Size = new System.Drawing.Size(136, 72);
            this.btnProdInfo.TabIndex = 3;
            this.btnProdInfo.Text = "Product Info";
            this.btnProdInfo.UseVisualStyleBackColor = false;
            this.btnProdInfo.Click += new System.EventHandler(this.btnProdInfo_Click);
            // 
            // btnViewOrder
            // 
            this.btnViewOrder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnViewOrder.BackColor = System.Drawing.Color.LightYellow;
            this.btnViewOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnViewOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnViewOrder.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnViewOrder.ForeColor = System.Drawing.Color.Red;
            this.btnViewOrder.Location = new System.Drawing.Point(24, 103);
            this.btnViewOrder.Name = "btnViewOrder";
            this.btnViewOrder.Size = new System.Drawing.Size(136, 72);
            this.btnViewOrder.TabIndex = 2;
            this.btnViewOrder.Text = "Received Orders ";
            this.btnViewOrder.UseVisualStyleBackColor = false;
            this.btnViewOrder.Click += new System.EventHandler(this.btnViewOrder_Click);
            // 
            // btnReturnedProd
            // 
            this.btnReturnedProd.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnReturnedProd.BackColor = System.Drawing.Color.LightYellow;
            this.btnReturnedProd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReturnedProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReturnedProd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReturnedProd.ForeColor = System.Drawing.Color.Red;
            this.btnReturnedProd.Location = new System.Drawing.Point(292, 103);
            this.btnReturnedProd.Name = "btnReturnedProd";
            this.btnReturnedProd.Size = new System.Drawing.Size(132, 72);
            this.btnReturnedProd.TabIndex = 1;
            this.btnReturnedProd.Text = "Returned Products";
            this.btnReturnedProd.UseVisualStyleBackColor = false;
            this.btnReturnedProd.Click += new System.EventHandler(this.btnReturnedProd_Click);
            // 
            // grpDatabaseManagement
            // 
            this.grpDatabaseManagement.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.grpDatabaseManagement.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(243)))), ((int)(((byte)(244)))));
            this.grpDatabaseManagement.Controls.Add(this.btnModifyTrademarks);
            this.grpDatabaseManagement.Controls.Add(this.btnAddModDelEmpl);
            this.grpDatabaseManagement.Controls.Add(this.btnAddModDelSuppl);
            this.grpDatabaseManagement.Controls.Add(this.btnAddModDelProd);
            this.grpDatabaseManagement.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDatabaseManagement.ForeColor = System.Drawing.Color.Blue;
            this.grpDatabaseManagement.Location = new System.Drawing.Point(480, 120);
            this.grpDatabaseManagement.Name = "grpDatabaseManagement";
            this.grpDatabaseManagement.Size = new System.Drawing.Size(439, 192);
            this.grpDatabaseManagement.TabIndex = 12;
            this.grpDatabaseManagement.TabStop = false;
            this.grpDatabaseManagement.Text = "DATABASE MANAGEMENT";
            // 
            // btnModifyTrademarks
            // 
            this.btnModifyTrademarks.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnModifyTrademarks.BackColor = System.Drawing.Color.Azure;
            this.btnModifyTrademarks.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnModifyTrademarks.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnModifyTrademarks.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnModifyTrademarks.ForeColor = System.Drawing.Color.Blue;
            this.btnModifyTrademarks.Location = new System.Drawing.Point(19, 103);
            this.btnModifyTrademarks.Name = "btnModifyTrademarks";
            this.btnModifyTrademarks.Size = new System.Drawing.Size(200, 73);
            this.btnModifyTrademarks.TabIndex = 4;
            this.btnModifyTrademarks.Text = "Trademarks";
            this.btnModifyTrademarks.UseVisualStyleBackColor = false;
            this.btnModifyTrademarks.Click += new System.EventHandler(this.btnModifyTrademarks_Click);
            // 
            // btnAddModDelEmpl
            // 
            this.btnAddModDelEmpl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddModDelEmpl.BackColor = System.Drawing.Color.Azure;
            this.btnAddModDelEmpl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddModDelEmpl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddModDelEmpl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddModDelEmpl.Location = new System.Drawing.Point(219, 103);
            this.btnAddModDelEmpl.Name = "btnAddModDelEmpl";
            this.btnAddModDelEmpl.Size = new System.Drawing.Size(200, 73);
            this.btnAddModDelEmpl.TabIndex = 3;
            this.btnAddModDelEmpl.Text = "Associates / Employees";
            this.btnAddModDelEmpl.UseVisualStyleBackColor = false;
            this.btnAddModDelEmpl.Click += new System.EventHandler(this.btnAddModDelEmpl_Click);
            // 
            // btnAddModDelSuppl
            // 
            this.btnAddModDelSuppl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddModDelSuppl.BackColor = System.Drawing.Color.Azure;
            this.btnAddModDelSuppl.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddModDelSuppl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddModDelSuppl.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddModDelSuppl.Location = new System.Drawing.Point(219, 31);
            this.btnAddModDelSuppl.Name = "btnAddModDelSuppl";
            this.btnAddModDelSuppl.Size = new System.Drawing.Size(200, 73);
            this.btnAddModDelSuppl.TabIndex = 2;
            this.btnAddModDelSuppl.Text = "Suppliers";
            this.btnAddModDelSuppl.UseVisualStyleBackColor = false;
            this.btnAddModDelSuppl.Click += new System.EventHandler(this.btnAddModDelSuppl_Click);
            // 
            // btnAddModDelProd
            // 
            this.btnAddModDelProd.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddModDelProd.BackColor = System.Drawing.Color.Azure;
            this.btnAddModDelProd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddModDelProd.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddModDelProd.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAddModDelProd.ForeColor = System.Drawing.Color.Blue;
            this.btnAddModDelProd.Location = new System.Drawing.Point(19, 31);
            this.btnAddModDelProd.Name = "btnAddModDelProd";
            this.btnAddModDelProd.Size = new System.Drawing.Size(200, 73);
            this.btnAddModDelProd.TabIndex = 0;
            this.btnAddModDelProd.Text = "Products";
            this.btnAddModDelProd.UseVisualStyleBackColor = false;
            this.btnAddModDelProd.Click += new System.EventHandler(this.btnAddModDelProd_Click);
            // 
            // grpOrderManagement
            // 
            this.grpOrderManagement.BackColor = System.Drawing.Color.Cornsilk;
            this.grpOrderManagement.Controls.Add(this.btnReprintResendOrder);
            this.grpOrderManagement.Controls.Add(this.btnCheckBackOrder);
            this.grpOrderManagement.Controls.Add(this.btnCheckSupplDelivery);
            this.grpOrderManagement.Controls.Add(this.btnEmergencyOrder);
            this.grpOrderManagement.Controls.Add(this.btnStandByOrder);
            this.grpOrderManagement.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpOrderManagement.ForeColor = System.Drawing.Color.Red;
            this.grpOrderManagement.Location = new System.Drawing.Point(40, 120);
            this.grpOrderManagement.Name = "grpOrderManagement";
            this.grpOrderManagement.Size = new System.Drawing.Size(439, 192);
            this.grpOrderManagement.TabIndex = 11;
            this.grpOrderManagement.TabStop = false;
            this.grpOrderManagement.Text = "ORDER MANAGEMENT";
            // 
            // btnReprintResendOrder
            // 
            this.btnReprintResendOrder.BackColor = System.Drawing.Color.LightYellow;
            this.btnReprintResendOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnReprintResendOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnReprintResendOrder.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnReprintResendOrder.Location = new System.Drawing.Point(24, 96);
            this.btnReprintResendOrder.Name = "btnReprintResendOrder";
            this.btnReprintResendOrder.Size = new System.Drawing.Size(400, 24);
            this.btnReprintResendOrder.TabIndex = 14;
            this.btnReprintResendOrder.Text = "Resend Order";
            this.btnReprintResendOrder.UseVisualStyleBackColor = false;
            this.btnReprintResendOrder.Click += new System.EventHandler(this.btnReprintResendOrder_Click);
            // 
            // btnCheckBackOrder
            // 
            this.btnCheckBackOrder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCheckBackOrder.BackColor = System.Drawing.Color.LightYellow;
            this.btnCheckBackOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckBackOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckBackOrder.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckBackOrder.ForeColor = System.Drawing.Color.Red;
            this.btnCheckBackOrder.Location = new System.Drawing.Point(224, 120);
            this.btnCheckBackOrder.Name = "btnCheckBackOrder";
            this.btnCheckBackOrder.Size = new System.Drawing.Size(200, 56);
            this.btnCheckBackOrder.TabIndex = 3;
            this.btnCheckBackOrder.Text = "Backorders";
            this.btnCheckBackOrder.UseVisualStyleBackColor = false;
            this.btnCheckBackOrder.Click += new System.EventHandler(this.btnCheckBackOrder_Click);
            // 
            // btnCheckSupplDelivery
            // 
            this.btnCheckSupplDelivery.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCheckSupplDelivery.BackColor = System.Drawing.Color.LightYellow;
            this.btnCheckSupplDelivery.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCheckSupplDelivery.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCheckSupplDelivery.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCheckSupplDelivery.ForeColor = System.Drawing.Color.Red;
            this.btnCheckSupplDelivery.Location = new System.Drawing.Point(24, 120);
            this.btnCheckSupplDelivery.Name = "btnCheckSupplDelivery";
            this.btnCheckSupplDelivery.Size = new System.Drawing.Size(200, 56);
            this.btnCheckSupplDelivery.TabIndex = 2;
            this.btnCheckSupplDelivery.Text = "Order Check-In";
            this.btnCheckSupplDelivery.UseVisualStyleBackColor = false;
            this.btnCheckSupplDelivery.Click += new System.EventHandler(this.btnCheckSupplDelivery_Click);
            // 
            // btnEmergencyOrder
            // 
            this.btnEmergencyOrder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnEmergencyOrder.BackColor = System.Drawing.Color.LightYellow;
            this.btnEmergencyOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEmergencyOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnEmergencyOrder.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEmergencyOrder.ForeColor = System.Drawing.Color.Red;
            this.btnEmergencyOrder.Location = new System.Drawing.Point(224, 31);
            this.btnEmergencyOrder.Name = "btnEmergencyOrder";
            this.btnEmergencyOrder.Size = new System.Drawing.Size(200, 64);
            this.btnEmergencyOrder.TabIndex = 1;
            this.btnEmergencyOrder.Text = "Express Order ";
            this.btnEmergencyOrder.UseVisualStyleBackColor = false;
            this.btnEmergencyOrder.Click += new System.EventHandler(this.btnEmergencyOrder_Click);
            // 
            // btnStandByOrder
            // 
            this.btnStandByOrder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnStandByOrder.BackColor = System.Drawing.Color.LightYellow;
            this.btnStandByOrder.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnStandByOrder.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnStandByOrder.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnStandByOrder.ForeColor = System.Drawing.Color.Red;
            this.btnStandByOrder.Location = new System.Drawing.Point(24, 31);
            this.btnStandByOrder.Name = "btnStandByOrder";
            this.btnStandByOrder.Size = new System.Drawing.Size(200, 64);
            this.btnStandByOrder.TabIndex = 0;
            this.btnStandByOrder.Text = "Regular Order ";
            this.btnStandByOrder.UseVisualStyleBackColor = false;
            this.btnStandByOrder.Click += new System.EventHandler(this.btnStandByOrder_Click);
            // 
            // lblMyComp
            // 
            this.lblMyComp.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMyComp.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMyComp.ForeColor = System.Drawing.Color.Blue;
            this.lblMyComp.Location = new System.Drawing.Point(304, 72);
            this.lblMyComp.Name = "lblMyComp";
            this.lblMyComp.Size = new System.Drawing.Size(360, 32);
            this.lblMyComp.TabIndex = 0;
            this.lblMyComp.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblMyComp.UseMnemonic = false;
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.Color.LightSteelBlue;
            this.panel1.Controls.Add(this.btnHelp);
            this.panel1.Controls.Add(this.btnOldOrders);
            this.panel1.Controls.Add(this.cmdOptions);
            this.panel1.Controls.Add(this.btnExit);
            this.panel1.Controls.Add(this.lblToday);
            this.panel1.Location = new System.Drawing.Point(40, 544);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(880, 32);
            this.panel1.TabIndex = 18;
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnHelp.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnHelp.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnHelp.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnHelp.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnHelp.Location = new System.Drawing.Point(548, 4);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(128, 24);
            this.btnHelp.TabIndex = 23;
            this.btnHelp.Text = "Help";
            this.btnHelp.UseVisualStyleBackColor = false;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnOldOrders
            // 
            this.btnOldOrders.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnOldOrders.BackColor = System.Drawing.Color.LightSteelBlue;
            this.btnOldOrders.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnOldOrders.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOldOrders.ForeColor = System.Drawing.Color.Black;
            this.btnOldOrders.Location = new System.Drawing.Point(148, 4);
            this.btnOldOrders.Name = "btnOldOrders";
            this.btnOldOrders.Size = new System.Drawing.Size(200, 24);
            this.btnOldOrders.TabIndex = 22;
            this.btnOldOrders.Text = "Past Invoices ";
            this.btnOldOrders.UseVisualStyleBackColor = false;
            this.btnOldOrders.Click += new System.EventHandler(this.btnOldOrders_Click);
            // 
            // cmdOptions
            // 
            this.cmdOptions.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmdOptions.BackColor = System.Drawing.Color.LightSteelBlue;
            this.cmdOptions.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmdOptions.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cmdOptions.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmdOptions.Location = new System.Drawing.Point(4, 4);
            this.cmdOptions.Name = "cmdOptions";
            this.cmdOptions.Size = new System.Drawing.Size(128, 24);
            this.cmdOptions.TabIndex = 13;
            this.cmdOptions.Text = "Options";
            this.cmdOptions.UseVisualStyleBackColor = false;
            this.cmdOptions.Click += new System.EventHandler(this.cmdOptions_Click);
            // 
            // btnExit
            // 
            this.btnExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnExit.BackColor = System.Drawing.Color.Aqua;
            this.btnExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnExit.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExit.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.btnExit.Location = new System.Drawing.Point(360, 4);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(160, 24);
            this.btnExit.TabIndex = 9;
            this.btnExit.Text = "Exit";
            this.btnExit.UseVisualStyleBackColor = false;
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // lblToday
            // 
            this.lblToday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblToday.BackColor = System.Drawing.Color.LightSteelBlue;
            this.lblToday.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblToday.ForeColor = System.Drawing.Color.White;
            this.lblToday.Location = new System.Drawing.Point(696, 4);
            this.lblToday.Name = "lblToday";
            this.lblToday.Size = new System.Drawing.Size(160, 24);
            this.lblToday.TabIndex = 11;
            this.lblToday.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // BlinkTimer
            // 
            this.BlinkTimer.Enabled = true;
            this.BlinkTimer.Interval = 5000;
            this.BlinkTimer.Tick += new System.EventHandler(this.BlinkTimer_Tick);
            // 
            // lblOrderToPay
            // 
            this.lblOrderToPay.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblOrderToPay.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblOrderToPay.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblOrderToPay.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderToPay.ForeColor = System.Drawing.Color.Black;
            this.lblOrderToPay.Location = new System.Drawing.Point(296, 508);
            this.lblOrderToPay.Name = "lblOrderToPay";
            this.lblOrderToPay.Size = new System.Drawing.Size(150, 30);
            this.lblOrderToPay.TabIndex = 19;
            this.lblOrderToPay.Text = "Unpaid Order(s)";
            this.lblOrderToPay.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblOrderToPay.Click += new System.EventHandler(this.lblOrderToPay_Click);
            // 
            // lblSentOrders
            // 
            this.lblSentOrders.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblSentOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblSentOrders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblSentOrders.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSentOrders.ForeColor = System.Drawing.Color.Black;
            this.lblSentOrders.Location = new System.Drawing.Point(88, 508);
            this.lblSentOrders.Name = "lblSentOrders";
            this.lblSentOrders.Size = new System.Drawing.Size(150, 30);
            this.lblSentOrders.TabIndex = 20;
            this.lblSentOrders.Text = "Awaiting Order(s)";
            this.lblSentOrders.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblSentOrders.Click += new System.EventHandler(this.lblSentOrders_Click);
            // 
            // lblBackOrders
            // 
            this.lblBackOrders.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblBackOrders.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblBackOrders.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblBackOrders.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBackOrders.ForeColor = System.Drawing.Color.Black;
            this.lblBackOrders.Location = new System.Drawing.Point(728, 508);
            this.lblBackOrders.Name = "lblBackOrders";
            this.lblBackOrders.Size = new System.Drawing.Size(150, 30);
            this.lblBackOrders.TabIndex = 21;
            this.lblBackOrders.Text = "Backorder(s)";
            this.lblBackOrders.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblBackOrders.Click += new System.EventHandler(this.lblBackOrders_Click);
            // 
            // lblReturnedProducts
            // 
            this.lblReturnedProducts.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.lblReturnedProducts.BackColor = System.Drawing.Color.Silver;
            this.lblReturnedProducts.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblReturnedProducts.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblReturnedProducts.ForeColor = System.Drawing.Color.Black;
            this.lblReturnedProducts.Location = new System.Drawing.Point(512, 508);
            this.lblReturnedProducts.Name = "lblReturnedProducts";
            this.lblReturnedProducts.Size = new System.Drawing.Size(150, 30);
            this.lblReturnedProducts.TabIndex = 22;
            this.lblReturnedProducts.Text = "Returned Product(s) w/o Return Number";
            this.lblReturnedProducts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.lblReturnedProducts.Click += new System.EventHandler(this.lblReturnedProducts_Click);
            // 
            // fclsGENInput
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.BackColor = System.Drawing.Color.Azure;
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(968, 584);
            this.Controls.Add(this.lblReturnedProducts);
            this.Controls.Add(this.lblBackOrders);
            this.Controls.Add(this.lblSentOrders);
            this.Controls.Add(this.lblOrderToPay);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.grpReports);
            this.Controls.Add(this.grpOperationalStatistics);
            this.Controls.Add(this.grpDatabaseManagement);
            this.Controls.Add(this.grpOrderManagement);
            this.Controls.Add(this.lblMyComp);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.Name = "fclsGENInput";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Stock";
            this.Load += new System.EventHandler(this.frmGENInput_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.fclsGENInput_Paint);
            this.Resize += new System.EventHandler(this.fclsGENInput_Resize);
            this.grpReports.ResumeLayout(false);
            this.grpOperationalStatistics.ResumeLayout(false);
            this.grpDatabaseManagement.ResumeLayout(false);
            this.grpOrderManagement.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
        #region AppStartup
		[STAThread]
		static void Main() 
		{
            // enables visual styles for the application.
            Application.EnableVisualStyles();

            // Handle unhandled thread exceptions
            Application.ThreadException += App_ThreadException;

            // Log really bad unhandled exceptions
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(AppDomain_UnhandledExceptionHandler);

            // start application
            Application.Run(new fclsGENInput());
        }

        static void App_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            // Does user want to save or quit?
            DialogResult dlg = MessageBox.Show("An unexpected problem has occued:\r\n\r\n" +
                                               e.Exception.Message + 
                                               "Would you like to continue the application so that you can save your work?",
                                               "QuickStock",
                                               MessageBoxButtons.YesNo,
                                               MessageBoxIcon.Error,
                                               MessageBoxDefaultButton.Button1);

            // If save: returning to continue the application and allow saving
            if( dlg == DialogResult.Yes )
                return;

            // If quit: shut down
            Application.Exit();
        }

        static void AppDomain_UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs args)
        {
            // get exception object
            Exception ex = (Exception)args.ExceptionObject;

            // TODO: only log exception (.NET will show an error message so no need to dispalay a MsgBox)
            
        }
        #endregion
        //============================================================================================
		private void frmGENInput_Load(object sender, System.EventArgs e)
		{
            fclsGENLogin frmLogin;
            fclsLSTViewReport frmLSTViewReport;
            Thread thrSplashThread;
			ToolTip ttToolTip;

            // load splash screen
            thrSplashThread = new Thread(new ThreadStart(StartSplash2));
            thrSplashThread.Start();

            //
            // do initialization work
            //
            // check database
            m_odcDatabaseConnection = new System.Data.OleDb.OleDbConnection();
            m_odcDatabaseConnection.ConnectionString = @String.Concat("Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + Application.StartupPath + "\\DSMS.mdb");//;Password=imre48");
            try
            {
                // connect to database
                m_odcDatabaseConnection.Open();

                // check database integrity
                if (!clsUtilities.CheckDatabaseIntegrity(m_odcDatabaseConnection))
                {
                    MessageBox.Show("Database integrity compromised! Application will now exit.", m_strApplicationTitle + " Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message + "\nApplication will now exit.", m_strApplicationTitle + " Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Application.Exit();
            }

            try
            {
                // initialize configuration object
                clsConfiguration.Initialize(m_odcDatabaseConnection);
            }
            catch (ConfigurationException ex)
            {
                if (ex.Critical)
                {
                    MessageBox.Show(ex.Message,
                                    clsConfiguration.Internal_ApplicationName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                    
                    CloseSplash2();
                    Application.Exit();
                }
                else
                {
                    MessageBox.Show(ex.Message,
                                    clsConfiguration.Internal_ApplicationName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Warning);
                }
            }

            // check activation
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            clsActivation2 objActivation = new clsActivation2(clsConfiguration.Internal_ApplicationName,
                                                              assembly.GetName().Version.Major.ToString() + "." + assembly.GetName().Version.Minor.ToString(),
                                                              clsConfiguration.Internal_ConfigurationFilesPath);
            if (!objActivation.IsLicenseValid())
            {
                fclsGENActivation frmGENActivation = new fclsGENActivation(objActivation, m_odcDatabaseConnection);
                if (frmGENActivation.ShowDialog() != DialogResult.OK)
                {
                    CloseSplash2();
                    Application.Exit();
                }
            }

            // if a password has been set display login form
            if (clsConfiguration.Security_EmployeeLoginRequired)
            {
                frmLogin = new fclsGENLogin(m_odcDatabaseConnection);
                frmLogin.Owner = this;
                if (frmLogin.ShowDialog() != DialogResult.OK)
                {
                    CloseSplash2();
                    Application.Exit();
                }
            }

            // initialize crystal reports
            frmLSTViewReport = new fclsLSTViewReport();
            frmLSTViewReport.typeReport = "btnEmployeeList";
            frmLSTViewReport.ShowInTaskbar = false;
            frmLSTViewReport.WindowState = FormWindowState.Minimized;
            frmLSTViewReport.Show();
            frmLSTViewReport.Close();

            // prepare misc. form items
            this.lblToday.Text = DateTime.Now.ToString(clsUtilities.FORMAT_DATE_DISPLAY);
            this.LoadDentalOfficeInfo();
            this.ConfigureReminderLabels();
                        			
			// initialize and set up the ToolTips
            ttToolTip = new ToolTip();
			ttToolTip.AutoPopDelay = 5000;
			ttToolTip.InitialDelay = 1000;
			ttToolTip.ReshowDelay = 500;
			ttToolTip.ShowAlways = true;			// Force the ToolTip text to be displayed whether or not the form is active.

			ttToolTip.SetToolTip(this.btnEmergencyOrder, "Send an Emergency Order");
			ttToolTip.SetToolTip(this.btnStandByOrder, "Make a Stand-by list to prepare a New Order");
			ttToolTip.SetToolTip(this.btnViewOrder, "View and reprint the received orders");
			ttToolTip.SetToolTip(this.btnIndexAlphabetic, "Index alphabetic of the Dental Supplies");
			ttToolTip.SetToolTip(this.btnSupplierList, "List of the Suppliers");
			ttToolTip.SetToolTip(this.btnEmployeeList, "List of the Associetes and of the Employees");
			ttToolTip.SetToolTip(this.btnProdByCateg, "List per Categories of the Dental Supplies");
			ttToolTip.SetToolTip(this.btnAddModDelEmpl, "Add, Modify or Delete information\nabout an Associete or an Employee.");
			ttToolTip.SetToolTip(this.btnAddModDelSuppl, "Add, Modify or Delete information about a Supplier");
			ttToolTip.SetToolTip(this.btnAddModDelProd, "Add, Modify or Delete Categories, Products or Subproducts.");
			ttToolTip.SetToolTip(this.btnModifyTrademarks, "Add, Modify or Delete a Trademark");
			ttToolTip.SetToolTip(this.btnExit, "Close the Database and Quit the Programm");
			ttToolTip.SetToolTip(this.btnAccounting, "Expenses mangement and Payement Update\nafter orders receive");
			ttToolTip.SetToolTip(this.btnReturnedProd, "View information about the returned products");
			ttToolTip.SetToolTip(this.btnCanceledBo, "View information about the canceled backorders");
			ttToolTip.SetToolTip(this.btnProdInfo, "Received products info");
			ttToolTip.SetToolTip(this.btnExpStatistics, "Expenses statistics by numbers and graphics\nfor the received orders");
			ttToolTip.SetToolTip(this.btnCheckSupplDelivery, "Verify orders on arrival and update prices, if needed");
			ttToolTip.SetToolTip(this.btnCheckBackOrder, "Verify the state of the backorders");
			ttToolTip.SetToolTip(this.lblOrderToPay, "Attention! There are some order to pay");
			ttToolTip.SetToolTip(this.lblSentOrders, "Attention! There are sent order");
			ttToolTip.SetToolTip(this.lblBackOrders, "Attention! There are backorders");
			ttToolTip.SetToolTip(this.lblReturnedProducts, "Attention! There are Returned Products without Return Number");
			ttToolTip.SetToolTip(this.cmdOptions, "Set the passwords and the default values");
			ttToolTip.SetToolTip(this.btnOldOrders, "Add the past year orders to the database");
			ttToolTip.SetToolTip(this.btnHelp, "Display HELP for user");

            // sit and spin while we wait for the minimum timer interval if
            // the interval has not already passed
            if (m_frmGENSplashScreen2 != null)
            {
                while (m_frmGENSplashScreen2.GetUpMilliseconds() < mc_intMinAmountOfSplashTime_ms)
                {
                    Thread.Sleep(mc_intSplashUpdateInterval_ms / 4);
                }
            }

            // Close the splash screen
            CloseSplash2();
			
			// close splash screen and activate this form
            //if(fclsGENSplashScreen.SplashForm != null )
            //    fclsGENSplashScreen.SplashForm.Owner = this;
            //this.Activate();
            //fclsGENSplashScreen.CloseForm();
		}

		private void fclsGENInput_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
		{
			Graphics  gr_Title = this.CreateGraphics();
			Font gr_Font;

			gr_Font = new System.Drawing.Font("Arial", 26, FontStyle.Bold);
			gr_Title.DrawString("DENTAL SUPPLY MANAGEMENT SYSTEM", gr_Font, System.Drawing.Brushes.Gray,126,29);
			gr_Title.DrawString("DENTAL SUPPLY MANAGEMENT SYSTEM", gr_Font, System.Drawing.Brushes.LightSteelBlue,124,26);
		}

		private void btnExit_Click(object sender, System.EventArgs e)
		{
            Application.Exit();
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm");
		}

		private void btnEmergencyOrder_Click(object sender, System.EventArgs e)
		{
			fclsOMEmergencyOrder frmOMEmergencyOrder = new fclsOMEmergencyOrder(m_odcDatabaseConnection);
			frmOMEmergencyOrder.ShowDialog();
		}

		private void btnStandByOrder_Click(object sender, System.EventArgs e)
		{
			fclsOMStandByOrder frmOMStandByOrder = new fclsOMStandByOrder(m_odcDatabaseConnection);
			frmOMStandByOrder.ShowDialog();
		}

		private void btnCheckSupplDelivery_Click(object sender, System.EventArgs e)
		{
			fclsOMCheckOrders frmOMCheckOrders = new fclsOMCheckOrders(null, m_odcDatabaseConnection);
			frmOMCheckOrders.ShowDialog();		
		}

		private void btnCheckBackOrder_Click(object sender, System.EventArgs e)
		{
			fclsOMBackOrders frmOMBackOrders = new fclsOMBackOrders(null, m_odcDatabaseConnection);
			frmOMBackOrders.ShowDialog();		
		}

		private void btnReprintResendOrder_Click(object sender, System.EventArgs e)
		{
			fclsOIViewOrders frmOIViewOrders = new fclsOIViewOrders(m_odcDatabaseConnection, fclsOIViewOrders.ViewOrdersType.NotReceivedOrders, "");
			frmOIViewOrders.ShowDialog();
		}

		private void btnEmployeeList_Click(object sender, System.EventArgs e)
		{
			fclsLSTViewReport frmLSTViewReport = new fclsLSTViewReport();
			frmLSTViewReport.typeReport = "btnEmployeeList";
			frmLSTViewReport.ShowDialog();
		}

		private void btnSupplierList_Click(object sender, System.EventArgs e)
		{
			fclsLSTViewReport frmLSTViewReport = new fclsLSTViewReport();
			frmLSTViewReport.typeReport = "btnSupplierList";
			frmLSTViewReport.ShowDialog();		
		}

		private void btnProdByCateg_Click(object sender, System.EventArgs e)
		{
			fclsLSTViewReport frmLSTViewReport = new fclsLSTViewReport();//oldYear
			frmLSTViewReport.typeReport = "btnProdByCateg";
			frmLSTViewReport.ShowDialog();		
		}

		private void btnIndexAlphabetic_Click(object sender, System.EventArgs e)
		{
			fclsLSTViewReport frmLSTViewReport = new fclsLSTViewReport();//oldYear
			frmLSTViewReport.typeReport = "btnIndexAlphabetic";
			frmLSTViewReport.ShowDialog();	
		}

		private void btnAddModDelSuppl_Click(object sender, System.EventArgs e)
		{
			fclsDMSuppliers	frmDMSuppliers = new fclsDMSuppliers(m_odcDatabaseConnection);
			frmDMSuppliers.ShowDialog();		
		}

		private void btnAddModDelEmpl_Click(object sender, System.EventArgs e)
		{
			fclsDMEmployees	frmDMEmployees = new fclsDMEmployees(m_odcDatabaseConnection);
			frmDMEmployees.ShowDialog();		
		}

		private void btnAddModDelProd_Click(object sender, System.EventArgs e)
		{
			fclsDMModifyProduct	frmDMModifyProduct = new fclsDMModifyProduct(m_odcDatabaseConnection);
			frmDMModifyProduct.ShowDialog();		
		}

		private void btnModifyTrademarks_Click(object sender, System.EventArgs e)
		{
			fclsDMTrademarks frmTrademarks = new fclsDMTrademarks(m_odcDatabaseConnection);
			frmTrademarks.ShowDialog();
		}

		private void btnViewOrder_Click(object sender, System.EventArgs e)
		{
			fclsOIViewOrders frmOIViewOrders = new fclsOIViewOrders(m_odcDatabaseConnection, fclsOIViewOrders.ViewOrdersType.ReceivedOrders, "");
			frmOIViewOrders.ShowDialog();
		}

		private void btnAccounting_Click(object sender, System.EventArgs e)
		{
			fclsOIAccounting frmOIAccounting = new fclsOIAccounting(fclsOIAccounting.FilterType.PaymentHistory, "", m_odcDatabaseConnection);
			frmOIAccounting.ShowDialog();
		}

		private void btnCanceledBo_Click(object sender, System.EventArgs e)
		{
			fclsOIViewOrders frmOIViewOrders = new fclsOIViewOrders(m_odcDatabaseConnection, fclsOIViewOrders.ViewOrdersType.CanceledBackorders, "");
			frmOIViewOrders.ShowDialog();
		}

		private void btnReturnedProd_Click(object sender, System.EventArgs e)
		{
			fclsOIViewOrders frmOIViewOrders = new fclsOIViewOrders(m_odcDatabaseConnection, fclsOIViewOrders.ViewOrdersType.ReturnedOrders, "");
			frmOIViewOrders.ShowDialog();
		}

		private void btnExpStatistics_Click(object sender, System.EventArgs e)
		{
			fclsOIStatistic frmOIStatistic = new fclsOIStatistic(m_odcDatabaseConnection);
			frmOIStatistic.ShowDialog();
		}

		private void btnProdInfo_Click(object sender, System.EventArgs e)
		{
			fclsOIViewProd frmOIViewProd = new fclsOIViewProd(m_odcDatabaseConnection);
			frmOIViewProd.ShowDialog();		
		}

		private void cmdOptions_Click(object sender, System.EventArgs e)
		{
			fclsGENOptions frmGENOptions = new fclsGENOptions(m_odcDatabaseConnection);
			frmGENOptions.Owner = this;
			frmGENOptions.ShowDialog();

			// refresh main window information upon exit from options dialog
			this.LoadDentalOfficeInfo();
		}

		private void btnOldOrders_Click(object sender, System.EventArgs e)
		{
			string strResponse = InputBox.ShowInputBox("Please enter the year for which you will introduce\nthe old Orders, Invoices (>= "
				+(oldYear-2)+" and <= "+oldYear+").","Old Orders, Invoices");
			int isNum = 1;
            if (strResponse != null)
            {
                if (strResponse.Length == 4)
                {
                    for (int i = 0; i < 4; i++)
                        if (!(char.IsNumber(strResponse, i) && isNum == 1))
                            isNum = 0;
                    if (isNum == 1)
                    {
                        oldYear = int.Parse(strResponse);
                        if (oldYear > oldYearComp || oldYear < oldYearComp - 2)
                        {
                            MessageBox.Show("'" + oldYear + "' is not a valid year!\nIt is < " + (oldYearComp - 2) + " or > " + oldYearComp);
                            return;
                        }
                    }
                    else
                    {
                        MessageBox.Show("'" + strResponse + "' is not a valid year!\nIt has non-numeric digit(s)!");
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("'" + strResponse + "' is not a valid year!\nIt has more or less then four digits!");
                    return;
                }
            }

			fclsGENOldOrder frmGENOldOrder = new fclsGENOldOrder(m_odcDatabaseConnection, oldYear);
			frmGENOldOrder.ShowDialog();
		}

		public void SetAccessAllowed(bool blnAccess)
		{
			m_blnAccessAllowed = blnAccess;
		}

		private void SetLanguage(int LanguageID)
		{
			if(LanguageID != m_intLanguageID)
			{
				m_intLanguageID = LanguageID;
				MessageBox.Show("Language Changed!");
			}
		}

		private void LoadDentalOfficeInfo()
		{
			OleDbDataAdapter odaCompany = new OleDbDataAdapter("Select * From [DentalOfficeInformation]", m_odcDatabaseConnection);
			DataTable dtaMyComp = new DataTable();
			odaCompany.Fill(dtaMyComp);
						
			this.lblMyComp.Text = dtaMyComp.Rows[0]["CompanyName"].ToString();
		}

		private void fclsGENInput_Resize(object sender, System.EventArgs e)
		{
			int intPanelWidth = (int) (this.Width * COL_PROPORTION);
			int intPanelHeight = (int) (this.Height * ROW_PROPORTION);

			this.grpOrderManagement.Height = intPanelHeight;
			this.grpOrderManagement.Width = intPanelWidth;
			
			this.grpOperationalStatistics.Location = new Point(this.grpOperationalStatistics.Location.X, this.grpOperationalStatistics.Location.Y - (intPanelHeight - this.grpOperationalStatistics.Height));
			this.grpOperationalStatistics.Height = intPanelHeight;
			this.grpOperationalStatistics.Width = intPanelWidth;
			
			this.grpDatabaseManagement.Location = new Point(this.grpDatabaseManagement.Location.X - (intPanelWidth - this.grpDatabaseManagement.Width), this.grpDatabaseManagement.Location.Y);
			this.grpDatabaseManagement.Height = intPanelHeight;
			this.grpDatabaseManagement.Width = intPanelWidth;
			
			this.grpReports.Location = new Point(this.grpReports.Location.X - (intPanelWidth - this.grpReports.Width),this.grpReports.Location.Y - (intPanelHeight - this.grpReports.Height));
			this.grpReports.Height = intPanelHeight;
			this.grpReports.Width = intPanelWidth;
		}
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            // Make sure the splash screen is closed
            CloseSplash2();
            
            base.OnClosing(e);
        }
        
		protected override void OnLoad(EventArgs args)
		{
			base.OnLoad(args);

			Application.Idle += new EventHandler(OnLoaded);
		}

		private void OnLoaded(object sender, EventArgs args)
		{
            //
			// NOTE: removed since 'Backorder' reminder performs a similar function
            //

            /*DateTime m_dtNow, m_dtOrdered;

			Application.Idle -= new EventHandler(OnLoaded);
			
			OleDbDataAdapter odaBO = new OleDbDataAdapter("Select * From [Orders] Where [BackOrderUnits] > 0", m_odcDatabaseConnection);
			DataTable dtaBO = new DataTable();
			odaBO.Fill(dtaBO);

			DialogResult dlgResult;
			System.TimeSpan timeToCancelBO = new System.TimeSpan(30, 0, 0, 0);
			m_dtNow = DateTime.Now;
			m_dtNow = new System.DateTime(m_dtNow.Year, m_dtNow.Month, m_dtNow.Day);
			for(int i=0; i<dtaBO.Rows.Count; i++)
			{
				m_dtOrdered = (DateTime) dtaBO.Rows[i]["OrderDate"];
				m_dtOrdered = new System.DateTime(m_dtOrdered.Year, m_dtOrdered.Month, m_dtOrdered.Day);
				DateTime m_dtCompar = m_dtOrdered.Add(timeToCancelBO);
				if(m_dtCompar.CompareTo(m_dtNow) <= 0)
				{
					dlgResult = MessageBox.Show("Would you like to cancel the backorders\nthat are delayed more then 30 days?", "Backorder delayed more then 30 days!",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
					
					if(dlgResult == DialogResult.Yes)
					{
						fclsGENBackOrders frmGENBackOrders = new fclsGENBackOrders(m_odcDatabaseConnection);
						frmGENBackOrders.ShowDialog();
					}
					return;
				}
			}*/
		}

        static public void StartSplash2()
        {
            // Instance a splash form given the image names
            m_frmGENSplashScreen2 = new fclsGENSplashScreen2(mc_intSplashUpdateInterval_ms);

            // Run the form
            Application.Run(m_frmGENSplashScreen2);
        }

        private void CloseSplash2()
        {
            if (m_frmGENSplashScreen2 == null)
                return;

            // Shut down the splash screen
            m_frmGENSplashScreen2.Invoke(new EventHandler(m_frmGENSplashScreen2.KillMe));
            //m_frmGENSplashScreen2.Dispose();
            m_frmGENSplashScreen2 = null;
        }

		#region BlinkingLabels
		private void ConfigureReminderLabels()
		{
			DataTable dtaMisc, dtaRemindersConfig;
			DateTime dtNow, dtReminderDate, dtStartDate, dtRemindDate;
			int intNDaysReminderInterval;
			OleDbDataAdapter odaMisc;
			TimeSpan tsReminderInterval;
			
			dtNow = DateTime.Now;
			
			// load reminder settings
			odaMisc = new OleDbDataAdapter("SELECT *  FROM [RemindMe]", m_odcDatabaseConnection);
			dtaRemindersConfig = new DataTable();
			odaMisc.Fill(dtaRemindersConfig);

			// Reminder for not orders that aren't received
			intNDaysReminderInterval = int.Parse(dtaRemindersConfig.Rows[0]["Days"].ToString());
			tsReminderInterval = new TimeSpan(intNDaysReminderInterval, 0, 0, 0);
			dtReminderDate = (DateTime) dtaRemindersConfig.Rows[0]["rmDate"];
			dtStartDate = new DateTime(dtReminderDate.Year, dtReminderDate.Month, dtReminderDate.Day);
			dtRemindDate = dtStartDate.Add(tsReminderInterval);

			odaMisc = new OleDbDataAdapter("SELECT * FROM [Orders] WHERE [Checked] = 0", m_odcDatabaseConnection);
			dtaMisc = new DataTable();
			odaMisc.Fill(dtaMisc);
			
			if (dtaMisc.Rows.Count > 0)
			{
				this.lblSentOrders.Enabled = true;
				blinking(m_intBlinkinterval,0);
			}
			else
			{
				this.lblSentOrders.BackColor = Color.Azure;
				this.lblSentOrders.ForeColor = Color.White;
			}
			if(dtRemindDate.Date > dtNow.Date)
			{
				this.lblSentOrders.Enabled = false;
				this.lblSentOrders.BackColor = Color.Azure;
				this.lblSentOrders.ForeColor = Color.White;
			}

			// Reminder for the unpayed orders
			intNDaysReminderInterval = int.Parse(dtaRemindersConfig.Rows[1]["Days"].ToString());
			tsReminderInterval = new TimeSpan(intNDaysReminderInterval, 0, 0, 0);
			dtReminderDate = (DateTime) dtaRemindersConfig.Rows[1]["rmDate"];
			dtStartDate = new DateTime(dtReminderDate.Year, dtReminderDate.Month, dtReminderDate.Day);
			dtRemindDate = dtStartDate.Add(tsReminderInterval);

			odaMisc = new OleDbDataAdapter("SELECT * FROM [OrderPayment] WHERE [checkPayment] = 0", m_odcDatabaseConnection);
			dtaMisc = new DataTable();
			odaMisc.Fill(dtaMisc);

			if (dtaMisc.Rows.Count > 0)
			{
				this.lblOrderToPay.Enabled = true;
				blinking(m_intBlinkinterval,1);
			}
			else
			{
				this.lblOrderToPay.BackColor = Color.Azure;
				this.lblOrderToPay.ForeColor = Color.White;
			}
			if(dtRemindDate.Date > dtNow.Date)
			{
				this.lblOrderToPay.Enabled = false;
				this.lblOrderToPay.BackColor = Color.Azure;
				this.lblOrderToPay.ForeColor = Color.White;
			}

			// Reminder for returned products w/o confirmation
			intNDaysReminderInterval = int.Parse(dtaRemindersConfig.Rows[3]["Days"].ToString());
			tsReminderInterval = new TimeSpan(intNDaysReminderInterval, 0, 0, 0);
			dtReminderDate = (DateTime) dtaRemindersConfig.Rows[3]["rmDate"];
			dtStartDate = new DateTime(dtReminderDate.Year, dtReminderDate.Month, dtReminderDate.Day);
			dtRemindDate = dtStartDate.Add(tsReminderInterval);

			odaMisc = new OleDbDataAdapter("Select * From [Orders] WHERE [ReturnNumber] = '0'", m_odcDatabaseConnection);
			dtaMisc = new DataTable();
			odaMisc.Fill(dtaMisc);

			if (dtaMisc.Rows.Count > 0)
			{
				this.lblReturnedProducts.Enabled = true;
				blinking(m_intBlinkinterval,2);
			}
			else
			{
				this.lblReturnedProducts.BackColor = Color.Azure;
				this.lblReturnedProducts.ForeColor = Color.White;
			}
			if(dtRemindDate.Date > dtNow.Date)
			{
				this.lblReturnedProducts.Enabled = false;
				this.lblReturnedProducts.BackColor = Color.Azure;
				this.lblReturnedProducts.ForeColor = Color.White;
			}

			// Reminder for backorders
			intNDaysReminderInterval = int.Parse(dtaRemindersConfig.Rows[2]["Days"].ToString());
			tsReminderInterval = new TimeSpan(intNDaysReminderInterval, 0, 0, 0);
			dtReminderDate = (DateTime) dtaRemindersConfig.Rows[2]["rmDate"];
			dtStartDate = new DateTime(dtReminderDate.Year, dtReminderDate.Month, dtReminderDate.Day);
			dtRemindDate = dtStartDate.Add(tsReminderInterval);

			odaMisc = new OleDbDataAdapter("SELECT * FROM [Orders] WHERE [BackOrderUnits] > 0", m_odcDatabaseConnection);
			dtaMisc = new DataTable();
			odaMisc.Fill(dtaMisc);

			if (dtaMisc.Rows.Count > 0)
			{
				this.lblBackOrders.Enabled = true;
				blinking(m_intBlinkinterval,3);
			}
			else
			{
				this.lblBackOrders.BackColor = Color.Azure;
				this.lblBackOrders.ForeColor = Color.White;
			}
			if(dtRemindDate.Date > dtNow.Date)
			{
				lblBackOrders.Enabled = false;
				this.lblBackOrders.BackColor = Color.Azure;
				this.lblBackOrders.ForeColor = Color.White;
			}
		}

		private void lblOrderToPay_Click(object sender, System.EventArgs e)
		{
			fclsGenRemindMe frmGenRemindMe = new fclsGenRemindMe(this,
                                                                 fclsGenRemindMe.ReminderType.UnpaidOrder,
                                                                 m_odcDatabaseConnection);
			frmGenRemindMe.ShowDialog();		
		}

		private void lblSentOrders_Click(object sender, System.EventArgs e)
		{
			fclsGenRemindMe frmGenRemindMe = new fclsGenRemindMe(this,
                                                                 fclsGenRemindMe.ReminderType.LateOrder,
                                                                 m_odcDatabaseConnection);
			frmGenRemindMe.ShowDialog();		
		}

		private void lblBackOrders_Click(object sender, System.EventArgs e)
		{
			fclsGenRemindMe frmGenRemindMe = new fclsGenRemindMe(this,
                                                                 fclsGenRemindMe.ReminderType.Backorder,
                                                                 m_odcDatabaseConnection);
			frmGenRemindMe.ShowDialog();		
		}

		private void lblReturnedProducts_Click(object sender, System.EventArgs e)
		{
			fclsGenRemindMe frmGenRemindMe = new fclsGenRemindMe(this,
                                                                 fclsGenRemindMe.ReminderType.UnsentReturnedProducts,
                                                                 m_odcDatabaseConnection);
			frmGenRemindMe.ShowDialog();		
		}

		public void SetEnabledChange(int nrLabel, bool bl_EnabledState)
		{
			switch(nrLabel)
			{
				case 0:												// Sent ORDERS!!!!
					this.lblSentOrders.Enabled = bl_EnabledState;
					if (!bl_EnabledState)
					{
						this.lblSentOrders.BackColor = Color.Azure;
						this.lblSentOrders.ForeColor = Color.White;
					}
					break;
				case 1:												// ORDERS to PAY!!!!
					this.lblOrderToPay.Enabled = bl_EnabledState;		
					if (!bl_EnabledState)
					{
						this.lblOrderToPay.BackColor = Color.Azure;
						this.lblOrderToPay.ForeColor = Color.White;
					}
					break;
				case 2:												// BACKORDERS!!!!
					this.lblBackOrders.Enabled = bl_EnabledState;		
					if (!bl_EnabledState)
					{
						this.lblBackOrders.BackColor = Color.Azure;
						this.lblBackOrders.ForeColor = Color.White;
					}
					break;
				case 3:												// RETURNED PRODUCTS without RETURN NUMBER!!!!
					this.lblReturnedProducts.Enabled = bl_EnabledState;		
					if (!bl_EnabledState)
					{
						this.lblReturnedProducts.BackColor = Color.Azure;
						this.lblReturnedProducts.ForeColor = Color.White;
					}
					break;
			}
		}
		public void blinking(int m_intInterval,int m_intNrLabel)
		{
			this.BlinkTimer.Interval = m_intInterval;
			this.BlinkTimer.Start();
			/*			switch(m_intNrLabel)
						{
							case 0:												
								BlinkTimer_Tick(lblSentOrders,null);
								break;
							case 1:												
								BlinkTimer_Tick(lblOrderToPay,null);
								break;
							case 2:												
								BlinkTimer_Tick(lblReturnedProducts,null);
								break;
							case 3:												
								BlinkTimer_Tick(lblBackOrders,null);
								break;
						}*/
		}

		private void BlinkTimer_Tick(object sender, System.EventArgs e)
		{
			if(lblSentOrders.Enabled)						// Sent ORDERS!!!!
			{
				if(lblSentOrders.BackColor == Color.Azure)
				{
                    lblSentOrders.BackColor = Color.White;
					lblSentOrders.ForeColor = Color.Blue;
				}
				else
				{
					this.lblSentOrders.BackColor = Color.Azure;
					this.lblSentOrders.ForeColor = Color.Gainsboro;
				}
			}
			if(lblOrderToPay.Enabled)						// ORDERS to PAY!!!!
			{
				if(lblOrderToPay.BackColor == Color.Azure)
				{
					lblOrderToPay.BackColor = Color.PaleTurquoise;
					lblOrderToPay.ForeColor = Color.Blue;
				}
				else
				{
					this.lblOrderToPay.BackColor = Color.Azure;
					this.lblOrderToPay.ForeColor = Color.Gainsboro;
				}
			}
			if(lblReturnedProducts.Enabled)					// RETURNED PRODUCTS without RETURN NUMBER!!!!
			{
				if(lblReturnedProducts.BackColor == Color.Azure)
				{
					lblReturnedProducts.BackColor = Color.White;
					lblReturnedProducts.ForeColor = Color.Red;
				}
				else
				{
					this.lblReturnedProducts.BackColor = Color.Azure;
					this.lblReturnedProducts.ForeColor = Color.Gainsboro;
				}
			}
			if(lblBackOrders.Enabled)						// BACKORDERS!!!!
			{
				if(lblBackOrders.BackColor == Color.Azure)
				{
					lblBackOrders.BackColor = Color.White;
					lblBackOrders.ForeColor = Color.Green;
				}
				else
				{
					this.lblBackOrders.BackColor = Color.Azure;
					this.lblBackOrders.ForeColor = Color.Gainsboro;
				}
			}
		
		}
		#endregion
	}
}
