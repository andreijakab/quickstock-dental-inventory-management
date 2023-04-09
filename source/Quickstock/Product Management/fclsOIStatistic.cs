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
	/// Summary description for Statistic.
	/// </summary>
	public class fclsOIStatistic : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox grpList;
		private System.Windows.Forms.RadioButton optConsum;
		private System.Windows.Forms.RadioButton optExpens;
		private System.Windows.Forms.GroupBox grpDate;
		public System.Windows.Forms.DateTimePicker dtpStart;
		public System.Windows.Forms.DateTimePicker dtpEnd;
		private System.Windows.Forms.Label lblEndDate;
		private System.Windows.Forms.Label lblStartDate;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnRepByCateg;
		private System.Windows.Forms.Button btnRepByProd;
		private System.Windows.Forms.ColumnHeader Product_Name;
		private System.Windows.Forms.ColumnHeader Subproduct_Name;
		private System.Windows.Forms.ColumnHeader Trademark;
		private System.Windows.Forms.ColumnHeader Categorie;
		private System.Windows.Forms.ColumnHeader Quantity;
		private System.Windows.Forms.ColumnHeader TotalPay;
		private System.Windows.Forms.ColumnHeader UnitPrice;
		private System.Windows.Forms.RadioButton optOrdersNumber;
		private System.Windows.Forms.Button btnGraph;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.ListView lstProd;
		private System.ComponentModel.Container components = null;
		/// <summary>
		/// Required designer variable.
		/// </summary>
        private clsListViewColumnSorter m_lvwColumnSorter;
		private DataTable			    m_dtaOrders;
		private OleDbConnection		    m_odcConnection;
		private OleDbDataAdapter	    m_odaOrders;
		private int					    m_intOptionChosen = -1;
		public int					    intColId, nrRecords;
		private string[,]			    strOrderArray;	

		public fclsOIStatistic(OleDbConnection odcConnection)
		{
            NumberFormatInfo nfiNumberFormat;

			InitializeComponent();

            // initialize global constants
            m_lvwColumnSorter = new clsListViewColumnSorter();
            m_odcConnection = odcConnection;            

            // configure listview (sets the listview control's sorter and currency symbol)
            this.lstProd.ListViewItemSorter = m_lvwColumnSorter;
            nfiNumberFormat = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            this.lstProd.Columns[4].Text += " " + nfiNumberFormat.CurrencySymbol;
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
            this.grpList = new System.Windows.Forms.GroupBox();
            this.optOrdersNumber = new System.Windows.Forms.RadioButton();
            this.optExpens = new System.Windows.Forms.RadioButton();
            this.optConsum = new System.Windows.Forms.RadioButton();
            this.grpDate = new System.Windows.Forms.GroupBox();
            this.lblEndDate = new System.Windows.Forms.Label();
            this.lblStartDate = new System.Windows.Forms.Label();
            this.dtpEnd = new System.Windows.Forms.DateTimePicker();
            this.dtpStart = new System.Windows.Forms.DateTimePicker();
            this.btnClose = new System.Windows.Forms.Button();
            this.lstProd = new System.Windows.Forms.ListView();
            this.Product_Name = new System.Windows.Forms.ColumnHeader();
            this.Subproduct_Name = new System.Windows.Forms.ColumnHeader();
            this.Trademark = new System.Windows.Forms.ColumnHeader();
            this.Categorie = new System.Windows.Forms.ColumnHeader();
            this.UnitPrice = new System.Windows.Forms.ColumnHeader();
            this.Quantity = new System.Windows.Forms.ColumnHeader();
            this.TotalPay = new System.Windows.Forms.ColumnHeader();
            this.btnRepByCateg = new System.Windows.Forms.Button();
            this.btnRepByProd = new System.Windows.Forms.Button();
            this.btnGraph = new System.Windows.Forms.Button();
            this.btnHelp = new System.Windows.Forms.Button();
            this.grpList.SuspendLayout();
            this.grpDate.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpList
            // 
            this.grpList.Controls.Add(this.optOrdersNumber);
            this.grpList.Controls.Add(this.optExpens);
            this.grpList.Controls.Add(this.optConsum);
            this.grpList.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.grpList.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpList.Location = new System.Drawing.Point(352, 8);
            this.grpList.Name = "grpList";
            this.grpList.Size = new System.Drawing.Size(256, 96);
            this.grpList.TabIndex = 0;
            this.grpList.TabStop = false;
            // 
            // optOrdersNumber
            // 
            this.optOrdersNumber.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optOrdersNumber.Location = new System.Drawing.Point(8, 64);
            this.optOrdersNumber.Name = "optOrdersNumber";
            this.optOrdersNumber.Size = new System.Drawing.Size(232, 24);
            this.optOrdersNumber.TabIndex = 2;
            this.optOrdersNumber.Text = "Cumulative Number of Orders";
            this.optOrdersNumber.Click += new System.EventHandler(this.optOrdersNumber_Click);
            // 
            // optExpens
            // 
            this.optExpens.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optExpens.Location = new System.Drawing.Point(8, 40);
            this.optExpens.Name = "optExpens";
            this.optExpens.Size = new System.Drawing.Size(232, 24);
            this.optExpens.TabIndex = 1;
            this.optExpens.Text = "Cumulative Product Expenses";
            this.optExpens.Click += new System.EventHandler(this.optExpens_Click);
            // 
            // optConsum
            // 
            this.optConsum.Checked = true;
            this.optConsum.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optConsum.Location = new System.Drawing.Point(8, 16);
            this.optConsum.Name = "optConsum";
            this.optConsum.Size = new System.Drawing.Size(232, 24);
            this.optConsum.TabIndex = 0;
            this.optConsum.TabStop = true;
            this.optConsum.Text = "Cumulative Product Consumption";
            this.optConsum.Click += new System.EventHandler(this.optConsum_Click);
            // 
            // grpDate
            // 
            this.grpDate.Controls.Add(this.lblEndDate);
            this.grpDate.Controls.Add(this.lblStartDate);
            this.grpDate.Controls.Add(this.dtpEnd);
            this.grpDate.Controls.Add(this.dtpStart);
            this.grpDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.grpDate.Location = new System.Drawing.Point(16, 8);
            this.grpDate.Name = "grpDate";
            this.grpDate.Size = new System.Drawing.Size(280, 72);
            this.grpDate.TabIndex = 1;
            this.grpDate.TabStop = false;
            this.grpDate.Text = "Time Periode";
            // 
            // lblEndDate
            // 
            this.lblEndDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEndDate.Location = new System.Drawing.Point(24, 42);
            this.lblEndDate.Name = "lblEndDate";
            this.lblEndDate.Size = new System.Drawing.Size(88, 16);
            this.lblEndDate.TabIndex = 3;
            this.lblEndDate.Text = "End Date";
            this.lblEndDate.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblStartDate
            // 
            this.lblStartDate.Font = new System.Drawing.Font("Arial", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
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
            this.dtpStart.Value = new System.DateTime(2000, 1, 1, 0, 0, 0, 0);
            this.dtpStart.CloseUp += new System.EventHandler(this.dtpStart_CloseUp);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(736, 472);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lstProd
            // 
            this.lstProd.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.Product_Name,
            this.Subproduct_Name,
            this.Trademark,
            this.Categorie,
            this.UnitPrice,
            this.Quantity,
            this.TotalPay});
            this.lstProd.FullRowSelect = true;
            this.lstProd.Location = new System.Drawing.Point(16, 112);
            this.lstProd.Name = "lstProd";
            this.lstProd.Size = new System.Drawing.Size(960, 352);
            this.lstProd.TabIndex = 4;
            this.lstProd.View = System.Windows.Forms.View.Details;
            this.lstProd.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lstProd_ColumnClick);
            this.lstProd.Click += new System.EventHandler(this.lstProd_Click);
            // 
            // Product_Name
            // 
            this.Product_Name.Text = "Product Name";
            this.Product_Name.Width = 235;
            // 
            // Subproduct_Name
            // 
            this.Subproduct_Name.Text = "Subproduct Name";
            this.Subproduct_Name.Width = 251;
            // 
            // Trademark
            // 
            this.Trademark.Text = "Trademark";
            this.Trademark.Width = 100;
            // 
            // Categorie
            // 
            this.Categorie.Text = "Categorie";
            this.Categorie.Width = 120;
            // 
            // UnitPrice
            // 
            this.UnitPrice.Text = "Unit Price";
            this.UnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.UnitPrice.Width = 80;
            // 
            // Quantity
            // 
            this.Quantity.Text = "Quantity";
            this.Quantity.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.Quantity.Width = 72;
            // 
            // TotalPay
            // 
            this.TotalPay.Text = "Total Pay";
            this.TotalPay.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.TotalPay.Width = 65;
            // 
            // btnRepByCateg
            // 
            this.btnRepByCateg.Location = new System.Drawing.Point(400, 472);
            this.btnRepByCateg.Name = "btnRepByCateg";
            this.btnRepByCateg.Size = new System.Drawing.Size(144, 23);
            this.btnRepByCateg.TabIndex = 5;
            this.btnRepByCateg.Text = "Show Report by Category";
            this.btnRepByCateg.Click += new System.EventHandler(this.btnRepByCateg_Click);
            // 
            // btnRepByProd
            // 
            this.btnRepByProd.Location = new System.Drawing.Point(568, 472);
            this.btnRepByProd.Name = "btnRepByProd";
            this.btnRepByProd.Size = new System.Drawing.Size(144, 23);
            this.btnRepByProd.TabIndex = 6;
            this.btnRepByProd.Text = "Show Report by Product";
            this.btnRepByProd.Click += new System.EventHandler(this.btnRepByProd_Click);
            // 
            // btnGraph
            // 
            this.btnGraph.Location = new System.Drawing.Point(272, 472);
            this.btnGraph.Name = "btnGraph";
            this.btnGraph.Size = new System.Drawing.Size(112, 24);
            this.btnGraph.TabIndex = 7;
            this.btnGraph.Text = "Product Graph";
            this.btnGraph.Visible = false;
            this.btnGraph.Click += new System.EventHandler(this.btnGraph_Click);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(840, 472);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(75, 23);
            this.btnHelp.TabIndex = 8;
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // fclsOIStatistic
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(984, 502);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.btnGraph);
            this.Controls.Add(this.btnRepByProd);
            this.Controls.Add(this.btnRepByCateg);
            this.Controls.Add(this.lstProd);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.grpDate);
            this.Controls.Add(this.grpList);
            this.Name = "fclsOIStatistic";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Stock - Statistics";
            this.Load += new System.EventHandler(this.fclsOIStatistic_Load);
            this.grpList.ResumeLayout(false);
            this.grpDate.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion
