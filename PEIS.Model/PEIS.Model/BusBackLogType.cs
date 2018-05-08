using System;

namespace PEIS.Model
{
	[Serializable]
	public class BusBackLogType
	{
		private int _id_backlogtype;

		private string _backlogtypename;

		private bool? _is_banned;

		private string _inputcode;

		private string _bandescribe;

		private int _id_operator;

		private string _operator;

		private DateTime _operatedate;

		private int _operatetype;

		private int _disporder;

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

		public string Operator
		{
			get
			{
				return this._operator;
			}
			set
			{
				this._operator = value;
			}
		}

		public int ID_Operator
		{
			get
			{
				return this._id_operator;
			}
			set
			{
				this._id_operator = value;
			}
		}

		public string BanDescribe
		{
			get
			{
				return this._bandescribe;
			}
			set
			{
				this._bandescribe = value;
			}
		}

		public int ID_BackLogType
		{
			get
			{
				return this._id_backlogtype;
			}
			set
			{
				this._id_backlogtype = value;
			}
		}

		public string BackLogTypeName
		{
			get
			{
				return this._backlogtypename;
			}
			set
			{
				this._backlogtypename = value;
			}
		}

		public bool? Is_Banned
		{
			get
			{
				return this._is_banned;
			}
			set
			{
				this._is_banned = value;
			}
		}

		public string InputCode
		{
			get
			{
				return this._inputcode;
			}
			set
			{
				this._inputcode = value;
			}
		}

		public int DispOrder
		{
			get
			{
				return this._disporder;
			}
			set
			{
				this._disporder = value;
			}
		}
	}
}
