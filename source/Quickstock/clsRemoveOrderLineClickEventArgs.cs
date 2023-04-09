using System;

namespace DSMS
{
	/// <summary>
	/// Summary description for clsRemoveOrderLineClickEventArgs.
	/// </summary>
	public class clsRemoveOrderLineClickEventArgs: EventArgs 
	{
		private int m_intLineIndex;
		private string m_strProductName;

		public clsRemoveOrderLineClickEventArgs(int intLineIndex, string strProductName) 
		{
			this.m_intLineIndex = intLineIndex;
			this.m_strProductName = strProductName;
		}

		public int GetLineIndex()
		{
			return m_intLineIndex;
		}

		public string GetProductName()
		{
			return m_strProductName;
		}
	}
}
