using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

namespace DSMS
{
	/// <summary>
	/// Summary description for OldOrderLine.
	/// </summary>
	public class OldOrderLine : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.TextBox txtProdName;
		private System.Windows.Forms.TextBox txtTradeMark;
		private System.Windows.Forms.TextBox txtPackaging;
		private System.Windows.Forms.TextBox txtPrice;
		private System.Windows.Forms.ComboBox cmbUnits;
		public System.Windows.Forms.Label lblNumber;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private int m_intTrademarkId = -1, m_intCategoryId = -1, m_intProductId = -1, m_intSubProductId = -1;

		public OldOrderLine()
		{
			InitializeComponent();

			ToolTip toolTip1 = new ToolTip();

			// Set up the delays for the ToolTip.
			toolTip1.AutoPopDelay = 5000;
			toolTip1.InitialDelay = 1000;
			toolTip1.ReshowDelay = 500;
			// Force the ToolTip text to be displayed whether or not the form is active.
			toolTip1.ShowAlways = true;
      
			// Set up the ToolTip text for the Button and Checkbox.
			toolTip1.SetToolTip(this.lblNumber, "Click on this label to remove this product from the order!!!");

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
			this.txtProdName = new System.Windows.Forms.TextBox();
			this.txtTradeMark = new System.Windows.Forms.TextBox();
			this.txtPackaging = new System.Windows.Forms.TextBox();
			this.cmbUnits = new System.Windows.Forms.ComboBox();
			this.lblNumber = new System.Windows.Forms.Label();
			this.txtPrice = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// txtProdName
			// 
			this.txtProdName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtProdName.ForeColor = System.Drawing.Color.Green;
			this.txtProdName.Location = new System.Drawing.Point(48, 0);
			this.txtProdName.Name = "txtProdName";
			this.txtProdName.Size = new System.Drawing.Size(454, 20);
			this.txtProdName.TabIndex = 0;
			this.txtProdName.Text = "";
			// 
			// txtTradeMark
			// 
			this.txtTradeMark.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtTradeMark.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(128)), ((System.Byte)(64)), ((System.Byte)(0)));
			this.txtTradeMark.Location = new System.Drawing.Point(502, 0);
			this.txtTradeMark.Name = "txtTradeMark";
			this.txtTradeMark.Size = new System.Drawing.Size(146, 20);
			this.txtTradeMark.TabIndex = 1;
			this.txtTradeMark.Text = "";
			// 
			// txtPackaging
			// 
			this.txtPackaging.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtPackaging.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.txtPackaging.Location = new System.Drawing.Point(648, 0);
			this.txtPackaging.Name = "txtPackaging";
			this.txtPackaging.Size = new System.Drawing.Size(168, 20);
			this.txtPackaging.TabIndex = 2;
			this.txtPackaging.Text = "";
			// 
			// cmbUnits
			// 
			this.cmbUnits.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.cmbUnits.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.cmbUnits.Items.AddRange(new object[] {
														  "1/4",
														  "1/2",
														  "  1",
														  "  2",
														  "  3",
														  "  4",
														  "  5",
														  "  6",
														  "  7",
														  "  8",
														  "  9",
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
			this.cmbUnits.Location = new System.Drawing.Point(816, 0);
			this.cmbUnits.Name = "cmbUnits";
			this.cmbUnits.Size = new System.Drawing.Size(48, 21);
			this.cmbUnits.TabIndex = 3;
			// 
			// lblNumber
			// 
			this.lblNumber.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblNumber.Cursor = System.Windows.Forms.Cursors.Hand;
			this.lblNumber.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblNumber.Location = new System.Drawing.Point(16, 0);
			this.lblNumber.Name = "lblNumber";
			this.lblNumber.Size = new System.Drawing.Size(24, 21);
			this.lblNumber.TabIndex = 4;
			this.lblNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtPrice
			// 
			this.txtPrice.Location = new System.Drawing.Point(864, 0);
			this.txtPrice.Name = "txtPrice";
			this.txtPrice.Size = new System.Drawing.Size(48, 20);
			this.txtPrice.TabIndex = 5;
			this.txtPrice.Text = "0.00";
			this.txtPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// OldOrderLine
			// 
			this.Controls.Add(this.txtPrice);
			this.Controls.Add(this.lblNumber);
			this.Controls.Add(this.cmbUnits);
			this.Controls.Add(this.txtPackaging);
			this.Controls.Add(this.txtTradeMark);
			this.Controls.Add(this.txtProdName);
			this.Name = "OldOrderLine";
			this.Size = new System.Drawing.Size(936, 21);
			this.Load += new System.EventHandler(this.OrderLine_Load);
			this.ResumeLayout(false);

		}
		#endregion

		public void OrderLine_Load(object sender, System.EventArgs e)
		{
			this.cmbUnits.SelectedIndex = 2;			
		}
	
		public string Number
		{
			set { lblNumber.Text = value; } get { return lblNumber.Text;}								  
		}
		public string ProdName
		{
			set { txtProdName.Text = value; } get { return txtProdName.Text;}												  
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
			set { txtTradeMark.Text = value; } get { return txtTradeMark.Text;}												  
		}

		public int TradeMarkId
		{
			set	{ m_intTrademarkId = value;	} get { return m_intTrademarkId;}
		}

		public string Packaging
		{
			set { txtPackaging.Text = value; } get { return txtPackaging.Text;}												  
		}

		public string Units
		{
			set { cmbUnits.Text = value; }
			get 
			{ 
				string retValue = cmbUnits.Text;
				if(cmbUnits.Text=="1/4")
					retValue = "0.25";
				 if(cmbUnits.Text=="1/2")
					retValue = "0.5";
				 return retValue;
			}
												  
		}
		public string UnitPrice
		{
			set { txtPrice.Text = value; }get { return txtPrice.Text;}												  
		}
	}
}
