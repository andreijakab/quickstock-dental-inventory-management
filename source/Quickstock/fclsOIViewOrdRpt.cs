using System;
using System.IO;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using CrystalDecisions.Shared;
using CrystalDecisions.CrystalReports.Engine;
using System.Data;
using System.Data.OleDb;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsSIViewOrdRpt.
	/// </summary>
	public class fclsOIViewOrdRpt : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Button btnClose;
		private CrystalDecisions.Windows.Forms.CrystalReportViewer crystalReportViewer1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		public System.Windows.Forms.Button btnSendEmail;
		private System.Windows.Forms.Button btnPrint;
        private System.Windows.Forms.Panel panel1;

		public enum ViewOrderReportCaller:int {ExpressOrder, PreviousOrder, Tender, RegularOrder, CanceledBackorder, ReturnedOrders};

        private bool                                m_blnOrderSent, m_blnTenderSent, m_blnCancelationSent;
		private ViewOrderReportCaller				m_vorcCaller;
		private Form								m_frmCaller;
        private OleDbDataAdapter                    m_oddaDataset;
        private OleDbConnection						m_odcConnection;
        private RPTViewOrder                        m_rptNewOrder;
        private RPTViewTender                       m_rptTender;
        private RPTViewPrevOrder                    m_rptPrevOrder;
        private RPTViewCancelation                  m_rptCancelation;
        private RPTViewReturnedOrders               m_rptReturnedOrders;
        private ReportClass                         m_rptCurrentReport;
		private string								m_strOrderNumber;
        private SupplierInformation                 m_siSupplier;


		public fclsOIViewOrdRpt(Form frmCaller, ViewOrderReportCaller vorcCaller, OleDbConnection odcConnection)
		{
			InitializeComponent();

			m_frmCaller = frmCaller;
			m_vorcCaller = vorcCaller;
			m_odcConnection = odcConnection;

            m_blnOrderSent = m_blnTenderSent = m_blnCancelationSent = false;

            this.DialogResult = DialogResult.Cancel;
		}

		private void fclsOIViewOrdRpt_Load(object sender, System.EventArgs e)
		{
			switch(m_vorcCaller)
			{
				case ViewOrderReportCaller.ExpressOrder:
				case ViewOrderReportCaller.RegularOrder:
					this.Text = "Quick Stock - Preview Order";
					this.btnPrint.Text = "Print";
				break;

				case ViewOrderReportCaller.PreviousOrder:
					this.Text = "Quick Stock - View Sent Order";
				break;

				case ViewOrderReportCaller.Tender:
					this.Text = "Quick Stock - Preview Tender";
					this.btnPrint.Text = "Print";
				break;
				
				case ViewOrderReportCaller.CanceledBackorder:
					this.Text = "Quick Stock - Preview Cancelled Backorder";
					// TODO: Ask Apu why
					this.btnPrint.Text = "Print & Fax Later";
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
			this.btnClose = new System.Windows.Forms.Button();
			this.crystalReportViewer1 = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
			this.btnSendEmail = new System.Windows.Forms.Button();
			this.btnPrint = new System.Windows.Forms.Button();
			this.panel1 = new System.Windows.Forms.Panel();
			this.SuspendLayout();
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(904, 704);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(104, 24);
			this.btnClose.TabIndex = 0;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// crystalReportViewer1
			// 
			this.crystalReportViewer1.ActiveViewIndex = -1;
			this.crystalReportViewer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.crystalReportViewer1.DisplayGroupTree = false;
			this.crystalReportViewer1.EnableDrillDown = false;
			this.crystalReportViewer1.Location = new System.Drawing.Point(0, 0);
			this.crystalReportViewer1.Name = "crystalReportViewer1";
			this.crystalReportViewer1.ShowCloseButton = false;
			this.crystalReportViewer1.ShowExportButton = false;
			this.crystalReportViewer1.ShowGroupTreeButton = false;
			this.crystalReportViewer1.ShowPrintButton = false;
			this.crystalReportViewer1.ShowRefreshButton = false;
			this.crystalReportViewer1.Size = new System.Drawing.Size(1020, 696);
			this.crystalReportViewer1.TabIndex = 1;
			this.crystalReportViewer1.Load += new System.EventHandler(this.crystalReportViewer1_Load);
			// 
			// btnSendEmail
			// 
			this.btnSendEmail.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSendEmail.Location = new System.Drawing.Point(784, 704);
			this.btnSendEmail.Name = "btnSendEmail";
			this.btnSendEmail.Size = new System.Drawing.Size(104, 24);
			this.btnSendEmail.TabIndex = 2;
			this.btnSendEmail.Text = "Send by Email";
			this.btnSendEmail.Click += new System.EventHandler(this.btnSendbyEmail_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrint.Location = new System.Drawing.Point(664, 704);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(104, 24);
			this.btnPrint.TabIndex = 13;
			this.btnPrint.Text = "Print";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// panel1
			// 
			this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.panel1.Location = new System.Drawing.Point(912, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(104, 32);
			this.panel1.TabIndex = 14;
			// 
			// fclsOIViewOrdRpt
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1014, 732);
			this.Controls.Add(this.panel1);
			this.Controls.Add(this.crystalReportViewer1);
			this.Controls.Add(this.btnPrint);
			this.Controls.Add(this.btnSendEmail);
			this.Controls.Add(this.btnClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "fclsOIViewOrdRpt";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quick Stock - ViewReports";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.fclsOIViewOrdRpt_Closing);
			this.Load += new System.EventHandler(this.fclsOIViewOrdRpt_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnSendbyEmail_Click(object sender, System.EventArgs e)
		{
            int i = 0;
			string strFilePath = null;

            switch(m_vorcCaller)
			{
				case ViewOrderReportCaller.ExpressOrder:
				case ViewOrderReportCaller.RegularOrder:
                    strFilePath = clsConfiguration.Internal_DataFilesPath +
                                  "\\Orders\\Order " +
                                  m_strOrderNumber +
                                  " - " +
                                  DateTime.Now.ToString(clsUtilities.FORMAT_DATE_ORDERED) +
                                  ".pdf";
				break;
				
				case ViewOrderReportCaller.PreviousOrder:
                    strFilePath = clsConfiguration.Internal_DataFilesPath +
                                  "\\Orders\\Order " +
                                  m_strOrderNumber +
                                  " - " +
                                  DateTime.Now.ToString(clsUtilities.FORMAT_DATE_ORDERED) +
                                  ".pdf";
                    
                    // determine a unique name if another order file exists for today's date
                    while (File.Exists(strFilePath))
                    {
                        strFilePath = clsConfiguration.Internal_DataFilesPath +
                                      "\\Orders\\Order " +
                                      m_strOrderNumber +
                                      " - " +
                                      DateTime.Now.ToString(clsUtilities.FORMAT_DATE_ORDERED) +
                                      " - " +
                                      (++i).ToString() +
                                      ".pdf";
                    }
				break;
				
				case ViewOrderReportCaller.Tender:
                    strFilePath = clsConfiguration.Internal_DataFilesPath +
                                  "\\Tenders\\Tender - " +
                                  DateTime.Now.ToString(clsUtilities.FORMAT_DATE_ORDERED) +
                                  ".pdf";

                    // determine a unique name if another order file exists for today's date
                    while (File.Exists(strFilePath))
                    {
                        strFilePath = clsConfiguration.Internal_DataFilesPath +
                                      "\\Tenders\\Tender - " +
                                      DateTime.Now.ToString(clsUtilities.FORMAT_DATE_ORDERED) +
                                      " - " +
                                      (++i).ToString() +
                                      ".pdf";
                    }
				break;
				
				case ViewOrderReportCaller.CanceledBackorder:
                    strFilePath = clsConfiguration.Internal_DataFilesPath +
                                  "\\CancelledBackOrders\\CancelledBO " +
                                  m_strOrderNumber +
                                  ".pdf";
				break;
			}
            
            // export currently loaded report to PDF
            ExportOptions rptExportOptions = m_rptCurrentReport.ExportOptions;
            DiskFileDestinationOptions rptFileDestinationOptions = new DiskFileDestinationOptions();
            rptFileDestinationOptions.DiskFileName = strFilePath;
            rptExportOptions.DestinationOptions = rptFileDestinationOptions;
            rptExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
            rptExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
            m_rptCurrentReport.Export();

            // email PDF file

            fclsGENSendEmail frmGENSendEmail = new fclsGENSendEmail(fclsGENSendEmail.EmailType.Order);
            frmGENSendEmail.NewEmail(m_siSupplier,
                                     rptFileDestinationOptions.DiskFileName.ToString());
            if (frmGENSendEmail.ShowDialog() == DialogResult.OK)
            {
                // if the "Order Backup" option has not been selected, delete .pdf file
                if (!clsConfiguration.General_BackupOrders)
                    File.Delete(strFilePath);
                
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
                File.Delete(strFilePath);
		}

		private void crystalReportViewer1_Load(object sender, System.EventArgs e)
		{
			ParameterValues pvCollection = new ParameterValues();
			ParameterDiscreteValue pdvOrderId = new ParameterDiscreteValue();
			pdvOrderId.Value = m_strOrderNumber;
			pvCollection.Add(pdvOrderId);
			
			Database crpDatabase;
			Tables crpTables;
			Table crpTable;
			TableLogOnInfo crpTableLogOnInfo;
			ConnectionInfo crpConnectionInfo = new ConnectionInfo();
			crpConnectionInfo.ServerName = Application.StartupPath + "\\DSMS.mdb";
			crpConnectionInfo.DatabaseName = "DSMS";
			crpConnectionInfo.UserID = "";
			crpConnectionInfo.Password = "";

			switch(m_vorcCaller)
			{
// ExpressOrder
				case ViewOrderReportCaller.ExpressOrder:
                    m_rptCurrentReport = m_rptNewOrder = new RPTViewOrder();
					crpDatabase = m_rptNewOrder.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}
					DatasetOrder dsOrder = new DatasetOrder();
					m_oddaDataset = new OleDbDataAdapter("SELECT Orders.OrderId, Orders.OrderDate, Employees.Title, "+
						"Employees.FirstName, Employees.LastName, Suppliers.CompanyName, Suppliers.BillingAdress, "+
						"Suppliers.City, Suppliers.StateOrProvince, Suppliers.PostalCode, Suppliers.Country, "+
						"Suppliers.ConTitle, Suppliers.ContactFirstName, Suppliers.ContactLastName, Suppliers.PhoneNumber, "+
						"Suppliers.FaxNumber, Suppliers.Email, Products.MatName, SubProducts.MatName, Trademarks.Trademark, "+
						"SubProducts.Pack, Orders.OrderQty, Orders.Prix, Suppliers.CustomId FROM Trademarks INNER JOIN (Suppliers INNER JOIN "+
						"(Employees INNER JOIN ((Orders INNER JOIN Products ON Orders.MatId = Products.MatId) INNER JOIN "+
						"SubProducts ON Orders.SubPrId = SubProducts.SubPrId) ON Employees.EmployeeId = Orders.EmployeeId) "+
						"ON Suppliers.FournisseurId = Orders.FournisseurId) ON Trademarks.MarComId = SubProducts.MarComId "+
						"WHERE ((Orders.OrderId)='"+m_strOrderNumber+"') ORDER BY Products.MatName,SubProducts.MatName", m_odcConnection);
					m_oddaDataset.Fill(dsOrder,"Order");
					m_oddaDataset = new OleDbDataAdapter("SELECT * FROM DentalOfficeInformation", m_odcConnection);
					m_oddaDataset.Fill(dsOrder,"DentalOfficeInformation");

					m_rptNewOrder.SetDataSource(dsOrder);
					crystalReportViewer1.ReportSource = m_rptNewOrder;
					
					crystalReportViewer1.Zoom(1);
				break;
// PreviousOrders				
				case ViewOrderReportCaller.PreviousOrder:
                    m_rptCurrentReport = m_rptPrevOrder = new RPTViewPrevOrder();
					crpDatabase = m_rptPrevOrder.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}

					dsOrder = new DatasetOrder();
					m_oddaDataset = new OleDbDataAdapter("SELECT Orders.OrderId, Orders.OrderDate, Employees.Title, "+
						"Employees.FirstName, Employees.LastName, Suppliers.CompanyName, Suppliers.BillingAdress, "+
						"Suppliers.City, Suppliers.StateOrProvince, Suppliers.PostalCode, Suppliers.Country, "+
						"Suppliers.ConTitle, Suppliers.ContactFirstName, Suppliers.ContactLastName, Suppliers.PhoneNumber, "+
						"Suppliers.FaxNumber, Suppliers.Email, Products.MatName, SubProducts.MatName, Trademarks.Trademark, "+
						"SubProducts.Pack, Orders.OrderQty, Orders.Prix, Suppliers.CustomId,Orders.CheckDate FROM Trademarks INNER JOIN (Suppliers INNER JOIN "+
						"(Employees INNER JOIN ((Orders INNER JOIN Products ON Orders.MatId = Products.MatId) INNER JOIN "+
						"SubProducts ON Orders.SubPrId = SubProducts.SubPrId) ON Employees.EmployeeId = Orders.EmployeeId) "+
						"ON Suppliers.FournisseurId = Orders.FournisseurId) ON Trademarks.MarComId = SubProducts.MarComId "+
						"WHERE ((Orders.OrderId)='"+m_strOrderNumber+"') ORDER BY Products.MatName,SubProducts.MatName", m_odcConnection);
					m_oddaDataset.Fill(dsOrder,"Order");
					m_oddaDataset = new OleDbDataAdapter("SELECT * FROM DentalOfficeInformation", m_odcConnection);
					m_oddaDataset.Fill(dsOrder,"DentalOfficeInformation");

					m_rptPrevOrder.SetDataSource(dsOrder);
					//this.btnSendEmail.Visible = false;
					crystalReportViewer1.ReportSource = m_rptPrevOrder;
					crystalReportViewer1.Zoom(1);
				break;
// Tender				
				case ViewOrderReportCaller.Tender:
                    m_rptCurrentReport = m_rptTender = new RPTViewTender();
					crpDatabase = m_rptTender.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}
					DatasetTender dsTender = new DatasetTender();
					m_oddaDataset = new OleDbDataAdapter("SELECT Employees.Title, Employees.FirstName, Employees.LastName, "+
						"Products.MatName, SubProducts.MatName, Trademarks.Trademark, SubProducts.Pack, tempTender.ordUnits, "+
						"Suppliers.CompanyName, Suppliers.BillingAdress, Suppliers.City, Suppliers.StateOrProvince, Suppliers.PostalCode, "+
						"Suppliers.Country, Suppliers.ConTitle, Suppliers.ContactFirstName, Suppliers.ContactLastName, Suppliers.PhoneNumber, "+
						"Suppliers.FaxNumber, Suppliers.Email, Suppliers.CustomId FROM Suppliers INNER JOIN (((Categories INNER JOIN Products "+
						"ON Categories.CategoryId = Products.CategoryId) INNER JOIN (Trademarks INNER JOIN SubProducts ON "+
						"Trademarks.MarComId = SubProducts.MarComId) ON Products.MatId = SubProducts.MatId) INNER JOIN "+
						"(Employees INNER JOIN tempTender ON Employees.EmployeeId = tempTender.EmployeeId) ON "+
						"SubProducts.SubPrId = tempTender.SubPrId) ON Suppliers.FournisseurId = tempTender.FournisseurId "+
						"ORDER BY Products.MatName, SubProducts.MatName", m_odcConnection);

					m_oddaDataset.Fill(dsTender,"Tender");
					m_oddaDataset = new OleDbDataAdapter("SELECT * FROM DentalOfficeInformation", m_odcConnection);
					m_oddaDataset.Fill(dsTender,"DentalOfficeInformation");

					m_rptTender.SetDataSource(dsTender);
					crystalReportViewer1.ReportSource = m_rptTender;
					crystalReportViewer1.Zoom(1);
				break;
// RegularOrder				
				case ViewOrderReportCaller.RegularOrder:
                    m_rptCurrentReport = m_rptNewOrder = new RPTViewOrder();
					crpDatabase = m_rptNewOrder.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}
					dsOrder = new DatasetOrder();
					m_oddaDataset = new OleDbDataAdapter("SELECT Orders.OrderId, Orders.OrderDate, Employees.Title, "+
						"Employees.FirstName, Employees.LastName, Suppliers.CompanyName, Suppliers.BillingAdress, "+
						"Suppliers.City, Suppliers.StateOrProvince, Suppliers.PostalCode, Suppliers.Country, "+
						"Suppliers.ConTitle, Suppliers.ContactFirstName, Suppliers.ContactLastName, Suppliers.PhoneNumber, "+
						"Suppliers.FaxNumber, Suppliers.Email, Products.MatName, SubProducts.MatName, Trademarks.Trademark, "+
						"SubProducts.Pack, Orders.OrderQty, Orders.Prix, Suppliers.CustomId FROM Trademarks INNER JOIN (Suppliers INNER JOIN "+
						"(Employees INNER JOIN ((Orders INNER JOIN Products ON Orders.MatId = Products.MatId) INNER JOIN "+
						"SubProducts ON Orders.SubPrId = SubProducts.SubPrId) ON Employees.EmployeeId = Orders.EmployeeId) "+
						"ON Suppliers.FournisseurId = Orders.FournisseurId) ON Trademarks.MarComId = SubProducts.MarComId "+
						"WHERE ((Orders.OrderId)='"+m_strOrderNumber+"') ORDER BY Products.MatName,SubProducts.MatName", m_odcConnection);
					m_oddaDataset.Fill(dsOrder,"Order");
					m_oddaDataset = new OleDbDataAdapter("SELECT * FROM DentalOfficeInformation", m_odcConnection);
					m_oddaDataset.Fill(dsOrder,"DentalOfficeInformation");

					m_rptNewOrder.SetDataSource(dsOrder);
					//crystalReportViewer1.ReportSource = m_rptNewOrder;
					//m_rptNewOrder.DataDefinition.ParameterFields["@newOrderId"].ApplyCurrentValues(pvCollection);
					crystalReportViewer1.ReportSource = m_rptNewOrder;
					crystalReportViewer1.Zoom(1);
				break;
//Cancelled Backorders				
				case ViewOrderReportCaller.CanceledBackorder:
                    m_rptCurrentReport = m_rptCancelation = new RPTViewCancelation();
					crpDatabase = m_rptCancelation.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}
					DatasetCanceledBO dsCanceledBO = new DatasetCanceledBO();
					m_oddaDataset = new OleDbDataAdapter("SELECT Orders.OrderId, Orders.OrderDate, Employees.Title, Employees.FirstName, Employees.LastName, "+
						"Suppliers.CompanyName, Suppliers.BillingAdress, Suppliers.City, Suppliers.StateOrProvince, Suppliers.PostalCode, Suppliers.Country, "+
						"Suppliers.ConTitle, Suppliers.ContactFirstName, Suppliers.ContactLastName, Suppliers.PhoneNumber, Suppliers.FaxNumber, Suppliers.Email, "+
						"Products.MatName, SubProducts.MatName, Trademarks.Trademark, SubProducts.Pack, Orders.OrderQty, Orders.Prix, Suppliers.CustomId, "+
						"Orders.CanceledBOUnits, Orders.CanceledBODate, Employees_1.Title AS CanBOTitle, Employees_1.FirstName AS CanBoFirstName, "+
						"Employees_1.LastName AS CanBOLastName, Orders.CheckDate FROM Trademarks INNER JOIN (Suppliers INNER JOIN (Employees INNER JOIN "+
						"(((Orders INNER JOIN Products ON Orders.MatId = Products.MatId) INNER JOIN Employees AS Employees_1 ON "+
						"Orders.CanceledBOEmployeeId = Employees_1.EmployeeId) INNER JOIN SubProducts ON Orders.SubPrId = SubProducts.SubPrId) "+
						"ON Employees.EmployeeId = Orders.EmployeeId) ON Suppliers.FournisseurId = Orders.FournisseurId) ON "+
						"Trademarks.MarComId = SubProducts.MarComId WHERE (((Orders.OrderId)='"+m_strOrderNumber+"')) "+
						"ORDER BY Products.MatName, SubProducts.MatName",m_odcConnection);

					m_oddaDataset.Fill(dsCanceledBO,"CanceledBO");
					m_oddaDataset = new OleDbDataAdapter("SELECT * FROM DentalOfficeInformation", m_odcConnection);
					m_oddaDataset.Fill(dsCanceledBO,"DentalOfficeInformation");

					m_rptCancelation.SetDataSource(dsCanceledBO);
					crystalReportViewer1.ReportSource = m_rptCancelation;
					crystalReportViewer1.Zoom(1);
				break;
// ReturnedOrders
				case ViewOrderReportCaller.ReturnedOrders:
                    m_rptCurrentReport = m_rptReturnedOrders = new RPTViewReturnedOrders();
					crpDatabase = m_rptReturnedOrders.Database;
					crpTables = crpDatabase.Tables;
					for(int i = 0; i < crpTables.Count; i++)
					{
						crpTable = crpTables[i];
						crpTableLogOnInfo = crpTable.LogOnInfo;
						crpTableLogOnInfo.ConnectionInfo = crpConnectionInfo;
						crpTable.ApplyLogOnInfo(crpTableLogOnInfo);
					}

					DatasetReturn dsReturn = new DatasetReturn();
					m_oddaDataset = new OleDbDataAdapter("SELECT Orders.OrderId, Orders.OrderDate, Employees.Title, Employees.FirstName, Employees.LastName, "+
						"Suppliers.CompanyName, Suppliers.BillingAdress, Suppliers.City, Suppliers.StateOrProvince, Suppliers.PostalCode, Suppliers.Country, "+
						"Suppliers.ConTitle, Suppliers.ContactFirstName, Suppliers.ContactLastName, Suppliers.PhoneNumber, Suppliers.FaxNumber, Suppliers.Email, "+
						"Products.MatName, SubProducts.MatName, Trademarks.Trademark, SubProducts.Pack, Orders.OrderQty, Orders.Prix, Suppliers.CustomId, "+
						"Orders.ReturnUnits, Orders.ReturnDate, Employees_1.Title AS ReturnTitle, Employees_1.FirstName AS ReturnFirstName, "+
						"Employees_1.LastName AS ReturnLastName, Orders.CheckDate, Orders.ReturnNumber FROM Trademarks INNER JOIN (Suppliers INNER JOIN (Employees INNER JOIN "+
						"((SubProducts INNER JOIN (Orders INNER JOIN Products ON Orders.MatId = Products.MatId) ON SubProducts.SubPrId = Orders.SubPrId) "+
						"INNER JOIN Employees AS Employees_1 ON Orders.ReturnEmployeeId = Employees_1.EmployeeId) ON Employees.EmployeeId = Orders.EmployeeId) "+
						"ON Suppliers.FournisseurId = Orders.FournisseurId) ON Trademarks.MarComId = SubProducts.MarComId WHERE (((Orders.OrderId)='"+m_strOrderNumber+"')) "+
						"ORDER BY Products.MatName, SubProducts.MatName", m_odcConnection);
					m_oddaDataset.Fill(dsReturn,"Return");
					m_oddaDataset = new OleDbDataAdapter("SELECT * FROM DentalOfficeInformation", m_odcConnection);
					m_oddaDataset.Fill(dsReturn,"DentalOfficeInformation");

					m_rptReturnedOrders.SetDataSource(dsReturn);
					//m_rptReturnedOrders.DataDefinition.ParameterFields["@newOrderId"].ApplyCurrentValues(pvCollection);
					crystalReportViewer1.ReportSource = m_rptReturnedOrders;
					crystalReportViewer1.Zoom(1);
				break;
			}
		}

		public void SetOrderInformation(string strOrderNumber, SupplierInformation siSupplier)
		{
			m_strOrderNumber = strOrderNumber;
            m_siSupplier = siSupplier;
		}

        private void btnPrint_Click(object sender, System.EventArgs e)
        {
            crystalReportViewer1.PrintReport();

            m_blnOrderSent = m_blnTenderSent = m_blnCancelationSent = true;
        }

 		public void SetOrderSentbyEmailStatus(bool blnSentEmail)
		{
            if (blnSentEmail)
                m_blnOrderSent = m_blnTenderSent = m_blnCancelationSent = true;
            else
                m_blnOrderSent = m_blnTenderSent = m_blnCancelationSent = false;
        }

		private void fclsOIViewOrdRpt_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			switch(m_vorcCaller)
			{
				case ViewOrderReportCaller.ExpressOrder:
					((fclsOMEmergencyOrder) m_frmCaller).SetOrderSentStatus(m_blnOrderSent);
				break;

				case ViewOrderReportCaller.RegularOrder:
					((fclsOMComparePrices) m_frmCaller).SetOrderSentStatus(m_blnOrderSent);
				break;

				case ViewOrderReportCaller.CanceledBackorder:
					//((fclsGENBackOrders) m_frmCaller).SetCancelationSentStatus(m_blnCancelationSent);
				break;

				case ViewOrderReportCaller.Tender:
					((fclsOMStandByOrder) m_frmCaller).SetTenderSentStatus(m_blnTenderSent);
				break;
			}
		}

	}
}
