using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsDMModifyProduct_SubProd.
	/// </summary>
	public class fclsDMModifyProduct_Products : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblTrademark;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private DataTable m_dtaCategories;
		private OleDbDataAdapter m_odaCategories;
		private System.Windows.Forms.Label lblProductName;
		private System.Windows.Forms.TextBox txtProductName;
		private System.Windows.Forms.ComboBox cmbCategories;
		private System.Windows.Forms.Button btnOK;
		private OleDbConnection m_odcConnection;

		public fclsDMModifyProduct_Products(OleDbConnection odcConnection)
		{
			InitializeComponent();

			m_odcConnection = odcConnection;
			m_dtaCategories = new DataTable();
			m_odaCategories = new OleDbDataAdapter("SELECT * FROM Categories ORDER BY CategName",m_odcConnection);
			m_odaCategories.Fill(m_dtaCategories);
			for(int i=0; i < m_dtaCategories.Rows.Count; i++)
			{
				this.cmbCategories.Items.Add(m_dtaCategories.Rows[i]["CategName"].ToString());
			}
			this.txtProductName.Focus();

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
			this.lblTrademark = new System.Windows.Forms.Label();
			this.lblProductName = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOK = new System.Windows.Forms.Button();
			this.txtProductName = new System.Windows.Forms.TextBox();
			this.cmbCategories = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// lblTrademark
			// 
			this.lblTrademark.AutoSize = true;
			this.lblTrademark.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTrademark.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblTrademark.Location = new System.Drawing.Point(8, 40);
			this.lblTrademark.Name = "lblTrademark";
			this.lblTrademark.Size = new System.Drawing.Size(59, 20);
			this.lblTrademark.TabIndex = 56;
			this.lblTrademark.Text = "Category";
			this.lblTrademark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblProductName
			// 
			this.lblProductName.AutoSize = true;
			this.lblProductName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProductName.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblProductName.Location = new System.Drawing.Point(8, 8);
			this.lblProductName.Name = "lblProductName";
			this.lblProductName.Size = new System.Drawing.Size(91, 20);
			this.lblProductName.TabIndex = 54;
			this.lblProductName.Text = "Product Name";
			this.lblProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(176, 72);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 59;
			this.btnCancel.Text = "Cancel";
			// 
			// btnOK
			// 
			this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOK.Location = new System.Drawing.Point(72, 72);
			this.btnOK.Name = "btnOK";
			this.btnOK.TabIndex = 58;
			this.btnOK.Text = "OK";
			// 
			// txtProductName
			// 
			this.txtProductName.Location = new System.Drawing.Point(136, 8);
			this.txtProductName.Name = "txtProductName";
			this.txtProductName.Size = new System.Drawing.Size(184, 20);
			this.txtProductName.TabIndex = 60;
			this.txtProductName.Text = "";
			// 
			// cmbCategories
			// 
			this.cmbCategories.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbCategories.Location = new System.Drawing.Point(136, 40);
			this.cmbCategories.Name = "cmbCategories";
			this.cmbCategories.Size = new System.Drawing.Size(184, 21);
			this.cmbCategories.TabIndex = 61;
			// 
			// fclsDMModifyProduct_Products
			// 
			this.AcceptButton = this.btnOK;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(330, 104);
			this.Controls.Add(this.cmbCategories);
			this.Controls.Add(this.txtProductName);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOK);
			this.Controls.Add(this.lblTrademark);
			this.Controls.Add(this.lblProductName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "fclsDMModifyProduct_Products";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "fclsDMModifyProduct_SubProd";
			this.ResumeLayout(false);

		}
		#endregion
	
		public string[] ShowProductWindow()
		{
			this.Text = "Quick Stock - Add New Product";
			this.txtProductName.Text = "";
			this.cmbCategories.Enabled = true;

			string[] strNewProduct = new string[2];
			strNewProduct[0] = "";
			strNewProduct[1] = "0";
			if(this.ShowDialog() == DialogResult.OK)
			{
				if(this.txtProductName.Text.Length > 0)
				{
					strNewProduct[0] = this.txtProductName.Text;
					strNewProduct[1] = m_dtaCategories.Rows[this.cmbCategories.SelectedIndex]["CategoryId"].ToString();
				}
				else
				{
					MessageBox.Show("Please fill out all the fields!","Data Missing",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return this.ShowProductWindow();
				}
			}
			return strNewProduct;
		}

		public string ShowProductWindow(int intCategoryId)
		{
			this.Text = "Quick Stock - Add New Product";
			this.txtProductName.Text = "";
			for(int i=0; i < this.m_dtaCategories.Rows.Count; i++)
			{
				if(int.Parse(m_dtaCategories.Rows[i]["CategoryId"].ToString()) == intCategoryId)
					this.cmbCategories.SelectedIndex = i;
			}
			this.cmbCategories.Enabled = false;
			if(this.ShowDialog() == DialogResult.OK)
			{
				if(this.txtProductName.Text.Length > 0)
					return this.txtProductName.Text;
				else
				{
					MessageBox.Show("Please fill out the Product name!","Data Missing",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return this.ShowProductWindow(intCategoryId);
				}
			}
			return this.txtProductName.Text;
		}

		public string[] ShowProductWindow(string[] strDefaultText)
		{
			this.Text = "Quick Stock - Modify Product";
			System.Diagnostics.Debug.WriteLine(strDefaultText[0]);
			this.txtProductName.Text = strDefaultText[0];
			string[] strProduct = new string[2];
			strProduct[0] = "";
			strProduct[1] = "0";
			for(int i = 0; i< m_dtaCategories.Rows.Count; i++)
			{
				if(int.Parse(m_dtaCategories.Rows[i]["CategoryId"].ToString()) == int.Parse(strDefaultText[1]))
					this.cmbCategories.SelectedIndex = i;
			}
			if(this.ShowDialog() == DialogResult.OK)
			{
				strProduct[0] = this.txtProductName.Text;
				strProduct[1] = m_dtaCategories.Rows[this.cmbCategories.SelectedIndex]["CategoryId"].ToString();
			}
			return strProduct;
		}

		public string ShowProductWindow(string strDefaultText, int intCategoryId)
		{
			this.Text = "Quick Stock - Modify Product";
			this.txtProductName.Text = strDefaultText;
			for(int i=0; i < this.m_dtaCategories.Rows.Count; i++)
			{
				if(int.Parse(m_dtaCategories.Rows[i]["CategoryId"].ToString()) == intCategoryId)
					this.cmbCategories.SelectedIndex = i;
			}
			this.cmbCategories.Enabled = false;
			if(this.ShowDialog() == DialogResult.OK)
				return this.txtProductName.Text;
			else
				return strDefaultText;
		}

	}
}
