using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using Utilities;

namespace OrderQuery
{
    public partial class OrderQuery : UserControl
    {
        public delegate void        NewSelectedOrderNumberHandler(string strSelectedOrderNumber, DateTime dtOrderDate, SupplierInformation siSupplier);
        public event                NewSelectedOrderNumberHandler OnNewSelectedOrderNumber;

        public delegate void        NoOrdersFound();
        public event                NoOrdersFound OnNoOrdersFound;


        public enum OrderQueryCaller : int { Backorders, Backorders_ReadOnly };
        private enum SearchOrders : int { All, BySupplier, ByEmployee };

        private bool                m_blnIsDbConnected, m_blnReadOnly;
        private DataTable           m_dtaEmployees, m_dtaSuppliers, m_dtaOrders;
        private DateTime            m_dtStart, m_dtEnd;
        private DateTimeFormatInfo  m_dtfiCurrentCulture;
        private OleDbConnection     m_odcConnection;
        private OrderQueryCaller    m_oqcCaller;
        private SearchOrders        m_soCurrentFilter;
        private string              m_strDateFormat_Display;
        private string              m_strApplicationTitle;
        private string              m_strQueriedOrderNumber;
        private string              m_strSelectedOrderNumber;
        private SupplierInformation m_siSupplier;

        public OrderQuery()
        {
            InitializeComponent();

            // initialize variables
            m_blnIsDbConnected = false;
            m_dtStart = m_dtEnd = DateTime.Now;
            m_soCurrentFilter = SearchOrders.All;

            // parse date formatting string
            m_dtfiCurrentCulture = CultureInfo.CurrentCulture.DateTimeFormat;
            m_strDateFormat_Display = "dd" + m_dtfiCurrentCulture.DateSeparator +
                                      "MM" + m_dtfiCurrentCulture.DateSeparator +
                                      "yyyy";

            // retrieve product title
            System.Reflection.Assembly assembly = System.Reflection.Assembly.GetExecutingAssembly();
            System.Reflection.AssemblyProductAttribute apaProductTitle = assembly.GetCustomAttributes(typeof(System.Reflection.AssemblyProductAttribute), false)[0] as System.Reflection.AssemblyProductAttribute;
            m_strApplicationTitle = apaProductTitle.Product;

            this.ReadOnly = false;
        }

        #region Events
        private void OrderQuery_Load(object sender, EventArgs e)
        {
            // set initial time period
            this.cmbTimePeriod.SelectedIndex = 3;
        }

        private void cmbOrderedBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearFields(false, SearchOrders.ByEmployee);

            if (this.cmbOrderedBy.SelectedIndex != -1)
                this.GetOrderNumbers(SearchOrders.ByEmployee);
        }

        private void cmbSupplier_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ClearFields(false, SearchOrders.BySupplier);