//============================================================================================
		private void fclsOIStatistic_Load(object sender, System.EventArgs e)
		{
			// Create the ToolTip and associate with the Form container.
			ToolTip toolTip1 = new ToolTip();

			// Set up the delays for the ToolTip.
			toolTip1.AutoPopDelay = 5000;
			toolTip1.InitialDelay = 1000;
			toolTip1.ReshowDelay = 500;
			// Force the ToolTip text to be displayed whether or not the form is active.
			toolTip1.ShowAlways = true;
      
			// Set up the ToolTip text for the Button and Checkbox.
			toolTip1.SetToolTip(this.lstProd, "Click on a product to view the order evolution in time.");
			
			optConsum_Click(null, null);
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","Statistics.htm");  //

		}
//============================================================================================
		private void optConsum_Click(object sender, System.EventArgs e)
		{
			m_intOptionChosen = 1;
			this.Update_Data();

/*			double	dMan, dPrix;
			double	dManQ, dQuantity;
			int		nrProd = 0, i, j;
			int		subPrId, subPrIdPrev;

//			Buid up the table with Orders for Ordered Quantity
			if (!this.loadOrders())
			{
				this.lstProd.Items.Clear();
				return;
			}
			strOrderArray = new string[nrRecords,7];
			
			j=-1;
			subPrIdPrev = -1;
			dMan = 0;
			dPrix = 0;
			dManQ = 0;
			dQuantity = 0;
			intColId = 6;

			DataRow dtrOrders;
			for (i = 0; i < nrRecords; i++)
			{
				dtrOrders = m_dtaOrders.Rows[i];
				subPrId = int.Parse(dtrOrders["SubProduct ID"].ToString());
				if(subPrId == subPrIdPrev)
				{
					dMan = double.Parse(dtrOrders["UnitPrice"].ToString());
					if(dMan > 0 && dMan < dPrix)
						dPrix = dMan;
					dManQ = double.Parse(dtrOrders["Quantity"].ToString());
					dQuantity += dManQ;

				}
				else
				{
					if(j >=0)
					{
						strOrderArray[j,4] = dPrix.ToString("#,##0.00");
						strOrderArray[j,5] = dQuantity.ToString("#,##0.00");
					}
					++j;
					strOrderArray[j,0] = dtrOrders["Product Name"].ToString();
					strOrderArray[j,1] = dtrOrders["Subproduct Name"].ToString();
					strOrderArray[j,2] = dtrOrders["Trademark"].ToString();
					strOrderArray[j,3] = dtrOrders["Categorie"].ToString();
					strOrderArray[j,6] = dtrOrders["SubProduct ID"].ToString();
					dPrix = double.Parse(dtrOrders["UnitPrice"].ToString());
					dQuantity = double.Parse(dtrOrders["Quantity"].ToString());
					subPrIdPrev = subPrId;
				}
			}	

			nrProd = j;
			strOrderArray[j,4] = dPrix.ToString("#,##0.00");
			strOrderArray[j,5] = dQuantity.ToString("#,##0.00");

			sortArray(strOrderArray, 6, nrProd, 7);
			sortArray1(strOrderArray, 6, nrProd, 7);
			sortArray2(strOrderArray, 6, nrProd, 7);

			this.lstProd.Items.Clear();
			lstProd.Columns[4].Text = "Unit Price";
			lstProd.Columns[5].Text = "Quantity";
			lstProd.Columns[6].Text = "";
		
			ListViewItem lviItem;
			for (i = 0; i <= nrProd; i++)
			{
				lviItem = lstProd.Items.Add(strOrderArray[i,0]);
				//lviItem.ForeColor = foreColor;
				lviItem.SubItems.Add(strOrderArray[i,1]);
				lviItem.SubItems.Add(strOrderArray[i,2]);
				lviItem.SubItems.Add(strOrderArray[i,3]);
				lviItem.SubItems.Add(strOrderArray[i,4]);
				lviItem.SubItems.Add(strOrderArray[i,5]);
			}*/			
		}
