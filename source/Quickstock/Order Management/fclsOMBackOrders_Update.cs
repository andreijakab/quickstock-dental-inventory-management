using System;
using System.Collections;
using System.ComponentModel;
using System.Data.OleDb;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsSMBackOrders_Update.
	/// </summary>
	public class fclsOMBackOrders_Update : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Label lblProduct_Data;
		private System.Windows.Forms.Label lblProduct;
        private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.ComboBox cmbNUnitsReceived;
		private System.Windows.Forms.Label lblNUnitsReceived;
		private System.Windows.Forms.Label lblNUnitsBackordered;
		private System.Windows.Forms.Label lblNUnitsBackordered_Data;
		private System.Windows.Forms.Label lblUpdatedUnitPrice;
		private System.Windows.Forms.Label lblCurrentUnitPrice_Data;
		private System.Windows.Forms.Label lblCurrentUnitPrice;
        private System.Windows.Forms.Button btnOk;
        private PriceTextBox.PriceTextBox txtUnitPrice;
        private Label lblOrderNr;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

        private clsBackorderListViewItem    m_blviProduct;
		private fclsOMBackOrders		    m_frmOwner;
                
        public fclsOMBackOrders_Update(fclsOMBackOrders frmOwner, string strOrderID, clsBackorderListViewItem blviProduct)
		{
            CultureInfo CurrentCulture;
			NumberFormatInfo nfiNumberFormat;
			
			InitializeComponent();

            m_blviProduct = blviProduct;
            m_frmOwner = frmOwner;

            // Get local number formatting information
            CurrentCulture = (CultureInfo) System.Globalization.CultureInfo.CurrentCulture.Clone();
            nfiNumberFormat = CurrentCulture.NumberFormat;
            nfiNumberFormat.CurrencySymbol = "";

            this.lblOrderNr.Text = "Order Nr. " + strOrderID;
            this.lblProduct_Data.Text = blviProduct.ProductName;
            this.lblNUnitsBackordered_Data.Text = blviProduct.NUnitsBackordered.ToString();
            this.lblCurrentUnitPrice_Data.Text = blviProduct.UnitPrice.ToString("C", nfiNumberFormat);
            this.txtUnitPrice.Price = blviProduct.UnitPrice;

			// initialize combo-box
            for (int i = 0; i <= blviProduct.NUnitsBackordered; i++)
				this.cmbNUnitsReceived.Items.Add(i.ToString());
            this.cmbNUnitsReceived.SelectedIndex = blviProduct.NUnitsReceived;
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
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.cmbNUnitsReceived = new System.Windows.Forms.ComboBox();
            this.lblNUnitsReceived = new System.Windows.Forms.Label();
            this.lblProduct_Data = new System.Windows.Forms.Label();
            this.lblProduct = new System.Windows.Forms.Label();
            this.lblNUnitsBackordered = new System.Windows.Forms.Label();
            this.lblNUnitsBackordered_Data = new System.Windows.Forms.Label();
            this.lblCurrentUnitPrice_Data = new System.Windows.Forms.Label();
            this.lblCurrentUnitPrice = new System.Windows.Forms.Label();
            this.lblUpdatedUnitPrice = new System.Windows.Forms.Label();
            this.txtUnitPrice = new PriceTextBox.PriceTextBox();
            this.lblOrderNr = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(216, 243);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(88, 24);
            this.btnOk.TabIndex = 0;
            this.btnOk.Text = "Ok";
            this.btnOk.Click += new System.EventHandler(this.btnSaveClose_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(312, 243);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(88, 24);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // cmbNUnitsReceived
            // 
            this.cmbNUnitsReceived.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbNUnitsReceived.Location = new System.Drawing.Point(152, 145);
            this.cmbNUnitsReceived.Name = "cmbNUnitsReceived";
            this.cmbNUnitsReceived.Size = new System.Drawing.Size(96, 21);
            this.cmbNUnitsReceived.TabIndex = 9;
            // 
            // lblNUnitsReceived
            // 
            this.lblNUnitsReceived.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblNUnitsReceived.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblNUnitsReceived.Location = new System.Drawing.Point(8, 147);
            this.lblNUnitsReceived.Name = "lblNUnitsReceived";
            this.lblNUnitsReceived.Size = new System.Drawing.Size(136, 16);
            this.lblNUnitsReceived.TabIndex = 10;
            this.lblNUnitsReceived.Text = "# Units Received";
            this.lblNUnitsReceived.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblProduct_Data
            // 
            this.lblProduct_Data.BackColor = System.Drawing.Color.White;
            this.lblProduct_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblProduct_Data.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblProduct_Data.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblProduct_Data.Location = new System.Drawing.Point(152, 50);
            this.lblProduct_Data.Name = "lblProduct_Data";
            this.lblProduct_Data.Size = new System.Drawing.Size(248, 54);
            this.lblProduct_Data.TabIndex = 15;
            // 
            // lblProduct
            // 
            this.lblProduct.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProduct.ForeColor = System.Drawing.Color.Red;
            this.lblProduct.Location = new System.Drawing.Point(64, 50);
            this.lblProduct.Name = "lblProduct";
            this.lblProduct.Size = new System.Drawing.Size(80, 16);
            this.lblProduct.TabIndex = 14;
            this.lblProduct.Text = "Product";
            this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNUnitsBackordered
            // 
            this.lblNUnitsBackordered.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNUnitsBackordered.ForeColor = System.Drawing.Color.Red;
            this.lblNUnitsBackordered.Location = new System.Drawing.Point(8, 115);
            this.lblNUnitsBackordered.Name = "lblNUnitsBackordered";
            this.lblNUnitsBackordered.Size = new System.Drawing.Size(136, 16);
            this.lblNUnitsBackordered.TabIndex = 16;
            this.lblNUnitsBackordered.Text = "# Units BackorderedProduct";
            this.lblNUnitsBackordered.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblNUnitsBackordered_Data
            // 
            this.lblNUnitsBackordered_Data.BackColor = System.Drawing.Color.White;
            this.lblNUnitsBackordered_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblNUnitsBackordered_Data.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblNUnitsBackordered_Data.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblNUnitsBackordered_Data.Location = new System.Drawing.Point(152, 113);
            this.lblNUnitsBackordered_Data.Name = "lblNUnitsBackordered_Data";
            this.lblNUnitsBackordered_Data.Size = new System.Drawing.Size(96, 21);
            this.lblNUnitsBackordered_Data.TabIndex = 17;
            this.lblNUnitsBackordered_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentUnitPrice_Data
            // 
            this.lblCurrentUnitPrice_Data.BackColor = System.Drawing.Color.White;
            this.lblCurrentUnitPrice_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblCurrentUnitPrice_Data.Font = new System.Drawing.Font("Arial", 9.75F);
            this.lblCurrentUnitPrice_Data.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblCurrentUnitPrice_Data.Location = new System.Drawing.Point(152, 177);
            this.lblCurrentUnitPrice_Data.Name = "lblCurrentUnitPrice_Data";
            this.lblCurrentUnitPrice_Data.Size = new System.Drawing.Size(96, 21);
            this.lblCurrentUnitPrice_Data.TabIndex = 19;
            this.lblCurrentUnitPrice_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCurrentUnitPrice
            // 
            this.lblCurrentUnitPrice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentUnitPrice.ForeColor = System.Drawing.Color.Red;
            this.lblCurrentUnitPrice.Location = new System.Drawing.Point(8, 179);
            this.lblCurrentUnitPrice.Name = "lblCurrentUnitPrice";
            this.lblCurrentUnitPrice.Size = new System.Drawing.Size(136, 16);
            this.lblCurrentUnitPrice.TabIndex = 18;
            this.lblCurrentUnitPrice.Text = "Current Unit Price";
            this.lblCurrentUnitPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblUpdatedUnitPrice
            // 
            this.lblUpdatedUnitPrice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblUpdatedUnitPrice.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblUpdatedUnitPrice.Location = new System.Drawing.Point(8, 211);
            this.lblUpdatedUnitPrice.Name = "lblUpdatedUnitPrice";
            this.lblUpdatedUnitPrice.Size = new System.Drawing.Size(136, 16);
            this.lblUpdatedUnitPrice.TabIndex = 20;
            this.lblUpdatedUnitPrice.Text = "Updated Unit Price";
            this.lblUpdatedUnitPrice.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUnitPrice
            // 
            this.txtUnitPrice.Location = new System.Drawing.Point(152, 207);
            this.txtUnitPrice.Name = "txtUnitPrice";
            this.txtUnitPrice.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtUnitPrice.Size = new System.Drawing.Size(96, 20);
            this.txtUnitPrice.TabIndex = 21;
            this.txtUnitPrice.Text = "0,00 ";
            // 
            // lblOrderNr
            // 
            this.lblOrderNr.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNr.ForeColor = System.Drawing.Color.Red;
            this.lblOrderNr.Location = new System.Drawing.Point(11, 8);
            this.lblOrderNr.Name = "lblOrderNr";
            this.lblOrderNr.Size = new System.Drawing.Size(389, 32);
            this.lblOrderNr.TabIndex = 22;
            this.lblOrderNr.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // fclsOMBackOrders_Update
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(410, 271);
            this.Controls.Add(this.lblOrderNr);
            this.Controls.Add(this.txtUnitPrice);
            this.Controls.Add(this.lblUpdatedUnitPrice);
            this.Controls.Add(this.lblCurrentUnitPrice_Data);
            this.Controls.Add(this.lblCurrentUnitPrice);
            this.Controls.Add(this.lblNUnitsBackordered_Data);
            this.Controls.Add(this.lblNUnitsBackordered);
            this.Controls.Add(this.lblProduct_Data);
            this.Controls.Add(this.lblProduct);
            this.Controls.Add(this.lblNUnitsReceived);
            this.Controls.Add(this.cmbNUnitsReceived);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fclsOMBackOrders_Update";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quick Stock - Backordered Product Update";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion
		
		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnSaveClose_Click(object sender, System.EventArgs e)
		{
            if (cmbNUnitsReceived.SelectedIndex > -1)
            {
                // save changes
                m_blviProduct.LastChanged = DateTime.Now;
                m_blviProduct.NUnitsReceived = cmbNUnitsReceived.SelectedIndex;
                m_blviProduct.NUnitsBackordered -= m_blviProduct.NUnitsReceived;
                m_blviProduct.UnitPrice = this.txtUnitPrice.Price;
                m_blviProduct.State = clsBackorderListViewItem.ChangeState.Updated;

                // inform parent form that data was changed
                m_frmOwner.UtilityFormChangedData();
                
                this.Close();
            }
		}
	}
}
