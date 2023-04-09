using Microsoft.Win32;
using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;


namespace DSMS
{
	public struct SupplierInformation
	{
		public int DatabaseID;
		public string Name;
		public string ContactName;
		public string PhoneNumber;
		public string Email;
	}

    public enum ConfigurationParameters : int
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

	public enum ApplicationParameters : int
	{
		ApplicationPath = 101
	};
    
	/// <summary>
	/// Summary description for clsUtilities.
	/// </summary>
	public class clsUtilities
	{
		//
		// GLOBAL CONSTANTS
		//
		public const string ADMIN_PASSWORD = "Adm1n22";
		public const string ACTIVATION_EMAIL = "andrei1@videotron.ca";
		private static string m_strDefaultKey = "19831210195210111997120119841210";

        // Global Format Strings
        public const string FORMAT_DATE_ORDERED = "yyyy-MM-dd";
        public const string FORMAT_DATE_QUERY = "MM/dd/yy";
		public const string FORMAT_DATE_DISPLAY = "dd MMMM yyyy";
        public const string FORMAT_CURRENCY = "#,##0.00";

		struct sctSMTPServer
		{
			public string Name;
			public string SMTPServer;
		}

		public enum EmailType:int {Activation, Order, Tender, CanceledBackorder}
        
        private enum DataType : int { Boolean, String, Numeric, Unknown };

		// Public Method that Compares Two Strings
		public static bool CompareStrings(string str1, string str2)
		{
			// compare the values, using the CompareTo method on the first string
			int cmpVal = str1.CompareTo(str2);

			if (cmpVal == 0) // the values are the same
				return true;
         
			else // the second string is greater than the first string
				return false;
		}

		// Public Method that Returns the Value of an User-Specific Parameter
		public static object GetApplicationParameter(ApplicationParameters apParameter)
		{
			System.Reflection.Assembly assCurrent = System.Reflection.Assembly.GetExecutingAssembly();
			System.Reflection.AssemblyTitleAttribute ataAssemblyTitle = assCurrent.GetCustomAttributes(typeof(System.Reflection.AssemblyTitleAttribute), false)[0] as System.Reflection.AssemblyTitleAttribute;
			System.Reflection.AssemblyCompanyAttribute acaCompany = assCurrent.GetCustomAttributes(typeof(System.Reflection.AssemblyCompanyAttribute), false)[0] as System.Reflection.AssemblyCompanyAttribute;
			object objParameter = null;
			string strTemp;

			switch(apParameter)
			{
				case ApplicationParameters.ApplicationPath:
					strTemp = Environment.GetFolderPath(Environment.SpecialFolder.Personal) + "\\" + acaCompany.Company + "\\" + ataAssemblyTitle.Title;
					objParameter = strTemp;
				break;
			}

			return objParameter;
		}

        // Public Method that Returns the Value of an User-Specific Parameter
        public static object GetParameterValue2(int intUserID, ConfigurationParameters cpParameter, OleDbConnection odcConnection)
        {
            bool blnParameterValue = false;
            DataTable dtaConfiguration = new DataTable();
            DataType dtParameterType = DataType.Unknown;
            int intParameterValue = -1;
            OleDbDataAdapter oddaConfiguration;
            string strParameterValue = "";
            
            // set default parameter value
            switch (cpParameter)
            {
                case ConfigurationParameters.Password:
                    dtParameterType = DataType.Boolean;
                    blnParameterValue = true;
                break;

                case ConfigurationParameters.OrderBackup:
                    dtParameterType = DataType.Boolean;
                break;

                case ConfigurationParameters.AdministratorID:
                case ConfigurationParameters.DefaultSupplierID:
                case ConfigurationParameters.LanguageID:
                case ConfigurationParameters.UserID:
                    dtParameterType = DataType.Numeric;
                break;
                
                case ConfigurationParameters.DefaultEmailBody:
                case ConfigurationParameters.DefaultEmailSubject:
                    dtParameterType = DataType.String;
                break;
            }
            
