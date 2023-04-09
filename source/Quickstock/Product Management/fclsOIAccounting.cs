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
	/// Summary description for Form1.
	/// </summary>
	public class fclsOIAccounting : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.RadioButton optAllPayment;
		private System.Windows.Forms.RadioButton optPayed;
		private System.Windows.Forms.RadioButton optNonPayed;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.ListView lstViewPayments;
		private System.Windows.Forms.ColumnHeader OrderId;
		private System.Windows.Forms.ColumnHeader OrderDate;
		private System.Windows.Forms.ColumnHeader PaymentDate;
		private System.Windows.Forms.ColumnHeader Tax;
		private System.Windows.Forms.ColumnHeader Transport;
		private System.Windows.Forms.ColumnHeader Duty;
		private System.Windows.Forms.ColumnHeader TotalPay;
		private System.Windows.Forms.ColumnHeader SupplierName;
		private System.Windows.Forms.ColumnHeader SumDue;
		private System.Windows.Forms.ColumnHeader Penalty;
		private System.Windows.Forms.ColumnHeader PayedPer;
		private System.Windows.Forms.ColumnHeader PayedByWho;
		private System.Windows.Forms.ColumnHeader CatalogPay;
		private System.Windows.Forms.GroupBox gpbOrderSelection;
		private System.Windows.Forms.RadioButton rbtnTimePeriod;
		private System.Windows.Forms.RadioButton rbtnAllOrders;
		private System.Windows.Forms.GroupBox grpDate;
		private System.Windows.Forms.Label lblEndDate;
        private System.Windows.Forms.Label lblStartDate;
        private DateTimePicker dtpEnd;
        private DateTimePicker dtpStart;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public static int			m_intCheckPayment = 0;	// 0 without payment	1 with payment
		public static int			m_intPayedBy;
		public static string		m_strPaymentDate, m_strPayedSum, m_strPayedPenalty, m_strPayedPer;
		public static DateTime		m_dtPaymentDate;
		public string				strdtpStart, strdtpEnd;
        		
		public enum FilterType : int {OrdersToBePaid, FullyPaidOrders, PaymentHistory};

		private bool					m_blnOrderPaid;
		private clsListViewColumnSorter m_lvwColumnSorter;
		private FilterType				m_fltCurrentFilter;
		private OleDbConnection			m_odcConnection;
        private string                  m_strQueriedOrderNumber;
		private ToolTip					m_ttpListViewToolTip;

		//public fclsOIAccounting(int oldYear, int intOption, OleDbConnection odcConnection)
        public fclsOIAccounting(FilterType ftFilter, string strQueriedOrderNumber, OleDbConnection odcConnection)
		{
			m_odcConnection = odcConnection;
            m_strQueriedOrderNumber = strQueriedOrderNumber;
            m_fltCurrentFilter = ftFilter;
			m_lvwColumnSorter = new clsListViewColumnSorter();

			InitializeComponent();
            			
			// Create and configure tool tip for the listview
			m_ttpListViewToolTip = new ToolTip();
			m_ttpListViewToolTip.AutoPopDelay = 5000;
			m_ttpListViewToolTip.InitialDelay = 1000;
			m_ttpListViewToolTip.ReshowDelay = 500;
			m_ttpListViewToolTip.ShowAlways = true;						// Forces the ToolTip text to be displayed whether or not the form is active.

			this.dtpStart.Value = new DateTime(DateTime.Now.Year,1,1);
			this.dtpEnd.Text = System.DateTime.Now.ToShortDateString();

			// Sets the listview control's sorter
			this.lstViewPayments.ListViewItemSorter = m_lvwColumnSorter;
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
            this.btnClose = new System.Windows.Forms.Button();
            this.optAllPayment = new System.Windows.Forms.RadioButton();
            this.optPayed = new System.Windows.Forms.RadioButton();
            this.optNonPayed = new System.Windows.Forms.RadioButton();
            this.lstViewPayments = new System.Windows.Forms.ListView();
            this.OrderId = new System.Windows.Forms.ColumnHeader();
            this.OrderDate = new System.Windows.Forms.ColumnHeader();
            this.SupplierName = new System.Windows.Forms.ColumnHeader();
            this.PaymentDate = new System.Windows.Forms.ColumnHeader();
            this.CatalogPay = new System.Windows.Forms.ColumnHeader();
            this.Tax = new System.Windows.Forms.ColumnHeader();
            this.Transport = new System.Windows.Forms.ColumnHeader();
            this.Duty = new System.Windows.Forms.ColumnHeader();
            this.SumDue = new System.Windows.Forms.ColumnHeader();
            this.Penalty = new System.Windows.Forms.ColumnHeader();
            this.TotalPay = new System.Windows.Forms.ColumnHeader();
            this.PayedPer = new System.Windows.Forms.ColumnHeader();
            this.PayedByWho = new System.Windows.Forms.ColumnHeader();
            this.btnHelp = new System.Windows.Forms.Button();
            this.gpbOrderSelection = new System.Windows.Forms.GroupBox();
            this.rbtnTimePeriod = new System.Windows.Forms.RadioButton();
            this.rbtnAllOrders = new System.Windows.Forms.RadioButton();
            this.grpDate = new System.Windows.Forms.GroupBox();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.gpbOrderSelection.SuspendLayout();
            this.grpDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnClose
            // 
            this.btnClose.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnClose.Location = new System.Drawing.Point(856, 488);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(112, 24);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // optAllPayment
            // 
            this.optAllPayment.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optAllPayment.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.optAllPayment.Location = new System.Drawing.Point(720, 56);
            this.optAllPayment.Name = "optAllPayment";
            this.optAllPayment.Size = new System.Drawing.Size(136, 32);
            this.optAllPayment.TabIndex = 2;
            this.optAllPayment.Text = "Payments history";
            this.optAllPayment.Click += new System.EventHandler(this.optAllPayment_Click);
            // 
            // optPayed
            // 
            this.optPayed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optPayed.ForeColor = System.Drawing.Color.Green;
            this.optPayed.Location = new System.Drawing.Point(536, 56);
            this.optPayed.Name = "optPayed";
            this.optPayed.Size = new System.Drawing.Size(136, 32);
            this.optPayed.TabIndex = 3;
            this.optPayed.Text = "Fully paid orders";
            this.optPayed.Click += new System.EventHandler(this.optPayed_Click);
            // 
            // optNonPayed
            // 
            this.optNonPayed.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optNonPayed.ForeColor = System.Drawing.Color.Red;
            this.optNonPayed.Location = new System.Drawing.Point(376, 56);
            this.optNonPayed.Name = "optNonPayed";
            this.optNonPayed.Size = new System.Drawing.Size(136, 32);
            this.optNonPayed.TabIndex = 4;
            this.optNonPayed.Text = "Orders to be paid";
            this.optNonPayed.Click += new System.EventHandler(this.optNonPayed_Click);
            // 
            // lstViewPayments
            // 
            this.lstViewPayments.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.lstViewPayments.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.OrderId,
            this.OrderDate,
            this.SupplierName,
            this.PaymentDate,
            this.CatalogPay,
            this.Tax,
            this.Transport,
            this.Duty,
            this.SumDue,
            this.Penalty,
            this.TotalPay,
            this.PayedPer,
            this.PayedByWho});
            this.lstViewPayments.FullRowSelect = true;
            this.lstViewPayments.HideSelection = false;
            this.lstViewPayments.Location = new System.Drawing.Point(0, 144);
            this.lstViewPayments.MultiSelect = false;
            this.lstViewPayments.Name = "lstViewPayments";
            this.lstViewPayments.Size = new System.Drawing.Size(973, 328);
            this.lstViewPayments.TabIndex = 6;
            this.lstViewPayments.UseCompatibleStateImageBehavior = false;
            this.lstViewPayments.View = System.Windows.Forms.View.Details;
            this.lstViewPayments.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstViewPayments_ColumnClick);
            this.lstViewPayments.Click += new System.EventHandler(this.lstViewPayments_Click);
            // 
            // OrderId
            // 
            this.OrderId.Text = "OrderId";
            this.OrderId.Width = 50;
            // 
            // OrderDate
            // 
            this.OrderDate.Text = "Order Date";
            this.OrderDate.Width = 81;
            // 
            // SupplierName
            // 
            this.SupplierName.Text = "Supplier";
            this.SupplierName.Width = 130;
            // 
            // PaymentDate
            // 
            this.PaymentDate.Text = "Pay Date";
            this.PaymentDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PaymentDate.Width = 78;
            // 
            // CatalogPay
            // 
            this.CatalogPay.Text = "Item(s) Price";
            this.CatalogPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.CatalogPay.Width = 71;
            // 
            // Tax
            // 
            this.Tax.Text = "Tax";
            this.Tax.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Tax.Width = 52;
            // 
            // Transport
            // 
            this.Transport.Text = "Transport";
            this.Transport.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Transport.Width = 63;
            // 
            // Duty
            // 
            this.Duty.Text = "Duty";
            this.Duty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Duty.Width = 54;
            // 
            // SumDue
            // 
            this.SumDue.Text = "Sum Due";
            this.SumDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.SumDue.Width = 62;
            // 
            // Penalty
            // 
            this.Penalty.Text = "Penalty";
            this.Penalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Penalty.Width = 57;
            // 
            // TotalPay
            // 
            this.TotalPay.Text = "Payed";
            this.TotalPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // PayedPer
            // 
            this.PayedPer.Text = "Payed Per";
            this.PayedPer.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PayedPer.Width = 114;
            // 
            // PayedByWho
            // 
            this.PayedByWho.Text = "Payed By";
            this.PayedByWho.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.PayedByWho.Width = 107;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(760, 488);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 7;
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // gpbOrderSelection
            // 
            this.gpbOrderSelection.Controls.Add(this.rbtnTimePeriod);
            this.gpbOrderSelection.Controls.Add(this.rbtnAllOrders);
            this.gpbOrderSelection.Controls.Add(this.grpDate);
            this.gpbOrderSelection.Location = new System.Drawing.Point(8, 8);
            this.gpbOrderSelection.Name = "gpbOrderSelection";
            this.gpbOrderSelection.Size = new System.Drawing.Size(352, 128);
            this.gpbOrderSelection.TabIndex = 11;
            this.gpbOrderSelection.TabStop = false;
            this.gpbOrderSelection.Text = "Order Selection";
            // 
            // rbtnTimePeriod
            // 
            this.rbtnTimePeriod.Checked = true;
            this.rbtnTimePeriod.Location = new System.Drawing.Point(8, 40);
            this.rbtnTimePeriod.Name = "rbtnTimePeriod";
            this.rbtnTimePeriod.Size = new System.Drawing.Size(104, 24);
            this.rbtnTimePeriod.TabIndex = 13;
            this.rbtnTimePeriod.TabStop = true;
            this.rbtnTimePeriod.Text = "By Time Period";
            this.rbtnTimePeriod.CheckedChanged += new System.EventHandler(this.rbtnTimePeriod_CheckedChanged);
            // 
            // rbtnAllOrders
            // 
            this.rbtnAllOrders.Location = new System.Drawing.Point(8, 96);
            this.rbtnAllOrders.Name = "rbtnAllOrders";
            this.rbtnAllOrders.Size = new System.Drawing.Size(104, 24);
            this.rbtnAllOrders.TabIndex = 12;
            this.rbtnAllOrders.Text = "All Orders";
            this.rbtnAllOrders.CheckedChanged += new System.EventHandler(this.rbtnAllOrders_CheckedChanged);
            // 
            // grpDate
            // 
            this.grpDate.Controls.Add(this.lblEndDate);
            this.grpDate.Controls.Add(this.lblStartDate);
            this.grpDate.Controls.Add(this.dtpEnd);
            this.grpDate.Controls.Add(this.dtpStart);
            this.grpDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDate.Location = new System.Drawing.Point(112, 16);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(232, 72);
            this.grpDate.TabIndex = 11;
            this.grpDate.TabStop = false;
            // 
            // lblEndDate
            // 
            this.lblEndDate.AutoSize = true;
            this.lblEndDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(8, 43);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(57, 15);
            this.lblEndDate.TabIndex = 3;
            this.lblEndDate.Text = "End Date";
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStartDate
            // 
            this.lblStartDate.AutoSize = true;
            this.lblStartDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStartDate.Location = new System.Drawing.Point(8, 19);
            this.lblStartDate.Name = "lblStartDate";
            this.lblStartDate.Size = new System.Drawing.Size(64, 15);
            this.lblStartDate.TabIndex = 2;
            this.lblStartDate.Text = "Start Date";
            this.lblStartDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // dtpEnd
            // 
            this.dtpEnd.Location = new System.Drawing.Point(72, 40);
            this.dtpEnd.Name = "dtpEnd";
            this.dtpEnd.Size = new System.Drawing.Size(152, 22);
            this.dtpEnd.TabIndex = 1;
            this.dtpEnd.ValueChanged += new System.EventHandler(this.dtpEnd_ValueChanged);
            // 
            // dtpStart
            // 
            this.dtpStart.Location = new System.Drawing.Point(72, 16);
            this.dtpStart.Name = "dtpStart";
            this.dtpStart.Size = new System.Drawing.Size(152, 22);
            this.dtpStart.TabIndex = 0;
            this.dtpStart.Value = new System.DateTime(2005, 1, 1, 0, 0, 0, 0);
            this.dtpStart.ValueChanged += new System.EventHandler(this.dtpStart_ValueChanged);
            // 
            // fclsOIAccounting
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(976, 526);
            this.Controls.Add(this.gpbOrderSelection);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lstViewPayments);
            this.Controls.Add(this.optNonPayed);
            this.Controls.Add(this.optPayed);
            this.Controls.Add(this.optAllPayment);
            this.Controls.Add(this.btnClose);
            this.Name = "fclsOIAccounting";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Stock - Accounting";
            this.Load += new System.EventHandler(this.fclsOIAccounting_Load);
            this.gpbOrderSelection.ResumeLayout(false);
            this.grpDate.ResumeLayout(false);
            this.grpDate.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion

