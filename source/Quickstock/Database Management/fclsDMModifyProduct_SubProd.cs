using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsDMModifyProduct_SubProd.
	/// </summary>
	public class fclsDMModifyProduct_SubProd : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblTrademark;
		private System.Windows.Forms.Label lblPackaging;
		private System.Windows.Forms.Label lblSubProductName;
		private System.Windows.Forms.Label lblReorderingLevel;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.TextBox txtSubProductName;
		private System.Windows.Forms.TextBox txtPackaging;
		private System.Windows.Forms.ComboBox cmbReorderingLevel;
		private System.Windows.Forms.ListBox lbxTrademarks;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		public enum WindowPurpose:int {Add, Modify};
		
		private DataTable m_dtaSubProducts, m_dtaTrademarks;
		private OleDbDataAdapter m_odaTrademarks;
		private OleDbConnection m_odcConnection;
		private WindowPurpose m_wpPurpose;

		public fclsDMModifyProduct_SubProd(OleDbConnection odcConnection, DataTable dtaSubProducts, WindowPurpose wpPurpose)
		{
			InitializeComponent();
			
			m_odcConnection = odcConnection;
			m_dtaSubProducts = dtaSubProducts;
			m_wpPurpose = wpPurpose;
			
			// set default reordering level
			this.cmbReorderingLevel.SelectedIndex = 0;
			
			// load trademarks
			m_dtaTrademarks = new DataTable();
			m_odaTrademarks = new OleDbDataAdapter("SELECT * FROM Trademarks ORDER BY Trademark",m_odcConnection);
			m_odaTrademarks.Fill(m_dtaTrademarks);
			foreach(DataRow dtrRow in m_dtaTrademarks.Rows)
				this.lbxTrademarks.Items.Add(dtrRow["Trademark"].ToString());

			// customize window depending on its purpose
			switch(m_wpPurpose)
			{
				case WindowPurpose.Add:
					this.Text = "Add New Sub-Product";
				break;
				
				case WindowPurpose.Modify:
					this.Text = "Modify Sub-Product";
				break;
			}
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
			this.lblPackaging = new System.Windows.Forms.Label();
			this.lblSubProductName = new System.Windows.Forms.Label();
			this.lblReorderingLevel = new System.Windows.Forms.Label();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.txtSubProductName = new System.Windows.Forms.TextBox();
			this.txtPackaging = new System.Windows.Forms.TextBox();
			this.cmbReorderingLevel = new System.Windows.Forms.ComboBox();
			this.lbxTrademarks = new System.Windows.Forms.ListBox();
			this.SuspendLayout();
			// 
			// lblTrademark
			// 
			this.lblTrademark.AutoSize = true;
			this.lblTrademark.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTrademark.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblTrademark.Location = new System.Drawing.Point(8, 69);
			this.lblTrademark.Name = "lblTrademark";
			this.lblTrademark.Size = new System.Drawing.Size(70, 20);
			this.lblTrademark.TabIndex = 56;
			this.lblTrademark.Text = "Trademark";
			this.lblTrademark.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblPackaging
			// 
			this.lblPackaging.AutoSize = true;
			this.lblPackaging.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPackaging.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblPackaging.Location = new System.Drawing.Point(8, 132);
			this.lblPackaging.Name = "lblPackaging";
			this.lblPackaging.Size = new System.Drawing.Size(66, 20);
			this.lblPackaging.TabIndex = 55;
			this.lblPackaging.Text = "Packaging";
			this.lblPackaging.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblSubProductName
			// 
			this.lblSubProductName.AutoSize = true;
			this.lblSubProductName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblSubProductName.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblSubProductName.Location = new System.Drawing.Point(8, 8);
			this.lblSubProductName.Name = "lblSubProductName";
			this.lblSubProductName.Size = new System.Drawing.Size(118, 20);
			this.lblSubProductName.TabIndex = 54;
			this.lblSubProductName.Text = "Sub-Product Name";
			this.lblSubProductName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblReorderingLevel
			// 
			this.lblReorderingLevel.AutoSize = true;
			this.lblReorderingLevel.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblReorderingLevel.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblReorderingLevel.Location = new System.Drawing.Point(8, 156);
			this.lblReorderingLevel.Name = "lblReorderingLevel";
			this.lblReorderingLevel.Size = new System.Drawing.Size(107, 20);
			this.lblReorderingLevel.TabIndex = 57;
			this.lblReorderingLevel.Text = "Reordering Level";
			this.lblReorderingLevel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(216, 192);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.TabIndex = 59;
			this.btnCancel.Text = "Cancel";
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(40, 192);
			this.btnOk.Name = "btnOk";
			this.btnOk.TabIndex = 58;
			this.btnOk.Text = "Ok";
			// 
			// txtSubProductName
			// 
			this.txtSubProductName.Location = new System.Drawing.Point(136, 8);
			this.txtSubProductName.Name = "txtSubProductName";
			this.txtSubProductName.Size = new System.Drawing.Size(184, 20);
			this.txtSubProductName.TabIndex = 60;
			this.txtSubProductName.Text = "";
			// 
			// txtPackaging
			// 
			this.txtPackaging.Location = new System.Drawing.Point(136, 132);
			this.txtPackaging.Name = "txtPackaging";
			this.txtPackaging.Size = new System.Drawing.Size(184, 20);
			this.txtPackaging.TabIndex = 62;
			this.txtPackaging.Text = "";
			// 
			// cmbReorderingLevel
			// 
			this.cmbReorderingLevel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbReorderingLevel.Items.AddRange(new object[] {
																	"0",
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
																	"20",
																	"21",
																	"22",
																	"23",
																	"24",
																	"25"});
			this.cmbReorderingLevel.Location = new System.Drawing.Point(136, 156);
			this.cmbReorderingLevel.Name = "cmbReorderingLevel";
			this.cmbReorderingLevel.Size = new System.Drawing.Size(56, 21);
			this.cmbReorderingLevel.TabIndex = 63;
			// 
			// lbxTrademarks
			// 
			this.lbxTrademarks.Location = new System.Drawing.Point(136, 32);
			this.lbxTrademarks.Name = "lbxTrademarks";
			this.lbxTrademarks.Size = new System.Drawing.Size(184, 95);
			this.lbxTrademarks.TabIndex = 64;
			this.lbxTrademarks.DoubleClick += new System.EventHandler(this.lbxTrademarks_DoubleClick);
			// 
			// fclsDMModifyProduct_SubProd
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(330, 224);
			this.Controls.Add(this.lbxTrademarks);
			this.Controls.Add(this.cmbReorderingLevel);
			this.Controls.Add(this.txtPackaging);
			this.Controls.Add(this.txtSubProductName);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.lblReorderingLevel);
			this.Controls.Add(this.lblTrademark);
			this.Controls.Add(this.lblPackaging);
			this.Controls.Add(this.lblSubProductName);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "fclsDMModifyProduct_SubProd";
			this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "fclsDMModifyProduct_SubProd";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.fclsDMModifyProduct_SubProd_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		private void lbxTrademarks_DoubleClick(object sender, System.EventArgs e)
		{
			fclsDMTrademarks frmTrademarks = new fclsDMTrademarks(m_odcConnection);
			frmTrademarks.ShowDialog();
			
			// refresh trademark combo box
			this.lbxTrademarks.Items.Clear();
			m_dtaTrademarks = new DataTable();
			m_odaTrademarks = new OleDbDataAdapter("SELECT * FROM Trademarks ORDER BY Trademark",m_odcConnection);
			m_odaTrademarks.Fill(m_dtaTrademarks);

			foreach(DataRow dtrRow in m_dtaTrademarks.Rows)
				this.lbxTrademarks.Items.Add(dtrRow["Trademark"].ToString());
		}

		public object[] GetSubProductData()
		{
			object[] objSubProductData = new object[4];

			objSubProductData[0] = this.txtSubProductName.Text;
			objSubProductData[1] = m_dtaTrademarks.Rows[this.lbxTrademarks.SelectedIndex]["MarComId"];
			objSubProductData[2] = this.txtPackaging.Text;
			objSubProductData[3] = int.Parse(this.cmbReorderingLevel.SelectedItem.ToString(), CultureInfo.InvariantCulture);

			return objSubProductData;
		}

		public void SetSubProductData(object[] objSubProductData)
		{
			this.txtSubProductName.Text = (string) objSubProductData[0];
			this.lbxTrademarks.SelectedIndex = (int) objSubProductData[1];
			this.txtPackaging.Text = (string) objSubProductData[2];
			this.cmbReorderingLevel.SelectedItem = clsUtilities.FindItemIndex(((int)objSubProductData[3]).ToString(),
																			  this.cmbReorderingLevel);
		}

		private void fclsDMModifyProduct_SubProd_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			bool blnItemExists = false;
			DataRow[] dtrItemsFound;

			// check if user canceled dialog, in which case no checks are going to be performed
			if(this.DialogResult != DialogResult.Cancel)
			{
				// forbid exit by default
				e.Cancel = true;

				// check if a subproduct name has been entered
				if(this.txtSubProductName.Text.Length > 0)
				{
					// check if a trademark was selected from the list
					if(this.lbxTrademarks.SelectedIndex != -1)
					{
						// check if the packaging field contains something
						if(this.txtPackaging.Text.Length > 0)
						{
							//
							// check if sub-product already exits
							//
							// check product name
							dtrItemsFound = m_dtaSubProducts.Select("[MatName] LIKE \'" + this.txtSubProductName.Text + "\'");

							// check trademark (if needed)
							foreach(DataRow dtrRow in dtrItemsFound)
							{
								if((int) dtrRow["MarComId"] == (int) m_dtaTrademarks.Rows[this.lbxTrademarks.SelectedIndex]["MarComId"])
								{
									blnItemExists = true;
									break;
								}
							}
							
							if(blnItemExists)
								MessageBox.Show("A sub-product with this name and trademark already exists!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
							else
							{
								// allow exit
								e.Cancel = false;
							}
						}
						else
							MessageBox.Show("The packaging field cannot be empty!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
					}
					else
						MessageBox.Show("A trademark must be selected from the list. The list's contents can be modified by double-clicking on it.", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
				}
				else
					MessageBox.Show("The sub-product name cannot be empty!", this.Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}
		
		[Obsolete("This method of calling this window is obsolete. Use GetSubProductData() to obtain form data.", false)]
		public string[] ShowSubProductWindow(string[] strDefaultText)
		{
			this.Text = "Quick Stock - Modify Sub-Product";
			this.txtSubProductName.Text = strDefaultText[0];
			for(int i = 0; i< m_dtaTrademarks.Rows.Count; i++)
			{
				if(int.Parse(m_dtaTrademarks.Rows[i]["MarComId"].ToString()) == int.Parse(strDefaultText[1]))
					this.lbxTrademarks.SelectedIndex = i;
			}
			this.txtPackaging.Text = strDefaultText[2];
			if(strDefaultText[3] == "0.5")
				strDefaultText[3] = "1/2";
			if(strDefaultText[3] == "0.25")
				strDefaultText[3] = "1/4";
			this.cmbReorderingLevel.SelectedIndex = clsUtilities.FindItemIndex(strDefaultText[3],this.cmbReorderingLevel);
			if(this.ShowDialog() == DialogResult.OK)
			{
				string[] strNewSubProduct = new string[4];
				strNewSubProduct[0] = this.txtSubProductName.Text;
				strNewSubProduct[1] = m_dtaTrademarks.Rows[this.lbxTrademarks.SelectedIndex]["MarComId"].ToString();
				strNewSubProduct[2] = this.txtPackaging.Text;
				strNewSubProduct[3] = this.cmbReorderingLevel.SelectedItem.ToString();
				return strNewSubProduct;
			}
			else
				return null;
		}

		[Obsolete("This method of calling this window is obsolete. Use GetSubProductData() to obtain form data.", false)]
		public string[] ShowSubProductWindow()
		{
			this.Text = "Quick Stock - Add New Sub-Product";
			this.cmbReorderingLevel.SelectedIndex = 0;
			if(this.ShowDialog() == DialogResult.OK)
			{
				if(!(this.txtSubProductName.Text.Length == 0 || this.lbxTrademarks.SelectedIndex == -1 || this.txtPackaging.Text.Length == 0))
				{
					string[] strNewSubProduct = new string[4];
					strNewSubProduct[0] = this.txtSubProductName.Text;
					strNewSubProduct[1] = m_dtaTrademarks.Rows[this.lbxTrademarks.SelectedIndex]["MarComId"].ToString();
					strNewSubProduct[2] = this.txtPackaging.Text;
					strNewSubProduct[3] = this.cmbReorderingLevel.SelectedItem.ToString();
					return strNewSubProduct;
				}
				else
				{
					MessageBox.Show("Please fill in all the fields!","Data Missing",MessageBoxButtons.OK,MessageBoxIcon.Error);
					return this.ShowSubProductWindow();
				}
			}
			else
				return null;
		}
	}
}