			// TODO: error-recovery
            try
            {
                // get configuration data from database
                //oddaConfiguration = new OleDbDataAdapter("SELECT * FROM [Configuration] WHERE [User ID]=" + intUserID, odcConnection);
                oddaConfiguration = new OleDbDataAdapter("SELECT * FROM [Configuration]", odcConnection);
                oddaConfiguration.Fill(dtaConfiguration);

                // retrieve desired parmeter value
                if (dtaConfiguration.Rows.Count == 1 && (int)cpParameter <= dtaConfiguration.Columns.Count)
                {
                    switch(dtParameterType)
                    {
                        case DataType.Boolean:
                            blnParameterValue = (bool)dtaConfiguration.Rows[0][(int)cpParameter];
                        break;

                        case DataType.Numeric:
                            intParameterValue = (int)dtaConfiguration.Rows[0][(int)cpParameter];
                        break;

                        case DataType.String:
                            strParameterValue = dtaConfiguration.Rows[0][(int)cpParameter].ToString();
                        break;
                    }
                }
            }
            catch
            { }
            
            switch (dtParameterType)
            {
                case DataType.Boolean:
                    return blnParameterValue;

                case DataType.Numeric:
                    return intParameterValue;

                case DataType.String:
                    return strParameterValue;

                default:
                    return null;
            }
        }

        // Public Method that Sets the Value of a Parameter
        public static bool SetParameterValue2(int intUserID, ConfigurationParameters cpParameter, Object objParmeterValue, OleDbConnection odcConnection)
        {
            bool blnParameterValue = false, blnSuccess = false;
            DataTable dtaConfiguration = new DataTable();
            DataType dtParameterType = DataType.Unknown;
            int intParameterValue = -1;
            OleDbCommandBuilder odcbCommandBuilder;
            OleDbDataAdapter oddaConfiguration;
            string strParameterValue = "";

            // set default parameter value
            switch (cpParameter)
            {
                case ConfigurationParameters.Password:
                case ConfigurationParameters.OrderBackup:
                    dtParameterType = DataType.Boolean;
                    blnParameterValue = (bool)objParmeterValue;
                break;

                case ConfigurationParameters.AdministratorID:
                case ConfigurationParameters.DefaultSupplierID:
                case ConfigurationParameters.LanguageID:
                case ConfigurationParameters.UserID:
                    dtParameterType = DataType.Numeric;
                    intParameterValue = (int)objParmeterValue;
                break;

                case ConfigurationParameters.DefaultEmailBody:
                case ConfigurationParameters.DefaultEmailSubject:
                    dtParameterType = DataType.String;
                    strParameterValue = (string)objParmeterValue;
                break;
            }

            try
            {
                // get configuration data from database
                //oddaConfiguration = new OleDbDataAdapter("SELECT * FROM [Configuration] WHERE [User ID]=" + intUserID, odcConnection);
                oddaConfiguration = new OleDbDataAdapter("SELECT * FROM [Configuration]", odcConnection);
                odcbCommandBuilder = new OleDbCommandBuilder(oddaConfiguration);
                odcbCommandBuilder.QuotePrefix = "[";
                odcbCommandBuilder.QuoteSuffix = "]";
                oddaConfiguration.Fill(dtaConfiguration);

                // retrieve desired parmeter value
                if (dtaConfiguration.Rows.Count == 1 && (int)cpParameter <= dtaConfiguration.Columns.Count)
                {
                    switch (dtParameterType)
                    {
                        case DataType.Boolean:
                            dtaConfiguration.Rows[0][(int)cpParameter] = blnParameterValue;
                        break;

                        case DataType.Numeric:
                            dtaConfiguration.Rows[0][(int)cpParameter] = intParameterValue;
                        break;

                        case DataType.String:
                            dtaConfiguration.Rows[0][(int)cpParameter] = strParameterValue;
                        break;
                    }

                    oddaConfiguration.Update(dtaConfiguration);
                    dtaConfiguration.AcceptChanges();

                    blnSuccess = true;
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message + "\n" + ex.InnerException);
            }

            return blnSuccess;
        }

		// Method for finding the index of an item in a ComboBox
		public static int FindItemIndex(String strSearchString, System.Windows.Forms.ComboBox cmbComboBox)
		{
			for(int i=0; i < cmbComboBox.Items.Count; i++)
			{
				if(strSearchString.CompareTo(cmbComboBox.Items[i].ToString()) == 0)
					return i;
			}
			return -1;
		}

