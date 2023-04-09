using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsDMTrademarks.
	/// </summary>
	public class fclsDMTrademarks : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.Button btnAdd;
		private System.Windows.Forms.Button btnClose;
		private System.Windows.Forms.ListBox lbxTrademarks;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private DataTable m_dtaTrademarks;
		private OleDbConnection m_odcConnection;
		private OleDbDataAdapter m_odaTrademarks;
		private System.Windows.Forms.MenuItem mnuAdd;
		private System.Windows.Forms.MenuItem mnuActive;
		private System.Windows.Forms.MenuItem mnuModify;
		private System.Windows.Forms.MenuItem mnuRemove;
		private System.Windows.Forms.ContextMenu ctmRightClick;

		private int m_intSelectedTrademarkIndex = -1, m_intTrademarkId = -1;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnEdit;

		public fclsDMTrademarks(OleDbConnection odcConnection)
		{
			InitializeComponent();

			m_odcConnection = odcConnection;
			this.LoadData();
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
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnAdd = new System.Windows.Forms.Button();
			this.btnClose = new System.Windows.Forms.Button();
			this.lbxTrademarks = new System.Windows.Forms.ListBox();
			this.ctmRightClick = new System.Windows.Forms.ContextMenu();
			this.mnuActive = new System.Windows.Forms.MenuItem();
			this.mnuAdd = new System.Windows.Forms.MenuItem();
			this.mnuModify = new System.Windows.Forms.MenuItem();
			this.mnuRemove = new System.Windows.Forms.MenuItem();
			this.btnEdit = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// btnRemove
			// 
			this.btnRemove.Enabled = false;
			this.btnRemove.Location = new System.Drawing.Point(184, 216);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(72, 32);
			this.btnRemove.TabIndex = 9;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnAdd
			// 
			this.btnAdd.Location = new System.Drawing.Point(8, 216);
			this.btnAdd.Name = "btnAdd";
			this.btnAdd.Size = new System.Drawing.Size(80, 32);
			this.btnAdd.TabIndex = 8;
			this.btnAdd.Text = "Add";
			this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
			// 
			// btnClose
			// 
			this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnClose.Location = new System.Drawing.Point(104, 256);
			this.btnClose.Name = "btnClose";
			this.btnClose.Size = new System.Drawing.Size(72, 24);
			this.btnClose.TabIndex = 10;
			this.btnClose.Text = "Close";
			this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
			// 
			// lbxTrademarks
			// 
			this.lbxTrademarks.ContextMenu = this.ctmRightClick;
			this.lbxTrademarks.Location = new System.Drawing.Point(8, 8);
			this.lbxTrademarks.Name = "lbxTrademarks";
			this.lbxTrademarks.Size = new System.Drawing.Size(248, 199);
			this.lbxTrademarks.TabIndex = 11;
			this.lbxTrademarks.DoubleClick += new System.EventHandler(this.lbxTrademarks_DoubleClick);
			this.lbxTrademarks.SelectedIndexChanged += new System.EventHandler(this.lbxTrademarks_SelectedIndexChanged);
			// 
			// ctmRightClick
			// 
			this.ctmRightClick.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
																						  this.mnuActive,
																						  this.mnuAdd,
																						  this.mnuModify,
																						  this.mnuRemove});
			this.ctmRightClick.Popup += new System.EventHandler(this.ctmRightClick_Popup);
			// 
			// mnuActive
			// 
			this.mnuActive.Index = 0;
			this.mnuActive.Text = "A&ctive";
			this.mnuActive.Click += new System.EventHandler(this.mnuActive_Click);
			// 
			// mnuAdd
			// 
			this.mnuAdd.Index = 1;
			this.mnuAdd.Text = "&Add";
			this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
			// 
			// mnuModify
			// 
			this.mnuModify.Index = 2;
			this.mnuModify.Text = "&Modify";
			this.mnuModify.Click += new System.EventHandler(this.mnuModify_Click);
			// 
			// mnuRemove
			// 
			this.mnuRemove.Index = 3;
			this.mnuRemove.Text = "&Remove";
			this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
			// 
			// btnEdit
			// 
			this.btnEdit.Enabled = false;
			this.btnEdit.Location = new System.Drawing.Point(104, 216);
			this.btnEdit.Name = "btnEdit";
			this.btnEdit.Size = new System.Drawing.Size(72, 32);
			this.btnEdit.TabIndex = 12;
			this.btnEdit.Text = "Modify";
			this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Location = new System.Drawing.Point(184, 256);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(72, 23);
			this.btnHelp.TabIndex = 13;
			this.btnHelp.Text = "Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// fclsDMTrademarks
			// 
			this.AcceptButton = this.btnClose;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnClose;
			this.ClientSize = new System.Drawing.Size(270, 284);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnEdit);
			this.Controls.Add(this.lbxTrademarks);
			this.Controls.Add(this.btnRemove);
			this.Controls.Add(this.btnAdd);
			this.Controls.Add(this.btnClose);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.MaximizeBox = false;
			this.Name = "fclsDMTrademarks";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quick Stock - Trademarks";
			this.ResumeLayout(false);

		}
		#endregion

		private void LoadData()
		{
			int intCurrentTrademarkId;
			this.lbxTrademarks.Items.Clear();
			
			m_dtaTrademarks = new DataTable();
			m_odaTrademarks = new OleDbDataAdapter("SELECT * FROM Trademarks ORDER BY Trademark",m_odcConnection);
			OleDbCommandBuilder ocbTrademarks = new OleDbCommandBuilder(m_odaTrademarks);
			m_odaTrademarks.Fill(m_dtaTrademarks);

			for(int i=0; i<m_dtaTrademarks.Rows.Count; i++)
			{
				this.lbxTrademarks.Items.Add(m_dtaTrademarks.Rows[i]["Trademark"].ToString());
				intCurrentTrademarkId = int.Parse(m_dtaTrademarks.Rows[i]["MarComId"].ToString());
				if(intCurrentTrademarkId > m_intTrademarkId)
					m_intTrademarkId = intCurrentTrademarkId;
			}
		}

		private void Add()
		{
			string strResponse = InputBox.ShowInputBox("Please enter the name of the new Trademark","New Trademark");
            if (strResponse != null && strResponse.Length > 0)
			{
				int n_nrTrademark = m_dtaTrademarks.Rows.Count;
				if(!checkName(strResponse, n_nrTrademark, m_dtaTrademarks))
					return;
				m_intTrademarkId++;
				DataRow dtrNewTrademark = m_dtaTrademarks.NewRow();
				dtrNewTrademark["MarComId"] = m_intTrademarkId;
				dtrNewTrademark["Trademark"] = strResponse;
				dtrNewTrademark["Status"] = 1;
				m_dtaTrademarks.Rows.Add(dtrNewTrademark);
				
				try
				{
					m_odaTrademarks.Update(m_dtaTrademarks);
					m_dtaTrademarks.AcceptChanges();
					
					this.LoadData();
					this.lbxTrademarks.SelectedIndex = clsUtilities.FindItemIndex(strResponse,this.lbxTrademarks);
				} 
				catch (OleDbException ex)
				{
					m_dtaTrademarks.RejectChanges();
					MessageBox.Show(ex.Message);
				}				
			}
		}

		private void Modify()
		{
			string oldName = m_dtaTrademarks.Rows[m_intSelectedTrademarkIndex]["Trademark"].ToString();
			string strResponse = InputBox.ShowInputBox("Replace '" + this.lbxTrademarks.SelectedItem.ToString() + "' with:","Edit Trademark",this.lbxTrademarks.SelectedItem.ToString());
            if (strResponse != null && strResponse.Length > 0)
			{
				int n_nrTrademark = m_dtaTrademarks.Rows.Count;
				if(strResponse == oldName)
					return;
				if(!checkName(strResponse, n_nrTrademark, m_dtaTrademarks))
					return;
				m_dtaTrademarks.Rows[m_intSelectedTrademarkIndex]["Trademark"] = strResponse;
				
				try
				{
					m_odaTrademarks.Update(m_dtaTrademarks);
					m_dtaTrademarks.AcceptChanges();
					
					this.LoadData();
					this.lbxTrademarks.SelectedIndex = m_intSelectedTrademarkIndex;
				} 
				catch (OleDbException ex)
				{
					m_dtaTrademarks.RejectChanges();
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void Remove()
		{
			if(MessageBox.Show("Are you sure you want to remove the '" + this.lbxTrademarks.SelectedItem.ToString() + "' trademark?","Remove Trademark",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
			{
				m_dtaTrademarks.Rows[m_intSelectedTrademarkIndex].Delete();

				try
				{
					m_odaTrademarks.Update(m_dtaTrademarks);
					m_dtaTrademarks.AcceptChanges();
					
					this.LoadData();
				} 
				catch (OleDbException ex)
				{
					m_dtaTrademarks.RejectChanges();
					MessageBox.Show(ex.Message);
				}
			}
		}

		private void lbxTrademarks_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			m_intSelectedTrademarkIndex = this.lbxTrademarks.SelectedIndex;
			if(m_intSelectedTrademarkIndex != -1)
			{
				this.btnEdit.Enabled = true;
				this.btnRemove.Enabled = true;
			}
			else
			{
				this.btnEdit.Enabled = false;
				this.btnRemove.Enabled = false;
			}
		}

		private void btnAdd_Click(object sender, System.EventArgs e)
		{
			this.Add();
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			this.Remove();
		}

		private void btnClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void lbxTrademarks_DoubleClick(object sender, System.EventArgs e)
		{
			this.Modify();
		}

		private void mnuAdd_Click(object sender, System.EventArgs e)
		{
			this.Add();
		}

		private void ctmRightClick_Popup(object sender, System.EventArgs e)
		{
			if(this.lbxTrademarks.SelectedIndex == -1)
			{
				this.mnuActive.Enabled = false;
				this.mnuModify.Enabled = false;
				this.mnuRemove.Enabled = false;
			}
			else
			{
				this.mnuActive.Enabled = true;
				if(this.IsCurrentItemActive())
					this.mnuActive.Checked = true;
				else
					this.mnuActive.Checked = false;
				this.mnuModify.Enabled = true;
				this.mnuRemove.Enabled = true;
			}
		}

		private void mnuActive_Click(object sender, System.EventArgs e)
		{
			int intStatus = -1;
			if(this.mnuActive.Checked)
			{
				this.mnuActive.Checked = false;
				intStatus = 0;
			}
			else
			{
				this.mnuActive.Checked = true;
				intStatus = 1;
			}
			m_dtaTrademarks.Rows[m_intSelectedTrademarkIndex]["Status"] = intStatus;
			m_odaTrademarks.Update(m_dtaTrademarks);
			m_dtaTrademarks.AcceptChanges();
			this.LoadData();
			this.lbxTrademarks.SelectedIndex = m_intSelectedTrademarkIndex;
		
		}
		
		private void mnuModify_Click(object sender, System.EventArgs e)
		{
			this.Modify();
		}

		private void mnuRemove_Click(object sender, System.EventArgs e)
		{
			this.Remove();
		}

		private void btnEdit_Click(object sender, System.EventArgs e)
		{
			this.Modify();
		}
		private bool checkName(string strResponse, int nrCheck, DataTable m_dtaCheck)
		{
			string strName;
			for(int i=0; i<nrCheck; i++)
			{
				strName = m_dtaCheck.Rows[i]["Trademark"].ToString();
				if(strName == strResponse)
				{
					MessageBox.Show("This name is already in the database!\n" +
						"You must change the name!","Trademark Name Error!");
					return false;
				}
			}
			return true;
		}
		private bool IsCurrentItemActive()
		{
			if(int.Parse(m_dtaTrademarks.Rows[m_intSelectedTrademarkIndex]["Status"].ToString()) == 1)
				return true;
			else
				return false;
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","ModifyTrademarks.htm");  //

		}

	}
}