//============================================================================================
		private void fclsOIAccounting_Load(object sender, System.EventArgs e)
		{
            if (m_strQueriedOrderNumber != null && m_strQueriedOrderNumber.Length > 0)
            {
                this.rbtnAllOrders.Select();

                //
                // make form 'read only'
                //
                this.Text += " (Viewing Only)";

                this.dtpEnd.Text = "";
                this.dtpStart.Text = "";

                this.gpbOrderSelection.Enabled = false;
                this.optAllPayment.Enabled = false;
                this.optNonPayed.Enabled = false;
                this.optPayed.Enabled = false;
            }
            else
            {
                // Select appropriate radio button
                switch (m_fltCurrentFilter)
                {
                    //	Select 'Payment history'
                    case FilterType.PaymentHistory:
                        this.optAllPayment.Select();
                    break;

                    //	Select 'Fully paid orders'
                    case FilterType.FullyPaidOrders:
                        this.optPayed.Select();
                    break;

                    //	Select 'Orders to be paid'
                    case FilterType.OrdersToBePaid:
                        this.optNonPayed.Select();
                    break;
                }
            }
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","Accounting.htm");  //
		}
//============================================================================================
		private void optNonPayed_Click(object sender, System.EventArgs e)
		{
			this.LoadAndDisplayData(FilterType.OrdersToBePaid);
		}

		private void optPayed_Click(object sender, System.EventArgs e)
		{
			this.LoadAndDisplayData(FilterType.FullyPaidOrders);
		}

		private void optAllPayment_Click(object sender, System.EventArgs e)
		{
			this.LoadAndDisplayData(FilterType.PaymentHistory);
		}

		public void PayTheOrder(int m_intKey)
		{
			double dblSumDue;
			DataRow dtrNewRow;
			DataRow[] dtrFoundRows;
			DataTable dtaOrderPayment;
			OleDbCommandBuilder odcbOrderPayment;
			OleDbDataAdapter oddaOrderPayment;
			
			dtaOrderPayment = new DataTable();

			try
			{
				oddaOrderPayment = new OleDbDataAdapter("SELECT * FROM OrderPayment WHERE (checkPayment = 0) ORDER BY OrderId", m_odcConnection);
				odcbOrderPayment = new OleDbCommandBuilder(oddaOrderPayment);
				oddaOrderPayment.Fill(dtaOrderPayment);

				// Update the initial order
				dtrFoundRows = dtaOrderPayment.Select("key = " + m_intKey);
				dtrFoundRows[0]["checkPayment"] = "1";
				oddaOrderPayment.Update(dtaOrderPayment);
				dtaOrderPayment.AcceptChanges();

				// Create a new order line with the new data
				dtrNewRow = dtaOrderPayment.NewRow();
				dtrNewRow["OrderId"] = dtrFoundRows[0]["OrderId"].ToString();
				dtrNewRow["PaymentDate"] = m_dtPaymentDate.ToShortDateString();
				dtrNewRow["PayedBy"] = m_intPayedBy;
				dtrNewRow["SubTotal"] = dtrFoundRows[0]["SubTotal"].ToString();
				dtrNewRow["Tax"] = dtrFoundRows[0]["Tax"].ToString();
				dtrNewRow["Transport"] = dtrFoundRows[0]["Transport"].ToString();
				dtrNewRow["Duty"] = dtrFoundRows[0]["Duty"].ToString();
				dtrNewRow["TotalPay"] = (double.Parse(dtrFoundRows[0]["TotalPay"].ToString()) + double.Parse(m_strPayedSum)).ToString();
				dtrNewRow["Penalty"] = (double.Parse(dtrFoundRows[0]["Penalty"].ToString()) + double.Parse(m_strPayedPenalty)).ToString();
				dtrNewRow["PayedPer"] = m_strPayedPer;
				dblSumDue = double.Parse(dtrFoundRows[0]["SumDue"].ToString()) - double.Parse(m_strPayedSum);
				dtrNewRow["SumDue"] = dblSumDue.ToString();;
				
				if(dblSumDue == 0.0)
					dtrNewRow["checkPayment"] = "1";
				else
					dtrNewRow["checkPayment"] = "0";

				// Add the new row to the table
				dtaOrderPayment.Rows.Add(dtrNewRow);
				oddaOrderPayment.Update(dtaOrderPayment);
				dtaOrderPayment.AcceptChanges();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace,this.Text);
			}
		}

		/// <summary>
		///		Function called by fclsOI_Accounting_Pay in order to return payment information for the current order.
		/// </summary>
		public void SetPaymentInformation(bool blnOrderPaid, DateTime dtPaymentDate, string strAmoundPaid, string strPenalty, string strPaymentMethod, int intPayerEmployeeId)
		{
			m_blnOrderPaid = blnOrderPaid;

			// TODO: figure out this crap!
			if(m_blnOrderPaid)
				m_intCheckPayment = 1;
			else
				m_intCheckPayment = 0;

			m_dtPaymentDate = dtPaymentDate;
			m_strPayedSum = strAmoundPaid;
			m_strPayedPenalty = strPenalty;
			m_strPayedPer = strPaymentMethod;
			m_intPayedBy = intPayerEmployeeId;
		}

		private void LoadAndDisplayData(FilterType fltOption)
		{
            CultureInfo ciCurrentCulture;
			DataTable dtaAccounting;
			AccountingListViewItem alviItem;
			OleDbDataAdapter odaAccounting;
			string strWHEREClause;
			
			// Variable initialization
            ciCurrentCulture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ciCurrentCulture.DateTimeFormat.DateSeparator = "/";
			strWHEREClause = "";
			dtaAccounting = new DataTable();
			
			// Clear listview
			lstViewPayments.Items.Clear();
			
			// Select the appropriate WHERE clause
			switch(fltOption)
			{
				case FilterType.PaymentHistory:
					strWHEREClause = "";
					m_ttpListViewToolTip.SetToolTip(this.lstViewPayments, "");
				break;
				
				case FilterType.FullyPaidOrders:
					strWHEREClause = " WHERE (OrderPayment.checkPayment = 1) AND (OrderPayment.SumDue = 0)";
					m_ttpListViewToolTip.SetToolTip(this.lstViewPayments, "");
				break;
				
				case FilterType.OrdersToBePaid:
					strWHEREClause = " WHERE (OrderPayment.checkPayment = 0)";
					m_ttpListViewToolTip.SetToolTip(this.lstViewPayments, "Click on an order to make the payment!");
				break;		
			}
			
			// Filter by date, if necessary
            if (this.rbtnTimePeriod.Checked)
            {
                if (strWHEREClause.Length > 0)
                    strWHEREClause += " AND ";
                else
                    strWHEREClause = " WHERE ";
                strWHEREClause += "(Orders.OrderDate BETWEEN #" + this.dtpStart.Value.ToString(clsUtilities.FORMAT_DATE_QUERY, ciCurrentCulture)
                                + "# AND #" + this.dtpEnd.Value.ToString(clsUtilities.FORMAT_DATE_QUERY, ciCurrentCulture) + "#)";
            }
            else
            {
                if (m_strQueriedOrderNumber != null && m_strQueriedOrderNumber.Length > 0)
                {
                    if (strWHEREClause.Length > 0)
                        strWHEREClause += " AND ";
                    else
                        strWHEREClause = " WHERE ";
                    strWHEREClause += "OrderPayment.OrderId ='" + m_strQueriedOrderNumber + "'"; ;
                }
            }

			// Load data
			try
			{
				// Get data from database and store it in a DataTable
				odaAccounting = new OleDbDataAdapter("SELECT DISTINCT OrderPayment.*, Orders.OrderDate, Suppliers.CompanyName, (Employees.Title+' '+Employees.FirstName+' '+Employees.LastName) AS PayedByWho " +
														"FROM Employees INNER JOIN (Suppliers INNER JOIN (Orders INNER JOIN OrderPayment ON Orders.OrderId = OrderPayment.OrderId) ON Suppliers.FournisseurId = Orders.FournisseurId) ON Employees.EmployeeId = OrderPayment.PayedBy" + strWHEREClause, m_odcConnection);
				odaAccounting.Fill(dtaAccounting);
			
				// Check if any orders were found
				if(dtaAccounting.Rows.Count > 0)
				{	
					// Add each item represented by a datarow to the list view
					foreach (DataRow dtrRow in dtaAccounting.Rows)
					{
						alviItem = new AccountingListViewItem(dtrRow["OrderId"].ToString(),
																((DateTime) dtrRow["OrderDate"]).ToString("yyyy-MM-dd"),
																dtrRow["CompanyName"].ToString(),
																((DateTime) dtrRow["PaymentDate"]).ToString("yyyy-MM-dd"),
																double.Parse(dtrRow["SubTotal"].ToString()),
																double.Parse(dtrRow["Tax"].ToString()),
																double.Parse(dtrRow["Transport"].ToString()),
																double.Parse(dtrRow["Duty"].ToString()),
																double.Parse(dtrRow["SumDue"].ToString()),
																double.Parse(dtrRow["Penalty"].ToString()),
																double.Parse(dtrRow["TotalPay"].ToString()),
																dtrRow["PayedPer"].ToString(),
																dtrRow["PayedByWho"].ToString(),
																int.Parse(dtrRow["key"].ToString()));
				
						this.lstViewPayments.Items.Add(alviItem);
					}
				}

				m_lvwColumnSorter.SortColumn = 0;
				m_lvwColumnSorter.Order = SortOrder.Ascending;
			
				m_fltCurrentFilter = fltOption;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace,this.Text);
			}
		}

		private void lstViewPayments_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
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
			this.lstViewPayments.Sort();			
		}

		private void rbtnTimePeriod_CheckedChanged(object sender, System.EventArgs e)
		{
			if(this.rbtnTimePeriod.Checked)
			{
				this.grpDate.Enabled = true;
				this.LoadAndDisplayData(m_fltCurrentFilter);
			}
			else
				this.grpDate.Enabled = false;
		}

		private void rbtnAllOrders_CheckedChanged(object sender, System.EventArgs e)
		{
			this.LoadAndDisplayData(m_fltCurrentFilter);
		}

		private void dtpStart_ValueChanged(object sender, System.EventArgs e)
		{
			this.LoadAndDisplayData(m_fltCurrentFilter);		
		}

		private void dtpEnd_ValueChanged(object sender, System.EventArgs e)
		{
			this.LoadAndDisplayData(m_fltCurrentFilter);
		}

		private void lstViewPayments_Click(object sender, System.EventArgs e)
		{
			AccountingListViewItem alviSelectedItem;

			if((m_fltCurrentFilter == FilterType.OrdersToBePaid) && (this.lstViewPayments.SelectedItems.Count > 0))
			{
				// If order has already been paid, reset flag and exit function
				if(m_intCheckPayment == 1)
				{
					m_intCheckPayment = 0;
					return;
				}
		
				// TODO: REMOVE! (Used by fclsOIAccounting_Pay.cs
				fclsGENInput.indPayFrom = 1;
				alviSelectedItem = (AccountingListViewItem) this.lstViewPayments.SelectedItems[0];
				fclsOIAccounting_Pay frmOIAccountingPay = new fclsOIAccounting_Pay(fclsOIAccounting_Pay.Caller.Accounting,this,alviSelectedItem.Text,-1,decimal.Parse(alviSelectedItem.SubItems[8].Text),m_odcConnection);
				frmOIAccountingPay.ShowDialog();
				if(m_intCheckPayment == 1)
				{
					this.PayTheOrder(alviSelectedItem.Key);
					this.LoadAndDisplayData(m_fltCurrentFilter);
				}
			}
		}
	}
	
	
	internal class AccountingListViewItem:ListViewItem
	{	
		private int m_intKey;

		public AccountingListViewItem(string strOrderID, string strOrderDate, string strCompanyName, string strPaymentDate, double dblSubTotal, double dblTax, double dblTransport, double dblDuty, double dblSumDue, double dblPenalty, double dblTotalPay, string strPayedPer, string strPayedBy, int intKey)
		{
			this.Text = strOrderID;

			// Set the line's color depending on the amount due
			this.ForeColor= Color.Green;
			if(dblSumDue > 0.0)
				this.ForeColor = Color.Red;
			
			this.SubItems.Add(strOrderDate);
			this.SubItems.Add(strCompanyName);
			this.SubItems.Add(strPaymentDate);
			this.SubItems.Add(dblSubTotal.ToString("#,##0.00"));
			this.SubItems.Add(dblTax.ToString("#,##0.00"));
			this.SubItems.Add(dblTransport.ToString("#,##0.00"));
			this.SubItems.Add(dblDuty.ToString("#,##0.00"));
			this.SubItems.Add(dblSumDue.ToString("#,##0.00"));
			this.SubItems.Add(dblPenalty.ToString("#,##0.00"));
			this.SubItems.Add(dblTotalPay.ToString("#,##0.00"));
			this.SubItems.Add(strPayedPer);
			this.SubItems.Add(strPayedBy);
			
			m_intKey = intKey;
		}
		
		public int Key
		{
			get
			{
				return m_intKey;
			}
		}
	}
}
