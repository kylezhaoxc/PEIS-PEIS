using System;

namespace PEIS.Model
{
	[Serializable]
	public class NatLog
	{
		private int _id_log;

		private string _operater;

		private DateTime _operatedate;

		private string _operateip;

		private int _operatetype;

		private string _operatecontent;

		public int ID_Log
		{
			get
			{
				return this._id_log;
			}
			set
			{
				this._id_log = value;
			}
		}

		public string Operater
		{
			get
			{
				return this._operater;
			}
			set
			{
				this._operater = value;
			}
		}

		public DateTime OperateDate
		{
			get
			{
				return this._operatedate;
			}
			set
			{
				this._operatedate = value;
			}
		}

		public string OperateIP
		{
			get
			{
				return this._operateip;
			}
			set
			{
				this._operateip = value;
			}
		}

		public int OperateType
		{
			get
			{
				return this._operatetype;
			}
			set
			{
				this._operatetype = value;
			}
		}

		public string OperateContent
		{
			get
			{
				return this._operatecontent;
			}
			set
			{
				this._operatecontent = value;
			}
		}
	}
}
