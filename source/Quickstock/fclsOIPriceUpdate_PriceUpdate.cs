using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsSIPriceUpdate_PriceUpdate.
	/// </summary>
	public class fclsOIPriceUpdate_PriceUpdate : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		public System.Windows.Forms.Label lblCurrentPrice;
		public System.Windows.Forms.TextBox txtNewPrice;
		public System.Windows.Forms.Label lblProdName;
		private System.Windows.Forms.Button btnModify;
		private System.Windows.Forms.Button btnClose;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public fclsOIPriceUpdate_PriceUpdate()
		{
			InitializeComponent();

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
			this.btnModify = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.lblCurrentPrice = new System.Windows.Forms.Label();
			this.txtNewPrice = new System.Windows.Forms.TextBox();
			this.lblProdName = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// btnModify
			// 
			this.btnModify.Location = new System.Drawing.Point(184, 128);
			this.btnModify.Name = "btnModify";
			this.btnModify.Size = new System.Drawing.Size(120, 24);
			this.btnModify.TabIndex = 0;
			this.btnModify.Text = "Modify";
			this.btnModify.Click += new System.EventHandler(this.btnModify_Click);
			// 
			// btnClose
			// 
			this.btnClose.Location = new System.Drawing.Point(32, 128);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(120, 24);
			this.btnClose.TabIndex = 1;
			this.btnClose.Text = "Close without Modify";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(64, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 2;
			this.label1.Text = "Current Price       ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label2.Location = new System.Drawing.Point(64, 96);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 3;
			this.label2.Text = "Updated Price ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblCurrentPrice
			// 
			this.lblCurrentPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblCurrentPrice.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblCurrentPrice.ForeColor = System.Drawing.Color.Red;
			this.lblCurrentPrice.Location = new System.Drawing.Point(200, 56);
			this.lblCurrentPrice.Name = "lblCurrentPrice";
			this.lblCurrentPrice.Size = new System.Drawing.Size(72, 24);
			this.lblCurrentPrice.TabIndex = 4;
			this.lblCurrentPrice.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtNewPrice
			// 
			this.txtNewPrice.Location = new System.Drawing.Point(200, 88);
			this.txtNewPrice.Name = "txtNewPrice";
			this.txtNewPrice.Size = new System.Drawing.Size(72, 20);
			this.txtNewPrice.TabIndex = 5;
			this.txtNewPrice.Text = "";
			this.txtNewPrice.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			// 
			// lblProdName
			// 
			this.lblProdName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProdName.ForeColor = System.Drawing.Color.Green;
			this.lblProdName.Location = new System.Drawing.Point(16, 8);
			this.lblProdName.Name = "lblProdName";
			this.lblProdName.Size = new System.Drawing.Size(304, 40);
			this.lblProdName.TabIndex = 6;
			this.lblProdName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fclsSIPriceUpdate_PriceUpdate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(336, 166);
			this.Controls.Add(this.lblProdName);
			this.Controls.Add(this.txtNewPrice);
			this.Controls.Add(this.lblCurrentPrice);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.btnClose);
			this.Controls.Add(this.btnModify);
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "fclsSIPriceUpdate_PriceUpdate";
			this.Text = "Quick Stock - Update the Product Price";
			this.ResumeLayout(false);

		}
		#endregion

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void btnModify_Click(object sender, System.EventArgs e)
		{
			fclsOIViewOrders.SetNewPrice(1, this.txtNewPrice.Text.ToString());
			this.Close();
		
		}
	}
}