            if (this.cmbSupplier.SelectedIndex != -1)
                this.GetOrderNumbers(SearchOrders.BySupplier);
        }

        private void cmbTimePeriod_SelectedIndexChanged(object sender, EventArgs e)
        {
            // get current date/time
            DateTime dtNow;
            int intMonth, intYear;
            TimeSpan tsSpan;

            // init
            dtNow = DateTime.Now;

            // compute new time period based on user selection
            switch (this.cmbTimePeriod.SelectedIndex)
            {
                // previous year
                case 0:
                    intYear = dtNow.Year > 0 ? dtNow.Year - 1 : 0;

                    m_dtStart = new DateTime(intYear, 1, 1);
                    m_dtEnd = new DateTime(intYear, 12, 31);
                break;

                // previous month
                case 1:
                    intYear = dtNow.Year;

                    if (dtNow.Month > 1)
                        intMonth = dtNow.Month - 1;
                    else
                    {
                        intMonth = 12;
                        intYear = intYear > 0 ? --intYear : 0;
                    }

                    m_dtStart = new DateTime(intYear, intMonth, 1);
                    m_dtEnd = new DateTime(intYear, intMonth, GetLastDayMonth(intMonth, intYear));
                break;

                // previous week
                case 2:
                    dtNow = dtNow.Subtract(new TimeSpan(7, 0, 0, 0));

                    tsSpan = new TimeSpan(GetDeltaFirstDayWeek(dtNow.DayOfWeek), 0, 0, 0);
                    m_dtStart = dtNow.Subtract(tsSpan);

                    m_dtEnd = m_dtStart.AddDays(6);
                break;

                // current week
                case 3:
                    tsSpan = new TimeSpan(GetDeltaFirstDayWeek(dtNow.DayOfWeek), 0, 0, 0);
                    m_dtStart = dtNow.Subtract(tsSpan);

                    m_dtEnd = dtNow;
                break;

                // current month
                case 4:
                    tsSpan = new TimeSpan(dtNow.Day - 1, 0, 0, 0);
                    m_dtStart = dtNow.Subtract(tsSpan);

                    m_dtEnd = dtNow;
                break;

                // current year
                case 5:
                    tsSpan = new TimeSpan(dtNow.DayOfYear - 1, 0, 0, 0);
                    m_dtStart = dtNow.Subtract(tsSpan);

                    m_dtEnd = dtNow;
                break;

                // custom time period
                case 6:
                    fclsCustomPeriod frmCustomTimePeriod = new fclsCustomPeriod(this);
                    frmCustomTimePeriod.ShowDialog();
                break;

                default:
                    return;
            }
            
            // show selected time period
            this.DisplayTimePeriod();

            // get matching orders
            if(m_blnIsDbConnected)
                this.GetOrderNumbers(m_soCurrentFilter);
        }

        private void lbxOrderNumber_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.ShowSelectedOrder();
        }

        private void optOrderNumber_Click(object sender, EventArgs e)
        {
            // Clear field of past data
            this.ClearFields(true, SearchOrders.All);

            // Clear and disable Employee combo box
            this.cmbOrderedBy.DropDownStyle = ComboBoxStyle.DropDown;
            this.cmbOrderedBy.Items.Clear();
            this.cmbOrderedBy.Enabled = false;

            // Clear and disable Supplier combo box
            this.cmbSupplier.DropDownStyle = ComboBoxStyle.DropDown;
            this.cmbSupplier.Items.Clear();
            this.cmbSupplier.Enabled = false;

            this.GetOrderNumbers(SearchOrders.All);
        }

        private void optSupplier_Click(object sender, EventArgs e)
        {
            // Clear fields of past data
            this.ClearFields(true, SearchOrders.BySupplier);

            // Add items to combo box and enable it
            this.cmbSupplier.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 0; i < m_dtaSuppliers.Rows.Count; i++)
                this.cmbSupplier.Items.Add(m_dtaSuppliers.Rows[i]["CompanyName"].ToString());
            this.cmbSupplier.Enabled = true;

            // Clear and disable Employee combo box
            this.cmbOrderedBy.DropDownStyle = ComboBoxStyle.DropDown;
            this.cmbOrderedBy.Items.Clear();
            this.cmbOrderedBy.Enabled = false;

            if (m_dtaSuppliers.Rows.Count > 0)
                this.cmbSupplier.SelectedIndex = 0;
        }

        private void optOrderedBy_Click(object sender, EventArgs e)
        {
            DataRow dtrRow;

            // Clear fields of past data
            this.ClearFields(true, SearchOrders.ByEmployee);

            // Clear, add items to and enable combobox
            this.cmbOrderedBy.DropDownStyle = ComboBoxStyle.DropDownList;
            for (int i = 0; i < m_dtaEmployees.Rows.Count; i++)
            {
                dtrRow = m_dtaEmployees.Rows[i];
                this.cmbOrderedBy.Items.Add(Utilities.clsUtilities.FormatName_List(dtrRow["Title"].ToString(), dtrRow["FirstName"].ToString(), dtrRow["LastName"].ToString()));
            }
            this.cmbOrderedBy.Enabled = true;

            // Clear and disable Supplier combo box
            this.cmbSupplier.DropDownStyle = ComboBoxStyle.DropDown;
            this.cmbSupplier.Items.Clear();
            this.cmbSupplier.Enabled = false;

            if (m_dtaEmployees.Rows.Count > 0)
                this.cmbOrderedBy.SelectedIndex = 0;
        }
        #endregion

        #region Methods
        private void ClearFields(bool blnOptionButton, SearchOrders soSearchBy)
        {
            switch (soSearchBy)
            {
                case SearchOrders.All:
                    this.cmbOrderedBy.Text = "";
                    this.cmbSupplier.Text = "";
                break;

                case SearchOrders.ByEmployee:
                    this.cmbSupplier.Text = "";
                    if (blnOptionButton)
                        this.cmbOrderedBy.Text = "";
                break;

                case SearchOrders.BySupplier:
                    this.cmbOrderedBy.Text = "";
                    if (blnOptionButton)
                        this.cmbSupplier.Text = "";
                break;
            }

            this.lbxOrderNumber.Items.Clear();
        }

        public bool Initialize(OleDbConnection odcConnection, OrderQueryCaller oqcCaller)
        {
            OleDbDataAdapter oddaTemp;

            if (odcConnection != null)
            {
                m_odcConnection = odcConnection;
                m_oqcCaller = oqcCaller;

                m_dtaEmployees = new DataTable();
                m_dtaSuppliers = new DataTable();

                try
                {
                    // Load employees and populate combo box
                    oddaTemp = new OleDbDataAdapter("SELECT * FROM [Employees] ORDER BY LastName", m_odcConnection);
                    oddaTemp.Fill(m_dtaEmployees);

                    // Load suppliers and populate combo box
                    oddaTemp = new OleDbDataAdapter("SELECT * FROM [Suppliers] ORDER BY CompanyName", m_odcConnection);
                    oddaTemp.Fill(m_dtaSuppliers);

                    m_blnIsDbConnected = true;

                    this.GetOrderNumbers(SearchOrders.All);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while accesing the database:\n" + ex.Message,
                                    m_strApplicationTitle,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }

            return m_blnIsDbConnected;
        }

        public bool Initialize(OleDbConnection odcConnection, OrderQueryCaller oqcCaller, string strQueriedOrderNumber)
        {
            m_strQueriedOrderNumber = strQueriedOrderNumber;
            
            return this.Initialize(odcConnection, oqcCaller);
        }

        private void DisplayTimePeriod()
        {
            // display new time period
            this.lblTimePeriod_Data.Text = m_dtStart.ToString(m_strDateFormat_Display) + " - " + m_dtEnd.ToString(m_strDateFormat_Display);
        }

        /// <summary>
        ///		Finds the employee associated with the supplied 'EmployeeId'
        /// </summary>
        /// <returns>
        ///		Returns the employee's name.
        /// </returns>		
        private string GetEmployee(int intEmployeeId)
        {
            DataRow dtrRow;
            int intCurrentEmployeeId = -1;
            string strEmployee = "";

            for (int i = 0; i < m_dtaEmployees.Rows.Count; i++)
            {
                intCurrentEmployeeId = int.Parse(m_dtaEmployees.Rows[i]["EmployeeId"].ToString());
                if (intCurrentEmployeeId == intEmployeeId)
                {
                    dtrRow = m_dtaEmployees.Rows[i];
                    strEmployee = clsUtilities.FormatName_List(dtrRow["Title"].ToString(), dtrRow["FirstName"].ToString(), dtrRow["LastName"].ToString());
                    break;
                }
            }

            return strEmployee;
        }

        private int GetDeltaFirstDayWeek(DayOfWeek dowWeekDay)
        {
            int intNDays;

            // figure out how many days need to be subtracted in order to reach the start of the week
            // (set by current culture info)
            if ((int)dowWeekDay < (int)m_dtfiCurrentCulture.FirstDayOfWeek)
                intNDays = 7 - ((int)m_dtfiCurrentCulture.FirstDayOfWeek - (int)dowWeekDay);
            else
                intNDays = (int)dowWeekDay - (int)m_dtfiCurrentCulture.FirstDayOfWeek;

            return intNDays;
        }

        private int GetLastDayMonth(int Month, int Year)
        { 
            switch(Month)
            {
                case 1:
                case 3:
                case 5:
                case 7:
                case 8:
                case 10:
                case 12:
                    return 31;

                case 4:
                case 6:
                case 9:
                case 11:
                    return 30;

                case 2:
                    if (this.IsLeapYear(Year))
                        return 29;
                    else
                        return 28;
            }

            return -1;
        }

        /// <summary>
        ///		Populates lbxOrderNumber with Order Numbers depending on soSearchBy.
        /// </summary>
        private void GetOrderNumbers(SearchOrders soSearchBy)
        {
            // Variable declaration
            CultureInfo ciCurrentCulture;
            OleDbDataAdapter odaOrders;
            string strEmployeeId, strSupplierId, strQuery;
            string strStartDate, strEndDate;

            if (m_blnIsDbConnected)
            {
                // Variable initialization
                ciCurrentCulture = (CultureInfo)CultureInfo.CurrentCulture.Clone();
                ciCurrentCulture.DateTimeFormat.DateSeparator = "/";
                m_dtaOrders = new DataTable();
                strQuery = "";

                // Clear order numbers listbox and order listview
                this.lbxOrderNumber.Items.Clear();

                // Get time period
                strStartDate = m_dtStart.ToString(Utilities.clsUtilities.FORMAT_DATE_QUERY, ciCurrentCulture);
                strEndDate = m_dtEnd.ToString(Utilities.clsUtilities.FORMAT_DATE_QUERY, ciCurrentCulture);

                // Save current filter for reference by date pickers
                m_soCurrentFilter = soSearchBy;

                // 
                // parse query
                //
                // first part depends only on the type of search being performed
                switch (soSearchBy)
                {
                    case SearchOrders.All:
                        strQuery = "SELECT DISTINCT OrderId, OrderDate, FournisseurId, EmployeeId " +
                                   "FROM Orders " +
                                   "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#))";
                        break;

                    case SearchOrders.ByEmployee:
                        // Get selected employee
                        strEmployeeId = m_dtaEmployees.Rows[this.cmbOrderedBy.SelectedIndex]["EmployeeId"].ToString();

                        strQuery = "SELECT DISTINCT OrderId, OrderDate, FournisseurId, EmployeeId " +
                                   "FROM Orders " +
                                   "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND EmployeeId = " + strEmployeeId;
                        break;

                    case SearchOrders.BySupplier:
                        // Get selected supplier
                        strSupplierId = m_dtaSuppliers.Rows[this.cmbSupplier.SelectedIndex]["FournisseurId"].ToString();

                        strQuery = "SELECT DISTINCT OrderId, OrderDate, FournisseurId, EmployeeId " +
                                   "FROM Orders " +
                                   "WHERE (((Orders.OrderDate) BETWEEN #" + strStartDate + "# AND #" + strEndDate + "#)) AND FournisseurId = " + strSupplierId;
                        break;
                }

                // second part depends on the owner form
                switch (m_oqcCaller)
                {
                    case OrderQueryCaller.Backorders:
                        strQuery += " AND BackOrderUnits > 0";
                    break;

                    case OrderQueryCaller.Backorders_ReadOnly:
                        strQuery = "SELECT DISTINCT OrderId, OrderDate, FournisseurId, EmployeeId " +
                                   "FROM Orders " +
                                   "WHERE OrderId = '" + m_strQueriedOrderNumber + "'";
                    break;
                }

                // third part is common to all queries
                strQuery += " ORDER BY OrderDate";

                //
                // Get orders from database
                //
                try
                {
                    odaOrders = new OleDbDataAdapter(strQuery, m_odcConnection);
                    odaOrders.Fill(m_dtaOrders);

                    if (m_dtaOrders.Rows.Count != 0)
                    {
                        this.lblNoOrdersFound.Visible = false;

                        for (int i = 0; i < m_dtaOrders.Rows.Count; i++)
                            this.lbxOrderNumber.Items.Add(m_dtaOrders.Rows[i]["OrderId"]);

                        this.lbxOrderNumber.SelectedIndex = this.lbxOrderNumber.Items.Count - 1;
                    }
                    else
                    {
                        this.lblNoOrdersFound.Visible = true;

                        if (OnNoOrdersFound != null)
                            OnNoOrdersFound();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occured while accesing the database:\n" + ex.Message,
                                    m_strApplicationTitle,
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Error);
                }
            }
        }

        /// <summary>
        ///		Finds the supplier associated with the supplied 'FournisseurId'
        /// </summary>
        /// <returns>
        ///		Returns the supplier's company name.
        /// </returns>
        private string GetSupplier(int intSupplierId)
        {
            int intCurrentSupplierId = -1, i;
            string strSupplier = "";

            for (i = 0; i < m_dtaSuppliers.Rows.Count; i++)
            {
                intCurrentSupplierId = int.Parse(m_dtaSuppliers.Rows[i]["FournisseurId"].ToString());
                if (intCurrentSupplierId == intSupplierId)
                {
                    strSupplier = m_dtaSuppliers.Rows[i]["CompanyName"].ToString();
                    break;
                }
            }

            // Set supplier information for fclsOMCheckOrders_ReturnProd
            m_siSupplier.DatabaseID = int.Parse(m_dtaSuppliers.Rows[i]["FournisseurId"].ToString());
            m_siSupplier.Name = m_dtaSuppliers.Rows[i]["CompanyName"].ToString();
            m_siSupplier.ContactName = clsUtilities.FormatName_Display(m_dtaSuppliers.Rows[i]["ConTitle"].ToString(), m_dtaSuppliers.Rows[i]["ContactFirstName"].ToString(), m_dtaSuppliers.Rows[i]["ContactLastName"].ToString());
            m_siSupplier.PhoneNumber = m_dtaSuppliers.Rows[i]["PhoneNumber"].ToString();
            m_siSupplier.Email = m_dtaSuppliers.Rows[i]["Email"].ToString();

            return strSupplier;
        }

        private bool IsLeapYear(int Year)
        {
            // any year that is evenly divisible by 4 is a leap year
            if ((Year % 4) == 0)
            {
                // a year that is evenly divisible by 100 is a leap year only if it is also evenly divisible by 400
                if ((Year % 100) == 0)
                {
                    if ((Year % 400) == 0)
                        return true;
                    else
                        return false;
                }
                else
                    return true;
            }

            return false;
        }

        private void ReadOnlyChanged()
        {
            this.scCriteriaFields.Enabled = !m_blnReadOnly;
            
            if (m_blnReadOnly)
                this.lblTimePeriod_Data.Text = "";
        }

        public void SetTimePeriod(DateTime dtStart, DateTime dtEnd)
        {
            m_dtStart = dtStart;
            m_dtEnd = dtEnd;

            this.DisplayTimePeriod();
        }

        private void ShowSelectedOrder()
        {
            // Variable declaration
            DataRow dtrRow;

            if ((this.lbxOrderNumber.Items.Count > 0) && (this.lbxOrderNumber.SelectedIndex != -1))
            {
                m_strSelectedOrderNumber = this.lbxOrderNumber.SelectedItem.ToString();

                // Get the selected order from the datatable
                dtrRow = m_dtaOrders.Rows[this.lbxOrderNumber.SelectedIndex];

                // Display the emplyee and/or the supplier associated with the current order
                switch (m_soCurrentFilter)
                {
                    case SearchOrders.All:
                        this.cmbOrderedBy.Text = this.GetEmployee(int.Parse(dtrRow["EmployeeId"].ToString()));
                        this.cmbSupplier.Text = this.GetSupplier(int.Parse(dtrRow["FournisseurId"].ToString()));
                    break;

                    case SearchOrders.ByEmployee:
                        this.cmbSupplier.Text = this.GetSupplier(int.Parse(dtrRow["FournisseurId"].ToString()));
                    break;

                    case SearchOrders.BySupplier:
                        this.cmbOrderedBy.Text = this.GetEmployee(int.Parse(dtrRow["EmployeeId"].ToString()));
                    break;
                }

                // store order date
                m_strSelectedOrderNumber = dtrRow["OrderId"].ToString();

                // invoke calling form with order number and order date
                if (OnNewSelectedOrderNumber != null)
                    OnNewSelectedOrderNumber(m_strSelectedOrderNumber, (DateTime)dtrRow["OrderDate"], m_siSupplier);
            }
        }
        #endregion

        #region Properties
        public bool ReadOnly
        {
            get
            {
                return m_blnReadOnly;
            }
            set
            {
                m_blnReadOnly = value;
                ReadOnlyChanged();
            }
        }
        public string SelectedOrderNumber
        {
            get
            { 
                return m_strSelectedOrderNumber;
            }
        }
        #endregion

        private void oqOrderSearch_OnNewSelectedOrderNumber(string strSelectedOrderNumber, DateTime dtOrderDate)
        {

        }
    }
}
