using System;

namespace PEIS.Common
{
	public class ServerControl
	{
		private string _WasteBaseName = "FYH_Waste";

		private string _OnlineBaseName = "FYH";

		private string _OffLineBaseName = "FYH_OffLine";

		private string _HisBaseName = "FYH_HisFile";

		public string GetDataBaseName(string YearStr)
		{
			string onlineBaseName = this._OnlineBaseName;
			return this.BaseConvert(YearStr);
		}

		private string BaseConvert(string YearStr)
		{
			int num = (int)Convert.ToInt16(YearStr);
			int num2 = num / 3 - 670;
			string str = ((num2 <= 0) ? 1 : num2).ToString().PadLeft(3, '0');
			return this._HisBaseName + str;
		}
	}
}
