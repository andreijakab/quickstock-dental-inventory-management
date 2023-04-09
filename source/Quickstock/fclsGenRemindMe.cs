using System;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Globalization;
using System.Windows.Forms;


namespace DSMS
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class fclsGenRemindMe : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnOK;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.ComboBox cmbRemindMe;
		private System.Windows.Forms.ListView lstOrders;
		private System.Windows.Forms.ColumnHeader orderId;
		private System.Windows.Forms.ColumnHeader orderDate;
		private System.Windows.Forms.ColumnHeader sumDue;

        public enum ReminderType : int { Backorder = 2, LateOrder = 0, UnpaidOrder = 1, UnsentReturnedProducts = 3 }
		
        private DataTable			m_dtaSO, m_dtaOP, m_dtaBO, m_dtaRetProd;
        private fclsGENInput        m_frmGENInput;
        private NumberFormatInfo    m_nfiLocalNumberFormat;
        private OleDbConnection     m_odcConnection;
        private ReminderType        m_rtReminderType;
		
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
        
        public fclsGenRemindMe(DSMS.fclsGENInput frmGENInput, ReminderType rtReminderType, OleDbConnection odcConnection)
		{
			InitializeComponent();
			
            // store variable value
            m_frmGENInput = frmGENInput;
			m_odcConnection = odcConnection;
            m_rtReminderType = rtReminderType;

            // configure currency formatting
            CultureInfo ciCurrentCulture = (CultureInfo)System.Globalization.CultureInfo.CurrentCulture.Clone();
            m_nfiLocalNumberFormat = ciCurrentCulture.NumberFormat;
            m_nfiLocalNumberFormat.CurrencySymbol = "";

            this.cmbRemindMe.SelectedIndex = 1;
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
            this.btnOK = new System.Windows.Forms.Button();
            this.cmbRemindMe = new System.Windows.Forms.ComboBox();
            this.lstOrders = new System.Windows.Forms.ListView();
            this.orderId = new System.Windows.Forms.ColumnHeader();
            this.orderDate = new System.Windows.Forms.ColumnHeader();
            this.sumDue = new System.Windows.Forms.ColumnHeader();
            this.btnHelp = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(8, 132);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(96, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Remind me in ";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Red;
            this.label2.Location = new System.Drawing.Point(160, 132);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 16);
            this.label2.TabIndex = 1;
            this.label2.Text = "day(s)";
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOK.ForeColor = System.Drawing.Color.Green;
            this.btnOK.Location = new System.Drawing.Point(272, 128);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(56, 24);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "OK";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // cmbRemindMe
            // 
            this.cmbRemindMe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbRemindMe.Items.AddRange(new object[] {
            "  0",
            "  1",
            "  2",
            "  3",
            "  4",
            "  5",
            "10",
            "15",
            "20",
            "30",
            "45",
            "60",
            "75",
            "90"});
            this.cmbRemindMe.Location = new System.Drawing.Point(104, 130);
            this.cmbRemindMe.Name = "cmbRemindMe";
            this.cmbRemindMe.Size = new System.Drawing.Size(56, 21);
            this.cmbRemindMe.TabIndex = 4;
            // 
            // lstOrders
            // 
            this.lstOrders.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.orderId,
            this.orderDate,
            this.sumDue});
            this.lstOrders.FullRowSelect = true;
            this.lstOrders.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstOrders.HideSelection = false;
            this.lstOrders.Location = new System.Drawing.Point(8, 16);
            this.lstOrders.MultiSelect = false;
            this.lstOrders.Name = "lstOrders";
            this.lstOrders.Size = new System.Drawing.Size(312, 104);
            this.lstOrders.TabIndex = 5;
            this.lstOrders.UseCompatibleStateImageBehavior = false;
            this.lstOrders.View = System.Windows.Forms.View.Details;
            this.lstOrders.Click += new System.EventHandler(this.lstOrders_Click);
            // 
            // orderId
            // 
            this.orderId.Text = "Order #";
            this.orderId.Width = 65;
            // 
            // orderDate
            // 
            this.orderDate.Text = "Order Date";
            this.orderDate.Width = 87;
            // 
            // sumDue
            // 
            this.sumDue.Text = "Sum Due";
            this.sumDue.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.sumDue.Width = 133;
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(208, 128);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(56, 24);
            this.btnHelp.TabIndex = 6;
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // fclsGenRemindMe
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(336, 158);
            this.ControlBox = false;
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.lstOrders);
            this.Controls.Add(this.cmbRemindMe);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "fclsGenRemindMe";
            this.Text = "Quick Stock - Remind Me";
            this.Load += new System.EventHandler(this.fclsGenRemindMe_Load);
            this.ResumeLayout(false);

		}
		#endregion

		private void btnOK_Click(object sender, System.EventArgs e)
		{
			OleDbDataAdapter odaRemindMe = new OleDbDataAdapter("SELECT *  FROM [RemindMe]", m_odcConnection);
            OleDbCommandBuilder ocbSaveRemindMe = new OleDbCommandBuilder(odaRemindMe);
			DataTable dtaRemindMe = new DataTable("RemindMe");
            odaRemindMe.Fill(dtaRemindMe);

            DataRow updateRow = dtaRemindMe.Rows[(int) m_rtReminderType];
			updateRow.BeginEdit();
			updateRow["Days"] = int.Parse(this.cmbRemindMe.SelectedItem.ToString());
			updateRow["rmDate"] = DateTime.Now.ToShortDateString();
			updateRow.EndEdit();
			
            try
			{
                odaRemindMe.Update(dtaRemindMe);
                dtaRemindMe.AcceptChanges();
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
			}

            if(int.Parse(this.cmbRemindMe.SelectedItem.ToString()) > 0)
            {
                switch (m_rtReminderType)
                {
                    case ReminderType.LateOrder:
                        m_frmGENInput.SetEnabledChange(0, false);
                    break;

                    case ReminderType.UnpaidOrder:
                        m_frmGENInput.SetEnabledChange(1, false);
                    break;

                    case ReminderType.Backorder:
                        m_frmGENInput.SetEnabledChange(2, false);
                    break;

                    case ReminderType.UnsentReturnedProducts:
                        m_frmGENInput.SetEnabledChange(3, false);
                    break;
                }
            }
			
            this.Close();
		}

		private void fclsGenRemindMe_Load(object sender, System.EventArgs e)
		{
			double dSomDue;
            OleDbDataAdapter odaRemindMe = new OleDbDataAdapter("SELECT * FROM [RemindMe] WHERE [Code] = " + (((int) m_rtReminderType).ToString()), m_odcConnection);
			DataTable dtaRemindMe = new DataTable();
            odaRemindMe.Fill(dtaRemindMe);

            this.cmbRemindMe.SelectedIndex = Utilities.clsUtilities.FindItemIndex(dtaRemindMe.Rows[0]["Days"].ToString(),
                                                                                  this.cmbRemindMe);
                        
            // customize form & load data
            switch(m_rtReminderType)
			{
				case ReminderType.LateOrder:
					// customize form
                    this.lstOrders.Columns[1].Text = "Order Date";
					this.lstOrders.Columns[2].Text = "";
                    this.Text = "Sent Orders";

                    // load data from db
					OleDbDataAdapter odaSO = new OleDbDataAdapter("SELECT DISTINCT OrderId, OrderDate " +
                                                                  "FROM [Orders] " +
                                                                  "WHERE [Checked] = 0 " + 
                                                                  "ORDER BY OrderDate", m_odcConnection);
					m_dtaSO = new DataTable();
					odaSO.Fill(m_dtaSO);
					lstOrders.Items.Clear();

                    // display data
					ListViewItem lviItem;
					for (int i = 0; i < m_dtaSO.Rows.Count; i++)
					{
						lviItem = lstOrders.Items.Add(m_dtaSO.Rows[i]["OrderId"].ToString());
						lviItem.SubItems.Add(((DateTime) m_dtaSO.Rows[i]["OrderDate"]).ToString(Utilities.clsUtilities.FORMAT_DATE_ORDERED));	
					}
				break;
				
                case ReminderType.UnpaidOrder:
                    // customize form
                    this.Text = "Order Payment";
					this.lstOrders.Columns[1].Text = "Received Date";
					this.lstOrders.Columns[2].Text = "Amount Due";
                    
                    // load data from db
                    OleDbDataAdapter m_odaOP = new OleDbDataAdapter("SELECT DISTINCT OrderId, PaymentDate, SumDue " +
                                                                    "FROM [OrderPayment] " +
                                                                    "WHERE [checkPayment] = 0 " +
                                                                    "ORDER BY PaymentDate", m_odcConnection);
					m_dtaOP = new DataTable();
					m_odaOP.Fill(m_dtaOP);
					lstOrders.Items.Clear();
                    
                    // display data
					for (int i = 0; i < m_dtaOP.Rows.Count; i++)
					{
						lviItem = lstOrders.Items.Add(m_dtaOP.Rows[i]["OrderId"].ToString());
                        lviItem.SubItems.Add(((DateTime)m_dtaOP.Rows[i]["PaymentDate"]).ToString(Utilities.clsUtilities.FORMAT_DATE_ORDERED));	
						dSomDue = double.Parse(m_dtaOP.Rows[i]["SumDue"].ToString());
						lviItem.SubItems.Add(dSomDue.ToString("C", m_nfiLocalNumberFormat));
					}
				break;
				
                case ReminderType.Backorder:
                    // customize form
                    this.Text = "BackOrders";
					this.lstOrders.Columns[1].Text = "Order Date";
					this.lstOrders.Columns[2].Text = "# Days Backordered";

                    // load data from db
                    OleDbDataAdapter m_odaBO = new OleDbDataAdapter("SELECT DISTINCT OrderId, OrderDate " +
                                                                    "FROM [Orders] " + 
                                                                    "WHERE [BackOrderUnits] > 0 " +
                                                                    "ORDER BY OrderDate", m_odcConnection);
					m_dtaBO = new DataTable();
					m_odaBO.Fill(m_dtaBO);
					lstOrders.Items.Clear();

                    // display data
					for (int i = 0; i < m_dtaBO.Rows.Count; i++)
					{
						lviItem = lstOrders.Items.Add(m_dtaBO.Rows[i]["OrderId"].ToString());
                        
                        DateTime dtOrderDate = (DateTime)m_dtaBO.Rows[i]["OrderDate"];
                        lviItem.SubItems.Add(dtOrderDate.ToString(Utilities.clsUtilities.FORMAT_DATE_ORDERED));

                        lviItem.SubItems.Add(DateTime.Now.Subtract(dtOrderDate).Days.ToString());
					}
				break;
				
                case ReminderType.UnsentReturnedProducts:
                    // customize form
                    this.Text = "Returned Products";
					this.lstOrders.Columns[1].Text = "Received Date";
					this.lstOrders.Columns[2].Text = "";

                    // load data from db
                    OleDbDataAdapter m_odaRetProd = new OleDbDataAdapter("SELECT DISTINCT OrderId, CheckDate " +
                                                                         "FROM [Orders] " +
                                                                         "WHERE [ReturnNumber] = '0' " + 
                                                                         "ORDER BY CheckDate", m_odcConnection);
					m_dtaRetProd = new DataTable();
					m_odaRetProd.Fill(m_dtaRetProd);
					lstOrders.Items.Clear();

                    // display data
//					ListViewItem lviItem;
					for (int i = 0; i < m_dtaRetProd.Rows.Count; i++)
					{
						lviItem = lstOrders.Items.Add(m_dtaRetProd.Rows[i]["OrderId"].ToString());
                        lviItem.SubItems.Add(((DateTime)m_dtaRetProd.Rows[i]["CheckDate"]).ToString(Utilities.clsUtilities.FORMAT_DATE_ORDERED));
					}
				break;
			}
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			string htmFile = "";
			switch(m_rtReminderType)
			{
				case ReminderType.LateOrder:
					htmFile = "StillSentOrders.htm";
				break;
				
                case ReminderType.UnpaidOrder:
					htmFile = "StillPayment.htm";
				break;
				
                case ReminderType.Backorder:
					htmFile = "StillBackorders.htm";
				break;
				
                case ReminderType.UnsentReturnedProducts:
					htmFile = "StillReturnProduct.htm";
				break;
			}
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm",htmFile);  

		}

		private void lstOrders_Click(object sender, System.EventArgs e)
		{
			int oldYear = int.Parse(DateTime.Now.ToString("yyyy"));
			string m_strOrderId;
			ListView.SelectedIndexCollection index = lstOrders.SelectedIndices;
			foreach(int m_int_clickedOrder in index)
			{
				switch(m_rtReminderType)
				{
					case ReminderType.LateOrder:
						m_strOrderId = m_dtaSO.Rows[m_int_clickedOrder]["OrderId"].ToString();
						fclsOMCheckOrders frmOMCheckOrders = new fclsOMCheckOrders(m_strOrderId, m_odcConnection);
						frmOMCheckOrders.ShowDialog();		
					break;

					case ReminderType.UnpaidOrder:
						m_strOrderId = m_dtaOP.Rows[m_int_clickedOrder]["OrderId"].ToString();
						fclsOIAccounting frmOIAccounting = new fclsOIAccounting(fclsOIAccounting.FilterType.PaymentHistory, m_strOrderId, m_odcConnection);
						frmOIAccounting.ShowDialog();
					break;

					case ReminderType.Backorder:
						m_strOrderId = m_dtaBO.Rows[m_int_clickedOrder]["OrderId"].ToString();
						fclsOMBackOrders frmOMBackOrders = new fclsOMBackOrders(m_strOrderId, m_odcConnection);
						frmOMBackOrders.ShowDialog();		
					break;

					case ReminderType.UnsentReturnedProducts:
						m_strOrderId = m_dtaRetProd.Rows[m_int_clickedOrder]["OrderId"].ToString();
                        fclsOIViewOrders frmOIViewOrders = new fclsOIViewOrders(m_odcConnection, fclsOIViewOrders.ViewOrdersType.ReturnedOrders_ReadOnly, m_strOrderId);
						frmOIViewOrders.ShowDialog();
					break;
				}
			}
		}
	}
}
