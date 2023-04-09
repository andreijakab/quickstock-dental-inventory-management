using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace OrderQuery
{
    public partial class fclsCustomPeriod : Form
    {
        private OrderQuery m_oqOwner;

        public fclsCustomPeriod(OrderQuery oqOwner)
        {
            InitializeComponent();

            m_oqOwner = oqOwner;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            m_oqOwner.SetTimePeriod(this.dtpStart.Value, this.dtpEnd.Value);
            this.Close();
        }
    }
}