		// Method for finding the index of an item in a ListBox
		public static int FindItemIndex(String strSearchString, System.Windows.Forms.ListBox lsbListBox)
		{
			for(int i=0; i < lsbListBox.Items.Count; i++)
			{
				if(strSearchString.CompareTo(lsbListBox.Items[i].ToString()) == 0)
					return i;
			}
			return -1;
		}

		// Method for finding the index of an item in a ListView
		public static int FindItemIndex(String strSearchString, System.Windows.Forms.ListView lsvListView)
		{
			for(int i=0; i < lsvListView.Items.Count; i++)
			{
				if(strSearchString.CompareTo(lsvListView.Items[i].ToString()) == 0)
					return i;
			}
			return -1;
		}

		public static byte[] Encrypt(string strPlainText)
		{
			return Encrypt(strPlainText, m_strDefaultKey);
		}

        public static int GetListIDfromDatabaseID(int intDatabaseID, DataTable dtaTable, int intDatabaseIDColumn)
        {
            int intListID = -1;

            if (intDatabaseID > -1)
            {
                for (int i = 0; i < dtaTable.Rows.Count; i++)
                {
                    if (((int) dtaTable.Rows[i][intDatabaseIDColumn]) == intDatabaseID)
                    {
                        intListID = i;
                        break;
                    }
                }
            }

            return intListID;
        }

        /// <summary>
        ///		Encrypts a string using the Rijndael symmetric encryption algorithm.
        /// </summary>
        /// <param name="strPlainText">
        ///		The string to be encrypted.
        /// </param>
        /// <param name="strPassphrase">
        ///		String that will be used to generate the encryption key and the IV.
        /// </param>
        /// <returns>
        ///		Returns the cipher text as a byte array.
        /// </returns>
        public static byte[] Encrypt(string strPlainText, string strPassphrase)
		{
			byte[] bytKey, bytIV, bytPlainText, bytCipherText;
			char [] chrKeyCharArray;
			CryptoStream stcCryptoStream;
			MemoryStream stmCipherText;
			RijndaelManaged rjmRijndaelManaged; //AES Block Content-Encyption Cipher
			
			// initialize variables
			bytIV = new byte[16];
			bytKey = new byte[32];
			stmCipherText = new MemoryStream();
			rjmRijndaelManaged = new RijndaelManaged();
						
			// create key and IV
			chrKeyCharArray = strPassphrase.ToCharArray();
			for(int i = 0; i < 32; i++)
			{
				if(i < chrKeyCharArray.Length)
					bytKey[i] = (byte) chrKeyCharArray[i];
				else
					bytKey[i] = (byte) '2';
				
				if(i % 2 == 0)
					bytIV[i/2] = bytKey[i];
			}

			// configure algorithm
			rjmRijndaelManaged.KeySize = 256;
			rjmRijndaelManaged.Mode = CipherMode.CBC;			//Cipher Block Chaining Mode
			rjmRijndaelManaged.Padding = PaddingMode.PKCS7;		//PKCS7 Padding String "03 03 03"
			rjmRijndaelManaged.IV = bytIV;
			rjmRijndaelManaged.Key = bytKey;

			//Create a CryptoStream in Write Mode; initialise withe the Rijndael's Encryptor ICryptoTransform
			stcCryptoStream = new CryptoStream(stmCipherText,rjmRijndaelManaged.CreateEncryptor(),CryptoStreamMode.Write);

			//Encode the passed plain text string into Unicode byte stream
			bytPlainText = new UnicodeEncoding().GetBytes(strPlainText);

			//Write the plaintext byte stream to CryptoStream
			stcCryptoStream.Write(bytPlainText,0,bytPlainText.Length);

			//close the stream
			stcCryptoStream.Close();

			//Extract the ciphertext byte stream and close the MemoryStream
			bytCipherText = stmCipherText.ToArray();

			stmCipherText.Close();

			return bytCipherText;
		}
		
		public static string Decrypt(byte[] bytCipherText)
		{
			return Decrypt(bytCipherText,m_strDefaultKey);
		}

