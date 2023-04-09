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
	/// Summary description for fclsGENLogin.
	/// </summary>
	public class fclsGENLogin : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblEmployee;
		private System.Windows.Forms.Label lblPassword;
		private System.Windows.Forms.Button cmdOk;
        private System.Windows.Forms.ComboBox cbxEmployees;
        private System.Windows.Forms.TextBox txtPassword;
        private Button btnExit;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        
        private DataTable           m_dtaEmployees;
        private int                 intNLoginAttempts;
        private OleDbConnection     m_odcConnection;
        private ToolTip             m_ttToolTip;
		
		public fclsGENLogin(OleDbConnection	odcConnection)
		{
			// Required for Windows Form Designer support
			InitializeComponent();

            this.DialogResult = DialogResult.Cancel;
			m_odcConnection = odcConnection;
            m_ttToolTip = new ToolTip();
            intNLoginAttempts = 0;
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
            this.lblEmployee = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.cbxEmployees = new System.Windows.Forms.ComboBox();
            this.cmdOk = new System.Windows.Forms.Button();
            this.btnExit = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblEmployee
            // 
            this.lblEmployee.AutoSize = true;
            this.lblEmployee.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblEmployee.Location = new System.Drawing.Point(12, 15);
            this.lblEmployee.Name = "lblEmployee";
            this.lblEmployee.Size = new System.Drawing.Size(60, 14);
            this.lblEmployee.TabIndex = 0;
            this.lblEmployee.Text = "Employee";
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Tahoma", 9F);
            this.lblPassword.Location = new System.Drawing.Point(12, 47);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(58, 14);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Password";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(76, 45);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Size = new System.Drawing.Size(152, 20);
            this.txtPassword.TabIndex = 1;
            this.txtPassword.UseSystemPasswordChar = true;
            this.txtPassword.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtPassword_KeyDown);
            // 
            // cbxEmployees
            // 
            this.cbxEmployees.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbxEmployees.Location = new System.Drawing.Point(76, 13);
            this.cbxEmployees.Name = "cbxEmployees";
            this.cbxEmployees.Size = new System.Drawing.Size(152, 21);
            this.cbxEmployees.TabIndex = 0;
            this.cbxEmployees.SelectedIndexChanged += new System.EventHandler(this.cbxEmployees_SelectedIndexChanged);
            this.cbxEmployees.Click += new System.EventHandler(this.cbxEmployees_Click);
            // 
            // cmdOk
            // 
            this.cmdOk.Location = new System.Drawing.Point(12, 86);
            this.cmdOk.Name = "cmdOk";
            this.cmdOk.Size = new System.Drawing.Size(96, 32);
            this.cmdOk.TabIndex = 2;
            this.cmdOk.Text = "Ok";
            this.cmdOk.Click += new System.EventHandler(this.cmdOk_Click);
            // 
            // btnExit
            // 
            this.btnExit.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnExit.Location = new System.Drawing.Point(132, 86);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(96, 32);
            this.btnExit.TabIndex = 3;
            this.btnExit.Text = "Exit";
            this.btnExit.Click += new System.EventHandler(this.btnExit_Click);
            // 
            // fclsGENLogin
            // 
            this.AcceptButton = this.cmdOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnExit;
            this.ClientSize = new System.Drawing.Size(240, 128);
            this.Controls.Add(this.btnExit);
            this.Controls.Add(this.cmdOk);
            this.Controls.Add(this.cbxEmployees);
            this.Controls.Add(this.txtPassword);
            this.Controls.Add(this.lblPassword);
            this.Controls.Add(this.lblEmployee);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fclsGENLogin";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Stock - Login";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.fclsGENLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbxEmployees_Click(object sender, EventArgs e)
        {
            m_ttToolTip.Hide(cbxEmployees);
        }

        private void cbxEmployees_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            this.txtPassword.Clear();
            this.txtPassword.Focus();
        }

		private void cmdOk_Click(object sender, System.EventArgs e)
		{
			if(this.cbxEmployees.SelectedIndex != -1)
            {
                if (this.txtPassword.Text.Length > 0)
                {
                    if (intNLoginAttempts == 2)
                    {
                        MessageBox.Show("You have failed to login three times in a row. \nThe application will now close.",
                                        clsConfiguration.Internal_ApplicationName,
                                        MessageBoxButtons.OK,
                                        MessageBoxIcon.Error,
                                        MessageBoxDefaultButton.Button1);
                        this.Close();
                    }
                    else
                    {
                        string strEmployeePasswordHash = this.m_dtaEmployees.Rows[this.cbxEmployees.SelectedIndex]["UserPassword"].ToString();
                        if (clsUtilities.String_CompareHashes(this.txtPassword.Text, strEmployeePasswordHash))
                        {
                            // set user who currently logged in as active user
                            clsConfiguration.Internal_CurrentUserID = (int)this.m_dtaEmployees.Rows[this.cbxEmployees.SelectedIndex]["EmployeeId"];
                            
                            // set return value for calling form & close
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            MessageBox.Show("You have entered an invalid password.\nPlease try again.",
                                            clsConfiguration.Internal_ApplicationName,
                                            MessageBoxButtons.OK,
                                            MessageBoxIcon.Exclamation,
                                            MessageBoxDefaultButton.Button1);
                            intNLoginAttempts++;
                            this.txtPassword.Clear();
                            this.txtPassword.Focus();
                        }
                    }
                }
                else
                {
                    this.txtPassword.Focus();
                    m_ttToolTip.ToolTipTitle = "Empty Password Field";
                    m_ttToolTip.Show("Please type in a password before pressing on 'OK'.",
                                     this.txtPassword,
                                     0, this.txtPassword.Size.Height,
                                     5000);
                }
			}
			else
            {
                this.cbxEmployees.Focus();
                m_ttToolTip.ToolTipTitle = "No Employee Selected";
                m_ttToolTip.Show("Please select an employee from the dropdown list.",
                                 this.cbxEmployees,
                                 0, this.cbxEmployees.Size.Height,
                                 5000);
            }
		}

        private void fclsGENLogin_Load(object sender, EventArgs e)
        {
            OleDbDataAdapter oddaEmployees;

            oddaEmployees = new OleDbDataAdapter("SELECT * FROM Employees " +
                                                 "WHERE Status=1 AND UserPassword IS NOT NULL AND LEN(UserPassword) > 0 " +
                                                 "ORDER BY FirstName, LastName", m_odcConnection);
            m_dtaEmployees = new DataTable();

            try
            {
                oddaEmployees.Fill(m_dtaEmployees);

                if (m_dtaEmployees.Rows.Count != 0)
                {
                    foreach (DataRow dtr in m_dtaEmployees.Rows)
                        cbxEmployees.Items.Add(clsUtilities.FormatName_List(dtr["Title"].ToString(), dtr["FirstName"].ToString(), dtr["LastName"].ToString()));

                    this.cbxEmployees.Focus();
                }
                else
                {
                    MessageBox.Show("Application logon bypassed since no active employee has a password configured.",
                                    clsConfiguration.Internal_ApplicationName,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch
            {
                MessageBox.Show("An error occured while initializing the login form. Application logon bypassed.",
                                clsConfiguration.Internal_ApplicationName,
                                MessageBoxButtons.OK,
                                MessageBoxIcon.Exclamation);
                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void txtPassword_KeyDown(object sender, KeyEventArgs e)
        {
            m_ttToolTip.Hide(txtPassword);
        }
	}
}
