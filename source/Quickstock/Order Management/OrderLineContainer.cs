using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;	
using System.Windows.Forms;

namespace DSMS
{
	/// <summary>
	/// Summary description for OrderLineContainer.
	/// </summary>
	public class OrderLineContainer : System.Windows.Forms.UserControl
	{
		private System.Windows.Forms.Panel pnlOrderLines;
		private System.Windows.Forms.Label lblUnits;
		private System.Windows.Forms.Label lblPackaging;
		private System.Windows.Forms.Label lblProduct;
		private System.Windows.Forms.Label lblTrademark;
		/// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;
		
		public delegate void			RemoveButtonClickHandler(object btnRemove, clsRemoveOrderLineClickEventArgs ceaEventArgs);
		public event					fclsOMEmergencyOrder.EmptyOrderLineContainerHandler OnEmptyOrderLineContainer;
        
        private ArrayList               m_alOrderLines;
		private double					m_dblProductLabelProportion, m_dblTrademarkLabelProportion, m_dblPackagingLabelProportion, m_dblUnitsLabelProportion;
		private int						m_intInterOrderLineSpacing, m_intLabelSpacing, m_intMaxOrderLines;
        
		public OrderLineContainer()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			// Global variable initalization
			m_alOrderLines = new ArrayList();
			m_dblProductLabelProportion = ((double) this.lblProduct.Width)/((double)this.Width);
			m_dblTrademarkLabelProportion = ((double) this.lblTrademark.Width)/((double)this.Width);
			m_dblPackagingLabelProportion = ((double) this.lblPackaging.Width)/((double)this.Width);
			m_dblUnitsLabelProportion = ((double) this.lblUnits.Width)/((double)this.Width);
			m_intInterOrderLineSpacing = 1;
			m_intLabelSpacing = this.lblTrademark.Location.X - (this.lblProduct.Location.X + this.lblProduct.Size.Width);
			
			// Object initialization
			this.MaxOrderLines = 15;
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

		#region Component Designer generated code
		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.pnlOrderLines = new System.Windows.Forms.Panel();
			this.lblUnits = new System.Windows.Forms.Label();
			this.lblPackaging = new System.Windows.Forms.Label();
			this.lblProduct = new System.Windows.Forms.Label();
			this.lblTrademark = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// pnlOrderLines
			// 
			this.pnlOrderLines.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.pnlOrderLines.AutoScroll = true;
			this.pnlOrderLines.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.pnlOrderLines.ForeColor = System.Drawing.SystemColors.Control;
			this.pnlOrderLines.Location = new System.Drawing.Point(0, 16);
			this.pnlOrderLines.Name = "pnlOrderLines";
			this.pnlOrderLines.Size = new System.Drawing.Size(902, 648);
			this.pnlOrderLines.TabIndex = 0;
			// 
			// lblUnits
			// 
			this.lblUnits.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblUnits.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblUnits.Location = new System.Drawing.Point(850, 0);
			this.lblUnits.Name = "lblUnits";
			this.lblUnits.Size = new System.Drawing.Size(48, 16);
			this.lblUnits.TabIndex = 36;
			this.lblUnits.Text = "Units";
			this.lblUnits.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblUnits.Visible = false;
			// 
			// lblPackaging
			// 
			this.lblPackaging.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblPackaging.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblPackaging.Location = new System.Drawing.Point(678, 0);
			this.lblPackaging.Name = "lblPackaging";
			this.lblPackaging.Size = new System.Drawing.Size(168, 16);
			this.lblPackaging.TabIndex = 35;
			this.lblPackaging.Text = "Packaging";
			this.lblPackaging.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblPackaging.Visible = false;
			// 
			// lblProduct
			// 
			this.lblProduct.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblProduct.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblProduct.Location = new System.Drawing.Point(26, 0);
			this.lblProduct.Name = "lblProduct";
			this.lblProduct.Size = new System.Drawing.Size(502, 16);
			this.lblProduct.TabIndex = 34;
			this.lblProduct.Text = "Product";
			this.lblProduct.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblProduct.Visible = false;
			// 
			// lblTrademark
			// 
			this.lblTrademark.Font = new System.Drawing.Font("Tahoma", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((System.Byte)(0)));
			this.lblTrademark.ForeColor = System.Drawing.Color.FromArgb(((System.Byte)(0)), ((System.Byte)(0)), ((System.Byte)(192)));
			this.lblTrademark.Location = new System.Drawing.Point(540, 0);
			this.lblTrademark.Name = "lblTrademark";
			this.lblTrademark.Size = new System.Drawing.Size(146, 16);
			this.lblTrademark.TabIndex = 33;
			this.lblTrademark.Text = "Trademark";
			this.lblTrademark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
			this.lblTrademark.Visible = false;
			// 
			// OrderLineContainer
			// 
			this.Controls.Add(this.lblUnits);
			this.Controls.Add(this.lblPackaging);
			this.Controls.Add(this.lblProduct);
			this.Controls.Add(this.lblTrademark);
			this.Controls.Add(this.pnlOrderLines);
			this.Name = "OrderLineContainer";
			this.Size = new System.Drawing.Size(904, 672);
			this.ResumeLayout(false);

		}
		#endregion
		
		#region Properties

		//--------------------------------------------------------------------------------------------------------------------
		// Properties
		//--------------------------------------------------------------------------------------------------------------------
		public int MaxOrderLines
		{
			set
			{
				m_intMaxOrderLines = value;
			}
			get
			{
				return m_intMaxOrderLines;
			}
		}

		public int NOrderLines
		{
			get
			{
				return m_alOrderLines.Count;
			}
		}
	
		public ArrayList OrderLines
		{
			get
			{
				return m_alOrderLines;
			}
		}
		#endregion
		
