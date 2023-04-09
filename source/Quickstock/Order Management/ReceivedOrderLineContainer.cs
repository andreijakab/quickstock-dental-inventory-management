using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;	
using System.Windows.Forms;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for OrderLineContainer.
	/// </summary>
	public class ReceivedOrderLineContainer : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label lblPack;
		private System.Windows.Forms.Label lblTrademark;
		private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Panel pnlOrderLines;
        private System.Windows.Forms.Label lblDuty;
        private System.Windows.Forms.Label lblShippingHandling;
		private System.Windows.Forms.Label lblTaxes;
		private System.Windows.Forms.Label lblOrderDate;
		public System.Windows.Forms.Label lblOrderDate_Data;
		private System.Windows.Forms.Panel pnlOrderInformation;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private int					m_intInterOrderLineSpacing;

        private bool                    m_blnChangesMade, m_blnReadOnly;
		private double					m_dblProductLabelProportion, m_dblTrademarkLabelProportion, m_dblPackagingLabelProportion;
		private int						m_intLabelSpacing;
		private NumberFormatInfo		m_nfiNumberFormat;
		private ReceivedOrderLine[]		m_rolOrderLines;
		private string					m_strDecimalSeparator, m_strGroupSeparator;
        private PriceTextBox.PriceTextBox txtTaxes;
        private PriceTextBox.PriceTextBox txtDuty;
        private PriceTextBox.PriceTextBox txtShippingHandling;
        private Label lblCurrency;		

		public ReceivedOrderLineContainer()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Variable declaration
			ToolTip ttToolTip;

			// Global variable initalization
			//m_intMaxShownOrderLines = 0;
			//m_intReceivedOrderLine_Height = 53;
			//m_intShownOrderLines = 0;
			//m_intTopShownOrderLineIndex = -1;
			//m_blnAllOrderLinesLoaded = false;
			m_dblProductLabelProportion = ((double) this.lblProduct.Size.Width)/((double)this.Width);
			m_dblTrademarkLabelProportion = ((double) this.lblTrademark.Size.Width)/((double)this.Width);
			m_dblPackagingLabelProportion = ((double) this.lblPack.Size.Width)/((double)this.Width);
			m_intInterOrderLineSpacing = 1;
			m_intLabelSpacing = this.lblTrademark.Location.X - (this.lblProduct.Location.X + this.lblProduct.Size.Width);
			m_nfiNumberFormat =	System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
			
			// Local variable initialization
			ttToolTip = new ToolTip();

			// Get local number formatting information
			m_strDecimalSeparator = m_nfiNumberFormat.CurrencyDecimalSeparator;
			m_strGroupSeparator = m_nfiNumberFormat.CurrencyGroupSeparator;

			// Configure tooltips
			ttToolTip.AutoPopDelay = 5000;
			ttToolTip.InitialDelay = 1000;
			ttToolTip.ReshowDelay = 500;
			ttToolTip.ShowAlways = true;
      
			// Set up the ToolTip text for the Button and Checkbox.
			//ttToolTip.SetToolTip(this.btnAllReceived, "Click here if All the units for All products are received!!!");

			// Object initialization
            this.lblCurrency.Text += "'" + m_nfiNumberFormat.CurrencySymbol + "'.";
            this.ReadOnly = false;
			this.Duty = 0.0M;
			this.ShippingHandling = 0.0M;
			this.Taxes = 0.0M;
			this.ChangesMade = false;
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
            this.pnlOrderLines = new System.Windows.Forms.Panel();
            this.pnlOrderInformation = new System.Windows.Forms.Panel();
            this.txtTaxes = new PriceTextBox.PriceTextBox();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.lblOrderDate_Data = new System.Windows.Forms.Label();
            this.lblOrderDate = new System.Windows.Forms.Label();
            this.lblDuty = new System.Windows.Forms.Label();
            this.lblShippingHandling = new System.Windows.Forms.Label();
            this.lblTaxes = new System.Windows.Forms.Label();
            this.lblPack = new System.Windows.Forms.Label();
            this.lblTrademark = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.txtShippingHandling = new PriceTextBox.PriceTextBox();
            this.txtDuty = new PriceTextBox.PriceTextBox();
            this.pnlOrderInformation.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlOrderLines
            // 
            this.pnlOrderLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOrderLines.AutoScroll = true;
            this.pnlOrderLines.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlOrderLines.ForeColor = System.Drawing.SystemColors.Control;
            this.pnlOrderLines.Location = new System.Drawing.Point(0, 24);
            this.pnlOrderLines.Name = "pnlOrderLines";
            this.pnlOrderLines.Size = new System.Drawing.Size(878, 651);
            this.pnlOrderLines.TabIndex = 0;
            // 
            // pnlOrderInformation
            // 
            this.pnlOrderInformation.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOrderInformation.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlOrderInformation.Controls.Add(this.txtDuty);
            this.pnlOrderInformation.Controls.Add(this.txtShippingHandling);
            this.pnlOrderInformation.Controls.Add(this.txtTaxes);
            this.pnlOrderInformation.Controls.Add(this.lblCurrency);
            this.pnlOrderInformation.Controls.Add(this.lblOrderDate_Data);
            this.pnlOrderInformation.Controls.Add(this.lblOrderDate);
            this.pnlOrderInformation.Controls.Add(this.lblDuty);
            this.pnlOrderInformation.Controls.Add(this.lblShippingHandling);
            this.pnlOrderInformation.Controls.Add(this.lblTaxes);
            this.pnlOrderInformation.Location = new System.Drawing.Point(0, 678);
            this.pnlOrderInformation.Name = "pnlOrderInformation";
            this.pnlOrderInformation.Size = new System.Drawing.Size(878, 40);
            this.pnlOrderInformation.TabIndex = 1;
            // 
            // txtTaxes
            // 
            this.txtTaxes.Location = new System.Drawing.Point(280, 8);
            this.txtTaxes.Name = "txtTaxes";
            this.txtTaxes.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtTaxes.Size = new System.Drawing.Size(56, 20);
            this.txtTaxes.TabIndex = 60;
            this.txtTaxes.Text = "0,00";
            this.txtTaxes.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtTaxes.OnEnterKeyPress += new PriceTextBox.PriceTextBox.EnterKeyPress(this.txtTaxes_OnEnterKeyPress);
            this.txtTaxes.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtTaxes_KeyPress);
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrency.ForeColor = System.Drawing.Color.Black;
            this.lblCurrency.Location = new System.Drawing.Point(711, 8);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(111, 13);
            this.lblCurrency.TabIndex = 80;
            this.lblCurrency.Text = "Note: all prices are in ";
            // 
            // lblOrderDate_Data
            // 
            this.lblOrderDate_Data.BackColor = System.Drawing.Color.White;
            this.lblOrderDate_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOrderDate_Data.Location = new System.Drawing.Point(88, 8);
            this.lblOrderDate_Data.Name = "lblOrderDate_Data";
            this.lblOrderDate_Data.Size = new System.Drawing.Size(132, 21);
            this.lblOrderDate_Data.TabIndex = 79;
            this.lblOrderDate_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblOrderDate
            // 
            this.lblOrderDate.AutoSize = true;
            this.lblOrderDate.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderDate.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblOrderDate.Location = new System.Drawing.Point(8, 9);
            this.lblOrderDate.Name = "lblOrderDate";
            this.lblOrderDate.Size = new System.Drawing.Size(80, 16);
            this.lblOrderDate.TabIndex = 65;
            this.lblOrderDate.Text = "Order Date";
            // 
            // lblDuty
            // 
            this.lblDuty.AutoSize = true;
            this.lblDuty.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDuty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblDuty.Location = new System.Drawing.Point(584, 8);
            this.lblDuty.Name = "lblDuty";
            this.lblDuty.Size = new System.Drawing.Size(39, 16);
            this.lblDuty.TabIndex = 63;
            this.lblDuty.Text = "Duty";
            // 
            // lblShippingHandling
            // 
            this.lblShippingHandling.AutoSize = true;
            this.lblShippingHandling.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShippingHandling.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblShippingHandling.Location = new System.Drawing.Point(360, 8);
            this.lblShippingHandling.Name = "lblShippingHandling";
            this.lblShippingHandling.Size = new System.Drawing.Size(149, 16);
            this.lblShippingHandling.TabIndex = 61;
            this.lblShippingHandling.Text = "Shipping and Handling";
            // 
            // lblTaxes
            // 
            this.lblTaxes.AutoSize = true;
            this.lblTaxes.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaxes.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTaxes.Location = new System.Drawing.Point(232, 8);
            this.lblTaxes.Name = "lblTaxes";
            this.lblTaxes.Size = new System.Drawing.Size(45, 16);
            this.lblTaxes.TabIndex = 59;
            this.lblTaxes.Text = "Taxes";
            // 
            // lblPack
            // 
            this.lblPack.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPack.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPack.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblPack.Location = new System.Drawing.Point(664, 0);
            this.lblPack.Name = "lblPack";
            this.lblPack.Size = new System.Drawing.Size(208, 24);
            this.lblPack.TabIndex = 44;
            this.lblPack.Text = "Packaging";
            this.lblPack.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTrademark
            // 
            this.lblTrademark.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTrademark.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrademark.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblTrademark.Location = new System.Drawing.Point(472, 0);
            this.lblTrademark.Name = "lblTrademark";
            this.lblTrademark.Size = new System.Drawing.Size(192, 24);
            this.lblTrademark.TabIndex = 43;
            this.lblTrademark.Text = "Trademark";
            this.lblTrademark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProduct
            // 
            this.lblProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblProduct.Location = new System.Drawing.Point(40, 0);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(432, 24);
            this.lblProduct.TabIndex = 42;
            this.lblProduct.Text = "Product Name";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtShippingHandling
            // 
            this.txtShippingHandling.Location = new System.Drawing.Point(512, 8);
            this.txtShippingHandling.Name = "txtShippingHandling";
            this.txtShippingHandling.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtShippingHandling.Size = new System.Drawing.Size(56, 20);
            this.txtShippingHandling.TabIndex = 62;
            this.txtShippingHandling.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtShippingHandling.OnEnterKeyPress += new PriceTextBox.PriceTextBox.EnterKeyPress(this.txtShippingHandling_OnEnterKeyPress);
            this.txtShippingHandling.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtShippingHandling_KeyPress);
            // 
            // txtDuty
            // 
            this.txtDuty.Location = new System.Drawing.Point(624, 8);
            this.txtDuty.Name = "txtDuty";
            this.txtDuty.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtDuty.Size = new System.Drawing.Size(56, 20);
            this.txtDuty.TabIndex = 64;
            this.txtDuty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtDuty.OnEnterKeyPress += new PriceTextBox.PriceTextBox.EnterKeyPress(this.txtDuty_OnEnterKeyPress);
            this.txtDuty.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtDuty_KeyPress);
            // 
            // ReceivedOrderLineContainer
            // 
            this.Controls.Add(this.lblPack);
            this.Controls.Add(this.lblTrademark);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.pnlOrderInformation);
            this.Controls.Add(this.pnlOrderLines);
            this.Name = "ReceivedOrderLineContainer";
            this.Size = new System.Drawing.Size(880, 720);
            this.Resize += new System.EventHandler(this.ReceivedOrderLineContainer_Resize);
            this.pnlOrderInformation.ResumeLayout(false);
            this.pnlOrderInformation.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		
		#region Properties
		//--------------------------------------------------------------------------------------------------------------------
		// Properties
		//--------------------------------------------------------------------------------------------------------------------
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

		public decimal Duty
		{
			set
			{
                this.txtDuty.Price = value;
			}
			get
			{
				return this.txtDuty.Price;
			}
		}

		public DateTime OrderDate
		{
			set
			{
				this.lblOrderDate_Data.Text = value.ToLongDateString();
			}
		}
        
        public bool ReadOnly
        {
            set
            {
                m_blnReadOnly = value;
                this.ReadOnlyChanged();
            }
            get
            {
                return m_blnReadOnly;
            }
        }
	
		public ReceivedOrderLine[] OrderLines
		{
			get
			{
				return m_rolOrderLines;
			}
		}

		public decimal ShippingHandling
		{
			set
			{
                this.txtShippingHandling.Price = value;
			}
			get
			{
                return this.txtShippingHandling.Price;
			}
		}

		public decimal Taxes
		{
			set
			{
                this.txtTaxes.Price = value;
			}
			get
			{
                return this.txtTaxes.Price;
			}
		}
		#endregion
		
		#region Methods
		//--------------------------------------------------------------------------------------------------------------------
		// Methods
		//--------------------------------------------------------------------------------------------------------------------
		/// <summary>
		///		Adds a new order line to the container.
		/// </summary>
		public void Add(ReceivedOrderLine rolNewOrderLine)
		{
			// Variable declaration
			int intNewOrderLineIndex, intNewOrderLineLocation_Y;
			ReceivedOrderLine[] rolTemp;
			
			rolNewOrderLine.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            rolNewOrderLine.ReadOnly = this.ReadOnly;

			if(m_rolOrderLines == null)
			{
				m_rolOrderLines = new ReceivedOrderLine[1];
				m_rolOrderLines[0] = rolNewOrderLine;

				rolNewOrderLine.Location = new System.Drawing.Point(0, 0);
				
				rolNewOrderLine.Width = this.pnlOrderLines.Width;
				this.pnlOrderLines.Controls.Add(rolNewOrderLine);
			}
			else
			{
				// increases storage array's capacity
				intNewOrderLineIndex = m_rolOrderLines.Length + 1;
				rolTemp = m_rolOrderLines;
				m_rolOrderLines = new ReceivedOrderLine[intNewOrderLineIndex];
				for(int i=0; i < rolTemp.Length; i++)
					m_rolOrderLines[i] = rolTemp[i];
				
				// add the new line
				m_rolOrderLines[intNewOrderLineIndex - 1] = rolNewOrderLine;

				// Draw the new received order line
				intNewOrderLineLocation_Y = rolTemp.Length*rolNewOrderLine.Size.Height + rolTemp.Length * m_intInterOrderLineSpacing;
				rolNewOrderLine.Location = new System.Drawing.Point(0,intNewOrderLineLocation_Y);
				rolNewOrderLine.Width = m_rolOrderLines[m_rolOrderLines.Length - 2].Width;
				this.pnlOrderLines.Controls.Add(rolNewOrderLine);
			}
		}
		/// <summary>
		///		Checks if all the order lines in the container have been checked. If so,
		///		it notifies the owner form.
		/// </summary>
		/*public void AllOrderLineCheckedCheck()
		{
			bool blnAllChecked = true;
			
			foreach(ReceivedOrderLine rolOrderLine in this.OrderLines)
			{
				if(!rolOrderLine.Checked)
				{
					blnAllChecked = false;
					break;
				}
			}

			if(blnAllChecked)
				((fclsOMCheckOrders) this.Parent).AllOrderLinesChecked(true);
			else
				((fclsOMCheckOrders) this.Parent).AllOrderLinesChecked(false);
		}*/

		/// <summary>
		///		Clears the container of all data.
		/// </summary>
		public void ClearAll()
		{
			this.pnlOrderLines.Controls.Clear();
			m_rolOrderLines = null;
			
			this.lblOrderDate_Data.Text = "";
			this.Duty = 0.0M;
			this.ShippingHandling = 0.0M;
			this.Taxes = 0.0M;
			this.ChangesMade = false;
		}

        /// <summary>
        ///		Sets the 'read-only' property of each order line in the container.
        /// </summary>
        private void ReadOnlyChanged()
        {
            if (m_rolOrderLines != null)
            {
                foreach (ReceivedOrderLine rolLine in m_rolOrderLines)
                    rolLine.ReadOnly = m_blnReadOnly;
            }

            this.txtDuty.Enabled = !m_blnReadOnly;
            this.txtShippingHandling.Enabled = !m_blnReadOnly;
            this.txtTaxes.Enabled = !m_blnReadOnly;
        }
		#endregion
		
		#region Events
		//--------------------------------------------------------------------------------------------------------------------
		// Events
		//--------------------------------------------------------------------------------------------------------------------
        private void txtTaxes_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            this.ChangesMade = true;
        }

        private void txtTaxes_OnEnterKeyPress()
        {
            this.pnlOrderLines.Focus();
        }

        private void txtShippingHandling_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            this.ChangesMade = true;
        }

        private void txtShippingHandling_OnEnterKeyPress()
        {
            this.pnlOrderLines.Focus();
        }

        private void txtDuty_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            this.ChangesMade = true;
        }

        private void txtDuty_OnEnterKeyPress()
        {
            this.pnlOrderLines.Focus();
        }

		private void ReceivedOrderLineContainer_Resize(object sender, System.EventArgs e)
		{
			this.lblProduct.Width = (int) (m_dblProductLabelProportion * this.Width);
			
			this.lblTrademark.Location = new Point(this.lblProduct.Location.X + this.lblProduct.Width + m_intLabelSpacing,this.lblTrademark.Location.Y);
			this.lblTrademark.Width = (int) (m_dblTrademarkLabelProportion * this.Width);
			
			this.lblPack.Location = new Point(this.lblTrademark.Location.X + this.lblTrademark.Width + m_intLabelSpacing,this.lblPack.Location.Y);
			this.lblPack.Width = (int) (m_dblPackagingLabelProportion * this.Width);
		}

		#endregion	
	}
}
