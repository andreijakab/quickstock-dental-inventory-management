using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace DSMS
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class InputBox : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label lblQuery;
		private System.Windows.Forms.TextBox txtAnswer;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.Button btnCancel;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public InputBox()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
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
            this.lblQuery = new System.Windows.Forms.Label();
            this.txtAnswer = new System.Windows.Forms.TextBox();
            this.btnOk = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lblQuery
            // 
            this.lblQuery.Location = new System.Drawing.Point(8, 8);
            this.lblQuery.Name = "lblQuery";
            this.lblQuery.Size = new System.Drawing.Size(280, 32);
            this.lblQuery.TabIndex = 0;
            this.lblQuery.Text = "label1";
            // 
            // txtAnswer
            // 
            this.txtAnswer.Location = new System.Drawing.Point(8, 48);
            this.txtAnswer.Name = "txtAnswer";
            this.txtAnswer.Size = new System.Drawing.Size(280, 20);
            this.txtAnswer.TabIndex = 1;
            // 
            // btnOk
            // 
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Location = new System.Drawing.Point(60, 80);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(75, 23);
            this.btnOk.TabIndex = 2;
            this.btnOk.Text = "Ok";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(164, 80);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            // 
            // InputBox
            // 
            this.AcceptButton = this.btnOk;
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(298, 112);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.txtAnswer);
            this.Controls.Add(this.lblQuery);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "InputBox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		public static string ShowInputBox(string strQuery)
		{
			InputBox ibxInputBox = new InputBox();
			ibxInputBox.Text = "";
            ibxInputBox.txtAnswer.MaxLength = 255;
			ibxInputBox.lblQuery.Text = strQuery;
			if(ibxInputBox.ShowDialog() == DialogResult.OK)
				return ibxInputBox.txtAnswer.Text;
			else
				return null;

		}

		public static string ShowInputBox(string strQuery, string strTitle)
		{
			InputBox ibxInputBox = new InputBox();
			ibxInputBox.Text = strTitle;
            ibxInputBox.txtAnswer.MaxLength = 255;
			ibxInputBox.lblQuery.Text = strQuery;
			if(ibxInputBox.ShowDialog() == DialogResult.OK)
				return ibxInputBox.txtAnswer.Text;
			else
				return null;

		}

		public static string ShowInputBox(string strQuery, string strTitle, string strDefaultText)
		{
			InputBox ibxInputBox = new InputBox();
			ibxInputBox.Text = strTitle;
            ibxInputBox.txtAnswer.MaxLength = 255;
			ibxInputBox.lblQuery.Text = strQuery;
			ibxInputBox.txtAnswer.Text = strDefaultText;
			if(ibxInputBox.ShowDialog() == DialogResult.OK)
				return ibxInputBox.txtAnswer.Text;
			else
				return null;
		}

		public static string ShowPasswordBox(string strQuery, string strTitle)
		{
			InputBox ibxInputBox = new InputBox();
			ibxInputBox.Text = strTitle;
			ibxInputBox.lblQuery.Text = strQuery;
            ibxInputBox.txtAnswer.UseSystemPasswordChar = true;
			if(ibxInputBox.ShowDialog() == DialogResult.OK)
				return ibxInputBox.txtAnswer.Text;
			else
				return null;
		}
	}
}
