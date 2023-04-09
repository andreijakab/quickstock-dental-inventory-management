using System;
using System.Collections;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// This class is an implementation of the 'IComparer' interface.
	/// </summary>
    public class clsBackorderListViewItem : ListViewItem
    {
        public enum ChangeState : int { Canceled, Updated, None}
        
        private ChangeState         m_State;
        private DateTime            m_dtLastChanged;
        private int                 m_intNUnitsBackordered, m_intNUnitsReceived;
        private decimal             m_decUnitPrice;
        private NumberFormatInfo    m_nfiNumberFormat;
        private string              m_strProductName, m_strTrademark;

        public clsBackorderListViewItem(string strProduct,
                                        string strSubProduct,
                                        string strTrademark,
                                        string strPackaging,
                                        decimal decUnitPrice,
                                        int intNUnitsBackordered,
                                        Object objBackorderLastUpdated)
        {
            CultureInfo ciCurrentCulture;

            // initialize global variables
            m_decUnitPrice = decUnitPrice;
            m_intNUnitsBackordered = intNUnitsBackordered;
            m_intNUnitsReceived = 0;
            m_State = ChangeState.None;
            m_strProductName = clsUtilities.FormatProduct_Display(strProduct, strSubProduct);
            m_strTrademark = strTrademark;

            // get local number format
            ciCurrentCulture = (CultureInfo) System.Globalization.CultureInfo.CurrentCulture.Clone();
            m_nfiNumberFormat = ciCurrentCulture.NumberFormat;
            m_nfiNumberFormat.CurrencySymbol = "";

            // Set the line's color depending on the amount due
            if (m_intNUnitsBackordered > 0)
            {
                this.ForeColor = Color.Black;
                m_dtLastChanged = (DateTime) objBackorderLastUpdated;
                this.Text = m_dtLastChanged.ToShortDateString();
            }
            else
            {
                m_dtLastChanged = clsUtilities.INVALID_DATE;
                this.ForeColor = Color.LightGray;
                this.Text = "";
            }

            this.SubItems.Add(strProduct);
            this.SubItems.Add(strSubProduct);
            this.SubItems.Add(m_strTrademark);
            this.SubItems.Add(m_decUnitPrice.ToString("C", m_nfiNumberFormat));
            this.SubItems.Add(m_intNUnitsBackordered.ToString());
            this.SubItems.Add(strPackaging);
        }

        public ChangeState State
        {
            get
            {
                return m_State;
            }

            set
            {
                m_State = value;

                switch (m_State)
                {
                    case ChangeState.Canceled:
                        this.Font = new Font(this.Font, FontStyle.Strikeout);
                    break;

                    default:
                        this.Font = new Font(this.Font, FontStyle.Regular);
                    break;
                }
            }
        }

        public DateTime LastChanged
        {
            set
            {
                m_dtLastChanged = value;
                this.SubItems[0].Text = m_dtLastChanged.ToShortDateString();
            }
            get
            {
                return m_dtLastChanged;
            }
        }

        public decimal UnitPrice
        {
            set
            {
                m_decUnitPrice = value;
                this.SubItems[4].Text = m_decUnitPrice.ToString("C", m_nfiNumberFormat);
            }
            get
            {
                return m_decUnitPrice;
            }
        }

        public int NUnitsBackordered
        {
            set
            {
                m_intNUnitsBackordered = value;
                if(m_intNUnitsBackordered == 0)
                    this.ForeColor = Color.LightGray;
                else
                    this.ForeColor = Color.Black;
                
                this.SubItems[5].Text = m_intNUnitsBackordered.ToString();
            }
            get
            {
                return m_intNUnitsBackordered;
            }
        }

        public string ProductName
        {
            get
            {
                return m_strProductName;
            }
        }

        public int NUnitsReceived
        {
            set
            {
                m_intNUnitsReceived = value;
            }
            get
            {
                return m_intNUnitsReceived;
            }
        }

        public string Trademark
        {
            get
            {
                return m_strTrademark;
            }
        }
    }
}