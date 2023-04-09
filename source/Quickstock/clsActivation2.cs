using Microsoft.Win32;
using System;
using System.IO;
using System.Management;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for clsActivation2.
	/// </summary>
	public class clsActivation2
	{
		public const string m_cstrActivationKeyFile = "activation_key.dat";
		public const string m_cstrActivationRequestFile = "activation.dat";

		private string m_strCustomerName, m_strCustomerEmail, m_strActivationVersion, m_strProductName, m_strProductVersion, m_strProdutctInstallPath, m_strMotherboardSerial, m_strWindowsVersion, m_strWindowsProductID;

		public clsActivation2(string strProductName, string strProductVersion, string strProdutctInstallPath)
		{
			this.CustomerName = "";
			this.CustomerEmail = "";
			this.ActivationVersion = "2.1";
			this.ProductName = strProductName;
			this.ProductVersion = strProductVersion;
			this.ProductInstallationPath = strProdutctInstallPath;
			this.MotherboardSerial = this.GetMotherboardSerial();
			this.WindowsVersion = this.GetOS();
			this.WindowsProductID = this.GetWindowsProductId();
		}

		public clsActivation2()
		{
			this.CustomerName = "";
			this.CustomerEmail = "";
			this.ActivationVersion = "2.1";
			this.ProductName = "";
			this.ProductVersion = "";
			this.ProductInstallationPath = "";
			this.MotherboardSerial = this.GetMotherboardSerial();
			this.WindowsVersion = this.GetOS();
			this.WindowsProductID = this.GetWindowsProductId();
		}
		
		#region Properties
		//--------------------------------------------------------------------------------------------------------------------
		// Properties
		//--------------------------------------------------------------------------------------------------------------------
		public string CustomerName
		{
			set
			{
				m_strCustomerName = value;
			}
			get
			{
				return m_strCustomerName;
			}
		}

		public string CustomerEmail
		{
			set
			{
				m_strCustomerEmail = value;
			}
			get
			{
				return m_strCustomerEmail;
			}
		}

		public string ActivationVersion
		{
			set
			{
				m_strActivationVersion = value;
			}
			get
			{
				return m_strActivationVersion;
			}
		}

		public string ProductName
		{
			set
			{
				m_strProductName = value;
			}
			get
			{
				return m_strProductName;
			}
		}

		public string ProductVersion
		{
			set
			{
				m_strProductVersion = value;
			}
			get
			{
				return m_strProductVersion;
			}
		}

		public string ProductInstallationPath
		{
			set
			{
				m_strProdutctInstallPath = value;
			}
			get
			{
				return m_strProdutctInstallPath;
			}
		}

		public string MotherboardSerial
		{
			set
			{
				m_strMotherboardSerial = value;
			}
			get
			{
				return m_strMotherboardSerial;
			}
		}

		public string WindowsVersion
		{
			set
			{
				m_strWindowsVersion = value;
			}
			get
			{
				return m_strWindowsVersion;
			}
		}

		public string WindowsProductID
		{
			set
			{
				m_strWindowsProductID = value;
			}
			get
			{
				return m_strWindowsProductID;
			}
		}
		#endregion
		
		#region Customer Information
		/// <summary>
		///		Retrieve the serial number of the motherboard from the WMI store.
		/// </summary>
		/// <returns>
		///		Returns motherboard's serial number.
		/// </returns>
		private string GetMotherboardSerial()
		{
			ManagementObjectCollection mocList = null;
			ManagementObjectSearcher mosWin32BaseBoard = new ManagementObjectSearcher("Select * From Win32_BaseBoard");
			string strMotherboarSerial = "";

			mocList = mosWin32BaseBoard.Get();

			foreach (ManagementObject mo in mocList)
			{
				strMotherboarSerial = mo["SerialNumber"].ToString();
				if(strMotherboarSerial.Length > 0)
					break;
			}

			return strMotherboarSerial;
		}

		/// <summary>
		///		Finds the version of the operating system and returns its name.
		/// </summary>
		/// <returns>
		///		The operating system's name formatted as a string.
		/// </returns>
		private string GetOS()
		{
			// Get OperatingSystem information from the system namespace.
			System.OperatingSystem osInfo = System.Environment.OSVersion;
			string strVersion = "";
         
			// Determine the platform.
			switch(osInfo.Platform)
			{
				// Platform is Windows 95, Windows 98, 
				// Windows 98 Second Edition, or Windows Me.
				case System.PlatformID.Win32Windows:
					switch (osInfo.Version.Minor)
					{
						case 0:
							strVersion = "Windows 95";
						break;
						
						case 10:
							if(osInfo.Version.Revision.ToString()=="2222A")
								strVersion = "Windows 98 Second Edition";
							else
								strVersion = "Windows 98";
						break;
						
						case  90:
							strVersion = "Windows Me";
						break;

						default:
							strVersion = "Unknown Windows version";
						break;
					}
				break;

				// Platform is Windows NT 3.51, Windows NT 4.0, Windows 2000,
				// or Windows XP.
				case System.PlatformID.Win32NT:
					switch(osInfo.Version.Major)
					{
						case 3:
							strVersion = "Windows NT 3.51";
						break;
						
						case 4:
							strVersion = "Windows NT 4.0";
						break;
						
						case 5:
							switch(osInfo.Version.Minor)
							{
								case 0:
									strVersion = "Windows 2000";
								break;

								case 1:
									strVersion = "Windows XP";
								break;

								case 2:
									strVersion = "Windows Server 2003";
								break;
							}
						break;
						
						case 6:
							strVersion = "Windows Vista";
						break;

						default:
							strVersion = "Future Windows version (unknown)";
						break;
					}
				break;

				default:
					strVersion = "Future Windows version (unknown)";
				break;
			}
			
			return strVersion;
		}
		
		/// <summary>
		///		Finds the Windows Product Id returns it.
		/// </summary>
		/// <returns>
		///		Retruns the Windows Product Id formatted as a string.
		/// </returns>
		private string GetWindowsProductId()
		{
			RegistryKey rgkWindowsCurrentVersion = Registry.LocalMachine.OpenSubKey("Software", false).OpenSubKey("Microsoft", false).OpenSubKey("Windows NT",false).OpenSubKey("CurrentVersion",false);

			if(rgkWindowsCurrentVersion != null)
			{
				foreach(string strKey in rgkWindowsCurrentVersion.GetValueNames())
				{
					if(strKey == "ProductId")
					{
						return (string)rgkWindowsCurrentVersion.GetValue("ProductId");
					}
				}
			}

			return "N/A";
		}
		# endregion

		/// <summary>
		///		Creates an encrypted activation request file for the customer on this system.
		/// </summary>
		/// <param name="strCustomerName">
		///		The customer's name.
		/// </param>
		/// <param name="strCustomerEmail">
		///		The customer's email.
		/// </param>
		/// <param name="strFilePath">
		///		The path where the activation request file will be saved.
		/// </param>
		/// <returns>
		///		TRUE if the file was created successfully, FALSE otherwise.
		/// </returns>
		public bool CreateActivationRequestFile(string strCustomerName, string strCustomerEmail)
		{
			byte[] bytCipherText;
			FileStream fstActivationFile;
			string strActivationRequestFile;
			StringWriter swWriter;
			XmlTextWriter xmltwWriter;
			XmlAttribute xmlaLicenseVersion, xmlaProductVersion;
			XmlDocument xmldActivationRequestFile;
			XmlNode xmlnCustomerInfo, xmlnLicenseKey, xmlnProductName, xmlnCustomerName, xmlnCustomerEmail, xmlnCustomerL1, xmlnCustomerL2, xmlnCustomerL3;
			
			// initalize variables
			swWriter = new StringWriter();
			xmldActivationRequestFile = new XmlDocument();
			xmltwWriter = new XmlTextWriter(swWriter);

			// create nodes
			xmlnCustomerInfo = xmldActivationRequestFile.CreateElement("CustomerInfo"); 

			xmlnLicenseKey = xmldActivationRequestFile.CreateElement( "LicenseKey" );
			xmlaLicenseVersion = xmldActivationRequestFile.CreateAttribute( "version" );
			xmlnProductName = xmldActivationRequestFile.CreateElement( "Product" );
			xmlaProductVersion = xmldActivationRequestFile.CreateAttribute( "version" );

			xmlnCustomerInfo = xmldActivationRequestFile.CreateElement( "CustomerInfo" );
			xmlnCustomerName = xmldActivationRequestFile.CreateElement( "CustomerName" );
			xmlnCustomerEmail = xmldActivationRequestFile.CreateElement( "CustomerEmail" );
			xmlnCustomerL1 = xmldActivationRequestFile.CreateElement( "CustomerL1" );
			xmlnCustomerL2 = xmldActivationRequestFile.CreateElement( "CustomerL2" );
			xmlnCustomerL3 = xmldActivationRequestFile.CreateElement( "CustomerL3" );
			
			// construct XML document
			xmldActivationRequestFile.AppendChild( xmlnLicenseKey );

			xmlnLicenseKey.AppendChild( xmlnProductName );
			xmlnLicenseKey.AppendChild( xmlnCustomerInfo );
			xmlnLicenseKey.Attributes.Append( xmlaLicenseVersion );

			xmlnProductName.Attributes.Append( xmlaProductVersion );

			xmlnCustomerInfo.AppendChild( xmlnCustomerName );
			xmlnCustomerInfo.AppendChild( xmlnCustomerEmail );
			xmlnCustomerInfo.AppendChild( xmlnCustomerL1 );
			xmlnCustomerInfo.AppendChild( xmlnCustomerL2 );
			xmlnCustomerInfo.AppendChild( xmlnCustomerL3 );
						
			// assign values to the nodes
			xmlnProductName.InnerText = this.ProductName;
			xmlnCustomerName.InnerText = strCustomerName;
			xmlnCustomerEmail.InnerText = strCustomerEmail;
			xmlnCustomerL1.InnerText = this.MotherboardSerial;
			xmlnCustomerL2.InnerText = this.WindowsVersion;
			xmlnCustomerL3.InnerText = this.WindowsProductID;
			xmlaProductVersion.Value = this.ProductVersion; //System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.Major.ToString();
			xmlaLicenseVersion.Value = this.ActivationVersion;
			
			// write the XML file to a string
			xmldActivationRequestFile.WriteTo(xmltwWriter);
			strActivationRequestFile = swWriter.ToString();

			try
			{
				// encrypt the activation file
				bytCipherText = clsUtilities.Encrypt(strActivationRequestFile);
			
				// write the encrypted file to disk
				fstActivationFile = new FileStream(this.ProductInstallationPath + "\\" + m_cstrActivationRequestFile,FileMode.Create);
				fstActivationFile.Write(bytCipherText,0,bytCipherText.Length);
				fstActivationFile.Close();
			}
			catch
			{
				return false;
			}

			return true;
		}

		/// <summary>
		///		Checks if the give activation key file is valid.
		/// </summary>
		/// <param name="strFileName">
		///		Path of the activation key file.
		/// </param>
		/// <returns>
		///		TRUE if the activation key is valid, FALSE otherwise.
		/// </returns>
		public bool IsLicenseValid()
		{
			byte[] bytSignature, bytData;
			bool blnResult = false;
			FileStream fstActivationKey;
			RSACryptoServiceProvider rsacspAsymetric = new RSACryptoServiceProvider();
			SHA1CryptoServiceProvider sha1CSP = new SHA1CryptoServiceProvider();
			StringBuilder sbString = new StringBuilder();
			
			// create string to be signed and convert to byte array
			sbString.Append( this.ProductName );
			sbString.Append( this.ProductVersion );
			sbString.Append( this.MotherboardSerial );
			sbString.Append( this.WindowsVersion );
			sbString.Append( this.WindowsProductID );
			bytData =  Encoding.UTF8.GetBytes( sbString.ToString() );

			try
			{	
				// open activation key file and extract signature
				fstActivationKey = new FileStream(this.ProductInstallationPath + "\\" + m_cstrActivationKeyFile,FileMode.Open,FileAccess.Read);
				bytSignature = new byte[fstActivationKey.Length];
				fstActivationKey.Read(bytSignature,0,(int)fstActivationKey.Length);
				fstActivationKey.Close();

				// load key
				rsacspAsymetric.FromXmlString("<RSAKeyValue><Modulus>u2hVxeMfQO2N67yK0rHsTbQnC3bN5fDv7WDoyUJb4/oQyK0/EEVoAE7rN82LbxFwJBJUHTdzGf9cS50kxkpcYCW42iXZMr2t+Sst8TAiV5sU0SokzL2JYJ/KgNBSgbnpmmjxpkzmUDmw98k7XFM3L6TKQN3rVJ/69oM7f4gFSt9uITGbQjUZtNnZeyCwwUl5NK4+hZMlNfDi+3jAOSq7imBuf6bkt4+L0IUPjF7nk1GWdGyLoVQWq+93CBNnyY9YVfNZFQjdXl1djC4CTzf6wxUT2HiXeYDa9mkfixaqvhRHVrXqnG8SIDxbT05mPHuvkmLuEBaRWrcowIV34pC2Qw==</Modulus><Exponent>AQAB</Exponent></RSAKeyValue>");

				blnResult = rsacspAsymetric.VerifyData( bytData, sha1CSP, bytSignature );
			}
			catch
			{
				blnResult = false;
			}

			return blnResult;
		}
	}
}
