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
	/// Summary description for frmEmployees.
	/// </summary>
	public class fclsDMEmployees : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblMessage;

		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		private OleDbConnection		m_odcConnection;
		private OleDbDataAdapter	m_odaEmployees;
		private System.Windows.Forms.Panel panel2;
		private System.Windows.Forms.Button btnSave;
		private System.Windows.Forms.Button btnRemove;
		private System.Windows.Forms.ListView lsvEmployees;
		private System.Windows.Forms.ColumnHeader colLastName;
		private System.Windows.Forms.ColumnHeader colFirstName;
		private System.Windows.Forms.ColumnHeader colTitle;
		private System.Windows.Forms.ColumnHeader colPhoneNumber;
		private System.Windows.Forms.ColumnHeader colActive;
		private System.Windows.Forms.Button btnNew;
		private System.Windows.Forms.TextBox txtTitle;
		private System.Windows.Forms.Label lblStatus;
		private System.Windows.Forms.Label lblPhoneNumber;
		private System.Windows.Forms.Label lblLastName;
		private System.Windows.Forms.Label lblFirstName;
		private System.Windows.Forms.Label lblTitle;
		private System.Windows.Forms.TextBox txtPhoneNumber;
		private System.Windows.Forms.TextBox txtLastName;
		private System.Windows.Forms.TextBox txtFirstName;
		private System.Windows.Forms.CheckBox ckbStatus;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Button btnCancel;
		private EmployeeListViewItem m_elviSelectedItem;

		private ArrayList m_alEmployeeList;
		private bool m_blnCancelButton, m_blnNewButton, m_blnOkButton, m_blnSaveButton;
		private bool m_blnChangesMade;
		private clsListViewColumnSorter m_lvwColumnSorter;
		private DataTable m_dtaEmployees;
		private int m_intLastUsedEmployeeId;

		public fclsDMEmployees(OleDbConnection odcConnection)
		{
			InitializeComponent();
			
			// Variable declaration
			int intCurrentEmployeeId;
			OleDbCommandBuilder ocbEmployees;
			EmployeeListViewItem elviItem;
						
			// Variable initialization
			m_alEmployeeList = new ArrayList();
			m_blnCancelButton = m_blnNewButton = m_blnOkButton = false;
			m_blnSaveButton = true;
			m_blnChangesMade = false;
			m_dtaEmployees = new DataTable();
			m_intLastUsedEmployeeId = -1;
			m_lvwColumnSorter = new clsListViewColumnSorter();
			m_odcConnection = odcConnection;

			// Get data from database and store it in DataTable m_dtaEmployees
			m_odaEmployees = new OleDbDataAdapter("SELECT * FROM Employees ORDER BY LastName,FirstName", m_odcConnection);
			ocbEmployees = new OleDbCommandBuilder(m_odaEmployees);
			m_odaEmployees.Fill(m_dtaEmployees);
			
			// Populate ListView and get last used EmployeeId and store it in m_intLastUsedEmployeeId
			foreach(DataRow dtrRow in m_dtaEmployees.Rows)
			{
				intCurrentEmployeeId = int.Parse(dtrRow["EmployeeId"].ToString());
				if(intCurrentEmployeeId > m_intLastUsedEmployeeId)
					m_intLastUsedEmployeeId = intCurrentEmployeeId;
				
				elviItem = new EmployeeListViewItem(int.Parse(dtrRow["EmployeeId"].ToString()),
													dtrRow["Title"].ToString(),
													dtrRow["FirstName"].ToString(),
													dtrRow["LastName"].ToString(),
													dtrRow["Phone"].ToString(),
													int.Parse(dtrRow["Status"].ToString()));
				
				this.lsvEmployees.Items.Add(elviItem);
			}

			// Sets the listview control's sorter and initialize the sorter
			this.lsvEmployees.ListViewItemSorter = m_lvwColumnSorter;
			m_lvwColumnSorter.SortColumn = 0;
			m_lvwColumnSorter.Order = SortOrder.Ascending;
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
			this.lblMessage = new System.Windows.Forms.Label();
			this.panel2 = new System.Windows.Forms.Panel();
			this.btnSave = new System.Windows.Forms.Button();
			this.btnRemove = new System.Windows.Forms.Button();
			this.btnNew = new System.Windows.Forms.Button();
			this.lsvEmployees = new System.Windows.Forms.ListView();
			this.colLastName = new System.Windows.Forms.ColumnHeader();
			this.colFirstName = new System.Windows.Forms.ColumnHeader();
			this.colTitle = new System.Windows.Forms.ColumnHeader();
			this.colPhoneNumber = new System.Windows.Forms.ColumnHeader();
			this.colActive = new System.Windows.Forms.ColumnHeader();
			this.txtTitle = new System.Windows.Forms.TextBox();
			this.lblStatus = new System.Windows.Forms.Label();
			this.lblPhoneNumber = new System.Windows.Forms.Label();
			this.lblLastName = new System.Windows.Forms.Label();
			this.lblFirstName = new System.Windows.Forms.Label();
			this.lblTitle = new System.Windows.Forms.Label();
			this.txtPhoneNumber = new System.Windows.Forms.TextBox();
			this.txtLastName = new System.Windows.Forms.TextBox();
			this.txtFirstName = new System.Windows.Forms.TextBox();
			this.ckbStatus = new System.Windows.Forms.CheckBox();
			this.btnOk = new System.Windows.Forms.Button();
			this.btnHelp = new System.Windows.Forms.Button();
			this.btnCancel = new System.Windows.Forms.Button();
			this.panel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblMessage
			// 
			this.lblMessage.AutoSize = true;
			this.lblMessage.Location = new System.Drawing.Point(8, 416);
			this.lblMessage.Name = "lblMessage";
			this.lblMessage.Size = new System.Drawing.Size(381, 16);
			this.lblMessage.TabIndex = 13;
			this.lblMessage.Text = "Press New to add a name or select a name to modify or to remove from list.";
			// 
			// panel2
			// 
			this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.panel2.Controls.Add(this.txtTitle);
			this.panel2.Controls.Add(this.lblStatus);
			this.panel2.Controls.Add(this.lblPhoneNumber);
			this.panel2.Controls.Add(this.lblLastName);
			this.panel2.Controls.Add(this.lblFirstName);
			this.panel2.Controls.Add(this.lblTitle);
			this.panel2.Controls.Add(this.txtPhoneNumber);
			this.panel2.Controls.Add(this.txtLastName);
			this.panel2.Controls.Add(this.txtFirstName);
			this.panel2.Controls.Add(this.ckbStatus);
			this.panel2.Controls.Add(this.btnSave);
			this.panel2.Controls.Add(this.btnRemove);
			this.panel2.Controls.Add(this.btnNew);
			this.panel2.Location = new System.Drawing.Point(8, 224);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(472, 136);
			this.panel2.TabIndex = 32;
			// 
			// btnSave
			// 
			this.btnSave.Location = new System.Drawing.Point(384, 51);
			this.btnSave.Name = "btnSave";
			this.btnSave.Size = new System.Drawing.Size(80, 32);
			this.btnSave.TabIndex = 15;
			this.btnSave.Text = "Save";
			this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
			// 
			// btnRemove
			// 
			this.btnRemove.Location = new System.Drawing.Point(384, 91);
			this.btnRemove.Name = "btnRemove";
			this.btnRemove.Size = new System.Drawing.Size(80, 32);
			this.btnRemove.TabIndex = 14;
			this.btnRemove.Text = "Remove";
			this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
			// 
			// btnNew
			// 
			this.btnNew.Location = new System.Drawing.Point(384, 11);
			this.btnNew.Name = "btnNew";
			this.btnNew.Size = new System.Drawing.Size(80, 32);
			this.btnNew.TabIndex = 13;
			this.btnNew.Text = "New";
			this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
			// 
			// lsvEmployees
			// 
			this.lsvEmployees.Activation = System.Windows.Forms.ItemActivation.OneClick;
			this.lsvEmployees.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						   this.colLastName,
																						   this.colFirstName,
																						   this.colTitle,
																						   this.colPhoneNumber,
																						   this.colActive});
			this.lsvEmployees.FullRowSelect = true;
			this.lsvEmployees.HideSelection = false;
			this.lsvEmployees.Location = new System.Drawing.Point(9, 8);
			this.lsvEmployees.MultiSelect = false;
			this.lsvEmployees.Name = "lsvEmployees";
			this.lsvEmployees.Size = new System.Drawing.Size(469, 208);
			this.lsvEmployees.TabIndex = 33;
			this.lsvEmployees.View = System.Windows.Forms.View.Details;
			this.lsvEmployees.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lsvEmployees_MouseDown);
			this.lsvEmployees.Click += new System.EventHandler(this.lsvEmployees_Click);
			this.lsvEmployees.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lsvEmployees_ColumnClick);
			// 
			// colLastName
			// 
			this.colLastName.Text = "Last Name";
			this.colLastName.Width = 120;
			// 
			// colFirstName
			// 
			this.colFirstName.Text = "First Name";
			this.colFirstName.Width = 120;
			// 
			// colTitle
			// 
			this.colTitle.Text = "Title";
			this.colTitle.Width = 45;
			// 
			// colPhoneNumber
			// 
			this.colPhoneNumber.Text = "Phone Number";
			this.colPhoneNumber.Width = 120;
			// 
			// colActive
			// 
			this.colActive.Text = "Active";
			// 
			// txtTitle
			// 
			this.txtTitle.Enabled = false;
			this.txtTitle.Location = new System.Drawing.Point(120, 9);
			this.txtTitle.Name = "txtTitle";
			this.txtTitle.Size = new System.Drawing.Size(72, 20);
			this.txtTitle.TabIndex = 44;
			this.txtTitle.Text = "";
			// 
			// lblStatus
			// 
			this.lblStatus.AutoSize = true;
			this.lblStatus.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblStatus.ForeColor = System.Drawing.Color.Red;
			this.lblStatus.Location = new System.Drawing.Point(8, 105);
			this.lblStatus.Name = "lblStatus";
			this.lblStatus.Size = new System.Drawing.Size(43, 20);
			this.lblStatus.TabIndex = 42;
			this.lblStatus.Text = "Status";
			this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblPhoneNumber
			// 
			this.lblPhoneNumber.AutoSize = true;
			this.lblPhoneNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPhoneNumber.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblPhoneNumber.Location = new System.Drawing.Point(8, 81);
			this.lblPhoneNumber.Name = "lblPhoneNumber";
			this.lblPhoneNumber.Size = new System.Drawing.Size(95, 20);
			this.lblPhoneNumber.TabIndex = 41;
			this.lblPhoneNumber.Text = "Phone Number";
			this.lblPhoneNumber.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblLastName
			// 
			this.lblLastName.AutoSize = true;
			this.lblLastName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLastName.ForeColor = System.Drawing.Color.Red;
			this.lblLastName.Location = new System.Drawing.Point(8, 57);
			this.lblLastName.Name = "lblLastName";
			this.lblLastName.Size = new System.Drawing.Size(69, 20);
			this.lblLastName.TabIndex = 40;
			this.lblLastName.Text = "Last Name";
			this.lblLastName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblFirstName
			// 
			this.lblFirstName.AutoSize = true;
			this.lblFirstName.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblFirstName.ForeColor = System.Drawing.Color.Red;
			this.lblFirstName.Location = new System.Drawing.Point(8, 33);
			this.lblFirstName.Name = "lblFirstName";
			this.lblFirstName.Size = new System.Drawing.Size(70, 20);
			this.lblFirstName.TabIndex = 39;
			this.lblFirstName.Text = "First Name";
			this.lblFirstName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// lblTitle
			// 
			this.lblTitle.AutoSize = true;
			this.lblTitle.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblTitle.Location = new System.Drawing.Point(8, 9);
			this.lblTitle.Name = "lblTitle";
			this.lblTitle.Size = new System.Drawing.Size(31, 20);
			this.lblTitle.TabIndex = 38;
			this.lblTitle.Text = "Title";
			this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// txtPhoneNumber
			// 
			this.txtPhoneNumber.Enabled = false;
			this.txtPhoneNumber.Location = new System.Drawing.Point(120, 81);
			this.txtPhoneNumber.Name = "txtPhoneNumber";
			this.txtPhoneNumber.Size = new System.Drawing.Size(160, 20);
			this.txtPhoneNumber.TabIndex = 37;
			this.txtPhoneNumber.Text = "";
			// 
			// txtLastName
			// 
			this.txtLastName.Enabled = false;
			this.txtLastName.Location = new System.Drawing.Point(120, 57);
			this.txtLastName.Name = "txtLastName";
			this.txtLastName.Size = new System.Drawing.Size(248, 20);
			this.txtLastName.TabIndex = 36;
			this.txtLastName.Text = "";
			// 
			// txtFirstName
			// 
			this.txtFirstName.Enabled = false;
			this.txtFirstName.Location = new System.Drawing.Point(120, 33);
			this.txtFirstName.Name = "txtFirstName";
			this.txtFirstName.Size = new System.Drawing.Size(248, 20);
			this.txtFirstName.TabIndex = 35;
			this.txtFirstName.Text = "";
			// 
			// ckbStatus
			// 
			this.ckbStatus.BackColor = System.Drawing.Color.White;
			this.ckbStatus.Enabled = false;
			this.ckbStatus.Location = new System.Drawing.Point(120, 105);
			this.ckbStatus.Name = "ckbStatus";
			this.ckbStatus.Size = new System.Drawing.Size(56, 20);
			this.ckbStatus.TabIndex = 43;
			this.ckbStatus.Text = "Active";
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(300, 376);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(80, 32);
			this.btnOk.TabIndex = 36;
			this.btnOk.Text = "Ok";
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// btnHelp
			// 
			this.btnHelp.Location = new System.Drawing.Point(107, 376);
			this.btnHelp.Name = "btnHelp";
			this.btnHelp.Size = new System.Drawing.Size(80, 32);
			this.btnHelp.TabIndex = 35;
			this.btnHelp.Text = "Help";
			this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(203, 376);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(80, 32);
			this.btnCancel.TabIndex = 34;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// fclsDMEmployees
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(486, 436);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnHelp);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.lblMessage);
			this.Controls.Add(this.lsvEmployees);
			this.Controls.Add(this.panel2);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
			this.Name = "fclsDMEmployees";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Quick Stock - Associates / Employees";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.fclsDMEmployees_Closing);
			this.panel2.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion
