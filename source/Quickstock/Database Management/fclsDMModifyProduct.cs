using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Data.OleDb;
using Utilities;

namespace DSMS
{
	/// <summary>
	/// Summary description for frmProductInfo.
	/// </summary>
	public class fclsDMModifyProduct : System.Windows.Forms.Form
	{
		private System.Windows.Forms.Button cmdClose;
		private System.Windows.Forms.ContextMenu ctmRightClick;
		private System.Windows.Forms.MenuItem mnuAdd;
		private System.Windows.Forms.MenuItem mnuActive;
		private System.Windows.Forms.MenuItem mnuModify;
		private System.Windows.Forms.MenuItem mnuRemove;
		private System.Windows.Forms.RadioButton optSearchByCategory;
		private System.Windows.Forms.ListBox lbxCategories;
		private System.Windows.Forms.ListBox lbxSubProducts;
		private System.Windows.Forms.ListBox lbxProducts;
		private System.Windows.Forms.RadioButton optSearchByProduct;
		private System.Windows.Forms.Label lblSubProducts;
		private System.Windows.Forms.Label lblProducts;
		private System.Windows.Forms.Label lblCategory;
		private System.Windows.Forms.Button btnHelp;
		private System.Windows.Forms.Panel pnlCategoryButtons;
		private System.Windows.Forms.Button btnDeleteCategory;
		private System.Windows.Forms.Button btnModifyCategory;
		private System.Windows.Forms.Button btnAddCategory;
		private System.Windows.Forms.Panel pnlProductsButtons;
		private System.Windows.Forms.Button btnDelProd;
		private System.Windows.Forms.Button btnModProd;
		private System.Windows.Forms.Button btnAddProd;
		private System.Windows.Forms.Panel pnlSubProductsButtons;
		private System.Windows.Forms.Button btnDelSubProd;
		private System.Windows.Forms.Button btnAddSubProd;
		private System.Windows.Forms.Button btnModSubProd;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		private bool							m_blnCategoriesLoaded, m_blnProductsLoaded, m_blnSubProductsLoaded, m_blnSearchByCategory;
		private double							m_dblCategoryLbxWProportion, m_dblProductLbxWProportion, m_dblSubProductLbxWProportion;
		private DataTable						m_dtaCategories, m_dtaProducts, m_dtaSubProducts, m_dtaTrademarks;
		private fclsDMModifyProduct_Products	frmDMModifyProduct_Products;
		private int								m_intCategoryId, m_intProductId, m_intSubProductId;
		private int								m_intCategoriesSelectedIndex, m_intProductsSelectedIndex, m_intSubProductsSelectedIndex;
		private OleDbDataAdapter				m_odaCategories, m_odaProducts, m_odaSubProducts;
		private OleDbConnection					m_odcConnection;

		private String m_strCurrentListBox;


