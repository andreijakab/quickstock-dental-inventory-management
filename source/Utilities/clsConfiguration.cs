using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
//using System.Net.Mail;
using System.Text;
//using System.Windows.Forms;
using System.Xml;

namespace Utilities
{
    public class clsConfiguration
    {
        private enum ConfigurationParameters : int
        {
            AdministratorID = 1,
            DefaultSupplierID = 2,
            UserID = 3,
            DefaultEmailSubject = 4,
            DefaultEmailBody = 5,
            LanguageID = 6,
            OrderBackup = 7,
            Password = 8,
        };

        #region Internal
        private static int      m_intCurrentUserID;
        private static string   m_strApplicationTitle, m_strComannyName;
        private static string   m_strConfigurationFilesPath, m_strDataFilesPath;

        private static void Internal_Init()
        {
            // retrieve product & company name
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Reflection.AssemblyProductAttribute apaProductTitle = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyProductAttribute), false)[0] as System.Reflection.AssemblyProductAttribute;
            System.Reflection.AssemblyCompanyAttribute acaCompany = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyCompanyAttribute), false)[0] as System.Reflection.AssemblyCompanyAttribute;
            clsConfiguration.Internal_ApplicationName = apaProductTitle.Product;
            clsConfiguration.Internal_CompanyName = acaCompany.Company;

            clsConfiguration.Internal_ConfigurationFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData) + "\\" + clsConfiguration.Internal_CompanyName + "\\" + clsConfiguration.Internal_ApplicationName;
            clsConfiguration.Internal_DataFilesPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + clsConfiguration.Internal_CompanyName + "\\" + clsConfiguration.Internal_ApplicationName;

            // make sure required folder structure is in place
            try
            {
                if (!Directory.Exists(clsConfiguration.Internal_DataFilesPath + "\\Orders"))
                    Directory.CreateDirectory(clsConfiguration.Internal_DataFilesPath + "\\Orders");
                if (!Directory.Exists(clsConfiguration.Internal_DataFilesPath + "\\Tenders"))
                    Directory.CreateDirectory(clsConfiguration.Internal_DataFilesPath + "\\Tenders");
                if (!Directory.Exists(clsConfiguration.Internal_DataFilesPath + "\\CancelledBackOrders"))
                    Directory.CreateDirectory(clsConfiguration.Internal_DataFilesPath + "\\CancelledBackOrders");
            }
            catch
            {
                clsConfiguration.Internal_DataFilesPath = Path.GetTempPath() + "\\" + clsConfiguration.Internal_CompanyName + "\\" + clsConfiguration.Internal_ApplicationName;
                try
                {
                    if (!Directory.Exists(clsConfiguration.Internal_DataFilesPath + "\\Orders"))
                        Directory.CreateDirectory(clsConfiguration.Internal_DataFilesPath + "\\Orders");
                    if (!Directory.Exists(clsConfiguration.Internal_DataFilesPath + "\\Tenders"))
                        Directory.CreateDirectory(clsConfiguration.Internal_DataFilesPath + "\\Tenders");
                    if (!Directory.Exists(clsConfiguration.Internal_DataFilesPath + "\\CancelledBackOrders"))
                        Directory.CreateDirectory(clsConfiguration.Internal_DataFilesPath + "\\CancelledBackOrders");
                }
                catch
                {
                    throw new ConfigurationException("Unable to create required folder structure. Application will exit.",
                                                     true);
                }
            }

            if (!Directory.Exists(clsConfiguration.Internal_ConfigurationFilesPath))
                Directory.CreateDirectory(clsConfiguration.Internal_ConfigurationFilesPath);

            // this value is changed by GetConfiguration() as well as by fclsGENLogin
            clsConfiguration.Internal_CurrentUserID = -1;
        }

        public static string Internal_ApplicationName
        {
            get { return m_strApplicationTitle; }
            set { m_strApplicationTitle = value; }
        }

        public static string Internal_CompanyName
        {
            get { return m_strComannyName; }
            set { m_strComannyName = value; }
        }

        public static int Internal_CurrentUserID
        {
            get { return m_intCurrentUserID; }
            set { m_intCurrentUserID = value; }
        }

        public static string Internal_ConfigurationFilesPath
        {
            get { return m_strConfigurationFilesPath; }
            set { m_strConfigurationFilesPath = value; }
        }

        public static string Internal_DataFilesPath
        {
            get { return m_strDataFilesPath; }
            set { m_strDataFilesPath = value; }
        }
        #endregion

        #region General
        private static bool     m_blnBackupOrders;
        private static int      m_intDefaultUserID;
        private static int      m_intDefaultSupplierID;

        public static bool General_BackupOrders
        {
            get { return m_blnBackupOrders; }
            set { m_blnBackupOrders = value; }
        }

        public static int General_DefaultUserID
        {
            get { return m_intDefaultUserID; }
            set { m_intDefaultUserID = value; }
        }

        public static int General_DefaultSupplierID
        {
            get { return m_intDefaultSupplierID; }
            set { m_intDefaultSupplierID = value; }
        }
        #endregion

        #region DentalOfficeInfo
        private static string               m_strDentalOfficeName;
        private static string               m_strDentalOfficeAddress;
        private static string               m_strDentalOfficeStateProvince;
        private static string               m_strDentalOfficePostalCode;
        private static string               m_strDentalOfficeCity;
        private static string               m_strDentalOfficeCountry;
        private static string               m_strDentalOfficePhoneNr;
        private static string               m_strDentalOfficeFaxNr;
        private static string               m_strDentalOfficeEmail;
        private static int                  m_strDentalOfficeAdministratorUserID;

        public static string DentalOffice_Name
        {
            get { return m_strDentalOfficeName; }
            set { m_strDentalOfficeName = value; }
        }

        public static string DentalOffice_Address
        {
            get { return m_strDentalOfficeAddress; }
            set { m_strDentalOfficeAddress = value; }
        }

        public static string DentalOffice_StateProvince
        {
            get { return m_strDentalOfficeStateProvince; }
            set { m_strDentalOfficeStateProvince = value; }
        }

        public static string DentalOffice_PostalCode
        {
            get { return m_strDentalOfficePostalCode; }
            set { m_strDentalOfficePostalCode = value; }
        }

        public static string DentalOffice_City
        {
            get { return m_strDentalOfficeCity; }
            set { m_strDentalOfficeCity = value; }
        }

        public static string DentalOffice_Country
        {
            get { return m_strDentalOfficeCountry; }
            set { m_strDentalOfficeCountry = value; }
        }

        public static string DentalOffice_PhoneNr
        {
            get { return m_strDentalOfficePhoneNr; }
            set { m_strDentalOfficePhoneNr = value; }
        }

        public static string DentalOffice_FaxNr
        {
            get { return m_strDentalOfficeFaxNr; }
            set { m_strDentalOfficeFaxNr = value; }
        }

        public static string DentalOffice_Email
        {
            get { return m_strDentalOfficeEmail; }
            set { m_strDentalOfficeEmail = value; }
        }

        public static int DentalOffice_AdministratorUserID
        {
            get { return m_strDentalOfficeAdministratorUserID; }
            set { m_strDentalOfficeAdministratorUserID = value; }
        }



        #endregion

        #region EMail
        private static string                   m_strEmailSubject;
        private static string                   m_strEmailBody;
        private static SMTPServersCollection    m_SMTPServers;

        public static string Email_Subject
        {
            get { return m_strEmailSubject; }
            set { m_strEmailSubject = value; }
        }

        public static string Email_Body
        {
            get { return m_strEmailBody; }
            set { m_strEmailBody = value; }
        }

        public static SMTPServersCollection Email_SMTPServers
        {
            get {return m_SMTPServers;}
        }
        #endregion

        #region Security
        private static bool m_blnLoginRequired;

        public static bool Security_EmployeeLoginRequired
        {
            get { return m_blnLoginRequired; }
            set { m_blnLoginRequired = value; }
        }
        #endregion

        #region UtilityFunctions
        private static DataTable            m_dtaCompany, m_dtaConfiguration;
        private static OleDbCommandBuilder  m_odcbCompany, m_odcbConfiguration;
        private static OleDbDataAdapter     m_oddaCompany, m_oddaConfiguration;
        private static OleDbConnection      m_odcConnection;

        public static void Initialize(OleDbConnection odcConnection)
        {
            m_odcConnection = odcConnection;

            // retrieve parameters
            clsConfiguration.Internal_Init();
            clsConfiguration.GetConfiguration();
            clsConfiguration.GetDentalOfficeInformation();
            clsConfiguration.GetSMTPServers();
        }

        public static void Save()
        {
            // save parameters
            clsConfiguration.SaveConfiguration();
            clsConfiguration.SaveDentalOfficeInformation();
            clsConfiguration.SaveSMTPServers();
        }

        private static void GetConfiguration()
        {
            DataRow dtrRow;

            //oddaConfiguration = new OleDbDataAdapter("SELECT * FROM [Configuration] WHERE [User ID]=" + clsConfiguration.Internal_CurrentUserID, odcConnection);
            m_oddaConfiguration = new OleDbDataAdapter("SELECT * FROM [Configuration]", m_odcConnection);
            m_odcbConfiguration = new OleDbCommandBuilder(m_oddaConfiguration);
            m_odcbConfiguration.QuotePrefix = "[";
            m_odcbConfiguration.QuoteSuffix = "]";
            m_dtaConfiguration = new DataTable();

            try
            {
                // get configuration data from database
                m_oddaConfiguration.Fill(m_dtaConfiguration);

                // store configuration locally
                dtrRow = m_dtaConfiguration.Rows[0];
                clsConfiguration.General_BackupOrders = (bool)dtrRow[(int)ConfigurationParameters.OrderBackup];
                clsConfiguration.General_DefaultSupplierID = (int)dtrRow[(int)ConfigurationParameters.DefaultSupplierID];
                clsConfiguration.Internal_CurrentUserID = clsConfiguration.General_DefaultUserID = (int)dtrRow[(int)ConfigurationParameters.UserID];
                clsConfiguration.DentalOffice_AdministratorUserID = (int)dtrRow[(int)ConfigurationParameters.AdministratorID];
                clsConfiguration.Email_Subject = (string)dtrRow[(int)ConfigurationParameters.DefaultEmailSubject];
                clsConfiguration.Email_Body = (string)dtrRow[(int)ConfigurationParameters.DefaultEmailBody];
                clsConfiguration.Security_EmployeeLoginRequired = (bool)dtrRow[(int)ConfigurationParameters.Password];
            }
            catch
            {
                clsConfiguration.General_BackupOrders = true;
                clsConfiguration.General_DefaultSupplierID = -1;
                clsConfiguration.General_DefaultUserID = -1;
                clsConfiguration.DentalOffice_AdministratorUserID = -1;
                clsConfiguration.Email_Subject = "";
                clsConfiguration.Email_Body = "";
                clsConfiguration.Security_EmployeeLoginRequired = false;

                throw new ConfigurationException("An error occured while loading the configuration data.\nDefault values have been used instead.");
            }
        }

        private static void GetDentalOfficeInformation()
        {
            DataRow dtrCompany;

            //Load Dental Office Information Table
            m_oddaCompany = new OleDbDataAdapter("SELECT * FROM [DentalOfficeInformation]", m_odcConnection);
            m_odcbCompany = new OleDbCommandBuilder(m_oddaCompany);
            m_odcbConfiguration.QuotePrefix = "[";
            m_odcbConfiguration.QuoteSuffix = "]";
            m_dtaCompany = new DataTable();

            try
            {

                m_oddaCompany.Fill(m_dtaCompany);
                dtrCompany = m_dtaCompany.Rows[0];

                // set local properties according to data loaded from database
                clsConfiguration.DentalOffice_Name = dtrCompany["CompanyName"].ToString();
                clsConfiguration.DentalOffice_Address = dtrCompany["Adress"].ToString();
                clsConfiguration.DentalOffice_City = dtrCompany["City"].ToString();
                clsConfiguration.DentalOffice_PostalCode = dtrCompany["PostalCode"].ToString();
                clsConfiguration.DentalOffice_StateProvince = dtrCompany["StateOrProvince"].ToString();
                clsConfiguration.DentalOffice_Country = dtrCompany["Country"].ToString();
                clsConfiguration.DentalOffice_PhoneNr = dtrCompany["PhoneNumber"].ToString();
                clsConfiguration.DentalOffice_FaxNr = dtrCompany["FaxNumber"].ToString();
                clsConfiguration.DentalOffice_Email = dtrCompany["Email"].ToString();
            }
            catch
            {
                clsConfiguration.DentalOffice_Name = "";
                clsConfiguration.DentalOffice_Address = "";
                clsConfiguration.DentalOffice_City = "";
                clsConfiguration.DentalOffice_PostalCode = "";
                clsConfiguration.DentalOffice_StateProvince = "";
                clsConfiguration.DentalOffice_Country = "";
                clsConfiguration.DentalOffice_PhoneNr = "";
                clsConfiguration.DentalOffice_FaxNr = "";
                clsConfiguration.DentalOffice_Email = "";

                throw new ConfigurationException("An error occured while loading the dental office information.");
            }
        }

        private static void GetSMTPServers()
        {
            clsSMTPServer server;
            XmlTextReader xtrReader = null;

            m_SMTPServers = new SMTPServersCollection();

            try
            {
                xtrReader = new XmlTextReader(Internal_ConfigurationFilesPath + "\\SMTP.xml");

                // Read the root element
                xtrReader.ReadStartElement("SMTPServersList");

                // Skip header
                xtrReader.ReadStartElement("Header");
                xtrReader.ReadElementString("Author");
                xtrReader.ReadElementString("Date");
                xtrReader.ReadEndElement();

                // Read elements in file
                while (xtrReader.Read())
                {
                    server = new clsSMTPServer();

                    if (xtrReader.NodeType == XmlNodeType.Element && xtrReader.Name == "Item")
                    {
                        xtrReader.ReadStartElement("Item");
                        server.AccountName = xtrReader.ReadElementString("AccountName");
                        server.Address = xtrReader.ReadElementString("Address");
                        server.Port = int.Parse(xtrReader.ReadElementString("Port"));
                        server.CredentialsRequired = bool.Parse(xtrReader.ReadElementString("UseCredentials"));
                        server.UserName = xtrReader.ReadElementString("UserName");
                        server.Password = xtrReader.ReadElementString("Password");
                        server.Timeout = int.Parse(xtrReader.ReadElementString("Timeout"));
                        xtrReader.ReadEndElement();

                        m_SMTPServers.Add(server);
                    }
                }
                xtrReader.Close();
            }
            catch
            {
                throw new ConfigurationException("The configuration file containing the outgoing email server information is invalid.\nIts contents were ignored.");
            }
        }

        private static void SaveConfiguration()
        {
            DataRow dtrRow;

            try
            {
                // store configuration in DataTable object
                dtrRow = m_dtaConfiguration.Rows[0];
                dtrRow[(int)ConfigurationParameters.OrderBackup] = clsConfiguration.General_BackupOrders;
                dtrRow[(int)ConfigurationParameters.DefaultSupplierID] = clsConfiguration.General_DefaultSupplierID;
                dtrRow[(int)ConfigurationParameters.UserID] = clsConfiguration.General_DefaultUserID;
                dtrRow[(int)ConfigurationParameters.AdministratorID] = clsConfiguration.DentalOffice_AdministratorUserID;
                dtrRow[(int)ConfigurationParameters.DefaultEmailSubject] = clsConfiguration.Email_Subject;
                dtrRow[(int)ConfigurationParameters.DefaultEmailBody] = clsConfiguration.Email_Body;
                dtrRow[(int)ConfigurationParameters.Password] = clsConfiguration.Security_EmployeeLoginRequired;

                m_oddaConfiguration.Update(m_dtaConfiguration);
                m_dtaConfiguration.AcceptChanges();
            }
            catch
            {
                m_dtaConfiguration.RejectChanges();

                throw new ConfigurationException("An error occured while saving the configuration data.");
            }
        }

        private static void SaveDentalOfficeInformation()
        {
            DataRow dtrCompany;

            try
            {
                // save local properties to DataTable object
                dtrCompany = m_dtaCompany.Rows[0];
                dtrCompany["CompanyName"] = clsConfiguration.DentalOffice_Name;
                dtrCompany["Adress"] = clsConfiguration.DentalOffice_Address;
                dtrCompany["City"] = clsConfiguration.DentalOffice_City;
                dtrCompany["PostalCode"] = clsConfiguration.DentalOffice_PostalCode;
                dtrCompany["StateOrProvince"] = clsConfiguration.DentalOffice_StateProvince;
                dtrCompany["Country"] = clsConfiguration.DentalOffice_Country;
                dtrCompany["PhoneNumber"] = clsConfiguration.DentalOffice_PhoneNr;
                dtrCompany["FaxNumber"] = clsConfiguration.DentalOffice_FaxNr;
                dtrCompany["Email"] = clsConfiguration.DentalOffice_Email;

                m_oddaCompany.Update(m_dtaCompany);
                m_dtaCompany.AcceptChanges();
            }
            catch
            {
                m_dtaCompany.RejectChanges();

                throw new ConfigurationException("An error occured while saving the dental office information.");
            }
        }
        
		private static void SaveSMTPServers()
		{
            clsSMTPServer server;

            try
            {
                XmlTextWriter xtwWriter = new XmlTextWriter(Internal_ConfigurationFilesPath + "\\SMTP.xml", System.Text.Encoding.UTF8);

                // Indent the XML document for readability
                xtwWriter.Formatting = System.Xml.Formatting.Indented;

                // Call WriteStartDocument to write XML declaration
                xtwWriter.WriteStartDocument();

                // Write root element
                xtwWriter.WriteStartElement("SMTPServersList");

                // Create element <Header>
                xtwWriter.WriteStartElement("Header");
                xtwWriter.WriteAttributeString("Title", "SMTP Servers List");
                xtwWriter.WriteElementString("Author", "QuickStock");
                xtwWriter.WriteElementString("Date", System.DateTime.Today.ToShortDateString().ToString());
                xtwWriter.WriteEndElement();

                // Create an element for each entry in the list
                foreach (Object obj in Email_SMTPServers)
                {
                    server = (clsSMTPServer)obj;
                    xtwWriter.WriteStartElement("Item");
                    xtwWriter.WriteElementString("AccountName", server.AccountName);
                    xtwWriter.WriteElementString("Address", server.Address);
                    xtwWriter.WriteElementString("Port", server.Port.ToString());
                    xtwWriter.WriteElementString("UseCredentials", server.CredentialsRequired.ToString());
                    xtwWriter.WriteElementString("UserName", server.UserName);
                    xtwWriter.WriteElementString("Password", server.Password);
                    xtwWriter.WriteElementString("Timeout", server.Timeout.ToString());
                    xtwWriter.WriteEndElement();
                }

                // Write end of root element
                xtwWriter.WriteEndElement();

                // Write end of document
                xtwWriter.WriteEndDocument();

                xtwWriter.Close();
            }
            catch
            {
                throw new ConfigurationException("An error occured while saving the outgoing email server information.");
            }
		}
        #endregion
    }
}