		#region Methods

		//--------------------------------------------------------------------------------------------------------------------
		// Methods
		//--------------------------------------------------------------------------------------------------------------------
		/// <summary>
		///		Adds a new order line to the container.
		/// </summary>
		public void Add(OrderLine olNewOrderLine)
		{
			// Variable declaration
			OrderLine olPreviousOrderLine;
			
			olNewOrderLine.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
			olNewOrderLine.OnRemoveButtonClick += new RemoveButtonClickHandler(this.OrderLineRemove_Click);
			
			if(m_alOrderLines.Count == 0)
			{
				olNewOrderLine.Width = this.pnlOrderLines.Width;
				olNewOrderLine.Location = new System.Drawing.Point(2, m_intInterOrderLineSpacing);
			}
			else
			{
				olPreviousOrderLine = (OrderLine) m_alOrderLines[this.NOrderLines - 1];
				olNewOrderLine.Width = olPreviousOrderLine.Width;
				olNewOrderLine.Location = new System.Drawing.Point(2,olPreviousOrderLine.Location.Y + olPreviousOrderLine.Height + m_intInterOrderLineSpacing);
			}

			m_alOrderLines.Add(olNewOrderLine);
			
			this.pnlOrderLines.Controls.Add(olNewOrderLine);

			olNewOrderLine.Focus();
		}

		/// <summary>
		///		Checks if there is room for one more product.
		/// </summary>
		/// <returns>
		///		Returns TRUE if one more product can be added to the order, FALSE otherwise.
		/// </returns>
		public bool CanAddOneProduct()
		{
			bool blnCanAddOneProduct = true;

			if(this.OrderLines != null)
			{
				if((this.m_alOrderLines.Count + 1) > this.MaxOrderLines)
					blnCanAddOneProduct = false;
			}

			return blnCanAddOneProduct;
		}

		/// <summary>
		///		Clears the container of all data.
		/// </summary>
		public void ClearAll()
		{
			this.pnlOrderLines.Controls.Clear();
			m_alOrderLines.Clear();
			this.SetLabelVisibility(false);
		}

		/// <summary>
		///		Checks if the given SubProduct is already a part of the order.
		/// </summary>
		/// <returns>
		///		Returns TRUE if the subproduct is found in the current order, FALSE otherwise.
		/// </returns>
		public bool IsSubProductAlreadyInOrder(int intSubProductId)
		{
			bool blnProductAlreadyInOrder = false;
			
			if(this.OrderLines != null)
			{
				foreach(OrderLine olOrderLine in this.OrderLines)
				{
					if(olOrderLine.SubProductId == intSubProductId)
					{
						blnProductAlreadyInOrder = true;
						break;
					}
				}
			}

			return blnProductAlreadyInOrder;
		}

		public void ResizeLabels(int intProductWidth, int intProductXPos,int intTrademarkWidth, int intTrademarkXPos, int intPackagingWidth, int intPackagingXPos, int intUnitsXPos)
		{
			this.lblProduct.Width = intProductWidth;
			this.lblProduct.Location = new Point(intProductXPos,this.lblProduct.Location.Y);
			
			this.lblPackaging.Width = intPackagingWidth;
			this.lblPackaging.Location = new Point(intPackagingXPos,this.lblPackaging.Location.Y);

			this.lblTrademark.Width = intTrademarkWidth;
			this.lblTrademark.Location = new Point(intTrademarkXPos,this.lblTrademark.Location.Y);

			this.lblUnits.Location = new Point(intUnitsXPos,this.lblUnits.Location.Y);

			this.SetLabelVisibility(true);
		}

		private void SetLabelVisibility(bool blnVisible)
		{
			this.lblProduct.Visible = blnVisible;
			this.lblTrademark.Visible = blnVisible;
			this.lblPackaging.Visible = blnVisible;
			this.lblUnits.Visible = blnVisible;
		}
		#endregion
		
		# region Events
		//--------------------------------------------------------------------------------------------------------------------
		// Events
		//--------------------------------------------------------------------------------------------------------------------
		private void OrderLineRemove_Click(object sender, clsRemoveOrderLineClickEventArgs e)
		{
			DialogResult dlgResult;
			int intLineIndex = e.GetLineIndex();
			OrderLine olCurrentOrderLine;
			string strProductName = e.GetProductName();
			
			dlgResult = MessageBox.Show("Are you sure you want to remove the product\r\n" + strProductName + "\r\nfrom this order?",
				this.Text,MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2);
			
			if(dlgResult == DialogResult.Yes)
			{
				// remove order line from the panel
				Object obj = this.OrderLines[intLineIndex];
				this.pnlOrderLines.Controls.Remove((OrderLine) obj);
				
				if(intLineIndex != (this.NOrderLines - 1))
				{
					// Shift all the order data up one line
					for(int i=intLineIndex+1; i < this.NOrderLines; i++)
					{
						olCurrentOrderLine = ((OrderLine) this.OrderLines[i]);
						olCurrentOrderLine.Location = new Point(2,olCurrentOrderLine.Location.Y - olCurrentOrderLine.Height - m_intInterOrderLineSpacing);
						olCurrentOrderLine.LineNumber = olCurrentOrderLine.LineNumber - 1;
					}				
				}

				this.OrderLines.Remove(obj);

				if(this.NOrderLines > 0)
				{
					if(intLineIndex > 0)
					{
						if(intLineIndex < this.NOrderLines)
							((OrderLine) this.OrderLines[intLineIndex]).Focus();
						else
							((OrderLine) this.OrderLines[intLineIndex-1]).Focus();
					}
				}
				else
				{
					if(OnEmptyOrderLineContainer != null)
						OnEmptyOrderLineContainer();
				}
			}
		}
		#endregion
	}
}