		public fclsDMModifyProduct(OleDbConnection odcConnection)
		{
			InitializeComponent();
			
			// initialize global variables
			m_blnCategoriesLoaded = m_blnProductsLoaded = m_blnSubProductsLoaded = false;
			m_blnSearchByCategory = true;
			m_dblCategoryLbxWProportion = ((double) this.lbxCategories.Size.Width) / this.Width;
			m_dblProductLbxWProportion = ((double) this.lbxProducts.Size.Width) / this.Width;
			m_dblSubProductLbxWProportion = ((double) this.lbxSubProducts.Size.Width) / this.Width;
			m_intCategoryId = m_intProductId = m_intSubProductId = -1;
			m_intCategoriesSelectedIndex = m_intProductsSelectedIndex = m_intSubProductsSelectedIndex = -1;
			m_odcConnection = odcConnection;
			frmDMModifyProduct_Products = new fclsDMModifyProduct_Products(m_odcConnection);

			//Load Trademarks
			this.LoadTrademarks();

			//Fill Category ComboBox
			this.GetCurrentIds();
			this.LoadData("Categories",-1);
			this.optSearchByCategory.Checked = true;
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.ctmRightClick = new System.Windows.Forms.ContextMenu();
            this.mnuActive = new System.Windows.Forms.MenuItem();
            this.mnuAdd = new System.Windows.Forms.MenuItem();
            this.mnuModify = new System.Windows.Forms.MenuItem();
            this.mnuRemove = new System.Windows.Forms.MenuItem();
            this.cmdClose = new System.Windows.Forms.Button();
            this.optSearchByCategory = new System.Windows.Forms.RadioButton();
            this.lbxCategories = new System.Windows.Forms.ListBox();
            this.lblSubProducts = new System.Windows.Forms.Label();
            this.lblProducts = new System.Windows.Forms.Label();
            this.lbxSubProducts = new System.Windows.Forms.ListBox();
            this.lbxProducts = new System.Windows.Forms.ListBox();
            this.lblCategory = new System.Windows.Forms.Label();
            this.optSearchByProduct = new System.Windows.Forms.RadioButton();
            this.btnHelp = new System.Windows.Forms.Button();
            this.pnlCategoryButtons = new System.Windows.Forms.Panel();
            this.btnDeleteCategory = new System.Windows.Forms.Button();
            this.btnModifyCategory = new System.Windows.Forms.Button();
            this.btnAddCategory = new System.Windows.Forms.Button();
            this.pnlProductsButtons = new System.Windows.Forms.Panel();
            this.btnDelProd = new System.Windows.Forms.Button();
            this.btnModProd = new System.Windows.Forms.Button();
            this.btnAddProd = new System.Windows.Forms.Button();
            this.pnlSubProductsButtons = new System.Windows.Forms.Panel();
            this.btnDelSubProd = new System.Windows.Forms.Button();
            this.btnAddSubProd = new System.Windows.Forms.Button();
            this.btnModSubProd = new System.Windows.Forms.Button();
            this.pnlCategoryButtons.SuspendLayout();
            this.pnlProductsButtons.SuspendLayout();
            this.pnlSubProductsButtons.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctmRightClick
            // 
            this.ctmRightClick.MenuItems.AddRange(new System.Windows.Forms.MenuItem[] {
            this.mnuActive,
            this.mnuAdd,
            this.mnuModify,
            this.mnuRemove});
            this.ctmRightClick.Popup += new System.EventHandler(this.ctmRightClick_Popup);
            // 
            // mnuActive
            // 
            this.mnuActive.Index = 0;
            this.mnuActive.RadioCheck = true;
            this.mnuActive.Text = "A&ctive";
            this.mnuActive.Click += new System.EventHandler(this.mnuActive_Click);
            // 
            // mnuAdd
            // 
            this.mnuAdd.Index = 1;
            this.mnuAdd.Text = "&Add";
            this.mnuAdd.Click += new System.EventHandler(this.mnuAdd_Click);
            // 
            // mnuModify
            // 
            this.mnuModify.Index = 2;
            this.mnuModify.Text = "Modify";
            this.mnuModify.Click += new System.EventHandler(this.mnuModify_Click);
            // 
            // mnuRemove
            // 
            this.mnuRemove.Index = 3;
            this.mnuRemove.Text = "Remove";
            this.mnuRemove.Click += new System.EventHandler(this.mnuRemove_Click);
            // 
            // cmdClose
            // 
            this.cmdClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.cmdClose.Location = new System.Drawing.Point(703, 268);
            this.cmdClose.Name = "cmdClose";
            this.cmdClose.Size = new System.Drawing.Size(96, 32);
            this.cmdClose.TabIndex = 39;
            this.cmdClose.Text = "Close";
            this.cmdClose.Click += new System.EventHandler(this.cmdClose_Click);
            // 
            // optSearchByCategory
            // 
            this.optSearchByCategory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.optSearchByCategory.Location = new System.Drawing.Point(316, 8);
            this.optSearchByCategory.Name = "optSearchByCategory";
            this.optSearchByCategory.Size = new System.Drawing.Size(132, 24);
            this.optSearchByCategory.TabIndex = 47;
            this.optSearchByCategory.Text = "Search by Category";
            this.optSearchByCategory.CheckedChanged += new System.EventHandler(this.optSearchByCategory_CheckedChanged);
            // 
            // lbxCategories
            // 
            this.lbxCategories.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbxCategories.ContextMenu = this.ctmRightClick;
            this.lbxCategories.Location = new System.Drawing.Point(8, 64);
            this.lbxCategories.Name = "lbxCategories";
            this.lbxCategories.Size = new System.Drawing.Size(297, 160);
            this.lbxCategories.TabIndex = 54;
            this.lbxCategories.SelectedIndexChanged += new System.EventHandler(this.lbxCategories_SelectedIndexChanged);
            this.lbxCategories.MouseEnter += new System.EventHandler(this.lbxCategories_MouseEnter);
            // 
            // lblSubProducts
            // 
            this.lblSubProducts.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblSubProducts.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSubProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblSubProducts.Location = new System.Drawing.Point(712, 40);
            this.lblSubProducts.Name = "lblSubProducts";
            this.lblSubProducts.Size = new System.Drawing.Size(101, 20);
            this.lblSubProducts.TabIndex = 53;
            this.lblSubProducts.Text = "Sub-Products";
            this.lblSubProducts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblProducts
            // 
            this.lblProducts.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblProducts.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblProducts.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblProducts.Location = new System.Drawing.Point(425, 40);
            this.lblProducts.Name = "lblProducts";
            this.lblProducts.Size = new System.Drawing.Size(69, 20);
            this.lblProducts.TabIndex = 52;
            this.lblProducts.Text = "Products";
            this.lblProducts.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbxSubProducts
            // 
            this.lbxSubProducts.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbxSubProducts.ContextMenu = this.ctmRightClick;
            this.lbxSubProducts.Location = new System.Drawing.Point(614, 64);
            this.lbxSubProducts.Name = "lbxSubProducts";
            this.lbxSubProducts.Size = new System.Drawing.Size(297, 160);
            this.lbxSubProducts.TabIndex = 50;
            this.lbxSubProducts.SelectedIndexChanged += new System.EventHandler(this.lbxSubProducts_SelectedIndexChanged);
            this.lbxSubProducts.MouseEnter += new System.EventHandler(this.lbxSubProducts_MouseEnter);
            // 
            // lbxProducts
            // 
            this.lbxProducts.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbxProducts.ContextMenu = this.ctmRightClick;
            this.lbxProducts.Location = new System.Drawing.Point(311, 64);
            this.lbxProducts.Name = "lbxProducts";
            this.lbxProducts.Size = new System.Drawing.Size(297, 160);
            this.lbxProducts.TabIndex = 49;
            this.lbxProducts.SelectedIndexChanged += new System.EventHandler(this.lbxProducts_SelectedIndexChanged);
            this.lbxProducts.MouseEnter += new System.EventHandler(this.lbxProducts_MouseEnter);
            // 
            // lblCategory
            // 
            this.lblCategory.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lblCategory.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCategory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(192)))));
            this.lblCategory.Location = new System.Drawing.Point(121, 40);
            this.lblCategory.Name = "lblCategory";
            this.lblCategory.Size = new System.Drawing.Size(70, 20);
            this.lblCategory.TabIndex = 51;
            this.lblCategory.Text = "Category";
            this.lblCategory.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // optSearchByProduct
            // 
            this.optSearchByProduct.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.optSearchByProduct.Location = new System.Drawing.Point(500, 8);
            this.optSearchByProduct.Name = "optSearchByProduct";
            this.optSearchByProduct.Size = new System.Drawing.Size(116, 24);
            this.optSearchByProduct.TabIndex = 55;
            this.optSearchByProduct.Text = "Search by Product";
            this.optSearchByProduct.CheckedChanged += new System.EventHandler(this.optSearchByProduct_CheckedChanged);
            // 
            // btnHelp
            // 
            this.btnHelp.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHelp.Location = new System.Drawing.Point(815, 268);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(96, 32);
            this.btnHelp.TabIndex = 59;
            this.btnHelp.Text = "Help";
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // pnlCategoryButtons
            // 
            this.pnlCategoryButtons.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pnlCategoryButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlCategoryButtons.Controls.Add(this.btnDeleteCategory);
            this.pnlCategoryButtons.Controls.Add(this.btnModifyCategory);
            this.pnlCategoryButtons.Controls.Add(this.btnAddCategory);
            this.pnlCategoryButtons.Location = new System.Drawing.Point(8, 224);
            this.pnlCategoryButtons.Name = "pnlCategoryButtons";
            this.pnlCategoryButtons.Size = new System.Drawing.Size(297, 38);
            this.pnlCategoryButtons.TabIndex = 60;
            // 
            // btnDeleteCategory
            // 
            this.btnDeleteCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnDeleteCategory.Enabled = false;
            this.btnDeleteCategory.Location = new System.Drawing.Point(196, 0);
            this.btnDeleteCategory.Name = "btnDeleteCategory";
            this.btnDeleteCategory.Size = new System.Drawing.Size(97, 35);
            this.btnDeleteCategory.TabIndex = 60;
            this.btnDeleteCategory.Text = "Delete Category";
            this.btnDeleteCategory.Click += new System.EventHandler(this.btnDeleteCategory_Click);
            // 
            // btnModifyCategory
            // 
            this.btnModifyCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnModifyCategory.Enabled = false;
            this.btnModifyCategory.Location = new System.Drawing.Point(98, 0);
            this.btnModifyCategory.Name = "btnModifyCategory";
            this.btnModifyCategory.Size = new System.Drawing.Size(97, 35);
            this.btnModifyCategory.TabIndex = 61;
            this.btnModifyCategory.Text = "Modify Category";
            this.btnModifyCategory.Click += new System.EventHandler(this.btnModifyCategory_Click);
            // 
            // btnAddCategory
            // 
            this.btnAddCategory.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.btnAddCategory.Enabled = false;
            this.btnAddCategory.Location = new System.Drawing.Point(0, 0);
            this.btnAddCategory.Name = "btnAddCategory";
            this.btnAddCategory.Size = new System.Drawing.Size(97, 35);
            this.btnAddCategory.TabIndex = 59;
            this.btnAddCategory.Text = "Add Category";
            this.btnAddCategory.Click += new System.EventHandler(this.btnAddCategory_Click);
            // 
            // pnlProductsButtons
            // 
            this.pnlProductsButtons.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pnlProductsButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlProductsButtons.Controls.Add(this.btnDelProd);
            this.pnlProductsButtons.Controls.Add(this.btnModProd);
            this.pnlProductsButtons.Controls.Add(this.btnAddProd);
            this.pnlProductsButtons.Location = new System.Drawing.Point(311, 224);
            this.pnlProductsButtons.Name = "pnlProductsButtons";
            this.pnlProductsButtons.Size = new System.Drawing.Size(297, 38);
            this.pnlProductsButtons.TabIndex = 61;
            // 
            // btnDelProd
            // 
            this.btnDelProd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDelProd.Enabled = false;
            this.btnDelProd.Location = new System.Drawing.Point(196, 0);
            this.btnDelProd.Name = "btnDelProd";
            this.btnDelProd.Size = new System.Drawing.Size(97, 35);
            this.btnDelProd.TabIndex = 47;
            this.btnDelProd.Text = "Delete Product";
            this.btnDelProd.Click += new System.EventHandler(this.btnDelProd_Click);
            // 
            // btnModProd
            // 
            this.btnModProd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnModProd.Enabled = false;
            this.btnModProd.Location = new System.Drawing.Point(98, 0);
            this.btnModProd.Name = "btnModProd";
            this.btnModProd.Size = new System.Drawing.Size(97, 35);
            this.btnModProd.TabIndex = 48;
            this.btnModProd.Text = "Modify Product";
            this.btnModProd.Click += new System.EventHandler(this.btnModProd_Click);
            // 
            // btnAddProd
            // 
            this.btnAddProd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddProd.Enabled = false;
            this.btnAddProd.Location = new System.Drawing.Point(0, 0);
            this.btnAddProd.Name = "btnAddProd";
            this.btnAddProd.Size = new System.Drawing.Size(97, 35);
            this.btnAddProd.TabIndex = 46;
            this.btnAddProd.Text = "Add Product";
            this.btnAddProd.Click += new System.EventHandler(this.btnAddProd_Click);
            // 
            // pnlSubProductsButtons
            // 
            this.pnlSubProductsButtons.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pnlSubProductsButtons.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.pnlSubProductsButtons.Controls.Add(this.btnDelSubProd);
            this.pnlSubProductsButtons.Controls.Add(this.btnAddSubProd);
            this.pnlSubProductsButtons.Controls.Add(this.btnModSubProd);
            this.pnlSubProductsButtons.Location = new System.Drawing.Point(614, 224);
            this.pnlSubProductsButtons.Name = "pnlSubProductsButtons";
            this.pnlSubProductsButtons.Size = new System.Drawing.Size(297, 38);
            this.pnlSubProductsButtons.TabIndex = 62;
            // 
            // btnDelSubProd
            // 
            this.btnDelSubProd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnDelSubProd.Enabled = false;
            this.btnDelSubProd.Location = new System.Drawing.Point(196, 0);
            this.btnDelSubProd.Name = "btnDelSubProd";
            this.btnDelSubProd.Size = new System.Drawing.Size(97, 35);
            this.btnDelSubProd.TabIndex = 47;
            this.btnDelSubProd.Text = "Delete Sub-Product";
            this.btnDelSubProd.Click += new System.EventHandler(this.btnDelSubProd_Click);
            // 
            // btnAddSubProd
            // 
            this.btnAddSubProd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAddSubProd.Enabled = false;
            this.btnAddSubProd.Location = new System.Drawing.Point(0, 0);
            this.btnAddSubProd.Name = "btnAddSubProd";
            this.btnAddSubProd.Size = new System.Drawing.Size(97, 35);
            this.btnAddSubProd.TabIndex = 48;
            this.btnAddSubProd.Text = "Add Sub-Product";
            this.btnAddSubProd.Click += new System.EventHandler(this.btnAddSubProd_Click);
            // 
            // btnModSubProd
            // 
            this.btnModSubProd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnModSubProd.Enabled = false;
            this.btnModSubProd.Location = new System.Drawing.Point(98, 0);
            this.btnModSubProd.Name = "btnModSubProd";
            this.btnModSubProd.Size = new System.Drawing.Size(97, 35);
            this.btnModSubProd.TabIndex = 49;
            this.btnModSubProd.Text = "Modify Sub-Product";
            this.btnModSubProd.Click += new System.EventHandler(this.btnModSubProd_Click);
            // 
            // fclsDMModifyProduct
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
            this.ClientSize = new System.Drawing.Size(932, 312);
            this.Controls.Add(this.pnlSubProductsButtons);
            this.Controls.Add(this.pnlProductsButtons);
            this.Controls.Add(this.pnlCategoryButtons);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.optSearchByProduct);
            this.Controls.Add(this.lbxCategories);
            this.Controls.Add(this.lblSubProducts);
            this.Controls.Add(this.lblProducts);
            this.Controls.Add(this.lblCategory);
            this.Controls.Add(this.lbxSubProducts);
            this.Controls.Add(this.lbxProducts);
            this.Controls.Add(this.optSearchByCategory);
            this.Controls.Add(this.cmdClose);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "fclsDMModifyProduct";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quick Stock - Modify Categories / Products / Sub-Products";
            this.Resize += new System.EventHandler(this.fclsDMModifyProduct_Resize);
            this.pnlCategoryButtons.ResumeLayout(false);
            this.pnlProductsButtons.ResumeLayout(false);
            this.pnlSubProductsButtons.ResumeLayout(false);
            this.ResumeLayout(false);

		}
		#endregion

		private void LoadTrademarks()
		{
			OleDbDataAdapter odaTrademarks = new OleDbDataAdapter("SELECT * FROM Trademarks ORDER BY Trademark",m_odcConnection);
			m_dtaTrademarks = new DataTable();
			odaTrademarks.Fill(m_dtaTrademarks);
		}

		private void cmdClose_Click(object sender, System.EventArgs e)
		{
			this.Close();
		}

		private void lbxCategories_MouseEnter(object sender, System.EventArgs e)
		{
			m_strCurrentListBox = "Categories";
		}

		private void lbxProducts_MouseEnter(object sender, System.EventArgs e)
		{
			m_strCurrentListBox = "Products";
		}

		private void lbxSubProducts_MouseEnter(object sender, System.EventArgs e)
		{
			m_strCurrentListBox = "Sub-Products";
		}

		private void btnHelp_Click(object sender, System.EventArgs e)
		{
			Help.ShowHelp(btnHelp, Application.StartupPath + "//help//DSMS.chm","ModifyProducts.htm");  //

		}

		private void lbxCategories_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(m_blnSearchByCategory)
			{
				if(this.lbxCategories.SelectedIndex != -1)
				{
					m_intCategoriesSelectedIndex = this.lbxCategories.SelectedIndex;
					this.LoadData("Products",int.Parse(m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["CategoryId"].ToString()));
					this.btnModifyCategory.Enabled = true;
					this.btnDeleteCategory.Enabled = true;
				
					// disables products buttons
					this.btnModProd.Enabled = false;
					this.btnDelProd.Enabled = false;
				
					// clears sub-product listbox, disables buttons & menus
					this.lbxSubProducts.Items.Clear();
					this.btnAddSubProd.Enabled = false;
					this.btnModSubProd.Enabled = false;
					this.btnDelSubProd.Enabled = false;
					m_blnSubProductsLoaded = false;
				}
			}
		}

		private void lbxProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.lbxProducts.SelectedIndex != -1)
			{
				this.LoadData("Sub-Products",int.Parse(m_dtaProducts.Rows[this.lbxProducts.SelectedIndex]["MatId"].ToString()));
				m_intProductsSelectedIndex = this.lbxProducts.SelectedIndex;
				this.btnModProd.Enabled = true;
				this.btnDelProd.Enabled = true;

				// disable the sub-products buttons
				this.btnModSubProd.Enabled = false;
				this.btnDelSubProd.Enabled = false;

				if(!m_blnSearchByCategory)
				{
					string strCategoryId = m_dtaProducts.Rows[m_intProductsSelectedIndex]["CategoryId"].ToString();
					for(int i=0; i < this.m_dtaCategories.Rows.Count; i++)
					{
						if(clsUtilities.CompareStrings(m_dtaCategories.Rows[i]["CategoryId"].ToString(),strCategoryId))
							this.lbxCategories.SelectedIndex = i;
					}
					if(this.lbxCategories.SelectedIndex == -1)
						this.lbxCategories.SelectedIndex = 0;
				}
			}
		}

		private void lbxSubProducts_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(this.lbxSubProducts.SelectedIndex != -1)
			{
				m_intSubProductsSelectedIndex = this.lbxSubProducts.SelectedIndex;
				this.btnModSubProd.Enabled = true;
				this.btnDelSubProd.Enabled = true;
			}
		}

		private void ctmRightClick_Popup(object sender, System.EventArgs e)
		{
			if((!m_blnCategoriesLoaded && m_strCurrentListBox == "Categories") || (!m_blnProductsLoaded && m_strCurrentListBox == "Products") || (!m_blnSubProductsLoaded && m_strCurrentListBox == "Sub-Products"))
			{
				this.mnuActive.Enabled = false;
				this.mnuActive.Checked = false;
				this.mnuAdd.Enabled = false;
				this.mnuModify.Enabled = false;
				this.mnuRemove.Enabled = false;
			}
			else
			{
				if((m_strCurrentListBox == "Categories" && this.lbxCategories.SelectedIndex == -1) || (m_strCurrentListBox == "Products" && this.lbxProducts.SelectedIndex == -1) || (m_strCurrentListBox == "Sub-Products" && this.lbxSubProducts.SelectedIndex == -1))
				{
					this.mnuActive.Enabled = false;
					this.mnuActive.Checked = false;
					this.mnuModify.Enabled = false;
					this.mnuRemove.Enabled = false;
				}
				else
				{
					this.mnuActive.Enabled = true;
					if(this.IsCurrentItemActive())
						this.mnuActive.Checked = true;
					else
						this.mnuActive.Checked = false;
					this.mnuModify.Enabled = true;
					this.mnuRemove.Enabled = true;
				}
				this.mnuAdd.Enabled = true;
			}
		}

		private void mnuActive_Click(object sender, System.EventArgs e)
		{
			int intStatus = -1;
			if(this.mnuActive.Checked)
			{
				this.mnuActive.Checked = false;
				intStatus = 0;
			}
			else
			{
				this.mnuActive.Checked = true;
				intStatus = 1;
			}

			switch(m_strCurrentListBox)
			{
				case "Categories":
					m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["Status"] = intStatus;
					m_odaCategories.Update(m_dtaCategories);
					m_dtaCategories.AcceptChanges();
					this.LoadData("Categories",-1);
					this.lbxCategories.SelectedIndex = m_intCategoriesSelectedIndex;
				break;

				case "Products":
					m_dtaProducts.Rows[m_intProductsSelectedIndex]["Status"] = intStatus;
					m_odaProducts.Update(m_dtaProducts);
					m_dtaProducts.AcceptChanges();
					if(m_blnSearchByCategory)
						this.LoadData("Products",int.Parse(m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["CategoryId"].ToString()));
					else
						this.LoadData("Products",-1);
					this.lbxProducts.SelectedIndex = m_intProductsSelectedIndex;
				break;
				
				case "Sub-Products":
					m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["Status"] = intStatus;
					m_odaSubProducts.Update(m_dtaSubProducts);
					m_dtaSubProducts.AcceptChanges();
					this.LoadData("Sub-Products",int.Parse(m_dtaProducts.Rows[m_intProductsSelectedIndex]["MatId"].ToString()));
					this.lbxSubProducts.SelectedIndex = m_intSubProductsSelectedIndex;
				break;
			}
		}

		private void mnuAdd_Click(object sender, System.EventArgs e)
		{
			this.Add();
		}
		
		private void mnuModify_Click(object sender, System.EventArgs e)
		{
			this.Modify();
		}

		private void mnuRemove_Click(object sender, System.EventArgs e)
		{
			this.Remove();
		}

		private void btnAddCategory_Click(object sender, System.EventArgs e)
		{
			m_strCurrentListBox = "Categories";
			this.Add();
		}

		private void btnModifyCategory_Click(object sender, System.EventArgs e)
		{
			m_strCurrentListBox = "Categories";
			this.Modify();
		}

		private void btnDeleteCategory_Click(object sender, System.EventArgs e)
		{
			m_strCurrentListBox = "Categories";
			this.Remove();
		}

		private void btnAddProd_Click(object sender, System.EventArgs e)
		{
			m_strCurrentListBox = "Products";
			this.Add();
		}

		private void btnModProd_Click(object sender, System.EventArgs e)
		{
			m_strCurrentListBox = "Products";
			this.Modify();
		}

		private void btnDelProd_Click(object sender, System.EventArgs e)
		{
			m_strCurrentListBox = "Products";
			this.Remove();
		}

		private void btnAddSubProd_Click(object sender, System.EventArgs e)
		{
			m_strCurrentListBox = "Sub-Products";
			this.Add();
		}

		private void btnModSubProd_Click(object sender, System.EventArgs e)
		{
			m_strCurrentListBox = "Sub-Products";
			this.Modify();
		}

		private void btnDelSubProd_Click(object sender, System.EventArgs e)
		{
			m_strCurrentListBox = "Sub-Products";
			this.Remove();
		}

		private void optSearchByProduct_CheckedChanged(object sender, System.EventArgs e)
		{
			m_blnSearchByCategory = false;
			
			this.lbxCategories.Enabled = false;
			this.lbxCategories.ClearSelected();
			this.btnAddCategory.Enabled = false;
			this.btnModifyCategory.Enabled = false;
			this.btnDeleteCategory.Enabled = false;
			this.LoadData("Products",-1);
		}

		private void optSearchByCategory_CheckedChanged(object sender, System.EventArgs e)
		{
			m_blnSearchByCategory = true;

			this.lbxCategories.Enabled = true;
			this.lbxCategories.ClearSelected();
			this.lbxProducts.Items.Clear();
			this.lbxSubProducts.Items.Clear();

			this.btnAddCategory.Enabled = true;
			this.btnModifyCategory.Enabled = false;
			this.btnDeleteCategory.Enabled = false;
			this.btnAddProd.Enabled = false;
			this.btnAddSubProd.Enabled = false;
		}

		private void fclsDMModifyProduct_Resize(object sender, System.EventArgs e)
		{
			int intListboxSpacing;
			
			// listbox width
			this.lbxCategories.Width = (int) (m_dblCategoryLbxWProportion*this.Width);
			this.lbxProducts.Width = (int) (m_dblProductLbxWProportion*this.Width);
			this.lbxSubProducts.Width = (int) (m_dblSubProductLbxWProportion*this.Width);

            // listbox height
            this.lbxCategories.Height = this.pnlCategoryButtons.Location.Y - this.lbxCategories.Location.Y;
            this.lbxProducts.Height = this.pnlProductsButtons.Location.Y - this.lbxProducts.Location.Y;
            this.lbxSubProducts.Height = this.pnlSubProductsButtons.Location.Y - this.lbxSubProducts.Location.Y;

			// button panel width
			this.pnlCategoryButtons.Width = this.lbxCategories.Width;
			this.pnlProductsButtons.Width = this.lbxProducts.Width;
			this.pnlSubProductsButtons.Width = this.lbxSubProducts.Width;

			// label width
			this.lblCategory.Width = this.lbxCategories.Width;
			this.lblProducts.Width = this.lbxProducts.Width;
			this.lblSubProducts.Width = this.lbxSubProducts.Width;

			// listbox location
			intListboxSpacing = (this.Width - (this.lbxCategories.Width + this.lbxProducts.Width + this.lbxSubProducts.Width))/4;
			this.lbxCategories.Location = new Point(intListboxSpacing, this.lbxCategories.Location.Y);
			this.lbxProducts.Location = new Point(this.lbxCategories.Location.X + this.lbxCategories.Width + intListboxSpacing, this.lbxProducts.Location.Y);
			this.lbxSubProducts.Location = new Point(this.lbxProducts.Location.X + this.lbxProducts.Width + intListboxSpacing, this.lbxSubProducts.Location.Y);

			// button panel location
			this.pnlCategoryButtons.Location = new Point(this.lbxCategories.Location.X, this.pnlCategoryButtons.Location.Y);
			this.pnlProductsButtons.Location = new Point(this.lbxProducts.Location.X, this.pnlProductsButtons.Location.Y);
			this.pnlSubProductsButtons.Location = new Point(this.lbxSubProducts.Location.X, this.pnlSubProductsButtons.Location.Y);

			// label location
			this.lblCategory.Location = new Point(this.lbxCategories.Location.X, this.lblCategory.Location.Y);
			this.lblProducts.Location = new Point(this.lbxProducts.Location.X, this.lblProducts.Location.Y);
			this.lblSubProducts.Location = new Point(this.lbxSubProducts.Location.X, this.lblSubProducts.Location.Y);
		}

		#region Methods
		// Adds either a new Category, Product or Sub-Product
		private void Add()
		{
			string strResponse = "";

			switch(m_strCurrentListBox)
			{
				case "Categories":
					strResponse = InputBox.ShowInputBox("Please enter the name of the new category:","Add New Category");
                    if (strResponse != null && strResponse.Length > 0)
					{
						int n_nrCategory = m_dtaCategories.Rows.Count;
						if(!checkName(strResponse, n_nrCategory, m_dtaCategories, "Category"))
							return;
						m_intCategoryId++;
						DataRow	dtrNewRow = m_dtaCategories.NewRow();
						dtrNewRow["CategoryId"] = m_intCategoryId;
						dtrNewRow["CategName"] = strResponse;
						dtrNewRow["Status"] = 1;

						// Add the new row to the table
						m_dtaCategories.Rows.Add(dtrNewRow);

						// Update the Database
						try
						{
							m_odaCategories.Update(m_dtaCategories);
							m_dtaCategories.AcceptChanges();

							this.LoadData("Categories",-1);
							this.lbxCategories.SelectedIndex = clsUtilities.FindItemIndex(strResponse,this.lbxCategories);
						} 
						catch (OleDbException ex)
						{
							m_dtaCategories.RejectChanges();
							MessageBox.Show(ex.Message);
						}
					}
					//					else
					//					{
					//						MessageBox.Show("Please fill out the Category name!","Data Missing",MessageBoxButtons.OK,MessageBoxIcon.Error);
					//						this.Add();
					//					}
					break;

				case "Products":
					string[] strNewProduct = new string[2];
					if(m_blnSearchByCategory)
						strNewProduct[0] = frmDMModifyProduct_Products.ShowProductWindow(int.Parse(m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["CategoryId"].ToString()));
					else
						strNewProduct = frmDMModifyProduct_Products.ShowProductWindow();
					if(strNewProduct[0].Length > 0)
					{
						int n_nrProduct = m_dtaProducts.Rows.Count;
						if(!checkName(strNewProduct[0], n_nrProduct, m_dtaProducts, "Product"))
							return;
						m_intProductId++;
						DataRow	dtrNewRow = m_dtaProducts.NewRow();
						dtrNewRow["MatId"] = m_intProductId;
						dtrNewRow["MatName"] = strNewProduct[0];
						if(m_blnSearchByCategory)
							dtrNewRow["CategoryId"] = int.Parse(m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["CategoryId"].ToString());
						else
							dtrNewRow["CategoryId"] = int.Parse(strNewProduct[1]);
						dtrNewRow["Status"] = 1;

						// Add the new row to the table
						m_dtaProducts.Rows.Add(dtrNewRow);

						// Update the Database
						try
						{
							m_odaProducts.Update(m_dtaProducts);
							m_dtaProducts.AcceptChanges();

							if(m_blnSearchByCategory)
								this.LoadData("Products",int.Parse(m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["CategoryId"].ToString()));
							else
								this.LoadData("Products",-1);
							this.lbxProducts.SelectedIndex = clsUtilities.FindItemIndex(strNewProduct[0],this.lbxProducts);
						} 
						catch (OleDbException ex)
						{
							m_dtaProducts.RejectChanges();
							MessageBox.Show(ex.Message);
						}
					}
					break;

				case "Sub-Products":
					fclsDMModifyProduct_SubProd frmSubProduct = new fclsDMModifyProduct_SubProd(m_odcConnection,
																								m_dtaSubProducts,
																								DSMS.fclsDMModifyProduct_SubProd.WindowPurpose.Add);
					if(frmSubProduct.ShowDialog() == DialogResult.OK)
					{
						DataRow	dtrNewRow = m_dtaSubProducts.NewRow();
						int n_nrSubProduct = m_dtaSubProducts.Rows.Count;
						object[] objSubProductData = frmSubProduct.GetSubProductData();
						
						dtrNewRow["SubPrId"]	= ++m_intSubProductId;
						dtrNewRow["MatId"]		= int.Parse(m_dtaProducts.Rows[m_intProductsSelectedIndex]["MatId"].ToString());
						dtrNewRow["MatName"]	= (string) objSubProductData[0];
						dtrNewRow["MarComId"]	= (int) objSubProductData[1];
						dtrNewRow["SuplId"]		= 0;
						dtrNewRow["Prix"]		= 0;
						dtrNewRow["Pack"]		= (string) objSubProductData[2];
						dtrNewRow["Reorder"]	= (int) objSubProductData[3];
						dtrNewRow["Invent"]		= 0;
						dtrNewRow["Qtty"]		= 0;
						dtrNewRow["PrixMin"]	= 0;
						dtrNewRow["PrixMax"]	= 0;
						dtrNewRow["PrixMinOi"]	= "0";
						dtrNewRow["PrixOrderId"]= "0";
						dtrNewRow["PrixMaxOi"]	= "0";
						dtrNewRow["CatalogPay"]	= 0;
						dtrNewRow["Tax"]		= 0;
						dtrNewRow["Transport"]	= 0;
						dtrNewRow["Duty"]		= 0;
						dtrNewRow["TotalPay"]	= 0;
						dtrNewRow["Status"]		= 1;

						// Add the new row to the table
						m_dtaSubProducts.Rows.Add(dtrNewRow);

						// Update the Database
						try
						{
							m_odaSubProducts.Update(m_dtaSubProducts);
							m_dtaSubProducts.AcceptChanges();
							
							// refresh listbox data and select the new subproduct
							this.LoadData("Sub-Products",int.Parse(m_dtaProducts.Rows[m_intProductsSelectedIndex]["MatId"].ToString()));
							this.lbxSubProducts.SelectedIndex = clsUtilities.FindItemIndex((string) objSubProductData[0], this.lbxSubProducts);
						} 
						catch (OleDbException ex)
						{
							m_dtaSubProducts.RejectChanges();
							MessageBox.Show(ex.Message);
						}					
					}

					// refresh trademarks datatable (could've been modified while in fclsDMModifyProduct_SubProd
					this.LoadTrademarks();
				break;
			}
		}
		[Obsolete("Check this function...", false)]
		private bool checkName(string strResponse, int nrCheck, DataTable m_dtaCheck, string strCase)
		{
			string strName;
			string strColumn = "MatName";
			if (strCase == "Category")
				strColumn = "CategName";
			string msgText = strCase;
			msgText += " Name Error";
			for(int i=0; i<nrCheck; i++)
			{
				strName = m_dtaCheck.Rows[i][strColumn].ToString();
				if(strName == strResponse)
				{
					MessageBox.Show("This name is already in the database!\n" +
						"You must change the name!",msgText);
					return false;
				}
			}
			return true;
		}

		// Gets the biggest key in the Categories, Products and Sub-Products table
		private void GetCurrentIds()
		{
			int intCurrentId = -1;

			m_dtaCategories = new DataTable();
			m_dtaProducts = new DataTable();
			m_dtaSubProducts = new DataTable();

			m_odaCategories = new OleDbDataAdapter("SELECT * FROM Categories",m_odcConnection);
			OleDbCommandBuilder ocbCategories = new OleDbCommandBuilder(m_odaCategories);
			m_odaCategories.Fill(m_dtaCategories);
			m_odaProducts = new OleDbDataAdapter("SELECT * FROM Products",m_odcConnection);
			OleDbCommandBuilder ocbProducts = new OleDbCommandBuilder();
			m_odaProducts.Fill(m_dtaProducts);
			m_odaSubProducts = new OleDbDataAdapter("SELECT * FROM SubProducts",m_odcConnection);
			OleDbCommandBuilder ocbSubProducts = new OleDbCommandBuilder();
			m_odaSubProducts.Fill(m_dtaSubProducts);


			for(int i=0; i < m_dtaCategories.Rows.Count; i++)
			{
				intCurrentId = int.Parse(m_dtaCategories.Rows[i]["CategoryId"].ToString());
				if(m_intCategoryId < intCurrentId)
					m_intCategoryId = intCurrentId;
			}
			for(int i=0 ; i < m_dtaProducts.Rows.Count; i++)
			{
				intCurrentId = int.Parse(m_dtaProducts.Rows[i]["MatId"].ToString());
				if(m_intProductId < intCurrentId)
					m_intProductId = intCurrentId;
			}
			for(int i=0; i < m_dtaSubProducts.Rows.Count; i++)
			{
				intCurrentId = int.Parse(m_dtaSubProducts.Rows[i]["SubPrId"].ToString());
				if(m_intSubProductId < intCurrentId)
					m_intSubProductId = intCurrentId;
			}

		}

		private string GetTrademark(int intTrademarkId)
		{
			for(int i = 0; i < m_dtaTrademarks.Rows.Count; i++)
			{
				if(int.Parse(m_dtaTrademarks.Rows[i]["MarComId"].ToString()) == intTrademarkId)
					return m_dtaTrademarks.Rows[i]["Trademark"].ToString();
			}
			return "";
		}

		// Returns true if the selected item in the current listbox is active
		private bool IsCurrentItemActive()
		{
			switch(m_strCurrentListBox)
			{
				case "Categories":
					if(int.Parse(m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["Status"].ToString()) == 1)
						return true;
					break;

				case "Products":
					if(int.Parse(m_dtaProducts.Rows[m_intProductsSelectedIndex]["Status"].ToString()) == 1)
						return true;
					break;

				case "Sub-Products":
					if(int.Parse(m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["Status"].ToString()) == 1)
						return true;
					break;
			}
			return false;
		}

		// Loads data into the ListBoxes depending on the parameters received
		private void LoadData(string strLevel, int intId)
		{
			switch(strLevel)
			{
				case "Categories":
					m_dtaCategories = new DataTable();
					this.lbxCategories.Items.Clear();
					m_odaCategories = new OleDbDataAdapter("SELECT * FROM Categories ORDER BY CategName",m_odcConnection);
					OleDbCommandBuilder ocbCategories = new OleDbCommandBuilder(m_odaCategories);
					m_odaCategories.Fill(m_dtaCategories);
					for(int i=0; i < m_dtaCategories.Rows.Count; i++)
					{
						this.lbxCategories.Items.Add(m_dtaCategories.Rows[i]["CategName"].ToString());
					}
					this.btnAddCategory.Enabled = true;
					this.btnModifyCategory.Enabled = false;
					this.btnDeleteCategory.Enabled = false;
					m_blnCategoriesLoaded = true;
					break;

				case "Products":
					string strSelectString = "";
					m_dtaProducts = new DataTable();
					this.lbxProducts.Items.Clear();
					if(m_blnSearchByCategory)
						strSelectString = "SELECT * FROM Products WHERE (((CategoryId)=" + intId + ")) ORDER BY MatName";
					else
						strSelectString = "SELECT * FROM Products ORDER BY MatName";
					m_odaProducts = new OleDbDataAdapter(strSelectString,m_odcConnection);
					OleDbCommandBuilder ocbProducts = new OleDbCommandBuilder(m_odaProducts);
					m_odaProducts.Fill(m_dtaProducts);
					for(int i=0 ; i < m_dtaProducts.Rows.Count; i++)
					{
						this.lbxProducts.Items.Add(m_dtaProducts.Rows[i]["MatName"].ToString());
					}
					this.btnAddProd.Enabled = true;
					this.btnModProd.Enabled = false;
					this.btnDelProd.Enabled = false;
					m_blnProductsLoaded = true;
					break;

				case "Sub-Products":
					m_dtaSubProducts = new DataTable();
					this.lbxSubProducts.Items.Clear();
					m_odaSubProducts = new OleDbDataAdapter("SELECT * FROM SubProducts WHERE SubProducts.MatId=" + intId + " ORDER BY SubProducts.MatName",m_odcConnection);
					OleDbCommandBuilder ocbSubProducts = new OleDbCommandBuilder(m_odaSubProducts);
					m_odaSubProducts.Fill(m_dtaSubProducts);
					for(int i=0; i < m_dtaSubProducts.Rows.Count; i++)
					{
						this.lbxSubProducts.Items.Add(m_dtaSubProducts.Rows[i]["MatName"].ToString() + " [" + this.GetTrademark(int.Parse(m_dtaSubProducts.Rows[i]["MarComId"].ToString())) + "]");
					}
					this.btnAddSubProd.Enabled = true;
					this.btnModSubProd.Enabled = false;
					this.btnDelSubProd.Enabled = false;
					m_blnSubProductsLoaded = true;
					break;
			}
		}

		// Modifies either the selected Category, Product or Sub-Product
		private void Modify()
		{
			string strResponse = "";
			string oldName = m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["CategName"].ToString();

			switch(m_strCurrentListBox)
			{
				case "Categories":
					strResponse = InputBox.ShowInputBox("Please change the name of the category:","Modify Category",this.lbxCategories.SelectedItem.ToString());
                    if (strResponse != null && strResponse.Length > 0)
					{
						int n_nrCategory = m_dtaCategories.Rows.Count;
						if(strResponse == oldName)
							return;
						if(!checkName(strResponse, n_nrCategory, m_dtaCategories, "Category"))
							return;
						m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["CategName"] = strResponse;
						m_odaCategories.Update(m_dtaCategories);
						m_dtaCategories.AcceptChanges();
						this.LoadData("Categories",-1);
						this.lbxCategories.SelectedIndex = m_intCategoriesSelectedIndex;
					}
					break;

				case "Products":
					string[] strModifyProduct = new string[2];
					oldName = m_dtaProducts.Rows[m_intProductsSelectedIndex]["MatName"].ToString();

					if(m_blnSearchByCategory)
					{
						strModifyProduct[0] = frmDMModifyProduct_Products.ShowProductWindow(this.lbxProducts.SelectedItem.ToString(),int.Parse(m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["CategoryId"].ToString()));
						strModifyProduct[1] = m_dtaCategories.Rows[this.lbxCategories.SelectedIndex]["CategoryId"].ToString();
					}
					else
					{
						string[] strDefaultText = new string[2];
						strDefaultText[0] = this.lbxProducts.SelectedItem.ToString();
						strDefaultText[1] = m_dtaCategories.Rows[this.lbxCategories.SelectedIndex]["CategoryId"].ToString();
						strModifyProduct = frmDMModifyProduct_Products.ShowProductWindow(strDefaultText);
					}
					if(strModifyProduct[0].Length > 0)
					{
						int n_nrProduct = m_dtaProducts.Rows.Count;
						if(strModifyProduct[0] != oldName)
							if(!checkName(strModifyProduct[0], n_nrProduct, m_dtaProducts, "Product"))
								return;
						m_dtaProducts.Rows[m_intProductsSelectedIndex]["MatName"] = strModifyProduct[0];
						if(!m_blnSearchByCategory)
							m_dtaProducts.Rows[m_intProductsSelectedIndex]["CategoryId"] = strModifyProduct[1];
						m_odaProducts.Update(m_dtaProducts);
						m_dtaProducts.AcceptChanges();
						if(m_blnSearchByCategory)
							this.LoadData("Products",int.Parse(m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["CategoryId"].ToString()));
						else
							this.LoadData("Products",-1);
						this.lbxProducts.SelectedIndex = m_intProductsSelectedIndex;
					}
					break;
				
				case "Sub-Products":
					object[] objSubProductData = new object[4];
					objSubProductData[0] = m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["MatName"].ToString();
					objSubProductData[1] = m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["MarComId"];
					objSubProductData[2] = m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["Pack"].ToString();
					objSubProductData[3] = m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["Reorder"];
					
					fclsDMModifyProduct_SubProd frmSubProduct = new fclsDMModifyProduct_SubProd(m_odcConnection,
																								m_dtaSubProducts,
																								DSMS.fclsDMModifyProduct_SubProd.WindowPurpose.Modify);
					frmSubProduct.SetSubProductData(objSubProductData);
					if(frmSubProduct.DialogResult == DialogResult.OK)
					{
						objSubProductData = frmSubProduct.GetSubProductData();

						m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["MatName"]		= (string) objSubProductData[0];
						m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["MarComId"]	= (int) objSubProductData[1];
						m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["Pack"]		= (string) objSubProductData[2];
						m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["Reorder"]		= (int) objSubProductData[3];
						m_odaSubProducts.Update(m_dtaSubProducts);
						m_dtaSubProducts.AcceptChanges();
						
						// refreash listbox data and select the newly modifed sub-product
						this.LoadData("Sub-Products",int.Parse(m_dtaProducts.Rows[m_intProductsSelectedIndex]["MatId"].ToString()));
						this.lbxSubProducts.SelectedIndex = m_intSubProductsSelectedIndex;
					}

					// refresh trademarks datatable (could've been modified while in fclsDMModifyProduct_SubProd
					this.LoadTrademarks();
				break;
			}
		}

		// Removes either the selected Category, Product or Sub-Product
		private void Remove()			
		{
			string strName;
			switch(m_strCurrentListBox)
			{
				case "Categories":
					strName = m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["CategName"].ToString();
					if(MessageBox.Show("Are you sure you want to remove the '" + strName + "' Category?","Remove Category",
						MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						m_dtaCategories.Rows[m_intCategoriesSelectedIndex].Delete();

						// update the database
						try
						{
							m_odaCategories.Update(m_dtaCategories);

							// accept the changes and repopulate the list box
							m_dtaCategories.AcceptChanges();
							this.LoadData("Categories",-1);
							this.btnAddProd.Enabled = false;
							this.btnAddSubProd.Enabled = false;
						}
						catch (OleDbException ex)
						{
							m_dtaCategories.RejectChanges();
							MessageBox.Show(ex.Message);
						}
					}
					break;

				case "Products":
					strName = m_dtaProducts.Rows[m_intProductsSelectedIndex]["MatName"].ToString();
					if(MessageBox.Show("Are you sure you want to remove the '" + strName + "' Product\nwith all associated Sub-Products?","Remove Product",
						MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						if(this.lbxSubProducts.Items.Count > 0)
						{
							for(int i = m_dtaSubProducts.Rows.Count - 1; i > -1; i--)
							{
								m_strCurrentListBox = "Sub-Products";
								m_intSubProductsSelectedIndex = i;
								this.Remove();
							}
						}

						m_dtaProducts.Rows[m_intProductsSelectedIndex].Delete();

						// update the database
						try
						{
							m_odaProducts.Update(m_dtaProducts);

							// accept the changes and repopulate the list box
							m_dtaProducts.AcceptChanges();
							if(m_blnSearchByCategory)
								this.LoadData("Products",int.Parse(m_dtaCategories.Rows[m_intCategoriesSelectedIndex]["CategoryId"].ToString()));
							else
								this.LoadData("Products",-1);
							this.btnAddSubProd.Enabled = false;
						}
						catch (OleDbException ex)
						{
							m_dtaProducts.RejectChanges();
							MessageBox.Show(ex.Message);
						}
					}			
					break;
				
				case "Sub-Products":
					strName = m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex]["MatName"].ToString();
					if(MessageBox.Show("Are you sure you want to remove the '" + strName + "' Sub-Product?","Remove Sub-Product",
						MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
					{
						m_dtaSubProducts.Rows[m_intSubProductsSelectedIndex].Delete();

						// update the database
						try
						{
							this.m_odaSubProducts.Update(m_dtaSubProducts);

							// accept the changes and repopulate the list box
							m_dtaSubProducts.AcceptChanges();
							this.LoadData("Sub-Products",int.Parse(m_dtaProducts.Rows[m_intProductsSelectedIndex]["MatId"].ToString()));
						}
						catch (OleDbException ex)
						{
							m_dtaSubProducts.RejectChanges();
							MessageBox.Show(ex.Message);
						}
					}
					break;
			}
		}
		#endregion

	}
}
