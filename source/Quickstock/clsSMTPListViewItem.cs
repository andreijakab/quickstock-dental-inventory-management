using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using Utilities;

namespace DSMS
{
    public class clsSMTPListViewItem : ListViewItem
    {
        private bool            m_blnCredentialsRequired, m_blnNew;
        private int             m_intPort, m_intTimeout;
        private clsSMTPServer   m_smtpServer;
        private string          m_strName, m_strAddress;
        private string          m_strUserName, m_strPassword;
        
        public clsSMTPListViewItem()
        {
            m_smtpServer = new clsSMTPServer();

            // initialize global variables
            m_blnNew = true;

            m_strName = m_smtpServer.AccountName;
            m_strAddress = m_smtpServer.Address;
            m_intPort = m_smtpServer.Port;
            m_intTimeout = m_smtpServer.Timeout;
            m_blnCredentialsRequired = m_smtpServer.CredentialsRequired;
            m_strUserName = m_smtpServer.UserName;
            m_strPassword = m_smtpServer.Password;

            // populate listeview item fields
            this.Text = m_strName;
            this.SubItems.Add(m_strAddress);
        }

        public clsSMTPListViewItem(clsSMTPServer server)
        {
            // initialize global variables
            m_blnNew = false;
            m_smtpServer = server;
            m_strName = m_smtpServer.AccountName;
            m_strAddress = m_smtpServer.Address;
            m_intPort = m_smtpServer.Port;
            m_intTimeout = m_smtpServer.Timeout;
            m_blnCredentialsRequired = m_smtpServer.CredentialsRequired;
            m_strUserName = m_smtpServer.UserName;
            m_strPassword = m_smtpServer.Password;

            // populate listeview item fields
            this.Text = m_strName;
            this.SubItems.Add(m_strAddress);
        }

        #region Properties
        public string AccountName
        {
            set
            {
                m_smtpServer.AccountName = m_strName = value;
                this.SubItems[0].Text = m_strName;
            }
            get { return m_strName; }
        }

        public string Address
        {
            set
            {
                m_smtpServer.Address = m_strAddress = value;
                this.SubItems[1].Text = m_strAddress;
            }
            get { return m_strAddress; }
        }

        public int Port
        {
            set { m_smtpServer.Port = m_intPort = value; }
            get { return m_intPort; }
        }

        public clsSMTPServer SMTPServerObject
        {
            get { return m_smtpServer; }
        }

        public int Timeout
        {
            set { m_smtpServer.Timeout = m_intTimeout = value; }
            get { return m_intTimeout; }
        }

        public bool CredentialsRequired
        {
            set { m_smtpServer.CredentialsRequired = m_blnCredentialsRequired = value; }
            get { return m_blnCredentialsRequired; }
        }

        public string UserName
        {
            set { m_smtpServer.UserName = m_strUserName = value; }
            get { return m_strUserName; }
        }

        public string Password
        {
            set { m_smtpServer.Password = m_strPassword = value; }
            get { return m_strPassword; }
        }

        public bool IsNew
        {
            set { m_blnNew = value; }
            get { return m_blnNew; }
        }
        #endregion
    }
}