		/// <summary>
		///		Decrypts a byte array that was encrypted using the Rijndael symmetric encryption algorithm.
		/// </summary>
		/// <param name="bytCipherText">
		///		Byte array containing the cipher text.
		/// </param>
		/// <param name="strPassphrase">
		///		String that will be used to generate the encryption key and the IV.
		/// </param>
		/// <returns>
		///		If the decryption is successfull, the clear text is returned. Otherwise an emtpy string is returned.
		/// </returns>
		public static string Decrypt(byte[] bytCipherText, string strPassphrase)
		{
			byte[] bytBuffer, bytIV, bytKey, bytPlainText;
			char[] chrKeyCharArray;
			CryptoStream cstCryptoStream;
			int intActualBytesRead;
			MemoryStream mstCipherText, mstPlainText;
			RijndaelManaged rjmRijndaelManaged;
			string strPlainText;

			// initalize variables
			bytBuffer = new Byte[100];					// byte array into which we will read the plaintext from CryptoStream
			bytIV = new byte[16];
			bytKey = new byte[32];
			intActualBytesRead = 0;
			rjmRijndaelManaged = new RijndaelManaged();
			mstPlainText = new MemoryStream();
			strPlainText = "";

			try
			{
				// decrypt cipher text
				// create key and IV
				chrKeyCharArray = strPassphrase.ToCharArray();
				for(int i = 0; i < 32; i++)
				{
					if(i < chrKeyCharArray.Length)
						bytKey[i] = (byte) chrKeyCharArray[i];
					else
						bytKey[i] = (byte) '2';
				
					if(i % 2 == 0)
						bytIV[i/2] = bytKey[i];
				}

				// configure algorithm
				rjmRijndaelManaged.IV = bytIV;
				rjmRijndaelManaged.Key = bytKey;
				rjmRijndaelManaged.Mode = CipherMode.CBC;		//Cipher Block Chaining Mode
				rjmRijndaelManaged.Padding = PaddingMode.PKCS7; //PKCS7 Padding String "03 03 03"

				//Create a memory stream from which CryptoStream will read the cipher text
				mstCipherText = new MemoryStream(bytCipherText);

				//Create a CryptoStream in Write Mode; initialise withe the Rijndael's Encryptor ICryptoTransform
				cstCryptoStream = new CryptoStream(mstCipherText,rjmRijndaelManaged.CreateDecryptor(),CryptoStreamMode.Read);

				do
				{
					//read the plaintext from CryptoStream
					intActualBytesRead = cstCryptoStream.Read(bytBuffer,0,100);

					//if we have reached the end of stream quit the loop
					if (intActualBytesRead == 0)
						break;

					//copy the plaintext byte array to MemoryStream
					mstPlainText.Write(bytBuffer,0,intActualBytesRead);

				}while(true);

				//don't forget to close the streams
				cstCryptoStream.Close();
				mstCipherText.Close();

				//Extract the plaintext byte stream and close the MemoryStream
				bytPlainText = mstPlainText.ToArray();
				mstPlainText.Close();

				//Encode the plaintext byte into Unicode string
				strPlainText = new UnicodeEncoding().GetString(bytPlainText);
			}
			catch
			{
				strPlainText = "";
			}

			return strPlainText;
		}

		public static string ValidateCurrency(string strToBeTested)
		{
			char chrToBeTested = (strToBeTested.ToCharArray())[strToBeTested.Length - 1];

			if(!(char.IsDigit(chrToBeTested)) && chrToBeTested != '.')
			{
				return strToBeTested.Substring(0,strToBeTested.Length - 1);
			}

			return strToBeTested;
		}