//============================================================================================
		private void optExpens_Click(object sender, System.EventArgs e)
		{
			m_intOptionChosen = 2;
			this.Update_Data();

/*			double	dMan, dPrix;
			double	dManC, dCatPay;
			double	dManT, dTotPay;
			int		nrProd = 0, i, j;
			int		subPrId, subPrIdPrev;
//				Buid up the table with Orders for Expenses
			strOrderArray = new string[nrRecords,8];

			j=-1;
			subPrIdPrev = -1;
			dMan = 0;
			dPrix = 0;
			dManC = 0;
			dCatPay = 0;
			dManT = 0;
			dTotPay = 0;
			intColId = 7;

			DataRow dtrOrders;
			for (i = 0; i < nrRecords; i++)
			{
				dtrOrders = m_dtaOrders.Rows[i];
				subPrId = int.Parse(dtrOrders["SubProduct ID"].ToString());
				if(subPrId == subPrIdPrev)
				{
					dMan = double.Parse(dtrOrders["UnitPrice"].ToString());
					if(dMan > 0 && dMan < dPrix)
						dPrix = dMan;
					dManC = double.Parse(dtrOrders["CatalogPay"].ToString());
					dCatPay += dManC;
					dManT = double.Parse(dtrOrders["TotalPay"].ToString());
					dTotPay += dManT;

				}
				else
				{
					if(j >=0)
					{
						strOrderArray[j,4] = dPrix.ToString("#,##0.00");
						strOrderArray[j,5] = dCatPay.ToString("#,##0.00");
						strOrderArray[j,6] = dTotPay.ToString("#,##0.00");
					}
					++j;
					strOrderArray[j,0] = dtrOrders["Product Name"].ToString();
					strOrderArray[j,1] = dtrOrders["Subproduct Name"].ToString();
					strOrderArray[j,2] = dtrOrders["Trademark"].ToString();
					strOrderArray[j,3] = dtrOrders["Categorie"].ToString();
					strOrderArray[j,7] = dtrOrders["SubProduct ID"].ToString();
					dPrix = double.Parse(dtrOrders["UnitPrice"].ToString());
					dCatPay = double.Parse(dtrOrders["CatalogPay"].ToString());
					dTotPay = double.Parse(dtrOrders["TotalPay"].ToString());
					subPrIdPrev = subPrId;
				}
			}	

			nrProd = j;
			strOrderArray[j,4] = dPrix.ToString("#,##0.00");
			strOrderArray[j,5] = dCatPay.ToString("#,##0.00");
			strOrderArray[j,6] = dTotPay.ToString("#,##0.00");

			sortArray(strOrderArray, 7, nrProd, 8);
			sortArray1(strOrderArray, 7, nrProd, 8);
			sortArray2(strOrderArray, 7, nrProd, 8);

			this.lstProd.Items.Clear();
			lstProd.Columns[4].Text = "Unit Price";
			lstProd.Columns[5].Text = "Catalog Pay";
			lstProd.Columns[6].Text = "Total Pay";
	
			ListViewItem lviItem;
			for (i = 0; i <= nrProd; i++)
			{
				lviItem = lstProd.Items.Add(strOrderArray[i,0]);
				//lviItem.ForeColor = foreColor;
				lviItem.SubItems.Add(strOrderArray[i,1]);
				lviItem.SubItems.Add(strOrderArray[i,2]);
				lviItem.SubItems.Add(strOrderArray[i,3]);
				lviItem.SubItems.Add(strOrderArray[i,4]);
				lviItem.SubItems.Add(strOrderArray[i,5]);
				lviItem.SubItems.Add(strOrderArray[i,6]);
			}	*/		

		}
