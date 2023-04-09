using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace PriceTextBox
{
    public partial class PriceTextBox:TextBox
    {
        public delegate void EnterKeyPress();
        public event EnterKeyPress OnEnterKeyPress;

        private decimal             m_decUnitPrice;
        private NumberFormatInfo    m_nfiLocalNumberFormat;
        private string              m_strDecimalSeparator;

        public PriceTextBox()
        {
            CultureInfo ciCurrentCulture = (CultureInfo) System.Globalization.CultureInfo.CurrentCulture.Clone();
            m_nfiLocalNumberFormat = ciCurrentCulture.NumberFormat;
            m_nfiLocalNumberFormat.CurrencySymbol = "";
            m_strDecimalSeparator = m_nfiLocalNumberFormat.CurrencyDecimalSeparator;

            this.CausesValidation = true;
            this.TextAlign = HorizontalAlignment.Right;
        }

        public decimal Price
        {
            get
            {
                return m_decUnitPrice;
            }
            set
            {
                m_decUnitPrice = value;
                this.Text = (m_decUnitPrice.ToString("C", m_nfiLocalNumberFormat)).Trim();
            }
        }

        protected override void OnEnter(EventArgs e)
        {
            // call the base event
 	        base.OnEnter(e);

            string strValue = this.Text;

            try
            {
                if (decimal.Parse(strValue) == 0)
                    this.Text = "";
            }
            catch
            { }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            // call the base events
            base.OnKeyPress(e);

            // validate typed char
            string strKeyInput = e.KeyChar.ToString();

            if (Char.IsDigit(e.KeyChar))
            {
                // Digits are OK
            }
            else if (strKeyInput.CompareTo(m_strDecimalSeparator) == 0)
            {
                // Decimal separators are OK
            }
            else if (e.KeyChar == '\b')
            {
                // Backspace key is OK
            }
            else if (e.KeyChar == (char)13)
            {
                if (OnEnterKeyPress != null)
                    OnEnterKeyPress();
            }
            else
            {
                // Swallow this invalid key
                e.Handled = true;
            }
        }

        protected override void OnValidating(CancelEventArgs e)
        {
            // call the base event
            base.OnValidating(e);

            string strValue = this.Text;
            try
            {
                if (strValue != null && strValue.Length > 0)
                {
                    if (decimal.Parse(strValue) < 0)
                        throw new OverflowException();
                    else
                        m_decUnitPrice = decimal.Parse(strValue);
                }
                else
                    m_decUnitPrice = 0.0M;
            }
            catch (FormatException)
            {
                e.Cancel = true;
                MessageBox.Show("Invalid price format!", "Quick Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.SelectAll();
            }
            catch (OverflowException)
            {
                e.Cancel = true;
                MessageBox.Show("Invalid price!", "Quick Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.SelectAll();
            }
        }

        protected override void  OnValidated(EventArgs e)
        {
 	        base.OnValidated(e);

            this.Text = (m_decUnitPrice.ToString("C", m_nfiLocalNumberFormat)).Trim();
        }

        public bool ValidatePrice()
        {
            try
            {
                if (this.Text != null && this.Text.Length > 0)
                {
                    m_decUnitPrice = decimal.Parse(this.Text);
                    if(m_decUnitPrice < 0.0M)
                        throw new OverflowException();
                }
                else
                    m_decUnitPrice = 0.0M;

                return true;
            }
            catch (FormatException)
            {
                MessageBox.Show("Invalid price format!", "Quick Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.SelectAll();
            }
            catch (OverflowException)
            {
                MessageBox.Show("Invalid price!", "Quick Stock", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.SelectAll();
            }

            return false;
        }
    }
}
