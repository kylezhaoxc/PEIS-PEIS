using System;

namespace PEIS.Model
{
	[Serializable]
	public class OnCustBackLog
	{
		private ExternByUpdateRegisteType _ExternByUpdateRegisteType;

		private long _id_backlog;

		private long _id_customer;

		private int _id_backlogtype;

		private DateTime _createdate;

		private DateTime _operatedate;

		private int _id_operator;

		private string _operator;

		private bool? _is_finished;

		public ExternByUpdateRegisteType ExternByUpdateRegisteType
		{
			get
			{
				return this._ExternByUpdateRegisteType;
			}
			set
			{
				this._ExternByUpdateRegisteType = value;
			}
		}

		public bool? Is_Finished
		{
			get
			{
				return this._is_finished;
			}
			set
			{
				this._is_finished = value;
			}
		}

		public long ID_BackLog
		{
			get
			{
				return this._id_backlog;
			}
			set
			{
				this._id_backlog = value;
			}
		}

		public long ID_Customer
		{
			get
			{
				return this._id_customer;
			}
			set
			{
				this._id_customer = value;
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

		public DateTime CreateDate
		{
			get
			{
				return this._createdate;
			}
			set
			{
				this._createdate = value;
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
	}
}
