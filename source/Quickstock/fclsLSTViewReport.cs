using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;

namespace DSMS
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class fclsLSTViewReport : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnHelp;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private System.Windows.Forms.Label lblTimePeriode;
		public System.Windows.Forms.Label lblFrom;
		public System.Windows.Forms.Label lblTo;
		private System.Windows.Forms.Label lblMinus;
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		public string typeReport;
		public DateTime dtpStart, dtpEnd;
		private System.Windows.Forms.Panel panel1;

		public fclsLSTViewReport()
		{
			InitializeComponent();

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
			this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.btnHelp = new System.Windows.Forms.Button();
			this.lblTimePeriode = new System.Windows.Forms.Label();
			this.lblFrom = new System.Windows.Forms.Label();
			this.lblTo = new System.Windows.Forms.Label();
			this.lblMinus = new System.Windows.Forms.Label();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// crystalReportViewer1
			// 
			this.crystalReportViewer1.ActiveViewIndex = -1;
			this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
			this.crystalReportViewer1.Name = "crystalReportViewer1";
			this.crystalReportViewer1.Size = new System.Drawing.Size(860, 544);
			this.crystalReportViewer1.TabIndex = 0;
			this.crystalReportViewer1.Load += new System.EventHandler(this.crystalReportViewer1_Load);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = System.Windows.Forms.AnchorStyles.Right;
			this.btnHelp.Location = new System.Drawing.Point(728, 48);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(120, 32);
			this.btnHelp.TabIndex = 1;
			this.btnHelp.Text = "Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// lblTimePeriode
			// 
			this.lblTimePeriode.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTimePeriode.Location = new System.Drawing.Point(416, 8);
			this.lblTimePeriode.Name = "lblTimePeriode";
			this.lblTimePeriode.Size = new System.Drawing.Size(72, 16);
			this.lblTimePeriode.TabIndex = 2;
			this.lblTimePeriode.Text = "Time Period:";
			this.lblTimePeriode.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblFrom
			// 
			this.lblFrom.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblFrom.Location = new System.Drawing.Point(504, 8);
			this.lblFrom.Name = "lblFrom";
			this.lblFrom.Size = new System.Drawing.Size(110, 16);
			this.lblFrom.TabIndex = 3;
			this.lblFrom.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblTo
			// 
			this.lblTo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblTo.Location = new System.Drawing.Point(632, 8);
			this.lblTo.Name = "lblTo";
			this.lblTo.Size = new System.Drawing.Size(110, 16);
			this.lblTo.TabIndex = 4;
			this.lblTo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblMinus
			// 
			this.lblMinus.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMinus.Location = new System.Drawing.Point(616, 8);
			this.lblMinus.Name = "lblMinus";
			this.lblMinus.Size = new System.Drawing.Size(16, 16);
			this.lblMinus.TabIndex = 5;
			this.lblMinus.Text = " - ";
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Location = new System.Drawing.Point(742, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(112, 32);
			this.panel1.TabIndex = 6;
			// 
			// fclsLSTViewReport
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(872, 546);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.lblMinus);
			this.Controls.Add(this.lblTo);
			this.Controls.Add(this.lblFrom);
			this.Controls.Add(this.lblTimePeriode);
			this.Controls.Add(this.crystalReportViewer1);
			this.Controls.Add(this.btnHelp);
			this.Name = "fclsLSTViewReport";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quick Stock - View Report";
			this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
			this.ResumeLayout(false);

		}
		#endregion