		public static string[,] GetSMTPServers()
		{
			ArrayList arrList = null;
			sctSMTPServer sctServer;
			string[,] strSMTPServers = null;
			XmlTextReader xtrReader = null;
			
			if(File.Exists(Application.StartupPath + "\\SMTP.xml"))
			{
				try
				{
					xtrReader = new XmlTextReader(Application.StartupPath + "\\SMTP.xml");
					arrList = new ArrayList();
			
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
						sctServer = new sctSMTPServer();

						if (xtrReader.NodeType == XmlNodeType.Element && xtrReader.Name == "Item")
						{
							xtrReader.ReadStartElement("Item");
							sctServer.Name = xtrReader.ReadElementString("Name");
							sctServer.SMTPServer = xtrReader.ReadElementString("SMTPServer");
							xtrReader.ReadEndElement();
															
							arrList.Add(sctServer);
						}
					}
					xtrReader.Close();

					strSMTPServers = new string[arrList.Count,2];

					for(int i = 0; i < arrList.Count; i++)
					{
						sctServer = (sctSMTPServer) arrList[i];
						strSMTPServers[i,0] = sctServer.Name;
						strSMTPServers[i,1] = sctServer.SMTPServer;
					}
				}
				catch
				{
					MessageBox.Show("The SMTP.xml file is invalid. It will be ignored.");
					strSMTPServers = null;
				}
			}

			return strSMTPServers;
		}

		public static bool SaveSMTPServers(ListView lsvSMTPServersList)
		{
			bool blnSuccess = false;
			XmlTextWriter xtwWriter = new XmlTextWriter(Application.StartupPath + "\\SMTP.xml", System.Text.Encoding.UTF8);

			// Indent the XML document for readability
			xtwWriter.Formatting = System.Xml.Formatting.Indented;			

			// Call WriteStartDocument to write XML declaration
			xtwWriter.WriteStartDocument();			

			// Write root element
			xtwWriter.WriteStartElement("SMTPServersList");

			// Create element <Header>
			xtwWriter.WriteStartElement("Header");
			xtwWriter.WriteAttributeString("Title", "SMTP Servers List");
			xtwWriter.WriteElementString("Author","QuickStock");
			xtwWriter.WriteElementString("Date",System.DateTime.Today.ToShortDateString().ToString());
			xtwWriter.WriteEndElement();
			
			// Create an element for each entry in the list
			foreach(ListViewItem lviItem in lsvSMTPServersList.Items)
			{
				xtwWriter.WriteStartElement("Item");
				xtwWriter.WriteElementString("Name",lviItem.SubItems[0].Text);
				xtwWriter.WriteElementString("SMTPServer",lviItem.SubItems[1].Text);
				xtwWriter.WriteEndElement();				
			}

			// Write end of root element
			xtwWriter.WriteEndElement(); 

			// Write end of document
			xtwWriter.WriteEndDocument();

			xtwWriter.Close();
			blnSuccess = true;

			return blnSuccess;
		}

		/// <summary>
		///		Formats an employee's name for use in a list (e.g. John Doe, Mr.).
		/// </summary>
		/// <param name="strTitle">
		///		Them employee's title if known, 'null' otherwise.
		/// </param>
		/// <param name="strFirstName">
		///		Them employee's first name if known, 'null' otherwise.
		/// </param>
		/// <param name="strLastName">
		///		Them employee's last name if known, 'null' otherwise.
		/// </param>
		/// <returns>
		///		Returns the employee's formatted name if successfull; otherwise, it returns an empty string.
		/// </returns>
		public static string FormatName_List(string strTitle, string strFirstName, string strLastName)
		{
			string strFullName = "";

			if(strFirstName != null && strFirstName.Length > 0)
			{
				strFullName = strFirstName.Trim();
			}

			if(strLastName != null && strLastName.Length > 0)
			{
				if(strFullName.Length > 0)
					strFullName += " " + strLastName.Trim();
				else
					strFullName = strLastName.Trim();
			}
			
			if(strTitle != null && strTitle.Length > 0)
			{
				if(strFullName.Length > 0)
					strFullName += ", " + strTitle.Trim();
				else
					strFullName = strTitle.Trim();
			}

			return strFullName;
		}
		
