using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsSMDBPrices.
	/// </summary>
	public class fclsOMDBPrices : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label14;
		private System.Windows.Forms.Label label15;
		public System.Windows.Forms.Label lblMinSupplier;
		public System.Windows.Forms.Label lblLastSupplier;
		public System.Windows.Forms.Label lblMaxSupplier;
		public System.Windows.Forms.Label lblMaxPack;
		public System.Windows.Forms.Label lblLastPack;
		public System.Windows.Forms.Label lblMinPack;
		public System.Windows.Forms.Label lblMaxPrice;
		public System.Windows.Forms.Label lblLastPrice;
		public System.Windows.Forms.Label lblMinPrice;
		public System.Windows.Forms.Label lblProdName;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public fclsOMDBPrices()
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
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblMinSupplier = new System.Windows.Forms.Label();
			this.lblLastSupplier = new System.Windows.Forms.Label();
			this.lblMaxSupplier = new System.Windows.Forms.Label();
			this.lblMaxPack = new System.Windows.Forms.Label();
			this.lblLastPack = new System.Windows.Forms.Label();
			this.lblMinPack = new System.Windows.Forms.Label();
			this.lblMaxPrice = new System.Windows.Forms.Label();
			this.lblLastPrice = new System.Windows.Forms.Label();
			this.lblMinPrice = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.lblProdName = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Blue;
			this.label1.Location = new System.Drawing.Point(8, 96);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(88, 16);
			this.label1.TabIndex = 0;
			this.label1.Text = "Supplier";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Blue;
			this.label2.Location = new System.Drawing.Point(8, 144);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(88, 16);
			this.label2.TabIndex = 1;
			this.label2.Text = "Price";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Blue;
			this.label3.Location = new System.Drawing.Point(8, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(88, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "Packaging";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblMinSupplier
			// 
			this.lblMinSupplier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblMinSupplier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMinSupplier.ForeColor = System.Drawing.Color.Blue;
			this.lblMinSupplier.Location = new System.Drawing.Point(104, 88);
			this.lblMinSupplier.Name = "lblMinSupplier";
			this.lblMinSupplier.Size = new System.Drawing.Size(184, 24);
			this.lblMinSupplier.TabIndex = 3;
			this.lblMinSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLastSupplier
			// 
			this.lblLastSupplier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLastSupplier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLastSupplier.ForeColor = System.Drawing.Color.Blue;
			this.lblLastSupplier.Location = new System.Drawing.Point(288, 88);
			this.lblLastSupplier.Name = "lblLastSupplier";
			this.lblLastSupplier.Size = new System.Drawing.Size(184, 24);
			this.lblLastSupplier.TabIndex = 4;
			this.lblLastSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMaxSupplier
			// 
			this.lblMaxSupplier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblMaxSupplier.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMaxSupplier.ForeColor = System.Drawing.Color.Blue;
			this.lblMaxSupplier.Location = new System.Drawing.Point(472, 88);
			this.lblMaxSupplier.Name = "lblMaxSupplier";
			this.lblMaxSupplier.Size = new System.Drawing.Size(184, 24);
			this.lblMaxSupplier.TabIndex = 5;
			this.lblMaxSupplier.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMaxPack
			// 
			this.lblMaxPack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblMaxPack.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMaxPack.ForeColor = System.Drawing.Color.Blue;
			this.lblMaxPack.Location = new System.Drawing.Point(472, 112);
			this.lblMaxPack.Name = "lblMaxPack";
			this.lblMaxPack.Size = new System.Drawing.Size(184, 24);
			this.lblMaxPack.TabIndex = 8;
			this.lblMaxPack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLastPack
			// 
			this.lblLastPack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLastPack.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLastPack.ForeColor = System.Drawing.Color.Blue;
			this.lblLastPack.Location = new System.Drawing.Point(288, 112);
			this.lblLastPack.Name = "lblLastPack";
			this.lblLastPack.Size = new System.Drawing.Size(184, 24);
			this.lblLastPack.TabIndex = 7;
			this.lblLastPack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMinPack
			// 
			this.lblMinPack.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblMinPack.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMinPack.ForeColor = System.Drawing.Color.Blue;
			this.lblMinPack.Location = new System.Drawing.Point(104, 112);
			this.lblMinPack.Name = "lblMinPack";
			this.lblMinPack.Size = new System.Drawing.Size(184, 24);
			this.lblMinPack.TabIndex = 6;
			this.lblMinPack.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMaxPrice
			// 
			this.lblMaxPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblMaxPrice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMaxPrice.ForeColor = System.Drawing.Color.Blue;
			this.lblMaxPrice.Location = new System.Drawing.Point(472, 136);
			this.lblMaxPrice.Name = "lblMaxPrice";
			this.lblMaxPrice.Size = new System.Drawing.Size(184, 24);
			this.lblMaxPrice.TabIndex = 11;
			this.lblMaxPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblLastPrice
			// 
			this.lblLastPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLastPrice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLastPrice.ForeColor = System.Drawing.Color.Blue;
			this.lblLastPrice.Location = new System.Drawing.Point(288, 136);
			this.lblLastPrice.Name = "lblLastPrice";
			this.lblLastPrice.Size = new System.Drawing.Size(184, 24);
			this.lblLastPrice.TabIndex = 10;
			this.lblLastPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblMinPrice
			// 
			this.lblMinPrice.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblMinPrice.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblMinPrice.ForeColor = System.Drawing.Color.Blue;
			this.lblMinPrice.Location = new System.Drawing.Point(104, 136);
			this.lblMinPrice.Name = "lblMinPrice";
			this.lblMinPrice.Size = new System.Drawing.Size(184, 24);
			this.lblMinPrice.TabIndex = 9;
			this.lblMinPrice.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label13.ForeColor = System.Drawing.Color.Blue;
			this.label13.Location = new System.Drawing.Point(112, 64);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(88, 16);
			this.label13.TabIndex = 12;
			this.label13.Text = "Minim";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label14
			// 
			this.label14.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label14.ForeColor = System.Drawing.Color.Blue;
			this.label14.Location = new System.Drawing.Point(480, 64);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(88, 16);
			this.label14.TabIndex = 13;
			this.label14.Text = "Maxim";
			this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label15.ForeColor = System.Drawing.Color.Blue;
			this.label15.Location = new System.Drawing.Point(296, 64);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(88, 16);
			this.label15.TabIndex = 14;
			this.label15.Text = "Last Order";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblProdName
			// 
			this.lblProdName.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblProdName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProdName.ForeColor = System.Drawing.Color.Red;
			this.lblProdName.Location = new System.Drawing.Point(8, 8);
			this.lblProdName.Name = "lblProdName";
			this.lblProdName.Size = new System.Drawing.Size(656, 48);
			this.lblProdName.TabIndex = 15;
			this.lblProdName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// fclsSMDBPrices
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(672, 174);
			this.Controls.Add(this.lblProdName);
			this.Controls.Add(this.label15);
			this.Controls.Add(this.label14);
			this.Controls.Add(this.label13);
			this.Controls.Add(this.lblMaxPrice);
			this.Controls.Add(this.lblLastPrice);
			this.Controls.Add(this.lblMinPrice);
			this.Controls.Add(this.lblMaxPack);
			this.Controls.Add(this.lblLastPack);
			this.Controls.Add(this.lblMinPack);
			this.Controls.Add(this.lblMaxSupplier);
			this.Controls.Add(this.lblLastSupplier);
			this.Controls.Add(this.lblMinSupplier);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Name = "fclsSMDBPrices";
			this.Text = "Quick Stock - Price Info";
			this.Load += new System.EventHandler(this.fclsOMDBPrices_Load);
			this.ResumeLayout(false);

		}
		#endregion

		private void fclsOMDBPrices_Load(object sender, System.EventArgs e)
		{
		
		}

	}
}
