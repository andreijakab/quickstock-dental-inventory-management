using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Windows.Forms;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for fclsSIAccounting_Pay.
	/// </summary>
	public class fclsOIAccounting_Pay : System.Windows.Forms.Form
    {
		private System.Windows.Forms.Button btnPay;
		private System.Windows.Forms.Label lblOrderNumber;
		private System.Windows.Forms.Label lblOrderNumber_Data;
        private System.Windows.Forms.Label lblAmountPaid;
		private System.Windows.Forms.ComboBox cmbEmployees;
		private System.Windows.Forms.Label lblPaidUsing;
		private System.Windows.Forms.Label lblAmountDue;
		private System.Windows.Forms.Label lblPaidBy;
		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox txtChequeNumber;
		private System.Windows.Forms.RadioButton optVisa;
		private System.Windows.Forms.RadioButton optMasterCard;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.RadioButton optCheque;
		private System.Windows.Forms.Label lblAmountDue_Data;
		private System.Windows.Forms.Label lblPaymentDate;
		private System.Windows.Forms.DateTimePicker dtpPaymentDate;
		private System.Windows.Forms.Button btnDontPay;
		private System.Windows.Forms.Label lblPenalty;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public enum Caller:int {Accounting, BackOrder, OldOrders, OrderCheckIn};
		
		private bool				m_blnIsPaid;
		private Caller				m_enuCaller;
		private DataTable			m_dtaEmployees;
		private Form				m_frmOwner;
		private NumberFormatInfo	m_nfiNumberFormat;
		private OleDbConnection		m_odcConnection;
		private string				m_strDecimalSeparator, m_strGroupSeparator;
        private PriceTextBox.PriceTextBox txtAmountPaid;
        private PriceTextBox.PriceTextBox txtPenalty;

		public fclsOIAccounting_Pay(Caller enuCaller, Form frmOwner, string strOrderNumber, int intEmployeeId, decimal decAmountDue, OleDbConnection odcConnection)
		{
			DataRow dtrRow;
			int intUserId = -1;
			OleDbDataAdapter odaEmployees;

			InitializeComponent();
			
			// initialize global variables
			m_blnIsPaid = false;
			m_enuCaller = enuCaller;
			m_frmOwner = frmOwner;
			m_odcConnection = odcConnection;

			// Get local number formatting information
			m_nfiNumberFormat =	System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
			m_strDecimalSeparator = m_nfiNumberFormat.CurrencyDecimalSeparator;
			m_strGroupSeparator = m_nfiNumberFormat.CurrencyGroupSeparator;

			// Employees
			// Open the table 'Employees'
			odaEmployees = new OleDbDataAdapter("SELECT * FROM [Employees] WHERE Status = 1 ORDER BY FirstName, LastName", m_odcConnection);
			m_dtaEmployees = new DataTable();
			odaEmployees.Fill(m_dtaEmployees);
			
			// Add employees to combo-box
			for (int i = 0; i < m_dtaEmployees.Rows.Count; i++)
			{
				dtrRow = m_dtaEmployees.Rows[i];
				this.cmbEmployees.Items.Add(clsUtilities.FormatName_List(dtrRow["Title"].ToString(), dtrRow["FirstName"].ToString(), dtrRow["LastName"].ToString()));
			}
			
			// set the default employee either as the one sent to the constructor, or the default employee from the
			// configuration file
            if (intEmployeeId > -1)
                this.cmbEmployees.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intEmployeeId, m_dtaEmployees, 0);
            else
            {
                intUserId = clsConfiguration.Internal_CurrentUserID;
                this.cmbEmployees.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(intUserId, m_dtaEmployees, 0);
            }

			// initalize form
			this.dtpPaymentDate.Value = System.DateTime.Now;
			this.lblOrderNumber_Data.Text = strOrderNumber;
			this.lblAmountDue_Data.Text = this.txtAmountPaid.Text = decAmountDue.ToString(clsUtilities.FORMAT_CURRENCY);
			this.txtPenalty.Text = (0.0M).ToString(clsUtilities.FORMAT_CURRENCY);

			// customize form depending on the caller
			switch(m_enuCaller)
			{
				case Caller.BackOrder:
					this.lblPenalty.Visible = false;
					this.txtPenalty.Visible = false;
				break;

				case Caller.OrderCheckIn:
					this.lblPenalty.Visible = false;
					this.txtPenalty.Visible = false;
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
            this.lblOrderNumber = new System.Windows.Forms.Label();
            this.lblOrderNumber_Data = new System.Windows.Forms.Label();
            this.lblPaymentDate = new System.Windows.Forms.Label();
            this.lblPaidUsing = new System.Windows.Forms.Label();
            this.lblAmountPaid = new System.Windows.Forms.Label();
            this.btnPay = new System.Windows.Forms.Button();
            this.lblPenalty = new System.Windows.Forms.Label();
            this.dtpPaymentDate = new System.Windows.Forms.DateTimePicker();
            this.btnDontPay = new System.Windows.Forms.Button();
            this.lblAmountDue = new System.Windows.Forms.Label();
            this.cmbEmployees = new System.Windows.Forms.ComboBox();
            this.lblPaidBy = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtChequeNumber = new System.Windows.Forms.TextBox();
            this.optVisa = new System.Windows.Forms.RadioButton();
            this.optMasterCard = new System.Windows.Forms.RadioButton();
            this.label4 = new System.Windows.Forms.Label();
            this.optCheque = new System.Windows.Forms.RadioButton();
            this.lblAmountDue_Data = new System.Windows.Forms.Label();
            this.txtAmountPaid = new PriceTextBox.PriceTextBox();
            this.txtPenalty = new PriceTextBox.PriceTextBox();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblOrderNumber
            // 
            this.lblOrderNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNumber.ForeColor = System.Drawing.Color.Red;
            this.lblOrderNumber.Location = new System.Drawing.Point(16, 16);
            this.lblOrderNumber.Name = "lblOrderNumber";
            this.lblOrderNumber.Size = new System.Drawing.Size(80, 16);
            this.lblOrderNumber.TabIndex = 0;
            this.lblOrderNumber.Text = "Order #";
            this.lblOrderNumber.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblOrderNumber_Data
            // 
            this.lblOrderNumber_Data.BackColor = System.Drawing.Color.White;
            this.lblOrderNumber_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblOrderNumber_Data.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblOrderNumber_Data.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblOrderNumber_Data.Location = new System.Drawing.Point(104, 14);
            this.lblOrderNumber_Data.Name = "lblOrderNumber_Data";
            this.lblOrderNumber_Data.Size = new System.Drawing.Size(248, 21);
            this.lblOrderNumber_Data.TabIndex = 1;
            this.lblOrderNumber_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblPaymentDate
            // 
            this.lblPaymentDate.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaymentDate.ForeColor = System.Drawing.Color.Red;
            this.lblPaymentDate.Location = new System.Drawing.Point(0, 42);
            this.lblPaymentDate.Name = "lblPaymentDate";
            this.lblPaymentDate.Size = new System.Drawing.Size(96, 16);
            this.lblPaymentDate.TabIndex = 2;
            this.lblPaymentDate.Text = "Payment date";
            this.lblPaymentDate.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblPaidUsing
            // 
            this.lblPaidUsing.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaidUsing.ForeColor = System.Drawing.Color.Red;
            this.lblPaidUsing.Location = new System.Drawing.Point(16, 122);
            this.lblPaidUsing.Name = "lblPaidUsing";
            this.lblPaidUsing.Size = new System.Drawing.Size(80, 28);
            this.lblPaidUsing.TabIndex = 4;
            this.lblPaidUsing.Text = "Payment method";
            this.lblPaidUsing.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblAmountPaid
            // 
            this.lblAmountPaid.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountPaid.ForeColor = System.Drawing.Color.Red;
            this.lblAmountPaid.Location = new System.Drawing.Point(8, 211);
            this.lblAmountPaid.Name = "lblAmountPaid";
            this.lblAmountPaid.Size = new System.Drawing.Size(88, 16);
            this.lblAmountPaid.TabIndex = 10;
            this.lblAmountPaid.Text = "Amount paid";
            this.lblAmountPaid.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnPay
            // 
            this.btnPay.Location = new System.Drawing.Point(144, 264);
            this.btnPay.Name = "btnPay";
            this.btnPay.Size = new System.Drawing.Size(88, 24);
            this.btnPay.TabIndex = 12;
            this.btnPay.Text = "Save";
            this.btnPay.Click += new System.EventHandler(this.btnPay_Click);
            // 
            // lblPenalty
            // 
            this.lblPenalty.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPenalty.ForeColor = System.Drawing.Color.Red;
            this.lblPenalty.Location = new System.Drawing.Point(8, 235);
            this.lblPenalty.Name = "lblPenalty";
            this.lblPenalty.Size = new System.Drawing.Size(88, 16);
            this.lblPenalty.TabIndex = 13;
            this.lblPenalty.Text = "Penalty";
            this.lblPenalty.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // dtpPaymentDate
            // 
            this.dtpPaymentDate.Location = new System.Drawing.Point(104, 40);
            this.dtpPaymentDate.Name = "dtpPaymentDate";
            this.dtpPaymentDate.Size = new System.Drawing.Size(248, 20);
            this.dtpPaymentDate.TabIndex = 15;
            // 
            // btnDontPay
            // 
            this.btnDontPay.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnDontPay.Location = new System.Drawing.Point(256, 264);
            this.btnDontPay.Name = "btnDontPay";
            this.btnDontPay.Size = new System.Drawing.Size(88, 24);
            this.btnDontPay.TabIndex = 16;
            this.btnDontPay.Text = "Cancel";
            this.btnDontPay.Click += new System.EventHandler(this.btnDontPay_Click);
            // 
            // lblAmountDue
            // 
            this.lblAmountDue.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountDue.ForeColor = System.Drawing.Color.Red;
            this.lblAmountDue.Location = new System.Drawing.Point(8, 187);
            this.lblAmountDue.Name = "lblAmountDue";
            this.lblAmountDue.Size = new System.Drawing.Size(88, 16);
            this.lblAmountDue.TabIndex = 17;
            this.lblAmountDue.Text = "Amount due";
            this.lblAmountDue.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cmbEmployees
            // 
            this.cmbEmployees.ItemHeight = 13;
            this.cmbEmployees.Location = new System.Drawing.Point(104, 64);
            this.cmbEmployees.Name = "cmbEmployees";
            this.cmbEmployees.Size = new System.Drawing.Size(248, 21);
            this.cmbEmployees.TabIndex = 20;
            // 
            // lblPaidBy
            // 
            this.lblPaidBy.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPaidBy.ForeColor = System.Drawing.Color.Red;
            this.lblPaidBy.Location = new System.Drawing.Point(24, 66);
            this.lblPaidBy.Name = "lblPaidBy";
            this.lblPaidBy.Size = new System.Drawing.Size(72, 16);
            this.lblPaidBy.TabIndex = 19;
            this.lblPaidBy.Text = "Paid by";
            this.lblPaidBy.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.txtChequeNumber);
            this.panel1.Controls.Add(this.optVisa);
            this.panel1.Controls.Add(this.optMasterCard);
            this.panel1.Controls.Add(this.label4);
            this.panel1.Controls.Add(this.optCheque);
            this.panel1.Location = new System.Drawing.Point(104, 92);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(248, 88);
            this.panel1.TabIndex = 21;
            // 
            // txtChequeNumber
            // 
            this.txtChequeNumber.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChequeNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtChequeNumber.Location = new System.Drawing.Point(120, 56);
            this.txtChequeNumber.Name = "txtChequeNumber";
            this.txtChequeNumber.Size = new System.Drawing.Size(120, 22);
            this.txtChequeNumber.TabIndex = 14;
            this.txtChequeNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // optVisa
            // 
            this.optVisa.Checked = true;
            this.optVisa.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optVisa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.optVisa.Location = new System.Drawing.Point(8, 32);
            this.optVisa.Name = "optVisa";
            this.optVisa.Size = new System.Drawing.Size(104, 24);
            this.optVisa.TabIndex = 11;
            this.optVisa.TabStop = true;
            this.optVisa.Text = "Visa";
            // 
            // optMasterCard
            // 
            this.optMasterCard.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optMasterCard.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.optMasterCard.Location = new System.Drawing.Point(8, 8);
            this.optMasterCard.Name = "optMasterCard";
            this.optMasterCard.Size = new System.Drawing.Size(104, 24);
            this.optMasterCard.TabIndex = 10;
            this.optMasterCard.Text = "Master Card";
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Red;
            this.label4.Location = new System.Drawing.Point(88, 59);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(32, 16);
            this.label4.TabIndex = 13;
            this.label4.Text = "No.";
            // 
            // optCheque
            // 
            this.optCheque.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optCheque.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.optCheque.Location = new System.Drawing.Point(8, 55);
            this.optCheque.Name = "optCheque";
            this.optCheque.Size = new System.Drawing.Size(86, 24);
            this.optCheque.TabIndex = 12;
            this.optCheque.Text = "Cheque";
            // 
            // lblAmountDue_Data
            // 
            this.lblAmountDue_Data.BackColor = System.Drawing.Color.White;
            this.lblAmountDue_Data.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lblAmountDue_Data.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAmountDue_Data.Location = new System.Drawing.Point(104, 184);
            this.lblAmountDue_Data.Name = "lblAmountDue_Data";
            this.lblAmountDue_Data.Size = new System.Drawing.Size(112, 21);
            this.lblAmountDue_Data.TabIndex = 80;
            this.lblAmountDue_Data.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtAmountPaid
            // 
            this.txtAmountPaid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtAmountPaid.Location = new System.Drawing.Point(104, 209);
            this.txtAmountPaid.Name = "txtAmountPaid";
            this.txtAmountPaid.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtAmountPaid.Size = new System.Drawing.Size(112, 20);
            this.txtAmountPaid.TabIndex = 11;
            this.txtAmountPaid.Text = "0,00";
            this.txtAmountPaid.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtAmountPaid.OnEnterKeyPress += new PriceTextBox.PriceTextBox.EnterKeyPress(this.txtAmountPaid_OnEnterKeyPress);
            this.txtAmountPaid.Leave += new System.EventHandler(this.txtAmountPaid_Leave);
            // 
            // txtPenalty
            // 
            this.txtPenalty.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.txtPenalty.Location = new System.Drawing.Point(104, 233);
            this.txtPenalty.Name = "txtPenalty";
            this.txtPenalty.Price = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.txtPenalty.Size = new System.Drawing.Size(112, 20);
            this.txtPenalty.TabIndex = 14;
            this.txtPenalty.Text = "0,00";
            this.txtPenalty.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.txtPenalty.OnEnterKeyPress += new PriceTextBox.PriceTextBox.EnterKeyPress(this.txtPenalty_OnEnterKeyPress);
            // 
            // fclsOIAccounting_Pay
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.CancelButton = this.btnDontPay;
            this.ClientSize = new System.Drawing.Size(358, 292);
            this.Controls.Add(this.txtPenalty);
            this.Controls.Add(this.txtAmountPaid);
            this.Controls.Add(this.lblAmountDue_Data);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cmbEmployees);
            this.Controls.Add(this.lblPaidBy);
            this.Controls.Add(this.lblAmountDue);
            this.Controls.Add(this.btnDontPay);
            this.Controls.Add(this.dtpPaymentDate);
            this.Controls.Add(this.lblPenalty);
            this.Controls.Add(this.btnPay);
            this.Controls.Add(this.lblAmountPaid);
            this.Controls.Add(this.lblPaidUsing);
            this.Controls.Add(this.lblPaymentDate);
            this.Controls.Add(this.lblOrderNumber_Data);
            this.Controls.Add(this.lblOrderNumber);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "fclsOIAccounting_Pay";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Quick Stock - Order Payment Information";
            this.Closing += new System.ComponentModel.CancelEventHandler(this.fclsOIAccounting_Pay_Closing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}
		#endregion

		// close with payment
		private void btnPay_Click(object sender, System.EventArgs e)
		{
			DateTime dtPaymentDate;
			decimal decAmoundPaid = 0, decPenalty = 0;
			string strAmoundPaid = "", strPenalty = "", strPaymentMethod = "";
			int intPayerEmplyeeId;

			if(this.cmbEmployees.SelectedIndex != -1)
			{
				// get payment date 
				dtPaymentDate = this.dtpPaymentDate.Value;
				
				// get data from form
				intPayerEmplyeeId = int.Parse(m_dtaEmployees.Rows[this.cmbEmployees.SelectedIndex]["EmployeeId"].ToString());
				strAmoundPaid = this.txtAmountPaid.Text;
				decAmoundPaid = this.txtAmountPaid.Price;
				strPenalty = this.txtPenalty.Text;
				decPenalty = this.txtPenalty.Price;

				// parse the payment method			
				if(this.optMasterCard.Checked)
					strPaymentMethod = "Master Card";
				else if(this.optVisa.Checked)
					strPaymentMethod = "Visa";
				else
					strPaymentMethod = "Cheque No. " + this.txtChequeNumber.Text.ToString();
				
				// mark order as paid
				m_blnIsPaid = true;

				// send payment information back to the calling form
				switch(m_enuCaller)
				{
					case Caller.Accounting:
						((fclsOIAccounting) m_frmOwner).SetPaymentInformation(m_blnIsPaid, dtPaymentDate, strAmoundPaid, strPenalty, strPaymentMethod, intPayerEmplyeeId);
					break;

					case Caller.BackOrder:
						((fclsOMBackOrders) m_frmOwner).SetPaymentInformation(m_blnIsPaid, dtPaymentDate, decAmoundPaid, decPenalty, strPaymentMethod, intPayerEmplyeeId);
					break;

					case Caller.OldOrders:
						((fclsGENOldOrder) m_frmOwner).SetPaymentInformation(m_blnIsPaid, dtPaymentDate, strAmoundPaid, strPenalty, strPaymentMethod, intPayerEmplyeeId);
					break;

					case Caller.OrderCheckIn:
						((fclsOMCheckOrders) m_frmOwner).SetPaymentInformation(dtPaymentDate, decAmoundPaid, decPenalty, strPaymentMethod, intPayerEmplyeeId);
					break;
				}

				this.Close();
			}
			else
				MessageBox.Show("You must first select and employee from the drop-down list!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error,MessageBoxDefaultButton.Button1);
		}

		private void fclsOIAccounting_Pay_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			int intEmplyeeId;

			if(!m_blnIsPaid)
			{
				switch(m_enuCaller)
				{
					case Caller.Accounting:
						((fclsOIAccounting) m_frmOwner).SetPaymentInformation(false, System.DateTime.MinValue, "","","",-1);
					break;

					case Caller.OrderCheckIn:
						if(this.cmbEmployees.SelectedIndex != -1)
						{
							intEmplyeeId = int.Parse(m_dtaEmployees.Rows[this.cmbEmployees.SelectedIndex]["EmployeeId"].ToString());
							((fclsOMCheckOrders) m_frmOwner).SetPaymentInformation(System.DateTime.Now,0,0,"",intEmplyeeId);
						}
						else
						{
							MessageBox.Show("You must first select an employee from the drop-down list!",this.Text,MessageBoxButtons.OK,MessageBoxIcon.Error);
							e.Cancel = true;
						}
					break;
				}
			}
		}

		private void txtChequeNumber_Leave(object sender, System.EventArgs e)
		{
			this.optCheque.Checked = true;
		}

		private void btnDontPay_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

        private void txtAmountPaid_Leave(object sender, EventArgs e)
        {
            if (txtAmountPaid.Price > decimal.Parse(this.lblAmountDue_Data.Text))
            {
                DialogResult dlgResult = MessageBox.Show("The amount paid is larger than the amount due.\nIs this correct?",
                                                         this.Text,
                                                         MessageBoxButtons.YesNo,
                                                         MessageBoxIcon.Question,
                                                         MessageBoxDefaultButton.Button1);
                if (dlgResult == DialogResult.No)
                    this.txtAmountPaid.Select();
            }
        }

        private void txtAmountPaid_OnEnterKeyPress()
        {
            switch (m_enuCaller)
            {
                case Caller.Accounting:
                    this.txtPenalty.Select();
                break;

                case Caller.BackOrder:
                    this.btnPay.Select();
                break;

                case Caller.OldOrders:
                    this.txtPenalty.Select();
                break;

                case Caller.OrderCheckIn:
                    this.btnPay.Select();
                break;
            }
        }

        private void txtPenalty_OnEnterKeyPress()
        {
            switch (m_enuCaller)
            {
                case Caller.Accounting:
                    this.btnPay.Select();
                break;

                case Caller.OldOrders:
                    this.btnPay.Select();
                break;
            }
        }
	}
}
