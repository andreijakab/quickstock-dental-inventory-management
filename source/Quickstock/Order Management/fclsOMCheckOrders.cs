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
	/// Summary description for Form1.
	/// </summary>
	public class fclsOMCheckOrders : System.Windows.Forms.Form
	{
		private System.Windows.Forms.GroupBox gpbSearchBy;
		private System.Windows.Forms.ListBox lbxOrderNumber;
		private System.Windows.Forms.ComboBox cmbOrderedBy;
		private System.Windows.Forms.RadioButton optOrderedBy;
		private System.Windows.Forms.RadioButton optOrderNumber;
		private System.Windows.Forms.RadioButton optSupplier;
		private System.Windows.Forms.Button btnUpdateOrder;
		private System.Windows.Forms.Button btnDeleteOrder;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ComboBox cmbCheckedBy;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.ComboBox cmbSupplier;
		private System.Windows.Forms.Button btnAllOrderReceived;
		private System.Windows.Forms.Label lblCheckedBy;
		private DSMS.ReceivedOrderLineContainer olcContainer;	
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		private enum SearchOrders:int {All, BySupplier, ByEmployee};
		
		private ArrayList											m_alReturnedProducts;
		private bool												m_blnDontQueryDiscardChanges, m_blnIsReturned, m_blnReadOnly;
		private DataTable											m_dtaEmployees, m_dtaOrders, m_dtaSuppliers;
		private DateTime											m_dtPaymentDate;
		private decimal 											m_decAmountPaid, m_decPenalty;
		private Utilities.SupplierInformation						m_siSupplier;
		private int													m_intPayerEmployeeId;
		private OleDbConnection										m_odcConnection;
		private SearchOrders										m_soCurrentFilter;
		private string												m_strCurrentOrderNumber, m_strPaymentMethod, m_strQueriedOrderNumber;
		private string												m_strReturnNumber;
		
		public fclsOMCheckOrders(string strQueriedOrderNumber, OleDbConnection odcConnection)
		{
            int intUserID;
			OleDbDataAdapter odaMiscellaneous;
			ToolTip ttpToolTip;

			// Initializa form components
			InitializeComponent();

			// Global variable initialization
			m_blnIsReturned = false;
			m_blnDontQueryDiscardChanges = false;
			m_dtaEmployees = new DataTable();
			m_dtaSuppliers = new DataTable();
			m_odcConnection = odcConnection;
			m_strCurrentOrderNumber = "";
			m_strQueriedOrderNumber = strQueriedOrderNumber;
			m_strReturnNumber = "";

			// Declare & Initialize Variables
			ttpToolTip = new ToolTip();

			// load Suppliers from database
			odaMiscellaneous = new OleDbDataAdapter("SELECT * FROM Suppliers ORDER BY CompanyName",m_odcConnection);
			odaMiscellaneous.Fill(m_dtaSuppliers);

			// Load Employees from Database and add them to cmbCheckedBy
			odaMiscellaneous = new OleDbDataAdapter("SELECT EmployeeId, Title, FirstName, LastName FROM Employees WHERE Status = 1 ORDER BY LastName, FirstName",m_odcConnection);
			odaMiscellaneous.Fill(m_dtaEmployees);
			foreach(DataRow dtrEmployee in m_dtaEmployees.Rows)
				this.cmbCheckedBy.Items.Add(clsUtilities.FormatName_List(dtrEmployee["Title"].ToString(), dtrEmployee["FirstName"].ToString(), dtrEmployee["LastName"].ToString()));
            intUserID = clsConfiguration.Internal_CurrentUserID;
            this.cmbCheckedBy.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intUserID, m_dtaEmployees, 0);

			// Set up the ToolTips for btnClose and btnUpdateOrder
			ttpToolTip.AutoPopDelay = 5000;
			ttpToolTip.InitialDelay = 1000;
			ttpToolTip.ReshowDelay = 500;
			ttpToolTip.ShowAlways = true;
			ttpToolTip.SetToolTip(this.btnClose, "Click here to Close this Dialogbox");
			ttpToolTip.SetToolTip(this.btnUpdateOrder, "Click here to Update the Database for this order");

            // set form to 'read only' if querying for specific order #
            if (m_strQueriedOrderNumber != null && m_strQueriedOrderNumber.Length > 0)
                this.SetReadOnly();

            // Populate order number listbox
			this.GetOrderNumbers(SearchOrders.All);
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
			this.gpbSearchBy = new System.Windows.Forms.GroupBox();
			this.lbxOrderNumber = new System.Windows.Forms.ListBox();
			this.cmbOrderedBy = new System.Windows.Forms.ComboBox();
			this.optOrderedBy = new System.Windows.Forms.RadioButton();
			this.optOrderNumber = new System.Windows.Forms.RadioButton();
			this.cmbSupplier = new System.Windows.Forms.ComboBox();
			this.optSupplier = new System.Windows.Forms.RadioButton();
			this.btnUpdateOrder = new System.Windows.Forms.Button();
			this.lblCheckedBy = new System.Windows.Forms.Label();
			this.cmbCheckedBy = new System.Windows.Forms.ComboBox();
			this.btnDeleteOrder = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.olcContainer = new DSMS.ReceivedOrderLineContainer();
			this.btnAllOrderReceived = new System.Windows.Forms.Button();
			this.gpbSearchBy.SuspendLayout();
			this.SuspendLayout();
			// 
			// gpbSearchBy
			// 
			this.gpbSearchBy.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.gpbSearchBy.Controls.Add(this.lbxOrderNumber);
			this.gpbSearchBy.Controls.Add(this.cmbOrderedBy);
			this.gpbSearchBy.Controls.Add(this.optOrderedBy);
			this.gpbSearchBy.Controls.Add(this.optOrderNumber);
			this.gpbSearchBy.Controls.Add(this.cmbSupplier);
			this.gpbSearchBy.Controls.Add(this.optSupplier);
			this.gpbSearchBy.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.gpbSearchBy.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.gpbSearchBy.Location = new System.Drawing.Point(8, 0);
			this.gpbSearchBy.Name = "gpbSearchBy";
			this.gpbSearchBy.Size = new System.Drawing.Size(680, 112);
			this.gpbSearchBy.TabIndex = 18;
			this.gpbSearchBy.TabStop = false;
			this.gpbSearchBy.Text = "Search by";
			// 
			// lbxOrderNumber
			// 
			this.lbxOrderNumber.ItemHeight = 16;
			this.lbxOrderNumber.Location = new System.Drawing.Point(136, 16);
			this.lbxOrderNumber.Name = "lbxOrderNumber";
			this.lbxOrderNumber.Size = new System.Drawing.Size(160, 84);
			this.lbxOrderNumber.TabIndex = 20;
			this.lbxOrderNumber.SelectedIndexChanged += new System.EventHandler(this.lbxOrderNumber_SelectedIndexChanged);
			// 
			// cmbOrderedBy
			// 
			this.cmbOrderedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.cmbOrderedBy.Enabled = false;
			this.cmbOrderedBy.Location = new System.Drawing.Point(472, 76);
			this.cmbOrderedBy.Name = "cmbOrderedBy";
			this.cmbOrderedBy.Size = new System.Drawing.Size(200, 24);
			this.cmbOrderedBy.TabIndex = 13;
			this.cmbOrderedBy.SelectedIndexChanged += new System.EventHandler(this.cmbOrderedBy_SelectedIndexChanged);
			// 
			// optOrderedBy
			// 
			this.optOrderedBy.Location = new System.Drawing.Point(352, 80);
			this.optOrderedBy.Name = "optOrderedBy";
			this.optOrderedBy.Size = new System.Drawing.Size(120, 16);
			this.optOrderedBy.TabIndex = 12;
			this.optOrderedBy.Text = "Ordered by";
			this.optOrderedBy.Click += new System.EventHandler(this.optOrderedBy_Click);
			// 
			// optOrderNumber
			// 
			this.optOrderNumber.Checked = true;
			this.optOrderNumber.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.optOrderNumber.Location = new System.Drawing.Point(16, 50);
			this.optOrderNumber.Name = "optOrderNumber";
			this.optOrderNumber.Size = new System.Drawing.Size(120, 16);
			this.optOrderNumber.TabIndex = 8;
			this.optOrderNumber.TabStop = true;
			this.optOrderNumber.Text = "Order Number";
			this.optOrderNumber.Click += new System.EventHandler(this.optOrderNumber_Click);
			// 
			// cmbSupplier
			// 
			this.cmbSupplier.DropDownStyle = System.Windows.Forms.ComboBoxStyle.Simple;
			this.cmbSupplier.Enabled = false;
			this.cmbSupplier.Location = new System.Drawing.Point(472, 16);
			this.cmbSupplier.Name = "cmbSupplier";
			this.cmbSupplier.Size = new System.Drawing.Size(200, 24);
			this.cmbSupplier.TabIndex = 7;
			this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSuppliers_SelectedIndexChanged);
			// 
			// optSupplier
			// 
			this.optSupplier.Location = new System.Drawing.Point(352, 20);
			this.optSupplier.Name = "optSupplier";
			this.optSupplier.Size = new System.Drawing.Size(104, 16);
			this.optSupplier.TabIndex = 0;
			this.optSupplier.Text = "Supplier";
			this.optSupplier.Click += new System.EventHandler(this.optSupplier_Click);
			// 
			// btnUpdateOrder
			// 
			this.btnUpdateOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnUpdateOrder.Location = new System.Drawing.Point(144, 600);
			this.btnUpdateOrder.Name = "btnUpdateOrder";
			this.btnUpdateOrder.Size = new System.Drawing.Size(112, 32);
			this.btnUpdateOrder.TabIndex = 32;
			this.btnUpdateOrder.Text = "Update Order";
			this.btnUpdateOrder.Click += new System.EventHandler(this.btnUpdateOrder_Click);
			// 
			// lblCheckedBy
			// 
			this.lblCheckedBy.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.lblCheckedBy.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblCheckedBy.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCheckedBy.ForeColor = System.Drawing.SystemColors.ActiveCaption;
			this.lblCheckedBy.Location = new System.Drawing.Point(696, 32);
			this.lblCheckedBy.Name = "lblCheckedBy";
			this.lblCheckedBy.Size = new System.Drawing.Size(200, 23);
			this.lblCheckedBy.TabIndex = 33;
			this.lblCheckedBy.Text = "Checked by";
			this.lblCheckedBy.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cmbCheckedBy
			// 
			this.cmbCheckedBy.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.cmbCheckedBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCheckedBy.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmbCheckedBy.ForeColor = System.Drawing.Color.Red;
			this.cmbCheckedBy.Location = new System.Drawing.Point(696, 72);
			this.cmbCheckedBy.Name = "cmbCheckedBy";
			this.cmbCheckedBy.Size = new System.Drawing.Size(200, 24);
			this.cmbCheckedBy.TabIndex = 34;
			// 
			// btnDeleteOrder
			// 
			this.btnDeleteOrder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnDeleteOrder.Location = new System.Drawing.Point(280, 600);
			this.btnDeleteOrder.Name = "btnDeleteOrder";
			this.btnDeleteOrder.Size = new System.Drawing.Size(112, 32);
			this.btnDeleteOrder.TabIndex = 35;
			this.btnDeleteOrder.Text = "Delete Order";
			this.btnDeleteOrder.Click += new System.EventHandler(this.btnDeleteOrder_Click);
			// 
			// btnClose
			// 
			this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnClose.Location = new System.Drawing.Point(664, 600);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(112, 32);
			this.btnClose.TabIndex = 36;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnHelp.Location = new System.Drawing.Point(800, 600);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(112, 32);
			this.btnHelp.TabIndex = 38;
			this.btnHelp.Text = "Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// olcContainer
			// 
			this.olcContainer.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.olcContainer.ChangesMade = false;
			this.olcContainer.Duty = new System.Decimal(new int[] {
																	  0,
																	  0,
																	  0,
																	  0});
			this.olcContainer.Location = new System.Drawing.Point(8, 120);
			this.olcContainer.Name = "olcContainer";
			this.olcContainer.ShippingHandling = new System.Decimal(new int[] {
																				  0,
																				  0,
																				  0,
																				  0});
			this.olcContainer.Size = new System.Drawing.Size(904, 472);
			this.olcContainer.TabIndex = 0;
			this.olcContainer.Taxes = new System.Decimal(new int[] {
																	   0,
																	   0,
																	   0,
																	   0});
			// 
			// btnAllOrderReceived
			// 
			this.btnAllOrderReceived.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.btnAllOrderReceived.Location = new System.Drawing.Point(8, 600);
			this.btnAllOrderReceived.Name = "btnAllOrderReceived";
			this.btnAllOrderReceived.Size = new System.Drawing.Size(112, 32);
			this.btnAllOrderReceived.TabIndex = 39;
			this.btnAllOrderReceived.Text = "All Order Received";
			this.btnAllOrderReceived.Click += new System.EventHandler(this.btnAllOrderReceived_Click);
			// 
			// fclsOMCheckOrders
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(918, 638);
			this.Controls.Add(this.btnAllOrderReceived);
			this.Controls.Add(this.olcContainer);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnDeleteOrder);
			this.Controls.Add(this.cmbCheckedBy);
			this.Controls.Add(this.lblCheckedBy);
			this.Controls.Add(this.btnUpdateOrder);
			this.Controls.Add(this.gpbSearchBy);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.Name = "fclsOMCheckOrders";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quick Stock - Order Check-In";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.fclsOMCheckOrders_Closing);
			this.gpbSearchBy.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void btnDeleteOrder_Click(object sender, System.EventArgs e)
		{
			// Variable declaration
			OleDbCommand odcCommand;
			OleDbTransaction odtTransaction;
			
			if(this.lbxOrderNumber.SelectedIndex != -1)
			{
				if(MessageBox.Show("This operation cannnot be undone.\nAre you sure you want to delete order "+ this.lbxOrderNumber.SelectedItem.ToString() +"?",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
				{
					odcCommand = m_odcConnection.CreateCommand();
					odtTransaction = m_odcConnection.BeginTransaction();
					odcCommand.Connection = m_odcConnection;
					odcCommand.Transaction = odtTransaction;

					try
					{
						odcCommand.CommandText = "DELETE FROM Orders WHERE OrderId='" + m_strCurrentOrderNumber + "'";
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
					
					this.olcContainer.ClearAll();
					this.GetOrderNumbers(m_soCurrentFilter);
				}
			}
			else
				MessageBox.Show("You must first select an order!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Information);
		}

		private void btnUpdateOrder_Click(object sender, System.EventArgs e)
		{
			DialogResult dlgResult;
			DataRow [] dtrFilteredRows;
			DataRow dtrCurrentRow;
			DataTable dtaOrder, dtaSubProducts;
			decimal [] decTaxesPerProduct, decShippingHandlingPerProduct, decDutyPerProduct, decSubTotalPerProduct, decTotalPerProduct;
			decimal decSubTotalProducts, decTotal;
			decimal decProportion, decCurrentPrice, decSavedPrice;
			int intNOrderLines;
			fclsOIAccounting_Pay frmOIAccounting;
			fclsOMReturnProdCanceledBO frmOMReturnProducts;
			OleDbCommandBuilder ocbOrder, ocbSubProducts;
			OleDbDataAdapter odaOrder, odaSubProducts;
			ReceivedOrderLine [] rolOrderLines;
						
			// initialize variables
																   
			rolOrderLines = this.olcContainer.OrderLines;
			intNOrderLines = rolOrderLines.Length;
			decSubTotalPerProduct = new decimal[intNOrderLines];
			decDutyPerProduct = new decimal[intNOrderLines];
			decShippingHandlingPerProduct = new decimal[intNOrderLines];
			decTaxesPerProduct = new decimal[intNOrderLines];
			decTotalPerProduct = new decimal[intNOrderLines];
			decSubTotalProducts = decTotal = 0.0m;
			
			if(this.cmbCheckedBy.SelectedIndex != -1)
			{
				dlgResult = MessageBox.Show("Please ensure all that all the order information is correct (including duty, shipping and handling and taxes).\nAre you sure you would like to proceed?",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button2);
				if(dlgResult == DialogResult.No)
					return;

				//
				// inquire about return product number if some products are to be returned
				//
				// create array of all products to be returned
				m_alReturnedProducts = new ArrayList();
				fclsOMReturnProdCanceledBO.ReturnedProduct rpCurrentProduct;
				foreach(ReceivedOrderLine rolCurrentLine in this.olcContainer.OrderLines)
				{
					if(rolCurrentLine.UnitsToReturn > 0)
					{
						rpCurrentProduct.Product = rolCurrentLine.Product;
						rpCurrentProduct.Trademark = rolCurrentLine.Trademark;
						rpCurrentProduct.UnitsReturned = rolCurrentLine.UnitsToReturn;
						
						m_alReturnedProducts.Add(rpCurrentProduct);
					}
				}
				
				// prepare returned products form, if needed
				if(m_alReturnedProducts.Count > 0)
				{
					frmOMReturnProducts = new fclsOMReturnProdCanceledBO(this,
																		 fclsOMReturnProdCanceledBO.Caller.CheckOrders,
																		 m_strCurrentOrderNumber,
																		 m_siSupplier,
																		 m_alReturnedProducts,
																		 m_odcConnection);
					frmOMReturnProducts.SetReturnInformation(DateTime.Now, null, (int) this.m_dtaEmployees.Rows[this.cmbCheckedBy.SelectedIndex]["EmployeeId"]);
					frmOMReturnProducts.ShowDialog();
				}

				//
				// compute amount owed and distribute the extra costs among the products
				//
				// Calculate the sub-total per product and the order sub-total
				for(int i=0; i<intNOrderLines; i++)
				{
					decSubTotalPerProduct[i] = rolOrderLines[i].UnitPrice * (rolOrderLines[i].UnitsReceived - rolOrderLines[i].UnitsToReturn);
					decSubTotalProducts += decSubTotalPerProduct[i];
				}
			
				// calculate the extra costs per product (i.e. duty, taxes and s&h)
				if(decSubTotalProducts > 0)
				{
					for(int i=0; i<intNOrderLines; i++)
					{
						decProportion = decSubTotalPerProduct[i]/decSubTotalProducts;
						decDutyPerProduct[i] = decProportion * this.olcContainer.Duty;
						decShippingHandlingPerProduct[i] = decProportion * this.olcContainer.ShippingHandling;
						decTaxesPerProduct[i] = decProportion * this.olcContainer.Taxes;
						decTotalPerProduct[i] = decSubTotalPerProduct[i] + decDutyPerProduct[i] + decShippingHandlingPerProduct[i] + decTaxesPerProduct[i];
						decTotal += decTotalPerProduct[i];
					}
				}
			
				//
				// Save data to database
				//
				// Update Subproduct Table
				try
				{
					// load SubProducts table from database
					odaSubProducts = new OleDbDataAdapter("SELECT * FROM SubProducts ORDER BY SubPrId",m_odcConnection);
					ocbSubProducts = new OleDbCommandBuilder(odaSubProducts);
					dtaSubProducts = new DataTable();
					odaSubProducts.Fill(dtaSubProducts);

					// load Orders table from database
					odaOrder = new OleDbDataAdapter("SELECT [Orders.Key], [Orders.Checked],  [Orders.CheckedBy],  [Orders.CheckDate],  [Orders.Prix],  [Orders.CatalogPay],  [Orders.Duty],  [Orders.Transport],  [Orders.Tax],  [Orders.TotalPay],  [Orders.ReceivedQty],  [Orders.CanceledBOUnits],  [Orders.BackOrderUnits],  [Orders.BackOrderUpdateDate],  [Orders.BackOrderEmployeeId],  [Orders.ReturnUnits],  [Orders.ReturnDate],  [Orders.ReturnEmployeeId],  [Orders.ReturnNumber] " +
													"FROM Orders " +
													"WHERE OrderId ='" + m_strCurrentOrderNumber + "'" + 
													"ORDER BY SubPrId",m_odcConnection);
					ocbOrder = new OleDbCommandBuilder(odaOrder);
					dtaOrder = new DataTable();
					odaOrder.Fill(dtaOrder);
				
					// process each subproduct individually
					for(int i=0; i<intNOrderLines; i++)
					{
						//
						// update SubProducts table
						//
						dtrFilteredRows = dtaSubProducts.Select("[SubPrId]=" + rolOrderLines[i].SubProductId.ToString());
					
						if(dtrFilteredRows.Length == 1)
						{
							dtrFilteredRows[0].BeginEdit();
							decSavedPrice = decimal.Parse(dtrFilteredRows[0]["Prix"].ToString());
							decCurrentPrice = rolOrderLines[i].UnitPrice;
						
							if(decCurrentPrice == 0)
							{
								dtrFilteredRows[0]["PrixMin"] = dtrFilteredRows[0]["PrixMax"] = decCurrentPrice;
								dtrFilteredRows[0]["PrixMinOI"] = dtrFilteredRows[0]["PrixMaxOI"] = m_strCurrentOrderNumber;
							}
							else
							{
								if(decimal.Parse(dtrFilteredRows[0]["PrixMin"].ToString()) >= decCurrentPrice)
								{
									dtrFilteredRows[0]["PrixMin"] = decCurrentPrice;
									dtrFilteredRows[0]["PrixMinOI"] = m_strCurrentOrderNumber;
								}
								if(decimal.Parse(dtrFilteredRows[0]["PrixMax"].ToString()) <= decCurrentPrice)
								{
									dtrFilteredRows[0]["PrixMax"] = decCurrentPrice;
									dtrFilteredRows[0]["PrixMaxOI"] = m_strCurrentOrderNumber;
								}
							}

							dtrFilteredRows[0]["Prix"] = decCurrentPrice;
							dtrFilteredRows[0]["PrixOrderId"] = m_strCurrentOrderNumber;
							dtrFilteredRows[0]["Qtty"] = rolOrderLines[i].UnitsReceived + decimal.Parse(dtrFilteredRows[0]["Qtty"].ToString());
							dtrFilteredRows[0]["CatalogPay"] = decSubTotalPerProduct[i] + decimal.Parse(dtrFilteredRows[0]["CatalogPay"].ToString());
							dtrFilteredRows[0]["Duty"] = decDutyPerProduct[i]+ decimal.Parse(dtrFilteredRows[0]["Duty"].ToString());
							dtrFilteredRows[0]["Tax"] = decTaxesPerProduct[i]+ decimal.Parse(dtrFilteredRows[0]["Tax"].ToString());
							dtrFilteredRows[0]["Transport"] = decShippingHandlingPerProduct[i]+ decimal.Parse(dtrFilteredRows[0]["Transport"].ToString());
							dtrFilteredRows[0]["TotalPay"] = decTotalPerProduct[i] + decimal.Parse(dtrFilteredRows[0]["TotalPay"].ToString());
							dtrFilteredRows[0].EndEdit();
						
						}
						else
							throw new Exception("There was a problem updating the subproducts table!");

						//
						// update Orders table
						//
						dtrCurrentRow = dtaOrder.Rows[i];
						dtrCurrentRow.BeginEdit();
						dtrCurrentRow["Orders.Checked"]					= 1;
						dtrCurrentRow["Orders.CheckedBy"]				= int.Parse(m_dtaEmployees.Rows[this.cmbCheckedBy.SelectedIndex]["EmployeeId"].ToString());
						dtrCurrentRow["Orders.CheckDate"]				= DateTime.Now.ToShortDateString();
					
						decSavedPrice = decimal.Parse(dtrCurrentRow["Orders.Prix"].ToString());
						if(decSavedPrice != decCurrentPrice)
							dtrCurrentRow["Orders.Prix"]				= decCurrentPrice;

						dtrCurrentRow["Orders.CatalogPay"]				= decSubTotalPerProduct[i];
						dtrCurrentRow["Orders.Duty"]					= decDutyPerProduct[i];
						dtrCurrentRow["Orders.Transport"]				= decShippingHandlingPerProduct[i];
						dtrCurrentRow["Orders.Tax"]						= decTaxesPerProduct[i];
						dtrCurrentRow["Orders.TotalPay"]				= decTotalPerProduct[i];
						dtrCurrentRow["Orders.ReceivedQty"]				= rolOrderLines[i].UnitsReceived;
						dtrCurrentRow["Orders.CanceledBOUnits"]			= 0;
											
						if(rolOrderLines[i].Backorder > 0)
						{
							dtrCurrentRow["Orders.BackOrderUnits"]		= rolOrderLines[i].Backorder;
							dtrCurrentRow["Orders.BackOrderUpdateDate"]	= DateTime.Now.ToShortDateString();
							dtrCurrentRow["Orders.BackOrderEmployeeId"]	= int.Parse(m_dtaEmployees.Rows[this.cmbCheckedBy.SelectedIndex]["EmployeeId"].ToString());
						}

						if(rolOrderLines[i].UnitsToReturn > 0)
						{
							dtrCurrentRow["Orders.ReturnUnits"] 		= rolOrderLines[i].UnitsToReturn;
							dtrCurrentRow["Orders.ReturnDate"]			= DateTime.Now.ToShortDateString();
							dtrCurrentRow["Orders.ReturnEmployeeId"]	= int.Parse(m_dtaEmployees.Rows[this.cmbCheckedBy.SelectedIndex]["EmployeeId"].ToString());
							
							if(m_blnIsReturned)
								dtrCurrentRow["Orders.ReturnNumber"] 		= m_strReturnNumber;
							else
								dtrCurrentRow["Orders.ReturnNumber"] 		= "0";
						}

						dtrCurrentRow.EndEdit();
					}
				
					// save SubProduct table changes to database
					odaSubProducts.Update(dtaSubProducts);
					dtaSubProducts.AcceptChanges();

					// save Order table changes to database
					odaOrder.Update(dtaOrder);
					dtaOrder.AcceptChanges();
					
					// if total to be paid > 0, open accounting form
					if(Decimal.Compare(decTotal, 0.0M) > 0)
					{
						frmOIAccounting = new fclsOIAccounting_Pay(fclsOIAccounting_Pay.Caller.OrderCheckIn,
																   this,
																   m_strCurrentOrderNumber,
																   int.Parse(this.m_dtaEmployees.Rows[this.cmbCheckedBy.SelectedIndex]["EmployeeId"].ToString()),
																   decTotal,
																   m_odcConnection);
						frmOIAccounting.ShowDialog();
						this.SavePaymentInformation(decSubTotalProducts, decTotal);
					}
					
					// update list of order numbers
					this.GetOrderNumbers(m_soCurrentFilter);
				}
				catch(Exception ex)
				{
					MessageBox.Show(ex.Message + "\n" + ex.InnerException + "\n" + ex.StackTrace, this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
				}
			}
			else
				MessageBox.Show("You must first select an employee from the 'Checked by' drop-down list.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
			
		}

		private void fclsOMCheckOrders_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			if(this.olcContainer.ChangesMade)
			{
				if(!DiscardUnsavedChanges())
					e.Cancel = true;
			}
		}

		private void optOrderNumber_Click(object sender, System.EventArgs e)
		{
			if(this.olcContainer.ChangesMade)
			{
				switch(m_soCurrentFilter)
				{	
					case SearchOrders.All:
						return;

					case SearchOrders.ByEmployee:
						if(!this.DiscardUnsavedChanges())
						{
							this.optOrderedBy.Checked = true;
							return;
						}
					break;

					case SearchOrders.BySupplier:
						if(!this.DiscardUnsavedChanges())
						{
							this.optSupplier.Checked = true;
							return;
						}
					break;
					

				}
			}

			// Clear field of past data
			this.ClearFields(true, SearchOrders.All);

			// Clear and disable Employee combo box
			this.cmbOrderedBy.Items.Clear();
			this.cmbOrderedBy.Enabled = false;
			this.cmbOrderedBy.DropDownStyle = ComboBoxStyle.Simple;

			// Clear and disable Supplier combo box
			this.cmbSupplier.Items.Clear();
			this.cmbSupplier.Enabled = false;
			this.cmbSupplier.DropDownStyle = ComboBoxStyle.Simple;

			this.GetOrderNumbers(SearchOrders.All);
		}

		private void optSupplier_Click(object sender, System.EventArgs e)
		{
			if(this.olcContainer.ChangesMade)
			{
				switch(m_soCurrentFilter)
				{
					case SearchOrders.All:
						if(!this.DiscardUnsavedChanges())
						{
							this.optOrderNumber.Checked = true;
							return;
						}
					break;

					case SearchOrders.ByEmployee:
						if(!this.DiscardUnsavedChanges())
						{
							this.optOrderedBy.Checked = true;
							return;
						}
					break;

					case SearchOrders.BySupplier:
						return;
				}
			}

			// Clear fields of past data
			this.ClearFields(true, SearchOrders.BySupplier);
			this.cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;

			// Add items to combo box and enable it
			for(int i=0; i<m_dtaSuppliers.Rows.Count; i++)
				this.cmbSupplier.Items.Add(m_dtaSuppliers.Rows[i]["CompanyName"].ToString());
			this.cmbSupplier.Enabled = true;

			// Clear and disable Employee combo box
			this.cmbOrderedBy.Items.Clear();
			this.cmbOrderedBy.Enabled = false;
			this.cmbOrderedBy.DropDownStyle = ComboBoxStyle.Simple;
		}

		private void optOrderedBy_Click(object sender, System.EventArgs e)
		{
			DataRow dtrRow;

			if(this.olcContainer.ChangesMade)
			{
				switch(m_soCurrentFilter)
				{
					case SearchOrders.All:
						if(!this.DiscardUnsavedChanges())
						{
							this.optOrderNumber.Checked = true;
							return;
						}
					break;

					case SearchOrders.ByEmployee:
						return;

					case SearchOrders.BySupplier:
						if(!this.DiscardUnsavedChanges())
						{
							this.optSupplier.Checked = true;
							return;
						}
					break;
				}
			}

			// Clear fields of past data
			this.ClearFields(true, SearchOrders.ByEmployee);
			this.cmbOrderedBy.DropDownStyle = ComboBoxStyle.DropDownList;

			// Clear, add items to and enable combobox
			for(int i=0; i<m_dtaEmployees.Rows.Count; i++)
			{
				dtrRow = m_dtaEmployees.Rows[i];
				this.cmbOrderedBy.Items.Add(clsUtilities.FormatName_List(dtrRow["Title"].ToString(), dtrRow["FirstName"].ToString(), dtrRow["LastName"].ToString()));
			}
			this.cmbOrderedBy.Enabled = true;
		
			// Clear and disable Supplier combo box
			this.cmbSupplier.Items.Clear();
			this.cmbSupplier.Enabled = false;
			this.cmbSupplier.DropDownStyle = ComboBoxStyle.Simple;
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void cmbSuppliers_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.cmbSupplier.SelectedIndex != -1)
				this.GetOrderNumbers(SearchOrders.BySupplier);
		}

		private void cmbOrderedBy_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.cmbOrderedBy.SelectedIndex != -1)
				this.GetOrderNumbers(SearchOrders.ByEmployee);
		}

		private void lbxOrderNumber_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.olcContainer.ChangesMade)
			{
				if(!m_blnDontQueryDiscardChanges)
				{
					if(!this.DiscardUnsavedChanges())
					{
						m_blnDontQueryDiscardChanges = true;
						this.lbxOrderNumber.SelectedIndex = clsUtilities.FindItemIndex(m_strCurrentOrderNumber,this.lbxOrderNumber);
						return;
					}
				}
				else
				{
					m_blnDontQueryDiscardChanges = false;
					return;
				}
			}
			
			this.ShowSelectedOrder();
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","DeliveriesCheckIn.htm");  //
		}

		private void btnAllOrderReceived_Click(object sender, System.EventArgs e)
		{
			/*bool blnPriceUpdated = true;
			DialogResult dlgResult;
			string strMessage;

			foreach(ReceivedOrderLine rolOrderLine in this.olcContainer.OrderLines)
			{
				if(rolOrderLine.UnitPrice == 0.0M)
				{
					blnPriceUpdated = false;
					break;
				}
			}
			
			if(blnPriceUpdated)
				strMessage = "Are all of the unit prices correct?";
			else
				strMessage = "Some of the unit prices have a value of 0. Are you sure you would like to continue?";
			
			dlgResult = MessageBox.Show(this, strMessage, this.Text, MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
			if(dlgResult == DialogResult.Yes)
			{*/
			foreach(ReceivedOrderLine rolOrderLine in this.olcContainer.OrderLines)
			{
				//rolOrderLine.Checked = true;
				rolOrderLine.UnitsReceived = rolOrderLine.UnitsOrdered;
			}
				
			//this.btnOrderComplete.Enabled = false;
			//this.btnUpdateOrder.Enabled = true;
			//this.btnUpdateOrder.Select();
			//}
		}

		//--------------------------------------------------------------------------------------------------------------------
		// Methods
		//--------------------------------------------------------------------------------------------------------------------
		/// <summary>
		///		Method called by the ReceivedOrderLineContainer once all the order lines
		///		have been checked.
		/// </summary>
		/*public void AllOrderLinesChecked(bool blnAllChecked)
		{
			if(blnAllChecked)
			{
				this.btnOrderComplete.Enabled = false;
				this.btnUpdateOrder.Enabled = true;
			}
			else
			{
				this.btnOrderComplete.Enabled = true;
				this.btnUpdateOrder.Enabled = false;
			}
		}*/

		/// <summary>
		///		Clears the data fields in the form. If blnOptionButton is TRUE, it means that
		///		the user has just clicked a given radio button and the associated combo 
		///		box text needs to be cleared as well.
		/// </summary>
		private void ClearFields(bool blnOptionButton, SearchOrders soSearchBy)
		{
			switch(soSearchBy)
			{
				case SearchOrders.All:
					this.cmbOrderedBy.Text = "";
					this.cmbSupplier.Text = "";
				break;

				case SearchOrders.ByEmployee:
					this.cmbSupplier.Text = "";
					if(blnOptionButton)
						this.cmbOrderedBy.Text = "";
				break;

				case SearchOrders.BySupplier:
					this.cmbOrderedBy.Text = "";
					if(blnOptionButton)
						this.cmbSupplier.Text = "";
				break;
			}

			// Listbox & ReceivedOrderlineContainer
			this.lbxOrderNumber.Items.Clear();
			this.olcContainer.ClearAll();
			
			// Disable order control buttons
			this.btnAllOrderReceived.Enabled = false;
			this.btnUpdateOrder.Enabled = false;
			this.btnDeleteOrder.Enabled = false;
		}
		
		/// <summary>
		///		Asks the user whether to discard the changes made to the current order.
		/// </summary>
		/// <returns>
		///		Returns TRUE if the changes should be discarded, FALSE otherwise.
		/// </returns>
		private bool DiscardUnsavedChanges()
		{
			bool blnResult = false;
			DialogResult dlgResult;

			dlgResult = MessageBox.Show("The current order has not been saved.\nAre you sure you want to discard all changes and proceed?",this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button2);
			if(dlgResult == DialogResult.Yes)
				blnResult = true;
			
			return blnResult;
		}

		/// <summary>
		///		Finds the list index of the order whose order number is strQueriedOrderNumber.
		/// </summary>
		/// <returns>
		///		Returns the order's list index if the order number was found. If not, the function returns -1.
		/// </returns>
		private int FindOrderIndex(string strQueriedOrderNumber)
		{
			string strCurrentOrderNumber = "";
			int intOrderIndex = -1;

			for(int i=0; i< m_dtaOrders.Rows.Count; i++)
			{
				strCurrentOrderNumber = m_dtaOrders.Rows[i]["OrderId"].ToString();
				
				if(clsUtilities.CompareStrings(strCurrentOrderNumber, strQueriedOrderNumber))
				{
					intOrderIndex = i;
					break;
				}
			}

			return intOrderIndex;
		}

		/// <summary>
		///		Finds the employee associated with a certain 'EmployeeId'
		/// </summary>
		/// <returns>
		///		Returns the emplyee's title, first name and last name.
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
					strEmployee = clsUtilities.FormatName_Display(dtrRow["Title"].ToString(), dtrRow["FirstName"].ToString(), dtrRow["LastName"].ToString());
					break;
				}
			}

			return strEmployee;
		}

		/// <summary>
		///		Populates lbxOrderNumber with Order Numbers depending on soSearchBy.
		/// </summary>
		private void GetOrderNumbers(SearchOrders soSearchBy)
		{
			// Variable declaration
			OleDbDataAdapter odaMiscellaneous;
			string strQuery;

			// Declares & Initializes Variables
			m_dtaOrders = new DataTable();
			strQuery = "";

			// Clear listbox
			this.lbxOrderNumber.Items.Clear();
			this.olcContainer.ClearAll();

			// save current filter for future reference
			m_soCurrentFilter = soSearchBy;
			
			// Select Query to be executed depening on intCriteria
			switch(soSearchBy)
			{
				// Search by Supplier
				case SearchOrders.BySupplier:
					strQuery = "SELECT DISTINCT OrderId, OrderDate, FournisseurId, EmployeeId FROM Orders WHERE FournisseurId=" + m_dtaSuppliers.Rows[this.cmbSupplier.SelectedIndex]["FournisseurId"].ToString() + " AND";
					this.cmbOrderedBy.Text = "";
				break;

				// Search by Employee
				case SearchOrders.ByEmployee:
					strQuery = "SELECT DISTINCT OrderId, OrderDate, FournisseurId, EmployeeId FROM Orders WHERE EmployeeId=" + m_dtaEmployees.Rows[this.cmbOrderedBy.SelectedIndex]["EmployeeId"].ToString() + " AND";
					this.cmbSupplier.Text = "";
				break;

				// Get everything
				case SearchOrders.All:
					strQuery = "SELECT DISTINCT OrderId, OrderDate, FournisseurId, EmployeeId FROM Orders WHERE";
				break;
			}
			// Complete the Query String 
			strQuery += " Checked = 0 ORDER BY OrderDate";

			// Gets the Orders from the Database and stores them in m_dtaOrders
			try
			{
				odaMiscellaneous = new OleDbDataAdapter(strQuery,m_odcConnection);
				odaMiscellaneous.Fill(m_dtaOrders);
				
				if(m_dtaOrders.Rows.Count > 0)
				{
					// Adds the Order Numbers in m_dtaOrders[]["OrderId"] to lbxOrderNumber
					for(int i=0; i < m_dtaOrders.Rows.Count; i++)
						this.lbxOrderNumber.Items.Add(m_dtaOrders.Rows[i]["OrderId"].ToString());
			
					// Selects either the order that was sent to the constructor ot the last order in the listbox
					if(m_strQueriedOrderNumber != null && m_strQueriedOrderNumber.Length > 0)
						this.lbxOrderNumber.SelectedIndex = FindOrderIndex(m_strQueriedOrderNumber);
					else
						this.lbxOrderNumber.SelectedIndex = this.lbxOrderNumber.Items.Count - 1;
				}
				else
					MessageBox.Show("No orders matching the specified criteria were found.", this.Text);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.InnerException + "\n" + ex.StackTrace, this.Text);
			}
		}
		
		/// <summary>
		///		Finds the supplier associated with a certain 'FournisseurId'
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
		
		/// <summary>
		///		Function that saves the new payment information in the database.
		/// </summary>
		private void SavePaymentInformation(decimal decSubTotalDue, decimal decTotalAmountDue)
		{
			// variable declaration
			DataRow dtrNewPaymentInformation;
			DataTable dtaPaymentInformation;
			decimal decAmountDue;
			OleDbCommandBuilder	ocbPaymentInformation;
			OleDbDataAdapter odaPaymentInformation;

			// variable initialization
			dtaPaymentInformation = new DataTable();
			odaPaymentInformation = new OleDbDataAdapter("SELECT * FROM OrderPayment ORDER BY OrderId", m_odcConnection);
			ocbPaymentInformation = new OleDbCommandBuilder(odaPaymentInformation);
			odaPaymentInformation.Fill(dtaPaymentInformation);

			// compute the amount that is left to pay for the order; if less than 1 dollar, round to 0
			decAmountDue = decTotalAmountDue - m_decAmountPaid;
			if(Math.Abs(decAmountDue) <= 1)
				decAmountDue = 0;

			// create a new row with the new payment information
			dtrNewPaymentInformation = dtaPaymentInformation.NewRow();
			dtrNewPaymentInformation["OrderId"] = m_strCurrentOrderNumber;
			dtrNewPaymentInformation["PaymentDate"] = m_dtPaymentDate.ToShortDateString();
			dtrNewPaymentInformation["SubTotal"] = decSubTotalDue;
			dtrNewPaymentInformation["Duty"] = this.olcContainer.Duty.ToString();
			dtrNewPaymentInformation["Tax"] = this.olcContainer.Taxes.ToString();
			dtrNewPaymentInformation["Transport"] = this.olcContainer.ShippingHandling.ToString();
			dtrNewPaymentInformation["TotalPay"] = m_decAmountPaid;
			dtrNewPaymentInformation["Penalty"] = m_decPenalty;
			dtrNewPaymentInformation["PayedPer"] = m_strPaymentMethod;
			dtrNewPaymentInformation["PayedBy"] = m_intPayerEmployeeId;
			dtrNewPaymentInformation["SumDue"] = decAmountDue;
			
			if(decAmountDue == 0)
				dtrNewPaymentInformation["checkPayment"] = "1";
			else
				dtrNewPaymentInformation["checkPayment"] = "0";
			
			// add the new row to the table
			dtaPaymentInformation.Rows.Add(dtrNewPaymentInformation);
			odaPaymentInformation.Update(dtaPaymentInformation);
			dtaPaymentInformation.AcceptChanges();
		}

		/// <summary>
		///		Function called by fclsOI_Accounting_Pay in order to return payment information for the current order.
		/// </summary>
		public void SetPaymentInformation(DateTime dtPaymentDate, decimal decAmoundPaid, decimal decPenalty, string strPaymentMethod, int intPayerEmployeeId)
		{
			m_dtPaymentDate = dtPaymentDate;
			m_decAmountPaid = decAmoundPaid;
			m_decPenalty = decPenalty;
			m_strPaymentMethod = strPaymentMethod;
			m_intPayerEmployeeId = intPayerEmployeeId;
		}

        /// <summary>
        ///		Function that disable certain GUI elements in order to make the form 'read only'.
        /// </summary>
        private void SetReadOnly()
        {
            m_blnReadOnly = true;

            this.Text += " (Viewing Only)";

            this.gpbSearchBy.Enabled = false;
            this.cmbCheckedBy.Enabled = false;

            this.olcContainer.ReadOnly = true;

            this.btnAllOrderReceived.Enabled = false;
            this.btnDeleteOrder.Enabled = false;
            this.btnUpdateOrder.Enabled = false;
        }

		/// <summary>
		///		Function called by fclsOMCheckOrders_ReturnProd in order to return information on the 'return of products to supplier' process.
		/// </summary>
		public void SetReturnedProductInformation(string strReturnNumber)
		{
			m_blnIsReturned = true;
			m_strReturnNumber = strReturnNumber;
		}

		/// <summary>
		///		Display the products that are part of the currently selected order in lbxOrderNumber in the
		///		ReceivedOrderLineContainer olcContainer.
		/// </summary>
		private void ShowSelectedOrder()
		{
			// Variable declaration
			DataRow dtrRow;
			DataTable dtaOrder;
			int i = 0;
			OleDbCommandBuilder ocbOrder;
			OleDbDataAdapter odaOrder;
			ReceivedOrderLine rolNewOrderLine;
			
			// Clear products from ReceivedOrderLineContainer and disable contol buttons
			this.olcContainer.ClearAll();
			this.btnAllOrderReceived.Enabled = false;
			this.btnUpdateOrder.Enabled = false;
			this.btnDeleteOrder.Enabled = false;

			if((this.lbxOrderNumber.Items.Count > 0) && (this.lbxOrderNumber.SelectedIndex != -1))
			{
				m_strCurrentOrderNumber = this.lbxOrderNumber.SelectedItem.ToString();
				
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
					break;
				}

				// Display the order date
				this.olcContainer.OrderDate = ((DateTime) dtrRow["OrderDate"]);

				// Display the products from the selected Order
				// Adds the ordered Items to olcContainer
				dtaOrder = new DataTable();
				odaOrder = new OleDbDataAdapter("SELECT ALL [Products.MatName], [SubProducts.MatName], [Trademarks.Trademark], [Orders.Pack], [Orders.OrderQty], [Orders.Prix], [Orders.BackOrderUnits], [Orders.SubPrId] " +
												"FROM Products INNER JOIN ((Trademarks INNER JOIN Orders ON Trademarks.MarComId = Orders.MarComId) INNER JOIN SubProducts ON (SubProducts.SubPrId = Orders.SubPrId) AND (Trademarks.MarComId = SubProducts.MarComId)) ON (Orders.MatId = Products.MatId) AND (Products.MatId = SubProducts.MatId) " +
												"WHERE (((Orders.OrderId)='" + m_strCurrentOrderNumber + "')) " +
												"ORDER BY Orders.SubPrId",m_odcConnection);
				ocbOrder = new OleDbCommandBuilder(odaOrder);
				odaOrder.Fill(dtaOrder);
				
				if(dtaOrder.Rows.Count > 0)
				{
					foreach(DataRow dtrOrderLine in dtaOrder.Rows)
					{
						rolNewOrderLine = new ReceivedOrderLine(this.olcContainer);
						rolNewOrderLine.LineNumber = i + 1;
						rolNewOrderLine.Product = dtrOrderLine["Products.MatName"].ToString() + " - " + dtrOrderLine["SubProducts.MatName"].ToString();
						rolNewOrderLine.Trademark = dtrOrderLine["Trademarks.Trademark"].ToString();
						rolNewOrderLine.Packaging = dtrOrderLine["Orders.Pack"].ToString();
						rolNewOrderLine.UnitsOrdered = int.Parse(dtrOrderLine["Orders.OrderQty"].ToString());
						rolNewOrderLine.UnitPrice = decimal.Parse(dtrOrderLine["Orders.Prix"].ToString());
						rolNewOrderLine.SubProductId = int.Parse(dtrOrderLine["Orders.SubPrId"].ToString());
						
						//string m_strMatId = strFindProductId(dtrOrderLine["Products.MatName"].ToString());
						//m_rolOrderLines[i].ProductId = int.Parse(m_strMatId);
						//m_rolOrderLines[i].CategoryId = fclsGENInput.categId;
						//string m_strSubPrId = strFindSubPrId(dtrOrderLine["SubProducts.MatName"].ToString(),m_strMatId);
						//m_rolOrderLines[i].SubProductId = int.Parse(m_strSubPrId);
						
						//int m_intTrademarkId = intFindTrademarkId(dtrOrderLine["Trademarks.Trademark"].ToString());
						//m_rolOrderLines[i].TrademarkId = m_intTrademarkId;
						this.olcContainer.Add(rolNewOrderLine);
						i++;
					}
					
					// enable order control buttons (if not in 'read only' mode)
                    if(!m_blnReadOnly)
                    {
                        this.btnAllOrderReceived.Enabled = true;
                        this.btnUpdateOrder.Enabled = true;
                        this.btnDeleteOrder.Enabled = true;
                    }

					this.olcContainer.ChangesMade = false;
				}
				
				// TODO: Figure out what screwes up because of this..Probably accounting or other forms
				//		 opened from this one -> Most likely ReceivedOrderLineContainer
				/* 
				orderId = this.lbxOrderNumber.SelectedItem.ToString();
				string strSupplierID = m_dtaOrders.Rows[intSelectedOrder]["FournisseurId"].ToString();
				string strEmployeeID = m_dtaOrders.Rows[intSelectedOrder]["EmployeeId"].ToString();
				fclsGENInput.orderId = orderId;
				fclsGENInput.orderDate = m_dtaOrders.Rows[intSelectedOrder]["OrderDate"].ToString();
				fclsGENInput.emplId = int.Parse(strEmployeeID);
				fclsGENInput.supplId = int.Parse(strSupplierID);*/
			}
		}
		
		//#####################################################################################################################
		// NOT USED SO FAR
		//#####################################################################################################################
		public string strFindProductId(string strName)
		{
			OleDbDataAdapter m_odaProducts = new OleDbDataAdapter("Select * FROM [Products] ORDER BY MatId", m_odcConnection);
			DataTable m_dtProducts = new DataTable("Products");
			try
			{
				m_odaProducts.Fill(m_dtProducts);
			}
			catch(OleDbException ex)
			{
				MessageBox.Show (ex.Message, this.Text);
			}
			int nrRecs = m_dtProducts.Rows.Count;
			string strComp = strName.Trim();
			for (int i=0; i<nrRecs; i++)
			{
				string strPrName = m_dtProducts.Rows[i]["MatName"].ToString();
				if (strComp == strPrName)
				{
					fclsGENInput.categId = int.Parse(m_dtProducts.Rows[i]["CategoryId"].ToString());
					return m_dtProducts.Rows[i]["MatId"].ToString();
				}
			}
			return "-1";
		}

		public string strFindSubPrId(string strName, string PrId)
		{
			OleDbDataAdapter m_odaSubProducts = new OleDbDataAdapter("Select * FROM [SubProducts] WHERE MatId=" + PrId + " ORDER BY SubPrId", m_odcConnection);
			DataTable m_dtSubProducts = new DataTable("SubProducts");
			try
			{
				m_odaSubProducts.Fill(m_dtSubProducts);
			}
			catch(OleDbException ex)
			{
				MessageBox.Show (ex.Message, this.Text);
			}
			int nrRecs = m_dtSubProducts.Rows.Count;
			string strComp = strName.Trim();
			for (int i=0; i<nrRecs; i++)
			{
				string strSubPrName = m_dtSubProducts.Rows[i]["MatName"].ToString();
				if (strComp == strSubPrName)
					return m_dtSubProducts.Rows[i]["SubPrId"].ToString();
			}
			return "-1";
		}

		public int intFindTrademarkId(string strName)
		{
			OleDbDataAdapter m_odaTrademarks = new OleDbDataAdapter("Select * FROM [Trademarks] ORDER BY MarComId", m_odcConnection);
			DataTable m_dtTrademarks = new DataTable("Trademarks");
			try
			{
				m_odaTrademarks.Fill(m_dtTrademarks);
			}
			catch(OleDbException ex)
			{
				MessageBox.Show (ex.Message, this.Text);
			}
			int nrRecs = m_dtTrademarks.Rows.Count;
			string strComp = strName.Trim();
			for (int i=0; i<nrRecs; i++)
			{
				string strPrName = m_dtTrademarks.Rows[i]["Trademark"].ToString();
				if (strComp == strPrName)
					return int.Parse(m_dtTrademarks.Rows[i]["MarComId"].ToString());
			}
			return -1;
		}

		private int GetRowIndex(int m_intSubId, DataTable m_dtaUpdateSubProd)
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
	}
}
