using System;
using System.Collections;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;

namespace Utilities
{
    public struct SupplierInformation
	{
		public int DatabaseID;
		public string Name;
		public string ContactName;
        public string FaxNumber;
		public string PhoneNumber;
		public string Email;

        public SupplierInformation(DataRow dtrSupplierInfo)
        {
            this.DatabaseID = (int) dtrSupplierInfo["FournisseurId"];
            this.Name = dtrSupplierInfo["CompanyName"].ToString();
            this.ContactName = clsUtilities.FormatName_Display(dtrSupplierInfo["ConTitle"].ToString(),
                                                               dtrSupplierInfo["ContactFirstName"].ToString(),
                                                               dtrSupplierInfo["ContactLastName"].ToString());
            this.PhoneNumber = dtrSupplierInfo["PhoneNumber"].ToString();
            this.FaxNumber = dtrSupplierInfo["FaxNumber"].ToString();
            this.Email = dtrSupplierInfo["Email"].ToString();
        }
	}

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

        public static DateTime INVALID_DATE = DateTime.MinValue.AddTicks(1);

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

		// Method for finding the index of an item in a ComboBox
		public static int FindItemIndex(String strSearchString, System.Windows.Forms.ComboBox cmbComboBox)
		{
			for(int i=0; i < cmbComboBox.Items.Count; i++)
			{
				if(strSearchString.CompareTo(cmbComboBox.Items[i].ToString().Trim()) == 0)
					return i;
			}
			return -1;
		}

		// Method for finding the index of an item in a ListBox
		public static int FindItemIndex(String strSearchString, System.Windows.Forms.ListBox lsbListBox)
		{
			for(int i=0; i < lsbListBox.Items.Count; i++)
			{
				if(strSearchString.CompareTo(lsbListBox.Items[i].ToString().Trim()) == 0)
					return i;
			}
			return -1;
		}