//============================================================================================
		private void optOrdersNumber_Click(object sender, System.EventArgs e)
		{
			m_intOptionChosen = 3;
			this.Update_Data();

			/*int		nrProd = 0, i, j, k;
			int		subPrId, subPrIdPrev;

//				Buid up the table with Orders for Orders number
			if (!this.loadOrders())
			{
				this.lstProd.Items.Clear();
				return;
			}
			strOrderArray = new string[nrRecords,6];
			
			j=-1;
			subPrIdPrev = -1;
			k = 0;
			intColId = 5;

			DataRow dtrOrders;
			for (i = 0; i < nrRecords; i++)
			{
				dtrOrders = m_dtaOrders.Rows[i];
				subPrId = int.Parse(dtrOrders["SubProduct ID"].ToString());
				if(subPrId == subPrIdPrev)
				{
					++k;
				}
				else
				{
					if(j >=0)
						strOrderArray[j,4] = k.ToString();
					++j;
					k = 1;
					strOrderArray[j,0] = dtrOrders["Product Name"].ToString();
					strOrderArray[j,1] = dtrOrders["Subproduct Name"].ToString();
					strOrderArray[j,2] = dtrOrders["Trademark"].ToString();
					strOrderArray[j,3] = dtrOrders["Categorie"].ToString();
					strOrderArray[j,5] = dtrOrders["SubProduct ID"].ToString();
					subPrIdPrev = subPrId;
				}
			}	
			nrProd = j;
			strOrderArray[j,4] = k.ToString();

			sortArray(strOrderArray, 5, nrProd, 6);
			sortArray1(strOrderArray, 5, nrProd, 6);
			sortArray2(strOrderArray, 5, nrProd, 6);

			this.lstProd.Items.Clear();
			lstProd.Columns[4].Text = "No. of Orders";
			lstProd.Columns[5].Text = "";
			lstProd.Columns[6].Text = "";
		
			ListViewItem lviItem;
			for (i = 0; i <= nrProd; i++)
			{
				lviItem = lstProd.Items.Add(strOrderArray[i,0]);
				//lviItem.ForeColor = foreColor;
				lviItem.SubItems.Add(strOrderArray[i,1]);
				lviItem.SubItems.Add(strOrderArray[i,2]);
				lviItem.SubItems.Add(strOrderArray[i,3]);
				lviItem.SubItems.Add(strOrderArray[i,4]);
			}*/			
		}
