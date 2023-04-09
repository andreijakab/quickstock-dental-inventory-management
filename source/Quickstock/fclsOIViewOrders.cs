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
	/// Summary description for frmReturnedProducts.
	/// </summary>
	public class fclsOIViewOrders : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox grpDate;
		private System.Windows.Forms.Label lblEndDate;
		private System.Windows.Forms.Label lblStartDate;
		private System.Windows.Forms.DateTimePicker dtpEnd;
		private System.Windows.Forms.DateTimePicker dtpStart;
		private System.Windows.Forms.Button cmdViewPrintOrder;
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.RadioButton optSupplier;
		private System.Windows.Forms.ComboBox cmbSupplier;
		private System.Windows.Forms.RadioButton optOrderNumber;
		private System.Windows.Forms.ComboBox cmbOrderedBy;
		private System.Windows.Forms.RadioButton optOrderedBy;
		private System.Windows.Forms.GroupBox gpbSearchBy;
		private System.Windows.Forms.ListView lstViewOrders;
		private System.Windows.Forms.ColumnHeader prodName;
		private System.Windows.Forms.ColumnHeader subProdName;
		private System.Windows.Forms.ColumnHeader marCom;
		private System.Windows.Forms.ColumnHeader units;
		private System.Windows.Forms.ColumnHeader pack;
		private System.Windows.Forms.ColumnHeader price;
		private System.Windows.Forms.ListBox lbxOrderNumber;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.GroupBox gpbOrderInformation;
		private System.Windows.Forms.Label lblOrderDate;
		private System.Windows.Forms.Label lblCanceledBy_Data;
		private System.Windows.Forms.Label lblCanceledBy;
		private System.Windows.Forms.Label lblCanceledDate_Data;
		private System.Windows.Forms.Label lblCanceledDate;
		private System.Windows.Forms.Label lblReturnNumber_Data;
		private System.Windows.Forms.Label lblReturnNumber;
        private System.Windows.Forms.Label lblOrderDate_Data;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private enum SearchOrders:int		{All, BySupplier, ByEmployee};
        public enum ViewOrdersType : int { NotReceivedOrders, ReceivedOrders, CanceledBackorders, ReturnedOrders, ReturnedOrders_ReadOnly};
		
		private ArrayList											m_alReturnedProducts;
		private bool												m_blnIsReturned;
		private clsListViewColumnSorter								m_lvwColumnSorter;
		private DataTable											m_dtaEmployees, m_dtaOrders, m_dtaOrderProducts, m_dtaSuppliers;
		private DateTime											m_dtReturnDate;
		private int													m_intReturnEmployeeId;
		private OleDbConnection										m_odcConnection;
		private SearchOrders										m_soCurrentFilter;
        private string                                              m_strReturnNumber, m_strQueriedOrderNumber;
		private SupplierInformation									m_siSupplier;
		private ViewOrdersType										m_votFormType;

        public fclsOIViewOrders(OleDbConnection odcConnection, ViewOrdersType votFormType, string strQueriedOrderNumber)
		{
            NumberFormatInfo nfiNumberFormat;

			InitializeComponent();

            // initialize global variables
			m_odcConnection = odcConnection;
			m_votFormType = votFormType;
            m_strQueriedOrderNumber = strQueriedOrderNumber;
			m_lvwColumnSorter = new clsListViewColumnSorter();

			// configure listview (sets the listview control's sorter and currency symbol)
			this.lstViewOrders.ListViewItemSorter = m_lvwColumnSorter;
            nfiNumberFormat = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            this.lstViewOrders.Columns[3].Text += " " + nfiNumberFormat.CurrencySymbol;

			switch(m_votFormType)
			{
				case ViewOrdersType.CanceledBackorders:
					this.Text = "Quick Stock - Canceled Backorders";

					this.lblCanceledBy.Visible = true;
					this.lblCanceledBy_Data.Visible = true;
					this.lblCanceledDate.Visible = true;
					this.lblCanceledDate_Data.Visible = true;
				break;

				case ViewOrdersType.NotReceivedOrders:
					this.Text = "Quick Stock - Resend or reprint the non-received Orders";
					this.cmdViewPrintOrder.Text = "View / Resend Order(s)";
				break;

				case ViewOrdersType.ReceivedOrders:
					this.Text = "Quick Stock - Received Orders";
				break;

				case ViewOrdersType.ReturnedOrders:
					this.Text = "Quick Stock - Orders with Returned Products";

					this.lblCanceledBy.Visible = true;
					this.lblCanceledBy.Text = "Returned by";
					this.lblCanceledBy_Data.Visible = true;
					this.lblCanceledDate.Visible = true;
					this.lblCanceledDate.Text = "Return Date";
					this.lblCanceledDate_Data.Visible = true;
					this.lblReturnNumber.Visible = true;
					this.lblReturnNumber_Data.Visible = true;
				break;

                case ViewOrdersType.ReturnedOrders_ReadOnly:
                    this.Text = "Quick Stock - Orders with Returned Products (Viewing Only)";

                    this.lblCanceledBy.Visible = true;
                    this.lblCanceledBy.Text = "Returned by";
                    this.lblCanceledBy_Data.Visible = true;
                    this.lblCanceledDate.Visible = true;
                    this.lblCanceledDate.Text = "Return Date";
                    this.lblCanceledDate_Data.Visible = true;
                    this.lblReturnNumber.Visible = true;
                    this.lblReturnNumber_Data.Visible = true;

                    this.gpbSearchBy.Enabled = false;
                    this.grpDate.Enabled = false;
                break;
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
            this.cmdViewPrintOrder = new System.Windows.Forms.Button();
            this.cmdClose = new System.Windows.Forms.Button();
            this.gpbSearchBy = new System.Windows.Forms.GroupBox();
            this.lbxOrderNumber = new System.Windows.Forms.ListBox();
            this.cmbOrderedBy = new System.Windows.Forms.ComboBox();
            this.optOrderedBy = new System.Windows.Forms.RadioButton();
            this.optOrderNumber = new System.Windows.Forms.RadioButton();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.optSupplier = new System.Windows.Forms.RadioButton();
            this.lstViewOrders = new System.Windows.Forms.ListView();
            this.prodName = new System.Windows.Forms.ColumnHeader();
            this.subProdName = new System.Windows.Forms.ColumnHeader();
            this.marCom = new System.Windows.Forms.ColumnHeader();
            this.price = new System.Windows.Forms.ColumnHeader();
            this.units = new System.Windows.Forms.ColumnHeader();
            this.pack = new System.Windows.Forms.ColumnHeader();
            this.btnHelp = new System.Windows.Forms.Button();
            this.grpDate = new System.Windows.Forms.GroupBox();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.gpbOrderInformation = new System.Windows.Forms.GroupBox();
            this.lblOrderDate_Data = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblCanceledBy_Data = new System.Windows.Forms.Label();
            this.lblCanceledBy = new System.Windows.Forms.Label();
            this.lblCanceledDate_Data = new System.Windows.Forms.Label();
            this.lblCanceledDate = new System.Windows.Forms.Label();
            this.lblReturnNumber_Data = new System.Windows.Forms.Label();
            this.lblReturnNumber = new System.Windows.Forms.Label();
            this.gpbSearchBy.SuspendLayout();
            this.grpDate.SuspendLayout();
            this.gpbOrderInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdViewPrintOrder
            // 
            this.cmdViewPrintOrder.Location = new System.Drawing.Point(624, 448);
            this.cmdViewPrintOrder.Name = "cmdViewPrintOrder";
            this.cmdViewPrintOrder.Size = new System.Drawing.Size(136, 32);
            this.cmdViewPrintOrder.TabIndex = 14;
            this.cmdViewPrintOrder.Text = "View / Print Order(s)";
            this.cmdViewPrintOrder.Click += new System.EventHandler(this.cmdViewPrintOrder_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Location = new System.Drawing.Point(784, 448);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(96, 32);
            this.cmdClose.TabIndex = 16;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // gpbSearchBy
            // 
            this.gpbSearchBy.Controls.Add(this.lbxOrderNumber);
            this.gpbSearchBy.Controls.Add(this.cmbOrderedBy);
            this.gpbSearchBy.Controls.Add(this.optOrderedBy);
            this.gpbSearchBy.Controls.Add(this.optOrderNumber);
            this.gpbSearchBy.Controls.Add(this.cmbSupplier);
            this.gpbSearchBy.Controls.Add(this.optSupplier);
            this.gpbSearchBy.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gpbSearchBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.gpbSearchBy.Location = new System.Drawing.Point(8, 0);
            this.gpbSearchBy.Name = "gpbSearchBy";
            this.gpbSearchBy.Size = new System.Drawing.Size(680, 112);
            this.gpbSearchBy.TabIndex = 17;
            this.gpbSearchBy.TabStop = false;
            this.gpbSearchBy.Text = "Search Criteria";
            // 
            // lbxOrderNumber
            // 
            this.lbxOrderNumber.ItemHeight = 16;
            this.lbxOrderNumber.Location = new System.Drawing.Point(128, 16);
            this.lbxOrderNumber.Name = "lbxOrderNumber";
            this.lbxOrderNumber.Size = new System.Drawing.Size(160, 84);
            this.lbxOrderNumber.TabIndex = 20;
            this.lbxOrderNumber.SelectedIndexChanged += new System.EventHandler(this.lbxOrderNumber_SelectedIndexChanged);
            this.lbxOrderNumber.Click += new System.EventHandler(this.lbxOrderNumber_Click);
            // 
            // cmbOrderedBy
            // 
            this.cmbOrderedBy.Enabled = false;
            this.cmbOrderedBy.Location = new System.Drawing.Point(448, 72);
            this.cmbOrderedBy.Name = "cmbOrderedBy";
            this.cmbOrderedBy.Size = new System.Drawing.Size(216, 24);
            this.cmbOrderedBy.TabIndex = 13;
            this.cmbOrderedBy.SelectedIndexChanged += new System.EventHandler(this.cmbOrderedBy_SelectedIndexChanged);
            // 
            // optOrderedBy
            // 
            this.optOrderedBy.Location = new System.Drawing.Point(328, 76);
            this.optOrderedBy.Name = "optOrderedBy";
            this.optOrderedBy.Size = new System.Drawing.Size(120, 16);
            this.optOrderedBy.TabIndex = 12;
            this.optOrderedBy.Text = "Ordered by";
            this.optOrderedBy.Click += new System.EventHandler(this.optOrderedBy_Click);
            // 
            // optOrderNumber
            // 
            this.optOrderNumber.Checked = true;
            this.optOrderNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optOrderNumber.Location = new System.Drawing.Point(16, 48);
            this.optOrderNumber.Name = "optOrderNumber";
            this.optOrderNumber.Size = new System.Drawing.Size(120, 16);
            this.optOrderNumber.TabIndex = 8;
            this.optOrderNumber.TabStop = true;
            this.optOrderNumber.Text = "Order Number";
            this.optOrderNumber.Click += new System.EventHandler(this.optOrderNumber_Click);
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.Enabled = false;
            this.cmbSupplier.Location = new System.Drawing.Point(448, 24);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(216, 24);
            this.cmbSupplier.TabIndex = 7;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier_SelectedIndexChanged);
            // 
            // optSupplier
            // 
            this.optSupplier.Location = new System.Drawing.Point(328, 28);
            this.optSupplier.Name = "optSupplier";
            this.optSupplier.Size = new System.Drawing.Size(104, 16);
            this.optSupplier.TabIndex = 0;
            this.optSupplier.Text = "Supplier";
            this.optSupplier.Click += new System.EventHandler(this.optSupplier_Click);
            // 
            // lstViewOrders
            // 
            this.lstViewOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.prodName,
            this.subProdName,
            this.marCom,
            this.price,
            this.units,
            this.pack});
            this.lstViewOrders.FullRowSelect = true;
            this.lstViewOrders.Location = new System.Drawing.Point(8, 120);
            this.lstViewOrders.MultiSelect = false;
            this.lstViewOrders.Name = "lstViewOrders";
            this.lstViewOrders.Size = new System.Drawing.Size(976, 248);
            this.lstViewOrders.TabIndex = 18;
            this.lstViewOrders.UseCompatibleStateImageBehavior = false;
            this.lstViewOrders.View = System.Windows.Forms.View.Details;
            this.lstViewOrders.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstViewOrders_ColumnClick);
            this.lstViewOrders.Click += new System.EventHandler(this.lstViewOrders_Click);
            // 
            // prodName
            // 
            this.prodName.Text = "Product Name";
            this.prodName.Width = 260;
            // 
            // subProdName
            // 
            this.subProdName.Text = "Sub-Product Name";
            this.subProdName.Width = 235;
            // 
            // marCom
            // 
            this.marCom.Text = "TradeMark";
            this.marCom.Width = 164;
            // 
            // price
            // 
            this.price.Text = "Unit Price";
            this.price.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.price.Width = 69;
            // 
            // units
            // 
            this.units.Text = "Units";
            this.units.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.units.Width = 78;
            // 
            // pack
            // 
            this.pack.Text = "Packaging";
            this.pack.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.pack.Width = 151;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(888, 448);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(96, 32);
            this.btnHelp.TabIndex = 27;
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // grpDate
            // 
            this.grpDate.Controls.Add(this.lblEndDate);
            this.grpDate.Controls.Add(this.lblStartDate);
            this.grpDate.Controls.Add(this.dtpEnd);
            this.grpDate.Controls.Add(this.dtpStart);
            this.grpDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDate.Location = new System.Drawing.Point(696, 0);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(288, 112);
            this.grpDate.TabIndex = 28;
            this.grpDate.TabStop = false;
            this.grpDate.Text = "Time Period";
            // 
            // lblEndDate
            // 
            this.lblEndDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(32, 75);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(64, 16);
            this.lblEndDate.TabIndex = 3;
            this.lblEndDate.Text = "End Date";
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(32, 35);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(64, 16);
            this.lblStartDate.TabIndex = 2;
            this.lblStartDate.Text = "Start Date";
            this.lblStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(104, 72);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(152, 22);
            this.dtpEnd.TabIndex = 1;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(104, 32);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(152, 22);
            this.dtpStart.TabIndex = 0;
            this.dtpStart.Value = new System.DateTime(2005, 1, 1, 0, 0, 0, 0);
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // gpbOrderInformation
            // 
            this.gpbOrderInformation.Controls.Add(this.lblOrderDate_Data);
            this.gpbOrderInformation.Controls.Add(this.lblOrderDate);
            this.gpbOrderInformation.Controls.Add(this.lblCanceledBy_Data);
            this.gpbOrderInformation.Controls.Add(this.lblCanceledBy);
            this.gpbOrderInformation.Controls.Add(this.lblCanceledDate_Data);
            this.gpbOrderInformation.Controls.Add(this.lblCanceledDate);
            this.gpbOrderInformation.Controls.Add(this.lblReturnNumber_Data);
            this.gpbOrderInformation.Controls.Add(this.lblReturnNumber);
            this.gpbOrderInformation.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.gpbOrderInformation.Location = new System.Drawing.Point(8, 376);
            this.gpbOrderInformation.Name = "gpbOrderInformation";
            this.gpbOrderInformation.Size = new System.Drawing.Size(976, 64);
            this.gpbOrderInformation.TabIndex = 29;
            this.gpbOrderInformation.TabStop = false;
            this.gpbOrderInformation.Text = "Order Information";
            // 
            // lblOrderDate_Data
            // 
            this.lblOrderDate_Data.BackColor = System.Drawing.Color.White;
            this.lblOrderDate_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblOrderDate_Data.Location = new System.Drawing.Point(88, 23);
            this.lblOrderDate_Data.Name = "lblOrderDate_Data";
            this.lblOrderDate_Data.Size = new System.Drawing.Size(132, 24);
            this.lblOrderDate_Data.TabIndex = 34;
            this.lblOrderDate_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Location = new System.Drawing.Point(8, 27);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(77, 16);
            this.lblOrderDate.TabIndex = 33;
            this.lblOrderDate.Text = "Order Date";
            // 
            // lblCanceledBy_Data
            // 
            this.lblCanceledBy_Data.BackColor = System.Drawing.Color.White;
            this.lblCanceledBy_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCanceledBy_Data.Location = new System.Drawing.Point(317, 23);
            this.lblCanceledBy_Data.Name = "lblCanceledBy_Data";
            this.lblCanceledBy_Data.Size = new System.Drawing.Size(184, 24);
            this.lblCanceledBy_Data.TabIndex = 32;
            this.lblCanceledBy_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCanceledBy_Data.Visible = false;
            // 
            // lblCanceledBy
            // 
            this.lblCanceledBy.AutoSize = true;
            this.lblCanceledBy.Location = new System.Drawing.Point(229, 27);
            this.lblCanceledBy.Name = "lblCanceledBy";
            this.lblCanceledBy.Size = new System.Drawing.Size(87, 16);
            this.lblCanceledBy.TabIndex = 31;
            this.lblCanceledBy.Text = "Canceled by";
            this.lblCanceledBy.Visible = false;
            // 
            // lblCanceledDate_Data
            // 
            this.lblCanceledDate_Data.BackColor = System.Drawing.Color.White;
            this.lblCanceledDate_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblCanceledDate_Data.Location = new System.Drawing.Point(604, 23);
            this.lblCanceledDate_Data.Name = "lblCanceledDate_Data";
            this.lblCanceledDate_Data.Size = new System.Drawing.Size(132, 24);
            this.lblCanceledDate_Data.TabIndex = 30;
            this.lblCanceledDate_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCanceledDate_Data.Visible = false;
            // 
            // lblCanceledDate
            // 
            this.lblCanceledDate.AutoSize = true;
            this.lblCanceledDate.Location = new System.Drawing.Point(505, 27);
            this.lblCanceledDate.Name = "lblCanceledDate";
            this.lblCanceledDate.Size = new System.Drawing.Size(101, 16);
            this.lblCanceledDate.TabIndex = 29;
            this.lblCanceledDate.Text = "Canceled Date";
            this.lblCanceledDate.Visible = false;
            // 
            // lblReturnNumber_Data
            // 
            this.lblReturnNumber_Data.BackColor = System.Drawing.Color.White;
            this.lblReturnNumber_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblReturnNumber_Data.Location = new System.Drawing.Point(838, 23);
            this.lblReturnNumber_Data.Name = "lblReturnNumber_Data";
            this.lblReturnNumber_Data.Size = new System.Drawing.Size(132, 24);
            this.lblReturnNumber_Data.TabIndex = 28;
            this.lblReturnNumber_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblReturnNumber_Data.Visible = false;
            // 
            // lblReturnNumber
            // 
            this.lblReturnNumber.AutoSize = true;
            this.lblReturnNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblReturnNumber.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblReturnNumber.Location = new System.Drawing.Point(737, 27);
            this.lblReturnNumber.Name = "lblReturnNumber";
            this.lblReturnNumber.Size = new System.Drawing.Size(104, 16);
            this.lblReturnNumber.TabIndex = 27;
            this.lblReturnNumber.Text = "Return Number";
            this.lblReturnNumber.Visible = false;
            // 
            // fclsOIViewOrders
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(992, 486);
            this.Controls.Add(this.gpbOrderInformation);
            this.Controls.Add(this.grpDate);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.gpbSearchBy);
            this.Controls.Add(this.cmdClose);
            this.Controls.Add(this.cmdViewPrintOrder);
            this.Controls.Add(this.lstViewOrders);
            this.MaximizeBox = false;
            this.Name = "fclsOIViewOrders";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Stock - View Orders";
            this.Load += new System.EventHandler(this.frmOIViewOrders_Load);
            this.gpbSearchBy.ResumeLayout(false);
            this.grpDate.ResumeLayout(false);
            this.gpbOrderInformation.ResumeLayout(false);
            this.gpbOrderInformation.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

        #region Events
        private void btnHelp_Click(object sender, System.EventArgs e)
        {
            string strHTMLHelpFile = "";

            switch (m_votFormType)
            {
                case ViewOrdersType.CanceledBackorders:
                    strHTMLHelpFile = "CancelledBackorders.htm";
                break;

                case ViewOrdersType.NotReceivedOrders:
                    //strHTMLHelpFile = "ResendReprintOrders.htm";
                break;

                case ViewOrdersType.ReceivedOrders:
                    strHTMLHelpFile = "PastOrders.htm";
                break;

                case ViewOrdersType.ReturnedOrders:
                    strHTMLHelpFile = "ReturnedProducts.htm";
                break;
            }

            Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm", strHTMLHelpFile);
        }

        private void cmbOrderedBy_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.ClearFields(false, SearchOrders.ByEmployee);

            if (this.cmbOrderedBy.SelectedIndex != -1)
                this.GetOrderNumbers(SearchOrders.ByEmployee);
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.ClearFields(false, SearchOrders.BySupplier);

            if (this.cmbSupplier.SelectedIndex != -1)
                this.GetOrderNumbers(SearchOrders.BySupplier);
        }

        private void cmdClose_Click(object sender, System.EventArgs e)
        {
            this.Close();
        }

        private void cmdViewPrintOrder_Click(object sender, System.EventArgs e)
        {
            // TODO: Think if other subtypes for ViewOrderReportCaller need to be called depending on ViewOrdersType
            switch (m_votFormType)
            {
                case ViewOrdersType.NotReceivedOrders:
                case ViewOrdersType.ReceivedOrders:
                    fclsOIViewOrdRpt frmOIViewOrdRpt = new fclsOIViewOrdRpt(this, fclsOIViewOrdRpt.ViewOrderReportCaller.PreviousOrder, m_odcConnection);
                    frmOIViewOrdRpt.SetOrderInformation(this.lbxOrderNumber.SelectedItem.ToString(),
                                                        m_siSupplier);
                    frmOIViewOrdRpt.ShowDialog();
                break;

                case ViewOrdersType.CanceledBackorders:
                    frmOIViewOrdRpt = new fclsOIViewOrdRpt(this, fclsOIViewOrdRpt.ViewOrderReportCaller.CanceledBackorder, m_odcConnection);
                    frmOIViewOrdRpt.SetOrderInformation(this.lbxOrderNumber.SelectedItem.ToString(),
                                                        m_siSupplier);
                    frmOIViewOrdRpt.ShowDialog();
                break;

                case ViewOrdersType.ReturnedOrders:
                    frmOIViewOrdRpt = new fclsOIViewOrdRpt(this, fclsOIViewOrdRpt.ViewOrderReportCaller.ReturnedOrders, m_odcConnection);
                    frmOIViewOrdRpt.SetOrderInformation(this.lbxOrderNumber.SelectedItem.ToString(),
                                                        m_siSupplier);
                    frmOIViewOrdRpt.ShowDialog();
                break;
            }
        }

        private void dtpStart_ValueChanged(object sender, System.EventArgs e)
        {
            this.GetOrderNumbers(m_soCurrentFilter);
        }

        private void dtpEnd_ValueChanged(object sender, System.EventArgs e)
        {
            this.GetOrderNumbers(m_soCurrentFilter);
        }

		private void frmOIViewOrders_Load(object sender, System.EventArgs e)
		{
			// Variable declaration
			OleDbDataAdapter odaEmployees, odaSuppliers;

			// Variable initialization
			m_dtaEmployees = new DataTable("Employees");
			m_dtaSuppliers = new DataTable("Suppliers");
			m_siSupplier = new SupplierInformation();

			// Create and configure the ToolTip and associate with the Form container.
			ToolTip toolTip1 = new ToolTip();
			toolTip1.AutoPopDelay = 5000;
			toolTip1.InitialDelay = 1000;
			toolTip1.ReshowDelay = 500;
			toolTip1.ShowAlways = true;
			
			// Set up the ToolTip text for the Button and Checkbox.
			if(m_votFormType == ViewOrdersType.ReturnedOrders)
				toolTip1.SetToolTip(this.lstViewOrders, "Click on the line of a Product\n in order to Update the Return Number (if it is null).");

			// Load employees and populate combo box
			odaEmployees = new OleDbDataAdapter("SELECT * FROM [Employees] ORDER BY LastName", m_odcConnection);
			odaEmployees.Fill(m_dtaEmployees);
			
			// Load suppliers and populate combo box
			odaSuppliers = new OleDbDataAdapter("SELECT * FROM [Suppliers] ORDER BY CompanyName", m_odcConnection);
			odaSuppliers.Fill(m_dtaSuppliers);
			
			// TODO: Set date in time period date pickers
			//this.dtpStart.Value = ;
			//this.dtpEnd.Text = System.DateTime.Now.ToShortDateString();

			this.GetOrderNumbers(SearchOrders.All);
			this.ShowSelectedOrder();
		}

        private void lbxOrderNumber_Click(object sender, System.EventArgs e)
        {
            this.ShowSelectedOrder();
        }

        private void lbxOrderNumber_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.ShowSelectedOrder();
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

        private void lstViewOrders_Click(object sender, System.EventArgs e)
        {
            // Variable declaration
            DataRow dtrRow;
            DataTable dtaReturnedProductOrder;
            fclsOMReturnProdCanceledBO frmReturnProduct;
            OleDbCommandBuilder ocbReturnedProductOrder;
            OleDbDataAdapter odaReturnedProductOrder;
            string strOrderId;

            // Variable initialization
            dtaReturnedProductOrder = new DataTable("ReturnedProducts");

            if ((m_votFormType == ViewOrdersType.ReturnedOrders) && (this.lstViewOrders.SelectedItems[0] != null))
            {
                dtrRow = m_dtaOrderProducts.Rows[this.lstViewOrders.SelectedItems[0].Index];
                strOrderId = dtrRow["Orders.OrderId"].ToString();

                if (dtrRow["Orders.ReturnNumber"].ToString() == "0")
                {
                    // Display return product form		
                    frmReturnProduct = new fclsOMReturnProdCanceledBO(this,
                                                                      fclsOMReturnProdCanceledBO.Caller.ViewOrders,
                                                                      strOrderId,
                                                                      m_siSupplier,
                                                                      m_alReturnedProducts,
                                                                      m_odcConnection);
                    frmReturnProduct.SetReturnInformation((DateTime)dtrRow["Orders.ReturnDate"],
                                                          dtrRow["Orders.ReturnNumber"].ToString(),
                                                          (int)dtrRow["Orders.ReturnEmployeeId"]);
                    frmReturnProduct.ShowDialog();

                    if (m_blnIsReturned)
                    {
                        odaReturnedProductOrder = new OleDbDataAdapter("SELECT * FROM Orders WHERE OrderId='" + strOrderId + "'", m_odcConnection);
                        ocbReturnedProductOrder = new OleDbCommandBuilder(odaReturnedProductOrder);
                        odaReturnedProductOrder.Fill(dtaReturnedProductOrder);

                        for (int i = 0; i < dtaReturnedProductOrder.Rows.Count; i++)
                        {
                            dtrRow = dtaReturnedProductOrder.Rows[i];

                            if (double.Parse(dtrRow["ReturnUnits"].ToString()) == 0.0)
                                continue;

                            dtrRow.BeginEdit();
                            dtrRow["ReturnDate"] = m_dtReturnDate.ToShortDateString();
                            dtrRow["ReturnNumber"] = m_strReturnNumber;
                            dtrRow["ReturnEmployeeId"] = m_intReturnEmployeeId;
                            dtrRow.EndEdit();

                            odaReturnedProductOrder.Update(dtaReturnedProductOrder);
                            dtaReturnedProductOrder.AcceptChanges();
                        }

                        this.ShowSelectedOrderProducts(strOrderId);
                    }
                }
            }
        }

        private void optOrderNumber_Click(object sender, System.EventArgs e)
        {
            // Clear field of past data
            this.ClearFields(true, SearchOrders.All);

            // Clear and disable Employee combo box
            this.cmbOrderedBy.Items.Clear();
            this.cmbOrderedBy.Enabled = false;

            // Clear and disable Supplier combo box
            this.cmbSupplier.Items.Clear();
            this.cmbSupplier.Enabled = false;

            this.GetOrderNumbers(SearchOrders.All);
        }

        private void optSupplier_Click(object sender, System.EventArgs e)
        {
            // Clear fields of past data
            this.ClearFields(true, SearchOrders.BySupplier);

            // Add items to combo box and enable it
            for (int i = 0; i < m_dtaSuppliers.Rows.Count; i++)
                this.cmbSupplier.Items.Add(m_dtaSuppliers.Rows[i]["CompanyName"].ToString());
            this.cmbSupplier.Enabled = true;

            // Clear and disable Employee combo box
            this.cmbOrderedBy.Items.Clear();
            this.cmbOrderedBy.Enabled = false;

            // Clear Order Number listbox
            this.lbxOrderNumber.Items.Clear();
        }
        private void optOrderedBy_Click(object sender, System.EventArgs e)
        {
            DataRow dtrRow;

            // Clear fields of past data
            this.ClearFields(true, SearchOrders.ByEmployee);

            // Clear, add items to and enable combobox
            for (int i = 0; i < m_dtaEmployees.Rows.Count; i++)
            {
                dtrRow = m_dtaEmployees.Rows[i];
                this.cmbOrderedBy.Items.Add(clsUtilities.FormatName_List(dtrRow["Title"].ToString(), dtrRow["FirstName"].ToString(), dtrRow["LastName"].ToString()));
            }
            this.cmbOrderedBy.Enabled = true;

            // Clear and disable Supplier combo box
            this.cmbSupplier.Items.Clear();
            this.cmbSupplier.Enabled = false;

            // Clear Order Number listbox
            this.lbxOrderNumber.Items.Clear();
        }
        
        #endregion
        
        #region Methods
        private void ClearFields(bool blnOptionButton, SearchOrders soSearchBy)
        {
            switch (soSearchBy)
            {
                case SearchOrders.All:
                    this.cmbOrderedBy.Text = "";
                    this.cmbSupplier.Text = "";
                    break;

                case SearchOrders.ByEmployee:
                    this.cmbSupplier.Text = "";
                    if (blnOptionButton)
                        this.cmbOrderedBy.Text = "";
                    break;

                case SearchOrders.BySupplier:
                    this.cmbOrderedBy.Text = "";
                    if (blnOptionButton)
                        this.cmbSupplier.Text = "";

                    break;
            }

            // Listbox & listview
            this.lbxOrderNumber.Items.Clear();
            this.lstViewOrders.Items.Clear();

            // Labels
            this.lblOrderDate_Data.Text = "";
            this.lblCanceledBy_Data.Text = "";
            this.lblCanceledDate_Data.Text = "";
            this.lblReturnNumber_Data.Text = "";
        }

        private void GetOrderNumbers(SearchOrders soSearchBy)
        {
            // Variable declaration
            CultureInfo ciCurrentCulture;
            OleDbDataAdapter odaOrders;
            string strEmployeeId, strSupplierId, strQuery;
            string strStartDate, strEndDate;

            // Variable initialization
            ciCurrentCulture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ciCurrentCulture.DateTimeFormat.DateSeparator = "/";
            m_dtaOrders = new DataTable();
            strQuery = "";

            // Clear order numbers listbox and order listview
            this.lbxOrderNumber.Items.Clear();
            this.lstViewOrders.Items.Clear();

            // Get time period
            strStartDate = this.dtpStart.Value.ToString(clsUtilities.FORMAT_DATE_QUERY, ciCurrentCulture);
            strEndDate = this.dtpEnd.Value.ToString(clsUtilities.FORMAT_DATE_QUERY, ciCurrentCulture);

            // Save current filter for reference by date pickers
            m_soCurrentFilter = soSearchBy;

            switch (soSearchBy)
            {
                case SearchOrders.All:
                    // Select appropriate SQL query
                    switch (m_votFormType)
                    {
                        case ViewOrdersType.ReceivedOrders:
                            strQuery = "SELECT DISTINCT OrderId, OrderDate,FournisseurId, EmployeeId " +
                                       "FROM [Orders] " +
                                       "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND (Checked = 1)" +
                                       "ORDER BY OrderDate";
                            break;

                        case ViewOrdersType.CanceledBackorders:
                            strQuery = "SELECT DISTINCT OrderId, OrderDate, CanceledBODate, FournisseurId, EmployeeId, CanceledBOEmployeeId, CanceledBOUnits " +
                                       "FROM [Orders] " +
                                       "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND CanceledBOUnits > 0 " +
                                       "ORDER BY OrderDate";
                            break;

                        case ViewOrdersType.ReturnedOrders_ReadOnly:
                            strQuery = "SELECT DISTINCT OrderId, OrderDate, FournisseurId, EmployeeId " +
                                           "FROM [Orders] " +
                                           "WHERE OrderId = '" + m_strQueriedOrderNumber + "'";
                            break;

                        case ViewOrdersType.ReturnedOrders:
                            strQuery = "SELECT DISTINCT OrderId, OrderDate, ReturnDate, FournisseurId, EmployeeId, ReturnEmployeeId " +
                                       "FROM [Orders] " +
                                       "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND " +
                                              "(ReturnUnits > 0) ORDER BY OrderDate";
                            break;

                        case ViewOrdersType.NotReceivedOrders:
                            strQuery = "SELECT DISTINCT OrderId, OrderDate,FournisseurId, EmployeeId " +
                                       "FROM [Orders] " +
                                       "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND (Checked = 0) " +
                                       "ORDER BY OrderDate";
                            break;
                    }
                    break;

                case SearchOrders.ByEmployee:
                    // Get selected employee
                    strEmployeeId = m_dtaEmployees.Rows[this.cmbOrderedBy.SelectedIndex]["EmployeeId"].ToString();

                    // Select appropriate SQL query
                    switch (m_votFormType)
                    {

                        case ViewOrdersType.ReceivedOrders:
                        case ViewOrdersType.NotReceivedOrders:
                            strQuery = "SELECT DISTINCT OrderId, OrderDate,FournisseurId, EmployeeId " +
                                       "FROM [Orders] " +
                                       "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND EmployeeId = " + strEmployeeId + " " +
                                       "ORDER BY OrderDate";
                            break;

                        case ViewOrdersType.CanceledBackorders:
                            strQuery = "Select distinct OrderId, OrderDate,FournisseurId, EmployeeId " +
                                       "FROM [Orders] " +
                                       "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND EmployeeId = " + strEmployeeId + " AND CanceledBOUnits > 0 " +
                                       "ORDER BY OrderDate";
                            break;

                        case ViewOrdersType.ReturnedOrders:
                            strQuery = "Select distinct OrderId, OrderDate,FournisseurId, EmployeeId " +
                                       "FROM [Orders] " +
                                       "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND EmployeeId = " + strEmployeeId + " AND ReturnUnits > 0 " +
                                       "ORDER BY OrderDate";
                            break;
                    }
                    break;

                case SearchOrders.BySupplier:
                    // Get selected supplier
                    strSupplierId = m_dtaSuppliers.Rows[this.cmbSupplier.SelectedIndex]["FournisseurId"].ToString();

                    // Select appropriate SQL query
                    switch (m_votFormType)
                    {
                        case ViewOrdersType.ReceivedOrders:
                            strQuery = "SELECT DISTINCT OrderId, OrderDate,FournisseurId, EmployeeId " +
                                       "FROM [Orders] " +
                                       "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND FournisseurId = " + strSupplierId + " " +
                                       "ORDER BY OrderDate";
                            break;

                        case ViewOrdersType.CanceledBackorders:
                            strQuery = "SELECT DISTINCT OrderId, OrderDate,FournisseurId, EmployeeId " +
                                       "FROM [Orders] " +
                                       "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND FournisseurId = " + strSupplierId + " AND CanceledBOUnits > 0 " +
                                       "ORDER BY OrderDate";
                            break;

                        case ViewOrdersType.NotReceivedOrders:
                            strQuery = "SELECT DISTINCT OrderId, OrderDate,FournisseurId, EmployeeId " +
                                       "FROM [Orders] " +
                                       "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND FournisseurId = " + strSupplierId + " " +
                                       "ORDER BY OrderDate";
                            break;

                        case ViewOrdersType.ReturnedOrders:
                            strQuery = "SELECT DISTINCT OrderId, OrderDate,FournisseurId, EmployeeId " +
                                       "FROM [Orders] " +
                                       "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND FournisseurId = " + strSupplierId + " AND ReturnUnits > 0 " +
                                       "ORDER BY OrderDate";
                            break;
                    }
                    break;
            }

            // Get orders from database
            odaOrders = new OleDbDataAdapter(strQuery, m_odcConnection);
            odaOrders.Fill(m_dtaOrders);

            if (m_dtaOrders.Rows.Count != 0)
            {
                for (int i = 0; i < m_dtaOrders.Rows.Count; i++)
                    this.lbxOrderNumber.Items.Add(m_dtaOrders.Rows[i]["OrderId"]);

                this.lbxOrderNumber.SelectedIndex = this.lbxOrderNumber.Items.Count - 1;
            }
            else
                MessageBox.Show("No orders matching the specified criteria were found.", this.Text);
        }

        private string GetEmployee(int intEmployeeId)
        {
            DataRow dtrRow;
            int intCurrentEmployeeId = -1;
            string strEmployee = "";

            for (int i = 0; i < m_dtaEmployees.Rows.Count; i++)
            {
                intCurrentEmployeeId = int.Parse(m_dtaEmployees.Rows[i]["EmployeeId"].ToString());
                if (intCurrentEmployeeId == intEmployeeId)
                {
                    dtrRow = m_dtaEmployees.Rows[i];
                    strEmployee = clsUtilities.FormatName_List(dtrRow["Title"].ToString(), dtrRow["FirstName"].ToString(), dtrRow["LastName"].ToString());
                    break;
                }
            }

            return strEmployee;
        }

        private string GetSupplier(int intSupplierId)
        {
            int intCurrentSupplierId = -1, i;
            string strSupplier = "";

            for (i = 0; i < m_dtaSuppliers.Rows.Count; i++)
            {
                intCurrentSupplierId = int.Parse(m_dtaSuppliers.Rows[i]["FournisseurId"].ToString());
                if (intCurrentSupplierId == intSupplierId)
                {
                    strSupplier = m_dtaSuppliers.Rows[i]["CompanyName"].ToString();
                    break;
                }
            }

            // Set supplier information for fclsOMCheckOrders_ReturnProd
            m_siSupplier.DatabaseID = int.Parse(m_dtaSuppliers.Rows[i]["FournisseurId"].ToString());
            m_siSupplier.Name = m_dtaSuppliers.Rows[i]["CompanyName"].ToString();
            m_siSupplier.ContactName = clsUtilities.FormatName_Display(m_dtaSuppliers.Rows[i]["ConTitle"].ToString(), m_dtaSuppliers.Rows[i]["ContactFirstName"].ToString(), m_dtaSuppliers.Rows[i]["ContactLastName"].ToString());
            m_siSupplier.PhoneNumber = m_dtaSuppliers.Rows[i]["PhoneNumber"].ToString();
            m_siSupplier.FaxNumber = m_dtaSuppliers.Rows[i]["FaxNumber"].ToString();
            m_siSupplier.Email = m_dtaSuppliers.Rows[i]["Email"].ToString();

            return strSupplier;
        }

        /// <summary>
        ///		Function called by fclsOMCheckOrders_ReturnProd in order to return information on the 'return of products to supplier' process.
        /// </summary>
        public void SetReturnedProductInformation(DateTime dtReturnDate, int intReturnEmployeeId, string strReturnNumber)
        {
            m_blnIsReturned = true;
            m_dtReturnDate = dtReturnDate;
            m_intReturnEmployeeId = intReturnEmployeeId; ;
            m_strReturnNumber = strReturnNumber;
        }

        private void ShowSelectedOrder()
		{
			// Variable declaration
			DataRow dtrRow;
			
			// Clear products listview
			this.lstViewOrders.Items.Clear();

			if((this.lbxOrderNumber.Items.Count > 0) && (this.lbxOrderNumber.SelectedIndex != -1))
			{
				// Get the selected order from the datatable
				dtrRow = m_dtaOrders.Rows[this.lbxOrderNumber.SelectedIndex];
				
				// Display the emplyee and/or the supplier associated with the current order
				switch(m_soCurrentFilter)
				{
					case SearchOrders.All:
						this.cmbOrderedBy.Text = this.GetEmployee(int.Parse(dtrRow["EmployeeId"].ToString()));
						this.cmbSupplier.Text = this.GetSupplier(int.Parse(dtrRow["FournisseurId"].ToString()));
					break;
					
					case SearchOrders.ByEmployee:
						this.cmbSupplier.Text = this.GetSupplier(int.Parse(dtrRow["FournisseurId"].ToString()));
					break;
					
					case SearchOrders.BySupplier:
						this.cmbOrderedBy.Text = this.GetEmployee(int.Parse(dtrRow["EmployeeId"].ToString()));
                        
                        // call GetSupplier() so that data in m_siSupplier get updated
                        this.GetSupplier(int.Parse(dtrRow["FournisseurId"].ToString()));
					break;
				}

				// Display the order date
				this.lblOrderDate_Data.Text = ((DateTime) dtrRow["OrderDate"]).ToLongDateString();

				// Display the products from the selected Order
				this.ShowSelectedOrderProducts(dtrRow["OrderId"].ToString());
			}
		}

		private void ShowSelectedOrderProducts(string strOrderId)
		{
			// Variable declaration
			Color clrItemForeColor;
			DataRow dtrRow;
			int intUnits;
			ListViewItem lviItem;
			fclsOMReturnProdCanceledBO.ReturnedProduct rpCurrentProduct;
			OleDbCommandBuilder	ocbOrderProducts;
			OleDbDataAdapter odaOrderProducts;
			string strQuery;

			// Variable initialization
			m_alReturnedProducts = new ArrayList();
			clrItemForeColor = new Color();
			m_dtaOrderProducts = new DataTable();
			intUnits = 0;
			strQuery = "";

			// Clear products listview
			this.lstViewOrders.Items.Clear();
			
			// TODO: make sure SQL queries are right (e.g. why doesn't ViewOrdersType.ReturnedOrders filter with Returned fields?)
			// Select appropriate SQL query
			switch(m_votFormType)
			{
				case ViewOrdersType.ReceivedOrders:
				case ViewOrdersType.NotReceivedOrders:
					strQuery = "SELECT ALL [Products.MatName], [SubProducts.MatName], [Trademarks.Trademark], [Orders.Pack], [Orders.OrderQty], [Orders.Prix], [Orders.OrderId] " +
							   "FROM Products INNER JOIN ((Trademarks INNER JOIN Orders ON Trademarks.MarComId = Orders.MarComId) INNER JOIN SubProducts ON (SubProducts.SubPrId = Orders.SubPrId) AND (Trademarks.MarComId = SubProducts.MarComId)) ON (Orders.MatId = Products.MatId) AND (Products.MatId = SubProducts.MatId) " +
							   "WHERE (((Orders.OrderId)='" + strOrderId + "'))";
				break;

				case ViewOrdersType.CanceledBackorders:
					strQuery = "SELECT ALL [Products.MatName], [SubProducts.MatName], [Trademarks.Trademark], [Orders.Pack], [Orders.CanceledBOUnits], [Orders.Prix], [Orders.CanceledBOEmployeeId], [Orders.CanceledBODate], [Orders.OrderId] " +
							   "FROM Products INNER JOIN ((Trademarks INNER JOIN Orders ON Trademarks.MarComId = Orders.MarComId) INNER JOIN SubProducts ON (SubProducts.SubPrId = Orders.SubPrId) AND (Trademarks.MarComId = SubProducts.MarComId)) ON (Orders.MatId = Products.MatId) AND (Products.MatId = SubProducts.MatId) " +
							   "WHERE (((Orders.OrderId)='" + strOrderId + "'))";
					//this.lstViewOrders.Columns(5).text="Units Cancelled";
				break;

                case ViewOrdersType.ReturnedOrders_ReadOnly:
				case ViewOrdersType.ReturnedOrders:
					strQuery = "SELECT ALL [Products.MatName], [SubProducts.MatName], [Trademarks.Trademark], [Orders.Pack], [Orders.ReturnUnits], [Orders.Prix], [Orders.ReturnNumber], [Orders.ReturnEmployeeId], [Orders.ReturnDate], [Orders.OrderId] " +
							   "FROM Products INNER JOIN ((Trademarks INNER JOIN Orders ON Trademarks.MarComId = Orders.MarComId) INNER JOIN SubProducts ON (SubProducts.SubPrId = Orders.SubPrId) AND (Trademarks.MarComId = SubProducts.MarComId)) ON (Orders.MatId = Products.MatId) AND (Products.MatId = SubProducts.MatId) " +
							   "WHERE (((Orders.OrderId)='" + strOrderId + "'))";
					//this.lstViewOrders..Columns(5).text = "Units Ordered";
				break;
			}
			
			// Get data from database
			odaOrderProducts = new OleDbDataAdapter(strQuery,m_odcConnection);
			ocbOrderProducts = new OleDbCommandBuilder(odaOrderProducts);
			odaOrderProducts.Fill(m_dtaOrderProducts);
			
			// Fill listview
			for (int i=0; i<m_dtaOrderProducts.Rows.Count; i++)
			{
				dtrRow = m_dtaOrderProducts.Rows[i];

				switch(m_votFormType)
				{
					case ViewOrdersType.ReceivedOrders:
					case ViewOrdersType.NotReceivedOrders:
						clrItemForeColor = Color.Black;
						intUnits = int.Parse(dtrRow["Orders.OrderQty"].ToString());
					break;
					
					case ViewOrdersType.CanceledBackorders:
						// Set the appropriate front color for the product
						clrItemForeColor = Color.Black;
						intUnits = int.Parse(dtrRow["Orders.CanceledBOUnits"].ToString());
						if(intUnits == 0)
							clrItemForeColor = Color.LightGray;

						// Get employee who canceled order and the date on which it was canceled
						if(double.Parse(dtrRow["Orders.CanceledBOUnits"].ToString()) > 0)
						{
							this.lblCanceledBy_Data.Text = this.GetEmployee(int.Parse(dtrRow["Orders.CanceledBOEmployeeId"].ToString()));
							this.lblCanceledDate_Data.Text = ((DateTime) dtrRow["Orders.CanceledBODate"]).ToLongDateString();
						}
					break;

                    case ViewOrdersType.ReturnedOrders_ReadOnly:
					case ViewOrdersType.ReturnedOrders:
						// Set the appropriate front color for the product
						clrItemForeColor = Color.Black;
						intUnits = int.Parse(dtrRow["Orders.ReturnUnits"].ToString());
						if(intUnits == 0)
							clrItemForeColor = Color.LightGray;

						// Get employee who canceled order and the date on which it was canceled
						if(double.Parse(dtrRow["Orders.ReturnUnits"].ToString()) > 0)
						{
							this.lblCanceledBy_Data.Text = this.GetEmployee(int.Parse(dtrRow["Orders.ReturnEmployeeId"].ToString()));
							this.lblCanceledDate_Data.Text = ((DateTime) dtrRow["Orders.ReturnDate"]).ToLongDateString();
							this.lblReturnNumber_Data.Text = dtrRow["Orders.ReturnNumber"].ToString();
						}
						
						// populate arraylist that will be used by fclsOMCheckOrders_ReturnProd
						if(intUnits > 0)
						{
							rpCurrentProduct.Product = clsUtilities.FormatProduct_Display(dtrRow["Products.MatName"].ToString(), dtrRow["SubProducts.MatName"].ToString());
							rpCurrentProduct.Trademark = dtrRow["Trademarks.Trademark"].ToString();
							rpCurrentProduct.UnitsReturned = (int) dtrRow["Orders.ReturnUnits"];
							m_alReturnedProducts.Add(rpCurrentProduct);
						}
					break;
				}

				lviItem = lstViewOrders.Items.Add(dtrRow["Products.MatName"].ToString());
				lviItem.SubItems.Add(dtrRow["SubProducts.MatName"].ToString());
				lviItem.SubItems.Add(dtrRow["Trademarks.Trademark"].ToString());
				lviItem.SubItems.Add(decimal.Parse(dtrRow["Orders.Prix"].ToString()).ToString(clsUtilities.FORMAT_CURRENCY));
				lviItem.SubItems.Add(intUnits.ToString());
				lviItem.SubItems.Add(dtrRow["Orders.Pack"].ToString());
				lviItem.ForeColor = clrItemForeColor;
			}
			
			// Set the listview's order column and order the list
			m_lvwColumnSorter.SortColumn = 0;
			m_lvwColumnSorter.Order = SortOrder.Ascending;
        }
        #endregion
	}
}