		/// <summary>
		///		Formats an employee's name only for display (e.g. Mr. John Doe).
		/// </summary>
		/// <param name="strTitle">
		///		Them employee's title if known, 'null' otherwise.
		/// </param>
		/// <param name="strFirstName">
		///		Them employee's first name if known, 'null' otherwise.
		/// </param>
		/// <param name="strLastName">
		///		Them employee's last name if known, 'null' otherwise.
		/// </param>
		/// <returns>
		///		Returns the employee's formatted name if successfull; otherwise, it returns an empty string.
		/// </returns>
		public static string FormatName_Display(string strTitle, string strFirstName, string strLastName)
		{
			string strFullName = "";

			if(strTitle != null && strTitle.Length > 0)
			{
				strFullName = strTitle.Trim();
			}

			if(strFirstName != null && strFirstName.Length > 0)
			{
				if(strFullName.Length > 0)
					strFullName += " " + strFirstName.Trim();
				else
					strFullName = strFirstName.Trim();
			}

			if(strLastName != null && strLastName.Length > 0)
			{
				if(strFullName.Length > 0)
					strFullName += " " + strLastName.Trim();
				else
					strFullName = strLastName.Trim();
			}
			

			return strFullName;
		}

		public static string FormatProduct_Display(string strProduct, string strSubProduct)
		{
			string strFullName = "";

			strFullName = strProduct.Trim() + " - " + strSubProduct.Trim();

			return strFullName;
		}
		
		// TODO: Make international
		public static string FormatPhoneNumbers(string strPhoneNumber)
		{
			int intExtensionStartPosition = -1;
			string strExtension = "";
			string strFormattedPhoneNumber = ((((strPhoneNumber.Trim()).Replace(" ", "")).Replace("-","")).Replace("(","")).Replace(")","").Replace("X","x");
			
			//TODO: make option of this thing
			string strDefaultAreaCode = "514";
			
			intExtensionStartPosition = strFormattedPhoneNumber.LastIndexOf("x");
			
			if(intExtensionStartPosition != -1)
			{
				strExtension = strFormattedPhoneNumber.Substring(intExtensionStartPosition,strFormattedPhoneNumber.Length - intExtensionStartPosition);
				strFormattedPhoneNumber = strFormattedPhoneNumber.Remove(intExtensionStartPosition,strFormattedPhoneNumber.Length - intExtensionStartPosition);
			}
			
			switch(strFormattedPhoneNumber.Length)
			{
				case 7:
					strFormattedPhoneNumber = "(" + strDefaultAreaCode + ") " + strFormattedPhoneNumber.Substring(0,3) + "-" + strFormattedPhoneNumber.Substring(3,4);
				break;

				case 10:
					strFormattedPhoneNumber = "(" + strFormattedPhoneNumber.Substring(0,3) + ") " + strFormattedPhoneNumber.Substring(3,3) + "-" + strFormattedPhoneNumber.Substring(5,4);
				break;
			}
			
			if(intExtensionStartPosition != -1)
				strFormattedPhoneNumber += strExtension;
			
			return strFormattedPhoneNumber;
		}

        // TODO: Write code for this! (checks that all required tables and fields are there)
        public static bool CheckDatabaseIntegrity(OleDbConnection odcConnection)
        {
            return true;    
        }

		#region Obsolete
		// Public Method that Returns the Value of a Parameter
		[Obsolete("This method cannot be used anymore. Use GetParameterValue2() instead.")]
		private static string[] GetParameterValue()
		{
			byte[] bytBuffer = new byte[1024];
			byte[] bytCipherText;
			FileStream fstConfigFile = null;
			int intConfigFileLength = 0, j = 0;
			string strConfigText;
			string[] strConfigTextParts = new string[10];
			string[] strMan;

			try
			{
				fstConfigFile = new FileStream(Application.StartupPath + "\\config.dat",FileMode.Open);
				intConfigFileLength = (int) fstConfigFile.Length;
				fstConfigFile.Read(bytBuffer,0,intConfigFileLength);
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}

			fstConfigFile.Close();

			bytCipherText = new byte[intConfigFileLength];
			for(int i = 0; i < intConfigFileLength; i++)
			{
				bytCipherText[i] = bytBuffer[i];
			}

			strConfigText = clsUtilities.Decrypt(bytCipherText);
			strMan = strConfigText.Split('|','|');
			
			for(int i = 0; i < strMan.Length; i++)
			{
				if (strMan[i].Length != 0) strConfigTextParts[j++] = strMan[i];
			}
			
			return strConfigTextParts;
		}

