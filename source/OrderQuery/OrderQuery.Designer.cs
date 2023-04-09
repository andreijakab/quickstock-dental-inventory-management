namespace OrderQuery
{
    partial class OrderQuery
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.scCriteriaFields = new System.Windows.Forms.SplitContainer();
            this.lblNoOrdersFound = new System.Windows.Forms.Label();
            this.lbxOrderNumber = new System.Windows.Forms.ListBox();
            this.cmbOrderedBy = new System.Windows.Forms.ComboBox();
            this.optOrderedBy = new System.Windows.Forms.RadioButton();
            this.optOrderNumber = new System.Windows.Forms.RadioButton();
            this.cmbSupplier = new System.Windows.Forms.ComboBox();
            this.optSupplier = new System.Windows.Forms.RadioButton();
            this.lblTimePeriod = new System.Windows.Forms.Label();
            this.lblTimePeriod_Data = new System.Windows.Forms.Label();
            this.cmbTimePeriod = new System.Windows.Forms.ComboBox();
            this.scCriteriaFields.Panel1.SuspendLayout();
            this.scCriteriaFields.Panel2.SuspendLayout();
            this.scCriteriaFields.SuspendLayout();
            this.SuspendLayout();
            // 
            // scCriteriaFields
            // 
            this.scCriteriaFields.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.scCriteriaFields.Location = new System.Drawing.Point(3, 3);
            this.scCriteriaFields.Name = "scCriteriaFields";
            // 
            // scCriteriaFields.Panel1
            // 
            this.scCriteriaFields.Panel1.Controls.Add(this.lblNoOrdersFound);
            this.scCriteriaFields.Panel1.Controls.Add(this.lbxOrderNumber);
            this.scCriteriaFields.Panel1.Controls.Add(this.cmbOrderedBy);
            this.scCriteriaFields.Panel1.Controls.Add(this.optOrderedBy);
            this.scCriteriaFields.Panel1.Controls.Add(this.optOrderNumber);
            this.scCriteriaFields.Panel1.Controls.Add(this.cmbSupplier);
            this.scCriteriaFields.Panel1.Controls.Add(this.optSupplier);
            // 
            // scCriteriaFields.Panel2
            // 
            this.scCriteriaFields.Panel2.Controls.Add(this.lblTimePeriod);
            this.scCriteriaFields.Panel2.Controls.Add(this.lblTimePeriod_Data);
            this.scCriteriaFields.Panel2.Controls.Add(this.cmbTimePeriod);
            this.scCriteriaFields.Size = new System.Drawing.Size(850, 93);
            this.scCriteriaFields.SplitterDistance = 682;
            this.scCriteriaFields.TabIndex = 19;
            // 
            // lblNoOrdersFound
            // 
            this.lblNoOrdersFound.AutoSize = true;
            this.lblNoOrdersFound.BackColor = System.Drawing.SystemColors.Window;
            this.lblNoOrdersFound.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoOrdersFound.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.lblNoOrdersFound.Location = new System.Drawing.Point(127, 31);
            this.lblNoOrdersFound.Name = "lblNoOrdersFound";
            this.lblNoOrdersFound.Size = new System.Drawing.Size(146, 20);
            this.lblNoOrdersFound.TabIndex = 27;
            this.lblNoOrdersFound.Text = "No Orders Found";
            // 
            // lbxOrderNumber
            // 
            this.lbxOrderNumber.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbxOrderNumber.ItemHeight = 16;
            this.lbxOrderNumber.Location = new System.Drawing.Point(121, 3);
            this.lbxOrderNumber.Name = "lbxOrderNumber";
            this.lbxOrderNumber.Size = new System.Drawing.Size(160, 84);
            this.lbxOrderNumber.TabIndex = 26;
            this.lbxOrderNumber.SelectedIndexChanged += new System.EventHandler(this.lbxOrderNumber_SelectedIndexChanged);
            // 
            // cmbOrderedBy
            // 
            this.cmbOrderedBy.Enabled = false;
            this.cmbOrderedBy.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbOrderedBy.ItemHeight = 16;
            this.cmbOrderedBy.Location = new System.Drawing.Point(441, 59);
            this.cmbOrderedBy.Name = "cmbOrderedBy";
            this.cmbOrderedBy.Size = new System.Drawing.Size(216, 24);
            this.cmbOrderedBy.TabIndex = 25;
            this.cmbOrderedBy.SelectedIndexChanged += new System.EventHandler(this.cmbOrderedBy_SelectedIndexChanged);
            // 
            // optOrderedBy
            // 
            this.optOrderedBy.AutoSize = true;
            this.optOrderedBy.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optOrderedBy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.optOrderedBy.Location = new System.Drawing.Point(321, 61);
            this.optOrderedBy.Name = "optOrderedBy";
            this.optOrderedBy.Size = new System.Drawing.Size(96, 21);
            this.optOrderedBy.TabIndex = 24;
            this.optOrderedBy.Text = "Ordered by";
            this.optOrderedBy.Click += new System.EventHandler(this.optOrderedBy_Click);
            // 
            // optOrderNumber
            // 
            this.optOrderNumber.AutoSize = true;
            this.optOrderNumber.Checked = true;
            this.optOrderNumber.Font = new System.Drawing.Font("Tahoma", 10F);
            this.optOrderNumber.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.optOrderNumber.Location = new System.Drawing.Point(9, 35);
            this.optOrderNumber.Name = "optOrderNumber";
            this.optOrderNumber.Size = new System.Drawing.Size(114, 21);
            this.optOrderNumber.TabIndex = 23;
            this.optOrderNumber.TabStop = true;
            this.optOrderNumber.Text = "Order Number";
            this.optOrderNumber.Click += new System.EventHandler(this.optOrderNumber_Click);
            // 
            // cmbSupplier
            // 
            this.cmbSupplier.Enabled = false;
            this.cmbSupplier.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbSupplier.ItemHeight = 16;
            this.cmbSupplier.Location = new System.Drawing.Point(441, 11);
            this.cmbSupplier.Name = "cmbSupplier";
            this.cmbSupplier.Size = new System.Drawing.Size(216, 24);
            this.cmbSupplier.TabIndex = 22;
            this.cmbSupplier.SelectedIndexChanged += new System.EventHandler(this.cmbSupplier_SelectedIndexChanged);
            // 
            // optSupplier
            // 
            this.optSupplier.AutoSize = true;
            this.optSupplier.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.optSupplier.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.optSupplier.Location = new System.Drawing.Point(321, 13);
            this.optSupplier.Name = "optSupplier";
            this.optSupplier.Size = new System.Drawing.Size(74, 21);
            this.optSupplier.TabIndex = 21;
            this.optSupplier.Text = "Supplier";
            this.optSupplier.Click += new System.EventHandler(this.optSupplier_Click);
            // 
            // lblTimePeriod
            // 
            this.lblTimePeriod.AutoSize = true;
            this.lblTimePeriod.Font = new System.Drawing.Font("Tahoma", 10F);
            this.lblTimePeriod.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblTimePeriod.Location = new System.Drawing.Point(45, 10);
            this.lblTimePeriod.Name = "lblTimePeriod";
            this.lblTimePeriod.Size = new System.Drawing.Size(79, 17);
            this.lblTimePeriod.TabIndex = 38;
            this.lblTimePeriod.Text = "Time Period";
            // 
            // lblTimePeriod_Data
            // 
            this.lblTimePeriod_Data.AutoSize = true;
            this.lblTimePeriod_Data.BackColor = System.Drawing.Color.White;
            this.lblTimePeriod_Data.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblTimePeriod_Data.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblTimePeriod_Data.Location = new System.Drawing.Point(6, 62);
            this.lblTimePeriod_Data.Name = "lblTimePeriod_Data";
            this.lblTimePeriod_Data.Size = new System.Drawing.Size(150, 18);
            this.lblTimePeriod_Data.TabIndex = 37;
            this.lblTimePeriod_Data.Text = "31.12.2099 - 31.12.2099";
            this.lblTimePeriod_Data.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // cmbTimePeriod
            // 
            this.cmbTimePeriod.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTimePeriod.FormattingEnabled = true;
            this.cmbTimePeriod.Items.AddRange(new object[] {
            "Previous Year",
            "Previous Month",
            "Previous Week",
            "Current Week",
            "Current Month",
            "Current Year",
            "Custom"});
            this.cmbTimePeriod.Location = new System.Drawing.Point(6, 33);
            this.cmbTimePeriod.Name = "cmbTimePeriod";
            this.cmbTimePeriod.Size = new System.Drawing.Size(150, 21);
            this.cmbTimePeriod.TabIndex = 0;
            this.cmbTimePeriod.SelectedIndexChanged += new System.EventHandler(this.cmbTimePeriod_SelectedIndexChanged);
            // 
            // OrderQuery
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.scCriteriaFields);
            this.Name = "OrderQuery";
            this.Size = new System.Drawing.Size(859, 99);
            this.Load += new System.EventHandler(this.OrderQuery_Load);
            this.scCriteriaFields.Panel1.ResumeLayout(false);
            this.scCriteriaFields.Panel1.PerformLayout();
            this.scCriteriaFields.Panel2.ResumeLayout(false);
            this.scCriteriaFields.Panel2.PerformLayout();
            this.scCriteriaFields.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer scCriteriaFields;
        private System.Windows.Forms.ListBox lbxOrderNumber;
        private System.Windows.Forms.ComboBox cmbOrderedBy;
        private System.Windows.Forms.RadioButton optOrderedBy;
        private System.Windows.Forms.RadioButton optOrderNumber;
        private System.Windows.Forms.ComboBox cmbSupplier;
        private System.Windows.Forms.RadioButton optSupplier;
        private System.Windows.Forms.ComboBox cmbTimePeriod;
        private System.Windows.Forms.Label lblTimePeriod_Data;
        private System.Windows.Forms.Label lblTimePeriod;
        private System.Windows.Forms.Label lblNoOrdersFound;
    }
}
