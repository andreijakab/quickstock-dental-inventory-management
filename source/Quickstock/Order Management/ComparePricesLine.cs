using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for PriceScanLine.
	/// </summary>
	public class ComparePricesLine : System.Windows.Forms.UserControl
    {
		private System.Windows.Forms.Label lblLineNumber;
		private System.Windows.Forms.ComboBox cmbUnits;
		private System.Windows.Forms.Label lblProduct;
		private System.Windows.Forms.Label lblTrademark;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		public event								ComparePricesLineContainer.ArrowKeysPressedHandler OnArrowKeyPress;

		private Color								m_clrSupplier1_BackColor, m_clrSupplier2_BackColor, m_clrSupplier3_BackColor, m_clrSupplier4_BackColor;		
		private Color								m_clrSupplier1_ForeColor, m_clrSupplier2_ForeColor, m_clrSupplier3_ForeColor, m_clrSupplier4_ForeColor;
		private ComparePricesLineContainer			m_cplcComparePricesLineContainer;
		private ComparePricesLineContainer.Supplier m_sSelectedSupplier;
		private double								m_dblLblProductProportion, m_dblLblTrademarkProportion, m_dblCmbUnitsProportion, m_dbltxtUnitPriceProportion;
		private int									m_intCategoryId, m_intProductId, m_intSubProductId, m_intTrademarkId;
		private int									m_intInterTextboxSpacing, m_intInterLabelSpacing;
		private string								m_strPackaging, m_strDecimalSeparator, m_strGroupSeparator;
        private PriceTextBox.PriceTextBox txtUnitPrice1;
        private PriceTextBox.PriceTextBox txtUnitPrice2;
        private PriceTextBox.PriceTextBox txtUnitPrice3;
        private PriceTextBox.PriceTextBox txtUnitPrice4;
		
		public ComparePricesLine(string strProductName, string strPackaging, string strTrademark, ComparePricesLineContainer cplcComparePricesLineContainer)
		{
			NumberFormatInfo nfiNumberFormat;
			ToolTip ttToolTip;

			InitializeComponent();
			
			// initialize local variables
			nfiNumberFormat = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
			ttToolTip = new ToolTip();

			// initialize global variables
			m_dblLblProductProportion = ((double) this.lblProduct.Width)/ ((double) this.Width);
			m_dblLblTrademarkProportion = ((double) this.lblTrademark.Width)/ ((double) this.Width);
			m_dblCmbUnitsProportion = ((double) this.cmbUnits.Width)/ ((double) this.Width);
			m_dbltxtUnitPriceProportion = ((double) this.txtUnitPrice1.Width)/ ((double) this.Width);
			m_intInterLabelSpacing = 2;
			m_intInterTextboxSpacing = 4;
			m_strDecimalSeparator = nfiNumberFormat.CurrencyDecimalSeparator;
			m_strGroupSeparator = nfiNumberFormat.CurrencyGroupSeparator;			

			// Set up the delays for the ToolTip.
			ttToolTip.AutoPopDelay = 5000;
			ttToolTip.InitialDelay = 1000;
			ttToolTip.ReshowDelay = 500;
			ttToolTip.ShowAlways = true;		// Force the ToolTip text to be displayed whether or not the form is active.
      
			// set tooltips
			ttToolTip.SetToolTip(this.lblLineNumber, "Click on this label to view the prices for this product!!!");
			ttToolTip.SetToolTip(this.lblProduct, "Product: " + strProductName + "\r\nPackaging: " + strPackaging + "\r\nTrademark: " + strTrademark);
			
			// initialize control
			this.CategoryId = -1;
			this.PriceComparisonContainer = cplcComparePricesLineContainer;
			this.Product = strProductName;
			this.ProductId = -1;
			this.SubProductId = -1;
			this.Trademark = strTrademark;
			this.TrademarkId = -1;
			this.UnitPrice1 = 0;
			this.UnitPrice2 = 0;
			this.UnitPrice3 = 0;
			this.UnitPrice4 = 0;
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
            this.lblLineNumber = new System.Windows.Forms.Label();
            this.cmbUnits = new System.Windows.Forms.ComboBox();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblTrademark = new System.Windows.Forms.Label();
            this.txtUnitPrice1 = new PriceTextBox.PriceTextBox();
            this.txtUnitPrice2 = new PriceTextBox.PriceTextBox();
            this.txtUnitPrice3 = new PriceTextBox.PriceTextBox();
            this.txtUnitPrice4 = new PriceTextBox.PriceTextBox();
            this.SuspendLayout();
            // 
            // lblLineNumber
            // 
            this.lblLineNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblLineNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLineNumber.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblLineNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineNumber.Location = new System.Drawing.Point(4, 0);
            this.lblLineNumber.Name = "lblLineNumber";
            this.lblLineNumber.Size = new System.Drawing.Size(24, 21);
            this.lblLineNumber.TabIndex = 5;
            this.lblLineNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbUnits
            // 
            this.cmbUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.cmbUnits.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20"});
            this.cmbUnits.Location = new System.Drawing.Point(532, 0);
            this.cmbUnits.Name = "cmbUnits";
            this.cmbUnits.Size = new System.Drawing.Size(48, 21);
            this.cmbUnits.TabIndex = 5;
            this.cmbUnits.SelectedIndexChanged += new System.EventHandler(this.cmbUnits_SelectedIndexChanged);
            this.cmbUnits.Leave += new System.EventHandler(this.cmbUnits_Leave);
            this.cmbUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUnits_KeyPress);
            // 
            // lblProduct
            // 
            this.lblProduct.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProduct.BackColor = System.Drawing.Color.White;
            this.lblProduct.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProduct.ForeColor = System.Drawing.Color.Black;
            this.lblProduct.Location = new System.Drawing.Point(32, 0);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(364, 20);
            this.lblProduct.TabIndex = 15;
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProduct.UseMnemonic = false;
            // 
            // lblTrademark
            // 
            this.lblTrademark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.lblTrademark.BackColor = System.Drawing.Color.White;
            this.lblTrademark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTrademark.ForeColor = System.Drawing.Color.Black;
            this.lblTrademark.Location = new System.Drawing.Point(400, 0);
            this.lblTrademark.Name = "lblTrademark";
            this.lblTrademark.Size = new System.Drawing.Size(128, 20);
            this.lblTrademark.TabIndex = 16;
            this.lblTrademark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTrademark.UseMnemonic = false;
            // 
            // txtUnitPrice1
            // 
            this.txtUnitPrice1.Location = new System.Drawing.Point(584, 0);
            this.txtUnitPrice1.Name = "txtUnitPrice1";
            this.txtUnitPrice1.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtUnitPrice1.Size = new System.Drawing.Size(100, 20);
            this.txtUnitPrice1.TabIndex = 1;
            this.txtUnitPrice1.Text = "0,00";
            this.txtUnitPrice1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUnitPrice1.DoubleClick += new System.EventHandler(this.txtUnitPrice1_DoubleClick);
            this.txtUnitPrice1.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnitPrice1_KeyDown);
            this.txtUnitPrice1.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnitPrice1_KeyPress);
            // 
            // txtUnitPrice2
            // 
            this.txtUnitPrice2.Location = new System.Drawing.Point(692, 0);
            this.txtUnitPrice2.Name = "txtUnitPrice2";
            this.txtUnitPrice2.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtUnitPrice2.Size = new System.Drawing.Size(100, 20);
            this.txtUnitPrice2.TabIndex = 2;
            this.txtUnitPrice2.Text = "0,00";
            this.txtUnitPrice2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUnitPrice2.DoubleClick += new System.EventHandler(this.txtUnitPrice2_DoubleClick);
            this.txtUnitPrice2.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnitPrice2_KeyDown);
            this.txtUnitPrice2.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnitPrice2_KeyPress);
            // 
            // txtUnitPrice3
            // 
            this.txtUnitPrice3.Location = new System.Drawing.Point(792, 0);
            this.txtUnitPrice3.Name = "txtUnitPrice3";
            this.txtUnitPrice3.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtUnitPrice3.Size = new System.Drawing.Size(100, 20);
            this.txtUnitPrice3.TabIndex = 3;
            this.txtUnitPrice3.Text = "0,00";
            this.txtUnitPrice3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUnitPrice3.DoubleClick += new System.EventHandler(this.txtUnitPrice3_DoubleClick);
            this.txtUnitPrice3.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnitPrice3_KeyDown);
            this.txtUnitPrice3.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnitPrice3_KeyPress);
            // 
            // txtUnitPrice4
            // 
            this.txtUnitPrice4.Location = new System.Drawing.Point(896, 0);
            this.txtUnitPrice4.Name = "txtUnitPrice4";
            this.txtUnitPrice4.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtUnitPrice4.Size = new System.Drawing.Size(100, 20);
            this.txtUnitPrice4.TabIndex = 4;
            this.txtUnitPrice4.Text = "0,00";
            this.txtUnitPrice4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUnitPrice4.DoubleClick += new System.EventHandler(this.txtUnitPrice4_DoubleClick);
            this.txtUnitPrice4.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtUnitPrice4_KeyDown);
            this.txtUnitPrice4.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnitPrice4_KeyPress);
            // 
            // ComparePricesLine
            // 
            this.Controls.Add(this.txtUnitPrice4);
            this.Controls.Add(this.txtUnitPrice3);
            this.Controls.Add(this.txtUnitPrice2);
            this.Controls.Add(this.txtUnitPrice1);
            this.Controls.Add(this.lblTrademark);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.cmbUnits);
            this.Controls.Add(this.lblLineNumber);
            this.Name = "ComparePricesLine";
            this.Size = new System.Drawing.Size(1000, 21);
            this.Resize += new System.EventHandler(this.ComparePricesLine_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
				
		#region Events
		private void cmbUnits_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			string strKeyInput = e.KeyChar.ToString();

			if (Char.IsDigit(e.KeyChar))
			{
				// Digits are OK
			}
			else if (e.KeyChar == '\b')
			{
				// Backspace key is OK
			}
			else if (e.KeyChar == (char)13)
			{
				this.Focus();
			}
			else
			{
				// Swallow this invalid key
				e.Handled = true;
			}

			this.PriceComparisonContainer.ChangesMade = true;
		}

		private void cmbUnits_Leave(object sender, System.EventArgs e)
		{
			decimal decNUnits;
			string strUnitPrice = this.cmbUnits.Text;
			
			if(strUnitPrice != null && strUnitPrice.Length > 0)
			{
				decNUnits = decimal.Parse(this.cmbUnits.Text);
				this.cmbUnits.Text = ((int) decNUnits).ToString();
			}
			else
				this.cmbUnits.Text = "1";		
		}

        private void cmbUnits_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.PriceComparisonContainer.ChangesMade = true;
        }

        private void ComparePricesLine_Resize(object sender, System.EventArgs e)
        {
            this.lblProduct.Width = (int)(m_dblLblProductProportion * this.Width);

            this.lblTrademark.Location = new Point(this.lblProduct.Location.X + this.lblProduct.Width + m_intInterLabelSpacing, this.lblTrademark.Location.Y);
            this.lblTrademark.Width = (int)(m_dblLblTrademarkProportion * this.Width);

            this.cmbUnits.Location = new Point(this.lblTrademark.Location.X + this.lblTrademark.Width + m_intInterLabelSpacing, this.cmbUnits.Location.Y);
            this.cmbUnits.Width = (int)(m_dblCmbUnitsProportion * this.Width);

            this.txtUnitPrice1.Location = new Point(this.cmbUnits.Location.X + this.cmbUnits.Width + m_intInterTextboxSpacing, this.txtUnitPrice1.Location.Y);
            this.txtUnitPrice1.Width = (int)(m_dbltxtUnitPriceProportion * this.Width);

            this.txtUnitPrice2.Location = new Point(this.txtUnitPrice1.Location.X + this.txtUnitPrice1.Width + m_intInterTextboxSpacing, this.txtUnitPrice2.Location.Y);
            this.txtUnitPrice2.Width = (int)(m_dbltxtUnitPriceProportion * this.Width);

            this.txtUnitPrice3.Location = new Point(this.txtUnitPrice2.Location.X + this.txtUnitPrice2.Width + m_intInterTextboxSpacing, this.txtUnitPrice3.Location.Y);
            this.txtUnitPrice3.Width = (int)(m_dbltxtUnitPriceProportion * this.Width);

            this.txtUnitPrice4.Location = new Point(this.txtUnitPrice3.Location.X + this.txtUnitPrice3.Width + m_intInterTextboxSpacing, this.txtUnitPrice4.Location.Y);
            this.txtUnitPrice4.Width = (int)(m_dbltxtUnitPriceProportion * this.Width);

            if (this.LineNumber == 1)
                m_cplcComparePricesLineContainer.ResizeControls(this.lblProduct.Width, this.lblProduct.Location.X,
                                                                this.lblTrademark.Width, this.lblTrademark.Location.X,
                                                                this.cmbUnits.Width, this.cmbUnits.Location.X,
                                                                this.txtUnitPrice1.Width,
                                                                this.txtUnitPrice1.Location.X, this.txtUnitPrice2.Location.X,
                                                                this.txtUnitPrice3.Location.X, this.txtUnitPrice4.Location.X);
        }

        private void txtUnitPrice1_DoubleClick(object sender, EventArgs e)
        {
            //this.txtUnitPrice1.Text = this.FormatUnitPrice(this.txtUnitPrice1.Text);
            this.txtUnitPrice1.SelectAll();
            if (this.UnitPrice1 != 0)
            {
                this.SelectedSupplier = ComparePricesLineContainer.Supplier.Supplier1;
                this.m_cplcComparePricesLineContainer.SetEnabledMakeOrderButtons();
            }
        }

        private void txtUnitPrice1_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) && OnArrowKeyPress != null)
                OnArrowKeyPress(e.KeyCode, this.LineNumber - 1, ComparePricesLineContainer.Supplier.Supplier1);
        }

        private void txtUnitPrice1_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            this.PriceComparisonContainer.ChangesMade = true;
        }

        private void txtUnitPrice2_DoubleClick(object sender, EventArgs e)
        {
            //this.txtUnitPrice2.Text = this.FormatUnitPrice(this.txtUnitPrice2.Text);
            this.txtUnitPrice2.SelectAll();
            if (this.UnitPrice2 != 0)
            {
                this.SelectedSupplier = ComparePricesLineContainer.Supplier.Supplier2;
                this.m_cplcComparePricesLineContainer.SetEnabledMakeOrderButtons();
            }
        }

        private void txtUnitPrice2_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) && OnArrowKeyPress != null)
                OnArrowKeyPress(e.KeyCode, this.LineNumber - 1, ComparePricesLineContainer.Supplier.Supplier2);
        }

        private void txtUnitPrice2_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            this.PriceComparisonContainer.ChangesMade = true;
        }

        private void txtUnitPrice3_DoubleClick(object sender, EventArgs e)
        {
            //this.txtUnitPrice3.Text = this.FormatUnitPrice(this.txtUnitPrice3.Text);
            this.txtUnitPrice3.SelectAll();
            if (this.UnitPrice3 != 0)
            {
                this.SelectedSupplier = ComparePricesLineContainer.Supplier.Supplier3;
                this.m_cplcComparePricesLineContainer.SetEnabledMakeOrderButtons();
            }
        }

        private void txtUnitPrice3_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) && OnArrowKeyPress != null)
                OnArrowKeyPress(e.KeyCode, this.LineNumber - 1, ComparePricesLineContainer.Supplier.Supplier3);
        }

        private void txtUnitPrice3_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            this.PriceComparisonContainer.ChangesMade = true;
        }

        private void txtUnitPrice4_DoubleClick(object sender, EventArgs e)
        {
            //this.txtUnitPrice4.Text = this.FormatUnitPrice(this.txtUnitPrice4.Text);
            this.txtUnitPrice4.SelectAll();
            if (this.UnitPrice4 != 0)
            {
                this.SelectedSupplier = ComparePricesLineContainer.Supplier.Supplier4;
                this.m_cplcComparePricesLineContainer.SetEnabledMakeOrderButtons();
            }
        }

        private void txtUnitPrice4_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Down || e.KeyCode == Keys.Up) && OnArrowKeyPress != null)
                OnArrowKeyPress(e.KeyCode, this.LineNumber - 1, ComparePricesLineContainer.Supplier.Supplier4);
        }

        private void txtUnitPrice4_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            this.PriceComparisonContainer.ChangesMade = true;
        }
		#endregion
	
		#region Properties
		public int CategoryId
		{
			set
			{
				m_intCategoryId = value;
			}
			get
			{
				return m_intCategoryId;
			}
		}

		public int LineNumber
		{
			set
			{
				this.lblLineNumber.Text = value.ToString();
			}
			get
			{
				return int.Parse(this.lblLineNumber.Text);
			}
		}
		
		private ComparePricesLineContainer PriceComparisonContainer
		{
			set
			{
				m_cplcComparePricesLineContainer = value;
			}
			get
			{
				return m_cplcComparePricesLineContainer;
			}
		}

		public string Product
		{
			set
			{
				this.lblProduct.Text = value;
			}
			get
			{
				return this.lblProduct.Text;
			}
		}

		public int ProductId
		{
			set
			{
				m_intProductId = value;
			}
			get
			{
				return m_intProductId;
			}
		}
		
		public ComparePricesLineContainer.Supplier SelectedSupplier
		{
			set
			{
				m_sSelectedSupplier = value;
				this.SetSelectedSupplier(m_sSelectedSupplier);
			}
			get
			{
				return m_sSelectedSupplier;
			}
		}

		public int SubProductId
		{
			set
			{
				m_intSubProductId = value;
			}
			get
			{
				return m_intSubProductId;
			}
		}
		
		public bool Ordered
		{
			set
			{
				this.Enabled = !value;
			}
			get
			{
				return !this.Enabled;
			}
		}

		public string Packaging
		{
			set
			{
				m_strPackaging = value;
			}
			get
			{
				return m_strPackaging;
			}
		}

		public string Trademark
		{
			set
			{
				this.lblTrademark.Text = value;
			}
			get
			{
				return this.lblTrademark.Text;
			}
		}

		public int TrademarkId
		{
			set
			{
				m_intTrademarkId = value;
			}
			get
			{
				return m_intTrademarkId;
			}
		}

		public int Units
		{
			set
			{
				this.cmbUnits.Text = value.ToString();
			}
			get
			{
				return (int) decimal.Parse(this.cmbUnits.Text);
			}
		}
	
		public decimal UnitPrice1
		{
			set
			{
				this.txtUnitPrice1.Price = value;
			}
			get
			{
                return this.txtUnitPrice1.Price;
			}
		}
		
		public decimal UnitPrice2
		{
			set
			{
                this.txtUnitPrice2.Price = value;
			}
			get
			{
                return this.txtUnitPrice2.Price;
			}
		}
		
		public decimal UnitPrice3
		{
			set
			{
                this.txtUnitPrice3.Price = value;
			}
			get
			{
                return this.txtUnitPrice3.Price;
			}
		}
		
		public decimal UnitPrice4
		{
			set
			{
                this.txtUnitPrice4.Price = value;
			}
			get
			{
                return this.txtUnitPrice4.Price;
			}
		}

		public bool UnitPrice1Enabled
		{
			set
			{
				this.txtUnitPrice1.Enabled = value;
			}
			get
			{
				return this.txtUnitPrice1.Enabled;
			}
		}

		public bool UnitPrice2Enabled
		{
			set
			{
				this.txtUnitPrice2.Enabled = value;
			}
			get
			{
				return this.txtUnitPrice2.Enabled;
			}
		}

		public bool UnitPrice3Enabled
		{
			set
			{
				this.txtUnitPrice3.Enabled = value;
			}
			get
			{
				return this.txtUnitPrice3.Enabled;
			}
		}

		public bool UnitPrice4Enabled
		{
			set
			{
				this.txtUnitPrice4.Enabled = value;
			}
			get
			{
				return this.txtUnitPrice4.Enabled;
			}
		}
		#endregion

		#region Methods
		private string FormatUnitPrice(string strUnitPrice)
		{
			decimal decUnitPrice;
			
			try
			{
				if(strUnitPrice != null && strUnitPrice.Length > 0)
					decUnitPrice = decimal.Parse(strUnitPrice);
				else
					decUnitPrice = 0;
			}
			catch(FormatException)
			{
				decUnitPrice = 0;
			}

			return decUnitPrice.ToString(clsUtilities.FORMAT_CURRENCY);
		}
		
		public decimal GetSelectedPrice()
		{
			decimal decSelectedPrice;

			switch(this.SelectedSupplier)
			{
				case ComparePricesLineContainer.Supplier.Supplier1:
					decSelectedPrice = this.UnitPrice1;
				break;

				case ComparePricesLineContainer.Supplier.Supplier2:
					decSelectedPrice = this.UnitPrice2;
				break;

				case ComparePricesLineContainer.Supplier.Supplier3:
					decSelectedPrice = this.UnitPrice3;
				break;
				
				case ComparePricesLineContainer.Supplier.Supplier4:
					decSelectedPrice = this.UnitPrice4;
				break;

				default:
					decSelectedPrice = 0;
				break;
			}

			return decSelectedPrice;
		}

		public void SelectBestPrice()
		{
			ComparePricesLineContainer.Supplier sMinPriceSupplier = ComparePricesLineContainer.Supplier.None;
			decimal decMinPrice = decimal.MaxValue;

			if(this.UnitPrice1 > 0 && this.UnitPrice1 < decMinPrice && this.txtUnitPrice1.Enabled)
			{
				decMinPrice = this.UnitPrice1;
				sMinPriceSupplier = ComparePricesLineContainer.Supplier.Supplier1;
			}

			if(this.UnitPrice2 > 0 && this.UnitPrice2 < decMinPrice && this.txtUnitPrice2.Enabled)
			{
				decMinPrice = this.UnitPrice2;
				sMinPriceSupplier = ComparePricesLineContainer.Supplier.Supplier2;
			}

			if(this.UnitPrice3 > 0 && this.UnitPrice3 < decMinPrice && this.txtUnitPrice3.Enabled)
			{
				decMinPrice = this.UnitPrice3;
				sMinPriceSupplier = ComparePricesLineContainer.Supplier.Supplier3;
			}

			if(this.UnitPrice4 > 0 && this.UnitPrice4 < decMinPrice && this.txtUnitPrice4.Enabled)
			{
				decMinPrice = this.UnitPrice4;
				sMinPriceSupplier = ComparePricesLineContainer.Supplier.Supplier4;
			}

			if(sMinPriceSupplier != ComparePricesLineContainer.Supplier.None)
				this.SelectedSupplier = sMinPriceSupplier;
		}

		public void SetColorScheme(Color clrSupplier1_BackColor, Color clrSupplier2_BackColor, Color clrSupplier3_BackColor, Color clrSupplier4_BackColor,
								   Color clrSupplier1_ForeColor, Color clrSupplier2_ForeColor, Color clrSupplier3_ForeColor, Color clrSupplier4_ForeColor)
		{
			m_clrSupplier1_BackColor = clrSupplier1_BackColor;
			m_clrSupplier2_BackColor = clrSupplier2_BackColor;
			m_clrSupplier3_BackColor = clrSupplier3_BackColor;
			m_clrSupplier4_BackColor = clrSupplier4_BackColor;
			m_clrSupplier1_ForeColor = clrSupplier1_ForeColor;
			m_clrSupplier2_ForeColor = clrSupplier2_ForeColor;
			m_clrSupplier3_ForeColor = clrSupplier3_ForeColor;
			m_clrSupplier4_ForeColor = clrSupplier4_ForeColor;

			this.txtUnitPrice1.ForeColor = m_clrSupplier1_BackColor;
			this.txtUnitPrice2.ForeColor = m_clrSupplier2_BackColor;
			this.txtUnitPrice3.ForeColor = m_clrSupplier3_BackColor;
			this.txtUnitPrice4.ForeColor = m_clrSupplier4_BackColor;
		}
		
		public void SetFocus(ComparePricesLineContainer.Supplier sSelectedSupplier)
		{
			switch(sSelectedSupplier)
			{
				case ComparePricesLineContainer.Supplier.Supplier1:
					this.txtUnitPrice1.TabIndex = 1;
					this.txtUnitPrice2.TabIndex = 2;
					this.txtUnitPrice3.TabIndex = 3;
					this.txtUnitPrice4.TabIndex = 4;
				break;

				case ComparePricesLineContainer.Supplier.Supplier2:
					this.txtUnitPrice1.TabIndex = 4;
					this.txtUnitPrice2.TabIndex = 1;
					this.txtUnitPrice3.TabIndex = 2;
					this.txtUnitPrice4.TabIndex = 3;
				break;
				
				case ComparePricesLineContainer.Supplier.Supplier3:
					this.txtUnitPrice1.TabIndex = 3;
					this.txtUnitPrice2.TabIndex = 4;
					this.txtUnitPrice3.TabIndex = 1;
					this.txtUnitPrice4.TabIndex = 2;
				break;

				case ComparePricesLineContainer.Supplier.Supplier4:
					this.txtUnitPrice1.TabIndex = 2;
					this.txtUnitPrice2.TabIndex = 3;
					this.txtUnitPrice3.TabIndex = 4;
					this.txtUnitPrice4.TabIndex = 1;
				break;
			}

			this.Focus();
		}

		private void SetSelectedSupplier(ComparePricesLineContainer.Supplier sSelectedSupplier)
		{
			switch(sSelectedSupplier)
			{
				case ComparePricesLineContainer.Supplier.Supplier1:
					this.txtUnitPrice1.ForeColor = m_clrSupplier1_ForeColor;
					this.txtUnitPrice1.BackColor = m_clrSupplier1_BackColor;
					this.txtUnitPrice2.ForeColor = m_clrSupplier2_BackColor;
					this.txtUnitPrice2.BackColor = m_clrSupplier2_ForeColor;
					this.txtUnitPrice3.ForeColor = m_clrSupplier3_BackColor;
					this.txtUnitPrice3.BackColor = m_clrSupplier3_ForeColor;
					this.txtUnitPrice4.ForeColor = m_clrSupplier4_BackColor;
					this.txtUnitPrice4.BackColor = m_clrSupplier4_ForeColor;
				break;
				
				case ComparePricesLineContainer.Supplier.Supplier2:
					this.txtUnitPrice1.ForeColor = m_clrSupplier1_BackColor;
					this.txtUnitPrice1.BackColor = m_clrSupplier1_ForeColor;
					this.txtUnitPrice2.ForeColor = m_clrSupplier2_ForeColor;
					this.txtUnitPrice2.BackColor = m_clrSupplier2_BackColor;
					this.txtUnitPrice3.ForeColor = m_clrSupplier3_BackColor;
					this.txtUnitPrice3.BackColor = m_clrSupplier3_ForeColor;
					this.txtUnitPrice4.ForeColor = m_clrSupplier4_BackColor;
					this.txtUnitPrice4.BackColor = m_clrSupplier4_ForeColor;
				break;
				
				case ComparePricesLineContainer.Supplier.Supplier3:
					this.txtUnitPrice1.ForeColor = m_clrSupplier1_BackColor;
					this.txtUnitPrice1.BackColor = m_clrSupplier1_ForeColor;
					this.txtUnitPrice2.ForeColor = m_clrSupplier2_BackColor;
					this.txtUnitPrice2.BackColor = m_clrSupplier2_ForeColor;
					this.txtUnitPrice3.ForeColor = m_clrSupplier3_ForeColor;
					this.txtUnitPrice3.BackColor = m_clrSupplier3_BackColor;
					this.txtUnitPrice4.ForeColor = m_clrSupplier4_BackColor;
					this.txtUnitPrice4.BackColor = m_clrSupplier4_ForeColor;
				break;
				
				case ComparePricesLineContainer.Supplier.Supplier4:
					this.txtUnitPrice1.ForeColor = m_clrSupplier1_BackColor;
					this.txtUnitPrice1.BackColor = m_clrSupplier1_ForeColor;
					this.txtUnitPrice2.ForeColor = m_clrSupplier2_BackColor;
					this.txtUnitPrice2.BackColor = m_clrSupplier2_ForeColor;
					this.txtUnitPrice3.ForeColor = m_clrSupplier3_BackColor;
					this.txtUnitPrice3.BackColor = m_clrSupplier3_ForeColor;
					this.txtUnitPrice4.ForeColor = m_clrSupplier4_ForeColor;
					this.txtUnitPrice4.BackColor = m_clrSupplier4_BackColor;
				break;

				case ComparePricesLineContainer.Supplier.None:
					this.txtUnitPrice1.ForeColor = m_clrSupplier1_BackColor;
					this.txtUnitPrice1.BackColor = m_clrSupplier1_ForeColor;
					this.txtUnitPrice2.ForeColor = m_clrSupplier2_BackColor;
					this.txtUnitPrice2.BackColor = m_clrSupplier2_ForeColor;
					this.txtUnitPrice3.ForeColor = m_clrSupplier3_BackColor;
					this.txtUnitPrice3.BackColor = m_clrSupplier3_ForeColor;
					this.txtUnitPrice4.ForeColor = m_clrSupplier4_BackColor;
					this.txtUnitPrice4.BackColor = m_clrSupplier4_ForeColor;
				break;
			}
		}
		#endregion
    }
}