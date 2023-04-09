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
	/// Summary description for OrderLine.
	/// </summary>
	public class ReceivedOrderLine : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Label lblUnitPrice;
		private System.Windows.Forms.ComboBox cmbUnitsReceived;
		private System.Windows.Forms.Label lblUnitsReceived;
		private System.Windows.Forms.Label lblUnitsOrdered;
		private System.Windows.Forms.ComboBox cmbUnitsToReturn;
		private System.Windows.Forms.Label lblUnitsToReturn;
		private System.Windows.Forms.Label lblBackorder_Data;
        private System.Windows.Forms.Label lblBackorder;
		private System.Windows.Forms.Label lblProductName;
		private System.Windows.Forms.Label lblTrademark;
		private System.Windows.Forms.Label lblPackaging;
		private System.Windows.Forms.Label lblLineNumber;
		private System.Windows.Forms.Panel pnlOwner;
		private System.Windows.Forms.Panel	pnlLine;
		private System.Windows.Forms.Label	lblUnitsOrdered_Data;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private enum PriceState:int {Price0, PriceNot0};

        private bool                        m_blnReadOnly;
		private double						m_dblProductLabelProportion, m_dblTrademarkLabelProportion, m_dblPackagingLabelProportion;
		private int							m_intLabelSpacing, m_intSubProductId;
		private ReceivedOrderLineContainer	m_rolcContainer;
		private string						m_strDecimalSeparator, m_strGroupSeparator;
		private System.Windows.Forms.Button btnAllUnitsReceived;
        private PriceTextBox.PriceTextBox txtUnitPrice;
		private ToolTip						m_ttToolTip;
		
		public ReceivedOrderLine(ReceivedOrderLineContainer rolcContainer)
		{
			NumberFormatInfo nfiNumberFormat;

			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Variable initialization
			m_intLabelSpacing = this.lblTrademark.Location.X - (this.lblProductName.Location.X + this.lblProductName.Size.Width);
			m_dblProductLabelProportion = ((double) this.lblProductName.Size.Width)/((double)this.pnlOwner.Size.Width);
			m_dblTrademarkLabelProportion = ((double) this.lblTrademark.Size.Width)/((double)this.pnlOwner.Size.Width);
			m_dblPackagingLabelProportion = ((double) this.lblPackaging.Size.Width)/((double)this.pnlOwner.Size.Width);
			nfiNumberFormat = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
			m_rolcContainer = rolcContainer;
			m_ttToolTip = new ToolTip();

			// Get local number formatting information
			m_strDecimalSeparator = nfiNumberFormat.CurrencyDecimalSeparator;
			m_strGroupSeparator = nfiNumberFormat.CurrencyGroupSeparator;

			// Configure tooltip properties
			m_ttToolTip.AutoPopDelay = 5000;
			m_ttToolTip.InitialDelay = 500;
			m_ttToolTip.ReshowDelay = 500;
			m_ttToolTip.ShowAlways = true;
			
			// Set up the ToolTip text for the Button and Checkbox.
			m_ttToolTip.SetToolTip(this.txtUnitPrice, "Update the unit price, if necessary");
			
			// Object initialization
            this.ReadOnly = false;
			this.LineNumber = 0;
			this.Product = "";
			this.Trademark = "";
			this.Packaging = "";
			this.UnitsOrdered = 0;
			this.UnitPrice = 0.0M;
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
            this.pnlOwner = new System.Windows.Forms.Panel();
            this.txtUnitPrice = new PriceTextBox.PriceTextBox();
            this.btnAllUnitsReceived = new System.Windows.Forms.Button();
            this.lblUnitsOrdered_Data = new System.Windows.Forms.Label();
            this.lblPackaging = new System.Windows.Forms.Label();
            this.lblTrademark = new System.Windows.Forms.Label();
            this.lblProductName = new System.Windows.Forms.Label();
            this.lblUnitPrice = new System.Windows.Forms.Label();
            this.lblBackorder_Data = new System.Windows.Forms.Label();
            this.lblBackorder = new System.Windows.Forms.Label();
            this.cmbUnitsReceived = new System.Windows.Forms.ComboBox();
            this.lblUnitsReceived = new System.Windows.Forms.Label();
            this.lblUnitsOrdered = new System.Windows.Forms.Label();
            this.cmbUnitsToReturn = new System.Windows.Forms.ComboBox();
            this.lblUnitsToReturn = new System.Windows.Forms.Label();
            this.pnlLine = new System.Windows.Forms.Panel();
            this.pnlOwner.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblLineNumber
            // 
            this.lblLineNumber.BackColor = System.Drawing.SystemColors.Window;
            this.lblLineNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblLineNumber.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblLineNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLineNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.lblLineNumber.Location = new System.Drawing.Point(4, 3);
            this.lblLineNumber.Name = "lblLineNumber";
            this.lblLineNumber.Size = new System.Drawing.Size(32, 40);
            this.lblLineNumber.TabIndex = 4;
            this.lblLineNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // pnlOwner
            // 
            this.pnlOwner.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlOwner.Controls.Add(this.txtUnitPrice);
            this.pnlOwner.Controls.Add(this.btnAllUnitsReceived);
            this.pnlOwner.Controls.Add(this.lblUnitsOrdered_Data);
            this.pnlOwner.Controls.Add(this.lblPackaging);
            this.pnlOwner.Controls.Add(this.lblTrademark);
            this.pnlOwner.Controls.Add(this.lblProductName);
            this.pnlOwner.Controls.Add(this.lblUnitPrice);
            this.pnlOwner.Controls.Add(this.lblBackorder_Data);
            this.pnlOwner.Controls.Add(this.lblBackorder);
            this.pnlOwner.Controls.Add(this.cmbUnitsReceived);
            this.pnlOwner.Controls.Add(this.lblUnitsReceived);
            this.pnlOwner.Controls.Add(this.lblUnitsOrdered);
            this.pnlOwner.Controls.Add(this.cmbUnitsToReturn);
            this.pnlOwner.Controls.Add(this.lblUnitsToReturn);
            this.pnlOwner.Controls.Add(this.lblLineNumber);
            this.pnlOwner.Location = new System.Drawing.Point(0, 4);
            this.pnlOwner.Name = "pnlOwner";
            this.pnlOwner.Size = new System.Drawing.Size(880, 50);
            this.pnlOwner.TabIndex = 68;
            this.pnlOwner.Resize += new System.EventHandler(this.pnlOwner_Resize);
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtUnitPrice.Location = new System.Drawing.Point(824, 23);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtUnitPrice.Size = new System.Drawing.Size(52, 20);
            this.txtUnitPrice.TabIndex = 71;
            this.txtUnitPrice.Text = "0,00";
            this.txtUnitPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtUnitPrice.OnEnterKeyPress += new PriceTextBox.PriceTextBox.EnterKeyPress(this.txtUnitPrice_OnEnterKeyPress);
            this.txtUnitPrice.Validated += new System.EventHandler(this.txtUnitPrice_Validated);
            this.txtUnitPrice.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtUnitPrice_KeyPress);
            // 
            // btnAllUnitsReceived
            // 
            this.btnAllUnitsReceived.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAllUnitsReceived.BackColor = System.Drawing.SystemColors.Control;
            this.btnAllUnitsReceived.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAllUnitsReceived.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAllUnitsReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAllUnitsReceived.ForeColor = System.Drawing.Color.ForestGreen;
            this.btnAllUnitsReceived.Location = new System.Drawing.Point(40, 24);
            this.btnAllUnitsReceived.Name = "btnAllUnitsReceived";
            this.btnAllUnitsReceived.Size = new System.Drawing.Size(112, 21);
            this.btnAllUnitsReceived.TabIndex = 87;
            this.btnAllUnitsReceived.Text = "All Units Received";
            this.btnAllUnitsReceived.UseVisualStyleBackColor = false;
            this.btnAllUnitsReceived.Click += new System.EventHandler(this.btnAllUnitsReceived_Click);
            // 
            // lblUnitsOrdered_Data
            // 
            this.lblUnitsOrdered_Data.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUnitsOrdered_Data.BackColor = System.Drawing.Color.White;
            this.lblUnitsOrdered_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblUnitsOrdered_Data.ForeColor = System.Drawing.Color.Black;
            this.lblUnitsOrdered_Data.Location = new System.Drawing.Point(256, 24);
            this.lblUnitsOrdered_Data.Name = "lblUnitsOrdered_Data";
            this.lblUnitsOrdered_Data.Size = new System.Drawing.Size(40, 21);
            this.lblUnitsOrdered_Data.TabIndex = 86;
            this.lblUnitsOrdered_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPackaging
            // 
            this.lblPackaging.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblPackaging.BackColor = System.Drawing.Color.White;
            this.lblPackaging.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblPackaging.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPackaging.ForeColor = System.Drawing.Color.Black;
            this.lblPackaging.Location = new System.Drawing.Point(668, 2);
            this.lblPackaging.Name = "lblPackaging";
            this.lblPackaging.Size = new System.Drawing.Size(208, 20);
            this.lblPackaging.TabIndex = 85;
            this.lblPackaging.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblPackaging.UseMnemonic = false;
            // 
            // lblTrademark
            // 
            this.lblTrademark.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblTrademark.BackColor = System.Drawing.Color.White;
            this.lblTrademark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblTrademark.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrademark.ForeColor = System.Drawing.Color.Black;
            this.lblTrademark.Location = new System.Drawing.Point(474, 2);
            this.lblTrademark.Name = "lblTrademark";
            this.lblTrademark.Size = new System.Drawing.Size(192, 20);
            this.lblTrademark.TabIndex = 84;
            this.lblTrademark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblTrademark.UseMnemonic = false;
            // 
            // lblProductName
            // 
            this.lblProductName.BackColor = System.Drawing.Color.White;
            this.lblProductName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProductName.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProductName.ForeColor = System.Drawing.Color.Black;
            this.lblProductName.Location = new System.Drawing.Point(40, 2);
            this.lblProductName.Name = "lblProductName";
            this.lblProductName.Size = new System.Drawing.Size(432, 20);
            this.lblProductName.TabIndex = 83;
            this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblProductName.UseMnemonic = false;
            // 
            // lblUnitPrice
            // 
            this.lblUnitPrice.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUnitPrice.AutoSize = true;
            this.lblUnitPrice.ForeColor = System.Drawing.Color.Black;
            this.lblUnitPrice.Location = new System.Drawing.Point(760, 26);
            this.lblUnitPrice.Name = "lblUnitPrice";
            this.lblUnitPrice.Size = new System.Drawing.Size(53, 13);
            this.lblUnitPrice.TabIndex = 80;
            this.lblUnitPrice.Text = "Unit Price";
            this.lblUnitPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBackorder_Data
            // 
            this.lblBackorder_Data.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBackorder_Data.BackColor = System.Drawing.Color.White;
            this.lblBackorder_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblBackorder_Data.ForeColor = System.Drawing.Color.Black;
            this.lblBackorder_Data.Location = new System.Drawing.Point(528, 24);
            this.lblBackorder_Data.Name = "lblBackorder_Data";
            this.lblBackorder_Data.Size = new System.Drawing.Size(40, 21);
            this.lblBackorder_Data.TabIndex = 78;
            this.lblBackorder_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblBackorder
            // 
            this.lblBackorder.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblBackorder.AutoSize = true;
            this.lblBackorder.ForeColor = System.Drawing.Color.Black;
            this.lblBackorder.Location = new System.Drawing.Point(472, 26);
            this.lblBackorder.Name = "lblBackorder";
            this.lblBackorder.Size = new System.Drawing.Size(56, 13);
            this.lblBackorder.TabIndex = 77;
            this.lblBackorder.Text = "Backorder";
            this.lblBackorder.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbUnitsReceived
            // 
            this.cmbUnitsReceived.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbUnitsReceived.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnitsReceived.Location = new System.Drawing.Point(392, 24);
            this.cmbUnitsReceived.Name = "cmbUnitsReceived";
            this.cmbUnitsReceived.Size = new System.Drawing.Size(56, 21);
            this.cmbUnitsReceived.TabIndex = 76;
            this.cmbUnitsReceived.SelectedIndexChanged += new System.EventHandler(this.cmbUnitsReceived_SelectedIndexChanged);
            this.cmbUnitsReceived.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUnitsReceived_KeyPress);
            // 
            // lblUnitsReceived
            // 
            this.lblUnitsReceived.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUnitsReceived.AutoSize = true;
            this.lblUnitsReceived.ForeColor = System.Drawing.Color.Black;
            this.lblUnitsReceived.Location = new System.Drawing.Point(312, 26);
            this.lblUnitsReceived.Name = "lblUnitsReceived";
            this.lblUnitsReceived.Size = new System.Drawing.Size(80, 13);
            this.lblUnitsReceived.TabIndex = 75;
            this.lblUnitsReceived.Text = "Units Received";
            this.lblUnitsReceived.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblUnitsOrdered
            // 
            this.lblUnitsOrdered.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUnitsOrdered.AutoSize = true;
            this.lblUnitsOrdered.ForeColor = System.Drawing.Color.Black;
            this.lblUnitsOrdered.Location = new System.Drawing.Point(184, 26);
            this.lblUnitsOrdered.Name = "lblUnitsOrdered";
            this.lblUnitsOrdered.Size = new System.Drawing.Size(72, 13);
            this.lblUnitsOrdered.TabIndex = 74;
            this.lblUnitsOrdered.Text = "Units Ordered";
            this.lblUnitsOrdered.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbUnitsToReturn
            // 
            this.cmbUnitsToReturn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cmbUnitsToReturn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbUnitsToReturn.Location = new System.Drawing.Point(672, 24);
            this.cmbUnitsToReturn.Name = "cmbUnitsToReturn";
            this.cmbUnitsToReturn.Size = new System.Drawing.Size(56, 21);
            this.cmbUnitsToReturn.TabIndex = 73;
            this.cmbUnitsToReturn.SelectedIndexChanged += new System.EventHandler(this.cmbUnitsToReturn_SelectedIndexChanged);
            this.cmbUnitsToReturn.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbUnitsToReturn_KeyPress);
            // 
            // lblUnitsToReturn
            // 
            this.lblUnitsToReturn.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblUnitsToReturn.AutoSize = true;
            this.lblUnitsToReturn.ForeColor = System.Drawing.Color.Black;
            this.lblUnitsToReturn.Location = new System.Drawing.Point(592, 26);
            this.lblUnitsToReturn.Name = "lblUnitsToReturn";
            this.lblUnitsToReturn.Size = new System.Drawing.Size(78, 13);
            this.lblUnitsToReturn.TabIndex = 72;
            this.lblUnitsToReturn.Text = "Units to Return";
            this.lblUnitsToReturn.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlLine
            // 
            this.pnlLine.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlLine.BackColor = System.Drawing.Color.Black;
            this.pnlLine.Location = new System.Drawing.Point(0, 0);
            this.pnlLine.Name = "pnlLine";
            this.pnlLine.Size = new System.Drawing.Size(880, 3);
            this.pnlLine.TabIndex = 69;
            // 
            // ReceivedOrderLine
            // 
            this.Controls.Add(this.pnlLine);
            this.Controls.Add(this.pnlOwner);
            this.Name = "ReceivedOrderLine";
            this.Size = new System.Drawing.Size(880, 52);
            this.pnlOwner.ResumeLayout(false);
            this.pnlOwner.PerformLayout();
            this.ResumeLayout(false);

		}
		#endregion
		
		#region Properties
		//--------------------------------------------------------------------------------------------------------------------
		// Properties
		//--------------------------------------------------------------------------------------------------------------------
		public int Backorder
		{
			set
			{
				this.lblBackorder_Data.Text = value.ToString();
			}
			get
			{
				return int.Parse(this.lblBackorder_Data.Text);
			}
		}
		
		public int LineNumber
		{
			set
			{
				this.lblLineNumber.Text = value.ToString();

				if(value == 1)
					this.pnlLine.Visible = false;
			}
			get
			{
				return int.Parse(this.lblLineNumber.Text);
			}
		}

		public string Packaging
		{
			set
			{
				this.lblPackaging.Text = value;
				m_ttToolTip.SetToolTip(this.lblPackaging,value);
			}
			get
			{
				return this.lblPackaging.Text;
			}
		}

		public string Product
		{
			set
			{
				this.lblProductName.Text = value;
				m_ttToolTip.SetToolTip(this.lblProductName,value);
			}
			get
			{
				return this.lblProductName.Text;
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

		public ReceivedOrderLineContainer OrderLineContainer
		{
			get
			{
				return m_rolcContainer;
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

		public string Trademark
		{
			set
			{
				this.lblTrademark.Text = value;
				m_ttToolTip.SetToolTip(this.lblTrademark,value);
			}
			get
			{
				return this.lblTrademark.Text;
			}
		}

		public int UnitsOrdered
		{
			set
			{
				this.lblUnitsOrdered_Data.Text = value.ToString();
				
				SetComboBoxItems(this.cmbUnitsReceived, value);
			}
			get
			{
				return int.Parse(this.lblUnitsOrdered_Data.Text);
			}
		}

		public decimal UnitPrice
		{
			set
			{
                this.txtUnitPrice.Price = value;

				if(value == 0)
					this.SetBackground(PriceState.Price0);
				else
					this.SetBackground(PriceState.PriceNot0);
			}
			get
			{
                return this.txtUnitPrice.Price;
			}
		}

		public int UnitsReceived
		{
			set
			{
				if(value <= this.cmbUnitsReceived.Items.Count)
					this.cmbUnitsReceived.SelectedIndex = value;
			}
			get
			{
				return int.Parse(this.cmbUnitsReceived.Text);
			}
		}

		public int UnitsToReturn
		{
			get
			{
				return int.Parse(this.cmbUnitsToReturn.Text);
			}
		}
		#endregion
		
		#region Methods
		//--------------------------------------------------------------------------------------------------------------------
		// Methods
		//--------------------------------------------------------------------------------------------------------------------
        private void ReadOnlyChanged()
        {
            this.btnAllUnitsReceived.Enabled = !m_blnReadOnly;
            this.cmbUnitsReceived.Enabled = !m_blnReadOnly;
            this.cmbUnitsToReturn.Enabled = !m_blnReadOnly;
            this.txtUnitPrice.Enabled = !m_blnReadOnly;
        }

        private void SetBackground(PriceState enuPriceState)
		{
			switch(enuPriceState)
			{
				case PriceState.Price0:
					// reddish
					this.BackColor = Color.FromArgb(255,160,122);
				break;

				case PriceState.PriceNot0:
					// light blue
					this.BackColor = Color.FromArgb(173,216,230);
				break;
			}
		}
		
		private void SetComboBoxItems(ComboBox cmbTarget, int intUpperBound)
		{
			// Clear items already present
			cmbTarget.Items.Clear();

			// Add new items
			for(int i=0; i < (intUpperBound + 1); i++)
				cmbTarget.Items.Add(i);

			// Select 0 as the default value
			cmbTarget.SelectedIndex = 0;
		}
		#endregion
		
		#region Events
		//--------------------------------------------------------------------------------------------------------------------
		// Events
		//--------------------------------------------------------------------------------------------------------------------
        private void txtUnitPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            this.OrderLineContainer.ChangesMade = true;
        }

        private void txtUnitPrice_OnEnterKeyPress()
        {
            this.pnlOwner.Focus();
        }

        private void txtUnitPrice_Validated(object sender, EventArgs e)
        {
            base.OnValidated(e);

            if (this.txtUnitPrice.Price == 0.0M)
                this.SetBackground(PriceState.Price0);
            else
                this.SetBackground(PriceState.PriceNot0);
        }

		private void cmbUnitsReceived_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			SetComboBoxItems(this.cmbUnitsToReturn, this.UnitsReceived);

			// Update backorder
			this.Backorder = this.UnitsOrdered - int.Parse(this.cmbUnitsReceived.Text);

			this.OrderLineContainer.ChangesMade = true;
		}

		private void pnlOwner_Resize(object sender, System.EventArgs e)
		{
			this.lblProductName.Width = (int) (m_dblProductLabelProportion * this.pnlOwner.Width);
			
			this.lblTrademark.Location = new Point(this.lblProductName.Location.X + this.lblProductName.Width + m_intLabelSpacing,this.lblTrademark.Location.Y);
			this.lblTrademark.Width = (int) (m_dblTrademarkLabelProportion * this.pnlOwner.Width);
			
			this.lblPackaging.Location = new Point(this.lblTrademark.Location.X + this.lblTrademark.Width + m_intLabelSpacing,this.lblPackaging.Location.Y);
			this.lblPackaging.Width = (int) (m_dblPackagingLabelProportion * this.pnlOwner.Width);
		}

		private void cmbUnitsToReturn_Enter(object sender, System.EventArgs e)
		{
			this.OrderLineContainer.ChangesMade = true;
		}

		private void btnAllUnitsReceived_Click(object sender, System.EventArgs e)
		{
			this.UnitsReceived = this.UnitsOrdered;

			this.OrderLineContainer.ChangesMade = true;
			this.txtUnitPrice.Select();
		}

		private void cmbUnitsToReturn_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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

			this.OrderLineContainer.ChangesMade = true;
		}

		private void cmbUnitsReceived_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
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

			this.OrderLineContainer.ChangesMade = true;
		}

		private void cmbUnitsToReturn_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			this.OrderLineContainer.ChangesMade = true;
		}
		#endregion
    }
}
