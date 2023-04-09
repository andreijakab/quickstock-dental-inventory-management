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
	/// Summary description for fclsGENBoCancel.
	/// </summary>
	public class fclsGENBoCancel : System.Windows.Forms.Form
	{
		public System.Windows.Forms.Label lblProdName;
		private System.Windows.Forms.ComboBox cmbEmployee;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label1;
		public System.Windows.Forms.Label lblPhone;
		private System.Windows.Forms.Label lblContact;
		public System.Windows.Forms.Label lblSupplier;
		private System.Windows.Forms.Label label3;
		public System.Windows.Forms.Label lblFax;
		private System.Windows.Forms.Label label7;
		public System.Windows.Forms.Label lblEmail;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.Button btnCancelBO;
		private System.Windows.Forms.Label label9;
		private System.Windows.Forms.Label label11;
		private System.Windows.Forms.Label label13;
		private System.Windows.Forms.Label label15;
		private System.Windows.Forms.Label label17;
		private System.Windows.Forms.Label label19;
		public System.Windows.Forms.Label lblUnitsBackorder;
		public System.Windows.Forms.Label lblOrderId;
		public System.Windows.Forms.Label lblOrderDate;
		public System.Windows.Forms.Label lblUnitsOrdered;
		public System.Windows.Forms.Label lblLastUpdate;
		public System.Windows.Forms.Label lblReceivedDate;
		public System.Windows.Forms.Label lblUnitsReceived;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox txtText;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		private OleDbConnection		m_odcConnection;
		private static string		m_strSuplId, m_strOrderId, m_strSubPrId;
		
		public fclsGENBoCancel(OleDbConnection odcConnection)
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();
			
			m_odcConnection = odcConnection;
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
			this.lblProdName = new System.Windows.Forms.Label();
			this.cmbEmployee = new System.Windows.Forms.ComboBox();
			this.label5 = new System.Windows.Forms.Label();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.lblPhone = new System.Windows.Forms.Label();
			this.lblContact = new System.Windows.Forms.Label();
			this.lblSupplier = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.lblFax = new System.Windows.Forms.Label();
			this.label7 = new System.Windows.Forms.Label();
			this.lblEmail = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.lblUnitsBackorder = new System.Windows.Forms.Label();
			this.label6 = new System.Windows.Forms.Label();
			this.btnCancelBO = new System.Windows.Forms.Button();
			this.lblOrderId = new System.Windows.Forms.Label();
			this.label9 = new System.Windows.Forms.Label();
			this.lblOrderDate = new System.Windows.Forms.Label();
			this.label11 = new System.Windows.Forms.Label();
			this.lblUnitsOrdered = new System.Windows.Forms.Label();
			this.label13 = new System.Windows.Forms.Label();
			this.lblLastUpdate = new System.Windows.Forms.Label();
			this.label15 = new System.Windows.Forms.Label();
			this.lblReceivedDate = new System.Windows.Forms.Label();
			this.label17 = new System.Windows.Forms.Label();
			this.lblUnitsReceived = new System.Windows.Forms.Label();
			this.label19 = new System.Windows.Forms.Label();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.txtText = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.SuspendLayout();
			// 
			// lblProdName
			// 
			this.lblProdName.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProdName.ForeColor = System.Drawing.Color.Green;
			this.lblProdName.Location = new System.Drawing.Point(96, 8);
			this.lblProdName.Name = "lblProdName";
			this.lblProdName.Size = new System.Drawing.Size(304, 40);
			this.lblProdName.TabIndex = 7;
			this.lblProdName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// cmbEmployee
			// 
			this.cmbEmployee.Location = new System.Drawing.Point(176, 480);
			this.cmbEmployee.Name = "cmbEmployee";
			this.cmbEmployee.Size = new System.Drawing.Size(180, 21);
			this.cmbEmployee.TabIndex = 15;
			// 
			// label5
			// 
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label5.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.label5.Location = new System.Drawing.Point(72, 480);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(88, 16);
			this.label5.TabIndex = 19;
			this.label5.Text = "Canceled by                     ";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox1
			// 
			this.groupBox1.BackColor = System.Drawing.Color.Linen;
			this.groupBox1.Controls.Add(this.label7);
			this.groupBox1.Controls.Add(this.lblEmail);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.lblFax);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.lblPhone);
			this.groupBox1.Controls.Add(this.lblContact);
			this.groupBox1.Controls.Add(this.lblSupplier);
			this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox1.ForeColor = System.Drawing.Color.Red;
			this.groupBox1.Location = new System.Drawing.Point(32, 288);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(424, 184);
			this.groupBox1.TabIndex = 21;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Supplier Info";
			// 
			// label2
			// 
			this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label2.ForeColor = System.Drawing.Color.Red;
			this.label2.Location = new System.Drawing.Point(32, 88);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(96, 16);
			this.label2.TabIndex = 26;
			this.label2.Text = "Phone                             ";
			this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label4
			// 
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label4.ForeColor = System.Drawing.Color.Red;
			this.label4.Location = new System.Drawing.Point(32, 24);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 16);
			this.label4.TabIndex = 25;
			this.label4.Text = "Supplier                             ";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// label1
			// 
			this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.Red;
			this.label1.Location = new System.Drawing.Point(32, 56);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(96, 16);
			this.label1.TabIndex = 24;
			this.label1.Text = "Contact                             ";
			this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblPhone
			// 
			this.lblPhone.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblPhone.Location = new System.Drawing.Point(144, 88);
			this.lblPhone.Name = "lblPhone";
			this.lblPhone.Size = new System.Drawing.Size(180, 21);
			this.lblPhone.TabIndex = 21;
			// 
			// lblContact
			// 
			this.lblContact.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblContact.Location = new System.Drawing.Point(144, 56);
			this.lblContact.Name = "lblContact";
			this.lblContact.Size = new System.Drawing.Size(180, 21);
			this.lblContact.TabIndex = 22;
			// 
			// lblSupplier
			// 
			this.lblSupplier.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblSupplier.Location = new System.Drawing.Point(144, 24);
			this.lblSupplier.Name = "lblSupplier";
			this.lblSupplier.Size = new System.Drawing.Size(180, 21);
			this.lblSupplier.TabIndex = 23;
			// 
			// label3
			// 
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label3.ForeColor = System.Drawing.Color.Red;
			this.label3.Location = new System.Drawing.Point(32, 120);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(96, 16);
			this.label3.TabIndex = 28;
			this.label3.Text = "Fax                     ";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblFax
			// 
			this.lblFax.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblFax.Location = new System.Drawing.Point(144, 120);
			this.lblFax.Name = "lblFax";
			this.lblFax.Size = new System.Drawing.Size(180, 21);
			this.lblFax.TabIndex = 27;
			// 
			// label7
			// 
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label7.ForeColor = System.Drawing.Color.Red;
			this.label7.Location = new System.Drawing.Point(32, 152);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(96, 16);
			this.label7.TabIndex = 30;
			this.label7.Text = "Email";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblEmail
			// 
			this.lblEmail.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblEmail.Location = new System.Drawing.Point(144, 152);
			this.lblEmail.Name = "lblEmail";
			this.lblEmail.Size = new System.Drawing.Size(180, 21);
			this.lblEmail.TabIndex = 29;
			// 
			// groupBox2
			// 
			this.groupBox2.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.groupBox2.Controls.Add(this.lblLastUpdate);
			this.groupBox2.Controls.Add(this.label15);
			this.groupBox2.Controls.Add(this.lblReceivedDate);
			this.groupBox2.Controls.Add(this.label17);
			this.groupBox2.Controls.Add(this.lblUnitsReceived);
			this.groupBox2.Controls.Add(this.label19);
			this.groupBox2.Controls.Add(this.lblUnitsOrdered);
			this.groupBox2.Controls.Add(this.label13);
			this.groupBox2.Controls.Add(this.lblOrderDate);
			this.groupBox2.Controls.Add(this.label11);
			this.groupBox2.Controls.Add(this.lblOrderId);
			this.groupBox2.Controls.Add(this.label9);
			this.groupBox2.Controls.Add(this.lblUnitsBackorder);
			this.groupBox2.Controls.Add(this.label6);
			this.groupBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.groupBox2.ForeColor = System.Drawing.Color.Blue;
			this.groupBox2.Location = new System.Drawing.Point(32, 48);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(272, 240);
			this.groupBox2.TabIndex = 22;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Order Info";
			// 
			// lblUnitsBackorder
			// 
			this.lblUnitsBackorder.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblUnitsBackorder.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUnitsBackorder.ForeColor = System.Drawing.Color.Blue;
			this.lblUnitsBackorder.Location = new System.Drawing.Point(144, 208);
			this.lblUnitsBackorder.Name = "lblUnitsBackorder";
			this.lblUnitsBackorder.Size = new System.Drawing.Size(104, 24);
			this.lblUnitsBackorder.TabIndex = 10;
			this.lblUnitsBackorder.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label6
			// 
			this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label6.ForeColor = System.Drawing.Color.Blue;
			this.label6.Location = new System.Drawing.Point(0, 208);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(128, 16);
			this.label6.TabIndex = 9;
			this.label6.Text = "Units in Backorder            ";
			this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// btnCancelBO
			// 
			this.btnCancelBO.Location = new System.Drawing.Point(384, 480);
			this.btnCancelBO.Name = "btnCancelBO";
			this.btnCancelBO.Size = new System.Drawing.Size(96, 32);
			this.btnCancelBO.TabIndex = 23;
			this.btnCancelBO.Text = "Cancel this Backorder";
			this.btnCancelBO.Click += new System.EventHandler(this.btnCancelBO_Click);
			// 
			// lblOrderId
			// 
			this.lblOrderId.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblOrderId.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblOrderId.ForeColor = System.Drawing.Color.Blue;
			this.lblOrderId.Location = new System.Drawing.Point(144, 16);
			this.lblOrderId.Name = "lblOrderId";
			this.lblOrderId.Size = new System.Drawing.Size(104, 24);
			this.lblOrderId.TabIndex = 12;
			this.lblOrderId.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label9
			// 
			this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label9.ForeColor = System.Drawing.Color.Blue;
			this.label9.Location = new System.Drawing.Point(16, 16);
			this.label9.Name = "label9";
			this.label9.Size = new System.Drawing.Size(112, 16);
			this.label9.TabIndex = 11;
			this.label9.Text = "Order Nr.";
			this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblOrderDate
			// 
			this.lblOrderDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblOrderDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblOrderDate.ForeColor = System.Drawing.Color.Blue;
			this.lblOrderDate.Location = new System.Drawing.Point(144, 48);
			this.lblOrderDate.Name = "lblOrderDate";
			this.lblOrderDate.Size = new System.Drawing.Size(104, 24);
			this.lblOrderDate.TabIndex = 14;
			this.lblOrderDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label11
			// 
			this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label11.ForeColor = System.Drawing.Color.Blue;
			this.label11.Location = new System.Drawing.Point(16, 48);
			this.label11.Name = "label11";
			this.label11.Size = new System.Drawing.Size(112, 16);
			this.label11.TabIndex = 13;
			this.label11.Text = "Order Date            ";
			this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblUnitsOrdered
			// 
			this.lblUnitsOrdered.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblUnitsOrdered.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUnitsOrdered.ForeColor = System.Drawing.Color.Blue;
			this.lblUnitsOrdered.Location = new System.Drawing.Point(144, 80);
			this.lblUnitsOrdered.Name = "lblUnitsOrdered";
			this.lblUnitsOrdered.Size = new System.Drawing.Size(104, 24);
			this.lblUnitsOrdered.TabIndex = 16;
			this.lblUnitsOrdered.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label13
			// 
			this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label13.ForeColor = System.Drawing.Color.Blue;
			this.label13.Location = new System.Drawing.Point(16, 80);
			this.label13.Name = "label13";
			this.label13.Size = new System.Drawing.Size(112, 16);
			this.label13.TabIndex = 15;
			this.label13.Text = "Units Ordered";
			this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblLastUpdate
			// 
			this.lblLastUpdate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblLastUpdate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblLastUpdate.ForeColor = System.Drawing.Color.Blue;
			this.lblLastUpdate.Location = new System.Drawing.Point(144, 176);
			this.lblLastUpdate.Name = "lblLastUpdate";
			this.lblLastUpdate.Size = new System.Drawing.Size(104, 24);
			this.lblLastUpdate.TabIndex = 22;
			this.lblLastUpdate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label15
			// 
			this.label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label15.ForeColor = System.Drawing.Color.Blue;
			this.label15.Location = new System.Drawing.Point(16, 176);
			this.label15.Name = "label15";
			this.label15.Size = new System.Drawing.Size(112, 16);
			this.label15.TabIndex = 21;
			this.label15.Text = "Last Update                 ";
			this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblReceivedDate
			// 
			this.lblReceivedDate.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblReceivedDate.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblReceivedDate.ForeColor = System.Drawing.Color.Blue;
			this.lblReceivedDate.Location = new System.Drawing.Point(144, 144);
			this.lblReceivedDate.Name = "lblReceivedDate";
			this.lblReceivedDate.Size = new System.Drawing.Size(104, 24);
			this.lblReceivedDate.TabIndex = 20;
			this.lblReceivedDate.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label17
			// 
			this.label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label17.ForeColor = System.Drawing.Color.Blue;
			this.label17.Location = new System.Drawing.Point(16, 144);
			this.label17.Name = "label17";
			this.label17.Size = new System.Drawing.Size(112, 16);
			this.label17.TabIndex = 19;
			this.label17.Text = "Received Date";
			this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// lblUnitsReceived
			// 
			this.lblUnitsReceived.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.lblUnitsReceived.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUnitsReceived.ForeColor = System.Drawing.Color.Blue;
			this.lblUnitsReceived.Location = new System.Drawing.Point(144, 112);
			this.lblUnitsReceived.Name = "lblUnitsReceived";
			this.lblUnitsReceived.Size = new System.Drawing.Size(104, 24);
			this.lblUnitsReceived.TabIndex = 18;
			this.lblUnitsReceived.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			// 
			// label19
			// 
			this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.label19.ForeColor = System.Drawing.Color.Blue;
			this.label19.Location = new System.Drawing.Point(16, 112);
			this.label19.Name = "label19";
			this.label19.Size = new System.Drawing.Size(112, 16);
			this.label19.TabIndex = 17;
			this.label19.Text = "Units Received";
			this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// groupBox3
			// 
			this.groupBox3.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.groupBox3.Controls.Add(this.txtText);
			this.groupBox3.Location = new System.Drawing.Point(304, 48);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(152, 240);
			this.groupBox3.TabIndex = 24;
			this.groupBox3.TabStop = false;
			// 
			// txtText
			// 
			this.txtText.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(192)), ((System.Byte)(255)), ((System.Byte)(255)));
			this.txtText.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, ((System.Drawing.FontStyle)((System.Drawing.FontStyle.Bold | System.Drawing.FontStyle.Italic))), System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.txtText.Location = new System.Drawing.Point(8, 16);
			this.txtText.Multiline = true;
			this.txtText.Name = "txtText";
			this.txtText.ReadOnly = true;
			this.txtText.Size = new System.Drawing.Size(136, 216);
			this.txtText.TabIndex = 0;
			this.txtText.Text = "";
			// 
			// fclsGENBoCancel
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(488, 518);
			this.Controls.Add(this.groupBox3);
			this.Controls.Add(this.btnCancelBO);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.cmbEmployee);
			this.Controls.Add(this.lblProdName);
			this.Name = "fclsGENBoCancel";
			this.Text = "Quick Stock - Backorder behind schedule more than 30 days";
			this.Load += new System.EventHandler(this.fclsGENBoCancel_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.ResumeLayout(false);

		}
		#endregion

		private void fclsGENBoCancel_Load(object sender, System.EventArgs e)
		{
			this.txtText.Text = "If you will cancel this Backorder click on \"Cancel this Backorder\" button. " +
				"\n If you do not will cancel this Backorder close the Window.";
			
			OleDbDataAdapter	employeDataAdapter = new OleDbDataAdapter("Select * From [Employees] Order by FirstName, LastName", m_odcConnection);
			DataTable			m_dtEmploye = new DataTable("Employees");
			// Open the table Employees
			employeDataAdapter.Fill(m_dtEmploye);
			int nrEmploye = m_dtEmploye.Rows.Count;
			DataRow	m_drEmploye;
		
			string Employe;
			System.Object[]	ItemObject = new System.Object[nrEmploye];			
			for (int i = 0; i <nrEmploye; i++)
			{
				m_drEmploye = m_dtEmploye.Rows[i];
				Employe = m_drEmploye["Title"].ToString() + " " + m_drEmploye["FirstName"].ToString() + ", " + m_drEmploye["LastName"].ToString();
				ItemObject[i] = Employe;
			}
			this.cmbEmployee.Items.AddRange(ItemObject);
            int m_intEmpl = clsConfiguration.Internal_CurrentUserID;
			this.cmbEmployee.SelectedIndex = clsUtilities.GetListIDfromDatabaseID(m_intEmpl, m_dtEmploye, 0);

			int m_intSuplId = int.Parse(m_strSuplId);
			OleDbDataAdapter	suplierDataAdapter = new OleDbDataAdapter("Select * From [Suppliers] Where FournisseurId=" + m_strSuplId, m_odcConnection);
			DataTable			m_dtSupplier = new DataTable("Suppliers");
			// Open the table Employees
			suplierDataAdapter.Fill(m_dtSupplier);
			this.lblSupplier.Text = m_dtSupplier.Rows[0]["CompanyName"].ToString();
			this.lblContact.Text = m_dtSupplier.Rows[0]["ConTitle"].ToString() + " " + m_dtSupplier.Rows[0]["ContactFirstName"].ToString() + 
				" " + m_dtSupplier.Rows[0]["ContactLastName"].ToString();
			this.lblPhone.Text = m_dtSupplier.Rows[0]["PhoneNumber"].ToString();
			this.lblFax.Text = m_dtSupplier.Rows[0]["FaxNumber"].ToString();
			this.lblEmail.Text = m_dtSupplier.Rows[0]["Email"].ToString();

			int m_intSubPrId = int.Parse(m_strSubPrId);
			OleDbDataAdapter	orderDataAdapter = new OleDbDataAdapter("Select Orders.*, Products.MatName, SubProducts.MatName " +
				"FROM (Orders INNER JOIN Products ON Orders.MatId = Products.MatId) INNER JOIN SubProducts" +
				" ON (SubProducts.SubPrId = Orders.SubPrId) AND (Products.MatId = SubProducts.MatId)" +
				"WHERE (((Orders.OrderId)='" + m_strOrderId + "') AND ((Orders.SubPrId)=" + m_intSubPrId + "))", m_odcConnection);
			DataTable			m_dtOrder = new DataTable("Orders");
			// Open the table Employees
			orderDataAdapter.Fill(m_dtOrder);
			this.lblProdName.Text = m_dtOrder.Rows[0]["Products.MatName"].ToString() + "\n" +
				m_dtOrder.Rows[0]["SubProducts.MatName"].ToString();
			this.lblOrderId.Text = m_strOrderId;
			this.lblOrderDate.Text = ((DateTime) m_dtOrder.Rows[0]["OrderDate"]).ToString("MMM dd, yyyy");
			this.lblUnitsOrdered.Text = m_dtOrder.Rows[0]["OrderQty"].ToString();
			this.lblUnitsReceived.Text = m_dtOrder.Rows[0]["ReceivedQty"].ToString();
			this.lblReceivedDate.Text = "";
			string m_strDate = m_dtOrder.Rows[0]["CheckDate"].ToString();
			if((string.Compare(m_strDate, "0",false)) > 0)
				this.lblReceivedDate.Text = ((DateTime) m_dtOrder.Rows[0]["CheckDate"]).ToString("MMM dd, yyyy");
			this.lblLastUpdate.Text = ((DateTime) m_dtOrder.Rows[0]["BackOrderUpdateDate"]).ToString("MMM dd, yyyy");
			this.lblUnitsBackorder.Text = m_dtOrder.Rows[0]["BackOrderUnits"].ToString();
		}

		public static void SetInfoValues(string SuplId, string orderId, string subPrId)
		{
			m_strSuplId = SuplId;
			m_strOrderId = orderId;
			m_strSubPrId = subPrId;
		}

		private void btnCancelBO_Click(object sender, System.EventArgs e)
		{
			//this.txtText.Visible = false;
			this.Close();
		}

	}
}