		// Public Method that Sets the Value of a Parameter
		[Obsolete("This method cannot be used anymore. Use SetParameterValue2() instead.")]
		public static bool SetParameterValue(string strParameter, string strParameterValue)
		{
			bool blnConfigFileCreated = false;
			FileStream fstConfigFile = null;
			int intIndex;
			string strContents;
			string[] strConfigTextParts;
			
			switch(strParameter)
			{
				case "Administrator":
					intIndex = 0;
					break;

				case "Default Supplier":
					intIndex = 1;
					break;

				case "Default User":
					intIndex = 2;
					break;

				case "Email Body":
					intIndex = 3;
					break;
				
				case "Email Subject":
					intIndex = 4;
					break;
				
				case "Evaluation Date":
					intIndex = 5;
					break;
				
				case "Evaluation Day":
					intIndex = 6;
					break;

				case "Language":
					intIndex = 7;
					break;
				
				case "Order Backup":
					intIndex = 8;
					break;

				case "Password":
					intIndex = 9;
					break;

				default:
					return blnConfigFileCreated;
			}

			strConfigTextParts = clsUtilities.GetParameterValue();
			
			// Administrator,Default Supplier,Default User,Email Body,Email Subject,Evaluation Date,Evaluation Day,Language,Order Backup,Password
			strContents = "";
			for(int i=0; i < 10; i++)
			{
				if(i == intIndex)
					strContents += strParameterValue + "||";
				else
					strContents += strConfigTextParts[i]+ "||";
			}
			
			byte[] bytCipherText = clsUtilities.Encrypt(strContents);

			try
			{
				fstConfigFile = new FileStream(Application.StartupPath + "\\config.dat",FileMode.Create);
				fstConfigFile.Write(bytCipherText,0,bytCipherText.Length);
			}
			catch
			{
				return blnConfigFileCreated;
			}

			fstConfigFile.Close();
			blnConfigFileCreated = true;

			return blnConfigFileCreated;
		}

		[Obsolete("This method is not needed since currency symbols are not supposed to be displayed together with prices anymore.")]
		public static string RemoveCurrencySymbol(string strSource)
		{
			int intIndexOfCurrencySymbol;
			NumberFormatInfo nfiNumberFormat;
			string strCurrencySymbol, strEditedSource;
			
			nfiNumberFormat = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
			strCurrencySymbol = nfiNumberFormat.CurrencySymbol;
			intIndexOfCurrencySymbol = strSource.IndexOf(strCurrencySymbol,0);
						
			if(intIndexOfCurrencySymbol > -1)
			{
				if(intIndexOfCurrencySymbol == 0)
					strEditedSource = strSource.Substring(strCurrencySymbol.Length, strSource.Length - strCurrencySymbol.Length);
				else
					strEditedSource = strSource.Substring(0,intIndexOfCurrencySymbol);
			}
			else
				strEditedSource = strSource;
			
			strEditedSource = strEditedSource.Trim();

			return strEditedSource;
		}


		// Public Method that Returns the Value of a Parameter
		/*public static string GetParameterValue(int intIndex)
		{
			byte[] bytBuffer = new byte[1024];
			byte[] bytCipherText;
			FileStream fstConfigFile = null;
			int intConfigFileLength = 0, j = 0;
			string strConfigText;
			string[] strConfigTextParts = new string[10];
			string[] strMan;

			try
			{
				fstConfigFile = new FileStream(Application.StartupPath + "\\config.dat",FileMode.Open);
				intConfigFileLength = (int) fstConfigFile.Length;
				fstConfigFile.Read(bytBuffer,0,intConfigFileLength);

				fstConfigFile.Close();

				bytCipherText = new byte[intConfigFileLength];
				for(int i = 0; i < intConfigFileLength; i++)
				{
					bytCipherText[i] = bytBuffer[i];
				}

				strConfigText = clsUtilities.Decrypt(bytCipherText);
				strMan = strConfigText.Split('|','|');
			
				for(int i = 0; i < strMan.Length; i++)
				{
					if (strMan[i].Length != 0) strConfigTextParts[j++] = strMan[i];
				}
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
				return null;
			}

			return strConfigTextParts[intIndex - 1];
		}*/
		#endregion
	}
}