		// Method for finding the index of an item in a ListView
		public static int FindItemIndex(String strSearchString, System.Windows.Forms.ListView lsvListView)
		{
			for(int i=0; i < lsvListView.Items.Count; i++)
			{
				if(strSearchString.CompareTo(lsvListView.Items[i].ToString().Trim()) == 0)
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

        public static string FormatEmail_Friendly(string strName, string strEmail)
        { 
            // TODO: check for non-ASCII characters

            // return formatted email string
            if (strName != null && strName.Length > 0)
                return strName + " [" + strEmail + "]";
            else
                return strEmail;
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

        public static string FormatSMTPServer_List(string strAccountName, string strServer)
        {
            return (strAccountName.Trim() + " (" + strServer.Trim() + ")");
        }

        // TODO: Write code for this! (checks that all required tables and fields are there)
        public static bool CheckDatabaseIntegrity(OleDbConnection odcConnection)
        {
            return true;    
        }

        /// <summary>
        /// Compares a hash of the specified plain text value to a given hash
        /// value. Plain text is hashed with the same salt value as the original
        /// hash.
        /// </summary>
        /// <remarks>
        /// Adapted from SimpleHash by Obviex (http://www.obviex.com/samples/hash.aspx).
        /// </remarks>
        /// <param name="strPassword">
        /// Plain text to be verified against the specified hash. The function
        /// does not check whether this parameter is null.
        /// </param>
        /// <param name="strHash">
        /// Base64-encoded hash value produced by String_Hash function. This value
        /// includes the original salt appended to it.
        /// </param>
        /// <returns>
        /// If computed hash mathes the specified hash the function the return
        /// value is true; otherwise, the function returns false.
        /// </returns>
        public static bool String_CompareHashes(string strPassword, string strHash)
        {
            // Convert base64-encoded hash value into a byte array.
            byte[] bytHashWithSaltBytes = Convert.FromBase64String(strHash);

            // size of hash (without salt)
            int intHashSizeInBits = 256;                    // for SHA256
            int intHashSizeInBytes = intHashSizeInBits/8;

            // Make sure that the specified hash value is long enough.
            if (bytHashWithSaltBytes.Length < intHashSizeInBytes)
                return false;

            // Allocate array to hold original salt bytes retrieved from hash.
            byte[] bytSaltBytes = new byte[bytHashWithSaltBytes.Length - intHashSizeInBytes];

            // Copy salt from the end of the hash to the new array.
            for (int i = 0; i < bytSaltBytes.Length; i++)
                bytSaltBytes[i] = bytHashWithSaltBytes[intHashSizeInBytes + i];

            // Compute a new hash string.
            string strExpectedHashString = String_Hash(strPassword, bytSaltBytes);

            // If the computed hash matches the specified hash,
            // the plain text value must be correct.
            return (strHash == strExpectedHashString);
        }

        /// <summary>
        /// Generates a hash for the given plain text value and returns a
        /// base64-encoded result. Before the hash is computed, a random salt
        /// is generated and appended to the plain text. This salt is stored at
        /// the end of the hash value, so it can be used later for hash
        /// verification.
        /// </summary>
        /// <remarks>
        /// Adapted from SimpleHash by Obviex (http://www.obviex.com/samples/hash.aspx).
        /// </remarks>
        /// <param name="plainText">
        /// String to be hashed. The function does not check whether
        /// this parameter is null.
        /// </param>
        /// <param name="bytSalt">
        /// Salt bytes. This parameter can be null, in which case a random salt
        /// value will be generated.
        /// </param>
        /// <returns>
        /// Hash value formatted as a base64-encoded string.
        /// </returns>
        public static string String_Hash(string strString, byte[] bytSalt)
        {
            //            
            // generate salt if none was specified
            //
            if (bytSalt == null)
            {
                // Generate a random number for the size of the salt.
                int intSaltSize = new Random().Next(4, 8);

                // Allocate a byte array, which will hold the salt.
                bytSalt = new byte[intSaltSize];

                // Initialize a random number generator.
                RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

                // Fill the salt with cryptographically strong byte values.
                rng.GetNonZeroBytes(bytSalt);
            }

            //
            // prepare to hash salt + string
            //
            // Convert plain text into a byte array.
            byte[] bytPlainTextBytes = Encoding.UTF8.GetBytes(strString);

            // Allocate array, which will hold plain text and salt.
            byte[] bytPlainTextWithSaltBytes = new byte[bytPlainTextBytes.Length + bytSalt.Length];

            // Copy plain text bytes into resulting array.
            for (int i = 0; i < bytPlainTextBytes.Length; i++)
                bytPlainTextWithSaltBytes[i] = bytPlainTextBytes[i];

            // Append salt bytes to the resulting array.
            for (int i = 0; i < bytSalt.Length; i++)
                bytPlainTextWithSaltBytes[bytPlainTextBytes.Length + i] = bytSalt[i];

            //
            // generate hash
            //
            // Initialize hashing algorithm class.
            SHA256Managed hash = new SHA256Managed();
            
            // Compute hash value of our plain text with appended salt.
            byte[] bytHashBytes = hash.ComputeHash(bytPlainTextWithSaltBytes);

            // Create array which will hold hash and original salt bytes.
            byte[] bytHashWithSaltBytes = new byte[bytHashBytes.Length + bytSalt.Length];

            // Copy hash bytes into resulting array.
            for (int i = 0; i < bytHashBytes.Length; i++)
                bytHashWithSaltBytes[i] = bytHashBytes[i];

            // Append salt bytes to the result.
            for (int i = 0; i < bytSalt.Length; i++)
                bytHashWithSaltBytes[bytHashBytes.Length + i] = bytSalt[i];

            // Convert result into a base64-encoded string.
            string strResult = Convert.ToBase64String(bytHashWithSaltBytes);
            
            // Return the result.
            return strResult;
        }
	}
}