//============================================================================================
		private void crystalReportViewer1_Load(object sender, System.EventArgs e)
		{
			ParameterValues pvCollection = new ParameterValues();
			ParameterValues pvCollection2 = new ParameterValues();
			ParameterValues pvCollection3 = new ParameterValues();
			ParameterValues pvCollection4 = new ParameterValues();
			ParameterDiscreteValue pdvSubProductID = new ParameterDiscreteValue();
			pdvSubProductID.Value = DSMS.fclsGENInput.subProdId;
			pvCollection.Add(pdvSubProductID);
			ParameterRangeValue rangeVal = new ParameterRangeValue();
			rangeVal.StartValue = dtpStart;
			rangeVal.EndValue = dtpEnd;
			pvCollection2.Add(rangeVal);
			ParameterDiscreteValue pdvStartOrderDate = new ParameterDiscreteValue();
			pdvStartOrderDate.Value = dtpStart;
			pvCollection3.Add(pdvStartOrderDate);
			ParameterDiscreteValue pdvEndOrderDate = new ParameterDiscreteValue();
			pdvEndOrderDate.Value = dtpEnd;
			pvCollection4.Add(pdvEndOrderDate);

			Database crpDatabase;
			Tables crpTables;
			Table crpTable;
			TableLogOnInfo crpTableLogOnInfo;
			ConnectionInfo crpConnectionInfo = new ConnectionInfo();
			crpConnectionInfo.ServerName = Application.StartupPath + "\\DSMS.mdb";
			crpConnectionInfo.DatabaseName = "DSMS";
			crpConnectionInfo.UserID = "";
			crpConnectionInfo.Password = "";

			this.Cursor = Cursors.WaitCursor;
			this.Text = "View Report: Loading Data Please Wait...";
			switch(	typeReport)
			{
				case "btnEmployeeList":
					this.lblFrom.Visible = false;
					this.lblTo.Visible = false;
					this.lblTimePeriode.Visible = false;
					this.lblMinus.Visible = false;

					DSMS.RPTEmployeesList rptEmployees = new RPTEmployeesList();
					crpDatabase = rptEmployees.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}

					crystalReportViewer1.ReportSource = rptEmployees;
					crystalReportViewer1.Zoom(1);
					break;
				case "btnSupplierList":
					this.lblFrom.Visible = false;
					this.lblTo.Visible = false;
					this.lblTimePeriode.Visible = false;
					this.lblMinus.Visible = false;

					DSMS.RPTSuppliersList rptSuppliers = new RPTSuppliersList();
					crpDatabase = rptSuppliers.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}

					crystalReportViewer1.ReportSource = rptSuppliers;
					crystalReportViewer1.Zoom(1);
					break;
				case "btnProdByCateg":
					this.lblFrom.Visible = false;
					this.lblTo.Visible = false;
					this.lblTimePeriode.Visible = false;
					this.lblMinus.Visible = false;

					DSMS.RPTProductByCategory rptProdByCateg = new RPTProductByCategory();
					crpDatabase = rptProdByCateg.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}

					crystalReportViewer1.ReportSource = rptProdByCateg;
					crystalReportViewer1.Zoom(1);
					break;
				case "btnIndexAlphabetic":
					this.lblFrom.Visible = false;
					this.lblTo.Visible = false;
					this.lblTimePeriode.Visible = false;
					this.lblMinus.Visible = false;

					DSMS.RPTIndexAlphabetic rptIndexAlphabetic = new RPTIndexAlphabetic();
					crpDatabase = rptIndexAlphabetic.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}

					crystalReportViewer1.ReportSource = rptIndexAlphabetic;
					crystalReportViewer1.Zoom(1);
					break;
				case "btnRepByCateg":
					this.lblFrom.Visible = true;
					this.lblTo.Visible = true;
					this.lblTimePeriode.Visible = true;
					this.lblMinus.Visible = true;

					DSMS.RPTCategPrint rptPrint = new RPTCategPrint();
					crpDatabase = rptPrint.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}

					rptPrint.DataDefinition.ParameterFields["@startOrderDate"].ApplyCurrentValues(pvCollection3);
					rptPrint.DataDefinition.ParameterFields["@endOrderDate"].ApplyCurrentValues(pvCollection4);
					crystalReportViewer1.ReportSource = rptPrint;
					crystalReportViewer1.Zoom(1);
					break;
				
				case "btnRepByProd":
					this.lblFrom.Visible = true;
					this.lblTo.Visible = true;
					this.lblTimePeriode.Visible = true;
					this.lblMinus.Visible = true;

					DSMS.RPTProdPrint rptPrint2 = new RPTProdPrint();
					crpDatabase = rptPrint2.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}

					rptPrint2.DataDefinition.ParameterFields["@startOrderDate"].ApplyCurrentValues(pvCollection3);
					rptPrint2.DataDefinition.ParameterFields["@endOrderDate"].ApplyCurrentValues(pvCollection4);
					crystalReportViewer1.ReportSource = rptPrint2;
					crystalReportViewer1.Zoom(1);
					break;
				case "btnGraph":
					this.lblFrom.Visible = true;
					this.lblTo.Visible = true;
					this.lblTimePeriode.Visible = true;
					this.lblMinus.Visible = true;

					DSMS.RPTProdGraph rptGraph = new RPTProdGraph();
					crpDatabase = rptGraph.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}

					rptGraph.DataDefinition.ParameterFields["@OrderDate"].ApplyCurrentValues(pvCollection2);
					rptGraph.DataDefinition.ParameterFields["@subProdId"].ApplyCurrentValues(pvCollection);
					crystalReportViewer1.ReportSource = rptGraph;
					crystalReportViewer1.Zoom(2);
					crystalReportViewer1.DisplayGroupTree = false;
					break;
			}
			this.Cursor = Cursors.Arrow;
			this.Text = "View Report";
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			string htmFile = "";
			switch(	typeReport)
			{
				case "btnEmployeeList":
					htmFile = "EmployeeList.htm";
					break;
				case "btnSupplierList":
					htmFile = "SupplierList.htm";
					break;
				case "btnProdByCateg":
					htmFile = "ListPerCategory.htm";
					break;
				case "btnIndexAlphabetic":
					htmFile = "ListAlphabetic.htm";
					break;
				case "btnGraph":
				case "btnRepByProd":
				case "btnRepByCateg":
					htmFile = "Statistics.htm";
					break;
			}
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm",htmFile);  		
		}
//============================================================================================
	}
}