//============================================================================================
		private void btnRepByCateg_Click(object sender, System.EventArgs e)
		{
			fclsLSTViewReport frmLSTViewReport = new fclsLSTViewReport();
			frmLSTViewReport.lblFrom.Text = dtpStart.Text.ToString();
			frmLSTViewReport.lblTo.Text = dtpEnd.Text.ToString();
			frmLSTViewReport.dtpStart = this.dtpStart.Value;
			frmLSTViewReport.dtpEnd = this.dtpEnd.Value;
			frmLSTViewReport.typeReport = "btnRepByCateg";
			frmLSTViewReport.ShowDialog();
		}

		private void btnRepByProd_Click(object sender, System.EventArgs e)
		{
			fclsLSTViewReport frmLSTViewReport = new fclsLSTViewReport();
			frmLSTViewReport.lblFrom.Text = dtpStart.Text.ToString();
			frmLSTViewReport.lblTo.Text = dtpEnd.Text.ToString();
			frmLSTViewReport.dtpStart = this.dtpStart.Value;
			frmLSTViewReport.dtpEnd = this.dtpEnd.Value;
			frmLSTViewReport.typeReport = "btnRepByProd";
			frmLSTViewReport.ShowDialog();		
		}
//============================================================================================
		private void lstProd_Click(object sender, System.EventArgs e)
		{
			string prodName;
			int prodId;

			ListView.SelectedIndexCollection index = lstProd.SelectedIndices;
			foreach(int m_int_clickedProd in index)
			{
				prodName = strOrderArray[m_int_clickedProd,1];
				prodId = int.Parse(strOrderArray[m_int_clickedProd, intColId]);

				DSMS.fclsGENInput.subProdName = prodName;
				DSMS.fclsGENInput.subProdId = prodId;
				btnGraph_Click(null, null);
			}
		}

		private void btnGraph_Click(object sender, System.EventArgs e)
		{
			fclsLSTViewReport frmLSTViewReport = new fclsLSTViewReport();
			frmLSTViewReport.lblFrom.Text = dtpStart.Text.ToString();
			frmLSTViewReport.lblTo.Text = dtpEnd.Text.ToString();
			frmLSTViewReport.dtpStart = this.dtpStart.Value;
			frmLSTViewReport.dtpEnd = this.dtpEnd.Value;
			frmLSTViewReport.typeReport = "btnGraph";
			frmLSTViewReport.ShowDialog();		
		}
