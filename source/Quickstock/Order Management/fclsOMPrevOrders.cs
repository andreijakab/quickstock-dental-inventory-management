using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsPrevOrders.
	/// </summary>
	public class fclsPrevOrders : System.Windows.Forms.Form
	{
        private System.Windows.Forms.ColumnHeader orderDate;
        private System.Windows.Forms.ColumnHeader orderSupl;
        private System.Windows.Forms.ColumnHeader orderPrice;
        public System.Windows.Forms.ListView lstViewOrder;
        private System.Windows.Forms.Label lblCurrency;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public fclsPrevOrders(System.Windows.Forms.Form frmOwner)
		{
            NumberFormatInfo nfiNumberFormat;

			InitializeComponent();
            nfiNumberFormat = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            this.Location = new Point(frmOwner.Location.X + 5, frmOwner.Location.Y + 20);
            this.lblCurrency.Text += "'" + nfiNumberFormat.CurrencySymbol + "'.";
            
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
            this.lstViewOrder = new System.Windows.Forms.ListView();
            this.orderDate = new System.Windows.Forms.ColumnHeader();
            this.orderSupl = new System.Windows.Forms.ColumnHeader();
            this.orderPrice = new System.Windows.Forms.ColumnHeader();
            this.lblCurrency = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lstViewOrder
            // 
            this.lstViewOrder.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.orderDate,
            this.orderSupl,
            this.orderPrice});
            this.lstViewOrder.Location = new System.Drawing.Point(24, 8);
            this.lstViewOrder.Name = "lstViewOrder";
            this.lstViewOrder.Size = new System.Drawing.Size(320, 104);
            this.lstViewOrder.TabIndex = 9;
            this.lstViewOrder.View = System.Windows.Forms.View.Details;
            // 
            // orderDate
            // 
            this.orderDate.Text = "Date";
            this.orderDate.Width = 80;
            // 
            // orderSupl
            // 
            this.orderSupl.Text = "Supplier";
            this.orderSupl.Width = 176;
            // 
            // orderPrice
            // 
            this.orderPrice.Text = "Unit Price";
            this.orderPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lblCurrency
            // 
            this.lblCurrency.AutoSize = true;
            this.lblCurrency.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrency.ForeColor = System.Drawing.Color.Black;
            this.lblCurrency.Location = new System.Drawing.Point(21, 115);
            this.lblCurrency.Name = "lblCurrency";
            this.lblCurrency.Size = new System.Drawing.Size(111, 13);
            this.lblCurrency.TabIndex = 10;
            this.lblCurrency.Text = "Note: all prices are in ";
            this.lblCurrency.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // fclsPrevOrders
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(368, 133);
            this.Controls.Add(this.lblCurrency);
            this.Controls.Add(this.lstViewOrder);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fclsPrevOrders";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "Quick Stock - Prices from previous Orders";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
	}
}
