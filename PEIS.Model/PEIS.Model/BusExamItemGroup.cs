using System;

namespace PEIS.Model
{
	[Serializable]
	public class BusExamItemGroup
	{
		private int _id_examitemgroup;

		private string _examitemgroupname;

		private string _inputcode;

		private int _disporder = 500;

		private string _note;

		private bool? _is_banned;

		private int? _id_operator;

		private string _operator;

		private DateTime? _operatedate;

		private int? _operatetype;

		private string _bandescribe;

		public int ID_ExamItemGroup
		{
			get
			{
				return this._id_examitemgroup;
			}
			set
			{
				this._id_examitemgroup = value;
			}
		}

		public string ExamItemGroupName
		{
			get
			{
				return this._examitemgroupname;
			}
			set
			{
				this._examitemgroupname = value;
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

		public string Note
		{
			get
			{
				return this._note;
			}
			set
			{
				this._note = value;
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

		public int? ID_Operator
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

		public DateTime? OperateDate
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

		public int? OperateType
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
	}
}
