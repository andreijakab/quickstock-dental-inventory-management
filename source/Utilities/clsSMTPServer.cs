using System;
using System.Collections.Generic;
using System.Text;

namespace Utilities
{
    public class clsSMTPServer
    {
        private bool    m_blnCredentialsRequired;
        private int     m_intPort, m_intTimeout;
        private string  m_strAccountName, m_strAddress;
        private string  m_strUserName, m_strPassword;

        public clsSMTPServer(string strAccountName, string strAddress, int intPort,
                          int intTimeout, bool blnCredentialsRequired,
                          string strUserName, string strPassword)
        {
            this.AccountName = strAccountName;
            this.Address = strAddress;
            this.Port = intPort;
            this.Timeout = intTimeout;
            this.CredentialsRequired = blnCredentialsRequired;
            this.UserName = strUserName;
            this.Password = strPassword;
        }

        public clsSMTPServer()
        {
            this.AccountName = "";
            this.Address = "";
            this.Port = 25;
            this.Timeout = 2;
            this.CredentialsRequired = false;
            this.UserName = "";
            this.Password = "";
        }

        #region Properties
        public string AccountName
        {
            set { m_strAccountName = value; }
            get { return m_strAccountName; }
        }

        public string Address
        {
            set { m_strAddress = value; }
            get { return m_strAddress; }
        }

        public int Port
        {
            set { m_intPort = value; }
            get { return m_intPort; }
        }

        public int Timeout
        {
            set { m_intTimeout = value; }
            get { return m_intTimeout; }
        }

        public bool CredentialsRequired
        {
            set { m_blnCredentialsRequired = value; }
            get { return m_blnCredentialsRequired; }
        }

        public string UserName
        {
            set { m_strUserName = value; }
            get { return m_strUserName; }
        }

        public string Password
        {
            set { m_strPassword = value; }
            get { return m_strPassword; }
        }

        #endregion
    }
}