//============================================================================================

		private void fclsDMEmployees_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			DialogResult dlgResult;

			if(!(m_blnCancelButton || m_blnOkButton) && (m_blnChangesMade || m_alEmployeeList.Count > 0))
			{
				dlgResult = MessageBox.Show(this,"Do you want to save the changes before closing?",this.Text,MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);

				switch(dlgResult)
				{
					case DialogResult.Yes:
						if(this.txtFirstName.Text.Length > 0 && this.txtLastName.Text.Length > 0)
							this.SaveToDatabase();
						this.SaveToDatabase();
					break;
					
					case DialogResult.Cancel:
						e.Cancel = true;
					break;
				}
			}
		}

		private void lsvEmployees_Click(object sender, System.EventArgs e)
		{
			if(this.lsvEmployees.SelectedItems.Count != 0)
			{
				m_elviSelectedItem = (EmployeeListViewItem) this.lsvEmployees.SelectedItems[0];
				
				this.txtTitle.Text = m_elviSelectedItem.EmployeeTitle;
				this.txtFirstName.Text = m_elviSelectedItem.EmployeeFirstName;
				this.txtLastName.Text = m_elviSelectedItem.EmployeeLastName;
				this.txtPhoneNumber.Text = m_elviSelectedItem.EmployeePhoneNumber;
				this.ckbStatus.Checked = m_elviSelectedItem.EmployeeStatus;

				m_blnNewButton = m_blnSaveButton = false;
				this.txtFirstName.Enabled = this.txtLastName.Enabled = this.txtPhoneNumber.Enabled = this.txtTitle.Enabled = this.ckbStatus.Enabled = true;
			}
		}
		
		private int CheckIfAlreadyInEmployeeList(string strFirstName, string strLastName)
		{
			int intIdenticalItemIndex = -1;
			
			foreach(EmployeeListViewItem elviEmployee in this.lsvEmployees.Items)
			{
				if(clsUtilities.CompareStrings(elviEmployee.SubItems[0].Text,strLastName) && clsUtilities.CompareStrings(elviEmployee.SubItems[1].Text,strFirstName))
				{
					intIdenticalItemIndex = elviEmployee.Index;
					break;
				}
			}

			return intIdenticalItemIndex;
		}

		private void lsvEmployees_ColumnClick(object sender, System.Windows.Forms.ColumnClickEventArgs e)
		{
			// Determine if clicked column is already the column that is being sorted.
			if (e.Column == m_lvwColumnSorter.SortColumn)
			{
				// Reverse the current sort direction for this column.
				if (m_lvwColumnSorter.Order == SortOrder.Ascending)
				{
					m_lvwColumnSorter.Order = SortOrder.Descending;
				}
				else
				{
					m_lvwColumnSorter.Order = SortOrder.Ascending;
				}
			}
			else
			{
				// Set the column number that is to be sorted; default to ascending.
				m_lvwColumnSorter.SortColumn = e.Column;
				m_lvwColumnSorter.Order = SortOrder.Ascending;
			}

			// Perform the sort with these new sort options.
			this.lsvEmployees.Sort();	
		}

		private void btnRemove_Click(object sender, System.EventArgs e)
		{
			if(m_elviSelectedItem != null)
			{	
				// If employee was just added, it won't be present in the db
				if(m_elviSelectedItem.State != EmployeeListViewItem.LineState.Added)
				{
					m_elviSelectedItem.State = EmployeeListViewItem.LineState.Removed;
					m_alEmployeeList.Add(m_elviSelectedItem);
				}
				this.lsvEmployees.Items[this.m_elviSelectedItem.Index].Remove();
				this.lsvEmployees.Sort();
				this.ClearCurrent();
			}			
		}

		private void btnSave_Click(object sender, System.EventArgs e)
		{
			if(this.txtFirstName.Text.Length > 0 && this.txtLastName.Text.Length > 0)
			{
				this.SaveToListView();
				this.lblMessage.Text = "Press New to add a name or select a name to modify or remove from list.";
				this.lblMessage.Update();
			}
			else
				MessageBox.Show(this,"A valid First and Last Name must be entered.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
		}

		private void ClearCurrent()
		{
			if(m_elviSelectedItem != null)
			{
				m_elviSelectedItem.Selected = false;
				m_elviSelectedItem = null;
			}

			this.txtFirstName.Text = this.txtLastName.Text = this.txtPhoneNumber.Text = this.txtTitle.Text = "";
			this.ckbStatus.Checked = false;
			this.txtFirstName.Enabled = this.txtLastName.Enabled = this.txtPhoneNumber.Enabled = this.txtTitle.Enabled = this.ckbStatus.Enabled = false;
		}

		private void btnNew_Click(object sender, System.EventArgs e)
		{
			m_blnNewButton = true;

			this.lblMessage.Text = "Fill at least all the red labeled Fieds and press the Save button.";
			this.lblMessage.Update();
			
			this.txtFirstName.Enabled = this.txtLastName.Enabled = this.txtPhoneNumber.Enabled = this.txtTitle.Enabled = this.ckbStatus.Enabled = true;
			this.txtFirstName.Text = this.txtLastName.Text = this.txtPhoneNumber.Text = this.txtTitle.Text = "";
			this.ckbStatus.Checked = true;
		}

		private void lsvEmployees_MouseDown(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			this.ClearCurrent();
		}

		private void SaveToDatabase()
		{
			DataRow	dtrNewRow;
			DataRow[] dtrFoundRows;
			
			// Add item in ListView to arraylist that already contains the removed items
			m_alEmployeeList.AddRange(this.lsvEmployees.Items);

			foreach(EmployeeListViewItem elviEmployee in m_alEmployeeList)
			{
				switch(elviEmployee.State)
				{
					case EmployeeListViewItem.LineState.Added:
						dtrNewRow = m_dtaEmployees.NewRow();
						
						dtrNewRow["EmployeeId"] = elviEmployee.EmployeeId;
						dtrNewRow["Title"] = elviEmployee.EmployeeTitle;
						dtrNewRow["FirstName"] = elviEmployee.EmployeeFirstName;
						dtrNewRow["LastName"] = elviEmployee.EmployeeLastName;
						dtrNewRow["Phone"] = elviEmployee.EmployeePhoneNumber;
						if(elviEmployee.EmployeeStatus)
							dtrNewRow["Status"] = 1;
						else
							dtrNewRow["Status"] = 0;

						// Add the new row to the table
						m_dtaEmployees.Rows.Add(dtrNewRow);
					break;

					case EmployeeListViewItem.LineState.Edited:
						dtrFoundRows = m_dtaEmployees.Select("EmployeeId = " + elviEmployee.EmployeeId);
						if(dtrFoundRows.Length == 1)
						{
							dtrFoundRows[0]["Title"] = elviEmployee.EmployeeTitle;
							dtrFoundRows[0]["FirstName"] = elviEmployee.EmployeeFirstName;
							dtrFoundRows[0]["LastName"] = elviEmployee.EmployeeLastName;
							dtrFoundRows[0]["Phone"] = elviEmployee.EmployeePhoneNumber;
							if(elviEmployee.EmployeeStatus)
								dtrFoundRows[0]["Status"] = 1;
							else
								dtrFoundRows[0]["Status"] = 0;
						}
					break;

					case EmployeeListViewItem.LineState.Removed:
						dtrFoundRows = m_dtaEmployees.Select("EmployeeId = " + elviEmployee.EmployeeId);
						if(dtrFoundRows.Length == 1)
							dtrFoundRows[0].Delete();
					break;
				}
			}

			// Update the Database
			try
			{
				m_odaEmployees.Update(m_dtaEmployees);
				m_dtaEmployees.AcceptChanges();

				// Inform the user
				this.lblMessage.Text = "Changes have been saved.";
				this.lblMessage.Update();
							
			} 
			catch (OleDbException ex)
			{
				m_dtaEmployees.RejectChanges();
				MessageBox.Show(ex.Message);
			}
		}

		private void SaveToListView()
		{
			int intIdenticalItemIndex = -1;
			EmployeeListViewItem elviEmployee;
			
			intIdenticalItemIndex = CheckIfAlreadyInEmployeeList(this.txtFirstName.Text,this.txtLastName.Text);
			if(m_blnNewButton)
			{
				if(intIdenticalItemIndex == -1)
				{
					elviEmployee = new EmployeeListViewItem(++m_intLastUsedEmployeeId,this.txtTitle.Text,this.txtFirstName.Text,this.txtLastName.Text,this.txtPhoneNumber.Text,this.ckbStatus.Checked);
					elviEmployee.State = EmployeeListViewItem.LineState.Added;
					this.lsvEmployees.Items.Add(elviEmployee);

					m_blnChangesMade = true;
					
					this.lsvEmployees.Sort();
					this.ClearCurrent();
				}
				else
					MessageBox.Show(this,"The item you are trying to add is identical to an item already in the list.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
			}
			else
			{
				if(!m_blnSaveButton)
				{
					if(intIdenticalItemIndex == -1 || intIdenticalItemIndex == m_elviSelectedItem.Index)
					{
						m_elviSelectedItem.EmployeeTitle = this.txtTitle.Text;
						m_elviSelectedItem.EmployeeFirstName = this.txtFirstName.Text;
						m_elviSelectedItem.EmployeeLastName = this.txtLastName.Text;
						m_elviSelectedItem.EmployeePhoneNumber = this.txtPhoneNumber.Text;
						m_elviSelectedItem.EmployeeStatus  = this.ckbStatus.Checked;
						m_elviSelectedItem.State = EmployeeListViewItem.LineState.Edited;
						
						m_blnChangesMade = true;
						m_blnSaveButton = true;

						this.lsvEmployees.Sort();
						this.ClearCurrent();
					}
					else
						MessageBox.Show(this,"The item you are trying to add is identical to an item already in the list.",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
				}
			}
		}

		private void btnOk_Click(object sender, System.EventArgs e)
		{
			m_blnOkButton = true;
			if(this.txtFirstName.Text.Length > 0 && this.txtLastName.Text.Length > 0)
				this.SaveToListView();
			this.SaveToDatabase();		
		}

		private void btnCancel_Click(object sender, System.EventArgs e)
		{
			m_blnCancelButton = true;		
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","ModifyEmployees.htm");
		}

//============================================================================================
	}

	public class EmployeeListViewItem:ListViewItem
	{	
		public enum LineState
		{
			Added,
			Edited,
			Removed,
			Unchanged
		}
		
		private LineState m_enuLineState;
		private int m_intEmployeeId, m_intStatus;
		
		public EmployeeListViewItem(int intEmployeeId, string strTitle, string strFirstName, string strLastName, string strPhoneNumber, int intStatus)
		{
			m_intEmployeeId = intEmployeeId;
			m_intStatus = intStatus;

			this.Text = strLastName;
			this.SubItems.Add(strFirstName);
			this.SubItems.Add(strTitle);
			this.SubItems.Add(clsUtilities.FormatPhoneNumbers(strPhoneNumber));
			this.SubItems.Add("");
			
			if(m_intStatus == 1)
				this.EmployeeStatus = true;
			else
				this.EmployeeStatus = false;

			this.State = LineState.Unchanged;
		}

		public EmployeeListViewItem(int intEmployeeId, string strTitle, string strFirstName, string strLastName, string strPhoneNumber, bool blnStatus)
		{
			m_intEmployeeId = intEmployeeId;

			this.Text = strLastName;
			this.SubItems.Add(strFirstName);
			this.SubItems.Add(strTitle);
			this.SubItems.Add(clsUtilities.FormatPhoneNumbers(strPhoneNumber));
			this.SubItems.Add("");

			this.EmployeeStatus = blnStatus;
			this.State = LineState.Unchanged;
		}

		public int EmployeeId
		{
			get
			{
				return m_intEmployeeId;
			}
		}

		public string EmployeeTitle
		{
			get
			{
				return this.SubItems[2].Text;
			}
			set
			{
				this.SubItems[2].Text = value;
			}
		}

		public string EmployeeFirstName
		{
			get
			{
				return this.SubItems[1].Text;
			}
			set
			{
				this.SubItems[1].Text = value;
			}
		}
		
		public string EmployeeLastName
		{
			get
			{
				return this.Text;
			}
			set
			{
				this.Text = value;
			}
		}

		public string EmployeePhoneNumber
		{
			get
			{
				return this.SubItems[3].Text;
			}
			set
			{
				this.SubItems[3].Text = value;
			}		
		}

		public bool EmployeeStatus
		{
			get
			{
				if(m_intStatus == 1)
					return true;
				else
					return false;
			}
			set
			{
				this.SubItems[4].Text = value.ToString();

				if(value == true)
					m_intStatus = 1;
				else
					m_intStatus = 0;
			}
		}

		public int DbEmployeeStatus
		{
			get
			{
				return m_intStatus;
			}
		}

		public LineState State
		{
			get
			{
				return m_enuLineState;
			}
			set
			{
				if(!(m_enuLineState == LineState.Added && value == LineState.Edited))
					m_enuLineState = value;
			}
		}

	}
}
