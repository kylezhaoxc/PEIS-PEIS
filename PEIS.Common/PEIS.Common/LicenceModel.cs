using System;

namespace PEIS.Common
{
	public class LicenceModel
	{
		private int _IsCanUse = 0;

		private string _CustomerCode = string.Empty;

		private string _CustomerName = string.Empty;

		private string _MachineCode = string.Empty;

		private string _RegisteDate = string.Empty;

		private object _UseDays = 0;

		private object _ConnectCount = 0;

		private string _EndDate;

		private string _LinceCode = string.Empty;

		public int IsCanUse
		{
			get
			{
				return this._IsCanUse;
			}
			set
			{
				this._IsCanUse = value;
			}
		}

		public string CustomerCode
		{
			get
			{
				return this._CustomerCode;
			}
			set
			{
				this._CustomerCode = value;
			}
		}

		public string CustomerName
		{
			get
			{
				return this._CustomerName;
			}
			set
			{
				this._CustomerName = value;
			}
		}

		public string MachineCode
		{
			get
			{
				return this._MachineCode;
			}
			set
			{
				this._MachineCode = value;
			}
		}

		public string RegisteDate
		{
			get
			{
				return this._RegisteDate;
			}
			set
			{
				this._RegisteDate = value;
			}
		}

		public object UseDays
		{
			get
			{
				return this._UseDays;
			}
			set
			{
				this._UseDays = value;
			}
		}

		public object ConnectCount
		{
			get
			{
				return this._ConnectCount;
			}
			set
			{
				this._ConnectCount = value;
			}
		}

		public string EndDate
		{
			get
			{
				return this._EndDate;
			}
			set
			{
				this._EndDate = value;
			}
		}

		public string LinceCode
		{
			get
			{
				return this._LinceCode;
			}
			set
			{
				this._LinceCode = value;
			}
		}
	}
}
