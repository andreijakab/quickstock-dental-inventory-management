using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Globalization;
using System.Windows.Forms;

namespace DSMS
{
	/// <summary>
	/// Summary description for OrderLine.
	/// </summary>
	public class OrderLine : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.ComboBox cmbUnits;
		public System.Windows.Forms.Label lblNumber;
		private System.Windows.Forms.Label lblProductName;
		private System.Windows.Forms.Label lblTradeMark;
		private System.Windows.Forms.Label lblPackaging;
		public System.Windows.Forms.Button btnRemove;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public event				OrderLineContainer.RemoveButtonClickHandler OnRemoveButtonClick;

		private decimal[]			m_decUnitPrices;
		private double				m_dblProductLabelProportion, m_dblTrademarkLabelProportion, m_dblPackagingLabelProportion, m_dblUnitsComboBoxProportion, m_dblRemoveButtonProportion, m_dblLabelSpacingProportion;
		private int					m_intTrademarkId = -1, m_intCategoryId = -1, m_intProductId = -1, m_intSubProductId = -1;
		private int					m_intLabelSpacing;
		private OrderLineContainer	m_olcOrder;
		private string				m_strComments, m_strGroupSeparator;

		public OrderLine(OrderLineContainer olcOrder)
		{
			NumberFormatInfo nfiNumberFormat;
			ToolTip toolTip1;

			InitializeComponent();
			
			// variable initialization
			m_dblProductLabelProportion = ((double) this.lblProductName.Width) /((double) this.Width);
			m_dblTrademarkLabelProportion = ((double)this.lblTradeMark.Width) / ((double) this.Width);
			m_dblPackagingLabelProportion = ((double)this.lblPackaging.Width) / ((double) this.Width);
			m_dblUnitsComboBoxProportion = ((double)this.cmbUnits.Width) / ((double) this.Width);
			m_dblRemoveButtonProportion = ((double)this.btnRemove.Width) / ((double) this.Width);
			m_dblLabelSpacingProportion = ((double)m_intLabelSpacing) / ((double) this.Width);
			m_decUnitPrices = new decimal[4];
			m_intLabelSpacing = 2;
			m_olcOrder = olcOrder;
			nfiNumberFormat = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
			toolTip1 = new ToolTip();
			
			// Get local number formatting information
			m_strGroupSeparator = nfiNumberFormat.NumberGroupSeparator;

			// Set up the delays for the ToolTip.
			toolTip1.AutoPopDelay = 5000;
			toolTip1.InitialDelay = 1000;
			toolTip1.ReshowDelay = 500;
			toolTip1.ShowAlways = true;			// Force the ToolTip text to be displayed whether or not the form is active.
      
			// Set up the ToolTip text for the Button and Checkbox.
			toolTip1.SetToolTip(this.btnRemove, "Click on this button to remove\nthis product from the order.");
			
			// initialize object
			this.Units = 1;
			this.UnitPrice1 = 0;
			this.UnitPrice2 = 0;
			this.UnitPrice3 = 0;
			this.UnitPrice4 = 0;
			this.Comments = "";
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
			this.cmbUnits = new System.Windows.Forms.ComboBox();
			this.lblNumber = new System.Windows.Forms.Label();
			this.btnRemove = new System.Windows.Forms.Button();
			this.lblProductName = new System.Windows.Forms.Label();
			this.lblTradeMark = new System.Windows.Forms.Label();
			this.lblPackaging = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// cmbUnits
			// 
			this.cmbUnits.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.cmbUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmbUnits.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(0)), ((System.Byte)(192)));
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
			this.cmbUnits.Location = new System.Drawing.Point(848, 0);
			this.cmbUnits.Name = "cmbUnits";
			this.cmbUnits.Size = new System.Drawing.Size(48, 21);
			this.cmbUnits.TabIndex = 10;
			this.cmbUnits.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUnits_KeyPress);
			this.cmbUnits.Leave += new System.EventHandler(this.cmbUnits_Leave);
			// 
			// lblNumber
			// 
			this.lblNumber.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.lblNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblNumber.Cursor = System.Windows.Forms.Cursors.Default;
			this.lblNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblNumber.ForeColor = System.Drawing.Color.Black;
			this.lblNumber.Location = new System.Drawing.Point(0, 0);
			this.lblNumber.Name = "lblNumber";
			this.lblNumber.Size = new System.Drawing.Size(24, 20);
			this.lblNumber.TabIndex = 1;
			this.lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnRemove
			// 
			this.btnRemove.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.btnRemove.Cursor = System.Windows.Forms.Cursors.Hand;
			this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.btnRemove.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.btnRemove.ForeColor = System.Drawing.Color.Red;
			this.btnRemove.Location = new System.Drawing.Point(896, 0);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(19, 19);
			this.btnRemove.TabIndex = 5;
			this.btnRemove.Text = "X";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// lblProductName
			// 
			this.lblProductName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left)));
			this.lblProductName.BackColor = System.Drawing.Color.White;
			this.lblProductName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblProductName.ForeColor = System.Drawing.Color.Black;
			this.lblProductName.Location = new System.Drawing.Point(24, 0);
			this.lblProductName.Name = "lblProductName";
			this.lblProductName.Size = new System.Drawing.Size(502, 20);
			this.lblProductName.TabIndex = 2;
			this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblProductName.UseMnemonic = false;
			// 
			// lblTradeMark
			// 
			this.lblTradeMark.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.lblTradeMark.BackColor = System.Drawing.Color.White;
			this.lblTradeMark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblTradeMark.ForeColor = System.Drawing.Color.Black;
			this.lblTradeMark.Location = new System.Drawing.Point(528, 0);
			this.lblTradeMark.Name = "lblTradeMark";
			this.lblTradeMark.Size = new System.Drawing.Size(146, 20);
			this.lblTradeMark.TabIndex = 3;
			this.lblTradeMark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblTradeMark.UseMnemonic = false;
			// 
			// lblPackaging
			// 
			this.lblPackaging.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
			this.lblPackaging.BackColor = System.Drawing.Color.White;
			this.lblPackaging.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.lblPackaging.ForeColor = System.Drawing.Color.Black;
			this.lblPackaging.Location = new System.Drawing.Point(676, 0);
			this.lblPackaging.Name = "lblPackaging";
			this.lblPackaging.Size = new System.Drawing.Size(168, 20);
			this.lblPackaging.TabIndex = 8;
			this.lblPackaging.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblPackaging.UseMnemonic = false;
			// 
			// OrderLine
			// 
			this.Controls.Add(this.lblPackaging);
			this.Controls.Add(this.lblTradeMark);
			this.Controls.Add(this.lblProductName);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.lblNumber);
			this.Controls.Add(this.cmbUnits);
			this.Name = "OrderLine";
			this.Size = new System.Drawing.Size(920, 21);
			this.Resize += new System.EventHandler(this.OrderLine_Resize);
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Events
		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			if(OnRemoveButtonClick != null)
			{
				clsRemoveOrderLineClickEventArgs ceaEventArgs = new clsRemoveOrderLineClickEventArgs(this.LineNumber - 1, this.lblProductName.Text);
				OnRemoveButtonClick(this.btnRemove,ceaEventArgs);
			}
		}

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

		private void OrderLine_Resize(object sender, System.EventArgs e)
		{
			double dblAdditionalSpace = m_dblLabelSpacingProportion/3;
            
			//if(this.Width == this.m_olcOrder.Width)
			{
				this.lblProductName.Width = (int) (m_dblProductLabelProportion * this.Width + dblAdditionalSpace);
			
				this.lblTradeMark.Location = new Point(this.lblProductName.Location.X + this.lblProductName.Width + m_intLabelSpacing,this.lblTradeMark.Location.Y);
				this.lblTradeMark.Width = (int) (m_dblTrademarkLabelProportion * this.Width + dblAdditionalSpace);
			
				this.lblPackaging.Location = new Point(this.lblTradeMark.Location.X + this.lblTradeMark.Width + m_intLabelSpacing,this.lblPackaging.Location.Y);
				this.lblPackaging.Width = (int) (m_dblPackagingLabelProportion * this.Width + dblAdditionalSpace);

				this.cmbUnits.Location = new Point(this.lblPackaging.Location.X + this.lblPackaging.Width + m_intLabelSpacing,this.cmbUnits.Location.Y);

				this.btnRemove.Location = new Point(this.cmbUnits.Location.X + this.cmbUnits.Width + m_intLabelSpacing,this.btnRemove.Location.Y);
				
				
				this.m_olcOrder.ResizeLabels(this.lblProductName.Width,this.lblProductName.Location.X,
					this.lblTradeMark.Width,this.lblTradeMark.Location.X,
					this.lblPackaging.Width,this.lblPackaging.Location.X,
					this.cmbUnits.Location.X);
			}
		}
		#endregion

        #region Properties
        public int LineNumber
		{
			set { this.lblNumber.Text = value.ToString(); } get { return int.Parse(this.lblNumber.Text);}
		}
		public string Product
		{
			set { this.lblProductName.Text = value; } get { return this.lblProductName.Text;}												  
		}

		public int CategoryId
		{
			set { m_intCategoryId = value; } get { return m_intCategoryId;}
		}

		public int ProductId
		{
			set { m_intProductId = value; } get { return m_intProductId;}
		}
        
		public int SubProductId
		{
			set { m_intSubProductId = value; } get { return m_intSubProductId;}
		}

		public string TradeMark
		{
			set { this.lblTradeMark.Text = value; } get { return this.lblTradeMark.Text;}												  
		}

		public int TradeMarkId
		{
			set	{ m_intTrademarkId = value;	} get { return m_intTrademarkId;}
		}

		public string Packaging
		{
			set { this.lblPackaging.Text = value; } get { return this.lblPackaging.Text;}												  
		}

		public int Units
		{
			set { cmbUnits.Text = value.ToString(); }get { return (int) decimal.Parse(cmbUnits.Text);}
		}

		public decimal UnitPrice1
		{
			set
			{
				m_decUnitPrices[0] = value;
			}
			get
			{
				return m_decUnitPrices[0];
			}
		}
		
		public decimal UnitPrice2
		{
			set
			{ 
				m_decUnitPrices[1] = value;
			}
			get
			{
				return m_decUnitPrices[1];
			}
		}
		
		public decimal UnitPrice3
		{
			set
			{ 
				m_decUnitPrices[2] = value;
			}
			get
			{
				return m_decUnitPrices[2];
			}
		}
		
		public decimal UnitPrice4
		{
			set
			{ 
				m_decUnitPrices[3] = value;
			}
			get
			{
				return m_decUnitPrices[3];
			}
		}

		public string Comments
		{
			set
			{
				m_strComments = value;
			}
			get
			{
				return m_strComments;
			}
		}
		#endregion
	}
}
