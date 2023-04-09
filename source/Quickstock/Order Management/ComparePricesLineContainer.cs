using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Windows.Forms;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for OrderLineContainer.
	/// </summary>
	public class ComparePricesLineContainer : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TextBox txtComments;
		private System.Windows.Forms.Label lblSuppliersUnitPrices;
		private System.Windows.Forms.ComboBox cmbSupplier4;
		private System.Windows.Forms.ComboBox cmbSupplier3;
		private System.Windows.Forms.ComboBox cmbSupplier2;
		private System.Windows.Forms.ComboBox cmbSupplier1;
		private System.Windows.Forms.Label lblSupplierContact4;
		private System.Windows.Forms.Label lblSupplierPhone4;
		private System.Windows.Forms.Label lblSupplierContact3;
		private System.Windows.Forms.Label lblSupplierPhone3;
		private System.Windows.Forms.Label lblSupplierContact2;
		private System.Windows.Forms.Label lblSupplierPhone2;
		private System.Windows.Forms.Label lblSupplierContact1;
		private System.Windows.Forms.Label lblSupplierPhone1;
		private System.Windows.Forms.Button btnMakeOrder4;
		private System.Windows.Forms.Button btnMakeOrder3;
		private System.Windows.Forms.Button btnMakeOrder2;
		private System.Windows.Forms.Button btnMakeOrder1;
		private System.Windows.Forms.Button btnSelectBestPrices;
		private System.Windows.Forms.Panel pnlComparePricesLines;
		private System.Windows.Forms.Panel pnlButtons;
		private System.Windows.Forms.Label lblProduct;
		private System.Windows.Forms.Label lblUnits;
		private System.Windows.Forms.Label lblTrademark;
		private System.Windows.Forms.Label lblComments;
		private System.Windows.Forms.Label lblEmployee;
		private System.Windows.Forms.ComboBox cmbEmployees;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		public delegate void			ArrowKeysPressedHandler(Keys kArrowKeyCode, int intComparePricesLineNumber, Supplier sSupplier);
		public enum Supplier: int {None,Supplier1,Supplier2,Supplier3,Supplier4};
		public event					fclsOMComparePrices.MakeOrderHandler OnMakeOrder;
		
		private ArrayList				m_alComparePricesLines;
        private bool                    m_blnChangesMade, m_blnOrdersInProgress;
		private Color					m_clrSupplier1_BackColor, m_clrSupplier2_BackColor, m_clrSupplier3_BackColor, m_clrSupplier4_BackColor;		
		private Color					m_clrSupplier1_ForeColor, m_clrSupplier2_ForeColor, m_clrSupplier3_ForeColor, m_clrSupplier4_ForeColor;
		private DataTable				m_dtaEmployees, m_dtaSuppliers;
		private int						m_intInterComparePricesLineSpacing;
		private ToolTip					m_ttToolTip;

		public ComparePricesLineContainer()
		{
            NumberFormatInfo nfiNumberFormat;

            // Global variable initalization
            InitializeComponent();
			m_alComparePricesLines = new ArrayList();
			m_intInterComparePricesLineSpacing = 1;
			m_ttToolTip = new ToolTip();

            // initialize local variables
            nfiNumberFormat = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;

			// configure tool tips
			m_ttToolTip.AutoPopDelay = 5000;
			m_ttToolTip.InitialDelay = 1000;
			m_ttToolTip.ReshowDelay = 500;
			m_ttToolTip.ShowAlways = true;						// Force the ToolTip text to be displayed whether or not the form is active.

			// intialize object
            this.lblSuppliersUnitPrices.Text += " (in '" + nfiNumberFormat.CurrencySymbol + "')";
			this.ChangesMade = false;
			this.OrdersInProgress = false;
			this.Supplier1_BackColor = Color.Red;
			this.Supplier1_ForeColor = Color.White;
			this.Supplier2_BackColor = Color.Blue;
			this.Supplier2_ForeColor = Color.White;;
			this.Supplier3_BackColor = Color.Maroon;
			this.Supplier3_ForeColor = Color.White;;
			this.Supplier4_BackColor = Color.Green;
			this.Supplier4_ForeColor = Color.White;;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.pnlComparePricesLines = new System.Windows.Forms.Panel();
            this.txtComments = new System.Windows.Forms.TextBox();
            this.cmbSupplier4 = new System.Windows.Forms.ComboBox();
            this.cmbSupplier3 = new System.Windows.Forms.ComboBox();
            this.cmbSupplier2 = new System.Windows.Forms.ComboBox();
            this.cmbSupplier1 = new System.Windows.Forms.ComboBox();
            this.lblSupplierContact4 = new System.Windows.Forms.Label();
            this.lblSupplierPhone4 = new System.Windows.Forms.Label();
            this.lblSupplierContact3 = new System.Windows.Forms.Label();
            this.lblSupplierPhone3 = new System.Windows.Forms.Label();
            this.lblSupplierContact2 = new System.Windows.Forms.Label();
            this.lblSupplierPhone2 = new System.Windows.Forms.Label();
            this.lblSupplierContact1 = new System.Windows.Forms.Label();
            this.lblSupplierPhone1 = new System.Windows.Forms.Label();
            this.lblSuppliersUnitPrices = new System.Windows.Forms.Label();
            this.lblComments = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblUnits = new System.Windows.Forms.Label();
            this.lblTrademark = new System.Windows.Forms.Label();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnSelectBestPrices = new System.Windows.Forms.Button();
            this.btnMakeOrder4 = new System.Windows.Forms.Button();
            this.btnMakeOrder3 = new System.Windows.Forms.Button();
            this.btnMakeOrder2 = new System.Windows.Forms.Button();
            this.btnMakeOrder1 = new System.Windows.Forms.Button();
            this.cmbEmployees = new System.Windows.Forms.ComboBox();
            this.lblEmployee = new System.Windows.Forms.Label();
            this.pnlButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlComparePricesLines
            // 
            this.pnlComparePricesLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlComparePricesLines.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlComparePricesLines.Location = new System.Drawing.Point(0, 96);
            this.pnlComparePricesLines.Name = "pnlComparePricesLines";
            this.pnlComparePricesLines.Size = new System.Drawing.Size(1008, 360);
            this.pnlComparePricesLines.TabIndex = 0;
            // 
            // txtComments
            // 
            this.txtComments.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.txtComments.Location = new System.Drawing.Point(40, 504);
            this.txtComments.Multiline = true;
            this.txtComments.Name = "txtComments";
            this.txtComments.Size = new System.Drawing.Size(968, 160);
            this.txtComments.TabIndex = 2;
            // 
            // cmbSupplier4
            // 
            this.cmbSupplier4.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupplier4.ForeColor = System.Drawing.Color.Green;
            this.cmbSupplier4.Location = new System.Drawing.Point(898, 24);
            this.cmbSupplier4.Name = "cmbSupplier4";
            this.cmbSupplier4.Size = new System.Drawing.Size(100, 21);
            this.cmbSupplier4.TabIndex = 36;
            this.cmbSupplier4.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier4_SelectedIndexChanged);
            // 
            // cmbSupplier3
            // 
            this.cmbSupplier3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupplier3.ForeColor = System.Drawing.Color.Maroon;
            this.cmbSupplier3.Location = new System.Drawing.Point(794, 24);
            this.cmbSupplier3.Name = "cmbSupplier3";
            this.cmbSupplier3.Size = new System.Drawing.Size(100, 21);
            this.cmbSupplier3.TabIndex = 35;
            this.cmbSupplier3.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier3_SelectedIndexChanged);
            // 
            // cmbSupplier2
            // 
            this.cmbSupplier2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupplier2.ForeColor = System.Drawing.Color.Blue;
            this.cmbSupplier2.Location = new System.Drawing.Point(690, 24);
            this.cmbSupplier2.Name = "cmbSupplier2";
            this.cmbSupplier2.Size = new System.Drawing.Size(100, 21);
            this.cmbSupplier2.TabIndex = 34;
            this.cmbSupplier2.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier2_SelectedIndexChanged);
            // 
            // cmbSupplier1
            // 
            this.cmbSupplier1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSupplier1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupplier1.ForeColor = System.Drawing.Color.Red;
            this.cmbSupplier1.Location = new System.Drawing.Point(586, 24);
            this.cmbSupplier1.Name = "cmbSupplier1";
            this.cmbSupplier1.Size = new System.Drawing.Size(100, 21);
            this.cmbSupplier1.TabIndex = 33;
            this.cmbSupplier1.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier1_SelectedIndexChanged);
            // 
            // lblSupplierContact4
            // 
            this.lblSupplierContact4.BackColor = System.Drawing.Color.White;
            this.lblSupplierContact4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSupplierContact4.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierContact4.ForeColor = System.Drawing.Color.Green;
            this.lblSupplierContact4.Location = new System.Drawing.Point(898, 50);
            this.lblSupplierContact4.Name = "lblSupplierContact4";
            this.lblSupplierContact4.Size = new System.Drawing.Size(100, 16);
            this.lblSupplierContact4.TabIndex = 32;
            this.lblSupplierContact4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupplierPhone4
            // 
            this.lblSupplierPhone4.BackColor = System.Drawing.Color.White;
            this.lblSupplierPhone4.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSupplierPhone4.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierPhone4.ForeColor = System.Drawing.Color.Green;
            this.lblSupplierPhone4.Location = new System.Drawing.Point(898, 72);
            this.lblSupplierPhone4.Name = "lblSupplierPhone4";
            this.lblSupplierPhone4.Size = new System.Drawing.Size(100, 16);
            this.lblSupplierPhone4.TabIndex = 31;
            this.lblSupplierPhone4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupplierContact3
            // 
            this.lblSupplierContact3.BackColor = System.Drawing.Color.White;
            this.lblSupplierContact3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSupplierContact3.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierContact3.ForeColor = System.Drawing.Color.Maroon;
            this.lblSupplierContact3.Location = new System.Drawing.Point(794, 50);
            this.lblSupplierContact3.Name = "lblSupplierContact3";
            this.lblSupplierContact3.Size = new System.Drawing.Size(100, 16);
            this.lblSupplierContact3.TabIndex = 30;
            this.lblSupplierContact3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupplierPhone3
            // 
            this.lblSupplierPhone3.BackColor = System.Drawing.Color.White;
            this.lblSupplierPhone3.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSupplierPhone3.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierPhone3.ForeColor = System.Drawing.Color.Maroon;
            this.lblSupplierPhone3.Location = new System.Drawing.Point(794, 72);
            this.lblSupplierPhone3.Name = "lblSupplierPhone3";
            this.lblSupplierPhone3.Size = new System.Drawing.Size(100, 16);
            this.lblSupplierPhone3.TabIndex = 29;
            this.lblSupplierPhone3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupplierContact2
            // 
            this.lblSupplierContact2.BackColor = System.Drawing.Color.White;
            this.lblSupplierContact2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSupplierContact2.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierContact2.ForeColor = System.Drawing.Color.Blue;
            this.lblSupplierContact2.Location = new System.Drawing.Point(690, 50);
            this.lblSupplierContact2.Name = "lblSupplierContact2";
            this.lblSupplierContact2.Size = new System.Drawing.Size(100, 16);
            this.lblSupplierContact2.TabIndex = 28;
            this.lblSupplierContact2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupplierPhone2
            // 
            this.lblSupplierPhone2.BackColor = System.Drawing.Color.White;
            this.lblSupplierPhone2.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSupplierPhone2.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierPhone2.ForeColor = System.Drawing.Color.Blue;
            this.lblSupplierPhone2.Location = new System.Drawing.Point(690, 72);
            this.lblSupplierPhone2.Name = "lblSupplierPhone2";
            this.lblSupplierPhone2.Size = new System.Drawing.Size(100, 16);
            this.lblSupplierPhone2.TabIndex = 27;
            this.lblSupplierPhone2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupplierContact1
            // 
            this.lblSupplierContact1.BackColor = System.Drawing.Color.White;
            this.lblSupplierContact1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSupplierContact1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierContact1.ForeColor = System.Drawing.Color.Red;
            this.lblSupplierContact1.Location = new System.Drawing.Point(586, 50);
            this.lblSupplierContact1.Name = "lblSupplierContact1";
            this.lblSupplierContact1.Size = new System.Drawing.Size(100, 16);
            this.lblSupplierContact1.TabIndex = 26;
            this.lblSupplierContact1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSupplierPhone1
            // 
            this.lblSupplierPhone1.BackColor = System.Drawing.Color.White;
            this.lblSupplierPhone1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSupplierPhone1.Font = new System.Drawing.Font("Times New Roman", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSupplierPhone1.ForeColor = System.Drawing.Color.Red;
            this.lblSupplierPhone1.Location = new System.Drawing.Point(586, 72);
            this.lblSupplierPhone1.Name = "lblSupplierPhone1";
            this.lblSupplierPhone1.Size = new System.Drawing.Size(100, 16);
            this.lblSupplierPhone1.TabIndex = 25;
            this.lblSupplierPhone1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSuppliersUnitPrices
            // 
            this.lblSuppliersUnitPrices.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSuppliersUnitPrices.ForeColor = System.Drawing.Color.Blue;
            this.lblSuppliersUnitPrices.Location = new System.Drawing.Point(592, 0);
            this.lblSuppliersUnitPrices.Name = "lblSuppliersUnitPrices";
            this.lblSuppliersUnitPrices.Size = new System.Drawing.Size(400, 16);
            this.lblSuppliersUnitPrices.TabIndex = 41;
            this.lblSuppliersUnitPrices.Text = "Supplier Unit Prices";
            this.lblSuppliersUnitPrices.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblComments
            // 
            this.lblComments.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblComments.Font = new System.Drawing.Font("Arial", 11F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblComments.ForeColor = System.Drawing.Color.Blue;
            this.lblComments.Location = new System.Drawing.Point(8, 512);
            this.lblComments.Name = "lblComments";
            this.lblComments.Size = new System.Drawing.Size(16, 136);
            this.lblComments.TabIndex = 42;
            this.lblComments.Text = "Comments";
            // 
            // lblProduct
            // 
            this.lblProduct.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.ForeColor = System.Drawing.Color.Blue;
            this.lblProduct.Location = new System.Drawing.Point(34, 72);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(368, 16);
            this.lblProduct.TabIndex = 45;
            this.lblProduct.Text = "Product";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblUnits
            // 
            this.lblUnits.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUnits.ForeColor = System.Drawing.Color.Blue;
            this.lblUnits.Location = new System.Drawing.Point(534, 72);
            this.lblUnits.Name = "lblUnits";
            this.lblUnits.Size = new System.Drawing.Size(48, 16);
            this.lblUnits.TabIndex = 44;
            this.lblUnits.Text = "Units";
            this.lblUnits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTrademark
            // 
            this.lblTrademark.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrademark.ForeColor = System.Drawing.Color.Blue;
            this.lblTrademark.Location = new System.Drawing.Point(406, 72);
            this.lblTrademark.Name = "lblTrademark";
            this.lblTrademark.Size = new System.Drawing.Size(124, 16);
            this.lblTrademark.TabIndex = 43;
            this.lblTrademark.Text = "Trademark";
            this.lblTrademark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlButtons
            // 
            this.pnlButtons.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlButtons.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlButtons.Controls.Add(this.btnSelectBestPrices);
            this.pnlButtons.Controls.Add(this.btnMakeOrder4);
            this.pnlButtons.Controls.Add(this.btnMakeOrder3);
            this.pnlButtons.Controls.Add(this.btnMakeOrder2);
            this.pnlButtons.Controls.Add(this.btnMakeOrder1);
            this.pnlButtons.Location = new System.Drawing.Point(0, 464);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(1008, 32);
            this.pnlButtons.TabIndex = 46;
            // 
            // btnSelectBestPrices
            // 
            this.btnSelectBestPrices.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnSelectBestPrices.Location = new System.Drawing.Point(248, 4);
            this.btnSelectBestPrices.Name = "btnSelectBestPrices";
            this.btnSelectBestPrices.Size = new System.Drawing.Size(120, 24);
            this.btnSelectBestPrices.TabIndex = 45;
            this.btnSelectBestPrices.Text = "Select Best Prices";
            this.btnSelectBestPrices.Click += new System.EventHandler(this.btnSelectBestPrices_Click);
            // 
            // btnMakeOrder4
            // 
            this.btnMakeOrder4.Enabled = false;
            this.btnMakeOrder4.Location = new System.Drawing.Point(898, 4);
            this.btnMakeOrder4.Name = "btnMakeOrder4";
            this.btnMakeOrder4.Size = new System.Drawing.Size(100, 24);
            this.btnMakeOrder4.TabIndex = 44;
            this.btnMakeOrder4.Text = "Make Order";
            this.btnMakeOrder4.Click += new System.EventHandler(this.btnMakeOrder4_Click);
            // 
            // btnMakeOrder3
            // 
            this.btnMakeOrder3.Enabled = false;
            this.btnMakeOrder3.Location = new System.Drawing.Point(794, 4);
            this.btnMakeOrder3.Name = "btnMakeOrder3";
            this.btnMakeOrder3.Size = new System.Drawing.Size(100, 24);
            this.btnMakeOrder3.TabIndex = 43;
            this.btnMakeOrder3.Text = "Make Order";
            this.btnMakeOrder3.Click += new System.EventHandler(this.btnMakeOrder3_Click);
            // 
            // btnMakeOrder2
            // 
            this.btnMakeOrder2.Enabled = false;
            this.btnMakeOrder2.Location = new System.Drawing.Point(690, 4);
            this.btnMakeOrder2.Name = "btnMakeOrder2";
            this.btnMakeOrder2.Size = new System.Drawing.Size(100, 24);
            this.btnMakeOrder2.TabIndex = 42;
            this.btnMakeOrder2.Text = "Make Order";
            this.btnMakeOrder2.Click += new System.EventHandler(this.btnMakeOrder2_Click);
            // 
            // btnMakeOrder1
            // 
            this.btnMakeOrder1.Enabled = false;
            this.btnMakeOrder1.Location = new System.Drawing.Point(586, 4);
            this.btnMakeOrder1.Name = "btnMakeOrder1";
            this.btnMakeOrder1.Size = new System.Drawing.Size(100, 24);
            this.btnMakeOrder1.TabIndex = 41;
            this.btnMakeOrder1.Text = "Make Order";
            this.btnMakeOrder1.Click += new System.EventHandler(this.btnMakeOrder1_Click);
            // 
            // cmbEmployees
            // 
            this.cmbEmployees.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbEmployees.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbEmployees.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.cmbEmployees.Location = new System.Drawing.Point(120, 24);
            this.cmbEmployees.Name = "cmbEmployees";
            this.cmbEmployees.Size = new System.Drawing.Size(216, 24);
            this.cmbEmployees.TabIndex = 48;
            // 
            // lblEmployee
            // 
            this.lblEmployee.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblEmployee.ForeColor = System.Drawing.Color.Blue;
            this.lblEmployee.Location = new System.Drawing.Point(8, 26);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(104, 16);
            this.lblEmployee.TabIndex = 47;
            this.lblEmployee.Text = "Order made by";
            this.lblEmployee.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ComparePricesLineContainer
            // 
            this.Controls.Add(this.cmbEmployees);
            this.Controls.Add(this.lblEmployee);
            this.Controls.Add(this.pnlButtons);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.lblUnits);
            this.Controls.Add(this.lblTrademark);
            this.Controls.Add(this.lblComments);
            this.Controls.Add(this.lblSuppliersUnitPrices);
            this.Controls.Add(this.cmbSupplier4);
            this.Controls.Add(this.cmbSupplier3);
            this.Controls.Add(this.cmbSupplier2);
            this.Controls.Add(this.cmbSupplier1);
            this.Controls.Add(this.lblSupplierContact4);
            this.Controls.Add(this.lblSupplierPhone4);
            this.Controls.Add(this.lblSupplierContact3);
            this.Controls.Add(this.lblSupplierPhone3);
            this.Controls.Add(this.lblSupplierContact2);
            this.Controls.Add(this.lblSupplierPhone2);
            this.Controls.Add(this.lblSupplierContact1);
            this.Controls.Add(this.lblSupplierPhone1);
            this.Controls.Add(this.txtComments);
            this.Controls.Add(this.pnlComparePricesLines);
            this.Name = "ComparePricesLineContainer";
            this.Size = new System.Drawing.Size(1009, 672);
            this.Load += new System.EventHandler(this.ComparePricesLineContainer_Load);
            this.pnlButtons.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		#region Properties
		public bool ChangesMade
		{
			set
			{
				m_blnChangesMade = value;
			}
			get
			{
				return m_blnChangesMade;
			}
		}

		public string Comments
		{
			set
			{
				this.txtComments.Text = value;
			}
			get
			{
				return this.txtComments.Text;
			}
		}

		public ArrayList ComparePricesLines
		{
			get
			{
				return m_alComparePricesLines;
			}
		}

        public int SelectedEmployeeID
		{
			set
			{
				if(value > -1)
				{
					for(int i=0; i<m_dtaEmployees.Rows.Count; i++)
					{
						if(int.Parse(m_dtaEmployees.Rows[i]["EmployeeId"].ToString()) == value)
						{
							this.cmbEmployees.SelectedIndex = i;
							break;
						}
					}
				}
			}
			get
			{
				if(this.cmbSupplier1.SelectedIndex != -1)
					return int.Parse(m_dtaEmployees.Rows[this.cmbEmployees.SelectedIndex]["EmployeeId"].ToString());
				else
					return -1;
			}
		}

		public int SelectedSupplier1
		{
			set
			{
				if(value > -1)
				{
					for(int i=0; i<m_dtaSuppliers.Rows.Count; i++)
					{
						if(int.Parse(m_dtaSuppliers.Rows[i]["FournisseurId"].ToString()) == value)
						{
							this.cmbSupplier1.SelectedIndex = i;
							break;
						}
					}
				}
			}
			get
			{
				if(this.cmbSupplier1.SelectedIndex != -1)
					return int.Parse(m_dtaSuppliers.Rows[this.cmbSupplier1.SelectedIndex]["FournisseurId"].ToString());
				else
					return -1;
			}
		}

		public int SelectedSupplier2
		{
			set
			{
				if(value > -1)
				{
					for(int i=0; i<m_dtaSuppliers.Rows.Count; i++)
					{
						if(int.Parse(m_dtaSuppliers.Rows[i]["FournisseurId"].ToString()) == value)
						{
							this.cmbSupplier2.SelectedIndex = i;
							break;
						}
					}
				}
			}
			get
			{
				if(this.cmbSupplier2.SelectedIndex != -1)
					return int.Parse(m_dtaSuppliers.Rows[this.cmbSupplier2.SelectedIndex]["FournisseurId"].ToString());
				else
					return -1;
			}
		}

		public int SelectedSupplier3
		{
			set
			{
				if(value > -1)
				{
					for(int i=0; i<m_dtaSuppliers.Rows.Count; i++)
					{
						if(int.Parse(m_dtaSuppliers.Rows[i]["FournisseurId"].ToString()) == value)
						{
							this.cmbSupplier3.SelectedIndex = i;
							break;
						}
					}
				}
			}
			get
			{
				if(this.cmbSupplier3.SelectedIndex != -1)
					return int.Parse(m_dtaSuppliers.Rows[this.cmbSupplier3.SelectedIndex]["FournisseurId"].ToString());
				else
					return -1;
			}
		}

		public int SelectedSupplier4
		{
			set
			{
				if(value > -1)
				{
					for(int i=0; i<m_dtaSuppliers.Rows.Count; i++)
					{
						if(int.Parse(m_dtaSuppliers.Rows[i]["FournisseurId"].ToString()) == value)
						{
							this.cmbSupplier4.SelectedIndex = i;
							break;
						}
					}
				}
			}
			get
			{
				if(this.cmbSupplier4.SelectedIndex != -1)
					return int.Parse(m_dtaSuppliers.Rows[this.cmbSupplier4.SelectedIndex]["FournisseurId"].ToString());
				else
					return -1;
			}
		}

		public int NComparePricesLines
		{
			get
			{
				return m_alComparePricesLines.Count;
			}
		}

		public bool OrdersInProgress
		{
			set
			{
				m_blnOrdersInProgress = value;
			}
			get
			{
				return m_blnOrdersInProgress;
			}
		}

		public Color Supplier1_BackColor
		{
			set
			{
				m_clrSupplier1_BackColor = value;
			}
			get
			{
				return m_clrSupplier1_BackColor;
			}
		}

		public Color Supplier1_ForeColor
		{
			set
			{
				m_clrSupplier1_ForeColor = value;
			}
			get
			{
				return m_clrSupplier1_ForeColor;
			}
		}

		public Color Supplier2_BackColor
		{
			set
			{
				m_clrSupplier2_BackColor = value;
			}
			get
			{
				return m_clrSupplier2_BackColor;
			}
		}

		public Color Supplier2_ForeColor
		{
			set
			{
				m_clrSupplier2_ForeColor = value;
			}
			get
			{
				return m_clrSupplier2_ForeColor;
			}
		}

		public Color Supplier3_BackColor
		{
			set
			{
				m_clrSupplier3_BackColor = value;
			}
			get
			{
				return m_clrSupplier3_BackColor;
			}
		}

		public Color Supplier3_ForeColor
		{
			set
			{
				m_clrSupplier3_ForeColor = value;
			}
			get
			{
				return m_clrSupplier3_ForeColor;
			}
		}

		public Color Supplier4_BackColor
		{
			set
			{
				m_clrSupplier4_BackColor = value;
			}
			get
			{
				return m_clrSupplier4_BackColor;
			}
		}

		public Color Supplier4_ForeColor
		{
			set
			{
				m_clrSupplier4_ForeColor = value;
			}
			get
			{
				return m_clrSupplier4_ForeColor;
			}
		}
		#endregion
		
		#region Methods
		/// <summary>
		///		Adds a new order line to the container.
		/// </summary>
		public void Add(ComparePricesLine cplNewComparePricesLine)
		{
			// Variable declaration
			ComparePricesLine cplPreviousComparePricesLine;
			
			cplNewComparePricesLine.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			cplNewComparePricesLine.SetColorScheme(this.Supplier1_BackColor,this.Supplier2_BackColor,this.Supplier3_BackColor,this.Supplier4_BackColor,
												   this.Supplier1_ForeColor,this.Supplier2_ForeColor,this.Supplier3_ForeColor,this.Supplier4_ForeColor);

			cplNewComparePricesLine.OnArrowKeyPress += new ArrowKeysPressedHandler(ComparePricesLine_OnArrowKeyPress);
			
			if(m_alComparePricesLines.Count == 0)
			{
				cplNewComparePricesLine.Width = this.pnlComparePricesLines.Width;
				cplNewComparePricesLine.Location = new System.Drawing.Point(0, m_intInterComparePricesLineSpacing);
			}
			else
			{
				cplPreviousComparePricesLine = (ComparePricesLine) m_alComparePricesLines[this.NComparePricesLines - 1];
				cplNewComparePricesLine.Width = cplPreviousComparePricesLine.Width;
				cplNewComparePricesLine.Location = new System.Drawing.Point(0,cplPreviousComparePricesLine.Location.Y + cplPreviousComparePricesLine.Height + m_intInterComparePricesLineSpacing);
			}

			m_alComparePricesLines.Add(cplNewComparePricesLine);
			
			this.pnlComparePricesLines.Controls.Add(cplNewComparePricesLine);
		}

		/// <summary>
		///		Clears the container of all data.
		/// </summary>
		public void ClearData()
		{
			this.ResetSupplierPrices(Supplier.Supplier1);
			this.ResetSupplierPrices(Supplier.Supplier2);
			this.ResetSupplierPrices(Supplier.Supplier3);
			this.ResetSupplierPrices(Supplier.Supplier4);
			
			foreach(Object objComparePricesLine in this.ComparePricesLines)
				((ComparePricesLine) objComparePricesLine).SelectedSupplier = Supplier.None;

			this.txtComments.Text = "";
		}

		/// <summary>
		///		
		/// </summary>
		/// <param name="kArrowKeyCode">
		///		
		/// </param>
		/// <param name="intComparePricesLineIndex">
		///		
		/// </param>		
		private void ComparePricesLine_OnArrowKeyPress(Keys kArrowKeyCode, int intComparePricesLineIndex, Supplier sSupplier)
		{
			int intFocusLineIndex = -1;
			
			switch(kArrowKeyCode)
			{
				case Keys.Up:
					if(intComparePricesLineIndex != 0)
						intFocusLineIndex = intComparePricesLineIndex - 1;
					else
						intFocusLineIndex = (this.NComparePricesLines - 1);
					break;

				case Keys.Down:
					if(intComparePricesLineIndex != this.NComparePricesLines - 1)
						intFocusLineIndex = intComparePricesLineIndex + 1;
					else
						intFocusLineIndex = 0;
					break;
			}
			
			if(intFocusLineIndex != -1)
				((ComparePricesLine) this.ComparePricesLines[intFocusLineIndex]).SetFocus(sSupplier);
		}

		/// <summary>
		///		Displays the contact information of the selected supplier.
		/// </summary>
		/// <param name="sSupplier">
		///		Member of the Supplier enumeration that represent the currently selected cmbSupplier.
		/// </param>
		private void DisplaySupplierContactInformation(Supplier sSupplier)
		{
			ComboBox cmbSupplier = null;
			Label lblSupplierContact = null, lblSupplierPhone = null;
			DataRow dtrSelectedSupplier;
			string strSupplierContact, strPhoneNumber;

			// reset the displayed prices
			this.ResetSupplierPrices(sSupplier);

			if(m_dtaSuppliers != null)
			{
				switch(sSupplier)
				{
					case Supplier.Supplier1:
						cmbSupplier = this.cmbSupplier1;
						lblSupplierContact = this.lblSupplierContact1;
						lblSupplierPhone = this.lblSupplierPhone1;
						break;

					case Supplier.Supplier2:
						cmbSupplier = this.cmbSupplier2;
						lblSupplierContact = this.lblSupplierContact2;
						lblSupplierPhone = this.lblSupplierPhone2;
						break;

					case Supplier.Supplier3:
						cmbSupplier = this.cmbSupplier3;
						lblSupplierContact = this.lblSupplierContact3;
						lblSupplierPhone = this.lblSupplierPhone3;
						break;

					case Supplier.Supplier4:
						cmbSupplier = this.cmbSupplier4;
						lblSupplierContact = this.lblSupplierContact4;
						lblSupplierPhone = this.lblSupplierPhone4;
						break;
				}

				if(cmbSupplier.SelectedIndex != -1)
				{
					dtrSelectedSupplier = m_dtaSuppliers.Rows[cmbSupplier.SelectedIndex]; 
					strSupplierContact = clsUtilities.FormatName_Display(dtrSelectedSupplier["ConTitle"].ToString(),dtrSelectedSupplier["ContactFirstName"].ToString(),	dtrSelectedSupplier["ContactLastName"].ToString());
					strPhoneNumber = dtrSelectedSupplier["PhoneNumber"].ToString();
				}
				else
				{
					strSupplierContact = "";
					strPhoneNumber = "";
				}

				lblSupplierContact.Text = strSupplierContact;
				m_ttToolTip.SetToolTip(lblSupplierContact,strSupplierContact);

				lblSupplierPhone.Text = strPhoneNumber;
				m_ttToolTip.SetToolTip(lblSupplierPhone,strPhoneNumber);
			}
		}

		/// <summary>
		///		Retrieves requested supplier's database id.
		/// </summary>
		/// <param name="sSupplier">
		///		Member of the Supplier enumeration that represents the currently the requested supplier.
		/// </param>
		/// <returns>
		///		Returns the requested supplier's database id if sSupplier is valid, -1 otherwise.
		/// </returns>
		public SupplierInformation GetSupplierInformation(Supplier sSupplier)
		{
			SupplierInformation CurrentSupplier = new SupplierInformation();
			
			switch(sSupplier)
			{
				case Supplier.Supplier1:
					CurrentSupplier.DatabaseID = this.SelectedSupplier1;
					CurrentSupplier.Email = m_dtaSuppliers.Rows[this.cmbSupplier1.SelectedIndex]["Email"].ToString();
				break;

				case Supplier.Supplier2:
					CurrentSupplier.DatabaseID = this.SelectedSupplier2;
					CurrentSupplier.Email = m_dtaSuppliers.Rows[this.cmbSupplier2.SelectedIndex]["Email"].ToString();
				break;

				case Supplier.Supplier3:
					CurrentSupplier.DatabaseID = this.SelectedSupplier3;
					CurrentSupplier.Email = m_dtaSuppliers.Rows[this.cmbSupplier3.SelectedIndex]["Email"].ToString();
				break;

				case Supplier.Supplier4:
					CurrentSupplier.DatabaseID = this.SelectedSupplier4;
					CurrentSupplier.Email = m_dtaSuppliers.Rows[this.cmbSupplier4.SelectedIndex]["Email"].ToString();
				break;

				default:
					CurrentSupplier.DatabaseID = -1;
					CurrentSupplier.Email = "";
				break;
			}

			return CurrentSupplier;
		}
		
		/// <summary>
		///		Loads the employee information into the cmbEmployees combo box.
		/// </summary>
		/// <param name="dtaSuppliers">
		///		DataTable containing the list of employees.
		/// </param>
		public void LoadEmployees(DataTable dtaEmployees)
		{
			m_dtaEmployees = dtaEmployees;

			foreach(DataRow dtrRow in m_dtaEmployees.Rows)
				this.cmbEmployees.Items.Add(clsUtilities.FormatName_List(dtrRow["Title"].ToString(), dtrRow["FirstName"].ToString(), dtrRow["LastName"].ToString()));
		}

		/// <summary>
		///		Loads the supplier information into the cmbSupplier combo boxes.
		/// </summary>
		/// <param name="dtaSuppliers">
		///		DataTable containing the list of suppliers.
		/// </param>
		public void LoadSuppliers(DataTable dtaSuppliers)
		{
			m_dtaSuppliers = dtaSuppliers;

			foreach(DataRow dtrRow in m_dtaSuppliers.Rows)
			{
				this.cmbSupplier1.Items.Add(dtrRow["CompanyName"].ToString());
				this.cmbSupplier2.Items.Add(dtrRow["CompanyName"].ToString());
				this.cmbSupplier3.Items.Add(dtrRow["CompanyName"].ToString());
				this.cmbSupplier4.Items.Add(dtrRow["CompanyName"].ToString());
			}
		}
		
		/// <summary>
		///		Resets prices of a given supplier.
		/// </summary>
		/// <param name="sSupplier">
		///		Member of the Supplier enumeration that represent the currently selected cmbSupplier.
		/// </param>
		private void ResetSupplierPrices(Supplier sSupplier)
		{
			switch(sSupplier)
			{
				case Supplier.Supplier1:
					foreach(Object objComparePricesLine in m_alComparePricesLines)
						((ComparePricesLine) objComparePricesLine).UnitPrice1 = 0;
				break;

				case Supplier.Supplier2:
					foreach(Object objComparePricesLine in m_alComparePricesLines)
						((ComparePricesLine) objComparePricesLine).UnitPrice2 = 0;
				break;

				case Supplier.Supplier3:
					foreach(Object objComparePricesLine in m_alComparePricesLines)
						((ComparePricesLine) objComparePricesLine).UnitPrice3 = 0;
				break;

				case Supplier.Supplier4:
					foreach(Object objComparePricesLine in m_alComparePricesLines)
						((ComparePricesLine) objComparePricesLine).UnitPrice4 = 0;
				break;
			}
		}
		
		/// <summary>
		///		Function called by the first ComparePricesLine in order to resize the controls in the container.
		/// </summary>
		public void ResizeControls(int intProductWidth, int intProductXPos,int intTrademarkWidth, int intTrademarkXPos, int intUnitsWidth, int intUnitsXPos, int intUnitPriceWidth, int intUnitPrice1XPos, int intUnitPrice2XPos, int intUnitPrice3XPos, int intUnitPrice4XPos)
		{
			int intPositionOffset = 2;

			this.lblProduct.Width = intProductWidth;
			this.lblProduct.Location = new Point(intProductXPos + intPositionOffset,this.lblProduct.Location.Y);
			
			this.lblTrademark.Width = intTrademarkWidth;
			this.lblTrademark.Location = new Point(intTrademarkXPos + intPositionOffset,this.lblTrademark.Location.Y);
			
			this.lblUnits.Width = intUnitsWidth;
			this.lblUnits.Location = new Point(intUnitsXPos + intPositionOffset,this.lblUnits.Location.Y);
			
			this.btnMakeOrder1.Location = new Point(intUnitPrice1XPos + intPositionOffset,this.btnMakeOrder1.Location.Y);
			this.cmbSupplier1.Location = new Point(intUnitPrice1XPos + intPositionOffset,this.cmbSupplier1.Location.Y);
			this.lblSupplierContact1.Location = new Point(intUnitPrice1XPos + intPositionOffset,this.lblSupplierContact1.Location.Y);
			this.lblSupplierPhone1.Location = new Point(intUnitPrice1XPos + intPositionOffset,this.lblSupplierPhone1.Location.Y);
			this.cmbSupplier1.Width = this.lblSupplierContact1.Width = this.lblSupplierPhone1.Width = this.btnMakeOrder1.Width = intUnitPriceWidth;

			this.btnMakeOrder2.Location = new Point(intUnitPrice2XPos + intPositionOffset,this.btnMakeOrder2.Location.Y);
			this.cmbSupplier2.Location = new Point(intUnitPrice2XPos + intPositionOffset,this.cmbSupplier2.Location.Y);
			this.lblSupplierContact2.Location = new Point(intUnitPrice2XPos + intPositionOffset,this.lblSupplierContact2.Location.Y);
			this.lblSupplierPhone2.Location = new Point(intUnitPrice2XPos + intPositionOffset,this.lblSupplierPhone2.Location.Y);
			this.cmbSupplier2.Width = this.lblSupplierContact2.Width = this.lblSupplierPhone2.Width = this.btnMakeOrder2.Width = intUnitPriceWidth;
			
			this.btnMakeOrder3.Location = new Point(intUnitPrice3XPos + intPositionOffset,this.btnMakeOrder3.Location.Y);
			this.cmbSupplier3.Location = new Point(intUnitPrice3XPos + intPositionOffset,this.cmbSupplier3.Location.Y);
			this.lblSupplierContact3.Location = new Point(intUnitPrice3XPos + intPositionOffset,this.lblSupplierContact3.Location.Y);
			this.lblSupplierPhone3.Location = new Point(intUnitPrice3XPos + intPositionOffset,this.lblSupplierPhone3.Location.Y);
			this.cmbSupplier3.Width = this.lblSupplierContact3.Width = this.lblSupplierPhone3.Width = this.btnMakeOrder3.Width = intUnitPriceWidth;

			this.btnMakeOrder4.Location = new Point(intUnitPrice4XPos + intPositionOffset,this.btnMakeOrder4.Location.Y);
			this.cmbSupplier4.Location = new Point(intUnitPrice4XPos + intPositionOffset,this.cmbSupplier4.Location.Y);
			this.lblSupplierContact4.Location = new Point(intUnitPrice4XPos + intPositionOffset,this.lblSupplierContact4.Location.Y);
			this.lblSupplierPhone4.Location = new Point(intUnitPrice4XPos + intPositionOffset,this.lblSupplierPhone4.Location.Y);
			this.cmbSupplier4.Width = this.lblSupplierContact4.Width = this.lblSupplierPhone4.Width = this.btnMakeOrder4.Width = intUnitPriceWidth;

			this.lblSuppliersUnitPrices.Location = new Point(this.cmbSupplier1.Location.X,this.lblSuppliersUnitPrices.Location.Y);
			this.lblSuppliersUnitPrices.Width = this.cmbSupplier4.Location.X + this.cmbSupplier4.Width - this.cmbSupplier1.Location.X;

			this.btnSelectBestPrices.Location = new Point((int) ((this.btnMakeOrder1.Location.X / 2) - (this.btnSelectBestPrices.Width / 2)),this.btnSelectBestPrices.Location.Y);
		}
		
		/// <summary>
		///		Function called each time the selected supplier of a ComparePricesLine changes in order
		///		to determine which btnMakeOrder should be enabled.
		/// </summary>
		public void SetEnabledMakeOrderButtons()
		{
			this.btnMakeOrder1.Enabled = false;
			this.btnMakeOrder2.Enabled = false;
			this.btnMakeOrder3.Enabled = false;
			this.btnMakeOrder4.Enabled = false;

			foreach(Object objCurrentComparePricesLine in this.ComparePricesLines)
			{
				switch(((ComparePricesLine) objCurrentComparePricesLine).SelectedSupplier)
				{
					case Supplier.Supplier1:
						if(this.cmbSupplier1.Enabled)
							this.btnMakeOrder1.Enabled = true;
					break;
					
					case Supplier.Supplier2:
						if(this.cmbSupplier2.Enabled)
							this.btnMakeOrder2.Enabled = true;
					break;
					
					case Supplier.Supplier3:
						if(this.cmbSupplier3.Enabled)
							this.btnMakeOrder3.Enabled = true;
					break;
					
					case Supplier.Supplier4:
						if(this.cmbSupplier4.Enabled)
							this.btnMakeOrder4.Enabled = true;
					break;
				}
			}
		}
		/// <summary>
		///		Sets the Enabled property of all the controls associated with a particular supplier.
		/// </summary>
		/// <param name="sSupplier">
		///		Member of the Supplier enumeration that represents the controls of one particular supplier.
		/// </param>
		/// <param name="blnEnabled">
		///		Value of the Enabled property.
		/// </param>
		public void SetSupplierEnabled(Supplier sSupplier, bool blnEnabled)
		{
			switch(sSupplier)
			{
				case Supplier.Supplier1:
					this.cmbSupplier1.Enabled = this.lblSupplierContact1.Enabled = this.lblSupplierPhone1.Enabled = this.btnMakeOrder1.Enabled = blnEnabled;
					foreach(Object objComparePricesLine in m_alComparePricesLines)
						((ComparePricesLine) objComparePricesLine).UnitPrice1Enabled = blnEnabled;
				break;

				case Supplier.Supplier2:
					this.cmbSupplier2.Enabled = this.lblSupplierContact2.Enabled = this.lblSupplierPhone2.Enabled = this.btnMakeOrder2.Enabled = blnEnabled;
					foreach(Object objComparePricesLine in m_alComparePricesLines)
						((ComparePricesLine) objComparePricesLine).UnitPrice2Enabled = blnEnabled;
				break;

				case Supplier.Supplier3:
					this.cmbSupplier3.Enabled = this.lblSupplierContact3.Enabled = this.lblSupplierPhone3.Enabled = this.btnMakeOrder3.Enabled = blnEnabled;
					foreach(Object objComparePricesLine in m_alComparePricesLines)
						((ComparePricesLine) objComparePricesLine).UnitPrice3Enabled = blnEnabled;
				break;

				case Supplier.Supplier4:
					this.cmbSupplier4.Enabled = this.lblSupplierContact4.Enabled = this.lblSupplierPhone4.Enabled = this.btnMakeOrder4.Enabled = blnEnabled;
					foreach(Object objComparePricesLine in m_alComparePricesLines)
						((ComparePricesLine) objComparePricesLine).UnitPrice4Enabled = blnEnabled;
				break;
			}
		}
		#endregion

		#region Events
		private void cmbSupplier1_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.DisplaySupplierContactInformation(Supplier.Supplier1);
		}

		private void cmbSupplier2_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.DisplaySupplierContactInformation(Supplier.Supplier2);
		}

		private void cmbSupplier3_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.DisplaySupplierContactInformation(Supplier.Supplier3);
		}

		private void cmbSupplier4_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.DisplaySupplierContactInformation(Supplier.Supplier4);
		}
		private void btnSelectBestPrices_Click(object sender, System.EventArgs e)
		{
			ComparePricesLine cplCurrentLine;

			foreach(Object objCurrentComparePricesLine in this.ComparePricesLines)
			{
				cplCurrentLine = (ComparePricesLine) objCurrentComparePricesLine;
				
				// only select best price if the current line is enable i.e. if the product has not
				// been ordered already
				if(cplCurrentLine.Enabled)
					cplCurrentLine.SelectBestPrice();
			}

			this.SetEnabledMakeOrderButtons();
		}		
		private void ComparePricesLineContainer_Load(object sender, System.EventArgs e)
		{
			// apply color scheme to supplier information controls
			this.cmbSupplier1.ForeColor = this.lblSupplierContact1.ForeColor = this.lblSupplierPhone1.ForeColor = m_clrSupplier1_BackColor;
			this.cmbSupplier2.ForeColor = this.lblSupplierContact2.ForeColor = this.lblSupplierPhone2.ForeColor = m_clrSupplier2_BackColor;
			this.cmbSupplier3.ForeColor = this.lblSupplierContact3.ForeColor = this.lblSupplierPhone3.ForeColor = m_clrSupplier3_BackColor;
			this.cmbSupplier4.ForeColor = this.lblSupplierContact4.ForeColor = this.lblSupplierPhone4.ForeColor = m_clrSupplier4_BackColor;
		}

		private void btnMakeOrder1_Click(object sender, System.EventArgs e)
		{
			if(OnMakeOrder != null)
				OnMakeOrder(Supplier.Supplier1);
		}

		private void btnMakeOrder2_Click(object sender, System.EventArgs e)
		{
			if(OnMakeOrder != null)
				OnMakeOrder(Supplier.Supplier2);
		}

		private void btnMakeOrder3_Click(object sender, System.EventArgs e)
		{
			if(OnMakeOrder != null)
				OnMakeOrder(Supplier.Supplier3);
		}

		private void btnMakeOrder4_Click(object sender, System.EventArgs e)
		{
			if(OnMakeOrder != null)
				OnMakeOrder(Supplier.Supplier4);
		}
		#endregion
	}
}
