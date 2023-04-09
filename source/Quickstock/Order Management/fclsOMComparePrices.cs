using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Windows.Forms;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsOMComparePrices.
	/// </summary>
	public class fclsOMComparePrices : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.Button btnReset;
		private System.Windows.Forms.Button btnSaveAndClose;
		private DSMS.ComparePricesLineContainer cplcPriceComparison;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		public delegate void			MakeOrderHandler(ComparePricesLineContainer.Supplier sSupplier);
		
		private bool					m_blnOrderSent;
		private DataTable				m_dtaPriceComparisonSuppliers;
		private fclsOMStandByOrder		m_frmOMStandByOrder;
		private OleDbConnection			m_odcConnection;
		private OleDbDataAdapter		m_odaPriceComparisonSuppliers;

		public fclsOMComparePrices(fclsOMStandByOrder frmOMStandByOrder, int intEmployeeID, OleDbConnection odcConnection)
		{
            DataRow[] dtrSuppliersFound;
			DataTable dtaMiscellaneous;
			OleDbDataAdapter odaMiscellaneous;
            String strSavedSupplierID;

			// Required for Windows Form Designer support
			InitializeComponent();
			
			// initialize global variables
			m_frmOMStandByOrder = frmOMStandByOrder;
			m_odcConnection = odcConnection;
            this.DialogResult = DialogResult.Cancel;

			try
			{
				// load suppliers from database and add them to the ComparePricesLineContainer
                odaMiscellaneous = new OleDbDataAdapter("SELECT * FROM Suppliers WHERE Status = 1 ORDER BY CompanyName", m_odcConnection);
				dtaMiscellaneous = new DataTable();
				odaMiscellaneous.Fill(dtaMiscellaneous);
				this.cplcPriceComparison.LoadSuppliers(dtaMiscellaneous);

				// load the default price comparison suppliers
				m_odaPriceComparisonSuppliers = new OleDbDataAdapter("SELECT * FROM tempPriceComparisonSuppliers",m_odcConnection);
				m_dtaPriceComparisonSuppliers = new DataTable();
				m_odaPriceComparisonSuppliers.Fill(m_dtaPriceComparisonSuppliers);

                strSavedSupplierID = m_dtaPriceComparisonSuppliers.Rows[0]["SupplierId"].ToString();
                dtrSuppliersFound = dtaMiscellaneous.Select("FournisseurId = " + strSavedSupplierID);
                if(dtrSuppliersFound.Length > 0)
                    this.cplcPriceComparison.SelectedSupplier1 = int.Parse(strSavedSupplierID);

                strSavedSupplierID = m_dtaPriceComparisonSuppliers.Rows[1]["SupplierId"].ToString();
                dtrSuppliersFound = dtaMiscellaneous.Select("FournisseurId = " + strSavedSupplierID);
                if (dtrSuppliersFound.Length > 0)
                    this.cplcPriceComparison.SelectedSupplier2 = int.Parse(strSavedSupplierID);

                strSavedSupplierID = m_dtaPriceComparisonSuppliers.Rows[2]["SupplierId"].ToString();
                dtrSuppliersFound = dtaMiscellaneous.Select("FournisseurId = " + strSavedSupplierID);
                if (dtrSuppliersFound.Length > 0)
                    this.cplcPriceComparison.SelectedSupplier3 = int.Parse(strSavedSupplierID);

                strSavedSupplierID = m_dtaPriceComparisonSuppliers.Rows[3]["SupplierId"].ToString();
                dtrSuppliersFound = dtaMiscellaneous.Select("FournisseurId = " + strSavedSupplierID);
                if (dtrSuppliersFound.Length > 0)
                    this.cplcPriceComparison.SelectedSupplier4 = int.Parse(strSavedSupplierID);

                /*this.cplcPriceComparison.SelectedSupplier2 = int.Parse(m_dtaPriceComparisonSuppliers.Rows[1]["SupplierId"].ToString());
				this.cplcPriceComparison.SelectedSupplier3 = int.Parse(m_dtaPriceComparisonSuppliers.Rows[2]["SupplierId"].ToString());
				this.cplcPriceComparison.SelectedSupplier4 = int.Parse(m_dtaPriceComparisonSuppliers.Rows[3]["SupplierId"].ToString());*/

				// load employees from database and add them to the ComparePricesLineContainer
				odaMiscellaneous = new OleDbDataAdapter("SELECT * FROM Employees WHERE Status = 1 ORDER BY FirstName, LastName",m_odcConnection);
				dtaMiscellaneous = new DataTable();
				odaMiscellaneous.Fill(dtaMiscellaneous);
				this.cplcPriceComparison.LoadEmployees(dtaMiscellaneous);
				this.cplcPriceComparison.SelectedEmployeeID = intEmployeeID;
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, this.Text, MessageBoxButtons.OK,MessageBoxIcon.Error);
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
			this.cplcPriceComparison = new DSMS.ComparePricesLineContainer();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnReset = new System.Windows.Forms.Button();
			this.btnSaveAndClose = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// cplcPriceComparison
			// 
			this.cplcPriceComparison.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.cplcPriceComparison.ChangesMade = false;
			this.cplcPriceComparison.Comments = "";
			this.cplcPriceComparison.Location = new System.Drawing.Point(8, 8);
			this.cplcPriceComparison.Name = "cplcPriceComparison";
			this.cplcPriceComparison.OrdersInProgress = false;
			this.cplcPriceComparison.SelectedEmployeeID = -1;
			this.cplcPriceComparison.SelectedSupplier1 = -1;
			this.cplcPriceComparison.SelectedSupplier2 = -1;
			this.cplcPriceComparison.SelectedSupplier3 = -1;
			this.cplcPriceComparison.SelectedSupplier4 = -1;
			this.cplcPriceComparison.Size = new System.Drawing.Size(1009, 672);
			this.cplcPriceComparison.Supplier1_BackColor = System.Drawing.Color.Red;
			this.cplcPriceComparison.Supplier1_ForeColor = System.Drawing.Color.White;
			this.cplcPriceComparison.Supplier2_BackColor = System.Drawing.Color.Blue;
			this.cplcPriceComparison.Supplier2_ForeColor = System.Drawing.Color.White;
			this.cplcPriceComparison.Supplier3_BackColor = System.Drawing.Color.Maroon;
			this.cplcPriceComparison.Supplier3_ForeColor = System.Drawing.Color.White;
			this.cplcPriceComparison.Supplier4_BackColor = System.Drawing.Color.Green;
			this.cplcPriceComparison.Supplier4_ForeColor = System.Drawing.Color.White;
			this.cplcPriceComparison.TabIndex = 0;
			this.cplcPriceComparison.OnMakeOrder += new DSMS.fclsOMComparePrices.MakeOrderHandler(this.cplcPriceComparison_OnMakeOrder);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.Location = new System.Drawing.Point(920, 688);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(96, 32);
			this.btnHelp.TabIndex = 45;
			this.btnHelp.Text = "Help";
			// 
			// btnReset
			// 
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReset.Location = new System.Drawing.Point(584, 688);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(96, 32);
			this.btnReset.TabIndex = 44;
			this.btnReset.Text = "Reset";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnSaveAndClose
			// 
			this.btnSaveAndClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSaveAndClose.Location = new System.Drawing.Point(696, 688);
			this.btnSaveAndClose.Name = "btnSaveAndClose";
			this.btnSaveAndClose.Size = new System.Drawing.Size(96, 32);
			this.btnSaveAndClose.TabIndex = 43;
			this.btnSaveAndClose.Text = "Save and Close";
			this.btnSaveAndClose.Click += new System.EventHandler(this.btnSaveAndClose_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(808, 688);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(96, 32);
			this.btnClose.TabIndex = 42;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// fclsOMComparePrices
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(1024, 726);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnReset);
			this.Controls.Add(this.btnSaveAndClose);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.cplcPriceComparison);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "fclsOMComparePrices";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quick Stock - Compare Product Prices";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.fclsOMComparePrices_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			this.cplcPriceComparison.ClearData();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void fclsOMComparePrices_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			DialogResult dlgResult;

            // always store the selected the selected price comparison suppliers in the database
            m_dtaPriceComparisonSuppliers.Rows[0]["SupplierId"] = this.cplcPriceComparison.SelectedSupplier1;
            m_dtaPriceComparisonSuppliers.Rows[1]["SupplierId"] = this.cplcPriceComparison.SelectedSupplier2;
            m_dtaPriceComparisonSuppliers.Rows[2]["SupplierId"] = this.cplcPriceComparison.SelectedSupplier3;
            m_dtaPriceComparisonSuppliers.Rows[3]["SupplierId"] = this.cplcPriceComparison.SelectedSupplier4;
            OleDbCommandBuilder odcbSQLCommandBuilder = new OleDbCommandBuilder(m_odaPriceComparisonSuppliers);

            try
            {
                m_odaPriceComparisonSuppliers.Update(m_dtaPriceComparisonSuppliers);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
            }		
            			
			if(this.cplcPriceComparison.OrdersInProgress)
			{
				dlgResult = MessageBox.Show("Exiting now will result in all information about un-ordered products being lost.\nWould you like to continue?",this.Text,MessageBoxButtons.OKCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);

				if(dlgResult == DialogResult.Cancel)
					e.Cancel =	true;
			}
			else
			{
				if(this.cplcPriceComparison.ChangesMade)
				{
					dlgResult = MessageBox.Show("Would you like to save all unsaved changes?",this.Text,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);

					switch(dlgResult)
					{
						case DialogResult.Cancel:
							e.Cancel = true;
						break;

						case DialogResult.Yes:
                            m_frmOMStandByOrder.SetPriceComparisonData(this.cplcPriceComparison.ComparePricesLines, this.cplcPriceComparison.Comments);
						break;
					}
				}
			}
		}

		public void LoadPriceComparisonData(ArrayList alOrder)
		{
			ComparePricesLine cplNewComparePricesLine;
			OrderLine olOrderLine;

			foreach(Object objOrderLine in alOrder)
			{
				olOrderLine = (OrderLine) objOrderLine;

				// create new compare prices line
				cplNewComparePricesLine = new ComparePricesLine(olOrderLine.Product, olOrderLine.Packaging, olOrderLine.TradeMark, this.cplcPriceComparison);
				cplNewComparePricesLine.LineNumber = olOrderLine.LineNumber;
				cplNewComparePricesLine.CategoryId = olOrderLine.CategoryId;
				cplNewComparePricesLine.Packaging = olOrderLine.Packaging;
				cplNewComparePricesLine.ProductId = olOrderLine.ProductId;
				cplNewComparePricesLine.SubProductId = olOrderLine.SubProductId;
				cplNewComparePricesLine.TrademarkId = olOrderLine.TradeMarkId;
				cplNewComparePricesLine.Units = olOrderLine.Units;
				cplNewComparePricesLine.UnitPrice1 = olOrderLine.UnitPrice1;
				cplNewComparePricesLine.UnitPrice2 = olOrderLine.UnitPrice2;
				cplNewComparePricesLine.UnitPrice3 = olOrderLine.UnitPrice3;
				cplNewComparePricesLine.UnitPrice4 = olOrderLine.UnitPrice4;
					
				this.cplcPriceComparison.Add(cplNewComparePricesLine);
				this.cplcPriceComparison.Comments = olOrderLine.Comments;
			}

			this.cplcPriceComparison.ChangesMade = false;
		}

		private void btnSaveAndClose_Click(object sender, System.EventArgs e)
		{
            m_frmOMStandByOrder.SetPriceComparisonData(this.cplcPriceComparison.ComparePricesLines, this.cplcPriceComparison.Comments);
			this.cplcPriceComparison.ChangesMade = false;
            this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void cplcPriceComparison_OnMakeOrder(DSMS.ComparePricesLineContainer.Supplier sSupplier)
		{
			bool blnAllProductsOrdered;
			ComparePricesLine cplCurrentLine;
			DataRow dtrNewOrderLine;
			DataTable dtaOrders;
			OleDbCommand odcCommand;
			OleDbCommandBuilder ocbOrders;
			OleDbDataAdapter odaOrders;
			OleDbTransaction odtTransaction;
			string strNewOrderNumber;
			SupplierInformation CurrentSupplier;

			// initialize varibles
			blnAllProductsOrdered = true;
			m_blnOrderSent = false;
			dtaOrders = new DataTable();
			strNewOrderNumber = GetNewOrderNumber();
			
			try
			{
				// get supplier information
				CurrentSupplier = this.cplcPriceComparison.GetSupplierInformation(sSupplier);

				// add order data to database
				odaOrders = new OleDbDataAdapter("SELECT * FROM [Orders]", m_odcConnection);
				ocbOrders = new OleDbCommandBuilder(odaOrders);
				odaOrders.Fill(dtaOrders);
				
				foreach(Object objComparePricesLine in this.cplcPriceComparison.ComparePricesLines)
				{
					cplCurrentLine = (ComparePricesLine) objComparePricesLine;

					if(cplCurrentLine.SelectedSupplier == sSupplier)
					{
						dtrNewOrderLine = dtaOrders.NewRow();
						dtrNewOrderLine["OrderId"]			= strNewOrderNumber;
						dtrNewOrderLine["OrderDate"]		= DateTime.Now.ToShortDateString();
						dtrNewOrderLine["MatId"]			= cplCurrentLine.ProductId;
						dtrNewOrderLine["SubPrId"]			= cplCurrentLine.SubProductId;
						dtrNewOrderLine["MarComId"]			= cplCurrentLine.TrademarkId;
						dtrNewOrderLine["FournisseurId"]	= CurrentSupplier.DatabaseID;
						dtrNewOrderLine["EmployeeId"]		= this.cplcPriceComparison.SelectedEmployeeID;
						dtrNewOrderLine["OrderQty"]			= cplCurrentLine.Units;
						dtrNewOrderLine["Pack"]				= cplCurrentLine.Packaging;
						dtrNewOrderLine["CategoryId"]		= cplCurrentLine.CategoryId;
						dtrNewOrderLine["Prix"]				= cplCurrentLine.GetSelectedPrice();
						dtrNewOrderLine["Checked"]			= 0;
						dtrNewOrderLine["ReceivedQty"]		= 0;
						dtrNewOrderLine["BackOrderUnits"]	= 0;
						dtrNewOrderLine["CanceledBOUnits"]	= 0;
						dtrNewOrderLine["ReturnUnits"]		= 0;
						dtrNewOrderLine["CatalogPay"]		= 0;
						dtrNewOrderLine["Tax"]				= 0;
						dtrNewOrderLine["Transport"]		= 0;
						dtrNewOrderLine["Duty"]				= 0;
						dtrNewOrderLine["TotalPay"]			= 0;
			
						//Add the new row to the table
						dtaOrders.Rows.Add(dtrNewOrderLine);

						// disable product line so that user cannot modify it in the future
						cplCurrentLine.Enabled = false;
					}
				}

				// save changes to database
				odaOrders.Update(dtaOrders);
				dtaOrders.AcceptChanges();
				
				// display order
				fclsOIViewOrdRpt frmOIViewOrdRpt = new fclsOIViewOrdRpt(this, fclsOIViewOrdRpt.ViewOrderReportCaller.RegularOrder, m_odcConnection);
				frmOIViewOrdRpt.SetOrderInformation(strNewOrderNumber, CurrentSupplier);
				frmOIViewOrdRpt.ShowDialog();
				
				// if the order was not sent successfully, delete it from the database and reenable all products
				if(m_blnOrderSent)
				{
					this.cplcPriceComparison.SetSupplierEnabled(sSupplier,false);						// disable supplier from which the current order was sent
					this.cplcPriceComparison.OrdersInProgress = true;									// mark that orders are being made
					m_frmOMStandByOrder.SetOrdersSentStatus(true);										// mark that at least an order has been sent, so that RegularOrder window can be closed
					
					// check if all products have been orderd
					foreach(Object objComparePricesLine in this.cplcPriceComparison.ComparePricesLines)
					{
						if(((ComparePricesLine) objComparePricesLine).Enabled)
						{
							blnAllProductsOrdered = false;
							break;
						}
					}
					
					// if all all products have been ordered, mark these fields as false so that form can close
					// without asking any questions and close the form
					if(blnAllProductsOrdered)
					{
						this.cplcPriceComparison.ChangesMade = false;
						this.cplcPriceComparison.OrdersInProgress = false;
						this.Close();
					}
				}
				else
				{
					// re-enable all products
					foreach(Object objComparePricesLine in this.cplcPriceComparison.ComparePricesLines)
					{
						cplCurrentLine = (ComparePricesLine) objComparePricesLine;
						
						if(cplCurrentLine.SelectedSupplier == sSupplier)
							cplCurrentLine.Enabled = true;
					}
					
					// delete order from database
					odcCommand = m_odcConnection.CreateCommand();
					odtTransaction = m_odcConnection.BeginTransaction();
					odcCommand.Connection = m_odcConnection;
					odcCommand.Transaction = odtTransaction;
					try
					{
						odcCommand.CommandText = "DELETE FROM [Orders] WHERE [OrderId]='" + strNewOrderNumber + "'";
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
			catch(OleDbException ode)
			{
				dtaOrders.RejectChanges();
				MessageBox.Show(ode.Message + "\r\n" + ode.InnerException + "\r\n" + ode.StackTrace, this.Text, MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\r\n" + ex.InnerException + "\r\n" + ex.StackTrace, this.Text, MessageBoxButtons.OK,MessageBoxIcon.Error);
			}
		}

		private string GetNewOrderNumber()
		{
			DataTable dtaOrders;
			OleDbDataAdapter odaOrders;
			string strCurrentYear, strNewOrderNumber;

			strCurrentYear = System.DateTime.Now.ToString("yy");
			odaOrders = new OleDbDataAdapter("SELECT [OrderId] FROM [Orders] WHERE [OrderId] LIKE '%" + strCurrentYear + "' ORDER BY [OrderId]", m_odcConnection);
			dtaOrders = new DataTable();				
			odaOrders.Fill(dtaOrders);

			if(dtaOrders.Rows.Count > 0)
				strNewOrderNumber = (int.Parse(dtaOrders.Rows[dtaOrders.Rows.Count - 1]["OrderId"].ToString().Substring(0,3)) + 1).ToString("000") + "-" + strCurrentYear;
			else
				strNewOrderNumber = "000-" + strCurrentYear;
			
			return strNewOrderNumber;
		}
		
		public void SetOrderSentStatus(bool blnOrderSent)
		{
			m_blnOrderSent = blnOrderSent;
		}
	}
}
