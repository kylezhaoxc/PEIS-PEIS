using System;

namespace PEIS.Model
{
	public class ClientInfo
	{
		private int _Interval = 3;

		private string _GUID = string.Empty;

		private LoginState _LoginState;

		private DateTime _LoginDate = DateTime.Now;

		private string _Message = string.Empty;

		private string _CpuID = string.Empty;

		private string _MacAddress = string.Empty;

		private string _DiskID = string.Empty;

		private string _IpAddress = string.Empty;

		private string _LoginUserName = string.Empty;

		private string _UserName = string.Empty;

		private string _UserID = string.Empty;

		private string _ComputerName = string.Empty;

		private string _SystemType = string.Empty;

		private string _TotalPhysicalMemory = string.Empty;

		public int Interval
		{
			get
			{
				return this._Interval;
			}
			set
			{
				this._Interval = value;
			}
		}

		public string GUID
		{
			get
			{
				return this._GUID;
			}
			set
			{
				this._GUID = value;
			}
		}

		public LoginState LoginState
		{
			get
			{
				return this._LoginState;
			}
			set
			{
				this._LoginState = value;
			}
		}

		public DateTime LoginDate
		{
			get
			{
				return this._LoginDate;
			}
			set
			{
				this._LoginDate = value;
			}
		}

		public string Message
		{
			get
			{
				return this._Message;
			}
			set
			{
				this._Message = value;
			}
		}

		public string CpuID
		{
			get
			{
				return this._CpuID;
			}
			set
			{
				this._CpuID = value;
			}
		}

		public string MacAddress
		{
			get
			{
				return this._MacAddress;
			}
			set
			{
				this._MacAddress = value;
			}
		}

		public string DiskID
		{
			get
			{
				return this._DiskID;
			}
			set
			{
				this._DiskID = value;
			}
		}

		public string IpAddress
		{
			get
			{
				return this._IpAddress;
			}
			set
			{
				this._IpAddress = value;
			}
		}

		public string UserName
		{
			get
			{
				return this._UserName;
			}
			set
			{
				this._UserName = value;
			}
		}

		public string LoginUserName
		{
			get
			{
				return this._LoginUserName;
			}
			set
			{
				this._LoginUserName = value;
			}
		}

		public string UserID
		{
			get
			{
				return this._UserID;
			}
			set
			{
				this._UserID = value;
			}
		}

		public string ComputerName
		{
			get
			{
				return this._ComputerName;
			}
			set
			{
				this._ComputerName = value;
			}
		}

		public string SystemType
		{
			get
			{
				return this._SystemType;
			}
			set
			{
				this._SystemType = value;
			}
		}

		public string TotalPhysicalMemory
		{
			get
			{
				return this._TotalPhysicalMemory;
			}
			set
			{
				this._TotalPhysicalMemory = value;
			}
		}
	}
}
