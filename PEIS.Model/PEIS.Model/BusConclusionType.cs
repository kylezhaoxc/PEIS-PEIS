using System;

namespace PEIS.Model
{
	[Serializable]
	public class BusConclusionType
	{
		private int _id_conclusiontype;

		private string _conclusiontypename;

		private string _inputcode;

		private bool? _is_banned;

		private int? _id_banopr;

		private string _banoperator;

		private DateTime? _bandate;

		private string _bandescribe;

		public int ID_ConclusionType
		{
			get
			{
				return this._id_conclusiontype;
			}
			set
			{
				this._id_conclusiontype = value;
			}
		}

		public string ConclusionTypeName
		{
			get
			{
				return this._conclusiontypename;
			}
			set
			{
				this._conclusiontypename = value;
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

		public int? ID_BanOpr
		{
			get
			{
				return this._id_banopr;
			}
			set
			{
				this._id_banopr = value;
			}
		}

		public string BanOperator
		{
			get
			{
				return this._banoperator;
			}
			set
			{
				this._banoperator = value;
			}
		}

		public DateTime? BanDate
		{
			get
			{
				return this._bandate;
			}
			set
			{
				this._bandate = value;
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