//============================================================================================
		private bool loadOrders()
		{
            CultureInfo ciCurrentCulture;
			string strQueryStartDate, strQueryEndDate;

            // Variable initialization
            ciCurrentCulture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
            ciCurrentCulture.DateTimeFormat.DateSeparator = "/";

            strQueryStartDate = this.dtpStart.Value.ToString(clsUtilities.FORMAT_DATE_QUERY, ciCurrentCulture);
            strQueryEndDate = this.dtpEnd.Value.ToString(clsUtilities.FORMAT_DATE_QUERY, ciCurrentCulture);

			/*			m_odaOrders = new OleDbDataAdapter("SELECT TOP 25 Products.MatName AS [Product Name], SubProducts.MatName AS [Subproduct Name], SubProducts.PrixMin AS UnitPrice, Trademarks.Trademark, " +

			string strLast2Digit = startDate.ToString("yy");	//string strLast2Digit = "05";					
			m_odaOrders = new OleDbDataAdapter("SELECT DISTINCT(Orders.OrderDate) " +
				"FROM Orders " +
				"WHERE (Orders.OrderDate) >= #" + strdtpStart + "# AND (Orders.OrderDate) Like  \'%" + strLast2Digit + "\' " +
				"ORDER BY Orders.OrderDate", odcConnection);
*/				
			m_odaOrders = new OleDbDataAdapter("SELECT Orders.OrderDate AS oDate, Products.MatName AS [Product Name], SubProducts.MatName AS [Subproduct Name], SubProducts.SubPrId AS [SubProduct ID], " +
				"Categories.CategName AS Categorie, Trademarks.Trademark, Orders.Prix AS UnitPrice, Orders.ReceivedQty AS Quantity, Orders.CatalogPay AS CatalogPay, Orders.TotalPay AS TotalPay " +
				"FROM Trademarks INNER JOIN (Categories INNER JOIN ((Orders INNER JOIN Products ON Orders.MatId = Products.MatId) INNER JOIN SubProducts ON (Products.MatId = SubProducts.MatId) AND " +
				"(Orders.SubPrId = SubProducts.SubPrId)) ON (Categories.CategoryId = Orders.CategoryId) AND (Categories.CategoryId = Products.CategoryId)) ON (Trademarks.MarComId = SubProducts.MarComId) AND " +
				"(Trademarks.MarComId = Orders.MarComId) " +
				"WHERE (((Orders.OrderDate) BETWEEN #" + strQueryStartDate + "# AND #" + strQueryEndDate + "#)) AND Orders.CatalogPay > 0.0 " +
				"ORDER BY SubProducts.SubPrId, Orders.OrderDate", m_odcConnection);
			m_dtaOrders = new DataTable();
			try
			{
				m_odaOrders.Fill(m_dtaOrders);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			nrRecords = m_dtaOrders.Rows.Count;
			if(nrRecords <= 0)
			{
				MessageBox.Show("There are no orders for this time period!");
				return false;
			}
			else
				return true;
		}

		private void sortArray(string[,] strArray, int colToSort, int linMax, int colMax)
		{
			string	strMan;
			int		intValue1, intValue2;
			double	dbValue1, dbValue2;
			for(int fin=0; fin<=linMax; fin++)
				for (int ini=fin+1; ini<=linMax; ini++)
				{
					if(colToSort == 5)
					{
						intValue1 = int.Parse(strArray[fin, colToSort-1].ToString());
						intValue2 = int.Parse(strArray[ini, colToSort-1].ToString());
						if(intValue1 < intValue2)
							for(int j=0; j<colMax; j++)
							{
								strMan = strArray[ini, j];
								strArray[ini, j] = strArray[fin, j];
								strArray[fin, j] = strMan;
							}
					}
					else
					{
						dbValue1 = double.Parse(strArray[fin, colToSort-1]);
						dbValue2 = double.Parse(strArray[ini, colToSort-1]);
						if(dbValue1 < dbValue2)
							for(int j=0; j<colMax; j++)
							{
								strMan = strArray[ini, j];
								strArray[ini, j] = strArray[fin, j];
								strArray[fin, j] = strMan;
							}
					}
				}
		}

		private void sortArray1(string[,] strArray, int colToSort, int linMax, int colMax)
		{
			string strMan;
			for(int fin=0; fin<=linMax; fin++)
				for (int ini=fin+1; ini<=linMax; ini++)
					if(string.Compare(strArray[fin, colToSort-1], strArray[ini, colToSort-1]) == 0)
						if(string.Compare(strArray[fin, 0], strArray[ini, 0]) > 0)
							for(int j=0; j<colMax; j++)
							{
								strMan = strArray[ini, j];
								strArray[ini, j] = strArray[fin, j];
								strArray[fin, j] = strMan;
							}
		}

		private void sortArray2(string[,] strArray, int colToSort, int linMax, int colMax)
		{
			string strMan;
			for(int fin=0; fin<=linMax; fin++)
				for (int ini=fin+1; ini<=linMax; ini++)
					if(string.Compare(strArray[fin, colToSort-1], strArray[ini, colToSort-1]) == 0)
						if(string.Compare(strArray[fin, 0], strArray[ini, 0]) == 0)
							if(string.Compare(strArray[fin, 1], strArray[ini, 1]) > 0)
								for(int j=0; j<colMax; j++)
								{
									strMan = strArray[ini, j];
									strArray[ini, j] = strArray[fin, j];
									strArray[fin, j] = strMan;
								}
		}

		private void dtpStart_ValueChanged(object sender, System.EventArgs e)
		{
			optConsum_Click(null,null);
			Update_Data();
		}

		private void Update_Data()
		{
			DataRow dtrOrders = null;
			double	dMan = 0, dManC = 0, dManQ = 0, dManT = 0;
			double	dPrix = 0, dCatPay = 0, dTotPay = 0, dQuantity = 0;
			int		nrProd = 0, i, j = -1, k = 0;
			int		subPrId, subPrIdPrev = -1;
			ListViewItem lviItem = null;

			switch(this.m_intOptionChosen)
			{
				// Cumulative Product consumption
				case 1:
					//			Buid up the table with Orders for Ordered Quantity
					if (!this.loadOrders())
					{
						this.lstProd.Items.Clear();
						return;
					}
					strOrderArray = new string[nrRecords,7];
			
					intColId = 6;

					for (i = 0; i < nrRecords; i++)
					{
						dtrOrders = m_dtaOrders.Rows[i];
						subPrId = int.Parse(dtrOrders["SubProduct ID"].ToString());
						if(subPrId == subPrIdPrev)
						{
							dMan = double.Parse(dtrOrders["UnitPrice"].ToString());
							if(dMan > 0.0 && dMan < dPrix)
								dPrix = dMan;
							dManQ = double.Parse(dtrOrders["Quantity"].ToString());
							dQuantity += dManQ;

						}
						else
						{
							if(j >=0)
							{
								strOrderArray[j,4] = dPrix.ToString("#,##0.00");
								strOrderArray[j,5] = dQuantity.ToString("#,##0.00");
							}
							++j;
							strOrderArray[j,0] = dtrOrders["Product Name"].ToString();
							strOrderArray[j,1] = dtrOrders["Subproduct Name"].ToString();
							strOrderArray[j,2] = dtrOrders["Trademark"].ToString();
							strOrderArray[j,3] = dtrOrders["Categorie"].ToString();
							strOrderArray[j,6] = dtrOrders["SubProduct ID"].ToString();
							dPrix = double.Parse(dtrOrders["UnitPrice"].ToString());
							dQuantity = double.Parse(dtrOrders["Quantity"].ToString());
							subPrIdPrev = subPrId;
						}
					}	

					nrProd = j;
					strOrderArray[j,4] = dPrix.ToString("#,##0.00");
					strOrderArray[j,5] = dQuantity.ToString("#,##0.00");

					sortArray(strOrderArray, 6, nrProd, 7);
					sortArray1(strOrderArray, 6, nrProd, 7);
					sortArray2(strOrderArray, 6, nrProd, 7);

					this.lstProd.Items.Clear();
					lstProd.Columns[4].Text = "Unit Price";
					lstProd.Columns[5].Text = "Quantity";
					lstProd.Columns[6].Text = "";
		
					for (i = 0; i <= nrProd; i++)
					{
						lviItem = lstProd.Items.Add(strOrderArray[i,0]);
						//lviItem.ForeColor = foreColor;
						lviItem.SubItems.Add(strOrderArray[i,1]);
						lviItem.SubItems.Add(strOrderArray[i,2]);
						lviItem.SubItems.Add(strOrderArray[i,3]);
						lviItem.SubItems.Add(strOrderArray[i,4]);
						lviItem.SubItems.Add(strOrderArray[i,5]);
					}			
				break;
				
				// Cumulative Product expenses
				case 2:
					//				Buid up the table with Orders for Expenses
					strOrderArray = new string[nrRecords,8];

					intColId = 7;

					for (i = 0; i < nrRecords; i++)
					{
						dtrOrders = m_dtaOrders.Rows[i];
						subPrId = int.Parse(dtrOrders["SubProduct ID"].ToString());
						if(subPrId == subPrIdPrev)
						{
							dMan = double.Parse(dtrOrders["UnitPrice"].ToString());
							if(dMan > 0 && dMan < dPrix)
								dPrix = dMan;
							dManC = double.Parse(dtrOrders["CatalogPay"].ToString());
							dCatPay += dManC;
							dManT = double.Parse(dtrOrders["TotalPay"].ToString());
							dTotPay += dManT;

						}
						else
						{
							if(j >=0)
							{
								strOrderArray[j,4] = dPrix.ToString("#,##0.00");
								strOrderArray[j,5] = dCatPay.ToString("#,##0.00");
								strOrderArray[j,6] = dTotPay.ToString("#,##0.00");
							}
							++j;
							strOrderArray[j,0] = dtrOrders["Product Name"].ToString();
							strOrderArray[j,1] = dtrOrders["Subproduct Name"].ToString();
							strOrderArray[j,2] = dtrOrders["Trademark"].ToString();
							strOrderArray[j,3] = dtrOrders["Categorie"].ToString();
							strOrderArray[j,7] = dtrOrders["SubProduct ID"].ToString();
							dPrix = double.Parse(dtrOrders["UnitPrice"].ToString());
							dCatPay = double.Parse(dtrOrders["CatalogPay"].ToString());
							dTotPay = double.Parse(dtrOrders["TotalPay"].ToString());
							subPrIdPrev = subPrId;
						}
					}	

					nrProd = j;
					strOrderArray[j,4] = dPrix.ToString("#,##0.00");
					strOrderArray[j,5] = dCatPay.ToString("#,##0.00");
					strOrderArray[j,6] = dTotPay.ToString("#,##0.00");

					sortArray(strOrderArray, 7, nrProd, 8);
					sortArray1(strOrderArray, 7, nrProd, 8);
					sortArray2(strOrderArray, 7, nrProd, 8);

					this.lstProd.Items.Clear();
					lstProd.Columns[4].Text = "Unit Price";
					lstProd.Columns[5].Text = "Catalog Pay";
					lstProd.Columns[6].Text = "Total Pay";
	
					for (i = 0; i <= nrProd; i++)
					{
						lviItem = lstProd.Items.Add(strOrderArray[i,0]);
						//lviItem.ForeColor = foreColor;
						lviItem.SubItems.Add(strOrderArray[i,1]);
						lviItem.SubItems.Add(strOrderArray[i,2]);
						lviItem.SubItems.Add(strOrderArray[i,3]);
						lviItem.SubItems.Add(strOrderArray[i,4]);
						lviItem.SubItems.Add(strOrderArray[i,5]);
						lviItem.SubItems.Add(strOrderArray[i,6]);
					}			
				break;
				
				// Number of Orders
				case 3:
					//				Buid up the table with Orders for Orders number
					if (!this.loadOrders())
					{
						this.lstProd.Items.Clear();
						return;
					}
					strOrderArray = new string[nrRecords,6];
			
					intColId = 5;

					for (i = 0; i < nrRecords; i++)
					{
						dtrOrders = m_dtaOrders.Rows[i];
						subPrId = int.Parse(dtrOrders["SubProduct ID"].ToString());
						if(subPrId == subPrIdPrev)
						{
							++k;
						}
						else
						{
							if(j >=0)
								strOrderArray[j,4] = k.ToString();
							++j;
							k = 1;
							strOrderArray[j,0] = dtrOrders["Product Name"].ToString();
							strOrderArray[j,1] = dtrOrders["Subproduct Name"].ToString();
							strOrderArray[j,2] = dtrOrders["Trademark"].ToString();
							strOrderArray[j,3] = dtrOrders["Categorie"].ToString();
							strOrderArray[j,5] = dtrOrders["SubProduct ID"].ToString();
							subPrIdPrev = subPrId;
						}
					}	
					nrProd = j;
					strOrderArray[j,4] = k.ToString();

					sortArray(strOrderArray, 5, nrProd, 6);
					sortArray1(strOrderArray, 5, nrProd, 6);
					sortArray2(strOrderArray, 5, nrProd, 6);

					this.lstProd.Items.Clear();
					lstProd.Columns[4].Text = "No. of Orders";
					lstProd.Columns[5].Text = "";
					lstProd.Columns[6].Text = "";
		
					for (i = 0; i <= nrProd; i++)
					{
						lviItem = lstProd.Items.Add(strOrderArray[i,0]);
						//lviItem.ForeColor = foreColor;
						lviItem.SubItems.Add(strOrderArray[i,1]);
						lviItem.SubItems.Add(strOrderArray[i,2]);
						lviItem.SubItems.Add(strOrderArray[i,3]);
						lviItem.SubItems.Add(strOrderArray[i,4]);
					}	
				break;
			}
		}

		private void dtpStart_CloseUp(object sender, System.EventArgs e)
		{
			this.Update_Data();
		}

		private void dtpEnd_CloseUp(object sender, System.EventArgs e)
		{
			this.Update_Data();
		}

        private void lstProd_ColumnClick(object sender, ColumnClickEventArgs e)
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
            this.lstProd.Sort();
        }
	}
}
